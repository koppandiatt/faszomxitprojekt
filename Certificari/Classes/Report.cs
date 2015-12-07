using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Additional references

using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Data;
using System.Windows.Controls;
using System.Runtime.InteropServices;

namespace Certificari.Classes
{
    static class Report
    {

        public static void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                        ref matchCase, ref matchWholeWord,
                        ref matchWildCards, ref matchSoundLike,
                        ref nmatchAllForms, ref forward,
                        ref wrap, ref format, ref replaceWithText,
                        ref replace, ref matchKashida,
                        ref matchDiactitics, ref matchAlefHamza,
                        ref matchControl);
        }

    
        public static void CreateWordDocument(object docPath, string savaAs, string docName,DataRowView[] candidats,string presedinte,string membru1, string membru2)
        {

           int candiLength = candidats.Length;
           Console.WriteLine("LENGTH" + candiLength);
         
           object missing = Missing.Value;
     //      Word.Document[] adocs = new Word.Document[candidats.Length];


           for (int i = 0; i < candiLength; ++i)
           {

               Word.Document aDoc = new Word.Document();
               Word.Application wordApp = new Word.Application();
                DataRow candidat = candidats[i].Row;
                List<int> processesbeforegen = getRunningProcesses();
                string savePath =(string) savaAs + docName + " " + candidat["Nume"] + "_" + candidat["Prenume"] ;

              

                if (File.Exists((string)docPath))
                {
                    Console.WriteLine("Fajlnal vagyok");
                    DateTime today = DateTime.Now;

                    object readOnly = false; //default
                    object isVisible = false;

                    wordApp.Visible = false;

                    aDoc = wordApp.Documents.Open(ref docPath, ref missing, ref readOnly,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing, ref missing);

                    aDoc.Activate();

                    Console.WriteLine("megnyitva");

                    //Find and replace:
                    FindAndReplace(wordApp, "{NrMatricol}", candidat["NrMatricol"]);
                    FindAndReplace(wordApp, "{Nume}", candidat["Nume"]);
                    FindAndReplace(wordApp, "{Prenume}", candidat["Prenume"]);
                    FindAndReplace(wordApp, "{Tata}", candidat["Tata"]);
                    FindAndReplace(wordApp, "{Mama}", candidat["Mama"]);
                    FindAndReplace(wordApp, "{CNP>", candidat["CNP"]);
                    FindAndReplace(wordApp, "{DataNasterii}", candidat["DataNasterii"]);
                    FindAndReplace(wordApp, "{LoculNasterii}", candidat["LoculNasterii"]);
                    FindAndReplace(wordApp, "{Strada}", candidat["Strada"]);
                    FindAndReplace(wordApp, "{Nr}", candidat["Nr"]);
                    FindAndReplace(wordApp, "{Bloc}", candidat["Bloc"]);
                    FindAndReplace(wordApp, "{Scara}", candidat["Scara"]);
                    FindAndReplace(wordApp, "{Ap}", candidat["Ap"]);
                    FindAndReplace(wordApp, "{Localitate}", candidat["Localitate"]);
                    FindAndReplace(wordApp, "{Judet}", candidat["Judet"]);
                    FindAndReplace(wordApp, "{Cp}", candidat["Cp"]);
                    FindAndReplace(wordApp, "{Telefon}", candidat["Telefon"]);
                    FindAndReplace(wordApp, "{Studii}", candidat["Studii"]);
                    FindAndReplace(wordApp, "{Profesia}", candidat["Profesia"]);
                    FindAndReplace(wordApp, "{LocMunca}", candidat["LocMunca"]);
                    FindAndReplace(wordApp, "{Presedinte}", presedinte);
                    FindAndReplace(wordApp, "{Membru1}", membru1);
                    FindAndReplace(wordApp, "{Membru2}", membru2);


                    Console.WriteLine("cserelgetes");

                    object saveFullPath = savePath;

                    aDoc.SaveAs2(ref saveFullPath, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                    //Close Document:

                    aDoc.Close(ref missing, ref missing, ref missing);
                    wordApp.Quit(false);

                  

                    List<int> processesaftergen = getRunningProcesses();
                    Console.WriteLine("COUNT > " + processesbeforegen.Count + "  " + processesaftergen.Count);
                    killProcesses(processesbeforegen, processesaftergen);
                }else {
                    System.Windows.MessageBox.Show("file dose not exist.");
                    return;
                }
            }

        
        }


        private static List<int> getRunningProcesses()
        {
            List<int> ProcessIDs = new List<int>();
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (Process.GetCurrentProcess().Id == clsProcess.Id)
                    continue;
                if (clsProcess.ProcessName.Contains("Microsoft Word"))
                {
                    ProcessIDs.Add(clsProcess.Id);
                }
            }
            return ProcessIDs;
        }

        private static void killProcesses(List<int> processesbeforegen, List<int> processesaftergen)
        {
            foreach (int pidafter in processesaftergen)
            {
                bool processfound = false;
                foreach (int pidbefore in processesbeforegen)
                {
                    if (pidafter == pidbefore)
                    {
                        processfound = true;
                    }
                }

                if (processfound == false)
                {
                    Process clsProcess = Process.GetProcessById(pidafter);
                    clsProcess.Kill();
                }
            }
        }

    }
}
