import React from "react";
import { useState, useContext } from 'react';
import axios from "axios";
import {LoggedUserContext} from './LoggedUserContext';

function Login(){

    
    const [customer, setCustomer] = useContext(LoggedUserContext);

    const [customerInput, setCustomerInput] = useState({
        Email:'jjj@gmail.com',
        PasswordCustomer: 'šifra100',
    })

    function handleChange(event){
        const { value, id } = event.target;

        setCustomerInput(prevValue => ({
            ...prevValue,
            [id]: value
        }))
      } 
     
    
    function login(event){
        axios.get(`https://localhost:44388/api/customer?Email=${customerInput.Email}`).then((response) => {
            setCustomer(response.data);
            console.log(response.data);
        });
        event.preventDefault();
        if(customer.PasswordCustomer === customerInput.PasswordCustomer){
            setCustomer(prevValue=> ({...prevValue, Success:true}));
        }else return <h1>Wrong password!</h1>
        
    }

    function logout(){
        setCustomer({
            Email:'',
            PasswordCustomer: '',
            Success: false,
        });
        
    }


    return(
        <>
        {customer.Success ? (<section>
                    <h1>You are logged in!</h1>
                    <br />
                    <button type="submit" onClick={logout} className="btn btn-primary">Logout</button>
                    <p>
                        <a href="#">Go to Home</a>
                    </p>
                </section>) : (
        <div class="d-flex justify-content-center">
            <form className='w-25'>
            <h4 className=' mb-3'>Login</h4>
            <div className=" mb-3">
                <label for="Email" className="form-label">Email</label>
                <input type="email"
                 className="form-control"
                  id="Email"
                  value ={customerInput.Email}
                  onChange={handleChange}  required/>
            </div>
            <div class="mb-3">
                <label for="PasswordCustomer" className="form-label">Password</label>
                <input type="password"
                 className="form-control"
                 id="PasswordCustomer"
                 value ={customerInput.PasswordCustomer}
                 onChange={handleChange}  required/>
            </div>
            <button type="submit" onClick={login} className="btn btn-primary">Login</button>
            </form>
        </div> )}  
        </>
    );
}

export default Login;