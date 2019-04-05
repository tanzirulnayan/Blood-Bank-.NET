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
    public partial class ModeratorHomeForm : Form
    {

        Thread thread;
        private int u_id;

        public ModeratorHomeForm()
        {
            InitializeComponent();
            button5.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            this.u_id = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //profile
            this.Close();
            thread = new Thread(openNewModeratorProfileForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void openNewModeratorProfileForm()
        {
            Application.Run(new Profile_M());
        }


        private void openNewLoginForm()
        {
            Application.Run(new Form1());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //log out
            this.Close();
            thread = new Thread(openNewLoginForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            button7.Visible = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button8.Visible = false;
            button8.Enabled = false;

            DataSet ds = new DataSet();
            SqlDbDataAccess da = new SqlDbDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * FROM DONATION_HISTORY");
            using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
            {
                cmd.Connection.Open();
                DataTable tbl = new DataTable();
                dt.Fill(tbl);
                cmd.Connection.Close();
                dataGridView1.DataSource = tbl;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            button7.Visible = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button8.Visible = true;
            button8.Enabled = true;

            DataSet ds = new DataSet();
            SqlDbDataAccess da = new SqlDbDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * FROM USERS");
            using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
            {
                cmd.Connection.Open();
                DataTable tbl = new DataTable();
                dt.Fill(tbl);
                cmd.Connection.Close();
                dataGridView1.DataSource = tbl;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button7.Visible = true;
            button5.Enabled = true;
            button7.Enabled = true;
            button8.Visible = false;
            button8.Enabled = false;
            DataSet ds = new DataSet();
            SqlDbDataAccess da = new SqlDbDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * FROM USERS, LOGIN_CREDENTIALS WHERE LOGIN_CREDENTIALS.STATUS = 'PENDING' AND LOGIN_CREDENTIALS.ID = USERS.ID;");
            using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
            {
                cmd.Connection.Open();
                DataTable tbl = new DataTable();
                dt.Fill(tbl);
                cmd.Connection.Close();
                dataGridView1.DataSource = tbl;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //APPROVE
            bool flag;
            button5.Visible = true;
            button7.Visible = true;
            button5.Enabled = true;
            button7.Enabled = true;
            button8.Visible = false;
            button8.Enabled = false;

            if (u_id>0)
            {
                try
                {
                    BloodBankData bbd = new BloodBankData();
                    flag = bbd.updateLoginCredentials_Status(u_id);
                    if (flag == true)
                    {
                        MessageBox.Show("User Has Been Approved!", "Approved Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error While Approving User", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT * FROM USERS, LOGIN_CREDENTIALS WHERE LOGIN_CREDENTIALS.STATUS = 'PENDING' AND LOGIN_CREDENTIALS.ID = USERS.ID;");
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

                }
            }
            else
            {
                MessageBox.Show("Select a valid column first", "Inavlid Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //DECLINE
            bool flag;
            button5.Visible = true;
            button7.Visible = true;
            button5.Enabled = true;
            button7.Enabled = true;
            button8.Visible = false;
            button8.Enabled = false;

            if (u_id > 0)
            {
                try
                {
                    BloodBankData bbd = new BloodBankData();
                    flag = bbd.deleteUsers(u_id);
                    if (flag == true)
                    {
                        MessageBox.Show("User Has Been Deleted from USERS Table!", "Delete Successfull", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error While Deleting User from USERS Table", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    flag = bbd.deleteLoginCredentials(u_id);
                    if (flag == true)
                    {
                        MessageBox.Show("User Has Been Deleted from LOGIN_CREDENTIALS Table!", "Delete Successfull", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error While Deleting User from LOGIN_CREDENTIALS Table", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    DataSet ds = new DataSet();
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT * FROM USERS, LOGIN_CREDENTIALS WHERE LOGIN_CREDENTIALS.STATUS = 'PENDING' AND LOGIN_CREDENTIALS.ID = USERS.ID;");
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
                this.u_id = Convert.ToInt32(id);
            }
            catch(Exception ex)
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool flag;
            button5.Visible = false;
            button7.Visible = false;
            button5.Enabled = false;
            button7.Enabled = false;
            button8.Visible = true;
            button8.Enabled = true;

            if (u_id > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        BloodBankData bbd = new BloodBankData();
                        flag = bbd.deleteUsers(u_id);
                        if (flag == true)
                        {
                            MessageBox.Show("User Has Been Deleted from USERS Table!", "Delete Successfull", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error While Deleting User from USERS Table", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                        flag = bbd.deleteLoginCredentials(u_id);
                        if (flag == true)
                        {
                            MessageBox.Show("User Has Been Deleted from LOGIN_CREDENTIALS Table!", "Delete Successfull", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error While Deleting User from LOGIN_CREDENTIALS Table", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                        DataSet ds = new DataSet();
                        SqlDbDataAccess da = new SqlDbDataAccess();
                        SqlCommand cmd = da.GetCommand("SELECT * FROM USERS;");
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
                        MessageBox.Show("Failed to delete user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a valid column first", "Inavlid Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
    }
}
