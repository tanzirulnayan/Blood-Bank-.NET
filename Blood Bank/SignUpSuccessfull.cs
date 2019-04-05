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

namespace Blood_Bank
{
    public partial class SignUpSuccessfull : Form
    {
        Thread thread;

        public SignUpSuccessfull()
        {
            InitializeComponent();
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
    }
}
