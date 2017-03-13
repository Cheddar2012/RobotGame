using UnityEngine;

public class PlayerFistHitBox : AttackHitBox
{
    // The type of attack that must be active for this attack hit box to deal damage
    [SerializeField]
    private PlayerMotion.Attack _attackType;

    private PlayerMotion _motion;

    protected override void Start()
    {
        base.Start();
        _motion = GameManager.Instance.GetPlayer().GetComponent<PlayerMotion>();
    }

    protected override void Update()
    {
        base.Update();
        
        // When an attack completes, make sure to clear the list of items that have been hit
        if (_motion.CurrentAttack != _attackType)
        {
            if (_hitObjects.Count > 0)
            {
                _hitObjects.Clear();
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // Only apply damage if the current attack matches with the attack type of this hit box
        if (_motion.CurrentAttack == _attackType)
        {
            base.OnTriggerEnter(other);
        }
    }
}
