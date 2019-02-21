using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonitoringSystemApp.Droid;
using MonitoringSystemApp.Models.ListView;
using Newtonsoft.Json;
using static MonitoringSystemApp.Models.Login.Users;

[assembly: Xamarin.Forms.Dependency(typeof(SaveConfigLocally))]

namespace MonitoringSystemApp.Droid
{
   class SaveConfigLocally: SaveToFile
   {
      //function used to read the file that is saved on the device and fetch the saved credentials for login
      public MonitorUser ReadFile(string fileName)
      {  //the path to the storage for the file which is : system\emulated\0 as default
         var documentsPath = Android.OS.Environment.ExternalStorageDirectory.ToString();
         //the name of the folder created on the device
         var dirName = System.IO.Path.Combine(documentsPath.ToString(), "MonitorSystemConfig");
         //the name of the file created on the device
         string fullFilePathName = System.IO.Path.Combine(dirName.ToString(), "MonitorSystemConfig.dat");
         //try and catch function used to read the information that is stored in the file and as it in JSON format then converted to the proper format   
         try
         {
            TextReader reader = new StreamReader(fullFilePathName);
            string jsonConfig = reader.ReadLine();
            reader.Close();

            MonitorUser monitorUser  =  JsonConvert.DeserializeObject<MonitorUser>(jsonConfig);

            return monitorUser;
         }
         catch
         {
            return null;
         }
        
      }

      public void SaveToFile(string fileName, string configJSON)
      {
         try
         {
            var filePath = "";
            //the path to the storage for the file which is : system\emulated\0 as default
            //create directory if it does not exist
            var documentsPath = Android.OS.Environment.ExternalStorageDirectory.ToString();
            //the name of the folder to create on the path on the device
            var dirName = System.IO.Path.Combine(documentsPath.ToString(), "MonitorSystemConfig");
            Directory.CreateDirectory(dirName);
           //the name of the file to be created as a data file on the device
            filePath = System.IO.Path.Combine(dirName, "MonitorSystemConfig.dat");
            TextWriter tw = new StreamWriter(filePath);
            //The data to write on the device which will be used in the login page
            tw.Write(configJSON);
            tw.Close();
         }
         catch(Exception ex)
         {
            throw;
         }
      }
   }
}