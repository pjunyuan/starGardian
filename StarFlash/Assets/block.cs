using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class block : MonoBehaviour {


    public int life = 99;
    public Text lifetext;
    public MeshExploder script;
	public AudioClip deadSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lifetext.text = life.ToString();
        if (life == 0)
		{ 
			script.Explode();
			SoundManager.instance.PlaySingle(deadSound);
			Destroy(gameObject);
        }
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball" && life > 0)
        {
            life--;
        }

	}





}
