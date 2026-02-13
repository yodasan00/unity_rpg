using UnityEngine;
using VoltstroStudios.UnityWebBrowser;
using VoltstroStudios.UnityWebBrowser.Core;
using System.Collections; // Needed for IEnumerator

public class ExampleLoadingSite : MonoBehaviour
{
    public static ExampleLoadingSite Instance{ get; private set; }
    //You need a reference to UWB's WebBrowserClient, which is an object kept on BaseUwbClientManager
    //All of UWB's higher level components (such as WebBrowserUIBasic or WebBrowserUIFull) inherit from BaseUwbClientManager
    //so we can use that as the data type
    [SerializeField] //SerializeField allows us to set this in the editor
    private BaseUwbClientManager clientManager;
    public GameObject browserUI;
        
    private WebBrowserClient webBrowserClient;
    private string urlToLoad = "about:blank";


    private void Start()
    {
        //You could also use Unity's GetComponent<BaseUwbClientManager>() method if this script exists on the same object.

        //Makes life easier having a local reference to WebBrowserClient
        webBrowserClient = clientManager.browserClient;
      
    }

    public void LoadMySite()
    {
        webBrowserClient.LoadUrl(urlToLoad);
    }

    public void load(string url)
    {
        bool flag = false;
        browserUI.SetActive(true);
        urlToLoad = url;
        print(url);
        if (!flag)
        {
            flag = true;
            print("starting coroutine");
            StartCoroutine(LoadAfterDelay());
        }
        LoadMySite();
    }
    private IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(0.8f); // Wait for 0.1 seconds
        print("waited 1 second");
        print(urlToLoad);
        LoadMySite();
    }

    public void ExitBrowser()
    {
        webBrowserClient.LoadUrl("about:blank");
    }
    
    private void OnDestroy()
    {
        if (clientManager != null)
        {
            clientManager.browserClient.Dispose();
        }
    }


}