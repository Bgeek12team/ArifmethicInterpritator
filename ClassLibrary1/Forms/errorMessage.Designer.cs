namespace Forms
{
    partial class errorMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lbErrorMessage = new Label();
            b1Contimue = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(111, 26);
            label1.TabIndex = 0;
            label1.Text = "Ошибка!";
            // 
            // lbErrorMessage
            // 
            lbErrorMessage.AutoSize = true;
            lbErrorMessage.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lbErrorMessage.Location = new Point(12, 48);
            lbErrorMessage.Name = "lbErrorMessage";
            lbErrorMessage.Size = new Size(63, 24);
            lbErrorMessage.TabIndex = 1;
            lbErrorMessage.Text = "label2";
            lbErrorMessage.Click += lbErrorMessage_Click;
            // 
            // b1Contimue
            // 
            b1Contimue.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            b1Contimue.Location = new Point(12, 90);
            b1Contimue.Name = "b1Contimue";
            b1Contimue.Size = new Size(330, 66);
            b1Contimue.TabIndex = 2;
            b1Contimue.Text = "Продолжить";
            b1Contimue.UseVisualStyleBackColor = true;
            b1Contimue.Click += b1Contimue_Click;
            // 
            // errorMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 168);
            Controls.Add(b1Contimue);
            Controls.Add(lbErrorMessage);
            Controls.Add(label1);
            Name = "errorMessage";
            Text = "errorMessage";
            Load += errorMessage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lbErrorMessage;
        private Button b1Contimue;
    }
}