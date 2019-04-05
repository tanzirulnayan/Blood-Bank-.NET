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
using System.IO;

namespace Blood_Bank
{
    public partial class SignUpModerator : Form
    {
        Thread thread;
        private string gender;
        private string address;
        private DateTime join_date;
        private string fileName;

        public SignUpModerator()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //insert into moderators
            bool flag = false;
           
            BloodBankData bbd = new BloodBankData();

            MODERATORS md = new MODERATORS();
            md.ID = 5;
            md.NAME = textBox1.Text;
            md.IMAGE = convertImageToBinary(pictureBox2.BackgroundImage);
            md.GENDER = gender;
            md.ADDRESS = address;
            md.EMAIL = textBox3.Text;
            md.CELLPHONE = textBox4.Text;
            md.JOIN_DATE = join_date;
            flag = bbd.insertModerators(md);
            
            if(flag == true)
            {
                this.Close();
                thread = new Thread(openSignUpSuccessForm);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
            {
                MessageBox.Show("Sign Up Failed!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
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

        private void openSignUpSuccessForm()
        {
            Application.Run(new SignUpSuccessfull());
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "MALE";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gender = "FEMALE";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            address = listBox1.SelectedItem.ToString();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            join_date = monthCalendar1.SelectionStart;
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
    }
}
