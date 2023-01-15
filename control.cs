using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    private bool isFacingRight = true;
    public float velocidad;
    private new Rigidbody2D  rigidbody;
    public float FuerzaSalto;



    [SerializeField] private float VelocidadDash;
    [SerializeField] private float TiempoDash;

    private float GravedadInicial;
    private bool puedeHacerDash=true;
    private bool SePuedeMover=true;

    private void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();

        GravedadInicial = rigidbody.gravityScale;

    }
    void Update()
    {
        if (SePuedeMover) { 
        ProcesarMovimiento();
        
        }
        ProcesarSalto();



        if(Input.GetKeyDown(KeyCode.Z) && puedeHacerDash)
        {
            

            StartCoroutine(Dash());
        }
    }

    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }
    void ProcesarSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector2.up*FuerzaSalto,ForceMode2D.Impulse);
        }
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if(isFacingRight && inputMovimiento < 0f || !isFacingRight && inputMovimiento > 0) {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        SePuedeMover = false;
        puedeHacerDash = false;
        rigidbody.gravityScale = 0;
        rigidbody.velocity = new Vector2(VelocidadDash*transform.localScale.x,0);

        yield return new WaitForSeconds(TiempoDash);
        SePuedeMover = true;
        puedeHacerDash=true;
        rigidbody.gravityScale = GravedadInicial;

    }
    

}
