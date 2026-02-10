using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NexusLing.Domain.Common;
using NexusLing.Domain.Interfaces;
using NexusLing.Infrastructure.Database;

namespace NexusLing.Infrastructure.Repositories
{
    /// <summary>
    /// Базовые операциии доступные у объектов в репозиториях
    /// </summary>
    /// <typeparam name="TEntity">DBDataContext или DBDocContext</typeparam>
    /// <typeparam name="TKey">Объект, представляющий тип Id</typeparam>
    public class RepositoryOperations<TEntity, TKey> : IRepositoryOperations<TEntity, TKey> where TEntity : class, new()
    {
        private ApplicationDbContext Context { get; set; }
        private DbSet<TEntity> DbSet;

        public RepositoryOperations(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Полные данные объекта, включая данные других объектов через внешние связи
        /// Может быть переопределён в конкретной реализации объекта данных
        /// По умолчанию соответствует значению PlainData
        /// </summary>
        public virtual IQueryable<TEntity> Data => DbSet;
        /// <summary>
        /// Данные только одной таблицы.
        /// Объекты (поля) связей с другими таблицами не заполнены (null)
        /// </summary>
        public IQueryable<TEntity> PlainData => DbSet;

        /// <summary>
        /// Получить один объект из набора данных
        /// </summary>
        /// <param name="id">Id объекта</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(TKey id)
            => await DbSet.FindAsync(id);

        /// <summary>
        /// Добавить один объект в набор данных
        /// </summary>
        /// <param name="entity">Добавляемый объект</param>
        /// <returns>Объект после добавления в БД</returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            ApplieAuditable();
            EntityEntry<TEntity> value = await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return value.Entity;
        }

        /// <summary>
        /// Добавить коллекцию объектов в набор данных
        /// </summary>
        /// <param name="entities">Добавляемые объекты</param>
        /// <returns>Объекты после добавления в БД</returns>
        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            List<TEntity> entityList = [.. entities];
            if (entityList.Count == 0)
                return [];
            ApplieAuditable();
            await DbSet.AddRangeAsync(entityList);
            await Context.SaveChangesAsync();
            return entityList;
        }

        /// <summary>
        /// Изменить один объект в наборе данных
        /// </summary>
        /// <param name="entity">Изменяемый объект</param>
        public virtual async Task Update(TEntity entity)
        {
            ApplieAuditable();
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Изменить коллекцию объектов в наборе данных
        /// </summary>
        /// <param name="entities">Изменяемые объекты</param>
        public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            List<TEntity> entityList = [.. entities];
            if (entityList.Count == 0)
                return;
            ApplieAuditable();
            DbSet.UpdateRange(entityList);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить один объект из набора данных
        /// </summary>
        /// <param name="id">Id объекта</param>
        public virtual async Task Delete(TKey id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
                return;
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить коллекцию объектов из набора данных
        /// </summary>
        /// <param name="ids">Id объектов</param>
        public virtual async Task DeleteRange(IEnumerable<TKey> ids)
        {
            List<TKey> idList = [.. ids];
            if (idList.Count == 0)
                return;
            List<TEntity> entityList = await DbSet.Where(e => idList.Contains(EF.Property<TKey>(e, "Id")))
                    .ToListAsync();
            if (entityList.Count == 0)
                return;
            DbSet.RemoveRange(entityList);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить один объект из набора данных
        /// </summary>
        /// <param name="entity">Удаляемый объект</param>
        public virtual async Task Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить коллекцию объектов из набора данных
        /// </summary>
        /// <param name="entities">Удаляемые объекты</param>
        public virtual async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            List<TEntity> entityList = [.. entities];
            if (entityList.Count == 0)
                return;
            DbSet.RemoveRange(entityList);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Установка значений аудита
        /// </summary>
        private void ApplieAuditable()
        {
            var entities = Context.ChangeTracker.Entries<BaseAuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                        entity.Entity.DateUpdated = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entity.Entity.DateCreated = DateTime.UtcNow;
                        entity.Entity.DateUpdated = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
