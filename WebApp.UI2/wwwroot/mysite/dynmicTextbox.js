function GetDynamicTextBox(value) {
    return '<input name = "AttributeOptions" type="text" value = "' + value + '" />' +
        '<input type="button" value="Remove" onclick = "RemoveTextBox(this)" />'
}
function AddTextBox() {
    var div = document.createElement('div');
    div.innerHTML = GetDynamicTextBox("");
    document.getElementById("TextBoxContainer").appendChild(div);
}

function RemoveTextBox(div) {
    document.getElementById("TextBoxContainer").removeChild(div.parentNode);
}

function RecreateDynamicTextboxes() {
    var values = eval('<%=Values%>');
    if (values != null) {
        var html = "";
        for (var i = 0; i < values.length; i++) {
            html += "<div>" + GetDynamicTextBox(values[i]) + "</div>";
        }
        document.getElementById("TextBoxContainer").innerHTML = html;
    }
}
window.onload = RecreateDynamicTextboxes;