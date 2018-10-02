// File:    Peon.cs
// Author:  Hendris
// Created: Lunes, 17 de Diciembre de 2007 03:33:52 p.m.
// Purpose: Definition of Class Peon

using System;
using System.Collections.Generic;

public class Peon : Pieza
{
    public Peon()
    {
    }

    public override List<Movimiento> DestinosPosibles(int pos,int distancia)
    {
        if (pos < 1 || pos > 32)
            throw new Exception("Posicion indicada no existe");
        int x = XirguGame.GetInstance().Juego.ColumnaDe(pos);
        int y = XirguGame.GetInstance().Juego.FilaDe(pos);

        List<Movimiento> rta = new List<Movimiento>();
        int nuevapos;

        if (Owner == Jugadores.blanco)
        {
            nuevapos = XirguGame.GetInstance().Juego.XYaPos(x - distancia, y + distancia);
            if (nuevapos != 0)
                rta.Add(new Movimiento(pos,nuevapos));
            nuevapos = XirguGame.GetInstance().Juego.XYaPos(x + distancia, y + distancia);
            if (nuevapos != 0)
                rta.Add(new Movimiento(pos, nuevapos));
        }
        if (Owner == Jugadores.negro)
        {
            nuevapos = XirguGame.GetInstance().Juego.XYaPos(x + distancia, y - distancia);
            if (nuevapos != 0)
                rta.Add(new Movimiento(pos, nuevapos));
            nuevapos = XirguGame.GetInstance().Juego.XYaPos(x - distancia, y - distancia);
            if (nuevapos != 0)
                rta.Add(new Movimiento(pos, nuevapos));
        }
        return rta;
    }




}