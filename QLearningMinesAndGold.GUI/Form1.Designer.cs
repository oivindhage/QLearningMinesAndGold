namespace WindowsFormsApp1
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblQValues = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStartStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStepCount = new System.Windows.Forms.Label();
            this.lblEpisodeCount = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentEpisodeReward = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMaxEpisodeReward = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbrEpsilon = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblExploitation = new System.Windows.Forms.Label();
            this.lblExploration = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrEpsilon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(32, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(638, 27);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(137, 23);
            this.btnNextStep.TabIndex = 2;
            this.btnNextStep.Text = "Next step";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(656, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Q-values:";
            // 
            // lblQValues
            // 
            this.lblQValues.AutoSize = true;
            this.lblQValues.Location = new System.Drawing.Point(817, 118);
            this.lblQValues.Name = "lblQValues";
            this.lblQValues.Size = new System.Drawing.Size(13, 13);
            this.lblQValues.TabIndex = 4;
            this.lblQValues.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Interval = 4;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(638, 320);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(137, 23);
            this.btnStartStop.TabIndex = 5;
            this.btnStartStop.Text = "Start/Stop";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(656, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Step count";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(656, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Episode count";
            // 
            // lblStepCount
            // 
            this.lblStepCount.AutoSize = true;
            this.lblStepCount.Location = new System.Drawing.Point(817, 139);
            this.lblStepCount.Name = "lblStepCount";
            this.lblStepCount.Size = new System.Drawing.Size(13, 13);
            this.lblStepCount.TabIndex = 8;
            this.lblStepCount.Text = "0";
            // 
            // lblEpisodeCount
            // 
            this.lblEpisodeCount.AutoSize = true;
            this.lblEpisodeCount.Location = new System.Drawing.Point(817, 161);
            this.lblEpisodeCount.Name = "lblEpisodeCount";
            this.lblEpisodeCount.Size = new System.Drawing.Size(13, 13);
            this.lblEpisodeCount.TabIndex = 9;
            this.lblEpisodeCount.Text = "0";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(638, 230);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(137, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(782, 230);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "g:\\temp\\qtable.csv";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(638, 259);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(137, 23);
            this.btnLoad.TabIndex = 12;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(782, 347);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 13;
            this.trackBar1.Value = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(782, 324);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Update UI";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(656, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Current episode reward";
            // 
            // lblCurrentEpisodeReward
            // 
            this.lblCurrentEpisodeReward.AutoSize = true;
            this.lblCurrentEpisodeReward.Location = new System.Drawing.Point(817, 95);
            this.lblCurrentEpisodeReward.Name = "lblCurrentEpisodeReward";
            this.lblCurrentEpisodeReward.Size = new System.Drawing.Size(13, 13);
            this.lblCurrentEpisodeReward.TabIndex = 16;
            this.lblCurrentEpisodeReward.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(656, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Max episode reward";
            // 
            // lblMaxEpisodeReward
            // 
            this.lblMaxEpisodeReward.AutoSize = true;
            this.lblMaxEpisodeReward.Location = new System.Drawing.Point(817, 73);
            this.lblMaxEpisodeReward.Name = "lblMaxEpisodeReward";
            this.lblMaxEpisodeReward.Size = new System.Drawing.Size(13, 13);
            this.lblMaxEpisodeReward.TabIndex = 18;
            this.lblMaxEpisodeReward.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(782, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Full speed start/stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(656, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Interval";
            // 
            // tbrEpsilon
            // 
            this.tbrEpsilon.Location = new System.Drawing.Point(782, 398);
            this.tbrEpsilon.Maximum = 100;
            this.tbrEpsilon.Name = "tbrEpsilon";
            this.tbrEpsilon.Size = new System.Drawing.Size(104, 45);
            this.tbrEpsilon.TabIndex = 21;
            this.tbrEpsilon.ValueChanged += new System.EventHandler(this.tbrEpsilon_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(656, 417);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Exploitation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(892, 417);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Exploration";
            // 
            // lblExploitation
            // 
            this.lblExploitation.AutoSize = true;
            this.lblExploitation.Location = new System.Drawing.Point(656, 398);
            this.lblExploitation.Name = "lblExploitation";
            this.lblExploitation.Size = new System.Drawing.Size(13, 13);
            this.lblExploitation.TabIndex = 24;
            this.lblExploitation.Text = "_";
            // 
            // lblExploration
            // 
            this.lblExploration.AutoSize = true;
            this.lblExploration.Location = new System.Drawing.Point(892, 398);
            this.lblExploration.Name = "lblExploration";
            this.lblExploration.Size = new System.Drawing.Size(13, 13);
            this.lblExploration.TabIndex = 25;
            this.lblExploration.Text = "_";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 651);
            this.Controls.Add(this.lblExploration);
            this.Controls.Add(this.lblExploitation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbrEpsilon);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMaxEpisodeReward);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCurrentEpisodeReward);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblEpisodeCount);
            this.Controls.Add(this.lblStepCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.lblQValues);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNextStep);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrEpsilon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQValues;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStepCount;
        private System.Windows.Forms.Label lblEpisodeCount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCurrentEpisodeReward;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMaxEpisodeReward;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar tbrEpsilon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblExploitation;
        private System.Windows.Forms.Label lblExploration;
    }
}

