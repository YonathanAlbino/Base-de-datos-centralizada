using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio; //Utilizacion del proyecto dominio con sus respectivas clases publicas
using negocio; //Utilizacion del proyecto negocio con sus respectivas clases publicas

namespace Ejemplos_ado_net
{
    //Capa de presentacion
    public partial class Form1 : Form
    {

        private List<Pokemon> listaPokemon; //Creo atributo de tipo lista de Pokemon
       
        public Form1()
        {
            InitializeComponent();
        }

        private void dgvPokemons_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e) //Evento load ventana principal
        {
            cargar();
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e) //Cuando se cambia la seleccion de la grilla-dgvPokemons, se cambia la imagen en la pictureBox-pbxPokemon
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;  //Se obtiene el objeto enlazado de la grilla-dgvPokemons en la fila actual, y se lo transforma en un objeto de tipo Pokemon y se guarda en (seleccionado)
            cargarImagen(seleccionado.UrlImagen); //Llamado al metodo (cargar imagen)
        }

        private void cargar() //Metodo de carga para el DataGriv
        {
            PokemonNegocio negocio = new PokemonNegocio(); //Creo un objeto de tipo (negocio) para poder utilizar su metodo de conexion a db
            try //Recibo la excepcion desde la definicion del metodo listar y se toman deciciones desde este (Try-catch)
            {
                listaPokemon = negocio.listar(); //Cargo el atributo-lista con el metodo (listar de la clase PokemonNegocio)
                dgvPokemons.DataSource = listaPokemon; //Le paso el atributo-lista a la DataGriv
                dgvPokemons.Columns["UrlImagen"].Visible = false; //Oculta la columna (UrlImagen) de la dgvPokemons
                dgvPokemons.Columns["Id"].Visible = false;
                cargarImagen(listaPokemon[0].UrlImagen); //Al cargase la ventana, se selecciona en la pbxPokemon la listaPokemon con la propiedad UrlImagen en el indice[0]

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen) //Metodo privado encargado de cargar imagen
        {
            try
            {
                pbxPokemon.Load(imagen); //Se carga en la pictureBox-pbxPokemon el objeto (seleccionado) con la propiedad (UrlImagen) obtenida anteriormente
               
            }
            catch (Exception ex)
            {

                pbxPokemon.Load("https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"); // Carga una imagen pre-seleccionada en caso de error
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e) //Evento agregar nuevo Pokemon a la DB
        {
            FrmAltaPokemon alta = new FrmAltaPokemon(); //Creo un objeto de tipo (FrmAltaPokemon) para navegar hacia la clase-ventana 
            alta.ShowDialog();
            cargar(); //Reutilizacion del metodo cargar para actualizar el DataGriv luego de una modificacion

        }

        private void btnModificar_Click(object sender, EventArgs e) //Evento Modificar Pokemon
        {
            Pokemon seleccionado; 
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; //Variable guardar el pokemon seleccionado de la grilla (dgvPokemons)

            FrmAltaPokemon modificar = new FrmAltaPokemon(seleccionado); //Creo un objeto de tipo (FrmAltaPokemon) con un objeto pokemon para navegar hacia la clase-ventana y modificar el pokemon que se manda por parametro
            modificar.ShowDialog();
            cargar(); //Reutilizacion del metodo cargar para actualizar el DataGriv luego de una modificacion

        }
    }
}
