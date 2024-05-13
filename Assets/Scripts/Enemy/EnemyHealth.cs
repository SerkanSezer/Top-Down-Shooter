using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private GameManager gameManager;
    private EnemyAnimation enemyAnimation;
    private EnemyAttack enemyAttack;
    private Enemy enemy;
    private bool isEnemyLive = true;
    [SerializeField] private ParticleSystem bloodSplash;
    [SerializeField] private Transform bloodPuddle;
    [SerializeField] private Transform damageNumber;
    [SerializeField] private int health = 100;

    private void Awake() {
        enemy = GetComponent<Enemy>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddEnemy();
    }
    
    public void Damage(int amount, Transform fromTrans) {
        if (isEnemyLive) {
            health -= amount;
            enemyAnimation.PlayHitAnim();
            int dNAmount = Random.Range(100, 300);
            gameManager.AddKillPoints(dNAmount);
            CreateDamageNumber(dNAmount);
            CreateBloodSplash(fromTrans);
            if (health <= 0) {
                gameManager.RemoveEnemy();
                isEnemyLive = false;
                enemyAnimation.PlayDeathAnim();
                enemy.SetDead();
                SetComponents();
                CreateBloodPuddle();
                enemyAttack.TurnItemToCollectible();
            }
        }
    }
    public void SetComponents() {
        Destroy(transform.GetComponent<CircleCollider2D>());
        Destroy(transform.GetComponent<Rigidbody2D>());
    }
    public void CreateBloodSplash(Transform transform) {
        Vector3 dir = (transform.position - this.transform.position).normalized;
        var bld = Instantiate(bloodSplash, this.transform.position, Quaternion.identity);
        bld.transform.right = dir;
    }

    public void CreateBloodPuddle() {
        Instantiate(bloodPuddle, transform.position - transform.right, Quaternion.identity);
    }
    public void CreateDamageNumber(int points) {
        var dNumber = Instantiate(damageNumber, transform.position, Quaternion.identity);
        dNumber.transform.GetComponent<DamageNumber>().SetKillPoints(points);
    }

}
