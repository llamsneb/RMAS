﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace RMAS.Models;

public partial class AspNetUsers
{
    public string Id { get; set; }

    public int AccessFailedCount { get; set; }

    public string ConcurrencyStamp { get; set; }

    public string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public string NormalizedEmail { get; set; }

    public string NormalizedUserName { get; set; }

    public string PasswordHash { get; set; }

    public string PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string SecurityStamp { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public string UserName { get; set; }

    public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; } = new List<AspNetUserClaims>();

    public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; } = new List<AspNetUserLogins>();

    public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; } = new List<AspNetUserRoles>();

    public virtual ICollection<Event> Event { get; set; } = new List<Event>();
}