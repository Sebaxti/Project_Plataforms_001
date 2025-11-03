using UnityEngine;

public class SCR_Player : MonoBehaviour
{
    /*----public----*/
    public enum Estados {Walk,Attack,Idle,Jump,Sprint,Dash }
    public float velocidad, fuerzaSalto, velocidadSprint, fuerzaDash, duracionDash, tiempoMaximoDblShift, velocidadRotacion, gravedadExtra, gravedadBajaSalto, alturaMaximaSalto;
    public GameObject visor, modeloVisual;
    

    /*----private----*/
    private Estados myState;
    private bool onGround, onPlataform;
    private bool enMovimiento , enDash;
    private int saltosDisponibles = 2, saltosRestantes;
    private Rigidbody rb;
    private float tiempoUltimoShift, tiempoDash, rotacionObjetivo;
    private Vector3 direccionDash,ultimaDireccion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        myState = Estados.Idle;
        saltosRestantes = saltosDisponibles; 

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
                //Debug.Log("Walking");
            }
        }
        if ((onPlataform == true||onGround == true) && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            //Debug.Log("Jump");
        }
        
    }

    void Walking()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rotacionObjetivo = 0;
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.forward;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotacionObjetivo = 90;
            transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.right;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotacionObjetivo = 180;
            transform.Translate(Vector3.back * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.back;
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotacionObjetivo = 270;
            transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);
            ultimaDireccion = Vector3.left;
            enMovimiento = true;
        }
        if (enMovimiento && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            myState = Estados.Sprint;
            //Debug.Log("Sprint");
        }
        if (!enMovimiento)
        {
            myState = Estados.Idle;
            //Debug.Log("Idle");
        }
        if (saltosRestantes > 0 && (onPlataform == true || onGround == true) && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            //Debug.Log("Jump");
        }
        FuerzaCaida();


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
            //Debug.Log("Walk");
        }
        if (!enMovimiento)
        {
            myState = Estados.Idle;
            //Debug.Log("Idle");
        }
        if (saltosRestantes > 0 && (onPlataform == true || onGround == true) && Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            //Debug.Log("Jump");
        }
        FuerzaCaida();



    }
    void Jumping()
    {
        if (rb.linearVelocity.y<0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); 
        }
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);

        saltosRestantes--;
   
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
    
    /*void setState(Estados newState)
    {
        myState=newState;
    }*/
    void FuerzaCaida()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (gravedadExtra - 1) * Time.deltaTime;
        }else if(rb.linearVelocity.y>0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (gravedadBajaSalto - 1) * Time.deltaTime;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag ("suelo"))
        {
            //Debug.Log("Tocando suelo");
            onGround = true;
            saltosRestantes = saltosDisponibles;

        }
        if (collision.gameObject.CompareTag ("plataforma"))
        {
            //Debug.Log("Tocando plataforma");
            onPlataform = true;
            saltosRestantes = saltosDisponibles;

            transform.SetParent (collision.transform);
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("plataforma"))
        { 
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //si toca la zona de muerte
        if (other.CompareTag("Muerte"))
        { 
            SCR_CheckPointControlador.Instancia.RespawnearJugador(gameObject); 
        }

        //si toca el checkpoint
        if (other.CompareTag("CheckPoint")) 
        {
            Transform checkpointPadre = other.transform.parent; // El "Checkpoint" padre
            Transform puntoRespawn = checkpointPadre.Find("PuntoRespawn");

            if (puntoRespawn != null)
            {
                SCR_CheckPointControlador.Instancia.ActualizarCheckPoint(puntoRespawn.position);
            }
        }
    }

}
