using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private float distanceOfProjectile;

    public GameObject hitEffect = null;
    private float projectileDamage = 0f;

    Vector3 sourcePosition;
    float rangeMax;

    bool isPlayerComponent = false;
    Vector3 target;



    private void OnEnable()
    {
        if (!isPlayerComponent)
        {
            if(GameObject.FindWithTag(InGameTags.Player.ToString()) != null)
            {
                target = GameObject.FindWithTag(InGameTags.Player.ToString()).transform.position;
            }
            else
            {
                target = transform.position + new Vector3(0,0, -90); ;
            }
        }
    }

    public void SetIsPlayerComponent(bool trueFalse)
    {
        isPlayerComponent = trueFalse;
    }

    public bool IsAPlayerProjectile() => isPlayerComponent;

    public void SetProjectilValues(float damage, float range, Transform sourcePosition)
    {
        SetFirstFrameRotation(sourcePosition);

        projectileDamage = damage;
        this.sourcePosition = sourcePosition.position;
        this.rangeMax = range;
    }

    private void SetFirstFrameRotation(Transform sourcePosition)
    {
        if (isPlayerComponent)
        {
            transform.position = sourcePosition.position;
            transform.rotation = sourcePosition.rotation;
        }
        else if(!isPlayerComponent)
        {
               transform.position = sourcePosition.position;
            Vector3 moveDir = (target - transform.position).normalized;
            transform.localEulerAngles = new Vector3(0, 0, (GetAngleFromVector(moveDir)-90));
        }
    }

    private float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    private void FixedUpdate()
    {
        MovingBehavior();
    }

    private void MovingBehavior()
    {
        distanceOfProjectile = Vector2.Distance(sourcePosition, transform.position);

        if (distanceOfProjectile > rangeMax)
        {
            DestroyProjectile();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void DestroyProjectile()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerComponent) {
            if (collision.tag == InGameTags.Enemy.ToString() || collision.tag == InGameTags.EnemyInnvocation.ToString() || collision.tag ==  InGameTags.Destructible.ToString())
            {
                collision.GetComponent<Health>().TakeDamage(projectileDamage);
                DestroyProjectile();
            } 
        }
        else if (!isPlayerComponent)
        {
            if (collision.tag == InGameTags.Player.ToString() || collision.tag == InGameTags.NPC.ToString())
            {
                collision.GetComponent<Health>().TakeDamage(projectileDamage);
                DestroyProjectile();
            }
        }
    }
}
