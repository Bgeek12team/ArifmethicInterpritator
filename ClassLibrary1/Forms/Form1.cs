using ClassLibrary1;
using System.Xml.Linq;
using System;

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
            rcTxBx_outData.Text = String.Empty;
            var expression = rcTxBx_InputData.Text;
            tokens = Token.Tokenize(expression);


            foreach (var token in tokens)
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

        private void b1_GetTreeLex_Click(object sender, EventArgs e)
        {
            rcTxBx_outData.Text = String.Empty;
            var expression = rcTxBx_InputData.Text;
            var tree = parsingTree.parseTree(expression);
            foreach (var item in tree)
            {
                rcTxBx_outData.Text += item;
            }

        }

        
    }
}
