using UnityEngine;
using UnityEngine.SceneManagement;

public class WebTrigger :SceneSwitcher
{
    
    [SerializeField]

    private bool istrigger = false;

    public PlayerController Player;


    void Start()
    {
        Debug.Log("web started");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && istrigger)
        {
            Debug.Log("Space key was pressed.");
            LoadBrowserSceneWithNewURL();
            Player.enabled = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            istrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            istrigger = false;
        }
    }
}
