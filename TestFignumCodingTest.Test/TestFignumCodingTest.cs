using FignumCodingTest;
using FignumCodingTest.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace TestFignumCodingTest.Test
{
    [TestClass]
    public class TestFignumCodingTest
    {
        private readonly HttpClient _httpClient;

        public TestFignumCodingTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task When_endpoint_called_with_correct_request_then_it_correctly_responds()
        {
            // When/Act
            var responseObj = await this.SetUp("1,2,3");

            // Assert
            Assert.IsNotNull(responseObj);
            Assert.IsTrue(!responseObj.Errors.InputStringNumber.Any());
            Assert.AreEqual(responseObj.CheckAndSortedList?.Count, 1);
        }

        [TestMethod]
        public async Task When_endpoint_called_with_single_number_followed_by_comma_then_it_correctly_responds()
        {
            // When/Act
            var responseObj = await this.SetUp("1,");

            // Assert
            Assert.IsNotNull(responseObj);
            Assert.IsTrue(!responseObj.Errors.InputStringNumber.Any());
            Assert.AreEqual(responseObj.CheckAndSortedList?.Count, 1);
        }

        [TestMethod]
        public async Task When_endpoint_called_with_negative_numbers_then_it_correctly_responds()
        {
            // When/Act
            var responseObj = await this.SetUp("-1,-5,7,9");

            // Assert
            Assert.IsNotNull(responseObj);
            Assert.IsTrue(!responseObj.Errors.InputStringNumber.Any());
            Assert.AreEqual(responseObj.CheckAndSortedList?.Count, 3);
        }

        [TestMethod]
        public async Task When_endpoint_called_with_invalid_requests_number_comma_space_then_it_correctly_responds()
        {
            // When/Act
            var responseObj = await this.SetUp("1, ");

            // Assert
            Assert.IsNotNull(responseObj);
            Assert.AreEqual("The numbers string provided is not in a correct format 1,  - eg: 1,2,3,4,5", responseObj.Errors.InputStringNumber[0]);
        }

        [TestMethod]
        public async Task When_endpoint_called_with_invalid_requests_alphanumeric_then_it_correctly_responds()
        {
            // When/Act
            var responseObj = await this.SetUp("1,2,3,4,5,as,3,5y");

            // Assert
            Assert.IsNotNull(responseObj);
            Assert.AreEqual("The numbers string provided is not in a correct format 1,2,3,4,5,as,3,5y - eg: 1,2,3,4,5", responseObj.Errors.InputStringNumber[0]);
        }

        [TestMethod]
        public async Task When_endpoint_called_with_invalid_requests_empty_then_it_correctly_responds()
        {
            // When/Act
            var responseObj = await this.SetUp(null);

            // Assert
            Assert.IsNotNull(responseObj);
            Assert.AreEqual(responseObj.Status, 400);
            Assert.AreEqual("The InputStringNumber field is required.", responseObj.Errors.InputStringNumber[0]);
        }

        private async Task<Response?> SetUp(string? inputString)
        {
            var request = new Request
            {
                InputStringNumber = inputString
            };

            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, Constants.JsonMediaType);

            var response = await _httpClient.PostAsync(Constants.Uri, httpContent);
            var stringResult = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<Response>(stringResult);

            return responseObj;
        }
    }
}