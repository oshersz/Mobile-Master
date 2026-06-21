using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RewardSpin : MonoBehaviour
{
    [SerializeField] GameObject[] prizes;
    [SerializeField] Sprite[] prizeSprites;
    private float spinSpeed;
    public float speedAccel;
    private float accelFactor;
    private float spinTime;

    private float tempSpeedAccel;
    
    [HideInInspector]public Prizes winningPrize;
    void Start()
    {
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spinTime = Random.Range(1,2f); //5 //3-7
            spinSpeed = 0;
            speedAccel = Random.Range(5,20f);//10 //20
            tempSpeedAccel = -speedAccel;
            accelFactor = (speedAccel * 2) / spinTime;
            //accelAccelerator = 0; //2
        }
        if (spinTime>0 || speedAccel < tempSpeedAccel) //
        {
            if (speedAccel < tempSpeedAccel)
                speedAccel = tempSpeedAccel;

            spinSpeed += speedAccel;
            speedAccel -= accelFactor * Time.deltaTime; //5 //8

            spinTime -= Time.deltaTime;
            if (spinTime <= 0)
            {
                float closestPrizeLength = float.MaxValue;
                for (int i = 0; i < prizes.Length; i++)
                {
                    if (Mathf.Abs(prizes[i].transform.localPosition.y) < closestPrizeLength)
                    {
                        winningPrize = prizes[i].GetComponent<Prize>().prize; //for later purposes
                        closestPrizeLength = prizes[i].transform.localPosition.y;
                    }
                }
                //after finding the closest, apply it for everyone to snap it to the middle
                for (int i = 0; i < prizes.Length; i++)
                {
                    prizes[i].transform.localPosition -= Vector3.up * closestPrizeLength;
                    if (prizes[i].transform.localPosition.y > 500)
                    {
                        int temp = (i - 1) % 5;
                        if (temp == -1)
                            temp = 4;
                        prizes[i].transform.localPosition = new Vector3(0, prizes[temp].transform.localPosition.y - 256, 0);
                    }
                }
                return;
            }

            for (int i=0;i<prizes.Length;i++)
            {
                //prizes[i].transform.position -= Vector3.up * spinSpeed * Time.deltaTime;
                prizes[i].transform.localPosition = new Vector3 (0, prizes[i].transform.localPosition.y - (spinSpeed * Time.deltaTime),0);
                if (prizes[i].transform.localPosition.y<-500)
                {
                    //prizes[i].transform.localPosition = prizes[(i + 1) % 5].transform.localPosition + Vector3.up * 256;
                    prizes[i].transform.localPosition = new Vector3(0,prizes[(i + 1) % 5].transform.localPosition.y + 256,0);
                }
            }

        }
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
