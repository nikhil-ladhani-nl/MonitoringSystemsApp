using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MonitoringSystemApp.Models.ListView
{
    public class Logs
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public ImageSource Icon { get; set; }

        public List<Logs> GetLogs()
        {
            List<Logs> logs = new List<Logs>(){};
            return logs;
        }
    }
}
