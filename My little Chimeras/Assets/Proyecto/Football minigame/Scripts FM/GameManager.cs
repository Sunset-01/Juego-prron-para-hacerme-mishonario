////using UnityEngine;
////using TMPro;

////public class GameManager : MonoBehaviour
////{
////    public static GameManager Instance;

////    [Header("Score")]
////    public int scoreTeamA;
////    public int scoreTeamB;

////    [Header("UI")]
////    public TextMeshProUGUI scoreText;

////    [Header("Ball")]
////    public Rigidbody ballRB;
////    public Transform ballSpawnPoint;

////    private void Awake()
////    {
////        if (Instance == null)
////            Instance = this;
////        else
////            Destroy(gameObject);
////    }

////    void Start()
////    {
////        UpdateUI();
////    }

////    public void GoalTeamA()
////    {
////        scoreTeamA++;
////        ResetBall();
////        UpdateUI();
////    }

////    public void GoalTeamB()
////    {
////        scoreTeamB++;
////        ResetBall();
////        UpdateUI();
////    }

////    void ResetBall()
////    {
////        ballRB.linearVelocity = Vector3.zero;
////        ballRB.angularVelocity = Vector3.zero;
////        ballRB.transform.position = ballSpawnPoint.position;
////    }

////    void UpdateUI()
////    {
////        scoreText.text = scoreTeamA + "  -  " + scoreTeamB;
////    }
////}

//using System.Collections;
//using UnityEngine;
//using TMPro;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    [Header("Score")]
//    public int scoreTeamA;
//    public int scoreTeamB;

//    [Header("UI")]
//    public TextMeshProUGUI scoreText;

//    [Header("Ball")]
//    public Rigidbody ballRB;
//    public Transform[] ballSpawnPoints;

//    [Header("Goal Pause")]
//    public float goalPauseTime = 2f;

//    bool isResetting;

//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    void Start()
//    {
//        UpdateUI();
//    }

//    public void GoalTeamA()
//    {
//        if (isResetting) return;
//        scoreTeamA++;
//        StartCoroutine(HandleGoal());
//    }

//    public void GoalTeamB()
//    {
//        if (isResetting) return;
//        scoreTeamB++;
//        StartCoroutine(HandleGoal());
//    }

//    IEnumerator HandleGoal()
//    {
//        isResetting = true;

//        // Congelar pelota
//        ballRB.linearVelocity = Vector3.zero;
//        ballRB.angularVelocity = Vector3.zero;
//        ballRB.isKinematic = true;

//        UpdateUI();

//        yield return new WaitForSeconds(goalPauseTime);

//        ResetBall();

//        ballRB.isKinematic = false;
//        isResetting = false;
//    }

//    void ResetBall()
//    {
//        Transform spawn = ballSpawnPoints[Random.Range(0, ballSpawnPoints.Length)];
//        ballRB.transform.position = spawn.position;
//    }

//    void UpdateUI()
//    {
//        scoreText.text = scoreTeamA + "  -  " + scoreTeamB;
//    }
//}

using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Team { TeamA, TeamB }

    [Header("Score")]
    public int scoreTeamA;
    public int scoreTeamB;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;

    [Header("Ball")]
    public Rigidbody ballRB;
    public GameObject ballObject;

    [Header("Respawn Points")]
    public Transform[] spawnPointsTeamA;
    public Transform[] spawnPointsTeamB;

    [Header("Goal Settings")]
    public float goalPauseTime = 3f;

    bool isResetting;
    public bool IsResetting => isResetting;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
        countdownText.gameObject.SetActive(false);
    }

    public void GoalScored(Team scoringTeam)
    {
        if (isResetting) return;

        if (scoringTeam == Team.TeamA)
            scoreTeamA++;
        else
            scoreTeamB++;

        StartCoroutine(HandleGoal(scoringTeam));
    }

    IEnumerator HandleGoal(Team scoringTeam)
    {
        isResetting = true;

        // Congelar y ocultar pelota
        ballRB.linearVelocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;
        ballRB.isKinematic = true;
        ballObject.SetActive(false);

        UpdateUI();

        // Cuenta regresiva
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);

        RespawnBall(scoringTeam);

        ballObject.SetActive(true);
        ballRB.isKinematic = false;

        isResetting = false;
    }

    void RespawnBall(Team scoringTeam)
    {
        // Respawn lejos del gol
        Transform[] validSpawns =
            scoringTeam == Team.TeamA ? spawnPointsTeamB : spawnPointsTeamA;

        Transform spawn = validSpawns[Random.Range(0, validSpawns.Length)];
        ballRB.transform.position = spawn.position;
    }

    void UpdateUI()
    {
        scoreText.text = scoreTeamA + "  -  " + scoreTeamB;
    }
}
