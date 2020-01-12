using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CCCPMatrixCounterApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();   
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string adress = $"http://{GetLocalIPAddress()}:12345";
            Console.WriteLine(adress);

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(adress).UseStartup<Startup>();
                });
        }
            

        public static string GetLocalIPAddress()
        {
            LaunchSettings settings = JsonConvert.DeserializeObject<LaunchSettings>(File.ReadAllText(@"LaunchSettings.json"));

            if (settings.UseSettingsIp)
            {
                return settings.IpAdress;
            }
            else
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    Console.WriteLine($"ip: {ip.ToString()}");
                    Console.WriteLine($"ip.AdressFamily: {ip.AddressFamily}");
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
        }
    }

    public class LaunchSettings
    {
        public bool UseSettingsIp { get; set; }
        public string IpAdress { get; set; }
    }
}
