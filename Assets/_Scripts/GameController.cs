using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Text;

public class GameController : MonoBehaviour
{
    public static int Gsize;
    public static int P1_simbol;
    public static int P2_simbol;
    public static bool recordGame;

    public MenuManager Mmanager;
    [TextArea(15,20)]
    public string movesText;

    [Header("3x3", order = 0)]
    [Header("animacion", order = 1)]
    public RectTransform x3_Bv1;
    public RectTransform x3_Bv2;
    public RectTransform x3_Bh1;
    public RectTransform x3_Bh2;

    [Header("Tabla")]
    public Transform x3_tabla;
    public Transform x3_tabla2;
    private Button[] x3_btns;
    private Image[] x3_Sbtns = new Image[9];
    //private Image[]

    [Header("4x4", order = 0)]
    [Header("animacion", order = 1)]
    public RectTransform x4_Bv1;
    public RectTransform x4_Bv2;
    public RectTransform x4_Bv3;
    public RectTransform x4_Bh1;
    public RectTransform x4_Bh2;
    public RectTransform x4_Bh3;

    [Header("Tabla")]
    public Transform x4_tabla;
    public Transform x4_tabla2;
    private Button[] x4_btns;
    private Image[] x4_Sbtns = new Image[16];

    [Header("Simbolos")]
    public Sprite Simb1;
    public Sprite Simb2;
    public Sprite Simb3;
    public Sprite Simb4;

    [Space]
    public TextMeshProUGUI T_j1;
    public TextMeshProUGUI T_j2;

    private int Turno;
    private bool Ganado;
    private bool[] P1_tablaX3 = new bool[9];
    private bool[] P2_tablaX3 = new bool[9];
    private bool[] P1_tablaX4 = new bool[16];
    private bool[] P2_tablaX4 = new bool[16];
    private int toques;

    private void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        movesText = "TicTacToe" + "\n \n" +
            "Grid size: " + Gsize + "x" + Gsize + "\n \n" +
            "Order:";


        Turno = 1;
        toques = 0;
        Ganado = false;
        T_j1.color = Color.white;
        T_j2.color = Color.grey;

        if (Gsize == 3)
        {
            x3_btns = x3_tabla.GetComponentsInChildren<Button>();
            for (int i = 0; i < x3_btns.Length; i++)
            {
                x3_Sbtns[i] = x3_btns[i].GetComponentsInChildren<Image>()[1];
                x3_Sbtns[i].enabled = false;
                x3_btns[i].interactable = true;
                P1_tablaX3[i] = false;
                P2_tablaX3[i] = false;
            }

            x3_tabla2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            x3_tabla.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            movesText = movesText + "\n" +
            "|1" + "|2" + "|3|" + "\n" +
            "|4" + "|5" + "|6|" + "\n" +
            "|7" + "|8" + "|9|" + "\n \n" +
            "Moves: " + "\n";
        }
        else if (Gsize == 4)
        {
            x4_btns = x4_tabla.GetComponentsInChildren<Button>();
            for (int i = 0; i < x4_btns.Length; i++)
            {
                x4_Sbtns[i] = x4_btns[i].GetComponentsInChildren<Image>()[1];
                x4_Sbtns[i].enabled = false;
                x4_btns[i].interactable = true;
                P1_tablaX4[i] = false;
                P2_tablaX4[i] = false;
            }

            x4_tabla2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            x4_tabla.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            movesText = movesText + "\n" +
            "| 1" + "| 2" + "| 3" + "| 4|" + "\n" +
            "| 5" + "| 6" + "| 7" + "| 8|" + "\n" +
            "| 9" + "|10" + "|11" + "|12|" + "\n" +
            "|13" + "|14" + "|15" + "|16|" + "\n \n" +
            "Moves: " + "\n";
        }
    }

    #region Acciones

    public void ComprobarAccion(int btnN)
    {
        toques = toques + 1;
        Image img;
        if (Gsize == 3)
        {
            x3_btns[btnN].interactable = false;
            img = x3_Sbtns[btnN];
        }
        else
        {
            x4_btns[btnN].interactable = false;
            img = x4_Sbtns[btnN];
        }

        if (Turno == 1)
        {
            switch (P1_simbol)
            {
                case 1:
                    img.sprite = Simb1;
                    img.enabled = true;
                    break;
                case 2:
                    img.sprite = Simb2;
                    img.enabled = true;
                    break;
                case 3:
                    img.sprite = Simb3;
                    img.enabled = true;
                    break;
                case 4:
                    img.sprite = Simb4;
                    img.enabled = true;
                    break;
            }

            if (Gsize == 3)
            {
                P1_tablaX3[btnN] = true;

                movesText = movesText +
                    "J1:" + "\t" + (btnN + 1) + "\n";

                if (P1_tablaX3[0] && P1_tablaX3[1] && P1_tablaX3[2])
                {
                    Gano(1);
                }
                else if (P1_tablaX3[3] && P1_tablaX3[4] && P1_tablaX3[5])
                {
                    Gano(1);
                }
                else if (P1_tablaX3[6] && P1_tablaX3[7] && P1_tablaX3[8])
                {
                    Gano(1);
                }
                else if (P1_tablaX3[0] && P1_tablaX3[3] && P1_tablaX3[6])
                {
                    Gano(1);
                }
                else if (P1_tablaX3[1] && P1_tablaX3[4] && P1_tablaX3[7])
                {
                    Gano(1);
                }
                else if (P1_tablaX3[2] && P1_tablaX3[5] && P1_tablaX3[8])
                {
                    Gano(1);
                }
                else if (P1_tablaX3[0] && P1_tablaX3[4] && P1_tablaX3[8])
                {
                    Gano(1);
                }
                else if (P1_tablaX3[2] && P1_tablaX3[4] && P1_tablaX3[6])
                {
                    Gano(1);
                }

                if (toques >= 9 && !Ganado)
                {
                    Empate();
                }
                else
                {
                    Turno = 2;
                    T_j1.color = Color.grey;
                    T_j2.color = Color.white;
                }
            }
            else
            {
                P1_tablaX4[btnN] = true;

                movesText = movesText +
                    "J1:" + "\t" + (btnN + 1) + "\n";

                if (P1_tablaX4[0] && P1_tablaX4[1] && P1_tablaX4[2] && P1_tablaX4[3])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[4] && P1_tablaX4[5] && P1_tablaX4[6] && P1_tablaX4[7])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[8] && P1_tablaX4[9] && P1_tablaX4[10] && P1_tablaX4[11])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[12] && P1_tablaX4[13] && P1_tablaX4[14] && P1_tablaX4[15])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[0] && P1_tablaX4[4] && P1_tablaX4[8] && P1_tablaX4[12])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[4] && P1_tablaX4[5] && P1_tablaX4[9] && P1_tablaX4[13])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[2] && P1_tablaX4[6] && P1_tablaX4[10] && P1_tablaX4[14])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[3] && P1_tablaX4[7] && P1_tablaX4[11] && P1_tablaX4[15])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[0] && P1_tablaX4[5] && P1_tablaX4[10] && P1_tablaX4[15])
                {
                    Gano(1);
                }
                else if (P1_tablaX4[3] && P1_tablaX4[6] && P1_tablaX4[9] && P1_tablaX4[12])
                {
                    Gano(1);
                }

                if (toques >= 16 && !Ganado)
                {
                    Empate();
                }
                else
                {
                    Turno = 2;
                    T_j1.color = Color.grey;
                    T_j2.color = Color.white;
                }
            }
        }
        else if (Turno == 2)
        {
            switch (P2_simbol)
            {
                case 1:
                    img.sprite = Simb1;
                    img.enabled = true;
                    break;
                case 2:
                    img.sprite = Simb2;
                    img.enabled = true;
                    break;
                case 3:
                    img.sprite = Simb3;
                    img.enabled = true;
                    break;
                case 4:
                    img.sprite = Simb4;
                    img.enabled = true;
                    break;
            }

            if (Gsize == 3)
            {
                P2_tablaX3[btnN] = true;

                movesText = movesText +
                    "J2:" + "\t" + (btnN + 1) + "\n";

                if (P2_tablaX3[0] && P2_tablaX3[1] && P2_tablaX3[2])
                {
                    Gano(2);
                }
                else if (P2_tablaX3[3] && P2_tablaX3[4] && P2_tablaX3[5])
                {
                    Gano(2);
                }
                else if (P2_tablaX3[6] && P2_tablaX3[7] && P2_tablaX3[8])
                {
                    Gano(2);
                }
                else if (P2_tablaX3[0] && P2_tablaX3[3] && P2_tablaX3[6])
                {
                    Gano(2);
                }
                else if (P2_tablaX3[1] && P2_tablaX3[4] && P2_tablaX3[7])
                {
                    Gano(2);
                }
                else if (P2_tablaX3[2] && P2_tablaX3[5] && P2_tablaX3[8])
                {
                    Gano(2);
                }
                else if (P2_tablaX3[0] && P2_tablaX3[4] && P2_tablaX3[8])
                {
                    Gano(2);
                }
                else if (P2_tablaX3[2] && P2_tablaX3[4] && P2_tablaX3[6])
                {
                    Gano(2);
                }

                if (toques >= 9 && !Ganado)
                {
                    Empate();
                }
                else
                {
                    Turno = 1;
                    T_j1.color = Color.white;
                    T_j2.color = Color.grey;
                }
            }
            else
            {
                P2_tablaX4[btnN] = true;

                movesText = movesText +
                    "J2:" + "\t" + (btnN + 1) + "\n";

                if (P2_tablaX4[0] && P2_tablaX4[1] && P2_tablaX4[2] && P2_tablaX4[3])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[4] && P2_tablaX4[5] && P2_tablaX4[6] && P2_tablaX4[7])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[8] && P2_tablaX4[9] && P2_tablaX4[10] && P2_tablaX4[11])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[12] && P2_tablaX4[13] && P2_tablaX4[14] && P2_tablaX4[15])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[0] && P2_tablaX4[4] && P2_tablaX4[8] && P2_tablaX4[12])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[1] && P2_tablaX4[5] && P2_tablaX4[9] && P2_tablaX4[13])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[2] && P2_tablaX4[6] && P2_tablaX4[10] && P2_tablaX4[14])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[3] && P2_tablaX4[7] && P2_tablaX4[11] && P2_tablaX4[15])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[0] && P2_tablaX4[5] && P2_tablaX4[10] && P2_tablaX4[15])
                {
                    Gano(2);
                }
                else if (P2_tablaX4[3] && P2_tablaX4[6] && P2_tablaX4[9] && P2_tablaX4[12])
                {
                    Gano(2);
                }

                if (toques >= 16 && !Ganado)
                {
                    Empate();
                }
                else
                {
                    Turno = 1;
                    T_j1.color = Color.white;
                    T_j2.color = Color.grey;
                }
            }
        }

    }

    #endregion

    private void Gano(int jug)
    {
        Ganado = true;

        Mmanager.Fin(jug);

        if (recordGame)
        {
            movesText = movesText + "\n" +
                "Player " + jug + " Win.";

            var time = System.DateTime.Now;
            string direccion = Application.dataPath;
            direccion = direccion.Replace("Assets", "");
            direccion = direccion + "Recorded plays";

            if (!Directory.Exists(direccion))
            {
                DirectoryInfo di = Directory.CreateDirectory(direccion);
            }

            direccion = direccion + "/Game_" +
                        time.Day + "-" + time.Month + "-" + time.Year + "_" + time.Hour + "-" + time.Minute + "-" + time.Second + ".txt";

            using (FileStream fs = File.Create(direccion))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(movesText);
                fs.Write(info, 0, info.Length);
            }
        }

        float move = Screen.width * 0.2475f;

        if (Gsize == 3)
        {
            for (int i = 0; i < x3_btns.Length; i++)
            {
                x3_btns[i].interactable = false;
            }

            x3_tabla2.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
            x3_tabla.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
        }
        else
        {
            for (int i = 0; i < x4_btns.Length; i++)
            {
                x4_btns[i].interactable = false;
            }

            x4_tabla2.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
            x4_tabla.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
        }
    }

    private void Empate()
    {
        Mmanager.Fin(3);

        movesText = movesText + "\n" +
            "It was a draw.";

        float move = Screen.width * 0.2475f;

        if (Gsize == 3)
        {
            for (int i = 0; i < x3_btns.Length; i++)
            {
                x3_btns[i].interactable = false;
            }

            x3_tabla2.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
            x3_tabla.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
        }
        else
        {

            for (int i = 0; i < x4_btns.Length; i++)
            {
                x4_btns[i].interactable = false;
            }

            x4_tabla2.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
            x4_tabla.GetComponent<RectTransform>().localPosition = new Vector3(move, 0, 0);
        }
    }

    public void Reiniciar()
    {
        StartGame();
        Mmanager.Reanudar();
    }
}