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
using System.IO;

namespace Blood_Bank
{
    public partial class Profile_D : Form
    {

        Thread thread;
        private byte[] IMAGE;
        private int u_id;
        private string currentPassword;
        private string newPassword;
        private string pwd;

        public Profile_D()
        {
            InitializeComponent();
            setValuesToTextbox();
            label25.Visible = false;
            label26.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button1.Visible = false;
            label25.Enabled = false;
            label26.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button3.Visible = false;
        }

        public void setValuesToTextbox()
        {
            try
            {
                SqlDbDataAccess da = new SqlDbDataAccess();
                SqlCommand cmd = da.GetCommand("SELECT * FROM USERS WHERE ID = " + idVal.id);

                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        idLabel.Text = reader.GetInt32(0).ToString();
                        label8.Text = reader.GetString(1);
                        IMAGE = (byte[])reader[2];
                        MemoryStream ms = new MemoryStream(IMAGE);
                        pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                        label11.Text = reader.GetDateTime(3).ToString();
                        label14.Text = reader.GetString(4);
                        label18.Text = reader.GetString(5);
                        label19.Text = reader.GetString(6);
                        label20.Text = reader.GetString(7);
                        label21.Text = reader.GetString(8);
                        label22.Text = reader.GetString(11);
                        label23.Text = reader.GetString(12);
                        label24.Text = reader.GetDateTime(13).ToString();
                    }
                }
                reader.Close();
                cmd.Connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to load data", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public Image converBinaryToImage(byte[] img)
        {
            using (MemoryStream ms = new MemoryStream(img))
            {
                return Image.FromStream(ms);
            }
        }

        private void Profile_D_Load(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //back
            this.Close();
            thread = new Thread(openNewDonorHomeForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void openNewDonorHomeForm()
        {
            Application.Run(new DonorHomeForm());
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

        private void button2_Click(object sender, EventArgs e)
        {
            label25.Visible = true;
            label26.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button1.Visible = true;
            label25.Enabled = true;
            label26.Enabled = true;
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
                            label25.Visible = false;
                            label26.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            button1.Visible = false;
                            label25.Enabled = false;
                            label26.Enabled = false;
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
            label25.Visible = false;
            label26.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button1.Visible = false;
            label25.Enabled = false;
            label26.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button3.Visible = false;
        }
    }
}
