using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.API.Services;
using WeatherApp.Tests.ServicesTests.SampleModels;
using Xunit;

namespace WeatherApp.Tests.ServicesTests
{
    public class ClientServiceTests
    {
        public async Task Client_Service_Should_Return_APIResponse_With_Specified_Return_Type()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;

            IClientService clientService = new ClientService(factory);

            var response = await clientService.GetAsync<PostModel>(SampleRequestsURls.Posts);
            var expected = new PostModel 
            {
                id = 1,
                title = "hello"
            };

            Assert.IsType<PostModel>(response);
            Assert.Equal(expected.title, response.title);
        }

        [Fact]
        public async Task Client_Service_Should_Throw_InvalidOperationException()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;

            IClientService clientService = new ClientService(factory);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => 
            {
                await clientService.GetAsync<PostModel>(SampleRequestsURls.TextResult);
            }); 
        }
    }
}
