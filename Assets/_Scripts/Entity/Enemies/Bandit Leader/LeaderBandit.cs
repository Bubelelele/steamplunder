using UnityEngine;

public class LeaderBandit : EnemyBase
{

    public GameObject healthCanvas;

    [HideInInspector] public bool isActive;
    [HideInInspector] public bool canBeHarmed;

    private bool firstDone = false;

    private AttackScript attackScript;
    private BossMovement bossMovement;
    private void Start()
    {
        attackScript = GetComponent<AttackScript>();
        bossMovement = GetComponent<BossMovement>();
        DeactivateBoss();
        bossMovement.WalkToPlayer(false);
    }
    public override void Hit(int damage, Artifact source)
    {
        if (!canBeHarmed) damage = 0;
        base.Hit(damage, source);

        if (_health <= maxHealth / 2 && !firstDone)
        {
            GetComponent<BossStages>().Stage2();
            firstDone = true;
        }

        
    }
    public void ActivateBoss()
    {
        healthCanvas.SetActive(true);
        bossMovement.WalkToPlayer(true);
        isActive = true;
        CanBeHarmed(true);

    }
    public void DeactivateBoss()
    {
        healthCanvas.SetActive(false);
        isActive = false;
        CanBeHarmed(false);
        attackScript.ActionOver();
    }
    public void CanBeHarmed(bool state)
    {
        canBeHarmed = state;
    }
    protected override void Die()
    {
        base.Die();
        healthCanvas.SetActive(false);
        Destroy(gameObject);
    }
    public override void Stun()
    {
        attackScript.Stunned();
    }
}
