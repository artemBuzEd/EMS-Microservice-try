namespace DAL.Specification;

public static class SpecificationQueryBuilder
{
    public static IQueryable<TEntity> BuildQuery<TEntity>(
        IQueryable<TEntity> inputQuery,
        Specification<TEntity> specification
    ) where TEntity : class
    {
        var query = inputQuery;
        
        if(specification.Criteria != null)
            query = query.Where(specification.Criteria);
        
        return query;
    }
}