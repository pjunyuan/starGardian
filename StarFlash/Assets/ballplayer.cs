using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballplayer : MonoBehaviour {

    public float force;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(pz.x, pz.y);
            Vector2 ballPos = transform.position;
            

            GetComponent<Rigidbody2D>().AddForce((mousePos - ballPos).normalized * force, ForceMode2D.Force);
        }
    }


    void destroyball()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<MeshExploder>().Explode();
            Destroy(gameObject);
        }

    }




}
