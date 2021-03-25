using combit.ListLabel25;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAndLabelWpfSample
{
    public class PrinterService
    {
        public void DesignListAndLabel(List<FlipDataSource> data, string labelFilePath, string labelFile, string printerName, int startPosition = 0)
        {
            using (ListLabel ll = new ListLabel())
            {
                FlipPrintDataToLaserFlips(data);
                SetUpListAndLabel(ll, data, startPosition);

                if (!string.IsNullOrEmpty(labelFile))
                    ll.Design("", LlProject.Label, GetFullLabelFilePath(labelFilePath, labelFile), false);
                else
                    ll.Design(LlProject.Label);
            }
        }

        public void PrintListAndLabel(List<FlipDataSource> data,
            string labelFilePath,
            string labelFile,
            string printerName,
            int startPosition = 0
            )
        {
            using (ListLabel ll = new ListLabel())
            {
                FlipPrintDataToLaserFlips(data);
                SetUpListAndLabel(ll, data, startPosition);

                ll.Print(printerName, LlProject.Label, GetFullLabelFilePath(labelFilePath, labelFile));
            }
        }

        private void SetUpListAndLabel(ListLabel ll, List<FlipDataSource> data, int startPosition = 0)
        {
            //get license
            var llKey = GetKeyFromConfig();
            ll.LicensingInfo = llKey;

            List<FlipDataSource> flips = new List<FlipDataSource>();
            if (startPosition > 1)
            {
                for (int i = 0; i < startPosition - 1; i++)
                {
                    flips.Add(new FlipDataSource { });
                }
            }

            flips.AddRange(data);

            ll.SetDataBinding(flips, string.Empty);
            
            // define to the databinding mode
            ll.DataBindingMode = DataBindingMode.Compatible;

            ll.AutoShowSelectFile = false;
            ll.AutoDesignerPreview = true;
            ll.AutoShowPrintOptions = false;

        }

        public List<string> GetPrinters()
        {
            List<string> printers = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (!printer.ToLower().Contains("zebra"))
                    printers.Add(printer);
            }
            return printers;
        }

        private string GetFullLabelFilePath(string labelFilePath, string labelFile)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(labelFile);
            if (fi.Exists)
            {
                return labelFile;
            }


            return $"{labelFilePath}{labelFile}";
        }

        private void FlipPrintDataToLaserFlips(List<FlipDataSource> flips)
        {
            //simplification of method used for generic conversions and image generation
            foreach (var flip in flips)
            {
                switch (flip.FlipTypeEnum)
                {
                    case FlipType.Box:
                        flip.FlipType = "7Line";
                        break;
                    case FlipType.ItemFlip:
                        flip.FlipType = string.IsNullOrEmpty(flip.Line6) ? "6Line" : "7Line";
                            flip.QRCode = GetSampleImageFromConfig();
                        break;
                    case FlipType.Sealer:
                        flip.FlipType = "7Line";
                        break;
                }
            }
        }

        private string GetKeyFromConfig()
        {
            return ConfigurationManager.AppSettings["LLKEY"];
        }

        private string GetSampleImageFromConfig()
        {
            return ConfigurationManager.AppSettings["sampleImagePath"];
        }

        public List<string> GetLabels()
        {
            return new List<string>
            {
                "LL25 Updated.lbl",
                "LL15 Original.lbl",
                "LL25 From Scratch.lbl"
            };
        }
    }
}
