using FCT_FUJI_FLORA.Business;
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
    public partial class FormMain : Form
    {
        private FileSystemWatcher watcher;
        private PVSServiceReferences.PVSWebServiceSoapClient _pvs_service = new PVSServiceReferences.PVSWebServiceSoapClient();
        private bool _isStart = false;
        private DataTable dt;
        private int total = 0;
        private int ng = 0;
        private int pass = 0;
        private DateTime lastRead = DateTime.MinValue;

        public FormMain()
        {
            InitializeComponent();
            lblVersion.Text = Ultils.GetRunningVersion();
            InitDataTableView();
        }

        private void InitDataTableView()
        {
            dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("BOARD NO", typeof(string)),
                        new DataColumn("PRODUCT", typeof(string)),
                        new DataColumn("STATION",typeof(string)),
                        new DataColumn("STATE",typeof(string)),
                        new DataColumn("DATE",typeof(string)), });
            dgrvResult.DataSource = dt;
        }

        private void InitWatcher()
        {
            try
            {
                watcher = new FileSystemWatcher();
                string path = Ultils.GetValueRegistryKey(KeyName.PATH_INPUT);
                DateTime currentDate = DateTime.Now;
                string dateConvert = currentDate.ToString("yyyyMMdd");
                path += "\\" + dateConvert;
                watcher.Path = path;
                watcher.NotifyFilter = NotifyFilters.LastWrite;

                watcher.Filter = Constants.FILE_INPUT_EXTENSION;
                watcher.Created += OnChanged;
                watcher.Changed += OnChanged;
            }
            catch (Exception)
            {
                ShowMessage("FAIL", "NG", "Kiểm tra lại File input");
                return;
            }
            ShowMessage("STOP", "", "");
        }
        private void checkFileLog(FileSystemEventArgs e)
        {
            System.Threading.Thread.Sleep(int.Parse(Ultils.GetValueRegistryKey(KeyName.SLEEP_TIME)));
            string fullPath = e.FullPath;
            BeginInvoke(new Action(() => { lblPathLog.Text = fullPath; }));
            string fileName = Path.GetFileName(fullPath);
            int indexOfUnderscore = fileName.IndexOf("_");
            int indexOfDot = fileName.IndexOf(".");
            string dateOfFile = fileName.Substring(indexOfUnderscore + 1, indexOfDot - indexOfUnderscore - 1);
            DateTime currentDate = DateTime.Now;
            string dateConvert = currentDate.ToString("yyyyMMdd");
            if (dateOfFile == dateConvert)
            {
                ReadFileLog(fullPath);
            }
            else
            {
                return;
            }
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (_isStart)
            {

                try
                {
                    DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
                    TimeSpan span = lastWriteTime - lastRead;
                    if (span.Seconds > 1)
                    {
                        checkFileLog(e);
                        lastRead = lastWriteTime;
                    }

                }
                catch (Exception ex)
                {
                    ShowMessage("FAIL", "NG", ex.Message.ToString());
                }

            }
        }

        private void ReadFileLog(string fullPath)
        {
            try
            {
                string line = Ultils.ReadLastLine(fullPath);
                var lines = line.Split(',');
                var barcode = lines[0];
                string stationBefore = "N/A";
                string stationCurrent = Ultils.GetValueRegistryKey(KeyName.STATION_NO);
                //var check = checkStation(barcode, stationCurrent, out stationBefore);
                //if (!check)
                //{
                //    ShowMessage("FAIL", "NG", $"Không tìm thấy dữ liệu tại trạm [{stationBefore}] | [{stationCurrent}]");
                //    return;
                //}
                int indexOfUnderscore = barcode.IndexOf("_");
                var productId = barcode.Substring(indexOfUnderscore + 1, barcode.Length - indexOfUnderscore - 1);
                var boardState = lines[4].ToUpper();
                var station = Ultils.GetValueRegistryKey(KeyName.STATION_NO);
                var dateSqlServer = _pvs_service.GetDateTime();
                PVSServiceReferences.SCANNING_LOGSEntity item = new PVSServiceReferences.SCANNING_LOGSEntity()
                {
                    BOARD_NO = barcode,
                    PRODUCT_ID = productId,
                    STATION_NO = station,
                    UPDATE_TIME = dateSqlServer,
                    BOARD_STATE = boardState == Constants.PASS ? 1 : 0,
                };
                total++;
                if (item.BOARD_STATE == 1)
                {
                    ShowMessage("PASS", "OK", "FCT check OK!");
                    pass++;
                }
                else
                {
                    ShowMessage("FAIL", "NG", "FCT check NG!");
                    ng++;
                }
                Ultils.IsCreateFileLog(item.PRODUCT_ID, item.BOARD_NO, item.BOARD_STATE == 1 ? "P" : "F", station, dateSqlServer);
                BeginInvoke(new Action(() =>
                {
                    lblPASS.Text = pass.ToString();
                    lblNG.Text = ng.ToString();
                    lblTOTAL.Text = total.ToString();
                    dt.Clear();
                    dt.Rows.Add(item.BOARD_NO, item.PRODUCT_ID, item.STATION_NO, item.BOARD_STATE == 1 ? "P" : "F", dateSqlServer);
                    dgrvResult.Refresh();
                    for (int i = 0; i < dgrvResult.Rows.Count; i++)
                    {
                        if (item.BOARD_STATE == 1)
                        {
                            dgrvResult.Rows[i].DefaultCellStyle.BackColor = Color.DarkGreen;
                            dgrvResult.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                        else
                        {
                            dgrvResult.Rows[i].DefaultCellStyle.BackColor = Color.DarkRed;
                            dgrvResult.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }

                }));

            }
            catch (Exception e)
            {

                Console.Write(e.ToString());
            }


        }

        private void lbl_Setting_Click(object sender, EventArgs e)
        {
            var frmSetting = new frmSetting();
            frmSetting.updateAfterSetting = new Action(() =>
            {
                InitWatcher();
            });
            frmSetting.ShowDialog();
        }


        private void resetTable()
        {

            dt.Clear();
            dgrvResult.Refresh();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            InitWatcher();
        }


        public bool checkStation(string item, string stationNo, out string stationBefore)
        {

            try
            {
                stationBefore = "N/A";
                var orderItem = _pvs_service.GetWorkOrderItemByBoardNo(item);
                if (orderItem == null)
                {
                    return false;
                }
                var woProcedure = _pvs_service.GetWorkOrderProcedure(orderItem.ORDER_NO);
                var indexLastStation = Convert.ToInt32(woProcedure.FirstOrDefault(r => r.STATION_NO == stationNo).INDEX);
                stationBefore = woProcedure.FirstOrDefault(r => r.INDEX == (indexLastStation - 1)).STATION_NO;
                var orderitem = _pvs_service.GetWorkOrderItem(item, stationBefore);
                var orderitemCurr = _pvs_service.GetWorkOrderItem(item, stationNo);
                if (orderitem != null || orderitemCurr != null)
                {
                    return true;
                }
                return false;

            }
            catch (Exception)
            {
                stationBefore = "N/A";
                return false;
            }

        }


        private void ShowMessage(string key, string str_status, string str_message)
        {
            switch (key)
            {
                case "PASS":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.DarkGreen; }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkGreen; }));
                    break;
                case "FAIL":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.DarkRed; }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkRed; }));
                    break;
                case "WAIT":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.FromArgb(255, 128, 0); }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.FromArgb(255, 128, 0); }));
                    break;
                case "Default":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = @"[N\A]"; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.FromArgb(255, 128, 0); }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = "no results"; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.FromArgb(255, 128, 0); }));
                    break;
                case "STOP":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = @"[N\A]"; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.FromArgb(255, 128, 0); }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = "Nhấn Start để bắt đầu"; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.FromArgb(255, 128, 0); }));
                    break;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            BeginInvoke(new Action(() =>
            {
                lblPathLog.ResetText();
                resetTable();
            }));


            _isStart = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lbl_Setting.Enabled = false;
            try
            {
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception)
            {
                ShowMessage("FAIL", "NG", "Kiểm tra lại File input");
                return;
            }

            ShowMessage("WAIT", "N/A", "Waiting file FCT...");
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                lblPathLog.ResetText();
                resetTable();
            }));


            _isStart = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lbl_Setting.Enabled = true;
            try
            {
                watcher.EnableRaisingEvents = false;
            }
            catch (Exception)
            {
                ShowMessage("FAIL", "NG", "Kiểm tra lại File input");
                return;
            }
            ShowMessage("STOP", "", "");
        }
    }
}
