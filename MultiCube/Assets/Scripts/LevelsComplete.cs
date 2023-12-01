using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsComplete : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
