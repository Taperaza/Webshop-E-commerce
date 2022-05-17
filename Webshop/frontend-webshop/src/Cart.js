import React, {useState, useEffect, useContext} from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import {CartContext} from './Components/CartContext';


function Cart(props) {
    const [modal, setModal] = useState({isOpen: false});
    const [cartItems, setCartItems] = useContext(CartContext);

    useEffect(() => {
       
    })

    function handleOpen() {
        setModal({isOpen : !modal.isOpen});
        setCartItems(JSON.parse(localStorage.getItem("cartItems")));
    }

    console.log(cartItems);

    function IsCartEmpty() {
        if (cartItems && cartItems.length) {
            return <div>
                {cartItems.map((item) =>
                <div key={item.Id}>{item.ProductName}, Price: {item.ProductPrice} kn</div>
            )}</div>
        }
        else{
            return <div>Add items to the cart to start shoping.</div>
        }
    }
    
    return (
        <div>
            <button className="cartButton" onClick={handleOpen}>Cart ({cartItems.length})</button>
            <Modal isOpen={modal.isOpen} toggle={handleOpen}>
                <ModalHeader>Your cart</ModalHeader>
                <ModalBody>
                    <IsCartEmpty />
                </ModalBody>
                <ModalFooter>
                    <button onClick={handleOpen}>Close</button>
                    <button>
                    <a href = {"/checkout"}>
                    Go to checkout
                    </a>
                    </button>
                </ModalFooter>
            </Modal>  
        </div>
    )
}

export default Cart;