namespace RosaBot.Shared.Communication.Requests
{
    public class Request
    {
        public string RequestId { get; private set; }
        public DateTime TimeStamp { get; set; }

        public Request()
        {
            RequestId = Guid.NewGuid().ToString();
            TimeStamp = DateTime.Now;
        }
    }
}
