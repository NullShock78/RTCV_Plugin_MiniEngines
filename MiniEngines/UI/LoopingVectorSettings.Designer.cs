
namespace MiniEngines.UI
{
    partial class LoopingVectorSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoopingVectorSettings));
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbVectorEngineValueText1 = new System.Windows.Forms.Label();
            this.cbDynaVectorValueList = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pnLimiterList = new System.Windows.Forms.Panel();
            this.lbVectorEngineLimiterText1 = new System.Windows.Forms.Label();
            this.cbDynaVectorLimiterList = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nmBleedForward = new System.Windows.Forms.NumericUpDown();
            this.nmBleedBack = new System.Windows.Forms.NumericUpDown();
            this.bResync = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.pnLimiterList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel2.Controls.Add(this.lbVectorEngineValueText1);
            this.panel2.Controls.Add(this.cbDynaVectorValueList);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Location = new System.Drawing.Point(12, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(279, 47);
            this.panel2.TabIndex = 149;
            this.panel2.Tag = "color:dark2";
            // 
            // lbVectorEngineValueText1
            // 
            this.lbVectorEngineValueText1.AutoSize = true;
            this.lbVectorEngineValueText1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbVectorEngineValueText1.ForeColor = System.Drawing.Color.White;
            this.lbVectorEngineValueText1.Location = new System.Drawing.Point(165, 25);
            this.lbVectorEngineValueText1.Name = "lbVectorEngineValueText1";
            this.lbVectorEngineValueText1.Size = new System.Drawing.Size(108, 13);
            this.lbVectorEngineValueText1.TabIndex = 138;
            this.lbVectorEngineValueText1.Text = "Replacement values";
            // 
            // cbDynaVectorValueList
            // 
            this.cbDynaVectorValueList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbDynaVectorValueList.DataSource = ((object)(resources.GetObject("cbDynaVectorValueList.DataSource")));
            this.cbDynaVectorValueList.DisplayMember = "Name";
            this.cbDynaVectorValueList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDynaVectorValueList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDynaVectorValueList.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbDynaVectorValueList.ForeColor = System.Drawing.Color.White;
            this.cbDynaVectorValueList.FormattingEnabled = true;
            this.cbDynaVectorValueList.IntegralHeight = false;
            this.cbDynaVectorValueList.Location = new System.Drawing.Point(8, 19);
            this.cbDynaVectorValueList.MaxDropDownItems = 15;
            this.cbDynaVectorValueList.Name = "cbDynaVectorValueList";
            this.cbDynaVectorValueList.Size = new System.Drawing.Size(152, 21);
            this.cbDynaVectorValueList.TabIndex = 81;
            this.cbDynaVectorValueList.Tag = "color:normal";
            this.cbDynaVectorValueList.ValueMember = "Value";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(5, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 13);
            this.label18.TabIndex = 80;
            this.label18.Text = "Value list:";
            // 
            // pnLimiterList
            // 
            this.pnLimiterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnLimiterList.Controls.Add(this.lbVectorEngineLimiterText1);
            this.pnLimiterList.Controls.Add(this.cbDynaVectorLimiterList);
            this.pnLimiterList.Controls.Add(this.label13);
            this.pnLimiterList.Location = new System.Drawing.Point(12, 46);
            this.pnLimiterList.Name = "pnLimiterList";
            this.pnLimiterList.Size = new System.Drawing.Size(279, 47);
            this.pnLimiterList.TabIndex = 148;
            this.pnLimiterList.Tag = "color:dark2";
            // 
            // lbVectorEngineLimiterText1
            // 
            this.lbVectorEngineLimiterText1.AutoSize = true;
            this.lbVectorEngineLimiterText1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbVectorEngineLimiterText1.ForeColor = System.Drawing.Color.White;
            this.lbVectorEngineLimiterText1.Location = new System.Drawing.Point(165, 24);
            this.lbVectorEngineLimiterText1.Name = "lbVectorEngineLimiterText1";
            this.lbVectorEngineLimiterText1.Size = new System.Drawing.Size(104, 13);
            this.lbVectorEngineLimiterText1.TabIndex = 141;
            this.lbVectorEngineLimiterText1.Text = "Comparison values";
            // 
            // cbDynaVectorLimiterList
            // 
            this.cbDynaVectorLimiterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbDynaVectorLimiterList.DataSource = ((object)(resources.GetObject("cbDynaVectorLimiterList.DataSource")));
            this.cbDynaVectorLimiterList.DisplayMember = "Name";
            this.cbDynaVectorLimiterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDynaVectorLimiterList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDynaVectorLimiterList.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbDynaVectorLimiterList.ForeColor = System.Drawing.Color.White;
            this.cbDynaVectorLimiterList.FormattingEnabled = true;
            this.cbDynaVectorLimiterList.IntegralHeight = false;
            this.cbDynaVectorLimiterList.Location = new System.Drawing.Point(8, 19);
            this.cbDynaVectorLimiterList.MaxDropDownItems = 15;
            this.cbDynaVectorLimiterList.Name = "cbDynaVectorLimiterList";
            this.cbDynaVectorLimiterList.Size = new System.Drawing.Size(152, 21);
            this.cbDynaVectorLimiterList.TabIndex = 78;
            this.cbDynaVectorLimiterList.Tag = "color:normal";
            this.cbDynaVectorLimiterList.ValueMember = "Value";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(6, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 79;
            this.label13.Text = "Limiter list:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(287, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 199;
            this.label2.Text = "Interval";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(298, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 198;
            this.label1.Text = "Units";
            // 
            // nmBleedForward
            // 
            this.nmBleedForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.nmBleedForward.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmBleedForward.ForeColor = System.Drawing.Color.White;
            this.nmBleedForward.Location = new System.Drawing.Point(334, 99);
            this.nmBleedForward.Margin = new System.Windows.Forms.Padding(4);
            this.nmBleedForward.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmBleedForward.Name = "nmBleedForward";
            this.nmBleedForward.Size = new System.Drawing.Size(73, 22);
            this.nmBleedForward.TabIndex = 197;
            this.nmBleedForward.Tag = "color:normal";
            this.nmBleedForward.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nmBleedForward.ValueChanged += new System.EventHandler(this.nmBleedForward_ValueChanged);
            // 
            // nmBleedBack
            // 
            this.nmBleedBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.nmBleedBack.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmBleedBack.ForeColor = System.Drawing.Color.White;
            this.nmBleedBack.Location = new System.Drawing.Point(334, 71);
            this.nmBleedBack.Margin = new System.Windows.Forms.Padding(4);
            this.nmBleedBack.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmBleedBack.Name = "nmBleedBack";
            this.nmBleedBack.Size = new System.Drawing.Size(73, 22);
            this.nmBleedBack.TabIndex = 196;
            this.nmBleedBack.Tag = "color:normal";
            this.nmBleedBack.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmBleedBack.ValueChanged += new System.EventHandler(this.nmBleedBack_ValueChanged);
            // 
            // bResync
            // 
            this.bResync.Location = new System.Drawing.Point(332, 125);
            this.bResync.Name = "bResync";
            this.bResync.Size = new System.Drawing.Size(75, 23);
            this.bResync.TabIndex = 202;
            this.bResync.Text = "Resync";
            this.bResync.UseVisualStyleBackColor = true;
            this.bResync.Click += new System.EventHandler(this.bResync_Click);
            // 
            // LoopingVectorSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(420, 151);
            this.Controls.Add(this.bResync);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmBleedForward);
            this.Controls.Add(this.nmBleedBack);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnLimiterList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 151);
            this.Name = "LoopingVectorSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "color:dark1";
            this.Text = "Dyna Vector";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnLimiterList.ResumeLayout(false);
            this.pnLimiterList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbVectorEngineValueText1;
        public System.Windows.Forms.ComboBox cbDynaVectorValueList;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnLimiterList;
        private System.Windows.Forms.Label lbVectorEngineLimiterText1;
        public System.Windows.Forms.ComboBox cbDynaVectorLimiterList;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmBleedForward;
        private System.Windows.Forms.NumericUpDown nmBleedBack;
        private System.Windows.Forms.Button bResync;
    }
}