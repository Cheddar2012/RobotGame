using UnityEngine;

public class PlayerHealth : CharacterHealth, IExplodingObject
{
    private PlayerMotion _playerMotion;

    [SerializeField]
    private GameObject _explosionParticles;

    protected override void Start()
    {
        base.Start();
        _playerMotion = GameManager.Instance.GetPlayer().GetComponent<PlayerMotion>();
    }
	
	protected override void Update ()
    {
        base.Update();
    }

    protected override void OnCharacterDeath()
    {
        _playerMotion.Die();
        OnExplode();
        GameManager.Instance.GetUI().ShowDeathMessage();
    }

    public void OnExplode()
    {
        GameObject explosion = Instantiate<GameObject>(_explosionParticles);
        explosion.transform.position = transform.position;
    }
}
