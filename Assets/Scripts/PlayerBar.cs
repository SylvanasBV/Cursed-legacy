using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public float maxHealth = 100f; // Salud máxima
    public float currentHealth;    // Salud actual
    public Image healthBar;        // Referencia a la barra de salud

    private void Start()
    {
        // Inicia la salud actual con la máxima salud
        currentHealth = maxHealth;
        UpdateHealthBar(); // Asegúrate de que la barra de salud se actualice al comienzo
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0f)
        {
            currentHealth = 0f; // Asegura que la salud no sea negativa
        }

        UpdateHealthBar(); // Actualiza la barra de salud
    }

    public void GiveHealt(float health)
    {
        currentHealth += health;
        if (currentHealth < 0f)
        {
            currentHealth = 0f; // Asegura que la salud no sea negativa
        }

        UpdateHealthBar(); // Actualiza la barra de salud
    }

    // Actualiza la barra de salud
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Calcula el porcentaje de salud restante
            float healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage; // Cambia el fillAmount de la imagen
        }
    }

    // Detecta colisiones con objetos que tengan el tag "Enemy"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10f); // Reduce la salud al colisionar con un enemigo
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Poison"))
        {
            GiveHealt(10f); // incrementa la salud al colisionar con un enemigo
        }
    }

}     
