project "MediaAnalyzer"
    location "./"
    kind "SharedLib"
    language "C#"
    dotnetframework "4.6"
    files {
        "**.cs",
        "**.xaml",
        "**.config",
        "**.manifest",
        "**.ico",
        "**.png",
        "**.resx",
        "ffmpeg/bin/**.exe"
    }
    excludes{
        "bin/**",
        "obj/**",
        "Tests/**"
    }
    links ("PresentationFramework")
    links ("System")
    links ("System.ComponentModel.Composition")
    links ("System.Configuration")
    links ("System.Data")
    links ("System.Drawing")
    links ("System.IO.Compression")
    links ("System.IO.Compression.FileSystem")
    links ("System.Numerics")
    links ("System.Runtime.Serialization")
    links ("System.ServiceModel")
    links ("System.Transactions")
    links ("System.Windows.Forms")
    links ("System.Xml")
    links ("Microsoft.CSharp")
    links ("System.Core")
    links ("System.Xml.Linq")
    links ("System.Data.DataSetExtensions")
    links ("System.Net.Http")
    links ("System.Xaml")
    links ("WindowsBase")
    links ("PresentationCore")

    filter { "configurations:Debug" }
        defines { "DEBUG", "TRACE" }

    filter { "configurations:Release" }
        defines { "NDEBUG" }
        optimize "On"

    filter {"files:**.resx" }
        buildaction "Embed"
    filter {"files:**.ico" }
        buildaction "Resource"
    filter {"files:**.png" }
        buildaction "Resource"
    filter {"files:ffmpeg/bin/**.exe" }
        buildaction "Embed"
