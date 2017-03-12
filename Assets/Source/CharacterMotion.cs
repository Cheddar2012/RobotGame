using UnityEngine;

public abstract class CharacterMotion : MonoBehaviour
{
    // Characters should face slightly toward the camera instead of directly left or right
    private const float _leftFacingRotation = 240.0F;
    private const float _rightFacingRotation = 120.0F;
    
    private const float _gravity = -0.8F;

    private CharacterController _controller;
    protected Animator _animator;
    
    // The horizontal speed of the character when in motion
    [SerializeField]
    protected float _moveSpeed = 0.0F;

    protected bool _haltMotion;
    protected float _horizontalMovement;
    protected float _verticalMovement;

    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _haltMotion = false;
        _horizontalMovement = 0;
        _verticalMovement = 0;
    }

    protected virtual void FixedUpdate()
    {
        HandleHorizontalMotion();
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

    public void MoveLeft()
    {
        if (!_haltMotion)
        {
            _horizontalMovement -= (_moveSpeed * Time.deltaTime);
        }
    }

    public void MoveRight()
    {
        if (!_haltMotion)
        {
            _horizontalMovement += (_moveSpeed * Time.deltaTime);
        }
    }

    public void HorizontalMovementStopped()
    {
        _animator.SetBool("moving", false);
    }

    protected virtual void HandleHorizontalMotion()
    {
        if (_horizontalMovement != 0)
        {
            if (_haltMotion)
            {
                _animator.SetBool("moving", false);
                _horizontalMovement = 0;
            }
            else
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
        }
    }

    protected virtual void HandleVerticalMotion()
    {
        if (_controller.isGrounded)
        {
            HandleGroundedVerticalMotion();
        }
        else
        {
            HandleAirborneVerticalMotion();
        }
    }

    protected virtual void HandleGroundedVerticalMotion()
    {
        Grounded();
    }

    protected virtual void Grounded()
    {
        // isGrounded will be false if _verticalMovement is 0, so gravity must be continuously applied
        _verticalMovement = _gravity;

        _animator.SetBool("airborne", false);
    }

    private void HandleAirborneVerticalMotion()
    {
        _verticalMovement += (_gravity * Time.deltaTime);
    }

    public virtual void Die()
    {
        _animator.SetBool("dead", true);
        _haltMotion = true;
    }
}
