// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function postOrder(e) {
    e.preventDefault();
    e.stopPropagation();
    let pedido = $('#pedido').html();
    let mesa = $('#mesa').html();
    let id = e.target.parentElement.getAttribute('data-id');
    let qtde = e.target.parentElement.querySelector('#qtde').value;
    let price = e.target.parentElement.querySelector('#price').innerText;
    console.log(price);
    console.log('pedido:' + pedido + ' mesa:' + mesa)

    let data = {
        number: mesa,
        Items: [{
            amount: qtde,
            menuItemId: id,
            orderId: pedido
        }]
    }

    console.log(data);

    fetch("/order", {
        method: 'POST',
        body: JSON.stringify(data),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(data => data)
        .then(json => {
            console.log(json.status);
        })
}

