using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    //Para que una clase sea visible entre proyectos, dicha clase debe ser (public) 
    public class Elemento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString() //Sobreescritura metodo ToString, para poder mostrar en la dgvPokemons la propiedad que corresponde 
        {
            return Descripcion;
        }

     
    }
}
