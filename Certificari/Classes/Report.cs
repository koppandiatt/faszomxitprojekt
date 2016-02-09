// Additional references

using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;

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

        public static bool CreateWordsDocuments(object docPath, string savaAs, string docName, DataRowView[] candidats, string presedinte, string membru1, string membru2)
        {
            int candiLength = candidats.Length;
            Console.WriteLine("LENGTH" + candiLength);

            object missing = Missing.Value;
            //      Word.Document[] adocs = new Word.Document[candidats.Length];

            Word.Document aDoc = null;
            Word.Application wordApp = null;


            try{
                for (int i = 0; i < candiLength; ++i)
                {

                    aDoc = new Word.Document();
                    wordApp = new Word.Application();
                    DataRow candidat = candidats[i].Row;
                    List<int> processesbeforegen = getRunningProcesses();
                    string savePath =(string) savaAs + docName + " " + candidat["Nume"] + "_" + candidat["Prenume"] ;
                 


                    if (File.Exists((string)docPath))
                    {
                        Console.WriteLine("Fajlnal vagyok");
                   
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

                        Marshal.FinalReleaseComObject(wordApp);
                      
                        aDoc = null;
                        wordApp = null;
                        List<int> processesaftergen = getRunningProcesses();
                        Console.WriteLine("COUNT > " + processesbeforegen.Count + "  " + processesaftergen.Count);
                        killProcesses(processesbeforegen, processesaftergen);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("file dose not exist.");
                        return false;
                    }
                }


 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error occured at Report!");
                return false;
            }
            finally
            {
                if (aDoc != null) aDoc.Close(ref missing, ref missing, ref missing);
                if (wordApp != null)
                {
                    wordApp.Quit(ref missing, ref missing, ref missing);
                    Marshal.FinalReleaseComObject(wordApp);
                }

            }

            return true;

        }

    
        public static bool CreateWordsDocumentToOne(object docPath, string savaAs, string docName,DataRowView[] candidats,string presedinte,string membru1, string membru2)
        {

           int candiLength = candidats.Length;
           Console.WriteLine("LENGTH" + candiLength);
         
           object missing = Missing.Value;
     //      Word.Document[] adocs = new Word.Document[candidats.Length];
   

           string output = savaAs + docName + "-" + "candidats.docx";


           Application createEmptyDocApp = null;
           Word.Document createEmptyDoc = null;

           Word._Application wordAppForOutput = null;
           Word._Document wordDocument = null;

           Word.Document aDoc = null;
           Word.Application wordApp = null;

           try
           {
               if (File.Exists(output))
                   File.Delete(output);

             
               if (!File.Exists(output))
               {
                   createEmptyDocApp = new Application();
                   createEmptyDoc = createEmptyDocApp.Documents.Add();

                   createEmptyDocApp.ActiveDocument.SaveAs(output, ref missing, ref missing,
                                                                          ref missing, ref missing, ref missing, ref missing,
                                                                          ref missing, ref missing, ref missing, ref missing);
                   createEmptyDoc.Close();
                   if (createEmptyDocApp != null)
                   {
                       createEmptyDocApp.Quit();
                       Marshal.FinalReleaseComObject(createEmptyDocApp);
                   }

                   createEmptyDoc = null;
                   createEmptyDocApp = null;

               }
               else
               {
                   return false;
               }

               object pageBreak = Word.WdBreakType.wdPageBreak;
               wordAppForOutput = new Word.Application();

               wordAppForOutput.Visible = false;

               wordDocument = wordAppForOutput.Documents.Open(
                                             output
                                             , false, false, ref missing,
                                                        ref missing, ref missing, ref missing, ref missing,
                                                        ref missing, ref missing, ref missing, true,
                                                        ref missing, ref missing, ref missing, ref missing);

               wordDocument.Activate();

               

               for (int i = 0; i < candiLength; ++i)
               {

                   aDoc = new Word.Document();
                   wordApp = new Word.Application();
                   DataRow candidat = candidats[i].Row;
                   List<int> processesbeforegen = getRunningProcesses();
                   // string savePath =(string) savaAs + docName + " " + candidat["Nume"] + "_" + candidat["Prenume"] ;
                   string savePath = savaAs + "tempDoc";


                   if (File.Exists((string)docPath))
                   {
                       Console.WriteLine("Fajlnal vagyok");
               
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
                      
                       Marshal.FinalReleaseComObject(wordApp);
                       string PathToMerge = savePath + ".docx";
                       if (File.Exists(PathToMerge))
                       {
                           wordAppForOutput.Selection.InsertFile(PathToMerge, ref missing, true, ref missing, ref missing);
                           wordAppForOutput.Selection.InsertBreak(ref pageBreak);
                           File.Delete(PathToMerge);
                       }
                       else
                       {
                           Console.WriteLine("dosent exist the file");
                       }

                       aDoc = null;
                       wordApp = null;
                       List<int> processesaftergen = getRunningProcesses();
                       Console.WriteLine("COUNT > " + processesbeforegen.Count + "  " + processesaftergen.Count);
                       killProcesses(processesbeforegen, processesaftergen);
                   }
                   else
                   {
                       System.Windows.MessageBox.Show("file dose not exist.");
                       return false;
                   }
               }


               // Clean up!
               wordDocument.SaveAs(output, ref missing, ref missing,
                                            ref missing, ref missing, ref missing, 
                                            ref missing, ref missing, ref missing, ref missing, ref missing);
               wordDocument.Close(ref missing, ref missing, ref missing);
               wordAppForOutput.Quit(ref missing, ref missing, ref missing);
               Marshal.FinalReleaseComObject(wordAppForOutput);
               wordDocument = null;
               wordAppForOutput = null;
           }
           catch (Exception ex)
           {
               System.Windows.MessageBox.Show("Error occured at Report!");
               return false;
           }
           finally
           {
               if (createEmptyDoc != null) createEmptyDoc.Close(ref missing, ref missing, ref missing);
               if (createEmptyDocApp != null)
               {
                   createEmptyDocApp.Quit(ref missing, ref missing, ref missing);
                   Marshal.FinalReleaseComObject(createEmptyDocApp);
               }

               if (wordDocument != null) wordDocument.Close(ref missing, ref missing, ref missing);
               if (wordAppForOutput != null)
               {
                   wordAppForOutput.Quit(ref missing, ref missing, ref missing);
                   Marshal.FinalReleaseComObject(wordAppForOutput);
               }

               if (aDoc != null) aDoc.Close(ref missing, ref missing, ref missing);
               if (wordApp != null)
               {
                   wordApp.Quit(ref missing, ref missing, ref missing);
                   Marshal.FinalReleaseComObject(wordApp);
               }
       
           }

           return true;
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
