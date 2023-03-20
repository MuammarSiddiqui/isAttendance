using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace isAttendance
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 300000; //number in milisecinds
            timer.Start();
            timer.Enabled = true;
            WriteToFile("Starting Services");
            try
            {
                var res = MyClass.getAll();
                WriteToFile("Attempting to post data ..." + res.ToString());
                var abc = await UploadData(res);
                if (!abc)
                {
                    WriteToFile("Error While Posting Data");
                }
            }
            catch (Exception e)
            {
                WriteToFile("Exception " + e);
                WriteToFile(e.Message);
                WriteToFile(e.InnerException.ToString());
                WriteToFile(e.StackTrace);
            }

        }
        public static async Task<bool> UploadData(List<AttendanceLog> abc)
        {
            try
            {

                string URI = StaticValues.GetUrl();

                var json = JsonConvert.SerializeObject(abc);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
    (se, cert, chain, sslerror) =>
    {
        return true;
    };
                    client.BaseAddress = new Uri(URI);

                    var res = await client.PostAsync(URI, data);

                }
                return true;
            }
            catch (Exception ex)
            {
                WriteToFile(ex.Message);
                WriteToFile(ex.InnerException.ToString());
                WriteToFile(ex.ToString());
                return false;
            }
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
            timer.Interval = 10000; //number in milisecinds
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Start();
        }
        private async void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service recall at " + DateTime.Now); timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 300000; //number in milisecinds
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Start();
            WriteToFile("Starting Services");
            try
            {
                var res = MyClass.getAll();
                WriteToFile("Attempting to post data ..." + res.ToString());
                var abc = await UploadData(res);
                if (!abc)
                {
                    WriteToFile("Error While Posting Data");
                }
            }
            catch (Exception ex)
            {
                WriteToFile("Exception " + ex);
                WriteToFile(ex.Message);
                WriteToFile(ex.InnerException.ToString());
                WriteToFile(ex.StackTrace);
            }
        }
        public static void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
