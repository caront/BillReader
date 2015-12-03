using BillReader.Utils;
using BillReader.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WindowsPreview.Media.Ocr;

namespace BillReader.Classes.BillReader
{
    public class BillReader
    {
        private VoiceEngine.VoiceEngine _voiceEngine;
        private List<OcrWord> listWord;
        private List<OcrWord> listPrice;

        public BillReader(CameraCapturer cC)
        {
            _voiceEngine = new VoiceEngine.VoiceEngine(cC);
        }

        private void DisplayNumber()
        {
            Debug.WriteLine("numbers find : ");
            foreach (var s in listWord)
            {
                Debug.WriteLine(s.Text);
            }
        }

        private void FindPrice()
        {
            listPrice = new List<OcrWord>();
            if (listWord.Count == 0)
            {
                Debug.WriteLine("==== final price ====");
                Debug.WriteLine("");
                _voiceEngine.Run("No encontrar el precio, Inténtalo de nuevo");
                return;
            }

            OcrWord tmp = listWord[0];
            int size = 0;

            foreach (var world in listWord)
            {
                if (world.Height > size)
                {
                    size = world.Height;
                    tmp = world;
                }
            }
            Debug.WriteLine("==== final price ====");
            Debug.WriteLine(tmp.Text);
            _voiceEngine.Run("El precio final es " + listWord[0].Text.ToString());
        }

        public void Run(OcrResult result)
        {
            _voiceEngine.Run("Comience para encontrar el precio");
            bool searchTotal = true;
            listWord = new List<OcrWord>();
            foreach (OcrLine line in result.Lines)
            {
                Debug.WriteLine(line.ToString());
                foreach (OcrWord word in line.Words)
                {
                    if (searchTotal)
                    {
                        //search word who contain "TOTAL"
                        if (word.Text.ToUpper().Contains("TOTAL"))
                        {
                            //searchPrice
                            searchTotal = false;
                        }
                    }
                    else
                    {
                        if (word.Text.ContainNumber() && word.Text.ContainDouble())
                        {
                            listWord.Add(word);
                        }
                    }
                }
            }
            DisplayNumber();
            FindPrice();
        }
    }
}
