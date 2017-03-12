using UnityEngine;

public class PlayerMotion : CharacterMotion
{
    private enum JumpState { Grounded, PreJump, Jump }
    private enum Attack { None, RightPunch, LeftPunch, RocketPunch, Mace }

    // The time, in seconds, between pressing the jump key and lifting off
    // This should match the amount of time the animation takes to lift off
    private const float _jumpDelay = 0.35F;

    private const float _jumpSpeed = 0.3F;

    // Allow player to begin next attack once their current attack is partway through
    private const float _earliestAttackFollowUp = 0.5F;
    private const float _earliestRangedAttackFollowUp = 0.6F;

    private JumpState _jumpState;
    private float _jumpStart;

    private Attack _currentAttack;

    private bool _blocking;
    
    protected override void Start ()
    {
        base.Start();

        _jumpState = JumpState.Grounded;
        _jumpStart = 0;

        _currentAttack = Attack.None;
        _blocking = false;
    }

    protected override void FixedUpdate ()
    {
        base.FixedUpdate();

        HandleAttackingState();
	}

    public void AttemptJump()
    {
        if (!_haltMotion && _jumpState == JumpState.Grounded)
        {
            _animator.SetBool("jumping", true);
            _jumpState = JumpState.PreJump;
            _jumpStart = Time.time;
        }
    }

    public void AttemptAttack()
    {
        if (_jumpState == JumpState.Grounded)
        {
            if (_currentAttack == Attack.None)
            {
                _haltMotion = true;
                _currentAttack = Attack.RightPunch;
            }
            else if (_currentAttack < Attack.RocketPunch)
            {
                float currentAnimationCompletion = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (currentAnimationCompletion >= _earliestAttackFollowUp)
                {
                    if (_currentAttack == Attack.RightPunch)
                    {
                        _currentAttack = Attack.LeftPunch;
                    }
                    else if (_currentAttack == Attack.LeftPunch)
                    {
                        _currentAttack = Attack.RightPunch;
                    }
                }
            }

            _animator.SetInteger("attackType", (int)_currentAttack);
        }
    }

    public void AttemptRangedAttack()
    {
        if (_jumpState == JumpState.Grounded && !_blocking)
        {
            if (_currentAttack == Attack.None)
            {
                _haltMotion = true;
                _currentAttack = Attack.RocketPunch;
            }
            else if (_currentAttack < Attack.RocketPunch)
            {
                float currentAnimationCompletion = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (currentAnimationCompletion >= _earliestRangedAttackFollowUp)
                {
                    _currentAttack = Attack.RocketPunch;
                }
            }
        }

        _animator.SetInteger("attackType", (int)_currentAttack);
    }

    public void ToggleBlock(bool blocking)
    {
        if (_jumpState == JumpState.Grounded && _currentAttack == Attack.None)
        {
            _haltMotion = blocking;
            _blocking = blocking;
            _animator.SetBool("blocking", blocking);
        }
    }

    private void Jump()
    {
        _verticalMovement = _jumpSpeed;
        _jumpState = JumpState.Jump;
    }

    protected override void HandleGroundedVerticalMotion()
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

    protected override void Grounded()
    {
        base.Grounded();

        _animator.SetBool("jumping", false);
        _jumpState = JumpState.Grounded;
    }

    private void HandleAttackingState()
    {
        if (_currentAttack > Attack.None)
        {
            float currentAnimationCompletion = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (currentAnimationCompletion >= 1.0F && !_animator.IsInTransition(0))
            {
                _currentAttack = Attack.None;
                _animator.SetInteger("attackType", (int)_currentAttack);
                _haltMotion = false;
            }
        }
    }

    public override void Die()
    {
        base.Die();
        if (_jumpState == JumpState.PreJump)
        {
            _jumpState = JumpState.Grounded;
        }

        _blocking = false;
    }
}
