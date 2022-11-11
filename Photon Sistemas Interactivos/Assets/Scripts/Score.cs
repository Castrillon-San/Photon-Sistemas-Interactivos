using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    private Text _text;

    void Start()
    {
        score = 0;
        _text = GetComponent<Text>();
    }

    void Update()
    {
        if (GetComponentInParent<ScoreWriter>()._isWritingScore == true)
        {
            _text.text = score.ToString();
        }
    }
}
