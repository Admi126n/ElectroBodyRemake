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
    float jumpInput;
    Vector2 playerVelocity;
    Rigidbody2D myRigidbody;
    Animator bodyAnimator;
    Animator armsAnimator;
    CapsuleCollider2D feetCollider;
    BoxCollider2D bodyCollider;

    bool hasGun = false;

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
        Jump();
        SetJumpAnim();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    /*
     * FIXME:
     * - when you jump left and during flight press right arrow player should immediately go right  DONE
     * - when you are pressing left/right arrow and up arrow you should continuously jump and jump
     * - when you jump left and during flight press right and up arrows player should immediately jump right
     */

    private void Move()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerVelocity = new(moveInput.x * walkSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0.01f;
            bodyAnimator.SetBool("Walk", playerHasHorizontalSpeed);
            armsAnimator.SetBool("Walk", playerHasHorizontalSpeed);
        }
        else
        {
            myRigidbody.velocity = new(playerVelocity.x, myRigidbody.velocity.y);
            bodyAnimator.SetBool("Walk", false);
            armsAnimator.SetBool("Walk", false);
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

        // block moving
        // play flip anim
        // unblock moving

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
        jumpInput = jumpSpeed * value.Get<Vector2>().y;
    }

    private void Jump()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpInput);
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
