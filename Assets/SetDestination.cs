using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestination : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agente;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);

                //agente.destination = target.transform.position;
                agente.destination = hit.point;

                target.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }
        }
    }
}