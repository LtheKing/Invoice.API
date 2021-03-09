// var script = document.createElement('script');
// script.src = 'https://code.jquery.com/jquery-3.5.1.slim.min.js';
// script.type = 'text/javascript';
// document.getElementsByTagName('head')[0].appendChild(script);

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

//add new line
function btnNewLineOnClick() {
    // debugger;
    // var i = 0;
    // var original = document.getElementById('bottomContent' + i);
    // var clone = original.cloneNode(true); // "deep" clone
    // clone.id = "bottomContent" + ++i; // there can only be one element with an ID
    // //clone.onclick = btnNewLineOnClick; // event handlers are not cloned
    // original.parentNode.appendChild(clone);

    var max_fields = 25;
    var wrapper = $('.multiContent');
    var add_button = $('.btnNewLine');
    var x = 0;
    if (x < max_fields) {
        x++;
        $(wrapper).append(`
        <div class="row induk">
        <div class="col-4">
              <label for="ContentNameLabel">Name</label>
              <textarea class="form-control" id="txt_contentName" name="ContentName" rows="4" cols="50"></textarea>
            </div>
            <div class="col">
              <label for="Quantity">Quantity</label>
              <input type="number" name="Quantity" id="txt_quantity" class="form-control c_qty" value="0">
            </div>
            <div class="col">
              <label for="ContentName">Rate</label>
              <input type="number" name="Rate" id="txt_rate" class="form-control c_rate" value="1000">
              <select class="form-control mt-2" id="select_rate">
                <option>hour</option>
                <option>day</option>
              </select>
            </div>
            <div class="col">
              <label for="Amount">Amount</label>
              <input type="number" name="Amount" id="txt_amount" class="form-control c_amount">
            </div>
            <hr style="width:100%;text-align:left;margin-left:0"></div> `);

        $('.c_qty').change(function (e) {
            var rate = $(e.target).closest('.induk').find('.c_rate').val();
            var field_amount = $(e.target).closest('.induk').find('.c_amount')
            calculateAmountMulti(e.target.value, rate, field_amount)
        });

        $('.c_rate').change(function(e) {
            var qty = $(e.target).closest('.induk').find('.c_qty').val();
            var field_amount = $(e.target).closest('.induk').find('.c_amount')
            calculateAmountMulti(qty, e.target.value, field_amount)
        })
    }
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
    var tbody = table.getElementsByTagName("tr")[0];
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
    setNomorPO();
};
// END FUNCTION CREATE DYNAMIC TABLE

// set value to nomor po input text when table clicked
function setNomorPO() {
    $('td').on('click', function (e) {
        // console.log($(this).text())
        document.getElementById('txt_poNumber').value = $(this).text();
        $('#exampleModalScrollable').modal('hide');
    });
}

//content editor on change
function onContentEditorChange() {

}

//calculate initial amount
function calculateAmount() {
    
}

//calculate amount multi
function calculateAmountMulti(qty, rate, field_amount) {
    var tot = qty * rate;
    field_amount.val(tot);
}

//calculate total
function calculateTotal() {
    var disc = document.getElementById('txt_discount').value;
    var subTot = document.getElementById('txt_subtotal').value;
    var tot = subTot - disc;
    document.getElementById('txt_total').value = tot;
}

//calculate diskon
function calculateDiskon() {
    var discPersen = document.getElementById('txt_discountPersentation').value / 100;
    var disc = document.getElementById('txt_discount').value;
    var subTot = document.getElementById('txt_subtotal').value;

    var res = discPersen * subTot;
    document.getElementById('txt_discount').value = res;
}

//calculate blok bawah
function calculateBlokBawah() {
    calculateDiskon();
    calculateTotal();
}