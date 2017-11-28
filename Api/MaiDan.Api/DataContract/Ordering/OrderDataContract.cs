using System.Collections.Generic;

namespace MaiDan.Api.DataContract.Ordering
{
    public class OrderDataContract 
    {
        public string Id { get; set; }

        public string TableId { get; set; }

        public int NumberOfGuests { get; set; }
        
        public IList<LineDataContract> Lines { get; set; }
        
    }
}
