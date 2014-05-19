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
using FlashThunderApplication.Context;

namespace FlashThunderApplication
{
    public partial class Form1 : Form, IFileObserver
    {
        private IFlashThunder flashThunder;
        
        /// <summary>
        /// Represents the integer values of various 'key down' actions
        /// </summary>
        public static class KeyValues
        {
            public const int Enter = 13;
        }

        public Form1(IFlashThunder flashThunder)
        {
            this.flashThunder = flashThunder;
            flashThunder.Subscribe(this);
            this.Icon = new Icon(@"ft.ico");
            InitializeComponent();

            textBoxDirectoryName.Text = @"C:\Users\TomekM\Documents\NPS\LnL";
        }

        #region IFileObserver Impl.

        public void OnDone(string s)
        {
            labelDirectoryLoading.Text = s;
        }

        public void OnNextFile(string s)
        {
            labelDirectoryLoading.Text = s;
            labelDirectoryLoading.Update();
        }

        #endregion

        #region Button Event Handlers

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                textBoxDirectoryName.Text = folderBrowserDialog1.SelectedPath;
            }
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

            HashSet<string> allFiles = flashThunder.LoadDirectory(textBoxDirectoryName.Text);
            if (allFiles == null)
            {
                Console.WriteLine("Error opening directory");
                labelDirectoryNameError.ForeColor = Color.Red;
                labelDirectoryNameError.Text = "Error Loading Directory";
                labelDirectoryNameError.Update();
                return;
            }
            labelDirectoryNameError.Text = "Loaded";
            labelDirectoryNameError.Update();

            lbxFilesLoaded.DataSource = allFiles.ToList();
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

        #endregion

        #region Textbox Event Handlers

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

        #endregion

        #region Listbox Event Handlers

        private void listBoxResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBoxResults.IndexFromPoint(e.Location);

            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                flashThunder.OpenFile(listBoxResults.SelectedItem.ToString());
            }
        }

        private void listBoxResults_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == KeyValues.Enter)
            {
                if(listBoxResults.SelectedItem != null)
                {
                    flashThunder.OpenFile(listBoxResults.SelectedItem.ToString());
                }
            }
        }

        private void lbxFilesLoaded_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbxFilesLoaded.IndexFromPoint(e.Location);

            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                flashThunder.OpenFile(lbxFilesLoaded.SelectedItem.ToString());
            }
        }

        #endregion

        private void lbxFilesLoaded_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeyValues.Enter)
            {
                if (lbxFilesLoaded.SelectedItem != null)
                {
                    flashThunder.OpenFile(lbxFilesLoaded.SelectedItem.ToString());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += " " + ContextManager.Instance.AppContext.VersionNumber;
        }        
    }
}
