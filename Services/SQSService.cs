using System.Net;
using Amazon.SQS;
using Amazon.SQS.Model;
using AWS.Consumer.Interfaces;
using AWS.Consumer.Models;
using Microsoft.Extensions.Configuration;

namespace AWS.Consumer.Services
{
    public class SQSService : ISQSService
    {
        private SQSCredential creds = new SQSCredential();
        private readonly IConfiguration _configuration;
        private Amazon.Runtime.BasicAWSCredentials AWSCredentials;
        private AmazonSQSClient AWSSQSClient;

        public SQSService(IConfiguration config)
        {
            _configuration = config;
            ConfigureCredentials();
        }

        public async Task<HttpStatusCode> DeleteMessageFromSQS(Message message)
        {
            var request = new DeleteMessageRequest(creds.SQSURL, message.ReceiptHandle);
            var response = await AWSSQSClient.DeleteMessageAsync(request);
            return response.HttpStatusCode;
        }

        public async Task<List<Message>> GetMessageFromSQS()
        {
            try
            {
                var request = new ReceiveMessageRequest(creds.SQSURL);
                var response = await AWSSQSClient.ReceiveMessageAsync(request);
                return response.Messages;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void ConfigureCredentials()
        {
            this.creds = _configuration.GetSection("AWS").Get<SQSCredential>();
            this.AWSCredentials = new Amazon.Runtime.BasicAWSCredentials(creds.AccessKey, creds.SecretAccessKey);
            this.AWSSQSClient = new AmazonSQSClient(AWSCredentials, Amazon.RegionEndpoint.SAEast1);
        }
    }
}