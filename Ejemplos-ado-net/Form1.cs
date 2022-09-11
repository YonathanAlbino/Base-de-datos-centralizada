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
            cboCampo.Items.Add("Número"); //Asigno valores al (cboCampo)
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e) //Cuando se cambia la seleccion de la grilla-dgvPokemons, se cambia la imagen en la pictureBox-pbxPokemon
        {
           if(dgvPokemons.CurrentRow != null)
            {
                Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;  //Se obtiene el objeto enlazado de la grilla-dgvPokemons en la fila actual, y se lo transforma en un objeto de tipo Pokemon y se guarda en (seleccionado)
                cargarImagen(seleccionado.UrlImagen); //Llamado al metodo (cargar imagen)
            } 
        }

        private void cargar() //Metodo de carga para el DataGriv
        {
            PokemonNegocio negocio = new PokemonNegocio(); //Creo un objeto de tipo (negocio) para poder utilizar su metodo de conexion a db
            try //Recibo la excepcion desde la definicion del metodo listar y se toman deciciones desde este (Try-catch)
            {
                listaPokemon = negocio.listar(); //Cargo el atributo-lista con el metodo (listar de la clase PokemonNegocio)
                dgvPokemons.DataSource = listaPokemon; //Le paso el atributo-lista a la DataGriv
                cargarImagen(listaPokemon[0].UrlImagen); //Al cargase la ventana, se selecciona en la pbxPokemon la listaPokemon con la propiedad UrlImagen en el indice[0]
                ocultarColumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas() //Metodo ocultar columnas
        {
            dgvPokemons.Columns["UrlImagen"].Visible = false; //Oculta la columna (UrlImagen) de la dgvPokemons
            dgvPokemons.Columns["Id"].Visible = false;
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

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        } //Evento eliminar fisico

        private void btnEliminarLogico_Click(object sender, EventArgs e) //Evento eliminar logico
        {
            eliminar(true); //Se envia por paremetro un (True)
        }

        private void eliminar(bool logico = false) // Metodo eliminar 
        {
            PokemonNegocio negocio = new PokemonNegocio(); //Creo el objeto negocio para accer al metodo (eliminar)
            Pokemon seleccionado; //Creo variable de tipo (pokemon) para guardar el id a eliminar
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad queres eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); //Validacion Si-No del (MessageBox)
                if (respuesta == DialogResult.Yes) //Si la respuesta del MessageBox es SI, se ejecuta el evento (eliminar)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; //Me guardo los datos del pokemon seleccionado en la grilla para luego obtener el id a eliminar

                    if(logico) //Si (logico) esta en True, elimina de forma logica sino elimina de forma fisica
                        negocio.eliminarLogico(seleccionado.Id); //Envio el Id del pokemon a eliminar de forma logica
                    else 
                    {
                        negocio.eliminar(seleccionado.Id); //Envio el Id del pokemon a eliminar de forma fisica
                    }

                    cargar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); //Muesta caja de dialogo con el posible error
            }
        }

        private bool validarFiltro() //Metodo para validar campos del filtro-Contra DB
        {
            if(cboCampo.SelectedIndex < 0) //Si el (cboCampo) esta vacio entra al if
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar");
                return true; 
            }
            if(cboCriterio.SelectedIndex < 0) //Si el (cboCriterio) esta vacio entra al if
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar");
                return true;
            }
            if(cboCampo.SelectedItem.ToString() == "Número")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text)) //Si la caja (txtFiltroAvanzado) es nula o vacia entra al if
                {
                    MessageBox.Show("Debes cagar el filtro para numéricos....");
                    return true;
                }

                if (!(soloNumeros(txtFiltroAvanzado.Text))) //Si no se ingresaron solo numeros, recibe un falso pero negado, enonces entra al if
                {
                    MessageBox.Show("Por favor ingrese solo numeros para busar por un campo numerico");
                    return true;
                }
            }
            
            return false;
        }
        private bool soloNumeros(string cadena) //Metodo para saber si se ingresaron solamente numestos en el (txtFiltroAvanzado)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter))) //Analiza la cadena caracter por caracter, si encuentra solo numeros no se ejecuta
                {
                    return false;
                }
            }
            return true;
        }
        private void btnBuscar_Click(object sender, EventArgs e) //Evento filtro contra DB, 
        {
            PokemonNegocio negocio = new PokemonNegocio(); //Creo el objeto (negocio) para tener acceso al metodo listar
            try
            {
                if (validarFiltro()) //Si (validarFiltro) se resuelve en verdadero, corta la ejecucion del metodo (BtnBuscar)
                    return; //Un return asi solo corta la ejecucion


                string campo = cboCampo.SelectedItem.ToString(); //Guardo en la variable (campo) el intem seleccionado del (cboCampo)
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvPokemons.DataSource = negocio.filtrar(campo, criterio, filtro); //Llamado al metodo filtrar
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e) //Evento filtro rapido
        {
            List<Pokemon> listaFiltrada; //Creo una lista para guardar los valores filtrados, no es necesario instanciar la lista ya que la (Funcion lamba) devuelve una lista con instancia

            string filtro = txtFiltro.Text;

            if (filtro.Length >= 3) //Si el filtro es >=3, realiza la accion
            {
                listaFiltrada = listaPokemon.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper())); //expresion lamnda: Compara una propiedad dada con lo que tenga adentro del (txtFiltro) y devuelve los objetos que cumplan la condicion
            }
            else
            {
                listaFiltrada = listaPokemon; //Si el (txtFiltro) se encuentra vacio, recargar la lista con todos datos
            }

            dgvPokemons.DataSource = null; //Limpiar la grilla antes de asignarle nuevos valores
            dgvPokemons.DataSource = listaFiltrada; //Carga la grilla con los objetos que cumpla la condicion del filtro
            ocultarColumnas();
        }

        private void lblFiltroAvanzado_Click(object sender, EventArgs e)
        {

        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString(); //Guardo en la variable (seleccionado) el valor actual del (cboCampo)

            if(opcion == "Número")
            {
                cboCriterio.Items.Clear(); //Limpio el (cboCriterio)
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");

            }
            else //Si no es numero es descripcion o nombre
            {
                cboCriterio.Items.Clear(); //Limpio el (cboCriterio)
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }

        }
    }
}
