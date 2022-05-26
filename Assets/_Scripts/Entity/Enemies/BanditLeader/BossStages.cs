using UnityEngine;
using UnityEngine.AI;
public class BossStages : MonoBehaviour
{

    [HideInInspector] public bool secondStage = false;
    [SerializeField] private Transform targetLocation;
    [SerializeField] private DungeonDoor door;
    [SerializeField] private DungeonDoor door2;

    private NavMeshAgent agent;
    private AttackScript attackScript;
    private LeaderBandit leaderBandit;

    private void Start()
    {
        attackScript = GetComponent<AttackScript>();
        leaderBandit = GetComponent<LeaderBandit>();
        agent = GetComponent<NavMeshAgent>();
        door2.Open();
    }

    public void Stage2()
    {
        secondStage = true;
        leaderBandit.DeactivateBoss();
        door.Open();
        
    }
    private void Update()
    {
        if (secondStage)
        {
            Invoke("MoveDelay", 1.5f);
            Invoke("DoorClose", 4.5f);

            if (Vector3.Distance(transform.position, targetLocation.position) < 2)
            {
                secondStage = false;
                agent.ResetPath();
                attackScript.LastStage();
            }
        }
    }
    private void MoveDelay()
    {
        agent.SetDestination(targetLocation.position);
    }
    private void DoorClose()
    {
        door2.Close();
    }


}