import React, { useContext } from 'react';
import { Grid } from '@material-ui/core';
import Product from './Product/Product';
import axios from 'axios' ;
import { useParams } from 'react-router-dom';
import { CartContext } from '../CartContext';

const Products = () => {
    const [products, setProducts] = React.useState(null);
    let {CategoryId} = useParams();
    const [cartItems, setCartItems] = useContext(CartContext);

    React.useEffect(() => {
        axios.get(`https://localhost:44388/api/Product/Category/${CategoryId}`).then((response) =>{
            setProducts(response.data);
        });
    }, []);

    if (!products) return null;

    
    return (
        <main>
            <Grid container justify="center" spacing={4}>
                {products.map((product) =>(
                    <Grid item key ={product.Id} xs={12} sm={6} md={4} lg={3}>
                        <Product product={product} />
                    </Grid>
                    
                ))};

            </Grid>
        </main>
    );
    
}
export default Products;