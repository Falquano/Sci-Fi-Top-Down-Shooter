using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveMesure : Mesure
{
    [SerializeField] private float Distance;
    [SerializeField] private float speed;
    [SerializeField] private float ValueMargin;

    private float Multiplicateur;

    private NavMeshAgent meshAgent;
    private Transform playerTransform;

    public float rotationSpeed;

    private Rigidbody RBennemy;

   

    private void Start()
    {
        //d�claration des variables utiles
        meshAgent = GetComponent<NavMeshAgent>();
        playerTransform = Player.GetComponent<Transform>();
    }

    private void Movement()
    {
        //calcul de la distance entre l'ennemi et le player en float
        float DistanceWithPlayer = Vector3.Distance(transform.position, playerTransform.position);

        //calcul de la direction et donc du vecteur dans la direction du player
        Vector3 VectorToPlayer = playerTransform.position - transform.position;
        //distance jusqu'au pts que l'on veux
        Multiplicateur = DistanceWithPlayer - Distance;
        
        //il prend le vector qui va de l'ennemi � son target et ajoute la position de l'ennemi afin d'avoir la position de la target dans l'espace
        meshAgent.destination = (VectorToPlayer.normalized * Multiplicateur) + transform.position;

    }
    private void OnEnable()
    {
        if(behavior == null)
        {
            behavior = GetComponent<EnnemyBehavior>();
        }
        if(RBennemy == null)
        {
            RBennemy = GetComponent<Rigidbody>();
        }
        if(Player == null)
        {
            Player = Player = FindObjectOfType<PlayerMove>().gameObject;
        }

        if (playerTransform == null)
            playerTransform = Player.transform;

        if (meshAgent == null)
            meshAgent = GetComponent<NavMeshAgent>();

        meshAgent.enabled = true;

        //appel de la fonction qui calcul le chemin de l'ennemi en premier lieu
        Movement();
    }

    private void OnDisable()
    {


        RBennemy.velocity = Vector3.zero;
        meshAgent.ResetPath();
        meshAgent.velocity = Vector3.zero;
        meshAgent.enabled = false;
    }

    private void Update()
    {
        if (Player == null || PlayerisDead == true)
            return;

        Vector3 direction = playerTransform.position - transform.position;
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }


}
