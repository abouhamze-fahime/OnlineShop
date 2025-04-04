using Core.Entities;
using Core.IRepositories;
using Infrastructure.Utility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AuthenticateCommandQuery.Command;

public class RegisterUserCommand : IRequest<RegisterUserCommandResponse>
{
   
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
    public DateTime RegisterDate { get; set; }
    
}
public  class RegisterUserCommandResponse
{
    public Guid Id { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
{

    private readonly IUserRepository userRepository;
    private readonly EncryptionUtility encryptionUtility;

    public RegisterUserCommandHandler(IUserRepository userRepository, EncryptionUtility encryptionUtility)
    {
        this.userRepository = userRepository;
        this.encryptionUtility = encryptionUtility;
    }
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var salt = encryptionUtility.GetNewSalt();
        var hashPassword = encryptionUtility.GetSHA256(request.Password, salt);
        User user = new User
        {
            Id= Guid.NewGuid(),
            UserName = request.UserName,
            Password = hashPassword,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
            PasswordSalt = salt,
            RegisterDate = DateTime.UtcNow,
        };

        RegisterUserCommandResponse userId = new()
        {
            Id = await userRepository.AddUserAsync(user)
        };
        return userId;
    }
}
