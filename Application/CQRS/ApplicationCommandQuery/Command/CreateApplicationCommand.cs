using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ApplicationCommandQuery.Command; 

public class CreateApplicationCommand :IRequest<int>
{
    public string ApplicationName { get; set; }
}



public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, int>
{
    public Task<int> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}