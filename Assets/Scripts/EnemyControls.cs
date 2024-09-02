using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    // Sets specific attributes for enemy behavior
    [SerializeField] private float enemySpeed;
    [SerializeField] private Rigidbody2D enemyRB2D;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // Have the enemy move at a fixed rate
        enemyRB2D.velocity = Vector2.left * enemySpeed;
    }
}
