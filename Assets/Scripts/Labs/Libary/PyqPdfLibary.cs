using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;

public class PyqPdfLibary : MonoBehaviour
{
    [Header("Panels")]
    public GameObject departmentPanel;
    public GameObject yearPanel;
    public GameObject subjectPanel;
    public Transform subjectContentParent; // ScrollView Content

    public GameObject pdfViewerPanel;
    public RawImage pdfDisplay;
    public Button prevButton, nextButton, closeButton;

    [Header("Prefabs")]
    public GameObject subjectButtonPrefab;

    private string selectedDepartment;
    private string selectedYear;
    private string selectedSubject;

    private List<Texture2D> pages = new List<Texture2D>();
    private int currentPageIndex = 0;

    void Start()
    {
        departmentPanel.SetActive(true);
        yearPanel.SetActive(false);
        subjectPanel.SetActive(false);
        pdfViewerPanel.SetActive(false);
    }
   
    public void OnDepartmentSelected(string department)
    {
        selectedDepartment = department;
        yearPanel.SetActive(true);
        departmentPanel.SetActive(false);
    }

    
    public void OnYearSelected(string year)
    {
        selectedYear = year;
        yearPanel.SetActive(false);
        subjectPanel.SetActive(true);
        ShowSubjects();
    }

  
    void ShowSubjects()
    {


       
        foreach (Transform child in subjectContentParent)
            Destroy(child.gameObject);

        string subjectPath = Path.Combine(Application.streamingAssetsPath, "Papers", selectedDepartment, selectedYear);
        if (!Directory.Exists(subjectPath))
        {
            Debug.LogWarning("No subjects found: " + subjectPath);
            return;
        }

        string[] folders = Directory.GetDirectories(subjectPath);
        Debug.Log("Subject folders found: " + folders.Length);
        foreach (string folder in folders)
        {
            Debug.Log("Folder: " + folder);
            string subjectName = Path.GetFileName(folder);
            GameObject btnObj = Instantiate(subjectButtonPrefab, subjectContentParent);
            TMP_Text tmpText = btnObj.GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
                tmpText.text = subjectName;
            else
                Debug.LogWarning("No TMP_Text found in button prefab!");
            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                OpenSubject(folder);
            });
        }
    }

 
    void OpenSubject(string folderPath)
    {
        selectedSubject = folderPath;
        pages.Clear();
        currentPageIndex = 0;

        string[] pageFiles = Directory.GetFiles(folderPath, "*.png");
        System.Array.Sort(pageFiles); // order page

        foreach (string file in pageFiles)
        {
            byte[] bytes = File.ReadAllBytes(file);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            pages.Add(tex);
        }

        if (pages.Count > 0)
        {
            pdfDisplay.texture = pages[0];
            pdfViewerPanel.SetActive(true);

            // Assign buttons
            prevButton.onClick.RemoveAllListeners();
            nextButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();

            prevButton.onClick.AddListener(PrevPage);
            nextButton.onClick.AddListener(NextPage);
            closeButton.onClick.AddListener(ClosePDF);

            UpdateNavButtons();
        }
        else
        {
            Debug.LogWarning("No pages found in " + folderPath);
        }
    }

   
    void UpdateNavButtons()
    {
        prevButton.interactable = currentPageIndex > 0;
        nextButton.interactable = currentPageIndex < pages.Count - 1;
    }

    public void NextPage()
    {
        if (currentPageIndex < pages.Count - 1)
        {
            currentPageIndex++;
            pdfDisplay.texture = pages[currentPageIndex];
            UpdateNavButtons();
        }
    }

    public void PrevPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            pdfDisplay.texture = pages[currentPageIndex];
            UpdateNavButtons();
        }
    }

    public void ClosePDF()
    {
        pdfViewerPanel.SetActive(false);
        pdfDisplay.texture = null;
    }

}
