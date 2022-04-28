using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Editor
{
    internal class BuildScripts
    {
        [MenuItem("Build/Web")]
        static void BuildWeb()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = new string[] { "Assets/Scenes/TestScene.unity" };
            options.locationPathName = "Builds/Web";
            options.target = BuildTarget.WebGL;
            options.options = BuildOptions.None;

            Debug.Log("Building for web target...");
            BuildReport report = BuildPipeline.BuildPlayer(options);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Build failed");
            }
        }

        [MenuItem("Build/Host server")]
        static void HostServer()
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.FileName = Path.Combine("Scripts", "SimpleWebServer.exe");
                    process.StartInfo.Arguments = new string("Builds/Web 8080");
                    process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();

                    Debug.Log("Server started, access via http://localhost:8080");
                    process.Start();

                    Process.Start("chrome.exe", "http://localhost:8080");
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

    }
}
