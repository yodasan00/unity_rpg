
using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    public float initialSpeed = 10f;
    public float speedIncreaseFactor = 1.05f;

    [Header("Spawn Points")]
    public Transform playerSpawn;
    public Transform opponentSpawn;

    [Header("Midline Trigger")]
    public Collider2D midlineTrigger; 

    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private GameManage gameManager;
    private string lastHitBy = ""; // "Player" or "AI"
    private bool isInPlay = false;
    private bool hasCrossedMidline = false;
    private string crossedFrom = "";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManage>(); // Unity 6 style
    }

    void Start()
    {
        bool aiServe = Random.Range(0, 2) == 0;
        ResetBall(aiServe ? opponentSpawn.position : playerSpawn.position, aiServe);
    }

    void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity; // Unity 6 uses linearVelocity for Rigidbody2D
    }

private void ResetBall(Vector3 position, bool aiServe)
{
    StopAllCoroutines();
    rb.linearVelocity = Vector2.zero;
    transform.position = position;
    isInPlay = false;
    hasCrossedMidline = false;
    crossedFrom = ""; // Reset on every serve
    initialSpeed = 7f;
    lastHitBy = aiServe ? "AI" : "Player";

    if (aiServe)
        StartCoroutine(AutoServeFromAI());
}

   // Also tweak AI serve to add randomness:
private IEnumerator AutoServeFromAI()
{
    yield return new WaitForSeconds(1f);
    if (!isInPlay)
    {
        isInPlay = true;
        Vector2 dir = Vector2.down;
        dir.x = Random.Range(-0.3f, 0.3f); // small X offset
        rb.linearVelocity = dir.normalized * initialSpeed;
        lastHitBy = "AI";
    }
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerPaddle"))
        {
            lastHitBy = "Player";
            MusicManager.Instance.PlayUISound("ball");
            if (!isInPlay)
            {
                isInPlay = true;
                rb.linearVelocity = Vector2.up * initialSpeed;
                return;
            }
            BounceBall(collision);
        }
        else if (collision.collider.CompareTag("AIPaddle"))
        {
            lastHitBy = "AI";
            MusicManager.Instance.PlayUISound("ball");
            if (!isInPlay)
            {
                isInPlay = true;
                rb.linearVelocity = Vector2.down * initialSpeed;
                return;
            }
            BounceBall(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other == midlineTrigger)
    {
        hasCrossedMidline = true;
        crossedFrom = lastHitBy; // Who last hit to cross midline
        return;
    }

    if (other.CompareTag("LeftZone") || other.CompareTag("RightZone"))
    {
        
        bool validCross = hasCrossedMidline && crossedFrom == lastHitBy;

        if (lastHitBy == "Player")
        {
            if (validCross)
                gameManager.PlayerScored();
            else
                gameManager.OpponentScored();

            ResetBall(playerSpawn.position, false);
        }
        else if (lastHitBy == "AI")
        {
            if (validCross)
                gameManager.OpponentScored();
            else
                gameManager.PlayerScored();

            ResetBall(opponentSpawn.position, true);
        }
        else
        {
            bool aiServe = Random.Range(0, 2) == 0;
            ResetBall(aiServe ? opponentSpawn.position : playerSpawn.position, aiServe);
        }
    }
    else if (other.CompareTag("BottomWall"))
    {
        gameManager.OpponentScored();
        ResetBall(opponentSpawn.position, true);
    }
    else if (other.CompareTag("TopWall"))
    {
        gameManager.PlayerScored();
        ResetBall(playerSpawn.position, false);
    }
}

    private void BounceBall(Collision2D collision)
{
    Vector2 reflectDir = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

    if (collision.collider.CompareTag("PlayerPaddle") && reflectDir.y < 0)
        reflectDir.y *= -1;
    if (collision.collider.CompareTag("AIPaddle") && reflectDir.y > 0)
        reflectDir.y *= -1;

   
    if (Mathf.Abs(reflectDir.x) < 0.1f)
        reflectDir.x = Random.Range(-0.3f, 0.3f);

    rb.linearVelocity = reflectDir.normalized * initialSpeed;
    initialSpeed *= speedIncreaseFactor;
}

    
    public bool HasCrossedMidline()
    {
        return hasCrossedMidline;
    }
    public string LastHitBy()
    {
        return lastHitBy;
    }
}

