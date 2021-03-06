using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float fuerzaMinima=12, fuerzaMaxima=16, torqueValor=10, positionX=4, positionY=-5;
    [Range(-100,50)]
    public int pointValue;
    private GameManager gameManager;
    public ParticleSystem explosion;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(),ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);
        transform.position= RandomSpamPos();
        //dos formas de buscar el script de gameManager del GameObject GameManager
        //gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager=GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        //TODO HACE ESTO
    }

    /// <summary>
    /// Devuelve un vector3 random entre FuerzaMinima y FuerzaMaxima
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomForce()
    {
        return Vector3.up*Random.Range(fuerzaMinima,fuerzaMaxima);
    }

    /// <summary>
    /// Devuelve un float random entre -torqueValor y torqueValor
    /// </summary>
    /// <returns></returns>
    private float RandomTorque()
    {
        return Random.Range(-torqueValor,torqueValor);
    }

    /// <summary>
    /// Devuelve un vector aleatorio para posicionar el spam de objetos con z=0
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomSpamPos()
    {
        return new Vector3(Random.Range(-positionX,positionX),positionY,1);
    }

    private void OnMouseOver() {
        if (gameManager.gameState==GameManager.GameState.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position,explosion.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    /// <summary>
    /// Destruyo los gameobject que traspazan el killzone de debajo de escena
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            {
                //gameManager.UpdateScore(-10);
                gameManager.GameOver();
            }
            
        }
    }
}
