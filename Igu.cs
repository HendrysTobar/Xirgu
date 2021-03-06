// File:    Igu.cs
// Author:  Hendris
// Created: Domingo, 16 de Diciembre de 2007 01:24:50 p.m.
// Purpose: Definition of Class Igu

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class Igu
{
    private Jugada jugada;
    private static Igu instancia;
    public Jugada Jugada
    {
        get { return jugada; }
        set { jugada = value; }
    }
    private char[] tablero;

    public char[] Tablero
    {
        get { return tablero; }
        set { tablero = value; }
    }
   
   private Igu()
   {
       jugada = new Jugada();
       tablero = new char[32];
   }

    public static Igu GetInstance()
    {
        if(instancia == null)
        {
            instancia = new Igu();           
        }
        return instancia;
    }

   public void ObtenerJugada(List<int> posiciones)
   {
       Queue<int> pasos = new Queue<int>();
       foreach (int pos in posiciones)
       {
           pasos.Enqueue(pos);         
       }
       Movimiento m = new Movimiento();
       
       m.Origen = pasos.Dequeue();
       m.Destino = pasos.Dequeue();
       jugada.AddLos_movs(m);
       while (pasos.Count != 0)
       {
           m.Origen = m.Destino;
           m.Destino = pasos.Dequeue();
           jugada.AddLos_movs(m);
       }
       
   }

    public void ActualizarTablero()
    {
        EstadoJuegoDamas estado = new EstadoJuegoDamas();
        estado = XirguGame.GetInstance().Juego.EstadoActual;
        Pieza.Jugadores o = new Pieza.Jugadores();
        Pieza.TiposPiezas t = new Pieza.TiposPiezas();
        for(int i =0;i<32;i++)
        {
            if (!estado.Las_celdas[i].EsVacia())
            {
                o = estado.Las_celdas[i].la_pieza.Owner;
                t = estado.Las_celdas[i].la_pieza.Tipo;
                if (o == Pieza.Jugadores.blanco && t == Pieza.TiposPiezas.peon)
                    tablero[i] = 'b';
                if (o == Pieza.Jugadores.negro && t == Pieza.TiposPiezas.peon)
                    tablero[i] = 'n';

                if (o == Pieza.Jugadores.blanco && t == Pieza.TiposPiezas.Reina)
                    tablero[i] = 'B';
                if (o == Pieza.Jugadores.negro && t == Pieza.TiposPiezas.Reina)
                    tablero[i] = 'N';
            }
            else
                tablero[i] = '█';

            
        }
    }

}