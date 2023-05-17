using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move speed")]
    [SerializeField] float walkSpeed = 2.27f;
    [SerializeField] float runSpeed = 3.21f;
    [SerializeField] float jumpSpeed = 7f;

    Vector2 moveInput;
    Vector2 playerVelocity;
    Rigidbody2D myRigidbody;
    Animator bodyAnimator;
    BoxCollider2D bodyCollider;
    AudioPlayer audioPlayer;
    PlayerAnimator playerAnimator;
    PlayerGunController playerGunController;

    private bool hasGun = false;
    float jumpInput;
    float moveSpeed;

    public bool HasGun
    {
        get
        {
            return hasGun;
        }
        set
        {
            hasGun = value;
            moveSpeed = hasGun ? walkSpeed : runSpeed;
            playerAnimator.SetHasGunBools(value);
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

    private void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        bodyAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerGunController = GetComponent<PlayerGunController>();

        moveSpeed = runSpeed;
    }

    private void Update()
    {
        Move();
        Jump();
        SetJumpAnim();
        FlipSprite();
        StopMovingWhileFlipping();
        StopWalkAnimOnWallCollission();
        SetXVelocityOnJumping();
    }

    private void StopWalkAnimOnWallCollission()
    {
        if (IsTouchingWall())
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
        }
        else
        {
            playerAnimator.SetArmsAlpha(1f);
        }
    }

    private void Move()
    {
        if (IsGrounded())
        {
            playerVelocity = new(moveInput.x * moveSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            // TODO: Set playerAnimator&&armsAnimator.SetBool("WalkSpeed", value) - value based on playerVelocity

            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            playerAnimator.SetWalkBools(playerHasHorizontalSpeed);
        }
        else
        {
            myRigidbody.velocity = new(playerVelocity.x, myRigidbody.velocity.y);
            playerAnimator.SetWalkBools(false);
        }
    }

    public void PlayFootSetpClip()
    {
        audioPlayer.PlayFootStepClip(transform.position);
    }

    private void FlipSprite()
    {
        // I don't know why but when I start using tilemaps after stop moving sometimes player has
        // speed -1.754443E-15 in opposite direction so player sprite is flipping and flipping.
        // Comparing to 0.01 works fine. But problem now doesn't occur. I leave it here just in case.
        // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0.01f;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed && IsGrounded())
        {
            transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void SetJumpAnim()
    {
        if (IsGrounded())
        {
            if (bodyAnimator.GetBool("Jump"))
            {
                audioPlayer.PlayLandingClip(myRigidbody.transform.position);
                playerAnimator.TriggerArmsLanding();
            }
            playerAnimator.SetJumpBool(false);
        }
        else
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
        if (IsGrounded())
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpInput);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.01f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bodyCollider.bounds.center, bodyCollider.bounds.size, 0f, Vector2.down, extraHeight, LayerMask.GetMask("Ground"));
        //Color rayColor;
        //if (raycastHit.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}

        //Debug.DrawRay(bodyCollider.bounds.center + new Vector3(bodyCollider.bounds.extents.x, 0f), Vector2.down * (bodyCollider.bounds.extents.y + extraHeight), rayColor);
        //Debug.DrawRay(bodyCollider.bounds.center - new Vector3(bodyCollider.bounds.extents.x, 0f), Vector2.down * (bodyCollider.bounds.extents.y + extraHeight), rayColor);
        //Debug.DrawRay(bodyCollider.bounds.center - new Vector3(bodyCollider.bounds.extents.x, bodyCollider.bounds.extents.y + extraHeight), Vector2.right * (bodyCollider.bounds.extents.x), rayColor);

        //Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }

    private bool IsTouchingWall()
    {
        float extraHeight = 0.02f;
        RaycastHit2D raycastHit;

        if (transform.localScale.x < 0)
        {
            raycastHit = Physics2D.Raycast(bodyCollider.bounds.center, Vector2.right, bodyCollider.bounds.extents.x + extraHeight, LayerMask.GetMask("Ground"));
        }
        else
        {
            raycastHit = Physics2D.Raycast(bodyCollider.bounds.center, Vector2.left, bodyCollider.bounds.extents.x + extraHeight, LayerMask.GetMask("Ground"));
        }


        //Color rayColor;
        //if (raycastHit.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}

        //if (transform.localScale.x < 0)
        //{
        //    Debug.DrawRay(bodyCollider.bounds.center, Vector2.right * (bodyCollider.bounds.extents.x + extraHeight), rayColor);
        //}
        //else
        //{
        //    Debug.DrawRay(bodyCollider.bounds.center, Vector2.left * (bodyCollider.bounds.extents.x + extraHeight), rayColor);
        //}

        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;


    }

    private void SetXVelocityOnJumping()
    {
        if (myRigidbody.velocity.x != 0 && myRigidbody.velocity.y != 0)
        {
            float xVelocity = HasGun ? walkSpeed * -transform.localScale.x : runSpeed * -transform.localScale.x;

            myRigidbody.velocity = new(xVelocity, myRigidbody.velocity.y);
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

    void OnHideTakeGun()
    {
        if (HasGun)
        {
            playerAnimator.TriggerGunHiding();
            HasGun = false;
        } else if (playerGunController.AmmoCounter > 0)
        {
            playerAnimator.TriggerGunTaking();
        }
    }

    void OnJumpLeft(InputValue value)
    {
        if (value.Get<Vector2>().y == 1)
        {
            jumpInput = jumpSpeed * 1;
            moveInput = new(-1f, 0f);
        } else
        {
            jumpInput = 0;
            moveInput = new(0f, 0f);
        }
    }

    void OnJumpRight(InputValue value)
    {
        if (value.Get<Vector2>().y == 1)
        {
            jumpInput = jumpSpeed * 1;
            moveInput = new(1f, 0f);
        }
        else
        {
            jumpInput = 0;
            moveInput = new(0f, 0f);
        }
    }
}
