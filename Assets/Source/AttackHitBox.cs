using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField]
    private int _attackPower = 1;

    protected virtual void Start () { }
	
	protected virtual void Update () { }

    protected virtual void OnTriggerEnter(Collider other)
    {
        IDestructibleObject target = other.GetComponent<IDestructibleObject>();
        if (target != null)
        {
            DealDamage(target);
        }
    }

    protected virtual void DealDamage(IDestructibleObject target)
    {
        target.TakeDamage(_attackPower);
    }
}
