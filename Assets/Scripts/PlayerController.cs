using System.Collections;
using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public float MoveSpeed;
//     private bool isMoving;

//     public LayerMask SolidObjects;
//     private Vector2 input;
//     private Animator animator;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(!isMoving){
//             input.x = Input.GetAxisRaw("Horizontal"); //sends 1 if Right key is  pressed nabha -1 left
//             input.y = Input.GetAxisRaw("Vertical"); //sends 1 if Upper key is  pressed nabha -1 down

//             if(input != Vector2.zero){
//                 animator.SetFloat("moveX",input.x);
//                 animator.SetFloat("moveY",input.y);
//                 var targetPos = transform.position;
//                 targetPos.x += input.x;
//                 targetPos.y += input.y;
//                 if(IsWalkable(targetPos))
//                     StartCoroutine(Move(targetPos));

//             }

//         }
//         animator.SetBool("isMoving",isMoving);
        
//     }

//     IEnumerator Move(Vector3 targetpos){
//         isMoving = true;
//         while((targetpos-transform.position).sqrMagnitude > Mathf.Epsilon){
//             transform.position = Vector3.MoveTowards(transform.position,targetpos,MoveSpeed*Time.deltaTime);
//             yield return null;
//         }

//         transform.position = targetpos;
//         isMoving = false;

//     }

//     private bool IsWalkable(Vector3 targetpos){
//         //check if the target position is walkable
//         //if not return false
//         //if walkable return true
//      if (Physics2D.OverlapCircle(targetpos,0.3f,SolidObjects) != null){
//             return false;
//         }
//         return true;    
//     }
// }


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    // public Animator animator;

    private Vector2 movement;
    private bool isMoving;

    void Update()
    {
        // Get raw input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Restrict to cardinal directions (no diagonal movement)
        if (Mathf.Abs(movement.x) > 0) movement.y = 0;

        // Update animator values
        // animator.SetFloat("moveX", movement.x);
        // animator.SetFloat("moveY", movement.y);
        // animator.SetBool("isMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero && !isMoving)
        {
            Vector2 targetPos = rb.position + movement;

            if (IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));
        }
    }

    System.Collections.IEnumerator Move(Vector2 targetPos)
    {
        isMoving = true;

        while ((targetPos - rb.position).sqrMagnitude > Mathf.Epsilon)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos); // Snap exactly to grid
        isMoving = false;
    }

    bool IsWalkable(Vector2 pos)
    {
        Collider2D hit = Physics2D.OverlapCircle(pos, 0.1f, LayerMask.GetMask("SolidObjects"));
        return hit == null;
    }
}
