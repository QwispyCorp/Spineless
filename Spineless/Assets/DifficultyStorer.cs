using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyStorer : MonoBehaviour
{
    private static DifficultyStorer _instance;
    public static DifficultyStorer Instance { get { return _instance; } }
    [SerializeField] private GameDifficulty difficulty;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
