using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Programozasi_kornyezetek_05 {
	class SajatKivetel : Exception {
		private const string Uzenet = "Sajat hibauzenet";

		public SajatKivetel() : base(Uzenet) {
		}

		public SajatKivetel(string Uzenet) : base(Uzenet) {
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

		public static void Exceptions() {
			int x = -12;
			try {
				if (x < 0) throw new SajatKivetel("Negativ ertek!");
			}
			catch (Exception e) {
				Console.WriteLine("Hiba: " + e.Message);
			}
		}

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
			class HibasSzuletesiEv : Exception {
				private const string Uzenet = "Hibas szuletesi ev!";

				public HibasSzuletesiEv() : base(Uzenet) {
				}

				public HibasSzuletesiEv(string Uzenet) : base(Uzenet) {
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
						Console.WriteLine("Hibas szuletesi ev!");
					}
					catch (Exception e) {
						Console.WriteLine("Hiba: " + e.Message);
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
		class Tort {
			private int szamlalo;
			private int nevezo;
			private float ertek;

			class TortKivetel : Exception {
				private const string Uzenet = "Hiba!";

				public TortKivetel() : base(Uzenet) {
				}

				public TortKivetel(string uzenet) : base(uzenet) {
				}
			}

			public float GetErtek() {
				ertek = (float) (szamlalo * 1.0 / nevezo);
				return ertek;
			}
			
			public void Input() {
				bool inputOk;

				inputOk = false;
				while (!inputOk) {
					try {
						Console.Write("Szamlalo: ");
						szamlalo = int.Parse(Console.ReadLine());
						inputOk = true;
					}
					catch (Exception e) {
						Console.WriteLine("Szamot!");
					}
				}

				inputOk = false;
				while (!inputOk) {
					try {
						Console.Write("Nevezo: ");
						nevezo = int.Parse(Console.ReadLine());
						if (nevezo == 0) throw new TortKivetel("Nevezo ne legyen 0!");
						inputOk = true;
					}
					catch (Exception e) {
						Console.WriteLine(e);
						Console.WriteLine("Szamot!");
					}
				}
			}
		}
		
		// 3. Művelet nevű delegált készítése, ami két egész szám között képes összeadást, vagy szorzást elvégezni, a választástól függően!
		
		public static void Main(string[] args) {
			//Exceptions();
			//Delegates();
			//FelhasznaloExec();
			
			Tort tort = new Tort();
			tort.Input();
			Console.WriteLine("Tort erteke: " + tort.GetErtek());

		}
	}
}