using ClassLibrary1;

namespace Forms
{

    public partial class Form1 : Form
    {
        Token[] tokens;
        errorMessage errorMessageForm;
        int errorKey;
        Dictionary<int, Action> actions;

        public Form1()
        {
            InitializeComponent();
            errorMessageForm = new errorMessage();
            actions = new Dictionary<int, Action>
            {
                {-1, () => {extensionType.blinkColor(b4_SaveEval ,Color.Green); changeState(true, b1_GetTreeLex, b2_GetListLex, b3_ExpEval); } },
                {0, () => errorMessageForm.showError("Неккоректно раставлены скобки") }
            };
        }

        private void rcTxBx_InputData_TextChanged(object sender, EventArgs e)
        {
            b4_SaveEval.Enabled = rcTxBx_InputData.Text == string.Empty ? false : true;
            rcTxBx_outData.Text = string.Empty;
            changeState(false, b1_GetTreeLex, b2_GetListLex, b3_ExpEval);
        }

        private void b4_SaveEval_Click(object sender, EventArgs e)
        {
            actions[rcTxBx_InputData.Text.chekCorrect()].Invoke();
        }
        private void changeState(bool state, params Control[] controls)
        {
            foreach (var item in controls)
                item.Enabled = state;
        }

        private void b2_GetListLex_Click(object sender, EventArgs e)
        {
            var expression = rcTxBx_InputData.Text;
            tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            foreach (var token in inverse)
            {
                rcTxBx_outData.Text += $"{token}\n";
            }
        }

        private void b3_ExpEval_Click(object sender, EventArgs e)
        {
            tokens = Token.Tokenize(rcTxBx_InputData.Text);
            var test = new expEval();
            test.formOpen(tokens, rcTxBx_InputData.Text);
        }

    }
}
