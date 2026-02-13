using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    // UI References
    [Header("UI References")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    // Dialogue Data
    [Header("Dialogue Data")]
    [SerializeField] private string[] speakerNames;
    [SerializeField, TextArea(3, 10)] private string[] dialogueLines;
    [SerializeField] private Sprite[] portraitImages;
    [SerializeField] private float lettersPerSecond;

    // State variables
    private bool playerIsInTrigger = false;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private int currentLineIndex = 0;
    private Coroutine typeCoroutine;

    private int minLength;

    void Start()
    {

        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }

        minLength = Mathf.Min(speakerNames.Length, dialogueLines.Length, portraitImages.Length);
    }

    void Update()
    {
        if (playerIsInTrigger && Input.GetKeyDown(KeyCode.Space) && !isDialogueActive)
        {
            if (gameObject.tag == "female")
                    MusicManager.Instance.PlayUISound("female");
            else if (gameObject.tag == "male")
                    MusicManager.Instance.PlayUISound("male");
                    
            StartDialogue();
        }
        else if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopCoroutine(typeCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                currentLineIndex++;
                if (currentLineIndex < minLength)
                {
                    if (gameObject.tag == "female")
                        MusicManager.Instance.PlayUISound("female");
                    else if (gameObject.tag == "male")
                        MusicManager.Instance.PlayUISound("male");
                    StartLine();
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInTrigger = false;
            EndDialogue();
        }
    }
    private void StartDialogue()
    {
        isDialogueActive = true;
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(true);
        }
        currentLineIndex = 0;
        StartLine();
    }

    private void StartLine()
    {
        if (currentLineIndex < minLength && speakerText != null && portraitImage != null)
        {
            speakerText.text = speakerNames[currentLineIndex];
            portraitImage.sprite = portraitImages[currentLineIndex];

            if (typeCoroutine != null)
            {
                StopCoroutine(typeCoroutine);
            }

            if (dialogueText != null)
            {
                typeCoroutine = StartCoroutine(TypeDialogue(dialogueLines[currentLineIndex]));
            }
        }
        else
        {
            EndDialogue();
        }
    }

    // The coroutine for the typewriter effect.
    public IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }

    private void EndDialogue()
    {
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }
        isDialogueActive = false;
        isTyping = false;
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
            QuestManager.Instance.CompleteQuest(QuestType.TalkToNPC);
        }
        currentLineIndex = 0;
        

    }
}


//handles dialouge interactions with NPCs