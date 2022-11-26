using Grpc.Net.Client;
using WorkManagerProto;
using static WorkManagerProto.DictionariesClient;

namespace MyClientForGRPC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var chanel = GrpcChannel.ForAddress("https://localhost:5001");
            DictionariesClientClient client = new DictionariesClientClient(chanel);

            Console.Write("Укажи id для поиска: ");
            var userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int id))
            {
                GetByIdResponse clientById = client.GetById(new GetByIdRequest
                {
                    Id = id
                });
                
                Console.WriteLine($"Инфо по запрашиваемому клиенту:" +
                    $"\nId: {clientById.Client.Id} / FirstName: {clientById.Client.FirstName} / LastName: {clientById.Client.LastName}");
            }

            Console.Write("Получить полный список клиентов?");
            ConsoleKeyInfo userBtn = Console.ReadKey(true);
            if (userBtn.Key == ConsoleKey.Enter)
            {
                foreach (var currentClient in client.GetElements(new GetElementsRequest()).Clients)
                {
                    Console.WriteLine($"Id: {currentClient.Id} / FirstName: {currentClient.FirstName} / LastName: {currentClient.LastName}" +
                        $" / Email: {currentClient.Email} / Company: {currentClient.Company}");
                }
            }

            Console.ReadKey(true);
        }
    }
}