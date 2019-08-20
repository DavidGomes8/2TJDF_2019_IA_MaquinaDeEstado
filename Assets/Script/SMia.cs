﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMia : MonoBehaviour
{
    public enum Estados
    {
        ESPERAR,
        PATRULHAR,
        PERSEGUIR
    }

    public Estados estadoAtual;

    private Transform alvo;

    private NavMeshAgent navMeshAgent;

    private Transform player;

    [Header("Esperar")]
    public float tempoEsperar = 2f;
    public float tempoEsperando = 0f;

    [Header("Patrulhar")]
    public Transform waypint1;
    public Transform waypint2;
    private Transform waypintAtual;
    public float distanciaMinimaWaypoint = 1f;
    private float distanciaWaypointAtual;

    [Header("Perseguir")]
    private float campoVisao = 5f;
    private float distanciaJogador;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        waypintAtual = waypint1;

        Esperar();
    }

    private void Update()
    {
        ChecarEstados();
    }

    private void ChecarEstados()
    {
        if (estadoAtual != Estados.PERSEGUIR && PossuiVisaoJogador())
        {
            Perseguir();
            return;
        }

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

            case Estados.PERSEGUIR:

                if (!PossuiVisaoJogador())
                {
                    Esperar();
                }
                else
                {
                    alvo = player;
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

    #region PERSEGUIR

    private void Perseguir()
    {
        estadoAtual = Estados.PERSEGUIR;
    }

    private bool PossuiVisaoJogador()
    {
        distanciaJogador = Vector3.Distance(transform.position, player.position);
        return distanciaJogador <= campoVisao;
    }

    #endregion PERSEGUIR
}