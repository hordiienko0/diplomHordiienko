using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Ctor.Application.Auth.Interfaces;
using Ctor.Domain.Entities.Enums;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ctor.IntegrationTests.ProjectControllerTests;

public abstract class ProjectControllerFixture : IClassFixture<TestingWebAppFactory<Program>>, IDisposable
{
    protected const string ProjectControllerApi = "/api/projects";

    protected TestingWebAppFactory<Program> Factory { get; }

    protected HttpClient Client { get; }

    protected ProjectControllerFixture()
    {
        Factory = new TestingWebAppFactory<Program>();
        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }

    protected void Authorize(long userId, UserRoles role, long companyId)
    {
        var tokenProvider = Factory.Services.GetRequiredService<ITokenProvider>();
        var token = tokenProvider.GenerateAccessToken(2, UserRoles.OperationalManager, companyId);

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
    }
}