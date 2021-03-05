using UnityEngine;
using UnityEngine.Events;

public class MovementSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform groundCheck;
    [SerializeField] private LayerMask layerMask;
    [Range(0, 1f)] private float smoothingMovement = 0.05f;
    public float jumpPower = 250f;

    const float RADIUS = .2f;
    bool isGrounded;
    bool isFacingRight = true;
    Vector3 velocity = Vector3.zero;

    public UnityEvent onLandEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if(onLandEvent == null)
        {
            onLandEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, RADIUS, layerMask);

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;

                if (!wasGrounded)
                {
                    onLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float axis,bool jumpFlag)
    {
        Vector3 target = new Vector2(axis, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, target, ref velocity, smoothingMovement);

        if (isFacingRight && axis < 0)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (!isFacingRight && axis > 0)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (isGrounded && jumpFlag)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }
    }
}
