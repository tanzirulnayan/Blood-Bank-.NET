using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankDbConnection.Entities
{
    public class MODERATORS
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public byte[] IMAGE { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }
        public string EMAIL { get; set; }
        public string CELLPHONE { get; set; }
        public DateTime JOIN_DATE { get; set; }
    }
}
