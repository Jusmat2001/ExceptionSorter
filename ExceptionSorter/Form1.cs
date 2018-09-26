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
        public int tifCount, pdfCount;
        ListOfFiles exceptionList = new ListOfFiles();


        public Form1()
        {
            InitializeComponent();

            fileNames = exceptionList.LoadFiles(fileLoc);
            lWindow.Items.Add("There are " + fileNames.Count() + " files to sort in " + fileLoc);
            foreach (var s in fileNames)
            {
                if (s.Contains(".pdf")) { pdfCount++; }
                if (s.Contains(".tif")) { tifCount++; }
            }
            lWindow.Items.Add("");
            lWindow.Items.Add("There are " + pdfCount + " pdfs.");
            lWindow.Items.Add("There are " + tifCount + " tifs.");
        }
        
        
        private void preBtn_Click(object sender, EventArgs e)
        {
            lWindow.Items.Clear();
           
            var praclist = fileNames.GroupBy(x => x.Substring(24, 3))
                        .Select(y => new { Prefix = y.Key, Count = y.Count() });
            foreach (var p in praclist)
            {
                lWindow.Items.Add("Practice: " + p.Prefix +"   has   "+ p.Count +"  file(s).");
            }
        }


    }
}
