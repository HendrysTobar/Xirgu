// File:    Escaque.cs
// Author:  Hendris
// Created: Sábado, 15 de Diciembre de 2007 10:24:10 p.m.
// Purpose: Definition of Class Escaque

using System;

public class Escaque
{
   public Escaque()
   {
      la_pieza = null;
   }
   
   public bool EsVacia()
   {
      return la_pieza == null || la_pieza.Owner == Pieza.Jugadores.vacio;
   }
   
   public Pieza la_pieza;
    public Escaque(Escaque old)
    {
        if (!old.EsVacia())
        {
            this.la_pieza = Pieza.HacerPieza(old.la_pieza.Tipo);
            la_pieza.Owner = old.la_pieza.Owner;
            la_pieza.Tipo = old.la_pieza.Tipo;
        }
        else
            this.la_pieza = null;
    }

}