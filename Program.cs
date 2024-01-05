using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SistemaAdministracionEdificio
{


	public class Propietario
	{
		protected string nombre;
		protected decimal saldo;
		
		public string Nombre{
			get {return nombre;}
			set {nombre=value;}
		}

		public decimal Saldo{
			get {return saldo;}
			set {saldo = value;}
		}
	}

	public class Servicio
	{
		protected string nombre;
		protected decimal costo;

		public string Nombre{
			get {return nombre;}
			set {nombre = value;}
		}

		public decimal Costo{
			get {return costo;}
			set {costo = value;}
		}
	}

	public class Departamento
	{
		protected Propietario propietario;
		protected decimal cuotaMantenimiento;

		public Propietario Propietario{
			get {return propietario;}
			set {propietario = value;}
		}

		public decimal CuotaMantenimiento{
			get {return cuotaMantenimiento;}
			set {cuotaMantenimiento = value;}
		}
	}

	public class Administrador
	{
		private List<Departamento> departamentos;
		private List<Servicio> servicios;
		private decimal ingresos;
		private decimal egresos;

		public Administrador()
		{
			departamentos = new List<Departamento>();
			servicios = new List<Servicio>();
			ingresos = 0;
			egresos = 0;
		}

		public void AgregarDepartamento(Departamento departamento)
		{
			departamentos.Add(departamento);
		}

		public void AgregarServicio(Servicio servicio)
		{
			servicios.Add(servicio);
		}

		public void RealizarPagoMantenimiento(Departamento departamento)
		{
			if (departamento.Propietario.Saldo >= departamento.CuotaMantenimiento)
			{
				departamento.Propietario.Saldo -= departamento.CuotaMantenimiento;
				ingresos += departamento.CuotaMantenimiento;
			}
			else
			{
				Console.WriteLine($"El propietario {departamento.Propietario.Nombre} no tiene saldo suficiente.");
			}
		}

		public void PagarServicios()
		{
			foreach (var servicio in servicios)
			{
				egresos += servicio.Costo;
			}
			Console.WriteLine($"Se pagaron los servicios por un total de {egresos:C}");
		}

		public void GenerarEstadoCuenta()
		{
			Console.WriteLine("Estado de cuenta mensual:");
			Console.WriteLine($"Ingresos: {ingresos:C}");
			Console.WriteLine($"Egresos: {egresos:C}");
			Console.WriteLine($"Saldo total: {CalcularSaldoTotal():C}");

			Console.WriteLine("\nDeudores:");
			foreach (var departamento in departamentos)
			{
				if (departamento.Propietario.Saldo < departamento.CuotaMantenimiento)
				{
					Console.WriteLine($"{departamento.Propietario.Nombre}: {departamento.Propietario.Saldo:C}");
				}
			}
		}

		private decimal CalcularSaldoTotal()
		{
			return ingresos - egresos;
		}
	}




	internal class Program
	{
		static void Main()
		{
			// Crear objetos
			Departamento depto1 = new Departamento { Propietario = new Propietario { Nombre = "Propietario1", Saldo = 1000 }, CuotaMantenimiento = 300 };
			Departamento depto2 = new Departamento { Propietario = new Propietario { Nombre = "Propietario2", Saldo = 800 }, CuotaMantenimiento = 350 };
			Departamento depto3 = new Departamento { Propietario = new Propietario { Nombre = "Propietario3", Saldo = 500 }, CuotaMantenimiento = 400 };
			Departamento depto4 = new Departamento { Propietario = new Propietario { Nombre = "Propietario4", Saldo = 3000 }, CuotaMantenimiento = 450 };
			Departamento depto5 = new Departamento { Propietario = new Propietario { Nombre = "Propietario5", Saldo = 2000 }, CuotaMantenimiento = 500 };
			


			Servicio luz = new Servicio { Nombre = "Luz", Costo = 100 };
			Servicio basura = new Servicio { Nombre = "Recoleccion de basura", Costo = 50 };
			Servicio limpieza = new Servicio { Nombre = "Limpieza", Costo = 50 };
			Servicio material = new Servicio { Nombre = "Compra de material", Costo = 200 };
			Servicio reparaciones = new Servicio { Nombre = "Reparaciones", Costo = 150 };
			Servicio gas = new Servicio { Nombre = "Gas", Costo = 100 };

			// Crear administrador
			Administrador administrador = new Administrador();

			// Agregar departamentos y servicios al administrador
			administrador.AgregarDepartamento(depto1);
			administrador.AgregarDepartamento(depto2);
			administrador.AgregarDepartamento(depto3);
			administrador.AgregarDepartamento(depto4);
			administrador.AgregarDepartamento(depto5);


			administrador.AgregarServicio(luz);
			administrador.AgregarServicio(basura);
			administrador.AgregarServicio(limpieza);
			administrador.AgregarServicio(material);
			administrador.AgregarServicio(reparaciones);
			administrador.AgregarServicio(gas);
			// generar un menú para que el usuario pueda elegir que hacer
			int opcion = 0;
			while (opcion != 4)
			{
				Console.Clear();
				Console.WriteLine("\n\nMenu:");
				Console.WriteLine("1. Pagar servicios");
				Console.WriteLine("2. Realizar pago de mantenimiento");
				Console.WriteLine("3. Generar estado de cuenta");
				Console.WriteLine("4. Salir");
				Console.Write("Opcion: ");
				opcion = int.Parse(Console.ReadLine());
				
				switch (opcion)
				{
					case 1:
						Console.WriteLine("Pagando servicios...");
						Thread.Sleep(2500);
						administrador.PagarServicios();
						Console.WriteLine("Servicios pagados");
						Thread.Sleep(1000);
						Console.WriteLine("Presione una tecla para continuar...");
						Console.ReadKey();
						break;
					case 3:
						Console.WriteLine("Generando estado de cuenta...");
						Thread.Sleep(2500);
						administrador.GenerarEstadoCuenta();
						Console.WriteLine("Estado de cuenta generado");
						Thread.Sleep(1000);
						Console.WriteLine("Presione una tecla para continuar...");
						Console.ReadKey();
						break;
					case 2:
						Console.Write("Elija el departamento a pagar o precione 6 para pagar todos: ");
						int depto = int.Parse(Console.ReadLine());
							switch (depto)
							{
								case 1:
									Console.WriteLine("Realizando pago de mantenimiento...");
									// esperar 3 segundos
									Thread.Sleep(2500);
									administrador.RealizarPagoMantenimiento(depto1);
									Console.WriteLine("Pago realizado");
									Thread.Sleep(1000);
									break;
								case 2:
									Console.WriteLine("Realizando pago de mantenimiento...");
									Thread.Sleep(2500);
									administrador.RealizarPagoMantenimiento(depto2);
									Console.WriteLine("Pago realizado");
									Thread.Sleep(1000);
									break;
								case 3:
									Console.WriteLine("Realizando pago de mantenimiento...");
									Thread.Sleep(2500);
									administrador.RealizarPagoMantenimiento(depto3);
									Console.WriteLine("Pago realizado");
									Thread.Sleep(1000);
									break;
								case 4:
									Console.WriteLine("Realizando pago de mantenimiento...");
									Thread.Sleep(2500);
									administrador.RealizarPagoMantenimiento(depto4);
									Console.WriteLine("Pago realizado");
									Thread.Sleep(1000);
									break;
								case 5:
									Console.WriteLine("Realizando pago de mantenimiento...");
									Thread.Sleep(2500);
									administrador.RealizarPagoMantenimiento(depto5);
									Console.WriteLine("Pago realizado");
									Thread.Sleep(1000);
									break;
								case 6:
									Console.WriteLine("Realizando pago de mantenimiento...");
									Thread.Sleep(2500);
									administrador.RealizarPagoMantenimiento(depto1);
									administrador.RealizarPagoMantenimiento(depto2);
									administrador.RealizarPagoMantenimiento(depto3);
									administrador.RealizarPagoMantenimiento(depto4);
									administrador.RealizarPagoMantenimiento(depto5);
									Console.WriteLine("Pago realizado");
									Thread.Sleep(1000);
									break;
								default:
									Console.WriteLine("Opcion invalida");
									break;
							}	
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