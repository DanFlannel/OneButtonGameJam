using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public EnemyStates state;
    public GameObject deathPrefab;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public float upwardForce = .3f;
    bool appliedForce;
    public float yOffset;

    private Animator anim;
    private NavMeshAgent agent;
    private GameObject player;
    private bool applyDamage;
    PlayerController playerControls;

    // Use this for initialization
    void Start () {
        applyDamage = false;
        appliedForce = false;
        player = GameObject.FindGameObjectWithTag(CustomStrings.Player);
        playerControls = player.GetComponent<PlayerController>();
        playerControls.enemies.Add(this.gameObject);
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        agent.SetDestination(player.transform.position);
        if(agent.stoppingDistance >= Vector3.Distance(this.transform.position, player.transform.position)){
            state = EnemyStates.Attacking;
            anim.SetBool("Walk Forward", false);
            anim.SetTrigger("PunchTrigger");
            CheckPunch();
            
        }
        else
        {
            state = EnemyStates.Moving;
            anim.SetBool("Walk Forward", true);
        }

	}

    private void CheckPunch()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime > .5f && stateInfo.IsName("Punch"))
        {
            if (!applyDamage)
            {

                playerControls.ApplyDamage();
                applyDamage = true;
                DestroyEnemy();
            }
        }
    }

    public void DestroyEnemy()
    {
        if (!appliedForce)
        {
            appliedForce = true;
            this.GetComponent<Animator>().enabled = false;
            Vector3 deathPos = new Vector3(this.transform.position.x, this.transform.position.y + yOffset, this.transform.position.z);
            GameObject death = Instantiate(deathPrefab, deathPos, Quaternion.identity) as GameObject;
            foreach(Transform child in death.transform)
            {
                Rigidbody rigid = child.GetComponent<Rigidbody>();
                float rnd = Random.Range(-1, 1);
                Vector3 explosionPos = new Vector3(child.transform.position.x + rnd, child.transform.position.y, child.transform.position.z + rnd);
                rigid.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardForce, ForceMode.Impulse);
            }

            playerControls.enemies.Remove(this.gameObject);
            GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIScript>().addKill();
            Debug.Log("Destroying Enemy");
            Destroy(this.gameObject);

        }
    }
}

public enum EnemyStates
{
    Moving,
    Attacking,
    Stopped
};
