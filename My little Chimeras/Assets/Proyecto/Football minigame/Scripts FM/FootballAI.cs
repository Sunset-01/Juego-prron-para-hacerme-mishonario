//using UnityEngine;
//using UnityEngine.AI;

//public class FootballAI : MonoBehaviour
//{
//    //public enum AIState { Idle, ChaseBall, HasBall }
//    public enum AIState
//    {
//        Idle,
//        ChaseBall,
//        CarryBall
//    }


//    [Header("Team")]
//    public GameManager.Team team;

//    [Header("References")]
//    public NavMeshAgent agent;
//    public Rigidbody ballRB;
//    public Transform enemyGoal;

//    [Header("Settings")]
//    public float shootForce = 12f;
//    public float pickUpDistance = 1.5f;
//    public float thinkRate = 0.2f;

//    [Header("Goal")]
//    public Transform goalShootPoint;


//    AIState currentState;
//    float thinkTimer;

//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        currentState = AIState.Idle;
//    }

//    void Update()
//    {
//        if (GameManager.Instance == null) return;

//        thinkTimer -= Time.deltaTime;
//        if (thinkTimer > 0) return;

//        thinkTimer = thinkRate;

//        switch (currentState)
//        {
//            case AIState.Idle:
//                LookForBall();
//                break;

//            case AIState.ChaseBall:
//                ChaseBall();
//                break;

//            case AIState.HasBall:
//                TryShoot();
//                break;

//        }
//    }

//    void LookForBall()
//    {
//        if (ballRB == null) return;

//        currentState = AIState.ChaseBall;
//        agent.SetDestination(ballRB.transform.position);
//    }

//    void ChaseBall()
//    {
//        agent.SetDestination(ballRB.transform.position);

//        float dist = Vector3.Distance(transform.position, ballRB.transform.position);

//        if (dist <= pickUpDistance)
//        {
//            currentState = AIState.HasBall;
//        }
//    }

//    void TryShoot()
//    {
//        float dist = Vector3.Distance(ballRB.transform.position, transform.position);

//        // Si la pelota ya no est� cerca, volver a perseguirla
//        if (dist > pickUpDistance + 0.5f)
//        {
//            currentState = AIState.ChaseBall;
//            return;
//        }

//        agent.ResetPath();

//        Vector3 shootDir =
//            (goalShootPoint.position - ballRB.transform.position).normalized;

//        ballRB.linearVelocity = Vector3.zero;
//        ballRB.angularVelocity = Vector3.zero;
//        ballRB.AddForce(shootDir * shootForce, ForceMode.Impulse);


//        //Vector3 shootDir = (enemyGoal.position - ballRB.transform.position).normalized;

//        //ballRB.linearVelocity = Vector3.zero;
//        //ballRB.angularVelocity = Vector3.zero;
//        //ballRB.AddForce(shootDir * shootForce, ForceMode.Impulse);

//        currentState = AIState.Idle;
//    }

//    //[System.Obsolete]
//    //void TryShoot()
//    //{
//    //    agent.ResetPath();

//    //    Vector3 dir = (enemyGoal.position - transform.position).normalized;

//    //    ballRB.velocity = Vector3.zero;
//    //    ballRB.AddForce(dir * shootForce, ForceMode.Impulse);

//    //    currentState = AIState.Idle;
//    //}
//}

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FootballAI : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        ChaseBall,
        CarryBall
    }

    [Header("Team")]
    public GameManager.Team team;

    [Header("References")]
    public Rigidbody ballRB;

    [Header("Goal Points")]
    public Transform goalApproachPoint;   // Punto frente al arco
    public Transform goalShootPoint;      // Centro del arco

    [Header("Movement")]
    public NavMeshAgent agent;
    public float pickUpDistance = 1.5f;
    public float shootDistance = 6f;

    [Header("Shoot")]
    public float shootForce = 12f;

    [Header("Think")]
    public float thinkRate = 0.1f;

    AIState currentState;
    float thinkTimer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentState = AIState.Idle;
    }

    void Update()
    {
        // Bloquear IA durante pausa de gol
        if (GameManager.Instance != null && GameManager.Instance.IsResetting)
        {
            agent.ResetPath();
            return;
        }

        thinkTimer -= Time.deltaTime;
        if (thinkTimer > 0f) return;
        thinkTimer = thinkRate;

        switch (currentState)
        {
            case AIState.Idle:
                LookForBall();
                break;

            case AIState.ChaseBall:
                ChaseBall();
                break;

            case AIState.CarryBall:
                CarryBall();
                break;
        }
    }

    // ================= STATES =================

    void LookForBall()
    {
        if (ballRB == null) return;

        currentState = AIState.ChaseBall;
        agent.SetDestination(ballRB.transform.position);
    }

    void ChaseBall()
    {
        if (ballRB == null) return;

        agent.SetDestination(ballRB.transform.position);

        float dist = Vector3.Distance(
            transform.position,
            ballRB.transform.position
        );

        if (dist <= pickUpDistance)
        {
            currentState = AIState.CarryBall;
        }
    }

    void CarryBall()
    {
        if (ballRB == null) return;

        // Siempre moverse hacia el arco rival
        agent.SetDestination(goalApproachPoint.position);

        float distToGoal = Vector3.Distance(
            ballRB.transform.position,
            goalShootPoint.position
        );

        if (distToGoal <= shootDistance)
        {
            Shoot();
        }
    }

    // ================= ACTIONS =================

    void Shoot()
    {
        Vector3 shootDir =
            (goalShootPoint.position - ballRB.transform.position).normalized;

        ballRB.linearVelocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;
        ballRB.AddForce(shootDir * shootForce, ForceMode.Impulse);

        currentState = AIState.Idle;
    }
}
