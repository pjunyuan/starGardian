using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ez.Pooly;

public class block : MonoBehaviour {


    public Text lifetext;
    public MeshExploder script;
	public AudioClip deadSound;
    public int life = 1;
    


	// Use this for initialization
	void Start () {
        UpdateColor();
        Debug.Log(color.r);
    }

    // Update is called once per frame
    void Update () {
        lifetext.text = life.ToString();
        if (life == 0)
        {
            script.Explode();
            SoundManager.instance.PlaySingle(deadSound);
            //GameManager.instance.GameOver();
            //Destroy(gameObject);
            Pooly.Despawn(transform);

            if (Pooly.GetActiveCloneCount("block") == 0)
            { 
                GameManager.instance.Spawnblock();
                GameManager.level++;
            }
        }
        UpdateColor();
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball" && life > 0)
        {
            life--;
        }
        
    }

    public void UpdateColor()
    {
        Color color = GetComponent<MeshRenderer>().material.color;     
        color.r = ((255 - 23) / 10 * life);
        GetComponent<Renderer>().material.color = color;
        transform.Find("Sphere").GetComponent<Renderer>().material.color = color;
        
    }

}
