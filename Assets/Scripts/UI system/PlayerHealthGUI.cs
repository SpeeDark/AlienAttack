using UnityEngine;
using UnityEngine.UI;

using Unit;

public class PlayerHealthGUI : MonoBehaviour
{
    private Image healthBar;

    [SerializeField] private Color fullHealthColor;
    [SerializeField] private Color mediumHealthColor;
    [SerializeField] private Color lowHealthColor;

    [Space(15f)]
    [SerializeField] private Player player;

    private void Awake()
    {
        healthBar = GetComponent<Image>();

        player.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void UpdateHealthBar()
    {
        var healthDifference = player.Health / player.MaxHealth;

        healthBar.fillAmount = healthDifference;

        if (healthDifference > 0.5f)
            healthBar.color = Color.Lerp(fullHealthColor, mediumHealthColor, (1f - healthDifference) * 2f);
        else
            healthBar.color = Color.Lerp(mediumHealthColor, lowHealthColor, (0.5f - healthDifference) * 2f);
    }
}