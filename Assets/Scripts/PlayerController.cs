using UnityEngine;
using UnityEngine.InputSystem; // 새 Input System

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float turnSpeed = 180f;
    Rigidbody rb;

    void Awake() { rb = GetComponent<Rigidbody>(); }

    void FixedUpdate()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float h = 0f;
        if (kb.aKey.isPressed)          h -= 1f;
        if (kb.leftArrowKey.isPressed)  h -= 1f;
        if (kb.dKey.isPressed)          h += 1f;
        if (kb.rightArrowKey.isPressed) h += 1f;

        float v = 0f;
        if (kb.sKey.isPressed)          v -= 1f;
        if (kb.downArrowKey.isPressed)  v -= 1f;
        if (kb.wKey.isPressed)          v += 1f;
        if (kb.upArrowKey.isPressed)    v += 1f;

        // 동시에 눌러서 2가 되는 것 방지
        h = Mathf.Clamp(h, -1f, 1f);
        v = Mathf.Clamp(v, -1f, 1f);

        // 회전
        transform.Rotate(Vector3.up, h * turnSpeed * Time.fixedDeltaTime);

        // 전진/후진
        Vector3 forward = transform.forward * v * moveSpeed;
        rb.MovePosition(rb.position + forward * Time.fixedDeltaTime);
    }
}
