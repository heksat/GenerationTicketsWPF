using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;
namespace GenerationTicketsWPF
{
    class WordHelper
    {
        private FileInfo fileinfo;
        public FileInfo getFileInfo { get { return fileinfo; } }
        public WordHelper(string filename)
        {
            if (File.Exists(filename))
            {
                fileinfo = new FileInfo(filename);
            }
            else
            {
                MessageBox.Show("Не найден шаблон");
            }
        }

        internal void Process(Dictionary<string, string> item, string path, Word.Application app, string file, object missing)
        {
           // var file = fileinfo.FullName;
            //var missing = Type.Missing; 
           // app.Documents.Open(file);
            foreach (var i in item)
            {
                Word.Find find = app.Selection.Find;
                find.Text = i.Key;
                find.Replacement.Text = i.Value;
                object wrap = Word.WdFindWrap.wdFindContinue;
                object replace = Word.WdReplace.wdReplaceAll;
                find.Execute(FindText: missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing,
                    Replace: replace);
            }
            var newFile = Path.Combine($"{path}\\Ticket{item["<NUMB>"]}");
            app.ActiveDocument.SaveAs2(newFile);
            //app.ActiveDocument.UndoClear();

            //https://docs.microsoft.com/ru-ru/dotnet/api/microsoft.office.tools.word.document.undo?view=vsto-2017
            // app.ActiveDocument.Close();
            // app.Quit();
        }
    }
}
