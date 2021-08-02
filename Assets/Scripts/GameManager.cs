using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<GameObject> enemies;

    public float killsToWin;
    public float enemiesKilled;
    

    public PlayerController pc;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
        if(pc.lives <= 0)
        {
            //Change Scene

            Debug.Log("YOU DIED");
        }
    }
}
