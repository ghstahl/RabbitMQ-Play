namespace ProfileDriven.RabbitServices
{
    public class RabbitServerConfig
    {
        public string NameSpace { get; set; }
        public string Host { get; set; }
        public string AltHost { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Port { get; set; }
    }
}