using System;
using System.Collections.Generic;
using System.Threading;

namespace PochodSmrti
{
	class MainClass
	{
		public static int sila = 5;
		public static int moralka = 5;
		private static bool gameIsOn = false;

		public static void Main(string[] args)
		{
			var lanes = new List<string> {"", "", ""};

			var rnd = new Random();
			for (int j = 0; j < 5000; j++)
			{
				if (rnd.NextDouble() < 0.003)
				{
					// large obstacle
					lanes[0] += "o";
					lanes[1] += "o";
					lanes[2] += "o";

				}

				if (rnd.NextDouble() < 0.02)
				{
					// soldier
					lanes[0] += "s";
					lanes[1] += "s";
					lanes[2] += "s";
				}

				for (int i = 0; i < 3; i++) {
					if (rnd.NextDouble() < 0.01) {
						// obstacle
						lanes[i] += "o";
					} else if (rnd.NextDouble() < 0.015) {
						// food
						lanes[i] += "f";
					} else if (rnd.NextDouble() < 0.02) {
						// body
						lanes[i] += "b";
					}  else {
						lanes[i] += ".";
					}
				}
			}


			ConsoleKeyInfo keyInfo;
			int offset = 0;
			int currentLane = 0;
		
			DrawHelp();
			Console.WriteLine("Pro START hry zmackni S");
			Console.WriteLine();
			if (Console.ReadKey(true).KeyChar == 's') {
				gameIsOn = true;
			}
				
			while (gameIsOn) {
				if (lanes[currentLane][offset] == 'o') {
					sila--;
				} else if (lanes[currentLane][offset] == 'f') {
					sila++;
				} else if (lanes[currentLane][offset] == 'b') {
					sila--;
					moralka++;
				} else if (lanes[currentLane][offset] == 's')
				{
					moralka--;
				}

				if (Console.KeyAvailable) {
					keyInfo = Console.ReadKey(true);

					if (keyInfo.Key == ConsoleKey.Escape) {
						break;
					}

					switch (keyInfo.Key) {
					case ConsoleKey.UpArrow:
						currentLane = Math.Max(0, currentLane - 1);
						break;
					case ConsoleKey.DownArrow:
						currentLane = Math.Min(2, currentLane + 1);
						break;
					default:
						break;
					}
				} else {
					Thread.Sleep(100);
				}

				offset++;

				if (sila > 0 && moralka > 0) {
					DrawGame(lanes, offset, currentLane);
				} else {
					Console.Clear();

					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Hra skoncila :(");
					Console.ResetColor();

					if (sila <= 0) {
						Console.WriteLine("Dosla ti fyzicka sila a svalil si se mrtev k zemi");
					} else {
						Console.WriteLine("Dosla ti moralka a vzdal si svuj pochod, svalil si se k zemi a odmitas vstat...");
					}

					Console.WriteLine("Zmackni Q pro exit");
					Console.WriteLine();

					if (Console.ReadKey(false).KeyChar == 'q') {
						return;
					}
				} 

			}
		}

		private static void DrawGame(List<string> lanes, int offset, int currentLane)
		{
			Console.Clear();

			for (int l = 0; l < 3; l++) {
				Console.WriteLine(lanes[l].Substring(offset, 50));
			}

			Console.SetCursorPosition(0, currentLane);
			Console.Write('X');

			Console.SetCursorPosition(0, 4);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Sila: " + sila);
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Moralka: " + moralka);
			Console.ResetColor();

			Console.SetCursorPosition(0, 7);

			DrawHelp();
		}

		private static void DrawHelp() {
			Console.WriteLine("Pochod Smrti: Minihra pro Ceskoslovensko 38-89: Vnitrni Pohranici");
			Console.WriteLine("----");
			Console.WriteLine("Sipkami nahoru a dolu ovladas vezne - muzes si vybrat jednu ze tri cest, kterou jit. ");
			Console.WriteLine("Jelikoz jsou ale pochody smrti krute, nikdo pro tebe nezastavi nebo nezpomali - musis drzet tempo! ...");
			Console.WriteLine("Po ceste muzes narazit na urcite objekty, ktere ti zvedaji ci ubiraji fyzickou silu nebo moralku:");
			Console.WriteLine("Pokud ti neco z techto ukazatlu klesne na nulu, prohral jsi a svuj pochod jsi nezvladl.");
			Console.WriteLine();

			Console.WriteLine("Legenda:");
			Console.WriteLine("F .... jidlo, pridava 1 zivot");
			Console.WriteLine("O .... prekazka, ubira 1 zivot");
			Console.WriteLine("B .... clovek co potrebuje pomoct, pridava 1 moralku");
			Console.WriteLine("S .... vojak, ubira 1 moralku");

		}
	}
}