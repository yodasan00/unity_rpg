using UnityEngine;
using UnityEngine.UI;  
using TMPro;
public class PYQTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject Book;

    private bool istrigger = false;

    public PlayerController Player;


    void Start()
    {
        Debug.Log("Pyq started");
        Book.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && istrigger)
        {
            Debug.Log("Space key was pressed.");
            Book.SetActive(true);
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

//same trigger logic as computer manager 
//will make a unifited or base trigger for everthing later