using UnityEngine;
using Unity.Netcode;

public class ProjectileController : NetworkBehaviour
{
    public float speed = 20f; // Velocidade do projétil
    public float lifetime = 5f; // Tempo de vida do projétil antes da destruição
    private float currentLifetime = 0f; // Tempo de vida atual do projétil

    private Rigidbody2D rb;

    private void Start()
    {
        currentLifetime = 0f;
        rb = GetComponent<Rigidbody2D>();

        // Aplica a velocidade inicial ao projétil
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        // Define a velocidade inicial do projétil
        
        // Atualiza o tempo de vida atual do projétil
        currentLifetime += Time.deltaTime;

        // Destroi o projétil se o tempo de vida atual exceder o tempo de vida máximo
        if (currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com outro jogador
        if (other.CompareTag("Player"))
        {
            // Colocar aqui a lógica de dano ou efeitos de colisão, se necessário

            // Destroi o projétil
            Destroy(gameObject);
        }
    }
    */
}
