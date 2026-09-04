using UnityEngine;

public class ControladorCanon : MonoBehaviour
{
    [Header("Configuración del cañón")]

    // Tubo del cañón que vamos a rotar.
    public Transform tuboCanon;

    // Proyectil que será lanzado.
    public MovimientoProyectil proyectil;

    [Header("Controles")]

    // Ángulo inicial del cañón.
    public float angulo = 45f;

    // Velocidad inicial del proyectil.
    public float velocidad = 10f;

    // Cantidad de grados que cambia el ángulo por segundo.
    public float velocidadCambioAngulo = 30f;

    // Cantidad de velocidad que añadimos o quitamos por segundo.
    public float velocidadCambioVelocidad = 5f;

    [Header("Límites")]

    // Ángulo mínimo permitido.
    public float anguloMinimo = 5f;

    // Ángulo máximo permitido.
    public float anguloMaximo = 85f;

    // Velocidad mínima permitida.
    public float velocidadMinima = 1f;

    // Velocidad máxima permitida.
    public float velocidadMaxima = 30f;

    private void Start()
    {
        // Actualizamos visualmente el cañón al comenzar.
        ActualizarCanon();

        // Sincronizamos los valores con el proyectil.
        SincronizarProyectil();
    }

    private void Update()
    {
        // Flecha izquierda: disminuir ángulo.
        if (Input.GetKey(KeyCode.DownArrow)) // LeftArrow
        {
            angulo -= velocidadCambioAngulo * Time.deltaTime;
        }

        // Flecha derecha: aumentar ángulo.
        if (Input.GetKey(KeyCode.UpArrow)) // RightArrow
        {
            angulo += velocidadCambioAngulo * Time.deltaTime;
        }

        // Flecha arriba: aumentar velocidad.
        if (Input.GetKey(KeyCode.RightArrow)) // UpArrow
        {
            velocidad += velocidadCambioVelocidad * Time.deltaTime;
        }

        // Flecha abajo: disminuir velocidad.
        if (Input.GetKey(KeyCode.LeftArrow)) // DownArrow
        {
            velocidad -= velocidadCambioVelocidad * Time.deltaTime;
        }

        // Limitamos el ángulo.
        angulo = Mathf.Clamp(angulo, anguloMinimo, anguloMaximo);

        // Limitamos la velocidad.
        velocidad = Mathf.Clamp(
            velocidad,
            velocidadMinima,
            velocidadMaxima
        );

        // Actualizamos la rotación visual.
        ActualizarCanon();

        // Actualizamos los parámetros del proyectil.
        SincronizarProyectil();

        // R reinicia el proyectil.
        if (Input.GetKeyDown(KeyCode.R))
        {
            proyectil.Reiniciar();
        }
    }

    private void ActualizarCanon()
    {
        // Rotamos el tubo alrededor del eje Z.
        tuboCanon.localRotation = Quaternion.Euler(
            0f,
            0f,
            angulo
        );
    }

    private void SincronizarProyectil()
    {
        // Enviamos los parámetros actuales al proyectil.
        proyectil.anguloLanzamiento = angulo;
        proyectil.velocidadInicial = velocidad;
    }
}