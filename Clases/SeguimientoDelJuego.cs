using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleShip.src
{
    class SeguimientoJuego
    {
        public EstadoJuego EstadoJuego { get; private set; }
        public EstadoTurno EstadoTurno { get; private set; }

        private readonly Jugador jugador1;
        private readonly Jugador jugador2;

        public SeguimientoJuego()
        {
            EstadoJuego = EstadoJuego.Finalizado;
            EstadoTurno = EstadoTurno.Jugador1;

            jugador1 = new Jugador("Jugador 1");
            jugador2 = new Jugador("Jugador 2");
        }

        public void IniciarJuego()
        {
            EstadoJuego = EstadoJuego.Iniciado;
            jugador1.Reiniciar();
            jugador2.Reiniciar();
            Estado();
            Console.WriteLine("¡Nuevo juego!");
        }

        public void FinalizarJuego()
        {
            EstadoJuego = EstadoJuego.Finalizado;
            Console.WriteLine("¡Fin del juego!");
        }

        public void ComprobarGanador()
        {
            Jugador? ganador = null, perdedor = null;
            if (jugador1.BarcosRestantes == 0)
            {
                ganador = jugador2;
                perdedor = jugador1;
            }
            else if (jugador2.BarcosRestantes == 0)
            {
                ganador = jugador1;
                perdedor = jugador2;
            }

            if (ganador != null && perdedor != null)
            {
                Console.WriteLine($"Todos los barcos de {perdedor.Nombre} se han hundido, ¡{ganador.Nombre} ha ganado el juego!");
                FinalizarJuego();
            }
        }

        public void AtaqueJugador(string casilla)
        {
            if (EstadoJuego != EstadoJuego.Iniciado)
            {
                Console.WriteLine("El juego no ha comenzado. Presiona [I] para iniciar un juego.");
                return;
            }

            if (!EsCasillaValida(casilla))
            {
                Console.WriteLine("Formato de casilla inválido. Por favor, introduce una casilla válida.");
                return;
            }

            int x = char.ToUpper(casilla[0]) - 65;
            int y = int.Parse(casilla[1..]);

            Jugador oponente = EstadoTurno == EstadoTurno.Jugador1 ? jugador2 : jugador1;

            if (oponente.Atacado(x, y, out string detalle))
            {
                CambiarTurno();
            }

            Estado();
            Console.WriteLine(detalle);
            ComprobarGanador();
        }

        private static bool EsCasillaValida(string casilla)
        {
            if (casilla.Length != 2)
                return false;

            char columna = char.ToUpper(casilla[0]);
            if (!int.TryParse(casilla[1..], out int fila))
                return false;

            return columna >= 'A' && columna <= 'J' && fila >= 0 && fila <= 9;
        }

        private void CambiarTurno()
        {
            EstadoTurno = EstadoTurno == EstadoTurno.Jugador1 ? EstadoTurno.Jugador2 : EstadoTurno.Jugador1;
        }

        public void Estado()
        {
            jugador1.ImprimirEstado();
            jugador2.ImprimirEstado();
            Console.WriteLine("Turno del jugador actual:  " + EstadoTurno);
        }
    }
}