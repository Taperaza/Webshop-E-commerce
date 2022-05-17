import React from 'react';
import { useState, useContext } from 'react';
import {Form, Row,Col, FormGroup, Input, Label, Button} from 'reactstrap';
import axios from 'axios';


function Register(){

        const [customer, setCustomer] = useState({
        FirstName: '',
        LastName: '',
        Username:'user0',
        PasswordCustomer: '',
        Email:'',
        Address1: '',
        Address2: '',
        City:'',
        PostalCode:'',
        Country:'',
        Mobile:''
     });

    function handleChange(event){
        const { value, id } = event.target;

        setCustomer(prevValue => ({
            ...prevValue,
            [id]: value
        }))
      } 


    function addCustomer(){
        axios.post("https://localhost:44388/api/customer", customer).then((response) =>{
            console.log(response.data);
        })

        
    }

    return(
        <div className='w-100 min-vh-100 mb-5'>
            <h4 className='mb-5 ms-3'>Register</h4>
            <div className='ms-5 me-5'>
            <Form className=''>
                <Row>
                    <Col md={6}>
                    <FormGroup>
                        <Label for="FirstName">First Name</Label>
                        <Input 
                        id="FirstName"
                        name="FirstName"
                         onChange={handleChange} 
                         value ={customer.FirstName}
                         required/>
                    </FormGroup>
                    </Col>
                    <Col md={6}>
                    <FormGroup>
                        <Label for="LastName">Last Name</Label>
                        <Input 
                        id="LastName" 
                        name="LastName"
                        onChange={handleChange} 
                        value ={customer.LastName} required/>
                    </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col md={6}>
                    <FormGroup>
                        <Label for="Email">Email</Label>
                        <Input
                         id="Email" 
                         name="Email"
                         type="email"
                         onChange={handleChange}
                         value ={customer.Email} required/>
                    </FormGroup>
                    </Col>
                    <Col md={6}>
                    <FormGroup>
                        <Label for="PasswordCustomer">Password</Label>
                        <Input id="PasswordCustomer"
                        name="PasswordCustomer"
                        type="password" onChange={handleChange} 
                        value ={customer.PasswordCustomer} required/>
                    </FormGroup>
                    </Col>
                </Row>
                    <FormGroup>
                        <Label for="Address1">
                        Address
                        </Label>
                        <Input
                        id="Address1"
                        name="Address1"
                        onChange={handleChange}
                        value ={customer.Address1}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="Address2">
                        Address 2
                        </Label>
                        <Input
                        id="Address2"
                        name="Address2"
                        onChange={handleChange}
                        value ={customer.Address2}/>
                    </FormGroup>
                <Row>
                    <Col md={6}>
                    <FormGroup>
                        <Label for="City">
                        City
                        </Label>
                        <Input
                        id="City"
                        name="City"
                        onChange={handleChange}
                        value ={customer.City}/>
                    </FormGroup>
                    </Col>
                    <Col md={4}>
                    <FormGroup>
                        <Label for="Country">
                        State
                        </Label>
                        <Input
                        id="Country"
                        name="Country"
                        onChange={handleChange}
                        value ={customer.Country}/>
                    </FormGroup>
                    </Col>
                    <Col md={2}>
                    <FormGroup>
                        <Label for="PostalCode">
                        Zip
                        </Label>
                        <Input
                        id="PostalCode"
                        name="PostalCode"
                        onChange={handleChange}
                        value ={customer.PostalCode}/>
                    </FormGroup>
                    </Col>
                </Row>
                <FormGroup>
                        <Label for="Mobile">
                        Mobile phone
                        </Label>
                        <Input
                        id="Mobile"
                        name="Mobile"
                        onChange={handleChange}
                        value ={customer.Mobile}/>
                    </FormGroup>
                <Button className='mt-3' onClick={addCustomer}>
                    Register
                </Button>
                </Form>
            </div>
        </div>
    );
}

export default Register;