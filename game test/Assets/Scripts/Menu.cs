using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void ChangeLevel()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

    public void QuitGame()
    {
        Application.Quit();
    }
}
