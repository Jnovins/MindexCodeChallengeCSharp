
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

		[TestMethod]
		public void CreateCompensation_Returns_Created()
			{
			Compensation compensation = new Compensation();

			// Arrange
			Employee employee = new Employee()
				{
				EmployeeId = Guid.NewGuid().ToString(),
				Department = "Complaints",
				FirstName = "Debbie",
				LastName = "Downer",
				Position = "Receiver",
				};
			float salary = 112000;
			DateTime effectiveDate = DateTime.Now;

			compensation.Employee = employee;
			compensation.Salary = salary;
			compensation.EffectiveDate = effectiveDate;

			var requestContent = new JsonSerialization().ToJson(compensation);

			// Execute
			var postRequestTask = _httpClient.PostAsync("api/compensation",
			   new StringContent(requestContent, Encoding.UTF8, "application/json"));
			var response = postRequestTask.Result;

			// Assert
			Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

			var newCompensation = response.DeserializeContent<Compensation>();
			Assert.IsNotNull(newCompensation.CompensationId);
			Assert.AreEqual(employee.FirstName, newCompensation.Employee.FirstName);
			Assert.AreEqual(employee.LastName, newCompensation.Employee.LastName);
			Assert.AreEqual(salary, newCompensation.Salary);
			Assert.AreEqual(effectiveDate, newCompensation.EffectiveDate);
			}
	}
}
