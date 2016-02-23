#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DG.UI.Helpers
{
    public class EnhancedTextBoxHelper
    {
        /// <summary>
        /// Info icon color
        /// </summary>
        public static Color infoiconColor = Color.MediumAquamarine;

        /// <summary>
        /// List of controls attached
        /// </summary>
        private static IDictionary<TextBox, EnhancedTextBoxHelper.Items[]> _attached_TextBox = new Dictionary<TextBox, EnhancedTextBoxHelper.Items[]>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<TextBox> _attached_KeyDown = new List<TextBox>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IDictionary<TextBox, MouseEventHandler> _attached_MouseDown = new Dictionary<TextBox, MouseEventHandler>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<TextBox> _attached_MouseHover = new List<TextBox>();

        /// <summary>
        /// List of parent controls with attached event
        /// </summary>
        private static IList<Control> _attached_parentMouseMove = new List<Control>();

        /// <summary>
        /// ToolTip display
        /// </summary>
        private static ToolTip tooltip = new ToolTip();

        /// <summary>
        /// ToolTip is shown
        /// </summary>
        private static bool tooltipShown = false;

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
        /// Attach the helper to a textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="enableTooltip"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, bool enableDeselectOnEsc, bool enableShowListOnRightclick, bool enableTooltip, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            UpdateItems(textBox, items, true);

            if (enableDeselectOnEsc)
                SetDeselectOnEsc(textBox);

            if (enableShowListOnRightclick && columnHeaders != null)
                SetShowListOnRightClick(textBox, columnHeaders, viewMode, postSetFunc, true);

            if (enableTooltip)
                SetToolTipOnMouseOver(textBox);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="enableTooltip"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, bool enableDeselectOnEsc, bool enableShowListOnRightclick, bool enableTooltip, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachTextBox(textBox, enableDeselectOnEsc, enableShowListOnRightclick, enableTooltip, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="enableTooltip"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachTextBox(TextBox textBox, bool enableDeselectOnEsc, bool enableShowListOnRightclick, bool enableTooltip, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items)
        {
            AttachTextBox(textBox, enableDeselectOnEsc, enableShowListOnRightclick, enableTooltip, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, null);
        }

        /// <summary>
        /// Attach the helper to a textbox, set the view mode, deselect enabled, showlist enabled, tooltip enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            AttachTextBox(textBox, true, true, true, columnHeaders, items, viewMode, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a textbox, set the view mode, deselect enabled, showlist enabled, tooltip enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, EnhancedTextBoxHelperList.ViewMode viewMode)
        {
            AttachTextBox(textBox, true, true, true, columnHeaders, items, viewMode, null);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change, deselect enabled, showlist enabled, tooltip enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachTextBox(textBox, true, true, true, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a textbox, view mode set caller on change, deselect enabled, showlist enabled, tooltip enabled
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachTextBox(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelper.Items[] items)
        {
            AttachTextBox(textBox, true, true, true, columnHeaders, items, EnhancedTextBoxHelperList.ViewMode.SelectOnChange, null);
        }

        /// <summary>
        /// Set/Update AutoComplete on textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="items"></param>
        public static void UpdateAutoComplete(TextBox textBox, EnhancedTextBoxHelper.Items[] items)
        {
            if (textBox != null && items != null)
            {
                AutoCompleteStringCollection autoCompleteCustomSource = new AutoCompleteStringCollection();
                foreach (EnhancedTextBoxHelper.Items item in items)
                    autoCompleteCustomSource.Add(item._value);
                textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = autoCompleteCustomSource;
            }
        }

        /// <summary>
        /// Update items
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="items"></param>
        /// <param name="updateAutoComplete"></param>
        public static void UpdateItems(TextBox textBox, EnhancedTextBoxHelper.Items[] items, bool updateAutoComplete)
        {
            if (!_attached_TextBox.ContainsKey(textBox))
                _attached_TextBox.Add(textBox, items);
            else
                _attached_TextBox[textBox] = items;

            if (updateAutoComplete)
                UpdateAutoComplete(textBox, items);
        }

        /// <summary>
        /// Update items and set/update autocomplete
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="items"></param>
        public static void UpdateItems(TextBox textBox, EnhancedTextBoxHelper.Items[] items)
        {
            UpdateItems(textBox, items, true);
        }

        /// <summary>
        /// Deselect on ESC
        /// </summary>
        /// <param name="textBox"></param>
        public static void SetDeselectOnEsc(TextBox textBox)
        {
            if (textBox != null)
            {
                if (!_attached_KeyDown.Contains(textBox))
                {
                    _attached_KeyDown.Add(textBox);
                    textBox.KeyDown += textBox_KeyDown;
                }
            }
        }

        /// <summary>
        /// Attache the show list on right click event
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        /// <param name="drawInfoicon"></param>
        private static void SetShowListOnRightClick(TextBox textBox, string[] columnHeaders, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc, bool drawInfoicon)
        {
            if (textBox != null)
            {
                if (!_attached_MouseDown.ContainsKey(textBox))
                {
                    _attached_MouseDown.Add(textBox, new MouseEventHandler((sender, e) => textBox_MouseDown(sender, e, columnHeaders, viewMode, postSetFunc)));
                    if (drawInfoicon && textBox.Visible)
                        textBox.Parent.Paint += new PaintEventHandler((sender, e) => textBox_PaintInfoicon(sender, e, textBox));
                }
                else
                {
                    textBox.MouseDown -= _attached_MouseDown[textBox];
                    _attached_MouseDown[textBox] = new MouseEventHandler((sender, e) => textBox_MouseDown(sender, e, columnHeaders, viewMode, postSetFunc));
                }
                textBox.MouseDown += _attached_MouseDown[textBox];
            }
        }

        /// <summary>
        /// Attach the show a tooltip on mouse over event
        /// </summary>
        /// <param name="textBox"></param>
        private static void SetToolTipOnMouseOver(TextBox textBox)
        {
            if (textBox != null)
            {
                if (!_attached_parentMouseMove.Contains(textBox.Parent))
                {
                    _attached_parentMouseMove.Add(textBox.Parent);
                    textBox.Parent.MouseMove += parentControl_MouseMove;
                }

                if (!_attached_MouseHover.Contains(textBox))
                {
                    _attached_MouseHover.Add(textBox);
                    textBox.MouseHover += textBox_MouseHover;
                }
            }
        }

        /// <summary>
        /// Show a tooltip
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="items"></param>
        private static void ShowTooltip(TextBox textBox, EnhancedTextBoxHelper.Items[] items)
        {
            if (textBox != null)
            {
                if (!String.IsNullOrEmpty(textBox.Text))
                {
                    EnhancedTextBoxHelper.Items item = items.FirstOrDefault(r => r._value == textBox.Text);
                    if (item != null)
                    {
                        if (item._values != null)
                        {
                            string tooltipText = "";
                            foreach (string value in item._values)
                                tooltipText += value + " ";

                            if (textBox.Visible && tooltip.Tag == null)
                            {
                                if (!tooltipShown)
                                {
                                    tooltip.Show(tooltipText, textBox, textBox.Width / 2, textBox.Height / 2);
                                    tooltip.Tag = textBox;
                                    tooltipShown = true;
                                }
                                else
                                {
                                    tooltip.Hide(textBox);
                                    tooltip.Tag = null;
                                    tooltipShown = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get attached items
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        private static EnhancedTextBoxHelper.Items[] GetItems(TextBox textBox)
        {
            EnhancedTextBoxHelper.Items[] ret = null;
            foreach (KeyValuePair<TextBox, EnhancedTextBoxHelper.Items[]> entry in _attached_TextBox)
            {
                if (entry.Key == textBox)
                {
                    ret = entry.Value;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            //clear textbox selection on ESC press
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
        /// <param name="columnHeaders"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        private static void textBox_MouseDown(object sender, MouseEventArgs e, string[] columnHeaders, EnhancedTextBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            if (e.Button == MouseButtons.Right)
            {
                TextBox textBox = (TextBox)sender;
                EnhancedTextBoxHelper.Items[] items = GetItems(textBox);

                if (textBox != null && items != null)
                {
                    if (textBox.Enabled && !textBox.ReadOnly)
                    {
                        textBox.Focus();
                        EnhancedTextBoxHelperList enhancedTextBoxHelperList = new EnhancedTextBoxHelperList(textBox, columnHeaders, items, textBox.Text, viewMode, postSetFunc);
                        enhancedTextBoxHelperList.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void textBox_MouseHover(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            EnhancedTextBoxHelper.Items[] items = GetItems(textBox);

            if (textBox != null && items != null)
            {
                ShowTooltip(textBox, items);
            }
        }

        /// <summary>
        /// Default attached event on parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void parentControl_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (KeyValuePair<TextBox, EnhancedTextBoxHelper.Items[]> entry in _attached_TextBox)
            {
                if (entry.Key.Parent == sender)
                {
                    Control control = ((Control)sender).GetChildAtPoint(e.Location);
                    if (control != null)
                    {
                        if (control == entry.Key)
                        {
                            ShowTooltip(entry.Key, entry.Value);
                        }
                    }
                    else
                    {
                        control = (Control)tooltip.Tag;
                        if (control != null)
                        {
                            tooltip.Hide(control);
                            tooltip.Tag = null;
                            tooltipShown = false;
                        }
                    }
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
            if (!textBox.Visible)
                return;

            using (Graphics g = textBox.Parent.CreateGraphics())
            {
                Pen pen = new Pen(infoiconColor, 1);
                g.DrawLine(pen, textBox.Location.X + textBox.Width - 10, textBox.Location.Y - 1, textBox.Location.X + textBox.Width, textBox.Location.Y - 1);
                g.DrawLine(pen, textBox.Location.X + textBox.Width, textBox.Location.Y, textBox.Location.X + textBox.Width, textBox.Location.Y + 10);
            }
        }

    }
}
