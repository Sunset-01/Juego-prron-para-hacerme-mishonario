
using UnityEngine;
using UnityEngine.AI;

public class WanderBehaviour : MonoBehaviour
{
    public float wanderRadius = 4f;
    public float wanderDistance = 6f;
    public float wanderJitter = 1f;
    public float wanderDelay = 2f;

    public float stuckDistance = 0.2f;
    public float stuckTimeLimit = 1.5f;

    NavMeshAgent agent;
    NavMeshPath path;
    Vector3 wanderTarget;
    float timer;

    Vector3 lastPosition;
    float stuckTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();

        wanderTarget = Random.insideUnitSphere * wanderRadius;
        wanderTarget.y = 0;

        lastPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        CheckIfStuck();

        if ((timer >= wanderDelay || IsStuck()) && !agent.pathPending)
        {
            Vector3 destination = GetSafeWanderPoint();

            if (agent.CalculatePath(destination, path) &&
                path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(destination);
                timer = 0f;
                stuckTimer = 0f;
            }
        }
    }

    void CheckIfStuck()
    {
        float movedDistance = Vector3.Distance(transform.position, lastPosition);

        if (movedDistance < stuckDistance)
            stuckTimer += Time.deltaTime;
        else
            stuckTimer = 0f;

        lastPosition = transform.position;
    }

    bool IsStuck()
    {
        return stuckTimer >= stuckTimeLimit;
    }

    Vector3 GetSafeWanderPoint()
    {
        for (int i = 0; i < 15; i++)
        {
            // Fuerza un cambio fuerte de dirección si está atorado
            if (IsStuck())
                wanderTarget = Random.insideUnitSphere * wanderRadius;

            wanderTarget += new Vector3(
                Random.Range(-1f, 1f) * wanderJitter,
                0,
                Random.Range(-1f, 1f) * wanderJitter
            );

            wanderTarget = wanderTarget.normalized * wanderRadius;

            Vector3 localTarget = wanderTarget + Vector3.forward * wanderDistance;
            Vector3 worldTarget = transform.TransformPoint(localTarget);

            if (NavMesh.SamplePosition(worldTarget, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
            {
                // 🔥 CLAVE: evitar puntos demasiado cercanos al borde
                if (hit.distance < 0.5f)
                    continue;

                if (agent.CalculatePath(hit.position, path) &&
                    path.status == NavMeshPathStatus.PathComplete)
                {
                    return hit.position;
                }
            }
        }

        // Último recurso: girar 180°
        return transform.position - transform.forward * wanderDistance;
    }

    void OnDrawGizmosSelected()
    {
        // Dibujo del área de wander (esto es seguro)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            transform.position + transform.forward * wanderDistance,
            wanderRadius
        );

        // Protección total
        if (!Application.isPlaying) return;

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent == null) return;

        // Solo dibuja destino si existe
        if (agent.hasPath)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(agent.destination, 0.2f);
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position + transform.forward * wanderDistance, wanderRadius);

    //    Gizmos.color = IsStuck() ? Color.red : Color.blue;
    //    Gizmos.DrawSphere(agent.destination, 0.2f);
    //}
}
