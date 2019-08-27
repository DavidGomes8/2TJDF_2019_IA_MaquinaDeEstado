using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObstaculo : MonoBehaviour
{
    public Vector3 sentidoMov = Vector3.left;
    public float velocidade = 2f;
    public float delayInverterMovimento = 2f;
    private int direcao = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        InvokeRepeating(nameof(InverterDirecao), delayInverterMovimento, delayInverterMovimento);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(sentidoMov * direcao * velocidade * Time.deltaTime, Space.World);
    }

    private void InverterDirecao()
    {
        direcao *= -1;
    }
}