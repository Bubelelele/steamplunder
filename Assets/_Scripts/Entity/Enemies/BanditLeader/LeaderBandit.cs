using UnityEngine;

public class LeaderBandit : EnemyBase
{

    public GameObject healthCanvas;

    [HideInInspector] public bool isActive = false;
    [HideInInspector] public bool canBeHarmed = true;

    private bool firstDone = false;

    public override void Hit(int damage, Artifact source)
    {
        base.Hit(damage, source);
        if (_health <= maxHealth / 2 && !firstDone)
        {
            GetComponent<BossStages>().Stage2();
            firstDone = true;
            Debug.Log("2");
        }
    }

    private AttackScript attackScript;
    private void Start()
    {
        attackScript = GetComponent<AttackScript>();
    }
    public void ActivateBoss()
    {
        healthCanvas.SetActive(true);
        isActive = true;
        CanBeHarmed(true);
    }
    public void DeactivateBoss()
    {
        healthCanvas.SetActive(false);
        isActive = false;
        CanBeHarmed(false);
        attackScript.Abort();
        attackScript.ActionOver();
    }
    public void CanBeHarmed(bool state)
    {
        canBeHarmed = state;
    }
}
