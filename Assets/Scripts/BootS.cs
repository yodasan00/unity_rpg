using UnityEngine;
using UnityEngine.SceneManagement;

public class BootS : MonoBehaviour
{
    private void Start()
    {
        // Load the main menu after managers initialize
        SceneManager.LoadScene("MainMenu");
    }
}
