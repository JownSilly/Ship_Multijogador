using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float decelerationRate = 2f;

    private PlayerInputHandler inputHandler;
    private Rigidbody2D rb;
    private float movementInputValue;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        movementInputValue = -inputHandler.movementInput.y;
        UpdateSpeed();
        Move();
        Turn();
    }

    private void UpdateSpeed()
    {
        if (movementInputValue == 0 && currentSpeed != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decelerationRate * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed * movementInputValue, Time.deltaTime * (moveSpeed / 2));
        }
    }

    private void Move()
    {
        Vector2 movement = transform.up * currentSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void Turn()
    {
        float turn = inputHandler.movementInput.x;
        float turnAngle = -turn * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + turnAngle);
    }
}
