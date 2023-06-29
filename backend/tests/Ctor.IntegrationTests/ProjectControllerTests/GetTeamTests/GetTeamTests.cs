using System.Net.Http.Json;
using System.Threading.Tasks;
using Ctor.Application.Projects.Queries.GetProjectTeam;
using Ctor.Domain.Entities.Enums;
using Xunit;

namespace Ctor.IntegrationTests.ProjectControllerTests.GetTeamTests;

public class GetTeamTests : ProjectControllerFixture
{
    [Fact]
    public async Task GetsTeamSuccessfully()
    {
        // Arrange
        var projectId = 1;
        var companyId = 1;

        Authorize(2, UserRoles.OperationalManager, companyId);

        // Act
        var response = await Client.GetAsync($"{ProjectControllerApi}/team?projectId={projectId}");

        // Assert
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<GetProjectTeamDto>();

        Assert.NotNull(dto);
        Assert.Equal(1, dto!.Users.Count);
    }
}