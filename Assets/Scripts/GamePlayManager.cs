using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{ 
    public Image m_fillImage;
    public Text scoretxt, scoretxt2, bestScoretxt;
    public Slider timeSlider;

    //GamePlay related properties
    private Transform[] m_AllPatterns;
    private string correctColorTag = string.Empty;
    private Color correctColor = Color.black;

    private int score = -1;
    private int highScore = 0;

    private void Start()
    {
        m_AllPatterns = GetTopLevelChildren(transform);
        highScore = PlayerPrefs.GetInt("Score", 0);
        HideAllPatterns();
        UpdateScoreUI();
        timeSlider.value = 1;
    }

    private Transform[] GetTopLevelChildren(Transform Parent)
    {
        Transform[] Children = new Transform[Parent.childCount];
        for (int ID = 0; ID < Parent.childCount; ID++)
        {
            Children[ID] = Parent.GetChild(ID);
        }
        return Children;
    }

    private void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit != null && hit.collider != null)
            {
               if(hit.collider.gameObject.tag == correctColorTag)
                {
                    //you wing
                    OnTouchPassed();
                }
               else
                {
                    //you loose.
                    ScenesManager.instance.ShowFinalScene();
                }


            }
        }

        if (ScenesManager.instance.GameplayScene.activeInHierarchy)
        {
            if (ScenesManager.isplaying)
            {
                if(score > 20)
                {
                    timeSlider.value -= Time.deltaTime * 2;
                }
                else
                    timeSlider.value -= (Time.deltaTime * (score + 1))/20;

                if(timeSlider.value <= 0)
                {
                    OnTouchFailed();
                }
            }
        }
    }
    private void HideAllPatterns()
    {
        foreach (Transform item in m_AllPatterns)
        {
            item.gameObject.SetActive(false);
        }
    }


    public void OnTouchPassed()
    {
        //Generate a random pattern on and hide others
        HideAllPatterns();
        Transform randomPattern = m_AllPatterns[Random.Range(0, m_AllPatterns.Length)];
        randomPattern.gameObject.SetActive(true);

        Transform randomCircle = randomPattern.GetChild(Random.Range(0, randomPattern.childCount));
        correctColorTag = randomCircle.tag;// for checking correctness in next loop.
        m_fillImage.color = randomCircle.GetComponent<SpriteRenderer>().color;
        //set the color of slider 

        // increase the score.
        score++;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("Score", highScore);
        }
        UpdateScoreUI();
        ResetTimeSlider();
    }

    private void UpdateScoreUI()
    {
        if (score < 0)
        {
            scoretxt.text = "0";
            scoretxt2.text = "0";
        }
        else
        {
            scoretxt.text = score.ToString();
            scoretxt2.text = score.ToString();
        }

        bestScoretxt.text = highScore.ToString();
    }

    private void ResetTimeSlider()
    {
        timeSlider.value = 1;
    }

    public void OnTouchFailed()
    {
        //load the final sceen
        ScenesManager.instance.ShowFinalScene();

        ResetTimeSlider();
    }
}
