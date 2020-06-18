local config_list = {
    "Release",
    "Debug",
}
local platform_list = {
    "x64",
    "Any CPU"
}
local mask = "Plugins/**.lua"
local plugins = os.matchfiles(mask)

workspace "MediaAnalyzer"
    startproject("MediaAnalyzer-Cmd")
    platforms(platform_list)
    configurations(config_list)
    dofile("MediaAnalyzer-Cmd/MediaAnalyzer-Cmd.lua")
    dofile("MediaAnalyzer/MediaAnalyzer.lua")
