using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeSpanHandler : MonoBehaviour {

    public Text Value;
    public Slider slider;
    public float maxSeconds;
    public float percentage;

	// Use this for initialization
	void Start () {
        percentage = maxSeconds;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateText();
        UpdateSlider();
	}

    private void UpdateText()
    {
        percentage -= Time.deltaTime;
        string text = string.Format("{0:0.00}", percentage) + "%";
        Value.text = text;
    }

    private void UpdateSlider()
    {
        float newValue = percentage / maxSeconds;
        slider.value = newValue;
    }

    public void resetLifeSpan()
    {
        percentage = maxSeconds;
    }
}
