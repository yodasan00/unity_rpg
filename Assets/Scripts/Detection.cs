using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private GameObject exclamationMark; 

    private void Start()
    {
        if (exclamationMark != null)
            exclamationMark.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (exclamationMark != null)
                exclamationMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (exclamationMark != null)
                exclamationMark.SetActive(false);
        }
    }
}
