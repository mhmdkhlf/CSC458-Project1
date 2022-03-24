using UnityEngine;

public class ShotBehavior : MonoBehaviour
{
    private Vector3 m_target;
    [SerializeField]
    private GameObject collisionExplosion;
    private float speed = 1000f;
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (m_target != null)
        {
            if (transform.position == m_target)
            {
                Destroy(this.gameObject);
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }
    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
    }

    void explode()
    {
        if (collisionExplosion != null) {
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}