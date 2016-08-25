using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public List<GameObject> enemies = new List<GameObject>();

    public int health;
    public GameObject Target;
    private GUIScript gui;

    Animator anim;
    bool clicked;

	// Use this for initialization
	void Start () {
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIScript>();
        anim = this.GetComponent<Animator>();
        gui.UpdateHealth(health);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (health > 0)
            {
                SortList();
                CheckDirection();
                anim.SetTrigger(CustomStrings.Continue);
            }
        }
	}

    public void ApplyDamage()
    {
        Debug.Log("Player Hit");
        health--;
        gui.UpdateHealth(health);
        if(health <= 0)
        {
            gui.GameOver();
            Debug.Log("Player Died");
        }
    }

    private void SortList()
    {
        for(int i = 0; i < enemies.Count - 1; i++)
        {
            if(Vector3.Distance(this.transform.position, enemies[i].transform.position) >= Vector3.Distance(this.transform.position, enemies[i + 1].transform.position)){
                Debug.Log("Swapping in list");
                GameObject tmp = enemies[i];
                enemies[i] = enemies[i + 1];
                enemies[i + 1] = tmp;
            }
        }
    }

    private void CheckDirection()
    {
        if(enemies.Count == 0)
        {
            return;
        }
        GameObject target = enemies[0];
        Vector3 targetPos = target.transform.position;

        Vector3 relativePos = targetPos - this.transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }
    }
}
