using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [Header("física")]
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] Vector2 _move;
    [SerializeField] float _speed;

    [SerializeField] float _forceJump;

    [SerializeField] bool _checkGround;

    [SerializeField] int _numbSort;



    void Start()

    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.linearVelocity = new Vector2(_move.x * _speed, _rb.linearVelocity.y);
    }

    void Jump()
    {
        _rb.linearVelocityY = 0;
        _rb.AddForceY(_forceJump);
    }


    public void SetMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector2>();
    }
 

    #region checagem
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            if (collision.GetComponent<GroundJumpControl>())
            {
                GroundJumpControl groundJump = collision.gameObject.GetComponent<GroundJumpControl>();

                if (groundJump._numbCor == _numbSort || groundJump._numbCor == 0)
                {
                    Jump();
                    _checkGround = true;

                    _numbSort = Random.Range(1, 5);
                }
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            _checkGround = false;

        }
    }
    #endregion



}
