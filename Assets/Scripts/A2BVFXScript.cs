using UnityEngine;

public class A2BVFXScript : MonoBehaviour
{
    [HideInInspector] public Vector3 Destination;
    [SerializeField] private float _flySpeed;
    private Vector3 _startingPosition;
    private float _timer;
    private float _easedTimer;
    void Start()
    {
        _startingPosition = transform.position;
        _timer = Time.time;
    }

    void Update()
    {
        _easedTimer = Mathf.SmoothStep(0, 1, (Time.time - _timer) * _flySpeed);
        transform.position = Vector3.Lerp(_startingPosition, Destination, _easedTimer);
        if (Vector3.Distance(transform.position, Destination) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
