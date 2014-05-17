namespace FlashThunderApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelDirectoryName = new System.Windows.Forms.Label();
            this.textBoxDirectoryName = new System.Windows.Forms.TextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.labelKeywords = new System.Windows.Forms.Label();
            this.textBoxKeywords = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelResults = new System.Windows.Forms.Label();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.labelDirectoryNameError = new System.Windows.Forms.Label();
            this.labelResultsNotFound = new System.Windows.Forms.Label();
            this.labelDirectoryLoading = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.lblFilesLoaded = new System.Windows.Forms.Label();
            this.lbxFilesLoaded = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelDirectoryName
            // 
            this.labelDirectoryName.AutoSize = true;
            this.labelDirectoryName.Location = new System.Drawing.Point(12, 9);
            this.labelDirectoryName.Name = "labelDirectoryName";
            this.labelDirectoryName.Size = new System.Drawing.Size(75, 13);
            this.labelDirectoryName.TabIndex = 0;
            this.labelDirectoryName.Text = "Enter directory";
            // 
            // textBoxDirectoryName
            // 
            this.textBoxDirectoryName.Location = new System.Drawing.Point(15, 25);
            this.textBoxDirectoryName.Name = "textBoxDirectoryName";
            this.textBoxDirectoryName.Size = new System.Drawing.Size(544, 20);
            this.textBoxDirectoryName.TabIndex = 1;
            this.textBoxDirectoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDirectoryName_KeyDown);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(646, 25);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // labelKeywords
            // 
            this.labelKeywords.AutoSize = true;
            this.labelKeywords.Location = new System.Drawing.Point(12, 61);
            this.labelKeywords.Name = "labelKeywords";
            this.labelKeywords.Size = new System.Drawing.Size(53, 13);
            this.labelKeywords.TabIndex = 3;
            this.labelKeywords.Text = "Keywords";
            // 
            // textBoxKeywords
            // 
            this.textBoxKeywords.Location = new System.Drawing.Point(15, 77);
            this.textBoxKeywords.Name = "textBoxKeywords";
            this.textBoxKeywords.Size = new System.Drawing.Size(625, 20);
            this.textBoxKeywords.TabIndex = 4;
            this.textBoxKeywords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxKeywords_KeyDown);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(646, 77);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelResults
            // 
            this.labelResults.AutoSize = true;
            this.labelResults.Location = new System.Drawing.Point(12, 110);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(79, 13);
            this.labelResults.TabIndex = 6;
            this.labelResults.Text = "Search Results";
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.Location = new System.Drawing.Point(15, 126);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(703, 160);
            this.listBoxResults.TabIndex = 7;
            this.listBoxResults.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxResults_KeyPress);
            this.listBoxResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxResults_MouseDoubleClick);
            // 
            // labelDirectoryNameError
            // 
            this.labelDirectoryNameError.AutoSize = true;
            this.labelDirectoryNameError.Location = new System.Drawing.Point(85, 307);
            this.labelDirectoryNameError.Name = "labelDirectoryNameError";
            this.labelDirectoryNameError.Size = new System.Drawing.Size(0, 13);
            this.labelDirectoryNameError.TabIndex = 8;
            // 
            // labelResultsNotFound
            // 
            this.labelResultsNotFound.AutoSize = true;
            this.labelResultsNotFound.Location = new System.Drawing.Point(74, 61);
            this.labelResultsNotFound.Name = "labelResultsNotFound";
            this.labelResultsNotFound.Size = new System.Drawing.Size(0, 13);
            this.labelResultsNotFound.TabIndex = 9;
            // 
            // labelDirectoryLoading
            // 
            this.labelDirectoryLoading.AutoSize = true;
            this.labelDirectoryLoading.Location = new System.Drawing.Point(139, 307);
            this.labelDirectoryLoading.Name = "labelDirectoryLoading";
            this.labelDirectoryLoading.Size = new System.Drawing.Size(0, 13);
            this.labelDirectoryLoading.TabIndex = 10;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(565, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblFilesLoaded
            // 
            this.lblFilesLoaded.AutoSize = true;
            this.lblFilesLoaded.Location = new System.Drawing.Point(12, 307);
            this.lblFilesLoaded.Name = "lblFilesLoaded";
            this.lblFilesLoaded.Size = new System.Drawing.Size(67, 13);
            this.lblFilesLoaded.TabIndex = 12;
            this.lblFilesLoaded.Text = "Files Loaded";
            // 
            // lbxFilesLoaded
            // 
            this.lbxFilesLoaded.FormattingEnabled = true;
            this.lbxFilesLoaded.Location = new System.Drawing.Point(15, 323);
            this.lbxFilesLoaded.Name = "lbxFilesLoaded";
            this.lbxFilesLoaded.Size = new System.Drawing.Size(703, 160);
            this.lbxFilesLoaded.TabIndex = 13;
            this.lbxFilesLoaded.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbxFilesLoaded_KeyPress);
            this.lbxFilesLoaded.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxFilesLoaded_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 503);
            this.Controls.Add(this.lbxFilesLoaded);
            this.Controls.Add(this.lblFilesLoaded);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.labelDirectoryLoading);
            this.Controls.Add(this.labelResultsNotFound);
            this.Controls.Add(this.labelDirectoryNameError);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxKeywords);
            this.Controls.Add(this.labelKeywords);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.textBoxDirectoryName);
            this.Controls.Add(this.labelDirectoryName);
            this.Name = "Form1";
            this.Text = "FlashThunder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDirectoryName;
        private System.Windows.Forms.TextBox textBoxDirectoryName;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label labelKeywords;
        private System.Windows.Forms.TextBox textBoxKeywords;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.Label labelDirectoryNameError;
        private System.Windows.Forms.Label labelResultsNotFound;
        private System.Windows.Forms.Label labelDirectoryLoading;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label lblFilesLoaded;
        private System.Windows.Forms.ListBox lbxFilesLoaded;       
    }
}

