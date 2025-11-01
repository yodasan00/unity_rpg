// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class WebTrigger :SceneSwitcher
// {
    
//     [SerializeField]

//     private bool istrigger = false;

//     public PlayerController Player;


//     void Start()
//     {
//         Debug.Log("web started");
//     }


//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && istrigger)
//         {
//             Debug.Log("Space key was pressed.");
//             LoadBrowserSceneWithNewURL();
//             Player.enabled = false;
//         }


//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             istrigger = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             istrigger = false;
//         }
//     }
// }

using UnityEngine;

public class WebTrigger : SceneSwitcher
{
    private bool istrigger = false;
    public PlayerController Player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && istrigger)
        {
            Debug.Log("Space key pressed near computer.");

            PlayerPrefs.SetString("LastScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            LoadBrowserSceneWithNewURL();
            Player.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            istrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            istrigger = false;
    }
}
