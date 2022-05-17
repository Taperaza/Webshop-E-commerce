import React, {useContext} from 'react';
import { AppBar, Toolbar, IconButton, Badge, MenuItem, Menu, Typography } from '@material-ui/core';
import { ShoppingCart } from '@material-ui/icons';
import useStyles from './styles';
import logo from '../../assets/commerce.png';
import Cart from '../../Cart';
import { CartContext } from '../CartContext';
import LoginButton from './LoginButton';
import { Link } from 'react-router-dom';
import RegButton from './RegButton';

const Navbar = () => {

  const classes = useStyles();
  const [cartItems, setCartItems] = useContext(CartContext);

  return (
    <div>
      <AppBar position="static" className={classes.appBar} color="inherit">
        <Toolbar>
          <Typography variant="h6" className={classes.title} color="inherit">
            <a href="/"><img src={logo} alt="WEBSHOP" height="25px" className={classes.image} /></a> WEBSHOP
          </Typography>
          <div className={classes.grow} />
          <LoginButton/>
          <RegButton/>
          <div className={classes.button}>
            <IconButton aria-label="Show cart items" color="inherit">
              <Badge color="secondary">
                <Cart />
              </Badge>
            </IconButton>
          </div>
        </Toolbar>
      </AppBar>

    </div>
  )
}
export default Navbar;