using UnityEngine;

public class MovimientoProyectil : MonoBehaviour
{
    [Header("Parámetros del lanzamiento")]

    // Velocidad inicial del proyectil en metros por segundo.
    public float velocidadInicial = 10f;

    // Ángulo de lanzamiento medido en grados.
    public float anguloLanzamiento = 45f;

    // Aceleración gravitatoria en metros por segundo al cuadrado.
    public float gravedad = 9.81f;

    // Posición inicial del proyectil.
    private Vector2 posicionInicial;

    // Tiempo transcurrido desde el lanzamiento.
    private float tiempoTranscurrido;

    // Indica si el proyectil está actualmente en movimiento.
    private bool estaLanzado = false;

    private void Start()
    {
        // Guardamos la posición inicial.
        posicionInicial = transform.position;
    }

    private void Update()
    {
        // Si presionamos espacio y el proyectil está detenido,
        // comenzamos el lanzamiento.
        if (!estaLanzado && Input.GetKeyDown(KeyCode.Space))
        {
            Lanzar();
        }

        // Si el proyectil no está lanzado, no hacemos nada.
        if (!estaLanzado)
            return;

        // Aumentamos el tiempo de simulación.
        tiempoTranscurrido += Time.deltaTime;

        // Convertimos el ángulo de grados a radianes.
        float anguloEnRadianes = anguloLanzamiento * Mathf.Deg2Rad;

        // Ecuación del movimiento horizontal:
        // x = x0 + v0 * cos(θ) * t
        float x = posicionInicial.x
                + velocidadInicial * Mathf.Cos(anguloEnRadianes) * tiempoTranscurrido;

        // Ecuación del movimiento vertical:
        // y = y0 + v0 * sin(θ) * t - (1/2) * g * t²
        float y = posicionInicial.y
                + velocidadInicial * Mathf.Sin(anguloEnRadianes) * tiempoTranscurrido
                - 0.5f * gravedad * tiempoTranscurrido * tiempoTranscurrido;

        // Aplicamos la posición calculada.
        transform.position = new Vector2(x, y);

        // Si llega al nivel del suelo, detenemos el lanzamiento.
        if (transform.position.y <= -2.5f)
        {
            estaLanzado = false;
        }
    }

    public void Lanzar()
    {
        // Reiniciamos el tiempo.
        tiempoTranscurrido = 0f;

        // Comenzamos el movimiento.
        estaLanzado = true;

        // Guardamos la posición desde donde se realizó el lanzamiento.
        posicionInicial = transform.position;
    }

    public void Reiniciar()
    {
        // Detenemos el movimiento.
        estaLanzado = false;

        // Reiniciamos el tiempo.
        tiempoTranscurrido = 0f;

        // Devolvemos el proyectil a su posición inicial.
        transform.position = posicionInicial;
    }
}