using FCT_FUJI_FLORA.Business;
using FCT_FUJI_FLORA.PVSServiceReferences;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FCT_FUJI_FLORA
{
    public partial class FormMain : Form
    {
        private FileSystemWatcher watcher;
        private PVSServiceReferences.PVSWebServiceSoapClient _pvs_service = new PVSServiceReferences.PVSWebServiceSoapClient();
        private bool _isStart = false;
        private bool _isFileChanged = false;
        private string _pathFileChanged = "";
        private DataTable dt;
        private int total = 0;
        private int ng = 0;
        private int pass = 0;
        private DateTime lastRead = DateTime.MinValue;
        private BackgroundWorker bgrwSoftInfo;
        private System.Timers.Timer timer;

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
            changeWidthColumnAtIndex(0, 260);
            changeWidthColumnAtIndex(1, 140);
            changeWidthColumnAtIndex(2, 100);
            changeWidthColumnAtIndex(3, 60);
        }
        private void changeWidthColumnAtIndex(int i, int width)
        {
            try
            {
                DataGridViewColumn column = dgrvResult.Columns[i];
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = width;
            }
            catch { }
        }

        private void InitWatcher()
        {
            try
            {
                watcher = new FileSystemWatcher();
                string path = Ultils.GetValueRegistryKey(KeyName.PATH_INPUT);
                string machine = Ultils.GetValueRegistryKey(KeyName.MACHINE);
                if(machine == Machine.FLORA)
                {
                    DateTime currentDate = DateTime.Now;
                    string dateConvert = currentDate.ToString("yyyyMMdd");
                    path += "\\" + dateConvert;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
              
                watcher.Path = path;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime
                                             | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                if (machine == Machine.FLORA)
                {
                    watcher.Filter = Constants.FILE_INPUT_EXTENSION;
                }
                else if (machine == Machine.ZAKURO)
                {
                    watcher.Filter = Constants.FILE_OUTPUT_EXTENSION;
                }
                else ShowMessage("FAIL", "NG", "Chưa chọn loại máy ở Setting!");
                watcher.IncludeSubdirectories = false;
                // watcher.Created += OnChanged;
                watcher.Changed += OnChanged;
            }
            catch (Exception)
            {
                ShowMessage("FAIL", "NG", "Kiểm tra lại File input");
                return;
            }
            ShowMessage("STOP", "", "");
        }



        private void checkFileLog(string fullPath)
        {
            string machine = Ultils.GetValueRegistryKey(KeyName.MACHINE);
            if (machine == Machine.FLORA)
            {
                System.Threading.Thread.Sleep(int.Parse(Ultils.GetValueRegistryKey(KeyName.SLEEP_TIME)));
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
            else if (machine == Machine.ZAKURO)
            {
                System.Threading.Thread.Sleep(int.Parse(Ultils.GetValueRegistryKey(KeyName.SLEEP_TIME)));
                BeginInvoke(new Action(() => { lblPathLog.Text = fullPath; }));
                string fileName = Path.GetFileName(fullPath);
                string dateOfFile = fileName.Substring(fileName.Length - 12, 8);
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

        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                checkFileLog(e.FullPath);
            }
            catch (Exception ex)
            {
                ShowMessage("FAIL", "NG", ex.Message.ToString());
            }
        }

        private string _timeChangedFile = "";
        private void ReadFileLog(string fullPath)
        {
            try
            {
                string line = Ultils.ReadLastLine(fullPath);
                var lines = line.Split(',');
                var barcode = lines[0];
                string machine = Ultils.GetValueRegistryKey(KeyName.MACHINE);
                var timeChangeFile = "";
                if (machine == Machine.FLORA)
                {
                    timeChangeFile = lines[2];
                }
                else if (machine == Machine.ZAKURO)
                {
                    timeChangeFile = lines[3];
                }

                if (timeChangeFile == _timeChangedFile) return;
                _timeChangedFile = timeChangeFile;
                string stationCurrent = Ultils.GetValueRegistryKey(KeyName.STATION_NO);
                var productId = "";
                var boardState = "";
                if (machine == Machine.FLORA)
                {
                    int indexOfUnderscore = barcode.IndexOf("_");
                    productId = barcode.Substring(indexOfUnderscore + 1, barcode.Length - indexOfUnderscore - 1);
                    boardState = lines[4].ToUpper();
                }
                else if (machine == Machine.ZAKURO)
                {
                    productId = barcode;
                    boardState = lines[5].ToUpper();
                }
               
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
                string machine = Ultils.GetValueRegistryKey(KeyName.MACHINE);
                string path = Ultils.GetValueRegistryKey(KeyName.PATH_INPUT);
                if (machine == Machine.FLORA)
                {
                    DateTime currentDate = DateTime.Now;
                    string dateConvert = currentDate.ToString("yyyyMMdd");
                    path += "\\" + dateConvert;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
               
                watcher.Path = path;
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
            bgrwSoftInfo = new BackgroundWorker();
            bgrwSoftInfo.DoWork += BgrwSoftInfo_DoWork;
            bgrwSoftInfo.RunWorkerCompleted += BgrwSoftInfo_RunWorkerCompleted;
            bgrwSoftInfo.RunWorkerAsync();
        }


        private void BgrwSoftInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as Base_Soft_InfoEntity;
            var rs = _pvs_service.RegisterSoftInfo(result);
            lblSoftInfo.Text = rs ? "OK" : "NG";
        }

        private void BgrwSoftInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            var param = new Base_Soft_InfoEntity()
            {
                ID = Common.GetGUIID(),
                HOST_NAME = Environment.MachineName,
                MAC_ADDRESS = Common.GetMACAddress(),
                IP_ADDRESS = Common.GetLocalIPAddress(),
                IS_WINDOWS_64 = Environment.Is64BitOperatingSystem,
                LOCAL_USER = Environment.UserName,
                SOFT_NAME = Text,
                UPDATE_TIME = _pvs_service.GetDateTime(),
                VERSION = Common.GetRunningVersion(),
                WINDOWS_EDITION = Common.GetOSFriendlyName(),
                IS_WINDOWS_ACTIVE = Common.IsWindowsActivated()
            };
            param.ID += param.MAC_ADDRESS;
            e.Result = param;
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
