using UnityEngine;

public class PlayerAttack : UnitAttackController
{
    private PlayerAnimationController playerAnimationController;
    private PlayerCombo playerCombo;
    private AttackHitbox attackHitbox;
    [SerializeField] private GameObject hitboxAttack;


    protected override void Awake()
    {
        base.Awake();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerCombo = GetComponent<PlayerCombo>();
        attackHitbox = GetComponentInChildren<AttackHitbox>();
    }
    protected override void Update()
    {
        base.Update();
    }

    public void ExecuteComboAttack()
    {
        if (unitStats.Stunned()) return;

        playerCombo.ExecuteCombo();
        ExecuteAttack(normalAttack);
    }

    public void ApplyDamage(EnemyStats enemyStats)
    {
        if (enemyStats != null)
        {
            enemyStats.TakeDamage(normalAttack);
        }
    }

    public void OnAnimationComplete()
    {
        if (playerCombo.GetComboStep() == 0)
        {
            playerCombo.ResetCombo();
        }
    }

    
    public void ExecuteNormalAttack()
    {
        if (unitStats.Stunned()) return;
        ExecuteAttack(normalAttack);
        playerAnimationController.SetAttack3();
    }
    

    public void ExecuteSpecialAttack()
    {
        ExecuteAttack(specialAttack);
        playerAnimationController.SetIsSpecialAttack();

    }

    public void ActivateHitbox()
    {
        hitboxAttack.SetActive(true);
    }

    public void DesactivateHitbox(){
        hitboxAttack.SetActive(false);
    }
}
