using UnityEngine;
using System.Collections;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] pistolSprites;
    public Sprite[] rifleSprites;
    public Sprite[] shotgunSprites;

    [SerializeField] private SpriteRenderer sRenderer;
    [SerializeField] private GunType gunType;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        if (transform.parent.transform.TryGetComponent<PistolItem>(out PistolItem pistolItem)) {
            gunType = GunType.Pistol;
        }else if(transform.parent.transform.TryGetComponent<RifleItem>(out RifleItem rifleItem)) {
            gunType = GunType.Rifle;
        }else if (transform.parent.transform.TryGetComponent<ShotgunItem>(out ShotgunItem shotgunItem)) {
            gunType = GunType.Shotgun;
        }
    }

    public void Fire() {
        if (gunType == GunType.Pistol) {
            PistolFire();
        }else if(gunType == GunType.Rifle) {
            RifleFire();
        }else if(gunType == GunType.Shotgun) {
            ShotgunFire();
        }
    }
    public void PistolFire() {
        StopAllCoroutines();
        StartCoroutine(PistolFireIn());
    }
    public IEnumerator PistolFireIn() {
        for (int i = 0; i < pistolSprites.Length; i++) {
            sRenderer.sprite = pistolSprites[i];
            yield return new WaitForSeconds(0.01f);
        }
        sRenderer.sprite = null;
    }
    public void ShotgunFire() {
        StopAllCoroutines();
        StartCoroutine(ShotgunFireIn());
    }
    public IEnumerator ShotgunFireIn() {
        for (int i = 0; i < shotgunSprites.Length; i++) {
            sRenderer.sprite = shotgunSprites[i];
            yield return new WaitForSeconds(0.01f);
        }
        sRenderer.sprite = null;
    }

    public void RifleFire() {
        sRenderer.sprite = rifleSprites[Random.Range(0,rifleSprites.Length)];
    }
    public void StopRifleVFX() {
        sRenderer.sprite = null;
    }

}
public enum GunType {
    Pistol,
    Rifle,
    Shotgun
}