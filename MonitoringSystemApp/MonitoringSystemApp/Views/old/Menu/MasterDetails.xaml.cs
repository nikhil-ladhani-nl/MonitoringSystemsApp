using MonitoringSystemApp.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonitoringSystemApp.Views.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetails : MasterDetailPage
	{
		public MasterDetails ()
		{
			InitializeComponent ();
            settingspage.ListView.ItemSelected += OnItemSelected;
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SettingsBar;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                settingspage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}