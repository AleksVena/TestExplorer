using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lesson1.Models
{
    public class Disc
    {
        public string name { get; set; }
        public bool isReady { get; set; }
        public string totalSize { get; set; }
        public string freeSize { get; set; }
        public DriveType driveType { get; set; }
        public string fileSystem { get; set; }
        public BitmapSource icon { get; set; }

        public static List<Disc> getDiscs()
        {
            return DriveInfo.GetDrives().Select(x => new Disc() { 
                name = x.Name, 
                isReady = x.IsReady, 
                totalSize = x.IsReady ? String.Format("{0}GB", (((x.TotalSize / 1024)/1024)/1024).ToString("0")) : string.Empty, 
                freeSize = x.IsReady ? String.Format("{0}GB", (((x.AvailableFreeSpace / 1024) / 1024)/ 1024).ToString("0")) : string.Empty, 
                driveType = x.DriveType,  
                fileSystem = x.IsReady ? x.DriveFormat : string.Empty,
                icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                         DefaultIcons.ExtractFromPath(x.Name).ToBitmap().GetHbitmap(),
                         IntPtr.Zero,
                         Int32Rect.Empty,
                         BitmapSizeOptions.FromEmptyOptions()) 
            }).ToList();            
        } 

        

    }
}
