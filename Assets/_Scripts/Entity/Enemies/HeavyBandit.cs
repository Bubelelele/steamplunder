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
        Debug.Log(canStun);

        distanceChase = distanceAttack + 1.5f;
        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            _navMeshAgent.SetDestination(player.transform.position);
            enemyAnim.SetBool("Walking", true);
            if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack)
            {
                enemyAnim.SetBool("Blocking", true);
                ChangeSpeed(slowWalkingSpeed);
                if (!attackInvoked)
                {
                    Invoke("Attack", Random.Range(1f, 1.5f));
                    attackInvoked = true;
                }

            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceChase && !idle)
            {
                enemyAnim.SetBool("Blocking", false);
                NotLethal();
                CannotStun();
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

    }
    public void Attack()
    {
        enemyAnim.SetBool("Walking", false);
        enemyAnim.SetInteger("AttackNumber", Random.Range(1, 3));
        _navMeshAgent.SetDestination(transform.position);
        animationPlaying = true;
        rotationSpeed = 0;
    }
    public override void Stun()
    {
        
        if (canStun)
        {
            CancelInvoke();
            NotLethal();
            rotationSpeed = 0;
            enemyAnim.SetTrigger("Stunned");
            enemyAnim.SetBool("Blocking", false);
            enemyAnim.SetBool("Walking", false);
            enemyAnim.SetInteger("AttackNumber", 0);
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
        rotationSpeed = 10;
        attackInvoked = false;
        animationPlaying = false;
    }
    public override void Hit(int damage, Artifact source)
    {
        if (canStun) damage = 0;
        base.Hit(damage, source);
    }

}
