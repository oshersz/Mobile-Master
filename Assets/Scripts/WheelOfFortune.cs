using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    public static WheelOfFortune wheel {get;private set;}
    [SerializeField] RewardSpin[] rewardSpins;

    private void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int doWeWin = Random.Range(0, 3);

            if (doWeWin == 0) //when wining. win chance is 33% currently
            {
                Prizes prize = (Prizes)Random.Range(0, 5);
                for (int i = 0; i < rewardSpins.Length; i++)
                {
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
                }
            }

        }
    }
}
