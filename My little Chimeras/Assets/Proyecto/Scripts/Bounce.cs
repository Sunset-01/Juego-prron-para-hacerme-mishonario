using UnityEngine;

public class BounceObject : MonoBehaviour
{
    [Header("Configuración del rebote")]
    [Tooltip("Qué tan fuerte rebotará el objeto que toque este collider")]
    public float bounceForce = 10f;

    [Tooltip("Opcional: Si quieres aplicar el rebote solo a objetos con cierto tag")]
    public string targetTag = "Player";

    [Tooltip("Si está activado, el rebote depende del ángulo de la colisión")]
    public bool useCollisionNormal = true;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisionó tiene Rigidbody
        Rigidbody rb = collision.rigidbody;
        if (rb == null) return;

        // Si se especificó un tag y no coincide, no hace nada
        if (!string.IsNullOrEmpty(targetTag) && !collision.gameObject.CompareTag(targetTag))
            return;

        // Calcula dirección del rebote
        Vector3 bounceDir = useCollisionNormal
            ? collision.contacts[0].normal  // Rebota según la normal de la superficie
            : Vector3.up;                   // Rebota siempre hacia arriba (opcional)

        // Aplica la fuerza de rebote
        rb.AddForce(bounceDir * bounceForce, ForceMode.Impulse);
    }
}
