using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _titleOrReplayScreen;
    [SerializeField]
    private Text _hitPointsCount;
    public int HitPointsCount
    {
        set
        {
            _hitPointsCount.text = value.ToString();
        }
    }

    [SerializeField]
    private Text _rocketsCount;
    public int RocketsCount
    {
        set
        {
            _rocketsCount.text = value.ToString();
        }
    }

    [SerializeField]
    private Text _enemiesCount;
    public int EnemiesCount
    {
        set
        {
            _enemiesCount.text = value.ToString();
        }
    }

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
