using UnityEngine;

public class SCR_Player : MonoBehaviour
{
    public enum Estados {Walk,Attack,Idle,Jump }
    public float velocidad, fuerzaSalto;
    private Animator animador;
    public Estados myState;
    public GameObject visor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            myState = Estados.Walk;
            Debug.Log("Walking");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Estado: Jumping");
        }
    }

    void Walking(){
        
        bool enMovimiento=false;

        if (Input.GetKey(KeyCode.W)){
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(transform.forward*velocidad*Time.deltaTime);
            enMovimiento = true;
            
        }
        if (Input.GetKey(KeyCode.D)){
            transform.eulerAngles=new Vector3(0,270,0);
            transform.Translate(transform.right*velocidad*Time.deltaTime);
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.S)){
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(transform.forward*-1*velocidad*Time.deltaTime);
            enMovimiento = true;
        }
        if (Input.GetKey(KeyCode.A)){
            transform.eulerAngles=new Vector3(0,90,0);
            transform.Translate(transform.right*-1*velocidad*Time.deltaTime);
            enMovimiento = true;
        }

        if (!enMovimiento)
        {
            myState= Estados.Idle;
            Debug.Log("Idle");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myState = Estados.Jump;
            Debug.Log("Jump");
        }
    }

    void Jumping()
    {
        transform.Translate(Vector3.up*fuerzaSalto*Time.deltaTime);
    }
    
    void setState(Estados newState)
    {
        myState=newState;
    }
}
