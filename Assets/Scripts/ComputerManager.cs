using UnityEngine;
using UnityEngine.UI;  
using TMPro;
public class ComputerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ComputerScreen;
    [SerializeField]
    private Button McqButton;

    [SerializeField]
    private Button CodingButton;

    [SerializeField]
    private GameObject MCQCanvas;

    [SerializeField]
    private Button closeButton;

    // [SerializeField]
    // private GameObject CodingCanvas;

    private bool istrigger = false;

    public PlayerController Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Computer Manager started");
        ComputerScreen.SetActive(false);
        MCQCanvas.SetActive(false);
        options();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && istrigger)
        {
            Debug.Log("Space key was pressed.");
            ComputerScreen.SetActive(true);
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

    void CloseComputerScreen()
    {
        ComputerScreen.SetActive(false);
        MCQCanvas.SetActive(false);
        Player.enabled = true;
    }

    void options()
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseComputerScreen);

        McqButton.onClick.RemoveAllListeners();
        McqButton.onClick.AddListener(OpenMCQ);

        // CodingButton.onClick.RemoveAllListeners();
        // CodingButton.onClick.AddListener(OpenCoding);

    }
    
    void OpenMCQ()
    {
        Debug.Log("MCQ Button Clicked");
        ComputerScreen.SetActive(false);
        GameObject mcq = MCQCanvas;
        if (mcq != null)
        {
            mcq.SetActive(true);
            mcq.GetComponent<ComputerMCQ>().intstantiate();
        }
        else
        {
            Debug.LogError("MCQCanvas not found!");
        }
    }
}
