/*
    Script to define the behavior for the arrow that indicated the player where the level end is
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorArrowBehavior : MonoBehaviour
{
    [SerializeField] private GameObject levelEnd;
    [SerializeField] private GameObject player;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelEnd == null)
        {
            // if the level end wasn't assigned, then we don't show the arrow as that might be confusing
            DestroyImmediate(gameObject);
        }

        // point the arrow towards the level end
        Vector3 direction = levelEnd.transform.position - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }
}
