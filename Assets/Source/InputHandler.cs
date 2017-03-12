using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string _playerObjectTag = "Player";
    private const string _movementAxis = "Horizontal";
    private const string _jumpButton = "Jump";
    private const string _attackButton = "Attack1";
    private const string _rangedAttackButton = "Attack2";
    private const string _blockButton = "Block";

    private PlayerMotion _playerMotion;

	void Start()
    {
        _playerMotion = GameObject.FindGameObjectWithTag(_playerObjectTag).GetComponent<PlayerMotion>();
	}

    void Update()
    {
        // Handle horizontal movement
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

        // Handle jumping
        if (Input.GetButtonDown(_jumpButton))
        {
            _playerMotion.AttemptJump();
        }

        // Handle attacking
        if (Input.GetButtonDown(_attackButton))
        {
            _playerMotion.AttemptAttack();
        }
        if (Input.GetButtonDown(_rangedAttackButton))
        {
            _playerMotion.AttemptRangedAttack();
        }

        // Handle blocking
        if (Input.GetButtonDown(_blockButton))
        {
            _playerMotion.ToggleBlock(true);
        }
        else if (Input.GetButtonUp(_blockButton))
        {
            _playerMotion.ToggleBlock(false);
        }
    }
}
