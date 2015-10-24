#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DG.UI.Helpers
{
    public class EnhancedComboBoxHelper
    {
        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<ComboBox> _attachedList_KeyPress = new List<ComboBox>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<ComboBox> _attachedList_KeyDown = new List<ComboBox>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IDictionary<ComboBox, MouseEventHandler> _attached_MouseDownEvents = new Dictionary<ComboBox, MouseEventHandler>();

        /// <summary>
        /// Info icon color
        /// </summary>
        private static Color _infoiconColor = Color.MediumAquamarine;

        /// <summary>
        /// The datasource element for the combobox
        /// </summary>
        public class Items
        {
            public object _id { get; set; }
            public string _value { get; set; }
            public string[] _values { get; set; }
            public Nullable<bool> _isshown { get; set; }
        }

        /// <summary>
        /// Autocomplete on Enter press
        /// </summary>
        /// <param name="comboBox"></param>
        public static void AutoCompleteOnEnter(ComboBox comboBox)
        {
            if (comboBox != null)
            {
                if (!_attachedList_KeyPress.Contains(comboBox))
                {
                    _attachedList_KeyPress.Add(comboBox);
                    comboBox.KeyPress += comboBox_KeyPress;

                    if (comboBox.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                        comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }
            }
        }

        /// <summary>
        /// Deselect on ESC
        /// </summary>
        /// <param name="comboBox"></param>
        public static void DeselectOnEsc(ComboBox comboBox)
        {
            if (comboBox != null)
            {
                if (!_attachedList_KeyDown.Contains(comboBox))
                {
                    _attachedList_KeyDown.Add(comboBox);
                    comboBox.KeyDown += comboBox_KeyDown;
                }
            }
        }

        /// <summary>
        /// Show list on right click
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        /// <param name="drawInfoicon"></param>
        public static void ShowListOnRightClick(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, EnhancedComboBoxHelperList.ViewMode viewMode, Action postSetFunc, bool drawInfoicon)
        {
            if (comboBox != null && items != null)
            {
                if (items.Length > 0)
                {
                    if (!_attached_MouseDownEvents.ContainsKey(comboBox))
                    {
                        _attached_MouseDownEvents.Add(comboBox, new MouseEventHandler((sender, e) => comboBox_MouseDown(sender, e, items, columnHeaders, viewMode, postSetFunc)));
                        if (drawInfoicon)
                            comboBox.Parent.Paint += new PaintEventHandler((sender, e) => comboBox_PaintInfoicon(sender, e, comboBox));
                    }
                    else
                    {
                        comboBox.MouseDown -= _attached_MouseDownEvents[comboBox];
                        _attached_MouseDownEvents[comboBox] = new MouseEventHandler((sender, e) => comboBox_MouseDown(sender, e, items, columnHeaders, viewMode, postSetFunc));
                    }
                    comboBox.MouseDown += _attached_MouseDownEvents[comboBox];
                }
            }
        }

        /// <summary>
        /// Set DataSource
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="items"></param>
        public static void SetDatasource(ComboBox comboBox, EnhancedComboBoxHelper.Items[] items)
        {
            if (comboBox != null && items != null)
            {
                comboBox.DataSource = items;
                comboBox.ValueMember = "_id";
                comboBox.DisplayMember = "_value";
                comboBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Attach the helper to a combobox
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="enableAutoCompleteOnEnter"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachComboBox(ComboBox comboBox, bool enableAutoCompleteOnEnter, bool enableDeselectOnEsc, bool enableShowListOnRightclick, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, EnhancedComboBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            if (enableAutoCompleteOnEnter)
                AutoCompleteOnEnter(comboBox);

            if (enableDeselectOnEsc)
                DeselectOnEsc(comboBox);

            if (enableShowListOnRightclick && columnHeaders != null)
                ShowListOnRightClick(comboBox, columnHeaders, items, viewMode, postSetFunc, true);

            SetDatasource(comboBox, items);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="enableAutoCompleteOnEnter"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachComboBox(ComboBox comboBox, bool enableAutoCompleteOnEnter, bool enableDeselectOnEsc, bool enableShowListOnRightclick, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachComboBox(comboBox, enableAutoCompleteOnEnter, enableDeselectOnEsc, enableShowListOnRightclick, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="enableAutoCompleteOnEnter"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachComboBox(ComboBox comboBox, bool enableAutoCompleteOnEnter, bool enableDeselectOnEsc, bool enableShowListOnRightclick, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items)
        {
            AttachComboBox(comboBox, enableAutoCompleteOnEnter, enableDeselectOnEsc, enableShowListOnRightclick, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, null);
        }

        /// <summary>
        /// Attach the helper to a combobox, set the view mode, autocomplete enabled, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachComboBox(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, EnhancedComboBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            AttachComboBox(comboBox, true, true, true, columnHeaders, items, viewMode, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a combobox, set the view mode, autocomplete enabled, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        public static void AttachComboBox(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, EnhancedComboBoxHelperList.ViewMode viewMode)
        {
            AttachComboBox(comboBox, true, true, true, columnHeaders, items, viewMode, null);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change, autocomplete enabled, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachComboBox(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachComboBox(comboBox, true, true, true, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change, autocomplete enabled, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachComboBox(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items)
        {
            AttachComboBox(comboBox, true, true, true, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, null);
        }

        /// <summary>
        /// Autocomplete a combobox on keypress
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="e"></param>
        public static void AutoCompleteOnKeyPress(ComboBox comboBox, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;
            string stringToFind = comboBox.Text.Substring(0, comboBox.SelectionStart) + e.KeyChar;
            int index = comboBox.FindStringExact(stringToFind);
            if (index == -1)
                index = comboBox.FindString(stringToFind);
            if (index == -1)
                return;
            comboBox.SelectedIndex = index;
            comboBox.SelectionStart = stringToFind.Length;
            comboBox.SelectionLength = comboBox.Text.Length - comboBox.SelectionStart;
            e.Handled = true;
        }

        /// <summary>
        /// Clear combobox selection on ESC press
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="e"></param>
        public static void DeselectOnKeyDown(ComboBox comboBox, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoCompleteOnKeyPress((ComboBox)sender, e);
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            DeselectOnKeyDown((ComboBox)sender, e);
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="items"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        private static void comboBox_MouseDown(object sender, MouseEventArgs e, EnhancedComboBoxHelper.Items[] items, string[] columnHeaders, EnhancedComboBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (((ComboBox)sender).Enabled)
                {
                    ((ComboBox)sender).Focus();
                    EnhancedComboBoxHelperList enhancedComboBoxHelperList = new EnhancedComboBoxHelperList((ComboBox)sender, columnHeaders, items, ((ComboBox)sender).SelectedValue, viewMode, postSetFunc);
                    enhancedComboBoxHelperList.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Paint an information icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="comboBox"></param>
        private static void comboBox_PaintInfoicon(object sender, PaintEventArgs e, ComboBox comboBox)
        {
            using (Graphics g = comboBox.Parent.CreateGraphics())
            {
                Pen pen = new Pen(_infoiconColor, 1);
                g.DrawLine(pen, comboBox.Location.X + comboBox.Width - 10, comboBox.Location.Y - 1, comboBox.Location.X + comboBox.Width, comboBox.Location.Y - 1);
                g.DrawLine(pen, comboBox.Location.X + comboBox.Width, comboBox.Location.Y, comboBox.Location.X + comboBox.Width, comboBox.Location.Y + 10);
            }
        }

    }
}
