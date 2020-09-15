using UnityEngine;

[System.Serializable]
public class Sentence
{
    public int id;
    public float waitTime = 0.2f;
    public float writingSpeed = 0.001f;

    [TextArea(3, 6)]
    public string sentence;
    // flaga czy coś czy ostatni dymek bota

    public int[] idAnswers;
}