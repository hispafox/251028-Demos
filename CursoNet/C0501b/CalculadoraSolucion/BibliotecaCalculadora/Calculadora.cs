namespace BibliotecaCalculadora
{
    public class Calculadora
    {
        public int Sumar(int a, int b)
        {
            return a + b ;
        }

        //public int Restar(int a, int b)
        //{
        //    return a - b;
        //}

        public int Multiplicar(int a, int b)
        {
            return a * b ;
        }

        public int Dividir(int a, int b)
        {
            //if (b == 0)
            //    throw new System.DivideByZeroException("No se puede dividir por cero.");

            return a / b;
        }

        public int Restar(int a, int b)
        {
            return a - b;
        }

        public int Potencia(int a, int b)
        {
            return (int)Math.Pow(a, b);
        }

        public int RaizCuadrada(int a)
        {
            if (a < 0)
                throw new System.ArgumentException("No se puede calcular la raíz cuadrada de un número negativo.");
            return (int)Math.Sqrt(a);
            
        }

        /// <summary>
        /// Calcula el factorial de un número entero.
        /// </summary>
        /// <param name="n">Número entero entre 0 y 20.</param>
        /// <returns>El factorial de n (n!).</returns>
        /// <exception cref="System.ArgumentException">
        /// Se lanza cuando n es negativo o mayor que 20.
        /// </exception>
        public long Factorial(int n)
        {
            if (n < 0)
                throw new System.ArgumentException("No se puede calcular el factorial de un número negativo.");
            
            if (n > 20)
                throw new System.ArgumentException("El factorial de números mayores a 20 excede la capacidad de cálculo (desbordamiento).");
    
            if (n == 0)
                return 1;
    
            long resultado = 1;
            for (int i = 1; i <= n; i++)
            {
                resultado *= i;
            }
     
            return resultado;
        }
    }
}
