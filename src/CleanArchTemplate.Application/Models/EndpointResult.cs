using System.Collections.Generic;
using CleanArchTemplate.Application.Models.Enumerations;

namespace CleanArchTemplate.Application.Models
{
    public record EndpointResult
    {
        public EndpointResultStatus Status { get; init; } = EndpointResultStatus.Success;
        public IEnumerable<string> Messages { get; init; } = new List<string>();

        public EndpointResult()
        {
        }

        public EndpointResult(EndpointResultStatus status)
        {
            Status = status;
        }

        public EndpointResult(EndpointResultStatus status, params string[] messages)
        {
            Status = status;
            Messages = messages;
        }
    }

    public record EndpointResult<TResult> : EndpointResult
    {
        public TResult? Data { get; init; }

        public EndpointResult(EndpointResultStatus status)
            : base(status)
        {
        }

        public EndpointResult(EndpointResultStatus status, params string[] messages)
            : base(status, messages)
        {
        }

        public EndpointResult(TResult data)
        {
            Data = data;
        }
    }
}
