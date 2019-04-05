using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BloodBankDbConnection.Entities;
using BloodBankDbConnection.Framework;
using BloodBankDbConnection.DataAccess;
using System.Threading;

namespace Blood_Bank
{
    public partial class BecomeDonorForm : Form
    {
        Thread thread;
        private double height;
        private double weight;
        private string drugAddiction;
        private string HIV;
        private DateTime donationDate;

        public BecomeDonorForm()
        {
            InitializeComponent();
        }

        public bool updateUsers()
        {
            int val = 0;
            try
            {
                height = Convert.ToDouble(textBox9.Text);
                weight = Convert.ToDouble(textBox10.Text);

                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("UPDATE USERS SET HEIGHT = "+height+", WEIGHT = "+weight+", DRUG_ADDICTION = '"+drugAddiction+"', HIV_STATUS = '"+HIV+"', LAST_DONATION_DATE = "+donationDate+" WHERE ID = "+idVal.id);
                
                cmd.Connection.Open();
                val = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

            }
            return val > 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            drugAddiction = "YES";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            drugAddiction = "NO";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            HIV = "POSITIVE";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            HIV = "NEGATIVE";
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            donationDate = monthCalendar1.SelectionStart;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            flag = updateUsers();
            if(flag == true)
            {
                this.Close();
                thread = new Thread(openSignUpSuccessForm);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
            {
                MessageBox.Show("Failed to Update USERS Table as Donor", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openSignUpSuccessForm()
        {
            Application.Run(new SignUpSuccessfull());
        }
    }
}
