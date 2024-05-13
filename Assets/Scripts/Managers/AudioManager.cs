using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip weaponChange;
    [SerializeField] private AudioClip[] hitsSounds;

    private float soundVolume;

    private void Awake() {
        instance = this;
        LoadMusicAndSoundVolume();
    }
    public void PlayButtonSound() {
        soundSource.PlayOneShot(buttonSound, soundVolume);
    }
    public void PlayDoorSound() {
        soundSource.PlayOneShot(doorSound, soundVolume);
    }
    public void PlayPickupSound() {
        soundSource.PlayOneShot(pickupSound, soundVolume);
    }
    public void PlayWeaponChangeSound() {
        soundSource.PlayOneShot(weaponChange, soundVolume);
    }
    public void PlayHitSound() {
        soundSource.PlayOneShot(hitsSounds[Random.Range(0,hitsSounds.Length)], soundVolume);
    }

    private void LoadMusicAndSoundVolume() {
        string loadString = SoundSaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);

        if(musicSource != null)
        musicSource.volume = saveProps.musicVolume;

        if(soundSource != null)
        soundSource.volume = saveProps.soundVolume;
        soundVolume = saveProps.soundVolume;
    }

}
