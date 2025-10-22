using UnityEngine;
using UnityEngine.EventSystems;

public class Dropper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string chemicalName = "NaOH";
    public float dropVolume = 0.5f;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        startPos = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.position = startPos;
    }

    public void DropOnFlask(AcidFlask flask)
    {
        flask.AddBase(dropVolume);
    }
}
