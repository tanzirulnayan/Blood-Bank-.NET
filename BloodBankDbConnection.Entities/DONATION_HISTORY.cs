using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankDbConnection.Entities
{
    public class DONATION_HISTORY
    {
        public int DONOR_ID { get; set; }
        public int RECIEVER_ID { get; set; }
        public DateTime DONATION_DATE { get; set; }
    }
}
