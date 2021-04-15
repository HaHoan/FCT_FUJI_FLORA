using System;

namespace FCT_FUJI_FLORA
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.txtMilliseconds = new System.Windows.Forms.MaskedTextBox();
            this.txtOutputLog = new System.Windows.Forms.TextBox();
            this.txtStationNo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveChanged = new System.Windows.Forms.Button();
            this.lblPathOutPut = new System.Windows.Forms.Label();
            this.lblPathInput = new System.Windows.Forms.Label();
            this.txtInputPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rb100 = new System.Windows.Forms.RadioButton();
            this.rb200 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFlora = new System.Windows.Forms.RadioButton();
            this.rbZakuro = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMilliseconds
            // 
            this.txtMilliseconds.Location = new System.Drawing.Point(102, 157);
            this.txtMilliseconds.Name = "txtMilliseconds";
            this.txtMilliseconds.Size = new System.Drawing.Size(216, 20);
            this.txtMilliseconds.TabIndex = 2;
            // 
            // txtOutputLog
            // 
            this.txtOutputLog.Location = new System.Drawing.Point(102, 88);
            this.txtOutputLog.Name = "txtOutputLog";
            this.txtOutputLog.Size = new System.Drawing.Size(216, 20);
            this.txtOutputLog.TabIndex = 34;
            // 
            // txtStationNo
            // 
            this.txtStationNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStationNo.Location = new System.Drawing.Point(102, 124);
            this.txtStationNo.Name = "txtStationNo";
            this.txtStationNo.Size = new System.Drawing.Size(216, 20);
            this.txtStationNo.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label17.Location = new System.Drawing.Point(326, 160);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 9);
            this.label17.TabIndex = 29;
            this.label17.Text = "Milliseconds";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 164);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 30;
            this.label16.Text = "Sleep Time:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(33, 131);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 31;
            this.label18.Text = "Station No:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Wip Path Log:";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 51);
            this.label1.TabIndex = 38;
            this.label1.Text = "Input Setting";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveChanged
            // 
            this.btnSaveChanged.BackColor = System.Drawing.Color.Green;
            this.btnSaveChanged.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanged.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanged.Image = global::FCT_FUJI_FLORA.Properties.Resources.Save;
            this.btnSaveChanged.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanged.Location = new System.Drawing.Point(232, 369);
            this.btnSaveChanged.Name = "btnSaveChanged";
            this.btnSaveChanged.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveChanged.Size = new System.Drawing.Size(86, 30);
            this.btnSaveChanged.TabIndex = 4;
            this.btnSaveChanged.Text = "&Saves";
            this.btnSaveChanged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveChanged.UseVisualStyleBackColor = false;
            this.btnSaveChanged.Click += new System.EventHandler(this.btnSaveChanged_Click);
            // 
            // lblPathOutPut
            // 
            this.lblPathOutPut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPathOutPut.Image = global::FCT_FUJI_FLORA.Properties.Resources.folder_saved_search_16;
            this.lblPathOutPut.Location = new System.Drawing.Point(327, 87);
            this.lblPathOutPut.Name = "lblPathOutPut";
            this.lblPathOutPut.Size = new System.Drawing.Size(18, 23);
            this.lblPathOutPut.TabIndex = 35;
            this.lblPathOutPut.Click += new System.EventHandler(this.lblPathOutPut_Click);
            // 
            // lblPathInput
            // 
            this.lblPathInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPathInput.Image = global::FCT_FUJI_FLORA.Properties.Resources.folder_saved_search_16;
            this.lblPathInput.Location = new System.Drawing.Point(327, 52);
            this.lblPathInput.Name = "lblPathInput";
            this.lblPathInput.Size = new System.Drawing.Size(18, 23);
            this.lblPathInput.TabIndex = 41;
            this.lblPathInput.Click += new System.EventHandler(this.lblPathInput_Click);
            // 
            // txtInputPath
            // 
            this.txtInputPath.Location = new System.Drawing.Point(102, 54);
            this.txtInputPath.Name = "txtInputPath";
            this.txtInputPath.Size = new System.Drawing.Size(216, 20);
            this.txtInputPath.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "FCT Path Log:";
            // 
            // rb100
            // 
            this.rb100.AutoSize = true;
            this.rb100.Checked = true;
            this.rb100.Location = new System.Drawing.Point(23, 30);
            this.rb100.Name = "rb100";
            this.rb100.Size = new System.Drawing.Size(43, 17);
            this.rb100.TabIndex = 43;
            this.rb100.TabStop = true;
            this.rb100.Text = "100";
            this.rb100.UseVisualStyleBackColor = true;
            this.rb100.CheckedChanged += new System.EventHandler(this.rb100_CheckedChanged);
            // 
            // rb200
            // 
            this.rb200.AutoSize = true;
            this.rb200.Location = new System.Drawing.Point(113, 30);
            this.rb200.Name = "rb200";
            this.rb200.Size = new System.Drawing.Size(43, 17);
            this.rb200.TabIndex = 44;
            this.rb200.Text = "200";
            this.rb200.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbZakuro);
            this.groupBox1.Controls.Add(this.rbFlora);
            this.groupBox1.Location = new System.Drawing.Point(100, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 72);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn loại máy";
            // 
            // rbFlora
            // 
            this.rbFlora.AutoSize = true;
            this.rbFlora.Checked = true;
            this.rbFlora.Location = new System.Drawing.Point(25, 30);
            this.rbFlora.Name = "rbFlora";
            this.rbFlora.Size = new System.Drawing.Size(48, 17);
            this.rbFlora.TabIndex = 0;
            this.rbFlora.TabStop = true;
            this.rbFlora.Text = "Flora";
            this.rbFlora.UseVisualStyleBackColor = true;
            this.rbFlora.CheckedChanged += new System.EventHandler(this.rbFlora_CheckedChanged);
            // 
            // rbZakuro
            // 
            this.rbZakuro.AutoSize = true;
            this.rbZakuro.Location = new System.Drawing.Point(115, 30);
            this.rbZakuro.Name = "rbZakuro";
            this.rbZakuro.Size = new System.Drawing.Size(59, 17);
            this.rbZakuro.TabIndex = 1;
            this.rbZakuro.Text = "Zakuro";
            this.rbZakuro.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb100);
            this.groupBox2.Controls.Add(this.rb200);
            this.groupBox2.Location = new System.Drawing.Point(102, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 61);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chọn Model 100V hoặc 200V";
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 416);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblPathInput);
            this.Controls.Add(this.txtInputPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveChanged);
            this.Controls.Add(this.txtMilliseconds);
            this.Controls.Add(this.lblPathOutPut);
            this.Controls.Add(this.txtOutputLog);
            this.Controls.Add(this.txtStationNo);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion

        private System.Windows.Forms.Button btnSaveChanged;
        private System.Windows.Forms.MaskedTextBox txtMilliseconds;
        private System.Windows.Forms.Label lblPathOutPut;
        private System.Windows.Forms.TextBox txtOutputLog;
        private System.Windows.Forms.TextBox txtStationNo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPathInput;
        private System.Windows.Forms.TextBox txtInputPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb100;
        private System.Windows.Forms.RadioButton rb200;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbZakuro;
        private System.Windows.Forms.RadioButton rbFlora;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}