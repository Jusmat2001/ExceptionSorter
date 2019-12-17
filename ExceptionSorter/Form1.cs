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
//using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Threading;


namespace ExceptionSorter
{

    public partial class Form1 : Form
    {
        static string sFileInputLoc = @"Z:\CodeOne835Exceptions\";
        public int iTifCount, iPdfCount;
        public clsSQL oSQL;
        public clsConfig oConfig;
        public const string XML_CONFIG_FILE = "ImagePrint.xml";
        Guid gNew = new Guid();
        string[] sPracticeGuid = new String[100];
        string[] sPracticeList = new String[100];
        string sPracticeGuidID = "";
        string sFileNameTif = "";
        public static DirectoryInfo di;
        FileInfo[] fi;
        string sPractice = "";
        public static string sLogPath = @"C:\CodeOne\ExceptionSorterLog\";
        

        public Form1()
        {
            InitializeComponent();
            oConfig = new clsConfig(XML_CONFIG_FILE);
            oSQL = new clsSQL(oConfig);
            di = new DirectoryInfo(sFileInputLoc);

            fi = di.GetFiles().Where(x=>x.Extension !=".db").ToArray();
            //foreach (var f in fi)
            //{
            //    if (f.Extension==(".db"))
            //    {
                    
            //    }
            //}
            lWindow.Items.Add("There are " + fi.Count() + " files to sort in " + sFileInputLoc);
            writeLog("******There are " + fi.Count() + " files to sort in " + sFileInputLoc, 0);
            foreach (var s in fi)
            {
                if (s.Name.Contains(".pdf")) { iPdfCount++; }
                if (s.Name.Contains(".tif")) { iTifCount++; }
            }
            lWindow.Items.Add("");
            lWindow.Items.Add("There are " + iPdfCount + " pdfs.");
            lWindow.Items.Add("There are " + iTifCount + " tifs.");
        }

        private void bPreviewFileBtn_Click(object sender, EventArgs e)
        {
            fi = di.GetFiles().Where(x => x.Extension != ".db").ToArray(); 
            lWindow.Items.Clear();
            var groups = from f in fi
                         group f by f.Name.Substring(0, 3) into g
                         select new { prac = g.Key, Count = g.Count() }; 
            foreach (var g in groups)
            {
                lWindow.Items.Add("Practice: " + g.prac + "   has   " + g.Count + "  file(s).");
                //writeLog("Practice: " + g.prac + "   has   " + g.Count + "  file(s).",0);
            }
        }

        private void bPdfToTifBtn_Click(object sender, EventArgs e) 
        {
            var iConversionCount =0;
            var files = di.GetFiles("*.pdf");
            if (files.Count() > 0)
            {
                try
                {
                    foreach (var f in files)
                    {
                        string input = f.FullName.ToString();
                        string output = input.Replace("pdf", "tif");
                        if (PdfToJpg(input, output))
                        {
                            iConversionCount++;
                            continue;
                        }
                        else
                        { break; }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    writeLog("Error in PDF conversion: Error= " + ex.Message, 2);
                }
            }
            else { MessageBox.Show("No pdf files found to convert."); }
            
            slStatusLabel.Text = "Files converted to PDF: " + iConversionCount;
            writeLog("Files converted: " + iConversionCount, 0);
            statStrip.Update();
            foreach (var f in files)
            {
                if (File.Exists(f.FullName)) { File.Delete(f.FullName.ToString()); }
                else { MessageBox.Show("File not Found."); }
            }
        }

        private bool PdfToJpg(string inputPDFFile, string outputImagesPath)
        {
            if (!Directory.GetFiles(sFileInputLoc).Contains(outputImagesPath))
            {
                try
                {
                    string ghostScriptPath = @"C:\Program Files (x86)\gs\gs9.50\bin\gswin64c.exe";
                    String ars = "-sDEVICE=tiffg4 -o " + outputImagesPath + " " + inputPDFFile;

                    Process proc = new Process();
                    proc.StartInfo.FileName = ghostScriptPath;
                    proc.StartInfo.Arguments = ars;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.Start();
                    proc.WaitForExit();
                    if (Directory.GetFiles(sFileInputLoc).Contains(outputImagesPath))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(outputImagesPath + "was not detected after conversion.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("File " + outputImagesPath + " already exists.");
                return false;
            }
        }

        private void bFileTifBtn_Click(object sender, EventArgs e)
        {
            try
            {
                lWindow.Clear();
                DataTable dtPractices = oSQL.GetPracticeList();
                if (dtPractices != null)
                {
                    Int32 i = 0;
                    foreach (DataRow drPractices in dtPractices.Rows)
                    {
                        sPracticeGuid[i] = drPractices["ID"].ToString();
                        sPracticeList[i] = drPractices["PracticeIdentifier"].ToString();
                        i++;
                    }
                }

                FileInfo[] fiTifFiles = di.GetFiles("*.tif");
                
                if (fiTifFiles.Length < 1) { MessageBox.Show("No files to sort."); return; }
                foreach (var tifFile in fiTifFiles)
                {
                    sFileNameTif = tifFile.FullName;
                    lWindow.Items.Add("Processing " + tifFile.Name);
                    writeLog("---------------------------------------------", 0);
                    writeLog("Original File: " + tifFile.Name,0);
                    //if first 3 of filename are NOT in practiceList, move file to desktop folder for manual processing
                    sPractice = tifFile.Name.Substring(0, 3);
                    if (!sPracticeList.Contains(sPractice))
                    {
                        string sDate = DateTime.Now.ToString("MM-dd-yyy");
                        var sPath = @"C:\Users\jmatlock\Desktop\" + sDate +" Sorter Exceptions";
                        var sDestFilename = sPath + "\\"+ tifFile.Name;
                        Directory.CreateDirectory(sPath);
                        File.Move(sFileNameTif, sDestFilename);
                        writeLog("Practice not found. " + sFileNameTif + " moved to " + sPath,1);
                    }
                    FileTifs(tifFile);
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FileTifs(FileInfo tifFile)
        {
            slStatusLabel.Text = "Processing File " + sFileNameTif;
            
            statStrip.Update();
            try
            {
                if (tifFile.Name == "") { return; }
                
                int iIndex = Array.FindIndex(sPracticeList, row => row.Contains(sPractice));
                sPracticeGuidID = sPracticeGuid[iIndex];
                gNew = Guid.NewGuid();
                string sNewFileName = sFileInputLoc + gNew + "_VS3.tif";
                writeLog("To File Name: " + sNewFileName, 0);
                File.Move(sFileNameTif, sNewFileName);
                sFileNameTif = sNewFileName;
                //sFileNameTif = "C:\\CodeOne\\ImagePrint\\2ccd1993-6733-465a-9d1a-027057fbf0fd_VS3.tif"

                //string sUnNumberedshare = "C:\\Users\\jmatlock\\Desktop\\tif_output";
                string sUnNumberedshare = string.Format("\\\\{0}", oConfig.sToDir);
                //Looks like -->     sUnnumberedshare = \\file1\Unnumbered

                string sUnNumberedName = string.Format("{0}\\{1}", sUnNumberedshare, Path.GetFileName(sFileNameTif));
                //Looks like -->     sUnNumberedName = "\\\\File1\\Unnumbered\\2ccd1993-6733-465a-9d1a-027057fbf0fd_VS3.tif"

                Int32 iRet = oSQL.doInsertTiffReq(sPracticeGuidID, "", sPractice, Path.GetFileName(sFileNameTif));
                if (iRet != 0)
                { //if we don't insert tif, dont move file. Unnumbered will loop looking for row.
                    writeLog("Error Inserting Tiff Request, Error = " + oSQL.sError, 2);
                    File.Delete(sFileNameTif);
                    MessageBox.Show("Error Inserting Tif ID into Item Table, Error = " + oSQL.sError, "Error Creating Item Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.Move(sFileNameTif, sUnNumberedName);
                writeLog("File Moved Successfully.", 0);

                File.Delete(sFileNameTif);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                File.Delete(sFileNameTif);
                return;
            }
        }

        public void writeLog(string sMsg, Int32 iType)
        {
            string sMsgDate = ""; string sMsgType = ""; string sMessage = "";
            try
            {
                sMsgDate = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss tt");
                switch (iType)
                {
                    case 0:
                        sMsgType = "Information "; break;
                    case 1:
                        sMsgType = "Warning     "; break;
                    case 2:
                        sMsgType = "Error       "; break;
                    default:
                        sMsgType = "Invalid Type"; break;
                }
                sMessage = sMsgDate + " " + sMsgType + "\t" + " " + sMsg;
                StreamWriter logWriter;
                DirectoryInfo dir = new DirectoryInfo(sLogPath);
                if (!dir.Exists) { dir.Create(); }
                string logFile = Path.Combine(sLogPath, DateTime.Now.ToString("yyyyMMdd") + ".LOG");
                if (File.Exists(logFile))
                {
                    logWriter = File.AppendText(logFile);
                }
                else
                {
                    logWriter = File.CreateText(logFile);
                }
                logWriter.WriteLine(sMessage);
                logWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot Write to Log File, Contact Codeone, Error =" + ex.Message, "Error in ImagePrint", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
