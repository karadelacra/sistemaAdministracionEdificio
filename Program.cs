using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaAdministracionEdificio
{

	public class Departamento
	{
		private int numerodepto;
		private decimal cuotaMantenimiento;

		public Departamento()
		{
			numerodepto = 0;
			cuotaMantenimiento = 0;
		}
		public Departamento(int numerodepto, decimal cuotaMantenimiento)
		{
			this.numerodepto = numerodepto;
			this.cuotaMantenimiento = cuotaMantenimiento;
		}

		public decimal CuotaMantenimiento
		{
			get { return cuotaMantenimiento; }
			set { cuotaMantenimiento = value; }
		}
		public int NumeroDepto
		{
			get { return numerodepto; }
			set { numerodepto = value; }
		}
	}
	public class Propietario : Departamento
	{
		private string nombre;
		private decimal saldo;

		public Propietario() : base()
		{
			nombre = "";
			saldo = 0;
		}
		public Propietario(int numerodepto, decimal cuotaMantenimiento, string nombre, decimal saldo) : base(numerodepto, cuotaMantenimiento)
		{
			this.nombre = nombre;
			this.saldo = saldo;
		}

		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}
		public decimal Saldo
		{
			get { return saldo; }
			set { saldo = value; }
		}


	}

	public class Servicio
	{
		private string nombre;
		private decimal costo;
		// lista de meses pagados
		private List<int> mesesPagados;

		public Servicio()
		{
			nombre = "";
			costo = 0;
		}

		public Servicio(string nombre, decimal costo)
		{
			this.nombre = nombre;
			this.costo = costo;
		}

		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}

		public decimal Costo
		{
			get { return costo; }
			set { costo = value; }
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

		public Administrador()
		{
			propietarios = new List<Propietario>();
			servicios = new List<Servicio>();
			ingresos = 0;
			egresos = 0;
		}

		public List<Propietario> Propietarios
		{
			get { return propietarios; }
			set { propietarios = value; }
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

			// pagar todos los servicios
			foreach (var servicio in servicios)
			{
				egresos += servicio.Costo;
				// agregar el mes a la lista de meses pagados
				servicio.mesesPagados.Add(meses);
			}
			
			
		}

		public void GenerarEstadoCuenta()
		{
			int totIngresos = 0;


			DateTime fecha = DateTime.Now;
			string FechaFormateada = fecha.ToString("MMMM", new System.Globalization.CultureInfo("es-ES")) + " del " + fecha.ToString("yyyy", new System.Globalization.CultureInfo("es-ES"));

			string archivo = "estado_cuenta.txt";

			Console.WriteLine("Estado de cuenta mensual:");
			// escribir en el archivo las cuotas de mantenimiento de cada departamento con su respectivo propietario
			foreach (var departamento in propietarios)
			{
				Console.WriteLine($"{departamento.Nombre}: {departamento.CuotaMantenimiento:C}");
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
			foreach (var departamento in propietarios)
			{
				if (departamento.Saldo < departamento.CuotaMantenimiento)
				{
					Console.WriteLine($"{departamento.Nombre}: {departamento.Saldo:C}");
				}
			}

			// repetir el estado de cuenta en el archivo de texto llamado estado_cuenta.txt




			// 	string contenido2 = $"Estado de cuenta del mes de {FechaFormateada}:\n\n";
			// 	foreach (var departamento in departamentos)
			// 	{
			// 		contenido2 += $"{departamento.Propietario.Nombre}: {departamento.CuotaMantenimiento:C}\n";
			// 	}
			// 	contenido2 += $"\n\n";
			// 	contenido2 += $"Ingresos: {totIngresos:C}\n";
			// 	foreach (var servicio in servicios)
			// 	{
			// 		contenido2 += $"{servicio.Nombre}: {servicio.Costo:C}\n";
			// 	}
			// 	contenido2 += $"Egresos: {egresos:C}\n";
			// 	contenido2 += $"Saldo total: {CalcularSaldoTotal():C}\n";
			// 	contenido2 += "\nDeudores:\n";
			// 	foreach (var departamento in departamentos)
			// 	{
			// 		if (departamento.Propietario.Saldo < departamento.CuotaMantenimiento)
			// 		{
			// 			contenido2 += $"{departamento.Propietario.Nombre}: {departamento.Propietario.Saldo:C}\n";
			// 		}
			// 	}
			// 	contenido2 += "\n\nPagado el dia " + fecha.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("es-ES"))+" a las "+fecha.ToString("HH:mm:ss", new System.Globalization.CultureInfo("es-ES"))+"\n";
			// 	File.WriteAllText(archivo, contenido2);

			// }

			// calcular estado de cuenta

		}
		public decimal CalcularSaldoTotal()
		{
			decimal saldoTotal = 0;
			foreach (var departamento in propietarios)
			{
				saldoTotal += departamento.Saldo;
			}
			return saldoTotal;
		}



		internal class Program
		{
			static void Main()
			{
				// lista de nombres de propietarios
				string[] nombres = { "Juan Perez", "Maria Lopez", "Pedro Hernandez", "Ana Ramirez", "Jose Gonzalez" };
				string[] servicios = { "Luz", "Recoleccion de basura", "Limpieza", "Compra de material", "Reparaciones", "Gas" };
				int[] costos = { 100, 50, 50, 200, 150, 100 };

				// Crear administrador
				Administrador administrador = new Administrador();
				// Departamento depto1 = new Departamento { Propietario = new Propietario { Nombre = "Juan Perez", Saldo = 1000 }, CuotaMantenimiento = 300 };
				int i = 1;
				for (i = 1; i < 6; i++)
				{
					Departamento depto = new Departamento { NumeroDepto = i, CuotaMantenimiento = 300 };
					Propietario prop = new Propietario { Nombre = nombres[i - 1], Saldo = 1000 };
					administrador.AgregarPropietario(prop);
				}
				// servicios
				// contar tamaño de la lista de servicios
				int tam = servicios.Length;
				for (i = 0; i < tam; i++)
				{
					Servicio servicio = new Servicio { Nombre = servicios[i], Costo = costos[i] };
					administrador.AgregarServicio(servicio);
				}


				// generar un menú para que el usuario pueda elegir que hacer
				int opcion = 0;
				while (opcion != 4)
				{
					Console.Clear();
					Console.WriteLine("\n\nMenu:");
					Console.WriteLine("1. Pagar servicios");
					Console.WriteLine("2. Realizar pago de mantenimiento");
					Console.WriteLine("3. Generar estado de cuenta");
					Console.WriteLine("4. agregar propietarios");
					Console.WriteLine("5. agregar servicios");
					Console.WriteLine("6. modificar propietarios");
					Console.WriteLine("7. modificar servicios");
					Console.WriteLine("8. salir");
					Console.Write("Opcion: ");
					opcion = int.Parse(Console.ReadLine());

					switch (opcion)
					{
						case 1:
							if (administrador.MantenimientoPagado == false)
							{
								Console.WriteLine("Primero debe pagar el mantenimiento");
								Thread.Sleep(2500);
								Console.WriteLine("Presione una tecla para continuar...");
								Console.ReadKey();
								break;
							}
							else
							{
								Console.WriteLine("1. Pagar todos los servicios");

								if (int.Parse(Console.ReadLine()) == 1)
								{
									administrador.PagarServicios(0);
								}
								else
								{
									for (i = 0; i < tam; i++)
									{
										Console.WriteLine($"{i + 1}. {servicios[i]}");
									}
									Console.WriteLine("Elija el servicio a pagar");
									int op = int.Parse(Console.ReadLine());
									int mes = 0;
									Console.WriteLine("Elija el mes a pagar");
									mes = int.Parse(Console.ReadLine());
									administrador.PagarServicios(mes);
								}
								break;
							}
						case 3:
							if (administrador.ServiciosPagados == false)
							{
								Console.WriteLine("Primero debe pagar los servicios");
								Thread.Sleep(2500);
								Console.WriteLine("Presione una tecla para continuar...");
								Console.ReadKey();
								break;
							}
							else
							{
								Console.WriteLine("Generando estado de cuenta...");
								Thread.Sleep(2500);
								administrador.GenerarEstadoCuenta();
								Console.WriteLine("Estado de cuenta generado");
								Thread.Sleep(1000);
								Console.WriteLine("Presione una tecla para continuar...");
								Console.ReadKey();
								break;
							}
						case 2:
							i = 1;

							foreach (var departamento in administrador.Departamentos)
							{

								Console.WriteLine($"{i}. {departamento.Propietario.Nombre}: {departamento.CuotaMantenimiento:C}");
								i++;
							}
							Console.WriteLine($"Elija el departamento a pagar o precione {i++} para pagar todos");
							int depto = int.Parse(Console.ReadLine());
							if (depto == i)
							{
								foreach (var departamento in administrador.Departamentos)
								{
									administrador.RealizarPagoMantenimiento(departamento);
								}
							}
							else if (depto == i++)
							{
								// pagar todos
								foreach (var departamento in administrador.Departamentos)
								{
									administrador.RealizarPagoMantenimiento(departamento);
								}
							}
							administrador.MantenimientoPagado = true;
							Console.WriteLine("Presione una tecla para continuar...");
							Console.ReadKey();
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
