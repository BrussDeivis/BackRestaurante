�
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesDatos\EFactura\partial\Documento.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
EFactura( 0
{ 
public		 

partial		 
class		 
	Documento		 "
{

 
public 
	Documento 
( 
long 

idSigescom (
,( )
string* 0!
codigoTipoComprobante1 F
,F G
stringH N
serieComprobanteO _
,_ `
stringa g
numeroComprobanteh y
,y z
string	{ �
fechaEmision
� �
,
� �
string
� �
tipoComprobante
� �
,
� �
int
� �
estado
� �
,
� �
int
� �
estadoSigescom
� �
,
� �
byte
� �
[
� �
]
� �
archivo
� �
)
� �
{
this 
. 

idSigescom 
= 

idSigescom (
;( )
this 
. !
codigoTipoComprobante &
=' (!
codigoTipoComprobante) >
;> ?
this 
. 
serieComprobante !
=" #
serieComprobante$ 4
;4 5
this 
. 
numeroComprobante "
=# $
numeroComprobante% 6
;6 7
this 
. 
fechaEmision 
= 
DateTime  (
.( )
Parse) .
(. /
fechaEmision/ ;
); <
;< =
this 
. 
tipoComprobante  
=! "
tipoComprobante# 2
;2 3
this 
. 
estado 
= 
estado  
;  !
this 
. 
estadoSigescom 
=  !
estadoSigescom" 0
;0 1
this 
. 
Binario 
= 
new 
Binario &
(& '
)' (
;( )
this 
. 
Binario 
. 
archivoBinario '
=( )
archivo* 1
;1 2
} 	
public 
string  
ComprobanteDocumento *
(* +
)+ ,
{ 	
return 
serieComprobante #
+$ %
$str& )
+* +
numeroComprobante, =
;= >
} 	
} 
} �	
]D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesDatos\EFactura\partial\DocumentoAvanzado.cs
	namespace 	
Tsp
 
.
FacturacionElectronica $
.$ %
Modelo% +
{ 
class		 	
DocumentoAvanzado		
 
:		  
DocumentoElectronico		 2
{

 
public 
DocumentoAvanzado  
(  !
	Documento! *
	documento+ 4
)4 5
{
try 
{ 
DocumentoElectronico $(
documentoElectronicoAvanzado% A
=B C
JsonConvertD O
.O P
DeserializeObjectP a
<a b 
DocumentoElectronicob v
>v w
(w x
Encoding	x �
.
� �
UTF8
� �
.
� �
	GetString
� �
(
� �
	documento
� �
.
� �
Binario
� �
.
� �
archivoBinario
� �
)
� �
)
� �
;
� �
} 
catch 
( 
	Exception 
ex 
)  
{ 
throw 
ex 
; 
} 
} 	
} 
} �
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesDatos\Principal\partial\Periodo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{ 
public		 

partial		 
class		 
Periodo		  
{

 
public 
DateTime 

FechaDesde "
{ 	
get
{
return
new
DateTime
(
Convert
.
ToInt32
(
this
.
anio
)
,
Convert
.
ToInt32
(
this
.
mes
)
,
$num
)
;
}
} 	
public 
DateTime 

FechaHasta "
{ 	
get 
{ 
return 
new 
DateTime #
(# $
Convert$ +
.+ ,
ToInt32, 3
(3 4
this4 8
.8 9
anio9 =
)= >
,> ?
Convert 
. 
ToInt32 
(  
this  $
.$ %
mes% (
)( )
,) *
DateTime+ 3
.3 4
DaysInMonth4 ?
(? @
Convert@ G
.G H
ToInt32H O
(O P
thisP T
.T U
anioU Y
)Y Z
,Z [
Convert 
. 
ToInt32 
(  
this  $
.$ %
mes% (
)( )
)) *
)* +
.+ ,
AddDays, 3
(3 4
$num4 5
)5 6
.6 7
AddMilliseconds7 F
(F G
-G H
$numH I
)I J
;J K
} 
} 	
} 
} �
XD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesDatos\Principal\partial\Tipo_cambio.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{ 
public		 

partial		 
class		 
Tipo_cambio		 $
{

 
public 
string 
FechaString !
{" #
get$ '
=>( *
fecha+ 0
.0 1
ToString1 9
(9 :
$str: F
)F G
;G H
}I J
} 
}
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\ResumenCliente.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 
ResumenCliente

 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
string
Codigo
{
get
;
set
;
}
public 
string 
RazonSocial !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
TipoPersona !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string "
TipoDocumentoIdentidad ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
string $
NumeroDocumentoIdentidad .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
string 
Telefono 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Correo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
DetalleDireccion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
UbigeoDireccion %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
	Direccion 
{  !
get" %
=>& (
DetalleDireccion) 9
+: ;
$str< A
+B C
UbigeoDireccionD S
;S T
}U V
public 
ResumenCliente 
( 
) 
{  !
}" #
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\MiembroGrupoClientes.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
Custom

( .
{ 
public 

class  
MiembroGrupoClientes %
{
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
	Documento 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
	EsVigente 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\GrupoClientesResumen.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
.		' (
Custom		( .
{

 
public 

class  
GrupoClientesResumen %
{ 
public
int
Id
{
get
;
set
;
}
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Tipo 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string  
DocumentoResponsable *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 
NombreResponsable '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
TelefonoResponsable )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
CorreoResponsable '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
int 
NumeroClientes !
{" #
get$ '
=>( *
Clientes+ 3
.3 4
Where4 9
(9 :
c: ;
=>< >
c? @
.@ A
	EsVigenteA J
)J K
.K L
CountL Q
(Q R
)R S
;S T
}U V
public 
List 
<  
MiembroGrupoClientes (
>( )
Clientes* 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
bool 
	EsVigente 
{ 
get  #
;# $
set% (
;( )
}* +
public  
GrupoClientesResumen #
(# $
)$ %
{ 	
} 	
} 
} �
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\GrupoClientes.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
Custom

( .
{ 
public 

class 

{
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
IdActor 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
ItemGenericoBase 
Tipo  $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
ItemGenericoBase 

{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
ActorComercial_ 
Responsable *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
List 
<  
MiembroGrupoClientes (
>( )
Clientes* 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 

( 
) 
{ 	
} 	
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\JsonCentroDeAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Actor1 6
{ 
public		 

class		  
JsonCentroDeAtencion		 %
{

 
public 
bool  
salidabienessinstock (
;( )
} 
}
dD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\JsonEstablecimientoComercial.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Actor1 6
{ 
public		 

class		 (
JsonEstablecimientoComercial		 -
{

 
public 
string 

;# $
} 
}
QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\Almacen.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Negocio %
.% &
Core& *
.* +
Almacen+ 2
{ 
public 

class 
Almacen 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public		 
string		 
Codigo		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
string

 
Nombre

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 
Almacen 
( 
) 
{
} 	
public 
Almacen 
( 
int 
id 
, 
string %
codigo& ,
,, -
string. 4
nombre5 ;
); <
{ 	
Id 
= 
id 
; 
Codigo 
= 
codigo 
; 
Nombre 
= 
nombre 
; 
} 	
} 
} �#
WD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\Establecimiento.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Negocio %
.% &
Core& *
.* +
Actor+ 0
{ 
public 

class 
Establecimiento  
{		 
public

 
int

 
Id

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public
List
<
ItemGenerico
>
CentrosAtencion
{
get
;
set
;
}
public 
Establecimiento 
( 
)  
{ 	
} 	
public 
Establecimiento 
(  #
ItemGenericoConSubItems! 8
item9 =
)= >
{ 	
Id 
= 
item 
. 
Id 
; 
Codigo 
= 
item 
. 
Codigo  
;  !
Nombre 
= 
item 
. 
Nombre  
;  !
CentrosAtencion 
= 
item "
." #
SubItems# +
;+ ,
} 	
public 
Establecimiento 
( 
int "
id# %
,% &
string' -
nombre. 4
)4 5
{ 	
Id 
= 
id 
; 
Nombre 
= 
nombre 
; 
} 	
public!! 
Establecimiento!! 
(!! $
EstablecimientoComercial!! 7$
establecimientoComercial!!8 P
)!!P Q
{"" 	
Id## 
=## $
establecimientoComercial## )
.##) *
Id##* ,
;##, -
Codigo$$ 
=$$ $
establecimientoComercial$$ -
.$$- .
Codigo$$. 4
;$$4 5
Nombre%% 
=%% $
establecimientoComercial%% -
.%%- .
Nombre%%. 4
;%%4 5
}&& 	
public(( 
static(( 
List(( 
<(( 
Establecimiento(( *
>((* +
Convert((, 3
(((3 4
List((4 8
<((8 9#
ItemGenericoConSubItems((9 P
>((P Q
_establecimientos((R c
)((c d
{)) 	
List** 
<** 
Establecimiento**  
>**  !
establecimientos**" 2
=**3 4
new**5 8
List**9 =
<**= >
Establecimiento**> M
>**M N
(**N O
)**O P
;**P Q
_establecimientos++ 
.++ 
ForEach++ %
(++% &
e++& '
=>++( *
establecimientos+++ ;
.++; <
Add++< ?
(++? @
new++@ C
Establecimiento++D S
(++S T
e++T U
)++U V
)++V W
)++W X
;++X Y
return,, 
establecimientos,, #
;,,# $
}-- 	
public// 
static// 
List// 
<// 
Establecimiento// *
>//* +
Convert//, 3
(//3 4
List//4 8
<//8 9$
EstablecimientoComercial//9 Q
>//Q R
_establecimientos//S d
)//d e
{00 	
List11 
<11 
Establecimiento11  
>11  !
establecimientos11" 2
=113 4
new115 8
List119 =
<11= >
Establecimiento11> M
>11M N
(11N O
)11O P
;11P Q
_establecimientos22 
.22 
ForEach22 %
(22% &
e22& '
=>22( *
establecimientos22+ ;
.22; <
Add22< ?
(22? @
new22@ C
Establecimiento22D S
(22S T
e22T U
)22U V
)22V W
)22W X
;22X Y
return33 
establecimientos33 #
;33# $
}44 	
}55 
}66 �@
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\OrdenAlmacen.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 
OrdenAlmacen

 
:

 
OrdenAlmacenResumen

  3
{ 
public 
List 
< 
long 
> 

IdsOrdenes $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
bool

{
get
=>
IdEstado
==
MaestroSettings
.
Default
.
IdDetalleMaestroEstadoPendiente
;
}
public 
bool 
EstaParcial 
{  !
get" %
=>& (
IdEstado) 1
==2 4
MaestroSettings5 D
.D E
DefaultE L
.L M)
IdDetalleMaestroEstadoParcialM j
;j k
}l m
public 
bool 
EstaCompletada "
{# $
get% (
=>) +
IdEstado, 4
==5 7
MaestroSettings8 G
.G H
DefaultH O
.O P,
 IdDetalleMaestroEstadoCompletadaP p
;p q
}r s
public 
List 
< 
OrdenDeOrdenAlmacen '
>' (
Ordenes) 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
List 
< !
DetalleDeOrdenAlmacen )
>) *
Detalles+ 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
List 
< $
MovimientoDeOrdenAlmacen ,
>, -
Movimientos. 9
{: ;
get< ?
;? @
setA D
;D E
}F G
} 
public 

class 
OrdenDeOrdenAlmacen $
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
int 
IdTipoTransaccion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
FechaEmision "
{# $
get% (
=>) +
Fecha, 1
.1 2
ToString2 :
(: ;
$str; P
)P Q
;Q R
}S T
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string  
SerieNumeroDocumento *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public  
ComprobanteDeAlmacen #
Comprobante$ /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
} 
public   

class   $
MovimientoDeOrdenAlmacen   )
{!! 
public"" 
long"" 
Id"" 
{"" 
get"" 
;"" 
set"" !
;""! "
}""# $
public## 
long## 
IdOrden## 
{## 
get## !
;##! "
set### &
;##& '
}##( )
public$$ 
int$$ 
IdTipoComprobante$$ $
{$$% &
get$$' *
;$$* +
set$$, /
;$$/ 0
}$$1 2
public%% 
int%% 
IdTipoTransaccion%% $
{%%% &
get%%' *
;%%* +
set%%, /
;%%/ 0
}%%1 2
public&& 
DateTime&& 
Fecha&& 
{&& 
get&&  #
;&&# $
set&&% (
;&&( )
}&&* +
public'' 
string'' 
FechaEmision'' "
{''# $
get''% (
=>'') +
Fecha'', 1
.''1 2
ToString''2 :
('': ;
$str''; P
)''P Q
;''Q R
}''S T
public(( 
string(( 
Destinatario(( "
{((# $
get((% (
;((( )
set((* -
;((- .
}((/ 0
public)) 
string))  
SerieNumeroDocumento)) *
{))+ ,
get))- 0
;))0 1
set))2 5
;))5 6
}))7 8
public** 
int** 
IdEstado** 
{** 
get** !
;**! "
set**# &
;**& '
}**( )
public++ 
bool++ 
	EsVigente++ 
{++ 
get++  #
=>++$ &
IdEstado++' /
==++0 2
MaestroSettings++3 B
.++B C
Default++C J
.++J K,
 IdDetalleMaestroEstadoConfirmado++K k
;++k l
}++m n
public,, 
List,, 
<,, !
DetalleDeOrdenAlmacen,, )
>,,) *
Detalles,,+ 3
{,,4 5
get,,6 9
;,,9 :
set,,; >
;,,> ?
},,@ A
public--  
ComprobanteDeAlmacen-- #
Comprobante--$ /
{--0 1
get--2 5
;--5 6
set--7 :
;--: ;
}--< =
}.. 
public00 

class00 !
DetalleDeOrdenAlmacen00 &
{11 
public22 
int22 

IdConcepto22 
{22 
get22  #
;22# $
set22% (
;22( )
}22* +
public33 
string33 
Concepto33 
{33  
get33! $
;33$ %
set33& )
;33) *
}33+ ,
public44 
decimal44 
Cantidad44 
{44  !
get44" %
;44% &
set44' *
;44* +
}44, -
public55 
decimal55 
Ordenado55 
{55  !
get55" %
;55% &
set55' *
;55* +
}55, -
public66 
decimal66 
Revocado66 
{66  !
get66" %
;66% &
set66' *
;66* +
}66, -
public77 
decimal77 
	Entregado77  
{77! "
get77# &
;77& '
set77( +
;77+ ,
}77- .
public88 
decimal88 
	Pendiente88  
{88! "
get88# &
;88& '
set88( +
;88+ ,
}88- .
public99 
decimal99 
StockActual99 "
{99# $
get99% (
;99( )
set99* -
;99- .
}99/ 0
public:: 
decimal:: 
Devuelto:: 
{::  !
get::" %
;::% &
set::' *
;::* +
}::, -
};; 
public<< 

class<<  
ComprobanteDeAlmacen<< %
{== 
public>> 
long>> 
Id>> 
{>> 
get>> 
;>> 
set>> !
;>>! "
}>># $
public?? 
long?? 
IdOrden?? 
{?? 
get?? !
=>??" $
Id??% '
;??' (
}??) *
public@@ 
string@@ %
CadenaHtmlDeComprobante80@@ /
{@@0 1
get@@2 5
;@@5 6
set@@7 :
;@@: ;
}@@< =
publicAA 
stringAA %
CadenaHtmlDeComprobanteA4AA /
{AA0 1
getAA2 5
;AA5 6
setAA7 :
;AA: ;
}AA< =
}BB 
}CC �P
]D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\OrdenAlmacenResumen.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 
OrdenAlmacenResumen

 $
{ 
private !
Parametro_transaccion %
aliasOrigenDestino& 8
;8 9
private
int
idActorInterno
;
private 
string '
establecimientoActorInterno 2
;2 3
private 
string 
nombreActorInterno )
;) *
private 
string %
tipoDocumentoActorInterno 0
;0 1
private 
string !
documentoActorInterno ,
;, -
private 
int 
idActorExterno "
;" #
private 
string '
establecimientoActorExterno 2
;2 3
private 
string 
nombreActorExterno )
;) *
private 
string %
tipoDocumentoActorExterno 0
;0 1
private 
string !
documentoActorExterno ,
;, -
public 
int 
IdActorInterno !
{" #
set$ '
=>( *
idActorInterno+ 9
=: ;
value< A
;A B
}C D
public 
string '
EstablecimientoActorInterno 1
{2 3
set4 7
=>8 :'
establecimientoActorInterno; V
=W X
valueY ^
;^ _
}` a
public 
string 
NombreActorInterno (
{) *
set+ .
=>/ 1
nombreActorInterno2 D
=E F
valueG L
;L M
}N O
public 
string %
TipoDocumentoActorInterno /
{0 1
set2 5
=>6 8%
tipoDocumentoActorInterno9 R
=S T
valueU Z
;Z [
}\ ]
public 
string !
DocumentoActorInterno +
{, -
set. 1
=>2 4!
documentoActorInterno5 J
=K L
valueM R
;R S
}T U
public 
int 
IdActorExterno !
{" #
set$ '
=>( *
idActorExterno+ 9
=: ;
value< A
;A B
}C D
public 
string '
EstablecimientoActorExterno 1
{2 3
set4 7
=>8 :'
establecimientoActorExterno; V
=W X
valueY ^
;^ _
}` a
public 
string 
NombreActorExterno (
{) *
set+ .
=>/ 1
nombreActorExterno2 D
=E F
valueG L
;L M
}N O
public   
string   %
TipoDocumentoActorExterno   /
{  0 1
set  2 5
=>  6 8%
tipoDocumentoActorExterno  9 R
=  S T
value  U Z
;  Z [
}  \ ]
public!! 
string!! !
DocumentoActorExterno!! +
{!!, -
set!!. 1
=>!!2 4!
documentoActorExterno!!5 J
=!!K L
value!!M R
;!!R S
}!!T U
public## 
long## 
Id## 
{## 
get## 
;## 
set## !
;##! "
}### $
public$$ 
DateTime$$ 
Fecha$$ 
{$$ 
get$$  #
;$$# $
set$$% (
;$$( )
}$$* +
public%% 
string%% 
FechaEmision%% "
{%%# $
get%%% (
=>%%) +
Fecha%%, 1
.%%1 2
ToString%%2 :
(%%: ;
$str%%; G
)%%G H
;%%H I
}%%J K
public&& 
int&& 
	IdAlmacen&& 
{&& 
get&& "
=>&&# %
EsBidireccional&&& 5
?&&6 7
(&&8 9
PorIngresar&&9 D
?&&D E
idActorExterno&&F T
:&&U V
idActorInterno&&W e
)&&e f
:&&g h
idActorInterno&&i w
;&&w x
}&&z {
public'' 
string'' 
Establecimiento'' %
{''& '
get''( +
=>'', .
EsBidireccional''/ >
?''? @
(''A B
PorIngresar''B M
?''N O'
establecimientoActorExterno''P k
:''l m(
establecimientoActorInterno	''n �
)
''� �
:
''� �)
establecimientoActorInterno
''� �
;
''� �
}
''� �
public(( 
string(( 
Almacen(( 
{(( 
get((  #
=>(($ &
EsBidireccional((' 6
?((7 8
(((9 :
PorIngresar((: E
?((F G
nombreActorExterno((H Z
:(([ \
nombreActorInterno((] o
)((o p
:((q r
nombreActorInterno	((s �
;
((� �
}
((� �
public)) 
string)) 

{))$ %
get))& )
;))) *
set))+ .
;)). /
}))0 1
public** 
string**  
SerieNumeroDocumento** *
{**+ ,
get**- 0
;**0 1
set**2 5
;**5 6
}**7 8
public++ 
int++ 
IdTipoOperacion++ "
{++# $
get++% (
;++( )
set++* -
;++- .
}++/ 0
public,, 
string,, 

{,,$ %
get,,& )
;,,) *
set,,+ .
;,,. /
},,0 1
public-- 
string-- 
	Ordenante-- 
{--  !
get--" %
;--% &
set--' *
;--* +
}--, -
public.. 
int.. 
IdOrigenDestino.. "
{..# $
get..% (
=>..) +
EsBidireccional.., ;
?..< =
(..> ?
PorIngresar..? J
?..K L
idActorInterno..M [
:..\ ]
idActorExterno..^ l
)..l m
:..n o
idActorExterno..p ~
;..~ 
}
..� �
public// 

string// "
DocumentoOrigenDestino// (
{//) *
get//+ .
=>/// 1
EsBidireccional//2 A
?//B C
(//D E
PorIngresar//E P
?//Q R
(//S T%
tipoDocumentoActorInterno//T m
+//n o
$str//p t
+//u v"
documentoActorInterno	//w �
)
//� �
:
//� �
(
//� �'
tipoDocumentoActorExterno
//� �
+
//� �
$str
//� �
+
//� �#
documentoActorExterno
//� �
)
//� �
)
//� �
:
//� �
(
//� �'
tipoDocumentoActorExterno
//� �
+
//� �
$str
//� �
+
//� �#
documentoActorExterno
//� �
)
//� �
;
//� �
}
//� �
public00 
string00 
NombreOrigenDestino00 )
{00* +
get00, /
=>000 2
EsBidireccional003 B
?00C D
(00E F
PorIngresar00F Q
?00R S
nombreActorInterno00T f
:00g h
nombreActorExterno00i {
)00{ |
:00} ~
nombreActorExterno	00 �
;
00� �
}
00� �
public11 !
Parametro_transaccion11 $
AliasOrigenDestino11% 7
{118 9
set11: =
=>11> @
aliasOrigenDestino11A S
=11T U
value11V [
;11[ \
}11] ^
public22 
string22 

{22$ %
get22& )
=>22* ,"
DocumentoOrigenDestino22- C
+22D E
$str22F K
+22L M
(22N O
aliasOrigenDestino22O a
==22b d
null22e i
?22j k
NombreOrigenDestino22l 
:
22� � 
aliasOrigenDestino
22� �
.
22� �
valor
22� �
)
22� �
;
22� �
}
22� �
public33 
int33 

{33! "
get33# &
;33& '
set33( +
;33+ ,
}33- .
public44 
string44 
ModoEntrega44 !
{44" #
get44$ '
=>44( *
	Enumerado44+ 4
.444 5
GetDescription445 C
(44C D
(44D E#
IndicadorImpactoAlmacen44E \
)44\ ]

)44j k
;44k l
}44m n
public55 
bool55 

EsDiferida55 
{55  
get55! $
=>55% '

==556 8
(559 :
int55: =
)55= >#
IndicadorImpactoAlmacen55> U
.55U V
Diferida55V ^
;55^ _
}55` a
public66 
int66 
IdEstado66 
{66 
get66 !
;66! "
set66# &
;66& '
}66( )
public77 
string77 
Estado77 
{77 
get77 "
;77" #
set77$ '
;77' (
}77) *
public88 
bool88 
EsBidireccional88 #
{88$ %
get88& )
;88) *
set88+ .
;88. /
}880 1
public99 
bool99 
PorIngresar99 
{99  !
get99" %
;99% &
set99' *
;99* +
}99, -
public:: 
bool:: $
PuedeRegistrarMovimiento:: ,
{::- .
get::/ 2
=>::3 5
IdEstado::6 >
==::? A
MaestroSettings::B Q
.::Q R
Default::R Y
.::Y Z+
IdDetalleMaestroEstadoPendiente::Z y
||::z |
IdEstado	::} �
==
::� �
MaestroSettings
::� �
.
::� �
Default
::� �
.
::� �+
IdDetalleMaestroEstadoParcial
::� �
;
::� �
}
::� �
}<< 
}== �
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\InventarioHistorico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{		 
public

 

class

 
InventarioHistorico

 $
:

% &
InventarioFisico

' 7
{ 
public
string
Descripcion
{
get
;
set
;
}
public 
decimal 

{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 

ValorTotal !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
InventarioHistorico "
(" #
)# $
{ 	
} 	
new 
public 
List 
< 
InventarioHistorico +
>+ ,
Convert- 4
(4 5
)5 6
{ 	
return 
new 
List 
< 
InventarioHistorico /
>/ 0
(0 1
)1 2
;2 3
} 	
public 
static 
List 
< 
Detalle_transaccion .
>. /!
ToDetallesTransaccion0 E
(E F
ListF J
<J K
InventarioHistoricoK ^
>^ _!
inventarioValorizados` u
,u v
stringv |
descripcion	~ �
)
� �
{ 	
List 
< 
Detalle_transaccion $
>$ %
detalles& .
=/ 0
new1 4
List5 9
<9 :
Detalle_transaccion: M
>M N
(N O
)O P
;P Q
foreach 
( 
var 
detalle  
in! #!
inventarioValorizados$ 9
)9 :
{ 
detalles   
.   
Add   
(   
new    
Detalle_transaccion  ! 4
(  4 5
detalle  5 <
.  < =
Cantidad  = E
,  E F
detalle  G N
.  N O

IdConcepto  O Y
,  Y Z
descripcion  [ f
,  f g
detalle  h o
.  o p

,  } ~
detalle	   �
.
  � �

ValorTotal
  � �
,
  � �
null
  � �
,
  � �
detalle
  � �
.
  � � 
CantidadSecundaria
  � �
,
  � �
null
  � �
,
  � �
null
  � �
,
  � �
$num
  � �
,
  � �
$num
  � �
,
  � �
$num
  � �
)
  � �
)
  � �
;
  � �
}!! 
return"" 
detalles"" 
;"" 
}## 	
public$$ 
Detalle_transaccion$$ " 
ToDetalleTransaccion$$# 7
($$7 8
)$$8 9
{%% 	
return&& 
new&& 
Detalle_transaccion&& *
(&&* + 
IdDetalleTransaccion&&+ ?
,&&? @
Cantidad&&A I
,&&I J

IdConcepto&&K U
,&&U V
Descripcion&&W b
,&&b c

,&&q r

ValorTotal&&s }
,&&} ~
null	&& �
,
&&� � 
CantidadSecundaria
&&� �
,
&&� �
null
&&� �
,
&&� �
null
&&� �
,
&&� �
$num
&&� �
,
&&� �
$num
&&� �
,
&&� �
$num
&&� �
,
&&� �
Lote
&&� �
,
&&� �
null
&&� �
,
&&� �
null
&&� �
)
&&� �
;
&&� �
}'' 	
}(( 
})) �
iD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\InventarioValorizado.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{		 
public

 

class

  
InventarioValorizado

 %
:

% &
InventarioFisico

' 7
{ 
public
decimal

{
get
;
set
;
}
public 
decimal 

ValorTotal !
{" #
get$ '
;' (
set) ,
;, -
}. /
public  
InventarioValorizado #
(# $
)$ %
{ 	
} 	
new 
public 
List 
<  
InventarioValorizado ,
>, -
Convert. 5
(5 6
)6 7
{ 	
return 
new 
List 
<  
InventarioValorizado 0
>0 1
(1 2
)2 3
;3 4
} 	
public 
static 
List 
< 
Detalle_transaccion .
>. /!
ToDetallesTransaccion0 E
(E F
ListF J
<J K 
InventarioValorizadoK _
>_ `!
inventarioValorizadosa v
,v w
stringw }
descripcion	 �
)
� �
{ 	
List 
< 
Detalle_transaccion $
>$ %
detalles& .
=/ 0
new1 4
List5 9
<9 :
Detalle_transaccion: M
>M N
(N O
)O P
;P Q
foreach 
( 
var 
detalle  
in! #!
inventarioValorizados$ 9
)9 :
{ 
detalles 
. 
Add 
( 
new  
Detalle_transaccion! 4
(4 5
detalle5 <
.< =
Cantidad= E
,E F
detalleG N
.N O

IdConceptoO Y
,Y Z
descripcion[ f
,f g
detalleh o
.o p

,} ~
detalle	 �
.
� �

ValorTotal
� �
,
� �
null
� �
,
� �
detalle
� �
.
� � 
CantidadSecundaria
� �
,
� �
null
� �
,
� �
null
� �
,
� �
$num
� �
,
� �
$num
� �
,
� �
$num
� �
)
� �
)
� �
;
� �
}   
return!! 
detalles!! 
;!! 
}"" 	
}## 
}$$ �
nD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\PrincipalOrdenAlmacenData.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,
Core		, 0
.		0 1
Almacen		1 8
.		8 9

PlainModel		9 C
{

 
public 

class %
PrincipalOrdenAlmacenData *
{ 
public
ItemGenerico

{
get
;
set
;
}
public 
List 
< 
ItemGenerico  
>  !
	Almacenes" +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
DateTime 
FechaActual #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
FechaDesdeDefault '
{( )
get* -
{. /
return0 6
FechaActual7 B
.B C
ToStringC K
(K L
$strL X
)X Y
;Y Z
}[ \
}] ^
public 
string 
FechaHastaDefault '
{( )
get* -
{. /
return0 6
FechaActual7 B
.B C
ToStringC K
(K L
$strL X
)X Y
;Y Z
}[ \
}] ^
public 
bool 
EsAdministrador #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} �
`D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\StockMinMax.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public 

class 
StockMinMax 
{ 
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
StockMinimo "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
decimal 
StockMaximo "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
}
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\DetalleKardexValorizado.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 #
DetalleKardexValorizado		 (
:		( )
DetalleKardexFisico		* =
{

 
public 
decimal "
ImporteUnitarioEntrada -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
decimal 
ImporteTotalEntrada *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public
decimal
ImporteUnitarioSalida
{
get
;
set
;
}
public 
decimal 
ImporteTotalSalida )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
decimal  
ImporteUnitarioSaldo +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
decimal 
ImporteTotalSaldo (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public #
DetalleKardexValorizado &
(& '
)' (
{ 	
}
 
new 
public 
static 
List 
< #
DetalleKardexValorizado 6
>6 7
Convert8 ?
(? @
)@ A
{ 	
return 
new 
List 
< #
DetalleKardexValorizado 3
>3 4
(4 5
)5 6
;6 7
} 	
} 
} �
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\DetalleKardexFisico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 
DetalleKardexFisico		 $
{

 
public 
int 
Index 
{ 
get 
; 
set  #
;# $
}% &
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public
string
ActorExterno
{
get
;
set
;
}
public 
string 
	Operacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string !
CodigoTipoComprobante +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
Decimal 
CantidadEntrada &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Decimal 
CantidadSalida %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
Decimal 

{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
DetalleKardexFisico "
(" #
)# $
{ 	
}
 
public 
static 
List 
< 
DetalleKardexFisico .
>. /
Convert0 7
(7 8
)8 9
{ 	
return 
new 
List 
< 
DetalleKardexFisico /
>/ 0
(0 1
)1 2
;2 3
} 	
} 
}   �
oD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\VencimientoConceptoNegocio.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 &
VencimientoConceptoNegocio		 +
{

 
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
CodigoBarra !
{" #
get$ '
;' (
set) ,
;, -
}. /
public
string
Concepto
{
get
;
set
;
}
public 
string 
UnidadMedida "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
Lote 
{ 
get  
;  !
set" %
;% &
}' (
public 
DateTime 
FechaVencimiento (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public &
VencimientoConceptoNegocio )
() *
)* +
{ 	
} 	
public 
static 
List 
< &
VencimientoConceptoNegocio 5
>5 6
Convert7 >
(> ?
)? @
{ 	
return 
new 
List 
< &
VencimientoConceptoNegocio 6
>6 7
(7 8
)8 9
;9 :
} 	
} 
} �
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\ConceptoLote.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public 

class 
ConceptoLote 
{ 
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Lote 
{ 
get  
;  !
set" %
;% &
}' (
}		 
}

 �	
jD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\InventarioVencimiento.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 !
InventarioVencimiento		 &
:		& '
InventarioFisico		' 7
{

 
public 
DateTime 
? 
FechaVencimiento )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public !
InventarioVencimiento $
($ %
)% &
{ 	
} 	
new 
public 
List 
< !
InventarioVencimiento -
>- .
Convert/ 6
(6 7
)7 8
{ 	
return 
new 
List 
< !
InventarioVencimiento 1
>1 2
(2 3
)3 4
;4 5
} 	
} 
} �
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\InventarioSemaforo.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,
Core		, 0
.		0 1
Almacen		1 8
.		8 9

PlainModel		9 C
{

 
public 

class 
InventarioSemaforo #
:$ %
InventarioFisico& 6
{ 
public
decimal
StockMinimo
{
get
;
set
;
}
public 
decimal 
StockMaximo "
{# $
get% (
=>) +
StockMinimo, 7
*8 9
(: ;
$num; <
+= >
(? @
decimal@ G
)G H
ConceptoSettingsH X
.X Y
DefaultY `
.` a-
 PorcentajeParaObtenerStockMaximo	a �
/
� �
$num
� �
)
� �
;
� �
}
� �
public "
NivelStockSemaforoEnum %

{4 5
get6 9
=>: <
StockMinimo= H
==I K
$numL M
?N O"
NivelStockSemaforoEnumP f
.f g

:u v
Cantidadw 
<
� �
StockMinimo
� �
?
� �$
NivelStockSemaforoEnum
� �
.
� �
Bajo
� �
:
� �
Cantidad
� �
<
� �
StockMaximo
� �
?
� �$
NivelStockSemaforoEnum
� �
.
� �
Normal
� �
:
� �$
NivelStockSemaforoEnum
� �
.
� �
Alto
� �
;
� �
}
� �
public 
int 
ValorSemaforoInt #
{$ %
get& )
=>* ,
(- .
(. /
int/ 2
)2 3

)@ A
;A B
}C D
public 
InventarioSemaforo !
(! "
)" #
{ 	
} 	
new 
public 
List 
< 
InventarioSemaforo *
>* +
Convert, 3
(3 4
)4 5
{ 	
return 
new 
List 
< 
InventarioSemaforo .
>. /
(/ 0
)0 1
;1 2
} 	
}   
}!! �	
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\InventarioEnAlmacen.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 
InventarioEnAlmacen		 $
:		% &
InventarioFisico		' 7
{

 
public 
string 
Almacen 
{ 
get  #
;# $
set% (
;( )
}* +
public 
InventarioEnAlmacen "
(" #
)# $
{ 	
} 	
public 
List 
< 
InventarioEnAlmacen '
>' (
Convert) 0
(0 1
)1 2
{ 	
return 
new 
List 
< 
InventarioEnAlmacen /
>/ 0
(0 1
)1 2
;2 3
} 	
} 
} �
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\InventarioFisico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 
InventarioFisico		 !
{

 
public 
long  
IdDetalleTransaccion (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
long 

{" #
get$ '
;' (
set) ,
;, -
}. /
public
DateTime
Fecha
{
get
;
set
;
}
public 
int 
	IdAlmacen 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
CodigoBarra !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Familia 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
UnidadMedida "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
Lote 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
CantidadSecundaria )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
bool 

TieneStock 
{  
get! $
{% &
return' -
Cantidad. 6
>7 8
$num9 :
;: ;
}< =
}> ?
public 
InventarioFisico 
(  
)  !
{ 	
} 	
public 
List 
< 
InventarioFisico $
>$ %
Convert& -
(- .
). /
{ 	
return   
new   
List   
<   
InventarioFisico   ,
>  , -
(  - .
)  . /
;  / 0
}!! 	
}%% 
}&& �
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\EntradaAlmacen.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 
EntradaAlmacen		 
{

 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
	Operacion 
{  !
get" %
;% &
set' *
;* +
}, -
public
string
Origen
{
get
;
set
;
}
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
Empleado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
Decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
EntradaAlmacen "
>" #
Convert$ +
(+ ,
), -
{ 	
return 
new 
List 
< 
EntradaAlmacen *
>* +
(+ ,
), -
;- .
} 	
} 
} �
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\PlainModel\SalidaAlmacen.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Almacen1 8
.8 9

PlainModel9 C
{ 
public		 

class		 

{

 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
	Operacion 
{  !
get" %
;% &
set' *
;* +
}, -
public
string
Destino
{
get
;
set
;
}
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
Empleado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
Decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 

>! "
Convert# *
(* +
)+ ,
{ 	
return 
new 
List 
< 

>) *
(* +
)+ ,
;, -
} 	
} 
} �
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\ReportData\PrincipalReportData.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Negocio		 %
.		% &
Almacen		& -
.		- .
Report		. 4
{

 
public 

class 
PrincipalReportData $
{ 
public
List
<
Establecimiento
>
Establecimientos
{
get
;
set
;
}
public 
Establecimiento !
EstablecimientoSesion 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
ItemGenerico 

{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
List 
< 
ItemGenerico  
>  !
	Almacenes" +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
DateTime 
FechaActual_ $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
List 
< &
Familia_Concepto_Comercial .
>. /
Familias0 8
{9 :
get; >
;> ?
set@ C
;C D
}E F
public 
long 
FechaActual 
{  !
get" %
{& '
return( .
FechaActual_/ ;
.; <$
ToJavaScriptMilliseconds< T
(T U
)U V
;V W
}X Y
}Z [
public 
long 
FechaHastaDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
long 
FechaDesdeDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
List 
< 
ItemGenerico  
>  !
	Conceptos" +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
bool 
EsAdministrador #
{$ %
get& )
;) *
set+ .
;. /
}0 1
}!! 
}"" �
dD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\ComprobanteDeOperacion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Comprobante1 <
{ 
public		 

class		 "
ComprobanteDeOperacion		 '
{

 
public 
long 
IdOperacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Comprobante !
{" #
get$ '
;' (
set) ,
;, -
}. /
}
} �
iD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\DocumentoDePedido.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
ComprobantesModel( 9
{
public 

class 
DocumentoDePedido "
:" #%
DocumentoElectronicoVenta# <
{ 
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: L
;L M
public 
bool 

MostrarIgv 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool )
MostrarInformacionAdicional80 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicional80 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
DocumentoDePedido  
(  !

orden/ 4
,4 54
(EstablecimientoComercialExtendidoConLogo6 ^
sede_ c
,c d.
!EstablecimientoComercialExtendido	e �
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	

MostrarIgv 
= 
orden 
. 
Igv "
(" #
)# $
>% &
$num' (
;( ))
MostrarInformacionAdicional80 )
=* +
!, -
String- 3
.3 4

(A B
AplicacionSettingsB T
.T U
DefaultU \
.\ ],
 InformacionAdicionalCotizacion80] }
)} ~
;~ "
InformacionAdicional80 "
=# $
AplicacionSettings% 7
.7 8
Default8 ?
.? @,
 InformacionAdicionalCotizacion80@ `
;` a
} 	
} 
} �$
tD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\DocumentoMovimientoDeAlmacen.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class (
DocumentoMovimientoDeAlmacen -
:. /%
DocumentoElectronicoVenta0 I
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public (
DocumentoMovimientoDeAlmacen +
(+ ,
), -
{ 	
} 	
public (
DocumentoMovimientoDeAlmacen +
(+ ,
MovimientoDeAlmacen, ?

movimiento@ J
,J K4
(EstablecimientoComercialExtendidoConLogoL t
sedeu y
,y z.
!EstablecimientoComercialExtendido	{ �
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
FechaEmision 
= 

movimiento %
.% &
FechaEmision& 2
;2 3
Emisor 
= 
new 
Emisor 
(  
sede  $
,$ %
establecimiento& 5
)5 6
;6 7
Receptor 
= 
new 
Receptor #
(# $
new$ '-
!EstablecimientoComercialExtendido( I
(I J

movimientoK U
.U V
TransaccionV a
(a b
)b c
.c d
Actor_negocio1d r
)r s
)s t
;t u
Detalles 
= 
Detalle 
. 
Convert &
(& '

movimiento' 1
.1 2
Detalles2 :
(: ;
); <
,< =(
modoImpresionCaracteristicas> Z
,Z [
$str\ ^
,^ _
false` e
)e f
;f g"
MostrarMensajeAmazonia "
=# $
false% *
;* +
MensajeNegocio 
= 
AplicacionSettings /
./ 0
Default0 7
.7 8
MensajeDeNegocio8 H
;H I
Observacion 
= 

movimiento $
.$ %
Observacion% 0
(0 1
)1 2
;2 3
MostrarLogo 
= *
FacturacionElectronicaSettings 8
.8 9
Default9 @
.@ A+
MostrarLogoEnComprobanteImpresoA `
;` a
CodigoQR 
= 
qrBytes 
; 
Serie 
= 

movimiento 
. 
Comprobante *
(* +
)+ ,
., -

;: ;
Numero   
=   

movimiento   
.    
Comprobante    +
(  + ,
)  , -
.  - .
NumeroDeComprobante  . A
;  A B
ImporteTotal!! 
=!! 

movimiento!! %
.!!% &
Total!!& +
;!!+ , 
ImporteTotalEnLetras""  
=""! "
Util""# '
.""' (
	APalabras""( 1
(""1 2

movimiento""2 <
.""< =
Total""= B
,""B C

movimiento""D N
.""N O
MonedaPlural""O [
(""[ \
)""\ ]
)""] ^
;""^ _
	Descuento## 
=## 

movimiento## "
.##" #
	Descuento### ,
(##, -
)##- .
;##. /
Igv$$ 
=$$ 

movimiento$$ 
.$$ 
Igv$$  
($$  !
)$$! "
;$$" #
MostrarTestigo%% 
=%% $
mostrarEncabezadoTestigo%% 5
;%%5 6'
ResolucionAutorizacionSunat&& '
=&&( )*
FacturacionElectronicaSettings&&* H
.&&H I
Default&&I P
.&&P Q(
ResolucionEmisionElectronica&&Q m
;&&m n

NombreTipo'' 
='' 

movimiento'' #
.''# $
Comprobante''$ /
(''/ 0
)''0 1
.''1 2

NombreTipo''2 <
;''< =
}(( 	
}** 
}++ �M
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeCreditoInterna.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class  
NotaDeCreditoInterna %
:& '%
DocumentoElectronicoVenta( A
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: S
;S T
public 
string 
TipoDeNotaCredito '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 

Referencia 
Discrepancia &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Relacionado  
DocumentoRelacionado /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public  
NotaDeCreditoInterna #
(# $
OperacionDeVenta$ 4
	operacion5 >
,> ?-
!EstablecimientoComercialExtendido@ a
sedeb f
,f g.
!EstablecimientoComercialExtendido	h �
establecimiento
� �
,
� �
List
� �
<
� �
Detalle_maestro
� �
>
� �"
tiposDeNotaDeCredito
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
	operacion
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TipoDeNotaCredito 
=  
tiposDeNotaDeCredito  4
.4 5
SingleOrDefault5 D
(D E
tE F
=>G I
tJ K
.K L
codigoL R
==S U
	operacionV _
._ `$
CodigoSunatDeTransaccion` x
(x y
)y z
)z {
.{ |
nombre	| �
;
� �
Discrepancia 
= 
new 

Referencia )
{ 

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

+^ _
$str` c
+d e
	operacionf o
.o p"
OperacionDeReferencia	p �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �
Tipo 
= 
	operacion  
.  !$
CodigoSunatDeTransaccion! 9
(9 :
): ;
,; <
Descripcion 
= 
	operacion '
.' (

Comentario( 2
,2 3
} 
;
DocumentoRelacionado  
=! "
new# &
Relacionado' 2
{ 
NroDocumento 
= 
	operacion (
.( )!
OperacionDeReferencia) >
(> ?
)? @
.@ A
ComprobanteA L
(L M
)M N
.N O

+] ^
$str_ b
+c d
	operacione n
.n o"
OperacionDeReferencia	o �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

CodigoTipoP Z
,Z [
}   
;  
}!! 	
public##  
NotaDeCreditoInterna## #
(### $
OperacionDeVenta##$ 4
	operacion##5 >
,##> ?4
(EstablecimientoComercialExtendidoConLogo##@ h
sede##i m
,##m n.
!EstablecimientoComercialExtendido	##o �
establecimiento
##� �
,
##� �
List
##� �
<
##� �
Detalle_maestro
##� �
>
##� �"
tiposDeNotaDeCredito
##� �
,
##� �
byte
##� �
[
##� �
]
##� �
qrBytes
##� �
,
##� �
bool
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �.
 ModoImpresionCaracteristicasEnum
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
:
##� �
base
##� �
(
##� �
	operacion
##� �
,
##� �
sede
##� �
,
##� �
establecimiento
##� �
,
##� �
qrBytes
##� �
,
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
{$$ 	
TipoDeNotaCredito%% 
=%%  
tiposDeNotaDeCredito%%  4
.%%4 5
SingleOrDefault%%5 D
(%%D E
t%%E F
=>%%G I
t%%J K
.%%K L
codigo%%L R
==%%S U
	operacion%%V _
.%%_ `$
CodigoSunatDeTransaccion%%` x
(%%x y
)%%y z
)%%z {
.%%{ |
nombre	%%| �
;
%%� �
Discrepancia&& 
=&& 
new&& 

Referencia&& )
{'' 

=(( 
	operacion((  )
.(() *!
OperacionDeReferencia((* ?
(((? @
)((@ A
.((A B
Comprobante((B M
(((M N
)((N O
.((O P

+((^ _
$str((` c
+((d e
	operacion((f o
.((o p"
OperacionDeReferencia	((p �
(
((� �
)
((� �
.
((� �
Comprobante
((� �
(
((� �
)
((� �
.
((� �!
NumeroDeComprobante
((� �
,
((� �
Tipo)) 
=)) 
	operacion))  
.))  !$
CodigoSunatDeTransaccion))! 9
())9 :
))): ;
,)); <
Descripcion** 
=** 
	operacion** '
.**' (

Comentario**( 2
,**2 3
}++ 
;++
DocumentoRelacionado,,  
=,,! "
new,,# &
Relacionado,,' 2
{-- 
NroDocumento.. 
=.. 
	operacion.. (
...( )!
OperacionDeReferencia..) >
(..> ?
)..? @
...@ A
Comprobante..A L
(..L M
)..M N
...N O

+..] ^
$str.._ b
+..c d
	operacion..e n
...n o"
OperacionDeReferencia	..o �
(
..� �
)
..� �
.
..� �
Comprobante
..� �
(
..� �
)
..� �
.
..� �!
NumeroDeComprobante
..� �
,
..� �

=// 
	operacion//  )
.//) *!
OperacionDeReferencia//* ?
(//? @
)//@ A
.//A B
Comprobante//B M
(//M N
)//N O
.//O P

CodigoTipo//P Z
,//Z [
}00 
;00
}11 	
public33 
static33 
List33 
<33  
NotaDeCreditoInterna33 /
>33/ 0
Convert331 8
(338 9
List339 =
<33= >
OperacionDeVenta33> N
>33N O
operaciones33P [
,33[ \-
!EstablecimientoComercialExtendido33] ~
sede	33 �
,
33� �
List
33� �
<
33� �
Detalle_maestro
33� �
>
33� �"
tiposDeNotaDeCredito
33� �
,
33� �.
 ModoImpresionCaracteristicasEnum
33� �*
modoImpresionCaracteristicas
33� �
)
33� �
{44 	
List55 
<55  
NotaDeCreditoInterna55 %
>55% &
	resultado55' 0
=551 2
new553 6
List557 ;
<55; < 
NotaDeCreditoInterna55< P
>55P Q
(55Q R
)55R S
;55S T
foreach66 
(66 
var66 
item66 
in66  
operaciones66! ,
)66, -
{77 
	resultado88 
.88 
Add88 
(88 
new88 ! 
NotaDeCreditoInterna88" 6
(886 7
item887 ;
,88; <
sede88= A
,88A B
new88C F4
(EstablecimientoComercialExtendidoConLogo88G o
(88o p
item88p t
.88t u
Transaccion	88u �
(
88� �
)
88� �
.
88� �
Actor_negocio2
88� �
.
88� �
Actor_negocio2
88� �
)
88� �
,
88� �"
tiposDeNotaDeCredito
88� �
,
88� �
null
88� �
,
88� �
false
88� �
,
88� �*
modoImpresionCaracteristicas
88� �
)
88� �
)
88� �
;
88� �
}99 
return:: 
	resultado:: 
;:: 
};; 	
}<< 
}== �M
kD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeDebitoInterna.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
NotaDeDebitoInterna $
:% &%
DocumentoElectronicoVenta' @
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: R
;R S
public 
string 
TipoDeNotaDebito &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 

Referencia 
Discrepancia &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Relacionado  
DocumentoRelacionado /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
NotaDeDebitoInterna "
(" #
OperacionDeVenta# 3
	operacion4 =
,= >-
!EstablecimientoComercialExtendido? `
sedea e
,e f.
!EstablecimientoComercialExtendido	g �
establecimiento
� �
,
� �
List
� �
<
� �
Detalle_maestro
� �
>
� �!
tiposDeNotaDeDebito
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
	operacion
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TipoDeNotaDebito 
= 
tiposDeNotaDeDebito 2
.2 3
SingleOrDefault3 B
(B C
tC D
=>E G
tH I
.I J
codigoJ P
==Q S
	operacionT ]
.] ^$
CodigoSunatDeTransaccion^ v
(v w
)w x
)x y
.y z
nombre	z �
;
� �
Discrepancia 
= 
new 

Referencia )
{ 

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

+^ _
$str` c
+d e
	operacionf o
.o p"
OperacionDeReferencia	p �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �
Tipo 
= 
	operacion  
.  !$
CodigoSunatDeTransaccion! 9
(9 :
): ;
,; <
Descripcion 
= 
	operacion '
.' (

Comentario( 2
,2 3
} 
;
DocumentoRelacionado  
=! "
new# &
Relacionado' 2
{ 
NroDocumento 
= 
	operacion (
.( )!
OperacionDeReferencia) >
(> ?
)? @
.@ A
ComprobanteA L
(L M
)M N
.N O

+] ^
$str_ b
+c d
	operacione n
.n o"
OperacionDeReferencia	o �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

CodigoTipoP Z
,Z [
}   
;  
}!! 	
public## 
NotaDeDebitoInterna## "
(##" #
OperacionDeVenta### 3
	operacion##4 =
,##= >4
(EstablecimientoComercialExtendidoConLogo##? g
sede##h l
,##l m.
!EstablecimientoComercialExtendido	##n �
establecimiento
##� �
,
##� �
List
##� �
<
##� �
Detalle_maestro
##� �
>
##� �!
tiposDeNotaDeDebito
##� �
,
##� �
byte
##� �
[
##� �
]
##� �
qrBytes
##� �
,
##� �
bool
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �.
 ModoImpresionCaracteristicasEnum
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
:
##� �
base
##� �
(
##� �
	operacion
##� �
,
##� �
sede
##� �
,
##� �
establecimiento
##� �
,
##� �
qrBytes
##� �
,
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
{$$ 	
TipoDeNotaDebito%% 
=%% 
tiposDeNotaDeDebito%% 2
.%%2 3
SingleOrDefault%%3 B
(%%B C
t%%C D
=>%%E G
t%%H I
.%%I J
codigo%%J P
==%%Q S
	operacion%%T ]
.%%] ^$
CodigoSunatDeTransaccion%%^ v
(%%v w
)%%w x
)%%x y
.%%y z
nombre	%%z �
;
%%� �
Discrepancia&& 
=&& 
new&& 

Referencia&& )
{'' 

=(( 
	operacion((  )
.(() *!
OperacionDeReferencia((* ?
(((? @
)((@ A
.((A B
Comprobante((B M
(((M N
)((N O
.((O P

+((^ _
$str((` c
+((d e
	operacion((f o
.((o p"
OperacionDeReferencia	((p �
(
((� �
)
((� �
.
((� �
Comprobante
((� �
(
((� �
)
((� �
.
((� �!
NumeroDeComprobante
((� �
,
((� �
Tipo)) 
=)) 
	operacion))  
.))  !$
CodigoSunatDeTransaccion))! 9
())9 :
))): ;
,)); <
Descripcion** 
=** 
	operacion** '
.**' (

Comentario**( 2
,**2 3
}++ 
;++
DocumentoRelacionado,,  
=,,! "
new,,# &
Relacionado,,' 2
{-- 
NroDocumento.. 
=.. 
	operacion.. (
...( )!
OperacionDeReferencia..) >
(..> ?
)..? @
...@ A
Comprobante..A L
(..L M
)..M N
...N O

+..] ^
$str.._ b
+..c d
	operacion..e n
...n o"
OperacionDeReferencia	..o �
(
..� �
)
..� �
.
..� �
Comprobante
..� �
(
..� �
)
..� �
.
..� �!
NumeroDeComprobante
..� �
,
..� �

=// 
	operacion//  )
.//) *!
OperacionDeReferencia//* ?
(//? @
)//@ A
.//A B
Comprobante//B M
(//M N
)//N O
.//O P

CodigoTipo//P Z
,//Z [
}00 
;00
}11 	
public33 
static33 
List33 
<33 
NotaDeDebitoInterna33 .
>33. /
Convert330 7
(337 8
List338 <
<33< =
OperacionDeVenta33= M
>33M N
operaciones33O Z
,33Z [-
!EstablecimientoComercialExtendido33\ }
sede	33~ �
,
33� �
List
33� �
<
33� �
Detalle_maestro
33� �
>
33� �!
tiposDeNotaDeDebito
33� �
,
33� �.
 ModoImpresionCaracteristicasEnum
33� �*
modoImpresionCaracteristicas
33� �
)
33� �
{44 	
List55 
<55 
NotaDeDebitoInterna55 $
>55$ %
	resultado55& /
=550 1
new552 5
List556 :
<55: ;
NotaDeDebitoInterna55; N
>55N O
(55O P
)55P Q
;55Q R
foreach66 
(66 
var66 
item66 
in66  
operaciones66! ,
)66, -
{77 
	resultado88 
.88 
Add88 
(88 
new88 !
NotaDeDebitoInterna88" 5
(885 6
item886 :
,88: ;
sede88< @
,88@ A
new88B E4
(EstablecimientoComercialExtendidoConLogo88F n
(88n o
item88o s
.88s t
Transaccion88t 
(	88 �
)
88� �
.
88� �
Actor_negocio2
88� �
.
88� �
Actor_negocio2
88� �
)
88� �
,
88� �!
tiposDeNotaDeDebito
88� �
,
88� �
null
88� �
,
88� �
false
88� �
,
88� �*
modoImpresionCaracteristicas
88� �
)
88� �
)
88� �
;
88� �
}99 
return:: 
	resultado:: 
;:: 
};; 	
}<< 
}== �
nD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaInvalidacionCompra.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class "
NotaInvalidacionCompra '
:( )&
DocumentoElectronicoCompra* D
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: Z
;Z [
public "
NotaInvalidacionCompra %
(% &
)& '
{ 	
} 	
public "
NotaInvalidacionCompra %
(% &

orden4 9
,9 :4
(EstablecimientoComercialExtendidoConLogo; c
seded h
,h i
bytej n
[n o
]o p
qrBytesq x
,x y
boolz ~%
mostrarEncabezadoTestigo	 �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	'
ResolucionAutorizacionSunat '
=( )
$str* ,
;, -
} 	
} 
} �
mD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaInvalidacionVenta.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class !
NotaInvalidacionVenta &
:' (%
DocumentoElectronicoVenta) B
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: Y
;Y Z
public 
bool )
MostrarInformacionAdicional80 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicional80 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
bool )
MostrarInformacionAdicionalA4 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicionalA4 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public !
NotaInvalidacionVenta $
($ %
)% &
{ 	
} 	
public !
NotaInvalidacionVenta $
($ %
OrdenDeVenta% 1
orden2 7
,7 84
(EstablecimientoComercialExtendidoConLogo9 a
sedeb f
,f g.
!EstablecimientoComercialExtendido	h �
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	'
ResolucionAutorizacionSunat '
=( )
$str* ,
;, -)
MostrarInformacionAdicional80 )
=* +
!, -
String- 3
.3 4

(A B
VentasSettingsB P
.P Q
DefaultQ X
.X Y+
InformacionAdicionalNotaVenta80Y x
)x y
;y z"
InformacionAdicional80 "
=# $
VentasSettings% 3
.3 4
Default4 ;
.; <+
InformacionAdicionalNotaVenta80< [
;[ \)
MostrarInformacionAdicionalA4 )
=* +
!, -
String- 3
.3 4

(A B
VentasSettingsB P
.P Q
DefaultQ X
.X Y+
InformacionAdicionalNotaVentaA4Y x
)x y
;y z"
InformacionAdicionalA4 "
=# $
VentasSettings% 3
.3 4
Default4 ;
.; <+
InformacionAdicionalNotaVentaA4< [
;[ \
} 	
}HH 
}II �
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\TipoTransaccionTipoComprobante.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 3
'OperacionTipoTransaccionTipoComprobante		 8
{

 
public 
long 
IdOperacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
int 
IdTipoTransaccion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
int
IdTipoComprobante
{
get
;
set
;
}
public 
string 
SerieComprobante &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
int 
NumeroComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Tercero 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DateTime 
FechaInicio #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
FechaEmision "
{# $
get% (
=>) +
FechaInicio, 7
.7 8
ToString8 @
(@ A
$strA M
)M N
;N O
}P Q
public 
string 
Comprobante !
{" #
get$ '
=>( *
SerieComprobante+ ;
+< =
$str> C
+D E
NumeroComprobanteF W
.W X
ToStringX `
(` a
)a b
;b c
}d e
} 
} �
iD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Conceptos\ConceptoConSusCaracteristicas.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 )
ConceptoConSusCaracteristicas

 .
{ 
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public
string
 NombreConceptoSinCaracteristicas
{
get
;
set
;
}
public 
List 
< 
ItemGenerico  
>  !
Caracteristicas" 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public )
ConceptoConSusCaracteristicas ,
(, -
)- .
{ 	
} 	
} 
} �
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Conceptos\Concepto.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Concepto1 9
{ 
public		 

class		 
Concepto		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public
string
CodigoBarra
{
get
;
set
;
}
public 
string 
Familia 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
UnidadMedida "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
decimal 
StockMinimo "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
Concepto 
( 
) 
{ 	
} 	
} 
} �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Conceptos\ReporteDigemid.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 
ReporteDigemid

 
{ 
public 
string !
CodigoEstablecimiento +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public
string
CodigoConcepto
{
get
;
set
;
}
public 
decimal 
PrecioUnitario %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal !
PrecioUnitarioPaquete ,
{ 	
get 
{ 
return 
PrecioUnitario #
-$ %
(& '
PrecioUnitario' 5
*6 7
(8 9
Convert9 @
.@ A
	ToDecimalA J
(J K
ConceptoSettingsK [
.[ \
Default\ c
.c dI
<PorcentajeDescuentoParaPrecioUnitarioPaqueteEnReporteDigemid	d �
)
� �
/
� �
$num
� �
)
� �
)
� �
;
� �
}
� �
} 	
public 
ReporteDigemid 
( 
) 
{ 	
}
 
} 
} �
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionDePedido.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Configuraciones1 @
{		 
public

 

class

 !
ConfiguracionDePedido

 &
{ 
public 
readonly 
bool )
MostrarSeccionEntregaEnPedido :
=; <
PedidoSettings= K
.K L
DefaultL S
.S T)
MostrarSeccionEntregaEnPedidoT q
;q r
public
string
FechaActual
;
} 
} �
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionesLogica.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
{

 
public 

sealed 
class !
ConfiguracionesLogica -
{ 
public 
static 
void 
Reset  
(  !
)! "
{ 	$
ConfiguracionTrazaDePago $
.$ %
Reset% *
(* +
)+ ,
;, -
} 	
} 
} �
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionNota.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
{

 
public 

class 
ConfiguracionNota "
{ 
public
readonly
int
&IdDetalleMaestroAnulacionDeLaOperacion
=
MaestroSettings
.
Default
.
>IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion	
;

public 
readonly 
int +
IdDetalleMaestroDescuentoGlobal ;
=< =
MaestroSettings> M
.M N
DefaultN U
.U VD
7IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal	V �
;
� �
public 
readonly 
int ,
 IdDetalleMaestroDescuentoPorItem <
== >
MaestroSettings? N
.N O
DefaultO V
.V WE
8IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem	W �
;
� �
public 
readonly 
int +
IdDetalleMaestroDevolucionTotal ;
=< =
MaestroSettings> M
.M N
DefaultN U
.U VD
7IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal	V �
;
� �
public 
readonly 
int -
!IdDetalleMaestroDevolucionPorItem =
=> ?
MaestroSettings@ O
.O P
DefaultP W
.W XF
9IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem	X �
;
� �
public 
readonly 
int ,
 IdDetalleMaestroInteresesPorMora <
== >
MaestroSettings? N
.N O
DefaultO V
.V WD
7IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora	W �
;
� �
public 
readonly 
int ,
 IdDetalleMaestroAumentoEnElValor <
== >
MaestroSettings? N
.N O
DefaultO V
.V WD
7IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor	W �
;
� �
public 
readonly 
int %
NumeroDecimalesEnCantidad 5
=6 7
AplicacionSettings8 J
.J K
DefaultK R
.R S%
NumeroDecimalesEnCantidadS l
;l m
public 
readonly 
int #
NumeroDecimalesEnPrecio 3
=4 5
AplicacionSettings6 H
.H I
DefaultI P
.P Q#
NumeroDecimalesEnPrecioQ h
;h i
public 
readonly 
bool (
MostrarSeccionEntregaEnVenta 9
=: ;
VentasSettings< J
.J K
DefaultK R
.R S(
MostrarSeccionEntregaEnVentaS o
;o p
public 
readonly 
decimal 
TasaIGV  '
=( )
TransaccionSettings* =
.= >
Default> E
.E F
TasaIGVF M
;M N
} 
} �2
nD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionRegistroDetalle.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
{

 
public 

sealed 
class (
ConfiguracionRegistroDetalle 4
{ 
public
readonly
bool
AplicaLeyAmazonia
=
TransaccionSettings
.
Default
.
AplicaLeyAmazonia
;
public 
readonly 
decimal 
TasaIGV  '
=( )
TransaccionSettings* =
.= >
Default> E
.E F
TasaIGVF M
;M N
public 
bool 3
'SalidaBienesSujetasADisponibilidadStock ;
;; <
public 
bool $
PermitirIngresarCantidad ,
;, -
public 
bool *
PermitirIngresarPrecioUnitario 2
;2 3
public 
bool #
PermitirIngresarImporte +
;+ ,
public 
bool 2
&IngresarCantidadCalcularPrecioUnitario :
;: ;
public 
bool 1
%IngresarPrecioUnitarioCalcularImporte 9
;9 :
public 
bool +
IngresarImporteCalcularCantidad 3
;3 4
public 
readonly 
bool !
PermitirRegistroFlete 2
=3 4
AplicacionSettings5 G
.G H
DefaultH O
.O P(
PermitirRegistroFleteEnVentaP l
;l m
public 
readonly 
int "
IdFamiliaBolsaPlastica 2
=3 4
MaestroSettings5 D
.D E
DefaultE L
.L M7
+IdDetalleMaestroConceptoBasicoBolsaPlasticaM x
;x y
public 
decimal "
CostoUnitarioDelIcbper -
;- .
public 
readonly 
int *
IdTarifaSeleccionadoPorDefecto :
=; <
VentasSettings= K
.K L
DefaultL S
.S T*
IdTarifaSeleccionadoPorDefectoT r
;r s
public 
readonly 
int %
NumeroDecimalesEnCantidad 5
=6 7
AplicacionSettings8 J
.J K
DefaultK R
.R S%
NumeroDecimalesEnCantidadS l
;l m
public 
readonly 
int #
NumeroDecimalesEnPrecio 3
=4 5
AplicacionSettings6 H
.H I
DefaultI P
.P Q#
NumeroDecimalesEnPrecioQ h
;h i
public 
readonly 
int $
ModoSeleccionTipoFamilia 4
=5 6
AplicacionSettings7 I
.I J
DefaultJ Q
.Q R0
$ModoDeSeleccionTipoDeFamiliaEnVentasR v
;v w
public 
readonly 
bool &
MostrarBuscadorCodigoBarra 7
=8 9
VentasSettings: H
.H I
DefaultI P
.P Q/
#ModoDeIngresoDeCodigoDeBarraEnVentaQ t
==u w
(x y
inty |
)| }*
ModoIngresoCodigoBarraEnVenta	} �
.
� �#
CodigoBarraDeProducto
� �
||
� �
VentasSettings
� �
.
� �
Default
� �
.
� �1
#ModoDeIngresoDeCodigoDeBarraEnVenta
� �
==
� �
(
� �
int
� �
)
� �+
ModoIngresoCodigoBarraEnVenta
� �
.
� �
Ambos
� �
;
� �
public 
readonly 
int !
ModoSeleccionConcepto 1
=2 3
VentasSettings4 B
.B C
DefaultC J
.J K.
"ModoDeSeleccionDeConceptoDeNegocioK m
;m n
public 
readonly 
int (
TiempoEsperaBusquedaSelector 8
=9 :
AplicacionSettings; M
.M N
DefaultN U
.U V=
0TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad	V �
;
� �
public   
readonly   
int   *
MinimoCaracteresBuscarConcepto   :
=  ; <
AplicacionSettings  = O
.  O P
Default  P W
.  W X;
.MinimoDeCaracteresParaBuscarEnSelectorConcepto	  X �
;
  � �
public!! 
readonly!! 
bool!! %
AplicarCantidadPorDefecto!! 6
=!!7 8
AplicacionSettings!!9 K
.!!K L
Default!!L S
.!!S T-
!AplicarCantidadPorDefectoEnVentas!!T u
;!!u v
public"" 
readonly"" 
string"" 
CantidadPorDefecto"" 1
=""2 3
AplicacionSettings""4 F
.""F G
Default""G N
.""N O&
CantidadPorDefectoEnVentas""O i
;""i j
public## 
readonly## 
string## &
MascaraDeCalculoPorDefecto## 9
=##: ;
VentasSettings##< J
.##J K
Default##K R
.##R S.
"MascaraDeCalculoPorDefectoEnVentas##S u
;##u v
public$$ 
readonly$$ 
string$$ 3
'MascaraDeCalculoPrecioUnitarioCalculado$$ F
=$$G H
VentasSettings$$I W
.$$W X
Default$$X _
.$$_ `4
'MascaraDeCalculoPrecioUnitarioCalculado	$$` �
;
$$� �
public%% 
readonly%% 
int%% '
InformacionSelectorConcepto%% 7
=%%8 9
VentasSettings%%: H
.%%H I
Default%%I P
.%%P Q/
#InformacionSelectorConceptoEnVentas%%Q t
;%%t u
public&& 
readonly&& 
int&& ,
 FlujoDespuesDeCodigoBarraEnVenta&& <
=&&= >
VentasSettings&&? M
.&&M N
Default&&N U
.&&U V,
 FlujoDespuesDeCodigoBarraEnVenta&&V v
;&&v w
public'' 
readonly'' 
int'' (
FlujoDespuesDeImporteEnVenta'' 8
=''9 :
VentasSettings''; I
.''I J
Default''J Q
.''Q R(
FlujoDespuesDeImporteEnVenta''R n
;''n o
})) 
}** �	
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionPago.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
{

 
public 

class 
ConfiguracionPago "
{ 
public
readonly
int
ModoPagoContado
=
(
int
)
ModoPago
.
Contado
;
public 
readonly 
int !
ModoPagoCreditoRapido 1
=2 3
(4 5
int5 8
)8 9
ModoPago9 A
.A B

;O P
public 
readonly 
int &
ModoPagoCreditoConfigurado 6
=7 8
(9 :
int: =
)= >
ModoPago> F
.F G
CreditoConfiguradoG Y
;Y Z
public 
string 
FechaActual !
;! "
} 
} �6
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Facturacion\Atencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Facturacion1 <
{		 
public

 

class

 
Atencion

 
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
string
Codigo
{
get
;
set
;
}
public 
IEnumerable 
< 

>( )
Ordenes* 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 

OrdenPrincipal +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
decimal 
Importe 
{  
get! $
=>% '
(( )
TieneFacturacion) 9
?: ;
$num< =
:> ?
OrdenPrincipal@ N
.N O
ImporteO V
)V W
+X Y
(Z [
Ordenes[ b
==c e
nullf j
?k l
$numm n
:o p
Ordenesq x
.x y
Wherey ~
(~ 
o	 �
=>
� �
o
� �
.
� �
Id
� �
!=
� �
OrdenPrincipal
� �
.
� �
Id
� �
&&
� �
!
� �
o
� �
.
� �
TieneFacturacion
� �
)
� �
.
� �
Sum
� �
(
� �
d
� �
=>
� �
d
� �
.
� �
Importe
� �
)
� �
)
� �
;
� �
}
� �
public 
bool 
TieneFacturacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
IEnumerable 
<  
ComprobanteFacturado /
>/ 0
ComprobantesOrden1 B
{C D
getE H
=>I K&
ComprobantesOrdenPrincipalL f
==g i
nullj n
?o p
(q r(
ComprobantesOrdenSecundario	r �
??
� �
null
� �
)
� �
:
� �
(
� �)
ComprobantesOrdenSecundario
� �
==
� �
null
� �
?
� �(
ComprobantesOrdenPrincipal
� �
:
� �(
ComprobantesOrdenPrincipal
� �
.
� �
Union
� �
(
� �)
ComprobantesOrdenSecundario
� �
)
� �
)
� �
;
� �
}
� �
public 
IEnumerable 
<  
ComprobanteFacturado /
>/ 0&
ComprobantesOrdenPrincipal1 K
{L M
getN Q
;Q R
setS V
;V W
}X Y
public 
IEnumerable 
<  
ComprobanteFacturado /
>/ 0'
ComprobantesOrdenSecundario1 L
{M N
getO R
;R S
setT W
;W X
}Y Z
public 
IEnumerable 
<  
ComprobanteFacturado /
>/ 0"
ComprobantesReferencia1 G
{H I
getJ M
=>N P+
ComprobantesReferenciaPrincipalQ p
==q s
nullt x
?y z
({ |-
 ComprobantesReferenciaSecundario	| �
??
� �
null
� �
)
� �
:
� �
(
� �.
 ComprobantesReferenciaSecundario
� �
==
� �
null
� �
?
� �-
ComprobantesReferenciaPrincipal
� �
:
� �-
ComprobantesReferenciaPrincipal
� �
.
� �
Union
� �
(
� �.
 ComprobantesReferenciaSecundario
� �
)
� �
)
� �
;
� �
}
� �
public 
IEnumerable 
<  
ComprobanteFacturado /
>/ 0+
ComprobantesReferenciaPrincipal1 P
{Q R
getS V
;V W
setX [
;[ \
}] ^
public 
IEnumerable 
<  
ComprobanteFacturado /
>/ 0,
 ComprobantesReferenciaSecundario1 Q
{R S
getT W
;W X
setY \
;\ ]
}^ _
public 
IEnumerable 
<  
ComprobanteFacturado /
>/ 0"
ComprobantesFacturados1 G
{H I
getJ M
=>N P
ComprobantesOrdenQ b
==c e
nullf j
?k l
(m n#
ComprobantesReferencia	n �
??
� �
null
� �
)
� �
:
� �
(
� �$
ComprobantesReferencia
� �
==
� �
null
� �
?
� �(
ComprobantesOrdenPrincipal
� �
:
� �
ComprobantesOrden
� �
.
� �
Union
� �
(
� �$
ComprobantesReferencia
� �
)
� �
)
� �
;
� �
}
� �
public 
List 
< 
DatosVentaIntegrada '
>' (
NuevosComprobantes) ;
{< =
get> A
;A B
setC F
;F G
}H I
public 
int 

TipoDePago 
{ 
get  #
;# $
set% (
;( )
}* +
public 
Atencion 
( 
) 
{ 
} 
} 
public   

class    
ComprobanteFacturado   %
{!! 
public"" 
long"" 
IdOrden"" 
{"" 
get"" !
;""! "
set""# &
;""& '
}""( )
public## 
long## 
Id## 
{## 
get## 
=>## 
IdOrden##  '
;##' (
}##) *
public$$ 
string$$ %
CadenaHtmlDeComprobante80$$ /
{$$0 1
get$$2 5
;$$5 6
set$$7 :
;$$: ;
}$$< =
public%% 
string%% %
CadenaHtmlDeComprobanteA4%% /
{%%0 1
get%%2 5
;%%5 6
set%%7 :
;%%: ;
}%%< =
public&&  
ComprobanteFacturado&& #
(&&# $
)&&$ %
{&&& '
}&&( )
}(( 
})) �
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Facturacion\DetalleOrdenAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Facturacion1 <
{		 
public

 

class

  
DetalleOrdenAtencion

 %
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
int

IdConcepto
{
get
;
set
;
}
public 
int 
	IdFamilia 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
NombreConcepto $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
PrecioUnitario %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
IdEstadoActual !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool 
EstaAnulado 
{  !
get" %
=>& (
IdEstadoActual) 7
==8 :
MaestroSettings; J
.J K
DefaultK R
.R S)
IdDetalleMaestroEstadoAnuladoS p
;p q
}r s
public  
DetalleOrdenAtencion #
(# $
)$ %
{& '
}( )
} 
} �>
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Facturacion\Envios.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{ 
public

 

class

 
LogEnvio

 
{ 
public 
List 
< 
	ItemEnvio 
> 
Exito $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
List
<
string
>
LogError
{
get
;
set
;
}
public 
List 
< 
string 
> 
Error !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string "
MensajeNoHayDocumentos ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
bool 
NoHayDocumentos #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
EstadoLogEnvio 
Estado $
($ %
)% &
{ 	
return 
NoHayDocumentos "
?# $
EstadoLogEnvio% 3
.3 4
NoHay4 9
:: ;
(< =
Exito= B
.B C
CountC H
>I J
$numK L
&&M O
ErrorP U
.U V
CountV [
>\ ]
$num^ _
)_ `
?a b
EstadoLogEnvioc q
.q r
Ambosr w
:x y
Errorz 
.	 �
Count
� �
>
� �
$num
� �
?
� �
EstadoLogEnvio
� �
.
� �
Error
� �
:
� �
EstadoLogEnvio
� �
.
� �
Exito
� �
;
� �
} 	
public 
void 
LogNoHayDocumentos &
(& '
bool' +
noHayDocumentos, ;
,; <
string= C"
mensajeNoHayDocumentosD Z
)Z [
{ 	
NoHayDocumentos 
= 
noHayDocumentos -
;- ."
MensajeNoHayDocumentos "
=# $"
mensajeNoHayDocumentos% ;
;; <
} 	
public 
LogEnvio 
( 
) 
{ 	
Exito 
= 
new 
List 
< 
	ItemEnvio &
>& '
(' (
)( )
;) *
LogError 
= 
new 
List 
<  
string  &
>& '
(' (
)( )
;) *
Error 
= 
new 
List 
< 
string #
># $
($ %
)% &
;& '
} 	
}   
public"" 

class"" *
LogEnvioFacturacionElectronica"" /
{## 
public$$ 
LogEnvio$$ 
Factura$$ 
{$$  !
get$$" %
;$$% &
set$$' *
;$$* +
}$$, -
public%% 
LogEnvio%% 
BoletasVenta%% $
{%%% &
get%%' *
;%%* +
set%%, /
;%%/ 0
}%%1 2
public&& 
LogEnvio&& 
NotaCredito&& #
{&&$ %
get&&& )
;&&) *
set&&+ .
;&&. /
}&&0 1
public'' 
LogEnvio'' 

NotaDebito'' "
{''# $
get''% (
;''( )
set''* -
;''- .
}''/ 0
public(( 
LogEnvio(( 
GuiaRemision(( $
{((% &
get((' *
;((* +
set((, /
;((/ 0
}((1 2
public)) 
LogEnvio)) 
ComunicacionBaja)) (
{))) *
get))+ .
;)). /
set))0 3
;))3 4
}))5 6
public++ *
LogEnvioFacturacionElectronica++ -
(++- .
)++. /
{,, 	
Factura-- 
=-- 
new-- 
LogEnvio-- "
(--" #
)--# $
;--$ %
BoletasVenta.. 
=.. 
new.. 
LogEnvio.. '
(..' (
)..( )
;..) *
NotaCredito// 
=// 
new// 
LogEnvio// &
(//& '
)//' (
;//( )

NotaDebito00 
=00 
new00 
LogEnvio00 %
(00% &
)00& '
;00' (
GuiaRemision11 
=11 
new11 
LogEnvio11 '
(11' (
)11( )
;11) *
}22 	
public44 
string44 
CodigoEstadoEnvio44 '
(44' (
)44( )
{55 	
return66 
(66 
(66 
int66 
)66 
Factura66  
.66  !
Estado66! '
(66' (
)66( )
)66) *
.66* +
ToString66+ 3
(663 4
)664 5
+666 7
(668 9
(669 :
int66: =
)66= >
BoletasVenta66> J
.66J K
Estado66K Q
(66Q R
)66R S
)66S T
.66T U
ToString66U ]
(66] ^
)66^ _
+66` a
(66b c
(66c d
int66d g
)66g h
NotaCredito66h s
.66s t
Estado66t z
(66z {
)66{ |
)66| }
.66} ~
ToString	66~ �
(
66� �
)
66� �
+
66� �
(
66� �
(
66� �
int
66� �
)
66� �

NotaDebito
66� �
.
66� �
Estado
66� �
(
66� �
)
66� �
)
66� �
.
66� �
ToString
66� �
(
66� �
)
66� �
+
66� �
(
66� �
(
66� �
int
66� �
)
66� �
GuiaRemision
66� �
.
66� �
Estado
66� �
(
66� �
)
66� �
)
66� �
.
66� �
ToString
66� �
(
66� �
)
66� �
;
66� �
}77 	
}88 
public:: 

class:: 
	ItemEnvio:: 
{;; 
public<< 
	ModoEnvio<< 
	ModoEnvio<< "
{<<# $
get<<% (
;<<( )
set<<* -
;<<- .
}<</ 0
public== 
string== 
Mensaje== 
{== 
get==  #
;==# $
set==% (
;==( )
}==* +
public?? 
	ItemEnvio?? 
(?? 
)?? 
{@@ 	
}BB 	
publicII 
staticII 
	ItemEnvioII 
ItemAdicionadoII  .
(II. /
stringII/ 5
mensajeII6 =
)II= >
{JJ 	
returnKK 
newKK 
	ItemEnvioKK  
(KK  !
)KK! "
{LL 
	ModoEnvioMM 
=MM 
	ModoEnvioMM %
.MM% &
AdicionMM& -
,MM- .
MensajeNN 
=NN 
mensajeNN !
}OO 
;OO
}PP 	
publicVV 
staticVV 
	ItemEnvioVV 
ItemAnuladoVV  +
(VV+ ,
stringVV, 2
mensajeVV3 :
)VV: ;
{WW 	
returnXX 
newXX 
	ItemEnvioXX  
(XX  !
)XX! "
{YY 
	ModoEnvioZZ 
=ZZ 
	ModoEnvioZZ %
.ZZ% &
	AnulacionZZ& /
,ZZ/ 0
Mensaje[[ 
=[[ 
mensaje[[ !
}\\ 
;\\
}]] 	
}^^ 
}__ �
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Facturacion\OrdenAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Facturacion1 <
{ 
public		 

class		 

{

 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public
IEnumerable
<
DetalleOrdenAtencion
>
Detalles
{
get
;
set
;
}
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
FechaRegistroString )
{* +
get, /
=>0 2

.@ A
ToStringA I
(I J
$strJ V
)V W
;W X
}Y Z
public 
string #
FechaHoraRegistroString -
{. /
get0 3
=>4 6

.D E
ToStringE M
(M N
$strN c
)c d
;d e
}f g
public 
bool 
TieneFacturacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 

( 
) 
{  
}! "
} 
} �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\DetalleCuotaPago.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public 

class 
DetalleCuotaPago !
{		 
public

 
long

 
Id

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 
long 
IdOperacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public
decimal
Total
{
get
;
set
;
}
public 
decimal 
Pagado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Revocado 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Saldo 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} �	
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\PlainModel\FlujoIngresoEgreso.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Finanza1 8
.8 9

PlainModel9 C
{ 
public

 

class

 
FlujoIngresoEgreso

 #
{ 
public 
ResumenFlujo 
Resumen #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public
List
<
DetalleFlujo
>
Detalles
{
get
;
set
;
}
public 
List 
< 
FlujoIngresoEgreso &
>& '
Convert( /
(/ 0
)0 1
{ 	
return 
new 
List 
< 
FlujoIngresoEgreso .
>. /
(/ 0
)0 1
;1 2
} 	
} 
} �
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\PlainModel\DetalleFlujo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Finanza1 8
.8 9

PlainModel9 C
{ 
public		 

class		 
DetalleFlujo		 
{

 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
IdOperacion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public
string
	Operacion
{
get
;
set
;
}
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
bool 
	EsIngreso 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Saldo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
List 
< 
DetalleFlujo  
>  !
Convert" )
() *
)* +
{ 	
return 
new 
List 
< 
DetalleFlujo (
>( )
() *
)* +
;+ ,
} 	
} 
} �
jD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\PlainModel\OperacionFamiliaGrupo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Finanza1 8
.8 9

PlainModel9 C
{		 
public

 

class

 !
OperacionFamiliaGrupo

 &
{ 
public 
int 
IdTipoTransaccion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
int
	IdFamilia
{
get
;
set
;
}
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
int 
IdGrupo 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
NombreGrupo !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
ImporteTotal #
{$ %
get& )
=>* ,
Diccionario- 8
.8 9M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_9 z
.z {
Contains	{ �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
Importe
� �
*
� �
$num
� �
:
� �
Importe
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
List 
< !
OperacionFamiliaGrupo )
>) *
Convert+ 2
(2 3
)3 4
{ 	
return 
new 
List 
< !
OperacionFamiliaGrupo 1
>1 2
(2 3
)3 4
;4 5
} 	
} 
} �'
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\PlainModel\OperacionGrupoDetallado.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Finanza1 8
.8 9

PlainModel9 C
{		 
public

 

class

 #
OperacionGrupoDetallado

 (
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
int
IdTipoTransaccion
{
get
;
set
;
}
public 
string 
NombreResponsable '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
DocumentoCliente &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
long 

{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
DateTime 
Emision 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
ImporteTotal #
{$ %
get& )
=>* ,
Diccionario- 8
.8 9M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_9 z
.z {
Contains	{ �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
Importe
� �
*
� �
$num
� �
:
� �
Importe
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
decimal 
Revocado 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 

{% &
get' *
=>+ -
Diccionario. 9
.9 :M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_: {
.{ |
Contains	| �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
Revocado
� �
*
� �
$num
� �
:
� �
Revocado
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
decimal 
ACuenta 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
ACuentaTotal #
{$ %
get& )
=>* ,
Diccionario- 8
.8 9M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_9 z
.z {
Contains	{ �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
ACuenta
� �
*
� �
$num
� �
:
� �
ACuenta
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
decimal 
Saldo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 

SaldoTotal !
{" #
get$ '
=>( *
Diccionario+ 6
.6 7M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_7 x
.x y
Contains	y �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
Saldo
� �
*
� �
$num
� �
:
� �
Saldo
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
List 
< #
OperacionGrupoDetallado +
>+ ,
Convert- 4
(4 5
)5 6
{ 	
return   
new   
List   
<   #
OperacionGrupoDetallado   3
>  3 4
(  4 5
)  5 6
;  6 7
}!! 	
}## 
}%% �(
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\PlainModel\OperacionGrupo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Finanza1 8
.8 9

PlainModel9 C
{		 
public

 

class

 
OperacionGrupo

 
{ 
public 
int 
IdTipoTransaccion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
int
IdGrupo
{
get
;
set
;
}
public 
string 
NombreGrupo !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string  
DocumentoResponsable *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 
NombreResponsable '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
int 
	IdCliente 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
long 

{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
InfoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
int 
NumeroOperaciones $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
ImporteTotal #
{$ %
get& )
=>* ,
Diccionario- 8
.8 9M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_9 z
.z {
Contains	{ �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
Importe
� �
*
� �
$num
� �
:
� �
Importe
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
decimal 
Revocado 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 

{% &
get' *
=>+ -
Diccionario. 9
.9 :M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_: {
.{ |
Contains	| �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
Revocado
� �
*
� �
$num
� �
:
� �
Revocado
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
decimal 
ACuenta 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
ACuentaTotal #
{$ %
get& )
=>* ,
Diccionario- 8
.8 9M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_9 z
.z {
Contains	{ �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
ACuenta
� �
*
� �
$num
� �
:
� �
ACuenta
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
decimal 
Saldo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 

SaldoTotal !
{" #
get$ '
=>( *
Diccionario+ 6
.6 7M
ATiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_7 x
.x y
Contains	y �
(
� �
IdTipoTransaccion
� �
)
� �
?
� �
Saldo
� �
*
� �
$num
� �
:
� �
Saldo
� �
*
� �
-
� �
$num
� �
;
� �
}
� �
public 
List 
< 
OperacionGrupo "
>" #
Convert$ +
(+ ,
), -
{   	
return!! 
new!! 
List!! 
<!! 
OperacionGrupo!! *
>!!* +
(!!+ ,
)!!, -
;!!- .
}"" 	
}$$ 
}&& �
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\PlainModel\ResumenFlujo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Finanza1 8
.8 9

PlainModel9 C
{ 
public		 

class		 
ResumenFlujo		 
{

 
public 
decimal 
SaldoInicial #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
Ingresos 
{  !
get" %
;% &
set' *
;* +
}, -
public
decimal
Egresos
{
get
;
set
;
}
public 
decimal 

SaldoFinal !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
List 
< 
ResumenFlujo  
>  !
Convert" )
() *
)* +
{ 	
return 
new 
List 
< 
ResumenFlujo (
>( )
() *
)* +
;+ ,
} 	
} 
} �
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\PlainModel\IngresoEgreso.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Finanza1 8
.8 9

PlainModel9 C
{ 
public		 

class		 

{

 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
IdOperacion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public
string
	Operacion
{
get
;
set
;
}
public 
int 
IdMedioPago 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
	MedioPago 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
Informacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 

>! "
Convert# *
(* +
)+ ,
{ 	
return 
new 
List 
< 

>) *
(* +
)+ ,
;, -
} 	
} 
} �
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\ReportData\PrincipalReportData.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Negocio		 %
.		% &
Finanza		& -
.		- .
Report		. 4
{

 
public 

class 
PrincipalReportData $
{ 
public
List
<
Establecimiento
>
Establecimientos
{
get
;
set
;
}
public 
Establecimiento !
EstablecimientoSesion 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
List 
< 
ItemGenerico  
>  !
Cajas" '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
ItemGenerico 

CajaSesion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
List 
< 
ItemGenerico  
>  !

MediosPago" ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
List 
< 
ItemGenerico  
>  !
MediosPagoCuenta" 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
List 
< 
ItemGenerico  
>  !
OperacionesIngresos" 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
List 
< 
ItemGenerico  
>  !
OperacionesEgresos" 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
DateTime 
FechaActual_ $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
long 
FechaActual 
{  !
get" %
{& '
return( .
FechaActual_/ ;
.; <$
ToJavaScriptMilliseconds< T
(T U
)U V
;V W
}X Y
}Z [
public 
long 
FechaHastaDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
long 
FechaDesdeDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
string 
FechaDesdeString &
{' (
get) ,
{- .
return/ 5
FechaActual_6 B
.B C
ToStringC K
(K L
$strL d
)d e
;e f
}g h
}i j
public 
string 
FechaHastaString &
{' (
get) ,
{- .
return/ 5
FechaActual_6 B
.B C
ToStringC K
(K L
$strL d
)d e
;e f
}g h
}i j
public 
bool 
EsAdministrador #
{$ %
get& )
;) *
set+ .
;. /
}0 1
}   
}!! �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Generico\CajaInicializar.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,
Core		, 0
.		0 1
Generico		1 9
{

 
public 

class 
CajaInicializar  
{
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Establecimiento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Caja 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Monto 
{ 
get "
;" #
set$ '
;' (
}) *
public 
CajaInicializar 
( 
)  
{ 	
} 	
public 
static 
List 
< 
CajaInicializar *
>* +
Convert, 3
(3 4
List4 8
<8 9
CentroDeAtencion9 I
>I J
cajasK P
)P Q
{ 	
List 
< 
CajaInicializar  
>  !
cajasInicializar" 2
=3 4
new5 8
List9 =
<= >
CajaInicializar> M
>M N
(N O
)O P
;P Q
foreach 
( 
var 
caja 
in  
cajas! &
)& '
{ 
cajasInicializar  
.  !
Add! $
($ %
new% (
CajaInicializar) 8
(8 9
)9 :
{ 
Id 
= 
caja 
. 
Id  
,  !
Establecimiento   #
=  $ %
caja  & *
.  * +$
EstablecimientoComercial  + C
.  C D
NombreComercial  D S
,  S T
Caja!! 
=!! 
caja!! 
.!!  
Nombre!!  &
,!!& '
Monto"" 
="" 
$num"" 
}## 
)## 
;## 
}$$ 
return%% 
cajasInicializar%% #
;%%# $
}&& 	
}'' 
}(( �	
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Generico\SerieComprobante_.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Generico1 9
{		 
public

 

class

 
SerieComprobante_

 "
:

# $
ItemGenerico

% 1
{ 
public 
bool 
EsAutonumerica "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
SerieComprobante_  
(  !
)! "
{ 	
} 	
public 
SerieComprobante_  
(  !
int! $
id% '
,' (
string) /
nombre0 6
,6 7
bool8 <
esAutonumerica= K
)K L
:M N
baseO S
(S T
idT V
,V W
nombreX ^
)^ _
{ 	
this 
. 
EsAutonumerica 
=  !
esAutonumerica" 0
;0 1
} 	
} 
} �

bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Generico\ItemGenericoConSubItems.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Generico1 9
{ 
public 

class #
ItemGenericoConSubItems (
:( )
ItemGenerico* 6
{ 
public 
List 
< 
ItemGenerico  
>  !
SubItems" *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public		 #
ItemGenericoConSubItems		 &
(		& '
)		' (
{

 	
} 	
public #
ItemGenericoConSubItems &
(& '
int' *
id+ -
,- .
string/ 5
codigo6 <
,< =
string> D
nombreE K
,K L
stringM S
valorT Y
)Y Z
{ 	
Id 
= 
id 
; 
Codigo 
= 
codigo 
; 
Nombre 
= 
nombre 
; 
Valor 
= 
valor 
; 
} 	
} 
} �
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Generico\ItemEstado.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Generico1 9
{		 
public

 

class

 

ItemEstado

 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
string
Nombre
{
get
;
set
;
}
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
FechaString !
{" #
get$ '
=>( *
Fecha+ 0
.0 1
ToString1 9
(9 :
$str: O
)O P
;P Q
}R S
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Valor 
{ 
get !
=>" $
Id% '
==( *
$num+ ,
?- .
$str/ 8
:9 :
DiccionarioHotel; K
.K L
ValorEstadoL W
.W X
SingleX ^
(^ _
ve_ a
=>b d
vee g
.g h
Keyh k
==l n
Ido q
)q r
.r s
Values x
;x y
}z {
public 

ItemEstado 
( 
) 
{ 
} 
public 

ItemEstado 
( 
Estado_transaccion ,
estado_Transaccion- ?
)? @
{ 	
Id 
= 
estado_Transaccion #
.# $
	id_estado$ -
;- .
Nombre 
= 
estado_Transaccion '
.' (
Detalle_maestro( 7
.7 8
nombre8 >
;> ?
Fecha 
= 
estado_Transaccion &
.& '
fecha' ,
;, -
Observacion 
= 
estado_Transaccion ,
., -

comentario- 7
;7 8
} 	
public 

ItemEstado 
( 
Evento_transaccion ,
evento_transaccion- ?
)? @
{ 	
Id 
= 
evento_transaccion #
.# $
	id_evento$ -
;- .
Nombre 
= 
evento_transaccion '
.' (
Detalle_maestro( 7
.7 8
nombre8 >
;> ?
Fecha 
= 
evento_transaccion &
.& '
fecha' ,
;, -
Observacion   
=   
evento_transaccion   ,
.  , -

comentario  - 7
;  7 8
}!! 	
}"" 
}## �O
yD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Adsoft\VentaClienteAdsoft.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{		 
public

 

class

 
VentaClienteAdsoft

 #
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
DateTime
FechaEmision
{
get
;
set
;
}
public 
string 
CodigoComprobante '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string "
NombreCortoComprobante ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
int 
? 
IdSerie 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
NumeroSerie !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
NumeroComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int !
IdActorNegocioExterno (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
int 
IdTipoDocumento "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
CodigoDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
NumeroDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
PrimerNombre "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int 
IdTipoTransaccion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int 
IdTipoComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int 
IdEstadoActual !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
? $
IdEstadoAnteriorAlActual ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
int 

{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
NumeroFinal 
{  
get! $
;$ %
set& )
;) *
}+ ,
public   
string   
CodigoMoneda   "
{  # $
get  % (
;  ( )
set  * -
;  - .
}  / 0
public"" 
string"" !
NumeroSerieReferencia"" +
{"", -
get"". 1
;""1 2
set""3 6
;""6 7
}""8 9
public## 
int## '
NumeroComprobanteReferencia## .
{##/ 0
get##1 4
;##4 5
set##6 9
;##9 :
}##; <
public$$ 
DateTime$$ "
FechaEmisionReferencia$$ .
{$$/ 0
get$$1 4
;$$4 5
set$$6 9
;$$9 :
}$$; <
public%% 
string%% '
CodigoComprobanteReferencia%% 1
{%%2 3
get%%4 7
;%%7 8
set%%9 <
;%%< =
}%%> ?
public'' 
int'' 
Anyo'' 
{'' 
get'' 
;'' 
set'' "
;''" #
}''$ %
public(( 
int(( 
Mes(( 
{(( 
get(( 
;(( 
set(( !
;((! "
}((# $
public)) 
int)) 
Dia)) 
{)) 
get)) 
;)) 
set)) !
;))! "
}))# $
public++ 
int++ 
Signo++ 
{++ 
get++ 
=>++ !
this++" &
.++& '
IdTipoComprobante++' 8
==++9 ;
MaestroSettings++< K
.++K L
Default++L S
.++S T2
&IdDetalleMaestroComprobanteNotaCredito++T z
?++{ |
-++} ~
$num++~ 
:
++� �
$num
++� �
;
++� �
}
++� �
public,, 
decimal,, 
Total,, 
{,, 
get,, "
;,," #
set,,$ '
;,,' (
},,) *
public-- 
decimal-- 
ImporteTotal-- #
{--$ %
get--& )
=>--* ,
Total--- 2
*--3 4
Signo--5 :
;--: ;
}--< =
public.. 
decimal.. 
?.. 
ValorIgv..  
{..! "
get..# &
;..& '
set..( +
;..+ ,
}..- .
public// 
decimal// 
Igv// 
{// 
get//  
=>//! #
ValorIgv//$ ,
!=//- /
null//0 4
?//5 6
(//7 8
decimal//8 ?
)//? @
ValorIgv//@ H
*//I J
Signo//K P
://Q R
$num//S T
;//T U
}//V W
public00 
decimal00 
ValorIcbper00 "
{00# $
get00% (
;00( )
set00* -
;00- .
}00/ 0
public11 
decimal11 
Icbper11 
{11 
get11  #
=>11$ &
ValorIcbper11' 2
*113 4
Signo115 :
;11: ;
}11< =
public22 
decimal22 

ValorVenta22 !
{22" #
get22$ '
;22' (
set22) ,
;22, -
}22. /
public33 
decimal33 
ValorDeVenta33 #
{33$ %
get33& )
=>33* ,

ValorVenta33- 7
*338 9
Signo33: ?
;33? @
}33A B
public55 
bool55 
EsInvalidada55  
{66 	
get77 
{88 
return99 
(99 
this99 
.99 
IdEstadoActual99 +
==99, .
MaestroSettings99/ >
.99> ?
Default99? F
.99F G,
 IdDetalleMaestroEstadoInvalidado99G g
||99h j
this99k o
.99o p%
IdEstadoAnteriorAlActual	99p �
==
99� �
MaestroSettings
99� �
.
99� �
Default
99� �
.
99� �.
 IdDetalleMaestroEstadoInvalidado
99� �
)
99� �
;
99� �
}:: 
};; 	
public== 
bool== 6
*ElComprobanteOriginalEsDeUnperiodoAnterior== >
(==> ?
)==? @
{>> 	
return?? 
(?? 
this?? 
.?? "
FechaEmisionReferencia?? /
.??/ 0
Year??0 4
<=??5 7
this??8 <
.??< =
FechaEmision??= I
.??I J
Year??J N
&&??O Q
this??R V
.??V W"
FechaEmisionReferencia??W m
.??m n
Month??n s
<??t u
this??v z
.??z {
FechaEmision	??{ �
.
??� �
Month
??� �
)
??� �
;
??� �
}@@ 	
publicBB 
decimalBB '
ImporteTotalComprobantePagoBB 2
{CC 	
getDD 
{EE 
returnFF 
ImporteTotalFF #
;FF# $
}GG 
}HH 	
publicJJ 
decimalJJ 1
%BaseImponibleOperacionGravadaConSignoJJ <
{KK 	
getLL 
{MM 
returnNN 
MathNN 
.NN 
AbsNN 
(NN  
(NN  !
decimalNN! (
)NN( )
IgvNN) ,
)NN, -
>NN. /
$numNN0 1
?NN2 3
ValorDeVentaNN4 @
:NNA B
$numNNC D
;NND E
}OO 
}PP 	
publicRR 
decimalRR =
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipalRR H
{SS 	
getTT 
{UU 
returnVV 
MathVV 
.VV 
AbsVV 
(VV  
(VV  !
decimalVV! (
)VV( )
IgvVV) ,
)VV, -
>VV. /
$numVV0 1
?VV2 3
(VV4 5
decimalVV5 <
)VV< =
IgvVV= @
:VVA B
$numVVC D
;VVD E
}WW 
}XX 	
publicZZ 
decimalZZ 2
&ImporteTotalOperacionExoneradaConSignoZZ =
{[[ 	
get\\ 
{]] 
return^^ 
Math^^ 
.^^ 
Abs^^ 
(^^  
(^^  !
decimal^^! (
)^^( )
Igv^^) ,
)^^, -
>^^. /
$num^^0 1
?^^2 3
$num^^4 5
:^^6 7
ValorDeVenta^^8 D
;^^D E
}__ 
}`` 	
publiccc 
intcc 
TipoAgrupamientocc #
{dd 	
getee 
{ee 
returnee 
EsInvalidadaee %
?ee& '
$numee( )
:ee* +!
IdActorNegocioExternoee, A
!=eeB D

.eeR S
DefaulteeS Z
.eeZ [
IdClienteGenericoee[ l
?eem n
$numeeo p
:eeq r
$numees t
;eet u
}eev w
}ff 	
}gg 
}hh �n
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Adsoft\ReporteVentaClienteAdsoft.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{		 
public

 

class

 %
ReporteVentaClienteAdsoft

 *
{ 
public 
string 
NumeroSerie !
{" #
get$ '
;' (
set) ,
;, -
}. /
public
string
NumeroComprobante
{
get
;
set
;
}
public 
string 
Fecha 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
	NumeroRUC 
{  !
get" %
;% &
set' *
;* +
}, -
public 
long 
	IdCliente 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
RazonSocial !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
CodigoComprobante '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 

TipoMoneda  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
decimal 

Detraccion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
ImporteVenta #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal &
ImpuestoSelectivoAlConsumo 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
decimal 
Icbper 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal  
ImporteTotalInafecta +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
decimal !
ImporteTotalExonerada ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
decimal #
ImporteTotalExportacion .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
decimal 
Recargo 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 

ImporteIGV !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
ImporteTotal #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public   
string   
Estado   
{   
get   "
;  " #
set  $ '
;  ' (
}  ) *
public"" 
string"" 
SerDqm"" 
{"" 
get"" "
;""" #
set""$ '
;""' (
}"") *
public## 
string## 
NumDqm## 
{## 
get## "
;##" #
set##$ '
;##' (
}##) *
public$$ 
string$$ 
FecDqm$$ 
{$$ 
get$$ "
;$$" #
set$$$ '
;$$' (
}$$) *
public%% 
string%% 
TipDqm%% 
{%% 
get%% "
;%%" #
set%%$ '
;%%' (
}%%) *
public&& 
string&& 
SerieFin&& 
{&&  
get&&! $
;&&$ %
set&&& )
;&&) *
}&&+ ,
public'' 
string'' 
	NumeroFin'' 
{''  !
get''" %
;''% &
set''' *
;''* +
}'', -
public(( 
string(( 
	NumeroDNI(( 
{((  !
get((" %
;((% &
set((' *
;((* +
}((, -
public)) 
string)) 
	Pasaporte)) 
{))  !
get))" %
;))% &
set))' *
;))* +
})), -
public** 
string** 

{**$ %
get**& )
;**) *
set**+ .
;**. /
}**0 1
public++ 
decimal++ 

TipoCambio++ !
{++" #
get++$ '
;++' (
set++) ,
;++, -
}++. /
public-- %
ReporteVentaClienteAdsoft-- (
(--( )
)--) *
{.. 	
}..
 
public00 %
ReporteVentaClienteAdsoft00 (
(00( )
VentaClienteAdsoft00) ;
venta00< A
)00A B
{11 	
this33 
.33 
NumeroSerie33 
=33 
venta33 $
.33$ %
NumeroSerie33% 0
;330 1
this44 
.44 
NumeroComprobante44 "
=44# $
venta44% *
.44* +
NumeroComprobante44+ <
>44= >
$num44? @
?44A B
venta44C H
.44H I
NumeroComprobante44I Z
.44Z [
ToString44[ c
(44c d
)44d e
:44f g
venta44h m
.44m n

.44{ |
ToString	44| �
(
44� �
)
44� �
;
44� �
this55 
.55 
Fecha55 
=55 
venta55 
.55 
FechaEmision55 +
.55+ ,
ToString55, 4
(554 5
$str555 A
)55A B
;55B C
this66 
.66 
	NumeroRUC66 
=66 

.66* +
Default66+ 2
.662 3'
IdTipoDocumentoIdentidadRuc663 N
==66O Q
venta66R W
.66W X
IdTipoDocumento66X g
?66h i
venta66j o
.66o p
NumeroDocumento66p 
:
66� �
$str
66� �
;
66� �
this77 
.77 
RazonSocial77 
=77 
venta77 $
.77$ %
EsInvalidada77% 1
?772 3
$str774 =
:77> ?
venta77@ E
.77E F
PrimerNombre77F R
.77R S
Replace77S Z
(77Z [
$str77[ ^
,77^ _
$str77` c
)77c d
;77d e
this88 
.88 
CodigoComprobante88 "
=88# $
venta88% *
.88* +
CodigoComprobante88+ <
;88< =
this99 
.99 

TipoMoneda99 
=99 
venta99 #
.99# $
CodigoMoneda99$ 0
;990 1
this;; 
.;; 

Detraccion;; 
=;; 
$num;; 
;;;  
this<< 
.<< 
ImporteVenta<< 
=<< 
venta<<  %
.<<% &
EsInvalidada<<& 2
?<<3 4
$num<<5 6
:<<7 8
venta<<9 >
.<<> ?=
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipal<<? p
><<q r
$num<<s t
?<<u v
venta<<w |
.<<| }2
%BaseImponibleOperacionGravadaConSigno	<<} �
:
<<� �
(
<<� �
venta
<<� �
.
<<� �)
ImporteTotalComprobantePago
<<� �
-
<<� �
venta
<<� �
.
<<� �
Icbper
<<� �
)
<<� �
;
<<� �
this>> 
.>> &
ImpuestoSelectivoAlConsumo>> +
=>>, -
$num>>. /
;>>/ 0
this?? 
.??  
ImporteTotalInafecta?? %
=??& '
$num??( )
;??) *
this@@ 
.@@ !
ImporteTotalExonerada@@ &
=@@' (
venta@@) .
.@@. /
EsInvalidada@@/ ;
?@@< =
$num@@> ?
:@@@ A
venta@@B G
.@@G H2
&ImporteTotalOperacionExoneradaConSigno@@H n
;@@n o
thisAA 
.AA #
ImporteTotalExportacionAA (
=AA) *
$numAA+ ,
;AA, -
thisCC 
.CC 
RecargoCC 
=CC 
$numCC 
;CC 
thisDD 
.DD 

ImporteIGVDD 
=DD 
ventaDD #
.DD# $
EsInvalidadaDD$ 0
?DD1 2
$numDD3 4
:DD5 6
ventaDD7 <
.DD< ==
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipalDD= n
;DDn o
thisEE 
.EE 
IcbperEE 
=EE 
ventaEE 
.EE  
EsInvalidadaEE  ,
?EE- .
$numEE/ 0
:EE1 2
ventaEE3 8
.EE8 9
IcbperEE9 ?
;EE? @
thisFF 
.FF 
ImporteTotalFF 
=FF 
ventaFF  %
.FF% &
EsInvalidadaFF& 2
?FF3 4
$numFF5 6
:FF7 8
ventaFF9 >
.FF> ?'
ImporteTotalComprobantePagoFF? Z
;FFZ [
thisGG 
.GG 
EstadoGG 
=GG 
ventaGG 
.GG  
EsInvalidadaGG  ,
?GG- .
$strGG/ 2
:GG3 4
$strGG5 7
;GG7 8
ifII 
(II 
ventaII 
.II 
IdTipoComprobanteII '
==II( *
MaestroSettingsII+ :
.II: ;
DefaultII; B
.IIB C2
&IdDetalleMaestroComprobanteNotaCreditoIIC i
||IIj l
ventaIIm r
.IIr s
IdTipoComprobante	IIs �
==
II� �
MaestroSettings
II� �
.
II� �
Default
II� �
.
II� �5
'IdDetalleMaestroComprobanteNotaDeDebito
II� �
)
II� �
{JJ 
thisKK 
.KK 
SerDqmKK 
=KK 
ventaKK #
.KK# $!
NumeroSerieReferenciaKK$ 9
;KK9 :
thisLL 
.LL 
NumDqmLL 
=LL 
ventaLL #
.LL# $'
NumeroComprobanteReferenciaLL$ ?
.LL? @
ToStringLL@ H
(LLH I
)LLI J
;LLJ K
thisMM 
.MM 
FecDqmMM 
=MM 
ventaMM #
.MM# $"
FechaEmisionReferenciaMM$ :
.MM: ;
ToStringMM; C
(MMC D
$strMMD P
)MMP Q
;MMQ R
thisNN 
.NN 
TipDqmNN 
=NN 
ventaNN #
.NN# $'
CodigoComprobanteReferenciaNN$ ?
;NN? @
thisOO 
.OO 
SerieFinOO 
=OO 
ventaOO  %
.OO% &!
NumeroSerieReferenciaOO& ;
;OO; <
}PP 
elseQQ 
{RR 
thisSS 
.SS 
SerDqmSS 
=SS 
$strSS  
;SS  !
thisTT 
.TT 
NumDqmTT 
=TT 
$strTT  
;TT  !
thisUU 
.UU 
FecDqmUU 
=UU 
$strUU  
;UU  !
thisVV 
.VV 
TipDqmVV 
=VV 
$strVV  
;VV  !
thisWW 
.WW 
SerieFinWW 
=WW 
$strWW  "
;WW" #
}XX 
thisZZ 
.ZZ 
	NumeroFinZZ 
=ZZ 
ventaZZ "
.ZZ" #
NumeroFinalZZ# .
>ZZ/ 0
$numZZ1 2
?ZZ3 4
ventaZZ5 :
.ZZ: ;
NumeroFinalZZ; F
.ZZF G
ToStringZZG O
(ZZO P
)ZZP Q
:ZZR S
ventaZZT Y
.ZZY Z
NumeroComprobanteZZZ k
.ZZk l
ToStringZZl t
(ZZt u
)ZZu v
;ZZv w
this[[ 
.[[ 
	NumeroDNI[[ 
=[[ 

.[[* +
Default[[+ 2
.[[2 3'
IdTipoDocumentoIdentidadDni[[3 N
==[[O Q
venta[[R W
.[[W X
IdTipoDocumento[[X g
?[[h i
venta[[j o
.[[o p
NumeroDocumento[[p 
:
[[� �
$str
[[� �
;
[[� �
this\\ 
.\\ 
	Pasaporte\\ 
=\\ 
$str\\ 
;\\  
this]] 
.]] 

=]]  
$str]]! #
;]]# $
this^^ 
.^^ 

TipoCambio^^ 
=^^ 
$num^^ 
;^^  
}__ 	
publicaa 
staticaa 
Listaa 
<aa %
ReporteVentaClienteAdsoftaa 4
>aa4 5
Convertaa6 =
(aa= >
Listaa> B
<aaB C
VentaClienteAdsoftaaC U
>aaU V
ventasaaW ]
)aa] ^
{bb 	
Listcc 
<cc %
ReporteVentaClienteAdsoftcc *
>cc* +
ventasViewModelcc, ;
=cc< =
newcc> A
ListccB F
<ccF G%
ReporteVentaClienteAdsoftccG `
>cc` a
(cca b
)ccb c
;ccc d
foreachdd 
(dd 
vardd 
itemdd 
indd  
ventasdd! '
)dd' (
{ee 
ventasViewModelff 
.ff  
Addff  #
(ff# $
newff$ '%
ReporteVentaClienteAdsoftff( A
(ffA B
itemffB F
)ffF G
)ffG H
;ffH I
}gg 
returnhh 
ventasViewModelhh "
;hh" #
}ii 	
}jj 
}kk �K
uD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Concar\OperacionVenta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{		 
public

 

class

 
OperacionVenta

 
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
DateTime
FechaDocumento
{
get
;
set
;
}
public 
DateTime 
FechaVencimiento (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
int 
IdMoneda 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
IdTipoComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
SerieComprobante &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
int 
NumeroComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int 
	IdCliente 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string "
NumeroDocumentoCliente ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 

TipoCambio !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
? 
	TotalBien !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
? 
IgvBien 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
? 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
? 
IgvServicio #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
Icbper 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
IdTipoTransaccion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string (
NombreTipoTransaccionWrapper 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
int 
IdEstado 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
EstaInvalidado "
{# $
get% (
=>) +
IdEstado, 4
==5 7
MaestroSettings8 G
.G H
DefaultH O
.O P,
 IdDetalleMaestroEstadoInvalidadoP p
;p q
}r s
public   
decimal   
BaseImponibleBien   (
{  ) *
get  + .
=>  / 1
(  2 3
	TotalBien  3 <
==  = ?
null  @ D
?  E F
$num  G H
:  I J
(  K L
decimal  L S
)  S T
	TotalBien  T ]
)  ] ^
-  _ `
(  a b
IgvBien  b i
==  j l
null  m q
?  r s
$num  t u
:  v w
(  x y
decimal	  y �
)
  � �
IgvBien
  � �
)
  � �
;
  � �
}
  � �
public!! 
decimal!! !
BaseImponibleServicio!! ,
{!!- .
get!!/ 2
=>!!3 5
(!!6 7

==!!E G
null!!H L
?!!M N
$num!!O P
:!!Q R
(!!S T
decimal!!T [
)!![ \

)!!i j
-!!k l
(!!m n
IgvServicio!!n y
==!!z |
null	!!} �
?
!!� �
$num
!!� �
:
!!� �
(
!!� �
decimal
!!� �
)
!!� �
IgvServicio
!!� �
)
!!� �
;
!!� �
}
!!� �
public"" 
decimal"" 
BaseImponibleTotal"" )
{""* +
get"", /
=>""0 2
BaseImponibleBien""3 D
+""E F!
BaseImponibleServicio""G \
;""\ ]
}""^ _
public## 
decimal## 
Total## 
{## 
get## "
=>### %
(##& '
	TotalBien##' 0
==##1 3
null##4 8
?##9 :
$num##; <
:##= >
(##? @
decimal##@ G
)##G H
	TotalBien##H Q
)##Q R
+##S T
(##U V

==##d f
null##g k
?##l m
$num##n o
:##p q
(##r s
decimal##s z
)##z {

)
##� �
+
##� �
Icbper
##� �
;
##� �
}
##� �
public$$ 
decimal$$ 
ImporteTotal$$ #
{$$$ %
get$$& )
=>$$* ,
EstaInvalidado$$- ;
?$$< =
$num$$> ?
:$$@ A
Total$$B G
;$$G H
}$$I J
public%% 
decimal%% 
TotalIgv%% 
{%%  !
get%%" %
=>%%& (
(%%) *
IgvBien%%* 1
==%%2 4
null%%5 9
?%%: ;
$num%%< =
:%%> ?
(%%@ A
decimal%%A H
)%%H I
IgvBien%%I P
)%%P Q
+%%R S
(%%T U
IgvServicio%%U `
==%%a c
null%%d h
?%%i j
$num%%k l
:%%m n
(%%o p
decimal%%p w
)%%w x
IgvServicio	%%x �
)
%%� �
;
%%� �
}
%%� �
public'' 
DateTime'' 
FechaEmision'' $
{''% &
get''' *
;''* +
set'', /
;''/ 0
}''1 2
public(( 
decimal(( 
MontoTotalPago(( %
{((& '
get((( +
;((+ ,
set((- 0
;((0 1
}((2 3
public++ 
int++ '
IdTipoComprobanteReferencia++ .
{++/ 0
get++1 4
;++4 5
set++6 9
;++9 :
}++; <
public,, 
string,, &
SerieComprobanteReferencia,, 0
{,,1 2
get,,3 6
;,,6 7
set,,8 ;
;,,; <
},,= >
public-- 
int-- '
NumeroComprobanteReferencia-- .
{--/ 0
get--1 4
;--4 5
set--6 9
;--9 :
}--; <
public.. 
DateTime.. "
FechaEmisionReferencia.. .
{../ 0
get..1 4
;..4 5
set..6 9
;..9 :
}..; <
public// 
DateTime// &
FechaVencimientoReferencia// 2
{//3 4
get//5 8
;//8 9
set//: =
;//= >
}//? @
public00 
string00 2
&NombreTipoTransaccionReferenciaWrapper00 <
{00= >
get00? B
;00B C
set00D G
;00G H
}00I J
public11 
decimal11 "
ImporteTotalReferencia11 -
{11. /
get110 3
;113 4
set115 8
;118 9
}11: ;
public22 
decimal22 

{22% &
get22' *
;22* +
set22, /
;22/ 0
}221 2
public33 
decimal33 #
BaseImponibleReferencia33 .
{33/ 0
get331 4
=>335 7"
ImporteTotalReferencia338 N
-33O P

;33^ _
}33` a
public55 
List55 
<55 
OperacionVenta55 "
>55" #
Convert55$ +
(55+ ,
)55, -
{66 	
return77 
new77 
List77 
<77 
OperacionVenta77 *
>77* +
(77+ ,
)77, -
;77- .
}88 	
}:: 
public;; 

class;; 
OperacionCuotaPago;; #
{<< 
public== 
long== 
IdOperacion== 
{==  !
get==" %
;==% &
set==' *
;==* +
}==, -
public>> 
decimal>> 
	MontoPago>>  
{>>! "
get>># &
;>>& '
set>>( +
;>>+ ,
}>>- .
public?? 
DateTime?? 
FechaEmision?? $
{??% &
get??' *
;??* +
set??, /
;??/ 0
}??1 2
}@@ 
}BB �9
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Concar\DetalleAsientoContableConcar.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{ 
public		 

class		 (
DetalleAsientoContableConcar		 -
{

 
public 
string 
	SubDiario 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
NumeroComprobante '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public
string
FechaComprobante
{
get
;
set
;
}
public 
string 
CodigoMoneda "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
GlosaPrincipal $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 

TipoCambio !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
TipoConversion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string  
FlagConversionMoneda *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 
FechaTipoCambio %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
CuentaContable $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CodigoAnexo !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
CodigoCentroCosto '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
	DebeHaber 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
ImporteOriginal &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
decimal 
ImporteDolares %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
ImporteSoles #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
NumeroDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
FechaDocumento $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
FechaVencimiento &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 

CodigoArea  
{! "
get# &
;& '
set( +
;+ ,
}- .
public   
string   
GlosaDetalle   "
{  # $
get  % (
;  ( )
set  * -
;  - .
}  / 0
public!! 
string!! 
CodigoAnexoAuxiliar!! )
{!!* +
get!!, /
;!!/ 0
set!!1 4
;!!4 5
}!!6 7
public"" 
string"" 
	MedioPago"" 
{""  !
get""" %
;""% &
set""' *
;""* +
}"", -
public## 
string## #
TipoDocumentoReferencia## -
{##. /
get##0 3
;##3 4
set##5 8
;##8 9
}##: ;
public$$ 
string$$ %
NumeroDocumentoReferencia$$ /
{$$0 1
get$$2 5
;$$5 6
set$$7 :
;$$: ;
}$$< =
public%% 
string%% $
FechaDocumentoReferencia%% .
{%%/ 0
get%%1 4
;%%4 5
set%%6 9
;%%9 :
}%%; <
public&& 
string&& (
NroMaqRegistradoraTipoDocRef&& 2
{&&3 4
get&&5 8
;&&8 9
set&&: =
;&&= >
}&&? @
public'' 
decimal'' ,
 BaseImponibleDocumentoReferencia'' 7
{''8 9
get'': =
;''= >
set''? B
;''B C
}''D E
public(( 
decimal(( !
IGVDocumentoProvision(( ,
{((- .
get((/ 2
;((2 3
set((4 7
;((7 8
}((9 :
public)) 
string)) $
TipoReferenciaenestadoMQ)) .
{))/ 0
get))1 4
;))4 5
set))6 9
;))9 :
})); <
public** 
string** '
NumeroSerieCajaRegistradora** 1
{**2 3
get**4 7
;**7 8
set**9 <
;**< =
}**> ?
public++ 
string++ 
FechaOperacion++ $
{++% &
get++' *
;++* +
set++, /
;++/ 0
}++1 2
public,, 
string,, 
TipoTasa,, 
{,,  
get,,! $
;,,$ %
set,,& )
;,,) *
},,+ ,
public-- 
decimal-- $
TasaDetraccionPercepcion-- /
{--0 1
get--2 5
;--5 6
set--7 :
;--: ;
}--< =
public.. 
decimal.. 2
&ImporteBaseDetraccionPercepcionDolares.. =
{..> ?
get..@ C
;..C D
set..E H
;..H I
}..J K
public// 
decimal// 0
$ImporteBaseDetraccionPercepcionSoles// ;
{//< =
get//> A
;//A B
set//C F
;//F G
}//H I
public00 
string00 
TipoCambioparaF00 %
{00& '
get00( +
;00+ ,
set00- 0
;000 1
}002 3
public11 
decimal11 -
!ImporteIGVSinDerechoCreditoFiscal11 8
{119 :
get11; >
;11> ?
set11@ C
;11C D
}11E F
public33 
List33 
<33 (
DetalleAsientoContableConcar33 0
>330 1
Convert332 9
(339 :
)33: ;
{44 	
return55 
new55 
List55 
<55 (
DetalleAsientoContableConcar55 8
>558 9
(559 :
)55: ;
;55; <
}66 	
}77 
}88 �
vD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Concar\RegistroCliente.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{ 
public		 

class		 
RegistroCliente		  
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
NumeroDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public
string
Nombre
{
get
;
set
;
}
public 
string 
	Direccion 
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} �
}D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Concar\LibroElectronicoConcar.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{ 
public		 

class		 "
LibroElectronicoConcar		 '
{

 
public 
List 
< (
DetalleAsientoContableConcar 0
>0 1
RegistroCobranzas2 C
{D E
getF I
;I J
setK N
;N O
}P Q
public 
List 
< (
DetalleAsientoContableConcar 0
>0 1
RegistroVentas2 @
{A B
getC F
;F G
setH K
;K L
}M N
public
List
<
DetalleAsientoContableConcar
>

{
get
;
set
;
}
public 
List 
< !
RegistroClienteConcar )
>) *
RegistroClientes+ ;
{< =
get> A
;A B
setC F
;F G
}H I
} 
} �
|D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Concar\RegistroClienteConcar.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{ 
public		 

class		 !
RegistroClienteConcar		 &
{

 
public 
string 
Avanexo 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Acodane 
{ 
get  #
;# $
set% (
;( )
}* +
public
string
Adesane
{
get
;
set
;
}
public 
string 
Arefane 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Aruc 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Acodmon 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Aestado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Adate 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Ahora 
{ 
get !
;! "
set# &
;& '
}( )
public 
List 
< !
RegistroClienteConcar )
>) *
Convert+ 2
(2 3
)3 4
{ 	
return 
new 
List 
< !
RegistroClienteConcar 1
>1 2
(2 3
)3 4
;4 5
} 	
} 
} �7
zD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Foxcont\VentaClienteFoxcom.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{		 
public

 

class

 
VentaClienteFoxcom

 #
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
DateTime
FechaEmision
{
get
;
set
;
}
public 
string 
CodigoComprobante '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
NumeroSerie !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
NumeroComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int !
IdActorNegocioExterno (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
CodigoDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
NumeroDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
RazonSocial !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
IdTipoComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int 
IdEstadoActual !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string '
CodigoComprobanteReferencia 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string !
NumeroSerieReferencia +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
int '
NumeroComprobanteReferencia .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
DateTime "
FechaEmisionReferencia .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
int 
Signo 
{ 
get 
=> !
this" &
.& '
IdTipoComprobante' 8
==9 ;
MaestroSettings< K
.K L
DefaultL S
.S T2
&IdDetalleMaestroComprobanteNotaCreditoT z
?{ |
-} ~
$num~ 
:
� �
$num
� �
;
� �
}
� �
public 
bool 
EsNota 
{ 
get  
=>! #
this$ (
.( )
IdTipoComprobante) :
==; =
MaestroSettings> M
.M N
DefaultN U
.U V2
&IdDetalleMaestroComprobanteNotaCreditoV |
||} 
this
� �
.
� �
IdTipoComprobante
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �5
'IdDetalleMaestroComprobanteNotaDeDebito
� �
;
� �
}
� �
public 
bool 
EsInvalidada  
{! "
get# &
=>' )
this* .
.. /
IdEstadoActual/ =
==> @
MaestroSettingsA P
.P Q
DefaultQ X
.X Y,
 IdDetalleMaestroEstadoInvalidadoY y
;y z
}{ |
public 
decimal 
Total 
{ 
get "
;" #
set$ '
;' (
}) *
public   
decimal   
ImporteTotal   #
{  $ %
get  & )
=>  * ,
Total  - 2
*  3 4
Signo  5 :
;  : ;
}  < =
public!! 
decimal!! 
?!! 
Igv!! 
{!! 
get!! !
;!!! "
set!!# &
;!!& '
}!!( )
public"" 
decimal"" 

ImporteIgv"" !
{""" #
get""$ '
=>""( *
Igv""+ .
!=""/ 1
null""2 6
?""7 8
(""9 :
decimal"": A
)""A B
Igv""B E
*""F G
Signo""H M
:""N O
$num""P Q
;""Q R
}""S T
public## 
decimal## 
Icbper## 
{## 
get##  #
;### $
set##% (
;##( )
}##* +
public$$ 
decimal$$ 

{$$% &
get$$' *
=>$$+ -
Icbper$$. 4
*$$5 6
Signo$$7 <
;$$< =
}$$> ?
public%% 
decimal%% 

ValorVenta%% !
{%%" #
get%%$ '
;%%' (
set%%) ,
;%%, -
}%%. /
public&& 
decimal&& 
ImporteValorVenta&& (
{&&) *
get&&+ .
=>&&/ 1

ValorVenta&&2 <
*&&= >
Signo&&? D
;&&D E
}&&F G
public(( 
decimal(( 0
$ImporteBaseImponibleOperacionGravada(( ;
{((< =
get((> A
=>((B D
Math((E I
.((I J
Abs((J M
(((M N

ImporteIgv((N X
)((X Y
>((Z [
$num((\ ]
?((^ _
ImporteValorVenta((` q
:((r s
$num((t u
;((u v
}((w x
public)) 
decimal)) =
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipal)) H
{))I J
get))K N
=>))O Q
Math))R V
.))V W
Abs))W Z
())Z [

ImporteIgv))[ e
)))e f
>))g h
$num))i j
?))k l

ImporteIgv))m w
:))x y
$num))z {
;)){ |
}))} ~
public** 
decimal** *
ImporteTotalOperacionExonerada** 5
{**6 7
get**8 ;
=>**< >
Math**? C
.**C D
Abs**D G
(**G H

ImporteIgv**H R
)**R S
>**T U
$num**V W
?**X Y
$num**Z [
:**\ ]
ImporteValorVenta**^ o
;**o p
}**q r
}++ 
},, �E
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\LibrosElectronicos\PlainModel\Foxcont\ReporteVentaClienteFoxcom.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
LibrosElectronicos1 C
.C D

PlainModelD N
{		 
public

 

class

 %
ReporteVentaClienteFoxcom

 *
{ 
public 
string 
Fecha 
{ 
get !
;! "
set# &
;& '
}( )
public
string
CodigoComprobante
{
get
;
set
;
}
public 
string 
NumeroSerie !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
NumeroComprobante '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
CodigoDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
NumeroDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
RazonSocial !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal  
BaseImponibleGravada +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
decimal 
IGVoIPM 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Icbper 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal !
ImporteTotalExonerada ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
decimal 
	Retencion  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
decimal #
ImporteTotalComprobante .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
string !
NumeroSerieReferencia +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
string '
NumeroComprobanteReferencia 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
FechaEmisionReferencia ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
string '
CodigoComprobanteReferencia 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public %
ReporteVentaClienteFoxcom (
(( )
)) *
{ 	
}
 
public!! %
ReporteVentaClienteFoxcom!! (
(!!( )
VentaClienteFoxcom!!) ;
ventaFoxcont!!< H
)!!H I
{"" 	
this## 
.## 
Fecha## 
=## 
ventaFoxcont## %
.##% &
FechaEmision##& 2
.##2 3
ToString##3 ;
(##; <
$str##< H
)##H I
;##I J
this$$ 
.$$ 
CodigoComprobante$$ "
=$$# $
ventaFoxcont$$% 1
.$$1 2
CodigoComprobante$$2 C
;$$C D
this%% 
.%% 
NumeroSerie%% 
=%% 
ventaFoxcont%% +
.%%+ ,
NumeroSerie%%, 7
;%%7 8
this&& 
.&& 
NumeroComprobante&& "
=&&# $
ventaFoxcont&&% 1
.&&1 2
NumeroComprobante&&2 C
.&&C D
ToString&&D L
(&&L M
)&&M N
.&&N O
PadLeft&&O V
(&&V W
$num&&W X
,&&X Y
$char&&Z ]
)&&] ^
;&&^ _
this'' 
.'' 
CodigoDocumento''  
=''! "
ventaFoxcont''# /
.''/ 0
CodigoDocumento''0 ?
;''? @
this(( 
.(( 
NumeroDocumento((  
=((! "
ventaFoxcont((# /
.((/ 0
NumeroDocumento((0 ?
;((? @
this)) 
.)) 
RazonSocial)) 
=)) 
ventaFoxcont)) +
.))+ ,
RazonSocial)), 7
;))7 8
this++ 
.++  
BaseImponibleGravada++ %
=++& '
ventaFoxcont++( 4
.++4 5
EsInvalidada++5 A
?++B C
$num++D E
:++F G
ventaFoxcont++H T
.++T U0
$ImporteBaseImponibleOperacionGravada++U y
;++y z
this,, 
.,, 
IGVoIPM,, 
=,, 
ventaFoxcont,, '
.,,' (
EsInvalidada,,( 4
?,,5 6
$num,,7 8
:,,9 :
ventaFoxcont,,; G
.,,G H=
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipal,,H y
;,,y z
this-- 
.-- 
Icbper-- 
=-- 
ventaFoxcont-- &
.--& '
EsInvalidada--' 3
?--4 5
$num--6 7
:--8 9
ventaFoxcont--: F
.--F G
Icbper--G M
;--M N
this.. 
... !
ImporteTotalExonerada.. &
=..' (
ventaFoxcont..) 5
...5 6
EsInvalidada..6 B
?..C D
$num..E F
:..G H
ventaFoxcont..I U
...U V*
ImporteTotalOperacionExonerada..V t
;..t u
this// 
.// 
	Retencion// 
=// 
$num// 
;// 
this00 
.00 #
ImporteTotalComprobante00 (
=00) *
ventaFoxcont00+ 7
.007 8
EsInvalidada008 D
?00E F
$num00G H
:00I J
ventaFoxcont00K W
.00W X
ImporteTotal00X d
;00d e
this22 
.22 !
NumeroSerieReferencia22 &
=22' (
ventaFoxcont22) 5
.225 6
EsNota226 <
?22= >
ventaFoxcont22? K
.22K L!
NumeroSerieReferencia22L a
:22b c
$str22d f
;22f g
this33 
.33 '
NumeroComprobanteReferencia33 ,
=33- .
ventaFoxcont33/ ;
.33; <
EsNota33< B
?33C D
ventaFoxcont33E Q
.33Q R'
NumeroComprobanteReferencia33R m
.33m n
ToString33n v
(33v w
)33w x
:33y z
$str33{ }
;33} ~
this44 
.44 "
FechaEmisionReferencia44 '
=44( )
ventaFoxcont44* 6
.446 7
EsNota447 =
?44> ?
ventaFoxcont44@ L
.44L M"
FechaEmisionReferencia44M c
.44c d
ToString44d l
(44l m
$str44m y
)44y z
:44{ |
$str44} 
;	44 �
this55 
.55 '
CodigoComprobanteReferencia55 ,
=55- .
ventaFoxcont55/ ;
.55; <
EsNota55< B
?55C D
ventaFoxcont55E Q
.55Q R'
CodigoComprobanteReferencia55R m
:55n o
$str55p r
;55r s
}66 	
public88 
static88 
List88 
<88 %
ReporteVentaClienteFoxcom88 4
>884 5
Convert886 =
(88= >
List88> B
<88B C
VentaClienteFoxcom88C U
>88U V

)88d e
{99 	
List:: 
<:: %
ReporteVentaClienteFoxcom:: *
>::* + 
reporteVentasFoxcont::, @
=::A B
new::C F
List::G K
<::K L%
ReporteVentaClienteFoxcom::L e
>::e f
(::f g
)::g h
;::h i
foreach;; 
(;; 
var;; 
ventaFoxcont;; %
in;;& (

);;6 7
{<< 
reporteVentasFoxcont== $
.==$ %
Add==% (
(==( )
new==) ,%
ReporteVentaClienteFoxcom==- F
(==F G
ventaFoxcont==G S
)==S T
)==T U
;==U V
}>> 
return??  
reporteVentasFoxcont?? '
;??' (
}@@ 	
}AA 
}BB �
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Operacion\ResumenDeTransaccionGeneral.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 '
ResumenDeTransaccionGeneral

 ,
{ 
public
ResumenDeTransaccionGeneral
(
)
{ 	
} 	
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string $
CodigoTipoOperacionSunat .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
string $
NombreTipoOperacionSunat .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DateTime $
FechaOperacionReferencia 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
string 
Comprobante !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string *
ComprobanteOperacionReferencia 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
string 
Empleado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
ActorNegocioExterno )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
decimal 
ImporteTotal #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
List 
< '
ResumenDeTransaccionGeneral /
>/ 0
Convert1 8
(8 9
)9 :
{ 	
return 
null 
; 
}   	
}!! 
}%% �
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Pedido\OrdenDePedido.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,
Core		, 0
.		0 1
Pedido		1 7
{

 
public 

class 

: 
OperacionDeVenta  0
{ 
public

(
)
{ 	
} 	
public 

( 
Transaccion (
_transaccion) 5
)5 6
{ 	
this 
. 
transaccion 
= 
_transaccion +
;+ ,
} 	
public 
static 
List 
< 

>( )
Convert* 1
(1 2
List2 6
<6 7
Transaccion7 B
>B C

)Q R
{ 	
List 
< 

> 
ordenesDeCotizacion  3
=4 5
new6 9
List: >
<> ?

>L M
(M N
)N O
;O P
foreach 
( 
var 
transaccion $
in% '

)5 6
{ 
ordenesDeCotizacion #
.# $
Add$ '
(' (
new( +

(9 :
transaccion: E
)E F
)F G
;G H
} 
return 
ordenesDeCotizacion &
;& '
} 	
public 
long 
IdPedido 
{   	
get!! 
{!! 
return!! 
(!! 
long!! 
)!! 
this!! #
.!!# $
transaccion!!$ /
.!!/ 0 
id_transaccion_padre!!0 D
;!!D E
}!!F G
}"" 	
}## 
}$$ �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Pedido\PlainModel\PedidosInvalidados.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Pedido1 7
.7 8

PlainModel8 B
{		 
public

 

class

 
PedidosInvalidados

 #
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
int
IdEstado
{
get
;
set
;
}
public 
DateTime 
FechaEmision $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
Comprobante !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
DocumentoCliente &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
Cliente 
{ 
get  #
=>$ &
DocumentoCliente' 7
+8 9
$str: ?
+@ A
(B C
AliasC H
==I K
nullL P
?Q R

:a b
Aliasc h
)h i
;i j
}k l
public 
string 
Alias 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Vendedor 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Total 
{ 
get "
;" #
set$ '
;' (
}) *
public 
DateTime 
FechaInvalidacion )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
static 
List 
< 
PedidosInvalidados -
>- .
Convert/ 6
(6 7
)7 8
{ 	
return 
new 
List 
< 
PedidosInvalidados .
>. /
(/ 0
)0 1
;1 2
} 	
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Pedido\PrincipalPedidoData.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Pedido1 7
{ 
public		 

class		 
PrincipalPedidoData		 $
{

 
public 
DateTime 
FechaActual #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
bool %
TieneRolVendedorDeNegocio -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public
bool
TieneRolCajeroDeNegocio
{
get
;
set
;
}
public 
int !
ComprobanteParaPedido (
{) *
get+ .
;. /
set/ 2
;2 3
}4 5
} 
} �
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Pedido\ReportData\PrincipalReportData.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 


 +
.

+ ,
Core

, 0
.

0 1
Pedido

1 7
.

7 8

ReportData

8 B
{ 
public 

class 
PrincipalReportData $
{
public 
List 
< 
Establecimiento #
># $
Establecimientos% 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
Establecimiento !
EstablecimientoSesion 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
ItemGenerico 
PuntoVentaSesion ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
DateTime 
FechaActual_ $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
long 
FechaActual 
{  !
get" %
{& '
return( .
FechaActual_/ ;
.; <$
ToJavaScriptMilliseconds< T
(T U
)U V
;V W
}X Y
}Z [
public 
long 
FechaHastaDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
long 
FechaDesdeDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
bool 
EsAdministrador #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} �
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Pedido\ResumenOrdenPedido.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Pedido1 7
{		 
public

 

class

 
ResumenOrdenPedido

 #
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
DateTime
FechaInicio
{
get
;
set
;
}
public 
string 
FechaEmision "
{# $
get% (
=>( *
FechaInicio* 5
.5 6
ToString6 >
(> ?
$str? W
)W X
;X Y
}Z [
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Comprobante !
{" #
get$ '
;' (
set( +
;+ ,
}- .
public 
string 
DocumentoCliente &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
Cliente 
{ 
get  #
=>$ &
DocumentoCliente' 7
+8 9
$str: ?
+@ A
(B C
AliasC H
==I K
nullL P
?Q R

:b c
Aliasd i
)i j
;j k
}l m
public 
string 
Alias 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Vendedor 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Total 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Estado 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
IdEstado 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
EstaInvalidado "
{# $
get% (
=>) +
IdEstado, 4
==5 7
MaestroSettings8 G
.G H
DefaultH O
.O P,
 IdDetalleMaestroEstadoInvalidadoP p
;p q
}r s
public 
static 
List 
< 
ResumenOrdenPedido -
>- .
Convert/ 6
(6 7
)7 8
{ 	
return 
new 
List 
< 
ResumenOrdenPedido .
>. /
(/ 0
)0 1
;1 2
} 	
}   
}"" �
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Sesion\MaestroSesion.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,
Core		, 0
.		0 1
Sesion		1 7
{

 
public 

class 

{ 
public
ItemGenerico
Moneda
{
get
;
set
;
}
public 

( 
) 
{ 	
} 	
} 
} �
WD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\TipoCambio\TipoCambio.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1

TipoCambio1 ;
{		 
public

 

class

 

TipoCambio

 
{ 
public 
int 
IdMoneda 
{ 
get !
;! "
set# &
;& '
}( )
public
DateTime
Fecha
{
get
;
set
;
}
public 
decimal 
ValorCompra "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
decimal 

ValorVenta !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool 
Estado 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
FechaString !
{" #
get$ '
=>( *
Fecha+ 0
.0 1
ToString1 9
(9 :
$str: F
)F G
;G H
}I J
public 

TipoCambio 
( 
) 
{ 	
IdMoneda 
= 
MaestroSettings &
.& '
Default' .
.. /)
IdDetalleMaestroMonedaDolares/ L
;L M
Fecha 
= 
DateTimeUtil  
.  !
FechaActual! ,
(, -
)- .
.. /
Date/ 3
;3 4
ValorCompra 
= 
$num 
; 

ValorVenta 
= 
$num 
; 
} 	
} 
} �	
dD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\ConsultaComprobanteParameter.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
Core, 0
.0 1
Venta1 6
{ 
public		 

class		 (
ConsultaComprobanteParameter		 -
{

 
public 
string 
Comprobante !
{" #
get$ '
;' (
set( +
;+ ,
}, -
public 
string 
Serie 
{ 
get !
;! "
set" %
;% &
}& '
public
string
Numero
{
get
;
set
;
}
public 
DateTime 
FechaEmision $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} �%
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\ItemDetalladoOperacionComercial.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
public 

class +
ItemDetalladoOperacionComercial 0
{
public 
long 
IdItem 
{ 
get  
;  !
set" %
;% &
}' (
public 
long 
IdOperacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
long 
? 
IdOperacionWrapper '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
Comprobante !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Familia 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Sufijo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Caracteristica1 %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Caracteristica2 %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Caracteristica3 %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public   
string   
Caracteristica4   %
{  & '
get  ( +
;  + ,
set  - 0
;  0 1
}  2 3
public!! 
string!! 
Caracteristica5!! %
{!!& '
get!!( +
;!!+ ,
set!!- 0
;!!0 1
}!!2 3
public"" 
string"" 
Caracteristica6"" %
{""& '
get""( +
;""+ ,
set""- 0
;""0 1
}""2 3
public## 
string## 
Caracteristica7## %
{##& '
get##( +
;##+ ,
set##- 0
;##0 1
}##2 3
public$$ 
string$$ 
Caracteristica8$$ %
{$$& '
get$$( +
;$$+ ,
set$$- 0
;$$0 1
}$$2 3
public%% 
string%% 
Caracteristica9%% %
{%%& '
get%%( +
;%%+ ,
set%%- 0
;%%0 1
}%%2 3
public&& 
string&& 
Caracteristica10&& &
{&&' (
get&&) ,
;&&, -
set&&. 1
;&&1 2
}&&3 4
public'' 
decimal'' 
Cantidad'' 
{''  
get''! $
;''$ %
set''& )
;'') *
}''+ ,
public(( 
decimal(( 
Importe(( 
{((  
get((! $
;(($ %
set((& )
;(() *
}((+ ,
public)) 
decimal)) 
PrecioUnitario)) %
{** 	
get++ 
{,, 
return,, 
Importe,, 
/,, 
Cantidad,, '
;,,' (
},,) *
}-- 	
public.. 
string.. 
	MedioPago.. 
{..  !
get.." %
;..% &
set..' *
;..* +
}.., -
public// +
ItemDetalladoOperacionComercial// .
(//. /
)/// 0
{00 	
}00
 
public11 +
ItemDetalladoOperacionComercial11 .
Clone11/ 4
(114 5
)115 6
{22 	
return33 
(33 +
ItemDetalladoOperacionComercial33 3
)333 4
this334 8
.338 9
MemberwiseClone339 H
(33H I
)33I J
;33J K
}44 	
public66 
static66 
List66 
<66 +
ItemDetalladoOperacionComercial66 :
>66: ;
Convert66< C
(66C D
)66D E
{77 	
return88 
new88 
List88 
<88 +
ItemDetalladoOperacionComercial88 ;
>88; <
(88< =
)88= >
;88> ?
}99 	
}<< 
}== �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\ItemAgrupadoOperacionComercial.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
public 

class *
ItemConGrupoOperacionComercial /
{
public 
long 
? 
IdItem 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 

NombreItem  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
IdGrupo 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
NombreGrupo !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal "
PrecioUnitarioPromedio -
{ 	
get 
{ 
return 
Importe 
/ 
Cantidad '
;' (
}) *
} 	
public *
ItemConGrupoOperacionComercial -
(- .
). /
{ 	
}
 
public 
static 
List 
< *
ItemConGrupoOperacionComercial 9
>9 :
Convert; B
(B C
)C D
{ 	
return 
new 
List 
< *
ItemConGrupoOperacionComercial :
>: ;
(; <
)< =
;= >
}   	
}## 
}$$ �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\ItemOperacionComercial.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
public		 

class		 "
ItemOperacionComercial		 '
{

 
public 
int 
IdItem 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 

NombreItem  
{! "
get# &
;& '
set( +
;+ ,
}- .
public
decimal
Cantidad
{
get
;
set
;
}
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal "
PrecioUnitarioPromedio -
{ 	
get 
{ 
return 
Importe 
/ 
Cantidad '
;' (
}) *
} 	
public "
ItemOperacionComercial %
(% &
)& '
{ 	
}
 
public 
static 
List 
< "
ItemOperacionComercial 1
>1 2
Convert3 :
(: ;
); <
{ 	
return 
new 
List 
< "
ItemOperacionComercial 2
>2 3
(3 4
)4 5
;5 6
} 	
} 
} �

VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\InvalidarVenta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public

class
InvalidarVenta
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
bool 

EsDiferida 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
ImporteTotal #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
	DatosPago 
Pago 
{ 
get  #
;# $
set% (
;( )
}* +
public 
InvalidarVenta 
( 
) 
{ 	
}
 
} 
} ��
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\OperacionInvalidacionVenta.cs
	namespace
Tsp
 
.
Sigescom
.
Modelo
.
	Entidades
.
Custom
{ 
public 

class !
OperacionInvalidacion &
{ 
public 
Venta 
Venta 
{ 
get  
;  !
set" %
;% &
}' (
public 
OrdenDeVenta 

OrdenVenta &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
List 
< 
Detalle_transaccion '
>' (
DetallesOperacion) :
{; <
get= @
;@ A
setB E
;E F
}G H
public 
List 
< 
Detalle_transaccion '
>' (#
DetallesBienesOperacion) @
{A B
getC F
;F G
setH K
;K L
}M N
public 
List 
< 
Detalle_transaccion '
>' (.
"DetallesMovimientoAlmacenOperacion) K
{L M
getN Q
;Q R
setS V
;V W
}X Y
public 
bool 
EsDiferidaOperacion '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
bool -
!HaySeccionEntregaAlmacenOperacion 5
{6 7
get8 ;
=>< >
VentasSettings? M
.M N
DefaultN U
.U V(
MostrarSeccionEntregaEnVentaV r
&&s u
(v w$
EsOrdenOrigenCompletada	w �
||
� �"
EsOrdenOrigenParcial
� �
)
� �
;
� �
}
� �
public 
bool )
HayMovimientoAlmacenOperacion 1
{2 3
get4 7
=>8 :
(; <
!< =
VentasSettings= K
.K L
DefaultL S
.S T(
MostrarSeccionEntregaEnVentaT p
&&q s$
EsOrdenOrigenCompletada	t �
)
� �
||
� �
(
� �/
!HaySeccionEntregaAlmacenOperacion
� �
&&
� �
!
� �!
EsDiferidaOperacion
� �
)
� �
;
� �
}
� �
public 
string  
ObservacionOperacion *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
	DatosPago 

{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
List 
< 
Cuota 
> 
Cuotas !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool #
EsPendienteEstadoCuotas +
{, -
get. 1
=>2 4
Cuotas5 ;
.; <
Sum< ?
(? @
c@ A
=>B D
cE F
.F G
saldoG L
)L M
==N P
CuotasQ W
.W X
SumX [
([ \
c\ ]
=>^ `
ca b
.b c
totalc h
)h i
;i j
}k l
public 
bool !
EsParcialEstadoCuotas )
{* +
get, /
=>0 2
Cuotas3 9
.9 :
Sum: =
(= >
c> ?
=>@ B
cC D
.D E
saldoE J
)J K
!=L N
$numO P
&&Q S
CuotasT Z
.Z [
Sum[ ^
(^ _
c_ `
=>a c
cd e
.e f
saldof k
)k l
!=m o
Cuotasp v
.v w
Sumw z
(z {
c{ |
=>} 
c
� �
.
� �
total
� �
)
� �
;
� �
}
� �
public 
bool "
EsCompletoEstadoCuotas *
{+ ,
get- 0
=>1 3
Cuotas4 :
.: ;
Sum; >
(> ?
c? @
=>A C
cD E
.E F
saldoF K
)K L
==M O
$numP Q
;Q R
}S T
public 
DateTime 
FechaActual #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public   
int   
IdMoneda   
{   
get   !
;  ! "
set  # &
;  & '
}  ( )
public!! 
decimal!! 
TipoDeCambio!! #
{!!$ %
get!!& )
;!!) *
set!!+ .
;!!. /
}!!0 1
public"" 
int"" 
IdUnidadDeNegocio"" $
{""% &
get""' *
;""* +
set"", /
;""/ 0
}""1 2
public## 
int## 
	IdCliente## 
{## 
get## "
;##" #
set##$ '
;##' (
}##) *
public$$ 
string$$ 
AliasCliente$$ "
{$$# $
get$$% (
;$$( )
set$$* -
;$$- .
}$$/ 0
public%% 
int%% 

IdEmpleado%% 
{%% 
get%%  #
;%%# $
set%%% (
;%%( )
}%%* +
public&& 
int&& 
IdCaja&& 
{&& 
get&& 
;&&  
set&&! $
;&&$ %
}&&& '
public'' 
int'' 
	IdAlmacen'' 
{'' 
get'' "
;''" #
set''$ '
;''' (
}'') *
public(( 
int(( 
IdCentroAtencion(( #
{(($ %
get((& )
;(() *
set((+ .
;((. /
}((0 1
public)) 
decimal)) 
ImporteTotal)) #
{))$ %
get))& )
;))) *
set))+ .
;)). /
}))0 1
public** 
decimal** 
ImportePagoTotal** '
{**( )
get*** -
;**- .
set**/ 2
;**2 3
}**4 5
public++ 
decimal++ 
DescuentoGlobal++ &
{++' (
get++) ,
;++, -
set++. 1
;++1 2
}++3 4
public,, 
decimal,, 
DescuentoPorItem,, '
{,,( )
get,,* -
;,,- .
set,,/ 2
;,,2 3
},,4 5
public-- 
decimal-- 
Anticipo-- 
{--  !
get--" %
;--% &
set--' *
;--* +
}--, -
public.. 
decimal.. 
Gravada.. 
{..  
get..! $
;..$ %
set..& )
;..) *
}..+ ,
public// 
decimal// 
	Exonerada//  
{//! "
get//# &
;//& '
set//( +
;//+ ,
}//- .
public00 
decimal00 
Inafecta00 
{00  !
get00" %
;00% &
set00' *
;00* +
}00, -
public11 
decimal11 
Gratuita11 
{11  !
get11" %
;11% &
set11' *
;11* +
}11, -
public22 
decimal22 
Igv22 
{22 
get22  
;22  !
set22" %
;22% &
}22' (
public33 
bool33 
GravaIgv33 
{33 
get33 "
=>33# %
Igv33& )
>33* +
$num33, -
;33- .
}33/ 0
public44 
decimal44 
Isc44 
{44 
get44  
;44  !
set44" %
;44% &
}44' (
public55 
decimal55 
Icbper55 
{55 
get55  #
;55# $
set55% (
;55( )
}55* +
public66 
decimal66  
NumeroBolsasPlastico66 +
{66, -
get66. 1
;661 2
set663 6
;666 7
}668 9
public77 
decimal77 
OtrosCargos77 "
{77# $
get77% (
;77( )
set77* -
;77- .
}77/ 0
public88 
decimal88 

{88% &
get88' *
;88* +
set88, /
;88/ 0
}881 2
public99 
bool99 !
EsOrdenOrigenDiferida99 )
{99* +
get99, /
=>990 2

OrdenVenta993 =
.99= >#
IndicadorImpactoAlmacen99> U
==99V X#
IndicadorImpactoAlmacen99Y p
.99p q
Diferida99q y
;99y z
}99{ |
public:: 
bool:: "
EsOrdenOrigenPendiente:: *
{::+ ,
get::- 0
=>::1 3

OrdenVenta::4 >
.::> ?&
IdEstadoActualOrdenAlmacen::? Y
==::Z \
MaestroSettings::] l
.::l m
Default::m t
.::t u,
IdDetalleMaestroEstadoPendiente	::u �
;
::� �
}
::� �
public;; 
bool;;  
EsOrdenOrigenParcial;; (
{;;) *
get;;+ .
=>;;/ 1

OrdenVenta;;2 <
.;;< =&
IdEstadoActualOrdenAlmacen;;= W
==;;X Z
MaestroSettings;;[ j
.;;j k
Default;;k r
.;;r s*
IdDetalleMaestroEstadoParcial	;;s �
;
;;� �
}
;;� �
public<< 
bool<< #
EsOrdenOrigenCompletada<< +
{<<, -
get<<. 1
=><<2 4

OrdenVenta<<5 ?
.<<? @&
IdEstadoActualOrdenAlmacen<<@ Z
==<<[ ]
MaestroSettings<<^ m
.<<m n
Default<<n u
.<<u v-
 IdDetalleMaestroEstadoCompletada	<<v �
;
<<� �
}
<<� �
public>> !
OperacionInvalidacion>> $
(>>$ %
)>>% &
{?? 	
}??
 
public@@ !
OperacionInvalidacion@@ $
(@@$ %
Venta@@% *
venta@@+ 0
,@@0 1
OrdenDeVenta@@2 >

ordenVenta@@? I
,@@I J
DateTime@@K S
fechaActual@@T _
,@@_ `
bool@@a e

esDiferida@@f p
,@@p q
string@@r x
observacion	@@y �
,
@@� �
	DatosPago
@@� �
pago
@@� �
,
@@� �$
UserProfileSessionData
@@� �

@@� �
)
@@� �
{AA 	
VentaBB 
=BB 
ventaBB 
;BB 

OrdenVentaCC 
=CC 

ordenVentaCC #
;CC# $
FechaActualDD 
=DD 
fechaActualDD %
;DD% &
DetallesOperacionEE 
=EE 

ordenVentaEE  *
.EE* +
DetallesEE+ 3
(EE3 4
)EE4 5
.EE5 6
SelectEE6 <
(EE< =
dvEE= ?
=>EE@ B
dvEEC E
.EEE F
DetalleTransaccionEEF X
(EEX Y
)EEY Z
)EEZ [
.EE[ \
ToListEE\ b
(EEb c
)EEc d
;EEd e#
DetallesBienesOperacionFF #
=FF$ %

ordenVentaFF& 0
.FF0 1
DetallesFF1 9
(FF9 :
)FF: ;
.FF; <
WhereFF< A
(FFA B
dFFB C
=>FFD F
dFFG H
.FFH I
ProductoFFI Q
.FFQ R
EsBienFFR X
)FFX Y
.FFY Z
SelectFFZ `
(FF` a
dFFa b
=>FFc e
dFFf g
.FFg h
DetalleTransaccionFFh z
(FFz {
)FF{ |
)FF| }
.FF} ~
ToList	FF~ �
(
FF� �
)
FF� �
;
FF� �
EsDiferidaOperacionGG 
=GG  !

esDiferidaGG" ,
;GG, - 
ObservacionOperacionHH  
=HH! "
observacionHH# .
=HH/ 0
stringHH1 7
.HH7 8

(HHE F
observacionHHF Q
)HHQ R
?HHS T
$strHHU ^
:HH_ `
RegexHHa f
.HHf g
ReplaceHHg n
(HHn o
observacionHHo z
,HHz {
$str	HH| �
,
HH� �
$str
HH� �
)
HH� �
;
HH� �
;
HH� �

=II 
pagoII  
;II  !

.JJ 
TrazaJJ 
.JJ  
MedioDePagoJJ  +
=JJ, -

.JJ; <
TrazaJJ< A
.JJA B
MedioDePagoJJB M
??JJN P
newJJQ T
ItemGenericoJJU a
(JJa b
MaestroSettingsJJb q
.JJq r
DefaultJJr y
.JJy z0
#IdDetalleMaestroMedioDepagoEfectivo	JJz �
)
JJ� �
;
JJ� �

.KK 
TrazaKK 
.KK  
InfoKK  $
=KK% &

.KK4 5
TrazaKK5 :
.KK: ;
InfoKK; ?
??KK@ B
newKKC F
InfoPagoKKG O
(KKO P
)KKP Q
;KKQ R

.LL 
TrazaLL 
.LL  
InfoLL  $
.LL$ %
EntidadFinancieraLL% 6
=LL7 8
(LL9 :

.LLG H
TrazaLLH M
.LLM N
MedioDePagoLLN Y
.LLY Z
IdLLZ \
==LL] _
MaestroSettingsLL` o
.LLo p
DefaultLLp w
.LLw x=
0IdDetalleMaestroMedioDepagoTransferenciaDeFondos	LLx �
||
LL� �

LL� �
.
LL� �
Traza
LL� �
.
LL� �
MedioDePago
LL� �
.
LL� �
Id
LL� �
==
LL� �
MaestroSettings
LL� �
.
LL� �
Default
LL� �
.
LL� �9
+IdDetalleMaestroMedioDepagoDepositoEnCuenta
LL� �
)
LL� �
?
LL� �
new
LL� �
ItemGenerico
LL� �
{
LL� �
Id
LL� �
=
LL� �
int
LL� �
.
LL� �
Parse
LL� �
(
LL� �

LL� �
.
LL� �
Traza
LL� �
.
LL� �
Info
LL� �
.
LL� �
CuentaBancaria
LL� �
.
LL� �
Valor
LL� �
)
LL� �
}
LL� �
:
LL� �

LL� �
.
LL� �
Traza
LL� �
.
LL� �
Info
LL� �
.
LL� �
EntidadFinanciera
LL� �
;
LL� �

.MM 
TrazaMM 
.MM  
InfoMM  $
.MM$ %
EntidadFinancieraMM% 6
=MM7 8
DiccionarioMM9 D
.MMD E3
'IdsMediosDePagoQueTienenEntidadBancariaMME l
.MMl m
ContainsMMm u
(MMu v

.
MM� �
Traza
MM� �
.
MM� �
MedioDePago
MM� �
.
MM� �
Id
MM� �
)
MM� �
?
MM� �

MM� �
.
MM� �
Traza
MM� �
.
MM� �
Info
MM� �
.
MM� �
EntidadFinanciera
MM� �
:
MM� �
new
MM� �
ItemGenerico
MM� �
(
MM� �
MaestroSettings
MM� �
.
MM� �
Default
MM� �
.
MM� �7
)IdDetalleMaestroEntidadBancariaPorDefecto
MM� �
)
MM� �
;
MM� �

.NN 
TrazaNN 
.NN  
InfoNN  $
.NN$ %
OperadorTarjetaNN% 4
=NN5 6

.NND E
TrazaNNE J
.NNJ K
MedioDePagoNNK V
.NNV W
IdNNW Y
==NNZ \
MaestroSettingsNN] l
.NNl m
DefaultNNm t
.NNt u6
)IdDetalleMaestroMedioDePagoTarjetaCredito	NNu �
?
NN� �

NN� �
.
NN� �
Traza
NN� �
.
NN� �
Info
NN� �
.
NN� �
OperadorTarjeta
NN� �
:
NN� �
new
NN� �
ItemGenerico
NN� �
(
NN� �
)
NN� �
;
NN� �

.OO 
TrazaOO 
.OO  
InfoOO  $
.OO$ %
ObservacionOO% 0
=OO1 2
stringOO3 9
.OO9 :

(OOG H

.OOU V
TrazaOOV [
.OO[ \
InfoOO\ `
.OO` a
ObservacionOOa l
)OOl m
?OOn o
$strOOp y
:OOz {

.
OO� �
Traza
OO� �
.
OO� �
Info
OO� �
.
OO� �
Observacion
OO� �
;
OO� �
CuotasPP 
=PP 

ordenVentaPP 
.PP  
CuotasPP  &
(PP& '
)PP' (
;PP( )
IdUnidadDeNegocioQQ 
=QQ 

ordenVentaQQ  *
.QQ* +
IdUnidadDeNegocioQQ+ <
;QQ< =
IdMonedaRR 
=RR 

ordenVentaRR !
.RR! "
IdMonedaRR" *
;RR* +
TipoDeCambioSS 
=SS 

ordenVentaSS %
.SS% &
TipoDeCambioSS& 2
;SS2 3
	IdClienteTT 
=TT 

ordenVentaTT "
.TT" #
	IdClienteTT# ,
;TT, -
AliasClienteUU 
=UU 

ordenVentaUU %
.UU% &
AliasClienteUU& 2
(UU2 3
)UU3 4
;UU4 5

IdEmpleadoVV 
=VV 

.VV& '
EmpleadoVV' /
.VV/ 0
IdVV0 2
;VV2 3
IdCajaWW 
=WW 
ventaWW 
.WW 
ObtenerPagosWW '
(WW' (
)WW( )
.WW) *
CountWW* /
(WW/ 0
)WW0 1
>WW2 3
$numWW4 5
?WW6 7
ventaWW8 =
.WW= >
ObtenerPagosWW> J
(WWJ K
)WWK L
.WWL M
FirstWWM R
(WWR S
)WWS T
.WWT U
TransaccionWWU `
(WW` a
)WWa b
.WWb c$
id_actor_negocio_internoWWc {
:WW| }

.
WW� �*
CentroDeAtencionSeleccionado
WW� �
.
WW� �
Id
WW� �
;
WW� �
	IdAlmacenXX 
=XX 

.XX% &4
(IdCentroAtencionQueTieneElStockIntegradaXX& N
;XXN O
IdCentroAtencionYY 
=YY 

.YY, -(
CentroDeAtencionSeleccionadoYY- I
.YYI J
IdYYJ L
;YYL M
ImporteTotalZZ 
=ZZ 
DetallesOperacionZZ ,
.ZZ, -
SumZZ- 0
(ZZ0 1
dZZ1 2
=>ZZ3 5
dZZ6 7
.ZZ7 8
totalZZ8 =
)ZZ= >
+ZZ? @

ordenVentaZZA K
.ZZK L
IcbperZZL R
(ZZR S
)ZZS T
;ZZT U
ImportePagoTotal[[ 
=[[ 
Cuotas[[ %
.[[% &
Sum[[& )
([[) *
c[[* +
=>[[, .
c[[/ 0
.[[0 1

)[[> ?
;[[? @
DescuentoGlobal\\ 
=\\ 
$num\\ 
;\\  
DescuentoPorItem]] 
=]] 
$num]]  
;]]  !
Anticipo^^ 
=^^ 
$num^^ 
;^^ 
Gravada__ 
=__ 
DetallesOperacion__ '
.__' (
Sum__( +
(__+ ,
d__, -
=>__. 0
d__1 2
.__2 3
igv__3 6
)__6 7
>__8 9
$num__: ;
?__< =
DetallesOperacion__> O
.__O P
Sum__P S
(__S T
d__T U
=>__V X
d__Y Z
.__Z [
total__[ `
-__a b
d__c d
.__d e
igv__e h
)__h i
:__j k
$num__l m
;__m n
	Exonerada`` 
=`` 
DetallesOperacion`` )
.``) *
Sum``* -
(``- .
d``. /
=>``0 2
d``3 4
.``4 5
igv``5 8
)``8 9
>``: ;
$num``< =
?``> ?
$num``@ A
:``B C
DetallesOperacion``D U
.``U V
Sum``V Y
(``Y Z
d``Z [
=>``\ ^
d``_ `
.``` a
total``a f
)``f g
;``g h
Inafectaaa 
=aa 
$numaa 
;aa 
Gratuitabb 
=bb 
$numbb 
;bb 
Igvcc 
=cc 
DetallesOperacioncc #
.cc# $
Sumcc$ '
(cc' (
dcc( )
=>cc* ,
dcc- .
.cc. /
igvcc/ 2
)cc2 3
>cc4 5
$numcc6 7
?cc8 9
DetallesOperacioncc: K
.ccK L
SumccL O
(ccO P
dccP Q
=>ccR T
dccU V
.ccV W
igvccW Z
)ccZ [
:cc\ ]
$numcc^ _
;cc_ `
Iscdd 
=dd 
$numdd 
;dd 
Icbperee 
=ee 

ordenVentaee 
.ee  
Icbperee  &
(ee& '
)ee' (
;ee( ) 
NumeroBolsasPlasticoff  
=ff! "

ordenVentaff# -
.ff- ."
NumeroBolsasDePlasticoff. D
(ffD E
)ffE F
;ffF G
OtrosCargosgg 
=gg 
$numgg 
;gg 

=hh 
$numhh 
;hh 
}ii 	
}kk 
publicll 

classll 

:ll  !
OperacionInvalidacionll! 6
{mm 
publicnn 
boolnn 
EsDebitonn 
{nn 
getnn "
;nn" #
setnn$ '
;nn' (
}nn) *
publicoo 
intoo 

IdTipoNotaoo 
{oo 
getoo  #
;oo# $
setoo% (
;oo( )
}oo* +
publicpp 
decimalpp 
	MontoNotapp  
{pp! "
getpp# &
;pp& '
setpp( +
;pp+ ,
}pp- .
publicqq 
boolqq 
EsPropioqq 
{qq 
getqq "
;qq" #
setqq$ '
;qq' (
}qq) *
publicrr 
intrr 
IdSerieComprobanterr %
{rr& '
getrr( +
;rr+ ,
setrr- 0
;rr0 1
}rr2 3
publicss 
intss 
IdTipoComprobantess $
{ss% &
getss' *
;ss* +
setss, /
;ss/ 0
}ss1 2
publictt 
stringtt 
NumeroSeriett !
{tt" #
gettt$ '
;tt' (
settt) ,
;tt, -
}tt. /
publicuu 
intuu 
NumeroComprobanteuu $
{uu% &
getuu' *
;uu* +
setuu, /
;uu/ 0
}uu1 2
publicvv 
Listvv 
<vv 
DetalleOrdenDeNotavv &
>vv& '
DetallesNotavv( 4
{vv5 6
getvv7 :
;vv: ;
setvv< ?
;vv? @
}vvA B
publicww 
intww '
IndicadorImpactoAlmacenNotaww .
{ww/ 0
getww1 4
;ww4 5
setww6 9
;ww9 :
}ww; <
publicxx 
boolxx $
HayMovimientoAlmacenNotaxx ,
{xx- .
getxx/ 2
;xx2 3
setxx4 7
;xx7 8
}xx9 :
publicyy 
stringyy 
SufijoCodigoyy "
{yy# $
getyy% (
;yy( )
setyy* -
;yy- .
}yy/ 0
publiczz 
intzz 
IdTipoTransaccionzz $
{zz% &
getzz' *
;zz* +
setzz, /
;zz/ 0
}zz1 2
public{{ 
int{{ "
IdTipoTransaccionOrden{{ )
{{{* +
get{{, /
;{{/ 0
set{{1 4
;{{4 5
}{{6 7
public|| 
int|| !
IdTipoTransaccionPago|| (
{||) *
get||+ .
;||. /
set||0 3
;||3 4
}||5 6
public}} 
int}} $
IdTipoTransaccionAlmacen}} +
{}}, -
get}}. 1
;}}1 2
set}}3 6
;}}6 7
}}}8 9
public~~ 
bool~~ 1
%NuevoComprobanteParaMovimientoAlmacen~~ 9
{~~: ;
get~~< ?
;~~? @
set~~A D
;~~D E
}~~F G
public
�� 

�� 
(
�� 
Venta
�� "
venta
��# (
,
��( )
OrdenDeVenta
��* 6

ordenVenta
��7 A
,
��A B
DateTime
��C K
fechaActual
��L W
,
��W X
int
��Y \

idTipoNota
��] g
,
��g h
int
��i l
idTipoComprobante
��m ~
,
��~ 
int��� �"
idSerieComprobante��� �
,��� �
bool��� �
esPropio��� �
,��� �
string��� �
numeroSerie��� �
,��� �
int��� �!
numeroComprobante��� �
,��� �
decimal��� �
	montoNota��� �
,��� �
List��� �
<��� �"
DetalleOrdenDeNota��� �
>��� �
detalles��� �
,��� �
bool��� �
esDebito��� �
,��� �
bool��� �

esDiferida��� �
,��� �
string��� �
observacion��� �
,��� �
	DatosPago��� �
pago��� �
,��� �&
UserProfileSessionData��� �

)��� �
:��� �
base��� �
(��� �
venta��� �
,��� �

ordenVenta��� �
,��� �
fechaActual��� �
,��� �

esDiferida��� �
,��� �
observacion��� �
,��� �
pago��� �
,��� �

)��� �
{
�� 	

IdTipoNota
�� 
=
�� 

idTipoNota
�� #
;
��# $
	MontoNota
�� 
=
�� 
	montoNota
�� !
;
��! "
EsPropio
�� 
=
�� 
esPropio
�� 
;
��   
IdSerieComprobante
�� 
=
��   
idSerieComprobante
��! 3
;
��3 4
IdTipoComprobante
�� 
=
�� 
idTipoComprobante
��  1
;
��1 2
NumeroSerie
�� 
=
�� 
numeroSerie
�� %
;
��% &
NumeroComprobante
�� 
=
�� 
numeroComprobante
��  1
;
��1 2
DetallesNota
�� 
=
�� 
detalles
�� #
;
��# $)
IndicadorImpactoAlmacenNota
�� '
=
��( )
(
��* +
int
��+ .
)
��. /%
IndicadorImpactoAlmacen
��/ F
.
��F G
NoImpactaNoBienes
��G X
;
��X Y&
HayMovimientoAlmacenNota
�� $
=
��% &
false
��' ,
;
��, -
EsDebito
�� 
=
�� 
esDebito
�� 
;
��  
SufijoCodigo
�� 
=
�� 
esDebito
�� #
?
��$ %
$str
��& *
:
��+ ,
$str
��- 1
;
��1 2
IdTipoTransaccion
�� 
=
�� 
Diccionario
��  +
.
��+ ,;
-MapeoDetalleMaestroVsTipoTransaccionParaVenta
��, Y
.
��Y Z
Single
��Z `
(
��` a
n
��a b
=>
��c e
n
��f g
.
��g h
Key
��h k
==
��l n

idTipoNota
��o y
)
��y z
.
��z {
Value��{ �
;��� �$
IdTipoTransaccionOrden
�� "
=
��# $
Diccionario
��% 0
.
��0 1 
MapeoWraperVsOrden
��1 C
.
��C D
Single
��D J
(
��J K
m
��K L
=>
��M O
m
��P Q
.
��Q R
Key
��R U
==
��V X
Diccionario
��Y d
.
��d e<
-MapeoDetalleMaestroVsTipoTransaccionParaVenta��e �
.��� �
Single��� �
(��� �
n��� �
=>��� �
n��� �
.��� �
Key��� �
==��� �

idTipoNota��� �
)��� �
.��� �
Value��� �
)��� �
.��� �
Value��� �
;��� �#
IdTipoTransaccionPago
�� !
=
��" #
Diccionario
��$ /
.
��/ 0-
MapeoOrdenVsMovimientoEconomico
��0 O
.
��O P
Single
��P V
(
��V W
m
��W X
=>
��Y [
m
��\ ]
.
��] ^
Key
��^ a
==
��b d$
IdTipoTransaccionOrden
��e {
)
��{ |
.
��| }
Value��} �
;��� �&
IdTipoTransaccionAlmacen
�� $
=
��% &
Diccionario
��' 2
.
��2 3-
MapeoOrdenVsMovimientoDeAlmacen
��3 R
.
��R S
SingleOrDefault
��S b
(
��b c
m
��c d
=>
��e g
m
��h i
.
��i j
Key
��j m
==
��n p%
IdTipoTransaccionOrden��q �
)��� �
.��� �
Value��� �
;��� �3
%NuevoComprobanteParaMovimientoAlmacen
�� 1
=
��2 3"
EsOrdenOrigenParcial
��4 H
&&
��I K
(
��L M

idTipoNota
��M W
==
��X Z
MaestroSettings
��[ j
.
��j k
Default
��k r
.
��r sM
>IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion��s �
||��� �

idTipoNota��� �
==��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �G
7IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal��� �
)��� �
;��� �
}
�� 	
}
�� 
}�� �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\ReportData\PrincipalReportData.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Negocio		 %
.		% &
Venta		& +
.		+ ,
Report		, 2
{

 
public 

class 
PrincipalReportData $
{ 
public
List
<
Establecimiento
>
Establecimientos
{
get
;
set
;
}
public 
Establecimiento !
EstablecimientoSesion 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
ItemGenerico 
PuntoVentaSesion ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
List 
< 
ItemGenerico  
>  !
PuntosVentas" .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
List 
< &
Familia_Concepto_Comercial .
>. /
Familias0 8
{9 :
get; >
;> ?
set@ C
;C D
}E F
public 
DateTime 
FechaActual_ $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
long 
FechaActual 
{  !
get" %
{& '
return( .
FechaActual_/ ;
.; <$
ToJavaScriptMilliseconds< T
(T U
)U V
;V W
}X Y
}Z [
public 
long 
FechaHastaDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
long 
FechaDesdeDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B$
ToJavaScriptMillisecondsB Z
(Z [
)[ \
;\ ]
}^ _
}` a
public 
bool 
EsAdministrador #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} �
qD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\TransaccionPorSerieDeComprobanteYConcepto.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
[		 
Serializable		 
]		 
public

 

class

 5
)TransaccionPorSerieDeComprobanteYConcepto

 :
{ 
public 
int 
IdTipoComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
string
NombreCortoComprobante
{
get
;
set
;
}
public 
string 
Serie 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
PrecioUnitario %
{ 	
get 
{ 
return 
Importe 
/ 
Cantidad '
;' (
}) *
} 	
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 5
)TransaccionPorSerieDeComprobanteYConcepto 8
(8 9
)9 :
{ 	
} 	
public 
static 
List 
< 5
)TransaccionPorSerieDeComprobanteYConcepto D
>D E
ConvertF M
(M N
)N O
{ 	
return   
new   
List   
<   5
)TransaccionPorSerieDeComprobanteYConcepto   E
>  E F
(  F G
)  G H
;  H I
}!! 	
}"" 
}## �
rD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\TransaccionPorSerieDeComprobanteYCategoria.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
[		 
Serializable		 
]		 
public

 

class

 6
*TransaccionPorSerieDeComprobanteYCategoria

 ;
{ 
public 
int 
IdTipoComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
string
NombreCortoComprobante
{
get
;
set
;
}
public 
string 
Serie 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
IdCategoria 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
	Categoria 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
PrecioUnitario %
{ 	
get 
{ 
return 
Importe 
/ 
Cantidad '
;' (
}) *
} 	
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 6
*TransaccionPorSerieDeComprobanteYCategoria 9
(9 :
): ;
{ 	
} 	
public 
static 
List 
< 6
*TransaccionPorSerieDeComprobanteYCategoria E
>E F
ConvertG N
(N O
)O P
{ 	
return   
new   
List   
<   6
*TransaccionPorSerieDeComprobanteYCategoria   F
>  F G
(  G H
)  H I
;  I J
}!! 	
}"" 
}## �#
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\VentaConceptoCliente.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{		 
public

 

class

  
VentaConceptoCliente

 %
{ 
public 
string 
DocumentoIdentidad (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public
string
RazonSocial
{
get
;
set
;
}
public 
string 
	Direccion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
int 
IdUbigeo 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Ubigeo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Departamento "
{# $
get% (
=>) +
IdUbigeo, 4
==5 7

.E F
DefaultF M
.M N"
idUbigeoNoEspecificadoN d
?e f
$strg j
:k l
Ubigeom s
.s t
Splitt y
(y z
$charz }
)} ~
[~ 
$num	 �
]
� �
;
� �
}
� �
public 
string 
	Provincia 
{  !
get" %
=>& (
IdUbigeo) 1
==2 4

.B C
DefaultC J
.J K"
idUbigeoNoEspecificadoK a
?b c
$strd g
:h i
Ubigeoj p
.p q
Splitq v
(v w
$charw z
)z {
[{ |
$num| }
]} ~
;~ 
}
� �
public 
string 
Distrito 
{  
get! $
=>% '
IdUbigeo( 0
==1 3

.A B
DefaultB I
.I J"
idUbigeoNoEspecificadoJ `
?a b
$strc f
:g h
Ubigeoi o
.o p
Splitp u
(u v
$charv y
)y z
[z {
$num{ |
]| }
;} ~
}	 �
public 
DateTime 
FechaInicio #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
FechaEmision "
{# $
get% (
=>) +
FechaInicio, 7
.7 8
ToString8 @
(@ A
)A B
;B C
}D E
public 
string 
SerieComprobante &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
int 
NumeroComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
Comprobante !
{" #
get$ '
=>( *
SerieComprobante+ ;
+< =
$str> A
+B C
NumeroComprobanteD U
.U V
ToStringV ^
(^ _
)_ `
;` a
}b c
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
UnidadMedida "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
static 
List 
<  
VentaConceptoCliente /
>/ 0
Convert1 8
(8 9
)9 :
{ 	
return   
new   
List   
<    
VentaConceptoCliente   0
>  0 1
(  1 2
)  2 3
;  3 4
}!! 	
}## 
}&& �
_D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EBookViewModel\Custom\LibroCompras.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
EBookViewModel, :
.: ;
Custom; A
{		 
public

 

class

 
LibroCompras

 
:

 
LibroElectronico

  0
{ 
public 
LibroCompras 
( 
) 
{
} 	
public 
static 
string 
ContenidoTXT )
() *
List* .
<. /
LibroCompras/ ;
>; <
logs= A
)A B
{ 	
string 
lines 
= 
$str 
; 
return 
lines 
; 
} 	
public 
static 
string 
NombreArchivoTXT -
(- .
string. 4"
documentoIdentidadSede5 K
,K L
PeriodoM T
periodoU \
,\ ]
ItemGenerico^ j
librok p
,p q
Listr v
<v w
LibroCompras	w �
>
� �
logs
� �
)
� �
{ 	
return 
( 
$str 
+ "
documentoIdentidadSede 3
+4 5
periodo6 =
.= >
nombre> D
+E F
$strG K
+L M
libroN S
.S T
CodigoT Z
+[ \
$str] b
+c d
(e f
logsf j
.j k
Countk p
>q r
$nums t
?u v
$strw z
:{ |
$str	} �
)
� �
+
� �
$str
� �
)
� �
;
� �
} 	
public 
static 
string 
NombreArchivoCSV -
(- .
string. 4"
documentoIdentidadSede5 K
,K L
PeriodoM T
periodoU \
,\ ]
ItemGenerico^ j
librok p
,p q
Listr v
<v w
LibroCompras	w �
>
� �
logs
� �
)
� �
{ 	
return 
( 
$str 
+ "
documentoIdentidadSede 3
+4 5
periodo6 =
.= >
nombre> D
+E F
$strG K
+L M
libroN S
.S T
CodigoT Z
+[ \
$str] b
+c d
(e f
logsf j
.j k
Countk p
>q r
$nums t
?u v
$strw z
:{ |
$str	} �
)
� �
+
� �
$str
� �
)
� �
;
� �
} 	
public 
static 
string 
CabeceraArchivoCSV /
{   	
get!! 
=>!! 
$str	!! �
;
!!� �
}"" 	
public$$ 
static$$ 
string$$ 
ContenidoCSV$$ )
($$) *
List$$* .
<$$. /
LibroCompras$$/ ;
>$$; <
logs$$= A
)$$A B
{%% 	
string&& 
lines&& 
=&& 
$str&& 
;&& 
lines'' 
+='' 
CabeceraArchivoCSV'' '
;''' (
lines(( 
+=(( 
Environment((  
.((  !
NewLine((! (
;((( )
return33 
lines33 
;33 
}44 	
}55 
}66 �
mD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EBookViewModel\Custom\LibroComprasNoDomiciliadas.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
EBookViewModel, :
.: ;
Custom; A
{ 
public		 

class		 &
LibroComprasNoDomiciliadas		 +
:		, -
LibroElectronico		. >
{

 
public &
LibroComprasNoDomiciliadas )
() *
)* +
{ 	
}
public 
static 
string 
ContenidoTXT )
() *
List* .
<. /&
LibroComprasNoDomiciliadas/ I
>I J
logsK O
)O P
{ 	
string 
lines 
= 
$str 
; 
return 
lines 
; 
} 	
public 
static 
string 
NombreArchivoTXT -
(- .
string. 4"
documentoIdentidadSede5 K
,K L
PeriodoM T
periodoU \
,\ ]
ItemGenerico^ j
librok p
,p q
Listr v
<v w'
LibroComprasNoDomiciliadas	w �
>
� �
logs
� �
)
� �
{ 	
return 
( 
$str 
+ "
documentoIdentidadSede 3
+4 5
periodo6 =
.= >
nombre> D
+E F
$strG K
+L M
libroN S
.S T
CodigoT Z
+[ \
$str] b
+c d
(e f
logsf j
.j k
Countk p
>q r
$nums t
?u v
$strw z
:{ |
$str	} �
)
� �
+
� �
$str
� �
)
� �
;
� �
} 	
public 
static 
string 
NombreArchivoCSV -
(- .
string. 4"
documentoIdentidadSede5 K
,K L
PeriodoM T
periodoU \
,\ ]
ItemGenerico^ j
librok p
,p q
Listr v
<v w'
LibroComprasNoDomiciliadas	w �
>
� �
logs
� �
)
� �
{ 	
return 
( 
$str 
+ "
documentoIdentidadSede 3
+4 5
periodo6 =
.= >
nombre> D
+E F
$strG K
+L M
libroN S
.S T
CodigoT Z
+[ \
$str] b
+c d
(e f
logsf j
.j k
Countk p
>q r
$nums t
?u v
$strw z
:{ |
$str	} �
)
� �
+
� �
$str
� �
)
� �
;
� �
} 	
public 
static 
string 
ContenidoCSV )
() *
List* .
<. /&
LibroComprasNoDomiciliadas/ I
>I J
logsK O
)O P
{ 	
string   
lines   
=   
$str   
;   
lines"" 
+="" 
Environment""  
.""  !
NewLine""! (
;""( )
return-- 
lines-- 
;-- 
}.. 	
}// 
}00 �
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EBookViewModel\Custom\LibroElectronico.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,
EBookViewModel		, :
.		: ;
Custom		; A
{

 
public 

class 
LibroElectronico !
{ 
} 
} ��
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EBookViewModel\Custom\LibroVentasIngresos.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
EBookViewModel, :
.: ;
Custom; A
{		 
public

 

class

 &
DetalleLibroVentasIngresos

 +
:

, -
LibroElectronico

. >
{ 
public 
string 
Periodo 
{ 
get  #
;# $
set% (
;( )
}* +
public
string
CUO
{
get
;
set
;
}
public 
string 
NumeroCorrelativo '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string '
FechaEmisionComprobantePago 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string &
FechaVencimientoOFechaPago 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
string )
TipoComprobantePagoODocumento 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
string 0
$NumeroSerieComprobantePagoODocumento :
{; <
get= @
;@ A
setB E
;E F
}G H
public 
string +
NumeroComprobantePagoODocumento 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
string 
NumeroFinal !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string )
TipoDocumentoIdentidadCliente 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
string +
NumeroDocumentoIdentidadCliente 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
string 
ApellidosYNombres '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string %
ValorFacturadoExportacion /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
string )
BaseImponibleOperacionGravada 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
string *
ImporteTotalOperacionExonerada 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
string )
ImporteTotalOperacionInafecta 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
string $
ImpuestoSelectivoConsumo .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
string =
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipal G
{H I
getJ M
;M N
setO R
;R S
}T U
public 
string B
6BaseImponibleOperacionGravadaImpuestoVentasArrozPilado L
{M N
getO R
;R S
setT W
;W X
}Y Z
public 
string %
ImpuestoVentasArrozPilado /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public   
string   B
6OtrosConceptosTributosCargosNoFormanParteBaseImponible   L
{  M N
get  O R
;  R S
set  T W
;  W X
}  Y Z
public!! 
string!! '
ImporteTotalComprobantePago!! 1
{!!2 3
get!!4 7
;!!7 8
set!!9 <
;!!< =
}!!> ?
public"" 
string"" 

TipoCambio""  
{""! "
get""# &
;""& '
set""( +
;""+ ,
}""- .
public## 
string## 4
(FechaEmisionComprobantePagoQueSeModifica## >
{##? @
get##A D
;##D E
set##F I
;##I J
}##K L
public$$ 
string$$ ,
 TipoComprobantePagoQueSeModifica$$ 6
{$$7 8
get$$9 <
;$$< =
set$$> A
;$$A B
}$$C D
public%% 
string%% M
ANumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera%% V
{%%W X
get%%Y \
;%%\ ]
set%%^ a
;%%a b
}%%c d
public&& 
string&& 8
,NumeroComprobantePagoQueSeModificaNúmeroDUA&& A
{&&B C
get&&D G
;&&G H
set&&I L
;&&L M
}&&N O
public'' 
string'' <
0EstadoIdentificaOportunidadAnotaciónIndicación'' D
{''E F
get''G J
;''J K
set''L O
;''O P
}''Q R
public(( 
string(( "
DescuentoBaseImponible(( ,
{((- .
get((/ 2
;((2 3
set((4 7
;((7 8
}((9 :
public)) 
string)) E
9DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal)) N
{))O P
get))Q T
;))T U
set))V Y
;))Y Z
}))[ \
public** 
string** 
CodigoMoneda** "
{**# $
get**% (
;**( )
set*** -
;**- .
}**/ 0
public++ 
string++ 9
-IdentificacionContratoColaboracionEmpresarial++ C
{++D E
get++F I
;++I J
set++K N
;++N O
}++P Q
public,, 
string,, 

ErrorTipo1,,  
{,,! "
get,,# &
;,,& '
set,,( +
;,,+ ,
},,- .
public-- 
string-- 9
-IndicadorComprobantesPagoCanceladosMediosPago-- C
{--D E
get--F I
;--I J
set--K N
;--N O
}--P Q
public.. 
string.. 
Icbper.. 
{.. 
get.. "
;.." #
set..$ '
;..' (
}..) *
public11 &
DetalleLibroVentasIngresos11 )
(11) *
)11* +
{22 	
}33 	
public55 &
DetalleLibroVentasIngresos55 )
(55) *
OperacionDeVenta55* :
operacionVenta55; I
,55I J
Periodo55K R
periodo55S Z
,55Z [
int55\ _
correlativo55` k
)55k l
{66 	
this77 
.77 
Periodo77 
=77 
periodo77 "
.77" #
anio77# '
+77( )
periodo77* 1
.771 2
mes772 5
+776 7
$str778 <
;77< =
this88 
.88 
CUO88 
=88 
$str88 
+88 
(88 
correlativo88 )
)88) *
.88* +
ToString88+ 3
(883 4
)884 5
;885 6
this99 
.99 
NumeroCorrelativo99 "
=99# $
$str99% (
+99) *
(99+ ,
correlativo99, 7
)997 8
.998 9
ToString999 A
(99A B
$str99B H
)99H I
;99I J
this:: 
.:: '
FechaEmisionComprobantePago:: ,
=::- .
operacionVenta::/ =
.::= >
FechaEmision::> J
.::J K
ToString::K S
(::S T
$str::T `
)::` a
;::a b
this;; 
.;; &
FechaVencimientoOFechaPago;; +
=;;, -
operacionVenta;;. <
.;;< =
FechaVencimiento;;= M
.;;M N
ToString;;N V
(;;V W
$str;;W c
);;c d
;;;d e
this<< 
.<< )
TipoComprobantePagoODocumento<< .
=<</ 0
operacionVenta<<1 ?
.<<? @
Comprobante<<@ K
(<<K L
)<<L M
.<<M N

CodigoTipo<<N X
;<<X Y
this== 
.== 0
$NumeroSerieComprobantePagoODocumento== 5
===6 7
operacionVenta==8 F
.==F G
Comprobante==G R
(==R S
)==S T
.==T U

;==b c
this>> 
.>> +
NumeroComprobantePagoODocumento>> 0
=>>1 2
operacionVenta>>3 A
.>>A B
Comprobante>>B M
(>>M N
)>>N O
.>>O P
NumeroDeComprobante>>P c
.>>c d
ToString>>d l
(>>l m
)>>m n
;>>n o
this@@ 
.@@ 
NumeroFinal@@ 
=@@ 
$str@@ !
;@@! "
thisAA 
.AA )
TipoDocumentoIdentidadClienteAA .
=AA/ 0
operacionVentaAA1 ?
.AA? @4
(CodigoSunatTipoDocumentoIdentidadClienteAA@ h
;AAh i
thisBB 
.BB +
NumeroDocumentoIdentidadClienteBB 0
=BB1 2
operacionVentaBB3 A
.BBA B+
NumeroDocumentoIdentidadClienteBBB a
;BBa b
thisCC 
.CC 
ApellidosYNombresCC "
=CC# $
operacionVentaCC% 3
.CC3 4
ApellidosYNombresCC4 E
==CCF H
$strCCI K
?CCL M
$strCCN _
:CC` a
operacionVentaCCb p
.CCp q
ApellidosYNombres	CCq �
;
CC� �
thisDD 
.DD %
ValorFacturadoExportacionDD *
=DD+ ,
$strDD- /
;DD/ 0
thisEE 
.EE )
BaseImponibleOperacionGravadaEE .
=EE/ 0
operacionVentaEE1 ?
.EE? @1
%BaseImponibleOperacionGravadaConSignoEE@ e
!=EEf h
$numEEi j
&&EEk m
!EEn o
operacionVentaEEo }
.EE} ~
EsInvalidada	EE~ �
?
EE� �
operacionVenta
EE� �
.
EE� �3
%BaseImponibleOperacionGravadaConSigno
EE� �
.
EE� �
ToString
EE� �
(
EE� �
$str
EE� �
)
EE� �
:
EE� �
$str
EE� �
;
EE� �
thisHH 
.HH "
DescuentoBaseImponibleHH '
=HH( )
operacionVentaHH* 8
.HH8 9"
DescuentoBaseImponibleHH9 O
!=HHP R
$numHHS T
&&HHU W
!HHX Y
operacionVentaHHY g
.HHg h
EsInvalidadaHHh t
?HHu v
operacionVenta	HHw �
.
HH� �$
DescuentoBaseImponible
HH� �
.
HH� �
ToString
HH� �
(
HH� �
$str
HH� �
)
HH� �
:
HH� �
$str
HH� �
;
HH� �
thisII 
.II =
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipalII B
=IIC D
operacionVentaIIE S
.IIS T>
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipal	IIT �
!=
II� �
$num
II� �
&&
II� �
!
II� �
operacionVenta
II� �
.
II� �
EsInvalidada
II� �
?
II� �
operacionVenta
II� �
.
II� �?
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipal
II� �
.
II� �
ToString
II� �
(
II� �
$str
II� �
)
II� �
:
II� �
$str
II� �
;
II� �
thisJJ 
.JJ E
9DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipalJJ I
=JJJ K
operacionVentaJJL Z
.JJZ [F
9DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal	JJ[ �
!=
JJ� �
$num
JJ� �
&&
JJ� �
!
JJ� �
operacionVenta
JJ� �
.
JJ� �
EsInvalidada
JJ� �
?
JJ� �
operacionVenta
JJ� �
.
JJ� �G
9DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal
JJ� �
.
JJ� �
ToString
JJ� �
(
JJ� �
$str
JJ� �
)
JJ� �
:
JJ� �
$str
JJ� �
;
JJ� �
thisLL 
.LL *
ImporteTotalOperacionExoneradaLL /
=LL0 1
operacionVentaLL2 @
.LL@ A2
&ImporteTotalOperacionExoneradaConSignoLLA g
!=LLh j
$numLLk l
&&LLm o
!LLp q
operacionVentaLLq 
.	LL �
EsInvalidada
LL� �
?
LL� �
operacionVenta
LL� �
.
LL� �4
&ImporteTotalOperacionExoneradaConSigno
LL� �
.
LL� �
ToString
LL� �
(
LL� �
$str
LL� �
)
LL� �
:
LL� �
$str
LL� �
;
LL� �
thisMM 
.MM )
ImporteTotalOperacionInafectaMM .
=MM/ 0
operacionVentaMM1 ?
.MM? @)
ImporteTotalOperacionInafectaMM@ ]
!=MM^ `
$numMMa b
&&MMc e
!MMf g
operacionVentaMMg u
.MMu v
EsInvalidada	MMv �
?
MM� �
operacionVenta
MM� �
.
MM� �+
ImporteTotalOperacionInafecta
MM� �
.
MM� �
ToString
MM� �
(
MM� �
$str
MM� �
)
MM� �
:
MM� �
$str
MM� �
;
MM� �
thisNN 
.NN $
ImpuestoSelectivoConsumoNN )
=NN* +
(NN, -
operacionVentaNN- ;
.NN; <$
ImpuestoSelectivoConsumoNN< T
!=NNU W
$numNNX Y
&&NNZ \
!NN] ^
operacionVentaNN^ l
.NNl m
EsInvalidadaNNm y
?NNz {
operacionVenta	NN| �
.
NN� �&
ImpuestoSelectivoConsumo
NN� �
.
NN� �
ToString
NN� �
(
NN� �
$str
NN� �
)
NN� �
:
NN� �
$str
NN� �
)
NN� �
;
NN� �
thisOO 
.OO B
6BaseImponibleOperacionGravadaImpuestoVentasArrozPiladoOO G
=OOH I
operacionVentaOOJ X
.OOX YC
6BaseImponibleOperacionGravadaImpuestoVentasArrozPilado	OOY �
!=
OO� �
$num
OO� �
&&
OO� �
!
OO� �
operacionVenta
OO� �
.
OO� �
EsInvalidada
OO� �
?
OO� �
operacionVenta
OO� �
.
OO� �D
6BaseImponibleOperacionGravadaImpuestoVentasArrozPilado
OO� �
.
OO� �
ToString
OO� �
(
OO� �
$str
OO� �
)
OO� �
:
OO� �
$str
OO� �
;
OO� �
thisPP 
.PP %
ImpuestoVentasArrozPiladoPP *
=PP+ ,
operacionVentaPP- ;
.PP; <%
ImpuestoVentasArrozPiladoPP< U
!=PPV X
$numPPY Z
&&PP[ ]
!PP^ _
operacionVentaPP_ m
.PPm n
EsInvalidadaPPn z
?PP{ |
operacionVenta	PP} �
.
PP� �'
ImpuestoVentasArrozPilado
PP� �
.
PP� �
ToString
PP� �
(
PP� �
$str
PP� �
)
PP� �
:
PP� �
$str
PP� �
;
PP� �
thisQQ 
.QQ B
6OtrosConceptosTributosCargosNoFormanParteBaseImponibleQQ G
=QQH I
operacionVentaQQJ X
.QQX YC
6OtrosConceptosTributosCargosNoFormanParteBaseImponible	QQY �
!=
QQ� �
$num
QQ� �
&&
QQ� �
!
QQ� �
operacionVenta
QQ� �
.
QQ� �
EsInvalidada
QQ� �
?
QQ� �
operacionVenta
QQ� �
.
QQ� �D
6OtrosConceptosTributosCargosNoFormanParteBaseImponible
QQ� �
.
QQ� �
ToString
QQ� �
(
QQ� �
$str
QQ� �
)
QQ� �
:
QQ� �
$str
QQ� �
;
QQ� �
thisRR 
.RR '
ImporteTotalComprobantePagoRR ,
=RR- .
(SS 
operacionVentaSS 
.SS  '
ImporteTotalComprobantePagoSS  ;
!=SS< >
$numSS? @
&&SSA C
!SSD E
operacionVentaSSE S
.SSS T
EsInvalidadaSST `
?SSa b
operacionVentaSSc q
.SSq r(
ImporteTotalComprobantePago	SSr �
.
SS� �
ToString
SS� �
(
SS� �
$str
SS� �
)
SS� �
:
SS� �
$str
SS� �
)
SS� �
;
SS� �
thisTT 
.TT 
CodigoMonedaTT 
=TT 
operacionVentaTT  .
.TT. /
CodigoMonedaTT/ ;
(TT; <
)TT< =
;TT= >
thisUU 
.UU 

TipoCambioUU 
=UU 
operacionVentaUU ,
.UU, -
TipoDeCambioUU- 9
.UU9 :
ToStringUU: B
(UUB C
$strUUC J
)UUJ K
;UUK L
thisWW 
.WW 4
(FechaEmisionComprobantePagoQueSeModificaWW 9
=WW: ;
(WW< =
operacionVentaWW= K
.WWK L4
(FechaEmisionComprobantePagoQueSeModificaWWL t
!=WWu w
nullWWx |
?WW} ~
(	WW �
(
WW� �
DateTime
WW� �
)
WW� �
operacionVenta
WW� �
.
WW� �6
(FechaEmisionComprobantePagoQueSeModifica
WW� �
)
WW� �
.
WW� �
ToString
WW� �
(
WW� �
$str
WW� �
)
WW� �
:
WW� �
$str
WW� �
)
WW� �
;
WW� �
thisXX 
.XX ,
 TipoComprobantePagoQueSeModificaXX 1
=XX2 3
operacionVentaXX4 B
.XXB C,
 TipoComprobantePagoQueSeModificaXXC c
;XXc d
thisYY 
.YY M
ANumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduaneraYY Q
=YYR S
operacionVentaYYT b
.YYb cN
ANumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera	YYc �
;
YY� �
thisZZ 
.ZZ 8
,NumeroComprobantePagoQueSeModificaNúmeroDUAZZ <
=ZZ= >
operacionVentaZZ? M
.ZZM N8
,NumeroComprobantePagoQueSeModificaNúmeroDUAZZN y
>ZZz {
$numZZ| }
?ZZ~ 
operacionVenta
ZZ� �
.
ZZ� �:
,NumeroComprobantePagoQueSeModificaNúmeroDUA
ZZ� �
.
ZZ� �
ToString
ZZ� �
(
ZZ� �
)
ZZ� �
:
ZZ� �
$str
ZZ� �
;
ZZ� �
this[[ 
.[[ 9
-IdentificacionContratoColaboracionEmpresarial[[ >
=[[? @
$str[[A C
;[[C D
this\\ 
.\\ 

ErrorTipo1\\ 
=\\ 
$str\\  
;\\  !
this]] 
.]] 9
-IndicadorComprobantesPagoCanceladosMediosPago]] >
=]]? @
$str]]A C
;]]C D
this^^ 
.^^ <
0EstadoIdentificaOportunidadAnotaciónIndicación^^ ?
=^^@ A
operacionVenta^^B P
.^^P Q
EsInvalidada^^Q ]
?^^^ _
$num^^` a
.^^a b
ToString^^b j
(^^j k
)^^k l
:^^m n
$num^^o p
.^^p q
ToString^^q y
(^^y z
)^^z {
;^^{ |
this__ 
.__ 
Icbper__ 
=__ 
operacionVenta__ (
.__( )
Icbper__) /
(__/ 0
)__0 1
.__1 2
ToString__2 :
(__: ;
$str__; A
)__A B
;__B C
}`` 	
publicbb 
staticbb 
Listbb 
<bb &
DetalleLibroVentasIngresosbb 5
>bb5 6
Convertbb7 >
(bb> ?
Listbb? C
<bbC D
OperacionDeVentabbD T
>bbT U
operacionesDeVentasbbV i
,bbi j
Periodobbk r
periodobbs z
)bbz {
{cc 	
Listdd 
<dd &
DetalleLibroVentasIngresosdd +
>dd+ ,
ebookVentasIngresosdd- @
=ddA B
newddC F
ListddG K
<ddK L&
DetalleLibroVentasIngresosddL f
>ddf g
(ddg h
)ddh i
;ddi j
intee 
correlativoee 
=ee 
$numee 
;ee  
foreachff 
(ff 
varff 
itemff 
inff  
operacionesDeVentasff! 4
)ff4 5
{gg 
ebookVentasIngresoshh #
.hh# $
Addhh$ '
(hh' (
newhh( +&
DetalleLibroVentasIngresoshh, F
(hhF G
itemhhG K
,hhK L
periodohhM T
,hhT U
++hhV X
correlativohhX c
)hhc d
)hhd e
;hhe f
}jj 
returnkk 
ebookVentasIngresoskk &
;kk& '
}ll 	
publicnn 
staticnn 
stringnn 
ContenidoFormatoPLEnn 0
(nn0 1
Listnn1 5
<nn5 6&
DetalleLibroVentasIngresosnn6 P
>nnP Q
logsnnR V
,nnV W
stringnnX ^
	separadornn_ h
)nnh i
{oo 	
stringpp 
linespp 
=pp 
$strpp 
;pp 
foreachqq 
(qq 
varqq 
logqq 
inqq 
logsqq  $
)qq$ %
{rr 
linesss 
+=ss 
logss 
.ss 
Periodoss $
+ss% &
	separadorss' 0
+tt 
logtt 
.tt 
CUOtt 
+tt 
	separadortt  )
+uu 
loguu 
.uu 
NumeroCorrelativouu +
+uu, -
	separadoruu. 7
+vv 
logvv 
.vv '
FechaEmisionComprobantePagovv 5
+vv6 7
	separadorvv8 A
+ww 
logww 
.ww &
FechaVencimientoOFechaPagoww 4
+ww5 6
	separadorww7 @
+wwA B
logwwC F
.wwF G)
TipoComprobantePagoODocumentowwG d
+wwe f
	separadorwwg p
+xx 
logxx 
.xx 0
$NumeroSerieComprobantePagoODocumentoxx >
+xx? @
	separadorxxA J
+yy 
logyy 
.yy +
NumeroComprobantePagoODocumentoyy 9
+yy: ;
	separadoryy< E
+zz 
logzz 
.zz 
NumeroFinalzz %
+zz& '
	separadorzz( 1
+{{ 
log{{ 
.{{ )
TipoDocumentoIdentidadCliente{{ 7
+{{8 9
	separador{{: C
+|| 
log|| 
.|| +
NumeroDocumentoIdentidadCliente|| 9
+||: ;
	separador||< E
+}} 
log}} 
.}} 
ApellidosYNombres}} +
+}}, -
	separador}}. 7
+~~ 
log~~ 
.~~ %
ValorFacturadoExportacion~~ 3
+~~4 5
	separador~~6 ?
+ 
log 
. )
BaseImponibleOperacionGravada 7
+8 9
	separador: C
+
�� 
log
�� 
.
�� $
DescuentoBaseImponible
�� 0
+
��1 2
	separador
��3 <
+
�� 
log
�� 
.
�� ?
1ImpuestoGeneralVentasYOImpuestoPromocionMunicipal
�� K
+
��L M
	separador
��N W
+
�� 
log
�� 
.
�� G
9DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal
�� R
+
��S T
	separador
��U ^
+
�� 
log
�� 
.
�� ,
ImporteTotalOperacionExonerada
�� 8
+
��9 :
	separador
��; D
+
�� 
log
�� 
.
�� +
ImporteTotalOperacionInafecta
�� 7
+
��8 9
	separador
��: C
+
�� 
log
�� 
.
�� &
ImpuestoSelectivoConsumo
�� 2
+
��3 4
	separador
��5 >
+
�� 
log
�� 
.
�� D
6BaseImponibleOperacionGravadaImpuestoVentasArrozPilado
�� P
+
��Q R
	separador
��S \
+
�� 
log
�� 
.
�� '
ImpuestoVentasArrozPilado
�� 3
+
��4 5
	separador
��6 ?
+
�� 
log
�� 
.
�� 
Icbper
��  
+
��! "
	separador
��# ,
+
�� 
log
�� 
.
�� D
6OtrosConceptosTributosCargosNoFormanParteBaseImponible
�� P
+
��Q R
	separador
��S \
+
�� 
log
�� 
.
�� )
ImporteTotalComprobantePago
�� 5
+
��6 7
	separador
��8 A
+
�� 
log
�� 
.
�� 
CodigoMoneda
�� &
+
��' (
	separador
��) 2
+
�� 
log
�� 
.
�� 

TipoCambio
�� $
+
��% &
	separador
��' 0
+
�� 
log
�� 
.
�� 6
(FechaEmisionComprobantePagoQueSeModifica
�� B
+
��C D
	separador
��E N
+
�� 
log
�� 
.
�� .
 TipoComprobantePagoQueSeModifica
�� :
+
��; <
	separador
��= F
+
�� 
log
�� 
.
�� O
ANumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera
�� Z
+
��[ \
	separador
��] f
+
�� 
log
�� 
.
�� :
,NumeroComprobantePagoQueSeModificaNúmeroDUA
�� E
+
��F G
	separador
��H Q
+
�� 
log
�� 
.
�� ;
-IdentificacionContratoColaboracionEmpresarial
�� G
+
��H I
	separador
��J S
+
�� 
log
�� 
.
�� 

ErrorTipo1
�� $
+
��% &
	separador
��' 0
+
�� 
log
�� 
.
�� ;
-IndicadorComprobantesPagoCanceladosMediosPago
�� G
+
��H I
	separador
��J S
+
�� 
log
�� 
.
�� >
0EstadoIdentificaOportunidadAnotaciónIndicación
�� H
;
��H I
if
�� 
(
�� 
	separador
�� 
.
�� 
Equals
�� $
(
��$ %
$str
��% (
)
��( )
)
��) *
lines
��+ 0
+=
��1 3
	separador
��4 =
;
��= >
lines
�� 
+=
�� 
Environment
�� $
.
��$ %
NewLine
��% ,
;
��, -
}
�� 
return
�� 
lines
�� 
;
�� 
}
�� 	
public
�� 
static
�� 
string
�� 
ContenidoTXT
�� )
(
��) *
List
��* .
<
��. /(
DetalleLibroVentasIngresos
��/ I
>
��I J
logs
��K O
)
��O P
{
�� 	
string
�� 
lines
�� 
=
�� !
ContenidoFormatoPLE
�� .
(
��. /
logs
��/ 3
,
��3 4
$str
��5 8
)
��8 9
;
��9 :
return
�� 
lines
�� 
;
�� 
}
�� 	
public
�� 
static
�� 
string
�� 
NombreArchivoTXT
�� -
(
��- .
string
��. 4$
documentoIdentidadSede
��5 K
,
��K L
Periodo
��M T
periodo
��U \
,
��\ ]
ItemGenerico
��^ j
libro
��k p
,
��p q
List
��r v
<
��v w)
DetalleLibroVentasIngresos��w �
>��� �
logs��� �
)��� �
{
�� 	
return
�� 
(
�� 
$str
�� 
+
�� $
documentoIdentidadSede
�� 3
+
��4 5
periodo
��6 =
.
��= >
nombre
��> D
+
��E F
$str
��G K
+
��L M
libro
��N S
.
��S T
Codigo
��T Z
+
��[ \
$str
��] b
+
��c d
(
��e f
logs
��f j
.
��j k
Count
��k p
>
��q r
$num
��s t
?
��u v
$str
��w z
:
��{ |
$str��} �
)��� �
+��� �
$str��� �
)��� �
;��� �
}
�� 	
public
�� 
static
�� 
string
�� 
NombreArchivoCSV
�� -
(
��- .
string
��. 4$
documentoIdentidadSede
��5 K
,
��K L
Periodo
��M T
periodo
��U \
,
��\ ]
ItemGenerico
��^ j
libro
��k p
,
��p q
List
��r v
<
��v w)
DetalleLibroVentasIngresos��w �
>��� �
logs��� �
)��� �
{
�� 	
return
�� 
(
�� 
$str
�� 
+
�� $
documentoIdentidadSede
�� 3
+
��4 5
periodo
��6 =
.
��= >
nombre
��> D
+
��E F
$str
��G K
+
��L M
libro
��N S
.
��S T
Codigo
��T Z
+
��[ \
$str
��] b
+
��c d
(
��e f
logs
��f j
.
��j k
Count
��k p
>
��q r
$num
��s t
?
��u v
$str
��w z
:
��{ |
$str��} �
)��� �
+��� �
$str��� �
)��� �
;��� �
}
�� 	
public
�� 
static
�� 
string
��  
CabeceraArchivoCSV
�� /
{
�� 	
get
�� 
=>
�� 
$str�� �
;��� �
}
�� 	
public
�� 
static
�� 
string
�� 
ContenidoCSV
�� )
(
��) *
List
��* .
<
��. /(
DetalleLibroVentasIngresos
��/ I
>
��I J
logs
��K O
)
��O P
{
�� 	
string
�� 
lines
�� 
=
�� 
$str
�� 
;
�� 
lines
�� 
+=
��  
CabeceraArchivoCSV
�� '
;
��' (
lines
�� 
+=
�� 
Environment
��  
.
��  !
NewLine
��! (
;
��( )
foreach
�� 
(
�� 
var
�� 
log
�� 
in
�� 
logs
��  $
)
��$ %
{
�� 
var
�� 3
%existeDetailDocumentoIdentidadCliente
�� 9
=
��: ;
log
��< ?
.
��? @-
NumeroDocumentoIdentidadCliente
��@ _
!=
��` b
$str
��c e
;
��e f
if
�� 
(
�� 3
%existeDetailDocumentoIdentidadCliente
�� 9
)
��9 :
{
�� 
log
�� 
.
�� -
NumeroDocumentoIdentidadCliente
�� 7
=
��8 9
$str
��: >
+
��? @
log
��A D
.
��D E-
NumeroDocumentoIdentidadCliente
��E d
;
��d e
}
�� 
}
�� 
lines
�� 
+=
�� !
ContenidoFormatoPLE
�� (
(
��( )
logs
��) -
,
��- .
$str
��/ 2
)
��2 3
;
��3 4
return
�� 
lines
�� 
;
�� 
}
�� 	
}
�� 
}�� �
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EFactura\DocumentoBajaAvanzado.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
EFactura( 0
{ 
class 	!
DocumentoBajaAvanzado
 
:  !

{		 
public !
DocumentoBajaAvanzado $
($ %
	Documento% .
	documento/ 8
,8 9
int9 <
idLinea= D
)D E
{ 	
try
{ 
DocumentoElectronico $(
documentoElectronicoAvanzado% A
=B C
JsonConvertD O
.O P
DeserializeObjectP a
<a b 
DocumentoElectronicob v
>v w
(w x
Encoding	x �
.
� �
UTF8
� �
.
� �
	GetString
� �
(
� �
	documento
� �
.
� �
Binario
� �
.
� �
archivoBinario
� �
)
� �
)
� �
;
� �

( (
documentoElectronicoAvanzado :
,: ;
idLinea; B
)B C
;C D
} 
catch 
( 
	Exception 
ex 
)  
{ 
throw 
ex 
; 
} 
} 	
private 
void 

(" # 
DocumentoElectronico# 7 
documentoElectronico8 L
,L M
intN Q
idLineaR Y
)Y Z
{ 	
this 
. 
Id 
= 
idLinea 
; 
this 
. 

=   
documentoElectronico! 5
.5 6

;C D
this 
. 
Serie 
=  
documentoElectronico -
.- .
IdDocumento. 9
.9 :
Split: ?
(? @
$char@ C
)C D
[D E
$numE F
]F G
;G H
this 
. 
Correlativo 
=  
documentoElectronico 3
.3 4
IdDocumento4 ?
.? @
Split@ E
(E F
$charF I
)I J
[J K
$numK L
]L M
;M N
this 
. 

MotivoBaja 
= 
$str 8
+8 9 
documentoElectronico: N
.N O
IdDocumentoO Z
;Z [
}   	
}## 
}$$ �
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EFactura\DocumentoImprimible.cs
	namespace 	
Tsp
 
.
FacturacionElectronica $
.$ %
Modelo% +
{ 
public 

class 
DocumentoImprimible $
{ 
public 
string 
	Contenido 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Hashcode 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
DocumentoImprimible		 "
(		" #
string		# )
	contenido		* 3
,		3 4
string		5 ;
hashcode		< D
)		D E
{

 	
	Contenido 
= 
	contenido !
;! "
Hashcode 
= 
hashcode 
;  
}
} 
} �/
WD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EFactura\EnvioGuiaRemision.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,
EFactura		, 4
{

 
public 

class 
TokenEnvioRequest "
{ 
public
string

grant_type
{
get
;
set
;
}
public 
string 
scope 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
	client_id 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
username 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
password 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
public 

class 
TokenEnvioResponse #
{ 
public 
string 
access_token "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 

token_type  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 

expires_in 
{ 
get  #
;# $
set% (
;( )
}* +
} 
public 

class !
EnvioDocumentoRequest &
{ 
public 
Archivo 
archivo 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
public   

class   
Archivo   
{!! 
public"" 
string"" 

nomArchivo""  
{""! "
get""# &
;""& '
set""( +
;""+ ,
}""- .
public## 
string## 
	arcGreZip## 
{##  !
get##" %
;##% &
set##' *
;##* +
}##, -
public$$ 
string$$ 
hashZip$$ 
{$$ 
get$$  #
;$$# $
set$$% (
;$$( )
}$$* +
}%% 
public&& 

class&& "
EnvioDocumentoResponse&& '
{'' 
public(( 
string(( 
	numTicket(( 
{((  !
get((" %
;((% &
set((' *
;((* +
}((, -
public)) 
DateTime)) 
fecRecepcion)) $
{))% &
get))' *
;))* +
set)), /
;))/ 0
}))1 2
}** 
public,, 

class,, +
RespuestaEnvioDocumentoResponse,, 0
{-- 
public.. 
string.. 
codRespuesta.. "
{..# $
get..% (
;..( )
set..* -
;..- .
}../ 0
public// '
ErrorEnvioDocumentoResponse// *
error//+ 0
{//1 2
get//3 6
;//6 7
set//8 ;
;//; <
}//= >
public00 
string00 
arcCdr00 
{00 
get00 "
;00" #
set00$ '
;00' (
}00) *
public11 
string11 
indCdrGenerado11 $
{11% &
get11' *
;11* +
set11, /
;11/ 0
}111 2
public33 #
EnviarDocumentoResponse33 &
	Convertir33' 0
(330 1
)331 2
{44 	
return55 
new55 #
EnviarDocumentoResponse55 .
{66 
Exito77 
=77 
true77 
,77 
CodigoRespuesta88 
=88  !
codRespuesta88" .
==88/ 1*
FacturacionElectronicaSettings882 P
.88P Q
Default88Q X
.88X Y/
#CodigoApiErrorRespuestaGuiaRemision88Y |
?88} ~
error	88 �
.
88� �
numError
88� �
:
88� �
codRespuesta
88� �
,
88� �
MensajeRespuesta99  
=99! "
codRespuesta99# /
==990 2*
FacturacionElectronicaSettings993 Q
.99Q R
Default99R Y
.99Y Z/
#CodigoApiErrorRespuestaGuiaRemision99Z }
?99~ 
error
99� �
.
99� �
desError
99� �
:
99� �
(
99� �
codRespuesta
99� �
==
99� �,
FacturacionElectronicaSettings
99� �
.
99� �
Default
99� �
.
99� �5
'CodigoApiEnProcesoRespuestaGuiaRemision
99� �
?
99� �
$str
99� �
:
99� �
$str
99� �
)
99� �
,
99� �
TramaZipCdr:: 
=:: 
codRespuesta:: *
==::+ -*
FacturacionElectronicaSettings::. L
.::L M
Default::M T
.::T U3
'CodigoApiEnProcesoRespuestaGuiaRemision::U |
?::} ~
null	:: �
:
::� �
arcCdr
::� �
,
::� �
NroTicketCdr;; 
=;; 
$str;; !
,;;! "
}<< 
;<<
}== 	
}>> 
public@@ 

class@@ '
ErrorEnvioDocumentoResponse@@ ,
{AA 
publicBB 
stringBB 
numErrorBB 
{BB  
getBB! $
;BB$ %
setBB& )
;BB) *
}BB+ ,
publicCC 
stringCC 
desErrorCC 
{CC  
getCC! $
;CC$ %
setCC& )
;CC) *
}CC+ ,
}DD 
}EE �

QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\EFactura\EnvioLigero.cs
	namespace 	
Tsp
 
.
FacturacionElectronica $
.$ %
Modelo% +
{ 
public		 

class		 
EnvioSimplificado		 "
{

 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
NumeroTicket "
{# $
get% (
;( )
set* -
;- .
}/ 0
public
string
CodigoTipoDocumento
{
get
;
set
;
}
public 
string 
SerieDocumento $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
NumeroDocumento %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
	TipoEnvio 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} �
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\AmbienteHotel.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public 

class 

{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
IdActor 
{ 
get  
;  !
set" %
;% &
}' (
public		 
string		 
Codigo		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
string

 
Nombre

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 
ItemGenerico 
Establecimiento +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
bool 
	EsVigente 
{ 
get  #
;# $
set% (
;( )
}* +
public 

( 
) 
{ 	
} 	
} 
} �
QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Anotacion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public		 

class		 
	Anotacion		 
{

 
public 
string 
Fecha 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Mensaje 
{ 
get  #
;# $
set% (
;( )
}* +
public 
	Anotacion 
( 
) 
{ 
} 
public 
	Anotacion 
( 
string 
fecha  %
,% &
string' -
mensaje. 5
)5 6
{ 	
Fecha 
= 
fecha 
; 
Mensaje 
= 
mensaje 
; 
} 	
} 
} �
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ComprobanteAtencion.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 


 +
.

+ ,

SigesHotel

, 6
{ 
public 

class 
ComprobanteAtencion $
{
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
long 
IdOrdenVenta  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
PuedeDarDeBaja "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
bool 
	DarDeBaja 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
MontoHospedaje %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
	Descuento  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
decimal 

Diferencia !
{" #
get$ '
=>( *
MontoHospedaje+ 9
-: ;
	Descuento< E
;E F
}G H
public 
decimal 

MontoSoles !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
IdTipoComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
bool  
EsComprobanteInterno (
{) *
get+ .
=>/ 1
IdTipoComprobante2 C
==D F
MaestroSettingsG V
.V W
DefaultW ^
.^ _8
+IdDetalleMaestroComprobanteNotaVentaInterna	_ �
;
� �
}
� �
public 
ComprobanteAtencion "
(" #
)# $
{% &
}' (
} 
} �
SD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Complemento.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public 

class 
Complemento 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public		 
string		 
Nombre		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
List

 
<

 
ItemGenerico

  
>

  !
Valores

" )
{

* +
get

, /
;

/ 0
set

1 4
;

4 5
}

6 7
} 
} �
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Configuraciones\ConfiguracionHuesped.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{		 
public

 

sealed

 
class

  
ConfiguracionHuesped

 ,
{ 
private 
static 
readonly  
ConfiguracionHuesped  4
defaultInstance5 D
=E F
newG J 
ConfiguracionHuespedK _
(_ `
)` a
;a b
public
static
ConfiguracionHuesped
Default
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
readonly 
int 
IdRolCliente (
=) *

.8 9
Default9 @
.@ A
IdRolClienteA M
;M N
public 
readonly 
int (
TiempoEsperaBusquedaSelector 8
=9 :
AplicacionSettings; M
.M N
DefaultN U
.U V=
0TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad	V �
;
� �
public 
readonly 
int 0
$MinimoCaracteresBuscarActorComercial @
=A B

.P Q
DefaultQ X
.X YA
4MinimoDeCaracteresParaBuscarEnSelectorActorComercial	Y �
;
� �
public 
readonly 
int 
IdClienteGenerico -
=. /

.= >
Default> E
.E F
IdClienteGenericoF W
;W X
public 
readonly 
int  
IdTipoPersonaNatural 0
=1 2

.@ A
DefaultA H
.H I%
IdTipoActorPersonaNaturalI b
;b c
public 
readonly 
string ;
/MascaraDeVisualizacionValidacionRegistroHuesped N
=O P

.^ _
Default_ f
.f g<
/MascaraDeVisualizacionValidacionRegistroHuesped	g �
;
� �
} 
} �
mD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Configuraciones\ConfiguracionFacturar.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{		 
public

 

sealed

 
class

 !
ConfiguracionFacturar

 -
{ 
private 
static 
readonly !
ConfiguracionFacturar  5
defaultInstance6 E
=F G
newH K!
ConfiguracionFacturarL a
(a b
)b c
;c d
public
static
ConfiguracionFacturar
Default
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
readonly 
int 
TipoDePagoGeneral -
=. /
(0 1
int1 4
)4 5
TipoPagoAtencion5 E
.E F
GeneralF M
;M N
public 
readonly 
int "
TipoDePagoDiferenciado 2
=3 4
(5 6
int6 9
)9 :
TipoPagoAtencion: J
.J K
DiferenciadoK W
;W X
public 
readonly 
int !
IdMedioDePagoEfectivo 1
=2 3
MaestroSettings4 C
.C D
DefaultD K
.K L/
#IdDetalleMaestroMedioDepagoEfectivoL o
;o p
public 
readonly 
int 
	Formato80 %
=& '
(( )
int) ,
), -
FormatoImpresion- =
.= >
_80mm> C
;C D
public 
readonly 
int 
	FormatoA4 %
=& '
(( )
int) ,
), -
FormatoImpresion- =
.= >
A4> @
;@ A
} 
} �
uD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Configuraciones\ConfiguracionEstadoHabitacion.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,

SigesHotel		, 6
.		6 7
Configuraciones		7 F
{

 
public 

sealed 
class )
ConfiguracionEstadoHabitacion 5
{ 
private
static
readonly
ConfiguracionEstadoHabitacion
defaultInstance
=
new
ConfiguracionEstadoHabitacion
(
)
;
public 
static )
ConfiguracionEstadoHabitacion 3
Default4 ;
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
readonly 
int 
Todo  
=! "
(# $
int$ '
)' ( 
EstadoHabitacionEnum( <
.< =
Todo= A
;A B
public 
readonly 
int 
EstadoDisponible ,
=- .
(/ 0
int0 3
)3 4 
EstadoHabitacionEnum4 H
.H I

DisponibleI S
;S T
public 
readonly 
int 

=* +
(, -
int- 0
)0 1 
EstadoHabitacionEnum1 E
.E F
OcupadoF M
;M N
public 
readonly 
int 
EstadoReservado +
=, -
(. /
int/ 2
)2 3 
EstadoHabitacionEnum3 G
.G H
	ReservadoH Q
;Q R
public 
readonly 
int #
EstadoOcupadoDisponible 3
=4 5
(6 7
int7 :
): ; 
EstadoHabitacionEnum; O
.O P
OcupadoDisponibleP a
;a b
} 
} �
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Configuraciones\ConfiguracionReserva.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{		 
public

 

class

  
ConfiguracionReserva

 %
{ 
public 
readonly 
int 
IdRolCliente (
=) *

.8 9
Default9 @
.@ A
IdRolClienteA M
;M N
public
readonly
int
IdRolHuesped
=

.
Default
.
IdRolHuesped
;
public 
readonly 
int %
NumeroDecimalesEnCantidad 5
=6 7
AplicacionSettings8 J
.J K
DefaultK R
.R S%
NumeroDecimalesEnCantidadS l
;l m
public 
readonly 
int (
TiempoEsperaBusquedaSelector 8
=9 :
AplicacionSettings; M
.M N
DefaultN U
.U V=
0TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad	V �
;
� �
public 
readonly 
int 0
$MinimoCaracteresBuscarActorComercial @
=A B

.P Q
DefaultQ X
.X YA
4MinimoDeCaracteresParaBuscarEnSelectorActorComercial	Y �
;
� �
public 
readonly 
int !
IdMedioDePagoEfectivo 1
=2 3
MaestroSettings4 C
.C D
DefaultD K
.K L/
#IdDetalleMaestroMedioDepagoEfectivoL o
;o p
public 
readonly 
int 
IdClienteGenerico -
=. /

.= >
Default> E
.E F
IdClienteGenericoF W
;W X
public 
readonly 
int )
DiasMaximoAnticipacionReserva 9
=: ;

.I J
DefaultJ Q
.Q R)
DiasMaximoAnticipacionReservaR o
;o p
public 
readonly 
int %
DiasMaximoDuracionReserva 5
=6 7

.E F
DefaultF M
.M N%
DiasMaximoDuracionReservaN g
;g h
public 
readonly 
string ;
/MascaraDeVisualizacionValidacionRegistroCliente N
=O P

.^ _
Default_ f
.f g<
/MascaraDeVisualizacionValidacionRegistroCliente	g �
;
� �
public 
string 
FechaActual !
;! "
public 
bool !
AgregarDiaAFechaDesde )
;) *
} 
} �
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\AtencionHotel.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 


 +
.

+ ,

SigesHotel

, 6
{ 
public 

class 

:  
EstadoAtencionHotel! 4
{
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
DateTime 
FechaIngreso $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
FechaIngresoString (
{) *
get+ .
=>/ 1
FechaIngreso2 >
.> ?
ToString? G
(G H
$strH T
)T U
;U V
}W X
public 
DateTime 
FechaSalida #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
FechaSalidaString '
{( )
get* -
=>. 0
FechaSalida1 <
.< =
ToString= E
(E F
$strF R
)R S
;S T
}U V
public 
int 
Noches 
{ 
get 
;  
set! $
;$ %
}& '
public 
decimal 
PrecioUnitario %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

Habitacion 

Habitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
IEnumerable 
< 
Huesped "
>" #
	Huespedes$ -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
	Anotacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< 
	Anotacion 
> 
Anotaciones *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 
AnotacionesJson %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 

( 
) 
{  
}! "
} 
} �
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\MensajesTransaccion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{ 
public		 

class		 
MensajesTransaccion		 $
{

 
public 
string )
MensajeRegistrarAtencionHotel 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
string )
MensajeConfirmarAtencionHotel 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
}
} �.
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\EstadoAtencionHotel.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,

SigesHotel		, 6
{

 
public 

class 
EstadoAtencionHotel $
{ 
public 
long 

IdAuxiliar 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
	Facturado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
bool 
TieneFacturacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
bool 
FacturadoGlobal #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 

ItemEstado 
EstadoActual &
{' (
get) ,
=>- /
Estados0 7
==8 :
null; ?
?@ A
newB E

ItemEstadoF P
(P Q
)Q R
:S T
EstadosU \
.\ ]
Except] c
(c d
Estadosd k
.k l
Wherel q
(q r
er s
=>t v
ew x
.x y
Idy {
==| ~
MaestroSettings	 �
.
� �
Default
� �
.
� �-
IdDetalleMaestroEstadoFacturado
� �
||
� �
e
� �
.
� �
Id
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �-
IdDetalleMaestroEstadoIncidente
� �
)
� �
)
� �
.
� �
Last
� �
(
� �
)
� �
;
� �
}
� �
public 

ItemEstado 
EstadoEventoFinal +
{, -
get. 1
=>2 4
Estados5 <
=== ?
null@ D
?E F
newG J

ItemEstadoK U
(U V
)V W
:X Y
EstadosZ a
.a b
Lastb f
(f g
)g h
;h i
}j k
public 
List 
< 

ItemEstado 
> 
Estados  '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
bool 

{" #
get$ '
=>( *
EstadoActual+ 7
.7 8
Id8 :
!=; =
MaestroSettings> M
.M N
DefaultN U
.U V,
 IdDetalleMaestroEstadoRegistradoV v
&&w y
EstadoActual	z �
.
� �
Id
� �
!=
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �+
IdDetalleMaestroEstadoAnulado
� �
;
� �
}
� �
public 
bool 
PuedeConfirmar "
{# $
get% (
=>) +
EstadoActual, 8
.8 9
Id9 ;
==< >
MaestroSettings? N
.N O
DefaultO V
.V W,
 IdDetalleMaestroEstadoRegistradoW w
;w x
}y z
public 
bool 
PuedeCheckIn  
{! "
get# &
=>' )
EstadoActual* 6
.6 7
Id7 9
==: <
MaestroSettings= L
.L M
DefaultM T
.T U,
 IdDetalleMaestroEstadoConfirmadoU u
;u v
}w x
public   
bool   

{  " #
get  $ '
=>  ( *
EstadoActual  + 7
.  7 8
Id  8 :
==  ; =
MaestroSettings  > M
.  M N
Default  N U
.  U V+
IdDetalleMaestroEstadoCheckedIn  V u
||  v x
EstadoActual	  y �
.
  � �
Id
  � �
==
  � �
MaestroSettings
  � �
.
  � �
Default
  � �
.
  � �3
%IdDetalleMaestroEstadoEntradaCambiado
  � �
;
  � �
}
  � �
public!! 
bool!! 
PuedeAnular!! 
{!!  !
get!!" %
=>!!& (
EstadoActual!!) 5
.!!5 6
Id!!6 8
==!!9 ;
MaestroSettings!!< K
.!!K L
Default!!L S
.!!S T,
 IdDetalleMaestroEstadoRegistrado!!T t
||!!u w
EstadoActual	!!x �
.
!!� �
Id
!!� �
==
!!� �
MaestroSettings
!!� �
.
!!� �
Default
!!� �
.
!!� �.
 IdDetalleMaestroEstadoConfirmado
!!� �
;
!!� �
}
!!� �
public"" 
bool"" "
PuedeCambiarHabitacion"" *
{""+ ,
get""- 0
=>""1 3
EstadoActual""4 @
.""@ A
Id""A C
==""D F
MaestroSettings""G V
.""V W
Default""W ^
.""^ _,
 IdDetalleMaestroEstadoConfirmado""_ 
||
""� �
EstadoActual
""� �
.
""� �
Id
""� �
==
""� �
MaestroSettings
""� �
.
""� �
Default
""� �
.
""� �-
IdDetalleMaestroEstadoCheckedIn
""� �
||
""� �
EstadoActual
""� �
.
""� �
Id
""� �
==
""� �
MaestroSettings
""� �
.
""� �
Default
""� �
.
""� �3
%IdDetalleMaestroEstadoEntradaCambiado
""� �
;
""� �
}
""� �
public$$ 
EstadoAtencionHotel$$ "
($$" #
)$$# $
{$$% &
}$$' (
}%% 
}&& �+
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\EstadoHabitacionEnPlanificador.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,

SigesHotel		, 6
{

 
public 

class *
EstadoHabitacionEnPlanificador /
{
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
FechaString !
{" #
get$ '
=>( *
Fecha+ 0
.0 1
ToString1 9
(9 :
$str: F
)F G
;G H
}I J
public 
string  
FechaSiguienteString *
{+ ,
get- 0
=>1 3
Fecha4 9
.9 :
AddDays: A
(A B
$numB C
)C D
.D E
ToStringE M
(M N
$strN Z
)Z [
;[ \
}] ^
public 
long 
IdAtencionMacro #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
IdEstado 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
EstaDisponible "
{# $
get% (
=>) +
IdEstado, 4
==5 7
MaestroSettings8 G
.G H
DefaultH O
.O P,
 IdDetalleMaestroEstadoDisponibleP p
||q s
IdEstadot |
==} 
MaestroSettings
� �
.
� �
Default
� �
.
� �+
IdDetalleMaestroEstadoAnulado
� �
||
� �
IdEstado
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �2
$IdDetalleMaestroEstadoSalidaCambiado
� �
;
� �
}
� �
public 
bool 
EstaOcupado 
{  !
get" %
=>& (
IdEstado) 1
==2 4
MaestroSettings5 D
.D E
DefaultE L
.L M+
IdDetalleMaestroEstadoCheckedInM l
||m o
IdEstadop x
==y {
MaestroSettings	| �
.
� �
Default
� �
.
� �3
%IdDetalleMaestroEstadoEntradaCambiado
� �
||
� �
IdEstado
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �.
 IdDetalleMaestroEstadoCheckedOut
� �
;
� �
}
� �
public 
bool !
EstaOcupadoDisponible )
{* +
get, /
=>0 2
(3 4
IdEstado4 <
=== ?
MaestroSettings@ O
.O P
DefaultP W
.W X0
$IdDetalleMaestroEstadoSalidaCambiadoX |
||} 
IdEstado
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �.
 IdDetalleMaestroEstadoCheckedOut
� �
)
� �
&&
� �
EsFechaAtencion
� �
;
� �
}
� �
public 
bool 

{" #
get$ '
=>( *
IdEstado+ 3
==4 6
MaestroSettings7 F
.F G
DefaultG N
.N O,
 IdDetalleMaestroEstadoConfirmadoO o
;o p
}q r
public 
int 
EstadoHabitacion #
{$ %
get& )
=>* ,!
EstaOcupadoDisponible- B
?C D
(E F
intF I
)I J 
EstadoHabitacionEnumJ ^
.^ _
OcupadoDisponible_ p
:q r
(s t
EstaOcupadot 
?
� �
(
� �
int
� �
)
� �"
EstadoHabitacionEnum
� �
.
� �
Ocupado
� �
:
� �
(
� �

� �
?
� �
(
� �
int
� �
)
� �"
EstadoHabitacionEnum
� �
.
� �
	Reservado
� �
:
� �
(
� �
int
� �
)
� �"
EstadoHabitacionEnum
� �
.
� �

Disponible
� �
)
� �
)
� �
;
� �
}
� �
public 
bool 
PuedeHacerConsumo %
{& '
get( +
=>, .
IdEstado/ 7
==8 :
MaestroSettings; J
.J K
DefaultK R
.R S+
IdDetalleMaestroEstadoCheckedInS r
&&s u
EsFechaAtencion	v �
;
� �
}
� �
public 
bool 
EsFechaAtencion #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public *
EstadoHabitacionEnPlanificador -
(- .
). /
{0 1
}2 3
} 
} �
RD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Habitacion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public 

class 

Habitacion 
{ 
public		 
int		 
Id		 
{		 
get		 
;		 
set		  
;		  !
}		" #
public

 
int

 
IdActor

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 

Ambiente %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public
Concepto_Negocio_Comercial_
TipoHabitacion
{
get
;
set
;
}
public 
string 
Anexo 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
	EsVigente 
{ 
get  #
;# $
set% (
;( )
}* +
public 
List 
< 
ItemGenerico  
>  !
Camas" '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
InformacionCamas &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
InformacionAforo &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 

Habitacion 
( 
) 
{ 
} 
} 
} �
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\HabitacionBandeja.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public 

class 
HabitacionBandeja "
{ 
public		 
int		 
Id		 
{		 
get		 
;		 
set		  
;		  !
}		" #
public

 
string

 
CodigoHabitacion

 &
{

' (
get

) ,
;

, -
set

. 1
;

1 2
}

3 4
public 
string 
Ambiente 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
string
Camas
{
get
;
set
;
}
public 
string 
CamasInformacion &
{ 	
get 
{ 
var 
camas 
= 
JsonConvert '
.' (
DeserializeObject( 9
<9 :
List: >
<> ?
ItemGenerico? K
>K L
>L M
(M N
CamasN S
)S T
;T U
string 
cadenaCamas "
=# $
$str% '
;' (
foreach 
( 
var 
item !
in" $
camas% *
)* +
{ 
cadenaCamas 
=  !
$"" $
$str$ %
{% &
cadenaCamas& 1
}1 2
{2 3
item3 7
.7 8
Valor8 =
}= >
$str> ?
{? @
item@ D
.D E
NombreE K
}K L
$strL O
"O P
;P Q
} 
cadenaCamas 
= 
cadenaCamas )
.) *
Remove* 0
(0 1
cadenaCamas1 <
.< =
Length= C
-D E
$numF G
)G H
;H I
return 
cadenaCamas "
;" #
} 
set 
{ 
} 
} 	
public   
string   
Aforo   
{   
get   !
;  ! "
set  # &
;  & '
}  ( )
public"" 
string"" 

{""$ %
get""& )
;"") *
set""+ .
;"". /
}""0 1
public## 
bool## 
	EsVigente## 
{## 
get##  #
;### $
set##% (
;##( )
}##* +
}%% 
}&& �
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ConsumoSimple.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public		 

class		 

{

 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public
string
FechaString
{
get
=>
Fecha
.
ToString
(
$str
)
;
}
public 
string 
Huesped 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
DetalleConsumo "
>" #
DetallesConsumo$ 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
} 
} �
`D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\HabitacionEnPlanificador.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 


 +
.

+ ,

SigesHotel

, 6
{ 
public 

class $
HabitacionEnPlanificador )
{
private 
List 
< 
Precio 
> 
precios $
;$ %
public 
List 
< 
Precio 
> 
Precios #
{$ %
set& )
=>* ,
precios- 4
=5 6
value7 <
;< =
}> ?
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Ambiente 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
decimal 
PrecioUnitario %
{ 	
get 
{ 
return 
precios 
. 
Count $
($ %
)% &
>' (
$num) *
?+ ,
precios- 4
.4 5
FirstOrDefault5 C
(C D
pD E
=>F H
pI J
.J K
id_tarifa_dK V
==W Y
MaestroSettingsZ i
.i j
Defaultj q
.q r)
IdDetalleMaestroTarifaNormal	r �
)
� �
!=
� �
null
� �
?
� �
precios
� �
.
� �
FirstOrDefault
� �
(
� �
p
� �
=>
� �
p
� �
.
� �
id_tarifa_d
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �*
IdDetalleMaestroTarifaNormal
� �
)
� �
.
� �
valor
� �
:
� �
precios
� �
.
� �
First
� �
(
� �
)
� �
.
� �
valor
� �
:
� �
$num
� �
;
� �
} 
} 	
public 
bool 

Disponible 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
Ocupada 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
PorIngresar 
{  !
get" %
;% &
set' *
;* +
}, -
public 
bool 
PorSalir 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 

EnLimpieza 
{  
get! $
;$ %
set& )
;) *
}+ ,
public   
List   
<   *
EstadoHabitacionEnPlanificador   2
>  2 3
EstadosHabitacion  4 E
{  F G
get  H K
;  K L
set  M P
;  P Q
}  R S
public"" $
HabitacionEnPlanificador"" '
(""' (
)""( )
{""* +
}"", -
}## 
}$$ �	
OD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Huesped.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public 

class 
Huesped 
: 
ActorComercial_ *
{ 
public		 
int		 
	IdHuesped		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
ItemGenerico

 


 )
{

* +
get

, /
;

/ 0
set

1 4
;

4 5
}

6 7
public 
string 
JsonHuesdep !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool 
	EsTitular 
{ 
get  #
;# $
set% (
;( )
}* +
public 
Huesped 
( 
) 
{ 
} 
} 
} �	
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\DetalleConsumo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public		 

class		 
DetalleConsumo		 
{

 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public
decimal
Cantidad
{
get
;
set
;
}
public 
decimal 
PrecioUnitario %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ModeloExtranet\Booking.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7
ModeloExtranet7 E
{ 
public		 

class		 
Booking		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
IdFilial 
{ 
get !
;! "
set# &
;& '
}( )
public
decimal

TotalPrice
{
get
;
set
;
}
public 
int 
QuantityRoom 
{  !
get" %
;% &
set' *
;* +
}, -
public 
PersonalData 
PersonalData (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
ConfirmationCode &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
List 
< 
RoomType 
> 
	RoomTypes '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
DateBooking 
DateBooking &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DateTime 
RegistrationDate (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
int 
NumberOfNights !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
PaymentTrace 
PaymentTrace (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} 
} �
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ModeloExtranet\PaymentInfo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7
ModeloExtranet7 E
{ 
public		 

class		 
PaymentInfo		 
{

 
public 
string 

id_payment  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
code_payment "
{# $
get% (
;( )
set* -
;- .
}/ 0
public
string
amount_payment
{
get
;
set
;
}
} 
} �
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ModeloExtranet\PaymentTrace.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7
ModeloExtranet7 E
{ 
public		 

class		 
PaymentTrace		 
{

 
public 
int 
IdPaymentMethod "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
ImageVoucher "
{# $
get% (
;( )
set* -
;- .
}/ 0
public
string
JsonPaymentInformation
{
get
;
set
;
}
public 
PaymentInfo 
PaymentInformation -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} 
} �
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ModeloExtranet\DateBooking.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7
ModeloExtranet7 E
{ 
public		 

class		 
DateBooking		 
{

 
public 
DateTime 
	EntryDate !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public
int
IdEstablishment
{
get
;
set
;
}
} 
} �
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ModeloExtranet\PersonalData.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7
ModeloExtranet7 E
{ 
public		 

class		 
PersonalData		 
{

 
public 
int 
IdTypeDocument !
{" #
get$ '
;' (
set) ,
;, -
}. /
public
string
DocumentNumber
{
get
;
set
;
}
public 
string 
NameComplete "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Phone 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
	IdCountry 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 

{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 

IdProvince 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 

IdDistrict 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
HomeAddress !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} �
_D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ModeloExtranet\RoomType.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7
ModeloExtranet7 E
{ 
public		 

class		 
RoomType		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public
string
	UrlImagen
{
get
;
set
;
}
public 
string 
[ 
] 
BedArray  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
AdultsCapacity !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
ChildrenCapacity #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 

PriceValue !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
AvailabilityAmount %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\Anulada.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{ 
public		 

class		 
Anulada		 
{

 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public
string
TipoHabitacion
{
get
;
set
;
}
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Empleado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
Anulada 
> 
Convert $
($ %
)% &
{ 	
return 
new 
List 
< 
Anulada #
># $
($ %
)% &
;& '
} 	
} 
} �
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\EventoIncidente.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{		 
public

 

class

 
EventoAtencion

 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
DateTime
Fecha
{
get
;
set
;
}
public 
string 
Empleado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
int 
ModoFacturacion "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
public 

class 
AtencionHabitacion #
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

Habitacion  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
}(( �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\Reserva.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{		 
public

 

class

 
Reserva

 
{ 
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public
DateTime
FechaIngreso
{
get
;
set
;
}
public 
DateTime 
FechaSalida #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
int 
Noches 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
bool 

{" #
get$ '
=>( *
ModoFacturacion+ :
!=; =
(> ?
int? B
)B C 
ModoFacturacionHotelC W
.W X
NoEspecificadoX f
;f g
}h i
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Responsable !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
ModoFacturacion "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
List 
< 
Reserva 
> 
Convert $
($ %
)% &
{ 	
return 
new 
List 
< 
Reserva #
># $
($ %
)% &
;& '
} 	
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\Incidente.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{		 
public

 

class

 
	Incidente

 
{ 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public
string
Codigo
{
get
;
set
;
}
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
MontoHospedaje %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
Devuelto 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 

Habitacion  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string $
TipoComprobanteDescuento .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
string ,
 SerieYNumeroComprobanteDescuento 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
string 
Solucion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Empleado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
List 
< 
	Incidente 
> 
Convert &
(& '
)' (
{ 	
return 
new 
List 
< 
	Incidente %
>% &
(& '
)' (
;( )
} 	
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\Facturada.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{		 
public

 

class

 
	Facturada

 
{ 
public 
int 
IdEventoFacturado $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
string
Facturacion
{
get
;
set
;
}
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DateTime 
FechaOperacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

Habitacion  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
Cliente 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
	Facturada 
> 
Convert &
(& '
)' (
{ 	
return 
new 
List 
< 
	Facturada %
>% &
(& '
)' (
;( )
} 	
} 
} �

ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\Huesped.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{		 
public

 

class

 
Huesped

 
{ 
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public
string

{
get
;
set
;
}
public 
bool 
	EsTitular 
{ 
get "
=># %
string& ,
., -

(: ;

)H I
?J K
falseL Q
:R S
JsonConvertT _
._ `
DeserializeObject` q
<q r
JsonHuespedr }
>} ~
(~ 

)
� �
.
� �
	estitular
� �
;
� �
}
� �
} 
} �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\Ingreso.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{ 
public		 

class		 
Ingreso		 
{

 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public
IEnumerable
<
Huesped
>
	Huespedes
{
get
;
set
;
}
public 
string 
Titular 
{ 
get  #
=>$ &
	Huespedes' 0
?0 1
.1 2
Single2 8
(8 9
h9 :
=>; =
h> ?
.? @
	EsTitular@ I
)I J
.J K
NombreK Q
;Q R
}S T
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
int 
Noches 
{ 
get 
;  
set! $
;$ %
}& '
public 
List 
< 
Ingreso 
> 
Convert $
($ %
)% &
{ 	
return 
new 
List 
< 
Ingreso #
># $
($ %
)% &
;& '
} 	
} 
} �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\NoFacturada.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{ 
public		 

class		 
NoFacturada		 
{

 
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
string
CodigoHabitacion
{
get
;
set
;
}
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
IEnumerable 
< 
Huesped "
>" #
	Huespedes$ -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
Titular 
{ 
get  #
=>$ &
	Huespedes' 0
.0 1
Count1 6
(6 7
)7 8
>9 :
$num; <
?= >
	Huespedes? H
.H I
SingleI O
(O P
hP Q
=>R T
hU V
.V W
	EsTitularW `
)` a
.a b
Nombreb h
:i j
$strk n
;n o
}p q
public 
string 
Estado 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
NoFacturada 
>  
Convert! (
(( )
)) *
{ 	
return 
new 
List 
< 
NoFacturada '
>' (
(( )
)) *
;* +
} 	
} 
} �M
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\RegistroHuesped.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,

SigesHotel		, 6
.		6 7

PlainModel		7 A
{

 
public 

class 
RegistroHuesped  
{ 
private
Estado_transaccion
estadoSalida
;
public 
Estado_transaccion !
EstadoSalida" .
{/ 0
set1 4
=>5 7
estadoSalida8 D
=E F
valueG L
;L M
}N O
public 
DateTime 
FechaFinAtencion (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Huesped 
{ 
get  #
;# $
set% (
;( )
}* +
public 
bool 
EsMasculino 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Nacionalidad "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int 
IdContinente 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 

Continente  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
IdPais 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 
Pais 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
IdUbigeo 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
IdRegionUbigeo !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
IdProvinciaUbigeo $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
Ubigeo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

Residencia  
{! "
get# &
=>' )
IdPais* 0
==1 3
MaestroSettings4 C
.C D
DefaultD K
.K L&
IdDetalleMaestroNacionPeruL f
?g h
Ubigeoi o
.o p
Splitp u
(u v
$charv y
)y z
[z {
$num{ |
]| }
:~ 
Pais
� �
;
� �
}
� �
public 
string  
TipoDocumentoCliente *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public   
string   "
NumeroDocumentoCliente   ,
{  - .
get  / 2
;  2 3
set  4 7
;  7 8
}  9 :
public!! 
Estado_transaccion!! !#
EstadoIngresoReferencia!!" 9
{!!: ;
get!!< ?
;!!? @
set!!A D
;!!D E
}!!F G
public"" 
Estado_transaccion"" !

{""0 1
get""2 5
;""5 6
set""7 :
;"": ;
}""< =
public## 
DateTime## 
FechaIngreso## $
{##% &
get##' *
=>##+ -

==##< >
null##? C
?##D E#
EstadoIngresoReferencia##F ]
.##] ^
fecha##^ c
:##d e

.##s t
fecha##t y
;##y z
}##{ |
public$$ 
DateTime$$ 
FechaSalida$$ #
{$$$ %
get$$& )
=>$$* ,
estadoSalida$$- 9
==$$: <
null$$= A
?$$B C
FechaFinAtencion$$D T
:$$U V
estadoSalida$$W c
.$$c d
fecha$$d i
;$$i j
}$$k l
public%% 
int%% 
IdTipoHabitacion%% #
{%%$ %
get%%& )
;%%) *
set%%+ .
;%%. /
}%%0 1
public&& 
string&& 
TipoHabitacion&& $
{&&% &
get&&' *
;&&* +
set&&, /
;&&/ 0
}&&1 2
public'' 
string'' 
CodigoHabitacion'' &
{''' (
get'') ,
;'', -
set''. 1
;''1 2
}''3 4
public(( 
decimal(( 
ImporteTotal(( #
{(($ %
get((& )
;(() *
set((+ .
;((. /
}((0 1
public)) 
decimal)) 
Tarifa)) 
{)) 
get))  #
;))# $
set))% (
;))( )
}))* +
public** 
int** 
Arribos** 
{** 
get**  
=>**! #
$num**$ %
;**% &
}**' (
public++ 
int++ 
Noches++ 
{++ 
get++ 
;++  
set++! $
;++$ %
}++& '
public,, 
int,, 
Pernoctaciones,, !
{,," #
get,,$ '
=>,,( *
Noches,,+ 1
;,,1 2
},,3 4
public-- 
int-- 

{--! "
get--# &
;--& '
set--( +
;--+ ,
}--- .
public.. 
int.. 
EsMotivoVacaciones.. %
{..& '
get..( +
=>.., .

==..= ?

...M N
Default..N U
...U V3
'IdDetalleMaestroMotivoDeViajeVacaciones..V }
?..~ 
$num
..� �
:
..� �
$num
..� �
;
..� �
}
..� �
public// 
int// 
EsMotivoVisita// !
{//" #
get//$ '
=>//( *

==//9 ;

.//I J
Default//J Q
.//Q R/
#IdDetalleMaestroMotivoDeViajeVisita//R u
?//v w
$num//x y
://z {
$num//| }
;//} ~
}	// �
public00 
int00 
EsMotivoEducacion00 $
{00% &
get00' *
=>00+ -

==00< >

.00L M
Default00M T
.00T U2
&IdDetalleMaestroMotivoDeViajeEducacion00U {
?00| }
$num00~ 
:
00� �
$num
00� �
;
00� �
}
00� �
public11 
int11 

{11! "
get11# &
=>11' )

==118 :

.11H I
Default11I P
.11P Q.
"IdDetalleMaestroMotivoDeViajeSalud11Q s
?11t u
$num11v w
:11x y
$num11z {
;11{ |
}11} ~
public22 
int22 
EsMotivoReligion22 #
{22$ %
get22& )
=>22* ,

==22; =

.22K L
Default22L S
.22S T1
%IdDetalleMaestroMotivoDeViajeReligion22T y
?22z {
$num22| }
:22~ 
$num
22� �
;
22� �
}
22� �
public33 
int33 
EsMotivoCompras33 "
{33# $
get33% (
=>33) +

==33: <

.33J K
Default33K R
.33R S0
$IdDetalleMaestroMotivoDeViajeCompras33S w
?33x y
$num33z {
:33| }
$num33~ 
;	33 �
}
33� �
public44 
int44 
EsMotivoNegocios44 #
{44$ %
get44& )
=>44* ,

==44; =

.44K L
Default44L S
.44S T1
%IdDetalleMaestroMotivoDeViajeNegocios44T y
?44z {
$num44| }
:44~ 
$num
44� �
;
44� �
}
44� �
public55 
int55 
EsMotivoTrabajo55 "
{55# $
get55% (
=>55) +

==55: <

.55J K
Default55K R
.55R S0
$IdDetalleMaestroMotivoDeViajeTrabajo55S w
?55x y
$num55z {
:55| }
$num55~ 
;	55 �
}
55� �
public66 
int66 

{66! "
get66# &
=>66' )

==668 :

.66H I
Default66I P
.66P Q.
"IdDetalleMaestroMotivoDeViajeOtros66Q s
?66t u
$num66v w
:66x y
$num66z {
;66{ |
}66} ~
public88 
List88 
<88 
RegistroHuesped88 #
>88# $
Convert88% ,
(88, -
)88- .
{99 	
return:: 
new:: 
List:: 
<:: 
RegistroHuesped:: +
>::+ ,
(::, -
)::- .
;::. /
};; 	
}<< 
}== �3
_D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\FormularioT1.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{ 
public		 

class		 
FormularioT1		 
{

 
public 
List 
< 
	DiaArribo 
> 
Dias1_8Arribos -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
List 
< 
	DiaArribo 
> 
Dias9_16Arribos .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public
List
<
	DiaArribo
>
Dias17_24Arribos
{
get
;
set
;
}
public 
List 
< 
	DiaArribo 
> 
Dias25_TotalArribos 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
List 
< 
ArriboPernoctacion &
>& ',
 ArribosPernoctacionesExtranjeros( H
{I J
getK N
;N O
setP S
;S T
}U V
public 
List 
< 
ArriboPernoctacion &
>& '+
ArribosPernoctacionesNacionales( G
{H I
getJ M
;M N
setO R
;R S
}T U
public 
List 
< 
MotivoViaje 
>  
MotivoViajes! -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} 
public 

class 
	DiaArribo 
{ 
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
Arribo 
{ 
get 
;  
set! $
;$ %
}& '
public 
	DiaArribo 
( 
) 
{ 	
} 	
public 
	DiaArribo 
( 
string 
nombre  &
,& '
int( +
arribo, 2
)2 3
{ 	
Nombre 
= 
nombre 
; 
Arribo   
=   
arribo   
;   
}!! 	
public## 
List## 
<## 
	DiaArribo## 
>## 
Convert## &
(##& '
)##' (
{$$ 	
return%% 
new%% 
List%% 
<%% 
	DiaArribo%% %
>%%% &
(%%& '
)%%' (
;%%( )
}&& 	
}'' 
public(( 

class(( 
ArriboPernoctacion(( #
{)) 
public++ 
string++ 
Nombre++ 
{++ 
get++ "
;++" #
set++$ '
;++' (
}++) *
public,, 
int,, 
Arribo,, 
{,, 
get,, 
;,,  
set,,! $
;,,$ %
},,& '
public-- 
int-- 
Pernoctacion-- 
{--  !
get--" %
;--% &
set--' *
;--* +
}--, -
public.. 
ArriboPernoctacion.. !
(..! "
).." #
{// 	
}11 	
public22 
ArriboPernoctacion22 !
(22! "
string22" (
nombre22) /
,22/ 0
int221 4
arribo225 ;
,22; <
int22= @
pernoctacion22A M
)22M N
{33 	
Nombre44 
=44 
nombre44 
;44 
Arribo55 
=55 
arribo55 
;55 
Pernoctacion66 
=66 
pernoctacion66 '
;66' (
}77 	
public88 
List88 
<88 
ArriboPernoctacion88 &
>88& '
Convert88( /
(88/ 0
)880 1
{99 	
return:: 
new:: 
List:: 
<:: 
ArriboPernoctacion:: .
>::. /
(::/ 0
)::0 1
;::1 2
};; 	
}== 
public>> 

class>> 
MotivoViaje>> 
{?? 
publicAA 
stringAA 
NombreAA 
{AA 
getAA "
;AA" #
setAA$ '
;AA' (
}AA) *
publicBB 
intBB 
TotalBB 
{BB 
getBB 
;BB 
setBB  #
;BB# $
}BB% &
publicCC 
intCC 

VacacionesCC 
{CC 
getCC  #
;CC# $
setCC% (
;CC( )
}CC* +
publicDD 
intDD 
VisitaDD 
{DD 
getDD 
;DD  
setDD! $
;DD$ %
}DD& '
publicEE 
intEE 
	EducacionEE 
{EE 
getEE "
;EE" #
setEE$ '
;EE' (
}EE) *
publicFF 
intFF 
SaludFF 
{FF 
getFF 
;FF 
setFF  #
;FF# $
}FF% &
publicGG 
intGG 
ReligionGG 
{GG 
getGG !
;GG! "
setGG# &
;GG& '
}GG( )
publicHH 
intHH 
ComprasHH 
{HH 
getHH  
;HH  !
setHH" %
;HH% &
}HH' (
publicII 
intII 
NegociosII 
{II 
getII !
;II! "
setII# &
;II& '
}II( )
publicJJ 
intJJ 
OtrosJJ 
{JJ 
getJJ 
;JJ 
setJJ  #
;JJ# $
}JJ% &
publicLL 
ListLL 
<LL 
MotivoViajeLL 
>LL  
ConvertLL! (
(LL( )
)LL) *
{MM 	
returnNN 
newNN 
ListNN 
<NN 
MotivoViajeNN '
>NN' (
(NN( )
)NN) *
;NN* +
}OO 	
}PP 
}RR �
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\PlainModel\Salida.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
.6 7

PlainModel7 A
{		 
public

 

class

 
Salida

 
{ 
public 
DateTime 
FechaSalida #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public
string
Codigo
{
get
;
set
;
}
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Estado_transaccion !#
EstadoIngresoReferencia" 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
Estado_transaccion !

{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
DateTime 
FechaIngreso $
{% &
get' *
=>+ -

==< >
null? C
?D E#
EstadoIngresoReferenciaF ]
.] ^
fecha^ c
:d e

.s t
fechat y
;y z
}{ |
public 
IEnumerable 
< 
Huesped "
>" #
	Huespedes$ -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
string 
Titular 
{ 
get  #
=>$ &
	Huespedes' 0
?0 1
.1 2
Single2 8
(8 9
h9 :
=>; =
h> ?
.? @
	EsTitular@ I
)I J
.J K
NombreK Q
;Q R
}S T
public 
int 
Noches 
{ 
get 
;  
set! $
;$ %
}& '
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
Salida 
> 
Convert #
(# $
)$ %
{ 	
return 
new 
List 
< 
Salida "
>" #
(# $
)$ %
;% &
} 	
} 
} �
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ReportePlanificador.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,

SigesHotel		, 6
{

 
public 

class 
ReportePlanificador $
{ 
public
int
Disponibles
{
get
;
set
;
}
public 
int 
Ocupadas 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
PorIngresar 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
PorSalir 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 

EnLimpieza 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DateTime 
FechaActual #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
FechaActualString '
{( )
get* -
=>. 0
FechaActual1 <
.< =
Date= A
.A B
ToLongDateStringB R
(R S
)S T
;T U
}V W
public 
string 
HoraActualString &
{' (
get) ,
=>- /
FechaActual0 ;
.; <
ToString< D
(D E
$strE O
)O P
;P Q
}R S
public 
ReportePlanificador "
(" #
)# $
{ 	
} 	
} 
} �
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Planificador.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 

.		+ ,

SigesHotel		, 6
{

 
public 

class 
Planificador 
{ 
public
List
<
HabitacionEnPlanificador
>
HabitacionesEnPlanificador
{
get
;
set
;
}
public 
List 
< 
string 
> 
FechasPlanificador .
{/ 0
get1 4
=>5 7&
HabitacionesEnPlanificador8 R
.R S

SelectManyS ]
(] ^
h^ _
=>` b
hc d
.d e
EstadosHabitacione v
)v w
.w x
Selectx ~
(~ 
h	 �
=>
� �
h
� �
.
� �
Fecha
� �
.
� �
Date
� �
)
� �
.
� �
Distinct
� �
(
� �
)
� �
.
� �
OrderBy
� �
(
� �
h
� �
=>
� �
h
� �
)
� �
.
� �
Select
� �
(
� �
h
� �
=>
� �
h
� �
.
� �
ToString
� �
(
� �
$str
� �
)
� �
)
� �
.
� �
ToList
� �
(
� �
)
� �
;
� �
}
� �
public 
Planificador 
( 
) 
{ 	&
HabitacionesEnPlanificador &
=' (
new) ,
List- 1
<1 2$
HabitacionEnPlanificador2 J
>J K
(K L
)L M
;M N
} 	
} 
} �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\AtencionMacroHotel.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public

class
AtencionMacroHotel
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
ActorComercial_ 
Responsable *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
FechaRegistroString )
{* +
get, /
=>0 2

.@ A
ToStringA I
(I J
$strJ V
)V W
;W X
}Y Z
public 
string #
FechaHoraRegistroString -
{. /
get0 3
=>4 6

.D E
ToStringE M
(M N
$strN c
)c d
;d e
}f g
public 
bool 
TieneFacturacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
bool 
FacturadoGlobal #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
DatosVentaIntegrada "
Comprobante# .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
IEnumerable 
< 

>( )

Atenciones* 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
decimal 
Total 
{ 
get "
;" #
set$ '
;' (
}) *
public   
int   
IdMedioPagoExtranet   &
{  ' (
get  ) ,
;  , -
set  . 1
;  1 2
}  3 4
public!! 
string!! !
ImagenVoucherExtranet!! +
{!!, -
get!!. 1
;!!1 2
set!!3 6
;!!6 7
}!!8 9
public"" 
bool"" $
HayImagenVoucherExtranet"" ,
{""- .
get""/ 2
;""2 3
set""4 7
;""7 8
}""9 :
public## 
List## 
<## 

ItemEstado## 
>## 
Eventos##  '
{##( )
get##* -
;##- .
set##/ 2
;##2 3
}##4 5
public%% 
AtencionMacroHotel%% !
(%%! "
)%%" #
{%%$ %
}%%& '
}&& 
}'' �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ReportData\PrincipalReportData.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 


 +
.

+ ,

SigesHotel

, 6
.

6 7
Report

7 =
{ 
public 

class 
PrincipalReportData $
{
public 
List 
< 
Establecimiento #
># $
Establecimientos% 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
Establecimiento !
EstablecimientoSesion 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
DateTime 
FechaActual_ $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
long 
FechaActual 
{  !
get" %
{& '
return( .
FechaActual_/ ;
.; <$
ToJavaScriptMilliseconds< T
(T U
)U V
;V W
}X Y
}Z [
public 
long 
FechaDesdeDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B
DateB F
.F G$
ToJavaScriptMillisecondsG _
(_ `
)` a
;a b
}c d
}e f
public 
long 
FechaHastaDefault %
{& '
get( +
{, -
return. 4
FechaActual_5 A
.A B
DateB F
.F G
AddDaysG N
(N O
$numO P
)P Q
.Q R

AddSecondsR \
(\ ]
-] ^
$num^ _
)_ `
.` a$
ToJavaScriptMillisecondsa y
(y z
)z {
;{ |
}} ~
}	 �
public 
int "
MaximoDiasReporteHotel )
{* +
get, /
{0 1
return2 8

.F G
DefaultG N
.N O"
MaximoDiasReporteHotelO e
;e f
}g h
}i j
public 
List 
< 
ItemGenerico  
>  !
TiposHabitacion" 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
bool 
EsAdministrador #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} �
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ReservaBandeja.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public		 

class		 
ReservaBandeja		 
{

 
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
long 
IdAtencionMacro #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public
string
Codigo
{
get
;
set
;
}
public 
string 
Responsable !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Ambiente 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DateTime 
Ingreso 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
FechaIngreso "
{# $
get% (
=>) +
Ingreso, 3
.3 4
ToString4 <
(< =
$str= I
)I J
;J K
}L M
public 
DateTime 
Salida 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
FechaSalida !
{" #
get$ '
=>( *
Salida+ 1
.1 2
ToString2 :
(: ;
$str; G
)G H
;H I
}J K
public 
int 
Noches 
{ 
get 
;  
set! $
;$ %
}& '
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Total 
{ 
get !
=>" $
Importe% ,
., -
ToString- 5
(5 6
$str6 :
): ;
;; <
}= >
public 
string 
Estado 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
	Facturado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 

{$ %
get& )
=>* ,
	Facturado- 6
?7 8
$str9 =
:> ?
$str@ D
;D E
}F G
} 
} �
OD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\Consumo.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 


 +
.

+ ,

SigesHotel

, 6
{ 
public 

class 
Consumo 
: 

{
public 
string 
TipoHabitacion $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CodigoHabitacion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
bool 
	Facturado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
bool 
PuedeInvalidar "
{# $
get% (
=>) +
	Facturado, 5
==6 8
false9 >
;> ?
}@ A
public 
int 
IdEstado 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
EstaInvalidado "
{# $
get% (
=>) +
IdEstado, 4
==5 7
MaestroSettings8 G
.G H
DefaultH O
.O P,
 IdDetalleMaestroEstadoInvalidadoP p
;p q
}r s
} 
} �
SD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\JsonHuesped.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public		 

class		 
JsonHuesped		 
{

 
public 
bool 
	estitular 
; 
} 
}
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\TipoHabitacion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public 

class 
TipoHabitacion 
{ 
public		 
int		 
Id		 
{		 
get		 
;		 
set		  
;		  !
}		" #
public

 
string

 
Nombre

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 
string 
Descripcion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
ItemGenerico 

AforoNinos &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public
ItemGenerico
AforoAdultos
{
get
;
set
;
}
public 
List 
< 
ItemGenerico  
>  !
Caracteristicas" 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
List 
< (
Precio_Compra_Venta_Concepto 0
>0 1
Precios2 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
List 
< 
int 
> %
IdsValoresCaracteristicas 2
{ 	
get 
{ 
List 
< 
int 
> %
IdsValoresCaracteristicas 3
=4 5
new6 9
List: >
<> ?
int? B
>B C
(C D
)D E
;E F%
IdsValoresCaracteristicas )
.) *
Add* -
(- .
AforoAdultos. :
.: ;
Id; =
)= >
;> ?%
IdsValoresCaracteristicas )
.) *
Add* -
(- .

AforoNinos. 8
.8 9
Id9 ;
); <
;< =
if 
( 
Caracteristicas #
!=$ &
null' +
)+ ,
{ 
foreach 
( 
var  
caracteristica! /
in0 2
Caracteristicas3 B
)B C
{ %
IdsValoresCaracteristicas 1
.1 2
Add2 5
(5 6
caracteristica6 D
.D E
IdE G
)G H
;H I
} 
} 
return %
IdsValoresCaracteristicas 0
;0 1
} 
set   
{!! 
}"" 
}## 	
public$$ 
List$$ 
<$$ 
FotoTipoHabitacion$$ &
>$$& '
Fotos$$( -
{$$. /
get$$0 3
;$$3 4
set$$5 8
;$$8 9
}$$: ;
public%% 
bool%% 
HayFotos%% 
{%% 
get%% "
=>%%# %
Fotos%%& +
!=%%, .
null%%/ 3
;%%3 4
}%%5 6
public&& 
List&& 
<&& 
string&& 
>&& 
FotosEliminadas&& +
{&&, -
get&&. 1
;&&1 2
set&&3 6
;&&6 7
}&&8 9
public'' 
bool'' 
HayFotosEliminadas'' &
{''' (
get'') ,
=>''- /
FotosEliminadas''0 ?
!=''@ B
null''C G
;''G H
}''I J
public(( 
bool(( 
	EsVigente(( 
{(( 
get((  #
;((# $
set((% (
;((( )
}((* +
})) 
public** 

class** 
FotoTipoHabitacion** #
{++ 
public,, 
string,, 
Nombre,, 
{,, 
get,, "
;,," #
set,,$ '
;,,' (
},,) *
public-- 
string-- 
Foto-- 
{-- 
get--  
;--  !
set--" %
;--% &
}--' (
public// 
FotoTipoHabitacion// !
(//! "
)//" #
{//$ %
}//& '
}00 
}11 �
_D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\TipoHabitacionesBandeja.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{		 
public

 

class

 #
TipoHabitacionesBandeja

 (
{ 
private 
List 
< 
Precio 
> 
precios $
;$ %
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Precio 
{ 
get 
{ 
string 

preciotemp !
=" #
$str$ &
;& '
foreach 
( 
var 
precio #
in$ &
precios' .
). /
{ 

preciotemp 
+= !
precio" (
.( )
Detalle_maestro3) 9
.9 :
nombre: @
+A B
$strC G
+H I
precioJ P
.P Q
valorQ V
+V W
$strW [
;[ \
} 
return 

preciotemp !
;! "
} 
} 	
public 
List 
< 
Precio 
> 
Precios #
{$ %
set& )
=>* ,
precios- 4
=5 6
value7 <
;< =
}> ?
public 
string 
CapacidadNinio $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
CapacidadAdulto %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
	Capacidad 
{ 	
get   
{!! 
return"" 
$str"" !
+""! "
CapacidadAdulto""" 1
+""1 2
$str""2 =
+""= >
CapacidadNinio""> L
;""L M
}## 
}$$ 	
public%% 
bool%% 
	EsVigente%% 
{%% 
get%%  #
;%%# $
set%%% (
;%%( )
}%%* +
}&& 
}'' �
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesHotel\ConsumoHabitacion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,

SigesHotel, 6
{ 
public

class
ConsumoHabitacion
{ 
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
IdHabitacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 

FechaDesde "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
DateTime 

FechaHasta "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 

{$ %
get& )
=>* ,

FechaDesde- 7
.7 8
ToString8 @
(@ A
$strA M
)M N
+O P
$strQ V
+W X

FechaHastaY c
.c d
ToStringd l
(l m
$strm y
)y z
;z {
}| }
public 
string 
Titular 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
ItemGenerico  
>  !
	Huespedes" +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
ItemGenerico 
HuespedConsumo *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
List 
< 

>! "
Consumos# +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
List 
< 
DetalleDeOperacion &
>& '
Detalles( 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
} 
} �
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\ConfiguracionTurnoCochera.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
.		$ %
SigesParking		% 1
{

 
public 

class %
ConfiguracionTurnoCochera *
{ 
public
int
IdConceptoServicioCochera
{
get
;
set
;
}
public 
int 
IdConceptoExceso #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} �
WD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\EntradaSalida.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public

class

{ 
public 
long 
	IdGeneral 
{ 
get  #
;# $
set% (
;( )
}* +
public 
long 
IdEspecifico  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
IdTipoMovimiento #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
Tipo 
{ 
get  
{! "
return# )
IdTipoMovimiento* :
==; =
MaestroSettings> M
.M N
DefaultN U
.U V+
IdDetalleMaestroEstadoIngresadoV u
?v w
$str	x �
:
� �
IdTipoMovimiento
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �.
 IdDetalleMaestroEstadoFinalizado
� �
?
� �
$str
� �
:
� �
throw
� �
new
� �
ModeloException
� �
(
� �
$str
� �
)
� �
;
� �
}
� �
}
� �
public 
DateTime 
	FechaHora !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Ticket 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
DescripcionVehiculo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public!! 

(!! 
)!! 
{"" 	
}## 	
public%% 
List%% 
<%% 

>%%! "
Convert%%# *
(%%* +
)%%+ ,
{&& 	
return'' 
null'' 
;'' 
}(( 	
})) 
}** �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\EntradaSalidaUsuario.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
.		$ %
SigesParking		% 1
{

 
public 

class  
EntradaSalidaUsuario %
:% &

{ 
public
string
DocumentoUsuario
{
get
;
set
;
}
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public  
EntradaSalidaUsuario #
(# $
)$ %
{ 	
} 	
public 
new 
List 
<  
EntradaSalidaUsuario ,
>, -
Convert. 5
(5 6
)6 7
{ 	
return 
null 
; 
} 	
} 
} �
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Exceptions\VehiculoNoExiste.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
.		$ %
SigesParking		% 1
.		1 2

Exceptions		2 <
{

 
public 

class %
VehiculoNoExisteException *
:* +
LogicaException, ;
{ 
public
string
Placa
{
get
;
set
;
}
public %
VehiculoNoExisteException (
() *
string* 0
Placa1 6
)6 7
:8 9
base: >
(> ?
String? E
.E F
FormatF L
(L M
$strM r
,r s
Placat y
)y z
)z {
{ 	
} 	
} 
} �
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Exceptions\VehiculoNoIngresado.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
.		$ %
SigesParking		% 1
.		1 2

Exceptions		2 <
{

 
public 

class (
VehiculoNoIngresadoException -
:- .
LogicaException/ >
{ 
public
string
Placa
{
get
;
set
;
}
public (
VehiculoNoIngresadoException +
(+ ,
string, 2
Placa3 8
)8 9
:: ;
base< @
(@ A
StringA G
.G H
FormatH N
(N O
$str	O �
,
� �
Placa
� �
)
� �
)
� �
{ 	
} 	
} 
} �
qD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Exceptions\VehiculoYaIngresadoException.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
.		$ %
SigesParking		% 1
.		1 2

Exceptions		2 <
{

 
public 

class (
VehiculoYaIngresadoException -
:- .
LogicaException/ >
{ 
public
string
Placa
{
get
;
set
;
}
public (
VehiculoYaIngresadoException +
(+ ,
string, 2
Placa3 8
)8 9
:: ;
base< @
(@ A
StringA G
.G H
FormatH N
(N O
$str	O �
,
� �
Placa
� �
)
� �
)
� �
{ 	
} 	
} 
} �
]D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\TicketSalidaCochera.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public		 

class		 
TicketSalidaCochera		 $
:		% &
ComprobanteImpreso		' 9
{

 
public 
byte 
[ 
] 
CodigoBarra !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
CodigoBarraSrc $
{% &
get' *
{+ ,
return- 3
(4 5
$str5 N
+O P
ConvertQ X
.X Y
ToBase64StringY g
(g h
CodigoBarrah s
,s t
$numu v
,v w
CodigoBarra	x �
.
� �
Length
� �
)
� �
)
� �
;
� �
}
� �
}
� �
public
override
string

NombreTipo
{
get
;
set
;
}
=
$str
;
public 
MovimientoCochera  
Salida! '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
TicketSalidaCochera "
(" #4
(EstablecimientoComercialExtendidoConLogo# K
sedeL P
,P Q-
!EstablecimientoComercialExtendidoR s%
establecimientoOperacion	t �
,
� �
MovimientoCochera
� �

movimiento
� �
,
� �
string
� �
serieTicket
� �
,
� �
int
� �
numeroTicket
� �
,
� �
byte
� �
[
� �
]
� �$
codigoBarrasMovimiento
� �
)
� �
:
� �
base
� �
(
� �
)
� �
{ 	
this 
. 
Serie 
= 
serieTicket $
;$ %
this 
. 
Numero 
= 
numeroTicket &
;& '
this 
. 
EsInvalidada 
= 
!  !

movimiento! +
.+ ,
EsValido, 4
;4 5
this 
. 
CodigoBarra 
= "
codigoBarrasMovimiento 5
;5 6
this 
. 
Emisor 
= 
new 
Emisor $
($ %
sede% )
,) *$
establecimientoOperacion+ C
)C D
;D E
this 
. 
FechaEmision 
= 

movimiento  *
.* +
Salida+ 1
;1 2
this 
. 
IdEstadoActual 
=  !
MaestroSettings" 1
.1 2
Default2 9
.9 :+
IdDetalleMaestroEstadoIngresado: Y
;Y Z
this 
. 
Salida 
= 

movimiento $
;$ %
} 	
} 
} �%
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\TurnoCochera.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
Custom

 $
.

$ %
SigesParking

% 1
{ 
public 

class 
TurnoCochera 
{
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 

{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
TimeSpan 

HoraInicio "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
decimal 
DuracionEnHoras &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public %
ConfiguracionTurnoCochera (

{7 8
get9 <
;< =
set> A
;A B
}C D
public 
string 
FechaInicioString '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
HoraInicioString &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
FechaFinString $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
DateTime 
Inicio 
( 
DateTime '
date( ,
), -
{ 	
return 
date 
. 
Date 
. 
Add  
(  !

HoraInicio! +
)+ ,
;, -
} 	
public 
DateTime 
Fin 
( 
DateTime $
date% )
)) *
{ 	
return   
Inicio   
(   
date   
.   
Date   #
)  # $
.  $ %
AddHours  % -
(  - .
(  . /
double  / 5
)  5 6
DuracionEnHoras  6 E
)  E F
;  F G
}!! 	
public"" 
TurnoCochera"" 
("" 
)"" 
{"" 
}""  !
public## 
TurnoCochera## 
(## 
Turno## !
turno_##" (
)##( )
{$$ 	
Id%% 
=%% 
turno_%% 
.%% 
id%% 
;%% 

=&& 
turno_&& "
.&&" #
id_tipo&&# *
;&&* +
Nombre'' 
='' 
turno_'' 
.'' 
nombre'' "
;''" #
DuracionEnHoras(( 
=(( 
turno_(( $
.(($ %
duracion_horas((% 3
;((3 4

HoraInicio)) 
=)) 
turno_)) 
.))  
hora_inicio))  +
;))+ ,

=** 
JsonConvert** '
.**' (
DeserializeObject**( 9
<**9 :%
ConfiguracionTurnoCochera**: S
>**S T
(**T U
turno_**U [
.**[ \
extension_json**\ j
)**j k
;**k l
}++ 	
public-- 
static-- 
List-- 
<-- 
TurnoCochera-- '
>--' (
Convert--) 0
(--0 1
List--1 5
<--5 6
Turno--6 ;
>--; <
turnos--= C
)--C D
{.. 	
List// 
<// 
TurnoCochera// 
>// 

=//- .
new/// 2
List//3 7
<//7 8
TurnoCochera//8 D
>//D E
(//E F
)//F G
;//G H
foreach00 
(00 
var00 
turno_00 
in00  "
turnos00# )
)00) *
{11 

.22 
Add22 !
(22! "
new22# &
TurnoCochera22' 3
(224 5
turno_225 ;
)22; <
)22< =
;22= >
}33 
return44 

;44  !
}55 	
}66 
}77 �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\ConfiguracionCochera.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
.		$ %
SigesParking		% 1
{

 
public 

class  
ConfiguracionCochera %
{ 
public
List
<
SistemaPagoCochera
>
SistemasDePagoHabilitados
{
get
;
set
;
}
public 
int 

{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int ;
/IdConceptoServicioCocheraEnSistemaDePagoPorHora B
{C D
getE H
;H I
setJ M
;M N
}O P
public 
int %
IdConceptoPerdidaDeTicket ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
int 5
)MinutosDeToleranciaEnSistemaDePagoPorHora <
{= >
get? B
;B C
setD G
;G H
}I J
public 
int B
6MinutosDeToleranciaExcesoEnSistemaDePagoPlanaPorTurnos I
{J K
getL O
;O P
setQ T
;T U
}V W
public 
List 
< 
PeriodoCochera "
>" #7
+PeriodosHabilitadosEnSistemasDePagoAbonados$ O
{P Q
getR U
;U V
setW Z
;Z [
}\ ]
} 
} �(
_D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\ExoneracionDeVehiculo.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
.		$ %
SigesParking		% 1
{

 
public 

class !
ExoneracionDeVehiculo &
{ 
public
int
Id
{
get
;
set
;
}
public 
int 
	IdCochera 
{ 
get "
;" #
set$ '
;' (
}) *
public 
Vehiculo 
Vehiculo  
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
Desde 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
DesdeString !
{" #
get$ '
{( )
return* 0
Desde1 6
.6 7
ToString7 ?
(? @
$str@ T
)T U
;U V
}W X
}Y Z
public 
DateTime 
Hasta 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
HastaString !
{" #
get$ '
{( )
return* 0
Hasta1 6
.6 7
ToString7 ?
(? @
$str@ T
)T U
;U V
}W X
}Y Z
public 
bool 
Vigente 
{ 
get !
;! "
set# &
;& '
}( )
public !
ExoneracionDeVehiculo $
($ %
)% &
{ 	
} 	
public 
List 
< !
ExoneracionDeVehiculo )
>) *
Convert+ 2
(2 3
)3 4
{ 	
return
 
null 
; 
} 
} 
public!! 

class!! &
ExoneracionDeVehiculo_Flat!! +
{"" 
public## 
string## 

{##$ %
get##& )
;##) *
set##+ .
;##. /
}##0 1
public$$ 
string$$ 
DescripcionVehiculo$$ )
{$$* +
get$$, /
;$$/ 0
set$$1 4
;$$4 5
}$$6 7
public%% 
DateTime%% 
Desde%% 
{%% 
get%%  #
;%%# $
set%%% (
;%%( )
}%%* +
public&& 
DateTime&& 
Hasta&& 
{&& 
get&&  #
;&&# $
set&&% (
;&&( )
}&&* +
public'' 
bool'' 
Vigente'' 
{'' 
get'' !
;''! "
set''# &
;''& '
}''( )
public)) &
ExoneracionDeVehiculo_Flat)) )
())) *
)))* +
{** 	
}**
 
public++ &
ExoneracionDeVehiculo_Flat++ )
(++) *!
ExoneracionDeVehiculo++* ?
exoneracion++@ K
)++L M
{,, 	

=-- 
exoneracion-- '
.--' (
Vehiculo--( 0
.--0 1
Placa--1 6
;--6 7
DescripcionVehiculo.. 
=..  !
exoneracion.." -
...- .
Vehiculo... 6
...6 7
NombreCompleto..7 E
;..E F
Desde// 
=// 
exoneracion// 
.//  
Desde//  %
;//% &
Hasta00 
=00 
exoneracion00 
.00  
Hasta00  %
;00% &
Vigente11 
=11 
exoneracion11 !
.11! "
Vigente11" )
;11) *
}22 	
public44 
static44 
List44 
<44 &
ExoneracionDeVehiculo_Flat44 6
>446 7
Convert448 ?
(44@ A
List44A E
<44E F!
ExoneracionDeVehiculo44F [
>44[ \

)44j k
{55 	
List66 
<66 &
ExoneracionDeVehiculo_Flat66 +
>66+ ,
	resultado66- 6
=667 8
new669 <
List66= A
<66A B&
ExoneracionDeVehiculo_Flat66B \
>66\ ]
(66] ^
)66^ _
;66_ `
foreach77 
(77 
var77 
exoneracion77 $
in77% '

)775 6
{88 
	resultado99 
.99 
Add99 
(99 
new99 !&
ExoneracionDeVehiculo_Flat99" <
(99< =
exoneracion99= H
)99H I
)99I J
;99J K
}:: 
return;; 
	resultado;; 
;;; 
}<< 	
}>> 
}?? �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\AtencionSinMesa.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
public		 

class		 
AtencionSinMesa		  
{

 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
int 
IdMesa 
{ 
get 
;  
set! $
;$ %
}& '
public
string
Cliente
{
get
;
set
;
}
public 
decimal 
Total 
{ 
get "
;" #
set$ '
;' (
}) *
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
HoraAtencion "
{# $
get% (
=>) +
Fecha, 1
.1 2
ToString2 :
(: ;
$str; E
)E F
;F G
}H I
public 
bool 

EsDelivery 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
ModoAtencion "
{# $
get% (
=>) +

EsDelivery, 6
?7 8
$str9 C
:D E
$strF O
;O P
}Q R
} 
} �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\CentroAtencionRestaurante.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
public 

class %
CentroAtencionRestaurante *
{		 
public

 
int

 
Id

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
EsPuntoDelivery #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public
bool

{
get
;
set
;
}
} 
} �I
`D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\AtencionRestaurante.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 
AtencionRestaurante

 $
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
Mesa
Mesa
{
get
;
set
;
}
public 
IEnumerable 
< 
Orden_Atencion )
>) *
Ordenes+ 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
decimal 
ImporteAtencion &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
int 

TipoDePago 
{ 
get  #
;# $
set% (
;( )
}* +
public 
List 
< 
DatosVentaIntegrada '
>' (
Comprobantes) 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
int 
Estado 
{ 
get 
;  
set! $
;$ %
}& '
public 
bool 
EsAtencionConMesa %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
bool !
EsAtencionPorDelivery )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
Cliente 
{ 
get  #
;# $
set% (
;( )
}* +
public 
bool 

Confirmado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
EstaRegistrado "
{# $
get% (
=>) +
Estado, 2
==3 5
MaestroSettings6 E
.E F
DefaultF M
.M N,
 IdDetalleMaestroEstadoRegistradoN n
;n o
}p q
public 
AtencionRestaurante "
(" #
)# $
{ 	
this 
. 
Id 
= 
$num 
; 
this 
. 
Mesa 
= 
null 
; 
this 
. 
Ordenes 
= 
new 
List #
<# $
Orden_Atencion$ 2
>2 3
(3 4
)4 5
;5 6
this 
. 
ImporteAtencion  
=! "
$num# $
;$ %
this 
. 

TipoDePago 
= 
$num 
;  
}   	
public"" 
AtencionRestaurante"" "
(""" #
Mesa""# '
mesa""( ,
)"", -
{## 	
this$$ 
.$$ 
Id$$ 
=$$ 
$num$$ 
;$$ 
this%% 
.%% 
Mesa%% 
=%% 
mesa%% 
;%% 
this&& 
.&& 
Ordenes&& 
=&& 
new&& 
List&& #
<&&# $
Orden_Atencion&&$ 2
>&&2 3
(&&3 4
)&&4 5
;&&5 6
this'' 
.'' 
ImporteAtencion''  
=''! "
$num''# $
;''$ %
this(( 
.(( 

TipoDePago(( 
=(( 
$num(( 
;((  
})) 	
public++ 
static++ 
AtencionRestaurante++ )
Convert++* 1
(++1 2
Transaccion++2 =
transaccion++> I
)++I J
{,, 	
var--  
tipoTransaccionOrden-- $
=--% &
RestauranteSettings--' :
.--: ;
Default--; B
.--B C/
#IdTipoTransaccionOrdenDeRestaurante--C f
;--f g
	Data_Mesa// 
dataMesa// 
=//  
JsonConvert//! ,
.//, -
DeserializeObject//- >
<//> ?
	Data_Mesa//? H
>//H I
(//I J
transaccion//J U
.//U V
Actor_negocio4//V d
.//d e
extension_json//e s
)//s t
;//t u
return00 
new00 
AtencionRestaurante00 *
(00* +
)00+ ,
{11 
Id22 
=22 
transaccion22  
.22  !
id22! #
,22# $
ImporteAtencion33 
=33  !
transaccion33" -
.33- .

,33; <
Mesa44 
=44 
new44 
Mesa44 
(44  
)44  !
{55 
Id66 
=66 
(66 
int66 
)66 
transaccion66 )
.66) *%
id_actor_negocio_interno166* C
,66C D

IdAmbiente77 
=77  
(77! "
int77" %
)77% &
transaccion77& 1
.771 2
Actor_negocio4772 @
.77@ A"
id_actor_negocio_padre77A W
,77W X
Nombre88 
=88 
dataMesa88 %
.88% &
nombre88& ,
}99 
,99 

TipoDePago:: 
=:: 
transaccion:: (
.::( )
enum1::) .
,::. /
Ordenes;; 
=;; 
transaccion;; %
.;;% &
Transaccion1;;& 2
.<< 
Where<< 
(<< 
to<< 
=><< 
to<< 
.<<  
id_tipo_transaccion<<  3
==<<4 6 
tipoTransaccionOrden<<7 K
)<<K L
.== 
Select== 
(== 
to== 
=>== 
new== !
Orden_Atencion==" 0
(==0 1
)==1 2
{>> 
Id?? 
=?? 
to?? 
.?? 
id?? 
,?? 
Codigo@@ 
=@@ 
to@@ 
.@@  
codigo@@  &
,@@& '
EstadoAA 
=AA 
(AA 
intAA !
)AA! "
toAA" $
.AA$ %
id_estado_actualAA% 5
,AA5 6

IdAtencionBB 
=BB  
(BB! "
longBB" &
)BB& '
toBB' )
.BB) * 
id_transaccion_padreBB* >
,BB> ?
ImporteOrdenCC  
=CC! "
toCC# %
.CC% &

,CC3 4
IdMesaDD 
=DD 
(DD 
intDD !
)DD! "
transaccionDD" -
.DD- .%
id_actor_negocio_interno1DD. G
,DDG H

NombreMesaEE 
=EE  
dataMesaEE! )
.EE) *
nombreEE* 0
,EE0 1
MozoFF 
=FF 
newFF 
ItemGenericoFF +
(FF+ ,
)FF, -
{GG 
IdHH 
=HH 
toHH 
.HH  $
id_actor_negocio_internoHH  8
,HH8 9
NombreII 
=II  
toII! #
.II# $
Actor_negocio2II$ 2
.II2 3
ActorII3 8
.II8 9

,IIF G
}JJ 
,JJ 
FechaHoraRegistroKK %
=KK& '
toKK( *
.KK* +
fecha_inicioKK+ 7
,KK7 8
DetallesDeOrdenLL #
=LL$ %
toLL& (
.LL( )
Detalle_transaccionLL) <
.LL< =
SelectLL= C
(LLC D
dtLLD F
=>LLG I
newLLJ M
DetalleOrdenLLN Z
(LLZ [
)LL[ \
{MM 
IdNN 
=NN 
dtNN 
.NN  
idNN  "
,NN" #
PrecioOO 
=OO  
dtOO! #
.OO# $
precio_unitarioOO$ 3
,OO3 4
CantidadPP  
=PP! "
dtPP# %
.PP% &
cantidadPP& .
,PP. /
ImporteQQ 
=QQ  !
dtQQ" $
.QQ$ %
totalQQ% *
,QQ* +
EstadoRR 
=RR  
(RR! "
intRR" %
)RR% &
dtRR& (
.RR( )#
indicadorMultipropositoRR) @
,RR@ A&
DetalleItemRestauranteJsonSS 2
=SS3 4
dtSS5 7
.SS7 8
detalleSS8 ?
,SS? @

NombreItemTT "
=TT# $
dtTT% '
.TT' (
Concepto_negocioTT( 8
.TT8 9
nombreTT9 ?
,TT? @
IdItemUU 
=UU  
dtUU! #
.UU# $
id_concepto_negocioUU$ 7
,UU7 8

=VV& '
dtVV( *
.VV* +
id_transaccionVV+ 9
}WW 
)WW 
}XX 
)XX 
,XX 
EstadoYY 
=YY 
(YY 
intYY 
)YY 
transaccionYY )
.YY) *
id_estado_actualYY* :
,YY: ;
}ZZ 
;ZZ
}[[ 	
}\\ 
}^^ �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\CobroAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
public		 

class		 

{

 
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Ambiente 
{  
get! $
;$ %
set& )
;) *
}+ ,
public
string
Mesa
{
get
;
set
;
}
public 
string 
Mozo 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 

{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool 
EstaConfirmado "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} �
XD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Complemento.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
SigesRestaurant		 -
{

 
public 

class 
Complemento 
{ 
public
int
Id
{
get
;
set
;
}
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
MostrarFamilia "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int 
NumeroComplementos %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Familia 
{ 
get  #
;# $
set% (
;( )
}* +
public 
IEnumerable 
< 
Item_Complemento +
>+ , 
Detalles_Complemento- A
{B C
getD G
;G H
setI L
;L M
}N O
public 
bool 

EsMultiple 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
Maximo 
{ 
get 
;  
set! $
;$ %
}& '
public 
bool !
EstaActivoRestaurante )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
Complemento 
( 
) 
{ 	
}
 
}&& 
})) �
sD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Comprobantes\ComprobanteCuentaAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
SigesRestaurant, ;
.; <
Comprobantes< H
{ 
public 

class %
ComprobanteCuentaAtencion *
{ 
public		 %
ComprobanteCuentaAtencion		 (
(		( )
AtencionRestaurante		) <
atencion		= E
,		E F
string		F L!
nombreEstablecimiento		M b
,		b c
DateTime		d l
	fechaHora		m v
)		v w
{

 	
this 
. 
atencion 
= 
atencion $
;$ %
this 
. 
mozo 
= 
atencion  
.  !
Ordenes! (
.( )
Last) -
(- .
). /
./ 0
Mozo0 4
.4 5
Nombre5 ;
;; <
this
.
nombreRestaurant
=
nombreEstablecimiento
;
this 
. 
	fechaHora 
= 
	fechaHora &
;& '
} 	
private 
AtencionRestaurante #
atencion$ ,
;, -
private 
string 
nombreRestaurant '
;' (
private 
string 
mozo 
; 
private 
DateTime 
	fechaHora "
;" #
public 
AtencionRestaurante "
Atencion# +
{, -
get. 1
=>2 4
atencion5 =
;= >
set? B
=>C E
atencionF N
=O P
valueQ V
;V W
}X Y
public 
string 
NombreRestaurant &
{' (
get) ,
=>- /
nombreRestaurant0 @
;@ A
setB E
=>F H
nombreRestaurantI Y
=Z [
value\ a
;a b
}c d
public 
string 
Mozo 
{ 
get  
=>! #
mozo$ (
;( )
set* -
=>. 0
mozo1 5
=6 7
value8 =
;= >
}? @
public 
DateTime 
	FechaHora !
{" #
get$ '
=>( *
	fechaHora+ 4
;4 5
set6 9
=>: <
	fechaHora= F
=G H
valueI N
;N O
}P Q
} 
} �
jD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Comprobantes\ComprobanteOrden.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 

.+ ,
SigesRestaurant, ;
.; <
Comprobantes< H
{ 
public 

class 
ComprobanteOrden !
{ 
public 
ComprobanteOrden 
(  
Orden_Atencion  .
orden/ 4
,4 5
string6 <!
nombreEstablecimiento= R
)R S
{ 	
this		 
.		 
orden		 
=		 
orden		 
;		 
this

 
.

 
nombreRestaurant

 !
=

" #!
nombreEstablecimiento

# 8
;

8 9
} 	
private
Orden_Atencion
orden
;
private 
string 
nombreRestaurant '
;' (
public 
Orden_Atencion 
Orden #
{$ %
get& )
=>* ,
orden- 2
;2 3
set4 7
=>8 :
orden; @
=A B
valueC H
;H I
}J K
public 
string 
NombreRestaurant &
{' (
get) ,
=>- /
nombreRestaurant0 @
;@ A
setB E
=>F H
nombreRestaurantI Y
=Z [
value\ a
;a b
}c d
} 
} �
yD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Configuraciones\ConfiguracionRestauranteCaja.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{ 
public		 

class		 (
ConfiguracionRestauranteCaja		 -
{

 
public 
bool !
UsuarioTieneRolCajero )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
DateTime 

FechaDesde "
{# $
get% (
;( )
set* -
;- .
}/ 0
public
DateTime

FechaHasta
{
get
;
set
;
}
public 
long 
FechaDesdeDefault %
{& '
get( +
{, -
return. 4

FechaDesde5 ?
.? @$
ToJavaScriptMilliseconds@ X
(X Y
)Y Z
;Z [
}\ ]
}^ _
public 
long 
FechaHastaDefault %
{& '
get( +
{, -
return. 4

FechaHasta5 ?
.? @$
ToJavaScriptMilliseconds@ X
(X Y
)Y Z
;Z [
}\ ]
}^ _
public 
string 
FechaDesdeString &
{' (
get) ,
{- .
return/ 5

FechaDesde6 @
.@ A
ToStringA I
(I J
$strJ b
)b c
;c d
}e f
}g h
public 
string 
FechaHastaString &
{' (
get) ,
{- .
return/ 5

FechaHasta6 @
.@ A
ToStringA I
(I J
$strJ b
)b c
;c d
}e f
}g h
public (
ConfiguracionRestauranteCaja +
(+ ,
), -
{ 	
} 	
} 
} �
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Configuraciones\ConfiguracionRestauranteFacturacion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{		 
public

 

class

 /
#ConfiguracionRestauranteFacturacion

 4
{ 
private 
static 
readonly /
#ConfiguracionRestauranteFacturacion  C
defaultInstanceD S
=T U
newV Y/
#ConfiguracionRestauranteFacturacionZ }
(} ~
)~ 
;	 �
public
static
#ConfiguracionRestauranteFacturacion
Default
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
readonly 
int 
TipoDePagoSimple ,
=- .
(/ 0
int0 3
)3 4
TipoPago4 <
.< =
Simple= C
;C D
public 
readonly 
int $
TipoDePagoCuentaDividida 4
=5 6
(7 8
int8 ;
); <
TipoPago< D
.D E
DivididoPorMontoE U
;U V
public 
readonly 
int 1
%TipoDePagoCuentaDiferenciadaDetallada A
=B C
(D E
intE H
)H I
TipoPagoI Q
.Q R
DivididoPorItemR a
;a b
public 
readonly 
int !
IdMedioDePagoEfectivo 1
=2 3
MaestroSettings4 C
.C D
DefaultD K
.K L/
#IdDetalleMaestroMedioDepagoEfectivoL o
;o p
public 
readonly 
int 
	Formato80 %
=& '
(( )
int) ,
), -
FormatoImpresion- =
.= >
_80mm> C
;C D
public 
readonly 
int 
	FormatoA4 %
=& '
(( )
int) ,
), -
FormatoImpresion- =
.= >
A4> @
;@ A
public 
readonly 0
$ConfiguracionRestauranteDetalleOrden <(
ConfiguracionDetallesDeOrden= Y
=Z [1
$ConfiguracionRestauranteDetalleOrden	\ �
.
� �
Default
� �
;
� �
} 
} �
}D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Configuraciones\ConfiguracionRestauranteReportes.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{		 
public

 

class

 ,
 ConfiguracionRestauranteReportes

 1
{ 
private 
static 
readonly ,
 ConfiguracionRestauranteReportes  @
defaultInstanceA P
=Q R
newS V,
 ConfiguracionRestauranteReportesW w
(w x
)x y
;y z
public
static
 ConfiguracionRestauranteReportes
Default
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
readonly 
int  
MaximoDiasAtenciones 0
=1 2
RestauranteSettings3 F
.F G
DefaultG N
.N O(
MaximoDiasConsultaAtencionesO k
;k l
public 
readonly 
int (
MaximoDiasOrdenesPorConcepto 8
=9 :
RestauranteSettings; N
.N O
DefaultO V
.V W0
$MaximoDiasConsultaOrdenesPorConceptoW {
;{ |
public 
readonly 
int $
MaximoDiasOrdenesPorMozo 4
=5 6
RestauranteSettings7 J
.J K
DefaultK R
.R S-
!MaximoDiasConsultasOrdenesPorMozoS t
;t u
public 
readonly 
int '
MaximoDiasOrdenesDetalladas 7
=8 9
RestauranteSettings: M
.M N
DefaultN U
.U V0
$MaximoDiasConsultasOrdenesDetalladasV z
;z {
public 
readonly 
int +
MaximoDiasDevolucionesEnOrdenes ;
=< =
RestauranteSettings> Q
.Q R
DefaultR Y
.Y Z5
(MaximoDiasConsultasDevolucionesEnOrdenes	Z �
;
� �
public 
readonly 
int '
MaximoDiasPorModoAtenciones 7
=8 9
RestauranteSettings: M
.M N
DefaultN U
.U V/
#MaximoDiasConsultaPorModoAtencionesV y
;y z
} 
} �
tD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Consultas\OrdenPorModoAtencion_Consulta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
[

 
Serializable

 
]

 
public 

class )
OrdenPorModoAtencion_Consulta .
{ 
public
DateTime
FechaHoraAtencion
{
get
;
set
;
}
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
CodigoOrden !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Mesa 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Mozo 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
	Facturado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Estado 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
EsAtencionEnSalon %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
bool !
EsAtencionPorDelivery )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
ModoAtencion "
{# $
get% (
=>) +
EsAtencionEnSalon, =
?> ? 
EnumeradosRestaurant@ T
.T U
GetDescriptionU c
(c d
ModoAtencionEnumd t
.t u
Salonu z
)z {
:| }
(~ "
EsAtencionPorDelivery	 �
?
� �"
EnumeradosRestaurant
� �
.
� �
GetDescription
� �
(
� �
ModoAtencionEnum
� �
.
� �
Delivery
� �
)
� �
:
� �"
EnumeradosRestaurant
� �
.
� �
GetDescription
� �
(
� �
ModoAtencionEnum
� �
.
� �
AlPaso
� �
)
� �
)
� �
;
� �
}
� �
public 
string 
Ambiente 
{  
get! $
;$ %
set& )
;) *
}+ ,
public )
OrdenPorModoAtencion_Consulta ,
(, -
)- .
{. /
}/ 0
public 
static 
List 
< )
OrdenPorModoAtencion_Consulta 8
>8 9
Convert: A
(A B
)B C
{ 	
return 
new 
List 
< )
OrdenPorModoAtencion_Consulta 9
>9 :
(: ;
); <
;< =
}   	
}!! 
}"" �
mD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Consultas\OrdenAtencion_Consulta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
[		 
Serializable		 
]		 
public

 

class

 "
OrdenAtencion_Consulta

 '
{ 
public 
DateTime 
FechaHoraAtencion )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public
long

IdAtencion
{
get
;
set
;
}
public 
string 
CodigoOrden !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Mesa 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Mozo 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
	Facturado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Estado 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
EsAtencionEnSalon %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public "
OrdenAtencion_Consulta %
(% &
)& '
{' (
}( )
public 
static 
List 
< "
OrdenAtencion_Consulta 1
>1 2
Convert3 :
(: ;
); <
{ 	
return 
new 
List 
< "
OrdenAtencion_Consulta 2
>2 3
(3 4
)4 5
;5 6
} 	
} 
} �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Detalle_Orden.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
public 

class 
DetalleOrden 
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public		 
string		 

NombreItem		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
public

 $
Detalle_Item_Restaurante

 '"
DetalleItemRestaurante

( >
{

? @
get

A D
;

D E
set

F I
;

I J
}

K L
public 
int 
IdItem 
{ 
get 
;  
set! $
;$ %
}& '
public 
long 

{" #
get$ '
;' (
set) ,
;, -
}. /
public
string
DetalleItemRestauranteJson
{
get
;
set
;
}
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
Estado 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 
EtiquetaEstado $
{% &
get' *
=>+ -
Estado. 4
==5 7
(8 9
int9 <
)< ="
EstadoDeDetalleDeOrden= S
.S T
DevueltoT \
?] ^
$str_ i
:j k
Estadol r
==s u
(v w
intw z
)z {#
EstadoDeDetalleDeOrden	{ �
.
� �
Anulado
� �
?
� �
$str
� �
:
� �
$str
� �
;
� �
}
� �
public 
List 
< 
string 
> 
ValoresComplementos /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
bool 
EsBien 
{ 
get  
;  !
set" %
;% &
}' (
public 
DetalleOrden 
( 
) 
{ 	
} 	
} 
} �
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Familia.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 
Familia

 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
string
Nombre
{
get
;
set
;
}
public 
IEnumerable 
< 
Complemento &
>& '
Complementos( 4
{5 6
get6 9
;9 :
set: =
;= >
}> ?
public 
Familia 
( 
) 
{ 	
} 	
public 
Familia 
( 
Detalle_maestro &
detalleMaestro' 5
)5 6
{ 	
Id 
= 
detalleMaestro 
.  
id  "
;" #
Nombre 
= 
detalleMaestro #
.# $
nombre$ *
;* +
} 	
public 
static 
List 
< 
Familia "
>" #
Convert$ +
(+ ,
List, 0
<0 1
Detalle_maestro1 @
>@ A
detalleB I
)I J
{ 	
return 
null 
; 
} 	
} 
}!! �
`D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Detalle_Complemento.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public 

class $
Detalle_Item_Restaurante )
{ 
public
string
AnotacionIndicacion
{
get
;
set
;
}
public 
string  
AnotacionObservacion *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
List 
< 
Detalle_Complemento '
>' (
DetallesComplemento) <
{= >
get? B
;B C
setD G
;G H
}I J
public $
Detalle_Item_Restaurante '
(' (
)( )
{ 	
}
 
} 
public 

class 
Detalle_Complemento $
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
List 
< 
Item_Complemento $
>$ %
ItemsComplemento& 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
Detalle_Complemento "
(" #
)# $
{ 	
}
 
}// 
public11 

class11 
Item_Complemento11 !
{22 
public33 
int33 
Id33 
{33 
get33 
;33 
set33  
;33  !
}33" #
public44 
string44 
Nombre44 
{44 
get44 "
;44" #
set44$ '
;44' (
}44) *
public77 
Item_Complemento77 
(77  
)77  !
{88 	
}88
 
}99 
}:: �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\InformacionDePago.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 
InformacionPago

  
{ 
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public
List
<
DatosVentaIntegrada
>
datos
{
get
;
set
;
}
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\ItemRestaurante.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 
ItemRestaurante

  
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
string
CodigoBarra
{
get
;
set
;
}
public 
int 
	IdFamilia 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
NombreCompleto $
{% &
get' *
=>+ -
CodigoBarra. 9
==: <
null= A
?B C
NombreD J
:K L
(M N
CodigoBarraN Y
+Z [
$str\ _
+` a
Nombreb h
)h i
;i j
}k l
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
List 
< 
Complemento 
>  !
ComplementosDeFamilia! 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
ItemRestaurante 
( 
Concepto_negocio /
conceptoNegocio0 ?
)? @
{ 	
this 
. 
Id 
= 
conceptoNegocio %
.% &
id& (
;( )
this 
. 
CodigoBarra 
= 
conceptoNegocio .
.. /
codigo_barra/ ;
;; <
this 
. 
	IdFamilia 
= 
conceptoNegocio ,
., -
id_concepto_basico- ?
;? @
this 
. 
Nombre 
= 
conceptoNegocio )
.) *
nombre* 0
;0 1
this 
. 

=  
conceptoNegocio! 0
.0 1
Detalle_maestro41 A
.A B
nombreB H
;H I
} 	
public 
ItemRestaurante 
( 
)  
{ 	
}   	
}"" 
}## �*
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\OrdenAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 
Orden_Atencion

 
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
string
Codigo
{
get
;
set
;
}
public 
IEnumerable 
< 
DetalleOrden '
>' (
DetallesDeOrden) 8
{9 :
get; >
;> ?
set@ C
;C D
}E F
public 
decimal 
ImporteOrden #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
long 

IdAtencion 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
IdMesa 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 

NombreMesa  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
Estado 
{ 
get 
;  
set! $
;$ %
}& '
public 
ItemGenerico 
Mozo  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DateTime 
FechaHoraRegistro )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
Mesa 
Mesa 
{ 
get 
; 
set  #
;# $
}% &
public 
Orden_Atencion 
( 
) 
{ 	
}
 
public 
Orden_Atencion 
( 
Transaccion )
transaccion* 5
)5 6
{ 	
	Data_Mesa 
dataMesa 
=  
JsonConvert! ,
., -
DeserializeObject- >
<> ?
	Data_Mesa? H
>H I
(I J
transaccionJ U
.U V
Actor_negocio4V d
.d e
extension_jsone s
)s t
;t u
Id 
= 
transaccion 
. 
id 
;  
Codigo 
= 
transaccion  
.  !
codigo! '
;' (
Estado   
=   
(   
int   
)   
transaccion   %
.  % &
id_estado_actual  & 6
;  6 7

IdAtencion!! 
=!! 
(!! 
long!! 
)!! 
transaccion!! *
.!!* + 
id_transaccion_padre!!+ ?
;!!? @
ImporteOrden"" 
="" 
transaccion"" &
.""& '

;""4 5
IdMesa## 
=## 
(## 
int## 
)## 
transaccion## %
.##% &%
id_actor_negocio_interno1##& ?
;##? @

NombreMesa$$ 
=$$ 
dataMesa$$ !
.$$! "
nombre$$" (
;$$( )
FechaHoraRegistro%% 
=%% 
transaccion%%  +
.%%+ ,
fecha_inicio%%, 8
;%%8 9
Mesa&& 
=&& 
new&& 
Mesa&& 
(&& 
transaccion&& '
.&&' (
Actor_negocio1&&( 6
)&&6 7
;&&7 8
DetallesDeOrden'' 
='' 
transaccion'' )
.'') *
Detalle_transaccion''* =
.''= >
Select''> D
(''D E
dt''E G
=>''H J
new''K N
DetalleOrden''O [
(''[ \
)''\ ]
{(( 
Id)) 
=)) 
dt)) 
.)) 
id)) 
,)) 
Precio** 
=** 
dt** 
.** 
precio_unitario** +
,**+ ,
Cantidad++ 
=++ 
dt++ 
.++ 
cantidad++ &
,++& '
Importe,, 
=,, 
dt,, 
.,, 
total,, "
,,," #
Estado-- 
=-- 
(-- 
int-- 
)-- 
dt--  
.--  !#
indicadorMultiproposito--! 8
,--8 9&
DetalleItemRestauranteJson.. *
=..+ ,
dt..- /
.../ 0
detalle..0 7
,..7 8

NombreItem// 
=// 
dt// 
.//  
Concepto_negocio//  0
.//0 1
nombre//1 7
,//7 8
IdItem00 
=00 
dt00 
.00 
id_concepto_negocio00 /
,00/ 0

=11 
dt11  "
.11" #
id_transaccion11# 1
}22 
)22
;22 
}33 	
}44 
}55 �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Data_Ambiente.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 


 
{ 
public 
string 
nombre 
; 
public
int
filas
;
public 
int 
columnas 
; 
public 
bool 
mesastemporales "
;" #
} 
} �
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Data_Mesa.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 
	Data_Mesa

 
{ 
public 
string 
nombre 
; 
public
int
fila
;
public 
int 
columna 
; 
} 
} �
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\ResumenAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
public		 

class		 
ResumenAtencion		  
{

 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
NombreAmbiente $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public
int
IdMesa
{
get
;
set
;
}
public 
string 

NombreMesa  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 

NombreMozo  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
decimal 
ImporteTotal #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
Total 
{ 
get !
=>" $
ImporteTotal% 1
.1 2
ToString2 :
(: ;
$str; ?
)? @
;@ A
}B C
public 
bool 
ImporteTotalCero $
{% &
get' *
=>+ -
ImporteTotal. :
==; =
$num> ?
;? @
}A B
public 
bool 
	Facturado 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 

{$ %
get& )
=>* ,
	Facturado- 6
?7 8
$str9 =
:> ?
$str@ D
;D E
}F G
public 
bool 

Confirmado 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
EstaConfirmado $
{% &
get' *
=>+ -

Confirmado. 8
?9 :
$str; ?
:@ A
$strB F
;F G
}H I
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
FechaAtencionString )
{* +
get, /
=>0 2

.@ A
ToStringA I
(I J
$strJ _
)_ `
;` a
}b c
} 
} �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\SesionRestaurante.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{
[ 
Serializable 
] 
public 

class 
SesionRestaurante "
{ 
public 
int 
IdCentroAtencion #
{$ %
get& )
{* +
return+ 1
this2 6
.6 7
SesionDeUsuario7 F
.F G*
IdCentroDeAtencionSeleccionadoG e
;e f
}g h
}i j
public "
UserProfileSessionData %
SesionDeUsuario& 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
List 
< 
Ambiente 
> 
	Ambientes '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} �
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\CentroDeAtencion_.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
{

 
public 

class 
CentroDeAtencion !
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
IdActor 
{ 
get  
;  !
set" %
;% &
}' (
public $
EstablecimientoComercial '$
EstablecimientoComercial( @
{A B
getC F
;F G
setH K
;K L
}M N
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
bool  
SalidaBienesSinStock (
{) *
get+ .
=>/ 1
string2 8
.8 9

(F G

)T U
?V W
falseX ]
:^ _
JsonConvert` k
.k l
DeserializeObjectl }
<} ~!
JsonCentroDeAtencion	~ �
>
� �
(
� �

� �
)
� �
.
� �"
salidabienessinstock
� �
;
� �
}
� �
public 
CentroDeAtencion 
(  
)  !
{ 	
} 	
public"" 
CentroDeAtencion"" 
(""  

actorDeNegocio"". <
)""< =
{## 	
this$$ 
.$$ 
Id$$ 
=$$ 
actorDeNegocio$$ $
.$$$ %
id$$% '
;$$' (
this%% 
.%% 
Codigo%% 
=%% 
actorDeNegocio%% (
.%%( )
codigo_negocio%%) 7
;%%7 8
this&& 
.&& 
Nombre&& 
=&& 
actorDeNegocio&& (
.&&( )
Actor&&) .
.&&. /

;&&< =
}(( 	
public** 
ItemGenerico** 
ToItemGenerico** *
(*** +
)**+ ,
{++ 	
return,, 
new,, 
ItemGenerico,, #
(,,# $
Id,,$ &
,,,& '
Nombre,,( .
),,. /
;,,/ 0
}-- 	
}.. 
}// �J
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\ComboActorComercial.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 
ComboActorComercial

 $
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
string
RazonSocial
{
get
;
set
;
}
public 
string $
NumeroDocumentoIdentidad .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
ComboActorComercial "
(" #
)# $
{ 	
} 	
public 
ComboActorComercial "
(" #
int# &
id' )
)) *
{ 	
this 
. 
Id 
= 
id 
; 
} 	
public 
ComboActorComercial "
(" #
int# &
id' )
,) *
string+ 1
razonSocial2 =
,= >
string? E$
numeroDocuemntoIdentidadF ^
)^ _
{ 	
this 
. 
Id 
= 
id 
; 
this 
. 
RazonSocial 
= 
razonSocial *
;* +
this 
. $
NumeroDocumentoIdentidad )
=* +$
numeroDocuemntoIdentidad, D
;D E
} 	
public 
static 
List 
< 
ComboActorComercial .
>. /
Convert0 7
(7 8
List8 <
<< =
Cliente= D
>D E
clientesF N
)N O
{   	
List!! 
<!! 
ComboActorComercial!! $
>!!$ % 
comboGenericoActores!!& :
=!!; <
new!!= @
List!!A E
<!!E F
ComboActorComercial!!F Y
>!!Y Z
(!!Z [
)!![ \
;!!\ ]
foreach"" 
("" 
var"" 
cleinte""  
in""! #
clientes""$ ,
)"", -
{## 
comboGenericoActores$$ $
.$$$ %
Add$$% (
($$( )
new$$) ,
ComboActorComercial$$- @
($$@ A
cleinte$$A H
.$$H I
Id$$I K
,$$K L
cleinte$$M T
.$$T U
RazonSocial$$U `
,$$` a
cleinte$$b i
.$$i j
DocumentoIdentidad$$j |
)$$| }
)$$} ~
;$$~ 
}%% 
return&&  
comboGenericoActores&& '
;&&' (
}'' 	
public)) 
static)) 
List)) 
<)) 
ComboActorComercial)) .
>)). /
Convert))0 7
())7 8
List))8 <
<))< =
	Proveedor))= F
>))F G
proveedores))H S
)))S T
{** 	
List++ 
<++ 
ComboActorComercial++ $
>++$ % 
comboGenericoActores++& :
=++; <
new++= @
List++A E
<++E F
ComboActorComercial++F Y
>++Y Z
(++Z [
)++[ \
;++\ ]
foreach,, 
(,, 
var,, 

prooveedor,, #
in,,$ &
proveedores,,' 2
),,2 3
{-- 
comboGenericoActores.. $
...$ %
Add..% (
(..( )
new..) ,
ComboActorComercial..- @
(..@ A

prooveedor..A K
...K L
Id..L N
,..N O

prooveedor..P Z
...Z [
RazonSocial..[ f
,..f g

prooveedor..h r
...r s
DocumentoIdentidad	..s �
)
..� �
)
..� �
;
..� �
}// 
return00  
comboGenericoActores00 '
;00' (
}11 	
public33 
static33 
List33 
<33 
ComboActorComercial33 .
>33. /
Convert330 7
(337 8
List338 <
<33< =
Empleado33= E
>33E F
	empleados33G P
)33P Q
{44 	
List55 
<55 
ComboActorComercial55 $
>55$ % 
comboGenericoActores55& :
=55; <
new55= @
List55A E
<55E F
ComboActorComercial55F Y
>55Y Z
(55Z [
)55[ \
;55\ ]
foreach66 
(66 
var66 
empleado66 !
in66" $
	empleados66% .
)66. /
{77 
comboGenericoActores88 $
.88$ %
Add88% (
(88( )
new88) ,
ComboActorComercial88- @
(88@ A
empleado88A I
.88I J
Id88J L
,88L M
empleado88N V
.88V W
NombreCompleto88W e
,88e f
empleado88g o
.88o p
DocumentoIdentidad	88p �
)
88� �
)
88� �
;
88� �
}99 
return::  
comboGenericoActores:: '
;::' (
};; 	
}<< 
public>> 

class>> 
ComboCentroAtencion>> $
{?? 
public@@ 
int@@ 
Id@@ 
{@@ 
get@@ 
;@@ 
set@@  
;@@  !
}@@" #
publicAA 
stringAA 
CodigoAA 
{AA 
getAA "
;AA" #
setAA$ '
;AA' (
}AA) *
publicBB 
stringBB 
NombreBB 
{BB 
getBB "
;BB" #
setBB$ '
;BB' (
}BB) *
publicDD 
ComboCentroAtencionDD "
(DD" #
)DD# $
{EE 	
}GG 	
publicHH 
ComboCentroAtencionHH "
(HH" #
intHH# &
idHH' )
,HH) *
stringHH+ 1
codigoHH2 8
,HH8 9
stringHH: @
nombreHHA G
)HHG H
{II 	
thisJJ 
.JJ 
IdJJ 
=JJ 
idJJ 
;JJ 
thisKK 
.KK 
CodigoKK 
=KK 
codigoKK  
;KK  !
thisLL 
.LL 
NombreLL 
=LL 
nombreLL  
;LL  !
}MM 	
publicOO 
staticOO 
ListOO 
<OO 
ComboCentroAtencionOO .
>OO. /
ConvertOO0 7
(OO7 8
ListOO8 <
<OO< =%
CentroDeAtencionExtendidoOO= V
>OOV W
centrosDeAtencionOOX i
)OOi j
{PP 	
ListQQ 
<QQ 
ComboCentroAtencionQQ $
>QQ$ % 
comboGenericoActoresQQ& :
=QQ; <
newQQ= @
ListQQA E
<QQE F
ComboCentroAtencionQQF Y
>QQY Z
(QQZ [
)QQ[ \
;QQ\ ]
foreachRR 
(RR 
varRR 
itemRR 
inRR  
centrosDeAtencionRR! 2
)RR2 3
{SS 
comboGenericoActoresTT $
.TT$ %
AddTT% (
(TT( )
newTT) ,
ComboCentroAtencionTT- @
(TT@ A
itemTTA E
.TTE F
IdTTF H
,TTH I
itemTTJ N
.TTN O
CodigoTTO U
,TTU V
itemTTW [
.TT[ \
CodigoTT\ b
+TTc d
$strTTe j
+TTk l
itemTTm q
.TTq r%
EstablecimientoComercial	TTr �
.
TT� �

TT� �
+
TT� �
$str
TT� �
+
TT� �
item
TT� �
.
TT� �
Nombre
TT� �
)
TT� �
)
TT� �
;
TT� �
}UU 
returnVV  
comboGenericoActoresVV '
;VV' (
}WW 	
publicYY 
staticYY 
ListYY 
<YY 
ComboCentroAtencionYY .
>YY. /
ConvertYY0 7
(YY7 8
ListYY8 <
<YY< =
EmpleadoYY= E
>YYE F
	empleadosYYG P
)YYP Q
{ZZ 	
List[[ 
<[[ 
ComboCentroAtencion[[ $
>[[$ % 
comboGenericoActores[[& :
=[[; <
new[[= @
List[[A E
<[[E F
ComboCentroAtencion[[F Y
>[[Y Z
([[Z [
)[[[ \
;[[\ ]
foreach\\ 
(\\ 
var\\ 
item\\ 
in\\  
	empleados\\! *
)\\* +
{]] 
comboGenericoActores^^ $
.^^$ %
Add^^% (
(^^( )
new^^) ,
ComboCentroAtencion^^- @
(^^@ A
item^^A E
.^^E F
Id^^F H
,^^H I
item^^J N
.^^N O
Codigo^^O U
,^^U V
item^^W [
.^^[ \
Codigo^^\ b
+^^c d
$str^^e j
+^^k l
item^^m q
.^^q r
NombreCompleto	^^r �
)
^^� �
)
^^� �
;
^^� �
}__ 
return``  
comboGenericoActores`` '
;``' (
}aa 	
}bb 
}cc �
ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Conceptos\ConceptoBasico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 
ConceptoBasico		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public
string
Valor
{
get
;
set
;
}
public 
bool 
EsBien 
{ 
get  
;  !
set" %
;% &
}' (
public 
bool '
TieneCaracteristicasPropias /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
List 
<  
CaracteristicaPropia (
>( )"
CaracteristicasPropias* @
{A B
getC F
;F G
setH K
;K L
}M N
public 
ConceptoBasico 
( 
) 
{ 	"
CaracteristicasPropias "
=# $
new% (
List) -
<- . 
CaracteristicaPropia. B
>B C
(C D
)D E
;E F
} 	
public 
ConceptoBasico 
( 
Detalle_maestro -
detalle. 5
)5 6
{ 	
Id 
= 
detalle 
. 
id 
; 
Nombre 
= 
detalle 
. 
nombre #
;# $
Valor 
= 
detalle 
. 
valor !
;! "
EsBien 
= 
detalle 
. 
valor "
==# %
$str& )
;) *
} 	
public 
ConceptoBasico 
( 
DetalleGenerico -
detalle. 5
)5 6
{   	
Id!! 
=!! 
detalle!! 
.!! 
Id!! 
;!! 
Nombre"" 
="" 
detalle"" 
."" 
Nombre"" #
;""# $
Valor## 
=## 
detalle## 
.## 
Valor## !
;##! "
EsBien$$ 
=$$ 
detalle$$ 
.$$ 
Valor$$ "
==$$# %
$str$$& )
;$$) *
}%% 	
public'' 
static'' 
List'' 
<'' 
ConceptoBasico'' )
>'') *
Convert''+ 2
(''2 3
List''3 7
<''7 8
Detalle_maestro''8 G
>''G H
detalles''I Q
)''Q R
{(( 	
List)) 
<)) 
ConceptoBasico)) 
>))  
	conceptos))! *
=))+ ,
new))- 0
List))1 5
<))5 6
ConceptoBasico))6 D
>))D E
())E F
)))F G
;))G H
foreach** 
(** 
var** 
detalle**  
in**! #
detalles**$ ,
)**, -
{++ 
	conceptos,, 
.,, 
Add,, 
(,, 
new,, !
ConceptoBasico,," 0
(,,0 1
detalle,,1 8
),,8 9
),,9 :
;,,: ;
}-- 
return.. 
	conceptos.. 
;.. 
}// 	
}22 
}33 �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Conceptos\ConceptoDeNegocioParaVenta.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
.		' (
Custom		( .
{

 
}vv �D
kD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionGuiaRemision.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{		 
public

 

class

 0
$ConfiguracionRegistradorGuiaRemision

 5
{ 
public 
readonly 
int *
IdUbigeoSeleccionadoPorDefecto :
=; <

.J K
DefaultK R
.R S5
)idUbigeoSeleccionadoPorDefectoEnProveedorS |
;| }
public
readonly
int
IdUbigeoNoEspecificado
=

.
Default
.
idUbigeoNoEspecificado
;
public 
readonly 
int *
IdDocumentoNotaAlamacenInterna :
=; <
MaestroSettings= L
.L M
DefaultM T
.T U:
-IdDetalleMaestroComprobanteNotaAlmacenInterna	U �
;
� �
public 
string 

;# $
public 
int 
IdUbigeoSede 
;  
public 
readonly 
int )
IdModalidadTrasladoPorDefecto 9
=: ;
MaestroSettings< K
.K L
DefaultL S
.S TA
4IdDetalleMaestroModalidadDeTrasladoTransportePublico	T �
;
� �
public 
readonly 
int &
IdModalidadTrasladoPublico 6
=7 8
MaestroSettings9 H
.H I
DefaultI P
.P QA
4IdDetalleMaestroModalidadDeTrasladoTransportePublico	Q �
;
� �
public 
readonly 
int &
IdModalidadTrasladoPrivado 6
=7 8
MaestroSettings9 H
.H I
DefaultI P
.P QA
4IdDetalleMaestroModalidadDeTrasladoTransportePrivado	Q �
;
� �
public 
readonly 
int $
IdMotivoTrasladoPorVenta 4
=5 6
MaestroSettings7 F
.F G
DefaultG N
.N O4
(IdDetalleMaestroMotivoDeTrasladoPorVentaO w
;w x
public 
readonly 
int %
IdMotivoTrasladoPorCompra 5
=6 7
MaestroSettings8 G
.G H
DefaultH O
.O P5
)IdDetalleMaestroMotivoDeTrasladoPorCompraP y
;y z
public 
readonly 
int *
IdMotivoTrasladoPorImportacion :
=; <
MaestroSettings= L
.L M
DefaultM T
.T U;
.IdDetalleMaestroMotivoDeTrasladoPorImportacion	U �
;
� �
public 
readonly 
int *
IdMotivoTrasladoPorExportacion :
=; <
MaestroSettings= L
.L M
DefaultM T
.T U;
.IdDetalleMaestroMotivoDeTrasladoPorExportacion	U �
;
� �
public 
readonly 
int 4
(IdMotivoTrasladoPorTrasladoAZonaPrimaria D
=E F
MaestroSettingsG V
.V W
DefaultW ^
.^ _E
8IdDetalleMaestroMotivoDeTrasladoPorTrasladoAZonaPrimaria	_ �
;
� �
public 
readonly 
int 9
-IdMotivoTrasladoPorTrasladoEmisorItineranteCP I
=J K
MaestroSettingsL [
.[ \
Default\ c
.c dJ
=IdDetalleMaestroMotivoDeTrasladoPorTrasladoEmisorItineranteCP	d �
;
� �
public 
readonly 
int B
6IdMotivoTrasladoPorVentaSujetaAConfirmacionDeComprador R
=S T
MaestroSettingsU d
.d e
Defaulte l
.l mS
FIdDetalleMaestroMotivoDeTrasladoPorVentaSujetaAConfirmacionDeComprador	m �
;
� �
public 
readonly 
int L
@IdMotivoTrasladoPorTrasladoEntreEstablecimientosDeLaMismaEmpresa \
=] ^
MaestroSettings_ n
.n o
Defaulto v
.v w]
PIdDetalleMaestroMotivoDeTrasladoPorTrasladoEntreEstablecimientosDeLaMismaEmpresa	w �
;
� �
public 
readonly 
int !
IdMotivoTrasladoOtros 1
=2 3
MaestroSettings4 C
.C D
DefaultD K
.K L1
%IdDetalleMaestroMotivoDeTrasladoOtrosL q
;q r
public 
readonly 
int )
IdTipoDeComprobantePorDefecto 9
=: ;
AplicacionSettings< N
.N O
DefaultO V
.V W>
1IdTipoDeComprobantePorDefectoEnSalidaDeMercaderia	W �
;
� �
public 
readonly 
int 
IdRolCliente (
=) *

.8 9
Default9 @
.@ A
IdRolClienteA M
;M N
public 
readonly 
int 
IdRolProveedor *
=+ ,

.: ;
Default; B
.B C
IdRolProveedorC Q
;Q R
public   
readonly   
bool   &
MostrarBuscadorCodigoBarra   7
=  8 9
AplicacionSettings  : L
.  L M
Default  M T
.  T U4
(MostrarBuscadorCodigoBarraEnGuiaRemision  U }
;  } ~
public!! 
readonly!! 
int!! !
ModoSeleccionConcepto!! 1
=!!2 3
AplicacionSettings!!4 F
.!!F G
Default!!G N
.!!N O<
0ModoDeSeleccionDeConceptoDeNegocioEnGuiaRemision!!O 
;	!! �
public"" 
readonly"" 
int"" $
ModoSeleccionTipoFamilia"" 4
=""5 6
AplicacionSettings""7 I
.""I J
Default""J Q
.""Q R6
*ModoDeSeleccionTipoDeFamiliaEnGuiaRemision""R |
;""| }
public## 
readonly## 
int## %
NumeroDecimalesEnCantidad## 5
=##6 7
AplicacionSettings##8 J
.##J K
Default##K R
.##R S%
NumeroDecimalesEnCantidad##S l
;##l m
public$$ 
readonly$$ 
int$$ *
MinimoCaracteresBuscarConcepto$$ :
=$$; <
AplicacionSettings$$= O
.$$O P
Default$$P W
.$$W X;
.MinimoDeCaracteresParaBuscarEnSelectorConcepto	$$X �
;
$$� �
public%% 
readonly%% 
int%% (
TiempoEsperaBusquedaSelector%% 8
=%%9 :
AplicacionSettings%%; M
.%%M N
Default%%N U
.%%U V=
0TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad	%%V �
;
%%� �
public&& 
readonly&& 
int&& 0
$MinimoCaracteresBuscarActorComercial&& @
=&&A B

.&&P Q
Default&&Q X
.&&X YA
4MinimoDeCaracteresParaBuscarEnSelectorActorComercial	&&Y �
;
&&� �
public'' 
readonly'' 
string'' =
1MascaraDeVisualizacionValidacionRegistroProveedor'' P
=''Q R

.''` a
Default''a h
.''h i>
1MascaraDeVisualizacionValidacionRegistroProveedor	''i �
;
''� �
public(( 
string(( 
NumeroDocumentoSede(( )
;(() *
public)) 
readonly)) 
int)) '
InformacionSelectorConcepto)) 7
=))8 9
()): ;
int)); >
)))> ?
	Entidades))? H
.))H I'
InformacionSelectorConcepto))I d
.))d e
Nombre))e k
;))k l
public** 
readonly** 
int** '
IdTipoDocumentoIdentidadRuc** 7
=**8 9

.**G H
Default**H O
.**O P'
IdTipoDocumentoIdentidadRuc**P k
;**k l
public++ 
readonly++ 
int++ '
IdTipoDocumentoIdentidadDni++ 7
=++8 9

.++G H
Default++H O
.++O P'
IdTipoDocumentoIdentidadDni++P k
;++k l
public,, 
readonly,, 
int,, 
IdProveedorGenerico,, /
=,,0 1

.,,? @
Default,,@ G
.,,G H
idProveedorGenerico,,H [
;,,[ \
}.. 
}// �
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Configuraciones\ConfiguracionRestauranteDetalleOrden.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
.		' (
Custom		( .
.		. /
configuraciones		/ >
{

 
public 

class 0
$ConfiguracionRestauranteDetalleOrden 5
{ 
private
static
readonly
$ConfiguracionRestauranteDetalleOrden
defaultInstance
=
new
$ConfiguracionRestauranteDetalleOrden
(	
)

;

public 
static 0
$ConfiguracionRestauranteDetalleOrden :
Default; B
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
readonly 
int 
EstadoRegistrado ,
=- .
(/ 0
int0 3
)3 4"
EstadoDeDetalleDeOrden4 J
.J K

RegistradoK U
;U V
public 
readonly 
int 
EstadoPreparando ,
=- .
(/ 0
int0 3
)3 4"
EstadoDeDetalleDeOrden4 J
.J K
PreparacionK V
;V W
public 
readonly 
int 

=* +
(, -
int- 0
)0 1"
EstadoDeDetalleDeOrden1 G
.G H
ServidoH O
;O P
public 
readonly 
int 
EstadoAtendido *
=+ ,
(- .
int. 1
)1 2"
EstadoDeDetalleDeOrden2 H
.H I
AtendidoI Q
;Q R
public 
readonly 
int 

=* +
(, -
int- 0
)0 1"
EstadoDeDetalleDeOrden1 G
.G H
AnuladoH O
;O P
public 
readonly 
int 
EstadoDevuelto *
=+ ,
(- .
int. 1
)1 2"
EstadoDeDetalleDeOrden2 H
.H I
DevueltoI Q
;Q R
public 
readonly 
int 
EstadoObservado +
=, -
(. /
int/ 2
)2 3"
EstadoDeDetalleDeOrden3 I
.I J
	ObservadoJ S
;S T
} 
}   �
zD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Configuraciones\ConfiguracionRestauranteOrden.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{		 
public

 

class

 )
ConfiguracionRestauranteOrden

 .
{ 
private 
static 
readonly )
ConfiguracionRestauranteOrden  =
defaultInstance> M
=N O
newP S)
ConfiguracionRestauranteOrdenT q
(q r
)r s
;s t
public
static
ConfiguracionRestauranteOrden
Default
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
readonly 
int 
EstadoConfirmado ,
=- .
MaestroSettings/ >
.> ?
Default? F
.F G,
 IdDetalleMaestroEstadoConfirmadoG g
;g h
public 
readonly 
int 

=* +
MaestroSettings, ;
.; <
Default< C
.C D)
IdDetalleMaestroEstadoCerradoD a
;a b
public 
readonly 0
$ConfiguracionRestauranteDetalleOrden <(
ConfiguracionDetallesDeOrden= Y
=Z [1
$ConfiguracionRestauranteDetalleOrden	\ �
.
� �
Default
� �
;
� �
} 
} �.
}D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Configuraciones\ConfiguracionRestauranteAtencion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
configuraciones/ >
{ 
public		 

class		 ,
 ConfiguracionRestauranteAtencion		 1
{

 
public ,
 ConfiguracionRestauranteAtencion /
(/ 0
)0 1
{ 	
} 	
public 
readonly 
int 
IdMedioPagoDefault .
=/ 0
MaestroSettings1 @
.@ A
DefaultA H
.H I/
#IdDetalleMaestroMedioDepagoEfectivoI l
;l m
public 
readonly 
int 
TipoDePagoSimple ,
=- .
(/ 0
int0 3
)3 4
TipoPago4 <
.< =
Simple= C
;C D
public 
readonly 
int $
TipoDePagoCuentaDividida 4
=5 6
(7 8
int8 ;
); <
TipoPago< D
.D E
DivididoPorMontoE U
;U V
public 
readonly 
int 1
%TipoDePagoCuentaDiferenciadaDetallada A
=B C
(D E
intE H
)H I
TipoPagoI Q
.Q R
DivididoPorItemR a
;a b
public 
readonly 
int 
IdEstadoRegistrado .
=/ 0
MaestroSettings1 @
.@ A
DefaultA H
.H I,
 IdDetalleMaestroEstadoRegistradoI i
;i j
public 
readonly 
int 
IdEstadoCerrado +
=, -
MaestroSettings. =
.= >
Default> E
.E F)
IdDetalleMaestroEstadoCerradoF c
;c d
public 
readonly 
int 
IdEstadoFinalizado .
=/ 0
MaestroSettings1 @
.@ A
DefaultA H
.H I,
 IdDetalleMaestroEstadoFinalizadoI i
;i j
public 
readonly 
int 
IdEstadoAnulado +
=, -
MaestroSettings. =
.= >
Default> E
.E F)
IdDetalleMaestroEstadoAnuladoF c
;c d
public 
readonly )
ConfiguracionRestauranteOrden 5 
ConfiguracionDeOrden6 J
=K L)
ConfiguracionRestauranteOrdenM j
.j k
Defaultk r
;r s
public 
readonly 
int 
IdRolCliente (
=) *

.8 9
Default9 @
.@ A
IdRolClienteA M
;M N
public 
readonly 
int 
	IdRolMozo %
=& '
RestauranteSettings( ;
.; <
Default< C
.C D
	IdRolMozoD M
;M N
public 
readonly 
int 
IdRolCajero '
=( )

.7 8
Default8 ?
.? @
IdRolCajero@ K
;K L
public 
readonly 
int 
IdCategoriaNula +
=, -
ConceptoSettings. >
.> ?
Default? F
.F G
IdCategoriaNulaG V
;V W
public 
readonly 
bool %
ModuloPreparacionActivado 6
=7 8
RestauranteSettings9 L
.L M
DefaultM T
.T U%
ModuloPreparacionActivadoU n
;n o
public 
readonly 
bool *
PermitirCierreRapidoDeAtencion ;
=; <
RestauranteSettings= P
.P Q
DefaultQ X
.X Y*
PermitirCierreRapidoDeAtencionY w
;w x
public 
string 
NombreRolCliente &
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public   
int   

IdEmpleado   
{   
get   "
;  " #
set  $ '
;  ' (
}  ) *
public"" 
bool"" !
UsuarioTieneRolCajero"" )
{"") *
get""+ .
;"". /
set""0 3
;""3 4
}""5 6
public## 
bool## 
PermitirVentaEnMesa## '
{##( )
get##* -
;##- .
set##/ 2
;##2 3
}##4 5
public$$ 
bool$$ $
PermitirVentaPorDelivery$$ ,
{$$- .
get$$/ 2
;$$2 3
set$$4 7
;$$7 8
}$$9 :
public%% 
bool%% 
PermitirVentaAlPaso%% '
{%%( )
get%%* -
;%%- .
set%%/ 2
;%%2 3
}%%4 5
public'' 
ItemGenerico'' !
EstablecimientoSesion'' 1
{''2 3
get''4 7
;''7 8
set''9 <
;''< =
}''> ?
public(( 
bool(( 1
%UsuarioTieneRolAdministradorDeNegocio(( 9
{((: ;
get((< ?
;((? @
set((A D
;((D E
}((F G
public)) 
bool)) &
UsuarioTieneRolCajeroYMozo)) .
{))/ 0
get))1 4
;))4 5
set))6 9
;))9 :
})); <
public** 
List** 
<** 
ItemGenerico**  
>**  !
Establecimientos**" 2
{**3 4
get**5 8
;**8 9
set**: =
;**= >
}**? @
}-- 
}.. �/
uD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionRegistroActorComercial.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{		 
public 

class /
#ConfiguracionRegistroActorComercial 4
{ 
public 
readonly 
int %
IdTipoActorPersonaNatural 5
=6 7

.E F
DefaultF M
.M N%
IdTipoActorPersonaNaturalN g
;g h
public 
readonly 
int &
IdTipoActorPersonaJuridica 6
=7 8

.F G
DefaultG N
.N O&
IdTipoActorPersonaJuridicaO i
;i j
public 
readonly 
int '
IdTipoDocumentoIdentidadDni 7
=8 9

.G H
DefaultH O
.O P'
IdTipoDocumentoIdentidadDniP k
;k l
public 
readonly 
int '
IdTipoDocumentoIdentidadRuc 7
=8 9

.G H
DefaultH O
.O P'
IdTipoDocumentoIdentidadRucP k
;k l
public 
readonly 
int 5
)IdTipoTipoDocumentoSeleccionadaPorDefecto E
=F G

.U V
DefaultV ]
.] ^'
IdTipoDocumentoIdentidadDni^ y
;y z
public 
readonly 
int *
IdUbigeoSeleccionadoPorDefecto :
=; <

.J K
DefaultK R
.R S*
IdUbigeoSeleccionadoPorDefectoS q
;q r
public 
readonly 
int "
IdUbigeoNoEspecificado 2
=3 4

.B C
DefaultC J
.J K"
idUbigeoNoEspecificadoK a
;a b
public 
readonly 
bool 2
&PermitirComprobantePorDefectoEnCliente C
=D E

.S T
DefaultT [
.[ \3
&PermitirComprobantePorDefectoEnCliente	\ �
;
� �
public 
readonly 
int 1
%IdComprobantePredeterminadoPorDefecto A
=B C
MaestroSettingsD S
.S T
DefaultT [
.[ \8
+IdDetalleMaestroComprobanteNotaVentaInterna	\ �
;
� �
public 
readonly 
int 
IdRolCliente (
=) *

.8 9
Default9 @
.@ A
IdRolClienteA M
;M N
public 
readonly 
int 
IdRolProveedor *
=+ ,

.: ;
Default; B
.B C
IdRolProveedorC Q
;Q R
public 
readonly 
int 

=* +

.9 :
Default: A
.A B

;O P
public 
readonly 
bool ;
/MostrarBotonCargarActorEnRegistroActorComercial L
=M N

.\ ]
Default] d
.d e<
/MostrarBotonCargarActorEnRegistroActorComercial	e �
;
� �
public 
readonly 
int 
IdNacionPeru (
=) *
MaestroSettings+ :
.: ;
Default; B
.B C&
IdDetalleMaestroNacionPeruC ]
;] ^
public 
string 
FechaActual !
;! "
public 
readonly 
int 
MascaraNoMostrar ,
=- .
(/ 0
int0 3
)3 47
+MascaraVisualizacionValidacionRegistroActor4 _
._ `
	NoMostrar` i
;i j
public 
readonly 
int 
MascaraOpcional +
=, -
(. /
int/ 2
)2 37
+MascaraVisualizacionValidacionRegistroActor3 ^
.^ _
Opcional_ g
;g h
public 
readonly 
int 
MascaraObligatorio .
=/ 0
(1 2
int2 5
)5 67
+MascaraVisualizacionValidacionRegistroActor6 a
.a b
Obligatoriob m
;m n
}!! 
public## 

sealed## 
class## /
#ConfiguracionSelectorActorComercial## ;
{$$ 
public%% 
int%%  
IdEmpleadoPorDefecto%% '
;%%' (
public&& 
readonly&& 
int&& 
IdClientePorDefecto&& /
=&&0 1

.&&? @
Default&&@ G
.&&G H
IdClienteGenerico&&H Y
;&&Y Z
public'' 
readonly'' 
int'' !
IdProveedorPorDefecto'' 1
=''2 3

.''A B
Default''B I
.''I J
idProveedorGenerico''J ]
;''] ^
public(( 
readonly(( 
int(( 
IdRolCliente(( (
=(() *

.((8 9
Default((9 @
.((@ A
IdRolCliente((A M
;((M N
public)) 
readonly)) 
int)) 
IdRolProveedor)) *
=))+ ,

.)): ;
Default)); B
.))B C
IdRolProveedor))C Q
;))Q R
public** 
readonly** 
int** 

=*** +

.**9 :
Default**: A
.**A B

;**O P
}++ 
},, �
jD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Configuraciones\ConfiguracionTrazaDePago.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
{

 
public 

sealed 
class $
ConfiguracionTrazaDePago 0
{ 
private
static
ConfiguracionTrazaDePago
defaultInstance
=
new
ConfiguracionTrazaDePago
(
)
;
public 
static $
ConfiguracionTrazaDePago .
Default/ 6
{ 	
get 
{ 
return 
defaultInstance &
;& '
} 
} 	
public 
static 
void 
Reset  
(  !
)! "
{ 	
defaultInstance 
= 
new !$
ConfiguracionTrazaDePago" :
(: ;
); <
;< =
} 	
public 
readonly 
int 
IdClienteGenerico -
=. /

.= >
Default> E
.E F
IdClienteGenericoF W
;W X
public 
readonly 
int !
IdMedioDePagoEfectivo 1
=2 3
MaestroSettings4 C
.C D
DefaultD K
.K L/
#IdDetalleMaestroMedioDepagoEfectivoL o
;o p
public 
readonly 
int '
IdMedioDePagoTarjetaCredito 7
=8 9
MaestroSettings: I
.I J
DefaultJ Q
.Q R5
)IdDetalleMaestroMedioDePagoTarjetaCreditoR {
;{ |
public 
readonly 
int &
IdMedioDePagoTarjetaDebito 6
=7 8
MaestroSettings9 H
.H I
DefaultI P
.P Q4
(IdDetalleMaestroMedioDePagoTarjetaDebitoQ y
;y z
public 
readonly 
int )
IdMedioDePagoDepositoEnCuenta 9
=: ;
MaestroSettings< K
.K L
DefaultL S
.S T7
+IdDetalleMaestroMedioDepagoDepositoEnCuentaT 
;	 �
public 
readonly 
int &
IdMedioDePagoTransferencia 6
=7 8
MaestroSettings9 H
.H I
DefaultI P
.P Q=
0IdDetalleMaestroMedioDepagoTransferenciaDeFondos	Q �
;
� �
public   
readonly   
int   $
IdMedioDePagoNotaCredito   4
=  5 6
MaestroSettings  7 F
.  F G
Default  G N
.  N O4
(IdDetalleMaestroMedioDePagoNotaDeCredito  O w
;  w x
public!! 
readonly!! 
int!! 
IdMedioDePagoPuntos!! /
=!!0 1
MaestroSettings!!2 A
.!!A B
Default!!B I
.!!I J-
!IdDetalleMaestroMedioDepagoPuntos!!J k
;!!k l
}"" 
}%% �(
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\CuentaBancaria.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 
CuentaBancaria		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
IdActor 
{ 
get  
;  !
set" %
;% &
}' (
public
ItemGenericoBase

TipoCuenta
{
get
;
set
;
}
public 
ItemGenericoBase 
EntidadFinanciera  1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string 
Titular 
{ 
get  #
;# $
set% (
;( )
}* +
public 
ItemGenericoBase 
Moneda  &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
NumeroCuenta "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
	NumeroCCI 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Ubigeo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 

EstaActivo 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
CuentaBancaria 
( 
) 
{ 	
} 	
public;; 

ActorNegocio;; )
(;;) *
);;* +
{<< 	
return== 
new== 

(==$ %
)==% &
{>> 
id?? 
=?? 
this?? 
.?? 
Id?? 
,?? 
Actor@@ 
=@@ 
new@@ 
Actor@@ !
(@@! "
)@@" #
{AA "
id_documento_identidadBB *
=BB+ ,
thisBB- 1
.BB1 2

TipoCuentaBB2 <
.BB< =
IdBB= ?
,BB? @&
numero_documento_identidadCC .
=CC/ 0
thisCC1 5
.CC5 6
NumeroCuentaCC6 B
,CCB C

=DD" #
thisDD$ (
.DD( )
	NumeroCCIDD) 2
,DD2 3
segundo_nombreEE "
=EE# $
thisEE% )
.EE) *
TitularEE* 1
,EE1 2%
id_detalle_multipropositoFF -
=FF. /
thisFF0 4
.FF4 5
EntidadFinancieraFF5 F
.FFF G
IdFFG I
,FFI J&
id_detalle_multiproposito1GG .
=GG/ 0
thisGG1 5
.GG5 6
MonedaGG6 <
.GG< =
IdGG= ?
,GG? @&
informacion_multipropositoHH .
=HH/ 0
thisHH1 5
.HH5 6
UbigeoHH6 <
}II 
,II 

es_vigenteJJ 
=JJ 

EstaActivoJJ '
}KK 
;KK
}LL 	
}MM 
publicNN 

classNN 
ItemGenericoBaseNN !
{OO 
publicPP 
intPP 
IdPP 
{PP 
getPP 
;PP 
setPP  
;PP  !
}PP" #
publicQQ 
stringQQ 
NombreQQ 
{QQ 
getQQ "
;QQ" #
setQQ$ '
;QQ' (
}QQ) *
publicSS 
ItemGenericoBaseSS 
(SS  
)SS  !
{TT 	
}VV 	
publicXX 
ItemGenericoBaseXX 
(XX  
intXX  #
idXX$ &
)XX& '
{YY 	
thisZZ 
.ZZ 
IdZZ 
=ZZ 
idZZ 
;ZZ 
}[[ 	
public]] 
ItemGenericoBase]] 
(]]  
int]]  #
id]]$ &
,]]& '
string]]( .
nombre]]/ 5
)]]5 6
{^^ 	
this__ 
.__ 
Id__ 
=__ 
id__ 
;__ 
this`` 
.`` 
Nombre`` 
=`` 
nombre``  
;``  !
}aa 	
}bb 
publicdd 

classdd  
ItemGenericoBaseLongdd %
{ee 
publicff 
longff 
Idff 
{ff 
getff 
;ff 
setff !
;ff! "
}ff# $
publicgg 
stringgg 
Nombregg 
{gg 
getgg "
;gg" #
setgg$ '
;gg' (
}gg) *
}hh 
}jj �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Operacion\DetalleDeOperacion.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
.		' (
Custom		( .
{

 
public 

class 
DetalleDeOperacion #
{ 
public
long
Id
{
get
;
set
;
}
public &
Concepto_Negocio_Comercial )
Producto* 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
PrecioUnitario %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Neto 
{ 
get !
;! "
set# &
;& '
}( )
public 
decimal 
Igv 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Isc 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
	Descuento  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
Detalle 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Lote 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Registro 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
DateTime 
? 
Vencimiento $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
MascaraDeCalculo &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
decimal 
	Cantidad1  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DetalleDeOperacion !
(! "
)" #
{ 	
} 	
public!! 
DetalleDeOperacion!! !
(!!! "
Detalle_transaccion!!" 5
detalleTransaccion!!6 H
)!!H I
{"" 	
this## 
.## 
Producto## 
=## 
new## &
Concepto_Negocio_Comercial##  :
(##: ;
)##; <
{$$ 
Id%% 
=%% 
detalleTransaccion%% '
.%%' (
Concepto_negocio%%( 8
.%%8 9
id%%9 ;
,%%; <
EsBien&& 
=&& 
detalleTransaccion&& +
.&&+ ,
Concepto_negocio&&, <
.&&< =
EsBien&&= C
}'' 
;''
this(( 
.(( 
PrecioUnitario(( 
=((  !
detalleTransaccion((" 4
.((4 5
precio_unitario((5 D
;((D E
this)) 
.)) 
Cantidad)) 
=)) 
detalleTransaccion)) .
.)). /
cantidad))/ 7
;))7 8
this** 
.** 
Importe** 
=** 
detalleTransaccion** -
.**- .
total**. 3
;**3 4
this++ 
.++ 
Igv++ 
=++ 
(++ 
decimal++ 
)++  
detalleTransaccion++  2
.++2 3
igv++3 6
;++6 7
this,, 
.,, 
Isc,, 
=,, 
(,, 
decimal,, 
),,  
detalleTransaccion,,  2
.,,2 3
isc,,3 6
;,,6 7
this-- 
.-- 
	Descuento-- 
=-- 
(-- 
decimal-- %
)--% &
detalleTransaccion--& 8
.--8 9
	descuento--9 B
;--B C
this.. 
... 
Lote.. 
=.. 
detalleTransaccion.. *
...* +
lote..+ /
;../ 0
this// 
.// 
Detalle// 
=// 
detalleTransaccion// -
.//- .
detalle//. 5
;//5 6
this00 
.00 
Vencimiento00 
=00 
detalleTransaccion00 1
.001 2
vencimiento002 =
;00= >
this11 
.11 
Registro11 
=11 
detalleTransaccion11 .
.11. /
registro11/ 7
;117 8
}22 	
public44 
DetalleDeOperacion44 !
(44! "
Detalle_transaccion44" 5
detalleTransaccion446 H
,44H I
string44J P
mascaraDeCalculo44Q a
)44a b
{55 	
this66 
.66 
Producto66 
=66 
new66 &
Concepto_Negocio_Comercial66  :
(66: ;
)66; <
{77 
Id88 
=88 
detalleTransaccion88 '
.88' (
Concepto_negocio88( 8
.888 9
id889 ;
,88; <
EsBien99 
=99 
detalleTransaccion99 +
.99+ ,
Concepto_negocio99, <
.99< =
EsBien99= C
}:: 
;::
this;; 
.;; 
PrecioUnitario;; 
=;;  !
detalleTransaccion;;" 4
.;;4 5
precio_unitario;;5 D
;;;D E
this<< 
.<< 
Cantidad<< 
=<< 
detalleTransaccion<< .
.<<. /
cantidad<</ 7
;<<7 8
this== 
.== 
Importe== 
=== 
detalleTransaccion== -
.==- .
total==. 3
;==3 4
this>> 
.>> 
Igv>> 
=>> 
(>> 
decimal>> 
)>>  
detalleTransaccion>>  2
.>>2 3
igv>>3 6
;>>6 7
this?? 
.?? 
Isc?? 
=?? 
(?? 
decimal?? 
)??  
detalleTransaccion??  2
.??2 3
isc??3 6
;??6 7
this@@ 
.@@ 
	Descuento@@ 
=@@ 
(@@ 
decimal@@ %
)@@% &
detalleTransaccion@@& 8
.@@8 9
	descuento@@9 B
;@@B C
thisAA 
.AA 
LoteAA 
=AA 
detalleTransaccionAA *
.AA* +
loteAA+ /
;AA/ 0
thisBB 
.BB 
DetalleBB 
=BB 
detalleTransaccionBB -
.BB- .
detalleBB. 5
;BB5 6
thisCC 
.CC 
VencimientoCC 
=CC 
detalleTransaccionCC 1
.CC1 2
vencimientoCC2 =
;CC= >
thisDD 
.DD 
RegistroDD 
=DD 
detalleTransaccionDD .
.DD. /
registroDD/ 7
;DD7 8
thisEE 
.EE 
MascaraDeCalculoEE !
=EE" #
mascaraDeCalculoEE$ 4
;EE4 5
}FF 	
publicHH 
DetalleDeOperacionHH !
(HH! "
VentaMonoDetalleHH" 2
detalleHH3 :
)HH: ;
{II 	
ProductoJJ 
=JJ 
newJJ &
Concepto_Negocio_ComercialJJ 5
(JJ5 6
)JJ6 7
{KK 
IdLL 
=LL 
detalleLL 
.LL 

IdConceptoLL '
,LL' (
EsBienMM 
=MM 
detalleMM  
.MM  !
EsBienMM! '
}NN 
;NN
CantidadOO 
=OO 
detalleOO 
.OO 
CantidadOO '
;OO' (
PrecioUnitarioPP 
=PP 
detallePP $
.PP$ %
PrecioUnitarioPP% 3
;PP3 4
ImporteQQ 
=QQ 
detalleQQ 
.QQ 
ImporteQQ %
;QQ% &
IgvRR 
=RR 
$numRR 
;RR 
IscSS 
=SS 
$numSS 
;SS 
	DescuentoTT 
=TT 
$numTT 
;TT 
LoteUU 
=UU 
nullUU 
;UU 
DetalleVV 
=VV 
$strVV 
;VV 
VencimientoWW 
=WW 
nullWW 
;WW 
RegistroXX 
=XX 
nullXX 
;XX 
MascaraDeCalculoYY 
=YY 
detalleYY &
.YY& '
MascaraDeCalculoYY' 7
;YY7 8
}ZZ 	
public[[ 
static[[ 
List[[ 
<[[ 
DetalleDeOperacion[[ -
>[[- .
Convert[[/ 6
([[6 7
List[[7 ;
<[[; <
VentaMonoDetalle[[< L
>[[L M
ventasMonoDetalle[[N _
)[[_ `
{\\ 	
List]] 
<]] 
DetalleDeOperacion]] #
>]]# $
detallesDeOperacion]]% 8
=]]9 :
new]]; >
List]]? C
<]]C D
DetalleDeOperacion]]D V
>]]V W
(]]W X
)]]X Y
;]]Y Z
foreach^^ 
(^^ 
var^^ 
ventaMonoDetalle^^ )
in^^* ,
ventasMonoDetalle^^- >
)^^> ?
{__ 
detallesDeOperacion`` #
.``# $
Add``$ '
(``' (
new``( +
DetalleDeOperacion``, >
(``> ?
ventaMonoDetalle``? O
)``O P
)``P Q
;``Q R
}aa 
returnbb 
detallesDeOperacionbb &
;bb& '
}cc 	
publicee 
DetalleDeOperacionee !
(ee! "
longee" &
idee' )
,ee) *
intee+ .
idConceptoNegocioee/ @
,ee@ A
stringeeB H
nombreeeI O
,eeO P
decimaleeQ X
cantidadeeY a
,eea b
decimaleec j
precioUnitarioeek y
,eey z
decimal	ee{ �
total
ee� �
,
ee� �
decimal
ee� �
igv
ee� �
,
ee� �
decimal
ee� �
isc
ee� �
,
ee� �
decimal
ee� �
	descuento
ee� �
,
ee� �
string
ee� �

comentario
ee� �
,
ee� �
string
ee� �
lote
ee� �
,
ee� �
DateTime
ee� �
?
ee� �
vencimiento
ee� �
,
ee� �
string
ee� �
registro
ee� �
,
ee� �
bool
ee� �
esBien
ee� �
,
ee� �
string
ee� �
mascaraDeCalculo
ee� �
,
ee� �
List
ee� �
<
ee� �3
%ValorDetalleMaestroDetalleTransaccion
ee� �
>
ee� �
valores
ee� �
)
ee� �
{ff 	
Idgg 
=gg 
idgg 
;gg 
Productohh 
=hh 
newhh &
Concepto_Negocio_Comercialhh 5
(hh5 6
)hh6 7
{ii 
Idjj 
=jj 
idConceptoNegociojj &
,jj& '
Nombrekk 
=kk 
nombrekk 
,kk  
EsBienll 
=ll 
esBienll 
,ll  
}mm 
;mm
Cantidadnn 
=nn 
cantidadnn 
;nn  
PrecioUnitariooo 
=oo 
precioUnitariooo +
;oo+ ,
Importepp 
=pp 
totalpp 
;pp 
Igvqq 
=qq 
igvqq 
;qq 
Iscrr 
=rr 
iscrr 
;rr 
	Descuentoss 
=ss 
	descuentoss !
;ss! "
Lotett 
=tt 
lotett 
;tt 
Detalleuu 
=uu 

comentariouu  
;uu  !
Vencimientovv 
=vv 
vencimientovv %
;vv% &
Registroww 
=ww 
registroww 
;ww  
MascaraDeCalculoxx 
=xx 
mascaraDeCalculoxx /
;xx/ 0
}yy 	
publiczz 
DetalleDeOperacionzz !
(zz! "
longzz" &
idzz' )
,zz) *
intzz+ .
idConceptoNegociozz/ @
,zz@ A
stringzzB H
codigozzI O
,zzO P
stringzzQ W
nombrezzX ^
,zz^ _
decimalzz` g
cantidadzzh p
,zzp q
decimalzzr y
precioUnitario	zzz �
,
zz� �
decimal
zz� �
total
zz� �
,
zz� �
decimal
zz� �
igv
zz� �
,
zz� �
decimal
zz� �
isc
zz� �
,
zz� �
decimal
zz� �
	descuento
zz� �
,
zz� �
string
zz� �

comentario
zz� �
,
zz� �
string
zz� �
lote
zz� �
,
zz� �
DateTime
zz� �
?
zz� �
vencimiento
zz� �
,
zz� �
string
zz� �
registro
zz� �
,
zz� �
bool
zz� �
esBien
zz� �
,
zz� �
string
zz� �
mascaraDeCalculo
zz� �
,
zz� �
List
zz� �
<
zz� �3
%ValorDetalleMaestroDetalleTransaccion
zz� �
>
zz� �
valores
zz� �
)
zz� �
{{{ 	
Id|| 
=|| 
id|| 
;|| 
Producto}} 
=}} 
new}} &
Concepto_Negocio_Comercial}} 5
(}}5 6
)}}6 7
{~~ 
Id 
= 
idConceptoNegocio &
,& '
Codigo
�� 
=
�� 
codigo
�� 
,
��  
Nombre
�� 
=
�� 
nombre
�� 
,
��  
EsBien
�� 
=
�� 
esBien
�� 
,
��  
}
�� 
;
��
Cantidad
�� 
=
�� 
cantidad
�� 
;
��  
PrecioUnitario
�� 
=
�� 
precioUnitario
�� +
;
��+ ,
Importe
�� 
=
�� 
total
�� 
;
�� 
Igv
�� 
=
�� 
igv
�� 
;
�� 
Isc
�� 
=
�� 
isc
�� 
;
�� 
	Descuento
�� 
=
�� 
	descuento
�� !
;
��! "
Lote
�� 
=
�� 
lote
�� 
;
�� 
Detalle
�� 
=
�� 

comentario
��  
;
��  !
Vencimiento
�� 
=
�� 
vencimiento
�� %
;
��% &
Registro
�� 
=
�� 
registro
�� 
;
��  
MascaraDeCalculo
�� 
=
�� 
mascaraDeCalculo
�� /
;
��/ 0
}
�� 	
public
��  
DetalleDeOperacion
�� !
(
��! "
long
��" &
id
��' )
,
��) *
int
��+ .
idConceptoNegocio
��/ @
,
��@ A
int
��B E
idConceptoBasico
��F V
,
��V W
string
��X ^
codigo
��_ e
,
��e f
string
��g m
nombre
��n t
,
��t u
decimal
��v }
cantidad��~ �
,��� �
decimal��� �
precioUnitario��� �
,��� �
decimal��� �
total��� �
,��� �
decimal��� �
igv��� �
,��� �
decimal��� �
isc��� �
,��� �
decimal��� �
	descuento��� �
,��� �
string��� �

comentario��� �
,��� �
string��� �
lote��� �
,��� �
DateTime��� �
?��� �
vencimiento��� �
,��� �
string��� �
registro��� �
,��� �
bool��� �
esBien��� �
,��� �
string��� � 
mascaraDeCalculo��� �
,��� �
List��� �
<��� �5
%ValorDetalleMaestroDetalleTransaccion��� �
>��� �
valores��� �
,��� �
decimal��� �
	cantidad1��� �
)��� �
{
�� 	
Id
�� 
=
�� 
id
�� 
;
�� 
Producto
�� 
=
�� 
new
�� (
Concepto_Negocio_Comercial
�� 5
(
��5 6
)
��6 7
{
�� 
Id
�� 
=
�� 
idConceptoNegocio
�� &
,
��& '
Codigo
�� 
=
�� 
codigo
�� 
,
��  
Nombre
�� 
=
�� 
nombre
�� 
,
��  
EsBien
�� 
=
�� 
esBien
�� 
,
��  
IdConceptoBasico
��  
=
��! "
idConceptoBasico
��# 3
,
��3 4
}
�� 
;
��
Cantidad
�� 
=
�� 
cantidad
�� 
;
��  
PrecioUnitario
�� 
=
�� 
precioUnitario
�� +
;
��+ ,
Importe
�� 
=
�� 
total
�� 
;
�� 
Igv
�� 
=
�� 
igv
�� 
;
�� 
Isc
�� 
=
�� 
isc
�� 
;
�� 
	Descuento
�� 
=
�� 
	descuento
�� !
;
��! "
Lote
�� 
=
�� 
lote
�� 
;
�� 
Detalle
�� 
=
�� 

comentario
��  
;
��  !
Vencimiento
�� 
=
�� 
vencimiento
�� %
;
��% &
Registro
�� 
=
�� 
registro
�� 
;
��  
MascaraDeCalculo
�� 
=
�� 
mascaraDeCalculo
�� /
;
��/ 0
	Cantidad1
�� 
=
�� 
	cantidad1
�� !
;
��! "
}
�� 	
public
��  
DetalleDeOperacion
�� !
(
��! "
long
��" &
id
��' )
,
��) *
int
��+ .
idConceptoNegocio
��/ @
,
��@ A
decimal
��B I
cantidad
��J R
,
��R S
decimal
��T [
precioUnitario
��\ j
,
��j k
decimal
��l s
total
��t y
,
��y z
decimal��{ �
igv��� �
,��� �
decimal��� �
isc��� �
,��� �
decimal��� �
	descuento��� �
,��� �
string��� �

comentario��� �
,��� �
string��� �
lote��� �
,��� �
DateTime��� �
?��� �
vencimiento��� �
,��� �
string��� �
registro��� �
,��� �
bool��� �
esBien��� �
,��� �
string��� � 
mascaraDeCalculo��� �
,��� �
List��� �
<��� �5
%ValorDetalleMaestroDetalleTransaccion��� �
>��� �
valores��� �
)��� �
{
�� 	
Id
�� 
=
�� 
id
�� 
;
�� 
Producto
�� 
=
�� 
new
�� (
Concepto_Negocio_Comercial
�� 5
(
��5 6
)
��6 7
{
�� 
Id
�� 
=
�� 
idConceptoNegocio
�� &
,
��& '
EsBien
�� 
=
�� 
esBien
�� 
,
��  
}
�� 
;
��
PrecioUnitario
�� 
=
�� 
precioUnitario
�� +
;
��+ ,
Cantidad
�� 
=
�� 
cantidad
�� 
;
��  
Importe
�� 
=
�� 
total
�� 
;
�� 
Igv
�� 
=
�� 
igv
�� 
;
�� 
Isc
�� 
=
�� 
isc
�� 
;
�� 
	Descuento
�� 
=
�� 
	descuento
�� !
;
��! "
Lote
�� 
=
�� 
lote
�� 
;
�� 
Detalle
�� 
=
�� 

comentario
��  
;
��  !
Vencimiento
�� 
=
�� 
vencimiento
�� %
;
��% &
Registro
�� 
=
�� 
registro
�� 
;
��  
MascaraDeCalculo
�� 
=
�� 
mascaraDeCalculo
�� /
;
��/ 0
}
�� 	
public
��  
DetalleDeOperacion
�� !
(
��! "
int
��" %
idConceptoNegocio
��& 7
,
��7 8
decimal
��9 @
cantidad
��A I
,
��I J
decimal
��K R
precioUnitario
��S a
,
��a b
decimal
��c j
total
��k p
,
��p q
decimal
��r y
igv
��z }
,
��} ~
decimal�� �
isc��� �
,��� �
decimal��� �
	descuento��� �
,��� �
string��� �

comentario��� �
,��� �
string��� �
lote��� �
,��� �
DateTime��� �
?��� �
vencimiento��� �
,��� �
string��� �
registro��� �
,��� �
bool��� �
esBien��� �
,��� �
string��� � 
mascaraDeCalculo��� �
,��� �
List��� �
<��� �5
%ValorDetalleMaestroDetalleTransaccion��� �
>��� �
valores��� �
)��� �
{
�� 	
Producto
�� 
=
�� 
new
�� (
Concepto_Negocio_Comercial
�� 5
(
��5 6
)
��6 7
{
�� 
Id
�� 
=
�� 
idConceptoNegocio
�� &
,
��& '
EsBien
�� 
=
�� 
esBien
�� 
,
��  
}
�� 
;
��
PrecioUnitario
�� 
=
�� 
precioUnitario
�� +
;
��+ ,
Cantidad
�� 
=
�� 
cantidad
�� 
;
��  
Importe
�� 
=
�� 
total
�� 
;
�� 
Igv
�� 
=
�� 
igv
�� 
;
�� 
Isc
�� 
=
�� 
isc
�� 
;
�� 
	Descuento
�� 
=
�� 
	descuento
�� !
;
��! "
Lote
�� 
=
�� 
lote
�� 
;
�� 
Detalle
�� 
=
�� 

comentario
��  
;
��  !
Vencimiento
�� 
=
�� 
vencimiento
�� %
;
��% &
Registro
�� 
=
�� 
registro
�� 
;
��  
MascaraDeCalculo
�� 
=
�� 
mascaraDeCalculo
�� /
;
��/ 0
}
�� 	
public
�� !
Detalle_transaccion
�� " 
DetalleTransaccion
��# 5
(
��5 6
)
��6 7
{
�� 	
return
�� 
new
�� !
Detalle_transaccion
�� *
(
��* +
Id
��+ -
,
��- .
Cantidad
��/ 7
,
��7 8
Producto
��9 A
.
��A B
Id
��B D
,
��D E
Detalle
��F M
,
��M N
PrecioUnitario
��O ]
,
��] ^
Importe
��_ f
,
��f g
null
��h l
,
��l m
$num
��n o
,
��o p
null
��q u
,
��u v
null
��w {
,
��{ |
Isc��} �
,��� �
Igv��� �
,��� �
	Descuento��� �
,��� �
Lote��� �
,��� �
Vencimiento��� �
,��� �
Registro��� �
,��� �
	Cantidad1��� �
)��� �
;��� �
}
�� 	
public
��  
DetalleDeOperacion
�� !
Clone
��" '
(
��' (
)
��( )
{
�� 	
return
�� 
new
��  
DetalleDeOperacion
�� )
(
��) *
Producto
��* 2
.
��2 3
Id
��3 5
,
��5 6
Cantidad
��7 ?
,
��? @
PrecioUnitario
��A O
,
��O P
Importe
��Q X
,
��X Y
Igv
��Z ]
,
��] ^
Isc
��_ b
,
��b c
	Descuento
��d m
,
��m n
Detalle
��o v
,
��v w
Lote
��x |
,
��| }
Vencimiento��~ �
,��� �
Registro��� �
,��� �
Producto��� �
.��� �
EsBien��� �
,��� � 
MascaraDeCalculo��� �
,��� �
null��� �
)��� �
;��� �
}
�� 	
public
�� 
static
�� 
List
�� 
<
��  
DetalleDeOperacion
�� -
>
��- .
Clone
��/ 4
(
��4 5
List
��5 9
<
��9 : 
DetalleDeOperacion
��: L
>
��L M(
detallesDeOperacionAClonar
��N h
)
��h i
{
�� 	
List
�� 
<
��  
DetalleDeOperacion
�� #
>
��# $)
detallesDeOperacionClonados
��% @
=
��A B
new
��C F
List
��G K
<
��K L 
DetalleDeOperacion
��L ^
>
��^ _
(
��_ `
)
��` a
;
��a b
foreach
�� 
(
�� 
var
�� '
detalleDeOperacionAClonar
�� 2
in
��3 5(
detallesDeOperacionAClonar
��6 P
)
��P Q
{
�� 
detallesDeOperacionClonados
�� +
.
��+ ,
Add
��, /
(
��/ 0'
detalleDeOperacionAClonar
��0 I
.
��I J
Clone
��J O
(
��O P
)
��P Q
)
��Q R
;
��R S
}
�� 
return
�� )
detallesDeOperacionClonados
�� .
;
��. /
}
�� 	
public
�� 
static
�� !
Detalle_transaccion
�� )
Convert
��* 1
(
��1 2 
DetalleDeOperacion
��2 D 
detalleDeOperacion
��E W
)
��W X
{
�� 	
return
�� 
new
�� !
Detalle_transaccion
�� *
(
��* + 
detalleDeOperacion
��+ =
.
��= >
Id
��> @
,
��@ A 
detalleDeOperacion
��B T
.
��T U
Cantidad
��U ]
,
��] ^ 
detalleDeOperacion
��_ q
.
��q r
Producto
��r z
.
��z {
Id
��{ }
,
��} ~!
detalleDeOperacion�� �
.��� �
Detalle��� �
,��� �"
detalleDeOperacion��� �
.��� �
PrecioUnitario��� �
,��� �"
detalleDeOperacion��� �
.��� �
Importe��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �"
detalleDeOperacion��� �
.��� �
	Descuento��� �
,��� �"
detalleDeOperacion��� �
.��� �
Lote��� �
,��� �"
detalleDeOperacion��� �
.��� �
Vencimiento��� �
,��� �"
detalleDeOperacion��� �
.��� �
Registro��� �
)��� �
;��� �
}
�� 	
public
�� 
static
��  
DetalleDeOperacion
�� (
Convert
��) 0
(
��0 1!
Detalle_transaccion
��1 D 
detalleTransaccion
��E W
)
��W X
{
�� 	
return
�� 
new
��  
DetalleDeOperacion
�� )
(
��) * 
detalleTransaccion
��* <
.
��< =
id
��= ?
,
��? @ 
detalleTransaccion
��A S
.
��S T!
id_concepto_negocio
��T g
,
��g h 
detalleTransaccion
��i {
.
��{ |
Concepto_negocio��| �
.��� �"
id_concepto_basico��� �
,��� �"
detalleTransaccion��� �
.��� � 
Concepto_negocio��� �
.��� �
codigo��� �
,��� �"
detalleTransaccion��� �
.��� � 
Concepto_negocio��� �
.��� �
nombre��� �
,��� �"
detalleTransaccion��� �
.��� �
cantidad��� �
,��� �"
detalleTransaccion��� �
.��� �
precio_unitario��� �
,��� �"
detalleTransaccion��� �
.��� �
total��� �
,��� �
(��� �
decimal��� �
)��� �"
detalleTransaccion��� �
.��� �
igv��� �
,��� �
(��� �
decimal��� �
)��� �"
detalleTransaccion��� �
.��� �
isc��� �
,��� �
(��� �
decimal��� �
)��� �"
detalleTransaccion��� �
.��� �
	descuento��� �
,��� �"
detalleTransaccion��� �
.��� �
detalle��� �
,��� �"
detalleTransaccion��� �
.��� �
lote��� �
,��� �"
detalleTransaccion��� �
.��� �
vencimiento��� �
,��� �"
detalleTransaccion��� �
.��� �
registro��� �
,��� �"
detalleTransaccion��� �
.��� � 
Concepto_negocio��� �
.��� �
EsBien��� �
,��� �
VentasSettings��� �
.��� �
Default��� �
.��� �6
&MascaraDeCalculoDeNingunValorCalculado��� �
,��� �
null��� �
,��� �"
detalleTransaccion��� �
.��� �

cantidad_1��� �
)��� �
;��� �
}
�� 	
public
�� 
static
�� 
List
�� 
<
��  
DetalleDeOperacion
�� -
>
��- .
Convert
��/ 6
(
��6 7
List
��7 ;
<
��; <!
Detalle_transaccion
��< O
>
��O P!
detallesTransaccion
��Q d
)
��d e
{
�� 	
List
�� 
<
��  
DetalleDeOperacion
�� #
>
��# $!
detallesDeOperacion
��% 8
=
��9 :
new
��; >
List
��? C
<
��C D 
DetalleDeOperacion
��D V
>
��V W
(
��W X
)
��X Y
;
��Y Z
foreach
�� 
(
�� 
var
��  
detalleTransaccion
�� +
in
��, .!
detallesTransaccion
��/ B
)
��B C
{
�� 
detallesDeOperacion
�� #
.
��# $
Add
��$ '
(
��' (
Convert
��( /
(
��/ 0 
detalleTransaccion
��0 B
)
��B C
)
��C D
;
��D E
}
�� 
return
�� !
detallesDeOperacion
�� &
;
��& '
}
�� 	
public
�� 
static
�� 
List
�� 
<
�� !
Detalle_transaccion
�� .
>
��. /
Convert
��0 7
(
��7 8
List
��8 <
<
��< = 
DetalleDeOperacion
��= O
>
��O P!
detallesDeOperacion
��Q d
)
��d e
{
�� 	
List
�� 
<
�� !
Detalle_transaccion
�� $
>
��$ %!
detallesTransaccion
��& 9
=
��: ;
new
��< ?
List
��@ D
<
��D E!
Detalle_transaccion
��E X
>
��X Y
(
��Y Z
)
��Z [
;
��[ \
foreach
�� 
(
�� 
var
��  
detalleDeOperacion
�� +
in
��, .!
detallesDeOperacion
��/ B
)
��B C
{
�� 
detallesTransaccion
�� #
.
��# $
Add
��$ '
(
��' (
Convert
��( /
(
��/ 0 
detalleDeOperacion
��0 B
)
��B C
)
��C D
;
��D E
}
�� 
return
�� !
detallesTransaccion
�� &
;
��& '
}
�� 	
public
�� %
OperacionGenericaNivel3
�� &
	Operacion
��' 0
(
��0 1
)
��1 2
{
�� 	
return
�� 
new
�� %
OperacionGenericaNivel3
�� .
(
��. /
)
��/ 0
;
��0 1
}
�� 	
}
�� 
}�� �(
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\EstadoCuentaCliente_VentaCobro\DetalleEstadoCuentaCliente_VentaCobro.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 1
%DetalleEstadoCuentaCliente_VentaCobro		 6
{

 
public 
long 
IdOperacion 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public
List
<
DetalleDeVenta
>
DetallesDeVenta
{
get
;
set
;
}
public 
decimal 
Total 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 

{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
Cobro 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 
Saldo 
{ 
get "
;" #
set$ '
;' (
}) *
public 1
%DetalleEstadoCuentaCliente_VentaCobro 4
(4 5
)5 6
{ 	
}
 
public 
static 
List 
< 1
%DetalleEstadoCuentaCliente_VentaCobro @
>@ A
ConvertB I
(I J
)J K
{ 	
return 
new 
List 
< 1
%DetalleEstadoCuentaCliente_VentaCobro A
>A B
(B C
)C D
;D E
} 	
} 
public 

class 
DetalleDeVenta 
{ 
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public   
decimal   
Cantidad   
{    !
get  " %
;  % &
set  ' *
;  * +
}  , -
public!! 
decimal!! 
Importe!! 
{!!  
get!!! $
;!!$ %
set!!& )
;!!) *
}!!+ ,
public"" 
decimal"" 
PrecioUnitario"" %
{## 	
get$$ 
{%% 
return%% 
Cantidad%% 
!=%%  
$num%%! "
?%%# $
Importe%%% ,
/%%- .
Cantidad%%/ 7
:%%8 9
$num%%: ;
;%%; <
}%%= >
}&& 	
public'' 
DetalleDeVenta'' 
('' 
)'' 
{(( 	
}((
 
public** 
static** 
List** 
<** 
DetalleDeVenta** )
>**) *
Convert**+ 2
(**2 3
)**3 4
{++ 	
return,, 
new,, 
List,, 
<,, 
DetalleDeVenta,, *
>,,* +
(,,+ ,
),,, -
;,,- .
}-- 	
}.. 
public00 

class00 (
DetalleTransaccionVentaCobro00 -
{11 
public22 
long22 
IdOperacion22 
{22  !
get22" %
;22% &
set22' *
;22* +
}22, -
public33 
DateTime33 
Fecha33 
{33 
get33  #
;33# $
set33% (
;33( )
}33* +
public44 
string44 
Codigo44 
{44 
get44 "
;44" #
set44$ '
;44' (
}44) *
public55 
string55 
Concepto55 
{55  
get55! $
;55$ %
set55& )
;55) *
}55+ ,
public66 
decimal66 
Cantidad66 
{66  !
get66" %
;66% &
set66' *
;66* +
}66, -
public77 
decimal77 
Importe77 
{77  
get77! $
;77$ %
set77& )
;77) *
}77+ ,
public88 
decimal88 
PrecioUnitario88 %
{99 	
get:: 
{;; 
return;; 
Cantidad;; 
!=;;  
$num;;! "
?;;# $
Importe;;% ,
/;;- .
Cantidad;;/ 7
:;;8 9
$num;;: ;
;;;; <
};;= >
}<< 	
public== 
decimal== 
Cobro== 
{== 
get== "
;==" #
set==$ '
;==' (
}==) *
}>> 
}?? �
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\EstadoCuentaCliente_VentaCobro\EstadoCuentaCliente_VentaCobro.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 *
EstadoCuentaCliente_VentaCobro		 /
{

 
public 
decimal 
	IdCliente  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
Cliente 
{ 
get  #
;# $
set% (
;( )
}* +
public
%ResumenEstadoCuentaCliente_VentaCobro
Resumen
{
get
;
set
;
}
public 
List 
< 1
%DetalleEstadoCuentaCliente_VentaCobro 9
>9 :
Detalles; C
{D E
getF I
;I J
setK N
;N O
}P Q
public *
EstadoCuentaCliente_VentaCobro -
(- .
). /
{ 	
}
 
public 
static 
List 
< *
EstadoCuentaCliente_VentaCobro 9
>9 :
Convert; B
(B C
)C D
{ 	
return 
new 
List 
< *
EstadoCuentaCliente_VentaCobro :
>: ;
(; <
)< =
;= >
} 	
} 
} �
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\EstadoCuentaCliente_VentaCobro\ResumenEstadoCuentaCliente_VentaCobro.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 1
%ResumenEstadoCuentaCliente_VentaCobro		 6
{

 
public 
decimal 

{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
EntregaTotal #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public
decimal

CobroTotal
{
get
;
set
;
}
public 
decimal 

SaldoFinal !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 1
%ResumenEstadoCuentaCliente_VentaCobro 4
(4 5
)5 6
{ 	
}
 
public 
static 
List 
< 1
%ResumenEstadoCuentaCliente_VentaCobro @
>@ A
ConvertB I
(I J
)J K
{ 	
return 
new 
List 
< 1
%ResumenEstadoCuentaCliente_VentaCobro A
>A B
(B C
)C D
;D E
} 	
} 
} ��
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\FacturacionPagoVenta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{
public 

partial 
class 
DatosVentaIntegrada ,
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
DatosOrdenVenta 
Orden $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
	DatosPago 
Pago 
{ 
get  #
;# $
set% (
;( )
}* +
public $
DatosMovimientoDeAlmacen '
MovimientoAlmacen( 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
DateTime 

{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
Transaccion 
TransaccionOrigen ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
List 
< 
Transaccion 
>  "
TransaccionesModificar! 7
{8 9
get: =
;= >
set? B
;B C
}D E
public 
Transaccion 
TransaccionCreacion .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 

ActorReferencia ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
Estado_transaccion !
NuevoEstado" -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
bool 
EsVentaModoCaja #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
DatosVentaIntegrada "
(" #
)# $
{% &
}' (
public 
DatosVentaIntegrada "
(" #
RegistroDeVenta# 2
registroDeVenta3 B
)B C
{ 	
Orden 
= 
new 
DatosOrdenVenta '
(' (
registroDeVenta( 7
)7 8
;8 9
Pago 
= 
new 
	DatosPago  
(  !
registroDeVenta! 0
)0 1
;1 2
MovimientoAlmacen   
=   
new    #$
DatosMovimientoDeAlmacen  $ <
(  < =
registroDeVenta  = L
)  L M
;  M N

=!! 
registroDeVenta!! +
.!!+ ,

;!!9 :
}"" 	
public$$ 
static$$ 
DatosVentaIntegrada$$ )
Convert$$* 1
($$1 2
Transaccion$$2 =
ordenDeVenta$$> J
)$$J K
{%% 	
DatosVentaIntegrada'' 

datosVenta''  *
=''+ ,
new''- 0
DatosVentaIntegrada''1 D
(''D E
)''E F
{(( 
Orden)) 
=)) 
new)) 
DatosOrdenVenta)) +
())+ ,
new)), /
OrdenDeVenta))0 <
())< =
ordenDeVenta))= I
)))I J
)))J K
,))K L
Pago** 
=** 
null** 
,** 
MovimientoAlmacen++ !
=++" #
null++$ (
,++( )

=,, 
ordenDeVenta,,  ,
.,,, -"
fecha_registro_sistema,,- C
,,,C D
EsVentaModoCaja-- 
=--  !
false--" '
}.. 
;..
return// 

datosVenta// 
;// 
}00 	
}11 
}22 
public44 
partial44 
class44 
DatosOrdenVenta44 $
{55 
public66 

long66 
Id66 
{66 
get66 
;66 
set66 
;66 
}66  
public77 

ItemGenerico77 
PuntoDeVenta77 $
{77% &
get77' *
;77* +
set77, /
;77/ 0
}771 2
public88 

ItemGenerico88 
Vendedor88  
{88! "
get88# &
;88& '
set88( +
;88+ ,
}88- .
public99 

ActorComercial_99 
Cliente99 "
{99# $
get99% (
;99( )
set99* -
;99- .
}99/ 0
public:: 
!
ComprobanteDeNegocio_::  
Comprobante::! ,
{::- .
get::/ 2
;::2 3
set::4 7
;::7 8
}::9 :
public;; 

List;; 
<;; 
DetalleDeOperacion;; "
>;;" #
Detalles;;$ ,
{;;- .
get;;/ 2
;;;2 3
set;;4 7
;;;7 8
};;9 :
public<< 

bool<< &
AplicarIGVCuandoEsAmazonia<< *
{<<+ ,
get<<- 0
;<<0 1
set<<2 5
;<<5 6
}<<7 8
public== 

bool== 
UnificarDetalles==  
{==! "
get==# &
;==& '
set==( +
;==+ ,
}==- .
public>> 

string>> !
ValorDetalleUnificado>> '
{>>( )
get>>* -
;>>- .
set>>/ 2
;>>2 3
}>>4 5
public?? 

string?? 
Observacion?? 
{?? 
get??  #
;??# $
set??% (
;??( )
}??* +
public@@ 

decimal@@ 
Total@@ 
{@@ 
get@@ 
;@@ 
set@@  #
;@@# $
}@@% &
publicAA 

decimalAA 
FleteAA 
{AA 
getAA 
;AA 
setAA  #
;AA# $
}AA% &
publicBB 

intBB "
NumeroBolsasDePlasticoBB %
{BB& '
getBB( +
;BB+ ,
setBB- 0
;BB0 1
}BB2 3
publicCC 

decimalCC 
IcbperCC 
{CC 
getCC 
;CC  
setCC! $
;CC$ %
}CC& '
publicDD 

stringDD 
PlacaDD 
{DD 
getDD 
;DD 
setDD "
;DD" #
}DD$ %
publicEE 

stringEE 
InformacionEE 
{EE 
getEE  #
;EE# $
setEE% (
;EE( )
}EE* +
publicFF 

boolFF 

{FF 
getFF  #
;FF# $
setFF% (
;FF( )
}FF* +
publicGG 

DateTimeGG 
FechaEmisionGG  
{GG! "
getGG# &
;GG& '
setGG( +
;GG+ ,
}GG- .
publicHH 

intHH 
IdEstadoHH 
{HH 
getHH 
;HH 
setHH "
;HH" #
}HH$ %
publicII 

intII 
IdTransaccionPadreII !
{II" #
getII$ '
;II' (
setII) ,
;II, -
}II. /
publicJJ 

intJJ $
IdTipoComprobanteaEmitirJJ '
{JJ( )
getJJ* -
;JJ- .
setJJ/ 2
;JJ2 3
}JJ4 5
publicKK 

boolKK "
EsOperacionPreGeneradaKK &
{KK' (
getKK) ,
;KK, -
setKK. 1
;KK1 2
}KK3 4
publicLL 

longLL "
IdOperacionPreGeneradaLL &
{LL' (
getLL) ,
;LL, -
setLL. 1
;LL1 2
}LL3 4
publicMM 

DatosOrdenVentaMM 
(MM 
)MM 
{MM 
}MM  
publicNN 

DatosOrdenVentaNN 
(NN 
OrdenDeVentaNN '
ordenNN( -
)NN- .
{OO 
PuntoDeVentaQQ 
=QQ 
newQQ 
ItemGenericoQQ '
(QQ' (
ordenQQ( -
.QQ- .
IdPuntoDeVentaQQ. <
,QQ< =
$strQQ> @
)QQ@ A
;QQA B
VendedorRR 
=RR 
newRR 
ItemGenericoRR #
(RR# $
ordenRR$ )
.RR) *

IdVendedorRR* 4
,RR4 5
$strRR6 8
)RR8 9
;RR9 :
ClienteSS 
=SS 
newSS 
ActorComercial_SS %
(SS% &
ordenSS& +
.SS+ ,
ClienteSS, 3
(SS3 4
)SS4 5
.SS5 6
ActorDeNegocioSS6 D
)SSD E
;SSE F
ComprobanteTT 
=TT 
newTT !
ComprobanteDeNegocio_TT /
(TT/ 0
)TT0 1
{UU 	
TipoVV 
=VV 
newVV 
ItemGenericoVV #
(VV# $
ordenVV$ )
.VV) *
ComprobanteVV* 5
(VV5 6
)VV6 7
.VV7 8
IdTipoVV8 >
,VV> ?
ordenVV@ E
.VVE F
ComprobanteVVF Q
(VVQ R
)VVR S
.VVS T
TipoVVT X
(VVX Y
)VVY Z
.VVZ [
NombreVV[ a
)VVa b
,VVb c
SerieWW 
=WW 
newWW 
SerieComprobante_WW )
(WW) *
(WW* +
intWW+ .
)WW. /
ordenWW/ 4
.WW4 5
ComprobanteWW5 @
(WW@ A
)WWA B
.WWB C
IdSerieWWC J
,WWJ K
ordenWWL Q
.WWQ R
ComprobanteWWR ]
(WW] ^
)WW^ _
.WW_ `

NombreTipoWW` j
,WWj k
ordenWWl q
.WWq r
ComprobanteWWr }
(WW} ~
)WW~ 
.	WW �
Tipo
WW� �
(
WW� �
)
WW� �
.
WW� �
EsPropio
WW� �
)
WW� �
,
WW� �
NumeroXX 
=XX 
ordenXX 
.XX 
ComprobanteXX &
(XX& '
)XX' (
.XX( )
NumeroDeComprobanteXX) <
}YY 	
;YY	 

DetallesZZ 
=ZZ 
ordenZZ 
.ZZ 
DetallesZZ !
(ZZ! "
)ZZ" #
;ZZ# $&
AplicarIGVCuandoEsAmazonia[[ "
=[[# $
orden[[% *
.[[* +,
 AplicaIGVCuandoAplicaLeyAmazonia[[+ K
;[[K L
UnificarDetalles\\ 
=\\ 
orden\\  
.\\  !&
TieneLosDetallesUnificados\\! ;
(\\; <
)\\< =
;\\= >
Observacion]] 
=]] 
orden]] 
.]] 
Observacion]] '
(]]' (
)]]( )
;]]) *
Flete^^ 
=^^ 
orden^^ 
.^^ 
Flete^^ 
;^^ 

=__ 
orden__ 
.__ +
EsVentaRegistradaConFechaPasada__ =
;__= >
FechaEmision`` 
=`` 
orden`` 
.`` 
FechaEmision`` )
;``) *"
NumeroBolsasDePlasticoaa 
=aa  
ordenaa! &
.aa& '"
NumeroBolsasDePlasticoaa' =
(aa= >
)aa> ?
;aa? @
Icbperbb 
=bb 
ordenbb 
.bb 
Icbperbb 
(bb 
)bb 
;bb  
Placacc 
=cc 
VentasSettingscc 
.cc 
Defaultcc &
.cc& '*
PermitirRegistroDePlacaEnVentacc' E
?ccF G
ordenccH M
.ccM N
DetallesccN V
(ccV W
)ccW X
.ccX Y
FirstccY ^
(cc^ _
)cc_ `
.cc` a
Registrocca i
:ccj k
nullccl p
;ccp q
}dd 
publicff 

DatosOrdenVentaff 
(ff 
RegistroDeVentaff *
registroDeVentaff+ :
)ff: ;
{gg 
PuntoDeVentahh 
=hh 
registroDeVentahh &
.hh& '
PuntoDeVentahh' 3
;hh3 4
Vendedorii 
=ii 
registroDeVentaii "
.ii" #
Vendedorii# +
;ii+ ,
Clientejj 
=jj 
registroDeVentajj !
.jj! "
Clientejj" )
;jj) *
ifkk 

(kk 
!kk 
Stringkk
.kk 

(kk! "
registroDeVentakk" 1
.kk1 2
Aliaskk2 7
)kk7 8
)kk8 9
Clientekk: A
.kkA B
AliaskkB G
=kkH I
registroDeVentakkJ Y
.kkY Z
AliaskkZ _
;kk_ `
Comprobantell 
=ll 
newll !
ComprobanteDeNegocio_ll /
(ll/ 0
)ll0 1
{mm 	
Tiponn 
=nn 
newnn 
ItemGenericonn #
(nn# $
registroDeVentann$ 3
.nn3 4
TipoDeComprobantenn4 E
.nnE F
TipoComprobantennF U
.nnU V
IdnnV X
,nnX Y
registroDeVentannZ i
.nni j
TipoDeComprobantennj {
.nn{ |
TipoComprobante	nn| �
.
nn� �
Nombre
nn� �
)
nn� �
,
nn� �
Serieoo 
=oo 
newoo 
SerieComprobante_oo )
(oo) *
registroDeVentaoo* 9
.oo9 :
TipoDeComprobanteoo: K
.ooK L
SerieSeleccionadaooL ]
,oo] ^
registroDeVentaoo_ n
.oon o
TipoDeComprobante	ooo �
.
oo� �
SerieIngresada
oo� �
,
oo� �
registroDeVenta
oo� �
.
oo� �
TipoDeComprobante
oo� �
.
oo� �
EsPropio
oo� �
)
oo� �
,
oo� �
Numeropp 
=pp 
registroDeVentapp $
.pp$ %
TipoDeComprobantepp% 6
.pp6 7
NumeroIngresadopp7 F
}qq 	
;qq	 

Detallesrr 
=rr 
registroDeVentarr "
.rr" #
Detallesrr# +
.rr+ ,
ToListrr, 2
(rr2 3
)rr3 4
;rr4 5&
AplicarIGVCuandoEsAmazoniass "
=ss# $
registroDeVentass% 4
.ss4 5
GrabaIgvss5 =
;ss= >
UnificarDetallestt 
=tt 
registroDeVentatt *
.tt* +
DetalleUnificadott+ ;
;tt; <
Observacionuu 
=uu 
registroDeVentauu %
.uu% &
Observacionuu& 1
;uu1 2
Fletevv 
=vv 
registroDeVentavv 
.vv  
Fletevv  %
;vv% &

=ww 
registroDeVentaww '
.ww' (

;ww5 6
FechaEmisionxx 
=xx 
registroDeVentaxx &
.xx& '
FechaEmisionxx' 3
;xx3 4"
NumeroBolsasDePlasticoyy 
=yy  
registroDeVentayy! 0
.yy0 1"
NumeroBolsasDePlasticoyy1 G
;yyG H
Icbperzz 
=zz 
registroDeVentazz  
.zz  !
Icbperzz! '
;zz' (
Placa{{ 
={{ 
registroDeVenta{{ 
.{{  
Placa{{  %
;{{% &
}|| 
public}} 

bool}} 

{~~ 
get 
{
�� 	
return
�� 
(
�� 
!
�� !
TransaccionSettings
�� (
.
��( )
Default
��) 0
.
��0 1
AplicaLeyAmazonia
��1 B
&&
��C E
Comprobante
��F Q
.
��Q R
Tipo
��R V
.
��V W
Id
��W Y
!=
��Z \
MaestroSettings
��] l
.
��l m
Default
��m t
.
��t u:
+IdDetalleMaestroComprobanteNotaVentaInterna��u �
)��� �
||��� �
(��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �!
AplicaLeyAmazonia��� �
&&��� �
Comprobante��� �
.��� �
Tipo��� �
.��� �
Id��� �
!=��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �;
+IdDetalleMaestroComprobanteNotaVentaInterna��� �
&&��� �*
AplicarIGVCuandoEsAmazonia��� �
)��� �
;��� �
}
�� 	
}
�� 
public
�� 

decimal
�� 
ImporteTotal
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
Detalles
�� 
!=
�� 
null
�� #
?
��$ %
Detalles
��& .
.
��. /
Sum
��/ 2
(
��2 3
d
��3 4
=>
��5 7
d
��8 9
.
��9 :
Importe
��: A
)
��A B
+
��C D
Icbper
��E K
:
��L M
$num
��N O
;
��O P
}
�� 	
}
�� 
public
�� 

List
�� 
<
�� !
Detalle_transaccion
�� #
>
��# $
DetallesDeVenta
��% 4
(
��4 5
)
��5 6
{
�� 
return
�� 
Detalles
�� 
.
�� 
Select
�� 
(
�� 
d
��  
=>
��! #
d
��$ %
.
��% & 
DetalleTransaccion
��& 8
(
��8 9
)
��9 :
)
��: ;
.
��; <
ToList
��< B
(
��B C
)
��C D
;
��D E
}
�� 
public
�� 

bool
�� $
HayBienesEnLosDetalles
�� &
(
��& '
)
��' (
{
�� 
return
�� 
Detalles
�� 
.
�� 
Count
�� 
(
�� 
d
�� 
=>
��  "
d
��# $
.
��$ %
Producto
��% -
.
��- .
EsBien
��. 4
)
��4 5
>
��6 7
$num
��8 9
;
��9 :
}
�� 
public
�� 

List
�� 
<
�� !
Detalle_transaccion
�� #
>
��# $)
DetallesDeVentaQueSonBienes
��% @
(
��@ A
)
��A B
{
�� 
return
�� 
Detalles
�� 
.
�� 
Where
�� 
(
�� 
d
�� 
=>
��  "
d
��# $
.
��$ %
Producto
��% -
.
��- .
EsBien
��. 4
)
��4 5
.
��5 6
Select
��6 <
(
��< =
dt
��= ?
=>
��@ B
dt
��C E
.
��E F 
DetalleTransaccion
��F X
(
��X Y
)
��Y Z
)
��Z [
.
��[ \
ToList
��\ b
(
��b c
)
��c d
;
��d e
}
�� 
public
�� 

decimal
�� 
DescuentoGlobal
�� "
{
��# $
get
��% (
;
��( )
set
��* -
;
��- .
}
��/ 0
public
�� 

decimal
�� 
DescuentoPorItem
�� #
{
�� 
get
�� 
{
�� 	
return
�� 
Detalles
�� 
!=
�� 
null
�� #
?
��$ %
Detalles
��& .
.
��. /
Sum
��/ 2
(
��2 3
d
��3 4
=>
��5 7
d
��8 9
.
��9 :
	Descuento
��: C
)
��C D
:
��E F
$num
��G H
;
��H I
}
�� 	
}
�� 
public
�� 

decimal
�� 
Anticipo
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
$num
�� 
;
�� 
}
�� 	
}
�� 
public
�� 

decimal
�� 
Gravada
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
Detalles
�� 
!=
�� 
null
�� #
?
��$ %

��& 3
?
��4 5
Detalles
��6 >
.
��> ?
Sum
��? B
(
��B C
d
��C D
=>
��E G
d
��H I
.
��I J
Importe
��J Q
-
��R S
d
��T U
.
��U V
Igv
��V Y
)
��Y Z
:
��[ \
$num
��] ^
:
��_ `
$num
��a b
;
��b c
}
�� 	
}
�� 
public
�� 

decimal
�� 
	Exonerada
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
Detalles
�� 
!=
�� 
null
�� #
?
��$ %

��& 3
?
��4 5
$num
��6 7
:
��8 9
Detalles
��: B
.
��B C
Sum
��C F
(
��F G
d
��G H
=>
��I K
d
��L M
.
��M N
Importe
��N U
)
��U V
:
��W X
$num
��Y Z
;
��Z [
}
�� 	
}
�� 
public
�� 

decimal
�� 
Inafecta
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
$num
�� 
;
�� 
}
�� 	
}
�� 
public
�� 

decimal
�� 
Gratuita
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
$num
�� 
;
�� 
}
�� 	
}
�� 
public
�� 

decimal
�� 
Igv
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
Detalles
�� 
!=
�� 
null
�� #
?
��$ %
Detalles
��& .
.
��. /
Sum
��/ 2
(
��2 3
d
��3 4
=>
��5 7
d
��8 9
.
��9 :
Igv
��: =
)
��= >
:
��? @
$num
��A B
;
��B C
}
�� 	
}
�� 
public
�� 

decimal
�� 
Isc
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
Detalles
�� 
!=
�� 
null
�� #
?
��$ %
Detalles
��& .
.
��. /
Sum
��/ 2
(
��2 3
d
��3 4
=>
��5 7
d
��8 9
.
��9 :
Isc
��: =
)
��= >
:
��? @
$num
��A B
;
��B C
}
�� 	
}
�� 
public
�� 

decimal
�� 
OtrosCargos
�� 
{
�� 
get
�� 
{
�� 	
return
�� 
$num
�� 
;
�� 
}
�� 	
}
�� 
public
�� 

decimal
�� 

��  
{
�� 
get
�� 
{
�� 	
return
�� 
$num
�� 
;
�� 
}
�� 	
}
�� 
}�� 
public�� 
partial
�� 
class
�� 
	DatosPago
�� 
{�� 
public
�� 

ItemGenerico
�� 
Caja
�� 
{
�� 
get
�� "
;
��" #
set
��$ '
;
��' (
}
��) *
public
�� 

ItemGenerico
�� 
Cajero
�� 
{
��  
get
��! $
;
��$ %
set
��& )
;
��) *
}
��+ ,
public
�� 

TrazaDePago_
�� 
Traza
�� 
{
�� 
get
��  #
;
��# $
set
��% (
;
��( )
}
��* +
public
�� 

ModoPago
�� 

ModoDePago
�� 
{
��  
get
��! $
;
��$ %
set
��& )
;
��) *
}
��+ ,
public
�� 

decimal
�� 
Inicial
�� 
{
�� 
get
��  
;
��  !
set
��" %
;
��% &
}
��' (
public
�� 

List
�� 
<
�� -
RegistroDetalleDeFinanciamiento
�� /
>
��/ 0
Cuotas
��1 7
{
��8 9
get
��: =
;
��= >
set
��? B
;
��B C
}
��D E
public
�� 

	DatosPago
�� 
(
�� 
)
�� 
{
�� 
}
�� 
public
�� 

	DatosPago
�� 
(
�� 
RegistroDeVenta
�� $
registroDeVenta
��% 4
)
��4 5
{
�� 
Caja
�� 
=
��
registroDeVenta
�� 
.
�� 
Caja
�� #
;
��# $
Cajero
�� 
=
�� 
registroDeVenta
��  
.
��  !
Cajero
��! '
;
��' (
Traza
�� 
=
�� 
registroDeVenta
�� 
.
��  
TrazaDePago
��  +
;
��+ ,

ModoDePago
�� 
=
�� 
registroDeVenta
�� $
.
��$ %

ModoDePago
��% /
;
��/ 0
Inicial
�� 
=
�� 
registroDeVenta
�� !
.
��! "
Inicial
��" )
;
��) *
Cuotas
�� 
=
�� 
registroDeVenta
��  
.
��  !
Cuotas
��! '
?
��' (
.
��( )
ToList
��) /
(
��/ 0
)
��0 1
;
��1 2
}
�� 
public
�� 

List
�� 
<
�� 
Cuota
�� 
>
�� 

�� $
(
��$ %
)
��% &
{
�� 
return
�� 

ModoDePago
�� 
==
�� 
ModoPago
�� %
.
��% & 
CreditoConfigurado
��& 8
?
��9 :-
RegistroDetalleDeFinanciamiento
��; Z
.
��Z [
Convert_
��[ c
(
��c d
Cuotas
��d j
.
��j k
ToList
��k q
(
��q r
)
��r s
)
��s t
:
��u v
null
��w {
;
��{ |
}
�� 
public
�� 

bool
�� 
HayIngresoDinero
��  
{
�� 
get
�� 
{
�� 	
return
�� 
(
�� 

ModoDePago
�� 
==
�� !
ModoPago
��" *
.
��* +
Contado
��+ 2
||
��3 5
Inicial
��6 =
>
��> ?
$num
��@ A
)
��A B
;
��B C
}
�� 	
}
�� 
}�� 
public�� 
partial
�� 
class
�� &
DatosMovimientoDeAlmacen
�� -
{�� 
public
�� 

ItemGenerico
�� 
Almacen
�� 
{
��  !
get
��" %
;
��% &
set
��' *
;
��* +
}
��, -
public
�� 

ItemGenerico
�� 

Almacenero
�� "
{
��# $
get
��% (
;
��( )
set
��* -
;
��- .
}
��/ 0
public
�� 

bool
�� 
EntregaDiferida
�� 
{
��  !
get
��" %
;
��% &
set
��' *
;
��* +
}
��, -
public
�� 

bool
�� 0
"HayComprobanteDeSalidaDeMercaderia
�� 2
{
��3 4
get
��5 8
;
��8 9
set
��: =
;
��= >
}
��? @
public
�� 

IEnumerable
�� 
<
�� '
RegistroMovimientoAlmacen
�� 0
>
��0 1,
RegistroDeMovimientosDeAlmacen
��2 P
{
��Q R
get
��S V
;
��V W
set
��X [
;
��[ \
}
��] ^
public
�� 
&
DatosMovimientoDeAlmacen
�� #
(
��# $
)
��$ %
{
��& '
}
��( )
public
�� 
&
DatosMovimientoDeAlmacen
�� #
(
��# $
RegistroDeVenta
��$ 3
registroDeVenta
��4 C
)
��C D
{
�� 
Almacen
�� 
=
�� 
registroDeVenta
�� !
.
��! "
Almacen
��" )
;
��) *

Almacenero
�� 
=
�� 
registroDeVenta
�� $
.
��$ %

Almacenero
��% /
;
��/ 00
"HayComprobanteDeSalidaDeMercaderia
�� *
=
��+ ,
registroDeVenta
��- <
.
��< =#
HaySalidaDeMercaderia
��= R
;
��R S,
RegistroDeMovimientosDeAlmacen
�� &
=
��' (
registroDeVenta
��) 8
.
��8 9!
SalidasDeMercaderia
��9 L
;
��L M
}
�� 
public
�� 

List
�� 
<
�� !
MovimientoDeAlmacen
�� #
>
��# $/
!ComprobantesDeSalidasDeMercaderia
��% F
{
�� 
get
�� 
{
�� 	
return
�� 0
"HayComprobanteDeSalidaDeMercaderia
�� 5
?
��6 7'
RegistroMovimientoAlmacen
��8 Q
.
��Q R
Convert_
��R Z
(
��Z [,
RegistroDeMovimientosDeAlmacen
��[ y
.
��y z
ToList��z �
(��� �
)��� �
)��� �
:��� �
null��� �
;��� �
}
�� 	
}
�� 
}�� �
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Ambiente.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
public 

class 
Ambiente 
{		 
public

 
int

 
Id

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
Filas 
{ 
get 
; 
set  #
;# $
}% &
public
int
Columnas
{
get
;
set
;
}
public 
bool 
MesasTemporales #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
ItemGenerico 
Establecimiento +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
Ambiente 
( 
) 
{ 	
}	 

public 
Ambiente 
( 

actorNegocio& 2
)2 3
{ 	

data 
=  
JsonConvert! ,
., -
DeserializeObject- >
<> ?

>L M
(M N
actorNegocioN Z
.Z [
extension_json[ i
)i j
;j k
this 
. 
Id 
= 
actorNegocio "
." #
id# %
;% &
this 
. 
Nombre 
= 
data 
. 
nombre %
;% &
this 
. 
Filas 
= 
data 
. 
filas #
;# $
this 
. 
Columnas 
= 
data  
.  !
columnas! )
;) *
this 
. 
MesasTemporales  
=! "
data# '
.' (
mesastemporales( 7
;7 8
} 	
public 
static 
List 
< 
Ambiente #
># $
Convert% ,
(, -
List- 1
<1 2

>? @
actorNegocioA M
)M N
{ 	
List 
< 
Ambiente 
> 
items  
=! "
new# &
List' +
<+ ,
Ambiente, 4
>4 5
(5 6
)6 7
;7 8
foreach   
(   
var   
actor   
in   !
actorNegocio  " .
)  . /
{!! 
items"" 
."" 
Add"" 
("" 
new"" 
Ambiente"" &
(""& '
actor""' ,
)"", -
)""- .
;"". /
}## 
return$$ 
items$$ 
;$$ 
}%% 	
}&& 
})) � 
QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Mesa.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{		 
public

 

class

 
Mesa

 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
string
Nombre
{
get
;
set
;
}
public 
bool 

{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
Fila 
{ 
get 
; 
set "
;" #
}$ %
public 
int 
Columna 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 

IdAmbiente 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
NombreAmbiente $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
JsonData 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
Mesa 
( 
) 
{ 	
}	 

public 
Mesa 
( 

actorNegocio" .
). /
{ 	
	Data_Mesa 
data 
= 
JsonConvert (
.( )
DeserializeObject) :
<: ;
	Data_Mesa; D
>D E
(E F
actorNegocioF R
.R S
extension_jsonS a
)a b
;b c
this 
. 
Id 
= 
actorNegocio #
.# $
id$ &
;& '
this 
. 

=  
actorNegocio! -
.- .

indicador1. 8
;8 9
this 
. 
Nombre 
= 
data 
. 
nombre %
;% &
this 
. 
Fila 
= 
data 
. 
fila !
;! "
this 
. 
Columna 
= 
data 
.  
columna  '
;' (
this   
.   

IdAmbiente   
=   
(   
int   "
)  " #
actorNegocio  # /
.  / 0"
id_actor_negocio_padre  0 F
;  F G
this!! 
.!! 
NombreAmbiente!! 
=!!  !
actorNegocio!!" .
.!!. /
Actor_negocio2!!/ =
.!!= >
PrimerNombre!!> J
;!!J K
}"" 	
public&& 
static&& 
List&& 
<&& 
Mesa&& 
>&&  
Convert&&! (
(&&( )
List&&) -
<&&- .

>&&; <
actoresNegocio&&= K
)&&K L
{'' 	
List(( 
<(( 
Mesa(( 
>(( 
items(( 
=(( 
new(( "
List((# '
<((' (
Mesa((( ,
>((, -
(((- .
)((. /
;((/ 0
foreach)) 
()) 
var)) 
actorNegocio)) %
in))& (
actoresNegocio))) 7
)))7 8
{** 
items++ 
.++ 
Add++ 
(++ 
new++ 
Mesa++ "
(++" #
actorNegocio++# /
)++/ 0
)++0 1
;++1 2
},, 
return-- 
items-- 
;-- 
}.. 	
}00 
}33 �
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Generico\ItemJerarquico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public 

class 
ItemJerarquico 
{		 
public

 
int

 
Id

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
int 
IdPadre 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
ItemJerarquico 
( 
) 
{ 	
} 	
public 
ItemJerarquico 
( #
Detalle_detalle_maestro 5!
detalleDetalleMaestro6 K
)K L
{ 	
Id 
= !
detalleDetalleMaestro &
.& ')
id_detalle_maestro_secundario' D
;D E
IdPadre 
= !
detalleDetalleMaestro +
.+ ,(
id_detalle_maestro_principal, H
;H I
Nombre 
= !
detalleDetalleMaestro *
.* +
Detalle_maestro1+ ;
.; <
nombre< B
;B C
} 	
public 
static 
List 
< 
ItemJerarquico )
>) *
Convert+ 2
(2 3
List3 7
<7 8#
Detalle_detalle_maestro8 O
>O P 
detalles_de_detallesQ e
)e f
{ 	
List 
< 
ItemJerarquico 
>  
items! &
=' (
new) ,
List- 1
<1 2
ItemJerarquico2 @
>@ A
(A B
)B C
;C D
foreach 
( 
var 
detalle  
in! # 
detalles_de_detalles$ 8
)8 9
{ 
items 
. 
Add 
( 
new 
ItemJerarquico ,
(, -
detalle- 4
)4 5
)5 6
;6 7
}   
return!! 
items!! 
;!! 
}"" 	
}## 
}&& �
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\Pago_cuota.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
{		 
public

 

partial

 
class

 

Pago_cuota

 #
{ 
public 

Pago_cuota 
( 
) 
{
} 	
public 

Pago_cuota 
( 
long 

,, -
int. 1
idCuota2 9
,9 :
decimal; B
importeC J
)J K
{ 	
this 
. 
id_transaccion 
=  !

;/ 0
SetData 
( 
idCuota 
, 
importe $
)$ %
;% &
	ValidarId 
( 

,# $
idCuota% ,
), -
;- .
} 	
public 

Pago_cuota 
( 
int 
idCuota %
,% &
decimal' .
importe/ 6
)6 7
{ 	
SetData 
( 
idCuota 
, 
importe $
)$ %
;% &
	ValidarId 
( 
idCuota 
) 
; 
} 	
public 
void 
SetData 
( 
int 
idCuota  '
,' (
decimal) 0
importe1 8
)8 9
{ 	
this 
. 
id_cuota 
= 
idCuota #
;# $
this 
. 
importe 
= 
importe "
;" #
} 	
	protected 
void 
	ValidarId  
(  !
long! %

,3 4
int5 8
idCuota9 @
)@ A
{   	
if!! 
(!! 
idCuota!! 
<!! 
$num!! 
)!! 
{!! 
throw!! $
new!!% (
IdNoValidoException!!) <
(!!< =
idCuota!!= D
,!!D E
$str!!F M
)!!M N
;!!N O
}!!P Q
if"" 
("" 

<"" 
$num""  !
)""! "
{""# $
throw""% *
new""+ .
IdNoValidoException""/ B
(""B C

,""P Q
$str""R _
)""_ `
;""` a
}""b c
}## 	
	protected$$ 
void$$ 
	ValidarId$$  
($$  !
int$$! $
idCuota$$% ,
)$$, -
{%% 	
if&& 
(&& 
idCuota&& 
<&& 
$num&& 
)&& 
{&& 
throw&& $
new&&% (
IdNoValidoException&&) <
(&&< =
idCuota&&= D
,&&D E
$str&&F M
)&&M N
;&&N O
}&&P Q
}'' 	
}(( 
})) �W
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\Reporte_Caja.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
public		 

class		 
Movimiento_Caja		  
{

 
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
IdCaja 
{ 
get 
;  
set! $
;$ %
}& '
public
string

NombreCaja
{
get
;
set
;
}
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string !
CodigoTipoComprobante +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
string #
SerieYNumeroComprobante -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
decimal 
Monto 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
	EsIngreso 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
Index 
{ 
get 
; 
set  #
;# $
}% &
public 
decimal 
Ingreso 
{ 	
get 
{ 
return 
	EsIngreso 
?  
Monto! &
:' (
$num) *
;* +
}, -
} 	
public 
decimal 
Egreso 
{ 	
get 
{ 
return 
	EsIngreso 
?  
$num! "
:# $
Monto% *
;* +
}, -
} 	
public 
decimal 
Saldo 
{ 
get "
;" #
set$ '
;' (
}) *
public!! 
Movimiento_Caja!! 
(!! 
)!!  
{"" 	
}""
 
public$$ 
static$$ 
List$$ 
<$$ 
Movimiento_Caja$$ *
>$$* +
Convert$$, 3
($$3 4
)$$4 5
{%% 	
return&& 
new&& 
List&& 
<&& 
Movimiento_Caja&& +
>&&+ ,
(&&, -
)&&- .
;&&. /
}'' 	
public)) 
static)) 
List)) 
<)) 
Movimiento_Caja)) *
>))* +
Convert)), 3
())3 4
List))4 8
<))8 9#
Resumen_Movimiento_Caja))9 P
>))P Q
movimientosResumen))R d
,))d e
List))f j
<))j k
Movimiento_Caja))k z
>))z {
movimientosCaja	))| �
,
))� �
List
))� �
<
))� �'
CentroDeAtencionExtendido
))� �
>
))� �

))� �
)
))� �
{** 	
List++ 
<++ 
Movimiento_Caja++  
>++  !$
movimientosCajaResultado++" :
=++; <
new++= @
List++A E
<++E F
Movimiento_Caja++F U
>++U V
(++V W
)++W X
;++X Y
foreach,, 
(,, 
var,, 

movimiento,, #
in,,$ &
movimientosResumen,,' 9
),,9 :
{-- 
var.. '
movimientosCajaSeleccionada.. /
=..0 1
movimientosCaja..2 A
...A B
Where..B G
(..G H
m..H I
=>..J L
m..M N
...N O
IdCaja..O U
==..V X

movimiento..Y c
...c d
IdCaja..d j
)..j k
...k l
OrderBy..l s
(..s t
m..t u
=>..v x
m..y z
...z {
Fecha	..{ �
)
..� �
.
..� �
ToList
..� �
(
..� �
)
..� �
;
..� �
decimal// 
saldo// 
=// 

movimiento//  *
.//* +
SaldoInicial//+ 7
;//7 8
int00 
index00 
=00 
$num00 
;00 
foreach11 
(11 
var11 "
movimientoSeleccionado11 3
in114 6'
movimientosCajaSeleccionada117 R
)11R S
{22 
saldo33 
+=33 
(33 "
movimientoSeleccionado33 4
.334 5
	EsIngreso335 >
?33? @"
movimientoSeleccionado33A W
.33W X
Monto33X ]
:33^ _
(33` a
-33a b
$num33b c
*33d e"
movimientoSeleccionado33f |
.33| }
Monto	33} �
)
33� �
)
33� �
;
33� �
var44 
item44 
=44 
new44 "
Movimiento_Caja44# 2
{55 
Index66 
=66 
index66  %
++66% '
,66' (
Fecha77 
=77 "
movimientoSeleccionado77  6
.776 7
Fecha777 <
,77< =

IdConcepto88 "
=88# $"
movimientoSeleccionado88% ;
.88; <

IdConcepto88< F
,88F G
Concepto99  
=99! ""
movimientoSeleccionado99# 9
.999 :
Concepto99: B
,99B C!
CodigoTipoComprobante:: -
=::. /"
movimientoSeleccionado::0 F
.::F G!
CodigoTipoComprobante::G \
,::\ ]#
SerieYNumeroComprobante;; /
=;;0 1"
movimientoSeleccionado;;2 H
.;;H I#
SerieYNumeroComprobante;;I `
,;;` a
IdCaja<< 
=<<  "
movimientoSeleccionado<<! 7
.<<7 8
IdCaja<<8 >
,<<> ?

NombreCaja== "
===# $"
movimientoSeleccionado==% ;
.==; <

NombreCaja==< F
,==F G
	EsIngreso>> !
=>>" #"
movimientoSeleccionado>>$ :
.>>: ;
	EsIngreso>>; D
,>>D E
Monto?? 
=?? "
movimientoSeleccionado??  6
.??6 7
Monto??7 <
,??< =
Saldo@@ 
=@@ 
saldo@@  %
}AA 
;AA $
movimientosCajaResultadoBB ,
.BB, -
AddBB- 0
(BB0 1
itemBB1 5
)BB5 6
;BB6 7
}CC 

movimientoDD 
.DD 

NombreCajaDD %
=DD& '

.DD5 6
FirstDD6 ;
(DD; <
cvDD< >
=>DD? A
cvDDB D
.DDD E
IdDDE G
==DDH J

movimientoDDK U
.DDU V
IdCajaDDV \
)DD\ ]
.DD] ^
NombreDD^ d
;DDd e

movimientoEE 
.EE 
IngresoEE "
=EE# $'
movimientosCajaSeleccionadaEE% @
.EE@ A
SumEEA D
(EED E
mEEE F
=>EEG I
mEEJ K
.EEK L
IngresoEEL S
)EES T
;EET U

movimientoFF 
.FF 
EgresoFF !
=FF" #'
movimientosCajaSeleccionadaFF$ ?
.FF? @
SumFF@ C
(FFC D
mFFD E
=>FFF H
mFFI J
.FFJ K
EgresoFFK Q
)FFQ R
;FFR S
}GG 
returnKK $
movimientosCajaResultadoKK +
;KK+ ,
}LL 	
}MM 
publicOO 

classOO #
Resumen_Movimiento_CajaOO (
{PP 
publicQQ 
intQQ 
IdCajaQQ 
{QQ 
getQQ 
;QQ  
setQQ! $
;QQ$ %
}QQ& '
publicRR 
stringRR 

NombreCajaRR  
{RR! "
getRR# &
;RR& '
setRR( +
;RR+ ,
}RR- .
publicSS 
decimalSS 
SaldoInicialSS #
{SS$ %
getSS& )
;SS) *
setSS+ .
;SS. /
}SS0 1
publicTT 
decimalTT 
IngresoTT 
{TT  
getTT! $
;TT$ %
setTT& )
;TT) *
}TT+ ,
publicUU 
decimalUU 
EgresoUU 
{UU 
getUU  #
;UU# $
setUU% (
;UU( )
}UU* +
publicVV 
decimalVV 

SaldoFinalVV !
{WW 	
getXX 
{YY 
returnYY 
SaldoInicialYY !
+YY" #
IngresoYY$ +
-YY, -
EgresoYY. 4
;YY4 5
}YY6 7
}ZZ 	
public\\ 
static\\ 
List\\ 
<\\ #
Resumen_Movimiento_Caja\\ 2
>\\2 3
Resumen\\4 ;
(\\; <
List\\< @
<\\@ A
Movimiento_Caja\\A P
>\\P Q
	resultado\\R [
)\\[ \
{]] 	
return^^ 
	resultado^^ 
.^^ 
GroupBy^^ $
(^^$ %
t^^% &
=>^^' )
t^^* +
.^^+ ,
IdCaja^^, 2
)__ 
.__
Select__ 
(__ 
t__ 
=>__ 
new__ #
Resumen_Movimiento_Caja__ 5
(__5 6
)__6 7
{`` 
IdCajaaa 
=aa 
taa 
.aa 
Keyaa 
,aa 

NombreCajabb 
=bb 
tbb 
.bb 
Firstbb $
(bb$ %
)bb% &
.bb& '

NombreCajabb' 1
,bb1 2
Ingresocc 
=cc 
tcc 
.cc 
Sumcc 
(cc  
ttcc  "
=>cc# %
ttcc& (
.cc( )
Ingresocc) 0
)cc0 1
,cc1 2
Egresodd 
=dd 
tdd 
.dd 
Sumdd 
(dd 
ttdd !
=>dd" $
ttdd% '
.dd' (
Egresodd( .
)dd. /
,dd/ 0
}ee 
)ee
.ee 
ToListee 
(ee 
)ee 
;ee 
}ff 	
publichh #
Resumen_Movimiento_Cajahh &
(hh& '
)hh' (
{ii 	
}ii
 
publickk 
statickk 
Listkk 
<kk #
Resumen_Movimiento_Cajakk 2
>kk2 3
Convertkk4 ;
(kk; <
)kk< =
{ll 	
returnmm 
newmm 
Listmm 
<mm #
Resumen_Movimiento_Cajamm 3
>mm3 4
(mm4 5
)mm5 6
;mm6 7
}nn 	
}oo 
}pp �
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Consultas\DetalleOrden_Consulta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
[		 
Serializable		 
]		 
public

 

class

 !
DetalleOrden_Consulta

 &
{ 
public 
DateTime 
	FechaHora !
{" #
get$ '
;' (
set) ,
;, -
}. /
public
string
Codigo
{
get
;
set
;
}
public 
string 

NombreItem  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Monto 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Mozo 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Mesa 
{ 
get  
;  !
set" %
;% &
}' (
public !
DetalleOrden_Consulta $
($ %
)% &
{& '
}' (
public 
static 
List 
< !
DetalleOrden_Consulta 0
>0 1
Convert2 9
(9 :
): ;
{ 	
return 
new 
List 
< !
DetalleOrden_Consulta 1
>1 2
(2 3
)3 4
;4 5
} 	
} 
} �B
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Puntos\Reporte_Puntos_Canjeados.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
public		 

class		 $
Reporte_Puntos_Canjeados		 )
{

 
public 
int 
IdCaja 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 

NombreCaja  
{! "
get# &
;& '
set( +
;+ ,
}- .
public
DateTime
	FechaPago
{
get
;
set
;
}
public 
string &
CodigoTipoDocumentoCliente 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
string "
NumeroDocumentoCliente ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
string 
Cliente 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string !
CodigoTipoComprobante +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
string 
TipoComprobante %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
SerieComprobante &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
int 
NumeroComprobante $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
Puntos 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Monto 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
	EsIngreso 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Comprobante !
{" #
get$ '
=>( *
SerieComprobante+ ;
+< =
$str> A
+B C
NumeroComprobanteD U
;U V
}W X
public 
int 
PuntosRealizados #
{$ %
get& )
=>* ,
	EsIngreso- 6
?7 8
(9 :
int: =
)= >
Puntos> D
:E F
$numG H
;H I
}J K
public 
int 
PuntosAnulados !
{" #
get$ '
=>( *
	EsIngreso+ 4
?5 6
$num7 8
:9 :
(; <
int< ?
)? @
Puntos@ F
;F G
}H I
public 
decimal 
MontoRealizado %
{& '
get( +
=>, .
	EsIngreso/ 8
?9 :
Monto; @
:A B
$numC D
;D E
}F G
public 
decimal 
MontoAnulado #
{$ %
get& )
=>* ,
	EsIngreso- 6
?7 8
$num9 :
:; <
Monto= B
;B C
}D E
public $
Reporte_Puntos_Canjeados '
(' (
)( )
{   	
}"" 	
public$$ 
static$$ 
List$$ 
<$$ $
Reporte_Puntos_Canjeados$$ 3
>$$3 4
Convert$$5 <
($$< =
)$$= >
{%% 	
return&& 
new&& 
List&& 
<&& $
Reporte_Puntos_Canjeados&& 4
>&&4 5
(&&5 6
)&&6 7
;&&7 8
}'' 	
}(( 
public** 

class** $
Resumen_Puntos_Canjeados** )
{++ 
public,, 
int,, 
IdCaja,, 
{,, 
get,, 
;,,  
set,,! $
;,,$ %
},,& '
public-- 
string-- 

NombreCaja--  
{--! "
get--# &
;--& '
set--( +
;--+ ,
}--- .
public.. 
int.. 
PuntosRealizados.. #
{..$ %
get..& )
;..) *
set..+ .
;... /
}..0 1
public// 
int// 
PuntosAnulados// !
{//" #
get//$ '
;//' (
set//) ,
;//, -
}//. /
public00 
decimal00 
MontoRealizado00 %
{00& '
get00( +
;00+ ,
set00- 0
;000 1
}002 3
public11 
decimal11 
MontoAnulado11 #
{11$ %
get11& )
;11) *
set11+ .
;11. /
}110 1
public22 
int22 
PuntosCanjeados22 "
=>22# %
PuntosRealizados22& 6
-227 8
PuntosAnulados229 G
;22G H
public33 
decimal33 

{33% &
get33' *
=>33+ -
MontoRealizado33. <
-33= >
MontoAnulado33? K
;33K L
}33M N
public55 $
Resumen_Puntos_Canjeados55 '
(55' (
)55( )
{66 	
}88 	
public:: 
static:: 
List:: 
<:: $
Resumen_Puntos_Canjeados:: 3
>::3 4
Convert::5 <
(::< =
)::= >
{;; 	
return<< 
new<< 
List<< 
<<< $
Resumen_Puntos_Canjeados<< 4
><<4 5
(<<5 6
)<<6 7
;<<7 8
}== 	
public?? 
static?? 
List?? 
<?? $
Resumen_Puntos_Canjeados?? 3
>??3 4
Convert??5 <
(??< =
List??= A
<??A B$
Reporte_Puntos_Canjeados??B Z
>??Z [&
registrosDePuntosCanjeados??\ v
)??v w
{@@ 	
varAA 
resumenAA 
=AA &
registrosDePuntosCanjeadosAA 4
.AA4 5
GroupByAA5 <
(AA< =
rpcAA= @
=>AAA C
newAAD G
{BB 
	idEntidadCC 
=CC 
rpcCC 
.CC  
IdCajaCC  &
,CC& '

=DD 
rpcDD  #
.DD# $

NombreCajaDD$ .
,DD. /
}EE 
)EE
.EE 
SelectEE 
(EE 
rEE 
=>EE 
newEE $
Resumen_Puntos_CanjeadosEE 7
(EE7 8
)EE8 9
{FF 
IdCajaGG 
=GG 
rGG 
.GG 
KeyGG 
.GG 
	idEntidadGG (
,GG( )

NombreCajaHH 
=HH 
rHH 
.HH 
KeyHH "
.HH" #

,HH0 1
PuntosRealizadosII  
=II! "
rII# $
.II$ %
SumII% (
(II( )
dII) *
=>II+ -
dII. /
.II/ 0
PuntosRealizadosII0 @
)II@ A
,IIA B
PuntosAnuladosJJ 
=JJ  
rJJ! "
.JJ" #
SumJJ# &
(JJ& '
dJJ' (
=>JJ) +
dJJ, -
.JJ- .
PuntosAnuladosJJ. <
)JJ< =
,JJ= >
MontoRealizadoKK 
=KK  
rKK! "
.KK" #
SumKK# &
(KK& '
dKK' (
=>KK) +
dKK, -
.KK- .
MontoRealizadoKK. <
)KK< =
,KK= >
MontoAnuladoLL 
=LL 
rLL  
.LL  !
SumLL! $
(LL$ %
dLL% &
=>LL' )
dLL* +
.LL+ ,
MontoAnuladoLL, 8
)LL8 9
,LL9 :
}MM 
)MM
.MM 
ToListMM 
(MM 
)MM 
;MM 
returnNN 
resumenNN 
;NN 
}OO 	
}PP 
}QQ �
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Puntos\Reporte_Puntos_Pendientes.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{		 
public

 

class

 %
Reporte_Puntos_Pendientes

 *
{ 
public 
string "
NumeroDocumentoCliente ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public
string
Cliente
{
get
;
set
;
}
public 
int 
PuntosPendientes #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal !
ValorPuntosPendientes ,
{- .
get/ 2
=>3 5
decimal6 =
.= >
Round> C
(C D
(D E
PuntosPendientesE U
*V W
VentasSettingsX f
.f g
Defaultg n
.n o*
ValorDeUnPuntoComoMedioDePago	o �
)
� �
,
� �
$num
� �
)
� �
;
� �
}
� �
public %
Reporte_Puntos_Pendientes (
(( )
)) *
{ 	
} 	
public 
static 
List 
< %
Reporte_Puntos_Pendientes 4
>4 5
Convert6 =
(= >
)> ?
{ 	
return 
new 
List 
< %
Reporte_Puntos_Pendientes 5
>5 6
(6 7
)7 8
;8 9
} 	
} 
} �
uD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Consultas\ResumenOrdenesPorMozo_Consulta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
[		 
Serializable		 
]		 
public

 

class

 *
ResumenOrdenesPorMozo_Consulta

 /
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
string
Mozo
{
get
;
set
;
}
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public *
ResumenOrdenesPorMozo_Consulta -
(- .
). /
{/ 0
}0 1
public 
static 
List 
< *
ResumenOrdenesPorMozo_Consulta 9
>9 :
Convert; B
(B C
)C D
{ 	
return 
new 
List 
< *
ResumenOrdenesPorMozo_Consulta :
>: ;
(; <
)< =
;= >
} 	
} 
} �
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Consultas\ResumenOrden_Consulta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
[		 
Serializable		 
]		 
public

 

class

 !
ResumenOrden_Consulta

 &
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public
string
Codigo
{
get
;
set
;
}
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Mozo 
{ 
get  
;  !
set" %
;% &
}' (
public 
DateTime 
Fecha 
{ 
get  #
;# $
set% (
;( )
}* +
public !
ResumenOrden_Consulta $
($ %
)% &
{& '
}' (
public 
static 
List 
< !
ResumenOrden_Consulta 0
>0 1
Convert2 9
(9 :
): ;
{ 	
return 
new 
List 
< !
ResumenOrden_Consulta 1
>1 2
(2 3
)3 4
;4 5
} 	
} 
} �
oD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesRestaurant\Consultas\ItemRestaurante_Consulta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
SigesRestaurant -
{ 
[		 
Serializable		 
]		 
public

 

class

 $
ItemRestaurante_Consulta

 )
{ 
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public
string

NombreItem
{
get
;
set
;
}
public 
decimal 
Precio 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
Cantidad 
{  !
get" %
;% &
set' *
;* +
}, -
public 
decimal 
Monto 
{ 
get "
;" #
set$ '
;' (
}) *
public $
ItemRestaurante_Consulta '
(' (
)( )
{) *
}* +
public 
static 
List 
< $
ItemRestaurante_Consulta 3
>3 4
Convert5 <
(< =
)= >
{ 	
return 
new 
List 
< $
ItemRestaurante_Consulta 4
>4 5
(5 6
)6 7
;7 8
} 	
} 
} �
`D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\CostoUtilidadPorConcepto.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
.. /
Partial/ 6
{ 
public		 

class		 $
CostoUtilidadPorConcepto		 )
{

 
public 
int 

IdConcepto 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Concepto 
{  
get! $
;$ %
set& )
;) *
}+ ,
public
decimal
Cantidad
{
get
;
set
;
}
public 
decimal 
Importe 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Costo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 
Utilidad 
{ 	
get 
{ 
return 
Importe 
- 
Costo $
;$ %
}& '
} 	
public 
decimal "
PrecioUnitarioPromedio -
{ 	
get 
{ 
return 
Importe 
/ 
Cantidad '
;' (
}) *
} 	
public 
decimal !
CostoUnitarioPromedio ,
{ 	
get 
{ 
return 
Costo 
/ 
Cantidad '
;' (
} 
}!! 	
public"" 
decimal"" 
UtilidadPromedio"" '
{## 	
get$$ 
{%% 
return%% 
Utilidad%% 
/%% 
Cantidad%%  (
;%%( )
}%%* +
}&& 	
public)) $
CostoUtilidadPorConcepto)) '
())' (
)))( )
{** 	
}**
 
public,, 
static,, 
List,, 
<,, $
CostoUtilidadPorConcepto,, 3
>,,3 4
Convert,,5 <
(,,< =
),,= >
{-- 	
return.. 
new.. 
List.. 
<.. $
CostoUtilidadPorConcepto.. 4
>..4 5
(..5 6
)..6 7
;..7 8
}// 	
}00 
}11 �2
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Precio\PrecioParaRegistroDeVenta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 %
PrecioParaRegistroDeVenta

 *
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public
string
Tarifa
{
get
;
set
;
}
public 
int 
IdTarifa 
{ 
get !
;! "
set# &
;& '
}( )
public 
decimal 
Valor 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public %
PrecioParaRegistroDeVenta (
(( )
)) *
{ 	
} 	
public %
PrecioParaRegistroDeVenta (
(( )
Precio) /
precio0 6
)6 7
{ 	
this 
. 
Id 
= 
precio 
. 
id 
;  
this 
. 
Tarifa 
= 
precio  
.  !
Detalle_maestro3! 1
.1 2
nombre2 8
;8 9
this 
. 
Valor 
= 
precio 
.  
valor  %
;% &
this 
. 
IdTarifa 
= 
precio "
." #
id_tarifa_d# .
;. /
this 
. 
Codigo 
= 
precio  
.  !
Detalle_maestro3! 1
.1 2
codigo2 8
;8 9
} 	
public"" %
PrecioParaRegistroDeVenta"" (
(""( )-
!Precio_Concepto_Negocio_Comercial"") J
precio""K Q
)""Q R
{## 	
this$$ 
.$$ 
Id$$ 
=$$ 
precio$$ 
.$$ 
Id$$ 
;$$  
this%% 
.%% 
Tarifa%% 
=%% 
precio%%  
.%%  !
Tarifa%%! '
;%%' (
this&& 
.&& 
Valor&& 
=&& 
precio&& 
.&&  
Valor&&  %
;&&% &
this'' 
.'' 
IdTarifa'' 
='' 
precio'' "
.''" #
IdTarifa''# +
;''+ ,
this(( 
.(( 
Codigo(( 
=(( 
precio((  
.((  !
Codigo((! '
;((' (
})) 	
internal++ 
static++ 
List++ 
<++ %
PrecioParaRegistroDeVenta++ 6
>++6 7
Convert++8 ?
(++? @
List++@ D
<++D E
Precio++E K
>++K L
precios++M T
)++T U
{,, 	
List-- 
<-- %
PrecioParaRegistroDeVenta-- *
>--* +
	resultado--, 5
=--6 7
new--8 ;
List--< @
<--@ A%
PrecioParaRegistroDeVenta--A Z
>--Z [
(--[ \
)--\ ]
;--] ^
foreach.. 
(.. 
var.. 
precio.. 
in..  "
precios..# *
)..* +
{// 
	resultado00 
.00 
Add00 
(00 
new00 !%
PrecioParaRegistroDeVenta00" ;
(00; <
precio00< B
)00B C
)00C D
;00D E
}11 
return22 
	resultado22 
;22 
}33 	
internal55 
static55 
List55 
<55 %
PrecioParaRegistroDeVenta55 6
>556 7
Convert558 ?
(55? @
List55@ D
<55D E-
!Precio_Concepto_Negocio_Comercial55E f
>55f g
precios55h o
)55o p
{66 	
List77 
<77 %
PrecioParaRegistroDeVenta77 *
>77* +
	resultado77, 5
=776 7
new778 ;
List77< @
<77@ A%
PrecioParaRegistroDeVenta77A Z
>77Z [
(77[ \
)77\ ]
;77] ^
foreach88 
(88 
var88 
precio88 
in88  "
precios88# *
)88* +
{99 
	resultado:: 
.:: 
Add:: 
(:: 
new:: !%
PrecioParaRegistroDeVenta::" ;
(::; <
precio::< B
)::B C
)::C D
;::D E
};; 
return<< 
	resultado<< 
;<< 
}== 	
internal?? 
static?? 
List?? 
<?? 
Precio?? #
>??# $
Convert??% ,
(??, -
List??- 1
<??1 2%
PrecioParaRegistroDeVenta??2 K
>??K L
precios??M T
)??T U
{@@ 	
ListBB 
<BB 
PrecioBB 
>BB 
	resultadoBB "
=BB# $
newBB% (
ListBB) -
<BB- .
PrecioBB. 4
>BB4 5
(BB5 6
)BB6 7
;BB7 8
PrecioCC 
precioCC 
;CC 
foreachDD 
(DD 
varDD 
preciovmDD !
inDD" $
preciosDD% ,
)DD, -
{EE 
precioFF 
=FF 
newFF 
PrecioFF #
(FF# $
)FF$ %
;FF% &
precioGG 
.GG 
id_tarifa_dGG "
=GG# $
preciovmGG% -
.GG- .
IdTarifaGG. 6
;GG6 7
precioHH 
.HH 
valorHH 
=HH 
preciovmHH '
.HH' (
ValorHH( -
;HH- .
	resultadoII 
.II 
AddII 
(II 
precioII $
)II$ %
;II% &
}JJ 
returnKK 
	resultadoKK 
;KK 
}LL 	
}NN 
}OO �
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Conceptos\RegistroCaracteristicaPropia.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

  
CaracteristicaPropia

 %
{ 
public 
int 
Numero 
{ 
get 
;  
set! $
;$ %
}& '
public
List
<
ValoreCaracteristicaPropia
>
Valores
{
get
;
set
;
}
public  
CaracteristicaPropia #
(# $
)$ %
{ 	
this 
. 
Valores 
= 
new 
List #
<# $&
ValoreCaracteristicaPropia$ >
>> ?
(? @
)@ A
;A B
} 	
public 
static 
List 
< 1
%ValorDetalleMaestroDetalleTransaccion @
>@ A
ConvertB I
(I J
ListJ N
<N O 
CaracteristicaPropiaO c
>c d$
caracteristicasViewModele }
)} ~
{ 	
List 
< 1
%ValorDetalleMaestroDetalleTransaccion 6
>6 7
valores_8 @
=A B
newC F
ListG K
<K L1
%ValorDetalleMaestroDetalleTransaccionL q
>q r
(r s
)s t
;t u
foreach 
( 
var 
caracteristicaModel ,
in- /$
caracteristicasViewModel0 H
)H I
{ 
foreach 
( 
var 
item !
in" $
caracteristicaModel% 8
.8 9
Valores9 @
)@ A
{ 
valores_ 
. 
Add  
(  !
new! $1
%ValorDetalleMaestroDetalleTransaccion% J
(J K
itemK O
.O P
IdP R
,R S
itemS W
.W X
IdDetalleMaestroX h
,h i
caracteristicaModelj }
.} ~
Numero	~ �
,
� �
item
� �
.
� �
Valor
� �
)
� �
)
� �
;
� �
} 
} 
return 
valores_ 
; 
} 	
}   
public"" 

class"" &
ValoreCaracteristicaPropia"" +
{## 
public%% 
int%% 
Id%% 
{%% 
get%% 
;%% 
set%%  
;%%  !
}%%" #
public&& 
int&& 
IdDetalleMaestro&& #
{&&$ %
get&&& )
;&&) *
set&&+ .
;&&. /
}&&0 1
public'' 
string'' 
Valor'' 
{'' 
get'' !
;''! "
set''# &
;''& '
}''( )
public)) &
ValoreCaracteristicaPropia)) )
())) *
)))* +
{** 	
},, 	
}.. 
}// �4
bD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Finanza\RegistroDeFinanciamiento.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 $
RegistroDeFinanciamiento		 )
{

 
public 
decimal 
Inicial 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Capital 
{  
get! $
;$ %
set& )
;) *
}+ ,
public
decimal
Interes
{
get
;
set
;
}
public 
decimal 
CapitalInteres %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
IEnumerable 
< +
RegistroDetalleDeFinanciamiento :
>: ;
Detalles< D
{E F
getG J
;J K
setL O
;O P
}Q R
} 
public 

class +
RegistroDetalleDeFinanciamiento 0
{ 
public 
int 
IdCuota 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
Cuota 
{ 
get 
; 
set  #
;# $
}% &
public 
decimal 
CapitalCuota #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
InteresCuota #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
ImporteCuota #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
bool 
EsCuotaInicial "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
DateTime 
FechaVencimiento (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
ObservacionCuota &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public +
RegistroDetalleDeFinanciamiento .
(. /
)/ 0
{ 	
} 	
public!! +
RegistroDetalleDeFinanciamiento!! .
(!!. /
int!!/ 2
numCuota!!3 ;
,!!; <
Cuota!!= B
cuota!!C H
)!!H I
{"" 	
IdCuota## 
=## 
cuota## 
.## 
id## 
;## 
Cuota$$ 
=$$ 
numCuota$$ 
;$$ 
CapitalCuota%% 
=%% 
cuota%%  
.%%  !
capital%%! (
;%%( )
InteresCuota&& 
=&& 
cuota&&  
.&&  !
interes&&! (
;&&( )
ImporteCuota'' 
='' 
cuota''  
.''  !
capital''! (
*'') *
cuota''+ 0
.''0 1
interes''1 8
;''8 9
FechaVencimiento(( 
=(( 
cuota(( $
.(($ %
fecha_vencimiento((% 6
;((6 7
ObservacionCuota)) 
=)) 
cuota)) $
.))$ %

comentario))% /
;))/ 0
}** 	
public.. 
static.. 
List.. 
<.. +
RegistroDetalleDeFinanciamiento.. :
>..: ;
Convert_..< D
(..D E
List..E I
<..I J
Cuota..J O
>..O P
cuotas..Q W
)..W X
{// 	
List00 
<00 +
RegistroDetalleDeFinanciamiento00 0
>000 1
nuevasCuotas002 >
=00? @
new00A D
List00E I
<00I J+
RegistroDetalleDeFinanciamiento00J i
>00i j
(00j k
)00k l
;00l m
int11 
numeroCuota11 
=11 
$num11 
;11  
foreach22 
(22 
var22 
item22 
in22  
cuotas22! '
)22' (
{33 
nuevasCuotas44 
.44 
Add44  
(44  !
new44! $+
RegistroDetalleDeFinanciamiento44% D
(44D E
numeroCuota44E P
++44P R
,44R S
item44T X
)44X Y
)44Y Z
;44Z [
}55 
return66 
nuevasCuotas66 
;66  
}77 	
public88 
static88 
List88 
<88 
Cuota88  
>88  !
Convert_88" *
(88* +
List88+ /
<88/ 0+
RegistroDetalleDeFinanciamiento880 O
>88O P
cuotas88Q W
)88W X
{99 	
List:: 
<:: 
Cuota:: 
>:: 
cuotasConstruidas:: )
=::* +
new::, /
List::0 4
<::4 5
Cuota::5 :
>::: ;
(::; <
)::< =
;::= >
foreach;; 
(;; 
var;; 
item;; 
in;;  
cuotas;;! '
);;' (
{<< 
cuotasConstruidas== !
.==! "
Add==" %
(==% &
new==& )
Cuota==* /
(==/ 0
)==0 1
{>> 
codigo?? 
=?? 
$str?? 
,??  

=@@" #
item@@$ (
.@@( )
FechaVencimiento@@) 9
,@@9 :
fecha_vencimientoAA %
=AA& '
itemAA( ,
.AA, -
FechaVencimientoAA- =
,AA= >
capitalBB 
=BB 
itemBB "
.BB" #
CapitalCuotaBB# /
,BB/ 0
interesCC 
=CC 
itemCC "
.CC" #
InteresCuotaCC# /
,CC/ 0
totalDD 
=DD 
itemDD  
.DD  !
ImporteCuotaDD! -
,DD- .

por_cobrarEE 
=EE  
falseEE! &
,EE& '

=FF" #
itemFF$ (
.FF( )
EsCuotaInicialFF) 7
}GG 
)GG 
;GG 
}HH 
returnII 
cuotasConstruidasII $
;II$ %
}JJ 	
}KK 
}NN �
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Operacion\RegistroTransacciones.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public

class
RegistroTransacciones
{ 
public 
List 
< 
Transaccion 
>  
Transacciones_Crear! 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
List 
< 
Transaccion 
>  #
Transacciones_Modificar! 8
{9 :
get; >
;> ?
set@ C
;C D
}E F
public 
List 
< 
Detalle_transaccion '
>' (%
DetallesTransaccion_Crear) B
{C D
getE H
;H I
setJ M
;M N
}O P
public 
List 
< 
Detalle_transaccion '
>' ()
DetallesTransaccion_Modificar) F
{G H
getI L
;L M
setN Q
;Q R
}S T
public 
List 
< 
Estado_transaccion &
>& '$
EstadosTransaccion_Crear( @
{A B
getC F
;F G
setH K
;K L
}M N
public 
List 
< 
Estado_cuota  
>  !
EstadosCuota_Crear" 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
public 
List 
< 

>! "%
Actores_Negocio_Modificar# <
{= >
get? B
;B C
setD G
;G H
}I J
public !
RegistroTransacciones $
($ %
)% &
{ 	
} 	
public !
RegistroTransacciones $
($ %
List% )
<) *
Transaccion* 5
>5 6
transacciones_Crear7 J
,J K
ListL P
<P Q
TransaccionQ \
>\ ]#
transacciones_Modificar^ u
,u v
Listw {
<{ | 
Detalle_transaccion	| �
>
� �'
detallesTransaccion_Crear
� �
,
� �
List
� �
<
� �!
Detalle_transaccion
� �
>
� �+
detallesTransaccion_Modificar
� �
,
� �
List
� �
<
� � 
Estado_transaccion
� �
>
� �&
estadosTransaccion_Crear
� �
,
� �
List
� �
<
� �
Estado_cuota
� �
>
� � 
estadosCuota_Crear
� �
)
� �
{ 	
Transacciones_Crear 
=  !
transacciones_Crear" 5
;5 6#
Transacciones_Modificar #
=$ %#
transacciones_Modificar& =
;= >%
DetallesTransaccion_Crear   %
=  & '%
detallesTransaccion_Crear  ( A
;  A B)
DetallesTransaccion_Modificar!! )
=!!* +)
detallesTransaccion_Modificar!!, I
;!!I J$
EstadosTransaccion_Crear"" $
=""% &$
estadosTransaccion_Crear""' ?
;""? @
EstadosCuota_Crear## 
=##  
estadosCuota_Crear##! 3
;##3 4
}$$ 	
}%% 
}&& ��
WD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Venta\RegistroDeVenta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 
RegistroDeVenta

  
{ 
public 
long 
IdVenta 
{ 
get !
;! "
set# &
;& '
}( )
public
bool
GrabaIgv
{
get
;
set
;
}
public 
bool 
DetalleUnificado $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
bool 

{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 
ModoPago 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool  
HayRegistroTrazaPago (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
bool +
HayRegistroMovimientoMercaderia 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
bool 
UsaComprobanteOrden '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
Alias 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
decimal 
Inicial 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
Flete 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Placa 
{ 
get !
;! "
set# &
;& '
}( )
public 
DateTime 
FechaEmision $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
ActorComercial_ 
Cliente &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public %
SelectorTipoDeComprobante (
TipoDeComprobante) :
{; <
get= @
;@ A
setB E
;E F
}G H
public 
IEnumerable 
< +
RegistroDetalleDeFinanciamiento :
>: ;
Cuotas< B
{C D
getE H
;H I
setJ M
;M N
}O P
public 
IEnumerable 
< 
DetalleDeOperacion -
>- .
Detalles/ 7
{8 9
get: =
;= >
set? B
;B C
}D E
public## 
TrazaDePago_## 
TrazaDePago## '
{##( )
get##* -
;##- .
set##/ 2
;##2 3
}##4 5
public$$ 
DatosOrdenVenta$$ 
Facturacion$$ *
{$$+ ,
get$$- 0
;$$0 1
set$$2 5
;$$5 6
}$$7 8
public&& 
bool&& !
HaySalidaDeMercaderia&& )
{&&* +
get&&, /
;&&/ 0
set&&1 4
;&&4 5
}&&6 7
public'' 
IEnumerable'' 
<'' %
RegistroMovimientoAlmacen'' 4
>''4 5
SalidasDeMercaderia''6 I
{''J K
get''L O
;''O P
set''Q T
;''T U
}''V W
public(( 
int(( "
NumeroBolsasDePlastico(( )
{((* +
get((, /
;((/ 0
set((1 4
;((4 5
}((6 7
public)) 
decimal)) 
Icbper)) 
{)) 
get))  #
;))# $
set))% (
;))( )
}))* +
public++ 
ItemGenerico++ 
PuntoDeVenta++ (
{++) *
get+++ .
;++. /
set++0 3
;++3 4
}++5 6
public,, 
ItemGenerico,, 
Almacen,, #
{,,$ %
get,,& )
;,,) *
set,,+ .
;,,. /
},,0 1
public-- 
ItemGenerico-- 
Caja--  
{--! "
get--# &
;--& '
set--( +
;--+ ,
}--- .
public.. 
ItemGenerico.. 
Vendedor.. $
{..% &
get..' *
;..* +
set.., /
;../ 0
}..1 2
public// 
ItemGenerico// 
Cajero// "
{//# $
get//% (
;//( )
set//* -
;//- .
}/// 0
public00 
ItemGenerico00 

Almacenero00 &
{00' (
get00) ,
;00, -
set00. 1
;001 2
}003 4
public22 
RegistroDeVenta22 
(22 
)22  
{33 	
}44 	
publicRR 
RegistroDeVentaRR 
(RR 
VentaMasivaRR *
ventaMasivaRR+ 6
,RR6 7
ListRR8 <
<RR< =
VentaMonoDetalleRR= M
>RRM N
ventasMonoDetalleRRO `
)RR` a
{SS 	
VendedorTT 
=TT 
newTT 
ItemGenericoTT '
(TT' (
ventaMasivaTT( 3
.TT3 4

IdVendedorTT4 >
)TT> ?
;TT? @
PuntoDeVentaUU 
=UU 
newUU 
ItemGenericoUU +
(UU+ ,
ventaMasivaUU, 7
.UU7 8
IdPuntoDeVentaUU8 F
)UUF G
;UUG H
CajeroVV 
=VV 
newVV 
ItemGenericoVV %
(VV% &
ventaMasivaVV& 1
.VV1 2
IdCajeroVV2 :
)VV: ;
;VV; <
CajaWW 
=WW 
newWW 
ItemGenericoWW #
(WW# $
ventaMasivaWW$ /
.WW/ 0
IdCajaWW0 6
)WW6 7
;WW7 8

AlmaceneroXX 
=XX 
newXX 
ItemGenericoXX )
(XX) *
ventaMasivaXX* 5
.XX5 6
IdAlmaceneroXX6 B
)XXB C
;XXC D
AlmacenYY 
=YY 
newYY 
ItemGenericoYY &
(YY& '
ventaMasivaYY' 2
.YY2 3
	IdAlmacenYY3 <
)YY< =
;YY= >
ClienteZZ 
=ZZ 
newZZ 
ActorComercial_ZZ )
(ZZ) *
)ZZ* +
{ZZ, -
IdZZ. 0
=ZZ1 2
ventasMonoDetalleZZ3 D
.ZZD E
FirstZZE J
(ZZJ K
)ZZK L
.ZZL M
	IdClienteZZM V
}ZZW X
;ZZX Y
Alias[[ 
=[[ 
$str[[ 
;[[ 
TipoDeComprobante\\ 
=\\ 
new\\  #%
SelectorTipoDeComprobante\\$ =
(\\= >
)\\> ?
{]] 
EsPropio^^ 
=^^ 
true^^ 
,^^  
TipoComprobante__ 
=__  !
new__" %
ItemGenerico__& 2
(__2 3
ventaMasiva__3 >
.__> ?
IdTipoDeComprobante__? R
)__R S
,__S T
SerieSeleccionada`` !
=``" #
ventaMasiva``$ /
.``/ 0 
IdSerieDeComprobante``0 D
}aa 
;aa

=bb 
truebb  
;bb  !
FechaEmisioncc 
=cc 
ventaMasivacc &
.cc& '
FechaEmisioncc' 3
;cc3 4
GrabaIgvdd 
=dd 
!dd 
TransaccionSettingsdd +
.dd+ ,
Defaultdd, 3
.dd3 4
AplicaLeyAmazoniadd4 E
;ddE F
DetalleUnificadoee 
=ee 
falseee $
;ee$ %
Observacionff 
=ff 
$strff 
;ff 
Cuotasgg 
=gg 
nullgg 
;gg 
Inicialhh 
=hh 
$numhh 
;hh 
Fleteii 
=ii 
$numii 
;ii "
NumeroBolsasDePlasticojj "
=jj# $
$numjj% &
;jj& '
Icbperkk 
=kk 
$numkk 
;kk 
TrazaDePagoll 
=ll 
newll 
TrazaDePago_ll *
(ll* +
)ll+ ,
;ll, - 
HayRegistroTrazaPagomm  
=mm! "
falsemm# (
;mm( )+
HayRegistroMovimientoMercaderiann +
=nn, -
falsenn. 3
;nn3 4!
HaySalidaDeMercaderiaoo !
=oo" #
falseoo$ )
;oo) *
SalidasDeMercaderiapp 
=pp  !
nullpp" &
;pp& '
UsaComprobanteOrdenqq 
=qq  !
falseqq" '
;qq' (
Detallesrr 
=rr 
DetalleDeOperacionrr )
.rr) *
Convertrr* 1
(rr1 2
ventasMonoDetallerr2 C
)rrC D
;rrD E
}ss 	
publictt 
statictt 
RegistroDeVentatt %(
ConvertVentaCobroPorVendedortt& B
(ttB C
VentaMasivattC N
ventaMasivattO Z
,ttZ [
Listtt\ `
<tt` a
VentaMonoDetalletta q
>ttq r
ventasMonoDetalle	tts �
)
tt� �
{uu 	
returnvv 
newvv 
RegistroDeVentavv &
(vv& '
ventaMasivavv' 2
,vv2 3
ventasMonoDetallevv4 E
)vvE F
{ww 
ModoPagoxx 
=xx 
(xx 
intxx 
)xx  
	Entidadesxx  )
.xx) *
ModoPagoxx* 2
.xx2 3

,xx@ A
TrazaDePagoyy 
=yy 
newyy !
TrazaDePago_yy" .
(yy. /
)yy/ 0
}zz 
;zz
}{{ 	
public}} 
static}} 
RegistroDeVenta}} %
ConvertVentaMasiva}}& 8
(}}8 9
VentaMasiva}}9 D
ventaMasiva}}E P
,}}P Q
VentaMonoDetalle}}R b
ventaMonoDetalle}}c s
)}}s t
{~~ 	
return 
new 
RegistroDeVenta &
(& '
ventaMasiva' 2
,2 3
new4 7
List8 <
<< =
VentaMonoDetalle= M
>M N
{O P
ventaMonoDetalleQ a
}b c
)c d
{
�� 
ModoPago
�� 
=
�� 
(
�� 
int
�� 
)
��  
	Entidades
��  )
.
��) *
ModoPago
��* 2
.
��2 3
Contado
��3 :
,
��: ;
TrazaDePago
�� 
=
�� 
new
�� !
TrazaDePago_
��" .
(
��. /
)
��/ 0
{
�� 
MedioDePago
�� 
=
��  !
new
��" %
ItemGenerico
��& 2
(
��2 3
MaestroSettings
��3 B
.
��B C
Default
��C J
.
��J K1
#IdDetalleMaestroMedioDepagoEfectivo
��K n
)
��n o
}
�� 
}
�� 
;
��
}
�� 	
public
�� 
List
�� 
<
��  
DetalleDeOperacion
�� &
>
��& '!
DetallesDeOperacion
��( ;
{
��< =
get
��> A
;
��A B
set
��C F
;
��F G
}
��H I
public
�� 
DateTime
�� 

�� %
{
��& '
get
��( +
;
��+ ,
set
��- 0
;
��0 1
}
��2 3
public
�� 
ModoPago
�� 

ModoDePago
�� "
{
�� 	
get
�� 
{
�� 
return
�� 
(
�� 
	Entidades
�� !
.
��! "
ModoPago
��" *
)
��* +
ModoPago
��+ 3
;
��3 4
}
�� 
}
�� 	
public
�� 
bool
�� 
HayIngresoDinero
�� $
{
�� 	
get
�� 
{
�� 
return
�� 
(
�� 

ModoDePago
�� "
==
��# %
	Entidades
��& /
.
��/ 0
ModoPago
��0 8
.
��8 9
Contado
��9 @
||
��A C
Inicial
��D K
>
��L M
$num
��N O
)
��O P
;
��P Q
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
Cuota
�� 
>
�� 

�� (
{
�� 	
get
�� 
{
�� 
return
�� 

ModoDePago
�� !
==
��" $
	Entidades
��% .
.
��. /
ModoPago
��/ 7
.
��7 8 
CreditoConfigurado
��8 J
?
��K L-
RegistroDetalleDeFinanciamiento
��M l
.
��l m
Convert_
��m u
(
��u v
Cuotas
��v |
.
��| }
ToList��} �
(��� �
)��� �
)��� �
:��� �
null��� �
;��� �
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� !
MovimientoDeAlmacen
�� '
>
��' (.
 ComprobantesDeSalidaDeMercaderia
��) I
{
�� 	
get
�� 
{
�� 
return
�� #
HaySalidaDeMercaderia
�� ,
?
��- .'
RegistroMovimientoAlmacen
��/ H
.
��H I
Convert_
��I Q
(
��Q R!
SalidasDeMercaderia
��R e
.
��e f
ToList
��f l
(
��l m
)
��m n
)
��n o
:
��p q
null
��r v
;
��v w
}
�� 
}
�� 	
public
�� 
bool
�� 

�� !
{
�� 	
get
�� 
{
�� 
return
�� 
(
�� 
!
�� !
TransaccionSettings
�� ,
.
��, -
Default
��- 4
.
��4 5
AplicaLeyAmazonia
��5 F
&&
��G I
TipoDeComprobante
��J [
.
��[ \
TipoComprobante
��\ k
.
��k l
Id
��l n
!=
��o q
MaestroSettings��r �
.��� �
Default��� �
.��� �;
+IdDetalleMaestroComprobanteNotaVentaInterna��� �
)��� �
||��� �
(��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �!
AplicaLeyAmazonia��� �
&&��� �!
TipoDeComprobante��� �
.��� �
TipoComprobante��� �
.��� �
Id��� �
!=��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �;
+IdDetalleMaestroComprobanteNotaVentaInterna��� �
&&��� �
GrabaIgv��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 
decimal
�� 
ImporteTotal
�� #
{
�� 	
get
�� 
{
�� 
return
�� !
DetallesDeOperacion
�� *
!=
��+ -
null
��. 2
?
��3 4!
DetallesDeOperacion
��5 H
.
��H I
Sum
��I L
(
��L M
d
��M N
=>
��O Q
d
��R S
.
��S T
Importe
��T [
)
��[ \
+
��] ^
Icbper
��_ e
:
��f g
$num
��h i
;
��i j
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� !
Detalle_transaccion
�� '
>
��' (
DetallesDeVenta
��) 8
{
�� 	
get
�� 
{
�� 
return
�� !
DetallesDeOperacion
�� *
.
��* +
Select
��+ 1
(
��1 2
d
��2 3
=>
��4 6
d
��7 8
.
��8 9 
DetalleTransaccion
��9 K
(
��K L
)
��L M
)
��M N
.
��N O
ToList
��O U
(
��U V
)
��V W
;
��W X
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� !
Detalle_transaccion
�� '
>
��' ()
DetallesDeVentaQueSonBienes
��) D
{
�� 	
get
�� 
{
�� 
return
�� !
DetallesDeOperacion
�� *
.
��* +
Where
��+ 0
(
��0 1
d
��1 2
=>
��3 5
d
��6 7
.
��7 8
Producto
��8 @
.
��@ A
EsBien
��A G
)
��G H
.
��H I
Select
��I O
(
��O P
dt
��P R
=>
��S U
dt
��V X
.
��X Y 
DetalleTransaccion
��Y k
(
��k l
)
��l m
)
��m n
.
��n o
ToList
��o u
(
��u v
)
��v w
;
��w x
}
�� 
}
�� 	
}
�� 
public
�� 

class
�� $
OperacionModificatoria
�� '
:
��( ) 
OperacionIntegrada
��* <
{
�� 
public
�� 
List
�� 
<
��  
Estado_transaccion
�� &
>
��& '3
%NuevosEstadosTransaccionesModificadas
��( M
{
��N O
get
��P S
;
��S T
set
��U X
;
��X Y
}
��Z [
public
�� 
List
�� 
<
�� 
Estado_cuota
��  
>
��  !=
/NuevosEstadosParaCuotasTransaccionesModificadas
��" Q
{
��R S
get
��T W
;
��W X
set
��Y \
;
��\ ]
}
��^ _
public
�� 
List
�� 
<
�� 
Cuota
�� 
>
�� 
CuotasModificadas
�� ,
{
��- .
get
��/ 2
;
��2 3
set
��4 7
;
��7 8
}
��9 :
public
�� 
List
�� 
<
�� !
Detalle_transaccion
�� '
>
��' (,
DetallesTransaccionModificados
��) G
{
��H I
get
��J M
;
��M N
set
��O R
;
��R S
}
��T U
}
�� 
public
�� 

class
��  
OperacionSoloOrden
�� #
{
�� 
public
�� 
Transaccion
�� 
	Operacion
�� $
{
��% &
get
��' *
;
��* +
set
��, /
;
��/ 0
}
��1 2
public
�� 
Transaccion
�� 
OrdenDeOperacion
�� +
{
��, -
get
��. 1
;
��1 2
set
��3 6
;
��6 7
}
��8 9
public
�� 
Transaccion
�� 
OperacionOrigen
�� *
{
��+ ,
get
��- 0
;
��0 1
set
��2 5
;
��5 6
}
��7 8
public
�� 
void
�� "
EnlazarTransacciones
�� (
(
��( )
)
��) *
{
�� 	
if
�� 
(
�� 
OrdenDeOperacion
��  
!=
��! #
null
��$ (
)
��( )
{
�� 
OrdenDeOperacion
��  
.
��  !
Transaccion2
��! -
=
��. /
	Operacion
��0 9
;
��9 :
	Operacion
�� 
.
�� 
Transaccion1
�� &
.
��& '
Add
��' *
(
��* +
OrdenDeOperacion
��+ ;
)
��; <
;
��< =
}
�� 
if
�� 
(
�� 
OperacionOrigen
�� 
!=
��  "
null
��# '
)
��' (
{
�� 
OrdenDeOperacion
��  
.
��  !
Transaccion3
��! -
=
��. /
OperacionOrigen
��0 ?
;
��? @
}
�� 
}
�� 	
public
�� 
void
��  
AsignarComprobante
�� &
(
��& '
Comprobante
��' 2
comprobante
��3 >
)
��> ?
{
�� 	
if
�� 
(
�� 
	Operacion
�� 
!=
�� 
null
�� !
)
��! "
	Operacion
��# ,
.
��, -
Comprobante
��- 8
=
��9 :
	Operacion
��; D
.
��D E
Comprobante
��E P
??
��Q S
comprobante
��T _
;
��_ `
if
�� 
(
�� 
OrdenDeOperacion
��  
!=
��! #
null
��$ (
)
��( )
OrdenDeOperacion
��* :
.
��: ;
Comprobante
��; F
=
��G H
OrdenDeOperacion
��I Y
.
��Y Z
Comprobante
��Z e
??
��f h
comprobante
��i t
;
��t u
}
�� 	
public
��  
OperacionSoloOrden
�� !
(
��! "
Transaccion
��" -
	operacion
��. 7
,
��7 8
Transaccion
��9 D
ordenDeOperacion
��E U
,
��U V
Transaccion
��W b
operacionOrigen
��c r
)
��r s
{
�� 	
	Operacion
�� 
=
�� 
	operacion
�� !
;
��! "
OrdenDeOperacion
�� 
=
�� 
ordenDeOperacion
�� /
;
��/ 0
OperacionOrigen
�� 
=
�� 
operacionOrigen
�� -
;
��- .
this
�� 
.
�� "
EnlazarTransacciones
�� %
(
��% &
)
��& '
;
��' (
}
�� 	
}
�� 
public
�� 

class
��  
OperacionIntegrada
�� #
{
�� 
public
�� 
long
�� 
Id
�� 
{
�� 
get
�� 
;
�� 
set
�� !
;
��! "
}
��# $
public
�� 
Transaccion
�� 
	Operacion
�� $
{
��% &
get
��' *
;
��* +
set
��, /
;
��/ 0
}
��1 2
public
�� 
Transaccion
�� 
OrdenDeOperacion
�� +
{
��, -
get
��. 1
;
��1 2
set
��3 6
;
��6 7
}
��8 9
public
�� 
Transaccion
�� !
MovimientoEconomico
�� .
{
��/ 0
get
��1 4
;
��4 5
set
��6 9
;
��9 :
}
��; <
public
�� 
List
�� 
<
�� 
Transaccion
�� 
>
��  
MovimientosBienes
��! 2
{
��3 4
get
��5 8
;
��8 9
set
��: =
;
��= >
}
��? @
public
�� 
Transaccion
�� 
OperacionOrigen
�� *
{
��+ ,
get
��- 0
;
��0 1
set
��2 5
;
��5 6
}
��7 8
public
�� 
List
�� 
<
�� 
Transaccion
�� 
>
��  &
TransaccionesModificadas
��! 9
{
��: ;
get
��< ?
;
��? @
set
��A D
;
��D E
}
��F G
public
�� 
Transaccion
�� 
OperacionCreacion
�� ,
{
��- .
get
��/ 2
;
��2 3
set
��4 7
;
��7 8
}
��9 :
public
�� 
List
�� 
<
�� 

�� !
>
��! "'
ActoresNegocioModificados
��# <
{
��= >
get
��? B
;
��B C
set
��D G
;
��G H
}
��I J
public
�� 
List
�� 
<
��  
Estado_transaccion
�� &
>
��& '&
NuevosEstadosTransaccion
��( @
{
��A B
get
��C F
;
��F G
set
��H K
;
��K L
}
��M N
public
��  
OperacionIntegrada
�� !
(
��! "
)
��" #
{
�� 	
}
�� 	
public
�� 
void
�� "
EnlazarTransacciones
�� (
(
��( )
)
��) *
{
�� 	
if
�� 
(
�� 
OrdenDeOperacion
��  
!=
��! #
null
��$ (
)
��( )
{
�� 
OrdenDeOperacion
��  
.
��  !
Transaccion2
��! -
=
��. /
	Operacion
��0 9
;
��9 :
	Operacion
�� 
.
�� 
Transaccion1
�� &
.
��& '
Add
��' *
(
��* +
OrdenDeOperacion
��+ ;
)
��; <
;
��< =
}
�� 
if
�� 
(
�� !
MovimientoEconomico
�� #
!=
��$ &
null
��' +
)
��+ ,
{
�� 
MovimientoEconomico
�� #
.
��# $
Transaccion2
��$ 0
=
��1 2
	Operacion
��3 <
;
��< =
	Operacion
�� 
.
�� 
Transaccion1
�� &
.
��& '
Add
��' *
(
��* +!
MovimientoEconomico
��+ >
)
��> ?
;
��? @
}
�� 
foreach
�� 
(
�� 
var
��  
salidaDeMercaderia
�� +
in
��, .
MovimientosBienes
��/ @
)
��@ A
{
�� 
salidaDeMercaderia
�� "
.
��" #
Transaccion2
��# /
=
��0 1
	Operacion
��2 ;
;
��; < 
salidaDeMercaderia
�� "
.
��" #
Transaccion3
��# /
=
��0 1
OrdenDeOperacion
��2 B
;
��B C
	Operacion
�� 
.
�� 
Transaccion1
�� &
.
��& '
Add
��' *
(
��* + 
salidaDeMercaderia
��+ =
)
��= >
;
��> ?
}
�� 
if
�� 
(
�� 
OperacionOrigen
�� 
!=
��  "
null
��# '
)
��' (
{
�� 
OrdenDeOperacion
��  
.
��  !
Transaccion3
��! -
=
��. /
OperacionOrigen
��0 ?
;
��? @
}
�� 
if
�� 
(
�� 
OperacionCreacion
�� !
!=
��" $
null
��% )
)
��) *
{
�� 
OrdenDeOperacion
��  
.
��  !
Transaccion3
��! -
=
��. /
OperacionCreacion
��0 A
;
��A B
}
�� 
}
�� 	
public
�� 
void
��  
AsignarComprobante
�� &
(
��& '
Comprobante
��' 2
comprobante
��3 >
)
��> ?
{
�� 	
if
�� 
(
�� 
	Operacion
�� 
!=
�� 
null
�� !
)
��! "
	Operacion
��# ,
.
��, -
Comprobante
��- 8
=
��9 :
	Operacion
��; D
.
��D E
Comprobante
��E P
??
��Q S
comprobante
��T _
;
��_ `
if
�� 
(
�� 
OrdenDeOperacion
��  
!=
��! #
null
��$ (
)
��( )
OrdenDeOperacion
��* :
.
��: ;
Comprobante
��; F
=
��G H
OrdenDeOperacion
��I Y
.
��Y Z
Comprobante
��Z e
??
��f h
comprobante
��i t
;
��t u
if
�� 
(
�� !
MovimientoEconomico
�� #
!=
��$ &
null
��' +
)
��+ ,!
MovimientoEconomico
��- @
.
��@ A
Comprobante
��A L
=
��M N!
MovimientoEconomico
��O b
.
��b c
Comprobante
��c n
??
��o q
comprobante
��r }
;
��} ~
foreach
�� 
(
�� 
var
��  
salidaDeMercaderia
�� +
in
��, .
MovimientosBienes
��/ @
)
��@ A
{
�� 
salidaDeMercaderia
�� "
.
��" #
Comprobante
��# .
=
��/ 0 
salidaDeMercaderia
��1 C
.
��C D
Comprobante
��D O
??
��P R
comprobante
��S ^
;
��^ _
}
�� 
}
�� 	
public
�� 
void
�� #
ReemplazarComprobante
�� )
(
��) *
Comprobante
��* 5
comprobante
��6 A
)
��A B
{
�� 	
if
�� 
(
�� 
	Operacion
�� 
!=
�� 
null
�� !
)
��! "
	Operacion
��# ,
.
��, -
Comprobante
��- 8
=
��9 :
comprobante
��; F
;
��F G
if
�� 
(
�� 
OrdenDeOperacion
��  
!=
��! #
null
��$ (
)
��( )
OrdenDeOperacion
��* :
.
��: ;
Comprobante
��; F
=
��G H
comprobante
��I T
;
��T U
if
�� 
(
�� !
MovimientoEconomico
�� #
!=
��$ &
null
��' +
)
��+ ,!
MovimientoEconomico
��- @
.
��@ A
Comprobante
��A L
=
��M N
comprobante
��O Z
;
��Z [
foreach
�� 
(
�� 
var
��  
salidaDeMercaderia
�� +
in
��, .
MovimientosBienes
��/ @
)
��@ A
{
�� 
salidaDeMercaderia
�� "
.
��" #
Comprobante
��# .
=
��/ 0
comprobante
��1 <
;
��< =
}
�� 
}
�� 	
public
��  
OperacionIntegrada
�� !
(
��! "
Transaccion
��" -
	operacion
��. 7
,
��7 8
Transaccion
��9 D
ordenDeOperacion
��E U
,
��U V
Transaccion
��W b
cobro
��c h
,
��h i
List
��j n
<
��n o
Transaccion
��o z
>
��z {"
salidasDeMercaderia��| �
,��� �
Transaccion��� �
operacionOrigen��� �
,��� �
Transaccion��� �!
operacionCreacion��� �
)��� �
{
�� 	
	Operacion
�� 
=
�� 
	operacion
�� !
;
��! "
OrdenDeOperacion
�� 
=
�� 
ordenDeOperacion
�� /
;
��/ 0!
MovimientoEconomico
�� 
=
��  !
cobro
��" '
;
��' (
MovimientosBienes
�� 
=
�� !
salidasDeMercaderia
��  3
;
��3 4
OperacionOrigen
�� 
=
�� 
operacionOrigen
�� -
;
��- .
OperacionCreacion
�� 
=
�� 
operacionCreacion
��  1
;
��1 2
}
�� 	
}
�� 
public
�� 

class
�� %
OperacionIntegradaSerie
�� (
{
�� 
public
�� %
OperacionIntegradaSerie
�� &
(
��& ' 
OperacionIntegrada
��' 9 
operacionIntegrada
��: L
,
��L M
int
��N Q 
idSerieComprobante
��R d
)
��d e
{
�� 	 
OperacionIntegrada
�� 
=
��   
operacionIntegrada
��! 3
;
��3 4 
IdSerieComprobante
�� 
=
��   
idSerieComprobante
��! 3
;
��3 4
}
�� 	
public
��  
OperacionIntegrada
�� ! 
OperacionIntegrada
��" 4
{
��5 6
get
��7 :
;
��: ;
set
��< ?
;
��? @
}
��A B
public
�� 
int
��  
IdSerieComprobante
�� %
{
��& '
get
��( +
;
��+ ,
set
��- 0
;
��0 1
}
��2 3
}
�� 
public
�� 

class
�� "
OperacionCorporativa
�� %
:
��& '$
OperacionModificatoria
��( >
{
�� 
public
�� 
Transaccion
�� 
OrdenDeAlmacen
�� )
{
��* +
get
��, /
;
��/ 0
set
��1 4
;
��4 5
}
��6 7
}
�� 
public
�� 

class
�� /
!OperacionVentaCobroCarteraCliente
�� 2
{
�� 
public
�� 
Transaccion
�� 
OperacionWrapper
�� +
{
��, -
get
��. 1
;
��1 2
set
��3 6
;
��6 7
}
��8 9
public
�� 
List
�� 
<
��  
OperacionIntegrada
�� &
>
��& '
Ventas
��( .
{
��/ 0
get
��1 4
;
��4 5
set
��6 9
;
��9 :
}
��; <
public
�� 
List
�� 
<
�� 
Transaccion
�� 
>
��  
Cobros
��! '
{
��( )
get
��* -
;
��- .
set
��/ 2
;
��2 3
}
��4 5
}
�� 
}�� ��
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\RegistroMovimientoAlmacen.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public		 

class		 %
RegistroMovimientoAlmacen		 *
{

 
public 
long 
IdOrdenDeAlmacen $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
long 
IdOperacion 
{  !
get" %
;% &
set' *
;* +
}, -
public
ActorComercial_
Tercero
{
get
;
set
;
}
public 
DateTime 
FechaInicioTraslado +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
RegistroTransporte !

{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
RegistroConductor  
	Conductor! *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
ItemGenerico 
ModalidadTransporte /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
ItemGenerico 
MotivoTraslado *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 
DescripcionMotivo '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
decimal 
PesoBrutoTotal %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
int 
NumeroBultos 
{  !
get" %
;% &
set' *
;* +
}, -
public %
SelectorTipoDeComprobante (
TipoDeComprobante) :
{; <
get= @
;@ A
setB E
;E F
}G H
public 
List 
< &
DetalleMovimientoDeAlmacen .
>. /
Detalles0 8
{9 :
get; >
;> ?
set@ C
;C D
}E F
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
ItemGenerico 
UbigeoOrigen (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
DireccionOrigen %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
ItemGenerico 

{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
DireccionDestino &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
ItemGenerico 
Almacen #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
ItemGenerico 

Almacenero &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
DocumentoReferencia )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public   
bool   
EsTrasladoTotal   #
{  $ %
get  & )
;  ) *
set  + .
;  . /
}  0 1
public!! 
int!! 
	IdTercero!! 
{!! 
get!! "
;!!" #
set!!$ '
;!!' (
}!!) *
public## %
RegistroMovimientoAlmacen## (
(##( )
)##) *
{$$ 	
this%% 
.%% 
Tercero%% 
=%% 
new%% 
ActorComercial_%% .
(%%. /
)%%/ 0
{&& 
TipoDocumentoIdentidad'' &
=''' (
new'') ,
ItemGenerico''- 9
(''9 :
)'': ;
}(( 
;((
this)) 
.)) 

=))  
new))! $
RegistroTransporte))% 7
())7 8
)))8 9
;))9 :
this** 
.** 
	Conductor** 
=** 
new**  
RegistroConductor**! 2
(**2 3
)**3 4
;**4 5
this++ 
.++ 
TipoDeComprobante++ "
=++# $
new++% (%
SelectorTipoDeComprobante++) B
(++B C
)++C D
;++D E
this,, 
.,, 
Detalles,, 
=,, 
new,, 
List,,  $
<,,$ %&
DetalleMovimientoDeAlmacen,,% ?
>,,? @
(,,@ A
),,A B
;,,B C
}-- 	
public.. 
static.. 
List.. 
<.. 
MovimientoDeAlmacen.. .
>... /
Convert_..0 8
(..8 9
List..9 =
<..= >%
RegistroMovimientoAlmacen..> W
>..W X
	registros..Y b
)..b c
{// 	
List00 
<00 
MovimientoDeAlmacen00 $
>00$ %
salidasDeMercaderia00& 9
=00: ;
new00< ?
List00@ D
<00D E
MovimientoDeAlmacen00E X
>00X Y
(00Y Z
)00Z [
;00[ \
foreach11 
(11 
var11 
registro11 !
in11" $
	registros11% .
)11. /
{22 
salidasDeMercaderia33 #
.33# $
Add33$ '
(33' (
new33( +
MovimientoDeAlmacen33, ?
(33? @
new33@ C
Transaccion33D O
(33O P
)33P Q
{44 
Detalle_transaccion55 '
=55( )0
$ConstruirDetalleMovimientoMercaderia55* N
(55N O
registro55O W
)55W X
,55X Y

comentario66 
=66  
registro66! )
.66) *
Observacion66* 5
,665 6
}77 
)77 
{88 
	IdTercero99 
=99 
registro99  (
.99( )
Tercero99) 0
.990 1
Id991 3
,993 4%
IdComprobanteSeleccionado:: -
=::. /
registro::0 8
.::8 9
TipoDeComprobante::9 J
.::J K
TipoComprobante::K Z
.::Z [
Id::[ ]
,::] ^
IdSerieSeleccionada;; '
=;;( )
registro;;* 2
.;;2 3
TipoDeComprobante;;3 D
.;;D E
SerieSeleccionada;;E V
,;;V W
EsPropio<< 
=<< 
registro<< '
.<<' (
TipoDeComprobante<<( 9
.<<9 :
EsPropio<<: B
,<<B C
SerieIngresada== "
===# $
registro==% -
.==- .
TipoDeComprobante==. ?
.==? @
SerieIngresada==@ N
,==N O
NumeroIngresado>> #
=>>$ %
registro>>& .
.>>. /
TipoDeComprobante>>/ @
.>>@ A
NumeroIngresado>>A P
,>>P Q
FechaInicioTraslado?? '
=??( )
registro??* 2
.??2 3
FechaInicioTraslado??3 F
,??F G
IdTransportista@@ #
=@@$ %
registro@@& .
.@@. /

.@@< =

.@@J K
Id@@K M
,@@M N
PlacaAA 
=AA 
registroAA $
.AA$ %

.AA2 3
PlacaAA3 8
,AA8 9
IdConductorBB 
=BB  !
registroBB" *
.BB* +
	ConductorBB+ 4
.BB4 5
	ConductorBB5 >
.BB> ?
IdBB? A
,BBA B
NumeroLicenciaCC "
=CC# $
registroCC% -
.CC- .
	ConductorCC. 7
.CC7 8
NumeroLicenciaCC8 F
,CCF G!
IdModalidadTransporteDD )
=DD* +
registroDD, 4
.DD4 5
ModalidadTransporteDD5 H
.DDH I
IdDDI K
,DDK L
IdMotivoTrasladoEE $
=EE% &
registroEE' /
.EE/ 0
MotivoTrasladoEE0 >
.EE> ?
IdEE? A
,EEA B
DescripcionMotivoFF %
=FF& '
registroFF( 0
.FF0 1
DescripcionMotivoFF1 B
,FFB C
PesoBrutoTotalGG "
=GG# $
registroGG% -
.GG- .
PesoBrutoTotalGG. <
,GG< =
NumeroBultosHH  
=HH! "
registroHH# +
.HH+ ,
NumeroBultosHH, 8
,HH8 9
DireccionOrigenII #
=II$ %
registroII& .
.II. /
DireccionOrigenII/ >
+II? @
$strIIA F
+IIG H
registroIII Q
.IIQ R
UbigeoOrigenIIR ^
.II^ _
NombreII_ e
,IIe f
DireccionDestinoJJ $
=JJ% &
registroJJ' /
.JJ/ 0
DireccionDestinoJJ0 @
+JJA B
$strJJC H
+JJI J
registroJJK S
.JJS T

.JJa b
NombreJJb h
,JJh i
IdUbigeoOrigenKK "
=KK# $
registroKK% -
.KK- .
UbigeoOrigenKK. :
.KK: ;
IdKK; =
,KK= >
IdUbigeoDestinoLL #
=LL$ %
registroLL& .
.LL. /

.LL< =
IdLL= ?
}MM 
)MM 
;MM 
}NN 
returnOO 
salidasDeMercaderiaOO &
;OO& '
}PP 	
publicRR 
staticRR 
ListRR 
<RR 
Detalle_transaccionRR .
>RR. /0
$ConstruirDetalleMovimientoMercaderiaRR0 T
(RRT U%
RegistroMovimientoAlmacenRRU n
salidaMercaderiaRRo 
)	RR �
{SS 	
ListTT 
<TT 
Detalle_transaccionTT $
>TT$ %
detallesConstruidosTT& 9
=TT: ;
newTT< ?
ListTT@ D
<TTD E
Detalle_transaccionTTE X
>TTX Y
(TTY Z
)TTZ [
;TT[ \
foreachUU 
(UU 
varUU 
itemUU 
inUU  
salidaMercaderiaUU! 1
.UU1 2
DetallesUU2 :
)UU: ;
{VV 
detallesConstruidosWW #
.WW# $
AddWW$ '
(WW' (
newWW( +
Detalle_transaccionWW, ?
(WW? @
itemWW@ D
.WWD E
IngresoSalidaActualWWE X
,WWX Y
itemWWZ ^
.WW^ _

IdProductoWW_ i
,WWi j
nullWWk o
,WWo p
$numWWq r
,WWr s
$numWWt u
,WWu v
nullWWw {
,WW{ |
$numWW} ~
,WW~ 
null
WW� �
,
WW� �
null
WW� �
,
WW� �
$num
WW� �
,
WW� �
$num
WW� �
,
WW� �
$num
WW� �
,
WW� �
item
WW� �
.
WW� �
Lote
WW� �
,
WW� �
null
WW� �
,
WW� �
null
WW� �
)
WW� �
{XX 
Concepto_negocioYY $
=YY% &
newYY' *
Concepto_negocioYY+ ;
(YY; <
)YY< =
{ZZ 
id[[ 
=[[ 
item[[ !
.[[! "

IdProducto[[" ,
,[[, -
nombre\\ 
=\\  
item\\! %
.\\% &
Descripcion\\& 1
,\\1 2
Detalle_maestro4]] (
=]]) *
new]]+ .
Detalle_maestro]]/ >
(]]> ?
)]]? @
{^^ 
valor__ !
=__" #
item__$ (
.__( )
EsBien__) /
?__0 1
$str__2 5
:__6 7
$str__8 ;
}`` 
}aa 
}bb 
)bb 
;bb 
}cc 
returndd 
detallesConstruidosdd &
;dd& '
}ee 	
}ff 
publichh 

classhh &
DetalleMovimientoDeAlmacenhh +
{ii 
publicjj 
intjj 

IdProductojj 
{jj 
getjj  #
;jj# $
setjj% (
;jj( )
}jj* +
publickk 
stringkk 
Descripcionkk !
{kk" #
getkk$ '
;kk' (
setkk) ,
;kk, -
}kk. /
publicll 
decimalll 
StockActualll "
{ll# $
getll% (
;ll( )
setll* -
;ll- .
}ll/ 0
publicmm 
decimalmm 
	Pendientemm  
{mm! "
getmm# &
;mm& '
setmm( +
;mm+ ,
}mm- .
publicnn 
decimalnn 
Ordenadonn 
{nn  !
getnn" %
;nn% &
setnn' *
;nn* +
}nn, -
publicoo 
decimaloo 
RecibidoEntregadooo (
{oo) *
getoo+ .
;oo. /
setoo0 3
;oo3 4
}oo5 6
publicpp 
decimalpp 
IngresoSalidaActualpp *
{pp+ ,
getpp- 0
;pp0 1
setpp2 5
;pp5 6
}pp7 8
publicqq 
stringqq 
Loteqq 
{qq 
getqq  
;qq  !
setqq" %
;qq% &
}qq' (
publicrr 
boolrr 
EsBienrr 
{rr 
getrr  
;rr  !
setrr" %
;rr% &
}rr' (
publictt &
DetalleMovimientoDeAlmacentt )
(tt) *
)tt* +
{uu 	
}vv 	
publicxx &
DetalleMovimientoDeAlmacenxx )
(xx) *
DetalleDeOperacionxx* <
detallexx= D
,xxD E
stringxxF L
lotexxM Q
,xxQ R
decimalxxS Z%
cantidadEntregadaRecibidaxx[ t
)xxt u
{yy 	

IdProductozz 
=zz 
detallezz  
.zz  !
Productozz! )
.zz) *
Idzz* ,
;zz, -
Descripcion{{ 
={{ 
detalle{{ !
.{{! "
Producto{{" *
.{{* +
NombreConcepto{{+ 9
;{{9 :
Ordenado|| 
=|| 
detalle|| 
.|| 
Cantidad|| '
;||' (
RecibidoEntregado}} 
=}} %
cantidadEntregadaRecibida}}  9
;}}9 :
IngresoSalidaActual~~ 
=~~  !
Ordenado~~" *
-~~+ ,
RecibidoEntregado~~- >
;~~> ?
Lote 
= 
lote 
; 
EsBien
�� 
=
�� 
detalle
�� 
.
�� 
Producto
�� %
.
��% &
EsBien
��& ,
;
��, -
}
�� 	
public
�� 
static
�� 
List
�� 
<
�� (
DetalleMovimientoDeAlmacen
�� 5
>
��5 6
	Convertir
��7 @
(
��@ A
List
��A E
<
��E F 
DetalleDeOperacion
��F X
>
��X Y
detalles
��Z b
,
��b c
List
��d h
<
��h i!
MovimientoDeAlmacen
��i |
>
��| }
ordenes��~ �
)��� �
{
�� 	
List
�� 
<
�� (
DetalleMovimientoDeAlmacen
�� +
>
��+ ,*
detallesTrasladoDeMercaderia
��- I
=
��J K
new
��L O
List
��P T
<
��T U(
DetalleMovimientoDeAlmacen
��U o
>
��o p
(
��p q
)
��q r
;
��r s
foreach
�� 
(
�� 
var
�� 
detalle
��  
in
��! #
detalles
��$ ,
)
��, -
{
�� 
var
�� 

idConcepto
�� 
=
��  
detalle
��! (
.
��( )
Producto
��) 1
.
��1 2
Id
��2 4
;
��4 5
decimal
�� '
cantidadEntregadaRecibida
�� 1
=
��2 3
$num
��4 5
;
��5 6
if
�� 
(
�� 
ordenes
�� 
!=
�� 
null
�� #
)
��# $
{
�� 
foreach
�� 
(
�� 
var
��  
orden
��! &
in
��' )
ordenes
��* 1
)
��1 2
{
�� '
cantidadEntregadaRecibida
�� 1
+=
��2 4
orden
��5 :
.
��: ; 
DetalleTransaccion
��; M
(
��M N
)
��N O
.
��O P
Single
��P V
(
��V W
dt
��W Y
=>
��Z \
dt
��] _
.
��_ `!
id_concepto_negocio
��` s
==
��t v

idConcepto��w �
)��� �
.��� �
cantidad��� �
;��� �
}
�� 
}
�� *
detallesTrasladoDeMercaderia
�� ,
.
��, -
Add
��- 0
(
��0 1
new
��1 4(
DetalleMovimientoDeAlmacen
��5 O
(
��O P
detalle
��P W
,
��W X
detalle
��Y `
.
��` a
Lote
��a e
,
��e f(
cantidadEntregadaRecibida��g �
)��� �
)��� �
;��� �
}
�� 
return
�� *
detallesTrasladoDeMercaderia
�� /
;
��/ 0
}
�� 	
}
�� 
public
�� 

class
��  
RegistroTransporte
�� #
{
�� 
public
�� 
ItemGenerico
�� 

�� )
{
��* +
get
��, /
;
��/ 0
set
��1 4
;
��4 5
}
��6 7
public
�� 
string
�� 
Placa
�� 
{
�� 
get
�� !
;
��! "
set
��# &
;
��& '
}
��( )
public
��  
RegistroTransporte
�� !
(
��! "
)
��" #
{
�� 	
}
�� 	
}
�� 
public
�� 

class
�� 
RegistroConductor
�� "
{
�� 
public
�� 
ItemGenerico
�� 
	Conductor
�� %
{
��& '
get
��( +
;
��+ ,
set
��- 0
;
��0 1
}
��2 3
public
�� 
string
�� 
NumeroLicencia
�� $
{
��% &
get
��' *
;
��* +
set
��, /
;
��/ 0
}
��1 2
public
�� 
RegistroConductor
��  
(
��  !
)
��! "
{
�� 	
}
�� 	
}
�� 
}�� �
dD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Almacen\AfectacionInventarioFisico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 &
AfectacionInventarioActual

 +
{ 
public 
List 
< 
Detalle_transaccion '
>' ( 
Detalles_modificados) =
{= >
get? B
;B C
setD G
;G H
}I J
public
List
<
Detalle_transaccion
>
Detalles_nuevos
{
get
;
set
;
}
} 
} �
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\ResultadoRegistroMovimientoCochera.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{		 
public

 

class

 .
"ResultadoRegistroMovimientoCochera

 3
{ 
public 
MovimientoCochera  

Movimiento! +
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public
OrdenDeVenta
OrdenDeVenta
{
get
;
set
;
}
} 
} �
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\ComprobanteDeNegocio_.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public 

class '
ComprobanteDeNegocioBasico_ ,
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
string 
Numero 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
string		 
Serie		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
int

 
IdTipo

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
public '
ComprobanteDeNegocioBasico_ *
(* +
)+ ,
{ 	
} 	
} 
public 

class !
ComprobanteDeNegocio_ &
{ 
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
int 
Numero 
{ 
get 
;  
set! $
;$ %
}& '
public 
int 
NumeroSerie 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
SerieComprobante_  
Serie! &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
ItemGenerico 
Tipo  
{! "
get# &
;& '
set( +
;+ ,
}- .
public   !
ComprobanteDeNegocio_   $
(  $ %
)  % &
{!! 	
}## 	
}%% 
}&& �@
QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\Empleado_.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
{ 
public		 

class		 
	Empleado_		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
IdActor 
{ 
get  
;  !
set" %
;% &
}' (
public 
ItemGenerico "
TipoDocumentoIdentidad 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
string $
NumeroDocumentoIdentidad .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
string 
ApellidoPaterno %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
ApellidoMaterno %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Nombres 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
NombresYApellidos '
{( )
get* -
{. /
return0 6
Nombres7 >
+? @
$strA D
+E F
ApellidoPaternoG V
+W X
$strY \
+] ^
ApellidoMaterno_ n
;n o
}p q
}r s
public 
string 
ApellidosYNombres '
{( )
get* -
{. /
return0 6
ApellidoPaterno7 F
+G H
$strI L
+M N
ApellidoMaternoO ^
+_ `
$stra d
+e f
Nombresg n
;n o
}p q
}r s
public 
IEnumerable 
< 

>( )
Roles* /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
string 
NombreCorto !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
	Empleado_ 
( 
) 
{ 	
} 	
public 
	Empleado_ 
( 

actorDeNegocio' 5
)5 6
{ 	
this   
.   
Id   
=   
actorDeNegocio   $
.  $ %
id  % '
;  ' (
this!! 
.!! 
IdActor!! 
=!! 
actorDeNegocio!! )
.!!) *
id_actor!!* 2
;!!2 3
this"" 
."" 
Nombres"" 
="" 
actorDeNegocio"" )
."") *
PrimerNombre""* 6
.""6 7
Split""7 <
(""< =
$char""= @
)""@ A
[""A B
$num""B C
]""C D
;""D E
;""F G
this## 
.## 
ApellidoPaterno##  
=##! "
actorDeNegocio### 1
.##1 2
PrimerNombre##2 >
.##> ?
Split##? D
(##D E
$char##E H
)##H I
[##I J
$num##J K
]##K L
;##L M
;##N O
this$$ 
.$$ 
ApellidoMaterno$$  
=$$! "
actorDeNegocio$$# 1
.$$1 2
PrimerNombre$$2 >
.$$> ?
Split$$? D
($$D E
$char$$E H
)$$H I
[$$I J
$num$$J K
]$$K L
;$$L M
;$$N O
this%% 
.%% $
NumeroDocumentoIdentidad%% )
=%%* +
actorDeNegocio%%, :
.%%: ;
DocumentoIdentidad%%; M
;%%M N
this&& 
.&& "
TipoDocumentoIdentidad&& '
=&&( )
new&&* -
ItemGenerico&&. :
(&&: ;
)&&; <
{&&= >
Id&&? A
=&&B C
actorDeNegocio&&D R
.&&R S 
IdDocumentoIdentidad&&S g
,&&g h
Nombre&&i o
=&&p q
actorDeNegocio	&&r �
.
&&� �
Actor
&&� �
.
&&� �
Detalle_maestro
&&� �
.
&&� �
nombre
&&� �
}
&&� �
;
&&� �
this'' 
.'' 
Roles'' 
='' 

.''& '
Convert_''' /
(''/ 0
actorDeNegocio''0 >
.''> ?
Actor''? D
.''D E

.''R S
Where''S X
(''X Y
an''Y [
=>''\ ^
an''_ a
.''a b
Rol''b e
.''e f
id_rol_padre''f r
==''s u

.
''� �
Default
''� �
.
''� �

''� �
&&
''� �
an
''� �
.
''� �

es_vigente
''� �
==
''� �
true
''� �
)
''� �
.
''� �
Select
''� �
(
''� �
an
''� �
=>
''� �
an
''� �
.
''� �
Rol
''� �
)
''� �
.
''� �
ToList
''� �
(
''� �
)
''� �
)
''� �
;
''� �
}(( 	
public)) 

Convert)) $
())$ %
)))% &
{** 	
return++ 
new++ 

{,, 
id-- 
=-- 
this-- 
.-- 
Id-- 
,-- 
Actor.. 
=.. 
new.. 
Actor.. !
(..! "
).." #
{// 

=00" #
this00$ (
.00( )
ApellidoPaterno00) 8
+009 :
$str00; >
+00? @
ApellidoMaterno00A P
+00Q R
$str00S V
+00W X
Nombres00Y `
,00` a
segundo_nombre11 "
=11# $
this11% )
.11) *
Nombres11* 1
+112 3
$str114 7
+118 9
this11: >
.11> ?
ApellidoPaterno11? N
+11O P
$str11Q T
+11U V
this11W [
.11[ \
ApellidoMaterno11\ k
,11k l

=22" #
this22$ (
.22( )
Nombres22) 0
+221 2
$str223 6
+227 8
this229 =
.22= >
ApellidoPaterno22> M
.22M N
	Substring22N W
(22W X
$num22X Y
,22Y Z
$num22[ \
)22\ ]
+22^ _
$str22` d
+22e f
this22g k
.22k l
ApellidoMaterno22l {
.22{ |
	Substring	22| �
(
22� �
$num
22� �
,
22� �
$num
22� �
)
22� �
+
22� �
$str
22� �
,
22� �
Detalle_maestro33 #
=33$ %
new33& )
Detalle_maestro33* 9
(339 :
)33: ;
{44 
id55 
=55 
this55 !
.55! ""
TipoDocumentoIdentidad55" 8
.558 9
Id559 ;
,55; <
nombre66 
=66  
this66! %
.66% &"
TipoDocumentoIdentidad66& <
.66< =
Nombre66= C
}77 
}88 
}99 
;99
}AA 	
publicCC 
boolCC 
TieneRolCC 
(CC 
intCC  
idRolCC! &
)CC& '
{DD 	
returnEE 
RolesEE 
.EE 
SelectEE 
(EE  
rEE  !
=>EE" $
rEE% &
.EE& '
IdEE' )
)EE) *
.EE* +
ContainsEE+ 3
(EE3 4
idRolEE4 9
)EE9 :
;EE: ;
}FF 	
}GG 
}HH ��
WD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Actor\ActorComercial_.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
Custom		 $
{

 
public 

class 
ActorComercial_  
{ 
public
int
Id
{
get
;
set
;
}
public 
int 
IdActor 
{ 
get  
;  !
set" %
;% &
}' (
public 
ItemGenerico "
TipoDocumentoIdentidad 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
ItemGenerico 
TipoPersona '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string $
NumeroDocumentoIdentidad .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
string 
NombreORazonSocial (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
ApellidoPaterno %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
ApellidoMaterno %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
Nombres 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
NombreComercial %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
NombreCorto !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Alias 
{ 
get !
;! "
set# &
;& '
}( )
public 
string (
CodigoTipoDocumentoIdentidad 2
{3 4
get5 8
{9 :
return; A
thisB F
.F G"
TipoDocumentoIdentidadG ]
.] ^
Valor^ c
;c d
}e f
}g h
public 
string -
!CodigoSunatTipoDocumentoIdentidad 7
{8 9
get: =
{> ?
return@ F
thisG K
.K L"
TipoDocumentoIdentidadL b
.b c
Codigoc i
;i j
}k l
}m n
public 
string 
Codigo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
bool 
	EsVigente 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Correo 
{ 
get "
;" #
set$ '
;' (
}) *
public 
DateTime 
FechaNacimiento '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string !
FechaNacimientoString +
{, -
get. 1
=>2 4
FechaNacimiento5 D
.D E
ToStringE M
(M N
$strN Z
)Z [
;[ \
}] ^
public   
string   #
InformacionPublicitaria   -
{  . /
get  0 3
;  3 4
set  5 8
;  8 9
}  : ;
public!! 
string!! 
Telefono!! 
{!!  
get!!! $
;!!$ %
set!!& )
;!!) *
}!!+ ,
public## 

Direccion_## 
DomicilioFiscal## )
{##* +
get##, /
;##/ 0
set##1 4
;##4 5
}##6 7
public$$ 
ItemGenerico$$ 

ClaseActor$$ &
{$$' (
get$$) ,
;$$, -
set$$. 1
;$$1 2
}$$3 4
public%% 
ItemGenerico%% 
EstadoLegal%% '
{%%( )
get%%* -
;%%- .
set%%/ 2
;%%2 3
}%%4 5
public&& 
ItemGenerico&& 
Nacionalidad&& (
{&&) *
get&&+ .
;&&. /
set&&0 3
;&&3 4
}&&5 6
public'' 
List'' 
<'' 
ItemGenerico''  
>''  !
Roles''" '
{''( )
get''* -
;''- .
set''/ 2
;''2 3
}''4 5
public(( 
bool(( 
SeleccionarGrupo(( $
{((% &
get((' *
;((* +
set((, /
;((/ 0
}((1 2
public)) 
bool)) 
NingunGrupo)) 
{))  !
get))" %
;))% &
set))' *
;))* +
})), -
public** 
ItemGenerico** 
Grupo** !
{**" #
get**$ '
;**' (
set**) ,
;**, -
}**. /
public-- 
ActorComercial_-- 
(-- 
)--  
{.. 	
}..
 
public11 
ActorComercial_11 
(11 

actorDeNegocio11- ;
)11; <
{22 	
this33 
.33 
Id33 
=33 
actorDeNegocio33 $
.33$ %
id33% '
;33' (
this44 
.44 
IdActor44 
=44 
actorDeNegocio44 )
.44) *
id_actor44* 2
;442 3
this55 
.55 
Codigo55 
=55 
actorDeNegocio55 (
.55( )
codigo_negocio55) 7
;557 8
this66 
.66 
Telefono66 
=66 
actorDeNegocio66 *
.66* +
Telefono66+ 3
;663 4
this77 
.77 
NombreComercial77  
=77! "
actorDeNegocio77# 1
.771 2

;77? @
this88 
.88 
NombreORazonSocial88 #
=88$ %
actorDeNegocio88& 4
.884 5
PrimerNombre885 A
.88A B
Replace88B I
(88I J
$str88J M
,88M N
$str88O R
)88R S
;88S T
this99 
.99 
ApellidoPaterno99  
=99! "
actorDeNegocio99# 1
.991 2
IdTipoActor992 =
==99> @

.99N O
Default99O V
.99V W%
IdTipoActorPersonaNatural99W p
?99q r
actorDeNegocio	99s �
.
99� �
PrimerNombre
99� �
.
99� �
Split
99� �
(
99� �
$char
99� �
)
99� �
[
99� �
$num
99� �
]
99� �
:
99� �
$str
99� �
;
99� �
this:: 
.:: 
ApellidoMaterno::  
=::! "
actorDeNegocio::# 1
.::1 2
IdTipoActor::2 =
==::> @

.::N O
Default::O V
.::V W%
IdTipoActorPersonaNatural::W p
&&::q s
actorDeNegocio	::t �
.
::� �
PrimerNombre
::� �
.
::� �
Split
::� �
(
::� �
$char
::� �
)
::� �
.
::� �
Count
::� �
(
::� �
)
::� �
>
::� �
$num
::� �
?
::� �
actorDeNegocio
::� �
.
::� �
PrimerNombre
::� �
.
::� �
Split
::� �
(
::� �
$char
::� �
)
::� �
[
::� �
$num
::� �
]
::� �
:
::� �
$str
::� �
;
::� �
this;; 
.;; 
Nombres;; 
=;; 
actorDeNegocio;; )
.;;) *
IdTipoActor;;* 5
==;;6 8

.;;F G
Default;;G N
.;;N O%
IdTipoActorPersonaNatural;;O h
&&;;i k
actorDeNegocio;;l z
.;;z {
PrimerNombre	;;{ �
.
;;� �
Split
;;� �
(
;;� �
$char
;;� �
)
;;� �
.
;;� �
Count
;;� �
(
;;� �
)
;;� �
>
;;� �
$num
;;� �
?
;;� �
actorDeNegocio
;;� �
.
;;� �
PrimerNombre
;;� �
.
;;� �
Split
;;� �
(
;;� �
$char
;;� �
)
;;� �
[
;;� �
$num
;;� �
]
;;� �
:
;;� �
$str
;;� �
;
;;� �
this<< 
.<< $
NumeroDocumentoIdentidad<< )
=<<* +
actorDeNegocio<<, :
.<<: ;
DocumentoIdentidad<<; M
;<<M N
this== 
.== "
TipoDocumentoIdentidad== '
===( )
new==* -
ItemGenerico==. :
(==: ;
)==; <
{=== >
Id==? A
===B C
actorDeNegocio==D R
.==R S 
IdDocumentoIdentidad==S g
,==g h
Nombre==i o
===p q
actorDeNegocio	==r �
.
==� �
Actor
==� �
.
==� �
Detalle_maestro
==� �
.
==� �
valor
==� �
,
==� �
Valor
==� �
=
==� �
actorDeNegocio
==� �
.
==� �
Actor
==� �
.
==� �
Detalle_maestro
==� �
.
==� �
valor
==� �
}
==� �
;
==� �
this>> 
.>> 
TipoPersona>> 
=>> 
new>> "
ItemGenerico>># /
(>>/ 0
)>>0 1
{>>2 3
Id>>4 6
=>>7 8
actorDeNegocio>>9 G
.>>G H
Actor>>H M
.>>M N

,>>[ \
Nombre>>] c
=>>d e
actorDeNegocio>>f t
.>>t u
Actor>>u z
.>>z {

Tipo_actor	>>{ �
.
>>� �
nombre
>>� �
}
>>� �
;
>>� �
this?? 
.?? 

ClaseActor?? 
=?? 
new?? !
ItemGenerico??" .
(??. /
)??/ 0
{??1 2
Id??3 5
=??6 7
actorDeNegocio??8 F
.??F G
Actor??G L
.??L M
id_clase_actor??M [
,??[ \
Nombre??] c
=??d e
actorDeNegocio??f t
.??t u
Actor??u z
.??z {
Clase_actor	??{ �
.
??� �
nombre
??� �
}
??� �
;
??� �
this@@ 
.@@ 
EstadoLegal@@ 
=@@ 
new@@ "
ItemGenerico@@# /
(@@/ 0
)@@0 1
{@@2 3
Id@@4 6
=@@7 8
actorDeNegocio@@9 G
.@@G H
Actor@@H M
.@@M N
id_estado_legal@@N ]
,@@^ _
Nombre@@` f
=@@g h
actorDeNegocio@@i w
.@@w x
Actor@@x }
.@@} ~
Estado_legal	@@~ �
.
@@� �
nombre
@@� �
}
@@� �
;
@@� �
thisAA 
.AA 
FechaNacimientoAA  
=AA! "
actorDeNegocioAA# 1
.AA1 2
FechaNacimientoAA2 A
;AAA B
thisBB 
.BB 
NacionalidadBB 
=BB 
newBB  #
ItemGenericoBB$ 0
(BB0 1
)BB1 2
{BB3 4
IdBB5 7
=BB8 9
actorDeNegocioBB: H
.BBH I
ActorBBI N
.BBN O
Detalle_maestro1BBO _
.BB_ `
idBB` b
,BBb c
NombreBBd j
=BBk l
actorDeNegocioBBm {
.BB{ |
Actor	BB| �
.
BB� �
Detalle_maestro1
BB� �
.
BB� �
nombre
BB� �
}
BB� �
;
BB� �
thisCC 
.CC 
DomicilioFiscalCC  
=CC! "
newCC# &

Direccion_CC' 1
(CC1 2
)CC2 3
{CC4 5
IdCC6 8
=CC9 :
actorDeNegocioCC; I
.CCI J
ActorCCJ O
.CCO P
DireccionPrincipalCCP b
.CCb c
idCCc e
,CCe f
PaisCCg k
=CCl m
newCCn q
ItemGenericoCCr ~
(CC~ 
)	CC �
{
CC� �
Id
CC� �
=
CC� �
actorDeNegocio
CC� �
.
CC� �
Actor
CC� �
.
CC� �
	Direccion
CC� �
.
CC� �
FirstOrDefault
CC� �
(
CC� �
)
CC� �
.
CC� �
Detalle_maestro1
CC� �
.
CC� �
id
CC� �
,
CC� �
Nombre
CC� �
=
CC� �
actorDeNegocio
CC� �
.
CC� �
Actor
CC� �
.
CC� �
	Direccion
CC� �
.
CC� �
FirstOrDefault
CC� �
(
CC� �
)
CC� �
.
CC� �
Detalle_maestro1
CC� �
.
CC� �
nombre
CC� �
}
CC� �
,
CC� �
Ubigeo
CC� �
=
CC� �
new
CC� �
ItemGenerico
CC� �
(
CC� �
actorDeNegocio
CC� �
.
CC� �
Actor
CC� �
.
CC� � 
DireccionPrincipal
CC� �
.
CC� �
Ubigeo
CC� �
.
CC� �
id
CC� �
,
CC� �
actorDeNegocio
CC� �
.
CC� �
Actor
CC� �
.
CC� � 
DireccionPrincipal
CC� �
.
CC� �
Ubigeo
CC� �
.
CC� �
descripcion_larga
CC� �
)
CC� �
,
CC� �
Detalle
CC� �
=
CC� �
actorDeNegocio
CC� �
.
CC� �
Actor
CC� �
.
CC� � 
DireccionPrincipal
CC� �
.
CC� �
detalle
CC� �
}
CC� �
;
CC� �
}DD 	
publicFF 
ActorComercial_FF 
(FF 
intFF "
idRolFF# (
,FF( )

actorDeNegocioFF8 F
)FFF G
{GG 	
thisHH 
.HH 
IdHH 
=HH 
actorDeNegocioHH $
.HH$ %
idHH% '
;HH' (
thisII 
.II 
IdActorII 
=II 
actorDeNegocioII )
.II) *
id_actorII* 2
;II2 3
thisJJ 
.JJ 
CodigoJJ 
=JJ 
actorDeNegocioJJ (
.JJ( )
codigo_negocioJJ) 7
;JJ7 8
thisKK 
.KK 
TelefonoKK 
=KK 
actorDeNegocioKK *
.KK* +
TelefonoKK+ 3
;KK3 4
thisLL 
.LL 
NombreComercialLL  
=LL! "
actorDeNegocioLL# 1
.LL1 2

;LL? @
thisMM 
.MM 
NombreORazonSocialMM #
=MM$ %
actorDeNegocioMM& 4
.MM4 5
PrimerNombreMM5 A
.MMA B
ReplaceMMB I
(MMI J
$strMMJ M
,MMM N
$strMMO R
)MMR S
;MMS T
thisNN 
.NN 
ApellidoPaternoNN  
=NN! "
actorDeNegocioNN# 1
.NN1 2
IdTipoActorNN2 =
==NN> @

.NNN O
DefaultNNO V
.NNV W%
IdTipoActorPersonaNaturalNNW p
?NNq r
actorDeNegocio	NNs �
.
NN� �
PrimerNombre
NN� �
.
NN� �
Split
NN� �
(
NN� �
$char
NN� �
)
NN� �
[
NN� �
$num
NN� �
]
NN� �
:
NN� �
$str
NN� �
;
NN� �
thisOO 
.OO 
ApellidoMaternoOO  
=OO! "
actorDeNegocioOO# 1
.OO1 2
IdTipoActorOO2 =
==OO> @

.OON O
DefaultOOO V
.OOV W%
IdTipoActorPersonaNaturalOOW p
&&OOq s
actorDeNegocio	OOt �
.
OO� �
PrimerNombre
OO� �
.
OO� �
Split
OO� �
(
OO� �
$char
OO� �
)
OO� �
.
OO� �
Count
OO� �
(
OO� �
)
OO� �
>
OO� �
$num
OO� �
?
OO� �
actorDeNegocio
OO� �
.
OO� �
PrimerNombre
OO� �
.
OO� �
Split
OO� �
(
OO� �
$char
OO� �
)
OO� �
[
OO� �
$num
OO� �
]
OO� �
:
OO� �
$str
OO� �
;
OO� �
thisPP 
.PP 
NombresPP 
=PP 
actorDeNegocioPP )
.PP) *
IdTipoActorPP* 5
==PP6 8

.PPF G
DefaultPPG N
.PPN O%
IdTipoActorPersonaNaturalPPO h
&&PPi k
actorDeNegocioPPl z
.PPz {
PrimerNombre	PP{ �
.
PP� �
Split
PP� �
(
PP� �
$char
PP� �
)
PP� �
.
PP� �
Count
PP� �
(
PP� �
)
PP� �
>
PP� �
$num
PP� �
?
PP� �
actorDeNegocio
PP� �
.
PP� �
PrimerNombre
PP� �
.
PP� �
Split
PP� �
(
PP� �
$char
PP� �
)
PP� �
[
PP� �
$num
PP� �
]
PP� �
:
PP� �
$str
PP� �
;
PP� �
thisQQ 
.QQ $
NumeroDocumentoIdentidadQQ )
=QQ* +
actorDeNegocioQQ, :
.QQ: ;
DocumentoIdentidadQQ; M
;QQM N
thisRR 
.RR "
TipoDocumentoIdentidadRR '
=RR( )
newRR* -
ItemGenericoRR. :
(RR: ;
)RR; <
{RR= >
IdRR? A
=RRB C
actorDeNegocioRRD R
.RRR S 
IdDocumentoIdentidadRRS g
,RRg h
NombreRRi o
=RRp q
actorDeNegocio	RRr �
.
RR� �
Actor
RR� �
.
RR� �
Detalle_maestro
RR� �
.
RR� �
valor
RR� �
,
RR� �
Valor
RR� �
=
RR� �
actorDeNegocio
RR� �
.
RR� �
Actor
RR� �
.
RR� �
Detalle_maestro
RR� �
.
RR� �
valor
RR� �
}
RR� �
;
RR� �
thisSS 
.SS 
TipoPersonaSS 
=SS 
newSS "
ItemGenericoSS# /
(SS/ 0
)SS0 1
{SS2 3
IdSS4 6
=SS7 8
actorDeNegocioSS9 G
.SSG H
ActorSSH M
.SSM N

,SS[ \
NombreSS] c
=SSd e
actorDeNegocioSSf t
.SSt u
ActorSSu z
.SSz {

Tipo_actor	SS{ �
.
SS� �
nombre
SS� �
}
SS� �
;
SS� �
thisTT 
.TT 

ClaseActorTT 
=TT 
newTT !
ItemGenericoTT" .
(TT. /
)TT/ 0
{TT1 2
IdTT3 5
=TT6 7
actorDeNegocioTT8 F
.TTF G
ActorTTG L
.TTL M
id_clase_actorTTM [
,TT[ \
NombreTT] c
=TTd e
actorDeNegocioTTf t
.TTt u
ActorTTu z
.TTz {
Clase_actor	TT{ �
.
TT� �
nombre
TT� �
}
TT� �
;
TT� �
thisUU 
.UU 
EstadoLegalUU 
=UU 
newUU "
ItemGenericoUU# /
(UU/ 0
)UU0 1
{UU2 3
IdUU4 6
=UU7 8
actorDeNegocioUU9 G
.UUG H
ActorUUH M
.UUM N
id_estado_legalUUN ]
,UU] ^
NombreUU_ e
=UUf g
actorDeNegocioUUh v
.UUv w
ActorUUw |
.UU| }
Estado_legal	UU} �
.
UU� �
nombre
UU� �
}
UU� �
;
UU� �
thisVV 
.VV 
FechaNacimientoVV  
=VV! "
actorDeNegocioVV# 1
.VV1 2
FechaNacimientoVV2 A
;VVA B
thisWW 
.WW 
NacionalidadWW 
=WW 
newWW  #
ItemGenericoWW$ 0
(WW0 1
)WW1 2
{WW3 4
IdWW5 7
=WW8 9
actorDeNegocioWW: H
.WWH I
ActorWWI N
.WWN O
Detalle_maestro1WWO _
.WW_ `
idWW` b
,WWb c
NombreWWd j
=WWk l
actorDeNegocioWWm {
.WW{ |
Actor	WW| �
.
WW� �
Detalle_maestro1
WW� �
.
WW� �
nombre
WW� �
}
WW� �
;
WW� �
thisXX 
.XX 
DomicilioFiscalXX  
=XX! "
newXX# &

Direccion_XX' 1
(XX1 2
)XX2 3
{XX4 5
IdXX6 8
=XX9 :
actorDeNegocioXX; I
.XXI J
ActorXXJ O
.XXO P
DireccionPrincipalXXP b
.XXb c
idXXc e
,XXe f
PaisXXg k
=XXl m
newXXn q
ItemGenericoXXr ~
(XX~ 
)	XX �
{
XX� �
Id
XX� �
=
XX� �
actorDeNegocio
XX� �
.
XX� �
Actor
XX� �
.
XX� �
	Direccion
XX� �
.
XX� �
FirstOrDefault
XX� �
(
XX� �
)
XX� �
.
XX� �
Detalle_maestro1
XX� �
.
XX� �
id
XX� �
,
XX� �
Nombre
XX� �
=
XX� �
actorDeNegocio
XX� �
.
XX� �
Actor
XX� �
.
XX� �
	Direccion
XX� �
.
XX� �
FirstOrDefault
XX� �
(
XX� �
)
XX� �
.
XX� �
Detalle_maestro1
XX� �
.
XX� �
nombre
XX� �
}
XX� �
,
XX� �
Ubigeo
XX� �
=
XX� �
new
XX� �
ItemGenerico
XX� �
(
XX� �
actorDeNegocio
XX� �
.
XX� �
Actor
XX� �
.
XX� � 
DireccionPrincipal
XX� �
.
XX� �
Ubigeo
XX� �
.
XX� �
id
XX� �
,
XX� �
actorDeNegocio
XX� �
.
XX� �
Actor
XX� �
.
XX� � 
DireccionPrincipal
XX� �
.
XX� �
Ubigeo
XX� �
.
XX� �
descripcion_larga
XX� �
)
XX� �
,
XX� �
Detalle
XX� �
=
XX� �
actorDeNegocio
XX� �
.
XX� �
Actor
XX� �
.
XX� � 
DireccionPrincipal
XX� �
.
XX� �
detalle
XX� �
}
XX� �
;
XX� �
ifYY 
(YY 
idRolYY 
==YY 

.YY% &
DefaultYY& -
.YY- .

)YY; <
{ZZ 
this[[ 
.[[ 
Roles[[ 
=[[ 
new[[  
List[[! %
<[[% &
ItemGenerico[[& 2
>[[2 3
([[3 4
)[[4 5
;[[5 6
foreach\\ 
(\\ 
var\\ 
rol\\  
in\\! #
actorDeNegocio\\$ 2
.\\2 3
Actor\\3 8
.\\8 9

.\\F G
Where\\G L
(\\L M
an\\M O
=>\\P R
an\\S U
.\\U V
Rol\\V Y
.\\Y Z
id_rol_padre\\Z f
==\\g i

.\\w x
Default\\x 
.	\\ �

\\� �
&&
\\� �
an
\\� �
.
\\� �

es_vigente
\\� �
==
\\� �
true
\\� �
)
\\� �
.
\\� �
Select
\\� �
(
\\� �
an
\\� �
=>
\\� �
an
\\� �
.
\\� �
Rol
\\� �
)
\\� �
.
\\� �
ToList
\\� �
(
\\� �
)
\\� �
)
\\� �
{]] 
this^^ 
.^^ 
Roles^^ 
.^^ 
Add^^ "
(^^" #
new^^# &
ItemGenerico^^' 3
(^^3 4
rol^^4 7
.^^7 8
id^^8 :
,^^: ;
rol^^< ?
.^^? @
nombre^^@ F
)^^F G
)^^G H
;^^H I
}__ 
}aa 
}bb 	
publicdd 
ActorComercial_dd 
(dd 
Actordd $
actordd% *
)dd* +
{ee 	
thisff 
.ff 
IdActorff 
=ff 
actorff  
.ff  !
idff! #
;ff# $
thisgg 
.gg 
Telefonogg 
=gg 
actorgg !
.gg! "
telefonogg" *
;gg* +
thishh 
.hh 
NombreComercialhh  
=hh! "
actorhh# (
.hh( )
segundo_nombrehh) 7
;hh7 8
thisii 
.ii 
NombreORazonSocialii #
=ii$ %
actorii& +
.ii+ ,

.ii9 :
Replaceii: A
(iiA B
$striiB E
,iiE F
$striiG J
)iiJ K
;iiK L
thisjj 
.jj 
ApellidoPaternojj  
=jj! "
actorjj# (
.jj( )

==jj7 9

.jjG H
DefaultjjH O
.jjO P%
IdTipoActorPersonaNaturaljjP i
?jjj k
actorjjl q
.jjq r

.	jj �
Split
jj� �
(
jj� �
$char
jj� �
)
jj� �
[
jj� �
$num
jj� �
]
jj� �
:
jj� �
$str
jj� �
;
jj� �
thiskk 
.kk 
ApellidoMaternokk  
=kk! "
actorkk# (
.kk( )

==kk7 9

.kkG H
DefaultkkH O
.kkO P%
IdTipoActorPersonaNaturalkkP i
&&kkj l
actorkkm r
.kkr s

.
kk� �
Split
kk� �
(
kk� �
$char
kk� �
)
kk� �
.
kk� �
Count
kk� �
(
kk� �
)
kk� �
>
kk� �
$num
kk� �
?
kk� �
actor
kk� �
.
kk� �

kk� �
.
kk� �
Split
kk� �
(
kk� �
$char
kk� �
)
kk� �
[
kk� �
$num
kk� �
]
kk� �
:
kk� �
$str
kk� �
;
kk� �
thisll 
.ll 
Nombresll 
=ll 
actorll  
.ll  !

==ll/ 1

.ll? @
Defaultll@ G
.llG H%
IdTipoActorPersonaNaturalllH a
&&llb d
actorlle j
.llj k

.llx y
Splitlly ~
(ll~ 
$char	ll �
)
ll� �
.
ll� �
Count
ll� �
(
ll� �
)
ll� �
>
ll� �
$num
ll� �
?
ll� �
actor
ll� �
.
ll� �

ll� �
.
ll� �
Split
ll� �
(
ll� �
$char
ll� �
)
ll� �
[
ll� �
$num
ll� �
]
ll� �
:
ll� �
$str
ll� �
;
ll� �
thismm 
.mm $
NumeroDocumentoIdentidadmm )
=mm* +
actormm, 1
.mm1 2&
numero_documento_identidadmm2 L
;mmL M
thisnn 
.nn "
TipoDocumentoIdentidadnn '
=nn( )
newnn* -
ItemGenericonn. :
(nn: ;
)nn; <
{nn= >
Idnn? A
=nnB C
actornnD I
.nnI J"
id_documento_identidadnnJ `
,nn` a
Nombrennb h
=nni j
actornnk p
.nnp q
Detalle_maestro	nnq �
.
nn� �
valor
nn� �
}
nn� �
;
nn� �
thisoo 
.oo 
TipoPersonaoo 
=oo 
newoo "
ItemGenericooo# /
(oo/ 0
)oo0 1
{oo2 3
Idoo4 6
=oo7 8
actoroo9 >
.oo> ?

,ooL M
NombreooN T
=ooU V
actorooW \
.oo\ ]

Tipo_actoroo] g
.oog h
nombreooh n
}ooo p
;oop q
thispp 
.pp 

ClaseActorpp 
=pp 
newpp !
ItemGenericopp" .
(pp. /
)pp/ 0
{pp1 2
Idpp3 5
=pp6 7
actorpp8 =
.pp= >
id_clase_actorpp> L
,ppL M
NombreppN T
=ppU V
actorppW \
.pp\ ]
Clase_actorpp] h
.pph i
nombreppi o
}ppp q
;ppq r
thisqq 
.qq 
EstadoLegalqq 
=qq 
newqq "
ItemGenericoqq# /
(qq/ 0
)qq0 1
{qq2 3
Idqq4 6
=qq7 8
actorqq9 >
.qq> ?
id_estado_legalqq? N
,qqN O
NombreqqP V
=qqW X
actorqqY ^
.qq^ _
Estado_legalqq_ k
.qqk l
nombreqql r
}qqs t
;qqt u
thisrr 
.rr 
FechaNacimientorr  
=rr! "
actorrr# (
.rr( )
fecha_nacimientorr) 9
;rr9 :
thisss 
.ss 
Nacionalidadss 
=ss 
newss  #
ItemGenericoss$ 0
(ss0 1
)ss1 2
{ss3 4
Idss5 7
=ss8 9
actorss: ?
.ss? @
Detalle_maestro1ss@ P
.ssP Q
idssQ S
,ssS T
NombressU [
=ss\ ]
actorss^ c
.ssc d
Detalle_maestro1ssd t
.sst u
nombressu {
}ss| }
;ss} ~
thistt 
.tt 
DomicilioFiscaltt  
=tt! "
newtt# &

Direccion_tt' 1
(tt1 2
)tt2 3
{tt4 5
Idtt6 8
=tt9 :
actortt; @
.tt@ A
DireccionPrincipalttA S
.ttS T
idttT V
,ttV W
PaisttX \
=tt] ^
newtt_ b
ItemGenericottc o
(tto p
)ttp q
{ttr s
Idttt v
=ttw x
actortty ~
.tt~ 
	Direccion	tt �
.
tt� �
FirstOrDefault
tt� �
(
tt� �
)
tt� �
.
tt� �
Detalle_maestro1
tt� �
.
tt� �
id
tt� �
,
tt� �
Nombre
tt� �
=
tt� �
actor
tt� �
.
tt� �
	Direccion
tt� �
.
tt� �
FirstOrDefault
tt� �
(
tt� �
)
tt� �
.
tt� �
Detalle_maestro1
tt� �
.
tt� �
nombre
tt� �
}
tt� �
,
tt� �
Ubigeo
tt� �
=
tt� �
new
tt� �
ItemGenerico
tt� �
(
tt� �
actor
tt� �
.
tt� � 
DireccionPrincipal
tt� �
.
tt� �
Ubigeo
tt� �
.
tt� �
id
tt� �
,
tt� �
actor
tt� �
.
tt� � 
DireccionPrincipal
tt� �
.
tt� �
Ubigeo
tt� �
.
tt� �
descripcion_larga
tt� �
)
tt� �
,
tt� �
Detalle
tt� �
=
tt� �
actor
tt� �
.
tt� � 
DireccionPrincipal
tt� �
.
tt� �
detalle
tt� �
}
tt� �
;
tt� �
}uu 	
publicvv 

Convertvv $
(vv$ %
)vv% &
{ww 	
returnxx 
newxx 

(xx$ %
)xx% &
{yy 
idzz 
=zz 
thiszz 
.zz 
Idzz 
,zz 
Actor{{ 
={{ 
new{{ 
Actor{{ !
({{! "
){{" #
{|| 

=}}" #
this}}$ (
.}}( )
NombreORazonSocial}}) ;
,}}; <
segundo_nombre~~ "
=~~# $
this~~% )
.~~) *
NombreComercial~~* 9
,~~9 :
Detalle_maestro #
=$ %
new& )
Detalle_maestro* 9
(9 :
): ;
{
�� 
nombre
�� 
=
��  
this
��! %
.
��% &$
TipoDocumentoIdentidad
��& <
.
��< =
Nombre
��= C
,
��C D
codigo
�� 
=
��  
this
��! %
.
��% &/
!CodigoSunatTipoDocumentoIdentidad
��& G
,
��G H
valor
�� 
=
�� 
this
��  $
.
��$ %*
CodigoTipoDocumentoIdentidad
��% A
}
�� 
,
�� (
numero_documento_identidad
�� .
=
��/ 0&
NumeroDocumentoIdentidad
��1 I
,
��I J
	Direccion
�� 
=
�� 
new
��  #
List
��$ (
<
��( )
	Direccion
��) 2
>
��2 3
(
��3 4
)
��4 5
{
�� 
(
�� 
VentasSettings
�� (
.
��( )
Default
��) 0
.
��0 15
'InformacionAMostrarEnDireccionDeCliente
��1 X
==
��Y [
(
��\ ]
int
��] `
)
��` a+
InformacionDireccionEnCliente
��a ~
.
��~ 
SoloDetalle�� �
)��� �
?��� �
DomicilioFiscal��� �
.��� �
Convert��� �
(��� �
)��� �
:��� �
DomicilioFiscal��� �
.��� �"
ConvertirConUbigeo��� �
(��� �
)��� �
}
�� 
}
�� 
}
�� 
;
��
}
�� 	
}
�� 
public
�� 

class
�� $
RegistroActorComercial
�� '
:
��( )
ActorComercial_
��* 9
{
�� 
public
�� 
ItemGenerico
�� '
ComprobantePredeterminado
�� 5
{
��6 7
get
��8 ;
;
��; <
set
��= @
;
��@ A
}
��B C
public
�� $
RegistroActorComercial
�� %
(
��% &
)
��& '
:
��( )
base
��* .
(
��. /
)
��/ 0
{
�� 	
}
�� 	
}
�� 
public
�� 

class
�� $
SelectorActorComercial
�� '
{
�� 
private
�� 
string
�� 
numeroDocumento
�� &
;
��& '
private
�� 
string
�� 
razonSocial
�� "
;
��" #
public
�� 
int
�� 
Id
�� 
{
�� 
get
�� 
;
�� 
set
��  
;
��  !
}
��" #
public
�� 
string
�� 
NumeroDocumento
�� %
{
�� 	
set
�� 
{
�� 
numeroDocumento
�� 
=
��  !
value
��" '
;
��' (
}
�� 
}
�� 	
public
�� 
string
�� 
RazonSocial
�� !
{
�� 	
get
�� 
{
�� 
return
�� 
numeroDocumento
�� &
+
��' (
$str
��) .
+
��/ 0
razonSocial
��1 <
.
��< =
Replace
��= D
(
��D E
$str
��E H
,
��H I
$str
��I L
)
��L M
;
��M N
}
�� 
set
�� 
{
�� 
razonSocial
�� 
=
�� 
value
�� #
;
��# $
}
�� 
}
�� 	
public
�� $
SelectorActorComercial
�� %
(
��% &
)
��& '
{
�� 	
}
�� 	
}
�� 
}�� �
UD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Comprobante.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public		 

class		 
Comprobante		 
{

 
public 
int 
id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
tipo 
{ 
get  
;  !
set" %
;% &
}' (
public
string
serie
{
get
;
set
;
}
public 
string 
numero 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} �	
kD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Comprobantes\TicketIngresoCochera.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
.1 2
Comprobantes2 >
{ 
public		 

class		  
TicketIngresoCochera		 %
{

 
public 
string 
NombreCompleto $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
Fecha 
{ 
get !
;! "
set# &
;& '
}( )
public
string
Hora
{
get
;
set
;
}
public 
string 
Usuario 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Tarifa 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} �
jD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Comprobantes\TicketSalidaCochera.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
.1 2
Comprobantes2 >
{ 
public		 

class		 
TicketSalidaCochera		 $
{

 
} 
} �	
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\DetallesACobrar.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public		 

class		 
DetallesACobrar		  
{

 
public 
decimal 
	Principal  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
decimal 
Exceso 
{ 
get  #
;# $
set% (
;( )
}* +
public
decimal
Ticket
{
get
;
set
;
}
public 
decimal 
Total 
{ 
get "
{# $
return% +
(, -
	Principal- 6
+7 8
Exceso9 ?
+@ A
TicketB H
)H I
;I J
}K L
}M N
} 
} �
^D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\TicketIngresoCochera.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public		 

class		  
TicketIngresoCochera		 %
:		% &
ComprobanteImpreso		' 9
{

 
public 
byte 
[ 
] 
CodigoBarra !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
CodigoBarraSrc $
{% &
get' *
{+ ,
return- 3
(4 5
$str5 N
+O P
ConvertQ X
.X Y
ToBase64StringY g
(g h
CodigoBarrah s
,s t
$numu v
,v w
CodigoBarra	x �
.
� �
Length
� �
)
� �
)
� �
;
� �
}
� �
}
� �
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: K
;K L
public 
Ingreso 
Ingreso 
{  
get! $
;$ %
set& )
;) *
}+ ,
public  
TicketIngresoCochera #
(# $4
(EstablecimientoComercialExtendidoConLogo$ L
sedeM Q
,Q R4
(EstablecimientoComercialExtendidoConLogoS {%
establecimientoOperacion	| �
,
� �
Ingreso
� �
ingreso
� �
,
� �
string
� �
serieTicket
� �
,
� �
int
� �
numeroTicket
� �
,
� �
byte
� �
[
� �
]
� �
codigoBarras
� �
)
� �
:
� �
base
� �
(
� �
)
� �
{ 	
this 
. 
Serie 
= 
serieTicket #
;# $
this 
. 
Numero 
= 
numeroTicket %
;% &
this 
. 
EsInvalidada 
= 
!  !
ingreso! (
.( )
EsValido) 1
;1 2
this 
. 
CodigoBarra 
= 
codigoBarras +
;+ ,
this 
. 
Emisor 
= 
new 
Emisor $
($ %
sede% )
,) *$
establecimientoOperacion+ C
)C D
;D E
this 
. 
FechaEmision 
= 
ingreso  '
.' (
	FechaHora( 1
;1 2
this 
. 
IdEstadoActual 
=  !
MaestroSettings" 1
.1 2
Default2 9
.9 :+
IdDetalleMaestroEstadoIngresado: Y
;Y Z
this 
. 
Ingreso 
= 
ingreso "
;" #
} 	
} 
}   �#
QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Ingreso.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public		 

class		 
Ingreso		 
{

 
public 
Vehiculo 
Vehiculo  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
ActorComercial_ 
Cliente &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public
DateTime
	FechaHora
{
get
;
set
;
}
public 
String 
Fecha 
{ 
get !
;! "
set# &
;& '
}( )
public 
String 
Hora 
{ 
get  
;  !
set" %
;% &
}' (
public 
bool 
EsValido 
{ 
get "
;" #
set$ '
;' (
}) *
public 
ItemGenerico 

{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
Transaccion 
Convert "
(" #"
UserProfileSessionData# 9
userProfile: E
,E F
DateTimeG O
fechaP U
)U V
{ 	
Transaccion 
transaccion #
=$ %
new& )
Transaccion* 5
(5 6
)6 7
{ 
id_actor_negocio_interno (
=) *
userProfile* 5
.5 6*
IdCentroDeAtencionSeleccionado6 T
,T U
id_empleado 
= 
userProfile )
.) *
Empleado* 2
.2 3
Id3 5
,5 6
	id_moneda 
= 
userProfile '
.' (
IdMonedaPorDefecto( :
(: ;
); <
,< =
id_unidad_negocio !
=" #
userProfile$ /
./ 0'
IdUnidadDeNegocioPorDefecto0 K
(K L
)L M
,M N"
fecha_registro_sistema &
=' (
fecha) .
,. /
	fecha_fin 
= 
fecha !
,! "#
fecha_registro_contable   '
=  ( )
fecha  * /
,  / 0
tipo_cambio!! 
=!! 
userProfile!! )
.!!) *
TipoDeCambio!!* 6
.!!6 7

ValorVenta!!7 A
,!!A B 
id_transaccion_padre"" $
=""% &
userProfile""' 2
.""2 3&
OperacionSesionContenedora""3 M
.""M N
Id""N P
,""P Q$
id_actor_negocio_externo%% (
=%%) *
this%%+ /
.%%/ 0
Cliente%%0 7
.%%7 8
Id%%8 :
,%%: ;%
id_actor_negocio_externo1&& )
=&&* +
this&&, 0
.&&0 1
Vehiculo&&1 9
.&&9 :
Id&&: <
,&&< =

comentario'' 
='' 
this'' !
.''! "
Observacion''" -
,''- .
enum1(( 
=(( 
this(( 
.(( 

.((* +
Id((+ -
,((- .
es_concreta)) 
=)) 
true)) "
,))" #
fecha_inicio** 
=** 
fecha** $
,**$ %

indicador1++ 
=++ 
false++ "
,++" #

indicador2,, 
=,, 
false,, "
,,," #
codigo-- 
=-- 
$str-- 
,-- 
id_tipo_transaccion.. #
=..$ %
CocheraSettings..& 5
...5 6
Default..6 =
...= >0
$IdTipoTransaccionMovimientoDeCochera..> b
,..b c

=// 
$num//  !
,//! "
}00 
;00
return11 
transaccion11 
;11 
}22 	
}33 
}44 �&
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\MovimientoCocheraBasico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public 

class #
MovimientoCocheraBasico (
{		 
public

 
long

 
Id

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 
long 
IdOrdenDeVenta "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int 
	IdCochera 
{ 
get "
;" #
set$ '
;' (
}) *
public
Vehiculo
Vehiculo
{
get
;
set
;
}
public 
ActorComercial_ 
Cliente &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DateTime 
Ingreso 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
Salida 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
EsValido 
{ 
get "
;" #
set$ '
;' (
}) *
public '
ComprobanteDeNegocioBasico_ *
Comprobante+ 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
ItemGenerico 
Estado "
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Observacion !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
ItemGenerico 

{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
TurnoCochera 
Turno !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 

{$ %
get& )
{* +
return, 2
Ingreso3 :
.: ;
ToString; C
(C D
$strD X
)X Y
;Y Z
}[ \
}] ^
public 
string 
SalidaString "
{# $
get% (
{) *
return+ 1
Salida2 8
.8 9
ToString9 A
(A B
$strB V
)V W
;W X
}Y Z
}[ \
public 
bool 
PuedeVer 
{ 
get "
{# $
return$ *
true+ /
;/ 0
}1 2
}3 4
public 
bool 
PuedeEditar 
{  !
get" %
{& '
return( .
this/ 3
.3 4
Estado4 :
!=: <
null< @
&&@ B
thisB F
.F G
EstadoG M
.M N
IdN P
==P R
MaestroSettingsS b
.b c
Defaultc j
.j k,
IdDetalleMaestroEstadoIngresado	k �
;
� �
}
� �
}
� �
public 
bool 
PuedeInvalidar "
{# $
get% (
{) *
return+ 1
this2 6
.6 7
Estado7 =
!=> @
nullA E
&&F H
thisI M
.M N
EstadoN T
.T U
IdU W
!=X Z
MaestroSettings[ j
.j k
Defaultk r
.r s-
 IdDetalleMaestroEstadoInvalidado	s �
;
� �
}
� �
}
� �
public 
bool 
PuedeFinalizar "
{# $
get% (
{) *
return+ 1
this2 6
.6 7
Estado7 =
!=> @
nullA E
&&F H
thisI M
.M N
EstadoN T
.T U
IdU W
==X Z
MaestroSettings[ j
.j k
Defaultk r
.r s,
IdDetalleMaestroEstadoIngresado	s �
;
� �
}
� �
}
� �
public"" #
MovimientoCocheraBasico"" &
(""& '
)""' (
{## 	
}$$ 	
}%% 
}&& �
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\MovimientoCochera.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
Custom

 $
.

$ %
SigesParking

% 1
{ 
public

class
TiempoHoras
{ 
public 
decimal 
Horas 
{ 
get "
;" #
set$ '
;' (
}) *
public 
decimal 
HorasACobrar #
{$ %
get& )
{* +
return, 2
Math3 7
.7 8
Ceiling8 ?
(? @
this@ D
.D E
HorasE J
)J K
;K L
}M N
}O P
public 
string 
HorasString !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
public 

class 
MovimientoCochera "
:" ##
MovimientoCocheraBasico$ ;
{ 
public 
bool 

{" #
get$ '
;' (
set) ,
;, -
}. /
public 
DetallesACobrar 
DetallesACobrar .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
bool 
VentaConsolidada $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
TiempoHoras -
!TiempoExcesoSistemaPlanaPorTurnos <
{= >
get? B
;B C
setD G
;G H
}I J
public   
TiempoHoras   !
TiempoSistemaPorHoras   0
{  1 2
get  3 6
;  6 7
set  8 ;
;  ; <
}  = >
public## 
MovimientoCochera##  
(##  !
)##! "
{$$ 	
}%% 	
public,, 
void,, #
EstablecerDatosDeSalida,, +
(,,+ ,
Transaccion,,, 7
transaccion,,8 C
),,C D
{-- 	
transaccion.. 
... 

indicador1.. "
=..# $
this..% )
...) *

;..7 8
transaccion// 
.// 

indicador2// "
=//# $
this//% )
.//) *
VentaConsolidada//* :
;//: ;
transaccion00 
.00 
importe100  
=00! "
this00# '
.00' (
DetallesACobrar00( 7
.007 8
	Principal008 A
;00A B
transaccion11 
.11 
importe211  
=11! "
this11# '
.11' (
DetallesACobrar11( 7
.117 8
Exceso118 >
;11> ?
transaccion22 
.22 
importe322  
=22! "
this22# '
.22' (
DetallesACobrar22( 7
.227 8
Ticket228 >
;22> ?
transaccion33 
.33 

=33& '
this33( ,
.33, -
DetallesACobrar33- <
.33< =
	Principal33= F
+33G H
this33I M
.33M N
DetallesACobrar33N ]
.33] ^
Exceso33^ d
+33e f
this33g k
.33k l
DetallesACobrar33l {
.33{ |
Ticket	33| �
;
33� �
transaccion44 
.44 
	fecha_fin44 !
=44" #
this44& *
.44* +
Salida44+ 1
;441 2
}55 	
public77 
List77 
<77 
MovimientoCochera77 %
>77% &
Convert77' .
(77. /
)77/ 0
{88 	
return99 
null99 
;99 
}:: 	
};; 
}<< �
WD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\TipoDocumento.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
}

 �
RD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\SigesParking\Vehiculo.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
Custom $
.$ %
SigesParking% 1
{ 
public 

class 
Vehiculo 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
IdActor 
{ 
get  
;  !
set" %
;% &
}' (
public		 
ItemGenerico		 
TipoDeVehiculo		 *
{		+ ,
get		- 0
;		0 1
set		2 5
;		5 6
}		7 8
public

 
string

 
Placa

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 
ItemGenerico 
Marca !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Color 
{ 
get !
;! "
set# &
;& '
}( )
public
string
NombreCompleto
{
get
{
return
TipoDeVehiculo
.
Nombre
+
$str
+
Marca
.
Nombre
+
$str
+
Color
;
}
}
public 
bool 
ExoneradoDePagos $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
Vehiculo 
( 
) 
{ 	
} 	
}   
}!! �.
XD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Accion\AccionOperativa.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
Custom( .
{ 
public 

class 
AccionOperativa  
{ 
Detalle_maestro 
accion 
; 
public

 
AccionOperativa

 
(

 
Detalle_maestro

 .
accion

/ 5
)

5 6
{ 	
this 
. 
accion 
= 
accion  
;  !
}
public 
int 
IdAccion 
{ 	
get 
{ 
return 
accion 
.  
id  "
;" #
}$ %
} 	
public 
string 
CodigoAccion "
{ 	
get 
{ 
return 
accion 
.  
codigo  &
;& '
}( )
} 	
public 
string 
NombreAccion "
{ 	
get 
{ 
return 
accion 
.  
nombre  &
;& '
}( )
} 	
public 
List 
< 
Rol 
> 
rolesPermitidos (
(( )
int) ,
idTipoTransaccion- >
,> ?
int@ C
idUnidadNegocioD S
)S T
{ 	
return   
this   
.   
accion   
.   
Accion_por_rol   -
.  - .
Where  . 3
(  3 4
apr  4 7
=>  8 :
apr  ; >
.  > ?
id_tipo_transaccion  ? R
==  S U
idTipoTransaccion  V g
&&  h j
apr  k n
.  n o
id_unidad_negocio	  o �
==
  � �
idUnidadNegocio
  � �
)
  � �
.
  � �
Select
  � �
(
  � �
apr
  � �
=>
  � �
apr
  � �
.
  � �
Rol
  � �
)
  � �
.
  � �
ToList
  � �
(
  � �
)
  � �
;
  � �
}!! 	
public## 
List## 
<## 
Detalle_maestro## #
>### $%
unidadesNegocioPermitidas##% >
(##> ?
int##? B
idTipoTransaccion##C T
,##T U
int##V Y
idRol##Z _
)##_ `
{$$ 	
return%% 
this%% 
.%% 
accion%% 
.%% 
Accion_por_rol%% -
.%%- .
Where%%. 3
(%%3 4
apr%%4 7
=>%%8 :
apr%%; >
.%%> ?
id_tipo_transaccion%%? R
==%%S U
idTipoTransaccion%%V g
&&%%h j
apr%%k n
.%%n o
id_rol%%o u
==%%u w
idRol%%x }
)%%} ~
.%%~ 
Select	%% �
(
%%� �
apr
%%� �
=>
%%� �
apr
%%� �
.
%%� �
Detalle_maestro1
%%� �
)
%%� �
.
%%� �
ToList
%%� �
(
%%� �
)
%%� �
;
%%� �
}&& 	
public(( 
List(( 
<(( 
Detalle_maestro(( #
>((# $%
unidadesNegocioPermitidas((% >
(((> ?
int((? B
idTipoTransaccion((C T
,((T U
Empleado((V ^
empleado((_ g
)((g h
{)) 	
int** 
[** 
]** 
idsRol** 
=** 
empleado** #
.**# $%
ObtenerRolesHijosVigentes**$ =
(**= >
)**> ?
.**? @
Select**@ F
(**F G
r**G H
=>**I K
r**L M
.**M N
id**N P
)**P Q
.**Q R
ToArray**R Y
(**Y Z
)**Z [
;**[ \
return++ 
this++ 
.++ 
accion++ 
.++ 
Accion_por_rol++ -
.++- .
Where++. 3
(++3 4
apr++4 7
=>++8 :
apr++; >
.++> ?
id_tipo_transaccion++? R
==++S U
idTipoTransaccion++V g
&&++h j
idsRol++k q
.++q r
Contains++r z
(++z {
apr++{ ~
.++~ 
id_rol	++ �
)
++� �
)
++� �
.
++� �
Select
++� �
(
++� �
apr
++� �
=>
++� �
apr
++� �
.
++� �
Detalle_maestro1
++� �
)
++� �
.
++� �
ToList
++� �
(
++� �
)
++� �
;
++� �
},, 	
public.. 
static.. 
List.. 
<.. 
AccionOperativa.. *
>..* +
convert..- 4
(..4 5
List..5 9
<..9 :
Detalle_maestro..: I
>..I J
acciones..K S
)..S T
{// 	
List00 
<00 
AccionOperativa00  
>00  !
accionesOperativas00" 4
=005 6
new007 :
List00; ?
<00? @
AccionOperativa00@ O
>00O P
(00P Q
)00Q R
;00R S
try11 
{22 
foreach33 
(33 
var33 
accion33 #
in33$ &
acciones33' /
)33/ 0
{44 
accionesOperativas55 &
.55& '
Add55' *
(55* +
new55+ .
AccionOperativa55/ >
(55> ?
accion55? E
)55E F
)55F G
;55G H
}66 
}77 
catch88 
{99 
};; 
return<< 
accionesOperativas<< %
;<<% &
}== 	
}>> 
}?? �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\BoletaDeCompra.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
ComprobantesModel( 9
{ 
public

class
BoletaDeCompra
:
DocumentoElectronicoCompra
{ 
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: W
;W X
public 
BoletaDeCompra 
( 
) 
{ 	
} 	
public 
BoletaDeCompra 
( 

orden, 1
,1 2-
!EstablecimientoComercialExtendido3 T
sedeU Y
,Y Z
byte[ _
[_ `
]` a
qrBytesb i
,i j
boolk o%
mostrarEncabezadoTestigo	p �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
} 	
public 
static 
List 
< 
BoletaDeCompra )
>) *
Convert+ 2
(2 3
List3 7
<7 8

>E F
ordenesG N
,N O-
!EstablecimientoComercialExtendidoP q
seder v
,v w-
 ModoImpresionCaracteristicasEnum	x �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
List 
< 
BoletaDeCompra 
>  
	resultado! *
=+ ,
new- 0
List1 5
<5 6
BoletaDeCompra6 D
>D E
(E F
)F G
;G H
foreach 
( 
var 
item 
in  
ordenes! (
)( )
{ 
	resultado   
.   
Add   
(   
new   !
BoletaDeCompra  " 0
(  0 1
item  1 5
,  5 6
sede  7 ;
,  ; <
null  = A
,  A B
false  C H
,  H I(
modoImpresionCaracteristicas  J f
)  f g
)  g h
;  h i
}!! 
return"" 
	resultado"" 
;"" 
}## 	
}$$ 
}%% �0
mD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\DocumentoDeCotizacion.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class !
DocumentoDeCotizacion &
:' (%
DocumentoElectronicoVenta) B
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: F
;F G
public 
DateTime 
FechaVencimiento (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
bool 

MostrarIgv 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool )
MostrarInformacionAdicional80 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicional80 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
bool )
MostrarInformacionAdicionalA4 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicionalA4 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public !
DocumentoDeCotizacion $
($ %
OrdenDeCotizacion% 6
orden7 <
,< =-
!EstablecimientoComercialExtendido> _
sede` d
,d e.
!EstablecimientoComercialExtendido	f �
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
FechaVencimiento 
= 
orden $
.$ %
FechaVencimiento% 5
;5 6

MostrarIgv 
= 
orden 
. 
Igv "
(" #
)# $
>% &
$num' (
;( ))
MostrarInformacionAdicional80 )
=* +
!, -
String- 3
.3 4

(A B
AplicacionSettingsB T
.T U
DefaultU \
.\ ],
 InformacionAdicionalCotizacion80] }
)} ~
;~ "
InformacionAdicional80 "
=# $
AplicacionSettings% 7
.7 8
Default8 ?
.? @,
 InformacionAdicionalCotizacion80@ `
;` a)
MostrarInformacionAdicionalA4 )
=* +
!, -
String- 3
.3 4

(A B
AplicacionSettingsB T
.T U
DefaultU \
.\ ],
 InformacionAdicionalCotizacionA4] }
)} ~
;~ "
InformacionAdicionalA4 "
=# $
AplicacionSettings% 7
.7 8
Default8 ?
.? @,
 InformacionAdicionalCotizacionA4@ `
;` a
} 	
public   !
DocumentoDeCotizacion   $
(  $ %
OrdenDeCotizacion  % 6
orden  7 <
,  < =4
(EstablecimientoComercialExtendidoConLogo  > f
sede  g k
,  k l.
!EstablecimientoComercialExtendido	  m �
establecimiento
  � �
,
  � �
byte
  � �
[
  � �
]
  � �
qrBytes
  � �
,
  � �
bool
  � �&
mostrarEncabezadoTestigo
  � �
,
  � �.
 ModoImpresionCaracteristicasEnum
  � �*
modoImpresionCaracteristicas
  � �
)
  � �
:
  � �
base
  � �
(
  � �
orden
  � �
,
  � �
sede
  � �
,
  � �
establecimiento
  � �
,
  � �
qrBytes
  � �
,
  � �&
mostrarEncabezadoTestigo
  � �
,
  � �*
modoImpresionCaracteristicas
  � �
)
  � �
{!! 	
FechaVencimiento"" 
="" 
orden"" $
.""$ %
FechaVencimiento""% 5
;""5 6

MostrarIgv## 
=## 
orden## 
.## 
Igv## "
(##" #
)### $
>##% &
$num##' (
;##( ))
MostrarInformacionAdicional80$$ )
=$$* +
!$$, -
String$$- 3
.$$3 4

($$A B
AplicacionSettings$$B T
.$$T U
Default$$U \
.$$\ ],
 InformacionAdicionalCotizacion80$$] }
)$$} ~
;$$~ "
InformacionAdicional80%% "
=%%# $
AplicacionSettings%%% 7
.%%7 8
Default%%8 ?
.%%? @,
 InformacionAdicionalCotizacion80%%@ `
;%%` a)
MostrarInformacionAdicionalA4&& )
=&&* +
!&&, -
String&&- 3
.&&3 4

(&&A B
AplicacionSettings&&B T
.&&T U
Default&&U \
.&&\ ],
 InformacionAdicionalCotizacionA4&&] }
)&&} ~
;&&~ "
InformacionAdicionalA4'' "
=''# $
AplicacionSettings''% 7
.''7 8
Default''8 ?
.''? @,
 InformacionAdicionalCotizacionA4''@ `
;''` a
}(( 	
})) 
}** �
mD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\DocumentoDePercepcion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
ComprobantesModel( 9
{		 
public

 

class

 !
DocumentoDePercepcion

 &
:

' ('
DocumentoElectronicoImpreso

) D
{ 
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: F
;F G
public
DateTime
FechaVencimiento
{
get
;
set
;
}
} 
} �
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\DocumentoDeRetencion.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Modelo		 
.		 
	Entidades		 '
.		' (
ComprobantesModel		( 9
{

 
public 

class  
DocumentoDeRetencion %
:& ''
DocumentoElectronicoImpreso( C
{ 
public
override
string

NombreTipo
{
get
;
set
;
}
=
$str
;
public 
decimal 
Igv 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
PlacaDelVehiculo &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
bool 
TieneGuiaDeRemision '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string )
TipoDeDocumentoGuiaDeRemision 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
string +
SerieDelDocumentoGuiaDeRemision 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
string ,
 NumeroDelDocumentoGuiaDeRemision 6
{7 8
get9 <
;< =
set> A
;A B
}C D
}HH 
}II �
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\FacturaCompra.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 

:  &
DocumentoElectronicoCompra! ;
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: O
;O P
public 
bool 
TieneGuiaDeRemision '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
List 
< 
Relacionado 
>  
GuiasDeRemision! 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 

( 
) 
{ 	
}
 
public 

( 

orden+ 0
,0 14
(EstablecimientoComercialExtendidoConLogo2 Z
sede[ _
,_ `
bytea e
[e f
]f g
qrBytesh o
,o p
boolq u%
mostrarEncabezadoTestigo	v �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TieneGuiaDeRemision 
=  !
orden" '
.' (
TieneGuiaDeRemision( ;
(; <
)< =
;= >
}   	
public"" 
static"" 
List"" 
<"" 

>""( )
Convert""* 1
(""1 2
List""2 6
<""6 7

>""D E
ordenes""F M
,""M N4
(EstablecimientoComercialExtendidoConLogo""O w
sede""x |
,""| }-
 ModoImpresionCaracteristicasEnum	""~ �*
modoImpresionCaracteristicas
""� �
)
""� �
{## 	
List$$ 
<$$ 

>$$ 
	resultado$$  )
=$$* +
new$$, /
List$$0 4
<$$4 5

>$$B C
($$C D
)$$D E
;$$E F
foreach%% 
(%% 
var%% 
item%% 
in%%  
ordenes%%! (
)%%( )
{&& 
	resultado'' 
.'' 
Add'' 
('' 
new'' !

(''/ 0
item''0 4
,''4 5
sede''6 :
,'': ;
null''< @
,''@ A
false''B G
,''G H(
modoImpresionCaracteristicas''I e
)''e f
)''f g
;''g h
}(( 
return)) 
	resultado)) 
;)) 
}** 	
}++ 
},, �
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\GuiaDeRemision.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
GuiaDeRemision 
:  !%
DocumentoElectronicoVenta" ;
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: L
;L M
public 
DateTime 
FechaInicioTraslado +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 


{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
	Conductor 
	Conductor "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string %
CodigoModalidadTransporte /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
string 
ModalidadTransporte )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string  
CodigoMotivoTraslado *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 
MotivoTraslado $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
PesoBrutoTotal %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
int 
NumeroBultos 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string !
UbigeoDireccionOrigen +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
string 
DireccionOrigen %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string "
UbigeoDireccionDestino ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
string 
DireccionDestino &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
DocumentoReferencia )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
EtiquetaTercero %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
=4 5
$str6 D
;D E
public 
GuiaDeRemision 
( 
) 
{   	
}"" 	
public$$ 
GuiaDeRemision$$ 
($$ 
MovimientoDeAlmacen$$ 1

movimiento$$2 <
,$$< =-
!EstablecimientoComercialExtendido$$> _
sede$$` d
,$$d e.
!EstablecimientoComercialExtendido	$$f �
establecimiento
$$� �
,
$$� �
byte
$$� �
[
$$� �
]
$$� �
qrBytes
$$� �
,
$$� �
bool
$$� �&
mostrarEncabezadoTestigo
$$� �
,
$$� �.
 ModoImpresionCaracteristicasEnum
$$� �*
modoImpresionCaracteristicas
$$� �
,
$$� �
List
$$� �
<
$$� �
	Proveedor
$$� �
>
$$� �
proveedores
$$� �
,
$$� �
List
$$� �
<
$$� �
Detalle_maestro
$$� �
>
$$� �
modalidades
$$� �
,
$$� �
List
$$� �
<
$$� �
Detalle_maestro
$$� �
>
$$� �
motivos
$$� �
)
$$� �
{%% 	
Emisor&& 
=&& 
new&& 
Emisor&& 
(&&  
sede&&  $
,&&$ %
establecimiento&&& 5
)&&5 6
;&&6 7 
RegistroGuiaRemision''  
(''  !

movimiento''! +
,''+ ,
establecimiento''- <
,''< =
qrBytes''> E
,''E F$
mostrarEncabezadoTestigo''G _
,''_ `(
modoImpresionCaracteristicas''a }
,''} ~
proveedores	'' �
,
''� �
modalidades
''� �
,
''� �
motivos
''� �
)
''� �
;
''� �
}(( 	
public** 
GuiaDeRemision** 
(** 
MovimientoDeAlmacen** 1

movimiento**2 <
,**< =4
(EstablecimientoComercialExtendidoConLogo**> f
sede**g k
,**k l.
!EstablecimientoComercialExtendido	**m �
establecimiento
**� �
,
**� �
byte
**� �
[
**� �
]
**� �
qrBytes
**� �
,
**� �
bool
**� �&
mostrarEncabezadoTestigo
**� �
,
**� �.
 ModoImpresionCaracteristicasEnum
**� �*
modoImpresionCaracteristicas
**� �
,
**� �
List
**� �
<
**� �
	Proveedor
**� �
>
**� �
proveedores
**� �
,
**� �
List
**� �
<
**� �
Detalle_maestro
**� �
>
**� �
modalidades
**� �
,
**� �
List
**� �
<
**� �
Detalle_maestro
**� �
>
**� �
motivos
**� �
)
**� �
{++ 	
Emisor,, 
=,, 
new,, 
Emisor,, 
(,,  
sede,,  $
,,,$ %
establecimiento,,& 5
),,5 6
;,,6 7 
RegistroGuiaRemision--  
(--  !

movimiento--! +
,--+ ,
establecimiento--- <
,--< =
qrBytes--> E
,--E F$
mostrarEncabezadoTestigo--G _
,--_ `(
modoImpresionCaracteristicas--a }
,--} ~
proveedores	-- �
,
--� �
modalidades
--� �
,
--� �
motivos
--� �
)
--� �
;
--� �
}.. 	
public00 
void00  
RegistroGuiaRemision00 (
(00( )
MovimientoDeAlmacen00) <

movimiento00= G
,00G H-
!EstablecimientoComercialExtendido00I j
establecimiento00k z
,00z {
byte	00| �
[
00� �
]
00� �
qrBytes
00� �
,
00� �
bool
00� �&
mostrarEncabezadoTestigo
00� �
,
00� �.
 ModoImpresionCaracteristicasEnum
00� �*
modoImpresionCaracteristicas
00� �
,
00� �
List
00� �
<
00� �
	Proveedor
00� �
>
00� �
proveedores
00� �
,
00� �
List
00� �
<
00� �
Detalle_maestro
00� �
>
00� �
modalidades
00� �
,
00� �
List
00� �
<
00� �
Detalle_maestro
00� �
>
00� �
motivos
00� �
)
00� �
{11 	
IdOrden22 
=22 

movimiento22  
.22  !
Id22! #
;22# $
FechaEmision33 
=33 

movimiento33 %
.33% &
FechaEmision33& 2
;332 3
Receptor44 
=44 
new44 
Receptor44 #
(44# $
new44$ '-
!EstablecimientoComercialExtendido44( I
(44I J

movimiento44J T
.44T U
Transaccion44U `
(44` a
)44a b
.44b c
Actor_negocio144c q
)44q r
)44r s
;44s t
Detalles55 
=55 
Detalle55 
.55 
Convert55 &
(55& '

movimiento55' 1
.551 2
Detalles552 :
(55: ;
)55; <
,55< =(
modoImpresionCaracteristicas55> Z
,55Z [
$str55\ ^
,55^ _
false55` e
)55e f
;55f g"
MostrarMensajeAmazonia66 "
=66# $
false66% *
;66* +
MensajeNegocio77 
=77 
AplicacionSettings77 /
.77/ 0
Default770 7
.777 8
MensajeDeNegocio778 H
;77H I!
MostrarMensajeNegocio88 !
=88" #
AplicacionSettings88$ 6
.886 7
Default887 >
.88> ?#
MostrarMensajeDeNegocio88? V
;88V W
Observacion99 
=99 

movimiento99 $
.99$ %
Observacion99% 0
(990 1
)991 2
;992 3
MostrarLogo:: 
=:: *
FacturacionElectronicaSettings:: 8
.::8 9
Default::9 @
.::@ A+
MostrarLogoEnComprobanteImpreso::A `
;::` a
CodigoQR;; 
=;; 
qrBytes;; 
;;; 
CodigoSunatMoneda<< 
=<< 

movimiento<<  *
.<<* +
Moneda<<+ 1
(<<1 2
)<<2 3
.<<3 4
Codigo<<4 :
;<<: ;
CodigoSunatTipo== 
=== 

movimiento== (
.==( )
Comprobante==) 4
(==4 5
)==5 6
.==6 7

CodigoTipo==7 A
;==A B
Serie>> 
=>> 

movimiento>> 
.>> 
Comprobante>> *
(>>* +
)>>+ ,
.>>, -

;>>: ;
Numero?? 
=?? 

movimiento?? 
.??  
Comprobante??  +
(??+ ,
)??, -
.??- .
NumeroDeComprobante??. A
;??A B
MostrarTestigo@@ 
=@@ $
mostrarEncabezadoTestigo@@ 5
;@@5 6'
ResolucionAutorizacionSunatAA '
=AA( )*
FacturacionElectronicaSettingsAA* H
.AAH I
DefaultAAI P
.AAP Q(
ResolucionEmisionElectronicaAAQ m
;AAm n
IdEstadoActualBB 
=BB 
MaestroSettingsBB ,
.BB, -
DefaultBB- 4
.BB4 5,
 IdDetalleMaestroEstadoConfirmadoBB5 U
;BBU V
FechaInicioTrasladoCC 
=CC  !
(CC" #
DateTimeCC# +
)CC+ ,

movimientoCC, 6
.CC6 7!
FechaInicioDeTrasladoCC7 L
(CCL M
)CCM N
;CCN O
varDD 

=DD 
proveedoresDD  +
.DD+ ,
SingleOrDefaultDD, ;
(DD; <
pDD< =
=>DD> @
pDDA B
.DDB C
IdDDC E
==DDF H

movimientoDDI S
.DDS T
IdTransporteDDT `
(DD` a
)DDa b
)DDb c
;DDc d
varEE 
	conductorEE 
=EE 
proveedoresEE '
.EE' (
SingleOrDefaultEE( 7
(EE7 8
pEE8 9
=>EE: <
pEE= >
.EE> ?
IdEE? A
==EEB D

movimientoEEE O
.EEO P!
IdConductorTransporteEEP e
(EEe f
)EEf g
)EEg h
;EEh i

=FF 

==FF* ,
nullFF- 1
?FF2 3
nullFF4 8
:FF9 :
newFF; >

{GG 
RazonSocialHH 
=HH 

.HH+ ,
RazonSocialHH, 7
,HH7 8"
TipoDocumentoIdentidadII &
=II' (

.II6 7(
CodigoTipoDocumentoIdentidadII7 S
(IIS T
)IIT U
,IIU V
DocumentoIdentidadJJ "
=JJ# $

.JJ2 3
DocumentoIdentidadJJ3 E
,JJE F
}KK 
;KK
	ConductorLL 
=LL 
	conductorLL !
==LL" $
nullLL% )
?LL* +
nullLL, 0
:LL1 2
newLL3 6
	ConductorLL7 @
{MM 
NombresNN 
=NN 
	conductorNN #
.NN# $
NombresNN$ +
,NN+ ,
	ApellidosOO 
=OO 
	conductorOO %
.OO% &
ApellidoPaternoOO& 5
+OO6 7
$strOO8 ;
+OO< =
	conductorOO> G
.OOG H
ApellidoMaternoOOH W
,OOW X"
TipoDocumentoIdentidadPP &
=PP' (
	conductorPP) 2
.PP2 3(
CodigoTipoDocumentoIdentidadPP3 O
(PPO P
)PPP Q
,PPQ R
DocumentoIdentidadQQ "
=QQ# $
	conductorQQ% .
.QQ. /
DocumentoIdentidadQQ/ A
,QQA B
NumeroLicenciaRR 
=RR  

movimientoRR! +
.RR+ ,'
LicenciaConductorTransporteRR, G
(RRG H
)RRH I
,RRI J
PlacaSS 
=SS 

movimientoSS "
.SS" #
PlacaTransporteSS# 2
(SS2 3
)SS3 4
,SS4 5
}TT 
;TT
CodigoModalidadTransporteUU %
=UU& '
modalidadesUU( 3
.UU3 4
SingleUU4 :
(UU: ;
mUU; <
=>UU= ?
mUU@ A
.UUA B
idUUB D
==UUE G

movimientoUUH R
.UUR S#
IdModalidadDeTransporteUUS j
(UUj k
)UUk l
)UUl m
.UUm n
codigoUUn t
;UUt u
ModalidadTransporteVV 
=VV  !
modalidadesVV" -
.VV- .
SingleVV. 4
(VV4 5
mVV5 6
=>VV7 9
mVV: ;
.VV; <
idVV< >
==VV? A

movimientoVVB L
.VVL M#
IdModalidadDeTransporteVVM d
(VVd e
)VVe f
)VVf g
.VVg h
nombreVVh n
;VVn o 
CodigoMotivoTrasladoWW  
=WW! "
motivosWW# *
.WW* +
SingleWW+ 1
(WW1 2
mWW2 3
=>WW4 6
mWW7 8
.WW8 9
idWW9 ;
==WW< >

movimientoWW? I
.WWI J 
IdMotivoDeTransporteWWJ ^
(WW^ _
)WW_ `
)WW` a
.WWa b
codigoWWb h
;WWh i
MotivoTrasladoXX 
=XX 
motivosXX $
.XX$ %
SingleXX% +
(XX+ ,
mXX, -
=>XX. 0
mXX1 2
.XX2 3
idXX3 5
==XX6 8

movimientoXX9 C
.XXC D 
IdMotivoDeTransporteXXD X
(XXX Y
)XXY Z
)XXZ [
.XX[ \
nombreXX\ b
;XXb c
PesoBrutoTotalYY 
=YY 

movimientoYY '
.YY' ($
PesoBrutoTotalTransporteYY( @
(YY@ A
)YYA B
;YYB C
NumeroBultosZZ 
=ZZ 

movimientoZZ %
.ZZ% &"
NumeroBultosTransporteZZ& <
(ZZ< =
)ZZ= >
;ZZ> ?!
UbigeoDireccionOrigen[[ !
=[[" #

movimiento[[$ .
.[[. /$
IdUbigeoOrigenDeTraslado[[/ G
([[G H
)[[H I
.[[I J
PadLeft[[J Q
([[Q R
$num[[R S
,[[S T
$char[[U X
)[[X Y
;[[Y Z
DireccionOrigen\\ 
=\\ 

movimiento\\ (
.\\( )%
DireccionOrigenDeTraslado\\) B
(\\B C
)\\C D
+\\E F
$str\\G L
+\\M N

movimiento\\O Y
.\\Y Z
UbigeoOrigen\\Z f
;\\f g"
UbigeoDireccionDestino]] "
=]]# $

movimiento]]% /
.]]/ 0%
IdUbigeoDestinoDeTraslado]]0 I
(]]I J
)]]J K
.]]K L
PadLeft]]L S
(]]S T
$num]]T U
,]]U V
$char]]W Z
)]]Z [
;]][ \
DireccionDestino^^ 
=^^ 

movimiento^^ )
.^^) *&
DireccionDestinoDeTraslado^^* D
(^^D E
)^^E F
+^^G H
$str^^I N
+^^O P

movimiento^^Q [
.^^[ \

;^^i j
DocumentoReferencia__ 
=__  !

movimiento__" ,
.__, -!
DocumentoDeReferencia__- B
(__B C
)__C D
;__D E
EtiquetaTercero`` 
=`` 
(`` 
motivos`` &
.``& '
Single``' -
(``- .
m``. /
=>``0 2
m``3 4
.``4 5
id``5 7
==``8 :

movimiento``; E
.``E F 
IdMotivoDeTransporte``F Z
(``Z [
)``[ \
)``\ ]
.``] ^
id``^ `
==``a c
MaestroSettings``d s
.``s t
Default``t {
.``{ |6
)IdDetalleMaestroMotivoDeTrasladoPorCompra	``| �
)
``� �
?
``� �
$str
``� �
:
``� �
$str
``� �
;
``� �
EsInvalidadaaa 
=aa 

movimientoaa %
.aa% &
EstaInvalidadaaa& 4
(aa4 5
)aa5 6
;aa6 7
MotivoInvalidacionbb 
=bb  

movimientobb! +
.bb+ ,
MotivoInvalidacionbb, >
(bb> ?
)bb? @
;bb@ A
}cc 	
publicee 
staticee 
Listee 
<ee 
GuiaDeRemisionee )
>ee) *
Convertee+ 2
(ee2 3
Listee3 7
<ee7 8
MovimientoDeAlmacenee8 K
>eeK L
ordeneseeM T
,eeT U-
!EstablecimientoComercialExtendidoeeV w
sedeeex |
,ee| }-
 ModoImpresionCaracteristicasEnum	ee~ �*
modoImpresionCaracteristicas
ee� �
,
ee� �
List
ee� �
<
ee� �
	Proveedor
ee� �
>
ee� �
proveedores
ee� �
,
ee� �
List
ee� �
<
ee� �
Detalle_maestro
ee� �
>
ee� �
modalidades
ee� �
,
ee� �
List
ee� �
<
ee� �
Detalle_maestro
ee� �
>
ee� �
motivos
ee� �
)
ee� �
{ff 	
Listgg 
<gg 
GuiaDeRemisiongg 
>gg  
	resultadogg! *
=gg+ ,
newgg- 0
Listgg1 5
<gg5 6
GuiaDeRemisiongg6 D
>ggD E
(ggE F
)ggF G
;ggG H
foreachhh 
(hh 
varhh 
itemhh 
inhh  
ordeneshh! (
)hh( )
{ii 
	resultadojj 
.jj 
Addjj 
(jj 
newjj !
GuiaDeRemisionjj" 0
(jj0 1
itemjj1 5
,jj5 6
sedejj7 ;
,jj; <
newjj= @4
(EstablecimientoComercialExtendidoConLogojjA i
(jji j
itemjjj n
.jjn o
Transaccionjjo z
(jjz {
)jj{ |
.jj| }
Actor_negocio2	jj} �
.
jj� �
Actor_negocio2
jj� �
)
jj� �
,
jj� �
null
jj� �
,
jj� �
false
jj� �
,
jj� �*
modoImpresionCaracteristicas
jj� �
,
jj� �
proveedores
jj� �
,
jj� �
modalidades
jj� �
,
jj� �
motivos
jj� �
)
jj� �
)
jj� �
;
jj� �
}kk 
returnll 
	resultadoll 
;ll 
}mm 	
}nn 
publicpp 

classpp 

{qq 
publicrr 
stringrr 
RazonSocialrr !
{rr" #
getrr$ '
;rr' (
setrr) ,
;rr, -
}rr. /
publicss 
stringss "
TipoDocumentoIdentidadss ,
{ss- .
getss/ 2
;ss2 3
setss4 7
;ss7 8
}ss9 :
publictt 
stringtt 
DocumentoIdentidadtt (
{tt) *
gettt+ .
;tt. /
settt0 3
;tt3 4
}tt5 6
}uu 
publicww 

classww 
	Conductorww 
{xx 
publicyy 
stringyy 
Nombresyy 
{yy 
getyy  #
;yy# $
setyy% (
;yy( )
}yy* +
publiczz 
stringzz 
	Apellidoszz 
{zz  !
getzz" %
;zz% &
setzz' *
;zz* +
}zz, -
public{{ 
string{{ "
TipoDocumentoIdentidad{{ ,
{{{- .
get{{/ 2
;{{2 3
set{{4 7
;{{7 8
}{{9 :
public|| 
string|| 
DocumentoIdentidad|| (
{||) *
get||+ .
;||. /
set||0 3
;||3 4
}||5 6
public}} 
string}} 
NumeroLicencia}} $
{}}% &
get}}' *
;}}* +
set}}, /
;}}/ 0
}}}1 2
public~~ 
string~~ 
Placa~~ 
{~~ 
get~~ !
;~~! "
set~~# &
;~~& '
}~~( )
}
�� 
}�� �M
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeAlmacen.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 

:  %
DocumentoElectronicoVenta! :
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: K
;K L
public 
Receptor 
ResponsableReceptor +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
Receptor 
ResponsableOrigen )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
Receptor 
Origen 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
EsTrasladoInterno %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 

( 
) 
{ 	
} 	
public 

( 
MovimientoDeAlmacen 0

movimiento1 ;
,; <4
(EstablecimientoComercialExtendidoConLogo= e
sedef j
,j k.
!EstablecimientoComercialExtendido	l �
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
FechaEmision 
= 

movimiento %
.% &
FechaEmision& 2
;2 3
Emisor 
= 
new 
Emisor 
(  
sede  $
,$ %
establecimiento& 5
)5 6
;6 7
EsTrasladoInterno 
= 

movimiento  *
.* +
IdTipoTransaccion+ <
=== ?
TransaccionSettings@ S
.S T
DefaultT [
.[ \E
8IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno	\ �
||
� �

movimiento
� �
.
� �
IdTipoTransaccion
� �
==
� �!
TransaccionSettings
� �
.
� �
Default
� �
.
� �E
7IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno
� �
;
� �
if 
( 
EsTrasladoInterno  
)  !
{ 
var 
ingresoMercaderia %
=& '

movimiento( 2
.2 3
IdTipoTransaccion3 D
==E G
TransaccionSettingsH [
.[ \
Default\ c
.c dE
8IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno	d �
;
� �
var   +
idTipoTransaccionComplementaria   3
=  4 5
ingresoMercaderia  6 G
?  H I
TransaccionSettings  J ]
.  ] ^
Default  ^ e
.  e fD
7IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno	  f �
:
  � �!
TransaccionSettings
  � �
.
  � �
Default
  � �
.
  � �F
8IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno
  � �
;
  � �
var!! 
empleadoResponsable!! '
=!!( )

movimiento!!* 4
.!!4 5
Transaccion!!5 @
(!!@ A
)!!A B
.!!B C
Transaccion3!!C O
.!!O P

.!!] ^
Single!!^ d
(!!d e
t!!e f
=>!!g i
t!!j k
.!!k l
id_tipo_transaccion!!l 
==
!!� �-
idTipoTransaccionComplementaria
!!� �
&&
!!� �
t
!!� �
.
!!� �
id_estado_actual
!!� �
==
!!� �
MaestroSettings
!!� �
.
!!� �
Default
!!� �
.
!!� �.
 IdDetalleMaestroEstadoConfirmado
!!� �
)
!!� �
.
!!� �

!!� �
;
!!� �
Receptor"" 
="" 
new"" 
Receptor"" '
(""' (
new""( +-
!EstablecimientoComercialExtendido"", M
(""M N
ingresoMercaderia""N _
?""` a

movimiento""b l
.""l m
Transaccion""m x
(""x y
)""y z
.""z {
Actor_negocio2	""{ �
:
""� �

movimiento
""� �
.
""� �
Transaccion
""� �
(
""� �
)
""� �
.
""� �
Actor_negocio1
""� �
)
""� �
)
""� �
;
""� �
ResponsableReceptor## #
=##$ %
new##& )
Receptor##* 2
(##2 3
new##3 6-
!EstablecimientoComercialExtendido##7 X
(##X Y
ingresoMercaderia##Y j
?##k l

movimiento##n x
.##x y
Transaccion	##y �
(
##� �
)
##� �
.
##� �

##� �
:
##� �!
empleadoResponsable
##� �
)
##� �
)
##� �
;
##� �
Origen$$ 
=$$ 
new$$ 
Receptor$$ %
($$% &
new$$& )-
!EstablecimientoComercialExtendido$$* K
($$K L
ingresoMercaderia$$L ]
?$$^ _

movimiento$$` j
.$$j k
Transaccion$$k v
($$v w
)$$w x
.$$x y
Actor_negocio1	$$y �
:
$$� �

movimiento
$$� �
.
$$� �
Transaccion
$$� �
(
$$� �
)
$$� �
.
$$� �
Actor_negocio2
$$� �
)
$$� �
)
$$� �
;
$$� �
ResponsableOrigen%% !
=%%" #
new%%$ '
Receptor%%( 0
(%%0 1
new%%1 4-
!EstablecimientoComercialExtendido%%5 V
(%%V W
ingresoMercaderia%%W h
?%%i j
empleadoResponsable%%k ~
:	%% �

movimiento
%%� �
.
%%� �
Transaccion
%%� �
(
%%� �
)
%%� �
.
%%� �

%%� �
)
%%� �
)
%%� �
;
%%� �
}&& 
else'' 
{(( 
Receptor)) 
=)) 
new)) 
Receptor)) '
())' (
new))( +-
!EstablecimientoComercialExtendido)), M
())M N

movimiento))N X
.))X Y
Transaccion))Y d
())d e
)))e f
.))f g
Actor_negocio1))g u
)))u v
)))v w
;))w x
}** 
Detalles++ 
=++ 
Detalle++ 
.++ 
Convert++ &
(++& '

movimiento++' 1
.++1 2
Detalles++2 :
(++: ;
)++; <
,++< =(
modoImpresionCaracteristicas++> Z
,++Z [
$str++\ ^
,++^ _
false++` e
)++e f
;++f g"
MostrarMensajeAmazonia,, "
=,,# $
false,,% *
;,,* +
MensajeNegocio-- 
=-- 
AplicacionSettings-- /
.--/ 0
Default--0 7
.--7 8
MensajeDeNegocio--8 H
;--H I
Observacion.. 
=.. 

movimiento.. $
...$ %
Observacion..% 0
(..0 1
)..1 2
;..2 3
MostrarLogo// 
=// *
FacturacionElectronicaSettings// 8
.//8 9
Default//9 @
.//@ A+
MostrarLogoEnComprobanteImpreso//A `
;//` a
CodigoQR00 
=00 
qrBytes00 
;00 
Serie11 
=11 

movimiento11 
.11 
Comprobante11 *
(11* +
)11+ ,
.11, -

;11: ;
Numero22 
=22 

movimiento22 
.22  
Comprobante22  +
(22+ ,
)22, -
.22- .
NumeroDeComprobante22. A
;22A B
ImporteTotal33 
=33 

movimiento33 %
.33% &
Total33& +
;33+ , 
ImporteTotalEnLetras44  
=44! "
Util44# '
.44' (
	APalabras44( 1
(441 2

movimiento442 <
.44< =
Total44= B
,44B C

movimiento44D N
.44N O
MonedaPlural44O [
(44[ \
)44\ ]
)44] ^
;44^ _
	Descuento55 
=55 

movimiento55 "
.55" #
	Descuento55# ,
(55, -
)55- .
;55. /
Igv66 
=66 

movimiento66 
.66 
Igv66  
(66  !
)66! "
;66" #
MostrarTestigo77 
=77 $
mostrarEncabezadoTestigo77 5
;775 6'
ResolucionAutorizacionSunat88 '
=88( )*
FacturacionElectronicaSettings88* H
.88H I
Default88I P
.88P Q(
ResolucionEmisionElectronica88Q m
;88m n
EsInvalidada99 
=99 

movimiento99 %
.99% &
EstaInvalidada99& 4
(994 5
)995 6
;996 7
MotivoInvalidacion:: 
=::  

movimiento::! +
.::+ ,
MotivoInvalidacion::, >
(::> ?
)::? @
;::@ A
}<< 	
}>> 
}?? �/
kD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeCreditoCompra.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
NotaDeCreditoCompra $
:% &&
DocumentoElectronicoCompra' A
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: W
;W X
public 
string 
TipoDeNotaCredito '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 

Referencia 
Discrepancia &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Relacionado  
DocumentoRelacionado /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
NotaDeCreditoCompra "
(" #
OperacionDeCompra# 4
	operacion5 >
,> ?4
(EstablecimientoComercialExtendidoConLogo@ h
sedei m
,m n
Listo s
<s t
Detalle_maestro	t �
>
� �"
tiposDeNotaDeCredito
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
	operacion
� �
,
� �
sede
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TipoDeNotaCredito 
=  
tiposDeNotaDeCredito  4
.4 5
SingleOrDefault5 D
(D E
tE F
=>G I
tJ K
.K L
codigoL R
==S U
	operacionV _
._ `$
CodigoSunatDeTransaccion` x
(x y
)y z
)z {
.{ |
nombre	| �
;
� �
Discrepancia 
= 
new 

Referencia )
{ 

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

+^ _
$str` c
+d e
	operacionf o
.o p"
OperacionDeReferencia	p �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �
Tipo 
= 
	operacion  
.  !$
CodigoSunatDeTransaccion! 9
(9 :
): ;
,; <
Descripcion 
= 
	operacion '
.' (

Comentario( 2
,2 3
} 
;
DocumentoRelacionado  
=! "
new# &
Relacionado' 2
{ 
NroDocumento 
= 
	operacion (
.( )!
OperacionDeReferencia) >
(> ?
)? @
.@ A
ComprobanteA L
(L M
)M N
.N O

+] ^
$str_ b
+c d
	operacione n
.n o"
OperacionDeReferencia	o �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

CodigoTipoP Z
,Z [
}   
;  
}!! 	
public## 
static## 
List## 
<## 
NotaDeCreditoCompra## .
>##. /
Convert##0 7
(##7 8
List##8 <
<##< =
OperacionDeCompra##= N
>##N O
operaciones##P [
,##[ \5
(EstablecimientoComercialExtendidoConLogo	##] �
sede
##� �
,
##� �
List
##� �
<
##� �
Detalle_maestro
##� �
>
##� �"
tiposDeNotaDeCredito
##� �
,
##� �.
 ModoImpresionCaracteristicasEnum
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
{$$ 	
List%% 
<%% 
NotaDeCreditoCompra%% $
>%%$ %
	resultado%%& /
=%%0 1
new%%2 5
List%%6 :
<%%: ;
NotaDeCreditoCompra%%; N
>%%N O
(%%O P
)%%P Q
;%%Q R
foreach&& 
(&& 
var&& 
item&& 
in&&  
operaciones&&! ,
)&&, -
{'' 
	resultado(( 
.(( 
Add(( 
((( 
new(( !
NotaDeCreditoCompra((" 5
(((5 6
item((6 :
,((: ;
sede((< @
,((@ A 
tiposDeNotaDeCredito((B V
,((V W
null((X \
,((\ ]
false((^ c
,((c d)
modoImpresionCaracteristicas	((e �
)
((� �
)
((� �
;
((� �
})) 
return** 
	resultado** 
;** 
}++ 	
},, 
}-- �/
jD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeDebitoCompra.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
NotaDeDebitoCompra #
:$ %&
DocumentoElectronicoCompra& @
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: V
;V W
public 
string 
TipoDeNotaDebito &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 

Referencia 
Discrepancia &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Relacionado  
DocumentoRelacionado /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
NotaDeDebitoCompra !
(! "
OperacionDeCompra" 3
	operacion4 =
,= >4
(EstablecimientoComercialExtendidoConLogo? g
sedeh l
,l m
Listn r
<r s
Detalle_maestro	s �
>
� �!
tiposDeNotaDeDebito
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
	operacion
� �
,
� �
sede
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TipoDeNotaDebito 
= 
tiposDeNotaDeDebito 2
.2 3
SingleOrDefault3 B
(B C
tC D
=>E G
tH I
.I J
codigoJ P
==Q S
	operacionT ]
.] ^$
CodigoSunatDeTransaccion^ v
(v w
)w x
)x y
.y z
nombre	z �
;
� �
Discrepancia 
= 
new 

Referencia )
{ 

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

+^ _
$str` c
+d e
	operacionf o
.o p"
OperacionDeReferencia	p �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �
Tipo 
= 
	operacion  
.  !$
CodigoSunatDeTransaccion! 9
(9 :
): ;
,; <
Descripcion 
= 
	operacion '
.' (

Comentario( 2
,2 3
} 
;
DocumentoRelacionado  
=! "
new# &
Relacionado' 2
{ 
NroDocumento 
= 
	operacion (
.( )!
OperacionDeReferencia) >
(> ?
)? @
.@ A
ComprobanteA L
(L M
)M N
.N O

+] ^
$str_ b
+c d
	operacione n
.n o"
OperacionDeReferencia	o �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

CodigoTipoP Z
,Z [
}   
;  
}!! 	
public## 
static## 
List## 
<## 
NotaDeDebitoCompra## -
>##- .
Convert##/ 6
(##6 7
List##7 ;
<##; <
OperacionDeCompra##< M
>##M N
operaciones##O Z
,##Z [5
(EstablecimientoComercialExtendidoConLogo	##\ �
sede
##� �
,
##� �
List
##� �
<
##� �
Detalle_maestro
##� �
>
##� �!
tiposDeNotaDeDebito
##� �
,
##� �.
 ModoImpresionCaracteristicasEnum
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
{$$ 	
List%% 
<%% 
NotaDeDebitoCompra%% #
>%%# $
	resultado%%% .
=%%/ 0
new%%1 4
List%%5 9
<%%9 :
NotaDeDebitoCompra%%: L
>%%L M
(%%M N
)%%N O
;%%O P
foreach&& 
(&& 
var&& 
item&& 
in&&  
operaciones&&! ,
)&&, -
{'' 
	resultado(( 
.(( 
Add(( 
((( 
new(( !
NotaDeDebitoCompra((" 4
(((4 5
item((5 9
,((9 :
sede((; ?
,((? @
tiposDeNotaDeDebito((A T
,((T U
null((V Z
,((Z [
false((\ a
,((a b(
modoImpresionCaracteristicas((c 
)	(( �
)
((� �
;
((� �
})) 
return** 
	resultado** 
;** 
}++ 	
},, 
}-- �L
dD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeDebito.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
NotaDeDebito 
: %
DocumentoElectronicoVenta  9
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: V
;V W
public 
string 
TipoDeNotaDebito &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 

Referencia 
Discrepancia &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Relacionado  
DocumentoRelacionado /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 
NotaDeDebito 
( 
OperacionDeVenta ,
	operacion- 6
,6 7-
!EstablecimientoComercialExtendido8 Y
sedeZ ^
,^ _.
!EstablecimientoComercialExtendido	` �
establecimiento
� �
,
� �
List
� �
<
� �
Detalle_maestro
� �
>
� �!
tiposDeNotaDeDebito
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
	operacion
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TipoDeNotaDebito 
= 
tiposDeNotaDeDebito 2
.2 3
SingleOrDefault3 B
(B C
tC D
=>E G
tH I
.I J
codigoJ P
==Q S
	operacionT ]
.] ^$
CodigoSunatDeTransaccion^ v
(v w
)w x
)x y
.y z
nombre	z �
;
� �
Discrepancia 
= 
new 

Referencia )
{ 

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

+^ _
$str` c
+d e
	operacionf o
.o p"
OperacionDeReferencia	p �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �
Tipo 
= 
	operacion  
.  !$
CodigoSunatDeTransaccion! 9
(9 :
): ;
,; <
Descripcion 
= 
	operacion '
.' (

Comentario( 2
,2 3
} 
;
DocumentoRelacionado  
=! "
new# &
Relacionado' 2
{ 
NroDocumento 
= 
	operacion (
.( )!
OperacionDeReferencia) >
(> ?
)? @
.@ A
ComprobanteA L
(L M
)M N
.N O

+] ^
$str_ b
+c d
	operacione n
.n o"
OperacionDeReferencia	o �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

CodigoTipoP Z
,Z [
}   
;  
}!! 	
public## 
NotaDeDebito## 
(## 
OperacionDeVenta## ,
	operacion##- 6
,##6 74
(EstablecimientoComercialExtendidoConLogo##8 `
sede##a e
,##e f.
!EstablecimientoComercialExtendido	##g �
establecimiento
##� �
,
##� �
List
##� �
<
##� �
Detalle_maestro
##� �
>
##� �!
tiposDeNotaDeDebito
##� �
,
##� �
byte
##� �
[
##� �
]
##� �
qrBytes
##� �
,
##� �
bool
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �.
 ModoImpresionCaracteristicasEnum
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
:
##� �
base
##� �
(
##� �
	operacion
##� �
,
##� �
sede
##� �
,
##� �
establecimiento
##� �
,
##� �
qrBytes
##� �
,
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
{$$ 	
TipoDeNotaDebito%% 
=%% 
tiposDeNotaDeDebito%% 2
.%%2 3
SingleOrDefault%%3 B
(%%B C
t%%C D
=>%%E G
t%%H I
.%%I J
codigo%%J P
==%%Q S
	operacion%%T ]
.%%] ^$
CodigoSunatDeTransaccion%%^ v
(%%v w
)%%w x
)%%x y
.%%y z
nombre	%%z �
;
%%� �
Discrepancia&& 
=&& 
new&& 

Referencia&& )
{'' 

=(( 
	operacion((  )
.(() *!
OperacionDeReferencia((* ?
(((? @
)((@ A
.((A B
Comprobante((B M
(((M N
)((N O
.((O P

+((^ _
$str((` c
+((d e
	operacion((f o
.((o p"
OperacionDeReferencia	((p �
(
((� �
)
((� �
.
((� �
Comprobante
((� �
(
((� �
)
((� �
.
((� �!
NumeroDeComprobante
((� �
,
((� �
Tipo)) 
=)) 
	operacion))  
.))  !$
CodigoSunatDeTransaccion))! 9
())9 :
))): ;
,)); <
Descripcion** 
=** 
	operacion** '
.**' (

Comentario**( 2
,**2 3
}++ 
;++
DocumentoRelacionado,,  
=,,! "
new,,# &
Relacionado,,' 2
{-- 
NroDocumento.. 
=.. 
	operacion.. (
...( )!
OperacionDeReferencia..) >
(..> ?
)..? @
...@ A
Comprobante..A L
(..L M
)..M N
...N O

+..] ^
$str.._ b
+..c d
	operacion..e n
...n o"
OperacionDeReferencia	..o �
(
..� �
)
..� �
.
..� �
Comprobante
..� �
(
..� �
)
..� �
.
..� �!
NumeroDeComprobante
..� �
,
..� �

=// 
	operacion//  )
.//) *!
OperacionDeReferencia//* ?
(//? @
)//@ A
.//A B
Comprobante//B M
(//M N
)//N O
.//O P

CodigoTipo//P Z
,//Z [
}00 
;00
}11 	
public33 
static33 
List33 
<33 
NotaDeDebito33 '
>33' (
Convert33) 0
(330 1
List331 5
<335 6
OperacionDeVenta336 F
>33F G
operaciones33H S
,33S T-
!EstablecimientoComercialExtendido33U v
sede33w {
,33{ |
List	33} �
<
33� �
Detalle_maestro
33� �
>
33� �!
tiposDeNotaDeDebito
33� �
,
33� �.
 ModoImpresionCaracteristicasEnum
33� �*
modoImpresionCaracteristicas
33� �
)
33� �
{44 	
List55 
<55 
NotaDeDebito55 
>55 
	resultado55 (
=55) *
new55+ .
List55/ 3
<553 4
NotaDeDebito554 @
>55@ A
(55A B
)55B C
;55C D
foreach66 
(66 
var66 
item66 
in66  
operaciones66! ,
)66, -
{77 
	resultado88 
.88 
Add88 
(88 
new88 !
NotaDeDebito88" .
(88. /
item88/ 3
,883 4
sede885 9
,889 :
new88; >4
(EstablecimientoComercialExtendidoConLogo88? g
(88g h
item88h l
.88l m
Transaccion88m x
(88x y
)88y z
.88z {
Actor_negocio2	88{ �
.
88� �
Actor_negocio2
88� �
)
88� �
,
88� �!
tiposDeNotaDeDebito
88� �
,
88� �
null
88� �
,
88� �
false
88� �
,
88� �*
modoImpresionCaracteristicas
88� �
)
88� �
)
88� �
;
88� �
}99 
return:: 
	resultado:: 
;:: 
};; 	
}<< 
}== �
dD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeCompra.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
NotaDeCompra 
: &
DocumentoElectronicoCompra  :
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: J
;J K
public 
NotaDeCompra 
( 
) 
{ 	
} 	
public 
NotaDeCompra 
( 

orden* /
,/ 04
(EstablecimientoComercialExtendidoConLogo1 Y
sedeZ ^
,^ _
byte` d
[d e
]e f
qrBytesg n
,n o
boolp t%
mostrarEncabezadoTestigo	u �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	'
ResolucionAutorizacionSunat '
=( )
$str* ,
;, -
} 	
} 
} �1
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\OrdenDeAlmacen.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
OrdenDeAlmacen 
:  !'
DocumentoElectronicoImpreso" =
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: L
;L M
public 
OrdenDeAlmacen 
( 
) 
{ 	
} 	
public 
OrdenDeAlmacen 
( &
OrdenDeMovimientoDeAlmacen 8
ordenDeMovimiento9 J
,J K-
!EstablecimientoComercialExtendidoL m
seden r
,r s.
!EstablecimientoComercialExtendido	t �
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
IdOrden 
= 
ordenDeMovimiento '
.' (
Id( *
;* +
FechaEmision 
= 
ordenDeMovimiento ,
., -
FechaEmision- 9
;9 :
Emisor 
= 
new 
Emisor 
(  
sede  $
,$ %
establecimiento& 5
)5 6
;6 7
Receptor 
= 
new 
Receptor #
(# $
new$ '-
!EstablecimientoComercialExtendido( I
(I J
ordenDeMovimientoJ [
.[ \
Transaccion\ g
(g h
)h i
.i j
Actor_negocio1j x
)x y
)y z
;z {
Detalles 
= 
Detalle 
. 
Convert &
(& '
ordenDeMovimiento' 8
.8 9
Detalles9 A
(A B
)B C
,C D(
modoImpresionCaracteristicasE a
,a b
$strc e
,e f
falseg l
)l m
;m n"
MostrarMensajeAmazonia "
=# $
false% *
;* +
MensajeNegocio 
= 
AplicacionSettings /
./ 0
Default0 7
.7 8
MensajeDeNegocio8 H
;H I!
MostrarMensajeNegocio !
=" #
AplicacionSettings$ 6
.6 7
Default7 >
.> ?#
MostrarMensajeDeNegocio? V
;V W
Observacion 
= 
ordenDeMovimiento +
.+ ,
Observacion, 7
(7 8
)8 9
;9 :
MostrarLogo   
=   *
FacturacionElectronicaSettings   8
.  8 9
Default  9 @
.  @ A+
MostrarLogoEnComprobanteImpreso  A `
;  ` a
CodigoQR!! 
=!! 
qrBytes!! 
;!! 
CodigoSunatMoneda"" 
="" 
ordenDeMovimiento""  1
.""1 2
Moneda""2 8
(""8 9
)""9 :
."": ;
Codigo""; A
;""A B
CodigoSunatTipo## 
=## 
ordenDeMovimiento## /
.##/ 0
Comprobante##0 ;
(##; <
)##< =
.##= >

CodigoTipo##> H
;##H I
Serie$$ 
=$$ 
ordenDeMovimiento$$ %
.$$% &
Comprobante$$& 1
($$1 2
)$$2 3
.$$3 4

;$$A B
Numero%% 
=%% 
ordenDeMovimiento%% &
.%%& '
Comprobante%%' 2
(%%2 3
)%%3 4
.%%4 5
NumeroDeComprobante%%5 H
;%%H I
MostrarTestigo&& 
=&& $
mostrarEncabezadoTestigo&& 5
;&&5 6'
ResolucionAutorizacionSunat'' '
=''( )*
FacturacionElectronicaSettings''* H
.''H I
Default''I P
.''P Q(
ResolucionEmisionElectronica''Q m
;''m n
IdEstadoActual(( 
=(( 
MaestroSettings(( ,
.((, -
Default((- 4
.((4 5,
 IdDetalleMaestroEstadoConfirmado((5 U
;((U V
}** 	
public++ 
static++ 
List++ 
<++ 
OrdenDeAlmacen++ )
>++) *
Convert+++ 2
(++2 3
List++3 7
<++7 8&
OrdenDeMovimientoDeAlmacen++8 R
>++R S
ordenes++T [
,++[ \-
!EstablecimientoComercialExtendido++] ~
sede	++ �
,
++� �.
 ModoImpresionCaracteristicasEnum
++� �*
modoImpresionCaracteristicas
++� �
)
++� �
{,, 	
List-- 
<-- 
OrdenDeAlmacen-- 
>--  
	resultado--! *
=--+ ,
new--- 0
List--1 5
<--5 6
OrdenDeAlmacen--6 D
>--D E
(--E F
)--F G
;--G H
foreach.. 
(.. 
var.. 
item.. 
in..  
ordenes..! (
)..( )
{// 
	resultado00 
.00 
Add00 
(00 
new00 !
OrdenDeAlmacen00" 0
(000 1
item001 5
,005 6
sede007 ;
,00; <
new00= @4
(EstablecimientoComercialExtendidoConLogo00A i
(00i j
item00j n
.00n o
Transaccion00o z
(00z {
)00{ |
.00| }
Actor_negocio2	00} �
.
00� �
Actor_negocio2
00� �
)
00� �
,
00� �
null
00� �
,
00� �
false
00� �
,
00� �*
modoImpresionCaracteristicas
00� �
)
00� �
)
00� �
;
00� �
}11 
return22 
	resultado22 
;22 
}33 	
}44 
}55 �G
mD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\ReciboDeIngresoEgreso.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
ComprobantesModel( 9
{ 
public

class
ReciboDeIngresoEgreso
:
DocumentoElectronicoImpreso
{ 
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
Caja 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Cajero 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string  
TipoPagadorRecibidor *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
string 
MedioDePago !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
EntidadBancaria %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
InformacionDePago '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
List 
< "
DetalleDeIngresoEgreso *
>* +#
DetallesDeIngresoEgreso, C
{D E
getF I
;I J
setK N
;N O
}P Q
public 
static !
ReciboDeIngresoEgreso +
Convert, 3
(3 4
MovimientoEconomico4 G

movimientoH R
,R S4
(EstablecimientoComercialExtendidoConLogoT |
sede	} �
,
� �/
!EstablecimientoComercialExtendido
� �
establecimiento
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
)
� �
{ 	
return 
new !
ReciboDeIngresoEgreso ,
{ 
FechaEmision 
= 

movimiento )
.) *
FechaEmision* 6
,6 7
Emisor 
= 
new 
Emisor #
(# $
sede$ (
,( )
establecimiento* 9
)9 :
,: ;
Receptor 
= 
new 
Receptor '
(' (
new( +-
!EstablecimientoComercialExtendido, M
(M N

movimientoN X
.X Y
TransaccionY d
(d e
)e f
.f g
Actor_negocio1g u
)u v
)v w
,w x

NombreTipo 
= 

movimiento '
.' (
Comprobante( 3
(3 4
)4 5
.5 6

NombreTipo6 @
,@ A
Serie   
=   

movimiento   "
.  " #
Comprobante  # .
(  . /
)  / 0
.  0 1

,  > ?
Numero!! 
=!! 

movimiento!! #
.!!# $
Comprobante!!$ /
(!!/ 0
)!!0 1
.!!1 2
NumeroDeComprobante!!2 E
,!!E F
ImporteTotal"" 
="" 

movimiento"" )
."") *
Total""* /
,""/ 0 
ImporteTotalEnLetras## $
=##% &
Util##' +
.##+ ,
	APalabras##, 5
(##5 6

movimiento##6 @
.##@ A
Total##A F
,##F G

movimiento##H R
.##R S
MonedaPlural##S _
(##_ `
)##` a
)##a b
,##b c
Observacion$$ 
=$$ 

movimiento$$ (
.$$( )

Comentario$$) 3
,$$3 4
MostrarLogo%% 
=%% *
FacturacionElectronicaSettings%% <
.%%< =
Default%%= D
.%%D E+
MostrarLogoEnComprobanteImpreso%%E d
,%%d e
MostrarTestigo&& 
=&&  
AplicacionSettings&&! 3
.&&3 4
Default&&4 ;
.&&; <"
MostrarCabeceraVoucher&&< R
,&&R S
MensajeNegocio'' 
=''  
AplicacionSettings''! 3
.''3 4
Default''4 ;
.''; <
MensajeDeNegocio''< L
,''L M#
DetallesDeIngresoEgreso(( '
=((( )"
DetalleDeIngresoEgreso((* @
.((@ A
Convert((A H
(((H I

movimiento((I S
.((S T
Detalles((T \
(((\ ]
)((] ^
)((^ _
,((_ `
Caja** 
=** 

movimiento** !
.**! "
Caja**" &
(**& '
)**' (
.**( )
RazonSocial**) 4
,**4 5
Cajero++ 
=++ 

movimiento++ #
.++# $
Cajero++$ *
(++* +
)+++ ,
.++, -
NombreCompleto++- ;
,++; < 
TipoPagadorRecibidor,, $
=,,% &

movimiento,,' 1
.,,1 2
	EsIngreso,,2 ;
(,,; <
),,< =
?,,> ?
$str,,@ M
:,,N O
$str,,P Z
,,,Z [
MedioDePago-- 
=-- 

movimiento-- (
.--( )
TrazaDePago--) 4
(--4 5
)--5 6
.--6 7
MedioDePago--7 B
(--B C
)--C D
.--D E
nombre--E K
,--K L
EntidadBancaria.. 
=..  !

movimiento.." ,
..., -
TrazaDePago..- 8
(..8 9
)..9 :
...: ;
EntidadBancaria..; J
(..J K
)..K L
...L M
nombre..M S
,..S T
InformacionDePago// !
=//" #

movimiento//$ .
.//. /
TrazaDePago/// :
(//: ;
)//; <
.//< =
Informacion//= H
,//H I
}00 
;00
}11 	
}22 
public44 

class44 "
DetalleDeIngresoEgreso44 '
{55 
public66 
string66 
Comprobante66 !
{66" #
get66$ '
;66' (
set66) ,
;66, -
}66. /
public77 
string77 
CodigoCuota77 !
{77" #
get77$ '
;77' (
set77) ,
;77, -
}77. /
public88 
decimal88 
Importe88 
{88  
get88! $
;88$ %
set88& )
;88) *
}88+ ,
public99 "
DetalleDeIngresoEgreso99 %
(99% &&
DetalleMovimientoEconomico99& @
detalle99A H
)99H I
{:: 	
this;; 
.;; 
Comprobante;; 
=;; 
detalle;; &
.;;& '
Cuota;;' ,
(;;, -
);;- .
.;;. /

(;;< =
);;= >
.;;> ?
Comprobante;;? J
(;;J K
);;K L
.;;L M

+;;[ \
$str;;] b
+;;c d
detalle;;e l
.;;l m
Cuota;;m r
(;;r s
);;s t
.;;t u

(
;;� �
)
;;� �
.
;;� �
Comprobante
;;� �
(
;;� �
)
;;� �
.
;;� �!
NumeroDeComprobante
;;� �
;
;;� �
this<< 
.<< 
CodigoCuota<< 
=<< 
detalle<< &
.<<& '
Cuota<<' ,
(<<, -
)<<- .
.<<. /
Codigo<</ 5
;<<5 6
this== 
.== 
Importe== 
=== 
detalle== "
.==" #
Importe==# *
;==* +
}>> 	
internal?? 
static?? 
List?? 
<?? "
DetalleDeIngresoEgreso?? 3
>??3 4
Convert??5 <
(??< =
List??= A
<??A B&
DetalleMovimientoEconomico??B \
>??\ ]
detalles??^ f
)??f g
{@@ 	
ListAA 
<AA "
DetalleDeIngresoEgresoAA '
>AA' (
	resultadoAA) 2
=AA3 4
newAA5 8
ListAA9 =
<AA= >"
DetalleDeIngresoEgresoAA> T
>AAT U
(AAU V
)AAV W
;AAW X
foreachBB 
(BB 
varBB 
itemBB 
inBB  
detallesBB! )
)BB) *
{CC 
	resultadoDD 
.DD 
AddDD 
(DD 
newDD !"
DetalleDeIngresoEgresoDD" 8
(DD8 9
itemDD9 =
)DD= >
)DD> ?
;DD? @
}EE 
returnFF 
	resultadoFF 
;FF 
}GG 	
}HH 
}II �M
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeCredito.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 

:  %
DocumentoElectronicoVenta! :
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: W
;W X
public 
string 
TipoDeNotaCredito '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 

Referencia 
Discrepancia &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Relacionado  
DocumentoRelacionado /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 

( 
OperacionDeVenta -
	operacion. 7
,7 8-
!EstablecimientoComercialExtendido9 Z
sede[ _
,_ `.
!EstablecimientoComercialExtendido	a �
establecimiento
� �
,
� �
List
� �
<
� �
Detalle_maestro
� �
>
� �"
tiposDeNotaDeCredito
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
	operacion
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TipoDeNotaCredito 
=  
tiposDeNotaDeCredito  4
.4 5
SingleOrDefault5 D
(D E
tE F
=>G I
tJ K
.K L
codigoL R
==S U
	operacionV _
._ `$
CodigoSunatDeTransaccion` x
(x y
)y z
)z {
.{ |
nombre	| �
;
� �
Discrepancia 
= 
new 

Referencia )
{ 

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

+^ _
$str` c
+d e
	operacionf o
.o p"
OperacionDeReferencia	p �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �
Tipo 
= 
	operacion  
.  !$
CodigoSunatDeTransaccion! 9
(9 :
): ;
,; <
Descripcion 
= 
	operacion '
.' (

Comentario( 2
,2 3
} 
;
DocumentoRelacionado  
=! "
new# &
Relacionado' 2
{ 
NroDocumento 
= 
	operacion (
.( )!
OperacionDeReferencia) >
(> ?
)? @
.@ A
ComprobanteA L
(L M
)M N
.N O

+] ^
$str_ b
+c d
	operacione n
.n o"
OperacionDeReferencia	o �
(
� �
)
� �
.
� �
Comprobante
� �
(
� �
)
� �
.
� �!
NumeroDeComprobante
� �
,
� �

= 
	operacion  )
.) *!
OperacionDeReferencia* ?
(? @
)@ A
.A B
ComprobanteB M
(M N
)N O
.O P

CodigoTipoP Z
,Z [
}   
;  
}!! 	
public## 

(## 
OperacionDeVenta## -
	operacion##. 7
,##7 84
(EstablecimientoComercialExtendidoConLogo##9 a
sede##b f
,##f g.
!EstablecimientoComercialExtendido	##h �
establecimiento
##� �
,
##� �
List
##� �
<
##� �
Detalle_maestro
##� �
>
##� �"
tiposDeNotaDeCredito
##� �
,
##� �
byte
##� �
[
##� �
]
##� �
qrBytes
##� �
,
##� �
bool
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �.
 ModoImpresionCaracteristicasEnum
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
:
##� �
base
##� �
(
##� �
	operacion
##� �
,
##� �
sede
##� �
,
##� �
establecimiento
##� �
,
##� �
qrBytes
##� �
,
##� �&
mostrarEncabezadoTestigo
##� �
,
##� �*
modoImpresionCaracteristicas
##� �
)
##� �
{$$ 	
TipoDeNotaCredito%% 
=%%  
tiposDeNotaDeCredito%%  4
.%%4 5
SingleOrDefault%%5 D
(%%D E
t%%E F
=>%%G I
t%%J K
.%%K L
codigo%%L R
==%%S U
	operacion%%V _
.%%_ `$
CodigoSunatDeTransaccion%%` x
(%%x y
)%%y z
)%%z {
.%%{ |
nombre	%%| �
;
%%� �
Discrepancia&& 
=&& 
new&& 

Referencia&& )
{'' 

=(( 
	operacion((  )
.(() *!
OperacionDeReferencia((* ?
(((? @
)((@ A
.((A B
Comprobante((B M
(((M N
)((N O
.((O P

+((^ _
$str((` c
+((d e
	operacion((f o
.((o p"
OperacionDeReferencia	((p �
(
((� �
)
((� �
.
((� �
Comprobante
((� �
(
((� �
)
((� �
.
((� �!
NumeroDeComprobante
((� �
,
((� �
Tipo)) 
=)) 
	operacion))  
.))  !$
CodigoSunatDeTransaccion))! 9
())9 :
))): ;
,)); <
Descripcion** 
=** 
	operacion** '
.**' (

Comentario**( 2
,**2 3
}++ 
;++
DocumentoRelacionado,,  
=,,! "
new,,# &
Relacionado,,' 2
{-- 
NroDocumento.. 
=.. 
	operacion.. (
...( )!
OperacionDeReferencia..) >
(..> ?
)..? @
...@ A
Comprobante..A L
(..L M
)..M N
...N O

+..] ^
$str.._ b
+..c d
	operacion..e n
...n o"
OperacionDeReferencia	..o �
(
..� �
)
..� �
.
..� �
Comprobante
..� �
(
..� �
)
..� �
.
..� �!
NumeroDeComprobante
..� �
,
..� �

=// 
	operacion//  )
.//) *!
OperacionDeReferencia//* ?
(//? @
)//@ A
.//A B
Comprobante//B M
(//M N
)//N O
.//O P

CodigoTipo//P Z
,//Z [
}00 
;00
}11 	
public33 
static33 
List33 
<33 

>33( )
Convert33* 1
(331 2
List332 6
<336 7
OperacionDeVenta337 G
>33G H
operaciones33I T
,33T U-
!EstablecimientoComercialExtendido33V w
sede33x |
,33| }
List	33~ �
<
33� �
Detalle_maestro
33� �
>
33� �"
tiposDeNotaDeCredito
33� �
,
33� �.
 ModoImpresionCaracteristicasEnum
33� �*
modoImpresionCaracteristicas
33� �
)
33� �
{44 	
List55 
<55 

>55 
	resultado55  )
=55* +
new55, /
List550 4
<554 5

>55B C
(55C D
)55D E
;55E F
foreach66 
(66 
var66 
item66 
in66  
operaciones66! ,
)66, -
{77 
	resultado88 
.88 
Add88 
(88 
new88 !

(88/ 0
item880 4
,884 5
sede886 :
,88: ;
new88< ?4
(EstablecimientoComercialExtendidoConLogo88@ h
(88h i
item88i m
.88m n
Transaccion88n y
(88y z
)88z {
.88{ |
Actor_negocio2	88| �
.
88� �
Actor_negocio2
88� �
)
88� �
,
88� �"
tiposDeNotaDeCredito
88� �
,
88� �
null
88� �
,
88� �
false
88� �
,
88� �*
modoImpresionCaracteristicas
88� �
)
88� �
)
88� �
;
88� �
}99 
return:: 
	resultado:: 
;:: 
};; 	
}<< 
}== �-
cD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\NotaDeVenta.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
NotaDeVenta 
: %
DocumentoElectronicoVenta 8
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: I
;I J
public 
bool )
MostrarInformacionAdicional80 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicional80 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
bool )
MostrarInformacionAdicionalA4 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicionalA4 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
NotaDeVenta 
( 
) 
{ 	
} 	
public 
NotaDeVenta 
( 
OrdenDeVenta '
orden( -
,- .4
(EstablecimientoComercialExtendidoConLogo/ W
sedeX \
,\ ]-
!EstablecimientoComercialExtendido^ 
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
if 
( 
AplicacionSettings "
." #
Default# *
.* +6
*PermitirCambioNombreComprobanteNotaDeVenta+ U
)U V
{ 

NombreTipo 
= 
orden "
." #
Comprobante# .
(. /
)/ 0
.0 1

NombreTipo1 ;
;; <
} 
ResolucionAutorizacionSunat '
=( )
$str* ,
;, -)
MostrarInformacionAdicional80 )
=* +
!, -
String- 3
.3 4

(A B
VentasSettingsB P
.P Q
DefaultQ X
.X Y+
InformacionAdicionalNotaVenta80Y x
)x y
;y z"
InformacionAdicional80   "
=  # $
VentasSettings  % 3
.  3 4
Default  4 ;
.  ; <+
InformacionAdicionalNotaVenta80  < [
;  [ \)
MostrarInformacionAdicionalA4!! )
=!!* +
!!!, -
String!!- 3
.!!3 4

(!!A B
VentasSettings!!B P
.!!P Q
Default!!Q X
.!!X Y+
InformacionAdicionalNotaVentaA4!!Y x
)!!x y
;!!y z"
InformacionAdicionalA4"" "
=""# $
VentasSettings""% 3
.""3 4
Default""4 ;
.""; <+
InformacionAdicionalNotaVentaA4""< [
;""[ \
	FormaPago## 
=## 
orden## 
.## 

ModoDePago## (
(##( )
)##) *
==##+ -
ModoPago##. 6
.##6 7
Contado##7 >
?##? @
	Enumerado##A J
.##J K
GetDescription##K Y
(##Y Z

.##g h
Contado##h o
)##o p
:##q r
	Enumerado##s |
.##| }
GetDescription	##} �
(
##� �

##� �
.
##� �
Credito
##� �
)
##� �
;
##� �

=$$ 
orden$$ !
.$$! "

ModoDePago$$" ,
($$, -
)$$- .
==$$/ 1
ModoPago$$2 :
.$$: ;
Contado$$; B
?$$C D
$num$$E F
:$$G H
orden$$I N
.$$N O
Cuotas$$O U
($$U V
)$$V W
.$$W X
Sum$$X [
($$[ \
c$$\ ]
=>$$^ `
c$$a b
.$$b c
total$$c h
)$$h i
-$$j k
orden$$l q
.$$q r
PagoEnFechaEmision	$$r �
(
$$� �
)
$$� �
;
$$� �
Cuotas%% 
=%% 
orden%% 
.%% 

ModoDePago%% %
(%%% &
)%%& '
==%%( *
ModoPago%%+ 3
.%%3 4
Contado%%4 ;
?%%< =
new%%> A
List%%B F
<%%F G
DetalleCuota%%G S
>%%S T
(%%T U
)%%U V
:%%W X
DetalleCuota%%Y e
.%%e f
Convert%%f m
(%%m n
orden%%n s
.%%s t
Cuotas%%t z
(%%z {
)%%{ |
,%%| }
orden	%%~ �
.
%%� �

ModoDePago
%%� �
(
%%� �
)
%%� �
,
%%� �
orden
%%� �
.
%%� � 
PagoEnFechaEmision
%%� �
(
%%� �
)
%%� �
)
%%� �
;
%%� �
}&& 	
}OO 
}PP �Z
_D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\Factura.cs
	namespace

 	
Tsp


 
.


Sigescom

 
.

 
Modelo

 
.

 
	Entidades

 '
.

' (
ComprobantesModel

( 9
{ 
public 

class 
Factura 
: %
DocumentoElectronicoVenta 4
{
public 
override 
string 

NombreTipo )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
$str: O
;O P
public 
bool 
TieneGuiaDeRemision '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
List 
< 
Relacionado 
>  
GuiasDeRemision! 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
bool )
MostrarInformacionAdicional80 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicional80 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
bool )
MostrarInformacionAdicionalA4 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
string "
InformacionAdicionalA4 ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
Factura 
( 
) 
{ 	
}
 
public 
Factura 
( 
OrdenDeVenta #
orden$ )
,) *-
!EstablecimientoComercialExtendido+ L
sedeM Q
,Q R-
!EstablecimientoComercialExtendidoS t
establecimiento	u �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
orden
� �
,
� �
sede
� �
,
� �
establecimiento
� �
,
� �
qrBytes
� �
,
� �&
mostrarEncabezadoTestigo
� �
,
� �*
modoImpresionCaracteristicas
� �
)
� �
{ 	
TieneGuiaDeRemision 
=  !
orden" '
.' (
TieneGuiaDeRemision( ;
(; <
)< =
;= >)
MostrarInformacionAdicional80"" )
=""* +
!"", -
String""- 3
.""3 4

(""A B
VentasSettings""B P
.""P Q
Default""Q X
.""X Y)
InformacionAdicionalFactura80""Y v
)""v w
;""w x"
InformacionAdicional80## "
=### $
VentasSettings##% 3
.##3 4
Default##4 ;
.##; <)
InformacionAdicionalFactura80##< Y
;##Y Z)
MostrarInformacionAdicionalA4$$ )
=$$* +
!$$, -
String$$- 3
.$$3 4

($$A B
VentasSettings$$B P
.$$P Q
Default$$Q X
.$$X Y)
InformacionAdicionalFacturaA4$$Y v
)$$v w
;$$w x"
InformacionAdicionalA4%% "
=%%# $
VentasSettings%%% 3
.%%3 4
Default%%4 ;
.%%; <)
InformacionAdicionalFacturaA4%%< Y
;%%Y Z
	FormaPago&& 
=&& 
orden&& 
.&& 

ModoDePago&& (
(&&( )
)&&) *
==&&+ -
ModoPago&&. 6
.&&6 7
Contado&&7 >
?&&? @
	Enumerado&&A J
.&&J K
GetDescription&&K Y
(&&Y Z

.&&g h
Contado&&h o
)&&o p
:&&q r
	Enumerado&&s |
.&&| }
GetDescription	&&} �
(
&&� �

&&� �
.
&&� �
Credito
&&� �
)
&&� �
;
&&� �

='' 
orden'' !
.''! "

ModoDePago''" ,
('', -
)''- .
==''/ 1
ModoPago''2 :
.'': ;
Contado''; B
?''C D
$num''E F
:''G H
orden''I N
.''N O
Cuotas''O U
(''U V
)''V W
.''W X
Sum''X [
(''[ \
c''\ ]
=>''^ `
c''a b
.''b c
total''c h
)''h i
-''j k
orden''l q
.''q r
PagoEnFechaEmision	''r �
(
''� �
)
''� �
;
''� �
Cuotas(( 
=(( 
orden(( 
.(( 

ModoDePago(( %
(((% &
)((& '
==((( *
ModoPago((+ 3
.((3 4
Contado((4 ;
?((< =
new((> A
List((B F
<((F G
DetalleCuota((G S
>((S T
(((T U
)((U V
:((W X
DetalleCuota((Y e
.((e f
Convert((f m
(((m n
orden((n s
.((s t
Cuotas((t z
(((z {
)(({ |
,((| }
orden	((~ �
.
((� �

ModoDePago
((� �
(
((� �
)
((� �
,
((� �
orden
((� �
.
((� � 
PagoEnFechaEmision
((� �
(
((� �
)
((� �
)
((� �
;
((� �
})) 	
public** 
Factura** 
(** 
OrdenDeVenta** #
orden**$ )
,**) *4
(EstablecimientoComercialExtendidoConLogo**+ S
sede**T X
,**X Y-
!EstablecimientoComercialExtendido**Z {
establecimiento	**| �
,
**� �
byte
**� �
[
**� �
]
**� �
qrBytes
**� �
,
**� �
bool
**� �&
mostrarEncabezadoTestigo
**� �
,
**� �.
 ModoImpresionCaracteristicasEnum
**� �*
modoImpresionCaracteristicas
**� �
)
**� �
:
**� �
base
**� �
(
**� �
orden
**� �
,
**� �
sede
**� �
,
**� �
establecimiento
**� �
,
**� �
qrBytes
**� �
,
**� �&
mostrarEncabezadoTestigo
**� �
,
**� �*
modoImpresionCaracteristicas
**� �
)
**� �
{++ 	
TieneGuiaDeRemision,, 
=,,  !
orden,," '
.,,' (
TieneGuiaDeRemision,,( ;
(,,; <
),,< =
;,,= >)
MostrarInformacionAdicional8000 )
=00* +
!00, -
String00- 3
.003 4

(00A B
VentasSettings00B P
.00P Q
Default00Q X
.00X Y)
InformacionAdicionalFactura8000Y v
)00v w
;00w x"
InformacionAdicional8011 "
=11# $
VentasSettings11% 3
.113 4
Default114 ;
.11; <)
InformacionAdicionalFactura8011< Y
;11Y Z)
MostrarInformacionAdicionalA422 )
=22* +
!22, -
String22- 3
.223 4

(22A B
VentasSettings22B P
.22P Q
Default22Q X
.22X Y)
InformacionAdicionalFacturaA422Y v
)22v w
;22w x"
InformacionAdicionalA433 "
=33# $
VentasSettings33% 3
.333 4
Default334 ;
.33; <)
InformacionAdicionalFacturaA433< Y
;33Y Z
	FormaPago44 
=44 
orden44 
.44 

ModoDePago44 (
(44( )
)44) *
==44+ -
ModoPago44. 6
.446 7
Contado447 >
?44? @
	Enumerado44A J
.44J K
GetDescription44K Y
(44Y Z

.44g h
Contado44h o
)44o p
:44q r
	Enumerado44s |
.44| }
GetDescription	44} �
(
44� �

44� �
.
44� �
Credito
44� �
)
44� �
;
44� �

=55 
orden55 !
.55! "

ModoDePago55" ,
(55, -
)55- .
==55/ 1
ModoPago552 :
.55: ;
Contado55; B
?55C D
$num55E F
:55G H
orden55I N
.55N O
Cuotas55O U
(55U V
)55V W
.55W X
Sum55X [
(55[ \
c55\ ]
=>55^ `
c55a b
.55b c
total55c h
)55h i
-55j k
orden55l q
.55q r
PagoEnFechaEmision	55r �
(
55� �
)
55� �
;
55� �
Cuotas66 
=66 
orden66 
.66 

ModoDePago66 %
(66% &
)66& '
==66( *
ModoPago66+ 3
.663 4
Contado664 ;
?66< =
new66> A
List66B F
<66F G
DetalleCuota66G S
>66S T
(66T U
)66U V
:66W X
DetalleCuota66Y e
.66e f
Convert66f m
(66m n
orden66n s
.66s t
Cuotas66t z
(66z {
)66{ |
,66| }
orden	66~ �
.
66� �

ModoDePago
66� �
(
66� �
)
66� �
,
66� �
orden
66� �
.
66� � 
PagoEnFechaEmision
66� �
(
66� �
)
66� �
)
66� �
;
66� �
}77 	
public99 
static99 
List99 
<99 
Factura99 "
>99" #
Convert99$ +
(99+ ,
List99, 0
<990 1
OrdenDeVenta991 =
>99= >
ordenes99? F
,99F G-
!EstablecimientoComercialExtendido99H i
sede99j n
,99n o-
 ModoImpresionCaracteristicasEnum	99p �*
modoImpresionCaracteristicas
99� �
)
99� �
{:: 	
List;; 
<;; 
Factura;; 
>;; 
	resultado;; #
=;;$ %
new;;& )
List;;* .
<;;. /
Factura;;/ 6
>;;6 7
(;;7 8
);;8 9
;;;9 :
foreach<< 
(<< 
var<< 
orden<< 
in<< !
ordenes<<" )
)<<) *
{== 
	resultado>> 
.>> 
Add>> 
(>> 
new>> !
Factura>>" )
(>>) *
orden>>* /
,>>/ 0
sede>>1 5
,>>5 6
new>>6 94
(EstablecimientoComercialExtendidoConLogo>>: b
(>>b c
orden>>c h
.>>h i
Transaccion>>i t
(>>t u
)>>u v
.>>v w
Actor_negocio2	>>w �
.
>>� �
Actor_negocio2
>>� �
)
>>� �
,
>>� �
null
>>� �
,
>>� �
false
>>� �
,
>>� �*
modoImpresionCaracteristicas
>>� �
)
>>� �
)
>>� �
;
>>� �
}?? 
return@@ 
	resultado@@ 
;@@ 
}AA 	
}BB 
}CC ��
lD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Modelo\ClasesNegocio\Core\Comprobante\Templates\DocumentoElectronico.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Modelo 
. 
	Entidades '
.' (
ComprobantesModel( 9
{
public 

class %
DocumentoElectronicoVenta *
:+ ,'
DocumentoElectronicoImpreso- H
{ 
public 
decimal 
Igv 
{ 
get  
;  !
set" %
;% &
}' (
public 
decimal 
Icbper 
{ 
get  #
;# $
set% (
;( )
}* +
public 
decimal 
ValorIcbper "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
bool 
MostrarEmpleado #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
EtiquetaEmpleado &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
NombreEmpleado $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
bool 

{" #
get$ '
;' (
set) ,
;, -
}. /
public 
int 

{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
PuntosAcumulados #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public %
DocumentoElectronicoVenta (
(( )
)) *
{ 	
}
 
public %
DocumentoElectronicoVenta (
(( )
OperacionDeVenta) 9
	operacion: C
,C D-
!EstablecimientoComercialExtendidoE f
sedeg k
,k l.
!EstablecimientoComercialExtendido	m �
establecimiento
� �
,
� �
byte
� �
[
� �
]
� �
qrBytes
� �
,
� �
bool
� �&
mostrarEncabezadoTestigo
� �
,
� �.
 ModoImpresionCaracteristicasEnum
� �*
modoImpresionCaracteristicas
� �
)
� �
:
� �
base
� �
(
� �
)
� �
{ 	
IdOrden 
= 
	operacion 
.  
Id  "
;" #
FechaEmision   
=   
	operacion   $
.  $ %
FechaEmision  % 1
;  1 2
Emisor!! 
=!! 
new!! 
Emisor!! 
(!!  
sede!!  $
,!!$ %
establecimiento!!& 5
)!!5 6
;!!6 7
Receptor"" 
="" 
new"" 
Receptor"" #
(""# $
	operacion""$ -
.""- .
Cliente"". 5
(""5 6
)""6 7
,""7 8
	operacion""9 B
.""B C
AliasCliente""C O
(""O P
)""P Q
)""Q R
;""R S
Detalles## 
=## 
Detalle## 
.## 
Convert## &
(##& '
	operacion##' 0
.##0 1
Detalles##1 9
(##9 :
)##: ;
,##; <(
modoImpresionCaracteristicas##= Y
,##Y Z
	operacion##[ d
.##d e
DetalleUnificado##e u
(##u v
)##v w
,##w x
true##y }
)##} ~
;##~ "
MostrarMensajeAmazonia$$ "
=$$# $
	operacion$$% .
.$$. /
AplicaLeyDeAmazonia$$/ B
;$$B C!
MostrarMensajeNegocio%% !
=%%" #
AplicacionSettings%%$ 6
.%%6 7
Default%%7 >
.%%> ?#
MostrarMensajeDeNegocio%%? V
;%%V W
MensajeNegocio&& 
=&& 
AplicacionSettings&& /
.&&/ 0
Default&&0 7
.&&7 8
MensajeDeNegocio&&8 H
;&&H I
Observacion'' 
='' 
	operacion'' #
.''# $
Observacion''$ /
(''/ 0
)''0 1
??''2 4
$str''5 8
;''8 9
MostrarLogo(( 
=(( *
FacturacionElectronicaSettings(( 8
.((8 9
Default((9 @
.((@ A+
MostrarLogoEnComprobanteImpreso((A `
;((` a.
"MostrarTrazabilidadConceptoNegocio)) .
=))/ 0
VentasSettings))1 ?
.))? @
Default))@ G
.))G H2
&PermitirRegistroDeLoteEnDetalleDeVenta))H n
;))n o
CodigoQR** 
=** 
qrBytes** 
;** 
CodigoSunatMoneda++ 
=++ 
	operacion++  )
.++) *
Moneda++* 0
(++0 1
)++1 2
.++2 3
Codigo++3 9
;++9 :
CodigoSunatTipo,, 
=,, 
	operacion,, '
.,,' (
Comprobante,,( 3
(,,3 4
),,4 5
.,,5 6

CodigoTipo,,6 @
;,,@ A
Serie-- 
=-- 
	operacion-- 
.-- 
Comprobante-- )
(--) *
)--* +
.--+ ,

;--9 :
Numero.. 
=.. 
	operacion.. 
... 
Comprobante.. *
(..* +
)..+ ,
..., -
NumeroDeComprobante..- @
;..@ A
ImporteTotal// 
=// 
	operacion// $
.//$ %
Total//% *
;//* +%
ImporteOperacionExonerada00 %
=00& '
	operacion00( 1
.001 2*
ImporteTotalOperacionExonerada002 P
;00P Q$
ImporteOperacionInafecta11 $
=11% &
	operacion11' 0
.110 1)
ImporteTotalOperacionInafecta111 N
;11N O#
ImporteOperacionGravada22 #
=22$ %
	operacion22& /
.22/ 0)
BaseImponibleOperacionGravada220 M
;22M N 
ImporteTotalEnLetras33  
=33! "
Util33# '
.33' (
	APalabras33( 1
(331 2
	operacion332 ;
.33; <
Total33< A
,33A B
	operacion33C L
.33L M
MonedaPlural33M Y
(33Y Z
)33Z [
)33[ \
;33\ ]
	Descuento44 
=44 
	operacion44 !
.44! "
	Descuento44" +
(44+ ,
)44, -
;44- .
Igv55 
=55 
	operacion55 
.55 
Igv55 
(55  
)55  !
;55! "
Icbper66 
=66 
	operacion66 
.66 
Icbper66 %
(66% &
)66& '
;66' (
ValorIcbper77 
=77 
	operacion77 #
.77# $
ValorIcbper77$ /
(77/ 0
)770 1
;771 2
MostrarTestigo88 
=88 $
mostrarEncabezadoTestigo88 5
;885 6'
ResolucionAutorizacionSunat99 '
=99( )*
FacturacionElectronicaSettings99* H
.99H I
Default99I P
.99P Q(
ResolucionEmisionElectronica99Q m
;99m n
IdEstadoActual:: 
=:: 
	operacion:: &
.::& '
IdEstadoActual::' 5
;::5 6
MostrarEmpleado;; 
=;; 
VentasSettings;; ,
.;;, -
Default;;- 4
.;;4 5/
#MostrarEmpleadoEnComprobanteDeVenta;;5 X
;;;X Y
EtiquetaEmpleado<< 
=<< 
VentasSettings<< -
.<<- .
Default<<. 5
.<<5 60
$EtiquetaEmpleadoEnComprobanteDeVenta<<6 Z
;<<Z [
NombreEmpleado== 
=== 
	operacion== &
.==& '
Empleado==' /
(==/ 0
)==0 1
.==1 2
Nombres==2 9
+==: ;
$str==< ?
+==@ A
	operacion==B K
.==K L
Empleado==L T
(==T U
)==U V
.==V W
ApellidoPaterno==W f
;==f g
MostrarPlaca>> 
=>> 
VentasSettings>> )
.>>) *
Default>>* 1
.>>1 2*
PermitirRegistroDePlacaEnVenta>>2 P
&&>>Q S
!>>T U
string>>U [
.>>[ \

(>>i j
	operacion>>j s
.>>s t
Detalles>>t |
(>>| }
)>>} ~
.>>~ 
First	>> �
(
>>� �
)
>>� �
.
>>� �
Registro
>>� �
)
>>� �
;
>>� �
Placa?? 
=?? 
	operacion?? 
.?? 
Detalles?? &
(??& '
)??' (
.??( )
First??) .
(??. /
)??/ 0
.??0 1
Registro??1 9
;??9 :
MostrarInformacion@@ 
=@@  
!@@! "
string@@" (
.@@( )

(@@6 7
	operacion@@7 @
.@@@ A
Informacion@@A L
)@@L M
;@@M N
InformacionAA 
=AA 
	operacionAA #
.AA# $
InformacionAA$ /
;AA/ 0

=BB 
VentasSettingsBB *
.BB* +
DefaultBB+ 2
.BB2 3!
GenerarPuntosEnVentasBB3 H
&&BBI K
	operacionBBL U
.BBU V
	IdClienteBBV _
!=BB` b

.BBp q
DefaultBBq x
.BBx y
IdClienteGenerico	BBy �
;
BB� �

=CC 
	operacionCC %
.CC% &

(CC3 4
)CC4 5
;CC5 6
PuntosAcumuladosDD 
=DD 
	operacionDD (
.DD( )
PuntosAcumuladosDD) 9
(DD9 :
)DD: ;
;DD; <
EsInvalidadaEE 
=EE 
	operacionEE $
.EE$ %
EsInvalidadaEE% 1
;EE1 2
MotivoInvalidacionFF 
=FF  
	operacionFF! *
.FF* +
EsInvalidadaFF+ 7
?FF8 9
	operacionFF: C
.FFC D
MotivoInvalidacionFFD V
(FFV W
)FFW X
:FFY Z
$strFF[ ]
;FF] ^
}GG 	
publicKK %
DocumentoElectronicoVentaKK ,
(KK, -
OperacionDeVentaKK- =
	operacionKK> G
,KKG H4
(EstablecimientoComercialExtendidoConLogoKKI q
sedeKKr v
,KKv w.
!EstablecimientoComercialExtendido	KKx �
establecimiento
KK� �
,
KK� �
byte
KK� �
[
KK� �
]
KK� �
qrBytes
KK� �
,
KK� �
bool
KK� �&
mostrarEncabezadoTestigo
KK� �
,
KK� �.
 ModoImpresionCaracteristicasEnum
KK� �*
modoImpresionCaracteristicas
KK� �
)
KK� �
:
KK� �
base
KK� �
(
KK� �
)
KK� �
{LL 	
IdOrdenMM 
=MM 
	operacionMM 
.MM  
IdMM  "
;MM" #
FechaEmisionNN 
=NN 
	operacionNN $
.NN$ %
FechaEmisionNN% 1
;NN1 2
EmisorOO 
=OO 
newOO 
EmisorOO 
(OO  
sedeOO  $
,OO$ %
establecimientoOO& 5
)OO5 6
;OO6 7
ReceptorPP 
=PP 
newPP 
ReceptorPP #
(PP# $
	operacionPP$ -
.PP- .
ClientePP. 5
(PP5 6
)PP6 7
,PP7 8
	operacionPP9 B
.PPB C
AliasClientePPC O
(PPO P
)PPP Q
)PPQ R
;PPR S
DetallesQQ 
=QQ 
DetalleQQ 
.QQ 
ConvertQQ &
(QQ& '
	operacionQQ' 0
.QQ0 1
DetallesQQ1 9
(QQ9 :
)QQ: ;
,QQ; <(
modoImpresionCaracteristicasQQ= Y
,QQY Z
	operacionQQ[ d
.QQd e
DetalleUnificadoQQe u
(QQu v
)QQv w
,QQw x
trueQQy }
)QQ} ~
;QQ~ "
MostrarMensajeAmazoniaRR "
=RR# $
	operacionRR% .
.RR. /
AplicaLeyDeAmazoniaRR/ B
;RRB C!
MostrarMensajeNegocioSS !
=SS" #
AplicacionSettingsSS$ 6
.SS6 7
DefaultSS7 >
.SS> ?#
MostrarMensajeDeNegocioSS? V
;SSV W
MensajeNegocioTT 
=TT 
AplicacionSettingsTT /
.TT/ 0
DefaultTT0 7
.TT7 8
MensajeDeNegocioTT8 H
;TTH I
ObservacionUU 
=UU 
	operacionUU #
.UU# $
ObservacionUU$ /
(UU/ 0
)UU0 1
??UU2 4
$strUU5 8
;UU8 9
MostrarLogoVV 
=VV *
FacturacionElectronicaSettingsVV 8
.VV8 9
DefaultVV9 @
.VV@ A+
MostrarLogoEnComprobanteImpresoVVA `
;VV` a.
"MostrarTrazabilidadConceptoNegocioWW .
=WW/ 0
VentasSettingsWW1 ?
.WW? @
DefaultWW@ G
.WWG H2
&PermitirRegistroDeLoteEnDetalleDeVentaWWH n
;WWn o
CodigoQRXX 
=XX 
qrBytesXX 
;XX 
CodigoSunatMonedaYY 
=YY 
	operacionYY  )
.YY) *
MonedaYY* 0
(YY0 1
)YY1 2
.YY2 3
CodigoYY3 9
;YY9 :
CodigoSunatTipoZZ 
=ZZ 
	operacionZZ '
.ZZ' (
ComprobanteZZ( 3
(ZZ3 4
)ZZ4 5
.ZZ5 6

CodigoTipoZZ6 @
;ZZ@ A
Serie[[ 
=[[ 
	operacion[[ 
.[[ 
Comprobante[[ )
([[) *
)[[* +
.[[+ ,

;[[9 :
Numero\\ 
=\\ 
	operacion\\ 
.\\ 
Comprobante\\ *
(\\* +
)\\+ ,
.\\, -
NumeroDeComprobante\\- @
;\\@ A
ImporteTotal]] 
=]] 
	operacion]] $
.]]$ %
Total]]% *
;]]* +%
ImporteOperacionExonerada^^ %
=^^& '
	operacion^^( 1
.^^1 2*
ImporteTotalOperacionExonerada^^2 P
;^^P Q$
ImporteOperacionInafecta__ $
=__% &
	operacion__' 0
.__0 1)
ImporteTotalOperacionInafecta__1 N
;__N O#
ImporteOperacionGravada`` #
=``$ %
	operacion``& /
.``/ 0)
BaseImponibleOperacionGravada``0 M
;``M N 
ImporteTotalEnLetrasaa  
=aa! "
Utilaa# '
.aa' (
	APalabrasaa( 1
(aa1 2
	operacionaa2 ;
.aa; <
Totalaa< A
,aaA B
	operacionaaC L
.aaL M
MonedaPluralaaM Y
(aaY Z
)aaZ [
)aa[ \
;aa\ ]
	Descuentobb 
=bb 
	operacionbb !
.bb! "
	Descuentobb" +
(bb+ ,
)bb, -
;bb- .
Igvcc 
=cc 
	operacioncc 
.cc 
Igvcc 
(cc  
)cc  !
;cc! "
Icbperdd 
=dd 
	operaciondd 
.dd 
Icbperdd %
(dd% &
)dd& '
;dd' (
ValorIcbperee 
=ee 
	operacionee #
.ee# $
ValorIcbperee$ /
(ee/ 0
)ee0 1
;ee1 2
MostrarTestigoff 
=ff $
mostrarEncabezadoTestigoff 5
;ff5 6'
ResolucionAutorizacionSunatgg '
=gg( )*
FacturacionElectronicaSettingsgg* H
.ggH I
DefaultggI P
.ggP Q(
ResolucionEmisionElectronicaggQ m
;ggm n
IdEstadoActualhh 
=hh 
	operacionhh &
.hh& '
IdEstadoActualhh' 5
;hh5 6
MostrarEmpleadoii 
=ii 
VentasSettingsii ,
.ii, -
Defaultii- 4
.ii4 5/
#MostrarEmpleadoEnComprobanteDeVentaii5 X
;iiX Y
EtiquetaEmpleadojj 
=jj 
VentasSettingsjj -
.jj- .
Defaultjj. 5
.jj5 60
$EtiquetaEmpleadoEnComprobanteDeVentajj6 Z
;jjZ [
NombreEmpleadokk 
=kk 
	operacionkk &
.kk& '
Empleadokk' /
(kk/ 0
)kk0 1
.kk1 2
Nombreskk2 9
+kk: ;
$strkk< ?
+kk@ A
	operacionkkB K
.kkK L
EmpleadokkL T
(kkT U
)kkU V
.kkV W
ApellidoPaternokkW f
;kkf g
MostrarPlacall 
=ll 
VentasSettingsll )
.ll) *
Defaultll* 1
.ll1 2*
PermitirRegistroDePlacaEnVentall2 P
&&llQ S
!llT U
stringllU [
.ll[ \

(lli j
	operacionllj s
.lls t
Detallesllt |
(ll| }
)ll} ~
.ll~ 
First	ll �
(
ll� �
)
ll� �
.
ll� �
Registro
ll� �
)
ll� �
;
ll� �
Placamm 
=mm 
	operacionmm 
.mm 
Detallesmm &
(mm& '
)mm' (
.mm( )
Firstmm) .
(mm. /
)mm/ 0
.mm0 1
Registromm1 9
;mm9 :
MostrarInformacionnn 
=nn  
!nn! "
stringnn" (
.nn( )

(nn6 7
	operacionnn7 @
.nn@ A
InformacionnnA L
)nnL M
;nnM N
Informacionoo 
=oo 
	operacionoo #
.oo# $
Informacionoo$ /
;oo/ 0

=pp 
VentasSettingspp *
.pp* +
Defaultpp+ 2
.pp2 3!
GenerarPuntosEnVentaspp3 H
&&ppI K
	operacionppL U
.ppU V
	IdClienteppV _
!=pp` b

.ppp q
Defaultppq x
.ppx y
IdClienteGenerico	ppy �
;
pp� �

=qq 
	operacionqq %
.qq% &

(qq3 4
)qq4 5
;qq5 6
PuntosAcumuladosrr 
=rr 
	operacionrr (
.rr( )
PuntosAcumuladosrr) 9
(rr9 :
)rr: ;
;rr; <
EsInvalidadass 
=ss 
	operacionss $
.ss$ %
EsInvalidadass% 1
;ss1 2
MotivoInvalidaciontt 
=tt  
	operaciontt! *
.tt* +
EsInvalidadatt+ 7
?tt8 9
	operaciontt: C
.ttC D
MotivoInvalidacionttD V
(ttV W
)ttW X
:ttY Z
$strtt[ ]
;tt] ^
}uu 	
}vv 
publicxx 

classxx &
DocumentoElectronicoCompraxx +
:xx, -'
DocumentoElectronicoImpresoxx. I
{yy 
publiczz 
decimalzz 
Igvzz 
{zz 
getzz  
;zz  !
setzz" %
;zz% &
}zz' (
public{{ 
decimal{{ 
Icbper{{ 
{{{ 
get{{  #
;{{# $
set{{% (
;{{( )
}{{* +
public}} &
DocumentoElectronicoCompra}} )
(}}) *
)}}* +
{~~ 	
} 	
public
�� (
DocumentoElectronicoCompra
�� )
(
��) *
OperacionDeCompra
��* ;
	operacion
��< E
,
��E F/
!EstablecimientoComercialExtendido
��G h
sede
��i m
,
��m n
byte
��o s
[
��s t
]
��t u
qrBytes
��v }
,
��} ~
bool�� �(
mostrarEncabezadoTestigo��� �
,��� �0
 ModoImpresionCaracteristicasEnum��� �,
modoImpresionCaracteristicas��� �
)��� �
:��� �
base��� �
(��� �
)��� �
{
�� 	
IdOrden
�� 
=
�� 
	operacion
�� 
.
��  
Id
��  "
;
��" #
FechaEmision
�� 
=
�� 
	operacion
�� $
.
��$ %
FechaEmision
��% 1
;
��1 2
Emisor
�� 
=
�� 
new
�� 
Emisor
�� 
(
��  
	operacion
��  )
.
��) *
	Proveedor
��* 3
(
��3 4
)
��4 5
)
��5 6
;
��6 7
Receptor
�� 
=
�� 
new
�� 
Receptor
�� #
(
��# $
sede
��$ (
)
��( )
;
��) *
Detalles
�� 
=
�� 
Detalle
�� 
.
�� 
Convert
�� &
(
��& '
	operacion
��' 0
.
��0 1
Detalles
��1 9
(
��9 :
)
��: ;
,
��; <*
modoImpresionCaracteristicas
��= Y
,
��Y Z
	operacion
��[ d
.
��d e
DetalleUnificado
��e u
(
��u v
)
��v w
,
��w x
false
��y ~
)
��~ 
;�� �$
MostrarMensajeAmazonia
�� "
=
��# $
	operacion
��% .
.
��. /!
AplicaLeyDeAmazonia
��/ B
;
��B C#
MostrarMensajeNegocio
�� !
=
��" # 
AplicacionSettings
��$ 6
.
��6 7
Default
��7 >
.
��> ?%
MostrarMensajeDeNegocio
��? V
;
��V W
MensajeNegocio
�� 
=
��  
AplicacionSettings
�� /
.
��/ 0
Default
��0 7
.
��7 8
MensajeDeNegocio
��8 H
;
��H I
Observacion
�� 
=
�� 
	operacion
�� #
.
��# $
Observacion
��$ /
(
��/ 0
)
��0 1
??
��2 4
$str
��5 8
;
��8 9
MostrarLogo
�� 
=
�� ,
FacturacionElectronicaSettings
�� 8
.
��8 9
Default
��9 @
.
��@ A-
MostrarLogoEnComprobanteImpreso
��A `
;
��` a0
"MostrarTrazabilidadConceptoNegocio
�� .
=
��/ 0 
AplicacionSettings
��1 C
.
��C D
Default
��D K
.
��K L+
PermitirLoteEnDetalleDeCompra
��L i
;
��i j
CodigoQR
�� 
=
�� 
qrBytes
�� 
;
�� 
CodigoSunatMoneda
�� 
=
�� 
	operacion
��  )
.
��) *
Moneda
��* 0
(
��0 1
)
��1 2
.
��2 3
Codigo
��3 9
;
��9 :
CodigoSunatTipo
�� 
=
�� 
	operacion
�� '
.
��' (
Comprobante
��( 3
(
��3 4
)
��4 5
.
��5 6

CodigoTipo
��6 @
;
��@ A
Serie
�� 
=
�� 
	operacion
�� 
.
�� 
Comprobante
�� )
(
��) *
)
��* +
.
��+ ,

��, 9
;
��9 :
Numero
�� 
=
�� 
	operacion
�� 
.
�� 
Comprobante
�� *
(
��* +
)
��+ ,
.
��, -!
NumeroDeComprobante
��- @
;
��@ A
ImporteTotal
�� 
=
�� 
	operacion
