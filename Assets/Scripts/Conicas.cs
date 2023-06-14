using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conicas : MonoBehaviour
{

    public Text txtConicas, lbl_A, lbl_B, lbl_H, lbl_K, lbl_Teta;
    public Material matLinea, matCircunferencia, matElipse, matParabola, matHiperbola;

    public Slider sl_a;
    private float a = 1;
    public Slider sl_b;
    private float b = 3;
    public Slider sl_h;
    private float h = 1;
    public Slider sl_k;
    private float k = 1;
    public Slider sl_teta;
    private float teta = 45;

    private int conicaSeleccionada = 0; //0-Sin seleccionar, 1- Recta, 2- Circunferencia, 3- Elipse, 4- Parábola, 5-Hipérbola
    private int resolucion = 1000;
    private Vector3[] posPuntos;

    private void Start()
    {
        sl_a.enabled = false;
        sl_b.enabled = false;
        sl_h.enabled = false;
        sl_k.enabled = false;
        sl_teta.enabled = false;

    }

    public void DibujaConica()
    {

        if (conicaSeleccionada != 0){

            //LINE RENDERER
            LineRenderer lr = GetComponent<LineRenderer>();
            lr.SetVertexCount(resolucion + 1);

            a = sl_a.value;
            b = sl_b.value;
            h = sl_h.value;
            k = sl_k.value;
            teta = sl_teta.value;

            switch (conicaSeleccionada)
            {

                case 1: //Recta
                    txtConicas.text = "Recta";
                    //MAT LR
                    lr.material = matLinea;
                    MuestraSlidersyEtiquetas();
                    lbl_A.text = "x0";
                    lbl_B.text = "y0";
                    lbl_H.text = "y1";
                    lbl_K.text = "y1";
                    lbl_Teta.gameObject.SetActive(false);
                    sl_teta.gameObject.SetActive(false);
                    posPuntos = CreaRecta(a, b, h, k, resolucion);
                    break;
                case 2: //Circunferencia
                    txtConicas.text = "Circunferencia";
                    //MAT LR
                    lr.material = matCircunferencia;
                    MuestraSlidersyEtiquetas();
                    lbl_B.text = "r";
                    lbl_A.gameObject.SetActive(false);
                    sl_a.gameObject.SetActive(false);
                    lbl_Teta.gameObject.SetActive(false);
                    sl_teta.gameObject.SetActive(false);
                    posPuntos = CreaCircunferencia(b, h, k, resolucion);
                    break;
                case 3: //Elipse
                    txtConicas.text = "Elipse";
                    lr.material = matElipse;
                    MuestraSlidersyEtiquetas();
                    posPuntos = CreaElipse(a, b, h, k, teta, resolucion);
                    break;
                case 4: //Parábola
                    txtConicas.text = "Parábola";
                    lr.material = matParabola;
                    MuestraSlidersyEtiquetas();
                    lbl_B.text = "p";
                    lbl_A.gameObject.SetActive(false);
                    sl_a.gameObject.SetActive(false);
                    posPuntos = CreaParabola(b, h, k, teta, resolucion);
                    break;
                case 5: //Hipérbola
                    txtConicas.text = "Hiperbola";
                    lr.material = matHiperbola;
                    MuestraSlidersyEtiquetas();
                    posPuntos = CreaHiperbola(a, b, h, k, teta, resolucion);
                    break;
            }

            //Ciclo para dibujar los elementos calculados previamente en posPuntos
            for (int i = 0; i <= resolucion; i++)
            {
                lr.SetPosition(i, posPuntos[i]);
            }
        }
        else{  // Si no se ha seleccionado ninguna cónica
        }
    }
    //*************************************** RECTA ***************************
    public void BtnRecta()
    {

        conicaSeleccionada = 1;
        DibujaConica();
    }

    private Vector3[] CreaRecta(float x0, float y0, float x1, float y1, int resolucion)
    {
        posPuntos = new Vector3[resolucion + 1]; //inicia el arreglo de vectores3 que almacenará los puntos del dibujo
        float dx = x1 - x0;
        float dy = y1 - y0;
        for (int i = 0; i <= resolucion; i++)
        {
            posPuntos[i] = new Vector3(x0 + dx * i / resolucion, y0 + dy * i / resolucion, 0);
        }
        return posPuntos;
    }
    //************************************** CIRCUNFERENCIA *********************
    public void BtnCircunferencia()
    {
        conicaSeleccionada = 2;
        DibujaConica();
    }
    private Vector3[] CreaCircunferencia(float r, float h, float k, int resolucion)
    {
        posPuntos = new Vector3[resolucion + 1]; //inicia el arreglo de vectores3 que almacenará los puntos del dibujo
        Vector3 centro = new Vector3(h, k, 0);
        for (int i = 0; i <= resolucion; i++)
        {
            float angulo = (float)i / (float)resolucion * 2.0f * Mathf.PI;
            posPuntos[i] = new Vector3(r * Mathf.Cos(angulo), r * Mathf.Sin(angulo), 0);
            posPuntos[i] = posPuntos[i] + centro;
        }
        return posPuntos;
    }
    //*************************************ELIPSE*****************************
    public void BtnElipse()
    {

        conicaSeleccionada = 3;
        DibujaConica();
    }

    private Vector3[] CreaElipse(float a, float b, float h, float k, float teta, int resolucion)
    {
        posPuntos = new Vector3[resolucion + 1];
        Quaternion q = Quaternion.AngleAxis(teta, Vector3.forward);
        Vector3 centro = new Vector3(h, k, 0);

        for (int i = 0; i <= resolucion; i++)
        {
            float angulo = (float)i / (float)resolucion * 2.0f * Mathf.PI;
            posPuntos[i] = new Vector3(a * Mathf.Cos(angulo), b * Mathf.Sin(angulo), 0);
            posPuntos[i] = q * posPuntos[i] + centro;

        }

        return posPuntos;
    }
    //********************************PARABOLA***********************
    public void BtnParabola()
    {
        conicaSeleccionada = 4;
        DibujaConica();
    }
    private Vector3[] CreaParabola(float p, float h, float k, float teta, int resolucion)
    {
        posPuntos = new Vector3[resolucion + 1]; //Inicia el arreglo  de vectores3 que almcenará ls puntos del dibujo
        Quaternion q = Quaternion.AngleAxis(teta, Vector3.forward);
        Vector3 vertice = new Vector3(h, k, 0);
        

        for (int i = 0; i <= resolucion; i++)
        {
            float parametro = i - (resolucion / 2);
            float angulo = (float)i / (float)resolucion * 2.0f * Mathf.PI;
            posPuntos[i] = new Vector3(parametro, (1 / (4 * p)) * Mathf.Pow(parametro, 2), 0);
            posPuntos[i] = q * posPuntos[i] + vertice;

        }

        return posPuntos;
    }
    //******************************HIPERBOLA**************************************
    public void BtnHiperbola()
    {
        conicaSeleccionada = 5;
        DibujaConica();
    }
    private Vector3[] CreaHiperbola(float a, float b, float h, float k, float teta, int resolucion)
    {
        posPuntos = new Vector3[resolucion + 1]; //inicia el arreglo de vectores3 que almacenará los puntos del dibujo
        Quaternion q = Quaternion.AngleAxis(teta, Vector3.forward);//rotar parábola en el eje z
        Vector3 centro = new Vector3(h, k, 0);

        for (int i = 0; i <= resolucion; i++){
            float angulo = (float)i / (float)resolucion * 2.0f * Mathf.PI;
            //Sec^2 t -tan^2 t= 1; pero sec t = 1/ cos t
            posPuntos[i] = new Vector3(h + a * (1 / (Mathf.Cos(angulo))), k + b * Mathf.Tan(angulo), 0);
            posPuntos[i] = q * posPuntos[i] + centro;
        }
        return posPuntos;
    }

    public void MuestraSlidersyEtiquetas()
    {

        sl_a.gameObject.SetActive(true);
        sl_b.gameObject.SetActive(true);
        sl_h.gameObject.SetActive(true);
        sl_k.gameObject.SetActive(true);
        sl_teta.gameObject.SetActive(true);

        lbl_A.gameObject.SetActive(true);
        lbl_B.gameObject.SetActive(true);
        lbl_H.gameObject.SetActive(true);
        lbl_K.gameObject.SetActive(true);
        lbl_Teta.gameObject.SetActive(true);

        lbl_A.text = "a";
        lbl_B.text = "b";
        lbl_H.text = "h";
        lbl_K.text = "k";
        lbl_Teta.text = "t";

        sl_a.enabled = true;
        sl_b.enabled = true;
        sl_h.enabled = true;
        sl_k.enabled = true;
        sl_teta.enabled = true;
    }
}
