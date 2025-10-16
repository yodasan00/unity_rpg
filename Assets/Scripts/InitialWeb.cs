using UnityEngine;
using UnityWebBrowser;
using VoltstroStudios.UnityWebBrowser.Core;

public class InitialWeb : MonoBehaviour
{
    public WebBrowserClient url;

    void Start()
    {
        libaryweb();
    }
    public void libaryweb()
    {
        url.initialUrl = "https://www.google.com";
    }
}
