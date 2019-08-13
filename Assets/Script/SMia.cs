using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMia : MonoBehaviour
{
    public enum Estados
    {
        ESPERAR,
        PATRULHAR
    }

    public Estados estadoAtual;

    private Transform alvo;

    private NavMeshAgent navMeshAgent;

    [Header("Esperar")]
    public float tempoEsperar = 2f;

    public float tempoEsperando = 0f;

    [Header("Patrulhar")]
    public Transform waypint1;

    public Transform waypint2;
    private Transform waypintAtual;
    public float distanciaMinimaWaypoint = 1f;
    private float distanciaWaypointAtual;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        waypintAtual = waypint1;

        Esperar();
    }

    private void Update()
    {
        ChecarEstados();
    }

    private void ChecarEstados()
    {
        switch (estadoAtual)
        {
            case Estados.ESPERAR:

                if (EsperouTemposuficinete())
                {
                    Patrulhar();
                }
                else
                {
                    alvo = transform;
                }
                break;

            case Estados.PATRULHAR:
                if (PertoWypointAtual())
                {
                    Esperar();
                    AlternarWaypoint();
                }
                else
                {
                    alvo = waypintAtual;
                }

                break;
        }

        navMeshAgent.destination = alvo.position;
    }

    #region ESPERAR

    private void Esperar()
    {
        estadoAtual = Estados.ESPERAR;

        tempoEsperando = Time.time;
    }

    private bool EsperouTemposuficinete()
    {
        return tempoEsperando + tempoEsperar <= Time.time;
    }

    #endregion ESPERAR

    #region PATRULHAR

    private void Patrulhar()
    {
        estadoAtual = Estados.PATRULHAR;
    }

    private bool PertoWypointAtual()
    {
        distanciaWaypointAtual = Vector3.Distance(transform.position, waypintAtual.position);
        return distanciaWaypointAtual <= distanciaMinimaWaypoint;
    }

    private void AlternarWaypoint()
    {
        waypintAtual = (waypintAtual == waypint1) ? waypint2 : waypint1;
    }

    #endregion PATRULHAR
}