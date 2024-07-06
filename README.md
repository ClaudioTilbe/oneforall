![](./images/header.png)

OFA es una aplicación web destinada al monitoreo de redes IPv4. Surge para cumplir con las necesidades de un software moderno y efectivo que se aboque en el monitoreo de la red de la empresa cliente, una importante cadena de supermercados del pais. La solucion fue desarrollada como proyecto de fin de carrera para el instituto BIOS.

A continuacion se ofrece la documentacion que contiene entre otras cosas...

# INTRODUCCIÓN (1)

### 1.1 - Presentación de equipo
El equipo está únicamente conformado por Claudio Tilbe, desarrollador a cargo de la documentación y codificación de la solución, de nacionalidad uruguaya.

### 1.2 - Presentación de cliente
Una cadena de supermercados mayoristas nacional que cuenta con varias sucursales de mayor y menor envergadura a lo largo del país, se pone en posición de cliente, solicitando un sistema a través del cual pueda monitorear toda su red; desde servidores de importancia en su sucursal central hasta pequeños equipos que sean puntos finales de la red.

### 1.3 - Introducción a la idea

#### Descripción
Actualmente trabajo en el área de soporte e infraestructura perteneciente al cliente.
Dentro de esta área hay varias tareas y funciones a desempeñar, de entre las mismas destaca el monitoreo de dispositivos de red, esencial a la hora de detectar anomalías en el flujo normal de trabajo. Detectar problemas de conexión en dispositivos a tiempo puede ser la diferencia entre un problema minúsculo o un mal mayor, especialmente si hablamos de servidores o similares.
Con esta premisa y con la idea de brindar un Software que permita monitorear, organizar y alertar al usuario en caso de incidentes surge OFA. Esta aplicación web por medio de varias funcionalidades va a permitir el monitoreo de la red, no solo desde la sucursal central hacia el resto de la Red, sino también desde cada respectivo grupo de operadores vinculado una determinada sucursal y/u otras subredes contiguas.
OFA tiene un sistema por el cual identifica el riesgo según el estado y características de un dispositivo y alerta, notifica o ignora basado en cómo el usuario haya configurado ese determinado dispositivo.
La distribución de usuarios y el hecho de brindar acceso a cada grupo de operadores al monitoreo de su propia sucursal da como ventaja una configuración más precisa del Software, ya que comprenden el contexto, finalidades y funciones de cada dispositivo informático en sus subredes.
También existe la posibilidad de programar tareas, como análisis de una determinada subred y análisis de puertos de un dispositivo señalado, dando así la posibilidad de identificar el estado de cada puerto y que servicio específicamente corre por el mismo. Por otro lado, en OFA estará presente la posibilidad de visualizar una serie de estadísticas por medio de un Dashboard con distintos gráficos que podrán ser de ayuda al usuario. Y para contribuir con este tipo de apartados gráficos también brindará, en otra sección de la aplicación web, una funcionalidad para que el usuario pueda subir y alojar en el servidor el diagrama correspondiente a su sucursal, siendo visible luego tanto para él como para cualquier administrador.

Esta aplicación, además de lo ya mencionado, también busca ser fácil de entender y configurar para un usuario no tan avanzado en el área de la informática y redes.

#### ¿Cuál es el enfoque?
Se busca que el funcionario encargado del soporte de un conjunto de dispositivos informáticos tenga una herramienta para poder diagnosticar y analizar un problema dentro de la red lo más rápido posible para poder accionar según lo sea conveniente.
Por otro lado, con OFA se busca también dar la posibilidad a quien administre la infraestructura, de tener una ventana al estado de cada una de las subredes que estarán siendo organizadas y administradas por cada respectivo grupo de funcionarios.

#### Finalidad
La finalidad del Software es conseguir agilizar la detección de errores en dispositivos dentro de la red para poder solventarlos y a su vez, ser un software fácil de utilizar y configurar por usuarios no tan avanzados en relación a conocimientos de redes. 
