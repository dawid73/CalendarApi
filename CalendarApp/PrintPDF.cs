using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp
{
    class PrintPDF
    {
        // drukowanie PDF przekazanego w filePath na domyślnej drukarce systemowej 
        public void print(String filePath)
        {
            PrintDocument pd = new PrintDocument();
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = true;
            startInfo.FileName = filePath;
            startInfo.Verb = "print";
            startInfo.Arguments = @"/p /h \" + filePath + "\"\"" + pd.PrinterSettings.PrinterName + "\"";
            p.StartInfo = startInfo;
            p.Start();
            p.WaitForExit();
        }

    }
}
