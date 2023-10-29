using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform leftFirePoint; // Ponto de origem do disparo para a esquerda
    public Transform rightFirePoint; // Ponto de origem do disparo para a direita
    public float fireRate = 0.5f; // Cadência de tiros ajustável para o lado direito
    private float nextLeftFireTime = 0f; // Momento do próximo disparo para o lado esquerdo
    private float nextRightFireTime = 0f; // Momento do próximo disparo para o lado direito

    private PlayerInputHandler inputHandler;

    private void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        if (inputHandler.LeftFired && Time.time >= nextLeftFireTime)
        {
            FireProjectile(leftFirePoint,ref nextLeftFireTime); // Dispara para a esquerda
        }
        if (inputHandler.RightFired && Time.time >= nextRightFireTime)
        {
            FireProjectile(rightFirePoint, ref nextRightFireTime); // Dispara para a direita
        }
    }

    private void FireProjectile(Transform firePoint,  ref float nextFireTime)
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        nextFireTime = Time.time + 1f / fireRate; // Atualiza o momento do próximo disparo com base na cadência
    }
}
