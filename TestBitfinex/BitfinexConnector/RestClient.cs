using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BitfinexConnector.Models;

namespace BitfinexConnector
{
    public class RestClient
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://api-pub.bitfinex.com/v2/";

        public RestClient(HttpClient client)
        {
            _client = client;
        }

        // Метод для получения списка последних трейдов
        public async Task<List<Trade>> GetNewTrades(string pair, int maxCount)
        {
            var url = $"{BaseUrl}trades/t{pair}/hist?limit={maxCount}";
            var response = await _client.GetStringAsync(url);
            return ParseTrades(response, pair);
        }

        // Метод для парсинга списка трейдов из JSON
        public List<Trade> ParseTrades(string json, string pair)
        {
            var trades = JsonSerializer.Deserialize<List<List<object>>>(json);
            var tradeList = new List<Trade>();

            foreach (var trade in trades)
            {
                tradeList.Add(new Trade
                {
                    Pair = pair,
                    Price = Convert.ToDecimal(trade[3]),
                    Amount = Convert.ToDecimal(trade[2]),
                    Side = Convert.ToDecimal(trade[2]) > 0 ? "buy" : "sell",
                    Time = DateTimeOffset.FromUnixTimeSeconds((int)trade[1]),
                    Id = trade[0].ToString()
                });
            }
            return tradeList;
        }

    }
}
