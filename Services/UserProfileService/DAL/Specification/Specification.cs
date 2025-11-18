using System.Linq.Expressions;

namespace DAL.Specification;

public abstract class Specification<TEntity> where TEntity : class
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    
    public Specification()
    {
        
    }

    public Specification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
}