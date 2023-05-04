using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move speed")]
    float walkSpeed = 3f;
    float runSpeed = 4f;
    float jumpSpeed = 5f;

    Vector2 moveInput;
    Vector2 playerVelocity;
    Rigidbody2D myRigidbody;
    Animator bodyAnimator;
    Animator armsAnimator;
    CapsuleCollider2D feetCollider;
    BoxCollider2D bodyCollider;

    bool hasGun = false;
    bool shouldStopAfterLanding = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        bodyAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        armsAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        SetJumpAnim();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            moveInput = value.Get<Vector2>();
        } else if (value.Get<Vector2>().x == 0)
        {
            shouldStopAfterLanding = true;
        } else
        {
            shouldStopAfterLanding = false;
        }
    }

    /*
     * FIXME:
     * - when you jump left and during flight press right arrow player should immediately go right
     * - when you are pressing left/right arrow and up arrow you should continuously jump and jump
     * - when you jump left and during flight press right and up arrows player should immediately jump right
     */

    private void Move()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && bodyAnimator.GetBool("Jump") && shouldStopAfterLanding)
        {
            moveInput = new(0f, 0f);
            shouldStopAfterLanding = false;
        } else
        {
            if (hasGun)
            {
                playerVelocity = new(moveInput.x * walkSpeed, myRigidbody.velocity.y);
            }
            else
            {
                playerVelocity = new(moveInput.x * runSpeed, myRigidbody.velocity.y);
            }

            myRigidbody.velocity = playerVelocity;

            // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0.01f;
            // Debug.Log(IsMoving());
            bodyAnimator.SetBool("Walk", playerHasHorizontalSpeed);
            armsAnimator.SetBool("Walk", playerHasHorizontalSpeed);
        }
    }

    private bool IsMoving()
    {
        return true;
    }


    private void FlipSprite()
    {
        // I don't know why but when I start using tilemaps after stop moving player has
        // speed -1.754443E-15 in opposite direction so player sprite is flipping and flipping.
        // Comparing to 0.01 works fine.
        // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0.01f;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void SetJumpAnim()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (bodyAnimator.GetBool("Jump"))
            {
                armsAnimator.SetTrigger("Landed");
            }
            bodyAnimator.SetBool("Jump", false);
        } else
        {
            bodyAnimator.SetBool("Jump", true);
        }
    }

    void OnJump(InputValue value)
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnTeleport()
    {
        Debug.Log("Teleporting...");
    }

    void OnFire()
    {
        Debug.Log("Fire!");
    }
}
