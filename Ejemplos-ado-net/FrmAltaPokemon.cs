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
        public FrmAltaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e) //Evento agregar un nuevo Pokemon
        {
            Pokemon poke = new Pokemon(); //Creo un objeto Pokemon para pasarle las propiedades-datos que ingrese el usuario
            PokemonNegocio negocio = new PokemonNegocio(); //Creo un objeto de tipo (PokemonNegocio) para acceder a la base de datos
             try
            {
                poke.Numero = int.Parse(txtNumero.Text);
                poke.Nombre = textNombre.Text;
                poke.Descripcion = txtDescripcion.Text;
                poke.Tipo = (Elemento)cboTipo.SelectedItem; //Cargo la propiedad (Tipo) del (Pokemon) con los datos obetenidos del ComboBox ("La instancia de la propiedad (Tipo) viene dada desde el metodo (listar()) de la clase (ElementoNegocio) ")
                poke.Debilidad = (Elemento)cboDebilidad.SelectedItem; //Cargo la propiedad (Debilidad) del (Pokemon) con los datos obetenidos del ComboBox. ("La instancia de la propiedad (Debelidad) viene dada desde el metodo (listar()) de la clase (ElementoNegocio) ")

                negocio.agregar(poke); //Envio por parametro el objeto (poke) al metodo (agregar) de la clase (PokemonNegocio)
                MessageBox.Show("Agregado exitosamente");
                Close(); //Cierra la ventana
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
                cboDebilidad.DataSource = elementoNegocio.listar(); //Cargo el (CboDebilidad) con los datos traidos mediante el metodo (listar())

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
