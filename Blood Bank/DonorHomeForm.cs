using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using BloodBankDbConnection.Entities;
using BloodBankDbConnection.Framework;
using BloodBankDbConnection.DataAccess;

namespace Blood_Bank
{
    public partial class DonorHomeForm : Form
    {
        Thread thread;
        private string gender;
        private string bloodGroup;
        private string address;
        private int u_id;
        private int d_id;

        public DonorHomeForm()
        {
            InitializeComponent();
            this.gender = " ";
            this.bloodGroup = " ";
            this.address = " ";
            this.u_id = idVal.id;
            button1.Visible = false;
            button1.Enabled = false;
            dateTimePicker1.Visible = false;
        }

        private void openNewLoginForm()
        {
            Application.Run(new Form1());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //profile
            this.Close();
            thread = new Thread(openNewDonorProfileForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void openNewDonorProfileForm()
        {
            Application.Run(new Profile_D());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //log out
            this.Close();
            thread = new Thread(openNewLoginForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bloodGroup = listBox1.SelectedItem.ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.address = listBox2.SelectedItem.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.gender = "MALE";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.gender = "FEMALE";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!bloodGroup.Equals(" ") && address.Equals(" ") && gender.Equals(" "))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT ID, NAME, BLOOD_GROUP, ADDRESS, GENDER, CELLPHONE, EMAIL, DRUG_ADDICTION, HIV_STATUS FROM USERS WHERE ID IN (SELECT ID FROM LOGIN_CREDENTIALS WHERE TYPE = 'DONOR' AND STATUS = 'ACTIVE') AND BLOOD_GROUP LIKE '" + bloodGroup + "';");
                    using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection.Open();
                        DataTable tbl = new DataTable();
                        dt.Fill(tbl);
                        cmd.Connection.Close();
                        dataGridView1.DataSource = tbl;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (bloodGroup.Equals(" ") && address.Equals(" ") && gender.Equals(" "))
            {
                MessageBox.Show("You have not selected any blood group or location or gender", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (bloodGroup.Equals(" ") && !address.Equals(" ") && gender.Equals(" "))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT ID, NAME, BLOOD_GROUP, ADDRESS, GENDER, CELLPHONE, EMAIL, DRUG_ADDICTION, HIV_STATUS FROM USERS WHERE ID IN (SELECT ID FROM LOGIN_CREDENTIALS WHERE TYPE = 'DONOR' AND STATUS = 'ACTIVE') AND ADDRESS LIKE '" + address + "';");
                    using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection.Open();
                        DataTable tbl = new DataTable();
                        dt.Fill(tbl);
                        cmd.Connection.Close();
                        dataGridView1.DataSource = tbl;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (bloodGroup.Equals(" ") && address.Equals(" ") && !gender.Equals(" "))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT ID, NAME, BLOOD_GROUP, ADDRESS, GENDER, CELLPHONE, EMAIL, DRUG_ADDICTION, HIV_STATUS FROM USERS WHERE ID IN (SELECT ID FROM LOGIN_CREDENTIALS WHERE TYPE = 'DONOR' AND STATUS = 'ACTIVE') AND GENDER LIKE '" + gender + "';");
                    using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection.Open();
                        DataTable tbl = new DataTable();
                        dt.Fill(tbl);
                        cmd.Connection.Close();
                        dataGridView1.DataSource = tbl;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!bloodGroup.Equals(" ") && !address.Equals(" ") && gender.Equals(" "))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT ID, NAME, BLOOD_GROUP, ADDRESS, GENDER, CELLPHONE, EMAIL, DRUG_ADDICTION, HIV_STATUS FROM USERS WHERE ID IN (SELECT ID FROM LOGIN_CREDENTIALS WHERE TYPE = 'DONOR' AND STATUS = 'ACTIVE') AND ADDRESS LIKE '" + address + "' AND BLOOD_GROUP LIKE '" + bloodGroup + "';");
                    using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection.Open();
                        DataTable tbl = new DataTable();
                        dt.Fill(tbl);
                        cmd.Connection.Close();
                        dataGridView1.DataSource = tbl;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (bloodGroup.Equals(" ") && !address.Equals(" ") && !gender.Equals(" "))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT ID, NAME, BLOOD_GROUP, ADDRESS, GENDER, CELLPHONE, EMAIL, DRUG_ADDICTION, HIV_STATUS FROM USERS WHERE ID IN (SELECT ID FROM LOGIN_CREDENTIALS WHERE TYPE = 'DONOR' AND STATUS = 'ACTIVE') AND ADDRESS LIKE '" + address + "' AND GENDER LIKE '" + gender + "';");
                    using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection.Open();
                        DataTable tbl = new DataTable();
                        dt.Fill(tbl);
                        cmd.Connection.Close();
                        dataGridView1.DataSource = tbl;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!bloodGroup.Equals(" ") && address.Equals(" ") && !gender.Equals(" "))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT ID, NAME, BLOOD_GROUP, ADDRESS, GENDER, CELLPHONE, EMAIL, DRUG_ADDICTION, HIV_STATUS FROM USERS WHERE ID IN (SELECT ID FROM LOGIN_CREDENTIALS WHERE TYPE = 'DONOR' AND STATUS = 'ACTIVE') AND BLOOD_GROUP LIKE '" + bloodGroup + "' AND GENDER LIKE '" + gender + "';");
                    using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection.Open();
                        DataTable tbl = new DataTable();
                        dt.Fill(tbl);
                        cmd.Connection.Close();
                        dataGridView1.DataSource = tbl;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!bloodGroup.Equals(" ") && !address.Equals(" ") && !gender.Equals(" "))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT ID, NAME, BLOOD_GROUP, ADDRESS, GENDER, CELLPHONE, EMAIL, DRUG_ADDICTION, HIV_STATUS FROM USERS WHERE ID IN (SELECT ID FROM LOGIN_CREDENTIALS WHERE TYPE = 'DONOR' AND STATUS = 'ACTIVE') AND ADDRESS LIKE '" + address + "' AND BLOOD_GROUP LIKE '" + bloodGroup + "' AND GENDER LIKE '" + gender + "';");
                    using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection.Open();
                        DataTable tbl = new DataTable();
                        dt.Fill(tbl);
                        cmd.Connection.Close();
                        dataGridView1.DataSource = tbl;
                    }
                    button1.Visible = true;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not selected any blood group or location or gender", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag;
            //MessageBox.Show(d_id.ToString());
            if (d_id > 0)
            {
                try
                {
                    //insert into db donation history
                    BloodBankData bbd = new BloodBankData();

                    DONATION_HISTORY dh = new DONATION_HISTORY();
                    dh.DONOR_ID = d_id;
                    dh.RECIEVER_ID = u_id;
                    dh.DONATION_DATE = dateTimePicker1.Value;
                    flag = bbd.insertDonationHistory(dh);
                    if (flag == true)
                    {
                        MessageBox.Show("Blood Request Successful!", "Blood Request", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error While Requesting Blood!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("Select a valid column first", "Inavlid Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                this.d_id = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                this.d_id = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button1.Enabled = false;

            try
            {
                DataSet ds = new DataSet();
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("SELECT * FROM USERS WHERE ID IN (SELECT RECIEVER_ID FROM DONATION_HISTORY WHERE DONOR_ID = "+u_id+");");
                using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
                {
                    cmd.Connection.Open();
                    DataTable tbl = new DataTable();
                    dt.Fill(tbl);
                    cmd.Connection.Close();
                    dataGridView1.DataSource = tbl;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
