using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerAnimation playerAnimation;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    private Player player;
    private GameManager gameManager;
    private bool isPlayerLive = true;

    [SerializeField] private ParticleSystem bloodSplash;
    [SerializeField] private Transform bloodPuddle;
    [SerializeField] private int health = 100;

    private void Awake() {
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAnimation = GetComponent<PlayerAnimation>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void Damage(int amount, Transform fromTrans) {
        if (isPlayerLive) {
            health -= amount;
            playerAnimation.PlayHitAnim();
            CreateBloodSplash(fromTrans);
            if (health <= 0) {
                isPlayerLive = false;
                gameManager.ShowGameLosePanel();
                SetPlayerPassive();
                playerAnimation.PlayDeathAnim();
                CreateBloodPuddle();
            }
        }
        
    }
    public void CreateBloodSplash(Transform transform) {
        Vector3 dir = (transform.position - this.transform.position).normalized;
        var bld = Instantiate(bloodSplash, this.transform.position, Quaternion.identity);
        bld.transform.right = dir;
    }

    public void CreateBloodPuddle() {
        Instantiate(bloodPuddle, transform.position - transform.right, Quaternion.identity);
    }

    public void SetPlayerPassive() {
        playerMove.enabled = false;
        playerAttack.enabled = false;
        player.ResetAllLayer();
        player.StopInputActions();
        gameManager.SetCursorActive();
    }
}
