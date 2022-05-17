import React,{useState, useContext} from 'react'
import { InputLabel, Select, MenuItem, Button, Grid, Typography } from '@material-ui/core';
import { useForm, FormProvider } from 'react-hook-form';
import FormInput from './CustomTextField';
import { LoggedUserContext } from '../Nikolina/LoggedUserContext';
import '../CheckoutForm/styles.css';

const AdressForm = () => {
  const methods = useForm();

  const [customer, setCustomer] = useContext(LoggedUserContext);

  return (
    <div>
      <Typography variant="h6" gutterBottom>Shipping address</Typography>
      <br/><br/>
      <FormProvider {...methods}>
        <form onSubmit=''>
          <Grid container spacing={3}>    
          <div>   
            <label for="firstname" class="form-label">First Name</label>
            <input type="text" required name='firstname' label='First name' value={customer.FirstName} />
          </div>
          <div>
            <label for="lastname" class="form-label">Last Name</label>
            <input type="text" required name='lastname' label='Last name' value={customer.LastName}/>
          </div>
          <div> 
            <label for="adress1" class="form-label">Adress1</label>
            <input type="text" required name='address1' label='Adress1' value={customer.Address1}/>
          </div>
          <div>
            <label for="adress2" class="form-label">Adress2</label>
            <input type="text" required name='address2' label='Adress2' value={customer.Address2}/>
          </div>
          <div>
            <label for="Email" class="form-label">Email</label>
            <input type="text" required name='email' label='Email' value={customer.Email}/>
          </div>
          <div>
            <label for="country" class="form-label">Country</label>
            <input type="text" required name='country' label='Country' value={customer.Country}/>
          </div>
          <div>
            <label for="zip" class="form-label">ZIP</label>
            <input type="text" required name='zip' label='ZIP / Postal code' value={customer.PostalCode}/>
          </div>
          <div>
            <label for="mobile" class="form-label">Mobile</label>
            <input type="text" required name='mobile' label='Mobile' value={customer.Mobile}/>
          </div>
          </Grid>
        </form>
      </FormProvider>
    </div>
  )
}

export default AdressForm