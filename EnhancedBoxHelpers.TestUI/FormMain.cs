#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DG.UI.Helpers
{
    public partial class FormMain : Form
    {
        public struct User
        {
            public int id;
            public string name;
            public string address;
        }

        private List<User> _users = new List<User>();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Random rand = new Random();

            //generate random users
            for (int i = 0; i < 100; i++)
            {
                _users.Add(new User()
                {
                    id = i + 1,
                    name = BuildRandomString(rand.Next(5, 15)),
                    address = BuildRandomString(rand.Next(20, 40))
                });
                Thread.Sleep(10);
            }

            //attach components

            EnhancedComboBoxHelper.AttachComboBox(
                comboBox1,
                new string[] { "Name", "Address" },
                _users.ToList().OrderBy(r => r.name).Select(
                r => new EnhancedComboBoxHelper.Items()
                {
                    _id = r.id,
                    _value = r.name,
                    _values = new string[]
                                {
                                    r.name,
                                    r.address
                                }
                }).ToArray());

            EnhancedComboBoxHelper.AttachComboBox(
                comboBox2,
                new string[] { "Name", "Address" },
                _users.ToList().OrderBy(r => r.name).Select(
                r => new EnhancedComboBoxHelper.Items()
                {
                    _id = r.id,
                    _value = r.name,
                    _values = new string[]
                    {
                        r.name,
                        r.address
                    }
                }).ToArray(),
                EnhancedComboBoxHelperList.ViewMode.SelectOnDoubleClick,
                () => MessageBox.Show(comboBox2.SelectedValue.ToString()));

            EnhancedTextBoxHelper.AttachTextBox(
                textBox1,
                new string[] { "Name", "Address" },
                _users.ToList().OrderBy(r => r.name).Select(
                r => new EnhancedTextBoxHelper.Items()
                {
                    _value = r.name,
                    _values = new string[]
                    {
                        r.name,
                        r.address
                    }
                }).ToArray());

            EnhancedTextBoxHelper.AttachTextBox(
                textBox2,
                new string[] { "Name", "Address" },
                _users.ToList().OrderBy(r => r.name).Select(
                r => new EnhancedTextBoxHelper.Items()
                {
                    _value = r.name,
                    _values = new string[]
                    {
                        r.name,
                        r.address
                    }
                }).ToArray(),
                EnhancedTextBoxHelperList.ViewMode.SelectOnDoubleClick,
                () => MessageBox.Show(textBox2.Text.ToString()));
        }

        /// <summary>
        /// Build a random string of a given size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string BuildRandomString(int size)
        {
            Random r = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                //26 letters in the alfabet, ascii + 65 for the capital letters
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65))));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Close click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
