using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Partes")]
    public GameObject parte_grid;
    private int Gsize;
    public Toggle t_x3;
    public Toggle t_x4;
    public TextMeshProUGUI saveT;
    private bool saveMoves = true;
    [Space]
    public GameObject parte_p1;
    private int P1_simbol;
    public Toggle P1_t_s1;
    public Toggle P1_t_s2;
    public Toggle P1_t_s3;
    public Toggle P1_t_s4;
    [Space]
    public GameObject parte_p2;
    private int P2_simbol;
    public Toggle P2_t_s1;
    public Toggle P2_t_s2;
    public Toggle P2_t_s3;
    public Toggle P2_t_s4;
    [Space]
    public GameObject MPausa;

    [Header("Fin partida")]
    public GameObject FinPartida;
    public TextMeshProUGUI ganador;

    [Header("Game")]
    public GameObject gameObj;
    public GameObject x3;
    public GameObject x4;

    [Header("In game")]
    public GameObject inGame;

    private void Awake()
    {
        StartMenu();
    }

    public void StartMenu()
    {
        parte_grid.SetActive(true);

        switch (P1_simbol)
        {
            case 3:
                t_x3.Select();
                break;
            case 4:
                t_x4.Select();
                break;
        }

        parte_p1.SetActive(false);
        parte_p2.SetActive(false);
        inGame.SetActive(false);
        gameObj.SetActive(false);
        MPausa.SetActive(false);
        FinPartida.SetActive(false);
        x3.SetActive(false);
        x4.SetActive(false);
    }

    #region UI

    //Parte grid size
    public void SelectGsize(int valor)
    {
        Gsize = valor;
    }

    public void CheckGsize()
    {
        //fade
        parte_grid.SetActive(false);
        parte_p1.SetActive(true);

        switch (P1_simbol)
        {
            case 1:
                P1_t_s1.Select();
                break;
            case 2:
                P1_t_s2.Select();
                break;
            case 3:
                P1_t_s3.Select();
                break;
            case 4:
                P1_t_s4.Select();
                break;
        }
    }

    //Parte jugador 1
    public void VolverGsize()
    {
        //fade
        parte_grid.SetActive(true);
        parte_p1.SetActive(false);
    }

    public void P1_simbolo(int simbolo)
    {
        P1_simbol = simbolo;
    }

    public void CheckP1()
    {
        //fade
        parte_p1.SetActive(false);
        parte_p2.SetActive(true);

        switch (P1_simbol)

        {
            case 1:
                P2_t_s1.interactable = false;
                break;
            case 2:
                P2_t_s2.interactable = false;
                break;
            case 3:
                P2_t_s3.interactable = false;
                break;
            case 4:
                P2_t_s4.interactable = false;
                break;
        }

        if (P2_simbol == 1 && P1_simbol != 1)
        {
            P2_t_s1.Select();
        }
        else if (P2_simbol == 2 && P1_simbol != 2)
        {
            P2_t_s2.Select();
        }
        else if (P2_simbol == 3 && P1_simbol != 3)
        {
            P2_t_s3.Select();
        }
        else if (P2_simbol == 4 && P1_simbol != 4)
        {
            P2_t_s4.Select();
        }
        else
        {
            P2_t_s1.isOn = false;
            P2_t_s2.isOn = false;
            P2_t_s3.isOn = false;
            P2_t_s4.isOn = false;
        }
    }

    //Parte jugador 2
    public void VolverP1simbol()
    {
        //fade
        parte_p1.SetActive(true);
        parte_p2.SetActive(false);
        
        P2_t_s1.interactable = true;
        P2_t_s2.interactable = true;
        P2_t_s3.interactable = true;
        P2_t_s4.interactable = true;
    }

    public void P2_simbolo(int simbolo)
    {
        P2_simbol = simbolo;
    }

    public void CheckP2()
    {
        //fade
        parte_p2.SetActive(false);
        GameController.Gsize = Gsize;
        GameController.P1_simbol = P1_simbol;
        GameController.P2_simbol = P2_simbol;
        GameController.recordGame = saveMoves;
        gameObj.SetActive(true);
        inGame.SetActive(true);

        switch (Gsize)
        {
            case 3:
                x3.SetActive(true);
                break;
            case 4:

                x4.SetActive(true);
                break;
        }
    }

    #endregion

    public void SaveMoves(bool on)
    {
        saveMoves = on;
        if (on)
        {
            saveT.text = "Save moves On";
        }
        else
        {
            saveT.text = "Save moves Off";
        }
    }

    public void ponerPausa()
    {
        MPausa.SetActive(true);
        inGame.SetActive(false);
    }

    public void Reanudar()
    {
        MPausa.SetActive(false);
        inGame.SetActive(true);
    }

    public void Fin(int valor)
    {
        FinPartida.SetActive(true);
        inGame.SetActive(false);
        switch (valor)
        {
            case 1:
                ganador.text = "The player #1 wins";
                break;
            case 2:
                ganador.text = "The player #2 wins";
                break;
            case 3:
                ganador.text = "It was a draw";
                break;
        }
    }
}