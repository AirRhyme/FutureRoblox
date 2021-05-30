using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Future
{
    class Functions
    {
        public static void addToScriptList(ListBox lsb, String Folder, String FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach(FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }
    }
}
