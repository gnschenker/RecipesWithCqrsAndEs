namespace Audit.Views
{
    partial class LoadSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadSettingsForm));
            this._ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._file = new System.Windows.Forms.TextBox();
            this._browse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._storage = new System.Windows.Forms.TextBox();
            this._help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._ok.Location = new System.Drawing.Point(383, 118);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 1;
            this._ok.Text = "OK";
            this._ok.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Settings File";
            // 
            // _file
            // 
            this._file.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._file.Enabled = false;
            this._file.Location = new System.Drawing.Point(12, 40);
            this._file.Name = "_file";
            this._file.Size = new System.Drawing.Size(412, 20);
            this._file.TabIndex = 3;
            // 
            // _browse
            // 
            this._browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browse.Location = new System.Drawing.Point(430, 38);
            this._browse.Name = "_browse";
            this._browse.Size = new System.Drawing.Size(28, 23);
            this._browse.TabIndex = 4;
            this._browse.Text = "...";
            this._browse.UseVisualStyleBackColor = true;
            this._browse.Click += new System.EventHandler(this.BrowseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Storage Account";
            // 
            // _storage
            // 
            this._storage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._storage.Enabled = false;
            this._storage.Location = new System.Drawing.Point(12, 84);
            this._storage.Name = "_storage";
            this._storage.Size = new System.Drawing.Size(446, 20);
            this._storage.TabIndex = 3;
            // 
            // _help
            // 
            this._help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._help.Location = new System.Drawing.Point(12, 118);
            this._help.Name = "_help";
            this._help.Size = new System.Drawing.Size(75, 23);
            this._help.TabIndex = 6;
            this._help.Text = "Help...";
            this._help.UseVisualStyleBackColor = true;
            this._help.Click += new System.EventHandler(this._help_Click);
            // 
            // LoadSettingsForm
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 153);
            this.Controls.Add(this._help);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._browse);
            this.Controls.Add(this._storage);
            this.Controls.Add(this._file);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoadSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Starting Maintenance...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _file;
        private System.Windows.Forms.Button _browse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _storage;
        private System.Windows.Forms.Button _help;
    }
}