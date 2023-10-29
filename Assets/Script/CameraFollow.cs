using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string targetTag = "Player"; // Tag do jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavização

    private Transform target; // Referência dinâmica para o transform do jogador
    private Vector3 offset; // Distância inicial entre a câmera e o jogador

    private void Start()
    {
        FindPlayer(); // Encontra o jogador no início
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(targetTag); // Encontra o GameObject do jogador pela tag
        if (player != null)
        {
            target = player.transform; // Define o jogador como alvo
            offset = transform.position - target.position; // Calcula a distância inicial entre a câmera e o jogador
        }
        else
        {
            Debug.LogWarning("Player not found with tag " + targetTag); // Avisa se o jogador não for encontrado
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            FindPlayer(); // Encontra o jogador se ele não existir ainda
            return;
        }

        Vector3 desiredPosition = target.position + offset; // Calcula a posição desejada da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suaviza o movimento
        transform.position = smoothedPosition; // Atualiza a posição da câmera
    }
}
