using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonitoringSystemApp.Models.ListView;
using MonitoringSystemApp.Models.Login;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MonitoringSystemApp.Models.Login.Users;

namespace MonitoringSystemApp.Views.LoginPage
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class LoginPage: ContentPage
   {
      //variables
      private bool isSaveToConfigFile;

      MonitorUser user = null;

      public LoginPage()
      {
         //this function implements a remember me function by using a dependency service grabbed from the android/ios platforms
         user = DependencyService.Get<SaveToFile>().ReadFile("");
         //checks whether the file is saved on the device
         isSaveToConfigFile = true;
         InitializeComponent();
         //the remember me will always be enabled for the user to save their credentials on the device and automatically logins in without
         //typing in the login credentials
         if (user != null && user.RememberMe == true)
         {
            unEntry.Text = user.Name;
            pwdEntry.Text = user.Password;
            isSaveToConfigFile = false;
            Task.Run(() => this.LoginButton_Clicked(null, null)).Wait();

         }

      }

      //login function
      private async void LoginButton_Clicked(object sender, EventArgs e)
      {
         string name = unEntry.Text;
         string password = pwdEntry.Text;
         await CheckLogin(name, password, user);
      }
       //check whether the user is on the server, if yes then login successful otherwise error and cannot login
      private async Task CheckLogin(string name, string password, MonitorUser monitorUserSaved)
      {
         // Use https to satisfy iOS ATS requirements.
         var client = new HttpClient();
         // var response = await client.GetAsync("http://hp-api.herokuapp.com/api/characters");
         
         //the url used to call the username and password from the API
         string url = "http://askari.edis.co.nz/api/users?api_username=edis&api_password=edispassword&employee_username=" + name + "&employee_password=" + password;
         
         //requesting the server with a script without needing a reply
         var response = await client.GetAsync(url);
         var responseString = await response.Content.ReadAsStringAsync();
         
         //using JSON to convert the attributes to the correct format from the server
         MonitorUser monitorUser = JsonConvert.DeserializeObject<MonitorUser>(responseString);

         //Fetchiing name and house using linq
         //var u = userList.Where(x => x.name == name && x.house == password).FirstOrDefault();

         if (user != null)
         {
            monitorUser.RememberMe = rememberBox.IsToggled;
            responseString = JsonConvert.SerializeObject(monitorUser);
            if (monitorUserSaved != null)
            {
               //users have been set with their prefered refresh intervals stored on the database 
               if (monitorUserSaved.Servers != monitorUser.Servers ||
                              monitorUserSaved.CheckInterevalInSeconds != monitorUser.CheckInterevalInSeconds)
               {
                  isSaveToConfigFile = true;
               }
               //default refresh interval will be 5 minutes unless set on the database on the server side
               if (monitorUserSaved.CheckInterevalInSeconds == 0)
               {
                  monitorUserSaved.CheckInterevalInSeconds = 300;
               }
            }
            //using dependency service to save the user credentials physically on the device if the remember me is toggled
            if (isSaveToConfigFile)
            {
               DependencyService.Get<SaveToFile>().SaveToFile("", responseString);
            }
            //after login the user is taken to the logs page
            await Navigation.PushModalAsync(new LogsPage(name, monitorUserSaved.CheckInterevalInSeconds));

         }
         else if(monitorUser != null)
         {
            monitorUser.RememberMe = rememberBox.IsToggled;
            responseString = JsonConvert.SerializeObject(monitorUser);

            DependencyService.Get<SaveToFile>().SaveToFile("", responseString);
            await Navigation.PushModalAsync(new LogsPage(name, monitorUser.CheckInterevalInSeconds));
         }
         else
         {
            //DependencyService.Get<INotificationAlarm>().PlayAlarm("alarm.mp3");

            await DisplayAlert("Login", "Login Error", "OK");
         }
      }

      //remember me - store user credentials - code not working
      private void rememberBox_Toggled(object sender, ToggledEventArgs e)
      {
         //string name = unEntry.Text;
         //string house = pwdEntry.Text;
         //var AuthService = DependencyService.Get<IRememberMe>();
         //if (rememberBox.IsToggled)
         //{
         //   bool doCredentialsExist = AuthService.DoCredentialsExist();
         //   if (!doCredentialsExist)
         //   {
         //      AuthService.SaveCredentials(name, house);
         //   }
         //}


      }
   }
}