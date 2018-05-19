using System.Collections.Generic;
using MaiDan.Infrastructure;

namespace MaiDan.Api.Documents
{
    public static class BillConfiguration
    {
        public static readonly IEnumerable<string> LEGAL_MENTIONS;

        static BillConfiguration()
        {
            LEGAL_MENTIONS = ConfigurationReader.GetCollection<string>("LegalMentions");
        }
    }
}
