 document.addEventListener('DOMContentLoaded', function () {
    const cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    updateCartDisplay(cartItems);

    document.querySelectorAll('.add2cart').forEach(button => {
        button.addEventListener('click', function () {
            const productItem = button.closest('.product-item');
            const productImage = productItem.querySelector('.pi-img-wrapper img').src;
            const productName = productItem.querySelector('h3 a').textContent;
            const productPrice = productItem.querySelector('.pi-price').textContent;
            const productId = +productItem.querySelector('#hfPid').value;
 
            //const orderId = orderItem.querySelector('#hfPid').value;
            //const ItemQuantity = orderItem.querySelector('#hfPid').value;



            const existingItemIndex = cartItems.findIndex(item => item.name === productName);
             
            if (existingItemIndex !== -1) {
                cartItems[existingItemIndex].quantity += 1;
            } else {
                const newItem = {
                    image: productImage,
                    name: productName,
                    price: productPrice,  
                    quantity: 1,
                    productid: productId,
                    customerId:1
                };
                cartItems.push(newItem);
            }

            localStorage.setItem('cartItems', JSON.stringify(cartItems));
            updateCartDisplay(cartItems);

 

            toastr.success('Product added to cart');
        });


        console.log(JSON.stringify(cartItems));

    });

    function updateCartDisplay(cartItems) {
        const cartCount = document.querySelector('.top-cart-info-count');
        const cartValue = document.querySelector('.top-cart-info-value');
        const cartList = document.querySelector('.scroller');
        let totalValue = 0;
        let totalCount = 0;

        cartList.innerHTML = '';

        cartItems.forEach((item, index) => {
            const cartItem = document.createElement('li');
            cartItem.innerHTML = `
                <a href="shop-item.html"><img src="${item.image}" alt="${item.name}" width="37" height="34"></a>
                <span class="cart-content-count">x ${item.quantity}</span>
                <strong><a href="shop-item.html">${item.name}</a></strong>
                <em>${item.price}</em>
                <div>
                    <button class="btn-increase" data-index="${index}">+</button>
                    <button class="btn-decrease" data-index="${index}">-</button>
                </div>
                <a href="javascript:void(0);" class="del-goods" data-index="${index}">&nbsp;</a>
            `;
            cartList.appendChild(cartItem);
            totalValue += parseFloat(item.price.replace('$', '')) * item.quantity;
            totalCount += item.quantity;
        });

        cartCount.textContent = `${totalCount} items`;
        cartValue.textContent = `$${totalValue.toFixed(2)}`;

        document.querySelectorAll('.del-goods').forEach(delButton => {
            delButton.addEventListener('click', function () {
                const index = delButton.getAttribute('data-index');
                cartItems.splice(index, 1);
                localStorage.setItem('cartItems', JSON.stringify(cartItems));
                updateCartDisplay(cartItems);
            });
        });

        document.querySelectorAll('.btn-increase').forEach(button => {
            button.addEventListener('click', function () {
                const index = button.getAttribute('data-index');
                cartItems[index].quantity += 1;
                localStorage.setItem('cartItems', JSON.stringify(cartItems));
                updateCartDisplay(cartItems);
            });
        });

        document.querySelectorAll('.btn-decrease').forEach(button => {
            button.addEventListener('click', function () {
                const index = button.getAttribute('data-index');
                if (cartItems[index].quantity > 1) {
                    cartItems[index].quantity -= 1;
                    localStorage.setItem('cartItems', JSON.stringify(cartItems));
                    updateCartDisplay(cartItems);
                } else {
                    toastr.warning('Quantity cannot be less than 1');
                }
            });
        });

    }
});
