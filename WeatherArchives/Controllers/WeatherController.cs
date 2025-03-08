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

                            // Пример чтения данных из столбцов. Измените индексы в зависимости от структуры файла.
                            // Предположим, что:
                            // 0 - дата, 1 - температура, 2 - влажность, 3 - скорость ветра, 4 - описание

                            if (row.GetCell(0) == null) continue;
                            DateTime date = row.GetCell(0).DateCellValue.GetValueOrDefault();

                            double temperature = row.GetCell(2)?.NumericCellValue ?? 0;
                            int humidity = (int)(row.GetCell(3)?.NumericCellValue ?? 0);
                            double windSpeed = row.GetCell(7)?.NumericCellValue ?? 0;
                            string description = row.GetCell(11)?.ToString() ?? "";

                            var record = new WeatherRecord
                            {
                                Date = date,
                                Temperature = temperature,
                                Humidity = humidity,
                                WindSpeed = windSpeed,
                                Description = description
                            };

                            _context.WeatherRecords.Add(record);
                        }
                        
                        // Сохраняем все записи после обработки файла
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            // Здесь мы выводим подробное сообщение, которое включает inner exception
                            ViewBag.ErrorMessage = $"Ошибка при сохранении изменений: {ex.InnerException?.Message ?? ex.Message}";
                            // Если нужно, можно также залогировать весь стек вызова
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Логирование ошибки, можно использовать ILogger для записи логов
                    // Если файл не подлежит разбору, переходим к следующему файлу
                    ViewBag.ErrorMessage = $"Ошибка при обработке файла {file.FileName}: {ex.Message}";
                    // При желании можно сохранить сообщение в TempData и вывести после загрузки нескольких файлов
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
