using UnityEngine;
using System.Collections.Generic;

public class SimpleCharacterControlFree : MonoBehaviour
{
    public void Initialize(GameObject character)
    {
        m_animator = character.GetComponent<Animator>();
        m_rigidBody = character.GetComponent<Rigidbody>();
    }

    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    private float m_jumpForce = 4;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    [SerializeField] private Item object_in_hand;
    private Item item_to_pick_up;
    private Plot plot_to_interact;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 2.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    
    private List<Collider> m_collisions = new List<Collider>();

    void Awake()
    {
        if(!m_animator) { gameObject.GetComponent<Animator>(); }
        if(!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Nouveau item à mettre en main
            if (item_to_pick_up != null)
            {
                SetObjetInHand(item_to_pick_up);
            }
            // Intéraction avec un plot
            else if (plot_to_interact != null)
            {
                InteractWith(plot_to_interact);
            }
            InteractAnimation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider)) {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if(validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        } else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

	void FixedUpdate ()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch(m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
    }

    private void TankUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0) {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        } else if(walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if(direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }
    }

    private void InteractAnimation()
    {
        if(object_in_hand != null)
        {
            m_animator.SetTrigger("Pickup");
        }
    }

    public void SetObjetInHand(Item obj)
    {
        if (obj != null)
        {
            object_in_hand = obj;
            GameObject.Find("Objet en main").GetComponent<InfoObjetEnMain>().SetObjet(obj);
        }
        else
        {
            Debug.Log("Object to put in inventory doesn't exist !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Watering can":
                //TODO: Show tooltip
                item_to_pick_up = Resources.Load<Item>("Arrosoir");
                break;
            case "Plot":
                //TODO Show tooltip
                plot_to_interact = other.GetComponent<Plot>();
                break;
            default:
                Debug.Log("tag not handled : " + other.tag);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        item_to_pick_up = null;
    }

    private void InteractWith(Plot plot)
    {
        if(object_in_hand.GetType() == typeof(Plant))
        {
            Debug.Log("Plante");
        }
        else if (object_in_hand.GetType() == typeof(Pesticide))
        {
            Debug.Log("Pesticide");
        }
        else if (object_in_hand.GetType() == typeof(Engrais))
        {
            Debug.Log("Engrais");
        }
        else
        {
            plot.addToQEau(1);
        }
    }
}

//private void JumpingAndLanding()
//    {
//        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

//        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
//        {
//            m_jumpTimeStamp = Time.time;
//            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
//        }

//        if (!m_wasGrounded && m_isGrounded)
//        {
//            m_animator.SetTrigger("Land");
//        }

//        if (!m_isGrounded && m_wasGrounded)
//        {
//            m_animator.SetTrigger("Jump");
//        }
//    }
