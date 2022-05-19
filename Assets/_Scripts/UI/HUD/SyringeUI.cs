using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SyringeUI : MonoBehaviour {
    
    public int Cogs { get; private set; }
    
    [SerializeField] private KeyCode healKey = KeyCode.X;
    [SerializeField] private int cogsForSyringe = 5;
    [SerializeField] private int healAmount = 50;

    [Header("UI elements")]
    [SerializeField] private TMP_Text cogCount;
    [SerializeField] private GameObject canHealText;
    [SerializeField] private GameObject cantHealText;
    [SerializeField] private Image radialProgressImage;
    [SerializeField] private Animator syringeAnimator;

    private bool _healReady;
    private float _radialTarget;

    private void Start() {
        if (Cogs < cogsForSyringe) SetHealReady(false);
        SetCogCount(Cogs);
    }

    private void Update() {
        radialProgressImage.fillAmount =
            Mathf.MoveTowards(radialProgressImage.fillAmount, _radialTarget, Time.deltaTime);
        
        if (Input.GetKeyDown(healKey)) {
            Heal();
        }
    }

    public void AddCogs(int amount) {
        if (_healReady) return;
        
        Cogs += amount;
        SetCogCount(Cogs);
        
        if (Cogs >= cogsForSyringe) {
            SetHealReady(true);
        }
    }

    private void SetHealReady(bool ready) {
        _healReady = ready;
        canHealText.SetActive(ready);
        cantHealText.SetActive(!ready);
    }

    private void Heal() {
        if (!_healReady) return;
        
        PlayerData.Heal(healAmount);
        SetHealReady(false);
        Cogs = 0;
        SetCogCount(Cogs);
        syringeAnimator.Play("SyringeFade");
    }
    
    private void SetCogCount(int cogs) {
        cogCount.text = cogs + "/" + cogsForSyringe;
        _radialTarget = (float) cogs / cogsForSyringe;
    }
}