using System.Collections;
using UnityEngine;

using UnityEngine.UI;

public class WheelOfFortune : MonoBehaviour
{
    public static WheelOfFortune wheel {get;private set;}
    [SerializeField] RewardSpin[] rewardSpins;
    private PrizeAnimations PA;
    [HideInInspector] public Prizes[] currentPrizes;




    private void Awake()
    {
        PA = GetComponent<PrizeAnimations>();

        currentPrizes = new Prizes[3];

        if (wheel == null)
        {
            wheel = this;
        }
        if (wheel!=this)
        {
            Destroy(wheel);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PA.spinsCompleted == 0)
        {
            PA.spinsCompleted = -10;

            StartCoroutine(PA.CoinAnimationRoutine(-100));

            //int doWeWin = Random.Range(0, 3);
            int doWeWin = 0;

            if (doWeWin == 0) //when wining. win chance is 33% currently
            {
                Prizes prize = (Prizes)Random.Range(0, 5);
                for (int i = 0; i < rewardSpins.Length; i++)
                {
                    currentPrizes[i] = prize;
                    rewardSpins[i].Spin(prize);
                }
            }
            else
            {
                Prizes[] prizes = new Prizes[3];
                while (prizes[0] == prizes[1] && prizes[1] == prizes[2])
                {
                    prizes[0] = (Prizes)Random.Range(0, 5);
                    prizes[1] = (Prizes)Random.Range(0, 5);
                    prizes[2] = (Prizes)Random.Range(0, 5);
                }
                for (int i = 0; i < rewardSpins.Length; i++)
                {
                    rewardSpins[i].Spin(prizes[i]);
                    currentPrizes[i] = prizes[i];
                }
            }
        }
    }
}
