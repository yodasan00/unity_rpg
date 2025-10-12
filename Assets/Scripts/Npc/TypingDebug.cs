using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class TypingDebug : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]
    private TMP_InputField codeInputField;
    [SerializeField]
    private TMP_Text consoleOutput;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Button runButton;
    [SerializeField]
    private Button nextButton;              

    [SerializeField]
    private GameObject CmdScreen;
    [SerializeField]
    private GameObject ScorePanel;
    [SerializeField]
    private TMP_Text FinalscoreText;

    [Header("Code Snippets")]
    [TextArea(3, 10)]
    [SerializeField]
    private string[] buggySnippets;
    [TextArea(3, 10)]
    [SerializeField]
    private string[] correctSnippets;
    [TextArea(3, 10)]
    [SerializeField]
    private string[] expectedOutputs;        

    private int currentIndex = 0;
    private int score = 0;

    // void Start()
    // {
    //     if (runButton != null)
    //         runButton.onClick.AddListener(CheckCode);

    //     if (nextButton != null)
    //         nextButton.onClick.AddListener(LoadNextSnippet);

    //     LoadSnippet();
    //     UpdateScore();
    // }
    public void Initialize()
    {
        currentIndex = 0;
        score = 0;
        CmdScreen.SetActive(false);
        ScorePanel.SetActive(false);
        runButton.interactable = true;
        nextButton.interactable = true;
        LoadSnippet();
        UpdateScore();
    }

    void LoadSnippet()
    {
        if (currentIndex < buggySnippets.Length)
        {
            codeInputField.text = buggySnippets[currentIndex];
        }
        else
        {
            CmdScreen.SetActive(true);
            consoleOutput.text = " All questions done!";
            runButton.interactable = false;
            nextButton.interactable = false;
            ScorePanel.SetActive(true);
            FinalscoreText.text = "Score: " + score;
        }
    }

    public void CheckCode()
    {
        string playerCode = codeInputField.text.Trim();
        string correctCode = correctSnippets[currentIndex].Trim();

        if (playerCode == correctCode)
        {
            CmdScreen.SetActive(true);
            consoleOutput.text = "CMD>C:TurboC++/bin/output: " + expectedOutputs[currentIndex];
            score++;
            UpdateScore();
        }
        else
        {
            CmdScreen.SetActive(true);
            consoleOutput.text = "CMD>C:TurboC++/bin/output:Syntax Error";
        }
    }

    public void LoadNextSnippet()
    {
        currentIndex++;
        CmdScreen.SetActive(false);
        LoadSnippet();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
