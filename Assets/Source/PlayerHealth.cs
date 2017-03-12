using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDestructibleObject
{
    [SerializeField]
    private int _hitPoints = 1;

    private PlayerMotion _playerMotion;

    void Start()
    {
        _playerMotion = GameManager.Instance.GetPlayer().GetComponent<PlayerMotion>();
    }
	
	void Update ()
    {
	
	}

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
        _playerMotion.Die();
    }
}
