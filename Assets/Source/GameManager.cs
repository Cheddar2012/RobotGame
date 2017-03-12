using UnityEngine;
using System.Collections;

public class GameManager {

    private const string _playerObjectTag = "Player";

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

    private GameManager() { }

    public GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag(_playerObjectTag);
    }
}
