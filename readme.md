Test para Engie
========

Este proyecto fue realizado en C# .net core 2.1 sin BD asociadas.

Para probar el endpoint, Una vez que se ejecute el codigo en Visual Studio se debe acceder, via **POST**,  a `/api/values`, enviando el siguiente JSON:
```json
{
    "ElementNumber": "3",
    "ListNumbers": "22 79 21"
}
```

Los resultados posibles son:
| Codigo respuesta        | Mensaje           | 
| ------------- |-------------|
|200 OK |The operation for obtain a number divisible for 101 is: [operation] :)|
|400 BadRequest |The ElementNumber should be an integer between 2 and 10^4|
|400 BadRequest |The ListNumbers has a problem. Remember write integers between 1 and 100, separated with a space between them|
|404 NotFound |With this numbers, isn't posible obtain a result divisible for 101 :(|


Ejemplo de respuesta:
```json

{
    "message": "The operation for obtain a number divisible for 101 is: 22+79*21   :)"
}
```

