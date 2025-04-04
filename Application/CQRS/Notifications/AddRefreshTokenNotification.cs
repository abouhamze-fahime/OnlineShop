using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Notifications;


public class AddRefreshTokenNotification :INotification
{
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public int RefreshTokenTimeOut { get; set; }
}

public class AddRefreshTokenNotificationHandler : INotificationHandler<AddRefreshTokenNotification>
{
    private readonly IMapper mapper;
    private readonly IUserRefreshTokenRepository userRefreshTokenRepository;

    public AddRefreshTokenNotificationHandler(
        IMapper mapper , 
        IUserRefreshTokenRepository userRefreshTokenRepository 
        
        )
    {
        this.mapper = mapper;
        this.userRefreshTokenRepository = userRefreshTokenRepository;
    }
    public async Task Handle(AddRefreshTokenNotification notification, CancellationToken cancellationToken)
    {
        var userRefreshToken = mapper.Map<UserRefreshToken>(notification);
        
        var userRefreshTokenDb = await userRefreshTokenRepository.GetUserRefreshToken( notification.UserId);
        if (userRefreshTokenDb == null)
        {
          await  userRefreshTokenRepository.InsertAsync(userRefreshToken);
        }
        else
        {
            userRefreshTokenDb.RefreshToken = userRefreshToken.RefreshToken;
            userRefreshTokenDb.RefreshTokenTimeout=userRefreshToken.RefreshTokenTimeout;
            userRefreshTokenDb.CreateDate=DateTime.Now;
            userRefreshTokenDb.IsValid=true;
           await userRefreshTokenRepository.UpdateRefreshTokenAsync();

        }
    }
}
