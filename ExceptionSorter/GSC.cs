using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceptionSorter
{
    class Gsc
    {
        public void PdfToJpg(string inputPDFFile, string outputImagesPath)
        {
            string ghostScriptPath = @"C:\Program Files\gs\gs9.25\bin\gswin64.exe";
            String ars = "-dNOPAUSE -sDEVICE=jpeg -r102.4 -o" + outputImagesPath + "%d.jpg -sPAPERSIZE=a4 " + inputPDFFile;
            Process proc = new Process();
            proc.StartInfo.FileName = ghostScriptPath;
            proc.StartInfo.Arguments = ars;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        /// <summary>Convert a single file!</summary>
        /// <param name="inputFile">The file PDf to convert</param>
        /// <param name="outputFile">The image file that will be created</param>
        /// <returns>True if the conversion succeeds!</returns>
        //public bool Convert(string inputFile, string outputFile)
        //{
        //    //These are the variables that I'm going to use
        //    int intReturn, intCounter, intElementCount;
        //    IntPtr intGSInstanceHandle;
        //    object[] aAnsiArgs;
        //    IntPtr[] aPtrArgs;
        //    GCHandle[] aGCHandle;
        //    IntPtr callerHandle, intptrArgs;
        //    GCHandle gchandleArgs;
        //    //Generate the list of the parameters
        //    string[] sArgs = GetGeneratedArgs(inputFile, outputFile);
        //    // Convert the Unicode strings to null terminated ANSI byte arrays
        //    // then get pointers to the byte arrays.
        //    intElementCount = sArgs.Length;
        //    aAnsiArgs = new object[intElementCount];
        //    aPtrArgs = new IntPtr[intElementCount];
        //    aGCHandle = new GCHandle[intElementCount];
        //    //Convert the parameters
        //    for (intCounter = 0; intCounter < intElementCount; intCounter++)
        //    {
        //        aAnsiArgs[intCounter] = StringToAnsiZ(sArgs[intCounter]);
        //        aGCHandle[intCounter] =
        //                 GCHandle.Alloc(aAnsiArgs[intCounter], GCHandleType.Pinned);
        //        aPtrArgs[intCounter] = aGCHandle[intCounter].AddrOfPinnedObject();
        //    }
        //    gchandleArgs = GCHandle.Alloc(aPtrArgs, GCHandleType.Pinned);
        //    intptrArgs = gchandleArgs.AddrOfPinnedObject();
        //    //Create a new instance of the library!
        //    try
        //    {
        //        intReturn = gsapi_new_instance(out intGSInstanceHandle, _objHandle);
        //        //Be sure that we create an instance!
        //        if (intReturn < 0)
        //        {
        //            MessageBox.Show("I can't create a new instance of Ghostscript please verify no other instance are running!");
        //            //Here you should also clean the memory
        //            ClearParameters(ref aGCHandle, ref gchandleArgs);
        //            return false;
        //        }
        //    }
        //    catch (DllNotFoundException ex)
        //    {//in this case the DLL we are using is not the DLL we expect
        //        MessageBox.Show("The gs32dll.dll in the program directory doesn't expose the methods i need \nplease download the version 8.63 from the original website!");
        //        return false;
        //    }
        //    callerHandle = IntPtr.Zero;//remove unwanted handler
        //    intReturn = -1;//if nothing changes, it is an error!
        //                   //Ok now is the time to call the interesting module
        //    try
        //    {
        //        intReturn =
        //        gsapi_init_with_args(intGSInstanceHandle, intElementCount, intptrArgs);
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }
        //    finally//No matter what happens, I MUST close the instance!
        //    {   //free all the memory
        //        ClearParameters(ref aGCHandle, ref gchandleArgs);
        //        gchandleArgs.Free();
        //        gsapi_exit(intGSInstanceHandle);//Close the instance
        //        gsapi_delete_instance(intGSInstanceHandle);//delete it
        //    }
        //    //Conversion was successful if return code was 0 or e_Quit
        //    return (intReturn == 0) | (intReturn == e_Quit);//e_Quit = -101
        //}
    }
}
