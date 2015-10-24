namespace DG.UI.Helpers
{
    partial class FormMain
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
            this.label_comboBox1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label_comboBox2 = new System.Windows.Forms.Label();
            this.label_textBox2 = new System.Windows.Forms.Label();
            this.label_textBox1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_comboBox1
            // 
            this.label_comboBox1.AutoSize = true;
            this.label_comboBox1.Location = new System.Drawing.Point(12, 9);
            this.label_comboBox1.Name = "label_comboBox1";
            this.label_comboBox1.Size = new System.Drawing.Size(362, 13);
            this.label_comboBox1.TabIndex = 0;
            this.label_comboBox1.Text = "ComboBox - AutoCompleteOnEnter, DeselectOnEsc, ShowListOnRightclick";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(15, 65);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(150, 21);
            this.comboBox2.TabIndex = 3;
            // 
            // label_comboBox2
            // 
            this.label_comboBox2.AutoSize = true;
            this.label_comboBox2.Location = new System.Drawing.Point(12, 49);
            this.label_comboBox2.Name = "label_comboBox2";
            this.label_comboBox2.Size = new System.Drawing.Size(603, 13);
            this.label_comboBox2.TabIndex = 2;
            this.label_comboBox2.Text = "ComboBox - ComboBox - AutoCompleteOnEnter, DeselectOnEsc, ShowListOnRightclick, S" +
    "electOnDoubleClick + PostFunction";
            // 
            // label_textBox2
            // 
            this.label_textBox2.AutoSize = true;
            this.label_textBox2.Location = new System.Drawing.Point(12, 129);
            this.label_textBox2.Name = "label_textBox2";
            this.label_textBox2.Size = new System.Drawing.Size(591, 13);
            this.label_textBox2.TabIndex = 5;
            this.label_textBox2.Text = "TextBox - ComboBox - AutoCompleteOnEnter, DeselectOnEsc, ShowListOnRightclick, Se" +
    "lectOnDoubleClick + PostFunction";
            // 
            // label_textBox1
            // 
            this.label_textBox1.AutoSize = true;
            this.label_textBox1.Location = new System.Drawing.Point(12, 89);
            this.label_textBox1.Name = "label_textBox1";
            this.label_textBox1.Size = new System.Drawing.Size(350, 13);
            this.label_textBox1.TabIndex = 4;
            this.label_textBox1.Text = "TextBox - AutoCompleteOnEnter, DeselectOnEsc, ShowListOnRightclick";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 105);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(15, 145);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 7;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(578, 170);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 8;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 200);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label_textBox2);
            this.Controls.Add(this.label_textBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label_comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label_comboBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(677, 239);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(677, 239);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Form";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_comboBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label_comboBox2;
        private System.Windows.Forms.Label label_textBox2;
        private System.Windows.Forms.Label label_textBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_close;
    }
}

