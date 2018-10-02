// File:    Reina.cs
// Author:  Hendris
// Created: Lunes, 17 de Diciembre de 2007 03:33:53 p.m.
// Purpose: Definition of Class Reina

using System;
using System.Collections.Generic;

public class Reina : Pieza
{
    public Reina()
    {
    }
   public override List<Movimiento> DestinosPosibles(int pos,int distancia)
   {
       if (pos < 1 || pos > 32)
           throw new Exception("Posicion indicada no existe");

       List<Movimiento> rta =  new List<Movimiento>();
       int nuevapos;
       int x = XirguGame.GetInstance().Juego.ColumnaDe(pos);
       int y = XirguGame.GetInstance().Juego.FilaDe(pos);

       nuevapos = XirguGame.GetInstance().Juego.XYaPos(x - distancia, y + distancia);
       if (nuevapos != 0)
           rta.Add(new Movimiento(pos, nuevapos));
       nuevapos = XirguGame.GetInstance().Juego.XYaPos(x + distancia, y + distancia);
       if (nuevapos != 0)
           rta.Add(new Movimiento(pos, nuevapos));
       nuevapos = XirguGame.GetInstance().Juego.XYaPos(x + distancia, y - distancia);
       if (nuevapos != 0)
           rta.Add(new Movimiento(pos, nuevapos));
       nuevapos = XirguGame.GetInstance().Juego.XYaPos(x - distancia, y - distancia);
       if (nuevapos != 0)
           rta.Add(new Movimiento(pos, nuevapos));
       return rta;
   }

}