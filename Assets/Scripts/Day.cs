using UnityEngine;
using System.Collections;

public class Day : MonoBehaviour
{
    private bool istrigger = false;
    public PlayerController Player;
    public FadeUI fadeUI; // Reference to FadeUI script

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && istrigger)
        {
            StartCoroutine(NextDaySequence());
        }
    }

    IEnumerator NextDaySequence()
    {
        Debug.Log("Next Day Triggered");

        if (Player != null)
            Player.enabled = false;

        // 🔆 Run the fade animation
        yield return StartCoroutine(fadeUI.FadeSequence());

        // 🌅 Now change to next day
        ProgressManager.Instance.NextDay();

        if (Player != null)
            Player.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            istrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            istrigger = false;
    }
}
