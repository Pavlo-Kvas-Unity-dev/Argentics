using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Argentics._2D
{
    public class PlatformerCharacter : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealthPoints = 3;                   // number of times character can be hurt until it dies
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField]
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        [SerializeField]
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody m_Rigidbody;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private int curHealthPoints;

        private void Awake()
        {
            m_Anim = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            curHealthPoints = maxHealthPoints;
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Grounded", m_Grounded);
        }

        [ContextMenu("Damage")]
        public void Damage()
        {
            Debug.Log("Player damaged");

            curHealthPoints--;
            if (curHealthPoints <= 0)
            {
                Die();
            }
        }

        [ContextMenu("Die")]
        private void Die()
        {
            StartCoroutine(DieAsync());
        }

        private IEnumerator DieAsync()
        {
            m_Anim.SetTrigger("Died");

            yield return new WaitForSeconds(2);//todo

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Move(float move, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Move the character
                m_Rigidbody.velocity = new Vector3(move*m_MaxSpeed, m_Rigidbody.velocity.y);
                m_Anim.SetFloat("MoveSpeed", Mathf.Abs(move));

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Grounded"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Grounded", false);
                m_Anim.SetTrigger("Jump");
                m_Rigidbody.AddForce(new Vector3(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            transform.rotation = Quaternion.Euler(0, 90 * (m_FacingRight ? 1 : -1), 0);
        }
    }
}
