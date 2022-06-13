namespace Recap.Core.Entities.Concrete
{
    public class EmailModel : IDto
    {
        public EmailModel()
        {
            CCList = new List<string>();
            BCCList = new List<string>();
            TOList = new List<string>();
        }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string EmailFrom { get; set; }
        public string Password { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public IList<string> CCList { get; set; }
        public IList<string> BCCList { get; set; }
        public IList<string> TOList { get; set; }
    }
}
