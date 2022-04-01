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
using System.Text.Json;
using System.Threading.Tasks;

namespace Module_10.IntegrationsTests
{
    [TestFixture]
    class LectureControllerTests
    {
        readonly WebApplicationFactory<Startup> webHost;

        LectureDb[] LectureTest = new LectureDb[]
        {
                    new LectureDb {Id=1 , Name = "Math"},
                    new LectureDb {Id=2 , Name = "English"},
                    new LectureDb {Id=3, Name = "PE" },
        };

        public LectureControllerTests()
        {
            webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
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
                });
            });

            UniversityDbContext dbContext = webHost
                .Services.CreateScope()
                .ServiceProvider
                .GetService<UniversityDbContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.AddRange(LectureTest);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllLecture_SendGetRequest_OK()
        {
            string jsonActual;
            string jsonExpected;

            var webHostStable = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
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
                });
            });

            UniversityDbContext dbContext = webHostStable
                .Services.CreateScope()
                .ServiceProvider
                .GetService<UniversityDbContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.AddRange(LectureTest);
            dbContext.SaveChanges();


            HttpClient httpClient = webHostStable.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync("/api/lecture");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(LectureTest);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(1)]
        public async Task GetLecture_SendGetRequest_OK(int testcase)
        {
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/lecture/id?id={testcase}");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(LectureTest[0]);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(201)]
        public async Task GetLecture_SendBadRequest_NotFound(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/lecture/id?id={testcase}");
            Assert.AreEqual(HttpStatusCode.NotFound, responce.StatusCode);
        }

        [Test]
        public async Task AddLecture_SendPostRequest_OK()
        {
            string jsonActual;
            string jsonExpected = "true";
            var lectureTest = new LectureDb
            { Id = 4, Name = "Geography" };
            var jsonLectureTest = JsonSerializer.Serialize(lectureTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonLectureTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PostAsync("/api/lecture", content);
            var stream = await responce.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();

            Assert.True(responce.IsSuccessStatusCode);
            Assert.AreEqual(jsonActual, jsonExpected);
        }

        [Test]
        public async Task UpdateLecture_SendPutRequest_OK()
        {
            var lectureTest = new LectureDb
            { Name = "Matematika" };
            var jsonLectureTest = JsonSerializer.Serialize(lectureTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonLectureTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PutAsync("/api/lecture/id?id=1", content);

            Assert.True(responce.IsSuccessStatusCode);
        }

        [TestCase(2)]
        public async Task DeleteLecture_SendDeleteRequest_OK(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();

            HttpResponseMessage deleteResponce = await httpClient.DeleteAsync($"/api/lecture?id={testcase}");
            HttpResponseMessage getResponce = await httpClient.GetAsync($"/api/lecture/id?id={testcase}");

            Assert.True(deleteResponce.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, getResponce.StatusCode);
        }
        [TestCase("Math", "Json")]
        public async Task ReportAttendance_GetReport_OK(string testLecture, string format)
        {
            var moqReturn = "testData";
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
                    var lectureService = services.SingleOrDefault(d => d.ServiceType == typeof(ILectureService));
                    services.Remove(lectureService);
                    var mockService = new Mock<ILectureService>();

                    mockService.Setup(_ => _.Report(testLecture, Domain.FormatSourse.Json)).Returns(() => Task.Run(() => moqReturn));
                    services.AddTransient(_ => mockService.Object);
                });
            });
            HttpClient httpClient = webHostReport.CreateClient();

            HttpResponseMessage getResponce = await httpClient.GetAsync($"/api/lecture/Report?Name={testLecture}&format={format}");
            var stream = await getResponce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            var jsonActual = await streamReader.ReadToEndAsync();

            Assert.AreEqual(moqReturn, jsonActual);
            Assert.AreEqual(HttpStatusCode.OK, getResponce.StatusCode);
        }
    }
}
