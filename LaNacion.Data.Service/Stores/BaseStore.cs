using LaNacion.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaNacion.Data.Service.Stores
{
    public class BaseStore<T> where T : class, IDbEntity
    {
        internal readonly LaNacionContext context;
        internal readonly DbSet<T> table;

        internal BaseStore(
            LaNacionContext context,
            Func<LaNacionContext, DbSet<T>> tableExpression)
        {
            this.context = context;
            this.table = tableExpression(context);
        }

        internal IQueryable<T> TableQueryable { get; set; }

        internal async Task<T> SaveEntity(T item)
            => (await this.SaveEntities(item)).FirstOrDefault();

        internal Task<IEnumerable<T>> SaveEntities(params T[] items)
            => this.SaveEntities(items.AsEnumerable());

        internal async Task<IEnumerable<T>> SaveEntities(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (this.context.Entry<T>(item).State == EntityState.Detached)
                {
                    this.table.Add(item);
                }
            }

            await this.SaveChangesAsync();
            return items;
        }

        internal async Task<T> RemoveEntity(T item)
            => (await this.RemoveEntities(new[] { item })).FirstOrDefault();

        internal async Task<IEnumerable<T>> RemoveEntities(IEnumerable<T> items)
        {
            var itemsRemoved = new List<T>();
            foreach (var item in items)
            {
                itemsRemoved.Add(this.table.Remove(item).Entity);
            }

            await this.SaveChangesAsync();
            return itemsRemoved;
        }

        internal Task<IQueryable<T>> FilterEntities(Expression<Func<T, bool>> filter = null)
        {
            var queryable = GetQueryable();
            var result = filter != null ? queryable.Where(filter) : queryable;
            return Task.FromResult(result);
        }

        internal async Task<T> GetEntity(Expression<Func<T, bool>> filter = null)
        {
            var queryable = GetQueryable();
            var result = filter != null ? queryable.Where(filter) : queryable;
            return await result.FirstOrDefaultAsync();
        }

        internal IQueryable<T> GetQueryable()
        {
            return TableQueryable ?? table;
        }

        internal async Task SaveChangesAsync()
        {
            if (this.context.ChangeTracker.HasChanges())
            {
                await this.context.SaveChangesAsync();
            }
        }
    }
}