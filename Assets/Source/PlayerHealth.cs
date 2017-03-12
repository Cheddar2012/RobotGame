using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    private PlayerMotion _playerMotion;

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
    }
}
