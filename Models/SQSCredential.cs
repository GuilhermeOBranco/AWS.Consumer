namespace AWS.Consumer.Models
{
    public class SQSCredential
    {
        public string AccessKey { get; set; }
        public string SecretAccessKey { get; set; }
        public string SQSURL { get; set; }

        public SQSCredential() { }
    }
}