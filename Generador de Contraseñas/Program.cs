using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_Contraseñas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Contraseña de 8 - 20 caracteres
             * Van a contener al menos una minuscula
             * Van a contener al menos una mayusacula
             * Un numero y un caracter especial de 6 */

            //Variables
            string nombreUsuario, opcion, contraseña;

            //Declaramos una tupla de nombre verificarContraseña para que reciba dos valores del metodo que valida la contraseña
            (bool contraseñaValida, string mensajeError) verificarContraseña;

            //Titulo
            Console.WriteLine("\t\tRegistro\n\n");

            //Pedimos el nombre de usuario
            Console.WriteLine("Ingrese un nombre de usuario: ");
            nombreUsuario = Console.ReadLine();

            //Preguntamos si se desea hacer uso del generador de contraseñas o hacerla manual
            Console.WriteLine("¿Desea que le generemos una contraseña segura? (si/no): ");
            opcion = Console.ReadLine();

            opcion = opcion.ToLower(); //Comvertimos a minuscula la respuesta del usuario por si ingresa su respuesta (SI/NO) en mayuscula

            //Seguimos una de las dos posibles opciones
            switch (opcion) 
            {
                case "si":
                    //Instanciamos a la clase Contraseña para poder usarla
                    Contraseña contraseña1 = new Contraseña();

                    //Llamamos a su metodo GenerarContraseña y le asignamos lo que devuelve a nuestra variable local contraseña
                    contraseña = contraseña1.GenerarContraseña();

                    Console.WriteLine($"Esta es la contraseña que generamos para ti, guardala en un lugar seguro: {contraseña}");

                    Console.WriteLine("\nPresiona cualquier tecla para terminar tu registro ");
                    Console.ReadKey();
                    Console.Clear();

                    //Mostrar un resumen de los datos
                    Console.WriteLine($"\nTus datos de acceso son los siguientes:\n\tusuario:{nombreUsuario}\n\tcontraseña:{contraseña}");

                    break;

                case "no":
                    Console.WriteLine("\nIngrese una contraseña segura que contenga de 8 a 20 caracteres, incluido un numero, una minuscula, una mayuscula y un caracter especial ($%#&!?): ");
                    contraseña = Console.ReadLine();

                    //Instanciamos a la clase contraseña
                    Contraseña contraseña2 = new Contraseña();

                    //Le asisgnamos a la tupla lo que devuleva el metodo Comprobar Contraseña y le mandamos como argumento a la variable local contraseña
                    verificarContraseña = contraseña2.ComprobarContraseña(contraseña);

                    //Si el primer elemento de la tupla (bool contraseñaValida) es true, entonces ingresamos al if.
                    if (verificarContraseña.contraseñaValida) 
                    {
                        Console.WriteLine("\nPresiona cualquier tecla para terminar tu registro ");
                        Console.ReadKey();
                        Console.Clear();

                        //Mostrar un resumen de los datos
                        Console.WriteLine($"\nTus datos de acceso son los siguientes:\n\tusuario:{nombreUsuario}\n\tcontraseña:{contraseña}");
                    }
                    //De lo contrasrio mostramos el mensaje de error devuelto por el metodo y agregamos uno extra
                    else
                    {
                        //Usamos el 2 elemento de la tupla (mensajeError) al que se le asigno una de las devoluviones del metodo, para despues mostrarlo
                        Console.WriteLine(verificarContraseña.mensajeError + "Ingresa una contraseña valida");
                    }

                    break;
            }

        }
    }
    //Se crea una clase para manejar los campos y metodos que van a generar la contraseña
    class Contraseña
    {
        //Campos
        //4 colecciones de caracteres para escoger y generar la contraseña
        string numeros = "0123456789";
        string letrasMin = "abcdefghijklmnopqrstuvwxyz";
        string letrasMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string caracterEspecial = "$%#&!?";
        //Contadores para verificar el numero de caracteres de casa grupo
        int numContiene = 0, minContiene = 0, mayContiene = 0, espContiene = 0;

        //Metodo para generar la contraseña
        public string GenerarContraseña()
        {
            //Aqui guardaremos la contraseña
            string contraseñaGenerada = "";

            //Instanciamos a la clase random
            Random random = new Random();

            //Declaramos una var que guarda el tamaño que tendra la contraseña. Generamos un numero aleatorio para determinar una long entre 8 y 20 caracteres y se lo asignamos a la variable.
            int longitudContraseña = random.Next(8, 21);

            //Variables que van a determinar el numero de caracteres que se usarian de cada grupo basandose en un % de la long de la contraseña.
            double numTener = longitudContraseña * .15; // 15% de los caracteres numeros
            double minTener = longitudContraseña * .35;
            double mayTener = longitudContraseña * .35;
            double espTener = longitudContraseña * .15;

            //Variable de tipo char que va a almacenar a cada uno de los caracteres que van a conformar a la contraseña.
            char caracterEscogido;

            //Usamos una iteracion while para ir colocando un caracter (4 grupo) hasta que completamos la long que se establecio anteriormente
            while (contraseñaGenerada.Length < longitudContraseña) 
            {
                //Volvemos a usar un numero aleatorio para seleccionar uno de los 4 grupos de string
                switch (random.Next(0, 4))
                {
                    case 0:
                        //Si los caracteres numericos que contiene la contraseña son menores a los que debe contener, entonces ingresa al bloque de codigo y los genera.
                        if (numContiene < numTener)
                        {
                            //A caracterEscogido se le va a asignar un caracter aleatorio de los contenidos en el string numeros, basandose en el indice y apoyandose en Lenght.
                            caracterEscogido = numeros[random.Next(numeros.Length)];
                            //Se le acumula el caracter escogido a random a la contraseña generada
                            contraseñaGenerada += caracterEscogido;
                            //Se aumenta en 1 a los caracteres numericos que contiene la contraseña
                            numContiene++;
                        }
                        break;

                    case 1:
                        if (minContiene < minTener)
                        {
                            caracterEscogido = letrasMin[random.Next(letrasMin.Length)];
                            contraseñaGenerada += caracterEscogido;
                            minContiene++;  
                        }
                        break;

                    case 2:
                        if (mayContiene < minTener)
                        {
                            caracterEscogido = letrasMay[random.Next(letrasMay.Length)];
                            contraseñaGenerada += caracterEscogido;
                            mayContiene++;
                        }
                        break;

                    case 3:
                        if (espContiene < minTener)
                        {
                            caracterEscogido = caracterEspecial[random.Next(caracterEspecial.Length)];
                            contraseñaGenerada += caracterEscogido;
                            espContiene++;
                        }
                        break;
                }
            }
            return contraseñaGenerada;
        }

        //Metodo para comprobar contraseña
        public (bool, string) ComprobarContraseña(string contraseñaPa)
        {
            //Variable que guardara el valor bool cuando compruebe todas las caracteristicas de la contraseña
            bool contraseñaValida = false;

            //variables para cada cristerio de la contraseña
            bool hayNumero = false, hayMinuscula = false, hayMayuscula = false, hayEspecial = false;

            //Variable que tendra el mensaje de error
            string mensajeError = "";

            //Verificar primero que se cumpla la longitud
            if (contraseñaPa.Length >= 8 && contraseñaPa.Length <= 20)
            {
                //Verificamos que contenga al menos un numero
                foreach (char elemento in numeros)
                {
                    //Si el elemento de numeros se encuentra en la contraseña dada por el usuario, entonces se ingresa al if y hayNumero es convierte en true
                    if (contraseñaPa.IndexOf(elemento) >= 0)
                    {
                        hayNumero = true;
                        break; //La instruccion break fuerza el foreach para que termine en el momento en que encuentra un numero.
                    }
                    else
                    {
                        hayNumero = false;
                        mensajeError = "La contraseña debe contener al menos un numero\n\n";
                    }
                }
                if (hayNumero)
                {
                    //Verificamos que contenga al menos una letra minuscula
                    foreach (char elemento in letrasMin)
                    {
                        if (contraseñaPa.IndexOf(elemento) >= 0)
                        {
                            hayMinuscula = true;
                            break; //La instruccion break fuerza el foreach para que termine en el momento en que encuentra una letra minuscula.
                        }
                        else
                        {
                            hayMinuscula = false;
                            mensajeError = "La contraseña debe contener al menos una letra minuscula";
                        }
                    }
                    if (hayMinuscula)
                    {
                        //Verificamos que contenga al menos una letra mayuscula
                        foreach (char elemento in letrasMay)
                        {
                            if (contraseñaPa.IndexOf(elemento) >= 0)
                            {
                                hayMayuscula = true;
                                break; //La instruccion break fuerza el foreach para que termine en el momento en que encuentra una letra mayuscula.
                            }
                            else
                            {
                                hayMayuscula = false;
                                mensajeError = "La contraseña debe contener al menos una                            letra mayuscula";
                            }
                        }
                    }
                    if (hayMayuscula)
                    {
                        //Verificamos que contenga al menos un especial
                        foreach (char elemento in caracterEspecial)
                        {
                            if (contraseñaPa.IndexOf(elemento) >= 0)
                            {
                                hayEspecial = true;
                                break; //La instruccion break fuerza el foreach para que termine en el momento en que encuentra un caracter especial.
                            }
                            else
                            {
                                hayEspecial = false;
                                mensajeError = "La contraseña debe contener al menos                        un caracter especial";
                            }
                        }
                    }
                }
                //Verificamos que exista un numero, una minuscula, una mayuscula y un caracter especial
                if (hayNumero && hayMinuscula && hayMayuscula && hayEspecial)
                {
                    //Si la contraseña cumple con todos los requisitos, entonces devolvemos un true
                    contraseñaValida = true;
                }
                else
                {
                    //Si la contraseña no cumple con los requisitos minimos, entonces devolvemos un false
                    contraseñaValida = false;
                }
            }
            else
            {
                //Si la contraseña no cumple con la longitud requerida, se lo indicamos al usuario
                mensajeError = "La contraseña debe contener entre 8-20 caracteres";
                contraseñaValida = false;
            }
            //Devolvemos una tupla conformada por el booleano de la contraseña y por el mensaje de error
            return (contraseñaValida, mensajeError);
        }
    }
}
