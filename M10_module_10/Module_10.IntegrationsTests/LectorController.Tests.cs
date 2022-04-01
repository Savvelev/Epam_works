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
    class LectorControllerTests
    {
        readonly WebApplicationFactory<Startup> webHost;

        LectorDb[] LectorsTest = new LectorDb[]
        {
            new LectorDb {Id=1 ,FirstName = "Vladimir", LastName="Lusev", Email="lysev.v.i@yandex.ru"},
            new LectorDb {Id=2 , FirstName = "Alexandr", LastName="Starkov",  Email="starkov.a.s@yandex.ru"},
            new LectorDb {Id=3 , FirstName = "Alexey", LastName="Timofeevskiy", Email="timAlex@yandex.ru"},
        };

        public LectorControllerTests()
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
            dbContext.AddRange(LectorsTest);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllLectors_SendGetRequest_OK()
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
            dbContext.AddRange(LectorsTest);
            dbContext.SaveChanges();
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHostStable.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync("/api/lector");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(LectorsTest);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(1)]
        public async Task GetLector_SendGetRequest_OK(int testcase)
        {
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/lector/id?id={testcase}");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(LectorsTest[0]);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(200)]
        public async Task GetLector_SendBadRequest_NotFound(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/lector/id?id={testcase}");
            Assert.AreEqual(HttpStatusCode.NotFound, responce.StatusCode);
        }

        [Test]
        public async Task AddLector_SendPostRequest_OK()
        {
            string jsonActual;
            string jsonExpected = "true";
            var lectorTest = new LectorDb
            { Id = 4, FirstName = "Grisha", LastName = "Cembrovskiy", Email = "gresha29@yandex.ru" };
            var jsonLectorTest = JsonSerializer.Serialize(lectorTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonLectorTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PostAsync("/api/lector", content);
            var stream = await responce.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();

            Assert.True(responce.IsSuccessStatusCode);
            Assert.AreEqual(jsonActual, jsonExpected);
        }

        [Test]
        public async Task UpdateLector_SendPutRequest_OK()
        {
            var lectorTest = new LectorDb
            { FirstName = "Alex", LastName = "Puskin", Email = "poet777@yandex.ru" };
            var jsonLectorTest = JsonSerializer.Serialize(lectorTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonLectorTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PutAsync("/api/lector/id?id=1", content);

            Assert.True(responce.IsSuccessStatusCode);
        }

        [TestCase(2)]
        public async Task DeleteLector_SendDeleteRequest_OK(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();

            HttpResponseMessage deleteResponce = await httpClient.DeleteAsync($"/api/lector?id={testcase}");
            HttpResponseMessage getResponce = await httpClient.GetAsync($"/api/lector/id?id={testcase}");

            Assert.True(deleteResponce.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, getResponce.StatusCode);
        }
    }
}
