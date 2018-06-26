#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace DG.UI.Helpers
{
    public class EnhancedComboBoxHelper
    {
        /// <summary>
        /// Info icon color
        /// </summary>
        public static Color infoiconColor = Color.MediumAquamarine;

        /// <summary>
        /// List of controls attached
        /// </summary>
        private static IDictionary<ComboBox, EnhancedComboBoxHelper.Items[]> _attached_ComboBox = new Dictionary<ComboBox, EnhancedComboBoxHelper.Items[]>();

        /// <summary>
        /// List of controls with attached bindingsource
        /// </summary>
        private static IDictionary<ComboBox, BindingSource> _attached_BindingSource = new Dictionary<ComboBox, BindingSource>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<ComboBox> _attached_KeyPress = new List<ComboBox>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<ComboBox> _attached_KeyDown = new List<ComboBox>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IDictionary<ComboBox, MouseEventHandler> _attached_MouseDown = new Dictionary<ComboBox, MouseEventHandler>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<ComboBox> _attached_MouseHover = new List<ComboBox>();

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
        public static void AttachComboBox(ComboBox comboBox, bool enableAutoCompleteOnEnter, bool enableDeselectOnEsc, bool enableShowListOnRightclick, bool enableTooltip, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, EnhancedComboBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            CleanLists();

            if (comboBox == null)
                return;

            UpdateItems(comboBox, items, true);

            if (enableAutoCompleteOnEnter)
                SetAutoCompleteOnEnter(comboBox);

            if (enableDeselectOnEsc)
                SetDeselectOnEsc(comboBox);

            if (enableShowListOnRightclick && columnHeaders != null)
                SetShowListOnRightClick(comboBox, columnHeaders, viewMode, postSetFunc, true);

            if (enableTooltip)
                SetToolTipOnMouseOver(comboBox);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="enableAutoCompleteOnEnter"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="enableTooltip"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachComboBox(ComboBox comboBox, bool enableAutoCompleteOnEnter, bool enableDeselectOnEsc, bool enableShowListOnRightclick, bool enableTooltip, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachComboBox(comboBox, enableAutoCompleteOnEnter, enableDeselectOnEsc, enableShowListOnRightclick, enableTooltip, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="enableAutoCompleteOnEnter"></param>
        /// <param name="enableDeselectOnEsc"></param>
        /// <param name="enableShowListOnRightclick"></param>
        /// <param name="enableTooltip"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachComboBox(ComboBox comboBox, bool enableAutoCompleteOnEnter, bool enableDeselectOnEsc, bool enableShowListOnRightclick, bool enableTooltip, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items)
        {
            AttachComboBox(comboBox, enableAutoCompleteOnEnter, enableDeselectOnEsc, enableShowListOnRightclick, enableTooltip, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, null);
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
            AttachComboBox(comboBox, true, true, true, true, columnHeaders, items, viewMode, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a combobox, set the view mode, autocomplete enabled, deselect enabled, showlist enabled, tooltip enabled
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="viewMode"></param>
        public static void AttachComboBox(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, EnhancedComboBoxHelperList.ViewMode viewMode)
        {
            AttachComboBox(comboBox, true, true, true, true, columnHeaders, items, viewMode, null);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change, autocomplete enabled, deselect enabled, showlist enabled, tooltip enabled
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        /// <param name="postSetFunc"></param>
        public static void AttachComboBox(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items, Action postSetFunc)
        {
            AttachComboBox(comboBox, true, true, true, true, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, postSetFunc);
        }

        /// <summary>
        /// Attach the helper to a combobox, view mode set caller on change, autocomplete enabled, deselect enabled, showlist enabled, tooltip enabled
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="items"></param>
        public static void AttachComboBox(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelper.Items[] items)
        {
            AttachComboBox(comboBox, true, true, true, true, columnHeaders, items, EnhancedComboBoxHelperList.ViewMode.SelectOnChange, null);
        }

        /// <summary>
        /// Remove disposed components from list
        /// </summary>
        public static void CleanLists()
        {
            List<ComboBox> comboBoxToRemove = new List<ComboBox>();
            List<Control> controlToRemove = new List<Control>();

            //clean attached list
            comboBoxToRemove = new List<ComboBox>();
            foreach (KeyValuePair<ComboBox, EnhancedComboBoxHelper.Items[]> entry in _attached_ComboBox)
            {
                if (entry.Key.IsDisposed)
                    comboBoxToRemove.Add(entry.Key);
            }
            foreach (ComboBox entry in comboBoxToRemove)
            {
                EventHandlerList events = typeof(ComboBox).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(entry, null) as EventHandlerList;
                object current = events.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField)[0].GetValue(events);
                List<Delegate> delegates = new List<Delegate>();
                while (current != null)
                {
                    delegates.Add((Delegate)(current.GetType().GetField("handler", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField).GetValue(current)));
                    current = current.GetType().GetField("next", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField).GetValue(current);
                }
                EventInfo[] eventinfol = typeof(ComboBox).GetEvents();
                foreach (Type eventinfotype in eventinfol.Select(r => r.EventHandlerType).Distinct())
                {
                    foreach (Delegate d in delegates.Where(r => r != null && r.GetType() == eventinfotype))
                    {
                        foreach (EventInfo eventinfo in eventinfol.Where(r => r.EventHandlerType == eventinfotype))
                            eventinfo.RemoveEventHandler(entry, d);
                    }
                }
            }
            foreach (ComboBox entry in comboBoxToRemove)
                entry.DataSource = null;
            foreach (ComboBox entry in comboBoxToRemove)
                _attached_ComboBox.Remove(entry);
            comboBoxToRemove = new List<ComboBox>();
            foreach (KeyValuePair<ComboBox, BindingSource> entry in _attached_BindingSource)
            {
                if (entry.Key.IsDisposed)
                    comboBoxToRemove.Add(entry.Key);
            }
            foreach (ComboBox entry in comboBoxToRemove)
                _attached_BindingSource.Remove(entry);
            comboBoxToRemove = new List<ComboBox>();
            foreach (ComboBox entry in _attached_KeyPress)
            {
                if (entry.IsDisposed)
                    comboBoxToRemove.Add(entry);
            }
            foreach (ComboBox entry in comboBoxToRemove)
                _attached_KeyPress.Remove(entry);
            comboBoxToRemove = new List<ComboBox>();
            foreach (ComboBox entry in _attached_KeyDown)
            {
                if (entry.IsDisposed)
                    comboBoxToRemove.Add(entry);
            }
            foreach (ComboBox entry in comboBoxToRemove)
                _attached_KeyDown.Remove(entry);
            comboBoxToRemove = new List<ComboBox>();
            foreach (KeyValuePair<ComboBox, MouseEventHandler> entry in _attached_MouseDown)
            {
                if (entry.Key.IsDisposed)
                    comboBoxToRemove.Add(entry.Key);
            }
            foreach (ComboBox entry in comboBoxToRemove)
                _attached_MouseDown.Remove(entry);
            comboBoxToRemove = new List<ComboBox>();
            foreach (ComboBox entry in _attached_MouseHover)
            {
                if (entry.IsDisposed)
                    comboBoxToRemove.Add(entry);
            }
            foreach (ComboBox entry in comboBoxToRemove)
                _attached_MouseHover.Remove(entry);
            controlToRemove = new List<Control>();
            foreach (Control entry in _attached_parentMouseMove)
            {
                if (entry.IsDisposed)
                    controlToRemove.Add(entry);
            }
            foreach (Control entry in controlToRemove)
                _attached_parentMouseMove.Remove(entry);
        }

        /// <summary>
        /// Set/Updat DataSource on combobox
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="items"></param>
        /// <param name="resetSelectedIndex"></param>
        public static void UpdateDataSource(ComboBox comboBox, EnhancedComboBoxHelper.Items[] items, bool resetSelectedIndex)
        {
            if (comboBox == null)
                return;

            if (comboBox != null && items != null)
            {
                if (_attached_BindingSource.ContainsKey(comboBox))
                {
                    if (_attached_BindingSource[comboBox] != null)
                    {
                        if (comboBox.DataSource != _attached_BindingSource[comboBox])
                        {
                            comboBox.DataSource = _attached_BindingSource[comboBox];
                        }
                    }
                }
                if (comboBox.ValueMember != "_id")
                    comboBox.ValueMember = "_id";
                if (comboBox.DisplayMember != "_value")
                    comboBox.DisplayMember = "_value";
                if (resetSelectedIndex)
                    comboBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Update items
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="items"></param>
        /// <param name="updateDataSource"></param>
        public static void UpdateItems(ComboBox comboBox, EnhancedComboBoxHelper.Items[] items, bool updateDataSource)
        {
            if (!_attached_ComboBox.ContainsKey(comboBox))
                _attached_ComboBox.Add(comboBox, items);
            else
                _attached_ComboBox[comboBox] = items;

            if (!_attached_BindingSource.ContainsKey(comboBox))
            {
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = items;
                _attached_BindingSource.Add(comboBox, bindingSource);
            }
            else
            {
                if (new JavaScriptSerializer().Serialize(_attached_BindingSource[comboBox].DataSource) != new JavaScriptSerializer().Serialize(items))
                {
                    _attached_BindingSource[comboBox].DataSource = items;
                }
            }

            if (updateDataSource)
                UpdateDataSource(comboBox, items, true);
        }

        /// <summary>
        /// Update items and set/update datasource
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="items"></param>
        public static void UpdateItems(ComboBox comboBox, EnhancedComboBoxHelper.Items[] items)
        {
            UpdateItems(comboBox, items, true);
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
        /// Deselect on ESC
        /// </summary>
        /// <param name="comboBox"></param>
        public static void SetDeselectOnEsc(ComboBox comboBox)
        {
            if (comboBox != null)
            {
                if (!_attached_KeyDown.Contains(comboBox))
                {
                    _attached_KeyDown.Add(comboBox);
                    comboBox.KeyDown += comboBox_KeyDown;
                }
            }
        }

        /// <summary>
        /// Attach the autocomplete on Enter press event
        /// </summary>
        /// <param name="comboBox"></param>
        public static void SetAutoCompleteOnEnter(ComboBox comboBox)
        {
            if (comboBox != null)
            {
                if (!_attached_KeyPress.Contains(comboBox))
                {
                    _attached_KeyPress.Add(comboBox);
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
        /// Attach the show list on right click event
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnHeaders"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        /// <param name="drawInfoicon"></param>
        private static void SetShowListOnRightClick(ComboBox comboBox, string[] columnHeaders, EnhancedComboBoxHelperList.ViewMode viewMode, Action postSetFunc, bool drawInfoicon)
        {
            if (comboBox != null)
            {
                if (!_attached_MouseDown.ContainsKey(comboBox))
                {
                    _attached_MouseDown.Add(comboBox, new MouseEventHandler((sender, e) => comboBox_MouseDown(sender, e, columnHeaders, viewMode, postSetFunc)));
                    if (drawInfoicon && comboBox.Parent != null)
                        comboBox.Parent.Paint += new PaintEventHandler((sender, e) => comboBox_PaintInfoicon(sender, e, comboBox));
                }
                else
                {
                    comboBox.MouseDown -= _attached_MouseDown[comboBox];
                    _attached_MouseDown[comboBox] = new MouseEventHandler((sender, e) => comboBox_MouseDown(sender, e, columnHeaders, viewMode, postSetFunc));
                }
                comboBox.MouseDown += _attached_MouseDown[comboBox];
            }
        }

        /// <summary>
        /// Attach the show a tooltip on mouse over event
        /// </summary>
        /// <param name="comboBox"></param>
        private static void SetToolTipOnMouseOver(ComboBox comboBox)
        {
            if (comboBox != null)
            {
                if (comboBox.Parent != null)
                {
                    if (!_attached_parentMouseMove.Contains(comboBox.Parent))
                    {
                        _attached_parentMouseMove.Add(comboBox.Parent);
                        comboBox.Parent.MouseMove += parentControl_MouseMove;
                    }
                }

                if (!_attached_MouseHover.Contains(comboBox))
                {
                    _attached_MouseHover.Add(comboBox);
                    comboBox.MouseHover += comboBox_MouseHover;
                }
            }
        }

        /// <summary>
        /// Show a tooltip
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="items"></param>
        private static void ShowTooltip(ComboBox comboBox, EnhancedComboBoxHelper.Items[] items)
        {
            if (comboBox != null)
            {
                if (!String.IsNullOrEmpty(comboBox.Text))
                {
                    EnhancedComboBoxHelper.Items item = items.FirstOrDefault(r => r._value == comboBox.Text);
                    if (item != null)
                    {
                        if (item._values != null)
                        {
                            string tooltipText = "";
                            foreach (string value in item._values)
                                tooltipText += value + " ";

                            if (comboBox.Visible && tooltip.Tag == null)
                            {
                                if (!tooltipShown)
                                {
                                    tooltip.Show(tooltipText, comboBox, comboBox.Width / 2, comboBox.Height / 2);
                                    tooltip.Tag = comboBox;
                                    tooltipShown = true;
                                }
                                else
                                {
                                    tooltip.Hide(comboBox);
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
        /// <param name="comboBox"></param>
        /// <returns></returns>
        private static EnhancedComboBoxHelper.Items[] GetItems(ComboBox comboBox)
        {
            EnhancedComboBoxHelper.Items[] ret = null;
            foreach (KeyValuePair<ComboBox, EnhancedComboBoxHelper.Items[]> entry in _attached_ComboBox)
            {
                if (entry.Key == comboBox)
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
        private static void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            AutoCompleteOnKeyPress(comboBox, e);
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

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
        /// <param name="columnHeaders"></param>
        /// <param name="viewMode"></param>
        /// <param name="postSetFunc"></param>
        private static void comboBox_MouseDown(object sender, MouseEventArgs e, string[] columnHeaders, EnhancedComboBoxHelperList.ViewMode viewMode, Action postSetFunc)
        {
            if (e.Button == MouseButtons.Right)
            {
                ComboBox comboBox = (ComboBox)sender;
                EnhancedComboBoxHelper.Items[] items = GetItems(comboBox);

                if (comboBox.Enabled)
                {
                    comboBox.Focus();
                    EnhancedComboBoxHelperList enhancedComboBoxHelperList = new EnhancedComboBoxHelperList(comboBox, columnHeaders, items, comboBox.SelectedValue, viewMode, postSetFunc);
                    enhancedComboBoxHelperList.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void comboBox_MouseHover(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            EnhancedComboBoxHelper.Items[] items = GetItems(comboBox);

            if (comboBox != null && items != null)
            {
                ShowTooltip(comboBox, items);
            }
        }

        /// <summary>
        /// Default attached event on parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void parentControl_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (KeyValuePair<ComboBox, EnhancedComboBoxHelper.Items[]> entry in _attached_ComboBox)
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
                            if (!control.IsDisposed)
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
        /// <param name="comboBox"></param>
        private static void comboBox_PaintInfoicon(object sender, PaintEventArgs e, ComboBox comboBox)
        {
            if (!comboBox.Visible)
                return;

            if (comboBox.Parent != null)
            {
                using (Graphics g = comboBox.Parent.CreateGraphics())
                {
                    Pen pen = new Pen(infoiconColor, 1);
                    g.DrawLine(pen, comboBox.Location.X + comboBox.Width - 10, comboBox.Location.Y - 1, comboBox.Location.X + comboBox.Width, comboBox.Location.Y - 1);
                    g.DrawLine(pen, comboBox.Location.X + comboBox.Width, comboBox.Location.Y, comboBox.Location.X + comboBox.Width, comboBox.Location.Y + 10);
                }
            }
        }

    }
}
