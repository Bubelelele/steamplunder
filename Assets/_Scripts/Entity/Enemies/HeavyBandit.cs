using UnityEngine;

public class HeavyBandit : BanditBase
{
    [Header("References")]
    public GameObject leftGauntlet;

    [HideInInspector] public bool lethal = false;

    private float distanceAttack = 3f;
    private float distanceChase;
    private bool canStun;
    private bool attackInvoked;
    private Collider weaponTrigger;

    protected override void Initialize()
    {
        weaponTrigger = leftGauntlet.GetComponent<Collider>();
    }
    protected override void UpdateSense()
    {
        Debug.Log(animationPlaying);
        distanceChase = distanceAttack + 1.5f;
        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            Debug.Log("yes");
            _navMeshAgent.SetDestination(player.transform.position);
            enemyAnim.SetBool("Walking", true);
            if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack)
            {
                enemyAnim.SetBool("Blocking", true);
                InAttackRange();
                ChangeSpeed(slowWalkingSpeed);
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceChase && !idle)
            {
                enemyAnim.SetBool("Blocking", false);
                CanMoveToDestination(movementSpeed);
            }
        }
        else if (!chasePlayer && !animationPlaying && goBackHome)
        {
            _navMeshAgent.speed = slowWalkingSpeed;
            _navMeshAgent.SetDestination(homePoint);
            enemyAnim.SetBool("Walking", true);
            if (Vector3.Distance(gameObject.transform.position, homePoint) < 1)
            {
                goBackHome = false;
                enemyAnim.SetBool("Walking", false);
            }
        }
        if (inAttackRange)
        {
            if (!attackInvoked)
            {
                Invoke("Attack", Random.Range(0.1f, 1f));
                attackInvoked = true;
            }
        }

    }
    public void Attack()
    {
        enemyAnim.SetBool("Walking", false);
        enemyAnim.SetInteger("AttackNumber", Random.Range(1, 3));
        _navMeshAgent.SetDestination(transform.position);
        animationPlaying = true;

    }
    public override void Stun()
    {
        
        if (canStun)
        {
            NotLethal();
            rotationSpeed = 0;
            enemyAnim.SetTrigger("Stunned");
            animationPlaying = true;
        }
    }
    private void CanStun()
    {
        canStun = true;
    }
    private void CannotStun()
    {
        canStun = false;
    }
    public void Lethal()
    {
        weaponTrigger.enabled = true;
        lethal = true;
    }
    public void NotLethal()
    {
        weaponTrigger.enabled = false;
        lethal = false;
    }
    private void AnimationDone()
    {
        enemyAnim.SetInteger("AttackNumber", 0);
        animationPlaying = false;
        rotationSpeed = 10;
        attackInvoked = false;
    }

}
