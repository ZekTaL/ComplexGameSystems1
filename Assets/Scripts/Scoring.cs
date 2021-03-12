using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private List<Joint> joints;
    private float currentScore = 0;
    private bool isStrike = false;
    private int pinCount = 0;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    public List<GameObject> bowlingPins;

    public  float minForceForScore = 3500;

    public Game game;

    // Start is called before the first frame update
    void Start()
    {
        joints = new List<Joint>(GetComponentsInChildren<Joint>());
        isStrike = false;
    }

    void Update()
    {
        pinCount = 0;

        foreach (GameObject pin in bowlingPins)
        {
            if (pin.transform.up.y < 0.5)
                pinCount++;
        }

        if (pinCount == 10 && !isStrike)
        {
            isStrike = true;
            StartCoroutine(game.Strike());
            Debug.Log("Strike!!!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forceBeingApplied = 0;

        foreach (Joint joint in joints)
        {
            forceBeingApplied += joint.currentForce.magnitude;
        }

        if (forceBeingApplied > minForceForScore)
        {
            currentScore += (forceBeingApplied * Time.deltaTime);
            scoreText.text = "Score: " +  Mathf.RoundToInt(currentScore).ToString();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "Score: 0";
    }
}
