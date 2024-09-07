using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private float flameSpreadSpeed;
    [SerializeField] private float flameSpreadDistance;
    [SerializeField] private float flameAnimSpeed;
    [SerializeField] private float flameAnimHeight;

    [HideInInspector] public static List<Transform> flames = new List<Transform>();
    [HideInInspector] public static List<float> flameAnimOffset = new List<float>();
    [HideInInspector] public static List<Vector2> flameBasePositions = new List<Vector2>();

    private float animTime = 0.0f;
    private float spreadTime = 0.0f;

    private void Start()
    {
        // Fire is always composed of a single flame, regardless
        flames.Add(Instantiate(flamePrefab, this.transform.position, Quaternion.identity, this.transform).transform);
        flameAnimOffset.Add(Random.Range(0.0f, 10.0f));
        flameBasePositions.Add(this.transform.position);
    }

    private void Update()
    {
        animTime += Time.deltaTime;
        spreadTime += Time.deltaTime * flameSpreadSpeed;

        // Flame animation
        for (int i = 0; i < flames.Count; i++)
        {
            flames[i].position = new Vector2(flameBasePositions[i].x, flameBasePositions[i].y + Mathf.Abs(flameAnimHeight * Mathf.Sin(flameAnimSpeed * (animTime + flameAnimOffset[i]))));
        }

        // Flame spread
        if (spreadTime >= 1.0f)
        {
            List<Transform> newFlames = new List<Transform>();
            List<float> newFlameAnimOffsets = new List<float>();
            List<Vector2> newFlameBasePositions = new List<Vector2>();

            foreach (Transform flame in flames) // Check if we can spread each flame
            {
                float initialAngle = Random.Range(0.0f, 70.0f);
                for (float angle = initialAngle; angle < 360.0f + initialAngle; angle += 360.0f/5) // Check an even spacing around the flame
                {
                    Vector3 newFlamePosition = flame.position + Quaternion.AngleAxis(angle, Vector3.back) * Vector3.up * flameSpreadDistance;
                    LayerMask fireLayerMask = LayerMask.GetMask("Fire");
                    RaycastHit2D hitInfo = Physics2D.CircleCast(newFlamePosition, 0.5f, Vector2.zero, Mathf.Infinity, fireLayerMask);
                    if (hitInfo.collider == null)
                    {
                        newFlames.Add(Instantiate(flamePrefab, newFlamePosition, Quaternion.identity, this.transform).transform);
                        newFlameAnimOffsets.Add(Random.Range(0.0f, 10.0f));
                        newFlameBasePositions.Add(newFlamePosition);
                    }
                }
            }

            flames.AddRange(newFlames);
            flameAnimOffset.AddRange(newFlameAnimOffsets);
            flameBasePositions.AddRange(newFlameBasePositions);

            spreadTime = 0.0f;
        }
    }
}
