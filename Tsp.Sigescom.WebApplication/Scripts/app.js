var app = angular.module('app', ['datetime', 'datatables', 'datatables.bootstrap', 'ngSweetAlert', /*'angucomplete',*/ 'flow', 'datatables.colreorder', 'datatables.buttons', 'ngSanitize',/* 'ngAnimate',*/ 'blockUI', 'ngResource', 'ui.bootstrap', 'datatables.columnfilter', 'datatables.light-columnfilter', 'datatables.fixedcolumns', 'ui.select', 'ngPatternRestrict', 'ngFileUpload']
);
/**
 * AngularJS default filter with the following expression:
 * "person in people | filter: {name: $select.search, age: $select.search}"
    * performs a AND between 'name: $select.search' and 'age: $select.search'.
 * We want to perform a OR.
 */
app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});


app.config(function (blockUIConfig) {
    blockUIConfig.message = 'Cargando...';
});


app.run(function ($rootScope, $sce, $timeout, DTOptionsBuilder, DTDefaultOptions) {

    $rootScope.patron = {
        validacionNumero: '/^\d{0,9}(\.\d{1,9})?$/',
    }

    $rootScope.formatNumber = function (number, decimales) {
        return isNaN(Number(number).toFixed(decimales)) ? Number(0).toFixed(decimales) : Number(number).toFixed(decimales);
    }




    $rootScope.export = function (tabla) {
        var styles = $("#" + tabla).attr('style');
        $('#' + tabla).attr('style', 'display:block');
        $('#' + tabla).tableExport({ type: 'excel', escape: 'false' });
        $('#' + tabla).attr('style', styles);
    }
    $rootScope.addDays = function (theDate, days) {
        return new Date(theDate.getTime() + days * 24 * 60 * 60 * 1000);
    }




    $rootScope.formatDate = function (date, language) {
        var date = date ? new Date(date) : new Date();
        var response = '';
        var dd = date.getDate();
        var mm = (date.getMonth() + 1);
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }

        switch (language) {
            case 'ES':
                response = dd + "/" + mm + "/" + date.getFullYear();
                break;
            case 'EN':
                response = date.getFullYear() + "-" + mm + "-" + dd;
                break;
        }

        return response;
    }
    $rootScope.formatoFecha = function (date) {

        date = date.split("/")
        if (date != null) {
            fecha = new Date(date[2], date[1], date[0]);
            fecha = fecha.valueOf();
            console.debug("entro");
        } else {
            fecha = new Date();
        }
        console.debug(fecha);
        return fecha;
    }
    $rootScope.messageSuccess = function (message) {
        $.notify({ title: '<strong>Mensaje:</strong>', message: message }, { type: 'success' });
    }
    $rootScope.messageError = function (message) {
        $.notify({ title: '<strong>Mensaje:</strong>', message: message }, { type: 'danger' });
    }
    $rootScope.messageWarning = function (message) {
        $.notify({ title: '<strong>Mensaje:</strong>', message: message }, { type: 'warning' });
    }

    $rootScope.getNumberWeek = function ($fecha) {
        /* A esta funcion se le pasa el parametro en formato fecha*/
        /* dd/mm/yyyy*/
        if ($fecha.match(/\//)) {
            $fecha = $fecha.replace(/\//g, "-", $fecha); /*Permite que se puedan ingresar formatos de fecha ustilizando el "/" o "-" como separador*/
        };

        $fecha = $fecha.split("-"); /*Dividimos el string de fecha en trozos (dia,mes,año)*/
        $dia = eval($fecha[0]);
        $mes = eval($fecha[1]);
        $ano = eval($fecha[2]);

        if ($mes == 1 || $mes == 2) {
            /*Cálculos si el mes es Enero o Febrero*/
            $a = $ano - 1;
            $b = Math.floor($a / 4) - Math.floor($a / 100) + Math.floor($a / 400);
            $c = Math.floor(($a - 1) / 4) - Math.floor(($a - 1) / 100) + Math.floor(($a - 1) / 400);
            $s = $b - $c;
            $e = 0;
            $f = $dia - 1 + (31 * ($mes - 1));
        } else {
            /*Calculos para los meses entre marzo y Diciembre*/
            $a = $ano;
            $b = Math.floor($a / 4) - Math.floor($a / 100) + Math.floor($a / 400);
            $c = Math.floor(($a - 1) / 4) - Math.floor(($a - 1) / 100) + Math.floor(($a - 1) / 400);
            $s = $b - $c;
            $e = $s + 1;
            $f = $dia + Math.floor(((153 * ($mes - 3)) + 2) / 5) + 58 + $s;
        };

        /*Adicionalmente sumándole 1 a la variable $f se obtiene numero ordinal del dia de la fecha ingresada con referencia al año actual.*/

        /*Estos cálculos se aplican a cualquier mes*/
        $g = ($a + $b) % 7;
        $d = ($f + $g - $e) % 7; /*Adicionalmente esta variable nos indica el dia de la semana 0=Lunes, ... , 6=Domingo.*/
        $n = $f + 3 - $d;

        if ($n < 0) {
            /*Si la variable n es menor a 0 se trata de una semana perteneciente al año anterior*/
            $semana = 53 - Math.floor(($g - $s) / 5);
            $ano = $ano - 1;
        } else if ($n > (364 + $s)) {
            /*Si n es mayor a 364 + $s entonces la fecha corresponde a la primera semana del año siguiente.*/
            $semana = 1;
            $ano = $ano + 1;
        } else {
            /*En cualquier otro caso es una semana del año actual.*/
            $semana = Math.floor($n / 7) + 1;
        };

        /* return $semana + "-" + $ano; /*La función retorna una cadena de texto indicando la semana y el año correspondiente a la fecha ingresada*/
        return ("0" + $semana).slice(-2);
    }
    $rootScope.dtOptions = DTOptionsBuilder.newOptions().withOption('aoColumnDefs', [{
        "bSearchable": false, "aTargets": ["not-search"]
    }]).withLanguage({
        "sEmptyTable": "No hay Datos Disponibles",
        "sInfo": "Mostrando _START_ hasta _END_ de _TOTAL_ Filas",
        "sInfoEmpty": "Viendo 0 hasta 0 de 0 filas",
        "sInfoFiltered": "(filtrado de _MAX_ Filas)",
        "sInfoPostFix": "",
        "sInfoThousands": ",",
        "sLengthMenu": "Ver _MENU_ Filas",
        "sLoadingRecords": "Cargando...",
        "sProcessing": "Procesando...",
        "sSearch": "Buscar:",
        "sZeroRecords": "No se encontraron registros",
        "oPaginate": {
            "sFirst": "Primero",
            "sLast": "Ultimo",
            "sNext": "Siguiente",
            "sPrevious": "Anterior"
        },
        "oAria": {
            "sSortAscending": ": activado para ordenar columna ascendente",
            "sSortDescending": ": activado para ordenar columna descendente"
        }
    }).withBootstrap();/*.withColReorder();/*.withButtons([
            {
                extend: 'excelFlash',
                 title: 'Descargar Excel',
                text: '<span class="glyphicon glyphicon-download-alt"> Descargar</span>',
                className: 'btn btn-success btn-xs',
                exportOptions: {
                    columns: "thead th:not(.noExport)"
                }
            }
    ]);*/
});



app.directive('evictForm', function () {
    return {
        restrict: 'A',
        link: {
            pre: function (scope, iElement) {
                iElement.data('$formController', null);
            }
        }
    };
})

app.directive('exportExcel', function () {
    return {
        replace: true,
        scope: {
            "tabla": "@tabla"
        },
        restrict: 'E',
        template: '<a title="Descargar excel" class="dt-button buttons-excel buttons-flash btn btn-sm btn-success" tabindex="0" aria-controls="{{tabla}}" href="#"><span class="glyphicon glyphicon-download-alt"></span> Descargar</a>',
        link: function ($scope, elem, attrs) {
            $scope.export = function () {
                var styles = $("#" + $scope.tabla).attr('style');

                $('#' + $scope.tabla).attr('style', 'display:block');
                $('#' + $scope.tabla).tableExport({ type: 'excel', escape: 'false' });
                $('#' + $scope.tabla).attr('style', styles);
            }
        }
    };
});
app.directive('number', function ($browser, $filter) {
    return {
        require: 'ngModel',
        link: function ($scope, $element, $attrs, ngModelCtrl) {
            $element.bind('blur', function (event) {
                var value = $element.val().replace(/,/g, "");
                value = $filter('number')(value, '2');
                $element.val(value);
            });

        }

    };
});

app.directive('sgNumberInput', sgNumberInput);


app.directive('richText', function () {
    return {
        scope: false,
        replace: true,
        templateUrl: "/Home/RichText",
        link: function ($scope, elem, attrs) {
            $scope.texto = " ";
            $scope.titulo = JSON.parse(attrs.title);
            $scope.tipo = JSON.parse(attrs.type);

            $scope.asingarTexto($scope.tipo, $scope.texto);
        }
    };
});
app.factory("confirm", function ($window, $q, $timeout) {

    function confirm(message) {
        var defer = $q.defer();

        $timeout(function () {
            if ($window.confirm(message)) {
                defer.resolve(true);
            }
            else {
                defer.reject(false);
            }
        }, 0, false);

        return defer.promise;
    }
    return confirm;
});
app.directive('ngEsc', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress keyup", function (event) {
            if (event.which === 27) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEsc);
                });
                event.preventDefault();
            }
        });
    };
});

app.directive('uppercaseOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            var capitalize = function (inputValue) {
                if (inputValue == undefined) inputValue = '';
                var capitalized = inputValue.toUpperCase();
                if (capitalized !== inputValue) {
                    // see where the cursor is before the update so that we can set it back
                    var selection = element[0].selectionStart;
                    modelCtrl.$setViewValue(capitalized);
                    modelCtrl.$render();
                    // set back the cursor after rendering
                    element[0].selectionStart = selection;
                    element[0].selectionEnd = selection;
                }
                return capitalized;
            }
            modelCtrl.$parsers.push(capitalize);
            capitalize(scope[attrs.ngModel]); // capitalize initial value
        }
    };
});
//Directiva de validación personalizada en AngularJS
app.directive('validador1', ['$parse', function ($parse) {
    return {
        require: 'ngModel',
        link: function (scope, ele, attrs, ngModelController) {
            scope.$watch(attrs.ngModel, function (value) {
                var expressionResults = $parse(attrs.validador1)(scope);
                for (var expressionName in expressionResults) {
                    ngModelController.$setValidity(expressionName, expressionResults[expressionName]);
                }
            });
        }
    };
}]);
app.directive('validador2', ['$parse', function ($parse) {
    return {
        require: 'ngModel',
        link: function (scope, ele, attrs, ngModelController) {
            scope.$watch(attrs.ngModel, function (value) {
                var expressionResults = $parse(attrs.validador2)(scope);
                for (var expressionName in expressionResults) {
                    ngModelController.$setValidity(expressionName, expressionResults[expressionName]);
                }
            });
        }
    };
}]);
app.directive('validador3', ['$parse', function ($parse) {
    return {
        require: 'ngModel',
        link: function (scope, ele, attrs, ngModelController) {
            scope.$watch(attrs.ngModel, function (value) {
                var expressionResults = $parse(attrs.validador3)(scope);
                for (var expressionName in expressionResults) {
                    ngModelController.$setValidity(expressionName, expressionResults[expressionName]);
                }
            });
        }
    };
}]);

app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });
                event.preventDefault();
            }
        });
    };
})
app.directive('ctrlEnter', function () {
    return function (scope, element) {
        var ctrlDown = false;
        element.bind("keydown", function (event) {

            // sets ctrl to true if the shift key (key code is 17) is pressed
            if (event.which === 17) {
                ctrlDown = true;
            }

            // sends the message if "enter" is pressed and "ctrl" is being held down
            if (event.which === 13 && ctrlDown) {
                event.preventDefault();
                if (scope.guardadoActivado == false) {
                    scope.guardar();
                    scope.guardadoActivado = true;
                }
                scope.$apply();
                ctrlDown = false;
            }
        });

        // sets ctrl to false if the shift key has been released
        element.bind("keyup", function (event) {
            if (event.which === 17) {
                ctrlDown = false;
            }
        });
    };
});

app.directive('altI', function () {
    return function (scope, element) {
        var altDown = false;
        element.bind("keydown", function (event) {

            // sets ctrl to true if the shift key (key code is 17) is pressed
            if (event.which === 18) {
                altDown = true;
            }

            // sends the message if "enter" is pressed and "ctrl" is being held down
            if (event.which === 73 && altDown) {
                event.preventDefault();
                scope.AltI();
                scope.$apply();
                altDown = false;
            }
        });

        // sets ctrl to false if the shift key has been released
        element.bind("keyup", function (event) {
            if (event.which === 18) {
                altDown = false;
            }
        });
    };
});

app.directive('autoFocus', function ($timeout) {
    return {
        restrict: 'AC',
        link: function (_scope, _element) {
            $timeout(function () {
                _element[0].focus();
            }, 0);
        }
    };
});
app.directive('focusMe', function ($timeout, $parse) {
    return {
        link: function (scope, element, attrs) {
            var model = $parse(attrs.focusMe);
            scope.$watch(model, function (value) {
                console.log('value=', value);
                if (value === true) {
                    console.log(element[0]);
                    $timeout(function () {
                        element[0].focus();
                    });
                }
            });
            element.bind('blur', function () {
                console.log('blur')
                scope.$apply(model.assign(scope, false));
            })
        }
    };
});

app.directive('angucomplete', function ($parse, $http, $sce, $timeout) {
    return {
        restrict: 'EA',
        scope: {
            "id": "@id",
            "placeholder": "@placeholder",
            "selectedObject": "=selectedobject",
            "accion": "&accion",
            "modal": "@modal",
            "url": "@url",
            "dataField": "@datafield",
            "titleField": "@titlefield",
            "descriptionField": "@descriptionfield",
            "imageField": "@imagefield",
            "imageUri": "@imageuri",
            "inputClass": "@inputclass",
            "userPause": "@pause",
            "localData": "=localdata",
            "searchFields": "@searchfields",
            "minLengthUser": "@minlength",
            "matchClass": "@matchclass"
        },
        template: '<div class="angucomplete-holder row">' +
            '<div class="col-md-10"><input id="{{id}}_value" ng-model="searchStr" type="text" placeholder="{{placeholder}}" class="{{inputClass}}" onmouseup="this.select();" ng-focus="resetHideResults()" ng-blur="hideResults()" /></div>' +
            '<a ng-show="addElement" data-toggle="modal" data-target="#{{modal}}" class="btn btn-success btn-sm" ng-click="agregar()"><span class="glyphicon glyphicon-plus"></span></a>' +
            '<div id="{{id}}_dropdown" class="angucomplete-dropdown" ng-if="showDropdown">' +
            '<div class="angucomplete-searching" ng-show="searching">Buscando...</div>' +
            /*'<div class="angucomplete-searching" ng-show="!searching && (!results || results.length == 0)">No se han encontrado resultados</div>' +*/
            '<div class="angucomplete-row" ng-repeat="result in results" ng-mousedown="selectResult(result)" ng-mouseover="hoverRow()" ng-class="{\'angucomplete-selected-row\': $index == currentIndex}">' +
            '<div ng-if="imageField" class="angucomplete-image-holder">' +
            '<img ng-if="result.image && result.image != \'\'" ng-src="{{result.image}}" class="angucomplete-image"/>' +
            '<div ng-if="!result.image && result.image != \'\'" class="angucomplete-image-default"></div>' +
            '</div>' +
            '<div class="angucomplete-title" ng-if="matchClass" ng-bind-html="result.title"></div>' +
            '<div class="angucomplete-title" ng-if="!matchClass">{{ result.title }}</div>' +
            '<div ng-if="result.description && result.description != \'\'" class="angucomplete-description">{{result.description}}</div>' +
            '</div>' +
            '</div>' +
            '</div>',

        link: function ($scope, elem, attrs) {
            $scope.lastSearchTerm = null;
            $scope.currentIndex = null;
            $scope.justChanged = false;
            $scope.searchTimer = null;
            $scope.hideTimer = null;
            $scope.searching = false;
            $scope.addElement = false;
            $scope.pause = 500;
            $scope.minLength = 3;
            $scope.searchStr = null;

            if ($scope.minLengthUser && $scope.minLengthUser != "") {
                $scope.minLength = $scope.minLengthUser;
            }

            if ($scope.userPause) {
                $scope.pause = $scope.userPause;
            }

            isNewSearchNeeded = function (newTerm, oldTerm) {
                return newTerm.length >= $scope.minLength && newTerm != oldTerm
            }

            $scope.processResults = function (responseData, str) {
                if (responseData && responseData.length > 0) {
                    $scope.results = [];
                    $scope.addElement = false;

                    var titleFields = [];
                    if ($scope.titleField && $scope.titleField != "") {
                        titleFields = $scope.titleField.split(",");
                    }

                    for (var i = 0; i < responseData.length; i++) {
                        // Get title variables
                        var titleCode = [];

                        for (var t = 0; t < titleFields.length; t++) {
                            titleCode.push(responseData[i][titleFields[t]]);
                        }

                        var description = "";
                        if ($scope.descriptionField) {
                            description = responseData[i][$scope.descriptionField];
                        }

                        var imageUri = "";
                        if ($scope.imageUri) {
                            imageUri = $scope.imageUri;
                        }

                        var image = "";
                        if ($scope.imageField) {
                            image = imageUri + responseData[i][$scope.imageField];
                        }

                        var text = titleCode.join(' ');
                        if ($scope.matchClass) {
                            var re = new RegExp(str, 'i');
                            var strPart = text.match(re)[0];
                            text = $sce.trustAsHtml(text.replace(re, '<span class="' + $scope.matchClass + '">' + strPart + '</span>'));
                        }

                        var resultRow = {
                            title: text,
                            description: description,
                            image: image,
                            originalObject: responseData[i]
                        }

                        $scope.results[$scope.results.length] = resultRow;
                    }


                } else {
                    $scope.results = [];
                    $scope.showDropdown = false;
                    $scope.addElement = true;

                }
            }

            $scope.searchTimerComplete = function (str) {
                // Begin the search

                if (str.length >= $scope.minLength) {
                    if ($scope.localData) {
                        var searchFields = $scope.searchFields.split(",");

                        var matches = [];

                        for (var i = 0; i < $scope.localData.length; i++) {
                            var match = false;

                            for (var s = 0; s < searchFields.length; s++) {
                                match = match || (typeof $scope.localData[i][searchFields[s]] === 'string' && typeof str === 'string' && $scope.localData[i][searchFields[s]].toLowerCase().indexOf(str.toLowerCase()) >= 0);
                            }

                            if (match) {
                                matches[matches.length] = $scope.localData[i];
                            }
                        }

                        $scope.searching = false;
                        $scope.processResults(matches, str);

                    } else {
                        $http.get($scope.url + str, {}).
                            success(function (responseData, status, headers, config) {
                                $scope.searching = false;
                                $scope.processResults((($scope.dataField) ? responseData[$scope.dataField] : responseData), str);
                            }).
                            error(function (data, status, headers, config) {
                                console.log("error");
                            });
                    }
                }
            }

            $scope.hideResults = function () {
                $scope.hideTimer = $timeout(function () {
                    $scope.showDropdown = false;
                }, $scope.pause);
            };

            $scope.resetHideResults = function () {
                if ($scope.hideTimer) {
                    $timeout.cancel($scope.hideTimer);
                };
            };

            $scope.hoverRow = function (index) {
                $scope.currentIndex = index;
            }

            $scope.keyPressed = function (event) {
                if (!(event.which == 38 || event.which == 40 || event.which == 13)) {
                    if (!$scope.searchStr || $scope.searchStr == "") {
                        $scope.showDropdown = false;
                        $scope.lastSearchTerm = null
                    } else if (isNewSearchNeeded($scope.searchStr, $scope.lastSearchTerm)) {
                        $scope.lastSearchTerm = $scope.searchStr
                        $scope.showDropdown = true;
                        $scope.currentIndex = 0;
                        $scope.results = [];

                        if ($scope.searchTimer) {
                            $timeout.cancel($scope.searchTimer);
                        }

                        $scope.searching = true;

                        $scope.searchTimer = $timeout(function () {
                            $scope.searchTimerComplete($scope.searchStr);
                        }, $scope.pause);
                    }
                } else {
                    event.preventDefault();
                }
            }

            $scope.selectResult = function (result) {
                if ($scope.matchClass) {
                    result.title = result.title.toString().replace(/(<([^>]+)>)/ig, '');
                }
                $scope.searchStr = $scope.lastSearchTerm = result.title;
                $scope.selectedObject = result.originalObject;
                $scope.showDropdown = false;
                $scope.results = [];
                //$scope.$apply();
            }

            var inputField = elem.find('input');

            inputField.on('keyup', $scope.keyPressed);

            elem.on("keyup", function (event) {
                if (event.which === 40) {
                    if ($scope.results && ($scope.currentIndex + 1) < $scope.results.length) {
                        $scope.currentIndex++;
                        $scope.$apply();
                        event.preventDefault;
                        event.stopPropagation();
                    }

                    $scope.$apply();
                } else if (event.which == 38) {
                    if ($scope.currentIndex >= 1) {
                        $scope.currentIndex--;
                        $scope.$apply();
                        event.preventDefault;
                        event.stopPropagation();
                    }

                } else if (event.which == 13) {
                    if ($scope.results && $scope.currentIndex >= 0 && $scope.currentIndex < $scope.results.length) {
                        $scope.selectResult($scope.results[$scope.currentIndex]);
                        $scope.$apply();
                        event.preventDefault;
                        event.stopPropagation();
                    } else {
                        $scope.results = [];
                        $scope.showDropdown = false;
                        $scope.$apply();
                        event.preventDefault;
                        event.stopPropagation();
                    }

                } else if (event.which == 27) {
                    $scope.results = [];
                    $scope.showDropdown = false;
                    $scope.selectedObject = null;
                    $scope.$apply();
                } else if (event.which == 8) {
                    $scope.selectedObject = null;
                    $scope.$apply();
                } else if (event.which == 9) {
                    $scope.results = [];
                    $scope.$apply();
                }
            });
            $scope.agregar = function () {
                $scope.accion({ nombre: $scope.searchStr });
            }
        }
    };
});

app.directive('checklistModel', ['$parse', '$compile', function ($parse, $compile) {
    // contains
    function contains(arr, item, comparator) {
        if (angular.isArray(arr)) {
            for (var i = arr.length; i--;) {
                if (comparator(arr[i], item)) {
                    return true;
                }
            }
        }
        return false;
    }

    // add
    function add(arr, item, comparator) {
        arr = angular.isArray(arr) ? arr : [];
        if (!contains(arr, item, comparator)) {
            arr.push(item);
        }
        return arr;
    }

    // remove
    function remove(arr, item, comparator) {
        if (angular.isArray(arr)) {
            for (var i = arr.length; i--;) {
                if (comparator(arr[i], item)) {
                    arr.splice(i, 1);
                    break;
                }
            }
        }
        return arr;
    }

    // http://stackoverflow.com/a/19228302/1458162
    function postLinkFn(scope, elem, attrs) {
        // exclude recursion, but still keep the model
        var checklistModel = attrs.checklistModel;
        attrs.$set("checklistModel", null);
        // compile with `ng-model` pointing to `checked`
        $compile(elem)(scope);
        attrs.$set("checklistModel", checklistModel);

        // getter for original model
        var checklistModelGetter = $parse(checklistModel);
        var checklistChange = $parse(attrs.checklistChange);
        var checklistBeforeChange = $parse(attrs.checklistBeforeChange);
        var ngModelGetter = $parse(attrs.ngModel);



        var comparator = function (a, b) {
            if (!isNaN(a) && !isNaN(b)) {
                return String(a) === String(b);
            } else {
                return angular.equals(a, b);
            }
        };

        if (attrs.hasOwnProperty('checklistComparator')) {
            if (attrs.checklistComparator[0] == '.') {
                var comparatorExpression = attrs.checklistComparator.substring(1);
                comparator = function (a, b) {
                    return a[comparatorExpression] === b[comparatorExpression];
                };

            } else {
                comparator = $parse(attrs.checklistComparator)(scope.$parent);
            }
        }

        // watch UI checked change
        var unbindModel = scope.$watch(attrs.ngModel, function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            if (checklistBeforeChange && (checklistBeforeChange(scope) === false)) {
                ngModelGetter.assign(scope, contains(checklistModelGetter(scope.$parent), getChecklistValue(), comparator));
                return;
            }

            setValueInChecklistModel(getChecklistValue(), newValue);

            if (checklistChange) {
                checklistChange(scope);
            }
        });

        // watches for value change of checklistValue
        var unbindCheckListValue = scope.$watch(getChecklistValue, function (newValue, oldValue) {
            if (newValue != oldValue && angular.isDefined(oldValue) && scope[attrs.ngModel] === true) {
                var current = checklistModelGetter(scope.$parent);
                checklistModelGetter.assign(scope.$parent, remove(current, oldValue, comparator));
                checklistModelGetter.assign(scope.$parent, add(current, newValue, comparator));
            }
        }, true);

        var unbindDestroy = scope.$on('$destroy', destroy);

        function destroy() {
            unbindModel();
            unbindCheckListValue();
            unbindDestroy();
        }

        function getChecklistValue() {
            return attrs.checklistValue ? $parse(attrs.checklistValue)(scope.$parent) : attrs.value;
        }

        function setValueInChecklistModel(value, checked) {
            var current = checklistModelGetter(scope.$parent);
            if (angular.isFunction(checklistModelGetter.assign)) {
                if (checked === true) {
                    checklistModelGetter.assign(scope.$parent, add(current, value, comparator));
                } else {
                    checklistModelGetter.assign(scope.$parent, remove(current, value, comparator));
                }
            }

        }

        // declare one function to be used for both $watch functions
        function setChecked(newArr, oldArr) {
            if (checklistBeforeChange && (checklistBeforeChange(scope) === false)) {
                setValueInChecklistModel(getChecklistValue(), ngModelGetter(scope));
                return;
            }
            ngModelGetter.assign(scope, contains(newArr, getChecklistValue(), comparator));
        }

        // watch original model change
        // use the faster $watchCollection method if it's available
        if (angular.isFunction(scope.$parent.$watchCollection)) {
            scope.$parent.$watchCollection(checklistModel, setChecked);
        } else {
            scope.$parent.$watch(checklistModel, setChecked, true);
        }
    }

    return {
        restrict: 'A',
        priority: 1000,
        terminal: true,
        scope: true,
        compile: function (tElement, tAttrs) {

            if (!tAttrs.checklistValue && !tAttrs.value) {
                throw 'You should provide `value` or `checklist-value`.';
            }

            // by default ngModel is 'checked', so we set it if not specified
            if (!tAttrs.ngModel) {
                // local scope var storing individual checkbox model
                tAttrs.$set("ngModel", "checked");
            }
            return postLinkFn;
        }
    };
}]);

//para dar formato a fechas que vienen como tipo Date desde MVC
app.filter('mvcDate', ['$filter', $filter =>
    (date, format, timezone) =>
        date && $filter('date')(date, format, timezone)
]);

app.filter("unique", function () {
    // we will return a function which will take in a collection
    // and a keyname
    return function (collection, keyname) {
        // we define our output and keys array;
        var output = [],
            keys = [];

        // we utilize angular's foreach function
        // this takes in our original collection and an iterator function
        angular.forEach(collection, function (item) {
            // we check to see whether our object exists
            var key = item[keyname];
            // if it's not already part of our keys array
            if (keys.indexOf(key) === -1) {
                // add it to our keys array
                keys.push(key);
                // push this item to our final output array
                output.push(item);
            }
        });
        // return our array which should be devoid of
        // any duplicates
        return output;
    };
});

function sgNumberInput($filter, $locale) {
    //#region helper methods
    function getCaretPosition(inputField) {
        // Initialize
        var position = 0;
        // IE Support
        if (document.selection) {
            inputField.focus();
            // To get cursor position, get empty selection range
            var emptySelection = document.selection.createRange();
            // Move selection start to 0 position
            emptySelection.moveStart('character', -inputField.value.length);
            // The caret position is selection length
            position = emptySelection.text.length;
        }
        else if (inputField.selectionStart || inputField.selectionStart === 0) {
            position = inputField.selectionStart;
        }
        return position;
    }
    function setCaretPosition(inputElement, position) {
        if (inputElement.createTextRange) {
            var range = inputElement.createTextRange();
            range.move('character', position);
            range.select();
        }
        else {
            if (inputElement.selectionStart) {
                inputElement.focus();
                inputElement.setSelectionRange(position, position);
            }
            else {
                inputElement.focus();
            }
        }
    }
    function countNonNumericChars(value) {
        return (value.match(/[^a-z0-9]/gi) || []).length;
    }
    //#endregion helper methods



    return {
        require: "ngModel",
        restrict: "A",
        link: function ($scope, element, attrs, ctrl) {
            var fractionSize = parseInt(attrs['fractionSize']) || 0;
            var numberFilter = $filter('number');
            //format the view value
            ctrl.$formatters.push(function (modelValue) {
                var retVal = numberFilter(modelValue, fractionSize);
                var isValid = !isNaN(modelValue);
                ctrl.$setValidity(attrs.name, isValid);
                return retVal;
            });
            //parse user's input
            ctrl.$parsers.push(function (viewValue) {
                var caretPosition = getCaretPosition(element[0]), nonNumericCount = countNonNumericChars(viewValue);
                viewValue = viewValue || '';
                //Replace all possible group separators
                var trimmedValue = viewValue.trim().replace(/,/g, '').replace(/`/g, '').replace(/'/g, '').replace(/\u00a0/g, '').replace(/ /g, '');
                //If numericValue contains more decimal places than is allowed by fractionSize, then numberFilter would round the value up
                //Thus 123.109 would become 123.11
                //We do not want that, therefore I strip the extra decimal numbers
                var separator = $locale.NUMBER_FORMATS.DECIMAL_SEP;
                var arr = trimmedValue.split(separator);
                var decimalPlaces = arr[1];
                if (decimalPlaces != null && decimalPlaces.length > fractionSize) {
                    //Trim extra decimal places
                    decimalPlaces = decimalPlaces.substring(0, fractionSize);
                    trimmedValue = arr[0] + separator + decimalPlaces;
                }
                var numericValue = parseFloat(trimmedValue);
                var isEmpty = numericValue == null || viewValue.trim() === "";
                var isRequired = attrs.required || false;
                var isValid = true;
                if (isEmpty && isRequired || !isEmpty && isNaN(numericValue)) {
                    isValid = false;
                }
                ctrl.$setValidity(attrs.name, isValid);
                if (!isNaN(numericValue) && isValid) {
                    var newViewValue = numberFilter(numericValue, fractionSize);
                    element.val(newViewValue);
                    var newNonNumbericCount = countNonNumericChars(newViewValue);
                    var diff = newNonNumbericCount - nonNumericCount;
                    var newCaretPosition = caretPosition + diff;
                    if (nonNumericCount === 0 && newCaretPosition > 0) newCaretPosition--;
                    setCaretPosition(element[0], newCaretPosition);
                }
                return !isNaN(numericValue) ? numericValue : null;
            });
        } //end of link function
    };
}

sgNumberInput.$inject = ["$filter", "$locale"];

const _MS_PER_DAY = 1000 * 60 * 60 * 24;

// a and b are javascript Date objects
function dateDiffInDays(a, b) {
    // Discard the time and time-zone information.
    const utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
    const utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());

    return Math.floor((utc2 - utc1) / _MS_PER_DAY);
}

function validarFechas(fechaHoraInicio, fechaHoraFin, maximoDiasDiferencia) {
    var resultado = { error: false, textoError: '' };
    if (fechaHoraFin.getTime() <= fechaHoraInicio.getTime()) {
        error = true;
        textoError = 'La fecha final debe ser menor a la fecha inicial';
    }
    else if ((dateDiffInDays(new Date(fechaHoraInicio), new Date(fechaHoraFin)) > maximoDiasDiferencia)) {
        error = true;
        textoError = '\nComo máximo se permite' + maximoDiasDiferencia + ' días.';
    }
    return resultado;

}


/**
 * Provides with pagination with infinite scroll to handle large list of choices. 
 * An upfront large list of choices makes the control unstable and unresponsive.
 * This feature avoid populaing the list upfront by pagination which is the primary cause of unstability.
 * Pagination works in 2 scenarios:-
 * 
 * 1) Simple scrolling of the contents.
 * 2) Scrolling when the Autocomplete/search text is enteded and the results are still too large.
 * 
 * @example
    <ui-select-choices position="up" all-choices="ctrl.allTenThousandItems"  refresh-delay="0"
        repeat="person in $select.pageOptions.people | propsFilter: {name: $select.search, age: $select.search} ">
      <div ng-bind-html="person.name | highlight: $select.search"></div>
      <small>
        email: {{person.email}}
        age: <span ng-bind-html="''+person.age | highlight: $select.search"></span>
      </small>
    </ui-select-choices>
 * 
 * 

/**
 * AngularJS default filter with the following expression:
 * "person in people | filter: {name: $select.search, age: $select.search}"
 * performs an AND between 'name: $select.search' and 'age: $select.search'.
 * We want to perform an OR.
 */
app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            var keys = Object.keys(props);

            items.forEach(function (item) {
                if (!item) return;
                var itemMatches = false;

                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});

app.directive("afterRender", [
    "$timeout",
    function ($timeout) {
        var def = {
            restrict: "A",
            terminal: true,
            transclude: false,
            link: function (scope, element, attrs) {
                $timeout(scope.$eval(attrs.afterRender), 0); //Calling a scoped method
            },
        };
        return def;
    },
]);

//ejecutara cualquier funcion de clik derecho
app.directive('ngRightClick', function ($parse) {
    return function (scope, element, attrs) {
        var fn = $parse(attrs.ngRightClick);
        element.bind('contextmenu', function (event) {
            scope.$apply(function () {
                event.preventDefault();
                fn(scope, { $event: event });
            });
        });
    };
});

app.directive('onLongPress', ['$parse', '$timeout', function ($parse, $timeout) {
    return {
        restrict: 'A',
        link: function ($scope, $elm, $attrs) {
            var timer;
            var timerDuration = (!isNaN($attrs.longPressDuration) && parseInt($attrs.longPressDuration)) || 600;
            // By default we prevent long press when user scrolls
            var preventLongPressOnScroll = ($attrs.preventOnscrolling ? $attrs.preventOnscrolling === 'true' : true)
            // Variable used to prevent long press while scrolling
            var touchStartY;
            var touchStartX;
            var MAX_DELTA = 15;
            // Bind touch, mouse and click event
            $elm.bind('touchstart', onEnter);
            $elm.bind('touchend', onExit);

            $elm.bind('mousedown', onEnter);
            $elm.bind('mouseup', onExit);

            $elm.bind('click', onClick);
            // For windows mobile browser
            $elm.bind('pointerdown', onEnter);
            $elm.bind('pointerup', onExit);
            if (preventLongPressOnScroll) {
                // Bind touchmove so that we prevent long press when user is scrolling
                $elm.bind('touchmove', onMove);
            }

            function onEnter(evt) {
                var functionHandler = $parse($attrs.onLongPress);
                // For tracking scrolling
                if ((evt.originalEvent || evt).touches) {
                    touchStartY = (evt.originalEvent || evt).touches[0].screenY;
                    touchStartX = (evt.originalEvent || evt).touches[0].screenX;
                }
                //Cancel existing timer
                $timeout.cancel(timer);
                //To handle click event properly
                $scope.longPressSent = false;
                // We'll set a timeout for 600 ms for a long press
                timer = $timeout(function () {
                    $scope.longPressSent = true;
                    // If the touchend event hasn't fired,
                    // apply the function given in on the element's on-long-press attribute
                    $scope.$apply(function () {
                        functionHandler($scope, {
                            $event: evt
                        });
                    });
                }, timerDuration);

            }

            function onExit(evt) {
                var functionHandler = $parse($attrs.onTouchEnd);
                // Prevent the onLongPress event from firing
                $timeout.cancel(timer);
                // If there is an on-touch-end function attached to this element, apply it
                if ($attrs.onTouchEnd) {
                    $scope.$apply(function () {
                        functionHandler($scope, {
                            $event: evt
                        });
                    });
                }

            }

            function onClick(evt) {
                //If long press is handled then prevent click
                if ($scope.longPressSent && (!$attrs.preventClick || $attrs.preventClick === "true")) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    evt.stopImmediatePropagation();
                }

            }

            function onMove(evt) {
                var yPosition = (evt.originalEvent || evt).touches[0].screenY;
                var xPosition = (evt.originalEvent || evt).touches[0].screenX;

                // If we scrolled, prevent long presses
                if (touchStartY !== undefined && touchStartX !== undefined &&
                    (Math.abs(yPosition - touchStartY) > MAX_DELTA) || Math.abs(xPosition - touchStartX) > MAX_DELTA) {
                    $timeout.cancel(timer);
                }

            }
        }
    };
}]);

//app.directive('ngDblclick',
//    function () {
//        const DblClickInterval = 300; //milliseconds
//        var firstClickTime;
//        var waitingSecondClick = false;
//        return {
//            restrict: 'A',
//            link: function (scope, element, attrs) {
//                element.bind('click', function (e) {

//                    if (!waitingSecondClick) {
//                        firstClickTime = (new Date()).getTime();
//                        waitingSecondClick = true;

//                        setTimeout(function () {
//                            waitingSecondClick = false;
//                        }, DblClickInterval);
//                    }
//                    else {
//                        waitingSecondClick = false;

//                        var time = (new Date()).getTime();
//                        if (time - firstClickTime < DblClickInterval) {
//                            scope.$apply(attrs.ngDblclick);
//                        }
//                    }
//                });
//            }
//        };
//    });