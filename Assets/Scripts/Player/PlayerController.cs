using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController: MonoBehaviour
{
    [Header("Move speed")]
    [SerializeField] float walkSpeed = 3.05f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpSpeed = 6.26f;

    Vector2 moveInput;
    Vector2 playerVelocity;
    Rigidbody2D myRigidbody;
    Animator bodyAnimator;
    CapsuleCollider2D feetCollider;
    BoxCollider2D bodyCollider;
    AudioPlayer audioPlayer;
    PlayerAnimator playerAnimator;

    bool hasGun = false;
    float jumpInput;
    float moveSpeed;

    private bool PlayerIsOnTheGround
    {
        get 
        { 
            return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || myRigidbody.velocity.y == 0;
        }
    }

    private bool PlayerIsFlipping
    {
        get 
        { 
            AnimatorClipInfo[] currClipInfo = bodyAnimator.GetCurrentAnimatorClipInfo(0);
            string name = currClipInfo[0].clip.name;
            return (name == "bodyNoGunFlip" || name == "bodyGunFlip");
        }
    }

    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        bodyAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        Move();
        Jump();
        SetJumpAnim();
        FlipSprite();
        StopMovingWhileFlipping();
        StopWalkAnimOnWallCollission();
    }

    private void StopWalkAnimOnWallCollission()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnimator.SetWalkBools(false);
        }
    }

    private void StopMovingWhileFlipping()
    {
        if (PlayerIsFlipping)
        {
            playerAnimator.SetArmsAlpha(0f);
            myRigidbody.velocity = new(0f, myRigidbody.velocity.y);
        } else
        {
            playerAnimator.SetArmsAlpha(1f);
        }
    }

    private void Move()
    {
        if (PlayerIsOnTheGround)
        {
            moveSpeed = hasGun ? walkSpeed : runSpeed;  // TODO: set it with hasGun bool

            playerVelocity = new(moveInput.x *  moveSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            playerAnimator.SetWalkBools(playerHasHorizontalSpeed);

            if (playerHasHorizontalSpeed && !bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                audioPlayer.PlayWalkClip(myRigidbody.transform.position);
            }
        }
        else
        {
            myRigidbody.velocity = new(playerVelocity.x, myRigidbody.velocity.y);
            playerAnimator.SetWalkBools(false);
        }
    }

    private void FlipSprite()
    {
        // I don't know why but when I start using tilemaps after stop moving sometimes player has
        // speed -1.754443E-15 in opposite direction so player sprite is flipping and flipping.
        // Comparing to 0.01 works fine. But problem now doesn't occur. I leave it here just in case.
        // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0.01f;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
                audioPlayer.PlayLandingClip(myRigidbody.transform.position);
                playerAnimator.TriggerArmsLanding();
            }
            playerAnimator.SetJumpBool(false);
        } else
        {
            if (!bodyAnimator.GetBool("Jump"))
            {
                audioPlayer.PlayJumpClip(myRigidbody.transform.position);
                playerAnimator.SetJumpBool(true);
            }
        }
    }

    private void Jump()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpInput);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.x != 0 && Mathf.Sign(moveInput.x) == Mathf.Sign(transform.localScale.x))
        {
            playerAnimator.TriggerBodyFlipping();
        }
    }

    void OnJump(InputValue value)
    {
        jumpInput = jumpSpeed * value.Get<Vector2>().y;
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
