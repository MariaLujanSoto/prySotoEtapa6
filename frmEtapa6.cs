using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prySotoEtapa6
{
    public partial class frmEtapa6 : Form
    {
        public frmEtapa6()
        {
            InitializeComponent();
            timer.Interval = 100; // Intervalo de 100 milisegundos para mover los vehículos
            timer.Tick += timer_Tick; // Asigna el evento Tick al Timer
            timer.Start(); // Inicia el Timer
        }

        private List<PictureBox> vehiculos = new List<PictureBox>();

        List<clsVehiculo> listaVehiculos = new List<clsVehiculo>();

        private Random rnd = new Random();
        clsVehiculo objAuto = new clsVehiculo();
        clsVehiculo objAvion = new clsVehiculo();
        clsVehiculo objBarco = new clsVehiculo();

        private void frmEtapa6_Load(object sender, EventArgs e)
        {

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Ingrese un valor numérico válido para la cantidad.");
                return;
            }

            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("Ingrese un valor numérico válido para la cantidad.");
                return;
            }

            for (int i = 0; i < cantidad; i++)
            {
                PictureBox vehiculo = CrearVehiculoAleatorio();
            }

        }

        private PictureBox CrearVehiculoAleatorio()
        {
            PictureBox vehiculo;
            int indiceAleatorio = rnd.Next(1, 4);

            switch (indiceAleatorio)
            {
                case 1:
                    if (objAuto.Auto != null)
                    {
                        Controls.Remove(objAuto.Auto);
                    }
                    objAuto.crearAuto();
                    objAuto.Auto.Location = new Point(400, 500);
                    vehiculo = objAuto.Auto;
                    break;

                case 2:
                    if (objAvion.Avion != null)
                    {
                        Controls.Remove(objAvion.Avion);
                    }
                    objAvion.crearAvion();
                    objAvion.Avion.Location = new Point(200, 100);
                    vehiculo = objAvion.Avion;
                    break;

                case 3:
                    if (objBarco.Barco != null)
                    {
                        Controls.Remove(objBarco.Barco);
                    }
                    objBarco.crearBarco();
                    objBarco.Barco.Location = new Point(300, 350);
                    vehiculo = objBarco.Barco;
                    break;

                default:
                    vehiculo = new PictureBox(); // En caso de un valor no válido, se crea un PictureBox vacío
                    break;
            }


            clsVehiculo nuevoVehiculo = new clsVehiculo();

            nuevoVehiculo.crearAuto();

            int posicionX;
            int posicionY;
            bool superpuesto;

            do
            {
                posicionX = rnd.Next(0, this.ClientSize.Width - nuevoVehiculo.Auto.Width);

                posicionY = rnd.Next(0, this.ClientSize.Height - nuevoVehiculo.Auto.Height);

                superpuesto = false;

                foreach (clsVehiculo vehiculoExistente in listaVehiculos)
                {
                    if (Math.Abs(posicionX - vehiculoExistente.Auto.Location.X) < nuevoVehiculo.Auto.Width && Math.Abs(posicionY - vehiculoExistente.Auto.Location.Y) < nuevoVehiculo.Auto.Height)
                    {
                        superpuesto = true;
                        break;
                    }
                }
            }
            while (superpuesto);

            nuevoVehiculo.Auto.Location = new Point(posicionX, posicionY);
            listaVehiculos.Add(nuevoVehiculo);
            Controls.Add(nuevoVehiculo.Auto);
            return vehiculo;

        }

        private void VerificarColisiones(PictureBox vehiculo)
        {
            foreach (PictureBox otroVehiculo in vehiculos.ToList())
            {
                if (vehiculo != otroVehiculo && vehiculo.Bounds.IntersectsWith(otroVehiculo.Bounds))
                {
                    //listaVehiculos.Remove(vehiculo);
                    //listaVehiculos.Remove(otroVehiculo);
                    MessageBox.Show("¡Choque entre vehículos!");

                }
            }

            //foreach (clsVehiculo otroVehiculo in listaVehiculos.ToList())
            //{
            //    if (otroVehiculo != vehiculo && vehiculo.pctAuto.Bounds.IntersectsWith(otroVehiculo.pctAuto.Bounds))
            //    {
            //        listaVehiculos.Remove(vehiculo);
            //        listaVehiculos.Remove(otroVehiculo);
            //        Controls.Remove(vehiculo.pctAuto);
            //        Controls.Remove(otroVehiculo.pctAuto);
            //        break;
            //    }
            //}
        }


        

        private void btnMover_Click(object sender, EventArgs e)
        {
            foreach (PictureBox vehiculo in vehiculos)
            {
                
                timer.Start();

            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (clsVehiculo vehiculo in listaVehiculos.ToList())
            {
                int dx = rnd.Next(-10, 11); // Movimiento aleatorio en el eje X
                int dy = rnd.Next(-10, 11); // Movimiento aleatorio en el eje Y

                // Calcula la nueva posición sumando los cambios aleatorios
                int nuevaPosX = vehiculo.Auto.Location.X + dx;
                int nuevaPosY = vehiculo.Auto.Location.Y + dy;

                // Actualiza la posición del vehículo
                vehiculo.Auto.Location = new Point(nuevaPosX, nuevaPosY);
            }
        }
    }
}