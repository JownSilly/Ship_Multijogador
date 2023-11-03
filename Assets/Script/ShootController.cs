using UnityEngine;
using Unity.Netcode;

public class ShootController : NetworkBehaviour
{
    private Rigidbody2D rb;
    public float damage = 10f;
    private void Start()
    {
        if (IsServer)
            Invoke(nameof(DestroyBullet), 2f);
    }

    private void FixedUpdate()
    {
        if (IsServer)
            transform.right = GetComponent<Rigidbody2D>().velocity;
    }
    public void DestroyBullet()
    {
        if (!NetworkObject.IsSpawned)return;
        
        NetworkObject.Despawn();
    }
    public void SetVelocity(Vector2 velocity)
    {
        if (IsServer)
        {
            var bulletRb = GetComponent<Rigidbody2D>();
            bulletRb.velocity = velocity;
            SetVelocityClientRpc(velocity);
        }
            
    }
    [ClientRpc]
    void SetVelocityClientRpc(Vector2 velocity)
    {
        if (!IsHost)
        {
            var bulletRb = GetComponent<Rigidbody2D>();
            bulletRb.velocity = velocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsServer)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null)
            {
              
                player.ApplyDamage(damage);
                DestroyBullet();
            }
        }
            
        
    }
    
}
