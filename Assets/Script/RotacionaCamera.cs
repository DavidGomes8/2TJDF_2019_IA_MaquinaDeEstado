using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionaCamera : MonoBehaviour
{
    public float v = 2f;

    private Quaternion rotacaoInicial;

    private Quaternion rotacaoDestino;

    public float velocidadeLerp = 3f;

    public float scroll;

    // Start is called before the first frame update
    private void Start()
    {
        rotacaoInicial = transform.localRotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            rotacaoDestino = Quaternion.Euler(-Input.mousePosition.y, Input.mousePosition.x, transform.localRotation.z);
        }
        else
        {
            rotacaoDestino = rotacaoInicial;
        }

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            rotacaoDestino,
            Time.deltaTime * velocidadeLerp
        );

        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.z, Camera.main.transform.position.y, transform.position.z + scroll);
        scroll = Input.mouseScrollDelta.y;
    }
}