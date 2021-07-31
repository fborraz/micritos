/*
 * Creado por SharpDevelop.
 * Usuario: PCnote
 * Fecha: 18/6/2017
 * Hora: 3:23 a. m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections;

namespace Micritos
{
	public class EntradaIncorrecta : Exception{}
	
	//############################################
	//#############INICIO DE MICRITOS#############
	//############################################
	class Program
	{
		public static void Main(string[] args)
		{
			ArrayList listaTerminales = new ArrayList();
			ArrayList listaOmnibus = new ArrayList();
			
			ArrayList listaChoferes = new ArrayList();
			ArrayList listaUsuarios = new ArrayList();
			ArrayList listaTerminalesSeleccionadas = new ArrayList();
			ArrayList datosSeleccionados = new ArrayList();
			ArrayList recorridos = new ArrayList();
			ArrayList listaRecorridosString = new ArrayList();
			int contadorLegajo = 1;
			int contadorNumeroUsuario = 1;
			
			
			//Creo una pantalla principal
			PantallaPrincipal pantallaprincipal = new PantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			//Inicializo MICRITOS
			pantallaprincipal.init();
		}
	}
	
	//############################################
	//#########MODULO PANTALLA PRINCIPAL##########
	//############################################
	public class PantallaPrincipal
	{
		
		ArrayList listaTerminales = new ArrayList();				//Almacena las terminales cargadas
		ArrayList listaOmnibus = new ArrayList();					//Almacena los ómnibus cargados
		ArrayList listaChoferes = new ArrayList();					//Almacena los choferes cargados	
		ArrayList listaUsuarios = new ArrayList();					//Almacena los usuarios cargados
		ArrayList listaTerminalesSeleccionadas = new ArrayList();	//Almacena los recorridos cargados
		ArrayList datosSeleccionados = new ArrayList();				//Almacena los viajes creados
		ArrayList recorridos = new ArrayList();						//Almacena un recorrido armado temporalmente
		ArrayList listaRecorridosString = new ArrayList();			//Almacena los recorridos cargados en formato string
		int contadorLegajo = 1;										//Contador para asignar numero de legajo al chofer
		int contadorNumeroUsuario = 1;								//Contador para asignar un numero al usuario
		
		//Constructor que inicializa el modulo PantallaPrincipal con sus respectivos arraylist
		public PantallaPrincipal(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			this.listaTerminales = listaTerminales;
			this.listaOmnibus = listaOmnibus;
			this.listaChoferes = listaChoferes;
			this.listaUsuarios = listaUsuarios;
			this.listaTerminalesSeleccionadas = listaTerminalesSeleccionadas;
			this.datosSeleccionados = datosSeleccionados;
			this.recorridos = recorridos;
			this.listaRecorridosString = listaRecorridosString;
			this.contadorLegajo = contadorLegajo;
			this.contadorNumeroUsuario = contadorNumeroUsuario;
		}
		
		public void init(){
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.ForegroundColor = ConsoleColor.Yellow;
			
			FuncionesGenerales.mostrarCabecera();
			
			Console.WriteLine("¿A qué módulo desea ingresar?");
			Console.WriteLine("");
			Console.WriteLine("1) Armado de recorridos");
			Console.WriteLine("2) Gestión de choferes");
			Console.WriteLine("3) Venta de pasajes");
			Console.WriteLine("4) Estadísticas");
			Console.WriteLine("5) Salir del sistema");
			try{
				int ingreso = int.Parse(Console.ReadLine());
				
				while(ingreso >= 1 && ingreso <=5){
					switch(ingreso)
					{
						case 1:
							ModuloRecorridos recorridos = new ModuloRecorridos();
							recorridos.init(this.listaTerminales, this.listaOmnibus, this.listaChoferes, this.listaUsuarios, this.listaTerminalesSeleccionadas, this.datosSeleccionados, this.recorridos, this.listaRecorridosString, this.contadorLegajo, this.contadorNumeroUsuario);
							break;
						case 2:
							moduloChoferes choferes = new moduloChoferes();
							choferes.init(this.listaTerminales, this.listaOmnibus, this.listaChoferes, this.listaUsuarios, this.listaTerminalesSeleccionadas, this.datosSeleccionados,this.recorridos, this.listaRecorridosString, this.contadorLegajo, this.contadorNumeroUsuario);
							break;
						case 3:
							moduloVentaPasajes pasajes = new moduloVentaPasajes();
							pasajes.init(this.listaTerminales, this.listaOmnibus, this.listaChoferes, this.listaUsuarios, this.listaTerminalesSeleccionadas, this.datosSeleccionados, this.recorridos, this.listaRecorridosString, this.contadorLegajo, this.contadorNumeroUsuario);
							break;
						case 4:
							moduloEstadisticas estadisticas = new moduloEstadisticas();
							estadisticas.init(this.listaTerminales, this.listaOmnibus, this.listaChoferes, this.listaUsuarios, this.listaTerminalesSeleccionadas, this.datosSeleccionados, this.recorridos, this.listaRecorridosString, this.contadorLegajo, this.contadorNumeroUsuario);
							break;
						case 5:
							Environment.Exit(-1);
							break;
					}
				}
				throw new EntradaIncorrecta();
			}
			catch(EntradaIncorrecta)//Si la opción es menor a 1 o mayor a 5  
			{
				Console.WriteLine("Ha ingresado una opción incorrecta.");
				Console.WriteLine("Presione una tecla para continuar.");
				Console.ReadKey();
				Console.Clear();
				this.init();
			}
			catch(OverflowException)//Si ingresa un numero muy grande
			{
				this.init();
			}
			catch(FormatException)//Si ingresa
			{
				this.init();
			}
		}
	}
	
	//############################################
	//############MODULO RECORRIDOS###############
	//############################################
	public class ModuloRecorridos
	{
		public string recorridosString = null;
		string terminal;
		string ciudad;
		string marca;
		int modelo;
		int capacidad;
		
		int contadorNumUnidad = 1;
		public void init(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas,ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			FuncionesGenerales.mostrarCabecera();
			Console.WriteLine("================================");
			Console.WriteLine("MODULO PARA ARMADO DE RECORRIDOS");
			Console.WriteLine("================================");
			Console.WriteLine("");
			
			Console.WriteLine("1) Alta de terminales");
			Console.WriteLine("2) Alta de ómnibus");
			Console.WriteLine("3) Alta de recorridos");
			Console.WriteLine("4) Volver");
			Console.WriteLine("");
			//
			try{
				int ingreso = int.Parse(Console.ReadLine());
				
				while(ingreso >= 1 && ingreso <=4){
					
					switch(ingreso){
						
						case 1:
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("==================");
							Console.WriteLine("ALTA DE TERMINALES");
							Console.WriteLine("==================");
							Console.WriteLine("");
							
							Console.Write("Ingrese nombre de la terminal: ");
							terminal = Console.ReadLine();
							
							Console.Write("Ingrese nombre de la ciudad: ");
							ciudad = Console.ReadLine();
							
							//Creo una terminal y la agrego a la lista de terminales
							Terminales terminales = new Terminales();
							terminales.NomTerminal = terminal;
							terminales.NomCiudad = ciudad;
							
							listaTerminales.Add(terminales);
							
							Console.WriteLine("");
							Console.WriteLine("La terminal ha sido dada de alta correctamente.");
							Console.WriteLine("Presione una tecla para continuar.");
							
							
							Console.ReadKey();
							this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							
							break;
							
						case 2:
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("===============");
							Console.WriteLine("ALTA DE OMNIBUS");
							Console.WriteLine("===============");
							Console.WriteLine("");
							
							Console.WriteLine("Ingrese la marca");
							marca = Console.ReadLine();
							Console.WriteLine("Ingrese el modelo");
							modelo = int.Parse(Console.ReadLine());
							Console.WriteLine("Ingrese la capacidad");
							capacidad = int.Parse(Console.ReadLine());
							
							Console.WriteLine("Ingrese el tipo");
							Console.WriteLine("1) Básico");
							Console.WriteLine("2) Semi-cama");
							Console.WriteLine("3) Coche-cama");
							Console.WriteLine("4) Suite");
							int opcion = int.Parse(Console.ReadLine());
							
							string tipo = null;
							
							switch(opcion)
							{
								case 1:
									tipo = "Básico";
									break;
								case 2:
									tipo = "Semi-cama";
									break;
								case 3:
									tipo = "Coche-cama";
									break;
								case 4:
									tipo = "Suite";
									break;
								default:
									Console.WriteLine("Opcion incorrecta. Intente nuevamente");
									opcion = int.Parse(Console.ReadLine());
									break;
							}
							
							//Creo un ómnibus
							Omnibus omnibus = new Omnibus();
							omnibus.Marca = marca;
							omnibus.Modelo = modelo;
							omnibus.Capacidad = capacidad;
							omnibus.Tipo = tipo;
							omnibus.NumUnidad = contadorNumUnidad;
							//lo agrego a la lista de ómnibus
							listaOmnibus.Add(omnibus);
							
							Console.WriteLine("El ómnibus fue dado de alta correctamente. A la unidad se le asignó el número: {0}",contadorNumUnidad++);
							Console.WriteLine("Presione una tecla para continuar");
							Console.ReadKey();
							
							this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							
							break;
							
						case 3:
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("====================");
							Console.WriteLine("ARMADO DE RECORRIDOS");
							Console.WriteLine("====================");
							Console.WriteLine("");
							//Verifico si la lista de terminales no tiene ninguna terminal
							if(listaTerminales.Count == 0)
							{
								Console.WriteLine("No hay terminales cargadas en el sistema. Intente nuevamente.");
								Console.WriteLine("Presione una tecla pra continuar.");
								Console.ReadKey();
								this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							}
							//Si la lista de terminales tiene una sola terminal  
							if(listaTerminales.Count == 1)
							{
								FuncionesGenerales.mostrarTerminales(listaTerminales);
								Console.WriteLine(""
								                 );
								Console.WriteLine("La terminal cargada es insuficiente para armar un recorrido. Cargue una nueva terminal.");
								Console.WriteLine("Presione una tecla para continuar.");
								Console.ReadKey();
								this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							}
							//Si tiene mas de una terminal
							else{
								Console.WriteLine("Seleccione las terminales del recorrido, ingrese 0 para finalizar.");
								Console.WriteLine("");
								//Muestro la lista de terminales
								FuncionesGenerales.mostrarTerminales(listaTerminales);
								Console.WriteLine("");
								try
								{
									int i = int.Parse(Console.ReadLine());
									
									ArrayList recorrido = new ArrayList();
									while(i != 0)
									{
										//Almaceno la terminal elegida en la variable terminalElegida de tipo Terminales
										Terminales terminalElegida = ((Terminales)listaTerminales[i-1]);
										//La agrego a un ArrayList
										recorrido.Add(terminalElegida);
										//A recorridoString le concateno las terminales que forman el recorrido
										//para luego mostrarlos										
										recorridosString += (Terminales)listaTerminales[i-1] +" - ";
										//imprime la terminal seleccionada
										Console.WriteLine(listaTerminales[i-1]);
										i = int.Parse(Console.ReadLine());
									}
									//Cuando el usuario presiona 0 termina el bucle y agrega el recorridoString
									listaRecorridosString.Add(recorridosString);
									//Vacía la variable que almacena los recorridos de tipo string
									recorridosString = null;
									//Agrega el arraylist recorrido al ArrayList
									listaTerminalesSeleccionadas.Add(recorrido);
									
									Console.WriteLine("El recorrido se ha dado de alta correctamente");
									Console.WriteLine("Presione una tecla para continuar");
									Console.ReadKey();
									
									//Vuelve al principio con los arraylist cargados
									this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
								}
								catch(ArgumentOutOfRangeException)
								{
									Console.WriteLine("Opción incorrecta. Intente nuevamente");
									Console.ReadKey();
									break;
								}
							}
							break;
						case 4:
							//Vuelve a la pantalla principal con todos los arraylist cargados
							volverPantallaPrincipal(listaTerminales, listaOmnibus,listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							break;
					}
				}
				throw new EntradaIncorrecta();
			}
			catch(EntradaIncorrecta)
			{
				Console.WriteLine("Ha ingresado una opción incorrecta");
				Console.WriteLine("Presione una tecla para continuar");
				Console.ReadKey();
				this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(OverflowException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(FormatException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
		}
		
		public void volverPantallaPrincipal(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			Console.Clear();
			PantallaPrincipal principal = new PantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			principal.init();
		}
	}
	
	//############################################
	//#############MODULO CHOFERES################
	//############################################
	public class moduloChoferes
	{
		
		string nombre;
		string apellido;
		int dni;
		public string diaElegido;
		public string [] dias;
		
		public void init(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			FuncionesGenerales.mostrarCabecera();
			
			Console.WriteLine("=======================================================");
			Console.WriteLine("MODULO PARA ALTA DE CHOFERES Y ASIGNACIÓN DE RECORRIDOS");
			Console.WriteLine("=======================================================");
			Console.WriteLine("");
			Console.WriteLine("1) Alta de choferes");
			Console.WriteLine("2) Asignación de recorridos");
			Console.WriteLine("3) Volver");
			Console.WriteLine("");
			
			try{
				int ingreso = int.Parse(Console.ReadLine());
				while(ingreso >= 1 && ingreso <= 3)
				{
					switch(ingreso)
					{
						case 1:
							
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("================");
							Console.WriteLine("ALTA DE CHOFERES");
							Console.WriteLine("================");
							Console.WriteLine("");
							
							Console.WriteLine("Ingrese el nombre");
							nombre = Console.ReadLine();
							Console.WriteLine("Ingrese el apellido");
							apellido = Console.ReadLine();
							Console.WriteLine("Ingrese el DNI");
							dni = int.Parse(Console.ReadLine());
							
							//Creo un chofer con su respectiva información
							Choferes chofer = new Choferes();
							chofer.Nombre = nombre;
							chofer.Apellido = apellido;
							chofer.Dni = dni;
							chofer.NumLegajo = contadorLegajo;
							//La lista de choferes debe tener choferes almacenados. Si los tiene, busca si en la lista de choferes existe
							//un chofer con el mismo numero de documento. 
							//Si no existe se almacena en la lista de choferes.
							//Si existe imprime un mensaje comunicandolo y vuelve al principio del moduloChoferes
							bool choferExiste = false;
							
							if(listaChoferes.Count != 0)
							{
								foreach(Choferes x in listaChoferes)
								{
									if(x.Dni == dni)
									{
										choferExiste = true;
									}
									
								}
								if(choferExiste == true)
								{
									Console.WriteLine("El chofer ya fue dado de alta en el sistema. Intente nuevamente.");
									Console.ReadKey();
									break;
								}
								else
								{
									if(choferExiste == false)
									{
										listaChoferes.Add(chofer);
										Console.WriteLine("El chofer fue dado de alta correctamente. El número de legajo asignado es: {0}.",contadorLegajo++);
										
										Console.WriteLine("Presione una tecla para continuar.");
										Console.ReadKey();
										this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
									}
								}
							}
							//Si no tiene choferes almacenados lo agrega directamente sin ningún tipo de condición
							else
							{
								listaChoferes.Add(chofer);
								Console.WriteLine("El chofer fue dado de alta correctamente. El número de legajo asignado es: {0}.",contadorLegajo++);
								
								Console.WriteLine("Presione una tecla para continuar.");
								Console.ReadKey();
								this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							}
							break;
						case 2:
							
							FuncionesGenerales.mostrarCabecera();
							
							dias = new string[]{"Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"};
							//Acá se realiza la elección del chofer previamente cargado al sistema
							Console.WriteLine("====================");
							Console.WriteLine("Seleccione el chofer");
							Console.WriteLine("====================");
							Console.WriteLine("");
							int contador = 1;
							//Se imprimen en pantalla los choferes cargados
							foreach(Choferes chofers in listaChoferes)
							{
								Console.WriteLine(contador + ") " + chofers.Nombre + " " + chofers.Apellido + " (" +chofers.NumLegajo + ")");
								contador++;
							}
							
							int seleccion = int.Parse(Console.ReadLine());
							Choferes choferEle = (Choferes)listaChoferes[seleccion-1];
							
							FuncionesGenerales.mostrarCabecera();
							
							//Aca se realiza la elección del omnibus previamente cargado al sistema
							Console.WriteLine("=====================");
							Console.WriteLine("Seleccione el omnibus");
							Console.WriteLine("=====================");
							Console.WriteLine("");
							int contBus = 1;
							//Se imprimen en pantalla los ómnibus cargados
							foreach(Omnibus omnibus in listaOmnibus)
							{
								Console.WriteLine(contBus + ") " +omnibus.ToString());
								contBus++;
							}
							seleccion = int.Parse(Console.ReadLine());
							Omnibus omnibusEle = (Omnibus)listaOmnibus[seleccion - 1];
							
							FuncionesGenerales.mostrarCabecera();
							//Acá se realiza la elección del recorrido previamente cargado al sistema
							Console.WriteLine("=======================");
							Console.WriteLine("Seleccione el recorrido");
							Console.WriteLine("=======================");
							int cont_term = 1;
							//Se imprimen en pantalla los recorridos
							foreach(String s in listaRecorridosString)
							{
								Console.WriteLine(cont_term+") "+ s);
								cont_term++;
							}
							seleccion = int.Parse(Console.ReadLine());
							//La variable de recorridoEle de tipo ArrayList almacena el recorrido.
							ArrayList recorridoEle = (ArrayList)listaTerminalesSeleccionadas[seleccion - 1];
							
							
							//Acá se realiza la elección del dia a realizar el recorrido
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("===========================================");
							Console.WriteLine("Seleccione el dia donde hacer el recorrido");
							Console.WriteLine("===========================================");
							Console.WriteLine("");
							
							int cont_Dias = 1;
							//Se imprimen en pantalla los dias
							foreach(string dia in dias){
								Console.WriteLine(cont_Dias + ") " + dia);
								cont_Dias++;}
							//SE REALIZA LA ELECCION DEL DIA
							seleccion = int.Parse(Console.ReadLine());
							//ALMACENO EL DIA ELEGIDO
							diaElegido =dias[seleccion-1];
							
							//CREO UN VIAJE CON LOS DATOS INGRESADOS
							Viajes viaje = new Viajes();
							viaje.ChoferElegido = choferEle;
							viaje.OmnibusElegido = omnibusEle;
							viaje.RecorridoElegido = recorridoEle;
							viaje.DiaElegido = diaElegido;
							
							
							bool choExiste = false;
							bool diaExiste = false;
							bool busExiste = false;
							if(datosSeleccionados.Count != 0)
							{
								//Busco en el ArrayList datosSeleccionados 
								foreach(Viajes x in datosSeleccionados)
								{
									
									if(x.ChoferElegido == choferEle)
									{
										choExiste = true;
									}
									if(x.DiaElegido == diaElegido)
									{
										diaExiste = true;
									}
									if(x.OmnibusElegido == omnibusEle)
									{
										busExiste = true;
									}
								}
								if((choExiste && diaExiste) == true)
								{
									Console.WriteLine("El chofer ya realiza un viaje ese día.");
									Console.WriteLine("Presione una tecla para continuar.");
									
									Console.ReadKey();
									break;
								}
								else
									if((busExiste && diaExiste) == true)
								{
									Console.WriteLine("El omnibus ya está reservado ese día.");
									Console.WriteLine("Presione una tecla para continuar.");
									Console.ReadKey();
									break;
								}
								else
								{
									datosSeleccionados.Add(viaje);
									Console.WriteLine("La asignación del recorrido fue dada de alta correctamente.");
									Console.WriteLine("Presione una tecla para continuar.");
									
									Console.ReadKey();
									this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
								}
							}
							else
							{
								datosSeleccionados.Add(viaje);
								Console.WriteLine("La asignación del recorrido fue dada de alta correctamente.");
								Console.WriteLine("Presione una tecla para continuar.");
								
								Console.ReadKey();
								this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							}
							break;
						case 3:
							volverPantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							break;
					}
				}
				throw new EntradaIncorrecta();
			}
			catch(EntradaIncorrecta)
			{
				Console.WriteLine("Ha ingresado una opción incorrecta.");
				Console.WriteLine("Presione una tecla para continuar.");
				Console.ReadKey();
				Console.Clear();
				this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(OverflowException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(FormatException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
		}
		
		public void volverPantallaPrincipal(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			Console.Clear();
			PantallaPrincipal principal = new PantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			principal.init();
		}
	}
	
	//############################################
	//#########MODULO DE VENTA DE PASAJES#########
	//############################################
	public class moduloVentaPasajes
	{
		string nombre;
		string apellido;
		string fechaNacimiento;
		int dni;
		int numUsuario;
		
		ArrayList recorridosValidos = new ArrayList();//Almacena los recorridos que tienen asignados un chofer y es el que se recorre cuando el
		//usuario ingresa las terminales de partida y las terminales de arribo
		
		public void init(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			FuncionesGenerales.mostrarCabecera();
			
			Console.WriteLine("================================================");
			Console.WriteLine("MODULO PARA ALTA DE USUARIOS Y COMPRA DE PASAJES");
			Console.WriteLine("================================================");
			Console.WriteLine("");
			Console.WriteLine("1) Alta de usuarios");
			Console.WriteLine("2) Compra de pasajes");
			Console.WriteLine("3) Volver");
			try{
				int ingreso = int.Parse(Console.ReadLine());
				while(ingreso >= 1 && ingreso <= 3){
					switch(ingreso)
					{
						case 1:
							
							bool dniExiste = false;
							
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("================");
							Console.WriteLine("ALTA DE USUARIOS");
							Console.WriteLine("================");
							Console.WriteLine("");
							
							Console.WriteLine("Ingrese el nombre");
								nombre = Console.ReadLine();
							Console.WriteLine("Ingrese el apellido");
								apellido = Console.ReadLine();
							Console.WriteLine("Ingrese el DNI");
								dni = int.Parse(Console.ReadLine());
							Console.WriteLine("Ingrese la fecha de nacimiento");
								fechaNacimiento = Console.ReadLine();
								
							//Creo un usuario con sus respectivos datos
							Usuarios usuario = new Usuarios();
							usuario.Nombre = nombre;
							usuario.Apellido = apellido;
							usuario.Dni = dni;
							usuario.FechaNac = fechaNacimiento;
							usuario.NumUsuario = contadorNumeroUsuario;
							usuario.PasajesComprados = 0;
							
							if(listaUsuarios.Count != 0)
							{
								foreach(Usuarios x in listaUsuarios)
								{
									if(x.Dni == dni)
									{
										dniExiste = true;
									}
								}
								if(dniExiste == true)
								{
									Console.WriteLine("El usuario ya fue dado de alta en el sistema. Intente nuevamente.");
									Console.WriteLine("Presione una tecla para continuar.");
									Console.ReadKey();
									break;
								}
								else
								{
									if(dniExiste == false)
									{
										listaUsuarios.Add(usuario);
										Console.WriteLine("El usuario fue dado de alta correctamente. Se le asignó el número {0}.",contadorNumeroUsuario);
										Console.WriteLine("Presione una tecla para continuar.");
										Console.ReadKey();
										contadorNumeroUsuario++;
										this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);	
									}
								}
							}
							else
							{
								listaUsuarios.Add(usuario);
								Console.WriteLine("El usuario fue dado de alta correctamente. Se le asignó el número {0}.",contadorNumeroUsuario);
								contadorNumeroUsuario++;
								Console.WriteLine("Presione una tecla para continuar.");
								Console.ReadKey();
								this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							}
							
							break;
						
						case 2:
							
							int contadorViajes = 1;
							
							bool terminalPart = false;
							bool terminalArri = false;
							bool numUserExiste = false;
							bool dniUserExiste = false;
							
							int posTerPartida = 0;
							int posTerArribo = 0;
							int posUsuario = 0;
							Usuarios usuarioQueCompro;
							
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("=================");
							Console.WriteLine("COMPRA DE PASAJES");
							Console.WriteLine("=================");
							Console.WriteLine("");
							
							Console.WriteLine("Ingrese el numero de usuario");
							numUsuario = int.Parse(Console.ReadLine());
							Console.WriteLine("Ingrese el DNI del usuario");
							dni = int.Parse(Console.ReadLine());
							if(listaUsuarios.Count != 0)
							{
								for(int i = 0; i < listaUsuarios.Count; i++)
								{
									Usuarios user = (Usuarios)listaUsuarios[i];
									if(user.NumUsuario == numUsuario)
									{
										numUserExiste = true;
										posUsuario = i;
									}
									if(user.Dni == dni)
									{
										dniUserExiste = true;
										posUsuario = i;
									}
								}
								if((numUserExiste && dniUserExiste) == false){
									Console.WriteLine("El usuario ingresado no existe en el sistema.");
									Console.WriteLine("Presione una tecla para continuar.");
									Console.ReadKey();
									break;
								}
								
								if((numUserExiste && dniUserExiste) == true){
									//variable que almacena el usuario
									
									Console.WriteLine("Seleccione la terminal de partida");
									FuncionesGenerales.mostrarTerminales(listaTerminales);
									int terminalPartidaElegida = int.Parse(Console.ReadLine());
									Terminales terminalPartida = (Terminales)listaTerminales[terminalPartidaElegida - 1];
									
									Console.WriteLine("Seleccione terminal de arribo");
									FuncionesGenerales.mostrarTerminales(listaTerminales);
									int terminalArriboElegida = int.Parse(Console.ReadLine());
									Terminales terminalArribo = (Terminales)listaTerminales[terminalArriboElegida - 1];
									
									//Verifico si la terminal de partida y la terminal de arribo seleccionadas son iguales
									
									if(terminalPartida == terminalArribo)
									{
										Console.WriteLine("La terminal de partida y arribo es la misma.");
										Console.WriteLine("Presione una tecla para continuar.");
										Console.ReadKey();
										break;
									}
									
									
									//Si son distintas busco en el ArrayList datosSeleccionados
									else
									{
										foreach(Viajes x in datosSeleccionados)
										{
											for(int i = 0; i < x.RecorridoElegido.Count; i++)
											{
												Terminales termP = (Terminales)x.RecorridoElegido[i];
												if(terminalPartida == termP)
												{
													terminalPart = true;
												}
											}
											for(int j = 0; j < x.RecorridoElegido.Count; j++)
											{
												Terminales termA = (Terminales)x.RecorridoElegido[j];
												if(terminalArribo == termA)
												{
													terminalArri = true;
												}
											}
											if((terminalPart && terminalArri) == true)
											{
												recorridosValidos.Add(x);
											}
										}
										if((terminalPart && terminalArri) == false)
										{
											Console.WriteLine("No existe ningún recorrido con las terminales de partida y arribo seleccionadas.");
											Console.WriteLine("Presione una tecla para continuar.");
											Console.ReadKey();
											break;
										}
										if((terminalPart && terminalArri) == true){
											Console.Clear();
											Console.WriteLine("Seleccione el itinerario");
											Console.WriteLine("");
											
											foreach(Viajes viaje in recorridosValidos){
												
												for(int t = 0; t < viaje.RecorridoElegido.Count; t++){
													Terminales termPart = (Terminales)viaje.RecorridoElegido[t];
													
													if(terminalPartida == termPart)
													{
														posTerPartida = t;
													}
												}
												for(int z = 0; z < viaje.RecorridoElegido.Count; z++)
												{
													Terminales termArri = (Terminales)viaje.RecorridoElegido[z];
													if(terminalArribo == termArri)
													{
														posTerArribo = z;
													}
												}
												Console.WriteLine(contadorViajes + ") Saliendo de {0} y llegando a {1} <{2} paradas intermedias, {3}, {4}>",terminalPartida, terminalArribo,Math.Abs((posTerPartida + 1) - (posTerArribo)), viaje.DiaElegido, viaje.OmnibusElegido.Tipo);
												contadorViajes++;
											}
											
											int itinerarioElegido = int.Parse(Console.ReadLine());
											//Creo una variable de tipo Viajes que almacena 
											Viajes viajeElegido = (Viajes)recorridosValidos[itinerarioElegido - 1];
											//Creo una variable de tipo Usuario al que se le asigna el usuario.
											usuarioQueCompro = (Usuarios)listaUsuarios[posUsuario];
											
											Console.WriteLine("¿Cuantos pasajes desea?");
											
											int cantPasajes = int.Parse(Console.ReadLine());
											if(cantPasajes > viajeElegido.OmnibusElegido.Capacidad)
											{
												Console.WriteLine("La cantidad de pasajes que solicitó exede la capacidad del omnibus. Hay ({0}) lugares disponibles.", viajeElegido.OmnibusElegido.Capacidad);
												Console.WriteLine("Presione una tecla para continuar");
												recorridosValidos.Clear();
												contadorViajes = 1;
												Console.ReadKey();
												break;
											}
											else
											{
												//Le resto a la capacidad del ómnibus la cantidad de pasajes que solicita el usuario
												int lugaresSobrantes = (viajeElegido.OmnibusElegido.Capacidad - cantPasajes);
												//Ahora el ómnibus del viaje elegido tiene su capacidad reducida 
												viajeElegido.OmnibusElegido.Capacidad = lugaresSobrantes;
												//Le sumo al usuario los pasajes que compró más los que tenia anteriormente 
												usuarioQueCompro.PasajesComprados += cantPasajes;
												
												Console.WriteLine("La venta se ha realizado con éxito.");
												Console.WriteLine("Presione una tecla para continuar.");
												//Limpio el listado de recorridos
												recorridosValidos.Clear();
												Console.ReadKey();
												this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
											}
										}
									}
								}
							}
							else
							{
								Console.WriteLine("");
								Console.WriteLine("No hay usuarios cargados en el sistema. Intente nuevamente.");
								Console.WriteLine("Presione una tecla para continuar.");
								Console.ReadKey();
								this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
								break;
							}
							
							Console.WriteLine("");
							break;
						case 3:
							volverPantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							break;
					}
				}
				throw new EntradaIncorrecta();
			}
			catch(EntradaIncorrecta)
			{
				Console.WriteLine("Ha ingresado una opción incorrecta");
				Console.WriteLine("Presione una tecla para continuar");
				Console.ReadKey();
				Console.Clear();
				this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(OverflowException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(FormatException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes,listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
		}
		public void volverPantallaPrincipal(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			Console.Clear();
			PantallaPrincipal principal = new PantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			principal.init();
		}
	}
	
	//############################################
	//###########MODULO DE ESTADÍSTICAS###########
	//############################################
	public class moduloEstadisticas
	{
		public void init(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			int cantTotalPasajesVendidos = 0;
			
			FuncionesGenerales.mostrarCabecera();
			
			Console.WriteLine("======================");
			Console.WriteLine("MODULO DE ESTADISTICAS");
			Console.WriteLine("======================");
			Console.WriteLine("");
			
			Console.WriteLine("1) Consultar total de pasajes vendidos");
			Console.WriteLine("2) Consultar usuarios");
			Console.WriteLine("3) Consultar terminal como partida");
			Console.WriteLine("4) Consultar terminal como partida");
			Console.WriteLine("5) Volver");
			try
			{
				int ingreso = int.Parse(Console.ReadLine());
				
				while(ingreso >= 1 && ingreso <= 5)
				{
					switch(ingreso)
					{
						case 1:
							FuncionesGenerales.mostrarCabecera();
							
							Console.WriteLine("=========================");
							Console.WriteLine("Total de pasajes vendidos");
							Console.WriteLine("=========================");
							Console.WriteLine("");
							foreach(Usuarios usuario in listaUsuarios)
							{
								cantTotalPasajesVendidos += usuario.PasajesComprados;
							}
							Console.WriteLine("En total se han vendido ({0}) pasajes.", cantTotalPasajesVendidos);
							Console.WriteLine("Presione una tecla para continuar.");
							Console.ReadKey();
							this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							
							break;
						case 2:
							FuncionesGenerales.mostrarCabecera();
							Console.WriteLine("=============================");
							Console.WriteLine("Listado de ventas por usuario");
							Console.WriteLine("=============================");
							Console.WriteLine("");
							foreach(Usuarios usuario in listaUsuarios)
							{
								if(usuario.PasajesComprados > 0)
								{
									Console.WriteLine(usuario.ToString());
								}
							}
					
							Console.WriteLine("Presione una tecla para continuar.");
							Console.ReadKey();
							
							this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							break;
						case 3:
							Console.WriteLine("Sección en construcción");
							Console.WriteLine("Presione una tecla para continuar.");
							Console.ReadKey();
							this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							break;
						case 4:
							Console.WriteLine("Sección en construcción");
							Console.WriteLine("Presione una tecla para continuar.");
							Console.ReadKey();
							this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							break;
						case 5:
							volverPantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
							break;
					}
				}
				throw new EntradaIncorrecta();
			}
			catch(EntradaIncorrecta)
			{
				Console.WriteLine("Ha ingresado una opción incorrecta");
				Console.WriteLine("Presione una tecla para continuar");
				Console.ReadKey();
				Console.Clear();
				this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(OverflowException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
			catch(FormatException)
			{
				this.init(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			}
		}
		public void volverPantallaPrincipal(ArrayList listaTerminales, ArrayList listaOmnibus, ArrayList listaChoferes, ArrayList listaUsuarios, ArrayList listaTerminalesSeleccionadas, ArrayList datosSeleccionados, ArrayList recorridos, ArrayList listaRecorridosString, int contadorLegajo, int contadorNumeroUsuario)
		{
			Console.Clear();
			PantallaPrincipal pantallaprincipal = new PantallaPrincipal(listaTerminales, listaOmnibus, listaChoferes, listaUsuarios, listaTerminalesSeleccionadas, datosSeleccionados, recorridos, listaRecorridosString, contadorLegajo, contadorNumeroUsuario);
			pantallaprincipal.init();
		}
	}
	

	public class Persona
	{
		private string nombre;
		private string apellido;
		private int dni;
		
		public string Nombre
		{
			set{this.nombre = value;}
			get{return this.nombre;}
		}
		public string Apellido
		{
			set{this.apellido = value;}
			get{return this.apellido;}
		}
		public int Dni
		{
			set{this.dni = value;}
			get{return this.dni;}
		}
	}
	public class Choferes: Persona
	{
		private int numLegajo;

		public int NumLegajo
		{
			set{this.numLegajo = value;}
			get{return this.numLegajo;}
		}
		public string mostrar()
		{
			return this.Nombre + " " + this.Apellido + " <" + this.NumLegajo +">";
		}
	}
	public class Usuarios: Persona
	{
		private string fechaNac;
		private int numUsuario;
		private int pasajesComprados;
		
		public string FechaNac
		{
			set{fechaNac = value;}
			get{return fechaNac;}
		}
		public int NumUsuario
		{
			set{numUsuario = value;}
			get{return numUsuario;}
		}
		public int PasajesComprados
		{
			set{this.pasajesComprados = value;}
			get{return this.pasajesComprados;}
		}
		public override string ToString()
		{
			return string.Format("{0} {1} ({2})", Nombre, Apellido, PasajesComprados);
		}

		
	}
	public class Omnibus
	{
		private string marca;
		private int modelo;
		private int capacidad;
		private int numUnidad;
		private string tipo;
		
		
		public string Marca
		{
			set{this.marca=value;}
			get{return this.marca;}
		}
		public int Modelo
		{
			set{this.modelo = value;}
			get{return this.modelo;}
		}
		public int Capacidad
		{
			set{this.capacidad = value;}
			get{return this.capacidad;}
		}
		public int NumUnidad
		{
			set{this.numUnidad = value;}
			get{return this.numUnidad;}
		}
		public string Tipo
		{
			set{this.tipo = value;}
			get{return this.tipo;}
		}
		
		public override string ToString()
		{
			return string.Format(numUnidad + " ({0} - {1}, {4},{2} ) ", marca, modelo, capacidad, numUnidad, tipo);
		}
		
		//{
		//	return  this.NumUnidad + " " +"<" + this.Marca + " - " + this.Modelo + ", " + this.Tipo + ", " + this.Capacidad;
		//}
		
	}
	public class Terminales
	{
		private string nomTerminal;
		private string nomCiudad;
		
		public string NomTerminal
		{
			set{nomTerminal = value;}
			get{return nomTerminal;}
		}
		public string NomCiudad
		{
			set{nomCiudad = value;}
			get{return nomCiudad;}
		}
		
		public override string ToString()
		{
			return string.Format("{0}", NomTerminal);
		}
	}
	public class Viajes
	{
		private ArrayList recorridoElegido;
		private Omnibus omnibusElegido;
		private Choferes choferElegido;
		private string diaElegido;
		
		public ArrayList RecorridoElegido
		{
			set{this.recorridoElegido = value;}
			get{return this.recorridoElegido;}
		}
		public Omnibus OmnibusElegido
		{
			set{this.omnibusElegido = value;}
			get{return this.omnibusElegido;}
		}
		public Choferes ChoferElegido
		{
			set{this.choferElegido = value;}
			get{return this.choferElegido;}
		}
		public string DiaElegido
		{
			set{this.diaElegido = value;}
			get{return this.diaElegido;}
		}
	}
	
	public class FuncionesGenerales
	{
		public static void mostrarCabecera()
		{
			Console.Clear();
			Console.WriteLine("***********************************************************************************************************************");
			Console.WriteLine("*****                                                  MICRITOS                                                   *****");
			Console.WriteLine("***********************************************************************************************************************");
			Console.WriteLine("");
			
		}
		
		public static void mostrarTerminales(ArrayList listaTerminales)
		{
			int contador = 1;
			foreach(Terminales s in listaTerminales){
				Console.WriteLine(contador + ") " + s.NomTerminal);
				contador = contador + 1;
			}
		}
	}
}