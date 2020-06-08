using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_FSM : MonoBehaviour {
    private CheckMyVision checkMyVision;
    private NavMeshAgent agent;
    private Transform playerTransform;

    private Transform patrolDestination;

    private Health playerHealth;

    public float maxDamage = 10f;
    
  
    
    // Enums to keep states
    public enum ENEMY_STATES { patrol, chase, attack }

    
    // We need a property to get the current state
    [SerializeField]
    private ENEMY_STATES currentState;
    public ENEMY_STATES CurrentState {
        get { return currentState; }
        set {
            currentState = value;
            StopAllCoroutines();
            switch (currentState) {
                case ENEMY_STATES.patrol:
                    StartCoroutine (EnemyPatrol ());
                    break;
                case ENEMY_STATES.chase:
                    StartCoroutine (EnemyChase ());
                    break;
                case ENEMY_STATES.attack:
                    StartCoroutine (EnemyAttack ());
                    break;
            }
        }
    }

    private void Awake () {
        checkMyVision = GetComponent<CheckMyVision> ();
        agent = GetComponent<NavMeshAgent> ();
        playerHealth = GameObject.Find ("Player").GetComponent<Health> ();
        playerTransform = playerHealth.GetComponent<Transform> ();
    }
    // Start is called before the first frame update
    void Start () {
       
        GameObject[] destinations = GameObject.FindGameObjectsWithTag ("Dest");
        int pathIndex = Random.Range(0, destinations.Length);
        patrolDestination = destinations[Random.Range(0,destinations.Length)].GetComponent<Transform>();
      //  print($"Path: {pathIndex}");
        CurrentState = ENEMY_STATES.patrol;
        
    }

    public IEnumerator EnemyPatrol () {
        print("Patroling"); 
        while (currentState == ENEMY_STATES.patrol) {

            checkMyVision.sensitivity = CheckMyVision.Sensitivity.HIGH;
            agent.isStopped = false;
            agent.SetDestination(patrolDestination.position);
            while (agent.pathPending ) {
              
                yield return null;
            }
            if (checkMyVision.targetInSight) {
                agent.isStopped = true;
                CurrentState = ENEMY_STATES.chase;
                yield break;
            }

            yield return null;
        }
        
    }
    public IEnumerator EnemyChase () {
        print("Chasing");
	
        while (currentState == ENEMY_STATES.chase)
        {
		
            checkMyVision.sensitivity = CheckMyVision.Sensitivity.LOW;
            // agent.acceleration = 600;

            agent.isStopped = false;
            bool destSet = agent.SetDestination(checkMyVision.lastKnownSighting);
            // print("Dest Set: " + destSet);
            while (agent.pathPending && agent.path.status == NavMeshPathStatus.PathInvalid){
                  print("Path Invalid: " + (agent.path.status == NavMeshPathStatus.PathInvalid));
                yield return null;
            }
           // print("Agent Remaining Distance: " + agent.remainingDistance);
          //  print("Agent Stopping Distance: " + agent.stoppingDistance);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                if (!checkMyVision.targetInSight)
                {
                    CurrentState = ENEMY_STATES.patrol;
                   
                }
                else
                {
                    print("Attacking");
                    CurrentState = ENEMY_STATES.attack;
                }
                yield break;
            }
            yield return null;
        }
        
    }
    public IEnumerator EnemyAttack () {

		 print("Attacking enemy");
	
        while (currentState == ENEMY_STATES.attack)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
            while (agent.pathPending)
            {
                yield return null;
            }
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                print("Distance increased");
                CurrentState = ENEMY_STATES.patrol;
                yield break;
            }
            else
            {
                // Do something
		
                playerHealth.HealthPoints -= maxDamage ;

				CurrentState = ENEMY_STATES.patrol;

                if(playerHealth.HealthPoints==0)
				{
					
					yield break;
				}
                print("loop ending");
            }
        }
        yield return null;
    }

    // Update is called once per frame
    void Update () {

    }
}