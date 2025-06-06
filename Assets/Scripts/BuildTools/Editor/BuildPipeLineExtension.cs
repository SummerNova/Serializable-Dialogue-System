

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class BuildPipeLineExtension
{
    static BuildPipeLineExtension()
    {
        BuildPlayerWindow.RegisterBuildPlayerHandler(BuildPlayerWithValidation);
    }
    private static void BuildPlayerWithValidation(BuildPlayerOptions options){

        //Check if dictionaries are valid before building and if the developer wants to continue the build or hide this prompt in the future.
        if(!EditorPrefs.GetBool("StopShowingDictionaryValidationWarning") && !DictionaryValidator.ValidateAll()){
            bool continueAfterFail = EditorUtility.DisplayDialog("Validation failed", "Some dictionaries are invalid. Do you want to continue with the build?", "Yes", "No");
            bool stopShowing = EditorUtility.DisplayDialog("Stop Showing This Warning", "Do you want to stop showing this warning in the future?", "Stop showing", "Continue showing");
            if(stopShowing){
                EditorPrefs.SetBool("StopShowingDictionaryValidationWarning", true);
            }
            if(!continueAfterFail){
                Debug.LogWarning("Build cancelled due to dictionary validation failure.");
                return;
            }
        }

        //Checks the desired build, dev or release.
        bool shouldBeDev = EditorUtility.DisplayDialog("Build Game", "Which build type would you like to create?", "Dev Build", "Release Build");
        options.options = shouldBeDev ? BuildOptions.Development : BuildOptions.None;

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);

    }


    

    [MenuItem("Tools/Build/Reset build preferences")]
    public static void ResetBuildPreferences()
    {
        //Can add more logic in the future if needed
        EditorPrefs.DeleteKey("StopShowingDictionaryValidationWarning");
        Debug.Log("Build preferences reset.");
    }
}