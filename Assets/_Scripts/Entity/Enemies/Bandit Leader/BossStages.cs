using UnityEngine;
using UnityEngine.AI;
public class BossStages : MonoBehaviour
{

    [HideInInspector] public bool secondStage = false;
    [SerializeField] private Transform targetLocation;
    [SerializeField] private DungeonDoor door;
    [SerializeField] private DungeonDoor door2;

    private bool invokedOnce;
    private NavMeshAgent agent;
    private AttackScript attackScript;
    private LeaderBandit leaderBandit;
    private BossMovement bossMovement;

    private void Start()
    {
        attackScript = GetComponent<AttackScript>();
        leaderBandit = GetComponent<LeaderBandit>();
        bossMovement = GetComponent<BossMovement>();
        agent = GetComponent<NavMeshAgent>();
        door2.Open();
    }

    public void Stage2()
    {
        secondStage = true;
        leaderBandit.DeactivateBoss();
        door.Open();
        bossMovement.WalkToPlayer(false);
        
    }
    private void Update()
    {
        if (secondStage)
        {
            if (!invokedOnce)
            {
                Invoke("MoveDelay", 1.5f);
                Invoke("DoorClose", 4.5f);
                invokedOnce = true;
            }


            if (Vector3.Distance(transform.position, targetLocation.position) < 2)
            {
                secondStage = false;
                agent.ResetPath();
                attackScript.LastStage();
                bossMovement.WalkToPlayer(false);
            }
        }
    }
    private void MoveDelay()
    {
        bossMovement.WalkToPlayer(true);
        agent.SetDestination(targetLocation.position);
    }
    private void DoorClose()
    {
        door2.Close();
    }


}