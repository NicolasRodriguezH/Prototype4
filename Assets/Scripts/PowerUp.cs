using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creo un tipo PowerUpType para usarlo dentro de la clase PowerUp - esto seria una interfaz
public enum PowerUpType { None, Pushback, Rockets, Smash }

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
