using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float distanceBeforeAttack = 1.5f;
    public float damping = 20;


    private bool walkToPlayer = true;
    private bool lookAtPlayer = true;
    private Vector3 playerPos;

    private LeaderBandit leaderBandit;

    private void Start()
    {
        leaderBandit = GetComponent<LeaderBandit>();
    }


    void Update()
    {
        playerPos = Player.GetPosition();
        if (leaderBandit.isActive && lookAtPlayer)
        {

            var targetRotation = Quaternion.LookRotation(new Vector3(playerPos.x, transform.position.y, playerPos.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);

            if (Vector3.Distance(transform.position, playerPos) > distanceBeforeAttack && walkToPlayer)
            {
                //Moving towards the player
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, transform.position.y, playerPos.z), movementSpeed * Time.deltaTime);
            }
        }
    }
    public void WalkToPlayer(bool state)
    {
        walkToPlayer = state;
    }
    public void LookAtPlayer(bool state)
    {
        lookAtPlayer = state;
    }
    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }
}

