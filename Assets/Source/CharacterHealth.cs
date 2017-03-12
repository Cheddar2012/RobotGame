using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour, IDestructibleObject
{
    [SerializeField]
    private int _hitPoints = 1;

    protected virtual void Start() { }

    protected virtual void Update() { }

    void IDestructibleObject.TakeDamage(int damage)
    {
        _hitPoints -= damage;
        if (_hitPoints <= 0)
        {
            IDestructibleObject obj = this;
            obj.DestroyObject();
        }
    }

    void IDestructibleObject.DestroyObject()
    {
        OnCharacterDeath();
    }

    protected abstract void OnCharacterDeath();
}
