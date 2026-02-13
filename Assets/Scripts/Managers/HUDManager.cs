// // using UnityEngine;
// // using TMPro;
// // using UnityEngine.UI;

// // public class HUDManager : MonoBehaviour
// // {
// //     public static HUDManager Instance { get; private set; }

// //     [Header("HUD Elements")]
// //     public TMP_Text starText;
// //     public TMP_Text semesterText;
// //     public TMP_Text timeText;

// //     private void Awake()
// //     {
// //         if (Instance != null && Instance != this)
// //         {
// //             Destroy(gameObject);
// //             return;
// //         }
// //         Instance = this;
// //         DontDestroyOnLoad(gameObject);
// //     }

// //     private void Start()
// //     {
// //         // UpdateStars(0);
// //         // UpdateSemester("1");
// //         // UpdateTime("Day 1");

// //         if (ProgressManager.Instance != null)
// //         {
// //             var data = ProgressManager.Instance.playerProgress;
// //             UpdateStars(data.totalStars);
// //             UpdateTime("Day " + data.currentDay);
// //             UpdateSemester(data.currentSemester.ToString());
// //         }
    
// //     }

// //     public void UpdateStars(int stars)
// //     {
// //         starText.text = $"Stars: {stars} ";
// //     }

// //     public void UpdateSemester(string sem)
// //     {
// //         semesterText.text = $"Semester: {sem} sem ";
// //     }

// //     public void UpdateTime(string time)
// //     {
// //         timeText.text = $"Time: {time} ";
// //     }
// // }


// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;

// public class HUDManager : MonoBehaviour
// {
//     public static HUDManager Instance { get; private set; }

//     [Header("HUD Elements")]
//     public TMP_Text starText;
//     public TMP_Text semesterText;
//     public TMP_Text timeText;

//     public TMP_Text fastForwardText;

//     [Header("Pause Menu")]
//     public GameObject pauseMenuUI;
//     public Button resumeButton;
//     public Button fastForwardButton;
//     public Button quitButton;

//     private bool isPaused = false;
//     private bool isFastForward = false;
//     private PlayerController playerController;

//     private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//         DontDestroyOnLoad(gameObject);
//     }

//     private void Start()
//     {
//         fastForwardText.text = "Speed: 1x";
//         if (ProgressManager.Instance != null)
//         {
//             var data = ProgressManager.Instance.playerProgress;
//             UpdateStars(data.totalStars);
//             UpdateTime("Day " + data.currentDay);
//             UpdateSemester(data.currentSemester.ToString());
//         }

//         pauseMenuUI.SetActive(false);

//         resumeButton.onClick.AddListener(ResumeGame);
//         fastForwardButton.onClick.AddListener(ToggleFastForward);
//         quitButton.onClick.AddListener(QuitGame);

    
//     }

//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             if (isPaused)
//                 ResumeGame();
//             else
//                 PauseGame();
//         }
//     }

//     public void UpdateStars(int stars)
//     {
//         starText.text = $"Stars: {stars}";
//     }

//     public void UpdateSemester(string sem)
//     {
//         semesterText.text = $"Semester: {sem} sem";
//     }

//     public void UpdateTime(string time)
//     {
//         timeText.text = $"Time: {time}";
//     }

//     public void PauseGame()
//     {
//         pauseMenuUI.SetActive(true);
//         // Time.timeScale = 0f;
//         isPaused = true;

//         if (playerController != null)
//             playerController.enabled = false; // stop movement

//         Cursor.visible = true;
//         Cursor.lockState = CursorLockMode.None; // unlock cursor if needed
//     }

//     public void ResumeGame()
//     {
//         pauseMenuUI.SetActive(false);
//         Time.timeScale = isFastForward ? 2.5f : 1f;
//         isPaused = false;

//         if (playerController != null)
//             playerController.enabled = true;

//         // Cursor.visible = false;
//         // Cursor.lockState = CursorLockMode.Locked;
//     }

//     public void ToggleFastForward()
//     {
//         isFastForward = !isFastForward;
//         Time.timeScale = isFastForward ? 2.5f : 1f;

//         string fast = isFastForward ? "Speed: 2x" : "Speed: 1x";
//         fastForwardText.text = fast;
//     }

//     public void QuitGame()
//     {
//         Application.Quit();
//         Debug.Log("Game quit!");
//     }
// }


using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [Header("HUD Elements")]
    public TMP_Text starText;
    public TMP_Text semesterText;
    public TMP_Text timeText;
    public TMP_Text fastForwardText;
    public GameObject Tab;
    

    [Header("Pause Menu")]
    public GameObject pauseMenuUI;
    public Button resumeButton;
    // public Button fastForwardButton;
    public GameObject Settings;

    public Button quitButton;

    [Header("Quest UI")]
    public GameObject questButton;
    public GameObject questPanel;
    public Transform questListParent;      
    public GameObject questItemPrefab;     
    public Sprite checkedSprite;
    public Sprite uncheckedSprite;

    


    private bool isPaused = false;
    private bool isFastForward = false;
    private bool questOpen = false;

    private bool settin = false;

    private PlayerController playerController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        fastForwardText.text = "Speed: 1x";

        if (ProgressManager.Instance != null)
        {
            var data = ProgressManager.Instance.playerProgress;
            UpdateStars(data.totalStars);
            UpdateTime("Day " + data.currentDay);
            UpdateSemester(data.currentSemester.ToString());
        }

        pauseMenuUI.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        // fastForwardButton.onClick.AddListener(ToggleFastForward);
        quitButton.onClick.AddListener(QuitGame);

        // QUEST UI SETUP
        questPanel.SetActive(false);
        questButton.GetComponent<Button>().onClick.AddListener(ToggleQuestPanel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void UpdateStars(int stars)
    {
        starText.text = $"Stars: {stars}";
    }

    public void UpdateSemester(string sem)
    {
        semesterText.text = $"Semester: {sem} sem";
    }

    public void UpdateTime(string time)
    {
        timeText.text = $"Time: {time}";
    }

    // -------------------------------
    // QUEST SYSTEM UI
    // -------------------------------

    public void ToggleQuestPanel()
    {
        questOpen = !questOpen;
        questPanel.SetActive(questOpen);

        if (questOpen)
            RefreshQuestUI();
    }

    public void RefreshQuestUI()
{
    if (QuestManager.Instance == null) return;

    foreach (Transform child in questListParent)
        Destroy(child.gameObject);

    foreach (var quest in QuestManager.Instance.dailyQuests)
    {
        GameObject item = Instantiate(questItemPrefab, questListParent);

        TMP_Text title = item.transform.Find("TitleText").GetComponent<TMP_Text>();
        TMP_Text desc = item.transform.Find("DescriptionText").GetComponent<TMP_Text>();
        Image checkmark = item.transform.Find("Checkmark").GetComponent<Image>();

        title.text = quest.questName;
        desc.text = quest.description;          
        checkmark.sprite = quest.isCompleted ? checkedSprite : uncheckedSprite; 
    }
}


    // -------------------------------
    // PAUSE MENU
    // -------------------------------

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        isPaused = true;

        if (playerController != null)
            playerController.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = isFastForward ? 2.5f : 1f;
        isPaused = false;

        if (playerController != null)
            playerController.enabled = true;
    }

    public void ToggleSettings()
    {
       settin = !settin;
        Settings.SetActive(settin);
    }

    

    public void ToggleFastForward()
    {
        isFastForward = !isFastForward;
        Time.timeScale = isFastForward ? 2.5f : 1f;

        fastForwardText.text = isFastForward ? "Speed: 2x" : "Speed: 1x";
    }

    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
