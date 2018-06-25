#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DG.UI.Helpers
{
    public partial class EnhancedTextBoxHelperList : Form
    {
        /// <summary>
        /// View modes
        /// </summary>
        public enum ViewMode { View, SelectOnChange, SelectOnDoubleClick };

        /// <summary>
        /// View mode
        /// </summary>
        private ViewMode _viewMode = ViewMode.SelectOnChange;

        /// <summary>
        /// Loaded columns
        /// </summary>
        private string[] _columnHeaders = new string[] { };

        /// <summary>
        /// Loaded values
        /// </summary>
        private EnhancedTextBoxHelper.Items[] _items = new EnhancedTextBoxHelper.Items[] { };

        /// <summary>
        /// Main datatable
        /// </summary>
        private DataTable _datatable = new DataTable();

        /// <summary>
        /// Main bindingsource
        /// </summary>
        private BindingSource _bindingSource = new BindingSource();

        /// <summary>
        /// Loading bindingsource
        /// </summary>
        private bool _isBindingSourceLoading = false;

        /// <summary>
        /// Selected id
        /// </summary>
        private object _selectedId = null;

        /// <summary>
        /// TextBox
        /// </summary>
        private TextBox _textBox = null;

        /// <summary>
        /// Post set function
        /// </summary>
        private Action _postSetFunc = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="selectedValue"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        public EnhancedTextBoxHelperList(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, object selectedValue, ViewMode viewMode, Action postSetFunc)
        {
            InitializeComponent();

            this._columnHeaders = columnHeaders;
            this._items = items;
            this._selectedId = selectedValue;
            this._textBox = textBox;
            this._viewMode = viewMode;
            this._postSetFunc = postSetFunc;

            //set title
            if (_viewMode == ViewMode.View)
                this.Text = "View";
            else
                this.Text = "Select";

            //set caller on change
            if (_viewMode == ViewMode.View)
            {
                checkBox_setcalleronchange.Checked = false;
                checkBox_setcalleronchange.Enabled = false;
            }
            else if (_viewMode == ViewMode.SelectOnChange)
            {
                checkBox_setcalleronchange.Checked = true;
            }
            else if (_viewMode == ViewMode.SelectOnDoubleClick)
            {
                checkBox_setcalleronchange.Checked = false;
            }

            //set change referred item mode
            _bindingSource.CurrentChanged += _bindingSource_CurrentChanged;

            //check items consistency
            int valuescount = 0;
            if (items != null && _items.Count() > 0)
            {
                //validate values
                valuescount = items[0]._values.Count();
                foreach (EnhancedTextBoxHelper.Items item in _items)
                {
                    if (item._values.Count() != valuescount)
                    {
                        _items = null;
                        break;
                    }
                }
            }

            //check column headers consistency
            if (_items != null)
            {
                //get max values
                int maxvaluescount = 0;
                foreach (EnhancedTextBoxHelper.Items item in _items)
                {
                    if (item._values != null)
                    {
                        if (item._values.Count() > maxvaluescount)
                        {
                            maxvaluescount = item._values.Count();
                        }
                    }
                }
                //set columns header if null
                if (_columnHeaders == null || _columnHeaders.Length != maxvaluescount)
                {
                    _columnHeaders = new string[] { };
                    for (int i = 1; i <= maxvaluescount; i++)
                        _columnHeaders = _columnHeaders.Concat(new string[] { (i).ToString() }).ToArray();
                }
                //check duplicates
                Dictionary<string, int> columnHeadersCounts = new Dictionary<string, int>();
                foreach (string columnHeader in _columnHeaders)
                {
                    if (columnHeadersCounts.ContainsKey(columnHeader))
                        columnHeadersCounts[columnHeader]++;
                    else
                        columnHeadersCounts.Add(columnHeader, 1);
                }
                List<string> output = new List<string>();
                foreach (KeyValuePair<string, int> pair in columnHeadersCounts)
                {
                    if (pair.Value > 1)
                    {
                        for (int i = 1; i <= pair.Value; i++)
                            output.Add(pair.Key + "-" + i);
                    }
                    else
                        output.Add(pair.Key);
                }
                _columnHeaders = output.ToArray();
            }

            //set filter search hanlder
            advancedDataGridViewSearchToolBar_main.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(advancedDataGridViewSearchToolBar_main_Search);

            //set binding source
            advancedDataGridView_main.DataSource = _bindingSource;
        }

        /// <summary>
        /// Loader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnhancedTextBoxHelperList_Load(object sender, EventArgs e)
        {
            if (_items == null)
            {
                MessageBox.Show("Error loading items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _datatable = new DataTable();

            //add columns
            bool addvalues = false;
            try
            {
                _datatable.Columns.Add("_value", typeof(string));
                foreach (string column in _columnHeaders)
                {
                    _datatable.Columns.Add(column, typeof(string));
                }
                addvalues = true;
            }
            catch { }

            //add values
            if (addvalues)
            {
                foreach (EnhancedTextBoxHelper.Items item in _items)
                {
                    bool insertitem = true;
                    if (item._isshown != null && !(bool)item._isshown)
                        insertitem = false;
                    if (insertitem)
                    {
                        DataRow row = _datatable.NewRow();
                        row["_value"] = item._value;
                        if (item._values != null)
                        {
                            for (int i = 0; i < item._values.Count(); i++)
                            {
                                row[_columnHeaders[i]] = (item._values[i] != null ? item._values[i].ToString() : "");
                            }
                        }
                        _datatable.Rows.Add(row);
                    }
                }
            }

            //set bindingsource
            _isBindingSourceLoading = true;
            _bindingSource.DataSource = _datatable;

            //set sort
            if (_columnHeaders.Length > 0)
                advancedDataGridView_main.SortASC(advancedDataGridView_main.Columns[1]);

            //set id visibility
            advancedDataGridView_main.Columns[0].Visible = false;

            //set filter
            advancedDataGridViewSearchToolBar_main.SetColumns(advancedDataGridView_main.Columns);

            _isBindingSourceLoading = false;

            //select current item
            if (_selectedId != null)
                _bindingSource.Position = _bindingSource.Find("_value", _selectedId.ToString());
        }

        /// <summary>
        /// Closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnhancedTextBoxHelperList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkBox_setcalleronchange.Checked)
                SetCaller();
        }

        /// <summary>
        /// Current index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (_isBindingSourceLoading)
                return;

            if (checkBox_setcalleronchange.Checked)
                SetCaller();
        }

        /// <summary>
        /// Set the caller
        /// </summary>
        private void SetCaller()
        {
            try
            {
                _selectedId = (((DataRowView)_bindingSource.Current).Row).Field<string>("_value");
                _textBox.Text = _selectedId.ToString();
            }
            catch
            {
                _textBox.Text = "";
            }

            if (_postSetFunc != null)
                _postSetFunc();
        }

        /// <summary>
        /// Close current form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedDataGridView_main_DoubleClick(object sender, EventArgs e)
        {
            if (_viewMode == ViewMode.SelectOnDoubleClick)
                checkBox_setcalleronchange.Checked = true;
            this.Close();
        }

        /// <summary>
        /// Reset filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_resetfilters_Click(object sender, EventArgs e)
        {
            advancedDataGridView_main.CleanFilter();
        }

        /// <summary>
        /// Reset sorting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_resetsorting_Click(object sender, EventArgs e)
        {
            advancedDataGridView_main.CleanSort();
            if (_columnHeaders.Length > 0)
                advancedDataGridView_main.SortASC(advancedDataGridView_main.Columns[1]);
        }

        /// <summary>
        /// Filter search handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedDataGridViewSearchToolBar_main_Search(object sender, Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = advancedDataGridView_main.CurrentCell.ColumnIndex + 1 >= advancedDataGridView_main.ColumnCount;
                bool endrow = advancedDataGridView_main.CurrentCell.RowIndex + 1 >= advancedDataGridView_main.RowCount;

                if (endcol && endrow)
                {
                    startColumn = advancedDataGridView_main.CurrentCell.ColumnIndex;
                    startRow = advancedDataGridView_main.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : advancedDataGridView_main.CurrentCell.ColumnIndex + 1;
                    startRow = advancedDataGridView_main.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = advancedDataGridView_main.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = advancedDataGridView_main.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                advancedDataGridView_main.CurrentCell = c;
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
