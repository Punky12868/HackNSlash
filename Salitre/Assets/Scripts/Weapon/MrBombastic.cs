using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MrBombastic : MonoBehaviour
{
    public Player input;

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
    public string throwKey = "Attack";

    private Vector3 startPosition;
    private Vector3 endPosition;
    public float endPosOffsetY;

    public float controllerSens;

    [HideInInspector] public bool isAiming = false;

    GameObject existingExplosionRadius;
    Vector3 direction;

    private void Start()
    {
        input = ReInput.players.GetPlayer(0);
        SetupLineRenderer();
    }

    private void Update()
    {
        if (input.GetButtonDown(throwKey))
        {
            if (direction != Vector3.zero)
            {
                direction = Vector3.zero;
            }

            StartAiming();
            FindObjectOfType<PlayerInput>().enabled = false;
        }

        if (isAiming)
        {
            Aim();
        }

        if (input.GetButtonUp(throwKey))
        {
            ThrowBomb();
            FindObjectOfType<PlayerInput>().enabled = true;
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

        // Obtener la dirección del input del mouse o el eje
        
        if (GetCurrentInput.isMouseInput)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            mousePosition.y = startPosition.y; // Proyectar el punto en el plano del suelo
            direction = mousePosition - startPosition;
        }
        else
        {
            float horizontalInput = input.GetAxis("Move Horizontal");
            float verticalInput = input.GetAxis("Move Vertical");

            // Obtener la dirección de la cámara sin incluir la componente vertical
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            // Obtener la dirección relativa al espacio de la cámara
            Vector3 inputDirection = horizontalInput * Camera.main.transform.right + verticalInput * cameraForward;

            // Aplicar la sensibilidad del controlador
            direction += inputDirection * controllerSens;

            // Ahora 'direction' contendrá la dirección ajustada según la cámara
        }

        direction = Vector3.ClampMagnitude(direction, maxThrowDistance);

        Vector3 pseudoEndPos = startPosition + direction;
        endPosition = pseudoEndPos + new Vector3(0, endPosOffsetY, 0);

        trajectoryLineRenderer.SetPositions(CalculateTrajectoryPoints(startPosition, endPosition, numberOfPoints));

        DrawExplosionRadius(endPosition);
    }

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

    private Vector3[] CalculateTrajectoryPoints(Vector3 start, Vector3 end, int pointsCount)
    {
        Vector3[] points = new Vector3[pointsCount];

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);

            // Aplica una función cuadrática para ajustar t
            t = Mathf.Pow(t, 2f) / (Mathf.Pow(t, 2f) + Mathf.Pow(1f - t, 2f));

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
            existingExplosionRadius.transform.localScale = new Vector3(explosionRadius, 0.1f, explosionRadius);
        }
        else
        {
            existingExplosionRadius.transform.position = position;
        }
    }

    public void DestroyExplosionRadius()
    {
        trajectoryLineRenderer.positionCount = 0;

        if (existingExplosionRadius != null)
        {
            Destroy(existingExplosionRadius);
            existingExplosionRadius = null;
        }
    }
}
