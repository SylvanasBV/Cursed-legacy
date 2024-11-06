using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // La imagen que representa la barra de salud

    // Método para actualizar la salud de la barra
    public void SetHealth(float healthFraction)
    {
        fillImage.fillAmount = healthFraction; // Actualiza la barra de salud
    }
}