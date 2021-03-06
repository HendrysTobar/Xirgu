// File:    EstadoJuegoDamas.cs
// Author:  Hendris
// Created: Sábado, 15 de Diciembre de 2007 07:41:24 p.m.
// Purpose: Definition of Class EstadoJuegoDamas

using System;
using System.Collections.Generic;
using System.IO;
public class EstadoJuegoDamas
{
    private Pieza.Jugadores jugadorAMover = Pieza.Jugadores.blanco;

    public Pieza.Jugadores JugadorAMover
    {
        get { return jugadorAMover; }
        set { jugadorAMover = value; }
    }
    private int nivel;

    public int Nivel
    {
        get { return nivel; }
        set { nivel = value; }
    }
    private int utilidad;

    public int Utilidad
    {
        get { return utilidad; }        
    }
   
   public void CalcularMovimientosLegales()
   {
      //Aqui se puso bueno carajo!!!!
       Jugadas_legales = new List<Jugada>();
       Jugadas_legales.Clear();
       for (int i = 0; i < 32; i++)
       {
           if (!las_celdas[i].EsVacia())
           {
               if (las_celdas[i].la_pieza.Owner == JugadorAMover)
               {
                   jugadas_legales.AddRange(CalcularJugadasLegales(las_celdas[i].la_pieza, i + 1));
               }
           }
           
       }
       List<Jugada> jugadas_legales_caza = new List<Jugada>();
       foreach (Jugada j in jugadas_legales)
       {
           if(j.Caza)
               jugadas_legales_caza.Add(new Jugada(j));
       }
       if (jugadas_legales_caza.Count > 0)
       {
           jugadas_legales.Clear();
           jugadas_legales = jugadas_legales_caza;
       }
      
   }
  
  public List<EstadoJuegoDamas> CalcularSucesores()
   {
      
       StreamWriter sw = new StreamWriter("estados.txt", true);
       sw.WriteLine("*/*/*/*//*//**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/");
       sw.WriteLine("Hijos de:");
       sw.Write(this.ToString());
       sw.WriteLine();

       int minivel = this.Nivel;
       List<EstadoJuegoDamas> rta = new List<EstadoJuegoDamas>();
        EstadoJuegoDamas copia = new EstadoJuegoDamas(this);
       //if((object)jugadas_legales == null || jugadas_legales.Count == 0)
           CalcularMovimientosLegales();
       foreach (Jugada j in jugadas_legales)
       {
           EstadoJuegoDamas sucesor = new EstadoJuegoDamas();
           sucesor = XirguGame.GetInstance().Juego.Mover(copia, j);
           if (sucesor != null)
           {
               sucesor.JugadaHecha = j;
               sucesor.Nivel = minivel + 1;
               sw.Write(sucesor.ToString());
               rta.Add(sucesor);
           }
       }

       
       
       sw.Flush();
       sw.Close();
       return rta;
   }
   
   public EstadoJuegoDamas()
   {
       siguiente = null;
       jugadas_legales = null;
       jugadaHecha = new Jugada();
       las_celdas = new Escaque[32];
       for(int i =0 ;i<32;i++)
       {
           las_celdas[i] = new Escaque();
       }
       nivel = 0;
     
   }

   public int CalcularUtilidad()
   {
       //Que funcion de utilidad tan chimba!!!!!
       int nroRM = NumeroPiezas(Pieza.Jugadores.negro, Pieza.TiposPiezas.Reina);
       int nroRA = NumeroPiezas(Pieza.Jugadores.blanco,Pieza.TiposPiezas.Reina); 
       int nroPM = NumeroPiezas(Pieza.Jugadores.negro, Pieza.TiposPiezas.peon);
       int nroPA = NumeroPiezas(Pieza.Jugadores.blanco, Pieza.TiposPiezas.peon);
       int nroCM = NumeroCustodiadas(Pieza.Jugadores.negro);
       int nroCA = NumeroCustodiadas(Pieza.Jugadores.blanco);

       //pillela!!

       utilidad =  10 * nroRM - 10* nroRA + 5*nroPM  - 5*nroPA +  2 * nroCM - nroCA ;
       return utilidad;
   }
    public int NumeroPiezas(Pieza.Jugadores owner, Pieza.TiposPiezas tipo)
    {
        //Esto si me gustó carajo!!!!
        int n = 0;
        for (int i = 0; i < 32; i++)
        {
            if (!las_celdas[i].EsVacia())
            {
                if (las_celdas[i].la_pieza.Owner == owner && las_celdas[i].la_pieza.Tipo == tipo)
                {
                    n++;
                }
            }
        }
        return n;
    }

    private int NumeroCustodiadas(Pieza.Jugadores owner)
    {
        int n = 0;
        
        for (int i = 0; i < 32; i++)
        {
            if (!las_celdas[i].EsVacia())
            {
                if (las_celdas[i].la_pieza.Owner == owner)
                {
                    if(EstaCustodiada(owner, i+1) || EstaEnUnBorde(i+1))
                        n++;
                }
            }
        }
        return n;
    }
    private bool EstaEnUnBorde(int pos)
    {
        int f = XirguGame.GetInstance().Juego.FilaDe(pos);
        int c = XirguGame.GetInstance().Juego.ColumnaDe(pos);

        if (f == 1 || f == 8)
            return true;
        if (c == 1 || c == 8)
            return true;
        return false;
        
    }

    private bool EstaCustodiada(Pieza.Jugadores owner,int pos)
    {
        List<int> ady = PosAdyacentes(pos);
        List<int> amigas = new List<int>();
        foreach (int celda in ady)
        {
            if (!las_celdas[celda - 1].EsVacia())
            {
                if (las_celdas[celda - 1].la_pieza.Owner == owner)
                {
                    amigas.Add(celda);
                }
            }
        }
        if (amigas.Count >= 2)
        {
            foreach (int amiga in amigas)
                foreach (int amiga2 in amigas)
                    if (XirguGame.GetInstance().Juego.FilaDe(amiga) == XirguGame.GetInstance().Juego.FilaDe(amiga2) ||
                        XirguGame.GetInstance().Juego.ColumnaDe(amiga) == XirguGame.GetInstance().Juego.ColumnaDe(amiga2))
                        return true;
 
        }
        return false;
    }

    private List<int> PosAdyacentes(int pos)
    {
        List<int> ady = new List<int>();
        if (pos < 1 || pos > 32)
            return null;
        int x = XirguGame.GetInstance().Juego.ColumnaDe(pos);
        int y = XirguGame.GetInstance().Juego.FilaDe(pos);
        int posadyacente;
        posadyacente = XirguGame.GetInstance().Juego.XYaPos(x - 1, y + 1);
        if(posadyacente != 0)
            ady.Add(posadyacente);
        posadyacente = XirguGame.GetInstance().Juego.XYaPos(x - 1, y - 1);
        if (posadyacente != 0)
            ady.Add(posadyacente);
        posadyacente = XirguGame.GetInstance().Juego.XYaPos(x + 1, y + 1);
        if (posadyacente != 0)
            ady.Add(posadyacente);
        posadyacente = XirguGame.GetInstance().Juego.XYaPos(x + 1, y - 1);
        if (posadyacente != 0)
            ady.Add(posadyacente);
        return ady;

    }


    public System.Collections.Generic.List<Jugada> jugadas_legales;
   
   /// <summary>
   /// Property for collection of Jugada
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Jugada> Jugadas_legales
   {
      get
      {
         if (jugadas_legales == null)
            jugadas_legales = new System.Collections.Generic.List<Jugada>();
         return jugadas_legales;
      }
      set
      {
         RemoveAllJugadas_legales();
         if (value != null)
         {
            foreach (Jugada oJugada in value)
               AddJugadas_legales(oJugada);
         }
      }
   }
   
   /// <summary>
   /// Add a new Jugada in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddJugadas_legales(Jugada newJugada)
   {
      if (newJugada == null)
         return;
      if (this.jugadas_legales == null)
         this.jugadas_legales = new System.Collections.Generic.List<Jugada>();
      if (!this.jugadas_legales.Contains(newJugada))
         this.jugadas_legales.Add(newJugada);
   }
   
   /// <summary>
   /// Remove an existing Jugada from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveJugadas_legales(Jugada oldJugada)
   {
      if (oldJugada == null)
         return;
      if (this.jugadas_legales != null)
         if (this.jugadas_legales.Contains(oldJugada))
            this.jugadas_legales.Remove(oldJugada);
   }
   
   /// <summary>
   /// Remove all instances of Jugada from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllJugadas_legales()
   {
      if (jugadas_legales != null)
         jugadas_legales.Clear();
   }
   private Escaque[] las_celdas;

   public Escaque[] Las_celdas
   {
       get { return las_celdas; }
       set { las_celdas = value; }
   }
   private Jugada jugadaHecha;

   public Jugada JugadaHecha
   {
       get { return jugadaHecha; }
       set { jugadaHecha = value; }
   }

    public List<Jugada> CalcularJugadasLegales(Pieza p, int pos)
    {
        List<Jugada> rta = new List<Jugada>();
        List<Jugada> rtacaza = new List<Jugada>();
        List<Jugada> jugadasPosibles = new List<Jugada>();
        List<Movimiento> movPosibles = new List<Movimiento>();
        List<Movimiento> movPosiblesCaza = new List<Movimiento>();
        movPosibles = p.DestinosPosibles(pos,1);
        movPosiblesCaza = p.DestinosPosibles(pos,2);

        foreach (Movimiento mov in movPosibles)
        {
            Jugada j = new Jugada();
            j.AddLos_movs(mov);
            jugadasPosibles.Add(j);           
        }


        foreach (Jugada jugp in jugadasPosibles)
        {
            foreach (Movimiento m in jugp.Los_movs)
            {
                if (EsLegal(m) && !rta.Contains(jugp))
                    rta.Add(jugp);
            }
        }
        jugadasPosibles.Clear();


        foreach (Movimiento mov in movPosiblesCaza)
        {
            Jugada j = new Jugada();
            j.Caza = true;
            j.AddLos_movs(mov);
            jugadasPosibles.Add(j);
        }
        
        foreach (Jugada jugp in jugadasPosibles)
        {
            foreach (Movimiento m in jugp.Los_movs)
            {
                if (EsLegalCaza(m) && !rtacaza.Contains(jugp))
                    rtacaza.Add(jugp);
            }
        }
        
        rta.AddRange(rtacaza);
        return rta;
    }

    public bool EsLegal(Movimiento m)
    {
        if (las_celdas[m.Destino-1].EsVacia())
            return true;
        return false;
    }
    public bool EsLegalCaza(Movimiento m)
    {
        
        if (!EsLegal(m))
            return false;
        int posacazar = m.PosACazar();
               
        if (posacazar == m.Origen)
            return false;
        if (posacazar != 0)
        {
            if (!las_celdas[posacazar - 1].EsVacia() && !las_celdas[m.Origen-1].EsVacia())
            {
                if (las_celdas[posacazar - 1].la_pieza.Owner != las_celdas[m.Origen - 1].la_pieza.Owner)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    private EstadoJuegoDamas siguiente;

    public EstadoJuegoDamas Siguiente
    {
        get { return siguiente; }
        set { siguiente = value; }
    }


    public EstadoJuegoDamas(EstadoJuegoDamas old)
    {
        if ((object)old.jugadaHecha != null)
        {
            this.jugadaHecha = new Jugada(old.jugadaHecha);
        }
        this.Jugadas_legales = new List<Jugada>();
        Jugada nuevaj;
        foreach (Jugada j in old.Jugadas_legales)
        {
            nuevaj = new Jugada(j);
            this.Jugadas_legales.Add(nuevaj);
        }

        this.JugadorAMover = new Pieza.Jugadores();
        this.JugadorAMover = old.JugadorAMover;
        this.las_celdas = new Escaque[32];
        
        for (int i = 0; i < old.las_celdas.Length; i++)
        {
            this.las_celdas[i] =  new Escaque(old.las_celdas[i]);                       
        }
        this.Nivel = old.Nivel;
        if(siguiente != null)
            this.siguiente = new EstadoJuegoDamas(old.siguiente);
        
        
            
        

    }

    public override string ToString()
    {
        String rta="";
        int fila = 0;
        int k = 0;
        for (int i = 31; i >= 0; i--)
        {
            if (k == 0 && fila % 2 != 0)
                rta+="   ";
            rta += " ";
            if (!las_celdas[i].EsVacia())
            {
                if (las_celdas[i].la_pieza.Tipo == Pieza.TiposPiezas.peon)
                {
                    if (las_celdas[i].la_pieza.Owner == Pieza.Jugadores.blanco)
                    {
                        rta += "b";
                    }
                    if (las_celdas[i].la_pieza.Owner == Pieza.Jugadores.negro)
                    {
                        rta += "n";
                    }
                }
            }
            else
            {
                rta += "█";
            }
            rta += " ";
            if (k < 4)
            {
                rta+="   ";
                k++;
            }
            if (k == 4)
            {
                if (fila % 2 == 0)
                    rta += "   \r\n";
                else
                    rta += "\r\n";
                fila++;
                k = 0;
            }

        }
        rta += "Utilidad: " + CalcularUtilidad()+"\r\n";
        return rta;
    }
}