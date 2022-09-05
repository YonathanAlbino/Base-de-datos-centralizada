using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ElementoNegocio
    {
        public List<Elemento> listar() //Metodo para obtener los datos de la clase (Elemento), imita lo que hace el metodo (PokemonListar) "SELECT"
        {
            List<Elemento> lista = new List<Elemento>(); //Creo la lista de elementos
            AccesoDatos datos = new AccesoDatos(); //Creo un objeto de la clase (AccesoDatos), con propiedades-metodos-atributos necesarios para realizar la conexion a una DB
            try
            {
                datos.setearConsulta("select id, descripcion from ELEMENTOS;"); //Carga la consulta en la tabla dada por parametro
                datos.ejecutarLectura(); //Metodo encargado de ejecutar la lectura y cargar el lector

                while (datos.Lector.Read()) //Si hay un registro entra al while, ademas posiciona un puntero en la siguiente posicion de la tabla
                {
                    Elemento aux = new Elemento(); //En cada vuelta del while crea un nuevo objeto reutilizando la varaible aux, pero crea una nueva instancia de Elemento
                                                   //Y en cada nueva instancia va a ir guardando los datos en las prop que correspondan en cada vueltas del while
                    aux.Id = (int)datos.Lector["id"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    lista.Add(aux); //En esta lista se guardan todas las referencias a todos los objetos que se hayan creado durante el while
                }

                return lista; //Retorna la lista
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
