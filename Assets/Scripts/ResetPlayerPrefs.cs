using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Import SceneManagement

public class ResetPlayerPrefs : MonoBehaviour
{
    // Reference to the button in the UI
    public Button resetButton;

    void Start()
    {
        // Assign the method to the button's onClick event
        resetButton.onClick.AddListener(ResetPrefs);
    }

    // Method to reset PlayerPrefs and reload the scene
    void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs have been reset.");

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
