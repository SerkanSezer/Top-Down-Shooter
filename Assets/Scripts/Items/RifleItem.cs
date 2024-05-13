using System.Collections;
using UnityEngine;

public class RifleItem : Item {

    private LayerMask targetMask;
    private Transform itemUser;
    private bool ownerIsHero = false;
    private float rifleTimer;
    private const float RIFLE_TIMER_MAX = 0.1f;
    private bool isPlayerHold = false;
    private Camera cam;
    private AudioSource audioSource;
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private AnimatedSprite animatedSprite;

    public override ItemSO GetItemSO() {
        return itemSO;
    }

    private void Update() {
        if (ownerIsHero && Input.GetMouseButtonUp(0)) {
            StopRifle();
        }
    }

    public override void Use(Vector3 targetPos) {
        StartCoroutine(UseRifle(targetPos));
    }

    public IEnumerator UseRifle(Vector3 targetPos) {
        Vector3 dir;
        while (true) {
            yield return null;
            rifleTimer += Time.deltaTime;
            if (rifleTimer > RIFLE_TIMER_MAX) {
                rifleTimer = 0;
                animatedSprite.Fire();
                if (!audioSource.isPlaying) { audioSource.Play(); }

                if (isPlayerHold) {
                    dir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                }else { 
                    dir = (targetPos - transform.position).normalized; 
                }
                
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 60, targetMask);
                if (hit && hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable)) {
                    damageable.Damage(itemSO.damageAmount, itemUser);
                }
            }
        }
    }
    public void StopRifle() {
        animatedSprite.StopRifleVFX();
        audioSource.Stop();
        StopAllCoroutines();
    }
    public override void SetWeaponAttribute(LayerMask targetMask) {
        this.targetMask = targetMask;
        SetItemUser(transform);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = itemSO.itemSound;
        if (transform.root.TryGetComponent<Player>(out Player player)) {
            isPlayerHold = true;
            cam = Camera.main;
        }
    }

    public override void SetItemUser(Transform user) {
        itemUser = user;
        if (transform.root.TryGetComponent<Player>(out Player player)) {
            ownerIsHero = true;
        }
    }
}
