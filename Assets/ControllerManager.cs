using UnityEngine;
using System.IO.Ports;
using UnityEngine.Events;

public class ControllerManager
{
    static SerialPort serialPort;
    static bool isInitialized = false;
    public static float LatestAltitude { get; private set; } = 0f;

    public static UnityEvent buttonPress = new UnityEvent();

    public static void Initialize(string portName = "COM4", int baudRate = 9600)
    {
        if (isInitialized) return;

        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 100;
        serialPort.Open();

        isInitialized = true;
        Debug.Log("Controller is ready");
    }

    public static void Toggle(string color)
    {
        if (!isInitialized) return;

        serialPort.WriteLine(color);
    }

    public static void SetBrightness(string color, int value)
    {
        if (!isInitialized) return;

        value = Mathf.Clamp(value, 0, 255);

        serialPort.WriteLine($"brightness {color} {value}");
    }

    public static void Blink(string color, bool on, int interval = 500)
    {
        if (!isInitialized) return;

        if (on) serialPort.WriteLine($"blink {color} on {interval}");
        else serialPort.WriteLine($"blink {color} off");
    }

    public static void Close()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();

            isInitialized = false;
            Debug.Log("Connection is closed.");
        }
    }

    public static void Update()
    {
        if (!isInitialized || serialPort == null || !serialPort.IsOpen) return;

        try
        {
            string incoming = serialPort.ReadLine();
            if (incoming.StartsWith("b"))
            {
                buttonPress.Invoke();
            }
            if (float.TryParse(incoming, out float alt))
            {
                LatestAltitude = alt;
            }
        } catch (System.TimeoutException)
        {

        }
    }
}