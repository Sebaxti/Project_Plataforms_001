using UnityEngine;

public class SCR_Player : MonoBehaviour
{
    /*----public----*/
    public enum Estados {Walk,Attack,Idle,Jump }
    public float velocidad, fuerzaSalto;
    public Estados myState;
    public GameObject visor;
    public bool onGround;

    /*----private----*/
    private Animator animador;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animador = GetComponent<Animator>();
        myState = Estados.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(myState)
        {
            case Estados.Idle:
                Idleing();
                break;
            case Estados.Walk:
                Walking();
                break;
            case Estados.Jump:
                Jumping();
                break;
            default:
                print("bye");
                break;

        }

        Debug.DrawRay(visor.transform.position, transform.forward);
    }

    void Idleing()
    {
        //animador.Play("Anim_Idle_P1");
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            myState = Estados.Walk;
            Debug.Log("Walking");
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
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(Vector3.back * velocidad * Time.deltaTime, Space.World);
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);
            enMovimiento = true;
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
