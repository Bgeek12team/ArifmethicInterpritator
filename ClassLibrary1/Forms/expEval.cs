using ClassLibrary1;
using Microsoft.VisualBasic;
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
    public partial class expEval : Form
    {
        private List<char> variables;
        private string exp;
        private GroupBox groupBox;
        private List<TextBox> txBxInputs;

        public expEval()
        {
            
            InitializeComponent();
            
            
        }
        public void formOpen(Token[] tokens, string expression)
        {
            variables = new List<char>();
            foreach (Token token in tokens)
                if (token.Type.ToString() == "VARIABLE" && !variables.Contains(Convert.ToChar(token.TokenString)))
                    variables.Add(Convert.ToChar(token.TokenString));
            exp = expression;
            this.Show();
            txBxInputs = new List<TextBox>();
            groupBox = forming();
            this.Controls.Add(groupBox);
            
        }


        private void expEval_Load(object sender, EventArgs e)
        {
            
        }


        private GroupBox forming()
        {
            var form = new GroupBox()
            {
                Size = new Size(750, 400),
                Location = new Point(5, 0),
                Font = new Font("TimeNewRomans", 24)
            };
            int y = 25;
            int x = 20;
            for (int i = 0; i < variables.Count; i++)
            {
                var lVar = new Label()
                {
                    Text = variables[i] + "=",
                    Size = new Size(60, 40),
                    Location = new Point(x, y)
                };
                var txBxVar = new TextBox()
                {
                    Size = new Size(80, 40),
                    Location = new Point(x + 65, y + 5),
                    Font = new Font("TimeNewRomans", 16),
                    Name = $"txBx{i}_InputData"
                };
                y += 40;
                form.Controls.Add(lVar);
                form.Controls.Add(txBxVar);
                txBxInputs.Add(txBxVar);
            }
            var buttonGetResult = new Button()
            {
                Text = "Получить результат",
                Size = new Size(180, 60),
                Location = new Point(x, y + 10),
                Font = new Font("TimeNewRomans", 14)
            };
            buttonGetResult.Click += ButtonGetResult_Click;
            form.Controls.Add(buttonGetResult);
            return form;
        }

        private void ButtonGetResult_Click(object? sender, EventArgs e)
        {
            var expression = new Expression(exp);
            var variable = new Dictionary<char, double>();
            for (int i = 0; i < variables.Count; i++)
                variable.Add(variables[i], Convert.ToDouble(txBxInputs[i].Text));
            MessageBox.Show(Convert.ToString(expression.CalculateAt(variable, out _)));


        }
    }


}
