�
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\App_Start\BundleConfig.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
{ 
public 

class 
BundleConfig 
{ 
public		 
static		 
void		 
RegisterBundles		 *
(		* +
BundleCollection		+ ;
bundles		< C
)		C D
{

 	
bundles 
. 
Add 
( 
new 
ScriptBundle (
(( )
$str) >
)> ?
.? @
Include@ G
(G H
$str 4
)4 5
)5 6
;6 7
bundles 
. 
Add 
( 
new 
ScriptBundle (
(( )
$str) <
)< =
.= >
Include> E
(E F
$str H
,H I
$str ;
,; <
$str B
,B C
$str H
,H I
$str O
,O P
$str W
,W X
$str 7
,7 8
$str 0
,0 1
$str 2
,2 3
$str F
,F G
$str @
,@ A
$str J
,J K
$str Q
,Q R
$str   G
,  G H
$str!! N
,!!N O
$str"" G
,""G H
$str## C
,##C D
$str$$ >
,$$> ?
$str%% :
,%%: ;
$str&& B
,&&B C
$str'' >
,''> ?
$str(( _
,((_ `
$str++ 6
,++6 7
$str,, C
,,,C D
$str-- C
,--C D
$str.. ;
,..; <
$str// C
,//C D
$str00 .
,00. /
$str11 /
,11/ 0
$str22 *
,22* +
$str33 .
)33. /
)33/ 0
;330 1
bundles>> 
.>> 
Add>> 
(>> 
new>> 
StyleBundle>> '
(>>' (
$str>>( :
)>>: ;
.>>; <
Include>>< C
(>>C D
$str?? 3
,??3 4
$str@@ /
,@@/ 0
$strAA 8
,AA8 9
$strBB /
,BB/ 0
$strCC /
,CC/ 0
$strDD /
,DD/ 0
$strEE H
,EEH I
$strFF O
,FFO P
$strGG K
,GGK L
$strHH <
,HH< =
$strII 8
,II8 9
$strJJ ,
,JJ, -
$strLL F
,LLF G
$strMM B
)NN 
)NN 
;NN 
bundlesPP 
.PP 
AddPP 
(PP 
newPP 
StyleBundlePP '
(PP' (
$strPP( B
)PPB C
.PPC D
IncludePPD K
(PPK L
$strQQ /
)RR 
)RR 
;RR 
BundleTableTT 
.TT 
EnableOptimizationsTT +
=TT, -
trueTT. 2
;TT2 3
}VV 	
}WW 
}XX �
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\App_Start\FilterConfig.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
{ 
public 

class 
FilterConfig 
{ 
public 
static 
void !
RegisterGlobalFilters 0
(0 1"
GlobalFilterCollection1 G
filtersH O
)O P
{		 	
filters

 
.

 
Add

 
(

 
new

 
ErrorHandler

 (
.

( )"
AiHandleErrorAttribute

) ?
(

? @
)

@ A
)

A B
;

B C
} 	
} 
}
OD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\App_Start\IdentityConfig.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
{ 
public 

class 
EmailService 
: #
IIdentityMessageService  7
{ 
public 
Task 
	SendAsync 
( 
IdentityMessage -
message. 5
)5 6
{ 	

SmtpClient 
client 
= 
new  #

SmtpClient$ .
(. / 
ConfigurationManager/ C
.C D
AppSettingsD O
[O P
$strP \
]\ ]
,] ^
int_ b
.b c
Parsec h
(h i 
ConfigurationManageri }
.} ~
AppSettings	~ �
[
� �
$str
� �
]
� �
)
� �
)
� �
;
� �
client 
. 
DeliveryMethod !
=" #
SmtpDeliveryMethod$ 6
.6 7
Network7 >
;> ?
client 
. !
UseDefaultCredentials (
=) *
false+ 0
;0 1
client 
. 
Credentials 
=  
new! $
NetworkCredential% 6
(6 7 
ConfigurationManager7 K
.K L
AppSettingsL W
[W X
$strX e
]e f
,f g 
ConfigurationManagerh |
.| }
AppSettings	} �
[
� �
$str
� �
]
� �
)
� �
;
� �
var%% 
mail%% 
=%% 
new%% 
MailMessage%% &
(%%& ' 
ConfigurationManager%%' ;
.%%; <
AppSettings%%< G
[%%G H
$str%%H U
]%%U V
,%%V W
message%%X _
.%%_ `
Destination%%` k
)%%k l
{&& 
Subject'' 
='' 
message'' !
.''! "
Subject''" )
,'') *
Body(( 
=(( 
message(( 
.(( 
Body(( #
,((# $

IsBodyHtml)) 
=)) 
true)) !
,))! "
}** 
;**
client,, 
.,, 
Send,, 
(,, 
mail,, 
),, 
;,, 
return.. 
Task.. 
... 

FromResult.. "
(.." #
$num..# $
)..$ %
;..% &
}// 	
}00 
public33 

class33 

SmsService33 
:33 #
IIdentityMessageService33 5
{44 
public55 
Task55 
	SendAsync55 
(55 
IdentityMessage55 -
message55. 5
)555 6
{66 	
return88 
Task88 
.88 

FromResult88 "
(88" #
$num88# $
)88$ %
;88% &
}99 	
}:: 
public== 

class== "
ApplicationUserManager== '
:==( )
UserManager==* 5
<==5 6
ApplicationUser==6 E
>==E F
{>> 
public?? "
ApplicationUserManager?? %
(??% &

IUserStore??& 0
<??0 1
ApplicationUser??1 @
>??@ A
store??B G
)??G H
:@@ 
base@@ 
(@@ 
store@@ 
)@@ 
{AA 	
}BB 	
publicDD 
staticDD "
ApplicationUserManagerDD ,
CreateDD- 3
(DD3 4"
IdentityFactoryOptionsDD4 J
<DDJ K"
ApplicationUserManagerDDK a
>DDa b
optionsDDc j
,DDj k
IOwinContextDDl x
context	DDy �
)
DD� �
{EE 	
varFF 
managerFF 
=FF 
newFF "
ApplicationUserManagerFF 4
(FF4 5
newFF5 8
	UserStoreFF9 B
<FFB C
ApplicationUserFFC R
>FFR S
(FFS T
contextFFT [
.FF[ \
GetFF\ _
<FF_ ` 
ApplicationDbContextFF` t
>FFt u
(FFu v
)FFv w
)FFw x
)FFx y
;FFy z
managerHH 
.HH 

=HH" #
newHH$ '

<HH5 6
ApplicationUserHH6 E
>HHE F
(HHF G
managerHHG N
)HHN O
{II 
AllowOnlyAlphanumericUserNamesJJ .
=JJ/ 0
falseJJ1 6
,JJ6 7
RequireUniqueEmailKK "
=KK# $
trueKK% )
}LL 
;LL
managerOO 
.OO 
PasswordValidatorOO %
=OO& '
newOO( +
PasswordValidatorOO, =
{PP 
RequiredLengthQQ 
=QQ  
$numQQ! "
,QQ" ##
RequireNonLetterOrDigitRR '
=RR( )
falseRR* /
,RR/ 0
RequireDigitSS 
=SS 
falseSS $
,SS$ %
RequireLowercaseTT  
=TT! "
falseTT# (
,TT( )
RequireUppercaseUU  
=UU! "
falseUU# (
,UU( )
}VV 
;VV
managerYY 
.YY '
UserLockoutEnabledByDefaultYY /
=YY0 1
trueYY2 6
;YY6 7
managerZZ 
.ZZ )
DefaultAccountLockoutTimeSpanZZ 1
=ZZ2 3
TimeSpanZZ4 <
.ZZ< =
FromMinutesZZ= H
(ZZH I
$numZZI J
)ZZJ K
;ZZK L
manager[[ 
.[[ 0
$MaxFailedAccessAttemptsBeforeLockout[[ 8
=[[9 :
$num[[; <
;[[< =
manager__ 
.__ %
RegisterTwoFactorProvider__ -
(__- .
$str__. :
,__: ;
new__< ?$
PhoneNumberTokenProvider__@ X
<__X Y
ApplicationUser__Y h
>__h i
{`` 

=aa 
$straa  ;
}bb 
)bb
;bb 
managercc 
.cc %
RegisterTwoFactorProvidercc -
(cc- .
$strcc. :
,cc: ;
newcc< ?
EmailTokenProvidercc@ R
<ccR S
ApplicationUserccS b
>ccb c
{dd 
Subjectee 
=ee 
$stree )
,ee) *

BodyFormatff 
=ff 
$strff 8
}gg 
)gg
;gg 
managerhh 
.hh 
EmailServicehh  
=hh! "
newhh# &
EmailServicehh' 3
(hh3 4
)hh4 5
;hh5 6
managerii 
.ii 

SmsServiceii 
=ii  
newii! $

SmsServiceii% /
(ii/ 0
)ii0 1
;ii1 2
varjj "
dataProtectionProviderjj &
=jj' (
optionsjj) 0
.jj0 1"
DataProtectionProviderjj1 G
;jjG H
ifkk 
(kk "
dataProtectionProviderkk &
!=kk' )
nullkk* .
)kk. /
{ll 
managermm 
.mm 
UserTokenProvidermm )
=mm* +
newnn &
DataProtectorTokenProvidernn 2
<nn2 3
ApplicationUsernn3 B
>nnB C
(nnC D"
dataProtectionProvidernnD Z
.nnZ [
Createnn[ a
(nna b
$strnnb t
)nnt u
)nnu v
;nnv w
}oo 
returnpp 
managerpp 
;pp 
}qq 	
}rr 
publicuu 

classuu $
ApplicationSignInManageruu )
:uu* +

<uu9 :
ApplicationUseruu: I
,uuI J
stringuuK Q
>uuQ R
{vv 
publicww $
ApplicationSignInManagerww '
(ww' ("
ApplicationUserManagerww( >
userManagerww? J
,wwJ K"
IAuthenticationManagerwwL b!
authenticationManagerwwc x
)wwx y
:xx 
basexx 
(xx 
userManagerxx 
,xx !
authenticationManagerxx  5
)xx5 6
{yy 	
}zz 	
public|| 
override|| 
Task|| 
<|| 
ClaimsIdentity|| +
>||+ ,#
CreateUserIdentityAsync||- D
(||D E
ApplicationUser||E T
user||U Y
)||Y Z
{}} 	
return 
user 
. %
GenerateUserIdentityAsync 1
(1 2
(2 3"
ApplicationUserManager3 I
)I J
UserManagerJ U
)U V
;V W
}
�� 	
public
�� 
static
�� &
ApplicationSignInManager
�� .
Create
��/ 5
(
��5 6$
IdentityFactoryOptions
��6 L
<
��L M&
ApplicationSignInManager
��M e
>
��e f
options
��g n
,
��n o
IOwinContext
��p |
context��} �
)��� �
{
�� 	
return
�� 
new
�� &
ApplicationSignInManager
�� /
(
��/ 0
context
��0 7
.
��7 8
GetUserManager
��8 F
<
��F G$
ApplicationUserManager
��G ]
>
��] ^
(
��^ _
)
��_ `
,
��` a
context
��b i
.
��i j
Authentication
��j x
)
��x y
;
��y z
}
�� 	
}
�� 
public
�� 

class
�� $
ApplicationRoleManager
�� '
:
��( )
RoleManager
��* 5
<
��5 6
IdentityRole
��6 B
>
��B C
{
�� 
public
�� $
ApplicationRoleManager
�� %
(
��% &

IRoleStore
��& 0
<
��0 1
IdentityRole
��1 =
,
��= >
string
��? E
>
��E F
store
��G L
)
��L M
:
�� 
base
�� 
(
�� 
store
�� 
)
�� 
{
�� 	
}
�� 	
public
�� 
static
�� $
ApplicationRoleManager
�� ,
Create
��- 3
(
��3 4$
IdentityFactoryOptions
��4 J
<
��J K$
ApplicationRoleManager
��K a
>
��a b
options
��c j
,
��j k
IOwinContext
��l x
context��y �
)��� �
{
�� 	
var
�� 
	roleStore
�� 
=
�� 
new
�� 
	RoleStore
��  )
<
��) *
IdentityRole
��* 6
>
��6 7
(
��7 8
context
��8 ?
.
��? @
Get
��@ C
<
��C D"
ApplicationDbContext
��D X
>
��X Y
(
��Y Z
)
��Z [
)
��[ \
;
��\ ]
return
�� 
new
�� $
ApplicationRoleManager
�� -
(
��- .
	roleStore
��. 7
)
��7 8
;
��8 9
}
�� 	
}
�� 
}�� �	
LD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\App_Start\RouteConfig.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
{		 
public

 

class

 
RouteConfig

 
{ 
public 
static 
void 
RegisterRoutes )
() *
RouteCollection* 9
routes: @
)@ A
{
routes 
. 
IgnoreRoute 
( 
$str ;
); <
;< =
routes 
. 
MapRoute 
( 
name 
: 
$str 
,  
url 
: 
$str 1
,1 2
defaults 
: 
new 
{ 

controller  *
=+ ,
$str- 6
,6 7
action8 >
=? @
$strA H
,H I
idJ L
=M N
UrlParameterO [
.[ \
Optional\ d
}e f
) 
;
} 	
} 
} �
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\App_Start\Startup.Auth.cs
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
 
WebApplication

 %
{ 
public 

partial 
class 
Startup  
{
public 
void 

(! "
IAppBuilder" -
app. 1
)1 2
{ 	
app 
.  
CreatePerOwinContext $
($ % 
ApplicationDbContext% 9
.9 :
Create: @
)@ A
;A B
app 
.  
CreatePerOwinContext $
<$ %"
ApplicationUserManager% ;
>; <
(< ="
ApplicationUserManager= S
.S T
CreateT Z
)Z [
;[ \
app 
.  
CreatePerOwinContext $
<$ %$
ApplicationSignInManager% =
>= >
(> ?$
ApplicationSignInManager? W
.W X
CreateX ^
)^ _
;_ `
app 
.  
CreatePerOwinContext $
<$ %"
ApplicationRoleManager% ;
>; <
(< ="
ApplicationRoleManager= S
.S T
CreateT Z
)Z [
;[ \
app 
. #
UseCookieAuthentication '
(' (
new( +'
CookieAuthenticationOptions, G
{ 
AuthenticationType "
=# $&
DefaultAuthenticationTypes% ?
.? @
ApplicationCookie@ Q
,Q R
	LoginPath 
= 
new 

PathString  *
(* +
$str+ ;
); <
,< =
}%% 
)%%
;%% 
app&& 
.&& #
UseExternalSignInCookie&& '
(&&' (&
DefaultAuthenticationTypes&&( B
.&&B C
ExternalCookie&&C Q
)&&Q R
;&&R S
app)) 
.)) $
UseTwoFactorSignInCookie)) (
())( )&
DefaultAuthenticationTypes))) C
.))C D
TwoFactorCookie))D S
,))S T
TimeSpan))U ]
.))] ^
FromMinutes))^ i
())i j
$num))j k
)))k l
)))l m
;))m n
app.. 
... -
!UseTwoFactorRememberBrowserCookie.. 1
(..1 2&
DefaultAuthenticationTypes..2 L
...L M*
TwoFactorRememberBrowserCookie..M k
)..k l
;..l m
}BB 	
}CC 
}DD �
LD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\App_Start\UnityConfig.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
WebApplication		 %
{

 
public 

class 
UnityConfig 
{ 
private 
static 
readonly 
Lazy  $
<$ %
IUnityContainer% 4
>4 5
	Container6 ?
=@ A
newB E
LazyF J
<J K
IUnityContainerK Z
>Z [
([ \
(\ ]
)] ^
=>_ a
{ 	
var 
	container 
= 
new 
UnityContainer  .
(. /
)/ 0
;0 1

( 
	container #
)# $
;$ %
return 
	container 
; 
} 	
)	 

;
 
public 
static 
IUnityContainer %"
GetConfiguredContainer& <
(< =
)= >
{ 	
return 
	Container 
. 
Value "
;" #
} 	
public%% 
static%% 
void%% 

(%%( )
IUnityContainer%%) 8
	container%%9 B
)%%B C
{&& 	
}:: 	
};; 
}<< �
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\App_Start\WebApiConfig.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
	App_Start& /
{		 
public

 

class

 
WebApiConfig

 
{ 
public 
static 
void 
Register #
(# $
HttpConfiguration$ 5

)C D
{

. 
Routes  
.  !
MapHttpRoute! -
(- .
$str. ;
,; <
$str= T
,T U
new 
{ 
id 
= 
RouteParameter )
.) *
Optional* 2
}3 4
)4 5
;5 6
var 

appXmlType 
= 

.* +

Formatters+ 5
.5 6
XmlFormatter6 B
.B C
SupportedMediaTypesC V
.V W
FirstOrDefaultW e
(e f
tf g
=>h j
tk l
.l m
	MediaTypem v
==w y
$str	z �
)
� �
;
� �

. 

Formatters $
.$ %
XmlFormatter% 1
.1 2
SupportedMediaTypes2 E
.E F
RemoveF L
(L M

appXmlTypeM W
)W X
;X Y
} 	
} 
} �
MD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Content\reports\Class1.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Content& -
.- .
reports. 5
{ 
public 

class 
Class1 
{		 
public

 
string

 
a

 
;

 
public 
int 
b 
; 
} 
}
TD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\AccountController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
[
	Authorize
]
public 

class 
AccountController "
:# $

Controller% /
{ 
private $
ApplicationSignInManager (
_signInManager) 7
;7 8
private "
ApplicationUserManager &
_userManager' 3
;3 4
public 
AccountController  
(  !
)! "
{ 	
} 	
public 
AccountController  
(  !"
ApplicationUserManager! 7
userManager8 C
,C D$
ApplicationSignInManagerE ]

)l m
{ 	
UserManager 
= 
userManager %
;% &

= 

;) *
} 	
public $
ApplicationSignInManager '

{ 	
get 
{   
return!! 
_signInManager!! %
??!!& (
HttpContext!!) 4
.!!4 5
GetOwinContext!!5 C
(!!C D
)!!D E
.!!E F
Get!!F I
<!!I J$
ApplicationSignInManager!!J b
>!!b c
(!!c d
)!!d e
;!!e f
}"" 
private## 
set## 
{$$ 
_signInManager%% 
=%%  
value%%! &
;%%& '
}&& 
}'' 	
public)) "
ApplicationUserManager)) %
UserManager))& 1
{** 	
get++ 
{,, 
return-- 
_userManager-- #
??--$ &
HttpContext--' 2
.--2 3
GetOwinContext--3 A
(--A B
)--B C
.--C D
GetUserManager--D R
<--R S"
ApplicationUserManager--S i
>--i j
(--j k
)--k l
;--l m
}.. 
private// 
set// 
{00 
_userManager11 
=11 
value11 $
;11$ %
}22 
}33 	
[77 	
AllowAnonymous77	 
]77 
[88 	
OutputCache88	 
(88 
NoStore88 
=88 
true88 #
,88# $
Duration88% -
=88. /
$num880 1
)881 2
]882 3
public99 
ActionResult99 
Login99 !
(99! "
string99" (
	returnUrl99) 2
)992 3
{:: 	
ViewBag;; 
.;; 
	ReturnUrl;; 
=;; 
	returnUrl;;  )
;;;) *
return<< 
View<< 
(<< 
)<< 
;<< 
}== 	
[AA 	
HttpPostAA	 
]AA 
[BB 	
AllowAnonymousBB	 
]BB 
[CC 	$
ValidateAntiForgeryTokenCC	 !
]CC! "
publicDD 
asyncDD 
TaskDD 
<DD 
ActionResultDD &
>DD& '
LoginDD( -
(DD- .
LoginViewModelDD. <
modelDD= B
,DDB C
stringDDD J
	returnUrlDDK T
)DDT U
{EE 	
tryFF 
{GG 
ifHH 
(HH 

ModelStateHH 
.HH 
IsValidHH &
)HH& '
{II 
varJJ 
userJJ 
=JJ 
awaitJJ $
UserManagerJJ% 0
.JJ0 1
	FindAsyncJJ1 :
(JJ: ;
modelJJ; @
.JJ@ A
EmailJJA F
,JJF G
modelJJH M
.JJM N
PasswordJJN V
)JJV W
;JJW X
ifKK 
(KK 
userKK 
!=KK 
nullKK  $
)KK$ %
{LL 
awaitMM 
SignInAsyncMM )
(MM) *
userMM* .
,MM. /
modelMM0 5
.MM5 6

RememberMeMM6 @
)MM@ A
;MMA B
returnPP 1
%RedirectToSeleccionarCentroDeAtencionPP D
(PPD E
	returnUrlPPE N
)PPN O
;PPO P
}QQ 
elseRR 
{SS 

ModelStateTT "
.TT" #

(TT0 1
$strTT1 3
,TT3 4
$strTT5 V
)TTV W
;TTW X
}UU 
}VV 
}WW 
catchXX 
(XX 
	ExceptionXX 
eXX 
)XX 
{YY 
}[[ 
return]] 
View]] 
(]] 
model]] 
)]] 
;]] 
}ss 	
[uu 	
HttpGetuu	 
]uu 
publicvv 
ActionResultvv 

(vv) *
)vv* +
{ww 	
Systemxx 
.xx 
Webxx 
.xx 
Securityxx 
.xx  
FormsAuthenticationxx  3
.xx3 4

(xxA B
UserxxB F
.xxF G
IdentityxxG O
.xxO P
NamexxP T
,xxT U
falsexxV [
)xx[ \
;xx\ ]
varyy 
datayy 
=yy 
newyy 
{yy 
	IsSuccessyy &
=yy' (
trueyy) -
}yy. /
;yy/ 0
returnzz 
Jsonzz 
(zz 
datazz 
,zz 
JsonRequestBehaviorzz 1
.zz1 2
AllowGetzz2 :
)zz: ;
;zz; <
}{{ 	
private}} 
async}} 
Task}} 
SignInAsync}} &
(}}& '
ApplicationUser}}' 6
user}}7 ;
,}}; <
bool}}= A
isPersistent}}B N
)}}N O
{~~ 	!
AuthenticationManager !
.! "
SignOut" )
() *&
DefaultAuthenticationTypes* D
.D E
ExternalCookieE S
)S T
;T U
var
�� 
identity
�� 
=
�� 
await
��  
UserManager
��! ,
.
��, -!
CreateIdentityAsync
��- @
(
��@ A
user
��A E
,
��E F(
DefaultAuthenticationTypes
��G a
.
��a b
ApplicationCookie
��b s
)
��s t
;
��t u#
AuthenticationManager
�� !
.
��! "
SignIn
��" (
(
��( )
new
��) ,&
AuthenticationProperties
��- E
(
��E F
)
��F G
{
��H I
IsPersistent
��J V
=
��W X
isPersistent
��Y e
}
��f g
,
��g h
identity
��i q
)
��q r
;
��r s
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '

VerifyCode
��( 2
(
��2 3
string
��3 9
provider
��: B
,
��B C
string
��D J
	returnUrl
��K T
,
��T U
bool
��V Z

rememberMe
��[ e
)
��e f
{
�� 	
if
�� 
(
�� 
!
�� 
await
�� 

�� $
.
��$ %"
HasBeenVerifiedAsync
��% 9
(
��9 :
)
��: ;
)
��; <
{
�� 
return
�� 
View
�� 
(
�� 
$str
�� #
)
��# $
;
��$ %
}
�� 
return
�� 
View
�� 
(
�� 
new
�� !
VerifyCodeViewModel
�� /
{
��0 1
Provider
��2 :
=
��; <
provider
��= E
,
��E F
	ReturnUrl
��G P
=
��Q R
	returnUrl
��S \
,
��\ ]

RememberMe
��^ h
=
��i j

rememberMe
��k u
}
��v w
)
��w x
;
��x y
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	
AllowAnonymous
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '

VerifyCode
��( 2
(
��2 3!
VerifyCodeViewModel
��3 F
model
��G L
)
��L M
{
�� 	
if
�� 
(
�� 
!
�� 

ModelState
�� 
.
�� 
IsValid
�� #
)
��# $
{
�� 
return
�� 
View
�� 
(
�� 
model
�� !
)
��! "
;
��" #
}
�� 
var
�� 
result
�� 
=
�� 
await
�� 

�� ,
.
��, -"
TwoFactorSignInAsync
��- A
(
��A B
model
��B G
.
��G H
Provider
��H P
,
��P Q
model
��R W
.
��W X
Code
��X \
,
��\ ]
isPersistent
��^ j
:
��j k
model
��m r
.
��r s

RememberMe
��s }
,
��} ~
rememberBrowser�� �
:��� �
model��� �
.��� �
RememberBrowser��� �
)��� �
;��� �
switch
�� 
(
�� 
result
�� 
)
�� 
{
�� 
case
�� 
SignInStatus
�� !
.
��! "
Success
��" )
:
��) *
return
�� 
RedirectToLocal
�� *
(
��* +
model
��+ 0
.
��0 1
	ReturnUrl
��1 :
)
��: ;
;
��; <
case
�� 
SignInStatus
�� !
.
��! "
	LockedOut
��" +
:
��+ ,
return
�� 
View
�� 
(
��  
$str
��  )
)
��) *
;
��* +
case
�� 
SignInStatus
�� !
.
��! "
Failure
��" )
:
��) *
default
�� 
:
�� 

ModelState
�� 
.
�� 

�� ,
(
��, -
$str
��- /
,
��/ 0
$str
��1 D
)
��D E
;
��E F
return
�� 
View
�� 
(
��  
model
��  %
)
��% &
;
��& '
}
�� 
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
ActionResult
�� 
Register
�� $
(
��$ %
)
��% &
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	
AllowAnonymous
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '
Register
��( 0
(
��0 1
RegisterViewModel
��1 B
model
��C H
)
��H I
{
�� 	
if
�� 
(
�� 

ModelState
�� 
.
�� 
IsValid
�� "
)
��" #
{
�� 
var
�� 
user
�� 
=
�� 
new
�� 
ApplicationUser
�� .
{
��/ 0
UserName
��1 9
=
��: ;
model
��< A
.
��A B
Email
��B G
,
��G H
Email
��I N
=
��O P
model
��Q V
.
��V W
Email
��W \
}
��] ^
;
��^ _
var
�� 
result
�� 
=
�� 
await
�� "
UserManager
��# .
.
��. /
CreateAsync
��/ :
(
��: ;
user
��; ?
,
��? @
model
��A F
.
��F G
Password
��G O
)
��O P
;
��P Q
if
�� 
(
�� 
result
�� 
.
�� 
	Succeeded
�� $
)
��$ %
{
�� 
await
�� 

�� '
.
��' (
SignInAsync
��( 3
(
��3 4
user
��4 8
,
��8 9
isPersistent
��: F
:
��F G
false
��G L
,
��L M
rememberBrowser
��N ]
:
��] ^
false
��^ c
)
��c d
;
��d e
return
�� 
RedirectToAction
�� +
(
��+ ,
$str
��, 3
,
��3 4
$str
��5 ;
)
��; <
;
��< =
}
�� 
	AddErrors
�� 
(
�� 
result
��  
)
��  !
;
��! "
}
�� 
return
�� 
View
�� 
(
�� 
model
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '
ConfirmEmail
��( 4
(
��4 5
string
��5 ;
userId
��< B
,
��B C
string
��D J
code
��K O
)
��O P
{
�� 	
if
�� 
(
�� 
userId
�� 
==
�� 
null
�� 
||
�� !
code
��" &
==
��' )
null
��* .
)
��. /
{
�� 
return
�� 
View
�� 
(
�� 
$str
�� #
)
��# $
;
��$ %
}
�� 
var
�� 
result
�� 
=
�� 
await
�� 
UserManager
�� *
.
��* +
ConfirmEmailAsync
��+ <
(
��< =
userId
��= C
,
��C D
code
��E I
)
��I J
;
��J K
return
�� 
View
�� 
(
�� 
result
�� 
.
�� 
	Succeeded
�� (
?
��) *
$str
��+ 9
:
��: ;
$str
��< C
)
��C D
;
��D E
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
ActionResult
�� 
ForgotPassword
�� *
(
��* +
)
��+ ,
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	
AllowAnonymous
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '
ForgotPassword
��( 6
(
��6 7%
ForgotPasswordViewModel
��7 N
model
��O T
)
��T U
{
�� 	
if
�� 
(
�� 

ModelState
�� 
.
�� 
IsValid
�� "
)
��" #
{
�� 
var
�� 
user
�� 
=
�� 
await
��  
UserManager
��! ,
.
��, -
FindByNameAsync
��- <
(
��< =
model
��= B
.
��B C
Email
��C H
)
��H I
;
��I J
if
�� 
(
�� 
user
�� 
==
�� 
null
��  
||
��! #
!
��$ %
(
��% &
await
��& +
UserManager
��, 7
.
��7 8#
IsEmailConfirmedAsync
��8 M
(
��M N
user
��N R
.
��R S
Id
��S U
)
��U V
)
��V W
)
��W X
{
�� 
return
�� 
View
�� 
(
��  
$str
��  <
)
��< =
;
��= >
}
�� 
}
�� 
return
�� 
View
�� 
(
�� 
model
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
ActionResult
�� (
ForgotPasswordConfirmation
�� 6
(
��6 7
)
��7 8
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
ActionResult
�� 

�� )
(
��) *
string
��* 0
code
��1 5
)
��5 6
{
�� 	
return
�� 
code
�� 
==
�� 
null
�� 
?
��  !
View
��" &
(
��& '
$str
��' .
)
��. /
:
��0 1
View
��2 6
(
��6 7
)
��7 8
;
��8 9
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	
AllowAnonymous
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '

��( 5
(
��5 6$
ResetPasswordViewModel
��6 L
model
��M R
)
��R S
{
�� 	
if
�� 
(
�� 
!
�� 

ModelState
�� 
.
�� 
IsValid
�� #
)
��# $
{
�� 
return
�� 
View
�� 
(
�� 
model
�� !
)
��! "
;
��" #
}
�� 
var
�� 
user
�� 
=
�� 
await
�� 
UserManager
�� (
.
��( )
FindByNameAsync
��) 8
(
��8 9
model
��9 >
.
��> ?
Email
��? D
)
��D E
;
��E F
if
�� 
(
�� 
user
�� 
==
�� 
null
�� 
)
�� 
{
�� 
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( C
,
��C D
$str
��E N
)
��N O
;
��O P
}
�� 
var
�� 
result
�� 
=
�� 
await
�� 
UserManager
�� *
.
��* + 
ResetPasswordAsync
��+ =
(
��= >
user
��> B
.
��B C
Id
��C E
,
��E F
model
��G L
.
��L M
Code
��M Q
,
��Q R
model
��S X
.
��X Y
Password
��Y a
)
��a b
;
��b c
if
�� 
(
�� 
result
�� 
.
�� 
	Succeeded
��  
)
��  !
{
�� 
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( C
,
��C D
$str
��E N
)
��N O
;
��O P
}
�� 
	AddErrors
�� 
(
�� 
result
�� 
)
�� 
;
�� 
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
ActionResult
�� '
ResetPasswordConfirmation
�� 5
(
��5 6
)
��6 7
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	
AllowAnonymous
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
ActionResult
�� 

�� )
(
��) *
string
��* 0
provider
��1 9
,
��9 :
string
��; A
	returnUrl
��B K
)
��K L
{
�� 	
return
�� 
new
�� 
ChallengeResult
�� &
(
��& '
provider
��' /
,
��/ 0
Url
��1 4
.
��4 5
Action
��5 ;
(
��; <
$str
��< S
,
��S T
$str
��U ^
,
��^ _
new
��` c
{
��d e
	ReturnUrl
��f o
=
��p q
	returnUrl
��r {
}
��| }
)
��} ~
)
��~ 
;�� �
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '
SendCode
��( 0
(
��0 1
string
��1 7
	returnUrl
��8 A
,
��A B
bool
��C G

rememberMe
��H R
)
��R S
{
�� 	
var
�� 
userId
�� 
=
�� 
await
�� 

�� ,
.
��, -$
GetVerifiedUserIdAsync
��- C
(
��C D
)
��D E
;
��E F
if
�� 
(
�� 
userId
�� 
==
�� 
null
�� 
)
�� 
{
�� 
return
�� 
View
�� 
(
�� 
$str
�� #
)
��# $
;
��$ %
}
�� 
var
�� 
userFactors
�� 
=
�� 
await
�� #
UserManager
��$ /
.
��/ 0-
GetValidTwoFactorProvidersAsync
��0 O
(
��O P
userId
��P V
)
��V W
;
��W X
var
�� 

�� 
=
�� 
userFactors
��  +
.
��+ ,
Select
��, 2
(
��2 3
purpose
��3 :
=>
��; =
new
��> A
SelectListItem
��B P
{
��Q R
Text
��S W
=
��X Y
purpose
��Z a
,
��a b
Value
��c h
=
��i j
purpose
��k r
}
��s t
)
��t u
.
��u v
ToList
��v |
(
��| }
)
��} ~
;
��~ 
return
�� 
View
�� 
(
�� 
new
�� 
SendCodeViewModel
�� -
{
��. /
	Providers
��0 9
=
��: ;

��< I
,
��I J
	ReturnUrl
��K T
=
��U V
	returnUrl
��W `
,
��` a

RememberMe
��b l
=
��m n

rememberMe
��o y
}
��z {
)
��{ |
;
��| }
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	
AllowAnonymous
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '
SendCode
��( 0
(
��0 1
SendCodeViewModel
��1 B
model
��C H
)
��H I
{
�� 	
if
�� 
(
�� 
!
�� 

ModelState
�� 
.
�� 
IsValid
�� #
)
��# $
{
�� 
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 
if
�� 
(
�� 
!
�� 
await
�� 

�� $
.
��$ %$
SendTwoFactorCodeAsync
��% ;
(
��; <
model
��< A
.
��A B
SelectedProvider
��B R
)
��R S
)
��S T
{
�� 
return
�� 
View
�� 
(
�� 
$str
�� #
)
��# $
;
��$ %
}
�� 
return
�� 
RedirectToAction
�� #
(
��# $
$str
��$ 0
,
��0 1
new
��2 5
{
��6 7
Provider
��8 @
=
��A B
model
��C H
.
��H I
SelectedProvider
��I Y
,
��Y Z
	ReturnUrl
��[ d
=
��e f
model
��g l
.
��l m
	ReturnUrl
��m v
,
��v w

RememberMe��x �
=��� �
model��� �
.��� �

RememberMe��� �
}��� �
)��� �
;��� �
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '#
ExternalLoginCallback
��( =
(
��= >
string
��> D
	returnUrl
��E N
)
��N O
{
�� 	
var
�� 
	loginInfo
�� 
=
�� 
await
�� !#
AuthenticationManager
��" 7
.
��7 8'
GetExternalLoginInfoAsync
��8 Q
(
��Q R
)
��R S
;
��S T
if
�� 
(
�� 
	loginInfo
�� 
==
�� 
null
�� !
)
��! "
{
�� 
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( /
)
��/ 0
;
��0 1
}
�� 
var
�� 
result
�� 
=
�� 
await
�� 

�� ,
.
��, -!
ExternalSignInAsync
��- @
(
��@ A
	loginInfo
��A J
,
��J K
isPersistent
��L X
:
��X Y
false
��Z _
)
��_ `
;
��` a
switch
�� 
(
�� 
result
�� 
)
�� 
{
�� 
case
�� 
SignInStatus
�� !
.
��! "
Success
��" )
:
��) *
return
�� 
RedirectToLocal
�� *
(
��* +
	returnUrl
��+ 4
)
��4 5
;
��5 6
case
�� 
SignInStatus
�� !
.
��! "
	LockedOut
��" +
:
��+ ,
return
�� 
View
�� 
(
��  
$str
��  )
)
��) *
;
��* +
case
�� 
SignInStatus
�� !
.
��! ""
RequiresVerification
��" 6
:
��6 7
return
�� 
RedirectToAction
�� +
(
��+ ,
$str
��, 6
,
��6 7
new
��8 ;
{
��< =
	ReturnUrl
��> G
=
��H I
	returnUrl
��J S
,
��S T

RememberMe
��U _
=
��` a
false
��b g
}
��h i
)
��i j
;
��j k
case
�� 
SignInStatus
�� !
.
��! "
Failure
��" )
:
��) *
default
�� 
:
�� 
ViewBag
�� 
.
�� 
	ReturnUrl
�� %
=
��& '
	returnUrl
��( 1
;
��1 2
ViewBag
�� 
.
�� 

�� )
=
��* +
	loginInfo
��, 5
.
��5 6
Login
��6 ;
.
��; <

��< I
;
��I J
return
�� 
View
�� 
(
��  
$str
��  ;
,
��; <
new
��= @0
"ExternalLoginConfirmationViewModel
��A c
{
��d e
Email
��f k
=
��l m
	loginInfo
��n w
.
��w x
Email
��x }
}
��~ 
)�� �
;��� �
}
�� 
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	
AllowAnonymous
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& ''
ExternalLoginConfirmation
��( A
(
��A B0
"ExternalLoginConfirmationViewModel
��B d
model
��e j
,
��j k
string
��l r
	returnUrl
��s |
)
��| }
{
�� 	
if
�� 
(
�� 
User
�� 
.
�� 
Identity
�� 
.
�� 
IsAuthenticated
�� -
)
��- .
{
�� 
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( /
,
��/ 0
$str
��1 9
)
��9 :
;
��: ;
}
�� 
if
�� 
(
�� 

ModelState
�� 
.
�� 
IsValid
�� "
)
��" #
{
�� 
var
�� 
info
�� 
=
�� 
await
��  #
AuthenticationManager
��! 6
.
��6 7'
GetExternalLoginInfoAsync
��7 P
(
��P Q
)
��Q R
;
��R S
if
�� 
(
�� 
info
�� 
==
�� 
null
��  
)
��  !
{
�� 
return
�� 
View
�� 
(
��  
$str
��  6
)
��6 7
;
��7 8
}
�� 
var
�� 
user
�� 
=
�� 
new
�� 
ApplicationUser
�� .
{
��/ 0
UserName
��1 9
=
��: ;
model
��< A
.
��A B
Email
��B G
,
��G H
Email
��I N
=
��O P
model
��Q V
.
��V W
Email
��W \
}
��] ^
;
��^ _
var
�� 
result
�� 
=
�� 
await
�� "
UserManager
��# .
.
��. /
CreateAsync
��/ :
(
��: ;
user
��; ?
)
��? @
;
��@ A
if
�� 
(
�� 
result
�� 
.
�� 
	Succeeded
�� $
)
��$ %
{
�� 
result
�� 
=
�� 
await
�� "
UserManager
��# .
.
��. /

��/ <
(
��< =
user
��= A
.
��A B
Id
��B D
,
��D E
info
��F J
.
��J K
Login
��K P
)
��P Q
;
��Q R
if
�� 
(
�� 
result
�� 
.
�� 
	Succeeded
�� (
)
��( )
{
�� 
await
�� 

�� +
.
��+ ,
SignInAsync
��, 7
(
��7 8
user
��8 <
,
��< =
isPersistent
��> J
:
��J K
false
��L Q
,
��Q R
rememberBrowser
��S b
:
��b c
false
��d i
)
��i j
;
��j k
return
�� 
RedirectToLocal
�� .
(
��. /
	returnUrl
��/ 8
)
��8 9
;
��9 :
}
�� 
}
�� 
	AddErrors
�� 
(
�� 
result
��  
)
��  !
;
��! "
}
�� 
ViewBag
�� 
.
�� 
	ReturnUrl
�� 
=
�� 
	returnUrl
��  )
;
��) *
return
�� 
View
�� 
(
�� 
model
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
ActionResult
�� 
LogOff
�� "
(
��" #
)
��# $
{
�� 	#
AuthenticationManager
�� !
.
��! "
SignOut
��" )
(
��) *(
DefaultAuthenticationTypes
��* D
.
��D E
ApplicationCookie
��E V
)
��V W
;
��W X
return
�� 
RedirectToAction
�� #
(
��# $
$str
��$ +
,
��+ ,
$str
��- 6
)
��6 7
;
��7 8
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
ActionResult
�� "
ExternalLoginFailure
�� 0
(
��0 1
)
��1 2
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
	protected
�� 
override
�� 
void
�� 
Dispose
��  '
(
��' (
bool
��( ,
	disposing
��- 6
)
��6 7
{
�� 	
if
�� 
(
�� 
	disposing
�� 
)
�� 
{
�� 
if
�� 
(
�� 
_userManager
��  
!=
��! #
null
��$ (
)
��( )
{
�� 
_userManager
��  
.
��  !
Dispose
��! (
(
��( )
)
��) *
;
��* +
_userManager
��  
=
��! "
null
��# '
;
��' (
}
�� 
if
�� 
(
�� 
_signInManager
�� "
!=
��# %
null
��& *
)
��* +
{
�� 
_signInManager
�� "
.
��" #
Dispose
��# *
(
��* +
)
��+ ,
;
��, -
_signInManager
�� "
=
��# $
null
��% )
;
��) *
}
�� 
}
�� 
base
�� 
.
�� 
Dispose
�� 
(
�� 
	disposing
�� "
)
��" #
;
��# $
}
�� 	
private
�� 
const
�� 
string
�� 
XsrfKey
�� $
=
��% &
$str
��' /
;
��/ 0
private
�� $
IAuthenticationManager
�� &#
AuthenticationManager
��' <
{
�� 	
get
�� 
{
�� 
return
�� 
HttpContext
�� "
.
��" #
GetOwinContext
��# 1
(
��1 2
)
��2 3
.
��3 4
Authentication
��4 B
;
��B C
}
�� 
}
�� 	
private
�� 
void
�� 
	AddErrors
�� 
(
�� 
IdentityResult
�� -
result
��. 4
)
��4 5
{
�� 	
foreach
�� 
(
�� 
var
�� 
error
�� 
in
�� !
result
��" (
.
��( )
Errors
��) /
)
��/ 0
{
�� 

ModelState
�� 
.
�� 

�� (
(
��( )
$str
��) +
,
��+ ,
error
��- 2
)
��2 3
;
��3 4
}
�� 
}
�� 	
private
�� 
ActionResult
�� 
RedirectToLocal
�� ,
(
��, -
string
��- 3
	returnUrl
��4 =
)
��= >
{
�� 	
if
�� 
(
�� 
Url
�� 
.
�� 

IsLocalUrl
�� 
(
�� 
	returnUrl
�� (
)
��( )
)
��) *
{
�� 
return
�� 
Redirect
�� 
(
��  
	returnUrl
��  )
)
��) *
;
��* +
}
�� 
return
�� 
RedirectToAction
�� #
(
��# $
$str
��$ 1
,
��1 2
$str
��3 ?
)
��? @
;
��@ A
}
�� 	
private
�� 
ActionResult
�� 3
%RedirectToSeleccionarCentroDeAtencion
�� B
(
��B C
string
��C I
	returnUrl
��J S
)
��S T
{
�� 	
if
�� 
(
�� 
Url
�� 
.
�� 

IsLocalUrl
�� 
(
�� 
	returnUrl
�� (
)
��( )
)
��) *
{
�� 
return
�� 
Redirect
�� 
(
��  
	returnUrl
��  )
)
��) *
;
��* +
}
�� 
return
�� 
RedirectToAction
�� #
(
��# $
$str
��$ A
,
��A B
$str
��C U
)
��U V
;
��V W
}
�� 	
internal
�� 
class
�� 
ChallengeResult
�� &
:
��' ($
HttpUnauthorizedResult
��) ?
{
�� 	
public
�� 
ChallengeResult
�� "
(
��" #
string
��# )
provider
��* 2
,
��2 3
string
��4 :
redirectUri
��; F
)
��F G
:
�� 
this
�� 
(
�� 
provider
�� 
,
��  
redirectUri
��! ,
,
��, -
null
��. 2
)
��2 3
{
�� 
}
�� 
public
�� 
ChallengeResult
�� "
(
��" #
string
��# )
provider
��* 2
,
��2 3
string
��4 :
redirectUri
��; F
,
��F G
string
��H N
userId
��O U
)
��U V
{
�� 

�� 
=
�� 
provider
��  (
;
��( )
RedirectUri
�� 
=
�� 
redirectUri
�� )
;
��) *
UserId
�� 
=
�� 
userId
�� 
;
��  
}
�� 
public
�� 
string
�� 

�� '
{
��( )
get
��* -
;
��- .
set
��/ 2
;
��2 3
}
��4 5
public
�� 
string
�� 
RedirectUri
�� %
{
��& '
get
��( +
;
��+ ,
set
��- 0
;
��0 1
}
��2 3
public
�� 
string
�� 
UserId
��  
{
��! "
get
��# &
;
��& '
set
��( +
;
��+ ,
}
��- .
public
�� 
override
�� 
void
��  

��! .
(
��. /
ControllerContext
��/ @
context
��A H
)
��H I
{
�� 
var
�� 

properties
�� 
=
��  
new
��! $&
AuthenticationProperties
��% =
{
��> ?
RedirectUri
��@ K
=
��L M
RedirectUri
��N Y
}
��Z [
;
��[ \
if
�� 
(
�� 
UserId
�� 
!=
�� 
null
�� "
)
��" #
{
�� 

properties
�� 
.
�� 

Dictionary
�� )
[
��) *
XsrfKey
��* 1
]
��1 2
=
��3 4
UserId
��5 ;
;
��; <
}
�� 
context
�� 
.
�� 
HttpContext
�� #
.
��# $
GetOwinContext
��$ 2
(
��2 3
)
��3 4
.
��4 5
Authentication
��5 C
.
��C D
	Challenge
��D M
(
��M N

properties
��N X
,
��X Y

��Z g
)
��g h
;
��h i
}
�� 
}
�� 	
}
�� 
}�� �$
VD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\ActorBaseController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Areas 
. 
Administracion +
.+ ,
Controllers, 7
{		 
public

 

class

 
ActorBaseController

 $
:

% &
BaseController

' 5
{ 
public
DataActorApi
ObtenerActorRucApi
(
string
numeroDocumento
)
{ 	
DataActorApi 
dataActorApi %
=& '
new( +
DataActorApi, 8
(8 9
)9 :
;: ;
try 
{ 
string 
url 
= 
AplicacionSettings /
./ 0
Default0 7
.7 8"
UrlApiConsultaActorRuc8 N
+O P
numeroDocumentoQ `
;` a
var 
t 
= 
Task 
. 
Run  
(  !
(! "
)" #
=>$ &
GetURI' -
(- .
new. 1
Uri2 5
(5 6
url6 9
)9 :
): ;
); <
;< =
t 
. 
Wait 
( 
) 
; 
string 
result 
= 
t  !
.! "
Result" (
;( )
dataActorApi 
= 

Newtonsoft )
.) *
Json* .
.. /
JsonConvert/ :
.: ;
DeserializeObject; L
<L M
DataActorApiM Y
>Y Z
(Z [
result[ a
)a b
;b c
if 
( 
dataActorApi  
.  !
Estado! '
==( *
false+ 0
)0 1
{ 
throw 
new 
ControllerException 1
(1 2
$str2 ]
)] ^
;^ _
} 
return 
dataActorApi #
;# $
} 
catch 
( 
	Exception 
e 
) 
{ 
throw 
new 
ControllerException -
(- .
$str. R
,R S
eT U
)U V
;V W
}   
}!! 	
public## 
DataActorApi## 
ObtenerActorDniApi## .
(##. /
string##/ 5
numeroDocumento##6 E
)##E F
{$$ 	
var%% 
dataActorApi%% 
=%% 
new%% "
DataActorApi%%# /
(%%/ 0
)%%0 1
;%%1 2
try&& 
{'' 
string(( 
url(( 
=(( 
AplicacionSettings(( /
.((/ 0
Default((0 7
.((7 8"
UrlApiConsultaActorDni((8 N
+((O P
numeroDocumento((Q `
;((` a
var)) 
t)) 
=)) 
Task)) 
.)) 
Run))  
())  !
())! "
)))" #
=>))$ &
GetURI))' -
())- .
new)). 1
Uri))2 5
())5 6
url))6 9
)))9 :
))): ;
))); <
;))< =
t** 
.** 
Wait** 
(** 
)** 
;** 
string++ 
result++ 
=++ 
t++  !
.++! "
Result++" (
;++( )
dataActorApi,, 
=,, 

Newtonsoft,, )
.,,) *
Json,,* .
.,,. /
JsonConvert,,/ :
.,,: ;
DeserializeObject,,; L
<,,L M
DataActorApi,,M Y
>,,Y Z
(,,Z [
result,,[ a
),,a b
;,,b c
if-- 
(-- 
dataActorApi--  
.--  !
Estado--! '
==--( *
false--+ 0
)--0 1
{.. 
throw// 
new// 
ControllerException// 1
(//1 2
$str//2 ]
)//] ^
;//^ _
}00 
return11 
dataActorApi11 #
;11# $
}22 
catch33 
(33 
	Exception33 
e33 
)33 
{44 
throw55 
new55 
ControllerException55 -
(55- .
$str55. R
,55R S
e55T U
)55U V
;55V W
}66 
}77 	
}88 
}99 ��
RD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\AdminController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class 
AdminController  
:! "
ActorBaseController# 6
{ 
public 
AdminController 
( 
)  
{ 	
} 	
private   "
ApplicationUserManager   &
_userManager  ' 3
;  3 4
private!! "
ApplicationRoleManager!! &
_roleManager!!' 3
;!!3 4
[&& 	
	Authorize&&	 
(&& 
Roles&& 
=&& 
$str&& ,
)&&, -
]&&- .
public(( 
ActionResult(( 
Index(( !
(((! "
)((" #
{)) 	
return** 
View** 
(** 
)** 
;** 
}++ 	
publicrr 

JsonResultrr 
ListarUsuariosrr (
(rr( )
)rr) *
{ss 	
trytt 
{uu 
Listvv 
<vv 
ExpandedUserDTOvv $
>vv$ %
col_UserDTOvv& 1
=vv2 3
newvv4 7
Listvv8 <
<vv< =
ExpandedUserDTOvv= L
>vvL M
(vvM N
)vvN O
;vvO P
varxx 
resultxx 
=xx 
UserManagerxx (
.xx( )
Usersxx) .
.xx. /
ToListxx/ 5
(xx5 6
)xx6 7
;xx7 8
foreachzz 
(zz 
varzz 
itemzz !
inzz" $
resultzz% +
)zz+ ,
{{{ 
ExpandedUserDTO|| #

objUserDTO||$ .
=||/ 0
new||1 4
ExpandedUserDTO||5 D
(||D E
)||E F
;||F G

objUserDTO~~ 
.~~ 
	IdUsuario~~ (
=~~) *
item~~+ /
.~~/ 0
Id~~0 2
;~~2 3

objUserDTO 
. 
UserName '
=( )
item* .
.. /
UserName/ 7
;7 8

objUserDTO
�� 
.
�� 
Email
�� $
=
��% &
item
��' +
.
��+ ,
Email
��, 1
;
��1 2

objUserDTO
�� 
.
�� 
LockoutEndDateUtc
�� 0
=
��1 2
item
��3 7
.
��7 8
LockoutEndDateUtc
��8 I
;
��I J

objUserDTO
�� 
.
��  
EsUsuarioPrincipal
�� 1
=
��2 3
item
��4 8
.
��8 9
UserName
��9 A
.
��A B
ToLower
��B I
(
��I J
)
��J K
!=
��L N
this
��O S
.
��S T
User
��T X
.
��X Y
Identity
��Y a
.
��a b
Name
��b f
.
��f g
ToLower
��g n
(
��n o
)
��o p
?
��q r
true
��s w
:
��x y
false
��z 
;�� �

objUserDTO
�� 
.
�� 
NombresRoles
�� +
=
��, -%
ObtenerNombreDeLosRoles
��. E
(
��E F
item
��F J
.
��J K
Roles
��K P
)
��P Q
;
��Q R
col_UserDTO
�� 
.
��  
Add
��  #
(
��# $

objUserDTO
��$ .
)
��. /
;
��/ 0
}
�� 
return
�� 
Json
�� 
(
�� 
col_UserDTO
�� '
)
��' (
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

ModelState
�� 
.
�� 

�� (
(
��( )
string
��) /
.
��/ 0
Empty
��0 5
,
��5 6
$str
��7 @
+
��A B
ex
��C E
)
��E F
;
��F G
List
�� 
<
�� 
ExpandedUserDTO
�� $
>
��$ %
col_UserDTO
��& 1
=
��2 3
new
��4 7
List
��8 <
<
��< =
ExpandedUserDTO
��= L
>
��L M
(
��M N
)
��N O
;
��O P
return
�� 
Json
�� 
(
�� 
col_UserDTO
�� '
)
��' (
;
��( )
}
�� 
}
�� 	
public
�� 
string
�� %
ObtenerNombreDeLosRoles
�� -
(
��- .
IEnumerable
��. 9
<
��9 :
IdentityUserRole
��: J
>
��J K

��L Y
)
��Y Z
{
�� 	
List
�� 
<
�� 
string
�� 
>
�� 
ListOfRoleNames
�� (
=
��) *
new
��+ .
List
��/ 3
<
��3 4
string
��4 :
>
��: ;
(
��; <
)
��< =
;
��= >
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  

��! .
)
��. /
{
�� 
string
�� 
rolename
�� 
=
��  !
RoleManager
��" -
.
��- .
FindById
��. 6
(
��6 7
item
��7 ;
.
��; <
RoleId
��< B
)
��B C
.
��C D
Name
��D H
;
��H I
ListOfRoleNames
�� 
.
��  
Add
��  #
(
��# $
rolename
��$ ,
)
��, -
;
��- .
}
�� 
string
�� 
nombresRoles
�� 
=
��  !
string
��" (
.
��( )
Join
��) -
(
��- .
$str
��. 2
,
��2 3
ListOfRoleNames
��4 C
.
��C D
ToArray
��D K
(
��K L
)
��L M
)
��M N
;
��N O
return
�� 
nombresRoles
�� 
;
��  
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 
ActionResult
�� 
Create
�� "
(
��" #
)
��# $
{
�� 	
return
�� 
View
�� 
(
�� 
)
��. /
;
��/ 0
}
�� 	
public
�� 

JsonResult
�� 
GuardarUsuario
�� (
(
��( )
ExpandedUserDTO
��) 8"
paramExpandedUserDTO
��9 M
)
��M N
{
�� 	
try
�� 
{
�� 
var
�� 
Email
�� 
=
�� "
paramExpandedUserDTO
�� 0
.
��0 1
Email
��1 6
.
��6 7
Trim
��7 ;
(
��; <
)
��< =
;
��= >
var
�� 
UserName
�� 
=
�� "
paramExpandedUserDTO
�� 3
.
��3 4
Email
��4 9
.
��9 :
Trim
��: >
(
��> ?
)
��? @
;
��@ A
var
�� 
Password
�� 
=
�� "
paramExpandedUserDTO
�� 3
.
��3 4
Password
��4 <
.
��< =
Trim
��= A
(
��A B
)
��B C
;
��C D
if
�� 
(
�� 
Email
�� 
==
�� 
$str
�� 
)
��  
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( 2
)
��2 3
;
��3 4
}
�� 
if
�� 
(
�� 
Password
�� 
==
�� 
$str
��  "
)
��" #
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( 5
)
��5 6
;
��6 7
}
�� 
UserName
�� 
=
�� 
Email
��  
.
��  !
ToLower
��! (
(
��( )
)
��) *
;
��* +
var
�� 
objNewAdminUser
�� #
=
��$ %
new
��& )
ApplicationUser
��* 9
{
��: ;
UserName
��< D
=
��E F
UserName
��G O
,
��O P
Email
��Q V
=
��W X
Email
��Y ^
}
��_ `
;
��` a
var
�� #
AdminUserCreateResult
�� )
=
��* +
UserManager
��, 7
.
��7 8
Create
��8 >
(
��> ?
objNewAdminUser
��? N
,
��N O
Password
��P X
)
��X Y
;
��Y Z
if
�� 
(
�� #
AdminUserCreateResult
�� )
.
��) *
	Succeeded
��* 3
)
��3 4
{
�� 
string
�� 

strNewRole
�� %
=
��& '"
paramExpandedUserDTO
��( <
.
��< =
Roles
��= B
.
��B C
First
��C H
(
��H I
)
��I J
.
��J K
RoleName
��K S
;
��S T
if
�� 
(
�� 

strNewRole
�� "
!=
��# %
$str
��& )
)
��) *
{
�� 
UserManager
�� #
.
��# $
	AddToRole
��$ -
(
��- .
objNewAdminUser
��. =
.
��= >
Id
��> @
,
��@ A

strNewRole
��B L
)
��L M
;
��M N
}
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� 3
(
��3 4
new
��4 7
{
��8 9
code_result
��: E
=
��F G!
OperationResultEnum
��H [
.
��[ \
Success
��\ c
,
��c d
data
��e i
=
��j k
$str
��l p
,
��p q!
result_description��r �
=��� �
$str��� �
+��� �
Email��� �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
else
�� 
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� 3
(
��3 4
Util
��4 8
.
��8 9
	ErrorJson
��9 B
(
��B C
new
��C F
	Exception
��G P
(
��P Q
$str
��Q t
+
��u v
Email
��w |
+
��} ~
$str�� �
+��� �%
AdminUserCreateResult��� �
.��� �
Errors��� �
.��� �
First��� �
(��� �
)��� �
.��� �
ToString��� �
(��� �
)��� �
)��� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M u
,
��u v
e
��w x
)
��x y
)
��y z
,
��z {
HttpStatusCode��| �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 
OperationResult
�� 
GuardarUsuario_
�� .
(
��. /
ExpandedUserDTO
��/ >"
paramExpandedUserDTO
��? S
)
��S T
{
�� 	
try
�� 
{
�� 
var
�� 
Email
�� 
=
�� "
paramExpandedUserDTO
�� 0
.
��0 1
Email
��1 6
.
��6 7
Trim
��7 ;
(
��; <
)
��< =
;
��= >
var
�� 
UserName
�� 
=
�� "
paramExpandedUserDTO
�� 3
.
��3 4
Email
��4 9
.
��9 :
Trim
��: >
(
��> ?
)
��? @
;
��@ A
var
�� 
Password
�� 
=
�� "
paramExpandedUserDTO
�� 3
.
��3 4
Password
��4 <
.
��< =
Trim
��= A
(
��A B
)
��B C
;
��C D
if
�� 
(
�� 
Email
�� 
==
�� 
$str
�� 
)
��  
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( 2
)
��2 3
;
��3 4
}
�� 
if
�� 
(
�� 
Password
�� 
==
�� 
$str
��  "
)
��" #
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( 5
)
��5 6
;
��6 7
}
�� 
UserName
�� 
=
�� 
Email
��  
.
��  !
ToLower
��! (
(
��( )
)
��) *
;
��* +
var
�� 
objNewAdminUser
�� #
=
��$ %
new
��& )
ApplicationUser
��* 9
{
��: ;
UserName
��< D
=
��E F
UserName
��G O
,
��O P
Email
��Q V
=
��W X
Email
��Y ^
}
��_ `
;
��` a
var
�� #
AdminUserCreateResult
�� )
=
��* +
UserManager
��, 7
.
��7 8
Create
��8 >
(
��> ?
objNewAdminUser
��? N
,
��N O
Password
��P X
)
��X Y
;
��Y Z
if
�� 
(
�� #
AdminUserCreateResult
�� )
.
��) *
	Succeeded
��* 3
==
��4 6
true
��7 ;
)
��; <
{
�� 
foreach
�� 
(
�� 
var
�� 
item
��  $
in
��% '"
paramExpandedUserDTO
��( <
.
��< =
Roles
��= B
)
��B C
{
�� 
UserManager
�� #
.
��# $
	AddToRole
��$ -
(
��- .
objNewAdminUser
��. =
.
��= >
Id
��> @
,
��@ A
item
��B F
.
��F G
RoleName
��G O
)
��O P
;
��P Q
}
�� 
}
�� 
return
�� 
new
�� 
OperationResult
�� *
(
��* +
objNewAdminUser
��+ :
.
��: ;
Id
��; =
,
��= >!
OperationResultEnum
��> Q
.
��Q R
Success
��R Y
,
��Y Z
$str
��[ ~
+�� �
Email��� �
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
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 
ActionResult
�� 
EditUser
�� $
(
��$ %
string
��% +
UserName
��, 4
)
��4 5
{
�� 	
if
�� 
(
�� 
UserName
�� 
==
�� 
null
��  
)
��  !
{
�� 
return
�� 
new
�� "
HttpStatusCodeResult
�� /
(
��/ 0
HttpStatusCode
��0 >
.
��> ?

BadRequest
��? I
)
��I J
;
��J K
}
�� 
ExpandedUserDTO
��  
objExpandedUserDTO
�� .
=
��/ 0
GetUser
��1 8
(
��8 9
UserName
��9 A
)
��A B
;
��B C
if
�� 
(
��  
objExpandedUserDTO
�� "
==
��# %
null
��& *
)
��* +
{
�� 
return
�� 
HttpNotFound
�� #
(
��# $
)
��$ %
;
��% &
}
�� 
return
�� 
View
�� 
(
��  
objExpandedUserDTO
�� *
)
��* +
;
��+ ,
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
ActionResult
�� 
EditUser
�� $
(
��$ %
ExpandedUserDTO
��% 4"
paramExpandedUserDTO
��5 I
)
��I J
{
�� 	
try
�� 
{
�� 
if
�� 
(
�� "
paramExpandedUserDTO
�� (
==
��) +
null
��, 0
)
��0 1
{
�� 
return
�� 
new
�� "
HttpStatusCodeResult
�� 3
(
��3 4
HttpStatusCode
��4 B
.
��B C

BadRequest
��C M
)
��M N
;
��N O
}
�� 
ExpandedUserDTO
��  
objExpandedUserDTO
��  2
=
��3 4

��5 B
(
��B C"
paramExpandedUserDTO
��C W
)
��W X
;
��X Y
if
�� 
(
��  
objExpandedUserDTO
�� &
==
��' )
null
��* .
)
��. /
{
�� 
return
�� 
HttpNotFound
�� '
(
��' (
)
��( )
;
��) *
}
�� 
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( /
)
��/ 0
;
��0 1
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

ModelState
�� 
.
�� 

�� (
(
��( )
string
��) /
.
��/ 0
Empty
��0 5
,
��5 6
$str
��7 @
+
��A B
ex
��C E
)
��E F
;
��F G
return
�� 
View
�� 
(
�� 
$str
�� &
,
��& '
GetUser
��( /
(
��/ 0"
paramExpandedUserDTO
��0 D
.
��D E
UserName
��E M
)
��M N
)
��N O
;
��O P
}
�� 
}
�� 	
public
�� 
OperationResult
�� 

�� ,
(
��, -
ExpandedUserDTO
��- <"
paramExpandedUserDTO
��= Q
)
��Q R
{
�� 	
try
�� 
{
�� 
ExpandedUserDTO
��  
objExpandedUserDTO
��  2
=
��3 4

��5 B
(
��B C"
paramExpandedUserDTO
��C W
)
��W X
;
��X Y
ApplicationUser
�� 
user
��  $
=
��% &
UserManager
��' 2
.
��2 3

FindByName
��3 =
(
��= >"
paramExpandedUserDTO
��> R
.
��R S
UserName
��S [
)
��[ \
;
��\ ]
foreach
�� 
(
�� 
var
�� 
item
�� !
in
��" $"
paramExpandedUserDTO
��% 9
.
��9 :
Roles
��: ?
)
��? @
{
�� 
UserManager
�� 
.
��  
	AddToRole
��  )
(
��) *
user
��* .
.
��. /
Id
��/ 1
,
��1 2
item
��3 7
.
��7 8
RoleName
��8 @
)
��@ A
;
��A B
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
��H y
+
��z {#
paramExpandedUserDTO��| �
.��� �
Email��� �
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
return
�� 
new
�� 
OperationResult
�� *
(
��* +
e
��+ ,
)
��, -
;
��- .
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 

JsonResult
�� 

DeleteUser
�� $
(
��$ %
string
��% +
UserName
��, 4
)
��4 5
{
�� 	
try
�� 
{
�� 
if
�� 
(
�� 
UserName
�� 
.
�� 
ToLower
�� $
(
��$ %
)
��% &
==
��' )
this
��* .
.
��. /
User
��/ 3
.
��3 4
Identity
��4 <
.
��< =
Name
��= A
.
��A B
ToLower
��B I
(
��I J
)
��J K
)
��K L
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( W
)
��W X
;
��X Y
}
�� 
ExpandedUserDTO
��  
objExpandedUserDTO
��  2
=
��3 4
GetUser
��5 <
(
��< =
UserName
��= E
)
��E F
;
��F G
if
�� 
(
��  
objExpandedUserDTO
�� &
==
��' )
null
��* .
)
��. /
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( ?
)
��? @
;
��@ A
}
�� 
else
�� 
{
�� 

DeleteUser
�� 
(
��  
objExpandedUserDTO
�� 1
)
��1 2
;
��2 3
}
�� 
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
SuccessJson
��! ,
(
��, -
$str
��- D
)
��D E
)
��E F
;
��F G
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
HttpContext
�� 
.
�� 
Response
�� $
.
��$ %

StatusCode
��% /
=
��0 1
$num
��2 5
;
��5 6
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
ex
��+ -
)
��- .
)
��. /
;
��/ 0
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 
ActionResult
�� 
	EditRoles
�� %
(
��% &
string
��& ,
UserName
��- 5
)
��5 6
{
�� 	
if
�� 
(
�� 
UserName
�� 
==
�� 
null
��  
)
��  !
{
�� 
return
�� 
new
�� "
HttpStatusCodeResult
�� /
(
��/ 0
HttpStatusCode
��0 >
.
��> ?

BadRequest
��? I
)
��I J
;
��J K
}
�� 
UserName
�� 
=
�� 
UserName
�� 
.
��  
ToLower
��  '
(
��' (
)
��( )
;
��) *
ExpandedUserDTO
��  
objExpandedUserDTO
�� .
=
��/ 0
GetUser
��1 8
(
��8 9
UserName
��9 A
)
��A B
;
��B C
if
�� 
(
��  
objExpandedUserDTO
�� "
==
��# %
null
��& *
)
��* +
{
�� 
return
�� 
HttpNotFound
�� #
(
��# $
)
��$ %
;
��% &
}
�� 
UserAndRolesDTO
��  
objUserAndRolesDTO
�� .
=
��/ 0
GetUserAndRoles
�� 
(
��  
UserName
��  (
)
��( )
;
��) *
return
�� 
View
�� 
(
��  
objUserAndRolesDTO
�� *
)
��* +
;
��+ ,
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
[
�� 	
HttpPost
��	 
]
�� 
[
�� 	&
ValidateAntiForgeryToken
��	 !
]
��! "
public
�� 
ActionResult
�� 
	EditRoles
�� %
(
��% &
UserAndRolesDTO
��& 5"
paramUserAndRolesDTO
��6 J
)
��J K
{
�� 	
try
�� 
{
�� 
if
�� 
(
�� "
paramUserAndRolesDTO
�� (
==
��) +
null
��, 0
)
��0 1
{
�� 
return
�� 
new
�� "
HttpStatusCodeResult
�� 3
(
��3 4
HttpStatusCode
��4 B
.
��B C

BadRequest
��C M
)
��M N
;
��N O
}
�� 
string
�� 
UserName
�� 
=
��  !"
paramUserAndRolesDTO
��" 6
.
��6 7
UserName
��7 ?
;
��? @
string
�� 

strNewRole
�� !
=
��" #
Convert
��$ +
.
��+ ,
ToString
��, 4
(
��4 5
Request
��5 <
.
��< =
Form
��= A
[
��A B
$str
��B K
]
��K L
)
��L M
;
��M N
if
�� 
(
�� 

strNewRole
�� 
!=
�� !
$str
��" 2
)
��2 3
{
�� 
ApplicationUser
�� #
user
��$ (
=
��) *
UserManager
��+ 6
.
��6 7

FindByName
��7 A
(
��A B
UserName
��B J
)
��J K
;
��K L
UserManager
�� 
.
��  
	AddToRole
��  )
(
��) *
user
��* .
.
��. /
Id
��/ 1
,
��1 2

strNewRole
��3 =
)
��= >
;
��> ?
}
�� 
ViewBag
�� 
.
�� 
AddRole
�� 
=
��  !
new
��" %

SelectList
��& 0
(
��0 1
RolesUserIsNotIn
��1 A
(
��A B
UserName
��B J
)
��J K
)
��K L
;
��L M
UserAndRolesDTO
��  
objUserAndRolesDTO
��  2
=
��3 4
GetUserAndRoles
�� #
(
��# $
UserName
��$ ,
)
��, -
;
��- .
return
�� 
View
�� 
(
��  
objUserAndRolesDTO
�� .
)
��. /
;
��/ 0
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 

ModelState
�� 
.
�� 

�� (
(
��( )
string
��) /
.
��/ 0
Empty
��0 5
,
��5 6
$str
��7 @
+
��A B
ex
��C E
)
��E F
;
��F G
return
�� 
View
�� 
(
�� 
$str
�� '
)
��' (
;
��( )
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 
ActionResult
�� 

DeleteRole
�� &
(
��& '
string
��' -
UserName
��. 6
,
��6 7
string
��8 >
RoleName
��? G
)
��G H
{
�� 	
try
�� 
{
�� 
if
�� 
(
�� 
(
�� 
UserName
�� 
==
��  
null
��! %
)
��% &
||
��' )
(
��* +
RoleName
��+ 3
==
��4 6
null
��7 ;
)
��; <
)
��< =
{
�� 
return
�� 
new
�� "
HttpStatusCodeResult
�� 3
(
��3 4
HttpStatusCode
��4 B
.
��B C

BadRequest
��C M
)
��M N
;
��N O
}
�� 
UserName
�� 
=
�� 
UserName
�� #
.
��# $
ToLower
��$ +
(
��+ ,
)
��, -
;
��- .
ExpandedUserDTO
��  
objExpandedUserDTO
��  2
=
��3 4
GetUser
��5 <
(
��< =
UserName
��= E
)
��E F
;
��F G
if
�� 
(
��  
objExpandedUserDTO
�� &
==
��' )
null
��* .
)
��. /
{
�� 
return
�� 
HttpNotFound
�� '
(
��' (
)
��( )
;
��) *
}
�� 
if
�� 
(
�� 
UserName
�� 
.
�� 
ToLower
�� $
(
��$ %
)
��% &
==
��' )
this
�� 
.
�� 
User
�� 
.
�� 
Identity
�� &
.
��& '
Name
��' +
.
��+ ,
ToLower
��, 3
(
��3 4
)
��4 5
&&
��6 8
RoleName
��9 A
==
��B D
$str
��E T
)
��T U
{
�� 

ModelState
�� 
.
�� 

�� ,
(
��, -
string
��- 3
.
��3 4
Empty
��4 9
,
��9 :
$str
�� V
)
��V W
;
��W X
}
�� 
ApplicationUser
�� 
user
��  $
=
��% &
UserManager
��' 2
.
��2 3

FindByName
��3 =
(
��= >
UserName
��> F
)
��F G
;
��G H
UserManager
�� 
.
�� 
RemoveFromRoles
�� +
(
��+ ,
user
��, 0
.
��0 1
Id
��1 3
,
��3 4
RoleName
��5 =
)
��= >
;
��> ?
UserManager
�� 
.
�� 
Update
�� "
(
��" #
user
��# '
)
��' (
;
��( )
ViewBag
�� 
.
�� 
AddRole
�� 
=
��  !
new
��" %

SelectList
��& 0
(
��0 1
RolesUserIsNotIn
��1 A
(
��A B
UserName
��B J
)
��J K
)
��K L
;
��L M
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( 3
,
��3 4
new
��5 8
{
��9 :
UserName
��; C
=
��D E
UserName
��F N
}
��O P
)
��P Q
;
��Q R
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 

ModelState
�� 
.
�� 

�� (
(
��( )
string
��) /
.
��/ 0
Empty
��0 5
,
��5 6
$str
��7 @
+
��A B
ex
��C E
)
��E F
;
��F G
ViewBag
�� 
.
�� 
AddRole
�� 
=
��  !
new
��" %

SelectList
��& 0
(
��0 1
RolesUserIsNotIn
��1 A
(
��A B
UserName
��B J
)
��J K
)
��K L
;
��L M
UserAndRolesDTO
��  
objUserAndRolesDTO
��  2
=
��3 4
GetUserAndRoles
�� #
(
��# $
UserName
��$ ,
)
��, -
;
��- .
return
�� 
View
�� 
(
�� 
$str
�� '
,
��' ( 
objUserAndRolesDTO
��) ;
)
��; <
;
��< =
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 
ActionResult
�� 
ViewAllRoles
�� (
(
��( )
)
��) *
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 

JsonResult
�� 
ListarRoles
�� %
(
��% &
)
��& '
{
�� 	
try
�� 
{
�� 
var
�� 
roleManager
�� 
=
��  !
new
�� 
RoleManager
�� #
<
��# $
IdentityRole
��$ 0
>
��0 1
(
�� 
new
�� 
	RoleStore
�� %
<
��% &
IdentityRole
��& 2
>
��2 3
(
��3 4
new
��4 7"
ApplicationDbContext
��8 L
(
��L M
)
��M N
)
��N O
)
�� 
;
�� 
List
�� 
<
�� 
RoleDTO
�� 
>
�� 

colRoleDTO
�� (
=
��) *
(
��+ ,
from
��, 0
objRole
��1 8
in
��9 ;
roleManager
��< G
.
��G H
Roles
��H M
select
��, 2
new
��3 6
RoleDTO
��7 >
{
��, -
Id
��0 2
=
��3 4
objRole
��5 <
.
��< =
Id
��= ?
,
��? @
RoleName
��0 8
=
��9 :
objRole
��; B
.
��B C
Name
��C G
}
��, -
)
��- .
.
��. /
ToList
��/ 5
(
��5 6
)
��6 7
;
��7 8
return
�� 
Json
�� 
(
�� 

colRoleDTO
�� &
)
��& '
;
��' (
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
List
�� 
<
�� 
RoleDTO
�� 
>
�� 

colRoleDTO
�� (
=
��) *
new
��+ .
List
��/ 3
<
��3 4
RoleDTO
��4 ;
>
��; <
(
��< =
)
��= >
;
��> ?
return
�� 
Json
�� 
(
�� 

colRoleDTO
�� &
)
��& '
;
��' (
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 
ActionResult
�� 
AddRole
�� #
(
��# $
)
��$ %
{
�� 	
return
�� 
View
�� 
(
�� 
)
��& '
;
��' (
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 

JsonResult
�� 

GuardarRol
�� $
(
��$ %
RoleDTO
��% ,
paramRoleDTO
��- 9
)
��9 :
{
�� 	
try
�� 
{
�� 
var
�� 
RoleName
�� 
=
�� 
paramRoleDTO
�� +
.
��+ ,
RoleName
��, 4
.
��4 5
Trim
��5 9
(
��9 :
)
��: ;
;
��; <
if
�� 
(
�� 
RoleName
�� 
==
�� 
$str
��  "
)
��" #
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( 5
)
��5 6
;
��6 7
}
�� 
var
�� 
roleManager
�� 
=
��  !
new
�� 
RoleManager
�� #
<
��# $
IdentityRole
��$ 0
>
��0 1
(
��1 2
new
�� 
	RoleStore
�� %
<
��% &
IdentityRole
��& 2
>
��2 3
(
��3 4
new
��4 7"
ApplicationDbContext
��8 L
(
��L M
)
��M N
)
��N O
)
�� 
;
�� 
if
�� 
(
�� 
!
�� 
roleManager
��  
.
��  !

RoleExists
��! +
(
��+ ,
RoleName
��, 4
)
��4 5
)
��5 6
{
�� 
roleManager
�� 
.
��  
Create
��  &
(
��& '
new
��' *
IdentityRole
��+ 7
(
��7 8
RoleName
��8 @
)
��@ A
)
��A B
;
��B C
}
�� 
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
SuccessJson
��! ,
(
��, -
$str
��- L
+
��M N
RoleName
��O W
)
��W X
)
��X Y
;
��Y Z
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
HttpContext
�� 
.
�� 
Response
�� $
.
��$ %

StatusCode
��% /
=
��0 1
$num
��2 5
;
��5 6
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 

JsonResult
�� 
DeleteUserRole
�� (
(
��( )
string
��) /
RoleName
��0 8
)
��8 9
{
�� 	
try
�� 
{
�� 
if
�� 
(
�� 
RoleName
�� 
.
�� 
ToLower
�� $
(
��$ %
)
��% &
==
��' )
$str
��* 9
)
��9 :
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
String
��( .
.
��. /
Format
��/ 5
(
��5 6
$str
��6 V
,
��V W
RoleName
��X `
)
��` a
)
��a b
;
��b c
}
�� 
var
�� 
roleManager
�� 
=
��  !
new
�� 
RoleManager
�� #
<
��# $
IdentityRole
��$ 0
>
��0 1
(
��1 2
new
�� 
	RoleStore
�� %
<
��% &
IdentityRole
��& 2
>
��2 3
(
��3 4
new
��4 7"
ApplicationDbContext
��8 L
(
��L M
)
��M N
)
��N O
)
��O P
;
��P Q
var
�� 
UsersInRole
�� 
=
��  !
roleManager
��" -
.
��- .

FindByName
��. 8
(
��8 9
RoleName
��9 A
)
��A B
.
��B C
Users
��C H
.
��H I
Count
��I N
(
��N O
)
��O P
;
��P Q
if
�� 
(
�� 
UsersInRole
�� 
>
��  !
$num
��" #
)
��# $
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
String
�� 
.
�� 
Format
�� %
(
��% &
$str
�� R
,
��R S
RoleName
�� $
)
��$ %
)
�� 
;
�� 
}
�� 
var
�� 
objRoleToDelete
�� #
=
��$ %
(
��& '
from
��' +
objRole
��, 3
in
��4 6
roleManager
��7 B
.
��B C
Roles
��C H
where
��' ,
objRole
��- 4
.
��4 5
Name
��5 9
==
��: <
RoleName
��= E
select
��' -
objRole
��. 5
)
��5 6
.
��6 7
FirstOrDefault
��7 E
(
��E F
)
��F G
;
��G H
if
�� 
(
�� 
objRoleToDelete
�� #
!=
��$ &
null
��' +
)
��+ ,
{
�� 
roleManager
�� 
.
��  
Delete
��  &
(
��& '
objRoleToDelete
��' 6
)
��6 7
;
��7 8
}
�� 
else
�� 
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
String
�� 
.
�� 
Format
�� %
(
��% &
$str
�� M
,
��M N
RoleName
�� $
)
��$ %
)
�� 
;
�� 
}
�� 
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
SuccessJson
��! ,
(
��, -
$str
��- @
)
��@ A
)
��A B
;
��B C
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
HttpContext
�� 
.
�� 
Response
�� $
.
��$ %

StatusCode
��% /
=
��0 1
$num
��2 5
;
��5 6
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
ex
��+ -
)
��- .
)
��. /
;
��/ 0
}
�� 
}
�� 	
public
�� $
ApplicationUserManager
�� %
UserManager
��& 1
{
�� 	
get
�� 
{
�� 
return
�� 
_userManager
�� #
??
��$ &
HttpContext
�� 
.
��  
GetOwinContext
��  .
(
��. /
)
��/ 0
.
�� 
GetUserManager
�� #
<
��# $$
ApplicationUserManager
��$ :
>
��: ;
(
��; <
)
��< =
;
��= >
}
�� 
private
�� 
set
�� 
{
�� 
_userManager
�� 
=
�� 
value
�� $
;
��$ %
}
�� 
}
�� 	
public
�� $
ApplicationRoleManager
�� %
RoleManager
��& 1
{
�� 	
get
�� 
{
�� 
return
�� 
_roleManager
�� #
??
��$ &
HttpContext
�� 
.
��  
GetOwinContext
��  .
(
��. /
)
��/ 0
.
�� 
GetUserManager
�� #
<
��# $$
ApplicationRoleManager
��$ :
>
��: ;
(
��; <
)
��< =
;
��= >
}
�� 
private
�� 
set
�� 
{
�� 
_roleManager
�� 
=
�� 
value
�� $
;
��$ %
}
�� 
}
�� 	
public
�� 

JsonResult
�� %
GetAllRolesAsSelectList
�� 1
(
��1 2
)
��2 3
{
�� 	
List
�� 
<
�� 
SelectListItem
�� 
>
��  !
SelectRoleListItems
��! 4
=
��5 6
new
�� 
List
�� 
<
�� 
SelectListItem
�� '
>
��' (
(
��( )
)
��) *
;
��* +
var
�� 
roleManager
�� 
=
�� 
new
�� 
RoleManager
�� 
<
��  
IdentityRole
��  ,
>
��, -
(
��- .
new
�� 
	RoleStore
�� !
<
��! "
IdentityRole
��" .
>
��. /
(
��/ 0
new
��0 3"
ApplicationDbContext
��4 H
(
��H I
)
��I J
)
��J K
)
��K L
;
��L M
var
�� 
colRoleSelectList
�� !
=
��" #
roleManager
��$ /
.
��/ 0
Roles
��0 5
.
��5 6
OrderBy
��6 =
(
��= >
x
��> ?
=>
��@ B
x
��C D
.
��D E
Name
��E I
)
��I J
.
��J K
ToList
��K Q
(
��Q R
)
��R S
;
��S T
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
colRoleSelectList
��! 2
)
��2 3
{
�� 
SelectRoleListItems
�� #
.
��# $
Add
��$ '
(
��' (
new
�� 
SelectListItem
�� &
{
�� 
Text
�� 
=
�� 
item
�� #
.
��# $
Name
��$ (
.
��( )
ToString
��) 1
(
��1 2
)
��2 3
,
��3 4
Value
�� 
=
�� 
item
��  $
.
��$ %
Name
��% )
.
��) *
ToString
��* 2
(
��2 3
)
��3 4
,
��4 5
}
�� 
)
�� 
;
�� 
}
�� 
return
�� 
Json
�� 
(
�� !
SelectRoleListItems
�� +
)
��+ ,
;
��, -
}
�� 	
private
�� 
ExpandedUserDTO
�� 
GetUser
��  '
(
��' (
string
��( .

��/ <
)
��< =
{
�� 	
ExpandedUserDTO
��  
objExpandedUserDTO
�� .
=
��/ 0
new
��1 4
ExpandedUserDTO
��5 D
(
��D E
)
��E F
;
��F G
var
�� 
result
�� 
=
�� 
UserManager
�� $
.
��$ %

FindByName
��% /
(
��/ 0

��0 =
)
��= >
;
��> ?
if
�� 
(
�� 
result
�� 
==
�� 
null
�� 
)
�� 
throw
��  %
new
��& )
	Exception
��* 3
(
��3 4
$str
��4 M
)
��M N
;
��N O 
objExpandedUserDTO
�� 
.
�� 
UserName
�� '
=
��( )
result
��* 0
.
��0 1
UserName
��1 9
;
��9 : 
objExpandedUserDTO
�� 
.
�� 
Email
�� $
=
��% &
result
��' -
.
��- .
Email
��. 3
;
��3 4 
objExpandedUserDTO
�� 
.
�� 
LockoutEndDateUtc
�� 0
=
��1 2
result
��3 9
.
��9 :
LockoutEndDateUtc
��: K
;
��K L 
objExpandedUserDTO
�� 
.
�� 
AccessFailedCount
�� 0
=
��1 2
result
��3 9
.
��9 :
AccessFailedCount
��: K
;
��K L 
objExpandedUserDTO
�� 
.
�� 
PhoneNumber
�� *
=
��+ ,
result
��- 3
.
��3 4
PhoneNumber
��4 ?
;
��? @
return
��  
objExpandedUserDTO
�� %
;
��% &
}
�� 	
private
�� 
ExpandedUserDTO
�� 

��  -
(
��- .
ExpandedUserDTO
��. ="
paramExpandedUserDTO
��> R
)
��R S
{
�� 	
ApplicationUser
�� 
result
�� "
=
��# $
UserManager
�� 
.
�� 

FindByName
�� &
(
��& '"
paramExpandedUserDTO
��' ;
.
��; <
UserName
��< D
)
��D E
;
��E F
if
�� 
(
�� 
result
�� 
==
�� 
null
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
$str
��$ =
)
��= >
;
��> ?
}
�� 
result
�� 
.
�� 
Email
�� 
=
�� "
paramExpandedUserDTO
�� /
.
��/ 0
Email
��0 5
;
��5 6
result
�� 
.
�� 
UserName
�� 
=
�� "
paramExpandedUserDTO
�� 2
.
��2 3
Email
��3 8
;
��8 9
if
�� 
(
�� 
UserManager
�� 
.
�� 
IsLockedOut
�� '
(
��' (
result
��( .
.
��. /
Id
��/ 1
)
��1 2
)
��2 3
{
�� 
UserManager
�� 
.
�� )
ResetAccessFailedCountAsync
�� 7
(
��7 8
result
��8 >
.
��> ?
Id
��? A
)
��A B
;
��B C
}
�� 
UserManager
�� 
.
�� 
Update
�� 
(
�� 
result
�� %
)
��% &
;
��& '
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 

�� %
(
��% &"
paramExpandedUserDTO
��& :
.
��: ;
Password
��; C
)
��C D
)
��D E
{
�� 
var
�� 
removePassword
�� "
=
��# $
UserManager
��% 0
.
��0 1
RemovePassword
��1 ?
(
��? @
result
��@ F
.
��F G
Id
��G I
)
��I J
;
��J K
if
�� 
(
�� 
removePassword
�� "
.
��" #
	Succeeded
��# ,
)
��, -
{
�� 
var
�� 
AddPassword
�� #
=
��$ %
UserManager
�� #
.
��# $
AddPassword
��$ /
(
��/ 0
result
�� "
.
��" #
Id
��# %
,
��% &"
paramExpandedUserDTO
�� 0
.
��0 1
Password
��1 9
)
�� 
;
�� 
if
�� 
(
�� 
AddPassword
�� #
.
��# $
Errors
��$ *
.
��* +
Count
��+ 0
(
��0 1
)
��1 2
>
��3 4
$num
��5 6
)
��6 7
{
�� 
throw
�� 
new
�� !
	Exception
��" +
(
��+ ,
AddPassword
��, 7
.
��7 8
Errors
��8 >
.
��> ?
FirstOrDefault
��? M
(
��M N
)
��N O
)
��O P
;
��P Q
}
�� 
}
�� 
}
�� 
paramExpandedUserDTO
��  
.
��  !
	IdUsuario
��! *
=
��+ ,
result
��- 3
.
��3 4
Id
��4 6
;
��6 7
return
�� "
paramExpandedUserDTO
�� '
;
��' (
}
�� 	
private
�� 
void
�� 

DeleteUser
�� 
(
��  
ExpandedUserDTO
��  /"
paramExpandedUserDTO
��0 D
)
��D E
{
�� 	
ApplicationUser
�� 
user
��  
=
��! "
UserManager
�� 
.
�� 

FindByName
�� &
(
��& '"
paramExpandedUserDTO
��' ;
.
��; <
UserName
��< D
)
��D E
;
��E F
if
�� 
(
�� 
user
�� 
==
�� 
null
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
$str
��$ =
)
��= >
;
��> ?
}
�� 
UserManager
�� 
.
�� 
RemoveFromRoles
�� '
(
��' (
user
��( ,
.
��, -
Id
��- /
,
��/ 0
UserManager
��1 <
.
��< =
GetRoles
��= E
(
��E F
user
��F J
.
��J K
Id
��K M
)
��M N
.
��N O
ToArray
��O V
(
��V W
)
��W X
)
��X Y
;
��Y Z
UserManager
�� 
.
�� 
Update
�� 
(
�� 
user
�� #
)
��# $
;
��$ %
UserManager
�� 
.
�� 
Delete
�� 
(
�� 
user
�� #
)
��# $
;
��$ %
}
�� 	
private
�� 
UserAndRolesDTO
�� 
GetUserAndRoles
��  /
(
��/ 0
string
��0 6
UserName
��7 ?
)
��? @
{
�� 	
ApplicationUser
�� 
user
��  
=
��! "
UserManager
��# .
.
��. /

FindByName
��/ 9
(
��9 :
UserName
��: B
)
��B C
;
��C D
List
�� 
<
�� 
UserRoleDTO
�� 
>
�� 
colUserRoleDTO
�� ,
=
��- .
(
�� 
from
�� 
objRole
�� 
in
��  
UserManager
��! ,
.
��, -
GetRoles
��- 5
(
��5 6
user
��6 :
.
��: ;
Id
��; =
)
��= >
select
�� 
new
�� 
UserRoleDTO
�� '
{
�� 
RoleName
�� 
=
�� 
objRole
��  '
,
��' (
UserName
�� 
=
�� 
UserName
��  (
}
�� 
)
�� 
.
�� 
ToList
�� 
(
�� 
)
�� 
;
�� 
if
�� 
(
�� 
colUserRoleDTO
�� 
.
�� 
Count
�� $
(
��$ %
)
��% &
==
��' )
$num
��* +
)
��+ ,
{
�� 
colUserRoleDTO
�� 
.
�� 
Add
�� "
(
��" #
new
��# &
UserRoleDTO
��' 2
{
��3 4
RoleName
��5 =
=
��> ?
$str
��@ N
}
��O P
)
��P Q
;
��Q R
}
�� 
ViewBag
�� 
.
�� 
AddRole
�� 
=
�� 
new
�� !

SelectList
��" ,
(
��, -
RolesUserIsNotIn
��- =
(
��= >
UserName
��> F
)
��F G
)
��G H
;
��H I
UserAndRolesDTO
��  
objUserAndRolesDTO
�� .
=
��/ 0
new
�� 
UserAndRolesDTO
�� #
(
��# $
)
��$ %
;
��% & 
objUserAndRolesDTO
�� 
.
�� 
UserName
�� '
=
��( )
UserName
��* 2
;
��2 3 
objUserAndRolesDTO
�� 
.
�� 
colUserRoleDTO
�� -
=
��. /
colUserRoleDTO
��0 >
;
��> ?
return
��  
objUserAndRolesDTO
�� %
;
��% &
}
�� 	
private
�� 
List
�� 
<
�� 
string
�� 
>
�� 
RolesUserIsNotIn
�� -
(
��- .
string
��. 4
UserName
��5 =
)
��= >
{
�� 	
var
�� 
colAllRoles
�� 
=
�� 
RoleManager
�� )
.
��) *
Roles
��* /
.
��/ 0
Select
��0 6
(
��6 7
x
��7 8
=>
��9 ;
x
��< =
.
��= >
Name
��> B
)
��B C
.
��C D
ToList
��D J
(
��J K
)
��K L
;
��L M
ApplicationUser
�� 
user
��  
=
��! "
UserManager
��# .
.
��. /

FindByName
��/ 9
(
��9 :
UserName
��: B
)
��B C
;
��C D
if
�� 
(
�� 
user
�� 
==
�� 
null
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
$str
��$ =
)
��= >
;
��> ?
}
�� 
var
�� 
colRolesForUser
�� 
=
��  !
UserManager
��" -
.
��- .
GetRoles
��. 6
(
��6 7
user
��7 ;
.
��; <
Id
��< >
)
��> ?
.
��? @
ToList
��@ F
(
��F G
)
��G H
;
��H I
var
�� !
colRolesUserInNotIn
�� #
=
��$ %
(
��& '
from
��' +
objRole
��, 3
in
��4 6
colAllRoles
��7 B
where
��' ,
!
��- .
colRolesForUser
��. =
.
��= >
Contains
��> F
(
��F G
objRole
��G N
)
��N O
select
��' -
objRole
��. 5
)
��5 6
.
��6 7
ToList
��7 =
(
��= >
)
��> ?
;
��? @
if
�� 
(
�� !
colRolesUserInNotIn
�� #
.
��# $
Count
��$ )
(
��) *
)
��* +
==
��, .
$num
��/ 0
)
��0 1
{
�� 
colRolesUserInNotIn
�� #
.
��# $
Add
��$ '
(
��' (
$str
��( 8
)
��8 9
;
��9 :
}
�� 
return
�� !
colRolesUserInNotIn
�� &
;
��& '
}
�� 	
}
�� 
}�� �
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CommonFunctions\HtmlStringBuilders\RestaurantHtmlStringBuilder.cs
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
 
WebApplication

 %
.

% &
Controllers

& 1
{ 
public 

static 
class '
RestaurantHtmlStringBuilder 3
{
public 
static 
string 
ObtenerHtmlString .
(. /
ComprobanteOrden/ ?
comprobanteOrden@ P
,P Q
FormatoImpresionR b
formatoc j
,j k.
!EstablecimientoComercialExtendido	l �
sede
� �
,
� �

Controller
� �

controller
� �
)
� �
{ 	


=( )
new* -

(; <
)< =
;= >
string 

htmlString 
= 
HtmlStringBuilder  1
.1 2#
RenderRazorViewToString2 I
(I J
$strJ |
,| }
comprobanteOrden	~ �
,
� �

controller
� �
)
� �
;
� �
return 

htmlString 
; 
} 	
public 
static 
string 
ObtenerHtmlString .
(. /%
ComprobanteCuentaAtencion/ H
comprobanteAtencionI \
,\ ]
FormatoImpresion^ n
formatoo v
,v w.
!EstablecimientoComercialExtendido	x �
sede
� �
,
� �

Controller
� �

controller
� �
)
� �
{ 	


=( )
new* -

(; <
)< =
;= >
string 

htmlString 
= 
HtmlStringBuilder  1
.1 2#
RenderRazorViewToString2 I
(I J
$str	J �
,
� �!
comprobanteAtencion
� �
,
� �

controller
� �
)
� �
;
� �
return 

htmlString 
; 
} 	
} 
}!! ��
{D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CommonFunctions\HtmlStringBuilders\CoreHtmlStringBuilder.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public

static
class
CoreHtmlStringBuilder
{ 
public 
static 
string 
ObtenerHtmlString .
(. /
OrdenDeVenta/ ;
ordenDeVenta< H
,H I
FormatoImpresionJ Z
formato[ b
,b c
byted h
[h i
]i j
qrBytesk r
,r s5
(EstablecimientoComercialExtendidoConLogo	t �
sede
� �
,
� �

Controller
� �

controller
� �
,
� �
IMaestroLogica
� �

� �
)
� �
{ 	


=( )
new* -

(; <
)< =
;= >
string 
result 
= 
$str 
; 
string 
nombreVista 
=  
$str! #
;# $
if 
( 
ordenDeVenta 
. 
IdTipoComprobante .
==/ 1
MaestroSettings2 A
.A B
DefaultB I
.I J-
!IdDetalleMaestroComprobanteBoletaJ k
)k l
{ 
nombreVista 
= 
formato %
==& (
FormatoImpresion) 9
.9 :
_80mm: ?
?@ A
$strB \
:] ^
$str_ y
;y z
result 
= 
HtmlStringBuilder *
.* +#
RenderRazorViewToString+ B
(B C
nombreVistaC N
,N O
newP S

(a b
ordenDeVentab n
,n o
sedep t
,t u
newv y.
!EstablecimientoComercialExtendido	z �
(
� �
ordenDeVenta
� �
.
� �
Transaccion
� �
(
� �
)
� �
.
� �
Actor_negocio2
� �
.
� �
Actor_negocio2
� �
)
� �
,
� �
qrBytes
� �
,
� � 
AplicacionSettings
� �
.
� �
Default
� �
.
� �$
MostrarCabeceraVoucher
� �
,
� �
(
� �.
 ModoImpresionCaracteristicasEnum
� �
)
� �
VentasSettings
� �
.
� �
Default
� �
.
� �*
modoImpresionCaracteristicas
� �
)
� �
,
� �

controller
� �
)
� �
;
� �
} 
else 
if 
( 
ordenDeVenta !
.! "
IdTipoComprobante" 3
==4 6
MaestroSettings7 F
.F G
DefaultG N
.N O.
"IdDetalleMaestroComprobanteFacturaO q
)q r
{ 
nombreVista 
= 
formato %
==& (
FormatoImpresion) 9
.9 :
_80mm: ?
?@ A
$strB V
:W X
$strY m
;m n
result   
=   
HtmlStringBuilder   *
.  * +#
RenderRazorViewToString  + B
(  B C
nombreVista  C N
,  N O
new  P S
Factura  T [
(  [ \
ordenDeVenta  \ h
,  h i
sede  j n
,  n o
new  p s.
!EstablecimientoComercialExtendido	  t �
(
  � �
ordenDeVenta
  � �
.
  � �
Transaccion
  � �
(
  � �
)
  � �
.
  � �
Actor_negocio2
  � �
.
  � �
Actor_negocio2
  � �
)
  � �
,
  � �
qrBytes
  � �
,
  � � 
AplicacionSettings
  � �
.
  � �
Default
  � �
.
  � �$
MostrarCabeceraVoucher
  � �
,
  � �
(
  � �.
 ModoImpresionCaracteristicasEnum
  � �
)
  � �
VentasSettings
  � �
.
  � �
Default
  � �
.
  � �*
modoImpresionCaracteristicas
  � �
)
  � �
,
  � �

controller
  � �
)
  � �
;
  � �
}!! 
else"" 
if"" 
("" 
ordenDeVenta"" !
.""! "
IdTipoComprobante""" 3
==""4 6
MaestroSettings""7 F
.""F G
Default""G N
.""N O7
+IdDetalleMaestroComprobanteNotaVentaInterna""O z
)""z {
{## 
nombreVista$$ 
=$$ 
formato$$ %
==$$& (
FormatoImpresion$$) 9
.$$9 :
_80mm$$: ?
?$$@ A
$str$$B Z
:$$[ \
$str$$] u
;$$u v
result%% 
=%% 
HtmlStringBuilder%% *
.%%* +#
RenderRazorViewToString%%+ B
(%%B C
nombreVista%%C N
,%%N O
new%%P S
NotaDeVenta%%T _
(%%_ `
ordenDeVenta%%` l
,%%l m
sede%%n r
,%%r s
new%%t w.
!EstablecimientoComercialExtendido	%%x �
(
%%� �
ordenDeVenta
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
%%� �
Actor_negocio2
%%� �
.
%%� �
Actor_negocio2
%%� �
)
%%� �
,
%%� �
qrBytes
%%� �
,
%%� � 
AplicacionSettings
%%� �
.
%%� �
Default
%%� �
.
%%� �$
MostrarCabeceraVoucher
%%� �
,
%%� �
(
%%� �.
 ModoImpresionCaracteristicasEnum
%%� �
)
%%� �
VentasSettings
%%� �
.
%%� �
Default
%%� �
.
%%� �*
modoImpresionCaracteristicas
%%� �
)
%%� �
,
%%� �

controller
%%� �
)
%%� �
;
%%� �
}&& 
else'' 
if'' 
('' 
ordenDeVenta'' !
.''! "
IdTipoComprobante''" 3
==''4 6
MaestroSettings''7 F
.''F G
Default''G N
.''N O<
0IdDetalleMaestroComprobanteNotaInvalidacionVenta''O 
)	'' �
{(( 
nombreVista)) 
=)) 
formato)) %
==))& (
FormatoImpresion))) 9
.))9 :
_80mm)): ?
?))@ A
$str))B d
:))e f
$str	))g �
;
))� �
result** 
=** 
HtmlStringBuilder** *
.*** +#
RenderRazorViewToString**+ B
(**B C
nombreVista**C N
,**N O
new**P S!
NotaInvalidacionVenta**T i
(**i j
ordenDeVenta**j v
,**v w
sede**x |
,**| }
new	**~ �/
!EstablecimientoComercialExtendido
**� �
(
**� �
ordenDeVenta
**� �
.
**� �
Transaccion
**� �
(
**� �
)
**� �
.
**� �
Actor_negocio2
**� �
.
**� �
Actor_negocio2
**� �
)
**� �
,
**� �
qrBytes
**� �
,
**� � 
AplicacionSettings
**� �
.
**� �
Default
**� �
.
**� �$
MostrarCabeceraVoucher
**� �
,
**� �
(
**� �.
 ModoImpresionCaracteristicasEnum
**� �
)
**� �
VentasSettings
**� �
.
**� �
Default
**� �
.
**� �*
modoImpresionCaracteristicas
**� �
)
**� �
,
**� �

controller
**� �
)
**� �
;
**� �
}++ 
else,, 
if,, 
(,, 
ordenDeVenta,, !
.,,! "
IdTipoComprobante,," 3
==,,4 6
MaestroSettings,,7 F
.,,F G
Default,,G N
.,,N O9
-IdDetalleMaestroComprobanteNotaCreditoInterna,,O |
),,| }
{-- 
List.. 
<.. 
Detalle_maestro.. $
>..$ %
tiposNotasDeCredito..& 9
=..: ;

...I J#
ObtenerDetallesMaestros..J a
(..a b
MaestroSettings..b q
...q r
Default..r y
...y z4
'IdMaestroTipoDeNotaDeCreditoElectronica	..z �
)
..� �
;
..� �
nombreVista// 
=// 
formato// %
==//& (
FormatoImpresion//) 9
.//9 :
_80mm//: ?
?//@ A
$str//B o
://p q
$str	//r �
;
//� �
result00 
=00 
HtmlStringBuilder00 *
.00* +#
RenderRazorViewToString00+ B
(00B C
nombreVista00C N
,00N O
new00P S 
NotaDeCreditoInterna00T h
(00h i
ordenDeVenta00i u
,00u v
sede00w {
,00{ |
new	00} �/
!EstablecimientoComercialExtendido
00� �
(
00� �
ordenDeVenta
00� �
.
00� �
Transaccion
00� �
(
00� �
)
00� �
.
00� �
Actor_negocio2
00� �
.
00� �
Actor_negocio2
00� �
)
00� �
,
00� �!
tiposNotasDeCredito
00� �
,
00� �
qrBytes
00� �
,
00� � 
AplicacionSettings
00� �
.
00� �
Default
00� �
.
00� �$
MostrarCabeceraVoucher
00� �
,
00� �
(
00� �.
 ModoImpresionCaracteristicasEnum
00� �
)
00� �
VentasSettings
00� �
.
00� �
Default
00� �
.
00� �*
modoImpresionCaracteristicas
00� �
)
00� �
,
00� �

controller
00� �
)
00� �
;
00� �
}11 
else22 
if22 
(22 
ordenDeVenta22 !
.22! "
IdTipoComprobante22" 3
==224 6
MaestroSettings227 F
.22F G
Default22G N
.22N O2
&IdDetalleMaestroComprobanteNotaCredito22O u
&&22v x
ordenDeVenta	22y �
.
22� �#
OperacionDeReferencia
22� �
(
22� �
)
22� �
.
22� �
IdTipoComprobante
22� �
==
22� �
MaestroSettings
22� �
.
22� �
Default
22� �
.
22� �/
!IdDetalleMaestroComprobanteBoleta
22� �
)
22� �
{33 
List44 
<44 
Detalle_maestro44 $
>44$ %
tiposNotasDeCredito44& 9
=44: ;

.44I J#
ObtenerDetallesMaestros44J a
(44a b
MaestroSettings44b q
.44q r
Default44r y
.44y z4
'IdMaestroTipoDeNotaDeCreditoElectronica	44z �
)
44� �
;
44� �
nombreVista55 
=55 
formato55 %
==55& (
FormatoImpresion55) 9
.559 :
_80mm55: ?
?55@ A
$str55B n
:55o p
$str	55q �
;
55� �
result66 
=66 
HtmlStringBuilder66 *
.66* +#
RenderRazorViewToString66+ B
(66B C
nombreVista66C N
,66N O
new66P S

(66a b
ordenDeVenta66b n
,66n o
sede66p t
,66t u
new66v y.
!EstablecimientoComercialExtendido	66z �
(
66� �
ordenDeVenta
66� �
.
66� �
Transaccion
66� �
(
66� �
)
66� �
.
66� �
Actor_negocio2
66� �
.
66� �
Actor_negocio2
66� �
)
66� �
,
66� �!
tiposNotasDeCredito
66� �
,
66� �
qrBytes
66� �
,
66� � 
AplicacionSettings
66� �
.
66� �
Default
66� �
.
66� �$
MostrarCabeceraVoucher
66� �
,
66� �
(
66� �.
 ModoImpresionCaracteristicasEnum
66� �
)
66� �
VentasSettings
66� �
.
66� �
Default
66� �
.
66� �*
modoImpresionCaracteristicas
66� �
)
66� �
,
66� �

controller
66� �
)
66� �
;
66� �
}77 
else88 
if88 
(88 
ordenDeVenta88 !
.88! "
IdTipoComprobante88" 3
==884 6
MaestroSettings887 F
.88F G
Default88G N
.88N O3
'IdDetalleMaestroComprobanteNotaDeDebito88O v
&&88w y
ordenDeVenta	88z �
.
88� �#
OperacionDeReferencia
88� �
(
88� �
)
88� �
.
88� �
IdTipoComprobante
88� �
==
88� �
MaestroSettings
88� �
.
88� �
Default
88� �
.
88� �/
!IdDetalleMaestroComprobanteBoleta
88� �
)
88� �
{99 
List:: 
<:: 
Detalle_maestro:: $
>::$ %
tiposNotasDeDebito::& 8
=::9 :

.::H I#
ObtenerDetallesMaestros::I `
(::` a
MaestroSettings::a p
.::p q
Default::q x
.::x y3
&IdMaestroTipoDeNotaDeDebitoElectronica	::y �
)
::� �
;
::� �
nombreVista;; 
=;; 
formato;; %
==;;& (
FormatoImpresion;;) 9
.;;9 :
_80mm;;: ?
?;;@ A
$str;;B m
:;;n o
$str	;;p �
;
;;� �
result<< 
=<< 
HtmlStringBuilder<< *
.<<* +#
RenderRazorViewToString<<+ B
(<<B C
nombreVista<<C N
,<<N O
new<<P S
NotaDeDebito<<T `
(<<` a
ordenDeVenta<<a m
,<<m n
sede<<o s
,<<s t
new<<u x.
!EstablecimientoComercialExtendido	<<y �
(
<<� �
ordenDeVenta
<<� �
.
<<� �
Transaccion
<<� �
(
<<� �
)
<<� �
.
<<� �
Actor_negocio2
<<� �
.
<<� �
Actor_negocio2
<<� �
)
<<� �
,
<<� � 
tiposNotasDeDebito
<<� �
,
<<� �
qrBytes
<<� �
,
<<� � 
AplicacionSettings
<<� �
.
<<� �
Default
<<� �
.
<<� �$
MostrarCabeceraVoucher
<<� �
,
<<� �
(
<<� �.
 ModoImpresionCaracteristicasEnum
<<� �
)
<<� �
VentasSettings
<<� �
.
<<� �
Default
<<� �
.
<<� �*
modoImpresionCaracteristicas
<<� �
)
<<� �
,
<<� �

controller
<<� �
)
<<� �
;
<<� �
}== 
else>> 
if>> 
(>> 
ordenDeVenta>> !
.>>! "
IdTipoComprobante>>" 3
==>>4 6
MaestroSettings>>7 F
.>>F G
Default>>G N
.>>N O2
&IdDetalleMaestroComprobanteNotaCredito>>O u
&&>>v x
ordenDeVenta	>>y �
.
>>� �#
OperacionDeReferencia
>>� �
(
>>� �
)
>>� �
.
>>� �
IdTipoComprobante
>>� �
==
>>� �
MaestroSettings
>>� �
.
>>� �
Default
>>� �
.
>>� �0
"IdDetalleMaestroComprobanteFactura
>>� �
)
>>� �
{?? 
List@@ 
<@@ 
Detalle_maestro@@ $
>@@$ %
tiposNotasDeCredito@@& 9
=@@: ;

.@@I J#
ObtenerDetallesMaestros@@J a
(@@a b
MaestroSettings@@b q
.@@q r
Default@@r y
.@@y z4
'IdMaestroTipoDeNotaDeCreditoElectronica	@@z �
)
@@� �
;
@@� �
nombreVistaAA 
=AA 
formatoAA %
==AA& (
FormatoImpresionAA) 9
.AA9 :
_80mmAA: ?
?AA@ A
$strAAB o
:AAp q
$str	AAr �
;
AA� �
resultBB 
=BB 
HtmlStringBuilderBB *
.BB* +#
RenderRazorViewToStringBB+ B
(BBB C
nombreVistaBBC N
,BBN O
newBBP S

(BBa b
ordenDeVentaBBb n
,BBn o
sedeBBp t
,BBt u
newBBv y.
!EstablecimientoComercialExtendido	BBz �
(
BB� �
ordenDeVenta
BB� �
.
BB� �
Transaccion
BB� �
(
BB� �
)
BB� �
.
BB� �
Actor_negocio2
BB� �
.
BB� �
Actor_negocio2
BB� �
)
BB� �
,
BB� �!
tiposNotasDeCredito
BB� �
,
BB� �
qrBytes
BB� �
,
BB� � 
AplicacionSettings
BB� �
.
BB� �
Default
BB� �
.
BB� �$
MostrarCabeceraVoucher
BB� �
,
BB� �
(
BB� �.
 ModoImpresionCaracteristicasEnum
BB� �
)
BB� �
VentasSettings
BB� �
.
BB� �
Default
BB� �
.
BB� �*
modoImpresionCaracteristicas
BB� �
)
BB� �
,
BB� �

controller
BB� �
)
BB� �
;
BB� �
}CC 
elseDD 
ifDD 
(DD 
ordenDeVentaDD !
.DD! "
IdTipoComprobanteDD" 3
==DD4 6
MaestroSettingsDD7 F
.DDF G
DefaultDDG N
.DDN O3
'IdDetalleMaestroComprobanteNotaDeDebitoDDO v
&&DDw y
ordenDeVenta	DDz �
.
DD� �#
OperacionDeReferencia
DD� �
(
DD� �
)
DD� �
.
DD� �
IdTipoComprobante
DD� �
==
DD� �
MaestroSettings
DD� �
.
DD� �
Default
DD� �
.
DD� �0
"IdDetalleMaestroComprobanteFactura
DD� �
)
DD� �
{EE 
ListFF 
<FF 
Detalle_maestroFF $
>FF$ %
tiposNotasDeDebitoFF& 8
=FF9 :

.FFH I#
ObtenerDetallesMaestrosFFI `
(FF` a
MaestroSettingsFFa p
.FFp q
DefaultFFq x
.FFx y3
&IdMaestroTipoDeNotaDeDebitoElectronica	FFy �
)
FF� �
;
FF� �
nombreVistaGG 
=GG 
formatoGG %
==GG& (
FormatoImpresionGG) 9
.GG9 :
_80mmGG: ?
?GG@ A
$strGGB n
:GGo p
$str	GGq �
;
GG� �
resultHH 
=HH 
HtmlStringBuilderHH *
.HH* +#
RenderRazorViewToStringHH+ B
(HHB C
nombreVistaHHC N
,HHN O
newHHP S
NotaDeDebitoHHT `
(HH` a
ordenDeVentaHHa m
,HHm n
sedeHHo s
,HHs t
newHHu x.
!EstablecimientoComercialExtendido	HHy �
(
HH� �
ordenDeVenta
HH� �
.
HH� �
Transaccion
HH� �
(
HH� �
)
HH� �
.
HH� �
Actor_negocio2
HH� �
.
HH� �
Actor_negocio2
HH� �
)
HH� �
,
HH� � 
tiposNotasDeDebito
HH� �
,
HH� �
qrBytes
HH� �
,
HH� � 
AplicacionSettings
HH� �
.
HH� �
Default
HH� �
.
HH� �$
MostrarCabeceraVoucher
HH� �
,
HH� �
(
HH� �.
 ModoImpresionCaracteristicasEnum
HH� �
)
HH� �
VentasSettings
HH� �
.
HH� �
Default
HH� �
.
HH� �*
modoImpresionCaracteristicas
HH� �
)
HH� �
,
HH� �

controller
HH� �
)
HH� �
;
HH� �
}II 
returnJJ 
resultJJ 
;JJ 
}KK 	
publicMM 
staticMM 
stringMM 
ObtenerHtmlStringMM .
(MM. /


,MMJ K
FormatoImpresionMML \
formatoMM] d
,MMd e
byteMMf j
[MMj k
]MMk l
qrBytesMMm t
,MMt u5
(EstablecimientoComercialExtendidoConLogo	MMv �
sede
MM� �
,
MM� �

Controller
MM� �

controller
MM� �
,
MM� �
IMaestroLogica
MM� �

MM� �
)
MM� �
{NN 	


=OO( )
newOO* -

(OO; <
)OO< =
;OO= >
stringPP 
resultPP 
=PP 
$strPP 
;PP 
stringQQ 
nombreVistaQQ 
=QQ  
$strQQ! #
;QQ# $
ifSS 
(SS 

.SS 
IdTipoComprobanteSS /
==SS0 2
MaestroSettingsSS3 B
.SSB C
DefaultSSC J
.SSJ K-
!IdDetalleMaestroComprobanteBoletaSSK l
)SSl m
{TT 
nombreVistaUU 
=UU 
formatoUU %
==UU& (
FormatoImpresionUU) 9
.UU9 :
_80mmUU: ?
?UU@ A
$strUUB ^
:UU_ `
$strUUa }
;UU} ~
resultVV 
=VV 
HtmlStringBuilderVV *
.VV* +#
RenderRazorViewToStringVV+ B
(VVB C
nombreVistaVVC N
,VVN O
newVVP S
BoletaDeCompraVVT b
(VVb c

,VVp q
sedeVVr v
,VVv w
qrBytesVVx 
,	VV � 
AplicacionSettings
VV� �
.
VV� �
Default
VV� �
.
VV� �$
MostrarCabeceraVoucher
VV� �
,
VV� �
(
VV� �.
 ModoImpresionCaracteristicasEnum
VV� �
)
VV� �
VentasSettings
VV� �
.
VV� �
Default
VV� �
.
VV� �*
modoImpresionCaracteristicas
VV� �
)
VV� �
,
VV� �

controller
VV� �
)
VV� �
;
VV� �
}WW 
elseXX 
ifXX 
(XX 

.XX" #
IdTipoComprobanteXX# 4
==XX5 7
MaestroSettingsXX8 G
.XXG H
DefaultXXH O
.XXO P.
"IdDetalleMaestroComprobanteFacturaXXP r
)XXr s
{YY 
nombreVistaZZ 
=ZZ 
formatoZZ %
==ZZ& (
FormatoImpresionZZ) 9
.ZZ9 :
_80mmZZ: ?
?ZZ@ A
$strZZB _
:ZZ` a
$strZZb 
;	ZZ �
result\\ 
=\\ 
HtmlStringBuilder\\ *
.\\* +#
RenderRazorViewToString\\+ B
(\\B C
nombreVista\\C N
,\\N O
new\\P S

(\\a b

,\\o p
sede\\q u
,\\u v
qrBytes\\w ~
,\\~  
AplicacionSettings
\\� �
.
\\� �
Default
\\� �
.
\\� �$
MostrarCabeceraVoucher
\\� �
,
\\� �
(
\\� �.
 ModoImpresionCaracteristicasEnum
\\� �
)
\\� �
VentasSettings
\\� �
.
\\� �
Default
\\� �
.
\\� �*
modoImpresionCaracteristicas
\\� �
)
\\� �
,
\\� �

controller
\\� �
)
\\� �
;
\\� �
}]] 
else^^ 
if^^ 
(^^ 

.^^" #
IdTipoComprobante^^# 4
==^^5 7
MaestroSettings^^8 G
.^^G H
Default^^H O
.^^O P8
,IdDetalleMaestroComprobanteNotaCompraInterna^^P |
)^^| }
{__ 
nombreVista`` 
=`` 
formato`` %
==``& (
FormatoImpresion``) 9
.``9 :
_80mm``: ?
?``@ A
$str``B \
:``] ^
$str``_ y
;``y z
resultaa 
=aa 
HtmlStringBuilderaa *
.aa* +#
RenderRazorViewToStringaa+ B
(aaB C
nombreVistaaaC N
,aaN O
newaaP S
NotaDeCompraaaT `
(aa` a

,aan o
sedeaap t
,aat u
qrBytesaav }
,aa} ~
AplicacionSettings	aa �
.
aa� �
Default
aa� �
.
aa� �$
MostrarCabeceraVoucher
aa� �
,
aa� �
(
aa� �.
 ModoImpresionCaracteristicasEnum
aa� �
)
aa� �
VentasSettings
aa� �
.
aa� �
Default
aa� �
.
aa� �*
modoImpresionCaracteristicas
aa� �
)
aa� �
,
aa� �

controller
aa� �
)
aa� �
;
aa� �
}bb 
elsecc 
ifcc 
(cc 

.cc" #
IdTipoComprobantecc# 4
==cc5 7
MaestroSettingscc8 G
.ccG H
DefaultccH O
.ccO P>
1IdDetalleMaestroComprobanteNotaInvalidacionCompra	ccP �
)
cc� �
{dd 
nombreVistaee 
=ee 
formatoee %
==ee& (
FormatoImpresionee) 9
.ee9 :
_80mmee: ?
?ee@ A
$streeB f
:eeg h
$str	eei �
;
ee� �
resultff 
=ff 
HtmlStringBuilderff *
.ff* +#
RenderRazorViewToStringff+ B
(ffB C
nombreVistaffC N
,ffN O
newffP S"
NotaInvalidacionCompraffT j
(ffj k

,ffx y
sedeffz ~
,ff~ 
qrBytes
ff� �
,
ff� � 
AplicacionSettings
ff� �
.
ff� �
Default
ff� �
.
ff� �$
MostrarCabeceraVoucher
ff� �
,
ff� �
(
ff� �.
 ModoImpresionCaracteristicasEnum
ff� �
)
ff� �
VentasSettings
ff� �
.
ff� �
Default
ff� �
.
ff� �*
modoImpresionCaracteristicas
ff� �
)
ff� �
,
ff� �

controller
ff� �
)
ff� �
;
ff� �
}gg 
elsehh 
ifhh 
(hh 

.hh" #
IdTipoComprobantehh# 4
==hh5 7
MaestroSettingshh8 G
.hhG H
DefaulthhH O
.hhO P2
&IdDetalleMaestroComprobanteNotaCreditohhP v
&&hhw y

.
hh� �#
OperacionDeReferencia
hh� �
(
hh� �
)
hh� �
.
hh� �
IdTipoComprobante
hh� �
==
hh� �
MaestroSettings
hh� �
.
hh� �
Default
hh� �
.
hh� �/
!IdDetalleMaestroComprobanteBoleta
hh� �
)
hh� �
{ii 
Listjj 
<jj 
Detalle_maestrojj $
>jj$ %
tiposNotasDeCreditojj& 9
=jj: ;

.jjI J#
ObtenerDetallesMaestrosjjJ a
(jja b
MaestroSettingsjjb q
.jjq r
Defaultjjr y
.jjy z4
'IdMaestroTipoDeNotaDeCreditoElectronica	jjz �
)
jj� �
;
jj� �
nombreVistakk 
=kk 
formatokk %
==kk& (
FormatoImpresionkk) 9
.kk9 :
_80mmkk: ?
?kk@ A
$strkkB v
:kkw x
$str	kky �
;
kk� �
resultll 
=ll 
HtmlStringBuilderll *
.ll* +#
RenderRazorViewToStringll+ B
(llB C
nombreVistallC N
,llN O
newllP S
NotaDeCreditoComprallT g
(llg h

,llu v
sedellw {
,ll{ | 
tiposNotasDeCredito	ll} �
,
ll� �
qrBytes
ll� �
,
ll� � 
AplicacionSettings
ll� �
.
ll� �
Default
ll� �
.
ll� �$
MostrarCabeceraVoucher
ll� �
,
ll� �
(
ll� �.
 ModoImpresionCaracteristicasEnum
ll� �
)
ll� �
VentasSettings
ll� �
.
ll� �
Default
ll� �
.
ll� �*
modoImpresionCaracteristicas
ll� �
)
ll� �
,
ll� �

controller
ll� �
)
ll� �
;
ll� �
}mm 
elsenn 
ifnn 
(nn 

.nn" #
IdTipoComprobantenn# 4
==nn5 7
MaestroSettingsnn8 G
.nnG H
DefaultnnH O
.nnO P3
'IdDetalleMaestroComprobanteNotaDeDebitonnP w
&&nnx z

.
nn� �#
OperacionDeReferencia
nn� �
(
nn� �
)
nn� �
.
nn� �
IdTipoComprobante
nn� �
==
nn� �
MaestroSettings
nn� �
.
nn� �
Default
nn� �
.
nn� �/
!IdDetalleMaestroComprobanteBoleta
nn� �
)
nn� �
{oo 
Listpp 
<pp 
Detalle_maestropp $
>pp$ %
tiposNotasDeDebitopp& 8
=pp9 :

.ppH I#
ObtenerDetallesMaestrosppI `
(pp` a
MaestroSettingsppa p
.ppp q
Defaultppq x
.ppx y3
&IdMaestroTipoDeNotaDeDebitoElectronica	ppy �
)
pp� �
;
pp� �
nombreVistaqq 
=qq 
formatoqq %
==qq& (
FormatoImpresionqq) 9
.qq9 :
_80mmqq: ?
?qq@ A
$strqqB u
:qqv w
$str	qqx �
;
qq� �
resultrr 
=rr 
HtmlStringBuilderrr *
.rr* +#
RenderRazorViewToStringrr+ B
(rrB C
nombreVistarrC N
,rrN O
newrrP S
NotaDeDebitoComprarrT f
(rrf g

,rrt u
sederrv z
,rrz {
tiposNotasDeDebito	rr| �
,
rr� �
qrBytes
rr� �
,
rr� � 
AplicacionSettings
rr� �
.
rr� �
Default
rr� �
.
rr� �$
MostrarCabeceraVoucher
rr� �
,
rr� �
(
rr� �.
 ModoImpresionCaracteristicasEnum
rr� �
)
rr� �
VentasSettings
rr� �
.
rr� �
Default
rr� �
.
rr� �*
modoImpresionCaracteristicas
rr� �
)
rr� �
,
rr� �

controller
rr� �
)
rr� �
;
rr� �
}ss 
elsett 
iftt 
(tt 

.tt" #
IdTipoComprobantett# 4
==tt5 7
MaestroSettingstt8 G
.ttG H
DefaultttH O
.ttO P2
&IdDetalleMaestroComprobanteNotaCreditottP v
&&ttw y

.
tt� �#
OperacionDeReferencia
tt� �
(
tt� �
)
tt� �
.
tt� �
IdTipoComprobante
tt� �
==
tt� �
MaestroSettings
tt� �
.
tt� �
Default
tt� �
.
tt� �0
"IdDetalleMaestroComprobanteFactura
tt� �
)
tt� �
{uu 
Listvv 
<vv 
Detalle_maestrovv $
>vv$ %
tiposNotasDeCreditovv& 9
=vv: ;

.vvI J#
ObtenerDetallesMaestrosvvJ a
(vva b
MaestroSettingsvvb q
.vvq r
Defaultvvr y
.vvy z4
'IdMaestroTipoDeNotaDeCreditoElectronica	vvz �
)
vv� �
;
vv� �
nombreVistaww 
=ww 
formatoww %
==ww& (
FormatoImpresionww) 9
.ww9 :
_80mmww: ?
?ww@ A
$strwwB w
:wwx y
$str	wwz �
;
ww� �
resultxx 
=xx 
HtmlStringBuilderxx *
.xx* +#
RenderRazorViewToStringxx+ B
(xxB C
nombreVistaxxC N
,xxN O
newxxP S
NotaDeCreditoCompraxxT g
(xxg h

,xxu v
sedexxw {
,xx{ | 
tiposNotasDeCredito	xx} �
,
xx� �
qrBytes
xx� �
,
xx� � 
AplicacionSettings
xx� �
.
xx� �
Default
xx� �
.
xx� �$
MostrarCabeceraVoucher
xx� �
,
xx� �
(
xx� �.
 ModoImpresionCaracteristicasEnum
xx� �
)
xx� �
VentasSettings
xx� �
.
xx� �
Default
xx� �
.
xx� �*
modoImpresionCaracteristicas
xx� �
)
xx� �
,
xx� �

controller
xx� �
)
xx� �
;
xx� �
}yy 
elsezz 
ifzz 
(zz 

.zz" #
IdTipoComprobantezz# 4
==zz5 7
MaestroSettingszz8 G
.zzG H
DefaultzzH O
.zzO P3
'IdDetalleMaestroComprobanteNotaDeDebitozzP w
&&zzx z

.
zz� �#
OperacionDeReferencia
zz� �
(
zz� �
)
zz� �
.
zz� �
IdTipoComprobante
zz� �
==
zz� �
MaestroSettings
zz� �
.
zz� �
Default
zz� �
.
zz� �0
"IdDetalleMaestroComprobanteFactura
zz� �
)
zz� �
{{{ 
List|| 
<|| 
Detalle_maestro|| $
>||$ %
tiposNotasDeDebito||& 8
=||9 :

.||H I#
ObtenerDetallesMaestros||I `
(||` a
MaestroSettings||a p
.||p q
Default||q x
.||x y3
&IdMaestroTipoDeNotaDeDebitoElectronica	||y �
)
||� �
;
||� �
nombreVista}} 
=}} 
formato}} %
==}}& (
FormatoImpresion}}) 9
.}}9 :
_80mm}}: ?
?}}@ A
$str}}B v
:}}w x
$str	}}y �
;
}}� �
result~~ 
=~~ 
HtmlStringBuilder~~ *
.~~* +#
RenderRazorViewToString~~+ B
(~~B C
nombreVista~~C N
,~~N O
new~~P S
NotaDeDebitoCompra~~T f
(~~f g

,~~t u
sede~~v z
,~~z {
tiposNotasDeDebito	~~| �
,
~~� �
qrBytes
~~� �
,
~~� � 
AplicacionSettings
~~� �
.
~~� �
Default
~~� �
.
~~� �$
MostrarCabeceraVoucher
~~� �
,
~~� �
(
~~� �.
 ModoImpresionCaracteristicasEnum
~~� �
)
~~� �
VentasSettings
~~� �
.
~~� �
Default
~~� �
.
~~� �*
modoImpresionCaracteristicas
~~� �
)
~~� �
,
~~� �

controller
~~� �
)
~~� �
;
~~� �
} 
return
�� 
result
�� 
;
�� 
}
�� 	
public
�� 
static
�� 
string
�� 
ObtenerHtmlString
�� .
(
��. /(
OrdenDeMovimientoDeAlmacen
��/ I
ordenDeMovimiento
��J [
,
��[ \
FormatoImpresion
��] m
formato
��n u
,
��u v
byte
��w {
[
��{ |
]
��| }
qrBytes��~ �
,��� �1
!EstablecimientoComercialExtendido��� �
sede��� �
,��� �

Controller��� �

controller��� �
)��� �
{
�� 	

�� 

�� '
=
��( )
new
��* -

��. ;
(
��; <
)
��< =
;
��= >
string
�� 
result
�� 
=
�� 
$str
�� 
;
�� 
string
�� 
nombreVista
�� 
=
��  
$str
��! #
;
��# $
if
�� 
(
�� 
ordenDeMovimiento
�� !
.
��! "
IdTipoComprobante
��" 3
==
��4 6
MaestroSettings
��7 F
.
��F G
Default
��G N
.
��N O7
)IdDetalleMaestroComprobanteOrdenDeAlmacen
��O x
)
��x y
{
�� 
nombreVista
�� 
=
�� 
formato
�� %
==
��& (
FormatoImpresion
��) 9
.
��9 :
_80mm
��: ?
?
��@ A
$str
��B _
:
��` a
$str
��b 
;�� �
result
�� 
=
�� 
HtmlStringBuilder
�� *
.
��* +%
RenderRazorViewToString
��+ B
(
��B C
nombreVista
��C N
,
��N O
new
��P S
OrdenDeAlmacen
��T b
(
��b c
ordenDeMovimiento
��c t
,
��t u
sede
��v z
,
��z {
new
��| 1
!EstablecimientoComercialExtendido��� �
(��� �!
ordenDeMovimiento��� �
.��� �
Transaccion��� �
(��� �
)��� �
.��� �
Actor_negocio2��� �
.��� �
Actor_negocio2��� �
)��� �
,��� �
qrBytes��� �
,��� �"
AplicacionSettings��� �
.��� �
Default��� �
.��� �&
MostrarCabeceraVoucher��� �
,��� �
(��� �0
 ModoImpresionCaracteristicasEnum��� �
)��� �
VentasSettings��� �
.��� �
Default��� �
.��� �,
modoImpresionCaracteristicas��� �
)��� �
,��� �

controller��� �
)��� �
;��� �
}
�� 
return
�� 
result
�� 
;
�� 
}
�� 	
public
�� 
static
�� 
string
�� 
ObtenerHtmlString
�� .
(
��. /!
MovimientoDeAlmacen
��/ B!
movimientoDeAlmacen
��C V
,
��V W
FormatoImpresion
��X h
formato
��i p
,
��p q
byte
��r v
[
��v w
]
��w x
qrBytes��y �
,��� �8
(EstablecimientoComercialExtendidoConLogo��� �
sede��� �
,��� �
List��� �
<��� �
	Proveedor��� �
>��� �
proveedores��� �
,��� �
List��� �
<��� �
Detalle_maestro��� �
>��� �%
modalidadesDeTraslado��� �
,��� �
List��� �
<��� �
Detalle_maestro��� �
>��� �!
motivosDeTraslado��� �
,��� �

Controller��� �

controller��� �
)��� �
{
�� 	

�� 

�� '
=
��( )
new
��* -

��. ;
(
��; <
)
��< =
;
��= >
string
�� 
result
�� 
=
�� 
$str
�� 
;
�� 
string
�� 
nombreVista
�� 
=
��  
$str
��! #
;
��# $
if
�� 
(
�� !
movimientoDeAlmacen
�� #
.
��# $
IdTipoComprobante
��$ 5
==
��6 8
MaestroSettings
��9 H
.
��H I
Default
��I P
.
��P QA
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��Q �
)��� �
{
�� 
nombreVista
�� 
=
�� 
formato
�� %
==
��& (
FormatoImpresion
��) 9
.
��9 :
_80mm
��: ?
?
��@ A
$str
��B _
:
��` a
$str
��b 
;�� �
result
�� 
=
�� 
HtmlStringBuilder
�� *
.
��* +%
RenderRazorViewToString
��+ B
(
��B C
nombreVista
��C N
,
��N O
new
��P S
GuiaDeRemision
��T b
(
��b c!
movimientoDeAlmacen
��c v
,
��v w
sede
��x |
,
��| }
new��~ �1
!EstablecimientoComercialExtendido��� �
(��� �#
movimientoDeAlmacen��� �
.��� �
Transaccion��� �
(��� �
)��� �
.��� �
Actor_negocio2��� �
.��� �
Actor_negocio2��� �
)��� �
,��� �
qrBytes��� �
,��� �"
AplicacionSettings��� �
.��� �
Default��� �
.��� �&
MostrarCabeceraVoucher��� �
,��� �
(��� �0
 ModoImpresionCaracteristicasEnum��� �
)��� �
VentasSettings��� �
.��� �
Default��� �
.��� �,
modoImpresionCaracteristicas��� �
,��� �
proveedores��� �
,��� �%
modalidadesDeTraslado��� �
,��� �!
motivosDeTraslado��� �
)��� �
,��� �

controller��� �
)��� �
;��� �
}
�� 
else
�� 
if
�� 
(
�� !
movimientoDeAlmacen
�� (
.
��( )
IdTipoComprobante
��) :
==
��; =
MaestroSettings
��> M
.
��M N
Default
��N U
.
��U V<
-IdDetalleMaestroComprobanteNotaAlmacenInterna��V �
)��� �
{
�� 
nombreVista
�� 
=
�� 
formato
�� %
==
��& (
FormatoImpresion
��) 9
.
��9 :
_80mm
��: ?
?
��@ A
$str
��B ^
:
��_ `
$str
��a }
;
��} ~
result
�� 
=
�� 
HtmlStringBuilder
�� *
.
��* +%
RenderRazorViewToString
��+ B
(
��B C
nombreVista
��C N
,
��N O
new
��P S

��T a
(
��a b!
movimientoDeAlmacen
��b u
,
��u v
sede
��w {
,
��{ |
new��} �1
!EstablecimientoComercialExtendido��� �
(��� �#
movimientoDeAlmacen��� �
.��� �
Transaccion��� �
(��� �
)��� �
.��� �
Actor_negocio2��� �
.��� �
Actor_negocio2��� �
)��� �
,��� �
qrBytes��� �
,��� �"
AplicacionSettings��� �
.��� �
Default��� �
.��� �&
MostrarCabeceraVoucher��� �
,��� �
(��� �0
 ModoImpresionCaracteristicasEnum��� �
)��� �
VentasSettings��� �
.��� �
Default��� �
.��� �,
modoImpresionCaracteristicas��� �
)��� �
,��� �

controller��� �
)��� �
;��� �
}
�� 
else
�� 
{
�� 
nombreVista
�� 
=
�� 
formato
�� %
==
��& (
FormatoImpresion
��) 9
.
��9 :
_80mm
��: ?
?
��@ A
$str
��B b
:
��c d
$str��e �
;��� �
result
�� 
=
�� 
HtmlStringBuilder
�� *
.
��* +%
RenderRazorViewToString
��+ B
(
��B C
nombreVista
��C N
,
��N O
new
��P S*
DocumentoMovimientoDeAlmacen
��T p
(
��p q"
movimientoDeAlmacen��q �
,��� �
sede��� �
,��� �
new��� �1
!EstablecimientoComercialExtendido��� �
(��� �#
movimientoDeAlmacen��� �
.��� �
Transaccion��� �
(��� �
)��� �
.��� �
Actor_negocio2��� �
.��� �
Actor_negocio2��� �
)��� �
,��� �
qrBytes��� �
,��� �"
AplicacionSettings��� �
.��� �
Default��� �
.��� �&
MostrarCabeceraVoucher��� �
,��� �
(��� �0
 ModoImpresionCaracteristicasEnum��� �
)��� �
VentasSettings��� �
.��� �
Default��� �
.��� �,
modoImpresionCaracteristicas��� �
)��� �
,��� �

controller��� �
)��� �
;��� �
}
�� 
return
�� 
result
�� 
;
�� 
}
�� 	
public
�� 
static
�� 
string
�� 
ObtenerHtmlString
�� .
(
��. /!
MovimientoEconomico
��/ B

movimiento
��C M
,
��M N
FormatoImpresion
��O _
formato
��` g
,
��g h7
(EstablecimientoComercialExtendidoConLogo��i �
sede��� �
,��� �

Controller��� �

controller��� �
)��� �
{
�� 	

�� 

�� '
=
��( )
new
��* -

��. ;
(
��; <
)
��< =
;
��= >
string
�� 
result
�� 
=
�� 
$str
�� 
;
�� 
string
�� 
nombreVista
�� 
=
��  
$str
��! #
;
��# $
nombreVista
�� 
=
�� 
formato
�� !
==
��" $
FormatoImpresion
��% 5
.
��5 6
_80mm
��6 ;
?
��< =
$str
��> b
:
��c d
$str��e �
;��� �
result
�� 
=
�� 
HtmlStringBuilder
�� &
.
��& '%
RenderRazorViewToString
��' >
(
��> ?
nombreVista
��? J
,
��J K#
ReciboDeIngresoEgreso
��L a
.
��a b
Convert
��b i
(
��i j

movimiento
��j t
,
��t u
sede
��v z
,
��z {
new
��| 1
!EstablecimientoComercialExtendido��� �
(��� �

movimiento��� �
.��� �
Transaccion��� �
(��� �
)��� �
.��� �
Actor_negocio2��� �
.��� �
Actor_negocio2��� �
)��� �
,��� �"
AplicacionSettings��� �
.��� �
Default��� �
.��� �&
MostrarCabeceraVoucher��� �
)��� �
,��� �

controller��� �
)��� �
;��� �
return
�� 
result
�� 
;
�� 
}
�� 	
}
�� 
}�� ��
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CommonFunctions\PrinterBuilder.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{   
public!! 

class!! 
PrinterBuilder!! 
{"" 
public## 
void## 
ImprimirOperacion## %
(##% &
long##& *
idOperacion##+ 6
,##6 7
int##8 ;

,##I J4
(EstablecimientoComercialExtendidoConLogo##K s
sede##t x
,##x y
PedidoController	##z �
pedidoController
##� �
,
##� �
IPedido_Logica
##� �
pedidoLogica
##� �
,
##� �
IOperacionLogica
##� �
operacionLogica
##� �
,
##� �!
IActorNegocioLogica
##� � 
actorNegocioLogica
##� �
,
##� �
IMaestroLogica
##� �

##� �
,
##� �+
IFacturacionElectronicaLogica
##� �*
facturacionElectronicaLogica
##� �
,
##� �
IBarCodeUtil
##� �
barCodeUtil
##� �
,
##� �
IPdfUtil
##� �
pdfUtil
##� �
)
##� �
{$$ 	
if%% 
(%% 

==%%  
$num%%! "
)%%" #
{&& 

orden'' #
=''$ %
pedidoLogica''& 2
.''2 3,
 ObtenerOrdenDePedidoParaImprimir''3 S
(''S T9
,ObtenerOrdenDePedidoParaImprimirEnAplicacion	''T �
(
''� �
idOperacion
''� �
)
''� �
)
''� �
;
''� �9
-EliminarOrdenDePedidoParaImprimirEnAplicacion(( =
(((= >
idOperacion((> I
)((I J
;((J K

PrintFile_)) 
()) 
orden))  
,))  !
sede))" &
,))& '
pedidoController))( 8
,))8 9

,))G H
pdfUtil))I P
)))P Q
;))Q R
}** 
else++ 
if++ 
(++ 

==++# %
$num++& '
)++' (
{,, 
OrdenDeVenta-- 
orden-- "
=--# $
operacionLogica--% 4
.--4 5+
ObtenerOrdenDeVentaParaImprimir--5 T
(--T U8
+ObtenerOrdenDeVentaParaImprimirEnAplicacion	--U �
(
--� �
idOperacion
--� �
)
--� �
)
--� �
;
--� �8
,EliminarOrdenDeVentaParaImprimirEnAplicacion.. <
(..< =
idOperacion..= H
)..H I
;..I J

PrintFile_// 
(// 
orden//  
,//  !
sede//" &
,//& '
pedidoController//( 8
,//8 9
operacionLogica//: I
,//I J
actorNegocioLogica//K ]
,//] ^

,//l m)
facturacionElectronicaLogica	//n �
,
//� �
barCodeUtil
//� �
,
//� �
pdfUtil
//� �
)
//� �
;
//� �
}00 
}11 	
public33 
void33 

PrintFile_33 
(33 

orden33- 2
,332 34
(EstablecimientoComercialExtendidoConLogo334 \
sede33] a
,33a b
PedidoController33c s
pedidoController	33t �
,
33� �
IMaestroLogica
33� �

33� �
,
33� �
IPdfUtil
33� �
pdfUtil
33� �
)
33� �
{44 	
var55 
PDFventa55 
=55 
ObtenerPdfPedido55 +
(55+ ,
orden55, 1
,551 2
sede553 7
,557 8
null559 =
,55= >
(55? @
FormatoImpresion55@ P
)55P Q
VentasSettings55Q _
.55_ `
Default55` g
.55g h'
formatoImpresionPorDefecto	55h �
,
55� �
pedidoController
55� �
,
55� �

55� �
,
55� �
pdfUtil
55� �
)
55� �
;
55� �
PrintFilePDF77 
file77 
=77 
new77  #
PrintFilePDF77$ 0
(770 1
PDFventa771 9
.779 :
Save77: >
(77> ?
)77? @
,77@ A
orden77B G
.77G H
Id77H J
+77K L
$str77M S
)77S T
;77T U
file88 
.88 

=88  

.88. /
None88/ 3
;883 4
ClientPrintJob:: 
cpj:: 
=::  
new::! $
ClientPrintJob::% 3
(::3 4
)::4 5
;::5 6
cpj;; 
.;; 
	PrintFile;; 
=;; 
file;;  
;;;  !
cpj<< 
.<< 
	PrintFile<< 
.<< 
Copies<<  
=<<! "
AplicacionSettings<<# 5
.<<5 6
Default<<6 =
.<<= >1
%NumeroCopiasAImprimirComprobanteVenta<<> c
;<<c d
cpj== 
.== 

=== 
new==  #
DefaultPrinter==$ 2
(==2 3
)==3 4
;==4 5
HttpContext?? 
.?? 
Current?? 
.??  
Response??  (
.??( )
ContentType??) 4
=??5 6
$str??7 Q
;??Q R
HttpContext@@ 
.@@ 
Current@@ 
.@@  
Response@@  (
.@@( )
BinaryWrite@@) 4
(@@4 5
cpj@@5 8
.@@8 9

GetContent@@9 C
(@@C D
)@@D E
)@@E F
;@@F G
HttpContextAA 
.AA 
CurrentAA 
.AA  
ResponseAA  (
.AA( )
EndAA) ,
(AA, -
)AA- .
;AA. /
}BB 	
publicDD 
PdfDocumentDD 
ObtenerPdfPedidoDD +
(DD+ ,


,DDG H4
(EstablecimientoComercialExtendidoConLogoDDI q
sedeDDr v
,DDv w
byteDDx |
[DD| }
]DD} ~
qrBytes	DD �
,
DD� �
FormatoImpresion
DD� �
formato
DD� �
,
DD� �
PedidoController
DD� �
pedidoController
DD� �
,
DD� �
IMaestroLogica
DD� �

DD� �
,
DD� �
IPdfUtil
DD� �
pdfUtil
DD� �
)
DD� �
{EE 	
stringFF 

htmlStringFF 
=FF 
pedidoControllerFF  0
.FF0 1%
ObtenerCadenaHtmlDePedidoFF1 J
(FFJ K

,FFX Y
formatoFFZ a
,FFa b
qrBytesFFc j
,FFj k
sedeFFl p
)FFp q
;FFq r
returnGG 
pdfUtilGG 
.GG 
ObtenerPdfDocumentoGG .
(GG. /

htmlStringGG/ 9
,GG9 :
formatoGG; B
)GGB C
;GGC D
}HH 	
publicKK 

ArmarOrdenPedidoParaImprimirKK 9
(KK9 :


,KKU V
DatosVentaIntegradaKKW j
pedidoKKk q
,KKq r#
UserProfileSessionData	KKs �

KK� �
)
KK� �
{LL 	

.MM 
TransaccionMM %
(MM% &
)MM& '
.MM' (
Detalle_maestro1MM( 8
=MM9 :

.MMH I
MaestrosFrecuentesMMI [
.MM[ \
MonedaMM\ b
.MMb c
ConvertMMc j
(MMj k
)MMk l
;MMl m

.NN 
TransaccionNN %
(NN% &
)NN& '
.NN' (
ComprobanteNN( 3
.NN3 4
Detalle_maestroNN4 C
=NND E
pedidoNNF L
.NNL M
OrdenNNM R
.NNR S
ComprobanteNNS ^
.NN^ _
TipoNN_ c
.NNc d
ConvertNNd k
(NNk l
)NNl m
;NNm n

.OO 
TransaccionOO %
(OO% &
)OO& '
.OO' (

=OO6 7

.OOE F
EmpleadoOOF N
.OON O
ConvertOOO V
(OOV W
)OOW X
;OOX Y

.PP 
TransaccionPP %
(PP% &
)PP& '
.PP' (
Actor_negocio1PP( 6
=PP7 8
pedidoPP9 ?
.PP? @
OrdenPP@ E
.PPE F
ClientePPF M
.PPM N
ConvertPPN U
(PPU V
)PPV W
;PPW X
foreachRR 
(RR 
varRR 
itemRR 
inRR  

.RR. /
TransaccionRR/ :
(RR: ;
)RR; <
.RR< =
Detalle_transaccionRR= P
)RRP Q
{SS 
itemTT 
.TT 
registroTT 
=TT 
pedidoTT  &
.TT& '
OrdenTT' ,
.TT, -
PlacaTT- 2
;TT2 3
itemUU 
.UU 
Concepto_negocioUU %
=UU& '
newUU( +
Concepto_negocioUU, <
(UU< =
)UU= >
{VV 
idWW 
=WW 
pedidoWW 
.WW  
OrdenWW  %
.WW% &
DetallesWW& .
.WW. /
FirstOrDefaultWW/ =
(WW= >
dWW> ?
=>WW@ B
dWWC D
.WWD E
ProductoWWE M
.WWM N
IdWWN P
==WWQ S
itemWWT X
.WWX Y
id_concepto_negocioWWY l
)WWl m
.WWm n
ProductoWWn v
.WWv w
IdWWw y
,WWy z
codigoXX 
=XX 
pedidoXX #
.XX# $
OrdenXX$ )
.XX) *
DetallesXX* 2
.XX2 3
FirstOrDefaultXX3 A
(XXA B
dXXB C
=>XXD F
dXXG H
.XXH I
ProductoXXI Q
.XXQ R
IdXXR T
==XXU W
itemXXX \
.XX\ ]
id_concepto_negocioXX] p
)XXp q
.XXq r
ProductoXXr z
.XXz {
Codigo	XX{ �
,
XX� �
nombreYY 
=YY 
pedidoYY #
.YY# $
OrdenYY$ )
.YY) *
DetallesYY* 2
.YY2 3
FirstOrDefaultYY3 A
(YYA B
dYYB C
=>YYD F
dYYG H
.YYH I
ProductoYYI Q
.YYQ R
IdYYR T
==YYU W
itemYYX \
.YY\ ]
id_concepto_negocioYY] p
)YYp q
.YYq r
ProductoYYr z
.YYz {
NombreConcepto	YY{ �
,
YY� �
Detalle_maestro4ZZ $
=ZZ% &
newZZ' *
Detalle_maestroZZ+ :
(ZZ: ;
)ZZ; <
{ZZ= >
valorZZ? D
=ZZE F
pedidoZZG M
.ZZM N
OrdenZZN S
.ZZS T
DetallesZZT \
.ZZ\ ]
FirstOrDefaultZZ] k
(ZZk l
dZZl m
=>ZZn p
dZZq r
.ZZr s
ProductoZZs {
.ZZ{ |
IdZZ| ~
==	ZZ �
item
ZZ� �
.
ZZ� �!
id_concepto_negocio
ZZ� �
)
ZZ� �
.
ZZ� �
Producto
ZZ� �
.
ZZ� �
EsBien
ZZ� �
?
ZZ� �
$str
ZZ� �
:
ZZ� �
$str
ZZ� �
}
ZZ� �
}[[ 
;[[ 
}\\ 
return]] 

;]]  !
}^^ 	
public`` 
void`` 8
,GuardarOrdenDePedidoParaImprimirEnAplicacion`` @
(``@ A


)``\ ]
{aa 	
Listbb 
<bb 

>bb 
ordenesDePedidobb  /
=bb0 1
(bb2 3
Listbb3 7
<bb7 8

>bbE F
)bbF G
HttpContextbbG R
.bbR S
CurrentbbS Z
.bbZ [
Applicationbb[ f
[bbf g
$strbbg y
]bby z
??bb{ }
new	bb~ �
List
bb� �
<
bb� �

bb� �
>
bb� �
(
bb� �
)
bb� �
;
bb� �
ordenesDePedidodd 
.dd 
Adddd 
(dd  

)dd- .
;dd. /
HttpContextff 
.ff 
Currentff 
.ff  
Applicationff  +
.ff+ ,
Lockff, 0
(ff0 1
)ff1 2
;ff2 3
HttpContextgg 
.gg 
Currentgg 
.gg  
Applicationgg  +
[gg+ ,
$strgg, >
]gg> ?
=gg@ A
ordenesDePedidoggB Q
;ggQ R
HttpContexthh 
.hh 
Currenthh 
.hh  
Applicationhh  +
.hh+ ,
UnLockhh, 2
(hh2 3
)hh3 4
;hh4 5
}ii 	
publickk 

,ObtenerOrdenDePedidoParaImprimirEnAplicacionkk I
(kkI J
longkkJ N
idOrdenDePedidokkO ^
)kk^ _
{ll 	
Listnn 
<nn 

>nn 
ordenesDePedidosnn  0
=nn1 2
(nn3 4
Listnn4 8
<nn8 9

>nnF G
)nnG H
HttpContextnnH S
.nnS T
CurrentnnT [
.nn[ \
Applicationnn\ g
[nng h
$strnnh z
]nnz {
;nn{ |


=pp( )
ordenesDePedidospp* :
.pp: ;
Singlepp; A
(ppA B
ovppB D
=>ppE G
ovppH J
.ppJ K
IdppK M
==ppN P
idOrdenDePedidoppQ `
)pp` a
;ppa b
returnrr 

;rr  !
}ss 	
publicuu 
voiduu 9
-EliminarOrdenDePedidoParaImprimirEnAplicacionuu A
(uuA B
longuuB F
idOrdenDePedidouuG V
)uuV W
{vv 	
Listxx 
<xx 

>xx 
ordenesDePedidoxx  /
=xx0 1
(xx2 3
Listxx3 7
<xx7 8

>xxE F
)xxF G
HttpContextxxG R
.xxR S
CurrentxxS Z
.xxZ [
Applicationxx[ f
[xxf g
$strxxg y
]xxy z
;xxz {
ordenesDePedidozz 
.zz 
Removezz "
(zz" #
ordenesDePedidozz# 2
.zz2 3
Singlezz3 9
(zz9 :
ovzz: <
=>zz= ?
ovzz@ B
.zzB C
IdzzC E
==zzF H
idOrdenDePedidozzI X
)zzX Y
)zzY Z
;zzZ [
HttpContext|| 
.|| 
Current|| 
.||  
Application||  +
.||+ ,
Lock||, 0
(||0 1
)||1 2
;||2 3
HttpContext}} 
.}} 
Current}} 
.}}  
Application}}  +
[}}+ ,
$str}}, >
]}}> ?
=}}@ A
ordenesDePedido}}B Q
;}}Q R
HttpContext~~ 
.~~ 
Current~~ 
.~~  
Application~~  +
.~~+ ,
UnLock~~, 2
(~~2 3
)~~3 4
;~~4 5
} 	
public
�� 
void
�� 

PrintFile_
�� 
(
�� 
OrdenDeVenta
�� +
orden
��, 1
,
��1 26
(EstablecimientoComercialExtendidoConLogo
��3 [
sede
��\ `
,
��` a

Controller
��b l

controller
��m w
,
��w x
IOperacionLogica��y �
operacionLogica��� �
,��� �#
IActorNegocioLogica��� �"
actorNegocioLogica��� �
,��� �
IMaestroLogica��� �

,��� �-
IFacturacionElectronicaLogica��� �,
facturacionElectronicaLogica��� �
,��� �
IBarCodeUtil��� �
barCodeUtil��� �
,��� �
IPdfUtil��� �
pdfUtil��� �
)��� �
{
�� 	
string
�� 
	QrContent
�� 
=
�� *
facturacionElectronicaLogica
�� ;
.
��; <
	ObtenerQR
��< E
(
��E F
orden
��F K
,
��K L
sede
��M Q
)
��Q R
;
��R S
byte
�� 
[
�� 
]
�� 
QrBytes
�� 
=
�� 
barCodeUtil
�� (
.
��( )
ObtenerCodigoQR
��) 8
(
��8 9
	QrContent
��9 B
)
��B C
;
��C D
var
�� 
PDFventa
�� 
=
�� 
ObtenerPdfVenta
�� *
(
��* +
orden
��+ 0
,
��0 1
sede
��2 6
,
��6 7
QrBytes
��8 ?
,
��? @
(
��A B
FormatoImpresion
��B R
)
��R S
VentasSettings
��S a
.
��a b
Default
��b i
.
��i j)
formatoImpresionPorDefecto��j �
,��� �

controller��� �
,��� �

,��� �
pdfUtil��� �
)��� �
;��� �
if
�� 
(
�� 
orden
�� 
.
�� !
TieneGuiaDeRemision
�� )
(
��) *
)
��* +
)
��+ ,
{
�� 
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� %
modalidadesDeTransporte
�� +
=
��, -

��. ;
.
��; <(
ObtenerModalidadesTraslado
��< V
(
��V W
)
��W X
;
��X Y
var
�� !
motivosDeTransporte
�� '
=
��( )

��* 7
.
��7 8$
ObtenerMotivosTraslado
��8 N
(
��N O
)
��O P
;
��P Q
var
��  
salidaDeMercaderia
�� &
=
��' (
operacionLogica
��) 8
.
��8 9.
 ObtenerSalidaDeMercaderiaDeVenta
��9 Y
(
��Y Z
orden
��Z _
.
��_ `
IdVenta
��` g
)
��g h
;
��h i
foreach
�� 
(
�� 
var
�� 
salida
�� #
in
��$ & 
salidaDeMercaderia
��' 9
)
��9 :
{
�� 
int
�� 
[
�� 
]
�� 

idsUbigeos
�� $
=
��% &
{
��' (
Convert
��) 0
.
��0 1
ToInt32
��1 8
(
��8 9
salida
��9 ?
.
��? @&
IdUbigeoOrigenDeTraslado
��@ X
(
��X Y
)
��Y Z
)
��Z [
,
��[ \
Convert
��] d
.
��d e
ToInt32
��e l
(
��l m
salida
��m s
.
��s t(
IdUbigeoDestinoDeTraslado��t �
(��� �
)��� �
)��� �
}��� �
;��� �
var
�� 
ubigeos
�� 
=
��  !

��" /
.
��/ 0

��0 =
(
��= >

idsUbigeos
��> H
)
��H I
;
��I J
salida
�� 
.
�� 
UbigeoOrigen
�� '
=
��( )
ubigeos
��* 1
.
��1 2
Single
��2 8
(
��8 9
u
��9 :
=>
��; =
u
��> ?
.
��? @
id
��@ B
==
��C E
Convert
��F M
.
��M N
ToInt32
��N U
(
��U V
salida
��V \
.
��\ ]&
IdUbigeoOrigenDeTraslado
��] u
(
��u v
)
��v w
)
��w x
)
��x y
.
��y z 
descripcion_corta��z �
;��� �
salida
�� 
.
�� 

�� (
=
��) *
ubigeos
��+ 2
.
��2 3
Single
��3 9
(
��9 :
u
��: ;
=>
��< >
u
��? @
.
��@ A
id
��A C
==
��D F
Convert
��G N
.
��N O
ToInt32
��O V
(
��V W
salida
��W ]
.
��] ^'
IdUbigeoDestinoDeTraslado
��^ w
(
��w x
)
��x y
)
��y z
)
��z {
.
��{ | 
descripcion_corta��| �
;��� �
byte
�� 
[
�� 
]
�� 
byteQr
�� !
=
��" #
barCodeUtil
��$ /
.
��/ 0
ObtenerCodigoQR
��0 ?
(
��? @
salida
��@ F
.
��F G
UrlDocumentoSunat
��G X
)
��X Y
;
��Y Z
PDFventa
�� 
.
�� 
Append
�� #
(
��# $

PdfBuilder
��$ .
.
��. /.
 ObtenerPdfMovimientoDeMercaderia
��/ O
(
��O P
salida
��P V
,
��V W
sede
��X \
,
��\ ]
byteQr
��^ d
,
��d e
(
��f g
FormatoImpresion
��g w
)
��w x
VentasSettings��x �
.��� �
Default��� �
.��� �*
formatoImpresionPorDefecto��� �
,��� �
proveedores��� �
,��� �'
modalidadesDeTransporte��� �
,��� �#
motivosDeTransporte��� �
,��� �

controller��� �
)��� �
)��� �
;��� �
}
�� 
}
�� 
PrintFilePDF
�� 
file
�� 
=
�� 
new
��  #
PrintFilePDF
��$ 0
(
��0 1
PDFventa
��1 9
.
��9 :
Save
��: >
(
��> ?
)
��? @
,
��@ A
orden
��B G
.
��G H
Id
��H J
+
��K L
$str
��M S
)
��S T
;
��T U
file
�� 
.
�� 

�� 
=
��  

��! .
.
��. /
None
��/ 3
;
��3 4
ClientPrintJob
�� 
cpj
�� 
=
��  
new
��! $
ClientPrintJob
��% 3
(
��3 4
)
��4 5
;
��5 6
cpj
�� 
.
�� 
	PrintFile
�� 
=
�� 
file
��  
;
��  !
cpj
�� 
.
�� 
	PrintFile
�� 
.
�� 
Copies
��  
=
��! " 
AplicacionSettings
��# 5
.
��5 6
Default
��6 =
.
��= >3
%NumeroCopiasAImprimirComprobanteVenta
��> c
;
��c d
cpj
�� 
.
�� 

�� 
=
�� 
new
��  #
DefaultPrinter
��$ 2
(
��2 3
)
��3 4
;
��4 5
System
�� 
.
�� 
Web
�� 
.
�� 
HttpContext
�� "
.
��" #
Current
��# *
.
��* +
Response
��+ 3
.
��3 4
ContentType
��4 ?
=
��@ A
$str
��B \
;
��\ ]
System
�� 
.
�� 
Web
�� 
.
�� 
HttpContext
�� "
.
��" #
Current
��# *
.
��* +
Response
��+ 3
.
��3 4
BinaryWrite
��4 ?
(
��? @
cpj
��@ C
.
��C D

GetContent
��D N
(
��N O
)
��O P
)
��P Q
;
��Q R
System
�� 
.
�� 
Web
�� 
.
�� 
HttpContext
�� "
.
��" #
Current
��# *
.
��* +
Response
��+ 3
.
��3 4
End
��4 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
PdfDocument
�� 
ObtenerPdfVenta
�� *
(
��* +
OrdenDeVenta
��+ 7
ordenDeVenta
��8 D
,
��D E6
(EstablecimientoComercialExtendidoConLogo
��F n
sede
��o s
,
��s t
byte
��u y
[
��y z
]
��z {
qrBytes��| �
,��� � 
FormatoImpresion��� �
formato��� �
,��� �

Controller��� �

controller��� �
,��� �
IMaestroLogica��� �

,��� �
IPdfUtil��� �
pdfUtil��� �
)��� �
{
�� 	
string
�� 

htmlString
�� 
=
�� #
CoreHtmlStringBuilder
��  5
.
��5 6
ObtenerHtmlString
��6 G
(
��G H
ordenDeVenta
��H T
,
��T U
formato
��V ]
,
��] ^
qrBytes
��_ f
,
��f g
sede
��h l
,
��l m

controller
��n x
,
��x y

)��� �
;��� �
return
�� 
pdfUtil
�� 
.
�� !
ObtenerPdfDocumento
�� .
(
��. /

htmlString
��/ 9
,
��9 :
formato
��; B
)
��B C
;
��C D
}
�� 	
public
�� 
OrdenDeVenta
�� )
ArmarOrdenVentaParaImprimir
�� 7
(
��7 8
OrdenDeVenta
��8 D
ordenDeVenta
��E Q
,
��Q R!
DatosVentaIntegrada
��S f
venta
��g l
,
��l m%
UserProfileSessionData��n �

)��� �
{
�� 	
ordenDeVenta
�� 
.
�� 
Transaccion
�� $
(
��$ %
)
��% &
.
��& '
Detalle_maestro1
��' 7
=
��8 9

��: G
.
��G H 
MaestrosFrecuentes
��H Z
.
��Z [
Moneda
��[ a
.
��a b
Convert
��b i
(
��i j
)
��j k
;
��k l
ordenDeVenta
�� 
.
�� 
Transaccion
�� $
(
��$ %
)
��% &
.
��& '
Comprobante
��' 2
.
��2 3
Detalle_maestro
��3 B
=
��C D
venta
��E J
.
��J K
Orden
��K P
.
��P Q
Comprobante
��Q \
.
��\ ]
Tipo
��] a
.
��a b
Convert
��b i
(
��i j
)
��j k
;
��k l
ordenDeVenta
�� 
.
�� 
Transaccion
�� $
(
��$ %
)
��% &
.
��& '

��' 4
=
��5 6

��7 D
.
��D E
Empleado
��E M
.
��M N
Convert
��N U
(
��U V
)
��V W
;
��W X
ordenDeVenta
�� 
.
�� 
Transaccion
�� $
(
��$ %
)
��% &
.
��& '
Actor_negocio1
��' 5
=
��6 7
venta
��8 =
.
��= >
Orden
��> C
.
��C D
Cliente
��D K
.
��K L
Convert
��L S
(
��S T
)
��T U
;
��U V
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
ordenDeVenta
��! -
.
��- .
Transaccion
��. 9
(
��9 :
)
��: ;
.
��; <!
Detalle_transaccion
��< O
)
��O P
{
�� 
item
�� 
.
�� 
registro
�� 
=
�� 
venta
��  %
.
��% &
Orden
��& +
.
��+ ,
Placa
��, 1
;
��1 2
item
�� 
.
�� 
Concepto_negocio
�� %
=
��& '
new
��( +
Concepto_negocio
��, <
(
��< =
)
��= >
{
�� 
id
�� 
=
�� 
venta
�� 
.
�� 
Orden
�� $
.
��$ %
Detalles
��% -
.
��- .
FirstOrDefault
��. <
(
��< =
d
��= >
=>
��? A
d
��B C
.
��C D
Producto
��D L
.
��L M
Id
��M O
==
��P R
item
��S W
.
��W X!
id_concepto_negocio
��X k
)
��k l
.
��l m
Producto
��m u
.
��u v
Id
��v x
,
��x y
codigo
�� 
=
�� 
venta
�� "
.
��" #
Orden
��# (
.
��( )
Detalles
��) 1
.
��1 2
FirstOrDefault
��2 @
(
��@ A
d
��A B
=>
��C E
d
��F G
.
��G H
Producto
��H P
.
��P Q
Id
��Q S
==
��T V
item
��W [
.
��[ \!
id_concepto_negocio
��\ o
)
��o p
.
��p q
Producto
��q y
.
��y z
Codigo��z �
,��� �
nombre
�� 
=
�� 
venta
�� "
.
��" #
Orden
��# (
.
��( )
Detalles
��) 1
.
��1 2
FirstOrDefault
��2 @
(
��@ A
d
��A B
=>
��C E
d
��F G
.
��G H
Producto
��H P
.
��P Q
Id
��Q S
==
��T V
item
��W [
.
��[ \!
id_concepto_negocio
��\ o
)
��o p
.
��p q
Producto
��q y
.
��y z
NombreConcepto��z �
,��� �
Detalle_maestro4
�� $
=
��% &
new
��' *
Detalle_maestro
��+ :
(
��: ;
)
��; <
{
��= >
valor
��? D
=
��E F
venta
��G L
.
��L M
Orden
��M R
.
��R S
Detalles
��S [
.
��[ \
FirstOrDefault
��\ j
(
��j k
d
��k l
=>
��m o
d
��p q
.
��q r
Producto
��r z
.
��z {
Id
��{ }
==��~ �
item��� �
.��� �#
id_concepto_negocio��� �
)��� �
.��� �
Producto��� �
.��� �
EsBien��� �
?��� �
$str��� �
:��� �
$str��� �
}��� �
}
�� 
;
�� 
}
�� 
return
�� 
ordenDeVenta
�� 
;
��  
}
�� 	
public
�� 
void
�� 9
+GuardarOrdenDeVentaParaImprimirEnAplicacion
�� ?
(
��? @
OrdenDeVenta
��@ L
ordenDeVenta
��M Y
)
��Y Z
{
�� 	
List
�� 
<
�� 
OrdenDeVenta
�� 
>
�� 
ordenesDeVenta
�� -
=
��. /
(
��0 1
List
��1 5
<
��5 6
OrdenDeVenta
��6 B
>
��B C
)
��C D
HttpContext
��D O
.
��O P
Current
��P W
.
��W X
Application
��X c
[
��c d
$str
��d u
]
��u v
??
��w y
new
��z }
List��~ �
<��� �
OrdenDeVenta��� �
>��� �
(��� �
)��� �
;��� �
ordenesDeVenta
�� 
.
�� 
Add
�� 
(
�� 
ordenDeVenta
�� +
)
��+ ,
;
��, -
HttpContext
�� 
.
�� 
Current
�� 
.
��  
Application
��  +
.
��+ ,
Lock
��, 0
(
��0 1
)
��1 2
;
��2 3
HttpContext
�� 
.
�� 
Current
�� 
.
��  
Application
��  +
[
��+ ,
$str
��, =
]
��= >
=
��? @
ordenesDeVenta
��A O
;
��O P
HttpContext
�� 
.
�� 
Current
�� 
.
��  
Application
��  +
.
��+ ,
UnLock
��, 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
OrdenDeVenta
�� 9
+ObtenerOrdenDeVentaParaImprimirEnAplicacion
�� G
(
��G H
long
��H L
idOrdenDeVenta
��M [
)
��[ \
{
�� 	
List
�� 
<
�� 
OrdenDeVenta
�� 
>
�� 
ordenesDeVenta
�� -
=
��. /
(
��0 1
List
��1 5
<
��5 6
OrdenDeVenta
��6 B
>
��B C
)
��C D
HttpContext
��D O
.
��O P
Current
��P W
.
��W X
Application
��X c
[
��c d
$str
��d u
]
��u v
;
��v w
OrdenDeVenta
�� 
ordenDeVenta
�� %
=
��& '
ordenesDeVenta
��( 6
.
��6 7
Single
��7 =
(
��= >
ov
��> @
=>
��A C
ov
��D F
.
��F G
Id
��G I
==
��J L
idOrdenDeVenta
��M [
)
��[ \
;
��\ ]
return
�� 
ordenDeVenta
�� 
;
��  
}
�� 	
public
�� 
void
�� :
,EliminarOrdenDeVentaParaImprimirEnAplicacion
�� @
(
��@ A
long
��A E
idOrdenDeVenta
��F T
)
��T U
{
�� 	
List
�� 
<
�� 
OrdenDeVenta
�� 
>
�� 
ordenesDeVenta
�� -
=
��. /
(
��0 1
List
��1 5
<
��5 6
OrdenDeVenta
��6 B
>
��B C
)
��C D
HttpContext
��D O
.
��O P
Current
��P W
.
��W X
Application
��X c
[
��c d
$str
��d u
]
��u v
;
��v w
ordenesDeVenta
�� 
.
�� 
Remove
�� !
(
��! "
ordenesDeVenta
��" 0
.
��0 1
Single
��1 7
(
��7 8
ov
��8 :
=>
��; =
ov
��> @
.
��@ A
Id
��A C
==
��D F
idOrdenDeVenta
��G U
)
��U V
)
��V W
;
��W X
HttpContext
�� 
.
�� 
Current
�� 
.
��  
Application
��  +
.
��+ ,
Lock
��, 0
(
��0 1
)
��1 2
;
��2 3
HttpContext
�� 
.
�� 
Current
�� 
.
��  
Application
��  +
[
��+ ,
$str
��, =
]
��= >
=
��? @
ordenesDeVenta
��A O
;
��O P
HttpContext
�� 
.
�� 
Current
�� 
.
��  
Application
��  +
.
��+ ,
UnLock
��, 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
}
�� 
}�� �p
]D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CommonFunctions\XmlBuilder.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

static 
class 

XmlBuilder "
{ 
public   
static   
DocumentoXml   "!
ObtenerXmlComprobante  # 8
(  8 9
OrdenDeVenta  9 E
ordenDeVenta  F R
,  R S-
!EstablecimientoComercialExtendido  T u
sede  v z
,  z {
IMaestroLogica	  | �

  � �
,
  � �'
IGeneracionArchivosLogica
  � �&
generacionArchivosLogica
  � �
,
  � �+
IFacturacionElectronicaLogica
  � �*
facturacionElectronicaLogica
  � �
)
  � �
{!! 	
try"" 
{## 
string$$ 
	metodoApi$$  
=$$! "
$str$$# %
,$$% &

=$$5 6
$str$$7 9
;$$9 : 
DocumentoElectronico%% $
	documento%%% .
;%%. /
switch&& 
(&& 
ordenDeVenta&& $
.&&$ %
Comprobante&&% 0
(&&0 1
)&&1 2
.&&2 3

CodigoTipo&&3 =
)&&= >
{'' 
case(( 
$str(( 
:(( 
	metodoApi)) !
=))" #
$str))$ <
;))< =
var** 
tiposNotasDeCredito** /
=**0 1

.**? @#
ObtenerDetallesMaestros**@ W
(**W X
MaestroSettings**X g
.**g h
Default**h o
.**o p4
'IdMaestroTipoDeNotaDeCreditoElectronica	**p �
)
**� �
;
**� �
var++ 

=++* +
new++, /

(++= >
ordenDeVenta++> J
,++J K
sede++L P
,++P Q
new++R U-
!EstablecimientoComercialExtendido++V w
(++w x
ordenDeVenta	++x �
.
++� �
Transaccion
++� �
(
++� �
)
++� �
.
++� �
Actor_negocio2
++� �
.
++� �
Actor_negocio2
++� �
)
++� �
,
++� �!
tiposNotasDeCredito
++� �
,
++� �
null
++� �
,
++� �
false
++� �
,
++� �
(
++� �.
 ModoImpresionCaracteristicasEnum
++� �
)
++� �
VentasSettings
++� �
.
++� �
Default
++� �
.
++� �*
modoImpresionCaracteristicas
++� �
)
++� �
;
++� �
	documento,, !
=,," #$
generacionArchivosLogica,,$ <
.,,< =2
&ObtenerDocumentoElectronicoNotaCredito,,= c
(,,c d

),,q r
;,,r s
break-- 
;-- 
case.. 
$str.. 
:.. 
	metodoApi// !
=//" #
$str//$ ;
;//; <
var00 
tiposNotasDeDebito00 .
=00/ 0

.00> ?#
ObtenerDetallesMaestros00? V
(00V W
MaestroSettings00W f
.00f g
Default00g n
.00n o3
&IdMaestroTipoDeNotaDeDebitoElectronica	00o �
)
00� �
;
00� �
var11 
notaDeDebito11 (
=11) *
new11+ .
NotaDeDebito11/ ;
(11; <
ordenDeVenta11< H
,11H I
sede11J N
,11N O
new11P S-
!EstablecimientoComercialExtendido11T u
(11u v
ordenDeVenta	11v �
.
11� �
Transaccion
11� �
(
11� �
)
11� �
.
11� �
Actor_negocio2
11� �
.
11� �
Actor_negocio2
11� �
)
11� �
,
11� � 
tiposNotasDeDebito
11� �
,
11� �
null
11� �
,
11� �
false
11� �
,
11� �
(
11� �.
 ModoImpresionCaracteristicasEnum
11� �
)
11� �
VentasSettings
11� �
.
11� �
Default
11� �
.
11� �*
modoImpresionCaracteristicas
11� �
)
11� �
;
11� �
	documento22 !
=22" #$
generacionArchivosLogica22$ <
.22< =1
%ObtenerDocumentoElectronicoNotaDebito22= b
(22b c
notaDeDebito22c o
)22o p
;22p q
break33 
;33 
default44 
:44 
	metodoApi55 !
=55" #
$str55$ 8
;558 9
var66 
factura66 #
=66$ %
new66& )
Factura66* 1
(661 2
ordenDeVenta662 >
,66> ?
sede66@ D
,66D E
new66F I-
!EstablecimientoComercialExtendido66J k
(66k l
ordenDeVenta66l x
.66x y
Transaccion	66y �
(
66� �
)
66� �
.
66� �
Actor_negocio2
66� �
.
66� �
Actor_negocio2
66� �
)
66� �
,
66� �
null
66� �
,
66� �
false
66� �
,
66� �
(
66� �.
 ModoImpresionCaracteristicasEnum
66� �
)
66� �
VentasSettings
66� �
.
66� �
Default
66� �
.
66� �*
modoImpresionCaracteristicas
66� �
)
66� �
;
66� �
	documento77 !
=77" #$
generacionArchivosLogica77$ <
.77< =.
"ObtenerDocumentoElectronicoFactura77= _
(77_ `
factura77` g
)77g h
;77h i
break88 
;88 
}99 
var:: 
documentoResponse:: %
=::& '

RestHelper::( 2
<::2 3 
DocumentoElectronico::3 G
,::G H
DocumentoResponse::I Z
>::Z [
.::[ \
Execute::\ c
(::c d
	metodoApi::d m
,::m n
	documento::o x
,::x y+
FacturacionElectronicaSettings	::z �
.
::� �
Default
::� �
.
::� �*
UrlApiFacturacionElectronica
::� �
)
::� �
;
::� �

=;; 
	documento;;  )
.;;) *
IdDocumento;;* 5
;;;5 6
if<< 
(<< 
!<< 
documentoResponse<< &
.<<& '
Exito<<' ,
)<<, -
{== 
throw>> 
new>> 
ControllerException>> 1
(>>1 2
documentoResponse>>2 C
.>>C D
MensajeError>>D P
)>>P Q
;>>Q R
}?? 
return@@ 
FimarXmlComprobante@@ *
(@@* +
sede@@+ /
,@@/ 0(
facturacionElectronicaLogica@@1 M
,@@M N
documentoResponse@@O `
,@@` a

)@@o p
;@@p q
}AA 
catchBB 
(BB 
	ExceptionBB 
exBB 
)BB  
{CC 
throwDD 
newDD 
ControllerExceptionDD -
(DD- .
$strDD. Y
,DDY Z
exDD[ ]
)DD] ^
;DD^ _
}EE 
}FF 	
publicHH 
staticHH 
DocumentoXmlHH "!
ObtenerXmlComprobanteHH# 8
(HH8 9
MovimientoDeAlmacenHH9 L
movimientoDeAlmacenHHM `
,HH` a.
!EstablecimientoComercialExtendido	HHb �
sede
HH� �
,
HH� �
List
HH� �
<
HH� �
	Proveedor
HH� �
>
HH� �
proveedores
HH� �
,
HH� �
List
HH� �
<
HH� �
Detalle_maestro
HH� �
>
HH� �#
modalidadesDeTraslado
HH� �
,
HH� �
List
HH� �
<
HH� �
Detalle_maestro
HH� �
>
HH� �
motivosDeTraslado
HH� �
,
HH� �'
IGeneracionArchivosLogica
HH� �&
generacionArchivosLogica
HH� �
,
HH� �+
IFacturacionElectronicaLogica
HH� �*
facturacionElectronicaLogica
HH� �
)
HH� �
{II 	
tryJJ 
{KK 
stringLL 
	metodoApiLL  
=LL! "
$strLL# %
,LL% &

=LL5 6
$strLL7 9
;LL9 :
	metodoApiMM 
=MM 
$strMM 5
;MM5 6
varNN 
guiaDeRemisionNN "
=NN# $
newNN% (
GuiaDeRemisionNN) 7
(NN7 8
movimientoDeAlmacenNN8 K
,NNK L
sedeNNM Q
,NNQ R
newNNS V-
!EstablecimientoComercialExtendidoNNW x
(NNx y 
movimientoDeAlmacen	NNy �
.
NN� �
Transaccion
NN� �
(
NN� �
)
NN� �
.
NN� �
Actor_negocio2
NN� �
.
NN� �
Actor_negocio2
NN� �
)
NN� �
,
NN� �
null
NN� �
,
NN� �
false
NN� �
,
NN� �
(
NN� �.
 ModoImpresionCaracteristicasEnum
NN� �
)
NN� �
VentasSettings
NN� �
.
NN� �
Default
NN� �
.
NN� �*
modoImpresionCaracteristicas
NN� �
,
NN� �
proveedores
NN� �
,
NN� �#
modalidadesDeTraslado
NN� �
,
NN� �
motivosDeTraslado
NN� �
)
NN� �
;
NN� �
varOO 
guiaRemisionOO  
=OO! "$
generacionArchivosLogicaOO# ;
.OO; <5
)ObtenerDocumentoElectronicoGuiaDeRemisionOO< e
(OOe f
guiaDeRemisionOOf t
)OOt u
;OOu v
varPP 
documentoResponsePP %
=PP& '

RestHelperPP( 2
<PP2 3
GuiaRemisionPP3 ?
,PP? @
DocumentoResponsePPA R
>PPR S
.PPS T
ExecutePPT [
(PP[ \
	metodoApiPP\ e
,PPe f
guiaRemisionPPg s
,PPs t+
FacturacionElectronicaSettings	PPu �
.
PP� �
Default
PP� �
.
PP� �*
UrlApiFacturacionElectronica
PP� �
)
PP� �
;
PP� �

=QQ 
guiaRemisionQQ  ,
.QQ, -
IdDocumentoQQ- 8
;QQ8 9
ifRR 
(RR 
!RR 
documentoResponseRR &
.RR& '
ExitoRR' ,
)RR, -
{SS 
throwTT 
newTT 
ControllerExceptionTT 1
(TT1 2
documentoResponseTT2 C
.TTC D
MensajeErrorTTD P
)TTP Q
;TTQ R
}UU 
returnVV 
FimarXmlComprobanteVV *
(VV* +
sedeVV+ /
,VV/ 0(
facturacionElectronicaLogicaVV1 M
,VVM N
documentoResponseVVO `
,VV` a

)VVo p
;VVp q
}WW 
catchXX 
(XX 
	ExceptionXX 
exXX 
)XX  
{YY 
throwZZ 
newZZ 
ControllerExceptionZZ -
(ZZ- .
$strZZ. Y
,ZZY Z
exZZ[ ]
)ZZ] ^
;ZZ^ _
}[[ 
}\\ 	
public^^ 
static^^ 
DocumentoXml^^ "
FimarXmlComprobante^^# 6
(^^6 7$
EstablecimientoComercial^^7 O
sede^^P T
,^^T U)
IFacturacionElectronicaLogica^^V s)
facturacionElectronicaLogica	^^t �
,
^^� �
DocumentoResponse
^^� �
documentoResponse
^^� �
,
^^� �
string
^^� �

^^� �
)
^^� �
{__ 	
try`` 
{aa 
bytebb 
[bb 
]bb 
archivoCertificadobb )
=bb* +(
facturacionElectronicaLogicabb, H
.bbH I
ObtenerCertificadobbI [
(bb[ \
sedebb\ `
.bb` a
DocumentoIdentidadbba s
)bbs t
;bbt u
varcc 
firmadocc 
=cc 
newcc !
FirmadoRequestcc" 0
{dd 
TramaXmlSinFirmaee $
=ee% &
documentoResponseee' 8
.ee8 9
TramaXmlSinFirmaee9 I
,eeI J
CertificadoDigitalff &
=ff' (
Convertff) 0
.ff0 1
ToBase64Stringff1 ?
(ff? @
archivoCertificadoff@ R
)ffR S
,ffS T
PasswordCertificadogg '
=gg( )*
FacturacionElectronicaSettingsgg* H
.ggH I
DefaultggI P
.ggP Q#
ClaveCertificadoDigitalggQ h
,ggh i
}hh 
;hh 
varii 

=ii" #

RestHelperii$ .
<ii. /
FirmadoRequestii/ =
,ii= >
FirmadoResponseii? N
>iiN O
.iiO P
ExecuteiiP W
(iiW X
$striiX d
,iid e
firmadoiif m
,iim n+
FacturacionElectronicaSettings	iio �
.
ii� �
Default
ii� �
.
ii� �*
UrlApiFacturacionElectronica
ii� �
)
ii� �
;
ii� �
ifjj 
(jj 
!jj 

.jj" #
Exitojj# (
)jj( )
{kk 
throwll 
newll 
ControllerExceptionll 1
(ll1 2

.ll? @
MensajeErrorll@ L
)llL M
;llM N
}mm 
bytenn 
[nn 
]nn 
xmlFirmadoBytenn %
=nn& '
Convertnn( /
.nn/ 0
FromBase64Stringnn0 @
(nn@ A

.nnN O
TramaXmlFirmadonnO ^
)nn^ _
;nn_ `
varpp 
documentoXmlpp  
=pp! "
newpp# &
DocumentoXmlpp' 3
{pp4 5
NombreDocumentopp6 E
=ppF G

,ppU V
	DocumentoppW `
=ppa b
xmlFirmadoByteppc q
}ppr s
;pps t
returnqq 
documentoXmlqq #
;qq# $
}rr 
catchss 
(ss 
	Exceptionss 
exss 
)ss  
{tt 
throwuu 
newuu 
ControllerExceptionuu -
(uu- .
$struu. Y
,uuY Z
exuu[ ]
)uu] ^
;uu^ _
}vv 
}ww 	
}xx 
}yy �
]D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CommonFunctions\PdfBuilder.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

static 
class 

PdfBuilder "
{ 
public   
static   
PdfDocument   !,
 ObtenerPdfMovimientoDeMercaderia  " B
(  B C
MovimientoDeAlmacen  C V

movimiento  W a
,  a b5
(EstablecimientoComercialExtendidoConLogo	  c �
sede
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
  � �
FormatoImpresion
  � �
formato
  � �
,
  � �
List
  � �
<
  � �
	Proveedor
  � �
>
  � �
proveedores
  � �
,
  � �
List
  � �
<
  � �
Detalle_maestro
  � �
>
  � �#
modalidadesDeTraslado
  � �
,
  � �
List
  � �
<
  � �
Detalle_maestro
  � �
>
  � �
motivosDeTraslado
  � �
,
  � �

Controller
  � �

controller
  � �
)
  � �
{!! 	
IPdfUtil"" 
pdfUtil"" 
="" 
Dependencia"" *
.""* +
Resolve""+ 2
<""2 3
IPdfUtil""3 ;
>""; <
(""< =
)""= >
;""> ?
string## 

htmlString## 
=## !
CoreHtmlStringBuilder##  5
.##5 6
ObtenerHtmlString##6 G
(##G H

movimiento##H R
,##R S
formato##T [
,##[ \
qrBytes##] d
,##d e
sede##f j
,##j k
proveedores##l w
,##w x"
modalidadesDeTraslado	##y �
,
##� �
motivosDeTraslado
##� �
,
##� �

controller
##� �
)
##� �
;
##� �
return$$ 
pdfUtil$$ 
.$$ 
ObtenerPdfDocumento$$ .
($$. /

htmlString$$/ 9
,$$9 :
formato$$; B
)$$B C
;$$C D
}%% 	
}&& 
}** �
wD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CommonFunctions\HtmlStringBuilders\HtmlStringBuilder.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{
public 

static 
class 
HtmlStringBuilder )
{ 
public 
static 
string #
RenderRazorViewToString 4
(4 5
string5 ;
viewName< D
,D E
objectF L
modelM R
,S T

ControllerU _

controller` j
)j k
{ 	

controller 
. 
ViewData 
.  
Model  %
=& '
model( -
;- .
using 
( 
var 
sw 
= 
new 
StringWriter  ,
(, -
)- .
). /
{ 
var 

viewResult 
=  
ViewEngines! ,
., -
Engines- 4
.4 5
FindPartialView5 D
(D E

controllerE O
.O P
ControllerContextP a
,a b
viewNamec k
)k l
;l m
var 
viewContext 
=  !
new" %
ViewContext& 1
(1 2

controller2 <
.< =
ControllerContext= N
,N O

viewResultP Z
.Z [
View[ _
,_ `

controllera k
.k l
ViewDatal t
,t u

controller	v �
.
� �
TempData
� �
,
� �
sw
� �
)
� �
;
� �

viewResult 
. 
View 
.  
Render  &
(& '
viewContext' 2
,2 3
sw4 6
)6 7
;7 8

viewResult   
.   

ViewEngine   %
.  % &
ReleaseView  & 1
(  1 2

controller  2 <
.  < =
ControllerContext  = N
,  N O

viewResult  P Z
.  Z [
View  [ _
)  _ `
;  ` a
return!! 
sw!! 
.!! 
GetStringBuilder!! *
(!!* +
)!!+ ,
.!!, -
ToString!!- 5
(!!5 6
)!!6 7
;!!7 8
}"" 
}## 	
}%% 	
})) �y
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\CommonAlmacenController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class #
CommonAlmacenController (
:) *
BaseController+ 9
{ 
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected   
readonly   
IMaestroLogica   )

;  7 8
	protected!! 
readonly!! 
IOperacionLogica!! +
operacionLogica!!, ;
;!!; <
	protected"" 
readonly"" '
IInventarioHistorico_Logica"" 6%
inventarioHistoricoLogica""7 P
;""P Q
	protected## 
readonly##  
IConfiguracionLogica## /
configuracionLogica##0 C
;##C D
	protected$$ 
readonly$$ %
IGeneracionArchivosLogica$$ 4$
generacionArchivosLogica$$5 M
;$$M N
	protected%% 
readonly%% )
IFacturacionElectronicaLogica%% 8(
facturacionElectronicaLogica%%9 U
;%%U V
	protected&& 
readonly&& 
IMailer&& "
mailer&&# )
;&&) *
	protected'' 
readonly'' 
IBarCodeUtil'' '
barCodeUtil''( 3
;''3 4
public)) #
CommonAlmacenController)) &
())& '
)))' (
:))) *
base))+ /
())/ 0
)))0 1
{** 	
actorNegocioLogica++ 
=++  
Dependencia++! ,
.++, -
Resolve++- 4
<++4 5
IActorNegocioLogica++5 H
>++H I
(++I J
)++J K
;++K L

=,, 
Dependencia,, '
.,,' (
Resolve,,( /
<,,/ 0
IMaestroLogica,,0 >
>,,> ?
(,,? @
),,@ A
;,,A B
operacionLogica-- 
=-- 
Dependencia-- )
.--) *
Resolve--* 1
<--1 2
IOperacionLogica--2 B
>--B C
(--C D
)--D E
;--E F
configuracionLogica.. 
=..  !
Dependencia.." -
...- .
Resolve... 5
<..5 6 
IConfiguracionLogica..6 J
>..J K
(..K L
)..L M
;..M N
mailer// 
=// 
Dependencia//  
.//  !
Resolve//! (
<//( )
IMailer//) 0
>//0 1
(//1 2
)//2 3
;//3 4
barCodeUtil00 
=00 
Dependencia00 %
.00% &
Resolve00& -
<00- .
IBarCodeUtil00. :
>00: ;
(00; <
)00< =
;00= >$
generacionArchivosLogica11 $
=11% &
Dependencia11' 2
.112 3
Resolve113 :
<11: ;%
IGeneracionArchivosLogica11; T
>11T U
(11U V
)11V W
;11W X(
facturacionElectronicaLogica22 (
=22) *
Dependencia22+ 6
.226 7
Resolve227 >
<22> ?)
IFacturacionElectronicaLogica22? \
>22\ ]
(22] ^
)22^ _
;22_ `%
inventarioHistoricoLogica33 %
=33& '
Dependencia33( 3
.333 4
Resolve334 ;
<33; <'
IInventarioHistorico_Logica33< W
>33W X
(33X Y
)33Y Z
;33Z [
}55 	
public77  
ComprobanteDeAlmacen77 #$
ObtenerMovimientoAlmacen77$ <
(77< =4
(EstablecimientoComercialExtendidoConLogo77= e
sede77f j
,77j k
List77l p
<77p q
	Proveedor77q z
>77z {
proveedores	77| �
,
77� �
List
77� �
<
77� �
Detalle_maestro
77� �
>
77� �
modalidades
77� �
,
77� �
List
77� �
<
77� �
Detalle_maestro
77� �
>
77� �
motivos
77� �
,
77� �"
ComprobanteDeAlmacen
77� �"
comprobanteDeAlmacen
77� �
)
77� �
{88 	
try99 
{:: 
MovimientoDeAlmacen;; #
movimientoDeAlmacen;;$ 7
=;;8 9
operacionLogica;;: I
.;;I J)
ObtenerMovimientoDeMercaderia;;J g
(;;g h 
comprobanteDeAlmacen;;h |
.;;| }
Id;;} 
)	;; �
;
;;� �
if== 
(== 
movimientoDeAlmacen== '
.==' (
IdTipoComprobante==( 9
====: <
MaestroSettings=== L
.==L M
Default==M T
.==T U?
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente	==U �
)
==� �
{>> 
var?? 
ubigeos?? 
=??  !

.??/ 0

(??= >
new??> A
int??B E
[??E F
]??F G
{??H I
Convert??J Q
.??Q R
ToInt32??R Y
(??Y Z
movimientoDeAlmacen??Z m
.??m n%
IdUbigeoOrigenDeTraslado	??n �
(
??� �
)
??� �
)
??� �
,
??� �
Convert
??� �
.
??� �
ToInt32
??� �
(
??� �!
movimientoDeAlmacen
??� �
.
??� �'
IdUbigeoDestinoDeTraslado
??� �
(
??� �
)
??� �
)
??� �
}
??� �
)
??� �
;
??� �
movimientoDeAlmacen@@ '
.@@' (
UbigeoOrigen@@( 4
=@@5 6
ubigeos@@7 >
.@@> ?
Single@@? E
(@@E F
u@@F G
=>@@H J
u@@K L
.@@L M
id@@M O
==@@P R
Convert@@S Z
.@@Z [
ToInt32@@[ b
(@@b c
movimientoDeAlmacen@@c v
.@@v w%
IdUbigeoOrigenDeTraslado	@@w �
(
@@� �
)
@@� �
)
@@� �
)
@@� �
.
@@� �
descripcion_corta
@@� �
;
@@� �
movimientoDeAlmacenAA '
.AA' (

=AA6 7
ubigeosAA8 ?
.AA? @
SingleAA@ F
(AAF G
uAAG H
=>AAI K
uAAL M
.AAM N
idAAN P
==AAQ S
ConvertAAT [
.AA[ \
ToInt32AA\ c
(AAc d
movimientoDeAlmacenAAd w
.AAw x&
IdUbigeoDestinoDeTraslado	AAx �
(
AA� �
)
AA� �
)
AA� �
)
AA� �
.
AA� �
descripcion_corta
AA� �
;
AA� �
varBB 
QrBytesBB 
=BB  !
barCodeUtilBB" -
.BB- .
ObtenerCodigoQRBB. =
(BB= >
movimientoDeAlmacenBB> Q
.BBQ R
InformacionBBR ]
)BB] ^
;BB^ _ 
comprobanteDeAlmacenCC (
.CC( )%
CadenaHtmlDeComprobante80CC) B
=CCC D!
CoreHtmlStringBuilderCCE Z
.CCZ [
ObtenerHtmlStringCC[ l
(CCl m 
movimientoDeAlmacen	CCm �
,
CC� �
FormatoImpresion
CC� �
.
CC� �
_80mm
CC� �
,
CC� �
QrBytes
CC� �
,
CC� �
sede
CC� �
,
CC� �
proveedores
CC� �
,
CC� �
modalidades
CC� �
,
CC� �
motivos
CC� �
,
CC� �
this
CC� �
)
CC� �
;
CC� � 
comprobanteDeAlmacenDD (
.DD( )%
CadenaHtmlDeComprobanteA4DD) B
=DDC D!
CoreHtmlStringBuilderDDE Z
.DDZ [
ObtenerHtmlStringDD[ l
(DDl m 
movimientoDeAlmacen	DDm �
,
DD� �
FormatoImpresion
DD� �
.
DD� �
A4
DD� �
,
DD� �
QrBytes
DD� �
,
DD� �
sede
DD� �
,
DD� �
proveedores
DD� �
,
DD� �
modalidades
DD� �
,
DD� �
motivos
DD� �
,
DD� �
this
DD� �
)
DD� �
;
DD� �
}EE 
elseFF 
ifFF 
(FF 
movimientoDeAlmacenFF ,
.FF, -
IdTipoComprobanteFF- >
==FF? A
MaestroSettingsFFB Q
.FFQ R
DefaultFFR Y
.FFY Z:
-IdDetalleMaestroComprobanteNotaAlmacenInterna	FFZ �
)
FF� �
{GG 
varHH 
	QrContentHH !
=HH" #(
facturacionElectronicaLogicaHH$ @
.HH@ A
	ObtenerQRHHA J
(HHJ K
movimientoDeAlmacenHHK ^
,HH^ _
sedeHH` d
)HHd e
;HHe f
varII 
QrBytesII 
=II  !
barCodeUtilII" -
.II- .
ObtenerCodigoQRII. =
(II= >
	QrContentII> G
)IIG H
;IIH I 
comprobanteDeAlmacenJJ (
.JJ( )%
CadenaHtmlDeComprobante80JJ) B
=JJC D!
CoreHtmlStringBuilderJJE Z
.JJZ [
ObtenerHtmlStringJJ[ l
(JJl m 
movimientoDeAlmacen	JJm �
,
JJ� �
FormatoImpresion
JJ� �
.
JJ� �
_80mm
JJ� �
,
JJ� �
QrBytes
JJ� �
,
JJ� �
sede
JJ� �
,
JJ� �
proveedores
JJ� �
,
JJ� �
modalidades
JJ� �
,
JJ� �
motivos
JJ� �
,
JJ� �
this
JJ� �
)
JJ� �
;
JJ� � 
comprobanteDeAlmacenKK (
.KK( )%
CadenaHtmlDeComprobanteA4KK) B
=KKC D!
CoreHtmlStringBuilderKKE Z
.KKZ [
ObtenerHtmlStringKK[ l
(KKl m 
movimientoDeAlmacen	KKm �
,
KK� �
FormatoImpresion
KK� �
.
KK� �
A4
KK� �
,
KK� �
QrBytes
KK� �
,
KK� �
sede
KK� �
,
KK� �
proveedores
KK� �
,
KK� �
modalidades
KK� �
,
KK� �
motivos
KK� �
,
KK� �
this
KK� �
)
KK� �
;
KK� �
}LL 
elseMM 
{NN  
comprobanteDeAlmacenQQ (
=QQ) *
newQQ+ . 
ComprobanteDeAlmacenQQ/ C
(QQC D
)QQD E
{QQF G
IdQQH J
=QQK L
movimientoDeAlmacenQQM `
.QQ` a!
IdOperacionReferenciaQQa v
(QQv w
)QQw x
}QQy z
;QQz {)
ObtenerOrdenMovimientoAlmacenRR 1
(RR1 2
sedeRR2 6
,RR6 7 
comprobanteDeAlmacenRR8 L
,RRL M
movimientoDeAlmacenRRN a
.RRa b1
$IdTipoTransaccionOperacionReferencia	RRb �
(
RR� �
)
RR� �
)
RR� �
;
RR� �
}SS 
returnTT  
comprobanteDeAlmacenTT +
;TT+ ,
}UU 
catchVV 
(VV 
	ExceptionVV 
eVV 
)VV 
{WW 
throwXX 
newXX 
ControllerExceptionXX -
(XX- .
$strXX. b
,XXb c
eXXd e
)XXe f
;XXf g
}YY 
}ZZ 	
public\\  
ComprobanteDeAlmacen\\ #)
ObtenerOrdenMovimientoAlmacen\\$ A
(\\A B4
(EstablecimientoComercialExtendidoConLogo\\B j
sede\\k o
,\\o p!
ComprobanteDeAlmacen	\\q �"
comprobanteDeAlmacen
\\� �
,
\\� �
int
\\� �
idTipoTransaccion
\\� �
)
\\� �
{]] 	
try^^ 
{__ 
if`` 
(`` 
Diccionario`` 
.``  :
.TiposDeTransaccionOrdenesDeOperacionesDeVentas``  N
.``N O
Contains``O W
(``W X
idTipoTransaccion``X i
)``i j
)``j k
{aa 
OrdenDeVentabb  
ordenDeVentabb! -
=bb. /
operacionLogicabb0 ?
.bb? @
ObtenerOrdenDeVentabb@ S
(bbS T 
comprobanteDeAlmacenbbT h
.bbh i
Idbbi k
)bbk l
;bbl m
varcc 
QrContentOrdencc &
=cc' ((
facturacionElectronicaLogicacc) E
.ccE F
	ObtenerQRccF O
(ccO P
ordenDeVentaccP \
,cc\ ]
sedecc^ b
)ccb c
;ccc d
vardd 
QrBytesOrdendd $
=dd% &
barCodeUtildd' 2
.dd2 3
ObtenerCodigoQRdd3 B
(ddB C
QrContentOrdenddC Q
)ddQ R
;ddR S 
comprobanteDeAlmacenee (
.ee( )%
CadenaHtmlDeComprobante80ee) B
=eeC D!
CoreHtmlStringBuildereeE Z
.eeZ [
ObtenerHtmlStringee[ l
(eel m
ordenDeVentaeem y
,eey z
FormatoImpresion	ee{ �
.
ee� �
_80mm
ee� �
,
ee� �
QrBytesOrden
ee� �
,
ee� �
sede
ee� �
,
ee� �
this
ee� �
,
ee� �

ee� �
)
ee� �
;
ee� � 
comprobanteDeAlmacenff (
.ff( )%
CadenaHtmlDeComprobanteA4ff) B
=ffC D!
CoreHtmlStringBuilderffE Z
.ffZ [
ObtenerHtmlStringff[ l
(ffl m
ordenDeVentaffm y
,ffy z
FormatoImpresion	ff{ �
.
ff� �
A4
ff� �
,
ff� �
QrBytesOrden
ff� �
,
ff� �
sede
ff� �
,
ff� �
this
ff� �
,
ff� �

ff� �
)
ff� �
;
ff� �
}gg 
elsehh 
ifhh 
(hh 
Diccionariohh $
.hh$ %;
/TiposDeTransaccionOrdenesDeOperacionesDeComprashh% T
.hhT U
ContainshhU ]
(hh] ^
idTipoTransaccionhh^ o
)hho p
)hhp q
{ii 


=jj0 1
operacionLogicajj2 A
.jjA B 
ObtenerOrdenDeComprajjB V
(jjV W 
comprobanteDeAlmacenjjW k
.jjk l
Idjjl n
)jjn o
;jjo p 
comprobanteDeAlmacenkk (
.kk( )%
CadenaHtmlDeComprobante80kk) B
=kkC D!
CoreHtmlStringBuilderkkE Z
.kkZ [
ObtenerHtmlStringkk[ l
(kkl m

,kkz {
FormatoImpresion	kk| �
.
kk� �
_80mm
kk� �
,
kk� �
null
kk� �
,
kk� �
sede
kk� �
,
kk� �
this
kk� �
,
kk� �

kk� �
)
kk� �
;
kk� � 
comprobanteDeAlmacenll (
.ll( )%
CadenaHtmlDeComprobanteA4ll) B
=llC D!
CoreHtmlStringBuilderllE Z
.llZ [
ObtenerHtmlStringll[ l
(lll m

,llz {
FormatoImpresion	ll| �
.
ll� �
A4
ll� �
,
ll� �
null
ll� �
,
ll� �
sede
ll� �
,
ll� �
this
ll� �
,
ll� �

ll� �
)
ll� �
;
ll� �
}mm 
returnoo  
comprobanteDeAlmacenoo +
;oo+ ,
}pp 
catchqq 
(qq 
	Exceptionqq 
eqq 
)qq 
{rr 
throwss 
newss 
ControllerExceptionss -
(ss- .
$strss. l
,ssl m
essn o
)sso p
;ssp q
}tt 
}uu 	
}vv 
}ww �u
fD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\OrdenAlmacenController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class "
OrdenAlmacenController '
:( )#
CommonAlmacenController* A
{ 
	protected   
readonly    
IOrdenAlmacen_Logica   /
ordenAlmacenLogica  0 B
;  B C
public"" "
OrdenAlmacenController"" %
(""% &
)""& '
:""( )
base""* .
("". /
)""/ 0
{## 	
ordenAlmacenLogica$$ 
=$$  
Dependencia$$! ,
.$$, -
Resolve$$- 4
<$$4 5 
IOrdenAlmacen_Logica$$5 I
>$$I J
($$J K
)$$K L
;$$L M
}%% 	
['' 	
	Authorize''	 
('' 
Roles'' 
='' 
$str'' <
)''< =
]''= >
public(( 
ActionResult(( 
	Principal(( %
(((% &
)((& '
{)) 	
ViewBag** 
.** 
Data** 
=** 
ordenAlmacenLogica** -
.**- .1
%ObtenerDatosParaOrdenAlmacenPrincipal**. S
(**S T
ProfileData**T _
(**_ `
)**` a
)**a b
;**b c
return@@ 
View@@ 
(@@ 
)@@ 
;@@ 
}AA 	
publicCC 

JsonResultCC !
ObtenerOrdenesAlmacenCC /
(CC/ 0
stringCC0 6
desdeCC7 <
,CC< =
stringCC> D
hastaCCE J
,CCJ K
boolCCL P
porIngresarCCQ \
,CC\ ]
boolCC^ b
entregaInmediataCCc s
,CCs t
boolCCu y
entregaDiferida	CCz �
,
CC� �
bool
CC� �
estadoPendiente
CC� �
,
CC� �
bool
CC� �

CC� �
,
CC� �
bool
CC� �
estadoCompletada
CC� �
,
CC� �
int
CC� �
[
CC� �
]
CC� �
idsAlmacenes
CC� �
)
CC� �
{DD 	
tryEE 
{FF 
DateTimeGG 

fechaDesdeGG #
=GG$ %
DateTimeGG& .
.GG. /
ParseGG/ 4
(GG4 5
desdeGG5 :
)GG: ;
;GG; <
DateTimeHH 

fechaHastaHH #
=HH$ %
DateTimeHH& .
.HH. /
ParseHH/ 4
(HH4 5
hastaHH5 :
+HH; <
$strHH= H
)HHH I
;HHI J
ListII 
<II 
OrdenAlmacenResumenII (
>II( )
ordenesAlmacenII* 8
=II9 :
ordenAlmacenLogicaII; M
.IIM N!
ObtenerOrdenesAlmacenIIN c
(IIc d

fechaDesdeIId n
,IIn o

fechaHastaIIp z
,IIz {
porIngresar	II| �
,
II� �
entregaInmediata
II� �
,
II� �
entregaDiferida
II� �
,
II� �
estadoPendiente
II� �
,
II� �

II� �
,
II� �
estadoCompletada
II� �
,
II� �
idsAlmacenes
II� �
,
II� �
ProfileData
II� �
(
II� �
)
II� �
)
II� �
;
II� �
returnJJ 
JsonJJ 
(JJ 
ordenesAlmacenJJ *
)JJ* +
;JJ+ ,
}KK 
catchLL 
(LL 
	ExceptionLL 
eLL 
)LL 
{MM 
returnNN 
newNN  
JsonHttpStatusResultNN /
(NN/ 0
UtilNN0 4
.NN4 5
	ErrorJsonNN5 >
(NN> ?
eNN? @
)NN@ A
,NNA B
HttpStatusCodeNNC Q
.NNQ R
InternalServerErrorNNR e
)NNe f
;NNf g
}OO 
}PP 	
publicRR 
asyncRR 
TaskRR 
<RR 

JsonResultRR $
>RR$ %
ObtenerOrdenAlmacenRR& 9
(RR9 :
longRR: >
idOrdenAlmacenRR? M
,RRM N
boolRRO S
porIngresarRRT _
)RR_ `
{SS 	
tryTT 
{UU 
OrdenAlmacenVV 
ordenAlmacenVV )
=VV* +
ordenAlmacenLogicaVV, >
.VV> ?
ObtenerOrdenAlmacenVV? R
(VVR S
idOrdenAlmacenVVS a
,VVa b
porIngresarVVc n
)VVn o
;VVo p
varWW 
sedeWW 
=WW 
ObtenerSedeWW &
(WW& '
)WW' (
;WW( )
varXX 
proveedoresXX 
=XX  !
newXX" %
ListXX& *
<XX* +
	ProveedorXX+ 4
>XX4 5
(XX5 6
)XX6 7
;XX7 8
varYY !
modalidadesDeTrasladoYY )
=YY* +
newYY, /
ListYY0 4
<YY4 5
Detalle_maestroYY5 D
>YYD E
(YYE F
)YYF G
;YYG H
varZZ 
motivosDeTrasladoZZ %
=ZZ& '
newZZ( +
ListZZ, 0
<ZZ0 1
Detalle_maestroZZ1 @
>ZZ@ A
(ZZA B
)ZZB C
;ZZC D
var[[ %
idsComprobantesMovimiento[[ -
=[[. /
new[[0 3
List[[4 8
<[[8 9
int[[9 <
>[[< =
{[[> ?
MaestroSettings[[@ O
.[[O P
Default[[P W
.[[W X:
-IdDetalleMaestroComprobanteNotaAlmacenInterna	[[X �
,
[[� �
MaestroSettings
[[� �
.
[[� �
Default
[[� �
.
[[� �@
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente
[[� �
}
[[� �
;
[[� �
if\\ 
(\\ 
ordenAlmacen\\  
.\\  !
Movimientos\\! ,
.\\, -
Select\\- 3
(\\3 4
m\\4 5
=>\\6 8
m\\9 :
.\\: ;
IdTipoComprobante\\; L
)\\L M
.\\M N
ToList\\N T
(\\T U
)\\U V
.\\V W
	Intersect\\W `
(\\` a%
idsComprobantesMovimiento\\a z
)\\z {
.\\{ |
Any\\| 
(	\\ �
)
\\� �
)
\\� �
{]] 
proveedores^^ 
=^^  !
actorNegocioLogica^^" 4
.^^4 5&
ObtenerProveedoresVigentes^^5 O
(^^O P
)^^P Q
;^^Q R!
modalidadesDeTraslado__ )
=__* +
await__, 1

.__? @+
ObtenerModalidadesTrasladoAsync__@ _
(___ `
)__` a
;__a b
motivosDeTraslado`` %
=``& '
await``( -

.``; <'
ObtenerMotivosTrasladoAsync``< W
(``W X
)``X Y
;``Y Z
}aa 
foreachbb 
(bb 
varbb 
ordenbb "
inbb# %
ordenAlmacenbb& 2
.bb2 3
Ordenesbb3 :
)bb: ;
{cc 
ordendd 
.dd 
Comprobantedd %
=dd& ')
ObtenerOrdenMovimientoAlmacendd( E
(ddE F
sededdF J
,ddJ K
ordenddL Q
.ddQ R
ComprobanteddR ]
,dd] ^
ordendd_ d
.ddd e
IdTipoTransacciondde v
)ddv w
;ddw x
}ee 
foreachff 
(ff 
varff 

movimientoff '
inff( *
ordenAlmacenff+ 7
.ff7 8
Movimientosff8 C
)ffC D
{gg 

movimientohh 
.hh 
Comprobantehh *
=hh+ ,$
ObtenerMovimientoAlmacenhh- E
(hhE F
sedehhF J
,hhJ K
proveedoreshhL W
,hhW X!
modalidadesDeTrasladohhY n
,hhn o
motivosDeTraslado	hhp �
,
hh� �

movimiento
hh� �
.
hh� �
Comprobante
hh� �
)
hh� �
;
hh� �
}ii 
returnjj 
Jsonjj 
(jj 
ordenAlmacenjj (
)jj( )
;jj) *
}kk 
catchll 
(ll 
	Exceptionll 
ell 
)ll 
{mm 
returnnn 
newnn  
JsonHttpStatusResultnn /
(nn/ 0
Utilnn0 4
.nn4 5
	ErrorJsonnn5 >
(nn> ?
enn? @
)nn@ A
,nnA B
HttpStatusCodennC Q
.nnQ R
InternalServerErrornnR e
)nne f
;nnf g
}oo 
}pp 	
publicrr 

JsonResultrr 1
%ObtenerRegistroMovimientoOrdenAlmacenrr ?
(rr? @
longrr@ D
idOrdenAlmacenrrE S
,rrS T
boolrrU Y
porIngresarrrZ e
)rre f
{ss 	
trytt 
{uu 
RegistroMovimientoAlmacenvv )*
registroMovimientoOrdenAlmacenvv* H
=vvI J
ordenAlmacenLogicavvK ]
.vv] ^2
%ObtenerRegistroMovimientoOrdenAlmacen	vv^ �
(
vv� �
idOrdenAlmacen
vv� �
,
vv� �
porIngresar
vv� �
,
vv� �
ProfileData
vv� �
(
vv� �
)
vv� �
)
vv� �
;
vv� �
returnww 
Jsonww 
(ww *
registroMovimientoOrdenAlmacenww :
)ww: ;
;ww; <
}xx 
catchyy 
(yy 
	Exceptionyy 
eyy 
)yy 
{zz 
return{{ 
new{{  
JsonHttpStatusResult{{ /
({{/ 0
Util{{0 4
.{{4 5
	ErrorJson{{5 >
({{> ?
e{{? @
){{@ A
,{{A B
HttpStatusCode{{C Q
.{{Q R
InternalServerError{{R e
){{e f
;{{f g
}|| 
}}} 	
public 
async 
Task 
< 

JsonResult $
>$ %)
GuardarMovimientoOrdenAlmacen& C
(C D%
RegistroMovimientoAlmacenD ]"
movimientoOrdenAlmacen^ t
)t u
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' ( 
ordenAlmacenLogica
��) ;
.
��; <+
GuardarMovimientoOrdenAlmacen
��< Y
(
��Y Z$
movimientoOrdenAlmacen
��Z p
,
��p q
ProfileData
��r }
(
��} ~
)
��~ 
)�� �
;��� �
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str��8 �
)��� �
;��� �
if
�� 
(
�� $
movimientoOrdenAlmacen
�� *
.
��* +
TipoDeComprobante
��+ <
.
��< =
TipoComprobante
��= L
.
��L M
Id
��M O
==
��P R
MaestroSettings
��S b
.
��b c
Default
��c j
.
��j kA
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��k �
)��� �
{
�� 
string
�� 
path
�� 
=
��  ! 
HostingEnvironment
��" 4
.
��4 5%
ApplicationPhysicalPath
��5 L
;
��L M
await
�� *
facturacionElectronicaLogica
�� 6
.
��6 7,
TransmitirEnviarGuiaDeRemision
��7 U
(
��U V
(
��V W
(
��W X
OrdenDeVenta
��X d
)
��d e
result
��e k
.
��k l
objeto
��l r
)
��r s
.
��s t
Transaccion
��t 
(�� �
)��� �
.��� �

.��� �
First��� �
(��� �
)��� �
.��� �
id��� �
,��� �
ProfileData��� �
(��� �
)��� �
.��� �
Sede��� �
,��� �
ProfileData��� �
(��� �
)��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �
path��� �
)��� �
;��� �
}
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
data
��6 :
=
��; <
result
��= C
.
��C D
information
��D O
,
��O P 
result_description
��Q c
=
��d e
result
��f l
.
��l m
title
��m r
}
��r s
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� -
InvalidarMovimientoOrdenAlmacen
�� 9
(
��9 :
long
��: >&
idMovimientoOrdenAlmacen
��? W
,
��W X
string
��Y _
observacion
��` k
)
��k l
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' ( 
ordenAlmacenLogica
��) ;
.
��; <-
InvalidarMovimientoOrdenAlmacen
��< [
(
��[ \&
idMovimientoOrdenAlmacen
��\ t
,
��t u
observacion��v �
,��� �
ProfileData��� �
(��� �
)��� �
)��� �
;��� �
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 x
)
��x y
;
��y z
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
data
��6 :
=
��; <
result
��= C
.
��C D
information
��D O
,
��O P 
result_description
��Q c
=
��d e
result
��f l
.
��l m
title
��m r
}
��s t
)
��t u
;
��u v
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
}
�� 
}�� δ

aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\AlmacenController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class 
AlmacenController "
:# $
BaseController% 3
{   
	protected!! 
readonly!! 
IActorNegocioLogica!! .
actorNegocioLogica!!/ A
;!!A B
	protected"" 
readonly"" 
IMaestroLogica"" )

;""7 8
	protected## 
readonly## 
ISede_Logica## '

sedeLogica##( 2
;##2 3
	protected$$ 
readonly$$ 
IOperacionLogica$$ +
operacionLogica$$, ;
;$$; <
	protected%% 
readonly%% '
IInventarioHistorico_Logica%% 6%
inventarioHistoricoLogica%%7 P
;%%P Q
	protected&& 
readonly&&  
IConfiguracionLogica&& /
configuracionLogica&&0 C
;&&C D
	protected'' 
readonly'' %
IGeneracionArchivosLogica'' 4$
generacionArchivosLogica''5 M
;''M N
	protected(( 
readonly(( )
IFacturacionElectronicaLogica(( 8(
facturacionElectronicaLogica((9 U
;((U V
	protected)) 
readonly)) 
IMailer)) "
mailer))# )
;))) *
	protected** 
readonly** 
IBarCodeUtil** '
barCodeUtil**( 3
;**3 4
public,, 
AlmacenController,,  
(,,  !
),,! "
:,,# $
base,,% )
(,,) *
),,* +
{-- 	
actorNegocioLogica.. 
=..  
Dependencia..! ,
..., -
Resolve..- 4
<..4 5
IActorNegocioLogica..5 H
>..H I
(..I J
)..J K
;..K L

=// 
Dependencia// '
.//' (
Resolve//( /
</// 0
IMaestroLogica//0 >
>//> ?
(//? @
)//@ A
;//A B

sedeLogica00 
=00 
Dependencia00 $
.00$ %
Resolve00% ,
<00, -
ISede_Logica00- 9
>009 :
(00: ;
)00; <
;00< =
operacionLogica11 
=11 
Dependencia11 )
.11) *
Resolve11* 1
<111 2
IOperacionLogica112 B
>11B C
(11C D
)11D E
;11E F
configuracionLogica22 
=22  !
Dependencia22" -
.22- .
Resolve22. 5
<225 6 
IConfiguracionLogica226 J
>22J K
(22K L
)22L M
;22M N
mailer33 
=33 
Dependencia33  
.33  !
Resolve33! (
<33( )
IMailer33) 0
>330 1
(331 2
)332 3
;333 4
barCodeUtil44 
=44 
Dependencia44 %
.44% &
Resolve44& -
<44- .
IBarCodeUtil44. :
>44: ;
(44; <
)44< =
;44= >$
generacionArchivosLogica55 $
=55% &
Dependencia55' 2
.552 3
Resolve553 :
<55: ;%
IGeneracionArchivosLogica55; T
>55T U
(55U V
)55V W
;55W X(
facturacionElectronicaLogica66 (
=66) *
Dependencia66+ 6
.666 7
Resolve667 >
<66> ?)
IFacturacionElectronicaLogica66? \
>66\ ]
(66] ^
)66^ _
;66_ `%
inventarioHistoricoLogica77 %
=77& '
Dependencia77( 3
.773 4
Resolve774 ;
<77; <'
IInventarioHistorico_Logica77< W
>77W X
(77X Y
)77Y Z
;77Z [
}99 	
public<< 
ActionResult<< 
OrdenesDeAlmacen<< ,
(<<, -
)<<- .
{== 	
List>> 
<>> 
string>> 
>>> 
fechas>> 
=>>  !
operacionLogica>>" 1
.>>1 23
'ObtenerFechaIncioyFinParaReporteAlmacen>>2 Y
(>>Y Z
)>>Z [
;>>[ \
ViewBag?? 
.?? 
fechaInicio?? 
=??  !
fechas??" (
[??( )
$num??) *
]??* +
;??+ ,
ViewBag@@ 
.@@ 
fechaFin@@ 
=@@ 
fechas@@ %
[@@% &
$num@@& '
]@@' (
;@@( )
ViewBagAA 
.AA '
idEstablecimientoPorDefectoAA /
=AA0 1
ProfileDataAA2 =
(AA= >
)AA> ?
.AA? @(
CentroDeAtencionSeleccionadoAA@ \
.AA\ ]$
EstablecimientoComercialAA] u
.AAu v
IdAAv x
;AAx y
ViewBagBB 
.BB (
idCentroDeAtencionPorDefectoBB 0
=BB1 2
ProfileDataBB3 >
(BB> ?
)BB? @
.BB@ A(
CentroDeAtencionSeleccionadoBBA ]
.BB] ^
IdBB^ `
;BB` a
ViewBagCC 
.CC &
idTipoActorPersonaJuridicaCC .
=CC/ 0

.CC> ?
DefaultCC? F
.CCF G&
IdTipoActorPersonaJuridicaCCG a
;CCa b
ViewBagDD 
.DD %
IdTipoActorPersonaNaturalDD -
=DD. /

.DD= >
DefaultDD> E
.DDE F%
IdTipoActorPersonaNaturalDDF _
;DD_ `
ViewBagEE 
.EE 
idProveedorGenericoEE '
=EE( )

.EE7 8
DefaultEE8 ?
.EE? @
idProveedorGenericoEE@ S
;EES T
ViewBagFF 
.FF /
#idTipoPersonaSeleccionadaPorDefectoFF 7
=FF8 9

.FFG H
DefaultFFH O
.FFO P/
#IdTipoPersonaSeleccionadaPorDefectoFFP s
;FFs t
ViewBagGG 
.GG <
0idTipoDocumentoSeleccionadaConTipoPersonaNaturalGG D
=GGE F

.GGT U
DefaultGGU \
.GG\ ]=
0IdTipoDocumentoSeleccionadaConTipoPersonaNatural	GG] �
;
GG� �
ViewBagHH 
.HH =
1idTipoDocumentoSeleccionadaConTipoPersonaJuridicaHH E
=HHF G

.HHU V
DefaultHHV ]
.HH] ^>
1IdTipoDocumentoSeleccionadaConTipoPersonaJuridica	HH^ �
;
HH� �
ViewBagII 
.II '
idTipoDocumentoIdentidadDniII /
=II0 1

.II? @
DefaultII@ G
.IIG H'
IdTipoDocumentoIdentidadDniIIH c
;IIc d
ViewBagJJ 
.JJ '
idTipoDocumentoIdentidadRucJJ /
=JJ0 1

.JJ? @
DefaultJJ@ G
.JJG H'
IdTipoDocumentoIdentidadRucJJH c
;JJc d
ViewBagKK 
.KK *
idUbigeoSeleccionadoPorDefectoKK 2
=KK3 4

.KKB C
DefaultKKC J
.KKJ K5
)idUbigeoSeleccionadoPorDefectoEnProveedorKKK t
;KKt u
ViewBagLL 
.LL "
idUbigeoNoEspecificadoLL *
=LL+ ,

.LL: ;
DefaultLL; B
.LLB C"
idUbigeoNoEspecificadoLLC Y
;LLY Z
ViewBagMM 
.MM *
idDocumentoNotaAlamacenInternaMM 2
=MM3 4
MaestroSettingsMM5 D
.MMD E
DefaultMME L
.MML M9
-IdDetalleMaestroComprobanteNotaAlmacenInternaMMM z
;MMz {
ViewBagNN 
.NN 

=NN" #
(NN$ %
ObtenerSedeNN% 0
(NN0 1
)NN1 2
.NN2 3
DomicilioFiscalNN3 B
!=NNC E
nullNNF J
)NNJ K
?NNL M
ObtenerSedeNNN Y
(NNY Z
)NNZ [
.NN[ \
DomicilioFiscalNN\ k
.NNk l
DetalleNNl s
:NNt u
$strNNv y
;NNy z
ViewBagOO 
.OO 
idUbigeoSedeOO  
=OO! "
(OO# $
ObtenerSedeOO$ /
(OO/ 0
)OO0 1
.OO1 2
DomicilioFiscalOO2 A
!=OOB D
nullOOE I
)OOI J
?OOK L
ObtenerSedeOOM X
(OOX Y
)OOY Z
.OOZ [
DomicilioFiscalOO[ j
.OOj k
UbigeoOOk q
.OOq r
IdOOr t
:OOu v
$numOOw x
;OOx y
ViewBagPP 
.PP )
idModalidadTrasladoPorDefectoPP 1
=PP2 3
MaestroSettingsPP4 C
.PPC D
DefaultPPD K
.PPK LA
4IdDetalleMaestroModalidadDeTrasladoTransportePublico	PPL �
;
PP� �
ViewBagQQ 
.QQ &
idMotivoTrasladoPorDefectoQQ .
=QQ/ 0
MaestroSettingsQQ1 @
.QQ@ A
DefaultQQA H
.QQH I5
)IdDetalleMaestroMotivoDeTrasladoPorCompraQQI r
;QQr s
ViewBagRR 
.RR %
idTransportistaPorDefectoRR -
=RR. /
AplicacionSettingsRR0 B
.RRB C
DefaultRRC J
.RRJ K9
-IdTransportistaPorDefectoEnSalidaDeMercaderiaRRK x
;RRx y
ViewBagSS 
.SS )
idTipoDeComprobantePorDefectoSS 1
=SS2 3
AplicacionSettingsSS4 F
.SSF G
DefaultSSG N
.SSN O>
1IdTipoDeComprobantePorDefectoEnSalidaDeMercaderia	SSO �
;
SS� �
ViewBagTT 
.TT 
	WCPScriptTT 
=TT 
WebClientPrintTT  .
.TT. /
CreateScriptTT/ ;
(TT; <
UrlUU 
.UU 
ActionUU 
(UU 
$strUU '
,UU' (
$strUU) <
,UU< =
nullUU> B
,UUB C
HttpContextUUD O
.UUO P
RequestUUP W
.UUW X
UrlUUX [
.UU[ \
SchemeUU\ b
)UUb c
,UUc d
UrlVV 
.VV 
ActionVV 
(VV 
$strVV "
,VV" #
$strVV$ -
,VV- .
nullVV/ 3
,VV3 4
HttpContextVV5 @
.VV@ A
RequestVVA H
.VVH I
UrlVVI L
.VVL M
SchemeVVM S
)VVS T
,VVT U
HttpContextVVV a
.VVa b
SessionVVb i
.VVi j
	SessionIDVVj s
)VVs t
;VVt u
returnWW 
ViewWW 
(WW 
)WW 
;WW 
}XX 	
publicZZ 
ActionResultZZ 
TrasladosInternosZZ -
(ZZ- .
)ZZ. /
{[[ 	
List\\ 
<\\ 
string\\ 
>\\ 
fechas\\ 
=\\  !
operacionLogica\\" 1
.\\1 23
'ObtenerFechaIncioyFinParaReporteAlmacen\\2 Y
(\\Y Z
)\\Z [
;\\[ \
ViewBag]] 
.]] 
fechaInicio]] 
=]]  !
fechas]]" (
[]]( )
$num]]) *
]]]* +
;]]+ ,
ViewBag^^ 
.^^ 
fechaFin^^ 
=^^ 
fechas^^ %
[^^% &
$num^^& '
]^^' (
;^^( )
ViewBag__ 
.__ "
NombreEmpleadoDeSesion__ *
=__+ ,
ProfileData__- 8
(__8 9
)__9 :
.__: ;
Empleado__; C
.__C D
NombresYApellidos__D U
;__U V
ViewBag`` 
.`` "
NombreCentroDeAtencion`` *
=``+ ,
ProfileData``- 8
(``8 9
)``9 :
.``: ;(
CentroDeAtencionSeleccionado``; W
.``W X
Nombre``X ^
;``^ _
ViewBagaa 
.aa *
idCentroDeAtencionSeleccionadoaa 2
=aa3 4
ProfileDataaa5 @
(aa@ A
)aaA B
.aaB C(
CentroDeAtencionSeleccionadoaaC _
.aa_ `
Idaa` b
;aab c
ViewBagbb 
.bb &
mostrarBuscadorCodigoBarrabb .
=bb/ 0
AplicacionSettingsbb1 C
.bbC D
DefaultbbD K
.bbK L:
.MostrarBuscadorCodigoBarraEnTrasladoMercaderiabbL z
;bbz {
ViewBagcc 
.cc %
modoDeSeleccionDeConceptocc -
=cc. /
AplicacionSettingscc0 B
.ccB C
DefaultccC J
.ccJ KC
6ModoDeSeleccionDeConceptoDeNegocioEnTrasladoMercaderia	ccK �
;
cc� �
ViewBagdd 
.dd $
modoSeleccionTipoFamiliadd ,
=dd- .
AplicacionSettingsdd/ A
.ddA B
DefaultddB I
.ddI J<
0ModoDeSeleccionTipoDeFamiliaEnTrasladoMercaderiaddJ z
;ddz {
ViewBagee 
.ee %
numeroDecimalesEnCantidadee -
=ee. /
AplicacionSettingsee0 B
.eeB C
DefaulteeC J
.eeJ K%
NumeroDecimalesEnCantidadeeK d
;eed e
ViewBagff 
.ff *
minimoCaracteresBuscarConceptoff 2
=ff3 4
AplicacionSettingsff5 G
.ffG H
DefaultffH O
.ffO P:
.MinimoDeCaracteresParaBuscarEnSelectorConceptoffP ~
;ff~ 
ViewBaggg 
.gg (
tiempoEsperaBusquedaSelectorgg 0
=gg1 2
AplicacionSettingsgg3 E
.ggE F
DefaultggF M
.ggM N<
0TiempoDeEsperaEnBusquedaSelectoresDeGranCantidadggN ~
;gg~ 
ViewBaghh 
.hh 3
'salidaBienesSujetasADisponibilidadStockhh ;
=hh< =
!hh> ?
ProfileDatahh? J
(hhJ K
)hhK L
.hhL M2
&CentroAtencionQueTieneElStockIntegradahhM s
.hhs t!
SalidaBienesSinStock	hht �
;
hh� �
ViewBagii 
.ii '
informacionSelectorConceptoii /
=ii0 1
(ii2 3
intii3 6
)ii6 7'
InformacionSelectorConceptoii7 R
.iiR S
NombreiiS Y
;iiY Z
ViewBagkk 
.kk 
	WCPScriptkk 
=kk 
WebClientPrintkk  .
.kk. /
CreateScriptkk/ ;
(kk; <
Urlll 
.ll 
Actionll 
(ll 
$strll '
,ll' (
$strll) <
,ll< =
nullll> B
,llB C
HttpContextllD O
.llO P
RequestllP W
.llW X
UrlllX [
.ll[ \
Schemell\ b
)llb c
,llc d
Urlmm 
.mm 
Actionmm 
(mm 
$strmm "
,mm" #
$strmm$ -
,mm- .
nullmm/ 3
,mm3 4
HttpContextmm5 @
.mm@ A
RequestmmA H
.mmH I
UrlmmI L
.mmL M
SchememmM S
)mmS T
,mmT U
HttpContextmmV a
.mma b
Sessionmmb i
.mmi j
	SessionIDmmj s
)mms t
;mmt u
returnnn 
Viewnn 
(nn 
)nn 
;nn 
}oo 	
publicqq 
ActionResultqq  
MovimientosDeAlmacenqq 0
(qq0 1
)qq1 2
{rr 	
Listss 
<ss 
stringss 
>ss 
fechasss 
=ss  !
operacionLogicass" 1
.ss1 23
'ObtenerFechaIncioyFinParaReporteAlmacenss2 Y
(ssY Z
)ssZ [
;ss[ \
ViewBagtt 
.tt 
FechaIniciott 
=tt  !
fechastt" (
[tt( )
$numtt) *
]tt* +
;tt+ ,
ViewBaguu 
.uu 
FechaFinuu 
=uu 
fechasuu %
[uu% &
$numuu& '
]uu' (
;uu( )
ViewBagvv 
.vv '
IdEstablecimientoPorDefectovv /
=vv0 1
ProfileDatavv2 =
(vv= >
)vv> ?
.vv? @(
CentroDeAtencionSeleccionadovv@ \
.vv\ ]$
EstablecimientoComercialvv] u
.vvu v
Idvvv x
;vvx y
ViewBagww 
.ww (
IdCentroDeAtencionPorDefectoww 0
=ww1 2
ProfileDataww3 >
(ww> ?
)ww? @
.ww@ A(
CentroDeAtencionSeleccionadowwA ]
.ww] ^
Idww^ `
;ww` a
ViewBagxx 
.xx *
TieneRolAdministradorDeNegocioxx 2
=xx3 4
ProfileDataxx5 @
(xx@ A
)xxA B
.xxB C
EmpleadoxxC K
.xxK L
TieneRolxxL T
(xxT U

.xxb c
Defaultxxc j
.xxj k(
idRolAdministradorDeNegocio	xxk �
)
xx� �
;
xx� �
ViewBagzz 
.zz 
	WCPScriptzz 
=zz 
WebClientPrintzz  .
.zz. /
CreateScriptzz/ ;
(zz; <
Url{{ 
.{{ 
Action{{ 
({{ 
$str{{ '
,{{' (
$str{{) <
,{{< =
null{{> B
,{{B C
HttpContext{{D O
.{{O P
Request{{P W
.{{W X
Url{{X [
.{{[ \
Scheme{{\ b
){{b c
,{{c d
Url|| 
.|| 
Action|| 
(|| 
$str|| "
,||" #
$str||$ -
,||- .
null||/ 3
,||3 4
HttpContext||5 @
.||@ A
Request||A H
.||H I
Url||I L
.||L M
Scheme||M S
)||S T
,||T U
HttpContext||V a
.||a b
Session||b i
.||i j
	SessionID||j s
)||s t
;||t u
return}} 
View}} 
(}} 
)}} 
;}} 
}~~ 	
public
�� 
ActionResult
�� !
InventarioHistorico
�� /
(
��/ 0
)
��0 1
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� D
)
��D E
]
��E F
public
�� 

JsonResult
�� %
ObtenerOrdenesDeAlmacen
�� 1
(
��1 2
bool
��2 6

porRecibir
��7 A
,
��A B
int
��C F
[
��F G
]
��G H"
idsCentrosDeAtencion
��I ]
,
��] ^
string
��_ e
desde
��f k
,
��k l
string
��m s
hasta
��t y
)
��y z
{
�� 	
DateTime
�� 

fechaDesde
�� 
=
��  !
DateTimeUtil
��" .
.
��. /E
7ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos
��/ f
(
��f g
desde
��g l
)
��l m
;
��m n
DateTime
�� 

fechaHasta
�� 
=
��  !
DateTimeUtil
��" .
.
��. /E
7ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos
��/ f
(
��f g
hasta
��g l
)
��l m
;
��m n
try
�� 
{
�� 
List
�� 
<
�� $
Orden_Recibir_Entregar
�� +
>
��+ ,
	resultado
��- 6
=
��7 8

porRecibir
��9 C
?
��D E
operacionLogica
��F U
.
��U V/
!ObtenerOrdenesDeAlmacenPorRecibir
��V w
(
��w x
ProfileData��x �
(��� �
)��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �$
idsCentrosDeAtencion��� �
.��� �
ToList��� �
(��� �
)��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
:��� �
operacionLogica��� �
.��� �2
"ObtenerOrdenesDeAlmacenPorEntregar��� �
(��� �
ProfileData��� �
(��� �
)��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �$
idsCentrosDeAtencion��� �
.��� �
ToList��� �
(��� �
)��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	resultado
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� #
ObtenerOrdenDeAlmacen
�� /
(
��/ 0
long
��0 4
idOrdenDeAlmacen
��5 E
,
��E F
int
��G J
formato
��K R
)
��R S
{
�� 	
try
�� 
{
�� 
OrdenDeAlmacenViewModel
�� '
	respuesta
��( 1
=
��2 3
new
��4 7%
OrdenDeAlmacenViewModel
��8 O
(
��O P
)
��P Q
;
��Q R(
OrdenDeMovimientoDeAlmacen
�� *(
ordenDeMovimientoDeAlmacen
��+ E
=
��F G
operacionLogica
��H W
.
��W X/
!ObtenerOrdenDeMovimientoDeAlmacen
��X y
(
��y z
idOrdenDeAlmacen��z �
)��� �
;��� �
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
string
�� (
htmlStringDeOrdenDeAlmacen
�� 1
=
��2 3#
CoreHtmlStringBuilder
��4 I
.
��I J
ObtenerHtmlString
��J [
(
��[ \(
ordenDeMovimientoDeAlmacen
��\ v
,
��v w
(
��x y
FormatoImpresion��y �
)��� �
formato��� �
,��� �
null��� �
,��� �
sede��� �
,��� �
this��� �
)��� �
;��� �
	respuesta
�� 
=
�� 
new
�� %
OrdenDeAlmacenViewModel
��  7
(
��7 8(
ordenDeMovimientoDeAlmacen
��8 R
.
��R S
Id
��S U
,
��U V(
htmlStringDeOrdenDeAlmacen
��W q
)
��q r
;
��r s
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� :
,ObtenerOrdenDeAlmacenParaMovimientoDeAlmacen
�� F
(
��F G
long
��G K
idOrdenDeAlmacen
��L \
)
��\ ]
{
�� 	
try
�� 
{
�� 
OrdenDeMovimientoDeAlmacen
�� *
ordenDeAlmacen
��+ 9
=
��: ;
operacionLogica
��< K
.
��K L/
!ObtenerOrdenDeMovimientoDeAlmacen
��L m
(
��m n
idOrdenDeAlmacen
��n ~
)
��~ 
;�� �<
.OrdenDeAlmacenParaMovimientoDeAlmacenViewModel
�� >%
ordenDeAlmacenViewModel
��? V
=
��W X
new
��Y \=
.OrdenDeAlmacenParaMovimientoDeAlmacenViewModel��] �
(��� �
ordenDeAlmacen��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� %
ordenDeAlmacenViewModel
�� 3
)
��3 4
;
��4 5
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� $
ObtenerStockDeProducto
�� 0
(
��0 1
int
��1 4

idProducto
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
�� 
Json
�� 
(
�� 
operacionLogica
�� +
.
��+ ,$
ObtenerStockDeProducto
��, B
(
��B C

idProducto
��C M
,
��M N
ProfileData
��O Z
(
��Z [
)
��[ \
.
��\ ]7
(IdCentroAtencionQueTieneElStockIntegrada��] �
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
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
��' (2
$ConstruirDetalleMovimientoMercaderia
��) M
(
��M N2
$RegistroMovimientoDeAlmacenViewModel
��N r 
ingresoMercaderia��s �
)��� �
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
detallesConstruidos
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
�� 
item
�� 
in
��  
ingresoMercaderia
��! 2
.
��2 3
Detalles
��3 ;
)
��; <
{
�� 
detallesConstruidos
�� #
.
��# $
Add
��$ '
(
��' (
new
��( +!
Detalle_transaccion
��, ?
(
��? @
item
��@ D
.
��D E!
IngresoSalidaActual
��E X
,
��X Y
item
��Z ^
.
��^ _

IdProducto
��_ i
,
��i j
$str
��k m
,
��m n
$num
��o p
,
��p q
$num
��r s
,
��s t
null
��u y
,
��y z
$num
��{ |
,
��| }
null��~ �
,��� �
null��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
item��� �
.��� �
Lote��� �
,��� �
null��� �
,��� �
null��� �
)��� �
)��� �
;��� �
}
�� 
return
�� !
detallesConstruidos
�� &
;
��& '
}
�� 	
[
�� 	
AllowAnonymous
��	 
]
�� 
public
�� 
async
�� 
Task
�� 
	PrintFile
�� #
(
��# $
long
��$ (
idMovimiento
��) 5
)
��5 6
{
�� 	!
MovimientoDeAlmacen
�� !
movimientoDeAlmacen
��  3
=
��4 5
operacionLogica
��6 E
.
��E F+
ObtenerMovimientoDeMercaderia
��F c
(
��c d
idMovimiento
��d p
)
��p q
;
��q r
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� "
(
��" #
)
��# $
;
��$ %
var
�� 
proveedores
�� 
=
��  
actorNegocioLogica
�� 0
.
��0 1(
ObtenerProveedoresVigentes
��1 K
(
��K L
)
��L M
;
��M N
var
�� #
modalidadesDeTraslado
�� %
=
��& '
await
��( -

��. ;
.
��; <-
ObtenerModalidadesTrasladoAsync
��< [
(
��[ \
)
��\ ]
;
��] ^
var
�� 
motivosDeTraslado
�� !
=
��" #
await
��$ )

��* 7
.
��7 8)
ObtenerMotivosTrasladoAsync
��8 S
(
��S T
)
��T U
;
��U V
string
�� 
	QrContent
�� 
=
�� *
facturacionElectronicaLogica
�� ;
.
��; <
	ObtenerQR
��< E
(
��E F!
movimientoDeAlmacen
��F Y
,
��Y Z
sede
��[ _
)
��_ `
;
��` a
byte
�� 
[
�� 
]
�� 
QrBytes
�� 
=
�� 
(
�� !
movimientoDeAlmacen
�� 1
.
��1 2
IdTipoComprobante
��2 C
==
��D F
MaestroSettings
��G V
.
��V W
Default
��W ^
.
��^ _A
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��_ �
)��� �
?��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �#
movimientoDeAlmacen��� �
.��� �
Informacion��� �
)��� �
:��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
	QrContent��� �
)��� �
;��� �
var
�� 
PDFventa
�� 
=
�� +
ObtenerPdfMovimientoDeAlmacen
�� 8
(
��8 9!
movimientoDeAlmacen
��9 L
,
��L M
sede
��N R
,
��R S
QrBytes
��T [
,
��[ \
(
��] ^
FormatoImpresion
��^ n
)
��n o
VentasSettings
��o }
.
��} ~
Default��~ �
.��� �*
formatoImpresionPorDefecto��� �
,��� �
proveedores��� �
,��� �%
modalidadesDeTraslado��� �
,��� �!
motivosDeTraslado��� �
)��� �
;��� �
PrintFilePDF
�� 
file
�� 
=
�� 
null
��  $
;
��$ %
file
�� 
=
�� 
new
�� 
PrintFilePDF
�� #
(
��# $
PDFventa
��$ ,
.
��, -
Save
��- 1
(
��1 2
)
��2 3
,
��3 4
idMovimiento
��5 A
+
��B C
$str
��D J
)
��J K
;
��K L
file
�� 
.
�� 

�� 
=
��  

��! .
.
��. /
None
��/ 3
;
��3 4
ClientPrintJob
�� 
cpj
�� 
=
��  
new
��! $
ClientPrintJob
��% 3
(
��3 4
)
��4 5
;
��5 6
cpj
�� 
.
�� 
	PrintFile
�� 
=
�� 
file
��  
;
��  !
cpj
�� 
.
�� 
	PrintFile
�� 
.
�� 
Copies
��  
=
��! " 
AplicacionSettings
��# 5
.
��5 6
Default
��6 =
.
��= >3
%NumeroCopiasAImprimirComprobanteVenta
��> c
;
��c d
cpj
�� 
.
�� 

�� 
=
�� 
new
��  #
DefaultPrinter
��$ 2
(
��2 3
)
��3 4
;
��4 5
System
�� 
.
�� 
Web
�� 
.
�� 
HttpContext
�� "
.
��" #
Current
��# *
.
��* +
Response
��+ 3
.
��3 4
ContentType
��4 ?
=
��@ A
$str
��B \
;
��\ ]
System
�� 
.
�� 
Web
�� 
.
�� 
HttpContext
�� "
.
��" #
Current
��# *
.
��* +
Response
��+ 3
.
��3 4
BinaryWrite
��4 ?
(
��? @
cpj
��@ C
.
��C D

GetContent
��D N
(
��N O
)
��O P
)
��P Q
;
��Q R
System
�� 
.
�� 
Web
�� 
.
�� 
HttpContext
�� "
.
��" #
Current
��# *
.
��* +
Response
��+ 3
.
��3 4
End
��4 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
PdfDocument
�� &
ObtenerPdfOrdenDeAlmacen
�� 3
(
��3 4(
OrdenDeMovimientoDeAlmacen
��4 N
ordenDeAlmacen
��O ]
,
��] ^7
(EstablecimientoComercialExtendidoConLogo��_ �
sede��� �
,��� �
byte��� �
[��� �
]��� �
qrBytes��� �
,��� � 
FormatoImpresion��� �
formato��� �
)��� �
{
�� 	
string
�� 
result
�� 
=
�� #
CoreHtmlStringBuilder
�� 1
.
��1 2
ObtenerHtmlString
��2 C
(
��C D
ordenDeAlmacen
��D R
,
��R S
formato
��T [
,
��[ \
qrBytes
��] d
,
��d e
sede
��f j
,
��j k
this
��l p
)
��p q
;
��q r
var
�� 
Renderer
�� 
=
�� 
new
�� 
	SelectPdf
�� (
.
��( )
	HtmlToPdf
��) 2
(
��2 3
)
��3 4
;
��4 5
if
�� 
(
�� 
formato
�� 
==
�� 
FormatoImpresion
�� +
.
��+ ,
_80mm
��, 1
)
��1 2
{
�� 
Renderer
�� 
.
�� 
Options
��  
.
��  !
AutoFitWidth
��! -
=
��. /"
HtmlToPdfPageFitMode
��0 D
.
��D E
NoAdjustment
��E Q
;
��Q R
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0"
HtmlToPdfPageFitMode
��1 E
.
��E F
NoAdjustment
��F R
;
��R S
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageWidth
��! -
=
��. /
$num
��0 4
;
��4 5
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0
$num
��1 2
;
��2 3
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageFixedSize
��! 1
=
��2 3
false
��4 9
;
��9 :
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageSize
��! ,
=
��- .
PdfPageSize
��/ :
.
��: ;
Custom
��; A
;
��A B
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageCustomSize
��! 2
=
��3 4
new
��5 8
SizeF
��9 >
(
��> ?
$num
��? B
,
��B C
$num
��D G
)
��G H
;
��H I
}
�� 
else
�� 
if
�� 
(
�� 
formato
�� 
==
�� 
FormatoImpresion
��  0
.
��0 1
A4
��1 3
)
��3 4
{
�� 
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageSize
��! ,
=
��- .
PdfPageSize
��/ :
.
��: ;
A4
��; =
;
��= >
}
�� 
else
�� 
if
�� 
(
�� 
formato
�� 
==
�� 
FormatoImpresion
��  0
.
��0 1
_56mm
��1 6
)
��6 7
{
�� 
Renderer
�� 
.
�� 
Options
��  
.
��  !
AutoFitWidth
��! -
=
��. /"
HtmlToPdfPageFitMode
��0 D
.
��D E
NoAdjustment
��E Q
;
��Q R
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0"
HtmlToPdfPageFitMode
��1 E
.
��E F
AutoFit
��F M
;
��M N
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageWidth
��! -
=
��. /
$num
��0 3
;
��3 4
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0
$num
��1 2
;
��2 3
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageFixedSize
��! 1
=
��2 3
false
��4 9
;
��9 :
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageSize
��! ,
=
��- .
PdfPageSize
��/ :
.
��: ;
Custom
��; A
;
��A B
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageCustomSize
��! 2
=
��3 4
new
��5 8
SizeF
��9 >
(
��> ?
$num
��? B
,
��B C
$num
��D G
)
��G H
;
��H I
}
�� 
Renderer
�� 
.
�� 
Options
�� 
.
�� 
MarginBottom
�� )
=
��* +
$num
��, -
;
��- .
Renderer
�� 
.
�� 
Options
�� 
.
�� 
	MarginTop
�� &
=
��' (
$num
��) *
;
��* +
Renderer
�� 
.
�� 
Options
�� 
.
�� 

MarginLeft
�� '
=
��( )
$num
��* +
;
��+ ,
Renderer
�� 
.
�� 
Options
�� 
.
�� 
MarginRight
�� (
=
��) *
$num
��+ ,
;
��, -
Renderer
�� 
.
�� 
Options
�� 
.
�� 

�� *
=
��+ ,
false
��- 2
;
��2 3
Renderer
�� 
.
�� 
Options
�� 
.
�� $
JpegCompressionEnabled
�� 3
=
��4 5
false
��6 ;
;
��; <
return
�� 
Renderer
�� 
.
�� 
ConvertHtmlString
�� -
(
��- .
result
��. 4
)
��4 5
;
��5 6
}
�� 	
public
�� 

JsonResult
�� 5
'EnviarCorreoElectronicoDeOrdenDeAlmacen
�� A
(
��A B
long
��B F
idOrden
��G N
,
��N O
int
��P S
formato
��T [
,
��[ \
List
��] a
<
��a b
string
��b h
>
��h i!
correosElectronicos
��j }
)
��} ~
{
�� 	
try
�� 
{
�� 
OrdenDeMovimientoDeAlmacen
�� *
ordenDeAlmacen
��+ 9
=
��: ;
operacionLogica
��< K
.
��K L/
!ObtenerOrdenDeMovimientoDeAlmacen
��L m
(
��m n
idOrden
��n u
)
��u v
;
��v w
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
PdfDocument
�� 

�� )
=
��* +&
ObtenerPdfOrdenDeAlmacen
��, D
(
��D E
ordenDeAlmacen
��E S
,
��S T
sede
��U Y
,
��Y Z
null
��[ _
,
��_ `
(
��a b
FormatoImpresion
��b r
)
��r s
formato
��s z
)
��z {
;
��{ |
string
�� 
asunto
�� 
=
�� 
operacionLogica
��  /
.
��/ 0.
 ObtenerAsuntoDeCorreoElectronico
��0 P
(
��P Q
sede
��Q U
,
��U V
ordenDeAlmacen
��W e
)
��e f
;
��f g
string
�� 
cuerpo
�� 
=
�� 
operacionLogica
��  /
.
��/ 0.
 ObtenerCuerpoDeCorreoElectronico
��0 P
(
��P Q
sede
��Q U
,
��U V
ordenDeAlmacen
��W e
)
��e f
;
��f g
OperationResult
�� 
result
��  &
=
��' (
mailer
��) /
.
��/ 0
Send
��0 4
(
��4 5
asunto
��5 ;
,
��; <
cuerpo
��= C
,
��C D!
correosElectronicos
��E X
,
��X Y 
AplicacionSettings
��Z l
.
��l m
Default
��m t
.
��t u

,��� �
new��� �
List��� �
<��� �

Attachment��� �
>��� �
(��� �
)��� �
{��� �
new��� �

Attachment��� �
(��� �
new��� �
MemoryStream��� �
(��� �

.��� �
Save��� �
(��� �
)��� �
)��� �
,��� �
ordenDeAlmacen��� �
.��� �
Comprobante��� �
(��� �
)��� �
.��� �

+��� �
$str��� �
+��� �
ordenDeAlmacen��� �
.��� �
Comprobante��� �
(��� �
)��� �
.��� �#
NumeroDeComprobante��� �
+��� �
$str��� �
+��� �
ordenDeAlmacen��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
,��� �
$str��� �
)��� �
}��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
data
��6 :
=
��; <
result
��= C
.
��C D
information
��D O
,
��O P 
result_description
��Q c
=
��d e
result
��f l
.
��l m
title
��m r
}
��s t
)
��t u
;
��u v
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 
ActionResult
�� %
DescargarOrdenDeAlmacen
�� 3
(
��3 4
long
��4 8
idOrden
��9 @
,
��@ A
int
��B E
formato
��F M
)
��M N
{
�� 	
try
�� 
{
�� 
OrdenDeMovimientoDeAlmacen
�� *
ordenDeAlmacen
��+ 9
=
��: ;
operacionLogica
��< K
.
��K L/
!ObtenerOrdenDeMovimientoDeAlmacen
��L m
(
��m n
idOrden
��n u
)
��u v
;
��v w
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
PdfDocument
�� 

�� )
=
��* +&
ObtenerPdfOrdenDeAlmacen
��, D
(
��D E
ordenDeAlmacen
��E S
,
��S T
sede
��U Y
,
��Y Z
null
��[ _
,
��_ `
(
��a b
FormatoImpresion
��b r
)
��r s
formato
��s z
)
��z {
;
��{ |
byte
�� 
[
�� 
]
�� 
	fileBytes
��  
=
��! "

��# 0
.
��0 1
Save
��1 5
(
��5 6
)
��6 7
;
��7 8
string
�� 
fileName
�� 
=
��  !
ordenDeAlmacen
��" 0
.
��0 1
Comprobante
��1 <
(
��< =
)
��= >
.
��> ?

��? L
+
��M N
$str
��O T
+
��U V
ordenDeAlmacen
��W e
.
��e f
Comprobante
��f q
(
��q r
)
��r s
.
��s t"
NumeroDeComprobante��t �
+��� �
$str��� �
+��� �
ordenDeAlmacen��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
;��� �
return
�� 
File
�� 
(
�� 
	fileBytes
�� %
,
��% &
$str
��' 8
,
��8 9
fileName
��: B
)
��B C
;
��C D
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str��M �
,��� �
e��� �
)��� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� 6
(ObtenerDetalleDeCompraParaOrdenDeAlmacen
�� B
(
��B C
long
��C G
idOrdenDeCompra
��H W
)
��W X
{
�� 	
try
�� 
{
�� 

�� 

�� +
=
��, -
operacionLogica
��. =
.
��= >"
ObtenerOrdenDeCompra
��> R
(
��R S
idOrdenDeCompra
��S b
)
��b c
;
��c d
List
�� 
<
�� ,
DetalleOrdenDeAlmacenViewModel
�� 3
>
��3 4
detalles
��5 =
=
��> ?,
DetalleOrdenDeAlmacenViewModel
��@ ^
.
��^ _
	Convertir
��_ h
(
��h i

��i v
.
��v w
Detalles
��w 
(�� �
)��� �
,��� �

.��� �'
ObtenerOrdenesDeAlmacen��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
detalles
�� $
)
��$ %
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� #
GuardarOrdenDeAlmacen
�� /
(
��/ 0-
RegistroOrdenDeAlmacenViewModel
��0 O
ordenDeAlmacen
��P ^
)
��^ _
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
��& '
List
�� 
<
��  
DetalleDeOperacion
�� '
>
��' (
detalles
��) 1
=
��2 3,
ConstruirDetalleOrdenDeAlmacen
��4 R
(
��R S
ordenDeAlmacen
��S a
)
��a b
;
��b c
result
�� 
=
�� 
operacionLogica
�� (
.
��( )#
GuardarOrdenDeAlmacen
��) >
(
��> ?
ordenDeAlmacen
��? M
.
��M N 
IdOrdenDeOperacion
��N `
,
��` a
ProfileData
��b m
(
��m n
)
��n o
.
��o p
Empleado
��p x
.
��x y
Id
��y {
,
��{ |
ProfileData��} �
(��� �
)��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �
ordenDeAlmacen��� �
.��� �!
TipoDeComprobante��� �
.��� �!
SerieSeleccionada��� �
,��� �
ordenDeAlmacen��� �
.��� � 
CentroDeAtencion��� �
.��� �
Id��� �
,��� �
ordenDeAlmacen��� �
.��� �

,��� �
ordenDeAlmacen��� �
.��� �
Observacion��� �
,��� �
detalles��� �
,��� �
ordenDeAlmacen��� �
.��� �!
EsGeneracionTotal��� �
)��� �
;��� �
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 :
)
��: ;
;
��; <
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
result
��6 <
.
��< =
code_result
��= H
,
��H I
result
��J P
.
��P Q
data
��Q U
,
��U V 
result_description
��W i
=
��j k
result
��l r
.
��r s
title
��s x
}
��y z
,
��z {
HttpStatusCode��| �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M |
,
��| }
e
��~ 
)�� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
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
DetalleDeOperacion
�� &
>
��& ',
ConstruirDetalleOrdenDeAlmacen
��( F
(
��F G-
RegistroOrdenDeAlmacenViewModel
��G f
ordenDeAlmacen
��g u
)
��u v
{
�� 	
List
�� 
<
��  
DetalleDeOperacion
�� #
>
��# $!
detallesConstruidos
��% 8
=
��9 :
new
��; >
List
��? C
<
��C D 
DetalleDeOperacion
��D V
>
��V W
(
��W X
)
��X Y
;
��Y Z
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
ordenDeAlmacen
��! /
.
��/ 0
Detalles
��0 8
)
��8 9
{
�� 
detallesConstruidos
�� #
.
��# $
Add
��$ '
(
��' (
new
��( + 
DetalleDeOperacion
��, >
(
��> ?
item
��? C
.
��C D

IdProducto
��D N
,
��N O
item
��P T
.
��T U!
IngresoSalidaActual
��U h
,
��h i
$num
��j k
,
��k l
$num
��m n
,
��n o
$num
��p q
,
��q r
$num
��s t
,
��t u
$num
��v w
,
��w x
null
��y }
,
��} ~
null�� �
,��� �
null��� �
,��� �
null��� �
,��� �
item��� �
.��� �
EsBien��� �
,��� �
null��� �
,��� �
null��� �
)��� �
)��� �
;��� �
}
�� 
return
�� !
detallesConstruidos
�� &
;
��& '
}
�� 	
public
�� 

JsonResult
�� )
ObtenerMovimientosDeAlmacen
�� 5
(
��5 6
bool
��6 :
	esEntrada
��; D
,
��D E
int
��F I
[
��I J
]
��J K"
idsCentrosDeAtencion
��L `
,
��` a
string
��b h
desde
��i n
,
��n o
string
��p v
hasta
��w |
)
��| }
{
�� 	
try
�� 
{
�� 
DateTime
�� 

fechaDesde
�� #
=
��$ %
DateTimeUtil
��& 2
.
��2 3E
7ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos
��3 j
(
��j k
desde
��k p
)
��p q
;
��q r
DateTime
�� 

fechaHasta
�� #
=
��$ %
DateTimeUtil
��& 2
.
��2 3E
7ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos
��3 j
(
��j k
hasta
��k p
)
��p q
;
��q r
List
�� 
<
�� $
Entrada_Salida_Almacen
�� +
>
��+ ,
	resultado
��- 6
=
��7 8
	esEntrada
��9 B
?
��C D
operacionLogica
��E T
.
��T U&
ObtenerEntradasDeAlmacen
��U m
(
��m n
ProfileData
��n y
(
��y z
)
��z {
.
��{ |
Empleado��| �
.��� �
Id��� �
,��� �$
idsCentrosDeAtencion��� �
.��� �
ToList��� �
(��� �
)��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
:��� �
operacionLogica��� �
.��� �'
ObtenerSalidasDeAlmacen��� �
(��� �
ProfileData��� �
(��� �
)��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �$
idsCentrosDeAtencion��� �
.��� �
ToList��� �
(��� �
)��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	resultado
�� %
)
��% &
;
��& '
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %(
ObtenerMovimientoDeAlmacen
��& @
(
��@ A
long
��A E
idMovimiento
��F R
,
��R S
int
��T W
formato
��X _
)
��_ `
{
�� 	
try
�� 
{
�� 
MovimientoDeAlmacen
�� #!
movimientoDeAlmacen
��$ 7
=
��8 9
operacionLogica
��: I
.
��I J+
ObtenerMovimientoDeMercaderia
��J g
(
��g h
idMovimiento
��h t
)
��t u
;
��u v*
MovimientoDeAlmacenViewModel
�� ,
	respuesta
��- 6
=
��7 8
new
��9 <*
MovimientoDeAlmacenViewModel
��= Y
(
��Y Z
)
��Z [
;
��[ \
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
string
�� "
htmlStringMovimiento
�� +
=
��, -
$str
��. 0
;
��0 1
string
�� 
htmlStringOrden
�� &
=
��' (
$str
��) +
;
��+ ,
bool
�� #
comprobanteMovimiento
�� *
=
��+ ,
false
��- 2
;
��2 3
if
�� 
(
�� !
movimientoDeAlmacen
�� '
.
��' (
IdTipoComprobante
��( 9
==
��: <
MaestroSettings
��= L
.
��L M
Default
��M T
.
��T U<
-IdDetalleMaestroComprobanteNotaAlmacenInterna��U �
||��� �#
movimientoDeAlmacen��� �
.��� �!
IdTipoComprobante��� �
==��� �
MaestroSettings��� �
.��� �
Default��� �
.��� �B
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��� �
)��� �
{
�� 
var
�� 
proveedores
�� #
=
��$ % 
actorNegocioLogica
��& 8
.
��8 9(
ObtenerProveedoresVigentes
��9 S
(
��S T
)
��T U
;
��U V
var
�� #
modalidadesDeTraslado
�� -
=
��. /
await
��0 5

��6 C
.
��C D-
ObtenerModalidadesTrasladoAsync
��D c
(
��c d
)
��d e
;
��e f
var
�� 
motivosDeTraslado
�� )
=
��* +
await
��, 1

��2 ?
.
��? @)
ObtenerMotivosTrasladoAsync
��@ [
(
��[ \
)
��\ ]
;
��] ^
var
�� 
	QrContent
�� !
=
��" #*
facturacionElectronicaLogica
��$ @
.
��@ A
	ObtenerQR
��A J
(
��J K!
movimientoDeAlmacen
��K ^
,
��^ _
sede
��` d
)
��d e
;
��e f
var
�� 
QrBytes
�� 
=
��  !
(
��" #!
movimientoDeAlmacen
��# 6
.
��6 7
IdTipoComprobante
��7 H
==
��I K
MaestroSettings
��L [
.
��[ \
Default
��\ c
.
��c dA
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��d �
)��� �
?��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �#
movimientoDeAlmacen��� �
.��� �
Informacion��� �
)��� �
:��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
	QrContent��� �
)��� �
;��� �#
comprobanteMovimiento
�� )
=
��* +
true
��, 0
;
��0 1"
htmlStringMovimiento
�� (
=
��) *#
CoreHtmlStringBuilder
��+ @
.
��@ A
ObtenerHtmlString
��A R
(
��R S!
movimientoDeAlmacen
��S f
,
��f g
(
��h i
FormatoImpresion
��i y
)
��y z
formato��z �
,��� �
QrBytes��� �
,��� �
sede��� �
,��� �
proveedores��� �
,��� �%
modalidadesDeTraslado��� �
,��� �!
motivosDeTraslado��� �
,��� �
this��� �
)��� �
;��� �
}
�� 
else
�� 
{
�� #
comprobanteMovimiento
�� )
=
��* +
false
��, 1
;
��1 2"
htmlStringMovimiento
�� (
=
��) *#
CoreHtmlStringBuilder
��+ @
.
��@ A
ObtenerHtmlString
��A R
(
��R S!
movimientoDeAlmacen
��S f
,
��f g
(
��h i
FormatoImpresion
��i y
)
��y z
formato��z �
,��� �
null��� �
,��� �
sede��� �
,��� �
null��� �
,��� �
null��� �
,��� �
null��� �
,��� �
this��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 
Diccionario
�� 
.
��  <
.TiposDeTransaccionOrdenesDeOperacionesDeVentas
��  N
.
��N O
Contains
��O W
(
��W X!
movimientoDeAlmacen
��X k
.
��k l
OrdenDeOperacion
��l |
(
��| }
)
��} ~
.
��~ 
TipoTransaccion�� �
(��� �
)��� �
.��� �
Id��� �
)��� �
)��� �
{
�� 
OrdenDeVenta
��  
ordenDeVenta
��! -
=
��. /
operacionLogica
��0 ?
.
��? @!
ObtenerOrdenDeVenta
��@ S
(
��S T!
movimientoDeAlmacen
��T g
.
��g h#
IdOperacionReferencia
��h }
(
��} ~
)
��~ 
)�� �
;��� �
var
�� 
QrContentOrden
�� &
=
��' (*
facturacionElectronicaLogica
��) E
.
��E F
	ObtenerQR
��F O
(
��O P!
movimientoDeAlmacen
��P c
,
��c d
sede
��e i
)
��i j
;
��j k
var
�� 
QrBytesOrden
�� $
=
��% &
barCodeUtil
��' 2
.
��2 3
ObtenerCodigoQR
��3 B
(
��B C
QrContentOrden
��C Q
)
��Q R
;
��R S
htmlStringOrden
�� #
=
��$ %#
CoreHtmlStringBuilder
��& ;
.
��; <
ObtenerHtmlString
��< M
(
��M N
ordenDeVenta
��N Z
,
��Z [
(
��\ ]
FormatoImpresion
��] m
)
��m n
formato
��n u
,
��u v
QrBytesOrden��w �
,��� �
sede��� �
,��� �
this��� �
,��� �

)��� �
;��� �
}
�� 
else
�� 
if
�� 
(
�� 
Diccionario
�� $
.
��$ %=
/TiposDeTransaccionOrdenesDeOperacionesDeCompras
��% T
.
��T U
Contains
��U ]
(
��] ^!
movimientoDeAlmacen
��^ q
.
��q r
OrdenDeOperacion��r �
(��� �
)��� �
.��� �
TipoTransaccion��� �
(��� �
)��� �
.��� �
Id��� �
)��� �
)��� �
{
�� 

�� !

��" /
=
��0 1
operacionLogica
��2 A
.
��A B"
ObtenerOrdenDeCompra
��B V
(
��V W!
movimientoDeAlmacen
��W j
.
��j k$
IdOperacionReferencia��k �
(��� �
)��� �
)��� �
;��� �
htmlStringOrden
�� #
=
��$ %#
CoreHtmlStringBuilder
��& ;
.
��; <
ObtenerHtmlString
��< M
(
��M N

��N [
,
��[ \
(
��] ^
FormatoImpresion
��^ n
)
��n o
formato
��o v
,
��v w
null
��x |
,
��| }
sede��~ �
,��� �
this��� �
,��� �

)��� �
;��� �
}
�� 
if
�� 
(
�� 
htmlStringOrden
�� #
==
��$ &
$str
��' )
)
��) *
htmlStringOrden
�� #
=
��$ %"
htmlStringMovimiento
��& :
;
��: ;
if
�� 
(
�� 
!
�� #
comprobanteMovimiento
�� *
)
��* +"
htmlStringMovimiento
�� (
=
��) *
htmlStringOrden
��+ :
;
��: ;
	respuesta
�� 
=
�� 
new
�� *
MovimientoDeAlmacenViewModel
��  <
(
��< =!
movimientoDeAlmacen
��= P
.
��P Q
Id
��Q S
,
��S T"
htmlStringMovimiento
��U i
,
��i j
htmlStringOrden
��k z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
PdfDocument
�� +
ObtenerPdfMovimientoDeAlmacen
�� 8
(
��8 9!
MovimientoDeAlmacen
��9 L!
movimientoDeAlmacen
��M `
,
��` a7
(EstablecimientoComercialExtendidoConLogo��b �
sede��� �
,��� �
byte��� �
[��� �
]��� �
qrBytes��� �
,��� � 
FormatoImpresion��� �
formato��� �
,��� �
List��� �
<��� �
	Proveedor��� �
>��� �
proveedores��� �
,��� �
List��� �
<��� �
Detalle_maestro��� �
>��� �%
modalidadesDeTraslado��� �
,��� �
List��� �
<��� �
Detalle_maestro��� �
>��� �!
motivosDeTraslado��� �
)��� �
{
�� 	
string
�� 
result
�� 
=
�� #
CoreHtmlStringBuilder
�� 1
.
��1 2
ObtenerHtmlString
��2 C
(
��C D!
movimientoDeAlmacen
��D W
,
��W X
formato
��Y `
,
��` a
qrBytes
��b i
,
��i j
sede
��k o
,
��o p
proveedores
��q |
,
��| }$
modalidadesDeTraslado��~ �
,��� �!
motivosDeTraslado��� �
,��� �
this��� �
)��� �
;��� �
var
�� 
Renderer
�� 
=
�� 
new
�� 
	SelectPdf
�� (
.
��( )
	HtmlToPdf
��) 2
(
��2 3
)
��3 4
;
��4 5
if
�� 
(
�� 
formato
�� 
==
�� 
FormatoImpresion
�� +
.
��+ ,
_80mm
��, 1
)
��1 2
{
�� 
Renderer
�� 
.
�� 
Options
��  
.
��  !
AutoFitWidth
��! -
=
��. /"
HtmlToPdfPageFitMode
��0 D
.
��D E
NoAdjustment
��E Q
;
��Q R
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0"
HtmlToPdfPageFitMode
��1 E
.
��E F
NoAdjustment
��F R
;
��R S
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageWidth
��! -
=
��. /
$num
��0 4
;
��4 5
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0
$num
��1 2
;
��2 3
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageFixedSize
��! 1
=
��2 3
false
��4 9
;
��9 :
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageSize
��! ,
=
��- .
PdfPageSize
��/ :
.
��: ;
Custom
��; A
;
��A B
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageCustomSize
��! 2
=
��3 4
new
��5 8
SizeF
��9 >
(
��> ?
$num
��? B
,
��B C
$num
��D G
)
��G H
;
��H I
}
�� 
else
�� 
if
�� 
(
�� 
formato
�� 
==
�� 
FormatoImpresion
��  0
.
��0 1
A4
��1 3
)
��3 4
{
�� 
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageSize
��! ,
=
��- .
PdfPageSize
��/ :
.
��: ;
A4
��; =
;
��= >
}
�� 
else
�� 
if
�� 
(
�� 
formato
�� 
==
�� 
FormatoImpresion
��  0
.
��0 1
_56mm
��1 6
)
��6 7
{
�� 
Renderer
�� 
.
�� 
Options
��  
.
��  !
AutoFitWidth
��! -
=
��. /"
HtmlToPdfPageFitMode
��0 D
.
��D E
NoAdjustment
��E Q
;
��Q R
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0"
HtmlToPdfPageFitMode
��1 E
.
��E F
AutoFit
��F M
;
��M N
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageWidth
��! -
=
��. /
$num
��0 3
;
��3 4
Renderer
�� 
.
�� 
Options
��  
.
��  !

��! .
=
��/ 0
$num
��1 2
;
��2 3
Renderer
�� 
.
�� 
Options
��  
.
��  !
WebPageFixedSize
��! 1
=
��2 3
false
��4 9
;
��9 :
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageSize
��! ,
=
��- .
PdfPageSize
��/ :
.
��: ;
Custom
��; A
;
��A B
Renderer
�� 
.
�� 
Options
��  
.
��  !
PdfPageCustomSize
��! 2
=
��3 4
new
��5 8
SizeF
��9 >
(
��> ?
$num
��? B
,
��B C
$num
��D G
)
��G H
;
��H I
}
�� 
Renderer
�� 
.
�� 
Options
�� 
.
�� 
MarginBottom
�� )
=
��* +
$num
��, -
;
��- .
Renderer
�� 
.
�� 
Options
�� 
.
�� 
	MarginTop
�� &
=
��' (
$num
��) *
;
��* +
Renderer
�� 
.
�� 
Options
�� 
.
�� 

MarginLeft
�� '
=
��( )
$num
��* +
;
��+ ,
Renderer
�� 
.
�� 
Options
�� 
.
�� 
MarginRight
�� (
=
��) *
$num
��+ ,
;
��, -
Renderer
�� 
.
�� 
Options
�� 
.
�� 

�� *
=
��+ ,
false
��- 2
;
��2 3
Renderer
�� 
.
�� 
Options
�� 
.
�� $
JpegCompressionEnabled
�� 3
=
��4 5
false
��6 ;
;
��; <
return
�� 
Renderer
�� 
.
�� 
ConvertHtmlString
�� -
(
��- .
result
��. 4
)
��4 5
;
��5 6
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %:
,EnviarCorreoElectronicoDeMovimientoDeAlmacen
��& R
(
��R S
long
��S W
idMovimiento
��X d
,
��d e
int
��f i
formato
��j q
,
��q r
List
��s w
<
��w x
string
��x ~
>
��~ #
correosElectronicos��� �
)��� �
{
�� 	
try
�� 
{
�� 
MovimientoDeAlmacen
�� #!
movimientoDeAlmacen
��$ 7
=
��8 9
operacionLogica
��: I
.
��I J+
ObtenerMovimientoDeMercaderia
��J g
(
��g h
idMovimiento
��h t
)
��t u
;
��u v
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� #
modalidadesDeTraslado
�� )
=
��* +
await
��, 1

��2 ?
.
��? @-
ObtenerModalidadesTrasladoAsync
��@ _
(
��_ `
)
��` a
;
��a b
var
�� 
motivosDeTraslado
�� %
=
��& '
await
��( -

��. ;
.
��; <)
ObtenerMotivosTrasladoAsync
��< W
(
��W X
)
��X Y
;
��Y Z
string
�� 
	QrContent
��  
=
��! "*
facturacionElectronicaLogica
��# ?
.
��? @
	ObtenerQR
��@ I
(
��I J!
movimientoDeAlmacen
��J ]
,
��] ^
sede
��_ c
)
��c d
;
��d e
byte
�� 
[
�� 
]
�� 
QrBytes
�� 
=
��  
(
��! "!
movimientoDeAlmacen
��" 5
.
��5 6
IdTipoComprobante
��6 G
==
��H J
MaestroSettings
��K Z
.
��Z [
Default
��[ b
.
��b cA
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��c �
)��� �
?��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �#
movimientoDeAlmacen��� �
.��� �
Informacion��� �
)��� �
:��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
	QrContent��� �
)��� �
;��� �
PdfDocument
�� 

�� )
=
��* ++
ObtenerPdfMovimientoDeAlmacen
��, I
(
��I J!
movimientoDeAlmacen
��J ]
,
��] ^
sede
��_ c
,
��c d
QrBytes
��e l
,
��l m
(
��n o
FormatoImpresion
��o 
)�� �
formato��� �
,��� �
proveedores��� �
,��� �%
modalidadesDeTraslado��� �
,��� �!
motivosDeTraslado��� �
)��� �
;��� �
string
�� 
asunto
�� 
=
�� 
operacionLogica
��  /
.
��/ 0.
 ObtenerAsuntoDeCorreoElectronico
��0 P
(
��P Q
sede
��Q U
,
��U V!
movimientoDeAlmacen
��W j
)
��j k
;
��k l
string
�� 
cuerpo
�� 
=
�� 
operacionLogica
��  /
.
��/ 0.
 ObtenerCuerpoDeCorreoElectronico
��0 P
(
��P Q
sede
��Q U
,
��U V!
movimientoDeAlmacen
��W j
)
��j k
;
��k l
OperationResult
�� 
result
��  &
=
��' (
mailer
��) /
.
��/ 0
Send
��0 4
(
��4 5
asunto
��5 ;
,
��; <
cuerpo
��= C
,
��C D!
correosElectronicos
��E X
,
��X Y 
AplicacionSettings
��Z l
.
��l m
Default
��m t
.
��t u

,��� �
new��� �
List��� �
<��� �

Attachment��� �
>��� �
(��� �
)��� �
{��� �
new��� �

Attachment��� �
(��� �
new��� �
MemoryStream��� �
(��� �

.��� �
Save��� �
(��� �
)��� �
)��� �
,��� �#
movimientoDeAlmacen��� �
.��� �
Comprobante��� �
(��� �
)��� �
.��� �

+��� �
$str��� �
+��� �#
movimientoDeAlmacen��� �
.��� �
Comprobante��� �
(��� �
)��� �
.��� �#
NumeroDeComprobante��� �
+��� �
$str��� �
+��� �#
movimientoDeAlmacen��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
,��� �
$str��� �
)��� �
}��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
data
��6 :
=
��; <
result
��= C
.
��C D
information
��D O
,
��O P 
result_description
��Q c
=
��d e
result
��f l
.
��l m
title
��m r
}
��s t
)
��t u
;
��u v
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '*
DescargarMovimientoDeAlmacen
��( D
(
��D E
long
��E I
idMovimiento
��J V
,
��V W
int
��X [
formato
��\ c
)
��c d
{
�� 	
try
�� 
{
�� 
PdfDocument
�� 

�� )
;
��) *
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )!
MovimientoDeAlmacen
�� #!
movimientoDeAlmacen
��$ 7
=
��8 9
operacionLogica
��: I
.
��I J+
ObtenerMovimientoDeMercaderia
��J g
(
��g h
idMovimiento
��h t
)
��t u
;
��u v
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� #
modalidadesDeTraslado
�� )
=
��* +
await
��, 1

��2 ?
.
��? @-
ObtenerModalidadesTrasladoAsync
��@ _
(
��_ `
)
��` a
;
��a b
var
�� 
motivosDeTraslado
�� %
=
��& '
await
��( -

��. ;
.
��; <)
ObtenerMotivosTrasladoAsync
��< W
(
��W X
)
��X Y
;
��Y Z
string
�� 
	QrContent
��  
=
��! "*
facturacionElectronicaLogica
��# ?
.
��? @
	ObtenerQR
��@ I
(
��I J!
movimientoDeAlmacen
��J ]
,
��] ^
sede
��_ c
)
��c d
;
��d e
byte
�� 
[
�� 
]
�� 
QrBytes
�� 
=
��  
(
��! "!
movimientoDeAlmacen
��" 5
.
��5 6
IdTipoComprobante
��6 G
==
��H J
MaestroSettings
��K Z
.
��Z [
Default
��[ b
.
��b cA
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��c �
)��� �
?��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �#
movimientoDeAlmacen��� �
.��� �
Informacion��� �
)��� �
:��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
	QrContent��� �
)��� �
;��� �

�� 
=
�� +
ObtenerPdfMovimientoDeAlmacen
��  =
(
��= >!
movimientoDeAlmacen
��> Q
,
��Q R
sede
��S W
,
��W X
QrBytes
��Y `
,
��` a
(
��b c
FormatoImpresion
��c s
)
��s t
formato
��t {
,
��{ |
proveedores��} �
,��� �%
modalidadesDeTraslado��� �
,��� �!
motivosDeTraslado��� �
)��� �
;��� �
byte
�� 
[
�� 
]
�� 
	fileBytes
��  
=
��! "

��# 0
.
��0 1
Save
��1 5
(
��5 6
)
��6 7
;
��7 8
string
�� 
fileName
�� 
=
��  !!
movimientoDeAlmacen
��" 5
.
��5 6
Comprobante
��6 A
(
��A B
)
��B C
.
��C D

��D Q
+
��R S
$str
��T Y
+
��Z [!
movimientoDeAlmacen
��\ o
.
��o p
Comprobante
��p {
(
��{ |
)
��| }
.
��} ~"
NumeroDeComprobante��~ �
+��� �
$str��� �
+��� �#
movimientoDeAlmacen��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
;��� �
return
�� 
File
�� 
(
�� 
	fileBytes
�� %
,
��% &
$str
��' 8
,
��8 9
fileName
��: B
)
��B C
;
��C D
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str��M �
,��� �
e��� �
)��� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� 7
)ObtenerOrdenesMovimientoInternoMercaderia
�� C
(
��C D
bool
��D H
tipoEntradaSalida
��I Z
,
��Z [
string
��\ b
desde
��c h
,
��h i
string
��j p
hasta
��q v
)
��v w
{
�� 	
DateTime
�� 

fechaDesde
�� 
=
��  !
DateTimeUtil
��" .
.
��. /E
7ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos
��/ f
(
��f g
desde
��g l
)
��l m
;
��m n
DateTime
�� 

fechaHasta
�� 
=
��  !
DateTimeUtil
��" .
.
��. /E
7ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos
��/ f
(
��f g
hasta
��g l
)
��l m
;
��m n
try
�� 
{
�� 
List
�� 
<
�� $
OrdenDeTrasladoInterno
�� +
>
��+ ,!
ordenesDeMovimiento
��- @
=
��A B
tipoEntradaSalida
��C T
?
��U V
operacionLogica
��W f
.
��f g5
&ObtenerOrdenesIngresoInternoMercaderia��g �
(��� �
ProfileData��� �
(��� �
)��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �
ProfileData��� �
(��� �
)��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
:��� �
operacionLogica
��X g
.
��g h4
%ObtenerOrdenesSalidaInternoMercaderia��h �
(��� �
ProfileData��� �
(��� �
)��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �
ProfileData��� �
(��� �
)��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �

fechaDesde��� �
,��� �

fechaHasta��� �
)��� �
;��� �
List
�� 
<
�� 9
+BandejaMovimientoInternoMercaderiaViewModel
�� @
>
��@ A
	respuesta
��B K
=
��L M9
+BandejaMovimientoInternoMercaderiaViewModel
��N y
.
��y z
Convert��z �
(��� �#
ordenesDeMovimiento��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� E
7ObtenerMovimientoInternoMercaderiaIncluidoDetallesOrden
�� Q
(
��Q R
long
��R V
idMovimiento
��W c
)
��c d
{
�� 	
try
�� 
{
�� 
TrasladoInterno
�� 
	resultado
��  )
=
��* +
operacionLogica
��, ;
.
��; <
ObtenerMovimiento
��< M
(
��M N
idMovimiento
��N Z
)
��Z [
;
��[ \
return
�� 
Json
�� 
(
�� 
new
�� 4
&MovimientoInternoDeMercaderiaViewModel
��  F
(
��F G
	resultado
��G P
.
��P Q#
OrdenDeDesplazamiento
��Q f
(
��f g
)
��g h
)
��h i
)
��i j
;
��j k
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 0
"GuardarMovimientoInternoMercaderia
�� <
(
��< =:
,RegistroTrasladoInternoDeMercaderiaViewModel
��= i*
movimientoInternoMercaderia��j �
)��� �
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
;
��& '
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
��T U
foreach
�� 
(
�� 
var
�� 
item
�� !
in
��" $)
movimientoInternoMercaderia
��% @
.
��@ A
Detalles
��A I
)
��I J
{
�� 
detalles
�� 
.
�� 
Add
��  
(
��  !
new
��! $!
Detalle_transaccion
��% 8
(
��8 9
item
��9 =
.
��= >
Cantidad
��> F
,
��F G
item
��H L
.
��L M
Producto
��M U
.
��U V
Id
��V X
,
��X Y
item
��Z ^
.
��^ _
Observacion
��_ j
,
��j k
$num
��l m
,
��m n
$num
��o p
,
��p q
$num
��r s
,
��s t
$num
��u v
,
��v w
null
��x |
,
��| }
null��~ �
,��� �
$num��� �
,��� �
$num��� �
,��� �
$num��� �
,��� �
null��� �
,��� �
null��� �
,��� �
null��� �
)��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� )
movimientoInternoMercaderia
�� /
.
��/ 0
TipoDeComprobante
��0 A
.
��A B
Series
��B H
==
��I K
null
��L P
||
��Q S)
movimientoInternoMercaderia
��T o
.
��o p 
TipoDeComprobante��p �
.��� �
Series��� �
.��� �
Count��� �
<��� �
$num��� �
)��� �
{
�� 
throw
�� 
new
�� 
LogicaException
�� -
(
��- .
$str��. �
)��� �
;��� �
}
�� 
result
�� 
=
�� 
operacionLogica
�� (
.
��( );
-ConfirmarMovimientoInternoMercaderiaIntegrado
��) V
(
��V W
ProfileData
��W b
(
��b c
)
��c d
.
��d e
Empleado
��e m
.
��m n
Id
��n p
,
��p q
ProfileData
�� 
(
��  
)
��  !
.
��! ",
IdCentroDeAtencionSeleccionado
��" @
,
��@ A)
movimientoInternoMercaderia
�� /
.
��/ 0
AlmacenDestino
��0 >
.
��> ?
Id
��? A
,
��A B)
movimientoInternoMercaderia
�� /
.
��/ 0 
ResponsableDestino
��0 B
.
��B C
Id
��C E
,
��E F)
movimientoInternoMercaderia
�� /
.
��/ 0
TipoDeComprobante
��0 A
.
��A B
EsPropio
��B J
==
��K M
true
��N R
?
��S T
$num
��U V
:
��W X)
movimientoInternoMercaderia
��Y t
.
��t u 
TipoDeComprobante��u �
.��� �
TipoComprobante��� �
.��� �
Id��� �
,��� �)
movimientoInternoMercaderia
�� /
.
��/ 0
TipoDeComprobante
��0 A
.
��A B
EsPropio
��B J
==
��K M
true
��N R
&&
��S U)
movimientoInternoMercaderia
��V q
.
��q r 
TipoDeComprobante��r �
.��� �!
SerieSeleccionada��� �
==��� �
$num��� �
?��� �+
movimientoInternoMercaderia��� �
.��� �!
TipoDeComprobante��� �
.��� �
Series��� �
.��� �
First��� �
(��� �
)��� �
.��� �
Id��� �
:��� �+
movimientoInternoMercaderia��� �
.��� �!
TipoDeComprobante��� �
.��� �!
SerieSeleccionada��� �
,��� �)
movimientoInternoMercaderia
�� /
.
��/ 0
TipoDeComprobante
��0 A
.
��A B
EsPropio
��B J
,
��J K)
movimientoInternoMercaderia
�� /
.
��/ 0
TipoDeComprobante
��0 A
.
��A B
SerieIngresada
��B P
,
��P Q)
movimientoInternoMercaderia
�� /
.
��/ 0
TipoDeComprobante
��0 A
.
��A B
NumeroIngresado
��B Q
,
��Q R)
movimientoInternoMercaderia
�� /
.
��/ 0
Observacion
��0 ;
,
��; <
detalles
�� 
,
�� 
ProfileData
�� )
(
��) *
)
��* +
)
��+ ,
;
��, -
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
result
��$ *
)
��* +
;
��+ ,
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
result
��6 <
.
��< =
code_result
��= H
,
��H I
data
��J N
=
��O P
result
��Q W
.
��W X
information
��X c
,
��c d 
result_description
��e w
=
��x y
result��z �
.��� �
title��� �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str��M �
,��� �
e��� �
)��� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %:
,ObtenerTiposDeComprobanteMovimientoDeAlmacen
��& R
(
��R S
)
��S T
{
�� 	
try
�� 
{
�� 
var
�� 

resultados
�� 
=
��  
await
��! &
operacionLogica
��' 6
.
��6 72
$ObtenerTiposDeComprobanteParaAlmacen
��7 [
(
��[ \
ProfileData
��\ g
(
��g h
)
��h i
.
��i j
Empleado
��j r
.
��r s
Id
��s u
,
��u v
ProfileData��w �
(��� �
)��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �
List
�� 
<
�� '
SelectorTipoDeComprobante
�� .
>
��. /
comprobantes
��0 <
=
��= >'
SelectorTipoDeComprobante
��? X
.
��X Y
Convert
��Y `
(
��` a

resultados
��a k
,
��k l
ProfileData
��m x
(
��x y
)
��y z
.
��z {-
IdCentroDeAtencionSeleccionado��{ �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
comprobantes
�� (
)
��( )
;
��) *
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
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 

JsonResult
�� 4
&ObtenerTipoDeComprobanteGuiaDeRemision
�� @
(
��@ A
)
��A B
{
�� 	
try
�� 
{
�� 
var
�� 

resultados
�� 
=
��  
operacionLogica
��! 0
.
��0 14
&ObtenerTipoDeComprobanteGuiaDeRemision
��1 W
(
��W X
ProfileData
��X c
(
��c d
)
��d e
.
��e f
Empleado
��f n
.
��n o
Id
��o q
,
��q r
ProfileData
��s ~
(
��~ 
)�� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �
List
�� 
<
�� '
SelectorTipoDeComprobante
�� .
>
��. /
comprobantes
��0 <
=
��= >'
SelectorTipoDeComprobante
��? X
.
��X Y
Convert
��Y `
(
��` a

resultados
��a k
,
��k l
ProfileData
��m x
(
��x y
)
��y z
.
��z {-
IdCentroDeAtencionSeleccionado��{ �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
comprobantes
�� (
)
��( )
;
��) *
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
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 

JsonResult
�� 3
%ObtenerTipoDeComprobanteNotaDeAlmacen
�� ?
(
��? @
)
��@ A
{
�� 	
try
�� 
{
�� 
var
�� 

resultados
�� 
=
��  
operacionLogica
��! 0
.
��0 13
%ObtenerTipoDeComprobanteNotaDeAlmacen
��1 V
(
��V W
ProfileData
��W b
(
��b c
)
��c d
.
��d e
Empleado
��e m
.
��m n
Id
��n p
,
��p q
ProfileData
��r }
(
��} ~
)
��~ 
.�� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �
List
�� 
<
�� '
SelectorTipoDeComprobante
�� .
>
��. /
comprobantes
��0 <
=
��= >'
SelectorTipoDeComprobante
��? X
.
��X Y
Convert
��Y `
(
��` a

resultados
��a k
,
��k l
ProfileData
��m x
(
��x y
)
��y z
.
��z {-
IdCentroDeAtencionSeleccionado��{ �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
comprobantes
�� (
)
��( )
;
��) *
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
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 

JsonResult
�� 5
'ObtenerTiposDeComprobanteOrdenDeAlmacen
�� A
(
��A B
)
��B C
{
�� 	
try
�� 
{
�� 
var
�� 

resultados
�� 
=
��  
operacionLogica
��! 0
.
��0 14
&ObtenerTipoDeComprobanteOrdenDeAlmacen
��1 W
(
��W X
ProfileData
��X c
(
��c d
)
��d e
.
��e f
Empleado
��f n
.
��n o
Id
��o q
,
��q r
ProfileData
��s ~
(
��~ 
)�� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �
List
�� 
<
�� '
SelectorTipoDeComprobante
�� .
>
��. /
comprobantes
��0 <
=
��= >'
SelectorTipoDeComprobante
��? X
.
��X Y
Convert
��Y `
(
��` a

resultados
��a k
,
��k l
ProfileData
��m x
(
��x y
)
��y z
.
��z {-
IdCentroDeAtencionSeleccionado��{ �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
comprobantes
�� (
)
��( )
;
��) *
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
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 

JsonResult
�� (
GenerarInventarioHistorico
�� 4
(
��4 5
)
��5 6
{
�� 	
try
�� 
{
�� 
var
�� 
	resultado
�� 
=
�� '
inventarioHistoricoLogica
��  9
.
��9 :>
0CrearInventarioHistoricoClonandoInventarioFisico
��: j
(
��j k
ProfileData
��k v
(
��v w
)
��w x
.
��x y
Empleado��y �
.��� �
Id��� �
)��� �
;��� �
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str
��; b
)
��b c
;
��c d
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
	resultado
��6 ?
.
��? @
code_result
��@ K
,
��K L
	resultado
��M V
.
��V W
data
��W [
,
��[ \ 
result_description
��] o
=
��p q
	resultado
��r {
.
��{ |
title��| �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M q
,
��q r
e
��s t
)
��t u
)
��u v
,
��v w
HttpStatusCode��x �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� 7
)GenerarInventarioHistoricoAutomaticamente
�� C
(
��C D
)
��D E
{
�� 	
try
�� 
{
�� 
var
�� 
	resultado
�� 
=
�� '
inventarioHistoricoLogica
��  9
.
��9 :>
0CrearInventarioHistoricoClonandoInventarioFisico
��: j
(
��j k
$num
��k l
)
��l m
;
��m n
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str
��; b
)
��b c
;
��c d
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
	resultado
��6 ?
.
��? @
code_result
��@ K
,
��K L
	resultado
��M V
.
��V W
data
��W [
,
��[ \ 
result_description
��] o
=
��p q
	resultado
��r {
.
��{ |
title��| �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M q
,
��q r
e
��s t
)
��t u
)
��u v
,
��v w
HttpStatusCode��x �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� 6
(ObtenerFechaDelUltimoInventarioHistorico
�� B
(
��B C
)
��C D
{
�� 	
try
�� 
{
�� 
var
�� 
	resultado
�� 
=
�� '
inventarioHistoricoLogica
��  9
.
��9 :6
(ObtenerFechaDelUltimoInventarioHistorico
��: b
(
��b c
ProfileData
��c n
(
��n o
)
��o p
.
��p q-
IdCentroDeAtencionSeleccionado��q �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	resultado
�� %
.
��% &
ToString
��& .
(
��. /
$str
��/ D
)
��D E
)
��E F
;
��F G
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str��M �
,��� �
e��� �
)��� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 
ActionResult
�� $
ConsultarGuiasRemision
�� 2
(
��2 3
)
��3 4
{
�� 	
List
�� 
<
�� 
string
�� 
>
�� 
fechas
�� 
=
��  !
operacionLogica
��" 1
.
��1 25
'ObtenerFechaIncioyFinParaReporteAlmacen
��2 Y
(
��Y Z
)
��Z [
;
��[ \
ViewBag
�� 
.
�� 
fechaInicio
�� 
=
��  !
fechas
��" (
[
��( )
$num
��) *
]
��* +
;
��+ ,
ViewBag
�� 
.
�� 
fechaFin
�� 
=
�� 
fechas
�� %
[
��% &
$num
��& '
]
��' (
;
��( )
ViewBag
�� 
.
�� )
idEstablecimientoPorDefecto
�� /
=
��0 1
ProfileData
��2 =
(
��= >
)
��> ?
.
��? @*
CentroDeAtencionSeleccionado
��@ \
.
��\ ]&
EstablecimientoComercial
��] u
.
��u v
Id
��v x
;
��x y
ViewBag
�� 
.
�� *
idCentroDeAtencionPorDefecto
�� 0
=
��1 2
ProfileData
��3 >
(
��> ?
)
��? @
.
��@ A*
CentroDeAtencionSeleccionado
��A ]
.
��] ^
Id
��^ `
;
��` a
ViewBag
�� 
.
�� 
	WCPScript
�� 
=
�� 
WebClientPrint
��  .
.
��. /
CreateScript
��/ ;
(
��; <
Url
�� 
.
�� 
Action
�� 
(
�� 
$str
�� '
,
��' (
$str
��) <
,
��< =
null
��> B
,
��B C
HttpContext
��D O
.
��O P
Request
��P W
.
��W X
Url
��X [
.
��[ \
Scheme
��\ b
)
��b c
,
��c d
Url
�� 
.
�� 
Action
�� 
(
�� 
$str
�� "
,
��" #
$str
��$ -
,
��- .
null
��/ 3
,
��3 4
HttpContext
��5 @
.
��@ A
Request
��A H
.
��H I
Url
��I L
.
��L M
Scheme
��M S
)
��S T
,
��T U
HttpContext
��V a
.
��a b
Session
��b i
.
��i j
	SessionID
��j s
)
��s t
;
��t u
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %"
ObtenerGuiasRemision
��& :
(
��: ;
int
��; >
[
��> ?
]
��? @"
idsCentrosDeAtencion
��A U
,
��U V
string
��W ]
desde
��^ c
,
��c d
string
��e k
hasta
��l q
)
��q r
{
�� 	
DateTime
�� 

fechaDesde
�� 
=
��  !
DateTimeUtil
��" .
.
��. /E
7ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos
��/ f
(
��f g
desde
��g l
)
��l m
;
��m n
DateTime
�� 

fechaHasta
�� 
=
��  !
DateTimeUtil
��" .
.
��. /E
7ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos
��/ f
(
��f g
hasta
��g l
)
��l m
;
��m n
try
�� 
{
�� 
var
�� 

�� !
=
��" #
operacionLogica
��$ 3
.
��3 4"
ObtenerGuiasRemision
��4 H
(
��H I"
idsCentrosDeAtencion
��I ]
,
��] ^

fechaDesde
��_ i
,
��i j

fechaHasta
��k u
)
��u v
;
��v w
var
�� !
motivosDeTransporte
�� '
=
��( )
await
��* /

��0 =
.
��= >)
ObtenerMotivosTrasladoAsync
��> Y
(
��Y Z
)
��Z [
;
��[ \
List
�� 
<
�� 2
$BandejaMovimientoMercaderiaViewModel
�� 9
>
��9 :
	respuesta
��; D
=
��E F2
$BandejaMovimientoMercaderiaViewModel
��G k
.
��k l
Convert
��l s
(
��s t

,��� �#
motivosDeTransporte��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %!
ObtenerGuiaRemision
��& 9
(
��9 :
long
��: >
idGuiaRemision
��? M
)
��M N
{
�� 	
try
�� 
{
�� 
MovimientoDeAlmacen
�� #
guiaRemision
��$ 0
=
��1 2
operacionLogica
��3 B
.
��B C!
ObtenerGuiaRemision
��C V
(
��V W
idGuiaRemision
��W e
)
��e f
;
��f g
int
�� 
[
�� 
]
�� 

idsUbigeos
��  
=
��! "
{
��# $
Convert
��% ,
.
��, -
ToInt32
��- 4
(
��4 5
guiaRemision
��5 A
.
��A B&
IdUbigeoOrigenDeTraslado
��B Z
(
��Z [
)
��[ \
)
��\ ]
,
��] ^
Convert
��_ f
.
��f g
ToInt32
��g n
(
��n o
guiaRemision
��o {
.
��{ |(
IdUbigeoDestinoDeTraslado��| �
(��� �
)��� �
)��� �
}��� �
;��� �
var
�� 
ubigeos
�� 
=
�� 

�� +
.
��+ ,

��, 9
(
��9 :

idsUbigeos
��: D
)
��D E
;
��E F
guiaRemision
�� 
.
�� 
UbigeoOrigen
�� )
=
��* +
ubigeos
��, 3
.
��3 4
Single
��4 :
(
��: ;
u
��; <
=>
��= ?
u
��@ A
.
��A B
id
��B D
==
��E G
Convert
��H O
.
��O P
ToInt32
��P W
(
��W X
guiaRemision
��X d
.
��d e&
IdUbigeoOrigenDeTraslado
��e }
(
��} ~
)
��~ 
)�� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
guiaRemision
�� 
.
�� 

�� *
=
��+ ,
ubigeos
��- 4
.
��4 5
Single
��5 ;
(
��; <
u
��< =
=>
��> @
u
��A B
.
��B C
id
��C E
==
��F H
Convert
��I P
.
��P Q
ToInt32
��Q X
(
��X Y
guiaRemision
��Y e
.
��e f'
IdUbigeoDestinoDeTraslado
��f 
(�� �
)��� �
)��� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� %
modalidadesDeTransporte
�� +
=
��, -
await
��. 3

��4 A
.
��A B-
ObtenerModalidadesTrasladoAsync
��B a
(
��a b
)
��b c
;
��c d
var
�� !
motivosDeTransporte
�� '
=
��( )
await
��* /

��0 =
.
��= >)
ObtenerMotivosTrasladoAsync
��> Y
(
��Y Z
)
��Z [
;
��[ \
byte
�� 
[
�� 
]
�� 
byteQr
�� 
=
�� 
barCodeUtil
��  +
.
��+ ,
ObtenerCodigoQR
��, ;
(
��; <
guiaRemision
��< H
.
��H I
UrlDocumentoSunat
��I Z
)
��Z [
;
��[ \
var
�� 
	documento
�� 
=
�� 
new
��  #+
DocumentoDeOperacionViewModel
��$ A
(
��A B
)
��B C
{
�� 
Id
�� 
=
�� 
idGuiaRemision
�� '
,
��' ("
SerieNumeroDocumento
�� (
=
��) *
guiaRemision
��+ 7
.
��7 8
Comprobante
��8 C
(
��C D
)
��D E
.
��E F

��F S
+
��T U
$str
��V [
+
��\ ]
guiaRemision
��^ j
.
��j k
Comprobante
��k v
(
��v w
)
��w x
.
��x y"
NumeroDeComprobante��y �
,��� �'
CadenaHtmlDeComprobante80
�� -
=
��. /#
CoreHtmlStringBuilder
��0 E
.
��E F
ObtenerHtmlString
��F W
(
��W X
guiaRemision
��X d
,
��d e
FormatoImpresion
��f v
.
��v w
_80mm
��w |
,
��| }
byteQr��~ �
,��� �
sede��� �
,��� �
proveedores��� �
,��� �'
modalidadesDeTransporte��� �
,��� �#
motivosDeTransporte��� �
,��� �
this��� �
)��� �
,��� �'
CadenaHtmlDeComprobanteA4
�� -
=
��. /#
CoreHtmlStringBuilder
��0 E
.
��E F
ObtenerHtmlString
��F W
(
��W X
guiaRemision
��X d
,
��d e
FormatoImpresion
��f v
.
��v w
A4
��w y
,
��y z
byteQr��{ �
,��� �
sede��� �
,��� �
proveedores��� �
,��� �'
modalidadesDeTransporte��� �
,��� �#
motivosDeTransporte��� �
,��� �
this��� �
)��� �
}
�� 
;
�� 
return
�� 
Json
�� 
(
�� 
	documento
�� %
)
��% &
;
��& '
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %7
)ObtenerTiposDeComprobanteParaGuiaRemision
��& O
(
��O P
)
��P Q
{
�� 	
try
�� 
{
�� 
var
�� 

resultados
�� 
=
��  
await
��! &
operacionLogica
��' 6
.
��6 77
)ObtenerTiposDeComprobanteParaGuiaRemision
��7 `
(
��` a
ProfileData
��a l
(
��l m
)
��m n
.
��n o
Empleado
��o w
.
��w x
Id
��x z
,
��z {
ProfileData��| �
(��� �
)��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �
List
�� 
<
�� '
SelectorTipoDeComprobante
�� .
>
��. /
comprobantes
��0 <
=
��= >'
SelectorTipoDeComprobante
��? X
.
��X Y
Convert
��Y `
(
��` a

resultados
��a k
)
��k l
;
��l m
return
�� 
Json
�� 
(
�� 
comprobantes
�� (
)
��( )
;
��) *
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� B
4ObtenerTiposDeComprobanteParaGuiaRemisionNotaAlmacen
�� N
(
��N O
)
��O P
{
�� 	
try
�� 
{
�� 
var
�� 

resultados
�� 
=
��  
operacionLogica
��! 0
.
��0 1>
0ObtenerTiposDeComprobanteGuiaRemisionNotaAlmacen
��1 a
(
��a b
ProfileData
��b m
(
��m n
)
��n o
.
��o p
Empleado
��p x
.
��x y
Id
��y {
,
��{ |
ProfileData��} �
(��� �
)��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
)��� �
;��� �
List
�� 
<
�� '
SelectorTipoDeComprobante
�� .
>
��. /
comprobantes
��0 <
=
��= >'
SelectorTipoDeComprobante
��? X
.
��X Y
Convert
��Y `
(
��` a

resultados
��a k
)
��k l
;
��l m
return
�� 
Json
�� 
(
�� 
comprobantes
�� (
)
��( )
;
��) *
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %!
GuardarGuiaRemision
��& 9
(
��9 :2
$RegistroMovimientoDeAlmacenViewModel
��: ^
guiaRemision
��_ k
)
��k l
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
;
��& '
List
�� 
<
�� !
Detalle_transaccion
�� (
>
��( )
detalles
��* 2
=
��3 42
$ConstruirDetalleMovimientoMercaderia
��5 Y
(
��Y Z
guiaRemision
��Z f
)
��f g
;
��g h
result
�� 
=
�� 
operacionLogica
�� (
.
��( )!
GuardarGuiaRemision
��) <
(
��< =
guiaRemision
��= I
.
��I J
Tercero
��J Q
.
��Q R
Id
��R T
,
��T U
guiaRemision
��V b
.
��b c
TipoDeComprobante
��c t
.
��t u
EsPropio
��u }
,
��} ~
guiaRemision�� �
.��� �!
TipoDeComprobante��� �
.��� �
TipoComprobante��� �
==��� �
null��� �
?��� �
$num��� �
:��� �
guiaRemision��� �
.��� �!
TipoDeComprobante��� �
.��� �
TipoComprobante��� �
.��� �
Id��� �
,��� �
guiaRemision��� �
.��� �!
TipoDeComprobante��� �
.��� �!
SerieSeleccionada��� �
,��� �
guiaRemision��� �
.��� �!
TipoDeComprobante��� �
.��� �
SerieIngresada��� �
,��� �
guiaRemision��� �
.��� �!
TipoDeComprobante��� �
.��� �
NumeroIngresado��� �
,��� �
guiaRemision��� �
.��� �#
FechaInicioTraslado��� �
,��� �
guiaRemision��� �
.��� �

.��� �

==��� �
null��� �
?��� �
$num��� �
:��� �
guiaRemision��� �
.��� �

.��� �

.��� �
Id��� �
,��� �
guiaRemision��� �
.��� �

.��� �
Placa��� �
,��� �
guiaRemision��� �
.��� �
	Conductor��� �
.��� �
	Conductor��� �
==��� �
null��� �
?��� �
$num��� �
:��� �
guiaRemision��� �
.��� �
	Conductor��� �
.��� �
	Conductor��� �
.��� �
Id��� �
,��� �
guiaRemision��� �
.��� �
	Conductor��� �
.��� �
NumeroLicencia��� �
,��� �
guiaRemision��� �
.��� �#
ModalidadTransporte��� �
==��� �
null��� �
?��� �
$num��� �
:��� �
guiaRemision��� �
.��� �#
ModalidadTransporte��� �
.��� �
Id��� �
,��� �
guiaRemision��� �
.��� �
MotivoTraslado��� �
==��� �
null��� �
?��� �
$num��� �
:��� �
guiaRemision��� �
.��� �
MotivoTraslado��� �
.��� �
Id��� �
,��� �
guiaRemision��� �
.��� �!
DescripcionMotivo��� �
,��� �
guiaRemision��� �
.��� �
PesoBrutoTotal��� �
,��� �
guiaRemision��� �
.��� �
NumeroBultos��� �
,��� �
guiaRemision��� �
.��� �
DireccionOrigen��� �
,��� �
guiaRemision��� �
.��� �
UbigeoOrigen��� �
==��� �
null��� �
?��� �
$num��� �
:��� �
guiaRemision��� �
.��� �
UbigeoOrigen��� �
.��� �
Id��� �
,��� �
guiaRemision��� �
.��� � 
DireccionDestino��� �
,��� �
guiaRemision��� �
.��� �

==��� �
null��� �
?��� �
$num��� �
:��� �
guiaRemision��� �
.��� �

.��� �
Id��� �
,��� �
guiaRemision��� �
.��� �#
DocumentoReferencia��� �	
,���	 �	
guiaRemision���	 �	
.���	 �	
Observacion���	 �	
,���	 �	
detalles���	 �	
,���	 �	
ProfileData���	 �	
(���	 �	
)���	 �	
)���	 �	
;���	 �	
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 g
)
��g h
;
��h i
string
�� 
path
�� 
=
��  
HostingEnvironment
�� 0
.
��0 1%
ApplicationPhysicalPath
��1 H
;
��H I
await
�� *
facturacionElectronicaLogica
�� 2
.
��2 3,
TransmitirEnviarGuiaDeRemision
��3 Q
(
��Q R
result
��R X
.
��X Y
data
��Y ]
,
��] ^
ProfileData
��_ j
(
��j k
)
��k l
.
��l m
Sede
��m q
,
��q r
ProfileData
��s ~
(
��~ 
)�� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �
path��� �
)��� �
;��� �
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
result
��6 <
.
��< =
code_result
��= H
,
��H I
result
��J P
.
��P Q
data
��Q U
,
��U V 
result_description
��W i
=
��j k
result
��l r
.
��r s
title
��s x
}
��y z
,
��z {
HttpStatusCode��| �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M v
,
��v w
e
��x y
)
��y z
)
��z {
,
��{ |
HttpStatusCode��} �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %1
#EnviarCorreoElectronicoConDocumento
��& I
(
��I J
long
��J N
idGuiaRemision
��O ]
,
��] ^
int
��_ b
formato
��c j
,
��j k
List
��l p
<
��p q
string
��q w
>
��w x"
correosElectronicos��y �
)��� �
{
�� 	
try
�� 
{
�� 
MovimientoDeAlmacen
�� #
guiaRemision
��$ 0
=
��1 2
operacionLogica
��3 B
.
��B C!
ObtenerGuiaRemision
��C V
(
��V W
idGuiaRemision
��W e
)
��e f
;
��f g
int
�� 
[
�� 
]
�� 

idsUbigeos
��  
=
��! "
{
��# $
Convert
��% ,
.
��, -
ToInt32
��- 4
(
��4 5
guiaRemision
��5 A
.
��A B&
IdUbigeoOrigenDeTraslado
��B Z
(
��Z [
)
��[ \
)
��\ ]
,
��] ^
Convert
��_ f
.
��f g
ToInt32
��g n
(
��n o
guiaRemision
��o {
.
��{ |(
IdUbigeoDestinoDeTraslado��| �
(��� �
)��� �
)��� �
}��� �
;��� �
var
�� 
ubigeos
�� 
=
�� 

�� +
.
��+ ,

��, 9
(
��9 :

idsUbigeos
��: D
)
��D E
;
��E F
guiaRemision
�� 
.
�� 
UbigeoOrigen
�� )
=
��* +
ubigeos
��, 3
.
��3 4
Single
��4 :
(
��: ;
u
��; <
=>
��= ?
u
��@ A
.
��A B
id
��B D
==
��E G
Convert
��H O
.
��O P
ToInt32
��P W
(
��W X
guiaRemision
��X d
.
��d e&
IdUbigeoOrigenDeTraslado
��e }
(
��} ~
)
��~ 
)�� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
guiaRemision
�� 
.
�� 

�� *
=
��+ ,
ubigeos
��- 4
.
��4 5
Single
��5 ;
(
��; <
u
��< =
=>
��> @
u
��A B
.
��B C
id
��C E
==
��F H
Convert
��I P
.
��P Q
ToInt32
��Q X
(
��X Y
guiaRemision
��Y e
.
��e f'
IdUbigeoDestinoDeTraslado
��f 
(�� �
)��� �
)��� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� %
modalidadesDeTransporte
�� +
=
��, -
await
��. 3

��4 A
.
��A B-
ObtenerModalidadesTrasladoAsync
��B a
(
��a b
)
��b c
;
��c d
var
�� !
motivosDeTransporte
�� '
=
��( )
await
��* /

��0 =
.
��= >)
ObtenerMotivosTrasladoAsync
��> Y
(
��Y Z
)
��Z [
;
��[ \
byte
�� 
[
�� 
]
�� 
byteQr
�� 
=
�� 
barCodeUtil
��  +
.
��+ ,
ObtenerCodigoQR
��, ;
(
��; <
guiaRemision
��< H
.
��H I
UrlDocumentoSunat
��I Z
)
��Z [
;
��[ \
PdfDocument
�� 
pdfGuia
�� #
=
��$ %

PdfBuilder
��& 0
.
��0 1.
 ObtenerPdfMovimientoDeMercaderia
��1 Q
(
��Q R
guiaRemision
��R ^
,
��^ _
sede
��` d
,
��d e
byteQr
��f l
,
��l m
(
��n o
FormatoImpresion
��o 
)�� �
formato��� �
,��� �
proveedores��� �
,��� �'
modalidadesDeTransporte��� �
,��� �#
motivosDeTransporte��� �
,��� �
this��� �
)��� �
;��� �
string
�� 
asunto
�� 
=
�� 
operacionLogica
��  /
.
��/ 0.
 ObtenerAsuntoDeCorreoElectronico
��0 P
(
��P Q
sede
��Q U
,
��U V
guiaRemision
��W c
)
��c d
;
��d e
string
�� 
cuerpo
�� 
=
�� 
operacionLogica
��  /
.
��/ 0.
 ObtenerCuerpoDeCorreoElectronico
��0 P
(
��P Q
sede
��Q U
,
��U V
guiaRemision
��W c
)
��c d
;
��d e
OperationResult
�� 
result
��  &
=
��' (
mailer
��) /
.
��/ 0
Send
��0 4
(
��4 5
asunto
��5 ;
,
��; <
cuerpo
��= C
,
��C D!
correosElectronicos
��E X
,
��X Y 
AplicacionSettings
��Z l
.
��l m
Default
��m t
.
��t u

,��� �
new��� �
List��� �
<��� �

Attachment��� �
>��� �
(��� �
)��� �
{��� �
new��� �

Attachment��� �
(��� �
new��� �
MemoryStream��� �
(��� �
pdfGuia��� �
.��� �
Save��� �
(��� �
)��� �
)��� �
,��� �
guiaRemision��� �
.��� �
Comprobante��� �
(��� �
)��� �
.��� �

+��� �
$str��� �
+��� �
guiaRemision��� �
.��� �
Comprobante��� �
(��� �
)��� �
.��� �#
NumeroDeComprobante��� �
+��� �
$str��� �
+��� �
guiaRemision��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
,��� �
$str��� �
)��� �
}��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
data
��6 :
=
��; <
result
��= C
.
��C D
information
��D O
,
��O P 
result_description
��Q c
=
��d e
result
��f l
.
��l m
title
��m r
}
��s t
)
��t u
;
��u v
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& ' 
DescargarDocumento
��( :
(
��: ;
long
��; ?
idGuiaRemision
��@ N
,
��N O
int
��P S
formato
��T [
)
��[ \
{
�� 	
try
�� 
{
�� 
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )!
MovimientoDeAlmacen
�� #
guiaRemision
��$ 0
=
��1 2
operacionLogica
��3 B
.
��B C!
ObtenerGuiaRemision
��C V
(
��V W
idGuiaRemision
��W e
)
��e f
;
��f g
int
�� 
[
�� 
]
�� 

idsUbigeos
��  
=
��! "
{
��# $
Convert
��% ,
.
��, -
ToInt32
��- 4
(
��4 5
guiaRemision
��5 A
.
��A B&
IdUbigeoOrigenDeTraslado
��B Z
(
��Z [
)
��[ \
)
��\ ]
,
��] ^
Convert
��_ f
.
��f g
ToInt32
��g n
(
��n o
guiaRemision
��o {
.
��{ |(
IdUbigeoDestinoDeTraslado��| �
(��� �
)��� �
)��� �
}��� �
;��� �
var
�� 
ubigeos
�� 
=
�� 

�� +
.
��+ ,

��, 9
(
��9 :

idsUbigeos
��: D
)
��D E
;
��E F
guiaRemision
�� 
.
�� 
UbigeoOrigen
�� )
=
��* +
ubigeos
��, 3
.
��3 4
Single
��4 :
(
��: ;
u
��; <
=>
��= ?
u
��@ A
.
��A B
id
��B D
==
��E G
Convert
��H O
.
��O P
ToInt32
��P W
(
��W X
guiaRemision
��X d
.
��d e&
IdUbigeoOrigenDeTraslado
��e }
(
��} ~
)
��~ 
)�� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
guiaRemision
�� 
.
�� 

�� *
=
��+ ,
ubigeos
��- 4
.
��4 5
Single
��5 ;
(
��; <
u
��< =
=>
��> @
u
��A B
.
��B C
id
��C E
==
��F H
Convert
��I P
.
��P Q
ToInt32
��Q X
(
��X Y
guiaRemision
��Y e
.
��e f'
IdUbigeoDestinoDeTraslado
��f 
(�� �
)��� �
)��� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� #
modalidadesDeTraslado
�� )
=
��* +
await
��, 1

��2 ?
.
��? @-
ObtenerModalidadesTrasladoAsync
��@ _
(
��_ `
)
��` a
;
��a b
var
�� 
motivosDeTraslado
�� %
=
��& '
await
��( -

��. ;
.
��; <)
ObtenerMotivosTrasladoAsync
��< W
(
��W X
)
��X Y
;
��Y Z
string
�� 
	QrContent
��  
=
��! "*
facturacionElectronicaLogica
��# ?
.
��? @
	ObtenerQR
��@ I
(
��I J
guiaRemision
��J V
,
��V W
sede
��X \
)
��\ ]
;
��] ^
byte
�� 
[
�� 
]
�� 
QrBytes
�� 
=
��  
(
��! "
guiaRemision
��" .
.
��. /
IdTipoComprobante
��/ @
==
��A C
MaestroSettings
��D S
.
��S T
Default
��T [
.
��[ \A
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��\ �
)��� �
?��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
guiaRemision��� �
.��� �
Informacion��� �
)��� �
:��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
	QrContent��� �
)��� �
;��� �
;��� �
var
�� 
pdfGuia
�� 
=
�� 

PdfBuilder
�� (
.
��( ).
 ObtenerPdfMovimientoDeMercaderia
��) I
(
��I J
guiaRemision
��J V
,
��V W
sede
��X \
,
��\ ]
QrBytes
��^ e
,
��e f
(
��g h
FormatoImpresion
��h x
)
��x y
formato��y �
,��� �
proveedores��� �
,��� �%
modalidadesDeTraslado��� �
,��� �!
motivosDeTraslado��� �
,��� �
this��� �
)��� �
;��� �
string
�� 
fileNameZip
�� "
=
��# $
guiaRemision
��% 1
.
��1 2
Comprobante
��2 =
(
��= >
)
��> ?
.
��? @

��@ M
+
��N O
$str
��P U
+
��V W
guiaRemision
��X d
.
��d e
Comprobante
��e p
(
��p q
)
��q r
.
��r s"
NumeroDeComprobante��s �
+��� �
$str��� �
+��� �
guiaRemision��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
;��� �
byte
�� 
[
�� 
]
�� 
	fileBytes
��  
=
��! "
null
��# '
;
��' (
byte
�� 
[
�� 
]
�� 
compressedBytes
�� &
;
��& '
using
�� 
(
�� 
var
�� 
	outStream
�� $
=
��% &
new
��' *
MemoryStream
��+ 7
(
��7 8
)
��8 9
)
��9 :
{
�� 
using
�� 
(
�� 
var
�� 
archive
�� &
=
��' (
new
��) ,

ZipArchive
��- 7
(
��7 8
	outStream
��8 A
,
��A B
ZipArchiveMode
��C Q
.
��Q R
Create
��R X
,
��X Y
true
��Z ^
)
��^ _
)
��_ `
{
�� 
ZipArchiveEntry
�� '

��( 5
=
��6 7
null
��8 <
;
��< =

�� %
=
��& '
archive
��( /
.
��/ 0
CreateEntry
��0 ;
(
��; <
guiaRemision
��< H
.
��H I
Comprobante
��I T
(
��T U
)
��U V
.
��V W

��W d
+
��e f
$str
��g l
+
��m n
guiaRemision
��o {
.
��{ |
Comprobante��| �
(��� �
)��� �
.��� �#
NumeroDeComprobante��� �
+��� �
$str��� �
+��� �
guiaRemision��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
,��� � 
CompressionLevel��� �
.��� �
Optimal��� �
)��� �
;��� �
	fileBytes
�� !
=
��" #
pdfGuia
��$ +
.
��+ ,
Save
��, 0
(
��0 1
)
��1 2
;
��2 3
using
�� 
(
�� 
var
�� "
entryStream
��# .
=
��/ 0

��1 >
.
��> ?
Open
��? C
(
��C D
)
��D E
)
��E F
using
�� 
(
�� 
var
�� ""
fileToCompressStream
��# 7
=
��8 9
new
��: =
MemoryStream
��> J
(
��J K
	fileBytes
��K T
)
��T U
)
��U V
{
�� "
fileToCompressStream
�� 0
.
��0 1
CopyTo
��1 7
(
��7 8
entryStream
��8 C
)
��C D
;
��D E"
fileToCompressStream
�� 0
.
��0 1
Close
��1 6
(
��6 7
)
��7 8
;
��8 9
}
�� 

�� %
=
��& '
archive
��( /
.
��/ 0
CreateEntry
��0 ;
(
��; <
guiaRemision
��< H
.
��H I
Comprobante
��I T
(
��T U
)
��U V
.
��V W

��W d
+
��e f
$str
��g l
+
��m n
guiaRemision
��o {
.
��{ |
Comprobante��| �
(��� �
)��� �
.��� �#
NumeroDeComprobante��� �
+��� �
$str��� �
+��� �
guiaRemision��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
,��� � 
CompressionLevel��� �
.��� �
Optimal��� �
)��� �
;��� �
	fileBytes
�� !
=
��" #

XmlBuilder
��$ .
.
��. /#
ObtenerXmlComprobante
��/ D
(
��D E
guiaRemision
��E Q
,
��Q R
sede
��S W
,
��W X
proveedores
��Y d
,
��d e#
modalidadesDeTraslado
��f {
,
��{ | 
motivosDeTraslado��} �
,��� �(
generacionArchivosLogica��� �
,��� �,
facturacionElectronicaLogica��� �
)��� �
.��� �
	Documento��� �
;��� �
using
�� 
(
�� 
var
�� "
entryStream
��# .
=
��/ 0

��1 >
.
��> ?
Open
��? C
(
��C D
)
��D E
)
��E F
using
�� 
(
�� 
var
�� ""
fileToCompressStream
��# 7
=
��8 9
new
��: =
MemoryStream
��> J
(
��J K
	fileBytes
��K T
)
��T U
)
��U V
{
�� "
fileToCompressStream
�� 0
.
��0 1
CopyTo
��1 7
(
��7 8
entryStream
��8 C
)
��C D
;
��D E"
fileToCompressStream
�� 0
.
��0 1
Close
��1 6
(
��6 7
)
��7 8
;
��8 9
}
�� 
}
�� 
compressedBytes
�� #
=
��$ %
	outStream
��& /
.
��/ 0
ToArray
��0 7
(
��7 8
)
��8 9
;
��9 :
}
�� 
return
�� 
File
�� 
(
�� 
compressedBytes
�� +
,
��+ ,
$str
��- >
,
��> ?
fileNameZip
��@ K
)
��K L
;
��L M
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M w
,
��w x
e
��y z
)
��z {
)
��{ |
,
��| }
HttpStatusCode��~ �
.��� �#
InternalServerError��� �
)��� �
;��� �
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
�� 
ActionResult
�� &
>
��& '#
DescargarDocumentoPdf
��( =
(
��= >
long
��> B
idGuiaRemision
��C Q
,
��Q R
int
��S V
formato
��W ^
)
��^ _
{
�� 	
try
�� 
{
�� 
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )!
MovimientoDeAlmacen
�� #
guiaRemision
��$ 0
=
��1 2
operacionLogica
��3 B
.
��B C!
ObtenerGuiaRemision
��C V
(
��V W
idGuiaRemision
��W e
)
��e f
;
��f g
int
�� 
[
�� 
]
�� 

idsUbigeos
��  
=
��! "
{
��# $
Convert
��% ,
.
��, -
ToInt32
��- 4
(
��4 5
guiaRemision
��5 A
.
��A B&
IdUbigeoOrigenDeTraslado
��B Z
(
��Z [
)
��[ \
)
��\ ]
,
��] ^
Convert
��_ f
.
��f g
ToInt32
��g n
(
��n o
guiaRemision
��o {
.
��{ |(
IdUbigeoDestinoDeTraslado��| �
(��� �
)��� �
)��� �
}��� �
;��� �
var
�� 
ubigeos
�� 
=
�� 

�� +
.
��+ ,

��, 9
(
��9 :

idsUbigeos
��: D
)
��D E
;
��E F
guiaRemision
�� 
.
�� 
UbigeoOrigen
�� )
=
��* +
ubigeos
��, 3
.
��3 4
Single
��4 :
(
��: ;
u
��; <
=>
��= ?
u
��@ A
.
��A B
id
��B D
==
��E G
Convert
��H O
.
��O P
ToInt32
��P W
(
��W X
guiaRemision
��X d
.
��d e&
IdUbigeoOrigenDeTraslado
��e }
(
��} ~
)
��~ 
)�� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
guiaRemision
�� 
.
�� 

�� *
=
��+ ,
ubigeos
��- 4
.
��4 5
Single
��5 ;
(
��; <
u
��< =
=>
��> @
u
��A B
.
��B C
id
��C E
==
��F H
Convert
��I P
.
��P Q
ToInt32
��Q X
(
��X Y
guiaRemision
��Y e
.
��e f'
IdUbigeoDestinoDeTraslado
��f 
(�� �
)��� �
)��� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� #
modalidadesDeTraslado
�� )
=
��* +
await
��, 1

��2 ?
.
��? @-
ObtenerModalidadesTrasladoAsync
��@ _
(
��_ `
)
��` a
;
��a b
var
�� 
motivosDeTraslado
�� %
=
��& '
await
��( -

��. ;
.
��; <)
ObtenerMotivosTrasladoAsync
��< W
(
��W X
)
��X Y
;
��Y Z
string
�� 
	QrContent
��  
=
��! "*
facturacionElectronicaLogica
��# ?
.
��? @
	ObtenerQR
��@ I
(
��I J
guiaRemision
��J V
,
��V W
sede
��X \
)
��\ ]
;
��] ^
byte
�� 
[
�� 
]
�� 
QrBytes
�� 
=
��  
(
��! "
guiaRemision
��" .
.
��. /
IdTipoComprobante
��/ @
==
��A C
MaestroSettings
��D S
.
��S T
Default
��T [
.
��[ \A
2IdDetalleMaestroComprobanteGuiaDeRemisionRemitente��\ �
)��� �
?��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
guiaRemision��� �
.��� �
Informacion��� �
)��� �
:��� �
barCodeUtil��� �
.��� �
ObtenerCodigoQR��� �
(��� �
	QrContent��� �
)��� �
;��� �
;��� �
var
�� 
pdfGuia
�� 
=
�� 

PdfBuilder
�� (
.
��( ).
 ObtenerPdfMovimientoDeMercaderia
��) I
(
��I J
guiaRemision
��J V
,
��V W
sede
��X \
,
��\ ]
QrBytes
��^ e
,
��e f
(
��g h
FormatoImpresion
��h x
)
��x y
formato��y �
,��� �
proveedores��� �
,��� �%
modalidadesDeTraslado��� �
,��� �!
motivosDeTraslado��� �
,��� �
this��� �
)��� �
;��� �
byte
�� 
[
�� 
]
�� 
	fileBytes
��  
=
��! "
pdfGuia
��# *
.
��* +
Save
��+ /
(
��/ 0
)
��0 1
;
��1 2
string
�� 
fileName
�� 
=
��  !
guiaRemision
��" .
.
��. /
Comprobante
��/ :
(
��: ;
)
��; <
.
��< =

��= J
+
��K L
$str
��M R
+
��S T
guiaRemision
��U a
.
��a b
Comprobante
��b m
(
��m n
)
��n o
.
��o p"
NumeroDeComprobante��p �
+��� �
$str��� �
+��� �
guiaRemision��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
;��� �
return
�� 
File
�� 
(
�� 
	fileBytes
�� %
,
��% &
$str
��' 8
,
��8 9
fileName
��: B
)
��B C
;
��C D
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M {
,
��{ |
e
��} ~
)
��~ 
)�� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
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
�� 
ActionResult
�� &
>
��& '#
DescargarDocumentoXml
��( =
(
��= >
long
��> B
idGuiaRemision
��C Q
)
��Q R
{
�� 	
try
�� 
{
�� 
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )!
MovimientoDeAlmacen
�� #
guiaRemision
��$ 0
=
��1 2
operacionLogica
��3 B
.
��B C!
ObtenerGuiaRemision
��C V
(
��V W
idGuiaRemision
��W e
)
��e f
;
��f g
int
�� 
[
�� 
]
�� 

idsUbigeos
��  
=
��! "
{
��# $
Convert
��% ,
.
��, -
ToInt32
��- 4
(
��4 5
guiaRemision
��5 A
.
��A B&
IdUbigeoOrigenDeTraslado
��B Z
(
��Z [
)
��[ \
)
��\ ]
,
��] ^
Convert
��_ f
.
��f g
ToInt32
��g n
(
��n o
guiaRemision
��o {
.
��{ |(
IdUbigeoDestinoDeTraslado��| �
(��� �
)��� �
)��� �
}��� �
;��� �
var
�� 
ubigeos
�� 
=
�� 

�� +
.
��+ ,

��, 9
(
��9 :

idsUbigeos
��: D
)
��D E
;
��E F
guiaRemision
�� 
.
�� 
UbigeoOrigen
�� )
=
��* +
ubigeos
��, 3
.
��3 4
Single
��4 :
(
��: ;
u
��; <
=>
��= ?
u
��@ A
.
��A B
id
��B D
==
��E G
Convert
��H O
.
��O P
ToInt32
��P W
(
��W X
guiaRemision
��X d
.
��d e&
IdUbigeoOrigenDeTraslado
��e }
(
��} ~
)
��~ 
)�� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
guiaRemision
�� 
.
�� 

�� *
=
��+ ,
ubigeos
��- 4
.
��4 5
Single
��5 ;
(
��; <
u
��< =
=>
��> @
u
��A B
.
��B C
id
��C E
==
��F H
Convert
��I P
.
��P Q
ToInt32
��Q X
(
��X Y
guiaRemision
��Y e
.
��e f'
IdUbigeoDestinoDeTraslado
��f 
(�� �
)��� �
)��� �
)��� �
.��� �!
descripcion_corta��� �
;��� �
var
�� 
proveedores
�� 
=
��  ! 
actorNegocioLogica
��" 4
.
��4 5(
ObtenerProveedoresVigentes
��5 O
(
��O P
)
��P Q
;
��Q R
var
�� #
modalidadesDeTraslado
�� )
=
��* +
await
��, 1

��2 ?
.
��? @-
ObtenerModalidadesTrasladoAsync
��@ _
(
��_ `
)
��` a
;
��a b
var
�� 
motivosDeTraslado
�� %
=
��& '
await
��( -

��. ;
.
��; <)
ObtenerMotivosTrasladoAsync
��< W
(
��W X
)
��X Y
;
��Y Z
byte
�� 
[
�� 
]
�� 
	fileBytes
��  
=
��! "

XmlBuilder
��# -
.
��- .#
ObtenerXmlComprobante
��. C
(
��C D
guiaRemision
��D P
,
��P Q
sede
��R V
,
��V W
proveedores
��X c
,
��c d#
modalidadesDeTraslado
��e z
,
��z { 
motivosDeTraslado��| �
,��� �(
generacionArchivosLogica��� �
,��� �,
facturacionElectronicaLogica��� �
)��� �
.��� �
	Documento��� �
;��� �
string
�� 
fileName
�� 
=
��  !
guiaRemision
��" .
.
��. /
Comprobante
��/ :
(
��: ;
)
��; <
.
��< =

��= J
+
��K L
$str
��M R
+
��S T
guiaRemision
��U a
.
��a b
Comprobante
��b m
(
��m n
)
��n o
.
��o p"
NumeroDeComprobante��p �
+��� �
$str��� �
+��� �
guiaRemision��� �
.��� �
Tercero��� �
(��� �
)��� �
.��� �
RazonSocial��� �
+��� �
$str��� �
;��� �
return
�� 
File
�� 
(
�� 
	fileBytes
�� %
,
��% &
$str
��' 8
,
��8 9
fileName
��: B
)
��B C
;
��C D
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M {
,
��{ |
e
��} ~
)
��~ 
)�� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� #
InvalidarGuiaRemision
�� /
(
��/ 0
long
��0 4
idGuiaRemision
��5 C
,
��C D
string
��E K
observacion
��L W
)
��W X
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' (
operacionLogica
��) 8
.
��8 9#
InvalidarGuiaRemision
��9 N
(
��N O
idGuiaRemision
��O ]
,
��] ^
ProfileData
��_ j
(
��j k
)
��k l
.
��l m
Empleado
��m u
.
��u v
Id
��v x
,
��x y
ProfileData��z �
(��� �
)��� �
.��� �.
IdCentroDeAtencionSeleccionado��� �
,��� �
observacion��� �
)��� �
;��� �
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 i
)
��i j
;
��j k
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
result
��6 <
.
��< =
data
��= A
,
��A B 
result_description
��C U
=
��V W
result
��X ^
.
��^ _
title
��_ d
}
��e f
)
��f g
;
��g h
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� :
,ObtenerParametrosParaRegistradorGuiaRemision
�� F
(
��F G
)
��G H
{
�� 	
try
�� 
{
�� 
var
�� 

�� !
=
��" #
new
��$ '2
$ConfiguracionRegistradorGuiaRemision
��( L
{
�� 

�� !
=
��" #
(
��$ %
ObtenerSede
��% 0
(
��0 1
)
��1 2
.
��2 3
DomicilioFiscal
��3 B
!=
��C E
null
��F J
)
��J K
?
��L M
ObtenerSede
��N Y
(
��Y Z
)
��Z [
.
��[ \
DomicilioFiscal
��\ k
.
��k l
Detalle
��l s
:
��t u
$str
��v x
,
��x y
IdUbigeoSede
��  
=
��! "
(
��# $
ObtenerSede
��$ /
(
��/ 0
)
��0 1
.
��1 2
DomicilioFiscal
��2 A
!=
��B D
null
��E I
)
��I J
?
��K L
ObtenerSede
��M X
(
��X Y
)
��Y Z
.
��Z [
DomicilioFiscal
��[ j
.
��j k
Ubigeo
��k q
.
��q r
Id
��r t
:
��u v

.��� �
Default��� �
.��� �.
IdUbigeoSeleccionadoPorDefecto��� �
,��� �!
NumeroDocumentoSede
�� '
=
��( )
ObtenerSede
��* 5
(
��5 6
)
��6 7
.
��7 8 
DocumentoIdentidad
��8 J
}
�� 
;
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
data
��" &
=
��' (

��) 6
}
��7 8
)
��8 9
;
��9 :
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 
async
�� 
Task
�� 
<
�� 

JsonResult
�� $
>
��$ %!
EnviarGuiasRemision
��& 9
(
��9 :
string
��: @
desde
��A F
,
��F G
string
��H N
hasta
��O T
)
��T U
{
�� 	
try
�� 
{
�� 
DateTime
�� 

fechaDesde
�� #
=
��$ %
DateTimeUtil
��& 2
.
��2 3E
7ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos
��3 j
(
��j k
desde
��k p
)
��p q
;
��q r
DateTime
�� 

fechaHasta
�� #
=
��$ %
DateTimeUtil
��& 2
.
��2 3E
7ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos
��3 j
(
��j k
hasta
��k p
)
��p q
;
��q r
string
�� 
path
�� 
=
��  
HostingEnvironment
�� 0
.
��0 1%
ApplicationPhysicalPath
��1 H
;
��H I
OperationResult
�� 
result
��  &
=
��' (
await
��) .*
facturacionElectronicaLogica
��/ K
.
��K L(
EnviarGuiaDeRemisionManual
��L f
(
��f g

fechaDesde
��g q
,
��q r

fechaHasta
��s }
,
��} ~
ProfileData�� �
(��� �
)��� �
.��� �
Sede��� �
,��� �
ProfileData��� �
(��� �
)��� �
.��� �
Empleado��� �
.��� �
Id��� �
,��� �
path��� �
)��� �
;��� �
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
result
��6 <
.
��< =
code_result
��= H
,
��H I
result
��J P
.
��P Q
data
��Q U
,
��U V 
result_description
��W i
=
��j k
result
��l r
.
��r s
title
��s x
}
��y z
,
��z {
HttpStatusCode��| �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M x
,
��x y
e
��z {
)
��{ |
)
��| }
,
��} ~
HttpStatusCode�� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� ,
)
��, -
]
��- .
public
�� 

JsonResult
�� )
ConsultarEnvioGuiasRemision
�� 5
(
��5 6
)
��6 7
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' (*
facturacionElectronicaLogica
��) E
.
��E F*
ConsultarGuiasRemisionManual
��F b
(
��b c
)
��c d
;
��d e
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
result
��6 <
.
��< =
code_result
��= H
,
��H I
result
��J P
.
��P Q
data
��Q U
,
��U V 
result_description
��W i
=
��j k
result
��l r
.
��r s
title
��s x
}
��y z
,
��z {
HttpStatusCode��| �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M {
,
��{ |
e
��} ~
)
��~ 
)�� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
}
�� 
}�� �
hD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\AlmacenAjustesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class $
AlmacenAjustesController 1
:2 3
BaseController4 B
{ 
	protected		 
readonly		 $
IAjusteInventario_Logica		 3
_ajusteInventario		4 E
;		E F
public
AlmacenAjustesController
(
)
:
base
(
)
{ 	
_ajusteInventario 
= 
Dependencia  +
.+ ,
Resolve, 3
<3 4$
IAjusteInventario_Logica4 L
>L M
(M N
)N O
;O P
} 	
[ 	
	Authorize	 
( 
Roles 
= 
$str ,
), -
]- .
public 
void 
CuadrarStock  
(  !
)! "
{ 	
_ajusteInventario !
.! "?
3CuadrarStockFisicoEntreInventarioActualYMovimientos" U
(U V
)V W
;W X
} 	
[ 	
	Authorize	 
( 
Roles 
= 
$str ,
), -
]- .
public 
void #
CorregirCostosUnitarios +
(+ ,
), -
{ 	
_ajusteInventario 
. 6
*RecalcularCostoUnitarioYTotalEnMovimientos H
(H I
)I J
;J K
} 	
}   
}!! ��
iD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\AlmacenReportesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class %
AlmacenReportesController 2
:3 4
BaseController5 C
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly "
IAlmacenReporte_Logica 1"
almacenReportingLogica2 H
;H I
	protected 
readonly $
IInventarioActual_Logica 3#
inventarioActual_Logica4 K
;K L
	protected 
readonly $
ICentroDeAtencion_Logica 3#
centroDeAtencion_Logica4 K
;K L
public!! %
AlmacenReportesController!! (
(!!( )
)!!) *
:!!* +
base!!+ /
(!!/ 0
)!!0 1
{"" 	
operacionLogica## 
=## 
Dependencia## )
.##) *
Resolve##* 1
<##1 2
IOperacionLogica##2 B
>##B C
(##C D
)##D E
;##E F
actorNegocioLogica$$ 
=$$  
Dependencia$$! ,
.$$, -
Resolve$$- 4
<$$4 5
IActorNegocioLogica$$5 H
>$$H I
($$I J
)$$J K
;$$K L
conceptoLogica%% 
=%% 
Dependencia%% (
.%%( )
Resolve%%) 0
<%%0 1
IConceptoLogica%%1 @
>%%@ A
(%%A B
)%%B C
;%%C D"
almacenReportingLogica&& "
=&&# $
Dependencia&&% 0
.&&0 1
Resolve&&1 8
<&&8 9"
IAlmacenReporte_Logica&&9 O
>&&O P
(&&P Q
)&&Q R
;&&R S#
inventarioActual_Logica'' #
=''$ %
Dependencia''& 1
.''1 2
Resolve''2 9
<''9 :$
IInventarioActual_Logica'': R
>''R S
(''S T
)''T U
;''U V#
centroDeAtencion_Logica)) #
=))$ %
Dependencia))& 1
.))1 2
Resolve))2 9
<))9 :$
ICentroDeAtencion_Logica)): R
>))R S
())S T
)))T U
;))U V
}++ 	
[-- 	
	Authorize--	 
(-- 
Roles-- 
=-- 
$str-- D
)--D E
]--E F
public.. 
ActionResult.. 
	Principal.. %
(..% &
)..& '
{// 	
ViewBag00 
.00 
Data00 
=00 "
almacenReportingLogica00 5
.005 6,
 ObtenerDatosParaReportePrincipal006 V
(00V W
ProfileData00W b
(00b c
)00c d
)00d e
;00e f
return11 
View11 
(11 
)11 
;11 
}22 	
[44 	
	Authorize44	 
(44 
Roles44 
=44 
$str44 D
)44D E
]44E F
public55 
ActionResult55 

(55) *
)55* +
{66 	
List77 
<77 
DateTime77 
>77 
fechas77 !
=77" #
operacionLogica77$ 3
.773 4=
1ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta774 e
(77e f
)77f g
;77g h
ViewBag88 
.88 
fechaInicio88 
=88  !
fechas88" (
[88( )
$num88) *
]88* +
.88+ ,
AddDays88, 3
(883 4
-884 5
$num885 6
)886 7
;887 8
ViewBag99 
.99 
fechaFin99 
=99 
fechas99 %
[99% &
$num99& '
]99' (
;99( )
return:: 
View:: 
(:: 
):: 
;:: 
};; 	
public== 
ActionResult== !
ReporteConceptoBasico== 1
(==1 2
)==2 3
{>> 	
List?? 
<?? 
DateTime?? 
>?? 
fechas?? !
=??" #
operacionLogica??$ 3
.??3 4=
1ObtenerFechaIncioyFinParaReporteVentaPuntoDeVenta??4 e
(??e f
)??f g
;??g h
ViewBag@@ 
.@@ 
fechaInicio@@ 
=@@  !
fechas@@" (
[@@( )
$num@@) *
]@@* +
.@@+ ,
AddDays@@, 3
(@@3 4
-@@4 5
$num@@5 6
)@@6 7
;@@7 8
ViewBagAA 
.AA 
fechaFinAA 
=AA 
fechasAA %
[AA% &
$numAA& '
]AA' (
;AA( )
returnBB 
ViewBB 
(BB 
)BB 
;BB 
}CC 	
[EE 	
	AuthorizeEE	 
(EE 
RolesEE 
=EE 
$strEE D
)EED E
]EEE F
publicFF 
ActionResultFF '
ReporteInventarioValorizadoFF 7
(FF7 8
)FF8 9
{GG 	
ViewBagHH 
.HH 
fechaActualHH 
=HH  
DateTimeUtilHH! -
.HH- .
FechaActualHH. 9
(HH9 :
)HH: ;
;HH; <
returnJJ 
ViewJJ 
(JJ 
)JJ 
;JJ 
}KK 	
publicPP 

JsonResultPP '
ObtenerInventarioValorizadoPP 5
(PP5 6
intPP6 9
	idAlmacenPP: C
,PPC D
intPPE H
[PPH I
]PPI J
idsConceptosBasicosPPK ^
,PP^ _
intPP` c
[PPc d
]PPd e(
idsValoresDeCaracteristicas	PPf �
,
PP� �
bool
PP� �
conLote
PP� �
)
PP� �
{QQ 	
tryRR 
{SS 
ListTT 
<TT 
stringTT 
>TT 
cadenaTT #
=TT$ %
newTT& )
ListTT* .
<TT. /
stringTT/ 5
>TT5 6
(TT6 7
)TT7 8
;TT8 9
ListUU 
<UU 
Detalle_maestroUU $
>UU$ %

resultadosUU& 0
=UU1 2
conceptoLogicaUU3 A
.UUA B"
ObtenerCaracteristicasUUB X
(UUX Y
)UUY Z
;UUZ [
ListVV 
<VV "
ComboGenericoViewModelVV +
>VV+ ,
caracteristicasVV- <
=VV= >"
ComboGenericoViewModelVV? U
.VVU V
ConvertVVV ]
(VV] ^

resultadosVV^ h
)VVh i
;VVi j
ListWW 
<WW )
InventarioValorizadoViewModelWW 2
>WW2 3
inventarioViewModelWW4 G
;WWG H
varXX #
idCentroAtencionPreciosXX +
=XX, -
ProfileDataXX. 9
(XX9 :
)XX: ;
.XX; <.
"IdCentroAtencionQueTieneLosPreciosXX< ^
;XX^ _
ifYY 
(YY '
idsValoresDeCaracteristicasYY /
==YY0 2
nullYY3 7
)YY7 8
{ZZ '
idsValoresDeCaracteristicas[[ /
=[[0 1
new[[2 5
int[[6 9
[[[9 :
][[: ;
{[[< =
}[[> ?
;[[? @
}\\ 
if^^ 
(^^ 
idsConceptosBasicos^^ '
==^^( *
null^^+ /
)^^/ 0
{__ 
idsConceptosBasicos`` '
=``( )
new``* -
int``. 1
[``1 2
]``2 3
{``4 5
}``6 7
;``7 8
}aa 
inventarioViewModelcc #
=cc$ %)
InventarioValorizadoViewModelcc& C
.ccC D
ConvertccD K
(ccK L#
inventarioActual_LogicaccL c
.ccc d.
!ObtenerInventarioValorizadoActual	ccd �
(
cc� �
	idAlmacen
cc� �
,
cc� �%
idCentroAtencionPrecios
cc� �
,
cc� �!
idsConceptosBasicos
cc� �
,
cc� �)
idsValoresDeCaracteristicas
cc� �
)
cc� �
,
cc� �
caracteristicas
cc� �
,
cc� �
conLote
cc� �
)
cc� �
;
cc� �
decimalee 
?ee 

=ee' (
nullee) -
;ee- .
decimalff 
?ff 
costoUnitarioTotalff +
=ff, -
nullff. 2
;ff2 3
decimalgg 
?gg 
precioVentaTotalgg )
=gg* +
nullgg, 0
;gg0 1
decimalhh 

costoTotalhh "
=hh# $
inventarioViewModelhh% 8
.hh8 9
Sumhh9 <
(hh< =
ivmhh= @
=>hhA C
ivmhhD G
.hhG H

CostoTotalhhH R
)hhR S
;hhS T
decimalii 
importeTotalii $
=ii% &
inventarioViewModelii' :
.ii: ;
Sumii; >
(ii> ?
ivmii? B
=>iiC E
ivmiiF I
.iiI J
ImporteTotaliiJ V
)iiV W
;iiW X
decimaljj 

=jj& '
inventarioViewModeljj( ;
.jj; <
Sumjj< ?
(jj? @
ivmjj@ C
=>jjD F
ivmjjG J
.jjJ K
UtilidadjjK S
)jjS T
;jjT U
varkk 
totaleskk 
=kk 
newkk !)
InventarioValorizadoViewModelkk" ?
(kk? @
$strkk@ I
,kkI J

,kkX Y
$strkkZ \
,kk\ ]
costoUnitarioTotalkk^ p
,kkp q

costoTotalkkr |
,kk| }
precioVentaTotal	kk~ �
,
kk� �
importeTotal
kk� �
,
kk� �

kk� �
,
kk� �
new
kk� �
List
kk� �
<
kk� �3
%Valor_caracteristica_concepto_negocio
kk� �
>
kk� �
(
kk� �
)
kk� �
,
kk� �
caracteristicas
kk� �
)
kk� �
;
kk� �
cadenamm 
=mm 0
$ConvertInventarioValorizadoViewModelmm =
(mm= >
inventarioViewModelmm> Q
)mmQ R
;mmR S
returnpp 
Jsonpp 
(pp 
newpp 
{pp  !
cadenapp" (
,pp( )
totalespp* 1
}pp2 3
)pp3 4
;pp4 5
}qq 
catchrr 
(rr 
	Exceptionrr 
err 
)rr 
{ss 
throwtt 
newtt 
ControllerExceptiontt -
(tt- .
$strtt. V
,ttV W
ettX Y
)ttY Z
;ttZ [
}uu 
}vv 	
publicxx 
Listxx 
<xx 
Stringxx 
>xx 0
$ConvertInventarioValorizadoViewModelxx @
(xx@ A
ListxxA E
<xxE F)
InventarioValorizadoViewModelxxF c
>xxc d
inventarioViewModelxxe x
)xxx y
{yy 	
varzz 
listDynamiczz 
=zz 
newzz !
Listzz" &
<zz& '
dynamiczz' .
>zz. /
(zz/ 0
)zz0 1
;zz1 2
List{{ 
<{{ 
JObject{{ 
>{{ 
jobject{{ !
={{" #
new{{$ '
List{{( ,
<{{, -
JObject{{- 4
>{{4 5
({{5 6
){{6 7
;{{7 8
List|| 
<|| 
string|| 
>|| 
cadena|| 
=||  !
new||" %
List||& *
<||* +
string||+ 1
>||1 2
(||2 3
)||3 4
;||4 5
foreach 
( 
var 
item 
in  
inventarioViewModel! 4
)4 5
{
�� 
dynamic
�� 
itemDynamic
�� #
=
��$ %
new
��& )
System
��* 0
.
��0 1
Dynamic
��1 8
.
��8 9

��9 F
(
��F G
)
��G H
;
��H I
itemDynamic
�� 
.
�� .
 _______Nombre_De_Concepto_______
�� <
=
��= >
item
��? C
.
��C D
Producto
��D L
;
��L M
foreach
�� 
(
�� 
var
�� 
	itemValor
�� &
in
��' )
item
��* .
.
��. /$
ValoresCaracteristicas
��/ E
)
��E F
{
�� 
(
�� 
(
�� 
IDictionary
�� !
<
��! "
string
��" (
,
��( )
object
��* 0
>
��0 1
)
��1 2
itemDynamic
��2 =
)
��= >
.
��> ?
Add
��? B
(
��B C
	itemValor
��C L
.
��L M"
NombreCaracteristica
��M a
,
��a b
	itemValor
��c l
.
��l m
NombreValor
��m x
)
��x y
;
��y z
}
�� 
itemDynamic
�� 
.
�� 
Lote
��  
=
��! "
item
��# '
.
��' (
Lote
��( ,
;
��, -
itemDynamic
�� 
.
�� 
Cant
��  
=
��! "
item
��# '
.
��' (
Cantidad
��( 0
!=
��1 3
null
��4 8
?
��9 :
(
��; <
(
��< =
decimal
��= D
)
��D E
item
��E I
.
��I J
Cantidad
��J R
)
��R S
.
��S T
ToString
��T \
(
��\ ]
$str
��] `
+
��a b 
AplicacionSettings
��c u
.
��u v
Default
��v }
.
��} ~(
NumeroDecimalesEnCantidad��~ �
)��� �
:��� �
null��� �
;��� �
itemDynamic
�� 
.
�� 
CU
�� 
=
��  
item
��! %
.
��% &

��& 3
!=
��4 6
null
��7 ;
?
��< =
(
��> ?
(
��? @
decimal
��@ G
)
��G H
item
��H L
.
��L M

��M Z
)
��Z [
.
��[ \
ToString
��\ d
(
��d e
$str
��e h
+
��i j 
AplicacionSettings
��k }
.
��} ~
Default��~ �
.��� �'
NumeroDecimalesEnPrecio��� �
)��� �
:��� �
null��� �
;��� �
itemDynamic
�� 
.
�� 
Costo
�� !
=
��" #
item
��$ (
.
��( )

CostoTotal
��) 3
.
��3 4
ToString
��4 <
(
��< =
$str
��= A
)
��A B
;
��B C
itemDynamic
�� 
.
�� 
PU
�� 
=
��  
item
��! %
.
��% &
PrecioVenta
��& 1
!=
��2 4
null
��5 9
?
��: ;
(
��< =
(
��= >
decimal
��> E
)
��E F
item
��F J
.
��J K
PrecioVenta
��K V
)
��V W
.
��W X
ToString
��X `
(
��` a
$str
��a d
+
��e f 
AplicacionSettings
��g y
.
��y z
Default��z �
.��� �'
NumeroDecimalesEnPrecio��� �
)��� �
:��� �
null��� �
;��� �
itemDynamic
�� 
.
�� 
Val_Vta
�� #
=
��$ %
item
��& *
.
��* +
ImporteTotal
��+ 7
.
��7 8
ToString
��8 @
(
��@ A
$str
��A E
)
��E F
;
��F G
itemDynamic
�� 
.
�� 
Utilidad
�� $
=
��% &
item
��' +
.
��+ ,
Utilidad
��, 4
.
��4 5
ToString
��5 =
(
��= >
$str
��> B
)
��B C
;
��C D
listDynamic
�� 
.
�� 
Add
�� 
(
��  
itemDynamic
��  +
)
��+ ,
;
��, -
string
�� 
json
�� 
=
�� 
JsonConvert
�� )
.
��) *
SerializeObject
��* 9
(
��9 :
itemDynamic
��: E
)
��E F
;
��F G
cadena
�� 
.
�� 
Add
�� 
(
�� 
json
�� 
)
��  
;
��  !
jobject
�� 
.
�� 
Add
�� 
(
�� 
JObject
�� #
.
��# $
Parse
��$ )
(
��) *
json
��* .
)
��. /
)
��/ 0
;
��0 1
}
�� 
return
�� 
cadena
�� 
;
�� 
}
�� 	
public
�� 
ActionResult
�� .
 ObtenerReporteDeSalidasDeAlcohol
�� <
(
��< =
string
��= C
fechaInicio
��D O
,
��O P
string
��Q W
fechaFin
��X `
,
��` a
[
��b c
System
��c i
.
��i j
Web
��j m
.
��m n
Http
��n r
.
��r s
FromUri
��s z
]
��z {
int
��| 
[�� �
]��� �!
idsEntidadInterna��� �
)��� �
{
�� 	
try
�� 
{
�� 
DateTime
�� 

fechaDesde
�� #
=
��$ %
DateTimeUtil
��& 2
.
��2 39
+ObtenerFechaDesdeConPrecisionDeMilisegundos
��3 ^
(
��^ _
fechaInicio
��_ j
)
��j k
;
��k l
DateTime
�� 

fechaHasta
�� #
=
��$ %
DateTimeUtil
��& 2
.
��2 39
+ObtenerFechaHastaConPrecisionDeMilisegundos
��3 ^
(
��^ _
fechaFin
��_ g
)
��g h
;
��h i
List
�� 
<
�� %
Reporte_Concepto_Basico
�� ,
>
��, -'
detalladoConceptosBasicos
��. G
=
��H I
operacionLogica
��J Y
.
��Y Z.
 ObtenerReporteDeSalidasDeAlcohol
��Z z
(
��z {

fechaDesde��{ �
,��� �

fechaHasta��� �
,��� �!
idsEntidadInterna��� �
)��� �
;��� �
List
�� 
<
�� 5
'ReporteDeConceptoBasicoAlcoholViewModel
�� <
>
��< =>
0reporteDetalladoDeConceptoBasicoAlcoholViewModel
��> n
=
��o p6
'ReporteDeConceptoBasicoAlcoholViewModel��q �
.��� �0
 ConvertirConceptosBasicosALitros��� �
(��� �)
detalladoConceptosBasicos��� �
)��� �
;��� �
string
�� %
_nombresCentrosAtencion
�� .
=
��/ 0
$str
��1 3
;
��3 4
foreach
�� 
(
�� 
var
�� 
item
�� !
in
��" $
idsEntidadInterna
��% 6
)
��6 7
{
�� 
string
�� #
_nombreCentroAtencion
�� 0
=
��1 2%
centroDeAtencion_Logica
��3 J
.
��J K-
ObtenerNombreDeCentroDeAtencion
��K j
(
��j k
item
��k o
)
��o p
;
��p q%
_nombresCentrosAtencion
�� +
+=
��, .
$str
��/ 5
+
��6 7#
_nombreCentroAtencion
��8 M
;
��M N
}
�� 
ReportParameter
�� $
parametroNombreEmpresa
��  6
=
��7 8
new
��9 <
ReportParameter
��= L
(
��L M
$str
��M \
,
��\ ]
ObtenerSede
��^ i
(
��i j
)
��j k
.
��k l
Nombre
��l r
)
��r s
;
��s t
ReportParameter
�� +
parametroNombreCentroAtencion
��  =
=
��> ?
new
��@ C
ReportParameter
��D S
(
��S T
$str
��T j
,
��j k&
_nombresCentrosAtencion��l �
)��� �
;��� �
ReportParameter
�� !
parametroFechaDesde
��  3
=
��4 5
new
��6 9
ReportParameter
��: I
(
��I J
$str
��J V
,
��V W

fechaDesde
��X b
.
��b c
ToString
��c k
(
��k l
)
��l m
)
��m n
;
��n o
ReportParameter
�� !
parametroFechaHasta
��  3
=
��4 5
new
��6 9
ReportParameter
��: I
(
��I J
$str
��J V
,
��V W

fechaHasta
��X b
.
��b c
ToString
��c k
(
��k l
)
��l m
)
��m n
;
��n o
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
string
�� 

logoString
�� !
=
��" #
Convert
��$ +
.
��+ ,
ToBase64String
��, :
(
��: ;
sede
��; ?
.
��? @
Logo
��@ D
,
��D E
$num
��F G
,
��G H
sede
��I M
.
��M N
Logo
��N R
.
��R S
Length
��S Y
)
��Y Z
;
��Z [
ReportParameter
�� 
logoSede
��  (
=
��) *
new
��+ .
ReportParameter
��/ >
(
��> ?
$str
��? I
,
��I J

logoString
��K U
)
��U V
;
��V W
ReportParameter
�� 
empleadoSede
��  ,
=
��- .
new
��/ 2
ReportParameter
��3 B
(
��B C
$str
��C L
,
��L M
ProfileData
��N Y
(
��Y Z
)
��Z [
.
��[ \
Empleado
��\ d
.
��d e
NombresYApellidos
��e v
)
��v w
;
��w x
DateTime
�� 
fechaActual
�� $
=
��% &
DateTimeUtil
��& 2
.
��2 3
FechaActual
��3 >
(
��> ?
)
��? @
;
��@ A
ReportParameter
��  
fechaActualSistema
��  2
=
��3 4
new
��5 8
ReportParameter
��9 H
(
��H I
$str
��I ]
,
��] ^
fechaActual
��_ j
.
��j k
ToString
��k s
(
��s t
)
��t u
)
��u v
;
��v w
var
�� 
	rptviewer
�� 
=
�� 
new
��  #
ReportViewer
��$ 0
(
��0 1
)
��1 2
;
��2 3
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

ReportPath
��& 0
=
��1 2
Request
��3 :
.
��: ;
MapPath
��; B
(
��B C
Request
��C J
.
��J K
ApplicationPath
��K Z
)
��Z [
+
��\ ]
$str��^ �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

��& 3
(
��3 4
new
��4 7
ReportParameter
��8 G
[
��G H
]
��H I
{
�� $
parametroNombreEmpresa
�� *
,
��+ ,!
parametroFechaDesde
��, ?
,
��? @!
parametroFechaHasta
��A T
,
��T U+
parametroNombreCentroAtencion
��V s
,
��s t
logoSede
��u }
,
��} ~
empleadoSede�� �
,��� �"
fechaActualSistema��� �
}
�� 
)
�� 
;
�� 
ReportDataSource
��  4
&rptdatasourceDetalladoConceptosBasicos
��! G
=
��H I
new
��J M
ReportDataSource
��N ^
(
��^ _
$str
��_ q
,
��q r?
0reporteDetalladoDeConceptoBasicoAlcoholViewModel��s �
)��� �
;��� �
	rptviewer
�� 
.
�� 
ProcessingMode
�� (
=
��) *
ProcessingMode
��+ 9
.
��9 :
Local
��: ?
;
��? @
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 64
&rptdatasourceDetalladoConceptosBasicos
��6 \
)
��\ ]
;
��] ^
	rptviewer
�� 
.
�� !
SizeToReportContent
�� -
=
��. /
true
��0 4
;
��4 5
	rptviewer
�� 
.
�� 
Width
�� 
=
��  !
Unit
��" &
.
��& '

Percentage
��' 1
(
��1 2
$num
��2 5
)
��5 6
;
��6 7
	rptviewer
�� 
.
�� 
Height
��  
=
��! "
Unit
��# '
.
��' (

Percentage
��( 2
(
��2 3
$num
��3 6
)
��6 7
;
��7 8
ViewBag
�� 
.
�� 
ReportViewer
�� $
=
��% &
	rptviewer
��' 0
;
��0 1
return
�� 
View
�� 
(
�� 
$str
�� 1
)
��1 2
;
��2 3
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
�� !
ControllerException
�� -
(
��- .
$str
��. \
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
�� 	
}
�� 
}�� ��
}D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\AlmacenReportesController_InventariosActuales.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public

partial
class
AlmacenReportesController
{ 
public 
ActionResult *
InventariosActuales_Inventario :
(: ;
[; <
System< B
.B C
WebC F
.F G
HttpG K
.K L
FromUriL S
]S T
stringU [
[[ \
]\ ]
	almacenes^ g
,g h
[i j
Systemj p
.p q
Webq t
.t u
Httpu y
.y z
FromUri	z �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
)
� �
{ 	
var 
	rptviewer 
= 1
%GenerarInventariosActuales_Inventario A
(A B
	almacenesB K
,K L
idsFamiliasM X
,X Y
nombresFamiliasZ i
,i j
todasLasFamiliask {
,{ |
true	} �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public 
ActionResult 2
&InventariosActuales_InventarioSemaforo B
(B C
[C D
SystemD J
.J K
WebK N
.N O
HttpO S
.S T
FromUriT [
][ \
string] c
[c d
]d e
	almacenesf o
,o p
[q r
Systemr x
.x y
Weby |
.| }
Http	} �
.
� �
FromUri
� �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
,
� �
bool
� �

estadoBajo
� �
,
� �
bool
� �
estadoNormal
� �
,
� �
bool
� �

estadoAlto
� �
)
� �
{ 	
var 
	rptviewer 
= 9
-GenerarInventariosActuales_InventarioSemaforo I
(I J
	almacenesJ S
,S T
idsFamiliasU `
,` a
nombresFamiliasb q
,q r
todasLasFamilias	s �
,
� �

estadoBajo
� �
,
� �
estadoNormal
� �
,
� �

estadoAlto
� �
,
� �
true
� �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public 
ActionResult 4
(InventariosActuales_InventarioValorizado D
(D E
[E F
SystemF L
.L M
WebM P
.P Q
HttpQ U
.U V
FromUriV ]
]] ^
string_ e
[e f
]f g
	almacenesh q
,q r
[s t
Systemt z
.z {
Web{ ~
.~ 
Http	 �
.
� �
FromUri
� �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
)
� �
{ 	
var 
	rptviewer 
= ;
/GenerarInventariosActuales_InventarioValorizado K
(K L
	almacenesL U
,U V
idsFamiliasW b
,b c
nombresFamiliasd s
,s t
todasLasFamilias	u �
,
� �
true
� �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return   
View   
(   
$str   -
)  - .
;  . /
}!! 	
public"" 
ReportViewer"" 1
%GenerarInventariosActuales_Inventario"" A
(""A B
string""B H
[""H I
]""I J
almacenesString""K Z
,""Z [
int""\ _
[""_ `
]""` a
idsFamilias""b m
,""m n
string""o u
nombresFamilias	""v �
,
""� �
bool
""� �
todasLasFamilias
""� �
,
""� �
bool
""� �
fromRequest
""� �
)
""� �
{## 	
var$$ 
	almacenes$$ 
=$$ 
ObtenerItems$$ (
($$( )
almacenesString$$) 8
)$$8 9
;$$9 :
List%% 
<%% 
InventarioFisico%% !
>%%! "

inventario%%# -
=%%. /#
inventarioActual_Logica%%0 G
.%%G H&
InventariosFisicosActuales%%H b
(%%b c
	almacenes%%c l
,%%l m
todasLasFamilias%%n ~
,%%~ 
idsFamilias
%%� �
)
%%� �
.
%%� �
OrderBy
%%� �
(
%%� �
i
%%� �
=>
%%� �
i
%%� �
.
%%� �
Concepto
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
%%� �
var&& 
parametrosBasicos&& !
=&&" #$
ObtenerParametrosBasicos&&$ <
(&&< =
)&&= >
;&&> ?
ReportParameter'' 
parametroAlmacenes'' .
=''/ 0
new''1 4
ReportParameter''5 D
(''D E
$str''E P
,''P Q
String''R X
.''X Y
Join''Y ]
(''] ^
$str''^ a
,''a b
	almacenes''b k
.''k l
Select''l r
(''r s
a''s t
=>''t v
a''w x
.''x y
Nombre''y 
)	'' �
)
''� �
)
''� �
;
''� �
ReportParameter(( 
parametroFamilias(( -
=((. /
new((0 3
ReportParameter((4 C
(((C D
$str((D N
,((N O
nombresFamilias((P _
??((` b
$str((c j
)((j k
;((k l
var)) 
	rptviewer)) 
=)) 
new)) 
ReportViewer))  ,
()), -
)))- .
{))/ 0
ProcessingMode))1 ?
=))@ A
ProcessingMode))B P
.))P Q
Local))Q V
,))V W
SizeToReportContent))X k
=))l m
true))n r
,))r s
Width))t y
=))z {
Unit	))| �
.
))� �

Percentage
))� �
(
))� �
$num
))� �
)
))� �
,
))� �
Height
))� �
=
))� �
Unit
))� �
.
))� �

Percentage
))� �
(
))� �
$num
))� �
)
))� �
}
))� �
;
))� �
string** 
path** 
=** 
$str** ^
;**^ _
	rptviewer++ 
.++ 
LocalReport++ !
.++! "

ReportPath++" ,
=++- .
fromRequest++/ :
?++; <
Request++= D
.++D E
MapPath++E L
(++L M
Request++M T
.++T U
ApplicationPath++U d
)++d e
+++f g
path++h l
:++m n
HostingEnvironment	++o �
.
++� �
MapPath
++� �
(
++� �
path
++� �
)
++� �
;
++� �
	rptviewer,, 
.,, 
LocalReport,, !
.,,! "

(,,/ 0
new,,0 3
ReportParameter,,4 C
[,,C D
],,D E
{,,F G
parametrosBasicos-- !
.--! "

NombreSede--" ,
,--, -
parametrosBasicos.. !
...! "
FechaActualSistema.." 4
,..4 5
parametrosBasicos// !
.//! "
LogoSede//" *
,//* +
parametrosBasicos00 !
.00! "
Usuario00" )
,00) *
parametroFamilias11 !
,11! "
parametroAlmacenes22 "
}33 
)33
;33 
ReportDataSource44 
rptdatasourceStock44 /
=440 1
new442 5
ReportDataSource446 F
(44F G
$str44G Z
,44Z [

inventario44\ f
)44f g
;44g h
	rptviewer55 
.55 
LocalReport55 !
.55! "
DataSources55" -
.55- .
Add55. 1
(551 2
rptdatasourceStock552 D
)55D E
;55E F
return66 
	rptviewer66 
;66 
}77 	
public88 
ReportViewer88 9
-GenerarInventariosActuales_InventarioSemaforo88 I
(88I J
string88J P
[88P Q
]88Q R
almacenesString88S b
,88b c
[88d e
System88e k
.88k l
Web88l o
.88o p
Http88p t
.88t u
FromUri88u |
]88| }
int	88~ �
[
88� �
]
88� �
idsFamilias
88� �
,
88� �
string
88� �
nombresFamilias
88� �
,
88� �
bool
88� �
todasLasFamilias
88� �
,
88� �
bool
88� �

estadoBajo
88� �
,
88� �
bool
88� �
estadoNormal
88� �
,
88� �
bool
88� �

estadoAlto
88� �
,
88� �
bool
88� �
fromRequest
88� �
)
88� �
{99 	
try:: 
{;; 
var<< 
	almacenes<< 
=<< 
ObtenerItems<<  ,
(<<, -
almacenesString<<- <
)<<< =
;<<= >
List== 
<== 
InventarioSemaforo== '
>==' (

inventario==) 3
===4 5#
inventarioActual_Logica==6 M
.==M N'
InventariosSemaforoActuales==N i
(==i j
	almacenes==j s
,==s t
todasLasFamilias	==u �
,
==� �
idsFamilias
==� �
,
==� �

estadoBajo
==� �
,
==� �
estadoNormal
==� �
,
==� �

estadoAlto
==� �
)
==� �
;
==� �
var>> 
parametrosBasicos>> %
=>>& '$
ObtenerParametrosBasicos>>( @
(>>@ A
)>>A B
;>>B C
ReportParameter?? 
parametroAlmacenes??  2
=??3 4
new??5 8
ReportParameter??9 H
(??H I
$str??I T
,??T U
String??V \
.??\ ]
Join??] a
(??a b
$str??b e
,??e f
	almacenes??g p
.??p q
Select??q w
(??w x
a??x y
=>??z |
a??} ~
.??~ 
Nombre	?? �
)
??� �
)
??� �
)
??� �
;
??� �
ReportParameter@@ 
parametroFamilias@@  1
=@@2 3
new@@4 7
ReportParameter@@8 G
(@@G H
$str@@H R
,@@R S
nombresFamilias@@T c
??@@d f
$str@@g n
)@@n o
;@@o p
varAA 
	rptviewerAA 
=AA 
newAA  #
ReportViewerAA$ 0
(AA0 1
)AA1 2
{AA3 4
ProcessingModeAA5 C
=AAD E
ProcessingModeAAF T
.AAT U
LocalAAU Z
,AAZ [
SizeToReportContentAA\ o
=AAp q
trueAAr v
,AAv w
WidthAAx }
=AA~ 
Unit
AA� �
.
AA� �

Percentage
AA� �
(
AA� �
$num
AA� �
)
AA� �
,
AA� �
Height
AA� �
=
AA� �
Unit
AA� �
.
AA� �

Percentage
AA� �
(
AA� �
$num
AA� �
)
AA� �
}
AA� �
;
AA� �
stringBB 
pathBB 
=BB 
$strBB j
;BBj k
	rptviewerCC 
.CC 
LocalReportCC %
.CC% &

ReportPathCC& 0
=CC1 2
fromRequestCC3 >
?CC? @
RequestCCA H
.CCH I
MapPathCCI P
(CCP Q
RequestCCQ X
.CCX Y
ApplicationPathCCY h
)CCh i
+CCj k
pathCCl p
:CCq r
HostingEnvironment	CCs �
.
CC� �
MapPath
CC� �
(
CC� �
path
CC� �
)
CC� �
;
CC� �
	rptviewerDD 
.DD 
LocalReportDD %
.DD% &

(DD3 4
newDD4 7
ReportParameterDD8 G
[DDG H
]DDH I
{DDJ K
parametrosBasicosEE !
.EE! "

NombreSedeEE" ,
,EE, -
parametrosBasicosFF !
.FF! "
FechaActualSistemaFF" 4
,FF4 5
parametrosBasicosGG !
.GG! "
LogoSedeGG" *
,GG* +
parametrosBasicosHH !
.HH! "
UsuarioHH" )
,HH) *
parametroFamiliasII !
,II! "
parametroAlmacenesJJ "
}KK 
)KK
;KK 
ReportDataSourceLL  (
rptdatasourceStockPorFamiliaLL! =
=LL> ?
newLL@ C
ReportDataSourceLLD T
(LLT U
$strLLU p
,LLp q

inventarioLLr |
)LL| }
;LL} ~
	rptviewerMM 
.MM 
LocalReportMM %
.MM% &
DataSourcesMM& 1
.MM1 2
AddMM2 5
(MM5 6(
rptdatasourceStockPorFamiliaMM6 R
)MMR S
;MMS T
returnNN 
	rptviewerNN  
;NN  !
}OO 
catchPP 
(PP 
	ExceptionPP 
ePP 
)PP 
{QQ 
throwRR 
newRR 
ControllerExceptionRR -
(RR- .
$strRR. _
,RR_ `
eRRa b
)RRb c
;RRc d
}SS 
}TT 	
publicVV 
ReportViewerVV ;
/GenerarInventariosActuales_InventarioValorizadoVV K
(VVK L
stringVVL R
[VVR S
]VVS T
almacenesStringVVU d
,VVd e
[VVf g
SystemVVg m
.VVm n
WebVVn q
.VVq r
HttpVVr v
.VVv w
FromUriVVw ~
]VV~ 
int
VV� �
[
VV� �
]
VV� �
idsFamilias
VV� �
,
VV� �
string
VV� �
nombresFamilias
VV� �
,
VV� �
bool
VV� �
todasLasFamilias
VV� �
,
VV� �
bool
VV� �
fromRequest
VV� �
)
VV� �
{WW 	
varXX 
	almacenesXX 
=XX 
ObtenerItemsXX (
(XX( )
almacenesStringXX) 8
)XX8 9
;XX9 :
ListYY 
<YY  
InventarioValorizadoYY %
>YY% &

inventarioYY' 1
=YY2 3#
inventarioActual_LogicaYY4 K
.YYK L*
InventariosValorizadosActualesYYL j
(YYj k
	almacenesYYk t
,YYt u
todasLasFamilias	YYv �
,
YY� �
idsFamilias
YY� �
)
YY� �
.
YY� �
OrderBy
YY� �
(
YY� �
i
YY� �
=>
YY� �
i
YY� �
.
YY� �
Concepto
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
YY� �
varZZ 
parametrosBasicosZZ !
=ZZ" #$
ObtenerParametrosBasicosZZ$ <
(ZZ< =
)ZZ= >
;ZZ> ?
ReportParameter[[ 
parametroAlmacenes[[ .
=[[/ 0
new[[1 4
ReportParameter[[5 D
([[D E
$str[[E P
,[[P Q
String[[R X
.[[X Y
Join[[Y ]
([[] ^
$str[[^ a
,[[a b
	almacenes[[c l
.[[l m
Select[[m s
([[s t
a[[t u
=>[[v x
a[[y z
.[[z {
Nombre	[[{ �
)
[[� �
)
[[� �
)
[[� �
;
[[� �
ReportParameter\\ 
parametroFamilias\\ -
=\\. /
new\\0 3
ReportParameter\\4 C
(\\C D
$str\\D N
,\\N O
nombresFamilias\\P _
??\\` b
$str\\c j
)\\j k
;\\k l
var]] 
	rptviewer]] 
=]] 
new]] 
ReportViewer]]  ,
(]], -
)]]- .
{]]/ 0
ProcessingMode]]1 ?
=]]@ A
ProcessingMode]]B P
.]]P Q
Local]]Q V
,]]V W
SizeToReportContent]]X k
=]]l m
true]]n r
,]]r s
Width]]t y
=]]z {
Unit	]]| �
.
]]� �

Percentage
]]� �
(
]]� �
$num
]]� �
)
]]� �
,
]]� �
Height
]]� �
=
]]� �
Unit
]]� �
.
]]� �

Percentage
]]� �
(
]]� �
$num
]]� �
)
]]� �
}
]]� �
;
]]� �
string^^ 
path^^ 
=^^ 
$str^^ h
;^^h i
	rptviewer__ 
.__ 
LocalReport__ !
.__! "

ReportPath__" ,
=__- .
fromRequest__/ :
?__; <
Request__= D
.__D E
MapPath__E L
(__L M
Request__M T
.__T U
ApplicationPath__U d
)__d e
+__f g
path__h l
:__m n
HostingEnvironment	__o �
.
__� �
MapPath
__� �
(
__� �
path
__� �
)
__� �
;
__� �
	rptviewer`` 
.`` 
LocalReport`` !
.``! "

(``/ 0
new``0 3
ReportParameter``4 C
[``C D
]``D E
{``F G
parametrosBasicosaa !
.aa! "

NombreSedeaa" ,
,aa, -
parametrosBasicosbb !
.bb! "
FechaActualSistemabb" 4
,bb4 5
parametrosBasicoscc !
.cc! "
LogoSedecc" *
,cc* +
parametrosBasicosdd !
.dd! "
Usuariodd" )
,dd) *
parametroFamiliasee !
,ee! "
parametroAlmacenesff "
}gg 
)gg
;gg 
ReportDataSourcehh 
rptdatasourceStockhh /
=hh0 1
newhh2 5
ReportDataSourcehh6 F
(hhF G
$strhhG d
,hhd e

inventariohhf p
)hhp q
;hhq r
	rptviewerii 
.ii 
LocalReportii !
.ii! "
DataSourcesii" -
.ii- .
Addii. 1
(ii1 2
rptdatasourceStockii2 D
)iiD E
;iiE F
returnjj 
	rptviewerjj 
;jj 
}kk 	
}mm 
}nn �
}D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\AlmacenReportesController_InventariosPorFecha.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public

partial
class
AlmacenReportesController
{ 
public 
ActionResult *
InventariosPorFecha_Inventario :
(: ;
int; >
	idAlmacen? H
,H I
stringJ P

,^ _
DateTime` h

fechaHastai s
,s t
[u v
Systemv |
.| }
Web	} �
.
� �
Http
� �
.
� �
FromUri
� �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
)
� �
{ 	
var 
	rptviewer 
= 1
%GenerarInventariosPorFecha_Inventario A
(A B
	idAlmacenB K
,K L

,Z [

fechaHasta\ f
,f g
idsFamiliash s
,s t
nombresFamilias	u �
,
� �
todasLasFamilias
� �
,
� �
true
� �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public 
ActionResult 2
&InventariosPorFecha_InventarioSemaforo B
(B C
intC F
	idAlmacenG P
,P Q
stringR X

,f g
DateTimeh p

fechaHastaq {
,{ |
[} ~
System	~ �
.
� �
Web
� �
.
� �
Http
� �
.
� �
FromUri
� �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
,
� �
bool
� �

estadoBajo
� �
,
� �
bool
� �
estadoNormal
� �
,
� �
bool
� �

estadoAlto
� �
)
� �
{ 	
var 
	rptviewer 
= 9
-GenerarInventariosPorFecha_InventarioSemaforo I
(I J
	idAlmacenJ S
,S T

,b c

fechaHastad n
,n o
idsFamiliasp {
,{ |
nombresFamilias	} �
,
� �
todasLasFamilias
� �
,
� �

estadoBajo
� �
,
� �
estadoNormal
� �
,
� �

estadoAlto
� �
,
� �
true
� �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public 
ActionResult 4
(InventariosPorFecha_InventarioValorizado D
(D E
intE H
	idAlmacenI R
,R S
stringT Z

,h i
DateTimej r

fechaHastas }
,} ~
[	 �
System
� �
.
� �
Web
� �
.
� �
Http
� �
.
� �
FromUri
� �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
)
� �
{   	
var"" 
	rptviewer"" 
="" ;
/GenerarInventariosPorFecha_InventarioValorizado"" K
(""K L
	idAlmacen""L U
,""U V

,""d e

fechaHasta""f p
,""p q
idsFamilias""r }
,""} ~
nombresFamilias	"" �
,
""� �
todasLasFamilias
""� �
,
""� �
true
""� �
)
""� �
;
""� �
ViewBag## 
.## 
ReportViewer##  
=##! "
	rptviewer### ,
;##, -
return$$ 
View$$ 
($$ 
$str$$ -
)$$- .
;$$. /
}%% 	
public&& 
ReportViewer&& 1
%GenerarInventariosPorFecha_Inventario&& A
(&&A B
int&&B E
	idAlmacen&&F O
,&&O P
string&&Q W

,&&e f
DateTime&&g o

fechaHasta&&p z
,&&z {
[&&| }
System	&&} �
.
&&� �
Web
&&� �
.
&&� �
Http
&&� �
.
&&� �
FromUri
&&� �
]
&&� �
int
&&� �
[
&&� �
]
&&� �
idsFamilias
&&� �
,
&&� �
string
&&� �
nombresFamilias
&&� �
,
&&� �
bool
&&� �
todasLasFamilias
&&� �
,
&&� �
bool
&&� �
fromRequest
&&� �
)
&&� �
{'' 	
List(( 
<(( 
InventarioFisico(( !
>((! "

inventario((# -
=((. /"
almacenReportingLogica((0 F
.((F G,
 ObtenerInventarioFisicoHistorico((G g
(((g h
	idAlmacen((h q
,((q r
ProfileData((s ~
(((~ 
)	(( �
.
((� �
Empleado
((� �
.
((� �
Id
((� �
,
((� �

fechaHasta
((� �
,
((� �
todasLasFamilias
((� �
,
((� �
idsFamilias
((� �
)
((� �
.
((� �
OrderBy
((� �
(
((� �
i
((� �
=>
((� �
i
((� �
.
((� �
Concepto
((� �
)
((� �
.
((� �
ToList
((� �
(
((� �
)
((� �
;
((� �
var)) 
parametrosBasicos)) !
=))" #$
ObtenerParametrosBasicos))$ <
())< =
)))= >
;))> ?
ReportParameter** "
parametroNombreAlmacen** 2
=**3 4
new**5 8
ReportParameter**9 H
(**H I
$str**I X
,**X Y

)**g h
;**h i
ReportParameter++ 
parametroFamilias++ -
=++. /
new++0 3
ReportParameter++4 C
(++C D
$str++D N
,++N O
nombresFamilias++P _
??++` b
$str++c j
)++j k
;++k l
ReportParameter,, 
parametroFechaHasta,, /
=,,0 1
new,,2 5
ReportParameter,,6 E
(,,E F
$str,,F R
,,,R S

fechaHasta,,T ^
.,,^ _
ToString,,_ g
(,,g h
),,h i
),,i j
;,,j k
var-- 
	rptviewer-- 
=-- 
new-- 
ReportViewer--  ,
(--, -
)--- .
{--/ 0
ProcessingMode--1 ?
=--@ A
ProcessingMode--B P
.--P Q
Local--Q V
,--V W
SizeToReportContent--X k
=--l m
true--n r
,--r s
Width--t y
=--z {
Unit	--| �
.
--� �

Percentage
--� �
(
--� �
$num
--� �
)
--� �
,
--� �
Height
--� �
=
--� �
Unit
--� �
.
--� �

Percentage
--� �
(
--� �
$num
--� �
)
--� �
}
--� �
;
--� �
string.. 
path.. 
=.. 
$str.. ^
;..^ _
	rptviewer// 
.// 
LocalReport// !
.//! "

ReportPath//" ,
=//- .
fromRequest/// :
?//; <
Request//= D
.//D E
MapPath//E L
(//L M
Request//M T
.//T U
ApplicationPath//U d
)//d e
+//f g
path//h l
://m n
HostingEnvironment	//o �
.
//� �
MapPath
//� �
(
//� �
path
//� �
)
//� �
;
//� �
	rptviewer00 
.00 
LocalReport00 !
.00! "

(00/ 0
new000 3
ReportParameter004 C
[00C D
]00D E
{00F G
parametrosBasicos11 !
.11! "

NombreSede11" ,
,11, -
parametrosBasicos22 !
.22! "
FechaActualSistema22" 4
,224 5
parametrosBasicos33 !
.33! "
LogoSede33" *
,33* +
parametrosBasicos44 !
.44! "
Usuario44" )
,44) *
parametroFamilias55 !
,55! ""
parametroNombreAlmacen66 &
,66& '
parametroFechaHasta77 #
}88 
)88
;88 
ReportDataSource99 
rptdatasourceStock99 /
=990 1
new992 5
ReportDataSource996 F
(99F G
$str99G Z
,99Z [

inventario99\ f
)99f g
;99g h
	rptviewer:: 
.:: 
LocalReport:: !
.::! "
DataSources::" -
.::- .
Add::. 1
(::1 2
rptdatasourceStock::2 D
)::D E
;::E F
return;; 
	rptviewer;; 
;;; 
}<< 	
public== 
ReportViewer== 9
-GenerarInventariosPorFecha_InventarioSemaforo== I
(==I J
int==J M
	idAlmacen==N W
,==W X
string==Y _

,==m n
DateTime==o w

fechaHasta	==x �
,
==� �
[
==� �
System
==� �
.
==� �
Web
==� �
.
==� �
Http
==� �
.
==� �
FromUri
==� �
]
==� �
int
==� �
[
==� �
]
==� �
idsFamilias
==� �
,
==� �
string
==� �
nombresFamilias
==� �
,
==� �
bool
==� �
todasLasFamilias
==� �
,
==� �
bool
==� �

estadoBajo
==� �
,
==� �
bool
==� �
estadoNormal
==� �
,
==� �
bool
==� �

estadoAlto
==� �
,
==� �
bool
==� �
fromRequest
==� �
)
==� �
{>> 	
try?? 
{@@ 
ListAA 
<AA 
InventarioSemaforoAA '
>AA' (

inventarioAA) 3
=AA4 5"
almacenReportingLogicaAA6 L
.AAL M'
InventarioSemaforoHistoricoAAM h
(AAh i
	idAlmacenAAi r
,AAr s
ProfileDataAAt 
(	AA �
)
AA� �
.
AA� �
Empleado
AA� �
.
AA� �
Id
AA� �
,
AA� �

fechaHasta
AA� �
,
AA� �
todasLasFamilias
AA� �
,
AA� �
idsFamilias
AA� �
,
AA� �

estadoBajo
AA� �
,
AA� �
estadoNormal
AA� �
,
AA� �

estadoAlto
AA� �
)
AA� �
;
AA� �
varBB 
parametrosBasicosBB %
=BB& '$
ObtenerParametrosBasicosBB( @
(BB@ A
)BBA B
;BBB C
ReportParameterCC "
parametroNombreAlmacenCC  6
=CC7 8
newCC9 <
ReportParameterCC= L
(CCL M
$strCCM \
,CC\ ]

)CCk l
;CCl m
ReportParameterDD 
parametroFamiliasDD  1
=DD2 3
newDD4 7
ReportParameterDD8 G
(DDG H
$strDDH R
,DDR S
nombresFamiliasDDT c
??DDd f
$strDDg n
)DDn o
;DDo p
ReportParameterEE 
parametroFechaHastaEE  3
=EE4 5
newEE6 9
ReportParameterEE: I
(EEI J
$strEEJ V
,EEV W

fechaHastaEEX b
.EEb c
ToStringEEc k
(EEk l
)EEl m
)EEm n
;EEn o
varFF 
	rptviewerFF 
=FF 
newFF  #
ReportViewerFF$ 0
(FF0 1
)FF1 2
{FF3 4
ProcessingModeFF5 C
=FFD E
ProcessingModeFFF T
.FFT U
LocalFFU Z
,FFZ [
SizeToReportContentFF\ o
=FFp q
trueFFr v
,FFv w
WidthFFx }
=FF~ 
Unit
FF� �
.
FF� �

Percentage
FF� �
(
FF� �
$num
FF� �
)
FF� �
,
FF� �
Height
FF� �
=
FF� �
Unit
FF� �
.
FF� �

Percentage
FF� �
(
FF� �
$num
FF� �
)
FF� �
}
FF� �
;
FF� �
stringGG 
pathGG 
=GG 
$strGG j
;GGj k
	rptviewerHH 
.HH 
LocalReportHH %
.HH% &

ReportPathHH& 0
=HH1 2
fromRequestHH3 >
?HH? @
RequestHHA H
.HHH I
MapPathHHI P
(HHP Q
RequestHHQ X
.HHX Y
ApplicationPathHHY h
)HHh i
+HHj k
pathHHl p
:HHq r
HostingEnvironment	HHs �
.
HH� �
MapPath
HH� �
(
HH� �
path
HH� �
)
HH� �
;
HH� �
	rptviewerII 
.II 
LocalReportII %
.II% &

(II3 4
newII4 7
ReportParameterII8 G
[IIG H
]IIH I
{IIJ K
parametrosBasicosJJ !
.JJ! "

NombreSedeJJ" ,
,JJ, -
parametrosBasicosKK !
.KK! "
FechaActualSistemaKK" 4
,KK4 5
parametrosBasicosLL !
.LL! "
LogoSedeLL" *
,LL* +
parametrosBasicosMM !
.MM! "
UsuarioMM" )
,MM) *
parametroFamiliasNN !
,NN! ""
parametroNombreAlmacenOO &
,OO& '
parametroFechaHastaPP #
}QQ 
)QQ
;QQ 
ReportDataSourceRR  (
rptdatasourceStockPorFamiliaRR! =
=RR> ?
newRR@ C
ReportDataSourceRRD T
(RRT U
$strRRU p
,RRp q

inventarioRRr |
)RR| }
;RR} ~
	rptviewerSS 
.SS 
LocalReportSS %
.SS% &
DataSourcesSS& 1
.SS1 2
AddSS2 5
(SS5 6(
rptdatasourceStockPorFamiliaSS6 R
)SSR S
;SSS T
returnTT 
	rptviewerTT  
;TT  !
}UU 
catchVV 
(VV 
	ExceptionVV 
eVV 
)VV 
{WW 
throwXX 
newXX 
ControllerExceptionXX -
(XX- .
$strXX. _
,XX_ `
eXXa b
)XXb c
;XXc d
}YY 
}ZZ 	
public\\ 
ReportViewer\\ ;
/GenerarInventariosPorFecha_InventarioValorizado\\ K
(\\K L
int\\L O
	idAlmacen\\P Y
,\\Y Z
string\\[ a

,\\o p
DateTime\\q y

fechaHasta	\\z �
,
\\� �
[
\\� �
System
\\� �
.
\\� �
Web
\\� �
.
\\� �
Http
\\� �
.
\\� �
FromUri
\\� �
]
\\� �
int
\\� �
[
\\� �
]
\\� �
idsFamilias
\\� �
,
\\� �
string
\\� �
nombresFamilias
\\� �
,
\\� �
bool
\\� �
todasLasFamilias
\\� �
,
\\� �
bool
\\� �
fromRequest
\\� �
)
\\� �
{]] 	
List^^ 
<^^  
InventarioValorizado^^ %
>^^% &

inventario^^' 1
=^^2 3"
almacenReportingLogica^^4 J
.^^J K)
InventarioValorizadoHistorico^^K h
(^^h i
	idAlmacen^^i r
,^^r s
ProfileData^^t 
(	^^ �
)
^^� �
.
^^� �
Empleado
^^� �
.
^^� �
Id
^^� �
,
^^� �

fechaHasta
^^� �
,
^^� �
todasLasFamilias
^^� �
,
^^� �
idsFamilias
^^� �
)
^^� �
.
^^� �
OrderBy
^^� �
(
^^� �
i
^^� �
=>
^^� �
i
^^� �
.
^^� �
Concepto
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
^^� �
var__ 
parametrosBasicos__ !
=__" #$
ObtenerParametrosBasicos__$ <
(__< =
)__= >
;__> ?
ReportParameter`` "
parametroNombreAlmacen`` 2
=``3 4
new``5 8
ReportParameter``9 H
(``H I
$str``I X
,``X Y

)``g h
;``h i
ReportParameteraa 
parametroFamiliasaa -
=aa. /
newaa0 3
ReportParameteraa4 C
(aaC D
$straaD N
,aaN O
nombresFamiliasaaP _
??aa` b
$straac j
)aaj k
;aak l
ReportParameterbb 
parametroFechaHastabb /
=bb0 1
newbb2 5
ReportParameterbb6 E
(bbE F
$strbbF R
,bbR S

fechaHastabbT ^
.bb^ _
ToStringbb_ g
(bbg h
)bbh i
)bbi j
;bbj k
varcc 
	rptviewercc 
=cc 
newcc 
ReportViewercc  ,
(cc, -
)cc- .
{cc/ 0
ProcessingModecc1 ?
=cc@ A
ProcessingModeccB P
.ccP Q
LocalccQ V
,ccV W
SizeToReportContentccX k
=ccl m
trueccn r
,ccr s
Widthcct y
=ccz {
Unit	cc| �
.
cc� �

Percentage
cc� �
(
cc� �
$num
cc� �
)
cc� �
,
cc� �
Height
cc� �
=
cc� �
Unit
cc� �
.
cc� �

Percentage
cc� �
(
cc� �
$num
cc� �
)
cc� �
}
cc� �
;
cc� �
stringdd 
pathdd 
=dd 
$strdd h
;ddh i
	rptvieweree 
.ee 
LocalReportee !
.ee! "

ReportPathee" ,
=ee- .
fromRequestee/ :
?ee; <
Requestee= D
.eeD E
MapPatheeE L
(eeL M
RequesteeM T
.eeT U
ApplicationPatheeU d
)eed e
+eef g
patheeh l
:eem n
HostingEnvironment	eeo �
.
ee� �
MapPath
ee� �
(
ee� �
path
ee� �
)
ee� �
;
ee� �
	rptviewerff 
.ff 
LocalReportff !
.ff! "

(ff/ 0
newff0 3
ReportParameterff4 C
[ffC D
]ffD E
{ffF G
parametrosBasicosgg !
.gg! "

NombreSedegg" ,
,gg, -
parametrosBasicoshh !
.hh! "
FechaActualSistemahh" 4
,hh4 5
parametrosBasicosii !
.ii! "
LogoSedeii" *
,ii* +
parametrosBasicosjj !
.jj! "
Usuariojj" )
,jj) *
parametroFamiliaskk !
,kk! ""
parametroNombreAlmacenll &
,ll& '
parametroFechaHastamm #
}nn 
)nn
;nn 
ReportDataSourceoo 
rptdatasourceStockoo /
=oo0 1
newoo2 5
ReportDataSourceoo6 F
(ooF G
$strooG d
,ood e

inventariooof p
)oop q
;ooq r
	rptviewerpp 
.pp 
LocalReportpp !
.pp! "
DataSourcespp" -
.pp- .
Addpp. 1
(pp1 2
rptdatasourceStockpp2 D
)ppD E
;ppE F
returnqq 
	rptviewerqq 
;qq 
}rr 	
}tt 
}uu �z
pD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\AlmacenReportesController_Kardex.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{
public 

partial 
class %
AlmacenReportesController 2
{ 
public 
ActionResult 

() *
int* -
	idAlmacen. 7
,7 8
string9 ?

,M N
DateTimeO W

fechaDesdeX b
,b c
DateTimed l

fechaHastam w
,w x
inty |

idConcepto	} �
,
� �
string
� �
nombreConcepto
� �
)
� �
{ 	
var 
	rptviewer 
= !
Generar_Kardex_Fisico 1
(1 2
	idAlmacen2 ;
,; <

,J K

fechaDesdeL V
,V W

fechaHastaX b
,b c

idConceptod n
,n o
nombreConceptop ~
,~ 
true	 �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public 
ActionResult 
Kardex_Valorizado -
(- .
int. 1
	idAlmacen2 ;
,; <
string= C

,Q R
DateTimeS [

fechaDesde\ f
,f g
DateTimeh p

fechaHastaq {
,{ |
int	} �

idConcepto
� �
,
� �
string
� �
nombreConcepto
� �
)
� �
{ 	
var 
	rptviewer 
= %
Generar_Kardex_Valorizado 5
(5 6
	idAlmacen6 ?
,? @

,N O

fechaDesdeP Z
,Z [

fechaHasta\ f
,f g

idConceptoh r
,r s
nombreConcepto	t �
,
� �
true
� �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public 
ReportViewer !
Generar_Kardex_Fisico 1
(1 2
int2 5
	idAlmacen6 ?
,? @
stringA G

,U V
DateTimeW _

fechaDesde` j
,j k
DateTimel t

fechaHastau 
,	 �
int
� �

idConcepto
� �
,
� �
string
� �
nombreConcepto
� �
,
� �
bool
� �
fromRequest
� �
)
� �
{   	
try!! 
{"" 
var## 
kardex## 
=## "
almacenReportingLogica## 3
.##3 4
KardexFisico##4 @
(##@ A
	idAlmacen##A J
,##J K
ProfileData##L W
(##W X
)##X Y
.##Y Z
Empleado##Z b
.##b c
Id##c e
,##e f

fechaDesde##g q
,##q r

fechaHasta##s }
,##} ~

idConcepto	## �
)
##� �
;
##� �
var$$ 
minMax$$ 
=$$ "
almacenReportingLogica$$ 3
.$$3 4%
ObtenerStockMinimoYMaximo$$4 M
($$M N

idConcepto$$N X
)$$X Y
;$$Y Z
var%% 
parametrosBasicos%% %
=%%& '$
ObtenerParametrosBasicos%%( @
(%%@ A
)%%A B
;%%B C
ReportParameter&& 
parametroAlamacen&&  1
=&&2 3
new&&4 7
ReportParameter&&8 G
(&&G H
$str&&H Q
,&&Q R

)&&` a
;&&a b
ReportParameter'' 
parametroFechaDesde''  3
=''4 5
new''6 9
ReportParameter'': I
(''I J
$str''J V
,''V W

fechaDesde''X b
.''b c
ToString''c k
(''k l
)''l m
)''m n
;''n o
ReportParameter(( 
parametroFechaHasta((  3
=((4 5
new((6 9
ReportParameter((: I
(((I J
$str((J V
,((V W

fechaHasta((X b
.((b c
ToString((c k
(((k l
)((l m
)((m n
;((n o
ReportParameter)) 
parametroConcepto))  1
=))2 3
new))4 7
ReportParameter))8 G
())G H
$str))H R
,))R S
nombreConcepto))T b
)))b c
;))c d
ReportParameter**  
parametroStockMinimo**  4
=**5 6
new**7 :
ReportParameter**; J
(**J K
$str**K X
,**X Y
minMax**Z `
.**` a
StockMinimo**a l
.**l m
ToString**m u
(**u v
)**v w
)**w x
;**x y
ReportParameter++  
parametroStockMaximo++  4
=++5 6
new++7 :
ReportParameter++; J
(++J K
$str++K X
,++X Y
minMax++Z `
.++` a
StockMaximo++a l
.++l m
ToString++m u
(++u v
)++v w
)++w x
;++x y
ReportParameter,,  
parametroStockActual,,  4
=,,5 6
new,,7 :
ReportParameter,,; J
(,,J K
$str,,K X
,,,X Y
kardex,,Z `
.,,` a
Last,,a e
(,,e f
),,f g
.,,g h

.,,u v
ToString,,v ~
(,,~ 
)	,, �
)
,,� �
;
,,� �
var.. 
	rptviewer.. 
=.. 
new..  #
ReportViewer..$ 0
(..0 1
)..1 2
{..3 4
ProcessingMode..5 C
=..D E
ProcessingMode..F T
...T U
Local..U Z
,..Z [
SizeToReportContent..\ o
=..p q
true..r v
,..v w
Width..x }
=..~ 
Unit
..� �
.
..� �

Percentage
..� �
(
..� �
$num
..� �
)
..� �
,
..� �
Height
..� �
=
..� �
Unit
..� �
.
..� �

Percentage
..� �
(
..� �
$num
..� �
)
..� �
}
..� �
;
..� �
string// 
path// 
=// 
$str// Q
;//Q R
	rptviewer11 
.11 
LocalReport11 %
.11% &

ReportPath11& 0
=111 2
fromRequest113 >
?11? @
Request11A H
.11H I
MapPath11I P
(11P Q
Request11Q X
.11X Y
ApplicationPath11Y h
)11h i
+11j k
path11l p
:11q r
HostingEnvironment	11s �
.
11� �
MapPath
11� �
(
11� �
path
11� �
)
11� �
;
11� �
	rptviewer22 
.22 
LocalReport22 %
.22% &

(223 4
new224 7
ReportParameter228 G
[22G H
]22H I
{33 
parametrosBasicos44 %
.44% &
FechaActualSistema44& 8
,448 9
parametrosBasicos55 %
.55% &
LogoSede55& .
,55. /
parametrosBasicos66 %
.66% &

NombreSede66& 0
,660 1
parametrosBasicos77 %
.77% &
Usuario77& -
,77- .
parametroAlamacen88 %
,88% &
parametroFechaDesde99 '
,99' (
parametroFechaHasta:: '
,::' (
parametroConcepto;; %
,;;% & 
parametroStockMinimo<< (
,<<( ) 
parametroStockMaximo== (
,==( ) 
parametroStockActual>> (
}?? 
)?? 
;?? 
ReportDataSource@@  !
rptdatasourceEntradas@@! 6
=@@7 8
new@@9 <
ReportDataSource@@= M
(@@M N
$str@@N c
,@@c d
kardex@@e k
)@@k l
;@@l m
	rptviewerAA 
.AA 
LocalReportAA %
.AA% &
DataSourcesAA& 1
.AA1 2
AddAA2 5
(AA5 6!
rptdatasourceEntradasAA6 K
)AAK L
;AAL M
returnBB 
	rptviewerBB  
;BB  !
}CC 
catchDD 
(DD 
	ExceptionDD 
eDD 
)DD 
{EE 
throwFF 
newFF 
ControllerExceptionFF -
(FF- .
$strFF. N
,FFN O
eFFP Q
)FFQ R
;FFR S
}GG 
}HH 	
publicII 
ReportViewerII %
Generar_Kardex_ValorizadoII 5
(II5 6
intII6 9
	idAlmacenII: C
,IIC D
stringIIE K

,IIY Z
DateTimeII[ c

fechaDesdeIId n
,IIn o
DateTimeIIp x

fechaHasta	IIy �
,
II� �
int
II� �

idConcepto
II� �
,
II� �
string
II� �
nombreConcepto
II� �
,
II� �
bool
II� �
fromRequest
II� �
)
II� �
{JJ 	
tryKK 
{LL 
varMM 
kardexMM 
=MM "
almacenReportingLogicaMM 3
.MM3 4
KardexValorizadoMM4 D
(MMD E
	idAlmacenMME N
,MMN O
ProfileDataMMP [
(MM[ \
)MM\ ]
.MM] ^
EmpleadoMM^ f
.MMf g
IdMMg i
,MMi j

fechaDesdeMMk u
,MMu v

fechaHasta	MMw �
,
MM� �

idConcepto
MM� �
)
MM� �
;
MM� �
varNN 
minMaxNN 
=NN "
almacenReportingLogicaNN 3
.NN3 4%
ObtenerStockMinimoYMaximoNN4 M
(NNM N

idConceptoNNN X
)NNX Y
;NNY Z
varPP 
parametrosBasicosPP %
=PP& '$
ObtenerParametrosBasicosPP( @
(PP@ A
)PPA B
;PPB C
ReportParameterQQ 
parametroAlamacenQQ  1
=QQ2 3
newQQ4 7
ReportParameterQQ8 G
(QQG H
$strQQH Q
,QQQ R

)QQ` a
;QQa b
ReportParameterRR 
parametroFechaDesdeRR  3
=RR4 5
newRR6 9
ReportParameterRR: I
(RRI J
$strRRJ V
,RRV W

fechaDesdeRRX b
.RRb c
ToStringRRc k
(RRk l
)RRl m
)RRm n
;RRn o
ReportParameterSS 
parametroFechaHastaSS  3
=SS4 5
newSS6 9
ReportParameterSS: I
(SSI J
$strSSJ V
,SSV W

fechaHastaSSX b
.SSb c
ToStringSSc k
(SSk l
)SSl m
)SSm n
;SSn o
ReportParameterTT 
parametroConceptoTT  1
=TT2 3
newTT4 7
ReportParameterTT8 G
(TTG H
$strTTH R
,TTR S
nombreConceptoTTT b
)TTb c
;TTc d
ReportParameterUU  
parametroStockMinimoUU  4
=UU5 6
newUU7 :
ReportParameterUU; J
(UUJ K
$strUUK X
,UUX Y
minMaxUUZ `
.UU` a
StockMinimoUUa l
.UUl m
ToStringUUm u
(UUu v
)UUv w
)UUw x
;UUx y
ReportParameterVV  
parametroStockMaximoVV  4
=VV5 6
newVV7 :
ReportParameterVV; J
(VVJ K
$strVVK X
,VVX Y
minMaxVVZ `
.VV` a
StockMaximoVVa l
.VVl m
ToStringVVm u
(VVu v
)VVv w
)VVw x
;VVx y
ReportParameterWW  
parametroStockActualWW  4
=WW5 6
newWW7 :
ReportParameterWW; J
(WWJ K
$strWWK X
,WWX Y
kardexWWZ `
.WW` a
LastWWa e
(WWe f
)WWf g
.WWg h

.WWu v
ToStringWWv ~
(WW~ 
)	WW �
)
WW� �
;
WW� �
varXX 
	rptviewerXX 
=XX 
newXX  #
ReportViewerXX$ 0
(XX0 1
)XX1 2
{XX3 4
ProcessingModeXX5 C
=XXD E
ProcessingModeXXF T
.XXT U
LocalXXU Z
,XXZ [
SizeToReportContentXX\ o
=XXp q
trueXXr v
,XXv w
WidthXXx }
=XX~ 
Unit
XX� �
.
XX� �

Percentage
XX� �
(
XX� �
$num
XX� �
)
XX� �
,
XX� �
Height
XX� �
=
XX� �
Unit
XX� �
.
XX� �

Percentage
XX� �
(
XX� �
$num
XX� �
)
XX� �
}
XX� �
;
XX� �
stringYY 
pathYY 
=YY 
$strYY U
;YYU V
	rptviewer[[ 
.[[ 
LocalReport[[ %
.[[% &

ReportPath[[& 0
=[[1 2
fromRequest[[3 >
?[[? @
Request[[A H
.[[H I
MapPath[[I P
([[P Q
Request[[Q X
.[[X Y
ApplicationPath[[Y h
)[[h i
+[[j k
path[[l p
:[[q r
HostingEnvironment	[[s �
.
[[� �
MapPath
[[� �
(
[[� �
path
[[� �
)
[[� �
;
[[� �
	rptviewer\\ 
.\\ 
LocalReport\\ %
.\\% &

(\\3 4
new\\4 7
ReportParameter\\8 G
[\\G H
]\\H I
{]] 
parametrosBasicos^^ %
.^^% &
FechaActualSistema^^& 8
,^^8 9
parametrosBasicos__ %
.__% &
LogoSede__& .
,__. /
parametrosBasicos`` %
.``% &

NombreSede``& 0
,``0 1
parametrosBasicosaa %
.aa% &
Usuarioaa& -
,aa- .
parametroAlamacenbb %
,bb% &
parametroFechaDesdecc '
,cc' (
parametroFechaHastadd '
,dd' (
parametroConceptoee %
,ee% & 
parametroStockMinimoff (
,ff( ) 
parametroStockMaximogg (
,gg( ) 
parametroStockActualhh (
}ii 
)ii 
;ii 
ReportDataSourcejj  !
rptdatasourceEntradasjj! 6
=jj7 8
newjj9 <
ReportDataSourcejj= M
(jjM N
$strjjN g
,jjg h
kardexjji o
)jjo p
;jjp q
	rptviewerkk 
.kk 
LocalReportkk %
.kk% &
DataSourceskk& 1
.kk1 2
Addkk2 5
(kk5 6!
rptdatasourceEntradaskk6 K
)kkK L
;kkL M
returnll 
	rptviewerll  
;ll  !
}mm 
catchnn 
(nn 
	Exceptionnn 
enn 
)nn 
{oo 
throwpp 
newpp 
ControllerExceptionpp -
(pp- .
$strpp. R
,ppR S
eppT U
)ppU V
;ppV W
}qq 
}rr 	
}ss 
}tt ��
uD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Almacen\AlmacenReportesController_Movimientos.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{
public 

partial 
class %
AlmacenReportesController 2
{ 
public 
ActionResult  
Movimientos_Entradas 0
(0 1
int1 4
	idAlmacen5 >
,> ?
string@ F

,T U
DateTimeV ^

fechaDesde_ i
,i j
DateTimek s

fechaHastat ~
,~ 
[
� �
System
� �
.
� �
Web
� �
.
� �
Http
� �
.
� �
FromUri
� �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
)
� �
{ 	
var 
	rptviewer 
= (
Generar_Movimientos_Entradas 8
(8 9
	idAlmacen9 B
,B C

,Q R

fechaDesdeS ]
,] ^

fechaHasta_ i
,i j
idsFamiliask v
,v w
nombresFamilias	y �
,
� �
todasLasFamilias
� �
,
� �
true
� �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public 
ActionResult 
Movimientos_Salidas /
(/ 0
int0 3
	idAlmacen4 =
,= >
string? E

,S T
DateTimeU ]

fechaDesde^ h
,h i
DateTimej r

fechaHastas }
,} ~
[	 �
System
� �
.
� �
Web
� �
.
� �
Http
� �
.
� �
FromUri
� �
]
� �
int
� �
[
� �
]
� �
idsFamilias
� �
,
� �
string
� �
nombresFamilias
� �
,
� �
bool
� �
todasLasFamilias
� �
)
� �
{ 	
var 
	rptviewer 
= '
Generar_Movimientos_Salidas 7
(7 8
	idAlmacen8 A
,A B

,P Q

fechaDesdeR \
,\ ]

fechaHasta^ h
,h i
idsFamiliasj u
,u v
nombresFamilias	w �
,
� �
todasLasFamilias
� �
,
� �
true
� �
)
� �
;
� �
ViewBag 
. 
ReportViewer  
=! "
	rptviewer# ,
;, -
return 
View 
( 
$str -
)- .
;. /
} 	
public   
ActionResult   $
Movimientos_Vencimientos   4
(  4 5
int  5 8
	idAlmacen  9 B
,  B C
string  D J

,  X Y
DateTime  Z b

fechaDesde  c m
,  m n
DateTime  o w

fechaHasta	  x �
,
  � �
[
  � �
System
  � �
.
  � �
Web
  � �
.
  � �
Http
  � �
.
  � �
FromUri
  � �
]
  � �
int
  � �
[
  � �
]
  � �
idsFamilias
  � �
,
  � �
string
  � �
nombresFamilias
  � �
,
  � �
bool
  � �
todasLasFamilias
  � �
)
  � �
{!! 	
var"" 
	rptviewer"" 
="" ,
 Generar_Movimientos_Vencimientos"" <
(""< =
	idAlmacen""= F
,""F G

,""U V

fechaDesde""W a
,""a b

fechaHasta""c m
,""m n
idsFamilias""o z
,""z {
nombresFamilias	""| �
,
""� �
todasLasFamilias
""� �
,
""� �
true
""� �
)
""� �
;
""� �
ViewBag## 
.## 
ReportViewer##  
=##! "
	rptviewer### ,
;##, -
return$$ 
View$$ 
($$ 
$str$$ -
)$$- .
;$$. /
}%% 	
public&& 
ReportViewer&& (
Generar_Movimientos_Entradas&& 8
(&&8 9
int&&9 <
	idAlmacen&&= F
,&&F G
string&&H N

,&&\ ]
DateTime&&^ f

fechaDesde&&g q
,&&q r
DateTime&&s {

fechaHasta	&&| �
,
&&� �
[
&&� �
System
&&� �
.
&&� �
Web
&&� �
.
&&� �
Http
&&� �
.
&&� �
FromUri
&&� �
]
&&� �
int
&&� �
[
&&� �
]
&&� �
idsFamilias
&&� �
,
&&� �
string
&&� �
nombresFamilias
&&� �
,
&&� �
bool
&&� �
todasLasFamilias
&&� �
,
&&� �
bool
&&� �
fromRequest
&&� �
)
&&� �
{'' 	
try(( 
{)) 
var** 
entradas** 
=** "
almacenReportingLogica** 5
.**5 6
Entradas**6 >
(**> ?
	idAlmacen**? H
,**H I

fechaDesde**J T
,**T U

fechaHasta**V `
,**` a
todasLasFamilias**b r
,**r s
idsFamilias**t 
)	** �
;
**� �
var++ 
parametrosBasicos++ %
=++& '$
ObtenerParametrosBasicos++( @
(++@ A
)++A B
;++B C
ReportParameter,, 
parametroFamilias,,  1
=,,2 3
new,,4 7
ReportParameter,,8 G
(,,G H
$str,,H R
,,,R S
nombresFamilias,,T c
??,,c e
$str,,f m
),,m n
;,,n o
ReportParameter-- 
parametroAlamacen--  1
=--2 3
new--4 7
ReportParameter--8 G
(--G H
$str--H Q
,--Q R

)--` a
;--a b
ReportParameter.. 
parametroFechaDesde..  3
=..4 5
new..6 9
ReportParameter..: I
(..I J
$str..J V
,..V W

fechaDesde..X b
...b c
ToString..c k
(..k l
)..l m
)..m n
;..n o
ReportParameter// 
parametroFechaHasta//  3
=//4 5
new//6 9
ReportParameter//: I
(//I J
$str//J V
,//V W

fechaHasta//X b
.//b c
ToString//c k
(//k l
)//l m
)//m n
;//n o
var11 
	rptviewer11 
=11 
new11  #
ReportViewer11$ 0
(110 1
)111 2
{113 4
ProcessingMode115 C
=11D E
ProcessingMode11F T
.11T U
Local11U Z
,11Z [
SizeToReportContent11\ o
=11p q
true11r v
,11v w
Width11x }
=11~ 
Unit
11� �
.
11� �

Percentage
11� �
(
11� �
$num
11� �
)
11� �
,
11� �
Height
11� �
=
11� �
Unit
11� �
.
11� �

Percentage
11� �
(
11� �
$num
11� �
)
11� �
}
11� �
;
11� �
string22 
path22 
=22 
$str22 X
;22X Y
	rptviewer44 
.44 
LocalReport44 %
.44% &

ReportPath44& 0
=441 2
fromRequest443 >
?44? @
Request44A H
.44H I
MapPath44I P
(44P Q
Request44Q X
.44X Y
ApplicationPath44Y h
)44h i
+44j k
path44l p
:44q r
HostingEnvironment	44s �
.
44� �
MapPath
44� �
(
44� �
path
44� �
)
44� �
;
44� �
	rptviewer55 
.55 
LocalReport55 %
.55% &

(553 4
new554 7
ReportParameter558 G
[55G H
]55H I
{66 
parametrosBasicos77 %
.77% &
FechaActualSistema77& 8
,778 9
parametrosBasicos88 %
.88% &
LogoSede88& .
,88. /
parametrosBasicos99 %
.99% &

NombreSede99& 0
,990 1
parametrosBasicos:: %
.::% &
Usuario::& -
,::- .
parametroFamilias;; %
,;;% &
parametroAlamacen<< %
,<<% &
parametroFechaDesde== '
,==' (
parametroFechaHasta>> '
}?? 
)?? 
;?? 
ReportDataSource@@  !
rptdatasourceEntradas@@! 6
=@@7 8
new@@9 <
ReportDataSource@@= M
(@@M N
$str@@N _
,@@_ `
entradas@@a i
)@@i j
;@@j k
	rptviewerAA 
.AA 
LocalReportAA %
.AA% &
DataSourcesAA& 1
.AA1 2
AddAA2 5
(AA5 6!
rptdatasourceEntradasAA6 K
)AAK L
;AAL M
returnBB 
	rptviewerBB  
;BB  !
}CC 
catchDD 
(DD 
	ExceptionDD 
eDD 
)DD 
{EE 
throwFF 
newFF 
ControllerExceptionFF -
(FF- .
$strFF. I
,FFI J
eFFK L
)FFL M
;FFM N
}GG 
}HH 	
publicII 
ReportViewerII '
Generar_Movimientos_SalidasII 7
(II7 8
intII8 ;
	idAlmacenII< E
,IIE F
stringIIG M

,II[ \
DateTimeII] e

fechaDesdeIIf p
,IIp q
DateTimeIIr z

fechaHasta	II{ �
,
II� �
int
II� �
[
II� �
]
II� �
idsFamilias
II� �
,
II� �
string
II� �
nombresFamilias
II� �
,
II� �
bool
II� �
todasLasFamilias
II� �
,
II� �
bool
II� �
fromRequest
II� �
)
II� �
{JJ 	
tryKK 
{LL 
varMM 
entradasMM 
=MM "
almacenReportingLogicaMM 5
.MM5 6
SalidasMM6 =
(MM= >
	idAlmacenMM> G
,MMG H

fechaDesdeMMI S
,MMS T

fechaHastaMMU _
,MM_ `
todasLasFamiliasMMa q
,MMq r
idsFamiliasMMs ~
)MM~ 
;	MM �
varNN 
parametrosBasicosNN %
=NN& '$
ObtenerParametrosBasicosNN( @
(NN@ A
)NNA B
;NNB C
ReportParameterOO 
parametroFamiliasOO  1
=OO2 3
newOO4 7
ReportParameterOO8 G
(OOG H
$strOOH R
,OOR S
nombresFamiliasOOT c
??OOc e
$strOOf m
)OOm n
;OOn o
ReportParameterPP 
parametroAlamacenPP  1
=PP2 3
newPP4 7
ReportParameterPP8 G
(PPG H
$strPPH Q
,PPQ R

)PP` a
;PPa b
ReportParameterQQ 
parametroFechaDesdeQQ  3
=QQ4 5
newQQ6 9
ReportParameterQQ: I
(QQI J
$strQQJ V
,QQV W

fechaDesdeQQX b
.QQb c
ToStringQQc k
(QQk l
)QQl m
)QQm n
;QQn o
ReportParameterRR 
parametroFechaHastaRR  3
=RR4 5
newRR6 9
ReportParameterRR: I
(RRI J
$strRRJ V
,RRV W

fechaHastaRRX b
.RRb c
ToStringRRc k
(RRk l
)RRl m
)RRm n
;RRn o
varTT 
	rptviewerTT 
=TT 
newTT  #
ReportViewerTT$ 0
(TT0 1
)TT1 2
{TT3 4
ProcessingModeTT5 C
=TTD E
ProcessingModeTTF T
.TTT U
LocalTTU Z
,TTZ [
SizeToReportContentTT\ o
=TTp q
trueTTr v
,TTv w
WidthTTx }
=TT~ 
Unit
TT� �
.
TT� �

Percentage
TT� �
(
TT� �
$num
TT� �
)
TT� �
,
TT� �
Height
TT� �
=
TT� �
Unit
TT� �
.
TT� �

Percentage
TT� �
(
TT� �
$num
TT� �
)
TT� �
}
TT� �
;
TT� �
stringUU 
pathUU 
=UU 
$strUU W
;UUW X
	rptviewerWW 
.WW 
LocalReportWW %
.WW% &

ReportPathWW& 0
=WW1 2
fromRequestWW3 >
?WW? @
RequestWWA H
.WWH I
MapPathWWI P
(WWP Q
RequestWWQ X
.WWX Y
ApplicationPathWWY h
)WWh i
+WWj k
pathWWl p
:WWq r
HostingEnvironment	WWs �
.
WW� �
MapPath
WW� �
(
WW� �
path
WW� �
)
WW� �
;
WW� �
	rptviewerXX 
.XX 
LocalReportXX %
.XX% &

(XX3 4
newXX4 7
ReportParameterXX8 G
[XXG H
]XXH I
{YY 
parametrosBasicosZZ %
.ZZ% &
FechaActualSistemaZZ& 8
,ZZ8 9
parametrosBasicos[[ %
.[[% &
LogoSede[[& .
,[[. /
parametrosBasicos\\ %
.\\% &

NombreSede\\& 0
,\\0 1
parametrosBasicos]] %
.]]% &
Usuario]]& -
,]]- .
parametroFamilias^^ %
,^^% &
parametroAlamacen__ %
,__% &
parametroFechaDesde`` '
,``' (
parametroFechaHastaaa '
}bb 
)bb 
;bb 
ReportDataSourcecc  !
rptdatasourceEntradascc! 6
=cc7 8
newcc9 <
ReportDataSourcecc= M
(ccM N
$strccN ^
,cc^ _
entradascc` h
)cch i
;cci j
	rptviewerdd 
.dd 
LocalReportdd %
.dd% &
DataSourcesdd& 1
.dd1 2
Adddd2 5
(dd5 6!
rptdatasourceEntradasdd6 K
)ddK L
;ddL M
returnee 
	rptvieweree  
;ee  !
}ff 
catchgg 
(gg 
	Exceptiongg 
egg 
)gg 
{hh 
throwii 
newii 
ControllerExceptionii -
(ii- .
$strii. H
,iiH I
eiiJ K
)iiK L
;iiL M
}jj 
}kk 	
publicmm 
ReportViewermm ,
 Generar_Movimientos_Vencimientosmm <
(mm< =
intmm= @
	idAlmacenmmA J
,mmJ K
stringmmL R

,mm` a
DateTimemmb j

fechaDesdemmk u
,mmu v
DateTimemmw 

fechaHasta
mm� �
,
mm� �
int
mm� �
[
mm� �
]
mm� �
idsFamilias
mm� �
,
mm� �
string
mm� �
nombresFamilias
mm� �
,
mm� �
bool
mm� �
todasLasFamilias
mm� �
,
mm� �
bool
mm� �
fromRequest
mm� �
)
mm� �
{nn 	
tryoo 
{pp 
varqq 
vencimientosqq  
=qq! ""
almacenReportingLogicaqq# 9
.qq9 :
Vencimientosqq: F
(qqF G
	idAlmacenqqG P
,qqP Q

fechaDesdeqqR \
,qq\ ]

fechaHastaqq^ h
,qqh i
todasLasFamiliasqqj z
,qqz {
idsFamilias	qq| �
)
qq� �
;
qq� �
varrr 
parametrosBasicosrr %
=rr& '$
ObtenerParametrosBasicosrr( @
(rr@ A
)rrA B
;rrB C
ReportParameterss 
parametroFamiliasss  1
=ss2 3
newss4 7
ReportParameterss8 G
(ssG H
$strssH R
,ssR S
nombresFamiliasssT c
??ssd f
$strssg n
)ssn o
;sso p
ReportParametertt 
parametroAlamacentt  1
=tt2 3
newtt4 7
ReportParametertt8 G
(ttG H
$strttH Q
,ttQ R

)tt` a
;tta b
ReportParameteruu 
parametroFechaDesdeuu  3
=uu4 5
newuu6 9
ReportParameteruu: I
(uuI J
$struuJ V
,uuV W

fechaDesdeuuX b
.uub c
ToStringuuc k
(uuk l
)uul m
)uum n
;uun o
ReportParametervv 
parametroFechaHastavv  3
=vv4 5
newvv6 9
ReportParametervv: I
(vvI J
$strvvJ V
,vvV W

fechaHastavvX b
.vvb c
ToStringvvc k
(vvk l
)vvl m
)vvm n
;vvn o
varxx 
	rptviewerxx 
=xx 
newxx  #
ReportViewerxx$ 0
(xx0 1
)xx1 2
{xx3 4
ProcessingModexx5 C
=xxD E
ProcessingModexxF T
.xxT U
LocalxxU Z
,xxZ [
SizeToReportContentxx\ o
=xxp q
truexxr v
,xxv w
Widthxxx }
=xx~ 
Unit
xx� �
.
xx� �

Percentage
xx� �
(
xx� �
$num
xx� �
)
xx� �
,
xx� �
Height
xx� �
=
xx� �
Unit
xx� �
.
xx� �

Percentage
xx� �
(
xx� �
$num
xx� �
)
xx� �
}
xx� �
;
xx� �
stringyy 
pathyy 
=yy 
$stryy \
;yy\ ]
	rptviewer{{ 
.{{ 
LocalReport{{ %
.{{% &

ReportPath{{& 0
={{1 2
fromRequest{{3 >
?{{? @
Request{{A H
.{{H I
MapPath{{I P
({{P Q
Request{{Q X
.{{X Y
ApplicationPath{{Y h
){{h i
+{{j k
path{{l p
:{{q r
HostingEnvironment	{{s �
.
{{� �
MapPath
{{� �
(
{{� �
path
{{� �
)
{{� �
;
{{� �
	rptviewer|| 
.|| 
LocalReport|| %
.||% &

(||3 4
new||4 7
ReportParameter||8 G
[||G H
]||H I
{}} 
parametrosBasicos~~ %
.~~% &
FechaActualSistema~~& 8
,~~8 9
parametrosBasicos %
.% &
LogoSede& .
,. /
parametrosBasicos
�� %
.
��% &

NombreSede
��& 0
,
��0 1
parametrosBasicos
�� %
.
��% &
Usuario
��& -
,
��- .
parametroFamilias
�� %
,
��% &
parametroAlamacen
�� %
,
��% &!
parametroFechaDesde
�� '
,
��' (!
parametroFechaHasta
�� '
}
�� 
)
�� 
;
�� 
ReportDataSource
��  #
rptdatasourceEntradas
��! 6
=
��7 8
new
��9 <
ReportDataSource
��= M
(
��M N
$str
��N c
,
��c d
vencimientos
��e q
)
��q r
;
��r s
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 6#
rptdatasourceEntradas
��6 K
)
��K L
;
��L M
return
�� 
	rptviewer
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
�� !
ControllerException
�� -
(
��- .
$str
��. M
,
��M N
e
��O P
)
��P Q
;
��Q R
}
�� 
}
�� 	
}
�� 
}�� �
iD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Concepto\CaracteristicaController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class $
CaracteristicaController )
:* +
BaseController, :
{ 
private 
readonly "
ICaracteristica_Logica /!
_logicaCaracteristica0 E
;E F
public $
CaracteristicaController '
(' (
)( )
{ 	!
_logicaCaracteristica !
=" #
Dependencia$ /
./ 0
Resolve0 7
<7 8"
ICaracteristica_Logica8 N
>N O
(O P
)P Q
;Q R
} 	
public 

JsonResult L
@ObtenerCaracteristicasYValoresDeCaracteristicasDeConceptoNegocio Z
(Z [
int[ ^

idConcepto_ i
)i j
{   	
try!! 
{"" 
var## 
concepto## 
=## !
_logicaCaracteristica## 4
.##4 5B
6ObtenerConceptoNegocioConSusCaracteristicasYSusValores##5 k
(##k l

idConcepto##l v
)##v w
;##w x
return$$ 
Json$$ 
($$ 
concepto$$ $
)$$$ %
;$$% &
}%% 
catch&& 
(&& 
	Exception&& 
e&& 
)&& 
{'' 
return(( 
Json(( 
((( 
Util((  
.((  !
	ErrorJson((! *
(((* +
e((+ ,
)((, -
)((- .
;((. /
})) 
}** 	
},, 
}-- �0
kD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Concepto\ConceptoReportesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class &
ConceptoReportesController +
:, -
BaseController. <
{ 
private 
readonly 
IConceptoLogica (
_logicaConcepto) 8
;8 9
public &
ConceptoReportesController )
() *
)* +
{ 	
_logicaConcepto 
= 
Dependencia )
.) *
Resolve* 1
<1 2
IConceptoLogica2 A
>A B
(B C
)C D
;D E
} 	
[   	
	Authorize  	 
(   
Roles   
=   
$str   E
)  E F
]  F G
public!! 
ActionResult!! !
ReportesAdministrador!! 1
(!!1 2
)!!2 3
{"" 	
return## 
View## 
(## 
)## 
;## 
}$$ 	
public&& 
ActionResult&& -
!ReporteDeProductoConCodigoDeBarra&& =
(&&= >
)&&> ?
{'' 	
List(( 
<(( 
ConceptoDeNegocio(( "
>((" #
	resultado(($ -
=((. /
_logicaConcepto((0 ?
.((? @
obtenerMercaderias((@ R
(((R S
)((S T
;((T U
List)) 
<)) $
ReporteProductoViewModel)) )
>))) *$
reporteProductoViewModel))+ C
=))D E$
ReporteProductoViewModel))F ^
.))^ _
Convert))_ f
())f g
	resultado))g p
)))p q
;))q r
ReportParameter** 
entidadInterna** *
=**+ ,
new**- 0
ReportParameter**1 @
(**@ A
$str**A W
,**W X
ProfileData**Y d
(**d e
)**e f
.**f g/
"NombreCentroDeAtencionSeleccionado	**g �
)
**� �
;
**� �
ReportParameter++ 
fecha++ !
=++" #
new++$ '
ReportParameter++( 7
(++7 8
$str++8 E
,++E F
DateTimeUtil++F R
.++R S
FechaActual++S ^
(++^ _
)++_ `
.++` a
ToString++a i
(++i j
)++j k
)++k l
;++l m
ReportParameter,, 

=,,* +
new,,, /
ReportParameter,,0 ?
(,,? @
$str,,@ O
,,,O P
ObtenerSede,,Q \
(,,\ ]
),,] ^
.,,^ _
Nombre,,_ e
),,e f
;,,f g
var-- 
sede-- 
=-- 
ObtenerSede-- "
(--" #
)--# $
;--$ %
string.. 

logoString.. 
=.. 
Convert..  '
...' (
ToBase64String..( 6
(..6 7
sede..7 ;
...; <
Logo..< @
,..@ A
$num..B C
,..C D
sede..E I
...I J
Logo..J N
...N O
Length..O U
)..U V
;..V W
ReportParameter// 
logoSede// $
=//% &
new//' *
ReportParameter//+ :
(//: ;
$str//; E
,//E F

logoString//G Q
)//Q R
;//R S
ReportParameter00 
empleadoSede00 (
=00) *
new00+ .
ReportParameter00/ >
(00> ?
$str00? H
,00H I
ProfileData00J U
(00U V
)00V W
.00W X
Empleado00X `
.00` a
NombresYApellidos00a r
)00r s
;00s t
DateTime11 
fechaActual11  
=11! "
DateTimeUtil11" .
.11. /
FechaActual11/ :
(11: ;
)11; <
;11< =
ReportParameter22 
fechaActualSistema22 .
=22/ 0
new221 4
ReportParameter225 D
(22D E
$str22E Y
,22Y Z
fechaActual22[ f
.22f g
ToString22g o
(22o p
)22p q
)22q r
;22r s
var33 
	rptviewer33 
=33 
new33 
ReportViewer33  ,
(33, -
)33- .
;33. /
	rptviewer44 
.44 
ProcessingMode44 $
=44% &
ProcessingMode44' 5
.445 6
Local446 ;
;44; <
	rptviewer55 
.55 
LocalReport55 !
.55! "

ReportPath55" ,
=55- .
Request55/ 6
.556 7
MapPath557 >
(55> ?
Request55? F
.55F G
ApplicationPath55G V
)55V W
+55X Y
$str	55Z �
;
55� �
	rptviewer66 
.66 
LocalReport66 !
.66! "

(66/ 0
new660 3
ReportParameter664 C
[66C D
]66D E
{66F G
entidadInterna66H V
,66V W

,66e f
fecha66g l
,66l m
logoSede66n v
,66v w
empleadoSede	66x �
,
66� � 
fechaActualSistema
66� �
}
66� �
)
66� �
;
66� �
ReportDataSource77 
rptdatasourceStock77 /
=770 1
new772 5
ReportDataSource776 F
(77F G
$str77G a
,77a b$
reporteProductoViewModel77c {
)77{ |
;77| }
	rptviewer88 
.88 
LocalReport88 !
.88! "
DataSources88" -
.88- .
Add88. 1
(881 2
rptdatasourceStock882 D
)88D E
;88E F
	rptviewer99 
.99 
SizeToReportContent99 )
=99* +
true99, 0
;990 1
ViewBag:: 
.:: 
ReportViewer::  
=::! "
	rptviewer::# ,
;::, -
return;; 
View;; 
(;; 
$str;; -
);;- .
;;;. /
}<< 	
}== 
}>> �L
vD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Finanza\FinanzaReportes_EstadoCuentaController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class 2
&FinanzaReportes_EstadoCuentaController ?
:@ A
BaseControllerB P
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly "
IFinanzaReporte_Logica 1"
finanzaReportingLogica2 H
;H I
public!! 2
&FinanzaReportes_EstadoCuentaController!! 5
(!!5 6
)!!6 7
:!!8 9
base!!: >
(!!> ?
)!!? @
{"" 	
operacionLogica## 
=## 
Dependencia## )
.##) *
Resolve##* 1
<##1 2
IOperacionLogica##2 B
>##B C
(##C D
)##D E
;##E F
actorNegocioLogica$$ 
=$$  
Dependencia$$! ,
.$$, -
Resolve$$- 4
<$$4 5
IActorNegocioLogica$$5 H
>$$H I
($$I J
)$$J K
;$$K L
conceptoLogica%% 
=%% 
Dependencia%% (
.%%( )
Resolve%%) 0
<%%0 1
IConceptoLogica%%1 @
>%%@ A
(%%A B
)%%B C
;%%C D"
finanzaReportingLogica&& "
=&&# $
Dependencia&&% 0
.&&0 1
Resolve&&1 8
<&&8 9"
IFinanzaReporte_Logica&&9 O
>&&O P
(&&P Q
)&&Q R
;&&R S
}'' 	
public)) 
ActionResult)) /
#EstadoCuenta_EstadoCuentaPorCliente)) ?
())? @
DateTime))@ H

fechaDesde))I S
,))S T
DateTime))U ]

fechaHasta))^ h
,))h i
int))j m
	idCliente))n w
,))w x
string))y 

))� �
)
))� �
{** 	

fechaDesde++ 
=++ 
DateTime++ !
.++! "
Parse++" '
(++' (

fechaDesde++( 2
.++2 3
ToString++3 ;
(++; <
$str++< H
)++H I
+++J K
$str++L W
)++W X
;++X Y

fechaHasta,, 
=,, 
DateTime,, !
.,,! "
Parse,," '
(,,' (

fechaHasta,,( 2
.,,2 3
ToString,,3 ;
(,,; <
$str,,< H
),,H I
+,,J K
$str,,L W
),,W X
;,,X Y
EstadoDeCuenta.. 
estadoDeCuenta.. )
=..* +
operacionLogica.., ;
...; <(
ObtenerEstadoDeCuentaCliente..< X
(..X Y
	idCliente..Y b
,..b c

fechaDesde..d n
,..n o

fechaHasta..p z
)..z {
;..{ |
ReportParameter00 "
parametroNombreEmpresa00 2
=003 4
new005 8
ReportParameter009 H
(00H I
$str00I X
,00X Y
ObtenerSede00Z e
(00e f
)00f g
.00g h
Nombre00h n
)00n o
;00o p
ReportParameter11 
parametroCliente11 ,
=11- .
new11/ 2
ReportParameter113 B
(11B C
$str11C L
,11L M

)11[ \
;11\ ]
ReportParameter22 
parametroFechaDesde22 /
=220 1
new222 5
ReportParameter226 E
(22E F
$str22F R
,22R S

fechaDesde22T ^
.22^ _
ToString22_ g
(22g h
)22h i
)22i j
;22j k
ReportParameter33 
parametroFechaHasta33 /
=330 1
new332 5
ReportParameter336 E
(33E F
$str33F R
,33R S

fechaHasta33T ^
.33^ _
ToString33_ g
(33g h
)33h i
)33i j
;33j k
ReportParameter44 "
parametroSaldoAnterior44 2
=443 4
new445 8
ReportParameter449 H
(44H I
$str44I X
,44X Y
estadoDeCuenta44Z h
.44h i

.44v w
ToString44w 
(	44 �
)
44� �
)
44� �
;
44� �
ReportParameter55 
parametroEntregas55 -
=55. /
new550 3
ReportParameter554 C
(55C D
$str55D N
,55N O
estadoDeCuenta55P ^
.55^ _
Entregas55_ g
.55g h
ToString55h p
(55p q
)55q r
)55r s
;55s t
ReportParameter66 
parametroPagos66 *
=66+ ,
new66- 0
ReportParameter661 @
(66@ A
$str66A H
,66H I
estadoDeCuenta66J X
.66X Y
Pagos66Y ^
.66^ _
ToString66_ g
(66g h
)66h i
)66i j
;66j k
ReportParameter77 
parametroSaldoFinal77 /
=770 1
new772 5
ReportParameter776 E
(77E F
$str77F R
,77R S
estadoDeCuenta77T b
.77b c

SaldoFinal77c m
.77m n
ToString77n v
(77v w
)77w x
)77x y
;77y z
var99 
sede99 
=99 
ObtenerSede99 "
(99" #
)99# $
;99$ %
string:: 

logoString:: 
=:: 
Convert::  '
.::' (
ToBase64String::( 6
(::6 7
sede::7 ;
.::; <
Logo::< @
,::@ A
$num::B C
,::C D
sede::E I
.::I J
Logo::J N
.::N O
Length::O U
)::U V
;::V W
ReportParameter;; 
logoSede;; $
=;;% &
new;;' *
ReportParameter;;+ :
(;;: ;
$str;;; E
,;;E F

logoString;;G Q
);;Q R
;;;R S
DateTime== 
fechaActual==  
===! "
DateTimeUtil==# /
.==/ 0
FechaActual==0 ;
(==; <
)==< =
;=== >
ReportParameter>> 
fechaActualSistema>> .
=>>/ 0
new>>1 4
ReportParameter>>5 D
(>>D E
$str>>E R
,>>R S
fechaActual>>T _
.>>_ `
ToString>>` h
(>>h i
)>>i j
)>>j k
;>>k l
ReportParameter@@ 
empleadoSede@@ (
=@@) *
new@@+ .
ReportParameter@@/ >
(@@> ?
$str@@? H
,@@H I
ProfileData@@J U
(@@U V
)@@V W
.@@W X
Empleado@@X `
.@@` a
NombresYApellidos@@a r
)@@r s
;@@s t
varBB 
	rptviewerBB 
=BB 
newBB 
ReportViewerBB  ,
(BB, -
)BB- .
;BB. /
	rptviewerCC 
.CC 
LocalReportCC !
.CC! "

ReportPathCC" ,
=CC- .
RequestCC/ 6
.CC6 7
MapPathCC7 >
(CC> ?
RequestCC? F
.CCF G
ApplicationPathCCG V
)CCV W
+CCX Y
$str	CCZ �
;
CC� �
	rptviewerDD 
.DD 
LocalReportDD !
.DD! "

(DD/ 0
newDD0 3
ReportParameterDD4 C
[DDC D
]DDD E
{EE 
parametroNombreEmpresaEE $
,EE$ %
parametroClienteEE% 5
,EE6 7
parametroFechaDesdeEE7 J
,EEJ K
parametroFechaHastaEEL _
,EE_ `"
parametroSaldoAnteriorEEa w
,EEw x
parametroEntregas	EEx �
,
EE� �
parametroPagos
EE� �
,
EE� �!
parametroSaldoFinal
EE� �
,
EE� �
empleadoSede
EE� �
,
EE� � 
fechaActualSistema
EE� �
,
EE� �
logoSede
EE� �
}
EE� �
)
EE� �
;
EE� �
ReportDataSourceGG "
rptdatasourceDetalladoGG 3
=GG4 5
newGG6 9
ReportDataSourceGG: J
(GGJ K
$strGGK i
,GGi j
estadoDeCuentaGGk y
.GGy z
Detalle	GGz �
)
GG� �
;
GG� �
	rptviewerII 
.II 
ProcessingModeII $
=II% &
ProcessingModeII' 5
.II5 6
LocalII6 ;
;II; <
	rptviewerJJ 
.JJ 
LocalReportJJ !
.JJ! "
DataSourcesJJ" -
.JJ- .
AddJJ. 1
(JJ1 2"
rptdatasourceDetalladoJJ2 H
)JJH I
;JJI J
	rptviewerKK 
.KK 
SizeToReportContentKK )
=KK* +
trueKK, 0
;KK0 1
	rptviewerLL 
.LL 
WidthLL 
=LL 
UnitLL "
.LL" #

PercentageLL# -
(LL- .
$numLL. 1
)LL1 2
;LL2 3
	rptviewerMM 
.MM 
HeightMM 
=MM 
UnitMM #
.MM# $

PercentageMM$ .
(MM. /
$numMM/ 2
)MM2 3
;MM3 4
ViewBagNN 
.NN 
ReportViewerNN  
=NN! "
	rptviewerNN# ,
;NN, -
returnPP 
ViewPP 
(PP 
$strPP -
)PP- .
;PP. /
}RR 	
}SS 
}TT ��
xD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Finanza\FinanzaReportes_PorCobrarPagarController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class 4
(FinanzaReportes_PorCobrarPagarController A
:B C
BaseControllerD R
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly "
IFinanzaReporte_Logica 1"
finanzaReportingLogica2 H
;H I
	protected   
readonly   $
ICentroDeAtencion_Logica   3"
centroDeAtencionLogica  4 J
;  J K
public"" 4
(FinanzaReportes_PorCobrarPagarController"" 7
(""7 8
)""8 9
:"": ;
base""< @
(""@ A
)""A B
{## 	
operacionLogica$$ 
=$$ 
Dependencia$$ )
.$$) *
Resolve$$* 1
<$$1 2
IOperacionLogica$$2 B
>$$B C
($$C D
)$$D E
;$$E F
actorNegocioLogica%% 
=%%  
Dependencia%%! ,
.%%, -
Resolve%%- 4
<%%4 5
IActorNegocioLogica%%5 H
>%%H I
(%%I J
)%%J K
;%%K L
conceptoLogica&& 
=&& 
Dependencia&& (
.&&( )
Resolve&&) 0
<&&0 1
IConceptoLogica&&1 @
>&&@ A
(&&A B
)&&B C
;&&C D"
finanzaReportingLogica'' "
=''# $
Dependencia''% 0
.''0 1
Resolve''1 8
<''8 9"
IFinanzaReporte_Logica''9 O
>''O P
(''P Q
)''Q R
;''R S"
centroDeAtencionLogica(( "
=((# $
Dependencia((% 0
.((0 1
Resolve((1 8
<((8 9$
ICentroDeAtencion_Logica((9 Q
>((Q R
(((R S
)((S T
;((T U
})) 	
public++ 
ActionResult++ ,
 PorCobrarPagar_PorCobrarClientes++ <
(++< =
)++= >
{,, 	
DateTime-- 
fechaActual--  
=--! "
DateTimeUtil--# /
.--/ 0
FechaActual--0 ;
(--; <
)--< =
;--= >
fechaActual.. 
=.. 
fechaActual.. %
...% &
AddMilliseconds..& 5
(..5 6
-..6 7
fechaActual..7 B
...B C
Millisecond..C N
)..N O
;..O P
List00 
<00 
Cuota00 
>00 
DeudasDeCliente00 '
=00( )
operacionLogica00* 9
.009 :)
ObtenerReporteDeudasDeCliente00: W
(00W X
)00X Y
;00Y Z
List11 
<11 !
DeudaClienteViewModel11 &
>11& '*
reporteDeDeudaDeClienteDetalle11( F
=11G H!
DeudaClienteViewModel11I ^
.11^ _
Convert11_ f
(11f g
DeudasDeCliente11g v
)11v w
;11w x
List22 
<22 !
DeudaClienteViewModel22 &
>22& '*
reporteDeDeudaDeClienteResumen22( F
=22G H!
DeudaClienteViewModel22I ^
.22^ _
Resumen22_ f
(22f g+
reporteDeDeudaDeClienteDetalle	22g �
)
22� �
;
22� �
var44 
	rptviewer44 
=44 
new44 
ReportViewer44  ,
(44, -
)44- .
;44. /
ReportParameter55  
parametroFechaActual55 0
=551 2
new553 6
ReportParameter557 F
(55F G
$str55G T
,55T U
fechaActual55V a
.55a b
ToString55b j
(55j k
)55k l
)55l m
;55m n
ReportParameter66 

=66* +
new66, /
ReportParameter660 ?
(66? @
$str66@ O
,66O P
ObtenerSede66Q \
(66\ ]
)66] ^
.66^ _
Nombre66_ e
)66e f
;66f g
ReportParameter77  
nombreCentroAtencion77 0
=771 2
new773 6
ReportParameter777 F
(77F G
$str77G ]
,77] ^
ProfileData77_ j
(77j k
)77k l
.77l m/
"NombreCentroDeAtencionSeleccionado	77m �
)
77� �
;
77� �
var99 
sede99 
=99 
ObtenerSede99 "
(99" #
)99# $
;99$ %
string:: 

logoString:: 
=:: 
Convert::  '
.::' (
ToBase64String::( 6
(::6 7
sede::7 ;
.::; <
Logo::< @
,::@ A
$num::B C
,::C D
sede::E I
.::I J
Logo::J N
.::N O
Length::O U
)::U V
;::V W
ReportParameter;; 
logoSede;; $
=;;% &
new;;' *
ReportParameter;;+ :
(;;: ;
$str;;; E
,;;E F

logoString;;G Q
);;Q R
;;;R S
ReportParameter<< 
empleadoSede<< (
=<<) *
new<<+ .
ReportParameter<</ >
(<<> ?
$str<<? H
,<<H I
ProfileData<<J U
(<<U V
)<<V W
.<<W X
Empleado<<X `
.<<` a
NombresYApellidos<<a r
)<<r s
;<<s t
	rptviewer== 
.== 
LocalReport== !
.==! "

ReportPath==" ,
===- .
Request==/ 6
.==6 7
MapPath==7 >
(==> ?
Request==? F
.==F G
ApplicationPath==G V
)==V W
+==X Y
$str	==Z �
;
==� �
	rptviewer?? 
.?? 
LocalReport?? !
.??! "

(??/ 0
new??0 3
ReportParameter??4 C
[??C D
]??D E
{??F G 
parametroFechaActual??H \
,??\ ]

,??k l!
nombreCentroAtencion	??m �
,
??� �
logoSede
??� �
,
??� �
empleadoSede
??� �
}
??� �
)
??� �
;
??� �
ReportDataSource@@ '
rptdatasourceReporteResumen@@ 8
=@@9 :
new@@; >
ReportDataSource@@? O
(@@O P
$str@@P l
,@@l m+
reporteDeDeudaDeClienteResumen	@@n �
)
@@� �
;
@@� �
ReportDataSourceAA '
rptdatasourcereporteDetalleAA 8
=AA9 :
newAA; >
ReportDataSourceAA? O
(AAO P
$strAAP l
,AAl m+
reporteDeDeudaDeClienteDetalle	AAn �
)
AA� �
;
AA� �
	rptviewerCC 
.CC 
ProcessingModeCC $
=CC% &
ProcessingModeCC' 5
.CC5 6
LocalCC6 ;
;CC; <
	rptviewerDD 
.DD 
LocalReportDD !
.DD! "
DataSourcesDD" -
.DD- .
AddDD. 1
(DD1 2'
rptdatasourceReporteResumenDD2 M
)DDM N
;DDN O
	rptviewerEE 
.EE 
LocalReportEE !
.EE! "
DataSourcesEE" -
.EE- .
AddEE. 1
(EE1 2'
rptdatasourcereporteDetalleEE2 M
)EEM N
;EEN O
	rptviewerFF 
.FF 
SizeToReportContentFF )
=FF* +
trueFF, 0
;FF0 1
	rptviewerGG 
.GG 
WidthGG 
=GG 
UnitGG "
.GG" #

PercentageGG# -
(GG- .
$numGG. 1
)GG1 2
;GG2 3
	rptviewerHH 
.HH 
HeightHH 
=HH 
UnitHH #
.HH# $

PercentageHH$ .
(HH. /
$numHH/ 2
)HH2 3
;HH3 4
ViewBagII 
.II 
ReportViewerII  
=II! "
	rptviewerII# ,
;II, -
returnJJ 
ViewJJ 
(JJ 
$strJJ -
)JJ- .
;JJ. /
}KK 	
publicMM 
ActionResultMM .
"PorCobrarPagar_PorPagarProveedoresMM >
(MM> ?
)MM? @
{NN 	
DateTimeOO 
fechaActualOO  
=OO! "
DateTimeUtilOO# /
.OO/ 0
FechaActualOO0 ;
(OO; <
)OO< =
;OO= >
fechaActualPP 
=PP 
fechaActualPP %
.PP% &
AddMillisecondsPP& 5
(PP5 6
-PP6 7
fechaActualPP7 B
.PPB C
MillisecondPPC N
)PPN O
;PPO P
ListRR 
<RR 
CuotaRR 
>RR 
DeudasAProveedorRR (
=RR) *
operacionLogicaRR+ :
.RR: ;*
ObtenerReporteDeudasAProveedorRR; Y
(RRY Z
)RRZ [
;RR[ \
ListSS 
<SS #
DeudaProveedorViewModelSS (
>SS( )+
reporteDeDeudaAProveedorDetalleSS* I
=SSJ K#
DeudaProveedorViewModelSSL c
.SSc d
ConvertSSd k
(SSk l
DeudasAProveedorSSl |
)SS| }
;SS} ~
ListTT 
<TT #
DeudaProveedorViewModelTT (
>TT( )+
reporteDeDeudaAProveedorResumenTT* I
=TTJ K#
DeudaProveedorViewModelTTL c
.TTc d
ResumenTTd k
(TTk l,
reporteDeDeudaAProveedorDetalle	TTl �
)
TT� �
;
TT� �
varVV 
	rptviewerVV 
=VV 
newVV 
ReportViewerVV  ,
(VV, -
)VV- .
;VV. /
ReportParameterWW  
parametroFechaActualWW 0
=WW1 2
newWW3 6
ReportParameterWW7 F
(WWF G
$strWWG T
,WWT U
fechaActualWWV a
.WWa b
ToStringWWb j
(WWj k
)WWk l
)WWl m
;WWm n
ReportParameterXX 

=XX* +
newXX, /
ReportParameterXX0 ?
(XX? @
$strXX@ O
,XXO P
ObtenerSedeXXQ \
(XX\ ]
)XX] ^
.XX^ _
NombreXX_ e
)XXe f
;XXf g
ReportParameterYY  
nombreCentroAtencionYY 0
=YY1 2
newYY3 6
ReportParameterYY7 F
(YYF G
$strYYG ]
,YY] ^
ProfileDataYY_ j
(YYj k
)YYk l
.YYl m/
"NombreCentroDeAtencionSeleccionado	YYm �
)
YY� �
;
YY� �
var[[ 
sede[[ 
=[[ 
ObtenerSede[[ "
([[" #
)[[# $
;[[$ %
string\\ 

logoString\\ 
=\\ 
Convert\\  '
.\\' (
ToBase64String\\( 6
(\\6 7
sede\\7 ;
.\\; <
Logo\\< @
,\\@ A
$num\\B C
,\\C D
sede\\E I
.\\I J
Logo\\J N
.\\N O
Length\\O U
)\\U V
;\\V W
ReportParameter]] 
logoSede]] $
=]]% &
new]]' *
ReportParameter]]+ :
(]]: ;
$str]]; E
,]]E F

logoString]]G Q
)]]Q R
;]]R S
ReportParameter^^ 
empleadoSede^^ (
=^^) *
new^^+ .
ReportParameter^^/ >
(^^> ?
$str^^? H
,^^H I
ProfileData^^J U
(^^U V
)^^V W
.^^W X
Empleado^^X `
.^^` a
NombresYApellidos^^a r
)^^r s
;^^s t
	rptviewer__ 
.__ 
LocalReport__ !
.__! "

ReportPath__" ,
=__- .
Request__/ 6
.__6 7
MapPath__7 >
(__> ?
Request__? F
.__F G
ApplicationPath__G V
)__V W
+__X Y
$str	__Z �
;
__� �
	rptviewer`` 
.`` 
LocalReport`` !
.``! "

(``/ 0
new``0 3
ReportParameter``4 C
[``C D
]``D E
{``F G 
parametroFechaActual``H \
,``\ ]

,``k l!
nombreCentroAtencion	``m �
,
``� �
empleadoSede
``� �
,
``� �
logoSede
``� �
}
``� �
)
``� �
;
``� �
ReportDataSourceaa '
rptdatasourceReporteResumenaa 8
=aa9 :
newaa; >
ReportDataSourceaa? O
(aaO P
$straaP n
,aan o,
reporteDeDeudaAProveedorResumen	aap �
)
aa� �
;
aa� �
ReportDataSourcebb '
rptdatasourcereporteDetallebb 8
=bb9 :
newbb; >
ReportDataSourcebb? O
(bbO P
$strbbP n
,bbn o,
reporteDeDeudaAProveedorDetalle	bbp �
)
bb� �
;
bb� �
	rptviewerdd 
.dd 
ProcessingModedd $
=dd% &
ProcessingModedd' 5
.dd5 6
Localdd6 ;
;dd; <
	rptvieweree 
.ee 
LocalReportee !
.ee! "
DataSourcesee" -
.ee- .
Addee. 1
(ee1 2'
rptdatasourceReporteResumenee2 M
)eeM N
;eeN O
	rptviewerff 
.ff 
LocalReportff !
.ff! "
DataSourcesff" -
.ff- .
Addff. 1
(ff1 2'
rptdatasourcereporteDetalleff2 M
)ffM N
;ffN O
	rptviewergg 
.gg 
SizeToReportContentgg )
=gg* +
truegg, 0
;gg0 1
	rptviewerhh 
.hh 
Widthhh 
=hh 
Unithh "
.hh" #

Percentagehh# -
(hh- .
$numhh. 1
)hh1 2
;hh2 3
	rptviewerii 
.ii 
Heightii 
=ii 
Unitii #
.ii# $

Percentageii$ .
(ii. /
$numii/ 2
)ii2 3
;ii3 4
ViewBagjj 
.jj 
ReportViewerjj  
=jj! "
	rptviewerjj# ,
;jj, -
returnkk 
Viewkk 
(kk 
$strkk -
)kk- .
;kk. /
}ll 	
publicnn 
ActionResultnn .
"PorCobrarPagar_PorCobrarPorClientenn >
(nn> ?
boolnn? C
todosLosClientesnnD T
,nnT U
[nnV W
SystemnnW ]
.nn] ^
Webnn^ a
.nna b
Httpnnb f
.nnf g
FromUrinng n
]nnn o
intnnp s
[nns t
]nnt u
idsClientes	nnv �
,
nn� �
string
nn� �
nombresClientes
nn� �
)
nn� �
{oo 	
varpp 
	rptviewerpp 
=pp 5
)GenerarPorCobrarPagar_PorCobrarPorClientepp E
(ppE F
todosLosClientesppF V
,ppV W
idsClientesppX c
,ppc d
nombresClientesppe t
,ppt u
trueppv z
)ppz {
;pp{ |
ViewBagqq 
.qq 
ReportViewerqq  
=qq! "
	rptviewerqq# ,
;qq, -
returnrr 
Viewrr 
(rr 
$strrr -
)rr- .
;rr. /
}ss 	
publicuu 
ReportVieweruu 5
)GenerarPorCobrarPagar_PorCobrarPorClienteuu E
(uuE F
booluuF J
todosLosClientesuuK [
,uu[ \
intuu] `
[uu` a
]uua b
idsClientesuuc n
,uun o
stringuup v
nombresClientes	uuw �
,
uu� �
bool
uu� �
fromRequest
uu� �
)
uu� �
{vv 	
tryww 
{xx 
varyy 
detalladoDeudasyy #
=yy$ %
operacionLogicayy& 5
.yy5 6#
ObtenerDeudasDeClientesyy6 M
(yyM N
todosLosClientesyyN ^
,yy^ _
idsClientesyy` k
)yyk l
;yyl m
varzz 

=zz" #

.zz1 2
Resumenzz2 9
(zz9 :
detalladoDeudaszz: I
)zzI J
;zzJ K
var{{ 
parametrosBasicos{{ %
={{& '$
ObtenerParametrosBasicos{{( @
({{@ A
){{A B
;{{B C
ReportParameter|| 
nombreDeReporte||  /
=||0 1
new||2 5
ReportParameter||6 E
(||E F
$str||F U
,||U V
$str||W p
)||p q
;||q r
var}} 
	rptviewer}} 
=}} 
new}}  #
ReportViewer}}$ 0
(}}0 1
)}}1 2
{}}3 4
ProcessingMode}}5 C
=}}D E
ProcessingMode}}F T
.}}T U
Local}}U Z
,}}Z [
SizeToReportContent}}\ o
=}}p q
true}}r v
,}}v w
Width}}x }
=}}~ 
Unit
}}� �
.
}}� �

Percentage
}}� �
(
}}� �
$num
}}� �
)
}}� �
,
}}� �
Height
}}� �
=
}}� �
Unit
}}� �
.
}}� �

Percentage
}}� �
(
}}� �
$num
}}� �
)
}}� �
}
}}� �
;
}}� �
string~~ 
path~~ 
=~~ 
$str~~ [
;~~[ \
	rptviewer 
. 
LocalReport %
.% &

ReportPath& 0
=1 2
fromRequest3 >
?? @
RequestA H
.H I
MapPathI P
(P Q
RequestQ X
.X Y
ApplicationPathY h
)h i
+j k
pathl p
:q r
HostingEnvironment	s �
.
� �
MapPath
� �
(
� �
path
� �
)
� �
;
� �
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

��& 3
(
��3 4
new
��4 7
ReportParameter
��8 G
[
��G H
]
��H I
{
��J K
parametrosBasicos
�� !
.
��! "

NombreSede
��" ,
,
��, -
parametrosBasicos
�� !
.
��! " 
FechaActualSistema
��" 4
,
��4 5
parametrosBasicos
�� !
.
��! "
LogoSede
��" *
,
��* +
parametrosBasicos
�� !
.
��! "
Usuario
��" )
,
��) *
nombreDeReporte
�� 
}
�� 
)
�� 
;
�� 
ReportDataSource
��  (
rptdatasourceResumenDeudas
��! ;
=
��< =
new
��> A
ReportDataSource
��B R
(
��R S
$str
��S i
,
��i j

��k x
)
��x y
;
��y z
ReportDataSource
��  *
rptdatasourceDetalladoDeudas
��! =
=
��> ?
new
��@ C
ReportDataSource
��D T
(
��T U
$str
��U k
,
��k l
detalladoDeudas
��m |
)
��| }
;
��} ~
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 6(
rptdatasourceResumenDeudas
��6 P
)
��P Q
;
��Q R
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 6*
rptdatasourceDetalladoDeudas
��6 R
)
��R S
;
��S T
return
�� 
	rptviewer
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
�� !
ControllerException
�� -
(
��- .
$str
��. b
,
��b c
e
��d e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 
ActionResult
�� 1
#PorCobrarPagar_PorPagarPorProveedor
�� ?
(
��? @
bool
��@ D!
todosLosProveedores
��E X
,
��X Y
[
��Z [
System
��[ a
.
��a b
Web
��b e
.
��e f
Http
��f j
.
��j k
FromUri
��k r
]
��r s
int
��t w
[
��w x
]
��x y
idsProveedores��z �
,��� �
string��� �"
nombresProveedores��� �
)��� �
{
�� 	
var
�� 
	rptviewer
�� 
=
�� 8
*GenerarPorCobrarPagar_PorPagarPorProveedor
�� F
(
��F G!
todosLosProveedores
��G Z
,
��Z [
idsProveedores
��\ j
,
��j k 
nombresProveedores
��l ~
,
��~ 
true��� �
)��� �
;��� �
ViewBag
�� 
.
�� 
ReportViewer
��  
=
��! "
	rptviewer
��# ,
;
��, -
return
�� 
View
�� 
(
�� 
$str
�� -
)
��- .
;
��. /
}
�� 	
public
�� 
ReportViewer
�� 8
*GenerarPorCobrarPagar_PorPagarPorProveedor
�� F
(
��F G
bool
��G K!
todosLosProveedores
��L _
,
��_ `
int
��a d
[
��d e
]
��e f
idsProveedores
��g u
,
��u v
string
��w }!
nombresProveedores��~ �
,��� �
bool��� �
fromRequest��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
detalladoDeudas
�� #
=
��$ %
operacionLogica
��& 5
.
��5 6%
ObtenerDeudasDeClientes
��6 M
(
��M N!
todosLosProveedores
��N a
,
��a b
idsProveedores
��c q
)
��q r
;
��r s
var
�� 

�� !
=
��" #

��$ 1
.
��1 2
Resumen
��2 9
(
��9 :
detalladoDeudas
��: I
)
��I J
;
��J K
var
�� 
parametrosBasicos
�� %
=
��& '&
ObtenerParametrosBasicos
��( @
(
��@ A
)
��A B
;
��B C
ReportParameter
�� 
nombreDeReporte
��  /
=
��0 1
new
��2 5
ReportParameter
��6 E
(
��E F
$str
��F U
,
��U V
$str
��W r
)
��r s
;
��s t
var
�� 
	rptviewer
�� 
=
�� 
new
��  #
ReportViewer
��$ 0
(
��0 1
)
��1 2
{
��3 4
ProcessingMode
��5 C
=
��D E
ProcessingMode
��F T
.
��T U
Local
��U Z
,
��Z [!
SizeToReportContent
��\ o
=
��p q
true
��r v
,
��v w
Width
��x }
=
��~ 
Unit��� �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
,��� �
Height��� �
=��� �
Unit��� �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
}��� �
;��� �
string
�� 
path
�� 
=
�� 
$str
�� [
;
��[ \
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

ReportPath
��& 0
=
��1 2
fromRequest
��3 >
?
��? @
Request
��A H
.
��H I
MapPath
��I P
(
��P Q
Request
��Q X
.
��X Y
ApplicationPath
��Y h
)
��h i
+
��j k
path
��l p
:
��q r!
HostingEnvironment��s �
.��� �
MapPath��� �
(��� �
path��� �
)��� �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

��& 3
(
��3 4
new
��4 7
ReportParameter
��8 G
[
��G H
]
��H I
{
��J K
parametrosBasicos
�� !
.
��! "

NombreSede
��" ,
,
��, -
parametrosBasicos
�� !
.
��! " 
FechaActualSistema
��" 4
,
��4 5
parametrosBasicos
�� !
.
��! "
LogoSede
��" *
,
��* +
parametrosBasicos
�� !
.
��! "
Usuario
��" )
,
��) *
nombreDeReporte
�� 
}
�� 
)
�� 
;
�� 
ReportDataSource
��  (
rptdatasourceResumenDeudas
��! ;
=
��< =
new
��> A
ReportDataSource
��B R
(
��R S
$str
��S i
,
��i j

��k x
)
��x y
;
��y z
ReportDataSource
��  *
rptdatasourceDetalladoDeudas
��! =
=
��> ?
new
��@ C
ReportDataSource
��D T
(
��T U
$str
��U k
,
��k l
detalladoDeudas
��m |
)
��| }
;
��} ~
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 6(
rptdatasourceResumenDeudas
��6 P
)
��P Q
;
��Q R
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 6*
rptdatasourceDetalladoDeudas
��6 R
)
��R S
;
��S T
return
�� 
	rptviewer
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
�� !
ControllerException
�� -
(
��- .
$str
��. d
,
��d e
e
��f g
)
��g h
;
��h i
}
�� 
}
�� 	
public
�� 
ActionResult
�� ,
PorCobrarPagar_PorCobrarGrupos
�� :
(
��: ;
bool
��; ?
todosLosGrupos
��@ N
,
��N O
[
��P Q
System
��Q W
.
��W X
Web
��X [
.
��[ \
Http
��\ `
.
��` a
FromUri
��a h
]
��h i
int
��j m
[
��m n
]
��n o
	idsGrupos
��p y
,
��y z
string��{ �

)��� �
{
�� 	
var
�� 
	rptviewer
�� 
=
�� 3
%GenerarPorCobrarPagar_PorCobrarGrupos
�� A
(
��A B
todosLosGrupos
��B P
,
��P Q
	idsGrupos
��R [
,
��[ \

��] j
,
��j k
true
��l p
)
��p q
;
��q r
ViewBag
�� 
.
�� 
ReportViewer
��  
=
��! "
	rptviewer
��# ,
;
��, -
return
�� 
View
�� 
(
�� 
$str
�� -
)
��- .
;
��. /
}
�� 	
public
�� 
ReportViewer
�� 3
%GenerarPorCobrarPagar_PorCobrarGrupos
�� A
(
��A B
bool
��B F
todosLosGrupos
��G U
,
��U V
[
��W X
System
��X ^
.
��^ _
Web
��_ b
.
��b c
Http
��c g
.
��g h
FromUri
��h o
]
��o p
int
��q t
[
��t u
]
��u v
	idsGrupos��w �
,��� �
string��� �

,��� �
bool��� �
fromRequest��� �
)��� �
{
�� 	
List
�� 
<
�� 
OperacionGrupo
�� 
>
��  $
cuentasPorCobrarGrupos
��! 7
=
��8 9$
finanzaReportingLogica
��: P
.
��P Q+
ObtenerCuentasPorCobrarGrupos
��Q n
(
��n o
todosLosGrupos
��o }
,
��} ~
	idsGrupos�� �
)��� �
;��� �
var
�� 
parametrosBasicos
�� !
=
��" #&
ObtenerParametrosBasicos
��$ <
(
��< =
)
��= >
;
��> ?
ReportParameter
�� !
parametroFechaHasta
�� /
=
��0 1
new
��2 5
ReportParameter
��6 E
(
��E F
$str
��F R
,
��R S
DateTimeUtil
��T `
.
��` a
FechaActual
��a l
(
��l m
)
��m n
.
��n o
ToString
��o w
(
��w x
)
��x y
)
��y z
;
��z {
ReportParameter
�� $
parametroNombresGrupos
�� 2
=
��3 4
new
��5 8
ReportParameter
��9 H
(
��H I
$str
��I X
,
��X Y

��Z g
??
��h j
$str
��k r
)
��r s
;
��s t
var
�� 
	rptviewer
�� 
=
�� 
new
�� 
ReportViewer
��  ,
(
��, -
)
��- .
{
��/ 0
ProcessingMode
��1 ?
=
��@ A
ProcessingMode
��B P
.
��P Q
Local
��Q V
,
��V W!
SizeToReportContent
��X k
=
��l m
true
��n r
,
��r s
Width
��t y
=
��z {
Unit��| �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
,��� �
Height��� �
=��� �
Unit��� �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
}��� �
;��� �
string
�� 
path
�� 
=
�� 
$str
�� _
;
��_ `
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "

ReportPath
��" ,
=
��- .
fromRequest
��/ :
?
��; <
Request
��= D
.
��D E
MapPath
��E L
(
��L M
Request
��M T
.
��T U
ApplicationPath
��U d
)
��d e
+
��f g
path
��h l
:
��m n!
HostingEnvironment��o �
.��� �
MapPath��� �
(��� �
path��� �
)��� �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "

��" /
(
��/ 0
new
��0 3
ReportParameter
��4 C
[
��C D
]
��D E
{
��F G
parametrosBasicos
�� !
.
��! "

NombreSede
��" ,
,
��, -
parametrosBasicos
�� !
.
��! " 
FechaActualSistema
��" 4
,
��4 5
parametrosBasicos
�� !
.
��! "
LogoSede
��" *
,
��* +
parametrosBasicos
�� !
.
��! "
Usuario
��" )
,
��) *!
parametroFechaHasta
�� #
,
��# $$
parametroNombresGrupos
�� &
}
�� 
)
��
;
�� 
ReportDataSource
�� %
rptdatasourceFacturadas
�� 4
=
��5 6
new
��7 :
ReportDataSource
��; K
(
��K L
$str
��L k
,
��k l%
cuentasPorCobrarGrupos��m �
)��� �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "
DataSources
��" -
.
��- .
Add
��. 1
(
��1 2%
rptdatasourceFacturadas
��2 I
)
��I J
;
��J K
return
�� 
	rptviewer
�� 
;
�� 
}
�� 	
public
�� 
ActionResult
�� 4
&PorCobrarPagar_PorCobrarGrupoDetallado
�� B
(
��B C
int
��C F
idGrupo
��G N
,
��N O
string
��P V
nombreGrupo
��W b
)
��b c
{
�� 	
var
�� 
	rptviewer
�� 
=
�� ;
-GenerarPorCobrarPagar_PorCobrarGrupoDetallado
�� I
(
��I J
idGrupo
��J Q
,
��Q R
nombreGrupo
��S ^
,
��^ _
true
��` d
)
��d e
;
��e f
ViewBag
�� 
.
�� 
ReportViewer
��  
=
��! "
	rptviewer
��# ,
;
��, -
return
�� 
View
�� 
(
�� 
$str
�� -
)
��- .
;
��. /
}
�� 	
public
�� 
ReportViewer
�� ;
-GenerarPorCobrarPagar_PorCobrarGrupoDetallado
�� I
(
��I J
int
��J M
idGrupo
��N U
,
��U V
string
��W ]
nombreGrupo
��^ i
,
��i j
bool
��k o
fromRequest
��p {
)
��{ |
{
�� 	
List
�� 
<
�� %
OperacionGrupoDetallado
�� (
>
��( ),
cuentasPorCobrarGrupoDetallado
��* H
=
��I J$
finanzaReportingLogica
��K a
.
��a b4
%ObtenerCuentasPorCobrarGrupoDetallado��b �
(��� �
idGrupo��� �
)��� �
;��� �
var
�� 
parametrosBasicos
�� !
=
��" #&
ObtenerParametrosBasicos
��$ <
(
��< =
)
��= >
;
��> ?
ReportParameter
�� !
parametroFechaHasta
�� /
=
��0 1
new
��2 5
ReportParameter
��6 E
(
��E F
$str
��F R
,
��R S
DateTimeUtil
��T `
.
��` a
FechaActual
��a l
(
��l m
)
��m n
.
��n o
ToString
��o w
(
��w x
)
��x y
)
��y z
;
��z {
ReportParameter
�� "
parametroNombreGrupo
�� 0
=
��1 2
new
��3 6
ReportParameter
��7 F
(
��F G
$str
��G T
,
��T U
nombreGrupo
��V a
)
��a b
;
��b c
ReportParameter
�� (
parametroNombreResponsable
�� 6
=
��7 8
new
��9 <
ReportParameter
��= L
(
��L M
$str
��M `
,
��` a-
cuentasPorCobrarGrupoDetallado��b �
.��� �
FirstOrDefault��� �
(��� �
)��� �
.��� �!
NombreResponsable��� �
)��� �
;��� �
var
�� 
	rptviewer
�� 
=
�� 
new
�� 
ReportViewer
��  ,
(
��, -
)
��- .
{
��/ 0
ProcessingMode
��1 ?
=
��@ A
ProcessingMode
��B P
.
��P Q
Local
��Q V
,
��V W!
SizeToReportContent
��X k
=
��l m
true
��n r
,
��r s
Width
��t y
=
��z {
Unit��| �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
,��� �
Height��� �
=��� �
Unit��� �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
}��� �
;��� �
string
�� 
path
�� 
=
�� 
$str
�� g
;
��g h
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "

ReportPath
��" ,
=
��- .
fromRequest
��/ :
?
��; <
Request
��= D
.
��D E
MapPath
��E L
(
��L M
Request
��M T
.
��T U
ApplicationPath
��U d
)
��d e
+
��f g
path
��h l
:
��m n!
HostingEnvironment��o �
.��� �
MapPath��� �
(��� �
path��� �
)��� �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "

��" /
(
��/ 0
new
��0 3
ReportParameter
��4 C
[
��C D
]
��D E
{
��F G
parametrosBasicos
�� !
.
��! "

NombreSede
��" ,
,
��, -
parametrosBasicos
�� !
.
��! " 
FechaActualSistema
��" 4
,
��4 5
parametrosBasicos
�� !
.
��! "
LogoSede
��" *
,
��* +
parametrosBasicos
�� !
.
��! "
Usuario
��" )
,
��) *!
parametroFechaHasta
�� #
,
��# $"
parametroNombreGrupo
�� $
,
��$ %(
parametroNombreResponsable
�� *
}
�� 
)
��
;
�� 
ReportDataSource
�� %
rptdatasourceFacturadas
�� 4
=
��5 6
new
��7 :
ReportDataSource
��; K
(
��K L
$str
��L s
,
��s t-
cuentasPorCobrarGrupoDetallado��u �
)��� �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "
DataSources
��" -
.
��- .
Add
��. 1
(
��1 2%
rptdatasourceFacturadas
��2 I
)
��I J
;
��J K
return
�� 
	rptviewer
�� 
;
�� 
}
�� 	
}
�� 
}�� ��
zD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Finanza\FinanzaReportes_MovimientosCajasController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class 6
*FinanzaReportes_MovimientosCajasController C
:D E
BaseControllerF T
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly "
IFinanzaReporte_Logica 1"
finanzaReportingLogica2 H
;H I
	protected   
readonly   $
ICentroDeAtencion_Logica   3"
centroDeAtencionLogica  4 J
;  J K
public"" 6
*FinanzaReportes_MovimientosCajasController"" 9
(""9 :
)"": ;
:""< =
base""> B
(""B C
)""C D
{## 	
operacionLogica$$ 
=$$ 
Dependencia$$ )
.$$) *
Resolve$$* 1
<$$1 2
IOperacionLogica$$2 B
>$$B C
($$C D
)$$D E
;$$E F
actorNegocioLogica%% 
=%%  
Dependencia%%! ,
.%%, -
Resolve%%- 4
<%%4 5
IActorNegocioLogica%%5 H
>%%H I
(%%I J
)%%J K
;%%K L
conceptoLogica&& 
=&& 
Dependencia&& (
.&&( )
Resolve&&) 0
<&&0 1
IConceptoLogica&&1 @
>&&@ A
(&&A B
)&&B C
;&&C D"
finanzaReportingLogica'' "
=''# $
Dependencia''% 0
.''0 1
Resolve''1 8
<''8 9"
IFinanzaReporte_Logica''9 O
>''O P
(''P Q
)''Q R
;''R S"
centroDeAtencionLogica(( "
=((# $
Dependencia((% 0
.((0 1
Resolve((1 8
<((8 9$
ICentroDeAtencion_Logica((9 Q
>((Q R
(((R S
)((S T
;((T U
})) 	
public** 
ActionResult** +
MovimientosCajas_CobrosClientes** ;
(**; <
DateTime**< D

fechaDesde**E O
,**O P
DateTime**Q Y

fechaHasta**Z d
,**d e
bool**f j

,**x y
[**z {
System	**{ �
.
**� �
Web
**� �
.
**� �
Http
**� �
.
**� �
FromUri
**� �
]
**� �
int
**� �
[
**� �
]
**� �
idsCajas
**� �
,
**� �
string
**� �
nombresCajas
**� �
,
**� �
bool
**� �
todosLosClientes
**� �
,
**� �
[
**� �
System
**� �
.
**� �
Web
**� �
.
**� �
Http
**� �
.
**� �
FromUri
**� �
]
**� �
int
**� �
[
**� �
]
**� �
idsClientes
**� �
,
**� �
string
**� �
nombresClientes
**� �
)
**� �
{++ 	
var,, 
	rptviewer,, 
=,, 2
&GenerarMovimientosCajas_CobrosClientes,, B
(,,B C

fechaDesde,,C M
,,,M N

fechaHasta,,O Y
,,,Y Z

,,,h i
idsCajas,,j r
,,,r s
nombresCajas	,,t �
,
,,� �
todosLosClientes
,,� �
,
,,� �
idsClientes
,,� �
,
,,� �
nombresClientes
,,� �
,
,,� �
true
,,� �
)
,,� �
;
,,� �
ViewBag-- 
.-- 
ReportViewer--  
=--! "
	rptviewer--# ,
;--, -
return.. 
View.. 
(.. 
$str.. -
)..- .
;... /
}// 	
public11 
ReportViewer11 2
&GenerarMovimientosCajas_CobrosClientes11 B
(11B C
DateTime11C K

fechaDesde11L V
,11V W
DateTime11X `

fechaHasta11a k
,11k l
bool11m q

,	11 �
int
11� �
[
11� �
]
11� �
idsCajas
11� �
,
11� �
string
11� �
nombresCajas
11� �
,
11� �
bool
11� �
todosLosClientes
11� �
,
11� �
int
11� �
[
11� �
]
11� �
idsClientes
11� �
,
11� �
string
11� �
nombresClientes
11� �
,
11� �
bool
11� �
fromRequest
11� �
)
11� �
{22 	
try33 
{44 
var55 
detalladoPagos55 "
=55# $
operacionLogica55% 4
.554 5"
ObtenerPagosDeClientes555 K
(55K L

fechaDesde55L V
,55V W

fechaHasta55X b
,55b c

,55q r
idsCajas55s {
,55{ |
todosLosClientes	55} �
,
55� �
idsClientes
55� �
)
55� �
;
55� �
var66 
resumenPagos66  
=66! "
Reporte_Pago66# /
.66/ 0
Resumen660 7
(667 8
detalladoPagos668 F
)66F G
;66G H
var77 
parametrosBasicos77 %
=77& '$
ObtenerParametrosBasicos77( @
(77@ A
)77A B
;77B C
ReportParameter88 
nombreDeReporte88  /
=880 1
new882 5
ReportParameter886 E
(88E F
$str88F U
,88U V
$str88W k
)88k l
;88l m
ReportParameter99 
parametroFechaDesde99  3
=994 5
new996 9
ReportParameter99: I
(99I J
$str99J V
,99V W

fechaDesde99X b
.99b c
ToString99c k
(99k l
)99l m
)99m n
;99n o
ReportParameter:: 
parametroFechaHasta::  3
=::4 5
new::6 9
ReportParameter::: I
(::I J
$str::J V
,::V W

fechaHasta::X b
.::b c
ToString::c k
(::k l
)::l m
)::m n
;::n o
var;; 
	rptviewer;; 
=;; 
new;;  #
ReportViewer;;$ 0
(;;0 1
);;1 2
{;;3 4
ProcessingMode;;5 C
=;;D E
ProcessingMode;;F T
.;;T U
Local;;U Z
,;;Z [
SizeToReportContent;;\ o
=;;p q
true;;r v
,;;v w
Width;;x }
=;;~ 
Unit
;;� �
.
;;� �

Percentage
;;� �
(
;;� �
$num
;;� �
)
;;� �
,
;;� �
Height
;;� �
=
;;� �
Unit
;;� �
.
;;� �

Percentage
;;� �
(
;;� �
$num
;;� �
)
;;� �
}
;;� �
;
;;� �
string<< 
path<< 
=<< 
$str<< Z
;<<Z [
	rptviewer== 
.== 
LocalReport== %
.==% &

ReportPath==& 0
===1 2
fromRequest==3 >
?==? @
Request==A H
.==H I
MapPath==I P
(==P Q
Request==Q X
.==X Y
ApplicationPath==Y h
)==h i
+==j k
path==l p
:==q r
HostingEnvironment	==s �
.
==� �
MapPath
==� �
(
==� �
path
==� �
)
==� �
;
==� �
	rptviewer>> 
.>> 
LocalReport>> %
.>>% &

(>>3 4
new>>4 7
ReportParameter>>8 G
[>>G H
]>>H I
{>>J K
parametrosBasicos?? !
.??! "

NombreSede??" ,
,??, -
parametrosBasicos@@ !
.@@! "
FechaActualSistema@@" 4
,@@4 5
parametrosBasicosAA !
.AA! "
LogoSedeAA" *
,AA* +
parametrosBasicosBB !
.BB! "
UsuarioBB" )
,BB) *
nombreDeReporteCC 
,CC  
parametroFechaDesdeDD #
,DD# $
parametroFechaHastaEE #
}FF 
)FF 
;FF 
ReportDataSourceGG  %
rptdatasourceResumenPagosGG! :
=GG; <
newGG= @
ReportDataSourceGGA Q
(GGQ R
$strGGR g
,GGg h
resumenPagosGGi u
)GGu v
;GGv w
ReportDataSourceHH  '
rptdatasourceDetalladoPagosHH! <
=HH= >
newHH? B
ReportDataSourceHHC S
(HHS T
$strHHT i
,HHi j
detalladoPagosHHk y
)HHy z
;HHz {
	rptviewerII 
.II 
LocalReportII %
.II% &
DataSourcesII& 1
.II1 2
AddII2 5
(II5 6%
rptdatasourceResumenPagosII6 O
)IIO P
;IIP Q
	rptviewerJJ 
.JJ 
LocalReportJJ %
.JJ% &
DataSourcesJJ& 1
.JJ1 2
AddJJ2 5
(JJ5 6'
rptdatasourceDetalladoPagosJJ6 Q
)JJQ R
;JJR S
returnKK 
	rptviewerKK  
;KK  !
}LL 
catchMM 
(MM 
	ExceptionMM 
eMM 
)MM 
{NN 
throwOO 
newOO 
ControllerExceptionOO -
(OO- .
$strOO. a
,OOa b
eOOc d
)OOd e
;OOe f
}PP 
}QQ 	
publicSS 
ActionResultSS -
!MovimientosCajas_PagosProveedoresSS =
(SS= >
DateTimeSS> F

fechaDesdeSSG Q
,SSQ R
DateTimeSSS [

fechaHastaSS\ f
,SSf g
boolSSh l

,SSz {
[SS| }
System	SS} �
.
SS� �
Web
SS� �
.
SS� �
Http
SS� �
.
SS� �
FromUri
SS� �
]
SS� �
int
SS� �
[
SS� �
]
SS� �
idsCajas
SS� �
,
SS� �
string
SS� �
nombresCajas
SS� �
,
SS� �
bool
SS� �!
todosLosProveedores
SS� �
,
SS� �
[
SS� �
System
SS� �
.
SS� �
Web
SS� �
.
SS� �
Http
SS� �
.
SS� �
FromUri
SS� �
]
SS� �
int
SS� �
[
SS� �
]
SS� �
idsProveedores
SS� �
,
SS� �
string
SS� � 
nombresProveedores
SS� �
)
SS� �
{TT 	
varUU 
	rptviewerUU 
=UU 4
(GenerarMovimientosCajas_PagosProveedoresUU D
(UUD E

fechaDesdeUUE O
,UUO P

fechaHastaUUQ [
,UU[ \

,UUj k
idsCajasUUl t
,UUt u
nombresCajas	UUv �
,
UU� �!
todosLosProveedores
UU� �
,
UU� �
idsProveedores
UU� �
,
UU� � 
nombresProveedores
UU� �
,
UU� �
true
UU� �
)
UU� �
;
UU� �
ViewBagVV 
.VV 
ReportViewerVV  
=VV! "
	rptviewerVV# ,
;VV, -
returnWW 
ViewWW 
(WW 
$strWW -
)WW- .
;WW. /
}XX 	
publicZZ 
ReportViewerZZ 4
(GenerarMovimientosCajas_PagosProveedoresZZ D
(ZZD E
DateTimeZZE M

fechaDesdeZZN X
,ZZX Y
DateTimeZZZ b

fechaHastaZZc m
,ZZm n
boolZZo s

,
ZZ� �
int
ZZ� �
[
ZZ� �
]
ZZ� �
idsCajas
ZZ� �
,
ZZ� �
string
ZZ� �
nombresCajas
ZZ� �
,
ZZ� �
bool
ZZ� �!
todosLosProveedores
ZZ� �
,
ZZ� �
int
ZZ� �
[
ZZ� �
]
ZZ� �
idsProveedores
ZZ� �
,
ZZ� �
string
ZZ� � 
nombresProveedores
ZZ� �
,
ZZ� �
bool
ZZ� �
fromRequest
ZZ� �
)
ZZ� �
{[[ 	
try\\ 
{]] 
var^^ 
detalladoPagos^^ "
=^^# $
operacionLogica^^% 4
.^^4 5$
ObtenerPagosAProveedores^^5 M
(^^M N

fechaDesde^^N X
,^^X Y

fechaHasta^^Z d
,^^d e

,^^s t
idsCajas^^u }
,^^} ~ 
todosLosProveedores	^^ �
,
^^� �
idsProveedores
^^� �
)
^^� �
;
^^� �
var__ 
resumenPagos__  
=__! "
Reporte_Pago__# /
.__/ 0
Resumen__0 7
(__7 8
detalladoPagos__8 F
)__F G
;__G H
var`` 
parametrosBasicos`` %
=``& '$
ObtenerParametrosBasicos``( @
(``@ A
)``A B
;``B C
ReportParameteraa 
nombreDeReporteaa  /
=aa0 1
newaa2 5
ReportParameteraa6 E
(aaE F
$straaF U
,aaU V
$straaW m
)aam n
;aan o
ReportParameterbb 
parametroFechaDesdebb  3
=bb4 5
newbb6 9
ReportParameterbb: I
(bbI J
$strbbJ V
,bbV W

fechaDesdebbX b
.bbb c
ToStringbbc k
(bbk l
)bbl m
)bbm n
;bbn o
ReportParametercc 
parametroFechaHastacc  3
=cc4 5
newcc6 9
ReportParametercc: I
(ccI J
$strccJ V
,ccV W

fechaHastaccX b
.ccb c
ToStringccc k
(cck l
)ccl m
)ccm n
;ccn o
vardd 
	rptviewerdd 
=dd 
newdd  #
ReportViewerdd$ 0
(dd0 1
)dd1 2
{dd3 4
ProcessingModedd5 C
=ddD E
ProcessingModeddF T
.ddT U
LocalddU Z
,ddZ [
SizeToReportContentdd\ o
=ddp q
trueddr v
,ddv w
Widthddx }
=dd~ 
Unit
dd� �
.
dd� �

Percentage
dd� �
(
dd� �
$num
dd� �
)
dd� �
,
dd� �
Height
dd� �
=
dd� �
Unit
dd� �
.
dd� �

Percentage
dd� �
(
dd� �
$num
dd� �
)
dd� �
}
dd� �
;
dd� �
stringee 
pathee 
=ee 
$stree Z
;eeZ [
	rptviewerff 
.ff 
LocalReportff %
.ff% &

ReportPathff& 0
=ff1 2
fromRequestff3 >
?ff? @
RequestffA H
.ffH I
MapPathffI P
(ffP Q
RequestffQ X
.ffX Y
ApplicationPathffY h
)ffh i
+ffj k
pathffl p
:ffq r
HostingEnvironment	ffs �
.
ff� �
MapPath
ff� �
(
ff� �
path
ff� �
)
ff� �
;
ff� �
	rptviewergg 
.gg 
LocalReportgg %
.gg% &

(gg3 4
newgg4 7
ReportParametergg8 G
[ggG H
]ggH I
{ggJ K
parametrosBasicoshh !
.hh! "

NombreSedehh" ,
,hh, -
parametrosBasicosii !
.ii! "
FechaActualSistemaii" 4
,ii4 5
parametrosBasicosjj !
.jj! "
LogoSedejj" *
,jj* +
parametrosBasicoskk !
.kk! "
Usuariokk" )
,kk) *
nombreDeReportell 
,ll  
parametroFechaDesdemm #
,mm# $
parametroFechaHastann #
}oo 
)oo 
;oo 
ReportDataSourcepp  %
rptdatasourceResumenPagospp! :
=pp; <
newpp= @
ReportDataSourceppA Q
(ppQ R
$strppR g
,ppg h
resumenPagosppi u
)ppu v
;ppv w
ReportDataSourceqq  '
rptdatasourceDetalladoPagosqq! <
=qq= >
newqq? B
ReportDataSourceqqC S
(qqS T
$strqqT i
,qqi j
detalladoPagosqqk y
)qqy z
;qqz {
	rptviewerrr 
.rr 
LocalReportrr %
.rr% &
DataSourcesrr& 1
.rr1 2
Addrr2 5
(rr5 6%
rptdatasourceResumenPagosrr6 O
)rrO P
;rrP Q
	rptviewerss 
.ss 
LocalReportss %
.ss% &
DataSourcesss& 1
.ss1 2
Addss2 5
(ss5 6'
rptdatasourceDetalladoPagosss6 Q
)ssQ R
;ssR S
returntt 
	rptviewertt  
;tt  !
}uu 
catchvv 
(vv 
	Exceptionvv 
evv 
)vv 
{ww 
throwxx 
newxx 
ControllerExceptionxx -
(xx- .
$strxx. c
,xxc d
exxe f
)xxf g
;xxg h
}yy 
}zz 	
}
�� 	
}
�� ��
yD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Finanza\FinanzaReportes_MovimientosCajaController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class 5
)FinanzaReportes_MovimientosCajaController B
:C D
BaseControllerE S
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly "
IFinanzaReporte_Logica 1"
finanzaReportingLogica2 H
;H I
	protected   
readonly   $
ICentroDeAtencion_Logica   3"
centroDeAtencionLogica  4 J
;  J K
public## 5
)FinanzaReportes_MovimientosCajaController## 8
(##8 9
)##9 :
:##; <
base##= A
(##A B
)##B C
{$$ 	
operacionLogica%% 
=%% 
Dependencia%% )
.%%) *
Resolve%%* 1
<%%1 2
IOperacionLogica%%2 B
>%%B C
(%%C D
)%%D E
;%%E F
actorNegocioLogica&& 
=&&  
Dependencia&&! ,
.&&, -
Resolve&&- 4
<&&4 5
IActorNegocioLogica&&5 H
>&&H I
(&&I J
)&&J K
;&&K L
conceptoLogica'' 
='' 
Dependencia'' (
.''( )
Resolve'') 0
<''0 1
IConceptoLogica''1 @
>''@ A
(''A B
)''B C
;''C D"
finanzaReportingLogica(( "
=((# $
Dependencia((% 0
.((0 1
Resolve((1 8
<((8 9"
IFinanzaReporte_Logica((9 O
>((O P
(((P Q
)((Q R
;((R S"
centroDeAtencionLogica)) "
=))# $
Dependencia))% 0
.))0 1
Resolve))1 8
<))8 9$
ICentroDeAtencion_Logica))9 Q
>))Q R
())R S
)))S T
;))T U
}++ 	
public-- 
ActionResult-- $
MovimientosCaja_Ingresos-- 4
(--4 5
bool--5 9
esCuenta--: B
,--B C
int--D G
idCaja--H N
,--N O
string--P V

nombreCaja--W a
,--a b
DateTime--c k

fechaDesde--l v
,--v w
DateTime	--x �

fechaHasta
--� �
,
--� �
bool
--� � 
todosLosMediosPago
--� �
,
--� �
[
--� �
System
--� �
.
--� �
Web
--� �
.
--� �
Http
--� �
.
--� �
FromUri
--� �
]
--� �
int
--� �
[
--� �
]
--� �

--� �
,
--� �
string
--� �
nombresMediosPago
--� �
,
--� �
bool
--� �!
todasLasOperaciones
--� �
,
--� �
[
--� �
System
--� �
.
--� �
Web
--� �
.
--� �
Http
--� �
.
--� �
FromUri
--� �
]
--� �
int
--� �
[
--� �
]
--� �
idsOperaciones
--� �
,
--� �
string
--� � 
nombresOperaciones
--� �
)
--� �
{.. 	
var// 
	rptviewer// 
=// +
GenerarMovimientosCaja_Ingresos// ;
(//; <
esCuenta//< D
,//D E
idCaja//F L
,//L M

nombreCaja//N X
,//X Y

fechaDesde//Z d
,//d e

fechaHasta//f p
,//p q
todosLosMediosPago	//r �
,
//� �

//� �
,
//� �
nombresMediosPago
//� �
,
//� �!
todasLasOperaciones
//� �
,
//� �
idsOperaciones
//� �
,
//� � 
nombresOperaciones
//� �
,
//� �
true
//� �
)
//� �
;
//� �
ViewBag00 
.00 
ReportViewer00  
=00! "
	rptviewer00# ,
;00, -
return11 
View11 
(11 
$str11 -
)11- .
;11. /
}22 	
public44 
ReportViewer44 +
GenerarMovimientosCaja_Ingresos44 ;
(44; <
bool44< @
esCuenta44A I
,44I J
int44K N
idCaja44O U
,44U V
string44W ]

nombreCaja44^ h
,44h i
DateTime44j r

fechaDesde44s }
,44} ~
DateTime	44 �

fechaHasta
44� �
,
44� �
bool
44� � 
todosLosMediosPago
44� �
,
44� �
[
44� �
System
44� �
.
44� �
Web
44� �
.
44� �
Http
44� �
.
44� �
FromUri
44� �
]
44� �
int
44� �
[
44� �
]
44� �

44� �
,
44� �
string
44� �
nombresMediosPago
44� �
,
44� �
bool
44� �!
todasLasOperaciones
44� �
,
44� �
[
44� �
System
44� �
.
44� �
Web
44� �
.
44� �
Http
44� �
.
44� �
FromUri
44� �
]
44� �
int
44� �
[
44� �
]
44� �
idsOperaciones
44� �
,
44� �
string
44� � 
nombresOperaciones
44� �
,
44� �
bool
44� �
fromRequest
44� �
)
44� �
{55 	
List66 
<66 

>66 
ingresos66  (
=66) *"
finanzaReportingLogica66+ A
.66A B
Ingresos66B J
(66J K
esCuenta66K S
,66S T
idCaja66U [
,66[ \

fechaDesde66] g
,66g h

fechaHasta66i s
,66s t
todosLosMediosPago	66u �
,
66� �

66� �
,
66� �!
todasLasOperaciones
66� �
,
66� �
idsOperaciones
66� �
)
66� �
;
66� �
var77 
parametrosBasicos77 !
=77" #$
ObtenerParametrosBasicos77$ <
(77< =
)77= >
;77> ?
ReportParameter88 
parametroNombreCaja88 /
=880 1
new882 5
ReportParameter886 E
(88E F
$str88F R
,88R S

nombreCaja88T ^
)88^ _
;88_ `
ReportParameter99 
parametroMediosPago99 /
=990 1
new992 5
ReportParameter996 E
(99E F
$str99F R
,99R S
nombresMediosPago99T e
??99f h
$str99i p
)99p q
;99q r
ReportParameter::  
parametroOperaciones:: 0
=::1 2
new::3 6
ReportParameter::7 F
(::F G
$str::G T
,::T U
nombresOperaciones::V h
??::i k
$str::l s
)::s t
;::t u
ReportParameter;; 
parametroFechaDesde;; /
=;;0 1
new;;2 5
ReportParameter;;6 E
(;;E F
$str;;F R
,;;R S

fechaDesde;;T ^
.;;^ _
ToString;;_ g
(;;g h
);;h i
);;i j
;;;j k
ReportParameter<< 
parametroFechaHasta<< /
=<<0 1
new<<2 5
ReportParameter<<6 E
(<<E F
$str<<F R
,<<R S

fechaHasta<<T ^
.<<^ _
ToString<<_ g
(<<g h
)<<h i
)<<i j
;<<j k
var== 
	rptviewer== 
=== 
new== 
ReportViewer==  ,
(==, -
)==- .
{==/ 0
ProcessingMode==1 ?
===@ A
ProcessingMode==B P
.==P Q
Local==Q V
,==V W
SizeToReportContent==X k
===l m
true==n r
,==r s
Width==t y
===z {
Unit	==| �
.
==� �

Percentage
==� �
(
==� �
$num
==� �
)
==� �
,
==� �
Height
==� �
=
==� �
Unit
==� �
.
==� �

Percentage
==� �
(
==� �
$num
==� �
)
==� �
}
==� �
;
==� �
string>> 
path>> 
=>> 
$str>> Y
;>>Y Z
	rptviewer?? 
.?? 
LocalReport?? !
.??! "

ReportPath??" ,
=??- .
fromRequest??/ :
???; <
Request??= D
.??D E
MapPath??E L
(??L M
Request??M T
.??T U
ApplicationPath??U d
)??d e
+??f g
path??h l
:??m n
HostingEnvironment	??o �
.
??� �
MapPath
??� �
(
??� �
path
??� �
)
??� �
;
??� �
	rptviewer@@ 
.@@ 
LocalReport@@ !
.@@! "

(@@/ 0
new@@0 3
ReportParameter@@4 C
[@@C D
]@@D E
{@@F G
parametrosBasicosAA !
.AA! "

NombreSedeAA" ,
,AA, -
parametrosBasicosBB !
.BB! "
FechaActualSistemaBB" 4
,BB4 5
parametrosBasicosCC !
.CC! "
LogoSedeCC" *
,CC* +
parametrosBasicosDD !
.DD! "
UsuarioDD" )
,DD) *
parametroMediosPagoEE #
,EE# $ 
parametroOperacionesFF $
,FF$ %
parametroNombreCajaGG #
,GG# $
parametroFechaDesdeHH #
,HH# $
parametroFechaHastaII #
}JJ 
)JJ
;JJ 
ReportDataSourceKK !
rptdatasourceIngresosKK 2
=KK3 4
newKK5 8
ReportDataSourceKK9 I
(KKI J
$strKKJ [
,KK[ \
ingresosKK] e
)KKe f
;KKf g
	rptviewerLL 
.LL 
LocalReportLL !
.LL! "
DataSourcesLL" -
.LL- .
AddLL. 1
(LL1 2!
rptdatasourceIngresosLL2 G
)LLG H
;LLH I
returnMM 
	rptviewerMM 
;MM 
}NN 	
publicPP 
ActionResultPP #
MovimientosCaja_EgresosPP 3
(PP3 4
boolPP4 8
esCuentaPP9 A
,PPA B
intPPC F
idCajaPPG M
,PPM N
stringPPO U

nombreCajaPPV `
,PP` a
DateTimePPb j

fechaDesdePPk u
,PPu v
DateTimePPw 

fechaHasta
PP� �
,
PP� �
bool
PP� � 
todosLosMediosPago
PP� �
,
PP� �
[
PP� �
System
PP� �
.
PP� �
Web
PP� �
.
PP� �
Http
PP� �
.
PP� �
FromUri
PP� �
]
PP� �
int
PP� �
[
PP� �
]
PP� �

PP� �
,
PP� �
string
PP� �
nombresMediosPago
PP� �
,
PP� �
bool
PP� �!
todasLasOperaciones
PP� �
,
PP� �
[
PP� �
System
PP� �
.
PP� �
Web
PP� �
.
PP� �
Http
PP� �
.
PP� �
FromUri
PP� �
]
PP� �
int
PP� �
[
PP� �
]
PP� �
idsOperaciones
PP� �
,
PP� �
string
PP� � 
nombresOperaciones
PP� �
)
PP� �
{QQ 	
varRR 
	rptviewerRR 
=RR *
GenerarMovimientosCaja_EgresosRR :
(RR: ;
esCuentaRR; C
,RRC D
idCajaRRE K
,RRK L

nombreCajaRRM W
,RRW X

fechaDesdeRRY c
,RRc d

fechaHastaRRe o
,RRo p
todosLosMediosPago	RRq �
,
RR� �

RR� �
,
RR� �
nombresMediosPago
RR� �
,
RR� �!
todasLasOperaciones
RR� �
,
RR� �
idsOperaciones
RR� �
,
RR� � 
nombresOperaciones
RR� �
,
RR� �
true
RR� �
)
RR� �
;
RR� �
ViewBagSS 
.SS 
ReportViewerSS  
=SS! "
	rptviewerSS# ,
;SS, -
returnTT 
ViewTT 
(TT 
$strTT -
)TT- .
;TT. /
}UU 	
publicWW 
ReportViewerWW *
GenerarMovimientosCaja_EgresosWW :
(WW: ;
boolWW; ?
esCuentaWW@ H
,WWH I
intWWJ M
idCajaWWN T
,WWT U
stringWWV \

nombreCajaWW] g
,WWg h
DateTimeWWi q

fechaDesdeWWr |
,WW| }
DateTime	WW~ �

fechaHasta
WW� �
,
WW� �
bool
WW� � 
todosLosMediosPago
WW� �
,
WW� �
[
WW� �
System
WW� �
.
WW� �
Web
WW� �
.
WW� �
Http
WW� �
.
WW� �
FromUri
WW� �
]
WW� �
int
WW� �
[
WW� �
]
WW� �

WW� �
,
WW� �
string
WW� �
nombresMediosPago
WW� �
,
WW� �
bool
WW� �!
todasLasOperaciones
WW� �
,
WW� �
[
WW� �
System
WW� �
.
WW� �
Web
WW� �
.
WW� �
Http
WW� �
.
WW� �
FromUri
WW� �
]
WW� �
int
WW� �
[
WW� �
]
WW� �
idsOperaciones
WW� �
,
WW� �
string
WW� � 
nombresOperaciones
WW� �
,
WW� �
bool
WW� �
fromRequest
WW� �
)
WW� �
{XX 	
ListYY 
<YY 

>YY 
egresosYY  '
=YY( )"
finanzaReportingLogicaYY* @
.YY@ A
EgresosYYA H
(YYH I
esCuentaYYI Q
,YYQ R
idCajaYYS Y
,YYY Z

fechaDesdeYY[ e
,YYe f

fechaHastaYYg q
,YYq r
todosLosMediosPago	YYs �
,
YY� �

YY� �
,
YY� �!
todasLasOperaciones
YY� �
,
YY� �
idsOperaciones
YY� �
)
YY� �
;
YY� �
varZZ 
parametrosBasicosZZ !
=ZZ" #$
ObtenerParametrosBasicosZZ$ <
(ZZ< =
)ZZ= >
;ZZ> ?
ReportParameter[[ 
parametroNombreCaja[[ /
=[[0 1
new[[2 5
ReportParameter[[6 E
([[E F
$str[[F R
,[[R S

nombreCaja[[T ^
)[[^ _
;[[_ `
ReportParameter\\ 
parametroMediosPago\\ /
=\\0 1
new\\2 5
ReportParameter\\6 E
(\\E F
$str\\F R
,\\R S
nombresMediosPago\\T e
??\\f h
$str\\i p
)\\p q
;\\q r
ReportParameter]]  
parametroOperaciones]] 0
=]]1 2
new]]3 6
ReportParameter]]7 F
(]]F G
$str]]G T
,]]T U
nombresOperaciones]]V h
??]]i k
$str]]l s
)]]s t
;]]t u
ReportParameter^^ 
parametroFechaDesde^^ /
=^^0 1
new^^2 5
ReportParameter^^6 E
(^^E F
$str^^F R
,^^R S

fechaDesde^^T ^
.^^^ _
ToString^^_ g
(^^g h
)^^h i
)^^i j
;^^j k
ReportParameter__ 
parametroFechaHasta__ /
=__0 1
new__2 5
ReportParameter__6 E
(__E F
$str__F R
,__R S

fechaHasta__T ^
.__^ _
ToString___ g
(__g h
)__h i
)__i j
;__j k
var`` 
	rptviewer`` 
=`` 
new`` 
ReportViewer``  ,
(``, -
)``- .
{``/ 0
ProcessingMode``1 ?
=``@ A
ProcessingMode``B P
.``P Q
Local``Q V
,``V W
SizeToReportContent``X k
=``l m
true``n r
,``r s
Width``t y
=``z {
Unit	``| �
.
``� �

Percentage
``� �
(
``� �
$num
``� �
)
``� �
,
``� �
Height
``� �
=
``� �
Unit
``� �
.
``� �

Percentage
``� �
(
``� �
$num
``� �
)
``� �
}
``� �
;
``� �
stringaa 
pathaa 
=aa 
$straa X
;aaX Y
	rptviewerbb 
.bb 
LocalReportbb !
.bb! "

ReportPathbb" ,
=bb- .
fromRequestbb/ :
?bb; <
Requestbb= D
.bbD E
MapPathbbE L
(bbL M
RequestbbM T
.bbT U
ApplicationPathbbU d
)bbd e
+bbf g
pathbbh l
:bbm n
HostingEnvironment	bbo �
.
bb� �
MapPath
bb� �
(
bb� �
path
bb� �
)
bb� �
;
bb� �
	rptviewercc 
.cc 
LocalReportcc !
.cc! "

(cc/ 0
newcc0 3
ReportParametercc4 C
[ccC D
]ccD E
{ccF G
parametrosBasicosdd !
.dd! "

NombreSededd" ,
,dd, -
parametrosBasicosee !
.ee! "
FechaActualSistemaee" 4
,ee4 5
parametrosBasicosff !
.ff! "
LogoSedeff" *
,ff* +
parametrosBasicosgg !
.gg! "
Usuariogg" )
,gg) *
parametroNombreCajahh #
,hh# $
parametroMediosPagoii #
,ii# $ 
parametroOperacionesjj $
,jj$ %
parametroFechaDesdekk #
,kk# $
parametroFechaHastall #
}mm 
)mm
;mm 
ReportDataSourcenn  
rptdatasourceEgresosnn 1
=nn2 3
newnn4 7
ReportDataSourcenn8 H
(nnH I
$strnnI Y
,nnY Z
egresosnn[ b
)nnb c
;nnc d
	rptvieweroo 
.oo 
LocalReportoo !
.oo! "
DataSourcesoo" -
.oo- .
Addoo. 1
(oo1 2 
rptdatasourceEgresosoo2 F
)ooF G
;ooG H
returnpp 
	rptviewerpp 
;pp 
}qq 	
publicss 
ActionResultss !
MovimientosCaja_Flujoss 1
(ss1 2
boolss2 6
esCuentass7 ?
,ss? @
intssA D
idCajassE K
,ssK L
stringssM S

nombreCajassT ^
,ss^ _
DateTimess` h

fechaDesdessi s
,sss t
DateTimessu }

fechaHasta	ss~ �
,
ss� �
bool
ss� � 
todosLosMediosPago
ss� �
,
ss� �
[
ss� �
System
ss� �
.
ss� �
Web
ss� �
.
ss� �
Http
ss� �
.
ss� �
FromUri
ss� �
]
ss� �
int
ss� �
[
ss� �
]
ss� �

ss� �
,
ss� �
string
ss� �
nombresMediosPago
ss� �
)
ss� �
{tt 	
varuu 
	rptvieweruu 
=uu (
GenerarMovimientosCaja_Flujouu 8
(uu8 9
esCuentauu9 A
,uuA B
idCajauuC I
,uuI J

nombreCajauuK U
,uuU V

fechaDesdeuuW a
,uua b

fechaHastauuc m
,uum n
todosLosMediosPago	uuo �
,
uu� �

uu� �
,
uu� �
nombresMediosPago
uu� �
,
uu� �
true
uu� �
)
uu� �
;
uu� �
ViewBagvv 
.vv 
ReportViewervv  
=vv! "
	rptviewervv# ,
;vv, -
returnww 
Viewww 
(ww 
$strww -
)ww- .
;ww. /
}xx 	
publiczz 
ReportViewerzz (
GenerarMovimientosCaja_Flujozz 8
(zz8 9
boolzz9 =
esCuentazz> F
,zzF G
intzzH K
idCajazzL R
,zzR S
stringzzT Z

nombreCajazz[ e
,zze f
DateTimezzg o

fechaDesdezzp z
,zzz {
DateTime	zz| �

fechaHasta
zz� �
,
zz� �
bool
zz� � 
todosLosMediosPago
zz� �
,
zz� �
[
zz� �
System
zz� �
.
zz� �
Web
zz� �
.
zz� �
Http
zz� �
.
zz� �
FromUri
zz� �
]
zz� �
int
zz� �
[
zz� �
]
zz� �

zz� �
,
zz� �
string
zz� �
nombresMediosPago
zz� �
,
zz� �
bool
zz� �
fromRequest
zz� �
)
zz� �
{{{ 	
var|| 
flujo|| 
=|| "
finanzaReportingLogica|| .
.||. /
Flujo||/ 4
(||4 5
esCuenta||5 =
,||= >
idCaja||? E
,||E F

fechaDesde||G Q
,||Q R

fechaHasta||S ]
,||] ^
todosLosMediosPago||_ q
,||q r

)
||� �
;
||� �
var}} 
parametrosBasicos}} !
=}}" #$
ObtenerParametrosBasicos}}$ <
(}}< =
)}}= >
;}}> ?
ReportParameter~~ 
parametroNombreCaja~~ /
=~~0 1
new~~2 5
ReportParameter~~6 E
(~~E F
$str~~F R
,~~R S

nombreCaja~~T ^
)~~^ _
;~~_ `
ReportParameter 
parametroMediosPago /
=0 1
new2 5
ReportParameter6 E
(E F
$strF R
,R S
nombresMediosPagoT e
??f h
$stri p
)p q
;q r
ReportParameter
�� !
parametroFechaDesde
�� /
=
��0 1
new
��2 5
ReportParameter
��6 E
(
��E F
$str
��F R
,
��R S

fechaDesde
��T ^
.
��^ _
ToString
��_ g
(
��g h
)
��h i
)
��i j
;
��j k
ReportParameter
�� !
parametroFechaHasta
�� /
=
��0 1
new
��2 5
ReportParameter
��6 E
(
��E F
$str
��F R
,
��R S

fechaHasta
��T ^
.
��^ _
ToString
��_ g
(
��g h
)
��h i
)
��i j
;
��j k
var
�� 
	rptviewer
�� 
=
�� 
new
�� 
ReportViewer
��  ,
(
��, -
)
��- .
{
��/ 0
ProcessingMode
��1 ?
=
��@ A
ProcessingMode
��B P
.
��P Q
Local
��Q V
,
��V W!
SizeToReportContent
��X k
=
��l m
true
��n r
,
��r s
Width
��t y
=
��z {
Unit��| �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
,��� �
Height��� �
=��� �
Unit��� �
.��� �

Percentage��� �
(��� �
$num��� �
)��� �
}��� �
;��� �
string
�� 
path
�� 
=
�� 
$str
�� V
;
��V W
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "

ReportPath
��" ,
=
��- .
fromRequest
��/ :
?
��; <
Request
��= D
.
��D E
MapPath
��E L
(
��L M
Request
��M T
.
��T U
ApplicationPath
��U d
)
��d e
+
��f g
path
��h l
:
��m n!
HostingEnvironment��o �
.��� �
MapPath��� �
(��� �
path��� �
)��� �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "

��" /
(
��/ 0
new
��0 3
ReportParameter
��4 C
[
��C D
]
��D E
{
��F G
parametrosBasicos
�� !
.
��! "

NombreSede
��" ,
,
��, -
parametrosBasicos
�� !
.
��! " 
FechaActualSistema
��" 4
,
��4 5
parametrosBasicos
�� !
.
��! "
LogoSede
��" *
,
��* +
parametrosBasicos
�� !
.
��! "
Usuario
��" )
,
��) *!
parametroNombreCaja
�� #
,
��# $!
parametroMediosPago
�� #
,
��# $!
parametroFechaDesde
�� #
,
��# $!
parametroFechaHasta
�� #
}
�� 
)
��
;
�� 
ReportDataSource
�� '
rptdatasourceResumenFlujo
�� 6
=
��7 8
new
��9 <
ReportDataSource
��= M
(
��M N
$str
��N c
,
��c d
new
��e h
List
��i m
<
��m n
ResumenFlujo
��n z
>
��z {
{
��| }
flujo��~ �
.��� �
Resumen��� �
}��� �
)��� �
;��� �
ReportDataSource
�� '
rptdatasourceDetalleFlujo
�� 6
=
��7 8
new
��9 <
ReportDataSource
��= M
(
��M N
$str
��N c
,
��c d
flujo
��e j
.
��j k
Detalles
��k s
)
��s t
;
��t u
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "
DataSources
��" -
.
��- .
Add
��. 1
(
��1 2'
rptdatasourceResumenFlujo
��2 K
)
��K L
;
��L M
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "
DataSources
��" -
.
��- .
Add
��. 1
(
��1 2'
rptdatasourceDetalleFlujo
��2 K
)
��K L
;
��L M
return
�� 
	rptviewer
�� 
;
�� 
}
�� 	
}
�� 
}�� �
jD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Finanza\TesoreriaAjustesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class &
TesoreriaAjustesController 3
:4 5
BaseController6 D
{		 
	protected

 
readonly

 #
IAjusteTesoreria_Logica

 2
_ajusteTesoreria

3 C
;

C D
public &
TesoreriaAjustesController )
() *
)* +
:+ ,
base, 0
(0 1
)1 2
{ 	
_ajusteTesoreria 
= 
Dependencia *
.* +
Resolve+ 2
<2 3#
IAjusteTesoreria_Logica3 J
>J K
(K L
)L M
;M N
} 	
[ 	
	Authorize	 
( 
Roles 
= 
$str ,
), -
]- .
public 
void $
CorregirTiposTransaccion ,
(, -
)- .
{ 	
_ajusteTesoreria 
. >
2CorregirTipoTransaccionPagoEnNotasDeCreditoYDebito O
(O P
)P Q
;Q R
} 	
} 
} �S
~D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\LibrosElectronicos\LibrosElectronicosFoxcontController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{   
public!! 

partial!! 
class!! /
#LibrosElectronicosFoxcontController!! <
:!!= >
BaseController!!? M
{"" 
	protected## 
readonly## 
IConceptoLogica## *
conceptoLogica##+ 9
;##9 :
	protected$$ 
readonly$$ 
IOperacionLogica$$ +
operacionLogica$$, ;
;$$; <
	protected%% 
readonly%% 
IActorNegocioLogica%% .
actorNegocioLogica%%/ A
;%%A B
	protected&& 
readonly&& %
ILibrosElectronicosLogica&& 4$
librosElectronicosLogica&&5 M
;&&M N
	protected'' 
readonly'' ,
 ILibrosElectronicosFoxcontLogica'' ;+
librosElectronicosFoxcontLogica''< [
;''[ \/
#LibrosElectronicosGeneradorReportes(( +
	generador((, 5
=((6 7
new((8 ;/
#LibrosElectronicosGeneradorReportes((< _
(((_ `
)((` a
;((a b
public** /
#LibrosElectronicosFoxcontController** 2
(**2 3
)**3 4
:**5 6
base**7 ;
(**; <
)**< =
{++ 	
conceptoLogica,, 
=,, 
Dependencia,, (
.,,( )
Resolve,,) 0
<,,0 1
IConceptoLogica,,1 @
>,,@ A
(,,A B
),,B C
;,,C D
operacionLogica-- 
=-- 
Dependencia-- )
.--) *
Resolve--* 1
<--1 2
IOperacionLogica--2 B
>--B C
(--C D
)--D E
;--E F
actorNegocioLogica.. 
=..  
Dependencia..! ,
..., -
Resolve..- 4
<..4 5
IActorNegocioLogica..5 H
>..H I
(..I J
)..J K
;..K L$
librosElectronicosLogica// $
=//% &
Dependencia//' 2
.//2 3
Resolve//3 :
<//: ;%
ILibrosElectronicosLogica//; T
>//T U
(//U V
)//V W
;//W X+
librosElectronicosFoxcontLogica00 +
=00, -
Dependencia00. 9
.009 :
Resolve00: A
<00A B,
 ILibrosElectronicosFoxcontLogica00B b
>00b c
(00c d
)00d e
;00e f
}11 	
public22 

JsonResult22 8
,ReporteVentaFormatoFoxcontBoletaVentaFactura22 F
(22F G
int22G J
	idPeriodo22K T
)22T U
{33 	
try44 
{55 
var66 
sede66 
=66 
ObtenerSede66 &
(66& '
)66' (
;66( )
var77 
periodo77 
=77 $
librosElectronicosLogica77 6
.776 7
ObtenerPeriodo777 E
(77E F
	idPeriodo77F O
)77O P
;77P Q
var88 
	rptviewer88 
=88 
	generador88  )
.88) *A
5GenerarReporteDeVentaFormatoFoxcontBoletaVentaFactura88* _
(88_ `
ProfileData88` k
(88k l
)88l m
.88m n
Empleado88n v
.88v w
Id88w y
,88y z
sede88{ 
,	88 �
periodo
88� �
,
88� �
true
88� �
,
88� �-
librosElectronicosFoxcontLogica
88� �
,
88� �
Request
88� �
.
88� �
MapPath
88� �
(
88� �
Request
88� �
.
88� �
ApplicationPath
88� �
)
88� �
)
88� �
;
88� �
string:: 
filename:: 
;::  
string;; 
filenameContent;; &
;;;& '
filenameContent<< 
=<<  !
periodo<<" )
.<<) *
mes<<* -
+<<. /
$str<<0 3
+<<4 5
periodo<<6 =
.<<= >
anio<<> B
;<<B C
filename== 
=== 
string== !
.==! "
Format==" (
(==( )
$str==) 2
,==2 3
filenameContent==4 C
,==C D
$str==E J
)==J K
;==K L
filename>> 
=>> 
filename>> #
.>># $
Replace>>$ +
(>>+ ,
$str>>, /
,>>/ 0
$str>>1 3
)>>3 4
;>>4 5
byte@@ 
[@@ 
]@@ 
bytes@@ 
=@@ 
	rptviewer@@ (
.@@( )
LocalReport@@) 4
.@@4 5
Render@@5 ;
(@@; <
$str@@< C
,@@C D
null@@E I
,@@I J
out@@K N
string@@O U
mimeType@@V ^
,@@^ _
out@@` c
string@@d j
encoding@@k s
,@@s t
out@@u x
string@@y 
	extension
@@� �
,
@@� �
out
@@� �
string
@@� �
[
@@� �
]
@@� �
	streamids
@@� �
,
@@� �
out
@@� �
Warning
@@� �
[
@@� �
]
@@� �
warnings
@@� �
)
@@� �
;
@@� �
ResponseBB 
.BB 
ClearHeadersBB %
(BB% &
)BB& '
;BB' (
ResponseCC 
.CC 
ClearCC 
(CC 
)CC  
;CC  !
ResponseDD 
.DD 
	AddHeaderDD "
(DD" #
$strDD# 8
,DD8 9
$strDD: Q
+DDR S
filenameDDT \
)DD\ ]
;DD] ^
ResponseEE 
.EE 
ContentTypeEE $
=EE% &
mimeTypeEE' /
;EE/ 0
ResponseFF 
.FF 
BinaryWriteFF $
(FF$ %
bytesFF% *
)FF* +
;FF+ ,
ResponseGG 
.GG 
FlushGG 
(GG 
)GG  
;GG  !
ResponseHH 
.HH 
EndHH 
(HH 
)HH 
;HH 
returnJJ 
JsonJJ 
(JJ 
$strJJ ;
)JJ; <
;JJ< =
}KK 
catchLL 
(LL 
	ExceptionLL 
eLL 
)LL 
{MM 
throwNN 
newNN 
ControllerExceptionNN -
(NN- .
$strNN. i
,NNi j
eNNk l
)NNl m
;NNm n
}OO 
}PP 	
publicRR 

JsonResultRR 7
+ReporteVentaFormatoFoxcontNotaCreditoDebitoRR E
(RRE F
intRRF I
	idPeriodoRRJ S
)RRS T
{SS 	
tryTT 
{UU 
varVV 
sedeVV 
=VV 
ObtenerSedeVV &
(VV& '
)VV' (
;VV( )
varWW 
periodoWW 
=WW $
librosElectronicosLogicaWW 6
.WW6 7
ObtenerPeriodoWW7 E
(WWE F
	idPeriodoWWF O
)WWO P
;WWP Q
varXX 
	rptviewerXX 
=XX 
	generadorXX  )
.XX) *@
4GenerarReporteDeVentaFormatoFoxcontNotaCreditoDebitoXX* ^
(XX^ _
ProfileDataXX_ j
(XXj k
)XXk l
.XXl m
EmpleadoXXm u
.XXu v
IdXXv x
,XXx y
sedeXXz ~
,XX~ 
periodo
XX� �
,
XX� �
true
XX� �
,
XX� �-
librosElectronicosFoxcontLogica
XX� �
,
XX� �
Request
XX� �
.
XX� �
MapPath
XX� �
(
XX� �
Request
XX� �
.
XX� �
ApplicationPath
XX� �
)
XX� �
)
XX� �
;
XX� �
ViewBagYY 
.YY 
ReportViewerYY $
=YY% &
	rptviewerYY' 0
;YY0 1
string[[ 
filename[[ 
;[[  
string\\ 
filenameContent\\ &
;\\& '
filenameContent]] 
=]]  !
periodo]]" )
.]]) *
mes]]* -
+]]. /
$str]]0 3
+]]4 5
periodo]]6 =
.]]= >
anio]]> B
;]]B C
filename^^ 
=^^ 
string^^ !
.^^! "
Format^^" (
(^^( )
$str^^) 2
,^^2 3
filenameContent^^4 C
,^^C D
$str^^E J
)^^J K
;^^K L
filename`` 
=`` 
filename`` #
.``# $
Replace``$ +
(``+ ,
$str``, /
,``/ 0
$str``1 3
)``3 4
;``4 5
bytebb 
[bb 
]bb 
bytesbb 
=bb 
	rptviewerbb (
.bb( )
LocalReportbb) 4
.bb4 5
Renderbb5 ;
(bb; <
$strbb< C
,bbC D
nullbbE I
,bbI J
outbbK N
stringbbO U
mimeTypebbV ^
,bb^ _
outbb` c
stringbbd j
encodingbbk s
,bbs t
outbbu x
stringbby 
	extension
bb� �
,
bb� �
out
bb� �
string
bb� �
[
bb� �
]
bb� �
	streamids
bb� �
,
bb� �
out
bb� �
Warning
bb� �
[
bb� �
]
bb� �
warnings
bb� �
)
bb� �
;
bb� �
Responsedd 
.dd 
ClearHeadersdd %
(dd% &
)dd& '
;dd' (
Responseee 
.ee 
Clearee 
(ee 
)ee  
;ee  !
Responseff 
.ff 
	AddHeaderff "
(ff" #
$strff# 8
,ff8 9
$strff: Q
+ffR S
filenameffT \
)ff\ ]
;ff] ^
Responsegg 
.gg 
ContentTypegg $
=gg% &
mimeTypegg' /
;gg/ 0
Responsehh 
.hh 
BinaryWritehh $
(hh$ %
byteshh% *
)hh* +
;hh+ ,
Responseii 
.ii 
Flushii 
(ii 
)ii  
;ii  !
Responsejj 
.jj 
Endjj 
(jj 
)jj 
;jj 
returnll 
Jsonll 
(ll 
$strll ;
)ll; <
;ll< =
}mm 
catchnn 
(nn 
	Exceptionnn 
enn 
)nn 
{oo 
throwpp 
newpp 
ControllerExceptionpp -
(pp- .
$str	pp. �
,
pp� �
e
pp� �
)
pp� �
;
pp� �
}qq 
}rr 	
}ss 
}tt �6
}D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\LibrosElectronicos\LibrosElectronicosAdsoftController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{   
public!! 

partial!! 
class!! .
"LibrosElectronicosAdsoftController!! ;
:!!< =
BaseController!!> L
{"" 
	protected## 
readonly## 
IConceptoLogica## *
conceptoLogica##+ 9
;##9 :
	protected$$ 
readonly$$ 
IOperacionLogica$$ +
operacionLogica$$, ;
;$$; <
	protected%% 
readonly%% 
IActorNegocioLogica%% .
actorNegocioLogica%%/ A
;%%A B
	protected&& 
readonly&& %
ILibrosElectronicosLogica&& 4$
librosElectronicosLogica&&5 M
;&&M N
	protected'' 
readonly'' +
ILibrosElectronicosAdsoftLogica'' :*
librosElectronicosAdsoftLogica''; Y
;''Y Z/
#LibrosElectronicosGeneradorReportes(( +
	generador((, 5
=((6 7
new((8 ;/
#LibrosElectronicosGeneradorReportes((< _
(((_ `
)((` a
;((a b
public** .
"LibrosElectronicosAdsoftController** 1
(**1 2
)**2 3
:**4 5
base**6 :
(**: ;
)**; <
{++ 	
conceptoLogica,, 
=,, 
Dependencia,, (
.,,( )
Resolve,,) 0
<,,0 1
IConceptoLogica,,1 @
>,,@ A
(,,A B
),,B C
;,,C D
operacionLogica-- 
=-- 
Dependencia-- )
.--) *
Resolve--* 1
<--1 2
IOperacionLogica--2 B
>--B C
(--C D
)--D E
;--E F
actorNegocioLogica.. 
=..  
Dependencia..! ,
..., -
Resolve..- 4
<..4 5
IActorNegocioLogica..5 H
>..H I
(..I J
)..J K
;..K L$
librosElectronicosLogica// $
=//% &
Dependencia//' 2
.//2 3
Resolve//3 :
<//: ;%
ILibrosElectronicosLogica//; T
>//T U
(//U V
)//V W
;//W X*
librosElectronicosAdsoftLogica00 *
=00+ ,
Dependencia00- 8
.008 9
Resolve009 @
<00@ A+
ILibrosElectronicosAdsoftLogica00A `
>00` a
(00a b
)00b c
;00c d
}11 	
public22 

JsonResult22 %
ReporteVentaFormatoAdsoft22 3
(223 4
int224 7
	idPeriodo228 A
)22A B
{33 	
try44 
{55 
var66 
periodo66 
=66 $
librosElectronicosLogica66 6
.666 7
ObtenerPeriodo667 E
(66E F
	idPeriodo66F O
)66O P
;66P Q
var77 
sede77 
=77 
ObtenerSede77 &
(77& '
)77' (
;77( )
var88 
	rptviewer88 
=88 
	generador88  )
.88) *.
"GenerarReporteDeVentaFormatoAdsoft88* L
(88L M
ProfileData88M X
(88X Y
)88Y Z
.88Z [
Empleado88[ c
.88c d
Id88d f
,88f g
sede88h l
,88l m
periodo88n u
,88u v
true88w {
,88{ |+
librosElectronicosAdsoftLogica	88} �
,
88� �
Request
88� �
.
88� �
MapPath
88� �
(
88� �
Request
88� �
.
88� �
ApplicationPath
88� �
)
88� �
)
88� �
;
88� �
string:: 
filename:: 
;::  
string;; 
filenameContent;; &
;;;& '
filenameContent<< 
=<<  !
this<<" &
.<<& '
ObtenerSede<<' 2
(<<2 3
)<<3 4
.<<4 5
DocumentoIdentidad<<5 G
+<<H I
$str<<J M
+<<N O
this<<P T
.<<T U
ObtenerSede<<U `
(<<` a
)<<a b
.<<b c
Nombre<<c i
+<<j k
$str<<l o
+<<p q
periodo<<r y
.<<y z
anio<<z ~
+	<< �
$str
<<� �
+
<<� �
periodo
<<� �
.
<<� �
mes
<<� �
;
<<� �
filename== 
=== 
string== !
.==! "
Format==" (
(==( )
$str==) 2
,==2 3
filenameContent==4 C
,==C D
$str==E J
)==J K
;==K L
filename>> 
=>> 
filename>> #
.>># $
Replace>>$ +
(>>+ ,
$str>>, /
,>>/ 0
$str>>1 3
)>>3 4
;>>4 5
byte@@ 
[@@ 
]@@ 
bytes@@ 
=@@ 
	rptviewer@@ (
.@@( )
LocalReport@@) 4
.@@4 5
Render@@5 ;
(@@; <
$str@@< C
,@@C D
null@@E I
,@@I J
out@@K N
string@@O U
mimeType@@V ^
,@@^ _
out@@` c
string@@d j
encoding@@k s
,@@s t
out@@u x
string@@y 
	extension
@@� �
,
@@� �
out
@@� �
string
@@� �
[
@@� �
]
@@� �
	streamids
@@� �
,
@@� �
out
@@� �
Warning
@@� �
[
@@� �
]
@@� �
warnings
@@� �
)
@@� �
;
@@� �
ResponseBB 
.BB 
ClearHeadersBB %
(BB% &
)BB& '
;BB' (
ResponseCC 
.CC 
ClearCC 
(CC 
)CC  
;CC  !
ResponseDD 
.DD 
	AddHeaderDD "
(DD" #
$strDD# 8
,DD8 9
$strDD: Q
+DDR S
filenameDDT \
)DD\ ]
;DD] ^
ResponseEE 
.EE 
ContentTypeEE $
=EE% &
mimeTypeEE' /
;EE/ 0
ResponseFF 
.FF 
BinaryWriteFF $
(FF$ %
bytesFF% *
)FF* +
;FF+ ,
ResponseGG 
.GG 
FlushGG 
(GG 
)GG  
;GG  !
ResponseHH 
.HH 
EndHH 
(HH 
)HH 
;HH 
returnJJ 
JsonJJ 
(JJ 
$strJJ ;
)JJ; <
;JJ< =
}KK 
catchLL 
(LL 
	ExceptionLL 
eLL 
)LL 
{MM 
throwNN 
newNN 
ControllerExceptionNN -
(NN- .
$strNN. a
,NNa b
eNNc d
)NNd e
;NNe f
}OO 
}PP 	
}QQ 
}RR ��
}D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\LibrosElectronicos\LibrosElectronicosConcarController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public   

partial   
class   .
"LibrosElectronicosConcarController   ;
:  < =
BaseController  > L
{!! 
	protected"" 
readonly"" 
IConceptoLogica"" *
conceptoLogica""+ 9
;""9 :
	protected## 
readonly## 
IOperacionLogica## +
operacionLogica##, ;
;##; <
	protected$$ 
readonly$$ 
IActorNegocioLogica$$ .
actorNegocioLogica$$/ A
;$$A B
	protected%% 
readonly%% %
ILibrosElectronicosLogica%% 4$
librosElectronicosLogica%%5 M
;%%M N
	protected&& 
readonly&& +
ILibrosElectronicosConcarLogica&& :*
librosElectronicosConcarLogica&&; Y
;&&Y Z
public(( .
"LibrosElectronicosConcarController(( 1
(((1 2
)((2 3
:((4 5
base((6 :
(((: ;
)((; <
{)) 	
conceptoLogica** 
=** 
Dependencia** (
.**( )
Resolve**) 0
<**0 1
IConceptoLogica**1 @
>**@ A
(**A B
)**B C
;**C D
operacionLogica++ 
=++ 
Dependencia++ )
.++) *
Resolve++* 1
<++1 2
IOperacionLogica++2 B
>++B C
(++C D
)++D E
;++E F
actorNegocioLogica,, 
=,,  
Dependencia,,! ,
.,,, -
Resolve,,- 4
<,,4 5
IActorNegocioLogica,,5 H
>,,H I
(,,I J
),,J K
;,,K L$
librosElectronicosLogica-- $
=--% &
Dependencia--' 2
.--2 3
Resolve--3 :
<--: ;%
ILibrosElectronicosLogica--; T
>--T U
(--U V
)--V W
;--W X*
librosElectronicosConcarLogica.. *
=..+ ,
Dependencia..- 8
...8 9
Resolve..9 @
<..@ A+
ILibrosElectronicosConcarLogica..A `
>..` a
(..a b
)..b c
;..c d
}// 	
public11 
ActionResult11 +
ObtenerLibrosElectronicosConcar11 ;
(11; <
int11< ?
	idPeriodo11@ I
)11I J
{22 	
try33 
{44 
var55 
sede55 
=55 
ObtenerSede55 &
(55& '
)55' (
;55( )
var66 
periodo66 
=66 $
librosElectronicosLogica66 6
.666 7
ObtenerPeriodo667 E
(66E F
	idPeriodo66F O
)66O P
;66P Q
string77 
fileNameZip77 "
=77# $
sede77% )
.77) *
DocumentoIdentidad77* <
+77= >
$str77? I
+77J K
periodo77L S
.77S T
nombre77T Z
+77[ \
$str77] c
;77c d
var99 
compressedBytes99 #
=99$ %+
GenerarLibrosElectronicosConcar99& E
(99E F
sede99F J
,99J K
periodo99L S
)99S T
;99T U
return:: 
File:: 
(:: 
compressedBytes:: +
,::+ ,
$str::- >
,::> ?
fileNameZip::@ K
)::K L
;::L M
};; 
catch<< 
(<< 
LogicaException<< "
oe<<# %
)<<% &
{== 
return>> 
new>>  
JsonHttpStatusResult>> /
(>>/ 0
Util>>0 4
.>>4 5
	ErrorJson>>5 >
(>>> ?
oe>>? A
)>>A B
,>>B C
HttpStatusCode>>D R
.>>R S
InternalServerError>>S f
)>>f g
;>>g h
}?? 
catch@@ 
(@@ 
	Exception@@ 
e@@ 
)@@ 
{AA 
returnBB 
newBB  
JsonHttpStatusResultBB /
(BB/ 0
UtilBB0 4
.BB4 5
	ErrorJsonBB5 >
(BB> ?
newBB? B
	ExceptionBBC L
(BBL M
$str	BBM �
,
BB� �
e
BB� �
)
BB� �
)
BB� �
,
BB� �
HttpStatusCode
BB� �
.
BB� �!
InternalServerError
BB� �
)
BB� �
;
BB� �
}CC 
}DD 	
publicFF 
byteFF 
[FF 
]FF +
GenerarLibrosElectronicosConcarFF 5
(FF5 6$
EstablecimientoComercialFF6 N
sedeFFO S
,FFS T
PeriodoFFU \
periodoFF] d
)FFd e
{GG 	
tryHH 
{II 
LibroElectronicoConcarJJ &$
librosElectronicosConcarJJ' ?
=JJ@ A*
librosElectronicosConcarLogicaJJB `
.JJ` a,
ObtenerLibrosElectronicosConcar	JJa �
(
JJ� �
periodo
JJ� �
)
JJ� �
;
JJ� �
byteKK 
[KK 
]KK 
	fileBytesKK  
=KK! "
nullKK# '
;KK' (
byteLL 
[LL 
]LL 
compressedBytesLL &
;LL& '
usingMM 
(MM 
varMM 
	outStreamMM $
=MM% &
newMM' *
MemoryStreamMM+ 7
(MM7 8
)MM8 9
)MM9 :
{NN 
usingOO 
(OO 
varOO 
archiveOO &
=OO' (
newOO) ,

ZipArchiveOO- 7
(OO7 8
	outStreamOO8 A
,OOA B
ZipArchiveModeOOC Q
.OOQ R
CreateOOR X
,OOX Y
trueOOZ ^
)OO^ _
)OO_ `
{PP 
ZipArchiveEntryQQ '
fileInArchive03QQ( 7
=QQ8 9
nullQQ: >
;QQ> ?
varRR 
rptviewer03RR '
=RR( )'
GenerarReporteConcarConRdlcRR* E
(RRE F$
librosElectronicosConcarRRF ^
.RR^ _
RegistroCobranzasRR_ p
,RRp q
trueRRr v
,RRv w
RequestRRx 
.	RR �
MapPath
RR� �
(
RR� �
Request
RR� �
.
RR� �
ApplicationPath
RR� �
)
RR� �
)
RR� �
;
RR� �
stringSS 
filenameContent03SS 0
=SS1 2
sedeSS3 7
.SS7 8
DocumentoIdentidadSS8 J
+SSK L
$strSSM \
+SS] ^
periodoSS_ f
.SSf g
nombreSSg m
;SSm n
fileInArchive03TT '
=TT( )
archiveTT* 1
.TT1 2
CreateEntryTT2 =
(TT= >
stringTT> D
.TTD E
FormatTTE K
(TTK L
$strTTL U
,TTU V
filenameContent03TTW h
,TTh i
$strTTj o
)TTo p
.TTp q
ReplaceTTq x
(TTx y
$strTTy |
,TT| }
$str	TT~ �
)
TT� �
,
TT� �
CompressionLevel
TT� �
.
TT� �
Optimal
TT� �
)
TT� �
;
TT� �
	fileBytesUU !
=UU" #
rptviewer03UU$ /
.UU/ 0
LocalReportUU0 ;
.UU; <
RenderUU< B
(UUB C
$strUUC J
,UUJ K
nullUUL P
,UUP Q
outUUR U
stringUUV \

mimeType03UU] g
,UUg h
outUUi l
stringUUm s

encoding03UUt ~
,UU~ 
out
UU� �
string
UU� �
extension03
UU� �
,
UU� �
out
UU� �
string
UU� �
[
UU� �
]
UU� �
streamids03
UU� �
,
UU� �
out
UU� �
Warning
UU� �
[
UU� �
]
UU� �

warnings03
UU� �
)
UU� �
;
UU� �
usingVV 
(VV 
varVV "
entryStreamVV# .
=VV/ 0
fileInArchive03VV1 @
.VV@ A
OpenVVA E
(VVE F
)VVF G
)VVG H
usingWW 
(WW 
varWW " 
fileToCompressStreamWW# 7
=WW8 9
newWW: =
MemoryStreamWW> J
(WWJ K
	fileBytesWWK T
)WWT U
)WWU V
{XX  
fileToCompressStreamYY 0
.YY0 1
CopyToYY1 7
(YY7 8
entryStreamYY8 C
)YYC D
;YYD E 
fileToCompressStreamZZ 0
.ZZ0 1
CloseZZ1 6
(ZZ6 7
)ZZ7 8
;ZZ8 9
}[[ 
ZipArchiveEntry]] '
fileInArchive05]]( 7
=]]8 9
null]]: >
;]]> ?
var^^ 
rptviewer05^^ '
=^^( )'
GenerarReporteConcarConRdlc^^* E
(^^E F$
librosElectronicosConcar^^F ^
.^^^ _
RegistroVentas^^_ m
,^^m n
true^^o s
,^^s t
Request^^u |
.^^| }
MapPath	^^} �
(
^^� �
Request
^^� �
.
^^� �
ApplicationPath
^^� �
)
^^� �
)
^^� �
;
^^� �
string__ 
filenameContent05__ 0
=__1 2
sede__3 7
.__7 8
DocumentoIdentidad__8 J
+__K L
$str__M \
+__] ^
periodo___ f
.__f g
nombre__g m
;__m n
fileInArchive05`` '
=``( )
archive``* 1
.``1 2
CreateEntry``2 =
(``= >
string``> D
.``D E
Format``E K
(``K L
$str``L U
,``U V
filenameContent05``W h
,``h i
$str``j o
)``o p
.``p q
Replace``q x
(``x y
$str``y |
,``| }
$str	``~ �
)
``� �
,
``� �
CompressionLevel
``� �
.
``� �
Optimal
``� �
)
``� �
;
``� �
	fileBytesaa !
=aa" #
rptviewer05aa$ /
.aa/ 0
LocalReportaa0 ;
.aa; <
Renderaa< B
(aaB C
$straaC J
,aaJ K
nullaaL P
,aaP Q
outaaR U
stringaaV \

mimeType05aa] g
,aag h
outaai l
stringaam s

encoding05aat ~
,aa~ 
out
aa� �
string
aa� �
extension05
aa� �
,
aa� �
out
aa� �
string
aa� �
[
aa� �
]
aa� �
streamids05
aa� �
,
aa� �
out
aa� �
Warning
aa� �
[
aa� �
]
aa� �

warnings05
aa� �
)
aa� �
;
aa� �
usingbb 
(bb 
varbb "
entryStreambb# .
=bb/ 0
fileInArchive05bb1 @
.bb@ A
OpenbbA E
(bbE F
)bbF G
)bbG H
usingcc 
(cc 
varcc " 
fileToCompressStreamcc# 7
=cc8 9
newcc: =
MemoryStreamcc> J
(ccJ K
	fileBytesccK T
)ccT U
)ccU V
{dd  
fileToCompressStreamee 0
.ee0 1
CopyToee1 7
(ee7 8
entryStreamee8 C
)eeC D
;eeD E 
fileToCompressStreamff 0
.ff0 1
Closeff1 6
(ff6 7
)ff7 8
;ff8 9
}gg 
ZipArchiveEntryii '
fileInArchive31ii( 7
=ii8 9
nullii: >
;ii> ?
varjj 
rptviewer31jj '
=jj( )'
GenerarReporteConcarConRdlcjj* E
(jjE F$
librosElectronicosConcarjjF ^
.jj^ _

,jjl m
truejjn r
,jjr s
Requestjjt {
.jj{ |
MapPath	jj| �
(
jj� �
Request
jj� �
.
jj� �
ApplicationPath
jj� �
)
jj� �
)
jj� �
;
jj� �
stringkk 
filenameContent31kk 0
=kk1 2
sedekk3 7
.kk7 8
DocumentoIdentidadkk8 J
+kkK L
$strkkM \
+kk] ^
periodokk_ f
.kkf g
nombrekkg m
;kkm n
fileInArchive31ll '
=ll( )
archivell* 1
.ll1 2
CreateEntryll2 =
(ll= >
stringll> D
.llD E
FormatllE K
(llK L
$strllL U
,llU V
filenameContent31llW h
,llh i
$strllj o
)llo p
.llp q
Replacellq x
(llx y
$strlly |
,ll| }
$str	ll~ �
)
ll� �
,
ll� �
CompressionLevel
ll� �
.
ll� �
Optimal
ll� �
)
ll� �
;
ll� �
	fileBytesmm !
=mm" #
rptviewer31mm$ /
.mm/ 0
LocalReportmm0 ;
.mm; <
Rendermm< B
(mmB C
$strmmC J
,mmJ K
nullmmL P
,mmP Q
outmmR U
stringmmV \

mimeType31mm] g
,mmg h
outmmi l
stringmmm s

encoding31mmt ~
,mm~ 
out
mm� �
string
mm� �
extension31
mm� �
,
mm� �
out
mm� �
string
mm� �
[
mm� �
]
mm� �
streamids31
mm� �
,
mm� �
out
mm� �
Warning
mm� �
[
mm� �
]
mm� �

warnings31
mm� �
)
mm� �
;
mm� �
usingnn 
(nn 
varnn "
entryStreamnn# .
=nn/ 0
fileInArchive31nn1 @
.nn@ A
OpennnA E
(nnE F
)nnF G
)nnG H
usingoo 
(oo 
varoo " 
fileToCompressStreamoo# 7
=oo8 9
newoo: =
MemoryStreamoo> J
(ooJ K
	fileBytesooK T
)ooT U
)ooU V
{pp  
fileToCompressStreamqq 0
.qq0 1
CopyToqq1 7
(qq7 8
entryStreamqq8 C
)qqC D
;qqD E 
fileToCompressStreamrr 0
.rr0 1
Closerr1 6
(rr6 7
)rr7 8
;rr8 9
}ss 
ZipArchiveEntryuu '

=uu6 7
nulluu8 <
;uu< =
varvv 
	rptviewervv %
=vv& '.
"GenerarReporteClienteConcarConRdlcvv( J
(vvJ K$
librosElectronicosConcarvvK c
.vvc d
RegistroClientesvvd t
,vvt u
truevvv z
,vvz {
Request	vv| �
.
vv� �
MapPath
vv� �
(
vv� �
Request
vv� �
.
vv� �
ApplicationPath
vv� �
)
vv� �
)
vv� �
;
vv� �
stringww 

=ww- .
sedeww/ 3
.ww3 4
DocumentoIdentidadww4 F
+wwG H
$strwwI R
+wwS T
periodowwU \
.ww\ ]
nombreww] c
;wwc d

=xx& '
archivexx( /
.xx/ 0
CreateEntryxx0 ;
(xx; <
stringxx< B
.xxB C
FormatxxC I
(xxI J
$strxxJ S
,xxS T

,xxb c
$strxxd i
)xxi j
.xxj k
Replacexxk r
(xxr s
$strxxs v
,xxv w
$strxxx z
)xxz {
,xx{ |
CompressionLevel	xx} �
.
xx� �
Optimal
xx� �
)
xx� �
;
xx� �
	fileBytesyy !
=yy" #
	rptvieweryy$ -
.yy- .
LocalReportyy. 9
.yy9 :
Renderyy: @
(yy@ A
$stryyA H
,yyH I
nullyyJ N
,yyN O
outyyP S
stringyyT Z
mimeTypeyy[ c
,yyc d
outyye h
stringyyi o
encodingyyp x
,yyx y
outyyz }
string	yy~ �
	extension
yy� �
,
yy� �
out
yy� �
string
yy� �
[
yy� �
]
yy� �
	streamids
yy� �
,
yy� �
out
yy� �
Warning
yy� �
[
yy� �
]
yy� �
warnings
yy� �
)
yy� �
;
yy� �
usingzz 
(zz 
varzz "
entryStreamzz# .
=zz/ 0

.zz> ?
Openzz? C
(zzC D
)zzD E
)zzE F
using{{ 
({{ 
var{{ " 
fileToCompressStream{{# 7
={{8 9
new{{: =
MemoryStream{{> J
({{J K
	fileBytes{{K T
){{T U
){{U V
{||  
fileToCompressStream}} 0
.}}0 1
CopyTo}}1 7
(}}7 8
entryStream}}8 C
)}}C D
;}}D E 
fileToCompressStream~~ 0
.~~0 1
Close~~1 6
(~~6 7
)~~7 8
;~~8 9
} 
}
�� 
compressedBytes
�� #
=
��$ %
	outStream
��& /
.
��/ 0
ToArray
��0 7
(
��7 8
)
��8 9
;
��9 :
}
�� 
return
�� 
compressedBytes
�� &
;
��& '
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
�� !
ControllerException
�� -
(
��- .
$str
��. b
,
��b c
e
��d e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 
ReportViewer
�� )
GenerarReporteConcarConRdlc
�� 7
(
��7 8
List
��8 <
<
��< =*
DetalleAsientoContableConcar
��= Y
>
��Y Z
registrosConcar
��[ j
,
��j k
bool
��l p
fromRequest
��q |
,
��| }
string��~ �
requestMapPath��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
	rptviewer
�� 
=
�� 
new
��  #
ReportViewer
��$ 0
(
��0 1
)
��1 2
;
��2 3
string
�� 
path
�� 
=
�� 
$str
�� p
;
��p q
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

ReportPath
��& 0
=
��1 2
fromRequest
��3 >
?
��? @
requestMapPath
��A O
+
��P Q
path
��R V
:
��W X 
HostingEnvironment
��Y k
.
��k l
MapPath
��l s
(
��s t
path
��t x
)
��x y
;
��y z
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

��& 3
(
��3 4
new
��4 7
ReportParameter
��8 G
[
��G H
]
��H I
{
��J K
}
��L M
)
��M N
;
��N O
ReportDataSource
��  "
rptdatasourcereporte
��! 5
=
��6 7
new
��8 ;
ReportDataSource
��< L
(
��L M
$str
��M d
,
��d e
registrosConcar
��f u
)
��u v
;
��v w
	rptviewer
�� 
.
�� 
ProcessingMode
�� (
=
��) *
ProcessingMode
��+ 9
.
��9 :
Local
��: ?
;
��? @
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 6"
rptdatasourcereporte
��6 J
)
��J K
;
��K L
	rptviewer
�� 
.
�� !
SizeToReportContent
�� -
=
��. /
true
��0 4
;
��4 5
	rptviewer
�� 
.
�� 
Width
�� 
=
��  !
Unit
��" &
.
��& '

Percentage
��' 1
(
��1 2
$num
��2 5
)
��5 6
;
��6 7
	rptviewer
�� 
.
�� 
Height
��  
=
��! "
Unit
��# '
.
��' (

Percentage
��( 2
(
��2 3
$num
��3 6
)
��6 7
;
��7 8
return
�� 
	rptviewer
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
�� !
ControllerException
�� -
(
��- .
$str
��. O
,
��O P
e
��Q R
)
��R S
;
��S T
}
�� 
}
�� 	
public
�� 
ReportViewer
�� 0
"GenerarReporteClienteConcarConRdlc
�� >
(
��> ?
List
��? C
<
��C D#
RegistroClienteConcar
��D Y
>
��Y Z
registrosCliente
��[ k
,
��k l
bool
��m q
fromRequest
��r }
,
��} ~
string�� �
requestMapPath��� �
)��� �
{
�� 	
try
�� 
{
�� 
var
�� 
	rptviewer
�� 
=
�� 
new
��  #
ReportViewer
��$ 0
(
��0 1
)
��1 2
;
��2 3
string
�� 
path
�� 
=
�� 
$str
�� w
;
��w x
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

ReportPath
��& 0
=
��1 2
fromRequest
��3 >
?
��? @
requestMapPath
��A O
+
��P Q
path
��R V
:
��W X 
HostingEnvironment
��Y k
.
��k l
MapPath
��l s
(
��s t
path
��t x
)
��x y
;
��y z
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &

��& 3
(
��3 4
new
��4 7
ReportParameter
��8 G
[
��G H
]
��H I
{
��J K
}
��L M
)
��M N
;
��N O
ReportDataSource
��  "
rptdatasourcereporte
��! 5
=
��6 7
new
��8 ;
ReportDataSource
��< L
(
��L M
$str
��M k
,
��k l
registrosCliente
��m }
)
��} ~
;
��~ 
	rptviewer
�� 
.
�� 
ProcessingMode
�� (
=
��) *
ProcessingMode
��+ 9
.
��9 :
Local
��: ?
;
��? @
	rptviewer
�� 
.
�� 
LocalReport
�� %
.
��% &
DataSources
��& 1
.
��1 2
Add
��2 5
(
��5 6"
rptdatasourcereporte
��6 J
)
��J K
;
��K L
	rptviewer
�� 
.
�� !
SizeToReportContent
�� -
=
��. /
true
��0 4
;
��4 5
	rptviewer
�� 
.
�� 
Width
�� 
=
��  !
Unit
��" &
.
��& '

Percentage
��' 1
(
��1 2
$num
��2 5
)
��5 6
;
��6 7
	rptviewer
�� 
.
�� 
Height
��  
=
��! "
Unit
��# '
.
��' (

Percentage
��( 2
(
��2 3
$num
��3 6
)
��6 7
;
��7 8
return
�� 
	rptviewer
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
�� !
ControllerException
�� -
(
��- .
$str
��. O
,
��O P
e
��Q R
)
��R S
;
��S T
}
�� 
}
�� 	
}
�� 
}�� �
iD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Finanza\FinanzaReportesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class %
FinanzaReportesController 2
:3 4
BaseController5 C
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly "
IFinanzaReporte_Logica 1"
finanzaReportingLogica2 H
;H I
public!! %
FinanzaReportesController!! (
(!!( )
)!!) *
:!!+ ,
base!!- 1
(!!1 2
)!!2 3
{"" 	
operacionLogica## 
=## 
Dependencia## )
.##) *
Resolve##* 1
<##1 2
IOperacionLogica##2 B
>##B C
(##C D
)##D E
;##E F
actorNegocioLogica$$ 
=$$  
Dependencia$$! ,
.$$, -
Resolve$$- 4
<$$4 5
IActorNegocioLogica$$5 H
>$$H I
($$I J
)$$J K
;$$K L
conceptoLogica%% 
=%% 
Dependencia%% (
.%%( )
Resolve%%) 0
<%%0 1
IConceptoLogica%%1 @
>%%@ A
(%%A B
)%%B C
;%%C D"
finanzaReportingLogica&& "
=&&# $
Dependencia&&% 0
.&&0 1
Resolve&&1 8
<&&8 9"
IFinanzaReporte_Logica&&9 O
>&&O P
(&&P Q
)&&Q R
;&&R S
}'' 	
[)) 	
	Authorize))	 
()) 
Roles)) 
=)) 
$str)) A
)))A B
]))B C
public** 
ActionResult** 
	Principal** %
(**% &
)**& '
{++ 	
ViewBag,, 
.,, 
Data,, 
=,, "
finanzaReportingLogica,, 1
.,,1 2,
 ObtenerDatosParaReportePrincipal,,2 R
(,,R S
ProfileData,,S ^
(,,^ _
),,_ `
),,` a
;,,a b
return-- 
View-- 
(-- 
)-- 
;-- 
}.. 	
}// 
}00 �I
gD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Pedido\PedidoReportesController.cs
	namespace   	
Tsp  
 
.  
Sigescom   
.   
WebApplication   %
.  % &
Controllers  & 1
{!! 
public"" 

class"" $
PedidoReportesController"" )
:""* +
BaseController"", :
{## 
	protected$$ 
readonly$$ $
ICentroDeAtencion_Logica$$ 3#
centroDeAtencion_Logica$$4 K
;$$K L
	protected%% 
readonly%% 
IOperacionLogica%% +
operacionLogica%%, ;
;%%; <
	protected&& 
readonly&& 
IPedido_Logica&& )
pedidoLogica&&* 6
;&&6 7
	protected'' 
readonly'' !
IPedidoReporte_Logica'' 0!
pedidoReportingLogica''1 F
;''F G
public** $
PedidoReportesController** '
(**' (
)**( )
:*** +
base**, 0
(**0 1
)**1 2
{++ 	
operacionLogica,, 
=,, 
Dependencia,, )
.,,) *
Resolve,,* 1
<,,1 2
IOperacionLogica,,2 B
>,,B C
(,,C D
),,D E
;,,E F#
centroDeAtencion_Logica-- #
=--$ %
Dependencia--& 1
.--1 2
Resolve--2 9
<--9 :$
ICentroDeAtencion_Logica--: R
>--R S
(--S T
)--T U
;--U V
pedidoLogica.. 
=.. 
Dependencia.. &
...& '
Resolve..' .
<... /
IPedido_Logica../ =
>..= >
(..> ?
)..? @
;..@ A!
pedidoReportingLogica// !
=//" #
Dependencia//$ /
./// 0
Resolve//0 7
<//7 8!
IPedidoReporte_Logica//8 M
>//M N
(//N O
)//O P
;//P Q
}00 	
public33 
ActionResult33 
Index33 !
(33! "
)33" #
{44 	
ViewBag55 
.55 
Data55 
=55 !
pedidoReportingLogica55 0
.550 1,
 ObtenerDatosParaReportePrincipal551 Q
(55Q R
ProfileData55R ]
(55] ^
)55^ _
)55_ `
;55` a
return66 
View66 
(66 
)66 
;66 
}77 	
public99 
ActionResult99 
PedidosInvalidados99 .
(99. /
int99/ 2&
idEstablecimientoComercial993 M
,99M N
string99O U!
nombreEstablecimiento99V k
,99k l
DateTime99m u

fechaDesde	99v �
,
99� �
DateTime
99� �

fechaHasta
99� �
,
99� �
[
99� �
System
99� �
.
99� �
Web
99� �
.
99� �
Http
99� �
.
99� �
FromUri
99� �
]
99� �
int
99� �
[
99� �
]
99� �
idsPuntosVenta
99� �
,
99� �
bool
99� �!
todosLosPuntosVenta
99� �
,
99� �
string
99� � 
nombresPuntosVenta
99� �
)
99� �
{:: 	
var;; 
	rptviewer;; 
=;; ,
 GenerarReportePedidosInvalidados;; <
(;;< =&
idEstablecimientoComercial;;= W
,;;W X!
nombreEstablecimiento;;Y n
,;;n o

fechaDesde;;p z
,;;z {

fechaHasta	;;| �
,
;;� �
idsPuntosVenta
;;� �
,
;;� �!
todosLosPuntosVenta
;;� �
,
;;� � 
nombresPuntosVenta
;;� �
,
;;� �
true
;;� �
)
;;� �
;
;;� �
ViewBag<< 
.<< 
ReportViewer<<  
=<<  !
	rptviewer<<! *
;<<* +
return== 
View== 
(== 
$str== -
)==- .
;==. /
}>> 	
public@@ 
ReportViewer@@ ,
 GenerarReportePedidosInvalidados@@ <
(@@< =
int@@= @&
idEstablecimientoComercial@@A [
,@@[ \
string@@\ b!
nombreEstablecimiento@@c x
,@@x y
DateTime	@@y �

fechaDesde
@@� �
,
@@� �
DateTime
@@� �

fechaHasta
@@� �
,
@@� �
[
@@� �
System
@@� �
.
@@� �
Web
@@� �
.
@@� �
Http
@@� �
.
@@� �
FromUri
@@� �
]
@@� �
int
@@� �
[
@@� �
]
@@� �
idsPuntosVenta
@@� �
,
@@� �
bool
@@� �!
todosLosPuntosVenta
@@� �
,
@@� �
string
@@� � 
nombresPuntosVenta
@@� �
,
@@� �
bool
@@� �
fromRequest
@@� �
)
@@� �
{AA 	
tryBB 
{CC 
varDD 
entradasDD 
=DD 
pedidoLogicaDD +
.DD+ ,,
 ObtenerReportePedidosInvalidadosDD, L
(DDL M

fechaDesdeDDM W
,DDW X

fechaHastaDDY c
,DDc d
idsPuntosVentaDDe s
,DDs t 
todosLosPuntosVenta	DDu �
,
DD� �(
idEstablecimientoComercial
DD� �
)
DD� �
;
DD� �
varEE 
parametrosBasicosEE %
=EE& '$
ObtenerParametrosBasicosEE( @
(EE@ A
)EEA B
;EEB C
ReportParameterFF  
parametroPuntosVentaFF  4
=FF5 6
newFF7 :
ReportParameterFF; J
(FFJ K
$strFFK X
,FFX Y
nombresPuntosVentaFFZ l
??FFm o
$strFFp w
)FFw x
;FFx y
ReportParameterGG *
parametroNombreEstablecimientoGG  >
=GG? @
newGGA D
ReportParameterGGE T
(GGT U
$strGGU l
,GGl m"
nombreEstablecimiento	GGn �
)
GG� �
;
GG� �
ReportParameterHH 
parametroFechaDesdeHH  3
=HH4 5
newHH6 9
ReportParameterHH: I
(HHI J
$strHHJ V
,HHV W

fechaDesdeHHX b
.HHb c
ToStringHHc k
(HHk l
)HHl m
)HHm n
;HHn o
ReportParameterII 
parametroFechaHastaII  3
=II4 5
newII6 9
ReportParameterII: I
(III J
$strIIJ V
,IIV W

fechaHastaIIX b
.IIb c
ToStringIIc k
(IIk l
)IIl m
)IIm n
;IIn o
varKK 
	rptviewerKK 
=KK 
newKK  #
ReportViewerKK$ 0
(KK0 1
)KK1 2
{KK3 4
ProcessingModeKK5 C
=KKD E
ProcessingModeKKF T
.KKT U
LocalKKU Z
,KKZ [
SizeToReportContentKK\ o
=KKp q
trueKKr v
,KKv w
WidthKKx }
=KK~ 
Unit
KK� �
.
KK� �

Percentage
KK� �
(
KK� �
$num
KK� �
)
KK� �
,
KK� �
Height
KK� �
=
KK� �
Unit
KK� �
.
KK� �

Percentage
KK� �
(
KK� �
$num
KK� �
)
KK� �
}
KK� �
;
KK� �
stringLL 
pathLL 
=LL 
$strLL V
;LLV W
	rptviewerNN 
.NN 
LocalReportNN %
.NN% &

ReportPathNN& 0
=NN1 2
fromRequestNN3 >
?NN? @
RequestNNA H
.NNH I
MapPathNNI P
(NNP Q
RequestNNQ X
.NNX Y
ApplicationPathNNY h
)NNh i
+NNj k
pathNNl p
:NNq r
HostingEnvironment	NNs �
.
NN� �
MapPath
NN� �
(
NN� �
path
NN� �
)
NN� �
;
NN� �
	rptviewerOO 
.OO 
LocalReportOO %
.OO% &

(OO3 4
newOO4 7
ReportParameterOO8 G
[OOG H
]OOH I
{PP 
parametrosBasicosQQ %
.QQ% &
FechaActualSistemaQQ& 8
,QQ8 9
parametrosBasicosRR %
.RR% &
LogoSedeRR& .
,RR. /
parametrosBasicosSS %
.SS% &
UsuarioSS& -
,SS- . 
parametroPuntosVentaTT (
,TT( )*
parametroNombreEstablecimientoUU 2
,UU2 3
parametroFechaDesdeVV '
,VV' (
parametroFechaHastaWW '
}XX 
)XX 
;XX 
ReportDataSourceYY  !
rptdatasourceEntradasYY! 6
=YY7 8
newYY9 <
ReportDataSourceYY= M
(YYM N
$strYYN b
,YYb c
entradasYYd l
)YYl m
;YYm n
	rptviewerZZ 
.ZZ 
LocalReportZZ %
.ZZ% &
DataSourcesZZ& 1
.ZZ1 2
AddZZ2 5
(ZZ5 6!
rptdatasourceEntradasZZ6 K
)ZZK L
;ZZL M
return[[ 
	rptviewer[[  
;[[  !
}\\ 
catch]] 
(]] 
	Exception]] 
e]] 
)]] 
{^^ 
throw__ 
new__ 
ControllerException__ -
(__- .
$str__. T
,__T U
e__V W
)__W X
;__X Y
}aa 
}bb 	
}cc 
}dd ��
_D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Pedido\PedidoController.cs
	namespace   	
Tsp  
 
.  
Sigescom   
.   
WebApplication   %
.  % &
Controllers  & 1
{!! 
public"" 

class"" 
PedidoController"" !
:""" #
BaseController""$ 2
{## 
private$$ 
readonly$$ 
IConceptoLogica$$ (
conceptoLogica$$) 7
;$$7 8
	protected%% 
readonly%% 
IMailer%% "
mailer%%# )
;%%) *
	protected&& 
readonly&& "
IVentaUtilitarioLogica&& 1
	ventaUtil&&2 ;
;&&; <
	protected'' 
readonly'' 
IPdfUtil'' #
pdfUtil''$ +
;''+ ,
	protected(( 
readonly(( 
IPedido_Logica(( )
pedidoLogica((* 6
;((6 7
	protected)) 
readonly)) 
IOperacionLogica)) +
operacionLogica)), ;
;)); <
	protected** 
readonly** )
IFacturacionElectronicaLogica** 8(
facturacionElectronicaLogica**9 U
;**U V
	protected++ 
readonly++ 
IActorNegocioLogica++ .
actorNegocioLogica++/ A
;++A B
	protected,, 
readonly,, 
IMaestroLogica,, )

;,,7 8
	protected-- 
readonly-- 
IBarCodeUtil-- '
barCodeUtil--( 3
;--3 4
private.. 
readonly..  
IConfiguracionLogica.. - 
_configuracionLogica... B
;..B C
public// 
PedidoController// 
(//  
)//  !
{00 	
conceptoLogica11 
=11 
Dependencia11 (
.11( )
Resolve11) 0
<110 1
IConceptoLogica111 @
>11@ A
(11A B
)11B C
;11C D
mailer22 
=22 
Dependencia22  
.22  !
Resolve22! (
<22( )
IMailer22) 0
>220 1
(221 2
)222 3
;223 4
	ventaUtil33 
=33 
Dependencia33 #
.33# $
Resolve33$ +
<33+ ,"
IVentaUtilitarioLogica33, B
>33B C
(33C D
)33D E
;33E F
pdfUtil44 
=44 
Dependencia44 !
.44! "
Resolve44" )
<44) *
IPdfUtil44* 2
>442 3
(443 4
)444 5
;445 6
pedidoLogica55 
=55 
Dependencia55 &
.55& '
Resolve55' .
<55. /
IPedido_Logica55/ =
>55= >
(55> ?
)55? @
;55@ A
operacionLogica66 
=66 
Dependencia66 )
.66) *
Resolve66* 1
<661 2
IOperacionLogica662 B
>66B C
(66C D
)66D E
;66E F(
facturacionElectronicaLogica77 (
=77) *
Dependencia77+ 6
.776 7
Resolve777 >
<77> ?)
IFacturacionElectronicaLogica77? \
>77\ ]
(77] ^
)77^ _
;77_ `
actorNegocioLogica88 
=88  
Dependencia88! ,
.88, -
Resolve88- 4
<884 5
IActorNegocioLogica885 H
>88H I
(88I J
)88J K
;88K L

=99 
Dependencia99 '
.99' (
Resolve99( /
<99/ 0
IMaestroLogica990 >
>99> ?
(99? @
)99@ A
;99A B
barCodeUtil:: 
=:: 
Dependencia:: %
.::% &
Resolve::& -
<::- .
IBarCodeUtil::. :
>::: ;
(::; <
)::< =
;::= > 
_configuracionLogica;;  
=;;! "
Dependencia;;# .
.;;. /
Resolve;;/ 6
<;;6 7 
IConfiguracionLogica;;7 K
>;;K L
(;;L M
);;M N
;;;N O
}== 	
public?? 
ActionResult?? 
Index?? !
(??! "
)??" #
{@@ 	
ViewBagAA 
.AA 
DataAA 
=AA 
pedidoLogicaAA '
.AA' ($
ObetenerDatosParaPedidosAA( @
(AA@ A
ProfileDataAAA L
(AAL M
)AAM N
)AAN O
;AAO P
ViewBagBB 
.BB 
FechaInicioBB 
=BB  !
DateTimeUtilBB" .
.BB. /
FechaActualBB/ :
(BB: ;
)BB; <
.BB< =
ToStringBB= E
(BBE F
$strBBF R
)BBR S
;BBS T
ViewBagCC 
.CC 
FechaFinCC 
=CC 
DateTimeUtilCC +
.CC+ ,
FechaActualCC, 7
(CC7 8
)CC8 9
.CC9 :
ToStringCC: B
(CCB C
$strCCC O
)CCO P
;CCP Q
varDD 
	modoVentaDD 
=DD 
(DD 
intDD  
)DD  !
	ModoVentaDD! *
.DD* +
VentaNormalDD+ 6
;DD6 7
ViewBagEE 
.EE 
TasaIGVEE 
=EE 
TransaccionSettingsEE 1
.EE1 2
DefaultEE2 9
.EE9 :
TasaIGVEE: A
;EEA B
ViewBagFF 
.FF 
AplicaLeyAmazoniaFF %
=FF& '
TransaccionSettingsFF( ;
.FF; <
DefaultFF< C
.FFC D
AplicaLeyAmazoniaFFD U
;FFU V
ViewBagGG 
.GG %
MostrarCodigoBarraBalanzaGG -
=GG. /
VentasSettingsGG0 >
.GG> ?
DefaultGG? F
.GGF G/
#ModoDeIngresoDeCodigoDeBarraEnVentaGGG j
==GGk m
(GGn o
intGGo r
)GGr s*
ModoIngresoCodigoBarraEnVenta	GGs �
.
GG� �"
CodigoBarraDeBalanza
GG� �
||
GG� �
VentasSettings
GG� �
.
GG� �
Default
GG� �
.
GG� �1
#ModoDeIngresoDeCodigoDeBarraEnVenta
GG� �
==
GG� �
(
GG� �
int
GG� �
)
GG� �+
ModoIngresoCodigoBarraEnVenta
GG� �
.
GG� �
Ambos
GG� �
;
GG� �
ViewBagHH 
.HH &
CursorInicialEnCodigoBarraHH .
=HH/ 0
VentasSettingsHH1 ?
.HH? @
DefaultHH@ G
.HHG H0
$CursorPorDefectoEnCodigoBarraEnVentaHHH l
==HHm o
(HHp q
intHHq t
)HHt u,
CursorInicialCodigoBarraEnVenta	HHu �
.
HH� �#
CodigoBarraDeProducto
HH� �
;
HH� �
ViewBagII 
.II "
EsVentaPorContingenciaII *
=II+ ,
	modoVentaII- 6
==II7 9
(II: ;
intII; >
)II> ?
	ModoVentaII? H
.IIH I 
VentaPorContingenciaIII ]
;II] ^
ViewBagJJ 
.JJ "
EsVentaModoCajaAlmacenJJ *
=JJ+ ,
	modoVentaJJ- 6
==JJ7 9
(JJ: ;
intJJ; >
)JJ> ?
	ModoVentaJJ? H
.JJH I 
VentaModoCajaAlmacenJJI ]
;JJ] ^
ViewBagKK 
.KK -
!PermitirRegistroDeGuiasDeRemisionKK 5
=KK6 7
VentasSettingsKK8 F
.KKF G
DefaultKKG N
.KKN O>
1PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada	KKO �
;
KK� �
ViewBagLL 
.LL ,
 PermitirRegistroConceptoServicioLL 4
=LL5 6
VentasSettingsLL7 E
.LLE F
DefaultLLF M
.LLM N,
 PermitirRegistroConceptoServicioLLN n
;LLn o
ViewBagMM 
.MM !
PermitirRegistroFleteMM )
=MM* +
AplicacionSettingsMM, >
.MM> ?
DefaultMM? F
.MMF G(
PermitirRegistroFleteEnVentaMMG c
;MMc d
ViewBagNN 
.NN !
PermitirRegistroPlacaNN )
=NN* +
VentasSettingsNN, :
.NN: ;
DefaultNN; B
.NNB C*
PermitirRegistroDePlacaEnVentaNNC a
;NNa b
ViewBagOO 
.OO 
IdClienteGenericoOO %
=OO& '

.OO5 6
DefaultOO6 =
.OO= >
IdClienteGenericoOO> O
;OOO P
ViewBagPP 
.PP !
IdMedioDePagoEfectivoPP )
=PP* +
MaestroSettingsPP, ;
.PP; <
DefaultPP< C
.PPC D/
#IdDetalleMaestroMedioDepagoEfectivoPPD g
;PPg h
ViewBagQQ 
.QQ "
IdTipoDocumentoFacturaQQ *
=QQ+ ,
MaestroSettingsQQ- <
.QQ< =
DefaultQQ= D
.QQD E.
"IdDetalleMaestroComprobanteFacturaQQE g
;QQg h
ViewBagRR 
.RR 2
&IdTipoDocumentoCuandoClienteEsGenericoRR :
=RR; <
TransaccionSettingsRR= P
.RRP Q
DefaultRRQ X
.RRX Y2
&IdTipoDocumentoCuandoClienteEsGenericoRRY 
;	RR �
ViewBagSS 
.SS .
"IdTipoDocumentoPorDefectoParaVentaSS 6
=SS7 8
TransaccionSettingsSS9 L
.SSL M
DefaultSSM T
.SST U.
"IdTipoDocumentoPorDefectoParaVentaSSU w
;SSw x
ViewBagTT 
.TT #
IdTipoComprobantePedidoTT +
=TT, -
PedidoSettingsTT. <
.TT< =
DefaultTT= D
.TTD E#
IdTipoComprobantePedidoTTE \
;TT\ ]
ViewBagUU 
.UU -
!IdTipoComprobanteEmitirPorDefectoUU 5
=UU6 7
PedidoSettingsUU8 F
.UUF G
DefaultUUG N
.UUN O-
!IdTipoComprobanteEmitirPorDefectoUUO p
;UUp q
ViewBagVV 
.VV 
ImprimirPedidoVV "
=VV# $
PedidoSettingsVV% 3
.VV3 4
DefaultVV4 ;
.VV; <
ImprimirPedidoVV< J
;VVJ K
ViewBagWW 
.WW !
EsPregeneracionPedidoWW )
=WW* +
falseWW, 1
;WW1 2
ViewBagXX 
.XX 
IdOrdenAPregenerarXX &
=XX' (
$numXX) *
;XX* +
ViewBagYY 
.YY 
	WCPScriptYY 
=YY 

NeodynamicYY  *
.YY* +
SDKYY+ .
.YY. /
WebYY/ 2
.YY2 3
WebClientPrintYY3 A
.YYA B
CreateScriptYYB N
(YYN O
UrlZZ 
.ZZ 
ActionZZ 
(ZZ 
$strZZ +
,ZZ+ ,
$strZZ- @
,ZZ@ A
nullZZB F
,ZZF G
HttpContextZZH S
.ZZS T
RequestZZT [
.ZZ[ \
UrlZZ\ _
.ZZ_ `
SchemeZZ` f
)ZZf g
,ZZg h
Url[[ 
.[[ 
Action[[ 
([[ 
$str[[ &
,[[& '
$str[[( 0
,[[0 1
null[[2 6
,[[6 7
HttpContext[[8 C
.[[C D
Request[[D K
.[[K L
Url[[L O
.[[O P
Scheme[[P V
)[[V W
,[[W X
HttpContext[[Y d
.[[d e
Session[[e l
.[[l m
	SessionID[[m v
)[[v w
;[[w x
return\\ 
View\\ 
(\\ 
)\\ 
;\\ 
}]] 	
[^^ 	

ActionName^^	 
(^^ 
$str^^ )
)^^) *
]^^* +
public__ 
ActionResult__ 
Index__ !
(__! "
long__" &
idOrden__' .
)__. /
{`` 	
ViewBagaa 
.aa 
Dataaa 
=aa 
pedidoLogicaaa '
.aa' ($
ObetenerDatosParaPedidosaa( @
(aa@ A
ProfileDataaaA L
(aaL M
)aaM N
)aaN O
;aaO P
ViewBagbb 
.bb 
FechaIniciobb 
=bb  !
DateTimeUtilbb" .
.bb. /
FechaActualbb/ :
(bb: ;
)bb; <
.bb< =
ToStringbb= E
(bbE F
$strbbF R
)bbR S
;bbS T
ViewBagcc 
.cc 
FechaFincc 
=cc 
DateTimeUtilcc +
.cc+ ,
FechaActualcc, 7
(cc7 8
)cc8 9
.cc9 :
ToStringcc: B
(ccB C
$strccC O
)ccO P
;ccP Q
vardd 
	modoVentadd 
=dd 
(dd 
intdd  
)dd  !
	ModoVentadd! *
.dd* +
VentaNormaldd+ 6
;dd6 7
ViewBagee 
.ee 
TasaIGVee 
=ee 
TransaccionSettingsee 1
.ee1 2
Defaultee2 9
.ee9 :
TasaIGVee: A
;eeA B
ViewBagff 
.ff 
AplicaLeyAmazoniaff %
=ff& '
TransaccionSettingsff( ;
.ff; <
Defaultff< C
.ffC D
AplicaLeyAmazoniaffD U
;ffU V
ViewBaggg 
.gg %
MostrarCodigoBarraBalanzagg -
=gg. /
VentasSettingsgg0 >
.gg> ?
Defaultgg? F
.ggF G/
#ModoDeIngresoDeCodigoDeBarraEnVentaggG j
==ggk m
(ggn o
intggo r
)ggr s*
ModoIngresoCodigoBarraEnVenta	ggs �
.
gg� �"
CodigoBarraDeBalanza
gg� �
||
gg� �
VentasSettings
gg� �
.
gg� �
Default
gg� �
.
gg� �1
#ModoDeIngresoDeCodigoDeBarraEnVenta
gg� �
==
gg� �
(
gg� �
int
gg� �
)
gg� �+
ModoIngresoCodigoBarraEnVenta
gg� �
.
gg� �
Ambos
gg� �
;
gg� �
ViewBaghh 
.hh &
CursorInicialEnCodigoBarrahh .
=hh/ 0
VentasSettingshh1 ?
.hh? @
Defaulthh@ G
.hhG H0
$CursorPorDefectoEnCodigoBarraEnVentahhH l
==hhm o
(hhp q
inthhq t
)hht u,
CursorInicialCodigoBarraEnVenta	hhu �
.
hh� �#
CodigoBarraDeProducto
hh� �
;
hh� �
ViewBagii 
.ii "
EsVentaPorContingenciaii *
=ii+ ,
	modoVentaii- 6
==ii7 9
(ii: ;
intii; >
)ii> ?
	ModoVentaii? H
.iiH I 
VentaPorContingenciaiiI ]
;ii] ^
ViewBagjj 
.jj "
EsVentaModoCajaAlmacenjj *
=jj+ ,
	modoVentajj- 6
==jj7 9
(jj: ;
intjj; >
)jj> ?
	ModoVentajj? H
.jjH I 
VentaModoCajaAlmacenjjI ]
;jj] ^
ViewBagkk 
.kk -
!PermitirRegistroDeGuiasDeRemisionkk 5
=kk6 7
VentasSettingskk8 F
.kkF G
DefaultkkG N
.kkN O>
1PermitirRegistroDeGuiasDeRemisionEnVentaIntegrada	kkO �
;
kk� �
ViewBagll 
.ll ,
 PermitirRegistroConceptoServicioll 4
=ll5 6
VentasSettingsll7 E
.llE F
DefaultllF M
.llM N,
 PermitirRegistroConceptoServiciollN n
;lln o
ViewBagmm 
.mm !
PermitirRegistroFletemm )
=mm* +
AplicacionSettingsmm, >
.mm> ?
Defaultmm? F
.mmF G(
PermitirRegistroFleteEnVentammG c
;mmc d
ViewBagnn 
.nn !
PermitirRegistroPlacann )
=nn* +
VentasSettingsnn, :
.nn: ;
Defaultnn; B
.nnB C*
PermitirRegistroDePlacaEnVentannC a
;nna b
ViewBagoo 
.oo 
IdClienteGenericooo %
=oo& '

.oo5 6
Defaultoo6 =
.oo= >
IdClienteGenericooo> O
;ooO P
ViewBagpp 
.pp !
IdMedioDePagoEfectivopp )
=pp* +
MaestroSettingspp, ;
.pp; <
Defaultpp< C
.ppC D/
#IdDetalleMaestroMedioDepagoEfectivoppD g
;ppg h
ViewBagqq 
.qq "
IdTipoDocumentoFacturaqq *
=qq+ ,
MaestroSettingsqq- <
.qq< =
Defaultqq= D
.qqD E.
"IdDetalleMaestroComprobanteFacturaqqE g
;qqg h
ViewBagrr 
.rr 2
&IdTipoDocumentoCuandoClienteEsGenericorr :
=rr; <
TransaccionSettingsrr= P
.rrP Q
DefaultrrQ X
.rrX Y2
&IdTipoDocumentoCuandoClienteEsGenericorrY 
;	rr �
ViewBagss 
.ss .
"IdTipoDocumentoPorDefectoParaVentass 6
=ss7 8
TransaccionSettingsss9 L
.ssL M
DefaultssM T
.ssT U.
"IdTipoDocumentoPorDefectoParaVentassU w
;ssw x
ViewBagtt 
.tt #
IdTipoComprobantePedidott +
=tt, -
PedidoSettingstt. <
.tt< =
Defaulttt= D
.ttD E#
IdTipoComprobantePedidottE \
;tt\ ]
ViewBaguu 
.uu -
!IdTipoComprobanteEmitirPorDefectouu 5
=uu6 7
PedidoSettingsuu8 F
.uuF G
DefaultuuG N
.uuN O-
!IdTipoComprobanteEmitirPorDefectouuO p
;uup q
ViewBagvv 
.vv 
ImprimirPedidovv "
=vv# $
PedidoSettingsvv% 3
.vv3 4
Defaultvv4 ;
.vv; <
ImprimirPedidovv< J
;vvJ K
ViewBagww 
.ww !
EsPregeneracionPedidoww )
=ww* +
trueww, 0
;ww0 1
ViewBagxx 
.xx 
IdOrdenAPregenerarxx &
=xx' (
idOrdenxx) 0
;xx0 1
ViewBagyy 
.yy 
	WCPScriptyy 
=yy 

Neodynamicyy  *
.yy* +
SDKyy+ .
.yy. /
Webyy/ 2
.yy2 3
WebClientPrintyy3 A
.yyA B
CreateScriptyyB N
(yyN O
Urlzz 
.zz 
Actionzz 
(zz 
$strzz +
,zz+ ,
$strzz- @
,zz@ A
nullzzB F
,zzF G
HttpContextzzH S
.zzS T
RequestzzT [
.zz[ \
Urlzz\ _
.zz_ `
Schemezz` f
)zzf g
,zzg h
Url{{ 
.{{ 
Action{{ 
({{ 
$str{{ &
,{{& '
$str{{( 0
,{{0 1
null{{2 6
,{{6 7
HttpContext{{8 C
.{{C D
Request{{D K
.{{K L
Url{{L O
.{{O P
Scheme{{P V
){{V W
,{{W X
HttpContext{{Y d
.{{d e
Session{{e l
.{{l m
	SessionID{{m v
){{v w
;{{w x
return|| 
View|| 
(|| 
$str|| 
)||  
;||  !
}}} 	
public 

JsonResult 
ObtenerPedidos (
(( )
string) /
desde0 5
,5 6
string7 =
hasta> C
)C D
{
�� 	
try
�� 
{
�� 
DateTime
�� 

fechaDesde
�� #
=
��$ %
DateTime
��& .
.
��. /
Parse
��/ 4
(
��4 5
desde
��5 :
)
��: ;
;
��; <
DateTime
�� 

fechaHasta
�� #
=
��$ %
DateTime
��& .
.
��. /
Parse
��/ 4
(
��4 5
hasta
��5 :
+
��; <
$str
��= H
)
��H I
;
��I J
List
�� 
<
��  
ResumenOrdenPedido
�� '
>
��' (
ordenPedidos
��) 5
=
��6 7
pedidoLogica
��8 D
.
��D E$
ObtenerOrdenesDePedido
��E [
(
��[ \

fechaDesde
��\ f
,
��f g

fechaHasta
��h r
)
��r s
;
��s t
return
�� 
Json
�� 
(
�� 
ordenPedidos
�� (
)
��( )
;
��) *
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
ex
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� (
ObtenerParametrosDePedidos
�� 4
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
var
�� %
parametrosConfiguracion
�� +
=
��, -
new
��. 1#
ConfiguracionDePedido
��2 G
{
�� 
FechaActual
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
.
��< =
ToString
��= E
(
��E F
$str
��F [
)
��[ \
}
�� 
;
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !%
parametrosConfiguracion
��" 9
}
��: ;
)
��; <
;
��< =
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
ex
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� 

�� '
(
��' (!
DatosVentaIntegrada
��( ;
pedido
��< B
)
��B C
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
;
��& '
PrinterBuilder
�� 
printerBuilder
�� -
=
��. /
new
��0 3
PrinterBuilder
��4 B
(
��B C
)
��C D
;
��D E
if
�� 
(
�� 
pedido
�� 
.
�� 
Orden
��  
.
��  !
Id
��! #
>
��$ %
$num
��& '
)
��' (
{
�� 
result
�� 
=
�� 
pedidoLogica
�� )
.
��) *
EditarPedido
��* 6
(
��6 7
pedido
��7 =
,
��= >
ProfileData
��? J
(
��J K
)
��K L
)
��L M
;
��M N
}
�� 
else
�� 
{
�� 
result
�� 
=
�� 
pedidoLogica
�� )
.
��) *

��* 7
(
��7 8
pedido
��8 >
,
��> ?
ProfileData
��@ K
(
��K L
)
��L M
)
��M N
;
��N O
}
�� 
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 Z
)
��Z [
;
��[ \
var
�� 
ordenPedido
�� 
=
��  !
printerBuilder
��" 0
.
��0 1*
ArmarOrdenPedidoParaImprimir
��1 M
(
��M N
(
��N O

��O \
)
��\ ]
result
��] c
.
��c d
objeto
��d j
,
��j k
pedido
��l r
,
��r s
ProfileData
��t 
(�� �
)��� �
)��� �
;��� �
printerBuilder
�� 
.
�� :
,GuardarOrdenDePedidoParaImprimirEnAplicacion
�� K
(
��K L
ordenPedido
��L W
)
��W X
;
��X Y
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 V
)
��V W
;
��W X
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
data
��6 :
=
��; <
result
��= C
.
��C D
data
��D H
,
��H I 
result_description
��J \
=
��] ^
result
��_ e
.
��e f
title
��f k
,
��k l
IdOrden
��m t
=
��u v
result
��w }
.
��} ~
information��~ �
}��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
LogicException
��# 1
)
��1 2
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
LogicException
��? M
)
��M N
,
��N O
HttpStatusCode
��P ^
.
��^ _!
InternalServerError
��_ r
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
�� 
ex
�� 
)
��  
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M r
,
��r s
ex
��t v
)
��v w
)
��w x
,
��x y
HttpStatusCode��z �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
InvalidarPedido
�� )
(
��) *
int
��* -

��. ;
,
��; <
string
��= C
Observacion
��D O
)
��O P
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
;
��& '
result
�� 
=
�� 
pedidoLogica
�� %
.
��% &
InvalidarPedido
��& 5
(
��5 6

��6 C
,
��C D
Observacion
��E P
,
��P Q
ProfileData
��R ]
(
��] ^
)
��^ _
)
��_ `
;
��` a
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 V
)
��V W
;
��W X
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5 
result_description
��6 H
=
��I J
result
��K Q
.
��Q R
title
��R W
}
��X Y
)
��Y Z
;
��Z [
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
ex
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� %
ObtenerPedidoParaEditar
�� 1
(
��1 2
int
��2 5

��6 C
)
��C D
{
�� 	
try
�� 
{
�� 
var
�� 
OrdenPedido
�� 
=
��  !
pedidoLogica
��" .
.
��. /"
ObtenerOrdenDePedido
��/ C
(
��C D

��D Q
)
��Q R
;
��R S
List
�� 
<
�� )
Concepto_Negocio_Comercial_
�� 0
>
��0 1
	Conceptos
��2 ;
=
��< =
new
��> A
List
��B F
<
��F G)
Concepto_Negocio_Comercial_
��G b
>
��b c
(
��c d
)
��d e
;
��e f
foreach
�� 
(
�� 
var
�� 
item
�� !
in
��" $
OrdenPedido
��% 0
.
��0 1
Orden
��1 6
.
��6 7
Detalles
��7 ?
)
��? @
{
�� 
	Conceptos
�� 
.
�� 
Add
�� !
(
��! "
conceptoLogica
��" 0
.
��0 1<
.ObtenerConceptoDeNegocioComercialPorIdConcepto
��1 _
(
��_ `
ProfileData
��` k
(
��k l
)
��l m
,
��m n
item
��o s
.
��s t
Producto
��t |
.
��| }
Id
��} 
,�� �
true��� �
,��� �
true��� �
)��� �
)��� �
;��� �
}
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
OrdenPedido
��" -
=
��. /
OrdenPedido
��0 ;
,
��; <
	Conceptos
��= F
=
��G H
	Conceptos
��I R
}
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
�� 
ex
�� 
)
��  
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
ex
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� &
ObtenerDocumentoDePedido
�� 2
(
��2 3
long
��3 7
idOrdenDePedido
��8 G
)
��G H
{
�� 	
try
�� 
{
�� 

�� 

�� +
=
��, -
pedidoLogica
��. :
.
��: ;-
ObtenerOrdenDePedidoComprobante
��; Z
(
��Z [
idOrdenDePedido
��[ j
)
��j k
;
��k l
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� &
(
��& '
)
��' (
;
��( )
var
�� 

htmlString
�� 
=
��  '
ObtenerCadenaHtmlDePedido
��! :
(
��: ;

��; H
,
��H I
(
��J K
FormatoImpresion
��K [
)
��[ \
$num
��\ ]
,
��] ^
null
��_ c
,
��c d
sede
��e i
)
��i j
;
��j k
var
�� 
	respuesta
�� 
=
�� 
new
��  #
PedidoViewModel
��$ 3
(
��3 4

��4 A
,
��A B

htmlString
��C M
)
��M N
;
��N O
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
HttpContext
�� 
.
�� 
Response
�� $
.
��$ %

StatusCode
��% /
=
��0 1
$num
��2 5
;
��5 6
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
PdfDocument
�� 
ObtenerPdfPedido
�� +
(
��+ ,

��, 9

��: G
,
��G H6
(EstablecimientoComercialExtendidoConLogo
��I q
sede
��r v
,
��v w
byte
��x |
[
��| }
]
��} ~
qrBytes�� �
,��� � 
FormatoImpresion��� �
formato��� �
)��� �
{
�� 	
string
�� 

htmlString
�� 
=
�� '
ObtenerCadenaHtmlDePedido
��  9
(
��9 :

��: G
,
��G H
formato
��I P
,
��P Q
qrBytes
��R Y
,
��Y Z
sede
��[ _
)
��_ `
;
��` a
return
�� 
pdfUtil
�� 
.
�� !
ObtenerPdfDocumento
�� .
(
��. /

htmlString
��/ 9
,
��9 :
formato
��; B
)
��B C
;
��C D
}
�� 	
public
�� 
string
�� '
ObtenerCadenaHtmlDePedido
�� /
(
��/ 0

��0 =
ordenDeCotizacion
��> O
,
��O P
FormatoImpresion
��Q a
formato
��b i
,
��i j
byte
��k o
[
��o p
]
��p q
qrBytes
��r y
,
��y z7
(EstablecimientoComercialExtendidoConLogo��{ �
sede��� �
)��� �
{
�� 	

�� 

�� '
=
��( )
new
��* -

��. ;
(
��; <
)
��< =
;
��= >
string
�� 
result
�� 
=
�� 
$str
�� 
;
�� 
string
�� 
nombreVista
�� 
=
��  
$str
��! #
;
��# $
nombreVista
�� 
=
�� 
formato
�� !
==
��" $
FormatoImpresion
��% 5
.
��5 6
_80mm
��6 ;
?
��< =
$str
��> S
:
��T U
$str
��V k
;
��k l
result
�� 
=
�� 
HtmlStringBuilder
�� &
.
��& '%
RenderRazorViewToString
��' >
(
��> ?
nombreVista
��? J
,
��J K
new
��L O
DocumentoDePedido
��P a
(
��a b
ordenDeCotizacion
��b s
,
��s t
sede
��u y
,
��y z
new
��{ ~0
!EstablecimientoComercialExtendido�� �
(��� �!
ordenDeCotizacion��� �
.��� �
Transaccion��� �
(��� �
)��� �
.��� �
Actor_negocio2��� �
.��� �
Actor_negocio2��� �
)��� �
,��� �
qrBytes��� �
,��� �"
AplicacionSettings��� �
.��� �
Default��� �
.��� �&
MostrarCabeceraVoucher��� �
,��� �
(��� �0
 ModoImpresionCaracteristicasEnum��� �
)��� �
VentasSettings��� �
.��� �
Default��� �
.��� �,
modoImpresionCaracteristicas��� �
)��� �
,��� �
this��� �
)��� �
;��� �
return
�� 
result
�� 
;
�� 
}
�� 	
public
�� 

JsonResult
�� *
ObtenerHtmlDocumentoDePedido
�� 6
(
��6 7
long
��7 ;
idOrden
��< C
,
��C D
int
��E H
formato
��I P
)
��P Q
{
�� 	

�� 

�� '
=
��( )
pedidoLogica
��* 6
.
��6 7-
ObtenerOrdenDePedidoComprobante
��7 V
(
��V W
idOrden
��W ^
)
��^ _
;
��_ `
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� "
(
��" #
)
��# $
;
��$ %
var
�� 

htmlString
�� 
=
�� '
ObtenerCadenaHtmlDePedido
�� 6
(
��6 7

��7 D
,
��D E
(
��F G
FormatoImpresion
��G W
)
��W X
formato
��X _
,
��_ `
null
��a e
,
��e f
sede
��g k
)
��k l
;
��l m
return
�� 
Json
�� 
(
�� 

htmlString
�� "
)
��" #
;
��# $
}
�� 	
public
�� 

JsonResult
�� (
ObtenerFormatosDeImpresion
�� 4
(
��4 5
)
��5 6
{
�� 	
var
�� 
valoresEnum
�� 
=
�� 
Enum
�� "
.
��" #
	GetValues
��# ,
(
��, -
typeof
��- 3
(
��3 4
FormatoImpresion
��4 D
)
��D E
)
��E F
;
��F G
List
�� 
<
�� $
ComboGenericoViewModel
�� '
>
��' (
formatos
��) 1
=
��2 3
new
��4 7
List
��8 <
<
��< =$
ComboGenericoViewModel
��= S
>
��S T
(
��T U
)
��U V
;
��V W
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
valoresEnum
��! ,
)
��, -
{
�� 
if
�� 
(
�� 
(
�� 
int
�� 
)
�� 
item
�� 
!=
��  
(
��! "
int
��" %
)
��% &
FormatoImpresion
��& 6
.
��6 7
_56mm
��7 <
)
��< =
formatos
�� 
.
�� 
Add
��  
(
��  !
new
��! $$
ComboGenericoViewModel
��% ;
(
��; <
(
��< =
int
��= @
)
��@ A
item
��A E
,
��E F
(
��G H
(
��H I
FormatoImpresion
��I Y
)
��Y Z
Convert
��Z a
.
��a b
ToInt32
��b i
(
��i j
item
��j n
)
��n o
)
��o p
.
��p q
ToString
��q y
(
��y z
)
��z {
)
��{ |
)
��| }
;
��} ~
}
�� 
return
�� 
Json
�� 
(
�� 
formatos
��  
)
��  !
;
��! "
}
�� 	
public
�� 

JsonResult
�� &
ObtenerTipoDeComprobante
�� 2
(
��2 3
int
��3 6!
idTipoDeComprobante
��7 J
)
��J K
{
�� 	
try
�� 
{
�� 
TipoDeComprobante
�� !
tipoDeComprobante
��" 3
=
��4 5"
_configuracionLogica
��6 J
.
��J K&
ObtenerTipoDeComprobante
��K c
(
��c d!
idTipoDeComprobante
��d w
)
��w x
;
��x y.
 RegistroTipoComprobanteViewModel
�� 0(
tipoDeComprobanteViewModel
��1 K
=
��L M
new
��N Q.
 RegistroTipoComprobanteViewModel
��R r
(
��r s 
tipoDeComprobante��s �
)��� �
;��� �
return
�� 
Json
�� 
(
�� (
tipoDeComprobanteViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
void
�� 
	PrintFile
�� 
(
�� 
long
�� "
idOperacion
��# .
,
��. /
int
��0 3

��4 A
)
��A B
{
�� 	
PrinterBuilder
�� 
printerBuilder
�� )
=
��* +
new
��, /
PrinterBuilder
��0 >
(
��> ?
)
��? @
;
��@ A6
(EstablecimientoComercialExtendidoConLogo
�� 4
sede
��5 9
=
��: ;
ObtenerSede
��< G
(
��G H
)
��H I
;
��I J
printerBuilder
�� 
.
�� 
ImprimirOperacion
�� ,
(
��, -
idOperacion
��- 8
,
��8 9

��: G
,
��G H
sede
��I M
,
��M N
this
��O S
,
��S T
pedidoLogica
��U a
,
��a b
operacionLogica
��c r
,
��r s!
actorNegocioLogica��t �
,��� �

,��� �,
facturacionElectronicaLogica��� �
,��� �
barCodeUtil��� �
,��� �
pdfUtil��� �
)��� �
;��� �
}
�� 	
public
�� 

JsonResult
�� 
ConfirmarPedido
�� )
(
��) *!
DatosVentaIntegrada
��* =
pedido
��> D
)
��D E
{
�� 	
try
�� 
{
�� 
PrinterBuilder
�� 
printerBuilder
�� -
=
��. /
new
��0 3
PrinterBuilder
��4 B
(
��B C
)
��C D
;
��D E
OperationResult
�� 
result
��  &
=
��' (
pedidoLogica
��) 5
.
��5 6
ConfirmarPedido
��6 E
(
��E F
ModoOperacionEnum
��F W
.
��W X
PorMostrador
��X d
,
��d e
ProfileData
��f q
(
��q r
)
��r s
,
��s t
pedido
��u {
)
��{ |
;
��| }
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
result
��0 6
,
��6 7
$str
��8 ]
)
��] ^
;
��^ _
var
�� 

ordenVenta
�� 
=
��  
printerBuilder
��! /
.
��/ 0)
ArmarOrdenVentaParaImprimir
��0 K
(
��K L
(
��L M
OrdenDeVenta
��M Y
)
��Y Z
result
��Z `
.
��` a
objeto
��a g
,
��g h
pedido
��i o
,
��o p
ProfileData
��q |
(
��| }
)
��} ~
)
��~ 
;�� �
printerBuilder
�� 
.
�� 9
+GuardarOrdenDeVentaParaImprimirEnAplicacion
�� J
(
��J K

ordenVenta
��K U
)
��U V
;
��V W
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
data
��6 :
=
��; <
result
��= C
.
��C D
data
��D H
,
��H I 
result_description
��J \
=
��] ^
result
��_ e
.
��e f
title
��f k
,
��k l
IdOrden
��m t
=
��u v
result
��w }
.
��} ~
information��~ �
}��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
le
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
le
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M q
,
��q r
e
��s t
)
��t u
)
��u v
,
��v w
HttpStatusCode��x �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
}
�� 
}�� ��
oD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Venta\VentaReportes_PorGruposController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class -
!VentaReportes_PorGruposController :
:; <
BaseController= K
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly  
IVentaReporte_Logica /
ventaReporte_Logica0 C
;C D
	protected 
readonly $
ICentroDeAtencion_Logica 3#
centroDeAtencion_Logica4 K
;K L
public -
!VentaReportes_PorGruposController 0
(0 1
)1 2
:3 4
base5 9
(9 :
): ;
{   	
operacionLogica!! 
=!! 
Dependencia!! )
.!!) *
Resolve!!* 1
<!!1 2
IOperacionLogica!!2 B
>!!B C
(!!C D
)!!D E
;!!E F
actorNegocioLogica"" 
=""  
Dependencia""! ,
."", -
Resolve""- 4
<""4 5
IActorNegocioLogica""5 H
>""H I
(""I J
)""J K
;""K L
conceptoLogica## 
=## 
Dependencia## (
.##( )
Resolve##) 0
<##0 1
IConceptoLogica##1 @
>##@ A
(##A B
)##B C
;##C D
ventaReporte_Logica$$ 
=$$  !
Dependencia$$" -
.$$- .
Resolve$$. 5
<$$5 6 
IVentaReporte_Logica$$6 J
>$$J K
($$K L
)$$L M
;$$M N#
centroDeAtencion_Logica%% #
=%%$ %
Dependencia%%& 1
.%%1 2
Resolve%%2 9
<%%9 :$
ICentroDeAtencion_Logica%%: R
>%%R S
(%%S T
)%%T U
;%%U V
}&& 	
public(( 
ActionResult(( -
!PorGrupos_VentasPorFamiliasGrupos(( =
(((= >
int((> A
idPuntoVenta((B N
,((N O
string((P V
nombrePuntoVenta((W g
,((g h
string((i o"
nombreEstablecimiento	((p �
,
((� �
DateTime
((� �

fechaDesde
((� �
,
((� �
DateTime
((� �

fechaHasta
((� �
,
((� �
bool
((� �
todasLasFamilias
((� �
,
((� �
[
((� �
System
((� �
.
((� �
Web
((� �
.
((� �
Http
((� �
.
((� �
FromUri
((� �
]
((� �
int
((� �
[
((� �
]
((� �
idsFamilias
((� �
,
((� �
string
((� �
nombresFamilias
((� �
,
((� �
bool
((� �
todosLosGrupos
((� �
,
((� �
[
((� �
System
((� �
.
((� �
Web
((� �
.
((� �
Http
((� �
.
((� �
FromUri
((� �
]
((� �
int
((� �
[
((� �
]
((� �
	idsGrupos
((� �
,
((� �
string
((� �

((� �
)
((� �
{)) 	
var** 
	rptviewer** 
=** 4
(GenerarPorGrupos_VentasPorFamiliasGrupos** D
(**D E
idPuntoVenta**E Q
,**Q R
nombrePuntoVenta**S c
,**c d!
nombreEstablecimiento**e z
,**z {

fechaDesde	**| �
,
**� �

fechaHasta
**� �
,
**� �
todasLasFamilias
**� �
,
**� �
idsFamilias
**� �
,
**� �
nombresFamilias
**� �
,
**� �
todosLosGrupos
**� �
,
**� �
	idsGrupos
**� �
,
**� �

**� �
,
**� �
true
**� �
)
**� �
;
**� �
ViewBag++ 
.++ 
ReportViewer++  
=++! "
	rptviewer++# ,
;++, -
return,, 
View,, 
(,, 
$str,, -
),,- .
;,,. /
}-- 	
public.. 
ReportViewer.. 4
(GenerarPorGrupos_VentasPorFamiliasGrupos.. D
(..D E
int..E H
idPuntoVenta..I U
,..U V
string..W ]
nombrePuntoVenta..^ n
,..n o
string..p v"
nombreEstablecimiento	..w �
,
..� �
DateTime
..� �

fechaDesde
..� �
,
..� �
DateTime
..� �

fechaHasta
..� �
,
..� �
bool
..� �
todasLasFamilias
..� �
,
..� �
[
..� �
System
..� �
.
..� �
Web
..� �
.
..� �
Http
..� �
.
..� �
FromUri
..� �
]
..� �
int
..� �
[
..� �
]
..� �
idsFamilias
..� �
,
..� �
string
..� �
nombresFamilias
..� �
,
..� �
bool
..� �
todosLosGrupos
..� �
,
..� �
[
..� �
System
..� �
.
..� �
Web
..� �
.
..� �
Http
..� �
.
..� �
FromUri
..� �
]
..� �
int
..� �
[
..� �
]
..� �
	idsGrupos
..� �
,
..� �
string
..� �

..� �
,
..� �
bool
..� �
fromRequest
..� �
)
..� �
{// 	
List00 
<00 !
OperacionFamiliaGrupo00 &
>00& '#
ventasPorFamiliasGrupos00( ?
=00@ A
ventaReporte_Logica00B U
.00U V*
ObtenerVentasPorFamiliasGrupos00V t
(00t u
idPuntoVenta	00u �
,
00� �

fechaDesde
00� �
,
00� �

fechaHasta
00� �
,
00� �
todasLasFamilias
00� �
,
00� �
idsFamilias
00� �
,
00� �
todosLosGrupos
00� �
,
00� �
	idsGrupos
00� �
)
00� �
;
00� �
var11 
parametrosBasicos11 !
=11" #$
ObtenerParametrosBasicos11$ <
(11< =
)11= >
;11> ?
ReportParameter22 %
parametroNombrePuntoVenta22 5
=226 7
new228 ;
ReportParameter22< K
(22K L
$str22L ^
,22^ _
nombrePuntoVenta22` p
)22p q
;22q r
ReportParameter33 *
parametroNombreEstablecimiento33 :
=33; <
new33= @
ReportParameter33A P
(33P Q
$str33Q h
,33h i!
nombreEstablecimiento33j 
)	33 �
;
33� �
ReportParameter44 
parametroFechaDesde44 /
=440 1
new442 5
ReportParameter446 E
(44E F
$str44F R
,44R S

fechaDesde44T ^
.44^ _
ToString44_ g
(44g h
)44h i
)44i j
;44j k
ReportParameter55 
parametroFechaHasta55 /
=550 1
new552 5
ReportParameter556 E
(55E F
$str55F R
,55R S

fechaHasta55T ^
.55^ _
ToString55_ g
(55g h
)55h i
)55i j
;55j k
var66 
	rptviewer66 
=66 
new66 
ReportViewer66  ,
(66, -
)66- .
{66/ 0
ProcessingMode661 ?
=66@ A
ProcessingMode66B P
.66P Q
Local66Q V
,66V W
SizeToReportContent66X k
=66l m
true66n r
,66r s
Width66t y
=66z {
Unit	66| �
.
66� �

Percentage
66� �
(
66� �
$num
66� �
)
66� �
,
66� �
Height
66� �
=
66� �
Unit
66� �
.
66� �

Percentage
66� �
(
66� �
$num
66� �
)
66� �
}
66� �
;
66� �
string77 
path77 
=77 
$str77 `
;77` a
	rptviewer88 
.88 
LocalReport88 !
.88! "

ReportPath88" ,
=88- .
fromRequest88/ :
?88; <
Request88= D
.88D E
MapPath88E L
(88L M
Request88M T
.88T U
ApplicationPath88U d
)88d e
+88f g
path88h l
:88m n
HostingEnvironment	88o �
.
88� �
MapPath
88� �
(
88� �
path
88� �
)
88� �
;
88� �
	rptviewer99 
.99 
LocalReport99 !
.99! "

(99/ 0
new990 3
ReportParameter994 C
[99C D
]99D E
{99F G
parametrosBasicos:: !
.::! "

NombreSede::" ,
,::, -
parametrosBasicos;; !
.;;! "
FechaActualSistema;;" 4
,;;4 5
parametrosBasicos<< !
.<<! "
LogoSede<<" *
,<<* +
parametrosBasicos== !
.==! "
Usuario==" )
,==) *%
parametroNombrePuntoVenta>> )
,>>) **
parametroNombreEstablecimiento?? .
,??. /
parametroFechaDesde@@ #
,@@# $
parametroFechaHastaAA #
,AA# $
}BB 
)BB
;BB 
ReportDataSourceCC #
rptdatasourceFacturadasCC 4
=CC5 6
newCC7 :
ReportDataSourceCC; K
(CCK L
$strCCL l
,CCl m$
ventasPorFamiliasGrupos	CCn �
)
CC� �
;
CC� �
	rptviewerDD 
.DD 
LocalReportDD !
.DD! "
DataSourcesDD" -
.DD- .
AddDD. 1
(DD1 2#
rptdatasourceFacturadasDD2 I
)DDI J
;DDJ K
returnEE 
	rptviewerEE 
;EE 
}FF 	
publicHH 
ActionResultHH %
PorGrupos_VentasPorGruposHH 5
(HH5 6
intHH6 9
idPuntoVentaHH: F
,HHF G
stringHHH N
nombrePuntoVentaHHO _
,HH_ `
stringHHa g!
nombreEstablecimientoHHh }
,HH} ~
DateTime	HH �

fechaDesde
HH� �
,
HH� �
DateTime
HH� �

fechaHasta
HH� �
,
HH� �
bool
HH� �
todosLosGrupos
HH� �
,
HH� �
[
HH� �
System
HH� �
.
HH� �
Web
HH� �
.
HH� �
Http
HH� �
.
HH� �
FromUri
HH� �
]
HH� �
int
HH� �
[
HH� �
]
HH� �
	idsGrupos
HH� �
,
HH� �
string
HH� �

HH� �
)
HH� �
{II 	
varJJ 
	rptviewerJJ 
=JJ ,
 GenerarPorGrupos_VentasPorGruposJJ <
(JJ< =
idPuntoVentaJJ= I
,JJI J
nombrePuntoVentaJJK [
,JJ[ \!
nombreEstablecimientoJJ] r
,JJr s

fechaDesdeJJt ~
,JJ~ 

fechaHasta
JJ� �
,
JJ� �
todosLosGrupos
JJ� �
,
JJ� �
	idsGrupos
JJ� �
,
JJ� �

JJ� �
,
JJ� �
true
JJ� �
)
JJ� �
;
JJ� �
ViewBagKK 
.KK 
ReportViewerKK  
=KK! "
	rptviewerKK# ,
;KK, -
returnLL 
ViewLL 
(LL 
$strLL -
)LL- .
;LL. /
}MM 	
publicNN 
ReportViewerNN ,
 GenerarPorGrupos_VentasPorGruposNN <
(NN< =
intNN= @
idPuntoVentaNNA M
,NNM N
stringNNO U
nombrePuntoVentaNNV f
,NNf g
stringNNh n"
nombreEstablecimiento	NNo �
,
NN� �
DateTime
NN� �

fechaDesde
NN� �
,
NN� �
DateTime
NN� �

fechaHasta
NN� �
,
NN� �
bool
NN� �
todosLosGrupos
NN� �
,
NN� �
[
NN� �
System
NN� �
.
NN� �
Web
NN� �
.
NN� �
Http
NN� �
.
NN� �
FromUri
NN� �
]
NN� �
int
NN� �
[
NN� �
]
NN� �
	idsGrupos
NN� �
,
NN� �
string
NN� �

NN� �
,
NN� �
bool
NN� �
fromRequest
NN� �
)
NN� �
{OO 	
ListPP 
<PP 
OperacionGrupoPP 
>PP  
ventasPorGruposPP! 0
=PP1 2
ventaReporte_LogicaPP3 F
.PPF G"
ObtenerVentasPorGruposPPG ]
(PP] ^
idPuntoVentaPP^ j
,PPj k

fechaDesdePPl v
,PPv w

fechaHasta	PPx �
,
PP� �
todosLosGrupos
PP� �
,
PP� �
	idsGrupos
PP� �
)
PP� �
;
PP� �
varQQ 
parametrosBasicosQQ !
=QQ" #$
ObtenerParametrosBasicosQQ$ <
(QQ< =
)QQ= >
;QQ> ?
ReportParameterRR %
parametroNombrePuntoVentaRR 5
=RR6 7
newRR8 ;
ReportParameterRR< K
(RRK L
$strRRL ^
,RR^ _
nombrePuntoVentaRR` p
)RRp q
;RRq r
ReportParameterSS *
parametroNombreEstablecimientoSS :
=SS; <
newSS= @
ReportParameterSSA P
(SSP Q
$strSSQ h
,SSh i!
nombreEstablecimientoSSj 
)	SS �
;
SS� �
ReportParameterTT 
parametroFechaDesdeTT /
=TT0 1
newTT2 5
ReportParameterTT6 E
(TTE F
$strTTF R
,TTR S

fechaDesdeTTT ^
.TT^ _
ToStringTT_ g
(TTg h
)TTh i
)TTi j
;TTj k
ReportParameterUU 
parametroFechaHastaUU /
=UU0 1
newUU2 5
ReportParameterUU6 E
(UUE F
$strUUF R
,UUR S

fechaHastaUUT ^
.UU^ _
ToStringUU_ g
(UUg h
)UUh i
)UUi j
;UUj k
ReportParameterVV "
parametroNombresGruposVV 2
=VV3 4
newVV5 8
ReportParameterVV9 H
(VVH I
$strVVI X
,VVX Y

??VVh j
$strVVk r
)VVr s
;VVs t
varWW 
	rptviewerWW 
=WW 
newWW 
ReportViewerWW  ,
(WW, -
)WW- .
{WW/ 0
ProcessingModeWW1 ?
=WW@ A
ProcessingModeWWB P
.WWP Q
LocalWWQ V
,WWV W
SizeToReportContentWWX k
=WWl m
trueWWn r
,WWr s
WidthWWt y
=WWz {
Unit	WW| �
.
WW� �

Percentage
WW� �
(
WW� �
$num
WW� �
)
WW� �
,
WW� �
Height
WW� �
=
WW� �
Unit
WW� �
.
WW� �

Percentage
WW� �
(
WW� �
$num
WW� �
)
WW� �
}
WW� �
;
WW� �
stringXX 
pathXX 
=XX 
$strXX X
;XXX Y
	rptviewerYY 
.YY 
LocalReportYY !
.YY! "

ReportPathYY" ,
=YY- .
fromRequestYY/ :
?YY; <
RequestYY= D
.YYD E
MapPathYYE L
(YYL M
RequestYYM T
.YYT U
ApplicationPathYYU d
)YYd e
+YYf g
pathYYh l
:YYm n
HostingEnvironment	YYo �
.
YY� �
MapPath
YY� �
(
YY� �
path
YY� �
)
YY� �
;
YY� �
	rptviewerZZ 
.ZZ 
LocalReportZZ !
.ZZ! "

(ZZ/ 0
newZZ0 3
ReportParameterZZ4 C
[ZZC D
]ZZD E
{ZZF G
parametrosBasicos[[ !
.[[! "

NombreSede[[" ,
,[[, -
parametrosBasicos\\ !
.\\! "
FechaActualSistema\\" 4
,\\4 5
parametrosBasicos]] !
.]]! "
LogoSede]]" *
,]]* +
parametrosBasicos^^ !
.^^! "
Usuario^^" )
,^^) *%
parametroNombrePuntoVenta__ )
,__) *
parametroFechaDesde`` #
,``# $*
parametroNombreEstablecimientoaa .
,aa. /
parametroFechaHastabb #
,bb# $"
parametroNombresGruposcc &
}dd 
)dd
;dd 
ReportDataSourceee #
rptdatasourceFacturadasee 4
=ee5 6
newee7 :
ReportDataSourceee; K
(eeK L
$streeL d
,eed e
ventasPorGruposeef u
)eeu v
;eev w
	rptviewerff 
.ff 
LocalReportff !
.ff! "
DataSourcesff" -
.ff- .
Addff. 1
(ff1 2#
rptdatasourceFacturadasff2 I
)ffI J
;ffJ K
returngg 
	rptviewergg 
;gg 
}hh 	
publicjj 
ActionResultjj -
!PorGrupos_VentasPorGrupoDetalladojj =
(jj= >
intjj> A
idPuntoVentajjB N
,jjN O
stringjjP V
nombrePuntoVentajjW g
,jjg h
stringjji o"
nombreEstablecimiento	jjp �
,
jj� �
DateTime
jj� �

fechaDesde
jj� �
,
jj� �
DateTime
jj� �

fechaHasta
jj� �
,
jj� �
int
jj� �
idGrupo
jj� �
,
jj� �
string
jj� �
nombreGrupo
jj� �
)
jj� �
{kk 	
varll 
	rptviewerll 
=ll 4
(GenerarPorGrupos_VentasPorGrupoDetalladoll D
(llD E
idPuntoVentallE Q
,llQ R
nombrePuntoVentallS c
,llc d!
nombreEstablecimientolle z
,llz {

fechaDesde	ll| �
,
ll� �

fechaHasta
ll� �
,
ll� �
idGrupo
ll� �
,
ll� �
nombreGrupo
ll� �
,
ll� �
true
ll� �
)
ll� �
;
ll� �
ViewBagmm 
.mm 
ReportViewermm  
=mm! "
	rptviewermm# ,
;mm, -
returnnn 
Viewnn 
(nn 
$strnn -
)nn- .
;nn. /
}oo 	
publicpp 
ReportViewerpp 4
(GenerarPorGrupos_VentasPorGrupoDetalladopp D
(ppD E
intppE H
idPuntoVentappI U
,ppU V
stringppW ]
nombrePuntoVentapp^ n
,ppn o
stringppp v"
nombreEstablecimiento	ppw �
,
pp� �
DateTime
pp� �

fechaDesde
pp� �
,
pp� �
DateTime
pp� �

fechaHasta
pp� �
,
pp� �
int
pp� �
idGrupo
pp� �
,
pp� �
string
pp� �
nombreGrupo
pp� �
,
pp� �
bool
pp� �
fromRequest
pp� �
)
pp� �
{qq 	
Listrr 
<rr #
OperacionGrupoDetalladorr (
>rr( )#
ventasPorGrupoDetalladorr* A
=rrB C
ventaReporte_LogicarrD W
.rrW X*
ObtenerVentasPorGrupoDetalladorrX v
(rrv w
idPuntoVenta	rrw �
,
rr� �

fechaDesde
rr� �
,
rr� �

fechaHasta
rr� �
,
rr� �
idGrupo
rr� �
)
rr� �
;
rr� �
varss 
parametrosBasicosss !
=ss" #$
ObtenerParametrosBasicosss$ <
(ss< =
)ss= >
;ss> ?
ReportParametertt %
parametroNombrePuntoVentatt 5
=tt6 7
newtt8 ;
ReportParametertt< K
(ttK L
$strttL ^
,tt^ _
nombrePuntoVentatt` p
)ttp q
;ttq r
ReportParameteruu *
parametroNombreEstablecimientouu :
=uu; <
newuu= @
ReportParameteruuA P
(uuP Q
$struuQ h
,uuh i!
nombreEstablecimientouuj 
)	uu �
;
uu� �
ReportParametervv 
parametroFechaDesdevv /
=vv0 1
newvv2 5
ReportParametervv6 E
(vvE F
$strvvF R
,vvR S

fechaDesdevvT ^
.vv^ _
ToStringvv_ g
(vvg h
)vvh i
)vvi j
;vvj k
ReportParameterww 
parametroFechaHastaww /
=ww0 1
newww2 5
ReportParameterww6 E
(wwE F
$strwwF R
,wwR S

fechaHastawwT ^
.ww^ _
ToStringww_ g
(wwg h
)wwh i
)wwi j
;wwj k
ReportParameterxx  
parametroNombreGrupoxx 0
=xx1 2
newxx3 6
ReportParameterxx7 F
(xxF G
$strxxG T
,xxT U
nombreGrupoxxV a
)xxa b
;xxb c
ReportParameteryy &
parametroNombreResponsableyy 6
=yy7 8
newyy9 <
ReportParameteryy= L
(yyL M
$stryyM `
,yy` a#
ventasPorGrupoDetalladoyyb y
.yyy z
FirstOrDefault	yyz �
(
yy� �
)
yy� �
==
yy� �
null
yy� �
?
yy� �
$str
yy� �
:
yy� �%
ventasPorGrupoDetallado
yy� �
.
yy� �
FirstOrDefault
yy� �
(
yy� �
)
yy� �
.
yy� �
NombreResponsable
yy� �
)
yy� �
;
yy� �
varzz 
	rptviewerzz 
=zz 
newzz 
ReportViewerzz  ,
(zz, -
)zz- .
{zz/ 0
ProcessingModezz1 ?
=zz@ A
ProcessingModezzB P
.zzP Q
LocalzzQ V
,zzV W
SizeToReportContentzzX k
=zzl m
truezzn r
,zzr s
Widthzzt y
=zzz {
Unit	zz| �
.
zz� �

Percentage
zz� �
(
zz� �
$num
zz� �
)
zz� �
,
zz� �
Height
zz� �
=
zz� �
Unit
zz� �
.
zz� �

Percentage
zz� �
(
zz� �
$num
zz� �
)
zz� �
}
zz� �
;
zz� �
string{{ 
path{{ 
={{ 
$str{{ `
;{{` a
	rptviewer|| 
.|| 
LocalReport|| !
.||! "

ReportPath||" ,
=||- .
fromRequest||/ :
?||; <
Request||= D
.||D E
MapPath||E L
(||L M
Request||M T
.||T U
ApplicationPath||U d
)||d e
+||f g
path||h l
:||m n
HostingEnvironment	||o �
.
||� �
MapPath
||� �
(
||� �
path
||� �
)
||� �
;
||� �
	rptviewer}} 
.}} 
LocalReport}} !
.}}! "

(}}/ 0
new}}0 3
ReportParameter}}4 C
[}}C D
]}}D E
{}}F G
parametrosBasicos~~ !
.~~! "

NombreSede~~" ,
,~~, -
parametrosBasicos !
.! "
FechaActualSistema" 4
,4 5
parametrosBasicos
�� !
.
��! "
LogoSede
��" *
,
��* +
parametrosBasicos
�� !
.
��! "
Usuario
��" )
,
��) *'
parametroNombrePuntoVenta
�� )
,
��) *!
parametroFechaDesde
�� #
,
��# $,
parametroNombreEstablecimiento
�� .
,
��. /!
parametroFechaHasta
�� #
,
��# $"
parametroNombreGrupo
�� $
,
��$ %(
parametroNombreResponsable
�� *
}
�� 
)
��
;
�� 
ReportDataSource
�� %
rptdatasourceFacturadas
�� 4
=
��5 6
new
��7 :
ReportDataSource
��; K
(
��K L
$str
��L l
,
��l m&
ventasPorGrupoDetallado��n �
)��� �
;��� �
	rptviewer
�� 
.
�� 
LocalReport
�� !
.
��! "
DataSources
��" -
.
��- .
Add
��. 1
(
��1 2%
rptdatasourceFacturadas
��2 I
)
��I J
;
��J K
return
�� 
	rptviewer
�� 
;
�� 
}
�� 	
}
�� 
}�� �
eD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Core\Venta\VentaReportesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

partial 
class #
VentaReportesController 0
:1 2
BaseController3 A
{ 
	protected 
readonly 
IOperacionLogica +
operacionLogica, ;
;; <
	protected 
readonly 
IActorNegocioLogica .
actorNegocioLogica/ A
;A B
	protected 
readonly 
IConceptoLogica *
conceptoLogica+ 9
;9 :
	protected 
readonly  
IVentaReporte_Logica / 
ventaReportingLogica0 D
;D E
	protected 
readonly $
ICentroDeAtencion_Logica 3#
centroDeAtencion_Logica4 K
;K L
public #
VentaReportesController &
(& '
)' (
:) *
base+ /
(/ 0
)0 1
{ 	
operacionLogica 
= 
Dependencia )
.) *
Resolve* 1
<1 2
IOperacionLogica2 B
>B C
(C D
)D E
;E F
actorNegocioLogica   
=    
Dependencia  ! ,
.  , -
Resolve  - 4
<  4 5
IActorNegocioLogica  5 H
>  H I
(  I J
)  J K
;  K L
conceptoLogica!! 
=!! 
Dependencia!! (
.!!( )
Resolve!!) 0
<!!0 1
IConceptoLogica!!1 @
>!!@ A
(!!A B
)!!B C
;!!C D 
ventaReportingLogica""  
=""! "
Dependencia""# .
."". /
Resolve""/ 6
<""6 7 
IVentaReporte_Logica""7 K
>""K L
(""L M
)""M N
;""N O#
centroDeAtencion_Logica## #
=##$ %
Dependencia##& 1
.##1 2
Resolve##2 9
<##9 :$
ICentroDeAtencion_Logica##: R
>##R S
(##S T
)##T U
;##U V
}$$ 	
[&& 	
	Authorize&&	 
(&& 
Roles&& 
=&& 
$str&& C
)&&C D
]&&D E
public'' 
ActionResult'' 
	Principal'' %
(''% &
)''& '
{(( 	
ViewBag)) 
.)) 
Data)) 
=))  
ventaReportingLogica)) /
.))/ 0,
 ObtenerDatosParaReportePrincipal))0 P
())P Q
ProfileData))Q \
())\ ]
)))] ^
)))^ _
;))_ `
return** 
View** 
(** 
)** 
;** 
}++ 	
},, 
}-- �
aD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CorreccionInventarioController.cs
	namespace		 	
Tsp		
 
.		
Sigescom		 
.		 
WebApplication		 %
.		% &
Controllers		& 1
{

 
public 

class *
CorreccionInventarioController /
:0 1
BaseController2 @
{ 
	protected 
readonly "
IAlmacenReporte_Logica 1"
almacenReportingLogica3 I
;I J
	protected 
readonly '
IInventarioHistorico_Logica 6%
inventarioHistoricoLogica7 P
;P Q
	protected 
readonly $
IInventarioActual_Logica 3"
inventarioActualLogica4 J
;J K
public *
CorreccionInventarioController -
(- .
). /
:0 1
base2 6
(6 7
)7 8
{ 	"
inventarioActualLogica "
=# $
Dependencia% 0
.0 1
Resolve1 8
<8 9$
IInventarioActual_Logica9 Q
>Q R
(R S
)S T
;T U%
inventarioHistoricoLogica %
=& '
Dependencia( 3
.3 4
Resolve4 ;
<; <'
IInventarioHistorico_Logica< W
>W X
(X Y
)Y Z
;Z ["
almacenReportingLogica "
=# $
Dependencia% 0
.0 1
Resolve1 8
<8 9"
IAlmacenReporte_Logica9 O
>O P
(P Q
)Q R
;R S
} 
public 

ActionResult 
Corregir  
(  !
)! "
{ 	
return 
View 
( 
) 
; 
} 	
}   
}!! �

uD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\HotelApiControllers\CatalogoHabitacionesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
.1 2
HotelApiControllers2 E
{ 
public

class
CatalogoHabitacionesController
:

{ 
private 
readonly 
IHotelLogica %
hotelLogica& 1
;1 2
List 
< 
RoomType
> 
RoomCatalog "
;" #
public *
CatalogoHabitacionesController -
(- .
). /
{0 1
hotelLogica 
= 
Dependencia %
.% &
Resolve& -
<- .
IHotelLogica. :
>: ;
(; <
)< =
;= >
} 	
[ 	
HttpGet	 
] 
public 
List 
< 
RoomType 
> 
CatologoRoomType .
(. /
)/ 0
{ 	
return 
RoomCatalog 
; 
} 	
} 
} �&
�D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\HotelApiControllers\Reserva\HabitacionesDisponiblesController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
.1 2
ApiControllers2 @
{ 
[
Route

(
 
$str
)
]
public 

class -
!HabitacionesDisponiblesController 2
:3 4

{ 
private 
readonly 
IHotelLogica %
hotelLogica& 1
;1 2
List 
< 
RoomType
> 

roomsTypes !
;! "
List 
< 
RoomType
> 
availableRooms %
;% &
public -
!HabitacionesDisponiblesController 0
(0 1
)1 2
{ 	
hotelLogica 
= 
Dependencia %
.% &
Resolve& -
<- .
IHotelLogica. :
>: ;
(; <
)< =
;= >
availableRooms 
= 
new  
List! %
<% &
RoomType& .
>. /
(/ 0
)0 1
{   
new!! 
RoomType!! 
(!! 
)!! 
{!! 
Id!!  
=!!  !
$num!!! "
,!!" #
Name!!# '
=!!' (
$str!!( 5
,!!5 6

PriceValue!!6 @
=!!@ A
$num!!A G
,!!G H
AvailabilityAmount!!I [
=!![ \
$num!!\ ]
}!!] ^
,!!^ _
new"" 
RoomType"" 
("" 
)"" 
{""  
Id""  "
=""" #
$num""# $
,""$ %
Name""% )
="") *
$str""* 5
,""5 6

PriceValue""6 @
=""@ A
$num""A D
,""D E
AvailabilityAmount""F X
=""X Y
$num""Y Z
}""Z [
,""[ \
new## 
RoomType## 
(## 
)## 
{##  
Id##  "
=##" #
$num### $
,##$ %
Name##% )
=##) *
$str##* 2
,##2 3

PriceValue##3 =
=##= >
$num##> A
,##A B
AvailabilityAmount##C U
=##U V
$num##V W
}##W X
,##X Y
new$$ 
RoomType$$ 
($$ 
)$$ 
{$$  
Id$$  "
=$$" #
$num$$# $
,$$$ %
Name$$% )
=$$) *
$str$$* 3
,$$3 4

PriceValue$$4 >
=$$> ?
$num$$? F
,$$F G
AvailabilityAmount$$H Z
=$$Z [
$num$$[ \
}$$\ ]
}%% 
;%%
}&& 	
[,, 	
HttpGet,,	 
],, 
[-- 	
Route--	 
(-- 
$str-- #
)--# $
]--$ %
public.. 
string.. 
GetAvailableRoomss.. (
(..( )
int..) ,
numero..- 3
)..3 4
{// 	
return00 
(00 
$str00 
)00 
;00 
}11 	
[44 	
HttpPost44	 
]44 
public55 
IHttpActionResult55  
AvailableRooms55! /
(55/ 0
DateBooking550 ;
dateBooking55< G
)55G H
{66 	
try77 
{88 
if99 
(99 

ModelState99 
.99 
IsValid99 &
)99& '
{:: 

roomsTypes;; 
=;;  
hotelLogica;;! ,
.;;, -'
ObtenerRoomTypesDisponibles;;- H
(;;H I
dateBooking;;I T
);;T U
;;;U V
Console<< 
.<< 
	WriteLine<< %
(<<% &
dateBooking<<& 1
.<<1 2
	EntryDate<<2 ;
.<<; <
ToString<<< D
(<<D E
)<<E F
,<<F G
dateBooking<<H S
.<<S T

.<<a b
ToString<<b j
(<<j k
)<<k l
)<<l m
;<<m n
return== 
Ok== 
(== 

roomsTypes== (
)==( )
;==) *
}>> 
else?? 
{@@ 
returnAA 

BadRequestAA %
(AA% &
$strAA& 7
)AA7 8
;AA8 9
}BB 
}DD 
catchEE 
(EE 
	ExceptionEE 
exEE 
)EE  
{FF 
returnGG 

BadRequestGG !
(GG! "
$strGG" Q
)GGQ R
;GGR S
}HH 
}II 	
}xx 
}yy �
vD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\HotelApiControllers\Reserva\ReservaOnlineController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
.1 2
ApiControllers2 @
{ 
[ 
Route 

(
 
$str 
) 
]  
public 

class #
ReservaOnlineController (
:) *

{ 
ObjectCache 
cache 
= 
MemoryCache '
.' (
Default( /
;/ 0
private 
readonly 
IHotelLogica %
hotelLogica& 1
;1 2
private 
readonly 
IEmpleado_Logica )
empleadoLogica* 8
;8 9
private 
readonly 
ISession_Logica (

;6 7
private 
readonly  
ITipoDeCambio_Logica -
tipoCambioLogica. >
;> ?
private 
readonly $
ICentroDeAtencion_Logica 1"
centroDeAtencionLogica2 H
;H I"
UserProfileSessionData 
sessionProfile -
;- .
public #
ReservaOnlineController &
(& '
)' (
{   	
hotelLogica!! 
=!! 
Dependencia!! %
.!!% &
Resolve!!& -
<!!- .
IHotelLogica!!. :
>!!: ;
(!!; <
)!!< =
;!!= >"
centroDeAtencionLogica"" "
=""# $
Dependencia""% 0
.""0 1
Resolve""1 8
<""8 9$
ICentroDeAtencion_Logica""9 Q
>""Q R
(""R S
)""S T
;""T U

=## 
Dependencia## &
.##& '
Resolve##' .
<##. /
ISession_Logica##/ >
>##> ?
(##? @
)##@ A
;##A B
tipoCambioLogica$$ 
=$$ 
Dependencia$$ *
.$$* +
Resolve$$+ 2
<$$2 3 
ITipoDeCambio_Logica$$3 G
>$$G H
($$H I
)$$I J
;$$J K
sessionProfile%% 
=%% 

.%%* + 
GenerarSesionUsuario%%+ ?
(%%? @
)%%@ A
;%%A B
}'' 	
[,, 	
HttpPost,,	 
],, 
public-- 
IHttpActionResult--  
PostBooking--! ,
(--, -
Booking--- 4
booking--5 <
)--< =
{.. 	
if// 
(// 

ModelState// 
.// 
IsValid// "
)//" #
{00 
var11 

tipoCambio11 
=11  
tipoCambioLogica11! 1
.111 2(
ObtenerTipoCambioDolarActual112 N
(11N O
)11O P
;11P Q
var22 
completeSession22 #
=22$ %

.223 4
ResolverSession224 C
(22C D
sessionProfile22D R
,22R S

tipoCambio22T ^
,22^ _
booking22` g
.22g h
IdFilial22h p
)22p q
;22q r
hotelLogica33 
.33 
RegistrarBooking33 ,
(33, -
booking33- 4
,334 5
completeSession336 E
)33E F
;33F G
}44 
else55 
{66 
}77 
Console99 
.99 
	WriteLine99 
(99 
booking99 %
.99% &
DateBooking99& 1
.991 2
	EntryDate992 ;
)99; <
;99< =
return;; 
Ok;; 
(;; 
);; 
;;; 
}<< 	
}\\ 
}]] �O
QD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\BaseController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class 
BaseController 
:  !

Controller" ,
{ 
public 
BaseController 
( 
) 
{ 	
} 	
	protected "
UserProfileSessionData (
ProfileData) 4
(4 5
)5 6
{ 	
var 
profileData 
= 
this "
." #
Session# *
[* +
$str+ 8
]8 9
as: <"
UserProfileSessionData= S
;S T
return 
profileData 
; 
} 	
	protected&& 
List&& 
<&& 
ItemGenericoBase&& '
>&&' (
ObtenerItems&&) 5
(&&5 6
string&&6 <
[&&< =
]&&= >
almacenesString&&? N
)&&N O
{'' 	
return(( 
almacenesString(( "
.((" #
Select((# )
((() *
a((* +
=>((, .
new((/ 2
ItemGenericoBase((3 C
(((C D
)((D E
{((F G
Id((H J
=((K L
int((M P
.((P Q
Parse((Q V
(((V W
a((W X
.((X Y
Split((Y ^
(((^ _
$char((_ b
)((b c
[((c d
$num((d e
]((e f
)((f g
,((g h
Nombre((i o
=((p q
a((r s
.((s t
Split((t y
(((y z
$char((z }
)((} ~
[((~ 
$num	(( �
]
((� �
}
((� �
)
((� �
.
((� �
ToList
((� �
(
((� �
)
((� �
;
((� �
})) 	
public++ $
ParametrosBasicosReporte++ '$
ObtenerParametrosBasicos++( @
(++@ A
)++A B
{,, 	
var-- 
sede-- 
=-- 
ObtenerSede-- "
(--" #
)--# $
;--$ %
DateTime.. 
fechaActual..  
=..! "
DateTimeUtil..# /
.../ 0
FechaActual..0 ;
(..; <
)..< =
;..= >
string// 

logoString// 
=// 
Convert//  '
.//' (
ToBase64String//( 6
(//6 7
sede//7 ;
.//; <
Logo//< @
,//@ A
$num//B C
,//C D
sede//E I
.//I J
Logo//J N
.//N O
Length//O U
)//U V
;//V W
return00 
new00 $
ParametrosBasicosReporte00 /
{11 

NombreSede22 
=22 
new22  
ReportParameter22! 0
(220 1
$str221 =
,22= >
sede22? C
.22C D
Nombre22D J
)22J K
,22K L
LogoSede33 
=33 
new33 
ReportParameter33 .
(33. /
$str33/ 9
,339 :

logoString33; E
)33E F
,33F G
FechaActualSistema44 "
=44# $
new44% (
ReportParameter44) 8
(448 9
$str449 F
,44F G
fechaActual44H S
.44S T
ToString44T \
(44\ ]
)44] ^
)44^ _
,44_ `
Usuario55 
=55 
new55 
ReportParameter55 -
(55- .
$str55. 7
,557 8
ProfileData559 D
(55D E
)55E F
.55F G
Empleado55G O
.55O P
NombresYApellidos55P a
)55a b
}66 
;66
}77 	
public88 4
(EstablecimientoComercialExtendidoConLogo88 7
ObtenerSede888 C
(88C D
)88D E
{99 	
return:: 
(:: 4
(EstablecimientoComercialExtendidoConLogo:: <
)::< =
System::= C
.::C D
Web::D G
.::G H
HttpContext::H S
.::S T
Current::T [
.::[ \
Application::\ g
[::g h
$str::h n
]::n o
;::o p
};; 	
public== 
static== 
string== =
1FormatearParametroDiasAntesDisponiblesParaReporte== N
(==N O
int==O R
	diasAntes==S \
)==\ ]
{>> 	
return?? 
(?? 
$str?? 
+?? 
	diasAntes?? #
.??# $
ToString??$ ,
(??, -
)??- .
+??/ 0
$str??1 4
)??4 5
;??5 6
}@@ 	
publicBB 
staticBB 
voidBB 
ResetParametersBB *
(BB* +
)BB+ ,
{CC 	
}EE 	
publicGG 

TipoCambioGG (
ObtenerTipoCambioDolarActualGG 6
(GG6 7
)GG7 8
{HH 	

TipoCambioII 

tipoCambioII !
=II" #
newII$ '

TipoCambioII( 2
(II2 3
)II3 4
;II4 5
tryJJ 
{KK 
stringLL 
urlLL 
=LL 
AplicacionSettingsLL /
.LL/ 0
DefaultLL0 7
.LL7 8$
UrlApiConsultaTipoCambioLL8 P
+LLQ R
AplicacionSettingsLLS e
.LLe f
DefaultLLf m
.LLm n'
IdMonedaDolarApiTipoCambio	LLn �
;
LL� �
varMM 
tMM 
=MM 
TaskMM 
.MM 
RunMM  
(MM  !
(MM! "
)MM" #
=>MM$ &
GetURIMM' -
(MM- .
newMM. 1
UriMM2 5
(MM5 6
urlMM6 9
)MM9 :
)MM: ;
)MM; <
;MM< =
tNN 
.NN 
WaitNN 
(NN 
)NN 
;NN 
stringOO 
resultOO 
=OO 
tOO  !
.OO! "
ResultOO" (
;OO( )

tipoCambioPP 
=PP 

NewtonsoftPP '
.PP' (
JsonPP( ,
.PP, -
JsonConvertPP- 8
.PP8 9
DeserializeObjectPP9 J
<PPJ K

TipoCambioPPK U
>PPU V
(PPV W
resultPPW ]
)PP] ^
;PP^ _

tipoCambioQQ 
.QQ 
IdMonedaQQ #
=QQ$ %
MaestroSettingsQQ& 5
.QQ5 6
DefaultQQ6 =
.QQ= >)
IdDetalleMaestroMonedaDolaresQQ> [
;QQ[ \
ifRR 
(RR 

tipoCambioRR 
.RR 
EstadoRR %
==RR& (
falseRR) .
)RR. /
{SS 

tipoCambioTT 
=TT  
newTT! $

TipoCambioTT% /
(TT/ 0
)TT0 1
;TT1 2
}UU 
}VV 
catchWW 
(WW 
	ExceptionWW 
)WW 
{XX 

tipoCambioYY 
=YY 
newYY  

TipoCambioYY! +
(YY+ ,
)YY, -
;YY- .
}ZZ 
return[[ 

tipoCambio[[ 
;[[ 
}\\ 	
	protected^^ 
static^^ 
async^^ 
Task^^ #
<^^# $
string^^$ *
>^^* +
GetURI^^, 2
(^^2 3
Uri^^3 6
u^^7 8
)^^8 9
{__ 	
var`` 
response`` 
=`` 
string`` !
.``! "
Empty``" '
;``' (
usingaa 
(aa 
varaa 
clientaa 
=aa 
newaa  #

HttpClientaa$ .
(aa. /
)aa/ 0
)aa0 1
{bb 
HttpResponseMessagecc #
resultcc$ *
=cc+ ,
awaitcc- 2
clientcc3 9
.cc9 :
GetAsynccc: B
(ccB C
uccC D
)ccD E
;ccE F
ifdd 
(dd 
resultdd 
.dd 
IsSuccessStatusCodedd .
)dd. /
{ee 
responseff 
=ff 
awaitff $
resultff% +
.ff+ ,
Contentff, 3
.ff3 4
ReadAsStringAsyncff4 E
(ffE F
)ffF G
;ffG H
}gg 
}hh 
returnii 
responseii 
;ii 
}jj 	
publicll 
staticll 
stringll 
	Encriptarll &
(ll& '
stringll' -
_cadenaAencriptarll. ?
)ll? @
{mm 	
stringnn 
resultnn 
=nn 
stringnn "
.nn" #
Emptynn# (
;nn( )
byteoo 
[oo 
]oo 
encrytedoo 
=oo 
Systemoo $
.oo$ %
Textoo% )
.oo) *
Encodingoo* 2
.oo2 3
ASCIIoo3 8
.oo8 9
GetBytesoo9 A
(ooA B
_cadenaAencriptarooB S
)ooS T
;ooT U
resultpp 
=pp 
Convertpp 
.pp 
ToBase64Stringpp +
(pp+ ,
encrytedpp, 4
)pp4 5
;pp5 6
returnqq 
resultqq 
;qq 
}rr 	
publictt 
statictt 
stringtt 
DesEncriptartt )
(tt) *
stringtt* 0 
_cadenaAdesencriptartt1 E
)ttE F
{uu 	
stringvv 
resultvv 
=vv 
stringvv "
.vv" #
Emptyvv# (
;vv( )
byteww 
[ww 
]ww 
decrytedww 
=ww 
Convertww %
.ww% &
FromBase64Stringww& 6
(ww6 7 
_cadenaAdesencriptarww7 K
)wwK L
;wwL M
resultxx 
=xx 
Systemxx 
.xx 
Textxx  
.xx  !
Encodingxx! )
.xx) *
ASCIIxx* /
.xx/ 0
	GetStringxx0 9
(xx9 :
decrytedxx: B
)xxB C
;xxC D
returnyy 
resultyy 
;yy 
}zz 	
}{{ 
}|| ��
]D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\CentroDeAtencionController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class &
CentroDeAtencionController +
:, -
BaseController. <
{ 
private 
readonly 
IConceptoLogica (
conceptoLogica) 7
;7 8
	protected   
readonly   
IMaestroLogica   )

;  7 8
	protected!! 
readonly!! 
IActorNegocioLogica!! .
actorNegocioLogica!!/ A
;!!A B
	protected"" 
readonly"" 
IOperacionLogica"" +
_operacionLogica"", <
;""< =
	protected## 
readonly## 
ISession_Logica## *

;##8 9
	protected$$ 
readonly$$ 
ISede_Logica$$ '
_sedeLogica$$( 3
;$$3 4
	protected%% 
readonly%% 
ISucursal_Logica%% +
_sucursalLogica%%, ;
;%%; <
	protected&& 
readonly&& (
IEstablecimiento_Repositorio&& 7!
_establecimientoDatos&&8 M
;&&M N
	protected'' 
readonly'' $
ICentroDeAtencion_Logica'' 3!
_centroAtencionLogica''4 I
;''I J
	protected(( 
readonly(( #
IEstablecimiento_Logica(( 2"
_establecimientoLogica((3 I
;((I J
	protected)) 
readonly)) $
IInventarioActual_Logica)) 3#
_inventarioActualLogica))4 K
;))K L
	protected** 
readonly**  
ITipoDeCambio_Logica** /
_tipoDeCambioLogica**0 C
;**C D
public33 &
CentroDeAtencionController33 )
(33) *
)33* +
{44 	
actorNegocioLogica55 
=55  
Dependencia55! ,
.55, -
Resolve55- 4
<554 5
IActorNegocioLogica555 H
>55H I
(55I J
)55J K
;55K L
conceptoLogica77 
=77 
Dependencia77 (
.77( )
Resolve77) 0
<770 1
IConceptoLogica771 @
>77@ A
(77A B
)77B C
;77C D

=88 
Dependencia88 '
.88' (
Resolve88( /
<88/ 0
IMaestroLogica880 >
>88> ?
(88? @
)88@ A
;88A B
_operacionLogica99 
=99 
Dependencia99 *
.99* +
Resolve99+ 2
<992 3
IOperacionLogica993 C
>99C D
(99D E
)99E F
;99F G

=:: 
Dependencia:: '
.::' (
Resolve::( /
<::/ 0
ISession_Logica::0 ?
>::? @
(::@ A
)::A B
;::B C
_sedeLogica;; 
=;; 
Dependencia;; $
.;;$ %
Resolve;;% ,
<;;, -
ISede_Logica;;- 9
>;;9 :
(;;: ;
);;; <
;;;< =!
_establecimientoDatos<< !
=<<" #
Dependencia<<$ /
.<</ 0
Resolve<<0 7
<<<7 8(
IEstablecimiento_Repositorio<<8 T
><<T U
(<<U V
)<<V W
;<<W X
_sucursalLogica>> 
=>> 
Dependencia>> )
.>>) *
Resolve>>* 1
<>>1 2
ISucursal_Logica>>2 B
>>>B C
(>>C D
)>>D E
;>>E F!
_centroAtencionLogica?? !
=??" #
Dependencia??$ /
.??/ 0
Resolve??0 7
<??7 8$
ICentroDeAtencion_Logica??8 P
>??P Q
(??Q R
)??R S
;??S T"
_establecimientoLogica@@ "
=@@# $
Dependencia@@% 0
.@@0 1
Resolve@@1 8
<@@8 9#
IEstablecimiento_Logica@@9 P
>@@P Q
(@@Q R
)@@R S
;@@S T#
_inventarioActualLogicaBB #
=BB$ %
DependenciaBB& 1
.BB1 2
ResolveBB2 9
<BB9 :$
IInventarioActual_LogicaBB: R
>BBR S
(BBS T
)BBT U
;BBU V
_tipoDeCambioLogicaCC 
=CC  !
DependenciaCC" -
.CC- .
ResolveCC. 5
<CC5 6 
ITipoDeCambio_LogicaCC6 J
>CCJ K
(CCK L
)CCL M
;CCM N
}GG 	
publicLL 
ActionResultLL '
SeleccionarCentroDeAtencionLL 7
(LL7 8
)LL8 9
{MM 	"
UserProfileSessionDataNN "
profileNN# *
=NN+ ,

.NN: ; 
GenerarSesionUsuarioNN; O
(NNO P
UserNNP T
.NNT U
IdentityNNU ]
.NN] ^
	GetUserIdNN^ g
(NNg h
)NNh i
,NNi j
UserNNk o
.NNo p
IdentityNNp x
.NNx y
GetUserName	NNy �
(
NN� �
)
NN� �
)
NN� �
;
NN� �
thisOO 
.OO 
SessionOO 
[OO 
$strOO &
]OO& '
=OO( )
profileOO* 1
;OO1 2
returnPP 
ViewPP 
(PP 
profilePP 
)PP  
;PP  !
}QQ 	
[TT 	
HttpPostTT	 
]TT 
publicUU 
ActionResultUU '
SeleccionarCentroDeAtencionUU 7
(UU7 8"
UserProfileSessionDataUU8 N
profileUUO V
)UUV W
{VV 	
varWW 
profileDataWW 
=WW (
EstablecerDatosSesionUsuarioWW :
(WW: ;
profileWW; B
)WWB C
;WWC D
ifYY 
(YY 
profileDataYY 
.YY 
MensajeErrorYY '
==YY( *
$strYY+ -
)YY- .
{ZZ 
this[[ 
.[[ 
Session[[ 
[[[ 
$str[[ -
][[- .
=[[/ 0
$num[[1 3
;[[3 4
this\\ 
.\\ 
Session\\ 
[\\ 
$str\\ -
]\\- .
=\\/ 0
$num\\1 3
;\\3 4
Configuraciones^^ 
.^^  
Reset^^  %
(^^% &
)^^& '
;^^' (!
ConfiguracionesLogica__ %
.__% &
Reset__& +
(__+ ,
)__, -
;__- .
Systemaa 
.aa 
Webaa 
.aa 
HttpContextaa &
.aa& '
Currentaa' .
.aa. /
Applicationaa/ :
.aa: ;
Lockaa; ?
(aa? @
)aa@ A
;aaA B
Systembb 
.bb 
Webbb 
.bb 
HttpContextbb &
.bb& '
Currentbb' .
.bb. /
Applicationbb/ :
[bb: ;
$strbb; A
]bbA B
=bbC D
profileDatabbE P
.bbP Q
SedebbQ U
;bbU V
Systemcc 
.cc 
Webcc 
.cc 
HttpContextcc &
.cc& '
Currentcc' .
.cc. /
Applicationcc/ :
.cc: ;
UnLockcc; A
(ccA B
)ccB C
;ccC D
returnee 
RedirectToLocalee &
(ee& '
)ee' (
;ee( )
}ff 
elsegg 
{hh 
returnii 
Viewii 
(ii 
profileDataii '
)ii' (
;ii( )
}jj 
}kk 	
privateoo 
ActionResultoo 
RedirectToLocaloo ,
(oo, -
)oo- .
{pp 	
returnqq 
RedirectToActionqq #
(qq# $
$strqq$ 1
,qq1 2
$strqq3 ?
)qq? @
;qq@ A
}rr 	
publictt 

JsonResulttt )
ObtenerRolesCentrosDeAtenciontt 7
(tt7 8
)tt8 9
{uu 	
tryvv 
{ww 
varxx 

resultadosxx 
=xx  !
_centroAtencionLogicaxx! 6
.xx6 7*
ObtenerRolesDeCentroDeAtencionxx7 U
(xxU V
)xxV W
;xxW X
Listyy 
<yy "
ComboGenericoViewModelyy +
>yy+ ,
rolesyy- 2
=yy3 4
newyy5 8
Listyy9 =
<yy= >"
ComboGenericoViewModelyy> T
>yyT U
(yyU V
)yyV W
;yyW X
roleszz 
=zz "
ComboGenericoViewModelzz .
.zz. /
Convertzz/ 6
(zz6 7

resultadoszz7 A
)zzA B
;zzB C
return{{ 
Json{{ 
({{ 
roles{{ !
){{! "
;{{" #
}|| 
catch}} 
(}} 
	Exception}} 
e}} 
)}} 
{~~ 
return 
Json 
( 
Util  
.  !
	ErrorJson! *
(* +
e+ ,
), -
)- .
;. /
}
�� 
}
�� 	
public
�� $
UserProfileSessionData
�� %*
EstablecerDatosSesionUsuario
��& B
(
��B C$
UserProfileSessionData
��C Y
profile
��Z a
)
��a b
{
�� 	
var
�� 
profileData
�� 
=
�� 
this
�� "
.
��" #
ProfileData
��# .
(
��. /
)
��/ 0
;
��0 1
profileData
�� 
.
�� 
MensajeError
�� $
=
��% &
$str
��' )
;
��) *
try
�� 
{
�� 
if
�� 
(
�� 

ModelState
�� 
.
�� 
IsValid
�� &
)
��& '
{
�� 
var
�� 
sede
�� 
=
�� 
_sedeLogica
�� *
.
��* + 
ObtenerSedeConLogo
��+ =
(
��= >
)
��> ?
;
��? @
profileData
�� 
.
��  
Sede
��  $
=
��% &
sede
��' +
;
��+ ,
profileData
�� 
.
��  

NombreSede
��  *
=
��+ ,
sede
��- 1
.
��1 2
Nombre
��2 8
;
��8 9
var
�� 
clientePorDefecto
�� )
=
��* + 
actorNegocioLogica
��, >
.
��> ?$
ObtenerClienteGenerico
��? U
(
��U V
)
��V W
;
��W X
profileData
�� 
.
��  
ClientePorDefecto
��  1
=
��2 3
clientePorDefecto
��4 E
;
��E F
if
�� 
(
�� 
profileData
�� #
.
��# $*
CentrosDeAtencionProgramados
��$ @
.
��@ A
Count
��A F
>
��G H
$num
��I J
)
��J K
{
�� 
profileData
�� #
.
��# $*
CentroDeAtencionSeleccionado
��$ @
=
��A B
profileData
��C N
.
��N O*
CentrosDeAtencionProgramados
��O k
.
��k l
SingleOrDefault
��l {
(
��{ |
cap
��| 
=>��� �
cap��� �
.��� �
Id��� �
==��� �
profile��� �
.��� �.
IdCentroDeAtencionInicioSesion��� �
)��� �
;��� �
}
�� 
if
�� 
(
�� 
profileData
�� #
.
��# $*
CentroDeAtencionSeleccionado
��$ @
!=
��A C
null
��D H
)
��H I
{
�� 
profileData
�� #
.
��# $2
$EstablecimientoComercialSeleccionado
��$ H
=
��I J
profileData
��K V
.
��V W*
CentroDeAtencionSeleccionado
��W s
.
��s t'
EstablecimientoComercial��t �
.��� �
Id��� �
==��� �
sede��� �
.��� �
Id��� �
?��� �
sede��� �
:��� �%
_establecimientoDatos��� �
.��� �?
/ObtenerEstablecimientoComercialExtendidoConLogo��� �
(��� �
profileData��� �
.��� �,
CentroDeAtencionSeleccionado��� �
.��� �(
EstablecimientoComercial��� �
.��� �
Id��� �
)��� �
;��� �
profileData
�� #
.
��# $0
"IdCentroAtencionQueTieneLosPrecios
��$ F
=
��G H#
_centroAtencionLogica
��I ^
.
��^ _>
/ObtenerIdCentroDeAtencionParaObtencionDePrecios��_ �
(��� �
profileData��� �
.��� �,
CentroDeAtencionSeleccionado��� �
,��� �
profileData��� �
.��� �4
$EstablecimientoComercialSeleccionado��� �
)��� �
;��� �
profileData
�� #
.
��# $6
(IdCentroAtencionQueTieneElStockIntegrada
��$ L
=
��M N#
_centroAtencionLogica
��O d
.
��d e<
-ObtenerIdCentroDeAtencionParaObtencionDeStock��e �
(��� �!
ModoOperacionEnum��� �
.��� �
PorMostrador��� �
,��� �
profileData��� �
.��� �,
CentroDeAtencionSeleccionado��� �
,��� �
profileData��� �
.��� �4
$EstablecimientoComercialSeleccionado��� �
)��� �
;��� �
profileData
�� #
.
��# $4
&CentroAtencionQueTieneElStockIntegrada
��$ J
=
��K L#
_centroAtencionLogica
��M b
.
��b c&
ObtenerCentroDeAtencion_
��c {
(
��{ |
profileData��| �
.��� �8
(IdCentroAtencionQueTieneElStockIntegrada��� �
)��� �
;��� �
profileData
�� #
.
��# $5
'IdCentroAtencionQueTieneElStockDosPasos
��$ K
=
��L M#
_centroAtencionLogica
��N c
.
��c d<
-ObtenerIdCentroDeAtencionParaObtencionDeStock��d �
(��� �!
ModoOperacionEnum��� �
.��� �&
PorMostradorEnDosPasos��� �
,��� �
profileData��� �
.��� �,
CentroDeAtencionSeleccionado��� �
,��� �
profileData��� �
.��� �4
$EstablecimientoComercialSeleccionado��� �
)��� �
;��� �
profileData
�� #
.
��# $8
*IdCentroAtencionQueTieneElStockCorporativa
��$ N
=
��O P#
_centroAtencionLogica
��Q f
.
��f g<
-ObtenerIdCentroDeAtencionParaObtencionDeStock��g �
(��� �!
ModoOperacionEnum��� �
.��� �
Corporativa��� �
,��� �
profileData��� �
.��� �,
CentroDeAtencionSeleccionado��� �
,��� �
profileData��� �
.��� �4
$EstablecimientoComercialSeleccionado��� �
)��� �
;��� �
}
�� 
profileData
�� 
.
��  $
CostoUnitarioDelIcbper
��  6
=
��7 8
conceptoLogica
��9 G
.
��G H3
%ObtenerCostoUnitarioDelIcbperALaFecha
��H m
(
��m n
)
��n o
;
��o p
profileData
�� 
.
��  
TipoDeCambio
��  ,
=
��- .!
_tipoDeCambioLogica
��/ B
.
��B C*
ObtenerTipoCambioDolarActual
��C _
(
��_ `
)
��` a
;
��a b
profileData
�� 
.
��  ,
SetIdAlmacenIdInventarioFisico
��  >
(
��> ?%
_inventarioActualLogica
��? V
.
��V W2
$ObtenerIdsAlmacenIdsInventarioActual
��W {
(
��{ |
)
��| }
)
��} ~
;
��~ 
profileData
�� 
.
��   
MaestrosFrecuentes
��  2
=
��3 4
new
��5 8

��9 F
{
�� 
Moneda
�� 
=
��  
ItemGenerico
��! -
.
��- .
Convert
��. 5
(
��5 6

��6 C
.
��C D%
ObtenerMonedaPorDefecto
��D [
(
��[ \
)
��\ ]
)
��] ^
}
�� 
;
�� 
profileData
�� 
.
��  (
OperacionSesionContenedora
��  :
=
��; <
_operacionLogica
��= M
.
��M N/
!ObtenerOperacionSesionContenedora
��N o
(
��o p
profileData
��p {
.
��{ |-
IdCentroDeAtencionSeleccionado��| �
)��� �
;��� �
var
�� 

stringJson
�� "
=
��# $
System
��% +
.
��+ ,
IO
��, .
.
��. /
File
��/ 3
.
��3 4
ReadAllText
��4 ?
(
��? @
Path
��@ D
.
��D E
Combine
��E L
(
��L M
System
��M S
.
��S T
	AppDomain
��T ]
.
��] ^

��^ k
.
��k l

��l y
,
��y z
$str��{ �
)��� �
)��� �
;��� �
profileData
�� 
.
��  !
MensajesTransaccion
��  3
=
��4 5
JsonConvert
��6 A
.
��A B
DeserializeObject
��B S
<
��S T!
MensajesTransaccion
��T g
>
��g h
(
��h i

stringJson
��i s
)
��s t
;
��t u
return
�� 
profileData
�� &
;
��& '
}
�� 
else
�� 
{
�� 

ModelState
�� 
.
�� 

�� ,
(
��, -
$str
��- /
,
��/ 0
$str
��1 p
)
��p q
;
��q r
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
profileData
�� 
.
�� 
MensajeError
�� (
=
��) *
$str
��+ K
+
��L M
e
��N O
.
��O P
Message
��P W
+
��X Y
e
��Z [
.
��[ \

StackTrace
��\ f
;
��f g
}
�� 
return
�� 
profileData
�� 
;
�� 
}
�� 	
public
�� 
ActionResult
�� 
Sede
��  
(
��  !
)
��! "
{
�� 	
ViewBag
�� 
.
�� F
8permitirRegistroCodigoDigemidEnEstableciemientoComercial
�� L
=
��M N

��O \
.
��\ ]
Default
��] d
.
��d eG
8PermitirRegistroCodigoDigemidEnEstableciemientoComercial��e �
;��� �
ViewBag
�� 
.
�� 
idRolAlmacen
��  
=
��! "

��# 0
.
��0 1
Default
��1 8
.
��8 9
IdRolAlmacen
��9 E
;
��E F
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 
ActionResult
�� 
Sucursal
�� $
(
��$ %
)
��% &
{
�� 	
ViewBag
�� 
.
�� F
8permitirRegistroCodigoDigemidEnEstableciemientoComercial
�� L
=
��M N

��O \
.
��\ ]
Default
��] d
.
��d eG
8PermitirRegistroCodigoDigemidEnEstableciemientoComercial��e �
;��� �
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 
ActionResult
�� 
	Sucursal_
�� %
(
��% &
int
��& )

idSucursal
��* 4
)
��4 5
{
�� 	&
EstablecimientoComercial
�� $
sucursal
��% -
=
��. /
_sucursalLogica
��0 ?
.
��? @0
"ObtenerSucursalComoEstablecimiento
��@ b
(
��b c

idSucursal
��c m
)
��m n
;
��n o
ViewBag
�� 
.
�� 

IdSucursal
�� 
=
��  
sucursal
��! )
.
��) *
Id
��* ,
;
��, -
ViewBag
�� 
.
�� 
nombre
�� 
=
�� 
sucursal
�� %
.
��% &
Nombre
��& ,
;
��, -
ViewBag
�� 
.
�� 
idRolAlmacen
��  
=
��! "

��# 0
.
��0 1
Default
��1 8
.
��8 9
IdRolAlmacen
��9 E
;
��E F
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 

JsonResult
�� 
	CrearSede
�� #
(
��# $#
RegistroSedeViewModel
��$ 9
sede
��: >
)
��> ?
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
;
��) *
if
�� 
(
�� 
sede
�� 
.
�� 
Id
�� 
>
�� 
$num
�� 
)
��  
{
�� 
	resultado
�� 
=
�� 
_sedeLogica
��  +
.
��+ ,
ActualizarSede
��, :
(
��: ;
sede
��; ?
.
��? @
IdActor
��@ G
,
��G H
sede
��I M
.
��M N
Id
��N P
,
��P Q
sede
��R V
.
��V W&
NumeroDocumentoIdentidad
��W o
,
��o p
sede
��q u
.
��u v$
CodigoEstablecimiento��v �
,��� �
sede��� �
.��� �,
CodigoEstablecimientoDigemid��� �
,��� �
sede��� �
.��� �'
InformacionPublicitaria��� �
,��� �
sede��� �
.��� �
TipoPersona��� �
.��� �
Id��� �
,��� �
sede��� �
.��� �

ClaseActor��� �
.��� �
Id��� �
,��� �
sede��� �
.��� �
RazonSocial��� �
,��� �
sede��� �
.��� �
NombreComercial��� �
,��� �
sede��� �
.��� �

,��� �
sede��� �
.��� �
Telefono��� �
,��� �
sede��� �
.��� �
Correo��� �
,��� �'
CrearYResolverDireccion��� �
(��� �
sede��� �
.��� �
	Direccion��� �
,��� �
sede��� �
.��� �
IdActor��� �
)��� �
,��� �
sede��� �
.��� �
Foto��� �
.��� �
HayFoto��� �
?��� �
Convert��� �
.��� � 
FromBase64String��� �
(��� �
sede��� �
.��� �
Foto��� �
.��� �
Foto��� �
)��� �
:��� �
null��� �
)��� �
;��� �
}
�� 
else
�� 
{
�� 
if
�� 
(
�� 
sede
�� 
.
�� 
IdActor
�� $
>
��% &
$num
��' (
)
��( )
{
�� 
	resultado
�� !
=
��" #
new
��$ '
OperationResult
��( 7
(
��7 8
)
��8 9
;
��9 :
}
�� 
else
�� 
{
�� 
	resultado
�� !
=
��" #
_sedeLogica
��$ /
.
��/ 0
	CrearSede
��0 9
(
��9 :
sede
��: >
.
��> ?&
NumeroDocumentoIdentidad
��? W
,
��W X
sede
��Y ]
.
��] ^#
CodigoEstablecimiento
��^ s
,
��s t
sede
��u y
.
��y z+
CodigoEstablecimientoDigemid��z �
,��� �
sede��� �
.��� �'
InformacionPublicitaria��� �
,��� �
sede��� �
.��� �
TipoPersona��� �
.��� �
Id��� �
,��� �
sede��� �
.��� �

ClaseActor��� �
.��� �
Id��� �
,��� �
sede��� �
.��� �
RazonSocial��� �
,��� �
sede��� �
.��� �
NombreComercial��� �
,��� �
sede��� �
.��� �

,��� �
sede��� �
.��� �
Telefono��� �
,��� �
sede��� �
.��� �
Correo��� �
,��� �
CrearDireccion��� �
(��� �
sede��� �
.��� �
	Direccion��� �
)��� �
,��� �
sede��� �
.��� �
Foto��� �
.��� �
HayFoto��� �
?��� �
Convert��� �
.��� � 
FromBase64String��� �
(��� �
sede��� �
.��� �
Foto��� �
.��� �
Foto��� �
)��� �
:��� �
null��� �
)��� �
;��� �
}
�� 
}
�� 
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str
��; R
)
��R S
;
��S T
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
	resultado
��6 ?
.
��? @
code_result
��@ K
,
��K L
	resultado
��M V
.
��V W
data
��W [
,
��[ \ 
result_description
��] o
=
��p q
	resultado
��r {
.
��{ |
title��| �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M p
,
��p q
e
��r s
)
��s t
)
��t u
,
��u v
HttpStatusCode��w �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� "
ObtenerSedePrincipal
�� .
(
��. /
)
��/ 0
{
�� 	
try
�� 
{
�� 
(EstablecimientoComercialExtendidoConLogo
�� 8
sede
��9 =
=
��> ?
_sedeLogica
��@ K
.
��K L 
ObtenerSedeConLogo
��L ^
(
��^ _
)
��_ `
;
��` a#
RegistroSedeViewModel
�� %

��& 3
=
��4 5
new
��6 9#
RegistroSedeViewModel
��: O
(
��O P
sede
��P T
)
��T U
;
��U V
return
�� 
Json
�� 
(
�� 

�� )
)
��) *
;
��* +
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 0
"ObtenerEstablecimientosComerciales
�� <
(
��< =
)
��= >
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� /
!EstablecimientoComercialExtendido
�� 6
>
��6 7)
establecimientosComerciales
��8 S
=
��T U$
_establecimientoLogica
��V l
.
��l mC
4ObtenerEstablecimientosComercialesExtendidosVigentes��m �
(��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,/
!establecimientoComercialViewModel
��- N
=
��O P$
ComboGenericoViewModel
��Q g
.
��g h
Convert
��h o
(
��o p*
establecimientosComerciales��p �
)��� �
;��� �
return
�� 
Json
�� 
(
�� /
!establecimientoComercialViewModel
�� =
)
��= >
;
��> ?
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 1
#ObtenerCentrosDeAtencionParaPrecios
�� =
(
��= >
)
��> ?
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
CentroDeAtencion
�� %
>
��% &
centrosDeAtencion
��' 8
=
��9 :#
_centroAtencionLogica
��; P
.
��P Q.
 ObtenerCentrosDeAtencionVigentes
��Q q
(
��q r
)
��r s
;
��s t
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,$
centrosDeAtencionCombo
��- C
=
��D E
new
��F I
List
��J N
<
��N O$
ComboGenericoViewModel
��O e
>
��e f
(
��f g
)
��g h
;
��h i
foreach
�� 
(
�� 
var
�� 
centroDeAtencion
�� -
in
��. 0
centrosDeAtencion
��1 B
)
��B C
{
�� $
centrosDeAtencionCombo
�� *
.
��* +
Add
��+ .
(
��. /
new
��/ 2$
ComboGenericoViewModel
��3 I
(
��I J
centroDeAtencion
��J Z
.
��Z [
Id
��[ ]
,
��] ^
centroDeAtencion
��_ o
.
��o p'
EstablecimientoComercial��p �
.��� �

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
�� 
return
�� 
Json
�� 
(
�� $
centrosDeAtencionCombo
�� 2
)
��2 3
;
��3 4
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 
	Direccion
�� %
CrearYResolverDireccion
�� 0
(
��0 1 
DireccionViewModel
��1 C 
direccionViewModel
��D V
,
��V W
int
��X [
idActor
��\ c
)
��c d
{
�� 	
	Direccion
�� 
	direccion
�� 
=
��  !
new
��" %
	Direccion
��& /
(
��/ 0
)
��0 1
;
��1 2
if
�� 
(
��  
direccionViewModel
�� "
!=
��# %
null
��& *
)
��* +
{
�� 
if
�� 
(
��  
direccionViewModel
�� &
.
��& '
Id
��' )
>
��* +
$num
��, -
)
��- .
{
�� 
	direccion
�� 
=
�� 
new
��  #
	Direccion
��$ -
(
��- . 
direccionViewModel
��. @
.
��@ A
Id
��A C
,
��C D
idActor
��E L
,
��L M 
direccionViewModel
��N `
.
��` a
Tipo
��a e
.
��e f
Id
��f h
,
��h i 
direccionViewModel
��j |
.
��| }
Nacion��} �
.��� �
Id��� �
,��� �"
direccionViewModel��� �
.��� �
Ubigeo��� �
.��� �
Id��� �
,��� �"
direccionViewModel��� �
.��� �
Detalle��� �
,��� �
null
�� 
,
�� 
null
�� "
,
��" # 
direccionViewModel
��$ 6
.
��6 7
EsPrincipal
��7 B
,
��B C 
direccionViewModel
��D V
.
��V W
	EsVigente
��W `
)
��` a
;
��a b
}
�� 
else
�� 
{
�� 
	direccion
�� 
=
�� 
new
��  #
	Direccion
��$ -
(
��- .
idActor
��. 5
,
��5 6
MaestroSettings
��7 F
.
��F G
Default
��G N
.
��N O:
,IdDetalleMaestroTipoDireccionDomicilioFiscal
��O {
,
��{ |
MaestroSettings��} �
.��� �
Default��� �
.��� �*
IdDetalleMaestroNacionPeru��� �
,��� �"
direccionViewModel��� �
.��� �
Ubigeo��� �
.��� �
Id��� �
,��� � 
direccionViewModel
�� *
.
��* +
Detalle
��+ 2
,
��2 3
null
��4 8
,
��8 9
null
��: >
,
��> ? 
direccionViewModel
��@ R
.
��R S
EsPrincipal
��S ^
,
��^ _ 
direccionViewModel
��` r
.
��r s
	EsVigente
��s |
)
��| }
;
��} ~
}
�� 
}
�� 
return
�� 
	direccion
�� 
;
�� 
}
�� 	
public
�� 
	Direccion
�� 
CrearDireccion
�� '
(
��' ( 
DireccionViewModel
��( :

��; H
)
��H I
{
�� 	
	Direccion
�� 
	direccion
�� 
=
��  !
new
��" %
	Direccion
��& /
(
��/ 0
)
��0 1
;
��1 2
if
�� 
(
�� 

�� 
!=
��  
null
��! %
)
��% &
{
�� 
	direccion
�� 
=
�� 
new
�� 
	Direccion
��  )
(
��) *
MaestroSettings
��* 9
.
��9 :
Default
��: A
.
��A B:
,IdDetalleMaestroTipoDireccionDomicilioFiscal
��B n
,
��n o
MaestroSettings
��p 
.�� �
Default��� �
.��� �*
IdDetalleMaestroNacionPeru��� �
,��� �

.��� �
Ubigeo��� �
.��� �
Id��� �
,��� �

.��� �
Detalle��� �
,��� �
null
�� 
,
�� 
null
�� 
,
�� 

��  -
.
��- .
EsPrincipal
��. 9
,
��9 :

��; H
.
��H I
	EsVigente
��I R
)
��R S
;
��S T
}
�� 
return
�� 
	direccion
�� 
;
�� 
}
�� 	
public
�� 

JsonResult
�� 

�� '
(
��' ('
RegistroSucursalViewModel
��( A
sucursal
��B J
)
��J K
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
if
�� 
(
�� 
sucursal
�� 
.
�� 
Id
�� 
>
��  !
$num
��" #
)
��# $
{
�� 
	resultado
�� 
=
�� 
_sucursalLogica
��  /
.
��/ 0 
ActualizarSucursal
��0 B
(
��B C
sucursal
��C K
.
��K L
IdActor
��L S
,
��S T
sucursal
��U ]
.
��] ^
Id
��^ `
,
��` a
sucursal
��b j
.
��j k$
CodigoEstablecimiento��k �
,��� �
sucursal��� �
.��� �,
CodigoEstablecimientoDigemid��� �
,��� �
sucursal��� �
.��� �'
InformacionPublicitaria��� �
,��� �
sucursal��� �
.��� �
Nombre��� �
,��� �
sucursal��� �
.��� �

,��� �
sucursal��� �
.��� �
Telefono��� �
,��� �
sucursal��� �
.��� �
Correo��� �
,��� �'
CrearYResolverDireccion��� �
(��� �
sucursal��� �
.��� �
	Direccion��� �
,��� �
sucursal��� �
.��� �
IdActor��� �
)��� �
,��� �
sucursal��� �
.��� �
Foto��� �
.��� �
HayFoto��� �
?��� �
Convert��� �
.��� � 
FromBase64String��� �
(��� �
sucursal��� �
.��� �
Foto��� �
.��� �
Foto��� �
)��� �
:��� �
null��� �
)��� �
;��� �
}
�� 
else
�� 
{
�� 
if
�� 
(
�� 
sucursal
��  
.
��  !
IdActor
��! (
>
��) *
$num
��+ ,
)
��, -
{
�� 
	resultado
�� !
=
��" #
new
��$ '
OperationResult
��( 7
(
��7 8
)
��8 9
;
��9 :
}
�� 
else
�� 
{
�� 
	resultado
�� !
=
��" #
_sucursalLogica
��$ 3
.
��3 4

��4 A
(
��A B
sucursal
��B J
.
��J K#
CodigoEstablecimiento
��K `
,
��` a
sucursal
��b j
.
��j k+
CodigoEstablecimientoDigemid��k �
,��� �
sucursal��� �
.��� �'
InformacionPublicitaria��� �
,��� �
sucursal��� �
.��� �
Nombre��� �
,��� �
sucursal��� �
.��� �

,��� �
sucursal��� �
.��� �
Telefono��� �
,��� �
sucursal��� �
.��� �
Correo��� �
,��� �
CrearDireccion��� �
(��� �
sucursal��� �
.��� �
	Direccion��� �
)��� �
,��� �
sucursal��� �
.��� �
Foto��� �
.��� �
HayFoto��� �
?��� �
Convert��� �
.��� � 
FromBase64String��� �
(��� �
sucursal��� �
.��� �
Foto��� �
.��� �
Foto��� �
)��� �
:��� �
null��� �
)��� �
;��� �
}
�� 
}
�� 
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str
��; Y
)
��Y Z
;
��Z [
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
code_result
��6 A
=
��B C
	resultado
��D M
.
��M N
code_result
��N Y
,
��Y Z
data
��[ _
=
��` a
	resultado
��b k
.
��k l
data
��l p
,
��p q!
result_description��r �
=��� �
	resultado��� �
.��� �
title��� �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M t
,
��t u
e
��v w
)
��w x
)
��x y
,
��y z
HttpStatusCode��{ �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� &
ObtenerSucursalesBandeja
�� 2
(
��2 3
)
��3 4
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
Sucursal
�� 
>
�� 

sucursales
�� )
=
��* +
_sucursalLogica
��, ;
.
��; <'
ObtenerSucursalesVigentes
��< U
(
��U V
)
��V W
;
��W X
List
�� 
<
�� &
BandejaSucursalViewModel
�� -
>
��- .!
sucursalesViewModel
��/ B
=
��C D&
BandejaSucursalViewModel
��E ]
.
��] ^
Convert
��^ e
(
��e f

sucursales
��f p
)
��p q
;
��q r
return
�� 
Json
�� 
(
�� !
sucursalesViewModel
�� /
)
��/ 0
;
��0 1
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
�� 
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
ObtenerSucursales
�� +
(
��+ ,
)
��, -
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
Sucursal
�� 
>
�� 

sucursales
�� )
=
��* +
_sucursalLogica
��, ;
.
��; <'
ObtenerSucursalesVigentes
��< U
(
��U V
)
��V W
;
��W X
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,!
sucursalesViewModel
��- @
=
��A B$
ComboGenericoViewModel
��C Y
.
��Y Z
Convert
��Z a
(
��a b

sucursales
��b l
)
��l m
;
��m n
return
�� 
Json
�� 
(
�� !
sucursalesViewModel
�� /
)
��/ 0
;
��0 1
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
ObtenerSucursal
�� )
(
��) *
int
��* -

idSucursal
��. 8
)
��8 9
{
�� 	
try
�� 
{
�� 
(EstablecimientoComercialExtendidoConLogo
�� 8
sucursal
��9 A
=
��B C#
_establecimientoDatos
��D Y
.
��Y Z>
/ObtenerEstablecimientoComercialExtendidoConLogo��Z �
(��� �

idSucursal��� �
)��� �
;��� �'
RegistroSucursalViewModel
�� )
sucursalViewModel
��* ;
=
��< =
new
��> A'
RegistroSucursalViewModel
��B [
(
��[ \
sucursal
��\ d
)
��d e
;
��e f
return
�� 
Json
�� 
(
�� 
sucursalViewModel
�� -
)
��- .
;
��. /
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
EliminarSucursal
�� *
(
��* +
int
��+ .

idSucursal
��/ 9
)
��9 :
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
_sucursalLogica
��, ;
.
��; <
DarDeBajaSucursal
��< M
(
��M N

idSucursal
��N X
)
��X Y
;
��Y Z
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str
��; [
)
��[ \
;
��\ ]
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
code_result
��6 A
=
��B C
	resultado
��D M
.
��M N
code_result
��N Y
,
��Y Z
data
��[ _
=
��` a
	resultado
��b k
.
��k l
data
��l p
,
��p q!
result_description��r �
=��� �
	resultado��� �
.��� �
title��� �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M u
,
��u v
e
��w x
)
��x y
)
��y z
,
��z {
HttpStatusCode��| �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� #
CrearCentroDeAtencion
�� /
(
��/ 0/
!RegistroCentroDeAtencionViewModel
��0 Q
centroDeAtencion
��R b
,
��b c
int
��d g)
idEstablecimientoComercial��h �
)��� �
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
List
�� 
<
�� 
int
�� 
>
�� 
roles
�� 
=
��  !
new
��" %
List
��& *
<
��* +
int
��+ .
>
��. /
(
��/ 0
)
��0 1
;
��1 2
roles
�� 
=
�� 
centroDeAtencion
�� (
.
��( )
Roles
��) .
.
��. /
Select
��/ 5
(
��5 6
r
��6 7
=>
��8 :
r
��; <
.
��< =
Id
��= ?
)
��? @
.
��@ A
ToList
��A G
(
��G H
)
��H I
;
��I J
if
�� 
(
�� 
centroDeAtencion
�� $
.
��$ %
Id
��% '
>
��( )
$num
��* +
)
��+ ,
{
�� 
	resultado
�� 
=
�� #
_centroAtencionLogica
��  5
.
��5 6(
ActualizarCentroDeAtencion
��6 P
(
��P Q
ProfileData
��Q \
(
��\ ]
)
��] ^
.
��^ _
Empleado
��_ g
.
��g h
Id
��h j
,
��j k
centroDeAtencion
��l |
.
��| }
IdActor��} �
,��� � 
centroDeAtencion��� �
.��� �
Id��� �
,��� � 
centroDeAtencion��� �
.��� �
Codigo��� �
,��� � 
centroDeAtencion��� �
.��� �
Nombre��� �
,��� � 
centroDeAtencion��� �
.��� �$
SalidaBienesSinStock��� �
,��� �
roles��� �
,��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
}
�� 
else
�� 
{
�� 
if
�� 
(
�� 
centroDeAtencion
�� (
.
��( )
IdActor
��) 0
>
��1 2
$num
��3 4
)
��4 5
{
�� 
	resultado
�� !
=
��" #
new
��$ '
OperationResult
��( 7
(
��7 8
)
��8 9
;
��9 :
}
�� 
else
�� 
{
�� 
	resultado
�� !
=
��" ##
_centroAtencionLogica
��$ 9
.
��9 :#
CrearCentroDeAtencion
��: O
(
��O P
ProfileData
��P [
(
��[ \
)
��\ ]
.
��] ^
Empleado
��^ f
.
��f g
Id
��g i
,
��i j
centroDeAtencion
��k {
.
��{ |
Codigo��| �
,��� � 
centroDeAtencion��� �
.��� �
Nombre��� �
,��� � 
centroDeAtencion��� �
.��� �$
SalidaBienesSinStock��� �
,��� �
roles��� �
,��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
}
�� 
}
�� 
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str
��; `
)
��` a
;
��a b
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
	resultado
��6 ?
.
��? @
code_result
��@ K
,
��K L
	resultado
��M V
.
��V W
data
��W [
,
��[ \ 
result_description
��] o
=
��p q
	resultado
��r {
.
��{ |
title��| �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M {
,
��{ |
e
��} ~
)
��~ 
)�� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� -
ObtenerCentrosDeAtencionBandeja
�� 9
(
��9 :
int
��: =(
idEstablecimientoComercial
��> X
)
��X Y
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C
new
��D G
List
��H L
<
��L M'
CentroDeAtencionExtendido
��M f
>
��f g
(
��g h
)
��h i
;
��i j
centrosDeAtencion
�� !
=
��" ##
_centroAtencionLogica
��$ 9
.
��9 :I
;ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial
��: u
(
��u v)
idEstablecimientoComercial��v �
)��� �
;��� �
List
�� 
<
�� .
 BandejaCentroDeAtencionViewModel
�� 5
>
��5 6(
centrosDeAtencionViewModel
��7 Q
=
��R S.
 BandejaCentroDeAtencionViewModel
��T t
.
��t u
Convert
��u |
(
��| } 
centrosDeAtencion��} �
)��� �
;��� �
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� &
ObtenerCentrosDeAtencion
�� 2
(
��2 3
)
��3 4
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
CentroDeAtencion
�� %
>
��% &
centrosDeAtencion
��' 8
=
��9 :#
_centroAtencionLogica
��; P
.
��P Q.
 ObtenerCentrosDeAtencionVigentes
��Q q
(
��q r
)
��r s
;
��s t
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� %
ObtenerCentroDeAtencion
�� 1
(
��1 2
int
��2 5 
idCentroDeAtencion
��6 H
,
��H I
int
��J M(
idEstablecimientoComercial
��N h
)
��h i
{
�� 	
try
�� 
{
�� 
CentroDeAtencionExtendido
�� )
centrosDeAtencion
��* ;
=
��< =#
_centroAtencionLogica
��> S
.
��S T2
$ObtenerCentroDeAtencionSucursalOSede
��T x
(
��x y!
idCentroDeAtencion��y �
,��� �*
idEstablecimientoComercial��� �
)��� �
;��� �/
!RegistroCentroDeAtencionViewModel
�� 1'
centroDeAtencionViewModel
��2 K
=
��L M
new
��N Q/
!RegistroCentroDeAtencionViewModel
��R s
(
��s t 
centrosDeAtencion��t �
)��� �
;��� �
return
�� 
Json
�� 
(
�� '
centroDeAtencionViewModel
�� 5
)
��5 6
;
��6 7
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
�� 
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� A
3ObtenerCentrosDeAtencionPorEstablecimientoComercial
�� M
(
��M N
int
��N Q(
idEstablecimientoComercial
��R l
)
��l m
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZJ
;ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial��Z �
(��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� &
EliminarCentroDeAtencion
�� 2
(
��2 3
int
��3 6 
idCentroDeAtencion
��7 I
)
��I J
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +#
_centroAtencionLogica
��, A
.
��A B'
DarDeBajaCentroDeAtencion
��B [
(
��[ \ 
idCentroDeAtencion
��\ n
)
��n o
;
��o p
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str
��; e
)
��e f
;
��f g
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
code_result
��6 A
=
��B C
	resultado
��D M
.
��M N
code_result
��N Y
,
��Y Z
data
��[ _
=
��` a
	resultado
��b k
.
��k l
data
��l p
,
��p q!
result_description��r �
=��� �
	resultado��� �
.��� �
title��� �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M 
,�� �
e��� �
)��� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� T
FEstablecerCentrosDeAtencionParaPreciosYStockDeEstablecimientoComercial
�� `
(
��` a
int
��a d(
idEstablecimientoComercial
��e 
,�� �
int��� �)
idCentroDeAtencionPrecios��� �
,��� �
int��� �'
idCentroDeAtencionStock��� �
)��� �
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +#
_centroAtencionLogica
��, A
.
��A BT
EEstablecerCentroDeAtencionParaPreciosYStockDeEstablecimientoComercial��B �
(��� �*
idEstablecimientoComercial��� �
,��� �)
idCentroDeAtencionPrecios��� �
,��� �'
idCentroDeAtencionStock��� �
)��� �
;��� �
Util
�� 
.
�� (
ManageIfResultIsNotSuccess
�� /
(
��/ 0
	resultado
��0 9
,
��9 :
$str��; �
)��� �
;��� �
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
	resultado
��6 ?
.
��? @
code_result
��@ K
,
��K L
	resultado
��M V
.
��V W
data
��W [
,
��[ \ 
result_description
��] o
=
��p q
	resultado
��r {
.
��{ |
title��| �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
)��� �
;��� �
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str��M �
,��� �
e��� �
)��� �
)��� �
,��� �
HttpStatusCode��� �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� B
4ObtenerCentrosDeAtencionConRolPuntoDeVentaNoVigentes
�� N
(
��N O
)
��O P
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
comboGenericoPuntosDeVenta
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i#
_centroAtencionLogica
��i ~
.
��~ -
ObtenerPuntosDeVentaNoVigentes�� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� (
comboGenericoPuntosDeVenta
�� 6
)
��6 7
;
��7 8
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� C
5ObtenerCentrosDeAtencionConRolPuntoDeCompraNoVigentes
�� O
(
��O P
)
��P Q
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,)
comboGenericoPuntosDeCompra
��- H
=
��I J$
ComboGenericoViewModel
��K a
.
��a b
Convert
��b i
(
��i j#
_centroAtencionLogica
��j 
.�� �/
ObtenerPuntosDeCompraNoVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� )
comboGenericoPuntosDeCompra
�� 7
)
��7 8
;
��8 9
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� =
/ObtenerCentrosDeAtencionConRolAlmacenNoVigentes
�� I
(
��I J
)
��J K
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,$
comboGenericoAlmacenes
��- C
=
��D E$
ComboGenericoViewModel
��F \
.
��\ ]
Convert
��] d
(
��d e#
_centroAtencionLogica
��e z
.
��z {)
ObtenerAlmacenesNoVigentes��{ �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� $
comboGenericoAlmacenes
�� 2
)
��2 3
;
��3 4
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
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 

JsonResult
�� 8
*ObtenerCentrosDeAtencionConRolPuntoDeVenta
�� D
(
��D E
)
��E F
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
comboGenericoPuntosDeVenta
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i#
_centroAtencionLogica
��i ~
.
��~ +
ObtenerPuntosDeVentaVigentes�� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� (
comboGenericoPuntosDeVenta
�� 6
)
��6 7
;
��7 8
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 9
+ObtenerCentrosDeAtencionConRolPuntoDeCompra
�� E
(
��E F
)
��F G
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,)
comboGenericoPuntosDeCompra
��- H
=
��I J$
ComboGenericoViewModel
��K a
.
��a b
Convert
��b i
(
��i j#
_centroAtencionLogica
��j 
.�� �-
ObtenerPuntosDeCompraVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� )
comboGenericoPuntosDeCompra
�� 7
)
��7 8
;
��8 9
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 0
"ObtenerCentrosDeAtencionConRolCaja
�� <
(
��< =
)
��= >
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,8
*comboGenericoDeEntidadesInternasConRolCaja
��- W
=
��X Y$
ComboGenericoViewModel
��Z p
.
��p q
Convert
��q x
(
��x y$
_centroAtencionLogica��y �
.��� �$
ObtenerCajasVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 8
*comboGenericoDeEntidadesInternasConRolCaja
�� F
)
��F G
;
��G H
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
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 

JsonResult
�� 3
%ObtenerCentrosDeAtencionConRolAlmacen
�� ?
(
��? @
)
��@ A
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,$
comboGenericoAlmacenes
��- C
=
��D E$
ComboGenericoViewModel
��F \
.
��\ ]
Convert
��] d
(
��d e#
_centroAtencionLogica
��e z
.
��z {'
ObtenerAlmacenesVigentes��{ �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� $
comboGenericoAlmacenes
�� 2
)
��2 3
;
��3 4
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
��  !
throw
��" '
e
��( )
;
��) *
}
��+ ,
}
�� 	
public
�� 

JsonResult
�� [
MObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConEstablecimientoComercial
�� g
(
��g h
)
��h i
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
comboGenericoPuntosDeVenta
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` aC
4ConvertirCentroDeAtencionConEstablecimientoComercial��a �
(��� �%
_centroAtencionLogica��� �
.��� �,
ObtenerPuntosDeVentaVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� (
comboGenericoPuntosDeVenta
�� 6
)
��6 7
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� \
NObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConEstablecimientoComercial
�� h
(
��h i
)
��i j
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,)
comboGenericoPuntosDeCompra
��- H
=
��I J$
ComboGenericoViewModel
��K a
.
��a bC
4ConvertirCentroDeAtencionConEstablecimientoComercial��b �
(��� �%
_centroAtencionLogica��� �
.��� �-
ObtenerPuntosDeCompraVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� )
comboGenericoPuntosDeCompra
�� 7
)
��7 8
;
��8 9
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� S
EObtenerCentrosDeAtencionConRolCajaVigentesConEstablecimientoComercial
�� _
(
��_ `
)
��` a
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ , 
comboGenericoCajas
��- ?
=
��@ A$
ComboGenericoViewModel
��B X
.
��X YC
4ConvertirCentroDeAtencionConEstablecimientoComercial��Y �
(��� �%
_centroAtencionLogica��� �
.��� �$
ObtenerCajasVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
��  
comboGenericoCajas
�� .
)
��. /
;
��/ 0
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� V
HObtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercial
�� b
(
��b c
)
��c d
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,$
comboGenericoAlmacenes
��- C
=
��D E$
ComboGenericoViewModel
��F \
.
��\ ]C
4ConvertirCentroDeAtencionConEstablecimientoComercial��] �
(��� �%
_centroAtencionLogica��� �
.��� �(
ObtenerAlmacenesVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� $
comboGenericoAlmacenes
�� 2
)
��2 3
;
��3 4
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� b
TObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConCodigoYEstablecimientoComercial
�� n
(
��n o
)
��o p
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� *
ComboCentroAtencionViewModel
�� 1
>
��1 2(
comboGenericoPuntosDeVenta
��3 M
=
��N O*
ComboCentroAtencionViewModel
��P l
.
��l m
Convert
��m t
(
��t u$
_centroAtencionLogica��u �
.��� �,
ObtenerPuntosDeVentaVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� (
comboGenericoPuntosDeVenta
�� 6
)
��6 7
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� c
UObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConCodigoYEstablecimientoComercial
�� o
(
��o p
)
��p q
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� *
ComboCentroAtencionViewModel
�� 1
>
��1 2)
comboGenericoPuntosDeCompra
��3 N
=
��O P*
ComboCentroAtencionViewModel
��Q m
.
��m n
Convert
��n u
(
��u v$
_centroAtencionLogica��v �
.��� �-
ObtenerPuntosDeCompraVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� )
comboGenericoPuntosDeCompra
�� 7
)
��7 8
;
��8 9
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� Z
LObtenerCentrosDeAtencionConRolCajaVigentesConCodigoYEstablecimientoComercial
�� f
(
��f g
)
��g h
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� *
ComboCentroAtencionViewModel
�� 1
>
��1 2 
comboGenericoCajas
��3 E
=
��F G*
ComboCentroAtencionViewModel
��H d
.
��d e
Convert
��e l
(
��l m$
_centroAtencionLogica��m �
.��� �$
ObtenerCajasVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
��  
comboGenericoCajas
�� .
)
��. /
;
��/ 0
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� ]
OObtenerCentrosDeAtencionConRolAlmacenVigentesConCodigoYEstablecimientoComercial
�� i
(
��i j
)
��j k
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� *
ComboCentroAtencionViewModel
�� 1
>
��1 2$
comboGenericoAlmacenes
��3 I
=
��J K*
ComboCentroAtencionViewModel
��L h
.
��h i
Convert
��i p
(
��p q$
_centroAtencionLogica��q �
.��� �(
ObtenerAlmacenesVigentes��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� $
comboGenericoAlmacenes
�� 2
)
��2 3
;
��3 4
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� [
MObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientoComercial
�� g
(
��g h
int
��h k)
idEstablecimientoComercial��l �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZF
7ObtenerPuntosDeVentaVigentesPorEstablecimientoComercial��Z �
(��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� \
NObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientoComercial
�� h
(
��h i
int
��i l)
idEstablecimientoComercial��m �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZG
8ObtenerPuntosDeCompraVigentesPorEstablecimientoComercial��Z �
(��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� S
EObtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientoComercial
�� _
(
��_ `
int
��` c(
idEstablecimientoComercial
��d ~
)
��~ 
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y Z>
/ObtenerCajasVigentesPorEstablecimientoComercial��Z �
(��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� V
HObtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientoComercial
�� b
(
��b c
int
��c f)
idEstablecimientoComercial��g �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZB
3ObtenerAlmacenesVigentesPorEstablecimientoComercial��Z �
(��� �*
idEstablecimientoComercial��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� ^
PObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientosComerciales
�� j
(
��j k
int
��k n
[
��n o
]
��o p-
idsEstablecimientosComerciales��q �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZI
:ObtenerPuntosDeVentaVigentesPorEstablecimientosComerciales��Z �
(��� �.
idsEstablecimientosComerciales��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� _
QObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientosComerciales
�� k
(
��k l
int
��l o
[
��o p
]
��p q-
idsEstablecimientosComerciales��r �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZJ
;ObtenerPuntosDeCompraVigentesPorEstablecimientosComerciales��Z �
(��� �.
idsEstablecimientosComerciales��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� V
HObtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientosComerciales
�� b
(
��b c
int
��c f
[
��f g
]
��g h-
idsEstablecimientosComerciales��i �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZA
2ObtenerCajasVigentesPorEstablecimientosComerciales��Z �
(��� �.
idsEstablecimientosComerciales��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� Y
KObtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientosComerciales
�� e
(
��e f
int
��f i
[
��i j
]
��j k-
idsEstablecimientosComerciales��l �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZE
6ObtenerAlmacenesVigentesPorEstablecimientosComerciales��Z �
(��� �.
idsEstablecimientosComerciales��� �
.��� �
ToList��� �
(��� �
)��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� V
HObtenerCentrosDeAtencionPorEstablecimientoComercialParaCarteraDeClientes
�� b
(
��b c
int
��c f)
idEstablecimientoComercial��g �
)��� �
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
int
�� 
>
�� )
excludeIdsCentrosDeAtencion
�� 5
=
��6 7 
actorNegocioLogica
��8 J
.
��J K>
0ObtenerIdsCentrosDeAtencionDeLaCarteraDeClientes
��K {
(
��{ |
)
��| }
;
��} ~
List
�� 
<
�� '
CentroDeAtencionExtendido
�� .
>
��. /
centrosDeAtencion
��0 A
=
��B C#
_centroAtencionLogica
��D Y
.
��Y ZJ
;ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial��Z �
(��� �*
idEstablecimientoComercial��� �
)��� �
.��� �
Where��� �
(��� �
ca��� �
=>��� �
!��� �+
excludeIdsCentrosDeAtencion��� �
.��� �
Contains��� �
(��� �
ca��� �
.��� �
Id��� �
)��� �
)��� �
.��� �
ToList��� �
(��� �
)��� �
;��� �
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,(
centrosDeAtencionViewModel
��- G
=
��H I$
ComboGenericoViewModel
��J `
.
��` a
Convert
��a h
(
��h i
centrosDeAtencion
��i z
)
��z {
;
��{ |
return
�� 
Json
�� 
(
�� (
centrosDeAtencionViewModel
�� 6
)
��6 7
;
��7 8
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
return
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
}
�� 
}�� ��
[D:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\ActorComercialController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
Areas 
. 
Administracion +
.+ ,
Controllers, 7
{ 
public 

class $
ActorComercialController )
:* +
ActorBaseController, ?
{ 
private 
new 
readonly 
IActorNegocioLogica 0
actorNegocioLogica1 C
;C D
private 
readonly *
IValidacionActorNegocio_Logica 7)
_validacionActorNegocioLogica8 U
;U V
public $
ActorComercialController '
(' (
)( )
{ 	)
_validacionActorNegocioLogica )
=* +
Dependencia, 7
.7 8
Resolve8 ?
<? @*
IValidacionActorNegocio_Logica@ ^
>^ _
(_ `
)` a
;a b
actorNegocioLogica 
=  
Dependencia! ,
., -
Resolve- 4
<4 5
IActorNegocioLogica5 H
>H I
(I J
)J K
;K L
} 	
public$$ 

JsonResult$$ 9
-ResolverActorComercialPorDocumentoDeIdentidad$$ G
($$G H
int$$H K
idRol$$L Q
,$$Q R
string$$S Y
numeroDocumento$$Z i
)$$i j
{%% 	
try&& 
{'' 
OperationResult(( 
result((  &
=((' (
new(() ,
OperationResult((- <
(((< =
OperationResultEnum((= P
.((P Q
Success((Q X
)((X Y
;((Y Z
ActorComercial_)) 
actorComercial))  .
=))/ 0"
ResolverActorComercial))1 G
())G H
idRol))H M
,))M N
numeroDocumento))O ^
)))^ _
;))_ `
return** 
new**  
JsonHttpStatusResult** /
(**/ 0
new**0 3
{**4 5
result**6 <
.**< =
code_result**= H
,**H I
result**J P
.**P Q
data**Q U
,**U V
result_description**W i
=**j k
result**l r
.**r s
title**s x
,**x y
information	**z �
=
**� �
actorComercial
**� �
}
**� �
,
**� �
HttpStatusCode
**� �
.
**� �
OK
**� �
)
**� �
;
**� �
}++ 
catch,, 
(,, 
	Exception,, 
e,, 
),, 
{-- 
return.. 
new..  
JsonHttpStatusResult.. /
(../ 0
Util..0 4
...4 5
	ErrorJson..5 >
(..> ?
e..? @
)..@ A
,..A B
HttpStatusCode..C Q
...Q R
InternalServerError..R e
)..e f
;..f g
}// 
}00 	
public22 
ActorComercial_22 "
ResolverActorComercial22 5
(225 6
int226 9
idRol22: ?
,22? @
string22A G
numeroDocumento22H W
)22W X
{33 	
ActorComercial_44 
actorComercial44 *
=44+ ,
actorNegocioLogica44- ?
.44? @?
3ObtenerActorComercialCreandoloSiExisteSoloComoActor44@ s
(44s t
idRol44t y
,44y z
numeroDocumento	44{ �
)
44� �
;
44� �
if55 
(55 
actorComercial55 
==55 !
null55" &
)55& '
{66 
RegistroActorComercial77 &"
registroActorComercial77' =
=77> ?
null77@ D
;77D E
ItemGenerico88 "
tipoDocumentoIdentidad88 3
=884 5
actorNegocioLogica886 H
.88H I0
$DeterminarTipoDeDocumentoDeIdentidad88I m
(88m n
numeroDocumento88n }
)88} ~
;88~ 
if99 
(99 "
tipoDocumentoIdentidad99 *
==99+ -
null99. 2
)992 3
{:: 
throw;; 
new;; 
ControllerException;; 1
(;;1 2
$str;;2 q
);;q r
;;;r s
}<< 
if== 
(== "
tipoDocumentoIdentidad== *
.==* +
Id==+ -
====. 0

.==> ?
Default==? F
.==F G'
IdTipoDocumentoIdentidadDni==G b
)==b c
{>> "
registroActorComercial?? *
=??+ ,
ObtenerActorDniApi??- ?
(??? @
numeroDocumento??@ O
)??O P
.??P Q
ConvertirConDni??Q `
(??` a
numeroDocumento??a p
)??p q
;??q r
}@@ 
elseAA 
ifAA 
(AA "
tipoDocumentoIdentidadAA /
.AA/ 0
IdAA0 2
==AA3 5

.AAC D
DefaultAAD K
.AAK L'
IdTipoDocumentoIdentidadRucAAL g
)AAg h
{BB "
registroActorComercialCC *
=CC+ ,
ObtenerActorRucApiCC- ?
(CC? @
numeroDocumentoCC@ O
)CCO P
.CCP Q
ConvertirConRucCCQ `
(CC` a
numeroDocumentoCCa p
)CCp q
;CCq r
}DD 
varEE 
resultEE 
=EE 
actorNegocioLogicaEE /
.EE/ 0!
GuardarActorComercialEE0 E
(EEE F
idRolEEF K
,EEK L"
registroActorComercialEEM c
)EEc d
;EEd e
UtilFF 
.FF &
ManageIfResultIsNotSuccessFF /
(FF/ 0
resultFF0 6
,FF6 7
$str	FF8 �
)
FF� �
;
FF� �
actorComercialGG 
=GG  
(GG! "
ActorComercial_GG" 1
)GG1 2
resultGG2 8
.GG8 9
informationGG9 D
;GGD E
}HH 
returnII 
actorComercialII !
;II! "
}JJ 	
publicSS 

JsonResultSS )
ResolverObtenerActorComercialSS 7
(SS7 8
intSS8 ;
idRolSS< A
,SSA B
intSSC F
idTipoDocumentoSSG V
,SSV W
stringSSX ^
numeroDocumentoSS_ n
)SSn o
{TT 	
ActorComercial_UU 
actorComercialUU *
=UU+ ,
newUU- 0
ActorComercial_UU1 @
(UU@ A
)UUA B
;UUB C
tryVV 
{WW 
_validacionActorNegocioLogicaXX -
.XX- .%
ValidarDocumentoIdentidadXX. G
(XXG H
numeroDocumentoXXH W
,XXW X
newXXY \
ItemGenericoXX] i
(XXi j
idTipoDocumentoXXj y
)XXy z
)XXz {
;XX{ |-
!RespuestaVerificacionActorNegocioYY 1
	respuestaYY2 ;
=YY< =
actorNegocioLogicaYY> P
.YYP Q
VerificarActorYYQ _
(YY_ `
idTipoDocumentoYY` o
,YYo p
numeroDocumento	YYq �
,
YY� �
idRol
YY� �
)
YY� �
;
YY� �
ifZZ 
(ZZ 
	respuestaZZ 
.ZZ 
	respuestaZZ '
==ZZ( *%
RespuestaVerificacionEnumZZ+ D
.ZZD E
ExisteSoloActorZZE T
)ZZT U
{[[ 
actorComercial\\ "
=\\# $
new\\% (
ActorComercial_\\) 8
(\\8 9
	respuesta\\9 B
.\\B C
actor\\C H
)\\H I
;\\I J
}]] 
if^^ 
(^^ 
	respuesta^^ 
.^^ 
	respuesta^^ '
==^^( *%
RespuestaVerificacionEnum^^+ D
.^^D E
ExisteActorNegocio^^E W
)^^W X
{__ 
actorComercial`` "
=``# $
new``% (
ActorComercial_``) 8
(``8 9
idRol``9 >
,``> ?
	respuesta``@ I
.``I J
actorNegocio``J V
)``V W
;``W X
}aa 
ifbb 
(bb 
	respuestabb 
.bb 
	respuestabb '
==bb( *%
RespuestaVerificacionEnumbb+ D
.bbD E

)bbR S
{cc 
ifdd 
(dd 
idTipoDocumentodd '
==dd( *

.dd8 9
Defaultdd9 @
.dd@ A'
IdTipoDocumentoIdentidadDniddA \
)dd\ ]
{ee 
actorComercialff &
=ff' (
ObtenerActorDniApiff) ;
(ff; <
numeroDocumentoff< K
)ffK L
.ffL M
ConvertirConDniffM \
(ff\ ]
numeroDocumentoff] l
)ffl m
;ffm n
}gg 
elsehh 
ifhh 
(hh 
idTipoDocumentohh ,
==hh- /

.hh= >
Defaulthh> E
.hhE F'
IdTipoDocumentoIdentidadRuchhF a
)hha b
{ii 
actorComercialjj &
=jj' (
ObtenerActorRucApijj) ;
(jj; <
numeroDocumentojj< K
)jjK L
.jjL M
ConvertirConRucjjM \
(jj\ ]
numeroDocumentojj] l
)jjl m
;jjm n
}kk 
elsell 
{mm 
thrownn 
newnn !
ControllerExceptionnn" 5
(nn5 6
$strnn6 
)	nn �
;
nn� �
}oo 
}pp 
returnqq 
Jsonqq 
(qq 
newqq 
{qq  !
	respuestaqq" +
.qq+ ,
	respuestaqq, 5
,qq5 6
actorComercialqq7 E
}qqF G
)qqG H
;qqH I
}rr 
catchss 
(ss 
	Exceptionss 
ess 
)ss 
{tt 
returnuu 
newuu  
JsonHttpStatusResultuu /
(uu/ 0
Utiluu0 4
.uu4 5
	ErrorJsonuu5 >
(uu> ?
euu? @
)uu@ A
,uuA B
HttpStatusCodeuuC Q
.uuQ R
InternalServerErroruuR e
)uue f
;uuf g
}vv 
}ww 	
publicyy 

JsonResultyy !
GuardarActorComercialyy /
(yy/ 0
intyy0 3
idRolyy4 9
,yy9 :"
RegistroActorComercialyy; Q
actorComercialyyR `
)yy` a
{zz 	
try{{ 
{|| 
OperationResult}} 
result}}  &
;}}& '
result~~ 
=~~ 
actorNegocioLogica~~ +
.~~+ ,!
GuardarActorComercial~~, A
(~~A B
idRol~~B G
,~~G H
actorComercial~~I W
)~~W X
;~~X Y
Util 
. &
ManageIfResultIsNotSuccess /
(/ 0
result0 6
,6 7
$str8 f
)f g
;g h
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
result
��" (
.
��( )
code_result
��) 4
,
��4 5
result
��6 <
.
��< =
data
��= A
,
��A B 
result_description
��C U
=
��V W
result
��X ^
.
��^ _
title
��_ d
,
��d e
result
��f l
.
��l m
information
��m x
}
��y z
)
��z {
;
��{ |
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� ;
-ObtenerParametrosParaSelectorDeActorComercial
�� G
(
��G H
)
��H I
{
�� 	
try
�� 
{
�� 
var
�� 
data
�� 
=
�� 
new
�� 1
#ConfiguracionSelectorActorComercial
�� B
{
�� "
IdEmpleadoPorDefecto
�� (
=
��) *
ProfileData
��+ 6
(
��6 7
)
��7 8
.
��8 9
Empleado
��9 A
.
��A B
Id
��B D
}
�� 
;
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
data
��" &
}
��' (
)
��( )
;
��) *
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� ;
-ObtenerParametrosParaRegistroDeActorComercial
�� G
(
��G H
)
��H I
{
�� 	
try
�� 
{
�� 
var
�� 
data
�� 
=
�� 
new
�� 1
#ConfiguracionRegistroActorComercial
�� B
{
�� 
FechaActual
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
.
��< =
ToString
��= E
(
��E F
$str
��F R
)
��R S
}
�� 
;
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
data
��" &
}
��' (
)
��( )
;
��) *
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� (
ObtenerActorComercialPorId
�� 4
(
��4 5
int
��5 8
idRol
��9 >
,
��> ?
int
��@ C
id
��D F
)
��F G
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
result
��  &
=
��' (
new
��) ,
OperationResult
��- <
(
��< =!
OperationResultEnum
��= P
.
��P Q
Success
��Q X
)
��X Y
;
��Y Z
ActorComercial_
�� 
actorComercial
��  .
=
��/ 0 
actorNegocioLogica
��1 C
.
��C D(
ObtenerActorComercialPorId
��D ^
(
��^ _
idRol
��_ d
,
��d e
id
��f h
)
��h i
;
��i j
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
new
��0 3
{
��4 5
result
��6 <
.
��< =
code_result
��= H
,
��H I
result
��J P
.
��P Q
data
��Q U
,
��U V 
result_description
��W i
=
��j k
result
��l r
.
��r s
title
��s x
,
��x y
information��z �
=��� �
actorComercial��� �
}��� �
,��� �
HttpStatusCode��� �
.��� �
OK��� �
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 6
(ObtenerActoresComercialesPorRolYBusqueda
�� B
(
��B C
int
��C F
idRol
��G L
,
��L M
string
��N T
cadenaBusqueda
��U c
)
��c d
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� $
SelectorActorComercial
�� +
>
��+ ,(
selectorActoresComerciales
��- G
=
��H I 
actorNegocioLogica
��J \
.
��\ ]?
0ObtenerActoresComercialesVigentesPorRolYBusqueda��] �
(��� �
idRol��� �
,��� �
cadenaBusqueda��� �
)��� �
;��� �
var
�� 

jsonResult
�� 
=
��  
Json
��! %
(
��% &(
selectorActoresComerciales
��& @
,
��@ A!
JsonRequestBehavior
��B U
.
��U V
AllowGet
��V ^
)
��^ _
;
��_ `

jsonResult
�� 
.
�� 

�� (
=
��) *
int
��+ .
.
��. /
MaxValue
��/ 7
;
��7 8
return
�� 

jsonResult
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
new
��? B
	Exception
��C L
(
��L M
$str
��M w
,
��w x
e
��y z
)
��z {
)
��{ |
,
��| }
HttpStatusCode��~ �
.��� �#
InternalServerError��� �
)��� �
;��� �
}
�� 
}
�� 	
public
�� 

JsonResult
�� :
,ObtenerActoresComercialesPorCentroDeAtencion
�� F
(
��F G
int
��G J 
idCentroDeAtencion
��K ]
)
��] ^
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
ItemGenerico
�� !
>
��! "
	empleados
��# ,
=
��- . 
actorNegocioLogica
��/ A
.
��A B:
,ObtenerActoresComercialesPorCentroDeAtencion
��B n
(
��n o!
idCentroDeAtencion��o �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	empleados
�� %
)
��% &
;
��& '
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� -
ObtenerGruposActoresComerciales
�� 9
(
��9 :
)
��: ;
{
�� 	
try
�� 
{
�� 
var
�� 
	respuesta
�� 
=
��  
actorNegocioLogica
��  2
.
��2 3-
ObtenerGruposActoresComerciales
��3 R
(
��R S
)
��S T
;
��T U
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� 3
%ObtenerGruposActoresComercialesPorRol
�� ?
(
��? @
int
��@ C
idRol
��D I
)
��I J
{
�� 	
try
�� 
{
�� 
var
�� 
	respuesta
�� 
=
��  
actorNegocioLogica
��  2
.
��2 33
%ObtenerGruposActoresComercialesPorRol
��3 X
(
��X Y
idRol
��Y ^
)
��^ _
;
��_ `
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� F
8ObtenerActoresComercialesDeGrupoActoresComercialesPorRol
�� R
(
��R S
int
��S V
idRol
��W \
,
��\ ]
int
��^ a'
idGrupoActoresComerciales
��b {
)
��{ |
{
�� 	
try
�� 
{
�� 
var
�� 
	respuesta
�� 
=
��  
actorNegocioLogica
��  2
.
��2 3F
8ObtenerActoresComercialesDeGrupoActoresComercialesPorRol
��3 k
(
��k l
idRol
��l q
,
��q r(
idGrupoActoresComerciales��s �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
public
�� 

JsonResult
�� C
5ObtenerGruposActoresComercialesPorRolDeActorComercial
�� O
(
��O P
int
��P S
idRol
��T Y
,
��Y Z
int
��[ ^
idActorComercial
��_ o
)
��o p
{
�� 	
try
�� 
{
�� 
var
�� 
	respuesta
�� 
=
��  
actorNegocioLogica
��  2
.
��2 3C
5ObtenerGruposActoresComercialesPorRolDeActorComercial
��3 h
(
��h i
idRol
��i n
,
��n o
idActorComercial��p �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
	respuesta
�� %
)
��% &
;
��& '
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
�� 
Json
�� 
(
�� 
Util
��  
.
��  !
	ErrorJson
��! *
(
��* +
e
��+ ,
)
��, -
)
��- .
;
��. /
}
�� 
}
�� 	
}
�� 
}�� ��
XD:\Tsp.Sigescom2.3-Back\Tsp.Sigescom.WebApplication\Controllers\Hotel\HotelController.cs
	namespace 	
Tsp
 
.
Sigescom 
. 
WebApplication %
.% &
Controllers& 1
{ 
public 

class 
HotelController  
:! "
BaseController# 1
{ 
private   
readonly   
IHotelLogica   %
hotelLogica  & 1
;  1 2
private!! 
new!! 
readonly!! 
IActorNegocioLogica!! 0
actorNegocioLogica!!1 C
;!!C D
private"" 
readonly"" (
IEstablecimiento_Repositorio"" 5!
_establecimientoDatos""6 K
;""K L
private## 
readonly## $
ICentroDeAtencion_Logica## 1#
_centroDeAtencionLogica##2 I
;##I J
private$$ 
new$$ 
readonly$$ 
IOperacionLogica$$ -
operacionLogica$$. =
;$$= >
private%% 
readonly%% 
IMaestroLogica%% '

;%%5 6
private&& 
readonly&& 
IConceptoLogica&& (
logicaConcepto&&) 7
;&&7 8
	protected'' 
readonly'' )
IFacturacionElectronicaLogica'' 8(
facturacionElectronicaLogica''9 U
;''U V
	protected(( 
readonly(( 
IPdfUtil(( #
pdfUtil(($ +
;((+ ,
	protected)) 
readonly)) 
IBarCodeUtil)) '
barCodeUtil))( 3
;))3 4
	protected** 
readonly** "
IVentaUtilitarioLogica** 1
	ventaUtil**2 ;
;**; <
public,, 
HotelController,, 
(,, 
),,  
{-- 	
hotelLogica.. 
=.. 
Dependencia.. %
...% &
Resolve..& -
<..- .
IHotelLogica... :
>..: ;
(..; <
)..< =
;..= >
actorNegocioLogica// 
=//  
Dependencia//! ,
.//, -
Resolve//- 4
<//4 5
IActorNegocioLogica//5 H
>//H I
(//I J
)//J K
;//K L!
_establecimientoDatos00 !
=00" #
Dependencia00$ /
.00/ 0
Resolve000 7
<007 8(
IEstablecimiento_Repositorio008 T
>00T U
(00U V
)00V W
;00W X#
_centroDeAtencionLogica11 #
=11$ %
Dependencia11& 1
.111 2
Resolve112 9
<119 :$
ICentroDeAtencion_Logica11: R
>11R S
(11S T
)11T U
;11U V
operacionLogica22 
=22 
Dependencia22 )
.22) *
Resolve22* 1
<221 2
IOperacionLogica222 B
>22B C
(22C D
)22D E
;22E F

=33 
Dependencia33 '
.33' (
Resolve33( /
<33/ 0
IMaestroLogica330 >
>33> ?
(33? @
)33@ A
;33A B
logicaConcepto44 
=44 
Dependencia44 (
.44( )
Resolve44) 0
<440 1
IConceptoLogica441 @
>44@ A
(44A B
)44B C
;44C D(
facturacionElectronicaLogica55 (
=55) *
Dependencia55+ 6
.556 7
Resolve557 >
<55> ?)
IFacturacionElectronicaLogica55? \
>55\ ]
(55] ^
)55^ _
;55_ `
barCodeUtil66 
=66 
Dependencia66 %
.66% &
Resolve66& -
<66- .
IBarCodeUtil66. :
>66: ;
(66; <
)66< =
;66= >
	ventaUtil77 
=77 
Dependencia77 #
.77# $
Resolve77$ +
<77+ ,"
IVentaUtilitarioLogica77, B
>77B C
(77C D
)77D E
;77E F
}88 	
[;; 	
	Authorize;;	 
(;; 
Roles;; 
=;; 
$str;; D
);;D E
];;E F
public<< 
ActionResult<< 
Index<< !
(<<! "
)<<" #
{== 	
var>> 
fechaActual>> 
=>> 
DateTimeUtil>> *
.>>* +
FechaActual>>+ 6
(>>6 7
)>>7 8
.>>8 9
Date>>9 =
;>>= >
ViewBag?? 
.?? 

fechaDesde?? 
=??  
fechaActual??! ,
.??, -
AddDays??- 4
(??4 5
-??5 6

.??C D
Default??D K
.??K L;
/DiasAntesALaFechaActualPorDefectoEnPlanificador??L {
)??{ |
.??| }
ToString	??} �
(
??� �
$str
??� �
)
??� �
;
??� �
ViewBag@@ 
.@@ 

fechaHasta@@ 
=@@  
fechaActual@@! ,
.@@, -
AddDays@@- 4
(@@4 5

.@@B C
Default@@C J
.@@J K=
1DiasDespuesALaFechaActualPorDefectoEnPlanificador@@K |
)@@| }
.@@} ~
ToString	@@~ �
(
@@� �
$str
@@� �
)
@@� �
;
@@� �
ViewBagAA 
.AA 
fechaActualAA 
=AA  !
fechaActualAA" -
.AA- .
ToStringAA. 6
(AA6 7
$strAA7 C
)AAC D
;AAD E
ViewBagBB 
.BB $
accionesEstadoDisponibleBB ,
=BB- .
DiccionarioHotelBB/ ?
.BB? @0
$AccionesDeEstadoHabitacionDisponibleBB@ d
(BBd e
)BBe f
;BBf g
ViewBagCC 
.CC !
accionesEstadoOcupadaCC )
=CC* +
DiccionarioHotelCC, <
.CC< =-
!AccionesDeEstadoHabitacionOcupadaCC= ^
(CC^ _
)CC_ `
;CC` a
ViewBagDD 
.DD #
accionesEstadoReservadaDD +
=DD, -
DiccionarioHotelDD. >
.DD> ?/
#AccionesDeEstadoHabitacionReservadaDD? b
(DDb c
)DDc d
;DDd e
ViewBagEE 
.EE !
EstablecimientoSesionEE )
=EE* +
ProfileDataEE, 7
(EE7 8
)EE8 9
.EE9 :0
$EstablecimientoComercialSeleccionadoEE: ^
.EE^ _
ToItemGenericoEE_ m
(EEm n
)EEn o
;EEo p
ViewBagFF 
.FF 1
%UsuarioTieneRolAdministradorDeNegocioFF 9
=FF: ;
ProfileDataFF< G
(FFG H
)FFH I
.FFI J
EmpleadoFFJ R
.FFR S
TieneRolFFS [
(FF[ \

.FFi j
DefaultFFj q
.FFq r(
idRolAdministradorDeNegocio	FFr �
)
FF� �
;
FF� �
ViewBagGG 
.GG 
EstablecimientosGG $
=GG% &
ViewBagGG' .
.GG. /1
%UsuarioTieneRolAdministradorDeNegocioGG/ T
?GGU V!
_establecimientoDatosGGW l
.GGl mI
<ObtenerEstablecimientosComercialesVigentesComoItemsGenericos	GGm �
(
GG� �
)
GG� �
:
GG� �
new
GG� �
List
GG� �
<
GG� �
ItemGenerico
GG� �
>
GG� �
(
GG� �
)
GG� �
{
GG� �
ProfileData
GG� �
(
GG� �
)
GG� �
.
GG� �2
$EstablecimientoComercialSeleccionado
GG� �
.
GG� �
ToItemGenerico
GG� �
(
GG� �
)
GG� �
}
GG� �
;
GG� �
ViewBagHH 
.HH +
MaximoDiasMostrarEnPlanificadorHH 3
=HH4 5

.HHC D
DefaultHHD K
.HHK L+
MaximoDiasMostrarEnPlanificadorHHL k
;HHk l
ViewBagII 
.II )
configuracionEstadoHabitacionII 1
=II2 3)
ConfiguracionEstadoHabitacionII4 Q
.IIQ R
DefaultIIR Y
;IIY Z
returnJJ 
ViewJJ 
(JJ 
)JJ 
;JJ 
}KK 	
[LL 	
	AuthorizeLL	 
(LL 
RolesLL 
=LL 
$strLL D
)LLD E
]LLE F
publicMM 
ActionResultMM 
ReservasMM $
(MM$ %
)MM% &
{NN 	
varOO 
fechaInicialesOO 
=OO  
operacionLogicaOO! 0
.OO0 14
(ObtenerFechaIncioyFinBasadoEnFechaActualOO1 Y
(OOY Z
)OOZ [
;OO[ \
ViewBagPP 
.PP 

fechaDesdePP 
=PP  
fechaInicialesPP! /
[PP/ 0
$numPP0 1
]PP1 2
.PP2 3
ToStringPP3 ;
(PP; <
$strPP< H
)PPH I
;PPI J
ViewBagQQ 
.QQ 

fechaHastaQQ 
=QQ  
fechaInicialesQQ! /
[QQ/ 0
$numQQ0 1
]QQ1 2
.QQ2 3
ToStringQQ3 ;
(QQ; <
$strQQ< H
)QQH I
;QQI J
ViewBagRR 
.RR !
EstablecimientoSesionRR )
=RR* +
ProfileDataRR, 7
(RR7 8
)RR8 9
.RR9 :0
$EstablecimientoComercialSeleccionadoRR: ^
.RR^ _
ToItemGenericoRR_ m
(RRm n
)RRn o
;RRo p
ViewBagSS 
.SS 1
%UsuarioTieneRolAdministradorDeNegocioSS 9
=SS: ;
ProfileDataSS< G
(SSG H
)SSH I
.SSI J
EmpleadoSSJ R
.SSR S
TieneRolSSS [
(SS[ \

.SSi j
DefaultSSj q
.SSq r(
idRolAdministradorDeNegocio	SSr �
)
SS� �
;
SS� �
ViewBagTT 
.TT 
EstablecimientosTT $
=TT% &
ViewBagTT' .
.TT. /1
%UsuarioTieneRolAdministradorDeNegocioTT/ T
?TTU V!
_establecimientoDatosTTW l
.TTl mI
<ObtenerEstablecimientosComercialesVigentesComoItemsGenericos	TTm �
(
TT� �
)
TT� �
:
TT� �
new
TT� �
List
TT� �
<
TT� �
ItemGenerico
TT� �
>
TT� �
(
TT� �
)
TT� �
{
TT� �
ProfileData
TT� �
(
TT� �
)
TT� �
.
TT� �2
$EstablecimientoComercialSeleccionado
TT� �
.
TT� �
ToItemGenerico
TT� �
(
TT� �
)
TT� �
}
TT� �
;
TT� �
returnUU 
ViewUU 
(UU 
)UU 
;UU 
}VV 	
[WW 	
	AuthorizeWW	 
(WW 
RolesWW 
=WW 
$strWW D
)WWD E
]WWE F
publicXX 
ActionResultXX 
ConsumosXX $
(XX$ %
)XX% &
{YY 	
stringZZ 
mascaraDeIngresoZZ #
=ZZ$ %
VentasSettingsZZ& 4
.ZZ4 5
DefaultZZ5 <
.ZZ< =,
 MascaraDeCamposAIngresarEnVentasZZ= ]
;ZZ] ^
var[[ 
fechaActual[[ 
=[[ 
DateTimeUtil[[ *
.[[* +
FechaActual[[+ 6
([[6 7
)[[7 8
.[[8 9
Date[[9 =
;[[= >
ViewBag\\ 
.\\ 
fechaActual\\ 
=\\  !
fechaActual\\" -
.\\- .
ToString\\. 6
(\\6 7
$str\\7 C
)\\C D
;\\D E
ViewBag]] 
.]] !
EstablecimientoSesion]] )
=]]* +
ProfileData]], 7
(]]7 8
)]]8 9
.]]9 :0
$EstablecimientoComercialSeleccionado]]: ^
.]]^ _
ToItemGenerico]]_ m
(]]m n
)]]n o
;]]o p
ViewBag^^ 
.^^ 1
%UsuarioTieneRolAdministradorDeNegocio^^ 9
=^^: ;
ProfileData^^< G
(^^G H
)^^H I
.^^I J
Empleado^^J R
.^^R S
TieneRol^^S [
(^^[ \

.^^i j
Default^^j q
.^^q r(
idRolAdministradorDeNegocio	^^r �
)
^^� �
;
^^� �
ViewBag__ 
.__ 
Establecimientos__ $
=__% &
ViewBag__' .
.__. /1
%UsuarioTieneRolAdministradorDeNegocio__/ T
?__U V!
_establecimientoDatos__W l
.__l mI
<ObtenerEstablecimientosComercialesVigentesComoItemsGenericos	__m �
(
__� �
)
__� �
:
__� �
new
__� �
List
__� �
<
__� �
ItemGenerico
__� �
>
__� �
(
__� �
)
__� �
{
__� �
ProfileData
__� �
(
__� �
)
__� �
.
__� �2
$EstablecimientoComercialSeleccionado
__� �
.
__� �
ToItemGenerico
__� �
(
__� �
)
__� �
}
__� �
;
__� �
ViewBagaa 
.aa $
permitirIngresarCantidadaa ,
=aa- .
	ventaUtilaa/ 8
.aa8 9*
ObtenerCamposEditablesEnVentasaa9 W
(aaW X
mascaraDeIngresoaaX h
,aah i*
ElementoDeCalculoEnVentasEnum	aaj �
.
aa� �
Cantidad
aa� �
)
aa� �
;
aa� �
ViewBagbb 
.bb .
"cursorPorDefectoCodigoBarraEnVentabb 6
=bb7 8
VentasSettingsbb9 G
.bbG H
DefaultbbH O
.bbO P0
$CursorPorDefectoEnCodigoBarraEnVentabbP t
;bbt u
ViewBagcc 
.cc ,
 flujoDespuesDeCodigoBarraEnVentacc 4
=cc5 6
VentasSettingscc7 E
.ccE F
DefaultccF M
.ccM N,
 FlujoDespuesDeCodigoBarraEnVentaccN n
;ccn o
ViewBagdd 
.dd #
checketDetalleUnificadodd +
=dd, -
AplicacionSettingsdd. @
.dd@ A
DefaultddA H
.ddH I#
ChecketDetalleUnificadoddI `
;dd` a
ViewBagee 
.ee 
tasaIGVee 
=ee 
TransaccionSettingsee 1
.ee1 2
Defaultee2 9
.ee9 :
TasaIGVee: A
;eeA B
ViewBagff 
.ff 
aplicaLeyAmazoniaff %
=ff& '
TransaccionSettingsff( ;
.ff; <
Defaultff< C
.ffC D
AplicaLeyAmazoniaffD U
;ffU V
ViewBaggg 
.gg #
numeroDecimalesEnPreciogg +
=gg, -
AplicacionSettingsgg. @
.gg@ A
DefaultggA H
.ggH I#
NumeroDecimalesEnPrecioggI `
;gg` a
ViewBaghh 
.hh &
mascaraDeCalculoPorDefectohh .
=hh/ 0
VentasSettingshh1 ?
.hh? @
Defaulthh@ G
.hhG H.
"MascaraDeCalculoPorDefectoEnVentashhH j
;hhj k
ViewBagii 
.ii )
idConceptoBasicoBolsaPlasticaii 1
=ii2 3
MaestroSettingsii4 C
.iiC D
DefaultiiD K
.iiK L7
+IdDetalleMaestroConceptoBasicoBolsaPlasticaiiL w
;iiw x
ViewBagjj 
.jj -
!ventasSujetasADisponibilidadStockjj 5
=jj6 7
!jj8 9
ProfileDatajj9 D
(jjD E
)jjE F
.jjF G(
CentroDeAtencionSeleccionadojjG c
.jjc d 
SalidaBienesSinStockjjd x
;jjx y
ViewBagkk 
.kk -
!aplicarCantidadPorDefectoEnVentaskk 5
=kk6 7
AplicacionSettingskk8 J
.kkJ K
DefaultkkK R
.kkR S-
!AplicarCantidadPorDefectoEnVentaskkS t
;kkt u
ViewBagll 
.ll &
cantidadPorDefectoEnVentasll .
=ll/ 0
AplicacionSettingsll1 C
.llC D
DefaultllD K
.llK L&
CantidadPorDefectoEnVentasllL f
;llf g
ViewBagmm 
.mm $
modoSeleccionTipoFamiliamm ,
=mm- .
AplicacionSettingsmm/ A
.mmA B
DefaultmmB I
.mmI J0
$ModoDeSeleccionTipoDeFamiliaEnVentasmmJ n
;mmn o
ViewBagnn 
.nn )
modoIngresoCodigoBarraEnVentann 1
=nn2 3
VentasSettingsnn4 B
.nnB C
DefaultnnC J
.nnJ K/
#ModoDeIngresoDeCodigoDeBarraEnVentannK n
;nnn o
ViewBagoo 
.oo %
modoDeSeleccionDeConceptooo -
=oo. /
VentasSettingsoo0 >
.oo> ?
Defaultoo? F
.ooF G.
"ModoDeSeleccionDeConceptoDeNegocioooG i
;ooi j
ViewBagpp 
.pp *
minimoCaracteresBuscarConceptopp 2
=pp3 4
AplicacionSettingspp5 G
.ppG H
DefaultppH O
.ppO P:
.MinimoDeCaracteresParaBuscarEnSelectorConceptoppP ~
;pp~ 
ViewBagqq 
.qq (
tiempoEsperaBusquedaSelectorqq 0
=qq1 2
AplicacionSettingsqq3 E
.qqE F
DefaultqqF M
.qqM N<
0TiempoDeEsperaEnBusquedaSelectoresDeGranCantidadqqN ~
;qq~ 
returnrr 
Viewrr 
(rr 
)rr 
;rr 
}ss 	
[tt 	
	Authorizett	 
(tt 
Rolestt 
=tt 
$strtt 1
)tt1 2
]tt2 3
publicuu 
ActionResultuu 
Habitacionesuu (
(uu( )
)uu) *
{vv 	
ViewBagww 
.ww '
idEstablecimientoPorDefectoww /
=ww0 1
ProfileDataww2 =
(ww= >
)ww> ?
.ww? @0
$EstablecimientoComercialSeleccionadoww@ d
.wwd e
Idwwe g
;wwg h
returnxx 
Viewxx 
(xx 
)xx 
;xx 
}yy 	
[zz 	
	Authorizezz	 
(zz 
Roleszz 
=zz 
$strzz 1
)zz1 2
]zz2 3
public{{ 
ActionResult{{ 
TipoHabitacion{{ *
({{* +
){{+ ,
{|| 	
ViewBag}} 
.}} 
idFamiliaHabitacion}} '
=}}( )

.}}7 8
Default}}8 ?
.}}? @-
!IdDetalleMaestroFamiliaHabitacion}}@ a
;}}a b
ViewBag~~ 
.~~ (
idCaracteristicaAforoAdultos~~ 0
=~~1 2

.~~@ A
Default~~A H
.~~H I(
IdCaracteristicaAforoAdultos~~I e
;~~e f
ViewBag 
. &
idCaracteristicaAforoNinos .
=/ 0

.> ?
Default? F
.F G&
IdCaracteristicaAforoNinosG a
;a b
ViewBag
�� 
.
�� %
numeroDecimalesEnPrecio
�� +
=
��, - 
AplicacionSettings
��. @
.
��@ A
Default
��A H
.
��H I%
NumeroDecimalesEnPrecio
��I `
;
��` a
ViewBag
�� 
.
�� -
tamanioMaximoFotoTipoHabitacion
�� 3
=
��4 5

��6 C
.
��C D
Default
��D K
.
��K L-
TamanioMaximoFotoTipoHabitacion
��L k
;
��k l
ViewBag
�� 
.
�� .
 maximaCantidadFotoTipoHabitacion
�� 4
=
��5 6

��7 D
.
��D E
Default
��E L
.
��L M.
 MaximaCantidadFotoTipoHabitacion
��M m
;
��m n
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� 1
)
��1 2
]
��2 3
public
�� 
ActionResult
�� 
	Ambientes
�� %
(
��% &
)
��& '
{
�� 	
ViewBag
�� 
.
�� )
idEstablecimientoPorDefecto
�� /
=
��0 1
ProfileData
��2 =
(
��= >
)
��> ?
.
��? @2
$EstablecimientoComercialSeleccionado
��@ d
.
��d e
Id
��e g
;
��g h
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
	Authorize
��	 
(
�� 
Roles
�� 
=
�� 
$str
�� D
)
��D E
]
��E F
public
�� 
ActionResult
�� 
DetalleReserva
�� *
(
��* +
long
��+ /
idEstablecimiento
��0 A
,
��A B
long
��C G
idAtencionMacro
��H W
,
��W X
long
��Y ]

idAtencion
��^ h
)
��h i
{
�� 	
ViewBag
�� 
.
�� '
accionesProcesoHabitacion
�� -
=
��. /
DiccionarioHotel
��0 @
.
��@ A)
AccionesProcesoDeHabitacion
��A \
.
��\ ]
ToList
��] c
(
��c d
)
��d e
;
��e f
ViewBag
�� 
.
�� 
idAtencionMacro
�� #
=
��$ %
idAtencionMacro
��& 5
;
��5 6
ViewBag
�� 
.
�� 

idAtencion
�� 
=
��  

idAtencion
��! +
;
��+ ,
ViewBag
�� 
.
�� 
idRolCliente
��  
=
��! "

��# 0
.
��0 1
Default
��1 8
.
��8 9
IdRolCliente
��9 E
;
��E F
ViewBag
�� 
.
�� 
idClienteGenerico
�� %
=
��& '

��( 5
.
��5 6
Default
��6 =
.
��= >
IdClienteGenerico
��> O
;
��O P
ViewBag
�� 
.
�� *
tiempoEsperaBusquedaSelector
�� 0
=
��1 2 
AplicacionSettings
��3 E
.
��E F
Default
��F M
.
��M N>
0TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad
��N ~
;
��~ 
ViewBag
�� 
.
�� 2
$minimoCaracteresBuscarActorComercial
�� 8
=
��9 :

��; H
.
��H I
Default
��I P
.
��P QC
4MinimoDeCaracteresParaBuscarEnSelectorActorComercial��Q �
;��� �
ViewBag
�� 
.
�� =
/mascaraDeVisualizacionValidacionRegistroCliente
�� C
=
��D E

��F S
.
��S T
Default
��T [
.
��[ \>
/MascaraDeVisualizacionValidacionRegistroCliente��\ �
;��� �
ViewBag
�� 
.
��  
idEstadoRegistrado
�� &
=
��' (
MaestroSettings
��) 8
.
��8 9
Default
��9 @
.
��@ A.
 IdDetalleMaestroEstadoRegistrado
��A a
;
��a b
ViewBag
�� 
.
��  
idEstadoConfirmado
�� &
=
��' (
MaestroSettings
��) 8
.
��8 9
Default
��9 @
.
��@ A.
 IdDetalleMaestroEstadoConfirmado
��A a
;
��a b
ViewBag
�� 
.
�� 
idEstadoCheckedIn
�� %
=
��& '
MaestroSettings
��( 7
.
��7 8
Default
��8 ?
.
��? @-
IdDetalleMaestroEstadoCheckedIn
��@ _
;
��_ `
ViewBag
�� 
.
��  
idEstadoCheckedOut
�� &
=
��' (
MaestroSettings
��) 8
.
��8 9
Default
��9 @
.
��@ A.
 IdDetalleMaestroEstadoCheckedOut
��A a
;
��a b
ViewBag
�� 
.
�� 
idEstadoFacturado
�� %
=
��& '
MaestroSettings
��( 7
.
��7 8
Default
��8 ?
.
��? @-
IdDetalleMaestroEstadoFacturado
��@ _
;
��_ `
ViewBag
�� 
.
�� 
idEstadoAnulado
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
��= >+
IdDetalleMaestroEstadoAnulado
��> [
;
��[ \
ViewBag
�� 
.
�� %
idEstadoEntradaCambiado
�� +
=
��, -
MaestroSettings
��. =
.
��= >
Default
��> E
.
��E F3
%IdDetalleMaestroEstadoEntradaCambiado
��F k
;
��k l
ViewBag
�� 
.
�� $
idEstadoSalidaCambiado
�� *
=
��+ ,
MaestroSettings
��- <
.
��< =
Default
��= D
.
��D E2
$IdDetalleMaestroEstadoSalidaCambiado
��E i
;
��i j
ViewBag
�� 
.
�� 
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
.
��< =
ToString
��= E
(
��E F
$str
��F R
)
��R S
;
��S T
ViewBag
�� 
.
�� 
idEstablecimiento
�� %
=
��& '
idEstablecimiento
��( 9
;
��9 :
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 
ActionResult
�� 
Complementos
�� (
(
��( )
)
��) *
{
�� 	
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 

JsonResult
�� (
ObtenerReportePlanificador
�� 4
(
��4 5
int
��5 8
idEstablecimiento
��9 J
)
��J K
{
�� 	
try
�� 
{
�� 
ReportePlanificador
�� #
planificador
��$ 0
=
��1 2
hotelLogica
��3 >
.
��> ?(
ObtenerReportePlanificador
��? Y
(
��Y Z
idEstablecimiento
��Z k
,
��k l
ProfileData
��m x
(
��x y
)
��y z
.
��z {1
"IdCentroAtencionQueTieneLosPrecios��{ �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
planificador
�� (
)
��( )
;
��) *
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� -
ObtenerPlanificadorHabitaciones
�� 9
(
��9 :
int
��: =
idEstablecimiento
��> O
,
��O P
string
��Q W

fechaDesde
��X b
,
��b c
string
��d j

fechaHasta
��k u
,
��u v
int
��w z

idAmbiente��{ �
,��� �
int��� � 
idTipoHabitacion��� �
)��� �
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaInicio
�� $
=
��% &
DateTime
��' /
.
��/ 0
Parse
��0 5
(
��5 6

fechaDesde
��6 @
)
��@ A
;
��A B
DateTime
�� 
fechaFin
�� !
=
��" #
DateTime
��$ ,
.
��, -
Parse
��- 2
(
��2 3

fechaHasta
��3 =
)
��= >
;
��> ?
Planificador
�� &
planificadorHabitaciones
�� 5
=
��6 7
hotelLogica
��8 C
.
��C D-
ObtenerPlanificadorHabitaciones
��D c
(
��c d
idEstablecimiento
��d u
,
��u v
fechaInicio��w �
,��� �
fechaFin��� �
,��� �

idAmbiente��� �
,��� � 
idTipoHabitacion��� �
,��� �
ProfileData��� �
(��� �
)��� �
.��� �2
"IdCentroAtencionQueTieneLosPrecios��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� &
planificadorHabitaciones
�� 4
)
��4 5
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� )
CambiarEnLimpiezaHabitacion
�� 5
(
��5 6
int
��6 9
idHabitacion
��: F
)
��F G
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
;
��) *
	resultado
�� 
=
�� 
hotelLogica
�� '
.
��' (+
CambiarEnLimpiezaDeHabitacion
��( E
(
��E F
idHabitacion
��F R
)
��R S
;
��S T
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
ObtenerHabitacion
�� +
(
��+ ,
int
��, /
id
��0 2
)
��2 3
{
�� 	
try
�� 
{
�� 

Habitacion
�� 

habitacion
�� %
=
��& '
hotelLogica
��( 3
.
��3 4
ObtenerHabitacion
��4 E
(
��E F
id
��F H
)
��H I
;
��I J
return
�� 
Json
�� 
(
�� 

habitacion
�� &
)
��& '
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� (
ObtenerHabitacionesBandeja
�� 4
(
��4 5
int
��5 8
idEstablecimiento
��9 J
)
��J K
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
HabitacionBandeja
�� &
>
��& '
habitaciones
��( 4
=
��5 6
hotelLogica
��7 B
.
��B C(
ObtenerHabitacionesBandeja
��C ]
(
��] ^
idEstablecimiento
��^ o
)
��o p
;
��p q
return
�� 
Json
�� 
(
�� 
habitaciones
�� (
)
��( )
;
��) *
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
GuardarHabitacion
�� +
(
��+ ,

Habitacion
��, 6

habitacion
��7 A
)
��A B
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
;
��) *
if
�� 
(
�� 

habitacion
�� 
.
�� 
Id
�� !
!=
��" $
$num
��% &
)
��& '
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ ,
EditarHabitacion
��, <
(
��< =

habitacion
��= G
)
��G H
;
��H I
}
�� 
else
�� 
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ ,
CrearHabitacion
��, ;
(
��; <

habitacion
��< F
)
��F G
;
��G H
}
�� 
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� (
CambiarEsVigenteHabitacion
�� 4
(
��4 5
int
��5 8
id
��9 ;
)
��; <
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
	resultado
�� 
=
�� 
hotelLogica
�� '
.
��' ((
CambiarEsVigenteHabitacion
��( B
(
��B C
id
��C E
)
��E F
;
��F G
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� ,
ObtenerHabitacionesDisponibles
�� 8
(
��8 9
int
��9 <
idTipoHabitacion
��= M
,
��M N
string
��O U

fechaDesde
��V `
,
��` a
string
��b h

fechaHasta
��i s
,
��s t
int
��u x 
idEstablecimiento��y �
,��� �
int��� �

idAmbiente��� �
)��� �
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaInicio
�� $
=
��% &
DateTime
��' /
.
��/ 0
Parse
��0 5
(
��5 6

fechaDesde
��6 @
)
��@ A
;
��A B
DateTime
�� 
fechaFin
�� !
=
��" #
DateTime
��$ ,
.
��, -
Parse
��- 2
(
��2 3

fechaHasta
��3 =
)
��= >
;
��> ?
List
�� 
<
�� 

Habitacion
�� 
>
��  %
habitacionesDisponibles
��! 8
=
��9 :
hotelLogica
��; F
.
��F G,
ObtenerHabitacionesDisponibles
��G e
(
��e f
idTipoHabitacion
��f v
,
��v w
fechaInicio��x �
,��� �
fechaFin��� �
,��� �!
idEstablecimiento��� �
,��� �

idAmbiente��� �
,��� �
ProfileData��� �
(��� �
)��� �
.��� �2
"IdCentroAtencionQueTieneLosPrecios��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� %
habitacionesDisponibles
�� 3
)
��3 4
;
��4 5
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� )
ObtenerHabitacionDisponible
�� 5
(
��5 6
int
��6 9
idHabitacion
��: F
)
��F G
{
�� 	
try
�� 
{
�� 

Habitacion
�� "
habitacionDisponible
�� /
=
��0 1
hotelLogica
��2 =
.
��= >)
ObtenerHabitacionDisponible
��> Y
(
��Y Z
idHabitacion
��Z f
,
��f g
ProfileData
��h s
(
��s t
)
��t u
.
��u v1
"IdCentroAtencionQueTieneLosPrecios��v �
)��� �
;��� �
return
�� 
Json
�� 
(
�� "
habitacionDisponible
�� 0
)
��0 1
;
��1 2
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� -
CambiarVigenciaDelAmbienteHotel
�� 9
(
��9 :
int
��: =
id
��> @
)
��@ A
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
	resultado
�� 
=
�� 
hotelLogica
�� '
.
��' (-
CambiarVigenciaDelAmbienteHotel
��( G
(
��G H
id
��H J
)
��J K
;
��K L
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� 5
'ObtenerAmbientesHotelPorEstablecimiento
�� A
(
��A B
int
��B E
idEstablecimiento
��F W
)
��W X
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 

�� "
>
��" #
ambientesDeHotel
��$ 4
=
��5 6
hotelLogica
��7 B
.
��B C5
'ObtenerAmbientesHotelPorEstablecimiento
��C j
(
��j k
idEstablecimiento
��k |
)
��| }
;
��} ~
return
�� 
Json
�� 
(
�� 
ambientesDeHotel
�� ,
)
��, -
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� D
6ObtenerAmbientesVigentesPorEstablecimientoSimplificado
�� P
(
��P Q
int
��Q T
idEstablecimiento
��U f
)
��f g
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
ItemGenerico
�� !
>
��! "
ambientesDeHotel
��# 3
=
��4 5
hotelLogica
��6 A
.
��A BD
6ObtenerAmbientesVigentesPorEstablecimientoSimplificado
��B x
(
��x y 
idEstablecimiento��y �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
ambientesDeHotel
�� ,
)
��, -
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
GuardarAmbiente
�� )
(
��) *

��* 7
ambiente
��8 @
)
��@ A
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
if
�� 
(
�� 
ambiente
�� 
.
�� 
Id
�� 
!=
��  "
$num
��# $
)
��$ %
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ ,
EditarAmbiente
��, :
(
��: ;
ambiente
��; C
)
��C D
;
��D E
}
�� 
else
�� 
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ ,

��, 9
(
��9 :
ambiente
��: B
)
��B C
;
��C D
}
�� 
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 8
*ObtenerTiposHabitacionVigentesSimplificado
�� D
(
��D E
)
��E F
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
ItemGenerico
�� !
>
��! "%
tipoHabitacionesDeHotel
��# :
=
��; <
hotelLogica
��= H
.
��H I8
*ObtenerTiposHabitacionVigentesSimplificado
��I s
(
��s t
)
��t u
;
��u v
return
�� 
Json
�� 
(
�� %
tipoHabitacionesDeHotel
�� 3
)
��3 4
;
��4 5
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� .
 CambiarVigenciaDelTipoHabitacion
�� :
(
��: ;
int
��; >
id
��? A
)
��A B
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
	resultado
�� 
=
�� 
hotelLogica
�� '
.
��' (.
 CambiarVigenciaDelTipoHabitacion
��( H
(
��H I
id
��I K
)
��K L
;
��L M
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
}
�� 
catch
�� 
(
�� 
LogicaException
�� "
oe
��# %
)
��% &
{
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
oe
��? A
)
��A B
,
��B C
HttpStatusCode
��D R
.
��R S!
InternalServerError
��S f
)
��f g
;
��g h
}
�� 
}
�� 	
public
�� 

JsonResult
�� #
ObtenerTipoHabitacion
�� /
(
��/ 0
int
��0 3
id
��4 6
)
��6 7
{
�� 	
try
�� 
{
�� 
TipoHabitacion
�� 
tipoHabitacion
�� -
=
��. /
hotelLogica
��0 ;
.
��; <#
ObtenerTipoHabitacion
��< Q
(
��Q R
id
��R T
,
��T U
ProfileData
��V a
(
��a b
)
��b c
)
��c d
;
��d e
List
�� 
<
�� $
ComboGenericoViewModel
�� +
>
��+ ,
puntosDePrecio
��- ;
=
��< =$
ComboGenericoViewModel
��> T
.
��T U
Convert
��U \
(
��\ ]%
_centroDeAtencionLogica
��] t
.
��t u$
ObtenerPuntosDePrecio��u �
(��� �
)��� �
)��� �
;��� �
var
�� 
tarifasDePrecio
�� #
=
��$ %$
ComboGenericoViewModel
��& <
.
��< =
Convert
��= D
(
��D E

��E R
.
��R S
obtenerTarifas
��S a
(
��a b
)
��b c
)
��c d
;
��d e
var
��  
preciosCompraVenta
�� &
=
��' (
logicaConcepto
��) 7
.
��7 88
*ObtenerPreciosCompraVentaDeConceptoNegocio
��8 b
(
��b c
tipoHabitacion
��c q
.
��q r
Id
��r t
)
��t u
;
��u v%
RegistroPrecioViewModel
�� '
precios
��( /
=
��0 1
new
��2 5%
RegistroPrecioViewModel
��6 M
(
��M N
tipoHabitacion
��N \
.
��\ ]
Precios
��] d
,
��d e
tipoHabitacion
��f t
.
��t u
Id
��u w
,
��w x
puntosDePrecio��y �
,��� �
tarifasDePrecio��� �
,��� �
DateTimeUtil��� �
.��� �
FechaActual��� �
(��� �
)��� �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
tipoHabitacion
��" 0
,
��0 1
precios
��2 9
}
��: ;
)
��; <
;
��< =
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� ,
ObtenerTipoHabitacionesBandeja
�� 8
(
��8 9
)
��9 :
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� %
TipoHabitacionesBandeja
�� ,
>
��, -%
tipoHabitacionesDeHotel
��. E
=
��F G
hotelLogica
��H S
.
��S T%
ObtenerTipoHabitaciones
��T k
(
��k l
)
��l m
;
��m n
return
�� 
Json
�� 
(
�� %
tipoHabitacionesDeHotel
�� 3
)
��3 4
;
��4 5
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� #
GuardarTipoHabitacion
�� /
(
��/ 0
TipoHabitacion
��0 >
tipoHabitacion
��? M
,
��M N%
RegistroPrecioViewModel
��O f
precios
��g n
)
��n o
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
tipoHabitacion
�� 
.
�� 
Precios
�� &
=
��' (%
RegistroPrecioViewModel
��) @
.
��@ A
Convert
��A H
(
��H I
precios
��I P
)
��P Q
;
��Q R
if
�� 
(
�� 
tipoHabitacion
�� "
.
��" #
Id
��# %
!=
��& (
$num
��) *
)
��* +
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ ,"
EditarTipoHabitacion
��, @
(
��@ A
tipoHabitacion
��A O
,
��O P
ProfileData
��Q \
(
��\ ]
)
��] ^
)
��^ _
;
��_ `
}
�� 
else
�� 
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ ,!
CrearTipoHabitacion
��, ?
(
��? @
tipoHabitacion
��@ N
,
��N O
ProfileData
��P [
(
��[ \
)
��\ ]
)
��] ^
;
��^ _
tipoHabitacion
�� "
.
��" #'
IdsValoresCaracteristicas
��# <
=
��= >
null
��? C
;
��C D
}
�� 
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
ObtenerTipoCamas
�� *
(
��* +
)
��+ ,
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
ItemGenerico
�� !
>
��! "
	tipoCamas
��# ,
=
��- .
hotelLogica
��/ :
.
��: ;
ObtenerTipoCamas
��; K
(
��K L
)
��L M
;
��M N
return
�� 
Json
�� 
(
�� 
	tipoCamas
�� %
)
��% &
;
��& '
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� !
ObtenerComplementos
�� -
(
��- .
)
��. /
{
�� 	
try
�� 
{
�� 
List
�� 
<
�� 
ItemGenerico
�� !
>
��! "
	tipoCamas
��# ,
=
��- .
hotelLogica
��/ :
.
��: ;
ObtenerTipoCamas
��; K
(
��K L
)
��L M
;
��M N
return
�� 
Json
�� 
(
�� 
	tipoCamas
�� %
)
��% &
;
��& '
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
��  
GuardarComplemento
�� ,
(
��, -
Complemento
��- 8
complemento
��9 D
)
��D E
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
;
��) *
if
�� 
(
�� 
complemento
�� 
.
��  
Id
��  "
!=
��# %
$num
��& '
)
��' (
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ ,#
ActualizarComplemento
��, A
(
��A B
complemento
��B M
)
��M N
;
��N O
}
�� 
else
�� 
{
�� 
	resultado
�� 
=
�� 
hotelLogica
��  +
.
��+ , 
GuardarComplemento
��, >
(
��> ?
complemento
��? J
)
��J K
;
��K L
}
�� 
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� $
ObtenerReservasBandeja
�� 0
(
��0 1
int
��1 4
idEstablecimiento
��5 F
,
��F G
string
��H N

fechaDesde
��O Y
,
��Y Z
string
��[ a

fechaHasta
��b l
)
��l m
{
�� 	
try
�� 
{
�� 
DateTime
�� 
fechaInicio
�� $
=
��% &
DateTime
��' /
.
��/ 0
Parse
��0 5
(
��5 6

fechaDesde
��6 @
)
��@ A
;
��A B
DateTime
�� 
fechaFin
�� !
=
��" #
DateTime
��$ ,
.
��, -
Parse
��- 2
(
��2 3

fechaHasta
��3 =
+
��> ?
$str
��@ K
)
��K L
;
��L M
List
�� 
<
�� 
ReservaBandeja
�� #
>
��# $
reservaBandejas
��% 4
=
��5 6
hotelLogica
��7 B
.
��B C#
ObtenerReservaBandeja
��C X
(
��X Y
idEstablecimiento
��Y j
,
��j k
fechaInicio
��l w
,
��w x
fechaFin��y �
)��� �
;��� �
return
�� 
Json
�� 
(
�� 
reservaBandejas
�� +
)
��+ ,
;
��, -
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
�� 
return
�� 
new
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 5
'ObtenerParametrosParaRegistradorReserva
�� A
(
��A B
)
��B C
{
�� 	
try
�� 
{
�� 
var
�� 
fechaActual
�� 
=
��  !
DateTimeUtil
��" .
.
��. /
FechaActual
��/ :
(
��: ;
)
��; <
;
��< =
var
�� 
data
�� 
=
�� 
new
�� "
ConfiguracionReserva
�� 3
{
�� 
FechaActual
�� 
=
��  !
fechaActual
��" -
.
��- .
ToString
��. 6
(
��6 7
$str
��7 C
)
��C D
,
��D E#
AgregarDiaAFechaDesde
�� )
=
��* +
(
��, -
(
��- .
fechaActual
��. 9
.
��9 :
Date
��: >
.
��> ?
AddHours
��? G
(
��G H
$num
��H J
)
��J K
-
��L M
fechaActual
��N Y
)
��Y Z
.
��Z [
TotalMinutes
��[ g
-
��h i

��j w
.
��w x
Default
��x 
.�� �/
ToleranciaEnMinutosParaChecking��� �
)��� �
>��� �
$num��� �
}
�� 
;
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
data
��" &
}
��' (
)
��( )
;
��) *
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 5
'ObtenerParametrosParaRegistradorHuesped
�� A
(
��A B
)
��B C
{
�� 	
try
�� 
{
�� 
var
�� 
data
�� 
=
�� "
ConfiguracionHuesped
�� /
.
��/ 0
Default
��0 7
;
��7 8
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
data
��" &
}
��' (
)
��( )
;
��) *
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� .
 ObtenerConfiguracionParaFacturar
�� :
(
��: ;
)
��; <
{
�� 	
try
�� 
{
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
data
��" &
=
��' (#
ConfiguracionFacturar
��) >
.
��> ?
Default
��? F
}
��G H
)
��H I
;
��I J
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
GuardarReserva
�� (
(
��( ) 
AtencionMacroHotel
��) ;
reserva
��< C
)
��C D
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8
ConfirmarReserva
��8 H
(
��H I
reserva
��I P
,
��P Q
ProfileData
��R ]
(
��] ^
)
��^ _
)
��_ `
;
��` a
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
CheckInReserva
�� (
(
��( ) 
AtencionMacroHotel
��) ;
reserva
��< C
)
��C D
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8
CheckInReserva
��8 F
(
��F G
reserva
��G N
,
��N O
ProfileData
��P [
(
��[ \
)
��\ ]
)
��] ^
;
��^ _
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� -
ObtenerUltimoMotivoViajeHuesped
�� 9
(
��9 :
int
��: =
	idHuesped
��> G
)
��G H
{
�� 	
try
�� 
{
�� 
ItemGenerico
�� 
motivoViaje
�� (
=
��) *
hotelLogica
��+ 6
.
��6 7-
ObtenerUltimoMotivoViajeHuesped
��7 V
(
��V W
	idHuesped
��W `
)
��` a
;
��a b
return
�� 
Json
�� 
(
�� 
motivoViaje
�� '
)
��' (
;
��( )
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
AgregarHuesped
�� (
(
��( )
long
��) -

idAtencion
��. 8
,
��8 9
int
��: =
idActorComercial
��> N
,
��N O
int
��P S

��T a
,
��a b
bool
��c g
	esTitular
��h q
)
��q r
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8
AgregarHuesped
��8 F
(
��F G

idAtencion
��G Q
,
��Q R
idActorComercial
��S c
,
��c d

��e r
,
��r s
	esTitular
��t }
)
��} ~
;
��~ 
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� #
CambiarTitularHuesped
�� /
(
��/ 0
int
��0 3
idHuespedCambiado
��4 E
,
��E F
int
��G J#
idHuespedNuevoTitular
��K `
)
��` a
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8#
CambiarTitularHuesped
��8 M
(
��M N
idHuespedCambiado
��N _
,
��_ `#
idHuespedNuevoTitular
��a v
)
��v w
;
��w x
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
EliminarHuesped
�� )
(
��) *
int
��* -
	idHuesped
��. 7
)
��7 8
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8
EliminarHuesped
��8 G
(
��G H
	idHuesped
��H Q
)
��Q R
;
��R S
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� '
ObtenerAtencionMacroHotel
�� 3
(
��3 4
long
��4 8

idAtencion
��9 C
)
��C D
{
�� 	
try
�� 
{
�� 
AtencionMacroHotel
�� "

��# 0
=
��1 2
hotelLogica
��3 >
.
��> ?"
ObtenerAtencionMacro
��? S
(
��S T

idAtencion
��T ^
,
��^ _
ProfileData
��` k
(
��k l
)
��l m
)
��m n
;
��n o
return
�� 
Json
�� 
(
�� 

�� )
)
��) *
;
��* +
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� /
!ObtenerAtencionDesdeAtencionMacro
�� ;
(
��; <
long
��< @
idAtencionMacro
��A P
)
��P Q
{
�� 	
try
�� 
{
�� 
Atencion
�� 
atencion
�� !
=
��" #
hotelLogica
��$ /
.
��/ 0/
!ObtenerAtencionDesdeAtencionMacro
��0 Q
(
��Q R
idAtencionMacro
��R a
)
��a b
;
��b c
if
�� 
(
�� 
atencion
�� 
.
�� 
TieneFacturacion
�� -
)
��- .
{
�� 
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� *
(
��* +
)
��+ ,
;
��, -
foreach
�� 
(
�� 
var
��  
comprobante
��! ,
in
��- /
atencion
��0 8
.
��8 9$
ComprobantesFacturados
��9 O
)
��O P
{
�� 
OrdenDeVenta
�� $
ordenDeVenta
��% 1
=
��2 3
operacionLogica
��4 C
.
��C D!
ObtenerOrdenDeVenta
��D W
(
��W X
comprobante
��X c
.
��c d
IdOrden
��d k
)
��k l
;
��l m
string
�� 
	QrContent
�� (
=
��) **
facturacionElectronicaLogica
��+ G
.
��G H
	ObtenerQR
��H Q
(
��Q R
ordenDeVenta
��R ^
,
��^ _
sede
��` d
)
��d e
;
��e f
byte
�� 
[
�� 
]
�� 
QrBytes
�� &
=
��' (
barCodeUtil
��) 4
.
��4 5
ObtenerCodigoQR
��5 D
(
��D E
	QrContent
��E N
)
��N O
;
��O P
comprobante
�� #
.
��# $'
CadenaHtmlDeComprobante80
��$ =
=
��> ?#
CoreHtmlStringBuilder
��@ U
.
��U V
ObtenerHtmlString
��V g
(
��g h
ordenDeVenta
��h t
,
��t u
FormatoImpresion��v �
.��� �
_80mm��� �
,��� �
QrBytes��� �
,��� �
sede��� �
,��� �
this��� �
,��� �

)��� �
;��� �
comprobante
�� #
.
��# $'
CadenaHtmlDeComprobanteA4
��$ =
=
��> ?#
CoreHtmlStringBuilder
��@ U
.
��U V
ObtenerHtmlString
��V g
(
��g h
ordenDeVenta
��h t
,
��t u
FormatoImpresion��v �
.��� �
A4��� �
,��� �
QrBytes��� �
,��� �
sede��� �
,��� �
this��� �
,��� �

)��� �
;��� �
}
�� 
}
�� 
return
�� 
Json
�� 
(
�� 
atencion
�� $
)
��$ %
;
��% &
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� *
ObtenerAtencionDesdeAtencion
�� 6
(
��6 7
long
��7 ;

idAtencion
��< F
)
��F G
{
�� 	
try
�� 
{
�� 
Atencion
�� 
atencion
�� !
=
��" #
hotelLogica
��$ /
.
��/ 0*
ObtenerAtencionDesdeAtencion
��0 L
(
��L M

idAtencion
��M W
)
��W X
;
��X Y
if
�� 
(
�� 
atencion
�� 
.
�� 
TieneFacturacion
�� -
)
��- .
{
�� 
var
�� 
sede
�� 
=
�� 
ObtenerSede
�� *
(
��* +
)
��+ ,
;
��, -
foreach
�� 
(
�� 
var
��  
comprobante
��! ,
in
��- /
atencion
��0 8
.
��8 9$
ComprobantesFacturados
��9 O
)
��O P
{
�� 
OrdenDeVenta
�� $
ordenDeVenta
��% 1
=
��2 3
operacionLogica
��4 C
.
��C D!
ObtenerOrdenDeVenta
��D W
(
��W X
comprobante
��X c
.
��c d
IdOrden
��d k
)
��k l
;
��l m
string
�� 
	QrContent
�� (
=
��) **
facturacionElectronicaLogica
��+ G
.
��G H
	ObtenerQR
��H Q
(
��Q R
ordenDeVenta
��R ^
,
��^ _
sede
��` d
)
��d e
;
��e f
byte
�� 
[
�� 
]
�� 
QrBytes
�� &
=
��' (
barCodeUtil
��) 4
.
��4 5
ObtenerCodigoQR
��5 D
(
��D E
	QrContent
��E N
)
��N O
;
��O P
comprobante
�� #
.
��# $'
CadenaHtmlDeComprobante80
��$ =
=
��> ?#
CoreHtmlStringBuilder
��@ U
.
��U V
ObtenerHtmlString
��V g
(
��g h
ordenDeVenta
��h t
,
��t u
FormatoImpresion��v �
.��� �
_80mm��� �
,��� �
QrBytes��� �
,��� �
sede��� �
,��� �
this��� �
,��� �

)��� �
;��� �
comprobante
�� #
.
��# $'
CadenaHtmlDeComprobanteA4
��$ =
=
��> ?#
CoreHtmlStringBuilder
��@ U
.
��U V
ObtenerHtmlString
��V g
(
��g h
ordenDeVenta
��h t
,
��t u
FormatoImpresion��v �
.��� �
A4��� �
,��� �
QrBytes��� �
,��� �
sede��� �
,��� �
this��� �
,��� �

)��� �
;��� �
}
�� 
}
�� 
return
�� 
Json
�� 
(
�� 
atencion
�� $
)
��$ %
;
��% &
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� 
GuardarAnotacion
�� *
(
��* +

��+ 8
atencion
��9 A
)
��A B
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8
GuardarAnotacion
��8 H
(
��H I
atencion
��I Q
)
��Q R
;
��R S
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
,
��m n
	resultado
��o x
.
��x y
information��y �
}��� �
)��� �
;��� �
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� ,
EditarResponsableAtencionMacro
�� 8
(
��8 9
long
��9 =
idAtencionMacro
��> M
,
��M N
int
��O R

��S `
)
��` a
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8,
EditarResponsableAtencionMacro
��8 V
(
��V W
idAtencionMacro
��W f
,
��f g

��h u
)
��u v
;
��v w
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� !
EditarFechaAtencion
�� -
(
��- .

��. ;
atencion
��< D
)
��D E
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8!
EditarFechaAtencion
��8 K
(
��K L
atencion
��L T
)
��T U
;
��U V
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� '
CambiarHabitacionAtencion
�� 3
(
��3 4

��4 A&
atencionCambioHabitacion
��B Z
)
��Z [
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8'
CambiarHabitacionAtencion
��8 Q
(
��Q R&
atencionCambioHabitacion
��R j
,
��j k
ProfileData
��l w
(
��w x
)
��x y
)
��y z
;
��z {
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
}
��n o
)
��o p
;
��p q
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	
public
�� 

JsonResult
�� $
ConfirmarAtencionMacro
�� 0
(
��0 1
long
��1 5
idAtencionMacro
��6 E
,
��E F
string
��G M
observacion
��N Y
)
��Y Z
{
�� 	
try
�� 
{
�� 
OperationResult
�� 
	resultado
��  )
=
��* +
hotelLogica
��, 7
.
��7 8$
ConfirmarAtencionMacro
��8 N
(
��N O
idAtencionMacro
��O ^
,
��^ _
observacion
��` k
,
��k l
ProfileData
��m x
(
��x y
)
��y z
)
��z {
;
��{ |
Util
�� 
.
�� 
VerificarError
�� #
(
��# $
	resultado
��$ -
)
��- .
;
��. /
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
	resultado
��" +
.
��+ ,
code_result
��, 7
,
��7 8
	resultado
��9 B
.
��B C
data
��C G
,
��G H 
result_description
��I [
=
��\ ]
	resultado
��^ g
.
��g h
title
��h m
,
��m n
	resultado
��o x
.
��x y
information��y �
}��� �
)��� �
;��� �
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
�� "
JsonHttpStatusResult
�� /
(
��/ 0
Util
��0 4
.
��4 5
	ErrorJson
��5 >
(
��> ?
e
��? @
)
��@ A
,
��A B
HttpStatusCode
��C Q
.
��Q R!
InternalServerError
��R e
)
��e f
;
��f g
}
�� 
}
�� 	

