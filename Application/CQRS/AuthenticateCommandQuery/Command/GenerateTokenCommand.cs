using Application.CQRS.Notifications;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Utility;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AuthenticateCommandQuery.Command; 

public class GenerateTokenCommand:IRequest<GenerateTokenCommandResponse>
{
    
    public string  Token { get; set; }
    public string RefreshToken { get; set; }
}

public class GenerateTokenCommandResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
 
}

public class GenerateTokenCommadnHandler : IRequestHandler<GenerateTokenCommand, GenerateTokenCommandResponse>
{
    private readonly EncryptionUtility encryptionUtility;
    private readonly IMediator mediator;
    private readonly IUserRefreshTokenRepository userRefreshTokenRepository;
    private readonly AppConfigurations appConfigurations;

    public GenerateTokenCommadnHandler(
          EncryptionUtility encryptionUtility ,
          IMediator mediator,
          IOptions<AppConfigurations> options,
          IUserRefreshTokenRepository userRefreshTokenRepository
        )
    {
        this.encryptionUtility = encryptionUtility;
        this.mediator = mediator;
        this.userRefreshTokenRepository = userRefreshTokenRepository;
        this.appConfigurations = options.Value;
    }
    public async Task<GenerateTokenCommandResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        UserRefreshToken userRefreshToken = await userRefreshTokenRepository.GetUserRefreshToken(request.RefreshToken);
        if (userRefreshToken == null) throw new Exception();

        var token = encryptionUtility.GenerateNewToken(userRefreshToken.UserId);
        var refreshToken = encryptionUtility.GenerateRefreshToken();

        // insert or update refresh token 
        // send login sms

        AddRefreshTokenNotification addRefreshTokenNotification = new()
        {
            RefreshToken = refreshToken,
            UserId = userRefreshToken.UserId,
            RefreshTokenTimeOut = appConfigurations.RefreshTokenTimeout,
        };

        await mediator.Publish(addRefreshTokenNotification);


        var result = new GenerateTokenCommandResponse
        {
            Token = token,
            RefreshToken = refreshToken,
        };
        return result;
    }



}
