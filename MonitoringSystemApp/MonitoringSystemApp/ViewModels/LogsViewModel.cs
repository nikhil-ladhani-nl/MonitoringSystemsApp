using MonitoringSystemApp.Models.ListView;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringSystemApp.ViewModels
{
    public class LogsViewModel
    {
        public List<Logs> Logs { get; set; }

        public LogsViewModel()
        {
            Logs = new Logs().GetLogs();
        }
    }
}
