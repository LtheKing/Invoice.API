window.onload = onPageLoaded();
function onPageLoaded() {
    localStorage.clear();
    getAllProps();
    // setCKEditor();
}

// function setCKEditor() {
//     ClassicEditor
//         .create(document.querySelector('#txt_contentName'))
//         .catch(error => {
//             console.error(error);
//         });
// }

async function getAllProps() {
    const data = await fetch("https://localhost:5001/api/Invoice/GetAllProps", {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    });

    const dataProps = await data.json();
    var rawData = JSON.stringify(dataProps);
    localStorage.setItem('data', rawData);
    onSelectLanguage();
    onSelectCurrency();
    onSelectCustomer();
    setNoInvoice();
}

function btnResetSenderOnClick() {
    document.getElementById('txt_sender').value = ''
}

function btnNewLineOnClick() {
    console.log('new line');
}

function onSelectCurrency() {
    var jsonData = JSON.parse(localStorage.data);
    var currencies = jsonData.value.currencies;
    var cr = currencies.map(a => a.currency);
    var el = document.getElementById('select_currency');
    cr.forEach(function (key, index) {
        var opt = document.createElement("option");
        opt.value = key;
        opt.innerHTML = key;
        el.appendChild(opt);
    });
}

function onSelectLanguage() {
    var jsonData = JSON.parse(localStorage.data);
    var lang = jsonData.value.language;
    var cr = lang.map(a => a.language);
    var el = document.getElementById('select_language');
    cr.forEach(function (key, index) {
        var opt = document.createElement("option");
        opt.value = key;
        opt.innerHTML = key;
        el.appendChild(opt);
    });
}

function onSelectCustomer() {
    var jsonData = JSON.parse(localStorage.data);
    var cust = jsonData.value.customers;
    var cr = cust.map(a => a.name);
    var el = document.getElementById('select_customers');
    cr.forEach(function (key, index) {
        var opt = document.createElement("option");
        opt.value = cust[index].id;
        opt.innerHTML = key;
        el.appendChild(opt);
    });
}

function setNoInvoice() {
    var jsonData = JSON.parse(localStorage.data);
    document.getElementById('txt_noInvoice').value = jsonData.value.noInvoice;
}

function onCustomerChange() {
    var jsonData = JSON.parse(localStorage.data);
    var cust = jsonData.value.customers;
    var newValue = document.getElementById('select_customers').value;
    var adr = cust.filter(a => a.id == parseInt(newValue));
    document.getElementById('txt_address').value = adr[0].address;
}

function getPOValue() {
    var table = document.getElementsByTagName("table")[0];
    var tbody = table.getElementsByTagName("tbody")[0];
    tbody.onclick = function (e) {
        e = e || window.event;
        var data = [];
        var target = e.srcElement || e.target;
        while (target && target.nodeName !== "TR") {
            target = target.parentNode;
        }
        if (target) {
            var cells = target.getElementsByTagName("td");
            for (var i = 0; i < cells.length; i++) {
                data.push(cells[i].innerHTML);
            }
        }
        console.log(data);
    }
}

// BEGIN FUNCTION CREATE DYNAMIC TABLE
function createDynamicTable() {
    debugger;
    var jsonData = JSON.parse(localStorage.data);
    var data = jsonData.value.customerPOs;
    document.addEventListener('DOMContentLoaded', populate(data), false);
}

function dom(tag, text) {
    let r = document.createElement(tag);
    if (text) r.innerText = text;
    return r;
};

function append(parent, child) {
    parent.appendChild(child);
    return parent;
};

function populate(json) {
    const wrapper = document.getElementById('listPO');
    if (json.length === 0) return;
    let keys = Object.keys(json[0]);
    let table = dom('table');
    //header
    append(table,
        keys.map(k => dom('th', k)).reduce(append, dom('tr'))
    );
    //values
    const makeRow = (acc, row) =>
        append(acc,
            keys.map(k => dom('td', row[k])).reduce(append, dom('tr'))
        );
    json.reduce(makeRow, table);
    wrapper.appendChild(table);
};
// END FUNCTION CREATE DYNAMIC TABLE

//event when modal show
