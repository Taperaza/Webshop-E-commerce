import { InputLabel, Select, MenuItem, Button, Grid, Typography } from '@material-ui/core';
import React, { useState } from 'react'


export default function PaymentForm() {
  const [paymentMethod, setPaymentMethod] = useState('PayPal');
  const submitHandler = (e) => {}


  return (
    <div>
      <form className="form" onSubmit={submitHandler}>
        <div>
        <Typography variant="h6" gutterBottom>Payment form</Typography>
        </div>
        <br/>
        <div>
          <div>
            <input 
            type="radio"
            id="paypal"
            value="PayPal"
            name="paymentMethod"
            required
            checked
            onChange={(e) => setPaymentMethod(e.target.value)}
            ></input>
            <label htmlFor="paypal">PayPal</label>
          </div>
        </div>
        <br/>
        <div>
          <div>
            <input 
            type="radio"
            id="cash"
            value="Cash"
            name="paymentMethod"
            required
            checked
            onChange={(e) => setPaymentMethod(e.target.value)}
            ></input>
            <label htmlFor="cash">Pay with cash upon delivery</label>
          </div>
          <br/> <br/>
        </div>
        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
              <Button type="button" >Back</Button>
              <Button type="submit" variant="contained" color="primary">
                Pay
              </Button>
        </div>
      </form>
    </div>
  )
}
