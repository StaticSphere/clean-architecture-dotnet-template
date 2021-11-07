using System.Collections.Generic;

namespace CleanArchTemplate.Application.Interfaces
{
    public interface IRequestValidator<TRequest>
    {
        IEnumerable<string> ValidateRequest(TRequest request);
    }
}