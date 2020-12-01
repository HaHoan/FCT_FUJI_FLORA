﻿using FCT_FUJI_FLORA.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCT_FUJI_FLORA
{
    public partial class frmSetting : Form
    {
        public Action updateAfterSetting;
        public frmSetting()
        {
            InitializeComponent();
            BinDataToControls();
        }

        private void BinDataToControls()
        {
            txtOutputLog.Text = Ultils.GetValueRegistryKey(KeyName.PROCESS);
            txtStationNo.Text = Ultils.GetValueRegistryKey(KeyName.STATION_NO);
            txtInputPath.Text = Ultils.GetValueRegistryKey(KeyName.PATH_INPUT);
            txtOutputLog.Text = Ultils.GetValueRegistryKey(KeyName.PATH_OUTPUT);
            txtMilliseconds.Text = Ultils.GetValueRegistryKey(KeyName.SLEEP_TIME);
        }

        private void lblPathInput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog inputLog = new FolderBrowserDialog();
            DialogResult open = inputLog.ShowDialog();
            if (open == DialogResult.OK)
            {
                txtInputPath.Text = inputLog.SelectedPath;
                if (string.IsNullOrEmpty(txtInputPath.Text))
                {
                    btnSaveChanged.Focus();
                }
                else
                {
                    txtInputPath.Focus();
                }
            }
        }

        private void lblPathOutPut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog outputLog = new FolderBrowserDialog();
            DialogResult open = outputLog.ShowDialog();
            if (open == DialogResult.OK)
            {
                txtOutputLog.Text = outputLog.SelectedPath;
                if (string.IsNullOrEmpty(txtOutputLog.Text))
                {
                    btnSaveChanged.Focus();
                }
                else
                {
                    lblPathOutPut.Focus();
                }
            }
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStationNo.Text))
            {
                MessageBox.Show("Tên trạm không được bỏ trống!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            int sleepTime = 0;
            if (!int.TryParse(txtMilliseconds.Text, out sleepTime))
            {
                MessageBox.Show("Sleep Time không hợp lệ!");
                return;
            }
            string inputFolder = txtInputPath.Text.Trim();
            if (!Directory.Exists(inputFolder))
            {
                MessageBox.Show("Không tồn tại folder: " + inputFolder);
                return;
            }

            string outputFolder = txtOutputLog.Text.Trim();
            if (!Directory.Exists(outputFolder))
            {
                MessageBox.Show("Không tồn tại folder: " + outputFolder);
                return;
            }
            Ultils.WriteRegistry(KeyName.PATH_INPUT,inputFolder );
            Ultils.WriteRegistry(KeyName.PATH_OUTPUT, outputFolder);
            Ultils.WriteRegistry(KeyName.STATION_NO, txtStationNo.Text.Trim());
            Ultils.WriteRegistry(KeyName.SLEEP_TIME, txtMilliseconds.Text.Trim());
            MessageBox.Show("Save sucess!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            updateAfterSetting();
            this.Dispose();
        }
    }
}