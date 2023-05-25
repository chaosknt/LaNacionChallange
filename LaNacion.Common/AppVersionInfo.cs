using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LaNacion.Common
{
    public static class AppVersionInfo
    {
        private const string _buildFileName = "buildinfo.json";
        private static BuildInfo _fileBuildInfo = new BuildInfo(
            BranchName: "",
            BuildNumber: DateTime.UtcNow.ToString("yyyyMMdd") + ".0",
            BuildId: "-1",
            Info: $"Not yet initialised - call {nameof(InitialiseBuildInfoGivenPath)}"
        );

        public static void InitialiseBuildInfoGivenPath(string path)
        {
            var buildFilePath = Path.Combine(path, _buildFileName);
            if (System.IO.File.Exists(buildFilePath))
            {
                try
                {
                    var buildInfoJson = System.IO.File.ReadAllText(buildFilePath);
                    var buildInfo = JsonSerializer.Deserialize<BuildInfo>(buildInfoJson, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    if (buildInfo == null) throw new Exception($"Failed to deserialise {_buildFileName}");

                    _fileBuildInfo = buildInfo;
                }
                catch (Exception)
                {
                    _fileBuildInfo = new BuildInfo(
                        BranchName: "",
                        BuildNumber: DateTime.UtcNow.ToString("yyyyMMdd") + ".0",
                        BuildId: "-1",
                        Info: "Failed to load build info from buildinfo.json"
                    );
                }
            }
        }

        public static BuildInfo GetBuildInfo() => _fileBuildInfo;
    }
}
