using BillReader.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WindowsPreview.Media.Ocr;

namespace BillReader.Classes.OCREngine
{
    public class OCREngine
    {
        private OcrEngine ocrEngine;
        private CameraCapturer _cameraCapturer;
        public VoiceEngine.VoiceEngine voiceEngine;
        private BillReader.BillReader billReader;
        public OCREngine(CameraCapturer cameraCapturer, OcrLanguage language)
        {
            _cameraCapturer = cameraCapturer;
            _cameraCapturer.ocrEngine = this;
            billReader = new BillReader.BillReader(cameraCapturer);
            voiceEngine = new VoiceEngine.VoiceEngine(cameraCapturer);
            Init(language);
        }
        public OCREngine(CameraCapturer cameraCapturer)
        {
            _cameraCapturer = cameraCapturer;
            voiceEngine = new VoiceEngine.VoiceEngine(cameraCapturer);
            _cameraCapturer.ocrEngine = this;
        }

        public void Init(OcrLanguage language)
        {
            ocrEngine = new OcrEngine(language);
        }

        public async void Run(Windows.Graphics.Imaging.PixelDataProvider pixelProvider, uint height, uint width)
        {
            OcrResult result;
            try
            {
                result = await ocrEngine.RecognizeAsync(height, width, pixelProvider.DetachPixelData());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
            Debug.WriteLine("result.lines");
            if (result.Lines != null)
            {
                string recognizedText = "";
                foreach (var line in result.Lines)
                {
                    foreach (var word in line.Words)
                    {
                        recognizedText += word.Text + " ";
                    }
                }
                Debug.WriteLine(recognizedText);

                billReader.Run(result);
                _cameraCapturer.Result.Text = recognizedText;
            }
        }
    }
}
