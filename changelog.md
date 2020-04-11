# Version 1.1: 
## added: 
Añadido soporte para poder utilizar los htc vive controllers respecto a una escena creada en Unity. 
La escena usa la anterior con los musicos y raycast pero agregando soporte para poder lanzar el raycast solo al accionar el trigger de cada controller (diferenciando entre mano izquierda y mano derecha)
## modified: 
- GameObjects: tripode_left y tripode_right con fines de depuracion para tener localizados los tripodes en VR y evitar posibles accidentes. 
- SteamVRInput: Agregado un nuevo tipo de input en ActionSet directing para hacer pruebas de direccion. 
- SteamVRInputUI: Mismo cambio. 
- GameObjects que representan a los musicos de orquesta acercados para que sea mas sencillo señalarlos con el raycast. 
- Script DirectorRaycast.cs modificado para agregar soporte para utilizar el trigger (gatillo) de los controller y lanzar el raycast solo en el caso de ser accionado. 
- Script DirectorRaycast.cs asociado a objetos controller left y controller right. 
- Objeto usado para accionar la direccion de dirigir (actions). 
