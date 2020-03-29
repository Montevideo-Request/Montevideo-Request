using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    
/* 
Business Logic va a ser la capa que es encargada de hacer
las funcionalidades con los objetos del dominio para
cumplir los requerimientos, Aca va a estar toda la logica.

Va a usar el acceso a datos para poder persistir los datos
(borrar, guardar, obtenerlos...) 
*/

    public class RequestLogic
    {
        public List<Request> GetAll()
        {
            return new List<Request>()
            {
                new Request()
                {
                    Id = 0,
                    RequestorsName = "Test Name"
                }
            };
        }
    }
}