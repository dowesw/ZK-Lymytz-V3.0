using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace ZK_LymytzService.TOOLS
{
    public class TxtFileReader : IDisposable
    {
        private StreamReader Reader;
        private string CurrLine;

        public TxtFileReader(Stream stream)
        {
            Reader = new StreamReader(stream);
        }

        public TxtFileReader(string path)
        {
            Reader = new StreamReader(path);
        }

        public List<string> ReadRow()
        {
            List<string> lignes = new List<string>();

            while ((CurrLine = Reader.ReadLine()) != null)
            {
                lignes.Add(CurrLine);
            }
            return lignes;
        }

        // Propagate Dispose to StreamReader
        public void Dispose()
        {
            Reader.Dispose();
        }
    }

    /// <summary>
    /// Class for writing to comma-separated-value (CSV) files.
    /// </summary>
    public class TxtFileWriter : IDisposable
    {
        private StreamWriter Writer;

        public TxtFileWriter(Stream stream)
        {
            Writer = new StreamWriter(stream);
        }

        public TxtFileWriter(string path)
        {
            Writer = new StreamWriter(path);
        }

        public TxtFileWriter(string path, bool append, Encoding encoding)
        {
            Writer = new StreamWriter(path, append, encoding);
        }

        public void WriteRow(string logMessage)
        {
            if (logMessage == null)
            {
                return;
            }
            Writer.WriteLine("{0}", logMessage);
        }

        // Propagate Dispose to StreamWriter
        public void Dispose()
        {
            Writer.Dispose();
        }
    }
}