using BillReader.Classes.OCREngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BillReader.View
{
    public sealed partial class CameraCapturer : UserControl
    {
        public TextBlock Result { get { return result; } set { result = value; } }
        public MediaElement mediaElement { get { return player; } set { player = value; } }
        public OCREngine ocrEngine { get; set; }
        private MediaCapture captureMgr { get; set; }
        public CameraCapturer()
        {
            this.InitializeComponent();
        }

        public async Task<bool> Init()
        {
            try
            {
                captureMgr = new MediaCapture();
                await captureMgr.InitializeAsync();
                captureMgr.SetPreviewRotation(VideoRotation.Clockwise90Degrees);

                captureMgr.SetRecordRotation(VideoRotation.Clockwise90Degrees);
                VideoRotation previewRotation = captureMgr.GetPreviewRotation();
                int max = 0;
                VideoEncodingProperties resolutionMax = null;
                var resolutions = captureMgr.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo);

                for (var i = 0; i < resolutions.Count; i++)
                {
                    VideoEncodingProperties res = (VideoEncodingProperties)resolutions[i];
                    Debug.WriteLine("resolution : " + res.Width + "x" + res.Height);
                    if (res.Width * res.Height < 1200)
                    {
                        max = (int)(res.Width * res.Height);
                        resolutionMax = res;
                    }
                }
                await captureMgr.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, (VideoEncodingProperties)resolutions[2]);
                this.capturePreview.Source = captureMgr;
                await captureMgr.StartPreviewAsync();
                return true;
                //                capturePreview.
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        
        private async void result_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string captureFileName = string.Empty;
            //declare image format
            ImageEncodingProperties format = ImageEncodingProperties.CreateJpeg();
            using (var imageStream = new InMemoryRandomAccessStream())
            {
                //generate stream from MediaCapture
                await captureMgr.VideoDeviceController.FocusControl.FocusAsync();

                await captureMgr.CapturePhotoToStreamAsync(format, imageStream);
                BitmapDecoder dec = await BitmapDecoder.CreateAsync(imageStream);
                BitmapTransform transform = new BitmapTransform();
                BitmapPixelFormat pixelFormat = dec.BitmapPixelFormat;
                BitmapAlphaMode alpha = dec.BitmapAlphaMode;
                PixelDataProvider pixelProvider = await dec.GetPixelDataAsync(
                             BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Straight,
            new BitmapTransform(),
            ExifOrientationMode.RespectExifOrientation,
            ColorManagementMode.ColorManageToSRgb);
                Debug.WriteLine(dec.PixelHeight);
                Debug.WriteLine(dec.PixelWidth);
                ocrEngine.Run(pixelProvider, dec.PixelHeight, dec.PixelWidth);
            }
        }
    }
}
