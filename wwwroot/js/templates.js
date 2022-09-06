var templates = []
var connectionSettings;

let settings = document.getElementById("template_settings");


//Получение списка шаблонов из json файла
async function GetTemplates() {
    const response = await fetch("/template/templates", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok) {
        templates = await response.json();
        console.log(templates)
        templates.forEach(template => {
            CreateTempateDiv(template);
        });
        CreateAddTemplateDiv()
    }
}
GetTemplates();

//Создание кнопки для каждого шаблона
function CreateTempateDiv(template) {
    let button = document.createElement("button");
    button.textContent = template.number;
    button.className = "template_item";
    button.onclick = function () {
        //Изменеие стиля нажатой кнопки
        $(".template_item").removeClass("clicked");
        $(this).addClass("clicked")
        //Вывод данных шаблона
        ShowSettings(template);
    }
    $('#templates_data').append(button);
}

//Создание кнопки для добавления шаблона
function CreateAddTemplateDiv() {
    let button = document.createElement("button");
    button.textContent = "Add";
    button.className = "template_item";
    button.onclick = function () {
        //Изменеие стиля нажатой кнопки
        $(".template_item").removeClass("clicked");
        $(this).addClass("clicked")
        //Вывод данных шаблона
        AddTemplate();
    }
    $('#templates_data').append(button);
}



async function AddTemplate() {
    const response = await fetch("/template/AddTemplate", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok) {
        $("#templates_data").empty();
        GetTemplates();
    }
}

async function DeleteTemplate(template) {
    $.ajax({
        type: "GET",                                       
        url: "/template/DeleteTemplate",
        data: { number: template.number },
        success: function () {
            window.location.reload()
        }
    });
}



//Вывод данных шаблона
function ShowSettings(template) {
    for (const [key, val] of Object.entries(template.scannerSettings)) {
        $('#' + key).text(val)
    }

    $("#save_settings").removeAttr("disabled");
    $("#save_settings").unbind();
    $("#save_settings").bind('click', function () {
        console.log("fefef")
        SaveCurrentSettingsToTemplate(template);
    });

    $("#delete_template").removeAttr("disabled");
    $("#delete_template").unbind();
    $('#delete_template').bind('click', function () {
        console.log("dwdw")
        DeleteTemplate(template);
    });
}



async function SaveCurrentSettingsToTemplate(template) {
    console.log(JSON.stringify(template))
    const response = await fetch("/scanner/GetScannerSettings", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok) {
        template.scannerSettings = await response.json();
    }

    const response1 = await fetch("/template/SaveTempalteSettings", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(template)
    });
    if (response1.ok) {
        ShowSettings(template);
    }
}

