using iTextSharp.text;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Threading;
using System.Windows.Forms;

using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;
using Image = System.Drawing.Image;
// hola
namespace CPCB 
{
    public partial class FormPanelPrincipal : Form
    {
        // Lista de planes
        List<PlanSeguro> listaPlanes = new List<PlanSeguro>();
        bool imprimirSinLogos = false;

        // Objetos de impresión
        PrintDocument documento = new PrintDocument();
        PrintPreviewDialog vistaPrevia = new PrintPreviewDialog();

        // Tasa de cambio (Ejemplo)
        decimal tasaCambio = 1;

        public FormPanelPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Configurar Impresión
            documento.PrintPage += new PrintPageEventHandler(ImprimirDiseño);

            // Asegurar que el Panel 28 se redibuje si cambia el tamaño
            if (panel28 != null)
            {
                panel28.Paint += panel28_Paint; // Vincular evento manualmente por seguridad
                panel28.Resize += (s, e) => panel28.Invalidate(); // Redibujar al cambiar tamaño
            }
        }
        // --- MÉTODO PARA CARGAR DATOS DE PRUEBA (TEMPORAL) ---
        private void FormPanelPrincipal_Load(object sender, EventArgs e)
        {
            CargarPlanes();
            txtTasaCambio.Text = tasaCambio.ToString();

            // --- AGREGAR ESTO PARA QUE APAREZCA EL TEXTO AL ABRIR ---

            txtGerente.Text = "ANDRES ROJAS";
            txtOficinaDetallada.Text = "OFICINA EJIDO: AV. BOLIVAR, SECTOR PIEDRAS BLANCAS, C.C. PIEDRAS BLANCAS, 1er. PISO, LOCAL 6, EJIDO ESTADO MERIDA.";
        }

        // --- MÉTODO PARA CARGAR DATOS FICTICIOS AUTOMÁTICAMENTE ---
        private void CargarPlanes()
        {
            listaPlanes.Clear();

            // --- MOTOS ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "MOTOS BAJA CILINDRADA 5$",
                PrecioUSD = 5,
                CobDanosCosas = 80,
                CobDanosPersonas = 80,
                ExcesoLimite = 80,
                CobInvalidez = 80,
                CobGastosMedicos = 80,
                CobMuerte = 80,
                IndemnizacionSemanal = 80,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 80
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "MOTOS BAJA CILINDRADA 7$",
                PrecioUSD = 7,
                CobDanosCosas = 100,
                CobDanosPersonas = 100,
                ExcesoLimite = 100,
                CobInvalidez = 100,
                CobGastosMedicos = 100,
                CobMuerte = 100,
                IndemnizacionSemanal = 100,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 100
            });

            // --- VEHICULOS PARTICULAR ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "VEHICULOS PARTICULAR 10$",
                PrecioUSD = 10,
                CobDanosCosas = 200,
                CobDanosPersonas = 200,
                ExcesoLimite = 200,
                CobInvalidez = 200,
                CobGastosMedicos = 200,
                CobMuerte = 200,
                IndemnizacionSemanal = 200,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 200
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "VEHICULOS PARTICULAR 20$",
                PrecioUSD = 20,
                CobDanosCosas = 250,
                CobDanosPersonas = 250,
                CobMuerte = 250,
                CobInvalidez = 250,
                CobGastosMedicos = 250,
                ExcesoLimite = 250,
                IndemnizacionSemanal = 250,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 250
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "VEHICULOS PARTICULAR 30$",
                PrecioUSD = 30,
                CobDanosCosas = 300,
                CobDanosPersonas = 300,
                CobMuerte = 300,
                CobInvalidez = 300,
                ExcesoLimite = 300,
                IndemnizacionSemanal = 300,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 300
            });

            // --- VEHICULOS TRANSPORTE PUBLICO ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "VEHICULOS TRANSPORTE PUBLICO 12$",
                PrecioUSD = 12,
                CobDanosCosas = 200,
                CobDanosPersonas = 200,
                CobMuerte = 200,
                CobInvalidez = 200,
                CobGastosMedicos = 200,
                ExcesoLimite = 200,
                IndemnizacionSemanal = 200,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 200
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "VEHICULOS TRANSPORTE PUBLICO 24$",
                PrecioUSD = 24,
                CobDanosCosas = 250,
                CobDanosPersonas = 250,
                CobMuerte = 250,
                CobInvalidez = 250,
                CobGastosMedicos = 250,
                ExcesoLimite = 250,
                IndemnizacionSemanal = 250,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 250
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "VEHICULOS TRANSPORTE PUBLICO 36$",
                PrecioUSD = 36,
                CobDanosCosas = 300,
                CobDanosPersonas = 300,
                CobMuerte = 300,
                CobInvalidez = 300,
                CobGastosMedicos = 300,
                ExcesoLimite = 300,
                IndemnizacionSemanal = 300,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 300
            });

            // --- CAMIONETA Y RUSTICOS (3, 5 Y 7 PUESTOS) ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMIONETA Y RUSTICOS (3-7 PUESTOS) 12$",
                PrecioUSD = 12,
                CobDanosCosas = 200,
                CobDanosPersonas = 200,
                CobMuerte = 200,
                CobInvalidez = 200,
                CobGastosMedicos = 200,
                ExcesoLimite = 200,
                IndemnizacionSemanal = 200,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 200
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMIONETA Y RUSTICOS (3-7 PUESTOS) 24$",
                PrecioUSD = 24,
                CobDanosCosas = 250,
                CobDanosPersonas = 250,
                CobMuerte = 250,
                CobInvalidez = 250,
                CobGastosMedicos = 250,
                ExcesoLimite = 250,
                IndemnizacionSemanal = 250,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 250
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMIONETA Y RUSTICOS (3-7 PUESTOS) 40$",
                PrecioUSD = 40,
                CobDanosCosas = 300,
                CobDanosPersonas = 300,
                CobMuerte = 300,
                CobInvalidez = 300,
                CobGastosMedicos = 300,
                ExcesoLimite = 300,
                IndemnizacionSemanal = 300,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 300
            });

            // --- AUTOBUS TRANSPORTE PUBLICO (HASTA 20 PUESTOS) ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "AUTOBUS (HASTA 20 PUESTOS) 18$",
                PrecioUSD = 18,
                CobDanosCosas = 250,
                CobDanosPersonas = 250,
                CobMuerte = 250,
                CobInvalidez = 250,
                CobGastosMedicos = 250,
                ExcesoLimite = 250,
                IndemnizacionSemanal = 250,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 250
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "AUTOBUS (HASTA 20 PUESTOS) 36$",
                PrecioUSD = 36,
                CobDanosCosas = 300,
                CobDanosPersonas = 300,
                CobMuerte = 300,
                CobInvalidez = 300,
                CobGastosMedicos = 300,
                ExcesoLimite = 300,
                IndemnizacionSemanal = 300,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 300
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "AUTOBUS (HASTA 20 PUESTOS) 60$",
                PrecioUSD = 60,
                CobDanosCosas = 350,
                CobDanosPersonas = 350,
                CobMuerte = 350,
                CobInvalidez = 350,
                CobGastosMedicos = 350,
                ExcesoLimite = 350,
                IndemnizacionSemanal = 350,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 350
            });

            // --- AUTOBUS TRANSPORTE PUBLICO (HASTA 35 PUESTOS) ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "AUTOBUS (HASTA 35 PUESTOS) 30$",
                PrecioUSD = 30,
                CobDanosCosas = 350,
                CobDanosPersonas = 350,
                CobMuerte = 350,
                CobInvalidez = 350,
                CobGastosMedicos = 350,
                ExcesoLimite = 350,
                IndemnizacionSemanal = 350,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 350
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "AUTOBUS (HASTA 35 PUESTOS) 45$",
                PrecioUSD = 45,
                CobDanosCosas = 400,
                CobDanosPersonas = 400,
                CobMuerte = 400,
                CobInvalidez = 400,
                CobGastosMedicos = 400,
                ExcesoLimite = 400,
                IndemnizacionSemanal = 400,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 400
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "AUTOBUS (HASTA 35 PUESTOS) 65$",
                PrecioUSD = 65,
                CobDanosCosas = 450,
                CobDanosPersonas = 450,
                CobMuerte = 450,
                CobInvalidez = 450,
                CobGastosMedicos = 450,
                ExcesoLimite = 450,
                IndemnizacionSemanal = 450,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 450
            });

            // --- CAMION (HASTA 5 TONELADAS) ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMION (HASTA 5 TON) 20$",
                PrecioUSD = 20,
                CobDanosCosas = 250,
                CobDanosPersonas = 250,
                CobMuerte = 250,
                CobInvalidez = 250,
                CobGastosMedicos = 250,
                ExcesoLimite = 250,
                IndemnizacionSemanal = 250,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 250
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMION (HASTA 5 TON) 40$",
                PrecioUSD = 40,
                CobDanosCosas = 300,
                CobDanosPersonas = 300,
                CobMuerte = 300,
                CobInvalidez = 300,
                CobGastosMedicos = 300,
                ExcesoLimite = 300,
                IndemnizacionSemanal = 300,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 300
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMION (HASTA 5 TON) 60$",
                PrecioUSD = 60,
                CobDanosCosas = 350,
                CobDanosPersonas = 350,
                CobMuerte = 350,
                CobInvalidez = 350,
                CobGastosMedicos = 350,
                ExcesoLimite = 350,
                IndemnizacionSemanal = 350,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 350
            });

            // --- CAMION (DESDE 5 HASTA 15 TONELADAS) ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMION (5-15 TON) 25$",
                PrecioUSD = 25,
                CobDanosCosas = 350,
                CobDanosPersonas = 350,
                CobMuerte = 350,
                CobInvalidez = 350,
                CobGastosMedicos = 350,
                ExcesoLimite = 350,
                IndemnizacionSemanal = 350,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 350
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMION (5-15 TON) 45$",
                PrecioUSD = 45,
                CobDanosCosas = 400,
                CobDanosPersonas = 400,
                CobMuerte = 400,
                CobInvalidez = 400,
                CobGastosMedicos = 400,
                ExcesoLimite = 400,
                IndemnizacionSemanal = 400,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 400
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CAMION (5-15 TON) 65$",
                PrecioUSD = 65,
                CobDanosCosas = 450,
                CobDanosPersonas = 450,
                CobMuerte = 450,
                CobInvalidez = 450,
                CobGastosMedicos = 450,
                ExcesoLimite = 450,
                IndemnizacionSemanal = 450,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 450
            });

            // --- CHUTO/BATEA ---
            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CHUTO/BATEA 40$",
                PrecioUSD = 40,
                CobDanosCosas = 350,
                CobDanosPersonas = 350,
                CobMuerte = 350,
                CobInvalidez = 350,
                CobGastosMedicos = 350,
                ExcesoLimite = 350,
                IndemnizacionSemanal = 350,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 350
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CHUTO/BATEA 60$",
                PrecioUSD = 60,
                CobDanosCosas = 400,
                CobDanosPersonas = 400,
                CobMuerte = 400,
                CobInvalidez = 400,
                CobGastosMedicos = 400,
                ExcesoLimite = 400,
                IndemnizacionSemanal = 400,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 400
            });

            listaPlanes.Add(new PlanSeguro
            {
                Nombre = "CHUTO/BATEA 80$",
                PrecioUSD = 80,
                CobDanosCosas = 450,
                CobDanosPersonas = 450,
                CobMuerte = 450,
                CobInvalidez = 450,
                CobGastosMedicos = 450,
                ExcesoLimite = 450,
                IndemnizacionSemanal = 450,
                GruaDanosMecanicos = 0,
                GruaSiniestro = 0,
                AsistenciaLegal = 450
            });

            // Asignar lista al ComboBox
            comboBox1.DataSource = null; // Reiniciar
            comboBox1.DataSource = listaPlanes;
        }
        // --- 2. GENERAR QR CON DATOS COMPLETOS ---
        private void GenerarQR()
        {
            // Evitar errores si aun no se ha cargado el formulario completo
            if (txtPlaca == null || comboBox1.SelectedItem == null) return;

            try
            {
                // Construimos el texto con los saltos de línea (\n)
                // Usamos el formato solicitado:
                string contenidoQR = $"BENEFICIARIO: {txtBeneficiario.Text}\n" +
                                     $"CI/RIF: {txtRifCedulaBeneficiario.Text}\n" +
                                     $"VENCIMIENTO: {dtpVencimiento.Value.ToString("dd/MM/yyyy")}\n" +
                                     $"PLACA: {txtPlaca.Text}\n" +
                                     $"MARCA: {txtMarca.Text}\n" +
                                     $"MODELO: {txtModelo.Text}\n" +
                                     $"AÑO: {txtAño.Text}\n" +
                                     $"PUESTOS: {txtPuestos.Text}\n" +
                                     $"SERIAL DEL VEHICULO: {txtSerialCarroceria.Text}";

                // Generación del Código
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(contenidoQR, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(5);

                if (pbQR != null)
                {
                    pbQR.Image = qrCodeImage;
                    pbQR.SizeMode = PictureBoxSizeMode.Zoom;
                }

                // Refrescar la vista previa en pantalla (Panel 28)
                if (panel28 != null) panel28.Invalidate();
            }
            catch (Exception)
            {
                // Manejo de error silencioso o MessageBox.Show(ex.Message);
            }
        }
        // --- 3. DIBUJAR RESUMEN EN EL PANEL 28 (VISUALIZACIÓN EN PANTALLA) ---
        private void panel28_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Obtener el plan seleccionado
            PlanSeguro plan = comboBox1.SelectedItem as PlanSeguro;
            if (plan == null) return;

            // Configuración de estilo
            int x = 10;
            int y = 20;
            int ancho = panel28.Width - 20;

            // Fuentes y Brochas
            Font fTitulo = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fSub = new Font("Segoe UI", 9, FontStyle.Bold);
            Brush bAzul = Brushes.DarkBlue;
            Brush bTexto = Brushes.Black;
            Brush bRojo = Brushes.Red;

            // 1. Título
            g.DrawString("RESUMEN DE COBERTURAS: " + plan.Nombre, fTitulo, bAzul, x, y);
            y += 40;

            // 2. Encabezados
            g.FillRectangle(Brushes.LightGray, x, y, ancho, 25);
            int colConcepto = x + 5;
            int colDolares = x + 300;
            int colBolivares = x + 450;

            g.DrawString("CONCEPTO", fSub, bTexto, colConcepto, y + 5);
            g.DrawString("SUMA ($)", fSub, bTexto, colDolares, y + 5);
            g.DrawString("SUMA (Bs.)", fSub, bTexto, colBolivares, y + 5);
            y += 30;

            // 3. Filas de Datos (AHORA ESTÁN LAS 6)

            // --- Coberturas a Terceros (Las que faltaban) ---
            DibujarFilaPantalla(g, "DAÑOS A COSAS:", plan.CobDanosCosas, x, ref y);
            DibujarFilaPantalla(g, "DAÑOS A PERSONAS:", plan.CobDanosPersonas, x, ref y);

            // --- Coberturas a Ocupantes ---
            DibujarFilaPantalla(g, "INVALIDEZ PERMANENTE:", plan.CobInvalidez, x, ref y);
            DibujarFilaPantalla(g, "GASTOS MEDICOS:", plan.CobGastosMedicos, x, ref y);
            DibujarFilaPantalla(g, "MUERTE ACCIDENTAL:", plan.CobMuerte, x, ref y);
            DibujarFilaPantalla(g, "ASISTENCIA LEGAL Y DEFENSA PENAL:", plan.CobDefensaPenal, x, ref y);

            y += 10;
            g.DrawLine(new Pen(Color.Black, 2), x, y, x + ancho, y);
            y += 15;

            // 4. Totales

            // Total Dólares
            g.DrawString("TOTAL PRECIO PLAN ($):", fTitulo, bAzul, x, y);
            g.DrawString($"$ {plan.PrecioUSD:N2}", fTitulo, bAzul, x + 300, y);
            y += 30;

            // Total Bolívares
            decimal totalBs = plan.PrecioUSD * tasaCambio;
            g.DrawString("TOTAL A PAGAR (Bs.):", fTitulo, bRojo, x, y);
            g.DrawString($"Bs. {totalBs:N2}", fTitulo, bRojo, x + 300, y);
        }        // Helper para dibujar filas en el Panel 28
        private void DibujarFilaPantalla(Graphics g, string titulo, decimal valorUSD, int x, ref int y)
        {
            decimal valorBs = valorUSD * tasaCambio; // Usar la variable global actualizada

            // El formato :N2 fuerza 2 decimales. Ejemplo: 150.00
            string txtValUSD = $"$ {valorUSD:N2}";
            string txtValBs = $"Bs. {valorBs:N2}";

            Font fItem = new Font("Segoe UI", 9, FontStyle.Regular);
            Font fNumero = new Font("Segoe UI", 9, FontStyle.Bold);

            g.DrawString(titulo, fItem, Brushes.Black, x + 5, y);
            g.DrawString(txtValUSD, fNumero, Brushes.DimGray, x + 300, y);
            g.DrawString(txtValBs, fNumero, Brushes.Black, x + 450, y);

            g.DrawLine(Pens.LightGray, x, y + 18, x + 600, y + 18);
            y += 22;
        }
        // --- 4. EVENTOS DE CONTROLES ---
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarQR(); // Al cambiar plan, regenera QR y repinta el Panel 28
        }

        // Vincula estos eventos a sus textbox correspondientes
        private void txtVendedor_TextChanged(object sender, EventArgs e) { }
        private void txtOficina_TextChanged(object sender, EventArgs e) { }
        private void dtpEmision_ValueChanged(object sender, EventArgs e) { GenerarQR(); }
        private void dtpVencimiento_ValueChanged(object sender, EventArgs e) { GenerarQR(); }
        private void txtBeneficiario_TextChanged(object sender, EventArgs e) { GenerarQR(); }
        private void txtContratante_TextChanged(object sender, EventArgs e) { }
        private void txtRifCedulaBeneficiario_TextChanged(object sender, EventArgs e) { GenerarQR(); }
        private void txtRifCedulaContratante_TextChanged(object sender, EventArgs e) { }
        private void txtPlaca_TextChanged(object sender, EventArgs e) { GenerarQR(); }
        private void txtMarca_TextChanged(object sender, EventArgs e) { GenerarQR(); }
        private void txtModelo_TextChanged(object sender, EventArgs e) { GenerarQR(); }
        private void txtColor_TextChanged(object sender, EventArgs e) { GenerarQR(); }
        private void txtSerialCarroceria_TextChanged(object sender, EventArgs e) { GenerarQR(); }

        // Otros eventos vacíos requeridos por el diseñador
        private void label5_Click(object sender, EventArgs e) { }
        private void txtDireccionContratante_TextChanged(object sender, EventArgs e) { }
        private void txtDireccionBeneficiario_TextChanged(object sender, EventArgs e) { }
        private void txtTelefonoCliente_TextChanged(object sender, EventArgs e) { }
        private void txtGerente_TextChanged(object sender, EventArgs e) { }
        private void txtTelefonoGerente_TextChanged(object sender, EventArgs e) { }
        private void txtClase_TextChanged(object sender, EventArgs e) { }
        private void txtAño_TextChanged(object sender, EventArgs e) { }
        private void txtSerialMotor_TextChanged(object sender, EventArgs e) { }
        private void txtPuestos_TextChanged(object sender, EventArgs e) { }
        private void txtUso_TextChanged(object sender, EventArgs e) { }
        private void txtTipo_TextChanged(object sender, EventArgs e) { }


        // --- 5. IMPRESIÓN DEL PDF (Lógica anterior mantenida) ---
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirSinLogos = false; // Queremos logos
            EjecutarImpresion();      // Llamamos a una función compartida para no repetir código
        }
        private void ImprimirDiseño(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            // --- DATOS Y FUENTES REDUCIDAS ---
            PlanSeguro plan = comboBox1.SelectedItem as PlanSeguro;
            if (plan == null && listaPlanes.Count > 0) plan = listaPlanes[0];
            decimal tasa = this.tasaCambio;

            // FUENTES MÁS PEQUEÑAS PERO EN NEGRITA
            Font fTituloGrande = new Font("Arial", 14, FontStyle.Bold);     // Reducido de 20
            Font fTituloMedio = new Font("Arial", 11, FontStyle.Bold);      // Reducido de 14
            Font fNormal = new Font("Arial", 9, FontStyle.Regular);         // Reducido de 11
            Font fNegrita = new Font("Arial", 9, FontStyle.Bold);           // Reducido de 11
            Font fPeque = new Font("Arial", 8, FontStyle.Regular);          // Reducido de 9
            Font fPequeNegrita = new Font("Arial", 8, FontStyle.Bold);      // Reducido de 9

            Pen pFino = new Pen(Color.Black, 0.5f);
            Pen pMedio = new Pen(Color.Black, 1);
            Brush bGris = Brushes.LightGray;

            // Coordenadas base - más compactas
            int x = 25;
            int y = 20;
            int w = 760;

            // ==========================================
            // 1. ENCABEZADO (LOGO Y CAJA RCV) - MÁS COMPACTO
            // ==========================================

            // Logo más pequeño
            if (pbQR.Image != null)
            {
                // Lo dibujamos pequeño (50x50) para que quepa en el encabezado
                g.DrawImage(pbQR.Image, x, y-15, 80, 80);
            }

            // B) LOGO (IMAGEN 2) CENTRADO - Solo si imprimirSinLogos es falso
            if (Properties.Resources.imagen2 != null)
            {
                int anchoLogo = 150;
                int altoLogo = 50;
                // Fórmula para centrar: X_Inicial + (Ancho_Total - Ancho_Objeto) / 2
                int xCentro1 = x + (w - anchoLogo) / 2;

                g.DrawImage(Properties.Resources.imagen2, xCentro1, y, anchoLogo, altoLogo);
            }

            // C) CAJA RCV (DERECHA) - Esto se queda igual
            Pen oMedio1 = new Pen(Color.Black, 1);
            int xRcv = w - 184; // Ajustado a tu coordenada original aprox
            g.DrawRectangle(pMedio, xRcv, y, 210, 50);
            g.DrawString("RCV Nº", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, xRcv + 70, y + 5);
            g.DrawLine(pMedio, xRcv, y + 20, xRcv + 210, y + 20);

            string numeroRCV = string.IsNullOrEmpty(txtRCV.Text) ? "0000 - 00000" : txtRCV.Text;
            StringFormat formatoCentro = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString(numeroRCV, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, new RectangleF(xRcv, y + 25, 210, 25), formatoCentro);

            y += 60;

            // ==========================================
            // 2. TÍTULO CENTRAL
            // ==========================================
            CentrarTexto(g, "CONTRATO DE GARANTIAS ADMINISTRADAS", new Font("Arial", 12, FontStyle.Bold), w, y);
            y += 20;

            // ==========================================
            // 3. DATOS EMISIÓN - MÁS COMPACTO
            // ==========================================
            int hCaja = 35;
            int wCol = w / 4;

            g.FillRectangle(bGris, x, y, w, 15);
            g.DrawRectangle(pFino, x, y, w, hCaja);
            g.DrawLine(pFino, x + wCol, y, x + wCol, y + hCaja);
            g.DrawLine(pFino, x + (wCol * 2), y, x + (wCol * 2), y + hCaja);
            g.DrawLine(pFino, x + (wCol * 3), y, x + (wCol * 3), y + hCaja);

            DrawCellTextCompact(g, "VENDEDOR", txtVendedor.Text, x, y, wCol);
            DrawCellTextCompact(g, "OFICINA", txtOficina.Text, x + wCol, y, wCol);
            DrawCellTextCompact(g, "EMISION", dtpEmision.Value.ToString("dd/MM/yyyy"), x + (wCol * 2), y, wCol);
            DrawCellTextCompact(g, "VENCIMIENTO", dtpVencimiento.Value.ToString("dd/MM/yyyy"), x + (wCol * 3), y, wCol);

            y += 40;

            // ==========================================
            // 4. PERSONAS - REORGANIZADO EN FILAS SEPARADAS
            // ==========================================

            // Aumentar espacio vertical entre secciones
            y += 5;

            // Fila 1: CONTRATANTE completo
            g.DrawString("CONTRATANTE:", fNegrita, Brushes.Black, x, y);
            g.DrawString(txtContratante.Text.ToUpper(), fNormal, Brushes.Black, x + 100, y);
            y += 16;

            // Fila 2: RIF/CÉDULA Contratante (alineado a la derecha)
            g.DrawString("RIF/CEDULA:", fNegrita, Brushes.Black, x, y);
            // Alinear a la derecha para aprovechar espacio
            StringFormat formatoDerecha = new StringFormat();
            formatoDerecha.Alignment = StringAlignment.Far;
            RectangleF rectRif = new RectangleF(x, y, w - 598, 16);
            g.DrawString(txtRifCedulaContratante.Text, fNormal, Brushes.Black, rectRif, formatoDerecha);
            y += 16;

            // Fila 3: DIRECCIÓN Contratante (usar espacio completo)
            g.DrawString("DIRECCION:", fNegrita, Brushes.Black, x, y);
            string dirContratante = txtDireccionContratante.Text.Length > 100 ?
                                   txtDireccionContratante.Text.Substring(0, 100) + "..." :
                                   txtDireccionContratante.Text;
            g.DrawString(dirContratante, fNormal, Brushes.Black, x + 87, y);
            y += 16;

            // Espacio entre contratante y beneficiario
            y += 4;

            // Fila 4: BENEFICIARIO
            g.DrawString("BENEFICIARIO:", fNegrita, Brushes.Black, x, y);
            g.DrawString(txtBeneficiario.Text.ToUpper(), fNormal, Brushes.Black, x + 95, y);
            y += 16;

            // Fila 5: RIF/CÉDULA Beneficiario
            g.DrawString("RIF/CEDULA:", fNegrita, Brushes.Black, x, y);
            RectangleF rectRifBenef = new RectangleF(x, y, w - 598, 16);
            g.DrawString(txtRifCedulaBeneficiario.Text, fNormal, Brushes.Black, rectRifBenef, formatoDerecha);
            y += 16;

            // Fila 6: DIRECCIÓN Beneficiario
            g.DrawString("DIRECCION:", fNegrita, Brushes.Black, x, y);
            string dirBeneficiario = txtDireccionBeneficiario.Text.Length > 100 ?
                                    txtDireccionBeneficiario.Text.Substring(0, 100) + "..." :
                                    txtDireccionBeneficiario.Text;
            g.DrawString(dirBeneficiario, fNormal, Brushes.Black, x + 87, y);
            y += 16;

            // Espacio entre beneficiario y gerente
            y += 4;

            // Fila 7: GERENTE (usar mitad izquierda)
            g.DrawString("GERENTE:", fNegrita, Brushes.Black, x, y);
            g.DrawString(txtGerente.Text, fNormal, Brushes.Black, x + 87, y);

            // Fila 7: TELÉFONOS CLIENTE (usar mitad derecha)
            g.DrawString("TELEFONOS:", fNegrita, Brushes.Black, x + 350, y);
            g.DrawString(txtTelefonoCliente.Text, fNormal, Brushes.Black, x + 435, y);
            y += 16;

            // Fila 8: TELÉFONOS OFICINA (centrado o completo)
            g.DrawString("TELEFONOS OFICINA:", fNegrita, Brushes.Black, x, y);
            g.DrawString("0414-961.84.41 / 0412-646.09.98", fNormal, Brushes.Black, x + 140, y);
            y += 20;

            // ==========================================
            // 5. VEHICULO - MÁS COMPACTO
            // ==========================================
            g.FillRectangle(bGris, x, y+2, w, 18);
            g.DrawRectangle(pMedio, x, y+2, w, 18);
            CentrarTexto(g, "DESCRIPCION DEL VEHICULO", new Font("Arial", 10, FontStyle.Bold), w, y + 3);
            y += 18;

            int alturaCajaVehiculo = 80;
            g.DrawRectangle(pMedio, x, y, w, alturaCajaVehiculo);

            // Tabla de 3 filas x 4 columnas para datos del vehículo
            int yV = y + 8;
            Font fVehiculoLabel = new Font("Arial", 8, FontStyle.Bold);
            Font fVehiculoValue = new Font("Arial", 8, FontStyle.Regular);

            // Fila 1: 4 columnas
            g.DrawString("PLACA:", fVehiculoLabel, Brushes.Black, x + 15, yV);
            g.DrawString(txtPlaca.Text, fVehiculoValue, Brushes.Black, x + 60, yV);

            g.DrawString("PUESTOS:", fVehiculoLabel, Brushes.Black, x + 160, yV);
            g.DrawString(txtPuestos.Text, fVehiculoValue, Brushes.Black, x + 230, yV);

            g.DrawString("AÑO:", fVehiculoLabel, Brushes.Black, x + 290, yV);
            g.DrawString(txtAño.Text, fVehiculoValue, Brushes.Black, x + 325, yV);

            g.DrawString("MARCA:", fVehiculoLabel, Brushes.Black, x + 465, yV);
            g.DrawString(txtMarca.Text, fVehiculoValue, Brushes.Black, x + 515, yV);

            yV += 18;
            // Fila 2: 4 columnas
            g.DrawString("CLASE:", fVehiculoLabel, Brushes.Black, x + 15, yV);
            g.DrawString(txtClase.Text, fVehiculoValue, Brushes.Black, x + 60, yV);

            g.DrawString("USO:", fVehiculoLabel, Brushes.Black, x + 160, yV);
            g.DrawString(txtUso.Text, fVehiculoValue, Brushes.Black, x + 195, yV);

            g.DrawString("TIPO:", fVehiculoLabel, Brushes.Black, x + 290, yV);
            g.DrawString(txtTipo.Text, fVehiculoValue, Brushes.Black, x + 325, yV);

            g.DrawString("COLOR:", fVehiculoLabel, Brushes.Black, x + 465, yV);
            g.DrawString(txtColor.Text, fVehiculoValue, Brushes.Black, x + 515, yV);

            yV += 18;
            // Fila 3: 3 columnas
            g.DrawString("MODELO:", fVehiculoLabel, Brushes.Black, x + 15, yV);
            g.DrawString(txtModelo.Text, fVehiculoValue, Brushes.Black, x + 70, yV);

            g.DrawString("SERIAL MOTOR:", fVehiculoLabel, Brushes.Black, x + 160, yV);
            g.DrawString(txtSerialMotor.Text, fVehiculoValue, Brushes.Black, x + 285, yV);

            g.DrawString("SERIAL CARROC.:", fVehiculoLabel, Brushes.Black, x + 465, yV);
            g.DrawString(txtSerialCarroceria.Text, fVehiculoValue, Brushes.Black, x + 570, yV);

            y += alturaCajaVehiculo + 10;

            // ==========================================
            // 6. TABLA DE PLANES - REORGANIZADA Y COMPACTA
            // ==========================================
            g.FillRectangle(bGris, x, y, w, 18);
            g.DrawRectangle(pMedio, x, y, w, 18);
            CentrarTexto(g, $"PLAN: {plan.Nombre}", new Font("Arial", 10, FontStyle.Bold), w, y + 3);
            y += 18;

            // Dimensiones de columnas optimizadas
            int anchoConcepto = 380;
            int anchoSuma = 190;
            int anchoPrima = 190;

            // COBERTURA A TERCEROS (Azul claro)
            g.FillRectangle(Brushes.LightBlue, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x + anchoConcepto, y, anchoSuma, 16);
            g.DrawRectangle(pFino, x + anchoConcepto + anchoSuma, y, anchoPrima, 16);

            g.DrawString("COBERTURA A TERCEROS", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + 10, y + 2);
            g.DrawString("SUMA ASEGURADA ($)", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + anchoConcepto + 20, y + 2);
            g.DrawString("MONTO PRIMA ($)", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + anchoConcepto + anchoSuma + 20, y + 2);
            y += 16;

            // Filas de cobertura a terceros
            DibujarFilaTablaCompacta(g, "DAÑOS A COSAS:", plan.CobDanosCosas, x, ref y, anchoConcepto, anchoSuma, anchoPrima);
            DibujarFilaTablaCompacta(g, "DAÑOS A PERSONAS:", plan.CobDanosPersonas, x, ref y, anchoConcepto, anchoSuma, anchoPrima);
            DibujarFilaTablaCompacta(g, "EXCESO DE LIMITE:", plan.ExcesoLimite, x, ref y, anchoConcepto, anchoSuma, anchoPrima);

            // COBERTURA A OCUPANTES (Verde claro)
            g.FillRectangle(Brushes.LightGreen, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x + anchoConcepto, y, anchoSuma, 16);
            g.DrawRectangle(pFino, x + anchoConcepto + anchoSuma, y, anchoPrima, 16);

            g.DrawString("COBERTURA A OCUPANTES (APOV)", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + 10, y + 2);
            g.DrawString("SUMA ASEGURADA ($)", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + anchoConcepto + 20, y + 2);
            g.DrawString("MONTO PRIMA ($)", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + anchoConcepto + anchoSuma + 20, y + 2);
            y += 16;

            // Filas de cobertura a ocupantes
            DibujarFilaTablaCompacta(g, "INVALIDEZ PERMANENTE:", plan.CobInvalidez, x, ref y, anchoConcepto, anchoSuma, anchoPrima);
            DibujarFilaTablaCompacta(g, "GASTOS MEDICOS:", plan.CobGastosMedicos, x, ref y, anchoConcepto, anchoSuma, anchoPrima);
            DibujarFilaTablaCompacta(g, "MUERTE ACCIDENTAL:", plan.CobMuerte, x, ref y, anchoConcepto, anchoSuma, anchoPrima);
            DibujarFilaTablaCompacta(g, "INDEMNIZACION SEMANAL:", plan.IndemnizacionSemanal, x, ref y, anchoConcepto, anchoSuma, anchoPrima);

            // GRUA Y ASISTENCIA (Amarillo claro)
            g.FillRectangle(Brushes.LightYellow, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x + anchoConcepto, y, anchoSuma, 16);
            g.DrawRectangle(pFino, x + anchoConcepto + anchoSuma, y, anchoPrima, 16);

            g.DrawString("GRUA Y ASISTENCIA", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + 10, y + 2);
            g.DrawString("SUMA ASEGURADA ($)", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + anchoConcepto + 20, y + 2);
            g.DrawString("MONTO PRIMA ($)", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + anchoConcepto + anchoSuma + 20, y + 2);
            y += 16;

            // Filas de grúa
            DibujarFilaTablaCompacta(g, "GRUA POR DAÑOS MECANICOS:", plan.GruaDanosMecanicos, x, ref y, anchoConcepto, anchoSuma, anchoPrima);
            DibujarFilaTablaCompacta(g, "GRUA POR SINIESTRO:", plan.GruaSiniestro, x, ref y, anchoConcepto, anchoSuma, anchoPrima);

            // ASISTENCIA LEGAL (Coral claro)
            g.FillRectangle(Brushes.LightCoral, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x, y, anchoConcepto, 16);
            g.DrawRectangle(pFino, x + anchoConcepto, y, anchoSuma, 16);
            g.DrawRectangle(pFino, x + anchoConcepto + anchoSuma, y, anchoPrima, 16);

            DibujarFilaTablaCompacta(g, "ASISTENCIA LEGAL Y DEFENSA PENAL:", plan.AsistenciaLegal, x, ref y, anchoConcepto, anchoSuma, anchoPrima);

            y += 3;

            // ==========================================
            // 7. TOTALES - COMPACTOS
            // ==========================================
            decimal totalBeneficiosUSD = plan.CobDanosCosas + plan.CobDanosPersonas + plan.ExcesoLimite +
                                         plan.CobInvalidez + plan.CobGastosMedicos + plan.CobMuerte +
                                         plan.IndemnizacionSemanal + plan.GruaDanosMecanicos +
                                         plan.GruaSiniestro + plan.AsistenciaLegal;
            string textotasaLength = txtTasaCambio1.Text;
            decimal totalPagarBs = plan.PrecioUSD * this.tasaCambio;

            // TOTAL EN BENEFICIOS
            g.DrawRectangle(pMedio, x, y, w, 20);
            g.DrawString("TOTAL EN BENEFICIOS EN $", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + 10, y + 4);
            g.DrawString($"$ {totalBeneficiosUSD:N2}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + 520, y + 4);
            y += 25;

            // CÓDIGO DE REFERENCIA
            g.DrawRectangle(pFino, x, y+5, w, 20);
            g.DrawString("CODIGO DE REFERENCIA", fNegrita, Brushes.Black, x + 10, y + 4);
            g.DrawString($"---{textotasaLength}---", fNegrita, Brushes.Black, x + 650, y + 4);
            y += 19;

            // TOTAL A PAGAR
            g.FillRectangle(Brushes.LightGray, x, y, w, 22);
            g.DrawRectangle(pMedio, x, y, w, 22);
            g.DrawString("TOTAL A PAGAR EN BOLIVARES (Bs.)", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, x + 10, y + 4);
            g.DrawString($"Bs. {totalPagarBs:N2}", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, x + 600, y + 4);
            y += 25;

            // Crear formato para texto centrado
            StringFormat formatoCentrado = new StringFormat();
            formatoCentrado.Alignment = StringAlignment.Center;
            formatoCentrado.LineAlignment = StringAlignment.Center; // Opcional para centrado vertical

            g.DrawString("PARA LA VALIDEZ DEL PRESENTE CONTRATO DEBE TENER LA FIRMA DE LA PERSONA AUTORIZADA",
                new Font("Arial", 8, FontStyle.Bold), Brushes.Black, x + 30, y);
            y += 100;

            // 1. IZQUIERDA: Firma del Contratante
            g.DrawLine(new Pen(Color.Black, 1.5f), x + 50, y, x + 280, y);
            g.DrawString("POR EL CONTRATANTE", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + 85, y + 5);

            // --- NUEVO: IMAGEN 4 EN EL CENTRO (ENTRE LAS DOS FIRMAS) ---
            if (!imprimirSinLogos && Properties.Resources.imagen4 != null)
            {
                try { g.DrawImage(Properties.Resources.imagen4, x + 305, y - 60, 150, 80); } catch { }
            }

            // 2. DERECHA: Firma de la Empresa (CON LA IMAGEN 3)
            if (!imprimirSinLogos && Properties.Resources.imagen3 != null)
            {
                g.DrawImage(Properties.Resources.imagen3, x + 525, y - 70, 120, 60);
            }

            g.DrawLine(new Pen(Color.Black, 1.5f), x + 480, y, x + 710, y);
            g.DrawString("EMPRESA - FIRMA AUTORIZADA", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + 500, y + 5);
            y += 20;

            // Texto Legal CENTRADO
            string textoLegal1 = txtOficinaDetallada.Text;
            string textoLegal2 = "TELEFONOS DE CONTACTO Y ATENCION AL CLIENTE: 0414-961.84.41 / 0412-646.09.98";
            string textoLegal3 = "INSCRITA EN EL REGISTRO MERCANTIL PRIMERO DEL ESTADO BOLIVARIANO DE MERIDA BAJO EL No. 2, TOMO 91-B DEL MUNICIPIO LIBERTADOR.";

            // Definir el área donde se centrará el texto (ancho completo de la página)
            RectangleF areaLegal = new RectangleF(x, y, w, 40);

            // Dibujar cada línea centrada
            g.DrawString(textoLegal1, new Font("Arial", 7, FontStyle.Bold), Brushes.Black, areaLegal, formatoCentrado);
            y += 12;

            areaLegal.Y = y; // Actualizar posición Y
            g.DrawString(textoLegal2, new Font("Arial", 7, FontStyle.Bold), Brushes.Black, areaLegal, formatoCentrado);
            y += 12;

            areaLegal.Y = y; // Actualizar posición Y
            g.DrawString(textoLegal3, new Font("Arial", 7, FontStyle.Bold), Brushes.Black, areaLegal, formatoCentrado);
            y += 15;

            // ==========================================
            // 9. CARNET - REDIMENSIONADO Y REDISTRIBUIDO
            // ==========================================

            // 1. Línea de corte (tijeras)
            y += 20;
            Pen pPunteado = new Pen(Color.Black, 1);
            pPunteado.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(pPunteado, 0, y, 850, y);
            g.DrawString("✂", new Font("Segoe UI Symbol", 12), Brushes.Black, 20, y - 8);

            y += 15;

            // --- VARIABLES DE TAMAÑO ---
            int margenReduccion = 50; // Ajustado a 50px para darte un pelín más de espacio interno

            int carnetX = x + margenReduccion;
            int carnetW = w - (margenReduccion * 2);
            int carnetH = 195;
            int carnetY = y;

            // 2. Borde del carnet
            Pen pBordeCarnet = new Pen(Color.Black, 1);
            pBordeCarnet.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            g.DrawRectangle(pBordeCarnet, carnetX, carnetY, carnetW, carnetH);

            // 3. Línea Divisoria (40% Izquierda / 60% Derecha)
            int lineaMediaX = carnetX + (int)(carnetW * 0.50);

            Pen pDivision = new Pen(Color.Gray, 1);
            pDivision.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(pDivision, lineaMediaX, carnetY, lineaMediaX, carnetY + carnetH);

            // =========================================================
            // A) SECCIÓN IZQUIERDA (QR A LA IZQ - FIRMA A LA DER)
            // =========================================================

            // Caja Roja
            int redBoxX = carnetX + 5;
            int redBoxY = carnetY + 5;
            int anchoSeccIzq = lineaMediaX - carnetX;
            int anchoCajaRoja = anchoSeccIzq - 10;

            g.FillRectangle(Brushes.Red, redBoxX, redBoxY, anchoCajaRoja, 18);

            StringFormat sfCentro = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString($"VENCIMIENTO: {dtpVencimiento.Value:dd/MM/yyyy}",
                new Font("Arial", 8, FontStyle.Bold), Brushes.White,
                new RectangleF(redBoxX, redBoxY + 3, anchoCajaRoja, 18), sfCentro);

            // --- QR (Alineado a la Izquierda) ---
            int qrY = redBoxY + 30;
            int qrSize = 120;
            if (pbQR.Image != null)
            {
                // Margen izquierdo de 10px
                g.DrawImage(pbQR.Image, carnetX + 20, qrY, qrSize, qrSize);
            }

            // --- FIRMA (A la derecha del QR) ---
            int firmaX = carnetX + 200; // Posición X fija a la derecha del QR
            int firmaY = qrY + 5;
            int firmaW = anchoSeccIzq; // El espacio restante

            // Imagen de la firma
            if (!imprimirSinLogos && Properties.Resources.imagen3 != null)
            {
                g.DrawImage(Properties.Resources.imagen3, firmaX - 50, firmaY, 150, 80);
            }

            // Línea y Texto "Firma Autorizada"
            g.DrawLine(Pens.Black, firmaX - 45, firmaY + 85, firmaX + 115, firmaY + 85);
            g.DrawString("Firma Autorizada",
                new Font("Arial", 12, FontStyle.Italic), Brushes.Black, firmaX - 30, firmaY + 90);

            // --- DATOS GERENTE (ABAJO DEL TODO) ---
            // Centrados en toda la sección izquierda
            RectangleF areaGerente = new RectangleF(carnetX + 20, carnetY + 158, anchoSeccIzq, 60);
            g.DrawString("GERENTE: ANDRES ROJAS\nRIF: V-14879903-9\nTEL: 0414-961.84.41 | 0412-646.09.98",
                new Font("Arial", 7, FontStyle.Bold), Brushes.Black, areaGerente, sfCentro);


            // =========================================================
            // B) SECCIÓN DERECHA (DATOS VEHÍCULO - AUMENTADOS)
            // =========================================================

            int xDer = lineaMediaX + 10; // Margen interno
            int yDer = carnetY + 8;
            int anchoDer = (carnetX + carnetW) - xDer - 5;

            // 1. TÍTULO RCV (Grande)
            g.DrawString("RCV Nº: " + numeroRCV,
                new Font("Arial", 13, FontStyle.Bold), Brushes.Black, xDer, yDer);

            // 2. BARRA EMISIÓN
            yDer += 25;
            g.FillRectangle(Brushes.LightCyan, xDer, yDer, anchoDer, 16);
            g.DrawRectangle(Pens.Gray, xDer, yDer, anchoDer, 16);

            g.DrawString($"EMISION: {dtpEmision.Value:dd/MM/yyyy}",
                new Font("Arial", 8, FontStyle.Bold), Brushes.Black, xDer + 4, yDer + 2);

            StringFormat sfDerecha = new StringFormat { Alignment = StringAlignment.Far };
            g.DrawString($"VENCE: {dtpVencimiento.Value:dd/MM/yyyy}",
                new Font("Arial", 8, FontStyle.Bold), Brushes.DarkRed,
                new RectangleF(xDer, yDer + 2, anchoDer - 4, 16), sfDerecha);

            // 3. DATOS PERSONALES
            yDer += 20;
            // AUMENTO DE FUENTE Y ESPACIADO
            float lineHeight = 13; // Más espacio vertical
            Font fLabel = new Font("Arial", 6.5f, FontStyle.Bold); // Letra más grande (antes 6)
            Font fValue = new Font("Arial", 7.5f, FontStyle.Regular); // Letra más grande (antes 7)

            // Contratante
            g.DrawString("CONTRATANTE:", fLabel, Brushes.Black, xDer, yDer);
            string contratante = txtContratante.Text.Length > 80 ? txtContratante.Text.Substring(0, 80) + "..." : txtContratante.Text;
            g.DrawString(contratante, fValue, Brushes.Black, xDer + 85, yDer);
            yDer += (int)lineHeight;

            g.DrawString("CI/RIF:", fLabel, Brushes.Black, xDer, yDer);
            g.DrawString(txtRifCedulaContratante.Text, fValue, Brushes.Black, xDer + 85, yDer);
            yDer += (int)lineHeight;

            // Beneficiario
            g.DrawString("BENEFICIARIO:", fLabel, Brushes.Black, xDer, yDer);
            string beneficiario = txtBeneficiario.Text.Length > 80 ? txtBeneficiario.Text.Substring(0, 80) + "..." : txtBeneficiario.Text;
            g.DrawString(beneficiario, fValue, Brushes.Black, xDer + 85, yDer);
            yDer += (int)lineHeight;

            g.DrawString("CI/RIF:", fLabel, Brushes.Black, xDer, yDer);
            g.DrawString(txtRifCedulaBeneficiario.Text, fValue, Brushes.Black, xDer + 85, yDer);

            yDer += (int)lineHeight + 6; // Separador extra antes del vehículo

            // 4. DATOS VEHÍCULO (COLUMNAS MEJOR DISTRIBUIDAS)
            // Ajuste de columnas para llenar el espacio derecho
            int col1 = xDer;
            int col2 = xDer + 155; // Movido más a la derecha (antes 130)

            // Fila 1
            g.DrawString("PLACA:", fLabel, Brushes.Black, col1, yDer);
            g.DrawString(txtPlaca.Text, fValue, Brushes.Black, col1 + 40, yDer);

            g.DrawString("AÑO:", fLabel, Brushes.Black, col2, yDer);
            g.DrawString(txtAño.Text, fValue, Brushes.Black, col2 + 30, yDer);
            yDer += (int)lineHeight;

            // Fila 2
            g.DrawString("MARCA:", fLabel, Brushes.Black, col1, yDer);
            string marca = txtMarca.Text.Length > 20 ? txtMarca.Text.Substring(0, 20) + "." : txtMarca.Text;
            g.DrawString(marca, fValue, Brushes.Black, col1 + 42, yDer);

            g.DrawString("COLOR:", fLabel, Brushes.Black, col2, yDer);
            string color = txtColor.Text.Length > 20 ? txtColor.Text.Substring(0, 20) + "." : txtColor.Text;
            g.DrawString(color, fValue, Brushes.Black, col2 + 40, yDer);
            yDer += (int)lineHeight;

            // Fila 3
            g.DrawString("MODELO:", fLabel, Brushes.Black, col1, yDer);
            string modelo = txtModelo.Text.Length > 20 ? txtModelo.Text.Substring(0, 20) + "." : txtModelo.Text;
            g.DrawString(modelo, fValue, Brushes.Black, col1 + 50, yDer);

            g.DrawString("PUESTOS:", fLabel, Brushes.Black, col2, yDer);
            g.DrawString(txtPuestos.Text, fValue, Brushes.Black, col2 + 55, yDer);
            yDer += (int)lineHeight;

            // Fila 4 (Seriales)
            g.DrawString("S. CARROCERIA:", fLabel, Brushes.Black, col1, yDer);
            g.DrawString(txtSerialCarroceria.Text, fValue, Brushes.Black, col1 + 90, yDer);
            yDer += (int)lineHeight;

            g.DrawString("S. MOTOR:", fLabel, Brushes.Black, col1, yDer);
            g.DrawString(txtSerialMotor.Text, fValue, Brushes.Black, col1 + 60, yDer);
        }
        private void CentrarTexto(Graphics g, string txt, Font f, int anchoPagina, int y)
        {
            SizeF size = g.MeasureString(txt, f);
            float x = (anchoPagina - size.Width) / 2 + 30; // +30 por el margen izquierdo
            g.DrawString(txt, f, Brushes.Black, x, y);
        }
        private void pbQR_Click(object sender, EventArgs e)
        {
            // Evento vacío requerido por el diseñador
        }

        // Actualiza los cálculos cuando escribes
        private void txtTasaCambio_TextChanged(object sender, EventArgs e)
        {
            string texto = txtTasaCambio.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                tasaCambio = 0;
            }
            else
            {
                // Convertimos a decimal permitiendo tanto punto como coma
                string textoNormalizado = texto.Replace(',', '.');

                if (decimal.TryParse(textoNormalizado, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal nuevaTasa))
                {
                    tasaCambio = nuevaTasa;
                }
                else
                {
                    tasaCambio = 0;
                }
            }

            // Esto solo redibuja el Panel de visualización con los nuevos cálculos en Bs.
            if (panel28 != null)
            {
                panel28.Invalidate();
            }
        }
        private void txtTasaCambio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números y teclas de control (borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Solo permitir UN separador decimal (sea punto o coma)
            if ((e.KeyChar == '.' || e.KeyChar == ','))
            {
                if (txtTasaCambio.Text.Contains(".") || txtTasaCambio.Text.Contains(","))
                {
                    e.Handled = true;
                }
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtRCV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOficinaDetallada_TextChanged(object sender, EventArgs e)
        {

        }
        // Método auxiliar para dibujar celdas compactas
        private void DrawCellTextCompact(Graphics g, string titulo, string valor, int x, int y, int w)
        {
            g.DrawString(titulo, new Font("Arial", 7, FontStyle.Regular), Brushes.Black, x + 2, y + 2);
            g.DrawString(valor, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, x + 5, y + 15);
        }

        // Método auxiliar para dibujar filas de tabla compactas
        private void DibujarFilaTablaCompacta(Graphics g, string concepto, decimal monto, int x, ref int y,
                                             int anchoConcepto, int anchoSuma, int anchoPrima)
        {
            int h = 14; // Altura reducida de la fila

            // Dibujar bordes
            g.DrawRectangle(Pens.Black, x, y, anchoConcepto, h);
            g.DrawRectangle(Pens.Black, x + anchoConcepto, y, anchoSuma, h);
            g.DrawRectangle(Pens.Black, x + anchoConcepto + anchoSuma, y, anchoPrima, h);

            // Texto del Concepto
            string conceptoAjustado = concepto.Length > 35 ? concepto.Substring(0, 35) + "..." : concepto;
            g.DrawString(conceptoAjustado, new Font("Arial", 8), Brushes.Black, x + 5, y + 2);

            // Texto del Monto
            string txtMonto = monto > 0 ? $"$ {monto:N2}" : "$ 0,00";
            StringFormat formatoDerecha = new StringFormat { Alignment = StringAlignment.Far };

            // Columna Suma Asegurada ($)
            g.DrawString(txtMonto, new Font("Arial", 8, FontStyle.Bold), Brushes.Black,
                         new RectangleF(x + anchoConcepto, y + 2, anchoSuma - 5, h), formatoDerecha);

            // Columna Prima ($)
            g.DrawString("$ 0,00", new Font("Arial", 8, FontStyle.Bold), Brushes.Black,
                         new RectangleF(x + anchoConcepto + anchoSuma, y + 2, anchoPrima - 5, h), formatoDerecha);

            y += h;
        }

        private void txtTasaCambio1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnImprimirSinLogos_Click(object sender, EventArgs e)
        {
            imprimirSinLogos = true; // NO queremos logos
            EjecutarImpresion();
        }

        private void EjecutarImpresion()
        {
            try
            {
                documento.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Carta", 850, 1100);
                documento.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);

                string impresoraEncontrada = "";

                // Buscar impresora PDF (o puedes quitar esto para usar la predeterminada)
                foreach (string imp in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    if (imp.ToUpper().Contains("PDF"))
                    {
                        impresoraEncontrada = imp;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(impresoraEncontrada))
                {
                    documento.PrinterSettings.PrinterName = impresoraEncontrada;
                    documento.DocumentName = $"RCV_{txtPlaca.Text}_{DateTime.Now:ddMMyyyy}";

                    // --- ESTA ES LA LÍNEA QUE FALTABA ---
                    documento.Print();
                    // ------------------------------------
                }
                else
                {
                    // Si no encuentra PDF, imprime en la predeterminada o avisa
                    // documento.Print(); // Descomenta si quieres imprimir aunque no sea PDF
                    MessageBox.Show("No se encontró impresora PDF automática.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message);
            }
        }
    }
}