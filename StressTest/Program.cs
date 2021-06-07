using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace StressTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter URI to test: ");
            var uriToTest = Console.ReadLine();
            Console.WriteLine("How many requests? (number): ");
            var requestsNumber = Console.ReadLine();
            
            if (uriToTest is null) throw new ArgumentException("Please provide a URI");
            if (requestsNumber is null || !int.TryParse(requestsNumber, out _))
                throw new ArgumentException("Please make sure you enter a valid number for the amount of requests");
            

            using var httpClient = new HttpClient();
            // httpClient.BaseAddress = new Uri(uriToTest);

            var tasks = new List<Task<HttpResponseMessage>>();
            for (int i = 0; i < int.Parse(requestsNumber); i++)
            {
                tasks.Add(httpClient.GetAsync(uriToTest));
            }

            // Run multi process/thread
            var sw = new Stopwatch();
            sw.Start();
            var results = await Task.WhenAll(tasks);
            foreach (var result in results)
            {
                Console.WriteLine($"Status code: {result.StatusCode}.");
            }
            
            sw.Stop();
            Console.WriteLine($"Executed for {sw.ElapsedMilliseconds/1000} seconds");
        }
    }
}