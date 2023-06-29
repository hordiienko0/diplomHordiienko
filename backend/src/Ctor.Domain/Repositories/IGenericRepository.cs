using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Enums;
using Ctor.Domain.Common;

namespace Ctor.Domain.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> SingleOrDefault(Expression<Func<T, bool>> filter);
    Task<List<T>> Get(Expression<Func<T, bool>> filter);
    Task<List<TResult>> Get<TResult>(Expression<Func<T, bool>> filter);
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<TResult?> FirstOrDefault<TResult>(Expression<Func<TResult, bool>> expression, CancellationToken cancellationToken = default);
    Task<List<T>> GetOrdered(string orderBy, Order order = Order.ASC, Expression<Func<T, bool>> filter = null);
    Task<List<TResult>> GetOrdered<TResult>(string orderBy, Order order = Order.ASC, Expression<Func<T, bool>> filter = null);
    Task<(List<T> entities, int total)> GetFilteredWithTotalSum(Expression<Func<T, bool>> filter,
        int page = 1, int count = 0, string orderBy = null, Order order = Order.ASC);
    Task<(List<TResult> entities, int total)> GetFilteredWithTotalSum<TResult>(Expression<Func<T, bool>> filter,
        int page = 1, int count = 0, string orderBy = null, Order order = Order.ASC);
    Task<(List<T> entities, int total)> GetFilteredWithTotalSumWithQuery(IQueryable<T> query,
            Expression<Func<T, bool>> filter, int page = 0, int count = 0, string orderBy = null, Order order = Order.ASC);
    Task<(List<TResult> entities, int total)> GetFilteredWithTotalSumWithQuery<TResult>(IQueryable<T> query,
        Expression<Func<T, bool>> filter, int page = 0, int count = 0, string orderBy = null, Order order = Order.ASC);
    Task<List<T>> GetAll();
    Task<List<TResult>> GetAll<TResult>();
    Task<T> GetById(long id, CancellationToken ct = default);
    Task<TProjectTo> GetById<TProjectTo>(long id, CancellationToken ct);
    Task<T?> FindById(long id, CancellationToken ct = default);
    Task Insert(T obj);
    void Update(T obj);
    void Delete(T obj);
    Task<bool> DeleteById(long id);
    bool Any();
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    Task AddRangeAsync(IEnumerable<T> values);
    Task AddRangeAsync(params T[] value);
}