#region License
// Copyright (c) 2018 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DG.UI.Helpers
{
    public class EnhancedDateTimePickerUtcHelper
    {
        /// <summary>
        /// Info icon color
        /// </summary>
        public static Color infoiconColor = Color.Peru;

        /// <summary>
        /// List of controls attached
        /// </summary>
        private static IList<DateTimePicker> _attached_DateTimePicker = new List<DateTimePicker>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<DateTimePicker> _attached_ValueChanged = new List<DateTimePicker>();

        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<DateTimePicker> _attached_MouseHover = new List<DateTimePicker>();

        /// <summary>
        /// List of parent controls with attached event
        /// </summary>
        private static IList<Control> _attached_parentMouseMove = new List<Control>();

        /// <summary>
        /// Default conversion format
        /// </summary>
        private static string DefaultFormat = "yyyy/MM/dd HH:mm";

        /// <summary>
        /// ToolTip display
        /// </summary>
        private static ToolTip tooltip = new ToolTip();

        /// <summary>
        /// ToolTip is shown
        /// </summary>
        private static bool tooltipShown = false;

        /// <summary>
        /// Attach the helper to a datetimepicker
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="customFormat"></param>
        /// <param name="labelFullText"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker, string customFormat, Label labelFullText)
        {
            CleanLists();

            if (dateTimePicker == null)
                return;

            if (!_attached_DateTimePicker.Contains(dateTimePicker))
            {
                _attached_DateTimePicker.Add(dateTimePicker);
                dateTimePicker.Parent.Paint += new PaintEventHandler((sender, e) => dateTimePicker_PaintInfoicon(sender, e, dateTimePicker));
            }

            //set formats
            if (String.IsNullOrEmpty(customFormat))
                customFormat = DefaultFormat;

            //set attached label or tooltip
            if (labelFullText != null)
            {
                SetLabelUpdateOnValueChanged(dateTimePicker, customFormat, labelFullText);
                dateTimePicker_ValueChanged(dateTimePicker, null, customFormat, labelFullText);
            }
            else
            {
                SetToolTipOnMouseOver(dateTimePicker, customFormat);
            }
        }

        /// <summary>
        /// Attach the helper to a datetimepicker
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="labelFullText"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker, Label labelFullText)
        {
            AttachDateTimePicker(dateTimePicker, null, labelFullText);
        }

        /// <summary>
        /// Attach the helper to a datetimepicker
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="customFormat"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker, string customFormat)
        {
            AttachDateTimePicker(dateTimePicker, customFormat, null);
        }

        /// <summary>
        /// Attach the helper to a datetimepicker
        /// </summary>
        /// <param name="dateTimePicker"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker)
        {
            AttachDateTimePicker(dateTimePicker, null, null);
        }

        /// <summary>
        /// Remove disposed components from list
        /// </summary>
        public static void CleanLists()
        {
            List<DateTimePicker> dateTimePickerToRemove = new List<DateTimePicker>();
            List<Control> controlToRemove = new List<Control>();

            //clean attached list
            dateTimePickerToRemove = new List<DateTimePicker>();
            foreach (DateTimePicker entry in _attached_DateTimePicker)
            {
                if (entry.IsDisposed)
                    dateTimePickerToRemove.Add(entry);
            }
            foreach (DateTimePicker entry in dateTimePickerToRemove)
                _attached_DateTimePicker.Remove(entry);
            dateTimePickerToRemove = new List<DateTimePicker>();
            foreach (DateTimePicker entry in _attached_MouseHover)
            {
                if (entry.IsDisposed)
                    dateTimePickerToRemove.Add(entry);
            }
            foreach (DateTimePicker entry in dateTimePickerToRemove)
                _attached_MouseHover.Remove(entry);
            foreach (Control entry in _attached_parentMouseMove)
            {
                if (entry.IsDisposed)
                    controlToRemove.Add(entry);
            }
            foreach (Control entry in controlToRemove)
                _attached_parentMouseMove.Remove(entry);
        }

        /// <summary>
        /// Attach the label update on value changed
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="format"></param>
        /// <param name="label"></param>
        private static void SetLabelUpdateOnValueChanged(DateTimePicker dateTimePicker, string format, Label label)
        {
            if (dateTimePicker != null)
            {
                if (!_attached_ValueChanged.Contains(dateTimePicker))
                {
                    _attached_ValueChanged.Add(dateTimePicker);
                    dateTimePicker.ValueChanged += (sender, e) => dateTimePicker_ValueChanged(sender, e, format, label);
                }
            }
        }

        /// <summary>
        /// Attach the show a tooltip on mouse over event
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="format"></param>
        private static void SetToolTipOnMouseOver(DateTimePicker dateTimePicker, string format)
        {
            if (dateTimePicker != null)
            {
                if (dateTimePicker.Parent != null)
                {
                    if (!_attached_parentMouseMove.Contains(dateTimePicker.Parent))
                    {
                        _attached_parentMouseMove.Add(dateTimePicker.Parent);
                        dateTimePicker.Parent.MouseMove += (sender, e) => parentControl_MouseMove(sender, e, format);
                    }
                }

                if (!_attached_MouseHover.Contains(dateTimePicker))
                {
                    _attached_MouseHover.Add(dateTimePicker);
                    dateTimePicker.MouseHover += (sender, e) => dateTimePicker_MouseHover(sender, e, format);
                }
            }
        }

        /// <summary>
        /// Get the converted value to utc text
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private static string GetUtcText(DateTimePicker dateTimePicker, string format)
        {
            //convert to local datetime, force the UTC time kind
            DateTime localDateTime = DateTime.SpecifyKind(dateTimePicker.Value, DateTimeKind.Utc).ToLocalTime();
            //get the utc text
            string utctext = localDateTime.ToLongTimeString();
            try
            {
                utctext = String.Format("{0:" + format + "}", localDateTime);
            }
            catch { }
            return utctext;
        }

        /// <summary>
        /// Show a tooltip
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="format"></param>
        private static void ShowTooltip(DateTimePicker dateTimePicker, string format)
        {
            if (dateTimePicker != null)
            {
                if (!tooltipShown)
                {
                    tooltip.Show(GetUtcText(dateTimePicker, format), dateTimePicker, dateTimePicker.Width / 2, dateTimePicker.Height / 2);
                    tooltip.Tag = dateTimePicker;
                    tooltipShown = true;
                }
                else
                {
                    tooltip.Hide(dateTimePicker);
                    tooltip.Tag = null;
                    tooltipShown = false;
                }
            }
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="format"></param>
        /// <param name="label"></param>
        private static void dateTimePicker_ValueChanged(object sender, EventArgs e, string format, Label label)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)sender;

            if (dateTimePicker != null)
            {
                label.Text = GetUtcText(dateTimePicker, format);
            }
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="format"></param>
        private static void dateTimePicker_MouseHover(object sender, EventArgs e, string format)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)sender;

            if (dateTimePicker != null)
            {
                ShowTooltip(dateTimePicker, format);
            }
        }

        /// <summary>
        /// Default attached event on parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="format"></param>
        private static void parentControl_MouseMove(object sender, MouseEventArgs e, string format)
        {
            foreach (DateTimePicker entry in _attached_DateTimePicker)
            {
                if (entry.Parent == sender)
                {
                    Control control = ((Control)sender).GetChildAtPoint(e.Location);
                    if (control != null)
                    {
                        if (control == entry)
                        {
                            ShowTooltip(entry, format);
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
        /// <param name="dateTimePicker"></param>
        private static void dateTimePicker_PaintInfoicon(object sender, PaintEventArgs e, DateTimePicker dateTimePicker)
        {
            if (!dateTimePicker.Visible)
                return;

            if (dateTimePicker.Parent != null)
            {
                using (Graphics g = dateTimePicker.Parent.CreateGraphics())
                {
                    Pen pen = new Pen(infoiconColor, 1);
                    g.DrawLine(pen, dateTimePicker.Location.X + dateTimePicker.Width - 10, dateTimePicker.Location.Y + dateTimePicker.Height, dateTimePicker.Location.X + dateTimePicker.Width, dateTimePicker.Location.Y + dateTimePicker.Height);
                    g.DrawLine(pen, dateTimePicker.Location.X + dateTimePicker.Width, dateTimePicker.Location.Y + dateTimePicker.Height, dateTimePicker.Location.X + dateTimePicker.Width, dateTimePicker.Location.Y + dateTimePicker.Height - 10);
                }
            }
        }
    }
}
