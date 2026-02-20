using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Usaremos Guna para la estética

namespace CPCB
{
    public partial class FormAdminUsuarios : Form
    {
        private Guna2DataGridView dgvUsuarios;
        private Guna2Panel panelHeader;
        private FlowLayoutPanel flowButtons;

        public FormAdminUsuarios()
        {
            InitializeComponent();
            ConfigurarDiseñoModerno();
        }

        private void FormAdminUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void ConfigurarDiseñoModerno()
        {
            // --- VENTANA ---
            this.Text = "Panel de Gestión de Usuarios";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(242, 245, 250);

            // --- HEADER (Título Estético) ---
            panelHeader = new Guna2Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 70;
            panelHeader.FillColor = Color.White;

            Label lblTitulo = new Label();
            lblTitulo.Text = "ADMINISTRACIÓN DE USUARIOS";
            lblTitulo.Font = new Font("Segoe UI Semibold", 16);
            lblTitulo.ForeColor = Color.FromArgb(45, 52, 54);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(30, 20);
            panelHeader.Controls.Add(lblTitulo);

            // --- GRID (Guna para que se vea premium) ---
            dgvUsuarios = new Guna2DataGridView();
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            dgvUsuarios.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvUsuarios.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvUsuarios.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.Margin = new Padding(20);

            // --- PANEL DE BOTONES ---
            flowButtons = new FlowLayoutPanel();
            flowButtons.Dock = DockStyle.Bottom;
            flowButtons.Height = 90;
            flowButtons.BackColor = Color.White;
            flowButtons.Padding = new Padding(25, 20, 0, 0);

            // Creación de botones con estilo Guna
            flowButtons.Controls.Add(CrearBotonGuna("NUEVO", Color.FromArgb(94, 148, 255), BtnAdd_Click));
            flowButtons.Controls.Add(CrearBotonGuna("ROL", Color.FromArgb(255, 159, 67), BtnRol_Click));
            flowButtons.Controls.Add(CrearBotonGuna("LÍMITE", Color.FromArgb(123, 104, 238), BtnLimite_Click));
            flowButtons.Controls.Add(CrearBotonGuna("RESETEAR", Color.FromArgb(46, 204, 113), BtnReset_Click));
            flowButtons.Controls.Add(CrearBotonGuna("ELIMINAR", Color.FromArgb(231, 76, 60), BtnDel_Click));

            // Agregar al form
            this.Controls.Add(dgvUsuarios);
            this.Controls.Add(panelHeader);
            this.Controls.Add(flowButtons);
        }

        private Guna2Button CrearBotonGuna(string texto, Color color, EventHandler evento)
        {
            Guna2Button btn = new Guna2Button();
            btn.Text = texto;
            btn.FillColor = color;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Size = new Size(140, 45);
            btn.BorderRadius = 10;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(0, 0, 15, 0);
            btn.Click += evento;
            return btn;
        }

        // --- LÓGICA CON CONTROL DE ERRORES (TRY-CATCH) ---

        private void CargarUsuarios()
        {
            try
            {
                using (var con = Conexion.LeerConexion())
                {
                    con.Open();
                    // Usamos 'name' en lugar de 'users' para que coincida con tu imagen inicial
                    string query = "SELECT id AS 'ID', users AS 'Usuario', role AS 'Rol', login_count AS 'Sesiones', login_limit AS 'Límite' FROM users";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvUsuarios.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico al cargar usuarios: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // (Simplificado para el ejemplo, puedes usar un Form secundario como el tuyo)
            try
            {
                using (Form f = new Form())
                {
                    // Configuramos el mini form de ingreso...
                    f.Text = "Registrar Nuevo Usuario";
                    f.Size = new Size(350, 320);
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.FormBorderStyle = FormBorderStyle.FixedDialog;

                    Label l1 = new Label() { Text = "Nombre:", Top = 20, Left = 30, AutoSize = true };
                    TextBox tUser = new TextBox() { Top = 45, Left = 30, Width = 260 };
                    Label l2 = new Label() { Text = "Contraseña:", Top = 80, Left = 30, AutoSize = true };
                    TextBox tPass = new TextBox() { Top = 105, Left = 30, Width = 260 };
                    Label l3 = new Label() { Text = "Rol:", Top = 140, Left = 30, AutoSize = true };
                    ComboBox cRol = new ComboBox() { Top = 165, Left = 30, Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
                    cRol.Items.AddRange(new string[] { "C", "A", "SA" });
                    cRol.SelectedIndex = 0;

                    Button bOk = new Button() { Text = "GUARDAR", Top = 220, Left = 110, Width = 110, Height = 40, DialogResult = DialogResult.OK, BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                    f.Controls.AddRange(new Control[] { l1, tUser, l2, tPass, l3, cRol, bOk });

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (string.IsNullOrWhiteSpace(tUser.Text)) throw new Exception("El nombre no puede estar vacío.");

                        EjecutarSQL("INSERT INTO users (users, password, role, login_count, login_limit) VALUES (@p1, @p2, @p3, 0, 40)",
                                    tUser.Text, tPass.Text, cRol.SelectedItem.ToString());

                        CargarUsuarios();
                        MessageBox.Show("Usuario guardado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0) return;
                string id = dgvUsuarios.SelectedRows[0].Cells["ID"].Value.ToString();
                string nuevoRol = dgvUsuarios.SelectedRows[0].Cells["Rol"].Value.ToString() == "C" ? "A" : "C"; // Ejemplo simple de switch

                EjecutarSQL("UPDATE users SET role = @p1 WHERE id = @p2", nuevoRol, id);
                CargarUsuarios();
            }
            catch (Exception ex) { MessageBox.Show("Error al cambiar rol: " + ex.Message); }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0) return;
                string id = dgvUsuarios.SelectedRows[0].Cells["ID"].Value.ToString();

                EjecutarSQL("UPDATE users SET login_count = 0 WHERE id = @p1", id);
                CargarUsuarios();
                MessageBox.Show("Contador de sesiones reiniciado.");
            }
            catch (Exception ex) { MessageBox.Show("Error al resetear: " + ex.Message); }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0) return;
                string id = dgvUsuarios.SelectedRows[0].Cells["ID"].Value.ToString();

                if (MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EjecutarSQL("DELETE FROM users WHERE id = @p1", id);
                    CargarUsuarios();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al eliminar: " + ex.Message); }
        }

        private void BtnLimite_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0) return;
                string id = dgvUsuarios.SelectedRows[0].Cells["ID"].Value.ToString();
                // Aquí podrías abrir un InputBox o pequeño Form para pedir el número
                EjecutarSQL("UPDATE users SET login_limit = 100 WHERE id = @p1", id); // Ejemplo a 100
                CargarUsuarios();
            }
            catch (Exception ex) { MessageBox.Show("Error al actualizar límite: " + ex.Message); }
        }

        // --- MÉTODO AUXILIAR PARA SQL CON TRY-CATCH ---
        private void EjecutarSQL(string query, string p1, string p2 = null, string p3 = null)
        {
            try
            {
                using (var con = Conexion.LeerConexion())
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@p1", p1);
                        if (p2 != null) cmd.Parameters.AddWithValue("@p2", p2);
                        if (p3 != null) cmd.Parameters.AddWithValue("@p3", p3);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error de Base de Datos: " + ex.Message);
            }
        }
    }
}