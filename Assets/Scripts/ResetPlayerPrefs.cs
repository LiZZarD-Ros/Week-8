using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Reference to the button in the UI
    public Button resetButton;

    void Start()
    {
        // Assign the method to the button's onClick event
        resetButton.onClick.AddListener(ResetPrefs);
    }

    // Method to reset PlayerPrefs
    void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs have been reset.");
    }
}
