
using ActionGame.Env.Building;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIdentifier : MonoBehaviour
{
    [SerializeField] BuildingTypSO buildingTyp;
    Rigidbody2D buildingRb;

    private void Awake()
    {
        buildingRb = gameObject.GetComponent<Rigidbody2D>();

        if(buildingTyp == null)
        {
            buildingTyp = Resources.Load<BuildingTypSO>(this.gameObject.name.ToString());
        }
    }

    public void Onhit()
    {
        StartCoroutine(BuildingHit());
    }

    IEnumerator BuildingHit()
    {
        float buldingAttackSPeed = 8f;
        Vector2 orginalPosition = transform.localPosition;
        Vector2 targetPosition = transform.localPosition + new Vector3(0, 0.2f);

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * buldingAttackSPeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.localPosition = Vector2.Lerp(orginalPosition, targetPosition, formula);

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == InGameTags.Projectile.ToString())
        {
            Debug.Log("Proj");
        }
    }


    public GameObject GetBuildPrefab() => buildingTyp.GetPrefab();
    public int GetBuildHealth() => buildingTyp.GetHealth();

}
