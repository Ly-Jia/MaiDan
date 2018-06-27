using System.Collections.Generic;

namespace MaiDan.Api.DataContracts.Requests
{
    public class Slip
    {
        public int Id { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
