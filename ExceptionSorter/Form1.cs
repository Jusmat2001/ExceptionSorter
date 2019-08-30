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
using PNSrv10Lib;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Threading;

namespace ExceptionSorter
{

    public partial class Form1 : Form
    {
        static string sFileInputLoc = @"C:\Users\jmatlock\Desktop\input\";
        IEnumerable<string> ienFileNames = new string[0];
        public int iTifCount, iPdfCount;
        ListOfFiles lofExceptionList = new ListOfFiles();
        //private Object LockObject = new Object();
        //private Object UploadLock = new Object();
        public clsSQL oSQL;
        public clsConfig oConfig;
        public const string XML_CONFIG_FILE = "ImagePrint.xml";
        Guid gNew = new Guid();
        string[] sPracticeGuid = new String[100];
        string sPracticeGuidID = "";
        string sFileNameTif = "";
        DirectoryInfo dirImage;
        FileInfo[] fiFiles;
        string sPractice = "";
        


        public Form1()
        {
            InitializeComponent();
            oConfig = new clsConfig(XML_CONFIG_FILE);
            oSQL = new clsSQL(oConfig);

            ienFileNames = lofExceptionList.LoadFiles(sFileInputLoc);
            lWindow.Items.Add("There are " + ienFileNames.Count() + " files to sort in " + sFileInputLoc);
            foreach (var s in ienFileNames)
            {
                if (s.Contains(".pdf")) { iPdfCount++; }
                if (s.Contains(".tif")) { iTifCount++; }
            }
            lWindow.Items.Add("");
            lWindow.Items.Add("There are " + iPdfCount + " pdfs.");
            lWindow.Items.Add("There are " + iTifCount + " tifs.");
        }

        

        private void bPreviewFileBtn_Click(object sender, EventArgs e)
        {
            lWindow.Items.Clear();

            var praclist = ienFileNames.GroupBy(x => x.Substring(24, 3))
                        .Select(y => new { Prefix = y.Key, Count = y.Count() });
            foreach (var p in praclist)
            {
                lWindow.Items.Add("Practice: " + p.Prefix + "   has   " + p.Count + "  file(s).");
            }
        }

        private void bFileTifBtn_Click(object sender, EventArgs e)
        {
            lWindow.Clear();
            dirImage = new DirectoryInfo(sFileInputLoc);
            fiFiles = dirImage.GetFiles(".tif");
            //foreach  set sfilenametif, then filetifs()
            if (fiFiles.Length<1) { MessageBox.Show("No files to sort."); return; }
            for (int i =0; i <fiFiles.Length; i++)
            {
                sFileNameTif = fiFiles[i].FullName;
                lWindow.Items.Add("Processing " + sFileNameTif);
                FileTifs(sFileNameTif);
                Thread.Sleep(45000);

            }

            
        }

        private void FileTifs(string sFileNameTif)
        {
            statStrip.Text = "Processing File " + sFileNameTif;
            try
            {
                if (sFileNameTif == "") { return; }
                sPractice = sFileNameTif.Substring(0, 3);
                gNew = Guid.NewGuid();
                string sNewFileName = sFileInputLoc + gNew + "_VS3.tif";
                File.Move(sFileNameTif, sNewFileName);
                sFileNameTif = sNewFileName;
                //     sFileNameTif = "C:\\CodeOne\\ImagePrint\\2ccd1993-6733-465a-9d1a-027057fbf0fd_VS3.tif"
                FileInfo originalFileInfo = new FileInfo(sFileNameTif);
                string sUnNumberedshare = "\\C:\\Users\\jmatlock\\Desktop\\tif output";
                    
                //    string.Format("\\\\{0}", oConfig.sToDir);

                //     sUnnumberedshare = \\file1\Unnumbered
                string sUnNumberedName = string.Format("{0}\\{1}", sUnNumberedshare, Path.GetFileName(sFileNameTif));
                //     sUnNumberedName = "\\\\File1\\Unnumbered\\2ccd1993-6733-465a-9d1a-027057fbf0fd_VS3.tif"
                //Int32 iRet = oSQL.doInsertTiffReq(sPracticeGuidID, "", sPractice, Path.GetFileName(sFileNameTif));
                //if (iRet != 0)
                //{ //if we don't insert tif, dont move file. Unnumbered will loop looking for row.
                //    File.Delete(sFileNameTif);
                //    MessageBox.Show("Error Inserting Tif ID into Item Table, Error = " + oSQL.sError, "Error Creating Item Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return; 
                //}
                MessageBox.Show("sPracticeGuidID = " + sPracticeGuidID + "/nsPractice = " + sPractice + "/nGetfilename = " + Path.GetFileName(sFileNameTif));
                File.Move(sFileNameTif, sUnNumberedName);
                File.Delete(sFileNameTif);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                File.Delete(sFileNameTif);
                return;
            }
        }


        //private void ConvertFile(String strFile)
        //{
        //    PNSession session = new PNSession();
        //    IPNPrintSession documentPrintSession = null;
        //    //Word.Application oWordApp = new Word.Application();
        //    //WordApp._Document oDoc = null;
        //    object myTrue = true,
        //    filename = strFile,
        //    optMissing = System.Type.Missing;
        //    bool bCompleted, bDocSpooling = false;
        //    int maxSpoolingWait = 0, currentSpoolingWait = 0;
        //    int maxCompleted = 0, currentCompletedWait = 0;

        //    // STEP 1: Initialize an IPNSession object
        //    session.SetSessionPrinter("TIFF Image Printer 10.0", 2, null, false);

        //    // STEP 2: Set the print job properties
        //    session.SetSaveOptions(@"C:\Users\jmatlock\Desktop\tif output", null, pnOutputFileFormat.pnOutputFileFormatTIFFSerialized, true,
        //            pnColorReduction.pnColorReductionBlackAndWhite, pnDitheringMethod.pnDitheringNone, false, false, true, true, true, false);

        //    // Use G4 compression for black and white images
        //    session.SetTIFFCompressionOptions(pnColorCompressionMethod.pnColorCompressionLZW,
        //    pnIndexedCompressionMethod.pnIndexedCompressionLZW,
        //    pnGreyscaleCompressionMethod.pnGreyscaleCompressionLZW,
        //    pnBWCompressionMethod.pnBWCompressionGroup4);
        //    // open the document

        //    var oDoc = Documents.Open(ref filename, ref optMissing, ref optMissing,
        //    ref optMissing, ref optMissing, ref optMissing,
        //    ref optMissing, ref optMissing, ref optMissing,
        //    ref optMissing, ref optMissing, ref optMissing,
        //    ref optMissing, ref optMissing, ref optMissing,
        //    ref optMissing);

        //    // STEP 3: Get an IPNPrintSession object for this document
        //    documentPrintSession = null;
        //    while (documentPrintSession == null)
        //    {
        //        try
        //        {
        //            // Get a new print session for this document with:
        //            //
        //            // int Timeout
        //            // 5 second timeout for an available printSession to be returned
        //            // int FirstJobTimeout
        //            // 1 minute max timeout for a job to appear in the queue before
        //            // releasing the printer back to the pool; applies only after
        //            // the printSession has been released
        //            // int AvailableTimeout
        //            // 1/4 second timeout waiting between jobs entering the queue.
        //            // If a new job is not seen in the print queue in the timeout
        //            // period, the printer is released back into the printer pool.
        //            // int OptionFlags
        //            // reserved for future use
        //            documentPrintSession = session.NewPrintSessionEx(5000, 60000, 250, 0);
        //            // Alternate call - not as customizable.
        //            // Wait max 5 seconds for an available printer
        //            // Wait max 2 seconds for another print job to appearing the queue
        //            // documentPrintSession = Session.NewPrintSession(5000, 2000)
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Exception on NewPrintSessionEx (" + ex.Message + ")");
        //            break;
        //        }
        //    }

        //    // STEP 4: Print the document to the IPNPrintSession printer
        //    if (documentPrintSession != null)
        //    {
        //        // Set active printer with FilePrintSetup, .ActivePrinter is not thread-safe
        //        object oWordbasic = oWordApp.WordBasic;
        //        object[] argValues = new object[] { documentPrintSession.PrinterName, 1 };
        //        String[] argNames = new String[] { "Printer", "DoNotSetAsSysDefault" };
        //        oWordbasic.GetType().InvokeMember("FilePrintSetup",
        //        System.Reflection.BindingFlags.InvokeMethod,
        //        null, oWordbasic, argValues, null, null, argNames);
        //        // print in the background
        //        oDoc.PrintOut(ref myTrue, ref optMissing, ref optMissing, ref optMissing,
        //        ref optMissing, ref optMissing, ref optMissing, ref optMissing,
        //        ref optMissing, ref optMissing, ref optMissing, ref optMissing,
        //        ref optMissing, ref optMissing, ref optMissing, ref optMissing,
        //        ref optMissing, ref optMissing);
        //        // STEP 5: Synchronize with file creation
        //        // Test that file made it to the printer. We are using a maximum wait time of
        //        // 1 minute, looping every 10 seconds.
        //        // This should be adjusted to reflect the size of your documents
        //        maxSpoolingWait = 60000;
        //        currentSpoolingWait = 0;
        //        while (currentSpoolingWait < maxSpoolingWait)
        //        {
        //            bDocSpooling = documentPrintSession.WaitForJobsSpooling(10000);
        //            currentSpoolingWait = currentSpoolingWait + 10000;
        //        }
        //        // The document did not enter the print queue so we need to cancel this
        //        // printSession to allow the printer to return to the printer pool.
        //        if (!bDocSpooling)
        //        {
        //            documentPrintSession.Cancel();
        //        }
        //        else
        //        {
        //            // wait for output file to be created here
        //            // maximum wait time of 10 minutes, looping every 20 seconds.
        //            // This should be adjusted to reflect the size of your documents.
        //            bCompleted = false;
        //            var maxCompletedWait = 600000;
        //            currentCompletedWait = 0;
        //            while (currentCompletedWait < maxCompletedWait)
        //            {
        //                bCompleted = documentPrintSession.WaitForJobsSpooling(20000);
        //                currentCompletedWait = currentCompletedWait + 20000;
        //            }
        //            if (!bCompleted)
        //            {
        //                documentPrintSession.Cancel();
        //            }
        //            else
        //            {
        //                // Loop through files collection for this job and
        //                // upload all created files to the archive system
        //                IPNJobs pJobList = session.Jobs;
        //                foreach (IPNJob pJob in pJobList)
        //                {
        //                    IPNFiles pFileList = pJob.Files;
        //                    foreach (IPNFile pFile in pFileList)
        //                    {
        //                        UploadToArchive(pFile.Filename);
        //                        Marshal.FinalReleaseComObject(pFile);
        //                    }
        //                    Marshal.FinalReleaseComObject(pFileList);
        //                    Marshal.FinalReleaseComObject(pJob);
        //                }
        //                Marshal.FinalReleaseComObject(pJobList);
        //            }
        //        }
        //        // STEP 3: Release print session
        //        Marshal.FinalReleaseComObject(documentPrintSession);
        //        documentPrintSession = null;
        //        // Make sure Word is done printing
        //        while (oWordApp.BackgroundPrintingStatus > 0)
        //        {
        //            System.Threading.Thread.Sleep(250);
        //        }
        //    }
        //    // Wait for all the jobs to be complete and do cleanup here
        //    bCompleted = false;
        //    while (!bCompleted)
        //    {
        //        bCompleted = session.WaitForCompletion(120000) );
        //    }
        //    oDoc.Close(ref optMissing, ref optMissing, ref optMissing);
        //    oWordApp.Quit(ref optMissing, ref optMissing, ref optMissing);
        //    // Clean up word automation objects
        //    Marshal.FinalReleaseComObject(oDoc);
        //    Marshal.FinalReleaseComObject(oWordApp);
        //    // STEP 1: Release IPNSession object
        //    Marshal.FinalReleaseComObject(session);
        //    session = null;
        //}

    }
}
