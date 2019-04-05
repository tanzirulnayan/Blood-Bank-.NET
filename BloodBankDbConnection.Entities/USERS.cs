using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankDbConnection.Entities
{
    public class USERS
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public byte[] IMAGE { get; set; }
        public DateTime DOB { get; set; }
        public string GENDER { get; set; }
        public string BLOOD_GROUP { get; set; }
        public string ADDRESS { get; set; }
        public string CELLPHONE { get; set; }
        public string EMAIL { get; set; }
        public double HEIGHT { get; set; }
        public double WEIGHT { get; set; }
        public string DRUG_ADDICTION { get; set; }
        public string HIV_STATUS { get; set; }
        public DateTime LAST_DONATION_DATE { get; set; }
    }
}
