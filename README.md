# Errores Encontrados

1. No cumple con uno de los patrones de diseño SOLID, Open Closed Principle. Si existiese otro mecanismo para guardar log, 
tocaría modificar el constructor de la clase para agregar este mecanismo, en lugar de extender la funcionalidad.
2. El método LogMessage tiene dos parametros de diferente tipo llamados message, error de compilacion
3. En el método LogMessage, si el mensaje que se ingresa es null, genera error no controlado al hacer la funcion trim
4. Sin importar el tipo de log que se elija, siempre lo hará en los tres tipos de log, archivo, BD y consola.
5. La responsibilidad de grabar el log está en un solo método. Dificil de mantener.
6. Para grabar log en archivo, no es necesario siempre recuperar lo previemente grabado para luego añadirle el nuevo log.
7. La validacion if(message&&_logMessage) se repite varias veces pudiendose realizar una única vez.
8. Existen variables como l y t que por su nombre no indican su funcionalidad


### SOLUCION PLANTEADA

Se reestructura el proyecto corrigiendo cada uno de los puntos de la siguiente forma>

1. Se crean dos enumeraciones que permitiran por mantenimiento de codigo agregar un nuevo target para grabar el log, o un nuevo tipo de mensjae.
2. Se corrige el método LogMessage cambiendo el nombre del parámetro repetido
3. Se corrigen las validaciones sobre el mensaje recibido
4. Se agrega validación para que de acuerdo al tipo de log a grabar, se realice unicamente el escogido.
5. Se creab métodos de grabación del log independientes
6. Se optimiza el método para grabar log en archivo de texto
7. Se optimizan las validaciones
8. Se eliminan las variables sin utilizar


## Autor

* **Guillermo Valenzuela** - *Desarrollador .NET Senior* - (https://github.com/kaoscore)


## Licencia

MIT License 




