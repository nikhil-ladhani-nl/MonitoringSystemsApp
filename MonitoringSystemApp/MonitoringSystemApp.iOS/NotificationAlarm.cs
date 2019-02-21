using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AVFoundation;
using Foundation;
using MonitoringSystemApp.iOS;
using MonitoringSystemApp.Models.Login;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationAlarm))]

namespace MonitoringSystemApp.iOS
{
    public class NotificationAlarm : NSObject, INotificationAlarm, IAVAudioPlayerDelegate
    {
            public NotificationAlarm()
            {
            }

            public void PlayAlarm(string fileName)
            {
                string sFilePath = NSBundle.MainBundle.PathForResource
                (Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
                var url = NSUrl.FromString(sFilePath);
                var _player = AVAudioPlayer.FromUrl(url);
                _player.FinishedPlaying += (object sender, AVStatusEventArgs e) => {
                    _player = null;
                };
                _player.Play();
            }

            public void StopAlarm(string fileName)
            {
                string sFilePath = NSBundle.MainBundle.PathForResource
                (Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
                var url = NSUrl.FromString(sFilePath);
                var _player = AVAudioPlayer.FromUrl(url);
                _player.FinishedPlaying += (object sender, AVStatusEventArgs e) => {
                    _player = null;
                };
                _player.Stop();
            }
       
    }
}