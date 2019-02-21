using MonitoringSystemApp.Models.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonitoringSystemApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string name = unEntry.Text;
            string house = pwdEntry.Text;


            // Use https to satisfy iOS ATS requirements.
            var client = new HttpClient();
            var response = await client.GetAsync("http://hp-api.herokuapp.com/api/characters");
            var responseString = await response.Content.ReadAsStringAsync();
            var userList = JsonConvert.DeserializeObject<List<Users>>(responseString);
            bool isFound = false;

            //Fetchiing name and house using linq
            var u = userList.Where(x => x.UserName == name && x.Password == house).FirstOrDefault();

            if (u != null)
            {
                await DisplayAlert("Login", "Login Successful", "OK");
                await Navigation.PushModalAsync(new MainPage(name));

            }
            else
            {
                DependencyService.Get<INotificationAlarm>().PlayAlarm("alarm.mp3");

                await DisplayAlert("Login", "Login Error", "OK");
            }

            //Usable but Long and Dirty Code

            //foreach (User user in userList)
            //{
            //    if (user.name == name && user.house == house)
            //    {
            //        await DisplayAlert("Login", "Login Successful", "OK");
            //        await Navigation.PushModalAsync(new MainPage(name));
            //        isFound = true;
            //    }

            //    else
            //    {
            //        if(user.name != name)
            //        {
            //            continue;
            //        }
            //        await DisplayAlert("Login", "Login Error", "OK");
            //    }

            //    if (isFound)
            //        break;
            //}

        }

        //remember me - store user credentials
        private void rememberBox_Toggled(object sender, ToggledEventArgs e)
        {
            string name = unEntry.Text;
            string house = pwdEntry.Text;
            var AuthService = DependencyService.Get<IRememberMe>();
            if (rememberBox.IsToggled)
            {
                bool doCredentialsExist = AuthService.DoCredentialsExist();
                if (!doCredentialsExist)
                {
                    AuthService.SaveCredentials(name, house);
                }
            }
        }
    }
}