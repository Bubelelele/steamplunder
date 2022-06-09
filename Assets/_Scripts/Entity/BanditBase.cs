using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class BanditBase : EnemyBase {
    
    [Header("Common enemy paramaters")]
    public float movementSpeed;
    public float slowWalkingSpeed;
    public float rotationSpeed;
    public float sightRange;
    public float rangeForStopChasingPlayer;
    public float FOV;
    public int attackDamage;
    public AudioClip[] deathSounds;

    protected GameObject player;
    protected Animator enemyAnim;
    protected Vector3 homePoint;
    protected bool goBackHome;
    protected bool idle;
    protected bool inAttackRange;
    protected bool chasePlayer;
    protected bool animationPlaying;

    private float alertRange = 20;
    private bool playerDetected = false;
    private bool checkedForNerbyEnemies = false;
    private bool calledByNerbyEnemies = false;
    private GameObject alertTrigger;
    private AudioSource _audioSource;
    private static AudioClip _previousDeathSound;

    [Header("Idle patroling")]
    //Idle
    public bool patrol;
    public float patrolRange = 10f;

    private Vector3 randomPos;
    private float timeSinceLastMoved;

    private float idleTurn;
    private float distanceBeforeImidiateDetection = 3;
    private bool moveToPatrolDestination = true;
    private bool randomCheck = false;
    private bool canIdleTurn = false;
    private bool idleTurnedDone = true;
    private bool changeDestinationNumber = true;
    private bool _died;

    protected virtual void UpdateSense() { }
    protected virtual void Initialize() { }

    private void Start()
    {
        Initialize();
        idle = true;
        player = Player.GetPlayer().gameObject;
        enemyAnim = gameObject.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        homePoint = transform.position;
        alertTrigger = transform.GetComponentInChildren<AlertTrigger>().gameObject;
    }

    

    private void Update() {
        if (_died) return;
        UpdateSense();
        Idle();
        timeSinceLastMoved += Time.deltaTime;

        //Idle turn
        if (canIdleTurn)
        {
            transform.Rotate(Vector3.up * idleTurn * Time.deltaTime);
        }

        if (!calledByNerbyEnemies)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < sightRange && Vector3.Angle(transform.forward, player.transform.position - transform.position) < FOV / 2)
            {
                EnemyInSight();
                playerDetected = true;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < distanceBeforeImidiateDetection)
            {
                EnemyInSight();
                playerDetected = true;
            }
            else if(playerDetected)
            {
                EnemyOutOfSight();
                goBackHome = true;
                idle = true;
                playerDetected = false;
                checkedForNerbyEnemies = false;
            }
        }
        if (Vector3.Distance(player.transform.position, transform.position) > rangeForStopChasingPlayer)
        {
            calledByNerbyEnemies = false;
            checkedForNerbyEnemies = false;
            playerDetected = false;
            alertTrigger.transform.localScale = Vector3.one;
            idle = true;
            EnemyOutOfSight();

        }


        if (playerDetected)
        {
            PlayerDetectedCommon();
            if (Vector3.Distance(player.transform.position, transform.position) < sightRange && Vector3.Angle(transform.forward, player.transform.position - transform.position) < FOV / 2)
            {
                EnemyInSight();
            }
        }
    }
    public void AwareOfPlayer()
    {
        playerDetected = true;
        calledByNerbyEnemies = true;
    }
    public void PlayerDetectedCommon()
    {
        idle = false;
        var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (!checkedForNerbyEnemies)
        {
            alertTrigger.transform.localScale = Vector3.Slerp(alertTrigger.transform.localScale, Vector3.one * alertRange, 0.6f * Time.deltaTime);
            if (alertTrigger.transform.localScale.magnitude >= alertRange)
            {
                checkedForNerbyEnemies = true;
            }
        }
    }
    public void Idle()
    {
        if (idle && !goBackHome)
        {
            enemyAnim.SetBool("Idle", true);
            if (!patrol)
            {
                if (idleTurnedDone)
                {
                    Invoke("TurnWhileIdle", Random.Range(4, 10));
                    Invoke("PlayIdleAnimation", Random.Range(2, 6));
                    idleTurnedDone = false;
                }
            }
            else if (patrol)
            {

                if (moveToPatrolDestination)
                {
                    if (!randomCheck)
                    {
                        timeSinceLastMoved = 0;
                        Vector3 randomDestination = Random.insideUnitSphere * patrolRange;
                        randomDestination += homePoint;
                        NavMeshHit hit;
                        NavMesh.SamplePosition(randomDestination, out hit, patrolRange, 1);
                        randomPos = hit.position;
                        randomCheck = true;
                    }
                    enemyAnim.SetBool("Walking", true);
                    CanMoveToDestination(slowWalkingSpeed);
                    _navMeshAgent.SetDestination(randomPos);
                    var targetRotation = Quaternion.LookRotation(new Vector3(randomPos.x, transform.position.y, randomPos.z) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                    if (Vector3.Distance(transform.position, randomPos) < 1 || timeSinceLastMoved > 4) { moveToPatrolDestination = false; }
                }
                else if (!moveToPatrolDestination)
                {

                    StopMovingToDestination();
                    enemyAnim.SetBool("Walking", false);
                    if (changeDestinationNumber)
                    {
                        Invoke("NextDestination", Random.Range(4, 11));
                        Invoke("PlayIdleAnimation", Random.Range(2, 5));
                        changeDestinationNumber = false;
                    }
                }
            }
        }
        else
        {
            enemyAnim.SetBool("Idle", false);
        }
    }
    public void EnemyInSight() { chasePlayer = true; distanceBeforeImidiateDetection = rangeForStopChasingPlayer; }
    public void EnemyOutOfSight() { chasePlayer = false; idle = true; distanceBeforeImidiateDetection = 3; }
    public void InAttackRange() { inAttackRange = true; }
    public void StopMovingToDestination() { _navMeshAgent.isStopped = true;}
    public void CanMoveToDestination(float speed)
    {
        _navMeshAgent.isStopped = false;
        inAttackRange = false;
        _navMeshAgent.speed = speed;
    }
    public void ChangeSpeed(float speed)
    {
        _navMeshAgent.speed = speed;
    }

    private void NextDestination()
    {
        moveToPatrolDestination = true;
        randomCheck = false;
        changeDestinationNumber = true;
    }
    private void TurnWhileIdle()
    {
        int chooseDir = Random.Range(0, 2);
        if (chooseDir == 0)
        {
            idleTurn = -Random.Range(30, 180);
        }
        else
        {
            idleTurn = Random.Range(30, 180);
        }

        canIdleTurn = true;
        Invoke("CannotIdleTurn", 0.7f);
    }
    private void CannotIdleTurn()
    {
        canIdleTurn = false;
        idleTurnedDone = true;
    }
    private void PlayIdleAnimation()
    {
        enemyAnim.SetInteger("IdleNumber", Random.Range(1, 4));
    }
    private void IdleAnimationDone()
    {
        enemyAnim.SetInteger("IdleNumber", 0);
    }

    protected override void Die() {
        base.Die();
        _died = true;
        CancelInvoke();
        GetComponent<Collider>().enabled = false;
        enemyAnim.SetTrigger("Die");
        
        //death sound
        if (_audioSource == null || deathSounds.Length == 0) return;
        AudioClip deathSoundToPlay;
        do deathSoundToPlay = deathSounds[Random.Range(0, deathSounds.Length)];
        while (deathSoundToPlay == _previousDeathSound);
        
        _audioSource.clip = deathSoundToPlay;
        _audioSource.Play();
        _previousDeathSound = deathSoundToPlay;
    }

    private void Destroy() => Destroy(gameObject);
}
