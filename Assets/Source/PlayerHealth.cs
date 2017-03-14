using UnityEngine;

public class PlayerHealth : CharacterHealth, IExplodingObject
{
    [SerializeField]
    private GameObject _explosionParticles;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.GetUI().HitPointsCount = _hitPoints;
    }
	
	protected override void Update ()
    {
        base.Update();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameManager.Instance.GetUI().HitPointsCount = _hitPoints;
    }

    protected override void OnCharacterDeath()
    {
        OnExplode();
        GameManager.Instance.OnPlayerDead();
    }

    public void OnExplode()
    {
        GameObject explosion = Instantiate(_explosionParticles);
        explosion.transform.position = transform.position;
    }
}
