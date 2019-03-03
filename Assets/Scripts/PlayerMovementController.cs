using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BounceKnockback
{
    Bounce,
    Knockback
}

public class PlayerMovementController : MonoBehaviour {

    private Transform _myTransform;
    private Rigidbody2D _myRB;
    private Animator _myAnimator;

    private float _initialGravityScale;

    private float _actualSpeed;
    public float ClimbSpeed = 2.0f;
    public float MovementSpeed = 2.0f;
    public float RunningSpeed = 4.0f;
    public float OnPlatformSpeedModifier = 0.75f;

    private float _jumpForce;
    public float JumpForceNormal;
    public float JumpForceTurbo;
    public float JumpSideForce;
    public LayerMask GroundLayerMask;
    public List<Transform> GroundCheck;

    private int _playerLayer;
    private int _platformLayer;
    //može se koristiti i layerMaska (više layera) za ove potrebe
    //pa definirati da se kroz layerMasku može prolaziti

    private bool _canMove;
    private bool _canJump;
    private bool _canClimb;
    private float _movementHorizontal; //right and left
    private float _climbingVertical; //up and down
    private bool _movementJump; //Space jump pressed
    private float _movingVertical; //Space jump released

    private bool _isMovingRight = true;

    private bool _isWalking = false;
    private bool _isGrounded = false;
    private bool _isCrouching = false;
    private bool _isClimbing = false;
    private bool _isJumping = false;
    private bool _isFalling = false;

    private void Awake()
    {
        _myTransform = transform;
        _myRB = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _playerLayer = gameObject.layer;
        _platformLayer = LayerMask.NameToLayer("Platform");
        _initialGravityScale = _myRB.gravityScale;
        _actualSpeed = MovementSpeed;
        _jumpForce = JumpForceNormal;
        _canMove = true;
        _canJump = true;
        _canClimb = false;
    }

    private void Update()
    {
        if (_canMove)
        {
            //HORIZONTAL MOVEMENT INPUT and ORIENTATION (pogledati dolje LateUpdate() metodu)
            _movementHorizontal = Input.GetAxisRaw("Horizontal");

            //CLIMBING and CROUCHING
            _climbingVertical = Input.GetAxisRaw("Vertical");

            //JUMP INPUT
            _movingVertical = _myRB.velocity.y; //parcijalno skakanje; uzimamo brzinu po y osi prije nego radimo išta sa inputom
                                                //to radimo kako bi to koristili za kretnju po y osi
                                                //i da možemo izjednačiti s nulom kada pustimo Jump tipku kako bi "dozirali" jačinu skoka parcijalnim pritiskom Jump tipke
            _movementJump = Input.GetButtonDown("Jump");

            //IGNORING PLATFORMS ON JUMP
            //AND HOLDING LEFT ALT KEY
            //TODO: zamijeniti getdownkey s Input axesom
            Physics2D.IgnoreLayerCollision(_playerLayer, _platformLayer, (_movingVertical > 0.0f) || Input.GetKey(KeyCode.LeftAlt));

            //WALKING BOOL
            if (_movementHorizontal != 0.0f)
                _isWalking = true;
            else
                _isWalking = false;

            //RUNNING SPEED
            //TODO: dodati animaciju za run
            //TODO: dodati Input ButtonDown ime, umjesto key code
            if (Input.GetKeyDown(KeyCode.LeftShift))
                _actualSpeed = RunningSpeed;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                _actualSpeed = MovementSpeed;

            //JUMP Linecast
            _isGrounded = CheckGround(GroundCheck);
            //_isGrounded = Physics2D.Linecast(_myTransform.position, GroundCheck.position, GroundLayerMask);

            //JUMP (samo ako smo grounded)
            if (_isGrounded) //ako isGrounded i stisak Jump tipke provjeravamo paralelno,
                             //onda pritiskanjem tipke Jump dok je player u zraku kočimo playerev pad (GetButtonUp niže)
                             //možemo dodati i provjeru brzine po y sa && _movingVertical > 0.0f
                             //u if(Input.GetButtonUp("Jump")) dolje
            {
                _isGrounded = true;
                _myAnimator.SetBool("IsGrounded", _isGrounded);

                _isFalling = false;
                _myAnimator.SetBool("IsFalling", _isFalling);

                _isJumping = false;
                _myAnimator.SetBool("IsJumping", _isJumping);

                if (_canJump)
                {
                    if (_movementJump)
                    {
                        _myRB.AddForce(Vector2.up * _jumpForce);

                        _isJumping = true;
                        _myAnimator.SetBool("IsJumping", _isJumping);

                        
                    }
                    if (Input.GetButtonUp("Jump")) //kada pustimo Jump tipku
                    {
                        _movingVertical = 0.0f; //srežemo brzinu po y na nula
                    }
                }
            } else {
                _isGrounded = false;
                _myAnimator.SetBool("IsGrounded", _isGrounded);

                //_isCrouching = false;
                //_myAnimator.SetBool("IsCrouching", _isCrouching);

                _isWalking = false;
                _myAnimator.SetBool("IsWalking", _isWalking);

               //FALLING        
                if (_myRB.velocity.y < 0)
                {
                    _isFalling = true;
                    _myAnimator.SetBool("IsFalling", _isFalling);

                    _isJumping = false;
                    _myAnimator.SetBool("IsJumping", _isJumping);
                }
                else if (_myRB.velocity.y >= 0)
                {
                    _isFalling = false;
                    _myAnimator.SetBool("IsFalling", _isFalling);
                }
            }

            //CLIMBING (samo kada smo na ljestvama)
            //TODO: provjeriti kada budem imao platformu iznad ljestvi
            //TODO2: provjeriti Animator i mogu li tranzicije iz Idle u PlayerClimbIdle biti bez IsGrounded parametra
            if (_canClimb)
            {
                if (_climbingVertical != 0)
                {
                    _isClimbing = true;
                    _myAnimator.SetBool("IsClimbing", _isClimbing);
                } else
                {
                    _isClimbing = false;
                    _myAnimator.SetBool("IsClimbing", _isClimbing);
                }
                _myRB.velocity = new Vector2(_movementHorizontal * _actualSpeed, _climbingVertical * ClimbSpeed);

                //Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, _climbingVertical > 0.0f);
            }
            //NORMAL WALK and NORMAL RUN
            else
            {
                _isClimbing = false;
                _myAnimator.SetBool("IsClimbing", _isClimbing);
                
                //ne treba nam množenje s Time.deltaTime, jer se rigidbody, fizika, izvršava uvijek u fiksnom vremenu neovisno o frejmu
                _myRB.velocity = new Vector2(_movementHorizontal * _actualSpeed, _movingVertical);
                //_myRB.velocity = new Vector2(_movementHorizontal * _actualSpeed, _myRV.velocity.y); //prije nego smo ubacili parcijalno skakanje

                _myAnimator.SetBool("IsWalking", _isWalking);

                //CROUCHING and CROUCH WALKING
                if(_climbingVertical < 0.0f)
                {
                    _isCrouching = true;
                    _myAnimator.SetBool("IsCrouching", _isCrouching);

                    _actualSpeed = MovementSpeed; //nema trčanja dok smo crouching
                    _jumpForce = JumpForceTurbo; //turbo jump iz crouching pozicije
                } else if(_climbingVertical >= 0.0f)
                {
                    _isCrouching = false;
                    _myAnimator.SetBool("IsCrouching", _isCrouching);

                    _jumpForce = JumpForceNormal;
                }
            }            
        }
    }

    //HORIZONTAL ORIENTATION
    private void LateUpdate()
    {
        //localscale na transformu je vektor i ne možemo mu mijenjati X komponentu,
        //već moramo definirati cijeli novi vektor kojemu možemo mijenjati X komponentu
        //pa ćemo transform vektoru (dolje) dodijeliti vrijednost tog novog vektora s postavljenom X komponentom
        Vector3 localScale = _myTransform.localScale;

        //ovisno kamo se krećemo tako smo i orijentirani
        if (_movementHorizontal > 0.0f)
            _isMovingRight = true;
        else if (_movementHorizontal < 0.0f)
            _isMovingRight = false;

        //ako se ne krećemo desno a lokalna skala je veća od nule (gledamo prema desno)
        //ili
        //ako se krećemo desno a lokalna skala je manja od nule (gledamo prema lijevo)
        //trebamo promijeniti orijentaciju skale
        if (
            (!_isMovingRight && (localScale.x > 0.0f))
            ||
            (_isMovingRight && (localScale.x < 0.0f)))
            localScale.x *= -1;

        _myTransform.localScale = localScale;
    }

    public void EditMove(bool canMove)
    {
        _canMove = canMove;
    }

    public void ChangeSpeed()
    {

    }

    private bool CheckGround(List<Transform> groundCheck)
    {
        bool check;
        for (int i = 0; i < groundCheck.Count; i++)
        {
            check = Physics2D.Linecast(_myTransform.position, GroundCheck[i].position, GroundLayerMask);
            if (check)
                return true;
        }
        return false;
    }

    //TRIGGERING THE LADDERS AND CLIMBING (samo na ljestvama)
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _canClimb = true;
            _canJump = false; //JUMP BLOCK WHILE CLIMBING
            _isWalking = false;
            _isCrouching = false;
            _myRB.gravityScale = 0.0f;
            _myAnimator.SetBool("CanClimb", _canClimb);
            _myAnimator.SetBool("IsWalking", _isWalking);
            _myAnimator.SetBool("IsCrouching", _isCrouching);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _canClimb = false;
            _canJump = true;
            _myRB.gravityScale = _initialGravityScale;
            //moram velocity.y izjednačit s nulom, inače zadrži raniju brzinu
            _myRB.velocity = new Vector2(_movementHorizontal * _actualSpeed, 0.0f);
            _myAnimator.SetBool("CanClimb", _canClimb);
        }
    }

    //MOVING WITH MOVING PLATFORMS and SLOWING THE MOVEMENT
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
            _myTransform.parent = collision.transform;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
            _actualSpeed = MovementSpeed * OnPlatformSpeedModifier;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            _myTransform.parent = null;
            _actualSpeed = MovementSpeed;
        }   
    }

    //BOUNCING OFF ENEMIES AND KNOCKBACK FROM ENEMIES
    public void Bounce(float bounceForce, BounceKnockback bounceknock)
    {
        if(bounceknock == BounceKnockback.Bounce)
            _myRB.velocity = new Vector2(_movementHorizontal * _actualSpeed, bounceForce);
        if(bounceknock == BounceKnockback.Knockback)
            _myRB.velocity = new Vector2(bounceForce, bounceForce);

        ////_myRB.velocity = new Vector2(_movementHorizontal * _actualSpeed, bounceForce);

        //_myRB.AddForce(Vector2.up*bounceForce, ForceMode2D.Impulse); //zanemari masu
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;        
        Gizmos.DrawLine(transform.position, GroundCheck[0].position);
        Gizmos.DrawLine(transform.position, GroundCheck[1].position);
        Gizmos.DrawLine(transform.position, GroundCheck[2].position);
        //Gizmos.DrawLine(transform.position, GroundCheck[GroundCheck.Count-1].position);
        //ne _transform.position, jer on još nije iniciran - ova metoda se pokreće u editoru, prije startanja igre
        //u DrawGizmos se ne koriste lokalne varijable
    }
}
