using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float topPos;
    [SerializeField] private float bottomPos;

    public Transform target;
    [SerializeField] private float offset;
    [SerializeField] private float offsetPositionZ;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        var position = transform.position;
            if (target)
            {
                var targetPosition = target.position;
                transform.position = Vector3.Lerp(position,
                    new Vector3(0, position.y,
                        Mathf.Clamp(targetPosition.z, bottomPos, topPos) - offsetPositionZ), offset * Time.fixedDeltaTime);
            }
    }
    
}
