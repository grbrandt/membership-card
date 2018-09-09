using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Membership.Data.Entities
{
    [Owned]
    public class ClubSettings
    {
        public TimeSpan QRDefaultValidityPeriod { get; set; } = TimeSpan.FromDays(7);

    }
}
