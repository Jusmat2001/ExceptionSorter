using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using PNSrv10Lib;
using Microsoft.Office.Interop.Word;

namespace ExceptionSorter
{
    class PdfToTifHandler
    {
        private Object LockObject = new Object();
        private Object UploadLock = new Object();
        int m_currentFileNumber;

        public void ConvertFile(String strFile, String outName, String outLoc)
        {
            PNSession session = null;
            IPNPrintSession documentPrintSession = null;
            _Application oWordApp = new Microsoft.Office.Interop.Word.Application();
            _Document oDoc = null;
            object myTrue = true,
                   filename = strFile,
                   optMissing = System.Type.Missing;
            bool bCompleted, bDocSpooling = false;
            int maxSpoolingWait = 0, currentSpoolingWait = 0;
            
            
            // STEP 1: Initialize an IPNSession object
            session = new PNSession();
            session.SetSessionPrinter("TIFF Image Printer 10.0", 1, null, false);

            
            // STEP 2: Add event handlers
            //session.OnEndJob += new _IPNSessionEvents_OnEndJobEventHandler(session_OnEndJob);
            //session.OnGetNextFileName += new _IPNSessionEvents_OnGetNextFileNameEventHandler(session_OnGetNextFileName);


            // STEP 3: Set the print job properties for saving and compression
            // Reduce to black and white, dithering set to Halftone
            // Set name and save location, create multipage tiff and append all into
            // a single file. Don't use jobid, don't prompt, always overwrite
            session.SetSaveOptions(outLoc, outName, pnOutputFileFormat.pnOutputFileFormatTIFFSerialized, false,
                            pnColorReduction.pnColorReductionOptimal, pnDitheringMethod.pnDitheringHalftone,
                            false, false, true, true, true, false);
            
            // Use G4 compression for black and white
            session.SetTIFFCompressionOptions(pnColorCompressionMethod.pnColorCompressionLZW, pnIndexedCompressionMethod.pnIndexedCompressionLZW,
                            pnGreyscaleCompressionMethod.pnGreyscaleCompressionLZW, pnBWCompressionMethod.pnBWCompressionGroup4);
            
            // open the document
            oDoc = oWordApp.Documents.Open(ref filename, ref optMissing, ref optMissing,
                                        ref optMissing, ref optMissing, ref optMissing,
                                        ref optMissing, ref optMissing, ref optMissing,
                                        ref optMissing, ref optMissing, ref optMissing,
                                        ref optMissing, ref optMissing, ref optMissing,
                                        ref optMissing);
            // do any document formatting using Word Automation here
            
            
            // STEP 4: Get an IPNPrintSession object for this document
            documentPrintSession = null;
            while (documentPrintSession == null)
            {
                try
                {
                    // Get a new print session for this document with:
                    //
                    // int Timeout
                    // 5 second timeout for an available printSession to be returned
                    // int FirstJobTimeout
                    // 1 minute max timeout for a job to appear in the queue before
                    // releasing the printer back to the pool; applies only after
                    // the printSession has been released
                    // int AvailableTimeout
                    // 1/4 second timeout waiting between jobs entering the queue.
                    // If a new job is not seen in the print queue in the timeout
                    // period, the printer is released back into the printer pool.
                    // int OptionFlags
                    // reserved for future use
                    documentPrintSession = session.NewPrintSessionEx(5000, 60000, 250, 0);
                    // Alternate call - not as customizable.
                    // Wait max 5 seconds for an available printer
                    // Wait max 2 seconds for another print job to appearin the queue
                    // documentPrintSession = Session.NewPrintSession(5000, 2000)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception on NewPrintSessionEx (" + ex.Message + ")");
                    break;
                }
            }
            
            
            // STEP 5: Print the document and check for successful print
            if (documentPrintSession != null)
            {
                // Set active printer with FilePrintSetup, .ActivePrinter is not thread-safe
                object oWordbasic = oWordApp.WordBasic;
                object[] argValues = new object[] { documentPrintSession.PrinterName, 1 };
                String[] argNames = new String[] { "Printer", "DoNotSetAsSysDefault" };

                oWordbasic.GetType().InvokeMember("FilePrintSetup", System.Reflection.BindingFlags.InvokeMethod,
                                    null, oWordbasic, argValues, null, null, argNames);

                // print in the background
                oDoc.PrintOut(ref myTrue, ref optMissing, ref optMissing, ref optMissing,
                ref optMissing, ref optMissing, ref optMissing, ref optMissing,
                ref optMissing, ref optMissing, ref optMissing, ref optMissing,
                ref optMissing, ref optMissing, ref optMissing, ref optMissing,
                ref optMissing, ref optMissing);
                // Test that file made it to the printer. We are using a maximum wait time of
                // 1 minute, looping every 10 seconds.
                // This should be adjusted to reflect the size of your documents

                maxSpoolingWait = 60000;
                currentSpoolingWait = 0;
                while (currentSpoolingWait < maxSpoolingWait)
                {
                    bDocSpooling = documentPrintSession.WaitForJobsSpooling(10000);
                    currentSpoolingWait = currentSpoolingWait + 10000;
                }

                // The document did not enter the print queue so we need to cancel this
                // printSession to allow the printer to return to the printer pool.
                if (!bDocSpooling)
                {
                    documentPrintSession.Cancel();
                }
                // STEP 4: Release print session when you are finished with it
                Marshal.FinalReleaseComObject(documentPrintSession);
                documentPrintSession = null;
                // Make sure Word is done printing
                while (oWordApp.BackgroundPrintingStatus > 0)
                {
                    System.Threading.Thread.Sleep(250);
                }
            }
            // Wait for all the jobs to be complete and do cleanup here
            
            bCompleted = false;
            // Set while to if to fix freeze
            if (!bCompleted)
            {
                session.WaitForCompletion(10000);
                
            }

            try
            {
                oDoc.Close(ref optMissing, ref optMissing, ref optMissing);
                oWordApp.Quit(ref optMissing, ref optMissing, ref optMissing);
                // release Word automation objects
                Marshal.FinalReleaseComObject(oDoc);
                Marshal.FinalReleaseComObject(oWordApp);
                // STEP 2: Remove added event handlers
                session.OnEndJob += null;
                session.OnGetNextFileName += null;
                //STEP 1: Release IPNSession objects
                Marshal.FinalReleaseComObject(session);
                session = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
            
        }
        // STEP 2: Event handler for getting a custom file name
        void session_OnGetNextFileName(IPNJob pJob)
        {
            string filename;
            lock (LockObject)
            {
                filename = "TestFile_" +
                           String.Format("{0:000000}", m_currentFileNumber);
                m_currentFileNumber++;
            }
            // sets the new output file name into the job
            pJob.SetVariableByKeyword(pnJobVariable.pnJobVariableOutputFileName,
                filename);
            // release job object pointer
            Marshal.FinalReleaseComObject(pJob);
        }
        // STEP 2: Event handler for custom action when the file is created
        void session_OnEndJob(IPNJob pJob)
        {
            lock (UploadLock)
            {
                // Loop through files collection for this job and upload all created
                // files to the archive system
                IPNFiles pFilesList = pJob.Files;
                foreach (IPNFile file in pFilesList)
                {
                    MessageBox.Show(file.Filename);
                    // release file object pointer
                    Marshal.FinalReleaseComObject(file);
                }
                // release files list object pointer
                Marshal.FinalReleaseComObject(pFilesList);
            }
            // release job object pointer
            Marshal.FinalReleaseComObject(pJob);
        }
    }
    
}
