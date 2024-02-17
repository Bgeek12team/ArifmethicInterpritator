using ClassLibrary1;

namespace Forms
{
    public partial class expEval : Form
    {
<<<<<<< Updated upstream
        private List<char> variables;
=======
        private List<(string name, Type type)> variables;
>>>>>>> Stashed changes
        private string exp;
        private GroupBox groupBox;
        private Label expControl;
        private List<TextBox> txBxInputs;

        public expEval()
        {
            
            InitializeComponent();
            
            
        }
        public void formOpen(Token[] tokens, string expression)
        {
<<<<<<< Updated upstream
            variables = new List<char>();
            foreach (Token token in tokens)
                if (token.Type.ToString() == "VARIABLE" && !variables.Contains(Convert.ToChar(token.TokenString)))
                    variables.Add(Convert.ToChar(token.TokenString));
=======
            expresion = new Expression(expression);
            expresion.ParseTree();

            variables = [];
            expresion.TreeNode?.ForEach(node =>
            {
                if (node.Value.Type == Token.TYPE.VARIABLE &&
                    !variables.Contains((node.Value.TokenString, typeof(double))) &&
                    !variables.Contains((node.Value.TokenString, typeof(bool))))
                {
                    if (node.Value.ExpectedType == typeof(double))
                        variables.Add((node.Value.TokenString, typeof(double)));
                    else
                        variables.Add((node.Value.TokenString, typeof(bool)));
                }
            });


            this.tokens = tokens;
>>>>>>> Stashed changes
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
            expControl = new Label()
            {
                Text = exp,
                Location = new Point(x + 200, y - variables.Count * 40),
                Font = new Font("TimeNewRomans", 21),
                Size = new Size(500, 300)
            };

            buttonGetResult.Click += ButtonGetResult_Click;
            form.Controls.Add(buttonGetResult);
            form.Controls.Add(expControl);
            return form;
        }

        private void ButtonGetResult_Click(object? sender, EventArgs e)
        {
<<<<<<< Updated upstream
            var expression = new Expression(exp);
            var variable = new Dictionary<char, double>();
            for (int i = 0; i < variables.Count; i++)
                variable.Add(variables[i], Convert.ToDouble(txBxInputs[i].Text));
            expControl.Text += "= " + Convert.ToString(expression.CalculateAt(variable));
=======
            var variables = new Dictionary<string, double>();
            var booleanVariables = new Dictionary<string, bool>();
>>>>>>> Stashed changes


        }
    }


}
