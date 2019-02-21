using MonitoringSystemApp.Models.Settings;
using MonitoringSystemApp.Views;
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
	public partial class SettingsPage : ContentPage
	{
        public ListView ListView
        {
            get { return listview; }
        }
        public SettingsPage()
		{
			InitializeComponent ();
            SetItems();
        }
   
        List<SettingsBar> items;
        public void SetItems()
        {
            items = new List<SettingsBar>();
            items.Add(new SettingsBar(("5 Seconds"), typeof(ListViewPage)));
            ListView.ItemsSource = items;
        }
    }
}