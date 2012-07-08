namespace Audit.Views
{
    partial class DomainLogView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this._log = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this._display = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._beginningEvents = new System.Windows.Forms.ToolStripButton();
            this._nextEvents = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.resendDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.modifySendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._detailsLabel = new System.Windows.Forms.Label();
            this._domainGrid = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this._domainLogTab = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.viewsTab = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._domainGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this._domainLogTab.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._log);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 319);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 100);
            this.panel1.TabIndex = 0;
            // 
            // _log
            // 
            this._log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._log.Dock = System.Windows.Forms.DockStyle.Fill;
            this._log.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._log.Location = new System.Drawing.Point(0, 0);
            this._log.Name = "_log";
            this._log.ReadOnly = true;
            this._log.Size = new System.Drawing.Size(836, 100);
            this._log.TabIndex = 0;
            this._log.Text = "";
            this._log.WordWrap = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 316);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(836, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // _display
            // 
            this._display.BackColor = System.Drawing.SystemColors.Window;
            this._display.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._display.Dock = System.Windows.Forms.DockStyle.Fill;
            this._display.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._display.Location = new System.Drawing.Point(0, 24);
            this._display.Name = "_display";
            this._display.ReadOnly = true;
            this._display.Size = new System.Drawing.Size(417, 258);
            this._display.TabIndex = 1;
            this._display.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._beginningEvents,
            this._nextEvents,
            this.toolStripSeparator1,
            this.resendDropDownButton,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.txtCount,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(400, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _beginningEvents
            // 
            this._beginningEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._beginningEvents.Name = "_beginningEvents";
            this._beginningEvents.Size = new System.Drawing.Size(77, 22);
            this._beginningEvents.Text = "Load Last";
            this._beginningEvents.Click += new System.EventHandler(this.BeginningEventsClick);
            // 
            // _nextEvents
            // 
            this._nextEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._nextEvents.Name = "_nextEvents";
            this._nextEvents.Size = new System.Drawing.Size(56, 22);
            this._nextEvents.Text = "Older";
            this._nextEvents.Click += new System.EventHandler(this.NextEventsClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // resendDropDownButton
            // 
            this.resendDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifySendMenuItem});
            this.resendDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resendDropDownButton.Name = "resendDropDownButton";
            this.resendDropDownButton.Size = new System.Drawing.Size(121, 22);
            this.resendDropDownButton.Text = "Resend Selected";
            this.resendDropDownButton.Click += new System.EventHandler(this.ResendClick);
            // 
            // modifySendMenuItem
            // 
            this.modifySendMenuItem.Name = "modifySendMenuItem";
            this.modifySendMenuItem.Size = new System.Drawing.Size(154, 22);
            this.modifySendMenuItem.Text = "Modify && Send";
            this.modifySendMenuItem.Click += new System.EventHandler(this.ModifySendMenuItemClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel2.Text = "Count";
            // 
            // txtCount
            // 
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(50, 25);
            this.txtCount.Text = "500";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _detailsLabel
            // 
            this._detailsLabel.BackColor = System.Drawing.Color.DimGray;
            this._detailsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._detailsLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._detailsLabel.Location = new System.Drawing.Point(0, 0);
            this._detailsLabel.Name = "_detailsLabel";
            this._detailsLabel.Size = new System.Drawing.Size(417, 24);
            this._detailsLabel.TabIndex = 1;
            this._detailsLabel.Text = "Details";
            // 
            // _domainGrid
            // 
            this._domainGrid.AllowUserToAddRows = false;
            this._domainGrid.AllowUserToDeleteRows = false;
            this._domainGrid.AllowUserToOrderColumns = true;
            this._domainGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._domainGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._domainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._domainGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this._domainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._domainGrid.Location = new System.Drawing.Point(0, 0);
            this._domainGrid.Name = "_domainGrid";
            this._domainGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._domainGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this._domainGrid.RowHeadersVisible = false;
            this._domainGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._domainGrid.Size = new System.Drawing.Size(398, 257);
            this._domainGrid.TabIndex = 0;
            this._domainGrid.SelectionChanged += new System.EventHandler(this.DataGridView1SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this._display);
            this.panel2.Controls.Add(this._detailsLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(406, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 284);
            this.panel2.TabIndex = 2;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(403, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 284);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this._domainGrid);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 259);
            this.panel3.TabIndex = 4;
            // 
            // _domainLogTab
            // 
            this._domainLogTab.Controls.Add(this.panel2);
            this._domainLogTab.Controls.Add(this.splitter2);
            this._domainLogTab.Controls.Add(this.panel4);
            this._domainLogTab.Location = new System.Drawing.Point(4, 22);
            this._domainLogTab.Name = "_domainLogTab";
            this._domainLogTab.Padding = new System.Windows.Forms.Padding(3);
            this._domainLogTab.Size = new System.Drawing.Size(828, 290);
            this._domainLogTab.TabIndex = 0;
            this._domainLogTab.Text = "Domain Log";
            this._domainLogTab.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.toolStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(400, 284);
            this.panel4.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._domainLogTab);
            this.tabControl1.Controls.Add(this.viewsTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(836, 316);
            this.tabControl1.TabIndex = 2;
            // 
            // viewsTab
            // 
            this.viewsTab.Location = new System.Drawing.Point(4, 22);
            this.viewsTab.Name = "viewsTab";
            this.viewsTab.Padding = new System.Windows.Forms.Padding(3);
            this.viewsTab.Size = new System.Drawing.Size(828, 290);
            this.viewsTab.TabIndex = 1;
            this.viewsTab.Text = "Views";
            this.viewsTab.UseVisualStyleBackColor = true;
            // 
            // DomainLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 419);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "DomainLogView";
            this.Text = "Hub.Audit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DomainLogView_FormClosing);
            this.Load += new System.EventHandler(this.DomainLogView_Load);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._domainGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this._domainLogTab.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox _log;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox _display;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _beginningEvents;
        private System.Windows.Forms.ToolStripButton _nextEvents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label _detailsLabel;
        private System.Windows.Forms.DataGridView _domainGrid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabPage _domainLogTab;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtCount;
        private System.Windows.Forms.ToolStripDropDownButton resendDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem modifySendMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabPage viewsTab;

    }
}

