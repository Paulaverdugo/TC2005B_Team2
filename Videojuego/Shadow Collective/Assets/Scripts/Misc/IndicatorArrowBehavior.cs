/*
    Script to define the behavior for the arrow that indicated the player where the level end is
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorArrowBehavior : MonoBehaviour
{
    [SerializeField] private GameObject levelEnd;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // point the arrow towards the level end
        Vector3 direction = levelEnd.transform.position - gameObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }
}
