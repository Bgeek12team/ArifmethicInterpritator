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
    public partial class ExpEval : Form
    {
        private List<char> variables;
        private string exp;
        private GroupBox groupBox;
        private List<TextBox> txBxInputs;
        private RichTextBox rcBox_postFix;
        Token[] tokens;

        public ExpEval()
        {
            
            InitializeComponent();
            
            
        }
        public void FormOpen(Token[] tokens, string expression)
        {
            variables = new List<char>();
            foreach (Token token in tokens)
                if (token.Type.ToString() == "VARIABLE" && !variables.Contains(Convert.ToChar(token.TokenString)))
                    variables.Add(Convert.ToChar(token.TokenString));
            this.tokens = tokens;
            exp = expression;
            this.Show();
            txBxInputs = new List<TextBox>();
            groupBox = Forming();
            this.Controls.Add(groupBox);
            
        }


        private void ExpEval_Load(object sender, EventArgs e)
        {
            
        }


        private GroupBox Forming()
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

            rcBox_postFix = new RichTextBox()
            {
                Text = exp,
                Location = new Point(x + 200, y - variables.Count * 40),
                Font = new Font("TimeNewRomans", 16),
                Size = new Size(500, 300)
            };
            buttonGetResult.Click += ButtonGetResult_Click;
            form.Controls.Add(buttonGetResult);
            form.Controls.Add(rcBox_postFix);
            return form;
        }

        private void ButtonGetResult_Click(object? sender, EventArgs e)
        {
            var expression = new Expression(exp);
            var variable = new Dictionary<char, double>();

            for (int i = 0; i < variables.Count; i++)
            {
                if (txBxInputs[i].Text.itsNum())
                {
                    variable.Add(variables[i], Convert.ToDouble(txBxInputs[i].Text));
                } else { MessageBox.Show("Некорректное значение переменных!","ОШИБКА"); return; }
                
            }
            var inverse = Polish.ToInversePolishView(tokens);
            var result = expression.CalculateAt(variable);
            if (expression.IsBooleanExpression)
                rcBox_postFix.Text += "= " + Convert.ToString((bool)result + "\n Постфиксная запись:");
            else
                rcBox_postFix.Text += "= " + Convert.ToString((double)result + "\n Постфиксная запись:");


            foreach (var item in inverse)
                rcBox_postFix.Text += $"\n{item}";
        }
    }


}
