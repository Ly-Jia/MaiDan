using System.Collections.Generic;

namespace MaiDan.Api.DataContract.Ordering
{
    public class OrderDataContract 
    {
        public virtual string Id { get; set; }
        
        public virtual IList<LineDataContract> Lines { get; set; }
        
    }
}
