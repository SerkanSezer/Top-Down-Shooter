using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerAttack : MonoBehaviour
{
    private Item item;
    private CinemachineBasicMultiChannelPerlin vCamWobble;
    private bool isShake = false;
    private Player player;
    private PlayerAnimation playerAnimation;
    private InputActions inputActions;
    private Camera mainCamera;

    [SerializeField] private Transform itemHolder;
    [SerializeField] private LayerMask targetMask;
    public event Action OnGunFired;

    private void Awake() {
        vCamWobble = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        player = GetComponent<Player>();
        playerAnimation = GetComponent<PlayerAnimation>();
        mainCamera = Camera.main;
    }
    private void Start() {
        inputActions = player.GetInputActions();
        inputActions.Player.Attack.performed += Attack;
    }

    public void Attack(InputAction.CallbackContext cntx) {
        ShakeScreen();
        TriggerGunFire();
        playerAnimation.PlayAttackAnim();
        if (item != null) {
            item.Use(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    public void Hit() {
        playerAnimation.PlayHitAnim();
    }
    public void SetItem(Item item) {
        ResetItemholder();
        var itemWorld = Instantiate(item.GetItemSO().pfItem);
        itemWorld.SetParent(itemHolder,false);
        this.item = itemWorld.GetComponent<Item>();
        this.item.SetWeaponAttribute(targetMask);
    }

    public void ResetItemholder() {
        if (itemHolder.childCount > 0) {
            Destroy(itemHolder.GetChild(0).gameObject);
        }
    }

    public void ShakeScreen() {
        if (!isShake) {
            StartCoroutine(ShakeScreenRoutine());
        }
    }
    public void TriggerGunFire() {
        if (item is PistolItem || item is RifleItem || item is ShotgunItem) {
            OnGunFired?.Invoke();
        }
    }
    public IEnumerator ShakeScreenRoutine() {
        isShake = true;
        vCamWobble.m_AmplitudeGain = 1;
        yield return new WaitForSeconds(0.1f);
        vCamWobble.m_AmplitudeGain = 0;
        isShake = false;
    }
}
