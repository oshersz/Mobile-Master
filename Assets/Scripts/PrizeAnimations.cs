using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrizeAnimations : MonoBehaviour
{
    public static PrizeAnimations anim { get; private set; }

    [SerializeField] Transform animationsStartingPos;

    private WheelOfFortune WOF;

    [HideInInspector] public int spinsCompleted;
    [SerializeField] Sprite[] prizeSprites;
    [SerializeField] GameObject[] UIIcons;
    [SerializeField] GameObject a2BVFXPrefab;
    [SerializeField] GameObject trailPrefab;

    [SerializeField] GameObject currencyAnimationPrefab;
    [SerializeField] Animator coinsAnim;
    [SerializeField] TextMeshProUGUI coinsAnimationText;
    [SerializeField] ParticleSystem coinsVFX;

    [SerializeField] Animator diamondWinning;

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI diamondsText;
    [SerializeField] Image[] starsSprites;

    private int coins = 1600;
    private int diamonds = 80;
    private int starsAmount = 0;

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

        coinsText.text = coins.ToString();
        diamondsText.text = diamonds.ToString();
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
        if (winningPrize == Prizes.Diamond)
        {
            yield return new WaitForSeconds(0.75f);
        }

        int prizeAmount;
        if (winningPrize == Prizes.Coin)
        {
            //prizeAmount = Random.Range(1, 6);
            //prizeAmount = 3;
            coinsVFX.Play();
        }
        else
        {
            prizeAmount = 1;

            for (int i = 0; i < prizeAmount; i++)
            {
                GameObject effectTrail = Instantiate(trailPrefab, animationsStartingPos);
                GameObject A2BEffect = Instantiate(a2BVFXPrefab, animationsStartingPos);
                A2BEffect.GetComponent<Image>().sprite = prizeSprites[(int)winningPrize];

                if (winningPrize == Prizes.Star)
                {
                    starsAmount++;
                    if (starsAmount > 3)
                    {
                        starsAmount = 3;
                    }
                    A2BEffect.GetComponent<A2BVFXScript>().Destination = starsSprites[starsAmount - 1].transform.position;
                }
                else
                {
                    A2BEffect.GetComponent<A2BVFXScript>().Destination = UIIcons[(int)winningPrize].transform.position; //_UIIcons[iconIndex].position
                }
                effectTrail.GetComponent<TrailScript>().gameObjectToFollow = A2BEffect.transform;

                //_audioSource.PlayOneShot(_rewardSFX);
                yield return new WaitForSeconds(0.125f); //0.125
            }

        }


        yield return new WaitForSeconds(0.875f); //waiting for the A2B animation to complete

        if (winningPrize == Prizes.Coin)
        {
            GameObject effect = Instantiate(currencyAnimationPrefab, coinsAnim.transform);
            effect.GetComponentInChildren<TextMeshProUGUI>().text = "+250";
            Destroy(effect, 0.5f);
            StartCoroutine(CoinAnimationRoutine(250));
        }
        else if (winningPrize == Prizes.Currency)
        {
            GameObject effect = Instantiate(currencyAnimationPrefab, coinsAnim.transform);
            effect.GetComponentInChildren<TextMeshProUGUI>().text = "+100";
            Destroy(effect, 0.5f);
            StartCoroutine(CoinAnimationRoutine(100));
        }
        else if (winningPrize == Prizes.Diamond)
        {
            /*
            GameObject effect = Instantiate(currencyAnimationPrefab, diamondsText.transform);
            effect.GetComponentInChildren<TextMeshProUGUI>().text = "+1";
            effect.transform.localScale = Vector3.one * 0.5f;
            Destroy(effect, 0.5f);
            */
            diamonds++;
            diamondsText.text = diamonds.ToString();
        }
        else if (winningPrize == Prizes.Star)
        {
            //starsAmount++;
            UpdateStars();
        }
        else if (winningPrize == Prizes.Lock)
        {
            //
        }

        yield return null;
    }

    private void UpdateStars()
    {
        for (int i=0;i<starsAmount;i++)
        {
            starsSprites[i].sprite = prizeSprites[(int)Prizes.Star];
        }
    }


    public IEnumerator CoinAnimationRoutine(int goldIncrease)
    {
        coins += goldIncrease;
        coinsAnimationText.text = coins.ToString();
        coinsAnim.Play("Gold Gain");
        yield return new WaitForSeconds(0.25f);
        coinsText.text = coins.ToString();

        yield return null;
    }
}
