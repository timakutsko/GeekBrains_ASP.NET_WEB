using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WorkManager.Data.Models;
using WorkManager.Responses;

namespace WorkManager.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/invoices")]
    public class InvoiceController : Controller
    {
        private readonly ILogger<InvoiceController> _logger;
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private InvoiceResponse _response;

        public InvoiceController(ILogger<InvoiceController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(InvoiceController).Name}");

            _provider = provider;
            _response = provider.GetService<InvoiceResponse>();
        }

        /// <summary>
		/// Запрос списка счетов
		/// </summary>
		/// <returns>Список сотрудников</returns>
        [HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех счетов. Параметры:...");

            try
            {
                IReadOnlyDictionary<int, Invoice> resp = _response.GetAllData();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление всех счетов
        /// </summary>
        /// <returns>Обновленный сотрудник</returns>
        [HttpPut("update/{id}/{reqColumnName}/{value}")]
        public IActionResult UpdateById([FromRoute] int id, [FromRoute] string reqColumnName, string value)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления клиента по id. Параметры:" +
                $"\nId для обновления: {id}" +
                $"\nИмя парамтера для обновления: {reqColumnName}" +
                $"\nЗначение для обновления: {value}");

            try
            {
                _response.UpdateById(id, reqColumnName, value);
                return Ok($"Счет с id {id} был обновлен параметр {reqColumnName} на значение {value}!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
