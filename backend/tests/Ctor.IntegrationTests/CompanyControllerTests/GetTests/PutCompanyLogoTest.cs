using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Ctor.Application.Companies.Commands;
using Mailjet.Client.Resources;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Ctor.IntegrationTests.CompanyControllerTests.GetTests;

public class PutCompanyLogoTest : CompanyControllerFixture
{
    private readonly ITestOutputHelper _testOutputHelper;
    private HttpClient _client;
    private Testing _testing;

    public PutCompanyLogoTest(ITestOutputHelper testOutputHelper, Testing testing)
    {
        _testOutputHelper = testOutputHelper;
        _client = testing._client;
    }

    [Fact]
    public async Task Put_CompanyLogo_Check()
    {
        //Arrange
        const int companyId = 1;
        PutCompanyLogoResponseDto expected = new() { Id = 1, Link = "companyLogos/name.png" };

        //Action
        HttpResponseMessage putResponse;
        await using (FileStream file = File.OpenRead("TestPhotos/dummy.png"))
        {
            using (StreamContent content = new(file))
            {
                using (MultipartFormDataContent formData = new())
                {
                    formData.Add(content, "data", "dummy.png");
                    putResponse = await _client.PutAsync($"{_COMPANY_CONTROLLER_API}/{companyId}/logo", formData);
                }
            }
        }

        //Delete from disk
        HttpResponseMessage deleteResponse = await _client.DeleteAsync($"{_COMPANY_CONTROLLER_API}/{companyId}/logo");

        //Assertion
        putResponse.EnsureSuccessStatusCode();
        string responseString = await putResponse.Content.ReadAsStringAsync();

        PutCompanyLogoResponseDto? actual =
            JsonConvert.DeserializeObject<PutCompanyLogoResponseDto>(responseString);

        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(Path.GetExtension(expected.Link), Path.GetExtension(actual.Link));
    }
}