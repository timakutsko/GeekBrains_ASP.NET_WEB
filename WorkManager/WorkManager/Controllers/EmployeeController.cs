using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WorkManager.Data.Models;
using WorkManager.Responses;
using WorkManager.Responses.Interfaces;

namespace WorkManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        private readonly IResponse<Employee> _response;

        public EmployeeController(ILogger<EmployeeController> logger, IResponse<Employee> response)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(EmployeeController).Name}");

            _response = response;
        }

        /// <summary>
        /// Создание нового сотрудника
        /// </summary>
        /// <returns>Созданный клиент</returns>
        [HttpPost("register")]
        public IActionResult RegisterElement([FromBody] Employee employee)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания нового клиента. Параметры:" +
                $"\nId: {employee.Id}" +
                $"\nFirst name: {employee.FirstName}" +
                $"\nLast name: {employee.LastName}" +
                $"\nEmail: {employee.Email}" +
                $"\nAge: {employee.Age}" +
                $"\nHourSalary: {employee.HourSalary}" +
                $"\nSpendingTime: {employee.SpendingTime}");

            try
            {
                _response.Register(employee);
                return Ok($"Сотрудник {employee.FirstName} {employee.LastName} был создан!");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
		/// Запрос списка сотрудников
		/// </summary>
		/// <returns>Список клиентов</returns>
        [HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех клиентов.");

            try
            {
                IReadOnlyDictionary<int, Employee> resp = _response.GetAllData();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Запрос клиента по id
        /// </summary>
        /// <returns>Необходимый сотрудник</returns>
        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения сотрудника по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                Employee currentElement = _response.GetById(id);
                return Ok(currentElement);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Обновление сотрудника по id
        /// </summary>
        /// <returns>Обновленный сотрудник</returns>
        [HttpPut("update/{id}/{reqColumnName}/{value}")]
        public IActionResult UpdateById([FromRoute] int id, [FromRoute] string reqColumnName, string value)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления сотрудника по id. Параметры:" +
                $"\nId для обновления: {id}" +
                $"\nИмя парамтера для обновления: {reqColumnName}" +
                $"\nЗначение для обновления: {value}");

            try
            {
                _response.UpdateById(id, reqColumnName, value);
                return Ok($"Клиенту с id {id} был обновлен параметр {reqColumnName} на значение {value}!");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Удаление сотрудника по id
        /// </summary>
        /// <returns>Сообщение об удалении</returns>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода удаления сотрудника по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                _response.DeleteById(id);
                return Ok($"Сотрудник с id {id} был успешно удален!");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
