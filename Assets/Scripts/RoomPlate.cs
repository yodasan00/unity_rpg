// using UnityEngine;
// using TMPro;

// public class RoomPlate : MonoBehaviour
// {
//     [SerializeField] private GameObject Plate; 
//     [SerializeField]private TMP_Text roomText;
//     [SerializeField] private string roomName;

//     private void Start()
//     {
//        // if (Plate != null)
//             SetVisi
//             Plate.SetActive(false);
//             roomText.text = "";
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             if (Plate != null)
//             {
//                 Plate.SetActive(true);
//                 roomText.text = roomName;
//             }
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             if (Plate != null)
//                 Plate.SetActive(false);
//         }
//     }
// }


using UnityEngine;
using TMPro;

public class RoomPlate : MonoBehaviour
{
    [SerializeField] private TextMeshPro roomText;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private string roomName = "";

    private void Start()
    {
        SetVisible(false);
        if (roomText != null)
            roomText.text = roomName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SetVisible(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SetVisible(false);
    }

    private void SetVisible(bool visible)
    {
        if (roomText != null)
            roomText.enabled = visible;

        if (background != null)
            background.enabled = visible;
    }
}
