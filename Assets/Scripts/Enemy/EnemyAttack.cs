using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Item item;
    private Transform itemWorld;
    private Player player;
    private EnemyAnimation enemyAnimation;
    private float attackTimer=1;
    private const float ATTACK_RATE = 1;
    private RifleItem rifleItem;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private Transform pfItem;
    [SerializeField] private LayerMask targetMask;
    private void Awake() {
        player = FindObjectOfType<Player>(true);
        enemyAnimation = GetComponent<EnemyAnimation>();
        SetItem();
    }

    public void Attack() {
        attackTimer += Time.deltaTime;
        if (attackTimer > ATTACK_RATE) {
            attackTimer = 0;
            enemyAnimation.PlayAttackAnim();
            if (item != null) {
                item.Use(player.transform.position);
            }
            if (rifleItem) { Invoke(nameof(StopRifle), 0.5f);  }
        }
    }

    public Item GetItem() {
        return item;
    }

    public void SetItem() {
        itemWorld = Instantiate(pfItem);
        itemWorld.SetParent(itemHolder, false);
        item = itemWorld.GetComponent<Item>();
        item.SetWeaponAttribute(targetMask);
        SetIfRifle();
    }

    public void TurnItemToCollectible() {
        itemWorld.AddComponent<BoxCollider2D>().isTrigger = true;
    }

    public void SetIfRifle() {
        if (item is RifleItem) {
            rifleItem = item.GetComponent<RifleItem>();
        }
    }
    public void StopRifle() {
        rifleItem.StopRifle();
    }
}
