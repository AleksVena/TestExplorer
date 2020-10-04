using Lesson1.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lesson1.Models
{
    public class Copy : IActions
    {
        public List<Element> listObjectsForCopy { get; set; }
        public string destinationFolder { get; set; }

        public bool DoIt()
        {
            bool result;
            CopyItems();
            result = true;
            return result;
        }

        public virtual void CopyItems()
        {
            foreach (string SourcePath in listObjectsForCopy.Where(o => o.isFolder).Select(c => c.fullName))
            {
                string df = String.Format(@"{0}\{1}", destinationFolder, Path.GetFileName(SourcePath));
                foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                    SearchOption.AllDirectories))
                {
                    string newFileName = newPath.Replace(SourcePath, df);
                    Directory.CreateDirectory(Path.GetDirectoryName(newFileName));
                    File.Copy(newPath, newFileName, true);
                }
            }
            foreach (string SourcePath in listObjectsForCopy.Where(o => !o.isFolder).Select(c => c.fullName))
            {
                 File.Copy(SourcePath, SourcePath.Replace(Path.GetDirectoryName(SourcePath), destinationFolder), true);
            }
        }
    }
}
