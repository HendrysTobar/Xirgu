// File:    Movimiento.cs
// Author:  Hendris
// Created: Sábado, 15 de Diciembre de 2007 11:54:46 p.m.
// Purpose: Definition of Class Movimiento

using System;

public class Movimiento
{
    private int origen;

    public int Origen
    {
        get { return origen; }
        set { origen = value; AcomodarSentido();AcomodarDireccion();  }
    }
    private int destino;

    public int Destino
    {
        get { return destino; }
        set { destino = value; AcomodarSentido(); AcomodarDireccion(); }
    }

    
   public Movimiento(int or, int dest)
   {
       origen = or;
       destino = dest;
       AcomodarSentido();
       AcomodarDireccion();
       
   }
    public Movimiento()
    {
        origen = 0;
        destino = 0;
        AcomodarSentido();
        AcomodarDireccion();
    }

    public static bool operator ==(Movimiento uno, Movimiento otro)
    {
        if((object)uno == null || (object)otro == null)
            return false;
        return uno.origen == otro.origen && uno.Destino == otro.destino;
            
        
    }
    public static bool operator !=(Movimiento uno, Movimiento otro)
    {
        return !(uno == otro);
    }
    public Movimiento(Movimiento old)
    {
        this.Destino = old.Destino;
        this.Origen = old.Origen;
        AcomodarSentido();
        AcomodarDireccion();
        
    }
    private Movimiento.Sentido sent;
    private Movimiento.Direccion dir;

    public enum Sentido
    {
        arriba,
        abajo,        
    }

    public enum Direccion
    {
        slash,
        backslash
    }
    public Movimiento.Direccion opuesto(Movimiento.Direccion d)
    {
        if (d == Movimiento.Direccion.slash)
            return Movimiento.Direccion.backslash;
        else
            return Movimiento.Direccion.slash;
    }
    public Movimiento.Sentido opuesto(Movimiento.Sentido s)
    {
        if (s == Movimiento.Sentido.arriba)
            return Movimiento.Sentido.abajo;
        else
            return Movimiento.Sentido.arriba;
    }
    public int PosACazar()
    {
        Movimiento.Sentido sopuesta = opuesto(this.sent);
        return XirguGame.GetInstance().posAdyacente(this.destino,sopuesta, this.dir);
    }
    private void AcomodarDireccion()
    {
        int co = XirguGame.GetInstance().Juego.ColumnaDe(origen);
        int cd = XirguGame.GetInstance().Juego.ColumnaDe(destino);
        if (cd < co)
        {
            if (sent == Movimiento.Sentido.arriba)
            {
                this.dir = Direccion.slash;
            }
            else
            {
                this.dir = Direccion.backslash;
            }
        }
        if (cd >= co)
        {
            if (sent == Movimiento.Sentido.arriba)
            {
                this.dir = Direccion.backslash;
            }
            else
            {
                this.dir = Direccion.slash;
            }
        }


            
    }
    private void AcomodarSentido()
    {
        int fo = XirguGame.GetInstance().Juego.FilaDe(origen);
        int fd = XirguGame.GetInstance().Juego.FilaDe(destino);
        if (fd < fo)
            this.sent = Sentido.abajo;
        else
            this.sent = Sentido.arriba;
    }

}