using UnityEngine;

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

    private bool _exploded;

	protected override void Start ()
    {
        base.Start();
        _currentSpeed = _startSpeed;
        _elapsedTime = 0;
        _exploded = false;
        Invoke("OnExplode", _durationAlive);
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
        OnExplode();
    }

    public void OnExplode()
    {
        if (!_exploded)
        {
            _exploded = true;

            GameObject explosion = Instantiate<GameObject>(_explosionParticles);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
