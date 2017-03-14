using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private Vector3 _lastTargetPosition;

    private void Start()
    {
        _lastTargetPosition = _target.position;
    }
    
	private void Update()
    {
        Vector3 targetPositionChange = _target.position - _lastTargetPosition;
        transform.position += new Vector3(targetPositionChange.x, targetPositionChange.y, 0);
        _lastTargetPosition = _target.position;
    }
}
