using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 跟随的目标
    Transform trackTarget;
    // 摄像机相对于角色的偏移
    public float xOffset;
    public float yOffset;
    protected float followSpeed;
    // 方向锁定
    protected bool isXLocked = false;
    protected bool isYLocked = false;
    void Start()
    {
        trackTarget = GameObject.Find("SnakeHead").transform;
        //followSpeed = 1.0f;
        transform.position = new Vector3(trackTarget.position.x, trackTarget.position.y,-100);
    }

    void Update()
    {
        float xNew = transform.position.x;
        if (!isXLocked)
        {
            float xTarget = trackTarget.position.x + xOffset;
            // 使相机可以平滑移动
            xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
        }
        float yNew = transform.position.y;
        if (!isYLocked)
        {
            float yTarget = trackTarget.position.y + yOffset;
            yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed);
        }
        transform.position = new Vector3(trackTarget.position.x, trackTarget.position.y,transform.position.z);
    }
}
