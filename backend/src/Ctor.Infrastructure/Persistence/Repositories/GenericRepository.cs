using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ctor.Application.Common.Enums;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Extensions;
using Ctor.Domain.Common;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected ApplicationDbContext _context;
    protected DbSet<T> table;
    private Lazy<IMapper> _mapper;

    public GenericRepository(ApplicationDbContext context, Lazy<IMapper> mapper)
    {
        this._context = context;
        table = _context.Set<T>();
        _mapper = mapper;
    }

    public Task<List<TResult>> Get<TResult>(Expression<Func<T, bool>> filter)
    {
        var query = GetInternal(filter);
        return query.ProjectTo<TResult>(_mapper.Value.ConfigurationProvider).ToListAsync();
    }

    public Task<List<T>> Get(Expression<Func<T, bool>> filter)
    {
        var query = GetInternal(filter);
        return query.ToListAsync();
    }

    private IQueryable<T> GetInternal(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = table;

        return query.Where(filter);
    }

    public Task<List<T>> GetAll()
    {
        return table.ToListAsync();
    }
    public Task<List<TResult>> GetAll<TResult>()
    {
        return table.ProjectTo<TResult>(_mapper.Value.ConfigurationProvider).ToListAsync();
    }
    public async Task<T> GetById(long id, CancellationToken ct)
    {
        return await FirstOrDefault(e => e.Id == id, ct) 
            ?? throw new NotFoundException();
    }

    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        var query = table;

        return await FirstOrDefaultInternal(query, expression, cancellationToken);
    }

    public async Task<TResult?> FirstOrDefault<TResult>(Expression<Func<TResult, bool>> expression, CancellationToken cancellationToken = default)
    {
        var query = table.ProjectTo<TResult>(_mapper.Value.ConfigurationProvider);

        return await FirstOrDefaultInternal<TResult>(query, expression, cancellationToken);
    }

    public Task<TProjectTo> GetById<TProjectTo>(long id, CancellationToken ct)
    {
        return table
            .Where(e => e.Id == id)
            .ProjectTo<TProjectTo>(_mapper.Value.ConfigurationProvider)
            .SingleAsync(cancellationToken: ct);
    }

    public Task<T?> FindById(long id, CancellationToken ct)
    {
        return table.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public Task Insert(T obj)
    {
        return table.AddAsync(obj).AsTask();
    }

    public void Update(T obj)
    {
        table.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(T obj)
    {
        table.Remove(obj);
    }

    public bool Any()
    {
        return table.Any();
    }
    public Task<bool> AnyAsync(Expression<Func<T,bool>> filter)
    {
        return table.AnyAsync(filter);
    }

    public Task AddRangeAsync(IEnumerable<T> values)
    {
        return table.AddRangeAsync(values);
    }

    public Task AddRangeAsync(params T[] value)
    {
        return table.AddRangeAsync(value);
    }

    public async Task<bool> DeleteById(long id)
    {
        var obj = await table.FindAsync(id);

        if (obj == null)
            return false;

        table.Remove(obj);
        return true;
    }


    public Task<List<T>> GetOrdered(string orderBy, Order order = Order.ASC,
        Expression<Func<T, bool>> filter = null)
    {
        var query = GetOrderedInternal(orderBy, order, filter);

        return query.ToListAsync();
    }

    public Task<List<TResult>> GetOrdered<TResult>(string orderBy, Order order = Order.ASC,
    Expression<Func<T, bool>> filter = null)
    {
        var query = GetOrderedInternal(orderBy, order, filter);

        return query.ProjectTo<TResult>(_mapper.Value.ConfigurationProvider).ToListAsync();
    }

    public async Task<(List<T> entities, int total)> GetFilteredWithTotalSum(Expression<Func<T, bool>> filter,
        int page = 0, int count = 0, string orderBy = null, Order order = Order.ASC)
    {
        (var query, var total) = await GetFilteredWithTotalSumInternal(filter, page, count, orderBy, order);

        return (await query.ToListAsync(), total);
    }

    public async Task<(List<TResult> entities, int total)> GetFilteredWithTotalSum<TResult>(Expression<Func<T, bool>> filter,
    int page = 1, int count = 0, string orderBy = null, Order order = Order.ASC)
    {
        (var query, var total) = await GetFilteredWithTotalSumInternal(filter, page, count, orderBy, order);

        return (await query.ProjectTo<TResult>(_mapper.Value.ConfigurationProvider).ToListAsync(), total);
    }

    public async Task<(List<T> entities, int total)> GetFilteredWithTotalSumWithQuery(IQueryable<T> query,
        Expression<Func<T, bool>> filter, int page = 1, int count = 0, string orderBy = null, Order order = Order.ASC)
    {

        (query, var total) = await GetFilteredWithTotalSumWithQueryInternal(query, filter, page, count, orderBy, order);

        return (await query.ToListAsync(), total);
    }

    public async Task<(List<TResult> entities, int total)> GetFilteredWithTotalSumWithQuery<TResult>(IQueryable<T> query,
    Expression<Func<T, bool>> filter, int page = 1, int count = 0, string orderBy = null, Order order = Order.ASC)
    {

        (query, var total) = await GetFilteredWithTotalSumWithQueryInternal(query, filter, page, count, orderBy, order);

        return (await query.ProjectTo<TResult>(_mapper.Value.ConfigurationProvider).ToListAsync(), total);
    }

    private IQueryable<T> GetOrderedInternal(string orderBy, Order order = Order.ASC, Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = table;

        if (filter != null)
            query = query.Where(filter);

        return query.DynamicOrderBy(orderBy, order);
    }

    private async Task<(IQueryable<T> query, int total)> GetFilteredWithTotalSumWithQueryInternal(IQueryable<T> query,
    Expression<Func<T, bool>> filter, int page = 1, int count = 0, string orderBy = null, Order order = Order.ASC)
    {
        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = query.DynamicOrderBy(orderBy, order);

        var total = await query.CountAsync();

        if (count != 0)
            query = query.Skip((page - 1) * count).Take(count);

        return (query, total);
    }

    private async Task<(IQueryable<T> query, int total)> GetFilteredWithTotalSumInternal(Expression<Func<T, bool>> filter,
    int page = 0, int count = 0, string orderBy = null, Order order = Order.ASC)
    {
        IQueryable<T> query = table;

        return await GetFilteredWithTotalSumWithQueryInternal(query, filter, page, count, orderBy, order);
    }

    private async Task<TResult> FirstOrDefaultInternal<TResult>(IQueryable<TResult> query, Expression<Func<TResult, bool>> expression, CancellationToken cancellationToken = default)
    {
        var entity = await query.FirstOrDefaultAsync(expression, cancellationToken);

        if (entity == null)
        {
            var body = expression.Body as BinaryExpression;
            var value = Expression.Lambda(body.Right).Compile().DynamicInvoke();
            throw new NotFoundException(typeof(T).Name, value);
        }

        return entity;
    }

    public Task<T?> SingleOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = table;

        query = query.Where(filter);
        return query.SingleOrDefaultAsync();
    }
}