using UnityEngine;

public class ClickSpawn : MonoBehaviour
{
    public SandSpawner spawner;
    private bool canClick = true;

    void Update()
    {
        if (!canClick || spawner == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log(" Клик пойман! Вызываем ToggleSpawn...");
                    spawner.ToggleSpawn();
                    canClick = false;
                    Invoke(nameof(ResetClick), 0.2f); // Защита от двойного клика
                }
            }
        }
    }

    void ResetClick() => canClick = true;
}