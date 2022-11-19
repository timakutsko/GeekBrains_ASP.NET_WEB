using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WorkManager.Data.Models;
using WorkManager.Repositories.Interfaces;
using WorkManager.Responses.Interfaces;
using WorkManager.Tokens;

namespace WorkManager.Responses
{
    internal sealed class UserResponse : IUserResponse
    {
        /// <summary>
        /// Секртеное слово для токена
        /// </summary>
        private static string _secretCode = "Secret 1! Secret 2! Secret 3!";

        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IUserRepository _repository;

        public UserResponse(IServiceProvider provider)
        {
            _provider = provider;
            _repository = provider.GetService<IUserRepository>();
        }

        public static string SecretCode
        {
            get { return _secretCode; }
        }

        public ContainerTokens Authenticate(string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) || !string.IsNullOrWhiteSpace(password))
            {
                IReadOnlyDictionary<int, User> usersDict = _repository.Get();

                ContainerTokens containerTokens = new ContainerTokens();

                foreach (KeyValuePair<int, User> pair in usersDict)
                {
                    if (string.CompareOrdinal(pair.Value.Login, login) == 0 
                        && VerifyPassowrd(password, pair.Value.PasswordSalt, pair.Value.PasswordHash))
                    {
                        string accessToken = GenerateJwtToken(pair.Key, pair.Value.Login, 1);
                        RefreshToken refreshToken = GenerateRefreshToken(pair.Key, pair.Value.Login);

                        containerTokens.AccessToken = accessToken;
                        containerTokens.RefreshToken = refreshToken;

                        if (_repository.SetRefreshToken(pair.Value.Login, pair.Value.PasswordHash, refreshToken.Token))
                        {
                            AuthResponse.CurrentUser = pair.Value;
                            AuthResponse.LatestRefreshToken = refreshToken;
                        }
                        else
                        {
                            throw new ArgumentException($"Error with DB work with User!");
                        }
                        return containerTokens;
                    }
                }

                throw new ArgumentException($"Now user, like this!");
            }
            else
            {
                throw new Exception($"Authenticate is failed!");
            }
        }

        public void Registration(string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) || !string.IsNullOrWhiteSpace(password))
            {
                IReadOnlyDictionary<int, User> usersDict = _repository.Get();

                if(usersDict != null)
                {
                    foreach (KeyValuePair<int, User> pair in usersDict)
                    {
                        if (string.CompareOrdinal(pair.Value.Login, login) == 0)
                        {
                            throw new ArgumentException($"This login is buzy! Input something else");
                        }
                    }
                }

                string passwordSalt = GeneratePassowrdSalt();
                string passwrdHash = GetPasswordHash(password, passwordSalt);
                if (!_repository.Create(new User(login, passwordSalt, passwrdHash)))
                {
                    throw new Exception("Can't create a user. Check out input data");
                }
            }
            else
            {
                throw new Exception($"Registration is failed! Do not leave empty lines");
            }
        }

        public string RefreshToken(string refreshToken)
        {
            IReadOnlyDictionary<int, User> usersDict = _repository.Get();

            foreach (KeyValuePair<int, User> pair in usersDict)
            {
                if (string.CompareOrdinal(pair.Value.RefreshToken, refreshToken) == 0 && AuthResponse.LatestRefreshToken.IsExpired is false)
                {
                    string accessToken = GenerateJwtToken(pair.Key, pair.Value.Login, 1);

                    return accessToken;
                }
                else
                {
                    return null;
                }
            }

            throw new Exception($"Error with DB!");
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
