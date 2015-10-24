namespace DG.UI.Helpers
{
    partial class EnhancedTextBoxHelperList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel_main = new System.Windows.Forms.Panel();
            this.advancedDataGridView_main = new Zuby.ADGV.AdvancedDataGridView();
            this.panel_filters = new System.Windows.Forms.Panel();
            this.button_resetsorting = new System.Windows.Forms.Button();
            this.button_resetfilters = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox_setcalleronchange = new System.Windows.Forms.CheckBox();
            this.button_close = new System.Windows.Forms.Button();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).BeginInit();
            this.panel_filters.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.advancedDataGridView_main);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 37);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(384, 187);
            this.panel_main.TabIndex = 0;
            // 
            // advancedDataGridView_main
            // 
            this.advancedDataGridView_main.AllowUserToAddRows = false;
            this.advancedDataGridView_main.AllowUserToDeleteRows = false;
            this.advancedDataGridView_main.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.advancedDataGridView_main.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.advancedDataGridView_main.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.advancedDataGridView_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.advancedDataGridView_main.DefaultCellStyle = dataGridViewCellStyle2;
            this.advancedDataGridView_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView_main.FilterAndSortEnabled = true;
            this.advancedDataGridView_main.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridView_main.MultiSelect = false;
            this.advancedDataGridView_main.Name = "advancedDataGridView_main";
            this.advancedDataGridView_main.ReadOnly = true;
            this.advancedDataGridView_main.RowHeadersVisible = false;
            this.advancedDataGridView_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.advancedDataGridView_main.Size = new System.Drawing.Size(384, 187);
            this.advancedDataGridView_main.TabIndex = 2;
            this.advancedDataGridView_main.SortStringChanged += new System.EventHandler(this.advancedDataGridView_main_SortStringChanged);
            this.advancedDataGridView_main.FilterStringChanged += new System.EventHandler(this.advancedDataGridView_main_FilterStringChanged);
            this.advancedDataGridView_main.DoubleClick += new System.EventHandler(this.advancedDataGridView_main_DoubleClick);
            // 
            // panel_filters
            // 
            this.panel_filters.Controls.Add(this.button_resetsorting);
            this.panel_filters.Controls.Add(this.button_resetfilters);
            this.panel_filters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_filters.Location = new System.Drawing.Point(0, 0);
            this.panel_filters.Name = "panel_filters";
            this.panel_filters.Size = new System.Drawing.Size(384, 37);
            this.panel_filters.TabIndex = 1;
            // 
            // button_resetsorting
            // 
            this.button_resetsorting.Location = new System.Drawing.Point(93, 8);
            this.button_resetsorting.Name = "button_resetsorting";
            this.button_resetsorting.Size = new System.Drawing.Size(75, 23);
            this.button_resetsorting.TabIndex = 1;
            this.button_resetsorting.Text = "Reset Sort";
            this.button_resetsorting.UseVisualStyleBackColor = true;
            this.button_resetsorting.Click += new System.EventHandler(this.button_resetsorting_Click);
            // 
            // button_resetfilters
            // 
            this.button_resetfilters.Location = new System.Drawing.Point(12, 8);
            this.button_resetfilters.Name = "button_resetfilters";
            this.button_resetfilters.Size = new System.Drawing.Size(75, 23);
            this.button_resetfilters.TabIndex = 0;
            this.button_resetfilters.Text = "Reset Filter";
            this.button_resetfilters.UseVisualStyleBackColor = true;
            this.button_resetfilters.Click += new System.EventHandler(this.button_resetfilters_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox_setcalleronchange);
            this.panel1.Controls.Add(this.button_close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 37);
            this.panel1.TabIndex = 2;
            // 
            // checkBox_setcalleronchange
            // 
            this.checkBox_setcalleronchange.AutoSize = true;
            this.checkBox_setcalleronchange.Location = new System.Drawing.Point(167, 11);
            this.checkBox_setcalleronchange.Name = "checkBox_setcalleronchange";
            this.checkBox_setcalleronchange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_setcalleronchange.Size = new System.Drawing.Size(124, 17);
            this.checkBox_setcalleronchange.TabIndex = 2;
            this.checkBox_setcalleronchange.Text = "Set caller on change";
            this.checkBox_setcalleronchange.UseVisualStyleBackColor = true;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(297, 7);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 0;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // EnhancedTextBoxHelperList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_filters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "EnhancedTextBoxHelperList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnhancedTextBoxHelperList_FormClosing);
            this.Load += new System.EventHandler(this.EnhancedTextBoxHelperList_Load);
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).EndInit();
            this.panel_filters.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView_main;
        private System.Windows.Forms.Panel panel_filters;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_resetsorting;
        private System.Windows.Forms.Button button_resetfilters;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.CheckBox checkBox_setcalleronchange;
    }
}