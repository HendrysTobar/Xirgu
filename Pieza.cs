// File:    Pieza.cs
// Author:  Hendris
// Created: Sábado, 15 de Diciembre de 2007 07:42:23 p.m.
// Purpose: Definition of Class Pieza

using System;
using System.Collections.Generic;

public abstract class Pieza
{
    private TiposPiezas tipo;
    
    public TiposPiezas Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }
   public  enum Jugadores
    {
       blanco = 1,
       negro = -1,
       vacio = 0,       
    }
    public enum TiposPiezas
    {
        Reina = 1,
        peon = 0,        
    }
    private Jugadores owner;

    public Jugadores Owner
    {
        get { return owner; }
        set { owner = value; }
    }
   
   public Pieza()
   {
      owner = Jugadores.vacio;
      tipo = TiposPiezas.peon;      
   }
    /// <summary>
    /// Factory Method
    /// </summary>
    /// <param name="tipo">es el tipo de pieza, peon o reina del enum Jugadores</param>
    /// <returns>retorna un objeto de las subclases peon o reina dependiendo del tipo</returns>

    public static Pieza HacerPieza(TiposPiezas t)
    {
        Pieza rta;
        if (t == TiposPiezas.peon)
        {
            rta = new Peon();
            rta.tipo = t;
            return rta;
        }
        if (t == TiposPiezas.Reina)
        {
            rta = new Reina();
            rta.tipo = t;
            return rta;
        }
        return null;
    }


    public abstract List<Movimiento> DestinosPosibles(int pos,int distancia);

    public Pieza(Pieza old)
    {
        this.Owner = old.Owner;
        this.Tipo = old.Tipo;        
    }
   

}