��
PD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Actores\GrupoClientes_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

partial 
class  
GrupoClientes_Logica -
:. /!
IGrupoClientes_Logica0 E
{ 
private 
readonly 
IActor_Repositorio +
_actor_Repositorio, >
;> ?
private 
readonly %
IVinculoActor_Repositorio 2%
_vinculoActor_Repositorio3 L
;L M
private 
readonly &
IConsultaActor_Repositorio 3&
_consultaActor_Repositorio4 N
;N O
public  
GrupoClientes_Logica #
(# $
IActor_Repositorio$ 6
actor_Repositorio7 H
,H I%
IVinculoActor_RepositorioJ c$
vinculoActor_Repositoriod |
,| }'
IConsultaActor_Repositorio	~ �'
consultaActor_Repositorio
� �
)
� �
{ 	
_actor_Repositorio 
=  
actor_Repositorio! 2
;2 3%
_vinculoActor_Repositorio %
=& '$
vinculoActor_Repositorio( @
;@ A&
_consultaActor_Repositorio &
=' (%
consultaActor_Repositorio) B
;B C
} 	
public!!  
GrupoClientes_Logica!! #
(!!# $
)!!$ %
{"" 	
}## 	
public$$ 
OperationResult$$ 
CrearGrupoClientes$$ 1
($$1 2


)$$M N
{%% 	
try&& 
{'' 
if(( 
((( %
_vinculoActor_Repositorio(( -
.((- .%
ExisteCodigoGrupoClientes((. G
(((G H
(((H I
int((I L
)((L M
TipoVinculo((M X
.((X Y
MiembroGrupo((Y e
,((e f

.((t u
Codigo((u {
)(({ |
)((| }
{)) 
throw** 
new** 
LogicaException** -
(**- .
$str	**. �
)
**� �
;
**� �
}++ 
if,, 
(,, %
_vinculoActor_Repositorio,, -
.,,- .=
1ExisteNombreGrupoClientesEnGruposClientesVigentes,,. _
(,,_ `
(,,` a
int,,a d
),,d e
TipoVinculo,,e p
.,,p q
MiembroGrupo,,q }
,,,} ~

.
,,� �
Nombre
,,� �
)
,,� �
)
,,� �
{-- 
throw.. 
new.. 
LogicaException.. -
(..- .
$str	... �
)
..� �
;
..� �
}// 
var00 
grupoActorNegocio00 %
=00& ',
 GenerarGrupoClientesActorNegocio00( H
(00H I

)00V W
;00W X
var11 
	resultado11 
=11 
_actor_Repositorio11  2
.112 3
CrearActorNegocio113 D
(11D E
grupoActorNegocio11E V
)11V W
;11W X
return22 
	resultado22  
;22  !
}33 
catch44 
(44 
	Exception44 
e44 
)44 
{44  !
throw44" '
e44( )
;44) *
}44+ ,
}55 	
public66 
OperationResult66 #
ActualizarGrupoClientes66 6
(666 7


)66R S
{77 	
try88 
{99 
if:: 
(:: %
_vinculoActor_Repositorio:: -
.::- .9
-ExisteCodigoGrupoClientesExceptoGrupoClientes::. [
(::[ \
(::\ ]
int::] `
)::` a
TipoVinculo::a l
.::l m
MiembroGrupo::m y
,::y z

.
::� �
Codigo
::� �
,
::� �

::� �
.
::� �
Id
::� �
)
::� �
)
::� �
{;; 
throw<< 
new<< 
LogicaException<< -
(<<- .
$str	<<. �
)
<<� �
;
<<� �
}== 
if>> 
(>> %
_vinculoActor_Repositorio>> -
.>>- .Q
EExisteNombreGrupoClientesEnGruposClientesVigentesExceptoGrupoClientes>>. s
(>>s t
(>>t u
int>>u x
)>>x y
TipoVinculo	>>y �
.
>>� �
MiembroGrupo
>>� �
,
>>� �

>>� �
.
>>� �
Nombre
>>� �
,
>>� �

>>� �
.
>>� �
Id
>>� �
)
>>� �
)
>>� �
{?? 
throw@@ 
new@@ 
LogicaException@@ -
(@@- .
$str	@@. �
)
@@� �
;
@@� �
}AA -
!ValidarActualizacionGrupoClientesBB 1
(BB1 2

)BB? @
;BB@ A
varCC 
grupoActorNegocioCC %
=CC& ',
 GenerarGrupoClientesActorNegocioCC( H
(CCH I

)CCV W
;CCW X
grupoActorNegocioDD !
.DD! "
idDD" $
=DD% &

.DD4 5
IdDD5 7
;DD7 8
grupoActorNegocioEE !
.EE! "
id_actorEE" *
=EE+ ,

.EE: ;
IdActorEE; B
;EEB C
grupoActorNegocioFF !
.FF! "
ActorFF" '
.FF' (
idFF( *
=FF+ ,

.FF: ;
IdActorFF; B
;FFB C
returnGG %
_vinculoActor_RepositorioGG 0
.GG0 1;
/ActualizarActorPrincipalConVinculosActorNegocioGG1 `
(GG` a
grupoActorNegocioGGa r
)GGr s
;GGs t
}HH 
catchII 
(II 
	ExceptionII 
eII 
)II 
{II  !
throwII" '
eII( )
;II) *
}II+ ,
}JJ 	
publicKK 
voidKK -
!ValidarActualizacionGrupoClientesKK 5
(KK5 6


)KKQ R
{LL 	
varMM 
grupoClientesActualMM #
=MM$ % 
ObtenerGrupoClientesMM& :
(MM: ;

.MMH I
IdMMI K
)MMK L
;MML M
foreachNN 
(NN 
varNN 
clienteNN  
inNN! #
grupoClientesActualNN$ 7
.NN7 8
ClientesNN8 @
)NN@ A
{OO 
ifPP 
(PP 
!PP 

.PP" #
ClientesPP# +
.PP+ ,
SelectPP, 2
(PP2 3
cPP3 4
=>PP5 7
cPP8 9
.PP9 :
IdPP: <
)PP< =
.PP= >
ContainsPP> F
(PPF G
clientePPG N
.PPN O
IdPPO Q
)PPQ R
)PPR S
{QQ 
ifRR 
(RR %
_vinculoActor_RepositorioRR 1
.RR1 2B
6ExisteDeudaDeClienteEnOperacionesVentaConGrupoClientesRR2 h
(RRh i
clienteRRi p
.RRp q
IdRRq s
,RRs t

.
RR� �
Id
RR� �
)
RR� �
)
RR� �
{SS 
throwTT 
newTT !
LogicaExceptionTT" 1
(TT1 2
$strTT2 r
+TTs t
clienteTTu |
.TT| }
Nombre	TT} �
+
TT� �
$str
TT� �
)
TT� �
;
TT� �
}UU 
}VV 
}WW 
}XX 	
privateYY 

 GenerarGrupoClientesActorNegocioYY >
(YY> ?


)YYZ [
{ZZ 	
try[[ 
{\\ 
DateTime]] 
fechaActual]] $
=]]% &
DateTimeUtil]]' 3
.]]3 4
FechaActual]]4 ?
(]]? @
)]]@ A
;]]A B
DateTime^^ 
fechaFin^^ !
=^^" #
fechaActual^^$ /
.^^/ 0
AddYears^^0 8
(^^8 9

.^^F G
Default^^G N
.^^N OD
7vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna	^^O �
)
^^� �
;
^^� �

grupoActorNegocio`` /
=``0 1
new``2 5

(``C D

.``Q R
Default``R Y
.``Y Z
IdRolGrupoClientes``Z l
,``l m
fechaActual``n y
,``y z
fechaFin	``{ �
,
``� �
$str
``� �
,
``� �
true
``� �
,
``� �
false
``� �
,
``� �
$str
``� �
)
``� �
;
``� �
Actorbb 
actorbb 
=bb 
newbb !
Actorbb" '
(bb' (

.bb5 6
Defaultbb6 =
.bb= >;
/IdDetalleMaestroDocumentoIdentidadGrupoClientesbb> m
,bbm n
fechaActualbbo z
,bbz {

.
bb� �
Codigo
bb� �
,
bb� �

bb� �
.
bb� �
Nombre
bb� �
,
bb� �
$str
bb� �
,
bb� �
$str
bb� �
,
bb� �

bb� �
.
bb� �
Default
bb� �
.
bb� �&
IdTipoActorGrupoClientes
bb� �
,
bb� �

bb� �
.
bb� �
Default
bb� �
.
bb� �#
IdFotoActorPorDefecto
bb� �
,
bb� �

bb� �
.
bb� �
Default
bb� �
.
bb� �'
IdClaseActorGrupoClientes
bb� �
,
bb� �

bb� �
.
bb� �
Default
bb� �
.
bb� �(
IdEstadoLegalGrupoClientes
bb� �
,
bb� �
$str
bb� �
,
bb� �
$str
bb� �
,
bb� �
$str
bb� �
)
bb� �
{cc %
id_detalle_multipropositodd -
=dd. /

.dd= >
Tipodd> B
.ddB C
IdddC E
,ddE F&
id_detalle_multiproposito1ee .
=ee/ 0

.ee> ?

.eeL M
IdeeM O
}ff 
;ff 
grupoActorNegociohh !
.hh! "
Actorhh" '
=hh( )
actorhh* /
;hh/ 0
grupoActorNegociojj !
.jj! ""
Vinculo_Actor_Negocio1jj" 8
=jj9 :
newjj; >
Listjj? C
<jjC D!
Vinculo_Actor_NegociojjD Y
>jjY Z
{jj[ \
newjj] `!
Vinculo_Actor_Negociojja v
{kk &
id_actor_negocio_principalll .
=ll/ 0

.ll> ?
Responsablell? J
.llJ K
IdllK M
,llM N
desdemm 
=mm 
fechaActualmm '
,mm' (
hastann 
=nn 
fechaFinnn $
,nn$ %
descripcionoo 
=oo  !
$stroo" $
,oo$ %
tipo_vinculopp  
=pp! "
(pp# $
intpp$ '
)pp' (
TipoVinculopp( 3
.pp3 4
ResponsableGrupopp4 D
,ppD E

es_vigenteqq 
=qq  
trueqq! %
}rr 
}rr 
;rr 
grupoActorNegociott !
.tt! "!
Vinculo_Actor_Negociott" 7
=tt8 9
newtt: =
Listtt> B
<ttB C!
Vinculo_Actor_NegociottC X
>ttX Y
(ttY Z
)ttZ [
;tt[ \
ifuu 
(uu 

.uu! "
Clientesuu" *
!=uu+ -
nulluu. 2
&&uu3 5

.uuC D
ClientesuuD L
.uuL M
CountuuM R
>uuS T
$numuuU V
)uuV W
{vv 
foreachww 
(ww 
varww  
clienteww! (
inww) +

.ww9 :
Clientesww: B
)wwB C
{xx 
varyy 
vinculoyy #
=yy$ %
newyy& )!
Vinculo_Actor_Negocioyy* ?
{zz &
id_actor_negocio_principal{{ 6
={{7 8

.{{F G
Id{{G I
,{{I J&
id_actor_negocio_vinculado|| 6
=||7 8
cliente||9 @
.||@ A
Id||A C
,||C D
desde}} !
=}}" #
fechaActual}}$ /
,}}/ 0
hasta~~ !
=~~" #
fechaFin~~$ ,
,~~, -
descripcion '
=( )
$str* ,
,, -
tipo_vinculo
�� (
=
��) *
(
��+ ,
int
��, /
)
��/ 0
TipoVinculo
��0 ;
.
��; <
MiembroGrupo
��< H
,
��H I

es_vigente
�� &
=
��' (
true
��) -
}
�� 
;
�� 
grupoActorNegocio
�� )
.
��) *#
Vinculo_Actor_Negocio
��* ?
.
��? @
Add
��@ C
(
��C D
vinculo
��D K
)
��K L
;
��L M
}
�� 
}
�� 
return
�� 
grupoActorNegocio
�� (
;
��( )
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ^
,
��^ _
e
��` a
)
��a b
;
��b c
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� "
GrupoClientesResumen
�� (
>
��( )#
ObtenerGruposClientes
��* ?
(
��? @
)
��@ A
{
�� 	
try
�� 
{
�� 
var
�� 
gruposClientes
�� "
=
��# $'
_vinculoActor_Repositorio
��% >
.
��> ?#
ObtenerGruposClientes
��? T
(
��T U
)
��U V
.
��V W
ToList
��W ]
(
��] ^
)
��^ _
;
��_ `
return
�� 
gruposClientes
�� %
;
��% &
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* X
,
��X Y
e
��Z [
)
��[ \
;
��\ ]
}
�� 
}
�� 	
public
�� 

�� "
ObtenerGrupoClientes
�� 1
(
��1 2
int
��2 5
idGrupoClientes
��6 E
)
��E F
{
�� 	
try
�� 
{
�� 
var
�� 

�� !
=
��" #'
_vinculoActor_Repositorio
��$ =
.
��= >"
ObtenerGrupoClientes
��> R
(
��R S
(
��S T
int
��T W
)
��W X
TipoVinculo
��X c
.
��c d
MiembroGrupo
��d p
,
��p q
idGrupoClientes��r �
)��� �
;��� �

�� 
.
�� 
Responsable
�� )
=
��* +(
_consultaActor_Repositorio
��, F
.
��F G$
ObtenerActorComercial_
��G ]
(
��] ^

��^ k
.
��k l
Default
��l s
.
��s t
IdRolCliente��t �
,��� �

.��� �
Responsable��� �
.��� �
Id��� �
)��� �
;��� �
return
�� 

�� $
;
��$ %
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* W
,
��W X
e
��Y Z
)
��Z [
;
��[ \
}
�� 
}
�� 	
public
�� 
OperationResult
�� "
DarBajaGrupoClientes
�� 3
(
��3 4
int
��4 7
idGrupoClientes
��8 G
)
��G H
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
DateTime
�� 
fechaFin
�� !
=
��" #
fechaActual
��$ /
.
��/ 0
AddYears
��0 8
(
��8 9
$num
��9 ;
)
��; <
;
��< =
if
�� 
(
�� '
_vinculoActor_Repositorio
�� -
.
��- .:
,ExisteDeudaDeGrupoClientesEnOperacionesVenta
��. Z
(
��Z [
idGrupoClientes
��[ j
)
��j k
)
��k l
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str
��. }
)
��} ~
;
��~ 
}
�� 
var
�� 

�� !
=
��" #"
ObtenerGrupoClientes
��$ 8
(
��8 9
idGrupoClientes
��9 H
)
��H I
;
��I J
var
�� 
grupoActorNegocio
�� %
=
��& '.
 GenerarGrupoClientesActorNegocio
��( H
(
��H I

��I V
)
��V W
;
��W X
grupoActorNegocio
�� !
.
��! "
id
��" $
=
��% &

��' 4
.
��4 5
Id
��5 7
;
��7 8
grupoActorNegocio
�� !
.
��! "
id_actor
��" *
=
��+ ,

��- :
.
��: ;
IdActor
��; B
;
��B C
grupoActorNegocio
�� !
.
��! "
Actor
��" '
.
��' (
id
��( *
=
��+ ,

��- :
.
��: ;
IdActor
��; B
;
��B C
grupoActorNegocio
�� !
.
��! "

es_vigente
��" ,
=
��- .
false
��/ 4
;
��4 5
grupoActorNegocio
�� !
.
��! "
	fecha_fin
��" +
=
��, -
fechaActual
��. 9
;
��9 :
return
�� '
_vinculoActor_Repositorio
�� 0
.
��0 1=
/ActualizarActorPrincipalConVinculosActorNegocio
��1 `
(
��` a
grupoActorNegocio
��a r
)
��r s
;
��s t
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ^
,
��^ _
e
��` a
)
��a b
;
��b c
}
�� 
}
�� 	
}
�� 
}�� �h
YD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Actores\ValidacionActorNegocio_Logica.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
Logica		 
.		 
Core		 "
.		" #
Actores		# *
{

 
public 

partial 
class )
ValidacionActorNegocio_Logica 6
:6 7*
IValidacionActorNegocio_Logica8 V
{ 
private 
readonly 
IActor_Repositorio +
_actorRepositorio, =
;= >
public )
ValidacionActorNegocio_Logica ,
(, -
IActor_Repositorio- ?
actorRepositorio@ P
)P Q
{ 	
_actorRepositorio 
= 
actorRepositorio  0
;0 1
} 	
public 
bool 

ValidarDNI 
( 
string %
dni& )
)) *
{ 	
return 
dni 
. 
Trim 
( 
) 
. 
Length $
==% '
$num( )
;) *
} 	
public 
void (
ValidarExistenciaDeDocumento 0
(0 1
int1 4$
idTipoDocumentoIdentidad5 M
,M N
stringO U$
numeroDocumentoIdentidadV n
)n o
{ 	
bool 

validacion 
= 
_actorRepositorio /
./ 0
ExisteDocumento0 ?
(? @$
idTipoDocumentoIdentidad@ X
,X Y$
numeroDocumentoIdentidadZ r
)r s
;s t
if 
( 

validacion 
) 
{ 
throw 
new 
	Exception #
(# $
$str$ J
+K L$
numeroDocumentoIdentidadM e
+f g
$strh x
)x y
;y z
} 
}   	
public!! 
bool!! 

ValidarRUC!! 
(!! 
string!! %
ruc!!& )
)!!) *
{"" 	
if## 
(## 
ruc## 
.## 
Length## 
!=## 
$num##  
)##  !
{$$ 
return%% 
false%% 
;%% 
}&& 
int'' 
dig01'' 
='' 
Convert'' 
.''  
ToInt32''  '
(''' (
ruc''( +
.''+ ,
	Substring'', 5
(''5 6
$num''6 7
,''7 8
$num''9 :
)'': ;
)''; <
*''= >
$num''? @
;''@ A
int(( 
dig02(( 
=(( 
Convert(( 
.((  
ToInt32((  '
(((' (
ruc((( +
.((+ ,
	Substring((, 5
(((5 6
$num((6 7
,((7 8
$num((9 :
)((: ;
)((; <
*((= >
$num((? @
;((@ A
int)) 
dig03)) 
=)) 
Convert)) 
.))  
ToInt32))  '
())' (
ruc))( +
.))+ ,
	Substring)), 5
())5 6
$num))6 7
,))7 8
$num))9 :
))): ;
))); <
*))= >
$num))? @
;))@ A
int** 
dig04** 
=** 
Convert** 
.**  
ToInt32**  '
(**' (
ruc**( +
.**+ ,
	Substring**, 5
(**5 6
$num**6 7
,**7 8
$num**9 :
)**: ;
)**; <
***= >
$num**? @
;**@ A
int++ 
dig05++ 
=++ 
Convert++ 
.++  
ToInt32++  '
(++' (
ruc++( +
.+++ ,
	Substring++, 5
(++5 6
$num++6 7
,++7 8
$num++9 :
)++: ;
)++; <
*++= >
$num++? @
;++@ A
int,, 
dig06,, 
=,, 
Convert,, 
.,,  
ToInt32,,  '
(,,' (
ruc,,( +
.,,+ ,
	Substring,,, 5
(,,5 6
$num,,6 7
,,,7 8
$num,,9 :
),,: ;
),,; <
*,,= >
$num,,? @
;,,@ A
int-- 
dig07-- 
=-- 
Convert-- 
.--  
ToInt32--  '
(--' (
ruc--( +
.--+ ,
	Substring--, 5
(--5 6
$num--6 7
,--7 8
$num--9 :
)--: ;
)--; <
*--= >
$num--? @
;--@ A
int.. 
dig08.. 
=.. 
Convert.. 
...  
ToInt32..  '
(..' (
ruc..( +
...+ ,
	Substring.., 5
(..5 6
$num..6 7
,..7 8
$num..9 :
)..: ;
)..; <
*..= >
$num..? @
;..@ A
int// 
dig09// 
=// 
Convert// 
.//  
ToInt32//  '
(//' (
ruc//( +
.//+ ,
	Substring//, 5
(//5 6
$num//6 7
,//7 8
$num//9 :
)//: ;
)//; <
*//= >
$num//? @
;//@ A
int00 
dig1000 
=00 
Convert00 
.00  
ToInt3200  '
(00' (
ruc00( +
.00+ ,
	Substring00, 5
(005 6
$num006 7
,007 8
$num009 :
)00: ;
)00; <
*00= >
$num00? @
;00@ A
int11 
dig1111 
=11 
Convert11 
.11  
ToInt3211  '
(11' (
ruc11( +
.11+ ,
	Substring11, 5
(115 6
$num116 8
,118 9
$num11: ;
)11; <
)11< =
;11= >
int33 
suma33 
=33 
dig0133 
+33 
dig0233 $
+33% &
dig0333' ,
+33- .
dig0433/ 4
+335 6
dig05337 <
+33= >
dig0633? D
+33E F
dig0733G L
+33M N
dig0833O T
+33U V
dig0933W \
+33] ^
dig1033_ d
;33d e
int44 
residuo44 
=44 
suma44 
%44  
$num44! #
;44# $
int55 
resta55 
=55 
$num55 
-55 
residuo55 $
;55$ %
int77 
digChk77 
=77 
$num77 
;77 
if88 
(88 
resta88 
==88 
$num88 
)88 
{99 
digChk:: 
=:: 
$num:: 
;:: 
};; 
else<< 
if<< 
(<< 
resta<< 
==<< 
$num<<  
)<<  !
{== 
digChk>> 
=>> 
$num>> 
;>> 
}?? 
else@@ 
{AA 
digChkBB 
=BB 
restaBB 
;BB 
}CC 
ifEE 
(EE 
dig11EE 
==EE 
digChkEE 
)EE  
{FF 
returnGG 
trueGG 
;GG 
}HH 
elseII 
{JJ 
returnKK 
falseKK 
;KK 
}LL 
}MM 	
publicNN 
boolNN 
ValidarClienteYRUCNN &
(NN& '
intNN' *
	idClienteNN+ 4
)NN4 5
{OO 	
ClientePP 
clientePP 
=PP 
newPP !
ClientePP" )
(PP) *
_actorRepositorioPP* ;
.PP; <!
ObtenerActorDeNegocioPP< Q
(PPQ R
	idClientePPR [
)PP[ \
)PP\ ]
;PP] ^
returnQQ 
clienteQQ 
.QQ $
IdTipoDocumentoIdentidadQQ 3
==QQ4 6

.QQD E
DefaultQQE L
.QQL M'
IdTipoDocumentoIdentidadRucQQM h
;QQh i
}RR 	
publicSS 
boolSS  
ValidarProveedorYRUCSS (
(SS( )
intSS) ,
idProveedorSS- 8
)SS8 9
{TT 	
	ProveedorUU 
	proveedorUU 
=UU  !
newUU" %
	ProveedorUU& /
(UU/ 0
_actorRepositorioUU0 A
.UUA B!
ObtenerActorDeNegocioUUB W
(UUW X
idProveedorUUX c
)UUc d
)UUd e
;UUe f
returnVV 
	proveedorVV 
.VV $
IdTipoDocumentoIdentidadVV 5
==VV6 8

.VVF G
DefaultVVG N
.VVN O'
IdTipoDocumentoIdentidadRucVVO j
;VVj k
}WW 	
publicXX 
voidXX %
ValidarDocumentoIdentidadXX -
(XX- .
stringXX. 4
documentoIdentidadXX5 G
,XXG H
ItemGenericoXXI U"
tipoDocumentoIdentidadXXV l
)XXl m
{YY 	
ifZZ 
(ZZ "
tipoDocumentoIdentidadZZ &
.ZZ& '
IdZZ' )
==ZZ* ,

.ZZ: ;
DefaultZZ; B
.ZZB C'
IdTipoDocumentoIdentidadDniZZC ^
)ZZ^ _
{[[ 
if\\ 
(\\ 
!\\ 

ValidarDNI\\ 
(\\  
documentoIdentidad\\  2
)\\2 3
)\\3 4
throw\\5 :
new\\; >
	Exception\\? H
(\\H I
$str\\I X
)\\X Y
;\\Y Z
}]] 
else^^ 
if^^ 
(^^ "
tipoDocumentoIdentidad^^ +
.^^+ ,
Id^^, .
==^^/ 1

.^^? @
Default^^@ G
.^^G H'
IdTipoDocumentoIdentidadRuc^^H c
)^^c d
{__ 
if`` 
(`` 
!`` 

ValidarRUC`` 
(``  
documentoIdentidad``  2
)``2 3
)``3 4
throw``5 :
new``; >
	Exception``? H
(``H I
$str``I Y
)``Y Z
;``Z [
}aa 
}bb 	
publiccc 
voidcc <
0ValidarExisteActorConElMismoDocumentoYDistintoIdcc D
(ccD E
intccE H
idccI K
,ccK L
stringccM S
NumeroDeDocumentoccT e
)cce f
{dd 	
ifee 
(ee 
_actorRepositorioee !
.ee! "5
)ExisteActorConElMismoDocumentoYDistintoIdee" K
(eeK L
ideeL N
,eeN O
NumeroDeDocumentoeeP a
)eea b
)eeb c
{ff 
throwgg 
newgg 
LogicaExceptiongg )
(gg) *
$strgg* j
)ggj k
;ggk l
}hh 
}ii 	
publicjj 
voidjj A
5ValidarExisteActorComercialConElMismoDocumentoVigentejj I
(jjI J
intjjJ M
idRoljjN S
,jjS T
stringjjU [
NumeroDeDocumentojj\ m
,jjm n
intjjo r
idActorComercial	jjs �
)
jj� �
{kk 	
ifll 
(ll 
idActorComercialll  
>ll! "
$numll# $
)ll$ %
{mm 
ifnn 
(nn 
_actorRepositorionn %
.nn% &:
.ExisteActorComercialConElMismoDocumentoVigentenn& T
(nnT U
idActorComercialnnU e
,nne f
idRolnng l
,nnl m
NumeroDeDocumentonnn 
)	nn �
)
nn� �
{oo 
throwpp 
newpp 
LogicaExceptionpp -
(pp- .
$strpp. x
)ppx y
;ppy z
}qq 
}rr 
elsess 
{tt 
ifuu 
(uu 
_actorRepositoriouu %
.uu% &:
.ExisteActorComercialConElMismoDocumentoVigenteuu& T
(uuT U
idRoluuU Z
,uuZ [
NumeroDeDocumentouu\ m
)uum n
)uun o
{vv 
throwww 
newww 
LogicaExceptionww -
(ww- .
$strww. x
)wwx y
;wwy z
}xx 
}yy 
}{{ 	
public|| 
void|| /
#ValidarOperaciondesDeActorComercial|| 7
(||7 8
int||8 ;
idRol||< A
,||A B
int||C F
id||G I
,||I J
string||K Q
numeroDeDocumento||R c
)||c d
{}} 	
var~~ 
actorComercial~~ 
=~~  
_actorRepositorio~~! 2
.~~2 3!
ObtenerActorComercial~~3 H
(~~H I
id~~I K
)~~K L
;~~L M
if 
( 
actorComercial 
. $
NumeroDocumentoIdentidad 7
!=8 :
numeroDeDocumento; L
)L M
{
�� 
if
�� 
(
�� 
_actorRepositorio
�� %
.
��% &+
ActorParticipaEnTransacciones
��& C
(
��C D
id
��D F
)
��F G
)
��G H
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str��. �
)��� �
;��� �
}
�� 
}
�� 
}
�� 	
}
�� 
}�� ��
OD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Almacen\OrdenAlmacen_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Almacen# *
{ 
public 

class 
OrdenAlmacen_Logica $
:% & 
IOrdenAlmacen_Logica' ;
{ 
	protected 
readonly %
IOrdenAlmacen_Repositorio 4
_ordenAlmacenDatos5 G
;G H
	protected 
readonly $
ICentroDeAtencion_Logica 3#
_centroDeAtencionLogica4 K
;K L
	protected 
readonly 
IActorNegocioLogica .
_actorNegocioLogica/ B
;B C
	protected 
readonly 
IOperacionLogica +
_operacionLogica, <
;< =
	protected 
readonly #
ITransaccionRepositorio 2#
_transaccionRepositorio3 J
;J K
public 
OrdenAlmacen_Logica "
(" #%
IOrdenAlmacen_Repositorio# <
ordenAlmacenDatos= N
,N O$
ICentroDeAtencion_LogicaP h"
centroDeAtencionLogicai 
,	 �!
IActorNegocioLogica
� � 
actorNegocioLogica
� �
,
� �
IOperacionLogica
� �
operacionLogica
� �
,
� �%
ITransaccionRepositorio
� �$
transaccionRepositorio
� �
)
� �
{ 	
_ordenAlmacenDatos 
=  
ordenAlmacenDatos! 2
;2 3#
_centroDeAtencionLogica   #
=  $ %"
centroDeAtencionLogica  & <
;  < =
_actorNegocioLogica!! 
=!!  !
actorNegocioLogica!!" 4
;!!4 5
_operacionLogica"" 
="" 
operacionLogica"" .
;"". /#
_transaccionRepositorio## #
=##$ %"
transaccionRepositorio##& <
;##< =
}$$ 	
public&& %
PrincipalOrdenAlmacenData&& (1
%ObtenerDatosParaOrdenAlmacenPrincipal&&) N
(&&N O"
UserProfileSessionData&&O e
profileData&&f q
)&&q r
{'' 	
var(( *
tieneRolAdministradorDeNegocio(( .
=((/ 0
profileData((1 <
.((< =
Empleado((= E
.((E F
TieneRol((F N
(((N O

.((\ ]
Default((] d
.((d e(
idRolAdministradorDeNegocio	((e �
)
((� �
;
((� �
var** 
data** 
=** 
new** %
PrincipalOrdenAlmacenData** 4
(**4 5
)**5 6
{++ 
FechaActual,, 
=,, 
DateTimeUtil,, *
.,,* +
FechaActual,,+ 6
(,,6 7
),,7 8
,,,8 9
EsAdministrador-- 
=--  !*
tieneRolAdministradorDeNegocio--" @
,--@ A
	Almacenes.. 
=.. *
tieneRolAdministradorDeNegocio.. :
?..; <
ItemGenerico..= I
...I J@
4ConvertirCentroDeAtencionConEstablecimientoComercial..J ~
(..~ $
_centroDeAtencionLogica	.. �
.
..� �&
ObtenerAlmacenesVigentes
..� �
(
..� �
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
)
..� �
:
..� �
new
..� �
List
..� �
<
..� �
ItemGenerico
..� �
>
..� �
(
..� �
)
..� �
{
..� �
profileData
..� �
.
..� �*
CentroDeAtencionSeleccionado
..� �
.
..� �
ToItemGenerico
..� �
(
..� �
)
..� �
}
..� �
,
..� �

=// 
profileData//  +
.//+ ,(
CentroDeAtencionSeleccionado//, H
.//H I
ToItemGenerico//I W
(//W X
)//X Y
,//Y Z
}00 
;00
return11 
data11 
;11 
}22 	
public44 
List44 
<44 
OrdenAlmacenResumen44 '
>44' (!
ObtenerOrdenesAlmacen44) >
(44> ?
DateTime44? G

fechaDesde44H R
,44R S
DateTime44T \

fechaHasta44] g
,44g h
bool44i m
porIngresar44n y
,44y z
bool44{ 
entregaInmediata
44� �
,
44� �
bool
44� �
entregaDiferida
44� �
,
44� �
bool
44� �
estadoPendiente
44� �
,
44� �
bool
44� �

44� �
,
44� �
bool
44� �
estadoCompletada
44� �
,
44� �
int
44� �
[
44� �
]
44� �
idsAlmacenes
44� �
,
44� �$
UserProfileSessionData
44� �
profileData
44� �
)
44� �
{55 	
try66 
{77 
List88 
<88 
int88 
>88 
idsModoEntrega88 (
=88) *
new88+ .
List88/ 3
<883 4
int884 7
>887 8
(888 9
)889 :
;88: ;
if99 
(99 
entregaInmediata99 $
)99$ %
idsModoEntrega99& 4
.994 5
Add995 8
(998 9
(999 :
int99: =
)99= >#
IndicadorImpactoAlmacen99> U
.99U V
	Inmediata99V _
)99_ `
;99` a
if:: 
(:: 
entregaDiferida:: #
)::# $
idsModoEntrega::% 3
.::3 4
Add::4 7
(::7 8
(::8 9
int::9 <
)::< =#
IndicadorImpactoAlmacen::= T
.::T U
Diferida::U ]
)::] ^
;::^ _
List;; 
<;; 
int;; 
>;; 
	idsEstado;; #
=;;$ %
new;;& )
List;;* .
<;;. /
int;;/ 2
>;;2 3
(;;3 4
);;4 5
;;;5 6
if<< 
(<< 
estadoPendiente<< #
)<<# $
	idsEstado<<% .
.<<. /
Add<</ 2
(<<2 3
MaestroSettings<<3 B
.<<B C
Default<<C J
.<<J K+
IdDetalleMaestroEstadoPendiente<<K j
)<<j k
;<<k l
if== 
(== 

)==! "
	idsEstado==# ,
.==, -
Add==- 0
(==0 1
MaestroSettings==1 @
.==@ A
Default==A H
.==H I)
IdDetalleMaestroEstadoParcial==I f
)==f g
;==g h
if>> 
(>> 
estadoCompletada>> $
)>>$ %
	idsEstado>>& /
.>>/ 0
Add>>0 3
(>>3 4
MaestroSettings>>4 C
.>>C D
Default>>D K
.>>K L,
 IdDetalleMaestroEstadoCompletada>>L l
)>>l m
;>>m n
var?? 
ordenesAlmacen?? "
=??# $
_ordenAlmacenDatos??% 7
.??7 8!
ObtenerOrdenesAlmacen??8 M
(??M N

fechaDesde??N X
,??X Y

fechaHasta??Z d
,??d e
porIngresar??f q
,??q r
idsModoEntrega	??s �
.
??� �
ToArray
??� �
(
??� �
)
??� �
,
??� �
	idsEstado
??� �
.
??� �
ToArray
??� �
(
??� �
)
??� �
,
??� �
idsAlmacenes
??� �
)
??� �
.
??� �
ToList
??� �
(
??� �
)
??� �
;
??� �
ordenesAlmacen@@ 
.@@ 
AddRange@@ '
(@@' (
_ordenAlmacenDatos@@( :
.@@: ;.
"ObtenerOrdenesAlmacenBidireccional@@; ]
(@@] ^

fechaDesde@@^ h
,@@h i

fechaHasta@@j t
,@@t u
porIngresar	@@v �
,
@@� �
idsModoEntrega
@@� �
.
@@� �
ToArray
@@� �
(
@@� �
)
@@� �
,
@@� �
	idsEstado
@@� �
.
@@� �
ToArray
@@� �
(
@@� �
)
@@� �
,
@@� �
idsAlmacenes
@@� �
)
@@� �
.
@@� �
ToList
@@� �
(
@@� �
)
@@� �
)
@@� �
;
@@� �
returnAA 
ordenesAlmacenAA %
;AA% &
}BB 
catchCC 
(CC 
	ExceptionCC 
eCC 
)CC 
{DD 
throwEE 
newEE 
LogicaExceptionEE )
(EE) *
$strEE* S
,EES T
eEEU V
)EEV W
;EEW X
}FF 
}GG 	
publicII 
OrdenAlmacenII 
ObtenerOrdenAlmacenII /
(II/ 0
longII0 4
idOrdenAlmacenII5 C
)IIC D
{JJ 	
tryKK 
{LL 
varMM 
ordenAlmacenMM  
=MM! "
_ordenAlmacenDatosMM# 5
.MM5 6
ObtenerOrdenAlmacenMM6 I
(MMI J
idOrdenAlmacenMMJ X
)MMX Y
;MMY Z
ordenAlmacenNN 
.NN 

IdsOrdenesNN '
.NN' (
InsertNN( .
(NN. /
$numNN/ 0
,NN0 1
ordenAlmacenNN2 >
.NN> ?
IdNN? A
)NNA B
;NNB C
ordenAlmacenOO 
.OO 
OrdenesOO $
=OO% &(
ObtenerOrdenesDeOrdenAlmacenOO' C
(OOC D
ordenAlmacenOOD P
.OOP Q

IdsOrdenesOOQ [
.OO[ \
ToArrayOO\ c
(OOc d
)OOd e
)OOe f
;OOf g
ordenAlmacenPP 
.PP 
MovimientosPP (
=PP) *,
 ObtenerMovimientosDeOrdenAlmacenPP+ K
(PPK L
ordenAlmacenPPL X
.PPX Y

IdsOrdenesPPY c
.PPc d
ToArrayPPd k
(PPk l
)PPl m
)PPm n
;PPn o
varQQ 

=QQ" #
_ordenAlmacenDatosQQ$ 6
.QQ6 7-
!ObtenerNotasCreditoDeOrdenAlmacenQQ7 X
(QQX Y
ordenAlmacenQQY e
.QQe f
IdQQf h
)QQh i
.QQi j

SelectManyQQj t
(QQt u
nQQu v
=>QQw y
nQQz {
.QQ{ |
Detalles	QQ| �
)
QQ� �
.
QQ� �
ToList
QQ� �
(
QQ� �
)
QQ� �
;
QQ� �
varRR 
detallesMovimientosRR '
=RR( )
ordenAlmacenRR* 6
.RR6 7
MovimientosRR7 B
.RRB C
WhereRRC H
(RRH I
mRRI J
=>RRK M
mRRN O
.RRO P
	EsVigenteRRP Y
)RRY Z
.RRZ [

SelectManyRR[ e
(RRe f
mRRf g
=>RRh j
mRRk l
.RRl m
DetallesRRm u
)RRu v
.RRv w
ToListRRw }
(RR} ~
)RR~ 
;	RR �
foreachSS 
(SS 
varSS 
detalleSS $
inSS% '
ordenAlmacenSS( 4
.SS4 5
DetallesSS5 =
)SS= >
{TT 
detalleUU 
.UU 
	EntregadoUU %
=UU& '
detallesMovimientosUU( ;
.UU; <
WhereUU< A
(UUA B
dmoUUB E
=>UUF H
dmoUUI L
.UUL M

IdConceptoUUM W
==UUX Z
detalleUU[ b
.UUb c

IdConceptoUUc m
)UUm n
.UUn o
SumUUo r
(UUr s
dUUs t
=>UUu w
dUUx y
.UUy z
Cantidad	UUz �
)
UU� �
;
UU� �
detalleVV 
.VV 
	PendienteVV %
=VV& '
detalleVV( /
.VV/ 0
OrdenadoVV0 8
-VV9 :
detalleVV; B
.VVB C
RevocadoVVC K
-VVL M
detalleVVN U
.VVU V
	EntregadoVVV _
;VV_ `
detalleWW 
.WW 
DevueltoWW $
=WW% &

.WW4 5
WhereWW5 :
(WW: ;
dmoWW; >
=>WW? A
dmoWWB E
.WWE F

IdConceptoWWF P
==WWQ S
detalleWWT [
.WW[ \

IdConceptoWW\ f
)WWf g
.WWg h
SumWWh k
(WWk l
dWWl m
=>WWn p
dWWq r
.WWr s
CantidadWWs {
)WW{ |
;WW| }
}XX 
returnYY 
ordenAlmacenYY #
;YY# $
}ZZ 
catch[[ 
([[ 
	Exception[[ 
e[[ 
)[[ 
{\\ 
throw]] 
new]] 
LogicaException]] )
(]]) *
$str]]* P
,]]P Q
e]]R S
)]]S T
;]]T U
}^^ 
}__ 	
publicaa 
OrdenAlmacenaa 
ObtenerOrdenAlmacenaa /
(aa/ 0
longaa0 4
idOrdenAlmacenaa5 C
,aaC D
boolaaE I
porIngresaraaJ U
)aaU V
{bb 	
trycc 
{dd 
varee %
ordenAlmacenBidireccionalee -
=ee. /
_ordenAlmacenDatosee0 B
.eeB C.
"VerificarOrdenAlmacenBidireccionaleeC e
(eee f
idOrdenAlmaceneef t
)eet u
;eeu v
varff 
ordenAlmacenff  
=ff! "%
ordenAlmacenBidireccionalff# <
?ff= >
_ordenAlmacenDatosff? Q
.ffQ R*
ObtenerOrdenAlmacenBireccionalffR p
(ffp q
idOrdenAlmacenffq 
,	ff �
porIngresar
ff� �
)
ff� �
:
ff� � 
_ordenAlmacenDatos
ff� �
.
ff� �!
ObtenerOrdenAlmacen
ff� �
(
ff� �
idOrdenAlmacen
ff� �
)
ff� �
;
ff� �
ordenAlmacengg 
.gg 

IdsOrdenesgg '
.gg' (
Insertgg( .
(gg. /
$numgg/ 0
,gg0 1
ordenAlmacengg2 >
.gg> ?
Idgg? A
)ggA B
;ggB C
ordenAlmacenhh 
.hh 
Ordeneshh $
=hh% &(
ObtenerOrdenesDeOrdenAlmacenhh' C
(hhC D
ordenAlmacenhhD P
.hhP Q

IdsOrdeneshhQ [
.hh[ \
ToArrayhh\ c
(hhc d
)hhd e
)hhe f
;hhf g
ordenAlmacenii 
.ii 
Movimientosii (
=ii) *,
 ObtenerMovimientosDeOrdenAlmacenii+ K
(iiK L
ordenAlmaceniiL X
.iiX Y

IdsOrdenesiiY c
.iic d
ToArrayiid k
(iik l
)iil m
,iim n
porIngresariio z
)iiz {
;ii{ |
varjj 
detallesMovimientosjj '
=jj( )
ordenAlmacenjj* 6
.jj6 7
Movimientosjj7 B
.jjB C
WherejjC H
(jjH I
mjjI J
=>jjK M
mjjN O
.jjO P
	EsVigentejjP Y
)jjY Z
.jjZ [

SelectManyjj[ e
(jje f
mjjf g
=>jjh j
mjjk l
.jjl m
Detallesjjm u
)jju v
.jjv w
ToListjjw }
(jj} ~
)jj~ 
;	jj �
foreachkk 
(kk 
varkk 
detallekk $
inkk% '
ordenAlmacenkk( 4
.kk4 5
Detalleskk5 =
)kk= >
{ll 
detallemm 
.mm 
	Entregadomm %
=mm& '
detallesMovimientosmm( ;
.mm; <
Wheremm< A
(mmA B
dmommB E
=>mmF H
dmommI L
.mmL M

IdConceptommM W
==mmX Z
detallemm[ b
.mmb c

IdConceptommc m
)mmm n
.mmn o
Summmo r
(mmr s
dmms t
=>mmu w
dmmx y
.mmy z
Cantidad	mmz �
)
mm� �
;
mm� �
detallenn 
.nn 
	Pendientenn %
=nn& '
detallenn( /
.nn/ 0
Ordenadonn0 8
-nn9 :
detallenn; B
.nnB C
RevocadonnC K
-nnL M
detallennN U
.nnU V
	EntregadonnV _
;nn_ `
}oo 
returnpp 
ordenAlmacenpp #
;pp# $
}qq 
catchrr 
(rr 
	Exceptionrr 
err 
)rr 
{ss 
throwtt 
newtt 
LogicaExceptiontt )
(tt) *
$strtt* P
,ttP Q
ettR S
)ttS T
;ttT U
}uu 
}vv 	
publicxx 
Listxx 
<xx 
OrdenDeOrdenAlmacenxx '
>xx' ((
ObtenerOrdenesDeOrdenAlmacenxx) E
(xxE F
longxxF J
[xxJ K
]xxK L

idsOrdenesxxM W
)xxW X
{yy 	
tryzz 
{{{ 
var|| !
ordenesDeOrdenAlmacen|| )
=||* +
_ordenAlmacenDatos||, >
.||> ?(
ObtenerOrdenesDeOrdenAlmacen||? [
(||[ \

idsOrdenes||\ f
)||f g
.||g h
ToList||h n
(||n o
)||o p
;||p q!
ordenesDeOrdenAlmacen}} %
.}}% &
ForEach}}& -
(}}- .
o}}. /
=>}}0 2
o}}3 4
.}}4 5
Comprobante}}5 @
=}}A B
new}}C F 
ComprobanteDeAlmacen}}G [
(}}[ \
)}}\ ]
{}}^ _
Id}}` b
=}}c d
o}}e f
.}}f g
Id}}g i
}}}j k
)}}k l
;}}l m
return~~ !
ordenesDeOrdenAlmacen~~ ,
;~~, -
} 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* \
,
��\ ]
e
��^ _
)
��_ `
;
��` a
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� &
MovimientoDeOrdenAlmacen
�� ,
>
��, -.
 ObtenerMovimientosDeOrdenAlmacen
��. N
(
��N O
long
��O S
[
��S T
]
��T U

idsOrdenes
��V `
)
��` a
{
�� 	
try
�� 
{
�� 
var
�� '
movimientosDeOrdenAlmacen
�� -
=
��. / 
_ordenAlmacenDatos
��0 B
.
��B C.
 ObtenerMovimientosDeOrdenAlmacen
��C c
(
��c d

idsOrdenes
��d n
)
��n o
.
��o p
ToList
��p v
(
��v w
)
��w x
;
��x y'
movimientosDeOrdenAlmacen
�� )
.
��) *
ForEach
��* 1
(
��1 2
o
��2 3
=>
��4 6
o
��7 8
.
��8 9
Comprobante
��9 D
=
��E F
new
��G J"
ComprobanteDeAlmacen
��K _
(
��_ `
)
��` a
{
��b c
Id
��d f
=
��g h
o
��i j
.
��j k
Id
��k m
}
��n o
)
��o p
;
��p q
return
�� '
movimientosDeOrdenAlmacen
�� 0
;
��0 1
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* `
,
��` a
e
��b c
)
��c d
;
��d e
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� &
MovimientoDeOrdenAlmacen
�� ,
>
��, -.
 ObtenerMovimientosDeOrdenAlmacen
��. N
(
��N O
long
��O S
[
��S T
]
��T U

idsOrdenes
��V `
,
��` a
bool
��b f
porIngresar
��g r
)
��r s
{
�� 	
try
�� 
{
�� 
var
�� '
movimientosDeOrdenAlmacen
�� -
=
��. / 
_ordenAlmacenDatos
��0 B
.
��B C.
 ObtenerMovimientosDeOrdenAlmacen
��C c
(
��c d

idsOrdenes
��d n
,
��n o
porIngresar
��p {
)
��{ |
.
��| }
ToList��} �
(��� �
)��� �
;��� �'
movimientosDeOrdenAlmacen
�� )
.
��) *
ForEach
��* 1
(
��1 2
o
��2 3
=>
��4 6
o
��7 8
.
��8 9
Comprobante
��9 D
=
��E F
new
��G J"
ComprobanteDeAlmacen
��K _
(
��_ `
)
��` a
{
��b c
Id
��d f
=
��g h
o
��i j
.
��j k
Id
��k m
}
��n o
)
��o p
;
��p q
return
�� '
movimientosDeOrdenAlmacen
�� 0
;
��0 1
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* `
,
��` a
e
��b c
)
��c d
;
��d e
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� &
MovimientoDeOrdenAlmacen
�� ,
>
��, -9
+ObtenerMovimientosConfirmadosDeOrdenAlmacen
��. Y
(
��Y Z
long
��Z ^
[
��^ _
]
��_ `

idsOrdenes
��a k
)
��k l
{
�� 	
try
�� 
{
�� 
var
�� '
movimientosDeOrdenAlmacen
�� -
=
��. / 
_ordenAlmacenDatos
��0 B
.
��B C9
+ObtenerMovimientosConfirmadosDeOrdenAlmacen
��C n
(
��n o

idsOrdenes
��o y
)
��y z
.
��z {
ToList��{ �
(��� �
)��� �
;��� �'
movimientosDeOrdenAlmacen
�� )
.
��) *
ForEach
��* 1
(
��1 2
o
��2 3
=>
��4 6
o
��7 8
.
��8 9
Comprobante
��9 D
=
��E F
new
��G J"
ComprobanteDeAlmacen
��K _
(
��_ `
)
��` a
{
��b c
Id
��d f
=
��g h
o
��i j
.
��j k
Id
��k m
}
��n o
)
��o p
;
��p q
return
�� '
movimientosDeOrdenAlmacen
�� 0
;
��0 1
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* `
,
��` a
e
��b c
)
��c d
;
��d e
}
�� 
}
�� 	
public
�� '
RegistroMovimientoAlmacen
�� (3
%ObtenerRegistroMovimientoOrdenAlmacen
��) N
(
��N O
long
��O S
idOrdenAlmacen
��T b
,
��b c
bool
��d h
porIngresar
��i t
,
��t u%
UserProfileSessionData��v �
profileData��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� ,
registroMovimientoOrdenAlmacen
�� 2
=
��3 4
new
��5 8'
RegistroMovimientoAlmacen
��9 R
(
��R S
)
��S T
;
��T U
var
�� 
ordenAlmacen
��  
=
��! "!
ObtenerOrdenAlmacen
��# 6
(
��6 7
idOrdenAlmacen
��7 E
)
��E F
;
��F G
var
�� !
detallesStockActual
�� '
=
��( ) 
_ordenAlmacenDatos
��* <
.
��< =4
&ObtenerStockActualDetallesOrdenAlmacen
��= c
(
��c d
ordenAlmacen
��d p
.
��p q
Detalles
��q y
.
��y z
Select��z �
(��� �
d��� �
=>��� �
d��� �
.��� �

IdConcepto��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
,��� �
ordenAlmacen��� �
.��� �
	IdAlmacen��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
�� 
detalle
�� $
in
��% '
ordenAlmacen
��( 4
.
��4 5
Detalles
��5 =
)
��= >
{
�� ,
registroMovimientoOrdenAlmacen
�� 2
.
��2 3
Detalles
��3 ;
.
��; <
Add
��< ?
(
��? @
new
��@ C(
DetalleMovimientoDeAlmacen
��D ^
{
�� 

IdProducto
�� "
=
��# $
detalle
��% ,
.
��, -

IdConcepto
��- 7
,
��7 8
Descripcion
�� #
=
��$ %
detalle
��& -
.
��- .
Concepto
��. 6
,
��6 7
StockActual
�� #
=
��$ %!
detallesStockActual
��& 9
.
��9 :
Single
��: @
(
��@ A
d
��A B
=>
��C E
d
��F G
.
��G H

IdConcepto
��H R
==
��S U
detalle
��V ]
.
��] ^

IdConcepto
��^ h
)
��h i
.
��i j
StockActual
��j u
,
��u v
	Pendiente
�� !
=
��" #
detalle
��$ +
.
��+ ,
	Pendiente
��, 5
,
��5 6!
IngresoSalidaActual
�� +
=
��, -
detalle
��. 5
.
��5 6
	Pendiente
��6 ?
}
�� 
)
�� 
;
�� 
}
�� ,
registroMovimientoOrdenAlmacen
�� .
.
��. /
IdOrdenDeAlmacen
��/ ?
=
��@ A
ordenAlmacen
��B N
.
��N O
Id
��O Q
;
��Q R,
registroMovimientoOrdenAlmacen
�� .
.
��. /
Tercero
��/ 6
.
��6 7
Id
��7 9
=
��: ;
ordenAlmacen
��< H
.
��H I
IdOrigenDestino
��I X
;
��X Y,
registroMovimientoOrdenAlmacen
�� .
.
��. /
UbigeoOrigen
��/ ;
=
��< =
porIngresar
��> I
?
��J K!
_actorNegocioLogica
��L _
.
��_ `3
$ObtenerUbigeoDireccionActorComercial��` �
(��� �.
registroMovimientoOrdenAlmacen��� �
.��� �
Tercero��� �
.��� �
Id��� �
)��� �
:��� �
profileData��� �
.��� �
Sede��� �
.��� �
DomicilioFiscal��� �
.��� �
Ubigeo��� �
;��� �,
registroMovimientoOrdenAlmacen
�� .
.
��. /
DireccionOrigen
��/ >
=
��? @
porIngresar
��A L
?
��M N!
_actorNegocioLogica
��O b
.
��b c4
%ObtenerDetalleDireccionActorComercial��c �
(��� �.
registroMovimientoOrdenAlmacen��� �
.��� �
Tercero��� �
.��� �
Id��� �
)��� �
:��� �
profileData��� �
.��� �
Sede��� �
.��� �
DomicilioFiscal��� �
.��� �
Detalle��� �
;��� �,
registroMovimientoOrdenAlmacen
�� .
.
��. /

��/ <
=
��= >
porIngresar
��? J
?
��K L
profileData
��M X
.
��X Y
Sede
��Y ]
.
��] ^
DomicilioFiscal
��^ m
.
��m n
Ubigeo
��n t
:
��u v"
_actorNegocioLogica��w �
.��� �4
$ObtenerUbigeoDireccionActorComercial��� �
(��� �.
registroMovimientoOrdenAlmacen��� �
.��� �
Tercero��� �
.��� �
Id��� �
)��� �
;��� �,
registroMovimientoOrdenAlmacen
�� .
.
��. /
DireccionDestino
��/ ?
=
��@ A
porIngresar
��B M
?
��N O
profileData
��P [
.
��[ \
Sede
��\ `
.
��` a
DomicilioFiscal
��a p
.
��p q
Detalle
��q x
:
��y z"
_actorNegocioLogica��{ �
.��� �5
%ObtenerDetalleDireccionActorComercial��� �
(��� �.
registroMovimientoOrdenAlmacen��� �
.��� �
Tercero��� �
.��� �
Id��� �
)��� �
;��� �
return
�� ,
registroMovimientoOrdenAlmacen
�� 5
;
��5 6
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* P
,
��P Q
e
��R S
)
��S T
;
��T U
}
�� 
}
�� 	
public
�� 
OperationResult
�� +
GuardarMovimientoOrdenAlmacen
�� <
(
��< ='
RegistroMovimientoAlmacen
��= V$
movimientoOrdenAlmacen
��W m
,
��m n%
UserProfileSessionData��o �

)��� �
{
�� 	
try
�� 
{
�� 
return
�� 
_operacionLogica
�� '
.
��' (+
GuardarMovimientoOrdenAlmacen
��( E
(
��E F$
movimientoOrdenAlmacen
��F \
,
��\ ]

��^ k
)
��k l
;
��l m
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* j
,
��j k
e
��l m
)
��m n
;
��n o
}
�� 
}
�� 	
public
�� 
OperationResult
�� -
InvalidarMovimientoOrdenAlmacen
�� >
(
��> ?
long
��? C&
idMovimientoOrdenAlmacen
��D \
,
��\ ]
string
��^ d
observacion
��e p
,
��p q%
UserProfileSessionData��r �

)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
fechaActual
�� 
=
��  !
DateTimeUtil
��" .
.
��. /
FechaActual
��/ :
(
��: ;
)
��; <
;
��< =
var
�� 
ordenAlmacen
��  
=
��! " 
_ordenAlmacenDatos
��# 5
.
��5 6<
.ObtenerOrdenAlmacenConIdMovimientoOrdenAlmacen
��6 d
(
��d e&
idMovimientoOrdenAlmacen
��e }
)
��} ~
;
��~ 
var
�� 
movimientos
�� 
=
��  !9
+ObtenerMovimientosConfirmadosDeOrdenAlmacen
��" M
(
��M N
new
��N Q
long
��R V
[
��V W
]
��W X
{
��Y Z
ordenAlmacen
��[ g
.
��g h
Id
��h j
}
��k l
)
��l m
;
��m n
var
�� "
estadosTransacciones
�� (
=
��) *
new
��+ .
List
��/ 3
<
��3 4 
Estado_transaccion
��4 F
>
��F G
(
��G H
)
��H I
;
��I J
if
�� 
(
�� 
movimientos
�� 
.
��  
Where
��  %
(
��% &
m
��& '
=>
��( *
m
��+ ,
.
��, -
	EsVigente
��- 6
)
��6 7
.
��7 8
Count
��8 =
(
��= >
)
��> ?
>
��@ A
$num
��B C
)
��C D
{
�� 
if
�� 
(
�� 
!
�� 
ordenAlmacen
�� %
.
��% &
EstaParcial
��& 1
)
��1 2
{
�� "
estadosTransacciones
�� ,
.
��, -
Add
��- 0
(
��0 1
new
��1 4 
Estado_transaccion
��5 G
(
��G H
ordenAlmacen
��H T
.
��T U
Id
��U W
,
��W X

��Y f
.
��f g
Empleado
��g o
.
��o p
Id
��p r
,
��r s
MaestroSettings��t �
.��� �
Default��� �
.��� �-
IdDetalleMaestroEstadoParcial��� �
,��� �
fechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
else
�� 
{
�� 
if
�� 
(
�� 
!
�� 
ordenAlmacen
�� %
.
��% &

��& 3
)
��3 4
{
�� "
estadosTransacciones
�� ,
.
��, -
Add
��- 0
(
��0 1
new
��1 4 
Estado_transaccion
��5 G
(
��G H
ordenAlmacen
��H T
.
��T U
Id
��U W
,
��W X

��Y f
.
��f g
Empleado
��g o
.
��o p
Id
��p r
,
��r s
MaestroSettings��t �
.��� �
Default��� �
.��� �/
IdDetalleMaestroEstadoPendiente��� �
,��� �
fechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
var
�� .
 movimientoOrdenAlmacenAInvalidar
�� 4
=
��5 6%
_transaccionRepositorio
��7 N
.
��N ON
?ObtenerTransaccionInclusiveEstadoTransaccionYDetalleTransaccion��O �
(��� �(
idMovimientoOrdenAlmacen��� �
)��� �
;��� �.
 movimientoOrdenAlmacenAInvalidar
�� 0
.
��0 1!
id_tipo_transaccion
��1 D
=
��E F.
 movimientoOrdenAlmacenAInvalidar
��G g
.
��g h
Tipo_transaccion
��h x
.
��x y5
&Accion_de_negocio_por_tipo_transaccion��y �
.��� �
First��� �
(��� �
)��� �
.��� �
valor��� �
?��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �=
-IdTipoTransaccionSalidaBienesAjusteInventario��� �
:��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �>
.IdTipoTransaccionEntradaBienesAjusteInventario��� �
;��� �"
estadosTransacciones
�� $
.
��$ %
Add
��% (
(
��( )
new
��) , 
Estado_transaccion
��- ?
(
��? @.
 movimientoOrdenAlmacenAInvalidar
��@ `
.
��` a
id
��a c
,
��c d

��e r
.
��r s
Empleado
��s {
.
��{ |
Id
��| ~
,
��~ 
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoInvalidado��� �
,��� �
fechaActual��� �
,��� �
observacion��� �
)��� �
)��� �
;��� �
return
�� 
_operacionLogica
�� '
.
��' (K
=AfectarInventarioFisicoYGuardarInventarioEstadosTransacciones
��( e
(
��e f/
 movimientoOrdenAlmacenAInvalidar��f �
,��� �$
estadosTransacciones��� �
,��� �

)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* i
,
��i j
e
��k l
)
��l m
;
��m n
}
�� 
}
�� 	
}
�� 
}�� ҭ
SD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Almacen\AjusteInventario_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Almacen# *
{ 
public 

class #
AjusteInventario_Logica (
:) *$
IAjusteInventario_Logica+ C
{ 
	protected 
readonly (
IInventarioActualRepositorio 7"
_inventarioActualDatos8 N
;N O
	protected 
readonly $
IMovimientos_Repositorio 3
_movimientosDatos4 E
;E F
	protected 
readonly (
IMaestrosAlmacen_Repositorio 7!
_maestrosAlmacenDatos8 M
;M N
	protected 
readonly 
IPermisos_Logica +
_permisosLogica, ;
;; <
	protected 
readonly $
ICodigosOperacion_Logica 3#
_codigosOperacionLogica4 K
;K L
	protected 
readonly )
ICrearTransaccion_Repositorio 8$
_crearTransaccionesDatos9 Q
;Q R
	protected 
readonly ,
 IConsultaTransaccion_Repositorio ;%
_consultaTransaccionDatos< U
;U V
	protected 
readonly (
IConsultaCompras_Repositorio 7!
_consultaComprasDatos8 M
;M N
	protected 
readonly 5
)IActualizarDetalleTransaccion_Repositorio D/
#_actualizarDetallesTransaccionDatosE h
;h i
const"" 
string""  
PrefijoCodigoEntrada"" )
=""* +
$str"", 1
;""1 2
const## 
string## 
PrefijoCodigoSalida## (
=##) *
$str##+ 0
;##0 1
public&& #
AjusteInventario_Logica&& &
(&&& '(
IInventarioActualRepositorio&&' C!
inventarioActualDatos&&D Y
,&&Y Z(
IMaestrosAlmacen_Repositorio&&[ w!
maestrosAlmacenDatos	&&x �
,
&&� �&
IMovimientos_Repositorio
&&� �
movimientosDatos
&&� �
,
&&� �+
ICrearTransaccion_Repositorio
&&� �%
crearTransaccionesDatos
&&� �
,
&&� �.
 IConsultaTransaccion_Repositorio
&&� �&
consultaTransaccionDatos
&&� �
,
&&� �*
IConsultaCompras_Repositorio
&&� �"
consultaComprasDatos
&&� �
,
&&� �
IPermisos_Logica
&&� �
permiso_logica
&&� �
,
&&� �&
ICodigosOperacion_Logica
&&� �$
codigosOperacionLogica
&&� �
,
&&� �7
)IActualizarDetalleTransaccion_Repositorio
&&� �0
"actualizarDetallesTransaccionDatos
&&� �
)
&&� �
{'' 	"
_inventarioActualDatos(( "
=((# $!
inventarioActualDatos((% :
;((: ;
_movimientosDatos)) 
=)) 
movimientosDatos))  0
;))0 1
_permisosLogica** 
=** 
permiso_logica** ,
;**, -#
_codigosOperacionLogica++ #
=++$ %"
codigosOperacionLogica++& <
;++< =!
_maestrosAlmacenDatos,, !
=,," # 
maestrosAlmacenDatos,,$ 8
;,,8 9$
_crearTransaccionesDatos-- $
=--% &#
crearTransaccionesDatos--' >
;--> ?%
_consultaTransaccionDatos.. %
=..& '$
consultaTransaccionDatos..( @
;..@ A!
_consultaComprasDatos// !
=//" # 
consultaComprasDatos//$ 8
;//8 9/
#_actualizarDetallesTransaccionDatos00 /
=000 1.
"actualizarDetallesTransaccionDatos002 T
;00T U
}11 	
public44 
OperationResult44 ?
3CuadrarStockFisicoEntreInventarioActualYMovimientos44 R
(44R S
)44S T
{55 	
try66 
{77 
var88 

idEmpleado88 
=88  

.88. /
Default88/ 6
.886 7,
 IdActorNegocioEmpleadoPorDefecto887 W
;88W X
var99 
idsAlmacenes99  
=99! "!
_maestrosAlmacenDatos99# 8
.998 9
ObtenerIdsAlmacenes999 L
(99L M
)99M N
;99N O
var:: 
inventariosActuales:: '
=::( )"
_inventarioActualDatos::* @
.::@ A1
%ObtenerInventariosValorizadosActuales::A f
(::f g
idsAlmacenes::g s
)::s t
.::t u
ToList::u {
(::{ |
)::| }
;::} ~
var;; $
idsConceptosEnInventario;; ,
=;;- .
inventariosActuales;;/ B
.;;B C
Select;;C I
(;;I J
i;;J K
=>;;L N
i;;O P
.;;P Q

IdConcepto;;Q [
);;[ \
.;;\ ]
ToArray;;] d
(;;d e
);;e f
;;;f g
var<< 
saldosMovimientos<< %
=<<& '
(<<( )
AplicacionSettings<<) ;
.<<; <
Default<<< C
.<<C D"
PermitirGestionDeLotes<<D Z
?<<[ \
_movimientosDatos<<] n
.<<n o7
*ObtenerSaldosDeMovimientosPorConceptoYLote	<<o �
(
<<� �&
idsConceptosEnInventario
<<� �
,
<<� �
idsAlmacenes
<<� �
)
<<� �
:
<<� �
_movimientosDatos
<<� �
.
<<� �3
%ObtenerSaldosDeMovimientosPorConcepto
<<� �
(
<<� �&
idsConceptosEnInventario
<<� �
,
<<� �
idsAlmacenes
<<� �
)
<<� �
)
<<� �
.
<<� �
ToList
<<� �
(
<<� �
)
<<� �
;
<<� �
List== 
<==  
InventarioValorizado== )
>==) *
ajustesEntradas==+ :
===; <
new=== @
List==A E
<==E F 
InventarioValorizado==F Z
>==Z [
(==[ \
)==\ ]
;==] ^
List>> 
<>>  
InventarioValorizado>> )
>>>) *
ajustesSalidas>>+ 9
=>>: ;
new>>< ?
List>>@ D
<>>D E 
InventarioValorizado>>E Y
>>>Y Z
(>>Z [
)>>[ \
;>>\ ]
foreach?? 
(?? 
var?? 
inventarioActual?? -
in??. 0
inventariosActuales??1 D
)??D E
{@@ 
varAA 
saldo_movimientoAA (
=AA) *
saldosMovimientosAA+ <
.AA< =
SingleOrDefaultAA= L
(AAL M
smAAM O
=>AAP R
smAAS U
.AAU V
	IdAlmacenAAV _
==AA` b
inventarioActualAAc s
.AAs t
	IdAlmacenAAt }
&&	AA~ �
sm
AA� �
.
AA� �

IdConcepto
AA� �
==
AA� �
inventarioActual
AA� �
.
AA� �

IdConcepto
AA� �
&&
AA� �
sm
AA� �
.
AA� �
Lote
AA� �
==
AA� �
inventarioActual
AA� �
.
AA� �
Lote
AA� �
)
AA� �
;
AA� �
ifCC 
(CC 
saldo_movimientoCC '
==CC( *
nullCC+ /
)CC/ 0
{DD 
ifEE 
(EE 
inventarioActualEE ,
.EE, -
CantidadEE- 5
>EE6 7
$numEE8 9
)EE9 :
{FF 
ajustesEntradasGG +
.GG+ ,
AddGG, /
(GG/ 0
newGG0 3 
InventarioValorizadoGG4 H
(GGH I
)GGI J
{GGK L
	IdAlmacenGGM V
=GGW X
inventarioActualGGY i
.GGi j
	IdAlmacenGGj s
,GGs t

IdConceptoGGu 
=
GG� �
inventarioActual
GG� �
.
GG� �

IdConcepto
GG� �
,
GG� �
Lote
GG� �
=
GG� �
inventarioActual
GG� �
.
GG� �
Lote
GG� �
,
GG� �
Cantidad
GG� �
=
GG� �
inventarioActual
GG� �
.
GG� �
Cantidad
GG� �
,
GG� � 
CantidadSecundaria
GG� �
=
GG� �
inventarioActual
GG� �
.
GG� � 
CantidadSecundaria
GG� �
}
GG� �
)
GG� �
;
GG� �
}HH 
elseII 
{JJ 
ajustesSalidasKK *
.KK* +
AddKK+ .
(KK. /
newKK/ 2 
InventarioValorizadoKK3 G
(KKG H
)KKH I
{KKJ K
	IdAlmacenKKL U
=KKV W
inventarioActualKKX h
.KKh i
	IdAlmacenKKi r
,KKr s

IdConceptoKKt ~
=	KK �
inventarioActual
KK� �
.
KK� �

IdConcepto
KK� �
,
KK� �
Lote
KK� �
=
KK� �
inventarioActual
KK� �
.
KK� �
Lote
KK� �
,
KK� �
Cantidad
KK� �
=
KK� �
inventarioActual
KK� �
.
KK� �
Cantidad
KK� �
,
KK� � 
CantidadSecundaria
KK� �
=
KK� �
inventarioActual
KK� �
.
KK� � 
CantidadSecundaria
KK� �
}
KK� �
)
KK� �
;
KK� �
}LL 
}MM 
elseNN 
ifNN 
(NN 
saldo_movimientoNN -
.NN- .
CantidadNN. 6
<NN7 8
inventarioActualNN9 I
.NNI J
CantidadNNJ R
)NNR S
{OO 
ajustesEntradasPP '
.PP' (
AddPP( +
(PP+ ,
newPP, / 
InventarioValorizadoPP0 D
(PPD E
)PPE F
{PPG H
	IdAlmacenPPI R
=PPS T
saldo_movimientoPPU e
.PPe f
	IdAlmacenPPf o
,PPo p

IdConceptoPPq {
=PP| }
saldo_movimiento	PP~ �
.
PP� �

IdConcepto
PP� �
,
PP� �
Lote
PP� �
=
PP� �
saldo_movimiento
PP� �
.
PP� �
Lote
PP� �
,
PP� �
Cantidad
PP� �
=
PP� �
inventarioActual
PP� �
.
PP� �
Cantidad
PP� �
-
PP� �
saldo_movimiento
PP� �
.
PP� �
Cantidad
PP� �
,
PP� � 
CantidadSecundaria
PP� �
=
PP� �
inventarioActual
PP� �
.
PP� � 
CantidadSecundaria
PP� �
-
PP� �
saldo_movimiento
PP� �
.
PP� � 
CantidadSecundaria
PP� �
}
PP� �
)
PP� �
;
PP� �
}QQ 
elseRR 
ifRR 
(RR 
saldo_movimientoRR -
.RR- .
CantidadRR. 6
>RR7 8
inventarioActualRR9 I
.RRI J
CantidadRRJ R
)RRR S
{SS 
ajustesSalidasTT &
.TT& '
AddTT' *
(TT* +
newTT+ . 
InventarioValorizadoTT/ C
(TTC D
)TTD E
{TTF G
	IdAlmacenTTH Q
=TTR S
saldo_movimientoTTT d
.TTd e
	IdAlmacenTTe n
,TTn o

IdConceptoTTp z
=TT{ |
saldo_movimiento	TT} �
.
TT� �

IdConcepto
TT� �
,
TT� �
Lote
TT� �
=
TT� �
saldo_movimiento
TT� �
.
TT� �
Lote
TT� �
,
TT� �
Cantidad
TT� �
=
TT� �
saldo_movimiento
TT� �
.
TT� �
Cantidad
TT� �
-
TT� �
inventarioActual
TT� �
.
TT� �
Cantidad
TT� �
,
TT� � 
CantidadSecundaria
TT� �
=
TT� �
saldo_movimiento
TT� �
.
TT� � 
CantidadSecundaria
TT� �
-
TT� �
inventarioActual
TT� �
.
TT� � 
CantidadSecundaria
TT� �
}
TT� �
)
TT� �
;
TT� �
}UU 
}VV 
varWW "
costosUnitariosComprasWW *
=WW+ ,!
_consultaComprasDatosWW- B
.WWB CJ
=ObtenerValorUnitarioDePrimeraOrdenDeCompraConPrecioMayorACero	WWC �
(
WW� �
ajustesEntradas
WW� �
.
WW� �
Union
WW� �
(
WW� �
ajustesSalidas
WW� �
)
WW� �
.
WW� �
Select
WW� �
(
WW� �
a
WW� �
=>
WW� �
a
WW� �
.
WW� �

IdConcepto
WW� �
)
WW� �
.
WW� �
Distinct
WW� �
(
WW� �
)
WW� �
.
WW� �
ToArray
WW� �
(
WW� �
)
WW� �
)
WW� �
.
WW� �
ToList
WW� �
(
WW� �
)
WW� �
;
WW� �
ajustesEntradasXX 
.XX  
ForEachXX  '
(XX' (
aeXX( *
=>XX+ -
{XX. /
aeXX0 2
.XX2 3

=XXA B
(XXC D
inventariosActualesXXD W
.XXW X
SingleOrDefaultXXX g
(XXg h
iaXXh j
=>XXj l
iaXXm o
.XXo p

IdConceptoXXp z
==XXz |
aeXX} 
.	XX �

IdConcepto
XX� �
&&
XX� �
ia
XX� �
.
XX� �
	IdAlmacen
XX� �
==
XX� �
ae
XX� �
.
XX� �
	IdAlmacen
XX� �
)
XX� �
.
XX� �

XX� �
!=
XX� �
$num
XX� �
?
XX� �!
inventariosActuales
XX� �
.
XX� �
SingleOrDefault
XX� �
(
XX� �
ia
XX� �
=>
XX� �
ia
XX� �
.
XX� �

IdConcepto
XX� �
==
XX� �
ae
XX� �
.
XX� �

IdConcepto
XX� �
&&
XX� �
ia
XX� �
.
XX� �
	IdAlmacen
XX� �
==
XX� �
ae
XX� �
.
XX� �
	IdAlmacen
XX� �
)
XX� �
.
XX� �

XX� �
:
XX� �
(
XX� �"
costosUnitariosComprasYY *
.YY* +
FirstOrDefaultYY+ 9
(YY9 :
cuYY: <
=>YY= ?
cuYY@ B
.YYB C

IdConceptoYYC M
==YYN P
aeYYQ S
.YYS T

IdConceptoYYT ^
)YY^ _
!=YY_ a
nullYYa e
?YYe f"
costosUnitariosComprasYYg }
.YY} ~
FirstOrDefault	YY~ �
(
YY� �
cu
YY� �
=>
YY� �
cu
YY� �
.
YY� �

IdConcepto
YY� �
==
YY� �
ae
YY� �
.
YY� �

IdConcepto
YY� �
)
YY� �
.
YY� �
Precio
YY� �
:
YY� �
$num
YY� �
)
YY� �
)
YY� �
;
YY� �
ae
YY� �
.
YY� �

ValorTotal
YY� �
=
YY� �
ae
YY� �
.
YY� �
Cantidad
YY� �
*
YY� �
ae
YY� �
.
YY� �

YY� �
;
YY� �
}
YY� �
)
YY� �
;
YY� �
returnZZ 7
+GenerarYCrearTransaccionesAjustesInventarioZZ B
(ZZB C
idsAlmacenesZZC O
,ZZO P

idEmpleadoZZQ [
,ZZ[ \
ajustesEntradasZZ] l
,ZZl m
ajustesSalidasZZn |
)ZZ| }
;ZZ} ~
}\\ 
catch]] 
(]] 
	Exception]] 
e]] 
)]] 
{^^ 
throw__ 
new__ 
LogicaException__ )
(__) *
$str__* o
+__p q
e__r s
.__s t
Message__t {
,__{ |
e__} ~
)__~ 
;	__ �
}`` 
}aa 	
privatecc 
OperationResultcc 7
+GenerarYCrearTransaccionesAjustesInventariocc  K
(ccK L
intccL O
[ccO P
]ccP Q
idsAlmacenesccR ^
,cc^ _
intcc` c

idEmpleadoccd n
,cco p
Listccp t
<cct u!
InventarioValorizado	ccu �
>
cc� �
ajustesEntradas
cc� �
,
cc� �
List
cc� �
<
cc� �"
InventarioValorizado
cc� �
>
cc� �
ajustesSalidas
cc� �
)
cc� �
{dd 	
tryee 
{ff 
Listgg 
<gg 
Transacciongg  
>gg  ! 
transaccionesAjustesgg" 6
=gg7 8
newgg9 <
Listgg= A
<ggA B
TransaccionggB M
>ggM N
(ggN O
)ggO P
;ggP Q
foreachhh 
(hh 
varhh 
	idAlmacenhh &
inhh' )
idsAlmaceneshh* 6
)hh6 7
{ii 
varjj 
hayAjustesEntradasjj *
=jj+ ,
ajustesEntradasjj- <
.jj< =
Wherejj= B
(jjB C
aejjC E
=>jjF H
aejjI K
.jjK L
	IdAlmacenjjL U
==jjV X
	idAlmacenjjY b
)jjb c
.jjc d
Anyjjd g
(jjg h
)jjh i
;jji j
varkk 
hayAjustesSalidaskk )
=kk* +
ajustesSalidaskk, :
.kk: ;
Wherekk; @
(kk@ A
aekkA C
=>kkD F
aekkG I
.kkI J
	IdAlmacenkkJ S
==kkT V
	idAlmacenkkW `
)kk` a
.kka b
Anykkb e
(kke f
)kkf g
;kkg h
varll 

=ll& '#
_codigosOperacionLogicall( ?
.ll? @.
"ObtenerMaximoCodigoParaTransaccionll@ b
(llb c 
PrefijoCodigoEntradallc w
,llw x 
TransaccionSettings	lly �
.
ll� �
Default
ll� �
.
ll� �<
.IdTipoTransaccionEntradaBienesAjusteInventario
ll� �
)
ll� �
;
ll� �
varmm 
codigoSalidamm $
=mm% &#
_codigosOperacionLogicamm' >
.mm> ?.
"ObtenerMaximoCodigoParaTransaccionmm? a
(mma b
PrefijoCodigoSalidammb u
,mmu v 
TransaccionSettings	mmw �
.
mm� �
Default
mm� �
.
mm� �;
-IdTipoTransaccionSalidaBienesAjusteInventario
mm� �
)
mm� �
;
mm� �
ifnn 
(nn 
hayAjustesEntradasnn *
)nn* +
{oo  
transaccionesAjustespp ,
.pp, -
Addpp- 0
(pp0 1#
GenerarAjusteInventariopp1 H
(ppH I

idEmpleadoppI S
,ppS T
	idAlmacenppU ^
,pp^ _
truepp` d
,ppd e

,pps t
ajustesEntradas	ppu �
.
pp� �
Where
pp� �
(
pp� �
ae
pp� �
=>
pp� �
ae
pp� �
.
pp� �
	IdAlmacen
pp� �
==
pp� �
	idAlmacen
pp� �
)
pp� �
.
pp� �
ToList
pp� �
(
pp� �
)
pp� �
)
pp� �
)
pp� �
;
pp� �
}qq 
ifrr 
(rr 
hayAjustesSalidasrr )
)rr) *
{ss  
transaccionesAjustestt ,
.tt, -
Addtt- 0
(tt0 1#
GenerarAjusteInventariott1 H
(ttH I

idEmpleadottI S
,ttS T
	idAlmacenttU ^
,tt^ _
falsett` e
,tte f
codigoSalidattg s
,tts t
ajustesSalidas	ttu �
.
tt� �
Where
tt� �
(
tt� �
a
tt� �
=>
tt� �
a
tt� �
.
tt� �
	IdAlmacen
tt� �
==
tt� �
	idAlmacen
tt� �
)
tt� �
.
tt� �
ToList
tt� �
(
tt� �
)
tt� �
)
tt� �
)
tt� �
;
tt� �
}uu 
}vv 
returnww $
_crearTransaccionesDatosww /
.ww/ 0
CrearTransaccionesww0 B
(wwB C 
transaccionesAjusteswwC W
)wwW X
;wwX Y
}xx 
catchyy 
(yy 
	Exceptionyy 
eyy 
)yy 
{zz 
throw{{ 
new{{ 
LogicaException{{ )
({{) *
$str{{* T
+{{U V
e{{W X
.{{X Y
Message{{Y `
,{{` a
e{{b c
){{c d
;{{d e
}|| 
}}} 	
private 
Transaccion #
GenerarAjusteInventario 3
(3 4
int4 7

idEmpleado8 B
,B C
intD G
	idAlmacenH Q
,Q R
boolS W
	esEntradaX a
,a b
intc f
maximoCodigog s
,t u
Listu y
<y z!
InventarioValorizado	z �
>
� �
detalles
� �
)
� �
{
�� 	
try
�� 
{
�� 
var
�� 
fechaActual
�� 
=
��  !
DateTimeUtil
��" .
.
��. /
FechaActual
��/ :
(
��: ;
)
��; <
;
��< =
var
�� %
fechaPrimeraTransaccion
�� +
=
��, -'
_consultaTransaccionDatos
��. G
.
��G H,
ObtenerFechaPrimeraTransaccion
��H f
(
��f g
	idAlmacen
��g p
)
��p q
??
��q s
fechaActual
��t 
;�� �
var
�� 
observacion
�� 
=
��  !
$str
��" $
;
��$ %
var
�� 

�� !
=
��" #
	esEntrada
��$ -
?
��- ."
PrefijoCodigoEntrada
��. B
:
��B C!
PrefijoCodigoSalida
��C V
;
��V W
var
�� 
idTipoTransaccion
�� %
=
��& '
	esEntrada
��( 1
?
��2 3!
TransaccionSettings
��4 G
.
��G H
Default
��H O
.
��O P<
.IdTipoTransaccionEntradaBienesAjusteInventario
��P ~
:�� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �=
-IdTipoTransaccionSalidaBienesAjusteInventario��� �
;��� �
int
�� 
idMoneda
�� 
=
�� 
MaestroSettings
�� .
.
��. /
Default
��/ 6
.
��6 7)
IdDetalleMaestroMonedaSoles
��7 R
;
��R S
int
�� 
idUnidadNegocio
�� #
=
��$ %
MaestroSettings
��& 5
.
��5 6
Default
��6 =
.
��= >8
*IdDetalleMaestroUnidadDeNegocioTransversal
��> h
;
��h i
decimal
�� 
tipoDeCambio
�� $
=
��% &
$num
��' (
;
��( )
string
�� 
codigo
�� 
=
�� 

��  -
+
��. /
(
��0 1
++
��1 3
maximoCodigo
��3 ?
)
��? @
.
��@ A
ToString
��A I
(
��I J
)
��J K
;
��K L
Transaccion
�� 
ajusteInventario
�� ,
=
��- .
new
��/ 2
Transaccion
��3 >
(
��> ?
codigo
��? E
,
��E F
null
��G K
,
��K L
fechaActual
��M X
,
��X Y
idTipoTransaccion
��Z k
,
��k l
idUnidadNegocio
��m |
,
��| }
true��~ �
,��� �'
fechaPrimeraTransaccion��� �
,��� �'
fechaPrimeraTransaccion��� �
,��� �
observacion��� �
,��� �'
fechaPrimeraTransaccion��� �
,��� �

idEmpleado��� �
,��� �
$num��� �
,��� �
	idAlmacen��� �
,��� �
idMoneda��� �
,��� �
tipoDeCambio��� �
,��� �
null��� �
,��� �
	idAlmacen��� �
)��� �
;��� �
ajusteInventario
��  
.
��  !
id_comprobante
��! /
=
��0 1!
TransaccionSettings
��2 E
.
��E F
Default
��F M
.
��M N#
IdComprobanteGenerico
��N c
;
��c d
ajusteInventario
��  
.
��  !!
Detalle_transaccion
��! 4
=
��5 6"
InventarioValorizado
��7 K
.
��K L#
ToDetallesTransaccion
��L a
(
��a b
detalles
��b j
.
��j k
Where
��k p
(
��p q
ae
��q s
=>
��t v
ae
��w y
.
��y z
	IdAlmacen��z �
==��� �
	idAlmacen��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
,��� �
$str��� �
)��� �
;��� �
return
�� 
ajusteInventario
�� '
;
��' (
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* R
,
��R S
e
��T U
)
��U V
;
��V W
}
�� 
}
�� 	
public
�� 
OperationResult
�� 8
*RecalcularCostoUnitarioYTotalEnMovimientos
�� I
(
��I J
)
��J K
{
�� 	
try
�� 
{
�� 
var
�� 
idsAlmacenes
��  
=
��! "#
_maestrosAlmacenDatos
��# 8
.
��8 9!
ObtenerIdsAlmacenes
��9 L
(
��L M
)
��M N
;
��N O
var
�� !
inventariosActuales
�� '
=
��( )$
_inventarioActualDatos
��* @
.
��@ A3
%ObtenerInventariosValorizadosActuales
��A f
(
��f g
idsAlmacenes
��g s
)
��s t
.
��t u
ToList
��u {
(
��{ |
)
��| }
;
��} ~
var
�� &
idsConceptosEnInventario
�� ,
=
��- .!
inventariosActuales
��/ B
.
��B C
Select
��C I
(
��I J
i
��J K
=>
��L N
i
��O P
.
��P Q

IdConcepto
��Q [
)
��[ \
.
��\ ]
Distinct
��] e
(
��e f
)
��f g
.
��g h
ToArray
��h o
(
��o p
)
��p q
;
��q r
var
�� 
fechaActual
�� 
=
��  !
DateTimeUtil
��" .
.
��. /
FechaActual
��/ :
(
��: ;
)
��; <
;
��< =
var
�� +
tiposOperacionSegunInventario
�� 1
=
��2 3
Diccionario
��4 ?
.
��? @R
CTiposDeTransaccionMovimientoDeBienesConCostoUnitarioSegunInventario��@ �
;��� �
var
�� &
tiposOperacionSegunOrden
�� ,
=
��- .
Diccionario
��/ :
.
��: ;T
ETiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeLaOrden��; �
;��� �
var
�� 2
$tiposOperacionSegunTransaccionOrigen
�� 8
=
��9 :
Diccionario
��; F
.
��F Gj
[TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeMovimientoDeTransaccionOrigen��G �
;��� �
foreach
�� 
(
�� 
var
�� 
	idAlmacen
�� &
in
��' )
idsAlmacenes
��* 6
)
��6 7
{
�� 
var
�� %
fechaPrimeraTransaccion
�� /
=
��0 1'
_consultaTransaccionDatos
��2 K
.
��K L,
ObtenerFechaPrimeraTransaccion
��L j
(
��j k
	idAlmacen
��k t
)
��t u
??
��v x
fechaActual��y �
;��� �
var
�� 
movimientos
�� #
=
��$ %
_movimientosDatos
��& 7
.
��7 8:
,ObtenerMovimientosDeAlmacenesConOrdenYOrigen
��8 d
(
��d e
	idAlmacen
��e n
,
��n o'
idsConceptosEnInventario��p �
,��� �'
fechaPrimeraTransaccion��� �
,��� �
fechaActual��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
if
�� 
(
�� 
movimientos
�� "
.
��" #
Count
��# (
>
��( )
$num
��) *
)
��* +
{
�� 
foreach
�� 
(
��  !
var
��! $

idConcepto
��% /
in
��0 2&
idsConceptosEnInventario
��3 K
)
��K L
{
�� 
var
�� &
entradaAjustesInventario
��  8
=
��9 :
movimientos
��; F
.
��F G
SingleOrDefault
��G V
(
��V W
m
��W X
=>
��Y [
m
��\ ]
.
��] ^
IdTipoTransaccion
��^ o
==
��p r"
TransaccionSettings��s �
.��� �
Default��� �
.��� �>
.IdTipoTransaccionEntradaBienesAjusteInventario��� �
&&��� �
m��� �
.��� �

IdConcepto��� �
==��� �

idConcepto��� �
)��� �
;��� �
decimal
�� #
saldoImporte
��$ 0
=
��1 2&
entradaAjustesInventario
��3 K
!=
��K M
null
��M Q
?
��Q R&
entradaAjustesInventario
��S k
.
��k l
ImporteTotal
��l x
:
��x y
$num
��y z
;
��z {
decimal
�� #

��$ 1
=
��2 3&
entradaAjustesInventario
��4 L
!=
��M O
null
��P T
?
��U V&
entradaAjustesInventario
��W o
.
��o p
Cantidad
��p x
:
��y z
$num
��{ |
;
��| }
decimal
�� ##
costoUnitarioPromedio
��$ 9
=
��: ;&
entradaAjustesInventario
��< T
!=
��U W
null
��X \
?
��] ^&
entradaAjustesInventario
��_ w
.
��w x
ImporteUnitario��x �
:��� �
$num��� �
;��� �
var
�� !
movimientosConcepto
��  3
=
��4 5
movimientos
��6 A
.
��A B
Where
��B G
(
��G H
m
��H I
=>
��J L
m
��M N
.
��N O

IdConcepto
��O Y
==
��Z \

idConcepto
��] g
&&
��h j
(
��k l
(
��m n'
entradaAjustesInventario��n �
!=��� �
null��� �
&&��� �
m��� �
.��� �$
IdDetalleTransaccion��� �
!=��� �(
entradaAjustesInventario��� �
.��� �$
IdDetalleTransaccion��� �
)��� �
||��� �(
entradaAjustesInventario��� �
==��� �
null��� �
)��� �
)��� �
.��� �
OrderBy��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Fecha��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� #
(
��$ %
var
��% (

movimiento
��) 3
in
��4 6!
movimientosConcepto
��7 J
)
��J K
{
�� 
var
��  #
factor
��$ *
=
��+ ,

movimiento
��- 7
.
��7 8
	EsEntrada
��8 A
?
��B C
$num
��D E
:
��F G
-
��H I
$num
��I J
;
��J K

��  -
+=
��. 0

movimiento
��1 ;
.
��; <
Cantidad
��< D
*
��E F
factor
��G M
;
��M N
if
��  "
(
��# $+
tiposOperacionSegunInventario
��$ A
.
��A B
Contains
��B J
(
��J K

movimiento
��K U
.
��U V
IdTipoTransaccion
��V g
)
��g h
)
��h i
{
��  !

movimiento
��$ .
.
��. /
ImporteUnitario
��/ >
=
��? @#
costoUnitarioPromedio
��A V
;
��V W

movimiento
��$ .
.
��. /
ImporteTotal
��/ ;
=
��< =

movimiento
��> H
.
��H I
Cantidad
��I Q
*
��R S

movimiento
��T ^
.
��^ _
ImporteUnitario
��_ n
;
��n o
}
��  !
else
��  $
if
��% '
(
��( )&
tiposOperacionSegunOrden
��) A
.
��A B
Contains
��B J
(
��J K

movimiento
��K U
.
��U V
IdTipoTransaccion
��V g
)
��g h
)
��h i
{
��  !

movimiento
��$ .
.
��. /
ImporteTotal
��/ ;
=
��< =!
TransaccionSettings
��> Q
.
��Q R
Default
��R Y
.
��Y Z
AplicaLeyAmazonia
��Z k
?
��l m

movimiento
��n x
.
��x y 
ImporteTotalOrden��y �
:��� �

movimiento��� �
.��� �!
ImporteTotalOrden��� �
-��� �

movimiento��� �
.��� �
IgvOrden��� �
;��� �

movimiento
��$ .
.
��. /
ImporteUnitario
��/ >
=
��? @

movimiento
��A K
.
��K L
Cantidad
��L T
!=
��T V
$num
��V W
?
��W X

movimiento
��Y c
.
��c d
ImporteTotal
��d p
/
��q r

movimiento
��s }
.
��} ~
Cantidad��~ �
:��� �
$num��� �
;��� �
}
��  !
else
��  $
if
��% '
(
��( )2
$tiposOperacionSegunTransaccionOrigen
��) M
.
��M N
Contains
��N V
(
��V W

movimiento
��W a
.
��a b
IdTipoTransaccion
��b s
)
��s t
)
��t u
{
��  !
var
��$ '
movimientoOrigen
��( 8
=
��9 :

movimiento
��; E
.
��E F+
IdTransaccionMovimientoOrigen
��F c
!=
��d f
null
��g k
?
��l m
movimientos
��n y
.
��y z
FirstOrDefault��z �
(��� �
m��� �
=>��� �
m��� �
.��� �

==��� �

movimiento��� �
.��� �-
IdTransaccionMovimientoOrigen��� �
&&��� �
m��� �
.��� �

IdConcepto��� �
==��� �

idConcepto��� �
)��� �
:��� �
null��� �
;��� �

movimiento
��$ .
.
��. /
ImporteUnitario
��/ >
=
��? @
movimientoOrigen
��A Q
!=
��Q S
null
��S W
?
��W X
movimientos
��Y d
.
��d e
FirstOrDefault
��e s
(
��s t
m
��t u
=>
��v x
m
��y z
.
��z {

==��� �

movimiento��� �
.��� �-
IdTransaccionMovimientoOrigen��� �
&&��� �
m��� �
.��� �

IdConcepto��� �
==��� �

idConcepto��� �
)��� �
.��� �
ImporteUnitario��� �
:��� �%
costoUnitarioPromedio��� �
;��� �

movimiento
��$ .
.
��. /
ImporteTotal
��/ ;
=
��< =

movimiento
��> H
.
��H I
ImporteUnitario
��I X
*
��Y Z

movimiento
��[ e
.
��e f
Cantidad
��f n
;
��n o
}
��  !
saldoImporte
��  ,
+=
��- /

movimiento
��0 :
.
��: ;
ImporteTotal
��; G
*
��H I
factor
��J P
;
��P Q#
costoUnitarioPromedio
��  5
=
��6 7

��8 E
!=
��E G
$num
��G H
?
��H I
saldoImporte
��J V
/
��W X

��Y f
:
��f g
$num
��g h
;
��h i
}
�� 
var
�� &
inventarioActualConcepto
��  8
=
��9 :!
inventariosActuales
��; N
.
��N O
SingleOrDefault
��O ^
(
��^ _
i
��_ `
=>
��a c
i
��d e
.
��e f
	IdAlmacen
��f o
==
��p r
	idAlmacen
��s |
&&
��} 
i��� �
.��� �

IdConcepto��� �
==��� �

idConcepto��� �
)��� �
;��� �
if
�� 
(
�� &
inventarioActualConcepto
�� 7
!=
��7 9
null
��: >
)
��> ?
{
�� &
inventarioActualConcepto
��  8
.
��8 9

��9 F
=
��G H#
costoUnitarioPromedio
��I ^
;
��^ _&
inventarioActualConcepto
��  8
.
��8 9

ValorTotal
��9 C
=
��D E
saldoImporte
��F R
;
��R S
}
�� 
}
�� !
inventariosActuales
�� +
.
��+ ,
ForEach
��, 3
(
��3 4
i
��4 5
=>
��6 8
movimientos
��9 D
.
��D E
Add
��E H
(
��H I
new
��I L
MovimientoAlmacen
��M ^
(
��^ _
)
��_ `
{
��a b"
IdDetalleTransaccion
��c w
=
��x y
i
��z {
.
��{ |#
IdDetalleTransaccion��| �
,��� �
ImporteUnitario��� �
=��� �
i��� �
.��� �

,��� �
ImporteTotal��� �
=��� �
i��� �
.��� �

ValorTotal��� �
}��� �
)��� �
)��� �
;��� �1
#_actualizarDetallesTransaccionDatos
�� ;
.
��; <0
"ActualizarValorUnitarioyValorTotal
��< ^
(
��^ _
movimientos
��_ j
)
��j k
;
��k l
}
�� 
}
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +!
OperationResultEnum
��+ >
.
��> ?
Success
��? F
)
��F G
;
��G H
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* s
,
��s t
e
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
�� 
}�� ��
SD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Almacen\InventarioActual_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Almacen# *
{ 
public 

class #
InventarioActual_Logica (
:( )$
IInventarioActual_Logica* B
{ 
	protected 
readonly (
IInventarioActualRepositorio 7"
_inventarioActualDatos8 N
;N O
	protected 
readonly (
IMaestrosAlmacen_Repositorio 7!
_maestrosAlmacenDatos8 M
;M N
	protected 
readonly 
IPermisos_Logica +
_permisosLogica, ;
;; <
	protected 
readonly $
ICodigosOperacion_Logica 3#
_codigosOperacionLogica4 K
;K L
	protected 
readonly )
ICrearTransaccion_Repositorio 8!
_crearTranaccionDatos9 N
;N O
	protected 
readonly !
IConcepto_Repositorio 0
_conceptoDatos1 ?
;? @
	protected 
readonly 
IActor_Repositorio -
_actorDatos. 9
;9 :
	protected!! 
readonly!! 0
$ICrearDetalleTransaccion_Repositorio!! ?*
_crearDetallesTransaccionDatos!!@ ^
;!!^ _
	protected"" 
readonly"" 3
'IConsultaDetalleTransaccion_Repositorio"" B-
!_consultaDetallesTransaccionDatos""C d
;""d e
	protected## 
readonly## 3
'IEliminarDetalleTransaccion_Repositorio## B-
!_eliminarDetallesTransaccionDatos##C d
;##d e
public(( #
InventarioActual_Logica(( &
(((& '(
IInventarioActualRepositorio((' C!
inventarioActualDatos((D Y
,((Y Z
IPermisos_Logica(([ k
permiso_logica((l z
,((z {%
ICodigosOperacion_Logica	((| �$
codigosOperacionLogica
((� �
,
((� �*
IMaestrosAlmacen_Repositorio
((� �"
maestrosAlmacenDatos
((� �
,
((� �+
ICrearTransaccion_Repositorio
((� �"
crearTranaccionDatos
((� �
,
((� �#
IConcepto_Repositorio
((� �

((� �
,
((� � 
IActor_Repositorio
((� �

actorDatos
((� �
,
((� �2
$ICrearDetalleTransaccion_Repositorio
((� �+
crearDetallesTransaccionDatos
((� �
,
((� �5
'IConsultaDetalleTransaccion_Repositorio
((� �.
 consultaDetallesTransaccionDatos
((� �
,
((� �5
'IEliminarDetalleTransaccion_Repositorio
((� �.
 eliminarDetallesTransaccionDatos
((� �
)
((� �
{)) 	"
_inventarioActualDatos** "
=**# $!
inventarioActualDatos**% :
;**: ;
_permisosLogica++ 
=++ 
permiso_logica++ ,
;++, -#
_codigosOperacionLogica,, #
=,,$ %"
codigosOperacionLogica,,& <
;,,< =!
_maestrosAlmacenDatos-- !
=--" # 
maestrosAlmacenDatos--$ 8
;--8 9!
_crearTranaccionDatos.. !
=.." # 
crearTranaccionDatos..$ 8
;..8 9
_conceptoDatos// 
=// 

;//* +
_actorDatos00 
=00 

actorDatos00 $
;00$ %*
_crearDetallesTransaccionDatos11 *
=11+ ,)
crearDetallesTransaccionDatos11- J
;11J K-
!_consultaDetallesTransaccionDatos22 -
=22. /,
 consultaDetallesTransaccionDatos220 P
;22P Q-
!_eliminarDetallesTransaccionDatos33 -
=33. /,
 eliminarDetallesTransaccionDatos330 P
;33P Q
}44 	
public77 
Transaccion77 #
GenerarInventarioActual77 2
(772 3
int773 6

idEmpleado777 A
,77A B
int77C F
idCentroDeAtencion77G Y
)77Y Z
{88 	
try99 
{:: 
	Operacion;; %
operacionInventarioActual;; 3
=;;4 5
new;;6 9
	Operacion;;: C
(;;C D"
_inventarioActualDatos;;D Z
.;;Z [3
&ObtenerUltimaOperacionInventarioActual	;;[ �
(
;;� �
)
;;� �
)
;;� �
;
;;� �
var<< 
fechaActual<< 
=<<  !
DateTimeUtil<<" .
.<<. /
FechaActual<</ :
(<<: ;
)<<; <
;<<< =
Transaccion== 
inventarioFisico== ,
===- .!
CrearInventarioActual==/ D
(==D E%
operacionInventarioActual==E ^
.==^ _
Id==_ a
,==a b%
operacionInventarioActual==c |
.==| }

,
==� �

idEmpleado
==� �
,
==� �
fechaActual
==� �
,
==� �
$str
==� �
,
==� � 
idCentroDeAtencion
==� �
)
==� �
;
==� �
return>> 
inventarioFisico>> '
;>>' (
}?? 
catch@@ 
(@@ 
	Exception@@ 
e@@ 
)@@ 
{AA 
throwBB 
newBB 
LogicaExceptionBB )
(BB) *
$strBB* X
,BBX Y
eBBZ [
)BB[ \
;BB\ ]
}CC 
}DD 	
publicGG 
TransaccionGG !
CrearInventarioActualGG 0
(GG0 1
longGG1 5'
idOperacionInventarioActualGG6 Q
,GGQ R
longGGS W

,GGe f
intGGg j

idEmpleadoGGk u
,GGu v
DateTimeGGw 

GG� �
,
GG� �
string
GG� �
observacion
GG� �
,
GG� �
int
GG� � 
idCentroDeAtencion
GG� �
)
GG� �
{HH 	
tryII 
{JJ 
varKK 
sufijoCodigoKK  
=KK! "
$strKK# '
;KK' (
varLL 
accionARealizarLL #
=LL$ %
MaestroSettingsLL& 5
.LL5 6
DefaultLL6 =
.LL= >+
IdDetalleMaestroAccionConfirmarLL> ]
;LL] ^
intMM 
idMonedaMM 
=MM 
MaestroSettingsMM .
.MM. /
DefaultMM/ 6
.MM6 7'
IdDetalleMaestroMonedaSolesMM7 R
;MMR S
intNN 
idUnidadNegocioNN #
=NN$ %
MaestroSettingsNN& 5
.NN5 6
DefaultNN6 =
.NN= >6
*IdDetalleMaestroUnidadDeNegocioTransversalNN> h
;NNh i
decimalOO 
tipoDeCambioOO $
=OO% &
$numOO' (
;OO( )
_permisosLogicaQQ 
.QQ  

(QQ- .

idEmpleadoQQ. 8
,QQ8 9
accionARealizarQQ: I
,QQI J
TransaccionSettingsQQK ^
.QQ^ _
DefaultQQ_ f
.QQf g.
!IdTipoTransaccionInventarioActual	QQg �
,
QQ� �
idUnidadNegocio
QQ� �
)
QQ� �
;
QQ� �
stringSS 
codigoSS 
=SS #
_codigosOperacionLogicaSS  7
.SS7 81
%ObtenerSiguienteCodigoParaFacturacionSS8 ]
(SS] ^
sufijoCodigoSS^ j
,SSj k
TransaccionSettingsSSl 
.	SS �
Default
SS� �
.
SS� �/
!IdTipoTransaccionInventarioActual
SS� �
)
SS� �
;
SS� �
TransaccionUU 
inventarioFisicoUU ,
=UU- .
newUU/ 2
TransaccionUU3 >
(UU> ?
codigoUU? E
,UUE F'
idOperacionInventarioActualUUG b
,UUb c

,UUq r 
TransaccionSettings	UUs �
.
UU� �
Default
UU� �
.
UU� �/
!IdTipoTransaccionInventarioActual
UU� �
,
UU� �
idUnidadNegocio
UU� �
,
UU� �
true
UU� �
,
UU� �

UU� �
,
UU� �

UU� �
,
UU� �
observacion
UU� �
,
UU� �

UU� �
,
UU� �

idEmpleado
UU� �
,
UU� �
$num
UU� �
,
UU� � 
idCentroDeAtencion
UU� �
,
UU� �
idMoneda
UU� �
,
UU� �
tipoDeCambio
UU� �
,
UU� �
null
UU� �
,
UU� � 
idCentroDeAtencion
UU� �
)
UU� �
;
UU� �
inventarioFisicoVV  
.VV  !
id_comprobanteVV! /
=VV0 1

;VV? @
Estado_transaccionWW "
estadoWW# )
=WW* +
newWW, /
Estado_transaccionWW0 B
(WWB C

idEmpleadoWWC M
,WWM N
MaestroSettingsWWO ^
.WW^ _
DefaultWW_ f
.WWf g-
 IdDetalleMaestroEstadoConfirmado	WWg �
,
WW� �

WW� �
,
WW� �
$str
WW� �
)
WW� �
;
WW� �
inventarioFisicoXX  
.XX  !
Estado_transaccionXX! 3
.XX3 4
AddXX4 7
(XX7 8
estadoXX8 >
)XX> ?
;XX? @
returnZZ 
inventarioFisicoZZ '
;ZZ' (
}\\ 
catch]] 
(]] 
	Exception]] 
e]] 
)]] 
{^^ 
throw__ 
new__ 
LogicaException__ )
(__) *
$str__* L
,__L M
e__N O
)__O P
;__P Q
}`` 
}aa 	
publiccc 
Listcc 
<cc 
Detalle_transaccioncc '
>cc' (4
(ResolverYCrearDetallesDeInventarioActualcc) Q
(ccQ R
intccR U
idCentroDeAtencionccV h
)cch i
{dd 	
tryee 
{ff 
Listgg 
<gg 
Detalle_transacciongg (
>gg( )
detallesgg* 2
=gg3 4
newgg5 8
Listgg9 =
<gg= >
Detalle_transacciongg> Q
>ggQ R
(ggR S
)ggS T
;ggT U
Listii 
<ii 
Detalle_transaccionii (
>ii( )$
detallesInventarioFisicoii* B
=iiC D"
_inventarioActualDatosiiE [
.ii[ \E
8ObtenerDetallesInventarioActualIncluyendoConceptoNegocio	ii\ �
(
ii� � 
idCentroDeAtencion
ii� �
)
ii� �
.
ii� �
ToList
ii� �
(
ii� �
)
ii� �
;
ii� �
varkk $
conceptosDeNegocioActualkk ,
=kk- .!
_maestrosAlmacenDatoskk/ D
.kkD E*
ObtenerIdsConceptosComercialeskkE c
(kkc d
)kkd e
;kke f
Listll 
<ll 
intll 
>ll 
idConceptosAAgregarll -
=ll. /$
detallesInventarioFisicoll0 H
!=llI K
nullllL P
?llQ R$
conceptosDeNegocioActualllS k
.llk l
Exceptlll r
(llr s%
detallesInventarioFisico	lls �
.
ll� �
Select
ll� �
(
ll� �
dt
ll� �
=>
ll� �
dt
ll� �
.
ll� �!
id_concepto_negocio
ll� �
)
ll� �
)
ll� �
.
ll� �
ToList
ll� �
(
ll� �
)
ll� �
:
ll� �&
conceptosDeNegocioActual
ll� �
.
ll� �
ToList
ll� �
(
ll� �
)
ll� �
;
ll� �
foreachoo 
(oo 
varoo 

idConceptooo '
inoo( *
idConceptosAAgregaroo+ >
)oo> ?
{pp 
detallesqq 
.qq 
Addqq  
(qq  !
newqq! $
Detalle_transaccionqq% 8
(qq8 9
$numqq9 :
,qq: ;

idConceptoqq< F
,qqF G
$strqqH [
,qq[ \
$numqq] _
,qq_ `
$numqqa c
,qqc d
nullqqe i
,qqi j
$numqqk l
,qql m
nullqqn r
,qqr s
nullqqt x
,qqx y
$numqqz |
,qq| }
$num	qq~ �
,
qq� �
$num
qq� �
)
qq� �
)
qq� �
;
qq� �
}rr 
returnss 
detallesss 
;ss  
}tt 
catchuu 
(uu 
	Exceptionuu 
)uu 
{vv 
throwww 
newww 
LogicaExceptionww )
(ww) *
$strww* c
)wwc d
;wwd e
}xx 
}yy 	
public|| 
List|| 
<|| 
InventarioFisico|| $
>||$ %&
InventariosFisicosActuales||& @
(||@ A
List||A E
<||E F
ItemGenericoBase||F V
>||V W
	almacenes||X a
,||a b
bool||c g
todasLasFamilias||h x
,||x y
int||z }
[||} ~
]||~ 
idsFamilias
||� �
)
||� �
{}} 	
try~~ 
{ 
var
�� 
idsAlmacenes
��  
=
��! "
	almacenes
��# ,
.
��, -
Select
��- 3
(
��3 4
a
��4 5
=>
��6 8
a
��9 :
.
��: ;
Id
��; =
)
��= >
.
��> ?
ToArray
��? F
(
��F G
)
��G H
;
��H I
idsFamilias
�� 
=
�� 
todasLasFamilias
�� .
?
��/ 0#
_maestrosAlmacenDatos
��1 F
.
��F G#
ObtenerFamiliasBienes
��G \
(
��\ ]
)
��] ^
.
��^ _
Select
��_ e
(
��e f
f
��f g
=>
��h j
f
��k l
.
��l m
Id
��m o
)
��o p
.
��p q
ToArray
��q x
(
��x y
)
��y z
:
��{ |
idsFamilias��} �
;��� �
var
�� 
inventarios
�� 
=
��  !$
_inventarioActualDatos
��" 8
.
��8 9/
!ObtenerInventariosFisicosActuales
��9 Z
(
��Z [
idsAlmacenes
��[ g
,
��g h
idsFamilias
��i t
)
��t u
.
��u v
ToList
��v |
(
��| }
)
��} ~
;
��~ 
inventarios
�� 
.
�� 
ForEach
�� #
(
��# $
i
��$ %
=>
��& (
i
��) *
.
��* +

��+ 8
=
��9 :
	almacenes
��; D
.
��D E
Single
��E K
(
��K L
a
��L M
=>
��N P
a
��Q R
.
��R S
Id
��S U
==
��V X
i
��Y Z
.
��Z [
	IdAlmacen
��[ d
)
��d e
.
��e f
Nombre
��f l
)
��l m
;
��m n
return
�� 
inventarios
�� "
;
��" #
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* W
,
��W X
e
��Y Z
)
��Z [
;
��[ \
}
�� 
}
�� 	
public
�� 
List
�� 
<
��  
InventarioSemaforo
�� &
>
��& ')
InventariosSemaforoActuales
��( C
(
��C D
List
��D H
<
��H I
ItemGenericoBase
��I Y
>
��Y Z
	almacenes
��[ d
,
��d e
bool
��f j
todasLasFamilias
��k {
,
��{ |
int��} �
[��� �
]��� �
idsFamilias��� �
,��� �
bool��� �

estadoBajo��� �
,��� �
bool��� �
estadoNormal��� �
,��� �
bool��� �

estadoAlto��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
idsAlmacenes
��  
=
��! "
	almacenes
��# ,
.
��, -
Select
��- 3
(
��3 4
a
��4 5
=>
��6 8
a
��9 :
.
��: ;
Id
��; =
)
��= >
.
��> ?
ToArray
��? F
(
��F G
)
��G H
;
��H I
List
�� 
<
�� 
int
�� 
>
�� 
nivelesRequeridos
�� +
=
��, -
new
��. 1
List
��2 6
<
��6 7
int
��7 :
>
��: ;
(
��; <
)
��< =
;
��= >
nivelesRequeridos
�� !
.
��! "
Add
��" %
(
��% &
(
��& '
int
��' *
)
��* +$
NivelStockSemaforoEnum
��+ A
.
��A B

��B O
)
��O P
;
��P Q
if
�� 
(
�� 

estadoAlto
�� 
)
�� 
nivelesRequeridos
��  1
.
��1 2
Add
��2 5
(
��5 6
(
��6 7
int
��7 :
)
��: ;$
NivelStockSemaforoEnum
��; Q
.
��Q R
Alto
��R V
)
��V W
;
��W X
if
�� 
(
�� 

estadoBajo
�� 
)
�� 
nivelesRequeridos
��  1
.
��1 2
Add
��2 5
(
��5 6
(
��6 7
int
��7 :
)
��: ;$
NivelStockSemaforoEnum
��; Q
.
��Q R
Bajo
��R V
)
��V W
;
��W X
if
�� 
(
�� 
estadoNormal
��  
)
��  !
nivelesRequeridos
��" 3
.
��3 4
Add
��4 7
(
��7 8
(
��8 9
int
��9 <
)
��< =$
NivelStockSemaforoEnum
��= S
.
��S T
Normal
��T Z
)
��Z [
;
��[ \
idsFamilias
�� 
=
�� 
todasLasFamilias
�� .
?
��/ 0#
_maestrosAlmacenDatos
��1 F
.
��F G#
ObtenerFamiliasBienes
��G \
(
��\ ]
)
��] ^
.
��^ _
Select
��_ e
(
��e f
f
��f g
=>
��h j
f
��k l
.
��l m
Id
��m o
)
��o p
.
��p q
ToArray
��q x
(
��x y
)
��y z
:
��{ |
idsFamilias��} �
;��� �
var
�� 
inventarios
�� 
=
��  !$
_inventarioActualDatos
��" 8
.
��8 90
"ObtenerInventariosSemaforoActuales
��9 [
(
��[ \
idsAlmacenes
��\ h
,
��h i
idsFamilias
��j u
)
��u v
.
��v w
ToList
��w }
(
��} ~
)
��~ 
;�� �
inventarios
�� 
=
�� 
inventarios
�� )
.
��) *
Where
��* /
(
��/ 0
i
��0 1
=>
��2 4
nivelesRequeridos
��5 F
.
��F G
Contains
��G O
(
��O P
i
��P Q
.
��Q R
ValorSemaforoInt
��R b
)
��b c
)
��c d
.
��d e
OrderBy
��e l
(
��l m
i
��m n
=>
��o q
i
��r s
.
��s t
Concepto
��t |
)
��| }
.
��} ~
ToList��~ �
(��� �
)��� �
;��� �
inventarios
�� 
.
�� 
ForEach
�� #
(
��# $
i
��$ %
=>
��& (
i
��) *
.
��* +

��+ 8
=
��9 :
	almacenes
��; D
.
��D E
Single
��E K
(
��K L
a
��L M
=>
��N P
a
��Q R
.
��R S
Id
��S U
==
��V X
i
��Y Z
.
��Z [
	IdAlmacen
��[ d
)
��d e
.
��e f
Nombre
��f l
)
��l m
;
��m n
return
�� 
inventarios
�� "
;
��" #
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* e
,
��e f
e
��g h
)
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
�� "
InventarioValorizado
�� (
>
��( ),
InventariosValorizadosActuales
��* H
(
��H I
List
��I M
<
��M N
ItemGenericoBase
��N ^
>
��^ _
	almacenes
��` i
,
��i j
bool
��k o
todasLasFamilias��p �
,��� �
int��� �
[��� �
]��� �
idsFamilias��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
idsAlmacenes
��  
=
��! "
	almacenes
��# ,
.
��, -
Select
��- 3
(
��3 4
a
��4 5
=>
��6 8
a
��9 :
.
��: ;
Id
��; =
)
��= >
.
��> ?
ToArray
��? F
(
��F G
)
��G H
;
��H I
List
�� 
<
�� 
int
�� 
>
�� 
nivelesRequeridos
�� +
=
��, -
new
��. 1
List
��2 6
<
��6 7
int
��7 :
>
��: ;
(
��; <
)
��< =
;
��= >
idsFamilias
�� 
=
�� 
todasLasFamilias
�� .
?
��/ 0#
_maestrosAlmacenDatos
��1 F
.
��F G#
ObtenerFamiliasBienes
��G \
(
��\ ]
)
��] ^
.
��^ _
Select
��_ e
(
��e f
f
��f g
=>
��h j
f
��k l
.
��l m
Id
��m o
)
��o p
.
��p q
ToArray
��q x
(
��x y
)
��y z
:
��{ |
idsFamilias��} �
;��� �
var
�� 
inventarios
�� 
=
��  !$
_inventarioActualDatos
��" 8
.
��8 93
%ObtenerInventariosValorizadosActuales
��9 ^
(
��^ _
idsAlmacenes
��_ k
,
��k l
idsFamilias
��m x
)
��x y
.
��y z
ToList��z �
(��� �
)��� �
;��� �
inventarios
�� 
.
�� 
ForEach
�� #
(
��# $
i
��$ %
=>
��& (
i
��) *
.
��* +

��+ 8
=
��9 :
	almacenes
��; D
.
��D E
Single
��E K
(
��K L
a
��L M
=>
��N P
a
��Q R
.
��R S
Id
��S U
==
��V X
i
��Y Z
.
��Z [
	IdAlmacen
��[ d
)
��d e
.
��e f
Nombre
��f l
)
��l m
;
��m n
return
�� 
inventarios
�� "
;
��" #
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Z
,
��Z [
e
��\ ]
)
��] ^
;
��^ _
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� +
Reporte_Inventario_Valorizado
�� 1
>
��1 2/
!ObtenerInventarioValorizadoActual
��3 T
(
��T U
int
��U X
	idAlmacen
��Y b
,
��b c
int
��d g%
idCentroAtencionPrecios
��h 
,�� �
int��� �
[��� �
]��� �#
idsConceptosBasicos��� �
,��� �
int��� �
[��� �
]��� �+
idsValoresDeCaracteristicas��� �
)��� �
{
�� 	
try
�� 
{
�� 
if
�� 
(
�� !
idsConceptosBasicos
�� '
.
��' (
Length
��( .
==
��/ 1
$num
��2 3
&&
��4 6)
idsValoresDeCaracteristicas
��7 R
.
��R S
Length
��S Y
==
��Z \
$num
��] ^
)
��^ _
{
�� 
return
�� $
_inventarioActualDatos
�� 1
.
��1 2%
ObtenerInventarioActual
��2 I
(
��I J
	idAlmacen
��J S
,
��S T%
idCentroAtencionPrecios
��U l
)
��l m
.
��m n
ToList
��n t
(
��t u
)
��u v
;
��v w
}
�� 
if
�� 
(
�� !
idsConceptosBasicos
�� '
.
��' (
Length
��( .
>
��/ 0
$num
��1 2
&&
��3 5)
idsValoresDeCaracteristicas
��6 Q
.
��Q R
Length
��R X
>
��Y Z
$num
��[ \
)
��\ ]
{
�� 
return
�� $
_inventarioActualDatos
�� 1
.
��1 2@
2ObtenerInventarioActualPorCaracteristicasYFamilias
��2 d
(
��d e
	idAlmacen
��e n
,
��n o&
idCentroAtencionPrecios��p �
,��� �+
idsValoresDeCaracteristicas��� �
,��� �#
idsConceptosBasicos��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� !
idsConceptosBasicos
�� '
.
��' (
Length
��( .
>
��/ 0
$num
��1 2
&&
��3 5)
idsValoresDeCaracteristicas
��6 Q
.
��Q R
Length
��R X
==
��Y [
$num
��\ ]
)
��] ^
{
�� 
return
�� $
_inventarioActualDatos
�� 1
.
��1 20
"ObtenerInventarioActualPorFamilias
��2 T
(
��T U
	idAlmacen
��U ^
,
��^ _%
idCentroAtencionPrecios
��` w
,
��w x"
idsConceptosBasicos��y �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� !
idsConceptosBasicos
�� '
.
��' (
Length
��( .
==
��/ 1
$num
��2 3
&&
��4 6)
idsValoresDeCaracteristicas
��7 R
.
��R S
Length
��S Y
>
��Z [
$num
��\ ]
)
��] ^
{
�� 
return
�� $
_inventarioActualDatos
�� 1
.
��1 27
)ObtenerInventarioActualPorCaracteristicas
��2 [
(
��[ \
	idAlmacen
��\ e
,
��e f%
idCentroAtencionPrecios
��g ~
,
��~ +
idsValoresDeCaracteristicas��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* d
)
��d e
;
��e f
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* R
,
��R S
e
��T U
)
��U V
;
��V W
}
�� 
}
�� 	
public
�� 
OperationResult
�� #
CrearInventarioActual
�� 4
(
��4 5
int
��5 8
	idAlmacen
��9 B
,
��B C
int
��D G

idEmpleado
��H R
)
��R S
{
�� 	
List
�� 
<
�� !
Detalle_transaccion
�� $
>
��$ %
detalles
��& .
;
��. /
OperationResult
�� $
resultInventarioFisico
�� 2
=
��3 4
null
��5 9
;
��9 :
detalles
�� 
=
�� 6
(ResolverYCrearDetallesDeInventarioActual
�� ?
(
��? @
	idAlmacen
��@ I
)
��I J
;
��J K
if
�� 
(
�� 
detalles
�� 
.
�� 
Count
�� 
(
�� 
)
��  
>
��! "
$num
��# $
)
��$ %
{
�� 
Transaccion
�� 
inventarioFisico
�� ,
=
��- .%
GenerarInventarioActual
��/ F
(
��F G

idEmpleado
��G Q
,
��Q R
	idAlmacen
��S \
)
��\ ]
;
��] ^$
resultInventarioFisico
�� &
=
��' (#
_crearTranaccionDatos
��) >
.
��> ?8
*CrearTransaccionYDetallesTransaccionMasivo
��? i
(
��i j
inventarioFisico
��j z
,
��z {
detalles��| �
)��� �
;��� �
}
�� 
return
�� $
resultInventarioFisico
�� )
;
��) *
}
�� 	
public
�� 
OperationResult
�� S
ECrearDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico
�� d
(
��d e
int
��e h
	idFamilia
��i r
)
��r s
{
�� 	
List
�� 
<
�� !
Detalle_transaccion
�� $
>
��$ %
detalles
��& .
=
��/ 0
new
��1 4
List
��5 9
<
��9 :!
Detalle_transaccion
��: M
>
��M N
(
��N O
)
��O P
;
��P Q
List
�� 
<
�� 
int
�� 
>
�� 
idsConceptos
�� "
=
��# $
_conceptoDatos
��% 3
.
��3 43
%ObtenerConceptosComercialesPorFamilia
��4 Y
(
��Y Z
	idFamilia
��Z c
)
��c d
.
��d e
Select
��e k
(
��k l
c
��l m
=>
��n p
c
��q r
.
��r s
id
��s u
)
��u v
.
��v w
ToList
��w }
(
��} ~
)
��~ 
;�� �

Dictionary
�� 
<
�� 
long
�� 
,
�� 
long
�� !
>
��! "+
idsAlmacenIdsInventarioFisico
��# @
=
��A B$
_inventarioActualDatos
��C Y
.
��Y Z2
$ObtenerIdsInventarioActualPorAlmacen
��Z ~
(
��~ 
)�� �
;��� �
List
�� 
<
�� 
int
�� 
>
�� 
idsAlmacenes
�� "
=
��# $
_actorDatos
��% 0
.
��0 16
(ObtenerIdsActorDeNegocioVigentePrincipal
��1 Y
(
��Y Z

��Z g
.
��g h
Default
��h o
.
��o p"
IdRolEntidadInterna��p �
,��� �

.��� �
Default��� �
.��� �
IdRolAlmacen��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
�� 
	idAlmacen
�� "
in
��# %
idsAlmacenes
��& 2
)
��2 3
{
�� 
List
�� 
<
�� !
Detalle_transaccion
�� (
>
��( )"
detallesDeInventario
��* >
=
��? @/
!_consultaDetallesTransaccionDatos
��A b
.
��b c'
ObtenerDetalleTransaccion
��c |
(
��| }
	idAlmacen��} �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �1
!IdTipoTransaccionInventarioActual��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoConfirmado��� �
,��� �
idsConceptos��� �
.��� �
ToArray��� �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
List
�� 
<
�� 
int
�� 
>
�� !
idConceptosAAgregar
�� -
=
��. /"
detallesDeInventario
��0 D
!=
��E G
null
��H L
?
��M N
idsConceptos
��O [
.
��[ \
Except
��\ b
(
��b c"
detallesDeInventario
��c w
.
��w x
Select
��x ~
(
��~ 
e�� �
=>��� �
e��� �
.��� �#
id_concepto_negocio��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �
idsConceptos��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  
idConceptoAAgregar
�� /
in
��0 2!
idConceptosAAgregar
��3 F
)
��F G
{
�� 
detalles
�� 
.
�� 
Add
��  
(
��  !
new
��! $!
Detalle_transaccion
��% 8
(
��8 9+
idsAlmacenIdsInventarioFisico
��9 V
.
��V W
Single
��W ]
(
��] ^
i
��^ _
=>
��` b
i
��c d
.
��d e
Key
��e h
==
��i k
	idAlmacen
��l u
)
��u v
.
��v w
Value
��w |
,
��| }
$num
��~ 
,�� �"
idConceptoAAgregar��� �
,��� �
$str��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
return
�� ,
_crearDetallesTransaccionDatos
�� 1
.
��1 2.
 CrearDetallesDeTransaccionMasivo
��2 R
(
��R S
detalles
��S [
)
��[ \
;
��\ ]
}
�� 	
public
�� 
OperationResult
�� V
HEliminarDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico
�� g
(
��g h
int
��h k
	idFamilia
��l u
)
��u v
{
�� 	
List
�� 
<
�� !
Detalle_transaccion
�� $
>
��$ %
detalles
��& .
=
��/ 0
new
��1 4
List
��5 9
<
��9 :!
Detalle_transaccion
��: M
>
��M N
(
��N O
)
��O P
;
��P Q
List
�� 
<
�� 
int
�� 
>
�� 
idsConceptos
�� "
=
��# $
_conceptoDatos
��% 3
.
��3 43
%ObtenerConceptosComercialesPorFamilia
��4 Y
(
��Y Z
	idFamilia
��Z c
)
��c d
.
��d e
Select
��e k
(
��k l
c
��l m
=>
��n p
c
��q r
.
��r s
id
��s u
)
��u v
.
��v w
ToList
��w }
(
��} ~
)
��~ 
;�� �
List
�� 
<
�� 
int
�� 
>
�� 
idsAlmacenes
�� "
=
��# $
_actorDatos
��% 0
.
��0 16
(ObtenerIdsActorDeNegocioVigentePrincipal
��1 Y
(
��Y Z

��Z g
.
��g h
Default
��h o
.
��o p"
IdRolEntidadInterna��p �
,��� �

.��� �
Default��� �
.��� �
IdRolAlmacen��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
�� 
	idAlmacen
�� "
in
��# %
idsAlmacenes
��& 2
)
��2 3
{
�� 
List
�� 
<
�� !
Detalle_transaccion
�� (
>
��( )"
detallesDeInventario
��* >
=
��? @/
!_consultaDetallesTransaccionDatos
��A b
.
��b c'
ObtenerDetalleTransaccion
��c |
(
��| }
	idAlmacen��} �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �1
!IdTipoTransaccionInventarioActual��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoConfirmado��� �
,��� �
idsConceptos��� �
.��� �
ToArray��� �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
detalles
�� 
.
�� 
AddRange
�� !
(
��! ""
detallesDeInventario
��" 6
)
��6 7
;
��7 8
}
�� 
return
�� /
!_eliminarDetallesTransaccionDatos
�� 4
.
��4 51
#EliminarDetallesDeTransaccionMasivo
��5 X
(
��X Y
detalles
��Y a
)
��a b
;
��b c
}
�� 	
public
�� 

Dictionary
�� 
<
�� 
long
�� 
,
�� 
long
��  $
>
��$ %2
$ObtenerIdsAlmacenIdsInventarioActual
��& J
(
��J K
)
��K L
{
�� 	
try
�� 
{
�� 
return
�� $
_inventarioActualDatos
�� -
.
��- .2
$ObtenerIdsInventarioActualPorAlmacen
��. R
(
��R S
)
��S T
;
��T U
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* j
,
��j k
e
��l m
)
��m n
;
��n o
}
�� 
}
�� 	
}
�� 
}�� ��
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Almacen\InventarioHistorico_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Almacen# *
{ 
public 

class &
InventarioHistorico_Logica +
:+ ,'
IInventarioHistorico_Logica- H
{ 
	protected 
readonly "
IInventarioRepositorio 1"
_almacenReportingDatos2 H
;H I
	protected 
readonly ,
 IInventarioHistorico_Repositorio ;%
_inventarioHistoricoDatos< U
;U V
	protected 
readonly $
IMovimientos_Repositorio 3
_movimientosDatos4 E
;E F
	protected 
readonly !
IConcepto_Repositorio 0
_conceptoDatos1 ?
;? @
	protected 
readonly ,
 IConsultaTransaccion_Repositorio ;%
_consultaTransaccionDatos< U
;U V
	protected 
readonly (
IMaestrosAlmacen_Repositorio 7!
_maestrosAlmacenDatos8 M
;M N
	protected 
readonly )
ICrearTransaccion_Repositorio 8"
_crearTransaccionDatos9 O
;O P
public &
InventarioHistorico_Logica )
() *"
IInventarioRepositorio* @!
almacenReportingDatosA V
,V W,
 IInventarioHistorico_RepositorioX x%
inventarioHistoricoDatos	y �
,
� �&
IMovimientos_Repositorio
� �
movimientosDatos
� �
,
� �#
IConcepto_Repositorio
� �

� �
,
� �.
 IConsultaTransaccion_Repositorio
� �&
consultaTransaccionDatos
� �
,
� �*
IMaestrosAlmacen_Repositorio
� �"
maestrosAlmacenDatos
� �
,
� �+
ICrearTransaccion_Repositorio
� �#
crearTransaccionDatos
� �
)
� �
{ 	"
_almacenReportingDatos   "
=  # $!
almacenReportingDatos  % :
;  : ;%
_inventarioHistoricoDatos!! %
=!!& '$
inventarioHistoricoDatos!!( @
;!!@ A
_movimientosDatos"" 
="" 
movimientosDatos""  0
;""0 1
_conceptoDatos## 
=## 

;##* +%
_consultaTransaccionDatos$$ %
=$$& '$
consultaTransaccionDatos$$( @
;$$@ A!
_maestrosAlmacenDatos%% !
=%%" # 
maestrosAlmacenDatos%%$ 8
;%%8 9"
_crearTransaccionDatos&& "
=&&# $!
crearTransaccionDatos&&% :
;&&: ;
}'' 	
public(( 
OperationResult(( &
CrearInventariosLogicosHoy(( 9
(((9 :
int((: =

idEmpleado((> H
)((H I
{)) 	
DateTime** 
fechaActual**  
=**! "
DateTimeUtil**# /
.**/ 0
FechaActual**0 ;
(**; <
)**< =
;**= >
return++ 
CrearInventarios++ #
(++# $

idEmpleado++$ .
,++. /
fechaActual++0 ;
)++; <
;++< =
}-- 	
public// 
OperationResult// 
CrearInventarios// /
(/// 0
int//0 3

idEmpleado//4 >
,//> ?
DateTime//@ H
fecha//I N
)//N O
{00 	
try11 
{22 
var33 
nuevosInventarios33 %
=33& '
GenerarInventarios33( :
(33: ;

idEmpleado33; E
,33E F
fecha33G L
)33L M
;33M N
if44 
(44 
nuevosInventarios44 %
.44% &
Count44& +
(44+ ,
)44, -
>44. /
$num440 1
)441 2
{55 
return66 "
_crearTransaccionDatos66 1
.661 28
,CrearTransaccionesYDetallesTransaccionMasivo662 ^
(66^ _
nuevosInventarios66_ p
)66p q
;66q r
}77 
throw88 
new88 
LogicaException88 )
(88) *
$str	88* �
)
88� �
;
88� �
}99 
catch:: 
(:: 
	Exception:: 
e:: 
):: 
{;; 
throw<< 
new<< 
LogicaException<< )
(<<) *
$str<<* Q
,<<Q R
e<<S T
)<<T U
;<<U V
}== 
}>> 	
public@@ 
OperationResult@@ 
CrearInventario@@ .
(@@. /
int@@/ 2

idEmpleado@@3 =
,@@= >
int@@? B
	idAlmacen@@C L
,@@L M
DateTime@@N V
fecha@@W \
)@@\ ]
{AA 	
tryBB 
{CC 
varDD 
nuevoInventarioDD #
=DD$ %
GenerarInventarioDD& 7
(DD7 8
	idAlmacenDD8 A
,DDA B

idEmpleadoDDC M
,DDM N
fechaDDO T
)DDT U
;DDU V
varFF 
	resultadoFF 
=FF "
_crearTransaccionDatosFF  6
.FF6 78
,CrearTransaccionesYDetallesTransaccionMasivoFF7 c
(FFc d
newFFd g
ListFFh l
<FFl m
TransaccionFFm x
>FFx y
(FFy z
)FFz {
{FF| }
nuevoInventario	FF~ �
}
FF� �
)
FF� �
;
FF� �
	resultadoGG 
.GG 
informationGG %
=GG& '
nuevoInventarioGG( 7
;GG7 8
returnHH 
	resultadoHH  
;HH  !
}II 
catchJJ 
(JJ 
	ExceptionJJ 
eJJ 
)JJ 
{KK 
throwLL 
newLL 
LogicaExceptionLL )
(LL) *
$strLL* S
+LLT U
$strLLV [
+LL\ ]
eLL^ _
.LL_ `
MessageLL` g
,LLg h
eLLi j
)LLj k
;LLk l
}MM 
}NN 	
publicPP 
ListPP 
<PP 
InventarioFisicoPP $
>PP$ %%
ObtenerInventariosFisicosPP& ?
(PP? @
intPPA D
	idAlmacenPPE N
,PPN O
intPPP S

idEmpleadoPPT ^
,PP^ _
DateTimePP` h
fechaPPi n
)PPn o
{QQ 	
tryRR 
{SS 
varTT 

inventarioTT 
=TT  
GenerarInventarioTT! 2
(TT2 3
	idAlmacenTT3 <
,TT< =

idEmpleadoTT> H
,TTH I
fechaTTJ O
)TTO P
;TTP Q
returnUU %
ConvertToInventarioFisicoUU 0
(UU0 1

inventarioUU1 ;
)UU; <
;UU< =
}VV 
catchWW 
(WW -
!NohayMovimientosDeBienesExceptionWW 4
eWW5 6
)WW6 7
{XX 
ListYY 
<YY 
InventarioFisicoYY %
>YY% &

inventarioYY' 1
=YY2 3
newYY4 7
ListYY8 <
<YY< =
InventarioFisicoYY= M
>YYM N
(YYN O
)YYO P
;YYP Q

inventarioZZ 
=ZZ 
(ZZ 
eZZ 
.ZZ  
HayInventarioPrevioZZ  3
?ZZ4 5"
_almacenReportingDatosZZ6 L
.ZZL M#
ObtenerInventarioFisicoZZM d
(ZZd e
(ZZe f
longZZf j
)ZZj k
eZZk l
.ZZl m
IdUltimoInventarioZZm 
)	ZZ �
.
ZZ� �
ToList
ZZ� �
(
ZZ� �
)
ZZ� �
:
ZZ� �
throw
ZZ� �
new
ZZ� �
LogicaException
ZZ� �
(
ZZ� �
$str
ZZ� �
+
ZZ� �
$str
ZZ� �
+
ZZ� �
e
ZZ� �
.
ZZ� �
Message
ZZ� �
,
ZZ� �
e
ZZ� �
)
ZZ� �
)
ZZ� �
;
ZZ� �
return[[ 

inventario[[ !
;[[! "
}\\ 
catch]] 
(]] 
	Exception]] 
e]] 
)]] 
{^^ 
throw__ 
new__ 
LogicaException__ )
(__) *
$str__* S
+__T U
$str__V [
+__\ ]
e__^ _
.___ `
Message__` g
,__g h
e__i j
)__j k
;__k l
}`` 
}aa 	
publiccc 
Listcc 
<cc 
InventarioSemaforocc &
>cc& '&
ObtenerInventariosSemaforocc( B
(ccB C
intccC F

idEmpleadoccG Q
,ccQ R
intccS V
	idAlmacenccW `
,cc` a
DateTimeccb j
fechacck p
)ccp q
{dd 	
tryee 
{ff 
vargg 

inventariogg 
=gg  
GenerarInventariogg! 2
(gg2 3
	idAlmacengg3 <
,gg< =

idEmpleadogg> H
,ggH I
fechaggJ O
)ggO P
;ggP Q
returnhh '
ConvertToInventarioSemaforohh 2
(hh2 3

inventariohh3 =
)hh= >
;hh> ?
}ii 
catchjj 
(jj -
!NohayMovimientosDeBienesExceptionjj 4
ejj5 6
)jj6 7
{kk 
Listll 
<ll 
InventarioSemaforoll '
>ll' (

inventarioll) 3
=ll4 5
newll6 9
Listll: >
<ll> ?
InventarioSemaforoll? Q
>llQ R
(llR S
)llS T
;llT U

inventariomm 
=mm 
(mm 
emm 
.mm  
HayInventarioPreviomm  3
?mm4 5"
_almacenReportingDatosmm6 L
.mmL M%
ObtenerInventarioSemaforommM f
(mmf g
(mmg h
longmmh l
)mml m
emmm n
.mmn o
IdUltimoInventario	mmo �
)
mm� �
.
mm� �
ToList
mm� �
(
mm� �
)
mm� �
:
mm� �
throw
mm� �
new
mm� �
LogicaException
mm� �
(
mm� �
$str
mm� �
+
mm� �
$str
mm� �
+
mm� �
e
mm� �
.
mm� �
Message
mm� �
,
mm� �
e
mm� �
)
mm� �
)
mm� �
;
mm� �
returnnn 

inventarionn !
;nn! "
}oo 
catchpp 
(pp 
	Exceptionpp 
epp 
)pp 
{qq 
throwrr 
newrr 
LogicaExceptionrr )
(rr) *
$strrr* S
+rrT U
$strrrV [
+rr\ ]
err^ _
.rr_ `
Messagerr` g
,rrg h
erri j
)rrj k
;rrk l
}ss 
}tt 	
publicvv 
Listvv 
<vv  
InventarioValorizadovv (
>vv( ))
ObtenerInventariosValorizadosvv* G
(vvG H
intvvH K

idEmpleadovvL V
,vvV W
intvvX [
	idAlmacenvv\ e
,vve f
DateTimevvg o
fechavvp u
)vvu v
{ww 	
tryxx 
{yy 
varzz 

inventariozz 
=zz  
GenerarInventariozz! 2
(zz2 3
	idAlmacenzz3 <
,zz< =

idEmpleadozz> H
,zzH I
fechazzJ O
)zzO P
;zzP Q
return|| )
ConvertToInventarioValorizado|| 4
(||4 5

inventario||5 ?
)||? @
;||@ A
}}} 
catch~~ 
(~~ -
!NohayMovimientosDeBienesException~~ 4
e~~5 6
)~~6 7
{ 
List
�� 
<
�� "
InventarioValorizado
�� )
>
��) *

inventario
��+ 5
=
��6 7
new
��8 ;
List
��< @
<
��@ A"
InventarioValorizado
��A U
>
��U V
(
��V W
)
��W X
;
��X Y

inventario
�� 
=
�� 
(
�� 
e
�� 
.
��  !
HayInventarioPrevio
��  3
?
��4 5$
_almacenReportingDatos
��6 L
.
��L M)
ObtenerInventarioValorizado
��M h
(
��h i
(
��i j
long
��j n
)
��n o
e
��o p
.
��p q!
IdUltimoInventario��q �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �
throw��� �
new��� �
LogicaException��� �
(��� �
$str��� �
+��� �
$str��� �
+��� �
e��� �
.��� �
Message��� �
,��� �
e��� �
)��� �
)��� �
;��� �
return
�� 

inventario
�� !
;
��! "
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* S
+
��T U
$str
��V [
+
��\ ]
e
��^ _
.
��_ `
Message
��` g
,
��g h
e
��i j
)
��j k
;
��k l
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
InventarioFisico
�� $
>
��$ %'
ObtenerInventariosFisicos
��& ?
(
��? @
int
��@ C
	idAlmacen
��D M
,
��M N
int
��O R

idEmpleado
��S ]
,
��] ^
DateTime
��` h
fecha
��i n
,
��n o
int
��p s
[
��s t
]
��t u
familias
��v ~
)
��~ 
{
�� 	
try
�� 
{
�� 
var
�� 

inventario
�� 
=
��  
GenerarInventario
��! 2
(
��2 3
	idAlmacen
��3 <
,
��< =

idEmpleado
��> H
,
��H I
fecha
��J O
,
��O P
familias
��Q Y
)
��Y Z
;
��Z [
return
�� '
ConvertToInventarioFisico
�� 0
(
��0 1

inventario
��1 ;
)
��; <
;
��< =
}
�� 
catch
�� 
(
�� /
!NohayMovimientosDeBienesException
�� 4
e
��5 6
)
��6 7
{
�� 
List
�� 
<
�� 
InventarioFisico
�� %
>
��% &

inventario
��' 1
=
��2 3
new
��4 7
List
��8 <
<
��< =
InventarioFisico
��= M
>
��M N
(
��N O
)
��O P
;
��P Q

inventario
�� 
=
�� 
(
�� 
e
�� 
.
��  !
HayInventarioPrevio
��  3
?
��4 5$
_almacenReportingDatos
��6 L
.
��L M%
ObtenerInventarioFisico
��M d
(
��d e
(
��e f
long
��f j
)
��j k
e
��k l
.
��l m 
IdUltimoInventario
��m 
,�� �
familias��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �
throw��� �
new��� �
LogicaException��� �
(��� �
$str��� �
+��� �
$str��� �
+��� �
e��� �
.��� �
Message��� �
,��� �
e��� �
)��� �
)��� �
;��� �
return
�� 

inventario
�� !
;
��! "
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* S
+
��T U
$str
��V [
+
��\ ]
e
��^ _
.
��_ `
Message
��` g
,
��g h
e
��i j
)
��j k
;
��k l
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� "
InventarioValorizado
�� (
>
��( )+
ObtenerInventariosValorizados
��* G
(
��G H
int
��H K

idEmpleado
��L V
,
��V W
int
��X [
	idAlmacen
��\ e
,
��e f
DateTime
��g o
fecha
��p u
,
��u v
int
��w z
[
��z {
]
��{ |
familias��} �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 

inventario
�� 
=
��  
GenerarInventario
��! 2
(
��2 3
	idAlmacen
��3 <
,
��< =

idEmpleado
��> H
,
��H I
fecha
��J O
,
��O P
familias
��Q Y
)
��Y Z
;
��Z [
return
�� +
ConvertToInventarioValorizado
�� 4
(
��4 5

inventario
��5 ?
)
��? @
;
��@ A
}
�� 
catch
�� 
(
�� /
!NohayMovimientosDeBienesException
�� 4
e
��5 6
)
��6 7
{
�� 
List
�� 
<
�� "
InventarioValorizado
�� )
>
��) *

inventario
��+ 5
=
��6 7
new
��8 ;
List
��< @
<
��@ A"
InventarioValorizado
��A U
>
��U V
(
��V W
)
��W X
;
��X Y

inventario
�� 
=
�� 
(
�� 
e
�� 
.
��  !
HayInventarioPrevio
��  3
?
��4 5$
_almacenReportingDatos
��6 L
.
��L M)
ObtenerInventarioValorizado
��M h
(
��h i
(
��i j
long
��j n
)
��n o
e
��o p
.
��p q!
IdUltimoInventario��q �
,��� �
familias��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �
throw��� �
new��� �
LogicaException��� �
(��� �
$str��� �
+��� �
$str��� �
+��� �
e��� �
.��� �
Message��� �
,��� �
e��� �
)��� �
)��� �
;��� �
return
�� 

inventario
�� !
;
��! "
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* S
+
��T U
$str
��V [
+
��\ ]
e
��^ _
.
��_ `
Message
��` g
,
��g h
e
��i j
)
��j k
;
��k l
}
�� 
}
�� 	
public
�� 
List
�� 
<
��  
InventarioSemaforo
�� &
>
��& '(
ObtenerInventariosSemaforo
��( B
(
��B C
int
��C F

idEmpleado
��G Q
,
��Q R
int
��S V
	idAlmacen
��W `
,
��` a
DateTime
��b j
fecha
��k p
,
��p q
int
��r u
[
��u v
]
��v w
familias��x �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 

inventario
�� 
=
��  
GenerarInventario
��! 2
(
��2 3
	idAlmacen
��3 <
,
��< =

idEmpleado
��> H
,
��H I
fecha
��J O
,
��O P
familias
��Q Y
)
��Y Z
;
��Z [
return
�� )
ConvertToInventarioSemaforo
�� 2
(
��2 3

inventario
��3 =
)
��= >
;
��> ?
}
�� 
catch
�� 
(
�� /
!NohayMovimientosDeBienesException
�� 4
e
��5 6
)
��6 7
{
�� 
List
�� 
<
��  
InventarioSemaforo
�� '
>
��' (

inventario
��) 3
=
��4 5
new
��6 9
List
��: >
<
��> ? 
InventarioSemaforo
��? Q
>
��Q R
(
��R S
)
��S T
;
��T U

inventario
�� 
=
�� 
(
�� 
e
�� 
.
��  !
HayInventarioPrevio
��  3
?
��4 5$
_almacenReportingDatos
��6 L
.
��L M'
ObtenerInventarioSemaforo
��M f
(
��f g
(
��g h
long
��h l
)
��l m
e
��m n
.
��n o!
IdUltimoInventario��o �
,��� �
familias��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �
throw��� �
new��� �
LogicaException��� �
(��� �
$str��� �
+��� �
$str��� �
+��� �
e��� �
.��� �
Message��� �
,��� �
e��� �
)��� �
)��� �
;��� �
return
�� 

inventario
�� !
;
��! "
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* S
+
��S T
$str
��U Z
+
��Z [
e
��\ ]
.
��] ^
Message
��^ e
,
��e f
e
��g h
)
��h i
;
��i j
}
�� 
}
�� 	
private
�� 
List
�� 
<
�� 
InventarioFisico
�� %
>
��% &'
ConvertToInventarioFisico
��' @
(
��@ A
Transaccion
��A L#
inventarioTransaccion
��M b
)
��b c
{
�� 	
List
�� 
<
�� 
InventarioFisico
�� !
>
��! "
inventarios
��# .
=
��/ 0
new
��1 4
List
��5 9
<
��9 :
InventarioFisico
��: J
>
��J K
(
��K L
)
��L M
;
��M N
List
�� 
<
�� 
Concepto
�� 
>
�� 
	conceptos
�� $
=
��% &
_conceptoDatos
��' 5
.
��5 6
ObtenerConceptos
��6 F
(
��F G#
inventarioTransaccion
��G \
.
��\ ]!
Detalle_transaccion
��] p
.
��p q
Select
��q w
(
��w x
dt
��x z
=>
��{ }
dt��~ �
.��� �#
id_concepto_negocio��� �
)��� �
.��� �
Distinct��� �
(��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
inventarios
�� 
=
�� #
inventarioTransaccion
�� /
.
��/ 0!
Detalle_transaccion
��0 C
.
��C D
Select
��D J
(
��J K
dt
��K M
=>
��N P
new
��Q T
InventarioFisico
��U e
(
��e f
)
��f g
{
�� 
CodigoBarra
�� 
=
�� 
	conceptos
�� '
.
��' (
Single
��( .
(
��. /
c
��/ 0
=>
��1 3
c
��4 5
.
��5 6
Id
��6 8
==
��9 ;
dt
��< >
.
��> ?!
id_concepto_negocio
��? R
)
��R S
.
��S T
CodigoBarra
��T _
,
��_ `
Concepto
�� 
=
�� 
	conceptos
�� $
.
��$ %
Single
��% +
(
��+ ,
c
��, -
=>
��. 0
c
��1 2
.
��2 3
Id
��3 5
==
��6 8
dt
��9 ;
.
��; <!
id_concepto_negocio
��< O
)
��O P
.
��P Q
Nombre
��Q W
,
��W X
Cantidad
�� 
=
�� 
dt
�� 
.
�� 
cantidad
�� &
,
��& '
Familia
�� 
=
�� 
	conceptos
�� #
.
��# $
Single
��$ *
(
��* +
c
��+ ,
=>
��- /
c
��0 1
.
��1 2
Id
��2 4
==
��5 7
dt
��8 :
.
��: ;!
id_concepto_negocio
��; N
)
��N O
.
��O P
Familia
��P W
,
��W X
Lote
�� 
=
�� 
dt
�� 
.
�� 
lote
�� 
,
�� 
UnidadMedida
�� 
=
�� 
	conceptos
�� (
.
��( )
Single
��) /
(
��/ 0
c
��0 1
=>
��2 4
c
��5 6
.
��6 7
Id
��7 9
==
��: <
dt
��= ?
.
��? @!
id_concepto_negocio
��@ S
)
��S T
.
��T U
UnidadMedida
��U a
,
��a b
}
�� 
)
�� 
.
��
ToList
�� 
(
�� 
)
�� 
;
�� 
return
�� 
inventarios
�� 
;
�� 
}
�� 	
private
�� 
List
�� 
<
�� "
InventarioValorizado
�� )
>
��) *+
ConvertToInventarioValorizado
��+ H
(
��H I
Transaccion
��I T#
inventarioTransaccion
��U j
)
��j k
{
�� 	
List
�� 
<
�� "
InventarioValorizado
�� %
>
��% &
inventarios
��' 2
=
��3 4
new
��5 8
List
��9 =
<
��= >"
InventarioValorizado
��> R
>
��R S
(
��S T
)
��T U
;
��U V
List
�� 
<
�� 
Concepto
�� 
>
�� 
	conceptos
�� $
=
��% &
_conceptoDatos
��' 5
.
��5 6
ObtenerConceptos
��6 F
(
��F G#
inventarioTransaccion
��G \
.
��\ ]!
Detalle_transaccion
��] p
.
��p q
Select
��q w
(
��w x
dt
��x z
=>
��z |
dt
��} 
.�� �#
id_concepto_negocio��� �
)��� �
.��� �
Distinct��� �
(��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
inventarios
�� 
=
�� #
inventarioTransaccion
�� /
.
��/ 0!
Detalle_transaccion
��0 C
.
��C D
Select
��D J
(
��J K
dt
��K M
=>
��N P
new
��Q T"
InventarioValorizado
��U i
(
��i j
)
��j k
{
�� 
CodigoBarra
�� 
=
�� 
	conceptos
�� '
.
��' (
Single
��( .
(
��. /
c
��/ 0
=>
��1 3
c
��4 5
.
��5 6
Id
��6 8
==
��9 ;
dt
��< >
.
��> ?!
id_concepto_negocio
��? R
)
��R S
.
��S T
CodigoBarra
��T _
,
��_ `
Concepto
�� 
=
�� 
	conceptos
�� $
.
��$ %
Single
��% +
(
��+ ,
c
��, -
=>
��- /
c
��/ 0
.
��0 1
Id
��1 3
==
��3 5
dt
��6 8
.
��8 9!
id_concepto_negocio
��9 L
)
��L M
.
��M N
Nombre
��N T
,
��T U
Cantidad
�� 
=
�� 
dt
�� 
.
�� 
cantidad
�� &
,
��& '
Familia
�� 
=
�� 
	conceptos
�� #
.
��# $
Single
��$ *
(
��* +
c
��+ ,
=>
��- /
c
��0 1
.
��1 2
Id
��2 4
==
��5 7
dt
��8 :
.
��: ;!
id_concepto_negocio
��; N
)
��N O
.
��O P
Familia
��P W
,
��W X
Lote
�� 
=
�� 
dt
�� 
.
�� 
lote
�� 
,
�� 
UnidadMedida
�� 
=
�� 
	conceptos
�� (
.
��( )
Single
��) /
(
��/ 0
c
��0 1
=>
��2 4
c
��5 6
.
��6 7
Id
��7 9
==
��: <
dt
��= ?
.
��? @!
id_concepto_negocio
��@ S
)
��S T
.
��T U
UnidadMedida
��U a
,
��a b

ValorTotal
�� 
=
�� 
dt
�� 
.
��  
total
��  %
,
��% &

�� 
=
�� 
dt
��  "
.
��" #
precio_unitario
��# 2
}
�� 
)
�� 
.
��
ToList
�� 
(
�� 
)
�� 
;
�� 
return
�� 
inventarios
�� 
;
�� 
}
�� 	
private
�� 
List
�� 
<
��  
InventarioSemaforo
�� '
>
��' ()
ConvertToInventarioSemaforo
��) D
(
��D E
Transaccion
��E P#
inventarioTransaccion
��Q f
)
��f g
{
�� 	
List
�� 
<
��  
InventarioSemaforo
�� #
>
��# $
inventarios
��% 0
=
��1 2
new
��3 6
List
��7 ;
<
��; < 
InventarioSemaforo
��< N
>
��N O
(
��O P
)
��P Q
;
��Q R
List
�� 
<
�� 
Concepto
�� 
>
�� 
	conceptos
�� $
=
��% &
_conceptoDatos
��' 5
.
��5 6
ObtenerConceptos
��6 F
(
��F G#
inventarioTransaccion
��G \
.
��\ ]!
Detalle_transaccion
��] p
.
��p q
Select
��q w
(
��w x
dt
��x z
=>
��{ }
dt��~ �
.��� �#
id_concepto_negocio��� �
)��� �
.��� �
Distinct��� �
(��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
inventarios
�� 
=
�� #
inventarioTransaccion
�� /
.
��/ 0!
Detalle_transaccion
��0 C
.
��C D
Select
��D J
(
��J K
dt
��K M
=>
��N P
new
��Q T 
InventarioSemaforo
��U g
(
��g h
)
��h i
{
�� 
CodigoBarra
�� 
=
�� 
	conceptos
�� '
.
��' (
Single
��( .
(
��. /
c
��/ 0
=>
��1 3
c
��4 5
.
��5 6
Id
��6 8
==
��9 ;
dt
��< >
.
��> ?!
id_concepto_negocio
��? R
)
��R S
.
��S T
CodigoBarra
��T _
,
��_ `
Concepto
�� 
=
�� 
	conceptos
�� $
.
��$ %
Single
��% +
(
��+ ,
c
��, -
=>
��. 0
c
��1 2
.
��2 3
Id
��3 5
==
��6 8
dt
��9 ;
.
��; <!
id_concepto_negocio
��< O
)
��O P
.
��P Q
Nombre
��Q W
,
��W X
Cantidad
�� 
=
�� 
dt
�� 
.
�� 
cantidad
�� &
,
��& '
Familia
�� 
=
�� 
	conceptos
�� #
.
��# $
Single
��$ *
(
��* +
c
��+ ,
=>
��- /
c
��0 1
.
��1 2
Id
��2 4
==
��5 7
dt
��8 :
.
��: ;!
id_concepto_negocio
��; N
)
��N O
.
��O P
Familia
��P W
,
��W X
Lote
�� 
=
�� 
dt
�� 
.
�� 
lote
�� 
,
�� 
UnidadMedida
�� 
=
�� 
	conceptos
�� (
.
��( )
Single
��) /
(
��/ 0
c
��0 1
=>
��2 4
c
��5 6
.
��6 7
Id
��7 9
==
��: <
dt
��= ?
.
��? @!
id_concepto_negocio
��@ S
)
��S T
.
��T U
UnidadMedida
��U a
,
��a b
StockMinimo
�� 
=
�� 
	conceptos
�� '
.
��' (
Single
��( .
(
��. /
c
��/ 0
=>
��1 3
c
��4 5
.
��5 6
Id
��6 8
==
��9 ;
dt
��< >
.
��> ?!
id_concepto_negocio
��? R
)
��R S
.
��S T
StockMinimo
��T _
}
�� 
)
�� 
.
��
ToList
�� 
(
�� 
)
�� 
;
�� 
return
�� 
inventarios
�� 
;
�� 
}
�� 	
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
GenerarInventarios
��! 3
(
��3 4
int
��4 7

idEmpleado
��8 B
,
��B C
DateTime
��D L
fecha
��M R
)
��R S
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
Transaccion
��  
>
��  !
nuevosInventarios
��" 3
=
��4 5
new
��6 9
List
��: >
<
��> ?
Transaccion
��? J
>
��J K
(
��K L
)
��L M
;
��M N
int
�� 
[
�� 
]
�� 
idsAlmacenes
�� "
=
��# $#
_maestrosAlmacenDatos
��% :
.
��: ;!
ObtenerIdsAlmacenes
��; N
(
��N O
)
��O P
;
��P Q
foreach
�� 
(
�� 
var
�� 
	idAlmacen
�� &
in
��' )
idsAlmacenes
��* 6
)
��6 7
{
�� 
var
�� 
nuevoInventario
�� '
=
��( )
GenerarInventario
��* ;
(
��; <
	idAlmacen
��< E
,
��E F

idEmpleado
��G Q
,
��Q R
fecha
��S X
)
��X Y
;
��Y Z
nuevosInventarios
�� %
.
��% &
Add
��& )
(
��) *
nuevoInventario
��* 9
)
��9 :
;
��: ;
}
�� 
return
�� 
nuevosInventarios
�� (
;
��( )
}
�� 
catch
�� 
(
�� /
!NohayMovimientosDeBienesException
�� 4
e
��5 6
)
��6 7
{
�� 
throw
�� 
(
�� 
e
�� 
)
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Q
,
��Q R
e
��S T
)
��T U
;
��U V
}
�� 
}
�� 	
public
�� 
Transaccion
�� 
GenerarInventario
�� ,
(
��, -
int
��- 0
	idAlmacen
��1 :
,
��: ;
int
��< ?

idEmpleado
��@ J
,
��J K
DateTime
��L T
fecha
��U Z
)
��Z [
{
�� 	
return
�� 
GenerarInventario
�� $
(
��$ %
	idAlmacen
��% .
,
��. /

idEmpleado
��0 :
,
��: ;
fecha
��< A
,
��A B
null
��C G
)
��G H
;
��H I
}
�� 	
private
�� 
Transaccion
�� 
GenerarInventario
�� -
(
��- .
int
��. 1
	idAlmacen
��2 ;
,
��; <
int
��= @

idEmpleado
��A K
,
��K L
DateTime
��M U
fecha
��V [
,
��[ \
int
��] `
[
��` a
]
��a b
familias
��c k
)
��k l
{
�� 	
try
�� 
{
�� 
Transaccion
�� 
nuevoInventario
�� +
=
��, -
null
��. 2
;
��2 3
DateTime
�� 
?
�� %
fechaPrimeraTransaccion
�� 1
=
��2 3
fecha
��4 9
;
��9 :
var
�� 
ultimoInventario
�� $
=
��% &
(
��' (
familias
��( 0
!=
��1 3
null
��3 7
?
��7 8'
_inventarioHistoricoDatos
��9 R
.
��R SB
3ObtenerUltimoInventarioValorizadoHistoricoAnteriorA��S �
(��� �
	idAlmacen��� �
,��� �
fecha��� �
,��� �
familias��� �
)��� �
:��� �)
_inventarioHistoricoDatos��� �
.��� �C
3ObtenerUltimoInventarioValorizadoHistoricoAnteriorA��� �
(��� �
	idAlmacen��� �
,��� �
fecha��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
if
�� 
(
�� 
ultimoInventario
�� $
==
��% '
null
��( ,
||
��- /
ultimoInventario
��0 @
.
��@ A
Count
��A F
==
��G I
$num
��J K
)
��K L
{
�� %
fechaPrimeraTransaccion
�� +
=
��, -'
_consultaTransaccionDatos
��. G
.
��G H,
ObtenerFechaPrimeraTransaccion
��H f
(
��f g
	idAlmacen
��g p
)
��p q
;
��q r
}
�� 
var
�� 

fechaDesde
�� 
=
��  
ultimoInventario
��! 1
!=
��2 4
null
��5 9
&&
��: <
ultimoInventario
��= M
.
��M N
Count
��N S
>
��S T
$num
��T U
?
��U V
ultimoInventario
��W g
.
��g h
First
��h m
(
��m n
)
��n o
.
��o p
Fecha
��p u
.
��u v
AddMilliseconds��v �
(��� �
$num��� �
)��� �
:��� �'
fechaPrimeraTransaccion��� �
!=��� �
null��� �
?��� �
(��� �
DateTime��� �
)��� �'
fechaPrimeraTransaccion��� �
:��� �
fecha��� �
;��� �
var
�� 

fechaHasta
�� 
=
��  
fecha
��! &
;
��& '
var
�� 
movimientos
�� 
=
��  !
(
��" # 
AplicacionSettings
��# 5
.
��5 6
Default
��6 =
.
��= >$
PermitirGestionDeLotes
��> T
?
��U V
(
��W X
familias
��X `
!=
��a c
null
��d h
?
��i j
_movimientosDatos
��k |
.
��| }7
(ObtenerMovimientosDeConceptoNegocioYLote��} �
(��� �
	idAlmacen��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
,��� �
familias��� �
)��� �
:��� �!
_movimientosDatos��� �
.��� �8
(ObtenerMovimientosDeConceptoNegocioYLote��� �
(��� �
	idAlmacen��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
)��� �
:��� �
(��� �
familias��� �
!=��� �
null��� �
?��� �!
_movimientosDatos��� �
.��� �3
#ObtenerMovimientosDeConceptoNegocio��� �
(��� �
	idAlmacen��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
,��� �
familias��� �
)��� �
:��� �!
_movimientosDatos��� �
.��� �3
#ObtenerMovimientosDeConceptoNegocio��� �
(��� �
	idAlmacen��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
List
�� 
<
�� 
ConceptoLote
�� !
>
��! "
conceptosLotes
��# 1
=
��2 3
new
��4 7
List
��8 <
<
��< =
ConceptoLote
��= I
>
��I J
(
��J K
)
��K L
;
��L M
if
�� 
(
�� 
ultimoInventario
�� $
!=
��% '
null
��( ,
&&
��- /
ultimoInventario
��0 @
.
��@ A
Count
��A F
>
��G H
$num
��I J
)
��J K
{
�� 
var
�� 
grupos_inventario
�� )
=
��* +
ultimoInventario
��, <
.
��< =
GroupBy
��= D
(
��D E
i
��E F
=>
��G I
new
��J M
{
��N O
i
��P Q
.
��Q R

IdConcepto
��R \
,
��\ ]
i
��^ _
.
��_ `
Lote
��` d
}
��e f
)
��f g
.
��g h
Select
��h n
(
��n o
g
��o p
=>
��q s
g
��t u
.
��u v
First
��v {
(
��{ |
)
��| }
)
��} ~
.
��~ 
ToList�� �
(��� �
)��� �
;��� �
grupos_inventario
�� %
.
��% &
ForEach
��& -
(
��- .
g
��. /
=>
��0 2
conceptosLotes
��3 A
.
��A B
Add
��B E
(
��E F
new
��F I
ConceptoLote
��J V
(
��V W
)
��W X
{
��Y Z

IdConcepto
��[ e
=
��f g
g
��h i
.
��i j

IdConcepto
��j t
,
��t u
Lote
��v z
=
��{ |
g
��} ~
.
��~ 
Lote�� �
}��� �
)��� �
)��� �
;��� �
}
�� 
var
�� 
mensaje
�� 
=
�� 
$str
�� f
;
��f g
if
�� 
(
�� 
movimientos
�� 
==
��  "
null
��# '
||
��( *
movimientos
��+ 6
.
��6 7
Count
��7 <
<=
��= ?
$num
��@ A
)
��A B
{
�� 
throw
�� 
ultimoInventario
�� *
==
��+ -
null
��. 2
||
��3 5
ultimoInventario
��6 F
.
��F G
Count
��G L
==
��L N
$num
��N O
?
��P Q
new
��R U/
!NohayMovimientosDeBienesException
��V w
(
��w x
mensaje
��x 
,�� �
false��� �
,��� �
$num��� �
,��� �
null��� �
)��� �
:��� �
throw��� �
new��� �1
!NohayMovimientosDeBienesException��� �
(��� �
mensaje��� �
,��� �
true��� �
,��� � 
ultimoInventario��� �
.��� �
First��� �
(��� �
)��� �
.��� �

,��� � 
ultimoInventario��� �
.��� �
First��� �
(��� �
)��� �
.��� �
Fecha��� �
)��� �
;��� �
}
�� 
var
��  
grupos_movimientos
�� &
=
��' (
movimientos
��) 4
.
��4 5
GroupBy
��5 <
(
��< =
m
��= >
=>
��? A
new
��B E
{
��F G
m
��H I
.
��I J!
Id_concepto_negocio
��J ]
,
��] ^
m
��_ `
.
��` a
Lote
��a e
}
��f g
)
��g h
.
��h i
Select
��i o
(
��o p
g
��p q
=>
��r t
g
��u v
.
��v w
First
��w |
(
��| }
)
��} ~
)
��~ 
.�� �
ToList��� �
(��� �
)��� �
;��� � 
grupos_movimientos
�� "
.
��" #
ForEach
��# *
(
��* +
g
��+ ,
=>
��- /
conceptosLotes
��0 >
.
��> ?
Add
��? B
(
��B C
new
��C F
ConceptoLote
��G S
(
��S T
)
��T U
{
��V W

IdConcepto
��X b
=
��c d
g
��e f
.
��f g!
Id_concepto_negocio
��g z
,
��z {
Lote��| �
=��� �
g��� �
.��� �
Lote��� �
}��� �
)��� �
)��� �
;��� �
var
�� $
conceptosLotesDistinct
�� *
=
��+ ,
conceptosLotes
��- ;
.
��; <
GroupBy
��< C
(
��C D
cl
��D F
=>
��G I
new
��J M
{
��N O
cl
��P R
.
��R S

IdConcepto
��S ]
,
��] ^
cl
��_ a
.
��a b
Lote
��b f
}
��g h
)
��h i
.
��i j
Select
��j p
(
��p q
g
��q r
=>
��s u
g
��v w
.
��w x
First
��x }
(
��} ~
)
��~ 
)�� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
nuevoInventario
�� 
=
��  !
new
��" %
Transaccion
��& 1
(
��1 2
$str
��2 4
,
��4 5
null
��6 :
,
��: ;
DateTimeUtil
��< H
.
��H I
FechaActual
��I T
(
��T U
)
��U V
,
��V W!
TransaccionSettings
��X k
.
��k l
Default
��l s
.
��s t3
$IdTipoTransaccionInventarioHistorico��t �
,��� �
MaestroSettings
�� #
.
��# $
Default
��$ +
.
��+ ,8
*IdDetalleMaestroUnidadDeNegocioTransversal
��, V
,
��V W
false
��X ]
,
��] ^

fechaHasta
��_ i
,
��i j

fechaHasta
��k u
,
��u v"
TransaccionSettings��w �
.��� �
Default��� �
.��� �%
IdComprobanteGenerico��� �
,��� �
$str
�� Z
,
��Z [

fechaHasta
��\ f
,
��f g

idEmpleado
��h r
,
��r s
$num
��t v
,
��v w
	idAlmacen��x �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �+
IdDetalleMaestroMonedaSoles��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
	idAlmacen��� �
)��� �
;��� � 
Estado_transaccion
�� "
estadoTransaccion
��# 4
=
��5 6
new
��7 : 
Estado_transaccion
��; M
(
��M N

idEmpleado
��N X
,
��X Y
MaestroSettings
��Z i
.
��i j
Default
��j q
.
��q r/
 IdDetalleMaestroEstadoConfirmado��r �
,��� �

fechaHasta��� �
,��� �
$str��� �
)��� �
;��� �
nuevoInventario
�� 
.
��   
Estado_transaccion
��  2
.
��2 3
Add
��3 6
(
��6 7
estadoTransaccion
��7 H
)
��H I
;
��I J
foreach
�� 
(
�� 
var
�� 
item
�� !
in
��" $$
conceptosLotesDistinct
��% ;
)
��; <
{
�� 
nuevoInventario
�� #
.
��# $!
Detalle_transaccion
��$ 7
.
��7 8
Add
��8 ;
(
��; <&
GenerarDetalleInventario
��< T
(
��T U
item
��U Y
,
��Y Z
ultimoInventario
��Z j
,
��j k
movimientos
��l w
,
��w x
item
��y }
.
��} ~

IdConcepto��~ �
,��� �
item��� �
.��� �
Lote��� �
)��� �
)��� �
;��� �
}
�� 
return
�� 
nuevoInventario
�� &
;
��& '
}
�� 
catch
�� 
(
�� /
!NohayMovimientosDeBienesException
�� 4
e
��5 6
)
��6 7
{
�� 
throw
�� 
(
�� 
e
�� 
)
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Q
,
��Q R
e
��S T
)
��T U
;
��U V
}
�� 
}
�� 	
public
�� !
Detalle_transaccion
�� "&
GenerarDetalleInventario
��# ;
(
��; <
ConceptoLote
��< H
item
��I M
,
��M N
List
��N R
<
��R S"
InventarioValorizado
��S g
>
��g h
ultimoInventario
��i y
,
��y z
List
��{ 
<�� �B
2Movimientos_concepto_negocio_actor_negocio_interno��� �
>��� �
movimientos��� �
,��� �
int��� �

idConcepto��� �
,��� �
string��� �
lote��� �
)��� �
{
�� 	
var
�� $
inventarioConceptoLote
�� &
=
��' (
ultimoInventario
��) 9
!=
��: <
null
��= A
?
��B C
ultimoInventario
��D T
.
��T U
SingleOrDefault
��U d
(
��d e
dt
��e g
=>
��h j
dt
��k m
.
��m n

IdConcepto
��n x
==
��y {

idConcepto��| �
&&��� �
dt��� �
.��� �
Lote��� �
==��� �
lote��� �
)��� �
:��� �
null��� �
;��� �
var
�� %
movimientosConceptoLote
�� '
=
��( )
movimientos
��* 5
.
��5 6
Where
��6 ;
(
��; <
m
��< =
=>
��> @
m
��A B
.
��B C!
Id_concepto_negocio
��C V
==
��W Y

idConcepto
��Z d
&&
��e g
m
��h i
.
��i j
Lote
��j n
==
��o q
lote
��r v
)
��v w
;
��w x
var
��  
cantidad_principal
�� "
=
��# $
(
��% &
ultimoInventario
��& 6
!=
��7 9
null
��: >
&&
��? A$
inventarioConceptoLote
��B X
!=
��Y [
null
��\ `
?
��a b$
inventarioConceptoLote
�� &
.
��& '
Cantidad
��' /
:
��0 1
$num
��2 3
)
��3 4
+
��5 6
(
�� %
movimientosConceptoLote
�� (
!=
��) +
null
��, 0
?
��1 2%
movimientosConceptoLote
��3 J
.
��J K
Sum
��K N
(
��N O
m
��O P
=>
��Q S
m
��T U
.
��U V 
Entradas_principal
��V h
)
��h i
:
��j k
$num
��l m
)
��m n
-
��o p
(
�� %
movimientosConceptoLote
�� (
!=
��) +
null
��, 0
?
��1 2%
movimientosConceptoLote
��3 J
.
��J K
Sum
��K N
(
��N O
m
��O P
=>
��Q S
m
��T U
.
��U V
Salidas_principal
��V g
)
��g h
:
��i j
$num
��k l
)
��l m
;
��m n
;
��o p
var
�� !
cantidad_secundaria
�� #
=
��$ %
(
��& '
decimal
��' .
)
��. /
(
��/ 0
(
�� 
ultimoInventario
�� !
!=
��" $
null
��% )
&&
��* ,$
inventarioConceptoLote
��- C
!=
��D F
null
��G K
?
��L M$
inventarioConceptoLote
�� &
.
��& ' 
CantidadSecundaria
��' 9
:
��: ;
$num
��< =
)
��= >
+
��? @
(
�� %
movimientosConceptoLote
�� (
!=
��) +
null
��, 0
?
��1 2%
movimientosConceptoLote
��3 J
.
��J K
Sum
��K N
(
��N O
m
��O P
=>
��Q S
m
��T U
.
��U V!
Entradas_secundaria
��V i
)
��i j
:
��k l
$num
��m n
)
��n o
-
��p q
(
�� %
movimientosConceptoLote
�� )
!=
��* ,
null
��- 1
?
��2 3%
movimientosConceptoLote
��4 K
.
��K L
Sum
��L O
(
��O P
m
��P Q
=>
��R T
m
��U V
.
��V W 
Salidas_secundaria
��W i
)
��i j
:
��k l
$num
��m n
)
��n o
)
��o p
;
��p q
var
�� 

costoTotal
�� 
=
�� 
(
�� 
ultimoInventario
�� .
!=
��/ 1
null
��2 6
&&
��7 9$
inventarioConceptoLote
��: P
!=
��Q S
null
��T X
?
��Y Z$
inventarioConceptoLote
�� &
.
��& '

ValorTotal
��' 1
:
��2 3
$num
��4 5
)
��5 6
+
��7 8
(
�� %
movimientosConceptoLote
�� (
!=
��) +
null
��, 0
?
��1 2%
movimientosConceptoLote
��3 J
.
��J K
Sum
��K N
(
��N O
m
��O P
=>
��Q S
m
��T U
.
��U V
Total
��V [
)
��[ \
:
��] ^
$num
��_ `
)
��` a
;
��a b
var
�� 

�� 
=
��  
cantidad_principal
��  2
!=
��3 5
$num
��6 7
?
��8 9

costoTotal
��: D
/
��E F 
cantidad_principal
��G Y
:
��Z [
$num
��\ ]
;
��] ^
return
�� 
(
�� 
new
�� !
Detalle_transaccion
�� +
(
��+ , 
cantidad_principal
��, >
,
��> ?
item
��@ D
.
��D E

IdConcepto
��E O
,
��O P
$str
��Q ]
,
��] ^

��_ l
,
��l m

costoTotal
��n x
,
��x y
null
��z ~
,
��~ #
cantidad_secundaria��� �
,��� �
null��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
)��� �
{��� �
lote��� �
=��� �
item��� �
.��� �
Lote��� �
}��� �
)��� �
;��� �
}
�� 	
public
�� 
OperationResult
�� >
0CrearInventarioHistoricoClonandoInventarioFisico
�� O
(
��O P
int
��P S

idEmpleado
��T ^
)
��^ _
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
DateTime
�� 
?
�� %
fechaPrimeraTransaccion
�� 1
=
��2 3
fechaActual
��4 ?
;
��? @
List
�� 
<
�� 
Transaccion
��  
>
��  !
nuevosInventarios
��" 3
=
��4 5
new
��6 9
List
��: >
<
��> ?
Transaccion
��? J
>
��J K
(
��K L
)
��L M
;
��M N
int
�� 
[
�� 
]
�� 
idsAlmacenes
�� "
=
��# $#
_maestrosAlmacenDatos
��% :
.
��: ;!
ObtenerIdsAlmacenes
��; N
(
��N O
)
��O P
;
��P Q
foreach
�� 
(
�� 
var
�� 
	idAlmacen
�� &
in
��' )
idsAlmacenes
��* 6
)
��6 7
{
�� 
var
�� 
ultimoInventario
�� (
=
��) *'
_inventarioHistoricoDatos
��+ D
.
��D E.
 ObtenerUltimoInventarioHistorico
��E e
(
��e f
	idAlmacen
��f o
)
��o p
;
��p q
if
�� 
(
�� 
ultimoInventario
�� (
==
��) +
null
��, 0
)
��0 1
{
�� %
fechaPrimeraTransaccion
�� /
=
��0 1'
_consultaTransaccionDatos
��2 K
.
��K L,
ObtenerFechaPrimeraTransaccion
��L j
(
��j k
	idAlmacen
��k t
)
��t u
;
��u v
}
�� 
var
�� 

fechaDesde
�� "
=
��# $
ultimoInventario
��% 5
!=
��6 8
null
��9 =
?
��> ?
ultimoInventario
��@ P
.
��P Q
fecha_inicio
��Q ]
.
��] ^
AddMilliseconds
��^ m
(
��m n
$num
��n o
)
��o p
:
��q r&
fechaPrimeraTransaccion��s �
!=��� �
null��� �
?��� �
(��� �
DateTime��� �
)��� �'
fechaPrimeraTransaccion��� �
:��� �
fechaActual��� �
;��� �
var
�� 

fechaHasta
�� "
=
��# $
fechaActual
��% 0
;
��0 1
var
�� 
inventarioFisico
�� (
=
��) *'
_inventarioHistoricoDatos
��+ D
.
��D E;
-ObtenerTransaccionInclusiveDetalleTransaccion
��E r
(
��r s
	idAlmacen
��s |
,
��| }"
TransaccionSettings��~ �
.��� �
Default��� �
.��� �1
!IdTipoTransaccionInventarioActual��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoConfirmado��� �
)��� �
;��� �
if
�� 
(
�� 
inventarioFisico
�� (
!=
��) +
null
��, 0
)
��0 1
{
�� 
var
�� 
nuevoInventario
�� +
=
��, -
inventarioFisico
��. >
.
��> ?'
CloneTransaccionYDetalles
��? X
(
��X Y
)
��Y Z
;
��Z [
nuevoInventario
�� '
.
��' (
codigo
��( .
=
��/ 0
$str
��1 5
;
��5 6
nuevoInventario
�� '
.
��' (
id_empleado
��( 3
=
��4 5

idEmpleado
��6 @
;
��@ A
nuevoInventario
�� '
.
��' (!
id_tipo_transaccion
��( ;
=
��< =!
TransaccionSettings
��> Q
.
��Q R
Default
��R Y
.
��Y Z2
$IdTipoTransaccionInventarioHistorico
��Z ~
;
��~ 
nuevoInventario
�� '
.
��' (

comentario
��( 2
=
��3 4
$str
��5 ~
;
��~ 
nuevoInventario
�� '
.
��' (
id_comprobante
��( 6
=
��7 8!
TransaccionSettings
��9 L
.
��L M
Default
��M T
.
��T U#
IdComprobanteGenerico
��U j
;
��j k
nuevoInventario
�� '
.
��' (
fecha_inicio
��( 4
=
��5 6

fechaHasta
��7 A
;
��A B
nuevoInventario
�� '
.
��' (
	fecha_fin
��( 1
=
��2 3

fechaHasta
��4 >
;
��> ?
nuevoInventario
�� '
.
��' (%
fecha_registro_contable
��( ?
=
��@ A

fechaHasta
��B L
;
��L M
nuevoInventario
�� '
.
��' ($
fecha_registro_sistema
��( >
=
��? @

fechaHasta
��A K
;
��K L 
Estado_transaccion
�� *
estadoTransaccion
��+ <
=
��= >
new
��? B 
Estado_transaccion
��C U
(
��U V

idEmpleado
��V `
,
��` a
MaestroSettings
��b q
.
��q r
Default
��r y
.
��y z/
 IdDetalleMaestroEstadoConfirmado��z �
,��� �
fechaActual��� �
,��� �
$str��� �
)��� �
;��� �
nuevoInventario
�� '
.
��' ( 
Estado_transaccion
��( :
.
��: ;
Add
��; >
(
��> ?
estadoTransaccion
��? P
)
��P Q
;
��Q R
nuevosInventarios
�� )
.
��) *
Add
��* -
(
��- .
nuevoInventario
��. =
)
��= >
;
��> ?
}
�� 
}
�� 
if
�� 
(
�� 
nuevosInventarios
�� %
.
��% &
Count
��& +
(
��+ ,
)
��, -
>
��. /
$num
��0 1
)
��1 2
{
�� 
return
�� $
_crearTransaccionDatos
�� 1
.
��1 2:
,CrearTransaccionesYDetallesTransaccionMasivo
��2 ^
(
��^ _
nuevosInventarios
��_ p
)
��p q
;
��q r
}
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str��* �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Q
,
��Q R
e
��S T
)
��T U
;
��U V
}
�� 
}
�� 	
public
�� 
OperationResult
�� ,
CrearInventariosLogicosPorLote
�� =
(
��= >
int
��> A

idEmpleado
��B L
)
��L M
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
DateTime
�� 
?
�� %
fechaPrimeraTransaccion
�� 1
=
��2 3
fechaActual
��4 ?
;
��? @
List
�� 
<
�� 
Transaccion
��  
>
��  !
nuevosInventarios
��" 3
=
��4 5
new
��6 9
List
��: >
<
��> ?
Transaccion
��? J
>
��J K
(
��K L
)
��L M
;
��M N
int
�� 
[
�� 
]
�� 
idsAlmacenes
�� "
=
��# $#
_maestrosAlmacenDatos
��% :
.
��: ;!
ObtenerIdsAlmacenes
��; N
(
��N O
)
��O P
;
��P Q
foreach
�� 
(
�� 
var
�� 
	idAlmacen
�� &
in
��' )
idsAlmacenes
��* 6
)
��6 7
{
�� 
var
�� 
ultimoInventario
�� (
=
��) *'
_inventarioHistoricoDatos
��+ D
.
��D E.
 ObtenerUltimoInventarioHistorico
��E e
(
��e f
	idAlmacen
��f o
)
��o p
;
��p q
if
�� 
(
�� 
ultimoInventario
�� (
==
��) +
null
��, 0
)
��1 2
{
�� %
fechaPrimeraTransaccion
�� /
=
��0 1'
_consultaTransaccionDatos
��2 K
.
��K L,
ObtenerFechaPrimeraTransaccion
��L j
(
��j k
	idAlmacen
��k t
)
��t u
;
��u v
}
�� 
var
�� 

fechaDesde
�� "
=
��# $
ultimoInventario
��% 5
!=
��6 8
null
��9 =
?
��> ?
ultimoInventario
��@ P
.
��P Q
fecha_inicio
��Q ]
.
��] ^
AddMilliseconds
��^ m
(
��m n
$num
��n o
)
��o p
:
��q r&
fechaPrimeraTransaccion��s �
!=��� �
null��� �
?��� �
(��� �
DateTime��� �
)��� �'
fechaPrimeraTransaccion��� �
:��� �
fechaActual��� �
;��� �
var
�� 

fechaHasta
�� "
=
��# $
fechaActual
��% 0
;
��0 1
var
�� 
movimientos
�� #
=
��$ %
_movimientosDatos
��& 7
.
��7 8G
9ObtenerMovimientosConceptoNegocioConLotePorEntidadInterna
��8 q
(
��q r
	idAlmacen
��r {
,
��{ |

fechaDesde��} �
,��� �

fechaHasta��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
List
�� 
<
�� (
VencimientoConceptoNegocio
�� 3
>
��3 4
conceptosConLote
��5 E
=
��F G
new
��H K
List
��L P
<
��P Q(
VencimientoConceptoNegocio
��Q k
>
��k l
(
��l m
)
��m n
;
��n o
if
�� 
(
�� 
ultimoInventario
�� (
!=
��) +
null
��, 0
)
��0 1
{
�� 
conceptosConLote
�� (
.
��( )
AddRange
��) 1
(
��1 2
ultimoInventario
��2 B
.
��B C!
Detalle_transaccion
��C V
.
��V W
Select
��W ]
(
��] ^
dt
��^ `
=>
��a c
new
��d g)
VencimientoConceptoNegocio��h �
(��� �
)��� �
{
�� 

IdConcepto
�� &
=
��' (
dt
��) +
.
��+ ,!
id_concepto_negocio
��, ?
,
��? @
Lote
��  
=
��! "
dt
��# %
.
��% &
lote
��& *
}
�� 
)
�� 
.
�� 
Distinct
�� #
(
��# $
)
��$ %
)
��% &
;
��& '
}
�� 
if
�� 
(
�� 
movimientos
�� #
!=
��$ &
null
��' +
)
��+ ,
{
�� 
conceptosConLote
�� (
.
��( )
AddRange
��) 1
(
��1 2
movimientos
��2 =
.
��= >
Select
��> D
(
��D E
m
��E F
=>
��G I
new
��J M(
VencimientoConceptoNegocio
��N h
(
��h i
)
��i j
{
�� 

IdConcepto
�� &
=
��' (
m
��) *
.
��* +!
Id_concepto_negocio
��+ >
,
��> ?
Lote
��  
=
��! "
m
��# $
.
��$ %
Lote
��% )
}
�� 
)
�� 
.
�� 
Distinct
�� #
(
��# $
)
��$ %
)
��% &
;
��& '
}
�� 
if
�� 
(
�� 
movimientos
�� #
!=
��$ &
null
��' +
&&
��, .
movimientos
��/ :
.
��: ;
Count
��; @
(
��@ A
)
��A B
>
��C D
$num
��E F
)
��F G
{
�� 
Transaccion
�� #
nuevoInventario
��$ 3
=
��4 5
new
��6 9
Transaccion
��: E
(
��E F
$str
��F H
,
��H I
null
��J N
,
��N O

fechaHasta
��P Z
,
��Z [!
TransaccionSettings
��\ o
.
��o p
Default
��p w
.
��w x3
$IdTipoTransaccionInventarioHistorico��x �
,��� �
MaestroSettings
�� +
.
��+ ,
Default
��, 3
.
��3 48
*IdDetalleMaestroUnidadDeNegocioTransversal
��4 ^
,
��^ _
false
��` e
,
��e f

fechaHasta
��g q
,
��q r

fechaHasta
��s }
,
��} ~"
TransaccionSettings�� �
.��� �
Default��� �
.��� �%
IdComprobanteGenerico��� �
,��� �
$str
�� e
,
��e f

fechaHasta
��g q
,
��q r

idEmpleado
��s }
,
��} ~
$num�� �
,��� �
	idAlmacen��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �+
IdDetalleMaestroMonedaSoles��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
	idAlmacen��� �
)��� �
;��� � 
Estado_transaccion
�� *
estadoTransaccion
��+ <
=
��= >
new
��? B 
Estado_transaccion
��C U
(
��U V

idEmpleado
��V `
,
��` a
MaestroSettings
��b q
.
��q r
Default
��r y
.
��y z/
 IdDetalleMaestroEstadoConfirmado��z �
,��� �
fechaActual��� �
,��� �
$str��� �
)��� �
;��� �
nuevoInventario
�� '
.
��' ( 
Estado_transaccion
��( :
.
��: ;
Add
��; >
(
��> ?
estadoTransaccion
��? P
)
��P Q
;
��Q R
nuevosInventarios
�� )
.
��) *
Add
��* -
(
��- .
nuevoInventario
��. =
)
��= >
;
��> ?
foreach
�� 
(
��  !
var
��! $
concepto
��% -
in
��. 0
conceptosConLote
��1 A
.
��A B
Distinct
��B J
(
��J K
)
��K L
)
��L M
{
�� 
decimal
�� # 
cantidad_principal
��$ 6
=
��7 8
$num
��9 :
;
��: ; 
cantidad_principal
�� .
=
��/ 0 
cantidad_principal
��1 C
+
��D E
(
��F G
ultimoInventario
��G W
!=
��X Z
null
��[ _
&&
��` b
ultimoInventario
�� ,
.
��, -!
Detalle_transaccion
��- @
.
��@ A
SingleOrDefault
��A P
(
��P Q
dt
��Q S
=>
��T V
dt
��W Y
.
��Y Z!
id_concepto_negocio
��Z m
==
��n p
concepto
��q y
.
��y z

IdConcepto��z �
&&��� �
dt��� �
.��� �
lote��� �
==��� �
concepto��� �
.��� �
Lote��� �
)��� �
!=��� �
null��� �
?��� �
ultimoInventario
�� ,
.
��, -!
Detalle_transaccion
��- @
.
��@ A
SingleOrDefault
��A P
(
��P Q
dt
��Q S
=>
��T V
dt
��W Y
.
��Y Z!
id_concepto_negocio
��Z m
==
��n p
concepto
��q y
.
��y z

IdConcepto��z �
&&��� �
dt��� �
.��� �
lote��� �
==��� �
concepto��� �
.��� �
Lote��� �
)��� �
.��� �
cantidad��� �
:��� �
$num��� �
)��� �
+
�� 
(
�� 
movimientos
�� '
.
��' (
Where
��( -
(
��- .
m
��. /
=>
��0 2
m
��3 4
.
��4 5!
Id_concepto_negocio
��5 H
==
��I K
concepto
��L T
.
��T U

IdConcepto
��U _
&&
��` b
m
��c d
.
��d e
Lote
��e i
==
��j l
concepto
��m u
.
��u v
Lote
��v z
)
��z {
!=
��| ~
null�� �
?��� �
movimientos
�� '
.
��' (
Where
��( -
(
��- .
m
��. /
=>
��0 2
m
��3 4
.
��4 5!
Id_concepto_negocio
��5 H
==
��I K
concepto
��L T
.
��T U

IdConcepto
��U _
&&
��` b
m
��c d
.
��d e
Lote
��e i
==
��j l
concepto
��m u
.
��u v
Lote
��v z
)
��z {
.
��{ |
Sum
��| 
(�� �
m��� �
=>��� �
m��� �
.��� �"
Entradas_principal��� �
)��� �
:��� �
$num��� �
)
�� 
-
�� 
(
�� 
movimientos
�� (
.
��( )
Where
��) .
(
��. /
m
��/ 0
=>
��1 3
m
��4 5
.
��5 6!
Id_concepto_negocio
��6 I
==
��J L
concepto
��M U
.
��U V

IdConcepto
��V `
&&
��a c
m
��d e
.
��e f
Lote
��f j
==
��k m
concepto
��n v
.
��v w
Lote
��w {
)
��{ |
!=
��} 
null��� �
?��� �
movimientos
�� (
.
��( )
Where
��) .
(
��. /
m
��/ 0
=>
��1 3
m
��4 5
.
��5 6!
Id_concepto_negocio
��6 I
==
��J L
concepto
��M U
.
��U V

IdConcepto
��V `
&&
��a c
m
��d e
.
��e f
Lote
��f j
==
��k m
concepto
��n v
.
��v w
Lote
��w {
)
��{ |
.
��| }
Sum��} �
(��� �
m��� �
=>��� �
m��� �
.��� �!
Salidas_principal��� �
)��� �
:��� �
$num��� �
)
�� 
;
�� 
decimal
�� #!
cantidad_secundaria
��$ 7
=
��8 9
$num
��: ;
;
��; <!
cantidad_secundaria
�� /
=
��0 1!
cantidad_secundaria
��2 E
+
��F G
(
��H I
decimal
��I P
)
��P Q
(
��Q R
ultimoInventario
��  0
!=
��1 3
null
��4 8
&&
��9 ;
ultimoInventario
��  0
.
��0 1!
Detalle_transaccion
��1 D
.
��D E
SingleOrDefault
��E T
(
��T U
dt
��U W
=>
��X Z
dt
��[ ]
.
��] ^!
id_concepto_negocio
��^ q
==
��r t
concepto
��u }
.
��} ~

IdConcepto��~ �
&&��� �
dt��� �
.��� �
lote��� �
==��� �
concepto��� �
.��� �
Lote��� �
)��� �
!=��� �
null��� �
?��� �
ultimoInventario
��  0
.
��0 1!
Detalle_transaccion
��1 D
.
��D E
SingleOrDefault
��E T
(
��T U
dt
��U W
=>
��X Z
dt
��[ ]
.
��] ^!
id_concepto_negocio
��^ q
==
��r t
concepto
��u }
.
��} ~

IdConcepto��~ �
&&��� �
dt��� �
.��� �
lote��� �
==��� �
concepto��� �
.��� �
Lote��� �
)��� �
.��� �#
cantidad_secundaria��� �
:��� �
$num��� �
+
��  !
(
��  !
movimientos
��" -
.
��- .
Where
��. 3
(
��3 4
m
��4 5
=>
��6 8
m
��9 :
.
��: ;!
Id_concepto_negocio
��; N
==
��O Q
concepto
��R Z
.
��Z [

IdConcepto
��[ e
&&
��f h
m
��i j
.
��j k
Lote
��k o
==
��p r
concepto
��s {
.
��{ |
Lote��| �
)��� �
!=��� �
null��� �
?��� �
movimientos
��! ,
.
��, -
Where
��- 2
(
��2 3
m
��3 4
=>
��5 7
m
��8 9
.
��9 :!
Id_concepto_negocio
��: M
==
��N P
concepto
��Q Y
.
��Y Z

IdConcepto
��Z d
&&
��e g
m
��h i
.
��i j
Lote
��j n
==
��o q
concepto
��r z
.
��z {
Lote
��{ 
)�� �
.��� �
Sum��� �
(��� �
m��� �
=>��� �
m��� �
.��� �"
Entradas_principal��� �
)��� �
:��� �
$num��� �
)
��! "
-
��! "
(
��! "
movimientos
��! ,
.
��, -
Where
��- 2
(
��2 3
m
��3 4
=>
��5 7
m
��8 9
.
��9 :!
Id_concepto_negocio
��: M
==
��N P
concepto
��Q Y
.
��Y Z

IdConcepto
��Z d
&&
��e g
m
��h i
.
��i j
Lote
��j n
==
��o q
concepto
��r z
.
��z {
Lote
��{ 
)�� �
!=��� �
null��� �
?��� �
movimientos
��! ,
.
��, -
Where
��- 2
(
��2 3
m
��3 4
=>
��5 7
m
��8 9
.
��9 :!
Id_concepto_negocio
��: M
==
��N P
concepto
��Q Y
.
��Y Z

IdConcepto
��Z d
&&
��e g
m
��h i
.
��i j
Lote
��j n
==
��o q
concepto
��r z
.
��z {
Lote
��{ 
)�� �
.��� �
Sum��� �
(��� �
m��� �
=>��� �
m��� �
.��� �!
Salidas_principal��� �
)��� �
:��� �
$num��� �
)
��  !
)
��! "
;
��" #
nuevoInventario
�� +
.
��+ ,!
Detalle_transaccion
��, ?
.
��? @
Add
��@ C
(
��C D
new
��D G!
Detalle_transaccion
��H [
(
��[ \ 
cantidad_principal
��\ n
,
��n o
concepto
��p x
.
��x y

IdConcepto��y �
,��� �
$str��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
null��� �
,��� �#
cantidad_secundaria��� �
,��� �
null��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
concepto��� �
.��� �
Lote��� �
,��� �
null��� �
,��� �
null��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
}
�� 
if
�� 
(
�� 
nuevosInventarios
�� %
.
��% &
Count
��& +
(
��+ ,
)
��, -
>
��. /
$num
��0 1
)
��1 2
{
�� 
return
�� $
_crearTransaccionDatos
�� 1
.
��1 2:
,CrearTransaccionesYDetallesTransaccionMasivo
��2 ^
(
��^ _
nuevosInventarios
��_ p
)
��p q
;
��q r
}
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str��* �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Q
,
��Q R
e
��S T
)
��T U
;
��U V
}
�� 
}
�� 	
public
�� '
InventarioConceptoNegocio
�� (<
.ObtenerInventarioHistoricoPorConceptoDeNegocio
��) W
(
��W X
int
��X [
	idAlmacen
��\ e
,
��e f
int
��g j

idConcepto
��k u
,
��u v
string
��w }
lote��~ �
,��� �
DateTime��� �
fecha��� �
)��� �
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
DateTime
�� 
?
�� %
fechaPrimeraTransaccion
�� 1
=
��2 3
fechaActual
��4 ?
;
��? @
DateTime
�� 

fechaHasta
�� #
=
��$ %
fecha
��& +
.
��+ ,
AddMilliseconds
��, ;
(
��; <
-
��< =
$num
��= >
)
��> ?
;
��? @
DateTime
�� 

fechaDesde
�� #
;
��# $
decimal
�� 
costo_unitario
�� &
=
��' (
$num
��) *
;
��* +
decimal
��  
cantidad_principal
�� *
=
��+ ,
$num
��- .
;
��. /
decimal
�� !
cantidad_secundaria
�� +
=
��, -
$num
��. /
;
��/ 0
decimal
�� 
costo_total
�� #
=
��$ %
$num
��& '
;
��' ('
InventarioConceptoNegocio
�� )
ultimoInventario
��* :
=
��; <'
_inventarioHistoricoDatos
��= V
.
��V W5
'ObtenerUltimoInventarioHistoricoAntesDe
��W ~
(
��~ 
	idAlmacen�� �
,��� �

idConcepto��� �
,��� �
lote��� �
,��� �
fecha��� �
)��� �
;��� �
costo_unitario
�� 
=
��  
ultimoInventario
��! 1
!=
��2 4
null
��5 9
?
��: ;
ultimoInventario
��< L
.
��L M

��M Z
:
��[ \
$num
��] ^
;
��^ _
costo_total
�� 
=
�� 
ultimoInventario
�� .
!=
��/ 1
null
��2 6
?
��7 8
ultimoInventario
��9 I
.
��I J

CostoTotal
��J T
:
��U V
$num
��W X
;
��X Y 
cantidad_principal
�� "
=
��# $
ultimoInventario
��% 5
!=
��6 8
null
��9 =
?
��> ?
ultimoInventario
��@ P
.
��P Q
CantidadPrincipal
��Q b
:
��c d
$num
��e f
;
��f g!
cantidad_secundaria
�� #
=
��$ %
ultimoInventario
��& 6
!=
��7 9
null
��: >
?
��? @
ultimoInventario
��A Q
.
��Q R 
CantidadSecundaria
��R d
:
��e f
$num
��g h
;
��h i
if
�� 
(
�� 
ultimoInventario
�� $
==
��% '
null
��( ,
)
��- .
{
�� %
fechaPrimeraTransaccion
�� +
=
��, -'
_consultaTransaccionDatos
��. G
.
��G H,
ObtenerFechaPrimeraTransaccion
��H f
(
��f g
	idAlmacen
��g p
)
��p q
;
��q r
}
�� 

fechaDesde
�� 
=
�� 
ultimoInventario
�� -
!=
��. 0
null
��1 5
?
��6 7
ultimoInventario
��8 H
.
��H I
Fecha
��I N
.
��N O
AddMilliseconds
��O ^
(
��^ _
$num
��_ `
)
��` a
:
��b c%
fechaPrimeraTransaccion
��d {
!=
��| ~
null�� �
?��� �
(��� �
DateTime��� �
)��� �'
fechaPrimeraTransaccion��� �
:��� �
fechaActual��� �
;��� �
var
�� 
movimientos
�� 
=
��  !
_movimientosDatos
��" 3
.
��3 4P
BObtenerMovimientosConceptoNegocioPorEntidadInternaYConceptoNegocio
��4 v
(
��v w
	idAlmacen��w �
,��� �

idConcepto��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
�� 

movimiento
�� '
in
��( *
movimientos
��+ 6
)
��6 7
{
�� 
var
�� 
factor
�� 
=
��  

movimiento
��! +
.
��+ ,
	EsIngreso
��, 5
?
��6 7
$num
��8 9
:
��: ;
-
��< =
$num
��= >
;
��> ? 
cantidad_principal
�� &
+=
��' )

movimiento
��* 4
.
��4 5 
Cantidad_Principal
��5 G
*
��H I
factor
��J P
;
��P Q!
cantidad_secundaria
�� '
+=
��( *

movimiento
��+ 5
.
��5 6!
Cantidad_Secundaria
��6 I
*
��J K
factor
��L R
;
��R S
costo_total
�� 
+=
��  "

movimiento
��# -
.
��- .
Total
��. 3
*
��4 5
factor
��6 <
;
��< =
}
�� 
costo_unitario
�� 
=
��  
(
��! "
costo_total
��" -
/
��. / 
cantidad_principal
��0 B
)
��B C
;
��C D
return
�� 
new
�� '
InventarioConceptoNegocio
�� 4
(
��4 5
)
��5 6
{
�� 
IdConceptoNegocio
�� %
=
��& '

idConcepto
��( 2
,
��2 3
Lote
�� 
=
�� 
lote
�� 
,
��  
Fecha
�� 
=
�� 

fechaHasta
�� &
,
��& '
CantidadPrincipal
�� %
=
��& ' 
cantidad_principal
��( :
,
��: ; 
CantidadSecundaria
�� &
=
��' (!
cantidad_secundaria
��) <
,
��< =

�� !
=
��" #
costo_unitario
��$ 2
,
��2 3

CostoTotal
�� 
=
��  
costo_total
��! ,
}
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* k
,
��k l
e
��m n
)
��n o
;
��o p
}
�� 
}
�� 	
private
�� !
Detalle_transaccion
�� #3
%CrearNuevoDetalleParaInventarioLogico
��$ I
(
��I J!
Detalle_transaccion
��J ]
detalleMovimiento
��^ o
,
��o p
long
��q u 
idInventaioLogico��v �
,��� �
bool��� �
	esEntrada��� �
)��� �
{
�� 	
var
�� 
factor
�� 
=
�� 
	esEntrada
�� "
?
��# $
$num
��% &
:
��' (
-
��) *
$num
��* +
;
��+ ,
return
�� 
new
�� !
Detalle_transaccion
�� *
(
��* +
)
��+ ,
{
�� 
id_transaccion
�� 
=
��  
idInventaioLogico
��! 2
,
��2 3!
id_concepto_negocio
�� #
=
��$ %
detalleMovimiento
��& 7
.
��7 8!
id_concepto_negocio
��8 K
,
��K L
cantidad
�� 
=
�� 
detalleMovimiento
�� ,
.
��, -
cantidad
��- 5
*
��6 7
factor
��8 >
,
��> ?!
cantidad_secundaria
�� #
=
��$ %
detalleMovimiento
��& 7
.
��7 8!
cantidad_secundaria
��8 K
*
��L M
factor
��N T
,
��T U
detalle
�� 
=
�� 
$str
�� &
,
��& '
precio_unitario
�� 
=
��  !
detalleMovimiento
��" 3
.
��3 4
precio_unitario
��4 C
,
��C D
total
�� 
=
�� 
detalleMovimiento
�� )
.
��) *
total
��* /
,
��/ 0
	id_precio
�� 
=
�� 
null
��  
,
��  !%
indicadorMultiproposito
�� '
=
��( )
$num
��* +
,
��+ , 
id_cuenta_contable
�� "
=
��# $
null
��% )
,
��) *
lote
�� 
=
�� 
detalleMovimiento
�� (
.
��( )
lote
��) -
}
�� 
;
��
}
�� 	
public
�� 
DateTime
�� 6
(ObtenerFechaDelUltimoInventarioHistorico
�� @
(
��@ A
int
��A D
	idAlmacen
��E N
)
��N O
{
�� 	
try
�� 
{
�� 
var
�� 
ultimoInventario
�� $
=
��% &'
_inventarioHistoricoDatos
��' @
.
��@ A.
 ObtenerUltimoInventarioHistorico
��A a
(
��a b
	idAlmacen
��b k
)
��k l
;
��l m
if
�� 
(
�� 
ultimoInventario
�� $
!=
��% '
null
��( ,
)
��, -
{
�� 
return
�� 
ultimoInventario
�� +
.
��+ ,
fecha_inicio
��, 8
;
��8 9
}
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* f
)
��f g
;
��g h
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* S
,
��S T
e
��U V
)
��V W
;
��W X
}
�� 
}
�� 	
public
�� 
Transaccion
�� +
ObtenerUltimoInventarioLogico
�� 8
(
��8 9
int
��9 <
	idAlmacen
��= F
)
��F G
{
�� 	
try
�� 
{
�� 
Transaccion
�� 
ultimoInventario
�� ,
=
��- .'
_inventarioHistoricoDatos
��/ H
.
��H I.
 ObtenerUltimoInventarioHistorico
��I i
(
��i j
	idAlmacen
��j s
)
��s t
;
��t u
return
�� 
ultimoInventario
�� '
;
��' (
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* X
,
��X Y
e
��Z [
)
��[ \
;
��\ ]
}
�� 
}
�� 	
}
�� 
}�� �
RD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Concepto\Caracteristica_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

partial 
class !
Caracteristica_Logica .
:/ 0"
ICaracteristica_Logica1 G
{ 
private 
readonly '
ICaracteristica_Repositorio 4&
_caracteristicaRepositorio5 O
;O P
public !
Caracteristica_Logica $
($ %'
ICaracteristica_Repositorio% @%
caracteristicaRepositorioA Z
)Z [
{ 	&
_caracteristicaRepositorio &
=' (%
caracteristicaRepositorio) B
;B C
} 	
public )
ConceptoConSusCaracteristicas ,B
6ObtenerConceptoNegocioConSusCaracteristicasYSusValores- c
(c d
intd g
idConceptoNegocioh y
)y z
{ 	
try 
{   
var!! 
concepto!! 
=!! &
_caracteristicaRepositorio!! 9
.!!9 :B
6ObtenerConceptoNegocioConSusCaracteristicasYSusValores!!: p
(!!p q
idConceptoNegocio	!!q �
)
!!� �
;
!!� �
concepto"" 
."" 
Caracteristicas"" (
.""( )
ForEach"") 0
(""0 1
c""1 2
=>""3 5
c""6 7
.""7 8
Nombre""8 >
=""? @
c""A B
.""B C
Nombre""C I
.""I J
ToLower""J Q
(""Q R
)""R S
)""S T
;""T U
concepto## 
.## 
Caracteristicas## (
.##( )
ForEach##) 0
(##0 1
c##1 2
=>##3 5
c##6 7
.##7 8
Valor##8 =
=##> ?
c##@ A
.##A B
Valor##B G
.##G H
ToLower##H O
(##O P
)##P Q
)##Q R
;##R S
return$$ 
concepto$$ 
;$$  
}%% 
catch&& 
(&& 
	Exception&& 
e&& 
)&& 
{'' 
throw(( 
new(( 
LogicaException(( )
((() *
$str((* t
,((t u
e((v w
)((w x
;((x y
})) 
}** 	
}++ 
},, ��
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Establecimientos\Sucursal_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Establecimientos# 3
{ 
public 

class 
Sucursal_Logica  
:! "
ISucursal_Logica# 3
{ 
private 
readonly (
IEstablecimiento_Repositorio 5'
_establecimientoRepositorio6 Q
;Q R
private 
readonly 
IActor_Repositorio +
_actorRepositorio, =
;= >
public 
Sucursal_Logica 
( (
IEstablecimiento_Repositorio ;&
establecimientoRepositorio< V
,V W
IActor_RepositorioX j
actorRepositoriok {
){ |
{ 	'
_establecimientoRepositorio '
=( )&
establecimientoRepositorio* D
;D E
_actorRepositorio 
= 
actorRepositorio  0
;0 1
} 	
public!! 
Actor!! "
CrearActorParaSucursal!! +
(!!+ ,
int!!, /
idActor!!0 7
,!!7 8
string!!9 ?#
informacionPublicitaria!!@ W
,!!W X
string!!Y _
nombre!!` f
,!!f g
string!!h n

,!!| }
string	!!~ �
telefono
!!� �
,
!!� �
string
!!� �
correo
!!� �
,
!!� �
	Direccion
!!� �
	direccion
!!� �
,
!!� �
byte
!!� �
[
!!� �
]
!!� �
foto
!!� �
)
!!� �
{"" 	
telefono## 
=## 
telefono## 
??##  "
$str### %
;##% &
correo$$ 
=$$ 
correo$$ 
??$$ 
$str$$ !
;$$! "$
EstablecimientoComercial%% $
sede%%% )
=%%* +'
_establecimientoRepositorio%%, G
.%%G H+
ObtenerEstablecimientoComercial%%H g
(%%g h

.%%u v
Default%%v }
.%%} ~
IdActorNegocioSede	%%~ �
)
%%� �
;
%%� �
string&& $
numeroDocumentoIdentidad&& +
=&&, -
sede&&. 2
.&&2 3
DocumentoIdentidad&&3 E
;&&E F
int'' 

='' 
sede''  $
.''$ %

;''2 3
int(( 
idEstadoLegalActor(( "
=((# $
sede((% )
.(() *

;((7 8
int)) 
idClaseActor)) 
=)) 
sede)) #
.))# $
IdClaseActor))$ 0
;))0 1
Actor** 
actor** 
;** 
if++ 
(++ 
idActor++ 
==++ 
$num++ 
)++ 
{,, 
actor-- 
=-- 
new-- 
Actor-- !
(--! "
)--" #
{.. "
id_documento_identidad// *
=//+ ,

.//: ;
Default//; B
.//B C'
IdTipoDocumentoIdentidadRuc//C ^
,//^ _
fecha_nacimiento00 $
=00% &
DateTimeUtil00' 3
.003 4
FechaActual004 ?
(00? @
)00@ A
,00A B&
numero_documento_identidad11 .
=11/ 0$
numeroDocumentoIdentidad111 I
,11I J

=22" #
nombre22$ *
,22* +
segundo_nombre33 "
=33# $
$str33% '
,33' (
telefono44 
=44 
telefono44 '
,44' (

=55" #

,551 2
id_clase_actor66 "
=66# $
idClaseActor66% 1
,661 2
id_estado_legal77 #
=77$ %
idEstadoLegalActor77& 8
,778 9
correo88 
=88 
correo88 #
,88# $

=99" #

,991 2

pagina_web:: 
=::  
$str::! #
,::# $&
informacion_multiproposito;; .
=;;/ 0#
informacionPublicitaria;;1 H
??;;I K
$str;;L N
}<< 
;<< 
}== 
else>> 
{?? 
actor@@ 
=@@ 
new@@ 
Actor@@ !
(@@! "
)@@" #
{AA 
idBB 
=BB 
idActorBB  
,BB  !"
id_documento_identidadCC *
=CC+ ,

.CC: ;
DefaultCC; B
.CCB C'
IdTipoDocumentoIdentidadRucCCC ^
,CC^ _
fecha_nacimientoDD $
=DD% &
DateTimeUtilDD' 3
.DD3 4
FechaActualDD4 ?
(DD? @
)DD@ A
,DDA B&
numero_documento_identidadEE .
=EE/ 0$
numeroDocumentoIdentidadEE1 I
,EEI J

=FF" #
nombreFF$ *
,FF* +
segundo_nombreGG "
=GG# $
$strGG% '
,GG' (
telefonoHH 
=HH 
telefonoHH '
,HH' (

=II" #

,II1 2
id_clase_actorJJ "
=JJ# $
idClaseActorJJ% 1
,JJ1 2
id_estado_legalKK #
=KK$ %
idEstadoLegalActorKK& 8
,KK8 9
correoLL 
=LL 
correoLL #
,LL# $

=MM" #

,MM1 2

pagina_webNN 
=NN  
$strNN! #
,NN# $&
informacion_multipropositoOO .
=OO/ 0#
informacionPublicitariaOO1 H
??OOI K
$strOOL N
}PP 
;PP 
}QQ 
ifRR 
(RR 
fotoRR 
!=RR 
nullRR 
)RR 
{SS 
actorTT 
.TT 
FotoTT 
=TT 
	CrearFotoTT &
(TT& '
fotoTT' +
)TT+ ,
;TT, -
}UU 
elseVV 
{WW 
actorXX 
.XX 
id_fotoXX 
=XX 

.XX- .
DefaultXX. 5
.XX5 6!
IdFotoActorPorDefectoXX6 K
;XXK L
}YY 
ifZZ 
(ZZ 
	direccionZZ 
!=ZZ 
nullZZ !
)ZZ! "
{[[ 
actor\\ 
.\\ 
	Direccion\\ 
=\\  !
CrearDirecciones\\" 2
(\\2 3
	direccion\\3 <
)\\< =
;\\= >
}]] 
return^^ 
actor^^ 
;^^ 
}__ 	
publicaa 
OperationResultaa 

(aa, -
stringaa- 3!
codigoEstablecimientoaa4 I
,aaI J
stringaaK Q(
codigoEstablecimientoDigemidaaR n
,aan o
stringaap v$
informacionPublicitaria	aaw �
,
aa� �
string
aa� �
nombre
aa� �
,
aa� �
string
aa� �

aa� �
,
aa� �
string
aa� �
telefono
aa� �
,
aa� �
string
aa� �
correo
aa� �
,
aa� �
	Direccion
aa� �
	direccion
aa� �
,
aa� �
byte
aa� �
[
aa� �
]
aa� �
foto
aa� �
)
aa� �
{bb 	
trycc 
{dd 
codigoEstablecimientoDigemidee ,
=ee- .

.ee< =
Defaultee= D
.eeD ED
8PermitirRegistroCodigoDigemidEnEstableciemientoComercialeeE }
?ee~ *
codigoEstablecimientoDigemid
ee� �
:
ee� �
null
ee� �
;
ee� �!
codigoEstablecimientoff %
=ff& '!
codigoEstablecimientoff( =
??ff> @
$strffA C
;ffC D
DateTimegg 
fechaActualgg $
=gg% &
DateTimeUtilgg' 3
.gg3 4
FechaActualgg4 ?
(gg? @
)gg@ A
;ggA B
DateTimehh 
fechaFinhh !
=hh" #
fechaActualhh$ /
.hh/ 0
AddYearshh0 8
(hh8 9

.hhF G
DefaulthhG N
.hhN OD
7vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna	hhO �
)
hh� �
;
hh� �

sucursalii &
=ii' (
newii) ,

(ii: ;

.iiH I
DefaultiiI P
.iiP Q

,ii^ _
fechaActualii` k
,iik l
fechaFiniim u
,iiu v"
codigoEstablecimiento	iiw �
,
ii� �
true
ii� �
,
ii� �
false
ii� �
,
ii� �
$str
ii� �
)
ii� �
{jj 
Actorkk 
=kk "
CrearActorParaSucursalkk 2
(kk2 3
$numkk3 4
,kk4 5#
informacionPublicitariakk6 M
,kkM N
nombrekkO U
,kkU V

,kkd e
telefonokkf n
,kkn o
correokkp v
,kkv w
	direccion	kkx �
,
kk� �
foto
kk� �
)
kk� �
}ll 
;ll 
sucursalmm 
.mm 
extension_jsonmm '
=mm( )(
codigoEstablecimientoDigemidmm* F
==mmG I
nullmmJ N
?mmO P
$strmmQ S
:mmT U
$strmmV k
+mml m)
codigoEstablecimientoDigemid	mmn �
+
mm� �
$str
mm� �
;
mm� �
returnnn 
_actorRepositorionn (
.nn( )
CrearActorNegocionn) :
(nn: ;
sucursalnn; C
)nnC D
;nnD E
}oo 
catchpp 
(pp 
	Exceptionpp 
epp 
)pp 
{qq 
returnrr 
newrr 
OperationResultrr *
(rr* +
err+ ,
)rr, -
;rr- .
}ss 
}tt 	
publicvv 
OperationResultvv 
ActualizarSucursalvv 1
(vv1 2
intvv2 5
idActorvv6 =
,vv= >
intvv? B

idSucursalvvC M
,vvM N
stringvvO U!
codigoEstablecimientovvV k
,vvk l
stringvvm s)
codigoEstablecimientoDigemid	vvt �
,
vv� �
string
vv� �%
informacionPublicitaria
vv� �
,
vv� �
string
vv� �
nombre
vv� �
,
vv� �
string
vv� �

vv� �
,
vv� �
string
vv� �
telefono
vv� �
,
vv� �
string
vv� �
correo
vv� �
,
vv� �
	Direccion
vv� �
	direccion
vv� �
,
vv� �
byte
vv� �
[
vv� �
]
vv� �
foto
vv� �
)
vv� �
{ww 	
tryxx 
{zz 
codigoEstablecimientoDigemid{{ ,
={{- .

.{{< =
Default{{= D
.{{D ED
8PermitirRegistroCodigoDigemidEnEstableciemientoComercial{{E }
?{{~ *
codigoEstablecimientoDigemid
{{� �
:
{{� �
null
{{� �
;
{{� �
DateTime|| 
fechaActual|| $
=||% &
DateTimeUtil||' 3
.||3 4
FechaActual||4 ?
(||? @
)||@ A
;||A B
DateTime}} 
fechaFin}} !
=}}" #
fechaActual}}$ /
.}}/ 0
AddYears}}0 8
(}}8 9

.}}F G
Default}}G N
.}}N OD
7vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna	}}O �
)
}}� �
;
}}� �

sucursal~~ &
=~~' (
new~~) ,

(~~: ;

idSucursal~~; E
,~~E F
idActor~~G N
,~~N O

.~~] ^
Default~~^ e
.~~e f

,~~s t
fechaActual	~~u �
,
~~� �
fechaFin
~~� �
,
~~� �#
codigoEstablecimiento
~~� �
,
~~� �
true
~~� �
,
~~� �
false
~~� �
,
~~� �
$str
~~� �
)
~~� �
{ 
Actor
�� 
=
�� $
CrearActorParaSucursal
�� 2
(
��2 3
idActor
��3 :
,
��: ;%
informacionPublicitaria
��< S
,
��S T
nombre
��U [
,
��[ \

��] j
,
��j k
telefono
��l t
,
��t u
correo
��v |
,
��| }
	direccion��~ �
,��� �
foto��� �
)��� �
}
�� 
;
�� 
sucursal
�� 
.
�� 
extension_json
�� '
=
��( )*
codigoEstablecimientoDigemid
��* F
==
��G I
null
��J N
?
��O P
$str
��Q S
:
��T U
$str
��V k
+
��l m+
codigoEstablecimientoDigemid��n �
+��� �
$str��� �
;��� �
return
�� 
_actorRepositorio
�� (
.
��( )?
1ActualizarActorNegocioSinTomarEnCuentaAParametros
��) Z
(
��Z [
sucursal
��[ c
)
��c d
;
��d e
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
Sucursal
�� 
>
�� '
ObtenerSucursalesVigentes
�� 7
(
��7 8
)
��8 9
{
�� 	
try
�� 
{
�� 
return
�� 
Sucursal
�� 
.
��  
Convert
��  '
(
��' (
_actorRepositorio
��( 9
.
��9 :6
(ObtenerActorDeNegocioPorRolVigentesAhora
��: b
(
��b c

��c p
.
��p q
Default
��q x
.
��x y

)��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� &
EstablecimientoComercial
�� '0
"ObtenerSucursalComoEstablecimiento
��( J
(
��J K
int
��K N

idSucursal
��O Y
)
��Y Z
{
�� 	
try
�� 
{
�� 
return
�� )
_establecimientoRepositorio
�� 2
.
��2 3-
ObtenerEstablecimientoComercial
��3 R
(
��R S

idSucursal
��S ]
)
��] ^
;
��^ _
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� &
EstablecimientoComercial
�� '
ObtenerSucursal
��( 7
(
��7 8
int
��8 ;

idSucursal
��< F
)
��F G
{
�� 	
try
�� 
{
�� 
return
�� )
_establecimientoRepositorio
�� 2
.
��2 3-
ObtenerEstablecimientoComercial
��3 R
(
��R S

idSucursal
��S ]
)
��] ^
;
��^ _
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
DarDeBajaSucursal
�� 0
(
��0 1
int
��1 4

idSucursal
��5 ?
)
��? @
{
�� 	
try
�� 
{
�� 
return
�� 
_actorRepositorio
�� (
.
��( )(
DarDeBajaActorNegocioAhora
��) C
(
��C D

idSucursal
��D N
,
��N O

��P ]
.
��] ^
Default
��^ e
.
��e f

��f s
)
��s t
;
��t u
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Direccion
�� 
>
�� 
CrearDirecciones
�� /
(
��/ 0
	Direccion
��0 9
	direccion
��: C
)
��C D
{
�� 	
List
�� 
<
�� 
	Direccion
�� 
>
�� 
direcciones
�� '
=
��( )
new
��* -
List
��. 2
<
��2 3
	Direccion
��3 <
>
��< =
(
��= >
)
��> ?
;
��? @
direcciones
�� 
.
�� 
Add
�� 
(
�� 
	direccion
�� %
)
��% &
;
��& '
return
�� 
direcciones
�� 
;
�� 
}
�� 	
public
�� 
Foto
�� 
	CrearFoto
�� 
(
�� 
byte
�� "
[
��" #
]
��# $
foto
��% )
)
��) *
{
�� 	
Foto
�� 

fotografia
�� 
=
�� 
new
�� !
Foto
��" &
(
��& '
)
��' (
;
��( )

fotografia
�� 
.
�� 
imagen
�� 
=
�� 
foto
��  $
;
��$ %
return
�� 

fotografia
�� 
;
�� 
}
�� 	
}
�� 
}�� ��
PD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Establecimientos\Sede_Logica.cs
	namespace
Tsp
 
.
Sigescom
.
Logica
.
Core
.
Establecimientos
{ 
public 

class 
Sede_Logica 
: 
ISede_Logica +
{ 
private 
readonly (
IEstablecimiento_Repositorio 5'
_establecimientoRepositorio6 Q
;Q R
private 
readonly 
ISede_Repositorio *
_sedeRepositorio+ ;
;; <
private 
readonly 
IActor_Repositorio +
_actorRepositorio, =
;= >
private 
readonly )
ICentroDeAtencion_Repositorio 6&
_centroAtencionRepositorio7 Q
;Q R
private 
readonly *
IValidacionActorNegocio_Logica 7)
_validacionActorNegocioLogica8 U
;U V
public 
Sede_Logica 
( (
IEstablecimiento_Repositorio 7&
establecimientoRepositorio8 R
,R S
ISede_RepositorioT e
sedeRepositoriof u
,u v
IActor_Repositorio	w �
actorRepositorio
� �
,
� �+
ICentroDeAtencion_Repositorio
� �'
centroAtencionRepositorio
� �
,
� �,
IValidacionActorNegocio_Logica
� �*
validacionActorNegocioLogica
� �
)
� �
{ 	'
_establecimientoRepositorio '
=( )&
establecimientoRepositorio* D
;D E
_sedeRepositorio 
= 
sedeRepositorio .
;. /
_actorRepositorio 
= 
actorRepositorio  0
;0 1&
_centroAtencionRepositorio &
=' (%
centroAtencionRepositorio) B
;B C)
_validacionActorNegocioLogica )
=* +(
validacionActorNegocioLogica, H
;H I
} 	
public"" 
Actor"" 
CrearActorParaSede"" '
(""' (
int""( +
idActor"", 3
,""3 4
string""5 ;$
numeroDocumentoIdentidad""< T
,""T U
string""V \!
codigoEstablecimiento""] r
,""r s
string""t z$
informacionPublicitaria	""{ �
,
""� �
int
""� �

""� �
,
""� �
int
""� �
idClaseActor
""� �
,
""� �
string
""� �
razonSocial
""� �
,
""� �
string
""� �
nombreComercial
""� �
,
""� �
string
""� �

""� �
,
""� �
string
""� �
telefono
""� �
,
""� �
string
""� �
correo
""� �
,
""� �
	Direccion
""� �
	direccion
""� �
,
""� �
byte
""� �
[
""� �
]
""� �
foto
""� �
)
""� �
{## 	
if$$ 
($$ $
numeroDocumentoIdentidad$$ (
.$$( )
Trim$$) -
($$- .
)$$. /
.$$/ 0
Length$$0 6
!=$$7 9
$num$$: <
)$$< =
throw$$> C
new$$D G
	Exception$$H Q
($$Q R
$str	$$R �
)
$$� �
;
$$� �
telefono%% 
=%% 
telefono%% 
??%%  "
$str%%# %
;%%% &
correo&& 
=&& 
correo&& 
??&& 
$str&& !
;&&! "
int'' 
idEstadoLegalActor'' "
;''" #
if(( 
((( 

==((  

.((. /
Default((/ 6
.((6 7%
IdTipoActorPersonaNatural((7 P
)((P Q
{)) 
idClaseActor** 
=** 
idClaseActor** +
!=**, .
$num**/ 0
?**1 2
idClaseActor**3 ?
:**@ A

.**O P
Default**P W
.**W X0
$IdClaseActorPersonaNaturalPorDefecto**X |
;**| }
idEstadoLegalActor++ "
=++# $

.++2 3
Default++3 :
.++: ;6
*IdEstadoLegalActorPersonaNaturalPorDefecto++; e
;++e f
},, 
else-- 
if-- 
(-- 

==--# %

.--3 4
Default--4 ;
.--; <&
IdTipoActorPersonaJuridica--< V
)--V W
{.. 
idClaseActor// 
=// 
idClaseActor// +
!=//, .
$num/// 0
?//1 2
idClaseActor//3 ?
://@ A

.//O P
Default//P W
.//W X1
%IdClaseActorPersonaJuridicaPorDefecto//X }
;//} ~
idEstadoLegalActor00 "
=00# $

.002 3
Default003 :
.00: ;7
+IdEstadoLegalActorPersonaJuridicaPorDefecto00; f
;00f g
}11 
else22 
{33 
idClaseActor44 
=44 
idClaseActor44 +
!=44, .
$num44/ 0
?441 2
idClaseActor443 ?
:44@ A

.44O P
Default44P W
.44W X0
$IdClaseActorEntidadInternaPorDefecto44X |
;44| }
idEstadoLegalActor55 "
=55# $

.552 3
Default553 :
.55: ;6
*IdEstadoLegalActorEntidadInternaPorDefecto55; e
;55e f
}66 
Actor77 
actor77 
;77 
if88 
(88 
idActor88 
==88 
$num88 
)88 
{99 
actor:: 
=:: 
new:: 
Actor:: !
(::! "
)::" #
{;; "
id_documento_identidad<< *
=<<+ ,

.<<: ;
Default<<; B
.<<B C'
IdTipoDocumentoIdentidadRuc<<C ^
,<<^ _
fecha_nacimiento== $
===% &
DateTimeUtil==' 3
.==3 4
FechaActual==4 ?
(==? @
)==@ A
,==A B&
numero_documento_identidad>> .
=>>/ 0$
numeroDocumentoIdentidad>>1 I
,>>I J

=??" #
razonSocial??$ /
,??/ 0
segundo_nombre@@ "
=@@# $
nombreComercial@@% 4
,@@4 5
telefonoAA 
=AA 
telefonoAA '
,AA' (

=BB" #

,BB1 2
id_clase_actorCC "
=CC# $
idClaseActorCC% 1
,CC1 2
id_estado_legalDD #
=DD$ %
idEstadoLegalActorDD& 8
,DD8 9
correoEE 
=EE 
correoEE #
,EE# $

=FF" #

,FF1 2

pagina_webGG 
=GG  
$strGG! #
,GG# $&
informacion_multipropositoHH .
=HH/ 0#
informacionPublicitariaHH1 H
??HHI K
$strHHL N
}II 
;II 
}JJ 
elseKK 
{LL 
actorMM 
=MM 
newMM 
ActorMM !
(MM! "
)MM" #
{NN 
idOO 
=OO 
idActorOO  
,OO  !"
id_documento_identidadPP *
=PP+ ,

.PP: ;
DefaultPP; B
.PPB C'
IdTipoDocumentoIdentidadRucPPC ^
,PP^ _
fecha_nacimientoQQ $
=QQ% &
DateTimeUtilQQ' 3
.QQ3 4
FechaActualQQ4 ?
(QQ? @
)QQ@ A
,QQA B&
numero_documento_identidadRR .
=RR/ 0$
numeroDocumentoIdentidadRR1 I
,RRI J

=SS" #
razonSocialSS$ /
,SS/ 0
segundo_nombreTT "
=TT# $
nombreComercialTT% 4
,TT4 5
telefonoUU 
=UU 
telefonoUU '
,UU' (

=VV" #

,VV1 2
id_clase_actorWW "
=WW# $
idClaseActorWW% 1
,WW1 2
id_estado_legalXX #
=XX$ %
idEstadoLegalActorXX& 8
,XX8 9
correoYY 
=YY 
correoYY #
,YY# $

=ZZ" #

,ZZ1 2

pagina_web[[ 
=[[  
$str[[! #
,[[# $&
informacion_multiproposito\\ .
=\\/ 0#
informacionPublicitaria\\1 H
??\\I K
$str\\L N
}]] 
;]] 
}^^ 
if__ 
(__ 
foto__ 
!=__ 
null__ 
)__ 
{`` 
actoraa 
.aa 
Fotoaa 
=aa 
	CrearFotoaa &
(aa& '
fotoaa' +
)aa+ ,
;aa, -
}bb 
elsecc 
{dd 
actoree 
.ee 
id_fotoee 
=ee 

.ee- .
Defaultee. 5
.ee5 6!
IdFotoActorPorDefectoee6 K
;eeK L
}ff 
ifgg 
(gg 
	direcciongg 
!=gg 
nullgg !
)gg! "
{hh 
actorii 
.ii 
	Direccionii 
=ii  !
CrearDireccionesii" 2
(ii2 3
	direccionii3 <
)ii< =
;ii= >
}jj 
returnkk 
actorkk 
;kk 
}ll 	
publicnn 
OperationResultnn 
	CrearSedenn (
(nn( )
stringnn) /$
numeroDocumentoIdentidadnn0 H
,nnH I
stringnnJ P!
codigoEstablecimientonnQ f
,nnf g
stringnnh n)
codigoEstablecimientoDigemid	nno �
,
nn� �
string
nn� �%
informacionPublicitaria
nn� �
,
nn� �
int
nn� �

nn� �
,
nn� �
int
nn� �
idClaseActor
nn� �
,
nn� �
string
nn� �
razonSocial
nn� �
,
nn� �
string
nn� �
nombreComercial
nn� �
,
nn� �
string
nn� �

nn� �
,
nn� �
string
nn� �
telefono
nn� �
,
nn� �
string
nn� �
correo
nn� �
,
nn� �
	Direccion
nn� �
	direccion
nn� �
,
nn� �
byte
nn� �
[
nn� �
]
nn� �
foto
nn� �
)
nn� �
{oo 	
trypp 
{qq 
codigoEstablecimientoDigemidrr ,
=rr- .

.rr< =
Defaultrr= D
.rrD ED
8PermitirRegistroCodigoDigemidEnEstableciemientoComercialrrE }
?rr~ *
codigoEstablecimientoDigemid
rr� �
:
rr� �
null
rr� �
;
rr� �!
codigoEstablecimientoss %
=ss& '!
codigoEstablecimientoss( =
??ss> @
$strssA C
;ssC D)
_validacionActorNegocioLogicauu -
.uu- .(
ValidarExistenciaDeDocumentouu. J
(uuJ K

.uuX Y
DefaultuuY `
.uu` a'
IdTipoDocumentoIdentidadRucuua |
,uu| }%
numeroDocumentoIdentidad	uu~ �
)
uu� �
;
uu� �
DateTimevv 
fechaIniciovv $
=vv% &
DateTimeUtilvv' 3
.vv3 4
FechaActualvv4 ?
(vv? @
)vv@ A
;vvA B
DateTimeww 
fechaFinww !
=ww" #
fechaInicioww$ /
.ww/ 0
AddYearsww0 8
(ww8 9

.wwF G
DefaultwwG N
.wwN OD
7vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna	wwO �
)
ww� �
;
ww� �

sedexx "
=xx# $
newxx% (

(xx6 7

.xxD E
DefaultxxE L
.xxL M
	IdRolSedexxM V
,xxV W
fechaInicioxxX c
,xxc d
fechaFinxxe m
,xxm n
$strxxo q
,xxq r
truexxs w
,xxw x
falsexxy ~
,xx~ 
$str
xx� �
)
xx� �
{yy 
Actorzz 
=zz 
CrearActorParaSedezz .
(zz. /
$numzz/ 0
,zz0 1$
numeroDocumentoIdentidadzz2 J
,zzJ K!
codigoEstablecimientozzL a
,zza b#
informacionPublicitariazzc z
,zzz {

,
zz� �
idClaseActor
zz� �
,
zz� �
razonSocial
zz� �
,
zz� �
nombreComercial
zz� �
,
zz� �

zz� �
,
zz� �
telefono
zz� �
,
zz� �
correo
zz� �
,
zz� �
	direccion
zz� �
,
zz� �
foto
zz� �
)
zz� �
}{{ 
;{{ 
sede|| 
.|| 
extension_json|| #
=||$ %(
codigoEstablecimientoDigemid||& B
==||C E
null||F J
?||K L
null||M Q
:||R S
$str||T i
+||j k)
codigoEstablecimientoDigemid	||l �
+
||� �
$str
||� �
;
||� �
return}} 
_actorRepositorio}} (
.}}( )
CrearActorNegocio}}) :
(}}: ;
sede}}; ?
)}}? @
;}}@ A
}~~ 
catch 
( 
	Exception 
e 
) 
{
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
ActualizarSede
�� -
(
��- .
int
��. 1
idActor
��2 9
,
��9 :
int
��; >
idSede
��? E
,
��E F
string
��G M&
numeroDocumentoIdentidad
��N f
,
��f g
string
��h n$
codigoEstablecimiento��o �
,��� �
string��� �,
codigoEstablecimientoDigemid��� �
,��� �
string��� �'
informacionPublicitaria��� �
,��� �
int��� �

,��� �
int��� �
idClaseActor��� �
,��� �
string��� �
razonSocial��� �
,��� �
string��� �
nombreComercial��� �
,��� �
string��� �

,��� �
string��� �
telefono��� �
,��� �
string��� �
correo��� �
,��� �
	Direccion��� �
	direccion��� �
,��� �
byte��� �
[��� �
]��� �
foto��� �
)��� �
{
�� 	
try
�� 
{
�� 
codigoEstablecimientoDigemid
�� ,
=
��- .

��/ <
.
��< =
Default
��= D
.
��D EF
8PermitirRegistroCodigoDigemidEnEstableciemientoComercial
��E }
?
��~ ,
codigoEstablecimientoDigemid��� �
:��� �
null��� �
;��� �
DateTime
�� 
fechaInicio
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
DateTime
�� 
fechaFin
�� !
=
��" #
fechaInicio
��$ /
.
��/ 0
AddYears
��0 8
(
��8 9

��9 F
.
��F G
Default
��G N
.
��N OF
7vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna��O �
)��� �
;��� �

�� 
sede
�� "
=
��# $
new
��% (

��) 6
(
��6 7
idSede
��7 =
,
��= >
idActor
��? F
,
��F G

��H U
.
��U V
Default
��V ]
.
��] ^
	IdRolSede
��^ g
,
��g h
fechaInicio
��i t
,
��t u
fechaFin
��v ~
,
��~ %
codigoEstablecimiento��� �
,��� �
true��� �
,��� �
false��� �
,��� �
$str��� �
)��� �
{
�� 
Actor
�� 
=
��  
CrearActorParaSede
�� .
(
��. /
idActor
��/ 6
,
��6 7&
numeroDocumentoIdentidad
��8 P
,
��P Q#
codigoEstablecimiento
��R g
,
��g h&
informacionPublicitaria��i �
,��� �

,��� �
idClaseActor��� �
,��� �
razonSocial��� �
,��� �
nombreComercial��� �
,��� �

,��� �
telefono��� �
,��� �
correo��� �
,��� �
	direccion��� �
,��� �
foto��� �
)��� �
}
�� 
;
�� 
sede
�� 
.
�� 
extension_json
�� #
=
��$ %*
codigoEstablecimientoDigemid
��& B
==
��C E
null
��F J
?
��K L
$str
��M O
:
��P Q
$str
��R g
+
��h i+
codigoEstablecimientoDigemid��j �
+��� �
$str��� �
;��� �
var
�� 
result
�� 
=
�� 
_actorRepositorio
�� .
.
��. /?
1ActualizarActorNegocioSinTomarEnCuentaAParametros
��/ `
(
��` a
sede
��a e
)
��e f
;
��f g
if
�� 
(
�� 
result
�� 
.
�� 
code_result
�� &
==
��' )!
OperationResultEnum
��* =
.
��= >
Success
��> E
)
��E F
{
�� (
_centroAtencionRepositorio
�� .
.
��. /M
?ActualizarDocumentoIdentidadDeTodosLosCentrosDeAtencionVigentes
��/ n
(
��n o
sede
��o s
.
��s t!
DocumentoIdentidad��t �
)��� �
;��� �
}
�� 
return
�� 
result
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
public
�� /
!EstablecimientoComercialExtendido
�� 0"
ObtenerSedeExtendida
��1 E
(
��E F
)
��F G
{
�� 	
try
�� 
{
�� 
return
�� )
_establecimientoRepositorio
�� 2
.
��2 36
(ObtenerEstablecimientoComercialExtendido
��3 [
(
��[ \

��\ i
.
��i j
Default
��j q
.
��q r!
IdActorNegocioSede��r �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
��  !
throw
��" '
;
��' (
}
��) *
}
�� 	
public
�� &
EstablecimientoComercial
�� '
ObtenerSede
��( 3
(
��3 4
)
��4 5
{
�� 	
try
�� 
{
�� 
return
�� )
_establecimientoRepositorio
�� 2
.
��2 3-
ObtenerEstablecimientoComercial
��3 R
(
��R S

��S `
.
��` a
Default
��a h
.
��h i 
IdActorNegocioSede
��i {
)
��{ |
;
��| }
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
��  !
throw
��" '
;
��' (
}
��) *
}
�� 	
public
�� 6
(EstablecimientoComercialExtendidoConLogo
�� 7 
ObtenerSedeConLogo
��8 J
(
��J K
)
��K L
{
�� 	
try
�� 
{
�� 
return
�� )
_establecimientoRepositorio
�� 2
.
��2 3=
/ObtenerEstablecimientoComercialExtendidoConLogo
��3 b
(
��b c

��c p
.
��p q
Default
��q x
.
��x y!
IdActorNegocioSede��y �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
��  !
throw
��" '
;
��' (
}
��) *
}
�� 	
public
�� 
List
�� 
<
�� 
	Direccion
�� 
>
�� 
CrearDirecciones
�� /
(
��/ 0
	Direccion
��0 9
	direccion
��: C
)
��C D
{
�� 	
List
�� 
<
�� 
	Direccion
�� 
>
�� 
direcciones
�� '
=
��( )
new
��* -
List
��. 2
<
��2 3
	Direccion
��3 <
>
��< =
(
��= >
)
��> ?
;
��? @
direcciones
�� 
.
�� 
Add
�� 
(
�� 
	direccion
�� %
)
��% &
;
��& '
return
�� 
direcciones
�� 
;
�� 
}
�� 	
public
�� 
Foto
�� 
	CrearFoto
�� 
(
�� 
byte
�� "
[
��" #
]
��# $
foto
��% )
)
��) *
{
�� 	
Foto
�� 

fotografia
�� 
=
�� 
new
�� !
Foto
��" &
(
��& '
)
��' (
;
��( )

fotografia
�� 
.
�� 
imagen
�� 
=
�� 
foto
��  $
;
��$ %
return
�� 

fotografia
�� 
;
�� 
}
�� 	
}
�� 
}�� �.
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Establecimientos\Establecimiento_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Establecimientos# 3
{ 
public

class
Establecimiento_Logica
:
IEstablecimiento_Logica
{ 
private 
readonly (
IEstablecimiento_Repositorio 5'
_establecimientoRepositorio6 Q
;Q R
public "
Establecimiento_Logica %
(% &(
IEstablecimiento_Repositorio& B&
establecimientoRepositorioC ]
)] ^
{ 	'
_establecimientoRepositorio '
=( )&
establecimientoRepositorio* D
;D E
} 	
public 
List 
< $
EstablecimientoComercial ,
>, -6
*ObtenerEstablecimientosComercialesVigentes. X
(X Y
)Y Z
{ 	
try 
{ 
return '
_establecimientoRepositorio 2
.2 36
*ObtenerEstablecimientosComercialesVigentes3 ]
(] ^
)^ _
._ `
ToList` f
(f g
)g h
;h i
} 
catch 
( 
	Exception 
e 
) 
{ 
throw   
e   
;   
}!! 
}"" 	
public## 
List## 
<## -
!EstablecimientoComercialExtendido## 5
>##5 6@
4ObtenerEstablecimientosComercialesExtendidosVigentes##7 k
(##k l
)##l m
{$$ 	
try%% 
{&& 
return'' '
_establecimientoRepositorio'' 2
.''2 3@
4ObtenerEstablecimientosComercialesExtendidosVigentes''3 g
(''g h
)''h i
.''i j
ToList''j p
(''p q
)''q r
;''r s
}(( 
catch)) 
()) 
	Exception)) 
e)) 
))) 
{** 
throw++ 
e++ 
;++ 
},, 
}-- 	
public00 
List00 
<00 
ItemGenerico00  
>00  !H
<ObtenerEstablecimientosComercialesVigentesComoItemsGenericos00" ^
(00^ _
)00_ `
{11 	
try22 
{33 
return44 '
_establecimientoRepositorio44 2
.442 3H
<ObtenerEstablecimientosComercialesVigentesComoItemsGenericos443 o
(44o p
)44p q
.44q r
ToList44r x
(44x y
)44y z
;44z {
}55 
catch66 
(66 
	Exception66 
e66 
)66 
{77 
throw88 
new88 
LogicaException88 )
(88) *
$str88* b
,88b c
e88d e
)88e f
;88f g
}99 
}:: 	
public<< 
List<< 
<<< #
ItemGenericoConSubItems<< +
><<+ ,E
9ObtenerEstablecimientosComercialesVigentesConSusAlmacenes<<- f
(<<f g
)<<g h
{== 	
try>> 
{?? 
return@@ '
_establecimientoRepositorio@@ 2
.@@2 3J
>ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRol@@3 q
(@@q r

.	@@ �
Default
@@� �
.
@@� �
IdRolAlmacen
@@� �
)
@@� �
.
@@� �
ToList
@@� �
(
@@� �
)
@@� �
;
@@� �
}AA 
catchBB 
(BB 
	ExceptionBB 
eBB 
)BB 
{CC 
throwDD 
newDD 
LogicaExceptionDD )
(DD) *
$strDD* b
,DDb c
eDDd e
)DDe f
;DDf g
}EE 
}FF 	
publicHH 
ListHH 
<HH #
ItemGenericoConSubItemsHH +
>HH+ ,A
5ObtenerEstablecimientosComercialesVigentesConSusCajasHH- b
(HHb c
)HHc d
{II 	
tryJJ 
{KK 
returnLL '
_establecimientoRepositorioLL 2
.LL2 3J
>ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRolLL3 q
(LLq r

.	LL �
Default
LL� �
.
LL� �
	IdRolCaja
LL� �
)
LL� �
.
LL� �
ToList
LL� �
(
LL� �
)
LL� �
;
LL� �
}MM 
catchNN 
(NN 
	ExceptionNN 
eNN 
)NN 
{OO 
throwPP 
newPP 
LogicaExceptionPP )
(PP) *
$strPP* b
,PPb c
ePPd e
)PPe f
;PPf g
}QQ 
}RR 	
publicSS 
ListSS 
<SS #
ItemGenericoConSubItemsSS +
>SS+ ,H
<ObtenerEstablecimientosComercialesVigentesConSusPuntosVentasSS- i
(SSi j
)SSj k
{TT 	
tryUU 
{VV 
returnWW '
_establecimientoRepositorioWW 2
.WW2 3J
>ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRolWW3 q
(WWq r

.	WW �
Default
WW� �
.
WW� �
IdRolPuntaDeVenta
WW� �
)
WW� �
.
WW� �
ToList
WW� �
(
WW� �
)
WW� �
;
WW� �
}XX 
catchYY 
(YY 
	ExceptionYY 
eYY 
)YY 
{ZZ 
throw[[ 
new[[ 
LogicaException[[ )
([[) *
$str[[* b
,[[b c
e[[d e
)[[e f
;[[f g
}\\ 
}]] 	
}^^ 
}__ �
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\ConceptoLogica_Validacion.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

partial 
class 
ConceptoLogica '
:( )
IConceptoLogica* 9
{ 
public 
bool )
ExisteCodigoDeBarraDeProducto 1
(1 2
string2 8
codigoBarra9 D
)D E
{ 	
try 
{ 
bool 
existe 
=  
_conceptoRepositorio 2
.2 3.
"ExisteCodigoDeBarraConceptoNegocio3 U
(U V
codigoBarraV a
)a b
;b c
return 
existe 
; 
} 
catch 
( 
	Exception 
e 
) 
{ 
throw 
new 
LogicaException )
() *
$str* X
,X Y
eZ [
)[ \
;\ ]
} 
} 	
public   
bool   )
ExisteNombreConceptoComercial   1
(  1 2
string  2 8
nombre  9 ?
)  ? @
{!! 	
try"" 
{## 
bool$$ 
existe$$ 
=$$  
_conceptoRepositorio$$ 2
.$$2 3'
ExisteNombreConceptoNegocio$$3 N
($$N O
nombre$$O U
)$$U V
;$$V W
return%% 
existe%% 
;%% 
}&& 
catch'' 
('' 
	Exception'' 
e'' 
)'' 
{(( 
throw)) 
new)) 
LogicaException)) )
())) *
$str))* O
,))O P
e))Q R
)))R S
;))S T
}** 
}++ 	
public-- 
bool-- -
!ExisteNombreDeValorCaracteristica-- 5
(--5 6
int--6 9
idCaracteristica--: J
,--J K
string--L R
valor--S X
)--X Y
{.. 	
try// 
{00 
bool11 
existe11 
=11  
_conceptoRepositorio11 2
.112 3-
!ExisteNombreDeValorCaracteristica113 T
(11T U
idCaracteristica11U e
,11e f
valor11g l
)11l m
;11m n
return22 
existe22 
;22 
}33 
catch44 
(44 
	Exception44 
e44 
)44 
{55 
throw66 
new66 
LogicaException66 )
(66) *
$str66* g
,66g h
e66i j
)66j k
;66k l
}77 
}88 	
}99 
}:: ��
\D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\CentroDeAtencion\CentroDeAtencion_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
CentrosDeAtencion# 4
{ 
public 

class #
CentroDeAtencion_Logica (
:) *$
ICentroDeAtencion_Logica+ C
{ 
private 
readonly )
ICentroDeAtencion_Repositorio 6(
_centroDeAtencionRepositorio7 S
;S T
private 
readonly (
IEstablecimiento_Repositorio 5'
_establecimientoRepositorio6 Q
;Q R
private 
readonly 
IRoles_Repositorio +
_rolesRepositorio, =
;= >
private 
readonly 
IActor_Repositorio +
_actorRepositorio, =
;= >
private 
readonly $
IInventarioActual_Logica 1$
_inventarioActual_Logica2 J
;J K
public #
CentroDeAtencion_Logica &
(& ')
ICentroDeAtencion_Repositorio' D'
centroDeAtencionRepositorioE `
,` a(
IEstablecimiento_Repositoriob ~'
establecimientoRepositorio	 �
,
� � 
IRoles_Repositorio
� �
rolesRepositorio
� �
,
� � 
IActor_Repositorio
� �
actorRepositorio
� �
,
� �&
IInventarioActual_Logica
� �%
inventarioActual_Logica
� �
)
� �
{ 	(
_centroDeAtencionRepositorio   (
=  ) *'
centroDeAtencionRepositorio  + F
;  F G'
_establecimientoRepositorio!! '
=!!( )&
establecimientoRepositorio!!* D
;!!D E
_rolesRepositorio"" 
="" 
rolesRepositorio""  0
;""0 1
_actorRepositorio## 
=## 
actorRepositorio##  0
;##0 1$
_inventarioActual_Logica$$ $
=$$% &#
inventarioActual_Logica$$' >
;$$> ?
}&& 	
public)) 
List)) 
<)) %
CentroDeAtencionExtendido)) -
>))- ./
#ObtenerCentrosDeAtencionProgramados))/ R
())R S
int))S V

idEmpleado))W a
)))a b
{** 	
return++ (
_centroDeAtencionRepositorio++ /
.++/ 0/
#ObtenerCentrosDeAtencionProgramados++0 S
(++S T

idEmpleado++T ^
)++^ _
;++_ `
},, 	
public.. 
List.. 
<.. 
CentroDeAtencion.. $
>..$ %0
$ObtenerCentrosDeAtencionProgramados_..& J
(..J K
int..K N

idEmpleado..O Y
)..Y Z
{// 	
return00 (
_centroDeAtencionRepositorio00 /
.00/ 00
$ObtenerCentrosDeAtencionProgramados_000 T
(00T U

idEmpleado00U _
)00_ `
.00` a
ToList00a g
(00g h
)00h i
;00i j
}11 	
public33 %
CentroDeAtencionExtendido33 (#
ObtenerCentroDeAtencion33) @
(33@ A
int33A D
id33E G
)33G H
{44 	
try55 
{66 
return77 (
_centroDeAtencionRepositorio77 3
.773 4$
_ObtenerCentroDeAtencion774 L
(77L M
id77M O
)77O P
;77P Q
}88 
catch99 
(99 
	Exception99 
e99 
)99 
{:: 
throw;; 
e;; 
;;; 
}<< 
}== 	
publicAA 
ListAA 
<AA %
CentroDeAtencionExtendidoAA -
>AA- .6
*ObtenerCentrosDeAtencionExtendidosVigentesAA/ Y
(AAY Z
)AAZ [
{BB 	
returnDD (
_centroDeAtencionRepositorioDD /
.DD/ 06
*ObtenerCentrosDeAtencionExtendidosVigentesDD0 Z
(DDZ [
)DD[ \
.DD\ ]
ToListDD] c
(DDc d
)DDd e
;DDe f
}EE 	
publicGG 
ListGG 
<GG 
CentroDeAtencionGG $
>GG$ %,
 ObtenerCentrosDeAtencionVigentesGG& F
(GGF G
)GGG H
{HH 	
tryII 
{JJ 
returnKK (
_centroDeAtencionRepositorioKK 3
.KK3 4,
 ObtenerCentrosDeAtencionVigentesKK4 T
(KKT U
)KKU V
.KKV W
ToListKKW ]
(KK] ^
)KK^ _
;KK_ `
}LL 
catchMM 
(MM 
	ExceptionMM 
eMM 
)MM 
{NN 
throwOO 
eOO 
;OO 
}PP 
}QQ 	
publicRR 
ListRR 
<RR %
CentroDeAtencionExtendidoRR -
>RR- .*
ObtenerPuntosDeVentaNoVigentesRR/ M
(RRM N
)RRN O
{SS 	
returnTT (
_centroDeAtencionRepositorioTT /
.TT/ 0:
.ObtenerCentrosDeAtencionExtendidosSegunRolHijoTT0 ^
(TT^ _

.TTl m
DefaultTTm t
.TTt u
IdRolPuntaDeVenta	TTu �
,
TT� �
false
TT� �
)
TT� �
.
TT� �
ToList
TT� �
(
TT� �
)
TT� �
;
TT� �
}UU 	
publicWW 
ListWW 
<WW %
CentroDeAtencionExtendidoWW -
>WW- .+
ObtenerPuntosDeCompraNoVigentesWW/ N
(WWN O
)WWO P
{XX 	
returnYY (
_centroDeAtencionRepositorioYY /
.YY/ 0:
.ObtenerCentrosDeAtencionExtendidosSegunRolHijoYY0 ^
(YY^ _

.YYl m
DefaultYYm t
.YYt u
IdRolPuntoDeCompra	YYu �
,
YY� �
false
YY� �
)
YY� �
.
YY� �
ToList
YY� �
(
YY� �
)
YY� �
;
YY� �
}ZZ 	
public\\ 
List\\ 
<\\ %
CentroDeAtencionExtendido\\ -
>\\- .&
ObtenerAlmacenesNoVigentes\\/ I
(\\I J
)\\J K
{]] 	
return^^ (
_centroDeAtencionRepositorio^^ /
.^^/ 0:
.ObtenerCentrosDeAtencionExtendidosSegunRolHijo^^0 ^
(^^^ _

.^^l m
Default^^m t
.^^t u
IdRolAlmacen	^^u �
,
^^� �
false
^^� �
)
^^� �
.
^^� �
ToList
^^� �
(
^^� �
)
^^� �
;
^^� �
}__ 	
public`` 
List`` 
<`` %
CentroDeAtencionExtendido`` -
>``- .(
ObtenerPuntosDeVentaVigentes``/ K
(``K L
)``L M
{aa 	
returnbb (
_centroDeAtencionRepositoriobb /
.bb/ 0:
.ObtenerCentrosDeAtencionExtendidosSegunRolHijobb0 ^
(bb^ _

.bbl m
Defaultbbm t
.bbt u
IdRolPuntaDeVenta	bbu �
,
bb� �
true
bb� �
)
bb� �
.
bb� �
ToList
bb� �
(
bb� �
)
bb� �
;
bb� �
}cc 	
publicee 
Listee 
<ee %
CentroDeAtencionExtendidoee -
>ee- .)
ObtenerPuntosDeCompraVigentesee/ L
(eeL M
)eeM N
{ff 	
returngg (
_centroDeAtencionRepositoriogg /
.gg/ 0:
.ObtenerCentrosDeAtencionExtendidosSegunRolHijogg0 ^
(gg^ _

.ggl m
Defaultggm t
.ggt u
IdRolPuntoDeCompra	ggu �
,
gg� �
true
gg� �
)
gg� �
.
gg� �
ToList
gg� �
(
gg� �
)
gg� �
;
gg� �
}hh 	
publicjj 
Listjj 
<jj %
CentroDeAtencionExtendidojj -
>jj- .$
ObtenerAlmacenesVigentesjj/ G
(jjG H
)jjH I
{kk 	
returnll (
_centroDeAtencionRepositorioll /
.ll/ 0:
.ObtenerCentrosDeAtencionExtendidosSegunRolHijoll0 ^
(ll^ _

.lll m
Defaultllm t
.llt u
IdRolAlmacen	llu �
,
ll� �
true
ll� �
)
ll� �
.
ll� �
ToList
ll� �
(
ll� �
)
ll� �
;
ll� �
}mm 	
publicoo 
Listoo 
<oo %
CentroDeAtencionExtendidooo -
>oo- . 
ObtenerCajasVigentesoo/ C
(ooC D
)ooD E
{pp 	
returnqq (
_centroDeAtencionRepositorioqq /
.qq/ 0:
.ObtenerCentrosDeAtencionExtendidosSegunRolHijoqq0 ^
(qq^ _

.qql m
Defaultqqm t
.qqt u
	IdRolCajaqqu ~
,qq~ 
true
qq� �
)
qq� �
.
qq� �
ToList
qq� �
(
qq� �
)
qq� �
;
qq� �
}rr 	
publictt 
inttt ;
/ObtenerIdCentroDeAtencionParaObtencionDePreciostt B
(ttB C
CentroDeAtencionttC S
centroDeAtencionttT d
,ttd e.
!EstablecimientoComercialExtendido	ttf �
establecimiento
tt� �
)
tt� �
{uu 	
tryvv 
{ww 
varxx 
politicaDePrecioxx $
=xx% &
VentasSettingsxx' 5
.xx5 6
Defaultxx6 =
.xx= >&
PoliticaDePreciosParaVentaxx> X
;xxX Y
intyy (
idCentroDeAtencionParaPrecioyy 0
=yy1 2
$numyy3 4
;yy4 5
ifzz 
(zz 
politicaDePreciozz $
==zz% '
(zz( )
intzz) ,
)zz, -*
PoliticaDePreciosParaVentaEnumzz- K
.zzK L
GlobalzzL R
)zzR S
{{{ (
idCentroDeAtencionParaPrecio|| 0
=||1 2
VentasSettings||3 A
.||A B
Default||B I
.||I JD
7IdCentroAtencionParaObtencionDePreciosPorPoliticaGlobal	||J �
;
||� �
}}} 
else~~ 
if~~ 
(~~ 
politicaDePrecio~~ )
==~~* ,
(~~- .
int~~. 1
)~~1 2*
PoliticaDePreciosParaVentaEnum~~2 P
.~~P Q$
EstablecimientoComercial~~Q i
)~~i j
{ *
idCentroDeAtencionParaPrecio
�� 0
=
��1 2
(
��3 4
int
��4 7
)
��7 8
establecimiento
��8 G
.
��G H6
(IdCentroDeAtencionParaObtencionDePrecios
��H p
;
��p q
}
�� 
else
�� 
if
�� 
(
�� 
politicaDePrecio
�� )
==
��* ,
(
��- .
int
��. 1
)
��1 2,
PoliticaDePreciosParaVentaEnum
��2 P
.
��P Q
CentroDeAtencion
��Q a
)
��a b
{
�� *
idCentroDeAtencionParaPrecio
�� 0
=
��1 2
centroDeAtencion
��3 C
.
��C D
Id
��D F
;
��F G
}
�� 
return
�� *
idCentroDeAtencionParaPrecio
�� 3
;
��3 4
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
int
�� =
/ObtenerIdCentroDeAtencionParaObtencionDePrecios
�� B
(
��B C
int
��C F
idCentroAtencion
��G W
)
��W X
{
�� 	
try
�� 
{
�� 
var
�� 
politicaDePrecio
�� $
=
��% &
VentasSettings
��' 5
.
��5 6
Default
��6 =
.
��= >(
PoliticaDePreciosParaVenta
��> X
;
��X Y
int
�� *
idCentroDeAtencionParaPrecio
�� 0
=
��1 2
$num
��3 4
;
��4 5
if
�� 
(
�� 
politicaDePrecio
�� $
==
��% '
(
��( )
int
��) ,
)
��, -,
PoliticaDePreciosParaVentaEnum
��- K
.
��K L
Global
��L R
)
��R S
{
�� *
idCentroDeAtencionParaPrecio
�� 0
=
��1 2
VentasSettings
��3 A
.
��A B
Default
��B I
.
��I JF
7IdCentroAtencionParaObtencionDePreciosPorPoliticaGlobal��J �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
politicaDePrecio
�� )
==
��* ,
(
��- .
int
��. 1
)
��1 2,
PoliticaDePreciosParaVentaEnum
��2 P
.
��P Q&
EstablecimientoComercial
��Q i
)
��i j
{
�� *
idCentroDeAtencionParaPrecio
�� 0
=
��1 2*
_centroDeAtencionRepositorio
��3 O
.
��O PT
EObtenerIdDelCentroDeAtencionQueTieneLosPreciosSegunIdCentroDeAtencion��P �
(��� � 
idCentroAtencion��� �
)��� �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
politicaDePrecio
�� )
==
��* ,
(
��- .
int
��. 1
)
��1 2,
PoliticaDePreciosParaVentaEnum
��2 P
.
��P Q
CentroDeAtencion
��Q a
)
��a b
{
�� *
idCentroDeAtencionParaPrecio
�� 0
=
��1 2
idCentroAtencion
��3 C
;
��C D
}
�� 
return
�� *
idCentroDeAtencionParaPrecio
�� 3
;
��3 4
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
int
�� ;
-ObtenerIdCentroDeAtencionParaObtencionDeStock
�� @
(
��@ A
ModoOperacionEnum
��A R
tipoDeVenta
��S ^
,
��^ _
CentroDeAtencion
��` p
centroDeAtencion��q �
,��� �8
(EstablecimientoComercialExtendidoConLogo��� �
establecimiento��� �
)��� �
{
�� 	
try
�� 
{
�� 
int
�� 
politicaDeStock
�� #
=
��$ %
$num
��& '
;
��' (
int
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8
$num
��9 :
;
��: ;
if
�� 
(
�� 
tipoDeVenta
�� 
==
��  "
ModoOperacionEnum
��# 4
.
��4 5
PorMostrador
��5 A
)
��A B
{
�� 
politicaDeStock
�� #
=
��$ %
VentasSettings
��& 4
.
��4 5
Default
��5 <
.
��< =2
$PoliticaDeStockParaVentaPorMostrador
��= a
;
��a b
}
�� 
else
�� 
if
�� 
(
�� 
tipoDeVenta
�� $
==
��% '
ModoOperacionEnum
��( 9
.
��9 :$
PorMostradorEnDosPasos
��: P
)
��P Q
{
�� 
politicaDeStock
�� #
=
��$ %
VentasSettings
��& 4
.
��4 5
Default
��5 <
.
��< =<
.PoliticaDeStockParaVentaPorMostradorEnDosPasos
��= k
;
��k l
}
�� 
else
�� 
if
�� 
(
�� 
tipoDeVenta
�� $
==
��% '
ModoOperacionEnum
��( 9
.
��9 :
Corporativa
��: E
)
��E F
{
�� 
politicaDeStock
�� #
=
��$ %
VentasSettings
��& 4
.
��4 5
Default
��5 <
.
��< =1
#PoliticaDeStockParaVentaCorporativa
��= `
;
��` a
}
�� 
if
�� 
(
�� 
politicaDeStock
�� #
==
��$ &
(
��' (
int
��( +
)
��+ ,,
PoliticaDePreciosParaVentaEnum
��, J
.
��J K
Global
��K Q
)
��Q R
{
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8
VentasSettings
��9 G
.
��G H
Default
��H O
.
��O PD
5IdCentroAtencionParaObtencionDeStockPorPoliticaGlobal��P �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
politicaDeStock
�� (
==
��) +
(
��, -
int
��- 0
)
��0 1,
PoliticaDePreciosParaVentaEnum
��1 O
.
��O P&
EstablecimientoComercial
��P h
)
��h i
{
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8
(
��9 :
int
��: =
)
��= >
establecimiento
��> M
.
��M N4
&IdCentroDeAtencionParaObtencionDeStock
��N t
;
��t u
}
�� 
else
�� 
if
�� 
(
�� 
politicaDeStock
�� (
==
��) +
(
��, -
int
��- 0
)
��0 1,
PoliticaDePreciosParaVentaEnum
��1 O
.
��O P
CentroDeAtencion
��P `
)
��` a
{
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8
centroDeAtencion
��9 I
.
��I J
Id
��J L
;
��L M
}
�� 
return
�� 0
"idCentroDeAtencionParaObtenerStock
�� 9
;
��9 :
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
CentroDeAtencion
�� &
ObtenerCentroDeAtencion_
��  8
(
��8 9
int
��9 <
idCentroAtencion
��= M
)
��M N
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4&
ObtenerCentroDeAtencion_
��4 L
(
��L M
idCentroAtencion
��M ]
)
��] ^
;
��^ _
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* [
,
��[ \
e
��] ^
)
��^ _
;
��_ `
}
�� 
}
�� 	
public
�� 
int
�� ;
-ObtenerIdCentroDeAtencionParaObtencionDeStock
�� @
(
��@ A
ModoOperacionEnum
��A R
tipoDeVenta
��S ^
,
��^ _
int
��` c
idCentroAtencion
��d t
)
��t u
{
�� 	
try
�� 
{
�� 
int
�� 
politicaDeStock
�� #
=
��$ %
$num
��& '
;
��' (
int
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8
$num
��9 :
;
��: ;
if
�� 
(
�� 
tipoDeVenta
�� 
==
��  "
ModoOperacionEnum
��# 4
.
��4 5
PorMostrador
��5 A
)
��A B
{
�� 
politicaDeStock
�� #
=
��$ %
VentasSettings
��& 4
.
��4 5
Default
��5 <
.
��< =2
$PoliticaDeStockParaVentaPorMostrador
��= a
;
��a b
}
�� 
else
�� 
if
�� 
(
�� 
tipoDeVenta
�� $
==
��% '
ModoOperacionEnum
��( 9
.
��9 :$
PorMostradorEnDosPasos
��: P
)
��P Q
{
�� 
politicaDeStock
�� #
=
��$ %
VentasSettings
��& 4
.
��4 5
Default
��5 <
.
��< =<
.PoliticaDeStockParaVentaPorMostradorEnDosPasos
��= k
;
��k l
}
�� 
else
�� 
if
�� 
(
�� 
tipoDeVenta
�� $
==
��% '
ModoOperacionEnum
��( 9
.
��9 :
Corporativa
��: E
)
��E F
{
�� 
politicaDeStock
�� #
=
��$ %
VentasSettings
��& 4
.
��4 5
Default
��5 <
.
��< =1
#PoliticaDeStockParaVentaCorporativa
��= `
;
��` a
}
�� 
if
�� 
(
�� 
politicaDeStock
�� #
==
��$ &
(
��' (
int
��( +
)
��+ ,,
PoliticaDePreciosParaVentaEnum
��, J
.
��J K
Global
��K Q
)
��Q R
{
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8
VentasSettings
��9 G
.
��G H
Default
��H O
.
��O PD
5IdCentroAtencionParaObtencionDeStockPorPoliticaGlobal��P �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
politicaDeStock
�� (
==
��) +
(
��, -
int
��- 0
)
��0 1,
PoliticaDePreciosParaVentaEnum
��1 O
.
��O P&
EstablecimientoComercial
��P h
)
��h i
{
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8*
_centroDeAtencionRepositorio
��9 U
.
��U VQ
BObtenerIdDelCentroDeAtencionQueTieneElStockSegunIdCentroDeAtencion��V �
(��� � 
idCentroAtencion��� �
)��� �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
politicaDeStock
�� (
==
��) +
(
��, -
int
��- 0
)
��0 1,
PoliticaDePreciosParaVentaEnum
��1 O
.
��O P
CentroDeAtencion
��P `
)
��` a
{
�� 0
"idCentroDeAtencionParaObtenerStock
�� 6
=
��7 8
idCentroAtencion
��9 I
;
��I J
}
�� 
return
�� 0
"idCentroDeAtencionParaObtenerStock
�� 9
;
��9 :
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
string
�� -
ObtenerNombreDeCentroDeAtencion
�� 5
(
��5 6
int
��6 9
idActorNegocio
��: H
)
��H I
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4-
ObtenerNombreDeCentroDeAtencion
��4 S
(
��S T
idActorNegocio
��T b
)
��b c
;
��c d
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* f
,
��f g
e
��h i
)
��i j
;
��j k
}
�� 
}
�� 	
public
�� '
CentroDeAtencionExtendido
�� (:
,ObtenerCentroDeAtencionSegunSerieComprobante
��) U
(
��U V
int
��V Y
idSerie
��Z a
)
��a b
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4D
6ObtenerCentroDeAtencionExtendidosSegunSerieComprobante
��4 j
(
��j k
idSerie
��k r
)
��r s
;
��s t
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
RolDeNegocio
��  
>
��  !,
ObtenerRolesDeCentroDeAtencion
��" @
(
��@ A
)
��A B
{
�� 	
try
�� 
{
�� 
return
�� 
RolDeNegocio
�� #
.
��# $
Convert_
��$ ,
(
��, -
_rolesRepositorio
��- >
.
��> ?
ObtenerRolesHijos
��? P
(
��P Q

��Q ^
.
��^ _
Default
��_ f
.
��f g!
IdRolEntidadInterna
��g z
)
��z {
.
��{ |
ToList��| �
(��� �
)��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 
List
�� 
<
�� 
DetalleGenerico
�� #
>
��# $#
ObtenerPuntosDePrecio
��% :
(
��: ;
)
��; <
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
DetalleGenerico
�� $
>
��$ %
puntosDePrecio
��& 4
=
��5 6
new
��7 :
List
��; ?
<
��? @
DetalleGenerico
��@ O
>
��O P
(
��P Q
)
��Q R
;
��R S
int
�� 
politicaDePrecio
�� $
=
��% &
VentasSettings
��' 5
.
��5 6
Default
��6 =
.
��= >(
PoliticaDePreciosParaVenta
��> X
;
��X Y
if
�� 
(
�� 
politicaDePrecio
�� $
==
��% '
(
��( )
int
��) ,
)
��, -,
PoliticaDePreciosParaVentaEnum
��- K
.
��K L
Global
��L R
)
��R S
{
�� 
var
�� *
centroDeAtencionPrecioGlobal
�� 4
=
��5 6*
_centroDeAtencionRepositorio
��7 S
.
��S T&
_ObtenerCentroDeAtencion
��T l
(
��l m
VentasSettings
��m {
.
��{ |
Default��| �
.��� �G
7IdCentroAtencionParaObtencionDePreciosPorPoliticaGlobal��� �
)��� �
;��� �
puntosDePrecio
�� "
.
��" #
Add
��# &
(
��& '
new
��' *
DetalleGenerico
��+ :
(
��: ;*
centroDeAtencionPrecioGlobal
��; W
.
��W X
Id
��X Z
,
��Z [*
centroDeAtencionPrecioGlobal
��\ x
.
��x y'
EstablecimientoComercial��y �
.��� �

+��� �
$str��� �
+��� �,
centroDeAtencionPrecioGlobal��� �
.��� �
Nombre��� �
)��� �
)��� �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
politicaDePrecio
�� )
==
��* ,
(
��- .
int
��. 1
)
��1 2,
PoliticaDePreciosParaVentaEnum
��2 P
.
��P Q&
EstablecimientoComercial
��Q i
)
��i j
{
�� 
var
�� :
,centrosDeAtencionConPrecioPorEstablecimiento
�� D
=
��E F*
_centroDeAtencionRepositorio
��G c
.
��c dL
=ObtenerCentrosDeAtencionConPrecioDeCadaEstablecimientoVigente��d �
(��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �:
,centrosDeAtencionConPrecioPorEstablecimiento
�� @
.
��@ A
ForEach
��A H
(
��H I
ca
��I K
=>
��L N
puntosDePrecio
��O ]
.
��] ^
Add
��^ a
(
��a b
new
��b e
DetalleGenerico
��f u
(
��u v
ca
��v x
.
��x y
Id
��y {
,
��{ |
ca
��} 
.�� �(
EstablecimientoComercial��� �
.��� �

+��� �
$str��� �
+��� �
ca��� �
.��� �
Nombre��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
politicaDePrecio
�� )
==
��* ,
(
��- .
int
��. 1
)
��1 2,
PoliticaDePreciosParaVentaEnum
��2 P
.
��P Q
CentroDeAtencion
��Q a
)
��a b
{
�� 
var
�� 
centrosDeAtencion
�� )
=
��* +*
_centroDeAtencionRepositorio
��, H
.
��H I.
 ObtenerCentrosDeAtencionVigentes
��I i
(
��i j
)
��j k
;
��k l
foreach
�� 
(
�� 
var
��  
centroDeAtencion
��! 1
in
��2 4
centrosDeAtencion
��5 F
)
��F G
{
�� 
puntosDePrecio
�� &
.
��& '
Add
��' *
(
��* +
new
��+ .
DetalleGenerico
��/ >
(
��> ?
centroDeAtencion
��? O
.
��O P
Id
��P R
,
��R S
centroDeAtencion
��T d
.
��d e&
EstablecimientoComercial
��e }
.
��} ~

+��� �
$str��� �
+��� � 
centroDeAtencion��� �
.��� �
Nombre��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
puntosDePrecio
�� 
=
��  
puntosDePrecio
��! /
.
��/ 0
OrderBy
��0 7
(
��7 8
pp
��8 :
=>
��; =
pp
��> @
.
��@ A
Id
��A C
)
��C D
.
��D E
ToList
��E K
(
��K L
)
��L M
;
��M N
return
�� 
puntosDePrecio
�� %
;
��% &
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Q
,
��Q R
e
��S T
)
��T U
;
��U V
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .I
;ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial
��/ j
(
��j k
int
��k n)
idEstablecimientoComercial��o �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4S
EObtenerCentrosDeAtencionExtendidosVigentesPorEstablecimientoComercial
��4 y
(
��y z)
idEstablecimientoComercial��z �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .E
7ObtenerPuntosDeVentaVigentesPorEstablecimientoComercial
��/ f
(
��f g
int
��g j)
idEstablecimientoComercial��k �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4Y
KObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial
��4 
(�� �

.��� �
Default��� �
.��� �!
IdRolPuntaDeVenta��� �
,��� �*
idEstablecimientoComercial��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* g
,
��g h
e
��i j
)
��j k
;
��k l
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .F
8ObtenerPuntosDeCompraVigentesPorEstablecimientoComercial
��/ g
(
��g h
int
��h k)
idEstablecimientoComercial��l �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4Y
KObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial
��4 
(�� �

.��� �
Default��� �
.��� �"
IdRolPuntoDeCompra��� �
,��� �*
idEstablecimientoComercial��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* h
,
��h i
e
��j k
)
��k l
;
��l m
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .A
3ObtenerAlmacenesVigentesPorEstablecimientoComercial
��/ b
(
��b c
int
��c f)
idEstablecimientoComercial��g �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4Y
KObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial
��4 
(�� �

.��� �
Default��� �
.��� �
IdRolAlmacen��� �
,��� �*
idEstablecimientoComercial��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* `
,
��` a
e
��b c
)
��c d
;
��d e
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .=
/ObtenerCajasVigentesPorEstablecimientoComercial
��/ ^
(
��^ _
int
��_ b(
idEstablecimientoComercial
��c }
)
��} ~
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4Y
KObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial
��4 
(�� �

.��� �
Default��� �
.��� �
	IdRolCaja��� �
,��� �*
idEstablecimientoComercial��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ]
,
��] ^
e
��_ `
)
��` a
;
��a b
}
�� 
}
�� 	
public
�� 
int
�� 
[
�� 
]
�� 7
)ObtenerIdsCentrosAtencionConRolPuntoVenta
�� >
(
��> ?
int
��? B(
idEstablecimientoComercial
��C ]
)
��] ^
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4T
FObtenerIdsDeCentrosDeAtencionVigentesSegunRolYEstablecimientoComercial
��4 z
(
��z {

.��� �
Default��� �
.��� �!
IdRolPuntaDeVenta��� �
,��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ]
,
��] ^
e
��_ `
)
��` a
;
��a b
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .H
:ObtenerPuntosDeVentaVigentesPorEstablecimientosComerciales
��/ i
(
��i j
List
��j n
<
��n o
int
��o r
>
��r s-
idsEstablecimientosComerciales��t �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4U
GObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos
��4 {
(
��{ |

.��� �
Default��� �
.��� �!
IdRolPuntaDeVenta��� �
,��� �.
idsEstablecimientosComerciales��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* g
,
��g h
e
��i j
)
��j k
;
��k l
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .I
;ObtenerPuntosDeCompraVigentesPorEstablecimientosComerciales
��/ j
(
��j k
List
��k o
<
��o p
int
��p s
>
��s t-
idsEstablecimientosComerciales��u �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4U
GObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos
��4 {
(
��{ |

.��� �
Default��� �
.��� �"
IdRolPuntoDeCompra��� �
,��� �.
idsEstablecimientosComerciales��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* h
,
��h i
e
��j k
)
��k l
;
��l m
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .D
6ObtenerAlmacenesVigentesPorEstablecimientosComerciales
��/ e
(
��e f
List
��f j
<
��j k
int
��k n
>
��n o-
idsEstablecimientosComerciales��p �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4U
GObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos
��4 {
(
��{ |

.��� �
Default��� �
.��� �
IdRolAlmacen��� �
,��� �.
idsEstablecimientosComerciales��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* `
,
��` a
e
��b c
)
��c d
;
��d e
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� -
>
��- .@
2ObtenerCajasVigentesPorEstablecimientosComerciales
��/ a
(
��a b
List
��b f
<
��f g
int
��g j
>
��j k-
idsEstablecimientosComerciales��l �
)��� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4U
GObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos
��4 {
(
��{ |

.��� �
Default��� �
.��� �
	IdRolCaja��� �
,��� �.
idsEstablecimientosComerciales��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ]
,
��] ^
e
��_ `
)
��` a
;
��a b
}
�� 
}
�� 	
public
�� '
CentroDeAtencionExtendido
�� (2
$ObtenerCentroDeAtencionSucursalOSede
��) M
(
��M N
int
��N Q 
idCentroDeAtencion
��R d
,
��d e
int
��f i#
idActorDeNegocioPadre
��j 
)�� �
{
�� 	
try
�� 
{
�� 
return
�� *
_centroDeAtencionRepositorio
�� 3
.
��3 4Y
KObtenerCentroDeAtencionExtendidoPorIdDeCentroDeAtencionEIdDeEstablecimiento
��4 
(�� �"
idCentroDeAtencion��� �
,��� �%
idActorDeNegocioPadre��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
OperationResult
�� '
DarDeBajaCentroDeAtencion
�� 8
(
��8 9
int
��9 < 
idCentroDeAtencion
��= O
)
��O P
{
�� 	
try
�� 
{
�� 
return
�� 
_actorRepositorio
�� (
.
��( )(
DarDeBajaActorNegocioAhora
��) C
(
��C D 
idCentroDeAtencion
��D V
,
��V W

��X e
.
��e f
Default
��f m
.
��m n"
IdRolEntidadInterna��n �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
OperationResult
�� S
EEstablecerCentroDeAtencionParaPreciosYStockDeEstablecimientoComercial
�� d
(
��d e
int
��e h)
idEstablecimientoComencial��i �
,��� �
int��� �)
idCentroDeAtencionPrecios��� �
,��� �
int��� �'
idCentroDeAtencionStock��� �
)��� �
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' (
new
��) ,
OperationResult
��- <
(
��< =
)
��= >
;
��> ?
if
�� 
(
�� '
idCentroDeAtencionPrecios
�� -
>
��. /
$num
��0 1
)
��1 2
{
�� %
Parametro_actor_negocio
�� +;
-parametroCentroDeAtencionParaObtencionPrecios
��, Y
=
��Z [)
_establecimientoRepositorio
��\ w
.
��w xC
4ObtenerParametroCentroDeAtencionParaObtencionPrecios��x �
(��� �*
idEstablecimientoComencial��� �
)��� �
;��� �
if
�� 
(
�� ;
-parametroCentroDeAtencionParaObtencionPrecios
�� E
==
��F H
null
��I M
)
��M N
{
�� %
Parametro_actor_negocio
�� /
	parametro
��0 9
=
��: ;
new
��< ?%
Parametro_actor_negocio
��@ W
(
��W X(
idEstablecimientoComencial
��X r
,
��r s
MaestroSettings��t �
.��� �
Default��� �
.��� �O
?IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios��� �
,��� �)
idCentroDeAtencionPrecios��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
;��� �
result
�� 
=
��  
_actorRepositorio
��! 2
.
��2 3(
CrearParametroActorNegocio
��3 M
(
��M N
	parametro
��N W
)
��W X
;
��X Y
}
�� 
else
�� 
{
�� ;
-parametroCentroDeAtencionParaObtencionPrecios
�� E
.
��E F
valor
��F K
=
��L M'
idCentroDeAtencionPrecios
��N g
.
��g h
ToString
��h p
(
��p q
)
��q r
;
��r s
result
�� 
=
��  
_actorRepositorio
��! 2
.
��2 3-
ActualizarParametroActorNegocio
��3 R
(
��R S<
-parametroCentroDeAtencionParaObtencionPrecios��S �
)��� �
;��� �
}
�� 
}
�� 
if
�� 
(
�� %
idCentroDeAtencionStock
�� +
>
��, -
$num
��. /
)
��/ 0
{
�� %
Parametro_actor_negocio
�� +;
-parametroCentroDeAtencionParaObtencionDeStock
��, Y
=
��Z [)
_establecimientoRepositorio
��\ w
.
��w xC
4ObtenerParametroCentroDeAtencionParaObtencionDeStock��x �
(��� �*
idEstablecimientoComencial��� �
)��� �
;��� �
if
�� 
(
�� ;
-parametroCentroDeAtencionParaObtencionDeStock
�� E
==
��F H
null
��I M
)
��M N
{
�� %
Parametro_actor_negocio
�� /
	parametro
��0 9
=
��: ;
new
��< ?%
Parametro_actor_negocio
��@ W
(
��W X(
idEstablecimientoComencial
��X r
,
��r s
MaestroSettings��t �
.��� �
Default��� �
.��� �M
=IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock��� �
,��� �'
idCentroDeAtencionStock��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
;��� �
result
�� 
=
��  
_actorRepositorio
��! 2
.
��2 3(
CrearParametroActorNegocio
��3 M
(
��M N
	parametro
��N W
)
��W X
;
��X Y
}
�� 
else
�� 
{
�� ;
-parametroCentroDeAtencionParaObtencionDeStock
�� E
.
��E F
valor
��F K
=
��L M%
idCentroDeAtencionStock
��N e
.
��e f
ToString
��f n
(
��n o
)
��o p
;
��p q
result
�� 
=
��  
_actorRepositorio
��! 2
.
��2 3-
ActualizarParametroActorNegocio
��3 R
(
��R S<
-parametroCentroDeAtencionParaObtencionDeStock��S �
)��� �
;��� �
}
�� 
}
�� 
return
�� 
result
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
public
�� 
Actor
�� ,
CrearActorParaCentroDeAtencion
�� 3
(
��3 4
int
��4 7
idActor
��8 ?
,
��? @
string
��A G
nombre
��H N
)
��N O
{
�� 	
string
�� &
numeroDocumentoIdentidad
�� +
;
��+ ,
int
�� 

�� 
;
�� 
int
��  
idEstadoLegalActor
�� "
;
��" #
int
�� 
idClaseActor
�� 
;
�� &
EstablecimientoComercial
�� $
sede
��% )
=
��* +)
_establecimientoRepositorio
��, G
.
��G H-
ObtenerEstablecimientoComercial
��H g
(
��g h

��h u
.
��u v
Default
��v }
.
��} ~!
IdActorNegocioSede��~ �
)��� �
;��� �&
numeroDocumentoIdentidad
�� $
=
��% &
sede
��' +
.
��+ , 
DocumentoIdentidad
��, >
;
��> ?

�� 
=
�� 
sede
��  
.
��  !

��! .
;
��. / 
idEstadoLegalActor
�� 
=
��  
sede
��! %
.
��% &

��& 3
;
��3 4
idClaseActor
�� 
=
�� 
sede
�� 
.
��  
IdClaseActor
��  ,
;
��, -
Actor
�� 
actor
�� 
;
�� 
if
�� 
(
�� 
idActor
�� 
==
�� 
$num
�� 
)
�� 
{
�� 
actor
�� 
=
�� 
new
�� 
Actor
�� !
(
��! "
)
��" #
{
�� $
id_documento_identidad
�� *
=
��+ ,

��- :
.
��: ;
Default
��; B
.
��B C)
IdTipoDocumentoIdentidadRuc
��C ^
,
��^ _
fecha_nacimiento
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
,
��A B(
numero_documento_identidad
�� .
=
��/ 0&
numeroDocumentoIdentidad
��1 I
,
��I J

�� !
=
��" #
nombre
��$ *
,
��* +
segundo_nombre
�� "
=
��# $
$str
��% '
,
��' (
telefono
�� 
=
�� 
$str
�� !
,
��! "

�� !
=
��" #

��$ 1
,
��1 2
id_foto
�� 
=
�� 

�� +
.
��+ ,
Default
��, 3
.
��3 4#
IdFotoActorPorDefecto
��4 I
,
��I J
id_clase_actor
�� "
=
��# $
idClaseActor
��% 1
,
��1 2
id_estado_legal
�� #
=
��$ % 
idEstadoLegalActor
��& 8
,
��8 9
correo
�� 
=
�� 
$str
�� 
,
��  

�� !
=
��" #
$str
��$ &
,
��& '

pagina_web
�� 
=
��  
$str
��! #
,
��# $(
informacion_multiproposito
�� .
=
��/ 0
$str
��1 3
}
�� 
;
�� 
}
�� 
else
�� 
{
�� 
actor
�� 
=
�� 
new
�� 
Actor
�� !
(
��! "
)
��" #
{
�� 
id
�� 
=
�� 
idActor
��  
,
��  !$
id_documento_identidad
�� *
=
��+ ,

��- :
.
��: ;
Default
��; B
.
��B C)
IdTipoDocumentoIdentidadRuc
��C ^
,
��^ _
fecha_nacimiento
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
,
��A B(
numero_documento_identidad
�� .
=
��/ 0&
numeroDocumentoIdentidad
��1 I
,
��I J

�� !
=
��" #
nombre
��$ *
,
��* +
segundo_nombre
�� "
=
��# $
$str
��% '
,
��' (
telefono
�� 
=
�� 
$str
�� !
,
��! "

�� !
=
��" #

��$ 1
,
��1 2
id_foto
�� 
=
�� 

�� +
.
��+ ,
Default
��, 3
.
��3 4#
IdFotoActorPorDefecto
��4 I
,
��I J
id_clase_actor
�� "
=
��# $
idClaseActor
��% 1
,
��1 2
id_estado_legal
�� #
=
��$ % 
idEstadoLegalActor
��& 8
,
��8 9
correo
�� 
=
�� 
$str
�� 
,
��  

�� !
=
��" #
$str
��$ &
,
��& '

pagina_web
�� 
=
��  
$str
��! #
,
��# $(
informacion_multiproposito
�� .
=
��/ 0
$str
��1 3
}
�� 
;
�� 
}
�� 
return
�� 
actor
�� 
;
�� 
}
�� 	
public
�� 
OperationResult
�� #
CrearCentroDeAtencion
�� 4
(
��4 5
int
��5 8

idEmpleado
��9 C
,
��C D
string
��E K
codigo
��L R
,
��R S
string
��T Z
nombre
��[ a
,
��a b
bool
��c g"
salidaBienesSinStock
��h |
,
��| }
List��~ �
<��� �
int��� �
>��� �
idRoles��� �
,��� �
int��� �'
idCentroDeAtencionPadre��� �
)��� �
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
DateTime
�� 
fechaFin
�� !
=
��" #
fechaActual
��$ /
.
��/ 0
AddYears
��0 8
(
��8 9

��9 F
.
��F G
Default
��G N
.
��N OF
7vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna��O �
)��� �
;��� �

�� 
centroDeAtencion
�� .
=
��/ 0
new
��1 4

��5 B
(
��B C

��C P
.
��P Q
Default
��Q X
.
��X Y!
IdRolEntidadInterna
��Y l
,
��l m
fechaActual
��n y
,
��y z
fechaFin��{ �
,��� �
codigo��� �
,��� �
true��� �
,��� �'
idCentroDeAtencionPadre��� �
,��� �
false��� �
,��� �
$str��� �
)��� �
{
�� 
Actor
�� 
=
�� ,
CrearActorParaCentroDeAtencion
�� :
(
��: ;
$num
��; <
,
��< =
nombre
��> D
)
��D E
}
�� 
;
�� 
if
�� 
(
�� 
idRoles
�� 
!=
�� 
null
�� #
)
��# $
{
�� 
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (
idRoles
��) 0
)
��0 1
{
�� 
centroDeAtencion
�� (
.
��( )
Actor
��) .
.
��. /

��/ <
.
��< =
Add
��= @
(
��@ A
new
��A D

��E R
(
��R S
item
��S W
,
��W X
fechaActual
��Y d
,
��d e
fechaFin
��f n
,
��n o
$str
��p r
,
��r s
true
��t x
,
��x y&
idCentroDeAtencionPadre��z �
,��� �
false��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
centroDeAtencion
��  
.
��  !
extension_json
��! /
=
��0 1
idRoles
��2 9
.
��9 :
Contains
��: B
(
��B C

��C P
.
��P Q
Default
��Q X
.
��X Y
IdRolAlmacen
��Y e
)
��e f
?
��g h
$str��i �
+��� �$
salidaBienesSinStock��� �
.��� �
ToString��� �
(��� �
)��� �
.��� �
ToLower��� �
(��� �
)��� �
+��� �
$str��� �
:��� �
$str��� �
;��� �
OperationResult
�� 
result
��  &
=
��' (
_actorRepositorio
��) :
.
��: ;
CrearActorNegocio
��; L
(
��L M
centroDeAtencion
��M ]
)
��] ^
;
��^ _
OperationResult
�� '
resultadoInventarioActual
��  9
=
��: ;
null
��< @
;
��@ A
if
�� 
(
�� 
result
�� 
.
�� 
code_result
�� &
==
��' )!
OperationResultEnum
��* =
.
��= >
Success
��> E
&&
��F H
idRoles
��I P
.
��P Q
Contains
��Q Y
(
��Y Z

��Z g
.
��g h
Default
��h o
.
��o p
IdRolAlmacen
��p |
)
��| }
)
��} ~
{
�� 
try
�� 
{
�� '
resultadoInventarioActual
�� 1
=
��2 3&
_inventarioActual_Logica
��4 L
.
��L M#
CrearInventarioActual
��M b
(
��b c
(
��c d
int
��d g
)
��g h
result
��h n
.
��n o
data
��o s
,
��s t

idEmpleado
��u 
)�� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� $
ee
��% '
)
��' (
{
�� 
throw
�� 
new
�� !
LogicaException
��" 1
(
��1 2
$str��2 �
,��� �
ee��� �
)��� �
;��� �
}
�� 
}
�� 
return
�� '
resultadoInventarioActual
�� 0
??
��1 3
result
��4 :
;
��: ;
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
public
�� 
OperationResult
�� (
ActualizarCentroDeAtencion
�� 9
(
��9 :
int
��: =

idEmpleado
��> H
,
��H I
int
��J M
idActor
��N U
,
��U V
int
��W Z 
idCentroDeAtencion
��[ m
,
��m n
string
��o u
codigo
��v |
,
��| }
string��~ �
nombre��� �
,��� �
bool��� �$
salidaBienesSinStock��� �
,��� �
List��� �
<��� �
int��� �
>��� �
idRoles��� �
,��� �
int��� �'
idCentroDeAtencionPadre��� �
)��� �
{
�� 	
try
�� 
{
�� 
codigo
�� 
=
�� 
String
�� 
.
��  

��  -
(
��- .
codigo
��. 4
)
��4 5
?
��6 7
$str
��8 :
:
��; <
codigo
��= C
;
��C D
List
�� 
<
�� 
_Existencia
��  
>
��  !
existencias
��" -
=
��. /
new
��0 3
List
��4 8
<
��8 9
_Existencia
��9 D
>
��D E
(
��E F
)
��F G
;
��G H
List
�� 
<
�� !
Detalle_transaccion
�� (
>
��( )
detalles
��* 2
=
��3 4
new
��5 8
List
��9 =
<
��= >!
Detalle_transaccion
��> Q
>
��Q R
(
��R S
)
��S T
;
��T U
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
DateTime
�� 
fechaFin
�� !
=
��" #
fechaActual
��$ /
.
��/ 0
AddYears
��0 8
(
��8 9

��9 F
.
��F G
Default
��G N
.
��N OF
7vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna��O �
)��� �
;��� �
Actor
�� 
actor
�� 
=
�� ,
CrearActorParaCentroDeAtencion
�� <
(
��< =
idActor
��= D
,
��D E
nombre
��F L
)
��L M
;
��M N

�� 
centroDeAtencion
�� .
=
��/ 0
new
��1 4

��5 B
(
��B C 
idCentroDeAtencion
��C U
,
��U V
idActor
��W ^
,
��^ _

��` m
.
��m n
Default
��n u
.
��u v"
IdRolEntidadInterna��v �
,��� �
fechaActual��� �
,��� �
fechaFin��� �
,��� �
codigo��� �
,��� �
true��� �
,��� �
false��� �
,��� �
$str��� �
)��� �
{��� �
Actor��� �
=��� �
actor��� �
}��� �
;��� �
if
�� 
(
�� 
idRoles
�� 
!=
�� 
null
�� #
)
��# $
{
�� 
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (
idRoles
��) 0
)
��0 1
{
�� 
centroDeAtencion
�� (
.
��( )
Actor
��) .
.
��. /

��/ <
.
��< =
Add
��= @
(
��@ A
new
��A D

��E R
(
��R S
idActor
��S Z
,
��Z [
item
��\ `
,
��` a
fechaActual
��b m
,
��m n
fechaFin
��o w
,
��w x
$str
��y {
,
��{ |
true��} �
,��� �
false��� �
,��� �
$str��� �
,��� �'
idCentroDeAtencionPadre��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
centroDeAtencion
��  
.
��  !
extension_json
��! /
=
��0 1
idRoles
��2 9
.
��9 :
Contains
��: B
(
��B C

��C P
.
��P Q
Default
��Q X
.
��X Y
IdRolAlmacen
��Y e
)
��e f
?
��g h
$str��i �
+��� �$
salidaBienesSinStock��� �
.��� �
ToString��� �
(��� �
)��� �
.��� �
ToLower��� �
(��� �
)��� �
+��� �
$str��� �
:��� �
$str��� �
;��� �
OperationResult
�� 
result
��  &
=
��' (
_actorRepositorio
��) :
.
��: ;$
ActualizarActorNegocio
��; Q
(
��Q R
centroDeAtencion
��R b
)
��b c
;
��c d
OperationResult
�� $
resultInventarioActual
��  6
=
��7 8
null
��9 =
;
��= >
if
�� 
(
�� 
result
�� 
.
�� 
code_result
�� &
==
��' )!
OperationResultEnum
��* =
.
��= >
Success
��> E
&&
��F H
idRoles
��I P
.
��P Q
Contains
��Q Y
(
��Y Z

��Z g
.
��g h
Default
��h o
.
��o p
IdRolAlmacen
��p |
)
��| }
&&��~ �
!��� �,
_centroDeAtencionRepositorio��� �
.��� �%
TieneInventarioActual��� �
(��� �"
idCentroDeAtencion��� �
)��� �
)��� �
{
�� $
resultInventarioActual
�� *
=
��+ ,&
_inventarioActual_Logica
��- E
.
��E F#
CrearInventarioActual
��F [
(
��[ \
(
��\ ]
int
��] `
)
��` a
result
��a g
.
��g h
data
��h l
,
��l m

idEmpleado
��n x
)
��x y
;
��y z
}
�� 
return
�� $
resultInventarioActual
�� -
??
��. 0
result
��1 7
;
��7 8
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
}
�� 
}�� �
LD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Empleado\Empleado_Logica.cs
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
Logica

 
.

 
Core

 "
.

" #
Empleado

# +
{ 
public 

class 
Empleado_Logica  
:  !
IEmpleado_Logica" 2
{
private 
readonly !
IEmpleado_Repositorio . 
_empleadoRepositorio/ C
;C D
public 
Empleado_Logica 
( !
IEmpleado_Repositorio 4
empleadoRepositorio5 H
)H I
{ 	 
_empleadoRepositorio  
=! "
empleadoRepositorio# 6
;6 7
} 	
public 
int 
ObtenerIdEmpleado $
($ %
string% +
	idUsuario, 5
)5 6
{ 	
try 
{ 
return  
_empleadoRepositorio -
.- .
	ObtenerId. 7
(7 8
	idUsuario8 A
)A B
;B C
}D E
catchF K
(L M
	ExceptionM V
eW X
)X Y
{Z [
throw\ a
eb c
;c d
}e f
} 	
public 
string !
ObtenerNombreEmpleado +
(+ ,
string, 2
	idUsuario3 <
)< =
{ 	
try 
{ 
return  
_empleadoRepositorio -
.- .

(; <
	idUsuario< E
)E F
;F G
}H I
catchJ O
(P Q
	ExceptionQ Z
e[ \
)\ ]
{^ _
throw` e
ef g
;g h
}i j
} 	
public 
	Empleado_ )
ObtenerEmpleadoInclusiveRoles 6
(6 7
string7 =
	idUsuario> G
)G H
{ 	
return  
_empleadoRepositorio '
.' (
ObtenerEmpleado( 7
(7 8
	idUsuario8 A
)A B
;B C
} 	
public   
	Empleado_   )
ObtenerEmpleadoInclusiveRoles   6
(  6 7
int  7 :
id  ; =
)  = >
{!! 	
return""  
_empleadoRepositorio"" '
.""' (
ObtenerEmpleado""( 7
(""7 8
id""8 :
)"": ;
;""; <
}## 	
}%% 
}&& �%
RD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Finanza\AjusteTesoreria_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
	Tesoreria# ,
{ 
public 

class "
AjusteTesoreria_Logica '
:( )#
IAjusteTesoreria_Logica* A
{ 
	protected 
readonly .
"IActualizarTransaccion_Repositorio ='
_actualizarTransaccionDatos> Y
;Y Z
	protected 
readonly ,
 IConsultaTransaccion_Repositorio ;%
_consultaTransaccionDatos< U
;U V
public "
AjusteTesoreria_Logica %
(% &,
 IConsultaTransaccion_Repositorio& F$
consultaTransaccionDatosG _
,_ `/
"IActualizarTransaccion_Repositorio	a �(
actualizarTransaccionDatos
� �
)
� �
{ 	%
_consultaTransaccionDatos %
=& '$
consultaTransaccionDatos( @
;@ A'
_actualizarTransaccionDatos '
=( )&
actualizarTransaccionDatos* D
;D E
} 	
public%% 
OperationResult%% >
2CorregirTipoTransaccionPagoEnNotasDeCreditoYDebito%% Q
(%%Q R
)%%R S
{&& 	
try'' 
{(( 
var++  
transaccionesErradas++ (
=++( )%
_consultaTransaccionDatos++* C
.++C DL
?ObtenerTransaccionesSegunTipoYConTipoTransaccionPadreDiferenteA	++D �
(
++� �!
TransaccionSettings
++� �
.
++� �
Default
++� �
.
++� �7
)IdTipoTransaccionCobranzaFacturasClientes
++� �
,
++� �!
TransaccionSettings
++� �
.
++� �
Default
++� �
.
++� �$
IdTipoTransaccionVenta
++� �
)
++� �
.
++� �
ToList
++� �
(
++� �
)
++� �
;
++� �
var,, 
transaccionesPadre,, &
=,,' (%
_consultaTransaccionDatos,,) B
.,,B C%
ObtenerTransaccionesPadre,,C \
(,,\ ] 
transaccionesErradas,,] q
.,,q r
Select,,r x
(,,x y
t,,y z
=>,,z |
t,,} ~
.,,~ 
id	,, �
)
,,� �
.
,,� �
ToArray
,,� �
(
,,� �
)
,,� �
)
,,� �
.
,,� �
ToList
,,� �
(
,,� �
)
,,� �
;
,,� �
foreach.. 
(.. 
var.. 
transaccionErrada.. .
in../ 1 
transaccionesErradas..2 F
)..F G
{// 
var00 "
idTipoTransaccionPadre00 .
=00/ 0
transaccionesPadre001 C
.00C D
FirstOrDefault00D R
(00R S
t00S T
=>00T V
t00W X
.00X Y
id00Y [
==00\ ^
transaccionErrada00_ p
.00p q!
id_transaccion_padre	00q �
)
00� �
.
00� �!
id_tipo_transaccion
00� �
;
00� �
var11 "
idTipoTransaccionOrden11 .
=11/ 0
Diccionario111 <
.11< =
MapeoWraperVsOrden11= O
.11O P
Single11P V
(11V W
t11W X
=>11Y [
t11\ ]
.11] ^
Key11^ a
==11b d"
idTipoTransaccionPadre11e {
)11{ |
.11| }
Value	11} �
;
11� �
var22 &
idTipoTransaccionTesoreria22 2
=223 4
Diccionario225 @
.22@ A+
MapeoOrdenVsMovimientoEconomico22A `
.22` a
Single22a g
(22g h
t22h i
=>22j l
t22m n
.22n o
Key22o r
==22s u#
idTipoTransaccionOrden	22v �
)
22� �
.
22� �
Value
22� �
;
22� �
transaccionErrada44 %
.44% &
id_tipo_transaccion44& 9
=44: ;&
idTipoTransaccionTesoreria44< V
;44V W
}55 
return66 '
_actualizarTransaccionDatos66 2
.662 3#
ActualizarTransacciones663 J
(66J K 
transaccionesErradas66K _
.66_ `
ToList66` f
(66f g
)66g h
)66h i
;66i j
}88 
catch99 
(99 
	Exception99 
e99 
)99 
{:: 
throw;; 
new;; 
LogicaException;; )
(;;) *
$str;;* R
+;;S T
e;;U V
.;;V W
Message;;W ^
,;;^ _
e;;` a
);;a b
;;;b c
}<< 
}== 	
}AA 
}BB �
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Finanza\ArqueoCaja_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Finanza# *
{ 
public 

class 
ArqueoCaja_Logica "
:# $
IArqueoCaja_Logica% 7
{ 
	protected 
readonly 
IActorNegocioLogica .
_actorNegocioLogica/ B
;B C
	protected 
readonly 
IOperacionLogica +
_operacionLogica, <
;< =
	protected 
readonly 
IMaestroRepositorio .

;< =
	protected 
readonly '
IFinanzaReporte_Repositorio 6"
_finanzaReportingDatos7 M
;M N
	protected 
readonly #
ITransaccionRepositorio 2
_transaccionDatos3 D
;D E
public 
ArqueoCaja_Logica  
(  !
IActorNegocioLogica! 4
actorNegocioLogica5 G
,G H
IMaestroRepositorioI \
maestroDatos] i
,i j(
IFinanzaReporte_Repositorio	k �#
finanzaReportingDatos
� �
,
� �
IOperacionLogica
� �
operacionLogica
� �
,
� �%
ITransaccionRepositorio
� �
transaccionDatos
� �
)
� �
{ 	
_actorNegocioLogica   
=    !
actorNegocioLogica  " 4
;  4 5

=!! 
maestroDatos!! (
;!!( )"
_finanzaReportingDatos"" "
=""# $!
finanzaReportingDatos""% :
;"": ;
_operacionLogica## 
=## 
operacionLogica## .
;##. /
_transaccionDatos$$ 
=$$ 
transaccionDatos$$  0
;$$0 1
}%% 	
}'' 
}(( ��
QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Finanza\FinanzaReporte_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Finanza# *
{ 
public 

class !
FinanzaReporte_Logica &
:' ("
IFinanzaReporte_Logica) ?
{ 
	protected 
readonly 
IActorNegocioLogica .
_actorNegocioLogica/ B
;B C
	protected 
readonly 
IOperacionLogica +
_operacionLogica, <
;< =
	protected 
readonly 
IMaestroRepositorio .

;< =
	protected 
readonly '
IFinanzaReporte_Repositorio 6"
_finanzaReportingDatos7 M
;M N
	protected 
readonly #
ITransaccionRepositorio 2
_transaccionDatos3 D
;D E
	protected 
readonly #
IEstablecimiento_Logica 2"
_establecimientoLogica3 I
;I J
public"" !
FinanzaReporte_Logica"" $
(""$ %
IActorNegocioLogica""% 8
actorNegocioLogica""9 K
,""K L
IMaestroRepositorio""M `
maestroDatos""a m
,""m n(
IFinanzaReporte_Repositorio	""o �#
finanzaReportingDatos
""� �
,
""� �
IOperacionLogica
""� �
operacionLogica
""� �
,
""� �%
ITransaccionRepositorio
""� �
transaccionDatos
""� �
,
""� �%
IEstablecimiento_Logica
""� �#
establecimientoLogica
""� �
)
""� �
{## 	
_actorNegocioLogica$$ 
=$$  !
actorNegocioLogica$$" 4
;$$4 5

=%% 
maestroDatos%% (
;%%( )"
_finanzaReportingDatos&& "
=&&# $!
finanzaReportingDatos&&% :
;&&: ;
_operacionLogica'' 
='' 
operacionLogica'' .
;''. /
_transaccionDatos(( 
=(( 
transaccionDatos((  0
;((0 1"
_establecimientoLogica)) "
=))# $!
establecimientoLogica))% :
;)): ;
}** 	
public++ 
PrincipalReportData++ ",
 ObtenerDatosParaReportePrincipal++# C
(++C D"
UserProfileSessionData++D Z
profileData++[ f
)++f g
{,, 	
var-- .
"establecimientosConSusCajasCuentas-- 2
=--3 4
new--5 8
List--9 =
<--= >
Establecimiento--> M
>--M N
(--N O
)--O P
;--P Q
var.. !
establecimientoSesion.. %
=..& '
profileData..( 3
...3 40
$EstablecimientoComercialSeleccionado..4 X
...X Y
ToEstablecimiento..Y j
(..j k
)..k l
;..l m
var// *
TieneRolAdministradorDeNegocio// .
=/// 0
profileData//1 <
.//< =
Empleado//= E
.//E F
TieneRol//F N
(//N O

.//\ ]
Default//] d
.//d e(
idRolAdministradorDeNegocio	//e �
)
//� �
;
//� �
var00 

CajaSesion00 
=00 
profileData00 (
.00( )(
CentroDeAtencionSeleccionado00) E
.00E F
ToItemGenerico00F T
(00T U
)00U V
;00V W
if22 
(22 
!22 *
TieneRolAdministradorDeNegocio22 /
)22/ 0
{33 
establecimientoSesion44 %
.44% &
CentrosAtencion44& 5
=446 7
new448 ;
List44< @
<44@ A
ItemGenerico44A M
>44M N
{44O P

CajaSesion44Q [
}44\ ]
;44] ^
}55 
if77 
(77 *
TieneRolAdministradorDeNegocio77 .
)77. /
{88 
"establecimientosConSusCajasCuentas99 2
=993 4
Establecimiento995 D
.99D E
Convert99E L
(99L M"
_establecimientoLogica99M c
.99c dB
5ObtenerEstablecimientosComercialesVigentesConSusCajas	99d �
(
99� �
)
99� �
)
99� �
;
99� �.
"establecimientosConSusCajasCuentas:: 2
.::2 3
ForEach::3 :
(::: ;
ec::; =
=>::> @
ec::A C
.::C D
CentrosAtencion::D S
.::S T
ForEach::T [
(::[ \
ca::\ ^
=>::_ a
ca::b d
.::d e
Valor::e j
=::k l
$str::m t
)::t u
)::u v
;::v w
var;; 
cuentasBancarias;; $
=;;% &
_operacionLogica;;' 7
.;;7 8@
4ObtenerCuentasBancariasConEntidadFinancieraConMoneda;;8 l
(;;l m
);;m n
;;;n o
cuentasBancarias<<  
.<<  !
ForEach<<! (
(<<( )
cb<<) +
=><<, .
cb<</ 1
.<<1 2
Valor<<2 7
=<<8 9
$str<<: @
)<<@ A
;<<A B.
"establecimientosConSusCajasCuentas== 2
.==2 3
ForEach==3 :
(==: ;
e==; <
=>=== ?
e==@ A
.==A B
CentrosAtencion==B Q
.==Q R
AddRange==R Z
(==Z [
cuentasBancarias==[ k
)==k l
)==l m
;==m n
}>> 
var?? 

mediosPago?? 
=?? 

.??* +-
!ObtenerDetallesComoItemsGenericos??+ L
(??L M
MaestroSettings??M \
.??\ ]
Default??] d
.??d e 
IdMaestroMedioDePago??e y
)??y z
.??z {
ToList	??{ �
(
??� �
)
??� �
;
??� �
var@@ 
data@@ 
=@@ 
new@@ 
PrincipalReportData@@ .
(@@. /
)@@/ 0
{AA 
FechaActual_BB 
=BB 
DateTimeUtilBB +
.BB+ ,
FechaActualBB, 7
(BB7 8
)BB8 9
,BB9 :!
EstablecimientoSesionCC %
=CC& '!
establecimientoSesionCC( =
,CC= >
EsAdministradorDD 
=DD  !*
TieneRolAdministradorDeNegocioDD" @
,DD@ A
EstablecimientosEE  
=EE! "*
TieneRolAdministradorDeNegocioEE# A
?EEB C.
"establecimientosConSusCajasCuentasEED f
:EEg h
newEEi l
ListEEm q
<EEq r
Establecimiento	EEr �
>
EE� �
(
EE� �
)
EE� �
{
EE� �#
establecimientoSesion
EE� �
}
EE� �
,
EE� �
CajasFF 
=FF *
TieneRolAdministradorDeNegocioFF 6
?FF7 8.
"establecimientosConSusCajasCuentasFF9 [
.FF[ \

SelectManyFF\ f
(FFf g
eFFg h
=>FFi k
eFFl m
.FFm n
CentrosAtencionFFn }
)FF} ~
.FF~ 
Where	FF �
(
FF� �
ca
FF� �
=>
FF� �
ca
FF� �
.
FF� �
Valor
FF� �
==
FF� �
$str
FF� �
)
FF� �
.
FF� �
ToList
FF� �
(
FF� �
)
FF� �
:
FF� �
new
FF� �
List
FF� �
<
FF� �
ItemGenerico
FF� �
>
FF� �
(
FF� �
)
FF� �
{
FF� �
profileData
FF� �
.
FF� �*
CentroDeAtencionSeleccionado
FF� �
.
FF� �
ToItemGenerico
FF� �
(
FF� �
)
FF� �
}
FF� �
,
FF� �

MediosPagoGG 
=GG 

mediosPagoGG '
,GG' (
MediosPagoCuentaHH  
=HH! "

mediosPagoHH# -
.HH- .
WhereHH. 3
(HH3 4
mHH4 5
=>HH6 8
mHH9 :
.HH: ;
IdHH; =
==HH> @
MaestroSettingsHHA P
.HHP Q
DefaultHHQ X
.HHX Y=
0IdDetalleMaestroMedioDepagoTransferenciaDeFondos	HHY �
||
HH� �
m
HH� �
.
HH� �
Id
HH� �
==
HH� �
MaestroSettings
HH� �
.
HH� �
Default
HH� �
.
HH� �9
+IdDetalleMaestroMedioDepagoDepositoEnCuenta
HH� �
)
HH� �
.
HH� �
ToList
HH� �
(
HH� �
)
HH� �
,
HH� �
OperacionesIngresosII #
=II$ %
_transaccionDatosII& 7
.II7 86
*ObtenerTipoDeTransaccionPorAccionDeNegocioII8 b
(IIb c
TransaccionSettingsIIc v
.IIv w
DefaultIIw ~
.II~ .
!IdAccionDeNegocioMovimientoEnCaja	II �
,
II� �
true
II� �
)
II� �
.
II� �
ToList
II� �
(
II� �
)
II� �
,
II� �
OperacionesEgresosJJ "
=JJ# $
_transaccionDatosJJ% 6
.JJ6 76
*ObtenerTipoDeTransaccionPorAccionDeNegocioJJ7 a
(JJa b
TransaccionSettingsJJb u
.JJu v
DefaultJJv }
.JJ} ~.
!IdAccionDeNegocioMovimientoEnCaja	JJ~ �
,
JJ� �
false
JJ� �
)
JJ� �
.
JJ� �
ToList
JJ� �
(
JJ� �
)
JJ� �
}LL 
;LL
returnMM 
dataMM 
;MM 
}NN 	
publicPP 
ListPP 
<PP 

>PP! "
IngresosPP# +
(PP+ ,
boolPP, 0
esCuentaPP1 9
,PP9 :
intPP; >
idCajaCuentaPP? K
,PPK L
DateTimePPM U

fechaDesdePPV `
,PP` a
DateTimePPb j

fechaHastaPPk u
,PPu v
boolPPw {
todosLosMediosPago	PP| �
,
PP� �
int
PP� �
[
PP� �
]
PP� �

mediosPago
PP� �
,
PP� �
bool
PP� �!
todasLasOperaciones
PP� �
,
PP� �
int
PP� �
[
PP� �
]
PP� �
operaciones
PP� �
)
PP� �
{QQ 	
tryRR 
{SS 
ListTT 
<TT 

>TT" #
ingresosTT$ ,
;TT, -
ifUU 
(UU 
esCuentaUU 
)UU 
{VV 
ingresosWW 
=WW 
(WW  
todosLosMediosPagoWW  2
?WW3 4
(WW5 6
todasLasOperacionesWW6 I
?WWJ K"
_finanzaReportingDatosWWL b
.WWb c3
&ObtenerIngresosEgresosEnCuentaBancaria	WWc �
(
WW� �
idCajaCuenta
WW� �
,
WW� �

fechaDesde
WW� �
,
WW� �

fechaHasta
WW� �
,
WW� �
true
WW� �
)
WW� �
.
WW� �
ToList
WW� �
(
WW� �
)
WW� �
:
WW� �$
_finanzaReportingDatos
WW� �
.
WW� �B
4ObtenerIngresosEgresosEnCuentaBancariaPorOperaciones
WW� �
(
WW� �
idCajaCuenta
WW� �
,
WW� �

fechaDesde
WW� �
,
WW� �

fechaHasta
WW� �
,
WW� �
operaciones
WW� �
)
WW� �
.
WW� �
ToList
WW� �
(
WW� �
)
WW� �
)
WW� �
:
WW� �
(
WW� �!
todasLasOperaciones
WW� �
?
WW� �$
_finanzaReportingDatos
WW� �
.
WW� �A
3ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago
WW� �
(
WW� �
idCajaCuenta
WW� �
,
WW� �

fechaDesde
WW� �
,
WW� �

fechaHasta
WW� �
,
WW� �
true
WW� �
,
WW� �

mediosPago
WW� �
)
WW� �
.
WW� �
ToList
WW� �
(
WW� �
)
WW� �
:
WW� �$
_finanzaReportingDatos
WW� �
.
WW� �M
?ObtenerIngresosEgresosEnCuentaBancariaPorOperacionesYMediosPago
WW� �
(
WW� �
idCajaCuenta
WW� �
,
WW� �

fechaDesde
WW� �
,
WW� �

fechaHasta
WW� �
,
WW� �
operaciones
WW� �
,
WW� �

mediosPago
WW� �
)
WW� �
.
WW� �
ToList
WW� �
(
WW� �
)
WW� �
)
WW� �
)
WW� �
;
WW� �
}XX 
elseYY 
{ZZ 
ingresos[[ 
=[[ 
([[  
todosLosMediosPago[[  2
?[[3 4
([[5 6
todasLasOperaciones[[6 I
?[[J K"
_finanzaReportingDatos[[L b
.[[b c"
ObtenerIngresosEgresos[[c y
([[y z
idCajaCuenta	[[z �
,
[[� �

fechaDesde
[[� �
,
[[� �

fechaHasta
[[� �
,
[[� �
true
[[� �
)
[[� �
.
[[� �
ToList
[[� �
(
[[� �
)
[[� �
:
[[� �$
_finanzaReportingDatos
[[� �
.
[[� �2
$ObtenerIngresosEgresosPorOperaciones
[[� �
(
[[� �
idCajaCuenta
[[� �
,
[[� �

fechaDesde
[[� �
,
[[� �

fechaHasta
[[� �
,
[[� �
operaciones
[[� �
)
[[� �
.
[[� �
ToList
[[� �
(
[[� �
)
[[� �
)
[[� �
:
[[� �
(
[[� �!
todasLasOperaciones
[[� �
?
[[� �$
_finanzaReportingDatos
[[� �
.
[[� �1
#ObtenerIngresosEgresosPorMediosPago
[[� �
(
[[� �
idCajaCuenta
[[� �
,
[[� �

fechaDesde
[[� �
,
[[� �

fechaHasta
[[� �
,
[[� �
true
[[� �
,
[[� �

mediosPago
[[� �
)
[[� �
.
[[� �
ToList
[[� �
(
[[� �
)
[[� �
:
[[� �$
_finanzaReportingDatos
[[� �
.
[[� �=
/ObtenerIngresosEgresosPorOperacionesYMediosPago
[[� �
(
[[� �
idCajaCuenta
[[� �
,
[[� �

fechaDesde
[[� �
,
[[� �

fechaHasta
[[� �
,
[[� �
operaciones
[[� �
,
[[� �

mediosPago
[[� �
)
[[� �
.
[[� �
ToList
[[� �
(
[[� �
)
[[� �
)
[[� �
)
[[� �
;
[[� �
}\\ 
ingresos]] 
=]] 
ingresos]] #
.]]# $
OrderBy]]$ +
(]]+ ,
m]], -
=>]]. 0
m]]1 2
.]]2 3
Fecha]]3 8
)]]8 9
.]]9 :
ToList]]: @
(]]@ A
)]]A B
;]]B C
return^^ 
ingresos^^ 
;^^  
}__ 
catch`` 
(`` 
	Exception`` 
e`` 
)`` 
{aa 
throwbb 
newbb 
LogicaExceptionbb )
(bb) *
$strbb* N
,bbN O
ebbP Q
)bbQ R
;bbR S
}cc 
}dd 	
publicff 
Listff 
<ff 

>ff! "
Egresosff# *
(ff* +
boolff+ /
esCuentaff0 8
,ff8 9
intff: =
idCajaCuentaff> J
,ffJ K
DateTimeffL T

fechaDesdeffU _
,ff_ `
DateTimeffa i

fechaHastaffj t
,fft u
boolffv z
todosLosMediosPago	ff{ �
,
ff� �
int
ff� �
[
ff� �
]
ff� �

mediosPago
ff� �
,
ff� �
bool
ff� �!
todasLasOperaciones
ff� �
,
ff� �
int
ff� �
[
ff� �
]
ff� �
operaciones
ff� �
)
ff� �
{gg 	
tryhh 
{ii 
Listjj 
<jj 

>jj" #
egresosjj$ +
;jj+ ,
ifkk 
(kk 
esCuentakk 
)kk 
{ll 
egresosmm 
=mm 
(mm 
todosLosMediosPagomm 1
?mm2 3
(mm4 5
todasLasOperacionesmm5 H
?mmI J"
_finanzaReportingDatosmmK a
.mma b3
&ObtenerIngresosEgresosEnCuentaBancaria	mmb �
(
mm� �
idCajaCuenta
mm� �
,
mm� �

fechaDesde
mm� �
,
mm� �

fechaHasta
mm� �
,
mm� �
false
mm� �
)
mm� �
.
mm� �
ToList
mm� �
(
mm� �
)
mm� �
:
mm� �$
_finanzaReportingDatos
mm� �
.
mm� �B
4ObtenerIngresosEgresosEnCuentaBancariaPorOperaciones
mm� �
(
mm� �
idCajaCuenta
mm� �
,
mm� �

fechaDesde
mm� �
,
mm� �

fechaHasta
mm� �
,
mm� �
operaciones
mm� �
)
mm� �
.
mm� �
ToList
mm� �
(
mm� �
)
mm� �
)
mm� �
:
mm� �
(
mm� �!
todasLasOperaciones
mm� �
?
mm� �$
_finanzaReportingDatos
mm� �
.
mm� �A
3ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago
mm� �
(
mm� �
idCajaCuenta
mm� �
,
mm� �

fechaDesde
mm� �
,
mm� �

fechaHasta
mm� �
,
mm� �
false
mm� �
,
mm� �

mediosPago
mm� �
)
mm� �
.
mm� �
ToList
mm� �
(
mm� �
)
mm� �
:
mm� �$
_finanzaReportingDatos
mm� �
.
mm� �M
?ObtenerIngresosEgresosEnCuentaBancariaPorOperacionesYMediosPago
mm� �
(
mm� �
idCajaCuenta
mm� �
,
mm� �

fechaDesde
mm� �
,
mm� �

fechaHasta
mm� �
,
mm� �
operaciones
mm� �
,
mm� �

mediosPago
mm� �
)
mm� �
.
mm� �
ToList
mm� �
(
mm� �
)
mm� �
)
mm� �
)
mm� �
;
mm� �
}nn 
elseoo 
{pp 
egresosqq 
=qq 
(qq 
todosLosMediosPagoqq 1
?qq2 3
(qq4 5
todasLasOperacionesqq5 H
?qqI J"
_finanzaReportingDatosqqK a
.qqa b"
ObtenerIngresosEgresosqqb x
(qqx y
idCajaCuenta	qqy �
,
qq� �

fechaDesde
qq� �
,
qq� �

fechaHasta
qq� �
,
qq� �
false
qq� �
)
qq� �
.
qq� �
ToList
qq� �
(
qq� �
)
qq� �
:
qq� �$
_finanzaReportingDatos
qq� �
.
qq� �2
$ObtenerIngresosEgresosPorOperaciones
qq� �
(
qq� �
idCajaCuenta
qq� �
,
qq� �

fechaDesde
qq� �
,
qq� �

fechaHasta
qq� �
,
qq� �
operaciones
qq� �
)
qq� �
.
qq� �
ToList
qq� �
(
qq� �
)
qq� �
)
qq� �
:
qq� �
(
qq� �!
todasLasOperaciones
qq� �
?
qq� �$
_finanzaReportingDatos
qq� �
.
qq� �1
#ObtenerIngresosEgresosPorMediosPago
qq� �
(
qq� �
idCajaCuenta
qq� �
,
qq� �

fechaDesde
qq� �
,
qq� �

fechaHasta
qq� �
,
qq� �
false
qq� �
,
qq� �

mediosPago
qq� �
)
qq� �
.
qq� �
ToList
qq� �
(
qq� �
)
qq� �
:
qq� �$
_finanzaReportingDatos
qq� �
.
qq� �=
/ObtenerIngresosEgresosPorOperacionesYMediosPago
qq� �
(
qq� �
idCajaCuenta
qq� �
,
qq� �

fechaDesde
qq� �
,
qq� �

fechaHasta
qq� �
,
qq� �
operaciones
qq� �
,
qq� �

mediosPago
qq� �
)
qq� �
.
qq� �
ToList
qq� �
(
qq� �
)
qq� �
)
qq� �
)
qq� �
;
qq� �
}rr 
egresosss 
=ss 
egresosss !
.ss! "
OrderByss" )
(ss) *
mss* +
=>ss, .
mss/ 0
.ss0 1
Fechass1 6
)ss6 7
.ss7 8
ToListss8 >
(ss> ?
)ss? @
;ss@ A
returntt 
egresostt 
;tt 
}uu 
catchvv 
(vv 
	Exceptionvv 
evv 
)vv 
{ww 
throwxx 
newxx 
LogicaExceptionxx )
(xx) *
$strxx* M
,xxM N
exxO P
)xxP Q
;xxQ R
}yy 
}zz 	
public|| 
decimal|| 
ObtenerSaldo|| #
(||# $
int||$ '
idCajaCuenta||( 4
,||4 5
DateTime||6 >
fecha||? D
,||D E
bool||F J
esCuenta||K S
)||S T
{}} 	
var~~ 
ultimoArqueoDeCaja~~ "
=~~# $
_transaccionDatos~~% 6
.~~6 7+
ObtenerUltimaTransaccionAntesDe~~7 V
(~~V W
idCajaCuenta~~W c
,~~c d
TransaccionSettings~~e x
.~~x y
Default	~~y �
.
~~� �)
IdTipoTransaccionArqueoCaja
~~� �
,
~~� �
fecha
~~� �
)
~~� �
;
~~� �
var #
hayArqueoDeCajaAnterior '
=( )
ultimoArqueoDeCaja* <
!== ?
null@ D
;D E
DateTime
�� %
fechaPrimeraTransaccion
�� ,
=
��- .
(
��/ 0
DateTime
��0 8
)
��8 9
_transaccionDatos
��9 J
.
��J K,
ObtenerFechaPrimeraTransaccion
��K i
(
��i j
)
��j k
;
��k l
var
�� (
fechaDesdeParaSaldoInicial
�� *
=
��+ ,%
hayArqueoDeCajaAnterior
��- D
?
��E F 
ultimoArqueoDeCaja
��G Y
.
��Y Z
fecha_inicio
��Z f
:
��g h&
fechaPrimeraTransaccion��i �
;��� �
var
�� (
fechaHastaParaSaldoInicial
�� *
=
��+ ,
fecha
��- 2
;
��2 3
var
�� )
movimientosParaSaldoInicial
�� +
=
��, -
esCuenta
��. 6
?
��7 8$
_finanzaReportingDatos
��9 O
.
��O P4
&ObtenerIngresosEgresosEnCuentaBancaria
��P v
(
��v w
idCajaCuenta��w �
,��� �*
fechaDesdeParaSaldoInicial��� �
,��� �*
fechaHastaParaSaldoInicial��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �&
_finanzaReportingDatos��� �
.��� �&
ObtenerIngresosEgresos��� �
(��� �
idCajaCuenta��� �
,��� �*
fechaDesdeParaSaldoInicial��� �
,��� �*
fechaHastaParaSaldoInicial��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
var
�� 
saldoInicial
�� 
=
�� 
(
��  %
hayArqueoDeCajaAnterior
��  7
?
��8 9 
ultimoArqueoDeCaja
��: L
.
��L M

��M Z
:
��[ \
$num
��] ^
)
��^ _
+
��` a
(
��b c)
movimientosParaSaldoInicial
��c ~
.
��~ 
Count�� �
>��� �
$num��� �
?��� �+
movimientosParaSaldoInicial��� �
.��� �
Where��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
	EsIngreso��� �
)��� �
.��� �
Sum��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Importe��� �
)��� �
-��� �+
movimientosParaSaldoInicial��� �
.��� �
Where��� �
(��� �
m��� �
=>��� �
!��� �
m��� �
.��� �
	EsIngreso��� �
)��� �
.��� �
Sum��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Importe��� �
)��� �
:��� �
$num��� �
)��� �
;��� �
return
�� 
saldoInicial
�� 
;
��  
}
�� 	
public
��  
FlujoIngresoEgreso
�� !
Flujo
��" '
(
��' (
bool
��( ,
esCuenta
��- 5
,
��5 6
int
��7 :
idCajaCuenta
��; G
,
��G H
DateTime
��I Q

fechaDesde
��R \
,
��\ ]
DateTime
��^ f

fechaHasta
��g q
,
��q r
bool
��s w!
todosLosMediosPago��x �
,��� �
int��� �
[��� �
]��� �

mediosPago��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
saldoInicial
��  
=
��! "
ObtenerSaldo
��# /
(
��/ 0
idCajaCuenta
��0 <
,
��< =

fechaDesde
��> H
,
��H I
esCuenta
��J R
)
��R S
;
��S T
var
�� 

�� !
=
��" #
esCuenta
��$ ,
?
��- .
(
��/ 0 
todosLosMediosPago
��0 B
?
��C D$
_finanzaReportingDatos
��E [
.
��[ \5
&ObtenerIngresosEgresosEnCuentaBancaria��\ �
(��� �
idCajaCuenta��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
.��� �
OrderBy��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Fecha��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �&
_finanzaReportingDatos��� �
.��� �C
3ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago��� �
(��� �
idCajaCuenta��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
,��� �

mediosPago��� �
)��� �
.��� �
OrderBy��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Fecha��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
:��� �
(��� �"
todosLosMediosPago��� �
?��� �&
_finanzaReportingDatos��� �
.��� �&
ObtenerIngresosEgresos��� �
(��� �
idCajaCuenta��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
.��� �
OrderBy��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Fecha��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
:��� �&
_finanzaReportingDatos��� �
.��� �3
#ObtenerIngresosEgresosPorMediosPago��� �
(��� �
idCajaCuenta��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
,��� �

mediosPago��� �
)��� �
.��� �
OrderBy��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Fecha��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
;��� �
var
�� 
saldo
�� 
=
�� 
saldoInicial
�� (
;
��( )
foreach
�� 
(
�� 
var
�� 
detalleFlujo
�� )
in
��* ,

��- :
)
��: ;
{
�� 
saldo
�� 
+=
�� 
(
�� 
detalleFlujo
�� *
.
��* +
	EsIngreso
��+ 4
?
��5 6
detalleFlujo
��7 C
.
��C D
Importe
��D K
:
��L M
(
��N O
-
��O P
$num
��P Q
*
��R S
detalleFlujo
��T `
.
��` a
Importe
��a h
)
��h i
)
��i j
;
��j k
detalleFlujo
��  
.
��  !
Saldo
��! &
=
��' (
saldo
��) .
;
��. /
}
�� 
var
�� 

saldoFinal
�� 
=
��  
saldo
��! &
;
��& '
return
�� 
new
��  
FlujoIngresoEgreso
�� -
(
��- .
)
��. /
{
�� 
Resumen
�� 
=
�� 
new
�� !
ResumenFlujo
��" .
(
��. /
)
��/ 0
{
�� 
SaldoInicial
�� $
=
��% &
saldoInicial
��' 3
,
��3 4
Ingresos
��  
=
��! "

��# 0
.
��0 1
Where
��1 6
(
��6 7
df
��7 9
=>
��: <
df
��= ?
.
��? @
	EsIngreso
��@ I
)
��I J
.
��J K
Sum
��K N
(
��N O
df
��O Q
=>
��R T
df
��U W
.
��W X
Importe
��X _
)
��_ `
,
��` a
Egresos
�� 
=
��  !

��" /
.
��/ 0
Where
��0 5
(
��5 6
df
��6 8
=>
��9 ;
!
��< =
df
��= ?
.
��? @
	EsIngreso
��@ I
)
��I J
.
��J K
Sum
��K N
(
��N O
df
��O Q
=>
��R T
df
��U W
.
��W X
Importe
��X _
)
��_ `
,
��` a

SaldoFinal
�� "
=
��# $

saldoFinal
��% /
}
�� 
,
�� 
Detalles
�� 
=
�� 

�� ,
}
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* K
,
��K L
e
��M N
)
��N O
;
��O P
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
OperacionGrupo
�� "
>
��" #+
ObtenerCuentasPorCobrarGrupos
��$ A
(
��A B
bool
��B F
todosLosGrupos
��G U
,
��U V
int
��W Z
[
��Z [
]
��[ \
	idsGrupos
��] f
)
��f g
{
�� 	
try
�� 
{
�� 
	idsGrupos
�� 
=
�� 
todosLosGrupos
�� *
?
��+ ,!
_actorNegocioLogica
��- @
.
��@ A-
ObtenerGruposActoresComerciales
��A `
(
��` a
)
��a b
.
��b c
Select
��c i
(
��i j
g
��j k
=>
��l n
g
��o p
.
��p q
Id
��q s
)
��s t
.
��t u
ToArray
��u |
(
��| }
)
��} ~
:�� �
	idsGrupos��� �
;��� �
List
�� 
<
�� 
OperacionGrupo
�� #
>
��# $
porCobrarGrupos
��% 4
=
��5 6$
_finanzaReportingDatos
��7 M
.
��M N*
ObtenerCuentasPorCobarGrupos
��N j
(
��j k
	idsGrupos
��k t
)
��t u
.
��u v
ToList
��v |
(
��| }
)
��} ~
;
��~ 
var
�� #
gruposPorCobrarGrupos
�� )
=
��* +
porCobrarGrupos
��, ;
.
��; <
GroupBy
��< C
(
��C D
g
��D E
=>
��F H
new
��I L
{
��M N
g
��O P
.
��P Q
IdGrupo
��Q X
,
��X Y
g
��Z [
.
��[ \
NombreGrupo
��\ g
,
��g h
g
��i j
.
��j k"
DocumentoResponsable
��k 
,�� �
g��� �
.��� �!
NombreResponsable��� �
,��� �
g��� �
.��� �
	IdCliente��� �
,��� �
g��� �
.��� �

}��� �
)��� �
;��� �
var
�� 
	resultado
�� 
=
�� #
gruposPorCobrarGrupos
��  5
.
��5 6
Select
��6 <
(
��< =
g
��= >
=>
��? A
new
��B E
OperacionGrupo
��F T
(
��T U
)
��U V
{
�� 
IdGrupo
�� 
=
�� 
g
�� 
.
��  
Key
��  #
.
��# $
IdGrupo
��$ +
,
��+ ,
NombreGrupo
�� 
=
��  !
g
��" #
.
��# $
Key
��$ '
.
��' (
NombreGrupo
��( 3
,
��3 4"
DocumentoResponsable
�� (
=
��) *
g
��+ ,
.
��, -
Key
��- 0
.
��0 1"
DocumentoResponsable
��1 E
,
��E F
NombreResponsable
�� %
=
��& '
g
��( )
.
��) *
Key
��* -
.
��- .
NombreResponsable
��. ?
,
��? @
	IdCliente
�� 
=
�� 
g
��  !
.
��! "
Key
��" %
.
��% &
	IdCliente
��& /
,
��/ 0

�� !
=
��" #
g
��$ %
.
��% &
Key
��& )
.
��) *

��* 7
,
��7 8
InfoComprobante
�� #
=
��$ %
String
��& ,
.
��, -
Join
��- 1
(
��1 2
$str
��2 6
,
��6 7
g
��8 9
.
��9 :
Select
��: @
(
��@ A
gg
��A C
=>
��D F
gg
��G I
.
��I J
InfoComprobante
��J Y
)
��Y Z
.
��Z [
Distinct
��[ c
(
��c d
)
��d e
.
��e f
ToArray
��f m
(
��m n
)
��n o
)
��o p
,
��p q
NumeroOperaciones
�� %
=
��& '
g
��( )
.
��) *
Select
��* 0
(
��0 1
gg
��1 3
=>
��4 6
gg
��7 9
.
��9 :
InfoComprobante
��: I
)
��I J
.
��J K
Distinct
��K S
(
��S T
)
��T U
.
��U V
Count
��V [
(
��[ \
)
��\ ]
,
��] ^
Importe
�� 
=
�� 
g
�� 
.
��  
Sum
��  #
(
��# $
gg
��$ &
=>
��' )
gg
��* ,
.
��, -
ImporteTotal
��- 9
)
��9 :
,
��: ;
Revocado
�� 
=
�� 
g
��  
.
��  !
Sum
��! $
(
��$ %
gg
��% '
=>
��( *
gg
��+ -
.
��- .

��. ;
)
��; <
,
��< =
ACuenta
�� 
=
�� 
g
�� 
.
��  
Sum
��  #
(
��# $
gg
��$ &
=>
��' )
gg
��* ,
.
��, -
ACuentaTotal
��- 9
)
��9 :
,
��: ;
Saldo
�� 
=
�� 
g
�� 
.
�� 
Sum
�� !
(
��! "
gg
��" $
=>
��% '
gg
��( *
.
��* +

SaldoTotal
��+ 5
)
��5 6
,
��6 7
}
�� 
)
�� 
.
�� 
ToList
�� 
(
�� 
)
�� 
;
�� 
return
�� 
	resultado
��  
;
��  !
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* c
,
��c d
e
��e f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� %
OperacionGrupoDetallado
�� +
>
��+ ,3
%ObtenerCuentasPorCobrarGrupoDetallado
��- R
(
��R S
int
��S V
idGrupo
��W ^
)
��^ _
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� %
OperacionGrupoDetallado
�� ,
>
��, -&
porCobrarGruposDetallado
��. F
=
��G H$
_finanzaReportingDatos
��I _
.
��_ `3
$ObtenerCuentasPorCobarGrupoDetallado��` �
(��� �
idGrupo��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
var
�� #
gruposPorCobrarGrupos
�� )
=
��* +&
porCobrarGruposDetallado
��, D
.
��D E
GroupBy
��E L
(
��L M
g
��M N
=>
��O Q
new
��R U
{
��V W
g
��X Y
.
��Y Z
Id
��Z \
,
��\ ]
g
��^ _
.
��_ `
IdTipoTransaccion
��` q
,
��q r
g
��s t
.
��t u 
NombreResponsable��u �
,��� �
g��� �
.��� � 
DocumentoCliente��� �
,��� �
g��� �
.��� �

,��� �
g��� �
.��� �
Emision��� �
,��� �
g��� �
.��� �
TipoComprobante��� �
,��� �
g��� �
.��� �

}��� �
)��� �
;��� �
var
�� 
	resultado
�� 
=
�� #
gruposPorCobrarGrupos
��  5
.
��5 6
Select
��6 <
(
��< =
g
��= >
=>
��? A
new
��B E%
OperacionGrupoDetallado
��F ]
(
��] ^
)
��^ _
{
�� 
Id
�� 
=
�� 
g
�� 
.
�� 
Key
�� 
.
�� 
Id
�� !
,
��! "
IdTipoTransaccion
�� %
=
��& '
g
��( )
.
��) *
Key
��* -
.
��- .
IdTipoTransaccion
��. ?
,
��? @
NombreResponsable
�� %
=
��& '
g
��( )
.
��) *
Key
��* -
.
��- .
NombreResponsable
��. ?
,
��? @
DocumentoCliente
�� $
=
��% &
g
��' (
.
��( )
Key
��) ,
.
��, -
DocumentoCliente
��- =
,
��= >

�� !
=
��" #
g
��$ %
.
��% &
Key
��& )
.
��) *

��* 7
,
��7 8
Emision
�� 
=
�� 
g
�� 
.
��  
Key
��  #
.
��# $
Emision
��$ +
,
��+ ,
TipoComprobante
�� #
=
��$ %
g
��& '
.
��' (
Key
��( +
.
��+ ,
TipoComprobante
��, ;
,
��; <

�� !
=
��" #
g
��$ %
.
��% &
Key
��& )
.
��) *

��* 7
,
��7 8%
SerieYNumeroComprobante
�� +
=
��, -
g
��. /
.
��/ 0
First
��0 5
(
��5 6
)
��6 7
.
��7 8%
SerieYNumeroComprobante
��8 O
,
��O P
Importe
�� 
=
�� 
g
�� 
.
��  
Sum
��  #
(
��# $
gg
��$ &
=>
��' )
gg
��* ,
.
��, -
Importe
��- 4
)
��4 5
,
��5 6
Revocado
�� 
=
�� 
g
��  
.
��  !
Sum
��! $
(
��$ %
gg
��% '
=>
��( *
gg
��+ -
.
��- .
Revocado
��. 6
)
��6 7
,
��7 8
ACuenta
�� 
=
�� 
g
�� 
.
��  
Sum
��  #
(
��# $
gg
��$ &
=>
��' )
gg
��* ,
.
��, -
ACuenta
��- 4
)
��4 5
,
��5 6
Saldo
�� 
=
�� 
g
�� 
.
�� 
Sum
�� !
(
��! "
gg
��" $
=>
��% '
gg
��( *
.
��* +
Saldo
��+ 0
)
��0 1
}
�� 
)
�� 
.
�� 
ToList
�� 
(
�� 
)
�� 
;
�� 
return
�� 
	resultado
��  
;
��  !
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* l
,
��l m
e
��n o
)
��o p
;
��p q
}
�� 
}
�� 	
}
�� 
}�� �&
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\LibrosElectronicos\LibrosElectronicosFoxcontLogica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

class +
LibrosElectronicosFoxcontLogica 0
:1 2,
 ILibrosElectronicosFoxcontLogica3 S
{ 
	protected 
readonly 1
%ILibrosElectronicosFoxcontRepositorio @+
_librosElectronicosFoxcontDatosA `
;` a
public +
LibrosElectronicosFoxcontLogica .
(. /1
%ILibrosElectronicosFoxcontRepositorio/ T*
librosElectronicosFoxcontDatosU s
)s t
{ 	+
_librosElectronicosFoxcontDatos +
=, -*
librosElectronicosFoxcontDatos. L
;L M
} 	
public 
List 
< %
ReporteVentaClienteFoxcom -
>- .7
+ObtenerVentaClienteFoxcomBoletaVentaFactura/ Z
(Z [
int[ ^

idEmpleado_ i
,i j
DateTimek s

fechaDesdet ~
,~ 
DateTime
� �

fechaHasta
� �
)
� �
{   	
try!! 
{"" 
var##  
ventasClientesFoxcom## (
=##) *+
_librosElectronicosFoxcontDatos##+ J
.##J K&
ObtenerVentasClienteFoxcom##K e
(##e f
Diccionario##f q
.##q rF
9TiposDeComprobanteTributablesExceptoNotasDeCreditoYDebito	##r �
,
##� �
new
##� �
int
##� �
[
##� �
]
##� �
{
##� �!
TransaccionSettings
##� �
.
##� �
Default
##� �
.
##� �+
IdTipoTransaccionOrdenDeVenta
##� �
}
##� �
,
##� �

fechaDesde
##� �
,
##� �

fechaHasta
##� �
)
##� �
.
##� �
ToList
##� �
(
##� �
)
##� �
;
##� �
var$$ 

=$$" # 
ventasClientesFoxcom$$$ 8
.$$8 9
OrderBy$$9 @
($$@ A
v$$A B
=>$$C E
v$$F G
.$$G H
FechaEmision$$H T
)$$T U
.$$U V
ToList$$V \
($$\ ]
)$$] ^
;$$^ _
var%% 
reporteFoxcont%% "
=%%# $%
ReporteVentaClienteFoxcom%%% >
.%%> ?
Convert%%? F
(%%F G

)%%T U
;%%U V
return&& 
reporteFoxcont&& %
;&&% &
}'' 
catch(( 
((( 
	Exception(( 
e(( 
)(( 
{)) 
throw** 
new** 
LogicaException** )
(**) *
$str*** I
,**I J
e**K L
)**L M
;**M N
}++ 
},, 	
public.. 
List.. 
<.. %
ReporteVentaClienteFoxcom.. -
>..- .6
*ObtenerVentaClienteFoxcomNotaCreditoDebito../ Y
(..Y Z
int..Z ]

idEmpleado..^ h
,..h i
DateTime..j r

fechaDesde..s }
,..} ~
DateTime	.. �

fechaHasta
..� �
)
..� �
{// 	
try00 
{11 
var22  
ventasClientesFoxcom22 (
=22) *+
_librosElectronicosFoxcontDatos22+ J
.22J K<
0ObtenerVentasClienteFoxcomConOperacionReferencia22K {
(22{ |
Diccionario	22| �
.
22� �D
6TiposDeComprobanteTributablesParaNotasDeCreditoYDebito
22� �
,
22� �
Diccionario
22� �
.
22� �U
GTiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito
22� �
,
22� �

fechaDesde
22� �
,
22� �

fechaHasta
22� �
)
22� �
.
22� �
ToList
22� �
(
22� �
)
22� �
;
22� �
var33 

=33" # 
ventasClientesFoxcom33$ 8
.338 9
OrderBy339 @
(33@ A
v33A B
=>33C E
v33F G
.33G H
FechaEmision33H T
)33T U
.33U V
ToList33V \
(33\ ]
)33] ^
;33^ _
var44 
reporteFoxcont44 "
=44# $%
ReporteVentaClienteFoxcom44% >
.44> ?
Convert44? F
(44F G

)44T U
;44U V
return55 
reporteFoxcont55 %
;55% &
}66 
catch77 
(77 
	Exception77 
e77 
)77 
{88 
throw99 
new99 
LogicaException99 )
(99) *
$str99* I
,99I J
e99K L
)99L M
;99M N
}:: 
};; 	
}<< 
}== ��
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\LibrosElectronicos\LibrosElectronicosAdsoftLogica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

class *
LibrosElectronicosAdsoftLogica /
:0 1+
ILibrosElectronicosAdsoftLogica2 Q
{ 
	protected 
readonly 0
$ILibrosElectronicosAdsoftRepositorio ?*
_librosElectronicosAdsoftDatos@ ^
;^ _
	protected 
readonly #
ITransaccionRepositorio 2#
_transaccionRepositorio3 J
;J K
public *
LibrosElectronicosAdsoftLogica -
(- .0
$ILibrosElectronicosAdsoftRepositorio. R)
librosElectronicosAdsoftDatosS p
,p q$
ITransaccionRepositorio	r �$
transaccionRepositorio
� �
)
� �
{ 	*
_librosElectronicosAdsoftDatos *
=+ ,)
librosElectronicosAdsoftDatos- J
;J K#
_transaccionRepositorio #
=$ %"
transaccionRepositorio& <
;< =
} 	
public!! 
List!! 
<!! %
ReporteVentaClienteAdsoft!! -
>!!- .+
ObtenerLibrosElectronicosAdsoft!!/ N
(!!N O
int!!O R

idEmpleado!!S ]
,!!] ^
DateTime!!_ g

fechaDesde!!h r
,!!r s
DateTime!!t |

fechaHasta	!!} �
)
!!� �
{"" 	
try## 
{$$ 
List%% 
<%% 
VentaClienteAdsoft%% '
>%%' (
	registros%%) 2
=%%3 4
new%%5 8
List%%9 =
<%%= >
VentaClienteAdsoft%%> P
>%%P Q
(%%Q R
)%%R S
;%%S T
List&& 
<&& 
VentaClienteAdsoft&& '
>&&' (
ventasDelPeriodo&&) 9
=&&: ;A
5ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito&&< q
(&&q r

fechaDesde&&r |
,&&| }

fechaHasta	&&~ �
)
&&� �
.
&&� �
OrderBy
&&� �
(
&&� �
vp
&&� �
=>
&&� �
vp
&&� �
.
&&� �
FechaEmision
&&� �
)
&&� �
.
&&� �
ThenBy
&&� �
(
&&� �
vp
&&� �
=>
&&� �
vp
&&� �
.
&&� �
NumeroSerie
&&� �
)
&&� �
.
&&� �
ThenBy
&&� �
(
&&� �
vp
&&� �
=>
&&� �
vp
&&� �
.
&&� �
NumeroComprobante
&&� �
)
&&� �
.
&&� �
ToList
&&� �
(
&&� �
)
&&� �
;
&&� �
List'' 
<'' 
VentaClienteAdsoft'' '
>''' (%
ventasDelPeriodoConBoleta'') B
=''C D
ventasDelPeriodo''E U
.''U V
Where''V [
(''[ \
vp''\ ^
=>''_ a
vp''b d
.''d e
IdTipoComprobante''e v
==''w y
MaestroSettings	''z �
.
''� �
Default
''� �
.
''� �/
!IdDetalleMaestroComprobanteBoleta
''� �
)
''� �
.
''� �
OrderBy
''� �
(
''� �
vp
''� �
=>
''� �
vp
''� �
.
''� �
FechaEmision
''� �
)
''� �
.
''� �
ThenBy
''� �
(
''� �
vp
''� �
=>
''� �
vp
''� �
.
''� �
NumeroSerie
''� �
)
''� �
.
''� �
ThenBy
''� �
(
''� �
vp
''� �
=>
''� �
vp
''� �
.
''� �
NumeroComprobante
''� �
)
''� �
.
''� �
ToList
''� �
(
''� �
)
''� �
;
''� �
List(( 
<(( 
int(( 
>(( 
	idsSeries(( #
=(($ %>
2ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta((& X
(((X Y
)((Y Z
;((Z [
foreach)) 
()) 
var)) 
idSerie)) $
in))% '
	idsSeries))( 1
)))1 2
{** 
List++ 
<++ 
VentaClienteAdsoft++ +
>+++ ,
ventasDeLaSerie++- <
=++= >%
ventasDelPeriodoConBoleta++? X
.++X Y
Where++Y ^
(++^ _
vi++_ a
=>++b d
vi++e g
.++g h
IdSerie++h o
==++p r
idSerie++s z
)++z {
.++{ |
OrderBy	++| �
(
++� �
vp
++� �
=>
++� �
vp
++� �
.
++� �
FechaEmision
++� �
)
++� �
.
++� �
ThenBy
++� �
(
++� �
vp
++� �
=>
++� �
vp
++� �
.
++� �
NumeroSerie
++� �
)
++� �
.
++� �
ThenBy
++� �
(
++� �
vp
++� �
=>
++� �
vp
++� �
.
++� �
NumeroComprobante
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
++� �
if-- 
(-- 
ventasDeLaSerie-- '
.--' (
Count--( -
(--- .
)--. /
>--0 1
$num--2 3
)--3 4
{.. 
int// 
contVentasDeLaSerie// /
=//0 1
$num//2 3
;//3 4
int00 %
cantidadDeVentasDeLaSerie00 5
=006 7
ventasDeLaSerie008 G
.00G H
Count00H M
(00M N
)00N O
;00O P
bool11 =
1terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha11 N
=11O P
false11Q V
;11V W
int22 <
0cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha22 L
=22M N
$num22O P
;22P Q
do44 
{55 
List66  
<66  !
VentaClienteAdsoft66! 3
>663 4.
"ventasAgrupadasPorTipoAgrupamiento665 W
=66X Y
new66Z ]
List66^ b
<66b c
VentaClienteAdsoft66c u
>66u v
(66v w
)66w x
;66x y
List77  
<77  !
VentaClienteAdsoft77! 3
>773 4
itemVentasDeLaSerie775 H
=77I J
null77K O
;77O P
itemVentasDeLaSerie88 /
=880 1
ventasDeLaSerie882 A
.88A B
Skip88B F
(88F G
contVentasDeLaSerie88G Z
)88Z [
.88[ \
Take88\ `
(88` a
$num88a b
)88b c
.88c d
ToList88d j
(88j k
)88k l
;88l m
List99  
<99  !
VentaClienteAdsoft99! 3
>993 4#
ventasDeLaSeriePorFecha995 L
=99M N
ventasDeLaSerie99O ^
.99^ _
Where99_ d
(99d e
vsf99e h
=>99i k
vsf99l o
.99o p
FechaEmision99p |
==99} !
itemVentasDeLaSerie
99� �
.
99� �
First
99� �
(
99� �
)
99� �
.
99� �
FechaEmision
99� �
)
99� �
.
99� �
ToList
99� �
(
99� �
)
99� �
;
99� �
int;; -
!cantidadDeVentasDeLaSeriePorFecha;;  A
=;;B C#
ventasDeLaSeriePorFecha;;D [
.;;[ \
Count;;\ a
(;;a b
);;b c
;;;c d
int<< '
contVentasDeLaSeriePorFecha<<  ;
=<<< ==
1terminoTodoElRecorridoDeLaVentasDeLaSeriePorFecha<<> o
?<<p q
$num<<r s
:<<t u=
0cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFecha	<<v �
;
<<� �.
"ventasAgrupadasPorTipoAgrupamiento== >
.==> ?
Add==? B
(==B C
itemVentasDeLaSerie==C V
.==V W
First==W \
(==\ ]
)==] ^
)==^ _
;==_ `
List>>  
<>>  !
VentaClienteAdsoft>>! 3
>>>3 4
element>>5 <
=>>= >#
ventasDeLaSeriePorFecha>>? V
.>>V W
Where>>W \
(>>\ ]
vs>>] _
=>>>` b
vs>>c e
.>>e f
NumeroComprobante>>f w
==>>x z
$num>>{ ~
)>>~ 
.	>> �
ToList
>>� �
(
>>� �
)
>>� �
;
>>� �
bool@@  
	consolido@@! *
=@@+ ,
false@@- 2
;@@2 3
whileAA !
(AA" #'
contVentasDeLaSeriePorFechaAA# >
<AA? @-
!cantidadDeVentasDeLaSeriePorFechaAAA b
-AAc d
$numAAe f
)AAf g
{BB 
varCC  #
itemCC$ (
=CC) *#
ventasDeLaSeriePorFechaCC+ B
.CCB C
SkipCCC G
(CCG H'
contVentasDeLaSeriePorFechaCCH c
+CCd e
$numCCf g
)CCg h
.CCh i
TakeCCi m
(CCm n
$numCCn o
)CCo p
.CCp q
ToListCCq w
(CCw x
)CCx y
;CCy z
ifDD  "
(DD# $
itemVentasDeLaSerieDD$ 7
.DD7 8
FirstDD8 =
(DD= >
)DD> ?
.DD? @!
IdActorNegocioExternoDD@ U
==DDV X
itemDDY ]
.DD] ^
FirstDD^ c
(DDc d
)DDd e
.DDe f!
IdActorNegocioExternoDDf {
&&DD| ~ 
itemVentasDeLaSerie	DD �
.
DD� �
First
DD� �
(
DD� �
)
DD� �
.
DD� �
TipoAgrupamiento
DD� �
==
DD� �
item
DD� �
.
DD� �
First
DD� �
(
DD� �
)
DD� �
.
DD� �
TipoAgrupamiento
DD� �
)
DD� �
{EE  !.
"ventasAgrupadasPorTipoAgrupamientoFF$ F
.FFF G
AddFFG J
(FFJ K
itemFFK O
.FFO P
FirstFFP U
(FFU V
)FFV W
)FFW X
;FFX Y'
contVentasDeLaSeriePorFechaGG$ ?
++GG? A
;GGA B
contVentasDeLaSerieHH$ 7
++HH7 9
;HH9 :
}II  !
elseJJ  $
{KK  !=
1terminoTodoElRecorridoDeLaVentasDeLaSeriePorFechaLL$ U
=LLV W'
contVentasDeLaSeriePorFechaLLX s
==LLt v.
!cantidadDeVentasDeLaSeriePorFecha	LLw �
-
LL� �
$num
LL� �
;
LL� �'
contVentasDeLaSeriePorFechaMM$ ?
++MM? A
;MMA B
contVentasDeLaSerieNN$ 7
++NN7 9
;NN9 :
ListOO$ (
<OO( )
VentaClienteAdsoftOO) ;
>OO; <'
ventasAgrupadasConsolidadasOO= X
=OOY Z%
ConsolidadarVentasClienteOO[ t
(OOt u/
"ventasAgrupadasPorTipoAgrupamiento	OOu �
)
OO� �
;
OO� �
	consolidoPP$ -
=PP. /
truePP0 4
;PP4 5
	registrosQQ$ -
.QQ- .
AddRangeQQ. 6
(QQ6 7'
ventasAgrupadasConsolidadasQQ7 R
)QQR S
;QQS T
breakRR$ )
;RR) *
}SS  !
}TT 
ifUU 
(UU  '
contVentasDeLaSeriePorFechaUU  ;
==UU< >-
!cantidadDeVentasDeLaSeriePorFechaUU? `
-UUa b
$numUUc d
)UUd e
{VV =
1terminoTodoElRecorridoDeLaVentasDeLaSeriePorFechaWW  Q
=WWR S
trueWWT X
;WWX Y
contVentasDeLaSerieXX  3
++XX3 5
;XX5 6<
0cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFechaYY  P
=YYQ R'
contVentasDeLaSeriePorFechaYYS n
;YYn o
ListZZ  $
<ZZ$ %
VentaClienteAdsoftZZ% 7
>ZZ7 8'
ventasAgrupadasConsolidadasZZ9 T
;ZZT U
if[[  "
([[# $
	consolido[[$ -
)[[- .
{\\  !
var]]$ '
item]]( ,
=]]- .#
ventasDeLaSeriePorFecha]]/ F
.]]F G
Skip]]G K
(]]K L'
contVentasDeLaSeriePorFecha]]L g
)]]g h
.]]h i
Take]]i m
(]]m n
$num]]n o
)]]o p
.]]p q
ToList]]q w
(]]w x
)]]x y
;]]y z'
ventasAgrupadasConsolidadas^^$ ?
=^^@ A%
ConsolidadarVentasCliente^^B [
(^^[ \
item^^\ `
)^^` a
;^^a b
}__  !
else``  $
{aa  !'
ventasAgrupadasConsolidadasbb$ ?
=bb@ A%
ConsolidadarVentasClientebbB [
(bb[ \.
"ventasAgrupadasPorTipoAgrupamientobb\ ~
)bb~ 
;	bb �
}cc  !
	registrosdd  )
.dd) *
AddRangedd* 2
(dd2 3'
ventasAgrupadasConsolidadasdd3 N
)ddN O
;ddO P
}ee 
elseff  
{gg <
0cantidadDondeSeQuedoDeLasVentasDeLaSeriePorFechahh  P
=hhQ R'
contVentasDeLaSeriePorFechahhS n
;hhn o
}ii 
}jj 
whilejj 
(jj  !
contVentasDeLaSeriejj! 4
<jj5 6%
cantidadDeVentasDeLaSeriejj7 P
)jjP Q
;jjQ R
}kk 
}ll 
Listmm 
<mm 
VentaClienteAdsoftmm '
>mm' (
ventasConFacturamm) 9
=mm: ;
ventasDelPeriodomm< L
.mmL M
WheremmM R
(mmR S
vmmS T
=>mmU W
!mmX Y
vmmY Z
.mmZ [
EsInvalidadamm[ g
&&mmh j
vmmk l
.mml m
IdTipoComprobantemmm ~
==	mm �
MaestroSettings
mm� �
.
mm� �
Default
mm� �
.
mm� �0
"IdDetalleMaestroComprobanteFactura
mm� �
)
mm� �
.
mm� �
ToList
mm� �
(
mm� �
)
mm� �
;
mm� �
	registrosnn 
.nn 
AddRangenn "
(nn" #
ventasConFacturann# 3
)nn3 4
;nn4 5
Listoo 
<oo 
VentaClienteAdsoftoo '
>oo' ('
ventasInvalidadasConFacturaoo) D
=ooE F
ventasDelPeriodoooG W
.ooW X
WhereooX ]
(oo] ^
voo^ _
=>oo` b
vooc d
.ood e
EsInvalidadaooe q
&&oor t
voou v
.oov w
IdTipoComprobante	oow �
==
oo� �
MaestroSettings
oo� �
.
oo� �
Default
oo� �
.
oo� �0
"IdDetalleMaestroComprobanteFactura
oo� �
)
oo� �
.
oo� �
ToList
oo� �
(
oo� �
)
oo� �
;
oo� �
	registrospp 
.pp 
AddRangepp "
(pp" #'
ventasInvalidadasConFacturapp# >
)pp> ?
;pp? @
Listqq 
<qq 
VentaClienteAdsoftqq '
>qq' (*
ventasConNotasDeCreditoYDebitoqq) G
=qqH IL
?ObtenerVentasClientesQueSeanConNotasDeCreditoYDebitoConfirmadas	qqJ �
(
qq� �

fechaDesde
qq� �
,
qq� �

fechaHasta
qq� �
)
qq� �
;
qq� �
	registrosrr 
.rr 
AddRangerr "
(rr" #*
ventasConNotasDeCreditoYDebitorr# A
)rrA B
;rrB C
	registrosss 
=ss 
	registrosss %
.ss% &
OrderByss& -
(ss- .
rss. /
=>ss0 2
rss3 4
.ss4 5
NumeroSeriess5 @
)ss@ A
.ssA B
ThenByssB H
(ssH I
rssI J
=>ssK M
rssN O
.ssO P
NumeroComprobantessP a
)ssa b
.ssb c
ThenByssc i
(ssi j
rssj k
=>ssl n
rsso p
.ssp q
FechaEmisionssq }
)ss} ~
.ss~ 
ThenBy	ss �
(
ss� �
r
ss� �
=>
ss� �
r
ss� �
.
ss� �
IdTipoComprobante
ss� �
)
ss� �
.
ss� �
ToList
ss� �
(
ss� �
)
ss� �
;
ss� �
vartt %
reporteVentaClienteAdsofttt -
=tt. /%
ReporteVentaClienteAdsofttt0 I
.ttI J
ConvertttJ Q
(ttQ R
	registrosttR [
)tt[ \
;tt\ ]
returnuu %
reporteVentaClienteAdsoftuu 0
;uu0 1
}vv 
catchww 
(ww 
	Exceptionww 
eww 
)ww 
{xx 
throwyy 
newyy 
LogicaExceptionyy )
(yy) *
$stryy* T
,yyT U
eyyV W
)yyW X
;yyX Y
}zz 
}{{ 	
private}} 
List}} 
<}} 
int}} 
>}} >
2ObtenerIdsDeSeriesDeComprobantesParaBoletasDeVenta}} L
(}}L M
)}}M N
{~~ 	
return #
_transaccionRepositorio *
.* +(
ObtenerIdsSeriesComprobantes+ G
(G H
newH K
intL O
[O P
]P Q
{R S
MaestroSettingsT c
.c d
Defaultd k
.k l.
!IdDetalleMaestroComprobanteBoleta	l �
}
� �
)
� �
.
� �
ToList
� �
(
� �
)
� �
;
� �
}
�� 	
private
�� 
List
�� 
<
��  
VentaClienteAdsoft
�� '
>
��' (C
5ObtenerVentasClienteQueNoSeanConNotasDeCreditoYDebito
��) ^
(
��^ _
DateTime
��_ g

fechaDesde
��h r
,
��r s
DateTime
��t |

fechaHasta��} �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
	resultado
�� 
=
�� ,
_librosElectronicosAdsoftDatos
��  >
.
��> ?"
ObtenerVentasCliente
��? S
(
��S T
Diccionario
��T _
.
��_ `H
9TiposDeComprobanteTributablesExceptoNotasDeCreditoYDebito��` �
,��� �
new��� �
int��� �
[��� �
]��� �
{��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �-
IdTipoTransaccionOrdenDeVenta��� �
}��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
return
�� 
	resultado
��  
;
��  !
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* I
,
��I J
e
��K L
)
��L M
;
��M N
}
�� 
}
�� 	
private
�� 
List
�� 
<
��  
VentaClienteAdsoft
�� '
>
��' (M
?ObtenerVentasClientesQueSeanConNotasDeCreditoYDebitoConfirmadas
��) h
(
��h i
DateTime
��i q

fechaDesde
��r |
,
��| }
DateTime��~ �

fechaHasta��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
	resultado
�� 
=
�� ,
_librosElectronicosAdsoftDatos
��  >
.
��> ?T
EObtenerVentasClienteConOperacionDeReferenciaSegunElEstadoQueDebeTener��? �
(��� �
Diccionario��� �
.��� �F
6TiposDeComprobanteTributablesParaNotasDeCreditoYDebito��� �
,��� �
Diccionario��� �
.��� �W
GTiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
.��� �
OrderBy��� �
(��� �
t��� �
=>��� �
t��� �
.��� �
FechaEmision��� �
)��� �
.��� �
ThenBy��� �
(��� �
t��� �
=>��� �
t��� �
.��� �
NumeroSerie��� �
)��� �
.��� �
ThenBy��� �
(��� �
t��� �
=>��� �
t��� �
.��� �
Id��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
return
�� 
	resultado
��  
;
��  !
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* w
,
��w x
e
��y z
)
��z {
;
��{ |
}
�� 
}
�� 	
private
�� 
List
�� 
<
��  
VentaClienteAdsoft
�� '
>
��' ('
ConsolidadarVentasCliente
��) B
(
��B C
List
��C G
<
��G H 
VentaClienteAdsoft
��H Z
>
��Z [
ventasAgrupadas
��\ k
)
��k l
{
�� 	
List
�� 
<
��  
VentaClienteAdsoft
�� #
>
��# $ 
ventasConsolidadas
��% 7
=
��8 9
ventasAgrupadas
��: I
.
�� 
GroupBy
�� 
(
�� 
vcs
�� 
=>
�� 
new
��  #
{
�� 
Fecha
�� 
=
�� 
new
�� 
{
��  !
y
��" #
=
��$ %
vcs
��& )
.
��) *
FechaEmision
��* 6
.
��6 7
Year
��7 ;
,
��; <
m
��= >
=
��? @
vcs
��A D
.
��D E
FechaEmision
��E Q
.
��Q R
Month
��R W
,
��W X
d
��Y Z
=
��[ \
vcs
��] `
.
��` a
FechaEmision
��a m
.
��m n
Day
��n q
}
��r s
,
��s t
vcs
�� 
.
�� 
IdSerie
�� 
,
��  
vcs
�� 
.
�� 
NumeroSerie
�� #
,
��# $
vcs
�� 
.
�� 
CodigoComprobante
�� )
,
��) *
vcs
�� 
.
�� 
IdTipoComprobante
�� )
,
��) *
vcs
�� 
.
�� 
CodigoMoneda
�� $
,
��$ %
}
�� 
)
�� 
.
�� 
Select
�� 
(
�� 
vcc
�� 
=>
�� 
new
�� " 
VentaClienteAdsoft
��# 5
(
��5 6
)
��6 7
{
�� 
Anyo
�� 
=
�� 
vcc
�� 
.
�� 
Key
�� "
.
��" #
Fecha
��# (
.
��( )
y
��) *
,
��* +
Mes
�� 
=
�� 
vcc
�� 
.
�� 
Key
�� !
.
��! "
Fecha
��" '
.
��' (
m
��( )
,
��) *
Dia
�� 
=
�� 
vcc
�� 
.
�� 
Key
�� !
.
��! "
Fecha
��" '
.
��' (
d
��( )
,
��) *
IdTipoComprobante
�� %
=
��& '
vcc
��( +
.
��+ ,
Key
��, /
.
��/ 0
IdTipoComprobante
��0 A
,
��A B
CodigoComprobante
�� %
=
��& '
vcc
��( +
.
��+ ,
Key
��, /
.
��/ 0
CodigoComprobante
��0 A
,
��A B
IdSerie
�� 
=
�� 
vcc
�� !
.
��! "
Key
��" %
.
��% &
IdSerie
��& -
,
��- .
NumeroSerie
�� 
=
��  !
vcc
��" %
.
��% &
Key
��& )
.
��) *
NumeroSerie
��* 5
,
��5 6
NumeroComprobante
�� %
=
��& '
(
��( )
int
��) ,
)
��, -
vcc
��- 0
.
��0 1
Min
��1 4
(
��4 5
vc
��5 7
=>
��8 :
vc
��; =
.
��= >
NumeroComprobante
��> O
)
��O P
,
��P Q

�� !
=
��" #
(
��$ %
int
��% (
)
��( )
vcc
��) ,
.
��, -
Min
��- 0
(
��0 1
vc
��1 3
=>
��4 6
vc
��7 9
.
��9 :
NumeroComprobante
��: K
)
��K L
,
��L M
NumeroFinal
�� 
=
��  !
(
��" #
int
��# &
)
��& '
vcc
��' *
.
��* +
Max
��+ .
(
��. /
vc
��/ 1
=>
��2 4
vc
��5 7
.
��7 8
NumeroComprobante
��8 I
)
��I J
,
��J K
ValorIgv
�� 
=
�� 
vcc
�� "
.
��" #
Sum
��# &
(
��& '
vc
��' )
=>
��* ,
(
��- .
decimal
��. 5
)
��5 6
vc
��6 8
.
��8 9
Igv
��9 <
)
��< =
,
��= >

ValorVenta
�� 
=
��  
vcc
��! $
.
��$ %
Sum
��% (
(
��( )
vc
��) +
=>
��, .
vc
��/ 1
.
��1 2
ValorDeVenta
��2 >
)
��> ?
,
��? @
Total
�� 
=
�� 
vcc
�� 
.
��  
Sum
��  #
(
��# $
vc
��$ &
=>
��' )
vc
��* ,
.
��, -
ImporteTotal
��- 9
)
��9 :
,
��: ;
ValorIcbper
�� 
=
��  !
vcc
��" %
.
��% &
Sum
��& )
(
��) *
vc
��* ,
=>
��- /
vc
��0 2
.
��2 3
ValorIcbper
��3 >
)
��> ?
,
��? @
PrimerNombre
��  
=
��! "
vcc
��# &
.
��& '
FirstOrDefault
��' 5
(
��5 6
)
��6 7
.
��7 8
PrimerNombre
��8 D
,
��D E
IdTipoDocumento
�� #
=
��$ %
vcc
��& )
.
��) *
FirstOrDefault
��* 8
(
��8 9
)
��9 :
.
��: ;
IdTipoDocumento
��; J
,
��J K
NumeroDocumento
�� #
=
��$ %
vcc
��& )
.
��) *
FirstOrDefault
��* 8
(
��8 9
)
��9 :
.
��: ;
NumeroDocumento
��; J
,
��J K
CodigoMoneda
��  
=
��! "
vcc
��# &
.
��& '
Key
��' *
.
��* +
CodigoMoneda
��+ 7
,
��7 8
IdEstadoActual
�� "
=
��# $
vcc
��% (
.
��( )
FirstOrDefault
��) 7
(
��7 8
)
��8 9
.
��9 :
IdEstadoActual
��: H
,
��H I&
IdEstadoAnteriorAlActual
�� ,
=
��- .
vcc
��/ 2
.
��2 3
FirstOrDefault
��3 A
(
��A B
)
��B C
.
��C D&
IdEstadoAnteriorAlActual
��D \
}
�� 
)
�� 
.
�� 
OrderBy
�� 
(
�� 
t
�� 
=>
�� 
t
�� 
.
��  
FechaEmision
��  ,
)
��, -
.
��- .
ThenBy
��. 4
(
��4 5
t
��5 6
=>
��7 9
t
��: ;
.
��; <
NumeroSerie
��< G
)
��G H
.
��H I
ThenBy
��I O
(
��O P
t
��P Q
=>
��R T
t
��U V
.
��V W
NumeroComprobante
��W h
)
��h i
.
��i j
ToList
��j p
(
��p q
)
��q r
;
��r s
return
��  
ventasConsolidadas
�� %
;
��% &
}
�� 	
}
�� 
}�� ��
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\LibrosElectronicos\LibrosElectronicosConcarLogica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

class *
LibrosElectronicosConcarLogica /
:0 1+
ILibrosElectronicosConcarLogica2 Q
{ 
	protected 
readonly 0
$ILibrosElectronicosConcarRepositorio ?*
_librosElectronicosConcarDatos@ ^
;^ _
public *
LibrosElectronicosConcarLogica -
(- .0
$ILibrosElectronicosConcarRepositorio. R)
librosElectronicosConcarDatosS p
)p q
{ 	*
_librosElectronicosConcarDatos *
=+ ,)
librosElectronicosConcarDatos- J
;J K
} 	
public 
static 

Dictionary  
<  !
int! $
,$ %
string& ,
>, -4
(MapeoComprobanteSigesVsComprobanteConcar. V
=W X
newY \

Dictionary] g
<g h
inth k
,k l
stringm s
>s t
{u v
{   
MaestroSettings  
.   
Default   $
.  $ %-
!IdDetalleMaestroComprobanteBoleta  % F
,  F G
ConcarSettings  H V
.  V W
Default  W ^
.  ^ _$
TipoDocumentoBoletaVenta  _ w
}  x y
,  y z
{!! 
MaestroSettings!!
.!! 
Default!! $
.!!$ %.
"IdDetalleMaestroComprobanteFactura!!% G
,!!G H
ConcarSettings!!I W
.!!W X
Default!!X _
.!!_ ` 
TipoDocumentoFactura!!` t
}!!u v
,!!v w
{"" 
MaestroSettings""
."" 
Default"" $
.""$ %2
&IdDetalleMaestroComprobanteNotaCredito""% K
,""K L
ConcarSettings""M [
.""[ \
Default""\ c
.""c d$
TipoDocumentoNotaCredito""d |
}""} ~
,""~ 
{## 
MaestroSettings##
.## 
Default## $
.##$ %3
'IdDetalleMaestroComprobanteNotaDeDebito##% L
,##L M
ConcarSettings##N \
.##\ ]
Default##] d
.##d e#
TipoDocumentoNotaDebito##e |
}##} ~
,##~ 
}$$ 	
;$$	 

private&& (
DetalleAsientoContableConcar&& ,/
#GenerarDetalleAsientoContableConcar&&- P
(&&P Q
OperacionVenta&&Q _
operacionVenta&&` n
,&&n o
string&&p v
	subDiario	&&w �
,
&&� �
bool
&&� �
esDebe
&&� �
,
&&� �
string
&&� �
correlativo
&&� �
,
&&� �
bool
&&� �!
esNotaCreditoDebito
&&� �
,
&&� �
string
&&� �
cuentaContable
&&� �
,
&&� �
decimal
&&� �
total
&&� �
)
&&� �
{'' 	
var(( 
detalleAsiento(( 
=((  
new((! $(
DetalleAsientoContableConcar((% A
{)) 
	SubDiario** 
=** 
	subDiario** %
,**% &
NumeroComprobante++ !
=++" #
correlativo++$ /
,++/ 0
FechaComprobante,,  
=,,! "
operacionVenta,,# 1
.,,1 2
FechaEmision,,2 >
.,,> ?
ToString,,? G
(,,G H
$str,,H T
),,T U
,,,U V
CodigoMoneda-- 
=-- 
operacionVenta-- -
.--- .
IdMoneda--. 6
==--7 9
MaestroSettings--: I
.--I J
Default--J Q
.--Q R'
IdDetalleMaestroMonedaSoles--R m
?--n o
ConcarSettings--p ~
.--~ 
Default	-- �
.
--� �
MonedaSoles
--� �
:
--� �
operacionVenta
--� �
.
--� �
IdMoneda
--� �
==
--� �
MaestroSettings
--� �
.
--� �
Default
--� �
.
--� �+
IdDetalleMaestroMonedaDolares
--� �
?
--� �
ConcarSettings
--� �
.
--� �
Default
--� �
.
--� �
MonedaDolarUsa
--� �
:
--� �
$str
--� �
,
--� �
GlosaPrincipal.. 
=..  4
(MapeoComprobanteSigesVsComprobanteConcar..! I
...I J
Single..J P
(..P Q
m..Q R
=>..S U
m..V W
...W X
Key..X [
==..\ ^
operacionVenta.._ m
...m n
IdTipoComprobante..n 
)	.. �
.
..� �
Value
..� �
+
..� �
$str
..� �
+
..� �
operacionVenta
..� �
.
..� �
SerieComprobante
..� �
+
..� �
$str
..� �
+
..� �
operacionVenta
..� �
.
..� �
NumeroComprobante
..� �
.
..� �
ToString
..� �
(
..� �
)
..� �
.
..� �
ToString
..� �
(
..� �
)
..� �
.
..� �
PadLeft
..� �
(
..� �
$num
..� �
,
..� �
$char
..� �
)
..� �
+
..� �
$str
..� �
+
..� �
operacionVenta
..� �
.
..� �

..� �
,
..� �

TipoCambio// 
=// 
operacionVenta// +
.//+ ,

TipoCambio//, 6
,//6 7
TipoConversion00 
=00  
ConcarSettings00! /
.00/ 0
Default000 7
.007 8
TipoConversionVenta008 K
,00K L 
FlagConversionMoneda11 $
=11% &
ConcarSettings11' 5
.115 6
Default116 =
.11= >-
!FlagConversionMonedaSiSeConvierte11> _
,11_ `
FechaTipoCambio22 
=22  !
$str22" $
,22$ %
CuentaContable33 
=33  
cuentaContable33! /
,33/ 0
CodigoAnexo44 
=44 
operacionVenta44 ,
.44, -"
NumeroDocumentoCliente44- C
,44C D
CodigoCentroCosto55 !
=55" #
$str55$ &
,55& '
	DebeHaber66 
=66 
esDebe66 "
?66# $
ConcarSettings66% 3
.663 4
Default664 ;
.66; <
Debe66< @
:66A B
ConcarSettings66C Q
.66Q R
Default66R Y
.66Y Z
Haber66Z _
,66_ `
ImporteOriginal77 
=77  !
total77" '
,77' (
ImporteDolares88 
=88  
$num88! "
,88" #
ImporteSoles99 
=99 
$num99  
,99  !

=:: 4
(MapeoComprobanteSigesVsComprobanteConcar::  H
.::H I
Single::I O
(::O P
m::P Q
=>::R T
m::U V
.::V W
Key::W Z
==::[ ]
operacionVenta::^ l
.::l m
IdTipoComprobante::m ~
)::~ 
.	:: �
Value
::� �
,
::� �
NumeroDocumento;; 
=;;  !
operacionVenta;;" 0
.;;0 1
SerieComprobante;;1 A
+;;B C
$str;;D G
+;;H I
operacionVenta;;J X
.;;X Y
NumeroComprobante;;Y j
.;;j k
ToString;;k s
(;;s t
);;t u
.;;u v
ToString;;v ~
(;;~ 
)	;; �
.
;;� �
PadLeft
;;� �
(
;;� �
$num
;;� �
,
;;� �
$char
;;� �
)
;;� �
,
;;� �
FechaDocumento<< 
=<<  
operacionVenta<<! /
.<</ 0
FechaDocumento<<0 >
.<<> ?
ToString<<? G
(<<G H
$str<<H T
)<<T U
,<<U V
FechaVencimiento==  
===! "
operacionVenta==# 1
.==1 2
FechaVencimiento==2 B
.==B C
ToString==C K
(==K L
$str==L X
)==X Y
,==Y Z

CodigoArea>> 
=>> 
$str>> 
,>>  
GlosaDetalle?? 
=?? 
operacionVenta?? -
.??- .(
NombreTipoTransaccionWrapper??. J
,??J K
CodigoAnexoAuxiliar@@ #
=@@$ %
$str@@& (
,@@( )
	MedioPagoAA 
=AA 
$strAA 
,AA #
TipoDocumentoReferenciaBB '
=BB( )
esNotaCreditoDebitoBB* =
?BB> ?4
(MapeoComprobanteSigesVsComprobanteConcarBB@ h
.BBh i
SingleBBi o
(BBo p
mBBp q
=>BBr t
mBBu v
.BBv w
KeyBBw z
==BB{ }
operacionVenta	BB~ �
.
BB� �)
IdTipoComprobanteReferencia
BB� �
)
BB� �
.
BB� �
Value
BB� �
:
BB� �
$str
BB� �
,
BB� �%
NumeroDocumentoReferenciaCC )
=CC* +
esNotaCreditoDebitoCC, ?
?CC@ A
operacionVentaCCB P
.CCP Q&
SerieComprobanteReferenciaCCQ k
+CCl m
$strCCn q
+CCr s
operacionVenta	CCt �
.
CC� �)
NumeroComprobanteReferencia
CC� �
.
CC� �
ToString
CC� �
(
CC� �
)
CC� �
.
CC� �
PadLeft
CC� �
(
CC� �
$num
CC� �
,
CC� �
$char
CC� �
)
CC� �
:
CC� �
$str
CC� �
,
CC� �$
FechaDocumentoReferenciaDD (
=DD) *
esNotaCreditoDebitoDD+ >
?DD? @
operacionVentaDDA O
.DDO P"
FechaEmisionReferenciaDDP f
.DDf g
ToStringDDg o
(DDo p
$strDDp |
)DD| }
:DD~ 
$str
DD� �
,
DD� �(
NroMaqRegistradoraTipoDocRefEE ,
=EE- .
$strEE/ 1
,EE1 2,
 BaseImponibleDocumentoReferenciaFF 0
=FF1 2
esNotaCreditoDebitoFF3 F
?FFG H
MathFFI M
.FFM N
RoundFFN S
(FFS T
operacionVentaFFT b
.FFb c#
BaseImponibleReferenciaFFc z
,FFz {
$numFF| }
)FF} ~
:	FF �
$num
FF� �
,
FF� �!
IGVDocumentoProvisionGG %
=GG& '
esNotaCreditoDebitoGG( ;
?GG< =
(GG> ?
operacionVentaGG? M
.GGM N

==GG\ ^
$numGG_ `
?GGa b
$numGGc d
:GGe f
MathGGg k
.GGk l
RoundGGl q
(GGq r
operacionVenta	GGr �
.
GG� �

GG� �
,
GG� �
$num
GG� �
)
GG� �
)
GG� �
:
GG� �
$num
GG� �
,
GG� �$
TipoReferenciaenestadoMQHH (
=HH) *
$strHH+ -
,HH- .'
NumeroSerieCajaRegistradoraII +
=II, -
$strII. 0
,II0 1
FechaOperacionJJ 
=JJ  
$strJJ! #
,JJ# $
TipoTasaKK 
=KK 
$strKK 
,KK $
TasaDetraccionPercepcionLL (
=LL) *
$numLL+ ,
,LL, -2
&ImporteBaseDetraccionPercepcionDolaresMM 6
=MM7 8
$numMM9 :
,MM: ;0
$ImporteBaseDetraccionPercepcionSolesNN 4
=NN5 6
$numNN7 8
,NN8 9
TipoCambioparaFOO 
=OO  !
$strOO" $
,OO$ %-
!ImporteIGVSinDerechoCreditoFiscalPP 1
=PP2 3
$numPP4 5
}QQ 
;QQ
returnRR 
detalleAsientoRR !
;RR! "
}SS 	
privateUU (
DetalleAsientoContableConcarUU ,<
0GenerarDetalleAsientoContableConcarRegistroNotasUU- ]
(UU] ^
OperacionVentaUU^ l
operacionVentaUUm {
,UU{ |
bool	UU} �
esDebe
UU� �
,
UU� �
string
UU� �
correlativo
UU� �
,
UU� �
bool
UU� �!
esNotaCreditoDebito
UU� �
,
UU� �
bool
UU� �
esReferencia
UU� �
,
UU� �
decimal
UU� �
total
UU� �
)
UU� �
{VV 	
varWW 
detalleAsientoWW 
=WW  
newWW! $(
DetalleAsientoContableConcarWW% A
{XX 
	SubDiarioYY 
=YY 
ConcarSettingsYY *
.YY* +
DefaultYY+ 2
.YY2 3&
SubDiarioProvisionesVariasYY3 M
,YYM N
NumeroComprobanteZZ !
=ZZ" #
correlativoZZ$ /
,ZZ/ 0
FechaComprobante[[  
=[[! "
operacionVenta[[# 1
.[[1 2
FechaDocumento[[2 @
.[[@ A
ToString[[A I
([[I J
$str[[J V
)[[V W
,[[W X
CodigoMoneda\\ 
=\\ 
operacionVenta\\ -
.\\- .
IdMoneda\\. 6
==\\7 9
MaestroSettings\\: I
.\\I J
Default\\J Q
.\\Q R'
IdDetalleMaestroMonedaSoles\\R m
?\\n o
ConcarSettings\\p ~
.\\~ 
Default	\\ �
.
\\� �
MonedaSoles
\\� �
:
\\� �
operacionVenta
\\� �
.
\\� �
IdMoneda
\\� �
==
\\� �
MaestroSettings
\\� �
.
\\� �
Default
\\� �
.
\\� �+
IdDetalleMaestroMonedaDolares
\\� �
?
\\� �
ConcarSettings
\\� �
.
\\� �
Default
\\� �
.
\\� �
MonedaDolarUsa
\\� �
:
\\� �
$str
\\� �
,
\\� �
GlosaPrincipal]] 
=]]  
$str]]! )
+]]* +4
(MapeoComprobanteSigesVsComprobanteConcar]], T
.]]T U
Single]]U [
(]][ \
m]]\ ]
=>]]^ `
m]]a b
.]]b c
Key]]c f
==]]g i
operacionVenta]]j x
.]]x y
IdTipoComprobante	]]y �
)
]]� �
.
]]� �
Value
]]� �
+
]]� �
$str
]]� �
+
]]� �
operacionVenta
]]� �
.
]]� �
SerieComprobante
]]� �
+
]]� �
$str
]]� �
+
]]� �
operacionVenta
]]� �
.
]]� �
NumeroComprobante
]]� �
.
]]� �
ToString
]]� �
(
]]� �
)
]]� �
.
]]� �
ToString
]]� �
(
]]� �
)
]]� �
.
]]� �
PadLeft
]]� �
(
]]� �
$num
]]� �
,
]]� �
$char
]]� �
)
]]� �
+
]]� �
$str
]]� �
+
]]� �6
(MapeoComprobanteSigesVsComprobanteConcar
]]� �
.
]]� �
Single
]]� �
(
]]� �
m
]]� �
=>
]]� �
m
]]� �
.
]]� �
Key
]]� �
==
]]� �
operacionVenta
]]� �
.
]]� �)
IdTipoComprobanteReferencia
]]� �
)
]]� �
.
]]� �
Value
]]� �
+
]]� �
$str
]]� �
+
]]� �
operacionVenta
]]� �
.
]]� �(
SerieComprobanteReferencia
]]� �
+
]]� �
$str
]]� �
+
]]� �
operacionVenta
]]� �
.
]]� �)
NumeroComprobanteReferencia
]]� �
.
]]� �
ToString
]]� �
(
]]� �
)
]]� �
.
]]� �
ToString
]]� �
(
]]� �
)
]]� �
.
]]� �
PadLeft
]]� �
(
]]� �
$num
]]� �
,
]]� �
$char
]]� �
)
]]� �
,
]]� �

TipoCambio^^ 
=^^ 
operacionVenta^^ +
.^^+ ,

TipoCambio^^, 6
,^^6 7
TipoConversion__ 
=__  
ConcarSettings__! /
.__/ 0
Default__0 7
.__7 8
TipoConversionVenta__8 K
,__K L 
FlagConversionMoneda`` $
=``% &
ConcarSettings``' 5
.``5 6
Default``6 =
.``= >-
!FlagConversionMonedaSiSeConvierte``> _
,``_ `
FechaTipoCambioaa 
=aa  !
$straa" $
,aa$ %
CuentaContablebb 
=bb  
ConcarSettingsbb! /
.bb/ 0
Defaultbb0 7
.bb7 8+
CuentaContableFacturasPorCobrarbb8 W
,bbW X
CodigoAnexocc 
=cc 
operacionVentacc ,
.cc, -"
NumeroDocumentoClientecc- C
,ccC D
CodigoCentroCostodd !
=dd" #
$strdd$ &
,dd& '
	DebeHaberee 
=ee 
esDebeee "
?ee# $
ConcarSettingsee% 3
.ee3 4
Defaultee4 ;
.ee; <
Debeee< @
:eeA B
ConcarSettingseeC Q
.eeQ R
DefaulteeR Y
.eeY Z
HabereeZ _
,ee_ `
ImporteOriginalff 
=ff  !
totalff" '
,ff' (
ImporteDolaresgg 
=gg  
$numgg! "
,gg" #
ImporteSoleshh 
=hh 
$numhh  
,hh  !

=ii 
esReferenciaii  ,
?ii- .4
(MapeoComprobanteSigesVsComprobanteConcarii/ W
.iiW X
SingleiiX ^
(ii^ _
mii_ `
=>iia c
miid e
.iie f
Keyiif i
==iij l
operacionVentaiim {
.ii{ |(
IdTipoComprobanteReferencia	ii| �
)
ii� �
.
ii� �
Value
ii� �
:
ii� �6
(MapeoComprobanteSigesVsComprobanteConcar
ii� �
.
ii� �
Single
ii� �
(
ii� �
m
ii� �
=>
ii� �
m
ii� �
.
ii� �
Key
ii� �
==
ii� �
operacionVenta
ii� �
.
ii� �
IdTipoComprobante
ii� �
)
ii� �
.
ii� �
Value
ii� �
,
ii� �
NumeroDocumentojj 
=jj  !
esReferenciajj" .
?jj/ 0
operacionVentajj1 ?
.jj? @&
SerieComprobanteReferenciajj@ Z
+jj[ \
$strjj] `
+jja b
operacionVentajjc q
.jjq r(
NumeroComprobanteReferencia	jjr �
.
jj� �
ToString
jj� �
(
jj� �
)
jj� �
.
jj� �
ToString
jj� �
(
jj� �
)
jj� �
.
jj� �
PadLeft
jj� �
(
jj� �
$num
jj� �
,
jj� �
$char
jj� �
)
jj� �
:
jj� �
operacionVenta
jj� �
.
jj� �
SerieComprobante
jj� �
+
jj� �
$str
jj� �
+
jj� �
operacionVenta
jj� �
.
jj� �
NumeroComprobante
jj� �
.
jj� �
ToString
jj� �
(
jj� �
)
jj� �
.
jj� �
ToString
jj� �
(
jj� �
)
jj� �
.
jj� �
PadLeft
jj� �
(
jj� �
$num
jj� �
,
jj� �
$char
jj� �
)
jj� �
,
jj� �
FechaDocumentokk 
=kk  
operacionVentakk! /
.kk/ 0
FechaDocumentokk0 >
.kk> ?
ToStringkk? G
(kkG H
$strkkH T
)kkT U
,kkU V
FechaVencimientoll  
=ll! "
operacionVentall# 1
.ll1 2
FechaVencimientoll2 B
.llB C
ToStringllC K
(llK L
$strllL X
)llX Y
,llY Z

CodigoAreamm 
=mm 
$strmm 
,mm  
GlosaDetallenn 
=nn 
esReferenciann +
?nn, -
operacionVentann. <
.nn< =2
&NombreTipoTransaccionReferenciaWrappernn= c
:nnd e
operacionVentannf t
.nnt u)
NombreTipoTransaccionWrapper	nnu �
,
nn� �
CodigoAnexoAuxiliaroo #
=oo$ %
$stroo& (
,oo( )
	MedioPagopp 
=pp 
$strpp 
,pp #
TipoDocumentoReferenciaqq '
=qq( )
esNotaCreditoDebitoqq* =
?qq> ?4
(MapeoComprobanteSigesVsComprobanteConcarqq@ h
.qqh i
Singleqqi o
(qqo p
mqqp q
=>qqr t
mqqu v
.qqv w
Keyqqw z
==qq{ }
operacionVenta	qq~ �
.
qq� �)
IdTipoComprobanteReferencia
qq� �
)
qq� �
.
qq� �
Value
qq� �
:
qq� �
$str
qq� �
,
qq� �%
NumeroDocumentoReferenciarr )
=rr* +
esNotaCreditoDebitorr, ?
?rr@ A
operacionVentarrB P
.rrP Q&
SerieComprobanteReferenciarrQ k
+rrl m
$strrrn q
+rrr s
operacionVenta	rrt �
.
rr� �)
NumeroComprobanteReferencia
rr� �
.
rr� �
ToString
rr� �
(
rr� �
)
rr� �
.
rr� �
PadLeft
rr� �
(
rr� �
$num
rr� �
,
rr� �
$char
rr� �
)
rr� �
:
rr� �
$str
rr� �
,
rr� �$
FechaDocumentoReferenciass (
=ss) *
esNotaCreditoDebitoss+ >
?ss? @
operacionVentassA O
.ssO P"
FechaEmisionReferenciassP f
.ssf g
ToStringssg o
(sso p
$strssp |
)ss| }
:ss~ 
$str
ss� �
,
ss� �(
NroMaqRegistradoraTipoDocReftt ,
=tt- .
$strtt/ 1
,tt1 2,
 BaseImponibleDocumentoReferenciauu 0
=uu1 2
esNotaCreditoDebitouu3 F
?uuG H
MathuuI M
.uuM N
RounduuN S
(uuS T
operacionVentauuT b
.uub c#
BaseImponibleReferenciauuc z
,uuz {
$numuu| }
)uu} ~
:	uu �
$num
uu� �
,
uu� �!
IGVDocumentoProvisionvv %
=vv& '
esNotaCreditoDebitovv( ;
?vv< =
(vv> ?
operacionVentavv? M
.vvM N

==vv\ ^
$numvv_ `
?vva b
$numvvc d
:vve f
Mathvvg k
.vvk l
Roundvvl q
(vvq r
operacionVenta	vvr �
.
vv� �

vv� �
,
vv� �
$num
vv� �
)
vv� �
)
vv� �
:
vv� �
$num
vv� �
,
vv� �$
TipoReferenciaenestadoMQww (
=ww) *
$strww+ -
,ww- .'
NumeroSerieCajaRegistradoraxx +
=xx, -
$strxx. 0
,xx0 1
FechaOperacionyy 
=yy  
$stryy! #
,yy# $
TipoTasazz 
=zz 
$strzz 
,zz $
TasaDetraccionPercepcion{{ (
={{) *
$num{{+ ,
,{{, -2
&ImporteBaseDetraccionPercepcionDolares|| 6
=||7 8
$num||9 :
,||: ;0
$ImporteBaseDetraccionPercepcionSoles}} 4
=}}5 6
$num}}7 8
,}}8 9
TipoCambioparaF~~ 
=~~  !
$str~~" $
,~~$ %-
!ImporteIGVSinDerechoCreditoFiscal 1
=2 3
$num4 5
}
�� 
;
��
return
�� 
detalleAsiento
�� !
;
��! "
}
�� 	
private
�� 
List
�� 
<
�� *
DetalleAsientoContableConcar
�� 1
>
��1 2&
ObtenerRegistroCobranzas
��3 K
(
��K L
Periodo
��L S
periodo
��T [
,
��[ \
List
��] a
<
��a b
OperacionVenta
��b p
>
��p q
operacionesVenta��r �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� *
DetalleAsientoContableConcar
�� 1
>
��1 2(
SubDiarioRegistroCobranzas
��3 M
=
��N O
new
��P S
List
��T X
<
��X Y*
DetalleAsientoContableConcar
��Y u
>
��u v
(
��v w
)
��w x
;
��x y
int
�� 
correlativo
�� 
=
��  !
$num
��" #
;
��# $
foreach
�� 
(
�� 
var
�� 
operacionVenta
�� +
in
��, .
operacionesVenta
��/ ?
)
��? @
{
�� 
bool
�� &
esDebeOperacionPrincipal
�� 1
=
��2 3
operacionVenta
��4 B
.
��B C
IdTipoComprobante
��C T
!=
��U W
MaestroSettings
��X g
.
��g h
Default
��h o
.
��o p5
&IdDetalleMaestroComprobanteNotaCredito��p �
;��� �
string
��  
correlativoDetalle
�� -
=
��. /
periodo
��0 7
.
��7 8
mes
��8 ;
+
��< =
correlativo
��> I
.
��I J
ToString
��J R
(
��R S
)
��S T
.
��T U
PadLeft
��U \
(
��\ ]
$num
��] ^
,
��^ _
$char
��` c
)
��c d
;
��d e(
SubDiarioRegistroCobranzas
�� .
.
��. /
Add
��/ 2
(
��2 31
#GenerarDetalleAsientoContableConcar
��3 V
(
��V W
operacionVenta
��W e
,
��e f
ConcarSettings
��g u
.
��u v
Default
��v }
.
��} ~*
SubDiarioPlanillaDeCobranza��~ �
,��� �(
esDebeOperacionPrincipal��� �
,��� �"
correlativoDetalle��� �
,��� �
false��� �
,��� �
ConcarSettings��� �
.��� �
Default��� �
.��� �"
CuentaContableCaja��� �
,��� �
operacionVenta��� �
.��� �
MontoTotalPago��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� 
operacionVenta
�� '
.
��' (
EstaInvalidado
��( 6
)
��6 7
{
�� (
SubDiarioRegistroCobranzas
�� 2
.
��2 3
Add
��3 6
(
��6 71
#GenerarDetalleAsientoContableConcar
��7 Z
(
��Z [
operacionVenta
��[ i
,
��i j
ConcarSettings
��k y
.
��y z
Default��z �
.��� �+
SubDiarioPlanillaDeCobranza��� �
,��� �
!��� �(
esDebeOperacionPrincipal��� �
,��� �"
correlativoDetalle��� �
,��� �
false��� �
,��� �
ConcarSettings��� �
.��� �
Default��� �
.��� �/
CuentaContableFacturasPorCobrar��� �
,��� �
operacionVenta��� �
.��� �
MontoTotalPago��� �
)��� �
)��� �
;��� �
}
�� 
correlativo
�� 
++
�� !
;
��! "
}
�� 
return
�� (
SubDiarioRegistroCobranzas
�� 1
;
��1 2
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ]
,
��] ^
e
��_ `
)
��` a
;
��a b
}
�� 
}
�� 	
private
�� 
List
�� 
<
�� *
DetalleAsientoContableConcar
�� 1
>
��1 2#
ObtenerRegistroVentas
��3 H
(
��H I
Periodo
��I P
periodo
��Q X
,
��X Y
List
��Z ^
<
��^ _
OperacionVenta
��_ m
>
��m n
operacionesVenta
��o 
)�� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� *
DetalleAsientoContableConcar
�� 1
>
��1 2%
SubDiarioRegistroVentas
��3 J
=
��K L
new
��M P
List
��Q U
<
��U V*
DetalleAsientoContableConcar
��V r
>
��r s
(
��s t
)
��t u
;
��u v
int
�� 
correlativo
�� 
=
��  !
$num
��" #
;
��# $
foreach
�� 
(
�� 
var
�� 
operacionVenta
�� +
in
��, .
operacionesVenta
��/ ?
)
��? @
{
�� 
bool
�� &
esDebeOperacionPrincipal
�� 1
=
��2 3
operacionVenta
��4 B
.
��B C
IdTipoComprobante
��C T
!=
��U W
MaestroSettings
��X g
.
��g h
Default
��h o
.
��o p5
&IdDetalleMaestroComprobanteNotaCredito��p �
;��� �
bool
�� !
esNotaCreditoDebito
�� ,
=
��- .
Diccionario
��/ :
.
��: ;D
6TiposDeComprobanteTributablesParaNotasDeCreditoYDebito
��; q
.
��q r
Contains
��r z
(
��z {
operacionVenta��{ �
.��� �!
IdTipoComprobante��� �
)��� �
;��� �
string
��  
correlativoDetalle
�� -
=
��. /
periodo
��0 7
.
��7 8
mes
��8 ;
+
��< =
correlativo
��> I
.
��I J
ToString
��J R
(
��R S
)
��S T
.
��T U
PadLeft
��U \
(
��\ ]
$num
��] ^
,
��^ _
$char
��` c
)
��c d
;
��d e%
SubDiarioRegistroVentas
�� +
.
��+ ,
Add
��, /
(
��/ 01
#GenerarDetalleAsientoContableConcar
��0 S
(
��S T
operacionVenta
��T b
,
��b c
ConcarSettings
��d r
.
��r s
Default
��s z
.
��z {&
SubDiarioRegistroVentas��{ �
,��� �(
esDebeOperacionPrincipal��� �
,��� �"
correlativoDetalle��� �
,��� �#
esNotaCreditoDebito��� �
,��� �
ConcarSettings��� �
.��� �
Default��� �
.��� �/
CuentaContableFacturasPorCobrar��� �
,��� �
operacionVenta��� �
.��� �
ImporteTotal��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� 
operacionVenta
�� '
.
��' (
EstaInvalidado
��( 6
)
��6 7
{
�� 
if
�� 
(
�� 
operacionVenta
�� *
.
��* +
TotalIgv
��+ 3
>
��4 5
$num
��6 7
)
��7 8%
SubDiarioRegistroVentas
�� 3
.
��3 4
Add
��4 7
(
��7 81
#GenerarDetalleAsientoContableConcar
��8 [
(
��[ \
operacionVenta
��\ j
,
��j k
ConcarSettings
��l z
.
��z {
Default��{ �
.��� �'
SubDiarioRegistroVentas��� �
,��� �
!��� �(
esDebeOperacionPrincipal��� �
,��� �"
correlativoDetalle��� �
,��� �#
esNotaCreditoDebito��� �
,��� �
ConcarSettings��� �
.��� �
Default��� �
.��� �!
CuentaContableIgv��� �
,��� �
(��� �
decimal��� �
)��� �
operacionVenta��� �
.��� �
TotalIgv��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 
operacionVenta
�� *
.
��* +
Icbper
��+ 1
>
��2 3
$num
��4 5
)
��5 6%
SubDiarioRegistroVentas
�� 3
.
��3 4
Add
��4 7
(
��7 81
#GenerarDetalleAsientoContableConcar
��8 [
(
��[ \
operacionVenta
��\ j
,
��j k
ConcarSettings
��l z
.
��z {
Default��{ �
.��� �'
SubDiarioRegistroVentas��� �
,��� �
!��� �(
esDebeOperacionPrincipal��� �
,��� �"
correlativoDetalle��� �
,��� �#
esNotaCreditoDebito��� �
,��� �
ConcarSettings��� �
.��� �
Default��� �
.��� �$
CuentaContableIcbper��� �
,��� �
(��� �
decimal��� �
)��� �
operacionVenta��� �
.��� �
Icbper��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 
operacionVenta
�� *
.
��* +
	TotalBien
��+ 4
>
��5 6
$num
��7 8
)
��8 9%
SubDiarioRegistroVentas
�� 3
.
��3 4
Add
��4 7
(
��7 81
#GenerarDetalleAsientoContableConcar
��8 [
(
��[ \
operacionVenta
��\ j
,
��j k
ConcarSettings
��l z
.
��z {
Default��{ �
.��� �'
SubDiarioRegistroVentas��� �
,��� �
!��� �(
esDebeOperacionPrincipal��� �
,��� �"
correlativoDetalle��� �
,��� �#
esNotaCreditoDebito��� �
,��� �
ConcarSettings��� �
.��� �
Default��� �
.��� �-
CuentaContableVentaMercaderia��� �
,��� �
(��� �
decimal��� �
)��� �
operacionVenta��� �
.��� �!
BaseImponibleBien��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 
operacionVenta
�� *
.
��* +

��+ 8
>
��9 :
$num
��; <
)
��< =%
SubDiarioRegistroVentas
�� 3
.
��3 4
Add
��4 7
(
��7 81
#GenerarDetalleAsientoContableConcar
��8 [
(
��[ \
operacionVenta
��\ j
,
��j k
ConcarSettings
��l z
.
��z {
Default��{ �
.��� �'
SubDiarioRegistroVentas��� �
,��� �
!��� �(
esDebeOperacionPrincipal��� �
,��� �"
correlativoDetalle��� �
,��� �#
esNotaCreditoDebito��� �
,��� �
ConcarSettings��� �
.��� �
Default��� �
.��� �+
CuentaContableVentaServicio��� �
,��� �
(��� �
decimal��� �
)��� �
operacionVenta��� �
.��� �%
BaseImponibleServicio��� �
)��� �
)��� �
;��� �
}
�� 
correlativo
�� 
++
�� !
;
��! "
}
�� 
return
�� %
SubDiarioRegistroVentas
�� .
;
��. /
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* [
,
��[ \
e
��] ^
)
��^ _
;
��_ `
}
�� 
}
�� 	
private
�� 
List
�� 
<
�� *
DetalleAsientoContableConcar
�� 1
>
��1 2"
ObtenerRegistroNotas
��3 G
(
��G H
Periodo
��H O
periodo
��P W
,
��W X
List
��Y ]
<
��] ^
OperacionVenta
��^ l
>
��l m
operacionesVenta
��n ~
)
��~ 
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� *
DetalleAsientoContableConcar
�� 1
>
��1 2%
SubDiarioRegistroVentas
��3 J
=
��K L
new
��M P
List
��Q U
<
��U V*
DetalleAsientoContableConcar
��V r
>
��r s
(
��s t
)
��t u
;
��u v
int
�� 
correlativo
�� 
=
��  !
$num
��" #
;
��# $
foreach
�� 
(
�� 
var
�� 
operacionVenta
�� +
in
��, .
operacionesVenta
��/ ?
)
��? @
{
�� 
bool
�� &
esDebeOperacionPrincipal
�� 1
=
��2 3
operacionVenta
��4 B
.
��B C
IdTipoComprobante
��C T
!=
��U W
MaestroSettings
��X g
.
��g h
Default
��h o
.
��o p5
&IdDetalleMaestroComprobanteNotaCredito��p �
;��� �
bool
�� !
esNotaCreditoDebito
�� ,
=
��- .
Diccionario
��/ :
.
��: ;D
6TiposDeComprobanteTributablesParaNotasDeCreditoYDebito
��; q
.
��q r
Contains
��r z
(
��z {
operacionVenta��{ �
.��� �!
IdTipoComprobante��� �
)��� �
;��� �
string
��  
correlativoDetalle
�� -
=
��. /
periodo
��0 7
.
��7 8
mes
��8 ;
+
��< =
correlativo
��> I
.
��I J
ToString
��J R
(
��R S
)
��S T
.
��T U
PadLeft
��U \
(
��\ ]
$num
��] ^
,
��^ _
$char
��` c
)
��c d
;
��d e%
SubDiarioRegistroVentas
�� +
.
��+ ,
Add
��, /
(
��/ 0>
0GenerarDetalleAsientoContableConcarRegistroNotas
��0 `
(
��` a
operacionVenta
��a o
,
��o p
!
��q r'
esDebeOperacionPrincipal��r �
,��� �"
correlativoDetalle��� �
,��� �#
esNotaCreditoDebito��� �
,��� �
false��� �
,��� �
operacionVenta��� �
.��� �
ImporteTotal��� �
)��� �
)��� �
;��� �%
SubDiarioRegistroVentas
�� +
.
��+ ,
Add
��, /
(
��/ 0>
0GenerarDetalleAsientoContableConcarRegistroNotas
��0 `
(
��` a
operacionVenta
��a o
,
��o p'
esDebeOperacionPrincipal��q �
,��� �"
correlativoDetalle��� �
,��� �
!��� �#
esNotaCreditoDebito��� �
,��� �
true��� �
,��� �
operacionVenta��� �
.��� �
ImporteTotal��� �
)��� �
)��� �
;��� �
correlativo
�� 
++
�� !
;
��! "
}
�� 
return
�� %
SubDiarioRegistroVentas
�� .
;
��. /
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* n
,
��n o
e
��p q
)
��q r
;
��r s
}
�� 
}
�� 	
private
�� 
List
�� 
<
�� #
RegistroClienteConcar
�� *
>
��* +%
ObtenerRegistroClientes
��, C
(
��C D
Periodo
��D K
periodo
��L S
)
��S T
{
�� 	
try
�� 
{
�� 
var
�� 
registrosCliente
�� $
=
��% &,
_librosElectronicosConcarDatos
��' E
.
��E F%
ObtenerRegistroClientes
��F ]
(
��] ^
Diccionario
��^ i
.
��i j,
TiposDeComprobanteTributables��j �
,��� �
Diccionario��� �
.��� �S
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones��� �
,��� �
periodo��� �
.��� �

FechaDesde��� �
,��� �
periodo��� �
.��� �

FechaHasta��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
registrosCliente
��  
.
��  !
Remove
��! '
(
��' (
registrosCliente
��( 8
.
��8 9
SingleOrDefault
��9 H
(
��H I
rc
��I K
=>
��L N
rc
��O Q
.
��Q R
Id
��R T
==
��U W

��X e
.
��e f
Default
��f m
.
��m n
IdClienteGenerico
��n 
)�� �
)��� �
;��� �
registrosCliente
��  
.
��  !
ForEach
��! (
(
��( )
m
��) *
=>
��+ -
{
��. /
var
��0 3 
nombreClienteArray
��4 F
=
��G H
m
��I J
.
��J K
Nombre
��K Q
.
��Q R
Split
��R W
(
��W X
$char
��X [
)
��[ \
;
��\ ]
m
��^ _
.
��_ `
Nombre
��` f
=
��g h 
nombreClienteArray
��i {
.
��{ |
Length��| �
==��� �
$num��� �
?��� �
(��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
+��� �
$str��� �
+��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
+��� �
$str��� �
+��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
)��� �
:��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
;��� �
}��� �
)��� �
;��� �
registrosCliente
��  
=
��! "
registrosCliente
��# 3
.
��3 4
OrderBy
��4 ;
(
��; <
c
��< =
=>
��> @
c
��A B
.
��B C
NumeroDocumento
��C R
)
��R S
.
��S T
ToList
��T Z
(
��Z [
)
��[ \
;
��\ ]
List
�� 
<
�� #
RegistroClienteConcar
�� *
>
��* +$
RegistroClientesConcar
��, B
=
��C D
new
��E H
List
��I M
<
��M N#
RegistroClienteConcar
��N c
>
��c d
(
��d e
)
��e f
;
��f g
foreach
�� 
(
�� 
var
�� 
registroCliente
�� ,
in
��- /
registrosCliente
��0 @
)
��@ A
{
�� $
RegistroClientesConcar
�� *
.
��* +
Add
��+ .
(
��. /
new
��/ 2#
RegistroClienteConcar
��3 H
{
�� 
Avanexo
�� 
=
��  !
ConcarSettings
��" 0
.
��0 1
Default
��1 8
.
��8 9,
RegistroClientesConcarAvanexoC
��9 W
,
��W X
Acodane
�� 
=
��  !
registroCliente
��" 1
.
��1 2
NumeroDocumento
��2 A
,
��A B
Adesane
�� 
=
��  !
registroCliente
��" 1
.
��1 2
Nombre
��2 8
,
��8 9
Arefane
�� 
=
��  !
(
��" #
string
��# )
.
��) *

��* 7
(
��7 8
registroCliente
��8 G
.
��G H
	Direccion
��H Q
)
��Q R
||
��S U
registroCliente
��V e
.
��e f
	Direccion
��f o
==
��p r
$str
��s v
)
��v w
?
��x y
ConcarSettings��z �
.��� �
Default��� �
.��� �9
)RegistorClientesConcarArefaneSinDireccion��� �
:��� �
registroCliente��� �
.��� �
	Direccion��� �
,��� �
Aruc
�� 
=
�� 
registroCliente
�� .
.
��. /
NumeroDocumento
��/ >
,
��> ?
Acodmon
�� 
=
��  !
ConcarSettings
��" 0
.
��0 1
Default
��1 8
.
��8 9
MonedaSoles
��9 D
,
��D E
Aestado
�� 
=
��  !
ConcarSettings
��" 0
.
��0 1
Default
��1 8
.
��8 9,
RegistorClientesConcarAestadoV
��9 W
}
�� 
)
�� 
;
�� 
}
�� 
return
�� $
RegistroClientesConcar
�� -
;
��- .
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ]
,
��] ^
e
��_ `
)
��` a
;
��a b
}
�� 
}
�� 	
public
�� $
LibroElectronicoConcar
�� %-
ObtenerLibrosElectronicosConcar
��& E
(
��E F
Periodo
��F M
periodo
��N U
)
��U V
{
�� 	
try
�� 
{
�� 
LibroElectronicoConcar
�� &$
libroElectronicoConcar
��' =
=
��> ?
new
��@ C$
LibroElectronicoConcar
��D Z
(
��Z [
)
��[ \
;
��\ ]
var
�� 
operacionesVenta
�� $
=
��% &,
_librosElectronicosConcarDatos
��' E
.
��E F%
ObtenerOperacionesVenta
��F ]
(
��] ^
Diccionario
��^ i
.
��i j,
TiposDeComprobanteTributables��j �
,��� �
Diccionario��� �
.��� �S
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones��� �
,��� �
periodo��� �
.��� �

FechaDesde��� �
,��� �
periodo��� �
.��� �

FechaHasta��� �
)��� �
.��� �
OrderBy��� �
(��� �
ov��� �
=>��� �
ov��� �
.��� �
FechaEmision��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
operacionesVenta
��  
.
��  !
ForEach
��! (
(
��( )
m
��) *
=>
��+ -
{
��. /
var
��0 3 
nombreClienteArray
��4 F
=
��G H
m
��I J
.
��J K

��K X
.
��X Y
Split
��Y ^
(
��^ _
$char
��_ b
)
��b c
;
��c d
m
��e f
.
��f g

��g t
=
��u v
m
��w x
.
��x y
	IdCliente��y �
==��� �

.��� �
Default��� �
.��� �!
IdClienteGenerico��� �
?��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
:��� �"
nombreClienteArray��� �
.��� �
Length��� �
==��� �
$num��� �
?��� �
(��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
+��� �
$str��� �
+��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
+��� �
$str��� �
+��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
)��� �
:��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
;��� �
}��� �
)��� �
;��� �
var
�� 
cobranzasVenta
�� "
=
��# $,
_librosElectronicosConcarDatos
��% C
.
��C D5
'ObtenerOperacionesIngresoDineroPorVenta
��D k
(
��k l
Diccionario
��l w
.
��w xE
6TiposDeComprobanteDeIngresoDineroPorOperacionesDeVenta��x �
,��� �
Diccionario��� �
.��� �H
8TiposDeTransaccionDeIngresoDeDineroPorOperacionesDeVenta��� �
,��� �
periodo��� �
.��� �

FechaDesde��� �
,��� �
periodo��� �
.��� �

FechaHasta��� �
)��� �
.��� �
OrderBy��� �
(��� �
ov��� �
=>��� �
ov��� �
.��� �
FechaEmision��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
cobranzasVenta
�� 
.
�� 
ForEach
�� &
(
��& '
m
��' (
=>
��) +
{
��, -
var
��. 1 
nombreClienteArray
��2 D
=
��E F
m
��G H
.
��H I

��I V
.
��V W
Split
��W \
(
��\ ]
$char
��] `
)
��` a
;
��a b
m
��c d
.
��d e

��e r
=
��s t
m
��u v
.
��v w
	IdCliente��w �
==��� �

.��� �
Default��� �
.��� �!
IdClienteGenerico��� �
?��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
:��� �"
nombreClienteArray��� �
.��� �
Length��� �
==��� �
$num��� �
?��� �
(��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
+��� �
$str��� �
+��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
+��� �
$str��� �
+��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
)��� �
:��� �"
nombreClienteArray��� �
[��� �
$num��� �
]��� �
;��� �
}��� �
)��� �
;��� �$
libroElectronicoConcar
�� &
.
��& '
RegistroCobranzas
��' 8
=
��9 :&
ObtenerRegistroCobranzas
��; S
(
��S T
periodo
��T [
,
��[ \
cobranzasVenta
��] k
)
��k l
;
��l m$
libroElectronicoConcar
�� &
.
��& '
RegistroVentas
��' 5
=
��6 7#
ObtenerRegistroVentas
��8 M
(
��M N
periodo
��N U
,
��U V
operacionesVenta
��W g
)
��g h
;
��h i
var
�� +
operacionesNotasCreditoDebito
�� 1
=
��2 3
operacionesVenta
��4 D
.
��D E
Where
��E J
(
��J K
o
��K L
=>
��M O
Diccionario
��P [
.
��[ \E
6TiposDeComprobanteTributablesParaNotasDeCreditoYDebito��\ �
.��� �
Contains��� �
(��� �
o��� �
.��� �!
IdTipoComprobante��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �$
libroElectronicoConcar
�� &
.
��& '

��' 4
=
��5 6"
ObtenerRegistroNotas
��7 K
(
��K L
periodo
��L S
,
��S T+
operacionesNotasCreditoDebito
��U r
)
��r s
;
��s t$
libroElectronicoConcar
�� &
.
��& '
RegistroClientes
��' 7
=
��8 9%
ObtenerRegistroClientes
��: Q
(
��Q R
periodo
��R Y
)
��Y Z
;
��Z [
return
�� $
libroElectronicoConcar
�� -
;
��- .
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* a
,
��a b
e
��c d
)
��d e
;
��e f
}
�� 
}
�� 	
}
�� 
}�� �!
OD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Pedido\PedidoReporte_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Pedido# )
{ 
public 

class  
PedidoReporte_Logica %
:& '!
IPedidoReporte_Logica( =
{ 
	protected 
readonly #
IEstablecimiento_Logica 2"
_establecimientoLogica3 I
;I J
	protected 
readonly &
IMaestrosVenta_Repositorio 5
_maestrosVentaDatos6 I
;I J
public  
PedidoReporte_Logica #
(# $#
IEstablecimiento_Logica$ ;"
establecimiento_Logica< R
,R S&
IMaestrosVenta_RepositorioT n
maestrosVentaDatos	o �
)
� �
{ 	"
_establecimientoLogica "
=# $"
establecimiento_Logica% ;
;; <
_maestrosVentaDatos 
=  !
maestrosVentaDatos" 4
;4 5
} 	
public 
PrincipalReportData ",
 ObtenerDatosParaReportePrincipal# C
(C D"
UserProfileSessionDataD Z
profileData[ f
)f g
{ 	
var   *
TieneRolAdministradorDeNegocio   .
=  / 0
profileData  1 <
.  < =
Empleado  = E
.  E F
TieneRol  F N
(  N O

.  \ ]
Default  ] d
.  d e(
idRolAdministradorDeNegocio	  e �
)
  � �
;
  � �
if!! 
(!! 
profileData!! 
.!! (
CentroDeAtencionSeleccionado!! 8
==!!9 ;
null!!< @
&&!!A C
!!!D E*
TieneRolAdministradorDeNegocio!!E c
)!!c d
{"" 
throw## 
new## 
LogicaException## )
(##) *
$str##* j
)##j k
;##k l
}$$ 
var%% +
establecimientosConPuntosVentas%% /
=%%0 1*
TieneRolAdministradorDeNegocio%%2 P
?%%Q R
Establecimiento%%S b
.%%b c
Convert%%c j
(%%j k#
_establecimientoLogica	%%k �
.
%%� �J
<ObtenerEstablecimientosComercialesVigentesConSusPuntosVentas
%%� �
(
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
)
%%� �
:
%%� �
null
%%� �
;
%%� �
var'' 
data'' 
='' 
new'' 
PrincipalReportData'' .
(''. /
)''/ 0
{(( 
FechaActual_)) 
=)) 
DateTimeUtil)) +
.))+ ,
FechaActual)), 7
())7 8
)))8 9
,))9 :
EsAdministrador** 
=**  !*
TieneRolAdministradorDeNegocio**" @
,**@ A
Establecimientos++  
=++! "*
TieneRolAdministradorDeNegocio++# A
?++B C+
establecimientosConPuntosVentas++D c
:++d e
new++f i
List++j n
<++n o
Establecimiento++o ~
>++~ 
(	++ �
)
++� �
{
++� �
profileData
++� �
.
++� �2
$EstablecimientoComercialSeleccionado
++� �
.
++� �
ToEstablecimiento
++� �
(
++� �
)
++� �
}
++� �
},, 
;,,
if-- 
(-- 
!-- *
TieneRolAdministradorDeNegocio-- /
)--/ 0
data--1 5
.--5 6
Establecimientos--6 F
.--F G
SingleOrDefault--G V
(--V W
)--W X
.--X Y
CentrosAtencion--Y h
=--i j
new--k n
List--o s
<--s t
ItemGenerico	--t �
>
--� �
(
--� �
)
--� �
{
--� �
profileData
--� �
.
--� �*
CentroDeAtencionSeleccionado
--� �
.
--� �
ToItemGenerico
--� �
(
--� �
)
--� �
}
--� �
;
--� �
return.. 
data.. 
;.. 
}// 	
}11 
}22 ��
HD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Pedido\Pedido_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Pedido# )
{ 
public 

class 

:  
IPedido_Logica! /
{ 
private 
readonly #
ITransaccionRepositorio 0#
_repositorioTransaccion1 H
;H I
private 
readonly 
IPedidoRepositorio +
_repositorioPedido, >
;> ?
private 
readonly 
IPermisos_Logica )
_permisos_logica* :
;: ;
private 
readonly $
ICodigosOperacion_Logica 1$
_codigosOperacion_Logica2 J
;J K
private 
readonly 
IActorNegocioLogica ,
_actorNegocioLogica- @
;@ A
private 
readonly #
ITransaccionRepositorio 0#
_transaccionRepositorio1 H
;H I
private 
readonly 
IActorRepositorio *
_actorRepositorio+ <
;< =
private   
readonly   
IOperacionLogica   )
_operacionLogica  * :
;  : ;
private!! 
readonly!! $
ICentroDeAtencion_Logica!! 1$
_centroDeAtencion_Logica!!2 J
;!!J K
public"" 

("" #
ITransaccionRepositorio"" 4"
repositorioTransaccion""5 K
,""K L
IPermisos_Logica""M ]
permisos_Logica""^ m
,""m n%
ICodigosOperacion_Logica	""o �%
codigosOperacion_Logica
""� �
,
""� � 
IPedidoRepositorio
""� �
pedidoRepositorio
""� �
,
""� �!
IActorNegocioLogica
""� � 
actorNegocioLogica
""� �
,
""� �%
ITransaccionRepositorio
""� �$
transaccionRepositorio
""� �
,
""� �
IActorRepositorio
""� �
actorRepositorio
""� �
,
""� �
IOperacionLogica
""� �
operacionLogica
""� �
,
""� �&
ICentroDeAtencion_Logica
""� �%
centroDeAtencion_Logica
""� �
)
""� �
{## 	#
_repositorioTransaccion$$ #
=$$$ %"
repositorioTransaccion$$& <
;$$< =$
_codigosOperacion_Logica%% $
=%%% &#
codigosOperacion_Logica%%' >
;%%> ?
_permisos_logica&& 
=&& 
permisos_Logica&& .
;&&. /
_repositorioPedido'' 
=''  
pedidoRepositorio''! 2
;''2 3
_actorNegocioLogica(( 
=((  !
actorNegocioLogica((" 4
;((4 5#
_transaccionRepositorio)) #
=))$ %"
transaccionRepositorio))& <
;))< =
_actorRepositorio** 
=** 
actorRepositorio**  0
;**0 1
_operacionLogica++ 
=++ 
operacionLogica++ .
;++. /$
_centroDeAtencion_Logica,, $
=,,% &#
centroDeAtencion_Logica,,' >
;,,> ?
}-- 	
public.. 
PrincipalPedidoData.. "$
ObetenerDatosParaPedidos..# ;
(..; <"
UserProfileSessionData..< R
profileSessionData..S e
)..e f
{// 	
var00 #
tieneRolCajeroDeNegocio00 '
=00( )
profileSessionData00* <
.00< =
Empleado00= E
.00E F
TieneRol00F N
(00N O

.00\ ]
Default00] d
.00d e
IdRolCajero00e p
)00p q
;00q r
var11 %
tieneRolVendedorDeNegocio11 )
=11* +
profileSessionData11, >
.11> ?
Empleado11? G
.11G H
TieneRol11H P
(11P Q

.11^ _
Default11_ f
.11f g

)11t u
;11u v
var22 
data22 
=22 
new22 
PrincipalPedidoData22 .
(22. /
)22/ 0
{33 
FechaActual44 
=44 
DateTimeUtil44 *
.44* +
FechaActual44+ 6
(446 7
)447 8
,448 9#
TieneRolCajeroDeNegocio55 '
=55( )#
tieneRolCajeroDeNegocio55* A
,55A B%
TieneRolVendedorDeNegocio66 )
=66* +%
tieneRolVendedorDeNegocio66, E
,66E F!
ComprobanteParaPedido77 %
=77& '
(77( )
int77) ,
)77, -
TipoComprobantePara77- @
.77@ A
Pedido77A G
,77G H
}88 
;88
return99 
data99 
;99 
}:: 	
public;; 
void;; *
CalcularDatosDePedidoIntegrada;; 2
(;;2 3
DatosVentaIntegrada;;3 F 
datosPedidoIntegrada;;G [
,;;[ \"
UserProfileSessionData;;] s
profileSessionData	;;t �
)
;;� �
{<< 	 
datosPedidoIntegrada==  
.==  !

===/ 0
DateTimeUtil==1 =
.=== >
FechaActual==> I
(==I J
)==J K
;==K L 
datosPedidoIntegrada>>  
.>>  !
Orden>>! &
.>>& '
FechaEmision>>' 3
=>>4 5 
datosPedidoIntegrada>>6 J
.>>J K

;>>X Y 
datosPedidoIntegrada??  
.??  !
Orden??! &
.??& '
Observacion??' 2
=??3 4
string??5 ;
.??; <

(??I J 
datosPedidoIntegrada??J ^
.??^ _
Orden??_ d
.??d e
Observacion??e p
)??p q
???r s
$str??t }
:??~ 
Regex
??� �
.
??� �
Replace
??� �
(
??� �"
datosPedidoIntegrada
??� �
.
??� �
Orden
??� �
.
??� �
Observacion
??� �
,
??� �
$str
??� �
,
??� �
$str
??� �
)
??� �
;
??� � 
datosPedidoIntegrada@@  
.@@  !
Orden@@! &
.@@& '
PuntoDeVenta@@' 3
=@@4 5
new@@6 9
ItemGenerico@@: F
(@@F G
profileSessionData@@G Y
.@@Y Z(
CentroDeAtencionSeleccionado@@Z v
.@@v w
Id@@w y
)@@y z
;@@z { 
datosPedidoIntegradaAA  
.AA  !
OrdenAA! &
.AA& '
VendedorAA' /
=AA0 1
newAA2 5
ItemGenericoAA6 B
(AAB C
profileSessionDataAAC U
.AAU V
EmpleadoAAV ^
.AA^ _
IdAA_ a
)AAa b
;AAb c
}BB 	
publicCC 
OperationResultCC 

(CC, -
DatosVentaIntegradaCC- @ 
datosPedidoIntegradaCCA U
,CCU V"
UserProfileSessionDataCCW m
profileSessionData	CCn �
)
CC� �
{DD 	
tryEE 
{FF 
OperationResultGG 
resultGG  &
;GG& '
varII 
detallesDePedidoII $
=II% &
ResolverDetalleII' 6
(II6 7 
datosPedidoIntegradaII7 K
.IIK L
OrdenIIL Q
.IIQ R
DetallesIIR Z
,IIZ [ 
datosPedidoIntegradaII\ p
)IIp q
;IIq r
varJJ  
detalles_transaccionJJ (
=JJ) *
detallesDePedidoJJ+ ;
.JJ; <
SelectJJ< B
(JJB C
dJJC D
=>JJE G
dJJH I
.JJI J
DetalleTransaccionJJJ \
(JJ\ ]
)JJ] ^
)JJ^ _
.JJ_ `
ToListJJ` f
(JJf g
)JJg h
;JJh i*
CalcularDatosDePedidoIntegradaKK .
(KK. / 
datosPedidoIntegradaKK/ C
,KKC D
profileSessionDataKKE W
)KKW X
;KKX Y
TransaccionMM 
pedidoMM "
=MM# $

(MM2 3 
datosPedidoIntegradaMM3 G
,MMG H
profileSessionDataMMI [
)MM[ \
;MM\ ]
pedidoNN 
.NN 
ComprobanteNN "
=NN# $1
%GenerarComprobantePropioAutonumerableNN% J
(NNJ K 
datosPedidoIntegradaNNK _
.NN_ `
OrdenNN` e
.NNe f
ComprobanteNNf q
.NNq r
SerieNNr w
.NNw x
IdNNx z
)NNz {
;NN{ |
TransaccionPP 

=PP* + 
GenerarOrdenDePedidoPP, @
(PP@ A
pedidoPPA G
,PPG H
profileSessionDataPPI [
,PP[ \ 
datosPedidoIntegradaPP] q
)PPq r
;PPr s

.QQ 
ComprobanteQQ )
=QQ* +
pedidoQQ, 2
.QQ2 3
ComprobanteQQ3 >
;QQ> ?
Estado_transaccionSS "
estadoDeLaOrdenSS# 2
=SS3 4
newSS5 8
Estado_transaccionSS9 K
(SSK L
profileSessionDataSSL ^
.SS^ _
EmpleadoSS_ g
.SSg h
IdSSh j
,SSj k
MaestroSettingsSSl {
.SS{ |
Default	SS| �
.
SS� �.
 IdDetalleMaestroEstadoRegistrado
SS� �
,
SS� �"
datosPedidoIntegrada
SS� �
.
SS� �
Orden
SS� �
.
SS� �
FechaEmision
SS� �
,
SS� �
$str
SS� �
)
SS� �
;
SS� �

.TT 
Estado_transaccionTT 0
.TT0 1
AddTT1 4
(TT4 5
estadoDeLaOrdenTT5 D
)TTD E
;TTE F
pedidoVV 
.VV 
Transaccion1VV #
.VV# $
AddVV$ '
(VV' (

)VV5 6
;VV6 7
resultXX 
=XX #
_repositorioTransaccionXX 0
.XX0 1
CrearTransaccionXX1 A
(XXA B
pedidoXXB H
)XXH I
;XXI J
resultYY 
.YY 
informationYY "
=YY# $

.YY2 3
idYY3 5
;YY5 6
resultZZ 
.ZZ 
objetoZZ 
=ZZ 
newZZ  #

(ZZ1 2

)ZZ? @
;ZZ@ A
return[[ 
result[[ 
;[[ 
}\\ 
catch]] 
(]] 
	Exception]] 
e]] 
)]] 
{^^ 
throw__ 
new__ 
LogicaException__ )
(__) *
$str__* H
,__H I
e__J K
)__K L
;__L M
}`` 
}aa 	
publiccc 
OperationResultcc 
InvalidarPedidocc .
(cc. /
intcc/ 2

,cc@ A
stringccB H
ObservacionccI T
,ccT U"
UserProfileSessionDataccV l
profileSessionDataccm 
)	cc �
{dd 	
tryee 
{ff 
OperationResultgg 
resultgg  &
=gg' (
newgg) ,
OperationResultgg- <
(gg< =
)gg= >
;gg> ?
Estado_transaccionhh "%
estadoDeLaOrdenInvalidadohh# <
=hh= >
newhh? B
Estado_transaccionhhC U
(hhU V

,hhc d
profileSessionDatahhe w
.hhw x
Empleado	hhx �
.
hh� �
Id
hh� �
,
hh� �
MaestroSettings
hh� �
.
hh� �
Default
hh� �
.
hh� �.
 IdDetalleMaestroEstadoInvalidado
hh� �
,
hh� �
DateTimeUtil
hh� �
.
hh� �
FechaActual
hh� �
(
hh� �
)
hh� �
,
hh� �
Observacion
hh� �
)
hh� �
;
hh� �#
_repositorioTransaccionii '
.ii' ()
CrearEstadoDeTransaccionAhoraii( E
(iiE F%
estadoDeLaOrdenInvalidadoiiF _
)ii_ `
;ii` a
returnjj 
resultjj 
;jj 
}kk 
catchll 
(ll 
	Exceptionll 
exll 
)ll  
{mm 
thrownn 
newnn 
LogicaExceptionnn )
(nn) *
$strnn* H
,nnH I
exnnJ L
)nnL M
;nnM N
}oo 
}pp 	
publicss 
Transaccionss  
GenerarOrdenDePedidoss /
(ss/ 0
Transaccionss0 ;
pedidoss< B
,ssB C"
UserProfileSessionDatassD Z
profileSessionDatass[ m
,ssm n 
DatosVentaIntegrada	sso �
pedidoP
ss� �
)
ss� �
{tt 	
Transaccionxx 

=xx& '
newxx( +
Transaccionxx, 7
(xx7 8$
_codigosOperacion_Logicaxx8 P
.xxP Q1
%ObtenerSiguienteCodigoParaFacturacionxxQ v
(xxv w
pedidoxxw }
.xx} ~
codigo	xx~ �
+
xx� �
$str
xx� �
+
xx� �
Diccionario
xx� �
.
xx� �5
'MapeoTipoTransaccionVsCodigoDeOperacion
xx� �
.
xx� �
Single
xx� �
(
xx� �
mt
xx� �
=>
xx� �
mt
xx� �
.
xx� �
Key
xx� �
==
xx� �
PedidoSettings
xx� �
.
xx� �
Default
xx� �
.
xx� �*
IdTipoTransaccionOrdenPedido
xx� �
)
xx� �
.
xx� �
Value
xx� �
,
xx� �
PedidoSettings
xx� �
.
xx� �
Default
xx� �
.
xx� �*
IdTipoTransaccionOrdenPedido
xx� �
)
xx� �
,
xx� �
null
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �

xx� �
,
xx� �
PedidoSettings
xx� �
.
xx� �
Default
xx� �
.
xx� �*
IdTipoTransaccionOrdenPedido
xx� �
,
xx� �
pedido
xx� �
.
xx� �
id_unidad_negocio
xx� �
,
xx� �
true
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
FechaEmision
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
FechaEmision
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Observacion
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
FechaEmision
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Vendedor
xx� �
.
xx� �
Id
xx� �
,
xx� �
pedido
xx� �
.
xx� �

xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
PuntoDeVenta
xx� �
.
xx� �
Id
xx� �
,
xx� �
pedido
xx� �
.
xx� �
	id_moneda
xx� �
,
xx� �
pedido
xx� �
.
xx� �
tipo_cambio
xx� �
,
xx� �
null
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Cliente
xx� �
.
xx� �
Id
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
DescuentoGlobal
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
DescuentoPorItem
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Anticipo
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Gravada
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
	Exonerada
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Inafecta
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Gratuita
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Igv
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Isc
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
Icbper
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �
OtrosCargos
xx� �
,
xx� �
pedidoP
xx� �
.
xx� �
Orden
xx� �
.
xx� �

xx� �
)
xx� �
;
xx� �

.zz 
AgregarDetalleszz )
(zz) *
pedidoPzz* 1
.zz1 2
Ordenzz2 7
.zz7 8
DetallesDeVentazz8 G
(zzG H
)zzH I
)zzI J
;zzJ K

.|| 
enum1|| 
=||  !
pedidoP||" )
.||) *
Orden||* /
.||/ 0"
HayBienesEnLosDetalles||0 F
(||F G
)||G H
?||I J
(||K L
PedidoSettings||L Z
.||Z [
Default||[ b
.||b c*
MostrarSeccionEntregaEnPedido	||c �
?
||� �
(
||� �
pedidoP
||� �
.
||� �
MovimientoAlmacen
||� �
.
||� �
EntregaDiferida
||� �
?
||� �
(
||� �
int
||� �
)
||� �%
IndicadorImpactoAlmacen
||� �
.
||� �
Diferida
||� �
:
||� �
(
||� �
int
||� �
)
||� �%
IndicadorImpactoAlmacen
||� �
.
||� �
	Inmediata
||� �
)
||� �
:
||� �
(
||� �
int
||� �
)
||� �%
IndicadorImpactoAlmacen
||� �
.
||� �
	Inmediata
||� �
)
||� �
:
||� �
(
||� �
int
||� �
)
||� �%
IndicadorImpactoAlmacen
||� �
.
||� �
NoImpactaNoBienes
||� �
;
||� �
if~~ 
(~~ 
!~~ 
String~~ 
.~~ 

(~~% &
pedidoP~~& -
.~~- .
Orden~~. 3
.~~3 4
Cliente~~4 ;
.~~; <
Alias~~< A
)~~A B
&&~~C E
!~~F G
String~~G M
.~~M N
IsNullOrWhiteSpace~~N `
(~~` a
pedidoP~~a h
.~~h i
Orden~~i n
.~~n o
Cliente~~o v
.~~v w
Alias~~w |
)~~| }
&&	~~~ �
pedidoP
~~� �
.
~~� �
Orden
~~� �
.
~~� �
Cliente
~~� �
.
~~� �
Id
~~� �
==
~~� �

~~� �
.
~~� �
Default
~~� �
.
~~� �
IdClienteGenerico
~~� �
)
~~� �
{ 

�� 
.
�� #
Parametro_transaccion
�� 3
.
��3 4
Add
��4 7
(
��7 8
new
��8 ;#
Parametro_transaccion
��< Q
(
��Q R
MaestroSettings
��R a
.
��a b
Default
��b i
.
��i j4
%IdDetalleMaestroParametroAliasCliente��j �
,��� �
pedidoP��� �
.��� �
Orden��� �
.��� �
Cliente��� �
.��� �
Alias��� �
)��� �
)��� �
;��� �
}
�� 

�� 
.
�� #
Parametro_transaccion
�� /
.
��/ 0
Add
��0 3
(
��3 4
new
��4 7#
Parametro_transaccion
��8 M
(
��M N
MaestroSettings
��N ]
.
��] ^
Default
��^ e
.
��e f?
0IdDetalleMaestroParametroIdTipoComprobanteEmitir��f �
,��� �
pedidoP��� �
.��� �
Orden��� �
.��� �(
IdTipoComprobanteaEmitir��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 
pedidoP
�� 
.
�� 
Orden
�� 
.
�� 
Icbper
�� $
>
��$ %
$num
��% &
)
��& '
{
�� 

�� 
.
�� #
Parametro_transaccion
�� 3
.
��3 4
Add
��4 7
(
��7 8
new
��8 ;#
Parametro_transaccion
��< Q
(
��Q R
MaestroSettings
��R a
.
��a b
Default
��b i
.
��i j>
/IdDetalleMaestroParametroNumeroBolsasDePlastico��j �
,��� �
pedidoP��� �
.��� �
Orden��� �
.��� �&
NumeroBolsasDePlastico��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �

�� 
.
�� #
Parametro_transaccion
�� 3
.
��3 4
Add
��4 7
(
��7 8
new
��8 ;#
Parametro_transaccion
��< Q
(
��Q R
MaestroSettings
��R a
.
��a b
Default
��b i
.
��i j.
IdDetalleMaestroParametroIcbper��j �
,��� �
pedidoP��� �
.��� �
Orden��� �
.��� �
Icbper��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 
pedidoP
�� 
.
�� 
Orden
�� 
.
�� 
UnificarDetalles
�� .
)
��. /
{
�� 

�� 
.
�� #
Parametro_transaccion
�� 3
.
��3 4
Add
��4 7
(
��7 8
new
��8 ;#
Parametro_transaccion
��< Q
(
��Q R
MaestroSettings
��R a
.
��a b
Default
��b i
.
��i j8
)IdDetalleMaestroParametroDetalleUnificado��j �
,��� �
VentasSettings��� �
.��� �
Default��� �
.��� �4
$ActivarDetalleUnificadoPersonalizado��� �
?��� �
pedidoP��� �
.��� �
Orden��� �
.��� �%
ValorDetalleUnificado��� �
:��� �"
AplicacionSettings��� �
.��� �
Default��� �
.��� �%
ValorDetalleUnificado��� �
)��� �
)��� �
;��� �
}
�� 
return
�� 

��  
;
��  !
}
�� 	
public
�� 
Comprobante
�� 3
%GenerarComprobantePropioAutonumerable
�� @
(
��@ A
int
��A D 
idSerieComprobante
��E W
)
��W X
{
�� 	
Serie_comprobante
�� 
serie
�� #
=
��$ %%
_repositorioTransaccion
��& =
.
��= >'
ObtenerSerieDeComprobante
��> W
(
��W X 
idSerieComprobante
��X j
)
��j k
;
��k l
Comprobante
�� 
comprobante
�� #
=
��$ %
new
��& )
Comprobante
��* 5
(
��5 6
serie
��6 ;
.
��; <!
id_tipo_comprobante
��< O
,
��O P
serie
��Q V
.
��V W
id
��W Y
,
��Y Z
serie
��[ `
.
��` a
proximo_numero
��a o
,
��o p
true
��q u
,
��u v
serie
��w |
.
��| }
numero��} �
)��� �
;��� �
serie
�� 
.
�� 
proximo_numero
��  
++
��  "
;
��" #%
_repositorioTransaccion
�� #
.
��# $'
MarcarSerieComoModificada
��$ =
(
��= >
serie
��> C
)
��C D
;
��D E
return
�� 
comprobante
�� 
;
�� 
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
��& '
ResolverDetalle
��( 7
(
��7 8
List
��8 <
<
��< = 
DetalleDeOperacion
��= O
>
��O P
detalles
��Q Y
,
��Y Z!
DatosVentaIntegrada
��[ n"
datosVentaIntegrada��o �
)��� �
{
�� 	
if
�� 
(
�� !
datosVentaIntegrada
�� #
.
��# $
Orden
��$ )
.
��) *
Flete
��* /
>
��0 1
$num
��2 3
)
��3 4
{
�� 
detalles
�� 
.
�� 
Add
�� 
(
�� 
new
��   
DetalleDeOperacion
��! 3
(
��3 4
ConceptoSettings
��4 D
.
��D E
Default
��E L
.
��L M$
IdConceptoNegocioFlete
��M c
,
��c d
$num
��e f
,
��f g!
datosVentaIntegrada
��h {
.
��{ |
Orden��| �
.��� �
Flete��� �
,��� �#
datosVentaIntegrada��� �
.��� �
Orden��� �
.��� �
Flete��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
null��� �
,��� �
null��� �
,��� �
null��� �
,��� �
false��� �
,��� �
VentasSettings��� �
.��� �
Default��� �
.��� �6
&MascaraDeCalculoDeNingunValorCalculado��� �
,��� �
null��� �
)��� �
)��� �
;��� �
}
�� 
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
detalles
��! )
)
��) *
{
�� 
if
�� 
(
�� 
item
�� 
.
�� 
Cantidad
�� !
<=
��" $
$num
��% &
)
��& '
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str
��. }
)
��} ~
;
��~ 
}
�� 
if
�� 
(
�� 
item
�� 
.
�� 
Importe
��  
<
��! "
$num
��# $
)
��$ %
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str
��. b
)
��b c
;
��c d
}
�� 
if
�� 
(
�� 0
"VerificarCalculadoMascaraDeCalculo
�� 6
(
��6 7
item
��7 ;
.
��; <
MascaraDeCalculo
��< L
,
��L M+
ElementoDeCalculoEnVentasEnum
��N k
.
��k l
Cantidad
��l t
)
��t u
)
��u v
{
�� 
item
�� 
.
�� 
Cantidad
�� !
=
��" #
item
��$ (
.
��( )
Importe
��) 0
/
��1 2
item
��3 7
.
��7 8
PrecioUnitario
��8 F
;
��F G
}
�� 
if
�� 
(
�� 0
"VerificarCalculadoMascaraDeCalculo
�� 6
(
��6 7
item
��7 ;
.
��; <
MascaraDeCalculo
��< L
,
��L M+
ElementoDeCalculoEnVentasEnum
��N k
.
��k l
PrecioUnitario
��l z
)
��z {
)
��{ |
{
�� 
item
�� 
.
�� 
PrecioUnitario
�� '
=
��( )
item
��* .
.
��. /
Importe
��/ 6
/
��7 8
item
��9 =
.
��= >
Cantidad
��> F
;
��F G
}
�� 
if
�� 
(
�� 0
"VerificarCalculadoMascaraDeCalculo
�� 6
(
��6 7
item
��7 ;
.
��; <
MascaraDeCalculo
��< L
,
��L M+
ElementoDeCalculoEnVentasEnum
��N k
.
��k l
Importe
��l s
)
��s t
)
��t u
{
�� 
item
�� 
.
�� 
Importe
��  
=
��! "
Math
��# '
.
��' (
Round
��( -
(
��- .
item
��. 2
.
��2 3
Cantidad
��3 ;
*
��< =
item
��> B
.
��B C
PrecioUnitario
��C Q
,
��Q R
$num
��S T
)
��T U
;
��U V
}
�� 
if
�� 
(
�� !
datosVentaIntegrada
�� '
.
��' (
Orden
��( -
.
��- .

��. ;
)
��; <
{
�� 
item
�� 
.
�� 
Igv
�� 
=
�� 
Math
�� #
.
��# $
Round
��$ )
(
��) *
item
��* .
.
��. /
Importe
��/ 6
-
��7 8
(
��9 :
item
��: >
.
��> ?
Importe
��? F
/
��G H
(
��I J
$num
��J K
+
��L M!
TransaccionSettings
��N a
.
��a b
Default
��b i
.
��i j
TasaIGV
��j q
)
��q r
)
��r s
,
��s t
$num
��u v
)
��v w
;
��w x
}
�� 
}
�� 
return
�� 
detalles
�� 
;
�� 
}
�� 	
private
�� 
bool
�� 0
"VerificarCalculadoMascaraDeCalculo
�� 7
(
��7 8
string
��8 >
mascaraDeCalculo
��? O
,
��O P+
ElementoDeCalculoEnVentasEnum
��Q n
orden
��o t
)
��t u
{
�� 	
List
�� 
<
�� 
int
�� 
>
�� #
mascaraDeCalculoArray
�� +
=
��, -
mascaraDeCalculo
��. >
.
��> ?
Select
��? E
(
��E F
digito
��F L
=>
��M O
int
��P S
.
��S T
Parse
��T Y
(
��Y Z
digito
��Z `
.
��` a
ToString
��a i
(
��i j
)
��j k
)
��k l
)
��l m
.
��m n
ToList
��n t
(
��t u
)
��u v
;
��v w
return
�� 
!
�� 
Convert
�� 
.
�� 
	ToBoolean
�� %
(
��% &#
mascaraDeCalculoArray
��& ;
[
��; <
(
��< =
int
��= @
)
��@ A
orden
��A F
]
��F G
)
��G H
;
��H I
}
�� 	
private
�� 
Transaccion
�� 

�� )
(
��) *!
DatosVentaIntegrada
��* =
pedido
��> D
,
��D E$
UserProfileSessionData
��F \ 
profileSessionData
��] o
)
��o p
{
�� 	
try
�� 
{
�� 
int
�� 
idMoneda
�� 
=
�� 
MaestroSettings
�� .
.
��. /
Default
��/ 6
.
��6 7)
IdDetalleMaestroMonedaSoles
��7 R
;
��R S
int
�� 
IdUnidadNegocio
�� #
=
��$ %
MaestroSettings
��& 5
.
��5 6
Default
��6 =
.
��= >8
*IdDetalleMaestroUnidadDeNegocioTransversal
��> h
;
��h i
decimal
�� 

tipoCambio
�� "
=
��# $ 
profileSessionData
��% 7
.
��7 8
TipoDeCambio
��8 D
.
��D E
ValorCompra
��E P
;
��P Q
_permisos_logica
��  
.
��  !

��! .
(
��. / 
profileSessionData
��/ A
.
��A B
Empleado
��B J
.
��J K
Id
��K M
,
��M N
MaestroSettings
��O ^
.
��^ _
Default
��_ f
.
��f g.
IdDetalleMaestroAccionRegistrar��g �
,��� �
PedidoSettings��� �
.��� �
Default��� �
.��� �,
IdTipoTransaccionOrdenPedido��� �
,��� �
IdUnidadNegocio��� �
)��� �
;��� �
	Operacion
�� 
operacionGenerica
�� +
=
��, -
new
��. 1
	Operacion
��2 ;
(
��; <%
_repositorioTransaccion
��< S
.
��S T&
ObtenerUltimaTransaccion
��T l
(
��l m"
TransaccionSettings��m �
.��� �
Default��� �
.��� �*
IdTipoTransaccionOperacion��� �
)��� �
)��� �
;��� �
string
�� 
codigo
�� 
=
�� &
_codigosOperacion_Logica
��  8
.
��8 93
%ObtenerSiguienteCodigoParaFacturacion
��9 ^
(
��^ _
Diccionario
��_ j
.
��j k6
'MapeoTipoTransaccionVsCodigoDeOperacion��k �
.��� �
Single��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Key��� �
==��� �
PedidoSettings��� �
.��� �
Default��� �
.��� �'
IdTipoTransaccionPedido��� �
)��� �
.��� �
Value��� �
,��� �
PedidoSettings��� �
.��� �
Default��� �
.��� �'
IdTipoTransaccionPedido��� �
)��� �
;��� �
Transaccion
�� 
pedidoTransaccion
�� -
=
��. /
new
��0 3
Transaccion
��4 ?
(
��? @
codigo
��@ F
,
��F G
operacionGenerica
��H Y
.
��Y Z
Id
��Z \
,
��\ ]
pedido
��^ d
.
��d e

��e r
,
��r s
PedidoSettings��t �
.��� �
Default��� �
.��� �'
IdTipoTransaccionPedido��� �
,��� �
IdUnidadNegocio��� �
,��� �
true��� �
,��� �
pedido��� �
.��� �
Orden��� �
.��� �
FechaEmision��� �
,��� �
pedido��� �
.��� �
Orden��� �
.��� �
FechaEmision��� �
,��� �
pedido��� �
.��� �
Orden��� �
.��� �
Observacion��� �
,��� �
pedido��� �
.��� �
Orden��� �
.��� �
FechaEmision��� �
,��� �"
profileSessionData��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �
pedido��� �
.��� �
Orden��� �
.��� �
ImporteTotal��� �
,��� �"
profileSessionData��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �
idMoneda��� �
,��� �

tipoCambio��� �
,��� �
null��� �
,��� �
pedido��� �
.��� �
Orden��� �
.��� �
Cliente��� �
.��� �
Id��� �
)��� �
;��� �
return
�� 
pedidoTransaccion
�� (
;
��( )
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* O
,
��O P
ex
��Q S
)
��S T
;
��T U
}
�� 
}
�� 	
public
�� 
List
�� 
<
��  
ResumenOrdenPedido
�� &
>
��& '$
ObtenerOrdenesDePedido
��( >
(
��> ?
DateTime
��? G

FechaDesde
��H R
,
��R S
DateTime
��T \

FechaHasta
��] g
)
��g h
{
�� 	
try
�� 
{
�� 
int
�� 
[
�� 
]
�� 
	idEstados
�� 
=
��  !
new
��" %
int
��& )
[
��) *
]
��* +
{
��+ ,
MaestroSettings
��, ;
.
��; <
Default
��< C
.
��C D.
 IdDetalleMaestroEstadoRegistrado
��D d
,
��d e
MaestroSettings
��f u
.
��u v
Default
��v }
.
��} ~/
 IdDetalleMaestroEstadoInvalidado��~ �
}
�� 
;
�� 
return
��  
_repositorioPedido
�� )
.
��) *#
ObtenerOrdenesPedidos
��* ?
(
��? @

FechaDesde
��@ J
,
��J K

FechaHasta
��L V
,
��V W
PedidoSettings
��X f
.
��f g
Default
��g n
.
��n o+
IdTipoTransaccionOrdenPedido��o �
,��� �
	idEstados��� �
)��� �
.��� �
OrderBy��� �
(��� �
p��� �
=>��� �
p��� �
.��� �
EstaInvalidado��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* G
,
��G H
ex
��I K
)
��K L
;
��L M
}
�� 
}
�� 	
public
�� !
DatosVentaIntegrada
�� ""
ObtenerOrdenDePedido
��# 7
(
��7 8
int
��8 ;
idPedido
��< D
)
��D E
{
�� 	
try
�� 
{
�� 
DatosVentaIntegrada
�� #
PedidoEditar
��$ 0
=
��1 2
new
��3 6!
DatosVentaIntegrada
��7 J
(
��J K
)
��K L
;
��L M
var
�� 

�� !
=
��" # 
_repositorioPedido
��$ 6
.
��6 7"
ObtenerOrdenDePedido
��7 K
(
��K L
idPedido
��L T
)
��T U
;
��U V
PedidoEditar
�� 
.
�� 
Orden
�� "
=
��# $
new
��% (
DatosOrdenVenta
��) 8
(
��8 9
)
��9 :
{
�� 
Id
�� 
=
�� 

�� &
.
��& '
id
��' )
,
��) *(
AplicarIGVCuandoEsAmazonia
�� .
=
��/ 0

��1 >
.
��> ?
importe8
��? G
>
��H I
$num
��J K
,
��K L
Cliente
�� 
=
�� !
_actorNegocioLogica
�� 1
.
��1 2(
ObtenerActorComercialPorId
��2 L
(
��L M

��M Z
.
��Z [
Default
��[ b
.
��b c
IdRolCliente
��c o
,
��o p

��q ~
.
��~ '
id_actor_negocio_externo�� �
)��� �
,��� �
Comprobante
�� 
=
��  !
new
��" %#
ComprobanteDeNegocio_
��& ;
{
�� 
Id
�� 
=
�� 

�� *
.
��* +
id_comprobante
��+ 9
,
��9 :
Tipo
�� 
=
�� 
new
�� "
ItemGenerico
��# /
{
�� 
Id
�� 
=
��  

��! .
.
��. /
Comprobante
��/ :
.
��: ;!
id_tipo_comprobante
��; N
,
��N O
Codigo
�� "
=
��# $

��% 2
.
��2 3
Comprobante
��3 >
.
��> ?
Detalle_maestro
��? N
.
��N O
codigo
��O U
,
��U V
Nombre
�� "
=
��# $

��% 2
.
��2 3
Comprobante
��3 >
.
��> ?
Detalle_maestro
��? N
.
��N O
nombre
��O U
}
�� 
,
�� 
Serie
�� 
=
�� 
new
��  #
SerieComprobante_
��$ 5
{
�� 
Id
�� 
=
��  
(
��! "
int
��" %
)
��% &

��& 3
.
��3 4
Comprobante
��4 ?
.
��? @"
id_serie_comprobante
��@ T
,
��T U
Nombre
�� "
=
��# $

��% 2
.
��2 3
Comprobante
��3 >
.
��> ?
numero_serie
��? K
,
��K L
EsAutonumerica
�� *
=
��+ ,

��- :
.
��: ;
Comprobante
��; F
.
��F G
Serie_comprobante
��G X
.
��X Y
es_autonumerable
��Y i
}
�� 
,
�� 
}
�� 
,
�� 
Detalles
�� 
=
��  
DetalleDeOperacion
�� 1
.
��1 2
Convert
��2 9
(
��9 :

��: G
.
��G H!
Detalle_transaccion
��H [
.
��[ \
Where
��\ a
(
��a b
dp
��b d
=>
��d f
dp
��f h
.
��h i!
id_concepto_negocio
��i |
!=
��}  
ConceptoSettings��� �
.��� �
Default��� �
.��� �&
IdConceptoNegocioFlete��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
,��� �
Flete
�� 
=
�� 

�� )
.
��) *!
Detalle_transaccion
��* =
.
��= >
SingleOrDefault
��> M
(
��M N
f
��N O
=>
��P R
f
��S T
.
��T U!
id_concepto_negocio
��U h
==
��i k
ConceptoSettings
��l |
.
��| }
Default��} �
.��� �&
IdConceptoNegocioFlete��� �
)��� �
==��� �
null��� �
?��� �
$num��� �
:��� �

.��� �#
Detalle_transaccion��� �
.��� �
SingleOrDefault��� �
(��� �
f��� �
=>��� �
f��� �
.��� �#
id_concepto_negocio��� �
==��� � 
ConceptoSettings��� �
.��� �
Default��� �
.��� �&
IdConceptoNegocioFlete��� �
)��� �
.��� �
total��� �
,��� �
Observacion
�� 
=
��  !

��" /
.
��/ 0

comentario
��0 :
,
��: ;
FechaEmision
��  
=
��! "

��# 0
.
��0 1
fecha_inicio
��1 =
,
��= >
Total
�� 
=
�� 

�� )
.
��) *

��* 7
,
��7 8
IdEstado
�� 
=
�� 
(
��  
int
��  #
)
��# $

��$ 1
.
��1 2
id_estado_actual
��2 B
,
��B C 
IdTransaccionPadre
�� &
=
��' (
(
��) *
int
��* -
)
��- .

��. ;
.
��; <"
id_transaccion_padre
��< P
,
��P Q
}
�� 
;
�� 
var
�� 
parametroAlias
�� "
=
��# $

��% 2
.
��2 3#
Parametro_transaccion
��3 H
.
��H I
SingleOrDefault
��I X
(
��X Y
pcn
��Y \
=>
��] _
pcn
��` c
.
��c d
id_parametro
��d p
==
��q s
MaestroSettings��t �
.��� �
Default��� �
.��� �5
%IdDetalleMaestroParametroAliasCliente��� �
)��� �
;��� �
PedidoEditar
�� 
.
�� 
Orden
�� "
.
��" #
Cliente
��# *
.
��* +
Alias
��+ 0
=
��1 2
parametroAlias
��3 A
!=
��B D
null
��E I
?
��J K
Convert
��L S
.
��S T
ToString
��T \
(
��\ ]
parametroAlias
��] k
.
��k l
valor
��l q
)
��q r
:
��s t
$str
��u w
;
��w x
var
�� -
parametroTipoComprobanteAEmitir
�� 3
=
��4 5

��6 C
.
��C D#
Parametro_transaccion
��D Y
.
��Y Z
SingleOrDefault
��Z i
(
��i j
pcn
��j m
=>
��n p
pcn
��q t
.
��t u
id_parametro��u �
==��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �@
0IdDetalleMaestroParametroIdTipoComprobanteEmitir��� �
)��� �
;��� �
PedidoEditar
�� 
.
�� 
Orden
�� "
.
��" #&
IdTipoComprobanteaEmitir
��# ;
=
��< =-
parametroTipoComprobanteAEmitir
��> ]
!=
��^ `
null
��a e
?
��f g
Convert
��h o
.
��o p
ToInt32
��p w
(
��w x.
parametroTipoComprobanteAEmitir��x �
.��� �
valor��� �
)��� �
:��� �
$num��� �
;��� �
var
�� 
ParametroIcbper
�� #
=
��$ %

��& 3
.
��3 4#
Parametro_transaccion
��4 I
.
��I J
SingleOrDefault
��J Y
(
��Y Z
pcn
��Z ]
=>
��^ `
pcn
��a d
.
��d e
id_parametro
��e q
==
��r t
MaestroSettings��u �
.��� �
Default��� �
.��� �/
IdDetalleMaestroParametroIcbper��� �
)��� �
;��� �
PedidoEditar
�� 
.
�� 
Orden
�� "
.
��" #
Icbper
��# )
=
��* +
ParametroIcbper
��, ;
!=
��< >
null
��? C
?
��D E
Convert
��F M
.
��M N
	ToDecimal
��N W
(
��W X
ParametroIcbper
��X g
.
��g h
valor
��h m
)
��m n
:
��o p
$num
��q r
;
��r s
var
�� '
ParametroDetalleUnificado
�� -
=
��. /

��0 =
.
��= >#
Parametro_transaccion
��> S
.
��S T
SingleOrDefault
��T c
(
��c d
pcn
��d g
=>
��h j
pcn
��k n
.
��n o
id_parametro
��o {
==
��| ~
MaestroSettings�� �
.��� �
Default��� �
.��� �9
)IdDetalleMaestroParametroDetalleUnificado��� �
)��� �
;��� �
PedidoEditar
�� 
.
�� 
Orden
�� "
.
��" #
UnificarDetalles
��# 3
=
��4 5'
ParametroDetalleUnificado
��6 O
!=
��P R
null
��S W
?
��X Y
true
��Z ^
:
��_ `
false
��a f
;
��f g
PedidoEditar
�� 
.
�� 
Orden
�� "
.
��" ##
ValorDetalleUnificado
��# 8
=
��9 :'
ParametroDetalleUnificado
��; T
!=
��U W
null
��X \
?
��] ^'
ParametroDetalleUnificado
��_ x
.
��x y
valor
��y ~
:�� �
string��� �
.��� �
Empty��� �
;��� �
PedidoEditar
�� 
.
�� 
MovimientoAlmacen
�� .
=
��/ 0
new
��1 4&
DatosMovimientoDeAlmacen
��5 M
(
��M N
)
��N O
{
�� 
EntregaDiferida
�� #
=
��$ %

��& 3
.
��3 4
enum1
��4 9
==
��: <
$num
��= >
,
��> ?
}
�� 
;
�� 
return
�� 
PedidoEditar
�� #
;
��# $
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* F
,
��F G
ex
��H J
)
��J K
;
��K L
}
�� 
}
�� 	
public
�� 

�� -
ObtenerOrdenDePedidoComprobante
�� <
(
��< =
long
��= A
idPedido
��B J
)
��J K
{
�� 	
try
�� 
{
�� 
return
�� 
(
�� 
new
�� 

�� )
(
��) *%
_transaccionRepositorio
��* A
.
��A BZ
KObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion��B �
(��� �
idPedido��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* M
,
��M N
e
��O P
)
��P Q
;
��Q R
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
EditarPedido
�� +
(
��+ ,!
DatosVentaIntegrada
��, ?"
datosPedidoIntegrada
��@ T
,
��T U$
UserProfileSessionData
��V l 
profileSessionData
��m 
)�� �
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
;
��& '
var
�� 
detallesDePedidos
�� %
=
��& '
ResolverDetalle
��( 7
(
��7 8"
datosPedidoIntegrada
��8 L
.
��L M
Orden
��M R
.
��R S
Detalles
��S [
,
��[ \"
datosPedidoIntegrada
��] q
)
��q r
;
��r s
var
�� "
detalles_transaccion
�� (
=
��) *
detallesDePedidos
��+ <
.
��< =
Select
��= C
(
��C D
d
��D E
=>
��F H
d
��I J
.
��J K 
DetalleTransaccion
��K ]
(
��] ^
)
��^ _
)
��_ `
.
��` a
ToList
��a g
(
��g h
)
��h i
;
��i j,
CalcularDatosDePedidoIntegrada
�� .
(
��. /"
datosPedidoIntegrada
��/ C
,
��C D 
profileSessionData
��E W
)
��W X
;
��X Y
Transaccion
�� 
dbPedido
�� $
=
��% & 
_repositorioPedido
��' 9
.
��9 : 
ObtenerTransaccion
��: L
(
��L M
(
��M N
int
��N Q
)
��Q R"
datosPedidoIntegrada
��R f
.
��f g
Orden
��g l
.
��l m 
IdTransaccionPadre
��m 
)�� �
;��� �
Transaccion
�� 
pedido
�� "
=
��# $

��% 2
(
��2 3"
datosPedidoIntegrada
��3 G
,
��G H 
profileSessionData
��I [
)
��[ \
;
��\ ]
pedido
�� 
.
�� 
id
�� 
=
�� 
(
�� 
int
��  
)
��  !"
datosPedidoIntegrada
��! 5
.
��5 6
Orden
��6 ;
.
��; < 
IdTransaccionPadre
��< N
;
��N O
pedido
�� 
.
�� 
id_comprobante
�� %
=
��& '"
datosPedidoIntegrada
��( <
.
��< =
Orden
��= B
.
��B C
Comprobante
��C N
.
��N O
Id
��O Q
;
��Q R
pedido
�� 
.
�� 
codigo
�� 
=
�� 
dbPedido
��  (
.
��( )
codigo
��) /
;
��/ 0
Transaccion
�� 

�� )
=
��* + 
_repositorioPedido
��, >
.
��> ? 
ObtenerTransaccion
��? Q
(
��Q R
(
��R S
int
��S V
)
��V W"
datosPedidoIntegrada
��W k
.
��k l
Orden
��l q
.
��q r
Id
��r t
)
��t u
;
��u v
Transaccion
�� 
ordenPedido
�� '
=
��( )"
GenerarOrdenDePedido
��* >
(
��> ?
pedido
��? E
,
��E F 
profileSessionData
��G Y
,
��Y Z"
datosPedidoIntegrada
��[ o
)
��o p
;
��p q
ordenPedido
�� 
.
�� 
id
�� 
=
��  "
datosPedidoIntegrada
��! 5
.
��5 6
Orden
��6 ;
.
��; <
Id
��< >
;
��> ?
ordenPedido
�� 
.
�� "
id_transaccion_padre
�� 0
=
��1 2"
datosPedidoIntegrada
��3 G
.
��G H
Orden
��H M
.
��M N 
IdTransaccionPadre
��N `
;
��` a
ordenPedido
�� 
.
�� 
id_comprobante
�� *
=
��+ ,"
datosPedidoIntegrada
��- A
.
��A B
Orden
��B G
.
��G H
Comprobante
��H S
.
��S T
Id
��T V
;
��V W
ordenPedido
�� 
.
�� 
id_estado_actual
�� ,
=
��- ."
datosPedidoIntegrada
��/ C
.
��C D
Orden
��D I
.
��I J
IdEstado
��J R
;
��R S
ordenPedido
�� 
.
�� 
codigo
�� "
=
��# $

��% 2
.
��2 3
codigo
��3 9
;
��9 :
ordenPedido
�� 
.
�� #
Parametro_transaccion
�� 1
.
��1 2
SingleOrDefault
��2 A
(
��A B
pt
��B D
=>
��E G
pt
��H J
.
��J K
id_parametro
��K W
==
��X Z
MaestroSettings
��[ j
.
��j k
Default
��k r
.
��r s?
0IdDetalleMaestroParametroIdTipoComprobanteEmitir��s �
)��� �
.��� �
id��� �
=��� �

.��� �%
Parametro_transaccion��� �
.��� �
SingleOrDefault��� �
(��� �
pt��� �
=>��� �
pt��� �
.��� �
id_parametro��� �
==��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �@
0IdDetalleMaestroParametroIdTipoComprobanteEmitir��� �
)��� �
.��� �
id��� �
;��� �
foreach
�� 
(
�� 
var
�� "
parametroOrdenPedido
�� 1
in
��2 4
ordenPedido
��5 @
.
��@ A#
Parametro_transaccion
��A V
)
��V W
{
�� "
parametroOrdenPedido
�� (
.
��( )
id_transaccion
��) 7
=
��8 9"
datosPedidoIntegrada
��: N
.
��N O
Orden
��O T
.
��T U
Id
��U W
;
��W X"
parametroOrdenPedido
�� (
.
��( )
id
��) +
=
��, -

��. ;
.
��; <#
Parametro_transaccion
��< Q
.
��Q R
SingleOrDefault
��R a
(
��a b
pt
��b d
=>
��e g
pt
��h j
.
��j k
id_parametro
��k w
==
��x z#
parametroOrdenPedido��{ �
.��� �
id_parametro��� �
)��� �
==��� �
null��� �
?��� �
$num��� �
:��� �

.��� �%
Parametro_transaccion��� �
.��� �
SingleOrDefault��� �
(��� �
pt��� �
=>��� �
pt��� �
.��� �
id_parametro��� �
==��� �$
parametroOrdenPedido��� �
.��� �
id_parametro��� �
)��� �
.��� �
id��� �
;��� �
}
�� 
ordenPedido
�� 
.
�� !
Detalle_transaccion
�� /
.
��/ 0
ToList
��0 6
(
��6 7
)
��7 8
.
��8 9
ForEach
��9 @
(
��@ A
d
��A B
=>
��C E
d
��F G
.
��G H
id_transaccion
��H V
=
��W X"
datosPedidoIntegrada
��Y m
.
��m n
Orden
��n s
.
��s t
Id
��t v
)
��v w
;
��w x
pedido
�� 
.
�� 
Transaccion1
�� #
.
��# $
Add
��$ '
(
��' (
ordenPedido
��( 3
)
��3 4
;
��4 5
result
�� 
=
�� %
_repositorioTransaccion
�� 0
.
��0 1@
2ActualizarTransaccionTransaccion1DetallesParametro
��1 c
(
��c d
pedido
��d j
)
��j k
;
��k l
result
�� 
.
�� 
information
�� "
=
��# $
pedido
��% +
.
��+ ,
Transaccion1
��, 8
.
��8 9
First
��9 >
(
��> ?
)
��? @
.
��@ A
id
��A C
;
��C D
ordenPedido
�� 
.
�� 
Comprobante
�� '
=
��( )
new
��* -
Comprobante
��. 9
(
��9 :
)
��: ;
;
��; <
result
�� 
.
�� 
objeto
�� 
=
�� 
new
��  #

��$ 1
(
��1 2
ordenPedido
��2 =
)
��= >
;
��> ?
return
�� 
result
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* L
,
��L M
ex
��N P
)
��P Q
;
��Q R
}
�� 
}
�� 	
public
�� 

�� .
 ObtenerOrdenDePedidoParaImprimir
�� =
(
��= >

��> K

��L Y
)
��Y Z
{
�� 	
try
�� 
{
�� 
int
�� 
[
�� 
]
�� 
idsActoresNegocio
�� '
=
��( )
{
��* +

��, 9
.
��9 :
Transaccion
��: E
(
��E F
)
��F G
.
��G H&
id_actor_negocio_interno
��H `
}
��a b
;
��b c
List
�� 
<
�� 

�� "
>
��" #
actoresDeNegocio
��$ 4
=
��5 6
_actorRepositorio
��7 H
.
��H I%
ObtenerActoresDeNegocio
��I `
(
��` a
idsActoresNegocio
��a r
)
��r s
.
��s t
ToList
��t z
(
��z {
)
��{ |
;
��| }

�� 
.
�� 
Transaccion
�� )
(
��) *
)
��* +
.
��+ ,
Actor_negocio2
��, :
=
��; <
actoresDeNegocio
��= M
.
��M N
Single
��N T
(
��T U
an
��U W
=>
��X Z
an
��[ ]
.
��] ^
id
��^ `
==
��a c

��d q
.
��q r
Transaccion
��r }
(
��} ~
)
��~ 
.�� �(
id_actor_negocio_interno��� �
)��� �
;��� �
return
�� 

�� $
;
��$ %
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* M
,
��M N
e
��O P
)
��P Q
;
��Q R
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
ConfirmarPedido
�� .
(
��. /
ModoOperacionEnum
��/ @
	tipoVenta
��A J
,
��J K$
UserProfileSessionData
��K a
sesionDeUsuario
��b q
,
��r s"
DatosVentaIntegrada��t �
pedido��� �
)��� �
{
�� 	
try
�� 
{
�� 
pedido
�� 
.
�� 
NuevoEstado
�� "
=
��# $
new
��% ( 
Estado_transaccion
��) ;
(
��; <
pedido
��< B
.
��B C
Orden
��C H
.
��H I
Id
��I K
,
��K L
sesionDeUsuario
��L [
.
��[ \
Empleado
��\ d
.
��d e
Id
��e g
,
��g h
MaestroSettings
��i x
.
��x y
Default��y �
.��� �0
 IdDetalleMaestroEstadoConfirmado��� �
,��� �
DateTimeUtil��� �
.��� �
FechaActual��� �
(��� �
)��� �
,��� �
$str��� �
)��� �
;��� �
return
�� 
_operacionLogica
�� '
.
��' (%
ConfirmarVentaIntegrada
��( ?
(
��? @
	tipoVenta
��@ I
,
��I J
sesionDeUsuario
��K Z
,
��Z [
pedido
��\ b
)
��b c
;
��c d
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* F
,
��F G
ex
��H J
)
��J K
;
��K L
}
�� 
}
�� 	
public
�� 
List
�� 
<
��  
PedidosInvalidados
�� &
>
��& '.
 ObtenerReportePedidosInvalidados
��( H
(
��H I
DateTime
��I Q

fechaDesde
��R \
,
��\ ]
DateTime
��^ f

fechaHasta
��g q
,
��q r
int
��s v
[
��v w
]
��w x
idsPuntosVenta��y �
,��� �
bool��� �#
todasLosPuntosVenta��� �
,��� �
int��� �*
idEstablecimientoComercial��� �
)��� �
{
�� 	
List
�� 
<
��  
PedidosInvalidados
�� #
>
��# $ 
pedidosInvalidados
��% 7
=
��8 9
new
��: =
List
��> B
<
��B C 
PedidosInvalidados
��C U
>
��U V
(
��V W
)
��W X
;
��X Y
idsPuntosVenta
�� 
=
�� !
todasLosPuntosVenta
�� 0
?
��1 2&
_centroDeAtencion_Logica
��3 K
.
��K LF
7ObtenerPuntosDeVentaVigentesPorEstablecimientoComercial��L �
(��� �*
idEstablecimientoComercial��� �
)��� �
.��� �
Select��� �
(��� �
pv��� �
=>��� �
pv��� �
.��� �
Id��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
:��� �
idsPuntosVenta��� �
;��� � 
pedidosInvalidados
�� 
=
��   
_repositorioPedido
��! 3
.
��3 4/
!ObtenerOrdenesDePedidoInvalidados
��4 U
(
��U V

fechaDesde
��V `
,
��` a

fechaHasta
��b l
,
��l m
idsPuntosVenta
��n |
)
��| }
.
��} ~
ToList��~ �
(��� �
)��� �
;��� �
return
��  
pedidosInvalidados
�� %
;
��% &
}
�� 	
}
�� 
}�� �P
LD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Permisos\Permisos_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Permisos# +
{ 
public

class
Permisos_Logica
:
IPermisos_Logica
{ 
	protected 
readonly 
IRoles_Repositorio -
_rolesDatos. 9
;9 :
public 
Permisos_Logica 
( 
IRoles_Repositorio 1

rolesDatos2 <
)< =
{ 	
_rolesDatos 
= 

rolesDatos $
;$ %
} 	
public 
void 

(! "
int" %

idEmpleado& 0
,0 1
int2 5
idAccion6 >
,> ?
int@ C
idTipoTransaccionD U
,U V
intW Z
idUnidadNegocio[ j
)j k
{ 	
var 
intersectedActions "
=# $
_rolesDatos% 0
.0 1C
7ContarInterseccionRolesAccion_RolesVigentesActorNegocio1 h
(h i

idEmpleadoi s
,s t

.
� �
Default
� �
.
� �

� �
,
� �
idAccion
� �
,
� �
idTipoTransaccion
� �
,
� �
idUnidadNegocio
� �
)
� �
;
� �
if 
( 
intersectedActions "
<=# %
$num& '
)' (
{ 
throw 
new 
LogicaException )
() *
$str* Y
)Y Z
;Z [
} 
} 	
public!! 
bool!! .
"ValidarActorNegocioTieneRolVigente!! 6
(!!6 7
int!!7 :

idEmpleado!!; E
,!!E F
int!!G J
idRol!!K P
)!!P Q
{"" 	
return## 
_rolesDatos## 
.## '
ActorNegocioTieneRolVigente## :
(##: ;

idEmpleado##; E
,##E F
idRol##G L
)##L M
;##M N
}$$ 	
public&& 
List&& 
<&& 
Rol&& 
>&& 
ObtenerRolesTercero&& ,
(&&, -
)&&- .
{'' 	
try(( 
{)) 
int** 
[** 
]** 
idsRoles** 
=**  
{**! "

.**0 1
Default**1 8
.**8 9
IdRolCliente**9 E
,**E F

.**T U
Default**U \
.**\ ]
IdRolProveedor**] k
}**l m
;**m n
return++ 
_rolesDatos++ "
.++" #
ObtenerRoles++# /
(++/ 0
idsRoles++0 8
)++8 9
.++9 :
ToList++: @
(++@ A
)++A B
;++B C
},, 
catch-- 
(-- 
	Exception-- 
e-- 
)-- 
{.. 
throw// 
e// 
;// 
}00 
}11 	
public33 
List33 
<33 
Tipo_transaccion33 $
>33$ %%
ObtenerTiposDeTransaccion33& ?
(33? @
)33@ A
{44 	
try55 
{66 
return77 
_rolesDatos77 "
.77" #'
ObtenerTiposDeTransacciones77# >
(77> ?
)77? @
.77@ A
ToList77A G
(77G H
)77H I
;77I J
}88 
catch99 
(99 
	Exception99 
e99 
)99 
{:: 
throw;; 
e;; 
;;; 
}<< 
}== 	
public?? 
List?? 
<?? 
Accion_por_rol?? "
>??" #A
5ObtenerAccionesPosiblesPorTipoTransaccionYRolPersonal??$ Y
(??Y Z
int??Z ]
idTipoTransaccion??^ o
,??o p
int??q t

)
??� �
{@@ 	
tryAA 
{BB 
returnCC 
_rolesDatosCC "
.CC" #0
$obtenerAccionesPorTipoTrasaccionYRolCC# G
(CCG H
idTipoTransaccionCCH Y
,CCY Z

)CCh i
.CCi j
ToListCCj p
(CCp q
)CCq r
;CCr s
}DD 
catchEE 
(EE 
	ExceptionEE 
eEE 
)EE 
{FF 
throwGG 
eGG 
;GG 
}HH 
}II 	
publicKK 
ListKK 
<KK 
Accion_por_estadoKK %
>KK% &B
6ObtenerAccionesPosiblesPorTipoTransaccionYEstadoActualKK' ]
(KK] ^
intKK^ a
idTipoTransaccionKKb s
,KKs t
intKKu x
idEstadoActual	KKy �
)
KK� �
{LL 	
tryMM 
{NN 
returnOO 
_rolesDatosOO "
.OO" #9
-obtenerAccionesPorTipoTrasaccionYEstadoActualOO# P
(OOP Q
idTipoTransaccionOOQ b
,OOb c
idEstadoActualOOd r
)OOr s
.OOs t
ToListOOt z
(OOz {
)OO{ |
;OO| }
}PP 
catchQQ 
(QQ 
	ExceptionQQ 
eQQ 
)QQ 
{RR 
throwSS 
eSS 
;SS 
}TT 
}UU 	
publicWW 
OperationResultWW +
ActualizarPermisosPorRolYEstadoWW >
(WW> ?
ListWW? C
<WWC D
Accion_por_rolWWD R
>WWR S
rolAccionesWWT _
,WW_ `
ListWWa e
<WWe f
Accion_por_estadoWWf w
>WWw x
estadoAcciones	WWy �
,
WW� �
int
WW� �
idTipoTransaccion
WW� �
,
WW� �
int
WW� �

WW� �
,
WW� �
int
WW� �
idEstadoActual
WW� �
)
WW� �
{XX 	
tryYY 
{ZZ 
if[[ 
([[ 
rolAcciones[[ 
!=[[  "
null[[# '
)[[' (
{\\ 
List]] 
<]] 
Accion_por_rol]] '
>]]' (
rolAccionesNuevos]]) :
=]]; <
rolAcciones]]= H
.]]H I
Where]]I N
(]]N O
ra]]O Q
=>]]R T
ra]]U W
.]]W X
id]]X Z
<=]][ ]
$num]]^ _
)]]_ `
.]]` a
ToList]]a g
(]]g h
)]]h i
;]]i j
foreach__ 
(__ 
var__  
rolAccionNuevo__! /
in__0 2
rolAccionesNuevos__3 D
)__D E
{`` 
ifaa 
(aa 
rolAccionesNuevosaa -
.aa- .
Countaa. 3
(aa3 4
raaa4 6
=>aa7 9
raaa: <
.aa< =
id_tipo_transaccionaa= P
==aaQ S
rolAccionNuevoaaT b
.aab c
id_tipo_transaccionaac v
&&aaw y
raaaz |
.aa| }
id_rol	aa} �
==
aa� �
rolAccionNuevo
aa� �
.
aa� �
id_rol
aa� �
&&
aa� �
ra
aa� �
.
aa� �
id_accion_posible
aa� �
==
aa� �
rolAccionNuevo
aa� �
.
aa� �
id_accion_posible
aa� �
&&
aa� �
ra
aa� �
.
aa� �
id_unidad_negocio
aa� �
==
aa� �
rolAccionNuevo
aa� �
.
aa� �
id_unidad_negocio
aa� �
)
aa� �
>
aa� �
$num
aa� �
)
aa� �
{bb 
returncc "
newcc# &
OperationResultcc' 6
(cc6 7
newcc7 :
	Exceptioncc; D
(ccD E
$str	ccE �
)
cc� �
)
cc� �
;
cc� �
}dd 
}ee 
}ff 
ifgg 
(gg 
estadoAccionesgg "
!=gg# %
nullgg& *
)gg* +
{hh 
Listii 
<ii 
Accion_por_estadoii *
>ii* + 
estadoAccionesNuevosii, @
=iiA B
estadoAccionesiiC Q
.iiQ R
WhereiiR W
(iiW X
raiiX Z
=>ii[ ]
raii^ `
.ii` a
idiia c
<=iid f
$numiig h
)iih i
.iii j
ToListiij p
(iip q
)iiq r
;iir s
foreachkk 
(kk 
varkk  
estadoAccionNuevokk! 2
inkk3 5 
estadoAccionesNuevoskk6 J
)kkJ K
{ll 
ifmm 
(mm  
estadoAccionesNuevosmm 0
.mm0 1
Countmm1 6
(mm6 7
eamm7 9
=>mm: <
eamm= ?
.mm? @
id_tipo_transaccionmm@ S
==mmT V
estadoAccionNuevommW h
.mmh i
id_tipo_transaccionmmi |
&&mm} 
ea
mm� �
.
mm� �
id_estado_actual
mm� �
==
mm� �
estadoAccionNuevo
mm� �
.
mm� �
id_estado_actual
mm� �
&&
mm� �
ea
mm� �
.
mm� �
id_accion_posible
mm� �
==
mm� �
estadoAccionNuevo
mm� �
.
mm� �
id_accion_posible
mm� �
)
mm� �
>
mm� �
$num
mm� �
)
mm� �
{nn 
returnoo "
newoo# &
OperationResultoo' 6
(oo6 7
newoo7 :
	Exceptionoo; D
(ooD E
$str	ooE �
)
oo� �
)
oo� �
;
oo� �
}pp 
}qq 
}rr 
returntt 
_rolesDatostt "
.tt" #+
actualizarAccionesPorRolYEstadott# B
(ttB C
rolAccionesttC N
,ttN O
estadoAccionesttP ^
,tt^ _
idTipoTransacciontt` q
,ttq r

,
tt� �
idEstadoActual
tt� �
)
tt� �
;
tt� �
}uu 
catchvv 
(vv 
	Exceptionvv 
evv 
)vv 
{ww 
throwxx 
exx 
;xx 
}yy 
}zz 	
}{{ 
}|| �
KD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\CodigosOperacion_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Permisos# +
{ 
public		 

class		 #
CodigosOperacion_Logica		 (
:		( )$
ICodigosOperacion_Logica		* B
{

 
	protected 
readonly +
ICodigosTransaccion_Repositorio :$
_codigosTransaccionDatos; S
;S T
public #
CodigosOperacion_Logica &
(& '+
ICodigosTransaccion_Repositorio' F#
codigosTransaccionDatosG ^
)^ _
{ 	$
_codigosTransaccionDatos $
=% &#
codigosTransaccionDatos' >
;> ?
} 	
public 
string 1
%ObtenerSiguienteCodigoParaFacturacion ;
(; <
string< B
prefijoC J
,J K
intL O
idTipoTransaccionP a
)a b
{ 	
try 
{ 
int 
maximo 
= $
_codigosTransaccionDatos 5
.5 6.
"ObtenerMaximoCodigoParaTransaccion6 X
(X Y
prefijoY `
,` a
idTipoTransaccionb s
)s t
;t u
string *
siguienteCodigoParaFacturacion 5
=6 7
prefijo8 ?
+@ A
(B C
maximoC I
+J K
$numL M
)M N
.N O
ToStringO W
(W X
)X Y
;Y Z
return *
siguienteCodigoParaFacturacion 5
;5 6
} 
catch 
( 
	Exception 
e 
) 
{ 
throw 
new 
LogicaException )
() *
$str* [
,[ \
e] ^
)^ _
;_ `
} 
}   	
public!! 
int!! .
"ObtenerMaximoCodigoParaTransaccion!! 5
(!!5 6
string!!6 <
prefijo!!= D
,!!D E
int!!F I
idTipoTransaccion!!J [
)!![ \
{"" 	
return## $
_codigosTransaccionDatos## +
.##+ ,.
"ObtenerMaximoCodigoParaTransaccion##, N
(##N O
prefijo##O V
,##V W
idTipoTransaccion##X i
)##i j
;##j k
}$$ 	
})) 
}** �9
JD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Session\Session_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Permisos# +
{ 
public 

class 
Session_Logica 
:  !
ISession_Logica" 1
{ 
	protected 
readonly 
IRoles_Repositorio -
_rolesDatos. 9
;9 :
	protected 
readonly !
IEmpleado_Repositorio 0
_empleadoDatos1 ?
;? @
	protected 
readonly $
ICentroDeAtencion_Logica 3#
_centroDeAtencionLogica4 K
;K L
	protected 
readonly 
ISede_Logica '
_sedeLogica( 3
;3 4
public 
Session_Logica 
( 
IRoles_Repositorio 0

rolesDatos1 ;
,; <!
IEmpleado_Repositorio= R

,` a$
ICentroDeAtencion_Logicab z#
centroDeAtencionLogica	{ �
,
� �
ISede_Logica
� �

sedeLogica
� �
)
� �
{ 	
_rolesDatos 
= 

rolesDatos $
;$ %
_empleadoDatos 
= 

;* +#
_centroDeAtencionLogica #
=$ %"
centroDeAtencionLogica& <
;< =
_sedeLogica   
=   

sedeLogica   $
;  $ %
}!! 	
public$$ "
UserProfileSessionData$$ % 
GenerarSesionUsuario$$& :
($$: ;
string$$; A
userId$$B H
,$$H I
string$$J P
userName$$Q Y
)$$Y Z
{%% 	
	Empleado_&& 
empleado&& 
=&&  
_empleadoDatos&&! /
.&&/ 0
ObtenerEmpleado&&0 ?
(&&? @
userId&&@ F
)&&F G
;&&G H
List'' 
<'' 
CentroDeAtencion'' !
>''! "(
centrosDeAtencionProgramados''# ?
=''@ A#
_centroDeAtencionLogica''B Y
.''Y Z0
$ObtenerCentrosDeAtencionProgramados_''Z ~
(''~ 
empleado	'' �
.
''� �
Id
''� �
)
''� �
;
''� �
var)) 
profile)) 
=)) 
new)) "
UserProfileSessionData)) 4
())4 5
)))5 6
{))7 8
	IdUsuario))9 B
=))C D
userId))E K
,))K L

=))[ \
userName))] e
,))e f)
CentrosDeAtencionProgramados	))g �
=
))� �*
centrosDeAtencionProgramados
))� �
}
))� �
;
))� �
profile** 
.** 
Empleado** 
=** 
empleado** '
??**( *
throw**+ 0
new**1 4
	Exception**5 >
(**> ?
$str	**? �
)
**� �
;
**� �
profile++ 
.++ +
SetCentrosDeAtencionProgramados++ 3
(++3 4(
centrosDeAtencionProgramados++4 P
)++P Q
;++Q R
if,, 
(,, 
profile,, 
.,, (
CentrosDeAtencionProgramados,, 4
.,,4 5
Count,,5 :
==,,; =
$num,,> ?
),,? @
profile,,A H
.,,H I(
CentroDeAtencionSeleccionado,,I e
=,,f g
profile,,h o
.,,o p)
CentrosDeAtencionProgramados	,,p �
.
,,� �
FirstOrDefault
,,� �
(
,,� �
)
,,� �
;
,,� �
return-- 
profile-- 
;-- 
}.. 	
public44 "
UserProfileSessionData44 % 
GenerarSesionUsuario44& :
(44: ;
)44; <
{55 	
	Empleado_66 
empleado66 
=66  
_empleadoDatos66! /
.66/ 0
ObtenerEmpleado660 ?
(66? @

.66M N
Default66N U
.66U V,
 IdActorNegocioEmpleadoPorDefecto66V v
)66v w
;66w x
List77 
<77 
CentroDeAtencion77 !
>77! "(
centrosDeAtencionProgramados77# ?
=77@ A#
_centroDeAtencionLogica77B Y
.77Y Z0
$ObtenerCentrosDeAtencionProgramados_77Z ~
(77~ 
empleado	77 �
.
77� �
Id
77� �
)
77� �
;
77� �
var99 
profile99 
=99 
new99 "
UserProfileSessionData99 4
(994 5
)995 6
{997 8
	IdUsuario999 B
=99C D
$str99E G
,99G H

=99W X
$str99Y b
,99b c)
CentrosDeAtencionProgramados	99d �
=
99� �*
centrosDeAtencionProgramados
99� �
}
99� �
;
99� �
profile:: 
.:: 
Empleado:: 
=:: 
empleado:: '
??::( *
throw::+ 0
new::1 4
	Exception::5 >
(::> ?
$str	::? �
)
::� �
;
::� �
profile;; 
.;; +
SetCentrosDeAtencionProgramados;; 3
(;;3 4(
centrosDeAtencionProgramados;;4 P
);;P Q
;;;Q R
if<< 
(<< 
profile<< 
.<< (
CentrosDeAtencionProgramados<< 4
.<<4 5
Count<<5 :
==<<; =
$num<<> ?
)<<? @
profile<<A H
.<<H I(
CentroDeAtencionSeleccionado<<I e
=<<f g
profile<<h o
.<<o p)
CentrosDeAtencionProgramados	<<p �
.
<<� �
FirstOrDefault
<<� �
(
<<� �
)
<<� �
;
<<� �
return== 
profile== 
;== 
}>> 	
publicAA "
UserProfileSessionDataAA %
ResolverSessionAA& 5
(AA5 6"
UserProfileSessionDataAA6 L
profileDataAAM X
,AAX Y

TipoCambioAAZ d

tipoCambioAAe o
,AAo p
intAAq t+
idCentroDeAtencionInicioSesion	AAu �
)
AA� �
{BB 	
profileDataCC 
.CC 
SedeCC 
=CC 
_sedeLogicaCC *
.CC* +
ObtenerSedeConLogoCC+ =
(CC= >
)CC> ?
;CC? @
profileDataDD 
.DD (
CentroDeAtencionSeleccionadoDD 4
=DD5 6
newDD7 :
CentroDeAtencionDD; K
(DDK L
)DDL M
{EE 
IdFF 
=FF *
idCentroDeAtencionInicioSesionFF 3
,FF3 4
}GG 
;GG
profileDataHH 
.HH *
IdCentroDeAtencionInicioSesionHH 6
=HH7 8*
idCentroDeAtencionInicioSesionHH9 W
;HHW X
profileDataII 
.II 4
(IdCentroAtencionQueTieneElStockIntegradaII @
=IIA B*
idCentroDeAtencionInicioSesionIIC a
;IIa b
profileDataJJ 
.JJ 
TipoDeCambioJJ $
=JJ% &

tipoCambioJJ' 1
;JJ1 2
returnKK 
profileDataKK 
;KK 
}LL 	
}MM 
}NN �
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\TipoDeCambio\TipoDeCambio_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
TipoDeCambio# /
{ 
public

class
TipoDeCambio_Logica
:
ITipoDeCambio_Logica
{ 
public 

TipoCambio (
ObtenerTipoCambioDolarActual 6
(6 7
)7 8
{ 	

TipoCambio 

tipoCambio !
=" #
new$ '

TipoCambio( 2
(2 3
)3 4
;4 5
try 
{ 
string 
url 
= 
AplicacionSettings /
./ 0
Default0 7
.7 8$
UrlApiConsultaTipoCambio8 P
+Q R
AplicacionSettingsS e
.e f
Defaultf m
.m n'
IdMonedaDolarApiTipoCambio	n �
;
� �
var 
response 
= 
Task #
.# $
Run$ '
(' (
(( )
)) *
=>+ -
GetURI. 4
(4 5
new5 8
Uri9 <
(< =
url= @
)@ A
)A B
)B C
;C D
response 
. 
Wait 
( 
) 
;  
string 
result 
= 
response  (
.( )
Result) /
;/ 0

tipoCambio 
= 

Newtonsoft '
.' (
Json( ,
., -
JsonConvert- 8
.8 9
DeserializeObject9 J
<J K

TipoCambioK U
>U V
(V W
resultW ]
)] ^
;^ _

tipoCambio 
. 
IdMoneda #
=$ %
MaestroSettings& 5
.5 6
Default6 =
.= >)
IdDetalleMaestroMonedaDolares> [
;[ \
if 
( 

tipoCambio 
. 
Estado %
==& (
false) .
). /
{ 

tipoCambio 
=  
new! $

TipoCambio% /
(/ 0
)0 1
;1 2
} 
} 
catch 
( 
	Exception 
) 
{   

tipoCambio!! 
=!! 
new!!  

TipoCambio!!! +
(!!+ ,
)!!, -
;!!- .
}"" 
return## 

tipoCambio## 
;## 
}$$ 	
	protected&& 
static&& 
async&& 
Task&& #
<&&# $
string&&$ *
>&&* +
GetURI&&, 2
(&&2 3
Uri&&3 6
u&&7 8
)&&8 9
{'' 	
var(( 
response(( 
=(( 
string(( !
.((! "
Empty((" '
;((' (
using)) 
()) 
var)) 
client)) 
=)) 
new))  #

HttpClient))$ .
()). /
)))/ 0
)))0 1
{** 
HttpResponseMessage++ #
result++$ *
=+++ ,
await++- 2
client++3 9
.++9 :
GetAsync++: B
(++B C
u++C D
)++D E
;++E F
if,, 
(,, 
result,, 
.,, 
IsSuccessStatusCode,, .
),,. /
{-- 
response.. 
=.. 
await.. $
result..% +
...+ ,
Content.., 3
...3 4
ReadAsStringAsync..4 E
(..E F
)..F G
;..G H
}// 
}00 
return11 
response11 
;11 
}22 	
}44 
}55 ��
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Venta\VentaReporte_Logica.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
. 
Core "
." #
Venta# (
{ 
public 

class 
VentaReporte_Logica $
:% & 
IVentaReporte_Logica' ;
{ 
	protected 
readonly %
IVentaReporte_Repositorio 4
_ventaReporteDatos5 G
;G H
	protected 
readonly &
IMaestrosVenta_Repositorio 5
_maestrosVentaDatos6 I
;I J
	protected 
readonly #
IEstablecimiento_Logica 2"
_establecimientoLogica3 I
;I J
	protected 
readonly 
IActorNegocioLogica .
_actorNegocioLogica/ B
;B C
public   
VentaReporte_Logica   "
(  " #%
IVentaReporte_Repositorio  # <
ventaReporteDatos  = N
,  N O&
IMaestrosVenta_Repositorio  P j
maestrosVentaDatos  k }
,  } ~$
IEstablecimiento_Logica	   �#
establecimientoLogica
  � �
,
  � �!
IActorNegocioLogica
  � � 
actorNegocioLogica
  � �
)
  � �
{!! 	
_ventaReporteDatos"" 
=""  
ventaReporteDatos""! 2
;""2 3
_maestrosVentaDatos## 
=##  !
maestrosVentaDatos##" 4
;##4 5"
_establecimientoLogica$$ "
=$$# $!
establecimientoLogica$$% :
;$$: ;
_actorNegocioLogica%% 
=%%  !
actorNegocioLogica%%" 4
;%%4 5
}&& 	
public(( 
PrincipalReportData(( ",
 ObtenerDatosParaReportePrincipal((# C
(((C D"
UserProfileSessionData((D Z
profileData(([ f
)((f g
{)) 	
var** *
TieneRolAdministradorDeNegocio** .
=**/ 0
profileData**1 <
.**< =
Empleado**= E
.**E F
TieneRol**F N
(**N O

.**\ ]
Default**] d
.**d e(
idRolAdministradorDeNegocio	**e �
)
**� �
;
**� �
if++ 
(++ 
profileData++ 
.++ (
CentroDeAtencionSeleccionado++ 8
==++9 ;
null++< @
&&++A C
!++D E*
TieneRolAdministradorDeNegocio++E c
)++c d
{,, 
throw-- 
new-- 
LogicaException-- )
(--) *
$str--* j
)--j k
;--k l
}.. 
var// +
establecimientosConPuntosVentas// /
=//0 1*
TieneRolAdministradorDeNegocio//2 P
?//Q R
Establecimiento//S b
.//b c
Convert//c j
(//j k#
_establecimientoLogica	//k �
.
//� �J
<ObtenerEstablecimientosComercialesVigentesConSusPuntosVentas
//� �
(
//� �
)
//� �
.
//� �
ToList
//� �
(
//� �
)
//� �
)
//� �
:
//� �
null
//� �
;
//� �
var11 
data11 
=11 
new11 
PrincipalReportData11 .
(11. /
)11/ 0
{22 
FechaActual_33 
=33 
DateTimeUtil33 +
.33+ ,
FechaActual33, 7
(337 8
)338 9
,339 :
EsAdministrador44 
=44  !*
TieneRolAdministradorDeNegocio44" @
,44@ A
Establecimientos55  
=55! "*
TieneRolAdministradorDeNegocio55# A
?55B C+
establecimientosConPuntosVentas55D c
:55d e
new55f i
List55j n
<55n o
Establecimiento55o ~
>55~ 
(	55 �
)
55� �
{
55� �
profileData
55� �
.
55� �2
$EstablecimientoComercialSeleccionado
55� �
.
55� �
ToEstablecimiento
55� �
(
55� �
)
55� �
}
55� �
,
55� �
PuntosVentas66 
=66 *
TieneRolAdministradorDeNegocio66 =
?66> ?+
establecimientosConPuntosVentas66@ _
.66_ `

SelectMany66` j
(66j k
e66k l
=>66m o
e66p q
.66q r
CentrosAtencion	66r �
)
66� �
.
66� �
ToList
66� �
(
66� �
)
66� �
:
66� �
new
66� �
List
66� �
<
66� �
ItemGenerico
66� �
>
66� �
(
66� �
)
66� �
{
66� �
profileData
66� �
.
66� �*
CentroDeAtencionSeleccionado
66� �
.
66� �
ToItemGenerico
66� �
(
66� �
)
66� �
}
66� �
,
66� �
Familias77 
=77 
_maestrosVentaDatos77 .
.77. /
ObtenerFamilias77/ >
(77> ?
)77? @
.77@ A
ToList77A G
(77G H
)77H I
}88 
;88
if99 
(99 
!99 *
TieneRolAdministradorDeNegocio99 /
)99/ 0
data991 5
.995 6
Establecimientos996 F
.99F G
SingleOrDefault99G V
(99V W
)99W X
.99X Y
CentrosAtencion99Y h
=99i j
new99k n
List99o s
<99s t
ItemGenerico	99t �
>
99� �
(
99� �
)
99� �
{
99� �
profileData
99� �
.
99� �*
CentroDeAtencionSeleccionado
99� �
.
99� �
ToItemGenerico
99� �
(
99� �
)
99� �
}
99� �
;
99� �
return:: 
data:: 
;:: 
};; 	
public<< 
List<< 
<<< !
OperacionFamiliaGrupo<< )
><<) **
ObtenerVentasPorFamiliasGrupos<<+ I
(<<I J
int<<J M
idPuntoVenta<<N Z
,<<Z [
DateTime<<\ d

fechaDesde<<e o
,<<o p
DateTime<<q y

fechaHasta	<<z �
,
<<� �
bool
<<� �
todasLasFamilias
<<� �
,
<<� �
int
<<� �
[
<<� �
]
<<� �
idsFamilias
<<� �
,
<<� �
bool
<<� �
todosLosGrupos
<<� �
,
<<� �
int
<<� �
[
<<� �
]
<<� �
	idsGrupos
<<� �
)
<<� �
{== 	
try>> 
{?? 
idsFamilias@@ 
=@@ 
todasLasFamilias@@ .
?@@/ 0
_maestrosVentaDatos@@1 D
.@@D E
ObtenerFamilias@@E T
(@@T U
)@@U V
.@@V W
Select@@W ]
(@@] ^
g@@^ _
=>@@` b
g@@c d
.@@d e
Id@@e g
)@@g h
.@@h i
ToArray@@i p
(@@p q
)@@q r
:@@s t
idsFamilias	@@u �
;
@@� �
varAA 
idsFamiliasListAA #
=AA$ %
idsFamiliasAA& 1
.AA1 2
ToListAA2 8
(AA8 9
)AA9 :
;AA: ;
idsFamiliasListBB 
.BB  
AddBB  #
(BB# $
MaestroSettingsBB$ 3
.BB3 4
DefaultBB4 ;
.BB; <-
!IdDetalleMaestroConceptoDescuentoBB< ]
)BB] ^
;BB^ _
idsFamiliasListCC 
.CC  
AddCC  #
(CC# $
MaestroSettingsCC$ 3
.CC3 4
DefaultCC4 ;
.CC; <+
IdDetalleMaestroConceptoInteresCC< [
)CC[ \
;CC\ ]
	idsGruposDD 
=DD 
todosLosGruposDD *
?DD+ ,
_actorNegocioLogicaDD- @
.DD@ A+
ObtenerGruposActoresComercialesDDA `
(DD` a
)DDa b
.DDb c
SelectDDc i
(DDi j
gDDj k
=>DDl n
gDDo p
.DDp q
IdDDq s
)DDs t
.DDt u
ToArrayDDu |
(DD| }
)DD} ~
:	DD �
	idsGrupos
DD� �
;
DD� �
varEE 
idsTipoTransaccionEE &
=EE' (
DiccionarioEE) 4
.EE4 5O
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidacionesEE5 x
;EEx y
varFF 
idEstadoFF 
=FF 
MaestroSettingsFF .
.FF. /
DefaultFF/ 6
.FF6 7,
 IdDetalleMaestroEstadoConfirmadoFF7 W
;FFW X
ListGG 
<GG !
OperacionFamiliaGrupoGG *
>GG* +#
ventasPorFamiliasGruposGG, C
=GGD E
_ventaReporteDatosGGF X
.GGX Y*
ObtenerVentasPorFamiliasGruposGGY w
(GGw x
idsTipoTransaccion	GGx �
,
GG� �
idEstado
GG� �
,
GG� �
idPuntoVenta
GG� �
,
GG� �

fechaDesde
GG� �
,
GG� �

fechaHasta
GG� �
,
GG� �
idsFamiliasList
GG� �
.
GG� �
ToArray
GG� �
(
GG� �
)
GG� �
,
GG� �
	idsGrupos
GG� �
)
GG� �
.
GG� �
ToList
GG� �
(
GG� �
)
GG� �
;
GG� �
returnHH #
ventasPorFamiliasGruposHH .
;HH. /
}II 
catchJJ 
(JJ 
	ExceptionJJ 
eJJ 
)JJ 
{KK 
throwLL 
newLL 
LogicaExceptionLL )
(LL) *
$strLL* `
,LL` a
eLLb c
)LLc d
;LLd e
}MM 
}NN 	
publicOO 
ListOO 
<OO 
OperacionGrupoOO "
>OO" #"
ObtenerVentasPorGruposOO$ :
(OO: ;
intOO; >
idPuntoVentaOO? K
,OOK L
DateTimeOOM U

fechaDesdeOOV `
,OO` a
DateTimeOOb j

fechaHastaOOk u
,OOu v
boolOOw {
todosLosGrupos	OO| �
,
OO� �
int
OO� �
[
OO� �
]
OO� �
	idsGrupos
OO� �
)
OO� �
{PP 	
tryQQ 
{RR 
	idsGruposSS 
=SS 
todosLosGruposSS *
?SS+ ,
_actorNegocioLogicaSS- @
.SS@ A+
ObtenerGruposActoresComercialesSSA `
(SS` a
)SSa b
.SSb c
SelectSSc i
(SSi j
gSSj k
=>SSl n
gSSo p
.SSp q
IdSSq s
)SSs t
.SSt u
ToArraySSu |
(SS| }
)SS} ~
:	SS �
	idsGrupos
SS� �
;
SS� �
varTT 
idsTipoTransaccionTT &
=TT' (
DiccionarioTT) 4
.TT4 5O
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidacionesTT5 x
;TTx y
varUU 
idEstadoUU 
=UU 
MaestroSettingsUU .
.UU. /
DefaultUU/ 6
.UU6 7,
 IdDetalleMaestroEstadoConfirmadoUU7 W
;UUW X
ListVV 
<VV 
OperacionGrupoVV #
>VV# $
ventasPorGruposVV% 4
=VV5 6
_ventaReporteDatosVV7 I
.VVI J"
ObtenerVentasPorGruposVVJ `
(VV` a
idsTipoTransaccionVVa s
,VVs t
idEstadoVVu }
,VV} ~
idPuntoVenta	VV �
,
VV� �

fechaDesde
VV� �
,
VV� �

fechaHasta
VV� �
,
VV� �
	idsGrupos
VV� �
)
VV� �
.
VV� �
ToList
VV� �
(
VV� �
)
VV� �
;
VV� �
varWW !
gruposVentasPorGruposWW )
=WW* +
ventasPorGruposWW, ;
.WW; <
GroupByWW< C
(WWC D
gWWD E
=>WWF H
newWWI L
{WWM N
gWWO P
.WWP Q
IdGrupoWWQ X
,WWX Y
gWWZ [
.WW[ \
NombreGrupoWW\ g
,WWg h
gWWi j
.WWj k 
DocumentoResponsableWWk 
,	WW �
g
WW� �
.
WW� �
NombreResponsable
WW� �
,
WW� �
g
WW� �
.
WW� �
	IdCliente
WW� �
,
WW� �
g
WW� �
.
WW� �

WW� �
}
WW� �
)
WW� �
;
WW� �
varXX 
	resultadoXX 
=XX !
gruposVentasPorGruposXX  5
.XX5 6
SelectXX6 <
(XX< =
gXX= >
=>XX? A
newXXB E
OperacionGrupoXXF T
(XXT U
)XXU V
{YY 
IdGrupoZZ 
=ZZ 
gZZ 
.ZZ  
KeyZZ  #
.ZZ# $
IdGrupoZZ$ +
,ZZ+ ,
NombreGrupo[[ 
=[[  !
g[[" #
.[[# $
Key[[$ '
.[[' (
NombreGrupo[[( 3
,[[3 4 
DocumentoResponsable\\ (
=\\) *
g\\+ ,
.\\, -
Key\\- 0
.\\0 1 
DocumentoResponsable\\1 E
,\\E F
NombreResponsable]] %
=]]& '
g]]( )
.]]) *
Key]]* -
.]]- .
NombreResponsable]]. ?
,]]? @
	IdCliente^^ 
=^^ 
g^^  !
.^^! "
Key^^" %
.^^% &
	IdCliente^^& /
,^^/ 0

=__" #
g__$ %
.__% &
Key__& )
.__) *

,__7 8
InfoComprobante`` #
=``$ %
String``& ,
.``, -
Join``- 1
(``1 2
$str``2 6
,``6 7
g``8 9
.``9 :
Select``: @
(``@ A
gg``A C
=>``D F
gg``G I
.``I J
InfoComprobante``J Y
)``Y Z
.``Z [
Distinct``[ c
(``c d
)``d e
.``e f
ToArray``f m
(``m n
)``n o
)``o p
,``p q
NumeroOperacionesaa %
=aa& '
gaa( )
.aa) *
Selectaa* 0
(aa0 1
ggaa1 3
=>aa4 6
ggaa7 9
.aa9 :
InfoComprobanteaa: I
)aaI J
.aaJ K
DistinctaaK S
(aaS T
)aaT U
.aaU V
CountaaV [
(aa[ \
)aa\ ]
,aa] ^
Importebb 
=bb 
gbb 
.bb  
Sumbb  #
(bb# $
ggbb$ &
=>bb' )
ggbb* ,
.bb, -
ImporteTotalbb- 9
)bb9 :
}cc 
)cc 
.cc 
ToListcc 
(cc 
)cc 
;cc 
returndd 
	resultadodd  
;dd  !
}ee 
catchff 
(ff 
	Exceptionff 
eff 
)ff 
{gg 
throwhh 
newhh 
LogicaExceptionhh )
(hh) *
$strhh* W
,hhW X
ehhY Z
)hhZ [
;hh[ \
}ii 
}jj 	
publickk 
Listkk 
<kk #
OperacionGrupoDetalladokk +
>kk+ ,*
ObtenerVentasPorGrupoDetalladokk- K
(kkK L
intkkL O
idPuntoVentakkP \
,kk\ ]
DateTimekk^ f

fechaDesdekkg q
,kkq r
DateTimekks {

fechaHasta	kk| �
,
kk� �
int
kk� �
idGrupo
kk� �
)
kk� �
{ll 	
trymm 
{nn 
varoo 
idsTipoTransaccionoo &
=oo' (
Diccionariooo) 4
.oo4 5O
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidacionesoo5 x
;oox y
varpp 
idEstadopp 
=pp 
MaestroSettingspp .
.pp. /
Defaultpp/ 6
.pp6 7,
 IdDetalleMaestroEstadoConfirmadopp7 W
;ppW X
Listqq 
<qq #
OperacionGrupoDetalladoqq ,
>qq, -#
ventasPorGrupoDetalladoqq. E
=qqF G
_ventaReporteDatosqqH Z
.qqZ [*
ObtenerVentasPorGrupoDetalladoqq[ y
(qqy z
idsTipoTransaccion	qqz �
,
qq� �
idEstado
qq� �
,
qq� �
idPuntoVenta
qq� �
,
qq� �

fechaDesde
qq� �
,
qq� �

fechaHasta
qq� �
,
qq� �
idGrupo
qq� �
)
qq� �
.
qq� �
ToList
qq� �
(
qq� �
)
qq� �
;
qq� �
returnrr #
ventasPorGrupoDetalladorr .
;rr. /
}ss 
catchtt 
(tt 
	Exceptiontt 
ett 
)tt 
{uu 
throwvv 
newvv 
LogicaExceptionvv )
(vv) *
$strvv* `
,vv` a
evvb c
)vvc d
;vvd e
}ww 
}xx 	
}yy 
}zz �8
SD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Venta\ConsultaMasivaVentaLogica.cs
	namespace
Tsp
 
.
Sigescom
.
Logica
.
Core
.
Venta
{ 
public 

class %
ConsultaMasivaVentaLogica *
:+ ,&
IConsultaMasivaVentaLogica- G
{ 
private 
readonly ,
 IConsultaTransaccion_Repositorio 9+
_consultaTransaccionRepositorio: Y
;Y Z
public %
ConsultaMasivaVentaLogica (
(( ),
 IConsultaTransaccion_Repositorio) I*
consultaTransaccionRepositorioJ h
)h i
{ 	
this 
. +
_consultaTransaccionRepositorio 0
=1 2*
consultaTransaccionRepositorio3 Q
;Q R
} 	
public 
List 
< 6
*TransaccionPorSerieDeComprobanteYCategoria >
>> ??
3ObtenerComprobanteVentaPorSerieYCategoriaConfirmado@ s
(s t
intt w
[w x
]x y
idsPuntosDeVentas	z �
,
� �
DateTime
� �

fechaDesde
� �
,
� �
DateTime
� �

fechaHasta
� �
)
� �
{ 	
var 
	resultado 
= +
_consultaTransaccionRepositorio ;
.; <?
3ObtenerTransaccionesPorSerieDeComprobanteYCategoria< o
(o p
idsPuntosDeVentas	p �
,
� �
Diccionario
� �
.
� �Q
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones
� �
,
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �.
 IdDetalleMaestroEstadoConfirmado
� �
,
� �

fechaDesde
� �
,
� �

fechaHasta
� �
)
� �
.
� �
ToList
� �
(
� �
)
� �
;
� �
foreach 
( 
var 
item 
in  
	resultado! *
.* +
Where+ 0
(0 1
r1 2
=>3 5
r6 7
.7 8
IdTipoComprobante8 I
==J L
MaestroSettingsM \
.\ ]
Default] d
.d e3
&IdDetalleMaestroComprobanteNotaCredito	e �
)
� �
)
� �
{ 
item 
. 
Cantidad 
*=  
-! "
$num" #
;# $
item 
. 
Importe 
*= 
-  !
$num! "
;" #
} 
return   
	resultado   
;   
}!! 	
public"" 
List"" 
<"" 6
*TransaccionPorSerieDeComprobanteYCategoria"" >
>""> ??
3ObtenerComprobanteVentaPorSerieYCategoriaInvalidado""@ s
(""s t
int""t w
[""w x
]""x y
idsPuntosDeVentas	""z �
,
""� �
DateTime
""� �

fechaDesde
""� �
,
""� �
DateTime
""� �

fechaHasta
""� �
)
""� �
{## 	
var$$ 
	resultado$$ 
=$$ +
_consultaTransaccionRepositorio$$ ;
.$$; <?
3ObtenerTransaccionesPorSerieDeComprobanteYCategoria$$< o
($$o p
idsPuntosDeVentas	$$p �
,
$$� �
Diccionario
$$� �
.
$$� �Q
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones
$$� �
,
$$� �
MaestroSettings
$$� �
.
$$� �
Default
$$� �
.
$$� �.
 IdDetalleMaestroEstadoInvalidado
$$� �
,
$$� �

fechaDesde
$$� �
,
$$� �

fechaHasta
$$� �
)
$$� �
.
$$� �
ToList
$$� �
(
$$� �
)
$$� �
;
$$� �
return%% 
	resultado%% 
;%% 
}&& 	
public'' 
List'' 
<'' 5
)TransaccionPorSerieDeComprobanteYConcepto'' =
>''= >>
2ObtenerComprobanteVentaPorSerieYConceptoConfirmado''? q
(''q r
int''r u
[''u v
]''v w
	idsSeries	''x �
,
''� �
DateTime
''� �

fechaDesde
''� �
,
''� �
DateTime
''� �

fechaHasta
''� �
)
''� �
{(( 	
var)) 
	resultado)) 
=)) +
_consultaTransaccionRepositorio)) ;
.)); <>
2ObtenerTransaccionesPorSerieDeComprobanteYConcepto))< n
())n o
	idsSeries))o x
,))x y
Diccionario	))z �
.
))� �Q
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones
))� �
,
))� �
MaestroSettings
))� �
.
))� �
Default
))� �
.
))� �.
 IdDetalleMaestroEstadoConfirmado
))� �
,
))� �

fechaDesde
))� �
,
))� �

fechaHasta
))� �
)
))� �
.
))� �
ToList
))� �
(
))� �
)
))� �
;
))� �
foreach** 
(** 
var** 
item** 
in**  
	resultado**! *
.*** +
Where**+ 0
(**0 1
r**1 2
=>**3 5
r**6 7
.**7 8
IdTipoComprobante**8 I
==**J L
MaestroSettings**M \
.**\ ]
Default**] d
.**d e3
&IdDetalleMaestroComprobanteNotaCredito	**e �
)
**� �
)
**� �
{++ 
item,, 
.,, 
Cantidad,, 
*=,,  
-,,! "
$num,," #
;,,# $
item-- 
.-- 
Importe-- 
*=-- 
---  !
$num--! "
;--" #
}.. 
return00 
	resultado00 
;00 
}11 	
public22 
List22 
<22 5
)TransaccionPorSerieDeComprobanteYConcepto22 =
>22= >>
2ObtenerComprobanteVentaPorSerieYConceptoInvalidado22? q
(22q r
int22r u
[22u v
]22v w
	idsSeries	22x �
,
22� �
DateTime
22� �

fechaDesde
22� �
,
22� �
DateTime
22� �

fechaHasta
22� �
)
22� �
{33 	
var44 
	resultado44 
=44 +
_consultaTransaccionRepositorio44 ;
.44; <>
2ObtenerTransaccionesPorSerieDeComprobanteYConcepto44< n
(44n o
	idsSeries44o x
,44x y
Diccionario	44z �
.
44� �Q
CTiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones
44� �
,
44� �
MaestroSettings
44� �
.
44� �
Default
44� �
.
44� �.
 IdDetalleMaestroEstadoInvalidado
44� �
,
44� �

fechaDesde
44� �
,
44� �

fechaHasta
44� �
)
44� �
.
44� �
ToList
44� �
(
44� �
)
44� �
;
44� �
return55 
	resultado55 
;55 
}66 	
}77 
}88 ��
XD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Venta\OperacionLogica_InvalidarVenta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

partial 
class 
OperacionLogica (
{ 
public 
OperationResult +
ResolverInvalidarOperacionVenta >
(> ?"
OperacionModificatoria? U"
operacionModificatoriaV l
,l m
longn r
idOrdenVentas 
,	 �
string
� �
observacion
� �
,
� �$
UserProfileSessionData
� �

� �
)
� �
{ 	
try 
{ 
var 
fechaActual 
=  !
DateTimeUtil" .
.. /
FechaActual/ :
(: ;
); <
;< =
var 

ordenVenta 
=  
new! $
OrdenDeVenta% 1
(1 2"
transaccionRepositorio2 H
.H IE
8ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado	I �
(
� �
idOrdenVenta
� �
)
� �
)
� �
;
� �
Estado_transaccion   ""
estadoDeLaOrdenDeVenta  # 9
=  : ;
new  < ?
Estado_transaccion  @ R
(  R S
idOrdenVenta  S _
,  _ `

.  n o
Empleado  o w
.  w x
Id  x z
,  z {
MaestroSettings	  | �
.
  � �
Default
  � �
.
  � �.
 IdDetalleMaestroEstadoInvalidado
  � �
,
  � �
fechaActual
  � �
,
  � �
$str!! A
)!!A B
;!!B C
List## 
<## 
Cuota## 
>## 
cuotasModificadas## -
=##. /
new##0 3
List##4 8
<##8 9
Cuota##9 >
>##> ?
(##? @
)##@ A
;##A B
foreach$$ 
($$ 
var$$ 
cuota$$ "
in$$# %

ordenVenta$$& 0
.$$0 1
Cuotas$$1 7
($$7 8
)$$8 9
.$$9 :
OrderByDescending$$: K
($$K L
c$$L M
=>$$N P
c$$Q R
.$$R S
fecha_vencimiento$$S d
)$$d e
)$$e f
{%% 
if&& 
(&& 
cuota&& 
.&& 
saldo&& #
>&&$ %
$num&&& '
)&&' (
{'' 
cuota(( 
.(( 
revocado(( &
=((' (
cuota(() .
.((. /
saldo((/ 4
;((4 5
cuota)) 
.)) 
saldo)) #
=))$ %
cuota))& +
.))+ ,
total)), 1
-))2 3
cuota))4 9
.))9 :

-))H I
cuota))J O
.))O P
revocado))P X
;))X Y
cuotasModificadas** )
.**) *
Add*** -
(**- .
cuota**. 3
)**3 4
;**4 5
}++ 
},, "
operacionModificatoria.. &
...& '1
%NuevosEstadosTransaccionesModificadas..' L
=..M N
new..O R
List..S W
<..W X
Estado_transaccion..X j
>..j k
{..l m#
estadoDeLaOrdenDeVenta	..n �
}
..� �
;
..� �"
operacionModificatoria// &
.//& '
CuotasModificadas//' 8
=//9 :
cuotasModificadas//; L
;//L M
var11 
	resultado11 
=11 4
(AfectarInventarioFisicoYGuardarOperacion11  H
(11H I"
operacionModificatoria11I _
,11_ `

)11n o
;11o p
if22 
(22 
	resultado22 
.22 
code_result22 )
==22* ,
OperationResultEnum22- @
.22@ A
Success22A H
)22H I
{33 
if44 
(44 

ordenVenta44 "
.44" #
EstaTransmitido44# 2
(442 3
)443 4
)444 5
{55 
OperationResult66 '
resultadoEFactura66( 9
=66: ;"
facturacionRepositorio66< R
.66R S%
ActualizarEstadoDocumento66S l
(66l m
idOrdenVenta66m y
,66y z'
EstadoDocumentoElectronico	66{ �
.
66� �
Anulado
66� �
,
66� �0
"EstadoSigescomDocumentoElectronico
66� �
.
66� �

Invalidado
66� �
)
66� �
;
66� �
if77 
(77 
resultadoEFactura77 -
.77- .
code_result77. 9
!=77: <
OperationResultEnum77= P
.77P Q
Success77Q X
)77X Y
{88 
	resultado99 %
.99% &

exceptions99& 0
.990 1
Add991 4
(994 5
resultadoEFactura995 F
.99F G

exceptions99G Q
.99Q R
First99R W
(99W X
)99X Y
)99Y Z
;99Z [
}:: 
};; 
}<< 
return== 
	resultado==  
;==  !
}>> 
catch?? 
(?? 
	Exception?? 
e?? 
)?? 
{@@ 
throwAA 
newAA 
LogicaExceptionAA )
(AA) *
$strAA* g
,AAg h
eAAi j
)AAj k
;AAk l
}BB 
}CC 	
publicEE 
OperationResultEE 
InvalidarVentaEE -
(EE- .
InvalidarVentaEE. <
invalidarVentaEE= K
,EEK L"
UserProfileSessionDataEEM c

)EEq r
{FF 	
tryGG 
{HH 
DateTimeII 
fechaActualII $
=II% &
DateTimeUtilII' 3
.II3 4
FechaActualII4 ?
(II? @
)II@ A
;IIA B
ListJJ 
<JJ 
CuotaJJ 
>JJ 
cuotasModificadasJJ -
=JJ. /
newJJ0 3
ListJJ4 8
<JJ8 9
CuotaJJ9 >
>JJ> ?
(JJ? @
)JJ@ A
;JJA B
ListKK 
<KK 
Estado_transaccionKK '
>KK' ($
nuevosEstadosTransaccionKK) A
=KKB C
newKKD G
ListKKH L
<KKL M
Estado_transaccionKKM _
>KK_ `
(KK` a
)KKa b
;KKb c
ListLL 
<LL 
Detalle_transaccionLL (
>LL( ))
detallesTransaccionAModificarLL* G
=LLH I
newLLJ M
ListLLN R
<LLR S
Detalle_transaccionLLS f
>LLf g
(LLg h
)LLh i
;LLi j
varMM 

ordenVentaMM 
=MM  
newMM! $
OrdenDeVentaMM% 1
(MM1 2"
transaccionRepositorioMM2 H
.MMH I.
"ObtenerTransaccionInclusiveEstadosMMI k
(MMk l
invalidarVentaMMl z
.MMz {
IdMM{ }
)MM} ~
)MM~ 
;	MM �
varNN 
ventaNN 
=NN 
newNN 
VentaNN  %
(NN% &"
transaccionRepositorioNN& <
.NN< =.
"ObtenerTransaccionInclusiveEstadosNN= _
(NN_ `

ordenVentaNN` j
.NNj k
IdVentaNNk r
)NNr s
)NNs t
;NNt u
varOO !
operacionInvalidacionOO )
=OO* +
newOO, /!
OperacionInvalidacionOO0 E
(OOE F
ventaOOF K
,OOK L

ordenVentaOOM W
,OOW X
fechaActualOOY d
,OOd e
invalidarVentaOOf t
.OOt u

EsDiferidaOOu 
,	OO �
invalidarVenta
OO� �
.
OO� �
Observacion
OO� �
,
OO� �
invalidarVenta
OO� �
.
OO� �
Pago
OO� �
,
OO� �

OO� �
)
OO� �
;
OO� �&
ValidarInvalidacionDeVentaQQ *
(QQ* +!
operacionInvalidacionQQ+ @
.QQ@ A

OrdenVentaQQA K
)QQK L
;QQL M
permisos_LogicaRR 
.RR  

(RR- .!
operacionInvalidacionRR. C
.RRC D

IdEmpleadoRRD N
,RRN O
MaestroSettingsRRP _
.RR_ `
DefaultRR` g
.RRg h,
IdDetalleMaestroAccionInvalidar	RRh �
,
RR� �!
TransaccionSettings
RR� �
.
RR� �
Default
RR� �
.
RR� �+
IdTipoTransaccionOrdenDeVenta
RR� �
,
RR� �#
operacionInvalidacion
RR� �
.
RR� �
IdUnidadDeNegocio
RR� �
)
RR� �
;
RR� �A
5CalcularDetallesYGenerarDetallesAModificarAlInvalidarSS E
(SSE F!
operacionInvalidacionSSF [
,SS[ \$
nuevosEstadosTransaccionSS] u
,SSu v*
detallesTransaccionAModificar	SSw �
)
SS� �
;
SS� �
TransaccionUU 
invalidacionVentaUU -
=UU. /%
GenerarInvalidarOperacionUU0 I
(UUI J!
operacionInvalidacionUUJ _
)UU_ `
;UU` a
TransaccionVV "
ordenInvalidacionVentaVV 2
=VV3 4*
GenerarOrdenInvalidarOperacionVV5 S
(VVS T
invalidacionVentaVVT e
,VVe f!
operacionInvalidacionVVg |
)VV| }
;VV} ~
TransaccionWW 
pagoWW  
=WW! ")
GenerarPagoInvalidarOperacionWW# @
(WW@ A
invalidacionVentaWWA R
,WWR S"
ordenInvalidacionVentaWWT j
,WWj k"
operacionInvalidacion	WWl �
)
WW� �
;
WW� �
TransaccionXX 
movimientoAlmacenXX -
=XX. /6
*GenerarMovimientoAlmacenInvalidarOperacionXX0 Z
(XXZ [
invalidacionVentaXX[ l
,XXl m#
ordenInvalidacionVenta	XXn �
,
XX� �#
operacionInvalidacion
XX� �
,
XX� �

XX� �
)
XX� �
;
XX� �>
2GenerarEstadosOperacionInvalidadaYCuotasActualizarZZ B
(ZZB C$
nuevosEstadosTransaccionZZC [
,ZZ[ \
cuotasModificadasZZ] n
,ZZn o"
operacionInvalidacion	ZZp �
)
ZZ� �
;
ZZ� �
var[[ (
movimientoEconomicoConPuntos[[ 0
=[[1 2
venta[[3 8
.[[8 9
ObtenerPagos[[9 E
([[E F
)[[F G
.[[G H
SingleOrDefault[[H W
([[W X
p[[X Y
=>[[Z \
p[[] ^
.[[^ _
TrazaDePago[[_ j
([[j k
)[[k l
.[[l m
MedioDePago[[m x
([[x y
)[[y z
.[[z {
id[[{ }
==	[[~ �
MaestroSettings
[[� �
.
[[� �
Default
[[� �
.
[[� �/
!IdDetalleMaestroMedioDepagoPuntos
[[� �
)
[[� �
;
[[� �
List\\ 
<\\ 
Transaccion\\  
>\\  !#
transaccionesAModificar\\" 9
=\\: ;*
GenerarTransaccionesAModificar\\< Z
(\\Z [(
movimientoEconomicoConPuntos\\[ w
)\\w x
;\\x y
var^^ "
operacionModificatoria^^ *
=^^+ ,
new^^- 0"
OperacionModificatoria^^1 G
(^^G H
)^^H I
{^^J K
	Operacion^^L U
=^^V W
invalidacionVenta^^X i
,^^i j
OrdenDeOperacion^^k {
=^^| }#
ordenInvalidacionVenta	^^~ �
,
^^� �!
MovimientoEconomico
^^� �
=
^^� �
pago
^^� �
,
^^� �
MovimientosBienes
^^� �
=
^^� �
movimientoAlmacen
^^� �
!=
^^� �
null
^^� �
?
^^� �
new
^^� �
List
^^� �
<
^^� �
Transaccion
^^� �
>
^^� �
(
^^� �
)
^^� �
{
^^� �
movimientoAlmacen
^^� �
}
^^� �
:
^^� �
null
^^� �
,
^^� �3
%NuevosEstadosTransaccionesModificadas
^^� �
=
^^� �&
nuevosEstadosTransaccion
^^� �
,
^^� �
CuotasModificadas
^^� �
=
^^� �
cuotasModificadas
^^� �
,
^^� �&
TransaccionesModificadas
^^� �
=
^^� �%
transaccionesAModificar
^^� �
,
^^� �,
DetallesTransaccionModificados
^^� �
=
^^� �+
detallesTransaccionAModificar
^^� �
}
^^� �
;
^^� �
var__ !
resultadoInvalidacion__ )
=__* +4
(AfectarInventarioFisicoYGuardarOperacion__, T
(__T U"
operacionModificatoria__U k
,__k l

)__z {
;__{ |
if`` 
(`` !
resultadoInvalidacion`` )
.``) *
code_result``* 5
==``6 8
OperationResultEnum``9 L
.``L M
Success``M T
&&``U W!
operacionInvalidacion``X m
.``m n

OrdenVenta``n x
.``x y
EstaTransmitido	``y �
(
``� �
)
``� �
)
``� �
{aa 
OperationResultbb #
resultadoEFacturabb$ 5
=bb6 7"
facturacionRepositoriobb8 N
.bbN O%
ActualizarEstadoDocumentobbO h
(bbh i!
operacionInvalidacionbbi ~
.bb~ 

OrdenVenta	bb �
.
bb� �
Id
bb� �
,
bb� �(
EstadoDocumentoElectronico
bb� �
.
bb� �
Anulado
bb� �
,
bb� �0
"EstadoSigescomDocumentoElectronico
bb� �
.
bb� �

Invalidado
bb� �
)
bb� �
;
bb� �
ifcc 
(cc 
resultadoEFacturacc )
.cc) *
code_resultcc* 5
!=cc6 8
OperationResultEnumcc9 L
.ccL M
SuccessccM T
)ccT U
{dd !
resultadoInvalidacionee -
.ee- .

exceptionsee. 8
.ee8 9
Addee9 <
(ee< =
resultadoEFacturaee= N
.eeN O

exceptionseeO Y
.eeY Z
FirsteeZ _
(ee_ `
)ee` a
)eea b
;eeb c
}ff 
}gg 
returnhh !
resultadoInvalidacionhh ,
;hh, -
}ii 
catchjj 
(jj 
	Exceptionjj 
ejj 
)jj 
{kk 
throwll 
newll 
	Exceptionll #
(ll# $
$strll$ N
,llN O
ellP Q
)llQ R
;llR S
}mm 
}nn 	
publicpp 
voidpp A
5CalcularDetallesYGenerarDetallesAModificarAlInvalidarpp I
(ppI J!
OperacionInvalidacionppJ _!
operacionInvalidacionpp` u
,ppu v
Listppw {
<pp{ |
Estado_transaccion	pp| �
>
pp� �&
nuevosEstadosTransaccion
pp� �
,
pp� �
List
pp� �
<
pp� �!
Detalle_transaccion
pp� �
>
pp� �+
detallesTransaccionAModificar
pp� �
)
pp� �
{qq 	
ifrr 
(rr !
operacionInvalidacionrr %
.rr% &#
EsOrdenOrigenCompletadarr& =
)rr= >
{ss 
operacionInvalidacionuu %
.uu% &.
"DetallesMovimientoAlmacenOperacionuu& H
=uuI J!
operacionInvalidacionuuK `
.uu` a#
DetallesBienesOperacionuua x
;uux y
}vv 
elseww 
ifww 
(ww !
operacionInvalidacionww *
.ww* + 
EsOrdenOrigenParcialww+ ?
)ww? @
{xx 
varyy #
detallesOperacionOrigenyy +
=yy, -!
operacionInvalidacionyy. C
.yyC D
DetallesOperacionyyD U
;yyU V
varzz 
ordenAlmacenzz  
=zz! "
ObtenerOrdenAlmacenzz# 6
(zz6 7!
operacionInvalidacionzz7 L
.zzL M

OrdenVentazzM W
.zzW X
IdzzX Z
)zzZ [
;zz[ \!
operacionInvalidacion{{ %
.{{% &
DetallesOperacion{{& 7
.{{7 8
ForEach{{8 ?
({{? @
di{{@ B
=>{{C E
di{{F H
.{{H I

cantidad_1{{I S
={{T U
ordenAlmacen{{V b
.{{b c
Detalles{{c k
.{{k l
Where{{l q
({{q r
d{{r s
=>{{t v
d{{w x
.{{x y

IdConcepto	{{y �
==
{{� �
di
{{� �
.
{{� �!
id_concepto_negocio
{{� �
)
{{� �
.
{{� �
Sum
{{� �
(
{{� �
d
{{� �
=>
{{� �
d
{{� �
.
{{� �
	Pendiente
{{� �
)
{{� �
)
{{� �
;
{{� �#
detallesOperacionOrigen|| '
.||' (
ForEach||( /
(||/ 0
di||0 2
=>||3 5
di||6 8
.||8 9

cantidad_1||9 C
=||D E
ordenAlmacen||F R
.||R S
Detalles||S [
.||[ \
Where||\ a
(||a b
d||b c
=>||d f
d||g h
.||h i

IdConcepto||i s
==||t v
di||w y
.||y z 
id_concepto_negocio	||z �
)
||� �
.
||� �
Sum
||� �
(
||� �
d
||� �
=>
||� �
d
||� �
.
||� �
	Pendiente
||� �
)
||� �
)
||� �
;
||� �$
nuevosEstadosTransaccion}} (
.}}( )
Add}}) ,
(}}, -
new}}- 0
Estado_transaccion}}1 C
(}}C D!
operacionInvalidacion}}D Y
.}}Y Z

OrdenVenta}}Z d
.}}d e
Id}}e g
,}}g h!
operacionInvalidacion}}i ~
.}}~ 

IdEmpleado	}} �
,
}}� �
MaestroSettings
}}� �
.
}}� �
Default
}}� �
.
}}� �.
 IdDetalleMaestroEstadoCompletada
}}� �
,
}}� �#
operacionInvalidacion
}}� �
.
}}� �
FechaActual
}}� �
,
}}� �
$str
}}� �
)
}}� �
)
}}� �
;
}}� �
foreach 
( 
var "
detalleOperacionOrigen 3
in4 6#
detallesOperacionOrigen7 N
)N O
{
�� 
var
�� 
detalle
�� 
=
��  !$
transaccionRepositorio
��" 8
.
��8 9'
ObtenerDetalleTransaccion
��9 R
(
��R S$
detalleOperacionOrigen
��S i
.
��i j
id
��j l
)
��l m
;
��m n
detalle
�� 
.
�� 

cantidad_1
�� &
=
��' ($
detalleOperacionOrigen
��) ?
.
��? @

cantidad_1
��@ J
;
��J K+
detallesTransaccionAModificar
�� 1
.
��1 2
Add
��2 5
(
��5 6
detalle
��6 =
)
��= >
;
��> ?
}
�� #
operacionInvalidacion
�� %
.
��% &0
"DetallesMovimientoAlmacenOperacion
��& H
=
��I J
new
��K N
List
��O S
<
��S T!
Detalle_transaccion
��T g
>
��g h
(
��h i
)
��i j
;
��j k
var
�� '
detallesMovimientoAlmacen
�� -
=
��. /
ordenAlmacen
��0 <
.
��< =
Detalles
��= E
.
��E F
Where
��F K
(
��K L
d
��L M
=>
��N P
d
��Q R
.
��R S
	Entregado
��S \
>
��] ^
$num
��_ `
)
��` a
.
��a b
ToList
��b h
(
��h i
)
��i j
;
��j k
foreach
�� 
(
�� 
var
�� 
detalle
�� $
in
��% ''
detallesMovimientoAlmacen
��( A
)
��A B
{
�� 
var
�� 
detalleAlmacen
�� &
=
��' (#
operacionInvalidacion
��) >
.
��> ?%
DetallesBienesOperacion
��? V
.
��V W
First
��W \
(
��\ ]
d
��] ^
=>
��_ a
d
��b c
.
��c d!
id_concepto_negocio
��d w
==
��x z
detalle��{ �
.��� �

IdConcepto��� �
)��� �
;��� �
detalleAlmacen
�� "
.
��" #
cantidad
��# +
=
��, -
detalle
��. 5
.
��5 6
	Entregado
��6 ?
;
��? @#
operacionInvalidacion
�� )
.
��) *0
"DetallesMovimientoAlmacenOperacion
��* L
.
��L M
Add
��M P
(
��P Q
detalleAlmacen
��Q _
)
��_ `
;
��` a
}
�� 
}
�� 
else
�� 
if
�� 
(
�� #
operacionInvalidacion
�� *
.
��* +$
EsOrdenOrigenPendiente
��+ A
)
��A B
{
�� 
var
�� %
detallesOperacionOrigen
�� +
=
��, -#
operacionInvalidacion
��. C
.
��C D
DetallesOperacion
��D U
;
��U V%
detallesOperacionOrigen
�� '
.
��' (
ForEach
��( /
(
��/ 0
di
��0 2
=>
��3 5
di
��6 8
.
��8 9

cantidad_1
��9 C
=
��D E
di
��F H
.
��H I
cantidad
��I Q
)
��Q R
;
��R S&
nuevosEstadosTransaccion
�� (
.
��( )
Add
��) ,
(
��, -
new
��- 0 
Estado_transaccion
��1 C
(
��C D#
operacionInvalidacion
��D Y
.
��Y Z

OrdenVenta
��Z d
.
��d e
Id
��e g
,
��g h#
operacionInvalidacion
��i ~
.
��~ 

IdEmpleado�� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoCompletada��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
�� $
detalleOperacionOrigen
�� 3
in
��4 6%
detallesOperacionOrigen
��7 N
)
��N O
{
�� 
var
�� 
detalle
�� 
=
��  !$
transaccionRepositorio
��" 8
.
��8 9'
ObtenerDetalleTransaccion
��9 R
(
��R S$
detalleOperacionOrigen
��S i
.
��i j
id
��j l
)
��l m
;
��m n
detalle
�� 
.
�� 

cantidad_1
�� &
=
��' ($
detalleOperacionOrigen
��) ?
.
��? @

cantidad_1
��@ J
;
��J K+
detallesTransaccionAModificar
�� 1
.
��1 2
Add
��2 5
(
��5 6
detalle
��6 =
)
��= >
;
��> ?
}
�� 
}
�� 
}
�� 	
public
�� 
OrdenAlmacen
�� !
ObtenerOrdenAlmacen
�� /
(
��/ 0
long
��0 4
idOrdenAlmacen
��5 C
)
��C D
{
�� 	
var
�� 
ordenAlmacen
�� 
=
��  
ordenAlmacen_Datos
�� 1
.
��1 2!
ObtenerOrdenAlmacen
��2 E
(
��E F
idOrdenAlmacen
��F T
)
��T U
;
��U V
ordenAlmacen
�� 
.
�� 

IdsOrdenes
�� #
.
��# $
Insert
��$ *
(
��* +
$num
��+ ,
,
��, -
ordenAlmacen
��. :
.
��: ;
Id
��; =
)
��= >
;
��> ?
ordenAlmacen
�� 
.
�� 
Movimientos
�� $
=
��% & 
ordenAlmacen_Datos
��' 9
.
��9 :9
+ObtenerMovimientosConfirmadosDeOrdenAlmacen
��: e
(
��e f
ordenAlmacen
��f r
.
��r s

IdsOrdenes
��s }
.
��} ~
ToArray��~ �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
var
�� !
detallesMovimientos
�� #
=
��$ %
ordenAlmacen
��& 2
.
��2 3
Movimientos
��3 >
.
��> ?
Where
��? D
(
��D E
m
��E F
=>
��G I
m
��J K
.
��K L
	EsVigente
��L U
)
��U V
.
��V W

SelectMany
��W a
(
��a b
m
��b c
=>
��d f
m
��g h
.
��h i
Detalles
��i q
)
��q r
.
��r s
ToList
��s y
(
��y z
)
��z {
;
��{ |
foreach
�� 
(
�� 
var
�� 
detalle
��  
in
��! #
ordenAlmacen
��$ 0
.
��0 1
Detalles
��1 9
)
��9 :
{
�� 
detalle
�� 
.
�� 
	Entregado
�� !
=
��" #!
detallesMovimientos
��$ 7
.
��7 8
Where
��8 =
(
��= >
dmo
��> A
=>
��B D
dmo
��E H
.
��H I

IdConcepto
��I S
==
��T V
detalle
��W ^
.
��^ _

IdConcepto
��_ i
)
��i j
.
��j k
Sum
��k n
(
��n o
d
��o p
=>
��q s
d
��t u
.
��u v
Cantidad
��v ~
)
��~ 
;�� �
detalle
�� 
.
�� 
	Pendiente
�� !
=
��" #
detalle
��$ +
.
��+ ,
Ordenado
��, 4
-
��5 6
detalle
��7 >
.
��> ?
	Entregado
��? H
;
��H I
}
�� 
return
�� 
ordenAlmacen
�� 
;
��  
}
�� 	
public
�� 
Transaccion
�� '
GenerarInvalidarOperacion
�� 4
(
��4 5#
OperacionInvalidacion
��5 J#
operacionInvalidacion
��K `
)
��` a
{
�� 	
string
�� 
codigo
�� 
=
�� %
codigosOperacion_Logica
�� 3
.
��3 43
%ObtenerSiguienteCodigoParaFacturacion
��4 Y
(
��Y Z
Diccionario
��Z e
.
��e f6
'MapeoTipoTransaccionVsCodigoDeOperacion��f �
.��� �
Single��� �
(��� �
c��� �
=>��� �
c��� �
.��� �
Key��� �
==��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �4
$IdTipoTransaccionInvalidacionDeVenta��� �
)��� �
.��� �
Value��� �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �4
$IdTipoTransaccionInvalidacionDeVenta��� �
)��� �
;��� �
Serie_comprobante
�� 
serie
�� #
=
��$ %$
transaccionRepositorio
��& <
.
��< =M
?ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante
��= |
(
��| }
MaestroSettings��} �
.��� �
Default��� �
.��� �@
0IdDetalleMaestroComprobanteNotaInvalidacionVenta��� �
,��� �%
operacionInvalidacion��� �
.��� � 
IdCentroAtencion��� �
)��� �
;��� �
if
�� 
(
�� 
serie
�� 
==
�� 
null
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str��* �
)��� �
;��� �
}
�� 
Comprobante
�� 
comprobante
�� #
=
��$ %3
%GenerarComprobantePropioAutonumerable
��& K
(
��K L
serie
��L Q
.
��Q R
id
��R T
)
��T U
;
��U V
Transaccion
�� !
invalidacionDeVenta
�� +
=
��, -
new
��. 1
Transaccion
��2 =
(
��= >
codigo
��> D
,
��D E#
operacionInvalidacion
��F [
.
��[ \
Venta
��\ a
.
��a b 
IdTransaccionPadre
��b t
,
��t u$
operacionInvalidacion��v �
.��� �
FechaActual��� �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �4
$IdTipoTransaccionInvalidacionDeVenta��� �
,��� �%
operacionInvalidacion��� �
.��� �!
IdUnidadDeNegocio��� �
,��� �
true��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �$
ObservacionOperacion��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �

IdEmpleado��� �
,��� �%
operacionInvalidacion��� �
.��� �
ImporteTotal��� �
,��� �%
operacionInvalidacion��� �
.��� � 
IdCentroAtencion��� �
,��� �%
operacionInvalidacion��� �
.��� �
IdMoneda��� �
,��� �%
operacionInvalidacion��� �
.��� �
TipoDeCambio��� �
,��� �
null��� �
,��� �%
operacionInvalidacion��� �
.��� �
	IdCliente��� �
)��� �
{
�� 
Comprobante
�� 
=
�� 
comprobante
�� )
,
��) *'
id_transaccion_referencia
�� )
=
��* +#
operacionInvalidacion
��, A
.
��A B
Venta
��B G
.
��G H
Id
��H J
}
�� 
;
��
return
�� !
invalidacionDeVenta
�� &
;
��& '
}
�� 	
public
�� 
Transaccion
�� ,
GenerarOrdenInvalidarOperacion
�� 9
(
��9 :
Transaccion
��: E
invalidacion
��F R
,
��R S#
OperacionInvalidacion
��T i#
operacionInvalidacion
��j 
)�� �
{
�� 	
Transaccion
�� $
ordenInvalidacionVenta
�� .
=
��/ 0
new
��1 4
Transaccion
��5 @
(
��@ A%
codigosOperacion_Logica
��A X
.
��X Y3
%ObtenerSiguienteCodigoParaFacturacion
��Y ~
(
��~ 
invalidacion�� �
.��� �
codigo��� �
+��� �
$str��� �
+��� �
Diccionario��� �
.��� �7
'MapeoTipoTransaccionVsCodigoDeOperacion��� �
.��� �
Single��� �
(��� �
c��� �
=>��� �
c��� �
.��� �
Key��� �
==��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �;
+IdTipoTransaccionOrdenDeInvalidacionDeVenta��� �
)��� �
.��� �
Value��� �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �;
+IdTipoTransaccionOrdenDeInvalidacionDeVenta��� �
)��� �
,��� �
null��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �;
+IdTipoTransaccionOrdenDeInvalidacionDeVenta��� �
,��� �%
operacionInvalidacion��� �
.��� �!
IdUnidadDeNegocio��� �
,��� �
true��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �$
ObservacionOperacion��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �

IdEmpleado��� �
,��� �%
operacionInvalidacion��� �
.��� �
ImporteTotal��� �
,��� �%
operacionInvalidacion��� �
.��� � 
IdCentroAtencion��� �
,��� �%
operacionInvalidacion��� �
.��� �
IdMoneda��� �
,��� �%
operacionInvalidacion��� �
.��� �
TipoDeCambio��� �
,��� �
null��� �
,��� �%
operacionInvalidacion��� �
.��� �
	IdCliente��� �
,��� �%
operacionInvalidacion��� �
.��� �
DescuentoGlobal��� �
,��� �%
operacionInvalidacion��� �
.��� � 
DescuentoPorItem��� �
,��� �%
operacionInvalidacion��� �
.��� �
Anticipo��� �
,��� �%
operacionInvalidacion��� �
.��� �
Gravada��� �
,��� �%
operacionInvalidacion��� �
.��� �
	Exonerada��� �
,��� �%
operacionInvalidacion��� �
.��� �
Inafecta��� �
,��� �%
operacionInvalidacion��� �
.��� �
Gratuita��� �
,��� �%
operacionInvalidacion��� �	
.���	 �	
Igv���	 �	
,���	 �	%
operacionInvalidacion���	 �	
.���	 �	
Isc���	 �	
,���	 �	%
operacionInvalidacion���	 �	
.���	 �	
Icbper���	 �	
,���	 �	%
operacionInvalidacion���	 �	
.���	 �	
OtrosCargos���	 �	
,���	 �	%
operacionInvalidacion���	 �

.���
 �


 �

)���
 �

{
�� 
Comprobante
�� 
=
�� 
invalidacion
�� *
.
��* +
Comprobante
��+ 6
,
��6 7'
id_transaccion_referencia
�� )
=
��* +#
operacionInvalidacion
��, A
.
��A B

OrdenVenta
��B L
.
��L M
Id
��M O
,
��O P'
id_actor_negocio_externo1
�� )
=
��* +#
operacionInvalidacion
��, A
.
��A B

OrdenVenta
��B L
.
��L M
IdGrupoCliente
��M [
,
��[ \
}
�� 
;
��
ordenInvalidacionVenta
�� "
.
��" #
AgregarDetalles
��# 2
(
��2 3#
operacionInvalidacion
��3 H
.
��H I
DetallesOperacion
��I Z
)
��Z [
;
��[ \
if
�� 
(
�� #
operacionInvalidacion
�� %
.
��% &
Icbper
��& ,
>
��- .
$num
��/ 0
)
��0 1
{
�� 
ordenInvalidacionVenta
�� &
.
��& '#
Parametro_transaccion
��' <
.
��< =
Add
��= @
(
��@ A
new
��A D#
Parametro_transaccion
��E Z
(
��Z [
MaestroSettings
��[ j
.
��j k
Default
��k r
.
��r s.
IdDetalleMaestroParametroIcbper��s �
,��� �%
operacionInvalidacion��� �
.��� �
Icbper��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� #
operacionInvalidacion
�� %
.
��% &"
NumeroBolsasPlastico
��& :
>
��; <
$num
��= >
)
��> ?
{
�� 
ordenInvalidacionVenta
�� &
.
��& '#
Parametro_transaccion
��' <
.
��< =
Add
��= @
(
��@ A
new
��A D#
Parametro_transaccion
��E Z
(
��Z [
MaestroSettings
��[ j
.
��j k
Default
��k r
.
��r s>
/IdDetalleMaestroParametroNumeroBolsasDePlastico��s �
,��� �%
operacionInvalidacion��� �
.��� �$
NumeroBolsasPlastico��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 

�� %
(
��% &#
operacionInvalidacion
��& ;
.
��; <
AliasCliente
��< H
)
��H I
)
��I J
{
�� 
ordenInvalidacionVenta
�� &
.
��& '#
Parametro_transaccion
��' <
.
��< =
Add
��= @
(
��@ A
new
��A D#
Parametro_transaccion
��E Z
(
��Z [
MaestroSettings
��[ j
.
��j k
Default
��k r
.
��r s4
%IdDetalleMaestroParametroAliasCliente��s �
,��� �%
operacionInvalidacion��� �
.��� �
AliasCliente��� �
)��� �
)��� �
;��� �
}
�� 
ordenInvalidacionVenta
�� "
.
��" ##
Parametro_transaccion
��# 8
.
��8 9
Add
��9 <
(
��< =
new
��= @#
Parametro_transaccion
��A V
(
��V W
MaestroSettings
��W f
.
��f g
Default
��g n
.
��n o2
#IdDetalleMaestroParametroModoDePago��o �
,��� �
(��� �
(��� �
int��� �
)��� �%
operacionInvalidacion��� �
.��� �

.��� �

ModoDePago��� �
)��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� � 
Estado_transaccion
�� 0
"estadoDeLaOrdenInvalidacionDeVenta
�� A
=
��B C
new
��D G 
Estado_transaccion
��H Z
(
��Z [#
operacionInvalidacion
��[ p
.
��p q

IdEmpleado
��q {
,
��{ |
MaestroSettings��} �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoConfirmado��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
;��� �$
ordenInvalidacionVenta
�� "
.
��" # 
Estado_transaccion
��# 5
.
��5 6
Add
��6 9
(
��9 :0
"estadoDeLaOrdenInvalidacionDeVenta
��: \
)
��\ ]
;
��] ^$
ordenInvalidacionVenta
�� "
.
��" #
enum1
��# (
=
��) *
(
��+ ,
int
��, /
)
��/ 0#
operacionInvalidacion
��0 E
.
��E F

OrdenVenta
��F P
.
��P Q%
IndicadorImpactoAlmacen
��Q h
;
��h i$
ordenInvalidacionVenta
�� "
.
��" #
enum1
��# (
=
��) *#
operacionInvalidacion
��+ @
.
��@ A$
EsOrdenOrigenPendiente
��A W
?
��X Y
(
��Z [
int
��[ ^
)
��^ _%
IndicadorImpactoAlmacen
��_ v
.
��v w5
&NoImpactaPorQueRevocaAOperacionInicial��w �
:��� �
(��� �%
operacionInvalidacion��� �
.��� �1
!HaySeccionEntregaAlmacenOperacion��� �
?��� �
(��� �%
operacionInvalidacion��� �
.��� �#
EsDiferidaOperacion��� �
?��� �
(��� �
int��� �
)��� �'
IndicadorImpactoAlmacen��� �
.��� �
Diferida��� �
:��� �
(��� �
int��� �
)��� �'
IndicadorImpactoAlmacen��� �
.��� �
	Inmediata��� �
)��� �
:��� �&
ordenInvalidacionVenta��� �
.��� �
enum1��� �
)��� �
;��� �
if
�� 
(
�� $
ordenInvalidacionVenta
�� &
.
��& '
enum1
��' ,
==
��- /
(
��0 1
int
��1 4
)
��4 5%
IndicadorImpactoAlmacen
��5 L
.
��L M
	Inmediata
��M V
||
��W Y$
ordenInvalidacionVenta
��Z p
.
��p q
enum1
��q v
==
��w y
(
��z {
int
��{ ~
)
��~ &
IndicadorImpactoAlmacen�� �
.��� �
Diferida��� �
)��� �
{
�� 
ordenInvalidacionVenta
�� &
.
��& ''
id_actor_negocio_interno1
��' @
=
��A B#
operacionInvalidacion
��C X
.
��X Y
	IdAlmacen
��Y b
;
��b c 
Estado_transaccion
�� " 
estadoOrdenAlmacen
��# 5
=
��6 7
new
��8 ; 
Estado_transaccion
��< N
(
��N O#
operacionInvalidacion
��O d
.
��d e

IdEmpleado
��e o
,
��o p$
operacionInvalidacion��q �
.��� �#
EsDiferidaOperacion��� �
?��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �/
IdDetalleMaestroEstadoPendiente��� �
:��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoCompletada��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
;��� �$
ordenInvalidacionVenta
�� &
.
��& ' 
Estado_transaccion
��' 9
.
��9 :
Add
��: =
(
��= > 
estadoOrdenAlmacen
��> P
)
��P Q
;
��Q R
}
�� 
invalidacion
�� 
.
�� 
Transaccion1
�� %
.
��% &
Add
��& )
(
��) *$
ordenInvalidacionVenta
��* @
)
��@ A
;
��A B
return
�� $
ordenInvalidacionVenta
�� )
;
��) *
}
�� 	
public
�� 
Transaccion
�� +
GenerarPagoInvalidarOperacion
�� 8
(
��8 9
Transaccion
��9 D
invalidacion
��E Q
,
��Q R
Transaccion
��S ^
ordenInvalidacion
��_ p
,
��p q$
OperacionInvalidacion��r �%
operacionInvalidacion��� �
)��� �
{
�� 	
Transaccion
�� 
pago
�� 
=
�� 
null
�� #
;
��# $
if
�� 
(
�� #
operacionInvalidacion
�� %
.
��% &
ImportePagoTotal
��& 6
>
��7 8
$num
��9 :
)
��: ;
{
�� 
if
�� 
(
�� #
operacionInvalidacion
�� )
.
��) *

��* 7
.
��7 8

ModoDePago
��8 B
==
��C E
ModoPago
��F N
.
��N O 
CreditoConfigurado
��O a
)
��a b
{
�� 
int
�� 
cont
�� 
=
�� 
$num
��  
;
��  !
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (#
operacionInvalidacion
��) >
.
��> ?

��? L
.
��L M

��M Z
(
��Z [
)
��[ \
)
��\ ]
{
�� 
var
�� 
cuota
�� !
=
��" #
new
��$ '
Cuota
��( -
(
��- .2
$ObtenerSiguienteCodigoParaNuevaCuota
��. R
(
��R S
false
��S X
,
��X Y#
operacionInvalidacion
��Z o
.
��o p
FechaActual
��p {
.
��{ |
Year��| �
)��� �
+��� �
$str��� �
+��� �
cont��� �
++��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �
item��� �
.��� �!
fecha_vencimiento��� �
,��� �
item��� �
.��� �
capital��� �
,��� �
item��� �
.��� �
interes��� �
,��� �
item��� �
.��� �
total��� �
,��� �
$str��� �
+��� �
cont��� �
,��� �
false��� �
,��� �
item��� �
.��� �

)��� �
;��� �
ordenInvalidacion
�� )
.
��) *
Cuota
��* /
.
��/ 0
Add
��0 3
(
��3 4
cuota
��4 9
)
��9 :
;
��: ;
}
�� 
var
�� 

diferencia
�� "
=
��# $#
operacionInvalidacion
��% :
.
��: ;
ImportePagoTotal
��; K
-
��L M
ordenInvalidacion
��N _
.
��_ `
Cuota
��` e
.
��e f
Sum
��f i
(
��i j
c
��j k
=>
��l n
c
��o p
.
��p q
total
��q v
)
��v w
;
��w x
ordenInvalidacion
�� %
.
��% &
Cuota
��& +
.
��+ ,
Last
��, 0
(
��0 1
)
��1 2
.
��2 3
total
��3 8
=
��9 :
ordenInvalidacion
��; L
.
��L M
Cuota
��M R
.
��R S
Last
��S W
(
��W X
)
��X Y
.
��Y Z
capital
��Z a
=
��b c
ordenInvalidacion
��d u
.
��u v
Cuota
��v {
.
��{ |
Last��| �
(��� �
)��� �
.��� �
total��� �
+��� �

diferencia��� �
;��� �
}
�� 
else
�� 
{
�� 
var
�� 
cuota
�� 
=
�� 
new
��  #
Cuota
��$ )
(
��) *2
$ObtenerSiguienteCodigoParaNuevaCuota
��* N
(
��N O
false
��O T
,
��T U#
operacionInvalidacion
��V k
.
��k l
FechaActual
��l w
.
��w x
Year
��x |
)
��| }
+
��~ 
$str��� �
+��� �
$num��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� � 
ImportePagoTotal��� �
,��� �
$str��� �
,��� �
false��� �
)��� �
;��� �
ordenInvalidacion
�� %
.
��% &
Cuota
��& +
.
��+ ,
Add
��, /
(
��/ 0
cuota
��0 5
)
��5 6
;
��6 7
}
�� 
if
�� 
(
�� #
operacionInvalidacion
�� )
.
��) *

��* 7
.
��7 8
HayIngresoDinero
��8 H
)
��H I
{
�� 
var
�� +
esCreditoRapidoConPagoInicial
�� 5
=
��6 7
(
��8 9#
operacionInvalidacion
��9 N
.
��N O

��O \
.
��\ ]

ModoDePago
��] g
==
��h j
ModoPago
��k s
.
��s t

&&��� �%
operacionInvalidacion��� �
.��� �

.��� �
Inicial��� �
>��� �
$num��� �
)��� �
;��� �
Cuota
�� 
cuotaACobrar
�� &
=
��' (
(
��) *#
operacionInvalidacion
��* ?
.
��? @

��@ M
.
��M N

ModoDePago
��N X
==
��Y [
ModoPago
��\ d
.
��d e
Contado
��e l
||
��m o,
esCreditoRapidoConPagoInicial��p �
)��� �
?��� �!
ordenInvalidacion��� �
.��� �
Cuota��� �
.��� �
First��� �
(��� �
)��� �
:��� �!
ordenInvalidacion��� �
.��� �
Cuota��� �
.��� �
Single��� �
(��� �
c��� �
=>��� �
c��� �
.��� �

)��� �
;��� �
cuotaACobrar
��  
.
��  !
SetPagoACuenta
��! /
(
��/ 0+
esCreditoRapidoConPagoInicial
��0 M
?
��N O#
operacionInvalidacion
��P e
.
��e f

��f s
.
��s t
Inicial
��t {
:
��| }
cuotaACobrar��~ �
.��� �
total��� �
)��� �
;��� �"
ValidarImporteAPagar
�� (
(
��( )
$num
��) *
,
��* +
cuotaACobrar
��, 8
.
��8 9
total
��9 >
,
��> ?
ordenInvalidacion
��@ Q
.
��Q R

��R _
)
��_ `
;
��` a
pago
�� 
=
�� 8
*GenerarMovimientoEconomicoPagoACuentaCuota
�� E
(
��E F
invalidacion
��F R
,
��R S
cuotaACobrar
��T `
,
��` a#
operacionInvalidacion
��b w
.
��w x

IdEmpleado��x �
,��� �%
operacionInvalidacion��� �
.��� �
IdCaja��� �
,��� �%
operacionInvalidacion��� �
.��� �
	IdCliente��� �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �E
5IdTipoTransaccionSalidaDeDineroPorInvalidacionDeVenta��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �$
ObservacionOperacion��� �
,��� �%
operacionInvalidacion��� �
.��� �

.��� �
Traza��� �
.��� �
MedioDePago��� �
.��� �
Id��� �
,��� �%
operacionInvalidacion��� �
.��� �

.��� �
Traza��� �
.��� �
Info��� �
.��� �!
EntidadFinanciera��� �
.��� �
Id��� �
,��� �%
operacionInvalidacion��� �
.��� �

.��� �
Traza��� �
.��� �
Info��� �
.��� �
Observacion��� �
)��� �
;��� �
if
�� 
(
�� #
operacionInvalidacion
�� -
.
��- .

��. ;
.
��; <
Traza
��< A
.
��A B
MedioDePago
��B M
.
��M N
Id
��N P
==
��Q S
MaestroSettings
��T c
.
��c d
Default
��d k
.
��k l0
!IdDetalleMaestroMedioDepagoPuntos��l �
)��� �
{
�� 
pago
�� 
.
�� 
	cantidad1
�� &
=
��' (#
operacionInvalidacion
��) >
.
��> ?

��? L
.
��L M
Traza
��M R
.
��R S
Info
��S W
.
��W X
PuntosCajeados
��X f
;
��f g
}
�� 
if
�� 
(
�� #
operacionInvalidacion
�� -
.
��- .

��. ;
.
��; <
Traza
��< A
.
��A B
MedioDePago
��B M
.
��M N
Id
��N P
==
��Q S
MaestroSettings
��T c
.
��c d
Default
��d k
.
��k l:
+IdDetalleMaestroMedioDepagoDepositoEnCuenta��l �
||��� �%
operacionInvalidacion��� �
.��� �

.��� �
Traza��� �
.��� �
MedioDePago��� �
.��� �
Id��� �
==��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �@
0IdDetalleMaestroMedioDepagoTransferenciaDeFondos��� �
)��� �
{
�� 
pago
�� 
.
�� '
id_actor_negocio_interno1
�� 6
=
��7 8#
operacionInvalidacion
��9 N
.
��N O

��O \
.
��\ ]
Traza
��] b
.
��b c
Info
��c g
.
��g h
CuentaBancaria
��h v
.
��v w
Id
��w y
;
��y z
}
�� 
if
�� 
(
�� 
!
�� 
string
�� 
.
��  

��  -
(
��- .#
operacionInvalidacion
��. C
.
��C D

��D Q
.
��Q R
Traza
��R W
.
��W X
Info
��X \
.
��\ ]
InformacionJson
��] l
)
��l m
)
��m n
pago
��o s
.
��s t

Traza_pago
��t ~
.
��~ 
First�� �
(��� �
)��� �
.��� �
extension_json��� �
=��� �%
operacionInvalidacion��� �
.��� �

.��� �
Traza��� �
.��� �
Info��� �
.��� �
InformacionJson��� �
;��� �
invalidacion
��  
.
��  !
Transaccion1
��! -
.
��- .
Add
��. 1
(
��1 2
pago
��2 6
)
��6 7
;
��7 8
}
�� 
}
�� 
return
�� 
pago
�� 
;
�� 
}
�� 	
public
�� 
Transaccion
�� 8
*GenerarMovimientoAlmacenInvalidarOperacion
�� E
(
��E F
Transaccion
��F Q
	operacion
��R [
,
��[ \
Transaccion
��] h
ordenInvalidacion
��i z
,
��z {$
OperacionInvalidacion��| �%
operacionInvalidacion��� �
,��� �&
UserProfileSessionData��� �

)��� �
{
�� 	
Transaccion
�� 3
%entradaMercaderiaPorInvalidacionVenta
�� =
=
��> ?
null
��@ D
;
��D E
if
�� 
(
�� #
operacionInvalidacion
�� %
.
��% &+
HayMovimientoAlmacenOperacion
��& C
)
��C D
{
�� 
var
�� $
salidasMercaderiaVenta
�� *
=
��+ ,#
operacionInvalidacion
��- B
.
��B C
Venta
��C H
.
��H I(
ObtenerSalidasDeMercaderia
��I c
(
��c d
)
��d e
;
��e f
if
�� 
(
�� $
salidasMercaderiaVenta
�� *
.
��* +
Count
��+ 0
>
��1 2
$num
��3 4
)
��4 5
{
�� 3
%entradaMercaderiaPorInvalidacionVenta
�� 9
=
��: ;+
GenerarMovimientoDeMercaderia
��< Y
(
��Y Z
	operacion
��Z c
,
��c d#
operacionInvalidacion
��e z
.
��z {

IdEmpleado��{ �
,��� �%
operacionInvalidacion��� �
.��� �
	IdAlmacen��� �
,��� �%
operacionInvalidacion��� �
.��� �
	IdCliente��� �
,��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �J
:IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �%
operacionInvalidacion��� �
.��� �$
ObservacionOperacion��� �
,��� �%
operacionInvalidacion��� �
.��� �2
"DetallesMovimientoAlmacenOperacion��� �
,��� �

,��� �&
salidasMercaderiaVenta��� �
.��� �
First��� �
(��� �
)��� �
.��� �
Id��� �
)��� �
;��� �3
%entradaMercaderiaPorInvalidacionVenta
�� 9
.
��9 :
Transaccion3
��: F
=
��G H
ordenInvalidacion
��I Z
;
��Z [
	operacion
�� 
.
�� 
Transaccion1
�� *
.
��* +
Add
��+ .
(
��. /3
%entradaMercaderiaPorInvalidacionVenta
��/ T
)
��T U
;
��U V
}
�� 
if
�� 
(
�� #
operacionInvalidacion
�� )
.
��) *"
EsOrdenOrigenParcial
��* >
)
��> ?
{
�� 
Serie_comprobante
�� %
serie
��& +
=
��, -$
transaccionRepositorio
��. D
.
��D EN
?ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante��E �
(��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �=
-IdDetalleMaestroComprobanteNotaAlmacenInterna��� �
,��� �

.��� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �3
%entradaMercaderiaPorInvalidacionVenta
�� 9
.
��9 :
Comprobante
��: E
=
��F G3
%GenerarComprobantePropioAutonumerable
��H m
(
��m n
serie
��n s
.
��s t
id
��t v
)
��v w
;
��w x
}
�� 
}
�� 
return
�� 3
%entradaMercaderiaPorInvalidacionVenta
�� 8
;
��8 9
}
�� 	
public
�� 
void
�� @
2GenerarEstadosOperacionInvalidadaYCuotasActualizar
�� F
(
��F G
List
��G K
<
��K L 
Estado_transaccion
��L ^
>
��^ _&
nuevosEstadosTransaccion
��` x
,
��x y
List
��z ~
<
��~ 
Cuota�� �
>��� � 
cuotasActualizar��� �
,��� �%
OperacionInvalidacion��� �%
operacionInvalidacion��� �
)��� �
{
�� 	&
nuevosEstadosTransaccion
�� $
.
��$ %
Add
��% (
(
��( )
new
��) , 
Estado_transaccion
��- ?
(
��? @#
operacionInvalidacion
��@ U
.
��U V

OrdenVenta
��V `
.
��` a
Id
��a c
,
��c d#
operacionInvalidacion
��e z
.
��z {

IdEmpleado��{ �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoInvalidado��� �
,��� �%
operacionInvalidacion��� �
.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� #
operacionInvalidacion
�� &
.
��& '$
EsCompletoEstadoCuotas
��' =
)
��= >
{
�� 
foreach
�� 
(
�� 
var
�� 
cuota
�� "
in
��# %#
operacionInvalidacion
��& ;
.
��; <
Cuotas
��< B
.
��B C
OrderByDescending
��C T
(
��T U
c
��U V
=>
��W Y
c
��Z [
.
��[ \
fecha_vencimiento
��\ m
)
��m n
)
��n o
{
�� 
if
�� 
(
�� 
cuota
�� 
.
�� 
saldo
�� #
>
��$ %
$num
��& '
)
��' (
{
�� 
cuota
�� 
.
�� 
revocado
�� &
=
��' (
cuota
��) .
.
��. /
saldo
��/ 4
;
��4 5
cuota
�� 
.
�� 
saldo
�� #
=
��$ %
cuota
��& +
.
��+ ,
total
��, 1
-
��2 3
cuota
��4 9
.
��9 :

��: G
-
��H I
cuota
��J O
.
��O P
revocado
��P X
;
��X Y
cuotasActualizar
�� (
.
��( )
Add
��) ,
(
��, -
cuota
��- 2
)
��2 3
;
��3 4
}
�� 
}
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
Transaccion
�� 
>
��  ,
GenerarTransaccionesAModificar
��! ?
(
��? @!
MovimientoEconomico
��@ S*
movimientoEconomicoConPuntos
��T p
)
��p q
{
�� 	
List
�� 
<
�� 
Transaccion
�� 
>
�� %
transaccionesAModificar
�� 5
=
��6 7
null
��8 <
;
��< =
if
�� 
(
�� *
movimientoEconomicoConPuntos
�� ,
!=
��- /
null
��0 4
)
��4 5
{
�� 
var
��  
extensionJsonTraza
�� &
=
��' (*
movimientoEconomicoConPuntos
��) E
.
��E F
TrazaDePago
��F Q
(
��Q R
)
��R S
.
��S T

��T a
;
��a b
var
�� 
puntosCanjeados
�� #
=
��$ %
JsonConvert
��& 1
.
��1 2
DeserializeObject
��2 C
<
��C D
List
��D H
<
��H I

��I V
>
��V W
>
��W X
(
��X Y 
extensionJsonTraza
��Y k
)
��k l
;
��l m
var
�� #
transaccionesDePuntos
�� )
=
��* +$
transaccionRepositorio
��, B
.
��B C"
ObtenerTransacciones
��C W
(
��W X
puntosCanjeados
��X g
.
��g h
Select
��h n
(
��n o
pc
��o q
=>
��r t
pc
��u w
.
��w x
Id
��x z
)
��z {
.
��{ |
ToArray��| �
(��� �
)��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  
transaccionDePunto
�� /
in
��0 2#
transaccionesDePuntos
��3 H
)
��H I
{
�� %
transaccionesAModificar
�� +
=
��, -
new
��. 1
List
��2 6
<
��6 7
Transaccion
��7 B
>
��B C
(
��C D
)
��D E
;
��E F
var
�� ,
puntosPendientesDeModificacion
�� 6
=
��7 8
puntosCanjeados
��9 H
.
��H I
Single
��I O
(
��O P
pc
��P R
=>
��S U
pc
��V X
.
��X Y
Id
��Y [
==
��\ ^ 
transaccionDePunto
��_ q
.
��q r
id
��r t
)
��t u
.
��u v
Cantidad
��v ~
;
��~  
transaccionDePunto
�� &
.
��& '
	cantidad2
��' 0
-=
��1 3,
puntosPendientesDeModificacion
��4 R
;
��R S 
transaccionDePunto
�� &
.
��& '
	cantidad3
��' 0
+=
��1 3,
puntosPendientesDeModificacion
��4 R
;
��R S%
transaccionesAModificar
�� +
.
��+ ,
Add
��, /
(
��/ 0 
transaccionDePunto
��0 B
)
��B C
;
��C D
}
�� 
}
�� 
return
�� %
transaccionesAModificar
�� *
;
��* +
}
�� 	
}
�� 
}�� ߽
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\Core\Venta\OperacionLogica_CreditoVenta.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Logica 
{ 
public 

partial 
class 
OperacionLogica (
{ 
public 
OperationResult )
DescuentoGlobalOperacionVenta <
(< =
long= A
idOrdenVentaB N
,N O
decimalP W
importeX _
,_ `
stringa g
observacionh s
,s t
intu x
idEventoReferencia	y �
,
� �$
UserProfileSessionData
� �

� �
)
� �
{ 	
try 
{ 
Comprobante 
comprobante '
=( )"
transaccionRepositorio* @
.@ A+
ObtenerComprobanteDeTransaccionA `
(` a
idOrdenVentaa m
)m n
;n o
var 
idTipoComprobante %
=& '
comprobante( 3
.3 4
id_tipo_comprobante4 G
==H J
MaestroSettingsK Z
.Z [
Default[ b
.b c8
+IdDetalleMaestroComprobanteNotaVentaInterna	c �
?
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �;
-IdDetalleMaestroComprobanteNotaCreditoInterna
� �
:
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �4
&IdDetalleMaestroComprobanteNotaCredito
� �
;
� �
var 
prefijo 
= 
comprobante )
.) *
id_tipo_comprobante* =
==> @
MaestroSettingsA P
.P Q
DefaultQ X
.X Y-
!IdDetalleMaestroComprobanteBoletaY z
?{ |+
FacturacionElectronicaSettings	} �
.
� �
Default
� �
.
� �5
'PrefijoSerieNotaCreditoNotaDebitoBoleta
� �
:
� �
(
� �
comprobante
� �
.
� �!
id_tipo_comprobante
� �
==
� �
MaestroSettings
� �
.
� �
Default
� �
.
� �0
"IdDetalleMaestroComprobanteFactura
� �
?
� �,
FacturacionElectronicaSettings
� �
.
� �
Default
� �
.
� �6
(PrefijoSerieNotaCreditoNotaDebitoFactura
� �
:
� �
String
� �
.
� �
Empty
� �
)
� �
;
� �
var   
serie   
=   
comprobante   '
.  ' (
id_tipo_comprobante  ( ;
==  < >
MaestroSettings  ? N
.  N O
Default  O V
.  V W8
+IdDetalleMaestroComprobanteNotaVentaInterna	  W �
?
  � �"
transaccionRepositorio!! *
.!!* +K
?ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante!!+ j
(!!j k
idTipoComprobante!!k |
,!!| }

.
!!� �,
IdCentroDeAtencionSeleccionado
!!� �
)
!!� �
:
!!� �"
transaccionRepositorio"" *
.""* +X
LObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobanteYPrefijoSerie""+ w
(""w x
idTipoComprobante	""x �
,
""� �

""� �
.
""� �,
IdCentroDeAtencionSeleccionado
""� �
,
""� �
prefijo
""� �
)
""� �
;
""� �
return## '
GuardarNotaDeCreditoDeVenta## 2
(##2 3
idOrdenVenta##3 ?
,##? @
MaestroSettings##A P
.##P Q
Default##Q X
.##X YD
7IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal	##Y �
,
##� �
observacion
##� �
,
##� �
idTipoComprobante
##� �
,
##� �
serie
##� �
.
##� �
id
##� �
,
##� �
true
##� �
,
##� �
String
##� �
.
##� �
Empty
##� �
,
##� �
$num
##� �
,
##� �
importe
##� �
.
##� �
ToString
##� �
(
##� �
)
##� �
,
##� � 
idEventoReferencia
##� �
,
##� �
null
##� �
,
##� �

##� �
)
##� �
;
##� �
}$$ 
catch%% 
(%% 
	Exception%% 
e%% 
)%% 
{&& 
throw'' 
new'' 
LogicaException'' )
('') *
$str''* c
,''c d
e''e f
)''f g
;''g h
}(( 
})) 	
public** 
OperationResult**  
AnularOperacionVenta** 3
(**3 4
long**4 8
idOrdenVenta**9 E
,**E F
bool**G K
	darDeBaja**L U
,**U V
string**W ]
observacion**^ i
,**i j
int**k n
idEventoReferencia	**o �
,
**� �$
UserProfileSessionData
**� �

**� �
)
**� �
{++ 	
try,, 
{-- 
OperationResult.. 
	resultado..  )
;..) *
Comprobante// 
comprobante// '
=//( )"
transaccionRepositorio//* @
.//@ A+
ObtenerComprobanteDeTransaccion//A `
(//` a
idOrdenVenta//a m
)//m n
;//n o
var00 
idTipoComprobante00 %
=00& '
	darDeBaja00( 1
?002 3
MaestroSettings004 C
.00C D
Default00D K
.00K L9
-IdDetalleMaestroComprobanteNotaCreditoInterna00L y
:00z {
MaestroSettings	00| �
.
00� �
Default
00� �
.
00� �4
&IdDetalleMaestroComprobanteNotaCredito
00� �
;
00� �
var11 
prefijo11 
=11 
(11 
comprobante11 *
.11* +
id_tipo_comprobante11+ >
==11? A
MaestroSettings11B Q
.11Q R
Default11R Y
.11Y Z-
!IdDetalleMaestroComprobanteBoleta11Z {
?11| }+
FacturacionElectronicaSettings	11~ �
.
11� �
Default
11� �
.
11� �5
'PrefijoSerieNotaCreditoNotaDebitoBoleta
11� �
:
11� �
(
11� �
comprobante
11� �
.
11� �!
id_tipo_comprobante
11� �
==
11� �
MaestroSettings
11� �
.
11� �
Default
11� �
.
11� �0
"IdDetalleMaestroComprobanteFactura
11� �
?
11� �,
FacturacionElectronicaSettings
11� �
.
11� �
Default
11� �
.
11� �6
(PrefijoSerieNotaCreditoNotaDebitoFactura
11� �
:
11� �
String
11� �
.
11� �
Empty
11� �
)
11� �
)
11� �
;
11� �
var22 
serie22 
=22 
	darDeBaja22 %
?22& '"
transaccionRepositorio22( >
.22> ?K
?ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante22? ~
(22~ 
idTipoComprobante	22 �
,
22� �

22� �
.
22� �,
IdCentroDeAtencionSeleccionado
22� �
)
22� �
:
22� �$
transaccionRepositorio
22� �
.
22� �Z
LObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobanteYPrefijoSerie
22� �
(
22� �
idTipoComprobante
22� �
,
22� �

22� �
.
22� �,
IdCentroDeAtencionSeleccionado
22� �
,
22� �
prefijo
22� �
)
22� �
;
22� �
if33 
(33 
	darDeBaja33 
)33 
{44 
var55  
operacionNotaCredito55 ,
=55- .#
GenerarNotaCreditoVenta55/ F
(55F G
idOrdenVenta55G S
,55S T
MaestroSettings55U d
.55d e
Default55e l
.55l mK
>IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion	55m �
,
55� �
observacion
55� �
,
55� �
idTipoComprobante
55� �
,
55� �
serie
55� �
.
55� �
id
55� �
,
55� �
true
55� �
,
55� �
String
55� �
.
55� �
Empty
55� �
,
55� �
$num
55� �
,
55� �
String
55� �
.
55� �
Empty
55� �
,
55� � 
idEventoReferencia
55� �
,
55� �
null
55� �
,
55� �

55� �
)
55� �
;
55� �
	resultado66 
=66 +
ResolverInvalidarOperacionVenta66  ?
(66? @ 
operacionNotaCredito66@ T
,66T U
idOrdenVenta66V b
,66b c
observacion66d o
,66o p

)66~ 
;	66 �
}77 
else88 
{99 
	resultado:: 
=:: '
GuardarNotaDeCreditoDeVenta::  ;
(::; <
idOrdenVenta::< H
,::H I
MaestroSettings::J Y
.::Y Z
Default::Z a
.::a bK
>IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion	::b �
,
::� �
observacion
::� �
,
::� �
idTipoComprobante
::� �
,
::� �
serie
::� �
.
::� �
id
::� �
,
::� �
true
::� �
,
::� �
String
::� �
.
::� �
Empty
::� �
,
::� �
$num
::� �
,
::� �
String
::� �
.
::� �
Empty
::� �
,
::� � 
idEventoReferencia
::� �
,
::� �
null
::� �
,
::� �

::� �
)
::� �
;
::� �
};; 
return<< 
	resultado<<  
;<<  !
}== 
catch>> 
(>> 
	Exception>> 
e>> 
)>> 
{?? 
throw@@ 
new@@ 
LogicaException@@ )
(@@) *
$str@@* c
,@@c d
e@@e f
)@@f g
;@@g h
}AA 
}BB 	
publicDD "
OperacionModificatoriaDD %#
GenerarNotaCreditoVentaDD& =
(DD= >
longDD> B
idOrdenVentaDDC O
,DDO P
intDDQ T

idTipoNotaDDU _
,DD_ `
stringDDa g
motivoDDh n
,DDn o
intDDp s
idTipoComprobante	DDt �
,
DD� �
int
DD� � 
idSerieComprobante
DD� �
,
DD� �
bool
DD� �
esPropio
DD� �
,
DD� �
string
DD� �&
numeroSerieDeComprobante
DD� �
,
DD� �
int
DD� �!
numeroDeComprobante
DD� �
,
DD� �
string
DD� �
valorDeNota
DD� �
,
DD� �
int
DD� � 
idEventoReferencia
DD� �
,
DD� �
List
DD� �
<
DD� � 
DetalleOrdenDeNota
DD� �
>
DD� �
detalles
DD� �
,
DD� �$
UserProfileSessionData
DD� �

DD� �
)
DD� �
{EE 	
tryFF 
{GG 
varHH 
fechaActualHH 
=HH  !
DateTimeUtilHH" .
.HH. /
FechaActualHH/ :
(HH: ;
)HH; <
;HH< =
varII 

ordenVentaII 
=II  
newII! $
OrdenDeVentaII% 1
(II1 2"
transaccionRepositorioII2 H
.IIH IE
8ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado	III �
(
II� �
idOrdenVenta
II� �
)
II� �
)
II� �
;
II� �
varJJ 
ventaJJ 
=JJ 
newJJ 
VentaJJ  %
(JJ% &"
transaccionRepositorioJJ& <
.JJ< =.
"ObtenerTransaccionInclusiveEstadosJJ= _
(JJ_ `

ordenVentaJJ` j
.JJj k
IdVentaJJk r
)JJr s
)JJs t
;JJt u
ListLL 
<LL 
Detalle_transaccionLL (
>LL( )
detallesNotaLL* 6
=LL7 8,
 CalcularDetalleNotaDebitoCreditoLL9 Y
(LLY Z
detallesLLZ b
,LLb c

ordenVentaLLd n
.LLn o
DetalleTransaccion	LLo �
(
LL� �
)
LL� �
,
LL� �

idTipoNota
LL� �
,
LL� �
valorDeNota
LL� �
,
LL� �
motivo
LL� �
,
LL� �

ordenVenta
LL� �
.
LL� �
Igv
LL� �
(
LL� �
)
LL� �
>
LL� �
$num
LL� �
)
LL� �
;
LL� �
varMM 
importeTotalMM  
=MM! "
detallesNotaMM# /
.MM/ 0
SumMM0 3
(MM3 4
dMM4 5
=>MM6 8
dMM9 :
.MM: ;
totalMM; @
)MM@ A
+MMB C

ordenVentaMMD N
.MMN O
IcbperMMO U
(MMU V
)MMV W
;MMW X%
ValidarNotaCreditoEnVentaNN )
(NN) *

ordenVentaNN* 4
,NN4 5
importeTotalNN6 B
)NNB C
;NNC D
intOO 
idUnidadNegocioOO #
=OO$ %
MaestroSettingsOO& 5
.OO5 6
DefaultOO6 =
.OO= >6
*IdDetalleMaestroUnidadDeNegocioTransversalOO> h
;OOh i
intQQ 3
'idTipoTransaccionMovimientoDeMercaderiaQQ ;
=QQ< =
DiccionarioQQ> I
.QQI J+
MapeoOrdenVsMovimientoDeAlmacenQQJ i
.QQi j
SingleOrDefaultQQj y
(QQy z
lQQz {
=>QQ| ~
l	QQ �
.
QQ� �
Key
QQ� �
==
QQ� �
Diccionario
QQ� �
.
QQ� � 
MapeoWraperVsOrden
QQ� �
.
QQ� �
Single
QQ� �
(
QQ� �
m
QQ� �
=>
QQ� �
m
QQ� �
.
QQ� �
Key
QQ� �
==
QQ� �
Diccionario
QQ� �
.
QQ� �;
-MapeoDetalleMaestroVsTipoTransaccionParaVenta
QQ� �
.
QQ� �
Single
QQ� �
(
QQ� �
n
QQ� �
=>
QQ� �
n
QQ� �
.
QQ� �
Key
QQ� �
==
QQ� �

idTipoNota
QQ� �
)
QQ� �
.
QQ� �
Value
QQ� �
)
QQ� �
.
QQ� �
Value
QQ� �
)
QQ� �
.
QQ� �
Value
QQ� �
;
QQ� �
permisos_LogicaSS 
.SS  

(SS- .

.SS; <
EmpleadoSS< D
.SSD E
IdSSE G
,SSG H
MaestroSettingsSSI X
.SSX Y
DefaultSSY `
.SS` a,
IdDetalleMaestroAccionConfirmar	SSa �
,
SS� �
Diccionario
SS� �
.
SS� � 
MapeoWraperVsOrden
SS� �
.
SS� �
Single
SS� �
(
SS� �
m
SS� �
=>
SS� �
m
SS� �
.
SS� �
Key
SS� �
==
SS� �
Diccionario
SS� �
.
SS� �;
-MapeoDetalleMaestroVsTipoTransaccionParaVenta
SS� �
.
SS� �
Single
SS� �
(
SS� �
n
SS� �
=>
SS� �
n
SS� �
.
SS� �
Key
SS� �
==
SS� �

idTipoNota
SS� �
)
SS� �
.
SS� �
Value
SS� �
)
SS� �
.
SS� �
Value
SS� �
,
SS� �
idUnidadNegocio
SS� �
)
SS� �
;
SS� �
TransaccionTT 
notaCreditoTT '
=TT( )&
GenerarNotaDeCreditoDebitoTT* D
(TTD E

.TTR S
EmpleadoTTS [
.TT[ \
IdTT\ ^
,TT^ _
idUnidadNegocioTT` o
,TTo p
esPropioTTq y
,TTy z
idSerieComprobante	TT{ �
,
TT� �
idTipoComprobante
TT� �
,
TT� �!
numeroDeComprobante
TT� �
,
TT� �&
numeroSerieDeComprobante
TT� �
,
TT� �
fechaActual
TT� �
,
TT� �
$str
TT� �
,
TT� �
Diccionario
TT� �
.
TT� �;
-MapeoDetalleMaestroVsTipoTransaccionParaVenta
TT� �
.
TT� �
Single
TT� �
(
TT� �
n
TT� �
=>
TT� �
n
TT� �
.
TT� �
Key
TT� �
==
TT� �

idTipoNota
TT� �
)
TT� �
.
TT� �
Value
TT� �
,
TT� �
importeTotal
TT� �
,
TT� �
motivo
TT� �
,
TT� �

ordenVenta
TT� �
.
TT� �
Cliente
TT� �
(
TT� �
)
TT� �
.
TT� �
Id
TT� �
,
TT� �

TT� �
.
TT� �,
IdCentroDeAtencionSeleccionado
TT� �
,
TT� �

TT� �
)
TT� �
;
TT� �
ModoPagoUU 
modoPagoUU !
=UU" #

ordenVentaUU$ .
.UU. /

ModoDePagoUU/ 9
(UU9 :
)UU: ;
;UU; <
TransaccionVV 
ordenNotaCreditoVV ,
=VV- .+
GenerarOrdenNotaDeCreditoDebitoVV/ N
(VVN O
notaCreditoVVO Z
,VVZ [

.VVi j
EmpleadoVVj r
.VVr s
IdVVs u
,VVu v
idUnidadNegocio	VVw �
,
VV� �

idTipoNota
VV� �
,
VV� �
fechaActual
VV� �
,
VV� �
(
VV� �
(
VV� �
int
VV� �
)
VV� �
modoPago
VV� �
)
VV� �
.
VV� �
ToString
VV� �
(
VV� �
)
VV� �
,
VV� �
$str
VV� �
,
VV� �
Diccionario
VV� �
.
VV� � 
MapeoWraperVsOrden
VV� �
.
VV� �
Single
VV� �
(
VV� �
m
VV� �
=>
VV� �
m
VV� �
.
VV� �
Key
VV� �
==
VV� �
Diccionario
VV� �
.
VV� �;
-MapeoDetalleMaestroVsTipoTransaccionParaVenta
VV� �
.
VV� �
Single
VV� �
(
VV� �
n
VV� �
=>
VV� �
n
VV� �
.
VV� �
Key
VV� �
==
VV� �

idTipoNota
VV� �
)
VV� �
.
VV� �
Value
VV� �
)
VV� �
.
VV� �
Value
VV� �
,
VV� �
motivo
VV� �
,
VV� �

ordenVenta
VV� �
.
VV� �
Cliente
VV� �
(
VV� �
)
VV� �
.
VV� �
Id
VV� �
,
VV� �

ordenVenta
VV� �
.
VV� �
AliasCliente
VV� �
(
VV� �
)
VV� �
,
VV� �

VV� �
.
VV� �,
IdCentroDeAtencionSeleccionado
VV� �
,
VV� �
detallesNota
VV� �
,
VV� �
MaestroSettings
VV� �
.
VV� �
Default
VV� �
.
VV� �.
 IdDetalleMaestroEstadoConfirmado
VV� �
,
VV� �
$str
VV� �
,
VV� �
true
VV� �
,
VV� �5
'idTipoTransaccionMovimientoDeMercaderia
VV� �
!=
VV� �
$num
VV� �
)
VV� �
;
VV� �
ifWW 
(WW 
idEventoReferenciaWW &
!=WW' )
$numWW* +
)WW+ ,
ordenNotaCreditoWW- =
.WW= > 
id_evento_referenciaWW> R
=WWS T
idEventoReferenciaWWU g
;WWg h%
ResolverIcbperNotaCreditoXX )
(XX) *

ordenVentaXX* 4
,XX4 5
ordenNotaCreditoXX6 F
)XXF G
;XXG H
ordenNotaCreditoYY  
.YY  !%
id_transaccion_referenciaYY! :
=YY; <

ordenVentaYY= G
.YYG H
IdYYH J
;YYJ K
notaCreditoZZ 
.ZZ 
Transaccion1ZZ (
.ZZ( )
AddZZ) ,
(ZZ, -
ordenNotaCreditoZZ- =
)ZZ= >
;ZZ> ?
Transaccion[[ 
pagoNota[[ $
=[[% &#
DevolverPagoNotaCredito[[' >
([[> ?
notaCredito[[? J
,[[J K
ordenNotaCredito[[L \
,[[\ ]
fechaActual[[^ i
,[[i j

idTipoNota[[k u
,[[u v
modoPago[[w 
,	[[ �
importeTotal
[[� �
,
[[� �

[[� �
)
[[� �
;
[[� �
Transaccion\\  
salidaMercaderiaNota\\ 0
=\\1 2/
#DevolverSalidaMercaderiaNotaCredito\\3 V
(\\V W
venta\\W \
,\\\ ]

ordenVenta\\^ h
,\\h i
notaCredito\\j u
,\\u v
ordenNotaCredito	\\w �
,
\\� �
fechaActual
\\� �
,
\\� �5
'idTipoTransaccionMovimientoDeMercaderia
\\� �
,
\\� �
detallesNota
\\� �
,
\\� �

\\� �
)
\\� �
;
\\� �
var]] "
operacionModificatoria]] *
=]]+ ,
new]]- 0"
OperacionModificatoria]]1 G
{^^ 
	Operacion__ 
=__ 
notaCredito__  +
,__+ ,
OrdenDeOperacion`` $
=``% &
ordenNotaCredito``' 7
,``7 8
MovimientoEconomicoaa '
=aa( )
pagoNotaaa* 2
,aa2 3
MovimientosBienesbb %
=bb& ' 
salidaMercaderiaNotabb( <
==bb= ?
nullbb@ D
?bbE F
nullbbG K
:bbL M
newbbN Q
ListbbR V
<bbV W
TransaccionbbW b
>bbb c
(bbc d
)bbd e
{bbf g 
salidaMercaderiaNotabbh |
}bb} ~
}cc 
;cc 
returndd "
operacionModificatoriadd -
;dd- .
}ee 
catchff 
(ff 
	Exceptionff 
eff 
)ff 
{gg 
throwhh 
newhh 
LogicaExceptionhh )
(hh) *
$strhh* j
,hhj k
ehhl m
)hhm n
;hhn o
}ii 
}jj 	
publickk 
voidkk %
ResolverIcbperNotaCreditokk -
(kk- .
OrdenDeVentakk. :
ordenDeVentakk; G
,kkG H
TransaccionkkI T 
ordenDeNotaDeCreditokkU i
)kki j
{ll 	
ifmm 
(mm 
ordenDeVentamm 
.mm 
Icbpermm #
(mm# $
)mm$ %
>mm& '
$nummm( )
)mm) *
{nn 
ordenDeNotaDeCreditooo $
.oo$ %
	importe10oo% .
=oo/ 0
ordenDeVentaoo1 =
.oo= >
Icbperoo> D
(ooD E
)ooE F
;ooF G 
ordenDeNotaDeCreditopp $
.pp$ %!
Parametro_transaccionpp% :
.pp: ;
Addpp; >
(pp> ?
newpp? B!
Parametro_transaccionppC X
(ppX Y
MaestroSettingsppY h
.pph i
Defaultppi p
.ppp q,
IdDetalleMaestroParametroIcbper	ppq �
,
pp� �
ordenDeVenta
pp� �
.
pp� �
Icbper
pp� �
(
pp� �
)
pp� �
.
pp� �
ToString
pp� �
(
pp� �
)
pp� �
)
pp� �
)
pp� �
;
pp� � 
ordenDeNotaDeCreditoqq $
.qq$ %!
Parametro_transaccionqq% :
.qq: ;
Addqq; >
(qq> ?
newqq? B!
Parametro_transaccionqqC X
(qqX Y
MaestroSettingsqqY h
.qqh i
Defaultqqi p
.qqp q<
/IdDetalleMaestroParametroNumeroBolsasDePlastico	qqq �
,
qq� �
ordenDeVenta
qq� �
.
qq� �$
NumeroBolsasDePlastico
qq� �
(
qq� �
)
qq� �
.
qq� �
ToString
qq� �
(
qq� �
)
qq� �
)
qq� �
)
qq� �
;
qq� �
}rr 
}ss 	
publictt 
Transacciontt #
DevolverPagoNotaCreditott 2
(tt2 3
Transacciontt3 >
notaCreditott? J
,ttJ K
TransaccionttL W
ordenNotaCreditottX h
,tth i
DateTimettj r
fechaActualtts ~
,tt~ 
int
tt� �

idTipoNota
tt� �
,
tt� �
ModoPago
tt� �
modoPago
tt� �
,
tt� �
decimal
tt� �
importeTotal
tt� �
,
tt� �$
UserProfileSessionData
tt� �

tt� �
)
tt� �
{uu 	
Transaccionvv 
pagovv 
=vv 
nullvv #
;vv# $
ifww 
(ww 

idTipoNotaww 
!=ww 
MaestroSettingsww -
.ww- .
Defaultww. 5
.ww5 6U
IIdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcionww6 
)	ww �
{xx 
Cuotayy 
cuotayy 
=yy 
newyy !
Cuotayy" '
(yy' (0
$ObtenerSiguienteCodigoParaNuevaCuotayy( L
(yyL M
falseyyM R
,yyR S
fechaActualyyT _
.yy_ `
Yearyy` d
)yyd e
+yyf g
$stryyh k
+yyl m
$numyyn o
,yyo p
fechaActualyyq |
,yy| }
fechaActual	yy~ �
,
yy� �
importeTotal
yy� �
,
yy� �
$str
yy� �
,
yy� �
false
yy� �
)
yy� �
;
yy� �
ordenNotaCreditozz  
.zz  !
Cuotazz! &
.zz& '
Addzz' *
(zz* +
cuotazz+ 0
)zz0 1
;zz1 2
if{{ 
({{ 
modoPago{{ 
=={{ 
ModoPago{{  (
.{{( )
Contado{{) 0
){{0 1
{||  
ValidarImporteAPagar}} (
(}}( )
$num}}) *
,}}* +
cuota}}, 1
.}}1 2
total}}2 7
,}}7 8
ordenNotaCredito}}9 I
.}}I J

)}}W X
;}}X Y
pago~~ 
=~~ ,
 GenerarPagoPorNotaCreditoODebito~~ ;
(~~; <
ordenNotaCredito~~< L
,~~L M

CodigoPago~~N X
(~~X Y
cuota~~Y ^
)~~^ _
,~~_ `
cuota~~a f
.~~f g
total~~g l
,~~l m

.~~{ |
Empleado	~~| �
.
~~� �
Id
~~� �
,
~~� �
fechaActual
~~� �
,
~~� �
$str
~~� �
,
~~� �

~~� �
.
~~� �,
IdCentroDeAtencionSeleccionado
~~� �
,
~~� �
MaestroSettings
~~� �
.
~~� �
Default
~~� �
.
~~� �.
 IdDetalleMaestroEstadoConfirmado
~~� �
,
~~� �
$str
~~� �
)
~~� �
;
~~� �
cuota 
. 
SetPagoACuenta (
(( )
ordenNotaCredito) 9
.9 :

)G H
;H I$
VincularPagoConLaCuota
�� *
(
��* +
pago
��+ /
,
��/ 0
cuota
��1 6
,
��6 7
ordenNotaCredito
��8 H
.
��H I

��I V
)
��V W
;
��W X
notaCredito
�� 
.
��  
Transaccion1
��  ,
.
��, -
Add
��- 0
(
��0 1
pago
��1 5
)
��5 6
;
��6 7
}
�� 
}
�� 
return
�� 
pago
�� 
;
�� 
}
�� 	
public
�� 
Transaccion
�� 1
#DevolverSalidaMercaderiaNotaCredito
�� >
(
��> ?
Venta
��? D
venta
��E J
,
��J K
OrdenDeVenta
��L X

ordenVenta
��Y c
,
��c d
Transaccion
��e p
notaCredito
��q |
,
��| }
Transaccion��~ �$
ordenDeNotaDeCredito��� �
,��� �
DateTime��� �
fechaActual��� �
,��� �
int��� �7
'idTipoTransaccionMovimientoDeMercaderia��� �
,��� �
List��� �
<��� �#
Detalle_transaccion��� �
>��� �
detallesNota��� �
,��� �&
UserProfileSessionData��� �

)��� �
{
�� 	
Transaccion
�� &
salidaMercaderiaPorVenta
�� 0
=
��1 2
null
��3 7
;
��7 8
var
�� $
salidasMercaderiaVenta
�� &
=
��' (
venta
��) .
.
��. /(
ObtenerSalidasDeMercaderia
��/ I
(
��I J
)
��J K
;
��K L
if
�� 
(
�� $
salidasMercaderiaVenta
�� &
.
��& '
Count
��' ,
>
��- .
$num
��/ 0
&&
��1 35
'idTipoTransaccionMovimientoDeMercaderia
��4 [
!=
��\ ^
$num
��_ `
)
��` a
{
�� 
salidaMercaderiaPorVenta
�� (
=
��) *+
GenerarMovimientoDeMercaderia
��+ H
(
��H I"
ordenDeNotaDeCredito
��I ]
,
��] ^

��_ l
.
��l m
Empleado
��m u
.
��u v
Id
��v x
,
��x y

.��� �8
(IdCentroAtencionQueTieneElStockIntegrada��� �
,��� �

ordenVenta��� �
.��� �
	IdCliente��� �
,��� �7
'idTipoTransaccionMovimientoDeMercaderia��� �
,��� �
fechaActual��� �
,��� �
$str��� �
,��� �
detallesNota��� �
,��� �

,��� �

ordenVenta��� �
.��� �"
OperacionDeAlmacen��� �
(��� �
)��� �
.��� �
Id��� �
)��� �
;��� �&
salidaMercaderiaPorVenta
�� (
.
��( )
Transaccion3
��) 5
=
��6 7"
ordenDeNotaDeCredito
��8 L
;
��L M
notaCredito
�� 
.
�� 
Transaccion1
�� (
.
��( )
Add
��) ,
(
��, -&
salidaMercaderiaPorVenta
��- E
)
��E F
;
��F G
}
�� 
return
�� &
salidaMercaderiaPorVenta
�� +
;
��+ ,
}
�� 	
public
�� 
OperationResult
�� )
GuardarNotaDeCreditoDeVenta
�� :
(
��: ;
long
��; ?
idOrdenVenta
��@ L
,
��L M
int
��N Q

idTipoNota
��R \
,
��\ ]
string
��^ d
motivo
��e k
,
��k l
int
��m p 
idTipoComprobante��q �
,��� �
int��� �"
idSerieComprobante��� �
,��� �
bool��� �
esPropio��� �
,��� �
string��� �(
numeroSerieDeComprobante��� �
,��� �
int��� �#
numeroDeComprobante��� �
,��� �
string��� �
valorDeNota��� �
,��� �
int��� �"
idEventoReferencia��� �
,��� �
List��� �
<��� �"
DetalleOrdenDeNota��� �
>��� �
detalles��� �
,��� �&
UserProfileSessionData��� �

)��� �
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaEmision
�� %
=
��& '
DateTimeUtil
��( 4
.
��4 5
FechaActual
��5 @
(
��@ A
)
��A B
;
��B C
OrdenDeVenta
�� 
ordenDeVenta
�� )
=
��* +
new
��, /
OrdenDeVenta
��0 <
(
��< =$
transaccionRepositorio
��= S
.
��S TG
8ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado��T �
(��� �
idOrdenVenta��� �
)��� �
)��� �
;��� �
Venta
�� 
venta
�� 
=
�� 
new
�� !
Venta
��" '
(
��' ($
transaccionRepositorio
��( >
.
��> ?0
"ObtenerTransaccionInclusiveEstados
��? a
(
��a b
ordenDeVenta
��b n
.
��n o
IdVenta
��o v
)
��v w
)
��w x
;
��x y
List
�� 
<
�� !
Detalle_transaccion
�� (
>
��( )
detallesDeNota
��* 8
=
��9 :.
 CalcularDetalleNotaDebitoCredito
��; [
(
��[ \
detalles
��\ d
,
��d e
ordenDeVenta
��f r
.
��r s!
DetalleTransaccion��s �
(��� �
)��� �
,��� �

idTipoNota��� �
,��� �
valorDeNota��� �
,��� �
motivo��� �
,��� �
ordenDeVenta��� �
.��� �
Igv��� �
(��� �
)��� �
>��� �
$num��� �
)��� �
;��� �
var
�� 
importeTotal
��  
=
��! "
detallesDeNota
��# 1
.
��1 2
Sum
��2 5
(
��5 6
d
��6 7
=>
��8 :
d
��; <
.
��< =
total
��= B
)
��B C
+
��D E
ordenDeVenta
��F R
.
��R S
Icbper
��S Y
(
��Y Z
)
��Z [
;
��[ \'
ValidarNotaCreditoEnVenta
�� )
(
��) *
ordenDeVenta
��* 6
,
��6 7
importeTotal
��8 D
)
��D E
;
��E F
int
�� 
idUnidadNegocio
�� #
=
��$ %
MaestroSettings
��& 5
.
��5 6
Default
��6 =
.
��= >8
*IdDetalleMaestroUnidadDeNegocioTransversal
��> h
;
��h i
int
�� 5
'idTipoTransaccionMovimientoDeMercaderia
�� ;
=
��< =
Diccionario
��> I
.
��I J-
MapeoOrdenVsMovimientoDeAlmacen
��J i
.
��i j
SingleOrDefault
��j y
(
��y z
l
��z {
=>
��| ~
l�� �
.��� �
Key��� �
==��� �
Diccionario��� �
.��� �"
MapeoWraperVsOrden��� �
.��� �
Single��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Key��� �
==��� �
Diccionario��� �
.��� �=
-MapeoDetalleMaestroVsTipoTransaccionParaVenta��� �
.��� �
Single��� �
(��� �
n��� �
=>��� �
n��� �
.��� �
Key��� �
==��� �

idTipoNota��� �
)��� �
.��� �
Value��� �
)��� �
.��� �
Value��� �
)��� �
.��� �
Value��� �
;��� �
permisos_Logica
�� 
.
��  

��  -
(
��- .

��. ;
.
��; <
Empleado
��< D
.
��D E
Id
��E G
,
��G H
MaestroSettings
��I X
.
��X Y
Default
��Y `
.
��` a.
IdDetalleMaestroAccionConfirmar��a �
,��� �
Diccionario��� �
.��� �"
MapeoWraperVsOrden��� �
.��� �
Single��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Key��� �
==��� �
Diccionario��� �
.��� �=
-MapeoDetalleMaestroVsTipoTransaccionParaVenta��� �
.��� �
Single��� �
(��� �
n��� �
=>��� �
n��� �
.��� �
Key��� �
==��� �

idTipoNota��� �
)��� �
.��� �
Value��� �
)��� �
.��� �
Value��� �
,��� �
idUnidadNegocio��� �
)��� �
;��� �
Transaccion
�� 

�� )
=
��* +(
GenerarNotaDeCreditoDebito
��, F
(
��F G

��G T
.
��T U
Empleado
��U ]
.
��] ^
Id
��^ `
,
��` a
idUnidadNegocio
��b q
,
��q r
esPropio
��s {
,
��{ |!
idSerieComprobante��} �
,��� �!
idTipoComprobante��� �
,��� �#
numeroDeComprobante��� �
,��� �(
numeroSerieDeComprobante��� �
,��� �
fechaEmision��� �
,��� �
$str��� �
,��� �
Diccionario��� �
.��� �=
-MapeoDetalleMaestroVsTipoTransaccionParaVenta��� �
.��� �
Single��� �
(��� �
n��� �
=>��� �
n��� �
.��� �
Key��� �
==��� �

idTipoNota��� �
)��� �
.��� �
Value��� �
,��� �
importeTotal��� �
,��� �
motivo��� �
,��� �
ordenDeVenta��� �
.��� �
Cliente��� �
(��� �
)��� �
.��� �
Id��� �
,��� �

.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �

)��� �
;��� �
ModoPago
�� 
modoPago
�� !
=
��" #
ordenDeVenta
��$ 0
.
��0 1

ModoDePago
��1 ;
(
��; <
)
��< =
;
��= >
Transaccion
�� "
ordenDeNotaDeCredito
�� 0
=
��1 2-
GenerarOrdenNotaDeCreditoDebito
��3 R
(
��R S

��S `
,
��` a

��b o
.
��o p
Empleado
��p x
.
��x y
Id
��y {
,
��{ |
idUnidadNegocio��} �
,��� �

idTipoNota��� �
,��� �
fechaEmision��� �
,��� �
(��� �
(��� �
int��� �
)��� �
modoPago��� �
)��� �
.��� �
ToString��� �
(��� �
)��� �
,��� �
$str��� �
,��� �
Diccionario��� �
.��� �"
MapeoWraperVsOrden��� �
.��� �
Single��� �
(��� �
m��� �
=>��� �
m��� �
.��� �
Key��� �
==��� �
Diccionario��� �
.��� �=
-MapeoDetalleMaestroVsTipoTransaccionParaVenta��� �
.��� �
Single��� �
(��� �
n��� �
=>��� �
n��� �
.��� �
Key��� �
==��� �

idTipoNota��� �
)��� �
.��� �
Value��� �
)��� �
.��� �
Value��� �
,��� �
motivo��� �
,��� �
ordenDeVenta��� �
.��� �
Cliente��� �
(��� �
)��� �
.��� �
Id��� �
,��� �
ordenDeVenta��� �
.��� �
AliasCliente��� �
(��� �
)��� �
,��� �

.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �
detallesDeNota��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoConfirmado��� �
,��� �
$str��� �
,��� �
true��� �
,��� �7
'idTipoTransaccionMovimientoDeMercaderia��� �
!=��� �
$num��� �
)��� �
;��� �
if
�� 
(
��  
idEventoReferencia
�� &
!=
��' )
$num
��* +
)
��+ ,"
ordenDeNotaDeCredito
��- A
.
��A B"
id_evento_referencia
��B V
=
��W X 
idEventoReferencia
��Y k
;
��k l
if
�� 
(
�� 
ordenDeVenta
��  
.
��  !
Icbper
��! '
(
��' (
)
��( )
>
��* +
$num
��, -
)
��- .
{
�� "
ordenDeNotaDeCredito
�� (
.
��( )
	importe10
��) 2
=
��3 4
ordenDeVenta
��5 A
.
��A B
Icbper
��B H
(
��H I
)
��I J
;
��J K"
ordenDeNotaDeCredito
�� (
.
��( )#
Parametro_transaccion
��) >
.
��> ?
Add
��? B
(
��B C
new
��C F#
Parametro_transaccion
��G \
(
��\ ]
MaestroSettings
��] l
.
��l m
Default
��m t
.
��t u.
IdDetalleMaestroParametroIcbper��u �
,��� �
ordenDeVenta��� �
.��� �
Icbper��� �
(��� �
)��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �"
ordenDeNotaDeCredito
�� (
.
��( )#
Parametro_transaccion
��) >
.
��> ?
Add
��? B
(
��B C
new
��C F#
Parametro_transaccion
��G \
(
��\ ]
MaestroSettings
��] l
.
��l m
Default
��m t
.
��t u>
/IdDetalleMaestroParametroNumeroBolsasDePlastico��u �
,��� �
ordenDeVenta��� �
.��� �&
NumeroBolsasDePlastico��� �
(��� �
)��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
}
�� "
ordenDeNotaDeCredito
�� $
.
��$ %'
id_transaccion_referencia
��% >
=
��? @
ordenDeVenta
��A M
.
��M N
Id
��N P
;
��P Q

�� 
.
�� 
Transaccion1
�� *
.
��* +
Add
��+ .
(
��. /"
ordenDeNotaDeCredito
��/ C
)
��C D
;
��D E
if
�� 
(
�� 

idTipoNota
�� 
!=
�� !
MaestroSettings
��" 1
.
��1 2
Default
��2 9
.
��9 :X
IIdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion��: �
)��� �
{
�� 
Cuota
�� 
cuota
�� 
=
��  !
new
��" %
Cuota
��& +
(
��+ ,2
$ObtenerSiguienteCodigoParaNuevaCuota
��, P
(
��P Q
false
��Q V
,
��V W
fechaEmision
��X d
.
��d e
Year
��e i
)
��i j
+
��k l
$str
��m p
+
��q r
$num
��s t
,
��t u
fechaEmision��v �
,��� �
fechaEmision��� �
,��� �
importeTotal��� �
,��� �
$str��� �
,��� �
false��� �
)��� �
;��� �"
ordenDeNotaDeCredito
�� (
.
��( )
Cuota
��) .
.
��. /
Add
��/ 2
(
��2 3
cuota
��3 8
)
��8 9
;
��9 :
if
�� 
(
�� 
modoPago
��  
==
��! #
ModoPago
��$ ,
.
��, -
Contado
��- 4
)
��4 5
{
�� "
ValidarImporteAPagar
�� ,
(
��, -
$num
��- .
,
��. /
cuota
��0 5
.
��5 6
total
��6 ;
,
��; <"
ordenDeNotaDeCredito
��= Q
.
��Q R

��R _
)
��_ `
;
��` a
Transaccion
�� #
pago
��$ (
=
��) *.
 GenerarPagoPorNotaCreditoODebito
��+ K
(
��K L"
ordenDeNotaDeCredito
��L `
,
��` a

CodigoPago
��b l
(
��l m
cuota
��m r
)
��r s
,
��s t
cuota
��u z
.
��z {
total��{ �
,��� �

.��� �
Empleado��� �
.��� �
Id��� �
,��� �
fechaEmision��� �
,��� �
$str��� �
,��� �

.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoConfirmado��� �
,��� �
$str��� �
)��� �
;��� �
cuota
�� 
.
�� 
SetPagoACuenta
�� ,
(
��, -"
ordenDeNotaDeCredito
��- A
.
��A B

��B O
)
��O P
;
��P Q$
VincularPagoConLaCuota
�� .
(
��. /
pago
��/ 3
,
��3 4
cuota
��5 :
,
��: ;"
ordenDeNotaDeCredito
��< P
.
��P Q

��Q ^
)
��^ _
;
��_ `

�� %
.
��% &
Transaccion1
��& 2
.
��2 3
Add
��3 6
(
��6 7
pago
��7 ;
)
��; <
;
��< =
}
�� 
}
�� 
Transaccion
�� &
salidaMercaderiaPorVenta
�� 4
=
��5 6
null
��7 ;
;
��; <
var
�� $
salidasMercaderiaVenta
�� *
=
��+ ,
venta
��- 2
.
��2 3(
ObtenerSalidasDeMercaderia
��3 M
(
��M N
)
��N O
;
��O P
if
�� 
(
�� $
salidasMercaderiaVenta
�� *
.
��* +
Count
��+ 0
>
��1 2
$num
��3 4
&&
��5 75
'idTipoTransaccionMovimientoDeMercaderia
��8 _
!=
��` b
$num
��c d
)
��d e
{
�� &
salidaMercaderiaPorVenta
�� ,
=
��- .+
GenerarMovimientoDeMercaderia
��/ L
(
��L M"
ordenDeNotaDeCredito
��M a
,
��a b

��c p
.
��p q
Empleado
��q y
.
��y z
Id
��z |
,
��| }

.��� �8
(IdCentroAtencionQueTieneElStockIntegrada��� �
,��� �
ordenDeVenta��� �
.��� �
	IdCliente��� �
,��� �7
'idTipoTransaccionMovimientoDeMercaderia��� �
,��� �
fechaEmision��� �
,��� �
$str��� �
,��� �
detallesDeNota��� �
,��� �

,��� �
ordenDeVenta��� �
.��� �"
OperacionDeAlmacen��� �
(��� �
)��� �
.��� �
Id��� �
)��� �
;��� �&
salidaMercaderiaPorVenta
�� ,
.
��, -
Transaccion3
��- 9
=
��: ;"
ordenDeNotaDeCredito
��< P
;
��P Q

�� !
.
��! "
Transaccion1
��" .
.
��. /
Add
��/ 2
(
��2 3&
salidaMercaderiaPorVenta
��3 K
)
��K L
;
��L M
}
�� 
if
�� 
(
�� 5
'idTipoTransaccionMovimientoDeMercaderia
�� ;
!=
��< >
$num
��? @
)
��@ A
{
�� 
return
�� 6
(AfectarInventarioFisicoYGuardarOperacion
�� C
(
��C D
new
��D G 
OperacionIntegrada
��H Z
{
��[ \
	Operacion
��] f
=
��g h

��i v
,
��v w
OrdenDeOperacion��x �
=��� �$
ordenDeNotaDeCredito��� �
,��� �!
MovimientosBienes��� �
=��� �
(��� �&
salidasMercaderiaVenta��� �
.��� �
Count��� �
>��� �
$num��� �
)��� �
?��� �
new��� �
List��� �
<��� �
Transaccion��� �
>��� �
(��� �
)��� �
{��� �(
salidaMercaderiaPorVenta��� �
}��� �
:��� �
null��� �
}��� �
,��� �

)��� �
;��� �
}
�� 
else
�� 
{
�� 
var
�� 
result
�� 
=
��  $
transaccionRepositorio
��! 7
.
��7 8
CrearTransaccion
��8 H
(
��H I

��I V
)
��V W
;
��W X
result
�� 
.
�� 
information
�� &
=
��' (
new
��) ,
	Operacion
��- 6
(
��6 7"
ordenDeNotaDeCredito
��7 K
)
��K L
.
��L M
Id
��M O
;
��O P
result
�� 
.
�� 
objeto
�� !
=
��" #
new
��$ '
OrdenDeVenta
��( 4
(
��4 5"
ordenDeNotaDeCredito
��5 I
)
��I J
;
��J K
return
�� 
result
�� !
;
��! "
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* l
,
��l m
e
��n o
)
��o p
;
��p q
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
GuardarNotaVenta
�� /
(
��/ 0
long
��0 4
idOrdenVenta
��5 A
,
��A B
int
��C F

idTipoNota
��G Q
,
��Q R
int
��S V
idTipoComprobante
��W h
,
��h i
int
��j m!
idSerieComprobante��n �
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
,��� �
int��� �"
idEventoReferencia��� �
,��� �
List��� �
<��� �"
DetalleOrdenDeNota��� �
>��� �
detalles��� �
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
	DatosPago��� �
	datosPago��� �
,��� �&
UserProfileSessionData��� �

)��� �
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��' 3
.
��3 4
FechaActual
��4 ?
(
��? @
)
��@ A
;
��A B
List
�� 
<
�� 
Cuota
�� 
>
�� 
cuotasModificadas
�� -
=
��. /
new
��0 3
List
��4 8
<
��8 9
Cuota
��9 >
>
��> ?
(
��? @
)
��@ A
;
��A B
List
�� 
<
��  
Estado_transaccion
�� '
>
��' (&
estadosTransaccionNuevos
��) A
=
��B C
new
��D G
List
��H L
<
��L M 
Estado_transaccion
��M _
>
��_ `
(
��` a
)
��a b
;
��b c
List
�� 
<
�� !
Detalle_transaccion
�� (
>
��( ),
detallesTransaccionModificados
��* H
=
��I J
new
��K N
List
��O S
<
��S T!
Detalle_transaccion
��T g
>
��g h
(
��h i
)
��i j
;
��j k
var
�� 

ordenVenta
�� 
=
��  
new
��! $
OrdenDeVenta
��% 1
(
��1 2$
transaccionRepositorio
��2 H
.
��H I0
"ObtenerTransaccionInclusiveEstados
��I k
(
��k l
idOrdenVenta
��l x
)
��x y
)
��y z
;
��z {
var
�� 
venta
�� 
=
�� 
new
�� 
Venta
��  %
(
��% &$
transaccionRepositorio
��& <
.
��< =0
"ObtenerTransaccionInclusiveEstados
��= _
(
��_ `

ordenVenta
��` j
.
��j k
IdVenta
��k r
)
��r s
)
��s t
;
��t u
var
�� 

�� !
=
��" #
new
��$ '

��( 5
(
��5 6
venta
��6 ;
,
��; <

ordenVenta
��= G
,
��G H
fechaActual
��I T
,
��T U

idTipoNota
��V `
,
��` a
idTipoComprobante
��b s
,
��s t!
idSerieComprobante��u �
,��� �
esPropio��� �
,��� �
numeroSerie��� �
,��� �!
numeroComprobante��� �
,��� �
	montoNota��� �
,��� �
detalles��� �
,��� �
esDebito��� �
,��� �

esDiferida��� �
,��� �
observacion��� �
,��� �
	datosPago��� �
,��� �

)��� �
;��� �1
#RecalcularDetalleNotaDebitoCredito_
�� 3
(
��3 4

��4 A
,
��A B&
estadosTransaccionNuevos
��C [
,
��[ \
cuotasModificadas
��] n
,
��n o-
detallesTransaccionModificados��p �
)��� �
;��� �
if
�� 
(
�� 
!
�� 
esDebito
�� 
)
�� '
ValidarNotaCreditoEnVenta
�� 8
(
��8 9

��9 F
.
��F G

OrdenVenta
��G Q
,
��Q R

��S `
.
��` a
ImporteTotal
��a m
)
��m n
;
��n o
Transaccion
�� 
	operacion
�� %
=
��& ''
GenerarNotaCreditoDebito_
��( A
(
��A B

��B O
)
��O P
;
��P Q
Transaccion
�� 
ordenOperacion
�� *
=
��+ ,,
GenerarOrdenNotaCreditoDebito_
��- K
(
��K L
	operacion
��L U
,
��U V

��W d
)
��d e
;
��e f
Transaccion
�� 
pago
��  
=
��! "'
GenerarPagoCreditoDebito_
��# <
(
��< =
	operacion
��= F
,
��F G
ordenOperacion
��H V
,
��V W

��X e
)
��e f
;
��f g
Transaccion
�� 
movimientoAlmacen
�� -
=
��. /4
&GenerarMovimientoAlmacenCreditoDebito_
��0 V
(
��V W
	operacion
��W `
,
��` a
ordenOperacion
��b p
,
��p q

��r 
,�� �

)��� �
;��� �
var
�� *
movimientoEconomicoConPuntos
�� 0
=
��1 2
venta
��3 8
.
��8 9
ObtenerPagos
��9 E
(
��E F
)
��F G
.
��G H
FirstOrDefault
��H V
(
��V W
p
��W X
=>
��Y [
p
��\ ]
.
��] ^
TrazaDePago
��^ i
(
��i j
)
��j k
.
��k l
MedioDePago
��l w
(
��w x
)
��x y
.
��y z
id
��z |
==
��} 
MaestroSettings��� �
.��� �
Default��� �
.��� �1
!IdDetalleMaestroMedioDepagoPuntos��� �
)��� �
;��� �
var
�� &
transaccionesModificadas
�� ,
=
��- .-
GenerarTransaccionesAModificar_
��/ N
(
��N O*
movimientoEconomicoConPuntos
��O k
)
��k l
;
��l m
var
�� $
operacionModificatoria
�� *
=
��+ ,
new
��- 0$
OperacionModificatoria
��1 G
(
��G H
)
��H I
{
��J K
	Operacion
��L U
=
��V W
	operacion
��X a
,
��a b
OrdenDeOperacion
��c s
=
��t u
ordenOperacion��v �
,��� �#
MovimientoEconomico��� �
=��� �
pago��� �
,��� �!
MovimientosBienes��� �
=��� �!
movimientoAlmacen��� �
!=��� �
null��� �
?��� �
new��� �
List��� �
<��� �
Transaccion��� �
>��� �
(��� �
)��� �
{��� �!
movimientoAlmacen��� �
}��� �
:��� �
null��� �
,��� �5
%NuevosEstadosTransaccionesModificadas��� �
=��� �(
estadosTransaccionNuevos��� �
,��� �!
CuotasModificadas��� �
=��� �!
cuotasModificadas��� �
,��� �(
TransaccionesModificadas��� �
=��� �(
transaccionesModificadas��� �
,��� �.
DetallesTransaccionModificados��� �
=��� �.
detallesTransaccionModificados��� �
}��� �
;��� �
var
�� 
	resultado
�� 
=
�� 6
(AfectarInventarioFisicoYGuardarOperacion
��  H
(
��H I$
operacionModificatoria
��I _
,
��_ `

��a n
)
��n o
;
��o p
	resultado
�� 
.
�� 
objeto
��  
=
��! "
new
��# &
OrdenDeVenta
��' 3
(
��3 4$
operacionModificatoria
��4 J
.
��J K
OrdenDeOperacion
��K [
)
��[ \
;
��\ ]
return
�� 
	resultado
��  
;
��  !
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* l
,
��l m
e
��n o
)
��o p
;
��p q
}
�� 
}
�� 	
private
�� 
Transaccion
�� '
GenerarNotaCreditoDebito_
�� 5
(
��5 6

��6 C

��D Q
)
��Q R
{
�� 	
	Operacion
�� 
operacionGenerica
�� '
=
��( )
new
��* -
	Operacion
��. 7
(
��7 8$
transaccionRepositorio
��8 N
.
��N O&
ObtenerUltimaTransaccion
��O g
(
��g h!
TransaccionSettings
��h {
.
��{ |
Default��| �
.��� �*
IdTipoTransaccionOperacion��� �
)��� �
)��� �
;��� �
string
�� 
codigo
�� 
=
�� %
codigosOperacion_Logica
�� 3
.
��3 43
%ObtenerSiguienteCodigoParaFacturacion
��4 Y
(
��Y Z

��Z g
.
��g h
SufijoCodigo
��h t
,
��t u

.��� �!
IdTipoTransaccion��� �
)��� �
;��� �
Comprobante
�� 
comprobante
�� #
=
��$ % 
GenerarComprobante
��& 8
(
��8 9

��9 F
.
��F G
EsPropio
��G O
,
��O P

��Q ^
.
��^ _ 
IdSerieComprobante
��_ q
,
��q r

.��� �!
IdTipoComprobante��� �
,��� �

.��� �
NumeroSerie��� �
,��� �

.��� �!
NumeroComprobante��� �
)��� �
;��� �
Transaccion
�� 
nota
�� 
=
�� 
new
�� "
Transaccion
��# .
(
��. /
codigo
��/ 5
,
��5 6
operacionGenerica
��7 H
.
��H I
Id
��I K
,
��K L

��M Z
.
��Z [
FechaActual
��[ f
,
��f g

��h u
.
��u v 
IdTipoTransaccion��v �
,��� �

.��� �!
IdUnidadDeNegocio��� �
,��� �
true��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �$
ObservacionOperacion��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �

IdEmpleado��� �
,��� �

.��� �
ImporteTotal��� �
,��� �

.��� � 
IdCentroAtencion��� �
,��� �

.��� �
IdMoneda��� �
,��� �

.��� �
TipoDeCambio��� �
,��� �
null��� �
,��� �

.��� �
	IdCliente��� �
)��� �
{
�� 
Comprobante
�� 
=
�� 
comprobante
�� )
}
�� 
;
��
return
�� 
nota
�� 
;
�� 
}
�� 	
private
�� 
Transaccion
�� ,
GenerarOrdenNotaCreditoDebito_
�� :
(
��: ;
Transaccion
��; F
	operacion
��G P
,
��P Q

��R _

��` m
)
��m n
{
�� 	
Transaccion
�� 
ordenDeNota
�� #
=
��$ %
new
��& )
Transaccion
��* 5
(
��5 6%
codigosOperacion_Logica
��6 M
.
��M N3
%ObtenerSiguienteCodigoParaFacturacion
��N s
(
��s t
	operacion
��t }
.
��} ~
codigo��~ �
+��� �
$str��� �
+��� �

.��� �
SufijoCodigo��� �
,��� �

.��� �&
IdTipoTransaccionOrden��� �
)��� �
,��� �
null��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �&
IdTipoTransaccionOrden��� �
,��� �

.��� �!
IdUnidadDeNegocio��� �
,��� �
true��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �$
ObservacionOperacion��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �

IdEmpleado��� �
,��� �
	operacion��� �
.��� �

,��� �

.��� � 
IdCentroAtencion��� �
,��� �
	operacion��� �
.��� �
	id_moneda��� �
,��� �
	operacion��� �
.��� �
tipo_cambio��� �
,��� �
null��� �
,��� �

.��� �
	IdCliente��� �
,��� �

.��� �
DescuentoGlobal��� �
,��� �

.��� � 
DescuentoPorItem��� �
,��� �

.��� �
Anticipo��� �
,��� �

.��� �
Gravada��� �
,��� �

.��� �
	Exonerada��� �
,��� �

.��� �
Inafecta��� �
,��� �

.��� �
Gratuita��� �
,��� �

.��� �
Igv��� �
,��� �

.��� �
Isc��� �
,��� �

.��� �
Icbper��� �
,��� �

.��� �
OtrosCargos��� �
,��� �

.��� �

)��� �
{
�� 
Comprobante
�� 
=
�� 
	operacion
�� '
.
��' (
Comprobante
��( 3
,
��3 4'
id_transaccion_referencia
�� )
=
��* +

��, 9
.
��9 :

OrdenVenta
��: D
.
��D E
Id
��E G
,
��G H'
id_actor_negocio_externo1
�� )
=
��* +

��, 9
.
��9 :

OrdenVenta
��: D
.
��D E
IdGrupoCliente
��E S
,
��S T
}
�� 
;
��
ordenDeNota
�� 
.
�� 
AgregarDetalles
�� '
(
��' (!
Detalle_transaccion
��( ;
.
��; <
Clone
��< A
(
��A B

��B O
.
��O P
DetallesOperacion
��P a
)
��a b
)
��b c
;
��c d
if
�� 
(
�� 

�� 
.
�� 
Icbper
�� $
>
��% &
$num
��' (
)
��( )
{
�� 
ordenDeNota
�� 
.
�� #
Parametro_transaccion
�� 1
.
��1 2
Add
��2 5
(
��5 6
new
��6 9#
Parametro_transaccion
��: O
(
��O P
MaestroSettings
��P _
.
��_ `
Default
��` g
.
��g h.
IdDetalleMaestroParametroIcbper��h �
,��� �

.��� �
Icbper��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 

�� 
.
�� "
NumeroBolsasPlastico
�� 2
>
��3 4
$num
��5 6
)
��6 7
{
�� 
ordenDeNota
�� 
.
�� #
Parametro_transaccion
�� 1
.
��1 2
Add
��2 5
(
��5 6
new
��6 9#
Parametro_transaccion
��: O
(
��O P
MaestroSettings
��P _
.
��_ `
Default
��` g
.
��g h>
/IdDetalleMaestroParametroNumeroBolsasDePlastico��h �
,��� �

.��� �$
NumeroBolsasPlastico��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 

�� %
(
��% &

��& 3
.
��3 4
AliasCliente
��4 @
)
��@ A
)
��A B
{
�� 
ordenDeNota
�� 
.
�� #
Parametro_transaccion
�� 1
.
��1 2
Add
��2 5
(
��5 6
new
��6 9#
Parametro_transaccion
��: O
(
��O P
MaestroSettings
��P _
.
��_ `
Default
��` g
.
��g h4
%IdDetalleMaestroParametroAliasCliente��h �
,��� �

.��� �
AliasCliente��� �
)��� �
)��� �
;��� �
}
�� 
ordenDeNota
�� 
.
�� #
Parametro_transaccion
�� -
.
��- .
Add
��. 1
(
��1 2
new
��2 5#
Parametro_transaccion
��6 K
(
��K L
MaestroSettings
��L [
.
��[ \
Default
��\ c
.
��c d>
/IdDetalleMaestroParametroCodigoTransaccionSunat��d �
,��� �"
maestroRepositorio��� �
.��� �
ObtenerDetalle��� �
(��� �

.��� �

IdTipoNota��� �
)��� �
.��� �
codigo��� �
)��� �
)��� �
;��� �
ordenDeNota
�� 
.
�� #
Parametro_transaccion
�� -
.
��- .
Add
��. 1
(
��1 2
new
��2 5#
Parametro_transaccion
��6 K
(
��K L
MaestroSettings
��L [
.
��[ \
Default
��\ c
.
��c d2
#IdDetalleMaestroParametroModoDePago��d �
,��� �
(��� �
(��� �
int��� �
)��� �

.��� �

.��� �

ModoDePago��� �
)��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
;��� �
ordenDeNota
�� 
.
��  
Estado_transaccion
�� *
.
��* +
Add
��+ .
(
��. /
new
��/ 2 
Estado_transaccion
��3 E
(
��E F

��F S
.
��S T

IdEmpleado
��T ^
,
��^ _
MaestroSettings
��` o
.
��o p
Default
��p w
.
��w x/
 IdDetalleMaestroEstadoConfirmado��x �
,��� �

.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
ordenDeNota
�� 
.
�� 
enum1
�� 
=
�� 

��  -
.
��- .)
IndicadorImpactoAlmacenNota
��. I
;
��I J
if
�� 
(
�� 
ordenDeNota
�� 
.
�� 
enum1
�� !
==
��" $
(
��% &
int
��& )
)
��) *%
IndicadorImpactoAlmacen
��* A
.
��A B
	Inmediata
��B K
||
��L N
ordenDeNota
��O Z
.
��Z [
enum1
��[ `
==
��a c
(
��d e
int
��e h
)
��h i&
IndicadorImpactoAlmacen��i �
.��� �
Diferida��� �
)��� �
{
�� 
ordenDeNota
�� 
.
�� '
id_actor_negocio_interno1
�� 5
=
��6 7

��8 E
.
��E F
	IdAlmacen
��F O
;
��O P 
Estado_transaccion
�� " 
estadoOrdenAlmacen
��# 5
=
��6 7
new
��8 ; 
Estado_transaccion
��< N
(
��N O

��O \
.
��\ ]

IdEmpleado
��] g
,
��g h

��i v
.
��v w"
EsDiferidaOperacion��w �
?��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �/
IdDetalleMaestroEstadoPendiente��� �
:��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoCompletada��� �
,��� �

.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
;��� �
ordenDeNota
�� 
.
��  
Estado_transaccion
�� .
.
��. /
Add
��/ 2
(
��2 3 
estadoOrdenAlmacen
��3 E
)
��E F
;
��F G
}
�� 
	operacion
�� 
.
�� 
Transaccion1
�� "
.
��" #
Add
��# &
(
��& '
ordenDeNota
��' 2
)
��2 3
;
��3 4
return
�� 
ordenDeNota
�� 
;
�� 
}
�� 	
private
�� 
Transaccion
�� '
GenerarPagoCreditoDebito_
�� 5
(
��5 6
Transaccion
��6 A
	operacion
��B K
,
��K L
Transaccion
��M X
ordenOperacion
��Y g
,
��g h

��i v

)��� �
{
�� 	
Transaccion
�� 
pago
�� 
=
�� 
null
�� #
;
��# $
if
�� 
(
�� 

�� 
.
�� 
ImportePagoTotal
�� .
>
��/ 0
$num
��1 2
)
��2 3
{
�� 
bool
�� 
	porCobrar
�� 
=
��  

��! .
.
��. /
EsDebito
��/ 7
;
��7 8
if
�� 
(
�� 

�� !
.
��! "

��" /
.
��/ 0

ModoDePago
��0 :
==
��; =
ModoPago
��> F
.
��F G 
CreditoConfigurado
��G Y
)
��Y Z
{
�� 
int
�� 
cont
�� 
=
�� 
$num
��  
;
��  !
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (

��) 6
.
��6 7

��7 D
.
��D E

��E R
(
��R S
)
��S T
)
��T U
{
�� 
var
�� 
cuota
�� !
=
��" #
new
��$ '
Cuota
��( -
(
��- .2
$ObtenerSiguienteCodigoParaNuevaCuota
��. R
(
��R S
	porCobrar
��S \
,
��\ ]

��^ k
.
��k l
FechaActual
��l w
.
��w x
Year
��x |
)
��| }
+
��~ 
$str��� �
+��� �
cont��� �
++��� �
,��� �

.��� �
FechaActual��� �
,��� �
item��� �
.��� �!
fecha_vencimiento��� �
,��� �
item��� �
.��� �
capital��� �
,��� �
item��� �
.��� �
interes��� �
,��� �
item��� �
.��� �
total��� �
,��� �
$str��� �
+��� �
cont��� �
,��� �
	porCobrar��� �
,��� �
item��� �
.��� �

)��� �
;��� �
ordenOperacion
�� &
.
��& '
Cuota
��' ,
.
��, -
Add
��- 0
(
��0 1
cuota
��1 6
)
��6 7
;
��7 8
}
�� 
var
�� 

diferencia
�� "
=
��# $

��% 2
.
��2 3
ImportePagoTotal
��3 C
-
��D E
ordenOperacion
��F T
.
��T U
Cuota
��U Z
.
��Z [
Sum
��[ ^
(
��^ _
c
��_ `
=>
��a c
c
��d e
.
��e f
total
��f k
)
��k l
;
��l m
ordenOperacion
�� "
.
��" #
Cuota
��# (
.
��( )
Last
��) -
(
��- .
)
��. /
.
��/ 0
total
��0 5
=
��6 7
ordenOperacion
��8 F
.
��F G
Cuota
��G L
.
��L M
Last
��M Q
(
��Q R
)
��R S
.
��S T
capital
��T [
=
��\ ]
ordenOperacion
��^ l
.
��l m
Cuota
��m r
.
��r s
Last
��s w
(
��w x
)
��x y
.
��y z
total
��z 
+��� �

diferencia��� �
;��� �
}
�� 
else
�� 
{
�� 
var
�� 
cuota
�� 
=
�� 
new
��  #
Cuota
��$ )
(
��) *2
$ObtenerSiguienteCodigoParaNuevaCuota
��* N
(
��N O
	porCobrar
��O X
,
��X Y

��Z g
.
��g h
FechaActual
��h s
.
��s t
Year
��t x
)
��x y
+
��z {
$str
��| 
+��� �
$num��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� � 
ImportePagoTotal��� �
,��� �
$str��� �
,��� �
	porCobrar��� �
)��� �
;��� �
ordenOperacion
�� "
.
��" #
Cuota
��# (
.
��( )
Add
��) ,
(
��, -
cuota
��- 2
)
��2 3
;
��3 4
}
�� 
if
�� 
(
�� 

�� !
.
��! "

��" /
.
��/ 0
HayIngresoDinero
��0 @
)
��@ A
{
�� 
var
�� +
esCreditoRapidoConPagoInicial
�� 5
=
��6 7
(
��8 9

��9 F
.
��F G

��G T
.
��T U

ModoDePago
��U _
==
��` b
ModoPago
��c k
.
��k l

��l y
&&
��z |

.��� �

.��� �
Inicial��� �
>��� �
$num��� �
)��� �
;��� �
Cuota
�� 
cuotaACobrar
�� &
=
��' (
(
��) *

��* 7
.
��7 8

��8 E
.
��E F

ModoDePago
��F P
==
��Q S
ModoPago
��T \
.
��\ ]
Contado
��] d
||
��e g,
esCreditoRapidoConPagoInicial��h �
)��� �
?��� �
ordenOperacion��� �
.��� �
Cuota��� �
.��� �
First��� �
(��� �
)��� �
:��� �
ordenOperacion��� �
.��� �
Cuota��� �
.��� �
Single��� �
(��� �
c��� �
=>��� �
c��� �
.��� �

)��� �
;��� �
cuotaACobrar
��  
.
��  !
SetPagoACuenta
��! /
(
��/ 0+
esCreditoRapidoConPagoInicial
��0 M
?
��N O

��P ]
.
��] ^

��^ k
.
��k l
Inicial
��l s
:
��t u
cuotaACobrar��v �
.��� �
total��� �
)��� �
;��� �"
ValidarImporteAPagar
�� (
(
��( )
$num
��) *
,
��* +
cuotaACobrar
��, 8
.
��8 9
total
��9 >
,
��> ?
ordenOperacion
��@ N
.
��N O

��O \
)
��\ ]
;
��] ^
pago
�� 
=
�� 8
*GenerarMovimientoEconomicoPagoACuentaCuota
�� E
(
��E F
	operacion
��F O
,
��O P
cuotaACobrar
��Q ]
,
��] ^

��_ l
.
��l m

IdEmpleado
��m w
,
��w x

.��� �
IdCaja��� �
,��� �

.��� �
	IdCliente��� �
,��� �

.��� �%
IdTipoTransaccionPago��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �$
ObservacionOperacion��� �
,��� �

.��� �

.��� �
Traza��� �
.��� �
MedioDePago��� �
.��� �
Id��� �
,��� �

.��� �

.��� �
Traza��� �
.��� �
Info��� �
.��� �!
EntidadFinanciera��� �
.��� �
Id��� �
,��� �

.��� �

.��� �
Traza��� �
.��� �
Info��� �
.��� �
Observacion��� �
)��� �
;��� �
if
�� 
(
�� 

�� %
.
��% &

��& 3
.
��3 4
Traza
��4 9
.
��9 :
MedioDePago
��: E
.
��E F
Id
��F H
==
��I K
MaestroSettings
��L [
.
��[ \
Default
��\ c
.
��c d0
!IdDetalleMaestroMedioDepagoPuntos��d �
)��� �
{
�� 
pago
�� 
.
�� 
	cantidad1
�� &
=
��' (

��) 6
.
��6 7

��7 D
.
��D E
Traza
��E J
.
��J K
Info
��K O
.
��O P
PuntosCajeados
��P ^
;
��^ _
}
�� 
if
�� 
(
�� 

�� %
.
��% &

��& 3
.
��3 4
Traza
��4 9
.
��9 :
MedioDePago
��: E
.
��E F
Id
��F H
==
��I K
MaestroSettings
��L [
.
��[ \
Default
��\ c
.
��c d:
+IdDetalleMaestroMedioDepagoDepositoEnCuenta��d �
||��� �

.��� �

.��� �
Traza��� �
.��� �
MedioDePago��� �
.��� �
Id��� �
==��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �@
0IdDetalleMaestroMedioDepagoTransferenciaDeFondos��� �
)��� �
{
�� 
pago
�� 
.
�� '
id_actor_negocio_interno1
�� 6
=
��7 8

��9 F
.
��F G

��G T
.
��T U
Traza
��U Z
.
��Z [
Info
��[ _
.
��_ `
CuentaBancaria
��` n
.
��n o
Id
��o q
;
��q r
}
�� 
if
�� 
(
�� 
!
�� 
string
�� 
.
��  

��  -
(
��- .

��. ;
.
��; <

��< I
.
��I J
Traza
��J O
.
��O P
Info
��P T
.
��T U
InformacionJson
��U d
)
��d e
)
��e f
pago
��g k
.
��k l

Traza_pago
��l v
.
��v w
First
��w |
(
��| }
)
��} ~
.
��~ 
extension_json�� �
=��� �

.��� �

.��� �
Traza��� �
.��� �
Info��� �
.��� �
InformacionJson��� �
;��� �
	operacion
�� 
.
�� 
Transaccion1
�� *
.
��* +
Add
��+ .
(
��. /
pago
��/ 3
)
��3 4
;
��4 5
}
�� 
}
�� 
return
�� 
pago
�� 
;
�� 
}
�� 	
public
�� 
Transaccion
�� 4
&GenerarMovimientoAlmacenCreditoDebito_
�� A
(
��A B
Transaccion
��B M
	operacion
��N W
,
��W X
Transaccion
��Y d
ordenOperacion
��e s
,
��s t


,��� �&
UserProfileSessionData��� �

)��� �
{
�� 	
Transaccion
�� 3
%entradaMercaderiaPorInvalidacionVenta
�� =
=
��> ?
null
��@ D
;
��D E
if
�� 
(
�� 

�� 
.
�� &
HayMovimientoAlmacenNota
�� 6
)
��6 7
{
�� 
var
�� $
salidasMercaderiaVenta
�� *
=
��+ ,

��- :
.
��: ;
Venta
��; @
.
��@ A(
ObtenerSalidasDeMercaderia
��A [
(
��[ \
)
��\ ]
;
��] ^
if
�� 
(
�� $
salidasMercaderiaVenta
�� *
.
��* +
Count
��+ 0
>
��1 2
$num
��3 4
)
��4 5
{
�� 3
%entradaMercaderiaPorInvalidacionVenta
�� 9
=
��: ;+
GenerarMovimientoDeMercaderia
��< Y
(
��Y Z
	operacion
��Z c
,
��c d

��e r
.
��r s

IdEmpleado
��s }
,
��} ~

.��� �
	IdAlmacen��� �
,��� �

.��� �
	IdCliente��� �
,��� �

.��� �(
IdTipoTransaccionAlmacen��� �
,��� �

.��� �
FechaActual��� �
,��� �

.��� �$
ObservacionOperacion��� �
,��� �

.��� �2
"DetallesMovimientoAlmacenOperacion��� �
,��� �

,��� �&
salidasMercaderiaVenta��� �
.��� �
First��� �
(��� �
)��� �
.��� �
Id��� �
)��� �
;��� �3
%entradaMercaderiaPorInvalidacionVenta
�� 9
.
��9 :
Transaccion3
��: F
=
��G H
ordenOperacion
��I W
;
��W X
	operacion
�� 
.
�� 
Transaccion1
�� *
.
��* +
Add
��+ .
(
��. /3
%entradaMercaderiaPorInvalidacionVenta
��/ T
)
��T U
;
��U V
}
�� 
if
�� 
(
�� 

�� !
.
��! "3
%NuevoComprobanteParaMovimientoAlmacen
��" G
)
��G H
{
�� 
Serie_comprobante
�� %
serie
��& +
=
��, -$
transaccionRepositorio
��. D
.
��D EN
?ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante��E �
(��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �=
-IdDetalleMaestroComprobanteNotaAlmacenInterna��� �
,��� �

.��� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �3
%entradaMercaderiaPorInvalidacionVenta
�� 9
.
��9 :
Comprobante
��: E
=
��F G3
%GenerarComprobantePropioAutonumerable
��H m
(
��m n
serie
��n s
.
��s t
id
��t v
)
��v w
;
��w x
}
�� 
}
�� 
return
�� 3
%entradaMercaderiaPorInvalidacionVenta
�� 8
;
��8 9
}
�� 	
public
�� 
void
�� 1
#RecalcularDetalleNotaDebitoCredito_
�� 7
(
��7 8

��8 E

��F S
,
��S T
List
��U Y
<
��Y Z 
Estado_transaccion
��Z l
>
��l m'
estadosTransaccionNuevos��n �
,��� �
List��� �
<��� �
Cuota��� �
>��� �!
cuotasModificadas��� �
,��� �
List��� �
<��� �#
Detalle_transaccion��� �
>��� �.
detallesTransaccionModificados��� �
)��� �
{
�� 	
if
�� 
(
�� 

�� 
.
�� 

IdTipoNota
�� (
==
��) +
MaestroSettings
��, ;
.
��; <
Default
��< C
.
��C DM
>IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion��D �
||��� �

.��� �

IdTipoNota��� �
==��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �G
7IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal��� �
)��� �
{
�� 

�� 
.
�� &
HayMovimientoAlmacenNota
�� 6
=
��7 8

��9 F
.
��F G+
HayMovimientoAlmacenOperacion
��G d
;
��d e

�� 
.
�� )
IndicadorImpactoAlmacenNota
�� 9
=
��: ;

��< I
.
��I J$
EsOrdenOrigenPendiente
��J `
?
��a b
(
��c d
int
��d g
)
��g h%
IndicadorImpactoAlmacen
��h 
.�� �6
&NoImpactaPorQueRevocaAOperacionInicial��� �
:��� �
(��� �

.��� �1
!HaySeccionEntregaAlmacenOperacion��� �
?��� �
(��� �

.��� �#
EsDiferidaOperacion��� �
?��� �
(��� �
int��� �
)��� �'
IndicadorImpactoAlmacen��� �
.��� �
Diferida��� �
:��� �
(��� �
int��� �
)��� �'
IndicadorImpactoAlmacen��� �
.��� �
	Inmediata��� �
)��� �
:��� �
(��� �
int��� �
)��� �

.��� �

OrdenVenta��� �
.��� �'
IndicadorImpactoAlmacen��� �
)��� �
;��� �
if
�� 
(
�� 

�� !
.
��! "%
EsOrdenOrigenCompletada
��" 9
)
��9 :
{
�� 

�� !
.
��! "0
"DetallesMovimientoAlmacenOperacion
��" D
=
��E F

��G T
.
��T U%
DetallesBienesOperacion
��U l
;
��l m
}
�� 
else
�� 
if
�� 
(
�� 

�� &
.
��& '"
EsOrdenOrigenParcial
��' ;
)
��; <
{
�� 
var
�� %
detallesOperacionOrigen
�� /
=
��0 1

��2 ?
.
��? @
DetallesOperacion
��@ Q
;
��Q R
var
�� 
ordenAlmacen
�� $
=
��% &!
ObtenerOrdenAlmacen
��' :
(
��: ;

��; H
.
��H I

OrdenVenta
��I S
.
��S T
Id
��T V
)
��V W
;
��W X

�� !
.
��! "
DetallesOperacion
��" 3
.
��3 4
ForEach
��4 ;
(
��; <
di
��< >
=>
��? A
di
��B D
.
��D E

cantidad_1
��E O
=
��P Q
ordenAlmacen
��R ^
.
��^ _
Detalles
��_ g
.
��g h
Where
��h m
(
��m n
d
��n o
=>
��p r
d
��s t
.
��t u

IdConcepto
��u 
==��� �
di��� �
.��� �#
id_concepto_negocio��� �
)��� �
.��� �
Sum��� �
(��� �
d��� �
=>��� �
d��� �
.��� �
	Pendiente��� �
)��� �
)��� �
;��� �%
detallesOperacionOrigen
�� +
.
��+ ,
ForEach
��, 3
(
��3 4
di
��4 6
=>
��7 9
di
��: <
.
��< =

cantidad_1
��= G
=
��H I
ordenAlmacen
��J V
.
��V W
Detalles
��W _
.
��_ `
Where
��` e
(
��e f
d
��f g
=>
��h j
d
��k l
.
��l m

IdConcepto
��m w
==
��x z
di
��{ }
.
��} ~"
id_concepto_negocio��~ �
)��� �
.��� �
Sum��� �
(��� �
d��� �
=>��� �
d��� �
.��� �
	Pendiente��� �
)��� �
)��� �
;��� �&
estadosTransaccionNuevos
�� ,
.
��, -
Add
��- 0
(
��0 1
new
��1 4 
Estado_transaccion
��5 G
(
��G H

��H U
.
��U V

OrdenVenta
��V `
.
��` a
Id
��a c
,
��c d

��e r
.
��r s

IdEmpleado
��s }
,
��} ~
MaestroSettings�� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoCompletada��� �
,��� �

.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  $
detalleOperacionOrigen
��! 7
in
��8 :%
detallesOperacionOrigen
��; R
)
��R S
{
�� 
var
�� 
detalle
�� #
=
��$ %$
transaccionRepositorio
��& <
.
��< ='
ObtenerDetalleTransaccion
��= V
(
��V W$
detalleOperacionOrigen
��W m
.
��m n
id
��n p
)
��p q
;
��q r
detalle
�� 
.
��  

cantidad_1
��  *
=
��+ ,$
detalleOperacionOrigen
��- C
.
��C D

cantidad_1
��D N
;
��N O,
detallesTransaccionModificados
�� 6
.
��6 7
Add
��7 :
(
��: ;
detalle
��; B
)
��B C
;
��C D
}
�� 

�� !
.
��! "0
"DetallesMovimientoAlmacenOperacion
��" D
=
��E F
new
��G J
List
��K O
<
��O P!
Detalle_transaccion
��P c
>
��c d
(
��d e
)
��e f
;
��f g
var
�� '
detallesMovimientoAlmacen
�� 1
=
��2 3
ordenAlmacen
��4 @
.
��@ A
Detalles
��A I
.
��I J
Where
��J O
(
��O P
d
��P Q
=>
��R T
d
��U V
.
��V W
	Entregado
��W `
>
��a b
$num
��c d
)
��d e
.
��e f
ToList
��f l
(
��l m
)
��m n
;
��n o
foreach
�� 
(
�� 
var
��  
detalle
��! (
in
��) +'
detallesMovimientoAlmacen
��, E
)
��E F
{
�� 
var
�� 
detalleAlmacen
�� *
=
��+ ,

��- :
.
��: ;%
DetallesBienesOperacion
��; R
.
��R S
First
��S X
(
��X Y
d
��Y Z
=>
��[ ]
d
��^ _
.
��_ `!
id_concepto_negocio
��` s
==
��t v
detalle
��w ~
.
��~ 

IdConcepto�� �
)��� �
;��� �
detalleAlmacen
�� &
.
��& '
cantidad
��' /
=
��0 1
detalle
��2 9
.
��9 :
	Entregado
��: C
;
��C D

�� %
.
��% &0
"DetallesMovimientoAlmacenOperacion
��& H
.
��H I
Add
��I L
(
��L M
detalleAlmacen
��M [
)
��[ \
;
��\ ]
}
�� 
}
�� 
else
�� 
if
�� 
(
�� 

�� &
.
��& '$
EsOrdenOrigenPendiente
��' =
)
��= >
{
�� 
var
�� %
detallesOperacionOrigen
�� /
=
��0 1

��2 ?
.
��? @
DetallesOperacion
��@ Q
;
��Q R%
detallesOperacionOrigen
�� +
.
��+ ,
ForEach
��, 3
(
��3 4
di
��4 6
=>
��7 9
di
��: <
.
��< =

cantidad_1
��= G
=
��H I
di
��J L
.
��L M
cantidad
��M U
)
��U V
;
��V W&
estadosTransaccionNuevos
�� ,
.
��, -
Add
��- 0
(
��0 1
new
��1 4 
Estado_transaccion
��5 G
(
��G H

��H U
.
��U V

OrdenVenta
��V `
.
��` a
Id
��a c
,
��c d

��e r
.
��r s

IdEmpleado
��s }
,
��} ~
MaestroSettings�� �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoCompletada��� �
,��� �

.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  $
detalleOperacionOrigen
��! 7
in
��8 :%
detallesOperacionOrigen
��; R
)
��R S
{
�� 
var
�� 
detalle
�� #
=
��$ %$
transaccionRepositorio
��& <
.
��< ='
ObtenerDetalleTransaccion
��= V
(
��V W$
detalleOperacionOrigen
��W m
.
��m n
id
��n p
)
��p q
;
��q r
detalle
�� 
.
��  

cantidad_1
��  *
=
��+ ,$
detalleOperacionOrigen
��- C
.
��C D

cantidad_1
��D N
;
��N O,
detallesTransaccionModificados
�� 6
.
��6 7
Add
��7 :
(
��: ;
detalle
��; B
)
��B C
;
��C D
}
�� 
}
�� 
RecalcularCuotas_
�� !
(
��! "

��" /
,
��/ 0
cuotasModificadas
��1 B
)
��B C
;
��C D
}
�� 
if
�� 
(
�� 

�� 
.
�� 

IdTipoNota
�� (
==
��) +
MaestroSettings
��, ;
.
��; <
Default
��< C
.
��C DG
9IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem
��D }
)
��} ~
{
�� 
var
�� 
HayImpactoAlmacen
�� %
=
��& '
false
��( -
;
��- .
var
��  
idsConceptosIcbper
�� &
=
��' (

��) 6
.
��6 7

OrdenVenta
��7 A
.
��A B
Detalles
��B J
(
��J K
)
��K L
.
��L M
Where
��M R
(
��R S
d
��S T
=>
��U W
d
��X Y
.
��Y Z
Producto
��Z b
.
��b c
IdConceptoBasico
��c s
==
��t v
MaestroSettings��w �
.��� �
Default��� �
.��� �;
+IdDetalleMaestroConceptoBasicoBolsaPlastica��� �
)��� �
.��� �
Select��� �
(��� �
d��� �
=>��� �
d��� �
.��� �
Producto��� �
.��� �
Id��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
�� 
detalleNota
�� (
in
��) +

��, 9
.
��9 :
DetallesNota
��: F
)
��F G
{
�� 
detalleNota
�� 
.
��  
ImporteRevocado
��  /
=
��0 1
detalleNota
��2 =
.
��= >

��> K
*
��L M
detalleNota
��N Y
.
��Y Z
PrecioUnitario
��Z h
;
��h i
detalleNota
�� 
.
��  
IcbperRevocado
��  .
=
��/ 0
detalleNota
��1 <
.
��< =

��= J
*
��K L
(
��M N 
idsConceptosIcbper
��N `
.
��` a
Contains
��a i
(
��i j
detalleNota
��j u
.
��u v
Producto
��v ~
.
��~ 
Id�� �
)��� �
?��� �

.��� �

OrdenVenta��� �
.��� �
ValorIcbper��� �
(��� �
)��� �
:��� �
$num��� �
)��� �
;��� �
detalleNota
�� 
.
��  
IgvRevocado
��  +
=
��, -
detalleNota
��. 9
.
��9 :
ImporteRevocado
��: I
-
��J K
(
��L M
detalleNota
��M X
.
��X Y
ImporteRevocado
��Y h
/
��i j
(
��k l
$num
��l m
+
��n o
(
��p q

��q ~
.
��~ 
GravaIgv�� �
?��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �
TasaIGV��� �
:��� �
$num��� �
)��� �
)��� �
)��� �
;��� �
detalleNota
�� 
.
��  
ImporteDevuelto
��  /
=
��0 1
detalleNota
��2 =
.
��= >

��> K
*
��L M
detalleNota
��N Y
.
��Y Z
PrecioUnitario
��Z h
;
��h i
detalleNota
�� 
.
��  
IcbperDevuelto
��  .
=
��/ 0
detalleNota
��1 <
.
��< =

��= J
*
��K L
(
��M N 
idsConceptosIcbper
��N `
.
��` a
Contains
��a i
(
��i j
detalleNota
��j u
.
��u v
Producto
��v ~
.
��~ 
Id�� �
)��� �
?��� �

.��� �

OrdenVenta��� �
.��� �
ValorIcbper��� �
(��� �
)��� �
:��� �
$num��� �
)��� �
;��� �
detalleNota
�� 
.
��  
IgvDevuelto
��  +
=
��, -
detalleNota
��. 9
.
��9 :
ImporteDevuelto
��: I
-
��J K
(
��L M
detalleNota
��M X
.
��X Y
ImporteDevuelto
��Y h
/
��i j
(
��k l
$num
��l m
+
��n o
(
��p q

��q ~
.
��~ 
GravaIgv�� �
?��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �
TasaIGV��� �
:��� �
$num��� �
)��� �
)��� �
)��� �
;��� �
}
�� 

�� 
.
�� 
Icbper
�� $
=
��% &

��' 4
.
��4 5
DetallesNota
��5 A
.
��A B
Sum
��B E
(
��E F
d
��F G
=>
��H J
d
��K L
.
��L M
IcbperRevocado
��M [
+
��\ ]
d
��^ _
.
��_ `
IcbperDevuelto
��` n
)
��n o
;
��o p

�� 
.
�� "
NumeroBolsasPlastico
�� 2
=
��3 4

��5 B
.
��B C
Icbper
��C I
==
��J L
$num
��M N
?
��O P
$num
��Q R
:
��S T
(
��U V

��V c
.
��c d
Icbper
��d j
/
��k l

��m z
.
��z {

OrdenVenta��{ �
.��� �
ValorIcbper��� �
(��� �
)��� �
)��� �
;��� �

�� 
.
�� 
ImporteTotal
�� *
=
��+ ,

��- :
.
��: ;
DetallesNota
��; G
.
��G H
Sum
��H K
(
��K L
d
��L M
=>
��N P
d
��Q R
.
��R S
ImporteRevocado
��S b
+
��c d
d
��e f
.
��f g
IcbperRevocado
��g u
+
��v w
d
��x y
.
��y z
ImporteDevuelto��z �
+��� �
d��� �
.��� �
IcbperDevuelto��� �
)��� �
;��� �
var
�� 
detallesOperacion
�� %
=
��& '
new
��( +
List
��, 0
<
��0 1!
Detalle_transaccion
��1 D
>
��D E
(
��E F
)
��F G
;
��G H
var
�� 0
"detallesMovimientoAlmacenOperacion
�� 6
=
��7 8
new
��9 <
List
��= A
<
��A B!
Detalle_transaccion
��B U
>
��U V
(
��V W
)
��W X
;
��X Y)
CalcularOperacionCompletada
�� +
(
��+ ,

��, 9
,
��9 :&
estadosTransaccionNuevos
��; S
)
��S T
;
��T U
foreach
�� 
(
�� 
var
�� 
detalleNota
�� (
in
��) +

��, 9
.
��9 :
DetallesNota
��: F
)
��F G
{
�� 
if
�� 
(
�� 
detalleNota
�� #
.
��# $

��$ 1
>
��2 3
$num
��4 5
)
��5 6
{
�� 
var
�� 
detalle
�� #
=
��$ %$
transaccionRepositorio
��& <
.
��< ='
ObtenerDetalleTransaccion
��= V
(
��V W

��W d
.
��d e
DetallesOperacion
��e v
.
��v w
Single
��w }
(
��} ~
d
��~ 
=>��� �
d��� �
.��� �#
id_concepto_negocio��� �
==��� �
detalleNota��� �
.��� �
Producto��� �
.��� �
Id��� �
)��� �
.��� �
id��� �
)��� �
;��� �
detalle
�� 
.
��  

cantidad_1
��  *
+=
��+ -
detalleNota
��. 9
.
��9 :

��: G
;
��G H,
detallesTransaccionModificados
�� 6
.
��6 7
Add
��7 :
(
��: ;
detalle
��; B
)
��B C
;
��C D
HayImpactoAlmacen
�� )
=
��* +
HayImpactoAlmacen
��, =
||
��> @
false
��A F
;
��F G
}
�� 
if
�� 
(
�� 
(
�� 
detalleNota
�� $
.
��$ %

��% 2
+
��3 4
detalleNota
��5 @
.
��@ A

��A N
)
��N O
>
��P Q
$num
��R S
)
��S T
{
�� 
var
�� 
detalleOperacion
�� ,
=
��- .

��/ <
.
��< =
DetallesOperacion
��= N
.
��N O
First
��O T
(
��T U
d
��U V
=>
��W Y
d
��Z [
.
��[ \!
id_concepto_negocio
��\ o
==
��p r
detalleNota
��s ~
.
��~ 
Producto�� �
.��� �
Id��� �
)��� �
;��� �
detalleOperacion
�� (
.
��( )
cantidad
��) 1
=
��2 3
detalleNota
��4 ?
.
��? @

��@ M
+
��N O
detalleNota
��P [
.
��[ \

��\ i
;
��i j
detalleOperacion
�� (
.
��( )

cantidad_1
��) 3
=
��4 5
detalleNota
��6 A
.
��A B

��B O
;
��O P
detalleOperacion
�� (
.
��( )
total
��) .
=
��/ 0
detalleOperacion
��1 A
.
��A B
cantidad
��B J
*
��K L
detalleOperacion
��M ]
.
��] ^
precio_unitario
��^ m
;
��m n
detallesOperacion
�� )
.
��) *
Add
��* -
(
��- .
detalleOperacion
��. >
)
��> ?
;
��? @
var
�� (
detalleMovimientoOperacion
�� 6
=
��7 8

��9 F
.
��F G%
DetallesBienesOperacion
��G ^
.
��^ _
First
��_ d
(
��d e
d
��e f
=>
��g i
d
��j k
.
��k l!
id_concepto_negocio
��l 
==��� �
detalleNota��� �
.��� �
Producto��� �
.��� �
Id��� �
)��� �
;��� �(
detalleMovimientoOperacion
�� 2
.
��2 3
cantidad
��3 ;
=
��< =
detalleNota
��> I
.
��I J

��J W
;
��W X(
detalleMovimientoOperacion
�� 2
.
��2 3

cantidad_1
��3 =
=
��> ?
$num
��@ A
;
��A B(
detalleMovimientoOperacion
�� 2
.
��2 3
total
��3 8
=
��9 :(
detalleMovimientoOperacion
��; U
.
��U V
cantidad
��V ^
*
��_ `(
detalleMovimientoOperacion
��a {
.
��{ |
precio_unitario��| �
;��� �0
"detallesMovimientoAlmacenOperacion
�� :
.
��: ;
Add
��; >
(
��> ?(
detalleMovimientoOperacion
��? Y
)
��Y Z
;
��Z [
}
�� 
if
�� 
(
�� 
detalleNota
�� #
.
��# $

��$ 1
>
��2 3
$num
��4 5
)
��5 6
{
�� 

�� %
.
��% &&
HayMovimientoAlmacenNota
��& >
=
��? @
(
��A B
!
��B C
VentasSettings
��C Q
.
��Q R
Default
��R Y
.
��Y Z*
MostrarSeccionEntregaEnVenta
��Z v
)
��v w
||
��x z
(
��{ |
VentasSettings��| �
.��� �
Default��� �
.��� �,
MostrarSeccionEntregaEnVenta��� �
&&��� �
!��� �

.��� �#
EsDiferidaOperacion��� �
)��� �
;��� �

�� %
.
��% &)
IndicadorImpactoAlmacenNota
��& A
=
��B C

��D Q
.
��Q R!
EsDiferidaOperacion
��R e
?
��f g
(
��h i
int
��i l
)
��l m&
IndicadorImpactoAlmacen��m �
.��� �
Diferida��� �
:��� �
(��� �
int��� �
)��� �'
IndicadorImpactoAlmacen��� �
.��� �
	Inmediata��� �
;��� �
HayImpactoAlmacen
�� )
=
��* +
HayImpactoAlmacen
��, =
||
��> @
true
��A E
;
��E F
}
�� 
}
�� 

�� 
.
�� 
DetallesOperacion
�� /
=
��0 1
detallesOperacion
��2 C
;
��C D

�� 
.
�� 0
"DetallesMovimientoAlmacenOperacion
�� @
=
��A B0
"detallesMovimientoAlmacenOperacion
��C e
;
��e f
RecalcularCuotas_
�� !
(
��! "

��" /
,
��/ 0
cuotasModificadas
��1 B
)
��B C
;
��C D
if
�� 
(
�� 
!
�� 
HayImpactoAlmacen
�� &
)
��& '

�� !
.
��! ")
IndicadorImpactoAlmacenNota
��" =
=
��> ?
(
��@ A
int
��A D
)
��D E%
IndicadorImpactoAlmacen
��E \
.
��\ ]5
&NoImpactaPorQueRevocaAOperacionInicial��] �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 

�� "
.
��" #

IdTipoNota
��# -
==
��. 0
MaestroSettings
��1 @
.
��@ A
Default
��A H
.
��H IF
7IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal��I �
)��� �
{
�� 

�� 
.
�� 
DetallesOperacion
�� /
=
��0 1
new
��2 5
List
��6 :
<
��: ;!
Detalle_transaccion
��; N
>
��N O
(
��O P
)
��P Q
{
��R S
new
��T W!
Detalle_transaccion
��X k
(
��k l
$num
��l m
,
��m n
ConceptoSettings
��o 
.�� �
Default��� �
.��� �0
 IdConceptoNegocioDescuentoGlobal��� �
,��� �

.��� �$
ObservacionOperacion��� �
,��� �

.��� �
	MontoNota��� �
,��� �

.��� �
	MontoNota��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
null��� �
,��� �
null��� �
)��� �
}��� �
;��� �

�� 
.
�� 
Icbper
�� $
=
��% &
$num
��' (
;
��( )

�� 
.
�� 
ImporteTotal
�� *
=
��+ ,

��- :
.
��: ;
	MontoNota
��; D
;
��D E
RecalcularCuotas_
�� !
(
��! "

��" /
,
��/ 0
cuotasModificadas
��1 B
)
��B C
;
��C D
}
�� 
else
�� 
if
�� 
(
�� 

�� "
.
��" #

IdTipoNota
��# -
==
��. 0
MaestroSettings
��1 @
.
��@ A
Default
��A H
.
��H IG
8IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem��I �
)��� �
{
�� 

�� 
.
�� 
DetallesOperacion
�� /
=
��0 1

��2 ?
.
��? @
DetallesNota
��@ L
.
��L M
Where
��M R
(
��R S
d
��S T
=>
��U W
d
��X Y
.
��Y Z
MontoDetalle
��Z f
!=
��g i
$num
��j k
)
��k l
.
��l m
Select
��m s
(
��s t
d
��t u
=>
��v x
{
��y z
d
��{ |
.
��| }
PrecioUnitario��} �
=��� �
d��� �
.��� �
MontoDetalle��� �
/��� �
d��� �
.��� �
Cantidad��� �
;��� �
d��� �
.��� �
Importe��� �
=��� �
d��� �
.��� �
MontoDetalle��� �
;��� �
return��� �
d��� �
;��� �
}��� �
)��� �
.��� �
Select��� �
(��� �
d��� �
=>��� �
d��� �
.��� �"
DetalleTransaccion��� �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �

�� 
.
�� 
Icbper
�� $
=
��% &
$num
��' (
;
��( )

�� 
.
�� 
ImporteTotal
�� *
=
��+ ,

��- :
.
��: ;
DetallesNota
��; G
.
��G H
Sum
��H K
(
��K L
d
��L M
=>
��N P
d
��Q R
.
��R S
MontoDetalle
��S _
)
��_ `
;
��` a
RecalcularCuotas_
�� !
(
��! "

��" /
,
��/ 0
cuotasModificadas
��1 B
)
��B C
;
��C D
}
�� 
else
�� 
if
�� 
(
�� 

�� "
.
��" #

IdTipoNota
��# -
==
��. 0
MaestroSettings
��1 @
.
��@ A
Default
��A H
.
��H IF
7IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora��I �
)��� �
{
�� 

�� 
.
�� 
DetallesOperacion
�� /
=
��0 1
new
��2 5
List
��6 :
<
��: ;!
Detalle_transaccion
��; N
>
��N O
(
��O P
)
��P Q
{
��R S
new
��T W!
Detalle_transaccion
��X k
(
��k l
$num
��l m
,
��m n
ConceptoSettings
��o 
.�� �
Default��� �
.��� �/
IdConceptoNegocioInteresPorMora��� �
,��� �

.��� �$
ObservacionOperacion��� �
,��� �

.��� �
	MontoNota��� �
,��� �

.��� �
	MontoNota��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
null��� �
,��� �
null��� �
)��� �
}��� �
;��� �

�� 
.
�� 
Icbper
�� $
=
��% &
$num
��' (
;
��( )

�� 
.
�� 
ImporteTotal
�� *
=
��+ ,

��- :
.
��: ;
	MontoNota
��; D
;
��D E
RecalcularCuotas_
�� !
(
��! "

��" /
,
��/ 0
cuotasModificadas
��1 B
)
��B C
;
��C D
}
�� 
else
�� 
if
�� 
(
�� 

�� "
.
��" #

IdTipoNota
��# -
==
��. 0
MaestroSettings
��1 @
.
��@ A
Default
��A H
.
��H IF
7IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor��I �
)��� �
{
�� 

�� 
.
�� 
DetallesOperacion
�� /
=
��0 1

��2 ?
.
��? @
DetallesNota
��@ L
.
��L M
Where
��M R
(
��R S
d
��S T
=>
��U W
d
��X Y
.
��Y Z
MontoDetalle
��Z f
!=
��g i
$num
��j k
)
��k l
.
��l m
Select
��m s
(
��s t
d
��t u
=>
��v x
{
��y z
d
��{ |
.
��| }
PrecioUnitario��} �
=��� �
d��� �
.��� �
MontoDetalle��� �
/��� �
d��� �
.��� �
Cantidad��� �
;��� �
d��� �
.��� �
Importe��� �
=��� �
d��� �
.��� �
MontoDetalle��� �
;��� �
return��� �
d��� �
;��� �
}��� �
)��� �
.��� �
Select��� �
(��� �
d��� �
=>��� �
d��� �
.��� �"
DetalleTransaccion��� �
(��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �

�� 
.
�� 
Icbper
�� $
=
��% &
$num
��' (
;
��( )

�� 
.
�� 
ImporteTotal
�� *
=
��+ ,

��- :
.
��: ;
DetallesNota
��; G
.
��G H
Sum
��H K
(
��K L
d
��L M
=>
��N P
d
��Q R
.
��R S
MontoDetalle
��S _
)
��_ `
;
��` a
RecalcularCuotas_
�� !
(
��! "

��" /
,
��/ 0
cuotasModificadas
��1 B
)
��B C
;
��C D
}
�� 
if
�� 
(
�� 

�� 
.
�� 
GravaIgv
�� &
)
��& '
{
�� 

�� 
.
�� 
DetallesOperacion
�� /
.
��/ 0
ForEach
��0 7
(
��7 8
d
��8 9
=>
��: <
d
��= >
.
��> ?
igv
��? B
=
��C D
Decimal
��E L
.
��L M
Round
��M R
(
��R S
d
��S T
.
��T U
total
��U Z
-
��[ \
(
��] ^
d
��^ _
.
��_ `
total
��` e
/
��f g
(
��h i
$num
��i j
+
��k l"
TransaccionSettings��m �
.��� �
Default��� �
.��� �
TasaIGV��� �
)��� �
)��� �
,��� �
$num��� �
)��� �
)��� �
;��� �

�� 
.
�� %
DetallesBienesOperacion
�� 5
.
��5 6
ForEach
��6 =
(
��= >
d
��> ?
=>
��@ B
d
��C D
.
��D E
igv
��E H
=
��I J
Decimal
��K R
.
��R S
Round
��S X
(
��X Y
d
��Y Z
.
��Z [
total
��[ `
-
��a b
(
��c d
d
��d e
.
��e f
total
��f k
/
��l m
(
��n o
$num
��o p
+
��q r"
TransaccionSettings��s �
.��� �
Default��� �
.��� �
TasaIGV��� �
)��� �
)��� �
,��� �
$num��� �
)��� �
)��� �
;��� �
if
�� 
(
�� 

�� !
.
��! "0
"DetallesMovimientoAlmacenOperacion
��" D
!=
��E G
null
��H L
)
��L M

�� !
.
��! "0
"DetallesMovimientoAlmacenOperacion
��" D
.
��D E
ForEach
��E L
(
��L M
d
��M N
=>
��O Q
d
��R S
.
��S T
igv
��T W
=
��X Y
Decimal
��Z a
.
��a b
Round
��b g
(
��g h
d
��h i
.
��i j
total
��j o
-
��p q
(
��r s
d
��s t
.
��t u
total
��u z
/
��{ |
(
��} ~
$num
��~ 
+��� �#
TransaccionSettings��� �
.��� �
Default��� �
.��� �
TasaIGV��� �
)��� �
)��� �
,��� �
$num��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 

�� 
.
�� 

IdTipoNota
�� (
!=
��) +
MaestroSettings
��, ;
.
��; <
Default
��< C
.
��C DM
>IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion��D �
||��� �

.��� �

IdTipoNota��� �
!=��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �G
7IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal��� �
)��� �
{
�� 

�� 
.
�� 
Igv
�� !
=
��" #

��$ 1
.
��1 2
DetallesOperacion
��2 C
.
��C D
Sum
��D G
(
��G H
d
��H I
=>
��J L
d
��M N
.
��N O
igv
��O R
)
��R S
;
��S T
}
�� 
}
�� 	
public
�� 
void
�� )
CalcularOperacionCompletada
�� /
(
��/ 0

��0 =

��> K
,
��K L
List
��M Q
<
��Q R 
Estado_transaccion
��R d
>
��d e&
estadosTransaccionNuevos
��f ~
)
��~ 
{
�� 	
var
�� 
ordenAlmacen
�� 
=
�� !
ObtenerOrdenAlmacen
�� 2
(
��2 3

��3 @
.
��@ A

OrdenVenta
��A K
.
��K L
Id
��L N
)
��N O
;
��O P
var
�� 
hayMontoRevocado
��  
=
��! "

��# 0
.
��0 1
DetallesNota
��1 =
.
��= >
Sum
��> A
(
��A B
d
��B C
=>
��D F
d
��G H
.
��H I

��I V
)
��V W
>
��X Y
$num
��Z [
;
��[ \
var
�� 
todoRevocado
�� 
=
�� 
true
�� #
;
��# $
foreach
�� 
(
�� 
var
�� 
detalle
��  
in
��! #
ordenAlmacen
��$ 0
.
��0 1
Detalles
��1 9
)
��9 :
{
�� 
if
�� 
(
�� 
detalle
�� 
.
�� 
	Pendiente
�� %
>
��& '
$num
��( )
)
��) *
{
�� 
todoRevocado
��  
=
��! "
todoRevocado
��# /
&&
��0 2
(
��3 4
detalle
��4 ;
.
��; <
	Pendiente
��< E
==
��F H

��I V
.
��V W
DetallesNota
��W c
.
��c d
FirstOrDefault
��d r
(
��r s
dn
��s u
=>
��v x
dn
��y {
.
��{ |
Producto��| �
.��� �
Id��� �
==��� �
detalle��� �
.��� �

IdConcepto��� �
)��� �
.��� �

)��� �
;��� �
}
�� 
}
�� 
if
�� 
(
�� 
hayMontoRevocado
��  
&&
��! #
todoRevocado
��$ 0
)
��0 1
{
�� 
estadosTransaccionNuevos
�� (
.
��( )
Add
��) ,
(
��, -
new
��- 0 
Estado_transaccion
��1 C
(
��C D

��D Q
.
��Q R

OrdenVenta
��R \
.
��\ ]
Id
��] _
,
��_ `

��a n
.
��n o

IdEmpleado
��o y
,
��y z
MaestroSettings��{ �
.��� �
Default��� �
.��� �0
 IdDetalleMaestroEstadoCompletada��� �
,��� �

.��� �
FechaActual��� �
,��� �
$str��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 
void
�� 
RecalcularCuotas_
�� %
(
��% &

��& 3

��4 A
,
��A B
List
��C G
<
��G H
Cuota
��H M
>
��M N
cuotasModificadas
��O `
)
��` a
{
�� 	
var
�� 

�� 
=
�� 

��  -
.
��- .
ImporteTotal
��. :
;
��: ;
if
�� 
(
�� 
!
�� 

�� 
.
�� 
EsDebito
�� '
)
��' (
{
�� 
foreach
�� 
(
�� 
var
�� 
cuota
�� "
in
��# %

��& 3
.
��3 4
Cuotas
��4 :
.
��: ;
OrderByDescending
��; L
(
��L M
c
��M N
=>
��O Q
c
��R S
.
��S T
fecha_vencimiento
��T e
)
��e f
)
��f g
{
�� 
if
�� 
(
�� 

�� %
>
��& '
$num
��( )
&&
��* ,
cuota
��- 2
.
��2 3
saldo
��3 8
>
��9 :
$num
��; <
)
��< =
{
�� 
cuota
�� 
.
�� 
revocado
�� &
+=
��' )
cuota
��* /
.
��/ 0
saldo
��0 5
>=
��6 8

��9 F
?
��G H
Math
��I M
.
��M N
Round
��N S
(
��S T

��T a
,
��a b
$num
��c d
)
��d e
:
��f g
Math
��h l
.
��l m
Round
��m r
(
��r s
cuota
��s x
.
��x y
saldo
��y ~
,
��~ 
$num��� �
)��� �
;��� �

�� %
-=
��& (
Math
��) -
.
��- .
Round
��. 3
(
��3 4
cuota
��4 9
.
��9 :
saldo
��: ?
,
��? @
$num
��A B
)
��B C
;
��C D
cuota
�� 
.
�� 
saldo
�� #
=
��$ %
Math
��& *
.
��* +
Round
��+ 0
(
��0 1
cuota
��1 6
.
��6 7
total
��7 <
,
��< =
$num
��> ?
)
��? @
-
��A B
Math
��C G
.
��G H
Round
��H M
(
��M N
cuota
��N S
.
��S T

��T a
,
��a b
$num
��c d
)
��d e
-
��f g
Math
��h l
.
��l m
Round
��m r
(
��r s
cuota
��s x
.
��x y
revocado��y �
,��� �
$num��� �
)��� �
;��� �
cuotasModificadas
�� )
.
��) *
Add
��* -
(
��- .
cuota
��. 3
)
��3 4
;
��4 5
}
�� 
}
�� 
}
�� 

�� 
.
�� 
ImportePagoTotal
�� *
=
��+ ,
Math
��- 1
.
��1 2
Round
��2 7
(
��7 8

��8 E
,
��E F
$num
��G H
)
��H I
;
��I J
}
�� 	
public
�� 
void
�� '
ValidarNotaCreditoEnVenta
�� -
(
��- .
OrdenDeVenta
��. :
ordenDeVenta
��; G
,
��G H
decimal
��I P)
importeTotalDeNotaDeCredito
��Q l
)
��l m
{
�� 	
if
�� 
(
�� )
importeTotalDeNotaDeCredito
�� +
>
��, -
$num
��. /
)
��/ 0
{
�� 
var
�� 5
'transaccionesReferenciaDeLaOrdenDeVenta
�� ;
=
��< =
ordenDeVenta
��> J
.
��J K
Transaccion
��K V
(
��V W
)
��W X
.
��X Y

��Y f
;
��f g
if
�� 
(
�� 5
'transaccionesReferenciaDeLaOrdenDeVenta
�� ;
.
��; <
Count
��< A
>
��B C
$num
��D E
)
��E F
{
�� 
var
�� )
sumaImporteDeNotasDeCredito
�� 3
=
��4 55
'transaccionesReferenciaDeLaOrdenDeVenta
��6 ]
.
��] ^
Where
��^ c
(
��c d
t
��d e
=>
��f h
Diccionario
��i t
.
��t uO
@TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCredito��u �
.��� �
Contains��� �
(��� �
t��� �
.��� �#
id_tipo_transaccion��� �
)��� �
)��� �
.��� �
Sum��� �
(��� �
t��� �
=>��� �
t��� �
.��� �

)��� �
;��� �
var
�� (
sumaImporteDeNotasDeDebito
�� 2
=
��3 45
'transaccionesReferenciaDeLaOrdenDeVenta
��5 \
.
��\ ]
Where
��] b
(
��b c
t
��c d
=>
��e g
Diccionario
��h s
.
��s tN
?TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeDebito��t �
.��� �
Contains��� �
(��� �
t��� �
.��� �#
id_tipo_transaccion��� �
)��� �
)��� �
.��� �
Sum��� �
(��� �
t��� �
=>��� �
t��� �
.��� �

)��� �
;��� �
if
�� 
(
�� )
importeTotalDeNotaDeCredito
�� 3
>
��4 5
ordenDeVenta
��6 B
.
��B C
Total
��C H
+
��I J(
sumaImporteDeNotasDeDebito
��K e
-
��f g*
sumaImporteDeNotasDeCredito��h �
)��� �
{
�� 
throw
�� 
new
�� !
LogicaException
��" 1
(
��1 2
$str��2 �
)��� �
;��� �
}
�� 
}
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
Transaccion
�� 
>
��  -
GenerarTransaccionesAModificar_
��! @
(
��@ A!
MovimientoEconomico
��A T*
movimientoEconomicoConPuntos
��U q
)
��q r
{
�� 	
List
�� 
<
�� 
Transaccion
�� 
>
�� %
transaccionesAModificar
�� 5
=
��6 7
null
��8 <
;
��< =
if
�� 
(
�� *
movimientoEconomicoConPuntos
�� ,
!=
��- /
null
��0 4
)
��4 5
{
�� 
var
��  
extensionJsonTraza
�� &
=
��' (*
movimientoEconomicoConPuntos
��) E
.
��E F
TrazaDePago
��F Q
(
��Q R
)
��R S
.
��S T

��T a
;
��a b
var
�� 
puntosCanjeados
�� #
=
��$ %
JsonConvert
��& 1
.
��1 2
DeserializeObject
��2 C
<
��C D
List
��D H
<
��H I

��I V
>
��V W
>
��W X
(
��X Y 
extensionJsonTraza
��Y k
)
��k l
;
��l m
var
�� #
transaccionesDePuntos
�� )
=
��* +$
transaccionRepositorio
��, B
.
��B C"
ObtenerTransacciones
��C W
(
��W X
puntosCanjeados
��X g
.
��g h
Select
��h n
(
��n o
pc
��o q
=>
��r t
pc
��u w
.
��w x
Id
��x z
)
��z {
.
��{ |
ToArray��| �
(��� �
)��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  
transaccionDePunto
�� /
in
��0 2#
transaccionesDePuntos
��3 H
)
��H I
{
�� %
transaccionesAModificar
�� +
=
��, -
new
��. 1
List
��2 6
<
��6 7
Transaccion
��7 B
>
��B C
(
��C D
)
��D E
;
��E F
var
�� ,
puntosPendientesDeModificacion
�� 6
=
��7 8
puntosCanjeados
��9 H
.
��H I
Single
��I O
(
��O P
pc
��P R
=>
��S U
pc
��V X
.
��X Y
Id
��Y [
==
��\ ^ 
transaccionDePunto
��_ q
.
��q r
id
��r t
)
��t u
.
��u v
Cantidad
��v ~
;
��~  
transaccionDePunto
�� &
.
��& '
	cantidad2
��' 0
-=
��1 3,
puntosPendientesDeModificacion
��4 R
;
��R S 
transaccionDePunto
�� &
.
��& '
	cantidad3
��' 0
+=
��1 3,
puntosPendientesDeModificacion
��4 R
;
��R S%
transaccionesAModificar
�� +
.
��+ ,
Add
��, /
(
��/ 0 
transaccionDePunto
��0 B
)
��B C
;
��C D
}
�� 
}
�� 
return
�� %
transaccionesAModificar
�� *
;
��* +
}
�� 	
}
�� 
}�� ו
]D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\EFactura\FacturacionElectronicaLogica_Consulta.cs
	namespace 	
Tsp
 
.
FacturacionElectronica $
.$ %
Logica% +
{ 
public 

partial 
class (
FacturacionElectronicaLogica 5
{ 
public!! 
bool!!  
HayBoletasNoEnviadas!! (
(!!( )
)!!) *
{"" 	
try## 
{$$ 
return&& 
_facturacionDatos&& (
.&&( )#
HayDocumentosNoEnviados&&) @
(&&@ A
MaestroSettings&&A P
.&&P Q
Default&&Q X
.&&X Y&
CodigoDetalleMaestroBoleta&&Y s
)&&s t
;&&t u
}'' 
catch(( 
((( 
	Exception(( 
e(( 
)(( 
{)) 
throw** 
new** 
LogicaException** )
(**) *
$str*** i
,**i j
e**k l
)**l m
;**m n
}++ 
},, 	
public.. 
List.. 
<.. 
	Documento.. 
>.. 9
-DevolverBoletasIncluidoBinarioPorEnviarPorDia.. L
(..L M
)..M N
{// 	
try00 
{11 
return22 
_facturacionDatos22 (
.22( )9
-ObtenerDocumentosIncluidoBinarioAEnviarPorDia22) V
(22V W
MaestroSettings22W f
.22f g
Default22g n
.22n o'
CodigoDetalleMaestroBoleta	22o �
)
22� �
.
22� �
ToList
22� �
(
22� �
)
22� �
;
22� �
}33 
catch44 
(44 
	Exception44 
e44 
)44 
{55 
throw66 
new66 
LogicaException66 )
(66) *
$str66* U
,66U V
e66W X
)66X Y
;66Y Z
}77 
}88 	
public:: 
List:: 
<:: 
	Documento:: 
>:: Q
EObtenerBoletasInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente:: d
(::d e
)::e f
{;; 	
try<< 
{== 
int>> 
[>> 
]>> '
estadosDeEnvioQueNoSeQuiere>> 1
=>>2 3
new>>4 7
int>>8 ;
[>>; <
]>>< =
{>>> ?
(>>@ A
int>>A D
)>>D E
EstadoEnvio>>E P
.>>P Q
	Pendiente>>Q Z
,>>Z [
(>>\ ]
int>>] `
)>>` a
EstadoEnvio>>a l
.>>l m
	Rechazado>>m v
}>>w x
;>>x y
return@@ 
_facturacionDatos@@ (
.@@( )6
*ObtenerDocumentosFaltantesAEmitirAnulacion@@) S
(@@S T.
"EstadoSigescomDocumentoElectronico@@T v
.@@v w

Invalidado	@@w �
,
@@� �
$num
@@� �
,
@@� �
MaestroSettings
@@� �
.
@@� �
Default
@@� �
.
@@� �(
CodigoDetalleMaestroBoleta
@@� �
,
@@� �)
estadosDeEnvioQueNoSeQuiere
@@� �
)
@@� �
.
@@� �
ToList
@@� �
(
@@� �
)
@@� �
;
@@� �
}AA 
catchBB 
(BB 
	ExceptionBB 
eBB 
)BB 
{CC 
throwDD 
newDD 
LogicaExceptionDD )
(DD) *
$strDD* P
,DDP Q
eDDR S
)DDS T
;DDT U
}EE 
}FF 	
publicHH 
ListHH 
<HH 
	DocumentoHH 
>HH R
FObtenerFacturasInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendienteHH e
(HHe f
)HHf g
{II 	
tryJJ 
{KK 
intLL 
[LL 
]LL '
estadosDeEnvioQueNoSeQuiereLL 1
=LL2 3
newLL4 7
intLL8 ;
[LL; <
]LL< =
{LL> ?
(LL@ A
intLLA D
)LLD E
EstadoEnvioLLE P
.LLP Q
	PendienteLLQ Z
,LLZ [
(LL\ ]
intLL] `
)LL` a
EstadoEnvioLLa l
.LLl m
	RechazadoLLm v
}LLw x
;LLx y
returnNN 
_facturacionDatosNN (
.NN( )6
*ObtenerDocumentosFaltantesAEmitirAnulacionNN) S
(NNS T.
"EstadoSigescomDocumentoElectronicoNNT v
.NNv w

Invalidado	NNw �
,
NN� �
$num
NN� �
,
NN� �
MaestroSettings
NN� �
.
NN� �
Default
NN� �
.
NN� �)
CodigoDetalleMaestroFactura
NN� �
,
NN� �)
estadosDeEnvioQueNoSeQuiere
NN� �
)
NN� �
.
NN� �
ToList
NN� �
(
NN� �
)
NN� �
;
NN� �
}OO 
catchPP 
(PP 
	ExceptionPP 
ePP 
)PP 
{QQ 
throwRR 
newRR 
LogicaExceptionRR )
(RR) *
$strRR* P
,RRP Q
eRRR S
)RRS T
;RRT U
}SS 
}TT 	
publicZZ 
ListZZ 
<ZZ 
	DocumentoZZ 
>ZZ A
5DevolverFacturasNoInvalidadasIncluidoBinarioPorEnviarZZ T
(ZZT U
)ZZU V
{[[ 	
try\\ 
{]] 
return^^ 
_facturacionDatos^^ (
.^^( )3
'ObtenerDocumentosIncluidoBinarioAEnviar^^) P
(^^P Q
MaestroSettings^^Q `
.^^` a
Default^^a h
.^^h i(
CodigoDetalleMaestroFactura	^^i �
,
^^� �
(
^^� �
int
^^� �
)
^^� �(
EstadoDocumentoElectronico
^^� �
.
^^� �

Adicionado
^^� �
)
^^� �
.
^^� �
ToList
^^� �
(
^^� �
)
^^� �
;
^^� �
}__ 
catch`` 
(`` 
	Exception`` 
e`` 
)`` 
{aa 
throwbb 
ebb 
;bb 
}cc 
}dd 	
publicjj 
Listjj 
<jj 
	Documentojj 
>jj 8
,DevolverNotasCreditoIncluidoBinarioPorEnviarjj K
(jjK L
)jjL M
{kk 	
tryll 
{mm 
returnnn 
_facturacionDatosnn (
.nn( )3
'ObtenerDocumentosIncluidoBinarioAEnviarnn) P
(nnP Q
MaestroSettingsnnQ `
.nn` a
Defaultnna h
.nnh i.
!CodigoDetalleMaestroNotaDeCredito	nni �
)
nn� �
.
nn� �
ToList
nn� �
(
nn� �
)
nn� �
;
nn� �
}oo 
catchpp 
(pp 
	Exceptionpp 
epp 
)pp 
{qq 
throwrr 
err 
;rr 
}ss 
}tt 	
publicvv 
Listvv 
<vv 
	Documentovv 
>vv 7
+DevolverNotasDebitoIncluidoBinarioPorEnviarvv J
(vvJ K
)vvK L
{ww 	
tryxx 
{yy 
returnzz 
_facturacionDatoszz (
.zz( )3
'ObtenerDocumentosIncluidoBinarioAEnviarzz) P
(zzP Q
MaestroSettingszzQ `
.zz` a
Defaultzza h
.zzh i-
 CodigoDetalleMaestroNotaDeDebito	zzi �
)
zz� �
.
zz� �
ToList
zz� �
(
zz� �
)
zz� �
;
zz� �
}{{ 
catch|| 
(|| 
	Exception|| 
e|| 
)|| 
{}} 
throw~~ 
e~~ 
;~~ 
} 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� =
/DevolverGuiasDeRemisionIncluidoBinarioPorEnviar
�� N
(
��N O
)
��O P
{
�� 	
try
�� 
{
�� 
return
�� 
_facturacionDatos
�� (
.
��( )5
'ObtenerDocumentosIncluidoBinarioAEnviar
��) P
(
��P Q
MaestroSettings
��Q `
.
��` a
Default
��a h
.
��h i:
+CodigoDetalleMaestroGuiaDeRemisionRemitente��i �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� <
.DevolverGuiaDeRemisionIncluidoBinarioPorEnviar
�� M
(
��M N
long
��N R
idDocumento
��S ^
)
��^ _
{
�� 	
try
�� 
{
�� 
return
�� 
_facturacionDatos
�� (
.
��( )5
'ObtenerDocumentosIncluidoBinarioAEnviar
��) P
(
��P Q
MaestroSettings
��Q `
.
��` a
Default
��a h
.
��h i:
+CodigoDetalleMaestroGuiaDeRemisionRemitente��i �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� X
JObtenerNotasCreditoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente
�� i
(
��i j
)
��j k
{
�� 	
try
�� 
{
�� 
int
�� 
[
�� 
]
�� )
estadosDeEnvioQueNoSeQuiere
�� 1
=
��2 3
new
��4 7
int
��8 ;
[
��; <
]
��< =
{
��> ?
(
��@ A
int
��A D
)
��D E
EstadoEnvio
��E P
.
��P Q
	Pendiente
��Q Z
,
��Z [
(
��\ ]
int
��] `
)
��` a
EstadoEnvio
��a l
.
��l m
	Rechazado
��m v
}
��w x
;
��x y
return
�� 
_facturacionDatos
�� (
.
��( )8
*ObtenerDocumentosFaltantesAEmitirAnulacion
��) S
(
��S T0
"EstadoSigescomDocumentoElectronico
��T v
.
��v w

Invalidado��w �
,��� �
$num��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �1
!CodigoDetalleMaestroNotaDeCredito��� �
,��� �+
estadosDeEnvioQueNoSeQuiere��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� W
IObtenerNotasDebitoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente
�� h
(
��h i
)
��i j
{
�� 	
try
�� 
{
�� 
int
�� 
[
�� 
]
�� )
estadosDeEnvioQueNoSeQuiere
�� 1
=
��2 3
new
��4 7
int
��8 ;
[
��; <
]
��< =
{
��> ?
(
��@ A
int
��A D
)
��D E
EstadoEnvio
��E P
.
��P Q
	Pendiente
��Q Z
,
��Z [
(
��\ ]
int
��] `
)
��` a
EstadoEnvio
��a l
.
��l m
	Rechazado
��m v
}
��w x
;
��x y
return
�� 
_facturacionDatos
�� (
.
��( )8
*ObtenerDocumentosFaltantesAEmitirAnulacion
��) S
(
��S T0
"EstadoSigescomDocumentoElectronico
��T v
.
��v w

Invalidado��w �
,��� �
$num��� �
,��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �0
 CodigoDetalleMaestroNotaDeDebito��� �
,��� �+
estadosDeEnvioQueNoSeQuiere��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
bool
�� .
 HayFacturasInvalidadasNoEnviadas
�� 4
(
��4 5
)
��5 6
{
�� 	
try
�� 
{
�� 
return
�� 
_facturacionDatos
�� (
.
��( )%
HayDocumentosNoEnviados
��) @
(
��@ A
MaestroSettings
��A P
.
��P Q
Default
��Q X
.
��X Y)
CodigoDetalleMaestroFactura
��Y t
,
��t u
(
��v w
int
��w z
)
��z {)
EstadoDocumentoElectronico��{ �
.��� �
Anulado��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� 9
+DevolverFacturasInvalidadasNoEnviadasPorDia
�� J
(
��J K
)
��K L
{
�� 	
try
�� 
{
�� 
return
�� 
_facturacionDatos
�� (
.
��( ),
ObtenerDocumentosAEnviarPorDia
��) G
(
��G H
MaestroSettings
��H W
.
��W X
Default
��X _
.
��_ `)
CodigoDetalleMaestroFactura
��` {
,
��{ |
(
��} ~
int��~ �
)��� �*
EstadoDocumentoElectronico��� �
.��� �
Anulado��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� J
<ObtenerFacturasInvalidadasAceptadasPeroSinComunicacionDeBaja
�� [
(
��[ \
)
��\ ]
{
�� 	
try
�� 
{
�� 
return
�� 
_facturacionDatos
�� (
.
��( )
ObtenerDocumentos
��) :
(
��: ;
MaestroSettings
��; J
.
��J K
Default
��K R
.
��R S)
CodigoDetalleMaestroFactura
��S n
,
��n o1
"EstadoSigescomDocumentoElectronico��p �
.��� �

Invalidado��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �+
TipoEnvioComunicacionDeBaja��� �
,��� �
$num��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �#
TipoEnvioIndividual��� �
,��� �
$num��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� N
@ObtenerNotasCreditoInvalidadasAceptadasPeroSinComunicacionDeBaja
�� _
(
��_ `
)
��` a
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� 

documentos
��  *
=
��+ ,
_facturacionDatos
��- >
.
��> ?
ObtenerDocumentos
��? P
(
��P Q
MaestroSettings
��Q `
.
��` a
Default
��a h
.
��h i0
!CodigoDetalleMaestroNotaDeCredito��i �
,��� �2
"EstadoSigescomDocumentoElectronico��� �
.��� �

Invalidado��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �+
TipoEnvioComunicacionDeBaja��� �
,��� �
$num��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �#
TipoEnvioIndividual��� �
,��� �
$num��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
return
�� 

documentos
�� !
;
��! "
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� M
?ObtenerNotasDebitoInvalidadasAceptadasPeroSinComunicacionDeBaja
�� ^
(
��^ _
)
��_ `
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
	Documento
�� 
>
�� 

documentos
��  *
=
��+ ,
_facturacionDatos
��- >
.
��> ?
ObtenerDocumentos
��? P
(
��P Q
MaestroSettings
��Q `
.
��` a
Default
��a h
.
��h i/
 CodigoDetalleMaestroNotaDeDebito��i �
,��� �2
"EstadoSigescomDocumentoElectronico��� �
.��� �

Invalidado��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �+
TipoEnvioComunicacionDeBaja��� �
,��� �
$num��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �#
TipoEnvioIndividual��� �
,��� �
$num��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
return
�� 

documentos
�� !
;
��! "
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
e
�� 
;
�� 
}
�� 
}
�� 	
}
�� 
}�� ��
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\EFactura\FacturacionElectronicaLogica_ConsultasApi.cs
	namespace 	
Tsp
 
.
FacturacionElectronica $
.$ %
Logica% +
{ 
public 

partial 
class (
FacturacionElectronicaLogica 5
{ 
public 
OperationResult 
ConsultarTickets /
(/ 0
)0 1
{ 	
try 
{ 
OperationResult 
result  &
=' (
null) -
;- .$
EstablecimientoComercial (
sede) -
=. /
_sedeLogica0 ;
.; <
ObtenerSede< G
(G H
)H I
;I J
List 
< 
EnvioSimplificado &
>& '$
enviosSinCodigoRespuesta( @
=A B-
!ObtenerEnviosSinCodigoDeRespuestaC d
(d e
)e f
;f g
List 
< 
EnvioSimplificado &
>& '*
enviosSinTicketCodigoRespuesta( F
=G H$
enviosSinCodigoRespuestaI a
.a b
Whereb g
(g h
eh i
=>j l
stringm s
.s t

(
� �
e
� �
.
� �
NumeroTicket
� �
)
� �
)
� �
.
� �
ToList
� �
(
� �
)
� �
;
� �
List 
< 
EnvioSimplificado &
>& '0
$enviosIndividualesSinCodigoRespuesta( L
=M N*
enviosSinTicketCodigoRespuestaO m
.m n
Wheren s
(s t
et u
=>v x
stringy 
.	 �

� �
(
� �
e
� �
.
� �
NumeroTicket
� �
)
� �
&&
� �,
FacturacionElectronicaSettings
� �
.
� �
Default
� �
.
� �!
TipoEnvioIndividual
� �
.
� �
Equals
� �
(
� �
e
� �
.
� �
	TipoEnvio
� �
)
� �
)
� �
.
� �
ToList
� �
(
� �
)
� �
;
� �
List   
<   
EnvioSimplificado   &
>  & '3
'enviosConTicketSinCodigoRespuestaTicket  ( O
=  P Q$
enviosSinCodigoRespuesta  R j
.  j k
Where  k p
(  p q
e  q r
=>  s u
!  v w
string  w }
.  } ~

(
  � �
e
  � �
.
  � �
NumeroTicket
  � �
)
  � �
&&
  � �
e
  � �
.
  � �!
CodigoTipoDocumento
  � �
!=
  � �
MaestroSettings
  � �
.
  � �
Default
  � �
.
  � �9
+CodigoDetalleMaestroGuiaDeRemisionRemitente
  � �
)
  � �
.
  � �
ToList
  � �
(
  � �
)
  � �
;
  � �
List!! 
<!! 
EnvioSimplificado!! &
>!!& '?
3enviosConTicketSinCodigoRespuestaTicketGuiaRemision!!( [
=!!\ ]$
enviosSinCodigoRespuesta!!^ v
.!!v w
Where!!w |
(!!| }
e!!} ~
=>	!! �
!
!!� �
string
!!� �
.
!!� �

!!� �
(
!!� �
e
!!� �
.
!!� �
NumeroTicket
!!� �
)
!!� �
&&
!!� �
e
!!� �
.
!!� �!
CodigoTipoDocumento
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
!!� �9
+CodigoDetalleMaestroGuiaDeRemisionRemitente
!!� �
)
!!� �
.
!!� �
ToList
!!� �
(
!!� �
)
!!� �
;
!!� �
foreach"" 
("" 
var"" 
envioAConsultar"" ,
in""- /0
$enviosIndividualesSinCodigoRespuesta""0 T
)""T U
{## 
try$$ 
{%% 
var&&  
documentoElectronico&& 0
=&&1 2
new&&3 6 
DocumentoElectronico&&7 K
(&&K L
)&&L M
{'' 
Emisor(( "
=((# $
new((% (
Compania(() 1
(((1 2
)((2 3
{((4 5
NroDocumento((6 B
=((C D
sede((E I
.((I J
DocumentoIdentidad((J \
}((] ^
,((^ _
IdDocumento)) '
=))( )
envioAConsultar))* 9
.))9 :
SerieDocumento)): H
+))I J
$str))K N
+))O P
envioAConsultar))Q `
.))` a
NumeroDocumento))a p
,))p q

=*** +
envioAConsultar**, ;
.**; <
CodigoTipoDocumento**< O
}++ 
;++ 
var,, 
respuestaConsulta,, -
=,,. /%
ResolverConsultaDocumento,,0 I
(,,I J 
documentoElectronico,,J ^
,,,^ _
envioAConsultar,,` o
.,,o p
Id,,p r
),,r s
;,,s t
}-- 
catch.. 
(.. 
	Exception.. $
)..$ %
{// 
}00 
}11 
foreach22 
(22 
var22 
envioAConsultar22 ,
in22- /3
'enviosConTicketSinCodigoRespuestaTicket220 W
)22W X
{33 
try44 
{55 
var66 
respuestaConsulta66 -
=66. /"
ResolverConsultaTicket660 F
(66F G
sede66G K
.66K L
DocumentoIdentidad66L ^
,66^ _
envioAConsultar66` o
)66o p
;66p q
}77 
catch88 
(88 
	Exception88 $
)88$ %
{99 
}:: 
};; 
foreach<< 
(<< 
var<< 
envioAConsultar<< ,
in<<- /?
3enviosConTicketSinCodigoRespuestaTicketGuiaRemision<<0 c
)<<c d
{== 
try>> 
{?? .
"ResolverConsultaTicketGuiaRemision@@ :
(@@: ;
sede@@; ?
.@@? @
DocumentoIdentidad@@@ R
,@@R S
envioAConsultar@@T c
)@@c d
;@@d e
}AA 
catchBB 
(BB 
	ExceptionBB $
)BB$ %
{CC 
}DD 
}EE 
resultFF 
=FF 
newFF 
OperationResultFF ,
(FF, -
OperationResultEnumFF- @
.FF@ A
SuccessFFA H
,FFH I
$strFFJ e
,FFe f
$strFFg k
)FFk l
;FFl m
returnGG 
resultGG 
;GG 
}HH 
catchII 
(II 
	ExceptionII 
eII 
)II 
{JJ 
throwKK 
newKK 
LogicaExceptionKK )
(KK) *
$strKK* F
,KKF G
eKKH I
)KKI J
;KKJ K
}LL 
}MM 	
publicOO #
EnviarDocumentoResponseOO &%
ResolverConsultaDocumentoOO' @
(OO@ A 
DocumentoElectronicoOOA U 
documentoElectronicoOOV j
,OOj k
longOOl p
idEnvioOOq x
)OOx y
{PP 	
varRR "
envioDocumentoResponseRR &
=RR' ((
ConsultarConstanciaDocumentoRR) E
(RRE F 
documentoElectronicoRRF Z
)RRZ [
;RR[ \
intSS 
numeroIntentosSS 
=SS  
$numSS! "
;SS" #
doTT 
{UU 
ifVV 
(VV 
!VV "
envioDocumentoResponseVV +
.VV+ ,
ExitoVV, 1
)VV1 2
{WW 
ThreadXX 
.XX 
SleepXX  
(XX  !*
FacturacionElectronicaSettingsXX! ?
.XX? @
DefaultXX@ G
.XXG H4
(TiempoEsperaParaConsultarIterativasEnvioXXH p
)XXp q
;XXq r"
envioDocumentoResponseYY *
=YY+ ,(
ConsultarConstanciaDocumentoYY- I
(YYI J 
documentoElectronicoYYJ ^
)YY^ _
;YY_ `
}ZZ 
else[[ 
{\\ &
ActualizarEnvioDeDocumento]] .
(]]. /"
envioDocumentoResponse]]/ E
,]]E F
idEnvio]]G N
)]]N O
;]]O P
}^^ 
numeroIntentos__ 
++__  
;__  !
}`` 
while`` 
(`` 
!`` "
envioDocumentoResponse`` ,
.``, -
Exito``- 2
||``3 5
numeroIntentos``6 D
<``E F*
FacturacionElectronicaSettings``G e
.``e f
Default``f m
.``m n5
(NumeroIntentosConsultaCDREnvioIndividual	``n �
)
``� �
;
``� �
returnaa "
envioDocumentoResponseaa )
;aa) *
}bb 	
publicdd 
OperationResultdd 
ConsultarTicketdd .
(dd. /
stringdd/ 5
rucdd6 9
,dd9 :
EnvioSimplificadodd; L
envioAConsultarddM \
)dd\ ]
{ee 	
tryff 
{gg 
OperationResulthh 
resulthh  &
=hh' (
nullhh) -
;hh- .
varii 
respuestaConsultaii %
=ii& '"
ResolverConsultaTicketii( >
(ii> ?
rucii? B
,iiB C
envioAConsultariiD S
)iiS T
;iiT U
resultjj 
=jj 
newjj 
OperationResultjj ,
(jj, -
OperationResultEnumjj- @
.jj@ A
SuccessjjA H
,jjH I
$strjjJ e
,jje f
$strjjg k
)jjk l
{kk 
informationll 
=ll  !#
DeterminarEstadoDeEnvioll" 9
(ll9 :
respuestaConsultall: K
)llK L
}mm 
;mm 
returnnn 
resultnn 
;nn 
}oo 
catchpp 
(pp 
	Exceptionpp 
epp 
)pp 
{qq 
throwrr 
newrr 
LogicaExceptionrr )
(rr) *
$strrr* P
,rrP Q
errR S
)rrS T
;rrT U
}ss 
}tt 	
publicvv #
EnviarDocumentoResponsevv &"
ResolverConsultaTicketvv' =
(vv= >
stringvv> D
rucvvE H
,vvH I
EnvioSimplificadovvJ [
envioAConsultarvv\ k
)vvk l
{ww 	
varxx "
envioDocumentoResponsexx &
=xx' (%
ConsultarConstanciaTicketxx) B
(xxB C
rucxxC F
,xxF G
envioAConsultarxxH W
)xxW X
;xxX Y
intyy 
numeroIntentosyy 
=yy  
$numyy! "
;yy" #
dozz 
{{{ 
if|| 
(|| 
!|| "
envioDocumentoResponse|| +
.||+ ,
Exito||, 1
)||1 2
{}} 
Thread~~ 
.~~ 
Sleep~~  
(~~  !*
FacturacionElectronicaSettings~~! ?
.~~? @
Default~~@ G
.~~G H4
(TiempoEsperaParaConsultarIterativasEnvio~~H p
)~~p q
;~~q r"
envioDocumentoResponse *
=+ ,%
ConsultarConstanciaTicket- F
(F G
rucG J
,J K
envioAConsultarL [
)[ \
;\ ]
}
�� 
else
�� 
{
�� (
ActualizarEnvioDeDocumento
�� .
(
��. /$
envioDocumentoResponse
��/ E
,
��E F
envioAConsultar
��G V
.
��V W
Id
��W Y
)
��Y Z
;
��Z [
}
�� 
numeroIntentos
�� 
++
��  
;
��  !
}
�� 
while
�� 
(
�� 
!
�� $
envioDocumentoResponse
�� ,
.
��, -
Exito
��- 2
||
��3 5
numeroIntentos
��6 D
<
��E F,
FacturacionElectronicaSettings
��G e
.
��e f
Default
��f m
.
��m n4
%NumeroIntentosConsultaCDREnvioResumen��n �
)��� �
;��� �
return
�� $
envioDocumentoResponse
�� )
;
��) *
}
�� 	
public
�� %
EnviarDocumentoResponse
�� &'
ConsultarConstanciaTicket
��' @
(
��@ A
string
��A G
ruc
��H K
,
��K L
EnvioSimplificado
��M ^
envioAConsultar
��_ n
)
��n o
{
�� 	
try
�� 
{
�� 
var
�� #
consultaTicketRequest
�� )
=
��* +
new
��, /#
ConsultaTicketRequest
��0 E
{
�� 
Ruc
�� 
=
�� 
ruc
�� 
,
�� 

UsuarioSol
�� 
=
��  ,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G H

UsuarioSol
��H R
,
��R S
ClaveSol
�� 
=
�� ,
FacturacionElectronicaSettings
�� =
.
��= >
Default
��> E
.
��E F
ClaveSol
��F N
,
��N O
EndPointUrl
�� 
=
��  !9
+DevolverUrlEnvioSunatFacturacionElectronica
��" M
(
��M N
)
��N O
,
��O P
	NroTicket
�� 
=
�� 
envioAConsultar
��  /
.
��/ 0
NumeroTicket
��0 <
}
�� 
;
�� 
var
�� 
respuestaConsulta
�� %
=
��& '

RestHelper
��( 2
<
��2 3#
ConsultaTicketRequest
��3 H
,
��H I%
EnviarDocumentoResponse
��J a
>
��a b
.
��b c
Execute
��c j
(
��j k
$str��k �
,��� �%
consultaTicketRequest��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� 
respuestaConsulta
�� &
.
��& '
Exito
��' ,
)
��, -
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str
��. M
+
��N O
envioAConsultar
��P _
.
��_ `
NumeroTicket
��` l
+
��m n
$str
��o z
+
��{ | 
respuestaConsulta��} �
.��� �
MensajeError��� �
)��� �
;��� �
}
�� 
return
�� 
respuestaConsulta
�� (
;
��( )
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* V
,
��V W
e
��X Y
)
��Y Z
;
��Z [
}
�� 
}
�� 	
public
�� 
void
�� 0
"ResolverConsultaTicketGuiaRemision
�� 6
(
��6 7
string
��7 =
ruc
��> A
,
��A B
EnvioSimplificado
��C T
envioAConsultar
��U d
)
��d e
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
new
��, /
OperationResult
��0 ?
(
��? @
)
��@ A
;
��A B
var
��  
tokenEnvioResponse
�� &
=
��' (+
ObtenerTokenEnvioGuiaRemision
��) F
(
��F G
ruc
��G J
)
��J K
;
��K L$
EnvioDocumentoResponse
�� &$
envioDocumentoResponse
��' =
=
��> ?
new
��@ C$
EnvioDocumentoResponse
��D Z
(
��Z [
)
��[ \
{
��] ^
	numTicket
��_ h
=
��i j
envioAConsultar
��k z
.
��z {
NumeroTicket��{ �
}��� �
;��� �
var
�� #
obtenerRespuestaSunat
�� )
=
��* +
true
��, 0
;
��0 1
var
�� 
numeroIntentos
�� "
=
��# $
$num
��% &
;
��& '-
RespuestaEnvioDocumentoResponse
�� /-
respuestaEnvioDocumentoResponse
��0 O
;
��O P
do
�� 
{
�� 
Thread
�� 
.
�� 
Sleep
��  
(
��  !,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G HK
<TiempoEsperaParaConsultarRespuestaGuiaRemisionEnMilisegundos��H �
)��� �
;��� �-
respuestaEnvioDocumentoResponse
�� 3
=
��4 5/
!ObtenerRespuestaEnvioGuiaRemision
��6 W
(
��W X$
envioDocumentoResponse
��X n
,
��n o!
tokenEnvioResponse��p �
)��� �
;��� �#
obtenerRespuestaSunat
�� )
=
��* +-
respuestaEnvioDocumentoResponse
��, K
.
��K L
codRespuesta
��L X
==
��Y [,
FacturacionElectronicaSettings
��\ z
.
��z {
Default��{ �
.��� �7
'CodigoApiEnProcesoRespuestaGuiaRemision��� �
;��� �
numeroIntentos
�� "
++
��" $
;
��$ %
}
�� 
while
�� 
(
�� #
obtenerRespuestaSunat
�� .
&&
��/ 1,
FacturacionElectronicaSettings
��2 P
.
��P Q
Default
��Q X
.
��X Y7
(NumeroIntentosConsultaCDREnvioIndividual��Y �
>=��� �
numeroIntentos��� �
)��� �
;��� �
if
�� 
(
�� #
obtenerRespuestaSunat
�� (
==
��) +
false
��, 1
)
��1 2
{
�� %
EnviarDocumentoResponse
�� +%
enviarDocumentoResponse
��, C
=
��D E-
respuestaEnvioDocumentoResponse
��F e
.
��e f
	Convertir
��f o
(
��o p
)
��p q
;
��q r(
ActualizarEnvioDeDocumento
�� .
(
��. /%
enviarDocumentoResponse
��/ F
,
��F G
envioAConsultar
��H W
.
��W X
Id
��X Z
)
��Z [
;
��[ \
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ]
,
��] ^
e
��_ `
)
��` a
;
��a b
}
�� 
}
�� 	
}
�� 
}�� ��

ZD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.Logica\EFactura\FacturacionElectronicaLogica_Envio.cs
	namespace"" 	
Tsp""
 
.""
FacturacionElectronica"" $
.""$ %
Logica""% +
{## 
public$$ 

static$$ 
class$$ 
	StringExt$$ !
{%% 
public&& 
static&& 
string&& 
Truncate&& %
(&&% &
this&&& *
string&&+ 1
value&&2 7
,&&7 8
int&&9 <
	maxLength&&= F
)&&F G
{'' 	
if(( 
((( 
string(( 
.(( 

((($ %
value((% *
)((* +
)((+ ,
return((- 3
value((4 9
;((9 :
return)) 
value)) 
.)) 
Length)) 
<=))  "
	maxLength))# ,
?))- .
value))/ 4
:))5 6
value))7 <
.))< =
	Substring))= F
())F G
$num))G H
,))H I
	maxLength))J S
)))S T
;))T U
}** 	
}++ 
public-- 

partial-- 
class-- (
FacturacionElectronicaLogica-- 5
{.. 
public11 
OperationResult11 

(11, -
LogEnvio11- 5
logEnvio116 >
)11> ?
{22 	
try33 
{44 
	Documento55 
[55 
]55 

documentos55 &
=55' (A
5DevolverFacturasNoInvalidadasIncluidoBinarioPorEnviar55) ^
(55^ _
)55_ `
.55` a
ToArray55a h
(55h i
)55i j
;55j k
if66 
(66 

documentos66 
.66 
Length66 %
>66& '
$num66( )
)66) *
{77 
var88 
result88 
=88  #
ResolverEnvioIndividual88! 8
(888 9

documentos889 C
,88C D
logEnvio88E M
)88M N
;88N O
Util99 
.99 +
ManejoEnLogicaResultadoSinExito99 8
(998 9
result999 ?
,99? @
$str99A i
)99i j
;99j k
}:: 
else;; 
{<< 
logEnvio== 
.== 
LogNoHayDocumentos== /
(==/ 0
true==0 4
,==4 5
$str==6 R
)==R S
;==S T
}>> 
}?? 
catch@@ 
(@@ 
	Exception@@ 
e@@ 
)@@ 
{AA 
logEnvioBB 
.BB 
ErrorBB 
.BB 
AddBB "
(BB" #
$strBB# K
+BBL M
$strBBN S
+BBT U
UtilBBV Z
.BBZ [
InicioErrorStringBB[ l
(BBl m
eBBm n
)BBn o
)BBo p
;BBp q
logEnvioCC 
.CC 
LogErrorCC !
.CC! "
AddCC" %
(CC% &
$strCC& N
+CCO P
$strCCQ V
+CCW X
UtilCCY ]
.CC] ^
SoloErrorStringCC^ m
(CCm n
eCCn o
)CCo p
)CCp q
;CCq r
}DD 
returnEE 
newEE 
OperationResultEE &
(EE& '
OperationResultEnumEE' :
.EE: ;
SuccessEE; B
,EEB C
$strEED _
,EE_ `
$strEEa e
)EEe f
;EEf g
}FF 	
publicHH 
OperationResultHH #
ResolverEnvioIndividualHH 6
(HH6 7
	DocumentoHH7 @
[HH@ A
]HHA B

documentosHHC M
,HHM N
LogEnvioHHO W
logEnvioHHX `
)HH` a
{II 	
forJJ 
(JJ 
intJJ 
iJJ 
=JJ 
$numJJ 
;JJ 
iJJ 
<JJ 

documentosJJ  *
.JJ* +
LengthJJ+ 1
;JJ1 2
iJJ3 4
++JJ4 6
)JJ6 7
{KK 
tryLL 
{MM  
DocumentoElectronicoNN ( 
documentoElectronicoNN) =
=NN> ?
JsonConvertNN@ K
.NNK L
DeserializeObjectNNL ]
<NN] ^ 
DocumentoElectronicoNN^ r
>NNr s
(NNs t
EncodingNNt |
.NN| }
UTF8	NN} �
.
NN� �
	GetString
NN� �
(
NN� �

documentos
NN� �
[
NN� �
i
NN� �
]
NN� �
.
NN� �
Binario
NN� �
.
NN� �
archivoBinario
NN� �
)
NN� �
)
NN� �
;
NN� �
boolOO )
documentoElectronicoEsUnaNotaOO 6
=OO7 8

documentosOO9 C
[OOC D
iOOD E
]OOE F
.OOF G!
codigoTipoComprobanteOOG \
==OO] _
MaestroSettingsOO` o
.OOo p
DefaultOOp w
.OOw x.
!CodigoDetalleMaestroNotaDeCredito	OOx �
||
OO� �

documentos
OO� �
[
OO� �
i
OO� �
]
OO� �
.
OO� �#
codigoTipoComprobante
OO� �
==
OO� �
MaestroSettings
OO� �
.
OO� �
Default
OO� �
.
OO� �.
 CodigoDetalleMaestroNotaDeDebito
OO� �
;
OO� �
ifPP 
(PP )
documentoElectronicoEsUnaNotaPP 5
)PP5 6
{QQ 
ifRR 
(RR 
!RR %
_generacionArchivosLogicaRR 6
.RR6 7*
DocumentoReferenciaFueAceptadoRR7 U
(RRU V 
documentoElectronicoRRV j
.RRj k
RelacionadosRRk w
.RRw x
FirstOrDefault	RRx �
(
RR� �
)
RR� �
.
RR� �

RR� �
,
RR� �"
documentoElectronico
RR� �
.
RR� �
Relacionados
RR� �
.
RR� �
FirstOrDefault
RR� �
(
RR� �
)
RR� �
.
RR� �
NroDocumento
RR� �
)
RR� �
)
RR� �
{SS 
throwTT !
newTT" %
LogicaExceptionTT& 5
(TT5 6
$strTT6 W
+TTX Y 
documentoElectronicoTTZ n
.TTn o
RelacionadosTTo {
.TT{ |
FirstOrDefault	TT| �
(
TT� �
)
TT� �
.
TT� �

TT� �
+
TT� �
$str
TT� �
+
TT� �"
documentoElectronico
TT� �
.
TT� �
Relacionados
TT� �
.
TT� �
FirstOrDefault
TT� �
(
TT� �
)
TT� �
.
TT� �
NroDocumento
TT� �
+
TT� �
$str
TT� �
)
TT� �
;
TT� �
}UU 
}VV 
varWW 
documentoResponseWW )
=WW* +

RestHelperWW, 6
<WW6 7 
DocumentoElectronicoWW7 K
,WWK L
DocumentoResponseWWM ^
>WW^ _
.WW_ `
ExecuteWW` g
(WWg h
ObtenerMetodoApiWWh x
(WWx y!
documentoElectronico	WWy �
.
WW� �

WW� �
)
WW� �
,
WW� �"
documentoElectronico
WW� �
,
WW� �,
FacturacionElectronicaSettings
WW� �
.
WW� �
Default
WW� �
.
WW� �*
UrlApiFacturacionElectronica
WW� �
)
WW� �
;
WW� �
ifXX 
(XX 
!XX 
documentoResponseXX *
.XX* +
ExitoXX+ 0
)XX0 1
{YY 
throwZZ 
newZZ !
LogicaExceptionZZ" 1
(ZZ1 2
documentoResponseZZ2 C
.ZZC D
MensajeErrorZZD P
)ZZP Q
;ZZQ R
}[[ 
var\\ 
archivoCertificado\\ *
=\\+ ,
ObtenerCertificado\\- ?
(\\? @ 
documentoElectronico\\@ T
.\\T U
Emisor\\U [
.\\[ \
NroDocumento\\\ h
)\\h i
;\\i j
var]] 
firmadoResponse]] '
=]]( )
FirmarDocumento]]* 9
(]]9 :
documentoResponse]]: K
,]]K L
archivoCertificado]]M _
)]]_ `
;]]` a
var^^ 
resultadoEnvio^^ &
=^^' (
EnviarIndividual^^) 9
(^^9 : 
documentoElectronico^^: N
,^^N O
firmadoResponse^^P _
,^^_ `

documentos^^a k
[^^k l
i^^l m
]^^m n
.^^n o
id^^o q
)^^q r
;^^r s
if__ 
(__ 
resultadoEnvio__ &
.__& '
code_result__' 2
==__3 5
OperationResultEnum__6 I
.__I J
Information__J U
)__U V
{`` 
logEnvioaa  
.aa  !
Erroraa! &
.aa& '
Addaa' *
(aa* + 
documentoElectronicoaa+ ?
.aa? @
IdDocumentoaa@ K
+aaL M
$straaN R
+aaS T
resultadoEnvioaaU c
.aac d
titleaad i
)aai j
;aaj k
breakbb 
;bb 
}cc 
ifdd 
(dd 
!dd 
stringdd 
.dd  

(dd- .
resultadoEnviodd. <
.dd< =
titledd= B
)ddB C
)ddC D
{ee 
logEnvioff  
.ff  !
Errorff! &
.ff& '
Addff' *
(ff* + 
documentoElectronicoff+ ?
.ff? @
IdDocumentoff@ K
+ffL M
$strffN R
+ffS T
resultadoEnvioffU c
.ffc d
titleffd i
)ffi j
;ffj k
}gg 
Utilhh 
.hh +
ManejoEnLogicaResultadoSinExitohh 8
(hh8 9
resultadoEnviohh9 G
,hhG H
$strhhI s
)hhs t
;hht u
logEnvioii 
.ii 
Exitoii "
.ii" #
Addii# &
(ii& '
	ItemEnvioii' 0
.ii0 1
ItemAdicionadoii1 ?
(ii? @ 
documentoElectronicoii@ T
.iiT U
IdDocumentoiiU `
)ii` a
)iia b
;iib c
}jj 
catchkk 
(kk 
	Exceptionkk  
ekk! "
)kk" #
{ll 
logEnviomm 
.mm 
Errormm "
.mm" #
Addmm# &
(mm& '

documentosmm' 1
[mm1 2
imm2 3
]mm3 4
.mm4 5 
ComprobanteDocumentomm5 I
(mmI J
)mmJ K
+mmL M
$strmmN R
+mmS T
UtilmmU Y
.mmY Z
InicioErrorStringmmZ k
(mmk l
emml m
)mmm n
)mmn o
;mmo p
logEnvionn 
.nn 
LogErrornn %
.nn% &
Addnn& )
(nn) *

documentosnn* 4
[nn4 5
inn5 6
]nn6 7
.nn7 8 
ComprobanteDocumentonn8 L
(nnL M
)nnM N
+nnO P
$strnnQ U
+nnV W
UtilnnX \
.nn\ ]
SoloErrorStringnn] l
(nnl m
ennm n
)nnn o
)nno p
;nnp q
}oo 
}pp 
returnqq 
newqq 
OperationResultqq &
(qq& '
OperationResultEnumqq' :
.qq: ;
Successqq; B
,qqB C
$strqqD _
,qq_ `
$strqqa e
)qqe f
;qqf g
}rr 	
privatett 
OperationResulttt 
EnviarIndividualtt  0
(tt0 1 
DocumentoElectronicott1 E 
documentoElectronicottF Z
,ttZ [
FirmadoResponsett\ k
firmadoResponsettl {
,tt{ |
long	tt} �
idDocumento
tt� �
)
tt� �
{uu 	
IndicadorExcepcionvv 

logDeEnviovv )
=vv* +
newvv, /
IndicadorExcepcionvv0 B
(vvB C
$strvvC E
)vvE F
;vvF G
OperationResultww 
resultadoCrearEnvioww /
=ww0 1
newww2 5
OperationResultww6 E
(wwE F
)wwF G
;wwG H#
EnviarDocumentoResponsexx #"
envioDocumentoResponsexx$ :
;xx: ;
tryyy 
{zz 
try{{ 
{|| 
resultadoCrearEnvio}} '
=}}( )

CrearEnvio}}* 4
(}}4 5
$str}}5 7
,}}7 8*
FacturacionElectronicaSettings}}9 W
.}}W X
Default}}X _
.}}_ `
TipoEnvioIndividual}}` s
,}}s t
(}}u v
int}}v y
)}}y z
EstadoEnvio	}}z �
.
}}� �
	Pendiente
}}� �
,
}}� �
$str
}}� �
,
}}� �
$str
}}� �
,
}}� �
$str
}}� �
,
}}� �
Encoding
}}� �
.
}}� �
UTF8
}}� �
.
}}� �
GetBytes
}}� �
(
}}� �
JsonConvert
}}� �
.
}}� �
SerializeObject
}}� �
(
}}� �
firmadoResponse
}}� �
)
}}� �
)
}}� �
,
}}� �
null
}}� �
,
}}� �
	ModoEnvio
}}� �
.
}}� �
Ninguno
}}� �
)
}}� �
;
}}� �
Util~~ 
.~~ +
ManejoEnLogicaResultadoSinExito~~ 8
(~~8 9
resultadoCrearEnvio~~9 L
,~~L M
$str~~N |
+~~} ~
idDocumento	~~ �
)
~~� �
;
~~� �
} 
catch
�� 
(
�� 
	Exception
��  
)
��  !
{
�� 
return
�� 
new
�� 
OperationResult
�� .
(
��. /!
OperationResultEnum
��/ B
.
��B C
Information
��C N
,
��N O
$str
��P f
,
��f g
$str
��h o
)
��o p
;
��p q
}
�� 
var
�� *
resultadoCrearEnvioDocumento
�� 0
=
��1 2!
CrearEnvioDocumento
��3 F
(
��F G!
resultadoCrearEnvio
��G Z
.
��Z [
data
��[ _
,
��_ `
idDocumento
��a l
)
��l m
;
��m n
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 4
(
��4 5*
resultadoCrearEnvioDocumento
��5 Q
,
��Q R
$str��S �
+��� �$
documentoElectronico��� �
.��� �
IdDocumento��� �
)��� �
;��� �
var
�� 2
$resultadoActualizarDocumentoAEnviado
�� 8
=
��9 :)
ActualizarDocumentoAEnviado
��; V
(
��V W
idDocumento
��W b
)
��b c
;
��c d
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 4
(
��4 52
$resultadoActualizarDocumentoAEnviado
��5 Y
,
��Y Z
$str��[ �
+��� �$
documentoElectronico��� �
.��� �
IdDocumento��� �
+��� �
$str��� �
)��� �
;��� �$
envioDocumentoResponse
�� &
=
��' ($
ResolverEnvioDocumento
��) ?
(
��? @
firmadoResponse
��@ O
,
��O P"
documentoElectronico
��Q e
,
��e f!
resultadoCrearEnvio
��g z
.
��z {
data
��{ 
,�� �

logDeEnvio��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� $
envioDocumentoResponse
�� +
.
��+ ,
Exito
��, 1
)
��1 2
{
�� (
ActualizarEnvioDeDocumento
�� .
(
��. /$
envioDocumentoResponse
��/ E
,
��E F!
resultadoCrearEnvio
��G Z
.
��Z [
data
��[ _
)
��_ `
;
��` a
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str
��. V
+
��W X$
envioDocumentoResponse
��Y o
.
��o p
MensajeError
��p |
)
��| }
;
��} ~
}
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +!
OperationResultEnum
��+ >
.
��> ?
Success
��? F
,
��F G
(
��H I
string
��I O
.
��O P

��P ]
(
��] ^

logDeEnvio
��^ h
.
��h i
RegistroExcepcion
��i z
)
��z {
?
��| }
$str��~ �
:��� �
Environment��� �
.��� �
NewLine��� �
+��� �

logDeEnvio��� �
.��� �!
RegistroExcepcion��� �
)��� �
,��� �
$str��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
e
��# $
)
��$ %
{
�� 
envioDocumentoResponse
�� &
=
��' ($
ResolverEnvioDocumento
��) ?
(
��? @
firmadoResponse
��@ O
,
��O P"
documentoElectronico
��Q e
,
��e f!
resultadoCrearEnvio
��g z
.
��z {
data
��{ 
,�� �

logDeEnvio��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� $
envioDocumentoResponse
�� +
.
��+ ,
Exito
��, 1
)
��1 2
{
�� (
ActualizarEnvioDeDocumento
�� .
(
��. /$
envioDocumentoResponse
��/ E
,
��E F!
resultadoCrearEnvio
��G Z
.
��Z [
data
��[ _
)
��_ `
;
��` a
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str
��. V
+
��W X$
envioDocumentoResponse
��Y o
.
��o p
MensajeError
��p |
)
��| }
;
��} ~
}
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Q
,
��Q R
e
��S T
)
��T U
;
��U V
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Q
,
��Q R
e
��S T
)
��T U
;
��U V
}
�� 
}
�� 	
public
�� %
EnviarDocumentoResponse
�� &$
ResolverEnvioDocumento
��' =
(
��= >
FirmadoResponse
��> M
firmadoResponse
��N ]
,
��] ^"
DocumentoElectronico
��_ s#
documentoElectronico��t �
,��� �
long��� �
idEnvio��� �
,��� �"
IndicadorExcepcion��� �

logDeEnvio��� �
)��� �
{
�� 	%
EnviarDocumentoResponse
�� #$
envioDocumentoResponse
��$ :
=
��; <
EnviarDocumento
��= L
(
��L M
firmadoResponse
��M \
,
��\ ]"
documentoElectronico
��^ r
)
��r s
;
��s t
int
�� 
numeroIntentos
�� 
=
��  
$num
��! "
;
��" #
do
�� 
{
�� 
if
�� 
(
�� 
!
�� $
envioDocumentoResponse
�� +
.
��+ ,
Exito
��, 1
)
��1 2
{
�� 

logDeEnvio
�� 
.
�� 
RegistroExcepcion
�� 0
=
��1 2
Environment
��3 >
.
��> ?
NewLine
��? F
+
��G H$
envioDocumentoResponse
��I _
.
��_ `
MensajeError
��` l
;
��l m
Thread
�� 
.
�� 
Sleep
��  
(
��  !,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G H6
(TiempoEsperaParaConsultarIterativasEnvio
��H p
)
��p q
;
��q r$
envioDocumentoResponse
�� *
=
��+ ,
EnviarDocumento
��- <
(
��< =
firmadoResponse
��= L
,
��L M"
documentoElectronico
��N b
)
��b c
;
��c d
}
�� 
if
�� 
(
�� $
envioDocumentoResponse
�� *
.
��* +
Exito
��+ 0
)
��0 1
{
�� 
if
�� 
(
�� #
CodigoFESunatSettings
�� -
.
��- .
Default
��. 5
.
��5 6
C1033
��6 ;
.
��; <
Equals
��< B
(
��B C$
envioDocumentoResponse
��C Y
.
��Y Z
CodigoRespuesta
��Z i
)
��i j
)
��j k
{
�� $
envioDocumentoResponse
�� .
=
��/ 0*
ConsultarConstanciaDocumento
��1 M
(
��M N"
documentoElectronico
��N b
)
��b c
;
��c d
}
�� (
ActualizarEnvioDeDocumento
�� .
(
��. /$
envioDocumentoResponse
��/ E
,
��E F
idEnvio
��G N
)
��N O
;
��O P
}
�� 
numeroIntentos
�� 
++
��  
;
��  !
}
�� 
while
�� 
(
�� 
!
�� $
envioDocumentoResponse
�� ,
.
��, -
Exito
��- 2
&&
��3 5
numeroIntentos
��6 D
<
��E F,
FacturacionElectronicaSettings
��G e
.
��e f
Default
��f m
.
��m n7
(NumeroIntentosConsultaCDREnvioIndividual��n �
)��� �
;��� �
return
�� $
envioDocumentoResponse
�� )
;
��) *
}
�� 	
public
�� %
EnviarDocumentoResponse
�� &
EnviarDocumento
��' 6
(
��6 7
FirmadoResponse
��7 F
firmadoResponse
��G V
,
��V W"
DocumentoElectronico
��X l#
documentoElectronico��m �
)��� �
{
�� 	
var
�� 
documentoRequest
��  
=
��! "
new
��# &$
EnviarDocumentoRequest
��' =
{
�� 
Ruc
�� 
=
�� "
documentoElectronico
�� *
.
��* +
Emisor
��+ 1
.
��1 2
NroDocumento
��2 >
,
��> ?

UsuarioSol
�� 
=
�� ,
FacturacionElectronicaSettings
�� ;
.
��; <
Default
��< C
.
��C D

UsuarioSol
��D N
,
��N O
ClaveSol
�� 
=
�� ,
FacturacionElectronicaSettings
�� 9
.
��9 :
Default
��: A
.
��A B
ClaveSol
��B J
,
��J K
EndPointUrl
�� 
=
�� 9
+DevolverUrlEnvioSunatFacturacionElectronica
�� I
(
��I J
)
��J K
,
��K L
IdDocumento
�� 
=
�� "
documentoElectronico
�� 2
.
��2 3
IdDocumento
��3 >
,
��> ?

�� 
=
�� "
documentoElectronico
��  4
.
��4 5

��5 B
,
��B C
TramaXmlFirmado
�� 
=
��  !
firmadoResponse
��" 1
.
��1 2
TramaXmlFirmado
��2 A
}
�� 
;
��
return
�� 

RestHelper
�� 
<
�� $
EnviarDocumentoRequest
�� 4
,
��4 5%
EnviarDocumentoResponse
��6 M
>
��M N
.
��N O
Execute
��O V
(
��V W
$str
��W l
,
��l m
documentoRequest
��n ~
,
��~ .
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
}
�� 	
public
�� %
EnviarDocumentoResponse
�� &*
ConsultarConstanciaDocumento
��' C
(
��C D"
DocumentoElectronico
��D X
	documento
��Y b
)
��b c
{
�� 	
try
�� 
{
�� 
var
�� '
consultaConstanciaRequest
�� -
=
��. /
new
��0 3'
ConsultaConstanciaRequest
��4 M
{
�� 
Ruc
�� 
=
�� 
	documento
�� #
.
��# $
Emisor
��$ *
.
��* +
NroDocumento
��+ 7
,
��7 8

UsuarioSol
�� 
=
��  ,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G H

UsuarioSol
��H R
,
��R S
ClaveSol
�� 
=
�� ,
FacturacionElectronicaSettings
�� =
.
��= >
Default
��> E
.
��E F
ClaveSol
��F N
,
��N O
EndPointUrl
�� 
=
��  !,
FacturacionElectronicaSettings
��" @
.
��@ A
Default
��A H
.
��H I1
#URLWebServiceSunatConsultaDocumento
��I l
,
��l m
IdDocumento
�� 
=
��  !
	documento
��" +
.
��+ ,
IdDocumento
��, 7
,
��7 8

�� !
=
��" #
	documento
��$ -
.
��- .

��. ;
,
��; <
Serie
�� 
=
�� 
	documento
�� %
.
��% &
IdDocumento
��& 1
.
��1 2
Split
��2 7
(
��7 8
$char
��8 ;
)
��; <
[
��< =
$num
��= >
]
��> ?
,
��? @
Numero
�� 
=
�� 
Convert
�� $
.
��$ %
ToInt32
��% ,
(
��, -
	documento
��- 6
.
��6 7
IdDocumento
��7 B
.
��B C
Split
��C H
(
��H I
$char
��I L
)
��L M
[
��M N
$num
��N O
]
��O P
)
��P Q
}
�� 
;
�� 
var
�� 
respuestaConsulta
�� %
=
��& '

RestHelper
��( 2
<
��2 3'
ConsultaConstanciaRequest
��3 L
,
��L M%
EnviarDocumentoResponse
��N e
>
��e f
.
��f g
Execute
��g n
(
��n o
$str��o �
,��� �)
consultaConstanciaRequest��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� 
respuestaConsulta
�� &
.
��& '
Exito
��' ,
)
��, -
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str
��. ^
+
��_ `
respuestaConsulta
��a r
.
��r s
MensajeError
��s 
)�� �
;��� �
}
�� 
return
�� 
respuestaConsulta
�� (
;
��( )
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Y
,
��Y Z
e
��[ \
)
��\ ]
;
��] ^
}
�� 
}
�� 	
public
�� 
OperationResult
�� (
ActualizarEnvioDeDocumento
�� 9
(
��9 :%
EnviarDocumentoResponse
��: Q$
envioDocumentoResponse
��R h
,
��h i
long
��j n
idEnvio
��o v
)
��v w
{
�� 	
try
�� 
{
�� 
Envio
�� 
envioAActualizar
�� &
=
��' (%
GenerarEnvioAActualizar
��) @
(
��@ A$
envioDocumentoResponse
��A W
,
��W X
idEnvio
��Y `
)
��` a
;
��a b
var
�� &
resultadoActualizarEnvio
�� ,
=
��- .
_facturacionDatos
��/ @
.
��@ A
ActualizarEnvio
��A P
(
��P Q
envioAActualizar
��Q a
)
��a b
;
��b c
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 4
(
��4 5&
resultadoActualizarEnvio
��5 M
,
��M N
$str
��O s
)
��s t
;
��t u
return
�� 
new
�� 
OperationResult
�� *
(
��* +!
OperationResultEnum
��+ >
.
��> ?
Success
��? F
,
��F G
$str
��H c
,
��c d
$str
��e i
)
��i j
;
��j k
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* U
,
��U V
e
��W X
)
��X Y
;
��Y Z
}
�� 
}
�� 	
public
�� 
Envio
�� %
GenerarEnvioAActualizar
�� ,
(
��, -%
EnviarDocumentoResponse
��- D$
envioDocumentoResponse
��E [
,
��[ \
long
��] a
idEnvio
��b i
)
��i j
{
�� 	
try
�� 
{
�� 
OperationResult
�� #
resultadoCrearBinario
��  5
=
��6 7
null
��8 <
;
��< =
if
�� 
(
�� $
envioDocumentoResponse
�� *
.
��* +
Exito
��+ 0
)
��0 1
{
�� #
resultadoCrearBinario
�� )
=
��* +
CrearBinario
��, 8
(
��8 9
Encoding
��9 A
.
��A B
UTF8
��B F
.
��F G
GetBytes
��G O
(
��O P
JsonConvert
��P [
.
��[ \
SerializeObject
��\ k
(
��k l%
envioDocumentoResponse��l �
)��� �
)��� �
)��� �
;��� �
}
�� 
var
�� 
envioAActualizar
�� $
=
��% &
new
��' *
Envio
��+ 0
{
�� 
id
�� 
=
�� 
idEnvio
��  
,
��  !
estado
�� 
=
�� $
envioDocumentoResponse
�� 3
.
��3 4
Exito
��4 9
?
��: ;%
DeterminarEstadoDeEnvio
��< S
(
��S T$
envioDocumentoResponse
��T j
)
��j k
:
��l m
(
��n o
int
��o r
)
��r s
EstadoEnvio
��s ~
.
��~ 
	Pendiente�� �
,��� �
codigoRespuesta
�� #
=
��$ %$
envioDocumentoResponse
��& <
.
��< =
CodigoRespuesta
��= L
,
��L M
observacion
�� 
=
��  !$
envioDocumentoResponse
��" 8
.
��8 9
Exito
��9 >
?
��? @$
envioDocumentoResponse
��A W
.
��W X
MensajeRespuesta
��X h
+
��i j
$str
��k n
+
��o p
(
��q r
string
��r x
.
��x y

(��� �&
envioDocumentoResponse��� �
.��� �
MensajeError��� �
)��� �
?��� �
$str��� �
:��� �&
envioDocumentoResponse��� �
.��� �
MensajeError��� �
)��� �
:��� �&
envioDocumentoResponse��� �
.��� �
MensajeError��� �
,��� � 
idBinarioRespuesta
�� &
=
��' (
null
��) -
}
�� 
;
�� 
envioAActualizar
��  
.
��  ! 
idBinarioRespuesta
��! 3
=
��4 5$
envioDocumentoResponse
��6 L
.
��L M
Exito
��M R
?
��S T#
resultadoCrearBinario
��U j
.
��j k
data
��k o
:
��p q
envioAActualizar��r �
.��� �"
idBinarioRespuesta��� �
;��� �
return
�� 
envioAActualizar
�� '
;
��' (
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* U
,
��U V
e
��W X
)
��X Y
;
��Y Z
}
�� 
}
�� 	
public
�� 
int
�� %
DeterminarEstadoDeEnvio
�� *
(
��* +%
EnviarDocumentoResponse
��+ B%
enviarDocumentoResponse
��C Z
)
��Z [
{
�� 	
int
�� 
estadoEnvio
�� 
;
�� 
if
�� 
(
�� %
enviarDocumentoResponse
�� '
.
��' (
Exito
��( -
&&
��. 0
!
��1 2
string
��2 8
.
��8 9

��9 F
(
��F G%
enviarDocumentoResponse
��G ^
.
��^ _
TramaZipCdr
��_ j
)
��j k
)
��k l
{
�� 
var
�� 
codigoDeRespuesta
�� %
=
��& '
Convert
��( /
.
��/ 0
ToInt32
��0 7
(
��7 8%
enviarDocumentoResponse
��8 O
.
��O P
CodigoRespuesta
��P _
)
��_ `
;
��` a
estadoEnvio
�� 
=
�� 
(
�� 
codigoDeRespuesta
�� 0
==
��1 3,
FacturacionElectronicaSettings
��4 R
.
��R S
Default
��S Z
.
��Z [%
CodigoRespuestaAceptado
��[ r
)
��r s
?
��t u
(
�� 
int
�� 
)
�� 
EstadoEnvio
�� $
.
��$ %
Aceptado
��% -
:
��. /
(
�� 
codigoDeRespuesta
�� *
<=
��+ -,
FacturacionElectronicaSettings
��. L
.
��L M
Default
��M T
.
��T U/
!MaximoCodigoRespuestaConExcepcion
��U v
&&
��w y
codigoDeRespuesta
�� )
>=
��* ,,
FacturacionElectronicaSettings
��- K
.
��K L
Default
��L S
.
��S T/
!MinimoCodigoRespuestaConExcepcion
��T u
)
��u v
?
��w x
(
�� 
int
��  
)
��  !
EstadoEnvio
��! ,
.
��, -
ConExcepcion
��- 9
:
��: ;
(
�� 
codigoDeRespuesta
�� .
<=
��/ 1,
FacturacionElectronicaSettings
��2 P
.
��P Q
Default
��Q X
.
��X Y,
MaximoCodigoRespuestaRechazado
��Y w
&&
�� 
codigoDeRespuesta
�� 0
>=
��1 3,
FacturacionElectronicaSettings
��4 R
.
��R S
Default
��S Z
.
��Z [,
MinimoCodigoRespuestaRechazado
��[ y
)
��y z
?
��{ |
(
��  !
int
��! $
)
��$ %
EstadoEnvio
��% 0
.
��0 1
	Rechazado
��1 :
:
��; <
(
��$ %
codigoDeRespuesta
��% 6
>=
��7 9,
FacturacionElectronicaSettings
��: X
.
��X Y
Default
��Y `
.
��` a2
#MinimoCodigoRespuestaConObservacion��a �
)��� �
?��� �
(
��( )
int
��) ,
)
��, -
EstadoEnvio
��- 8
.
��8 9&
AceptadoConObservaciones
��9 Q
:
��R S
(
��( )
int
��) ,
)
��, -
EstadoEnvio
��- 8
.
��8 9
ConExcepcion
��9 E
;
��E F
if
�� 
(
�� 
estadoEnvio
�� 
!=
��  "
(
��# $
int
��$ '
)
��' (
EstadoEnvio
��( 3
.
��3 4
Aceptado
��4 <
)
��< =
estadoEnvio
�� 
=
��  !
Diccionario
��" -
.
��- .7
)CodigoFESunatConsideradosAceptadosEnSiges
��. W
.
��W X
Contains
��X `
(
��` a%
enviarDocumentoResponse
��a x
.
��x y
CodigoRespuesta��y �
)��� �
?��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �
Aceptado��� �
:��� �
estadoEnvio��� �
;��� �
}
�� 
else
�� 
{
�� 
estadoEnvio
�� 
=
�� 
(
�� 
int
�� "
)
��" #
EstadoEnvio
��# .
.
��. /
ConExcepcion
��/ ;
;
��; <
}
�� 
if
�� 
(
�� 
estadoEnvio
�� 
!=
�� 
(
��  
int
��  #
)
��# $
EstadoEnvio
��$ /
.
��/ 0
Aceptado
��0 8
)
��8 9
{
�� 
EstablecimientoComercial
�� (
sede
��) -
=
��. /
_sedeLogica
��0 ;
.
��; <
ObtenerSede
��< G
(
��G H
)
��H I
;
��I J
_mailer
�� 
.
�� 
	SendEmail
�� !
(
��! " 
AplicacionSettings
��" 4
.
��4 5
Default
��5 <
.
��< =3
%CorreoParaNotificacionDeErrorHangfire
��= b
,
��b c
$str��d �
+��� �
$str��� �
+��� �
sede��� �
.��� �"
DocumentoIdentidad��� �
+��� �
$str��� �
+��� �
sede��� �
.��� �
Nombre��� �
+��� �
$str��� �
+��� �
sede��� �
.��� �
NombreComercial��� �
+��� �
$str��� �
,��� �
$str��� �
+��� �
	Enumerado��� �
.��� �
GetDescription��� �
(��� �
(��� �
EstadoEnvio��� �
)��� �
estadoEnvio��� �
)��� �
+��� �
$str��� �
+��� �
sede��� �
.��� �
Nombre��� �
)��� �
;��� �
}
�� 
return
�� 
estadoEnvio
�� 
;
�� 
}
�� 	
public
�� 
FirmadoResponse
�� 
FirmarDocumento
�� .
(
��. /
DocumentoResponse
��/ @
documentoResponse
��A R
,
��R S
byte
��T X
[
��X Y
]
��Y Z 
archivoCertificado
��[ m
)
��m n
{
�� 	
try
�� 
{
�� 
var
�� 
firmado
�� 
=
�� 
new
�� !
FirmadoRequest
��" 0
{
�� 
TramaXmlSinFirma
�� $
=
��% &
documentoResponse
��' 8
.
��8 9
TramaXmlSinFirma
��9 I
,
��I J 
CertificadoDigital
�� &
=
��' (
Convert
��) 0
.
��0 1
ToBase64String
��1 ?
(
��? @ 
archivoCertificado
��@ R
)
��R S
,
��S T!
PasswordCertificado
�� '
=
��( ),
FacturacionElectronicaSettings
��* H
.
��H I
Default
��I P
.
��P Q%
ClaveCertificadoDigital
��Q h
,
��h i
}
�� 
;
�� 
var
�� 
respuestaFirmado
�� $
=
��% &

RestHelper
��' 1
<
��1 2
FirmadoRequest
��2 @
,
��@ A
FirmadoResponse
��B Q
>
��Q R
.
��R S
Execute
��S Z
(
��Z [
$str
��[ g
,
��g h
firmado
��i p
,
��p q-
FacturacionElectronicaSettings��r �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� 
respuestaFirmado
�� %
.
��% &
Exito
��& +
)
��+ ,
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
respuestaFirmado
��. >
.
��> ?
MensajeError
��? K
)
��K L
;
��L M
}
�� 
return
�� 
respuestaFirmado
�� '
;
��' (
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* T
,
��T U
e
��V W
)
��W X
;
��X Y
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
EnvioBoletas
�� +
(
��+ ,
LogEnvio
��, 4
logEnvio
��5 =
)
��= >
{
�� 	
try
�� 
{
�� 
!EstablecimientoComercialExtendido
�� 1
sede
��2 6
=
��7 8
_sedeLogica
��9 D
.
��D E"
ObtenerSedeExtendida
��E Y
(
��Y Z
)
��Z [
;
��[ \
	Documento
�� 
[
�� 
]
�� 
documentosPorDia
�� ,
;
��, -
var
�� #
noHayBoletasPorEnviar
�� )
=
��* +;
-DevolverBoletasIncluidoBinarioPorEnviarPorDia
��, Y
(
��Y Z
)
��Z [
.
��[ \
Count
��\ a
==
��b d
$num
��e f
;
��f g
do
�� 
{
�� 
documentosPorDia
�� $
=
��% &;
-DevolverBoletasIncluidoBinarioPorEnviarPorDia
��' T
(
��T U
)
��U V
.
��V W
OrderBy
��W ^
(
��^ _
d
��_ `
=>
��a c
d
��d e
.
��e f

idSigescom
��f p
)
��p q
.
��q r
ToArray
��r y
(
��y z
)
��z {
;
��{ |
if
�� 
(
�� 
documentosPorDia
�� (
.
��( )
Length
��) /
>
��0 1
$num
��2 3
)
��3 4
{
�� 
var
�� !
resultResumenDiario
�� /
=
��0 1)
ResolverResumenDiarioPorDia
��2 M
(
��M N
sede
��N R
,
��R S
documentosPorDia
��T d
,
��d e
logEnvio
��f n
)
��n o
;
��o p
}
�� 
}
�� 
while
�� 
(
�� 
documentosPorDia
�� '
.
��' (
Length
��( .
>
��/ 0
$num
��1 2
)
��2 3
;
��3 4
	Documento
�� 
[
�� 
]
�� #
documentosInvalidados
�� 1
=
��2 3S
EObtenerBoletasInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente
��4 y
(
��y z
)
��z {
.
��{ |
ToArray��| �
(��� �
)��� �
;��� �
if
�� 
(
�� #
documentosInvalidados
�� )
.
��) *
Length
��* 0
>
��1 2
$num
��3 4
)
��4 5
{
�� 
ConsultarTickets
�� $
(
��$ %
)
��% &
;
��& '
List
�� 
<
�� 
DateTime
�� !
>
��! "&
fechasDeEmisionDistintas
��# ;
=
��< =#
documentosInvalidados
��> S
.
��S T
Select
��T Z
(
��Z [
d
��[ \
=>
��] _
d
��` a
.
��a b
fechaEmision
��b n
.
��n o
Date
��o s
)
��s t
.
��t u
Distinct
��u }
(
��} ~
)
��~ 
.�� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (&
fechasDeEmisionDistintas
��) A
)
��A B
{
�� 
try
�� 
{
�� 
var
�� 
resultInvalidados
��  1
=
��2 3#
ResolverResumenDiario
��4 I
(
��I J
sede
��J N
,
��N O#
documentosInvalidados
��P e
.
��e f
Where
��f k
(
��k l
d
��l m
=>
��n p
d
��q r
.
��r s
fechaEmision
��s 
.�� �
Date��� �
==��� �
item��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
,��� �
false��� �
,��� �
$str��� �
,��� �
$str��� �
,��� �
	ModoEnvio��� �
.��� �
	Anulacion��� �
)��� �
;��� �
Util
��  
.
��  !-
ManejoEnLogicaResultadoSinExito
��! @
(
��@ A
resultInvalidados
��A R
,
��R S
$str��T �
)��� �
;��� �
foreach
�� #
(
��$ %
var
��% (
serie
��) .
in
��/ 1#
documentosInvalidados
��2 G
.
��G H
Select
��H N
(
��N O
ed
��O Q
=>
��R T
ed
��U W
.
��W X
serieComprobante
��X h
)
��h i
.
��i j
Distinct
��j r
(
��r s
)
��s t
.
��t u
ToList
��u {
(
��{ |
)
��| }
)
��} ~
{
�� 
logEnvio
��  (
.
��( )
Exito
��) .
.
��. /
Add
��/ 2
(
��2 3
	ItemEnvio
��3 <
.
��< =
ItemAnulado
��= H
(
��H I
$str
��I L
+
��M N
serie
��O T
+
��U V
$str
��W Z
+
��[ \#
documentosInvalidados
��] r
.
��r s
Where
��s x
(
��x y
s
��y z
=>
��{ }
s
��~ 
.�� � 
serieComprobante��� �
==��� �
serie��� �
)��� �
.��� �
OrderBy��� �
(��� �
d��� �
=>��� �
Convert��� �
.��� �
ToInt32��� �
(��� �
d��� �
.��� �!
numeroComprobante��� �
)��� �
)��� �
.��� �
FirstOrDefault��� �
(��� �
)��� �
.��� �!
numeroComprobante��� �
+��� �
$str��� �
+��� �%
documentosInvalidados��� �
.��� �
Where��� �
(��� �
s��� �
=>��� �
s��� �
.��� � 
serieComprobante��� �
==��� �
serie��� �
)��� �
.��� �!
OrderByDescending��� �
(��� �
d��� �
=>��� �
Convert��� �
.��� �
ToInt32��� �
(��� �
d��� �
.��� �!
numeroComprobante��� �
)��� �
)��� �
.��� �
FirstOrDefault��� �
(��� �
)��� �
.��� �!
numeroComprobante��� �
+��� �
$str��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� (
e
��) *
)
��* +
{
�� 
logEnvio
�� $
.
��$ %
Error
��% *
.
��* +
Add
��+ .
(
��. /
$str
��/ T
+
��U V
$str
��W \
+
��] ^
Util
��_ c
.
��c d
InicioErrorString
��d u
(
��u v
e
��v w
)
��w x
)
��x y
;
��y z
logEnvio
�� $
.
��$ %
LogError
��% -
.
��- .
Add
��. 1
(
��1 2
$str
��2 W
+
��X Y
$str
��Z _
+
��` a
Util
��b f
.
��f g
SoloErrorString
��g v
(
��v w
e
��w x
)
��x y
)
��y z
;
��z {
}
�� 
}
�� 
}
�� 
if
�� 
(
�� #
noHayBoletasPorEnviar
�� )
&&
��* ,#
documentosInvalidados
��- B
.
��B C
Length
��C I
==
��J L
$num
��M N
)
��N O
{
�� 
logEnvio
�� 
.
��  
LogNoHayDocumentos
�� /
(
��/ 0
true
��0 4
,
��4 5
$str
��6 Z
)
��Z [
;
��[ \
}
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +!
OperationResultEnum
��+ >
.
��> ?
Success
��? F
,
��F G
$str
��H c
,
��c d
$str
��e i
)
��i j
;
��j k
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* W
,
��W X
e
��Y Z
)
��Z [
;
��[ \
}
�� 
}
�� 	
public
�� 
OperationResult
�� )
ResolverResumenDiarioPorDia
�� :
(
��: ;/
!EstablecimientoComercialExtendido
��; \
sede
��] a
,
��a b
	Documento
��c l
[
��l m
]
��m n

documentos
��o y
,
��y z
LogEnvio��{ �
logEnvio��� �
)��� �
{
�� 	
try
�� 
{
�� 
int
�� 
tamanyoLote
�� 
=
��  !,
FacturacionElectronicaSettings
��" @
.
��@ A
Default
��A H
.
��H I&
TamanyoLoteResumenDiario
��I a
;
��a b
int
�� )
contadorResumenesProcesados
�� /
=
��0 1
$num
��2 3
;
��3 4
int
�� #
totalResumenesAEnviar
�� )
=
��* +
Convert
��, 3
.
��3 4
ToInt16
��4 ;
(
��; <
Math
��< @
.
��@ A
Ceiling
��A H
(
��H I
Convert
��I P
.
��P Q
	ToDecimal
��Q Z
(
��Z [
Convert
��[ b
.
��b c
	ToDecimal
��c l
(
��l m

documentos
��m w
.
��w x
Length
��x ~
)
��~ 
/��� �
Convert��� �
.��� �
	ToDecimal��� �
(��� �
tamanyoLote��� �
)��� �
)��� �
)��� �
)��� �
;��� �
while
�� 
(
�� )
contadorResumenesProcesados
�� 2
<
��3 4#
totalResumenesAEnviar
��5 J
)
��J K
{
�� )
contadorResumenesProcesados
�� /
++
��/ 1
;
��1 2
	Documento
�� 
[
�� 
]
�� 
documentosLote
��  .
=
��/ 0

documentos
��1 ;
.
��; <
Skip
��< @
(
��@ A
(
��A B)
contadorResumenesProcesados
��B ]
-
��^ _
$num
��` a
)
��a b
*
��c d
tamanyoLote
��e p
)
��p q
.
��q r
Take
��r v
(
��v w
tamanyoLote��w �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
var
�� 

�� %
=
��& '#
ResolverResumenDiario
��( =
(
��= >
sede
��> B
,
��B C
documentosLote
��D R
,
��R S
true
��T X
,
��X Y
$str��Z �
,��� �
$str��� �
,��� �
	ModoEnvio��� �
.��� �
Adicion��� �
)��� �
;��� �
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� 3
(
��3 4

��4 A
,
��A B
$str
��C q
)
��q r
;
��r s
foreach
�� 
(
�� 
var
��  
serie
��! &
in
��' )
documentosLote
��* 8
.
��8 9
Select
��9 ?
(
��? @
ed
��@ B
=>
��C E
ed
��F H
.
��H I
serieComprobante
��I Y
)
��Y Z
.
��Z [
Distinct
��[ c
(
��c d
)
��d e
.
��e f
ToList
��f l
(
��l m
)
��m n
)
��n o
{
�� 
logEnvio
��  
.
��  !
Exito
��! &
.
��& '
Add
��' *
(
��* +
	ItemEnvio
��+ 4
.
��4 5
ItemAdicionado
��5 C
(
��C D
$str
��D G
+
��H I
serie
��J O
+
��P Q
$str
��R U
+
��V W
documentosLote
��X f
.
��f g
Where
��g l
(
��l m
s
��m n
=>
��o q
s
��r s
.
��s t
serieComprobante��t �
==��� �
serie��� �
)��� �
.��� �
OrderBy��� �
(��� �
d��� �
=>��� �
Convert��� �
.��� �
ToInt32��� �
(��� �
d��� �
.��� �!
numeroComprobante��� �
)��� �
)��� �
.��� �
FirstOrDefault��� �
(��� �
)��� �
.��� �!
numeroComprobante��� �
+��� �
$str��� �
+��� �
documentosLote��� �
.��� �
Where��� �
(��� �
s��� �
=>��� �
s��� �
.��� � 
serieComprobante��� �
==��� �
serie��� �
)��� �
.��� �!
OrderByDescending��� �
(��� �
d��� �
=>��� �
Convert��� �
.��� �
ToInt32��� �
(��� �
d��� �
.��� �!
numeroComprobante��� �
)��� �
)��� �
.��� �
FirstOrDefault��� �
(��� �
)��� �
.��� �!
numeroComprobante��� �
+��� �
$str��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 

�� %
.
��% &
code_result
��& 1
==
��2 4!
OperationResultEnum
��5 H
.
��H I
Success
��I P
&&
��Q S
(
��T U
(
��U V
int
��V Y
)
��Y Z

��Z g
.
��g h
information
��h s
==
��t v
(
��w x
int
��x {
)
��{ |
EstadoEnvio��| �
.��� �
Aceptado��� �
||��� �
(��� �
int��� �
)��� �

.��� �
information��� �
==��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �(
AceptadoConObservaciones��� �
)��� �
)��� �
{
�� 
if
�� 
(
�� 
documentosLote
�� *
.
��* +
Any
��+ .
(
��. /
d
��/ 0
=>
��1 3
d
��4 5
.
��5 6
estadoSigescom
��6 D
==
��E G
(
��H I
int
��I L
)
��L M0
"EstadoSigescomDocumentoElectronico
��M o
.
��o p

Invalidado
��p z
)
��z {
)
��{ |
{
�� 
	Documento
�� %
[
��% &
]
��& '#
documentosInvalidados
��( =
=
��> ?
documentosLote
��@ N
.
��N O
Where
��O T
(
��T U
d
��U V
=>
��W Y
d
��Z [
.
��[ \
estadoSigescom
��\ j
==
��k m
(
��n o
int
��o r
)
��r s1
"EstadoSigescomDocumentoElectronico��s �
.��� �

Invalidado��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
var
�� %
resultResumenInvalidado
��  7
=
��8 9.
 ResolverResumenDiarioInvalidados
��: Z
(
��Z [
sede
��[ _
,
��_ `#
documentosInvalidados
��a v
)
��v w
;
��w x
Util
��  
.
��  !(
ManageIfResultIsNotSuccess
��! ;
(
��; <%
resultResumenInvalidado
��< S
,
��S T
$str��U �
)��� �
;��� �
logEnvio
�� $
.
��$ %
Exito
��% *
.
��* +
Add
��+ .
(
��. /
	ItemEnvio
��/ 8
.
��8 9
ItemAnulado
��9 D
(
��D E
string
��E K
.
��K L
Join
��L P
(
��P Q
$str
��Q U
,
��U V#
documentosInvalidados
��W l
.
��l m
Select
��m s
(
��s t
d
��t u
=>
��v x
d
��y z
.
��z {#
ComprobanteDocumento��{ �
(��� �
)��� �
)��� �
)��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
logEnvio
�� 
.
�� 
Error
�� 
.
�� 
Add
�� "
(
��" #
$str
��# H
+
��I J
$str
��K P
+
��Q R
Util
��S W
.
��W X
InicioErrorString
��X i
(
��i j
e
��j k
)
��k l
)
��l m
;
��m n
logEnvio
�� 
.
�� 
LogError
�� !
.
��! "
Add
��" %
(
��% &
$str
��& K
+
��L M
$str
��N S
+
��T U
Util
��V Z
.
��Z [
SoloErrorString
��[ j
(
��j k
e
��k l
)
��l m
)
��m n
;
��n o
}
�� 
return
�� 
new
�� 
OperationResult
�� &
(
��& '!
OperationResultEnum
��' :
.
��: ;
Success
��; B
,
��B C
$str
��D _
,
��_ `
$str
��a e
)
��e f
;
��f g
;
��h i
}
�� 	
public
�� 
OperationResult
�� .
 ResolverResumenDiarioInvalidados
�� ?
(
��? @/
!EstablecimientoComercialExtendido
��@ a
sede
��b f
,
��f g
	Documento
��h q
[
��q r
]
��r s$
documentosInvalidados��t �
)��� �
{
�� 	
try
�� 
{
�� 
int
�� 
tamanyoLote
�� 
=
��  !,
FacturacionElectronicaSettings
��" @
.
��@ A
Default
��A H
.
��H I&
TamanyoLoteResumenDiario
��I a
;
��a b
int
�� )
contadorResumenesProcesados
�� /
=
��0 1
$num
��2 3
;
��3 4
int
�� #
totalResumenesAEnviar
�� )
=
��* +
Convert
��, 3
.
��3 4
ToInt16
��4 ;
(
��; <
Math
��< @
.
��@ A
Ceiling
��A H
(
��H I
Convert
��I P
.
��P Q
	ToDecimal
��Q Z
(
��Z [
Convert
��[ b
.
��b c
	ToDecimal
��c l
(
��l m$
documentosInvalidados��m �
.��� �
Length��� �
)��� �
/��� �
Convert��� �
.��� �
	ToDecimal��� �
(��� �
tamanyoLote��� �
)��� �
)��� �
)��� �
)��� �
;��� �
while
�� 
(
�� )
contadorResumenesProcesados
�� 2
<
��3 4#
totalResumenesAEnviar
��5 J
)
��J K
{
�� )
contadorResumenesProcesados
�� /
++
��/ 1
;
��1 2
	Documento
�� 
[
�� 
]
�� 
documentosLote
��  .
=
��/ 0#
documentosInvalidados
��1 F
.
��F G
Skip
��G K
(
��K L
(
��L M)
contadorResumenesProcesados
��M h
-
��i j
$num
��k l
)
��l m
*
��n o
tamanyoLote
��p {
)
��{ |
.
��| }
Take��} �
(��� �
tamanyoLote��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
var
�� '
resultTicketDeInvalidados
�� 1
=
��2 3#
ResolverResumenDiario
��4 I
(
��I J
sede
��J N
,
��N O
documentosLote
��P ^
,
��^ _
false
��` e
,
��e f
$str��g �
,��� �
$str��� �
,��� �
	ModoEnvio��� �
.��� �
	Anulacion��� �
)��� �
;��� �
}
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +!
OperationResultEnum
��+ >
.
��> ?
Success
��? F
,
��F G
$str
��H c
,
��c d
$str
��e i
)
��i j
;
��j k
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* Z
,
��Z [
e
��\ ]
)
��] ^
;
��^ _
}
�� 
}
�� 	
public
�� 
OperationResult
�� #
ResolverResumenDiario
�� 4
(
��4 5/
!EstablecimientoComercialExtendido
��5 V
sede
��W [
,
��[ \
	Documento
��] f
[
��f g
]
��g h

documentos
��i s
,
��s t
bool
��u y4
%cambiarEstadoItemDeAnuladoAAdicionado��z �
,��� �
string��� �!
mensajeErrorEnvio��� �
,��� �
string��� �*
mensajeErrorConsultaTicket��� �
,��� �
	ModoEnvio��� �
	modoEnvio��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� $
documentoResumenDiario
�� *
=
��+ ,'
_generacionArchivosLogica
��- F
.
��F G"
ObtenerResumenDiario
��G [
(
��[ \

documentos
��\ f
,
��f g
sede
��h l
,
��l m4
%cambiarEstadoItemDeAnuladoAAdicionado��n �
)��� �
;��� �
var
�� ,
documentoResumenDiarioResponse
�� 2
=
��3 4

RestHelper
��5 ?
<
��? @ 
ResumenDiarioNuevo
��@ R
,
��R S
DocumentoResponse
��T e
>
��e f
.
��f g
Execute
��g n
(
��n o
$str��o �
,��� �&
documentoResumenDiario��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� ,
documentoResumenDiarioResponse
�� 3
.
��3 4
Exito
��4 9
)
��9 :
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .,
documentoResumenDiarioResponse
��. L
.
��L M
MensajeError
��M Y
)
��Y Z
;
��Z [
}
�� 
else
�� 
{
�� 
var
��  
archivoCertificado
�� *
=
��+ , 
ObtenerCertificado
��- ?
(
��? @$
documentoResumenDiario
��@ V
.
��V W
Emisor
��W ]
.
��] ^
NroDocumento
��^ j
)
��j k
;
��k l
var
�� 
firmadoResponse
�� '
=
��( )
FirmarDocumento
��* 9
(
��9 :,
documentoResumenDiarioResponse
��: X
,
��X Y 
archivoCertificado
��Z l
)
��l m
;
��m n
var
�� 
resultadoEnvio
�� &
=
��' (!
EnviarResumenDiario
��) <
(
��< =$
documentoResumenDiario
��= S
,
��S T
firmadoResponse
��U d
,
��d e

documentos
��f p
.
��p q
Select
��q w
(
��w x
d
��x y
=>
��z |
d
��} ~
.
��~ 
id�� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
,��� �
	modoEnvio��� �
)��� �
;��� �
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 8
(
��8 9
resultadoEnvio
��9 G
,
��G H
mensajeErrorEnvio
��I Z
)
��Z [
;
��[ \
Thread
�� 
.
�� 
Sleep
��  
(
��  !,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G H3
%TiempoEsperaParaConsultarEnvioResumen
��H m
)
��m n
;
��n o
var
�� 
resultadoTicket
�� '
=
��( )
ConsultarTicket
��* 9
(
��9 :
sede
��: >
.
��> ? 
DocumentoIdentidad
��? Q
,
��Q R
new
��S V
EnvioSimplificado
��W h
(
��h i
)
��i j
{
��k l
Id
��m o
=
��p q
resultadoEnvio��r �
.��� �
data��� �
,��� �
NumeroTicket��� �
=��� �
(��� �
string��� �
)��� �
resultadoEnvio��� �
.��� �
information��� �
}��� �
)��� �
;��� �
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 8
(
��8 9
resultadoTicket
��9 H
,
��H I(
mensajeErrorConsultaTicket
��J d
)
��d e
;
��e f
return
�� 
new
�� 
OperationResult
�� .
(
��. /!
OperationResultEnum
��/ B
.
��B C
Success
��C J
,
��J K
$str
��L g
,
��g h
$str
��i m
)
��m n
{
�� 
information
�� #
=
��$ %
resultadoTicket
��& 5
.
��5 6
information
��6 A
}
�� 
;
�� 
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* j
,
��j k
e
��l m
)
��m n
;
��n o
}
�� 
}
�� 	
public
�� 
OperationResult
�� !
EnviarResumenDiario
�� 2
(
��2 3 
ResumenDiarioNuevo
��3 E 
resumenDiarioNuevo
��F X
,
��X Y
FirmadoResponse
��Z i
firmadoResponse
��j y
,
��y z
long
��{ 
[�� �
]��� �
idDocumentos��� �
,��� �
	ModoEnvio��� �
	modoEnvio��� �
)��� �
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
resultCrearEnvio
��  0
=
��1 2
null
��3 7
;
��7 8
try
�� 
{
�� 
resultCrearEnvio
�� $
=
��% &

CrearEnvio
��' 1
(
��1 2 
resumenDiarioNuevo
��2 D
.
��D E
IdDocumento
��E P
.
��P Q
Split
��Q V
(
��V W
$char
��W Z
)
��Z [
[
��[ \
$num
��\ ]
]
��] ^
,
��^ _,
FacturacionElectronicaSettings
��` ~
.
��~ 
Default�� �
.��� �&
TipoEnvioResumenDiario��� �
,��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �
	Pendiente��� �
,��� �
$str��� �
,��� �
$str��� �
,��� �
$str��� �
,��� �
Encoding��� �
.��� �
UTF8��� �
.��� �
GetBytes��� �
(��� �
JsonConvert��� �
.��� �
SerializeObject��� �
(��� �
firmadoResponse��� �
)��� �
)��� �
,��� �
null��� �
,��� �
	modoEnvio��� �
)��� �
;��� �
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 8
(
��8 9
resultCrearEnvio
��9 I
,
��I J
$str
��K ~
+�� �"
resumenDiarioNuevo��� �
.��� �
IdDocumento��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
��  
)
��  !
{
�� 
return
�� 
new
�� 
OperationResult
�� .
(
��. /!
OperationResultEnum
��/ B
.
��B C
Information
��C N
,
��N O
$str
��P f
,
��f g
$str
��h o
)
��o p
;
��p q
}
�� 
var
�� -
resultCrearEnvioDocumentoMasivo
�� 3
=
��4 5'
CrearEnvioDocumentoMasivo
��6 O
(
��O P
resultCrearEnvio
��P `
.
��` a
data
��a e
,
��e f
idDocumentos
��g s
)
��s t
;
��t u
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 4
(
��4 5-
resultCrearEnvioDocumentoMasivo
��5 T
,
��T U
$str��V �
)��� �
;��� �
var
�� 1
#resultActualizarDocumentosAEnviados
�� 7
=
��8 9+
ActualizarDocumentosAEnviados
��: W
(
��W X
idDocumentos
��X d
)
��d e
;
��e f
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 4
(
��4 51
#resultActualizarDocumentosAEnviados
��5 X
,
��X Y
$str��Z �
)��� �
;��� �
var
�� $
enviarDocumentoRequest
�� *
=
��+ ,
new
��- 0$
EnviarDocumentoRequest
��1 G
{
�� 
Ruc
�� 
=
��  
resumenDiarioNuevo
�� ,
.
��, -
Emisor
��- 3
.
��3 4
NroDocumento
��4 @
,
��@ A

UsuarioSol
�� 
=
��  ,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G H

UsuarioSol
��H R
,
��R S
ClaveSol
�� 
=
�� ,
FacturacionElectronicaSettings
�� =
.
��= >
Default
��> E
.
��E F
ClaveSol
��F N
,
��N O
EndPointUrl
�� 
=
��  !9
+DevolverUrlEnvioSunatFacturacionElectronica
��" M
(
��M N
)
��N O
,
��O P
IdDocumento
�� 
=
��  ! 
resumenDiarioNuevo
��" 4
.
��4 5
IdDocumento
��5 @
,
��@ A
TramaXmlFirmado
�� #
=
��$ %
firmadoResponse
��& 5
.
��5 6
TramaXmlFirmado
��6 E
}
�� 
;
�� 
var
�� #
enviarResumenResponse
�� )
=
��* +

RestHelper
��, 6
<
��6 7$
EnviarDocumentoRequest
��7 M
,
��M N#
EnviarResumenResponse
��O d
>
��d e
.
��e f
Execute
��f m
(
��m n
$str��n �
,��� �&
enviarDocumentoRequest��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� #
enviarResumenResponse
�� *
.
��* +
Exito
��+ 0
)
��0 1
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .#
enviarResumenResponse
��. C
.
��C D
MensajeError
��D P
)
��P Q
;
��Q R
}
�� 
var
�� 
envioAActualizar
�� $
=
��% &
new
��' *
Envio
��+ 0
{
�� 
id
�� 
=
�� 
resultCrearEnvio
�� )
.
��) *
data
��* .
,
��. /
estado
�� 
=
�� #
enviarResumenResponse
�� 2
.
��2 3
Exito
��3 8
?
��9 :
(
��; <
int
��< ?
)
��? @
EstadoEnvio
��@ K
.
��K L
	Pendiente
��L U
:
��V W
(
��X Y
int
��Y \
)
��\ ]
EstadoEnvio
��] h
.
��h i
	Rechazado
��i r
,
��r s
observacion
�� 
=
��  !
string
��" (
.
��( )

��) 6
(
��6 7#
enviarResumenResponse
��7 L
.
��L M
MensajeError
��M Y
)
��Y Z
?
��[ \
$str
��] _
:
��` a#
enviarResumenResponse
��b w
.
��w x
MensajeError��x �
,��� �
numeroTicket
��  
=
��! "#
enviarResumenResponse
��# 8
.
��8 9
	NroTicket
��9 B
}
�� 
;
�� 4
&ActualizarEstadoObservacionTicketEnvio
�� 6
(
��6 7
envioAActualizar
��7 G
)
��G H
;
��H I
OperationResult
�� 
result
��  &
=
��' (
new
��) ,
OperationResult
��- <
(
��< =!
OperationResultEnum
��= P
.
��P Q
Success
��Q X
,
��X Y
$str
��Z u
,
��u v
$str
��w {
)
��{ |
{
�� 
data
�� 
=
�� 
resultCrearEnvio
�� +
.
��+ ,
data
��, 0
,
��0 1
information
�� 
=
��  !#
enviarResumenResponse
��" 7
.
��7 8
	NroTicket
��8 A
}
�� 
;
�� 
return
�� 
result
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* J
,
��J K
e
��L M
)
��M N
;
��N O
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
EnvioNotasCredito
�� 0
(
��0 1
LogEnvio
��1 9
logEnvio
��: B
)
��B C
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' (
null
��) -
;
��- ./
!EstablecimientoComercialExtendido
�� 1
sede
��2 6
=
��7 8
_sedeLogica
��9 D
.
��D E"
ObtenerSedeExtendida
��E Y
(
��Y Z
)
��Z [
;
��[ \
	Documento
�� 
[
�� 
]
�� #
notasCreditoPorEnviar
�� 1
=
��2 3:
,DevolverNotasCreditoIncluidoBinarioPorEnviar
��4 `
(
��` a
)
��a b
.
��b c
ToArray
��c j
(
��j k
)
��k l
;
��l m
if
�� 
(
�� #
notasCreditoPorEnviar
�� )
.
��) *
Length
��* 0
>
��1 2
$num
��3 4
)
��4 5
{
�� 
result
�� 
=
��  
ResolverEnvioNotas
�� /
(
��/ 0#
notasCreditoPorEnviar
��0 E
,
��E F
logEnvio
��G O
)
��O P
;
��P Q
}
�� 
var
�� /
!documentosIndividualesInvalidados
�� 5
=
��6 7N
@ObtenerNotasCreditoInvalidadasAceptadasPeroSinComunicacionDeBaja
��8 x
(
��x y
)
��y z
.
��z {
ToArray��{ �
(��� �
)��� �
;��� �
if
�� 
(
�� /
!documentosIndividualesInvalidados
�� 5
.
��5 6
Count
��6 ;
(
��; <
)
��< =
>
��> ?
$num
��@ A
)
��A B
{
�� 
var
�� <
.resultadoResolverComunicacionBajaNoComunicadas
�� F
=
��G H&
ResolverComunicacionBaja
��I a
(
��a b
sede
��b f
,
��f g0
!documentosIndividualesInvalidados��h �
,��� �
logEnvio��� �
)��� �
;��� �
}
�� 
	Documento
�� 
[
�� 
]
�� #
documentosInvalidados
�� 1
=
��2 3X
JObtenerNotasCreditoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente
��4 ~
(
��~ 
)�� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
if
�� 
(
�� #
documentosInvalidados
�� )
.
��) *
Count
��* /
(
��/ 0
)
��0 1
>
��2 3
$num
��4 5
)
��5 6
{
�� 
	Documento
�� 
[
�� 
]
�� *
documentosBoletasInvalidados
��  <
=
��= >#
documentosInvalidados
��? T
.
��T U
Where
��U Z
(
��Z [
d
��[ \
=>
��] _
d
��` a
.
��a b
serieComprobante
��b r
.
��r s
	Substring
��s |
(
��| }
$num
��} ~
,
��~ 
$num��� �
)��� �
==��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �7
'PrefijoSerieNotaCreditoNotaDebitoBoleta��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
List
�� 
<
�� 
DateTime
�� !
>
��! "1
#fechasDeEmisionDistintasInvalidadas
��# F
=
��G H*
documentosBoletasInvalidados
��I e
.
��e f
Select
��f l
(
��l m
d
��m n
=>
��o q
d
��r s
.
��s t
fechaEmision��t �
.��� �
Date��� �
)��� �
.��� �
Distinct��� �
(��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (1
#fechasDeEmisionDistintasInvalidadas
��) L
)
��L M
{
�� 
try
�� 
{
�� 
var
�� 
documentosDelDia
��  0
=
��1 2*
documentosBoletasInvalidados
��3 O
.
��O P
Where
��P U
(
��U V
d
��V W
=>
��X Z
d
��[ \
.
��\ ]
fechaEmision
��] i
.
��i j
Date
��j n
==
��o q
item
��r v
)
��v w
.
��w x
ToArray
��x 
(�� �
)��� �
;��� �
var
�� "
resultadoInvalidados
��  4
=
��5 6#
ResolverResumenDiario
��7 L
(
��L M
sede
��M Q
,
��Q R
documentosDelDia
��S c
,
��c d
false
��e j
,
��j k
$str��l �
,��� �
$str��� �
,��� �
	ModoEnvio��� �
.��� �
	Anulacion��� �
)��� �
;��� �
Util
��  
.
��  !-
ManejoEnLogicaResultadoSinExito
��! @
(
��@ A"
resultadoInvalidados
��A U
,
��U V
$str��W �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� (
e
��) *
)
��* +
{
�� 
logEnvio
�� $
.
��$ %
Error
��% *
.
��* +
Add
��+ .
(
��. /
string
��/ 5
.
��5 6
Join
��6 :
(
��: ;
$str
��; ?
,
��? @*
documentosBoletasInvalidados
��A ]
.
��] ^
Where
��^ c
(
��c d
d
��d e
=>
��f h
d
��i j
.
��j k
fechaEmision
��k w
.
��w x
Date
��x |
==
��} 
item��� �
)��� �
)��� �
+��� �
$str��� �
+��� �
Util��� �
.��� �!
InicioErrorString��� �
(��� �
e��� �
)��� �
)��� �
;��� �
logEnvio
�� $
.
��$ %
LogError
��% -
.
��- .
Add
��. 1
(
��1 2
string
��2 8
.
��8 9
Join
��9 =
(
��= >
$str
��> B
,
��B C*
documentosBoletasInvalidados
��D `
.
��` a
Where
��a f
(
��f g
d
��g h
=>
��i k
d
��l m
.
��m n
fechaEmision
��n z
.
��z {
Date
��{ 
==��� �
item��� �
)��� �
)��� �
+��� �
$str��� �
+��� �
Util��� �
.��� �
SoloErrorString��� �
(��� �
e��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
}
�� 
if
�� 
(
�� #
notasCreditoPorEnviar
�� )
.
��) *
Length
��* 0
==
��1 3
$num
��4 5
&&
��6 8/
!documentosIndividualesInvalidados
��9 Z
.
��Z [
Count
��[ `
(
��` a
)
��a b
==
��c e
$num
��f g
&&
��h j$
documentosInvalidados��k �
.��� �
Count��� �
(��� �
)��� �
==��� �
$num��� �
)��� �
{
�� 
logEnvio
�� 
.
��  
LogNoHayDocumentos
�� /
(
��/ 0
true
��0 4
,
��4 5
$str
��6 Z
)
��Z [
;
��[ \
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
logEnvio
�� 
.
�� 
Error
�� 
.
�� 
Add
�� "
(
��" #
$str
��# N
+
��O P
$str
��Q V
+
��W X
Util
��Y ]
.
��] ^
InicioErrorString
��^ o
(
��o p
e
��p q
)
��q r
)
��r s
;
��s t
logEnvio
�� 
.
�� 
LogError
�� !
.
��! "
Add
��" %
(
��% &
$str
��& Q
+
��R S
$str
��T Y
+
��Z [
Util
��\ `
.
��` a
SoloErrorString
��a p
(
��p q
e
��q r
)
��r s
)
��s t
;
��t u
}
�� 
return
�� 
new
�� 
OperationResult
�� &
(
��& '!
OperationResultEnum
��' :
.
��: ;
Success
��; B
,
��B C
$str
��D _
,
��_ `
$str
��a e
)
��e f
;
��f g
}
�� 	
public
�� 
OperationResult
�� 
EnvioNotasDebito
�� /
(
��/ 0
LogEnvio
��0 8
logEnvio
��9 A
)
��A B
{
�� 	
try
�� 
{
�� 
!EstablecimientoComercialExtendido
�� 1
sede
��2 6
=
��7 8
_sedeLogica
��9 D
.
��D E"
ObtenerSedeExtendida
��E Y
(
��Y Z
)
��Z [
;
��[ \
	Documento
�� 
[
�� 
]
�� "
notasDebitoPorEnviar
�� 0
=
��1 29
+DevolverNotasDebitoIncluidoBinarioPorEnviar
��3 ^
(
��^ _
)
��_ `
.
��` a
ToArray
��a h
(
��h i
)
��i j
;
��j k
if
�� 
(
�� "
notasDebitoPorEnviar
�� (
.
��( )
Length
��) /
>
��0 1
$num
��2 3
)
��3 4
{
��  
ResolverEnvioNotas
�� &
(
��& '"
notasDebitoPorEnviar
��' ;
,
��; <
logEnvio
��= E
)
��E F
;
��F G
}
�� 
	Documento
�� 
[
�� 
]
�� 2
$documentosInvalidadosEnvioIndividual
�� @
=
��A BN
?ObtenerNotasDebitoInvalidadasAceptadasPeroSinComunicacionDeBaja��C �
(��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
if
�� 
(
�� 2
$documentosInvalidadosEnvioIndividual
�� 8
.
��8 9
Count
��9 >
(
��> ?
)
��? @
>
��A B
$num
��C D
)
��D E
{
�� &
ResolverComunicacionBaja
�� ,
(
��, -
sede
��- 1
,
��1 22
$documentosInvalidadosEnvioIndividual
��3 W
,
��W X
logEnvio
��Y a
)
��a b
;
��b c
}
�� 
	Documento
�� 
[
�� 
]
�� /
!documentosInvalidadosEnvioResumen
�� =
=
��> ?X
IObtenerNotasDebitoInvalidadasEnviadasAceptadasUnaSolaVezSinEnvioPendiente��@ �
(��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
if
�� 
(
�� /
!documentosInvalidadosEnvioResumen
�� 5
.
��5 6
Count
��6 ;
(
��; <
)
��< =
>
��> ?
$num
��@ A
)
��A B
{
�� 
	Documento
�� 
[
�� 
]
�� *
documentosBoletasInvalidados
��  <
=
��= >/
!documentosInvalidadosEnvioResumen
��? `
.
��` a
Where
��a f
(
��f g
d
��g h
=>
��i k
d
��l m
.
��m n
serieComprobante
��n ~
.
��~ 
	Substring�� �
(��� �
$num��� �
,��� �
$num��� �
)��� �
==��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �7
'PrefijoSerieNotaCreditoNotaDebitoBoleta��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
;��� �
List
�� 
<
�� 
DateTime
�� !
>
��! "1
#fechasDeEmisionDistintasInvalidadas
��# F
=
��G H*
documentosBoletasInvalidados
��I e
.
��e f
Select
��f l
(
��l m
d
��m n
=>
��o q
d
��r s
.
��s t
fechaEmision��t �
.��� �
Date��� �
)��� �
.��� �
Distinct��� �
(��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (1
#fechasDeEmisionDistintasInvalidadas
��) L
)
��L M
{
�� 
try
�� 
{
�� 
var
�� 
documentosDelDia
��  0
=
��1 2*
documentosBoletasInvalidados
��3 O
.
��O P
Where
��P U
(
��U V
d
��V W
=>
��X Z
d
��[ \
.
��\ ]
fechaEmision
��] i
.
��i j
Date
��j n
==
��o q
item
��r v
)
��v w
.
��w x
ToArray
��x 
(�� �
)��� �
;��� �
var
�� "
resultadoInvalidados
��  4
=
��5 6#
ResolverResumenDiario
��7 L
(
��L M
sede
��M Q
,
��Q R
documentosDelDia
��S c
,
��c d
false
��e j
,
��j k
$str��l �
,��� �
$str��� �
,��� �
	ModoEnvio��� �
.��� �
	Anulacion��� �
)��� �
;��� �
Util
��  
.
��  !-
ManejoEnLogicaResultadoSinExito
��! @
(
��@ A"
resultadoInvalidados
��A U
,
��U V
$str��W �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� (
e
��) *
)
��* +
{
�� 
logEnvio
�� $
.
��$ %
Error
��% *
.
��* +
Add
��+ .
(
��. /
string
��/ 5
.
��5 6
Join
��6 :
(
��: ;
$str
��; ?
,
��? @*
documentosBoletasInvalidados
��A ]
.
��] ^
Where
��^ c
(
��c d
d
��d e
=>
��f h
d
��i j
.
��j k
fechaEmision
��k w
.
��w x
Date
��x |
==
��} 
item��� �
)��� �
)��� �
+��� �
$str��� �
+��� �
Util��� �
.��� �!
InicioErrorString��� �
(��� �
e��� �
)��� �
)��� �
;��� �
logEnvio
�� $
.
��$ %
LogError
��% -
.
��- .
Add
��. 1
(
��1 2
string
��2 8
.
��8 9
Join
��9 =
(
��= >
$str
��> B
,
��B C*
documentosBoletasInvalidados
��D `
.
��` a
Where
��a f
(
��f g
d
��g h
=>
��i k
d
��l m
.
��m n
fechaEmision
��n z
.
��z {
Date
��{ 
==��� �
item��� �
)��� �
)��� �
+��� �
$str��� �
+��� �
Util��� �
.��� �
SoloErrorString��� �
(��� �
e��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
}
�� 
if
�� 
(
�� "
notasDebitoPorEnviar
�� (
.
��( )
Length
��) /
==
��0 2
$num
��3 4
&&
��5 72
$documentosInvalidadosEnvioIndividual
��8 \
.
��\ ]
Count
��] b
(
��b c
)
��c d
==
��e g
$num
��h i
&&
��j l0
!documentosInvalidadosEnvioResumen��m �
.��� �
Count��� �
(��� �
)��� �
==��� �
$num��� �
)��� �
{
�� 
logEnvio
�� 
.
��  
LogNoHayDocumentos
�� /
(
��/ 0
true
��0 4
,
��4 5
$str
��6 Y
)
��Y Z
;
��Z [
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
logEnvio
�� 
.
�� 
Error
�� 
.
�� 
Add
�� "
(
��" #
$str
��# C
+
��D E
$str
��F K
+
��L M
Util
��N R
.
��R S
InicioErrorString
��S d
(
��d e
e
��e f
)
��f g
)
��g h
;
��h i
logEnvio
�� 
.
�� 
LogError
�� !
.
��! "
Add
��" %
(
��% &
$str
��& F
+
��G H
$str
��I N
+
��O P
Util
��Q U
.
��U V
SoloErrorString
��V e
(
��e f
e
��f g
)
��g h
)
��h i
;
��i j
}
�� 
return
�� 
new
�� 
OperationResult
�� &
(
��& '!
OperationResultEnum
��' :
.
��: ;
Success
��; B
,
��B C
$str
��D _
,
��_ `
$str
��a e
)
��e f
;
��f g
}
�� 	
public
�� 
OperationResult
��  
ResolverEnvioNotas
�� 1
(
��1 2
	Documento
��2 ;
[
��; <
]
��< =

documentos
��> H
,
��H I
LogEnvio
��J R
logEnvio
��S [
)
��[ \
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' (
null
��) -
;
��- ./
!EstablecimientoComercialExtendido
�� 1
sede
��2 6
=
��7 8
_sedeLogica
��9 D
.
��D E"
ObtenerSedeExtendida
��E Y
(
��Y Z
)
��Z [
;
��[ \
List
�� 
<
�� 
	Documento
�� 
>
�� &
documentosNotasDeBoletas
��  8
=
��9 :
new
��; >
List
��? C
<
��C D
	Documento
��D M
>
��M N
(
��N O
)
��O P
;
��P Q
List
�� 
<
�� 
	Documento
�� 
>
�� '
documentosNotasDeFacturas
��  9
=
��: ;
new
��< ?
List
��@ D
<
��D E
	Documento
��E N
>
��N O
(
��O P
)
��P Q
;
��Q R
List
�� 
<
�� 
	Documento
�� 
>
�� 2
$documentosNotasDeFacturasAdicionadas
��  D
=
��E F
new
��G J
List
��K O
<
��O P
	Documento
��P Y
>
��Y Z
(
��Z [
)
��[ \
;
��\ ]
List
�� 
<
�� 
	Documento
�� 
>
�� /
!documentosNotasDeFacturasAnuladas
��  A
=
��B C
new
��D G
List
��H L
<
��L M
	Documento
��M V
>
��V W
(
��W X
)
��X Y
;
��Y Z'
documentosNotasDeFacturas
�� )
.
��) *
AddRange
��* 2
(
��2 3

documentos
��3 =
.
��= >
Where
��> C
(
��C D
d
��D E
=>
��F H
d
��I J
.
��J K
serieComprobante
��K [
.
��[ \
	Substring
��\ e
(
��e f
$num
��f g
,
��g h
$num
��i j
)
��j k
==
��l n-
FacturacionElectronicaSettings��o �
.��� �
Default��� �
.��� �8
(PrefijoSerieNotaCreditoNotaDebitoFactura��� �
)��� �
)��� �
;��� �&
documentosNotasDeBoletas
�� (
.
��( )
AddRange
��) 1
(
��1 2

documentos
��2 <
.
��< =
Where
��= B
(
��B C
d
��C D
=>
��E G
d
��H I
.
��I J
serieComprobante
��J Z
.
��Z [
	Substring
��[ d
(
��d e
$num
��e f
,
��f g
$num
��h i
)
��i j
==
��k m-
FacturacionElectronicaSettings��n �
.��� �
Default��� �
.��� �7
'PrefijoSerieNotaCreditoNotaDebitoBoleta��� �
)��� �
)��� �
;��� �2
$documentosNotasDeFacturasAdicionadas
�� 4
=
��5 6'
documentosNotasDeFacturas
��7 P
.
��P Q
Where
��Q V
(
��V W
d
��W X
=>
��Y [
d
��\ ]
.
��] ^
estado
��^ d
==
��e g
(
��h i
int
��i l
)
��l m)
EstadoDocumentoElectronico��m �
.��� �

Adicionado��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �/
!documentosNotasDeFacturasAnuladas
�� 1
=
��2 3'
documentosNotasDeFacturas
��4 M
.
��M N
Where
��N S
(
��S T
d
��T U
=>
��V X
d
��Y Z
.
��Z [
estado
��[ a
==
��b d
(
��e f
int
��f i
)
��i j)
EstadoDocumentoElectronico��j �
.��� �
Anulado��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
if
�� 
(
�� 2
$documentosNotasDeFacturasAdicionadas
�� 8
.
��8 9
Count
��9 >
>
��? @
$num
��A B
)
��B C
{
�� 
var
�� :
,resultEnvioDocumentoNotasFacturasAdicionadas
�� D
=
��E F%
ResolverEnvioIndividual
��G ^
(
��^ _3
$documentosNotasDeFacturasAdicionadas��_ �
.��� �
ToArray��� �
(��� �
)��� �
,��� �
logEnvio��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� /
!documentosNotasDeFacturasAnuladas
�� 5
.
��5 6
Count
��6 ;
>
��< =
$num
��> ?
)
��? @
{
�� 
var
�� .
 resultadoResolverEnvioIndividual
�� 8
=
��9 :%
ResolverEnvioIndividual
��; R
(
��R S/
!documentosNotasDeFacturasAnuladas
��S t
.
��t u
ToArray
��u |
(
��| }
)
��} ~
,
��~ 
logEnvio��� �
)��� �
;��� �
List
�� 
<
�� 
	Documento
�� "
>
��" #.
 documentosAceptadosParaDarDeBaja
��$ D
=
��E F
ObtenerDocumentos
��G X
(
��X Y

documentos
��Y c
.
��c d
Select
��d j
(
��j k
d
��k l
=>
��m o
d
��p q
.
��q r
id
��r t
)
��t u
.
��u v
ToList
��v |
(
��| }
)
��} ~
)
��~ 
.�� �
Where��� �
(��� �
d��� �
=>��� �
d��� �
.��� �
EnvioDocumento��� �
.��� �
Count��� �
(��� �
ed��� �
=>��� �
ed��� �
.��� �
Envio��� �
.��� �
estado��� �
==��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �
Aceptado��� �
||��� �
ed��� �
.��� �
Envio��� �
.��� �
estado��� �
==��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �(
AceptadoConObservaciones��� �
)��� �
>��� �
$num��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
if
�� 
(
�� .
 documentosAceptadosParaDarDeBaja
�� 8
.
��8 9
Count
��9 >
(
��> ?
)
��? @
>
��A B
$num
��C D
)
��D E
{
�� 
List
�� 
<
�� 
DateTime
�� %
>
��% &&
fechasDeEmisionDistintas
��' ?
=
��@ A.
 documentosAceptadosParaDarDeBaja
��B b
.
��b c
Select
��c i
(
��i j
d
��j k
=>
��l n
d
��o p
.
��p q
fechaEmision
��q }
.
��} ~
Date��~ �
)��� �
.��� �
Distinct��� �
(��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
��  !
var
��! $
item
��% )
in
��* ,&
fechasDeEmisionDistintas
��- E
)
��E F
{
�� 
var
�� :
,resultadoEnvioDocumentoNotasFacturasAnuladas
��  L
=
��M N&
ResolverComunicacionBaja
��O g
(
��g h
sede
��h l
,
��l m/
 documentosAceptadosParaDarDeBaja��n �
.��� �
Where��� �
(��� �
d��� �
=>��� �
d��� �
.��� �
fechaEmision��� �
.��� �
Date��� �
==��� �
item��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
,��� �
logEnvio��� �
)��� �
;��� �
}
�� 
}
�� 
}
�� 
if
�� 
(
�� &
documentosNotasDeBoletas
�� ,
.
��, -
Count
��- 2
(
��2 3
)
��3 4
>
��5 6
$num
��7 8
)
��8 9
{
�� 
List
�� 
<
�� 
DateTime
�� !
>
��! "&
fechasDeEmisionDistintas
��# ;
=
��< =&
documentosNotasDeBoletas
��> V
.
��V W
Select
��W ]
(
��] ^
d
��^ _
=>
��` b
d
��c d
.
��d e
fechaEmision
��e q
.
��q r
Date
��r v
)
��v w
.
��w x
Distinct��x �
(��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (&
fechasDeEmisionDistintas
��) A
)
��A B
{
�� 
var
�� 1
#resultadoEnvioDocumentoNotasBoletas
�� ?
=
��@ A)
ResolverResumenDiarioPorDia
��B ]
(
��] ^
sede
��^ b
,
��b c&
documentosNotasDeBoletas
��d |
.
��| }
Where��} �
(��� �
d��� �
=>��� �
d��� �
.��� �
fechaEmision��� �
.��� �
Date��� �
==��� �
item��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
,��� �
logEnvio��� �
)��� �
;��� �
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� <
(
��< =1
#resultadoEnvioDocumentoNotasBoletas
��= `
,
��` a
$str��b �
)��� �
;��� �
}
�� 
}
�� 
result
�� 
=
�� 
new
�� 
OperationResult
�� ,
(
��, -!
OperationResultEnum
��- @
.
��@ A
Success
��A H
,
��H I
$str
��J e
,
��e f
$str
��g k
)
��k l
;
��l m
return
�� 
result
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* O
,
��O P
e
��Q R
)
��R S
;
��S T
}
�� 
}
�� 	
public
�� 
OperationResult
�� %
EnvioComunicacionesBaja
�� 6
(
��6 7
LogEnvio
��7 ?
logEnvio
��@ H
)
��H I
{
�� 	
try
�� 
{
�� 
!EstablecimientoComercialExtendido
�� 1
sede
��2 6
=
��7 8
_sedeLogica
��9 D
.
��D E"
ObtenerSedeExtendida
��E Y
(
��Y Z
)
��Z [
;
��[ \
var
�� (
noHayComunicacionesAEnviar
�� .
=
��/ 0
!
��1 2.
 HayFacturasInvalidadasNoEnviadas
��2 R
(
��R S
)
��S T
;
��T U
while
�� 
(
�� .
 HayFacturasInvalidadasNoEnviadas
�� 7
(
��7 8
)
��8 9
)
��9 :
{
�� 
var
�� 

documentos
�� "
=
��# $9
+DevolverFacturasInvalidadasNoEnviadasPorDia
��% P
(
��P Q
)
��Q R
.
��R S
ToArray
��S Z
(
��Z [
)
��[ \
;
��\ ]
var
�� .
 resultadoResolverEnvioIndividual
�� 8
=
��9 :%
ResolverEnvioIndividual
��; R
(
��R S

documentos
��S ]
,
��] ^
logEnvio
��_ g
)
��g h
;
��h i
var
�� .
 documentosAceptadosParaDarDeBaja
�� 8
=
��9 :
ObtenerDocumentos
��; L
(
��L M

documentos
��M W
.
��W X
Select
��X ^
(
��^ _
d
��_ `
=>
��a c
d
��d e
.
��e f
id
��f h
)
��h i
.
��i j
ToList
��j p
(
��p q
)
��q r
)
��r s
.
��s t
Where
��t y
(
��y z
d
��z {
=>
��| ~
d�� �
.��� �
EnvioDocumento��� �
.��� �
Count��� �
(��� �
ed��� �
=>��� �
ed��� �
.��� �
Envio��� �
.��� �
estado��� �
==��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �
Aceptado��� �
||��� �
ed��� �
.��� �
Envio��� �
.��� �
estado��� �
==��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �(
AceptadoConObservaciones��� �
)��� �
>��� �
$num��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
if
�� 
(
�� .
 documentosAceptadosParaDarDeBaja
�� 8
.
��8 9
Count
��9 >
(
��> ?
)
��? @
>
��A B
$num
��C D
)
��D E
{
�� 
var
�� /
!resultadoResolverComunicacionBaja
�� =
=
��> ?&
ResolverComunicacionBaja
��@ X
(
��X Y
sede
��Y ]
,
��] ^.
 documentosAceptadosParaDarDeBaja
��_ 
.�� �
ToArray��� �
(��� �
)��� �
,��� �
logEnvio��� �
)��� �
;��� �
}
�� 
}
�� 
var
�� #
documentosInvalidados
�� )
=
��* +J
<ObtenerFacturasInvalidadasAceptadasPeroSinComunicacionDeBaja
��, h
(
��h i
)
��i j
.
��j k
ToArray
��k r
(
��r s
)
��s t
;
��t u
if
�� 
(
�� #
documentosInvalidados
�� )
.
��) *
Length
��* 0
>
��1 2
$num
��3 4
)
��4 5
{
�� 
var
�� &
fechasDeEmisionDistintas
�� 0
=
��1 2#
documentosInvalidados
��3 H
.
��H I
Select
��I O
(
��O P
d
��P Q
=>
��R T
d
��U V
.
��V W
fechaEmision
��W c
.
��c d
Date
��d h
)
��h i
.
��i j
Distinct
��j r
(
��r s
)
��s t
.
��t u
ToList
��u {
(
��{ |
)
��| }
;
��} ~
foreach
�� 
(
�� 
var
��  
item
��! %
in
��& (&
fechasDeEmisionDistintas
��) A
)
��A B
{
�� 
var
��  
documentosPorFecha
�� .
=
��/ 0#
documentosInvalidados
��1 F
.
��F G
Where
��G L
(
��L M
d
��M N
=>
��O Q
d
��R S
.
��S T
fechaEmision
��T `
.
��` a
Date
��a e
==
��f h
item
��i m
)
��m n
.
��n o
ToArray
��o v
(
��v w
)
��w x
;
��x y
var
�� "
resultadoInvalidados
�� 0
=
��1 2&
ResolverComunicacionBaja
��3 K
(
��K L
sede
��L P
,
��P Q 
documentosPorFecha
��R d
,
��d e
logEnvio
��f n
)
��n o
;
��o p
}
�� 
}
�� 
if
�� 
(
�� 
logEnvio
�� 
.
�� 
NoHayDocumentos
�� ,
&&
��- /(
noHayComunicacionesAEnviar
��0 J
&&
��K M#
documentosInvalidados
��N c
.
��c d
Length
��d j
==
��k m
$num
��n o
)
��o p
{
�� 
logEnvio
�� 
.
��  
LogNoHayDocumentos
�� /
(
��/ 0
true
��0 4
,
��4 5
$str
��6 R
)
��R S
;
��S T
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
logEnvio
�� 
.
�� 
Error
�� 
.
�� 
Add
�� "
(
��" #
$str
��# Z
+
��[ \
$str
��] `
+
��a b
Util
��c g
.
��g h
InicioErrorString
��h y
(
��y z
e
��z {
)
��{ |
)
��| }
;
��} ~
logEnvio
�� 
.
�� 
LogError
�� !
.
��! "
Add
��" %
(
��% &
$str
��& ]
+
��^ _
$str
��` e
+
��f g
Util
��h l
.
��l m
SoloErrorString
��m |
(
��| }
e
��} ~
)
��~ 
)�� �
;��� �
}
�� 
return
�� 
new
�� 
OperationResult
�� &
(
��& '!
OperationResultEnum
��' :
.
��: ;
Success
��; B
,
��B C
$str
��D _
,
��_ `
$str
��a e
)
��e f
;
��f g
}
�� 	
public
�� 
OperationResult
�� &
ResolverComunicacionBaja
�� 7
(
��7 8/
!EstablecimientoComercialExtendido
��8 Y
sede
��Z ^
,
��^ _
	Documento
��` i
[
��i j
]
��j k

documentos
��l v
,
��v w
LogEnvio��x �
logEnvio��� �
)��� �
{
�� 	
try
�� 
{
�� 
ComunicacionBaja
��  
comunicacionBaja
��! 1
=
��2 3'
_generacionArchivosLogica
��4 M
.
��M N%
ObtenerComunicacionBaja
��N e
(
��e f

documentos
��f p
,
��p q
sede
��r v
)
��v w
;
��w x
var
�� 
documentoResponse
�� %
=
��& '

RestHelper
��( 2
<
��2 3
ComunicacionBaja
��3 C
,
��C D
DocumentoResponse
��E V
>
��V W
.
��W X
Execute
��X _
(
��_ `
$str
��` }
,
��} ~
comunicacionBaja�� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� 
documentoResponse
�� &
.
��& '
Exito
��' ,
)
��, -
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
documentoResponse
��. ?
.
��? @
MensajeError
��@ L
)
��L M
;
��M N
}
�� 
else
�� 
{
�� 
var
��  
archivoCertificado
�� *
=
��+ , 
ObtenerCertificado
��- ?
(
��? @
sede
��@ D
.
��D E 
DocumentoIdentidad
��E W
)
��W X
;
��X Y
var
�� 
firmadoResponse
�� '
=
��( )
FirmarDocumento
��* 9
(
��9 :
documentoResponse
��: K
,
��K L 
archivoCertificado
��M _
)
��_ `
;
��` a
var
�� 
resultadoEnvio
�� &
=
��' ($
EnviarComunicacionBaja
��) ?
(
��? @
sede
��@ D
,
��D E
comunicacionBaja
��F V
,
��V W
firmadoResponse
��X g
,
��g h

documentos
��i s
.
��s t
Select
��t z
(
��z {
d
��{ |
=>
��} 
d��� �
.��� �
id��� �
)��� �
.��� �
ToArray��� �
(��� �
)��� �
)��� �
;��� �
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 8
(
��8 9
resultadoEnvio
��9 G
,
��G H
$str
��I q
)
��q r
;
��r s
Thread
�� 
.
�� 
Sleep
��  
(
��  !,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G H3
%TiempoEsperaParaConsultarEnvioResumen
��H m
)
��m n
;
��n o
OperationResult
�� #
resultadoTicket
��$ 3
=
��4 5
ConsultarTicket
��6 E
(
��E F
sede
��F J
.
��J K 
DocumentoIdentidad
��K ]
,
��] ^
new
��_ b
EnvioSimplificado
��c t
(
��t u
)
��u v
{
��w x
Id
��y {
=
��| }
resultadoEnvio��~ �
.��� �
data��� �
,��� �
NumeroTicket��� �
=��� �
(��� �
string��� �
)��� �
resultadoEnvio��� �
.��� �
information��� �
}��� �
)��� �
;��� �
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 8
(
��8 9
resultadoTicket
��9 H
,
��H I
$str
��J }
)
��} ~
;
��~ 
logEnvio
�� 
.
�� 
Exito
�� "
.
��" #
Add
��# &
(
��& '
	ItemEnvio
��' 0
.
��0 1
ItemAnulado
��1 <
(
��< =
string
��= C
.
��C D
Join
��D H
(
��H I
$str
��I M
,
��M N

documentos
��O Y
.
��Y Z
Select
��Z `
(
��` a
d
��a b
=>
��c e
d
��f g
.
��g h"
ComprobanteDocumento
��h |
(
��| }
)
��} ~
)
��~ 
)�� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
logEnvio
�� 
.
�� 
Error
�� 
.
�� 
Add
�� "
(
��" #
string
��# )
.
��) *
Join
��* .
(
��. /
$str
��/ 3
,
��3 4

documentos
��5 ?
.
��? @
Select
��@ F
(
��F G
d
��G H
=>
��I K
d
��L M
.
��M N"
ComprobanteDocumento
��N b
(
��b c
)
��c d
)
��d e
)
��e f
+
��g h
$str
��i l
+
��m n
Util
��o s
.
��s t 
InicioErrorString��t �
(��� �
e��� �
)��� �
)��� �
;��� �
logEnvio
�� 
.
�� 
LogError
�� !
.
��! "
Add
��" %
(
��% &
string
��& ,
.
��, -
Join
��- 1
(
��1 2
$str
��2 6
,
��6 7

documentos
��8 B
.
��B C
Select
��C I
(
��I J
d
��J K
=>
��L N
d
��O P
.
��P Q"
ComprobanteDocumento
��Q e
(
��e f
)
��f g
)
��g h
)
��h i
+
��j k
$str
��l q
+
��r s
Util
��t x
.
��x y
SoloErrorString��y �
(��� �
e��� �
)��� �
)��� �
;��� �
}
�� 
return
�� 
new
�� 
OperationResult
�� &
(
��& '!
OperationResultEnum
��' :
.
��: ;
Success
��; B
,
��B C
$str
��D _
,
��_ `
$str
��a e
)
��e f
;
��f g
}
�� 	
public
�� 
OperationResult
�� $
EnviarComunicacionBaja
�� 5
(
��5 6&
EstablecimientoComercial
��6 N
sede
��O S
,
��S T
ComunicacionBaja
��U e
comunicacionBaja
��f v
,
��v w
FirmadoResponse��x �
firmadoResponse��� �
,��� �
long��� �
[��� �
]��� �
idDocumentos��� �
)��� �
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' (
null
��) -
;
��- .
var
�� $
enviarDocumentoRequest
�� *
=
��+ ,
new
��- 0$
EnviarDocumentoRequest
��1 G
{
�� 
Ruc
�� 
=
�� 
sede
�� 
.
��  
DocumentoIdentidad
�� 1
,
��1 2

UsuarioSol
�� 
=
��  ,
FacturacionElectronicaSettings
��! ?
.
��? @
Default
��@ G
.
��G H

UsuarioSol
��H R
,
��R S
ClaveSol
�� 
=
�� ,
FacturacionElectronicaSettings
�� =
.
��= >
Default
��> E
.
��E F
ClaveSol
��F N
,
��N O
EndPointUrl
�� 
=
��  !9
+DevolverUrlEnvioSunatFacturacionElectronica
��" M
(
��M N
)
��N O
,
��O P
IdDocumento
�� 
=
��  !
comunicacionBaja
��" 2
.
��2 3
IdDocumento
��3 >
,
��> ?

�� !
=
��" #,
FacturacionElectronicaSettings
��$ B
.
��B C
Default
��C J
.
��J K.
 CodigoTipoDocumentoResumenDiario
��K k
,
��k l
TramaXmlFirmado
�� #
=
��$ %
firmadoResponse
��& 5
.
��5 6
TramaXmlFirmado
��6 E
}
�� 
;
�� 
var
�� #
enviarResumenResponse
�� )
=
��* +

RestHelper
��, 6
<
��6 7$
EnviarDocumentoRequest
��7 M
,
��M N#
EnviarResumenResponse
��O d
>
��d e
.
��e f
Execute
��f m
(
��m n
$str��n �
,��� �&
enviarDocumentoRequest��� �
,��� �.
FacturacionElectronicaSettings��� �
.��� �
Default��� �
.��� �,
UrlApiFacturacionElectronica��� �
)��� �
;��� �
if
�� 
(
�� 
!
�� #
enviarResumenResponse
�� *
.
��* +
Exito
��+ 0
)
��0 1
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .#
enviarResumenResponse
��. C
.
��C D
MensajeError
��D P
)
��P Q
;
��Q R
}
�� 
var
�� 
resultCrearEnvio
�� $
=
��% &

CrearEnvio
��' 1
(
��1 2
comunicacionBaja
��2 B
.
��B C
IdDocumento
��C N
.
��N O
Split
��O T
(
��T U
$char
��U X
)
��X Y
[
��Y Z
$num
��Z [
]
��[ \
,
��\ ],
FacturacionElectronicaSettings
��^ |
.
��| }
Default��} �
.��� �+
TipoEnvioComunicacionDeBaja��� �
,��� �%
enviarResumenResponse��� �
.��� �
Exito��� �
?��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �
	Pendiente��� �
:��� �
(��� �
int��� �
)��� �
EstadoEnvio��� �
.��� �
	Rechazado��� �
,��� �
$str��� �
,��� �
(��� �
string��� �
.��� �

(��� �%
enviarResumenResponse��� �
.��� �
MensajeError��� �
)��� �
?��� �
$str��� �
:��� �%
enviarResumenResponse��� �
.��� �
MensajeError��� �
)��� �
,��� �%
enviarResumenResponse��� �
.��� �
	NroTicket��� �
,��� �
Encoding��� �
.��� �
UTF8��� �
.��� �
GetBytes��� �
(��� �
JsonConvert��� �
.��� �
SerializeObject��� �
(��� �
firmadoResponse��� �
)��� �
)��� �
,��� �
null��� �
,��� �
	ModoEnvio��� �
.��� �
Ninguno��� �
)��� �
;��� �
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 4
(
��4 5
resultCrearEnvio
��5 E
,
��E F
$str��G �
+��� �%
enviarResumenResponse��� �
.��� �
	NroTicket��� �
+��� �
$str��� �
+��� � 
comunicacionBaja��� �
.��� �
IdDocumento��� �
+��� �
$str��� �
+��� �
(��� �
string��� �
.��� �

(��� �%
enviarResumenResponse��� �
.��� �
MensajeError��� �
)��� �
?��� �
$str��� �
:��� �%
enviarResumenResponse��� �
.��� �
MensajeError��� �
)��� �
)��� �
;��� �
for
�� 
(
�� 
int
�� 
i
�� 
=
�� 
$num
�� 
;
�� 
i
��  !
<
��" #
idDocumentos
��$ 0
.
��0 1
Length
��1 7
;
��7 8
i
��9 :
++
��: <
)
��< =
{
�� 
var
�� '
resultCrearEnvioDocumento
�� 1
=
��2 3!
CrearEnvioDocumento
��4 G
(
��G H
resultCrearEnvio
��H X
.
��X Y
data
��Y ]
,
��] ^
idDocumentos
��_ k
[
��k l
i
��l m
]
��m n
)
��n o
;
��o p
Util
�� 
.
�� -
ManejoEnLogicaResultadoSinExito
�� 8
(
��8 9'
resultCrearEnvioDocumento
��9 R
,
��R S
$str
��T }
)
��} ~
;
��~ 
}
�� 
result
�� 
=
�� 
new
�� 
OperationResult
�� ,
(
��, -!
OperationResultEnum
��- @
.
��@ A
Success
��A H
,
��H I
$str
��J e
,
��e f
$str
��g k
)
��k l
{
�� 
data
�� 
=
�� 
resultCrearEnvio
�� +
.
��+ ,
data
��, 0
,
��0 1
information
�� 
=
��  !#
enviarResumenResponse
��" 7
.
��7 8
	NroTicket
��8 A
}
�� 
;
�� 
return
�� 
result
�� 
;
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* S
,
��S T
e
��U V
)
��V W
;
��W X
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
OperationResult
�� )
>
��) *(
EnviarGuiaDeRemisionManual
��+ E
(
��E F
DateTime
��F N

fechaDesde
��O Y
,
��Y Z
DateTime
��[ c

fechaHasta
��d n
,
��n o0
!EstablecimientoComercialExtendido��p �
sede��� �
,��� �
int��� �

idEmpleado��� �
,��� �
string��� �
path��� �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
long
�� 
>
�� 
idsGuiasRemision
�� +
=
��, -
_operacionLogica
��. >
.
��> ?2
$ObtenerIdsGuiaRemisionPorEnviarSunat
��? c
(
��c d

fechaDesde
��d n
,
��n o

fechaHasta
��p z
)
��z {
;
��{ |
foreach
�� 
(
�� 
var
�� 
idGuiaRemision
�� +
in
��, .
idsGuiasRemision
��/ ?
)
��? @
{
�� 
await
�� ,
TransmitirEnviarGuiaDeRemision
�� 8
(
��8 9
idGuiaRemision
��9 G
,
��G H
sede
��I M
,
��M N

idEmpleado
��O Y
,
��Y Z
path
��[ _
)
��_ `
;
��` a
}
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +!
OperationResultEnum
��+ >
.
��> ?
Success
��? F
,
��F G
$str
��H c
,
��c d
$str
��e i
)
��i j
;
��j k
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ^
,
��^ _
e
��` a
)
��a b
;
��b c
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
OperationResult
�� )
>
��) *,
TransmitirEnviarGuiaDeRemision
��+ I
(
��I J
long
��J N
idMovimiento
��O [
,
��[ \/
!EstablecimientoComercialExtendido
��] ~
sede�� �
,��� �
int��� �

idEmpleado��� �
,��� �
string��� �
path��� �
)��� �
{
�� 	
try
�� 
{
�� 
MovimientoDeAlmacen
�� #

movimiento
��$ .
=
��/ 0
_operacionLogica
��1 A
.
��A B+
ObtenerMovimientoDeMercaderia
��B _
(
��_ `
idMovimiento
��` l
)
��l m
;
��m n
var
�� '
detalleMaestroComprobante
�� -
=
��. /
await
��0 5
_maestroLogica
��6 D
.
��D E*
ObtenerDetallesMaestrosAsync
��E a
(
��a b
MaestroSettings
��b q
.
��q r
Default
��r y
.
��y z!
IdMaestroDocumento��z �
)��� �
;��� �
var
�� 
proveedores
�� 
=
��  !!
_actorNegocioLogica
��" 5
.
��5 6(
ObtenerProveedoresVigentes
��6 P
(
��P Q
)
��Q R
;
��R S
var
�� 
modalidades
�� 
=
��  !
await
��" '
_maestroLogica
��( 6
.
��6 7*
ObtenerDetallesMaestrosAsync
��7 S
(
��S T
MaestroSettings
��T c
.
��c d
Default
��d k
.
��k l+
IdMaestroModalidadDeTraslado��l �
)��� �
;��� �
var
�� 
motivos
�� 
=
�� 
await
�� #
_maestroLogica
��$ 2
.
��2 3*
ObtenerDetallesMaestrosAsync
��3 O
(
��O P
MaestroSettings
��P _
.
��_ `
Default
��` g
.
��g h(
IdMaestroMotivoDeTraslado��h �
)��� �
;��� �
GuiaDeRemision
�� 
guiaDeRemision
�� -
=
��. /
new
��0 3
GuiaDeRemision
��4 B
(
��B C

movimiento
��C M
,
��M N
sede
��O S
,
��S T
new
��U X7
(EstablecimientoComercialExtendidoConLogo��Y �
(��� �

movimiento��� �
.��� �
Transaccion��� �
(��� �
)��� �
.��� �
Actor_negocio2��� �
.��� �
Actor_negocio2��� �
)��� �
,��� �
null��� �
,��� �
false��� �
,��� �
(��� �0
 ModoImpresionCaracteristicasEnum��� �
)��� �
VentasSettings��� �
.��� �
Default��� �
.��� �,
modoImpresionCaracteristicas��� �
,��� �
proveedores��� �
,��� �
modalidades��� �
,��� �
motivos��� �
)��� �
;��� �
GuiaRemision
�� 
guiaRemision
�� )
=
��* +'
_generacionArchivosLogica
��, E
.
��E F7
)ObtenerDocumentoElectronicoGuiaDeRemision
��F o
(
��o p
guiaDeRemision
��p ~
)
��~ 
;�� �
Envio
�� 
	envioGuia
�� 
=
��  !
await
��" ''
ResolverEnvioGuiaRemision
��( A
(
��A B
guiaRemision
��B N
,
��N O
path
��P T
)
��T U
;
��U V
var
�� 
	resultado
�� 
=
�� 
await
��  %!
CrearGuiaDeRemision
��& 9
(
��9 :
guiaDeRemision
��: H
,
��H I'
detalleMaestroComprobante
��J c
,
��c d
	envioGuia
��e n
,
��n o

idEmpleado
��p z
)
��z {
;
��{ |
return
�� 
	resultado
��  
;
��  !
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
LogicaException
�� )
(
��) *
$str
��* ^
,
��^ _
e
��` a
)
��a b
;
��b c
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
OperationResult
�� )
>
��) *!
CrearGuiaDeRemision
��+ >
(
��> ?
GuiaDeRemision
��? M
guiasDeRemision
��N ]
,
��] ^
List
��_ c
<
��c d
Detalle_maestro
��d s
>
��s t)
detallesMaestroComprobante��u �
,��� �
Envio��� �
envio��� �
,��� �
int��� �

idEmpleado��� �
)��� �
{
�� 	
try
�� 
{
�� 
GuiaRemision
�� 
guiaRemision
�� )
=
��* +'
_generacionArchivosLogica
��, E
.
��E F7
)ObtenerDocumentoElectronicoGuiaDeRemision
��F o
(
��o p
guiasDeRemision
��p 
)�� �
;��� �
	Documento
�� 
	documento
�� #
=
��$ %
new
��& )
	Documento
��* 3
{
�� 

idSigescom
�� 
=
��  
guiasDeRemision
��! 0
.
��0 1
IdOrden
��1 8
,
��8 9#
codigoTipoComprobante
�� )
=
��* +
guiaRemision
��, 8
.
��8 9

��9 F
,
��F G
serieComprobante
�� $
=
��% &
guiaRemision
��' 3
.
��3 4
IdDocumento
��4 ?
.
��? @
Split
��@ E
(
��E F
$char
��F I
)
��I J
[
��J K
$num
��K L
]
��L M
,
��M N
numeroComprobante
�� %
=
��& '
guiaRemision
��( 4
.
��4 5
IdDocumento
��5 @
.
��@ A
Split
��A F
(
��F G
$char
��G J
)
��J K
[
��K L
$num
��L M
]
��M N
,
��N O
fechaEmision
��  
=
��! "
DateTime
��# +
.
��+ ,
Parse
��, 1
(
��1 2
guiaRemision
��2 >
.
��> ?
FechaEmision
��? K
)
��K L
,
��L M
tipoComprobante
�� #
=
��$ %(
detallesMaestroComprobante
��& @
.
��@ A
Single
��A G
(
��G H
d
��H I
=>
��J L
d
��M N
.
��N O
codigo
��O U
==
��V X
guiaRemision
��Y e
.
��e f

��f s
)
��s t
.
��t u
nombre
��u {
,
��{ |
estado
�� 
=
�� 
guiasDeRemision
�� ,
.
��, -
IdEstadoActual
��- ;
==
��< >
MaestroSettings
��? N
.
��N O
Default
��O V
.
��V W.
 IdDetalleMaestroEstadoConfirmado
��W w
?
��x y
(
��z {
int
��{ ~
)
��~ )
EstadoDocumentoElectronico�� �
.��� �

Adicionado��� �
:��� �
(��� �
int��� �
)��� �*
EstadoDocumentoElectronico��� �
.��� �
Anulado��� �
,��� �
estadoSigescom
�� "
=
��# $
guiasDeRemision
��% 4
.
��4 5
IdEstadoActual
��5 C
==
��D F
MaestroSettings
��G V
.
��V W
Default
��W ^
.
��^ _.
 IdDetalleMaestroEstadoConfirmado
��_ 
?��� �
(��� �
int��� �
)��� �2
"EstadoSigescomDocumentoElectronico��� �
.��� �

Confirmado��� �
:��� �
(��� �
int��� �
)��� �2
"EstadoSigescomDocumentoElectronico��� �
.��� �

Invalidado��� �
,��� �
Binario
�� 
=
�� 
new
�� !
Binario
��" )
{
��* +
archivoBinario
��, :
=
��; <
Encoding
��= E
.
��E F
UTF8
��F J
.
��J K
GetBytes
��K S
(
��S T
JsonConvert
��T _
