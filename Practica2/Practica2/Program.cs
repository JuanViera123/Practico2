using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica2
{
    class Program
    {
        private object listaEmpleados;

        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {

                Console.WriteLine("1. Ejercicio 1");
                Console.WriteLine("2. Ejercicio 2");
                Console.WriteLine("3. Opción 3");
                Console.WriteLine("4. Salir");
                Console.WriteLine("Elige una de las opciones");
                int opcion = Convert.ToInt32(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Ejercicio1();
                        break;

                    case 2:
                        Ejercicio2();
                        break;

             
                    case 4:
                        Console.WriteLine("Has elegido salir de la aplicación");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Elige una opcion entre 1 y 4");
                        break;
                }
            }


        }

        internal class Persona
        {
            public int Edad { get; set; }
            public string Nombre { get; set; }
            public string Ciudad { get; set; }
        }
        static void Ejercicio1()
        {
            List<Persona> personas = new List<Persona>
            {
                new Persona { Nombre = "Juan", Edad = 25, Ciudad = "Lima"},
                new Persona { Nombre = "Maria", Edad = 40, Ciudad = "Bogota"},
                new Persona { Nombre = "Pedro", Edad = 35, Ciudad = "Madrid"},
                new Persona { Nombre = "Ana", Edad = 20, Ciudad = "Lima"},
                new Persona { Nombre = "Jose", Edad = 40, Ciudad = "Buenos Aires"}

            };

            var consulta = from p in personas
                           where p.Edad < 25 && p.Ciudad == "Lima"
                           orderby p.Nombre descending
                           select new { p.Nombre, p.Edad };

            var mayores30 = from p in personas
                            where p.Edad > 30 && p.Ciudad == "Bogota"
                            orderby p.Nombre descending
                            select new { p.Nombre, p.Edad };

            var consulta3 = from p in personas
                            where p.Edad >= 20 && p.Edad <= 35
                            orderby p.Edad ascending
                            select new { p.Nombre, p.Edad };


            foreach (var persona in consulta)
            {
                Console.WriteLine($"{persona.Nombre} ({persona.Edad} Años)");
            }
            foreach (var persona in mayores30)
            {
                Console.WriteLine($"{persona.Nombre} ({persona.Edad} Años)");
            }
            foreach (var persona in consulta3)
            {
                Console.WriteLine($"{persona.Nombre} ({persona.Edad} Años)");
            }

        }

        internal class Empleado
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Cargo { get; set; }
            public int Salario { get; set; }
            public int EmpresaId { get; set; }

            public void getDatosEmpleado()
            {
                Console.WriteLine("Empleado {0} con Id {1}, con cargo{2}, con salario{3} y pertenece a " +
                    "La empresa {4}", Nombre, Id, Cargo, Salario, EmpresaId);
            }
        }

        internal class ControlEmpresasEmpleados {
            public List<Empresa> listaEmpresas;
            public List<Empleado> listaEmpleados;

            public ControlEmpresasEmpleados()
            {
                listaEmpresas = new List<Empresa>();
                listaEmpleados = new List<Empleado>();

                listaEmpresas.Add(new Empresa { Id = 1, Nombre = "IAlpha" });
                listaEmpresas.Add(new Empresa { Id = 2, Nombre = "Udelar" });
                listaEmpresas.Add(new Empresa { Id = 3, Nombre = "SpaceZ" });

                listaEmpleados.Add(new Empleado { Id = 1, Nombre = "Gonzalo", Cargo = "CEO", EmpresaId = 1, Salario = 3000 });
                listaEmpleados.Add(new Empleado { Id = 2, Nombre = "JuanC", Cargo = "Desarrollador", EmpresaId = 1, Salario = 3000 });
                listaEmpleados.Add(new Empleado { Id = 3, Nombre = "JuanR", Cargo = "Desarrollador", EmpresaId = 1, Salario = 3000 });
                listaEmpleados.Add(new Empleado { Id = 4, Nombre = "Daniel", Cargo = "Desarrollador", EmpresaId = 1, Salario = 3000 });
                listaEmpleados.Add(new Empleado { Id = 5, Nombre = "GonzaloT", Cargo = "CEO", EmpresaId = 2, Salario = 3000 });
                listaEmpleados.Add(new Empleado { Id = 6, Nombre = "Leonardo", Cargo = "CEO", EmpresaId = 1, Salario = 3000 });
                listaEmpleados.Add(new Empleado { Id = 1, Nombre = "Gonzalo", Cargo = "CEO", EmpresaId = 3, Salario = 3000 });
                listaEmpleados.Add(new Empleado { Id = 6, Nombre = "Leonardo", Cargo = "CEO", EmpresaId = 3, Salario = 3000 });
            }
        

        public void getCeo(string _Cargo)
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              where empleado.Cargo == _Cargo
                                              select empleado;
            foreach (Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }

        }

        public void getEmpleadosOrdenados()
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              orderby empleado.Nombre
                                              select empleado;
            foreach (Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }
        }

        public void getEmpleadosOrdenadosSegun()
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              orderby empleado.Salario
                                              select empleado;
            foreach (Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }
        }

        public void getEmpleadosEmpresa(int _Empresa)
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados
                                              join empresa in listaEmpresas on empleado.EmpresaId
                                              equals empresa.Id
                                              where empresa.Id == _Empresa
                                              select empleado;
            foreach (Empleado elemento in empleados)
            {
                elemento.getDatosEmpleado();
            }
        }

        public void promedioSalario()
        {
            var consulta = from e in listaEmpleados
                           group e by e.EmpresaId into g
                           select new { empresa = g.Key, PromedioSalario = g.Average(e => e.Salario) };
            foreach (var resultado in consulta)
            {
                switch (resultado.empresa)
                {
                    case 1: Console.WriteLine($"Empresa IAlpha - Promedio de salario: {resultado.PromedioSalario}");
                        break;
                    case 2: Console.WriteLine($"Empresa UdeLaR - Promedio de salario: {resultado.PromedioSalario}");
                        break;
                    case 3: Console.WriteLine($"Empresa SpaceZ - Promedio de salario: {resultado.PromedioSalario}");
                        break;

                }

            }
        }
    }
        internal class Empresa { 
        public int Id { get; set; }
        public string Nombre { get; set; }

         public void getDatoEmpresa()
            {
                Console.WriteLine("Empresa {0} con Id {1}", Nombre, Id);
            }
        }

        static void Ejercicio2()
        {
            ControlEmpresasEmpleados ce = new ControlEmpresasEmpleados();

            Console.WriteLine("Promedios por empresas \n********************");
            ce.promedioSalario();
            Console.WriteLine("");

            Console.WriteLine("Peces Gordos \n**********************");
            ce.getCeo("CEO");

            Console.WriteLine("");
            Console.WriteLine("Plantilla \n*********************");
            ce.getEmpleadosOrdenados();
            Console.WriteLine("");
            Console.WriteLine("Plantilla ordenada por salario \n*******************");
            ce.getEmpleadosOrdenadosSegun();

            Console.WriteLine("\nIngrese la empresa:(entero 1 a 3)\n1 para IAplha\n2 para UdelaR\n3 para SpaceZ");
            string _Id = Console.ReadLine();
            try
            {
                int _Empresa = int.Parse(_Id);
                ce.getEmpleadosEmpresa(_Empresa);
            }
            catch
            {
                Console.WriteLine("Ha introducido un Id erroneo. Debe ingresar un numero entero");
            }
        }
    }
}
