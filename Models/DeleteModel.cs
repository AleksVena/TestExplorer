using Lesson1.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Models
{
    public class DeleteModel : IActions
    {
        public List<Element> listObjects { get; set; }
        public bool DoIt()
        {
            bool result;
            Delete(listObjects);
            result = true;
            return result;
        }

        public static void Delete(List<Element> listObjects)
        {
            foreach (string SourcePath in listObjects.Where(o => o.isFolder).Select(c => c.fullName))
            {
                var allF = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories);
                foreach (string newPath in allF)
                    File.Delete(newPath);

                Directory.Delete(SourcePath, true);
            }
            foreach (string SourcePath in listObjects.Where(o => !o.isFolder).Select(c => c.fullName))
            {
                File.Delete(SourcePath);
            }
        }
    }
}
