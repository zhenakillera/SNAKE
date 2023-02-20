using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _gridCollider;


    private void Start()
    {
        RandomizedSpawn();
    }


    private void RandomizedSpawn()
    {
        Bounds gridBounds = this._gridCollider.bounds;

        float x = Random.Range(gridBounds.min.x, gridBounds.max.x);
        float y = Random.Range(gridBounds.min.y, gridBounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }


    private void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.tag == "Snake" || other.tag == "Obstacle")
        {
            RandomizedSpawn();
        }
    }
}
