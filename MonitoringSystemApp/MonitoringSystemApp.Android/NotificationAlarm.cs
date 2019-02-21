using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonitoringSystemApp.Droid;
using MonitoringSystemApp.Models.Login;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationAlarm))]

namespace MonitoringSystemApp.Droid
{
   class NotificationAlarm: INotificationAlarm
   {
      //variables
      static bool isAlarmRunning = false;

      static MediaPlayer player = null;
      //android function specifically to start the alarm if there is an error on the device which is inherited by the interface INotification Alarm
      public void PlayAlarm(string fileName)
      {
         if (isAlarmRunning)
         {
            return;
         }


         player = new MediaPlayer();

         isAlarmRunning = true;
         //the alarm mp3 file stored in the assets folder is called upon
         var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);

         player.Prepared += (s, e) =>
         {
            player.Start();
            player.Looping = true;
            player.SetVolume(100,100);
         };

         player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
         player.Prepare();
      }
       //same method as the play alarm but this one used to stop the alarm from playing when on loop
      public void StopAlarm(string fileName)
      {
         if (isAlarmRunning)
         {
            player.Stop();
         }
         isAlarmRunning = false;
      }
   }
}