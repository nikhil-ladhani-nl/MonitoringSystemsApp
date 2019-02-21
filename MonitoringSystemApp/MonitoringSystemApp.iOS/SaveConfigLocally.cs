using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using MonitoringSystemApp.Models.ListView;
using MonitoringSystemApp.Models.Login;
using Newtonsoft.Json;
using UIKit;
using static MonitoringSystemApp.Models.Login.Users;

namespace MonitoringSystemApp.iOS
{
    public class SaveConfigLocally : SaveToFile
    {
        //function used to read the file that is saved on the device and fetch the saved credentials for login
        public MonitorUser ReadFile(string fileName)
        {   //the path to the storage for the file 
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //the name of the folder created on the device
            var directoryname = Path.Combine(documents, "MonitorSystemConfig" + "/" + fileName);
            //the name of the file created on the device
            string fullFilePathName = System.IO.Path.Combine(directoryname.ToString(), "MonitorSystemConfig.dat");
            //try and catch function used to read the information that is stored in the file and as it in JSON format then converted to the proper format
            try
            {
                TextReader reader = new StreamReader(fullFilePathName);
                string jsonConfig = reader.ReadLine();
                reader.Close();

                MonitorUser monitorUser = JsonConvert.DeserializeObject<MonitorUser>(jsonConfig);

                return monitorUser;
            }
            catch
            {
                return null; ;
            }
        }

        public void SaveToFile(string filename, string configJSON)
        {
            //create directory if it does not exist
            // Serialize object
            var json = JsonConvert.SerializeObject(configJSON, Newtonsoft.Json.Formatting.Indented);
            // Save to file 
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //the name of the folder to create on the path on the device
            var dirName = System.IO.Path.Combine(documents.ToString(), "MonitorSystemConfig");

            var file = Path.Combine(documents, "MonitorSystemConfig.dat");
            TextWriter tw = new StreamWriter(file);
            //The data to write on the device which will be used in the login page
            tw.Write(configJSON);
            tw.Close();
        }
    }
}