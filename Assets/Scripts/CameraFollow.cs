using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    private float y;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;//target is at 0,0,0
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followPosition = target.position + offset;
        RaycastHit hit;
        if (Physics.Raycast(target.position, Vector3.down, out hit, 10f))
        {
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * speed);
        } else {
            y = Mathf.Lerp(y, target.position.y, Time.deltaTime * speed);
        }
        followPosition.y = y+offset.y;
        transform.position = followPosition;
    }
}
