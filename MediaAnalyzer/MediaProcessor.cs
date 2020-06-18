using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;

namespace MediaAnalyzer
{
    public abstract class MediaProcessor : IDisposable
    {
        private bool isDisposed = false;

        protected virtual string ResourcePath { get; }
        protected virtual string ExePath { get; }
        protected virtual Process MediaProcess { get; set; }

        protected MediaProcessor()
        {
            isDisposed = false;
            EnsureExecutableFile();
        }
        private void EnsureExecutableFile()
        {
            if (!File.Exists(ExePath))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(ResourcePath))
                {
                    byte[] process_bin = new byte[stream.Length];
                    stream.Read(process_bin, 0, (int)stream.Length);
                    using (FileStream fs = new FileStream(ExePath, FileMode.Create))
                    {
                        fs.Write(process_bin, 0, process_bin.Length);
                    }
                }
            }
        }
        public virtual void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || isDisposed)
            {
                return;
            }

            if (MediaProcess != null)
            {
                MediaProcess.Dispose();
            }
            MediaProcess = null;
            isDisposed = true;
        }
    }
}
