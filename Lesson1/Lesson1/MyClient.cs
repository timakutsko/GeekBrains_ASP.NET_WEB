using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson1
{
    /// <summary>
    /// Класс для осуществления запроса
    /// </summary>
    static class MyClient
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<string> GetResponse(int id, CancellationTokenSource cts)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}", cts.Token);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (HttpRequestException ex)
            {
                return ($"Ошибка: {ex}.\nMessage :{ex.Message} ");
            }
        }
    }
}
