using UnityEngine;

public class Collectible : MonoBehaviour
{

    public float rotationSpeed;
    public GameObject onCollectEffect;

    //sounds
    public AudioClip collectSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy the collectible
        if (other.CompareTag("Player"))
        {
            //sounds
            if (collectSound != null) AudioSource.PlayClipAtPoint(collectSound, transform.position);

            Destroy(gameObject);

            //instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation); //(which object, what position, what rotation) 
        }
    }
}
