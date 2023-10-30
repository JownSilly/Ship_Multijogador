using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float decelerationRate = 2f;
    private Vector2 _moveDirection;
    private PlayerInputHandler inputHandler;
    private Rigidbody2D rb;
    private float movementInputValue;
    private float currentSpeed;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) gameObject.GetComponent<PlayerInput>().enabled = false;
    }
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.zero;
        inputHandler = GetComponent<PlayerInputHandler>();
        
        
    }

    [ServerRpc]
    public void MoveServerRpc(Vector2 move)
    {
        _moveDirection = move.normalized;
    }

    private void FixedUpdate()
    {
        if(IsClient)
        Debug.Log("cLIENT" + inputHandler.movementInput);
        if (IsHost)
        {
            Debug.Log("HOST" + inputHandler.movementInput);
        }
        
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
