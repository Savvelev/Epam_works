using DataAccess;
using DataAccess.DbEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RestApi;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Module_10.IntegrationsTests
{
    [TestFixture]
    class AttendanceControllerTests
    {
        readonly WebApplicationFactory<Startup> webHost;

        AttendanceDb[] AttendanceTest = new AttendanceDb[]
        {
             new AttendanceDb
             {
                Id = 1,
                Student = null,
                IsStudentAttended = true,
                Homework = null,
                Lecture = null
             },
             new AttendanceDb
             {
                Id = 2,
                Student = null,
                IsStudentAttended = false,
                Homework = null,
                Lecture = null
             },
        };

        public AttendanceControllerTests()
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
            dbContext.AddRange(AttendanceTest);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task GetAllAttendances_SendGetRequest_OK()
        {
            string jsonActual;
            List<AttendanceDb> attendancesFromResponce;
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
            dbContext.AddRange(AttendanceTest);
            dbContext.SaveChanges();

            HttpClient httpClient = webHostStable.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync("/api/attendance");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            attendancesFromResponce = JsonSerializer.Deserialize<List<AttendanceDb>>(jsonActual, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = false
            }); ;

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(attendancesFromResponce[0].Id, AttendanceTest[0].Id);
            Assert.AreEqual(attendancesFromResponce[0].IsStudentAttended, AttendanceTest[0].IsStudentAttended);
            Assert.AreEqual(attendancesFromResponce[0].Lecture, AttendanceTest[0].Lecture);
            Assert.AreEqual(attendancesFromResponce[0].Student, AttendanceTest[0].Student);
            Assert.AreEqual(attendancesFromResponce[0].Homework, AttendanceTest[0].Homework);
        }

        [TestCase(1)]
        public async Task GetAttendance_SendGetRequest_OK(int testcase)
        {
            string jsonActual;
            string jsonExpected;

            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/attendance/id?id={testcase}");

            var stream = await responce.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();
            jsonExpected = JsonSerializer.Serialize(AttendanceTest[0]);

            Assert.AreEqual(HttpStatusCode.OK, responce.StatusCode);
            Assert.AreEqual(jsonActual.ToLower(), jsonExpected.ToLower());
        }

        [TestCase(200)]
        public async Task GetAttendance_SendBadRequest_NotFound(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();
            HttpResponseMessage responce = await httpClient.GetAsync($"/api/attendance/id?id={testcase}");
            Assert.AreEqual(HttpStatusCode.NotFound, responce.StatusCode);
        }

        [Test]
        public async Task AddAttendance_SendPostRequest_OK()
        {
            string jsonActual;
            string jsonExpected = "true";
            var attendanceTest = new AttendanceDb { Id = 3, Student = null, IsStudentAttended = true, Homework = null, Lecture = null };
            var jsonAttendanceTest = JsonSerializer.Serialize(attendanceTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonAttendanceTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PostAsync("/api/attendance", content);
            var stream = await responce.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(stream);
            jsonActual = await streamReader.ReadToEndAsync();

            Assert.True(responce.IsSuccessStatusCode);
            Assert.AreEqual(jsonActual, jsonExpected);
        }

        [TestCase(1)]
        public async Task UpdateAttendance_SendPutRequest_OK(int id)
        {
            var attendanceTest = new AttendanceDb { Student = null, IsStudentAttended = false, Homework = null, Lecture = null };
            var jsonAttendanceTest = JsonSerializer.Serialize(attendanceTest);

            HttpClient httpClient = webHost.CreateClient();

            HttpContent content = new StringContent(jsonAttendanceTest);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage responce = await httpClient.PutAsync($"/api/attendance/id?id={id}", content);

            Assert.True(responce.IsSuccessStatusCode);
        }

        [TestCase(2)]
        public async Task DeleteAttendance_SendDeleteRequest_OK(int testcase)
        {
            HttpClient httpClient = webHost.CreateClient();

            HttpResponseMessage deleteResponce = await httpClient.DeleteAsync($"/api/attendance?id={testcase}");
            HttpResponseMessage getResponce = await httpClient.GetAsync($"/api/attendance/id?id={testcase}");

            Assert.True(deleteResponce.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, getResponce.StatusCode);
        }
    }
}
