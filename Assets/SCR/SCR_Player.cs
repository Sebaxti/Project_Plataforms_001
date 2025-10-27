using UnityEngine;

public class SCR_Player : MonoBehaviour
{
    /*----public----*/
    public enum Estados {Walk,Attack,Idle,Jump,Sprint,Dash }
    public float velocidad, fuerzaSalto, velocidadSprint, fuerzaDash, duracionDash,tiempoMaximoDblShift, velocidadRotacion, fuerzaCaida;
    public Estados myState;
    public GameObject visor, modeloVisual;
    public bool onGround, onPlataform;

    /*----private----*/
    private bool enMovimiento , dblSalto, enDash;
    private Rigidbody rb;
    private int dblSaltoCont;
    private float tiempoUltimoShift, tiempoDash, rotacionObjetivo;
    private Vector3 direccionDash,ultimaDireccion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dblSalto =true;
        rb = GetComponent<Rigidbody>();
        myState = Estados.Idle;

        enMovimiento = false;
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
                dblSaltoCont++;
                print(dblSaltoCont);
                break;
            case Estados.Dash:
                Dashing();
                break;
            default:
                print("bye");
                break;

        }
        
        if(modeloVisual !=null)
        {
            float rotacionActual = modeloVisual.transform.eulerAngles.y;
            float nuevaRotacion = Mathf.LerpAngle(rotacionActual, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
            modeloVisual.transform.eulerAngles = new Vector3(0, nuevaRotacion, 0);
        }

        if (dblSaltoCont>1)
        {
            dblSalto = false;
        }

        Debug.DrawRay(transform.position, visor.transform.forward);
    }
    void DetectarDblShift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift))
        {
            float tiempoActual = Time.time;

            if(tiempoActual- tiempoUltimoShift < tiempoMaximoDblShift)
            {
                if (myState != Estados.Dash && myState != Estados.Jump && onGround || onPlataform)
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

        if (dblSalto==true && (onPlataform == true||onGround == true) && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Jump");
        }
        
    }

    void Walking()
    {
        

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Rotacion Y del transform: " + transform.eulerAngles.y);
            rotacionObjetivo = 0;
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.forward;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotacion Y del transform: " + transform.eulerAngles.y);
            rotacionObjetivo = 90;
            transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.right;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Rotacion Y del transform: " + transform.eulerAngles.y);
            rotacionObjetivo = 180;
            transform.Translate(Vector3.back * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.back;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotacion Y del transform: " + transform.eulerAngles.y);
            rotacionObjetivo = 270;
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

        if (dblSalto == true && (onPlataform == true || onGround == true) && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Jump");
        }
        if (rb.linearVelocity.y < 5 || rb.linearVelocity.y>10)
        {
            rb.AddForce(Vector3.down*fuerzaCaida,ForceMode.Acceleration);
        }
    }


    void Sprinting()
    {
        bool enMovimiento = false;
        if (Input.GetKey(KeyCode.W))
        {
            rotacionObjetivo = 0;
            transform.Translate(Vector3.forward * velocidadSprint * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.forward;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotacionObjetivo = 90;
            transform.Translate(Vector3.right * velocidadSprint * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.right;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotacionObjetivo = 180;
            transform.Translate(Vector3.back * velocidadSprint * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.back;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotacionObjetivo = 270;
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

        if (dblSalto == true && (onPlataform == true || onGround == true) && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Jump");
        }
        if (rb.linearVelocity.y < 5)
        {
            rb.AddForce(Vector3.down * fuerzaCaida, ForceMode.Acceleration);
        }

    }
    void Jumping()
    {

        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        if (dblSalto == false)
        {
            onGround = false;
            onPlataform = false;
        }
        myState = Estados.Walk;
        
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
            Debug.Log("Tocando suelo");
            onGround = true;
            dblSalto = true;
            dblSaltoCont = 0;
        }
        if (collision.gameObject.CompareTag ("plataforma"))
        {
            Debug.Log("Tocando plataforma");
            onPlataform = true;
            dblSalto = true;
            dblSaltoCont = 0;
            transform.SetParent (collision.transform);
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            if (dblSalto==false)
            {
                Debug.Log("En el aire");
                onGround = false;
            }
            
        }
        if (collision.gameObject.CompareTag("plataforma"))
        {
            if (dblSalto==false)
            {
                Debug.Log("En el aire");
                onPlataform = false;
            }
            
            transform.SetParent(null);
        }
    }
    
}
