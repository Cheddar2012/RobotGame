using UnityEngine;
using System.Collections;

public class MissileHitBox : AttackHitBox, IExplodingObject
{
    [SerializeField]
    private float _startSpeed = 4.0F;

    [SerializeField]
    private float _maxSpeed = 25.0F;

    [SerializeField]
    private float _acceleration = 10.0F;

    [SerializeField]
    private float _durationAlive = 5.0F;

    private float _currentSpeed;
    private float _elapsedTime;
    
    [SerializeField]
    private GameObject _explosionParticles;


	protected override void Start ()
    {
        base.Start();
        _currentSpeed = _startSpeed;
        _elapsedTime = 0;
        Invoke("OnExpire", _durationAlive);
    }
	
	protected override void Update ()
    {
        base.Update();
	}

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;
        _currentSpeed = Mathf.Min(_maxSpeed, (_startSpeed + (_elapsedTime * _acceleration)));
        GetComponent<Rigidbody>().velocity = transform.forward * _currentSpeed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            IExplodingObject missile = this;
            missile.OnExplode();
        }
    }

    void OnExpire()
    {
        IExplodingObject missile = this;
        missile.OnExplode();
    }

    void IExplodingObject.OnExplode()
    {
        GameObject explosion = Instantiate<GameObject>(_explosionParticles);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
