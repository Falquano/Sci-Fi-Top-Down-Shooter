using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifetime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;
    [SerializeField] private ShootPlayer OK;
    [SerializeField] private TempoManager tempoManager;

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(2 * transform.position);

        float direction = Random.rotation.eulerAngles.x;
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        if (timer > lifetime) Destroy(gameObject);
        else if (timer > fraction) text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifetime));
    }

    public void SetDamageText(int damage)
    {
        
        text.text = damage.ToString();
 
        //Refaire les tailles de text en fonction des d�g�ts possible 
        if(damage <= 10)
        {
            text.fontSize = 10f;
            text.color = new Color(250, 200, 0, 0.75f);
        }
        else if (damage >= 10 && damage <=20)
        {
            text.fontSize = 10f;
            text.color = new Color(250, 0, 0, 1f);
        }
        else if (damage >= 30 && damage <=60)
        {
            text.fontSize = 25f;
            text.color = new Color(250, 0, 0, 1f);
        }  
        else if (damage >= 61 && damage <=80)
        {
            text.fontSize = 40f;
            text.color = new Color(250, 0, 0, 1f);
        }
        else if (damage == 120)
        {
            text.fontSize = 55f;
            text.color = new Color(250, 0, 0, 1f);
        }

    }

   


}