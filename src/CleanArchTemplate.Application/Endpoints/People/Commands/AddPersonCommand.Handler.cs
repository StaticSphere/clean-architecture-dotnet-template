using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchTemplate.Application.Interfaces;
using CleanArchTemplate.Application.Interfaces.Persistence.DataServices.People.Commands;
using CleanArchTemplate.Application.Models;
using CleanArchTemplate.Application.Models.Enumerations;
using CleanArchTemplate.Domain.Entities;
using MediatR;

namespace CleanArchTemplate.Application.Endpoints.People.Commands
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, EndpointResult<PersonViewModel>>
    {
        public readonly IRequestValidator<AddPersonCommand> _requestValidator;
        public readonly IAddPersonDataService _addPersonDataService;
        public readonly IMapper _mapper;

        public AddPersonCommandHandler(
            IRequestValidator<AddPersonCommand> requestValidator,
            IAddPersonDataService addPersonDataService,
            IMapper mapper
        )
        {
            _requestValidator = requestValidator;
            _addPersonDataService = addPersonDataService;
            _mapper = mapper;
        }

        public async Task<EndpointResult<PersonViewModel>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = _requestValidator.ValidateRequest(request);
            if (validationErrors.Count() > 0)
                return new EndpointResult<PersonViewModel>(EndpointResultStatus.Invalid, validationErrors.ToArray());

            var person = await _addPersonDataService.ExecuteAsync(_mapper.Map<Person>(request));
            return new EndpointResult<PersonViewModel>(_mapper.Map<PersonViewModel>(person));
        }
    }
}
