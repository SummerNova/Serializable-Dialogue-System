using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;



public class BuildProcess : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    
    //Post process to immediatly zip the build after it is created
     public void OnPostprocessBuild(BuildReport report)
    {
        string buildPath = report.summary.outputPath;
        string directoryToZip = Path.GetDirectoryName(buildPath);
        string zipPath = directoryToZip + ".zip";

        if (File.Exists(zipPath))
            File.Delete(zipPath);

        ZipFile.CreateFromDirectory(directoryToZip, zipPath);
        Debug.Log("Zipped build to: " + zipPath);
    }


    
}