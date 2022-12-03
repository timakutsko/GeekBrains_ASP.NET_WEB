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
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private ClientResponse _clientResponse = new ClientResponse();

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(ClientController).Name}");
        }

        /// <summary>
		/// Запрос списка клиентов
		/// </summary>
		/// <returns>Список клиентов</returns>
		[HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех клиентов. Параметры:...");
           
            IList<ITSModel> allElements = _clientResponse.GetAllData();

            return Ok();
        }

        /// <summary>
        /// Запрос клиента по id
        /// </summary>
        /// <returns>Необходимый клиент</returns>
        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения клиента по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                ITSModel currentElement = _clientResponse.GetById(id);
                
                return Ok(currentElement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создание нового килента
        /// </summary>
        /// <returns>Созданный клиент</returns>
        [HttpPost("register")]
        public IActionResult RegisterElement()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания нового клиента. Параметры:...");

            ITSModel newElement = _clientResponse.Register();

            return Ok(newElement);
        }

        /// <summary>
        /// Обновление клиента по id
        /// </summary>
        /// <returns>Обновленный клиент</returns>
        [HttpPut("update/{id}")]
        public IActionResult UpdateById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления клиента по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                ITSModel updateElementt = _clientResponse.UpdateById(id);

                return Ok(updateElementt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаление клиента по id
        /// </summary>
        /// <returns>Сообщение об удалении</returns>
        [HttpPut("delete/{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода удаления клиента по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                _clientResponse.DeleteById(id);
                
                return Ok("Client have been deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
