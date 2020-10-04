using Lesson1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Models
{
    public class StartAction
    {
        public static void make(string action, List<Element> listObjectsForCopy, string destinationFolder)
        {            
            bool result =
                action.Equals("copy") ? start(new Copy() { destinationFolder = destinationFolder, listObjectsForCopy = listObjectsForCopy }) :
                action.Equals("cut") ? start(new CutModel() { destinationFolder = destinationFolder, listObjectsForCopy = listObjectsForCopy }) :
                 action.Equals("delete") ? start(new DeleteModel() { listObjects = listObjectsForCopy }) : 
                 start(new CompareModel());
        }

        private static bool start(IActions any) => any.DoIt();        

    }
}
