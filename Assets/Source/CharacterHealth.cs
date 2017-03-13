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
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        OnCharacterDeath();
    }

    protected abstract void OnCharacterDeath();
    
    public bool IsDead()
    {
        return _hitPoints <= 0;
    }
}
