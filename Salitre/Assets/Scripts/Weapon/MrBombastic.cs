using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBombastic : MonoBehaviour
{
    public LineRenderer trajectoryLineRenderer;
    public GameObject explosionRadiusPrefab;
    public GameObject bombPrefab;  // Agrega un prefab de la bomba aquí

    [Header("Throw Settings")]
    public float throwForce = 10f;
    public float maxThrowDistance = 10f;
    public float trajectoryHeight = 2f;
    public float roundnessFactor = 1f;
    public int numberOfPoints = 30;

    [Header("Trajectory Visualization")]
    public float lineRendererWidth = 0.1f;
    public Material lineRendererMaterial;

    [Header("Explosion Settings")]
    public float explosionRadius = 2f;

    [Header("Controls")]
    public KeyCode throwKey = KeyCode.Space;

    private Vector3 startPosition;
    private Vector3 endPosition;
    public float endPosOffsetY;

    private bool isAiming = false;

    GameObject existingExplosionRadius;

    private void Start()
    {
        SetupLineRenderer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            StartAiming();
        }

        if (isAiming)
        {
            Aim();
        }

        if (Input.GetKeyUp(throwKey))
        {
            ThrowBomb();
        }
    }

    private void StartAiming()
    {
        trajectoryLineRenderer.positionCount = numberOfPoints;
        isAiming = true;
    }

    private void Aim()
    {
        startPosition = transform.position;
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 direction = mousePosition - startPosition;
        direction = Vector3.ClampMagnitude(direction, maxThrowDistance);

        Vector3 pseudoEndPos = startPosition + direction;
        endPosition = pseudoEndPos + new Vector3(0, endPosOffsetY, 0);

        trajectoryLineRenderer.SetPositions(CalculateTrajectoryPoints(startPosition, endPosition, numberOfPoints));

        DrawExplosionRadius(endPosition);
    }

    /*private void ThrowBomb()
    {
        isAiming = false;
        trajectoryLineRenderer.positionCount = 0;

        Vector3 throwDirection = (endPosition - startPosition).normalized;

        // Crear la bomba y configurarla para que siga la parábola
        GameObject bomb = Instantiate(bombPrefab, startPosition, Quaternion.identity);
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        bombRigidbody.velocity = throwDirection * throwForce;

        DestroyExplosionRadius();
    }*/

    private void ThrowBomb()
    {
        Vector3 throwDirection = (endPosition - startPosition).normalized;

        // Crear la bomba y configurarla para que siga la parábola
        GameObject bomb = Instantiate(bombPrefab, startPosition, Quaternion.identity);
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        bombRigidbody.velocity = throwDirection * throwForce;

        // Obtener los puntos de la trayectoria
        Vector3[] trajectoryPoints = CalculateTrajectoryPoints(startPosition, endPosition, numberOfPoints);

        // Iniciar una corrutina para seguir la trayectoria
        StartCoroutine(FollowTrajectory(bomb.transform, trajectoryPoints));

        isAiming = false;
        trajectoryLineRenderer.positionCount = 0;
        DestroyExplosionRadius();
    }

    private IEnumerator FollowTrajectory(Transform bombTransform, Vector3[] trajectoryPoints)
    {
        for (int i = 0; i < trajectoryPoints.Length; i++)
        {
            bombTransform.position = trajectoryPoints[i];
            yield return null; // Esperar un frame antes de pasar al siguiente punto
        }

        // Destruir la bomba al finalizar la trayectoria (puedes ajustar esto según tus necesidades)
        //Destroy(bombTransform.gameObject);
    }


    /*private Vector3[] CalculateTrajectoryPoints(Vector3 start, Vector3 end, int pointsCount)
    {
        Vector3[] points = new Vector3[pointsCount];

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);
            float height = Mathf.Lerp(start.y, end.y, t) + trajectoryHeight * Mathf.Sin(t * Mathf.PI);
            float distance = Mathf.Lerp(0, Vector3.Distance(start, end), t);
            Vector3 point = start + distance * (end - start).normalized + Vector3.up * height;

            points[i] = point;
        }

        return points;
    }*/

    private Vector3[] CalculateTrajectoryPoints(Vector3 start, Vector3 end, int pointsCount)
    {
        Vector3[] points = new Vector3[pointsCount];

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);

            // Ajusta la curva para hacerla más suave y redonda
            float height = Mathf.Lerp(start.y, end.y, t) + trajectoryHeight * Mathf.Sin(t * Mathf.PI * roundnessFactor);

            float distance = Mathf.Lerp(0, Vector3.Distance(start, end), t);
            Vector3 point = start + distance * (end - start).normalized + Vector3.up * height;

            points[i] = point;
        }

        return points;
    }

    private void SetupLineRenderer()
    {
        trajectoryLineRenderer.startWidth = lineRendererWidth;
        trajectoryLineRenderer.endWidth = lineRendererWidth;
        trajectoryLineRenderer.material = lineRendererMaterial;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    private void DrawExplosionRadius(Vector3 position)
    {
        
        if (existingExplosionRadius == null)
        {
            //DestroyExplosionRadius();
            Instantiate(explosionRadiusPrefab, position, Quaternion.identity);
            existingExplosionRadius = GameObject.FindWithTag("ExplosionRadius");
        }
        else
        {
            existingExplosionRadius.transform.position = position;
        }
    }

    private void DestroyExplosionRadius()
    {
        if (existingExplosionRadius != null)
        {
            Destroy(existingExplosionRadius);
            existingExplosionRadius = null;
        }
    }
}
