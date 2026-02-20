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
        public loginForm()
        {
            InitializeComponent();
        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (btnAcceder.Enabled)
            {
                // Lógica de autenticación aquí
                string username = txtUser.Text;
                string password = txtPassword.Text;
                // Ejemplo simple de validación
                if (username == "Arojas" && password == "Arojas0001")
                {
                    this.Hide();
                    FormPanelPrincipal frmP = new FormPanelPrincipal();
                    frmP.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
        }
    }
}
