using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Lesson1.Models
{
    public class Element
    {
        public string fullName { get; set; }
        public string shortName { get; set; }
        public bool isFolder { get; set; }
        public BitmapSource icon { get; set; }
        public string size { get; set; }
        public bool IsSelected { get; set; }

        public static List<Element> GetElements(string path)
        {
            List<Element> list = new List<Element>();

            if (path != null)
            {
                list.AddRange(Directory.GetDirectories(path).Select(x => new Element()
                {
                    fullName = x,
                    isFolder = true,
                    shortName = Path.GetFileName(x),
                    size = string.Empty,
                    icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                         DefaultIcons.ExtractFromPath(x).ToBitmap().GetHbitmap(),
                         IntPtr.Zero,
                         Int32Rect.Empty,
                         BitmapSizeOptions.FromEmptyOptions())
                }));

                list.AddRange(Directory.GetFiles(path).Select(x => new Element()
                {
                    fullName = x,
                    isFolder = false,
                    shortName = Path.GetFileName(x),
                    size = (new FileInfo(x).Length / 1024).ToString("0"),
                    icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                         DefaultIcons.ExtractFromPath(x).ToBitmap().GetHbitmap(),
                         IntPtr.Zero,
                         Int32Rect.Empty,
                         BitmapSizeOptions.FromEmptyOptions())
                }));

            }
            return list;
        }

    }
}
