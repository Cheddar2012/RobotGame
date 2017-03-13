using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _titleOrReplayScreen;

    private void Start() { }

    public void ShowVictoryMessage()
    {
        _titleOrReplayScreen.SetActive(true);
        _titleOrReplayScreen.GetComponentInChildren<Text>().text = "YOU WON";
    }

    public void ShowDeathMessage()
    {
        _titleOrReplayScreen.SetActive(true);
        _titleOrReplayScreen.GetComponentInChildren<Text>().text = "YOU ARE DEAD";
    }
}
