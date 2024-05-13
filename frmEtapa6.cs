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
            timer.Interval = 100; 
            timer.Tick += timer_Tick; 
           
        }

        //private List<PictureBox> vehiculos = new List<PictureBox>();

        List<clsVehiculo> listaVehiculos = new List<clsVehiculo>();

        private Random rnd = new Random();
           

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
                CrearVehiculoAleatorio();
            }

        }

        private void CrearVehiculoAleatorio()
        {
            //PictureBox vehiculo;
            //int indiceAleatorio = rnd.Next(1, 4);

            //switch (indiceAleatorio)
            //{
            //    case 1:
            //        if (objAvion.Avion != null)
            //        {
            //            Controls.Remove(objAvion.Avion);
            //        }
            //        objAvion.crearAvion();
            //        objAvion.Avion.Location = new Point(400, 500);
            //        vehiculo = objAvion.Avion;
            //        break;

            //    case 2:
            //        if (objAvion.Avion != null)
            //        {
            //            Controls.Remove(objAvion.Avion);
            //        }
            //        objAvion.crearAvion();
            //        objAvion.Avion.Location = new Point(200, 100);
            //        vehiculo = objAvion.Avion;
            //        break;

            //    case 3:
            //        if (objBarco.Barco != null)
            //        {
            //            Controls.Remove(objBarco.Barco);
            //        }
            //        objBarco.crearBarco();
            //        objBarco.Barco.Location = new Point(300, 350);
            //        vehiculo = objBarco.Barco;
            //        break;

            //    default:
            //        vehiculo = new PictureBox(); // En caso de un valor no válido, se crea un PictureBox vacío
            //        break;
            //}


            //clsVehiculo nuevoVehiculo = new clsVehiculo();

            //nuevoVehiculo.crearAvion();

            //int posicionX;
            //int posicionY;
            //bool superpuesto;

            //do
            //{
            //    posicionX = rnd.Next(0, this.ClientSize.Width - nuevoVehiculo.Avion.Width);

            //    posicionY = rnd.Next(0, this.ClientSize.Height - nuevoVehiculo.Avion.Height);

            //    superpuesto = false;

            //    foreach (clsVehiculo vehiculoExistente in listaVehiculos)
            //    {
            //        if (Math.Abs(posicionX - vehiculoExistente.Avion.Location.X) < nuevoVehiculo.Avion.Width && Math.Abs(posicionY - vehiculoExistente.Avion.Location.Y) < nuevoVehiculo.Avion.Height)
            //        {
            //            superpuesto = true;
            //            break;
            //        }
            //    }
            //}
            //while (superpuesto);

            //nuevoVehiculo.Avion.Location = new Point(posicionX, posicionY);
            //listaVehiculos.Add(nuevoVehiculo);
            //Controls.Add(nuevoVehiculo.Avion);
            //return vehiculo;


            clsVehiculo nuevoVehiculo = new clsVehiculo();
            nuevoVehiculo.crearAvion();

            int margen = 50; // Margen

            int posicionX;
            int posicionY;
            bool superpuesto;

            do
            {
                // Pos aleatorioa dentro del frm
                posicionX = rnd.Next(margen, this.ClientSize.Width - margen - nuevoVehiculo.Avion.Width);
                posicionY = rnd.Next(margen, this.ClientSize.Height - margen - nuevoVehiculo.Avion.Height);

                superpuesto = false;

                // Verificar q la nueva posición no este sobre las otras
                foreach (clsVehiculo vehiculoExistente in listaVehiculos)
                {
                    if (Math.Abs(posicionX - vehiculoExistente.Avion.Location.X) < nuevoVehiculo.Avion.Width &&
                        Math.Abs(posicionY - vehiculoExistente.Avion.Location.Y) < nuevoVehiculo.Avion.Height)
                    {
                        superpuesto = true;
                        break;
                    }
                }
            } while (superpuesto);

            // dar la pos y crear el vehiculo
            nuevoVehiculo.Avion.Location = new Point(posicionX, posicionY);
            listaVehiculos.Add(nuevoVehiculo);
            Controls.Add(nuevoVehiculo.Avion);


        }
   
       
        

        private void btnMover_Click(object sender, EventArgs e)
        {           
                 
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            foreach (clsVehiculo vehiculo in listaVehiculos.ToList())
            {
                int dx = rnd.Next(-30, 30); // Mov eje X
                int dy = rnd.Next(-30, 30); // Movimiento aleatorio en el eje Y

                // Calcula la nueva posición sumando los cambios aleatorios
                int nuevaPosX = vehiculo.Avion.Location.X + dx;
                int nuevaPosY = vehiculo.Avion.Location.Y + dy;

                // Verifica que la nueva posición esté dentro de los límites del formulario
                nuevaPosX = Math.Max(0, Math.Min(nuevaPosX, this.ClientSize.Width - vehiculo.Avion.Width));
                nuevaPosY = Math.Max(0, Math.Min(nuevaPosY, this.ClientSize.Height - vehiculo.Avion.Height));

                // Actualiza la posición del vehículo
                vehiculo.Avion.Location = new Point(nuevaPosX, nuevaPosY);

                // Verifica colisiones y elimina los vehículos involucrados
                foreach (clsVehiculo otroVehiculo in listaVehiculos.ToList())
                {
                    if (vehiculo != otroVehiculo && vehiculo.Avion.Bounds.IntersectsWith(otroVehiculo.Avion.Bounds))
                    {
                        // Eliminar vehículos de la lista y del formulario
                        Controls.Remove(otroVehiculo.Avion);
                        listaVehiculos.Remove(otroVehiculo);
                    }
                }
            }

        }
    }
}