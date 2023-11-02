using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D rb;
    private Vector2 _moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.zero;
        if (IsLocalPlayer)
        {
            GetComponent<PlayerInput>().enabled = true;
            _inputHandler = GetComponent<PlayerInputHandler>();
        }
    }
    void Update()
    {
        if (IsClient)
            if (!IsLocalPlayer) return;
            if (_inputHandler.movementInput != _moveDirection)
            _moveDirection = _inputHandler.movementInput;
            MoveServerRpc(_moveDirection);
    }
    [ServerRpc]
    public void MoveServerRpc(Vector2 move)
    {
        _moveDirection = move.normalized;
    }
    private void FixedUpdate()
    {
        if (IsServer)
        {
            float moveInput = -_moveDirection.y;
            float rotateInput = _moveDirection.x;

            // Rotacionar o barco
            if (rotateInput != 0)
            {

                float rotation = rotateInput * rotationSpeed * Time.fixedDeltaTime;
                transform.Rotate(0, 0, -rotation);
            }

            // Mover o barco para frente ou para trás
            Vector2 movement = transform.up * moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        
    }
}
