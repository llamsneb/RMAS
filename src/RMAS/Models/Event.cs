﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace RMAS.Models;

public partial class Event
{
    public string EventName { get; set; }

    public DateOnly EventDate { get; set; }

    public TimeSpan BeginTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public int RoomNumber { get; set; }

    public string UserId { get; set; }

    public virtual Room RoomNumberNavigation { get; set; }

    public virtual AspNetUsers User { get; set; }
}