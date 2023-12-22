using UnityEngine.SceneManagement;
using UnityEngine;

namespace Player
{
    public class PlayerMono : Damagable
    {
        private static float Speed = 0.005f;
        private static  float RotationSpeed = 0.05f;
        [SerializeField] private  MeteorSpawnerMono meteorSpawnerMono;
        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                Rotate(-RotationSpeed);
            }
            else if (Input.GetKey(KeyCode.A))
            {

                Rotate(RotationSpeed);
            }

            if (Input.GetKey(KeyCode.W))
            {
                Move(Speed);
            }else if (Input.GetKey(KeyCode.S))
            {
                Move(-Speed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }


        private void Rotate(float direction)
        {
            transform.Rotate(new Vector3(0,0,direction));
        }
        private void Move(float speed)
        {
            transform.position += transform.up * speed;
        }


        private void Shoot()
        {


            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.TransformDirection(Vector3.up) * 1000);

            Debug.DrawLine(transform.position + transform.up, transform.TransformDirection(Vector3.up) * 1000);
            if (hit)
            {
                hit.transform.gameObject.GetComponent<Damagable>().TakeDamage(100);
                print(hit.transform);
                meteorSpawnerMono.data.CurrentMeteorCount--;
                meteorSpawnerMono.CheckWave();

            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Meteor>() != null)
            {
                Destroy(collision.gameObject);
                meteorSpawnerMono.data.CurrentMeteorCount--;
                meteorSpawnerMono.CheckWave();
                TakeDamage(21);
            
            }

        }

        private void OnDestroy()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

    }
}