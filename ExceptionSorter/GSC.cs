using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;
using System.IO;

namespace ExceptionSorter
{
    class Gsc
    {
        public void ConverttoTif(string inputFile, string outputFolder, string outputFileName)
        {
            var xDpi = 100; //set the x DPI
            var yDpi = 100; //set the y DPI
            var pageNumber = 1; // the pages in a PDF document



            using (var rasterizer = new GhostscriptRasterizer()) //create an instance for GhostscriptRasterizer
            {
                rasterizer.Open(inputFile); //opens the PDF file for rasterizing

                //set the output image complete path
                var outputTiffPath = Path.Combine(outputFolder, string.Format("{0}.tif", outputFileName));

                //converts the PDF pages to png's 
                var pdf2Tiff = rasterizer.GetPage(xDpi, yDpi, pageNumber);

                //save the png's
                pdf2Tiff.Save(outputTiffPath, ImageFormat.Tiff);
            }
        }
    }
}
