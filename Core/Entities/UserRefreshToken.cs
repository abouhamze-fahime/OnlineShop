﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class UserRefreshToken
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public string RefreshToken { get; set; }
    public int RefreshTokenTimeout { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsValid { get; set; }
}
