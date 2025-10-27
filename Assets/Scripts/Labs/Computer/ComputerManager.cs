using UnityEngine;
using UnityEngine.UI;  
using TMPro;
public class ComputerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ComputerScreen;

    private bool istrigger = false;

    public PlayerController Player;


    void Start()
    {
        Debug.Log("Computer Manager started");
        ComputerScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && istrigger) //press space in the area to start or trigger lab
        {
            Debug.Log("Space key was pressed.");
            ComputerScreen.SetActive(true);
            Player.enabled = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)  //checks if the collider is player or random npc or obj
    {
        if (other.gameObject.CompareTag("Player"))
        {
            istrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) //when player exits the collider
    {
        if (other.gameObject.CompareTag("Player"))
        {
            istrigger = false;
        }
    }

}