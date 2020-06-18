using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace MediaAnalyzer
{
    public class FFmpegProcessor : MediaProcessor
    {
        protected override string ResourcePath { get { return "MediaAnalyzer.ffmpeg.bin.ffmpeg.exe"; } }
        protected override string ExePath { get { return "ffmpeg.exe"; } }
        protected override Process MediaProcess { get; set; }

        public FFmpegProcessor() : base()
        {

        }
    }
}
