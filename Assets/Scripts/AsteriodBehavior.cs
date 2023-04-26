using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteriodBehavior : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 3f;
    [SerializeField] GameObject explosion;
    public UnityEvent onAsteriodDestroy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            onAsteriodDestroy.Invoke();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Instantiate(explosion, this.transform);
            Destroy(other.gameObject);
            Destroy(this.gameObject,5);
        }
    }

   
}
