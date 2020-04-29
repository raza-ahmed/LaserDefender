﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int score = 0;
    private Text myText;

    void Start(){
       myText = GetComponent<Text>();
    }

    public void Score(int points) {
        Debug.Log("Score Points");
        score += points;
        myText.text = score.ToString();
    }

    public void Reset(){
        score = 0;
        myText.text = score.ToString();
    }
}