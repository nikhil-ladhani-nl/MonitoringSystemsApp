using MonitoringSystemApp.Models.ListView;
using MonitoringSystemApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonitoringSystemApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewPage : ContentPage
	{
        LogsViewModel vm;

        List<Logs> logs = new List<Logs>();
        public ListViewPage ()
		{
			InitializeComponent ();

            //fetching items for the listview
            vm = new LogsViewModel();
            //listLogs.ItemsSource = vm.Logs;
            BindingContext = vm;
            //Timer for auto refresh using refresh data function
            Device.StartTimer(TimeSpan.FromSeconds(15), refreshData);
        }
        public bool refreshData()
        {
            if (logs != null)
            {
                logs.Add(new Logs() { Name = "Success", DateTime = DateTime.Now, Icon = "http://www.myiconfinder.com/uploads/iconsets/05409bbfe3da6e971e94af5725956810.png" });
            }
            else
            {
                logs.Add(new Logs() { Name = "Error", DateTime = DateTime.Now, Icon = "http://www.myiconfinder.com/uploads/iconsets/05409bbfe3da6e971e94af5725956810.png" });
            }
            listLogs.ItemsSource = null;
            listLogs.ItemsSource = logs;
            return true;
        }
    }
}