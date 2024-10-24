using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralSceneManagement : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
