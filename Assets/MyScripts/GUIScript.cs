using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIScript : MonoBehaviour {

    public Text Health;
    public Text Kills;
    public GameObject Tutorial;

    public GameObject DeathPanel;

    public bool isDead;
    private int kills;
    private int doubleClick;

	// Use this for initialization
	void Start () {
        doubleClick = 0;
        isDead = false;
        Time.timeScale = 0;
        if (DeathPanel.activeInHierarchy)
        {
            DeathPanel.SetActive(false);
        }

        if(!Tutorial.activeInHierarchy)
        {
            Tutorial.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            Tutorial.SetActive(false);
            Time.timeScale = 1f;
        }

        if (isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                doubleClick++;
                if(doubleClick == 2)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
	}

    public void addKill()
    {
        if (!isDead)
        {
            kills++;
            Kills.text = kills.ToString();
        }
    }

    public void UpdateHealth(int n)
    {
        if(n <= 0)
        {
            isDead = true;
            n = 0;
        }
        Health.text = n.ToString();
    }

    public void GameOver()
    {
        DeathPanel.SetActive(true);
    }
}
