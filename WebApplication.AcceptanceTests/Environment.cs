namespace WebApplication.AcceptanceTests
{
    using System;
    using System.Net.Http;
    using TechTalk.SpecFlow;

    [Binding]
    public static class Environment
    {
        static WebApplication application;

        public static HttpClient GetClient()
        {
            return application.GetClient();
        }

        [BeforeTestRun]
        static void StartServer()
        {
            application = new WebApplication();
            application.Start();
            
            WaitForTheServerToStart();
        }

        [AfterTestRun]
        static void StopServer()
        {
            application?.Dispose();
        }

        static void WaitForTheServerToStart()
        {
            try
            {
                var client = GetClient();
                client.Timeout = TimeSpan.FromSeconds(30);
                var result = client.GetAsync("").Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}