using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio; //Inclusion proyecto (dominio)
using negocio; //Inclusion proyecto (negocio)

namespace Ejemplos_ado_net
{
    public partial class FrmAltaPokemon : Form
    {
        private Pokemon pokemon = null; //Atributo privado 
        public FrmAltaPokemon()
        {
            InitializeComponent();
        }

        public FrmAltaPokemon(Pokemon pokemon) //Sobrecarga del constructor para poder modificar un Pokemon
        {
            InitializeComponent();
            Text = "Modificar pokemon";
            this.pokemon = pokemon; //Al llamar al evento (modificar) el atributo (pokemon) se carga con los datos del pokemon que se pretende modificar
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e) //Evento agregar un nuevo Pokemon
        {
            //Pokemon poke = new Pokemon(); //Creo un objeto Pokemon para pasarle las propiedades-datos que ingrese el usuario
            PokemonNegocio negocio = new PokemonNegocio(); //Creo un objeto de tipo (PokemonNegocio) para acceder a la base de datos
            try
            {
                if (pokemon == null)
                    pokemon = new Pokemon();

                pokemon.Nombre = textNombre.Text;
                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                pokemon.Tipo = (Elemento)cboTipo.SelectedItem; //Cargo la propiedad (Tipo) del (Pokemon) con los datos obetenidos del ComboBox ("La instancia de la propiedad (Tipo) viene dada desde el metodo (listar()) de la clase (ElementoNegocio) ")
                pokemon.Debilidad = (Elemento)cboDebilidad.SelectedItem; //Cargo la propiedad (Debilidad) del (Pokemon) con los datos obetenidos del ComboBox. ("La instancia de la propiedad (Debelidad) viene dada desde el metodo (listar()) de la clase (ElementoNegocio) ")

                if(pokemon.Id != 0)
                {
                    negocio.modificar(pokemon); //Metodo modificar
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(pokemon); //Envio por parametro el objeto (poke) al metodo (agregar) de la clase (PokemonNegocio)
                    MessageBox.Show("Agregado exitosamente");
                }

                Close(); //Cierra la ventana
            }

            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("La descripcion es demasiado larga");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); //En caso de error, el manejo de excepciones muestra un mensaje "amigable"
            }
        }

        private void FrmAltaPokemon_Load(object sender, EventArgs e) //Evento load (Formulario-AltaPokemon)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio(); //Creo objeto de la clase (ElementoNegocio) para acceder a su metodo (listar())

            try
            {
                cboTipo.DataSource = elementoNegocio.listar(); //Cargo el (CboTipo) con los datos traidos mediante el metodo (listar())
                cboTipo.ValueMember = "Id"; //Se escoge la "columna" como clave oculta para el ComboBox
                cboTipo.DisplayMember = "Descripcion"; //Y aca se elige la "columna" a mostrar en el ComboBox 
                
                cboDebilidad.DataSource = elementoNegocio.listar(); //Cargo el (CboDebilidad) con los datos traidos mediante el metodo (listar())
                cboDebilidad.ValueMember = "Id";
                cboDebilidad.DisplayMember = "Descripcion";

                if(pokemon != null) //Si el atributo privado (pokemon) esta cargado con datos, significa que se llamo al evento (modificar) 
                {
                    txtNumero.Text = pokemon.Numero.ToString(); //Recargo los controles de la ventana (FrmAltaPokemon) con los datos del pokemon seleccionado para modificar
                    textNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cboTipo.SelectedValue = pokemon.Tipo.Id; //Asignacion de valor para la clave del (cbo.Tipo)
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id; //Asignacion de valor para la clave del (cbo.Tipo)
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e) //Evento leave de (txtUrlImagen), intenta cargar la imagen url a la (pcnPokemonAlta)
        {
            cargarImagen(txtUrlImagen.Text); //Llamado a la funcion cargar imagen
        }

        private void cargarImagen(string imagen) //Metodo privado encargado de cargar imagen
        {
            try
            {
                pcbAltaPokemon.Load(imagen); //Se carga en la pictureBox-pbxPokemon el objeto (seleccionado) con la propiedad (UrlImagen) obtenida anteriormente

            }
            catch (Exception ex)
            {

                pcbAltaPokemon.Load("https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"); // Carga una imagen pre-seleccionada en caso de error
            }
        }
    }
}
