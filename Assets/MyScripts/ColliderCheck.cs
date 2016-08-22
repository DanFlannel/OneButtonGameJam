using UnityEngine;
using System.Collections;

public class ColliderCheck : MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;
    private Animator anim;
    private AnimatorStateInfo stateInfo;
    private AnimatorTransitionInfo transitionInfo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(CustomStrings.Player);
        playerController = player.GetComponent<PlayerController>();
        anim = player.GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        transitionInfo = anim.GetAnimatorTransitionInfo(0);

        if (stateInfo.IsName("Idle") && !transitionInfo.anyState)
        {
            return;
        }


        if (other.tag != CustomStrings.Ground)
        {
            if (other == null)
            {
                return;
            }

            bool foundParent = false;
            //Debug.Log("TRIGGER: " + other.tag);
            Transform parent = other.transform;

            if(parent == null)
            {
                return;
            }

            if (parent.tag != CustomStrings.Enemy)
            {
                for(int i = 0; i < 100; i++)
                {
                    

                    parent = parent.transform.parent;

                    if (parent == null)
                    {
                        return;
                    }
                    if (parent.tag == CustomStrings.Enemy)
                    {
                        //Debug.Log("Enemy Parent Found");
                        foundParent = true;
                        break;
                    }
                }
            }
            if (foundParent)
            {
                if(parent == null)
                {
                    return;
                }
                Enemy enemy = parent.GetComponent<Enemy>();
                //Debug.Log("Enemy Name: " +parent.transform.name);
                playerController.Target = parent.gameObject;
                enemy.DestroyEnemy();
            }
            else
            {
                Debug.Log("Not an enemy");
            }
        }
        
        
    }
}
