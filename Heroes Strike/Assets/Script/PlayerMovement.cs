using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    MovementSystem controller;
    Animator anim;

    [Header("Movement Speed")]
    public float movementSpeed;

    [Header("Player Conditions")]
    public bool isAttacking;
    public bool isDefending;
    public bool isJump;

    float inputX = 0f;
    public bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<MovementSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isGamePaused && !FindObjectOfType<UIManager>().shopMenu.activeInHierarchy && !CharacterStatus.isDead)
        {
            inputX = Input.GetAxisRaw("Horizontal") * movementSpeed;            

            if (isAttacking || isDefending || Portal.isCanLoad)
            {
                canMove = false;
            }
            else
            {
                canMove = true;

                if (Input.GetButton("Jump"))
                {
                    isJump = true;
                    anim.SetBool("isJump", true);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.Move(inputX, isJump);
            anim.SetFloat("xValue", Mathf.Abs(inputX));
            isJump = false;
        }
    }

    public void OnLanding()
    {
        anim.SetBool("isJump", false);
    }

}
