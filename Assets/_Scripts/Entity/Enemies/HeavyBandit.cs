using UnityEngine;

public class HeavyBandit : BanditBase
{
    [Header("References")]
    public GameObject leftGauntlet;
    public GameObject rightGauntlet;

    [HideInInspector] public bool lethal = false;

    private float distanceAttack = 3f;
    private float distanceChase;
    private bool canStun;
    private bool isStunned;
    private bool attackInvoked;
    private Collider weaponTrigger;
    private CapsuleCollider _collider;

    protected override void Initialize()
    {
        weaponTrigger = leftGauntlet.GetComponent<Collider>();
        _collider = GetComponent<CapsuleCollider>();
    }
    protected override void UpdateSense()
    {
        Debug.Log(canStun);
        distanceChase = distanceAttack + 1.5f;
        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            if(Vector3.Distance(player.transform.position, transform.position) > _collider.radius + 0.6)
            {
                _navMeshAgent.SetDestination(player.transform.position);
                enemyAnim.SetBool("Walking", true);
            }
            else
            {
                _navMeshAgent.SetDestination(transform.position);
                enemyAnim.SetBool("Walking", false);

            }

            if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack)
            {
                if (!isStunned)
                {
                    enemyAnim.SetBool("Blocking", true);
                    //CanStun();
                }
                
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
                animationPlaying = false;
                NotLethal();
                //CannotStun();
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
        if (isStunned)
        {
            rotationSpeed = 0;
            movementSpeed = 0;
        }

    }
    public void Attack()
    {
        CannotStun();
        enemyAnim.SetBool("Walking", false);
        enemyAnim.SetInteger("AttackNumber", Random.Range(1, 3));
        _navMeshAgent.SetDestination(transform.position);
        animationPlaying = true;
        rotationSpeed = 0;
    }
    public override void Stun()
    {
        Debug.Log("lol");
        if (canStun)
        {
            isStunned = true;
            CancelInvoke();
            NotLethal();
            CannotStun();
            
            enemyAnim.SetBool("Blocking", false);
            enemyAnim.SetTrigger("Stunned");
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
    private void RaisedUp()
    {
        isStunned = false;
    }
    private void AnimationDone()
    {
        movementSpeed = 4;
        rotationSpeed = 10;
        enemyAnim.SetInteger("AttackNumber", 0);
        attackInvoked = false;
        animationPlaying = false;
    }
    public override void Hit(int damage, Artifact source)
    {
        if (canStun)
        {
            damage = 0;
            EffectSpawner.SpawnSparksFX(rightGauntlet.transform.position + Vector3.up * 0.5f);
        }
        
        base.Hit(damage, source);
    }

}
