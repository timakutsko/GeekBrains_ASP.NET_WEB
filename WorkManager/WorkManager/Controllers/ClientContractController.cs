using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WorkManager.DAL.Interfaces;
using WorkManager.DAL.Models;
using WorkManager.Responses;

namespace WorkManager.Controllers
{
    [ApiController]
    [Route("api/contracts")]
    public class ClientContractController : Controller
    {
        private readonly ILogger<ClientContractController> _logger;
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private ClientContractResponse _clientContractResponse;

        public ClientContractController(ILogger<ClientContractController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(ClientContractController).Name}");

            _provider = provider;
            _clientContractResponse = provider.GetService<ClientContractResponse>();
        }

        /// <summary>
        /// Создание нового контратка
        /// </summary>
        /// <returns>Созданный контракт</returns>
        [HttpPost("register")]
        public IActionResult RegisterElement([FromBody] ClientContract clientContract)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания нового контракта. Параметры:" +
                $"\nId: {clientContract.Id}" +
                $"\nTitle: {clientContract.Title}" +
                $"\nFullTime: {clientContract.FullTime}");

            try
            {
                _clientContractResponse.Register(clientContract);
                return Ok($"Контракт {clientContract.Title} был создан!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    }

        /// <summary>
		/// Запрос списка контрактов
		/// </summary>
		/// <returns>Список кантрактов</returns>
		[HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех контрактов. Параметры:...");

            try
            {
                IReadOnlyDictionary<int, ClientContract> resp = _clientContractResponse.GetAllData();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                ClientContract currentElement = _clientContractResponse.GetById(id);
                
                return Ok(currentElement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление контратка по id
        /// </summary>
        /// <returns>Обновленный контракт</returns>
        [HttpPut("update/{id}/{reqColumnName}/{value}")]
        public IActionResult UpdateById([FromRoute] int id, [FromRoute] string reqColumnName, string value)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления контракта по id. Параметры:" +
                $"\nId для обновления: {id}" +
                $"\nИмя парамтера для обновления: {reqColumnName}" +
                $"\nЗначение для обновления: {value}");

            try
            {
                _clientContractResponse.UpdateById(id, reqColumnName, value);
                return Ok($"Контракту с id {id} был обновлен параметр {reqColumnName} на значение {value}!");
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
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода удаления контракта по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                _clientContractResponse.DeleteById(id);
                
                return Ok($"Контракт с id{id} был успешно удален!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
