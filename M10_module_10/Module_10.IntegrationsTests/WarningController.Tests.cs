using DataAccess;
using DataAccess.DbEntities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using RestApi;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Module_10.IntegrationsTests
{
    class WarningControllerTests
    {
               
        [TestCase("Savelev", "Mikhail")]
        public async Task EmailWarning_SendGetRequest_OK(string testLastName, string testFirstName)
        {
            var expectedResult = "true";
            var webHostReport = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>

                        d.ServiceType == typeof(DbContextOptions<UniversityDbContext>));
                    services.Remove(dbContextDescriptor);
                    services.AddDbContext<UniversityDbContext>(option =>
                    {
                        option.UseInMemoryDatabase("InMemory_Db");
                    });
                    var warningService = services.SingleOrDefault(d => d.ServiceType == typeof(IWarningService));
                    services.Remove(warningService);
                    var mockService = new Mock<IWarningService>();

                    mockService.Setup(_ => _.MarkCourseWarning(testLastName, testFirstName)).Returns(() => Task.Run(() => true));
                    services.AddTransient(_ => mockService.Object);

                });
            });

            HttpClient httpClient = webHostReport.CreateClient();
            HttpContent content = new StringContent("");

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage Responce = await httpClient.PutAsync($"/EmailWarning?lastName={testLastName}&firstName={testFirstName}", content);
            var stream = await Responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            var jsonActual = await streamReader.ReadToEndAsync();

            Assert.AreEqual(expectedResult, jsonActual);
            Assert.AreEqual(HttpStatusCode.OK, Responce.StatusCode);


        }

        [TestCase("Savelev", "Mikhail")]
        public async Task AttendanceWarning_SendGetRequest_OK(string testLastName, string testFirstName)
        {
            var expectedResult = "true";
            var webHostReport = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>

                        d.ServiceType == typeof(DbContextOptions<UniversityDbContext>));
                    services.Remove(dbContextDescriptor);
                    services.AddDbContext<UniversityDbContext>(option =>
                    {
                        option.UseInMemoryDatabase("InMemory_Db");
                    });
                    var warningService = services.SingleOrDefault(d => d.ServiceType == typeof(IWarningService));
                    services.Remove(warningService);
                    var mockService = new Mock<IWarningService>();

                    mockService.Setup(_ => _.AttendanceWarning(testLastName, testFirstName)).Returns(() => Task.Run(() => true));
                    services.AddTransient(_ => mockService.Object);

                });
            });

            HttpClient httpClient = webHostReport.CreateClient();
            HttpContent content = new StringContent("");

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage Responce = await httpClient.PutAsync($"/CourseMarkWarning?lastName={testLastName}&firstName={testFirstName}", content);
            var stream = await Responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            var jsonActual = await streamReader.ReadToEndAsync();

            Assert.AreEqual(expectedResult, jsonActual);
            Assert.AreEqual(HttpStatusCode.OK, Responce.StatusCode);
        }
    }
}
