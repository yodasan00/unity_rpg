// using System.Collections;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public float moveSpeed = 7f;
//     public Rigidbody2D rb;
//     private Animator animator;

//     private Vector2 movement;
//     private bool isMoving;

//     void Awake()
//     {
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         // Get raw input
//         movement.x = Input.GetAxisRaw("Horizontal");
//         movement.y = Input.GetAxisRaw("Vertical");

//         // Restrict to cardinal directions (no diagonal movement)
//         if (Mathf.Abs(movement.x) > 0) movement.y = 0;

//         animator.SetFloat("moveX", movement.x);
//         animator.SetFloat("moveY", movement.y);
//         animator.SetBool("isMoving", movement != Vector2.zero);
//     }

//     void FixedUpdate()
//     {
//         if (movement != Vector2.zero && !isMoving)
//         {
//             Vector2 targetPos = rb.position + movement;

//             if (IsWalkable(targetPos))
//                 StartCoroutine(Move(targetPos));
//         }
//     }

//     System.Collections.IEnumerator Move(Vector2 targetPos)
//     {
//         isMoving = true;

//         while ((targetPos - rb.position).sqrMagnitude > Mathf.Epsilon)
//         {
//             rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime));
//             yield return new WaitForFixedUpdate();
//         }

//         rb.MovePosition(targetPos); // Snap exactly to grid
//         isMoving = false;
//     }

//     bool IsWalkable(Vector2 pos)
//     {
//         Collider2D hit = Physics2D.OverlapCircle(pos, 0.1f, LayerMask.GetMask("SolidObjects"));
//         return hit == null;
//     }
// }

using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    private bool isMoving;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(movement.x) > 0) movement.y = 0;

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        animator.SetBool("isMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero && !isMoving)
        {
            if (IsWalkable(movement))
            {
                Vector2 targetPos = rb.position + movement;
                StartCoroutine(Move(targetPos));
            }
        }
    }

    IEnumerator Move(Vector2 targetPos)
    {
        isMoving = true;

        while ((targetPos - rb.position).sqrMagnitude > Mathf.Epsilon)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos);
        isMoving = false;
    }

    bool IsWalkable(Vector2 direction)
    {
        // Raycast 1 unit in the direction of movement
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 1f, LayerMask.GetMask("SolidObjects"));
        return hit.collider == null;
    }
}
