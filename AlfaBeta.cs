// File:    AlfaBeta.cs
// Author:  Hendris
// Created: Sábado, 15 de Diciembre de 2007 05:05:54 p.m.
// Purpose: Definition of Class AlfaBeta

using System;

public class AlfaBeta
{
   private int alfa;
   private int beta;
    public AlfaBeta()
    {
        alfa = int.MinValue;
        beta = int.MaxValue;
    }
   public int Alfa
   {
      get
      {
         return alfa;
      }
      set
      {
         this.alfa = value;
      }
   }
   
   public int Beta
   {
      get
      {
         return beta;
      }
      set
      {
         this.beta = value;
      }
   }
    public AlfaBeta Copy()
    {
        AlfaBeta copia = new AlfaBeta();
        copia.alfa = this.alfa;
        copia.beta = this.beta;
        return copia;        
    }

}