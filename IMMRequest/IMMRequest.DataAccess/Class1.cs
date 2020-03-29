using System;

/*
Data Access es la encargada de conectarse a la base de datos 
y obtener los datos y darselo a la capa de Business Logic

Es la capa más baja, es el encargado de CATCHEAR cualquier
excepcion en el intento de tanto persistir como obtener datos
de la base y una vez cacheada esa excepcion, lanzarla para arriba 
como una excepcion creada por el mismo, de esa forma estamos no 
revelando informacion importante que surge cuando nos da ciertos 
problemas.

Necesitamos tener un buen manejo de errores aca.
*/

namespace IMMRequest.DataAccess
{
    public class Class1
    {
    }
}
