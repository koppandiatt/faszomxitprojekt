using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;
using System.IO.Packaging; 

namespace Certificari.Classes
{
    static class DocumentPrevier
    {
        public static XpsDocument ConvertWordToXps(string wordFilename, string xpsFilename)
        {
            // Create a WordApplication and host word document 
            Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                wordApp.Documents.Open(wordFilename);

                // To Invisible the word document 
                wordApp.Application.Visible = false;


                // Minimize the opened word document 
                wordApp.WindowState = WdWindowState.wdWindowStateMinimize;


                Document doc = wordApp.ActiveDocument;


                doc.SaveAs(xpsFilename, WdSaveFormat.wdFormatXPS);


                XpsDocument xpsDocument = new XpsDocument(xpsFilename, FileAccess.Read);
                
                return xpsDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs, The error message is  " + ex.ToString());
                return null;
            }
            finally
            {
                wordApp.Documents.Close();
                ((_Application)wordApp).Quit(WdSaveOptions.wdDoNotSaveChanges);
            }
        } 


    }
}
