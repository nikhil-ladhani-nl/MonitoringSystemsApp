using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonitoringSystemApp.Models.ListView;
using MonitoringSystemApp.Models.Login;
using MonitoringSystemApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonitoringSystemApp.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class LogsPage: ContentPage
   {
      //variables
      LogsViewModel vm;
      //creating objects using a list  
      List<Logs> logs = new List<Logs>();
      public LogsPage(string text, int intervalInSeconds)
      {
         InitializeComponent();
         //username displayed on the logs page
         unLabel.Text = "Welcome " + text;

         //fetching items for the listview
         vm = new LogsViewModel();
         BindingContext = vm;

         //timer for auto refresh using refresh data function
         Device.StartTimer(TimeSpan.FromSeconds(intervalInSeconds), refreshData);

      }


      public bool refreshData()
      {
         //method called
         CheckServersStatus();
         return true;
      }


      //method used in the logs page to check the status on what goes on the server side fetched from a database  
      private void CheckServersStatus(string token = "xyz")
      {
         string responseString = "";
         using (var client = new HttpClient())
         {
            //url used to fetch the results if it is a success or an error
            string url = "http://askari.edis.co.nz/api/CheckServers?token=" + token;
           
            //requesting the server with a script without needing a reply
            var response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
               var responseContent = response.Content;

               // by calling .Result you are synchronously reading the result
               responseString = responseContent.ReadAsStringAsync().Result;

            }
         }

         if (responseString.Contains("Error"))
         {
            //the database contains different messages on a csv type format needed to be formatted to be displayed properly
            string[] errorArray = responseString.Split(',');
            for (int i = 0; i < errorArray.Length; i++)
            {
               //using replace to format of the attributes written as
               errorArray[i] = errorArray[i].Replace("[", "").Replace("]", "").Replace("\\", "").Replace("/", "").Replace("\"", "");
            }
            //3 different messages filtered out one at a time as per the message and if it is an error or not and displayed on the listview
            //after a period of time
            string ErrorMessage = "";
            for (int i = 0; i <= errorArray.Length - 3; i = i + 3)
            {
               if(errorArray[i+1] == "Error")
               {
                  ErrorMessage = errorArray[i] + ": " + errorArray[i + 2] + "\n";
               }
            }
            //adds a error log on the listview if there is a problem on the server side with the message set up on the database on the server side
            AddLog(false, ErrorMessage.Trim());
            //dependency service used to play an alarm for every error that is displayed on the list view
            DependencyService.Get<INotificationAlarm>().PlayAlarm("alarm.mp3");
         }
         else
         {
            //adds a success log on the listview if there is no problem on the server side with the message set up on the database on the server side
            AddLog(true, "Success");
            //dependency service used to stop the alarm after a success appears on the list as there is no error on the server
            DependencyService.Get<INotificationAlarm>().StopAlarm("alarm.mp3");

         }

      }

      //function to add the add the logs with the given attributes such as the name, datetime, and the icon as a checkmark if a success otherwise
      //a cross if there is an error
      private void AddLog(bool b, string alarmMessage)
      {
         if (b)
         {
            //success
            logs.Insert(0, new Logs() { Name = alarmMessage, DateTime = DateTime.Now, Icon = "http://www.myiconfinder.com/uploads/iconsets/256-256-c0829a49b2acd49adeab380f70eb680a-accept.png" });
            //logs.Add(new Logs() { Name = "Success", DateTime = DateTime.Now, Icon = "http://www.myiconfinder.com/uploads/iconsets/256-256-c0829a49b2acd49adeab380f70eb680a-accept.png" });
         }
         else
         {
           //error
            logs.Insert(0, new Logs() { Name = alarmMessage, DateTime = DateTime.Now, Icon = "http://www.myiconfinder.com/uploads/iconsets/256-256-4509ba2b6663b1e81748e24eb16204d2-cross.png" });
            //logs.Add(new Logs() { Name = "Error", DateTime = DateTime.Now, Icon = "http://www.myiconfinder.com/uploads/iconsets/256-256-4509ba2b6663b1e81748e24eb16204d2-cross.png" });
         }

         listLogs.ItemsSource = null;

         listLogs.ItemsSource = logs;
      }
      
      //this function is used when the alarm is playing as it is on a loop configured on the android and iOS solutions by a button click
      //using a dependency service
      private void btnStop_Clicked(object sender, EventArgs e)
      {
         DependencyService.Get<INotificationAlarm>().StopAlarm("alarm.mp3");
      }

   }
}