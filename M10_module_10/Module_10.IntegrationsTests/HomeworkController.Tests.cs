using DataAccess;
using DataAccess.DbEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    class HomeworkControllerTests
    {
        readonly WebApplicationFactory<Startup> webHost;

        HomeworkDb[] HomeworkTest = new HomeworkDb[]
        {
            new HomeworkDb {Id=1 , Name = "Math_Homework", CourseMark = 2},
            new HomeworkDb {Id=2 , Name = "English_Homework", CourseMark =5},
            new HomeworkDb {Id=3 , Name = "PE_Homework", CourseMark =4},
        };

        public HomeworkControllerTests()
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
            dbContext.AddRange(HomeworkTest);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllHomeworks_SendGetRequest_OK()
        {
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
            dbContext.AddRange(HomeworkTest);
            dbContext.SaveChanges();
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHostStable.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync("/api/homework");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(HomeworkTest);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(1)]
        public async Task GetHomework_SendGetRequest_OK(int testcase)
        {
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/homework/id?id={testcase}");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(HomeworkTest[0]);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(200)]
        public async Task GetHomework_SendBadRequest_NotFound(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/homework/id?id={testcase}");
            Assert.AreEqual(HttpStatusCode.NotFound, responce.StatusCode);
        }

        [Test]
        public async Task AddHomework_SendPostRequest_OK()
        {
            string jsonActual;
            string jsonExpected = "true";
            var homeworkTest = new HomeworkDb { Id = 4, Name = "Geography_Homework", CourseMark = 2 };
            var jsonHomeworkTest = JsonSerializer.Serialize(homeworkTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonHomeworkTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PostAsync("/api/homework", content);
            var stream = await responce.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();

            Assert.True(responce.IsSuccessStatusCode);
            Assert.AreEqual(jsonActual, jsonExpected);
        }

        [TestCase(1)]
        public async Task UpdateHomework_SendPutRequest_OK(int id)
        {
            var homeworkTest = new HomeworkDb { Name = "English_Homework", CourseMark = 5 };
            var jsonHomeworkTest = JsonSerializer.Serialize(homeworkTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonHomeworkTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PutAsync($"/api/homework/id?id={id}", content);

            Assert.True(responce.IsSuccessStatusCode);
        }

        [TestCase(2)]
        public async Task DeleteHomework_SendDeleteRequest_OK(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();

            HttpResponseMessage deleteResponce = await httpClient.DeleteAsync($"/api/homework?id={testcase}");
            HttpResponseMessage getResponce = await httpClient.GetAsync($"/api/homework/id?id={testcase}");

            Assert.True(deleteResponce.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, getResponce.StatusCode);
        }
    }
}
