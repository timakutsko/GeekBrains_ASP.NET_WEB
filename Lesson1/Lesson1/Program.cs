using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson1
{
    internal class Program
    {
        private static readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private static readonly string _docPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static async Task Main(string[] args)
        {
            // Очищаю файл от предыдущих записей
            FileStream fs = new FileStream(Path.Combine(_docPath, "Response.txt"), FileMode.Create);
            fs.Dispose();

            // Диапазон записей
            int startId = 4;
            int endId = 14;

            try
            {
                using (_cts)
                {
                    for (int i = startId; i <= endId; i++)
                    {
                        // Получаю ответы по запросу
                        _cts.CancelAfter(2000);
                        string response = MyClient.GetResponse(i, _cts).Result;
                        Response jsonResp = JsonSerializer.Deserialize<Response>(response);

                        await WriteToFile(jsonResp);
                    }
                }
            }
            
            catch (TaskCanceledException)
            {
                Console.WriteLine("Слишком длительный ответ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex}.\nMessage :{ex.Message} ");
            }
        }

        /// <summary>
        /// Метод для записи в файл
        /// </summary>
        /// <returns></returns>
        private static async Task WriteToFile(Response jsonResp)
        {
            // Создаю FileStream с пользовательскими настройками - добавление (запись) данных в файл
            using (FileStream fs = new FileStream(Path.Combine(_docPath, "Response.txt"), FileMode.Append, FileAccess.Write))
            using (StreamWriter outputFile = new StreamWriter(fs))
            {
                await outputFile.WriteLineAsync(jsonResp.userId.ToString());
                await outputFile.WriteLineAsync(jsonResp.id.ToString());
                await outputFile.WriteLineAsync(jsonResp.title);
                await outputFile.WriteLineAsync(jsonResp.body);
            }
        }
    }
}
