using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringSystemApp.Models.Login
{
    //interface to use dependancy services on android and iOS environment 
    //using 2 functions that will be inherited in the android and iOS platforms
    public interface INotificationAlarm
    {
        void PlayAlarm(string fileName);
       void StopAlarm(string fileName);
    }
}
