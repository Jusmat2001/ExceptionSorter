using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExceptionSorter.Properties;
using Ghostscript.NET;
using Microsoft.Office.Interop.Word;


namespace ExceptionSorter
{

    public partial class Form1 : Form
    {
        static string fileLocIn = @"C:\Users\jmatlock\Desktop\input";
        static string fileLocOut = @"C:\Users\jmatlock\Desktop\tif output";
        IEnumerable<string> fileNames = new string[0];
        public int tifCount, pdfCount, startFileCount;
        ListOfFiles exceptionList = new ListOfFiles();
        ListOfFiles tifList = new ListOfFiles();
        //PdfToTifHandler handler = new PdfToTifHandler();
        
        //Gsc gsc = new Gsc();
        //GhostscriptVersionInfo gsVersionInfo = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);


        public Form1()
        {
            InitializeComponent();

            fileNames = exceptionList.LoadAllFiles(fileLocIn);
            startFileCount = fileNames.Count();
            lWindow.Items.Add("There are " + startFileCount + " files to sort in " + fileLocIn);
            foreach (var s in fileNames)
            {
                if (s.Contains(".pdf")) { pdfCount++; }
                if (s.Contains(".tif")) { tifCount++; }
            }
            lWindow.Items.Add("");
            lWindow.Items.Add("There are " + pdfCount + " pdfs.");
            lWindow.Items.Add("There are " + tifCount + " tifs.");
        }




        private void sortBtn_Click(object sender, EventArgs e)
        {
            tiffBtn.BackColor = Color.DimGray;
            try
            {
                var tifList = from f in fileNames where f.Contains(".pdf") select f;
                foreach (var f in tifList)
                {
                    var fileName = Path.GetFileNameWithoutExtension(f);
                    //gsc.ConverttoTif(f, fileLocOut, fileName);
                    
                    //statStrip.Text = "Processing file: " + f;
                    //if (File.Exists(oN)) { File.Delete(f);}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

            var LTifCount = tifList.LoadTifFiles(fileLocOut).Count();
            tiffBtn.BackColor = Color.Yellow;
            statStrip.Text = "Finished Tiffing. Starting file count = " + startFileCount+" Post Tiff file count = " + LTifCount;
        }




        private void fileBtn_Click(object sender, EventArgs e)
        {
            fileBtn.BackColor = Color.DimGray;
        }




        private void preBtn_Click(object sender, EventArgs e)
        {
            lWindow.Items.Clear();
            loadBtn.BackColor = Color.DimGray;
            statStrip.Text = "Fetching files....";
           
            var praclist = fileNames.GroupBy(x => x.Substring(32, 3))
                         .Select(y => new { Prefix = y.Key, Count = y.Count() });
            foreach (var p in praclist)
            {
                lWindow.Items.Add("Practice: " + p.Prefix +"   has   "+ p.Count +"  file(s).");
            }
            loadBtn.BackColor = Color.Red;
            statStrip.Text = "List Loaded.";
        }
    }
}
