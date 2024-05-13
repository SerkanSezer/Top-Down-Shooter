using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeItem : Item {
    
    private LayerMask targetMask;
    private Transform itemUser;
    private AudioSource audioSource;
    [SerializeField] private ItemSO itemSO;

    public override ItemSO GetItemSO() {
        return itemSO;
    }

    public override void Use(Vector3 targetPos) {
        audioSource.PlayOneShot(itemSO.itemSound);
        Vector3 dir = transform.right;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, dir, 0.4f, targetMask);
        if (hit && hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable)) {
            damageable.Damage(itemSO.damageAmount, itemUser);
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
