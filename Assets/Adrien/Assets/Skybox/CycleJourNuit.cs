using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CycleJourNuit : MonoBehaviour
{
    protected Light soleil;
    protected float vitesseAngulaire;
    public float dureeCycle;
    [Range(0, 30)]
    public float indiceSaison;

    // Start is called before the first frame update
    void Start()
    {
        soleil = GetComponent <Light> ();

    }

    // Update is called once per frame
    void Update()
    {
        float angleSolaire = ((Time.time / (dureeCycle * 60)) % 1) * 360;
        transform.eulerAngles = new Vector3(angleSolaire, indiceSaison, 0);
        if (angleSolaire < 180)
        {
            float eclaircissement = Mathf.Abs(angleSolaire - 90);
            if (eclaircissement < 80)
            {
                soleil.intensity = 1;
            }
            else
            {
                soleil.intensity = 1- ((eclaircissement - 80) / 10);
            }
        }
        else
        {
            soleil.intensity = 0;
        }

    }
}
