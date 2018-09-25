using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceptionSorter
{

    public partial class Form1 : Form
    {
        static string fileLoc = @"Z:\CodeOne835Exceptions";
        IEnumerable<string> fileNames = new string[0];
        DirectoryInfo dI = new DirectoryInfo(fileLoc);
        private int fileCount, loopCount;
        public int tifCount, pdfCount;
        public string prac = null;
        
        

        public Form1()
        {
            InitializeComponent();
            LoadWindow();
        }
        

        public void LoadWindow()
        {
            fileNames = Directory.EnumerateFiles(fileLoc, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".pdf") || s.EndsWith(".tif"));
            lWindow.Items.Add("There are " + fileNames.Count() + " files to sort in " + fileLoc);
        }

        private void preBtn_Click(object sender, EventArgs e)
        {
            lWindow.Items.Clear();
            #region old foreach loop
            //foreach (var s in fileNames)
            //{
            //    if (s.Contains(".pdf")) { pdfCount++; }
            //    if (s.Contains(".tif")) { tifCount++; }

            //    loopCount++;
            //    int arrCount = fileNames.Count();

            //    if (prac == null)
            //    {
            //        fileCount = 1;
            //        prac = s.Substring(24, 3);
            //    }
            //    else if (s.Substring(24, 3) != prac)
            //    {
            //        lWindow.Items.Add(prac + " has " + fileCount + " file(s) to sort.");
            //        prac = s.Substring(24, 3);
            //        fileCount = 1;
            //        if (loopCount==arrCount) { lWindow.Items.Add(prac + " has " + fileCount + " file(s) to sort."); }
            //    }
            //    else
            //    {
            //        fileCount++;
            //        if (loopCount == arrCount) { lWindow.Items.Add(prac + " has " + fileCount + " file(s) to sort."); }
            //    }
            //}
            #endregion
            var praclist = fileNames.GroupBy(x => x.Substring(24, 3))
                .Select(y => new { Prefix = y.Key, Count = y.Count() });
            foreach (var p in praclist)
            {
                lWindow.Items.Add("Practice: " + p.Prefix +"   has   "+ p.Count +"  files.");
            }

            foreach (var s in fileNames)
            {
                if (s.Contains(".pdf"))
                {
                    pdfCount++;
                }

                if (s.Contains(".tif"))
                {
                    tifCount++;
                }
            }
            lWindow.Items.Add("");
            lWindow.Items.Add("There are " + pdfCount + " pdfs.");
            lWindow.Items.Add("There are " + tifCount + " tifs.");
        }
    }
}
