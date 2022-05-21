using UnityEngine;

public class MeleeBandit : BanditBase
{

    //Paramaters to other scripts
    [HideInInspector] public bool lethal = false;

    [Header("Melee parameters")]
    //Movement
    public float pivotSpeed = 20f;
    public float zigZagSpeed = 2f;

    [Header("References")]
    public Transform pivotTrans;
    public Transform zigZagForward;
    public Transform zigZagBackward;


    private float distanceAttack = 3f;
    private float distanceChase;
    private float stepBackSpeed = 5f;
    private int pivotDirection;
    private bool forwardZigZag = false;
    private bool pivot;
    private bool positionChecked;
    private bool moveBack;

    //Sword
    public Renderer swordRenderer;
    public Material swordMat, swordMatLight;


    //Waiting to attack
    private float minWaitBeforeAttack = 1.5f;
    private float maxWaitBeforeAttack = 6.5f;
    private float distanceBeforeImidiateAttack = 1.3f;

    private bool attackInvoked = false;


    //Stunning parameters
    private bool isStunned = false;
    private bool canBeStunned = false;

    protected override void UpdateSense()
    {

        //Distance control
        distanceChase = distanceAttack + 1.5f;

        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            _navMeshAgent.SetDestination(player.transform.position);
            if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack)
            {
                InAttackRange();
                StopMovingToDestination();
                CanPivot();
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceChase && !idle)
            {
                CanMoveToDestination(movementSpeed);
                CannotPivot();
            }
        }
        else if (!chasePlayer && !animationPlaying && goBackHome)
        {
            _navMeshAgent.speed = slowWalkingSpeed;
            _navMeshAgent.SetDestination(homePoint);
            enemyAnim.SetBool("Walking", true);
            if(Vector3.Distance(gameObject.transform.position, homePoint) < 1)
            {
                goBackHome = false;
                enemyAnim.SetBool("Walking", false);
            }
        }

        //Stunning the enemy
        if (Input.GetKeyDown(KeyCode.Space) && canBeStunned && Vector3.Angle(player.transform.forward, player.transform.position - player.transform.position) < 60f && Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            if (!isStunned)
            {
                Stun();
            }
        }

        //In attack range
        if (inAttackRange && !moveBack)
        {
            if (!animationPlaying)
            {
                _navMeshAgent.speed = slowWalkingSpeed;

                //Pivoting around the player
                if (pivot)
                {
                    if (!positionChecked)
                    {
                        pivotTrans.position = player.transform.position;
                        pivotDirection = Random.Range(0, 2);
                        positionChecked = true;
                        Invoke("ChangePivotDirection", Random.Range(minWaitBeforeAttack, maxWaitBeforeAttack - 2.5f));
                        Invoke("ChangePivotDirection", Random.Range(minWaitBeforeAttack + 2.5f, maxWaitBeforeAttack));
                    }

                    pivotTrans.parent = null;
                    transform.parent = pivotTrans;
                    if (pivotDirection == 0)
                    {
                        pivotTrans.Rotate(Vector3.up * pivotSpeed * Time.deltaTime);
                    }
                    else if (pivotDirection == 1)
                    {
                        pivotTrans.Rotate(-Vector3.up * pivotSpeed * Time.deltaTime);
                    }

                    //ZigZag
                    if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack - 1) { forwardZigZag = false; }
                    else if (Vector3.Distance(player.transform.position, transform.position) > distanceAttack + 1) { forwardZigZag = true; }

                    if (!forwardZigZag)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, zigZagBackward.position, zigZagSpeed * Time.deltaTime);
                    }
                    else if (forwardZigZag)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, zigZagForward.position, zigZagSpeed * Time.deltaTime);
                    }


                }


                //Attacking the player
                if (!attackInvoked)
                {
                    Invoke("Attack", Random.Range(minWaitBeforeAttack, maxWaitBeforeAttack));
                    attackInvoked = true;
                }
                else if (Vector3.Distance(transform.position, player.transform.position) < distanceBeforeImidiateAttack)
                {
                    CancelInvoke();
                    Attack();
                }
            }
        }

        //Move back after an attack
        if (moveBack)
        {
            var backedUpPos = transform.position - transform.forward;
            transform.position = Vector3.MoveTowards(transform.position, backedUpPos, stepBackSpeed * Time.deltaTime);
        }
    }
    public void Attack()
    {
        animationPlaying = true;
        enemyAnim.SetInteger("Swing", Random.Range(1, 4));
        CanMoveToDestination(movementSpeed);
        CannotPivot();
    }

    //Other functions
    public override void Stun()
    {
        animationPlaying = true;
        enemyAnim.SetTrigger("Stunned");
        isStunned = true;
        ChangeSwordMat(swordMat);
        lethal = false;
    }
    public void CanBeStunned()
    {
        _navMeshAgent.SetDestination(player.transform.position);
        canBeStunned = true;
        ChangeSwordMat(swordMatLight);
    }
    public void CannnotBeStunned()
    {
        canBeStunned = false;
        ChangeSwordMat(swordMat);
    }
    public void Lethal()
    {
        lethal = true;
    }
    public void NotLethal()
    {
        lethal = false;
    }
    public void ChangeSwordMat(Material newMat)
    {
        var tempMaterials = swordRenderer.materials;
        tempMaterials[2] = newMat;
        swordRenderer.materials = tempMaterials;
    }
    private void CanPivot() 
    { 
        pivot = true; 
        enemyAnim.SetBool("Walking", false); 
    }
    private void CannotPivot()
    {
        pivot = false;
        positionChecked = false;
        transform.parent = null;
        pivotTrans.position = transform.position;
        pivotTrans.parent = transform;
        positionChecked = false;
        enemyAnim.SetBool("Walking", true);
    }
    private void ChangePivotDirection()
    {
        pivotSpeed = -pivotSpeed;
    }
    private void MovingBack()
    {
        moveBack = true;
        Invoke("StopMovingBack", 0.2f);
    }
    private void StopMovingBack()
    {
        moveBack = false;
    }
    private void AnimationDone()
    {
        animationPlaying = false;
        isStunned = false;
        attackInvoked = false;
        enemyAnim.SetInteger("Swing", 0);
        MovingBack();
    }
}