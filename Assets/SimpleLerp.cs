using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLerp : MonoBehaviour
{
    [SerializeField] Vector3 posicionOriginal;
    [SerializeField] bool muevete;
    [SerializeField] Transform destino;
    [SerializeField] float velocidad;
    [SerializeField] float tiempoRecorrido;
    [SerializeField] float delta;

    float duracionTotal;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (muevete) {
            posicionOriginal = transform.position;
            muevete = false;
            tiempoRecorrido = 0;
            duracionTotal = Vector3.Distance(posicionOriginal,destino.position)/ velocidad;
        }

        if (tiempoRecorrido<duracionTotal)
        {
            
            tiempoRecorrido = Mathf.Clamp(tiempoRecorrido + Time.deltaTime, 0,duracionTotal);
            delta = tiempoRecorrido / duracionTotal;
            transform.position = Vector3.Lerp(posicionOriginal, destino.position, delta);
        }

    }
}
