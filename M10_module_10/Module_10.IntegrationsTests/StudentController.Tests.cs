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
    class StudentControllerTests
    {
        readonly WebApplicationFactory<Startup> webHost;

        StudentDb[] StudentTest = new StudentDb[]
        {
                new StudentDb {Id=1 , FirstName = "Mikhail", LastName= "Savelev", Email = "Savelev@yandex.ru", Phone ="79800553535"},
                new StudentDb {Id=2 , FirstName = "Geka", LastName= "Plechev", Email = "PlecevGekaChill@yandex.ru", Phone ="79800553535"},
                new StudentDb {Id=3 , FirstName = "Ilya", LastName= "Kugov", Email = "VerniSotku@yandex.ru",Phone ="79800553535"},
        };

        public StudentControllerTests()
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
            dbContext.AddRange(StudentTest);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllStudent_SendGetRequest_OK()
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
            dbContext.AddRange(StudentTest);
            dbContext.SaveChanges();
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHostStable.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync("/api/student");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(StudentTest);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(1)]
        public async Task GetStudent_SendGetRequest_OK(int testcase)
        {
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/student/id?id={testcase}");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(StudentTest[0]);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(202)]
        public async Task GetStudent_SendBadRequest_NotFound(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/student/id?id={testcase}");
            Assert.AreEqual(HttpStatusCode.NotFound, responce.StatusCode);
        }

        [Test]
        public async Task AddStudent_SendPostRequest_OK()
        {
            string jsonActual;
            string jsonExpected = "true";
            var studentTest = new StudentDb
            { Id = 4, FirstName = "Stepan", LastName = "Royter", Email = "StepanJudoist@yandex.ru", Phone = "79800553535" };
            var jsonStudentTest = JsonSerializer.Serialize(studentTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonStudentTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PostAsync("/api/student", content);
            var stream = await responce.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();

            Assert.True(responce.IsSuccessStatusCode);
            Assert.AreEqual(jsonActual, jsonExpected);
        }

        [TestCase(1)]
        public async Task UpdateStudent_SendPutRequest_OK(int id)
        {
            var studentTest = new StudentDb
            { FirstName = "Vanya", LastName = "Pimax", Email = "superivan@yandex.ru", Phone = "79800553535" };
            var jsonStudentTest = JsonSerializer.Serialize(studentTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonStudentTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PutAsync($"/api/student/id?id={id}", content);

            Assert.True(responce.IsSuccessStatusCode);
        }

        [TestCase(2)]
        public async Task DeleteStudent_SendDeleteRequest_OK(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();

            HttpResponseMessage deleteResponce = await httpClient.DeleteAsync($"/api/student?id={testcase}");
            HttpResponseMessage getResponce = await httpClient.GetAsync($"/api/student/id?id={testcase}");

            Assert.True(deleteResponce.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, getResponce.StatusCode);
        }

        [TestCase(1, 2)]
        public async Task AddStudentToAttendance_SendThisRequest_OK(int testId1, int testId2)
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
                    var studentService = services.SingleOrDefault(d => d.ServiceType == typeof(IStudentService));
                    services.Remove(studentService);
                    var mockService = new Mock<IStudentService>();

                    mockService.Setup(_ => _.AddStudentToAttendance(It.IsAny<int>(), It.IsAny<int>())).Returns(() => Task.Run(() => true));
                    services.AddTransient(_ => mockService.Object);

                });
            });

            HttpClient httpClient = webHostReport.CreateClient();
            HttpContent content = new StringContent("");

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage Responce = await httpClient.PutAsync($"/addStudentToLecture?StudentId={testId1}&AttendanceId={testId2}", content);
            var stream = await Responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            var jsonActual = await streamReader.ReadToEndAsync();

            Assert.AreEqual(expectedResult, jsonActual);
            Assert.AreEqual(HttpStatusCode.OK, Responce.StatusCode);
        }

        [TestCase("Mikhail", "Savelev", "Xml")]
        public async Task ReportAttendance_GetReport_OK(string firstName, string lastName, string format)
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
                    var studentService = services.SingleOrDefault(d => d.ServiceType == typeof(IStudentService));
                    services.Remove(studentService);
                    var mockService = new Mock<IStudentService>();

                    mockService.Setup(_ => _.Report(firstName, lastName, Domain.FormatSourse.Xml)).Returns(() => Task.Run(() => moqReturn));
                    services.AddTransient(_ => mockService.Object);
                });
            });
            HttpClient httpClient = webHostReport.CreateClient();

            HttpResponseMessage getResponce = await httpClient.GetAsync($"api/student/Report?FirstName={firstName}&LastName={lastName}&format={Domain.FormatSourse.Xml}");


            var stream = await getResponce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            var jsonActual = await streamReader.ReadToEndAsync();

            Assert.AreEqual(moqReturn, jsonActual);
            Assert.AreEqual(HttpStatusCode.OK, getResponce.StatusCode);
        }
    }
}
