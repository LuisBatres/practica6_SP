using System;
using System.Windows.Forms;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace PuertoSerie
{
    public partial class frmSerie : Form
    {
        SerialPort sp = new SerialPort();

        public frmSerie()
        {
            InitializeComponent();
            sp.BaudRate = 9600;
            sp.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        private void frmSerie_Load(object sender, EventArgs e)
        {
            cmbPuertos.DataSource = SerialPort.GetPortNames();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (cmbPuertos.SelectedItem == null)
            {
                MessageBox.Show("Por favor selecciona un puerto COM");
                return;
            }

            try
            {
                if (!sp.IsOpen)
                {
                    sp.PortName = cmbPuertos.Text;
                    sp.Open();

                    if (sp.IsOpen)
                    {
                        label6.Text = "Conectado";
                        label6.ForeColor = Color.Green;
                    }
                    else
                    {
                        MessageBox.Show("Error de conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Este método se ejecuta cuando se reciben datos a través del puerto serialf4r
            try
            {
                SerialPort actualSP = (SerialPort)sender;

                // Leer los datos recibidos del puerto serial
                string inData = actualSP.ReadLine();
                rtbLog.Invoke(new MethodInvoker(
                    delegate
                    {
                        // Mostrar los datos recibidos en el log
                        //rtbLog.AppendText(inData + "\n");
                        rtbLog.Text = inData;
                    }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recibir datos: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                    label6.Text = "Desconectado";
                    label6.ForeColor = Color.Red;
                    rtbLog.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            sp.Close();
            sp.Dispose();
            Close();
        }
    }
}
