using System.Net;
using Amazon.SQS.Model;

namespace AWS.Consumer.Interfaces
{
    public interface ISQSService
    {
        Task<HttpStatusCode> DeleteMessageFromSQS(Message message);
        Task<List<Message>> GetMessageFromSQS();
    }
}