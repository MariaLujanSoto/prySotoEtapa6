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
                vehiculos.Add(vehiculo);
                Controls.Add(vehiculo);
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

            // Verificar colisiones al crear el vehículo
            bool hayColisiones = true;
            while (hayColisiones)
            {
                // Comprobar si hay colisiones con otros vehículos existentes
                hayColisiones = false;
                foreach (PictureBox v in vehiculos)
                {
                    if (vehiculo.Bounds.IntersectsWith(v.Bounds))
                    {
                        // Si hay colisión, ajustar la posición y volver a comprobar
                        vehiculo.Location = new Point(rnd.Next(ClientSize.Width - vehiculo.Width), rnd.Next(ClientSize.Height - vehiculo.Height));
                        hayColisiones = true;
                        break;
                    }
                }
            }

            // Agregar el vehículo a la lista y devolverlo
            vehiculos.Add(vehiculo);
            return vehiculo;

        }

        private void VerificarColisiones(PictureBox vehiculo)
        {
            foreach (PictureBox otroVehiculo in vehiculos)
            {
                if (vehiculo != otroVehiculo && vehiculo.Bounds.IntersectsWith(otroVehiculo.Bounds))
                {
                    MessageBox.Show("¡Choque entre vehículos!");
                    // Aquí podrías implementar otras acciones, como eliminar los vehículos implicados en la colisión
                }
            }
        }


        private void MoverVehiculo(PictureBox vehiculo)
        {
            int dx = rnd.Next(-10, 11); // Movimiento aleatorio en el eje X
            int dy = rnd.Next(-10, 11); // Movimiento aleatorio en el eje Y

            // Calcula la nueva posición sumando los cambios aleatorios
            int nuevaX = vehiculo.Location.X + dx;
            int nuevaY = vehiculo.Location.Y + dy;

            // Verifica que el vehículo no se salga de los límites del formulario
            nuevaX = Math.Max(0, Math.Min(nuevaX, this.ClientSize.Width - vehiculo.Width));
            nuevaY = Math.Max(0, Math.Min(nuevaY, this.ClientSize.Height - vehiculo.Height));

            // Actualiza la posición del vehículo
            vehiculo.Location = new Point(nuevaX, nuevaY);
        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            foreach (PictureBox vehiculo in vehiculos)
            {
                MoverVehiculo(vehiculo);
                VerificarColisiones(vehiculo);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }
    }
}