using UnityEngine;

public class SCR_Player : MonoBehaviour
{
    /*----public----*/
    public enum Estados {Walk,Attack,Idle,Jump,Sprint,Dash }
    public float velocidad, fuerzaSalto, velocidadSprint, fuerzaDash, duracionDash,tiempoMaximoDblShift;
    public Estados myState;
    public GameObject visor;
    public bool onGround;

    /*----private----*/
    private Animator animador;
    private Rigidbody rb;
    private bool enDash;
    private float tiempoUltimoShift, tiempoDash;
    private Vector3 direccionDash,ultimaDireccion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animador = GetComponent<Animator>();
        myState = Estados.Idle;

        tiempoUltimoShift = 0f;
        enDash = false;
        tiempoDash = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        DetectarDblShift();

        switch (myState)
        {
            case Estados.Idle:
                Idleing();
                break;
            case Estados.Walk:
                Walking();
                break;
            case Estados.Sprint:
                Sprinting();
                break;
            case Estados.Jump:
                Jumping();
                break;
            case Estados.Dash:
                Dashing();
                break;
            default:
                print("bye");
                break;

        }

        Debug.DrawRay(visor.transform.position, transform.forward);
    }
    void DetectarDblShift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift))
        {
            float tiempoActual = Time.time;

            if(tiempoActual- tiempoUltimoShift < tiempoMaximoDblShift)
            {
                if (myState != Estados.Dash && myState != Estados.Jump && onGround)
                {
                    IniciarDash();
                }
            }
            tiempoUltimoShift= tiempoActual;

        }

    }
    void Idleing()
    {
        //animador.Play("Anim_Idle_P1");
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                myState = Estados.Sprint;
            }
            else
            {
                myState = Estados.Walk;
                Debug.Log("Walking");
            }
        }

        if (onGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Jump");
        }
    }

    void Walking()
    {
        bool enMovimiento = false;

        if (Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.forward;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.right;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.back * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.back;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.left;
            enMovimiento = true;
        }

        if (enMovimiento && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            myState = Estados.Sprint;
            Debug.Log("Sprint");
        }


        if (!enMovimiento)
        {
            myState = Estados.Idle;
            Debug.Log("Idle");
        }

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Jump");
        }
    }

    void Jumping()
    {
        animador.SetTrigger("Jump");
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        onGround = false;
        myState = Estados.Walk;
        
    }

    void Sprinting()
    {
        bool enMovimiento = false;
        if (Input.GetKey(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.forward * velocidadSprint * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.forward;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            transform.Translate(Vector3.right * velocidadSprint * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.right;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.back * velocidadSprint * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.back;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            transform.Translate(Vector3.left * velocidadSprint * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.left;
            enMovimiento = true;
        }

        if (enMovimiento && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            myState = Estados.Walk;
            Debug.Log("Walk");
        }
        if (!enMovimiento)
        {
            myState = Estados.Idle;
            Debug.Log("Idle");
        }

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Jump");
        }

    }

    void IniciarDash()
    {
        myState = Estados.Dash;
        enDash = true;
        tiempoDash = 0f;

        direccionDash = ultimaDireccion;
        Debug.Log("Dash");
    }

    void Dashing()
    {
        tiempoDash += Time.deltaTime;

        if (tiempoDash < duracionDash)
        {
            rb.linearVelocity = new Vector3(direccionDash.x * fuerzaDash, rb.linearVelocity.y, direccionDash.z * fuerzaDash);
        }
        else 
        {
            enDash = false;
            rb.linearVelocity = new Vector3(0, 0, 0);
            myState = Estados.Walk;
            Debug.Log("Dash terminado");
        }
    }
    
    void setState(Estados newState)
    {
        myState=newState;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag ("suelo"))
        {
            Debug.Log("Tocando");
            onGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            Debug.Log("En el aire");
            onGround = false;
        }
    }
    
}
