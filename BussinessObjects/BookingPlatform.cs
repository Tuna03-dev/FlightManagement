﻿using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class BookingPlatform
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
