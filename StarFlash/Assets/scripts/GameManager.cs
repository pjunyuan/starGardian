using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ez.Pooly;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int level;
    public Transform blockparent;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameStart()
    {
        Debug.Log("Game Start");
    }


    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void CreateLevel()
    {

    }

    private void Start()
    {
        Pooly.Spawn("block", new Vector3(0, 4, 0), Quaternion.identity );
    }





}
