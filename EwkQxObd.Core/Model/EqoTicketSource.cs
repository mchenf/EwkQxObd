﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    /// <summary>
    /// Represent a ticket source, where this ticket is from
    /// </summary>
    public class EqoTicketSource
    {
        public string TicketNumber { get; set; } = string.Empty;
        public string TicketDescription { get; set; } = string.Empty;
        
    }
}
