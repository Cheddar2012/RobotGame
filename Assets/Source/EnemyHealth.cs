using UnityEngine;

public class EnemyHealth : CharacterHealth, IExplodingObject
{
    [SerializeField]
    private GameObject _explosionParticles;

    protected override void Start()
    {
        ++GameManager.Instance.Objectives;
    }

    protected override void Update()
    {

    }

    protected override void OnCharacterDeath()
    {
        OnExplode();
        Destroy(gameObject);
        --GameManager.Instance.Objectives;
    }

    public void OnExplode()
    {
        GameObject explosion = Instantiate<GameObject>(_explosionParticles);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}