using CadParcial2Mevy;
using ClnParcial2Mevy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpParcial2Mervy
{
    public partial class FrmSerie : Form
    {
        bool esNuevo = false;
        public FrmSerie()
        {
            InitializeComponent();
            listar();
        }


        private void listar()
        {
            var series = SerieCln.Listar();
            dgvSerie.DataSource = series;
        }



        private void FrmSerie_Load(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvSerie.SelectedRows.Count > 0)
            {
                int id = (int)dgvSerie.SelectedRows[0].Cells["id"].Value;
                string Titulo = "titulo";
                SerieCln.Eliminar(id, Titulo);
                listar();
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvSerie.SelectedRows.Count > 0)
            {
                esNuevo = false;
                HabilitarCampos(true);
                CargarCampos();
            }
            else
            {
                MessageBox.Show("Seleccione una fila para editar.");
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(860, 481);
            txtTitulo.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var serie = new Serie
                {
                    titulo = txtTitulo.Text.Trim(),
                    sinopsis = txtSinopsis.Text.Trim(),
                    director = txtDirector.Text.Trim(),
                    episodios = int.Parse(nudEpisodios.Value.ToString()),
                    fecha_estreno = DateTime.Parse(dtpFechaEstreno.Value.ToString()), 
                    estado = (short)(int)nudestado.Value,
                    idioma_principal = cbxIdiomaPrincipal.Text.Trim()
                };

                if (esNuevo)
                {
                    serie.fecha_estreno = DateTime.Now; 
                    SerieCln.Insertar(serie);
                }
                else
                {
                    int index = dgvSerie.CurrentCell.RowIndex;
                    serie.id = Convert.ToInt32(dgvSerie.Rows[index].Cells["id"].Value);
                    SerieCln.Actualizar(serie);
                }

                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Serie guardada correctamente", "::: Serie - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool validar()
        {
            bool esValido = true;

          
            erpTitulo.SetError(txtTitulo, "");
            erpSinopsis.SetError(txtSinopsis, "");
            erpDirector.SetError(txtDirector, "");
            erpEpisodios.SetError(nudEpisodios, "");
            erpFechaEstreno.SetError(dtpFechaEstreno, "");
            erpEstado.SetError(nudestado, "");
            erpIdiomaPrincipal.SetError(cbxIdiomaPrincipal, "");

          
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                esValido = false;
                erpTitulo.SetError(txtTitulo, "El campo Título es obligatorio");
            }

            
            if (string.IsNullOrEmpty(txtSinopsis.Text))
            {
                esValido = false;
                erpSinopsis.SetError(txtSinopsis, "El campo Sinopsis es obligatorio");
            }

           
            if (string.IsNullOrEmpty(txtDirector.Text))
            {
                esValido = false;
                erpDirector.SetError(txtDirector, "El campo Director es obligatorio");
            }

            
            if (nudEpisodios.Value <= 0)
            {
                esValido = false;
                erpEpisodios.SetError(nudEpisodios, "El campo Episodios debe ser mayor que cero");
            }

            
            if (dtpFechaEstreno.Value.Date > DateTime.Now.Date)
            {
                esValido = false;
                erpFechaEstreno.SetError(dtpFechaEstreno, "La Fecha de Estreno no puede ser futura");
            }

           
            if (nudestado.Value < 0)
            {
                esValido = false;
                erpEstado.SetError(nudestado, "El Estado no puede ser negativo");
            }

            if (string.IsNullOrEmpty(cbxIdiomaPrincipal.Text))
            {
                esValido = false;
                erpIdiomaPrincipal.SetError(cbxIdiomaPrincipal, "El campo idioma principal es obligatorio");
            }



            return esValido;
        }

        private void CargarCampos()
        {
            txtTitulo.Text = dgvSerie.SelectedRows[0].Cells["titulo"].Value.ToString();
            txtSinopsis.Text = dgvSerie.SelectedRows[0].Cells["sinopsis"].Value.ToString();
            txtDirector.Text = dgvSerie.SelectedRows[0].Cells["director"].Value.ToString();
            nudEpisodios.Text = dgvSerie.SelectedRows[0].Cells["episodios"].Value.ToString();
            dtpFechaEstreno.Text = dgvSerie.SelectedRows[0].Cells["fecha_estreno"].Value.ToString();
            nudestado.Text = dgvSerie.SelectedRows[0].Cells["estado"].Value.ToString();
            cbxIdiomaPrincipal.Text = dgvSerie.SelectedRows[0].Cells["idioma_principal"].Value.ToString();
        }

        private void HabilitarCampos(bool habilitar)
        {
            txtTitulo.Enabled = habilitar;
            txtSinopsis.Enabled = habilitar;
            txtDirector.Enabled = habilitar;
            nudEpisodios.Enabled = habilitar;
           dtpFechaEstreno.Enabled = habilitar;
            nudestado.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
            cbxIdiomaPrincipal.Enabled = habilitar;
        }


        private void LimpiarCampos()
        {
            txtTitulo.Text = "";
            txtSinopsis.Text = "";
            txtDirector.Text = "";
            nudEpisodios.Text = "";
            nudestado.Text = "";
            cbxIdiomaPrincipal.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string parametro = txtParametro.Text;
            var series = SerieCln.ListarPa(parametro);
            dgvSerie.DataSource = series;
        }

    }
}
