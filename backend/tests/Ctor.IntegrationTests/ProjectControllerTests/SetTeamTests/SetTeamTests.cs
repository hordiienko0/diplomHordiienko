using System.Collections.Generic;
using System.Threading.Tasks;
using Ctor.Application.Projects.Commands.SetProjectTeam;
using Ctor.Domain.Entities.Enums;
using Xunit;

namespace Ctor.IntegrationTests.ProjectControllerTests.SetTeamTests;

public class SetTeamTests : ProjectControllerFixture
{
    [Fact]
    public async Task SetsTeamSuccessfully()
    {
        // Arrange
        var projectId = 1;
        var companyId = 1;
        var command = new SetProjectTeamCommand(projectId, new HashSet<long> { 3, 4 });

        Authorize(2, UserRoles.OperationalManager, companyId);

        // Act
        var response = await Client.PostAsync($"{ProjectControllerApi}/team", command.ToJsonContent());

        // Assert
        response.EnsureSuccessStatusCode();
    }
}