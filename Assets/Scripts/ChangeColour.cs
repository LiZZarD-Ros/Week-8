using UnityEngine;
using UnityEngine.UI;


public class ChangeColour : MonoBehaviour
{
    public Material redMaterial;
    public Material purpleMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material yellowMaterial;
    public Material orangeMaterial;

    private SkinnedMeshRenderer penguinRenderer;

    void Start()
    {
        // Get the SkinnedMeshRenderer component from the GameObject
        penguinRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Functions to change the material and start the game
    public void ChangeToRed()
    {
        penguinRenderer.material = redMaterial;
        GameManager.Instance.StartGameAfterMaterialSelection();  // Notify GameManager to start the game
        Debug.Log("Red material selected. Game starting.");
    }

    public void ChangeToPurple()
    {
        penguinRenderer.material = purpleMaterial;
        GameManager.Instance.StartGameAfterMaterialSelection();
        Debug.Log("Purple material selected. Game starting.");
    }

    public void ChangeToBlue()
    {
        penguinRenderer.material = blueMaterial;
        GameManager.Instance.StartGameAfterMaterialSelection();
        Debug.Log("Blue material selected. Game starting.");
    }

    public void ChangeToGreen()
    {
        penguinRenderer.material = greenMaterial;
        GameManager.Instance.StartGameAfterMaterialSelection();
        Debug.Log("Green material selected. Game starting.");
    }

    public void ChangeToYellow()
    {
        penguinRenderer.material = yellowMaterial;
        GameManager.Instance.StartGameAfterMaterialSelection();
        Debug.Log("Yellow material selected. Game starting.");
    }

    public void ChangeToOrange()
    {
        penguinRenderer.material = orangeMaterial;
        GameManager.Instance.StartGameAfterMaterialSelection();
        Debug.Log("Orange material selected. Game starting.");
    }
}
