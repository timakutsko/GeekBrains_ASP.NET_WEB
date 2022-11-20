using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WorkManager.Data.Contexts;
using WorkManager.Data.Models;
using WorkManager.Models;
using WorkManager.Repositories.Interfaces;
using WorkManager.Responses.Interfaces;
using WorkManager.Tokens;

namespace WorkManager.Responses
{
    internal sealed class AccountResponse : IAccountResponse
    {
        /// <summary>
        /// Секртеное слово для токена
        /// </summary>
        private const string _secretCode = "Secret 1! Secret 2! Secret 3!";

        private readonly Dictionary<string, SessionDto> _sessionDtos = new Dictionary<string, SessionDto>();

        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        
        private readonly IUserRepository _repository;
        
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AccountResponse(IServiceProvider provider, IServiceScopeFactory serviceScopeFactory)
        {
            _provider = provider;
            _serviceScopeFactory = serviceScopeFactory;
            _repository = provider.GetService<IUserRepository>();
        }

        public static string SecretCode
        {
            get { return _secretCode; }
        }

        public void Registration(string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) || !string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"Registration is failed! Do not leave empty lines");
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            WorkManagerDbContext context = scope.ServiceProvider.GetRequiredService<WorkManagerDbContext>();

            Account account = FindAccByLogin(context, login);
            if (account != null)
            {
                throw new ArgumentException($"This login is buzy! Input something else");
            }

            string passwordSalt = GeneratePassowrdSalt();
            string passwrdHash = GetPasswordHash(password, passwordSalt);
            if (!_repository.Create(new Account(login, passwordSalt, passwrdHash)))
            {
                throw new Exception("Can't create a user. Check out input data");
            }
        }

        public AuthenticateDto Authenticate(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return new AuthenticateDto()
                {
                    Status = AuthenticationStatus.UserNotFound
                };
            }
            
            using IServiceScope scope = _serviceScopeFactory.CreateScope(); 
            WorkManagerDbContext context = scope.ServiceProvider.GetRequiredService<WorkManagerDbContext>();

            Account account = FindAccByLogin(context, login);
            if (account == null)
            {
                return new AuthenticateDto()
                {
                    Status = AuthenticationStatus.InvalidLogin
                };
            }

            if (!VerifyPassowrd(password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticateDto()
                {
                    Status = AuthenticationStatus.InvalidPassword
                };
            }

            AccountSession session = new AccountSession()
            {
                AccountId = account.Id,
                SessionToken = GenerateJwtToken(account.Id, account.Login, 1),
                TimeCreated = DateTime.Now,
                IsClosed = false,
            };

            context.AccountSessions.Add(session);
            context.SaveChanges();

            SessionDto sessionDto = GetSessionDto(account, session);

            lock (_sessionDtos)
            {
                _sessionDtos[session.SessionToken] = sessionDto;
            }

            return new AuthenticateDto()
            {
                Status = AuthenticationStatus.Success,
                SessionDto = sessionDto
            };
        }

        public SessionDto GetSession(string token)
        {
            SessionDto sessionDto;

            lock (_sessionDtos)
            {
                _sessionDtos.TryGetValue(token, out sessionDto);
            }
            
            if (sessionDto == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                WorkManagerDbContext context = scope.ServiceProvider.GetRequiredService<WorkManagerDbContext>();

                AccountSession session = context.AccountSessions.FirstOrDefault(session => session.SessionToken == token);
                if (session == null)
                    return null;

                Account account = context.Accounts.FirstOrDefault(account => account.Id == session.AccountId);

                sessionDto = GetSessionDto(account, session);

                if (sessionDto != null)
                {
                    lock (_sessionDtos)
                    {
                        _sessionDtos[session.SessionToken] = sessionDto;
                    }
                }
            }

            return sessionDto;
        }

        public string RefreshToken(string refreshToken)
        {
            //IReadOnlyDictionary<int, Account> usersDict = _repository.Get();

            //foreach (KeyValuePair<int, Account> pair in usersDict)
            //{
            //    if (string.CompareOrdinal(pair.Value.RefreshToken, refreshToken) == 0 && AuthenticateDto.LatestRefreshToken.IsExpired is false)
            //    {
            //        string accessToken = GenerateJwtToken(pair.Key, pair.Value.Login, 1);

            //        return accessToken;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}

            throw new Exception($"Error with DB!");
        }

        private Account FindAccByLogin(WorkManagerDbContext context, string login)
        {
            return context.Accounts.FirstOrDefault(a => a.Login.Equals(login));
        }

        private SessionDto GetSessionDto(Account account, AccountSession accountSession)
        {
            return new SessionDto()
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                AccountDto = new AccountDto()
                {
                    Id = account.Id,
                    Login = account.Login
                }
            };
        }

        private string GenerateJwtToken(int id, string name, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] 
                    { 
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.NameIdentifier, id.ToString()) 
                    }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(int id, string name)
        {
            RefreshToken refreshToken = new RefreshToken();
            refreshToken.Expires = DateTime.Now.AddMinutes(2);
            refreshToken.Token = GenerateJwtToken(id, name, 2);

            return refreshToken;
        }

        /// <summary>
        /// Проверка пароля на соответвие хэш-паролю из БД
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="salt">Соль</param>
        /// <param name="verifyPasswordHash">Хэш-пароль, с которым нужно сравнить</param>
        /// <returns></returns>
        private bool VerifyPassowrd(string password, string salt, string verifyPasswordHash)
        {
            return GetPasswordHash(password, salt) == verifyPasswordHash;
        }

        /// <summary>
        /// Генерация хэш-пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="salt">Соль</param>
        /// <returns></returns>
        private string GetPasswordHash(string password, string salt)
        {
            password = $"{password}~{salt}~{SecretCode}";

            byte[] key = Encoding.ASCII.GetBytes(password);
            SHA512 sha512 = new SHA512Managed();
            byte[] hash = sha512.ComputeHash(key);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Генерация рандомной соли
        /// </summary>
        /// <returns></returns>
        private string GeneratePassowrdSalt()
        {
            byte[] buffer = new byte[16];
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            rNGCryptoServiceProvider.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }
    }
}
