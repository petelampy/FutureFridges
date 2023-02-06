namespace FutureFridges.Business.Email
{
    public class EmailData
    {
        public string Body { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public List<string> AttachmentPath { get; set; }
    }
}
