﻿

#pragma checksum "E:\c#\BillReader\BillReader\BillReader.Shared\View\CameraCapturer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6395D328A527013DD4E0725DB98E19F0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillReader.View
{
    partial class CameraCapturer : global::Windows.UI.Xaml.Controls.UserControl
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.MediaElement player; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.CaptureElement capturePreview; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock result; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///View/CameraCapturer.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            player = (global::Windows.UI.Xaml.Controls.MediaElement)this.FindName("player");
            capturePreview = (global::Windows.UI.Xaml.Controls.CaptureElement)this.FindName("capturePreview");
            result = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("result");
        }
    }
}


