using UnityEngine;

public class ExplosionHitBox : AttackHitBox
{

    protected override void Start ()
    {
        base.Start();
        float durationAlive = GetComponent<ParticleSystem>().duration;
        Invoke("OnExpire", durationAlive);
	}
	
	protected override void Update ()
    {
        base.Update();
	}

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    void OnExpire()
    {
        Destroy(gameObject);
    }
}
