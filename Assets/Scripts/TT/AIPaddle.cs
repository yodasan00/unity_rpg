// using UnityEngine;

// public class AIPaddle : MonoBehaviour
// {
//     public float moveSpeed = 6f;
//     public Transform ball;
//     public float upperLimitY = 4f;   // Top region boundary
//     public float lowerLimitY = 0f;   // Midline
//     public float leftLimitX = -7f;
//     public float rightLimitX = 7f;

//     void Update()
//     {
//         if (ball == null) return;

//         Vector3 targetPos = transform.position;

//         // Only move when ball is in or above AI’s half
//         if (ball.position.y > lowerLimitY)
//         {
//             targetPos.x = Mathf.MoveTowards(transform.position.x, ball.position.x, moveSpeed * Time.deltaTime);
//             targetPos.y = Mathf.MoveTowards(transform.position.y, ball.position.y, moveSpeed * Time.deltaTime);
//         }

//         // Clamp to AI's half
//         targetPos.x = Mathf.Clamp(targetPos.x, leftLimitX, rightLimitX);
//         targetPos.y = Mathf.Clamp(targetPos.y, lowerLimitY, upperLimitY);

//         transform.position = targetPos;
//     }
// }


// using UnityEngine;

// public class AIPaddle : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public Transform ball;
//     public Vector2 minBounds;
//     public Vector2 maxBounds;
//     public float reactionDelay = 0.1f; // small delay for realism

//     private Vector3 targetPos;
//     private float timer = 0f;

//     void Update()
//     {
//         if (ball == null) return;

//         timer += Time.deltaTime;
//         if (timer >= reactionDelay)
//         {
//             timer = 0f;
//             targetPos = new Vector3(
//                 Mathf.Clamp(ball.position.x, minBounds.x, maxBounds.x),
//                 Mathf.Clamp(ball.position.y, minBounds.y, maxBounds.y),
//                 0f
//             );
//         }

//         transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
//     }
// }


// using UnityEngine;

// public class AIPaddle : MonoBehaviour
// {
//     [Header("AI Settings")]
//     public float moveSpeed = 5f;           // Paddle movement speed
//     public float predictionFactor = 0.5f;  // How far ahead AI predicts

//     [Header("Movement Bounds (AI region)")]
//     public Vector2 minBounds; // bottom-left corner of AI half
//     public Vector2 maxBounds; // top-right corner of AI half

//     [Header("Ball Reference")]
//     public Transform ball;

//     private BallController ballController;

//     void Awake()
//     {
//         if (ball != null)
//             ballController = ball.GetComponent<BallController>();
//     }

//     void Update()
//     {
//         if (ball == null || ballController == null) return;

//         // AI only moves when ball has crossed midline towards AI
//         if (!ballController.HasCrossedMidline() && ballController.LastHitBy() != "AI")
//             return;

//         // Predict ball position slightly
//         Vector3 predictedPos = ball.position + (Vector3)(ball.GetComponent<Rigidbody2D>().linearVelocity * predictionFactor);

//         Vector3 targetPos = new Vector3(
//             Mathf.Clamp(predictedPos.x, minBounds.x, maxBounds.x),
//             Mathf.Clamp(predictedPos.y, minBounds.y, maxBounds.y),
//             0f
//         );

//         transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
//     }
// }

using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [Header("AI Settings")]
    public float moveSpeed = 5f;           // Paddle movement speed
    public float predictionFactor = 0.5f;  // How far ahead AI predicts the ball

    [Header("Movement Bounds (AI region)")]
    public Vector2 minBounds; // bottom-left corner of AI half
    public Vector2 maxBounds; // top-right corner of AI half

    [Header("Ball Reference")]
    public Transform ball;

    private Rigidbody2D ballRb;

    void Awake()
    {
        if (ball != null)
            ballRb = ball.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (ball == null || ballRb == null) return;

        // Only track ball if it's moving toward AI (assuming AI is top)
        if (ballRb.linearVelocity.y <= 0) return;

        // Predict future position based on ball velocity
        Vector3 predictedPos = ball.position + (Vector3)(ballRb.linearVelocity * predictionFactor);

        // Clamp predicted position to AI region
        predictedPos.x = Mathf.Clamp(predictedPos.x, minBounds.x, maxBounds.x);
        predictedPos.y = Mathf.Clamp(predictedPos.y, minBounds.y, maxBounds.y);

        // Move AI paddle toward predicted position
        transform.position = Vector3.MoveTowards(transform.position, predictedPos, moveSpeed * Time.deltaTime);
    }
}

