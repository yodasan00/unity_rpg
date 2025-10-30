using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hudPrefab;

    private void Awake()
    {
        // Instantiate HUD only if not already there
        if (HUDManager.Instance == null)
        {
            Instantiate(hudPrefab);
        }
    }
}
