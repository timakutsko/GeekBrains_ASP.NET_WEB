using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    [Route("api/clients")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        
        private readonly IServiceProvider _provider;
        
        private ClientResponse _clientResponse;
        
        private readonly IValidator<Client> _clientValidator;

        public ClientController(ILogger<ClientController> logger, IServiceProvider provider, IValidator<Client> clientValidator)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(ClientController).Name}");

            _provider = provider;
            _clientResponse = provider.GetService<ClientResponse>();

            _clientValidator = clientValidator;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Создание нового килента
        /// </summary>
        /// <returns>Созданный клиент</returns>
        public IActionResult RegisterElement([FromBody] Client client)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания нового клиента. Параметры:" +
                $"\nFirst name: {client.FirstName}" +
                $"\nLast name: {client.LastName}" +
                $"\nEmail: {client.Email}" +
                $"\nAge: {client.Age}" +
                $"\nCompany: {client.Company}");

            ValidationResult validationResult = _clientValidator.Validate(client);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            try
            {
                _clientResponse.Register(client);
                return Ok($"Клиент {client.FirstName} {client.LastName} был создан!");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("get")]
        /// <summary>
		/// Запрос списка клиентов
		/// </summary>
		/// <returns>Список клиентов</returns>
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
                return Conflict(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        /// <summary>
        /// Запрос клиента по id
        /// </summary>
        /// <returns>Необходимый клиент</returns>
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
                return Conflict(ex.Message);
            }
        }

        [HttpPut("update/{id}/{reqColumnName}/{value}")]
        /// <summary>
        /// Обновление клиента по id
        /// </summary>
        /// <returns>Обновленный клиент</returns>
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
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        /// <summary>
        /// Удаление клиента по id
        /// </summary>
        /// <returns>Сообщение об удалении</returns>
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
                return Conflict(ex.Message);
            }
        }
    }
}
