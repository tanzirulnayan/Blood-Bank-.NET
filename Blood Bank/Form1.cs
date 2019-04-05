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
    public partial class Form1 : Form
    {
        public int id { get; set; }
        private string password;
        private int loginFlag;
        Thread thread;

        public Form1()
        {
            InitializeComponent();
            this.id = 0;
            this.password = " ";
        }

        //public delegate void openNewSignUpDonorMethodDelegate();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            //sign up
            DialogResult result = MessageBox.Show("Do you want to sign up as a donor?\nClick YES if you want to sign up as a donor\nClick NO if you want to sign up as a receiver", "Sign Up Type", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.Close();
                thread = new Thread(openNewSignUpDonorForm);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else if(result == DialogResult.No)
            {
                this.Close();
                thread = new Thread(openNewSignUpForm);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            
        }

        private void openNewSignUpDonorForm()
        {
            Application.Run(new SignUpDonor());
        }

        private void openNewSignUpForm()
        {
            Application.Run(new SignUpForm());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //log in
            try
            {
                password = textBox2.Text;
                this.id = Convert.ToInt32(textBox1.Text);
            }
            catch(Exception ex)
            {

            }
            if (this.id == 0 || this.password.Equals(" "))
            {
                MessageBox.Show("Enter Username & Password First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                try
                {
                    idVal iv = new idVal(id);

                    BloodBankData bbd = new BloodBankData();
                    loginFlag = bbd.logIn(id, password);
                    if (loginFlag == 3)
                    {
                        this.Close();
                        thread = new Thread(openNewDonorHomeForm);
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                    else if (loginFlag == 2)
                    {
                        this.Close();
                        thread = new Thread(openNewUserHomeForm);
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                    else if (loginFlag == 1)
                    {
                        this.Close();
                        thread = new Thread(openNewModeratorHomeForm);
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                    else
                    {
                        MessageBox.Show("Your account is not active yet or invalid username or password.Please wait for admin approval", "Invalid Login Credentials!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {

                }
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void openNewUserHomeForm()
        {
            Application.Run(new UserHomeForm());
        }

        private void openNewDonorHomeForm()
        {
            Application.Run(new DonorHomeForm());
        }

        private void openNewModeratorHomeForm()
        {
            Application.Run(new ModeratorHomeForm());
        }
    }
}
