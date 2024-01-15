using System;
using System.Collections.Generic;
using System.Threading;

namespace SistemaAdministracionEdificio
{
	public class Departamento
	{
		public int NumeroDepto { get; set; }
		public decimal CuotaMantenimiento { get; set; }

		public Departamento()
		{
			NumeroDepto = 0;
			CuotaMantenimiento = 0;
		}

		public Departamento(int numeroDepto, decimal cuotaMantenimiento)
		{
			NumeroDepto = numeroDepto;
			CuotaMantenimiento = cuotaMantenimiento;
		}
	}

	public class Propietario : Departamento
	{
		public string Nombre { get; set; }
		public decimal Saldo { get; set; }

		public Propietario() : base()
		{
			Nombre = "";
			Saldo = 0;
		}

		public Propietario(int numeroDepto, decimal cuotaMantenimiento, string nombre, decimal saldo)
			: base(numeroDepto, cuotaMantenimiento)
		{
			Nombre = nombre;
			Saldo = saldo;
		}
	}

	public class Servicio
	{
		public string Nombre { get; set; }
		public decimal Costo { get; set; }
		public List<int> MesesPagados { get; set; }

		public Servicio()
		{
			Nombre = "";
			Costo = 0;
			MesesPagados = new List<int>();
		}

		public Servicio(string nombre, decimal costo)
		{
			Nombre = nombre;
			Costo = costo;
			MesesPagados = new List<int>();
		}
	}

	public class Administrador
	{
		private List<Propietario> propietarios;
		private List<Servicio> servicios;
		private decimal ingresos;
		private decimal egresos;
		private bool mantenimientoPagado = false;

		public bool MantenimientoPagado
		{
			get { return mantenimientoPagado; }
			set { mantenimientoPagado = value; }
		}

		public List<Servicio> Servicios
		{
			get { return servicios; }
			set { servicios = value; }
		}

		public List<Propietario> Propietarios
		{
			get { return propietarios; }
			set { propietarios = value; }
		}

		public Administrador()
		{
			propietarios = new List<Propietario>();
			servicios = new List<Servicio>();
			ingresos = 0;
			egresos = 0;
		}

		public void AgregarPropietario(Propietario propietario)
		{
			propietarios.Add(propietario);
		}

		public void AgregarServicio(Servicio servicio)
		{
			servicios.Add(servicio);
		}

		public void RealizarPagoMantenimiento(Departamento departamento)
		{
			propietarios[departamento.NumeroDepto - 1].Saldo -= departamento.CuotaMantenimiento;
			ingresos += departamento.CuotaMantenimiento;
		}

		public void PagarServicios(int meses)
		{
			foreach (var servicio in servicios)
			{
				egresos += servicio.Costo;
				servicio.MesesPagados.Add(meses);
			}
			mantenimientoPagado = true;
			Console.WriteLine("Servicios pagados.");
			Console.WriteLine("Presione una tecla para continuar...");
			Console.ReadKey();
		}


		public void GenerarEstadoCuenta()
		{
			int totIngresos = 0;


			DateTime fecha = DateTime.Now;
			string FechaFormateada = fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES")) + " del " + fecha.ToString("yyyy", new System.Globalization.CultureInfo("es-ES"));

			string archivo = "estado_cuenta.txt";

			Console.WriteLine("Estado de cuenta mensual:");
			// escribir en el archivo las cuotas de mantenimiento de cada departamento con su respectivo propietario
			foreach (var propietario in propietarios)
			{
				Console.WriteLine($"{propietario.Nombre}: {propietario.CuotaMantenimiento:C}");
			}
			Console.WriteLine($"\n\n");
			foreach (var servicio in servicios)
			{
				totIngresos += Convert.ToInt32(servicio.Costo);
			}
			Console.WriteLine($"Ingresos: {totIngresos:C}");
			// escribir en el archivo los servicios pagados
			Console.WriteLine("Servicios pagados:");
			foreach (var servicio in servicios)
			{
				Console.WriteLine($"{servicio.Nombre}: {servicio.Costo:C}");
			}
			Console.WriteLine($"Egresos: {egresos:C}");

			// escribir en el archivo el saldo total
			Console.WriteLine($"Saldo total: {CalcularSaldoTotal():C}");

			// escribir en el archivo los deudores
			Console.WriteLine("\nDeudores:");
			foreach (var propietario in propietarios)
			{
				if (propietario.Saldo < propietario.CuotaMantenimiento)
				{
					Console.WriteLine($"{propietario.Nombre}: {propietario.CuotaMantenimiento - propietario.Saldo:C}");
				}
			}

			// repetir el estado de cuenta en el archivo de texto llamado estado_cuenta.txt




			string contenido2 = $"Estado de cuenta del mes de {FechaFormateada}:\n\n";
			foreach (var propietario in propietarios)
			{
				contenido2 += $"{propietario.Nombre}: {propietario.CuotaMantenimiento:C}\n";
			}
			contenido2 += $"\n\n";
			contenido2 += $"Ingresos: {totIngresos:C}\n";
			foreach (var servicio in servicios)
			{
				contenido2 += $"{servicio.Nombre}: {servicio.Costo:C}\n";
			}
			contenido2 += $"Egresos: {egresos:C}\n";
			contenido2 += $"Saldo total: {CalcularSaldoTotal():C}\n";
			contenido2 += "\nDeudores:\n";
			foreach (var propietario in propietarios)
			{
				if (propietario.Saldo < propietario.CuotaMantenimiento)
				{
					contenido2 += $"{propietario.Nombre}: {propietario.CuotaMantenimiento - propietario.Saldo:C}\n";
				}
			}
			contenido2 += "\n\nPagado el dia " + fecha.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("es-ES")) + " a las " + fecha.ToString("HH:mm:ss", new System.Globalization.CultureInfo("es-ES")) + "\n";
			File.WriteAllText(archivo, contenido2);
		}

		public decimal CalcularSaldoTotal()
		{
			decimal saldoTotal = 0;
			foreach (var propietario in propietarios)
			{
				saldoTotal += propietario.Saldo;
			}
			return saldoTotal;
		}
	}

	internal class Program
	{
		static void Main()
		{
			string[] nombres = { "Juan Perez", "Maria Lopez", "Pedro Hernandez", "Ana Ramirez", "Jose Gonzalez" };
			string[] serviciosArray = { "Luz", "Recoleccion de basura", "Limpieza", "Compra de material", "Reparaciones", "Gas" };
			int[] costos = { 100, 50, 50, 200, 150, 100 };

			Administrador administrador = new Administrador();

			for (int i = 1; i < 6; i++)
			{
				Departamento depto = new Departamento { NumeroDepto = i, CuotaMantenimiento = 300 };
				Propietario prop = new Propietario { NumeroDepto = i, CuotaMantenimiento = 300, Nombre = nombres[i - 1], Saldo = 1000 };
				administrador.AgregarPropietario(prop);
			}

			for (int i = 0; i < serviciosArray.Length; i++)
			{
				Servicio servicio = new Servicio { Nombre = serviciosArray[i], Costo = costos[i] };
				administrador.AgregarServicio(servicio);
			}

			int opcion = 0;

			while (opcion != 8)
			{
				Console.Clear();
				Console.WriteLine("\n\nMenu:");
				Console.WriteLine("1. Pagar servicios");
				Console.WriteLine("2. Realizar pago de mantenimiento");
				Console.WriteLine("3. Generar estado de cuenta");
				Console.WriteLine("4. Agregar propietarios");
				Console.WriteLine("5. Agregar servicios");
				Console.WriteLine("6. Modificar propietarios");
				Console.WriteLine("8. Salir");
				Console.Write("Opcion: ");
				opcion = int.Parse(Console.ReadLine());
				//
				
				switch (opcion)
				{
					case 1:
						if (administrador.MantenimientoPagado == true)//Se valida si el mantenimiento ya fue pagado, se supone que el dinero para pagar los servicios sale del pago de mantenimiento
						{
							Console.WriteLine("0. Pagar todos los servicios");
							Console.WriteLine("1. Pagar un servicio en especifico");
							if (int.Parse(Console.ReadLine()) == 0)
							{
								administrador.PagarServicios(0);
								Console.WriteLine("Servicios pagados.");
								Console.WriteLine("Presione una tecla para continuar...");
								Console.ReadKey();
							}
							else
							{
								int j = 0;
								//servicios
								for (j = 0; j < administrador.Servicios.Count; j++)
								{
									Console.WriteLine($"{j + 1}. {administrador.Servicios[j].Nombre}");//ya nomas subelo a github y ya
								}
								Console.WriteLine("Elija el servicio a pagar");
								int op = int.Parse(Console.ReadLine());
								int mes = 0;
								Console.WriteLine("Elija el mes a pagar");
								mes = int.Parse(Console.ReadLine());
								administrador.PagarServicios(mes);

							}
						}
						else
						{
							Console.WriteLine("El mantenimiento no ha sido pagado");
						}
						break;
					case 3:
						Console.WriteLine("Generando estado de cuenta...");
						Thread.Sleep(2500);
						administrador.GenerarEstadoCuenta();
						Console.WriteLine("Presione una tecla para continuar...");
						Console.ReadKey();
						break;
					case 2:
						int i = 1;
						for (i = 1; i <= administrador.Propietarios.Count; i++)
						{
							Console.WriteLine($"{i}. {administrador.Propietarios[i - 1].Nombre} (departamento {administrador.Propietarios[i - 1].NumeroDepto})");
						}
						Console.WriteLine("Elija el departamento a pagar");
						int depto = int.Parse(Console.ReadLine());
						administrador.RealizarPagoMantenimiento(administrador.Propietarios[depto - 1]);
						administrador.MantenimientoPagado = true;
						break;
					case 4:
						Console.WriteLine("Ingrese el nombre del propietario");
						string nombre = Console.ReadLine();
						Console.WriteLine("Ingrese el saldo del propietario");
						decimal saldo = decimal.Parse(Console.ReadLine());
						//Console.WriteLine("Ingrese el numero de departamento");
						//int numDepto = int.Parse(Console.ReadLine());
						int numDepto = administrador.Propietarios.Count + 1;
						Console.WriteLine("Ingrese la cuota de mantenimiento");
						decimal cuota = decimal.Parse(Console.ReadLine());
						Propietario propietarionvo = new Propietario(numDepto, cuota, nombre, saldo);
						administrador.AgregarPropietario(propietarionvo);
						break;
					case 5:
						Console.WriteLine("Ingrese el nombre del servicio");
						string nombreServicio = Console.ReadLine();
						Console.WriteLine("Ingrese el costo del servicio");
						decimal costoServicio = decimal.Parse(Console.ReadLine());
						Servicio servicio = new Servicio(nombreServicio, costoServicio);
						administrador.AgregarServicio(servicio);
						break;
					case 6:
						for (int j = 0; j < administrador.Propietarios.Count; j++)
						{
							Console.WriteLine($"{j + 1}. {administrador.Propietarios[j].Nombre}");
						}
						Console.WriteLine("Ingrese el numero del propietario a modificar");
						int numProp = int.Parse(Console.ReadLine());
						Console.WriteLine("Ingrese el nuevo nombre del propietario");
						string nombreProp = Console.ReadLine();
						Console.WriteLine("Ingrese el nuevo saldo del propietario");
						decimal saldoProp = decimal.Parse(Console.ReadLine());
						//Console.WriteLine("Ingrese el nuevo numero de departamento");
						//int numDeptoProp = int.Parse(Console.ReadLine());
						int numDeptoProp = administrador.Propietarios[numProp - 1].NumeroDepto;
						Console.WriteLine("Ingrese la nueva cuota de mantenimiento");
						decimal cuotaProp = decimal.Parse(Console.ReadLine());
						administrador.Propietarios[numProp - 1].Nombre = nombreProp;
						administrador.Propietarios[numProp - 1].Saldo = saldoProp;
						administrador.Propietarios[numProp - 1].NumeroDepto = numDeptoProp;
						administrador.Propietarios[numProp - 1].CuotaMantenimiento = cuotaProp;
						break;
					default:
						Console.WriteLine("Saliendo...");
						Thread.Sleep(2500);
						break;
				}
			}
		}
	}
}
