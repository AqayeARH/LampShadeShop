
const cookieName = "cart-items";

function addToCart(id, name, price, picture) {

    let products = $.cookie(cookieName);

    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();

    const currentProduct = products.find(x => x.id === id);

    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);

    products = JSON.parse(products);

    $("#cart_items_count").text(products.length);

    let cartItems = $("#cart_items");

    cartItems.html('');

    products.forEach(item => {
        let product = `<div class="single-cart-item">
        <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${item.id}')">
            <i class="ion-android-close"></i>
        </a>
        <div class="image">
            <a href="single-product.html">
                <img src="/pictures/products-banner/${item.picture}"
                     class="img-fluid" alt="محصولات موجود در سبد خرید ${item.name}" title="محصولات موجود در سبد خرید ${item.name}">
            </a>
        </div>
        <div class="content">
            <p class="product-title">
                <a href="single-product.html">${item.name}</a>
            </p>
            <p class="count">${item.count} x <br/> ${item.unitPrice} تومان</p>
        </div>
    </div>`;
        cartItems.append(product);
    });
}

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalPrice, count) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = parseInt(count);
    const product = products[productIndex];
    const newPrice = parseInt(product.unitPrice) * parseInt(count);
    $(`#${totalPrice}`).text(newPrice);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();

    var settings = {
        "url": "https://localhost:7168/api/inventory",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "ProductId": id,
            "Count": count
        }),
    };

    $.ajax(settings).done(function (data) {
        if (data.isInStock == false) {
            const warningsDiv = $('#productStockWarnings');
            if ($(`#${id}`).length == 0) {
                warningsDiv.append(`
                    <div class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </div>
                `);
            }
        } else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}
