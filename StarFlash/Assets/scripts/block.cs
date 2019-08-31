using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ez.Pooly;

public class block : MonoBehaviour {


    public Text lifetext;
	public AudioClip deadSound;
    public int life;


	// Use this for initialization
	void Start () {
        UpdateColor();
        //Debug.Log(color.r);
    }

    // Update is called once per frame
    void Update () {
        lifetext.text = life.ToString();

        if (life == 0)
        {
            //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.transform.position = transform.position;
            //sphere.transform.localScale = new Vector3(1, 1, 1);
            //sphere.transform.SetParent(transform);
            //sphere.GetComponent<Renderer>().material = GetComponent<Renderer>().material;

            //MeshExploder scripte = sphere.AddComponent<MeshExploder>();
            GetComponent<MeshExploder>().Explode();

            SoundManager.instance.PlaySingle(deadSound);

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
        Color color = new Color ( 25.5f * life /255.0f ,0.5f,0.5f);

        GetComponent<Renderer>().material.color = color;
        
    }

}
