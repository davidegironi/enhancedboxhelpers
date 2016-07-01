#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DG.UI.Helpers
{
    public class EnhancedDateTimePickerHelper
    {
        /// <summary>
        /// List of controls with attached event
        /// </summary>
        private static IList<DateTimePicker> _attachedList_KeyDown = new List<DateTimePicker>();

        /// <summary>
        /// Attach keydown event
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="firstDayOfWeek"></param>
        public static void AttachKeyDown(DateTimePicker dateTimePicker, DayOfWeek firstDayOfWeek)
        {
            if (dateTimePicker != null)
            {
                if (!_attachedList_KeyDown.Contains(dateTimePicker))
                {
                    _attachedList_KeyDown.Add(dateTimePicker);
                    dateTimePicker.KeyDown += (sender, e) => dateTimePicker_KeyDown(sender, e, firstDayOfWeek);
                }
            }
        }

        /// <summary>
        /// Attach the helper to a textbox
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="customFormat"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <param name="enableFastKeys"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker, string customFormat, DayOfWeek firstDayOfWeek, bool enableFastKeys)
        {
            //enable fast key
            if (enableFastKeys)
            {
                AttachKeyDown(dateTimePicker, firstDayOfWeek);
            }

            //set custom format
            if (!String.IsNullOrEmpty(customFormat))
            {
                dateTimePicker.CustomFormat = customFormat;
                dateTimePicker.Format = DateTimePickerFormat.Custom;
            }
        }

        /// <summary>
        /// Attach the helper to a textbox
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="customFormat"></param>
        /// <param name="firstDayOfWeek"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker, string customFormat, DayOfWeek firstDayOfWeek)
        {
            AttachDateTimePicker(dateTimePicker, customFormat, firstDayOfWeek, true);

        }
        /// <summary>
        /// Attach the helper to a textbox
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="customFormat"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker, string customFormat)
        {
            AttachDateTimePicker(dateTimePicker, customFormat, DayOfWeek.Monday, true);
        }

        /// <summary>
        /// Attach the helper to a textbox
        /// </summary>
        /// <param name="dateTimePicker"></param>
        public static void AttachDateTimePicker(DateTimePicker dateTimePicker)
        {
            AttachDateTimePicker(dateTimePicker, null, DayOfWeek.Monday, true);
        }

        /// <summary>
        /// Set now datetime on n or N press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        public static void NowOnNKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Modifiers != Keys.Control)
            {
                dateTimePicker.Value = DateTime.Now;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set today date on t or T press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        public static void TodayOnTKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T && e.Modifiers != Keys.Control)
            {
                dateTimePicker.Value = DateTime.Now.Date;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set today date on d or D press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        public static void TodayOnDKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D && e.Modifiers != Keys.Control)
            {
                dateTimePicker.Value = DateTime.Now.Date;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set first day of week on W press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <param name="date"></param>
        public static void FirstDayOfWeekOnMKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e, DayOfWeek firstDayOfWeek, DateTime date)
        {
            if (e.KeyCode == Keys.W && e.Modifiers != Keys.Control && e.Modifiers != Keys.Shift)
            {
                dateTimePicker.Value = date.AddDays(firstDayOfWeek - date.DayOfWeek).Date;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set last day of week on w press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <param name="date"></param>
        public static void LastDayOfWeekOnMKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e, DayOfWeek firstDayOfWeek, DateTime date)
        {
            if (e.KeyCode == Keys.W && e.Modifiers != Keys.Control && e.Modifiers == Keys.Shift)
            {
                dateTimePicker.Value = date.AddDays(firstDayOfWeek - date.DayOfWeek).Date.AddDays(6);
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set first day of month on M press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        /// <param name="date"></param>
        public static void FirstDayOfMonthOnMKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e, DateTime date)
        {
            if (e.KeyCode == Keys.M && e.Modifiers != Keys.Control && e.Modifiers != Keys.Shift)
            {
                dateTimePicker.Value = new DateTime(date.Year, date.Month, 1).Date;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set last day of month on M press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        /// <param name="date"></param>
        public static void LastDayOfMonthOnMKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e, DateTime date)
        {
            if (e.KeyCode == Keys.M && e.Modifiers != Keys.Control && e.Modifiers == Keys.Shift)
            {
                dateTimePicker.Value = new DateTime(date.Year, date.Month + 1, 1).Date.AddDays(-1);
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set first day of year on y press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        /// <param name="date"></param>
        public static void FirstDayOfYearOnYKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e, DateTime date)
        {
            if (e.KeyCode == Keys.Y && e.Modifiers != Keys.Control && e.Modifiers != Keys.Shift)
            {
                dateTimePicker.Value = new DateTime(date.Year, 1, 1).Date;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Set last day of year on Y press
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        /// <param name="date"></param>
        public static void LastDayOfYearOnYKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e, DateTime date)
        {
            if (e.KeyCode == Keys.Y && e.Modifiers != Keys.Control && e.Modifiers == Keys.Shift)
            {
                dateTimePicker.Value = new DateTime(date.Year + 1, 1, 1).Date.AddDays(-1);
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Go next on / pressed
        /// </summary>
        /// <param name="dateTimePicker"></param>
        /// <param name="e"></param>
        public static void SelectNetOnOemQuestionKeyDown(DateTimePicker dateTimePicker, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemQuestion)
            {
                SendKeys.Send("{Right}");
            }
        }

        /// <summary>
        /// Default attached event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void dateTimePicker_KeyDown(object sender, KeyEventArgs e, DayOfWeek firstDayOfWeek)
        {
            DateTime now = DateTime.Now;
            NowOnNKeyDown((DateTimePicker)sender, e);
            TodayOnTKeyDown((DateTimePicker)sender, e);
            TodayOnDKeyDown((DateTimePicker)sender, e);
            FirstDayOfWeekOnMKeyDown((DateTimePicker)sender, e, firstDayOfWeek, now);
            LastDayOfWeekOnMKeyDown((DateTimePicker)sender, e, firstDayOfWeek, now);
            FirstDayOfMonthOnMKeyDown((DateTimePicker)sender, e, now);
            LastDayOfMonthOnMKeyDown((DateTimePicker)sender, e, now);
            FirstDayOfYearOnYKeyDown((DateTimePicker)sender, e, now);
            LastDayOfYearOnYKeyDown((DateTimePicker)sender, e, now);
            SelectNetOnOemQuestionKeyDown((DateTimePicker)sender, e);
        }

        /// <summary>
        /// Set a from to date
        /// </summary>
        public class FilterDateHelper
        {
            /// <summary>
            /// From selector
            /// </summary>
            private DateTimePicker _fromPicker = null;

            /// <summary>
            /// To selector
            /// </summary>
            private DateTimePicker _toPicker = null;

            /// <summary>
            /// Interval selector
            /// </summary>
            private ComboBox _intervalSelector = null;

            /// <summary>
            /// Reload action
            /// </summary>
            private Action _reloadAction = null;

            /// <summary>
            /// Max days between selectors
            /// </summary>
            private int _maxDaysInterval = 365;

            /// <summary>
            /// Skip a value changed
            /// </summary>
            private bool _skipValueChanged = false;

            /// <summary>
            /// From selector last value
            /// </summary>
            private DateTime _fromPickerLastValue = DateTime.Now;

            /// <summary>
            /// To selector last value
            /// </summary>
            private DateTime _toPickerLastValue = DateTime.Now;

            /// <summary>
            /// Do reload after a change in date
            /// </summary>
            public bool ReloadAfterChange { get; set; }

            /// <summary>
            /// Set the from picker default value
            /// </summary>
            public enum FromPickerDefaultValue { DoNotChange, Today, FirstDayOfWeek, FirstDayOfMonth, FirstDayOfYear }

            /// <summary>
            /// Set the to picker default value
            /// </summary>
            public enum ToPickerDefaultValue { DoNotChange, FromPickerSameDay, FromPickerPlus1W, FromPickerPlus1M, FromPickerPlus3M, FromPickerPlus6M, FromPickerPlus1Y }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="fromPicker"></param>
            /// <param name="toPicker"></param>
            /// <param name="reloadAction"></param>
            /// <param name="intervalSelector"></param>
            /// <param name="maxDaysInterval"></param>
            /// <param name="fromPickerDefaultValue"></param>
            /// <param name="toPickerDefaultValue"></param>
            public FilterDateHelper(DateTimePicker fromPicker, DateTimePicker toPicker, Action reloadAction, ComboBox intervalSelector, int maxDaysInterval, DayOfWeek firstDayOfWeek, FromPickerDefaultValue fromPickerDefaultValue, ToPickerDefaultValue toPickerDefaultValue)
            {
                this._fromPicker = fromPicker;
                this._toPicker = toPicker;
                this._reloadAction = reloadAction;
                this._intervalSelector = intervalSelector;
                this._maxDaysInterval = maxDaysInterval;

                //set reload after change
                ReloadAfterChange = true;

                //now
                DateTime now = DateTime.Now;

                //set from picker
                if (fromPickerDefaultValue == FromPickerDefaultValue.Today)
                    _fromPicker.Value = DateTime.Now.Date;
                else if (fromPickerDefaultValue == FromPickerDefaultValue.FirstDayOfWeek)
                    _fromPicker.Value = now.AddDays(firstDayOfWeek - now.DayOfWeek).Date;
                else if (fromPickerDefaultValue == FromPickerDefaultValue.FirstDayOfMonth)
                    _fromPicker.Value = new DateTime(now.Year, now.Month, 1);
                else if (fromPickerDefaultValue == FromPickerDefaultValue.FirstDayOfYear)
                    _fromPicker.Value = new DateTime(now.Year, 1, 1);

                //set to picker
                if (toPickerDefaultValue == ToPickerDefaultValue.FromPickerSameDay)
                    _toPicker.Value = new DateTime(_fromPicker.Value.Year, _fromPicker.Value.Month, _fromPicker.Value.Day).Date.AddDays(1).AddMilliseconds(-1);
                else if (toPickerDefaultValue == ToPickerDefaultValue.FromPickerPlus1W)
                    _toPicker.Value = new DateTime(_fromPicker.Value.Year, _fromPicker.Value.Month, _fromPicker.Value.Day).Date.AddDays(7).AddMilliseconds(-1);
                else if (toPickerDefaultValue == ToPickerDefaultValue.FromPickerPlus1M)
                    _toPicker.Value = new DateTime(_fromPicker.Value.Year, _fromPicker.Value.Month, _fromPicker.Value.Day).Date.AddMonths(1).AddMilliseconds(-1);
                else if (toPickerDefaultValue == ToPickerDefaultValue.FromPickerPlus3M)
                    _toPicker.Value = new DateTime(_fromPicker.Value.Year, _fromPicker.Value.Month, _fromPicker.Value.Day).Date.AddMonths(3).AddMilliseconds(-1);
                else if (toPickerDefaultValue == ToPickerDefaultValue.FromPickerPlus6M)
                    _toPicker.Value = new DateTime(_fromPicker.Value.Year, _fromPicker.Value.Month, _fromPicker.Value.Day).Date.AddMonths(6).AddMilliseconds(-1);
                else if (toPickerDefaultValue == ToPickerDefaultValue.FromPickerPlus1Y)
                    _toPicker.Value = new DateTime(_fromPicker.Value.Year, _fromPicker.Value.Month, _fromPicker.Value.Day).Date.AddYears(1).AddMilliseconds(-1);

                //check max interval
                if ((_toPicker.Value - _fromPicker.Value).TotalDays > _maxDaysInterval)
                    _toPicker.Value = _fromPicker.Value.Date.AddDays(_maxDaysInterval).Date.AddDays(1).AddMilliseconds(-1);

                //set last value
                _fromPickerLastValue = _fromPicker.Value;
                _toPickerLastValue = _toPicker.Value;

                //attach event handlers
                _fromPicker.ValueChanged += new System.EventHandler(fromPicker_ValueChanged);
                _fromPicker.Leave += fromPicker_Leave;
                _toPicker.ValueChanged += new System.EventHandler(toPicker_ValueChanged);
                _toPicker.Leave += toPicker_Leave;
                if (_intervalSelector != null)
                    _intervalSelector.SelectedIndexChanged += new System.EventHandler(intervalSelector_SelectedIndexChanged);
                _fromPicker.KeyDown += (sender, e) => fromDateTimePicker_KeyDown(sender, e, _toPicker);

                //set reload after change
                ReloadAfterChange = true;
            }

            /// <summary>
            /// Call the value changed action
            /// </summary>
            private void valueChanged()
            {
                if (ReloadAfterChange)
                {
                    if (_reloadAction != null)
                    {
                        _reloadAction();
                    }
                }
            }

            /// <summary>
            /// From picker control leaved event handler
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void fromPicker_Leave(object sender, EventArgs e)
            {
                if (_skipValueChanged)
                    return;

                _skipValueChanged = true;
                _fromPicker.Value = _fromPicker.Value.Date;
                if (_toPicker.Value.Date < _fromPicker.Value.Date)
                    _toPicker.Value = _fromPicker.Value.Date.AddDays(1).AddMilliseconds(-1);
                _skipValueChanged = false;

                //check max interval
                if ((_toPicker.Value - _fromPicker.Value).TotalDays > _maxDaysInterval)
                    _toPicker.Value = _fromPicker.Value.Date.AddDays(_maxDaysInterval).Date.AddDays(1).AddMilliseconds(-1);

                valueChanged();
            }

            /// <summary>
            /// To picker control leaved event handler
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void toPicker_Leave(object sender, EventArgs e)
            {
                if (_skipValueChanged)
                    return;

                _skipValueChanged = true;
                _toPicker.Value = _toPicker.Value.Date;
                if (_toPicker.Value.Date < _fromPicker.Value.Date)
                    _fromPicker.Value = _toPicker.Value.Date;
                _skipValueChanged = false;

                //check max interval
                if ((_toPicker.Value - _fromPicker.Value).TotalDays > _maxDaysInterval)
                    _fromPicker.Value = _toPicker.Value.Date.AddDays(-_maxDaysInterval).Date;

                valueChanged();
            }

            /// <summary>
            /// From picker value changed event handler
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void fromPicker_ValueChanged(object sender, EventArgs e)
            {
                if (_fromPicker.Focused)
                    return;

                fromPicker_Leave(sender, e);
            }

            /// <summary>
            /// To picker value changed event handler
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void toPicker_ValueChanged(object sender, EventArgs e)
            {
                if (_toPicker.Focused)
                    return;

                toPicker_Leave(sender, e);
            }

            /// <summary>
            /// Keydown from picker attached event
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <param name="toDateTimePicker"></param>
            private void fromDateTimePicker_KeyDown(object sender, KeyEventArgs e, DateTimePicker toDateTimePicker)
            {
                _skipValueChanged = true;

                DateTime fromDate = ((DateTimePicker)sender).Value;
                if (e.KeyCode == Keys.D && e.Modifiers == Keys.Control)
                {
                    toDateTimePicker.Value = fromDate.Date.AddDays(1).AddMilliseconds(-1);
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.W && e.Modifiers == Keys.Control)
                {
                    toDateTimePicker.Value = fromDate.Date.AddDays(7).AddMilliseconds(-1);
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.M && e.Modifiers == Keys.Control)
                {
                    toDateTimePicker.Value = fromDate.Date.AddMonths(1).AddMilliseconds(-1);
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Y && e.Modifiers == Keys.Control)
                {
                    toDateTimePicker.Value = fromDate.Date.AddYears(1).AddMilliseconds(-1);
                    e.SuppressKeyPress = true;
                }

                _skipValueChanged = false;

                valueChanged();
            }

            /// <summary>
            /// Interval selector value changed event handler
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void intervalSelector_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (_skipValueChanged)
                    return;

                if (_intervalSelector.SelectedItem == null)
                {
                    _intervalSelector.SelectedIndex = -1;
                    return;
                }

                _skipValueChanged = true;

                try
                {
                    Match matchd = (new Regex(@"^(?<digit>[+|-]?\d*)d$")).Match(_intervalSelector.SelectedItem.ToString());
                    Match matchw = (new Regex(@"^(?<digit>[+|-]?\d*)w$")).Match(_intervalSelector.SelectedItem.ToString());
                    Match matchm = (new Regex(@"^(?<digit>[+|-]?\d*)m$")).Match(_intervalSelector.SelectedItem.ToString());
                    Match matchy = (new Regex(@"^(?<digit>[+|-]?\d*)y$")).Match(_intervalSelector.SelectedItem.ToString());
                    if (matchd.Success)
                    {
                        int days = Convert.ToInt32(matchd.Groups[1].Value);
                        _fromPicker.Value = _fromPicker.Value.Date;
                        _toPicker.Value = _fromPicker.Value.Date.AddDays(days).AddMilliseconds(-1);
                    }
                    else if (matchw.Success)
                    {
                        int weeks = Convert.ToInt32(matchw.Groups[1].Value);
                        _fromPicker.Value = _fromPicker.Value.Date;
                        _toPicker.Value = _fromPicker.Value.Date.AddDays(7 * weeks).AddMilliseconds(-1);
                    }
                    else if (matchm.Success)
                    {
                        int months = Convert.ToInt32(matchm.Groups[1].Value);
                        _fromPicker.Value = _fromPicker.Value.Date;
                        _toPicker.Value = _fromPicker.Value.Date.AddMonths(months).AddMilliseconds(-1);
                    }
                    else if (matchy.Success)
                    {
                        int years = Convert.ToInt32(matchy.Groups[1].Value);
                        _fromPicker.Value = _fromPicker.Value.Date;
                        _toPicker.Value = _fromPicker.Value.Date.AddYears(years).AddMilliseconds(-1);
                    }
                }
                catch { }

                //check max interval
                if ((_toPicker.Value - _fromPicker.Value).TotalDays > _maxDaysInterval)
                    _toPicker.Value = _fromPicker.Value.Date.AddDays(_maxDaysInterval).Date.AddDays(1).AddMilliseconds(-1);

                _intervalSelector.SelectedIndex = -1;

                _skipValueChanged = false;

                valueChanged();
            }
        }
    }
}
