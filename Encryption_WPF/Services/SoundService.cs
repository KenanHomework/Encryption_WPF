using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BlockAlignReductionStream = NAudio.Wave.BlockAlignReductionStream;
using PlaybackState = NAudio.Wave.PlaybackState;
using WaveCallbackInfo = NAudio.Wave.WaveCallbackInfo;
using WaveOut = NAudio.Wave.WaveOut;
using WaveStream = NAudio.Wave.WaveStream;

namespace Encryption_WPF.Services
{
    public abstract class SoundService
    {
        public static MediaPlayer MediaPlayer = new MediaPlayer();
        public static void Succes() => PlaySoundWithUrl(App.SuccesSoundEffect);
        public static void Error() => PlaySoundWithUrl(App.ErrorSoundEffect);
        public static void Notification() => PlaySoundWithUrl(App.NotificationSoundEffect);

        public static void PlaySoundWithUrl(string url)
        {
            //MediaPlayer.Open(new Uri(url));
        }
    }
}
