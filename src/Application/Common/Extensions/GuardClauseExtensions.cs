using Ardalis.GuardClauses;
using NotFoundException = CaWorkshop.Application.Common.Exceptions.NotFoundException;

namespace CaWorkshop.Application.Common.Extensions;

public static class GuardClauseExtensions
{
    public static void NotFound<T>(this IGuardClause guardClause, T entity, object key)
    {
        if (entity is null)
            throw new NotFoundException(typeof(T).Name, key);
    }
}
