using UnityEngine;
using VoltstroStudios.UnityWebBrowser;

public class BrowserZoomController : MonoBehaviour
{
    // Link this to the WebBrowserUIBasic component in the Inspector!
    public WebBrowserUIBasic browserUI;
    
    [Header("Zoom Settings")]
    [Tooltip("The amount to increase or decrease the zoom level by.")]
    public float zoomStep = 0.25f;

    // Use KeyCode.Mouse2 for the middle mouse button/scroll wheel click
    [Tooltip("The key that must be held down to enable scrolling zoom.")]
    public KeyCode holdKey = KeyCode.Mouse2; 

    private float currentZoomLevel = 0.0f;

    private void Start()
    {
        if (browserUI == null)
        {
            Debug.LogError("WebBrowserUIBasic reference is missing!");
            return;
        }
        currentZoomLevel = 0.0f; 
    }

    private void Update()
    {
        // 1. Check if the required key (middle mouse button) is currently being held down
        if (Input.GetKey(holdKey))
        {
            // 2. Get the scroll wheel input
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // 3. Only proceed if there is actual scrolling input
            if (scrollInput != 0f)
            {
                // Scroll Up (Positive value) = Zoom In
                if (scrollInput > 0f)
                {
                    ZoomIn();
                }
                // Scroll Down (Negative value) = Zoom Out
                else if (scrollInput < 0f)
                {
                    ZoomOut();
                }
            }
        }
    }

    // --- Core Zoom Functions ---
    
    public void ZoomIn()
    {
        currentZoomLevel += zoomStep;
        SetBrowserZoom(currentZoomLevel);
    }

    public void ZoomOut()
    {
        // Prevent zooming too far out
        if (currentZoomLevel - zoomStep < -2.0f) 
            return;
            
        currentZoomLevel -= zoomStep;
        SetBrowserZoom(currentZoomLevel);
    }

    public void ResetZoom()
    {
        currentZoomLevel = 0.0f;
        SetBrowserZoom(currentZoomLevel);
    }

    private void SetBrowserZoom(float level)
    {
        if (browserUI != null && browserUI.browserClient != null)
        {
            browserUI.browserClient.SetZoomLevel(level);
        }
    }
}