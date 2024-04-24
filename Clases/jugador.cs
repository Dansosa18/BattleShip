
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.src
{
    class Jugador
    {
        public string Nombre { get; set; }
        public char[,] Tablero { get; set; }
        public List<Barco> Barcos { get; set; }
        public int BarcosRestantes { get; set; }

        // configuración del tablero
        private readonly int anchoTablero = 10;
        private readonly int altoTablero = 10;

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Tablero = new char[anchoTablero, altoTablero];
            PoblarTablero();
            Barcos = [];
            ColocarBarcos();
            BarcosRestantes = Barcos.Count;
        }

        public void Reiniciar()
        {
            PoblarTablero();
            ColocarBarcos();
            BarcosRestantes = Barcos.Count;
        }

        public void PoblarTablero()
        {
            for (int i = 0; i < anchoTablero; i++)
            {
                for (int j = 0; j < altoTablero; j++)
                {
                    Tablero[j, i] = 'O';
                }
            }
        }

        public void ImprimirTablero()
        {
            Console.WriteLine("  A B C D E F G H I J");

            for (int i = 0; i < anchoTablero; i++)
            {
                Console.Write(i);
                for (int j = 0; j < altoTablero; j++)
                {
                    Console.Write(" " + Tablero[j, i]);
                }
                Console.WriteLine();
            }
        }

        public void ColocarBarcos()
        {
            Barcos.Clear();
            Barcos.Add(new Barco("fragata", 3, Alineacion.Horizontal));
            Barcos.Add(new Barco("galeon", 3, Alineacion.Vertical));
            Barcos.Add(new Barco("navio", 3, Alineacion.Horizontal));

            Barcos[0].Posicion[0] = new int[] { 3, 3 }; // D3
            Barcos[0].Posicion[1] = new int[] { 4, 3 }; // E3
            Barcos[0].Posicion[2] = new int[] { 5, 3 }; // F3

            Barcos[1].Posicion[0] = new int[] { 7, 4 }; // H4
            Barcos[1].Posicion[1] = new int[] { 7, 5 }; // H5
            Barcos[1].Posicion[2] = new int[] { 7, 6 }; // H6

            Barcos[2].Posicion[0] = new int[] { 1, 1 }; // B1
            Barcos[2].Posicion[1] = new int[] { 2, 1 }; // C1
            Barcos[2].Posicion[2] = new int[] { 3, 1 }; // D1
        }

        public bool Atacado(int x, int y, out string detalle)
        {
            if (Tablero[x, y] == 'O')
            {
                if (ComprobarImpactoBarco(x, y, out int indice))
                {
                    DestruirBarco(indice);
                    detalle = "¡Ataque exitoso! ¡Un barco ha sido destruido!";
                }
                else
                {
                    Tablero[x, y] = 'X';
                    detalle = "¡Ataque exitoso! Pero no se ha impactado ningún barco.";
                }
                return true;
            }

            detalle = "¡Ataque fallido! Esta casilla ya ha sido atacada anteriormente. Elige otra casilla.";
            return false;
        }

        public bool ComprobarImpactoBarco(int x, int y, out int indice)
        {
            indice = 0;
            foreach (Barco barco in Barcos)
            {
                foreach (int[] pos in barco.Posicion)
                {
                    if (pos[0] == x && pos[1] == y)
                    {
                        return true;
                    }
                }
                indice++;
            }
            return false;
        }

        public void DestruirBarco(int indice)
        {
            Barco barco = Barcos[indice];
            foreach (int[] pos in barco.Posicion)
            {
                Tablero[pos[0], pos[1]] = 'X';
            }
            Barcos.RemoveAt(indice);
            BarcosRestantes = Barcos.Count;
        }

        public void ImprimirEstado()
        {
            Console.WriteLine(Nombre);
            Console.WriteLine("Barcos restantes: " + BarcosRestantes);
            Console.WriteLine("Tablero:");
            ImprimirTablero();
            Console.WriteLine();
        }
    }
}