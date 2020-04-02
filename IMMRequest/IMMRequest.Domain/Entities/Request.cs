namespace IMMRequest.Domain
{
    
/*
La capa de Dominio es una de las mas altas, es donde van todas
las clases que componen el dominio del negocio
*/

    public class Request
    {
        public int Id { get; set; }
        
        public string RequestorsName { get; set; }

        public string RequestorsEmail { get; set; }

        public string RequestorsPhone { get; set; }

        public Area Area { get; set; }

        public Topic Topic { get; set; }

        public Type Type { get; set; }

        public string State { get; set; }

        private string Description { get; set; }
    }
}
