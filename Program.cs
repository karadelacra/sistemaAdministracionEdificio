using System;
using System.Collections.Generic;

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

            Servicio luz = new Servicio { Nombre = "Luz", Costo = 100 };
            Servicio basura = new Servicio { Nombre = "Recoleccion de basura", Costo = 50 };
            Servicio limpieza = new Servicio { Nombre = "Limpieza", Costo = 50 };
            Servicio material = new Servicio { Nombre = "Compra de material", Costo = 200 };
            Servicio reparaciones = new Servicio { Nombre = "Reparaciones", Costo = 150 };

            // Crear administrador
            Administrador administrador = new Administrador();

            // Agregar departamentos y servicios al administrador
            administrador.AgregarDepartamento(depto1);
            administrador.AgregarDepartamento(depto2);

            administrador.AgregarServicio(luz);
            administrador.AgregarServicio(basura);
            administrador.AgregarServicio(limpieza);
            administrador.AgregarServicio(material);
            administrador.AgregarServicio(reparaciones);

            // Realizar pagos y generar estado de cuenta
            administrador.RealizarPagoMantenimiento(depto1);
            administrador.RealizarPagoMantenimiento(depto2);

            administrador.PagarServicios();
            administrador.GenerarEstadoCuenta();
        }
    }
}