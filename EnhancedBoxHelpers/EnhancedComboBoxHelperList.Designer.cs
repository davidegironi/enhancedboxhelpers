namespace DG.UI.Helpers
{
    partial class EnhancedComboBoxHelperList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel_main = new System.Windows.Forms.Panel();
            this.advancedDataGridView_main = new Zuby.ADGV.AdvancedDataGridView();
            this.panel_filters = new System.Windows.Forms.Panel();
            this.panel_filters_grid = new System.Windows.Forms.Panel();
            this.advancedDataGridViewSearchToolBar_main = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.button_resetsorting = new System.Windows.Forms.Button();
            this.button_resetfilters = new System.Windows.Forms.Button();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.checkBox_setcalleronchange = new System.Windows.Forms.CheckBox();
            this.button_close = new System.Windows.Forms.Button();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).BeginInit();
            this.panel_filters.SuspendLayout();
            this.panel_filters_grid.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.advancedDataGridView_main);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 65);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(534, 214);
            this.panel_main.TabIndex = 0;
            // 
            // advancedDataGridView_main
            // 
            this.advancedDataGridView_main.AllowUserToAddRows = false;
            this.advancedDataGridView_main.AllowUserToDeleteRows = false;
            this.advancedDataGridView_main.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.advancedDataGridView_main.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.advancedDataGridView_main.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.advancedDataGridView_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.advancedDataGridView_main.DefaultCellStyle = dataGridViewCellStyle4;
            this.advancedDataGridView_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView_main.FilterAndSortEnabled = true;
            this.advancedDataGridView_main.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridView_main.MultiSelect = false;
            this.advancedDataGridView_main.Name = "advancedDataGridView_main";
            this.advancedDataGridView_main.ReadOnly = true;
            this.advancedDataGridView_main.RowHeadersVisible = false;
            this.advancedDataGridView_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.advancedDataGridView_main.Size = new System.Drawing.Size(534, 214);
            this.advancedDataGridView_main.TabIndex = 2;
            this.advancedDataGridView_main.SortStringChanged += new System.EventHandler(this.advancedDataGridView_main_SortStringChanged);
            this.advancedDataGridView_main.FilterStringChanged += new System.EventHandler(this.advancedDataGridView_main_FilterStringChanged);
            this.advancedDataGridView_main.DoubleClick += new System.EventHandler(this.advancedDataGridView_main_DoubleClick);
            // 
            // panel_filters
            // 
            this.panel_filters.Controls.Add(this.panel_filters_grid);
            this.panel_filters.Controls.Add(this.button_resetsorting);
            this.panel_filters.Controls.Add(this.button_resetfilters);
            this.panel_filters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_filters.Location = new System.Drawing.Point(0, 0);
            this.panel_filters.Name = "panel_filters";
            this.panel_filters.Size = new System.Drawing.Size(534, 65);
            this.panel_filters.TabIndex = 1;
            // 
            // panel_filters_grid
            // 
            this.panel_filters_grid.Controls.Add(this.advancedDataGridViewSearchToolBar_main);
            this.panel_filters_grid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_filters_grid.Location = new System.Drawing.Point(0, 37);
            this.panel_filters_grid.Name = "panel_filters_grid";
            this.panel_filters_grid.Size = new System.Drawing.Size(534, 28);
            this.panel_filters_grid.TabIndex = 2;
            // 
            // advancedDataGridViewSearchToolBar_main
            // 
            this.advancedDataGridViewSearchToolBar_main.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_main.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridViewSearchToolBar_main.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_main.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_main.Name = "advancedDataGridViewSearchToolBar_main";
            this.advancedDataGridViewSearchToolBar_main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_main.Size = new System.Drawing.Size(534, 27);
            this.advancedDataGridViewSearchToolBar_main.TabIndex = 0;
            this.advancedDataGridViewSearchToolBar_main.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_main.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_main_Search);
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
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.checkBox_setcalleronchange);
            this.panel_bottom.Controls.Add(this.button_close);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 279);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(534, 37);
            this.panel_bottom.TabIndex = 2;
            // 
            // checkBox_setcalleronchange
            // 
            this.checkBox_setcalleronchange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_setcalleronchange.AutoSize = true;
            this.checkBox_setcalleronchange.Location = new System.Drawing.Point(317, 11);
            this.checkBox_setcalleronchange.Name = "checkBox_setcalleronchange";
            this.checkBox_setcalleronchange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_setcalleronchange.Size = new System.Drawing.Size(124, 17);
            this.checkBox_setcalleronchange.TabIndex = 1;
            this.checkBox_setcalleronchange.Text = "Set caller on change";
            this.checkBox_setcalleronchange.UseVisualStyleBackColor = true;
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.Location = new System.Drawing.Point(447, 7);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 0;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // EnhancedComboBoxHelperList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 316);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_filters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 350);
            this.Name = "EnhancedComboBoxHelperList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnhancedComboBoxHelperList_FormClosing);
            this.Load += new System.EventHandler(this.EnhancedComboBoxHelperList_Load);
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).EndInit();
            this.panel_filters.ResumeLayout(false);
            this.panel_filters_grid.ResumeLayout(false);
            this.panel_filters_grid.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView_main;
        private System.Windows.Forms.Panel panel_filters;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Button button_resetsorting;
        private System.Windows.Forms.Button button_resetfilters;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.CheckBox checkBox_setcalleronchange;
        private System.Windows.Forms.Panel panel_filters_grid;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_main;
    }
}