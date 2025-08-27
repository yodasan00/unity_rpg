using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ComputerScreen;
     [SerializeField]
    private GameObject MenuScreen;

    [SerializeField]
    private GameObject MCQCanvas;

    [SerializeField]
    private GameObject TypingCanvas;

    [SerializeField]
    private GameObject Mcqmanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerController Player;
    void Start()
    {
        Debug.Log("Menu Manager started");
        MenuScreen.SetActive(true);
        MCQCanvas.SetActive(false);
        TypingCanvas.SetActive(false);
    }

    // Update is called once per frame
    public void CloseComputerScreen()
    {
        ComputerScreen.SetActive(false);
        MCQCanvas.SetActive(false);
        TypingCanvas.SetActive(false);
         MenuScreen.SetActive(true);
        Player.enabled = true;
    }    
    public void OpenMCQ()
    {
        Debug.Log("MCQ Button Clicked");
        // ComputerScreen.SetActive(true);
        GameObject mcq = Mcqmanager;
            MenuScreen.SetActive(false);
            MCQCanvas.SetActive(true);
            mcq.GetComponent<ComputerMCQ>().Intstantiate();
        }
        
    //         Debug.LogError("MCQCanvas not found!");
    //     }
    // }
}


