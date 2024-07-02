using UnityEngine;
using System.Collections;

public class RoadMovement : MonoBehaviour
{
    public float speed = 5f;

    public float destroyTime = 20f;

    void Start()
    {
        StartCoroutine(DestroyAfterTime(destroyTime));
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);

        destroyTime += 10f;
    }
}
