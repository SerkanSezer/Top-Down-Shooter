using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    private PlayerAttack playerAttack;
    private PlayerWeapon selectedWeapon;
    private Animator animator;
    private InputActions inputActions;
    private const int BASE_LAYER_INDEX = 0;
    private const int TOOL_LAYER_INDEX = 1;
    private const int PISTOL_LAYER_INDEX = 2;
    private const int RIFLE_LAYER_INDEX = 3;
    [SerializeField] private List<PlayerWeapon> playerInventory;
    public event Action<List<PlayerWeapon>> OnInventoryChanged;

    private void Awake() {
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact;
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }
    private void Start() {
        PlayerSetWeapon(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            PlayerSetWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            PlayerSetWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            PlayerSetWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            PlayerSetWeapon(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            PlayerSetWeapon(5);
        }
    }
    public void ResetAllLayer() {
        animator.SetLayerWeight(BASE_LAYER_INDEX, 0);
        animator.SetLayerWeight(TOOL_LAYER_INDEX, 0);
        animator.SetLayerWeight(PISTOL_LAYER_INDEX, 0);
        animator.SetLayerWeight(RIFLE_LAYER_INDEX, 0);
    }

    public InputActions GetInputActions() {
        return inputActions;
    }

    public void Interact(InputAction.CallbackContext cntx) {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.5f, transform.right, 2);
        foreach (var hit in hits) {
            if (hit.transform.TryGetComponent<IInteractable>(out IInteractable interactable)) {
                interactable.Interact();
            }
            if(hit.transform.TryGetComponent<Item>(out Item item)) {
                AddInventory(item, hit.transform.gameObject);
            }
        }
    }

    public void PlayerSetWeapon(int weaponIndex) {
        if (playerInventory[weaponIndex].isExist) {
            AudioManager.instance.PlayWeaponChangeSound();
            if (selectedWeapon != null) selectedWeapon.isSelected = false;
            playerAttack.SetItem(playerInventory[weaponIndex].itemSO.pfItem.GetComponent<Item>());
            playerInventory[weaponIndex].isSelected = true;
            selectedWeapon = playerInventory[weaponIndex];
            ResetAllLayer();
            animator.SetLayerWeight(playerInventory[weaponIndex].animationLayerIndex, 1f);
            OnInventoryChanged?.Invoke(playerInventory);
        }
        
    }
    public void AddInventory(Item item, GameObject itemGO) {
        foreach (var weapon in playerInventory) {
            if (item.GetItemSO() == weapon.itemSO && !weapon.isExist) {
                AudioManager.instance.PlayPickupSound();
                weapon.isExist = true;
                OnInventoryChanged?.Invoke(playerInventory);
                Destroy(itemGO);
            }
        }
    }

    public void StopInputActions() {
        inputActions.Player.Disable();
    }

}

[Serializable]
public class PlayerWeapon {
    public ItemSO itemSO;
    public bool isExist;
    public int animationLayerIndex;
    public bool isSelected;
}
