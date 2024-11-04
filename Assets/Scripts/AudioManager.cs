using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Clips de música
    public AudioClip menuMusic;        // Música para el menú principal
    public AudioClip backgroundMusic;   // Música para los niveles
    // Clips de efectos de sonido
    public AudioClip[] enemyHitSounds;  // Efectos para cuando el jugador golpea al enemigo
    public AudioClip[] instructions;    // Instrucciones que dice el personaje en cada nivel
    public AudioClip swordSFX;          // SFX de la espada del jugador
    private AudioSource audioSource;
    private AudioSource backgroundSource;  // Para música de fondo separada
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
        // Configura dos AudioSources, uno para la música de fondo y otro para los efectos de sonido
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length < 2)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            backgroundSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = sources[0];
            backgroundSource = sources[1];
        }
    }
    // Reproduce la música del menú
    public void PlayMenuMusic()
    {
        backgroundSource.clip = menuMusic;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }
    // Reproduce la música de los niveles
    public void PlayBackgroundMusic()
    {
        backgroundSource.clip = backgroundMusic;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }
    // Reproduce un sonido específico
    public void PlaySoundEffect(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
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
        backgroundSource.Stop();
    }
}