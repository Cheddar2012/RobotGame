using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
    private enum JumpState { Grounded, PreJump, Jump }

    private const float _walkSpeed = 5.0F;

    // Characters should face slightly toward the camera instead of directly left or right
    private const float _leftFacingRotation = 240.0F;
    private const float _rightFacingRotation = 120.0F;
    
    // The time, in seconds, between pressing the jump key and lifting off
    // This should match the amount of time the animation takes
    private const float _jumpDelay = 0.35F;

    private const float _gravity = -0.8F;
    private const float _jumpSpeed = 0.3F;
    
    private CharacterController _controller;
    private Animator _animator;

    private float _horizontalMovement;
    private float _verticalMovement;

    private JumpState _jumpState;
    private float _jumpStart;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _horizontalMovement = 0;
        _verticalMovement = 0;
        
        _jumpState = JumpState.Grounded;
        _jumpStart = 0;
    }

    void FixedUpdate()
    {
        if (_horizontalMovement != 0)
        {
            HandleHorizontalMotion();
        }

        HandleVerticalMotion();

        if (_horizontalMovement != 0 || _verticalMovement != 0)
        {
            _controller.Move(new Vector3(_horizontalMovement, _verticalMovement, 0));

            // Horizontal movement has been handled, so clear it
            _horizontalMovement = 0;

            if (!_controller.isGrounded)
            {
                _animator.SetBool("airborne", true);
            }
        }
    }

    private void HandleHorizontalMotion()
    {
        _animator.SetBool("moving", true);

        if (_horizontalMovement < 0)
        {
            _controller.transform.eulerAngles = new Vector3(0, _leftFacingRotation, 0);
        }
        else
        {
            _controller.transform.eulerAngles = new Vector3(0, _rightFacingRotation, 0);
        }
    }

    private void HandleVerticalMotion()
    {
        if (_controller.isGrounded)
        {
            if (_jumpState != JumpState.PreJump)
            {
                Grounded();
            }
            else if (_jumpState == JumpState.PreJump && Time.time >= _jumpStart + _jumpDelay)
            {
                Jump();
            }
        }
        else
        {
            HandleAirborneMotion();
        }
    }

    private void Jump()
    {
        _verticalMovement = _jumpSpeed;
        _jumpState = JumpState.Jump;
    }

    private void HandleAirborneMotion()
    {
        _verticalMovement += (_gravity * Time.deltaTime);
    }

    public void MoveLeft()
    {
        _horizontalMovement -= (_walkSpeed * Time.deltaTime);
    }

    public void MoveRight()
    {
        _horizontalMovement += (_walkSpeed * Time.deltaTime);
    }

    public void HorizontalMovementStopped()
    {
        _animator.SetBool("moving", false);
    }

    public void AttemptJump()
    {
        if (_jumpState == JumpState.Grounded)
        {
            _animator.SetBool("jumping", true);
            _jumpState = JumpState.PreJump;
            _jumpStart = Time.time;
        }
    }

    private void Grounded()
    {
        _animator.SetBool("jumping", false);
        _animator.SetBool("airborne", false);
        _jumpState = JumpState.Grounded;

        // isGrounded will be false if _verticalMovement is 0, so gravity must be continuously applied
        _verticalMovement = _gravity;
    }
}
