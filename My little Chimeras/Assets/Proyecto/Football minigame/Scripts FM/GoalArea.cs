////using UnityEngine;

////public class GoalArea : MonoBehaviour
////{
////    public enum Team { TeamA, TeamB }
////    public Team goalFor;

////    private void OnTriggerEnter(Collider other)
////    {
////        if (!other.CompareTag("Ball")) return;

////        if (goalFor == Team.TeamA)
////            GameManager.Instance.GoalTeamA();
////        else
////            GameManager.Instance.GoalTeamB();
////    }
////}

//using UnityEngine;

//public class GoalArea : MonoBehaviour
//{
//    public enum Team { TeamA, TeamB }
//    public Team goalFor;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (!other.CompareTag("Ball")) return;

//        if (goalFor == Team.TeamA)
//            GameManager.Instance.GoalTeamA();
//        else
//            GameManager.Instance.GoalTeamB();
//    }
//}

using UnityEngine;

public class GoalArea : MonoBehaviour
{
    public GameManager.Team goalFor;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        GameManager.Instance.GoalScored(goalFor);
    }
}
