using BitfinexConnector.Models;
using BitfinexConnector;
using Microsoft.AspNetCore.Mvc;

namespace TestBitfinex.Controllers
{
    public class BitfinexController : Controller
    {
        private readonly RestClient _restClient;

        public BitfinexController()
        {
            _restClient = new RestClient(new HttpClient());
        }   
        public async Task<IActionResult> Trades(string pair, int count = 5)
        {
            var trades = await _restClient.GetNewTrades(pair, count);
            return View(trades);
        }
    }
}
