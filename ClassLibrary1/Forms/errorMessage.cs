using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class errorMessage : Form
    {
        public errorMessage()
        {
            InitializeComponent();
        }


        private void errorMessage_Load(object sender, EventArgs e)
        {

        }

        private void b1Contimue_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void showError(string errorMessage)
        {
            lbErrorMessage.Text = errorMessage;
            this.Show();
        }

        private void lbErrorMessage_Click(object sender, EventArgs e)
        {

        }
    }
}
