using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = Vector3.zero;
    public Vector2 limits = new Vector2(5, 3);
    [Range(0, 1)]
    public float smoothTime;
    private Vector3 _velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = offset;
        FollowTarget();
    }

    private void LateUpdate()
    {
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -limits.x, limits.x), Mathf.Clamp(transform.localPosition.y, -limits.y, limits.y), transform.position.z);
    }

    void FollowTarget()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(target.localPosition.x, target.localPosition.y + offset.y, transform.localPosition.z), ref _velocity, smoothTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(limits.x, -limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limits.x, -limits.y, transform.position.z), new Vector3(-limits.x, limits.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(limits.x, -limits.y, transform.position.z), new Vector3(limits.x, limits.y, transform.position.z));
    }
}
