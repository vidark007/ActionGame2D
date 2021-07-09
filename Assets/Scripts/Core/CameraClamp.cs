using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    
    [SerializeField] float camMoveSpeed = 5f;

    Transform playerPos;

    private void Awake()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float clampX = Mathf.Clamp(playerPos.position.x, minX, maxX); 
        float clampY = Mathf.Clamp(playerPos.position.y, minY, maxY);

        transform.position = Vector2.Lerp(transform.position, new Vector2(clampX, clampY), camMoveSpeed * Time.deltaTime);
    }
}
