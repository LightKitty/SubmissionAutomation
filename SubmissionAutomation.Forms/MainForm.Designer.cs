namespace SubmissionAutomation.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControlChannels = new System.Windows.Forms.TabControl();
            this.tabPageBilibili = new System.Windows.Forms.TabPage();
            this.tabPageDouyu = new System.Windows.Forms.TabPage();
            this.textBoxVideoPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOpenVideo = new System.Windows.Forms.Button();
            this.textBoxCoverPath = new System.Windows.Forms.TextBox();
            this.buttonOpenCover = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTags = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxIntroduction = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.checkBoxPublishBilibili = new System.Windows.Forms.CheckBox();
            this.checkBoxPublishDouyu = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDouyuClassify = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPageXigua = new System.Windows.Forms.TabPage();
            this.checkBoxPublishXigua = new System.Windows.Forms.CheckBox();
            this.tabPageBaidu = new System.Windows.Forms.TabPage();
            this.checkBoxPublishBaidu = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tabControlChannels.SuspendLayout();
            this.tabPageBilibili.SuspendLayout();
            this.tabPageDouyu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPageXigua.SuspendLayout();
            this.tabPageBaidu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSubmit);
            this.groupBox1.Controls.Add(this.textBoxIntroduction);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxTags);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonOpenCover);
            this.groupBox1.Controls.Add(this.textBoxCoverPath);
            this.groupBox1.Controls.Add(this.buttonOpenVideo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxVideoPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 524);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通用";
            // 
            // tabControlChannels
            // 
            this.tabControlChannels.Controls.Add(this.tabPageBilibili);
            this.tabControlChannels.Controls.Add(this.tabPageDouyu);
            this.tabControlChannels.Controls.Add(this.tabPageXigua);
            this.tabControlChannels.Controls.Add(this.tabPageBaidu);
            this.tabControlChannels.Location = new System.Drawing.Point(393, 12);
            this.tabControlChannels.Name = "tabControlChannels";
            this.tabControlChannels.SelectedIndex = 0;
            this.tabControlChannels.Size = new System.Drawing.Size(379, 524);
            this.tabControlChannels.TabIndex = 1;
            // 
            // tabPageBilibili
            // 
            this.tabPageBilibili.Controls.Add(this.checkBoxPublishBilibili);
            this.tabPageBilibili.Location = new System.Drawing.Point(4, 26);
            this.tabPageBilibili.Name = "tabPageBilibili";
            this.tabPageBilibili.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBilibili.Size = new System.Drawing.Size(371, 507);
            this.tabPageBilibili.TabIndex = 0;
            this.tabPageBilibili.Text = "哔哩";
            this.tabPageBilibili.UseVisualStyleBackColor = true;
            // 
            // tabPageDouyu
            // 
            this.tabPageDouyu.Controls.Add(this.textBoxDouyuClassify);
            this.tabPageDouyu.Controls.Add(this.label6);
            this.tabPageDouyu.Controls.Add(this.checkBoxPublishDouyu);
            this.tabPageDouyu.Location = new System.Drawing.Point(4, 26);
            this.tabPageDouyu.Name = "tabPageDouyu";
            this.tabPageDouyu.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDouyu.Size = new System.Drawing.Size(371, 494);
            this.tabPageDouyu.TabIndex = 1;
            this.tabPageDouyu.Text = "斗鱼";
            this.tabPageDouyu.UseVisualStyleBackColor = true;
            // 
            // textBoxVideoPath
            // 
            this.textBoxVideoPath.Location = new System.Drawing.Point(56, 22);
            this.textBoxVideoPath.Name = "textBoxVideoPath";
            this.textBoxVideoPath.Size = new System.Drawing.Size(236, 23);
            this.textBoxVideoPath.TabIndex = 0;
            this.textBoxVideoPath.Text = "E:\\地球频道\\2.videos\\20210402毅力号自拍\\导出.mp4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "视频：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "封面：";
            // 
            // buttonOpenVideo
            // 
            this.buttonOpenVideo.Location = new System.Drawing.Point(298, 22);
            this.buttonOpenVideo.Name = "buttonOpenVideo";
            this.buttonOpenVideo.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenVideo.TabIndex = 3;
            this.buttonOpenVideo.Text = "打开";
            this.buttonOpenVideo.UseVisualStyleBackColor = true;
            this.buttonOpenVideo.Click += new System.EventHandler(this.buttonOpenVideo_Click);
            // 
            // textBoxCoverPath
            // 
            this.textBoxCoverPath.Location = new System.Drawing.Point(56, 51);
            this.textBoxCoverPath.Name = "textBoxCoverPath";
            this.textBoxCoverPath.Size = new System.Drawing.Size(236, 23);
            this.textBoxCoverPath.TabIndex = 4;
            this.textBoxCoverPath.Text = "E:\\地球频道\\2.videos\\20200809\\vlcsnap-2020-08-09-23h21m21s931.png";
            // 
            // buttonOpenCover
            // 
            this.buttonOpenCover.Location = new System.Drawing.Point(298, 51);
            this.buttonOpenCover.Name = "buttonOpenCover";
            this.buttonOpenCover.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenCover.TabIndex = 5;
            this.buttonOpenCover.Text = "打开";
            this.buttonOpenCover.UseVisualStyleBackColor = true;
            this.buttonOpenCover.Click += new System.EventHandler(this.buttonOpenCover_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "标题：";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(56, 80);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(317, 23);
            this.textBoxTitle.TabIndex = 7;
            this.textBoxTitle.Text = "标题1234522";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = " 标签：";
            // 
            // textBoxTags
            // 
            this.textBoxTags.Location = new System.Drawing.Point(56, 109);
            this.textBoxTags.Name = "textBoxTags";
            this.textBoxTags.Size = new System.Drawing.Size(317, 23);
            this.textBoxTags.TabIndex = 9;
            this.textBoxTags.Text = "标签 地球 拉拉";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = " 简介：";
            // 
            // textBoxIntroduction
            // 
            this.textBoxIntroduction.Location = new System.Drawing.Point(56, 138);
            this.textBoxIntroduction.Multiline = true;
            this.textBoxIntroduction.Name = "textBoxIntroduction";
            this.textBoxIntroduction.Size = new System.Drawing.Size(317, 115);
            this.textBoxIntroduction.TabIndex = 11;
            this.textBoxIntroduction.Text = "简介很长00000000000000000999999999999999";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(298, 259);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 12;
            this.buttonSubmit.Text = "发布";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // checkBoxPublishBilibili
            // 
            this.checkBoxPublishBilibili.AutoSize = true;
            this.checkBoxPublishBilibili.Checked = true;
            this.checkBoxPublishBilibili.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPublishBilibili.Location = new System.Drawing.Point(6, 6);
            this.checkBoxPublishBilibili.Name = "checkBoxPublishBilibili";
            this.checkBoxPublishBilibili.Size = new System.Drawing.Size(51, 21);
            this.checkBoxPublishBilibili.TabIndex = 0;
            this.checkBoxPublishBilibili.Text = "发布";
            this.checkBoxPublishBilibili.UseVisualStyleBackColor = true;
            // 
            // checkBoxPublishDouyu
            // 
            this.checkBoxPublishDouyu.AutoSize = true;
            this.checkBoxPublishDouyu.Checked = true;
            this.checkBoxPublishDouyu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPublishDouyu.Location = new System.Drawing.Point(6, 6);
            this.checkBoxPublishDouyu.Name = "checkBoxPublishDouyu";
            this.checkBoxPublishDouyu.Size = new System.Drawing.Size(51, 21);
            this.checkBoxPublishDouyu.TabIndex = 1;
            this.checkBoxPublishDouyu.Text = "发布";
            this.checkBoxPublishDouyu.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "分类：";
            // 
            // textBoxDouyuClassify
            // 
            this.textBoxDouyuClassify.Location = new System.Drawing.Point(56, 25);
            this.textBoxDouyuClassify.Name = "textBoxDouyuClassify";
            this.textBoxDouyuClassify.Size = new System.Drawing.Size(100, 23);
            this.textBoxDouyuClassify.TabIndex = 13;
            this.textBoxDouyuClassify.Text = "科学科普";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel.Text = "就绪";
            // 
            // tabPageXigua
            // 
            this.tabPageXigua.Controls.Add(this.checkBoxPublishXigua);
            this.tabPageXigua.Location = new System.Drawing.Point(4, 26);
            this.tabPageXigua.Name = "tabPageXigua";
            this.tabPageXigua.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageXigua.Size = new System.Drawing.Size(371, 494);
            this.tabPageXigua.TabIndex = 2;
            this.tabPageXigua.Text = "西瓜";
            this.tabPageXigua.UseVisualStyleBackColor = true;
            // 
            // checkBoxPublishXigua
            // 
            this.checkBoxPublishXigua.AutoSize = true;
            this.checkBoxPublishXigua.Checked = true;
            this.checkBoxPublishXigua.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPublishXigua.Location = new System.Drawing.Point(6, 6);
            this.checkBoxPublishXigua.Name = "checkBoxPublishXigua";
            this.checkBoxPublishXigua.Size = new System.Drawing.Size(51, 21);
            this.checkBoxPublishXigua.TabIndex = 2;
            this.checkBoxPublishXigua.Text = "发布";
            this.checkBoxPublishXigua.UseVisualStyleBackColor = true;
            // 
            // tabPageBaidu
            // 
            this.tabPageBaidu.Controls.Add(this.checkBoxPublishBaidu);
            this.tabPageBaidu.Location = new System.Drawing.Point(4, 26);
            this.tabPageBaidu.Name = "tabPageBaidu";
            this.tabPageBaidu.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBaidu.Size = new System.Drawing.Size(371, 494);
            this.tabPageBaidu.TabIndex = 3;
            this.tabPageBaidu.Text = "百度";
            this.tabPageBaidu.UseVisualStyleBackColor = true;
            // 
            // checkBoxPublishBaidu
            // 
            this.checkBoxPublishBaidu.AutoSize = true;
            this.checkBoxPublishBaidu.Checked = true;
            this.checkBoxPublishBaidu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPublishBaidu.Location = new System.Drawing.Point(6, 6);
            this.checkBoxPublishBaidu.Name = "checkBoxPublishBaidu";
            this.checkBoxPublishBaidu.Size = new System.Drawing.Size(51, 21);
            this.checkBoxPublishBaidu.TabIndex = 3;
            this.checkBoxPublishBaidu.Text = "发布";
            this.checkBoxPublishBaidu.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlChannels);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动投稿助手";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControlChannels.ResumeLayout(false);
            this.tabPageBilibili.ResumeLayout(false);
            this.tabPageBilibili.PerformLayout();
            this.tabPageDouyu.ResumeLayout(false);
            this.tabPageDouyu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPageXigua.ResumeLayout(false);
            this.tabPageXigua.PerformLayout();
            this.tabPageBaidu.ResumeLayout(false);
            this.tabPageBaidu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOpenCover;
        private System.Windows.Forms.TextBox textBoxCoverPath;
        private System.Windows.Forms.Button buttonOpenVideo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVideoPath;
        private System.Windows.Forms.TabControl tabControlChannels;
        private System.Windows.Forms.TabPage tabPageBilibili;
        private System.Windows.Forms.TabPage tabPageDouyu;
        private System.Windows.Forms.TextBox textBoxIntroduction;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTags;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.CheckBox checkBoxPublishBilibili;
        private System.Windows.Forms.CheckBox checkBoxPublishDouyu;
        private System.Windows.Forms.TextBox textBoxDouyuClassify;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TabPage tabPageXigua;
        private System.Windows.Forms.CheckBox checkBoxPublishXigua;
        private System.Windows.Forms.TabPage tabPageBaidu;
        private System.Windows.Forms.CheckBox checkBoxPublishBaidu;
    }
}

