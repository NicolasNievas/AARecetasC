using Final_Recetas_Prog2.Dominio;
using Final_Recetas_Prog2.Servicios;
using Final_Recetas_Prog2.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Recetas_Prog2
{
    public partial class FrmAltaRecetas : Form
    {
        private Receta nuevo;
        private IServicio service;
        public FrmAltaRecetas()
        {
            InitializeComponent();
            nuevo = new Receta();
            service = new ServiceFactoryImp().crearServicio();
        }

        private void FrmAltaRecetas_Load(object sender, EventArgs e)
        {
            ProximaReceta();
            CargarCombo();
        }

        private void CargarCombo()
        {
            cboIngrediente.DataSource = service.ObtenerIngredientes();
            cboIngrediente.ValueMember = "IngredienteID";
            cboIngrediente.DisplayMember = "Nombre";
            cboIngrediente.SelectedIndex = -1;
        }

        private void ProximaReceta()
        {
            int next = service.ProximaReceta();
            if(next > 0)
            {
                lblNro.Text += next.ToString();
            }
            else
            {
                MessageBox.Show("Error de datos, no se pudo obtener la proxima receta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Limpiar()
        {
            txtNombre.Text = "";
            txtCheff.Text = "";
            cboIngrediente.SelectedIndex = -1;
            cboTipo.SelectedIndex = -1;
            nudCantidad.Value = 1;
            dgvDetalles.Rows.Clear();
        }
        private bool ValidarAgregar()
        {
            if(cboIngrediente.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un ingrediente correctamente","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            if(nudCantidad.Value == 0)
            {
                MessageBox.Show("Seleccione una cantidad valida", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            foreach(DataGridViewRow row in dgvDetalles.Rows)
            {
                if(row.Cells["CodIngrediente"].Value.ToString().Equals(cboIngrediente.Text))
                {
                    MessageBox.Show("Ingrediente: " + cboIngrediente.Text + " ya se encuentra en el detalle", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(ValidarAgregar())
            {
                DetalleReceta det = new DetalleReceta();
                det.cantidad = (int)nudCantidad.Value;
                det.ingrediente = (Ingrediente)cboIngrediente.SelectedItem;
                nuevo.AgregaDetalle(det);
                dgvDetalles.Rows.Add(new object[] { det.ingrediente.IngredienteID, det.ingrediente.Nombre, det.cantidad });
            }
            CalcularTotal();
        }

        private bool ValidarAceptar()
        {
            if(txtNombre.Text == "")
            {
                MessageBox.Show("Escriba el nombre de la Receta correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(txtCheff.Text == "")
            {
                MessageBox.Show("Escriba el nombre del Chef correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(cboTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de receta correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(dgvDetalles.Rows.Count < 3)
            {
                MessageBox.Show("Ha olvidado Ingredientes?", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(ValidarAceptar())
            {
                nuevo.RecetaNro = service.ProximaReceta();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Cheff = txtCheff.Text;
                nuevo.TipoReceta = Convert.ToInt32(cboIngrediente.SelectedIndex);
                if(service.ConfirmarReceta(nuevo))
                {
                    MessageBox.Show("La receta se agrego correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("La receta no se pudo agregar correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CalcularTotal();
            }
        }

        private void CalcularTotal()
        {
            lblTotalIngredientes.Text = "total Ingredientes: " + dgvDetalles.Rows.Count;
        }

        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Seguro que desea cancelar y salir?","AVISO",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Close();
            }
            //DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    this.Dispose();
            //}
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex == 3)
            {
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
            }
        }
        
        
        //                  CALCULAR SUBTOTAL
        /*Calcular SubTotal
        public int Subtotal()
        {
            int subtotal = 0;
            foreach (DataGridViewRow dr in dgvDetalles.Rows)
            {
                subtotal = Convert.ToInt32(dr.Cells["precio"].Value) * Convert.ToInt32(dr.Cells["cantidad"].Value);
            }
            return subtotal;
        }
        */
    }
}
