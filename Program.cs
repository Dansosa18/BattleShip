

using BattleShip.src;
using System.Text.RegularExpressions;


namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            string comando;
            bool salir = false;

            Console.WriteLine("¡BATALLA NAVAL!");
            Console.WriteLine();
            ImprimirAyuda();

            while (!salir)
            {
                Console.WriteLine("Introducir comando:");
                comando = Console.ReadLine();
                Console.WriteLine();

                if (comando == "j")
                {
                    JugarPartida();
                }
                else if (comando == "a")
                {
                    ImprimirAyuda();
                }
                else if (comando == "x")
                {
                    salir = true;
                }
                else
                {
                    Console.WriteLine("ERROR  Comando inválido");
                }

            }

            Console.WriteLine("¡Hasta luego!");
            //Console.ReadKey();
        }

        private static void JugarPartida()
        {
            string comandoJugar;
            bool detener = false;
            SeguimientoJuego juego = new SeguimientoJuego();
            ImprimirAyudaJuego();

            while (!detener)
            {
                Console.WriteLine("¿Qué deseas hacer?");
                comandoJugar = Console.ReadLine();
                Console.WriteLine();

                if (comandoJugar == "i")
                {
                    juego.IniciarJuego();
                }
                else if (comandoJugar.Length > 1 && VerificarEntradaAtaque(comandoJugar))
                {
                    juego.AtaqueJugador(comandoJugar);
                }
                else if (comandoJugar == "s")
                {
                    ImprimirAyudaJuego();
                }
                else if (comandoJugar == "u")
                {
                    detener = true;
                    Console.WriteLine("Abandonando juego");
                }
                else
                {
                    Console.WriteLine("ERROR - Comando inválido");
                }

            }

        }

        private static bool VerificarEntradaAtaque(string casilla)
        {
            Match resultado = Regex.Match(casilla, @"\b[A-J][0-9]\b");
            return resultado.Success;
        }


        private static void ImprimirAyuda()
        {
            Console.WriteLine("Menú de Ayuda de Batalla Naval");
            Console.WriteLine("j -- Iniciar partida");
            Console.WriteLine("a -- Imprimir este menú de ayuda");
            Console.WriteLine("x -- Salir");
            Console.WriteLine();
        }

        private static void ImprimirAyudaJuego()
        {
            Console.WriteLine("Menú de Ayuda de Juego de Batalla Naval");
            Console.WriteLine("i -- Iniciar o reiniciar el juego");
            Console.WriteLine("<Casilla> -- Seleccionar la casilla de ataque ej. A0 o C5");
            Console.WriteLine("a -- Imprimir este menú de ayuda");
            Console.WriteLine("x -- Detener y abandonar el juego");
            Console.WriteLine();
        }
    }
}