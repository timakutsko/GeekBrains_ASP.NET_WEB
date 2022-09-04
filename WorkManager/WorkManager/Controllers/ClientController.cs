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
    [Route("api/clients")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private ClientResponse _clientResponse;

        public ClientController(ILogger<ClientController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(ClientController).Name}");
            
            _provider = provider;
            _clientResponse = provider.GetService<ClientResponse>();
        }

        /// <summary>
        /// Создание нового килента
        /// </summary>
        /// <returns>Созданный клиент</returns>
        [HttpPost("register")]
        public IActionResult RegisterElement([FromBody] Client client)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания нового клиента. Параметры:" +
                $"\nId: {client.Id}" +
                $"\nFirst name: {client.FirstName}" +
                $"\nLast name: {client.LastName}" +
                $"\nEmail: {client.Email}" +
                $"\nAge: {client.Age}" +
                $"\nCompany: {client.Company}");
            
            try
            {
                _clientResponse.Register(client);
                return Ok($"Клиент {client.FirstName} {client.LastName} был создан!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
		/// Запрос списка клиентов
		/// </summary>
		/// <returns>Список клиентов</returns>
		[HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех клиентов.");
            
            try 
            {
                IReadOnlyDictionary<int, Client> resp = _clientResponse.GetAllData();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                Client currentElement = _clientResponse.GetById(id);
                return Ok(currentElement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление клиента по id
        /// </summary>
        /// <returns>Обновленный клиент</returns>
        [HttpPut("update/{id}/{reqColumnName}/{value}")]
        public IActionResult UpdateById([FromRoute] int id, [FromRoute] string reqColumnName, string value)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления клиента по id. Параметры:" +
                $"\nId для обновления: {id}" +
                $"\nИмя парамтера для обновления: {reqColumnName}" +
                $"\nЗначение для обновления: {value}");

            try
            {
                _clientResponse.UpdateById(id, reqColumnName, value);
                return Ok($"Клиенту с id {id} был обновлен параметр {reqColumnName} на значение {value}!");
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
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода удаления клиента по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                _clientResponse.DeleteById(id);
                return Ok($"Клиент с id {id} был успешно удален!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
