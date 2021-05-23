using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehaviourScript : MonoBehaviour
{
    public float xSpeed = 0;
    public float xSpeedRandom = 0.25f;
    public float ySpeed = 0;
    private bool isAlive = true;
    private SpriteRenderer DSpriteRenderer;
    Collider2D DCollider2D;
    private Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        DSpriteRenderer = GetComponent<SpriteRenderer>();
        DCollider2D = GetComponent<Collider2D>();
        xSpeed = xSpeed + Random.Range(-xSpeedRandom, xSpeedRandom);
        if (xSpeed >= 0) DSpriteRenderer.flipX = true;

        StartCoroutine(ChangeDirection(Random.Range(2.5f, 4f)));

    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.Translate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);

    }

    private void SetDirection(float direction)
    {
        xSpeed *= direction;
    }

    public void DuckDead()
    {
        Anim.SetTrigger("DuckDead");
        isAlive = false;
        DCollider2D.enabled = false;
        ySpeed = -1.5f;
        xSpeed = 0f;
        StartCoroutine(DisappearSec(5f));
        StartCoroutine(PlaySound(0.3f));
    }


    IEnumerator DisappearSec(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    IEnumerator PlaySound(float time)
    {
        yield return new WaitForSeconds(time);

    }
    IEnumerator ChangeDirection(float time)
    {
        yield return new WaitForSeconds(time);
        if (isAlive) ySpeed = Mathf.Abs(xSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("45465654");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("adasdasd");
    }
}
