using BillReader.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Core;

namespace BillReader.Classes.VoiceEngine
{
    public class VoiceEngine
    {
        private CameraCapturer _cameraCapturer;


        public VoiceEngine(CameraCapturer cC)
        {
            _cameraCapturer = cC;
        }

        public void Run(string text)
        {
            var synthesizer = new SpeechSynthesizer();
            var voices = SpeechSynthesizer.AllVoices;

            foreach (var info in voices)
            {

                Debug.WriteLine(" Language:      " + info.Language);
                Debug.WriteLine(" Name:          " + info.DisplayName);

                Debug.WriteLine(" Gender:        " + info.Gender);
                Debug.WriteLine(" Description:   " + info.Description);
                Debug.WriteLine(" ID:            " + info.Id);
            }
         //   VoiceInformation info = synthesizer.Voice;

            

            //synthesizer.Voice = voices[voice];

            var spokenStream = synthesizer.SynthesizeTextToStreamAsync(text);

            spokenStream.Completed += SpokenStreamCompleted;
        
        }

        private async void SpokenStreamCompleted(Windows.Foundation.IAsyncOperation<SpeechSynthesisStream> asyncInfo, Windows.Foundation.AsyncStatus asyncStatus)
        {
            try
            {
                var results = asyncInfo.GetResults();
                await _cameraCapturer.mediaElement.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new DispatchedHandler(
                    () => { _cameraCapturer.mediaElement.SetSource(results, results.ContentType); }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
