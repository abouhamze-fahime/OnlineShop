using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ApplicationCommandQuery.Command; 

public class DeleteApplicationCommand :IRequest<bool>
{
    public int Id { get; set; }
}


public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, bool>
{
    public Task<bool> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}



