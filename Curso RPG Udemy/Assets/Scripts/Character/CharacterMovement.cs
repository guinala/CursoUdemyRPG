using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]private float velocity;

    public bool inMovement => _movement.magnitude > 0f;
    public Vector2 DireccionMovimiento => _movement;
    /** Forma antigua de hacerlo
    public Vector2 DireccionMovimiento
    {
        get { return _movement; }
    }
    */

    private Rigidbody2D _rigidbody2D;
    private Vector2 _input;
    private Vector2 _movement;



    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        //X
        if(_input.x > 0.1f)
        {
            _movement.x = 1f;
        }

        else if (_input .x < 0f)
        {
            _movement.x = -1f;
        }

        else
        {
            _movement.x = 0f;
        }

        //Y
        if (_input.y > 0.1f)
        {
            _movement.y = 1f;
            Debug.Log("Arriba");
        }

        else if (_input.y < 0f)
        {
            _movement.y = -1f;
        }

        else
        {
            _movement.y = 0f;
        }

        // Detectar si se ha pulsado alguna tecla
        if (Input.anyKeyDown)
        {
            // Obtener la tecla pulsada
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    // Mostrar la tecla pulsada en la consola
                    Debug.Log("Tecla pulsada: " + keyCode);
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movement * velocity * Time.fixedDeltaTime);
    }
    
}
