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
    public class EnhancedTextBoxHelper
    {
        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<TextBox> _attachedList_KeyDown = new List<TextBox>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IDictionary<TextBox, MouseEventHandler> _attached_MouseDownEvents = new Dictionary<TextBox, MouseEventHandler>();

        /// <summary>
        /// Info icon color
        /// </summary>
        private static Color _infoiconColor = Color.MediumAquamarine;

        /// <summary>
        /// The datasource element for the textbox
        /// </summary>
        public class Items
        {
            public string _value { get; set; }
            public string[] _values { get; set; }
            public Nullable<bool> _isshown { get; set; }
        }

        /// <summary>
        /// Deselect on ESC
        /// </summary>
        /// <param name="textBox"></param>
        public static void DeselectOnEsc(TextBox textBox)
        {
            if (textBox != null)
            {
                if (!_attachedList_KeyDown.Contains(textBox))
                {
                    _attachedList_KeyDown.Add(textBox);
                    textBox.KeyDown += textBox_KeyDown;
                }
            }
        }

        /// <summary>
        /// Show list on right click
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        /// <param name="drawInfoicon"></param>
        public static void ShowListOnRightClick(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc, bool drawInfoicon)
        {
            if (textBox != null && items != null)
            {
                if (items.Length > 0)
                {
                    if (!_attached_MouseDownEvents.ContainsKey(textBox))
                    {
                        _attached_MouseDownEvents.Add(textBox, new MouseEventHandler((sender, e) => textBox_MouseDown(sender, e, items, columnHeaders, viewMode, postSetFunc)));
                        if (drawInfoicon)
                            textBox.Parent.Paint += new PaintEventHandler((sender, e) => textBox_PaintInfoicon(sender, e, textBox));
                    }
                    else
                    {
                        textBox.MouseDown -= _attached_MouseDownEvents[textBox];
                        _attached_MouseDownEvents[textBox] = new MouseEventHandler((sender, e) => textBox_MouseDown(sender, e, items, columnHeaders, viewMode, postSetFunc));
                    }
                    textBox.MouseDown += _attached_MouseDownEvents[textBox];
                }
            }
        }

        /// <summary>
        /// Set autocomplete on textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="items"></param>
        public static void SetAutoComplete(TextBox textBox, EnhancedTextBoxHelper.Items[] items)
        {
            if (textBox != null && items != null)
            {
                AutoCompleteStringCollection autoCompleteCustomSource = new AutoCompleteStringCollection();
                foreach (EnhancedTextBoxHelper.Items item in items)
                {
                    autoCompleteCustomSource.Add(item._value);
                }
                textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = autoCompleteCustomSource;
            }
        }

        /// <summary>
        /// Attach the helper to a textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, bool enableDeselectOnEsc, bool enableShowListOnRightclick, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            if (enableDeselectOnEsc)
                DeselectOnEsc(textBox);

            if (enableShowListOnRightclick && columnHeaders != null)
                ShowListOnRightClick(textBox, columnHeaders, items, viewMode, postSetFunc, true);

            SetAutoComplete(textBox, items);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, bool enableDeselectOnEsc, bool enableShowListOnRightclick, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachTextBox(textBox, enableDeselectOnEsc, enableShowListOnRightclick, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachTextBox(TextBox textBox, bool enableDeselectOnEsc, bool enableShowListOnRightclick, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items)
        {
            AttachTextBox(textBox, enableDeselectOnEsc, enableShowListOnRightclick, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, null);
        }

        /// <summary>
        /// Attach the helper to a textbox, set the view mode, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            AttachTextBox(textBox, true, true, columnHeaders, items, viewMode, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a textbox, set the view mode, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, EnhancedTextBoxHelperList.ViewMode viewMode)
        {
            AttachTextBox(textBox, true, true, columnHeaders, items, viewMode, null);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachTextBox(textBox, true, true, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change, deselect enabled, showlist enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items)
        {
            AttachTextBox(textBox, true, true, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, null);
        }

        /// <summary>
        /// Clear combobox selection on ESC press
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        public static void DeselectOnKeyDown(TextBox textBox, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox.Text = null;
            }
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            DeselectOnKeyDown((TextBox)sender, e);
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
        private static void textBox_MouseDown(object sender, MouseEventArgs e, EnhancedTextBoxHelper.Items[] items, string[] columnHeaders, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (((TextBox)sender).Enabled && !((TextBox)sender).ReadOnly)
                {
                    ((TextBox)sender).Focus();
                    EnhancedTextBoxHelperList enhancedTextBoxHelperList = new EnhancedTextBoxHelperList((TextBox)sender, columnHeaders, items, ((TextBox)sender).Text, viewMode, postSetFunc);
                    enhancedTextBoxHelperList.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Paint an information icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="textBox"></param>
        private static void textBox_PaintInfoicon(object sender, PaintEventArgs e, TextBox textBox)
        {
            using (Graphics g = textBox.Parent.CreateGraphics())
            {
                Pen pen = new Pen(_infoiconColor, 1);
                g.DrawLine(pen, textBox.Location.X + textBox.Width - 10, textBox.Location.Y - 1, textBox.Location.X + textBox.Width, textBox.Location.Y - 1);
                g.DrawLine(pen, textBox.Location.X + textBox.Width, textBox.Location.Y, textBox.Location.X + textBox.Width, textBox.Location.Y + 10);
            }
        }
    }
}
