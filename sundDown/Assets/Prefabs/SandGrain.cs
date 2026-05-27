using UnityEngine;

public class SandGrain : MonoBehaviour
{
    private Rigidbody rb;
    private const float SleepVelocityThreshold = 0.02f;
    private const float SettleTime = 0.4f;
    private float settledTimer = 0f;

    void Awake() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        if (rb.isKinematic) return;

        // Проверяем, остановилась ли песчинка
        if (rb.linearVelocity.sqrMagnitude < SleepVelocityThreshold &&
            rb.angularVelocity.sqrMagnitude < SleepVelocityThreshold)
        {
            settledTimer += Time.fixedDeltaTime;
            if (settledTimer >= SettleTime)
            {
                rb.isKinematic = true; // Отключаем симуляцию, экономим FPS
            }
        }
        else
        {
            settledTimer = 0f;
        }
    }

    public void WakeUp(Vector3 position)
    {
        gameObject.SetActive(true); 
        transform.position = position;
        rb.isKinematic = false;
        rb.WakeUp();
        rb.AddForce(Random.insideUnitSphere * 0.01f, ForceMode.Impulse);
        settledTimer = 0f;
    }
}