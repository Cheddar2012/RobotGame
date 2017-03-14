using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : CharacterHealth, IExplodingObject
{
    [SerializeField]
    private GameObject _explosionParticles;

    [SerializeField]
    private Text _hitPointsCount;

    protected override void Start()
    {
        ++GameManager.Instance.Objectives;
        _hitPointsCount.text = _hitPoints.ToString();
    }

    protected override void Update()
    {

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _hitPointsCount.text = _hitPoints.ToString();
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