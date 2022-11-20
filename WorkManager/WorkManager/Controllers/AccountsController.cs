using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http.Headers;
using WorkManager.Models;
using WorkManager.Responses;
using WorkManager.Tokens;

namespace WorkManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private AccountResponse _accountResponse;

        public AccountsController(ILogger<AccountsController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(AccountsController).Name}");

            _provider = provider;
            _accountResponse = provider.GetService<AccountResponse>();
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <returns>Созданный токен</returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(AuthenticateDto), StatusCodes.Status200OK)]
        public IActionResult Authenticate([FromQuery] string login, string password)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания аутентификации пользователя. Параметры:" +
                $"\nLogin: {login}" +
                $"\nPassword: {password}");

            try
            {
                AuthenticateDto authenticateDto = _accountResponse.Authenticate(login, password);

                if (authenticateDto.Status == AuthenticationStatus.Success)
                {
                    Response.Headers.Add("X-Session-Token", authenticateDto.SessionDto.SessionToken);
                    SetTokenCookie(authenticateDto.SessionDto.SessionToken);
                }

                return Ok(authenticateDto);
                //return Ok($"Успешная аутентификация для пользователя: {authenticateDto.AccountDto.Id} - {authenticateDto.AccountDto.Login}." +
                //    $"\nТокен: {authenticateDto.SessionDto.SessionToken}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <returns>Созданный токен</returns>
        [AllowAnonymous]
        [HttpPost("Registration")]
        public IActionResult Registration([FromQuery] string login, string password)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода регистрации пользователя. Параметры:" +
                $"\nLogin: {login}" +
                $"\nPassword: {password}");

            try
            {
                _accountResponse.Registration(login, password);

                return Ok($"Успешная регистрация для пользователя: {login}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получить текущую сессию
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("session")]
        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
        public IActionResult GetSession()
        {
            var authHeader =  Request.Headers[HeaderNames.Authorization];
            if(AuthenticationHeaderValue.TryParse(authHeader, out var authHeaderValue))
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

        /// <summary>
        /// Обновление токенов
        /// </summary>
        /// <returns>Обновленный токен</returns>
        [HttpPost("refresh-token")]
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
                return BadRequest(ex.Message);
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
