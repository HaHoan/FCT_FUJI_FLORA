using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FCT_FUJI_FLORA.Business
{
    public static class Ultils
    {
        public static string GetRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string GetValueRegistryKey(string keyName)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.PATH_CONFIG);
            string value = null;
            if (key != null)
            {
                if (key.GetValue(keyName) != null)
                {
                    value = key.GetValue(keyName).ToString();
                    key.Close();
                    return value;
                }
            }

            return null;
        }

        public static void WriteRegistry(string stringValue, string content)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(Constants.PATH_CONFIG);
            if (!string.IsNullOrEmpty(stringValue) && !string.IsNullOrEmpty(content))
            {
                key.SetValue(stringValue, content);
                key.Close();
            }
        }

        public static string ReadLastLine(string path)
        {
            try
            {
                return File.ReadLines(path).Last();

            }
            catch (Exception e)
            {

                return "";
            }

        }

        public static bool IsCreateFileLog(string model, string productId, string status, string process, DateTime dateCheck)
        {
            string dateTime = dateCheck.ToString("yyMMddHHmmss");
            string fileName = $"{dateTime}_{model}_{productId}.txt";
            string folderRoot = $@"{GetValueRegistryKey(KeyName.PATH_OUTPUT)}\";

            bool exists = Directory.Exists(folderRoot);
            if (!exists)
                Directory.CreateDirectory(folderRoot);

            string path = folderRoot + fileName;
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine($"{model}|{productId}|{dateTime}|{status}|{process}");
                        tw.Close();
                        return true;
                    }
                }
                else if (File.Exists(path))
                {
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine($"{model}|{productId}|{dateTime}|{status}|{process}");
                        tw.Close();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

    }
}
