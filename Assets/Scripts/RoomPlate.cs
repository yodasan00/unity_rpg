using UnityEngine;
using TMPro;

public class RoomPlate : MonoBehaviour
{
    [SerializeField] private GameObject Plate; 
    [SerializeField]private TMP_Text roomText;
    [SerializeField] private string roomName;

    private void Start()
    {
        if (Plate != null)
            Plate.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Plate != null)
            {
                Plate.SetActive(true);
                roomText.text = roomName;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Plate != null)
                Plate.SetActive(false);
        }
    }
}
