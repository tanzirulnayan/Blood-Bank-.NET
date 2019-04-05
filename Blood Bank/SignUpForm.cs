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
    public partial class SignUpForm : Form
    {
        private int id;
        private string name;
        private string password;
        private string password2;
        private DateTime dob;
        private string gender;
        private string bloodGroup;
        private string address;
        private string cellphone;
        private string email;
        private string fileName;
        private byte[] receiverImage;

        Thread thread;

        public SignUpForm()
        {
            InitializeComponent();
            setID();
            idVal.id = this.id;
            this.name = " ";
            this.password = " ";
            this.password2 = " ";
            this.gender = " ";
            this.bloodGroup = " ";
            this.address = " ";
            this.cellphone = " ";
            this.email = " ";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            thread = new Thread(openNewLoginForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void openNewLoginForm()
        {
            Application.Run(new Form1());
        }
        public void setID()
        {
            BloodBankData bbd = new BloodBankData();
            id = bbd.getNextID();
            idLabel.Text = id.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //insert into db as receiver
            name = textBox1.Text;
            password = passwordTB.Text.ToString();
            password2 = textBox2.Text.ToString();
            cellphone = textBox4.Text.ToString();
            email = textBox3.Text.ToString();

            if(!password.Equals(password2) || password.Equals(" "))
            {
                MessageBox.Show("Passwords don't match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(password.Equals(password2) && !password.Equals(" "))
            {
                if(name.Equals(" ") || gender.Equals(" ") || bloodGroup.Equals(" ") || address.Equals(" ") || cellphone.Equals(" ") || email.Equals(" "))
                {
                    MessageBox.Show("Fill Up All The Fields Properly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    BloodBankData bbd = new BloodBankData();

                    LOGIN_CREDENTIALS lc = new LOGIN_CREDENTIALS();
                    lc.PASSWORD = passwordTB.Text;
                    lc.TYPE = "RECEIVER";
                    lc.STATUS = "PENDING";
                    bbd.insertLoginCredentials(lc);

                    USERS u = new USERS();
                    u.ID = id;
                    u.NAME = textBox1.Text;
                    u.IMAGE = convertImageToBinary(pictureBox2.BackgroundImage);
                    u.DOB = dob;
                    u.GENDER = gender;
                    u.BLOOD_GROUP = bloodGroup;
                    u.ADDRESS = address;
                    u.CELLPHONE = cellphone;
                    u.EMAIL = email;

                    bbd.insertUsersReceiver(u);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    passwordTB.Clear();
                    monthCalendar1.ResetText();

                    this.Close();
                    thread = new Thread(openSignUpSuccessForm);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
            }
        }

        private void openSignUpSuccessForm()
        {
            Application.Run(new SignUpSuccessfull());
        }

        private void openNewBecomeDonorForm()
        {
            Application.Run(new BecomeDonorForm());
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            dob = monthCalendar1.SelectionStart;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "MALE";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gender = "FEMALE";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "A+";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "A-";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "AB+";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "AB-";
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "B+";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "B-";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "O+";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            bloodGroup = "O-";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            address = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "png files(.png)|*.png|jpg files(.jpg)|*.jpg", Multiselect = false })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    pictureBox2.BackgroundImage = Image.FromFile(fileName);
                }
            }    
        }

        public byte[] convertImageToBinary(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}
