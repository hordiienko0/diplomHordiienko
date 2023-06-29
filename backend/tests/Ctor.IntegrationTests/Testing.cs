using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Ctor.Application.Auth.Interfaces;
using Ctor.Domain.Entities.Enums;
using Ctor.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ctor.IntegrationTests;

public partial class Testing : IDisposable
{
    private WebApplicationFactory<Program> _factory = null!;
    private IServiceScopeFactory _scopeFactory = null!;
    public HttpClient _client;
    private long? _currentUserId;

    public Testing()
    {
        _factory = new TestingWebAppFactory<Program>();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        _client = _factory.CreateClient();
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public long? GetCurrentUserId()
    {
        return _currentUserId;
    }

    public async Task<HttpClient> RunAsOperationalManagerAsync()
    {
        var tokenProvider = _factory.Services.GetRequiredService<ITokenProvider>();
        var token = tokenProvider.GenerateAccessToken(2, UserRoles.OperationalManager, 1);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        return _client;
    }

    public async Task<HttpClient> RunAsAdministratorAsync()
    {
        var tokenProvider = _factory.Services.GetRequiredService<ITokenProvider>();
        var token = tokenProvider.GenerateAccessToken(1, UserRoles.Admin, null);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

        return _client;
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public async Task AddRangeAsync<TEntity>(params TEntity[] entities)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddRangeAsync(entities as object[]);
        await context.SaveChangesAsync();
    }

    public async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    public void Dispose()
    {
        // tear down
    }
}