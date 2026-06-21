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
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
