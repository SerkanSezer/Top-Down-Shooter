using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Player player;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color existColor;
    [SerializeField] private Color notExistColor;
    [SerializeField] private List<WeaponUI> weaponUIList;

    private void Awake() {
        player = FindObjectOfType<Player>(true);
        player.OnInventoryChanged += Player_OnInventoryChanged;
    }

    private void Player_OnInventoryChanged(List<PlayerWeapon> playerInventory) {
        foreach (var weapon in playerInventory) {
            foreach (var weaponUI in weaponUIList) {
                if (weapon.itemSO == weaponUI.itemSO) {
                    if (!weapon.isExist) {
                        weaponUI.weaponImageBG.color = notExistColor;
                    }
                    else {
                        weaponUI.weaponImageBG.color = existColor;
                        if(weapon.isSelected)
                            weaponUI.weaponImageBG.color = selectedColor;  
                    }
                }
            }
        }
    }

}
[Serializable]
public class WeaponUI {
    public ItemSO itemSO;
    public Image weaponImageBG;
}
