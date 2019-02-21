using System;
using System.Collections.Generic;
using System.Text;
using static MonitoringSystemApp.Models.Login.Users;

namespace MonitoringSystemApp.Models.ListView
{
    //interface to use dependancy services on android and iOS environment 
    public interface SaveToFile
    {
        //method names with constructors as objects
        void SaveToFile(string fileName,string configJSON);
        MonitorUser ReadFile(string fileName);
    }
}
