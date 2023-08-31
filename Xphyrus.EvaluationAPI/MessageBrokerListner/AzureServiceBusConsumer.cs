﻿using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using Xphyrus.EvaluationAPI.Models.Dtos;
using Xphyrus.EvaluationAPI.Service;
using Xphyrus.EvaluationAPI.Service.IService;

namespace Xphyrus.EvaluationAPI.MessageBrokerListner
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string _serviceBusConnectionSring;
        private readonly string _queueName;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor _processor;
        private readonly ResultService _resultService;
        public AzureServiceBusConsumer(IConfiguration configuration, ResultService resultService)
        {
            _configuration = configuration;
            _serviceBusConnectionSring = _configuration.GetValue<string>("ServiceBusConnectionString");
            _queueName = _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions");

            var client = new ServiceBusClient(_serviceBusConnectionSring);

            _processor = client.CreateProcessor(_queueName);
        
            _resultService = resultService;
        }

        public async Task Start()
        {
            _processor.ProcessMessageAsync += OnSubmissionReceived;
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnSubmissionReceived(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            SubmissionRequest objmessage = JsonConvert.DeserializeObject<SubmissionRequest>(body);
            try
            {
                //try to log email
                await _resultService.AddResult(objmessage);
                Console.WriteLine(objmessage);
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
