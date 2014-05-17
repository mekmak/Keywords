using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlashThunderApplication.Interfaces;

namespace FlashThunderApplication
{
    public partial class Form1 : Form, IFileObserver
    {
        private IFlashThunder flashThunder;

        public Form1(IFlashThunder flashThunder)
        {
            this.flashThunder = flashThunder;
            flashThunder.Subscribe(this);
            this.Icon = new Icon(@"ft.ico");
            InitializeComponent();
        }

        public void OnDone(string s)
        {
            labelDirectoryLoading.Text = s;
        }

        public void OnNextFile(string s)
        {
            labelDirectoryLoading.Text = s;
            labelDirectoryLoading.Update();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            Console.Write("Load ");
            Console.WriteLine("[" + textBoxDirectoryName.Text + "]");

            labelDirectoryNameError.ForeColor = Color.Black;
            labelDirectoryNameError.Text = "Loading...";
            labelDirectoryLoading.Text = "";

            labelDirectoryNameError.Update();
            labelDirectoryLoading.Update();

            if (!flashThunder.LoadDirectory(textBoxDirectoryName.Text))
            {
                Console.WriteLine("Error opening directory");
                labelDirectoryNameError.ForeColor = Color.Red;
                labelDirectoryNameError.Text = "Error Loading Directory";
                labelDirectoryNameError.Update();
                return;
            }
            labelDirectoryNameError.Text = "Loaded";
            labelDirectoryNameError.Update();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Search [" + textBoxKeywords.Text + "]");
            labelResultsNotFound.Text = "";

            HashSet<string> resultsSet = flashThunder.Query(textBoxKeywords.Text);
            if (resultsSet == null)
            {
                labelResultsNotFound.Text = "Directory not loaded";
                return;
            }

            List<string> resultsList = resultsSet.ToList();
            listBoxResults.DataSource = resultsList;
            if (resultsList.Count == 0)
            {
                labelResultsNotFound.Text = "None found [" + textBoxKeywords.Text + "]";
            }
            
        }

        private void textBoxDirectoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonLoad_Click(sender, e);
                e.Handled = true;
            }   
        }

        private void textBoxKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void listBoxResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxResults.IndexFromPoint(e.Location);

            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                flashThunder.OpenFile(listBoxResults.SelectedItem.ToString());
            }
        }
    }
}
