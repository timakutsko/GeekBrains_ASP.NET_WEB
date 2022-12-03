using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TimeSheets.DAL.Interfaces;
using TimeSheets.DAL.Models;
using TimeSheets.Responses;

namespace TimeSheets.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private InvoiceResponse _invoiceResponse = new InvoiceResponse();

        public InvoiceController(ILogger<InvoiceController> logger)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(InvoiceController).Name}");
        }

        /// <summary>
		/// Запрос списка счетов
		/// </summary>
		/// <returns>Список сотрудников</returns>
		[HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех счетов. Параметры:...");

            IList<ITSModel> allElements = _invoiceResponse.GetAllData();

            return Ok();
        }

        /// <summary>
        /// Обновление всех счетов
        /// </summary>
        /// <returns>Обновленный сотрудник</returns>
        [HttpPut("update")]
        public IActionResult UpdateElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления всех счетов. Параметры:...");

            try
            {
                _invoiceResponse.UpdateAllData();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
