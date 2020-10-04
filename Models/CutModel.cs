using Lesson1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lesson1.Models
{
    public class CutModel : Copy, IActions
    {
        public override void CopyItems()
        {
            base.CopyItems();
            DeleteModel.Delete(listObjectsForCopy);
        }
    }
}
