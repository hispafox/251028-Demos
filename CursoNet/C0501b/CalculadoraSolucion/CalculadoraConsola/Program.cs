// See https://aka.ms/new-console-template for more information
using BibliotecaCalculadora;

Console.WriteLine("Hello, World!");

// Crear la instancia de la calculadora
Calculadora calculadora = new Calculadora();

// Sumar dos números
int resultadoSuma = calculadora.Sumar(5, 3);

// mostrar el resultado
Console.WriteLine($"La suma de 5 y 3 es: {resultadoSuma}");

Console.ReadLine();

