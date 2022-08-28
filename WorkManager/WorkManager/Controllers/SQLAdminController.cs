using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WorkManager.DAL.Models;
using WorkManager.DAL.Repositories;
using WorkManager.MySQLsettings;
using WorkManager.Responses;

namespace WorkManager.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class SQLAdminController : Controller
    {
        private readonly ILogger<SQLAdminController> _logger;
        private CreateDefaultClients _createDefaultClients = new CreateDefaultClients();

        public SQLAdminController(ILogger<SQLAdminController> logger)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(InvoiceController).Name}");
        }

        /// <summary>
		/// Создание БД по ДЗ №2
		/// </summary>
		[HttpGet("create")]
        public IActionResult CreateElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода генерации данных в БД");

            _createDefaultClients.Create();

            return Ok();
        }
    }
}
