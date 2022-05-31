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

namespace Assets.Scripts.Editor
{
    public class BuildScripts
    {
        static string[] Scenes = new string[] {
            "Assets/Scenes/TestScene.unity"
        };

        [MenuItem("Build/Web")]
        static void BuildWeb()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = Scenes;
            options.locationPathName = "Builds/Web/Client";
            options.target = BuildTarget.WebGL;
            options.options = BuildOptions.None;

            Console.WriteLine("Building Web target...");
            BuildBinary(options);
        }

        [MenuItem("Build/Windows")]
        static void BuildWindows()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = Scenes;
            options.locationPathName = "Builds/Windows/Client/Client.exe";
            options.target = BuildTarget.StandaloneWindows64;
            //options.options = BuildOptions.;

            Console.WriteLine("Building Windows target...");
            BuildBinary(options);
        }

        [MenuItem("Build/WindowsServer")]
        static void BuildWindowsServer()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = Scenes;
            options.locationPathName = "Builds/Windows/Server/Server.exe";
            options.target = BuildTarget.StandaloneWindows64;
            options.subtarget = (int)StandaloneBuildSubtarget.Server;

            Console.WriteLine("Building Windows Server target...");
            BuildBinary(options);
        }

        [MenuItem("Build/LinuxServer")]
        static void BuildLinuxServer()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = Scenes;
            options.locationPathName = "Builds/Linux/Server/Server";
            options.target = BuildTarget.StandaloneLinux64;
            options.subtarget = (int)StandaloneBuildSubtarget.Server;

            Console.WriteLine("Building Linux Server target...");
            BuildBinary(options);
        }

        [MenuItem("Build/BuildAll")]
        static void BuildAll()
        {
            Console.WriteLine("Building all...");
            BuildWeb();
            BuildWindows();
            BuildWindowsServer();
            BuildLinuxServer();
        }

        static void BuildBinary(BuildPlayerOptions options)
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.WebGL, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.EmbeddedLinux, ScriptingImplementation.IL2CPP);

            BuildReport report = BuildPipeline.BuildPlayer(options);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Console.WriteLine("Build succeeded\n" +
                    $"  Time: {summary.totalTime} seconds\n" +
                    $"  Size: {summary.totalSize} bytes\n" +
                    $"  Path: {summary.outputPath}");
            }

            if (summary.result == BuildResult.Failed)
            {
                Console.WriteLine("Build failed " + summary.result);
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
                    process.StartInfo.FileName = Path.Combine("..", "Tools", "SimpleWebServer.exe");
                    process.StartInfo.Arguments = new string("Builds/Web 8080");
                    process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();

                    Console.WriteLine("Server started, access via http://localhost:8080");
                    process.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [MenuItem("Build/Open Chrome")]
        static void OpenChrome()
        {
            Process.Start("chrome.exe", "http://localhost:8080");
        }
    }
}
