using UnityEngine;
using UnityEngine.UI;
using TMPro; // Standard Unity library for text components

public class UIPanelManager : MonoBehaviour
{
    public GameObject rootcanvas;
    [Header("Panel References")]
    [Tooltip("The parent GameObject for the initial department selection.")]
    public GameObject departmentPanel;
    
    [Tooltip("The ROOT container for all subject UI (the panel that holds the Scroll View, Title, and Back button).")]
   public GameObject subjectRootPanel;

   public GameObject BrowswerPanel;

    [Tooltip("The Scroll View's CONTENT GameObject where buttons are spawned.")]
    public GameObject subjectContentContainer;
    
    //[Tooltip("The text component to display the selected department's name.")]
   // public TextMeshProUGUI subjectPanelTitle;

    [Header("Dynamic Generation")]
    [Tooltip("A reference to the Button Prefab used for creating subject buttons.")]
    public Button subjectButtonPrefab;
    [Tooltip("A reference to the site component to load the final URL.")]
    public ExampleLoadingSite ExampleLoadingSite;
     public PlayerController Player;

    
    [Header("Data")]
    [Tooltip("Drag all DepartmentSubjectData Scriptable Objects here.")]
    public DepartmentSubjectData[] departmentData;

    public void Setup()
    {
        // Ensure all panels are correctly set up at start
        ShowDepartmentPanel();
    }

    private void ShowDepartmentPanel()
    {
        rootcanvas.SetActive(true);
        departmentPanel.SetActive(true);
        // Toggle the root subject panel OFF
        if (subjectRootPanel != null)
        {
            subjectRootPanel.SetActive(false);
        }
        BrowswerPanel.SetActive(false);
        ClearSubjects();
    }

    // Public method called by the Civil/Computer buttons
    public void SelectDepartment(string departmentName)
    {
        DepartmentSubjectData selectedDept = null;

        // Find the correct Scriptable Object data based on the button's name
        foreach (var data in departmentData)
        {
            if (data.departmentName.Equals(departmentName, System.StringComparison.OrdinalIgnoreCase))
            {
                selectedDept = data;
                break;
            }
        }

        if (selectedDept != null)
        {
            // Switch UI state
            departmentPanel.SetActive(false);
            // Toggle the root subject panel ON
            if (subjectRootPanel != null)
            {
                subjectRootPanel.SetActive(true);
            }
            
          //  subjectPanelTitle.text = selectedDept.departmentName + " Subjects";
            
            // Spawn the buttons
            PopulateSubjectButtons(selectedDept);
        }
        else
        {
            Debug.LogError($"Department data not found for: {departmentName}");
        }
    }

    private void PopulateSubjectButtons(DepartmentSubjectData deptData)
    {
        // Remove any old buttons before spawning new ones
        ClearSubjects();

        foreach (var subject in deptData.subjects)
        {
            // 1. Instantiate the button prefab, parenting it to the Content container
            Button newButton = Instantiate(subjectButtonPrefab, subjectContentContainer.transform);
            
            // 2. Set the button text
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
                buttonText.text = subject.subjectName;

            // 3. Configure the OnClick event to load the URL
            // C# captures the current 'subject.url' string here to pass it later.
            string urlToLoad = subject.url;
            // newButton.onClick.AddListener(() => sceneSwitcher.LoadBrowserWithURL(urlToLoad));
            newButton.onClick.AddListener(() => ExampleLoadingSite.load(urlToLoad));
        }
    }

    private void ClearSubjects()
    {
        // Clean up dynamically created buttons from the Content container
        if (subjectContentContainer != null)
        {
            foreach (Transform child in subjectContentContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    // Optional: Add a back button function
    public void GoBackToDepartments()
    {
        ShowDepartmentPanel();
    }

    public void ExitAll()
    {
        ShowDepartmentPanel();
        rootcanvas.SetActive(false);
        Player.enabled = true;

    }
}