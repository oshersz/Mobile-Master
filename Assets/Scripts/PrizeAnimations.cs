using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrizeAnimations : MonoBehaviour
{
    public static PrizeAnimations anim { get; private set; }

    private WheelOfFortune WOF;

    [HideInInspector] public int spinsCompleted;
    [SerializeField] Sprite[] prizeSprites;
    [SerializeField] GameObject[] UIIcons;
    [SerializeField] GameObject a2BVFXPrefab;

    [SerializeField] Animator diamondWinning;

    private void Awake()
    {
        WOF = GetComponent<WheelOfFortune>();

        if (anim == null)
        {
            anim = this;
        }
        if (anim != this)
        {
            Destroy(anim);
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

            if (WOF.currentPrizes[0] == WOF.currentPrizes[1] && WOF.currentPrizes[1] == WOF.currentPrizes[2])
            {
                if (WOF.currentPrizes[0] == Prizes.Diamond)
                {
                    diamondWinning.Play("Diamond Spin");
                }
                StartCoroutine(PrizeWinRoutine(WOF.currentPrizes[0]));
            }
        }
    }

    private IEnumerator PrizeWinRoutine(Prizes winningPrize)
    {
        //yield return new WaitForSeconds(1.5f);
        int prizeAmount;
        if (winningPrize == Prizes.Coin)
        {
            prizeAmount = Random.Range(1, 6);
        }
        else
        {
            prizeAmount = 1;
        }

        for (int i = 0; i < prizeAmount; i++)
        {
            GameObject A2BEffect = Instantiate(a2BVFXPrefab, transform);
            A2BEffect.GetComponent<Image>().sprite = prizeSprites[(int)winningPrize];
            A2BEffect.GetComponent<A2BVFXScript>().Destination = UIIcons[(int)winningPrize].transform.position; //_UIIcons[iconIndex].position
            //_audioSource.PlayOneShot(_rewardSFX);
            yield return new WaitForSeconds(0.125f); //0.125
        }
        yield return null;
    }
}
