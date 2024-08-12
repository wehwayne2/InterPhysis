using UnityEngine;
using Uduino;

public class ReceiveIMUValues : MonoBehaviour {

    Vector3 position;
    Vector3 rotation;
    public Vector3 rotationOffset;
    public float speedFactor = 15.0f;
    public string imuName = "r"; // You should ignore this if there is one IMU.

    void Start() {
        // Note that here, we don't use the delegate but the Events, assigned in the Inspector Panel
        UduinoManager.Instance.OnDataReceived += ReadIMU;
    }

    void Update() {
        // Update logic if necessary
    }

    public void ReadIMU(string data, UduinoDevice device) {
        // Debug.Log(data);

        // Split by '|'
        string[] parts = data.Split('|');
        if (parts.Length == 7) {
            string imuData = parts[6]; // Get the IMU part of the message

            // Split the IMU data by '/'
            string[] values = imuData.Split('/');
            if (values.Length == 5 && values[0] == imuName) // Rotation of the first one 
            {
                try
                {
                    float w = float.Parse(values[1]);
                    float x = float.Parse(values[2]);
                    float y = float.Parse(values[3]);
                    float z = float.Parse(values[4]);

                    // Correctly apply the quaternion rotation
                    Quaternion targetRotation = new Quaternion(w, y, x, z);
                    this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, targetRotation, Time.deltaTime * speedFactor);
                }
                catch (System.FormatException e)
                {
                    Debug.LogError("Failed to parse IMU data: " + imuData + " - " + e.Message);
                }
            }
            else
            {
                Debug.LogWarning("Unexpected IMU data format: " + imuData);
            }
        } else {
            Debug.LogWarning("Unexpected data format: " + data);
        }

        // Apply the rotation offset to the parent object
        this.transform.parent.transform.eulerAngles = rotationOffset;
    }
}