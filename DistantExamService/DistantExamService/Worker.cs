using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DistantExamService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        internal HttpClient _client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string txtold = string.Empty;

            string txtnew = string.Empty;
            _client.BaseAddress = new Uri("http://localhost:51342");
            while (!stoppingToken.IsCancellationRequested)
            {
                txtnew = TextCopy.ClipboardService.GetText();

                if (txtnew.Equals(txtold) || string.IsNullOrEmpty(txtnew))
                {
                    Console.WriteLine("null");
                }
                else
                {
                    ConsoleDTO req = new ConsoleDTO();

                    req.QuestionKey = txtnew;
             
                    _client.PostAsJsonAsync("/sendanswer", req);

                    Console.WriteLine(txtnew);

                    txtold = txtnew;
                }
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
