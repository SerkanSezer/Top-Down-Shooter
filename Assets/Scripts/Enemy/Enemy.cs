using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private GameManager gameManager;
    private PlayerAttack playerAttack;
    private EnemyMove enemyMove;
    private EnemyAttack enemyAttack;    
    private EnemyAnimation enemyAnimation;
    private float CHASE_RADIUS = 30;
    private float ATTACK_RADIUS = 10;

    [SerializeField] Transform radarTransform;
    [SerializeField] private EnemyState state;
    [SerializeField] LayerMask layerMask;
    
    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameFinished += SetPassive;
        playerAttack = FindObjectOfType<PlayerAttack>(true);
        playerAttack.OnGunFired += ChasePlayer;
        enemyMove = GetComponent<EnemyMove>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Start() {
        SetRadiuses();
    }

    void Update() {
        switch (state) {
            case EnemyState.Idle:
                enemyAnimation.StopWalk();
                if (GetPlayer() && Vector3.Distance(playerAttack.transform.position, transform.position) < CHASE_RADIUS) {
                    state = EnemyState.Chase;
                }
                break;
            case EnemyState.Patrol:
                enemyAnimation.PlayWalk();
                enemyMove.Patrol();
                if (GetPlayer() && Vector3.Distance(playerAttack.transform.position,transform.position) < CHASE_RADIUS) {
                    state = EnemyState.Chase;
                }
                break;
            case EnemyState.Chase:
                enemyAnimation.PlayWalk();
                enemyMove.Chase();
                if (Vector3.Distance(playerAttack.transform.position, transform.position) > CHASE_RADIUS) {
                    state = EnemyState.Patrol;
                }else if(GetPlayer() && Vector3.Distance(playerAttack.transform.position, transform.position) < ATTACK_RADIUS) {
                    state = EnemyState.Attack;
                }
                break;
            case EnemyState.Attack:
                enemyAnimation.StopWalk();
                enemyMove.FaceOnPlayer();
                enemyAttack.Attack();
                if (Vector3.Distance(playerAttack.transform.position,transform.position) > ATTACK_RADIUS*1.5f) {
                    state = EnemyState.Chase;
                }
                break;
            case EnemyState.Dead:
                enemyAnimation.StopWalk();
                break;
            default:
                break;
        }
    }

    public void ChasePlayer() {
        if (Vector3.Distance(playerAttack.transform.position, transform.position) < CHASE_RADIUS) {
            state = EnemyState.Chase;
        }
    }

    public bool GetPlayer() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(radarTransform.transform.position, 0.05f, radarTransform.transform.right, 60, layerMask);

        System.Array.Sort(hits, (a, b) => (a.distance.CompareTo(b.distance)));

        if (hits.Length > 0) {
            if (hits[0].transform.gameObject.layer == 7) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
    public void SetDead() {
        playerAttack.OnGunFired -= ChasePlayer;
        state = EnemyState.Dead;
    }
    public void SetPassive(bool status) {
        if (!status) {
            state = EnemyState.Dead;
        }
    }

    public void SetRadiuses() {
        CHASE_RADIUS = enemyAttack.GetItem().GetItemSO().chaseRadius;
        ATTACK_RADIUS = enemyAttack.GetItem().GetItemSO().attackRadius;
    }

}
public enum EnemyState {
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead
}