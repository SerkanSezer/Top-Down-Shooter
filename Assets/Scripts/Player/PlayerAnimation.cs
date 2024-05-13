using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string ATTACK_ANIM = "Attack";
    private const string HIT_ANIM = "Hit";
    private const string DEATH_ANIM = "Death";
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnim() {
        animator.SetTrigger(ATTACK_ANIM);
    }

    public void PlayHitAnim() {
        animator.SetTrigger(HIT_ANIM);
    }
    public void PlayDeathAnim() {
        animator.SetTrigger(DEATH_ANIM);
    }
}
