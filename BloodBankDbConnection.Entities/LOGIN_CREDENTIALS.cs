using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankDbConnection.Entities
{
    public class LOGIN_CREDENTIALS
    {
        public int ID { get; set; }
        public string PASSWORD { get; set; }
        public string TYPE { get; set; }
        public string STATUS { get; set; }
    }
}
