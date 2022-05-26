using UnityEngine;
using UnityEngine.AI;
public class BossStages : MonoBehaviour
{

    [HideInInspector] public bool secondStage = false;


    [SerializeField] private Transform[] bossLocation;
    [SerializeField] private Transform targetLocation;

    private NavMeshAgent agent;
    private AttackScript attackScript;
    private LeaderBandit leaderBandit;

    private void Start()
    {
        attackScript = GetComponent<AttackScript>();
        leaderBandit = GetComponent<LeaderBandit>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Stage2()
    {
        secondStage = true;
        agent.enabled = true;
        leaderBandit.DeactivateBoss();
    }
    private void Update()
    {
        if (secondStage)
        {
            targetLocation.position = bossLocation[0].position;
            agent.SetDestination(targetLocation.position);


            if (Vector3.Distance(transform.position, targetLocation.position) < 2)
            {
                secondStage = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                attackScript.LastStage();
            }
        }
    }


}