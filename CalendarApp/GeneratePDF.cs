using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Printing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalendarApp
{
    class GeneratePDF
    {
        public void pdfCreate(String[,] googleEventTab)
        {
            

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "test PDF";
            PdfPage pdfpage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfpage);
            XFont font= new XFont("Verdana", 20, XFontStyle.Bold);

            int y = 0;
            for(int i = 0; i<3; i ++) { 
                y += 45;
                graph.DrawString(googleEventTab[0,i], font, XBrushes.Black, 10, y);
                y += 25;
                graph.DrawString(googleEventTab[1, i], font, XBrushes.Black, 30, y);
            }

            string pdffilename = "test.pdf";
            pdf.Save(pdffilename);
            
            string filePath = "C:\\Users\\dhonkisz\\source\\repos\\CalendarApi\\CalendarApp\\bin\\Debug\\test.pdf";
            PrintPDF print = new PrintPDF();
            print.print(filePath);




        }
    }
}
    