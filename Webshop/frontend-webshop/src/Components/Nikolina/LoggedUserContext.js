import React from 'react';
import {useState, createContext} from 'react';


//context importamo gdje ga želimo koristiti
export const LoggedUserContext = createContext(); 

//ono što želimo dijeliti
export const LoggedUserProvider = props => {

   

    const [customer, setCustomer] = useState({
        FirstName: 'Ivan',
        LastName: 'Pandzic',
        Username:'user0',
        PasswordCustomer: '1234',
        Email:'ivanblabla@gmail.com',
        Address1: 'vic',
        Address2: 'vijenac1',
        City:'Osijek',
        PostalCode:'31000',
        Country:'Hrvatska',
        Mobile:'5050505',
        Success: false
     });



    return(
        //u ovo dolje dodajemo komponente kojima želimo prenijeti podatke iz providera, ali bolje je napisati
        <LoggedUserContext.Provider value={[customer, setCustomer]}>
            {props.children}
        </LoggedUserContext.Provider>

    );


}