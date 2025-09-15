using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    [Header("어디로 보낼지")]
    public Transform target;            // Spawn_Start 같은 목적지
    public string playerTag = "Player"; // MouseRoot 태그

    [Header("옵션")]
    public bool keepYVelocity = false;  // 점프 중 Y속도 유지할지
    public bool oneShot = false;        // 한 번만 작동시키고 싶으면 체크

    BoxCollider col;

    void Awake()
    {
        col = GetComponent<BoxCollider>();
        if (col && !col.isTrigger) col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag) || target == null) return;

        // 플레이어의 Rigidbody가 있으면 물리적으로 이동
        var rb = other.attachedRigidbody;

        if (rb)
        {
            // 텔레포트 직전 속도 정리 (원하면 Y만 유지)
            rb.linearVelocity = keepYVelocity ? new Vector3(0f, rb.linearVelocity.y, 0f) : Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 위치/회전 이동
            rb.position = target.position;
            rb.rotation = target.rotation;
        }
        else
        {
            // 혹시 Rigidbody가 없다면 Transform로 이동
            other.transform.SetPositionAndRotation(target.position, target.rotation);
        }

        if (oneShot && col) col.enabled = false; // 재진입 방지(선택)
    }
}
