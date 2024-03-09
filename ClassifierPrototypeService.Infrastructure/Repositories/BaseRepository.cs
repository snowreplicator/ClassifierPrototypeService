using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Bll.Common;
using Prototype.ClassifierPrototypeService.Infrastructure.common;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

namespace Prototype.ClassifierPrototypeService.Infrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly RequestContext RequestContext;
    protected ApplicationDbContext DbContext => RequestContext.ApplicationDbContext;

    public BaseRepository(IRequestContext requestContext)
    {
        if (requestContext is RequestContext requestContextRaw)
            RequestContext = requestContextRaw;
        else
            throw new InfrastructureLayerException(
                $"Wrong implementation {typeof(IRequestContext)}",
                Error.S102WrongRequestContext
            );
    }
}
