# Guión para Video Explicativo: Sistema de Validación de Vigas en C#

## Introducción

**[Pantalla de Título: "Sistema de Validación de Vigas - Programación Orientada a Objetos"]**

**Narrador:** ¡Hola! Bienvenidos a este tutorial donde aprenderemos sobre Programación Orientada a Objetos a través de un proyecto práctico: un sistema que valida si una viga puede soportar su propio peso.

**[Mostrar diagrama simple de una viga con símbolos: %===*==]**

**Narrador:** Imaginen que tenemos una viga construida con diferentes símbolos. Cada símbolo tiene un significado especial:
- Los símbolos **%**, **&** y **#** representan las bases que soportan la viga
- El símbolo **=** representa los largueros que forman la estructura
- El símbolo ***** representa las conexiones que unen las partes

Nuestro programa debe determinar si la base puede soportar el peso total de la viga.

---

## Desarrollo Parte 1: Conceptos Fundamentales 

**[Mostrar código de la clase abstracta BeamPart]**

**Narrador:** Comencemos con el centro de nuestro programa: la **clase abstracta BeamPart**. 


**Narrador:** Una clase abstracta es como un molde o plantilla que define características comunes que compartirán todas las partes de nuestra viga.

**Variables importantes:**
- **Symbol**: guarda el carácter que representa la parte (%, &, #, =, *)
- **Name**: almacena el nombre descriptivo de la parte

**Métodos abstractos:**
- **CalculateWeight**: cada parte calculará su peso de manera diferente
- **IsValidConnection**: verifica si una parte puede conectarse con la anterior

La palabra **abstract** significa que esta clase no puede crear objetos directamente, sino que sirve como base para otras clases.

---

## Desarrollo Parte 2: Las Clases Derivadas 

**[Mostrar código de la clase Base]**

**Narrador:** Ahora veamos cómo las clases **heredan** de nuestra clase abstracta. Empezamos con la clase **Base**:


**Narrador:** La clase Base **hereda** de BeamPart, lo que significa que automáticamente tiene las propiedades Symbol y Name.

**Nueva variable:**
- **Resistance**: indica cuántas unidades de peso puede soportar esta base

**El constructor** recibe el símbolo de la base y asigna la resistencia correspondiente:
- **%** resiste 10 unidades
- **&** resiste 30 unidades  
- **#** resiste 90 unidades

**[Mostrar código de la clase Larguero]**

**Narrador:** La clase **Larguero** representa las partes estructurales de la viga:


**Narrador:** Aquí vemos conceptos importantes:
- **override**: significa que estamos dando nuestra propia implementación a los métodos abstractos
- **CalculateWeight**: el peso del larguero depende de su posición en la secuencia
- **IsValidConnection**: un larguero puede conectarse después de una base, otro larguero o una conexión

**[Mostrar código de la clase Conexion]**

**Narrador:** Finalmente, la clase **Conexion**:


**Narrador:** Las conexiones tienen una regla especial: **solo pueden venir después de un larguero**, nunca después de otra conexión.

---

## Desarrollo Parte 3: El Validador y los Cálculos 

**[Mostrar código de BeamValidator]**

**Narrador:** La clase **BeamValidator** es nuestro "cerebro" que contiene toda la lógica de validación:


**Narrador:** **CreateBeamPart** es una **fábrica de objetos**. Recibe un carácter y crea el objeto correspondiente. Es como un traductor que convierte símbolos en objetos de nuestro programa.

**[Mostrar método CalculateTotalWeight]**

**Narrador:** El método más complejo es **CalculateTotalWeight**:


**Variables importantes:**
- **totalWeight**: acumula el peso total de toda la viga
- **currentSequenceWeight**: peso de la secuencia actual de largueros
- **largueroPosition**: posición del larguero en su secuencia (1, 2, 3...)

**Lógica del cálculo:**
1. Los largueros en secuencia pesan 1, 2, 3, etc.
2. Las conexiones (*) pesan el doble de la secuencia anterior
3. Cada conexión reinicia el contador de posición

---

## Desarrollo Parte 4: El Programa Principal 

**[Mostrar código del Main]**

**Narrador:** El programa principal coordina todo el sistema:

**Narrador:** El método **Main** es el punto de entrada. Primero ejecuta casos de prueba automáticos, luego permite al usuario ingresar sus propias vigas.

**[Mostrar método ProcessBeam]**

**Narrador:** **ProcessBeam** es donde ocurre todo:

**Narrador:** Este método sigue un proceso ordenado:
1. **Valida** que la estructura sea correcta
2. **Calcula** el peso total
3. **Compara** con la resistencia de la base
4. **Informa** el resultado

---

## Demostración Práctica 

**[Mostrar ejecución del programa]**

**Narrador:** Veamos el programa en acción con los casos de prueba:

**Caso 1: "%"**
- Base: % (resiste 10 unidades)
- Sin largueros ni conexiones
- Peso total: 0
- Resultado: ✅ La viga soporta el peso

**Caso 2: "%=*"**
- Base: % (resiste 10 unidades)  
- Un larguero (=) pesa 1 unidad
- Una conexión (*) pesa 2 × 1 = 2 unidades
- Peso total: 1 + 2 = 3 unidades
- Resultado: ✅ La viga soporta el peso (10 ≥ 3)

**Caso 3: "#===*==*="**
- Base: # (resiste 90 unidades)
- Primera secuencia: === (1+2+3 = 6)
- Primera conexión: * (6 × 2 = 12)
- Segunda secuencia: == (1+2 = 3)  
- Segunda conexión: * (3 × 2 = 6)
- Tercera secuencia: = (1)
- Peso total: 6 + 12 + 3 + 6 + 1 = 28 unidades
- Resultado: ✅ La viga soporta el peso (90 ≥ 28)

**Caso 4: "%===*==*===="**
- Base: % (resiste 10 unidades)
- Cálculo similar al anterior pero peso total = 37 unidades
- Resultado:  La viga NO soporta el peso (10 < 37)

---

## Conclusión 

**[Mostrar diagrama de clases]**

**Narrador:** En este proyecto hemos aplicado conceptos fundamentales de la Programación Orientada a Objetos:

**1. Abstracción:** La clase abstracta BeamPart define la estructura común
**2. Herencia:** Las clases Base, Larguero y Conexion heredan comportamiento común
**3. Polimorfismo:** Cada clase implementa los métodos abstractos de manera específica
**4. Encapsulación:** Las propiedades y métodos están organizados en clases cohesivas

**Ventajas de este diseño:**
- **Modular:** Cada clase tiene una responsabilidad específica
- **Extensible:** Podemos agregar nuevos tipos de partes fácilmente
- **Mantenible:** Los cambios en una clase no afectan las otras
- **Reutilizable:** El código puede adaptarse para otros problemas similares

**Narrador:** Este sistema demuestra cómo la Programación Orientada a Objetos nos ayuda a modelar problemas del mundo real de manera organizada y eficiente.

¡Gracias por acompañarnos en este recorrido por el código! Esperamos que este ejemplo les haya ayudado a comprender mejor los conceptos de POO en C#.

**[Pantalla final con resumen de conceptos clave]**

