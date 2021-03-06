﻿using System;
using System.Collections.Generic;

namespace MaiDan.Api.DataContracts.Requests
{
    public class Order 
    {
        public int Id { get; set; }

        public bool IsTakeAway { get; set; }

        public string TableId { get; set; }

        public int NumberOfGuests { get; set; }

        public DateTime OrderingDate { get; set; }
        
        public List<Line> Lines { get; set; }
    }
}
