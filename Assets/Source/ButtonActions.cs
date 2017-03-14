using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        GameManager.Instance.Objectives = -1;
        SceneManager.LoadScene(1);
    }
}
