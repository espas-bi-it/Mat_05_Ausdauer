using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] Button startButton;
    [SerializeField] Button skipButton;

    [SerializeField] TMP_InputField inputField;
    public TMP_InputField InputField { get { return inputField; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Check if name is filled out. If it is, allow user to continue. Otherwise display error text.
    /// </summary>
    /// <returns></returns>
    public bool CheckIfNameFilled()
    {
        if (inputField.text == "")
        {
            ErrorMessage($"Bitte geben Sie Ihren Vor- und Nachnamen an.");
            startButton.interactable = false;
            skipButton.interactable = false;
            return false;
        }
        else
        {
            ErrorMessage($"");
            startButton.interactable = true;
            skipButton.interactable = true;
            return true;
        }
    }

    public void ErrorMessage(string error)
    {
        GameObject.FindGameObjectWithTag("ErrorMessage").GetComponent<TMP_Text>().text = error;
    }
}
