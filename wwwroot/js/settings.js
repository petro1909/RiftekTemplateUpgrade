
async function LoadDefaultCommParamsForm() {
    const response = await fetch("/connection/GetDefaultConnectionParameters", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok) {
        connectionSettings = await response.json();
        $("#desktop_ip").val(connectionSettings.desktopAddress);
        $("#desktop_port").val(connectionSettings.desktopPort);
        $("#scanner_ip").val(connectionSettings.scannerAddress);
    }
}
LoadDefaultCommParamsForm();

async function SaveCommSettingsForm() {

    connectionSettings.desktopAddress = $("#desktop_ip").val();
    connectionSettings.desktopPort = $("#desktop_port").val();
    connectionSettings.scannerAddress = $("#scanner_ip").val();

    console.log("ewfewf")
    const response = await fetch("/connection/SetConnectionParameters", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(connectionSettings)
    });
    if (response.ok) {

    }
}