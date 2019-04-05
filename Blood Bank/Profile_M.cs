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
using BloodBankDbConnection.Framework;
using BloodBankDbConnection.Entities;
using BloodBankDbConnection.DataAccess;
using System.IO;

namespace Blood_Bank
{
    public partial class Profile_M : Form
    {
        Thread thread;
        public int ID { get; set; }
        public string NAME { get; set; }
        public byte[] IMAGE { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }
        public string EMAIL { get; set; }
        public string CELLPHONE { get; set; }
        public DateTime JOIN_DATE { get; set; }
        private int u_id;
        private string currentPassword;
        private string newPassword;
        private string pwd;

        public Profile_M()
        {
            InitializeComponent();
            setValuesToTextbox();
            label19.Visible = false;
            label20.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button1.Visible = false;
            label19.Enabled = false;
            label20.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button3.Visible = false;
        }

        public void setValuesToTextbox()
        {
            SqlDbDataAccess da = new SqlDbDataAccess();
            SqlCommand cmd = da.GetCommand("SELECT * FROM MODERATORS WHERE ID = " + idVal.id);

            cmd.Connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    idLabel.Text = reader.GetInt32(0).ToString();
                    label6.Text = reader.GetString(1);
                    IMAGE = (byte[])reader[2];
                    MemoryStream ms = new MemoryStream(IMAGE);
                    pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                    label8.Text = reader.GetString(3);
                    label9.Text = reader.GetString(4);
                    label11.Text = reader.GetString(5);
                    label13.Text = reader.GetString(6);
                    label14.Text = reader.GetDateTime(7).ToString();
                }
            }
            reader.Close();
            cmd.Connection.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //back
            this.Close();
            thread = new Thread(openNewModeratorHomeForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void openNewModeratorHomeForm()
        {
            Application.Run(new ModeratorHomeForm());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //log out
            this.Close();
            thread = new Thread(openNewLoginForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void openNewLoginForm()
        {
            Application.Run(new Form1());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label19.Visible = true;
            label20.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button1.Visible = true;
            label19.Enabled = true;
            label20.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            button3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            bool idMatchFlag = false;

            currentPassword = textBox1.Text.ToString();
            newPassword = textBox2.Text.ToString();

            if (currentPassword.Equals("") || newPassword.Equals(""))
            {
                MessageBox.Show("Enter Current Password & New Password Properly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!currentPassword.Equals("") && !newPassword.Equals(""))
            {
                //check current password
                try
                {
                    SqlDbDataAccess da = new SqlDbDataAccess();
                    SqlCommand cmd = da.GetCommand("SELECT PASSWORD FROM LOGIN_CREDENTIALS WHERE ID = " + idVal.id);

                    cmd.Connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            this.pwd = reader.GetString(0);
                        }
                    }
                    reader.Close();
                    cmd.Connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to check password in DB", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!pwd.Equals(currentPassword))
                {
                    MessageBox.Show("Wrong Current Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (pwd.Equals(currentPassword))
                {
                    //change to new password
                    try
                    {
                        BloodBankData bbd = new BloodBankData();
                        flag = bbd.updateLoginCredentials_Password(idVal.id, newPassword);
                        if (flag == true)
                        {
                            MessageBox.Show("Password Has Been Changed Successfully!", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();
                            textBox2.Clear();
                            label19.Visible = false;
                            label20.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            button1.Visible = false;
                            label19.Enabled = false;
                            label20.Enabled = false;
                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            button1.Enabled = false;
                            button3.Enabled = false;
                            button3.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Failed to Change Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to Change Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            label19.Visible = false;
            label20.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button1.Visible = false;
            label19.Enabled = false;
            label20.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button3.Visible = false;
        }
    }
}
