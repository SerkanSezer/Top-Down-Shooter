using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract void Use(Vector3 targetPos);
    public abstract ItemSO GetItemSO();
    public abstract void SetWeaponAttribute(LayerMask mask);
    public abstract void SetItemUser(Transform user);
}
