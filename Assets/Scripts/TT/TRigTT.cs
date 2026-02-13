using UnityEngine;
using UnityEngine.SceneManagement;

public class TRigTT : MonoBehaviour
{
    private bool canEnter = false;
    private string currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetString("ReturnScene", currentScene);
            SceneManager.LoadScene("TT");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canEnter = false;
        }
    }
}
