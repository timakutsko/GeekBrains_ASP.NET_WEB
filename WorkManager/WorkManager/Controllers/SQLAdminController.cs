using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WorkManager.Responses;

namespace WorkManager.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class SQLAdminController : Controller
    {
        private readonly ILogger<SQLAdminController> _logger;

        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private CreateDefaultClients _createDefaultClients;

        public SQLAdminController(ILogger<SQLAdminController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(ClientController).Name}");

            _provider = provider;
            _createDefaultClients = provider.GetService<CreateDefaultClients>();
        }


        /// <summary>
		/// Создание БД по ДЗ №2
		/// </summary>
		[HttpGet("create")]
        public IActionResult CreateElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода генерации данных в БД");

            _createDefaultClients.Create();

            return Ok("База данных клиентов по умолчанию - наполнена успешно!");
        }
    }
}
