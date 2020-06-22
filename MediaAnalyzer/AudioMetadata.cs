using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediaAnalyzer
{
    public class AudioMetadata
    {
        public string FileName { get; set; } = string.Empty;

        /// <summary>Audio Codec</summary>
        public enum AudioCodecId
        {
            AUDIO_CODEC_ID_UNKNOWN,
            AUDIO_CODEC_ID_PCM_S16LE,
            AUDIO_CODEC_ID_PCM_S16BE,
            AUDIO_CODEC_ID_PCM_U16LE,
            AUDIO_CODEC_ID_PCM_U16BE,
            AUDIO_CODEC_ID_PCM_S8,
            AUDIO_CODEC_ID_PCM_U8,
            AUDIO_CODEC_ID_PCM_MULAW,
            AUDIO_CODEC_ID_PCM_ALAW,
            AUDIO_CODEC_ID_PCM_S32LE,
            AUDIO_CODEC_ID_PCM_S32BE,
            AUDIO_CODEC_ID_PCM_U32LE,
            AUDIO_CODEC_ID_PCM_U32BE,
            AUDIO_CODEC_ID_PCM_S24LE,
            AUDIO_CODEC_ID_PCM_S24BE,
            AUDIO_CODEC_ID_PCM_U24LE,
            AUDIO_CODEC_ID_PCM_U24BE,
            AUDIO_CODEC_ID_PCM_S24DAUD,
            AUDIO_CODEC_ID_PCM_ZORK,
            AUDIO_CODEC_ID_PCM_S16LE_PLANAR,
            AUDIO_CODEC_ID_PCM_DVD,
            AUDIO_CODEC_ID_PCM_F32BE,
            AUDIO_CODEC_ID_PCM_F32LE,
            AUDIO_CODEC_ID_PCM_F64BE,
            AUDIO_CODEC_ID_PCM_F64LE,
            AUDIO_CODEC_ID_PCM_BLURAY,
            AUDIO_CODEC_ID_PCM_LXF,
            AUDIO_CODEC_ID_S302M,
            AUDIO_CODEC_ID_PCM_S8_PLANAR,
            AUDIO_CODEC_ID_PCM_S24LE_PLANAR,
            AUDIO_CODEC_ID_PCM_S32LE_PLANAR,
            AUDIO_CODEC_ID_PCM_S16BE_PLANAR,
            AUDIO_CODEC_ID_PCM_S64LE,
            AUDIO_CODEC_ID_PCM_S64BE,
            AUDIO_CODEC_ID_PCM_F16LE,
            AUDIO_CODEC_ID_PCM_F24LE,
            AUDIO_CODEC_ID_PCM_VIDC,
            AUDIO_CODEC_ID_ADPCM_IMA_QT,
            AUDIO_CODEC_ID_ADPCM_IMA_WAV,
            AUDIO_CODEC_ID_ADPCM_IMA_DK3,
            AUDIO_CODEC_ID_ADPCM_IMA_DK4,
            AUDIO_CODEC_ID_ADPCM_IMA_WS,
            AUDIO_CODEC_ID_ADPCM_IMA_SMJPEG,
            AUDIO_CODEC_ID_ADPCM_MS,
            AUDIO_CODEC_ID_ADPCM_4XM,
            AUDIO_CODEC_ID_ADPCM_XA,
            AUDIO_CODEC_ID_ADPCM_ADX,
            AUDIO_CODEC_ID_ADPCM_EA,
            AUDIO_CODEC_ID_ADPCM_G726,
            AUDIO_CODEC_ID_ADPCM_CT,
            AUDIO_CODEC_ID_ADPCM_SWF,
            AUDIO_CODEC_ID_ADPCM_YAMAHA,
            AUDIO_CODEC_ID_ADPCM_SBPRO_4,
            AUDIO_CODEC_ID_ADPCM_SBPRO_3,
            AUDIO_CODEC_ID_ADPCM_SBPRO_2,
            AUDIO_CODEC_ID_ADPCM_THP,
            AUDIO_CODEC_ID_ADPCM_IMA_AMV,
            AUDIO_CODEC_ID_ADPCM_EA_R1,
            AUDIO_CODEC_ID_ADPCM_EA_R3,
            AUDIO_CODEC_ID_ADPCM_EA_R2,
            AUDIO_CODEC_ID_ADPCM_IMA_EA_SEAD,
            AUDIO_CODEC_ID_ADPCM_IMA_EA_EACS,
            AUDIO_CODEC_ID_ADPCM_EA_XAS,
            AUDIO_CODEC_ID_ADPCM_EA_MAXIS_XA,
            AUDIO_CODEC_ID_ADPCM_IMA_ISS,
            AUDIO_CODEC_ID_ADPCM_G722,
            AUDIO_CODEC_ID_ADPCM_IMA_APC,
            AUDIO_CODEC_ID_ADPCM_VIMA,
            AUDIO_CODEC_ID_ADPCM_AFC,
            AUDIO_CODEC_ID_ADPCM_IMA_OKI,
            AUDIO_CODEC_ID_ADPCM_DTK,
            AUDIO_CODEC_ID_ADPCM_IMA_RAD,
            AUDIO_CODEC_ID_ADPCM_G726LE,
            AUDIO_CODEC_ID_ADPCM_THP_LE,
            AUDIO_CODEC_ID_ADPCM_PSX,
            AUDIO_CODEC_ID_ADPCM_AICA,
            AUDIO_CODEC_ID_ADPCM_IMA_DAT4,
            AUDIO_CODEC_ID_ADPCM_MTAF,
            AUDIO_CODEC_ID_ADPCM_AGM,
            AUDIO_CODEC_ID_ADPCM_ARGO,
            AUDIO_CODEC_ID_ADPCM_IMA_SSI,
            AUDIO_CODEC_ID_ADPCM_ZORK,
            AUDIO_CODEC_ID_ADPCM_IMA_APM,
            AUDIO_CODEC_ID_ADPCM_IMA_ALP,
            AUDIO_CODEC_ID_ADPCM_IMA_MTF,
            AUDIO_CODEC_ID_ADPCM_IMA_CUNNING,
            AUDIO_CODEC_ID_AMR_NB,
            AUDIO_CODEC_ID_AMR_WB,
            AUDIO_CODEC_ID_RA_144,
            AUDIO_CODEC_ID_RA_288,
            AUDIO_CODEC_ID_ROQ_DPCM,
            AUDIO_CODEC_ID_INTERPLAY_DPCM,
            AUDIO_CODEC_ID_XAN_DPCM,
            AUDIO_CODEC_ID_SOL_DPCM,
            AUDIO_CODEC_ID_SDX2_DPCM,
            AUDIO_CODEC_ID_GREMLIN_DPCM,
            AUDIO_CODEC_ID_DERF_DPCM,
            AUDIO_CODEC_ID_MP2,
            // <summary>preferred ID for decoding MPEG audio layer 1, 2 or 3</summary>
            AUDIO_CODEC_ID_MP3,
            AUDIO_CODEC_ID_AAC,
            AUDIO_CODEC_ID_AC3,
            AUDIO_CODEC_ID_DTS,
            AUDIO_CODEC_ID_VORBIS,
            AUDIO_CODEC_ID_DVAUDIO,
            AUDIO_CODEC_ID_WMAV1,
            AUDIO_CODEC_ID_WMAV2,
            AUDIO_CODEC_ID_MACE3,
            AUDIO_CODEC_ID_MACE6,
            AUDIO_CODEC_ID_VMDAUDIO,
            AUDIO_CODEC_ID_FLAC,
            AUDIO_CODEC_ID_MP3ADU,
            AUDIO_CODEC_ID_MP3ON4,
            AUDIO_CODEC_ID_SHORTEN,
            AUDIO_CODEC_ID_ALAC,
            AUDIO_CODEC_ID_WESTWOOD_SND1,
            // <summary>as in Berlin toast format</summary>
            AUDIO_CODEC_ID_GSM,
            AUDIO_CODEC_ID_QDM2,
            AUDIO_CODEC_ID_COOK,
            AUDIO_CODEC_ID_TRUESPEECH,
            AUDIO_CODEC_ID_TTA,
            AUDIO_CODEC_ID_SMACKAUDIO,
            AUDIO_CODEC_ID_QCELP,
            AUDIO_CODEC_ID_WAVPACK,
            AUDIO_CODEC_ID_DSICINAUDIO,
            AUDIO_CODEC_ID_IMC,
            AUDIO_CODEC_ID_MUSEPACK7,
            AUDIO_CODEC_ID_MLP,
            AUDIO_CODEC_ID_GSM_MS,
            AUDIO_CODEC_ID_ATRAC3,
            AUDIO_CODEC_ID_APE,
            AUDIO_CODEC_ID_NELLYMOSER,
            AUDIO_CODEC_ID_MUSEPACK8,
            AUDIO_CODEC_ID_SPEEX,
            AUDIO_CODEC_ID_WMAVOICE,
            AUDIO_CODEC_ID_WMAPRO,
            AUDIO_CODEC_ID_WMALOSSLESS,
            AUDIO_CODEC_ID_ATRAC3P,
            AUDIO_CODEC_ID_EAC3,
            AUDIO_CODEC_ID_SIPR,
            AUDIO_CODEC_ID_MP1,
            AUDIO_CODEC_ID_TWINVQ,
            AUDIO_CODEC_ID_TRUEHD,
            AUDIO_CODEC_ID_MP4ALS,
            AUDIO_CODEC_ID_ATRAC1,
            AUDIO_CODEC_ID_BINKAUDIO_RDFT,
            AUDIO_CODEC_ID_BINKAUDIO_DCT,
            AUDIO_CODEC_ID_AAC_LATM,
            AUDIO_CODEC_ID_QDMC,
            AUDIO_CODEC_ID_CELT,
            AUDIO_CODEC_ID_G723_1,
            AUDIO_CODEC_ID_G729,
            AUDIO_CODEC_ID_8SVX_EXP,
            AUDIO_CODEC_ID_8SVX_FIB,
            AUDIO_CODEC_ID_BMV_AUDIO,
            AUDIO_CODEC_ID_RALF,
            AUDIO_CODEC_ID_IAC,
            AUDIO_CODEC_ID_ILBC,
            AUDIO_CODEC_ID_OPUS,
            AUDIO_CODEC_ID_COMFORT_NOISE,
            AUDIO_CODEC_ID_TAK,
            AUDIO_CODEC_ID_METASOUND,
            AUDIO_CODEC_ID_PAF_AUDIO,
            AUDIO_CODEC_ID_ON2AVC,
            AUDIO_CODEC_ID_DSS_SP,
            AUDIO_CODEC_ID_CODEC2,
            AUDIO_CODEC_ID_FFWAVESYNTH,
            AUDIO_CODEC_ID_SONIC,
            AUDIO_CODEC_ID_SONIC_LS,
            AUDIO_CODEC_ID_EVRC,
            AUDIO_CODEC_ID_SMV,
            AUDIO_CODEC_ID_DSD_LSBF,
            AUDIO_CODEC_ID_DSD_MSBF,
            AUDIO_CODEC_ID_DSD_LSBF_PLANAR,
            AUDIO_CODEC_ID_DSD_MSBF_PLANAR,
            AUDIO_CODEC_ID_4GV,
            AUDIO_CODEC_ID_INTERPLAY_ACM,
            AUDIO_CODEC_ID_XMA1,
            AUDIO_CODEC_ID_XMA2,
            AUDIO_CODEC_ID_DST,
            AUDIO_CODEC_ID_ATRAC3AL,
            AUDIO_CODEC_ID_ATRAC3PAL,
            AUDIO_CODEC_ID_DOLBY_E,
            AUDIO_CODEC_ID_APTX,
            AUDIO_CODEC_ID_APTX_HD,
            AUDIO_CODEC_ID_SBC,
            AUDIO_CODEC_ID_ATRAC9,
            AUDIO_CODEC_ID_HCOM,
            AUDIO_CODEC_ID_ACELP_KELVIN,
            AUDIO_CODEC_ID_MPEGH_3D_AUDIO,
            AUDIO_CODEC_ID_SIREN,
            AUDIO_CODEC_ID_HCA,
        }

        static private readonly Dictionary<AudioCodecId, string> AudioCodecIdMap = new Dictionary<AudioCodecId, string>()
        {
            { AudioCodecId.AUDIO_CODEC_ID_UNKNOWN,           "UNKNOWN" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S16LE,         "PCM_S16LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S16BE,         "PCM_S16BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_U16LE,         "PCM_U16LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_U16BE,         "PCM_U16BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S8,            "PCM_S8" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_U8,            "PCM_U8" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_MULAW,         "PCM_MULAW" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_ALAW,          "PCM_ALAW" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S32LE,         "PCM_S32LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S32BE,         "PCM_S32BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_U32LE,         "PCM_U32LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_U32BE,         "PCM_U32BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S24LE,         "PCM_S24LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S24BE,         "PCM_S24BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_U24LE,         "PCM_U24LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_U24BE,         "PCM_U24BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S24DAUD,       "PCM_S24DAUD" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_ZORK,          "PCM_ZORK" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S16LE_PLANAR,  "PCM_S16LE_PLANAR" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_DVD,           "PCM_DVD" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_F32BE,         "PCM_F32BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_F32LE,         "PCM_F32LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_F64BE,         "PCM_F64BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_F64LE,         "PCM_F64LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_BLURAY,        "PCM_BLURAY" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_LXF,           "PCM_LXF" },
            { AudioCodecId.AUDIO_CODEC_ID_S302M,             "S302M" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S8_PLANAR,     "PCM_S8_PLANAR" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S24LE_PLANAR,  "PCM_S24LE_PLANAR" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S32LE_PLANAR,  "PCM_S32LE_PLANAR" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S16BE_PLANAR,  "PCM_S16BE_PLANAR" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S64LE,         "PCM_S64LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_S64BE,         "PCM_S64BE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_F16LE,         "PCM_F16LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_F24LE,         "PCM_F24LE" },
            { AudioCodecId.AUDIO_CODEC_ID_PCM_VIDC,          "PCM_VIDC" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_QT,      "ADPCM_IMA_QT" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_WAV,     "ADPCM_IMA_WAV" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_DK3,     "ADPCM_IMA_DK3" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_DK4,     "ADPCM_IMA_DK4" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_WS,      "ADPCM_IMA_WS" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_SMJPEG,  "ADPCM_IMA_SMJPEG" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_MS,          "ADPCM_MS" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_4XM,         "ADPCM_4XM" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_XA,          "ADPCM_XA" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_ADX,         "ADPCM_ADX" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_EA,          "ADPCM_EA" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_G726,        "ADPCM_G726" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_CT,          "ADPCM_CT" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_SWF,         "ADPCM_SWF" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_YAMAHA,      "ADPCM_YAMAHA" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_SBPRO_4,     "ADPCM_SBPRO_4" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_SBPRO_3,     "ADPCM_SBPRO_3" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_SBPRO_2,     "ADPCM_SBPRO_2" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_THP,         "ADPCM_THP" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_AMV,     "ADPCM_IMA_AMV" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_EA_R1,       "ADPCM_EA_R1" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_EA_R3,       "ADPCM_EA_R3" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_EA_R2,       "ADPCM_EA_R2" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_EA_SEAD, "ADPCM_IMA_EA_SEAD" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_EA_EACS, "ADPCM_IMA_EA_EACS" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_EA_XAS,      "ADPCM_EA_XAS" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_EA_MAXIS_XA, "ADPCM_EA_MAXIS_XA" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_ISS,     "ADPCM_IMA_ISS" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_G722,        "ADPCM_G722" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_APC,     "ADPCM_IMA_APC" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_VIMA,        "ADPCM_VIMA" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_AFC,         "ADPCM_AFC" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_OKI,     "ADPCM_IMA_OKI" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_DTK,         "ADPCM_DTK" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_RAD,     "ADPCM_IMA_RAD" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_G726LE,      "ADPCM_G726LE" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_THP_LE,      "ADPCM_THP_LE" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_PSX,         "ADPCM_PSX" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_AICA,        "ADPCM_AICA" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_DAT4,    "ADPCM_IMA_DAT4" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_MTAF,        "ADPCM_MTAF" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_AGM,         "ADPCM_AGM" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_ARGO,        "ADPCM_ARGO" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_SSI,     "ADPCM_IMA_SSI" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_ZORK,        "ADPCM_ZORK" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_APM,     "ADPCM_IMA_APM" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_ALP,     "ADPCM_IMA_ALP" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_MTF,     "ADPCM_IMA_MTF" },
            { AudioCodecId.AUDIO_CODEC_ID_ADPCM_IMA_CUNNING, "ADPCM_IMA_CUNNING" },
            { AudioCodecId.AUDIO_CODEC_ID_AMR_NB,            "AMR_NB" },
            { AudioCodecId.AUDIO_CODEC_ID_AMR_WB,            "AMR_WB" },
            { AudioCodecId.AUDIO_CODEC_ID_RA_144,            "RA_144" },
            { AudioCodecId.AUDIO_CODEC_ID_RA_288,            "RA_288" },
            { AudioCodecId.AUDIO_CODEC_ID_ROQ_DPCM,          "ROQ_DPCM" },
            { AudioCodecId.AUDIO_CODEC_ID_INTERPLAY_DPCM,    "INTERPLAY_DPCM" },
            { AudioCodecId.AUDIO_CODEC_ID_XAN_DPCM,          "XAN_DPCM" },
            { AudioCodecId.AUDIO_CODEC_ID_SOL_DPCM,          "SOL_DPCM" },
            { AudioCodecId.AUDIO_CODEC_ID_SDX2_DPCM,         "SDX2_DPCM" },
            { AudioCodecId.AUDIO_CODEC_ID_GREMLIN_DPCM,      "GREMLIN_DPCM" },
            { AudioCodecId.AUDIO_CODEC_ID_DERF_DPCM,         "DERF_DPCM" },
            { AudioCodecId.AUDIO_CODEC_ID_MP2,               "MP2" },
            { AudioCodecId.AUDIO_CODEC_ID_MP3,               "MP3" },
            { AudioCodecId.AUDIO_CODEC_ID_AAC,               "AAC" },
            { AudioCodecId.AUDIO_CODEC_ID_AC3,               "AC3" },
            { AudioCodecId.AUDIO_CODEC_ID_DTS,               "DTS" },
            { AudioCodecId.AUDIO_CODEC_ID_VORBIS,            "VORBIS" },
            { AudioCodecId.AUDIO_CODEC_ID_DVAUDIO,           "DVAUDIO" },
            { AudioCodecId.AUDIO_CODEC_ID_WMAV1,             "WMAV1" },
            { AudioCodecId.AUDIO_CODEC_ID_WMAV2,             "WMAV2" },
            { AudioCodecId.AUDIO_CODEC_ID_MACE3,             "MACE3" },
            { AudioCodecId.AUDIO_CODEC_ID_MACE6,             "MACE6" },
            { AudioCodecId.AUDIO_CODEC_ID_VMDAUDIO,          "VMDAUDIO" },
            { AudioCodecId.AUDIO_CODEC_ID_FLAC,              "FLAC" },
            { AudioCodecId.AUDIO_CODEC_ID_MP3ADU,            "MP3ADU" },
            { AudioCodecId.AUDIO_CODEC_ID_MP3ON4,            "MP3ON4" },
            { AudioCodecId.AUDIO_CODEC_ID_SHORTEN,           "SHORTEN" },
            { AudioCodecId.AUDIO_CODEC_ID_ALAC,              "ALAC" },
            { AudioCodecId.AUDIO_CODEC_ID_WESTWOOD_SND1,     "WESTWOOD_SND1" },
            { AudioCodecId.AUDIO_CODEC_ID_GSM,               "GSM" },
            { AudioCodecId.AUDIO_CODEC_ID_QDM2,              "QDM2" },
            { AudioCodecId.AUDIO_CODEC_ID_COOK,              "COOK" },
            { AudioCodecId.AUDIO_CODEC_ID_TRUESPEECH,        "TRUESPEECH" },
            { AudioCodecId.AUDIO_CODEC_ID_TTA,               "TTA" },
            { AudioCodecId.AUDIO_CODEC_ID_SMACKAUDIO,        "SMACKAUDIO" },
            { AudioCodecId.AUDIO_CODEC_ID_QCELP,             "QCELP" },
            { AudioCodecId.AUDIO_CODEC_ID_WAVPACK,           "WAVPACK" },
            { AudioCodecId.AUDIO_CODEC_ID_DSICINAUDIO,       "DSICINAUDIO" },
            { AudioCodecId.AUDIO_CODEC_ID_IMC,               "IMC" },
            { AudioCodecId.AUDIO_CODEC_ID_MUSEPACK7,         "MUSEPACK7" },
            { AudioCodecId.AUDIO_CODEC_ID_MLP,               "MLP" },
            { AudioCodecId.AUDIO_CODEC_ID_GSM_MS,            "GSM_MS" },
            { AudioCodecId.AUDIO_CODEC_ID_ATRAC3,            "ATRAC3" },
            { AudioCodecId.AUDIO_CODEC_ID_APE,               "APE" },
            { AudioCodecId.AUDIO_CODEC_ID_NELLYMOSER,        "NELLYMOSER" },
            { AudioCodecId.AUDIO_CODEC_ID_MUSEPACK8,         "MUSEPACK8" },
            { AudioCodecId.AUDIO_CODEC_ID_SPEEX,             "SPEEX" },
            { AudioCodecId.AUDIO_CODEC_ID_WMAVOICE,          "WMAVOICE" },
            { AudioCodecId.AUDIO_CODEC_ID_WMAPRO,            "WMAPRO" },
            { AudioCodecId.AUDIO_CODEC_ID_WMALOSSLESS,       "WMALOSSLESS" },
            { AudioCodecId.AUDIO_CODEC_ID_ATRAC3P,           "ATRAC3P" },
            { AudioCodecId.AUDIO_CODEC_ID_EAC3,              "EAC3" },
            { AudioCodecId.AUDIO_CODEC_ID_SIPR,              "SIPR" },
            { AudioCodecId.AUDIO_CODEC_ID_MP1,               "MP1" },
            { AudioCodecId.AUDIO_CODEC_ID_TWINVQ,            "TWINVQ" },
            { AudioCodecId.AUDIO_CODEC_ID_TRUEHD,            "TRUEHD" },
            { AudioCodecId.AUDIO_CODEC_ID_MP4ALS,            "MP4ALS" },
            { AudioCodecId.AUDIO_CODEC_ID_ATRAC1,            "ATRAC1" },
            { AudioCodecId.AUDIO_CODEC_ID_BINKAUDIO_RDFT,    "BINKAUDIO_RDFT" },
            { AudioCodecId.AUDIO_CODEC_ID_BINKAUDIO_DCT,     "BINKAUDIO_DCT" },
            { AudioCodecId.AUDIO_CODEC_ID_AAC_LATM,          "AAC_LATM" },
            { AudioCodecId.AUDIO_CODEC_ID_QDMC,              "QDMC" },
            { AudioCodecId.AUDIO_CODEC_ID_CELT,              "CELT" },
            { AudioCodecId.AUDIO_CODEC_ID_G723_1,            "G723_1" },
            { AudioCodecId.AUDIO_CODEC_ID_G729,              "G729" },
            { AudioCodecId.AUDIO_CODEC_ID_8SVX_EXP,          "8SVX_EXP" },
            { AudioCodecId.AUDIO_CODEC_ID_8SVX_FIB,          "8SVX_FIB" },
            { AudioCodecId.AUDIO_CODEC_ID_BMV_AUDIO,         "BMV_AUDIO" },
            { AudioCodecId.AUDIO_CODEC_ID_RALF,              "RALF" },
            { AudioCodecId.AUDIO_CODEC_ID_IAC,               "IAC" },
            { AudioCodecId.AUDIO_CODEC_ID_ILBC,              "ILBC" },
            { AudioCodecId.AUDIO_CODEC_ID_OPUS,              "OPUS" },
            { AudioCodecId.AUDIO_CODEC_ID_COMFORT_NOISE,     "COMFORT_NOISE" },
            { AudioCodecId.AUDIO_CODEC_ID_TAK,               "TAK" },
            { AudioCodecId.AUDIO_CODEC_ID_METASOUND,         "METASOUND" },
            { AudioCodecId.AUDIO_CODEC_ID_PAF_AUDIO,         "PAF_AUDIO" },
            { AudioCodecId.AUDIO_CODEC_ID_ON2AVC,            "ON2AVC" },
            { AudioCodecId.AUDIO_CODEC_ID_DSS_SP,            "DSS_SP" },
            { AudioCodecId.AUDIO_CODEC_ID_CODEC2,            "CODEC2" },
            { AudioCodecId.AUDIO_CODEC_ID_FFWAVESYNTH,       "FFWAVESYNTH" },
            { AudioCodecId.AUDIO_CODEC_ID_SONIC,             "SONIC" },
            { AudioCodecId.AUDIO_CODEC_ID_SONIC_LS,          "SONIC_LS" },
            { AudioCodecId.AUDIO_CODEC_ID_EVRC,              "EVRC" },
            { AudioCodecId.AUDIO_CODEC_ID_SMV,               "SMV" },
            { AudioCodecId.AUDIO_CODEC_ID_DSD_LSBF,          "DSD_LSBF" },
            { AudioCodecId.AUDIO_CODEC_ID_DSD_MSBF,          "DSD_MSBF" },
            { AudioCodecId.AUDIO_CODEC_ID_DSD_LSBF_PLANAR,   "DSD_LSBF_PLANAR" },
            { AudioCodecId.AUDIO_CODEC_ID_DSD_MSBF_PLANAR,   "DSD_MSBF_PLANAR" },
            { AudioCodecId.AUDIO_CODEC_ID_4GV,               "4GV" },
            { AudioCodecId.AUDIO_CODEC_ID_INTERPLAY_ACM,     "INTERPLAY_ACM" },
            { AudioCodecId.AUDIO_CODEC_ID_XMA1,              "XMA1" },
            { AudioCodecId.AUDIO_CODEC_ID_XMA2,              "XMA2" },
            { AudioCodecId.AUDIO_CODEC_ID_DST,               "DST" },
            { AudioCodecId.AUDIO_CODEC_ID_ATRAC3AL,          "ATRAC3AL" },
            { AudioCodecId.AUDIO_CODEC_ID_ATRAC3PAL,         "ATRAC3PAL" },
            { AudioCodecId.AUDIO_CODEC_ID_DOLBY_E,           "DOLBY_E" },
            { AudioCodecId.AUDIO_CODEC_ID_APTX,              "APTX" },
            { AudioCodecId.AUDIO_CODEC_ID_APTX_HD,           "APTX_HD" },
            { AudioCodecId.AUDIO_CODEC_ID_SBC,               "SBC" },
            { AudioCodecId.AUDIO_CODEC_ID_ATRAC9,            "ATRAC9" },
            { AudioCodecId.AUDIO_CODEC_ID_HCOM,              "HCOM" },
            { AudioCodecId.AUDIO_CODEC_ID_ACELP_KELVIN,      "ACELP_KELVIN" },
            { AudioCodecId.AUDIO_CODEC_ID_MPEGH_3D_AUDIO,    "MPEGH_3D_AUDIO" },
            { AudioCodecId.AUDIO_CODEC_ID_SIREN,             "SIREN" },
            { AudioCodecId.AUDIO_CODEC_ID_HCA,               "HCA" },
        };

        static public string AudioCodecIdToAudioCodecStr(AudioCodecId audioCodecId)
        {
            if (AudioCodecIdMap.ContainsKey(audioCodecId))
            {
                return AudioCodecIdMap[audioCodecId];
            }
            return AudioCodecIdMap[AudioCodecId.AUDIO_CODEC_ID_UNKNOWN];
        }

        static public AudioCodecId AudioCodecStrToAudioCodecId(string audioCodecStr)
        {
            foreach (var AudioCodecIt in AudioCodecIdMap)
            {
                if (audioCodecStr.Contains(AudioCodecIt.Value))
                {
                    return AudioCodecIt.Key;
                }
            }
            return AudioCodecId.AUDIO_CODEC_ID_UNKNOWN;
        }

        public AudioCodecId Format { get; set; } = AudioCodecId.AUDIO_CODEC_ID_UNKNOWN;

        /// <summary>Audio Sample Format</summary>
        public enum AudioSampleFormatId
        {
            AUDIO_SAMPLE_FMT_NONE,
            AUDIO_SAMPLE_FMT_U8,
            AUDIO_SAMPLE_FMT_S16,
            AUDIO_SAMPLE_FMT_S32,
            AUDIO_SAMPLE_FMT_FLT,
            AUDIO_SAMPLE_FMT_DBL,
            AUDIO_SAMPLE_FMT_U8P,
            AUDIO_SAMPLE_FMT_S16P,
            AUDIO_SAMPLE_FMT_S32P,
            AUDIO_SAMPLE_FMT_FLTP,
            AUDIO_SAMPLE_FMT_DBLP,
            AUDIO_SAMPLE_FMT_S64,
            AUDIO_SAMPLE_FMT_S64P,
            AUDIO_SAMPLE_FMT_NB,
        }

        static private readonly Dictionary<AudioSampleFormatId, string> AudioSampleFormatIdMap = new Dictionary<AudioSampleFormatId, string>()
        {
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_NONE,    "NONE" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_U8,      "U8" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_S16,     "S16" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_S32,     "S32" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_FLT,     "FLT" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_DBL,     "DBL" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_U8P,     "U8P" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_S16P,    "S16P" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_S32P,    "S32P" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_FLTP,    "FLTP" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_DBLP,    "DBLP" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_S64,     "S64" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_S64P,    "S64P" },
            { AudioSampleFormatId.AUDIO_SAMPLE_FMT_NB,      "NB" },
        };

        static public string AudioSampleFormatIdToAudioSampleFormatStr(AudioSampleFormatId audioSampleFormatId)
        {
            if (AudioSampleFormatIdMap.ContainsKey(audioSampleFormatId))
            {
                return AudioSampleFormatIdMap[audioSampleFormatId];
            }
            return AudioSampleFormatIdMap[AudioSampleFormatId.AUDIO_SAMPLE_FMT_NONE];
        }

        static public AudioSampleFormatId AudioSampleFormatStrToAudioSampleFormatId(string audioSampleFormatStr)
        {
            foreach (var AudioSampleFormatIt in AudioSampleFormatIdMap)
            {
                if (audioSampleFormatStr.Contains(AudioSampleFormatIt.Value))
                {
                    return AudioSampleFormatIt.Key;
                }
            }
            return AudioSampleFormatId.AUDIO_SAMPLE_FMT_NONE;
        }

        public AudioSampleFormatId SampleFormat { get; set; } = AudioSampleFormatId.AUDIO_SAMPLE_FMT_NONE;

        /// <summary>Sample Rate</summary>
        public int SampleRate { get; set; } = 0;

        /// <summary>Channel</summary>
        public enum ChannelId
        {
            UNKNOWN,
            FL,     // front left
            FR,     // front right
            FC,     // front center
            LFE,    // low frequency
            BL,     // back left
            BR,     // back right
            FLC,    // front left-of-center
            FRC,    // front right-of-center
            BC,     // back center
            SL,     // side left
            SR,     // side right
            TC,     // top center
            TFL,    // top front left
            TFC,    // top front center
            TFR,    // top front right
            TBL,    // top back left
            TBC,    // top back center
            TBR,    // top back right
            DL,     // downmix left
            DR,     // downmix right
            WL,     // wide left
            WR,     // wide right
            SDL,    // surround direct left
            SDR,    // surround direct right
            LFE2,   // low frequency 2
        }

        static private readonly Dictionary<ChannelId, string> ChannelIdMap = new Dictionary<ChannelId, string>()
        {
            { ChannelId.FL,  "FL"    }, // front left
            { ChannelId.FR,  "FR"    }, // front right
            { ChannelId.FC,  "FC"    }, // front center
            { ChannelId.LFE, "LFE"   }, // low frequency
            { ChannelId.BL,  "BL"    }, // back left
            { ChannelId.BR,  "BR"    }, // back right
            { ChannelId.FLC, "FLC"   }, // front left-of-center
            { ChannelId.FRC, "FRC"   }, // front right-of-center
            { ChannelId.BC,  "BC"    }, // back center
            { ChannelId.SL,  "SL"    }, // side left
            { ChannelId.SR,  "SR"    }, // side right
            { ChannelId.TC,  "TC"    }, // top center
            { ChannelId.TFL, "TFL"   }, // top front left
            { ChannelId.TFC, "TFC"   }, // top front center
            { ChannelId.TFR, "TFR"   }, // top front right
            { ChannelId.TBL, "TBL"   }, // top back left
            { ChannelId.TBC, "TBC"   }, // top back center
            { ChannelId.TBR, "TBR"   }, // top back right
            { ChannelId.DL,  "DL"    }, // downmix left
            { ChannelId.DR,  "DR"    }, // downmix right
            { ChannelId.WL,  "WL"    }, // wide left
            { ChannelId.WR,  "WR"    }, // wide right
            { ChannelId.SDL, "SDL"   }, // surround direct left
            { ChannelId.SDR, "SDR"   }, // surround direct right
            { ChannelId.LFE2,"LFE2"  }, // low frequency 2
        };

        static public string ChannelIdToChannelStr(ChannelId channelId)
        {
            if (ChannelIdMap.ContainsKey(channelId))
            {
                return ChannelIdMap[channelId];
            }
            return string.Empty;
        }

        static public string[] ChannelIdToChannelStr(ChannelId[] channelId)
        {
            string[] channels = new string[channelId.Length];
            for (int i = 0; i < channels.Length; i++)
            {
                channels[i] = ChannelIdToChannelStr(channelId[i]);
            }
            return channels;
        }

        static public ChannelId ChannelStrToChannelId(string channelStr)
        {
            foreach (var ChannelIt in ChannelIdMap)
            {
                if (ChannelIt.Value.Contains(channelStr))
                {
                    return ChannelIt.Key;
                }
            }
            return ChannelId.UNKNOWN;
        }

        static public ChannelId[] ChannelStrToChannelId(string[] channelStr)
        {
            ChannelId[] channels = new ChannelId[channelStr.Length];
            for (int i = 0; i < channels.Length; i++)
            {
                channels[i] = ChannelStrToChannelId(channelStr[i]);
            }
            return channels;
        }

        /// <summary>Channel Layout</summary>
        static private readonly Dictionary<string, ChannelId[]> ChannelLayoutMap = new Dictionary<string, ChannelId[]>
        {
            /* FC                                                       */ { "mono",                new ChannelId[] { ChannelId.FC } },
            /* FL                                                       */ { "1 channels (FL)",     new ChannelId[] { ChannelId.FL } },
            /* FR                                                       */ { "1 channels (FR)",     new ChannelId[] { ChannelId.FR } },
            /* LFE                                                      */ { "1 channels (LFE)",    new ChannelId[] { ChannelId.LFE } },
            /* BL                                                       */ { "1 channels (BL)",     new ChannelId[] { ChannelId.BL } },
            /* BR                                                       */ { "1 channels (BR)",     new ChannelId[] { ChannelId.BR } },
            /* FLC                                                      */ { "1 channels (FLC)",    new ChannelId[] { ChannelId.FLC } },
            /* FRC                                                      */ { "1 channels (FRC)",    new ChannelId[] { ChannelId.FRC } },
            /* BC                                                       */ { "1 channels (BC)",     new ChannelId[] { ChannelId.BC } },
            /* SL                                                       */ { "1 channels (SL)",     new ChannelId[] { ChannelId.SL } },
            /* SR                                                       */ { "1 channels (SR)",     new ChannelId[] { ChannelId.SR } },
            /* TC                                                       */ { "1 channels (TC)",     new ChannelId[] { ChannelId.TC } },
            /* TFL                                                      */ { "1 channels (TFL)",    new ChannelId[] { ChannelId.TFL } },
            /* TFC                                                      */ { "1 channels (TFC)",    new ChannelId[] { ChannelId.TFC } },
            /* TFR                                                      */ { "1 channels (TFR)",    new ChannelId[] { ChannelId.TFR } },
            /* TBL                                                      */ { "1 channels (TBL)",    new ChannelId[] { ChannelId.TBL } },
            /* TBC                                                      */ { "1 channels (TBC)",    new ChannelId[] { ChannelId.TBC } },
            /* TBR                                                      */ { "1 channels (TBR)",    new ChannelId[] { ChannelId.TBR } },
            /* DL                                                       */ { "1 channels (DL)",     new ChannelId[] { ChannelId.DL } },
            /* DR                                                       */ { "1 channels (DR)",     new ChannelId[] { ChannelId.DR } },
            /* WL                                                       */ { "1 channels (WL)",     new ChannelId[] { ChannelId.WL } },
            /* WR                                                       */ { "1 channels (WR)",     new ChannelId[] { ChannelId.WR } },
            /* SDL                                                      */ { "1 channels (SDL)",    new ChannelId[] { ChannelId.SDL } },
            /* SDR                                                      */ { "1 channels (SDR)",    new ChannelId[] { ChannelId.SDR } },
            /* LFE2                                                     */ { "1 channels (LFE2)",   new ChannelId[] { ChannelId.LFE2 } },
            /* FL+FR                                                    */ { "stereo",              new ChannelId[] { ChannelId.FL, ChannelId.FR } },                                                                                                                                                                                                               
            /* FL+FR+LFE                                                */ { "2.1",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.LFE } },                                                                                                                                                                                                
            /* FL+FR+FC                                                 */ { "3.0",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC } },                                                                                                                                                                                                 
            /* FL+FR+BC                                                 */ { "3.0(back)",           new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.BC } },                                                                                                                                                                                                 
            /* FL+FR+FC+BC                                              */ { "4.0",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.BC } },                                                                                                                                                                                   
            /* FL+FR+BL+BR                                              */ { "quad",                new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.BL, ChannelId.BR } },                                                                                                                                                                                   
            /* FL+FR+SL+SR                                              */ { "quad(side)",          new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.SL, ChannelId.SR } },                                                                                                                                                                                   
            /* FL+FR+FC+LFE                                             */ { "3.1",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE } },                                                                                                                                                                                  
            /* FL+FR+FC+BL+BR                                           */ { "5.0",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.BL, ChannelId.BR } },                                                                                                                                                                     
            /* FL+FR+FC+SL+SR                                           */ { "5.0(side)",           new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.SL, ChannelId.SR } },                                                                                                                                                                     
            /* FL+FR+FC+LFE+BC                                          */ { "4.1",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.BC } },                                                                                                                                                                    
            /* FL+FR+FC+LFE+BL+BR                                       */ { "5.1",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.BL, ChannelId.BR } },                                                                                                                                                      
            /* FL+FR+FC+LFE+SL+SR                                       */ { "5.1(side)",           new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.SL, ChannelId.SR } },                                                                                                                                                      
            /* FL+FR+FC+BC+SL+SR                                        */ { "6.0",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.BC, ChannelId.SL, ChannelId.SR } },                                                                                                                                                       
            /* FL+FR+FLC+FRC+SL+SR                                      */ { "6.0(front)",          new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FLC, ChannelId.FRC, ChannelId.SL, ChannelId.SR } },                                                                                                                                                     
            /* FL+FR+FC+BL+BR+BC                                        */ { "hexagonal",           new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.BL, ChannelId.BR, ChannelId.BC } },                                                                                                                                                       
            /* FL+FR+FC+LFE+BC+SL+SR                                    */ { "6.1(side)",           new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.BC, ChannelId.SL, ChannelId.SR } },                                                                                                                                        
            /* FL+FR+FC+LFE+BL+BR+BC                                    */ { "6.1",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.BL, ChannelId.BR, ChannelId.BC } },                                                                                                                                        
            /* FL+FR+LFE+FLC+FRC+SL+SR                                  */ { "6.1(front)",          new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.LFE, ChannelId.FLC, ChannelId.FRC, ChannelId.SL, ChannelId.SR } },                                                                                                                                      
            /* FL+FR+FC+BL+BR+SL+SR                                     */ { "7.0",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.BL, ChannelId.BR, ChannelId.SL, ChannelId.SR } },                                                                                                                                         
            /* FL+FR+FC+FLC+FRC+SL+SR                                   */ { "7.0(front)",          new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.FLC, ChannelId.FRC, ChannelId.SL, ChannelId.SR } },                                                                                                                                       
            /* FL+FR+FC+LFE+BL+BR+SL+SR                                 */ { "7.1",                 new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.BL, ChannelId.BR, ChannelId.SL, ChannelId.SR } },                                                                                                                          
            /* FL+FR+FC+LFE+BL+BR+FLC+FRC                               */ { "7.1(wide)",           new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.BL, ChannelId.BR, ChannelId.FLC, ChannelId.FRC } },                                                                                                                        
            /* FL+FR+FC+LFE+FLC+FRC+SL+SR                               */ { "7.1(wide-side)",      new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.LFE, ChannelId.FLC, ChannelId.FRC, ChannelId.SL, ChannelId.SR } },                                                                                                                        
            /* FL+FR+FC+BL+BR+BC+SL+SR                                  */ { "octagonal",           new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.BL, ChannelId.BR, ChannelId.BC, ChannelId.SL, ChannelId.SR } },                                                                                                                           
            /* FL+FR+FC+BL+BR+BC+SL+SR+WL+WR+TBL+TBR+TBC+TFC+TFL+TFR    */ { "hexadecagonal",       new ChannelId[] { ChannelId.FL, ChannelId.FR, ChannelId.FC, ChannelId.BL, ChannelId.BR, ChannelId.BC, ChannelId.SL, ChannelId.SR, ChannelId.WL, ChannelId.WR, ChannelId.TBL, ChannelId.TBR, ChannelId.TBC, ChannelId.TFC, ChannelId.TFL, ChannelId.TFR } },     
            /* DL+DR                                                    */ { "downmix",             new ChannelId[] { ChannelId.DL, ChannelId.DR } },
        };

        static public string ChannelLayoutToChannelLayoutStr(ChannelId[] channelLayout)
        {
            foreach (var ChannelLayoutIt in ChannelLayoutMap)
            {
                if (ChannelLayoutIt.Value.SequenceEqual(channelLayout))
                {
                    return ChannelLayoutIt.Key;
                }
            }
            return string.Empty;
        }

        static public ChannelId[] ChannelLayoutStrToChannelLayout(string channelLayoutStr)
        {
            if (ChannelLayoutMap.ContainsKey(channelLayoutStr))
            {
                return ChannelLayoutMap[channelLayoutStr];
            }

            return null;
        }

        public ChannelId[] ChannelLayout { get; set; } = null;

        /// <summary>Bit Rate</summary>
        public int BitRateKbPerSec { get; set; } = 0;

        public TimeSpan Duration { get; set; } = TimeSpan.MinValue;


        public bool Parse(string meta)
        {
            Regex FileNameRegex = new Regex(@"Input\s*#[0-9]*,\s[^,]*, from '(.*)':");
            Match matchFileName = FileNameRegex.Match(meta);
            if (matchFileName.Success)
            {
                FileName = matchFileName.Groups[1].Value;
            }

            Regex DurationRegex = new Regex(@"Duration: ([^,]*), ");
            Match matchDuration = DurationRegex.Match(meta);
            if (matchDuration.Success)
            {
                TimeSpan outDuration = new TimeSpan();
                TimeSpan.TryParse(matchDuration.Groups[1].Value, out outDuration);
                Duration = outDuration;
            }

            Regex MetaAudioRegex = new Regex(@"(Stream\s*#[0-9]*:[0-9]*\(?[^\)]*?\)?: Audio:.*)");
            Match matchMetaAudio = MetaAudioRegex.Match(meta);
            if (matchMetaAudio.Success)
            {

                Regex BitRateRegex = new Regex(@"([0-9]*)\s*kb/s");
                Regex SampleRateRegex = new Regex(@"([0-9]*)\s*Hz");
                Regex AudioFormatHzChannelRegex = new Regex(@"Audio:\s*([^,]*),\s([^,]*),\s([^,]*),\s([^,]*)");

                string fullMetadata = matchMetaAudio.Groups[1].ToString();
                GroupCollection matchAudioFormatHzChannel = AudioFormatHzChannelRegex.Match(fullMetadata).Groups;
                GroupCollection matchAudioBitRate = BitRateRegex.Match(fullMetadata).Groups;
                GroupCollection matchAudioSampleRate = SampleRateRegex.Match(fullMetadata).Groups;

                Format = AudioCodecStrToAudioCodecId(matchAudioFormatHzChannel[1].ToString().ToUpper());
                ChannelLayout = ChannelLayoutStrToChannelLayout(matchAudioFormatHzChannel[3].ToString());
                SampleFormat = AudioSampleFormatStrToAudioSampleFormatId(matchAudioFormatHzChannel[4].ToString().ToUpper());
                SampleRate = !(string.IsNullOrWhiteSpace(matchAudioSampleRate[1].ToString())) ? Convert.ToInt32(matchAudioSampleRate[1].ToString()) : 0;
                BitRateKbPerSec = !(string.IsNullOrWhiteSpace(matchAudioBitRate[1].ToString())) ? Convert.ToInt32(matchAudioBitRate[1].ToString()) : 0;
            }
            return matchDuration.Success || matchMetaAudio.Success;
        }
    }

}
