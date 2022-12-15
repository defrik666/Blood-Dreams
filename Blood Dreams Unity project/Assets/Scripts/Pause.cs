using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    bool gameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject Camera;
    public GameObject weapon;
    public GameObject crosshair;
    public GameObject bloodScreen;
    public GameObject enemyCounter;
    public GameObject deathScreen;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && deathScreen.activeInHierarchy == false)
        {
            if (gameIsPaused) Resume();
            else if (gameIsPaused == false) PauseGame();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<MouseLook>().enabled = true;
        Camera.GetComponent<MouseLook>().enabled = true;
        weapon.GetComponent<MouseLook>().enabled = true;
        crosshair.SetActive(true);
        bloodScreen.SetActive(true);
        enemyCounter.SetActive(true);

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        gameObject.GetComponent<MouseLook>().enabled = false;
        Camera.GetComponent<MouseLook>().enabled = false;
        weapon.GetComponent<MouseLook>().enabled = false;
        crosshair.SetActive(false);
        bloodScreen.SetActive(false);
        enemyCounter.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
