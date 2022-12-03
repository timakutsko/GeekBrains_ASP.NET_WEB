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
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private EmployeeResponse _employeeResponse;

        public EmployeeController(ILogger<EmployeeController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(EmployeeController).Name}");

            _provider = provider;
            _employeeResponse = provider.GetService<EmployeeResponse>();
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
                _employeeResponse.Register(employee);
                return Ok($"Сотрудник {employee.FirstName} {employee.LastName} был создан!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                IReadOnlyDictionary<int, Employee> resp = _employeeResponse.GetAllData();
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
        /// <returns>Необходимый сотрудник</returns>
        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения сотрудника по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                Employee currentElement = _employeeResponse.GetById(id);
                return Ok(currentElement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                _employeeResponse.UpdateById(id, reqColumnName, value);
                return Ok($"Клиенту с id {id} был обновлен параметр {reqColumnName} на значение {value}!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                _employeeResponse.DeleteById(id);
                return Ok($"Сотрудник с id {id} был успешно удален!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
