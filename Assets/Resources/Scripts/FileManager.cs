using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    public static FileManager Instance { get; private set; }
    public string sourceFileName1 = "Graph.xlsm";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CopyFilesToLocalLow()
    {
        // Get the source file paths from the StreamingAssets folder
        string sourceFilePath1 = Path.Combine(Application.streamingAssetsPath, sourceFileName1);

        // Define the destination paths in persistentDataPath (LocalLow folder)
        string destFilePath1 = Path.Combine(Application.persistentDataPath, sourceFileName1);

        try
        {
            // Check if the source files exist in StreamingAssets
            if (File.Exists(sourceFilePath1))
            {
                // Copy the files to the persistentDataPath (overwrite if file exists)
                File.Copy(sourceFilePath1, destFilePath1, true);
                Debug.Log("Files copied successfully!");
            }
            else
            {
                Debug.LogError("One or more source files do not exist in the StreamingAssets folder.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error copying files: " + e.Message);
        }
    }
}