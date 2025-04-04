using Application.CQRS.Notifications;
using Core.IRepositories;
using Infrastructure.Models;
using Infrastructure.Utility;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AuthenticateCommandQuery.Command;

public class LoginCommand :IRequest<LoginCommandResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}


public class LoginCommandResponse
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public int ExpireTime { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IUserRepository userRepository;
    private readonly EncryptionUtility encryptionUtility;
    private readonly IMediator mediator;
    private readonly AppConfigurations appConfigurations;

    public LoginCommandHandler(
        IUserRepository userRepository,
        EncryptionUtility encryptionUtility, 
        IMediator mediator,
        IOptions<AppConfigurations> options
        
        )
    {
        this.userRepository = userRepository;
        this.encryptionUtility = encryptionUtility;
        this.mediator = mediator;
        this.appConfigurations =options.Value;
    }
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserAsync(request.UserName);

        var hashPassword = encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
        if (user.Password != hashPassword) throw new Exception();

        var token = encryptionUtility.GenerateNewToken(user.Id);
        var refreshToken = encryptionUtility.GenerateRefreshToken();

        // insert or update refresh token 
        // send login sms

        AddRefreshTokenNotification addRefreshTokenNotification = new()
        {
            RefreshToken = refreshToken,
            UserId = user.Id,
            RefreshTokenTimeOut = appConfigurations.RefreshTokenTimeout,
        };

        await mediator.Publish(addRefreshTokenNotification);

        var response = new LoginCommandResponse { UserName = user.UserName, Token = token , RefreshToken=refreshToken };

        return response;


    }
}
