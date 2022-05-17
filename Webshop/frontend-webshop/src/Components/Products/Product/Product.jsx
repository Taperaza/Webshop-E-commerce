import React, {useContext} from 'react'
import { Card, CardMedia, CardContent, CardActions, Typography, IconButton, Button } from '@material-ui/core';
import { AddShoppingCart } from '@material-ui/icons';
import useStyles from './styles';
import { CartContext } from '../../CartContext';

const Product = ( {product} ) => {
    const classes = useStyles();
    const [cartItems, setCartItems] = useContext(CartContext);

    function handleAddToCart() {
          setCartItems([...cartItems, product]);
          localStorage.setItem("cartItems", JSON.stringify(cartItems));
    }
    
    return (
        <Card className={classes.root}>
            <CardMedia className={classes.media} image={product.ImgUrl} title={product.ProductName} />
            <CardContent>
                <div className={classes.cardContent}>
                    <Typography variant="h5" gutterBottom>
                        {product.ProductName}
                    </Typography>
                    <Typography variant="h5">
                        {product.ProductPrice} kn
                    </Typography>
                </div>
                <Typography variant="body2" color="textSecondary">{product.ProductDescription}</Typography>
            </CardContent>
            <CardActions disableSpacing className={classes.cardActions}>
                <Button size="small" color="secondary" >
                <a href={`/products/details/${product.Id}`}>
                    Details
                </a>
                </Button>
                <IconButton aria-label="Add to Cart" onClick={handleAddToCart}>
                    <AddShoppingCart />
                </IconButton>
            </CardActions>
        </Card>
    );
}

export default Product;
