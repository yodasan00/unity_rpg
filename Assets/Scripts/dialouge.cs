using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialouge : MonoBehaviour
{
    //UI refernce
    [SerializeField]
    private GameObject dialougeCanvas;

    [SerializeField]
    private TMP_Text speakerText;

    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private Image potraitImage;

    //Dialogue data
    [SerializeField]
    private string[] speakerName;

    [SerializeField]
    [TextArea(3, 10)]
    private string[] dialogueLines;

    [SerializeField]
    private Sprite[] potraitImages;

    private bool isDialogueActive = false;
    private int currentLineIndex = 0;
    void Start()
    {
        dialougeCanvas.SetActive(false);
    }

   
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialogueActive)
        {
            int minLength = Mathf.Min(speakerName.Length, dialogueLines.Length, potraitImages.Length);
            if (currentLineIndex >= minLength)
            {
                dialougeCanvas.SetActive(false);
                currentLineIndex = 0;
            }
            else
            {
                dialougeCanvas.SetActive(true);
                speakerText.text = speakerName[currentLineIndex];
                dialogueText.text = dialogueLines[currentLineIndex];
                potraitImage.sprite = potraitImages[currentLineIndex];
                currentLineIndex++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isDialogueActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isDialogueActive = false;
       dialougeCanvas.SetActive(false);
    }
}
