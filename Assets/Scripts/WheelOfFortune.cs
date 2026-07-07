using System.Collections;
using UnityEngine;

using UnityEngine.UI;

public class WheelOfFortune : MonoBehaviour
{
    public static WheelOfFortune wheel {get;private set;}
    [SerializeField] RewardSpin[] rewardSpins;


    private int spinsCompleted;
    private Prizes[] currentPrizes;
    [SerializeField] Sprite[] prizeSprites;
    [SerializeField] GameObject[] UIIcons;
    [SerializeField] GameObject a2BVFXPrefab;

    [SerializeField] Animator diamondWinning;



    private void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && spinsCompleted == 0)
        {
            spinsCompleted = -10;

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

    public void RewardSpinFinished()
    {
        if (spinsCompleted == -10)
        {
            spinsCompleted = 1;
        }
        else
        {
            spinsCompleted++;
        }
        

        if (spinsCompleted == 3)
        {
            spinsCompleted = 0;

            if (currentPrizes[0] == currentPrizes[1] && currentPrizes[1] == currentPrizes[2])
            {
                if (currentPrizes[0] == Prizes.Diamond)
                {
                    diamondWinning.Play("Diamond Spin");
                }
                StartCoroutine(PrizeWinRoutine(currentPrizes[0]));
            }
        }
    }

    private IEnumerator PrizeWinRoutine(Prizes winningPrize)
    {
        //yield return new WaitForSeconds(1.5f);

        int diamondsAmount = Random.Range(1, 4);
        for (int i = 0; i < diamondsAmount; i++)
        {
            GameObject A2BEffect = Instantiate(a2BVFXPrefab, transform);
            A2BEffect.GetComponentInChildren<Image>().sprite = prizeSprites[(int)winningPrize];
            A2BEffect.GetComponent<A2BVFXScript>().Destination = UIIcons[(int)winningPrize].transform.position; //_UIIcons[iconIndex].position
            //_audioSource.PlayOneShot(_rewardSFX);
            yield return new WaitForSeconds(0.125f);
        }
        yield return null;
    }
}
