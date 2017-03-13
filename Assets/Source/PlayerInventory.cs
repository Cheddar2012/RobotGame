using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    private int _missileCount;
    public int MissileCount
    {
        get
        {
            return _missileCount;
        }

        set
        {
            if (value > _missileCount)
            {
                GetComponent<AudioSource>().Play();
            }
            _missileCount = Mathf.Max(0, value);
        }
    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
