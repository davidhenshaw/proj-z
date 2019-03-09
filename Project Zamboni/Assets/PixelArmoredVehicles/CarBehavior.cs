using UnityEngine;
using System.Collections;

public class CarBehavior : MonoBehaviour {
    public bool OnlyKeyboard_setFalseToTouchControl;// set FALSE to use touchscreen
    public bool moveCamera;//your camera must compare tag "MainCamera"
    public bool tankControl;//rotation
    public bool dont_show_touch_Buttons;//if TouchControl is activated

    public float max_power;//characteristics
    public float min_power;
    public float maxsteer;

    public float speedometer;

    private Transform wheel_L;
    private Transform wheel_R;

    private Rigidbody2D Car_Body;
    private GameObject Camera;
    private GameObject help;
    private float speed=0;
    private float gas_pedal;
    private bool touch_gas;
    private bool touch_stop;
    private float cursteer;
    private bool invesion;
	void Start () {
        invesion = false;
        Camera = GameObject.FindWithTag("MainCamera");
        Car_Body = gameObject.GetComponent<Rigidbody2D>();

        wheel_L = transform.Find("wheel L");
        wheel_R = transform.Find("wheel R");
	}

    void OnGUI() {
        if (!dont_show_touch_Buttons & !OnlyKeyboard_setFalseToTouchControl) {
            int x = Screen.width;
            int y = Screen.height;
            if (GUI.Button(new Rect(x * 0f, y * 0.7f, x * 0.2f, y * 0.3f), "Down")){}
            if (GUI.Button(new Rect(x * 0f, y * 0.4f, x * 0.2f, y * 0.3f), "UP")) { }
            if (GUI.Button(new Rect(x * 0.6f, y * 0.7f, x * 0.2f, y * 0.3f), "<")) { }
            if (GUI.Button(new Rect(x * 0.8f, y * 0.7f, x * 0.2f, y * 0.3f), ">")) { }
        }
    }
	void Update () {
        if (wheel_L != null & wheel_R != null)
        {
            wheel_L.localRotation = Quaternion.Euler(new Vector3(0, 0, cursteer / maxsteer * 30));
            wheel_R.localRotation = Quaternion.Euler(new Vector3(0, 0, cursteer / maxsteer * 30));
        }
        
        if (moveCamera) Camera.transform.position = new Vector3(transform.position.x, transform.position.y, -2 - Mathf.Abs(gas_pedal) * 0.02f);
        if (OnlyKeyboard_setFalseToTouchControl)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Gas();
            }

            else if (!Input.GetKey(KeyCode.S))
            {
                Neytralka();
            }

            if (Input.GetKey(KeyCode.S))
            {
                Tormoz();
            }


            if (Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D))
            {
                Left();
            }

            else if (Input.GetKey(KeyCode.D))
            {
                Right();
            }

            else
            {
                Straight();
            }

        }
        else
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Vector2 pos = new Vector2(Input.GetTouch(i).position.x / Screen.width, Input.GetTouch(i).position.y / Screen.height);
                if (pos.x < 0.2f & pos.y > 0.3f & pos.y < 0.6f) { touch_gas = true; touch_stop = false; }
                else if (pos.x < 0.2f & pos.y < 0.3f) { touch_gas = false; touch_stop = true; }
                if (pos.x < 0.8f & pos.x > 0.6f & pos.y < 0.3f) { Left(); if (Input.GetTouch(i).phase == TouchPhase.Ended) { Straight(); } }
                else if (pos.x > 0.8f & pos.y < 0.3f) { Right(); if (Input.GetTouch(i).phase == TouchPhase.Ended) { Straight(); } }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended) { touch_gas = false; touch_stop = false; }

            }
            if (Input.touchCount == 0) { touch_gas = false; touch_stop = false; }

            if (touch_gas) { Gas(); }
            else if (touch_stop) { Tormoz(); }
            else { Neytralka(); }
        }

        if (gas_pedal >= 0) { invesion = false; }
        if (gas_pedal < 0) { invesion = true; }

    }
    void FixedUpdate() {
            if (invesion)
            {
                speed = -Car_Body.velocity.magnitude;
                if (tankControl) Car_Body.AddTorque(-cursteer);
                else Car_Body.AddTorque(cursteer * speed + cursteer * speed * speed * 0.05f);
            }
            else
            {
                speed = Car_Body.velocity.magnitude;
                if (tankControl) Car_Body.AddTorque(cursteer);
                else Car_Body.AddTorque(cursteer * speed - cursteer * speed * speed * 0.05f);
            }
            speedometer = Mathf.Round(speed * 50f);

            Car_Body.AddRelativeForce(new Vector2(0, gas_pedal));
    }

    void Gas() 
    {
        if (gas_pedal < 0) { Tormozhenie(); }
        else if (gas_pedal < max_power) { Razgon(); }
        else { Maksimalka(); }
    }
    void Tormoz()
    {
        if (gas_pedal > 0) { Tormozhenie_Nazad(); }
        else if (gas_pedal > min_power) { Razgon_Nazad(); }
        else { Maksimalka_Nazad(); }
    }

    void Left()
    {
        if (cursteer < maxsteer) { cursteer += Time.deltaTime * 10; }
        else { cursteer = maxsteer; }
    }
    void Right()
    {
        if (cursteer > -maxsteer) { cursteer -= Time.deltaTime * 10; }
        else { cursteer = -maxsteer; }
    }
    void Straight() {
        cursteer = 0; 
    }
    void Tormozhenie(){
        gas_pedal += Time.deltaTime * 100;
    }
    void Razgon()
    {
        gas_pedal += Time.deltaTime * 40;
    }
    void Maksimalka()
    {
        gas_pedal = max_power;
    }
    void Neytralka()
    {
        if (gas_pedal > 3) { gas_pedal -= Time.deltaTime * 20;}
        else if (gas_pedal < -3) { gas_pedal += Time.deltaTime * 30;}
        else { gas_pedal = 0;}
    }
    void Tormozhenie_Nazad()
    {
        gas_pedal -= Time.deltaTime * 120;
    }
    void Razgon_Nazad()
    {
        gas_pedal -= Time.deltaTime * 30;
    }
    void Maksimalka_Nazad()
    {
        gas_pedal = min_power;
    }
}