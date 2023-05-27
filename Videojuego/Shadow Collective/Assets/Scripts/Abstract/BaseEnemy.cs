/*
    Script to define an abstract class that will be used by enemy classes

    Using abstract classes normalizes behavior so that all enemies have certain attributes or methods
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemy : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject player; // set by enemy handler
    
    [System.NonSerialized]
    public PlayerController playerController; // set by enemy handler
    
    // radius that defines the max distance to alert another enemy when the player is seen
    [SerializeField] float alertingRadius = 10f; 

    [SerializeField] float alertedTimeLimit = 10f; 
    
    [SerializeField] protected LayerMask playerLayer;

    [SerializeField] Vector3 startingVisionConeDirection = Vector3.down;
    
    public float sightDistance;
    protected float fov;
    protected PolygonCollider2D visionConeCollider;
    protected GameObject visionConeVisual;


    protected SpriteRenderer spriteRenderer;
    // attributes related to the state of the enemy being alerted of the enemies position
    protected bool isAlerted = false;
    protected float alertedTime = 0f;

    // when alerted, the guard will try to move to the player's position
    protected Vector3 playerLastPos;

    // attributes related to the state of the enemy being hacked 
    protected bool isHacked = false;
    protected float hackDuration;
    protected float hackTimer;


    virtual protected void Start() 
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        visionConeVisual = gameObject.transform.GetChild(1).gameObject;
        visionConeVisual.SetActive(false);

        visionConeCollider = gameObject.GetComponent<PolygonCollider2D>();

        // set sight distance to the vision cone's distance
        sightDistance = Mathf.Sqrt(Mathf.Pow(visionConeCollider.points[1].y,2) + Mathf.Pow(visionConeCollider.points[1].x,2));
        fov = Mathf.Atan(Mathf.Abs(visionConeCollider.points[1].x) / visionConeCollider.points[1].y);

        UpdateVisionCone(startingVisionConeDirection);
    }

    virtual protected void Update() 
    {
        if (isHacked) 
        {
            hackTimer += Time.deltaTime;

            if (hackTimer > hackDuration) UnHack();
        }
        else if (isAlerted) 
        {
            alertedTime += Time.deltaTime;

            if (alertedTime > alertedTimeLimit) UnAlert();            
        }
    }

    /* 
        function that checks if the gameobject that entered the vision cone is the player
        if it is, then it asks through the playercontroller if it can be seen 
        if it can be seen, we alert ourselves, aswell as other enemies within the radius
    */ 
    protected void OnTriggerEnter2D(Collider2D collision) 
    {
        if (isHacked) return;

        if (GameObject.ReferenceEquals(player, collision.gameObject)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, sightDistance, playerLayer);
            
            if (hit.collider != null)
            {
                if (playerController.CheckVisibility(gameObject)) 
                {
                    AlertOthers(player.transform.position);
                }
            }
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (isHacked) return;

        if (GameObject.ReferenceEquals(player, collision.gameObject)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, sightDistance, playerLayer);
            
            if (hit.collider != null)
            {
                if (playerController.CheckVisibility(gameObject)) 
                {
                    AlertOthers(player.transform.position);
                }
            }
        }
    }

    virtual protected void UpdateVisionCone(Vector3 direction)
    {
        direction = new Vector2(direction.x, direction.y).normalized;
        float directionAngle = Mathf.Acos(Vector2.Dot(direction, Vector2.right));

        if (direction.y < 0) directionAngle = 2 * Mathf.PI - directionAngle;

        // + 90 because of its starting position
        visionConeVisual.transform.rotation = Quaternion.Euler(0f, 0f, directionAngle * Mathf.Rad2Deg + 90);

        PolygonCollider2D col = gameObject.GetComponent<PolygonCollider2D>();

        col.enabled = false;

        col.points = new[]
        {
            Vector2.zero,
            sightDistance * new Vector2(Mathf.Cos(directionAngle + fov), Mathf.Sin(directionAngle + fov)),
            sightDistance * new Vector2(Mathf.Cos(directionAngle - fov), Mathf.Sin(directionAngle - fov))
        };

        col.enabled = true;
    }

    // function to be alerted that the player has been seen
    virtual public void Alert(Vector3 playerPos) 
    {
        isAlerted = true;
        alertedTime = 0f;
        playerLastPos = playerPos;
    }

    virtual protected void UnAlert() 
    {
        isAlerted = false;
    }

    public void AlertOthers(Vector3 playerPos) 
    {
        // overlap circle returns a list of colliders within a radius
        Collider2D[] colsInRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, alertingRadius, LayerMask.GetMask("Enemy"));

        foreach (var col in colsInRadius)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<EnemyController>().enemyScript.Alert(playerPos);
            }
        }
    }

    // function to be hacked by the player or by a gadget
    virtual public void Hack(float hackDuration_) 
    {
        spriteRenderer.color = new Color(0.258544f,0.4632035f,0.6603774f,1);

        hackDuration = hackDuration_;
        hackTimer = 0f;
        isHacked = true;
    }

    virtual public void UnHack()
    {
        spriteRenderer.color = new Color(1,1,1,1);
        isHacked = false;
    }

    virtual public void Die()
    {
        Destroy(gameObject);
    }

    virtual public void ShowVisionCone()
    {
        visionConeVisual.SetActive(true);
    }
}
