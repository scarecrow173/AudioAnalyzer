using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MediaAnalyzer;

namespace MediaAnalyzer_Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            FFmpegProcessor fMPEG = new FFmpegProcessor();
            string[] files = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.wav");
            foreach (string path in files)
            {
                fMPEG.ParseAudioMetadata(path);
            }
            SortedDictionary<AudioMetadata.ChannelId, string> MonoFiles = new SortedDictionary<AudioMetadata.ChannelId, string>()
            {
                { AudioMetadata.ChannelId.FL, Path.Combine(Directory.GetCurrentDirectory(), "front_left.wav") },
                { AudioMetadata.ChannelId.FR, Path.Combine(Directory.GetCurrentDirectory(), "front_right.wav") },
                { AudioMetadata.ChannelId.FC, Path.Combine(Directory.GetCurrentDirectory(), "front_center.wav") },
                { AudioMetadata.ChannelId.LFE, Path.Combine(Directory.GetCurrentDirectory(), "lfe.wav") },
                { AudioMetadata.ChannelId.BL, Path.Combine(Directory.GetCurrentDirectory(), "side_left.wav") },
                { AudioMetadata.ChannelId.BR, Path.Combine(Directory.GetCurrentDirectory(), "side_right.wav") },
            };
            fMPEG.ConvertAudioChannel(MonoFiles, "output_51.wav");

            SortedDictionary<AudioMetadata.ChannelId, string> OutputFiles = new SortedDictionary<AudioMetadata.ChannelId, string>()
            {
                { AudioMetadata.ChannelId.FL, Path.Combine(Directory.GetCurrentDirectory(), "output_front_left.wav") },
                { AudioMetadata.ChannelId.FR, Path.Combine(Directory.GetCurrentDirectory(), "output_front_right.wav") },
                { AudioMetadata.ChannelId.FC, Path.Combine(Directory.GetCurrentDirectory(), "output_front_center.wav") },
                { AudioMetadata.ChannelId.LFE, Path.Combine(Directory.GetCurrentDirectory(), "output_lfe.wav") },
                { AudioMetadata.ChannelId.BL, Path.Combine(Directory.GetCurrentDirectory(), "output_side_left.wav") },
                { AudioMetadata.ChannelId.BR, Path.Combine(Directory.GetCurrentDirectory(), "output_side_right.wav") },
            };
            fMPEG.ConvertAudioChannel(Path.Combine(Directory.GetCurrentDirectory(), "51sample.wma"), OutputFiles);

            fMPEG.AudioDownmix("51sample.wma", "output_downmix.mp3", 2);
        }
    }
}
