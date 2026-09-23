using System;

namespace Aurore.Application.Common.Interfaces;

public interface IUseCase<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken ct = default);
}
