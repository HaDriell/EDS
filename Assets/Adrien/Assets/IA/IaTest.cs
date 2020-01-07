using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class IaTest : MonoBehaviour
{
    public float angleDeVue, distanceDeVue;

    public float maxDistanceEloignement;
    public float distanceDeplacement;
    public float tempsEntreDeplacements;
    public float tempsPoursuite = 10;
    protected Vector3 positionDepart;

    protected GameObject cible;
    protected NavMeshAgent agent;
    protected Vector3 derniereDirectionConnue;
    protected bool isRandomPath = false;

    // Start is called before the first frame update.
    void Start()
    {
        //tip 1 : pour retrouver une variable : select + f12.
        cible = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        positionDepart = this.transform.position;

    }
    protected void deplacementAléatoire()
    {
        isRandomPath = true;
        float distanceEloignement = Vector3.Distance(positionDepart, this.transform.position);
        Invoke("deplacementAléatoire", tempsEntreDeplacements);
        if (distanceEloignement <= maxDistanceEloignement)
        {
            Vector3 destination = this.transform.position;
            //a = a +1 equivaut à a += 1.
            destination.x += Random.Range(-distanceDeplacement, distanceDeplacement);
            destination.z += Random.Range(-distanceDeplacement, distanceDeplacement);
            agent.SetDestination(destination);
        }
        else
        {
            agent.SetDestination(positionDepart);
        }

    }
    protected bool presenceJoueur()
    {
 
        bool resultat = false;
        //si "deplacementAléatoire" n'est pas invoqué alors...
        if (!IsInvoking("deplacementAléatoire"))
        {
            Invoke("deplacementAléatoire", tempsPoursuite);
        }
        if (!cible) return false;
        //"this" pas obligatoire car sert a faire réference à soi-même.
        //Les "." sont équivalents aux "/" en répertoire.
        float distanceJoueur = Vector3.Distance(cible.transform.position, this.transform.position);
        if (distanceJoueur <= distanceDeVue)
        {
            Vector3 directionJoueur = cible.transform.position - this.transform.position;
            //angle tjrs entre 0 et 180° -> max = direction opposée à l'angle prévu.
            //structurer code : ctrl +k, d.
            float angle = Vector3.Angle(directionJoueur, transform.forward);
            if (angle < angleDeVue / 2)
            {
                RaycastHit touche;
                //cette fonction renvoie vrai si elle a touché un collider.
                if (Physics.Raycast(transform.position, directionJoueur, out touche, distanceDeVue))
                {
                    if (touche.transform == cible.transform)
                    {
                        resultat = true;
                        derniereDirectionConnue = cible.transform.position - transform.position;
                        CancelInvoke("deplacementAléatoire");
                        isRandomPath = false;
                    }

                }


            }
        }
        return resultat;
    }

    // Update is called once per frame
    void Update()
    {
        if (presenceJoueur())
        {
            agent.SetDestination(cible.transform.position);
        }
        else if(!isRandomPath)
        {
            //se dirige vers la direction que tu regarde.
            transform.LookAt(transform.position + derniereDirectionConnue);            
            transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
        }
    }
}
