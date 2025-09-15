using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 1.1f; //2.6f;
    public float height = 1f; //0.6f;
    public float pitch = 0; //12f;
    public float yawOffset = 0f;
    public float posSmooth = 10f;
    public float rotSmooth = 12f;
    public Vector3 lookAtOffset = new Vector3(0f, 1.1f, 0f);

    // ➕ 어깨 쪽으로 치우치기
    public float sideOffset = 0f;   // +값이면 오른쪽, -값이면 왼쪽

    void LateUpdate()
    {
        if (!target) return;

        float yaw = target.eulerAngles.y + yawOffset;
        Quaternion yawRot = Quaternion.Euler(0f, yaw, 0f);

        // 뒤로 distance, 위로 height, 옆으로 sideOffset
        Vector3 desiredPos =
            target.position
          + yawRot * (new Vector3(sideOffset, 0f, -distance))
          + Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position, desiredPos,
            1f - Mathf.Exp(-posSmooth * Time.deltaTime)
        );

        Vector3 lookTarget = target.position + lookAtOffset;
        Quaternion desiredRot = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, desiredRot,
            1f - Mathf.Exp(-rotSmooth * Time.deltaTime)
        );
    }
}
