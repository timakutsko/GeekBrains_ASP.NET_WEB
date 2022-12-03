using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using WorkManager.Models;
using WorkManager.Requests;
using WorkManager.Responses;
using WorkManager.Responses.Interfaces;

namespace WorkManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;

        private readonly IAccountResponse _accountResponse;

        private readonly IValidator<AuthenticateRequest> _authValidator;

        public AccountsController(ILogger<AccountsController> logger, IAccountResponse accountResponse, IValidator<AuthenticateRequest> authValidator)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(AccountsController).Name}");

            _accountResponse = accountResponse;

            _authValidator = authValidator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticateDto), StatusCodes.Status200OK)]
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <returns>Созданный токен</returns>
        public IActionResult Authenticate([FromBody] AuthenticateRequest auth)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания аутентификации пользователя. Параметры:" +
                $"\nLogin: {auth.Login}" +
                $"\nPassword: {auth.Password}");

            ValidationResult validationResult = _authValidator.Validate(auth);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            try
            {
                AuthenticateDto authenticateDto = _accountResponse.Authenticate(auth);

                if (authenticateDto.Status == AuthenticationStatus.Success)
                {
                    Response.Headers.Add("X-Session-Token", authenticateDto.SessionDto.SessionToken);
                    SetTokenCookie(authenticateDto.SessionDto.SessionToken);

                    return Ok(authenticateDto);
                }

                return Conflict(authenticateDto);
                //return Ok($"Успешная аутентификация для пользователя: {authenticateDto.AccountDto.Id} - {authenticateDto.AccountDto.Login}." +
                //    $"\nТокен: {authenticateDto.SessionDto.SessionToken}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <returns>Созданный токен</returns>
        public IActionResult Registration([FromBody] AuthenticateRequest auth)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода регистрации пользователя. Параметры:" +
                $"\nLogin: {auth.Login}" +
                $"\nPassword: {auth.Password}");

            ValidationResult validationResult = _authValidator.Validate(auth);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            try
            {
                _accountResponse.Registration(auth);

                return Ok($"Успешная регистрация для пользователя: {auth.Login}.");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        [Route("session")]
        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
        /// <summary>
        /// Получить текущую сессию
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSession()
        {
            var authHeader = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authHeader, out var authHeaderValue))
            {
                // Bearer
                var scheme = authHeaderValue.Scheme;
                // Token
                var token = authHeaderValue.Parameter;
                if (string.IsNullOrEmpty(token))
                    return Unauthorized();

                SessionDto sessionDto = _accountResponse.GetSession(token);
                if (sessionDto == null)
                    return Unauthorized();

                return Ok(sessionDto);
            }

            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        /// <summary>
        /// Обновление токенов
        /// </summary>
        /// <returns>Обновленный токен</returns>
        public IActionResult Refresh()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления токенов.");

            try
            {
                string oldRefreshToken = Request.Cookies["refreshToken"];
                string newRefreshToken = _accountResponse.RefreshToken(oldRefreshToken);
                if (string.IsNullOrWhiteSpace(newRefreshToken))
                {
                    return Unauthorized(new { message = "Old refresh token" });
                }

                SetTokenCookie(newRefreshToken);

                return Ok($"Успешная обновление токена доступа:" +
                    $"\n{newRefreshToken}");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
