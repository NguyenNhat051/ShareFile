
namespace OpenFile
{
    partial class OpenFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenFile));
            this.notificationLabel = new System.Windows.Forms.Label();
            this.btnOpenfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.fileSaveLocationLabel = new System.Windows.Forms.Label();
            this.exitBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.onlinePCList = new System.Windows.Forms.ListView();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notificationLabel
            // 
            this.notificationLabel.AutoSize = true;
            this.notificationLabel.Location = new System.Drawing.Point(4, 37);
            this.notificationLabel.Name = "notificationLabel";
            this.notificationLabel.Size = new System.Drawing.Size(0, 13);
            this.notificationLabel.TabIndex = 17;
            // 
            // btnOpenfile
            // 
            this.btnOpenfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenfile.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnOpenfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenfile.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenfile.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOpenfile.Location = new System.Drawing.Point(7, 308);
            this.btnOpenfile.Name = "btnOpenfile";
            this.btnOpenfile.Size = new System.Drawing.Size(78, 34);
            this.btnOpenfile.TabIndex = 24;
            this.btnOpenfile.Text = "Open File";
            this.btnOpenfile.UseVisualStyleBackColor = false;
            this.btnOpenfile.Click += new System.EventHandler(this.btnOpenfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(90, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Target Pc\'s IP";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(90, 322);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(100, 20);
            this.ipBox.TabIndex = 26;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDownload.Location = new System.Drawing.Point(373, 306);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(182, 36);
            this.btnDownload.TabIndex = 12;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(373, 54);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(184, 20);
            this.txtFilePath.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "File Path :";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(114, 38);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(0, 13);
            this.infoLabel.TabIndex = 20;
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButton.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.browseButton.Location = new System.Drawing.Point(264, 306);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(109, 35);
            this.browseButton.TabIndex = 30;
            this.browseButton.Text = "Save Location";
            this.browseButton.UseVisualStyleBackColor = false;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // fileSaveLocationLabel
            // 
            this.fileSaveLocationLabel.AutoSize = true;
            this.fileSaveLocationLabel.Location = new System.Drawing.Point(261, 289);
            this.fileSaveLocationLabel.Name = "fileSaveLocationLabel";
            this.fileSaveLocationLabel.Size = new System.Drawing.Size(89, 13);
            this.fileSaveLocationLabel.TabIndex = 31;
            this.fileSaveLocationLabel.Text = "FileSaveLocation";
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.exitBtn.Location = new System.Drawing.Point(412, -1);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(145, 36);
            this.exitBtn.TabIndex = 14;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.stopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopBtn.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.stopBtn.Location = new System.Drawing.Point(138, -1);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(147, 36);
            this.stopBtn.TabIndex = 13;
            this.stopBtn.Text = "Stop Search";
            this.stopBtn.UseVisualStyleBackColor = false;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.startBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startBtn.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.startBtn.Location = new System.Drawing.Point(0, -1);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(144, 36);
            this.startBtn.TabIndex = 12;
            this.startBtn.Text = "Find/Start";
            this.startBtn.UseVisualStyleBackColor = false;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 53);
            this.progressBar1.Maximum = 255;
            this.progressBar1.Minimum = 1;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(262, 21);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 32;
            this.progressBar1.Value = 1;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IP Address";
            this.columnHeader1.Width = 273;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Computer Name";
            this.columnHeader2.Width = 278;
            // 
            // onlinePCList
            // 
            this.onlinePCList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.onlinePCList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.onlinePCList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.onlinePCList.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onlinePCList.FullRowSelect = true;
            this.onlinePCList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.onlinePCList.HideSelection = false;
            this.onlinePCList.HoverSelection = true;
            this.onlinePCList.Location = new System.Drawing.Point(0, 73);
            this.onlinePCList.MultiSelect = false;
            this.onlinePCList.Name = "onlinePCList";
            this.onlinePCList.Size = new System.Drawing.Size(557, 229);
            this.onlinePCList.TabIndex = 14;
            this.onlinePCList.UseCompatibleStateImageBehavior = false;
            this.onlinePCList.View = System.Windows.Forms.View.Details;
            this.onlinePCList.Click += new System.EventHandler(this.onlinePCList_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecrypt.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecrypt.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDecrypt.Location = new System.Drawing.Point(274, -1);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(147, 36);
            this.btnDecrypt.TabIndex = 33;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = false;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // OpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 345);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.fileSaveLocationLabel);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.btnOpenfile);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.notificationLabel);
            this.Controls.Add(this.onlinePCList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenFile";
            this.Text = "OpenFileFromAnotherPC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenFile_FormClosing);
            this.Load += new System.EventHandler(this.OpenFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label notificationLabel;
        private System.Windows.Forms.Button btnOpenfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label fileSaveLocationLabel;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView onlinePCList;
        private System.Windows.Forms.Button btnDecrypt;
    }
}

