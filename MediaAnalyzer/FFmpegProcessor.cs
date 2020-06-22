using System;
using System.Collections.Generic;
using System.Collections;
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
        /*
         * ‘-L’
         *    ライセンスを表示します。
         * ‘-h, -?, -help, --help’
         *    ヘルプを表示します。
         * ‘-version’
         *    バージョンを表示します。
         * ‘-formats’
         *    利用可能なフォーマットを表示します。
         *    フォーマット名の前にあるフィールドは次のような意味があります:
         *   ‘D’
         *      デコーディングが利用できる
         *   ‘E’
         *      エンコーディングが利用できる
         * ‘-codecs’
         *    利用可能なコーデックを表示します。
         *    コーデック名の前にあるフィールドは次のような意味があります:
         *   ‘D’
         *      デコーディングが利用できる
         *   ‘E’
         *      エンコーディングが利用できる
         *   ‘V/A/S’
         *      映像/音声/サブタイトルコーデック
         *   ‘S’
         *      コーデックがスライスをサポートしている
         *   ‘D’
         *      コーデックが direct rendering をサポートしている
         *   ‘T’
         *      コーデックがフレーム境界でだけでなくランダムな場所での切り取られた入力を扱える
         * ‘-bsfs’
         *    利用可能な bitstream フィルターを表示します。
         * ‘-protocols’
         *    利用可能なプロトコルを表示します。
         * ‘-filters’
         *    利用可能な libavfilter フィルターを表示します。
         * ‘-pix_fmts’
         *    利用可能な pixel フォーマットを表示します。
         * ‘-loglevel loglevel’
         *    そのライブラリで使用されるログの冗長さを設定します。 loglevel は以下の値の1つを含んだ数値または文字列:
         *   ‘quiet’
         *   ‘panic’
         *   ‘fatal’
         *   ‘error’
         *   ‘warning’
         *   ‘info’
         *   ‘verbose’
         *   ‘debug’
         *    既定ではプログラムは標準エラー出力にログを出力し、端末が色付けに 対応していれば、エラーと警告に印をつけるように色が使われます。 ログの色付けは環境変数 NO_COLOR をセットすることで無効にできます。
         * ‘-aframes number’
         *    録音するオーディオフレームの数を設定します。
         * ‘-ar freq’
         *    オーディオサンプリング頻度を設定します(既定値は44100Hz)。
         * ‘-ab bitrate’
         *    オーディオビットレートをbit/sで設定します(既定値は64k)。
         * ‘-aq q’
         *    オーディオ品質を設定します(コーデック別、VBR)。
         * ‘-ac channels’
         *    オーディオチャンネルの数を設定します。入力ストリームについては、既定では1に 設定されます。出力ストリームについては、既定では入力にあるオーディオチャンネルと 同じ数に設定されます。入力ファイルにチャンネル数と異なる数のオーディオストリームが ある場合、その振る舞いは不定です。
         * ‘-an’
         *    音声の録音を無効にします。
         * ‘-acodec codec’
         *    オーディオのコーデックを codec に強制します。未加工のコーデックデータ をありのままコピーするには copy という特別な値を使ってください。 値を使ってください。
         * ‘-vol volume’
         *    音声のボリュームを変更する。(未指定 or 256 でそのまま)。
         * ‘-newaudio’
         *    出力ファイルに新しいオーディオトラックを追加氏増す。パラメーターを指定したい場合には、 -newaudio の前にそうしてください(-acodec、-ab、など)。
         *    出力ストリームの数が入力ストリームの数と等しい場合には、対応づけは自動的にされます。 そうでない場合には、マッチする最初のものが選ばれます。 いつものように対応づけは -map を使って変更できます。
         *    例:
         *    ffmpeg -i file.mpg -vcodec copy -acodec ac3 -ab 384k test.mpg -acodec mp2 -ab 192k -newaudio
         * ‘-alang code’
         *    現在の音声ストリームの ISO 639 言語コード(3文字)を設定します。
         * ‘-sn’
         *    サブタイトルの記録を無効にします。
         * ‘-sbsf bitstream_filter’
         *    ビットストリームフィルター。"mov2textsub"、"text2movsub" が利用できます。
         *    ffmpeg -i file.mov -an -vn -sbsf mov2textsub -scodec copy -f rawvideo sub.txt
         * ‘-atag fourcc/tag’
         *    オーディオのタグ/fourcc を強制します。
         * ‘-absf bitstream_filter’
         *    ビットストリームフィルター。"dump_extra"、"remove_extra"、"noise"、"mp3comp"、"mp3decomp" が利用できます。
         * ‘-map input_stream_id[:sync_stream_id]’
         *    入力ストリームから出力ストリームへのストリームの対応付けを設定します。 入力ストリームを出力したい順に並べてください。 sync_stream_id が指定された場合、同期する入力ストリームを設定します。
         * ‘-map_meta_data outfile[,metadata]:infile[,metadata]’
         *    infile から outfile のメタデータ情報を設定します。これらは (0から始まる)ファイルのインデックスであり、ファイル名ではないことに注意してください。 省略可能な metadata パラメータはどのメタデータをコピーするか - (g)lobal (つまり、ファイル全体に適用されるメタデータ)、per-(s)tream、per-(c)hapter または per-(p)rogram です。global 以外の全てのメタデータ指定子では stream/chapter/program 番号が続かなければなりません。メタデータ指定子が省略された場合、既定の global に なります。
         *    既定では、global メタデータは最初の入力ファイルから全ての出力ファイルにコピーされ、 per-stream および per-chapter メタデータは stream/chapter に合わせてコピーされます。 これらの既定のマッピングは関連する種類のマッピングを作成することで無効になります。 自動的なコピーを無効にするだけのダミーのマッピングを作成するために、負のファイルインデックスが利用できます。
         *    例えばメタデータを入力ファイルの最初のストリームから出力ファイルの global メタデータ にコピーするには:
         *    ffmpeg -i in.ogg -map_meta_data 0:0,s0 out.mp3
         * ‘-map_chapters outfile:infile’
         *    infile から outfile へチャプターをコピーします。チャプターマッピングが指定されていなければ、 少なくとも1つチャプターがある最初の入力ファイルから全ての出力ファイルへコピーされます。チャプターのコピーを 一切無効にするには負のファイルインデックスを使ってください。
         * ‘-debug’
         *    特定のデバグ情報を印字します。
         * ‘-benchmark’
         *    エンコードの終了時にベンチマーク情報を表示します。 CPU 時間と最大メモリ消費を表示します。 最大メモリ消費は全てのシステムでサポートされているわけではなく、 サポートされていない場合にはたいてい 0 として表示します。
         * ‘-dump’
         *    各入力パケットをダンプします。
         * ‘-hex’
         *    パケットをダンプする際に、ペイロードもダンプします。
         * ‘-bitexact’
         *    (コーデックのテストのために)bit exact アルゴリズムのみを使います。
         * ‘-ps size’
         *    バイト単位で RTP ペイロードサイズを設定します。
         * ‘-re’
         *    ネイティブのフレームレートで入力を読み込みます。主にグラブデバイスをシミュレートするために使います。
         * ‘-loop_input’
         *    入力ストリームをループします。現時点では画像ストリームに対してのみ 動作します。このオプションは自動的な FFserver テストに使われます。
         * ‘-loop_output number_of_times’
         *    アニメーション GIF のようなループをサポートしているフォーマット向けに 繰り返しループして出力します(0 で無限にループ出力します)。
         * ‘-threads count’
         *    スレッド数。
         * ‘-vsync parameter’
         *    ビデオ同期方式。 0 demuxer から muxer へタイムスタンプとともに各フレームを渡します。 1 要求された定数フレームレートを実現するためにフレームを重複させたり 抜かしたりします。 2 フレームはタイムスタンプとともに渡されますが、同じタイムスタンプの2つの フレームがあれば抜かします。 -1 muxer の能力に応じて 1 か 2 を選択する。これが既定の方法です。 -map を併用することでどのストリームからタイムスタンプを取得するべきか選択できます。 ビデオまたはオーディオを変更せずにおいておくことができ、その変更しないものに対し 残りのストリームを同期することができます。
         * ‘-async samples_per_second’
         *    オーディオ同期方式。タイムスタンプに合わせて音声ストリームを“伸長/圧縮”し、 パラメーターは音声を変更するときに用いられる秒間最大サンプル数です。 -async 1 は音声ストリームの開始のみを補正し以後補正しないという特殊な場合です。
         * ‘-copyts’
         *    入力から出力へタイムスタンプをコピーします。
         * ‘-shortest’
         *    最も短い入力ストリームが終わり次第エンコーディングを終えます。
         * ‘-dts_delta_threshold’
         *    タイムスタンプを不連続差分閾値にする。
         * ‘-muxdelay seconds’
         *    demux-デコード遅延の最大値を設定する。
         * ‘-muxpreload seconds’
         *    初期 demux-デコード遅延を設定する。
         * ‘-streamid output-stream-index:new-value’
         *    次の出力ファイルの中の、あるストリームの stream-id フィールドに新しい値を 割り当てます。各出力ファイルにおいて、全ての stream-id フィールドは既定値に リセットされます。
         *    例えば、ある出力 mpegts ファイルに対して stream 0 PID に33を、そして stream 1 PID に36を設定するためには:	
         *    ffmpeg -i infile -streamid 0:33 -streamid 1:36 out.ts
         */
        
        protected override string ResourcePath { get { return "MediaAnalyzer.ffmpeg.bin.ffmpeg.exe"; } }
        protected override string ExePath { get { return "ffmpeg.exe"; } }
        protected override Process MediaProcess { get; set; }

        static private string BaseArguments = "-nostdin -y -loglevel info -hide_banner ";
        static private string InputFileArgFormat = "-i \"{0}\" ";
        static private string FilterComplexOptionArgFormat = "-filter_complex \"{0}\" ";

        public FFmpegProcessor() : base()
        {
            
        }

        private ProcessStartInfo GenerateProcessInfo(string arguments)
        {
            return new ProcessStartInfo
            {
                Arguments = BaseArguments + arguments,
                FileName = ExePath,
                CreateNoWindow = true,
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };
        }

        public AudioMetadata ParseAudioMetadata(string inputFile)
        {
            AudioMetadata audioMetadata = new AudioMetadata();
            ProcessStartInfo psi = GenerateProcessInfo(string.Format(InputFileArgFormat, inputFile));
            string ffmpeg_output = StartProcess(psi);
            audioMetadata.Parse(ffmpeg_output);
            return audioMetadata;
        }
        private string StartProcess(ProcessStartInfo psi)
        {
            string ffmpeg_output = string.Empty;
            using (MediaProcess = Process.Start(psi))
            {
                MediaProcess.ErrorDataReceived += (sender, received) =>
                {
                    if (received.Data == null) return;
                    ffmpeg_output += received.Data + Environment.NewLine;
                };
                MediaProcess.BeginErrorReadLine();
                MediaProcess.WaitForExit();
                Console.WriteLine(ffmpeg_output);
            }
            return ffmpeg_output;
        }

        // mono*N to multichannel
        public void ConvertAudioChannel(Dictionary<AudioMetadata.ChannelId, string> inputFiles, string outputFile)
        {
            string InputFileArgs = string.Empty;
            string JoinFilterArgs = string.Empty;
            string MapArg = string.Empty;
            foreach (var input in inputFiles.Select((item, index) => new { item, index }))
            {
                InputFileArgs += string.Format(InputFileArgFormat, input.item.Value);
                JoinFilterArgs += string.Format("[{0}:a]", input.index);
                MapArg += string.Format("{0:F1}-{1}{2}", 
                    input.index, 
                    AudioMetadata.ChannelIdToChannelStr(input.item.Key),
                    input.index != inputFiles.Count - 1 ? "|" : "");
            }
            JoinFilterArgs += string.Format("join=inputs={0}:channel_layout={1}:map={2}[a]", 
                inputFiles.Count,
                inputFiles.Count == 1 ? "mono" : AudioMetadata.ChannelLayoutToChannelLayoutStr(inputFiles.Keys.ToArray()),
                MapArg);

            string MapOptionArgs = "-map \"[a]\" ";
            string Arguments = InputFileArgs + string.Format(FilterComplexOptionArgFormat, JoinFilterArgs) + MapOptionArgs + string.Format("\"{0}\"", outputFile);
            ProcessStartInfo psi = GenerateProcessInfo(Arguments);
            StartProcess(psi);
        }

        public void ConvertAudioChannel(string inputFile, Dictionary<AudioMetadata.ChannelId, string> outputFiles)
        {
            string LayoutFilterArgs = string.Format("channel_layout={0}", outputFiles.Count == 1 ? "mono" : AudioMetadata.ChannelLayoutToChannelLayoutStr(outputFiles.Keys.ToArray()));
            string MapOptionArgs = string.Empty;
            foreach (var output in outputFiles)
            {
                LayoutFilterArgs += string.Format("[{0}]", AudioMetadata.ChannelIdToChannelStr(output.Key));
                MapOptionArgs += string.Format(" -map \"[{0}]\" \"{1}\"", AudioMetadata.ChannelIdToChannelStr(output.Key), output.Value);
            }
            string SplitFilterArgs = string.Format("channelsplit={0}", LayoutFilterArgs);
            string Arguments = string.Format(InputFileArgFormat, inputFile) + string.Format(FilterComplexOptionArgFormat, SplitFilterArgs) + MapOptionArgs;
            ProcessStartInfo psi = GenerateProcessInfo(Arguments);
            StartProcess(psi);
        }

        public void AudioDownmix(string inputFile, string outputFile, int outputChannelCount)
        {
            string Arguments = string.Format(InputFileArgFormat, inputFile) + string.Format("-ac {0} ", outputChannelCount) + string.Format("\"{0}\"", outputFile);
            ProcessStartInfo psi = GenerateProcessInfo(Arguments);
            StartProcess(psi);
        }

    }
}
