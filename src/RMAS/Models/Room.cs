﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace RMAS.Models;

public partial class Room
{
    public int RoomNumber { get; set; }

    public string RoomType { get; set; }

    public virtual ICollection<Event> Event { get; set; } = new List<Event>();
}