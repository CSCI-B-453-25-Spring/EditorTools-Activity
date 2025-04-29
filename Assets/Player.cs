using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotateSpeed = 60;
    public int HP = 100;
    public TextMeshProUGUI hpValueText;
    public GameObject lightBulb;

    public float minPressure;
    public float maxPressure;
    public float maxBrightness;

    bool isGreenOn = true;
    bool isYellowOn = false;
    bool isRedOn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
        ControllerManager.Initialize("COM4", 9600);
        ControllerManager.buttonPress.AddListener(ToggleLight);
        ControllerManager.Toggle("green");
    }

    // Update is called once per frame
    void Update()
    {
        ControllerManager.Update();

        transform.Translate(Vector3.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime * 60 * Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.C))
        {
            ControllerManager.Close();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ControllerManager.Toggle("green");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ControllerManager.Toggle("red");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ControllerManager.Toggle("yellow");
        }

        float pressureValue = ControllerManager.LatestAltitude;
        Debug.Log(pressureValue);
        float normalizedPressure = Mathf.InverseLerp(minPressure, maxPressure, pressureValue);
        lightBulb.GetComponent<Light>().intensity = Mathf.Lerp(0, maxBrightness, normalizedPressure);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hp")
        {
            // Heal
            HP += 10;
            UpdateUI();
            UpdateLights(HP);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Sphere")
        {
            // Take damage
            HP -= 10;
            UpdateUI();
            UpdateLights(HP);
            if (isGreenOn)
            {
                ControllerManager.Blink("green", true, 100);
            }
            Destroy(other.gameObject);
        }
    }

    void UpdateUI()
    {
        hpValueText.text = HP.ToString();
    }

    void UpdateLights(int HP)
    {
        if (HP > 50 && !isGreenOn)
        {
            ControllerManager.Toggle("green");
            isGreenOn = true;
        }
        else if (HP <= 50 && HP > 25 && !isYellowOn)
        {
            if (isGreenOn)
            {
                ControllerManager.Toggle("green");
                isGreenOn= false;
            }
            ControllerManager.Toggle("yellow");
            isYellowOn = true;
        }
        else if (HP <= 25)
        {
            if (isYellowOn)
            {
                ControllerManager.Toggle("yellow");
                isYellowOn = false;
            }
            if (!isRedOn)
            {
                ControllerManager.Toggle("red");
                isRedOn = true;
            }
        }
    }

    void ToggleLight()
    {
        lightBulb.SetActive(false);
    }
}