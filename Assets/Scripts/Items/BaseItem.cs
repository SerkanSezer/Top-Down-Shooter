using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : Item {

    private LayerMask targetMask;
    private Transform itemUser;
    private AudioSource audioSource;
    [SerializeField] private ItemSO itemSO;

    public override ItemSO GetItemSO() {
        return itemSO;
    }

    public override void Use(Vector3 targetPos) {
        audioSource.PlayOneShot(itemSO.itemSound);
        Vector3 dir = ((transform.right+transform.up)/2);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, dir, 1, targetMask);
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
