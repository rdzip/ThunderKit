using System.IO;
using UnityEditor;

namespace ThunderKit.Core.Config.Common
{
    public class DisableAssemblyUpdater : OptionalExecutor
    {
        public override int Priority => int.MaxValue;

        public override string Description => "Prompt the user to Restart Unity to disable the ASsembly";

        public override bool Execute()
        {
            var args = System.Environment.GetCommandLineArgs();
            var promptRestart = true;
            for (int i = 0; i < args.Length; i++)
                if (args[i] == "-disable-assembly-updater")
                    promptRestart = false;

            if (promptRestart)
            {
                var restart = EditorUtility.DisplayDialog(
                          title: "Disable Assembly Updater",
                        message: "Disabling the Unity Automatic Assembly Updater is recommended as game assemblies should not be updated. Disabling the updater will reduce import times. Disabling the Assembly Updater requires the project to restart.",
                             ok: "Restart Project",
                         cancel: "No Thanks");

                if (restart)
                    EditorApplication.OpenProject(Directory.GetCurrentDirectory(), "-disable-assembly-updater");
            }
            return true;
        }
    }
}