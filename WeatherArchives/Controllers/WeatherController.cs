using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherArchives.Data;
using WeatherArchives.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;

namespace WeatherArchives.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherContext _context;

        public WeatherController(WeatherContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Upload()
        {
            ViewData["Message"] = "";
            ViewData["ErrorMessage"] = "";
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewBag.Message = "Не выбран ни один файл.";
                return View();
            }

            foreach (var file in files)
            {
                try
                {
                    using (var stream = file.OpenReadStream())
                    {
                        // Предполагается, что файлы в формате .xlsx
                        IWorkbook workbook = new XSSFWorkbook(stream);
                        ISheet sheet = workbook.GetSheetAt(0);

                        // Пропускаем строку заголовков (если она есть)
                        for (int rowIndex = 4; rowIndex <= sheet.LastRowNum; rowIndex++)
                        {
                            IRow row = sheet.GetRow(rowIndex);
                            if (row == null) continue;
                            if (row.GetCell(0) == null) continue;
                            
                            string dateStr = row.GetCell(0).StringCellValue.Trim(); // Формат: dd.MM.YYYY
                            string timeStr = row.GetCell(1).StringCellValue.Trim(); // Формат: HH:mm
                            string dateTimeString = $"{dateStr} {timeStr}";
                            DateTime dateTime = DateTime.ParseExact(dateTimeString, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                            double temperature = row.GetCell(2).CellType != CellType.String ? row.GetCell(2).NumericCellValue : 0;
                            double humidity = row.GetCell(3).CellType != CellType.String ? row.GetCell(3).NumericCellValue : 0;
                            double td = row.GetCell(4).CellType != CellType.String ? row.GetCell(4).NumericCellValue : 0;
                            int pressure = row.GetCell(5).CellType != CellType.String ? (int)(row.GetCell(5).NumericCellValue) : 0;
                            string direction = row.GetCell(6)?.ToString() ?? "";
                            int windSpeed = row.GetCell(7).CellType != CellType.String ? (int)(row.GetCell(7).NumericCellValue) : 0;
                            int clouds = row.GetCell(8).CellType != CellType.String ? (int)(row.GetCell(8).NumericCellValue) : 0;
                            int h  = row.GetCell(9).CellType != CellType.String ? (int)(row.GetCell(9).NumericCellValue) : 0;
                            int vv  = row.GetCell(10).CellType != CellType.String ? (int)(row.GetCell(10).NumericCellValue) : 0;
                            string description = row.GetCell(11)?.ToString() ?? "";

                            var record = new WeatherRecord
                            {
                                Date = dateTime,
                                Temperature = temperature,
                                Humidity = humidity,
                                Td = td,
                                AirPressure = pressure,
                                WindDirection = direction,
                                WindSpeed = windSpeed,
                                Clouds = clouds,
                                H = h,
                                VV = vv,
                                Description = description
                            };

                            _context.WeatherRecords.Add(record);
                        }
                        
                        // Сохраняем все записи после обработки файла
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Ошибка при обработке файла {file.FileName}: {ex.Message}";
                }
            }

            ViewBag.Message = "Файлы успешно обработаны.";
            return View();
        }

        // Страница просмотра архивов
        public async Task<IActionResult> List(int? month, int? year)
        {
            var query = _context.WeatherRecords.AsQueryable();

            if (year.HasValue)
                query = query.Where(w => w.Date.Year == year.Value);
            if (month.HasValue)
                query = query.Where(w => w.Date.Month == month.Value);

            // Получаем все записи (отсортированные по дате в порядке убывания)
            var records = await query
                .OrderByDescending(w => w.Date)
                .ToListAsync();

            return View(records);
        }
    }
}
