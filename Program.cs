using System;
using System.Reflection;

namespace Programozasi_kornyezetek_05 {
	class SajatKivetel : Exception {
		private const string Uzenet = "Sajat hibauzenet";

		public SajatKivetel() : base(Uzenet) {
		}

		public SajatKivetel(string uzenet) : base(uzenet) {
		}
	}
	
	internal class Program {
		// Delegált deklarálása
		public delegate void Zenehallgatas(string user);
		
		// Delegált típusú metódus
		public static void Bartok(string hallgato) {
			Console.WriteLine(("Bartokot hallgat: " + hallgato));
		}

		public static void Edda(string hallgato) {
			Console.WriteLine(("Eddat hallgat: " + hallgato));
		}

		/*
		public static void Exceptions() {
			int x = -12;
			try {
				if (x < 0) throw new SajatKivetel("Negativ ertek!");
			}
			catch (Exception e) {
				Console.WriteLine("Hiba: " + e.Message);
			}
		}
		*/

		public static void Delegates() {
			// Delegált típusú változó
			Zenehallgatas zene;
			zene = Bartok;
			zene += Edda;
			zene("En");
		}
		
		
		// 1. Kérjük be a felhasználó születési évét, és írjuk ki az életkorát. Abban az esetben, ha érvénytelen számot,
		// vagy szöveget ad meg, ismételjük a bevitelt!
		class Felhasznalo {
			private class HibasSzuletesiEv : Exception {
				private const string Uzenet = "Hiba:";

				public HibasSzuletesiEv(string uzenet) : base(Uzenet) {
					Console.WriteLine(Uzenet + " " + uzenet);
				}
			}
			
			private int _szuletesiEv;

			public void SetSzuletesiEv() {
				bool szuletesiEvRendben = false;
				while (!szuletesiEvRendben) {
					try {
						Console.Write("Szuletesi ev: ");
						int x = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

						if (x < 1900) throw new HibasSzuletesiEv("Tul regi szuletesi ev!");
						if (x > DateTime.Today.Year) throw new HibasSzuletesiEv("Jovobeni szuletesi ev!");

						_szuletesiEv = x;
						szuletesiEvRendben = true;
					}
					catch (FormatException e) {
						Console.WriteLine(e.Message);
					}
					catch (Exception e) {
						Console.WriteLine(e.Message);
					}
					finally {
						Console.WriteLine("Bevitt szuletesi ev: " + _szuletesiEv);
					}
				}
			}

			public int GetEletkor() {
				return DateTime.Now.Year - _szuletesiEv;
			}
		}

		static void FelhasznaloExec() {
			Felhasznalo felhasznalo = new Felhasznalo();
			felhasznalo.SetSzuletesiEv();
			Console.WriteLine("Eletkor: " + felhasznalo.GetEletkor());
		}
		
		// 2. Készíts programot, ami egy tört értékét számolja ki a számláló és a nevező bekérésével! Készíts saját kivételosztályt, amit meghívol akkor, ha a nevező 0!
		private class Tort {
			private int _szamlalo;
			private int _nevezo;
			private float _ertek;

			private class TortKivetel : Exception {
				private const string Uzenet = "Hiba:";
				public TortKivetel(string uzenet) : base(Uzenet) {
					Console.WriteLine(Uzenet + " " + uzenet);
				}
			}

			public float GetErtek() {
				_ertek = (float) (_szamlalo * 1.0 / _nevezo);
				return _ertek;
			}
			
			public void Input() {
				bool inputOk;

				inputOk = false;
				while (!inputOk) {
					try {
						Console.Write("Szamlalo: ");
						_szamlalo = int.Parse(Console.ReadLine() ?? throw new TortKivetel("Szamot!"));
						inputOk = true;
					}
					catch (Exception) {
						Console.WriteLine("Szamot!");
					}
				}

				inputOk = false;
				while (!inputOk) {
					try {
						Console.Write("Nevezo: ");
						_nevezo = int.Parse(Console.ReadLine() ?? throw new TortKivetel("Nem 0 szamot!"));
						if (_nevezo == 0) throw new TortKivetel("Nevezo ne legyen 0!");
						inputOk = true;
					}
					catch (Exception) {
						Console.WriteLine("Szamot!");
					}
				}
			}
		}
		
		// 3. Művelet nevű delegált készítése, ami két egész szám között képes összeadást, vagy szorzást elvégezni,
		// a választástól függően!
		private class Muveletek {
			public delegate int Muvelet(int a, int b);
		
			// Delegált típusú metódus
			public static int Osszead(int a, int b) {
				return a + b;
			}

			public static int Szoroz(int a, int b) {
				return a * b;
			}

			public static string MuveletiJel(string nev) {
				switch (nev) {
					case "Osszead": return "+";
					case "Szoroz": return "*";
					default: return null;
				}
			}
		}

		private static void TortExec() {
			Tort tort = new Tort();
			tort.Input();
			Console.WriteLine("Tort erteke: " + tort.GetErtek());
		}

		static void DelegaltMuveletExec() {
			int a = 3;
			int b = 4;
			
			Muveletek.Muvelet muvelet;
			
			muvelet = Muveletek.Osszead;
			Console.WriteLine(a + " " + Muveletek.MuveletiJel(muvelet.GetMethodInfo().Name) + " " + b + " = " + muvelet(a, b));
			
			muvelet = Muveletek.Szoroz;
			Console.WriteLine(a + " " + Muveletek.MuveletiJel(muvelet.GetMethodInfo().Name) + " " + b + " = " + muvelet(a, b));
		}
		
		public static void Main(string[] args) {
			//Exceptions();
			//Delegates();
			
			FelhasznaloExec();
			TortExec();
			DelegaltMuveletExec();
		}
	}
}