using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class RewardSpin : MonoBehaviour
{
    [SerializeField] GameObject[] prizes;
    [SerializeField] Sprite[] prizeSprites;
    [HideInInspector]public Prizes winningPrize;

    private Animator anim;

    private Vector3[] startingPos;
    void Start()
    {
        anim = GetComponent<Animator>();

        startingPos = new Vector3[5];
        for (int i = 0;i<startingPos.Length;i++)
        {
            startingPos[i] = prizes[i].transform.localPosition;
        }

        List<int> prizeList = new List<int>();

        for (int i = 0; i < prizeSprites.Length; i++)
        {
            prizeList.Add(i);
        }

        for (int i = 0; i < prizeSprites.Length; i++)
        {
            int selectedPrize = Random.Range(0, prizeList.Count);
            prizes[i].GetComponent<Image>().sprite = prizeSprites[prizeList[selectedPrize]];
            prizes[i].GetComponent<Prize>().prize = (Prizes)prizeList[selectedPrize];
            prizeList.RemoveAt(selectedPrize);
        }

    }

    public void Spin(Prizes winner)
    {
        StartCoroutine(SpinRoutine(winner));
    }

    private IEnumerator SpinRoutine(Prizes winner)
    {
        float waitTime = Random.Range(0, 0.65f);

        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < startingPos.Length; i++)
        {
            prizes[i].transform.localPosition = startingPos[i];
        }

        anim.enabled = true;
        anim.Play("Spin Animation",0,0);

        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        //float realDuration = clipInfo[0].clip.length / stateInfo.speed;

        yield return new WaitForSeconds(1); //realDuration * 3

        anim.enabled = false;

        for (int i = 0; i < startingPos.Length; i++)
        {
            prizes[i].transform.localPosition = startingPos[i];
        }

        for (int i = 0; i < prizes.Length; i++)
        {
            if (prizes[i].GetComponent<Prize>().prize == winner)
            {
                Vector3 tempPos = prizes[2].transform.localPosition;
                prizes[2].transform.localPosition = prizes[i].transform.localPosition;
                prizes[i].transform.localPosition = tempPos;
                break;
            }
        }

        WheelOfFortune.wheel.RewardSpinFinished();
        //WheelOfFortune.spinsCompleted++;
    }

}

public enum Prizes
{
    Coin,
    Star,
    Diamond,
    Lock,
    Currency

}