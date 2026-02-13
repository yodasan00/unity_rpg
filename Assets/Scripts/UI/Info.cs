

using UnityEngine;

public class Info : MonoBehaviour
{
    private bool istrigger = false;
    public GameObject infoUI;

    private bool toogle = false;

   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && istrigger)
        {
            toogle = !toogle;
            Debug.Log("Space key pressed near info.");
            infoUI.SetActive(toogle);

        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            istrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            istrigger = false;
            infoUI.SetActive(false);
    }
    }
}
