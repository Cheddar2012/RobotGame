using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string _playerObjectTag = "Player";
    private const string _movementAxis = "Horizontal";
    private const string _jumpAxis = "Jump";

    private CharacterMotion _playerMotion;

	void Start()
    {
        _playerMotion = GameObject.FindGameObjectWithTag(_playerObjectTag).GetComponent<CharacterMotion>();
	}

    void Update()
    {
        float movementDirection = Input.GetAxisRaw(_movementAxis);
        if (movementDirection < 0)
        {
            _playerMotion.MoveLeft();
        }
        else if (movementDirection > 0)
        {
            _playerMotion.MoveRight();
        }
        else
        {
            _playerMotion.HorizontalMovementStopped();
        }

        if (Input.GetButtonDown("Jump"))
        {
            _playerMotion.AttemptJump();
        }
    }
}
