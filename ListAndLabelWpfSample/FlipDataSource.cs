using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAndLabelWpfSample
{
    public class FlipDataSource
    {
        public int OrderID { get; set; }
        public string QRCode { get; set; }
        public bool Holdered { get; set; }
        public string Line0 { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Line5 { get; set; }
        public string Line6 { get; set; }
        public string Line7 { get; set; }
        public string Line8 { get; set; }
        public string MintError { get; set; }
        public string FlipType { get; set; }
        public string AdditionalData { get; set; }
        public string Prefix { get; set; }
        public FlipType FlipTypeEnum { get; set; }
    }
    public enum FlipType
    {
        Box,
        ItemFlip,
        OrderNo,
        Sealer
    }
}
