//NOTE: The Obstacle Avoidance radius of the Nav Mesh Agent was originally set to 0.2
//It can be changed to ~3 so the agents would be considered to be placed on the navmesh

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public GameObject damageAlert;
    public GameObject cloneContainer;
    public GameObject enemy;
    public GameObject originalEnemy;

    public string tagName;
    public string originalName;
    public string enemyName;

    public int maxEnemy;
    private int currentEnemy = 0;

    public int spawnRange = 20;
    public int spawnX = 0;
    public int spawnY = 0;
    public int spawnZ = 300;

    public float lookRadius = 10f;
    public float stoppingDistance = 1f;
    public float maxHealth = 100f;
    public float attackDamage = 5f;
    public float attackCooldown = 0.5f;
    public float attackRadius = 1f;
    public float deathValue = 10f;

    public string spawnLocation;

    public HealthBar healthBar;
    private float currentHealth;

    public LayerMask acceptedLayers;
    private Transform target;
    private NavMeshAgent agent;

    private RaycastHit hit;
    private Ray landingRay;
    private float raycastLength;
    private float timer;

    void Start() {
    
    raycastLength = 1000f;
    landingRay = new Ray(transform.position, Vector3.down);

    if(Physics.Raycast(landingRay, out hit, raycastLength, acceptedLayers)) {
            transform.position = hit.point;
    }

        originalEnemy = GameObject.FindGameObjectWithTag(tagName);

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
        public void Create() {
                if(Player.playerLocation.Equals(spawnLocation)) {
            //New name cannot be the same as the original name
            //Set the name of the tag to the original name
            //Each original name must be different from other original names
        if(currentEnemy < maxEnemy && gameObject.name.Equals(originalName)) {
            //enemy.SetActive(true);
            GameObject Clone = Instantiate(enemy, new Vector3(Random.Range(spawnX-spawnRange, spawnX+spawnRange), spawnY, Random.Range(spawnZ-spawnRange, spawnZ+spawnRange)), enemy.transform.rotation);
            Clone.transform.parent = cloneContainer.transform;
            Clone.name = enemyName;
            originalEnemy.GetComponent<Enemy>().currentEnemy++;
            //enemy.SetActive(true);
        }
                } else {
                    if(gameObject.name.Equals(originalName) == false) {
                        Destroy(gameObject);
                    }
                    originalEnemy.GetComponent<Enemy>().currentEnemy = 0;
                }
    }

    public void TakeDamage(float amount) {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0f) {
            damageAlert.SetActive(false);
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
        originalEnemy.GetComponent<Enemy>().currentEnemy--;
        Player.credits += deathValue;
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //What happens when attacking
    void Attack() {
        FaceTarget();
        Player.playerHealth -= attackDamage;
        return;
    }

    IEnumerator AlertAttack() {
        damageAlert.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damageAlert.SetActive(false);
        yield return null;
    }

    void Update() {

        Create();
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius && gameObject.name.Equals(originalName) == false && agent.isOnNavMesh) {
                agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance + attackRadius) {
                //Attack
                timer += Time.deltaTime;
                if (timer > attackCooldown) {
                    StartCoroutine(AlertAttack());
                    Attack();
                    timer -= attackCooldown;
                }
                    //Enemy is stopping to attack
                    if(distance <= stoppingDistance + attackRadius) {
                        agent.isStopped = true;
                        agent.ResetPath();
                    }
            }
        }
    
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}