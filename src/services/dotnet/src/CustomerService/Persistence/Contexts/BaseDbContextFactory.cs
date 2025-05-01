using Core.Persistence.Contexts.SqlServer;

namespace Persistence.Contexts;

public class BaseDbContextFactory : DbContextDesignFactoryBase<BaseDbContext>
{
    public BaseDbContextFactory() : base("BaseDb")
    {
    }
}
