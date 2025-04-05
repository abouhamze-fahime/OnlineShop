using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ApplicationCommandQuery.Command; 

public class UpdateApplicationCommand:IRequest<bool>
{
    public string Title { get; set; }
}

public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, bool>
{
    public Task<bool> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}



