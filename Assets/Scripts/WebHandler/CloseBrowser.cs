using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseBrowser : MonoBehaviour
{
    public GameObject browserUI;
    public void OnCloseButtonClick()
    {
        HUDManager.Instance.Tab.SetActive(true);
        HUDManager.Instance.questButton.SetActive(true);
        QuestManager.Instance.CompleteQuest(QuestType.AttendLecture);
        browserUI.SetActive(false);
        GetComponent<ExampleLoadingSite>().ExitBrowser();
    }
}
