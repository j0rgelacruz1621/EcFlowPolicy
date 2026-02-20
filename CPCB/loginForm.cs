using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CPCB
{


    public partial class loginForm : Form
    {
        // 1. Declaramos el timer manualmente
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public loginForm()
        {
            InitializeComponent();

            // 2. Configuramos el evento Tick por código para que sepa qué función ejecutar
            timer1.Tick += new EventHandler(timer1_Tick);
            this.WindowState = FormWindowState.Maximized;
        }


        private void loginForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            { // Crearemos 5 bolitas
                CrearBurbuja();
            }

            // Configurar el Timer (Asegúrate de arrastrar un Timer desde el cuadro de herramientas)
            timer1.Interval = 20; // 20 milisegundos para que se vea fluido
            timer1.Start();
        }
        private void CrearBurbuja()
        {
            var circulo = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            circulo.Size = new Size(30, 30);
            circulo.FillColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            circulo.Location = new Point(rnd.Next(0, guna2Panel1.Width - 30), rnd.Next(0, guna2Panel1.Height - 30));

            // Agregar al panel
            guna2Panel1.Controls.Add(circulo);

            // Guardar en nuestra lista con velocidades aleatorias
            listaBurbujas.Add(new Burbuja
            {
                Control = circulo,
                VelocidadX = rnd.Next(2, 6), // Velocidad entre 2 y 5 píxeles
                VelocidadY = rnd.Next(2, 6)
            });
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var b in listaBurbujas)
            {
                // 1. Mover la bolita
                b.Control.Left += b.VelocidadX;
                b.Control.Top += b.VelocidadY;

                // 2. Rebote en bordes IZQUIERDO o DERECHO
                if (b.Control.Left <= 0 || b.Control.Right >= guna2Panel1.Width)
                {
                    b.VelocidadX *= -1; // Invertir dirección
                    CambiarColor(b.Control);
                }

                // 3. Rebote en bordes SUPERIOR o INFERIOR
                if (b.Control.Top <= 0 || b.Control.Bottom >= guna2Panel1.Height)
                {
                    b.VelocidadY *= -1; // Invertir dirección
                    CambiarColor(b.Control);
                }
            }
        }

        private void CambiarColor(Guna.UI2.WinForms.Guna2CirclePictureBox p)
        {
            // Generar un color aleatorio brillante
            p.FillColor = Color.FromArgb(rnd.Next(100, 256), rnd.Next(100, 256), rnd.Next(100, 256));
        }
        // Lista para guardar varias bolitas
        List<Burbuja> listaBurbujas = new List<Burbuja>();
        Random rnd = new Random();

        private void TxtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario = TxtUser.Text.Trim();
            string password = TxtPassword.Text.Trim();

            // 1. Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, llene todos los campos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Usar la clase de conexión
                using (var conexion = Conexion.LeerConexion())
                {
                    conexion.Open();

                    // 3. Consulta SQL (la tabla es 'users', columnas 'name' y 'password')
                    string query = "SELECT id FROM users WHERE name = @user AND password = @pass";

                    using (var comando = new MySqlCommand(query, conexion))
                    {
                        // Agregamos los parámetros para seguridad
                        comando.Parameters.AddWithValue("@user", usuario);
                        comando.Parameters.AddWithValue("@pass", password);

                        // Ejecutamos y obtenemos el resultado
                        object result = comando.ExecuteScalar();

                        if (result != null)
                        {
                            // LOGIN EXITOSO
                            MessageBox.Show("¡Bienvenido " + usuario + "!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            FormPanelPrincipal principal = new FormPanelPrincipal();
                            principal.Show();
                            this.Hide(); 
                        }
                        else
                        {
                            // DATOS INCORRECTOS
                            MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TxtPassword.Clear();
                            TxtUser.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }


    // Esta clase guardará la información de cada bolita
    public class Burbuja
    {
        public Guna.UI2.WinForms.Guna2CirclePictureBox Control; // El control visual
        public int VelocidadX; // Dirección y velocidad en X
        public int VelocidadY; // Dirección y velocidad en Y
    }

}
