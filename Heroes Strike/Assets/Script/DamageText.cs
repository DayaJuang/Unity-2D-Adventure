using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damageText,criticalDamage;
    public float jitter = .3f;

    float lifetTime = 1f;
    float movingSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetTime);
        transform.position += new Vector3(0f, movingSpeed*Time.deltaTime, 0f);
    }

    public void SetDamageText(int damage)
    {
        damageText.gameObject.SetActive(true);
        criticalDamage.gameObject.SetActive(false);
        damageText.text = damage.ToString();
        transform.position += new Vector3(Random.Range(-jitter, +jitter), Random.Range(-jitter, +jitter), 0f);
    }

    public void SetCriticalDamageText(int damage)
    {
        damageText.gameObject.SetActive(false);
        criticalDamage.gameObject.SetActive(true);
        criticalDamage.text = damage.ToString();
        transform.position += new Vector3(Random.Range(-jitter, +jitter), Random.Range(-jitter, +jitter), 0f);
    }
}
