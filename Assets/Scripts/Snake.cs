using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _input;
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments;

    [SerializeField] private Transform _snakeSegment;

    private int _score;

    private float nextUpdate;
    private float _speed = 40f;
    private float _speedMultiplier = 0.1f;


    private void Start()
    {
        _segments = new List<Transform>();
        Reset();
    }
    

    private void Update() 
    {
        PlayerInput();

    }


    private void PlayerInput()
    {
        if (_direction.x != 0f) 
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _input = Vector2.up;

            } else if (Input.GetKeyDown(KeyCode.S) 
                    || Input.GetKeyDown(KeyCode.DownArrow))

            {
                _input = Vector2.down;

            }
        }

        if (_direction.y != 0f) 
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _input = Vector2.right;

            } else if (Input.GetKeyDown(KeyCode.A) 
                    || Input.GetKeyDown(KeyCode.LeftArrow))

            {
                _input = Vector2.left;

            }
        }
    }


    private void FixedUpdate()
    {

        if (Time.time < nextUpdate) {
            return;
        }

        if (_input != Vector2.zero)
        {
            _direction = _input;
        }

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0f);

        nextUpdate = Time.time + (1f / (_speed * _speedMultiplier));
        
    }

    private void Growth()
    {
        Transform segment = Instantiate(this._snakeSegment);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

        _speedMultiplier += 0.05f;

        _score++;
        Debug.Log($"Score: {_score}");
        
    }

    private void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.tag == "Food")
        {
            Growth();
        }
        
        else if (other.tag == "Obstacle")
        {
            Reset();
        }
    }

    private void Reset()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);
        _segments.Add(Instantiate(_snakeSegment));

        this.transform.position = Vector3.zero;

        _speedMultiplier = 0.1f;

        _score = 0;
    }
}
