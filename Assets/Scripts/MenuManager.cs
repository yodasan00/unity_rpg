using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject ComputerScreen;
    [SerializeField] private GameObject MenuScreen;
    [SerializeField] private GameObject MCQCanvas;
    [SerializeField] private GameObject TypingCanvas;
    [SerializeField] private GameObject Mcqmanager;
    [SerializeField] private GameObject TypingManager;

    public PlayerController Player;

    void Start()
    {
        Debug.Log("Menu Manager started");
        MenuScreen.SetActive(true);
        MCQCanvas.SetActive(false);
        TypingCanvas.SetActive(false);
    }

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
        MenuScreen.SetActive(false);
        MCQCanvas.SetActive(true);

        var mcq = Mcqmanager.GetComponent<ComputerMCQ>();
        if (mcq != null)
        {
            mcq.Initialize(); 
        }
        else
        {
            Debug.LogError("ComputerMCQ script missing on Mcqmanager!");
        }
    }

    public void OpenTyping()
    {
        Debug.Log("Typing Button Clicked");
        MenuScreen.SetActive(false);
        TypingCanvas.SetActive(true);

        var typing = TypingManager.GetComponent<TypingDebug>();
        if (typing != null)
        {
            typing.Initialize(); 
        }
        else
        {
            Debug.LogError("TypingDebug script missing on TypingCanvas!");
        }
    }
}
