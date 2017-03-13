using UnityEngine;
using System.Collections;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform _missileSpawnPoint;

    [SerializeField]
    private GameObject _missile;

    // How many seconds to wait before the next missile is fired
    [SerializeField]
    private float _fireDelay = 3.0F;

    [SerializeField]
    private float _maxLineOfSight = 8.0F;

    private float _fireTimer;

    private void Start ()
    {
        _fireTimer = 0.0F;
	}
	
	private void FixedUpdate()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer >= _fireDelay)
        {
            RaycastHit hit;
            if (Physics.Raycast(_missileSpawnPoint.position, transform.forward, out hit, _maxLineOfSight))
            {
                if (hit.collider.tag == "Player")
                {
                    FireMissile();
                }
            }
        }
	}

    private void FireMissile()
    {
        GameObject missile = Instantiate<GameObject>(_missile);
        missile.transform.position = _missileSpawnPoint.position;
        missile.transform.rotation = transform.rotation;
        _fireTimer = 0.0F;
    }
}
