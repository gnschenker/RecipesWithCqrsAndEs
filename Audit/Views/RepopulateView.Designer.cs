namespace Audit.Views
{
    partial class RepopulateView
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepopulateView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._rebuild = new System.Windows.Forms.ToolStripButton();
            this._rebuildAll = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this._testRebuild = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._rebuild,
            this._rebuildAll,
            this._testRebuild});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(544, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _rebuild
            // 
            this._rebuild.Enabled = false;
            this._rebuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._rebuild.Name = "_rebuild";
            this._rebuild.Size = new System.Drawing.Size(51, 22);
            this._rebuild.Text = "Rebuild";
            this._rebuild.Click += new System.EventHandler(this.RebuildClick);
            // 
            // _rebuildAll
            // 
            this._rebuildAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._rebuildAll.Image = ((System.Drawing.Image)(resources.GetObject("_rebuildAll.Image")));
            this._rebuildAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._rebuildAll.Name = "_rebuildAll";
            this._rebuildAll.Size = new System.Drawing.Size(68, 22);
            this._rebuildAll.Text = "Rebuild All";
            this._rebuildAll.Click += new System.EventHandler(this.RebuildAllClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(544, 439);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(181, 439);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "mail_16.png");
            this.imageList1.Images.SetKeyName(1, "document");
            this.imageList1.Images.SetKeyName(2, "message-event");
            this.imageList1.Images.SetKeyName(3, "folder");
            this.imageList1.Images.SetKeyName(4, "upgrade");
            this.imageList1.Images.SetKeyName(5, "handler");
            this.imageList1.Images.SetKeyName(6, "view-singleton");
            this.imageList1.Images.SetKeyName(7, "view-entity");
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(359, 439);
            this.textBox1.TabIndex = 0;
            // 
            // _testRebuild
            // 
            this._testRebuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._testRebuild.Image = ((System.Drawing.Image)(resources.GetObject("_testRebuild.Image")));
            this._testRebuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._testRebuild.Name = "_testRebuild";
            this._testRebuild.Size = new System.Drawing.Size(85, 22);
            this._testRebuild.Text = "Rebuild Test...";
            this._testRebuild.Click += new System.EventHandler(this._testRebuild_Click);
            // 
            // RepopulateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RepopulateView";
            this.Size = new System.Drawing.Size(544, 464);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton _rebuild;
        private System.Windows.Forms.ToolStripButton _rebuildAll;
        private System.Windows.Forms.ToolStripButton _testRebuild;

    }
}
