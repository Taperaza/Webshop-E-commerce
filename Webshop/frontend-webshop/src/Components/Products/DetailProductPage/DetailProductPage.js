import React, { useEffect, useState, useContext } from 'react'
import Axios from 'axios'
import { useParams } from 'react-router-dom';
import { Card, CardMedia, CardContent, CardActions, Typography, IconButton, Button } from '@material-ui/core';
import { AddShoppingCart } from '@material-ui/icons';
import useStyles from './DetailStyle';
import {CartContext} from '../../CartContext'


function DetailProductPage()  {


    let { productId } = useParams();
    const [Product, setProduct] = useState([])
    const classes = useStyles();
    const [cartItems, setCartItems] = useContext(CartContext);

    useEffect(() => {
        Axios.get(`https://localhost:44388/api/Product/${productId}`)
            .then(response => {
                setProduct(response.data)

        })

    }, [])
  
    function handleAddToCart() {
      setCartItems([...cartItems, Product]);
      localStorage.setItem("cartItems", JSON.stringify(cartItems));
    }
  return (
    <div>
        <Card className={classes.root}>
            <CardMedia className={classes.media} image={Product.ImgUrl} title={Product.ProductName} />
            <CardContent>
                <div className={classes.cardContent}>
                    <Typography variant="h5" gutterBottom>
                        {Product.ProductName}
                    </Typography>
                    <Typography variant="h5">
                        {Product.ProductPrice} kn
                    </Typography>
                </div>
                <Typography variant="body2" color="textSecondary">{Product.ProductDescription}</Typography>
            </CardContent>
            <CardActions disableSpacing className={classes.cardActions}>
                <IconButton aria-label="Add to Cart" onClick={handleAddToCart}>
                    <AddShoppingCart />
                </IconButton>
            </CardActions>
        </Card>
    </div>
  )
}

export default DetailProductPage