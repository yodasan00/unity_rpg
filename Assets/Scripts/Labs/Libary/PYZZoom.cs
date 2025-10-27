using UnityEngine;
using UnityEngine.EventSystems;

public class PYZZoom : MonoBehaviour, IDragHandler, IScrollHandler
{
    [SerializeField]
    private PlayerController Player;

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.1f;
    public float minZoom = 0.5f;
    public float maxZoom = 3f;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject departmentPanel;
    public GameObject yearPanel;
    public GameObject subjectPanel;
    public GameObject pdfViewerPanel;


    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag delta: " + eventData.delta);
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("Scroll delta: " + eventData.scrollDelta);
        float scale = rectTransform.localScale.x;
        scale += eventData.scrollDelta.y * zoomSpeed;
        scale = Mathf.Clamp(scale, minZoom, maxZoom);
        rectTransform.localScale = new Vector3(scale, scale, 1);
    }

    public void ShowDepartmentPanel()
    {
        departmentPanel.SetActive(true);
        yearPanel.SetActive(false);
        subjectPanel.SetActive(false);
        pdfViewerPanel.SetActive(false);
    }

    public void ShowYearPanel()
    {
        departmentPanel.SetActive(false);
        yearPanel.SetActive(true);
        subjectPanel.SetActive(false);
        pdfViewerPanel.SetActive(false);
    }

    public void ShowSubjectPanel()
    {
        departmentPanel.SetActive(false);
        yearPanel.SetActive(false);
        subjectPanel.SetActive(true);
        pdfViewerPanel.SetActive(false);
    }

    public void ShowPDFPanel()
    {
        departmentPanel.SetActive(false);
        yearPanel.SetActive(false);
        subjectPanel.SetActive(false);
        pdfViewerPanel.SetActive(true);
    }
    public void Back()
    {

        if (subjectPanel.activeSelf)
        {
            ShowYearPanel();
        }
        else if (yearPanel.activeSelf)
        {
            ShowDepartmentPanel();
        }
    }

    public void exit()
    {
        departmentPanel.SetActive(true);
        yearPanel.SetActive(false);
        subjectPanel.SetActive(false);
        pdfViewerPanel.SetActive(false);
        mainPanel.SetActive(false);
        Player.enabled = true;
    }

}

//for navigation between panels in pyq library UI




