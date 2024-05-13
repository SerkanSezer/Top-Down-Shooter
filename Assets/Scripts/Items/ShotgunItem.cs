using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunItem : Item {
    
    private LayerMask targetMask;
    private Transform itemUser;
    private AudioSource audioSource;
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private AnimatedSprite animatedSprite;

    public override ItemSO GetItemSO() {
        return itemSO;
    }

    public override void Use(Vector3 targetPos) {
        animatedSprite.Fire();
        audioSource.PlayOneShot(itemSO.itemSound);
        Vector3 dir = (targetPos - transform.position).normalized;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, dir, 100, targetMask);
        if (hit && hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable)) {
            damageable.Damage(itemSO.damageAmount,itemUser);
        }
    }
    public override void SetWeaponAttribute(LayerMask targetMask) {
        this.targetMask = targetMask;
        SetItemUser(transform);
        audioSource = GetComponent<AudioSource>();
    }
    public override void SetItemUser(Transform user) {
        itemUser = user;
    }
}
