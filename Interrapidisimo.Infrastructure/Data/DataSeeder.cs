using Microsoft.EntityFrameworkCore;
using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Verificar si ya hay datos
            if (await context.Materias.AnyAsync() || await context.Profesores.AnyAsync() || await context.Estudiantes.AnyAsync())
            {
                return; // Ya hay datos
            }

            // Crear las 10 materias requeridas
            var materias = new List<Materia>
            {
                new Materia { Nombre = "Matemáticas I", Codigo = "MAT001", Descripcion = "Fundamentos de matemáticas básicas", Creditos = 3 },
                new Materia { Nombre = "Física I", Codigo = "FIS001", Descripcion = "Introducción a la física clásica", Creditos = 3 },
                new Materia { Nombre = "Química General", Codigo = "QUI001", Descripcion = "Principios básicos de química", Creditos = 3 },
                new Materia { Nombre = "Programación I", Codigo = "PRG001", Descripcion = "Fundamentos de programación", Creditos = 3 },
                new Materia { Nombre = "Bases de Datos", Codigo = "BD001", Descripcion = "Diseño y gestión de bases de datos", Creditos = 3 },
                new Materia { Nombre = "Álgebra Lineal", Codigo = "ALG001", Descripcion = "Operaciones con matrices y vectores", Creditos = 3 },
                new Materia { Nombre = "Estadística", Codigo = "EST001", Descripcion = "Análisis estadístico y probabilidad", Creditos = 3 },
                new Materia { Nombre = "Inglés Técnico", Codigo = "ING001", Descripcion = "Inglés aplicado a ciencias exactas", Creditos = 3 },
                new Materia { Nombre = "Ética Profesional", Codigo = "ETI001", Descripcion = "Principios éticos en el ejercicio profesional", Creditos = 3 },
                new Materia { Nombre = "Metodología de Investigación", Codigo = "MET001", Descripcion = "Técnicas de investigación científica", Creditos = 3 }
            };

            context.Materias.AddRange(materias);
            await context.SaveChangesAsync();

            // Crear los 5 profesores requeridos
            var profesores = new List<Profesor>
            {
                new Profesor 
                { 
                    Nombre = "Carlos", 
                    Apellido = "Rodríguez", 
                    Email = "carlos.rodriguez@universidad.edu", 
                    Telefono = "3001234567", 
                    Especialidad = "Matemáticas y Álgebra" 
                },
                new Profesor 
                { 
                    Nombre = "Ana", 
                    Apellido = "Martínez", 
                    Email = "ana.martinez@universidad.edu", 
                    Telefono = "3007654321", 
                    Especialidad = "Física y Química" 
                },
                new Profesor 
                { 
                    Nombre = "Luis", 
                    Apellido = "García", 
                    Email = "luis.garcia@universidad.edu", 
                    Telefono = "3009876543", 
                    Especialidad = "Programación y Bases de Datos" 
                },
                new Profesor 
                { 
                    Nombre = "María", 
                    Apellido = "López", 
                    Email = "maria.lopez@universidad.edu", 
                    Telefono = "3003456789", 
                    Especialidad = "Estadística e Inglés" 
                },
                new Profesor 
                { 
                    Nombre = "Jorge", 
                    Apellido = "Herrera", 
                    Email = "jorge.herrera@universidad.edu", 
                    Telefono = "3005432109", 
                    Especialidad = "Ética y Metodología" 
                }
            };

            context.Profesores.AddRange(profesores);
            await context.SaveChangesAsync();

            // Asignar 2 materias a cada profesor (5 profesores × 2 materias = 10 materias)
            var materiasProfesores = new List<MateriaProfesor>
            {
                // Carlos Rodríguez - Matemáticas y Álgebra
                new MateriaProfesor { MateriaId = materias[0].Id, ProfesorId = profesores[0].Id }, // Matemáticas I
                new MateriaProfesor { MateriaId = materias[5].Id, ProfesorId = profesores[0].Id }, // Álgebra Lineal

                // Ana Martínez - Física y Química
                new MateriaProfesor { MateriaId = materias[1].Id, ProfesorId = profesores[1].Id }, // Física I
                new MateriaProfesor { MateriaId = materias[2].Id, ProfesorId = profesores[1].Id }, // Química General

                // Luis García - Programación y Bases de Datos
                new MateriaProfesor { MateriaId = materias[3].Id, ProfesorId = profesores[2].Id }, // Programación I
                new MateriaProfesor { MateriaId = materias[4].Id, ProfesorId = profesores[2].Id }, // Bases de Datos

                // María López - Estadística e Inglés
                new MateriaProfesor { MateriaId = materias[6].Id, ProfesorId = profesores[3].Id }, // Estadística
                new MateriaProfesor { MateriaId = materias[7].Id, ProfesorId = profesores[3].Id }, // Inglés Técnico

                // Jorge Herrera - Ética y Metodología
                new MateriaProfesor { MateriaId = materias[8].Id, ProfesorId = profesores[4].Id }, // Ética Profesional
                new MateriaProfesor { MateriaId = materias[9].Id, ProfesorId = profesores[4].Id }  // Metodología de Investigación
            };

            context.MateriaProfesor.AddRange(materiasProfesores);
            await context.SaveChangesAsync();

            // Crear estudiantes de ejemplo
            var estudiantes = new List<Estudiante>
            {
                new Estudiante 
                { 
                    Nombre = "Juan Carlos", 
                    Apellido = "Pérez", 
                    Email = "juan.perez@estudiante.edu", 
                    Telefono = "3101234567", 
                    FechaNacimiento = new DateTime(2000, 5, 15), 
                    Documento = "1234567890",
                    CreditosSeleccionados = 0
                },
                new Estudiante 
                { 
                    Nombre = "María Elena", 
                    Apellido = "González", 
                    Email = "maria.gonzalez@estudiante.edu", 
                    Telefono = "3107654321", 
                    FechaNacimiento = new DateTime(1999, 8, 22), 
                    Documento = "1234567891",
                    CreditosSeleccionados = 0
                },
                new Estudiante 
                { 
                    Nombre = "Diego", 
                    Apellido = "Ramírez", 
                    Email = "diego.ramirez@estudiante.edu", 
                    Telefono = "3109876543", 
                    FechaNacimiento = new DateTime(2001, 3, 10), 
                    Documento = "1234567892",
                    CreditosSeleccionados = 0
                },
                new Estudiante 
                { 
                    Nombre = "Sofía", 
                    Apellido = "Castillo", 
                    Email = "sofia.castillo@estudiante.edu", 
                    Telefono = "3103456789", 
                    FechaNacimiento = new DateTime(2000, 11, 30), 
                    Documento = "1234567893",
                    CreditosSeleccionados = 0
                },
                new Estudiante 
                { 
                    Nombre = "Andrés", 
                    Apellido = "Torres", 
                    Email = "andres.torres@estudiante.edu", 
                    Telefono = "3105432109", 
                    FechaNacimiento = new DateTime(1999, 12, 5), 
                    Documento = "1234567894",
                    CreditosSeleccionados = 0
                }
            };

            context.Estudiantes.AddRange(estudiantes);
            await context.SaveChangesAsync();
        }
    }
}
