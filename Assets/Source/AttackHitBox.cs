using UnityEngine;
using System.Collections.Generic;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField]
    private int _attackPower = 1;

    // The list of objects that have been hit by this attack
    protected HashSet<Collider> _hitObjects;


    protected virtual void Start ()
    {
        _hitObjects = new HashSet<Collider>();
    }
	
	protected virtual void Update () { }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Pickup")
        {
            IDestructibleObject target = other.GetComponent<IDestructibleObject>();
            if (target != null)
            {
                // If the collider is not already in the HashSet, it has not yet been hit during this attack, so proceed with dealing damage
                if (_hitObjects.Add(other))
                {
                    DealDamage(target);
                }
            }
        }
    }

    protected virtual void DealDamage(IDestructibleObject target)
    {
        target.TakeDamage(_attackPower);
    }
}
