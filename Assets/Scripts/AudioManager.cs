using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Clips de música
    public AudioClip menuMusic;
    public AudioClip backgroundMusic;
    // Clips de efectos de sonido
    public AudioClip[] enemyHitSounds;
    public AudioClip[] instructions;
    public AudioClip swordSFX;
    public AudioMixer audioMixer;
    private AudioSource sfxSource;
    private AudioSource menuMusicSource;
    private AudioSource backgroundMusicSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        menuMusicSource = transform.Find("MenuMusic").GetComponent<AudioSource>();
        backgroundMusicSource = transform.Find("BackgroundMusic").GetComponent<AudioSource>();
        sfxSource = transform.Find("SFX").GetComponent<AudioSource>();

        menuMusicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        backgroundMusicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        sfxSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
    }
    private void Start()
    {
        PlayMenuMusic();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Reproduce la instrucción cuando se carga una escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Map"))
        {
            int levelIndex = scene.buildIndex - SceneManager.GetActiveScene().buildIndex;
            PlayInstruction(levelIndex);
        }
    }
    public void PlayMenuMusic()
    {
        if (menuMusicSource.clip != menuMusic)
        {
            menuMusicSource.clip = menuMusic;
            menuMusicSource.loop = true;
            menuMusicSource.Play();
        }
        backgroundMusicSource.Stop();
    }
    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource.clip != backgroundMusic)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
        menuMusicSource.Stop();
    }
    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayEnemyHitSound()
    {
        int randomIndex = Random.Range(0, enemyHitSounds.Length);
        PlaySoundEffect(enemyHitSounds[randomIndex]);
    }
    public void PlayInstruction(int level)
    {
        if (level >= 0 && level < instructions.Length)
        {
            PlaySoundEffect(instructions[level]);
        }
    }
    public void PlaySwordSFX()
    {
        PlaySoundEffect(swordSFX);
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
}