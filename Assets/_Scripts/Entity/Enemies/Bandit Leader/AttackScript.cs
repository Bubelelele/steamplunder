using UnityEngine;
using Random = UnityEngine.Random;

public class AttackScript : MonoBehaviour
{
    [HideInInspector] public int attackDamage;
    [HideInInspector] public bool lethal = false;
    [HideInInspector] public bool lastStage = false;

    public Collider gauntletCollider;

    //Gun
    [SerializeField] private GameObject gearBoomerang;


    [SerializeField] private GameObject gearGauntlet;
    [SerializeField] private GameObject boomerangStuff;


    private bool animationIsPlaying = false;
    private bool isCharging = false;
    private bool canBeStunned = false;

    private BossMovement bossMovement;
    private LeaderBandit leaderBandit;
    private Animator bossAnim;

    private void Start()
    {
        bossMovement = GetComponent<BossMovement>();
        leaderBandit = GetComponent<LeaderBandit>();
        bossAnim = GetComponent<Animator>();
    }



    private void Update()
    {
        if (leaderBandit.isActive)
        {
            if (lastStage)
            {
                boomerangStuff.SetActive(true);
            }
            //Shooting and charging
            float dist = Vector3.Distance(transform.position, Player.GetPosition());

            if (dist > 6f && !animationIsPlaying)
            {
                bossMovement.WalkToPlayer(false);
                animationIsPlaying = true;
                if (!lastStage)
                {
                    bossAnim.SetBool("Charge", true);
                }
                else
                {
                    //Shoot gear
                    bossAnim.SetTrigger("ShootGear");
                    bossAnim.SetBool("Charge", true);
                    attackDamage = 7;
                }
            }


            //If within the range of the player
            if (dist < bossMovement.distanceBeforeAttack)
            {
                if (isCharging)
                {
                    //Charge at the player
                    attackDamage = 10;
                    bossMovement.WalkToPlayer(false);
                    bossMovement.LookAtPlayer(false);
                    bossAnim.SetBool("Charge", false);
                }

                if (!animationIsPlaying)
                {
                    int whichAttack = Random.Range(0, 6);

                    if (whichAttack == 0) //Slash three times
                    {
                        bossMovement.SetSpeed(1.5f);
                        bossAnim.SetTrigger("SlashSpree");
                        attackDamage = 7;
                    }
                    else if (whichAttack == 1) //Single slash
                    {
                        bossMovement.SetSpeed(1.5f);
                        bossAnim.SetTrigger("SingleSlash");
                        attackDamage = 7;
                    }
                    else if (whichAttack == 2 || whichAttack == 3)
                    {
                        if (!lastStage) // Punch
                        {
                            Punch();
                            attackDamage = 5;
                        }
                        else // Gearpunch
                        {
                            Punch();
                            attackDamage = 7;
                        }
                        bossMovement.WalkToPlayer(false);
                    }
                    else
                    {
                        if (!lastStage) // Block
                        {
                            bossAnim.SetBool("Block", true);
                            Invoke("Slash", Random.Range(30, 40f) * 0.1f);
                            attackDamage = 10;

                        }
                        else // Gearattack
                        {
                            bossAnim.SetTrigger("GearAttack");
                            attackDamage = 3;
                            bossMovement.WalkToPlayer(false);
                        }

                    }
                    animationIsPlaying = true;
                }
            }
        }

    }

    //Functions called from this script
    private void Slash()
    {
        bossMovement.SetSpeed(1.5f);
        bossAnim.SetBool("Block", false);
    }
    private void Punch()
    {
        //Randomize amount of punches
        int punchChance = Random.Range(0, 3);

        if (punchChance == 0) { bossAnim.SetInteger("PunchInt", 1); }
        else if (punchChance == 1) { bossAnim.SetInteger("PunchInt", 2); }
        else { bossAnim.SetInteger("PunchInt", 3); }
    }

    //Functions called from other scripts
    public void LastStage() { lastStage = true; gearGauntlet.SetActive(true); }
    public void Stunned()
    {
        if (canBeStunned)
        {
            bossAnim.SetBool("Stunned", true);
            bossMovement.LookAtPlayer(false);
        }

    }


    //Functions called from animations
    public void Shield()
    {
        canBeStunned = true;
        leaderBandit.CanBeHarmed(false);
    }
    public void NoShield()
    {
        bossAnim.SetBool("Block", false);
        leaderBandit.CanBeHarmed(true);

    }
    public void NotLethal() { lethal = false; gauntletCollider.enabled = false; }
    public void IsLethal()
    {
        lethal = true;
        gauntletCollider.enabled = true;
    }
    public void ChargeSpeed()
    {
        isCharging = true;
        bossMovement.WalkToPlayer(true);
        bossMovement.SetSpeed(16);
    }
    public void ShootGear()
    {
        gearBoomerang.GetComponent<GearBoomerang>().ActivateBoomerang();
    }

    public void ActionOver()
    {
        Invoke("AnimationDelay", 0f);
        bossAnim.SetInteger("PunchInt", 0);
        isCharging = false;
        canBeStunned = false;
        bossAnim.SetBool("Block", false);
        bossAnim.SetBool("Stunned", false);
        bossMovement.WalkToPlayer(true);
        bossMovement.LookAtPlayer(true);
        bossMovement.SetSpeed(7);
    }
    public void AnimationDelay()
    {
        animationIsPlaying = false;
    }

}

