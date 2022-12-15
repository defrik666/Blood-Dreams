using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Exit : MonoBehaviour
{
    public int NextScene;
    public Enemy[] Enemies;
    bool EnemiesAlive = true;
    int EnemiesDead = 0;

    public GameObject enemyCounter;

    private void Start()
    {
        Enemies = FindObjectsOfType<Enemy>();
        CheckEnemies();
    }

    public void CheckEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            if (enemy.enabled == false)
            {
                EnemiesDead++;
            }
        }
        if (Enemies.Length - EnemiesDead > 0) enemyCounter.GetComponent<TextMeshProUGUI>().text = $"Врагов осталось:{Enemies.Length - EnemiesDead}";
        else enemyCounter.GetComponent<TextMeshProUGUI>().text = "Все враги убиты. Направляйтесь к выходу.";
        if (EnemiesDead == Enemies.Length) EnemiesAlive = false;
        else EnemiesDead = 0;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (EnemiesAlive == false) SceneManager.LoadScene(NextScene);
        }
    }
}
