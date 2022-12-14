using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    //Para que una clase sea visible entre proyectos, dicha clase debe ser (public) 
    public class Pokemon
    {
        //Propiedades

        public int Id { get; set; }

        [DisplayName("Número")] //( Annotations) (DisplayName) se puede usar para agregar una etiqueta a la propiedad en el DataGriv 
        public int Numero { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public Elemento Tipo { get; set; }
        public Elemento Debilidad { get; set; }
        
    }
}
