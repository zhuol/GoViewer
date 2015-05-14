namespace GoViewer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.itemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.statPanel = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnJudge = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnMode = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.timerView = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            this.statPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(970, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemOpen,
            this.itemSave,
            this.itemSaveAs,
            this.itemQuit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(43, 20);
            this.menuFile.Text = "文件";
            // 
            // itemOpen
            // 
            this.itemOpen.Name = "itemOpen";
            this.itemOpen.Size = new System.Drawing.Size(122, 22);
            this.itemOpen.Text = "打开棋谱";
            this.itemOpen.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // itemSave
            // 
            this.itemSave.Name = "itemSave";
            this.itemSave.Size = new System.Drawing.Size(122, 22);
            this.itemSave.Text = "保存";
            // 
            // itemSaveAs
            // 
            this.itemSaveAs.Name = "itemSaveAs";
            this.itemSaveAs.Size = new System.Drawing.Size(122, 22);
            this.itemSaveAs.Text = "另存为";
            // 
            // itemQuit
            // 
            this.itemQuit.Name = "itemQuit";
            this.itemQuit.Size = new System.Drawing.Size(122, 22);
            this.itemQuit.Text = "退出";
            this.itemQuit.Click += new System.EventHandler(this.itemQuit_Click);
            // 
            // statPanel
            // 
            this.statPanel.BackColor = System.Drawing.Color.LightGray;
            this.statPanel.Controls.Add(this.btnFile);
            this.statPanel.Controls.Add(this.btnJudge);
            this.statPanel.Controls.Add(this.btnMode);
            this.statPanel.Controls.Add(this.lblResult);
            this.statPanel.Controls.Add(this.label2);
            this.statPanel.Controls.Add(this.label1);
            this.statPanel.Controls.Add(this.btnEnd);
            this.statPanel.Controls.Add(this.btnStart);
            this.statPanel.Controls.Add(this.btnNext);
            this.statPanel.Controls.Add(this.btnPrevious);
            this.statPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.statPanel.Location = new System.Drawing.Point(830, 24);
            this.statPanel.Name = "statPanel";
            this.statPanel.Size = new System.Drawing.Size(140, 773);
            this.statPanel.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("KaiTi", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.ForeColor = System.Drawing.Color.DarkRed;
            this.lblResult.Location = new System.Drawing.Point(17, 139);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(82, 21);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "label3";
            this.lblResult.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "白方";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "黑方";
            // 
            // btnJudge
            // 
            this.btnJudge.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnJudge.Location = new System.Drawing.Point(21, 655);
            this.btnJudge.Name = "btnJudge";
            this.btnJudge.Size = new System.Drawing.Size(75, 23);
            this.btnJudge.TabIndex = 6;
            this.btnJudge.Text = "点目";
            this.btnJudge.UseVisualStyleBackColor = true;
            this.btnJudge.Click += new System.EventHandler(this.btnJudge_Click);
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFile.Location = new System.Drawing.Point(21, 717);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 5;
            this.btnFile.Text = "打开棋谱";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnMode
            // 
            this.btnMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMode.Location = new System.Drawing.Point(21, 686);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(75, 23);
            this.btnMode.TabIndex = 4;
            this.btnMode.Text = "自动打谱";
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEnd.Location = new System.Drawing.Point(21, 624);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 23);
            this.btnEnd.TabIndex = 3;
            this.btnEnd.Text = "结束";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Location = new System.Drawing.Point(21, 593);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrevious.Location = new System.Drawing.Point(21, 562);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "上一步";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStart.Location = new System.Drawing.Point(21, 531);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timerView
            // 
            this.timerView.Interval = 1000;
            this.timerView.Tick += new System.EventHandler(this.timerView_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 797);
            this.Controls.Add(this.statPanel);
            this.Controls.Add(this.menu);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statPanel.ResumeLayout(false);
            this.statPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem itemOpen;
        private System.Windows.Forms.ToolStripMenuItem itemSave;
        private System.Windows.Forms.ToolStripMenuItem itemSaveAs;
        private System.Windows.Forms.ToolStripMenuItem itemQuit;
        private System.Windows.Forms.Panel statPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnMode;
        private System.Windows.Forms.Timer timerView;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Button btnJudge;
        private System.Windows.Forms.Label lblResult;
    }
}

