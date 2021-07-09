using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float distanceOfProjectile;

    public GameObject hitEffect = null;
    [SerializeField] private float projectileDamage = 0f;

    [SerializeField] Vector3 sourcePosition;
    [SerializeField] float rangeMax;

    [SerializeField] bool isPlayerComponent = false;
    [SerializeField] Vector3 target;
    [SerializeField] float m_Angle;


    private void OnEnable()
    {
        if (!isPlayerComponent)
        {
            target = GameObject.FindWithTag("Player").transform.position;
            m_Angle = Vector2.Angle(transform.localPosition, target);
        }
    }

    public void SetProjectilValues(float damage, float range, Transform sourcePosition, bool isPlayer)
    {
        SetFirstFrameRotation(sourcePosition);

        projectileDamage = damage;
        this.sourcePosition = sourcePosition.position;
        this.rangeMax = range;
        isPlayerComponent = isPlayer;
    }

    private void SetFirstFrameRotation(Transform sourcePosition)
    {
        if (isPlayerComponent)
        {
            Debug.Log("IsPlayer : " + isPlayerComponent);
            transform.position = sourcePosition.position;
            transform.rotation = sourcePosition.rotation;
        }
        else if(!isPlayerComponent)
        {
            Debug.Log("IsPlayer : " + isPlayerComponent);
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
            if (collision.tag == "Enemy" || collision.tag == "EnemyInnvocation" )
            {
                collision.GetComponent<Health>().TakeDamage(projectileDamage);
                DestroyProjectile();
            } 
        }
        else if (!isPlayerComponent)
        {
            if (collision.tag == "Player" || collision.tag == "NPC")
            {
                collision.GetComponent<Health>().TakeDamage(projectileDamage);
                DestroyProjectile();
            }
        }
    }
}
