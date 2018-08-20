using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Ms.TeamService.Tests.IntegrationTests
{
    public class TestServerFixture
    {
        private readonly TestServer _server;
        public HttpClient httpClient { get; set; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(GetContentRootPath())
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            _server = new TestServer(builder);
            httpClient = _server.CreateClient();
        }

        private string GetContentRootPath()
        {
            var testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
            var relativePathToHostProject = @"..\..\..\..\..\..\Ms.TeamService";
            return @"C:\Users\arshjots\Desktop\MsUnitTesting\Ms.TeamService";
            //return Path.Combine(testProjectPath, relativePathToHostProject);
        }

        public void Dispose()
        {
            httpClient.Dispose();
            _server.Dispose();
        }

        
    }
}
