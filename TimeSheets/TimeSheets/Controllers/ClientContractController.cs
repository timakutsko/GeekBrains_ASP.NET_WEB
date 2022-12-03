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
    [Route("api/contracts")]
    public class ClientContractController : ControllerBase
    {
        private readonly ILogger<ClientContractController> _logger;
        private ClientContractResponse _clientContractResponse = new ClientContractResponse();

        public ClientContractController(ILogger<ClientContractController> logger)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(ClientContractController).Name}");
        }

        /// <summary>
		/// Запрос списка контрактов
		/// </summary>
		/// <returns>Список кантрактов</returns>
		[HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех контрактов. Параметры:...");

            IList<ITSModel> allElements = _clientContractResponse.GetAllData();

            return Ok();
        }

        /// <summary>
        /// Запрос контракта по id
        /// </summary>
        /// <returns>Необходимый контракт</returns>
        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения контракта по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                ITSModel currentElement = _clientContractResponse.GetById(id);
                
                return Ok(currentElement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создание нового контратка
        /// </summary>
        /// <returns>Созданный контракт</returns>
        [HttpPost("register")]
        public IActionResult RegisterElement()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания нового контракта. Параметры:...");

            ITSModel newElement = _clientContractResponse.Register();

            return Ok(newElement);
        }

        /// <summary>
        /// Обновление контратка по id
        /// </summary>
        /// <returns>Обновленный контракт</returns>
        [HttpPut("update/{id}")]
        public IActionResult UpdateById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления контракта по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                ITSModel updateElementt = _clientContractResponse.UpdateById(id);
                
                return Ok(updateElementt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаление контратка по id
        /// </summary>
        /// <returns>Сообщение об удалении</returns>
        [HttpPut("delete/{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода удаления контракта по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                _clientContractResponse.DeleteById(id);
                
                return Ok("Contract have been deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
