using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Update()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(
            Mathf.Clamp(mouseWorld.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(mouseWorld.y, minBounds.y, maxBounds.y),
            0f
        );

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
