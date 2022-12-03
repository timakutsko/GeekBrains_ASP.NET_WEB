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
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private EmployeeResponse _employeeResponse = new EmployeeResponse();

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(EmployeeController).Name}");
        }

        /// <summary>
		/// Запрос списка сотрудников
		/// </summary>
		/// <returns>Список сотрудников</returns>
		[HttpGet("get")]
        public IActionResult GetElements()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода получения всех сотрудников. Параметры:...");

            IList<ITSModel> allElements = _employeeResponse.GetAllData();

            return Ok();
        }

        /// <summary>
        /// Запрос сотрудника по id
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
                ITSModel currentElement = _employeeResponse.GetById(id);
                
                return Ok(currentElement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создание нового сотрудника
        /// </summary>
        /// <returns>Созданный сотрудник</returns>
        [HttpPost("register")]
        public IActionResult RegisterElement()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания нового сотрудника. Параметры:...");

            ITSModel newElement = _employeeResponse.Register();

            return Ok(newElement);
        }

        /// <summary>
        /// Обновление сотрудника по id
        /// </summary>
        /// <returns>Обновленный сотрудник</returns>
        [HttpPut("update/{id}")]
        public IActionResult UpdateById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления сотрудника по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                ITSModel updateElementt = _employeeResponse.UpdateById(id);

                return Ok(updateElementt);
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
        [HttpPut("delete/{id}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода удаления сотрудника по id. Параметры:" +
                $"id = {id}"
                );

            try
            {
                _employeeResponse.DeleteById(id);
                
                return Ok("Employee have been deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
