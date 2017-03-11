using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
    private const float _walkSpeed = 0.1f;

    // Characters should face slightly toward the camera instead of directly left or right
    private const float _leftFacingRotation = 240;
    private const float _rightFacingRotation = 120;

    private CharacterController _controller;
    private Animator _animator;

    private float _horizontalMovement;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _horizontalMovement = 0;
    }

    void FixedUpdate()
    {
        if (_horizontalMovement != 0)
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

            _controller.Move(new Vector3(_horizontalMovement, 0, 0));

            // Clear the movement vector after new movement has been applied
            _horizontalMovement = 0;
        }
    }

    public void MoveLeft()
    {
        _horizontalMovement -= _walkSpeed;
    }

    public void MoveRight()
    {
        _horizontalMovement += _walkSpeed;
    }

    public void HorizontalMovementStopped()
    {
        _animator.SetBool("moving", false);
    }
}
