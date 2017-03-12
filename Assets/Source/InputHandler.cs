using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerMotion _playerMotion;

	void Start()
    {
        _playerMotion = GameManager.Instance.GetPlayer().GetComponent<PlayerMotion>();
	}

    void Update()
    {
        // Handle horizontal movement
        float movementDirection = Input.GetAxisRaw(InputStrings.MovementAxis);
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
        if (Input.GetButtonDown(InputStrings.JumpButton))
        {
            _playerMotion.AttemptJump();
        }

        // Handle attacking
        if (Input.GetButtonDown(InputStrings.AttackButton))
        {
            _playerMotion.AttemptAttack();
        }
        if (Input.GetButtonDown(InputStrings.RangedAttackButton))
        {
            _playerMotion.AttemptRangedAttack();
        }

        // Handle blocking
        if (Input.GetButtonDown(InputStrings.BlockButton))
        {
            _playerMotion.ToggleBlock(true);
        }
        else if (Input.GetButtonUp(InputStrings.BlockButton))
        {
            _playerMotion.ToggleBlock(false);
        }
    }
}
