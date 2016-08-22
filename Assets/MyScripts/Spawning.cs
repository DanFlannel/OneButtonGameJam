using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {

    public GameObject[] SpawnLoc;
    public GameObject Prefab;

    public float SpawnTimer;
    public float StartingTimer;
    private float AdjustedTimer;

	// Use this for initialization
	void Start () {
        SpawnTimer = StartingTimer;
        AdjustedTimer = StartingTimer;
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer <= 0)
        {
            Spawn();
            SpawnTimer = AdjustedTimer;
        }
	}
    
    private void Spawn()
    {
        int rnd = Random.Range(0, SpawnLoc.Length);
        GameObject enemy = Instantiate(Prefab, SpawnLoc[rnd].transform.position, Quaternion.identity) as GameObject;
        enemy.transform.parent = this.transform;
        AdjustedTimer -= .01f;
        if(AdjustedTimer <= .75f)
        {
            AdjustedTimer = .75f;
        }
    }
}
