using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

    private const string ATTACK_ANIM = "Attack";
    private const string HIT_ANIM = "Hit";
    private const string DEATH_ANIM = "Death";
    private const int BASE_LAYER_INDEX = 0;
    private const int TOOL_LAYER_INDEX = 1;
    private const int PISTOL_LAYER_INDEX = 2;
    private const int RIFLE_LAYER_INDEX = 3;
    private const int WALK_LAYER_INDEX = 4;
    private Animator animator;
    [SerializeField] private EnemyLayer enemyLayer;
    private void Awake() {
        animator = GetComponent<Animator>();
        SetAnimLayer();
    }

    public void PlayAttackAnim() {
        animator.SetTrigger(ATTACK_ANIM);
    }

    public void PlayHitAnim() {
        animator.SetTrigger(HIT_ANIM);
    }
    public void PlayDeathAnim() {
        ResetAllLayer();
        animator.SetTrigger(DEATH_ANIM);
    }
    public void PlayWalk() {
        animator.SetLayerWeight(WALK_LAYER_INDEX, 1);
    }
    public void StopWalk() {
        animator.SetLayerWeight(WALK_LAYER_INDEX, 0);
    }
    public void SetAnimLayer() {
        ResetAllLayer();
        switch (enemyLayer) {
            case EnemyLayer.Base:
                animator.SetLayerWeight(BASE_LAYER_INDEX, 1);
                break;
            case EnemyLayer.Knife:
                animator.SetLayerWeight(TOOL_LAYER_INDEX, 1);
                break;
            case EnemyLayer.Pistol:
                animator.SetLayerWeight(PISTOL_LAYER_INDEX, 1);
                break;
            case EnemyLayer.Rifle:
                animator.SetLayerWeight(RIFLE_LAYER_INDEX, 1);
                break;
            default:
                break;
        }
    }
    public void ResetAllLayer() {
        animator.SetLayerWeight(BASE_LAYER_INDEX, 0);
        animator.SetLayerWeight(TOOL_LAYER_INDEX, 0);
        animator.SetLayerWeight(PISTOL_LAYER_INDEX, 0);
        animator.SetLayerWeight(RIFLE_LAYER_INDEX, 0);
    }
}
public enum EnemyLayer {
    Base,
    Knife,
    Pistol,
    Rifle
}
