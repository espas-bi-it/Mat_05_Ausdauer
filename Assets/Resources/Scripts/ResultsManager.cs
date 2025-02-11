using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using System;

public class ResultsManager : MonoBehaviour
{
    public static ResultsManager Instance { get; private set; }
    private List<int> results = new();
    public List<int> Results { get { return results; } set { results = value; } }

    private string nameOfUser = string.Empty;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Save file in appdata/LocalLow/Espas/MAT05_Ausdauer
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="content"></param>
    public void WriteToFile(string fileName, string content)
    {
        // Define the file path
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        //Debug Only
        //GameObject.FindGameObjectWithTag("Path").GetComponent<TMP_Text>().text = $"File has been saved at {filePath}";

        File.WriteAllText(filePath, content);
    }

    /// <summary>
    /// Function called by button event only.
    /// </summary>
    public void SaveName()
    {
        nameOfUser = UIManager.Instance.InputField.text;
    }

    /// <summary>
    /// Create checksum with results. Only take last two digits of each result.
    /// </summary>
    public void SaveResults()
    {
        // String that will saved into the txt file.
        string resultString = string.Empty;

        // Checksum used to check if participant edited the scores
        string checksum = string.Empty;

        for (int i = 0; i < results.Count; i++)
        {
            string numberStr = string.Empty;

            // If result is single digit, add two 0's. If double digit, add one 0. Ensures all result steps are three digits. -> 003, 017, 103 etc
            if (results[i].ToString().Length == 1)
            {
                numberStr += "00";
            }
            else if (results[i].ToString().Length == 2)
            {
                numberStr += "0";
            }

            // Add result step after the 0s
            numberStr += results[i].ToString();

            // Add it to the result string, seperate it by tabs for readability
            resultString += $"{numberStr}\t";

            //Only take the last two digits for the checksum
            int secondDigit = int.Parse(numberStr[1].ToString());
            int thirdDigit = int.Parse(numberStr[2].ToString());

            checksum += secondDigit.ToString();
            checksum += thirdDigit.ToString();
        }

        string checksumToHex = ConvertToHexadecimal(checksum);

        //Add name, add disguised checksum as student-ID and add time of completion on a new line
        resultString += $"\n{nameOfUser}, Student-ID: {checksumToHex}, {DateTime.Now}\n";

        // Write to file
        WriteToFile($"{nameOfUser}.txt", resultString);

        // Copy the graph into the same folder as the result txt file
        FileManager.Instance.CopyFilesToLocalLow();

        //Debug Only
        //ResetState();
    }

    /// <summary>
    /// Converts string of decimals into hexadecimal
    /// </summary>
    /// <param name="decimalNumber"></param>
    /// <returns>Hexadecimal string</returns>
    static string ConvertToHexadecimal(string decimalNumber)
    {
        BigInteger bigInt = BigInteger.Parse(decimalNumber);
        return bigInt.ToString("X");
    }

    #region Debug Only
    //private int speed = 1;
    //public int Speed { get { return speed; } set { speed = value; } }
    //private bool skipped = false;
    //public bool Skipped { get { return skipped; } private set { skipped = value; } }

    /// <summary>
    /// Allow dev to skip the task itself. Generates random results in TaskManager
    /// </summary>
    //public void Skip()
    //{
    //    skipped = true;
    //}
    //public void SetSpeed(int speed)
    //{
    //    this.speed = speed;
    //}

    /// <summary>
    /// Debug only. Reset state to allow retry.
    /// </summary>
    //public void ResetState()
    //{
    //    skipped = false;
    //    speed = 1;

    //    nameOfUser = string.Empty;
    //    results = null;
    //}
    #endregion
}
