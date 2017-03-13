using UnityEngine;

public class PlayerHealth : CharacterHealth, IExplodingObject
{
    [SerializeField]
    private GameObject _explosionParticles;

    protected override void Start()
    {
        base.Start();
    }
	
	protected override void Update ()
    {
        base.Update();
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
