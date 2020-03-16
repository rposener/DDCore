using DDCore.Abstractions;
using DDCore.Domain;
using Microsoft.EntityFrameworkCore;
using ShopData.ViewTypes;
using ShopDomain.Catalog;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopData
{
    public class ShopContext : DbContext
    {
        private readonly IDomainEventDispatcher dispatcher;

        public ShopContext(DbContextOptions<ShopContext> options, IDomainEventDispatcher dispatcher) 
            : base(options)
        {
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Configuration for the Context
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);
        }

        /// <summary>
        /// Aggregate Roots Go Here 
        /// note: Owned Value Types do not appear here
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Query Only Set backed by a View
        /// </summary>
        public DbSet<ProductResult> ProductSummaries { get; set; }

        public override int SaveChanges()
        {
            DispatchEvents();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            DispatchEvents();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchEventsAsync().ConfigureAwait(false);
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DispatchEventsAsync().ConfigureAwait(false);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);
        }

        #region Domain Event Dispatching

        private void DispatchEvents()
        {
            var domainEventEntities = ChangeTracker.Entries<EntityObject>()
                .Select(po => po.Entity)
                .Where(po => po.Events.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    dispatcher.DispatchAsync(domainEvent)
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }

        private async Task DispatchEventsAsync()
        {
            var domainEventEntities = ChangeTracker.Entries<EntityObject>()
                            .Select(po => po.Entity)
                            .Where(po => po.Events.Any())
                            .ToArray();

            foreach (var entity in domainEventEntities)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await dispatcher.DispatchAsync(domainEvent).ConfigureAwait(false);
                }
            }
        }

        #endregion Domain Event Dispatching
    }
}
