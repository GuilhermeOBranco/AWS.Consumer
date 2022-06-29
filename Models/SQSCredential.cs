namespace AWS.Consumer.Models
{
    /// <summary>
    /// Quero dar rollback nisso aqui
    /// </summary>
    public class SQSCredential
    {
        public string AccessKey { get; set; }
        public string SecretAccessKey { get; set; }
        public string SQSURL { get; set; }

        public SQSCredential() { }
    }
}