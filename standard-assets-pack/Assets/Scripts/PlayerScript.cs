using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    // we need some sort of currency and a display text to display it
    public Text scoreText;
    public int money;
    public GameObject buildingPrefab;
    public GameObject fencePrefab;

    private Animator m_Animator;

    // Use this for initialization
    void Start() {
        money = 1000;
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = money.ToString();
    }

    public void FeedPressed()
    {
        m_Animator.SetTrigger("FeedPressed");
    }

    public void SetInteger(int feedSpeedInt)
    {
        m_Animator.SetInteger("FeedSpeedInt", feedSpeedInt);
    }
}
