var factory = function ($) {
    var convertToObjectName = function (name) {
        var re = new RegExp("^([^\\[]*)\\[\\]([^\\]]*)$", "g");
        var oldName = null;
        while (oldName != name) {
            oldName = name;
            name = name.replace(re, "$1.$2");
        }
        return name.replace(/^\.+|\.+$/g, '');
    }
    function parseObject(form, obj) {
        var _this = form;

        $(_this).find("[name]").each(function (index, element) {
            var name = $(element).attr("name");
            var count = $(_this).find("[name='" + name + "']").length;
            name = convertToObjectName(name);
            var arr = name.split(".");
            name = "obj";

            for (var i = 0; i < arr.length; i++) {
                if (arr[i]) {
                    name += "['" + arr[i] + "']";
                    if (typeof eval(name) === 'undefined') {
                        return;
                    }
                    if (i < arr.length - 1 && eval(name) === null) {
                        if (typeof $(element).get(0).dataset["forcheckbox"] == "undefined") {
                            $(element).val("");
                        }
                        $(element).prop("checked", true);
                        return;
                    }
                }
            }

            var value = eval(name);
            if (typeof kendo != "undefined" && typeof $(element).data("role") != "undefined") {
                if ($(element).data("role") == "numerictextbox") {
                    $(element).data("kendoNumericTextBox").value(value);
                } else if ($(element).data("role") == "dropdownlist") {
                    $(element).data("kendoDropDownList").value(value);
                } else if ($(element).data("role") == "multiselect") {
                    $(element).data("kendoMultiSelect").value(value);
                } else if ($(element).data("role") == "switch") {
                    $(element).data("kendoMobileSwitch").value(value);
                }
                else {
                    $(element).val(value);
                }
            }
            else {
                if ($(element).attr("type") == 'checkbox' || $(element).attr("type") == 'radio') {
                    if (typeof value == "object" && typeof value.length != "undefined") {
                        var evalue = $(element).val();
                        if (!isNaN(evalue)) { evalue = parseFloat(evalue); }
                        if ($.inArray(evalue, value) !== -1) {
                            $(element).get(0).checked = true;
                        } else {
                            $(element).get(0).checked = false;
                        }
                    } else {
                        var myValue = $(element).val();
                        var isBool = myValue != null &&
                            (typeof (myValue) == "boolean" ||
                                (typeof (myValue) == "string" && (myValue.toLowerCase() == "true" || myValue.toLowerCase() == "false")));
                        if (isBool) {
                            myValue = JSON.parse(myValue);
                        }
                        if (myValue == value) {
                            $(element).get(0).checked = true;
                        } else {
                            $(element).get(0).checked = false;
                        }
                    }
                } else {
                    if (value == null) {
                        value = "";
                    }
                    if (count == 1) {
                        $(element).val(value);
                    }
                }
            }

            //console.log(name, " = ", value);
        });
    };
    function serializeObject(form) {
        var _this = form;

        var obj = {};

        $(_this).find("[name]:enabled").not("fieldset").not("[ignore-serialize]").each(function (index, element) {
            var name = $(element).attr("name");
            var value = $(element).val();
            var role = $(element).data("role");
            if (typeof kendo != "undefined" && typeof role != "undefined" && role) {
                if ($(element).data("role") == "numerictextbox") {
                    value = $(element).getKendoNumericTextBox().value();
                } else if ($(element).data("role") == "dropdownlist") {
                    value = $(element).getKendoDropDownList().value();
                } else if ($(element).data("role") == "multiselect") {
                    value = $(element).getKendoMultiSelect().value();
                } else if ($(element).data("role") == "switch") {
                    value = $(element).getKendoMobileSwitch().value();
                }

                name = convertToObjectName(name);
                var arr = name.split(".");
                name = "obj";
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i]) {
                        name += "['" + arr[i] + "']";
                        if (typeof eval(name) == 'undefined') {
                            eval(name + "={};");
                        }
                    } else if (typeof eval(name) != 'object' || typeof eval(name + ".length") == "undefined" || is_array) {
                        eval(name + "=(typeof " + name + "=='undefined' || (typeof " + name + ".length=='undefined' && " + is_array + "))?[]:" + name);
                    }
                }
                //console.log(value);
                if (typeof value == "string") {
                    eval(name + "=unescape('" + escape(value) + "')");
                } else if (typeof value == "object" && typeof value.length != "undefined") {
                    eval(name + "=" + JSON.stringify(value));
                }
                else {
                    eval(name + "=typeof(eval(value))!='undefined'?eval(" + value + "):unescape('" + escape(value) + "')");
                }
            } else {

                // Initializing default value for obj's field with multi-layer
                // Eg: with field name = "a.b.c" => obj.a.b.c={}
                // "a.b[]" => obj.a.b=[]
                // "a.b[0].x" => obj.a.b=[{x:}]

                // re-init RegExp everytime
                var arrayInputPatt = new RegExp(/^[^\n\[]+\[[0-9]+\].[^\n\]]+$/g);
                var arrayFieldPatt = new RegExp(/([^\n]*)\[([0-9]+)\]$/g);
                if (arrayInputPatt.test(name)) {
                    var arr = name.split(".");
                    name = "obj";
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i]) {
                            var matched = arrayFieldPatt.exec(arr[i]);
                            if (matched != null && matched.length == 3) {
                                name += "." + matched[1];
                                if (typeof eval(name) == 'undefined') {
                                    eval(name + "=[];");
                                }
                                var index = matched[2];
                                name += "[" + index + "]";
                                if (typeof eval(name) == "undefined") {
                                    eval(name + "={};");
                                }
                            } else {
                                name += "." + arr[i];
                                if (typeof eval(name) == 'undefined') {
                                    eval(name + "={};");
                                }
                            }
                        }
                    }
                    eval(name + "=unescape('" + escape(value) + "')");
                } else {
                    var is_array = false;
                    var outputPatt = new RegExp(/^\[[^\n]+\]$/g);
                    if (outputPatt.test(name)) {
                        name = `obj["${name}"]`;
                    } else {
                        if (name.endsWith("[]")) {
                            is_array = true;
                            name = name.substring(0, name.length - 2);
                        }
                        name = convertToObjectName(name);
                        var arr = name.split(".");
                        name = "obj";
                        for (var i = 0; i < arr.length; i++) {
                            if (arr[i]) {
                                name += "['" + arr[i] + "']";
                                if (typeof eval(name) == 'undefined') {
                                    eval(name + "={};");
                                }
                            }
                        }
                    }
                    if (is_array) {
                        eval(name + "=(typeof " + name + "=='undefined' || (typeof " + name + ".length=='undefined'))?[]:" + name);
                    }
                    // Check value's type
                    var isBool = value != null &&
                        (typeof (value) == "boolean" ||
                            (typeof (value) == "string" && (value.toLowerCase() == "true" || value.toLowerCase() == "false")));
                    if (isBool) {
                        value = JSON.parse(value);
                    }
                    var isNan = isNaN(value);

                    var checked = $(element).is(":checked") || typeof $(element).attr("checked") != "undefined";
                    var type = $(element).attr("type");
                    type = typeof type == "undefined" ? $(element).prop("tagName").toLowerCase() : type;
                    //console.log(name, value, isNan, isBool, type, checked);

                    if (type == "radio" && checked) {
                        eval(name + "=unescape('" + escape(value) + "')");
                    }

                    if (type == "checkbox") {
                        if (checked) {
                            if (is_array) {
                                if (!isNan) {
                                    eval(name + ".push(JSON.parse(" + value + "))");
                                } else {
                                    eval(name + ".push(unescape('" + escape(value) + "'))");
                                }
                            } else {
                                if (!isNan && value) {
                                    eval(name + "= JSON.parse(" + value + ")");
                                } else {
                                    eval(name + "= (unescape('" + escape(value) + "'))");
                                }
                            }
                        } else if (!is_array && isBool) {
                            eval(name + "= JSON.parse(" + (!value) + ")");
                        }
                    } else if (type == "select") {
                        if (is_array) {
                            if (value != null) {
                                value = JSON.stringify((value + "").split(","));
                                eval(name + "=" + value);
                            }
                        } else {
                            if (!isNan && value) {
                                eval(name + "= JSON.parse(" + value + ")");
                            } else {
                                eval(name + "= (unescape('" + escape(value) + "'))");
                            }
                        }
                    } else {
                        var originName = $(element).attr("name").replace(".", "\\.");
                        //console.log(originName, $(_this).find("[name='" + originName + "']:checked"));
                        if ($(_this).find("[name='" + originName + "']:checked").length == 0) {
                            if (isBool) eval(name + "=JSON.parse(" + value + ")");
                            else
                                eval(name + "=unescape('" + escape(value) + "')");
                        }
                    }
                }
                /*
                if ($(element).attr("type") == 'checkbox' && (!isNan || isBool || is_array)) {
                    if (typeof eval(name) == "object" && typeof eval(name + ".length") != "undefined") {
                        if (checked) {
                            if (!isNan) {
                                eval(name + ".push(JSON.parse('" + escape(value) + "'))");
                            } else {
                                eval(name + ".push(unescape('" + escape(value) + "'))");
                            }
                        }
                    } else {
                        if (isBool) {
                            if (checked) {                                
                                eval(name + "=JSON.parse(" + value + ")");                                
                            }
                        } else
                            if (!checked) {
                                eval(name + "=(eval(" + name + ")||false)&&true");
                            } else {
                                if (typeof (value) == "undefined" || value=="") {
                                    eval(name + "=true");
                                }else
                                if (!isNaN(value)) {
                                    eval(name + "=JSON.parse(" + value + ")");
                                } else {
                                    eval(name + "=unescape('" + escape(value) + "')");
                                }
                            }
                    }
                } else if ($(element).attr("type") == 'radio') {
                    if (checked) {
                        eval(name + "=unescape('" + escape(value) + "')");
                    }
                } else if (is_array) {
                    eval(name + "=typeof " + name + "=='undefined'?[]:" + name);
                    //console.log("arr", name, value, !isNan);
                    if (!isNan && value!=='') {
                        eval(name + ".push(JSON.parse('" + escape(value) + "'))");
                    } else {
                        eval(name + ".push(unescape('" + escape(value) + "'))");
                    }
                } else {
                    if (isBool) eval(name + "=JSON.parse(" + value + ")");
                    else
                        eval(name + "=unescape('" + escape(value) + "')");
                }
                */
            }
        });
        return obj;
    };
    function toValidQueryName(name) {
        return name.replace(/[\\]*(\.|\[|\])/g, "\\$1");
    }
    $.formHelper = {
        parseObject: parseObject,
        serializeObject: serializeObject,
        convertToObjectName: convertToObjectName,
        toValidQueryName: toValidQueryName
    };
    return $.formHelper;
};
if (typeof define == "function") {
    define(["jquery"
        , 'kendo.multiselect.min', 'kendo.numerictextbox.min', 'kendo.dropdownlist.min', 'kendo.mobile.switch.min'
    ], factory);
}
else { factory(jQuery); }