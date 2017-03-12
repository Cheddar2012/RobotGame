using UnityEngine;
using System.Collections.Generic;

public class PlayerFistHitBox : AttackHitBox
{
    // The type of attack that must be active for this attack hit box to deal damage
    [SerializeField]
    private PlayerMotion.Attack _attackType;

    private PlayerMotion _motion;

    // The list of objects that have been hit by this attack
    private HashSet<Collider> _hitObjects;

    protected override void Start()
    {
        base.Start();
        _motion = GameManager.Instance.GetPlayer().GetComponent<PlayerMotion>();
        _hitObjects = new HashSet<Collider>();
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
        if (_motion.CurrentAttack == _attackType)
        {
            // If the collider is not already in the HashSet, it has not yet been hit during this attack, so proceed with dealing damage
            if (_hitObjects.Add(other))
            {
                base.OnTriggerEnter(other);
            }
        }
    }
}
