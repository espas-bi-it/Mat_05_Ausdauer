using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Function is called by button event. If name isn't filled out, don't load.
    /// </summary>
    public void LoadSceneWithRequirements(int i)
    {
        if (UIManager.Instance.CheckIfNameFilled())
        {
            SceneManager.LoadScene(i);
        }
    }

    /// <summary>
    /// Function is called by button event.
    /// </summary>
    public void LoadSceneWithoutRequirements(int i)
    {
        SceneManager.LoadScene(i);
    }

    /// <summary>
    /// Function is called by button event.
    /// </summary>
    public void SaveFile()
    {
        ResultsManager.Instance.SaveResults();
    }

    /// <summary>
    /// Function is called by button event.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
