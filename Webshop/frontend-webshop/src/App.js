
import {Navbar, Checkout} from './Components';
import React, { useEffect } from 'react'
import Products from './Components/Products/Products';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import DetailProductPage from './Components/Products/DetailProductPage/DetailProductPage'; 
import Categories from './Categories';
import { CartProvider } from './Components/CartContext';
import Footer from './Components/Footer/Footer';
import Login from './Components/Nikolina/Login';
import Register from './Components/Nikolina/Register';
import { LoggedUserProvider } from './Components/Nikolina/LoggedUserContext';
import RegisterLogin from './RegisterLogin';
import PaymentForm from './Components/CheckoutForm/PaymentForm';

const App = () => {

  return (
    <Router>
      <div>
      <LoggedUserProvider>
        <CartProvider>
        
          <Navbar />
          <Routes>
              <Route exact path = "/" element = {<Categories/>} />
              <Route exact path="/products/:CategoryId" element={<Products />} />  
              <Route exact path="/products/details/:productId" element={<DetailProductPage/>} />
              <Route exact path="/register" element={<Register/>} />
              <Route exact path="/login" element={<Login/>} />
              <Route exact path="/checkout" element={<Checkout/>} />
              <Route exact path="/payment" element={<PaymentForm/>} />

   
          </Routes>
          </CartProvider>
        </LoggedUserProvider> 
          
        <br/>
        <br/>
        <br/>
        <br/>
        <br/>
        <br/>
        <br/>

        <br/>
        <br/>
        <br/>
        <br/>
        <br/>
        <br/>

        <br/>
        <br/>
        <br/>
        <br/>
        <Footer />
      </div>
    </Router>
  )
}

export default App;