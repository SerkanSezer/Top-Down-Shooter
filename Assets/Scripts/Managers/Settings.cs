using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    private float soundVolume;
    private float musicVolume;

    [SerializeField] private Transform settingsPanel;
    [SerializeField] AudioSource musicSource;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;

    private void Awake() {
        if (musicSlider)
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);

        if (soundSlider)
            soundSlider.onValueChanged.AddListener(ChangeSoundVolume);

        SoundSaveManager.Init();
        LoadMusicAndSoundVolume();
    }
    public void OpenSettingsPanel() {
        AudioManager.instance.PlayButtonSound();
        settingsPanel.gameObject.SetActive(true);
    }
    public void CloseSettingsPanel() {
        AudioManager.instance.PlayButtonSound();
        SaveMusicSoundVolume();
        settingsPanel.gameObject.SetActive(false);
    }

    public void SaveMusicSoundVolume() {
        string loadString = SoundSaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);
        saveProps.musicVolume = musicVolume;
        saveProps.soundVolume = soundVolume;
        string newLoad = JsonUtility.ToJson(saveProps);
        SoundSaveManager.Save(newLoad);
    }
    private void LoadMusicAndSoundVolume() {
        string loadString = SoundSaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);

        musicSource.volume = saveProps.musicVolume;
        if (musicSlider != null)
            musicSlider.value = saveProps.musicVolume;

        soundVolume = saveProps.soundVolume;
        if (soundSlider != null)
            soundSlider.value = saveProps.soundVolume;
    }

    private void ChangeMusicVolume(float volume) {
        musicSource.volume = volume;
        musicVolume = volume;
    }
    private void ChangeSoundVolume(float volume) {
        soundVolume = volume;
    }
}
