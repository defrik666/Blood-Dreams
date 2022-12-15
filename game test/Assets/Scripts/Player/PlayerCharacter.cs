using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int maxHealth = 5;
    private int health = 5;

    public BloodScreen BloodScreen;
    public GameObject DeathScreen;

    private void Awake()
    {
        BloodScreen = FindObjectOfType<BloodScreen>();
        Time.timeScale = 1f;
    }

    private void Start()
    {
        health = maxHealth;
        BloodScreen.ChangeScreen(health, maxHealth);
    }


    public void Hurt(int damage)
    {
        health -= damage;
        BloodScreen.ChangeScreen(health, maxHealth);
        if (health <= 0)
        {
            Time.timeScale = 0f;
            DeathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        Debug.Log("Player health: " + health);
    }
}