using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.MyEntity.Queries;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Ctor.IntegrationTests.MyEntityControllerTests.GetTests;
public class GetTest : MyEntityControllerFixture
{
    private readonly HttpClient _client;
    public GetTest(TestingWebAppFactory<Program> factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task Index_WhenCalled_ReturnsApplicationForm()
    {
        //Arrange
        var id = 5;
        var expected= new MyEntityDto
        {
            Name = "Outer text",
            Detail = new MyEntityDetailDto
            {
                Detail = "Inner Text"
            }
        };

        //Action
        var response = await _client.GetAsync($"{_MY_ENTITY_CONTROLLER_API}/{id}");

        
        response.EnsureSuccessStatusCode();
        var responceString = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<MyEntityDto>(responceString);

        expected.Should().BeEquivalentTo(actual);

    }
}
