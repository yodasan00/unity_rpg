using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSounds : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public bool playClick = true;
    public bool playHover = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (playClick)
            MusicManager.Instance.PlayUISound("click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (playHover)
            MusicManager.Instance.PlayUISound("hover");
    }
}
