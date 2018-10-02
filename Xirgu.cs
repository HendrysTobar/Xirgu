// File:    Xirgu.cs
// Author:  Hendris
// Created: Domingo, 16 de Diciembre de 2007 01:44:35 p.m.
// Purpose: Definition of Class Xirgu

using System;
using System.Collections.Generic;

public class XirguGame
{
   private static XirguGame instancia;

    private XirguGame()
    {
        juego = new Juego();
    }
   public void Run()
   {

               
        
   }
    public void HacerJugadaMaquina()
    {
        juego.HacerMovAlfaBeta();
    }
    public bool HacerJugadaDeJugador()
    {
        List<Jugada> jl = new List<Jugada>();
        Jugada jugada_player = new Jugada();

        jugada_player = Igu.GetInstance().Jugada;
        jl = juego.ObtenerJugadasLegalesEstadoActual();
        bool sehizo = false;
        foreach (Jugada j in jl)
        {

            if (j == jugada_player)
            {
                juego.Mover(jugada_player);
                sehizo = true;
               
                break;
            }
        }
        if (!sehizo)
        {
            jugada_player.RemoveAllLos_movs();
            return false;
        }

        jugada_player.RemoveAllLos_movs();
        juego.ObtenerJugadasLegalesEstadoActual();
        return true;
    }

   
   public static XirguGame GetInstance()
   {
       if (instancia == null)
           return (instancia = new XirguGame());
       else
           return instancia;
   }

   private Juego juego;

   public Juego Juego
   {
       get { return juego; }
       set { juego = value; }
   }

    public int posAdyacente(int pos, Movimiento.Sentido s, Movimiento.Direccion d)
    {
        int x = Juego.ColumnaDe(pos);
        int y = Juego.FilaDe(pos);
        if (pos < 1 || pos > 32)
            return 0;
        if (s == Movimiento.Sentido.arriba)
        {
            if (d == Movimiento.Direccion.slash)
            {
                return Juego.XYaPos(x-1, y+1);                
            }
            if (d == Movimiento.Direccion.backslash)
            {
                return Juego.XYaPos(x + 1, y + 1);                
            }
        }
        if (s == Movimiento.Sentido.abajo)
        {
            if (d == Movimiento.Direccion.slash)
            {
                return Juego.XYaPos(x + 1, y - 1);                
            }
            if (d == Movimiento.Direccion.backslash)
            {
                return Juego.XYaPos(x - 1, y - 1);                
            }
        }
        return 0;
    }

}