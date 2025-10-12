using UnityEngine;
using UnityEngine.UI;  
using TMPro;
public class ComputerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ComputerScreen;

    private bool istrigger = false;

    public PlayerController Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Computer Manager started");
        ComputerScreen.SetActive(false);
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

}