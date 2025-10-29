//using UnityEngine;
//using System.Collections.Generic;

//public class RandomSpawner : MonoBehaviour
//{
//    [Header("Configuración")]
//    public GameObject prefab;          // Prefab que quieres instanciar
//    public int cantidad = 10;          // Cuántos objetos generar
//    public Vector2 rangoX = new Vector2(-10f, 10f);
//    public Vector2 rangoZ = new Vector2(-10f, 10f);
//    public float distanciaMinima = 2f; // Distancia mínima entre objetos

//    private List<Vector3> posicionesUsadas = new List<Vector3>();

//    void Start()
//    {
//        GenerarObjetos();
//    }

//    void GenerarObjetos()
//    {
//        int intentosMax = 1000; // Evita bucles infinitos
//        int intentos = 0;

//        for (int i = 0; i < cantidad; i++)
//        {
//            Vector3 nuevaPos;
//            bool posicionValida = false;

//            // Buscar una posición válida sin que se encimen
//            do
//            {
//                float x = Random.Range(rangoX.x, rangoX.y);
//                float z = Random.Range(rangoZ.x, rangoZ.y);
//                nuevaPos = new Vector3(x, 0, z);

//                // Comprobar distancia con los ya generados
//                posicionValida = true;
//                foreach (Vector3 pos in posicionesUsadas)
//                {
//                    if (Vector3.Distance(nuevaPos, pos) < distanciaMinima)
//                    {
//                        posicionValida = false;
//                        break;
//                    }
//                }

//                intentos++;
//                if (intentos > intentosMax)
//                {
//                    Debug.LogWarning("No se pudieron colocar todos los objetos sin encimarse.");
//                    return;
//                }

//            } while (!posicionValida);

//            posicionesUsadas.Add(nuevaPos);
//            Instantiate(prefab, nuevaPos, Quaternion.identity);
//        }
//    }
//}

using UnityEngine;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefab;
    public int cantidad = 10;
    public Vector2 rangoX = new Vector2(-10f, 10f);
    public Vector2 rangoZ = new Vector2(-10f, 10f);
    public float distanciaMinima = 2f;

    private List<Vector3> posicionesUsadas = new List<Vector3>();

    void Start()
    {
        GenerarObjetos();
    }

    void GenerarObjetos()
    {
        int intentosMax = 1000;
        int intentos = 0;

        for (int i = 0; i < cantidad; i++)
        {
            Vector3 nuevaPos;
            bool posicionValida = false;

            do
            {
                float x = Random.Range(rangoX.x, rangoX.y);
                float z = Random.Range(rangoZ.x, rangoZ.y);

                Vector3 centro = transform.position;
                nuevaPos = new Vector3(x, 0, z) + centro;

                posicionValida = true;
                foreach (Vector3 pos in posicionesUsadas)
                {
                    if (Vector3.Distance(nuevaPos, pos) < distanciaMinima)
                    {
                        posicionValida = false;
                        break;
                    }
                }

                intentos++;
                if (intentos > intentosMax)
                {
                    Debug.LogWarning("No se pudieron colocar todos los objetos sin encimarse.");
                    return;
                }

            } while (!posicionValida);

            posicionesUsadas.Add(nuevaPos);
            Instantiate(prefab, nuevaPos, Quaternion.identity);
        }
    }
}

