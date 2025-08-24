using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isMoving = false;
    private Collider2D currentTargetCollider;
    private Action onReachTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isMoving && currentTargetCollider != null)
        {
            Vector2 targetPos = currentTargetCollider.bounds.center;

            // Si ya estoy MUY cerca del destino → considerar que llegué
            if (Vector2.Distance(rb.position, targetPos) < 0.05f)
            {
                StopMovement();
                return;
            }

            moveDirection = (targetPos - rb.position).normalized;
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void MoveToTarget(Vector3 targetPosition, Collider2D targetCollider, Action callback)
    {
        // 🔑 Si ya estoy dentro del collider → no mover, ejecutar callback directo
        if (targetCollider.bounds.Contains(rb.position))
        {
            callback?.Invoke();
            return;
        }

        currentTargetCollider = targetCollider;
        onReachTarget = callback;
        moveDirection = ((Vector2)targetPosition - rb.position).normalized;
        isMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == currentTargetCollider)
        {
            StopMovement();
        }
    }

    private void StopMovement()
    {
        isMoving = false;
        moveDirection = Vector2.zero;
        onReachTarget?.Invoke();
        currentTargetCollider = null;
    }
}