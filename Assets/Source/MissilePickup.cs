using UnityEngine;

public class MissilePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ++other.GetComponent<PlayerInventory>().MissileCount;
            Destroy(gameObject);
        }
    }
}
