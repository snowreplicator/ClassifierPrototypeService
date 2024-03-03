using System.Threading;
using System.Threading.Tasks;

namespace Prototype.ClassifierPrototypeService.Application.Common;

public interface IApplicationService<in TRequest, TResponse> where TRequest : BaseRequest
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
