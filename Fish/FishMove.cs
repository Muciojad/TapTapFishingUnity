using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic fish movement.
/// </summary>
public class FishMove : MonoBehaviour
{
    [SerializeField] private Vector3 _swimSpeed = new Vector2(1f,0f);
    
    void Update()
    {
        // simpliest way to move fish with given velocity
        transform.position -= _swimSpeed * Time.deltaTime;
        // if fish is out of screen -> disable it
        if(transform.position.x < GameBordersController.instance.getLeftBorder().x)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Detect collisions.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if collision is with bait and this single fish isn't the red one, play sound and add bubble splash particle
        if(collision.gameObject.tag.CompareTo("Bait") == 0 && gameObject.tag.CompareTo("RedFish") != 0)
        {
            BubbleSplashDispatcher.instance.AddBubbleSplash(transform.position);
            GlobalSoundSystem.instance.PlaySound();

            gameObject.SetActive(false);
        }
    }
}
