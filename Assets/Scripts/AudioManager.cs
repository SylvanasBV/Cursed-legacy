using UnityEngine;

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
    }

    private void Start()
    {
        // Reproducir la música del menú al iniciar
        PlayMenuMusic();
    }

    // Reproduce la música del menú
    public void PlayMenuMusic()
    {
        if (menuMusicSource.clip != menuMusic)
        {
            menuMusicSource.clip = menuMusic;
            menuMusicSource.loop = true;
            menuMusicSource.Play();
        }
        backgroundMusicSource.Stop(); // Detener la música de fondo si está sonando
    }

    // Reproduce la música de los niveles
    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource.clip != backgroundMusic)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
        menuMusicSource.Stop(); // Detener la música del menú si está sonando
    }

    // Reproduce un sonido específico
    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Reproduce un sonido aleatorio cuando el jugador golpea al enemigo
    public void PlayEnemyHitSound()
    {
        int randomIndex = Random.Range(0, enemyHitSounds.Length);
        PlaySoundEffect(enemyHitSounds[randomIndex]);
    }

    // Reproduce el audio del personaje en función del nivel
    public void PlayInstruction(int level)
    {
        if (level >= 0 && level < instructions.Length)
        {
            PlaySoundEffect(instructions[level]);
        }
    }

    // Reproduce el sonido de la espada
    public void PlaySwordSFX()
    {
        PlaySoundEffect(swordSFX);
    }

    // Detiene la música actual (útil para transiciones entre menú y niveles)
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
}