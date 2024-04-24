using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.src
{
    class Barco(string nombre, int tamaño, Alineacion alineación)
    {
        public string Nombre { get; set; } = nombre;
        public int Tamaño { get; set; } = tamaño;
        public int[][] Posicion { get; set; } = new int[tamaño][];
        public Alineacion Alineación { get; set; } = alineación;
        public bool Destruido { get; set; } = false;
    }
}