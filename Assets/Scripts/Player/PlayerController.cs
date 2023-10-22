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

    private Vector2 _moveInput;
    private Vector2 _playerVelocity;
    private Rigidbody2D _playerRigidbody;
    private Animator _bodyAnimator;
    private BoxCollider2D _bodyCollider;
    private AudioPlayer _audioPlayer;
    private PlayerAnimator _playerAnimator;
    private PlayerGunController _playerGunController;
    private PlayerInput _playerInput;

    private float _jumpInput;
    private float _moveSpeed;
    private bool _hasGun = false;
    
    public bool HasGun
    {
        set
        {
            _hasGun = value;
            _moveSpeed = HasGun ? walkSpeed : runSpeed;
            _playerAnimator.SetHasGunBools(value);
        }
        get { return _hasGun; }
    }

    public bool CanMove { set; get; }

    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _bodyAnimator = GetComponent<Animator>();
        _bodyCollider = GetComponent<BoxCollider2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerGunController = GetComponent<PlayerGunController>();
        _playerInput = GetComponent<PlayerInput>();

        _moveSpeed = runSpeed;
        CanMove = true;
    }

    private void FixedUpdate()
    {
        SetVelocityOnFalling();
    }

    private void Update()
    {
        Move();
        FlipSprite();
        Jump();
        SetJumpAnimAndSound();
        StopMovingWhileFlipping();
        StopWalkAnimOnWallCollission();
    }

    private void StopWalkAnimOnWallCollission()
    {
        if (IsTouchingWall())
        {
            _playerAnimator.SetWalkBools(false);
        }
    }

    private void StopMovingWhileFlipping()
    {
        if (_playerAnimator.PlayerIsFlipping || _playerAnimator.PlayerIsTeleporting)
        {
            _playerRigidbody.velocity = new(0f, _playerRigidbody.velocity.y);
        }
    }

    private void Move()
    {
        if (!CanMove)
        {
            _playerRigidbody.velocity = new(0f, _playerRigidbody.velocity.y);
            return;
        }

        if (IsGrounded())
        {
            _playerVelocity = new(_moveInput.x * _moveSpeed, _playerRigidbody.velocity.y);
            _playerRigidbody.velocity = _playerVelocity;

            _playerAnimator.SetWalkSpeed(Mathf.Abs(_playerRigidbody.velocity.x / walkSpeed));

            bool playerHasHorizontalSpeed = Mathf.Abs(_playerRigidbody.velocity.x) > Mathf.Epsilon;
            _playerAnimator.SetWalkBools(playerHasHorizontalSpeed);
        }
        else if (_playerRigidbody.velocity.y > 0)
        {
            _playerRigidbody.velocity = new(_playerVelocity.x, _playerRigidbody.velocity.y);
            _playerAnimator.SetWalkBools(false);
        } else
        {
            _playerAnimator.SetWalkBools(false);
        }
    }

    /// <summary>
    /// Method is called in BodyWalk anim.
    /// </summary>
    public void PlayFootSetpClip()
    {
        _audioPlayer.PlayFootStepClip(transform.position);
    }

    private void FlipSprite()
    {
        // I don't know why but when I start using tilemaps after stop moving sometimes player has
        // speed -1.754443E-15 in opposite direction so player sprite is flipping and flipping.
        // Comparing to 0.01 works fine. But problem now doesn't occur. I leave it here just in case.
        // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0.01f;
        bool playerHasHorizontalSpeed = Mathf.Abs(_playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed && IsGrounded())
        {
            transform.localScale = new Vector2(-Mathf.Sign(_playerRigidbody.velocity.x), 1f);
        }
    }

    private void SetJumpAnimAndSound()
    {
        if (IsGrounded())
        {
            if (_bodyAnimator.GetBool(K.ACP.Jump))
            {
                _audioPlayer.PlayLandingClip(_playerRigidbody.transform.position);
                _playerAnimator.TriggerArmsLanding();
            }
            _playerAnimator.SetJumpBool(false);
        }
        else
        {
            if (!_bodyAnimator.GetBool(K.ACP.Jump))
            {
                _playerAnimator.SetJumpBool(true);

                if (_jumpInput > 0.0f)
                {
                    _audioPlayer.PlayJumpingClip(_playerRigidbody.transform.position);
                }
            }
        }
    }

    private void Jump()
    {
        if (IsGrounded() && !_playerAnimator.GetIsTeleportingBool())
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpInput);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.02f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(_bodyCollider.bounds.center, _bodyCollider.bounds.size, 0f, Vector2.down, extraHeight, LayerMask.GetMask(K.L.Ground));

        // I've changes this becasue of one way platforms. Hope it doesn't
        // destroy something.
        //return raycastHit.collider != null;
        return raycastHit.collider != null && Mathf.Abs(_playerRigidbody.velocity.y) <= 0.01;
    }

    private bool IsTouchingWall()
    {
        float extraHeight = 0.02f;
        RaycastHit2D topRaycastHit;
        RaycastHit2D bottomRaycastHit;
        Vector2 bodyCenter = _bodyCollider.bounds.center;
        Vector2 bodyTop = new(bodyCenter.x, bodyCenter.y + 0.5f);
        Vector2 bodyBottom = new(bodyCenter.x, bodyCenter.y - 0.7f);

        if (transform.localScale.x < 0)
        {
            topRaycastHit = Physics2D.Raycast(bodyTop, Vector2.right, _bodyCollider.bounds.extents.x + extraHeight, LayerMask.GetMask(K.L.Ground));
            bottomRaycastHit = Physics2D.Raycast(bodyBottom, Vector2.right, _bodyCollider.bounds.extents.x + extraHeight, LayerMask.GetMask(K.L.Ground));
        }
        else
        {
            topRaycastHit = Physics2D.Raycast(bodyTop, Vector2.left, _bodyCollider.bounds.extents.x + extraHeight, LayerMask.GetMask(K.L.Ground));
            bottomRaycastHit = Physics2D.Raycast(bodyBottom, Vector2.left, _bodyCollider.bounds.extents.x + extraHeight, LayerMask.GetMask(K.L.Ground));
        }

        return topRaycastHit.collider != null || bottomRaycastHit.collider != null;
    }

    /// <summary>
    /// Sets player's horizontal velocity when player is falling off the platform's edge.
    /// </summary>
    private void SetVelocityOnFalling()
    {
        // And here again, I can't compare value to Mathf.Epsilon because player has some stupid x velocity when moving left.
        if (_playerRigidbody.velocity.y > -11)
        {
            if (Mathf.Abs(_playerRigidbody.velocity.x) > 0.01 && Mathf.Abs(_playerRigidbody.velocity.x) < _moveSpeed && _playerRigidbody.velocity.y != 0)
            {
                float xVelocity = _moveSpeed * -transform.localScale.x;
                _playerRigidbody.velocity = new(xVelocity, _playerRigidbody.velocity.y);
            }
        } else
        {
            _playerRigidbody.velocity = new(_playerRigidbody.velocity.x * 0.96f, _playerRigidbody.velocity.y);
        }

        if (_playerRigidbody.velocity.y < -13)
        {
            _playerRigidbody.velocity = new(_playerRigidbody.velocity.x, -13f);
        }
    }

    public void SetPlayerInput(bool value)
    {
        _playerInput.enabled = value;
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    /// <param name="value"></param>
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();

        if (_moveInput.x != 0
            && Mathf.Sign(_moveInput.x) == Mathf.Sign(transform.localScale.x)
            && IsGrounded())
        {
            _playerAnimator.TriggerBodyFlipping();
        }
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    void OnManageGun()
    {
        if (HasGun)
        {
            HasGun = false;
            _playerAnimator.TriggerGunHiding();
        } else if (_playerGunController.AmmoCounter > 0)
        {
            _playerAnimator.TriggerGunTaking();
        }
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    /// <param name="value">InputValue from input system.</param>
    private void OnJump(InputValue value)
    {
        _jumpInput = jumpSpeed * value.Get<Vector2>().y;
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    /// <param name="value">InputValue from input system.</param>
    private void OnJumpLeft(InputValue value)
    {
        _jumpInput = jumpSpeed * value.Get<Vector2>().y;
        _moveInput = new(-value.Get<Vector2>().y, 0f);
    }

    /// <summary>
    /// Method triggered by input system.
    /// </summary>
    /// <param name="value">InputValue from input system.</param>
    private void OnJumpRight(InputValue value)
    {
        _jumpInput = jumpSpeed * value.Get<Vector2>().y;
        _moveInput = new(value.Get<Vector2>().y, 0f);
    }
}
