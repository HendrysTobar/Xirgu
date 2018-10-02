// File:    Jugada.cs
// Author:  Hendris
// Created: Sábado, 15 de Diciembre de 2007 09:05:03 p.m.
// Purpose: Definition of Class Jugada

using System;
using System.Collections.Generic;

public class Jugada
{
   public Jugada()
   {
       los_movs = new List<Movimiento>();
       caza = false;
   }
   
   public System.Collections.Generic.List<Movimiento> los_movs;
   
   /// <summary>
   /// Property for collection of Movimiento
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Movimiento> Los_movs
   {
      get
      {
         if (los_movs == null)
            los_movs = new System.Collections.Generic.List<Movimiento>();
         return los_movs;
      }
      set
      {
         RemoveAllLos_movs();
         if (value != null)
         {
            foreach (Movimiento oMovimiento in value)
               AddLos_movs(oMovimiento);
         }
      }
   }
   
   /// <summary>
   /// Add a new Movimiento in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddLos_movs(Movimiento newMovimiento)
   {
      if (newMovimiento == null)
         return;
      if (this.los_movs == null)
         this.los_movs = new System.Collections.Generic.List<Movimiento>();
      if (!this.los_movs.Contains(newMovimiento))
         this.los_movs.Add(newMovimiento);
   }
   
   /// <summary>
   /// Remove an existing Movimiento from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveLos_movs(Movimiento oldMovimiento)
   {
      if (oldMovimiento == null)
         return;
      if (this.los_movs != null)
         if (this.los_movs.Contains(oldMovimiento))
            this.los_movs.Remove(oldMovimiento);
   }
   
   /// <summary>
   /// Remove all instances of Movimiento from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllLos_movs()
   {
      if (los_movs != null)
         los_movs.Clear();
   }

    public static bool operator ==(Jugada uno, Jugada otro)
    {
        
        if ((object)uno == null || (object)otro == null)
            return false;
        if (uno.los_movs.Count != otro.Los_movs.Count)
            return false;
        for (int i = 0; i < uno.los_movs.Count; i++)
            if (uno.los_movs[i] != otro.los_movs[i])
                return false;
        return true;
        
    }
    public static bool operator !=(Jugada uno, Jugada otro)
    {
        return !(uno == otro);
    }
    private bool caza;

    public bool Caza
    {
        get { return caza; }
        set { caza = value; }
    }
    public Jugada(Jugada old)
    {
        Movimiento nuevo;
        foreach(Movimiento m in old.Los_movs)
        {
            nuevo = new Movimiento(m);
            this.AddLos_movs(nuevo);
        }
        this.caza = old.caza;
    }



}