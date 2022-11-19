using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WorkManager.Data.Models;
using WorkManager.Responses;
using WorkManager.Tokens;

namespace WorkManager.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private UserResponse _userResponse;

        public UsersController(ILogger<UsersController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _logger.LogInformation($"\n[MyInfo]: Вызов конструктора класса {typeof(UsersController).Name}");

            _provider = provider;
            _userResponse = provider.GetService<UserResponse>();
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <returns>Созданный токен</returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromQuery] string login, string password)
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода создания аутентификации пользователя. Параметры:" +
                $"\nLogin: {login}" +
                $"\nPassword: {password}");

            try
            {
                ContainerTokens containerTokens = _userResponse.Authenticate(login, password);

                SetTokenCookie(containerTokens.RefreshToken.Token);

                return Ok($"Успешная аутентификация для пользователя: {login}." +
                    $"\nТокен: {containerTokens.AccessToken}");
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
                _userResponse.Registration(login, password);

                return Ok($"Успешная регистрация для пользователя: {login}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление токенов
        /// </summary>
        /// <returns>Обновленный токен</returns>
        [Authorize]
        [HttpPost("refresh-token")]
        public IActionResult Refresh()
        {
            _logger.LogInformation("\n[MyInfo]: Вызов метода обновления токенов.");

            try
            {
                string oldRefreshToken = Request.Cookies["refreshToken"];
                string newRefreshToken = _userResponse.RefreshToken(oldRefreshToken);
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
