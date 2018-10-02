using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Xirgu
{
    public partial class frmXirgu : Form
    {
        Jugada jugada_player = new Jugada();
        Igu the_igu = Igu.GetInstance();
        private char[] tablero;

        Bitmap peonrojo = new Bitmap("peonrojo.bmp");
        Bitmap peonnegro = new Bitmap("peonnegro.bmp");
        Bitmap reinaroja = new Bitmap("reinaroja.bmp");
        Bitmap reinanegra = new Bitmap("reinanegra.bmp");
        Bitmap peonrojoseleccionado = new Bitmap("peonrojoseleccionado.bmp");
        Bitmap reinarojaseleccionada = new Bitmap("reinarojaseleccionada.bmp");

        XirguGame the_game = XirguGame.GetInstance();
        List<int> posiciones_seleccionadas = new List<int>();

        public frmXirgu()
        {
            InitializeComponent();
        }

        private void frmXirgu_Load(object sender, EventArgs e)
        {
        }

        public void MostrarTablero(Graphics g)
        {
            int possel = -1;
            if (posiciones_seleccionadas.Count > 0)
            {
                possel = posiciones_seleccionadas[0];
            }
            the_igu.ActualizarTablero();
            tablero = the_igu.Tablero;
            int fila = 0;
            int k = 0;
            for (int i = 31; i >= 0; i--)
            {
                if (k == 0 && fila % 2 != 0)
                {
                    
                }
                
                if (tablero[i] == 'b')
                    g.DrawImage(peonrojo,DePosAPintar(i+1));
                if (tablero[i] == 'n')
                    g.DrawImage(peonnegro, DePosAPintar(i + 1));
                if (tablero[i] == 'N')
                    g.DrawImage(reinanegra, DePosAPintar(i + 1));
                if (tablero[i] == 'B')
                    g.DrawImage(reinaroja, DePosAPintar(i + 1));
                if (possel == i + 1)
                {
                    if (tablero[i] == 'B')
                        g.DrawImage(reinarojaseleccionada, DePosAPintar(i + 1));
                    if (tablero[i] == 'b')
                        g.DrawImage(peonrojoseleccionado, DePosAPintar(i + 1));
                }
                
                if (k < 4)
                {
                   
                    k++;
                }
                if (k == 4)
                {
                    fila++;
                    k = 0;
                }

            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            MostrarTablero(e.Graphics);
        }

        private Point DePosAPintar(int pos)
        {
            int c = the_game.Juego.ColumnaDe(pos);
            int f = the_game.Juego.FilaDe(pos);
            int x, y;
            x = (720 - c * 90) + 5;
            y = (400 - f * 50) + 5;

            return new Point(x, y);
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
            int pos = DePintarAPos(e.X, e.Y);
            if (pos == 0)
                return;

            if (posiciones_seleccionadas.Count == 0)
            {
                
                posiciones_seleccionadas.Add(pos);
                pnlTablero.Refresh();
            }
            else
            {
                if (posiciones_seleccionadas.Count == 1)
                {
                    if (pos == posiciones_seleccionadas[0])
                        return;
                    posiciones_seleccionadas.Add(pos);
                    the_igu.ObtenerJugada(posiciones_seleccionadas);
                    if (!XirguGame.GetInstance().HacerJugadaDeJugador())
                    {
                        MessageBox.Show("Jugada Ilegal!", "Alerta");
                    }
                    else
                    {
                        the_igu.ActualizarTablero();
                        pnlTablero.Refresh();
                        VerificarTerminacion();



                        the_game.HacerJugadaMaquina();
                        the_igu.ActualizarTablero();
                        pnlTablero.Refresh();
                        VerificarTerminacion();
                    }
                    posiciones_seleccionadas.Clear();
                    pnlTablero.Refresh();

                }

            }
        }
        private void VerificarTerminacion()
        {
            Pieza.Jugadores ganador;
            if ((ganador = XirguGame.GetInstance().Juego.QuienGano()) == Pieza.Jugadores.blanco)
            {
                MessageBox.Show("Felicitaciones me Has Ganado!!!!", "Juego Terminado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if ((ganador = XirguGame.GetInstance().Juego.QuienGano()) == Pieza.Jugadores.negro)
                    MessageBox.Show("Perdió Ember, pónganos  5.0 en el corte!!!", "Juego Terminado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private int DePintarAPos(int pintarx, int pintary)
        {
            int x = 720 - pintarx;
            int y = 400 - pintary;
            int posx = 0;
            int posy = 0;
            for (int i = 1; i <= 8; i++)
            {
                if (i * 90 >= x && (i - 1) * 90 <= x)
                {
                    posx = i;
                    break;
                }
            }
            for (int i = 1; i <= 8; i++)
            {
                if (i * 50 >= y && (i - 1) * 50 <= y)
                {
                    posy = i;
                    break;
                }
            }
            return the_game.Juego.XYaPos(posx, posy);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            AcercaDe ad = new AcercaDe();
            ad.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            the_game.Juego.InicializarEstado();
            pnlTablero.Refresh();
        }
        
        
        

        
    }
}