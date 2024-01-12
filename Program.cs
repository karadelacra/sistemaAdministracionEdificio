using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace SistemaAdministracionEdificio
{


	public class Propietario
	{
		protected string nombre;
		protected decimal saldo;
		
		public Propietario()
		{
			nombre = "";
			saldo = 0;
		}
		
		public Propietario(string nombre, decimal saldo)
		{
			this.nombre = nombre;
			this.saldo = saldo;
		}
		
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
		
		public Departamento()
		{
			propietario = new Propietario();
			cuotaMantenimiento = 0;
		}
		
		public Departamento(Propietario propietario, decimal cuotaMantenimiento)
		{
			this.propietario = propietario;
			this.cuotaMantenimiento = cuotaMantenimiento;
		}

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
		
		public List<Departamento> Departamentos{
			get {return departamentos;}
			set {departamentos = value;}
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
				// imprimir en pantalla el pago realizado
				Console.WriteLine($"Se realizo el pago de mantenimiento del departamento {departamento.Propietario.Nombre} por un total de {departamento.CuotaMantenimiento:C}");
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
				Console.WriteLine($"Se pago el servicio de {servicio.Nombre} por un total de {servicio.Costo:C}");
			}
			Console.WriteLine($"Se pagaron los servicios por un total de {egresos:C}");
		}

		public void GenerarEstadoCuenta()
		{
			// archivo de prueba
			string rutaArchivo = "prueba.txt";

		// Contenido que deseas escribir en el archivo
		string contenido = "Este es un ejemplo de contenido.\nOtra línea de texto.";

		// Escribe el contenido en el archivo
		File.WriteAllText(rutaArchivo, contenido);

		Console.WriteLine("Contenido escrito en el archivo.");
   
			// imprimir un estado de cuenta en un archivo de texto
			// crear el archivo
			// Ruta del archivo que deseas crear o sobrescribir
		string archivo = "estado_cuenta.txt";
		
		
			Console.WriteLine("Estado de cuenta mensual:");
			// escribir en el archivo las cuotas de mantenimiento de cada departamento con su respectivo propietario
			foreach (var departamento in departamentos)
			{
				Console.WriteLine($"{departamento.Propietario.Nombre}: {departamento.CuotaMantenimiento:C}");
			}
			Console.WriteLine($"\n\n");
			Console.WriteLine($"Ingresos: {ingresos:C}");
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
			foreach (var departamento in departamentos)
			{
				if (departamento.Propietario.Saldo < departamento.CuotaMantenimiento)
				{
					Console.WriteLine($"{departamento.Propietario.Nombre}: {departamento.CuotaMantenimiento-departamento.Propietario.Saldo:C}");
				}
			}
			
			// repetir el estado de cuenta en el archivo de texto llamado estado_cuenta.txt
			
			string contenido2 = "estado de cuenta mensual:\n";
			foreach (var departamento in departamentos)
			{
				contenido2 += $"{departamento.Propietario.Nombre}: {departamento.CuotaMantenimiento:C}\n";
			}
			contenido2 += $"\n\n";
			contenido2 += $"Ingresos: {ingresos:C}\n";
			foreach (var servicio in servicios)
			{
				contenido2 += $"{servicio.Nombre}: {servicio.Costo:C}\n";
			}
			contenido2 += $"Egresos: {egresos:C}\n";
			contenido2 += $"Saldo total: {CalcularSaldoTotal():C}\n";
			contenido2 += "\nDeudores:\n";
			foreach (var departamento in departamentos)
			{
				if (departamento.Propietario.Saldo < departamento.CuotaMantenimiento)
				{
					contenido2 += $"{departamento.Propietario.Nombre}: {departamento.Propietario.Saldo:C}\n";
				}
			}
			File.WriteAllText(archivo, contenido2);
			
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
			Departamento depto1 = new Departamento { Propietario = new Propietario { Nombre = "Juan Perez", Saldo = 1000 }, CuotaMantenimiento = 300 };
			Departamento depto2 = new Departamento { Propietario = new Propietario { Nombre = "Eduardo Lopez", Saldo = 2000 }, CuotaMantenimiento = 350 };
			Departamento depto3 = new Departamento { Propietario = new Propietario { Nombre = "Maria Hernandez", Saldo = 15 }, CuotaMantenimiento = 400 };
			Departamento depto4 = new Departamento { Propietario = new Propietario { Nombre = "Pedro Sanchez", Saldo = 100 }, CuotaMantenimiento = 450 };
			Departamento depto5 = new Departamento { Propietario = new Propietario { Nombre = "Ana Garcia", Saldo = 2500 }, CuotaMantenimiento = 500 };
			


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
						int i = 1;
						
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
						else if(depto == i++)
						{
							// pagar todos
							foreach (var departamento in administrador.Departamentos)
							{
								administrador.RealizarPagoMantenimiento(departamento);
							}
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