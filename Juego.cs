// File:    Juego.cs
// Author:  Hendris
// Created: Sábado, 15 de Diciembre de 2007 05:06:53 p.m.
// Purpose: Definition of Class Juego

using System;
using System.IO;
using System.Collections.Generic;

public class Juego
{
   private int nivel;
    
   
   public EstadoJuegoDamas GetState()
   {
      return estadoActual;
   }

   public int ComputarUtilidad()
   {
       return ComputarUtilidad(estadoActual);
   }
   public int ComputarUtilidad(EstadoJuegoDamas estado)
   {
       return estado.CalcularUtilidad();
   }
   
   public bool EsTerminal(EstadoJuegoDamas estado)
   {
      
       return HaTerminado() || estado.Nivel == nivel;
   }
   
   public Juego()
   {
       estado_inicial = new EstadoJuegoDamas();
       estadoActual = new EstadoJuegoDamas();
       InicializarEstado();
       nivel = 5;
   }
   
   public bool HaTerminado()
   {
       estadoActual.CalcularMovimientosLegales();
       return estadoActual.jugadas_legales.Count == 0;      
   }
    public Pieza.Jugadores QuienGano()
    {
        if (HaTerminado())
        {
            if (estadoActual.JugadorAMover == Pieza.Jugadores.blanco)
                return Pieza.Jugadores.negro;
            else
                return Pieza.Jugadores.negro;
        }
        return Pieza.Jugadores.vacio;
    }
   
   public int MinValue(EstadoJuegoDamas estado, AlfaBeta ab)
   {
       int v = int.MaxValue;
       if (EsTerminal(estado))
       {
           return ComputarUtilidad(estado);
       }
       else
       {
           List<EstadoJuegoDamas> successorList = estado.CalcularSucesores();
           foreach (EstadoJuegoDamas successor in successorList)
           {
               int maximumValueOfSuccessor = MaxValue(successor, ab.Copy());
               if (maximumValueOfSuccessor < v)
               {
                   v = maximumValueOfSuccessor;
                   estado.Siguiente = successor;
               }
               //Si β <= α   retorne β... esta es una parte importante de la poda 
               if (v <= ab.Alfa)
               {
                   //Poda desde min
                   return v;
               }
               ab.Beta = Math.Min(ab.Beta, v);
           }
           return v;
       }
   }
   
   public int MaxValue(EstadoJuegoDamas estado, AlfaBeta ab)
   {
       int v = int.MinValue;
       if (EsTerminal(estado))
       {
           return ComputarUtilidad(estado);
       }
       else
       {
           List<EstadoJuegoDamas> successorList = estado.CalcularSucesores();
           foreach (EstadoJuegoDamas successor in successorList)
           {
               int minimumValueOfSuccessor = MinValue(successor, ab.Copy());
               if (minimumValueOfSuccessor > v)
               {
                   v = minimumValueOfSuccessor;
                   estado.Siguiente = successor;
               }
               //Si α >= β retorne α... esta es una parte importante de la poda 
               if (v >= ab.Beta)
               {
                   //Poda desde max;
                   return v;
               }
               ab.Alfa = Math.Max(ab.Alfa, v); 
           }
           return v;
       }


   }
   
   public void HacerMovAlfaBeta()
   {
       File.Delete("estados.txt");
       getAlfaBetaValue(estadoActual);
       EstadoJuegoDamas nextState = estadoActual.Siguiente;
       if(nextState == null)
       {
           throw new  Exception("Movimiento Alfa-Beta Fallido!!");
       }
       Mover(nextState.JugadaHecha);
   }
    public int getAlfaBetaValue(EstadoJuegoDamas estado)
    {
        return MaxValue(estado, new AlfaBeta());
    }
   public void Mover(Jugada jug)
   {
       
       estadoActual = Mover(EstadoActual, jug);
       estadoActual.Nivel = 0;
   }
    public EstadoJuegoDamas Mover(EstadoJuegoDamas estado, Jugada j)
    {
        EstadoJuegoDamas estAuxiliar = new EstadoJuegoDamas(estado);
        int posfinal = 0;
        foreach (Movimiento m in j.Los_movs)
        {
            if (estAuxiliar.EsLegalCaza(m))
            {
                int pac = m.PosACazar();
                estAuxiliar.Las_celdas[pac - 1].la_pieza = null;
            }
            estAuxiliar.Las_celdas[m.Destino - 1].la_pieza = estAuxiliar.Las_celdas[m.Origen - 1].la_pieza;
            estAuxiliar.Las_celdas[m.Origen - 1].la_pieza = null;
            posfinal = m.Destino;      
        }
        estAuxiliar.JugadaHecha = j;
        //Convertir en reina

        Pieza.Jugadores color;
        if (estAuxiliar.Las_celdas[posfinal - 1].la_pieza.Owner == Pieza.Jugadores.blanco &&
            FilaDe(posfinal) == 8 &&
            estAuxiliar.Las_celdas[posfinal - 1].la_pieza.Tipo == Pieza.TiposPiezas.peon
            )
        {
            color = estAuxiliar.Las_celdas[posfinal - 1].la_pieza.Owner;
            estAuxiliar.Las_celdas[posfinal - 1].la_pieza = Pieza.HacerPieza(Pieza.TiposPiezas.Reina);
            estAuxiliar.Las_celdas[posfinal-1].la_pieza.Owner = color;
        }
        else
        {
            if (estAuxiliar.Las_celdas[posfinal - 1].la_pieza.Owner == Pieza.Jugadores.negro &&
            FilaDe(posfinal) == 1 &&
            estAuxiliar.Las_celdas[posfinal - 1].la_pieza.Tipo == Pieza.TiposPiezas.peon
            )
            {
                color = estAuxiliar.Las_celdas[posfinal - 1].la_pieza.Owner;
                estAuxiliar.Las_celdas[posfinal - 1].la_pieza = Pieza.HacerPieza(Pieza.TiposPiezas.Reina);
                estAuxiliar.Las_celdas[posfinal-1].la_pieza.Owner = color;
            }
        }

            




        if (estAuxiliar.JugadorAMover == Pieza.Jugadores.blanco)
            estAuxiliar.JugadorAMover = Pieza.Jugadores.negro;
        else
            estAuxiliar.JugadorAMover = Pieza.Jugadores.blanco;

        return estAuxiliar;
    }

   private EstadoJuegoDamas estadoActual;

   public EstadoJuegoDamas EstadoActual
   {
       get { return estadoActual; }
       set { estadoActual = value; }
   }
   public EstadoJuegoDamas estado_inicial;

    public void InicializarEstado()
    {
        estado_inicial.Nivel = 0;
        estado_inicial.JugadorAMover = Pieza.Jugadores.blanco;
        for (int i = 0; i < 12; i++)
        {
            estado_inicial.Las_celdas[i].la_pieza = Pieza.HacerPieza(Pieza.TiposPiezas.peon);
            estado_inicial.Las_celdas[i].la_pieza.Owner = Pieza.Jugadores.blanco;
        }
        for (int i = 20; i < 32; i++)
        {
            estado_inicial.Las_celdas[i].la_pieza = Pieza.HacerPieza(Pieza.TiposPiezas.peon);
            estado_inicial.Las_celdas[i].la_pieza.Owner = Pieza.Jugadores.negro;
        }
        estadoActual = estado_inicial;

    }

    public List<Jugada> ObtenerJugadasLegalesEstadoActual()
    {
        return ObtenerJugadasLegales(estadoActual);
    }

    public List<Jugada> ObtenerJugadasLegales(EstadoJuegoDamas estado)
    {
        EstadoJuegoDamas e = new EstadoJuegoDamas();
        e = estado;
        e.CalcularMovimientosLegales();
        return e.Jugadas_legales;
    }

    public int FilaDe(int pos)
    {
        if (pos < 1 || pos > 32)
            return 0;
        if (pos % 4 == 0)
            return pos / 4;
        else
            return pos / 4 + 1;
    }

    public int ColumnaDe(int pos)
    {
        if (pos < 1 || pos > 32)
            return 0;
        int c = 1;
        int acum = 1;

        while (acum < pos)
        {
            c += 2;
            if (c > 8)
            {
                if (FilaDe(acum) % 2 != 0)
                {
                    c = 2;
                }
                else
                    c = 1;
                                
            }
            acum++;
        }
        return c;        
    }

    public int XYaPos(int x, int y)
    {
        int acum = 1;
        if (x % 2 == 0 && y % 2 != 0)
            return 0;
        if (x % 2 != 0 && y % 2 == 0)
            return 0;
        if (x < 1 || y < 1 || x > 8 || y > 8)
            return 0;
        while (FilaDe(acum) != y || ColumnaDe(acum) != x)
        {
            acum++;
        }
        return acum;
        
    }



    
}