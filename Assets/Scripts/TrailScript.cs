using UnityEngine;

public class TrailScript : MonoBehaviour
{
    public Transform gameObjectToFollow;
    private float stopFollowTime;

    private void Start()
    {
        Destroy(gameObject, 1.75f);
        stopFollowTime = Time.time + 1;
    }

    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (Time.time<stopFollowTime)
        {
            transform.position = gameObjectToFollow.position;
        }
    }
}
