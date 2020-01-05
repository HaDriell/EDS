using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPS_Controller : MonoBehaviour
{
    public float Speed = 5, JumpHeight = 2, Gravity = 10, CameraTurningSensibilty = 1;
    [Range(0,90)]
    public float LimitAngleCam = 90;

    public Vector3 direction { get; private set; }
    private Camera cam;
    private CharacterController controller;
    private float verticalSpeed = 0;
    private float jumpSpeed;
    private float camAngleUpDown;
    // Start is called before the first frame update
    void Start()
    {
        //Obtention de la référence du character controller
        controller = GetComponent<CharacterController>();


        #region v1
        /*//Création d'un objet pour la caméra
        GameObject camera = new GameObject("PlayerCamera");
        //Ajout de la caméra à l'objet
        cam = camera.AddComponent<Camera>();
        //Assignation de la position de la caméra
        camera.transform.position = transform.position + Vector3.up * (controller.height/2 -.1f);
        //Assignation de la caméra en tant qu'enfant de l'objet joueur
        camera.transform.parent = transform;*/
        #endregion

        #region v2
        cam = GetComponentInChildren<Camera>();
        #endregion

        //Calcul de la vitesse de saut nécessaire pour une hauteur donnée
        jumpSpeed = Mathf.Sqrt(2 * Gravity * JumpHeight);
        

    }

    // Update is called once per frame
    void Update()
    {
        //Obtention du mouvement par les contrôles
        Vector3 motion = (transform.forward * Input.GetAxisRaw("Vertical") +
                         transform.right * Input.GetAxisRaw("Horizontal"))
                         * Speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift)) motion *= 3;

        //Si on touche le sol...
        if (controller.isGrounded)
        {            
            if (Input.GetButton("Jump")) verticalSpeed = jumpSpeed;//...et on appuie sur sauter, on saute
            else verticalSpeed = 0;//Sinon on s'arrète de tomber
        }
        else//Sinon...
            verticalSpeed -= Gravity * Time.deltaTime;//On tombe

        //Application de la vitesse verticale au vecteur de mouvement
        motion.y = verticalSpeed * Time.deltaTime;

        //Tourner la caméra de gauche à droite
        transform.eulerAngles += Vector3.up * Input.GetAxis("Mouse X") * CameraTurningSensibilty;

        //Obtention du mouvement de la caméra de haut en bas
        camAngleUpDown += Input.GetAxis("Mouse Y") * CameraTurningSensibilty;
        //Limitation de l'angle de la caméra
        camAngleUpDown = Mathf.Clamp(camAngleUpDown, -LimitAngleCam, LimitAngleCam);
        //Application de l'angle X de la caméra une fois corrigé 
        cam.transform.localEulerAngles = Vector3.left * camAngleUpDown;


        //Déplace le joueur selon le vecteur de mouvement
        controller.Move(motion);
        direction = motion;

        
    }
}
