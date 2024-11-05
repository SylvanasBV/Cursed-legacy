using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Clips de música
    public AudioClip menuMusic;        // Música para el menú principal
    public AudioClip backgroundMusic;   // Música para los niveles

    // Clips de efectos de sonido
    public AudioClip[] enemyHitSounds;  // Efectos para cuando el jugador golpea al enemigo
    public AudioClip[] instructions;     // Instrucciones que dice el personaje en cada nivel
    public AudioClip swordSFX;           // SFX de la espada del jugador

    public AudioMixer audioMixer;        // AudioMixer para ajustar el volumen

    private AudioSource sfxSource;          // Para efectos de sonido
    private AudioSource menuMusicSource;    // Para la música del menú
    private AudioSource backgroundMusicSource;// Para la música de fondo

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

        // Obtener los AudioSources directamente desde los hijos
        menuMusicSource = transform.Find("MenuMusic").GetComponent<AudioSource>();
        backgroundMusicSource = transform.Find("BackgroundMusic").GetComponent<AudioSource>();
        sfxSource = transform.Find("SFX").GetComponent<AudioSource>();

        // Asegura que cada AudioSource esté vinculado al grupo adecuado en el AudioMixer
        menuMusicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        backgroundMusicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        sfxSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
    }

    private void Start()
    {
        PlayMenuMusic();
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
