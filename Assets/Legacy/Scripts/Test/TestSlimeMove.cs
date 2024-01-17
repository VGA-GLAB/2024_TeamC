using SoulRun.InGame;
using UniRx;
using UnityEngine;

public class TestSlimeMove : MonoBehaviour, IAlphaPausable
{
    public float moveSpeed = Settings.Instance.UpdateSpeed; // 移動速度
    public float jumpForce = 5f; // 跳ねる力
    public float stopTime = Settings.Instance.SlimeStopTime; // 停止時間
    [SerializeField] private float _activeDistance = 5;
    private GameObject _playerMover;
    private bool _active = false;

    private Rigidbody rb;
    private bool isMoving = false;
    private float moveTimer = 0f;
    private float originPosY = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerMover = FindAnyObjectByType<MockPlayerController>().gameObject;
        originPosY = transform.position.y;
        MessageBroker.Default.Receive<TestPause>().Subscribe(isPause => Pause(isPause.Paused));
        Pause(false);
    }

    void Update()
    {
        if (transform.position.z < -3) Destroy(gameObject);

        moveSpeed = Settings.Instance.UpdateSpeed; // 移動速度
        jumpForce = 5f; // 跳ねる力
        stopTime = Settings.Instance.SlimeStopTime; 

        if (Vector3.Distance(transform.position, _playerMover.transform.position) <= _activeDistance) 
            _active = true;
        
        if (_active)
        {
            if (!isMoving)
            {
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    JumpAndMove();
                    moveTimer = stopTime;
                }
            }
        }

        if (transform.position.y < originPosY)
        {
            transform.position = new Vector3(transform.position.x, originPosY, transform.position.z);
        }
    }

    void JumpAndMove()
    {
        rb.velocity = new Vector3(0, jumpForce, -moveSpeed);
        isMoving = true;
        Invoke("StopMoving", stopTime);
    }

    void StopMoving()
    {
        rb.velocity = Vector3.zero;
        isMoving = false;
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            _active = false;
            if (rb == null) return;
            rb?.Sleep();
        }
        else
        {
            _active = true;
            if (rb == null) return;
            rb?.WakeUp();
        }
    }
}
