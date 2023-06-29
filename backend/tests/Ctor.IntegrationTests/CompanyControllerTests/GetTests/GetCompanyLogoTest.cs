using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Ctor.Application.Companies.Queries.GetCompanyLogoByCompanyId;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Ctor.IntegrationTests.CompanyControllerTests.GetTests;

public class GetCompanyLogoTest : CompanyControllerFixture
{
    private readonly ITestOutputHelper _testOutputHelper;
    private HttpClient _client;
    private Testing _testing;

    public GetCompanyLogoTest(ITestOutputHelper testOutputHelper, Testing testing)
    {
        _testOutputHelper = testOutputHelper;
        _testing = testing;
        _client = testing._client;
    }

    [Fact]
    public async Task Get_CompanyLogo_Check()
    {
        //Arrange

        const int companyId = 1;
        GetCompanyLogoByCompanyIdResponseDto expected = new() { Id = 1, Link = "companyLogos/name.png" };

        _testOutputHelper.WriteLine("1");

        //Action
        //Put photo for get
        await using (FileStream file = File.OpenRead("TestPhotos/dummy.png"))
        {
            using (StreamContent content = new(file))
            {
                using (MultipartFormDataContent formData = new())
                {
                    formData.Add(content, "data", "dummy.png");
                    await _client.PutAsync($"{_COMPANY_CONTROLLER_API}/{companyId}/logo", formData);
                }
            }
        }

        //Test
        HttpResponseMessage getResponse = await _client.GetAsync($"{_COMPANY_CONTROLLER_API}/{companyId}/logo");

        //Delete from disk
        HttpResponseMessage deleteResponse = await _client.DeleteAsync($"{_COMPANY_CONTROLLER_API}/{companyId}/logo");

        //Assertion
        getResponse.EnsureSuccessStatusCode();
        string responseString = await getResponse.Content.ReadAsStringAsync();
        GetCompanyLogoByCompanyIdResponseDto? actual =
            JsonConvert.DeserializeObject<GetCompanyLogoByCompanyIdResponseDto>(responseString);

        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(Path.GetExtension(expected.Link), Path.GetExtension(actual.Link));
    }
}