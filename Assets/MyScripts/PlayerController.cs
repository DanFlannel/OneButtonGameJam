using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Animator anim;
    bool clicked;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            /*if (!clicked)
            {
                StartCoroutine(continueCombo());
            }*/
            anim.SetTrigger(CustomStrings.Continue);
            
        }
	}

    IEnumerator continueCombo()
    {
        clicked = true;
        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
        float totalTime = animInfo.length;
        totalTime *= animInfo.normalizedTime;

        anim.SetBool(CustomStrings.Continue, true);
        yield return new WaitForSeconds(totalTime);
        anim.SetBool(CustomStrings.Continue, false);
        clicked = false;
    }
}
