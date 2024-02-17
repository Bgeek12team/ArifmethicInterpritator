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
        private List<(string name, Type type)> variables;
        private string exp;
        private GroupBox groupBox;
        private List<TextBox> txBxInputs;
        private RichTextBox rcBox_postFix;
        Token[] tokens;
        Expression expresion;
        public ExpEval()
        {

            InitializeComponent();


        }
        public void FormOpen(Token[] tokens, string expression)
        {
            expresion = new Expression(expression);
            expresion.ParseTree();

            variables = [];
            expresion.TreeNode.ForEach(node =>
            {
                if (node.Value.Type == Token.TYPE.VARIABLE &&
                    !variables.Contains((Convert.ToString(node.Value.TokenString), typeof(double))) &&
                    !variables.Contains((Convert.ToString(node.Value.TokenString), typeof(bool))))
                {
                    if (node.Value.ExpectedType == typeof(double))
                        variables.Add((Convert.ToString(node.Value.TokenString), typeof(double)));
                    else
                        variables.Add((Convert.ToString(node.Value.TokenString), typeof(bool)));
                }
            });


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
                Size = new Size(850, 400),
                Location = new Point(5, 0),
                Font = new Font("TimeNewRomans", 24)
            };
            int y = 25;
            int x = 20;
            for (int i = 0; i < variables.Count; i++)
            {
                var lVar = new Label()
                {
                    Text = variables[i].name + " (" + variables[i].type + ") =",
                    Size = new Size(160, 40),
                    Location = new Point(x, y),
                    Font = new Font("Times New Roman", 11)
                };
                var txBxVar = new TextBox()
                {
                    Size = new Size(80, 40),
                    Location = new Point(x + 165, y + 5),
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
                Location = new Point(x + 300, y - variables.Count * 40),
                Font = new Font("TimeNewRomans", 16),
                Size = new Size(600, 300)
            };
            buttonGetResult.Click += ButtonGetResult_Click;
            form.Controls.Add(buttonGetResult);
            form.Controls.Add(rcBox_postFix);
            return form;
        }

        private void ButtonGetResult_Click(object? sender, EventArgs e)
        {
            var variables = new Dictionary<string, double>();
            var booleanVariables = new Dictionary<string, bool>();

            for (int i = 0; i < this.variables.Count; i++)
            {

                if (txBxInputs[i].Text.IsNumeric() && this.variables[i].type == typeof(double))
                {
                    variables.Add(this.variables[i].name, Convert.ToDouble(txBxInputs[i].Text));
                }
                else if (txBxInputs[i].Text.IsBoolean() && this.variables[i].type == typeof(bool))
                {
                    booleanVariables.Add(this.variables[i].name, Convert.ToBoolean(txBxInputs[i].Text));
                }
                else { MessageBox.Show("Некорректное значение переменных!", "ОШИБКА"); return; }

            }
            var result = expresion.CalculateAt(variables, booleanVariables);
            if (expresion.IsBooleanExpression)
                rcBox_postFix.Text += "= " + Convert.ToString((bool)result + "\n Постфиксная запись:");
            else
                rcBox_postFix.Text += "= " + Convert.ToString((double)result + "\n Постфиксная запись:");

            expresion.TreeNode.ForEach(node =>
            {
                rcBox_postFix.Text += $"\n{node.Value}";
            });


        }
    }


}