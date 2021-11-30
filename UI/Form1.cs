using IoTBLL;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Propiedades
        private SerialPort serialPort;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Creo el objeto serialPort
            serialPort = new SerialPort();
            serialPort.PortName = "COM5";
            serialPort.BaudRate = 9600;

            // Inicio en estado desconectado
            labelEstado.Text = "Desconectado";
            labelEstado.ForeColor = Color.Red;
        }

        /// <summary>
        /// Inicia la lectura desde el dispositivo IoT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeer_Click(object sender, EventArgs e)
        {
            try
            {                
                if (serialPort.IsOpen == true)
                {
                    int timer = 20;
                    labelContador.Text = timer.ToString();
                    labelContador.Refresh();

                    while (timer > 0)
                    {
                        // Lectura de valores desde el saturómetro
                        leerSaturometro();
                        timer--;
                        labelContador.Text = timer.ToString();
                        labelContador.Refresh();
                        System.Threading.Thread.Sleep(500);
                    }
                }
                else
                {
                    MessageBox.Show("No se encuentra conectado el dispositivo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Guarda los datos leídos desde el dispositivo IoT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //var response = OxigenoBLL.Current.EnviarAsync(labelOxigeno.Text);
                //MessageBox.Show(response.Result.ToString());
                EnviarDatosBLL.Current.EnviarAsync(labelOxigeno.Text, labelLatidos.Text, textBoxTemperatura.Text);

                MessageBox.Show("Datos enviados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Conecta al puerto Bluetooth
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    // Abre el puerto serie de Bluetooth
                    serialPort.Open();
                    labelEstado.Text = "Conectado";
                    labelEstado.ForeColor = Color.Green;
                }
                else
                {
                    labelEstado.Text = "Desconectado";
                    labelEstado.ForeColor = Color.Red;
                }            
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }

        }

        /// <summary>
        /// Desconecta el puerto Bluetooth
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }

                labelEstado.Text = "Desconectado";
                labelEstado.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Botón para finalizar la lectura de valores del Saturómetro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetener_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Lee valores del Saturómetro
        /// </summary>
        private void leerSaturometro()
        {
            try
            {
                serialPort.Write("F");
                string cadena = serialPort.ReadExisting();
                string[] lecturas = cadena.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (var item in lecturas)
                {
                    string[] valores = item.Split(';');
                    if (valores.Length == 2)
                    {
                        textBoxLectura.Text = item;
                        textBoxLectura.Refresh();

                        // Latidos por minuto
                        if (Convert.ToInt32(valores[0]) > 0)
                        {
                            labelLatidos.Text = valores[0];
                            labelLatidos.Refresh();
                        }

                        // SPO2
                        if (Convert.ToInt32(valores[1]) > 0)
                        {
                            labelOxigeno.Text = valores[1];
                            labelOxigeno.Refresh();
                        }
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
