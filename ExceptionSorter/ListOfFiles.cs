using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionSorter
{
    public class ListOfFiles
    {
        public IEnumerable<string> LoadAllFiles(string fileLoc)
        {
            var fileNames = Directory.EnumerateFiles(fileLoc, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".pdf") || s.EndsWith(".tif"));
            return fileNames;
        }

        public IEnumerable<string> LoadTifFiles(string fileLoc)
        {
            var fileNames = Directory.EnumerateFiles(fileLoc, "*.tif*", SearchOption.AllDirectories);
            return fileNames;
        }



    }
}
