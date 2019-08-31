using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ez.Pooly;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public static int level = 1;
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

    private void Start()
    {
        Spawnblock();
    }

    public void Spawnblock()
    {
        for (int i = 0; i < level; i++)
        {
            int x = Random.Range(-5, 5);
            int y = Random.Range(-4, 4);
            Transform current = Pooly.Spawn("block", new Vector3(x, y, 0), Quaternion.identity);
            current.GetComponent<block>().life = Random.Range(1, level);

        }


    }

}
