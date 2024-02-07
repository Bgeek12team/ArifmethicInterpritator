namespace Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void rcTxBx_InputData_TextChanged(object sender, EventArgs e)
        {
            b4_SaveEval.Enabled = rcTxBx_InputData.Text == string.Empty ? false : true;
        }

        private void b4_SaveEval_Click(object sender, EventArgs e)
        {
            string expression = rcTxBx_InputData.Text;
            if (expression.count('(') != expression.count(')'))
            {
                MessageBox.Show("ׂû המכבאוב?");
            } else { b4_SaveEval.blinkColor(Color.Green); }
        }
       
    }
}
