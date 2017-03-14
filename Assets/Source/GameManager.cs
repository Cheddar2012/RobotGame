using UnityEngine;

public class GameManager
{
    private const string _playerObjectTag = "Player";

    private int _objectivesToComplete;
    public int Objectives
    {
        get
        {
            return _objectivesToComplete;
        }

        set
        {
            // First objective added will bring the value to 0 from -1, so make sure it is 1 instead to avoid showing victory screen
            if (_objectivesToComplete == -1 && value == 0)
            {
                _objectivesToComplete = 1;
            }
            else
            {
                _objectivesToComplete = value;
            }

            GetUI().EnemiesCount = _objectivesToComplete;

            if (_objectivesToComplete == 0)
            {
                OnObjectivesComplete();
            }
        }
    }

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    private GameManager()
    {
        // Default to -1 so the victory scren does not instantly appear
        _objectivesToComplete = -1;
    }

    public GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag(_playerObjectTag);
    }

    public UIManager GetUI()
    {
        return GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void OnPlayerDead()
    {
        GetPlayer().GetComponent<PlayerMotion>().Die();
        GetUI().ShowDeathMessage();
    }

    public bool CanControlPlayerCharacter()
    {
        return !GetPlayer().GetComponent<PlayerHealth>().IsDead();
    }

    public void OnObjectivesComplete()
    {
        GetUI().ShowVictoryMessage();
    }
}
