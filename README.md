![](./images/header.png)

OFA es una aplicación web destinada al monitoreo de redes IPv4. Surge para cumplir con las necesidades de un software moderno y efectivo que se aboque en el monitoreo de la red de la empresa cliente, una importante cadena de supermercados del pais. La solucion fue desarrollada como proyecto de fin de carrera para el instituto BIOS.

A continuacion presento una sintesis de la documentacion generada para el proyecto.

# INTRODUCCIÓN (1)

### 1.1 - Presentación de equipo
El equipo está únicamente conformado por Claudio Tilbe, de nacionalidad uruguaya, desarrollador a cargo de la documentación y codificación de la solución.

### 1.2 - Presentación de cliente
Una cadena de supermercados mayoristas nacional que cuenta con varias sucursales de mayor y menor envergadura a lo largo del país, se pone en posición de cliente, solicitando un sistema a través del cual pueda monitorear toda su red; desde servidores de importancia en su sucursal central hasta pequeños equipos que sean puntos finales de la red.

### 1.3 - Introducción a la idea

Actualmente trabajo en el área de soporte e infraestructura perteneciente al cliente.
Dentro de esta área hay varias tareas y funciones a desempeñar, de entre las mismas destaca el monitoreo de dispositivos de red, esencial a la hora de detectar anomalías en el flujo normal de trabajo. Detectar problemas de conexión en dispositivos a tiempo puede ser la diferencia entre un problema minúsculo o un mal mayor, especialmente si hablamos de servidores o similares.
Con esta premisa y con la idea de brindar un Software que permita monitorear, organizar y alertar al usuario en caso de incidentes surge OFA. Esta aplicación web, por medio de varias funcionalidades, va a permitir el monitoreo de la red, no solo desde la sucursal central hacia el resto de la Red, sino también desde cada respectivo grupo de operadores vinculado una determinada sucursal y/u otras subredes contiguas.
OFA tiene un sistema por el cual identifica el riesgo según el estado y características de un dispositivo, y alerta, notifica o ignora basado en cómo el usuario haya configurado ese determinado dispositivo.
La distribución de usuarios y el hecho de brindar acceso a cada grupo de operadores al monitoreo de su propia sucursal da como ventaja una configuración más precisa del Software, ya que comprenden el contexto, finalidades y funciones de cada dispositivo informático en sus subredes.
También existe la posibilidad de programar tareas, como análisis de una determinada subred y análisis de puertos de un dispositivo señalado, dando así la posibilidad de identificar el estado de cada puerto y que servicio específicamente corre por el mismo. Por otro lado, en OFA estará presente la posibilidad de visualizar una serie de estadísticas por medio de un Dashboard con distintos gráficos que podrán ser de ayuda al usuario. Y para contribuir con este tipo de apartados gráficos también brindará, en otra sección de la aplicación web, una funcionalidad para que el usuario pueda subir y alojar en el servidor el diagrama de red correspondiente a su sucursal, siendo visible luego tanto para él como para cualquier administrador.

Se busca que el funcionario encargado del soporte de un conjunto de dispositivos informáticos tenga una herramienta para poder diagnosticar y analizar un problema dentro de la red lo más rápido posible para poder accionar según lo sea conveniente.
Por otro lado, con OFA se busca también dar la posibilidad a quien administre la infraestructura, de tener una ventana al estado de cada una de las subredes que están siendo organizadas y administradas por cada respectivo grupo de funcionarios.

La finalidad del Software es conseguir agilizar la detección de errores en dispositivos dentro de la red para poder solventarlos y a su vez, ser un software fácil de utilizar y configurar por usuarios no tan avanzados en relación a conocimientos de redes. 


# DESCRIPCIÓN DEL NEGOCIO Y REQUERIMIENTOS (2)
### 2.1 – Información sobre el cliente
El software fue desarrollado para una importante cadena de supermercados del país que también cuenta con una serie de depósitos y pequeños locales abocados a otros rubros. En mayor o menor medida cada uno de estos locales cuenta con su respectiva infraestructura informática que será monitoreada a través del software. El mismo se ubicará en un servidor en la sucursal central desde el que podrá ser utilizado en aquellos locales que dispongan de un equipo de operadores. Algunos locales de menor envergadura, que no disponen de un equipo de operadores serán monitoreados desde Central a través de OFA.

Por otra parte, hay equipos de operadores que monitorean más de una subred, ya sea porque la sucursal dispone de más de una subred o porque se les asigna el monitoreo de infraestructura de locales físicamente cercanos.





### 2.2 – Requerimientos funcionales
#### 2.2.1 – Identificación y descripción de actores
**Operador:** Será un funcionario de la empresa vinculado al área de soporte e infraestructura informática. Cumplirá con el rol de monitorear la red y mantener organizados los dispositivos conectados por medio de la aplicación web.

**Administrador:** Será un funcionario de la empresa orientado al área de la infraestructura informática. Cumplirá con el rol de monitorear la red, gestionar los usuarios de la aplicación y podrá utilizar las funcionalidades de análisis de la misma.

**Usuario (Referencia):** Nos haremos de este término para indicar cuando un caso de uso puede tener tanto un operador o un administrador como actor.

#### 2.2.2 – Diagrama de casos de uso
![](https://github.com/ClaudioTilbe/oneforall/blob/3b6b944fcf9c666104da3eb1d666f5d9f9cbc5ac/Diagrams/Capitulo%202/Diagrama%20de%20CU.png)

### 2.3 – Requerimientos no funcionales
FURPS+


# 🛠 TECNOLOGÍAS Y PLATAFORMAS (3)

**Aclaración:** Se utilizara **.NET 5** como plataforma en común para desarrollar tanto Back-end como Front-end.

### 3.1 – Repositorio de datos
**SQL Server 2019** se utilizará para gestionar la base de datos empleada por el sistema.
La base de datos se creará a través de un script en el cual también se definirán una serie de procedimientos almacenados que serán utilizados por la API Rest.
Por otro lado, la seguridad integrada estará deshabilitada y la seguridad será definida principalmente por un conjunto de usuarios, roles y permisos delimitados por el desarrollador.

### 3.2 – Back-End
**API REST**. Será la interfaz que permitirá la comunicación entre nuestro Back-end y Front-end. En la API Rest se alojan una serie de métodos que podrán ser utilizados por nuestra aplicación web y que cumplen la función de utilizar las operaciones de nuestras capas en el Back-end para lograr el procesamiento y finalmente la resolución de los objetivos del usuario al efectuar los diversos casos de uso.

**ADO.NET** será utilizado en la capa de persistencia para poder acceder a los registros de la base de datos.

**Nmap** es un software de código abierto que será el responsable de procesar las peticiones de rastreo de puertos cuando el usuario así lo solicite mediante un análisis.

### 3.3 – Front-End
**Aplicación web de ASP.NET MVC**. Será el principal componente del Front-end y la tecnología que utilizará nuestro sistema para proveer de un sitio web a nuestros usuarios.

**Bootstrap** será utilizado en la aplicación web para agilizar los tiempos de desarrollo y diseñar una interfaz para el sitio de una manera rápida y efectiva.

**JQuery** será utilizado en el Front-end para facilitar y agilizar la creación de las páginas web que necesiten ser más dinámicas e interactivas; este factor será más recurrente en aquellas paginas que requieren el uso de SignalR.

**SignalR** será el componente destinado a simplificar la adición de funcionalidad web en tiempo real a la aplicación.

**Chart.js** es una librería javascript destinada a la creación de gráficos en base a datos; dicha función tendrá la finalidad de agregar esta librería al Front-End.
ClosedXML es una API C# de código abierto para leer, manipular y escribir documentos de Microsoft Excel 2007+. Será utilizada para generar los documentos excel cuando se solicite la descarga de un listado.


### 3.4 – Tecnologías de soporte
**Postman** es una plataforma que permite y hace más sencilla la creación y el uso de APIs; permitiendo hacer pruebas y comprobar el correcto funcionamiento de las mismas. Será utilizada para validar el funcionamiento de los métodos get, post, put, y delete que expondrá el back-end de nuestra aplicación para que sean consumidas por el front-end.

**Swagger** es un conjunto de herramientas de software de código abierto para diseñar, construir, documentar y utilizar servicios web RESTful. Será utilizada para validar el correcto funcionamiento de los métodos de nuestra API Rest.


# EVALUACIÓN DE RIESGOS (4)
### 4.1 – Identificación de Riesgos
#### 4.1.1 – Riesgos del sistema


| Fallo en el Motor  | 
| ------------- | 
| **Descripción**: Falla no controlada en los hilos de trabajo del motor.  | 
| **Probabilidad de Ocurrencia:** Baja | 
| **Impacto en el sistema:** Alto  | 
| **Estrategia de mitigación:** Seguir buenas prácticas de desarrollo y estructurar los algoritmos de manera ordenada. Realizar un testeo intensivo del motor tanto durante la etapa de desarrollo de iteraciones como durante la integración.  | 
| **Plan de contingencia:** Reinicio del Motor por medio de la funcionalidad integrada en el sistema. | 

| Ataques al sistema | 
| ------------- | 
| **Descripción**:  Ataques informáticos dirigidos a vulnerar el sistema por parte de cibercriminales. | 
| **Probabilidad de Ocurrencia:** Baja | 
| **Impacto en el sistema:** Alto  | 
| **Estrategia de mitigación:** Estructurar la solución con una seguridad robusta. Investigar posibles vulnerabilidades de las tecnologías utilizadas. Realizar pruebas de Pentesting contra el sistema una vez finalizado.  | 
| **Plan de contingencia:** Analizar el ataque para ubicar puntos débiles en el sistema. Fortalecer y reforzar en materia de seguridad él o los sectores afectados por el ataque. | 

| Exceder recursos del servidor | 
| ------------- | 
| **Descripción**:  El sistema exige más potencia en términos de recursos de los que el servidor puede proveer.| 
| **Probabilidad de Ocurrencia:** Baja | 
| **Impacto en el sistema:** Alto  | 
| **Estrategia de mitigación:**  Realizar pruebas al finalizar el desarrollo del sistema en algún equipo para poder brindar una recomendación de base a fundamentos sobre un ajuste óptimo con un hardware definido. Efectuar pruebas luego del deploy para ajustar los recursos que va a consumir a un nivel que no exceda las capacidades del servidor.  | 
| **Plan de contingencia:** Realizar un análisis de consumo de recursos apoyado en herramientas del sistema operativo y/o software externo con licencias de uso gratuito. Disminuir la potencia del sistema y por lo tanto los recursos consumidos por el mismo. En caso de no obtener resultados óptimos consultar la posibilidad de mejorar el hardware del servidor.  | 

#### 4.1.2 – Riesgos durante la etapa de desarrollo

| Planificación demasiado optimista | 
| ------------- | 
| **Descripción**:  Superar los límites de tiempo estimados en alguno o varios ciclos de desarrollo.| 
| **Probabilidad de Ocurrencia:** Media | 
| **Impacto en el sistema:** Bajo  | 
| **Estrategia de mitigación:**  Investigación previa y constante sobre tecnologías a utilizar. Llevar un control semanal sobre los ciclos de desarrollo. | 
| **Plan de contingencia:** Rediseñar el ciclo de desarrollo en función de los cambios que se hayan producido, esto incluye suma de carga horaria y/o agregar más ciclos de desarrollo. | 

| Falta de experiencia | 
| ------------- | 
| **Descripción**: Falta de experiencia en tecnologías y metodologías utilizadas para el desarrollo del sistema. | 
| **Probabilidad de Ocurrencia:** Alta | 
| **Impacto en el sistema:** Media  | 
| **Estrategia de mitigación:** Capacitación e investigación constante. | 
| **Plan de contingencia:** Revisiones en retrospectiva mensualmente analizando el sistema en busca de mejoras teniendo en cuenta la progresión en términos de conocimiento del desarrollador. |

| Investigación insuficiente | 
| ------------- | 
| **Descripción**: Fuentes de información inadecuadas o insuficientes. | 
| **Probabilidad de Ocurrencia:** Media | 
| **Impacto en el sistema:** Medio  | 
| **Estrategia de mitigación:** Reafirmar la veracidad y aplicación de la información extraída de las fuentes comparándolas con otras; extraer información principalmente de fuentes oficiales o de confianza.  | 
| **Plan de contingencia:** Realizar una revisión de la estructura, configuración y/o código escrito realizado en base a fuentes de información poco fiables o inadecuadas. |

| Conceptualización de la idea no acertada | 
| ------------- | 
| **Descripción**:  Fallo al conceptualizar la solución con la información proporcionada del cliente. | 
| **Probabilidad de Ocurrencia:** Baja | 
| **Impacto en el sistema:** Medio  | 
| **Estrategia de mitigación:** Grabar audio de reuniones con el cliente para su posterior revisión en caso de ser necesario  (si está de acuerdo). Efectuar revisiones de la documentación generada. | 
| **Plan de contingencia:** Programar nuevas reuniones con el cliente, promover un feed-back constante si es necesario. Planificar anticipadamente las consultas a realizar con el cliente en las reuniones. |

| Cambios en los requisitos del cliente | 
| ------------- | 
| **Descripción**: Cambio en las necesidades del cliente que afecten a los requerimientos de la solución. | 
| **Probabilidad de Ocurrencia:** Baja | 
| **Impacto en el sistema:** Medio  | 
| **Estrategia de mitigación:** Establecer una buena comunicación con el cliente. Proporcionar recomendaciones con respecto a la estructura y funcionalidades de la solución. | 
| **Plan de contingencia:** Planificar una nueva reunión y re estructurar las secciones de la solución que sean necesarias. |














